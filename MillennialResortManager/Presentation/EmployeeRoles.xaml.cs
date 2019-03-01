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
    /// /// Austin Berquam
	/// Created: 2019/01/26
	/// 
    /// Interaction logic for EmployeeRoles.xaml
    /// </summary>
    public partial class EmployeeRoles : Window
    {
        public List<EmpRoles> _roles;
        public List<EmpRoles> _currentRoles;
        IEmpRolesManager empRolesManager;

        /// <summary>
        /// Loads the datagrid with the roles table
        /// </summary>
        public EmployeeRoles()
        {

            InitializeComponent();

            empRolesManager = new EmpRolesManager();

            try
            {
                _roles = empRolesManager.RetrieveAllRoles("All");
                if (_currentRoles == null)
                {
                    _currentRoles = _roles;
                }
                dgEmployeeRoles.ItemsSource = _currentRoles;
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
            var addRoles = new AddRoles();
            var result = addRoles.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentRoles = null;
                    _roles = empRolesManager.RetrieveAllRoles("All");
                    if (_currentRoles == null)
                    {
                        _currentRoles = _roles;
                    }
                    dgEmployeeRoles.ItemsSource = _currentRoles;
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
            var deleteRoles = new DeleteRole();
            var result = deleteRoles.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentRoles = null;
                    _roles = empRolesManager.RetrieveAllRoles("All");
                    if (_currentRoles == null)
                    {
                        _currentRoles = _roles;
                    }
                    dgEmployeeRoles.ItemsSource = _currentRoles;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
