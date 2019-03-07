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
    /// Interaction logic for AddGuestType.xaml
    /// </summary>
    public partial class AddGuestType : Window
    {
        IGuestTypeManager guestManager;

        private GuestType _guestType;

        private bool result = false;

        /// <summary>
        /// Loads the page
        /// </summary>
        public AddGuestType()
        {
            InitializeComponent();

            guestManager = new GuestTypeManager();
        }

        /// <summary>
        /// Sends the guestType to the manager
        /// </summary>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            createNewEmpRole();
            if (result == true)
            {
                try
                {
                    result = guestManager.CreateGuestType(_guestType);
                    if (result == true)
                    {
                        this.DialogResult = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Adding Guest Type Failed.");
                }
            }

        }

        /// <summary>
        /// Verifies that the fields are filled out and creates a guestType object
        /// </summary>
        private bool createNewEmpRole()
        {
            if (txtGuestTypeID.Text == "" ||
                txtDescription.Text == "")
            {
                MessageBox.Show("You must fill out all the fields.");
            }
            else if (txtGuestTypeID.Text.Length > 50 || txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your Guest Type is too long! Please shorten it.");
            }
            else if (txtDescription.Text.Length > 250)
            {
                MessageBox.Show("Your description is too long! Please shorten it.");
            }
            else
            {
                result = true;
                //Valid
                _guestType = new GuestType()
                {
                    GuestTypeID = txtGuestTypeID.Text,
                    Description = txtDescription.Text,
                };
            }
            return result;
        }
    }
}
