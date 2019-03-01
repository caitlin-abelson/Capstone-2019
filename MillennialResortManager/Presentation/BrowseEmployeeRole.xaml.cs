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
using DataObjects;
using LogicLayer;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for EmployeeRole.xaml
    /// </summary>
    public partial class BrowseEmployeeRole : Window
    {


        private IRoleManager _roleManager;
        private List<Role> _roles;
        private List<Role> _currentRoles;
        Role _selectedRole = new Role();


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// Default constructor: employee roles.
        /// </summary>
        public BrowseEmployeeRole()
        {
            _roleManager = new RoleManager();

            InitializeComponent();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// constructor: employee roles with one parameter.
        /// </summary>
        public BrowseEmployeeRole(IRoleManager roleManager = null)
        {
            if(roleManager == null)
            {
                _roleManager = new RoleManager();
            }

            _roleManager = roleManager;
            InitializeComponent();
        }
       
        private void TabRole_GotFocus(object sender, RoutedEventArgs e)
        {

            //dgRole.Items.Refresh();
          

        }

        private void DgRole_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {  
           //   var role = (Role)dgRole.SelectedItem;
            //  var detailForm = new UpdateEmployeeRole(role); 
   
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// method to open the create employee roles.
        /// </summary>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            
            var detailForm = new CreateEmployeeRole();
         
            var result = detailForm.ShowDialog();// need to be added



            if (result == true)
            {
               
                MessageBox.Show(result.ToString());
            }
            refreshRoles();
           
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// method to refresh employee roles list.
        /// </summary>
        private void refreshRoles()
        {
            try
            {
                _roles = _roleManager.RetrieveAllRoles();

                _currentRoles = _roles;
                //txtSearch.Text = "";
                dgRole.ItemsSource = _currentRoles;
                filterRoles();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to call the filter method
        /// </summary>

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            filterRoles();
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to filter the  view employee roles
        /// </summary>
        private void filterRoles()
        {
           
            IEnumerable<Role> currentRoles = _roles;
            try
            {
                List<Role> _currentRoles = new List<Role>();
                
                if (txtSearch.Text.ToString() != "")
                {
                   
                    if (txtSearch.Text != "" && txtSearch.Text != null)
                    {
                        currentRoles = currentRoles.Where(b => b.Description.ToLower().Contains(txtSearch.Text.ToLower()));

                        
                    }
                }

                if (cbActive.IsChecked == true && cbDeactive.IsChecked == false)
                {
                    currentRoles = currentRoles.Where(b => b.Active == true);
                }
                else if (cbActive.IsChecked == false && cbDeactive.IsChecked == true)
                {
                    currentRoles = currentRoles.Where(b => b.Active == false);
                }
                else if (cbActive.IsChecked == false && cbDeactive.IsChecked == false)
                {
                    currentRoles = currentRoles.Where(b => b.Active == false && b.Active == true);
                }

                dgRole.ItemsSource =  null;

                dgRole.ItemsSource = currentRoles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

 
            }
       
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to clear the filters
        /// </summary>
        private void BtnClearRoles_Click(object sender, RoutedEventArgs e)
        {
            
            txtSearch.Text = "";
           _currentRoles = _roles;
            cbDeactive.IsChecked = true;
            cbActive.IsChecked = true;
          
            dgRole.ItemsSource = _currentRoles;
        }

       
       


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to update an employee role
        /// </summary>
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

            if (dgRole.SelectedItem != null)
            {
                 _selectedRole = (Role)dgRole.SelectedItem;


                var assign = new CreateEmployeeRole(_selectedRole);
                assign.ShowDialog();
            }
            else
            {

                MessageBox.Show("You must select an item first");

            }
            refreshRoles();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to cancel and exit a window
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to quit?", "Closing Application", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if(result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to Deactivate an employee role
        /// </summary>
        private void BtnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            if (dgRole.SelectedItem != null)
            {
                Role current = (Role)dgRole.SelectedItem;

                try
                {
                    if (current.Active == true)
                    {
                        var result = MessageBox.Show("Are you sure that you want to deactivate this role?", "Deactivate Role", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            _roleManager.DeleteRole(current.RoleID, current.Active);
                        }
                    }
                    else
                    {
                        var result = MessageBox.Show("Are you sure that you want to delete this role?", "Delete Role", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            _roleManager.DeleteRole(current.RoleID, current.Active);
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }


           
            }
            else
            {

                MessageBox.Show("You must select an item first");
              
            }
            refreshRoles();
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/25
        /// 
        /// method window loaded to refresh roles
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshRoles();
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/25
        /// 
        /// method to filter deactive
        /// </summary>
        private void CbDeactive_Click(object sender, RoutedEventArgs e)
        {
            filterRoles();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/25
        /// 
        /// method to filter active
        /// </summary>
        private void CbActive_Click(object sender, RoutedEventArgs e)
        {
            filterRoles();
        }
    }


}







