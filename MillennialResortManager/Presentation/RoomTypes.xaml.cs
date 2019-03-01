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
    /// Interaction logic for RoomTypes.xaml
    /// </summary>
    public partial class RoomTypes : Window
    {
        public List<RoomType> _room;
        public List<RoomType> _currentRoom;
        IRoomType roomManager;

        /// <summary>
        /// Loads the datagrid with the guest type table
        /// </summary>
        public RoomTypes()
        {
            InitializeComponent();

            roomManager = new RoomTypeManager();
            try
            {
                _room = roomManager.RetrieveAllRoomTypes("All");
                if (_currentRoom == null)
                {
                    _currentRoom = _room;
                }
                dgRooms.ItemsSource = _currentRoom;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Opens up the add window and updates the datagrid if guest type was created successfully
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addGuest = new AddRoom();
            var result = addGuest.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentRoom = null;
                    _room = roomManager.RetrieveAllRoomTypes("All");
                    if (_currentRoom == null)
                    {
                        _currentRoom = _room;
                    }
                    dgRooms.ItemsSource = _currentRoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Opens up the delete window and updates the datagrid if guest type was deleted successfully
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteGuestType = new DeleteRoom();
            var result = deleteGuestType.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentRoom = null;
                    _room = roomManager.RetrieveAllRoomTypes("All");
                    if (_currentRoom == null)
                    {
                        _currentRoom = _room;
                    }
                    dgRooms.ItemsSource = _currentRoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
