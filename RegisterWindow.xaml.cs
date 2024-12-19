using System;
using System.Windows;
using System.Windows.Controls;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string ownerName = OwnerNameTextBox.Text;
            string ownerLastName = OwnerLastNameTextBox.Text;
            string ownerPatronymic = OwnerPatronymicTextBox.Text;
            string contactDetails = ContactDetailsTextBox.Text;

            // Проверка на заполненность полей
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(ownerName) ||
                string.IsNullOrWhiteSpace(ownerLastName) || string.IsNullOrWhiteSpace(contactDetails))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            try
            {
                var adapter = new Login_DetailsTableAdapter();
                adapter.RegisterUser(username, email, password, 1, ownerName, ownerLastName, ownerPatronymic, contactDetails, null, null, null, null, null);

                MessageBox.Show("Регистрация успешна!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка регистрации: " + ex.Message);
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
