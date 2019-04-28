using LogicLayer;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BrowseAppointment : Window
    {
        private IAppointmentManager _appointmentManager;

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// Default constructor:  BrowseAppointment.
        /// </summary>
        public BrowseAppointment()
        {
            if (_appointmentManager == null)
            {
                _appointmentManager = new AppointmentManager();
            }
            InitializeComponent();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// constructor: BrowseAppointment with one parameter.
        /// </summary>
        public BrowseAppointment(IAppointmentManager appointmentManager = null)
        {
            _appointmentManager = appointmentManager;
            if(_appointmentManager == null)
            {
                _appointmentManager = new AppointmentManager();
            }
            InitializeComponent();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// method to open the create walking appointment dialog.
        /// </summary>
        private void BtnAddAppointment_Click(object sender, RoutedEventArgs e)
        {
            var form = new WalkInAppointmentDetail();
            var result = form.ShowDialog();
            refreshDataGrid();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// method to refresh browse appointment datagrid.
        /// </summary>
        private void refreshDataGrid()
        {
            try
            {
                dgAppointments.ItemsSource = _appointmentManager.RetrieveAppointments();
              dgAppointments.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }

        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// 
        /// method window loaded to refresh  appointment lists
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshDataGrid();
        }
    }
}
