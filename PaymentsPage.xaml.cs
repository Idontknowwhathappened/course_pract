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
    /// Логика взаимодействия для PaymentsPage.xaml
    /// </summary>
    public partial class PaymentsPage : Page
    {
        public int PatientID { get; set; }
        public AppointmentsTableAdapter appointmentsTableAdapter = new AppointmentsTableAdapter();
        public QueriesTableAdapter queriesTableAdapter = new QueriesTableAdapter();
        public Payment_MethodsTableAdapter payment_MethodsTableAdapter = new Payment_MethodsTableAdapter();
        public ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>();
        public ObservableCollection<PaymentMethod> PaymentMethods { get; set; } = new ObservableCollection<PaymentMethod>();


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public PaymentsPage(int patient_id)
        {
            InitializeComponent();
            PatientID = patient_id;
            LoadPaymentMethods();
            if (appointmentsTableAdapter.GetAppointmentWithPriceByPatientID(PatientID) is DataTable historyData)
            {
                foreach (DataRow row in historyData.Rows)
                {
                    DateTime dateTime = row["app_date_time"] != DBNull.Value
                                        ? DateTime.Parse(row["app_date_time"].ToString())
                                        : DateTime.MinValue;

                    Appointments.Add(new Appointment
                    {
                        AppointmentId = Convert.ToInt32(row["appointment_id"]),
                        AppoinmentName = row["appointment_name"]?.ToString() ?? "Не указано",
                        AppointmentDate = dateTime.Date,
                        AppointmentTime = dateTime.TimeOfDay,
                        DoctorName = row["doctor_name"]?.ToString(),
                        Description = row["desc_problem"]?.ToString(),
                        TotalAmount = Convert.ToDecimal(row["service_price"])
                    });
                }
            }

            DataContext = this;
        }
        private void LoadPaymentMethods()
        {
            var methodsTable = payment_MethodsTableAdapter.GetData();

            foreach (DataRow row in methodsTable.Rows)
            {
                PaymentMethods.Add(new PaymentMethod
                {
                    MethodID = Convert.ToInt32(row["method_id"]),
                    Name = row["name"].ToString(),
                });
            }
            OnPropertyChanged(nameof(PaymentMethods));
        }

        private void GenerateInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentsList.SelectedItem is Appointment selectedAppointment)
            {
                if (AppointmentsList.ItemContainerGenerator.ContainerFromItem(selectedAppointment) is ListViewItem)
                {
                    if (selectedAppointment.TotalAmount == 0.0m)
                    {
                        try
                        {
                            queriesTableAdapter.GenerateInvoiceFromAppointment(selectedAppointment.AppointmentId, null);
                            MessageBox.Show("Счет успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                            CheckAndUpdateInvoices(selectedAppointment.AppointmentId);

                            RefreshAppointments();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при генерации счета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else if (PaymentMethodComboBox?.SelectedValue is int methodId)
                    {
                        try
                        {
                            queriesTableAdapter.GenerateInvoiceFromAppointment(selectedAppointment.AppointmentId, methodId);
                            MessageBox.Show("Счет успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                            CheckAndUpdateInvoices(selectedAppointment.AppointmentId);

                            RefreshAppointments();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при генерации счета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите способ оплаты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }


        private void RefreshAppointments()
        {
            Appointments.Clear();

            if (appointmentsTableAdapter.GetAppointmentWithPriceByPatientID(PatientID) is DataTable historyData)
            {
                foreach (DataRow row in historyData.Rows)
                {
                    DateTime dateTime = row["app_date_time"] != DBNull.Value
                                        ? DateTime.Parse(row["app_date_time"].ToString())
                                        : DateTime.MinValue;

                    Appointments.Add(new Appointment
                    {
                        AppointmentId = Convert.ToInt32(row["appointment_id"]),
                        AppoinmentName = row["appointment_name"]?.ToString() ?? "Не указано",
                        AppointmentDate = dateTime.Date,
                        AppointmentTime = dateTime.TimeOfDay,
                        DoctorName = row["doctor_name"]?.ToString(),
                        Description = row["desc_problem"]?.ToString(),
                        TotalAmount = Convert.ToDecimal(row["service_price"])
                    });
                }
            }

            OnPropertyChanged(nameof(Appointments));
        }


        private void CheckAndUpdateInvoices(int appointmentId)
        {
            bool invoiceExists = Convert.ToBoolean(queriesTableAdapter.CheckInvoiceExists(appointmentId));
            if (GenerateInvoice != null)
            {
                GenerateInvoice.IsEnabled = !invoiceExists;
                if (invoiceExists)
                {
                    GenerateInvoice.Content = "Счёт уже создан";
                    PaymentMethodComboBox.IsEnabled = false;
                }
                else
                {
                    GenerateInvoice.Content = "Сгенерировать счёт";
                }
            }
        }

        public void UpdateAccount(Pet selectedPet)
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
                                AppointmentId = Convert.ToInt32(row["appointment_id"]),
                                AppoinmentName = row["appointment_name"]?.ToString() ?? "Не указано",
                                AppointmentDate = dateTime.Date,
                                AppointmentTime = dateTime.TimeOfDay,
                                DoctorName = row["doctor_name"]?.ToString(),
                                Description = row["desc_problem"]?.ToString(),
                                TotalAmount = Convert.ToDecimal(row["service_price"])
                            });
                        }
                    }
                }
                OnPropertyChanged(nameof(Appointments));
            }
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
                TotalAmount.Text = selectedAppointment.TotalAmount.ToString("C");

                // Проверяем, если стоимость больше 0, включаем ComboBox
                PaymentMethodComboBox.IsEnabled = selectedAppointment.TotalAmount > 0.0m;

                // Обновляем состояние кнопки для генерации счёта
                CheckAndUpdateInvoices(selectedAppointment.AppointmentId);
            }
            else
            {
                // Если ничего не выбрано, блокируем ComboBox
                PaymentMethodComboBox.IsEnabled = false;
            }
        }

    }
    public class PaymentMethod
    {
        public int MethodID { get; set; }
        public string Name { get; set; }
    }
}
