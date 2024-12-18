using System.Windows.Controls;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для AccountsPage.xaml
    /// </summary>
    public partial class AccountsPage : Page
    {
        private readonly View_Accounts_PaymentsTableAdapter view_Accounts_PaymentsTableAdapter = new View_Accounts_PaymentsTableAdapter();
        public AccountsPage()
        {
            InitializeComponent();
            AccountsDataGrid.ItemsSource = view_Accounts_PaymentsTableAdapter.GetData();
        }
    }
}
