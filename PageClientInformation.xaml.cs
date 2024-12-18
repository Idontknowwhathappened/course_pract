using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для PageClientInformation.xaml
    /// </summary>
    public partial class PageClientInformation : Page, INotifyPropertyChanged
    {
        public int PatientID { get; set; }
        public ManagerWindow ParentWindow { get; set; }
        public int OwnerID { get; set; }
        private readonly PatientsTableAdapter patientsTableAdapter = new PatientsTableAdapter();

        private string _petNameValue;
        public string PetNameValue
        {
            get => _petNameValue;
            set
            {
                if (_petNameValue != value)
                {
                    _petNameValue = value;
                    OnPropertyChanged(nameof(PetNameValue));
                }
            }
        }


        private string _petSex;
        public string PetSexValue
        {
            get => _petSex;
            set
            {
                if (_petSex != value)
                {
                    _petSex = value;
                    OnPropertyChanged(nameof(PetSexValue));
                }
            }
        }

        // Аналогично для остальных свойств
        private string _petKind;
        public string PetKindValue
        {
            get => _petKind;
            set
            {
                if (_petKind != value)
                {
                    _petKind = value;
                    OnPropertyChanged(nameof(PetKindValue));
                }
            }
        }

        private string _petBirth;
        public string PetBirthValue
        {
            get => _petBirth;
            set
            {
                if (_petBirth != value)
                {
                    _petBirth = value;
                    OnPropertyChanged(nameof(PetBirthValue));
                }
            }
        }

        private string _ownerName;
        public string OwnerNameValue
        {
            get => _ownerName;
            set
            {
                if (_ownerName != value)
                {
                    _ownerName = value;
                    OnPropertyChanged(nameof(OwnerNameValue));
                }
            }
        }


        public PageClientInformation(int patientID, int ownerID, ManagerWindow parentWindow)
        {
            InitializeComponent();
            PatientID = patientID;
            OwnerID = ownerID;
            DataContext = this;
            ParentWindow = parentWindow;
            LoadClientInformation();
        }

        public void UpdateClientInformation(Pet selectedPet)
        {
            if (selectedPet == null) return;

            // Обновляем идентификатор пациента
            PatientID = selectedPet.Id;

            // Получаем информацию о питомце из базы данных
            var clientInfo = patientsTableAdapter.GetInformationByPatientID(PatientID).Rows[0];

            // Обновляем свойства, привязанные к UI
            PetNameValue = clientInfo["name"].ToString();
            PetSexValue = clientInfo["sex"].ToString();
            PetKindValue = clientInfo["kind"].ToString();
            PetBirthValue = DateTime.Parse(clientInfo["birth_date"].ToString()).ToString("dd.MM.yyyy");
            OwnerNameValue = clientInfo["owner_name"].ToString();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            Console.WriteLine($"PropertyChanged: {propertyName}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void LoadClientInformation()
        {
            var clientInfo = patientsTableAdapter.GetInformationByPatientID(PatientID).Rows[0];

            PetNameValue = clientInfo["name"].ToString();
            PetSexValue = clientInfo["sex"].ToString();
            PetKindValue = clientInfo["kind"].ToString();
            PetBirthValue = DateTime.Parse(clientInfo["birth_date"].ToString()).ToString("dd.MM.yyyy");
            OwnerNameValue = clientInfo["owner_name"].ToString();
        }

        private void PackIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageEditOwner(OwnerID, ParentWindow));
        }
    }
}
