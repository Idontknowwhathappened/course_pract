using MaterialDesignThemes.Wpf;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для PageMakeAppointment.xaml
    /// </summary>
    public partial class PageMakeAppointment : Page
    {
        private readonly int _petId;
        private readonly ClientServicesViewTableAdapter clientServicesViewTableAdapter = new ClientServicesViewTableAdapter();
        public SpecializationsTableAdapter specializationsTableAdapter = new SpecializationsTableAdapter();
        public int doctorId = 0;

        public PageMakeAppointment(int petId)
        {
            InitializeComponent();
            _petId = petId;
            LoadSpecializations();
            LoadVaccinationServices();
            LoadFreeServices();
            LoadDiagnostServices();
            LoadSurgicalServices();
        }

        private void LoadVaccinationServices()
        {
            foreach (DataRow row in clientServicesViewTableAdapter.GetVaccinationData())
            {
                string servicesName = row["service_name"].ToString();
                decimal price = (decimal)row["price"];
                int serviceId = (int)row["service_id"];
                PackIconKind packIconKind = PackIconKind.Syringe;
                Button vaccinationButton = new Button
                {
                    Width = 200,
                    Height = 80,
                    FontFamily = (FontFamily)FindResource("CustomFont"),
                    Margin = new Thickness(10),
                    Content = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Children =
                        {
                            new PackIcon
                            {
                                Kind = packIconKind,
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Center,
                            },
                            new TextBlock
                            {
                                Text = servicesName,
                                FontSize = 13,
                                TextWrapping = TextWrapping.Wrap,
                                FontWeight = FontWeights.Bold,
                                TextAlignment = TextAlignment.Center,
                            },
                            new TextBlock
                            {
                                Text = price.ToString(),
                                FontSize = 12,
                                TextAlignment = TextAlignment.Center,
                            }
                        }
                    },
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC28DAB")),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAB648C")),
                    Tag = serviceId
                };
                vaccinationButton.Click += ServiceButton_Click;
                stackVaccinations.Children.Add(vaccinationButton);
            }
        }

        private void LoadFreeServices()
        {
            foreach (DataRow row in clientServicesViewTableAdapter.GetFreeServicesData())
            {
                string servicesName = row["service_name"].ToString();
                int serviceId = (int)row["service_id"];
                PackIconKind packIconKind = PackIconKind.Syringe;
                Button serviceButton = new Button
                {
                    Width = 200,
                    Height = 70,
                    FontFamily = (FontFamily)FindResource("CustomFont"),
                    Margin = new Thickness(10),
                    Content = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Children =
                        {
                            new PackIcon
                            {
                                Kind = packIconKind,
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Center,
                            },
                            new TextBlock
                            {
                                Text = servicesName,
                                FontSize = 13,
                                TextWrapping = TextWrapping.Wrap,
                                TextAlignment = TextAlignment.Center,
                            }
                        }
                    },
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC28DAB")),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAB648C")),
                    Tag = serviceId
                };
                serviceButton.Click += ServiceButton_Click;
                stackFreeServ.Children.Add(serviceButton);
            }
        }

        private void LoadSpecializations()
        {

            foreach (DataRow row in specializationsTableAdapter.GetData())
            {
                int specializationId = (int)row["specialization_id"];
                string specializationName = row["name"].ToString();
                PackIconKind iconKind;

                switch (specializationName)
                {
                    case "Терапевт":
                        iconKind = PackIconKind.Doctor;
                        break;
                    case "Хирург":
                        iconKind = PackIconKind.Needle;
                        break;
                    case "Кардиолог":
                        iconKind = PackIconKind.Heart;
                        break;
                    case "Дерматолог":
                        iconKind = PackIconKind.Allergy;
                        break;
                    case "Акушер":
                        iconKind = PackIconKind.Paw;
                        break;
                    case "Онколог":
                        iconKind = PackIconKind.Virus;
                        break;
                    default:
                        iconKind = PackIconKind.Help;
                        break;
                }


                Button specializationButton = new Button
                {
                    Width = 200,
                    Height = 60,
                    FontFamily = (FontFamily)FindResource("CustomFont"),
                    Margin = new Thickness(10),
                    Content = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Children =
                        {
                            new PackIcon
                            {
                                Kind = iconKind,
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Center,
                            },
                            new TextBlock
                            {
                                Text = specializationName,
                                FontSize = 13,
                                TextAlignment = TextAlignment.Center,
                            }
                        }
                    },
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC28DAB")),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAB648C")),
                    Tag = specializationId
                };
                specializationButton.Click += SpecializationButton_Click;
                stackSpecial.Children.Add(specializationButton);
            }
        }
        private void LoadDiagnostServices()
        {
            foreach (DataRow row in clientServicesViewTableAdapter.GetDiagnosticServicesData())
            {
                string servicesName = row["service_name"].ToString();
                decimal price = (decimal)row["price"];
                int serviceId = (int)row["service_id"];
                PackIconKind packIconKind = PackIconKind.Syringe;
                Button diagnosticButton = new Button
                {
                    Width = 200,
                    Height = 70,
                    FontFamily = (FontFamily)FindResource("CustomFont"),
                    Margin = new Thickness(10),
                    Content = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Children =
                        {
                            new PackIcon
                            {
                                Kind = packIconKind,
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Center,
                            },
                            new TextBlock
                            {
                                Text = servicesName,
                                FontSize = 13,
                                TextWrapping = TextWrapping.Wrap,
                                FontWeight = FontWeights.Bold,
                                TextAlignment = TextAlignment.Center,
                            },
                            new TextBlock
                            {
                                Text = price.ToString(),
                                FontSize = 12,
                                TextAlignment = TextAlignment.Center,
                            }
                        }
                    },
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC28DAB")),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAB648C")),
                    Tag = serviceId
                };
                diagnosticButton.Click += ServiceButton_Click;
                stackDiagnServ.Children.Add(diagnosticButton);
            }
        }
        private void LoadSurgicalServices()
        {
            foreach (DataRow row in clientServicesViewTableAdapter.GetSurgicalData())
            {
                string servicesName = row["service_name"].ToString();
                decimal price = (decimal)row["price"];
                int serviceId = (int)row["service_id"];
                PackIconKind packIconKind = PackIconKind.Syringe;
                Button surgicButton = new Button
                {
                    Width = 200,
                    Height = 80,
                    FontFamily = (FontFamily)FindResource("CustomFont"),
                    Margin = new Thickness(10),
                    Content = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Children =
                        {
                            new PackIcon
                            {
                                Kind = packIconKind,
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Center,
                            },
                            new TextBlock
                            {
                                Text = servicesName,
                                FontSize = 13,
                                TextWrapping = TextWrapping.Wrap,
                                FontWeight = FontWeights.Bold,
                                TextAlignment = TextAlignment.Center,
                            },
                            new TextBlock
                            {
                                Text = price.ToString(),
                                FontSize = 12,
                                TextAlignment = TextAlignment.Center,
                            }
                        }
                    },
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC28DAB")),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAB648C")),
                    Tag = serviceId
                };
                surgicButton.Click += ServiceButton_Click;
                stackSurgServ.Children.Add(surgicButton);
            }
        }
        private void SpecializationButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int specializationId = (int)clickedButton.Tag;
            ChooseSpecialist chooseSpecialistPage = new ChooseSpecialist(null, specializationId, _petId);
            NavigationService.Navigate(chooseSpecialistPage);
        }
        private void ServiceButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int serviceId = (int)clickedButton.Tag;
            ChooseSpecialist chooseSpecialistPage = new ChooseSpecialist(serviceId, null, _petId);
            NavigationService.Navigate(chooseSpecialistPage);
        }
    }
}