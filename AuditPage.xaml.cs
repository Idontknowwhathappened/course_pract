using System.Windows.Controls;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для AuditPage.xaml
    /// </summary>
    public partial class AuditPage : Page
    {
        private readonly Audit_LogTableAdapter audit_LogTableAdapter = new Audit_LogTableAdapter();
        public AuditPage()
        {
            InitializeComponent();
            AuditDataGrid.ItemsSource = audit_LogTableAdapter.GetData();
        }
    }
}
