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
    /// Interaction logic for AddDepartment.xaml
    /// </summary>
    public partial class AddDepartment : Window
    {
        IDepartmentTypeManager departmentManager;

        private Department _department;

        private bool result = false;

        /// <summary>
        /// Loads the page
        /// </summary>
        public AddDepartment()
        {
            InitializeComponent();

            departmentManager = new DepartmentTypeManager();
        }

        /// <summary>
        /// Sends the department to the manager
        /// </summary>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            createNewDepartment();
            if (result == true)
            {
                try
                {
                    result = departmentManager.CreateDepartment(_department);
                    if (result == true)
                    {
                        this.DialogResult = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Adding Department Failed.");
                }
            }

        }

        /// <summary>
        /// Verifies that the fields are filled out and creates a department object
        /// </summary>
        private bool createNewDepartment()
        {
            if (txtDepartment.Text == "" ||
                txtDescription.Text == "")
            {
                MessageBox.Show("You must fill out all the fields.");
            }
            else if (txtDepartment.Text.Length > 50 || txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your Department Name is too long! Please shorten it.");
            }
            else if (txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your description is too long! Please shorten it.");
            }
            else
            {
                result = true;
                //Valid
                _department = new Department()
                {
                    DepartmentID = txtDepartment.Text,
                    Description = txtDescription.Text,
                };
            }
            return result;
        }
    }
}
