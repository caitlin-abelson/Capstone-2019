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
    /// Interaction logic for AddRoom.xaml
    /// </summary>
    public partial class AddRoom : Window
    {
        IRoomType roomManager;

        private RoomType _RoomType;

        private bool result = false;

        /// <summary>
        /// Loads the page
        /// </summary>
        public AddRoom()
        {
            InitializeComponent();

            roomManager = new RoomTypeManager();
        }

        /// <summary>
        /// Sends the RoomType to the manager
        /// </summary>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            createNewEmpRole();
            if (result == true)
            {
                try
                {
                    var result = roomManager.CreateRoomType(_RoomType);
                    if (result == true)
                    {
                        this.DialogResult = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Adding Room Type Failed.");
                }
            }

        }

        /// <summary>
        /// Verifies that the fields are filled out and creates a RoomType object
        /// </summary>
        private bool createNewEmpRole()
        {
            if (txtRoomTypeID.Text == "" ||
                txtDescription.Text == "")
            {
                MessageBox.Show("You must fill out all the fields.");
            }
            else if (txtRoomTypeID.Text.Length > 50 || txtDescription.Text.Length > 250)
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
                _RoomType = new RoomType()
                {
                    RoomTypeID = txtRoomTypeID.Text,
                    Description = txtDescription.Text,
                };
            }
            return result;
        }
    }
}
