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
    /// James Heim and Jared Greenfield
    /// Created 2019-04-11
    /// 
    /// Interaction logic for GuestMemberRoomDetail.xaml
    /// </summary>
    public partial class GuestMemberRoomDetail : Window
    {

        private Guest _guest;
        private Member _member;
        private Reservation _reservation;
        private RoomReservation _roomReservation;
        private Room _room;
        private Building _building;
        private MemberTab _memberTab;

        private GuestManager _guestManager = new GuestManager();
        private MemberManagerMSSQL _memberManager = new MemberManagerMSSQL();
        private ReservationManagerMSSQL _reservationManager = new ReservationManagerMSSQL();
        private RoomReservationManager _roomReservationManager = new RoomReservationManager();
        private RoomManager _roomManager = new RoomManager();
        private BuildingManager _buildingManager = new BuildingManager();
        private MemberTabManager _memberTabManager = new MemberTabManager();

        private readonly string NO_ROOM_ASSIGNED_LABEL_MESSAGE = "Room not assigned.";
        private readonly string ROOM_ASSIGNED_LABEL_MESSAGE = "Room {0} in {1}.";

        public GuestMemberRoomDetail(Guest guest)
        {
            InitializeComponent();

            _guest = guest;
            _member = _memberManager.RetrieveMember(guest.MemberID);

            // Get the Member's only Open reservation.
            _reservation = _reservationManager.RetrieveReservationByGuestID(guest.GuestID);

            // Retrieve the Guest's assigned room.
            _roomReservation = _roomReservationManager.RetrieveRoomReservationByGuestID(_guest.GuestID);
            if (_roomReservation != null)
            {
                // If the Room is assigned, get the room data.
                _room = _roomManager.RetreieveRoomByID(_roomReservation.RoomID);
                _building = _buildingManager.RetrieveBuilding(_room.Building);
                
            }

            // Populate the form.
            populateGuestTextBoxes();
            populateMemberTextBoxes();
            populateRoomInformation();
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-11
        /// 
        /// Update the fields and labels on the form with the 
        /// room details.
        /// </summary>
        private void populateRoomInformation()
        {
            
            if (_roomReservation == null)
            {
                // The guest has not been assigned to a Room Reservation.
                lblRoomMessage.Content = NO_ROOM_ASSIGNED_LABEL_MESSAGE;
                btnRoomAssignment.Content = "Assign";
                btnCheckIn.IsEnabled = false;
            } 
            else
            {
                lblRoomMessage.Content = string.Format(ROOM_ASSIGNED_LABEL_MESSAGE, _room.RoomNumber, _building.Name);
                btnRoomAssignment.Content = "Unassign";
                btnCheckIn.IsEnabled = true;
            }
            if (_guest.CheckedIn == false)
            {
                // Guest is assigned a room but not checked in.
                btnCheckIn.Content = "Check In";
            }
            else
            {
                // Guest is assigned a room and checked in.
                btnCheckIn.Content = "Check Out";
            }
        }

        private void populateGuestTextBoxes()
        {
            txtGuestFirstName.Text = _guest.FirstName;
            txtGuestLastName.Text = _guest.LastName;
            txtGuestPhoneNumber.Text = _guest.PhoneNumber;
        }

        private void populateMemberTextBoxes()
        {
            txtMemberFirstName.Text = _member.FirstName;
            txtMemberLastName.Text = _member.LastName;
            txtMemberPhoneNumber.Text = _member.PhoneNumber;
        }


        private void BtnViewGuestDetails_Click(object sender, RoutedEventArgs e)
        {
            frmAddEditGuest guestDetail = new frmAddEditGuest(_guest);
            var formResult = guestDetail.ShowDialog();
            if (formResult == true)
            {
                // If the guest form was edited, refresh the guest.
                _guest = _guestManager.ReadGuestByGuestID(_guest.GuestID);
                populateGuestTextBoxes();
            }
        }

        private void BtnViewMemberDetails_Click(object sender, RoutedEventArgs e)
        {
            frmAccount memberDetail = new frmAccount(_member);
            var formResult = memberDetail.ShowDialog();
            if (formResult == true)
            {
                // If the member form was edited, refresh the guest.
                try
                {
                    _member = _memberManager.RetrieveMember(_member.MemberID);
                }
                catch (Exception)
                {

                    throw;
                }
                populateMemberTextBoxes();
            }
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-11
        /// 
        /// Add or Modify a guest assignment to a Room Reservation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRoomAssignment_Click(object sender, RoutedEventArgs e)
        {
            if (_roomReservation == null)
            {
                // Assign a guest to a room.

                // Get the list of RoomReservations that can be assigned a guest.
                List<VMRoomRoomReservation> vmRooms = _roomReservationManager.RetrieveAvailableVMRoomRoomReservations(_reservation.ReservationID);

                BrowseAvailableRooms availableRoomsForm = new BrowseAvailableRooms(_guest.GuestID, vmRooms);
                var formResult = availableRoomsForm.ShowDialog();
                if (formResult == true)
                {
                    // Guest was assigned to a room. Refresh the Room info.
                    _roomReservation = new RoomReservation()
                    {
                        ReservationID = _reservation.ReservationID,
                        RoomReservationID = availableRoomsForm.SelectedVM.RoomReservationID,
                        RoomID = availableRoomsForm.SelectedVM.RoomID,
                        CheckInDate = availableRoomsForm.SelectedVM.CheckInDate,
                        CheckOutDate = availableRoomsForm.SelectedVM.CheckOutDate
                    };

                    _room = _roomManager.RetreieveRoomByID(_roomReservation.RoomID);
                    _building = _buildingManager.RetrieveBuilding(_room.Building);

                    populateRoomInformation();
                    
                }
            }
            else
            {

                // Unassign the Guest.
                bool result = false;
                try
                {
                    result = _roomReservationManager.DeleteGuestAssignment(_guest.GuestID, _roomReservation.RoomReservationID);

                    // Succesfully Removed.
                    if (result)
                    {
                        _roomReservation = null;
                        _room = null;

                        populateRoomInformation();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Unable to remove guest's room assignment. Error: \n" + ex.Message);
                }

                // CheckOut the Guest.

            }
        }

        private void BtnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            checkInGuest();
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-11
        /// 
        /// 
        /// </summary>
        private void checkInGuest()
        {
            if (!_guest.CheckedIn)
            {
                // Guest is not checked in yet.

                // Check if the guest is assigned a room.
                if (_room != null)
                {
                    try
                    {
                        // Check if there already exists a Member Tab.
                        _memberTab = _memberTabManager.RetrieveActiveMemberTabByMemberID(_member.MemberID);

                        // Assign guest to member tab.
                        // Check if Guest already exists in GuestTabAssignment
                        // Assign Guest to GuestTabAssignment
                        MessageBox.Show("Currently the MemberTab is not hooked up to Guest CheckIn.");

                        // Set CheckInDate on RoomReservation
                        _roomReservationManager.UpdateCheckInDateToNow(_roomReservation);

                        // Set CheckedIn on Guest
                        _guestManager.CheckInGuest(_guest.GuestID);

                        // Update the form.
                        btnCheckIn.Content = "Check Out";
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Error Checking Guest In");
                    }
                    
                }
            }
            else
            {
                // Check out the Guest.

            }
        }
    }
}
