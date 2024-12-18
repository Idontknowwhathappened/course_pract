using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Login_DetailsTableAdapter login_DetailsTable = new Login_DetailsTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int? doctorID = 0;
                if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Password))
                {
                    MessageBox.Show("Логин и пароль не могут быть пустыми.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(PasswordTextBox.Password))
                {
                    MessageBox.Show("Введите пароль.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
                {
                    MessageBox.Show("Введите логин.");
                    return;
                }

                // Выполняем хранимую процедуру и получаем результат в виде DataTable
                var resultTable = login_DetailsTable.AuthenticateUser(UsernameTextBox.Text, PasswordTextBox.Password);

                if (resultTable == null || resultTable.Rows.Count == 0)
                {
                    MessageBox.Show("Ошибка при выполнении аутентификации.");
                    return;
                }

                // Получаем первую строку из таблицы
                var resultRow = resultTable.Rows[0];
                int roleId = Convert.ToInt32(resultRow["RoleId"]);
                if (resultRow["DoctorID"] != DBNull.Value)
                {
                    doctorID = (int)resultRow["DoctorID"];
                }

                if (roleId == -1)
                {
                    MessageBox.Show("Пользователь не найден.");
                }
                else if (roleId == 0)
                {
                    MessageBox.Show("Неверный пароль.");
                }
                else if (roleId == 4) // Врач
                {
                    if (doctorID.HasValue)
                    {
                        MainWindowVetDoctor vetWindow = new MainWindowVetDoctor(doctorID.Value);
                        vetWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Доктор не найден.");
                    }
                }
                else if (roleId == 3) // Менеджер
                {
                    ManagerWindow managerWindow = new ManagerWindow();
                    managerWindow.Show();
                    Close();
                }
                else if (roleId == 2) // Администратор
                {
                    AdministratorWindow administratorWindow = new AdministratorWindow();
                    administratorWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неподдерживаемая роль.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void ForgotPassword_Click(object sender, MouseButtonEventArgs e)
        {

        }

    }
}