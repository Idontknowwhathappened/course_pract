using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для PageAppointment.xaml
    /// </summary>
    public partial class PageAppointment : Page
    {
        private int _patientId;
        public AppointmentsTableAdapter appointmentsTableAdapter;
        public ObservableCollection<Appointment> NextThreeMonthsAppointments { get; set; }
        public ObservableCollection<Appointment> PreviousThreeMonthsAppointments { get; set; }
        public PageAppointment(int patientId)
        {
            InitializeComponent();
            appointmentsTableAdapter = new AppointmentsTableAdapter();
            _patientId = patientId;

            // Инициализация коллекций
            NextThreeMonthsAppointments = new ObservableCollection<Appointment>();
            PreviousThreeMonthsAppointments = new ObservableCollection<Appointment>();

            var appointmentManager = new AppointmentManager(patientId);

            // Заполняем коллекции
            var upcomingAppointments = appointmentManager.GetUpcomingAppointments();
            var archivedAppointments = appointmentManager.GetArchivedAppointments();

            // Очищаем старые данные и добавляем новые
            NextThreeMonthsAppointments.Clear();
            foreach (var appointment in upcomingAppointments)
            {
                NextThreeMonthsAppointments.Add(appointment);
            }

            PreviousThreeMonthsAppointments.Clear();
            foreach (var appointment in archivedAppointments)
            {
                PreviousThreeMonthsAppointments.Add(appointment);
            }

            DataContext = this;
        }

        private void MakeAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is ManagerWindow parentWindow && parentWindow.MainFrame != null)
            {
                parentWindow.MainFrame.Navigate(new PageMakeAppointment(_patientId));
            }
        }

        public void UpdatePetData(Pet selectedPet)
        {
            if (selectedPet != null)
            {
                _patientId = selectedPet.Id;

                var appointmentManager = new AppointmentManager(_patientId);
                var upcomingAppointments = appointmentManager.GetUpcomingAppointments();
                var archivedAppointments = appointmentManager.GetArchivedAppointments();

                // Очищаем и обновляем коллекции
                NextThreeMonthsAppointments.Clear();
                foreach (var appointment in upcomingAppointments)
                {
                    NextThreeMonthsAppointments.Add(appointment);
                }

                PreviousThreeMonthsAppointments.Clear();
                foreach (var appointment in archivedAppointments)
                {
                    PreviousThreeMonthsAppointments.Add(appointment);
                }

                // Уведомляем WPF о том, что данные изменились
                OnPropertyChanged(nameof(NextThreeMonthsAppointments));
                OnPropertyChanged(nameof(PreviousThreeMonthsAppointments));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    public class AppointmentManager
    {
        private readonly int _patientId;
        private readonly AppointmentsTableAdapter _upcomingAppointmentsAdapter = new AppointmentsTableAdapter();
        private readonly AppointmentsTableAdapter _archivedAppointmentsAdapter = new AppointmentsTableAdapter();

        public AppointmentManager(int patientId)
        {
            _patientId = patientId;
        }

        public List<Appointment> GetUpcomingAppointments()
        {
            var appointmentsTable = _upcomingAppointmentsAdapter.GetUpcomingAppointmentsPatient(_patientId);
            return MapAppointments(appointmentsTable);
        }

        public List<Appointment> GetArchivedAppointments()
        {
            var appointmentsTable = _archivedAppointmentsAdapter.GetArchivedAppointmentsForPatient(_patientId);
            return MapAppointments(appointmentsTable);
        }

        private List<Appointment> MapAppointments(DataTable dataTable)
        {
            var appointments = new List<Appointment>();

            foreach (DataRow row in dataTable.Rows)
            {
                appointments.Add(new Appointment
                {
                    AppointmentId = row["appointment_id"] != DBNull.Value ? (int)row["appointment_id"] : 0,
                    AppoinmentName = row["appointment_name"]?.ToString() ?? "Не указано",
                    PatientName = row["patient_name"]?.ToString(),
                    PatientType = row["patient_kind"]?.ToString(),
                    PatientAge = row["patient_age"] != DBNull.Value ? (int)row["patient_age"] : 0,
                    DoctorName = row["doctor_name"]?.ToString(),
                    Description = row["desc_problem"]?.ToString(),
                    AppointmentDate = row["appointment_date"] != DBNull.Value ? (DateTime)row["appointment_date"] : DateTime.MinValue,
                    AppointmentTime = row["appointment_time"] != DBNull.Value ? (TimeSpan)row["appointment_time"] : TimeSpan.Zero,
                    AppointmentStatus = row["app_status"]?.ToString(),
                    AdditionalServices = row["additional_services"]?.ToString()
                });
            }

            return appointments;
        }
    }
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string AppoinmentName { get; set; }
        public string PatientName { get; set; }
        public string PatientType { get; set; }
        public int PatientAge { get; set; }
        public string DoctorName { get; set; }
        public string Description { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string AppointmentStatus { get; set; }
        public string AdditionalServices { get; set; }
        public string AppointmentDateTime => $"{AppointmentDate:dd.MM.yyyy} {AppointmentTime:hh\\:mm}";
        public decimal TotalAmount { get; set; }

    }
}