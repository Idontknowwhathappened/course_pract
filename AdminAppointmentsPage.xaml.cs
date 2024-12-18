using System.Windows.Controls;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    /// <summary>
    /// Логика взаимодействия для AdminAppointmentsPage.xaml
    /// </summary>
    public partial class AdminAppointmentsPage : Page
    {
        private readonly AppointmentsViewTableAdapter appointmentsViewTable = new AppointmentsViewTableAdapter();
        public AdminAppointmentsPage()
        {
            InitializeComponent();
            AppointmentsDataGrid.ItemsSource = appointmentsViewTable.GetData();
        }
    }
}
