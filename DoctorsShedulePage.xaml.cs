using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для DoctorsShedulePage.xaml
    /// </summary>
    public partial class DoctorsShedulePage : Page
    {
        private readonly Doctor_ScheduleTableAdapter doctorScheduleTableAdapter = new Doctor_ScheduleTableAdapter();
        private readonly DoctorsTableAdapter doctorsTableAdapter = new DoctorsTableAdapter();

        public DoctorsShedulePage()
        {
            InitializeComponent();
            LoadDoctors();
        }
        private void AddOrUpdateScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorsComboBox.SelectedValue == null)
            {
                MessageBox.Show("Пожалуйста, выберите врача.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int doctorID = Convert.ToInt32(DoctorsComboBox.SelectedValue);
            string workDay = WorkDayComboBox.Text;

            if (!TimeSpan.TryParse(StartTimeTextBox.Text, out TimeSpan startTime) || !TimeSpan.TryParse(EndTimeTextBox.Text, out TimeSpan endTime))
            {
                MessageBox.Show("Пожалуйста, введите корректное время.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(DurationTextBox.Text, out int appointmentDuration) || appointmentDuration <= 0)
            {
                MessageBox.Show("Длительность записи должна быть положительным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ScheduleDataGrid.SelectedItem is DataRowView selectedRow)
            {
                // Извлекаем ID выбранного графика
                int scheduleID = Convert.ToInt32(selectedRow["ScheduleID"]);

                // Обновляем график
                doctorScheduleTableAdapter.UpdateSchedule(
                    scheduleID,
                    doctorID,
                    workDay,
                    startTime,
                    endTime,
                    appointmentDuration
                );

                MessageBox.Show("График успешно обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Проверяем, существует ли график для этого врача и дня
                var existingSchedule = doctorScheduleTableAdapter.GetScheduleByDoctorAndDay(doctorID, workDay);
                if (existingSchedule.Rows.Count > 0)
                {
                    MessageBox.Show("График для этого врача и дня уже существует. Выберите его для изменения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Если нет выбранной строки, добавляем новый график
                doctorScheduleTableAdapter.InsertSchedule(
                    doctorID,
                    workDay,
                    startTime,
                    endTime,
                    appointmentDuration
                );

                MessageBox.Show("График успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Обновляем данные
            LoadSchedule(doctorID);
        }

        private void LoadDoctors()
        {
            var doctors = doctorsTableAdapter.GetData();
            DoctorsComboBox.ItemsSource = doctors;
        }

        private void LoadSchedule(int doctorID)
        {
            var schedule = doctorScheduleTableAdapter.GetScheduleByDoctorID(doctorID);
            ScheduleDataGrid.ItemsSource = schedule;
        }

        private void DoctorsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoctorsComboBox.SelectedValue != null)
            {
                int doctorID = Convert.ToInt32(DoctorsComboBox.SelectedValue);
                LoadSchedule(doctorID);
            }
        }

        private void ScheduleDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ScheduleDataGrid.SelectedItem is DataRowView selectedRow)
            {
                // Получение данных из выбранной строки
                string workDay = selectedRow["WorkDay"]?.ToString();
                TimeSpan? startTime = selectedRow["StartTime"] as TimeSpan?;
                TimeSpan? endTime = selectedRow["EndTime"] as TimeSpan?;
                int? appointmentDuration = selectedRow["AppointmentDuration"] as int?;

                // Заполнение полей интерфейса
                WorkDayComboBox.SelectedValue = workDay;
                StartTimeTextBox.Text = startTime?.ToString(@"hh\:mm");
                EndTimeTextBox.Text = endTime?.ToString(@"hh\:mm");
                DurationTextBox.Text = appointmentDuration?.ToString();
            }
            else
            {
                // Очистка полей, если строка не выбрана
                WorkDayComboBox.SelectedIndex = -1;
                StartTimeTextBox.Clear();
                EndTimeTextBox.Clear();
                DurationTextBox.Clear();
            }
        }
    }
}
