using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private readonly Login_DetailsTableAdapter login_DetailsTableAdapter = new Login_DetailsTableAdapter();
        private readonly DoctorsTableAdapter doctorsTableAdapter = new DoctorsTableAdapter();
        public UsersPage()
        {
            InitializeComponent();
            LoadDoctors();
            LoadFilters();
        }
        private void LoadDoctors(int? roleId = null, string username = null, string firstName = null, string lastName = null, string patronymic = null)
        {
            try
            {
                var users = login_DetailsTableAdapter.GetUsers(roleId, username, firstName, lastName, patronymic);
                UsersDataGrid.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadFilters()
        {
            var roleTableAdapter = new RoleTableAdapter();
            var specializationTableAdapter = new SpecializationsTableAdapter();

            DoctorSpecializationComboBox.ItemsSource = specializationTableAdapter.GetData();
            DoctorSpecializationComboBox.DisplayMemberPath = "name";
            DoctorSpecializationComboBox.SelectedValuePath = "specialization_id";

            RoleFilterComboBox.ItemsSource = roleTableAdapter.GetData();
            RoleComboBox.ItemsSource = roleTableAdapter.GetData();
            RoleFilterComboBox.DisplayMemberPath = "name";
            RoleFilterComboBox.SelectedValuePath = "role_id";
            RoleComboBox.DisplayMemberPath = "name";
            RoleComboBox.SelectedValuePath = "role_id";
        }
        private void ClearForm()
        {
            FirstNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            DoctorSpecializationComboBox.SelectedIndex = -1;
            DoctorExperienceTextBox.Text = string.Empty;
            PatronymicTextBox.Text = string.Empty;
            RoleComboBox.SelectedIndex = -1;
            EmailTextBox.Text = string.Empty;
            NumberTextBox.Text = string.Empty;
            LoginTextBox.Text = string.Empty;
            PasswordTextBox.Text = string.Empty;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string username = string.IsNullOrWhiteSpace(UsernameTextBox.Text) ? null : $"%{UsernameTextBox.Text}%";
            string first_name = string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ? null : $"%{FirstNameTextBox.Text}%";
            string last_name = string.IsNullOrWhiteSpace(LastNameTextBox.Text) ? null : $"%{LastNameTextBox.Text}%";
            string patronymic = string.IsNullOrWhiteSpace(PatronymicTextBox.Text) ? null : $"%{PatronymicTextBox.Text}%";

            int? role_id = RoleFilterComboBox.SelectedValue != null ? (int?)Convert.ToInt32(RoleFilterComboBox.SelectedValue) : null;

            LoadDoctors(role_id, username, first_name, last_name, patronymic);

        }
        private int? FindRoleIdByName(string roleName)
        {
            var roleTable = new RoleTableAdapter().GetData();
            var roleRow = roleTable.FirstOrDefault(row => row["name"].ToString() == roleName);
            return roleRow?["role_id"] as int?;
        }
        private int? FindSpecializationIdByName(string specializationName)
        {
            var specializationTable = new SpecializationsTableAdapter().GetData();
            var specializationRow = specializationTable.FirstOrDefault(row => row["name"].ToString() == specializationName);
            return specializationRow?["specialization_id"] as int?;
        }

        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is DataRowView selectedRow)
            {
                if (selectedRow["Role"].ToString() == "Врач")
                {
                    string fullName = selectedRow["DoctorFullName"].ToString().Trim();
                    string[] nameParts = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string lastName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
                    string name = nameParts.Length > 1 ? nameParts[1] : string.Empty;
                    string patronymic = nameParts.Length > 2 ? nameParts[2] : null;
                    DoctorFirstNameTextBox.Text = name;
                    DoctorLastNameTextBox.Text = lastName;
                    DoctorPatronymicTextBox.Text = patronymic;
                    DoctorExperienceTextBox.Text = selectedRow["WorkExperience"].ToString();
                    LoginTextBox.Text = selectedRow["Username"].ToString();
                    EmailTextBox.Text = selectedRow["Email"].ToString();
                    NumberTextBox.Text = selectedRow["NumberPhone"].ToString();
                    string specializationName = selectedRow["Specialization"].ToString();
                    DoctorSpecializationComboBox.SelectedValue = FindSpecializationIdByName(specializationName);
                }
                string roleName = selectedRow["Role"].ToString();
                RoleComboBox.SelectedValue = FindRoleIdByName(roleName);
            }
        }

        private void AddDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string lastName = DoctorLastNameTextBox.Text;
                string name = DoctorFirstNameTextBox.Text;
                string patronymic = DoctorPatronymicTextBox.Text;
                string email = EmailTextBox.Text;
                string number_phone = NumberTextBox.Text;
                string login = LoginTextBox.Text;
                string password = PasswordTextBox.Text;

                int experience = Convert.ToInt32(DoctorExperienceTextBox.Text);
                int? specializationId = DoctorSpecializationComboBox.SelectedValue != null
                                        ? (int?)Convert.ToInt32(DoctorSpecializationComboBox.SelectedValue)
                                        : null;
                int? roleId = RoleComboBox.SelectedValue != null
                                        ? (int?)Convert.ToInt32(RoleComboBox.SelectedValue)
                                        : null;


                login_DetailsTableAdapter.RegisterUser(login, email, password, roleId, null, null, null, number_phone, name, lastName, patronymic, specializationId, experience);

                MessageBox.Show("Врач успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearForm();

                LoadDoctors();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is DataRowView selectedRow)
            {
                int loginDetailsID = Convert.ToInt32(selectedRow["UserID"]);
                int doctorID = selectedRow["DoctorID"] == DBNull.Value ? 0 : Convert.ToInt32(selectedRow["DoctorID"]);

                // Проверяем, изменяем ли роль или данные врача
                if (RoleComboBox.SelectedValue != null && RoleComboBox.SelectedValue.ToString() != selectedRow["RoleID"].ToString())
                {
                    int newRoleID = Convert.ToInt32(RoleComboBox.SelectedValue);

                    // Подтверждение изменения роли
                    var roleChangeResult = MessageBox.Show(
                        "Вы уверены, что хотите изменить роль пользователя?",
                        "Подтверждение изменения",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question
                    );

                    if (roleChangeResult == MessageBoxResult.Yes)
                    {
                        login_DetailsTableAdapter.UpdateUserRole(loginDetailsID, newRoleID);
                        MessageBox.Show("Роль пользователя успешно обновлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else if (doctorID > 0) // Если ID врача указан, обновляем данные врача
                {

                    string doctorName = string.IsNullOrWhiteSpace(DoctorFirstNameTextBox.Text) ? null : DoctorFirstNameTextBox.Text;
                    string doctorLastName = string.IsNullOrWhiteSpace(DoctorLastNameTextBox.Text) ? null : DoctorLastNameTextBox.Text;
                    string doctorPatronymic = string.IsNullOrWhiteSpace(DoctorPatronymicTextBox.Text) ? null : DoctorPatronymicTextBox.Text;
                    int specializationID = DoctorSpecializationComboBox.SelectedValue == null
                        ? 0
                        : Convert.ToInt32(DoctorSpecializationComboBox.SelectedValue);
                    int workExperience = string.IsNullOrWhiteSpace(DoctorExperienceTextBox.Text)
                        ? 0
                        : Convert.ToInt32(DoctorExperienceTextBox.Text);
                    string doctorFullName = $"{doctorLastName} {doctorName} {doctorPatronymic}";

                    bool dataChanged = !(
                        doctorFullName == selectedRow["DoctorFullName"].ToString() &&
                        specializationID == Convert.ToInt32(selectedRow["SpecializationID"]) &&
                        workExperience == Convert.ToInt32(selectedRow["WorkExperience"]));

                    if (dataChanged)
                    {
                        // Подтверждение изменения данных врача
                        var doctorChangeResult = MessageBox.Show(
                            "Вы уверены, что хотите изменить данные врача?",
                            "Подтверждение изменения",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question
                        );

                        if (doctorChangeResult == MessageBoxResult.Yes)
                        {
                            doctorsTableAdapter.UpdateDoctor(
                                doctorID,
                                doctorName,
                                doctorLastName,
                                doctorPatronymic,
                                specializationID == 0 ? (int?)null : specializationID,
                                workExperience == 0 ? (int?)null : workExperience
                            );
                            MessageBox.Show("Данные врача успешно обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите действие для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                LoadDoctors();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}