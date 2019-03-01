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
    /// Interaction logic for DeleteDepartment.xaml
    /// </summary>
    public partial class DeleteDepartment : Window
    {
        IDepartmentTypeManager departmentManager;

        private List<Department> department;
        private List<Department> currentdepartment;
        private bool result = false;

        /// <summary>
        /// Loads the combo box of departments to choose from
        /// </summary>
        public DeleteDepartment()
        {
            InitializeComponent();

            departmentManager = new DepartmentTypeManager();
            try
            {
                if (cboDepartment.Items.Count == 0)
                {
                    var departmentId = departmentManager.RetrieveAllDepartmentTypes();
                    foreach (var item in departmentId)
                    {
                        cboDepartment.Items.Add(item);
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
            if (cboDepartment.SelectedItem == null)
            {
                MessageBox.Show("You must select a Department.");
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Event Handler that deletes the selected department when clicked
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            delete();
            if (result == true)
            {
                try
                {
                    var result = departmentManager.DeleteDepartment(cboDepartment.SelectedItem.ToString());
                    if (result == true)
                    {
                        this.DialogResult = true;
                        MessageBox.Show("Department Record Deleted.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot delete a department that is currently assigned.", " Deleting Deapartment Record Failed.");
                }
            }

        }
    }
}
