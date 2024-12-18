using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window, INotifyPropertyChanged
    {
        private readonly View_PatientsTableAdapter _adapter = new View_PatientsTableAdapter();
        public ICommand SelectPetCommand { get; }

        private int _patientID;
        public int PatientID
        {
            get => _patientID;
            set
            {
                if (_patientID != value)
                {
                    _patientID = value;
                    OnPropertyChanged(nameof(PatientID));
                    LoadPetsByOwner();
                }
            }
        }

        private int _ownerID;
        public int OwnerID
        {
            get => _ownerID;
            set
            {
                if (_ownerID != value)
                {
                    _ownerID = value;
                    OnPropertyChanged(nameof(OwnerID));
                    LoadPetsByOwner();
                }
            }
        }

        private Pet _selectedPet;
        public Pet SelectedPet
        {
            get => _selectedPet;
            set
            {
                if (_selectedPet != value)
                {
                    _selectedPet = value;
                    OnPropertyChanged(nameof(SelectedPet));
                    OnSelectedPetChanged(); // Метод для обновления логики
                }
            }
        }
        private bool isClientFound = false;
        public ObservableCollection<Pet> PetsList { get; set; } = new ObservableCollection<Pet>();

        private string _selectedPetName = "Выберите питомца";
        public string SelectedPetName
        {
            get => _selectedPetName;
            set
            {
                if (_selectedPetName != value)
                {
                    _selectedPetName = value;
                    OnPropertyChanged(nameof(SelectedPetName));
                }
            }
        }

        public ManagerWindow()
        {
            InitializeComponent();
            DataContext = this;
            SelectPetCommand = new DelegateCommand<object>(SelectPet);
            MainFrame.Navigate(new SearchClientPage { ParentWindow = this });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SelectPet(object selectedPet)
        {
            if (selectedPet is Pet pet)
            {
                SelectedPetName = pet.Name;
                PatientID = pet.Id;
            }
        }

        public void LoadPetsByOwner()
        {
            if (OwnerID <= 0) return;

            var dataTable = new VeterinaryClinicDataSet.View_PatientsDataTable();
            _adapter.FillByOwnerName(dataTable, OwnerID);

            PetsList.Clear();

            foreach (DataRow row in dataTable.Rows)
            {
                PetsList.Add(new Pet
                {
                    Id = (int)row["patient_id"],
                    Name = row["patient_name"].ToString()
                });
            }

            // Устанавливаем первого питомца как выбранного
            SelectedPet = PetsList.Count > 0 ? PetsList[0] : null;

            // Обновление выбранного имени питомца
            SelectedPetName = SelectedPet.Name;
        }


        public void SetClientFound(int ownerId, int patientId)
        {
            isClientFound = true;
            OwnerID = ownerId;
            PatientID = patientId;
            MainTabControl.IsEnabled = true;

            // Получаем питомца из списка питомцев
            Pet selectedPet = PetsList.FirstOrDefault(p => p.Id == patientId);

            // Передаем выбранного питомца на страницу
            var pageAppointment = new PageAppointment(patientId);
            pageAppointment.UpdatePetData(selectedPet);
            MainFrame.Navigate(pageAppointment);
            SelectedPetName = SelectedPet.Name;
        }

        private void OnSelectedPetChanged()
        {
            OnPropertyChanged(nameof(SelectedPet));

            // Проверка на null для предотвращения ошибок
            SelectedPetName = SelectedPet?.Name ?? "Выберите питомца";
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainFrame == null) return;

            if (e.Source is TabControl tabControl)
            {
                switch (tabControl.SelectedIndex)
                {
                    case 0: // Вкладка для записи на прием
                        if (isClientFound)
                        {
                            var pageAppointment = new PageAppointment(PatientID);
                            pageAppointment.UpdatePetData(SelectedPet);  // Обновляем питомца на странице
                            MainFrame.Navigate(pageAppointment);
                        }
                        break;

                    case 1: // Вкладка истории
                        var paymentsPage = new PaymentsPage(PatientID);
                        paymentsPage.UpdateAccount(SelectedPet); // Обновляем историю питомца
                        MainFrame.Navigate(paymentsPage);
                        break;

                    case 2:
                        var clientInformation = new PageClientInformation(PatientID, OwnerID, this);
                        clientInformation.UpdateClientInformation(SelectedPet);
                        MainFrame.Navigate(clientInformation);
                        break;
                    case 3:
                        MessageBoxResult result = MessageBox.Show(
                            "Вы уверены, что хотите переключиться на другого клиента?",
                            "Подтверждение переключения",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            ResetClientData(); // Сбрасываем данные клиента
                            MainFrame.Navigate(new SearchClientPage { ParentWindow = this });
                            MainTabControl.IsEnabled = false;
                        }
                        break;
                    case 4:
                        MessageBoxResult result1 = MessageBox.Show(
                            "Вы уверены, что хотите выйти из аккаунта?",
                            "Подтверждение выхода",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);
                        if (result1 == MessageBoxResult.Yes)
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            Close();
                        }
                        break;
                }
            }
        }

        private void ResetClientData()
        {
            // Сброс идентификаторов и текущих данных
            OwnerID = 0;
            PatientID = 0;
            SelectedPet = null;
            SelectedPetName = "Выберите питомца";

            // Очищаем список питомцев
            PetsList.Clear();

            // Устанавливаем флаг поиска клиента
            isClientFound = false;

            // Отключаем взаимодействие с вкладками
            MainTabControl.IsEnabled = false;
        }



        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is Pet selectedPet)
            {
                // Устанавливаем выбранного питомца
                SelectedPet = selectedPet;
                SelectedPetName = selectedPet.Name; // Обновляем имя питомца

                // Обновляем данные на текущей странице
                if (MainFrame.Content is HistoryAppointment historyAppointment)
                {
                    historyAppointment.UpdateHistory(selectedPet);
                }

                if (MainFrame.Content is PageAppointment pageAppointment)
                {
                    pageAppointment.UpdatePetData(selectedPet);
                }
                if (MainFrame.Content is PaymentsPage paymentsPage)
                {
                    paymentsPage.UpdateAccount(selectedPet);
                }

                if (MainFrame.Content is PageClientInformation clientInformationPage)
                {
                    clientInformationPage.UpdateClientInformation(selectedPet);
                }
            }
        }
    }
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}