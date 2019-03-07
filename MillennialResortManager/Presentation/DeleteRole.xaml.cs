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
    /// Interaction logic for DeleteRole.xaml
    /// </summary>
    public partial class DeleteRole : Window
    {
        IEmpRolesManager empRolesManager;
        
		private bool result = false;

        /// <summary>
        /// Loads the combo box of roles to choose from
        /// </summary>
        public DeleteRole()
        {
            InitializeComponent();

            empRolesManager = new EmpRolesManager();

            try
            {
                if (cboRole.Items.Count == 0)
                {
                    var roleID = empRolesManager.RetrieveAllRoles();
                    foreach (var item in roleID)
                    {
                        cboRole.Items.Add(item);
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
            if (cboRole.SelectedItem == null)
            {
                MessageBox.Show("You must select a Role.");
            }
			else
			{
				result = true;
			}
			return result;
        }

        /// <summary>
        /// Event Handler that deletes the selected role when clicked
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            delete();
			if(result == true)
			{
				try
				{
					result = empRolesManager.DeleteRole(cboRole.SelectedItem.ToString());
					if (result == true)
					{
						this.DialogResult = true;
						MessageBox.Show("Role Record Deleted.");
					}
				}
				catch (Exception)
				{
					MessageBox.Show("Cannot delete a roll that is currently assigned to an employee.", " Deleting Role Record Failed.");
				}
			}
            
        }
    }
}
