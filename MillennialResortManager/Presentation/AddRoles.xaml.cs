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
    /// Interaction logic for AddRoles.xaml
    /// </summary>
    public partial class AddRoles : Window
    {
        IEmpRolesManager _empManager;

        private EmpRoles _empRole;

		private bool result = false;

		/// <summary>
		/// Loads the page
		/// </summary>
		public AddRoles()
        {
            InitializeComponent();

            _empManager = new EmpRolesManager();

        }

        /// <summary>
        /// Sends the empRole to the manager
        /// </summary>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            createNewEmpRole();
			if(result == true)
			{
				try
				{
					var result = _empManager.CreateRole(_empRole);
					if (result == true)
					{
						this.DialogResult = true;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Adding Employee Role Failed.");
				}
			}
				
        }

        /// <summary>
        /// Verifies that the fields are filled out and creates a emp role object
        /// </summary>
        private bool createNewEmpRole()
        {
            if (txtRoleID.Text == "" ||
                txtDescription.Text == "")
            {
                MessageBox.Show("You must fill out all the fields.");
            }
            else if (txtRoleID.Text.Length > 50 || txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your Role Name is too long! Please shorten it.");
            }
            else if (txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your description is too long! Please shorten it.");
            }
            else
            {
				result = true;
				//Valid
				_empRole = new EmpRoles()
				{
					RoleID = txtRoleID.Text,
					Description = txtDescription.Text,
				};
			}
			return result; 
        }
    }
}
