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
using LogicLayer;
using DataObjects;


namespace Presentation
{
    /// <summary>
    /// Interaction logic for CreateEmployeeRole.xaml
    /// </summary>
    public partial class CreateEmployeeRole : Window
    {


        private IRoleManager _roleManager;
        private Role _selectedRole;

        bool _isUpdate = false;

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// constructor  to create employee roles.
        /// </summary>
        public CreateEmployeeRole(IRoleManager roleManager = null)
        {
            if (roleManager == null)
            {
                roleManager = new RoleManager();
            }

            _roleManager = roleManager;



            InitializeComponent();
        }

        public CreateEmployeeRole(Role selectedRole, IRoleManager roleManager = null)
        {
            _selectedRole = selectedRole;

            if (roleManager == null)
            {
                roleManager = new RoleManager();
            }

            _roleManager = roleManager;

            InitializeComponent();

            txtRoleID.Text = _selectedRole.RoleID;
            txtRoleID.IsEnabled = false;

            txtDescription.Text = _selectedRole.Description;

            lblNewRole.Content = "Update Employee Role";

            this.Title = "Update Employee Role";

            _isUpdate = true;
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// method to save employee roles.
        /// </summary>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // make sure they fill out all of the stuff correctly
            if (txtRoleID.Text == "")
            {
                MessageBox.Show("Role Name can't be Blank");
                txtRoleID.Focus();
                txtRoleID.SelectAll();
                return;
            }
            else if (txtDescription.Text == "")
            {
                MessageBox.Show("Description can't be Blank");
                txtDescription.Focus();
                txtDescription.SelectAll();
                return;
            }

            Role newRole = new Role()
            {
                RoleID = txtRoleID.Text,
                Description = txtDescription.Text

            };

            if (_isUpdate)
            {
                try
                {
                    _roleManager.UpdateRole(_selectedRole, newRole);
                    this.DialogResult = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {

                try
                {


                    if (_roleManager.CreateRole(newRole) == 0)
                    {
                        MessageBox.Show("Role Saved");
                        this.DialogResult = true;
                    }



                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            this.Close();
        }



        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/05
        /// 
        /// method to close a window.
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

            var result = MessageBox.Show("Are you sure you want to quit?", "Closing Application", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }

        }
    }

}


