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
using LogicLayer;


namespace Presentation
{
    /// <summary>
    /// Interaction logic for BrowseEmployee.xaml
    /// </summary>
    public partial class BrowseEmployee : Window
    {
        EmployeeManager _employeeManager;
        List<Employee> _employees;
        List<Employee> _currentEmployees;

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// The constructor for the browse window.
        /// </summary>
        public BrowseEmployee()
        {
            InitializeComponent();
            _employeeManager = new EmployeeManager();
            refreshAllEmployees();
            populateEmployees();
        }


        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/02/04
        /// 
        /// A method to create filters for the browse window.
        /// </summary>
        public void ApplyFilters()
        {
            try
            {

                // Get a fresh grid.
                repopulateEmployees();

                if (txtSearchFirstName.Text.ToString() != "")
                {
                    _currentEmployees = _currentEmployees.FindAll(s => s.FirstName.ToLower().Contains(txtSearchFirstName.Text.ToString().ToLower()));
                }

                if (txtSearchLastName.Text.ToString() != "")
                {
                    _currentEmployees = _currentEmployees.FindAll(s => s.LastName.ToLower().Contains(txtSearchLastName.Text.ToString().ToLower()));
                }

                if (txtSearchDepartment.Text.ToString() != "")
                {
                    _currentEmployees = _currentEmployees.FindAll(s => s.DepartmentID.ToLower().Contains(txtSearchDepartment.Text.ToString().ToLower()));
                }

                if (rbtnActiveEmployee.IsChecked == true)
                {
                    _currentEmployees = _employeeManager.SelectAllActiveEmployees();
                }

                if (rbtnInactiveEmployee.IsChecked == true)
                {
                    _currentEmployees = _employeeManager.SelectAllInActiveEmployees();
                }

                dgEmployees.ItemsSource = _currentEmployees;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/02/04
        /// 
        /// A method to creat the clear functions for the clear button
        /// </summary>
        public void ClearFilters()
        {
            txtSearchFirstName.Clear();
            txtSearchLastName.Clear();
            txtSearchRole.Clear();
            txtSearchDepartment.Clear();

            repopulateEmployees();

            rbtnActiveEmployee.IsChecked = true;
        }

        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/02/04
        /// 
        /// Used to populate the DataGrid.
        /// </summary>
        private void populateEmployees()
        {
            try
            {
                _employees = _employeeManager.SelectAllEmployees();
                if (_currentEmployees == null)
                {
                    _currentEmployees = _employees;
                }
                dgEmployees.ItemsSource = _currentEmployees;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/02/04
        /// 
        /// Used to repopulate the current employees in the datagrid.
        /// </summary>
        private void repopulateEmployees()
        {
            _currentEmployees = _employees;
            dgEmployees.ItemsSource = _currentEmployees;
        }

        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/02/04
        /// 
        /// Used to apply the filters to the browse window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilters();


        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/6/2019
        /// 
        /// Retrieves all of the employees in order for the data grid to be refreshed.
        /// </summary>
        private void refreshAllEmployees()
        {
            try
            {
                _employees = _employeeManager.SelectAllEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            _currentEmployees = _employees;
        }


        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/02/04
        /// 
        /// Used to clear the filters in the Browse window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            ClearFilters();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// This button cancels the window and closes it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelBrowseEmployee_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel?", "Leaving Employee browse screen.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// This opens with window to add a new employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var createEmployee = new EmployeeDetail();
            createEmployee.ShowDialog();
            refreshAllEmployees();
            populateEmployees();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// This button opens the window to read the information for the chosen employee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee chosenEmployee = new Employee();

            chosenEmployee = (Employee)dgEmployees.SelectedItem;
            try
            {
                var readUpdateEmployee = new EmployeeDetail(chosenEmployee);
                readUpdateEmployee.ShowDialog();

                refreshAllEmployees();
                populateEmployees();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to find Employee." + ex.Message);
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/13/19
        /// 
        /// The delete button first deactivates and then deletes an employee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                _employeeManager.DeleteEmployee(((Employee)dgEmployees.SelectedItem).EmployeeID, ((Employee)dgEmployees.SelectedItem).Active);
                if(((Employee)dgEmployees.SelectedItem).Active)
                {
                    var result = MessageBox.Show("Are you sure you want to deactivate this employee?", "This employee will no longer be active in the system.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("The employee has been deactivated.");
                    }
                }
                else
                {
                    var result = MessageBox.Show("Are you sure you want to delete this employee?", "This employee will no longer be in the system.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("The employee has been purged.");
                    }
                }
                refreshAllEmployees();
                populateEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't delete this employee" + ex.Message);
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// Opens the window to read the information for the chosen employee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgEmployees_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Employee chosenEmployee = new Employee();

            chosenEmployee = (Employee)dgEmployees.SelectedItem;
            try
            {
                var readUpdateEmployee = new EmployeeDetail(chosenEmployee);
                readUpdateEmployee.ShowDialog();

                refreshAllEmployees();
                populateEmployees();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to find Employee." + ex.Message);
            }
        }

    }
}
