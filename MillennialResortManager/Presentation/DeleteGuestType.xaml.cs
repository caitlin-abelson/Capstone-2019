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
    /// Interaction logic for DeleteGuestType.xaml
    /// </summary>
    public partial class DeleteGuestType : Window
    {
        IGuestTypeManager guestTypeManager;

        private List<GuestType> guestType;
        private List<GuestType> currentguestType;
        private bool result = false;

        /// <summary>
        /// Loads the combo box of guest types to choose from
        /// </summary>
        public DeleteGuestType()
        {
            InitializeComponent();

            guestTypeManager = new GuestTypeManager();
            try
            {
                if (cboGuest.Items.Count == 0)
                {
                    var guestId = guestTypeManager.RetrieveAllGuestTypes();
                    foreach (var item in guestId)
                    {
                        cboGuest.Items.Add(item);
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
            if (cboGuest.SelectedItem == null)
            {
                MessageBox.Show("You must select a type.");
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Event Handler that deletes the selected guest type when clicked
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            delete();
            if (result == true)
            {
                try
                {
                    result = guestTypeManager.DeleteGuestType(cboGuest.SelectedItem.ToString());
                    if (result == true)
                    {
                        this.DialogResult = true;
                        MessageBox.Show("Guest Record Deleted.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot delete a record that is currently assigned to a guest.", " Deleting Guest Type Record Failed.");
                }
            }

        }
    }
}
