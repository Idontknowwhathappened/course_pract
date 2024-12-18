using System;
using System.Data;
using System.Windows;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    public partial class AddPetWindow : Window
    {
        private int _ownerId;
        private readonly KindTableAdapter _kindsTableAdapter = new KindTableAdapter();
        private readonly SexTableAdapter _sexesTableAdapter = new SexTableAdapter();
        private readonly PatientsTableAdapter _patientsTableAdapter = new PatientsTableAdapter();

        public AddPetWindow(int ownerId)
        {
            InitializeComponent();
            _ownerId = ownerId;
            LoadKinds();
            LoadSexes();
        }

        private void LoadKinds()
        {
            var kindsTable = _kindsTableAdapter.GetData();
            KindComboBox.ItemsSource = kindsTable.DefaultView;
            KindComboBox.DisplayMemberPath = "name";
            KindComboBox.SelectedValuePath = "kind_id";
        }

        private void LoadSexes()
        {
            var sexesTable = _sexesTableAdapter.GetData();
            SexComboBox.ItemsSource = sexesTable.DefaultView;
            SexComboBox.DisplayMemberPath = "name";
            SexComboBox.SelectedValuePath = "sex_id";
        }

        private void AddPetButton_Click(object sender, RoutedEventArgs e)
        {
            string petName = PetNameTextBox.Text.Trim();
            DateTime? birthDate = BirthDatePicker.SelectedDate;
            int? kindId = KindComboBox.SelectedValue as int?;
            int? sexId = SexComboBox.SelectedValue as int?;

            if (string.IsNullOrWhiteSpace(petName) || birthDate == null || kindId == null || sexId == null)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _patientsTableAdapter.AddPet(_ownerId, petName, kindId.Value, sexId.Value, birthDate.Value);
                MessageBox.Show("Питомец успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении питомца: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}