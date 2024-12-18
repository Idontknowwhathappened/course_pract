using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для PageServices.xaml
    /// </summary>
    public partial class PageServices : Page
    {
        private readonly ServicesTableAdapter servicesAdapter = new ServicesTableAdapter();
        public PageServices()
        {
            InitializeComponent();
            ServicesDataGrid.ItemsSource = servicesAdapter.GetData();
        }

        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

               string name = ServiceNameTextBox.Text;
               string description = ServiceDescriptionTextBox.Text;
               decimal price = Convert.ToDecimal(ServicePriceTextBox.Text);

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Введите название услуги.");
                    return;
                }

                servicesAdapter.InsertQuery(name, description, price);

                MessageBox.Show("Услуга успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearFields();

                ServicesDataGrid.ItemsSource = servicesAdapter.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditServiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesDataGrid.SelectedItem is DataRowView selectedRow && ValidateFields())
            {
                int serviceId = Convert.ToInt32(selectedRow["service_id"]);
                string name = ServiceNameTextBox.Text;
                string description = ServiceDescriptionTextBox.Text;
                decimal price = Convert.ToDecimal(ServicePriceTextBox.Text);

                servicesAdapter.UpdateQuery(name, description, price, serviceId);
                ServicesDataGrid.ItemsSource = servicesAdapter.GetData();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Выберите услугу и заполните все поля корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteServiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesDataGrid.SelectedItem is DataRowView selectedRow)
            {
                int service_id = Convert.ToInt32(selectedRow["service_id"]);
                string serviceName = selectedRow["name"].ToString();

                // Отображение подтверждающего окна
                var result = MessageBox.Show(
                    $"Вы действительно хотите удалить услугу \"{serviceName}\"?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    servicesAdapter.DeleteQuery(service_id);
                    ServicesDataGrid.ItemsSource = servicesAdapter.GetData();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите услугу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void ServicesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServicesDataGrid.SelectedItem is DataRowView selectedRow)
            {
                ServiceNameTextBox.Text = selectedRow["name"].ToString();
                ServiceDescriptionTextBox.Text = selectedRow["description"].ToString();
                ServicePriceTextBox.Text = selectedRow["price"].ToString();
            }
        }

        private bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace(ServiceNameTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(ServicePriceTextBox.Text) &&
                   decimal.TryParse(ServicePriceTextBox.Text, out _);
        }
        private void ClearFields()
        {
            ServiceNameTextBox.Clear();
            ServiceDescriptionTextBox.Clear();
            ServicePriceTextBox.Clear();
        }
    }
}
