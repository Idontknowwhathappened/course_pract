using System;
using System.Windows;
using System.Windows.Controls;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для PageMainAppointment.xaml
    /// </summary>
    public partial class SearchClientPage : Page
    {
        public OwnersTableAdapter Adapter { get; set; } = new OwnersTableAdapter();
        public PatientsTableAdapter PatientsTableAdapter { get; set; } = new PatientsTableAdapter();
        public ManagerWindow ParentWindow { get; set; }

        public SearchClientPage()
        {
            InitializeComponent();
        }

        private void SearchClient_Click(object sender, RoutedEventArgs e)
        {
            string searchInput = PhoneMaskedTextBox.Text;
            var ownersRows = Adapter.SearchClient(searchInput, searchInput);

            if (ownersRows.Rows.Count > 0)
            {
                int ownerId = Convert.ToInt32(ownersRows.Rows[0]["owner_id"]);
                var patientsRows = PatientsTableAdapter.SearchPatientsByOwner(ownerId);
                if (patientsRows.Rows.Count > 0)
                {
                    int patientId = Convert.ToInt32(patientsRows.Rows[0]["patient_id"]);
                    ParentWindow?.SetClientFound(ownerId, patientId);
                }
                else
                {
                    MessageBoxResult addPetResult = MessageBox.Show(
                        "У клиента нет питомцев. Хотите добавить нового питомца?",
                        "Информация",
                        MessageBoxButton.YesNo);

                    if (addPetResult == MessageBoxResult.Yes)
                    {
                        new AddPetWindow(ownerId).ShowDialog();
                        // Обновляем список питомцев после добавления
                        ParentWindow.PatientID = 0;
                        ParentWindow?.LoadPetsByOwner();
                    }
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(
                    "Клиент не найден. Хотите зарегистрировать нового клиента?",
                    "Информация",
                    MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    new RegisterWindow().Show();
                }
            }
        }

    }
}
