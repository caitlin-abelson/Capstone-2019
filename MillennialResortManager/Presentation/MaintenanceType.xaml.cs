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
using System.Windows.Shapes;
using DataObjects;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MaintenanceType.xaml
    /// </summary>
    public partial class MaintenanceType : Window
    {
        public List<MaintenanceTypes> type;
        public List<MaintenanceTypes> currentType;
        IMaintenanceTypeManager maintenanceManager;

        /// <summary>
        /// Loads the datagrid with the MaintenanceType table
        /// </summary>
        public MaintenanceType()
        {
            InitializeComponent();

            maintenanceManager = new MaintenanceTypeManager();
            try
            {
                type = maintenanceManager.RetrieveMaintenanceTypes("status");
                if (currentType == null)
                {
                    currentType = type;
                }
                dgMaintenanceTypes.ItemsSource = currentType;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string MaintenanceTypeID { get; internal set; }

        /// <summary>
        /// Opens up the add window and updates the datagrid if MaintenanceType was created successfully
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addType = new AddMaintenanceType();
            var result = addType.ShowDialog();
            if (result == true)
            {
                try
                {
                    currentType = null;
                    type = maintenanceManager.RetrieveMaintenanceTypes("All");
                    if (currentType == null)
                    {
                        currentType = type;
                    }
                    dgMaintenanceTypes.ItemsSource = currentType;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Opens up the delete window and updates the datagrid if MaintenanceType was deleted successfully
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteMaintenanceType = new DeleteMaintenanceType();
            var result = deleteMaintenanceType.ShowDialog();
            if (result == true)
            {
                try
                {
                    currentType = null;
                    type = maintenanceManager.RetrieveMaintenanceTypes("All");
                    if (currentType == null)
                    {
                        currentType = type;
                    }
                    dgMaintenanceTypes.ItemsSource = currentType;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
