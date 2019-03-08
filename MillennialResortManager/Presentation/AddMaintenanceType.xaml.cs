using DataObjects;
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
    /// Interaction logic for AddMaintenanceType.xaml
    /// </summary>
    public partial class AddMaintenanceType : Window
    {
        IMaintenanceTypeManager typeManager;

        private MaintenanceTypes _maintenanceType;

        private bool result = false;

        /// <summary>
        /// Loads the page
        /// </summary>
        public AddMaintenanceType()
        {
            InitializeComponent();

            typeManager = new MaintenanceTypeManager();
        }

        /// <summary>
        /// Sends the MaintenanceType to the manager
        /// </summary>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            createNewMaintenanceType();
            if (result == true)
            {
                try
                {
                    result = typeManager.CreateMaintenanceType(_maintenanceType);
                    if (result == true)
                    {
                        this.DialogResult = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Adding Maintenance Type Failed.");
                }
            }

        }

        /// <summary>
        /// Verifies that the fields are filled out and creates a MaintenanceType object
        /// </summary>
        private bool createNewMaintenanceType()
        {
            if (txtMaintenanceTypeID.Text == "" ||
                txtDescription.Text == "")
            {
                MessageBox.Show("You must fill out all the fields.");
            }
            else if (txtMaintenanceTypeID.Text.Length > 50 || txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your Maintenance Type is too long! Please shorten it.");
            }
            else if (txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your description is too long! Please shorten it.");
            }
            else
            {
                result = true;
                //Valid
                _maintenanceType = new MaintenanceTypes()
                {
                    MaintenanceTypeID = txtMaintenanceTypeID.Text,
                    Description = txtDescription.Text,
                };
            }
            return result;
        }
    }
}
