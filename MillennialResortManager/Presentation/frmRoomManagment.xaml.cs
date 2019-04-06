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
    /// Interaction logic for frmRoomManagment.xaml
    /// </summary>
    public partial class frmRoomManagment : Window
    {
        private RoomManager _roomManager = new RoomManager();
        private List<string> _buidlingIDList;
        private List<string> _roomTypesIDList;
        private List<Room> _roomList;
        private List<Room> _currentRooms;

        public frmRoomManagment()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshRoomData();
            if (_currentRooms == null)
            {
                _currentRooms = _roomList;
            }
            dgRoom.ItemsSource = _currentRooms;
        }

        private void DgRoom_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            viewRoom();
        }

        private void viewRoom()
        {
            var room = (Room)dgRoom.SelectedItem;
            if (room != null)
            {
                var roomForm = new frmAddEditViewRoom(EditMode.View, room.RoomID);
                var results = roomForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must select an item");
            }

        }

        private void refreshRoomData()
        {
            try
            {
                _roomList = _roomManager.RetrieveRoomList();
                _buidlingIDList = _roomManager.RetrieveBuildingList();
                _roomTypesIDList = _roomManager.RetrieveRoomTypeList();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            try
            {
                if (cboRoomBuilding.Items.Count == 0)
                {
                    this.dgRoom.ItemsSource = _roomList;
                    this.cboRoomBuilding.Items.Add("Show All");
                    foreach (var item in _buidlingIDList)
                    {
                        cboRoomBuilding.Items.Add(item);
                    }
                    cboRoomBuilding.SelectedItem = "Show All";
                }
            }
            catch (Exception)
            {

                //MessageBox.Show(ex.Message);
            }

            if (cboRoomType.Items.Count == 0)
            {
                this.cboRoomType.Items.Add("Show All");
                foreach (var item in _roomTypesIDList)
                {
                    cboRoomType.Items.Add(item);
                }
                cboRoomType.SelectedItem = "Show All";
            }
            cbxRoomActive.IsChecked = true;
            cbxRoomInactive.IsChecked = true;
            txtRoomCapacity.Text = "2";
        }

        private void BtnViewRoom_Click(object sender, RoutedEventArgs e)
        {
            viewRoom();
        }

        private void BtnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            var roomForm = new frmAddEditViewRoom();
            var results = roomForm.ShowDialog();
        }

        private void BtnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature not yet enabled");
        }

        ///<remarks>
        /// Danielle Russo
        /// Updated: 2019/04/05
        /// Removed lamba expression used to find all current rooms since Active is no longer a field in the Room table
        /// </remarks>
        private void filterRooms()
        {
            int capacity = 1;
            try
            {
                if (txtRoomCapacity.Text != "")
                {
                    capacity = int.Parse(txtRoomCapacity.Text);
                }
                if (capacity < 1)
                {
                    txtRoomCapacity.Text = "1";
                    capacity = 1;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("You must enter a number for capacity");
            }

            try
            {
                _currentRooms = _roomList.FindAll(r => r.Capacity >= capacity);

                if (cboRoomBuilding.SelectedItem.ToString() != "Show All")
                {
                    _currentRooms = _currentRooms.FindAll(r => r.Building == cboRoomBuilding.SelectedItem.ToString());
                }

                if (cboRoomType.SelectedItem.ToString() != "Show All")
                {
                    _currentRooms = _currentRooms.FindAll(r => r.RoomType == cboRoomType.SelectedItem.ToString());
                }



                this.dgRoom.ItemsSource = _currentRooms;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CboRoomBuilding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterRooms();
        }

        private void CboRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterRooms();
        }

        private void CbxRoomActive_Click(object sender, RoutedEventArgs e)
        {
            filterRooms();
        }

        private void CbxRoomInactive_Click(object sender, RoutedEventArgs e)
        {
            filterRooms();
        }

        private void txtRoomCapacity_TextChanged(object sender, TextChangedEventArgs e)
        {
            filterRooms();
        }
    }
}
