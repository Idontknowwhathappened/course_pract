using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для PageEditOwner.xaml
    /// </summary>
    public partial class PageEditOwner : Page
    {
        public int OwnerID;
        private readonly OwnersTableAdapter ownersTableAdapter = new OwnersTableAdapter();
        private readonly QueriesTableAdapter queriesTableAdapter = new QueriesTableAdapter();
        public ManagerWindow ParentWindow { get; set; }

        public PageEditOwner(int ownerID, ManagerWindow parentWindow)
        {
            InitializeComponent();
            OwnerID = ownerID;
            ParentWindow = parentWindow;
            LoadOwnerInformation();
        }

        private void LoadOwnerInformation()
        {
            var ownerData = ownersTableAdapter.GetInformationByOwnerID(OwnerID);
            if (ownerData.Rows.Count > 0)
            {
                var row = ownerData.Rows[0];
                OwnerNameTxb.Text = row["owner_name"].ToString();

                OwnerPhoneTxb.Text = row["phone_number"].ToString();
                OwnerEmailTxb.Text = row["email"].ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Разделение полного имени на части
                var fullName = OwnerNameTxb.Text;
                var nameParts = fullName.Split(' ');

                string firstName = string.Empty;
                string lastName = string.Empty;
                string patronymic = string.Empty;

                if (nameParts.Length >= 3)
                {
                    lastName = nameParts[0]; // Фамилия
                    firstName = nameParts[1]; // Имя
                    patronymic = nameParts[2]; // Отчество
                }
                else if (nameParts.Length == 2)
                {
                    lastName = nameParts[0]; // Фамилия
                    firstName = nameParts[1]; // Имя
                }
                else if (nameParts.Length == 1)
                {
                    lastName = nameParts[0]; // Только фамилия
                }

                // Обновление данных через процедуру
                queriesTableAdapter.UpdateOwnerDetails(
                    OwnerID,
                    firstName, // Имя
                    lastName,  // Фамилия
                    patronymic, // Отчество
                    OwnerPhoneTxb.Text,
                    OwnerEmailTxb.Text
                );

                MessageBox.Show("Данные успешно обновлены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack(); // Возврат к предыдущей странице
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddNewPetButton_Click(object sender, RoutedEventArgs e)
        {
            new AddPetWindow(OwnerID).ShowDialog();
            if (ParentWindow != null)
            {
                ParentWindow.PatientID = 0;
                ParentWindow.LoadPetsByOwner();
            }
        }
    }
}
