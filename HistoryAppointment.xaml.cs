using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для HistoryAppointment.xaml
    /// </summary>
    public partial class HistoryAppointment : Window
    {
        public int PatientID { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>();

        public AppointmentsTableAdapter appointmentsTableAdapter = new AppointmentsTableAdapter();

        public HistoryAppointment(int patientID)
        {
            InitializeComponent();
            DataContext = this;
            PatientID = patientID;

            if (appointmentsTableAdapter.GetAppointmentWithPriceByPatientID(patientID) is DataTable history)
            {
                Appointments.Clear();
                foreach (DataRow row in history.Rows)
                {
                    DateTime dateTime = row["app_date_time"] != DBNull.Value
                                                ? DateTime.Parse(row["app_date_time"].ToString())
                                                : DateTime.MinValue;
                    Appointments.Add(new Appointment
                    {
                        AppoinmentName = row["appointment_name"]?.ToString() ?? "Не указано",
                        DoctorName = row["doctor_name"]?.ToString(),
                        Description = row["desc_problem"]?.ToString(),
                        AppointmentDate = dateTime.Date,
                        AppointmentTime = dateTime.TimeOfDay,
                    });
                }
            }

            DataContext = this;
        }

        public void UpdateHistory(Pet selectedPet)
        {
            if (selectedPet != null)
            {
                PatientID = selectedPet.Id;
                Appointments.Clear();
                if (PatientID > 0)
                {
                    if (appointmentsTableAdapter.GetAppointmentWithPriceByPatientID(PatientID) is DataTable historyData)
                    {
                        foreach (DataRow row in historyData.Rows)
                        {
                            DateTime dateTime = row["app_date_time"] != DBNull.Value
                                                ? DateTime.Parse(row["app_date_time"].ToString())
                                                : DateTime.MinValue;

                            Appointments.Add(new Appointment
                            {
                                AppoinmentName = row["appointment_name"]?.ToString() ?? "Не указано",
                                AppointmentDate = dateTime.Date,
                                AppointmentTime = dateTime.TimeOfDay,
                                DoctorName = row["doctor_name"]?.ToString(),
                                Description = row["desc_problem"]?.ToString(),
                            });
                        }
                    }
                }
                // Уведомляем WPF о том, что данные изменились
                OnPropertyChanged(nameof(Appointments));
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AppointmentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AppointmentsList.SelectedItem is Appointment selectedAppointment)
            {
                NameAppoinment.Text = selectedAppointment.AppoinmentName;
                Veterinarian.Text = selectedAppointment.DoctorName;
                AppointmentDateTextBlock.Text = selectedAppointment.AppointmentDate.ToString("dd.MM.yyyy");
                AppointmentTimeTextBlock.Text = selectedAppointment.AppointmentTime.ToString(@"hh\:mm");
                ComplaintsTextBlock.Text = selectedAppointment.Description;
            }
        }

    }

}
