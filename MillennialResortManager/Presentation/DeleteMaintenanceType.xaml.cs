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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for DeleteMaintenanceType.xaml
    /// </summary>
    public partial class DeleteMaintenanceType : Window
    {
        IMaintenanceTypeManager maintenanceTypeManager;
        
        private bool result = false;

        /// <summary>
        /// Loads the combo box of maintenance types to choose from
        /// </summary>
        public DeleteMaintenanceType()
        {
            InitializeComponent();

            maintenanceTypeManager = new MaintenanceTypeManager();
            try
            {
                if (cboType.Items.Count == 0)
                {
                    var type = maintenanceTypeManager.RetrieveAllMaintenanceTypes();
                    foreach (var item in type)
                    {
                        cboType.Items.Add(item);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Method that verifies that a selection was made before deleting
        /// </summary>
        private bool delete()
        {
            if (cboType.SelectedItem == null)
            {
                MessageBox.Show("You must select a type.");
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Event Handler that deletes the selected maintenance type when clicked
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            delete();
            if (result == true)
            {
                try
                {
                    result = maintenanceTypeManager.DeleteMaintenanceType(cboType.SelectedItem.ToString());
                    if (result == true)
                    {
                        this.DialogResult = true;
                        MessageBox.Show("Maintenance Record Deleted.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot delete a record that is currently assigned to a Maintenance Ticket.", " Deleting Maintenance Type Record Failed.");
                }
            }

        }
    }
}
