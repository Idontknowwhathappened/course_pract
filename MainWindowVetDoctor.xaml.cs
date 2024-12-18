using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для MainWindowVetDoctor.xaml
    /// </summary>
    public partial class MainWindowVetDoctor : Window, INotifyPropertyChanged
    {
        public int _doctorID;

        public AppointmentsTableAdapter appointmentsTableAdapter = new AppointmentsTableAdapter();
        public ServicesTableAdapter servicesTableAdapter = new ServicesTableAdapter();
        public View_Stock_MedicinesTableAdapter stocksTableAdapter = new View_Stock_MedicinesTableAdapter();
        public QueriesTableAdapter queriesTableAdapter = new QueriesTableAdapter();

        private readonly bool isInitialized = false;
        private bool _isSessionActive;

        public bool IsSessionActive
        {
            get => _isSessionActive;
            set
            {
                _isSessionActive = value;
                OnPropertyChanged(nameof(IsSessionActive));
            }
        }
        public MainWindowVetDoctor(int doctorID)
        {
            InitializeComponent();
            _doctorID = doctorID;
            DataContext = this;
            IsSessionActive = false;
            var appointments = appointmentsTableAdapter.GetAppointmentsByDoctorID(doctorID, null);
            PatientList.ItemsSource = appointments;
            isInitialized = true;
            DoctorDatePicker.SelectedDate = DateTime.Today;
        }
        private void SetComboBoxState(ComboBox comboBox, bool isEnabled, bool? isChecked = null)
        {
            comboBox.IsEnabled = isEnabled;
            if (isChecked.HasValue)
                _ = isEnabled;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadComboBoxes()
        {
            MedicinesComboBox.ItemsSource = stocksTableAdapter.GetData();
            MedicinesComboBox.DisplayMemberPath = "medicine_name";
            MedicinesComboBox.SelectedValuePath = "medicine_id";
            ServicesComboBox.ItemsSource = servicesTableAdapter.GetData();
            ServicesComboBox.DisplayMemberPath = "name";
            ServicesComboBox.SelectedValuePath = "service_id";
        }

        private void StartAppointment_Click(object sender, RoutedEventArgs e)
        {
            IsSessionActive = true;
            LoadComboBoxes();
        }

        private void PatientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatientList.SelectedItem is DataRowView selectedRow)
            {
                ownerNameTextBlock.Text = $"Владелец - {selectedRow["owner_name"]}";
                patientNameTextBlock.Text = $"Пациент - {selectedRow["patient_name"]}";
                appointmentNameTextBlock.Text = $"Приём - {selectedRow["service_or_problem_name"]}";
                Complaint.Text = selectedRow["desc_problem"].ToString();
            }
        }
        private void DoctorDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isInitialized || _doctorID == 0 || !DoctorDatePicker.SelectedDate.HasValue)
            {
                return;
            }
            _isSessionActive = false;

            DateTime selectedDate = DoctorDatePicker.SelectedDate.Value;

            var appointments = appointmentsTableAdapter.GetAppointmentsByDoctorID(_doctorID, selectedDate);

            PatientList.ItemsSource = appointments;

            if (appointments.Count == 0)
            {
                ownerNameTextBlock.Text = "Владелец: -";
                patientNameTextBlock.Text = "Пациент: -";
                Complaint.Text = string.Empty;
            }
        }


        private void CompleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (PatientList.SelectedItem is DataRowView selectedRow)
            {
                try
                {
                    var appointmentId = Convert.ToInt32(selectedRow["appointment_id"]);
                    var patientId = Convert.ToInt32(selectedRow["patient_id"]);
                    var diagnosis = Diagnosis.Text;
                    var notes = Notes.Text;
                    var weight = Convert.ToDecimal(Weight.Text);

                    // Создаем таблицы для услуг и медикаментов
                    DataTable servicesTable = new DataTable();
                    servicesTable.Columns.Add("service_id", typeof(int));
                    servicesTable.Columns.Add("count", typeof(int));

                    DataTable medicinesTable = new DataTable();
                    medicinesTable.Columns.Add("medicine_id", typeof(int));
                    medicinesTable.Columns.Add("count", typeof(int));
                    medicinesTable.Columns.Add("comment", typeof(string));

                    // Добавляем данные в таблицу услуг, если выбрано
                    if (IsService.IsChecked == true && ServicesComboBox.SelectedValue != null)
                    {
                        int serviceId = Convert.ToInt32(ServicesComboBox.SelectedValue);
                        servicesTable.Rows.Add(serviceId, 1);
                    }

                    // Добавляем данные в таблицу медикаментов, если выбрано
                    if (IsMedicine.IsChecked == true && MedicinesComboBox.SelectedValue != null)
                    {
                        int medicineId = Convert.ToInt32(MedicinesComboBox.SelectedValue);
                        var comment = CommentTextBox.Text;
                        medicinesTable.Rows.Add(medicineId, 1, comment ?? string.Empty);
                    }

                    // Вызов процедуры через TableAdapter
                    queriesTableAdapter.CompleteAppointment(
                        appointmentId,
                        diagnosis,
                        string.IsNullOrWhiteSpace(notes) ? null : notes,
                        _doctorID,
                        patientId,
                        DateTime.Now,
                        weight,
                        servicesTable,
                        medicinesTable
                    );
                    IsSessionActive = false; // Завершаем сессию
                    selectedRow["app_status"] = "Завершено";
                    PatientList.Items.Refresh();
                    MessageBox.Show("Прием успешно завершен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите запись пациента из списка.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void IsMedicine_Checked(object sender, RoutedEventArgs e)
        {
            MedicinesComboBox.IsEnabled = true;
            CommentTextBox.IsEnabled = true;
        }

        private void IsMedicine_Unchecked(object sender, RoutedEventArgs e)
        {
            MedicinesComboBox.IsEnabled = false;
            CommentTextBox.IsEnabled = false;
        }


        private void IsService_Unchecked(object sender, RoutedEventArgs e)
            => SetComboBoxState(ServicesComboBox, false, IsMedicine.IsChecked);

        private void IsService_Checked(object sender, RoutedEventArgs e)
        => SetComboBoxState(ServicesComboBox, true);

        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            if (PatientList.SelectedItem is DataRowView selectedRow)
            {
                int id = Convert.ToInt32(selectedRow["patient_id"]);
                HistoryAppointment historyAppointment = new HistoryAppointment(id);
                historyAppointment.Show();
            }
            else
            {
                MessageBox.Show("Выберите элемент из списка.");
            }
        }
    }
}