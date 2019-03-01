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
    /// Interaction logic for DeleteRoom.xaml
    /// </summary>
    public partial class DeleteRoom : Window
    {
        IRoomType roomManager;

        private List<RoomType> RoomType;
        private List<RoomType> currentRoomType;
        private bool result = false;

        /// <summary>
        /// Loads the combo box of room types to choose from
        /// </summary>
        public DeleteRoom()
        {
            InitializeComponent();

            roomManager = new RoomTypeManager();
            try
            {
                if (cboRoom.Items.Count == 0)
                {
                    var guestId = roomManager.RetrieveAllRoomTypes();
                    foreach (var item in guestId)
                    {
                        cboRoom.Items.Add(item);
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
            if (cboRoom.SelectedItem == null)
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
        /// Event Handler that deletes the selected room type when clicked
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            delete();
            if (result == true)
            {
                try
                {
                    var result = roomManager.DeleteRoomType(cboRoom.SelectedItem.ToString());
                    if (result == true)
                    {
                        this.DialogResult = true;
                        MessageBox.Show("Room Record Deleted.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot delete a record that is currently assigned to a room.", " Deleting Room Type Record Failed.");
                }
            }

        }
    }
}
