using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        public AdministratorWindow()
        {
            InitializeComponent();
            var pageAppointment = new AdminAppointmentsPage();
            MainFrame.Navigate(pageAppointment);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainFrame == null) return;

            if (e.Source is TabControl tabControl)
            {
                switch (tabControl.SelectedIndex)
                {
                    case 0:
                        var pageAppointment = new AdminAppointmentsPage();
                        MainFrame.Navigate(pageAppointment);
                        break;

                    case 1:
                        var doctorsShedule = new DoctorsShedulePage();
                        MainFrame.Navigate(doctorsShedule);
                        break;

                    case 2:
                        var servicesPage = new PageServices();
                        MainFrame.Navigate(servicesPage);
                        break;
                    case 3:
                        var usersPage = new UsersPage();
                        MainFrame.Navigate(usersPage);
                        break;
                    case 4:
                        var statisticsPage = new StatisticsPage();
                        MainFrame.Navigate(statisticsPage);
                        break;
                    case 5:
                        var accountsPage = new AccountsPage();
                        MainFrame.Navigate(accountsPage);
                        break;
                    case 6:
                        var auditPage = new AuditPage();
                        MainFrame.Navigate(auditPage);
                        break;
                    case 7:
                        MessageBoxResult result = MessageBox.Show(
                            "Вы уверены, что хотите выйти из аккаунта?",
                            "Подтверждение выхода",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            Close();
                        }
                        break;
                }
            }
        }
    }
}
