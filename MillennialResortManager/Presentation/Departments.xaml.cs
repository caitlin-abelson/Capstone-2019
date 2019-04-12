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
    /// Interaction logic for Departments.xaml
    /// </summary>
    public partial class Departments : Window
    {
        public List<Department> _departmentsList;
        public List<Department> _currentDepartments;
        IDepartmentTypeManager departmentManager;

        /// <summary>
        /// Loads the datagrid with the department table
        /// </summary>
        public Departments()
        {
            InitializeComponent();

            departmentManager = new DepartmentTypeManager();
            try
            {
                _departmentsList = departmentManager.RetrieveAllDepartments("All");
                if (_currentDepartments == null)
                {
                    _currentDepartments = _departmentsList;
                }
                dgDepartment.ItemsSource = _currentDepartments;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Opens up the add window and updates the datagrid if role was created successfully
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addRoles = new AddDepartment();
            var result = addRoles.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentDepartments = null;
                    _departmentsList = departmentManager.RetrieveAllDepartments("All");
                    if (_currentDepartments == null)
                    {
                        _currentDepartments = _departmentsList;
                    }
                    dgDepartment.ItemsSource = _currentDepartments;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Opens up the delete window and updates the datagrid if role was deleted successfully
        /// NOTE : If you the role is assigned to an Employee the role cannot be deleted
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteRoles = new DeleteDepartment();
            var result = deleteRoles.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentDepartments = null;
                    _departmentsList = departmentManager.RetrieveAllDepartments("All");
                    if (_currentDepartments == null)
                    {
                        _currentDepartments = _departmentsList;
                    }
                    dgDepartment.ItemsSource = _currentDepartments;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
