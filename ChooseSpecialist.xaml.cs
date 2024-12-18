using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    public partial class ChooseSpecialist : Page
    {
        // Константы для цветов
        private static readonly Brush DefaultBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC28DAB"));
        private static readonly Brush DefaultBorder = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAB648C"));
        private static readonly Brush SelectedBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAB648C"));
        private static readonly Brush SelectedBorder = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC28DAB"));

        private string specialization;
        private int? specializationID;
        private int doctorID;
        private readonly int? serviceID;
        private readonly int petID;
        private DateTime selectedDay;
        private Button selectedDayButton;
        private Button selectedTimeButton;
        public DoctorsTableAdapter doctorsTableAdapter = new DoctorsTableAdapter();
        public PatientsTableAdapter patientsTableAdapter = new PatientsTableAdapter();
        public Doctor_ScheduleTableAdapter doctor_SchedulesTableAdapter = new Doctor_ScheduleTableAdapter();
        public ServiceWithSpecialistTableAdapter serviceWithSpecialistTableAdapter = new ServiceWithSpecialistTableAdapter();

        public ChooseSpecialist(int? serviceId, int? specializationId, int petId)
        {
            InitializeComponent();
            // Присваиваем значения полям
            serviceID = serviceId ?? serviceID;
            specializationID = specializationId ?? specializationID;

            // Определяем список докторов в зависимости от наличия serviceId или specializationId
            var doctors = serviceId != null
                ? serviceWithSpecialistTableAdapter.GetSpecialistsByService(serviceId)
                : (DataTable)doctorsTableAdapter.GetDoctorsBySpecialization(specializationId);

            DoctorsList.ItemsSource = doctors.AsEnumerable()
                .Select(row => new Doctor
                {
                    DoctorId = row.Field<int>("doctor_id"),
                    Name = row.Field<string>("doctor_name")
                })
                .ToList();

            // Устанавливаем специализацию
            SetSpecialization(doctors.AsEnumerable());

            // Инициализируем сетки для недели
            InitializeWeekGrids();
            petID = petId;
        }


        private void SetSpecialization(IEnumerable<DataRow> doctors)
        {
            if (doctors != null && doctors.Any())
            {
                specialization = doctors.First()["specialization_name"].ToString();
                doctorID = (int)doctors.First()["doctor_id"];
                SpecializationTextBlock.Text = $"Выбор специалиста - {specialization}";
            }
        }

        private void DoctorsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoctorsList.SelectedItem is Doctor selectedDoctor)
            {
                doctorID = selectedDoctor.DoctorId;
                ColorWeekGrids();
            }
        }

        private void InitializeWeekGrids()
        {
            GenerateWeekGrid(CurrentWeekGrid, DateTime.Today);
            GenerateWeekGrid(NextWeekGrid, DateTime.Today.AddDays(7));
        }

        private void GenerateWeekGrid(UniformGrid weekGrid, DateTime startDate)
        {
            weekGrid.Children.Clear();

            foreach (var day in Enumerable.Range(0, 7).Select(i => startDate.AddDays(i)))
            {
                var dayButton = CreateDayButton(day);
                dayButton.Click += DayButton_Click;
                weekGrid.Children.Add(dayButton);
            }
        }

        private void ColorWeekGrids()
        {
            DateTime today = DateTime.Today;

            for (int i = 0; i < 7; i++)
            {
                DateTime currentDay = today.AddDays(i);
                Button dayButton = (Button)CurrentWeekGrid.Children[i];
                UpdateDayButtonColor(dayButton, currentDay);
            }

            for (int i = 0; i < 7; i++)
            {
                DateTime nextWeekDay = today.AddDays(i + 7);
                Button dayButton = (Button)NextWeekGrid.Children[i];
                UpdateDayButtonColor(dayButton, nextWeekDay);
            }
        }

        private Button CreateDayButton(DateTime day)
        {
            Button dayButton = new Button
            {
                Content = day.ToString("ddd, dd MMM"),
                FontFamily = (FontFamily)FindResource("CustomFont"),
                Margin = new Thickness(5),
                Background = Brushes.LightGray,
                BorderBrush = Brushes.LightGray,
                FontSize = 11,
                Tag = day // Сохраняем дату в Tag для использования при нажатии
            };

            return dayButton;
        }

        private void UpdateDayButtonColor(Button button, DateTime day)
        {
            if (IsDayAvailable(day))
            {
                button.IsEnabled = true;
                button.Background = DefaultBackground;
                button.BorderBrush = DefaultBorder;
            }
            else
            {
                button.IsEnabled = false;
                button.Background = Brushes.LightGray;
                button.BorderBrush = Brushes.LightGray;
            }
        }

        private bool IsDayAvailable(DateTime day)
        {
            var availableSlots = doctor_SchedulesTableAdapter.GetAvailableSlots(doctorID, day);
            return availableSlots.Rows.Count > 0;
        }

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            SetButtonSelection(sender as Button, ref selectedDayButton);
            selectedDay = (DateTime)selectedDayButton.Tag;
            UpdateTimeSlots(selectedDay);
        }
        private void SetButtonSelection(Button newButton, ref Button selectedButton)
        {
            if (selectedButton != null)
            {
                selectedButton.Background = DefaultBackground;
                selectedButton.BorderBrush = DefaultBorder;
            }

            selectedButton = newButton;

            if (selectedButton != null)
            {
                selectedButton.Background = SelectedBackground;
                selectedButton.BorderBrush = SelectedBorder;
            }
        }

        private void UpdateTimeSlots(DateTime day)
        {
            TimeSlotsGrid.Children.Clear();

            var availableSlots = doctor_SchedulesTableAdapter.GetAvailableSlots(doctorID, day);

            foreach (DataRow slot in availableSlots.Rows)
            {
                Button timeButton = new Button
                {
                    Content = DateTime.Parse(slot["AvailableTime"].ToString()).ToString("HH:mm"),
                    Margin = new Thickness(5),
                    FontFamily = (FontFamily)FindResource("CustomFont"),
                    FontSize = 11,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC28DAB")),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAB648C"))
                };

                timeButton.Click += TimeButton_Click; // Добавляем событие клика для кнопок времени
                TimeSlotsGrid.Children.Add(timeButton);
            }
        }

        private void TimeButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button timeButton)
            {
                // Сбрасываем цвет предыдущей выбранной кнопки времени
                if (selectedTimeButton != null)
                {
                    selectedTimeButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC28DAB"));
                    selectedTimeButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAB648C"));
                }

                // Устанавливаем выбранную кнопку времени и выделяем её цветом
                selectedTimeButton = timeButton;
                selectedTimeButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAB648C"));
                selectedTimeButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC28DAB"));
            }
        }

        private void ScheduleAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTimeButton == null || selectedDayButton == null)
            {
                MessageBox.Show("Выберите дату и время приёма.");
                return;
            }

            var selectedTime = DateTime.Parse(selectedTimeButton.Content.ToString());
            TimeSpan appointmentTime = selectedTime.TimeOfDay;
            var problems = ProblemDescriptionTextBox.Text;

            try
            {
                using (var adapter = new AppointmentsTableAdapter())
                {
                    adapter.ScheduleAppointment(doctorID, selectedDay, appointmentTime, petID, problems, serviceID);
                    MessageBox.Show("Запись на приём успешно выполнена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при записи на приём: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
    }

}
