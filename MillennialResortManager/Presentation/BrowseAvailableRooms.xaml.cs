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
using DataObjects;
using LogicLayer;

namespace Presentation
{
    /// <summary>
    /// James Heim and Jared Greenfield.
    /// Created 2019-04-12
    /// 
    /// Interaction logic for BrowseAvailableRooms.xaml
    /// 
    /// Jared helped Debug a lot of issues. -[
    /// </summary>
    public partial class BrowseAvailableRooms : Window
    {

        List<VMRoomRoomReservation> _vmRooms = new List<VMRoomRoomReservation>();


        RoomManager _roomManager = new RoomManager();
        RoomReservationManager _roomReservationManager = new RoomReservationManager();
        

        private int _guestId;

        public VMRoomRoomReservation SelectedVM { get; set; }

        /// <summary>
        /// James Heim
        /// Created 2019-04-12
        /// 
        /// Take the supplied Reservation IDs and construct View Models for them.
        /// </summary>
        /// <param name="roomReservationIDs"></param>
        public BrowseAvailableRooms(int guestId, List<VMRoomRoomReservation> vmRooms)
        {
            InitializeComponent();

            _guestId = guestId;

            _vmRooms = vmRooms;

            // Throw the ViewModels into the DataGrid.
            dgAvailableRooms.ItemsSource = this._vmRooms;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-12
        /// 
        /// Return to the previous window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-12
        /// 
        /// View who is assigned to the room in the selected View Model.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            var selectedVM = (VMRoomRoomReservation) dgAvailableRooms.SelectedItem;
            if (selectedVM != null)
            {
                Room selectedRoom = _roomManager.RetreieveRoomByID(selectedVM.RoomID);
                var form = new frmAddEditViewRoom(EditMode.View, selectedVM.RoomID);
                form.ShowDialog();
            }
            
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-12
        /// 
        /// Assign the guest to the room in the selected ViewModel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAssign_Click(object sender, RoutedEventArgs e)
        {
            SelectedVM = (VMRoomRoomReservation)dgAvailableRooms.SelectedItem;
            if (SelectedVM != null)
            {
                var result = _roomReservationManager.AddGuestAssignment(
                    _guestId, SelectedVM.RoomReservationID);
                if (result == true)
                {
                    // Assigned the Guest successfully.
                    
                    DialogResult = true;
                } else
                {
                    // Failed for some reason.
                    MessageBox.Show("Error assigning Guest to room.");
                    DialogResult = false;
                    SelectedVM = null;
                }
            }
        }
    }
}
