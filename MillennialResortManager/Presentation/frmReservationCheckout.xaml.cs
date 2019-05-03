using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
        private MemberManagerMSSQL _memberManager;
        private ReservationManagerMSSQL _reservationManager;
        private MemberTabManager _tabManager;
        private OfferingManager _offeringManager;
        private CheckoutReceiptManager _checkoutReceiptManager;
        private int _reservationID;
        public frmReservationCheckout(int reservationID)
        {
            _reservationID = reservationID;
            _roomAssignmentManager = new GuestRoomAssignmentManager();
            _memberManager = new MemberManagerMSSQL();
            _reservationManager = new ReservationManagerMSSQL();
            _tabManager = new MemberTabManager();
            _offeringManager = new OfferingManager();
            _checkoutReceiptManager = new CheckoutReceiptManager();
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
                try
                {
                    string path = generateReportHTML();
                }
                catch (Exception)
                {
                    MessageBox.Show("Receipt Error");
                }
            }
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-30
        /// Creates the customer receipt in html 
        /// </summary>
        private string generateReportHTML()
        {
            String filepath = AppDomain.CurrentDomain.BaseDirectory + @"Receipts";
            Reservation reservation = _reservationManager.RetrieveReservation(_reservationID);
            Member member = _memberManager.RetrieveMember(reservation.MemberID);
            List<OfferingVM> allOfferings = _offeringManager.RetrieveAllOfferingViewModels();
            MemberTab tab = _tabManager.RetrieveLastMemberTabByMemberID(member.MemberID);
            // Removed because CSS wouldn't be included if they didn't choose correct folder. 
            // Possible future feature, maybe specify static CSS location.

            //Instead of using a static folder, let the user pick folder
            //CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //dialog.InitialDirectory = "C:\\Users";
            //dialog.IsFolderPicker = true;
            //if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            //{
            //    filepath = dialog.FileName;
            //}
            
            // Create the file name
            string fileName = @"\" + reservation.DepartureDate.ToShortDateString().Replace("/", "-") + member.Email + ".html";
            var result = _checkoutReceiptManager.generateMemberTabReceipt(reservation, member, allOfferings, tab, filepath + fileName, _allGuests);
            
            System.Diagnostics.Process.Start("IExplore.exe", filepath + fileName);
            return filepath + fileName;
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
            catch (Exception ex)
            {
                MessageBox.Show("Unable to checkout guest." + ex.Message);
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
