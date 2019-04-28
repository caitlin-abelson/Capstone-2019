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
    /// Author: Jared Greenfield
    /// Created On: 2019-04-25
    /// Checkout Window for Users in a Reservation
    /// </summary>
    public partial class frmReservationCheckout : Window
    {
        private List<GuestRoomAssignmentVM> _allGuests;
        private List<GuestRoomAssignmentVM> _currentGuests;
        private GuestRoomAssignmentManager _roomAssignmentManager;
        private int _reservationID;
        public frmReservationCheckout(int reservationID)
        {
            _reservationID = reservationID;
            _roomAssignmentManager = new GuestRoomAssignmentManager();
            InitializeComponent();
            try
            {
                populateGrid();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Populate Everything
        /// </summary>
        private void populateGrid()
        {
            _allGuests = _roomAssignmentManager.SelectGuestRoomAssignmentVMSByReservationID(_reservationID);
            dgReservationGuests.ItemsSource = _allGuests;
            if (_allGuests.Find(x => x.CheckOutDate == null) == null)
            {
                MessageBox.Show("Go to final tab page.");
            }
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Exit Page
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Checkout a Guest
        /// </summary>
        private void BtnCheckoutIndividual_Click(object sender, RoutedEventArgs e)
        {
            if (dgReservationGuests.SelectedItem != null)
            {
                var assignment = (GuestRoomAssignmentVM)dgReservationGuests.SelectedItem;
                try
                {
                    var approvalForm = new frmConfirmAction(CrudFunction.Checkout);
                    var approved = approvalForm.ShowDialog();
                    if (approved == true)
                    {
                        var result = _roomAssignmentManager.UpdateGuestRoomAssignmentToCheckedOut(assignment.GuestID, assignment.RoomReservationID);
                        if (result == true)
                        {
                            populateGrid();
                        }
                        else
                        {
                            MessageBox.Show("Checkout Failed!");
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to checkout guest.");
                }
            }
            else
            {
                MessageBox.Show("Please select a Guest to checkout.");
            }
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Format Grid
        /// </summary>
        private void DgReservationGuests_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime?))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
            }
                
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Hide the Checkout button if a record has already been checked out
        /// </summary>
        private void DgReservationGuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgReservationGuests.SelectedItem != null && ((GuestRoomAssignmentVM)dgReservationGuests.SelectedItem).CheckOutDate == null)
            {
                btnCheckoutIndividual.Visibility = Visibility.Visible;
            }
            else
            {
                btnCheckoutIndividual.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Checkout all Guests
        /// </summary>
        private void BtnCheckoutAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var approvalForm = new frmConfirmAction(CrudFunction.Checkout);
                var approved = approvalForm.ShowDialog();
                if (approved == true)
                {
                    foreach (GuestRoomAssignmentVM assignment in _allGuests)
                    {
                        if (assignment.CheckOutDate == null)
                        {
                            var result = _roomAssignmentManager.UpdateGuestRoomAssignmentToCheckedOut(assignment.GuestID, assignment.RoomReservationID);
                            if (result == true)
                            {
                                populateGrid();
                            }
                        }
                    }
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to checkout guest.");
            }
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Sort all Guests by last name
        /// </summary>
        private void BtnFilterReservation_Click(object sender, RoutedEventArgs e)
        {
            _currentGuests = _allGuests.FindAll(x => x.LastName.ToLower().Contains(txtLastName.Text.ToLower()));
            dgReservationGuests.ItemsSource = _currentGuests;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Clear Filter
        /// </summary>
        private void BtnClearFiltersReservation_Click(object sender, RoutedEventArgs e)
        {
            txtLastName.Text = "";
            populateGrid();
        }
    }
}
