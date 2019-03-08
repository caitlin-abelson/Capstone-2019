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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicLayer;
using DataObjects;
using Presentation;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for BrowseGuests.xaml
    /// </summary>
    public partial class BrowseGuests : Window
    {

        private List<Guest> _guests = new List<Guest>();
        private List<Guest> _guestsSearched = new List<Guest>();
        private GuestManager _guestManager = new GuestManager();

        /// <summary>
        ///  Alisa Roehr
        /// Created: 2019/01/25
        /// 
        /// The constructor for the browse window.
        /// </summary>
        public BrowseGuests()
        {
            InitializeComponent();
            _guestManager = new GuestManager();
            try
            {
                _guests = _guestManager.ReadAllGuests();
                if (dgGuests.ItemsSource == null)
                {
                    dgGuests.ItemsSource = _guests;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/01
        /// 
        /// for loading the guest details
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgGuests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgGuests.SelectedItem != null && ((Guest)dgGuests.SelectedItem).Active != false)
                {
                    var selectedGuest = (Guest)dgGuests.SelectedItem;
                    var detail = new frmAddEditGuest(selectedGuest);
                    var result = detail.ShowDialog();
                    _guests = _guestManager.ReadAllGuests();
                    dgGuests.ItemsSource = _guests;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Viewing Guest Failed!");
            }
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/01
        /// 
        /// for creating a new guest. 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            var detail = new frmAddEditGuest();
            detail.ShowDialog();
            _guests = _guestManager.ReadAllGuests();
            dgGuests.ItemsSource = _guests;
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/05
        /// 
        /// for searching for guests.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuestSearch_Click(object sender, RoutedEventArgs e)
        {
           /* try
            {
                string searchFirst = txtGuestFirst.Text.ToString();
                string searchLast = txtGuestLast.Text.ToString();
                searchFirst.Trim();
                searchLast.Trim();

                searchFirst.ToLower();
                searchLast.ToLower();

                _guestsSearched = _guestManager.RetrieveGuestsSearched(searchLast, searchFirst);
                dgGuests.ItemsSource = _guestsSearched;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Searching Guests Failed!");
            }*/
            string searchFirst = txtGuestFirst.Text.ToString();
            string searchLast = txtGuestLast.Text.ToString();
            searchFirst.Trim();
            searchLast.Trim();
            _guestsSearched = _guests.FindAll(g => g.FirstName.ToLower().Contains(searchFirst) 
                && g.LastName.ToLower().Contains(searchLast));
            dgGuests.ItemsSource = _guestsSearched;
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/03/01
        /// 
        /// for activating and deactivating guests.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActivateGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGuests.SelectedItem != null)
                {
                    Guest guest = _guestManager.ReadGuestByGuestID(((Guest)dgGuests.SelectedItem).GuestID);
                    if (guest.Active == true)
                    {
                        _guestManager.DeactivateGuest(guest.GuestID);
                    }
                    else if (guest.Active == false)
                    {
                        _guestManager.ReactivateGuest(guest.GuestID);
                    }
                    _guests = _guestManager.ReadAllGuests();
                    dgGuests.ItemsSource = _guests;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Activating or Deactivating Guest Failed!");
            }
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/03/01
        /// 
        /// for deleting guests.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGuests.SelectedItem != null)
                {
                    Guest guest = _guestManager.ReadGuestByGuestID(((Guest)dgGuests.SelectedItem).GuestID);
                    if (guest.Active == false)
                    {
                        var result = MessageBox.Show("Are you sure you want to delete this guest?", "This guest will no longer be in the system.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            _guestManager.DeleteGuest(guest.GuestID);
                            MessageBox.Show("The guest has been purged.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Guest must be deactivated to be deleted.");
                    }
                    _guests = _guestManager.ReadAllGuests();
                    dgGuests.ItemsSource = _guests;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Deleting Guest Failed!");
            }

        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/03/01
        /// 
        /// for checking in and out guests.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGuests.SelectedItem != null)
                {
                    Guest guest = _guestManager.ReadGuestByGuestID(((Guest)dgGuests.SelectedItem).GuestID);
                    if (guest.CheckedIn == false)
                    {
                        _guestManager.CheckInGuest(guest.GuestID);
                    }
                    else if (guest.CheckedIn == true)
                    {
                        _guestManager.CheckOutGuest(guest.GuestID);
                    }
                    _guests = _guestManager.ReadAllGuests();
                    dgGuests.ItemsSource = _guests;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Checking In or Out Guest Failed!");
            }

        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/01
        /// 
        /// for loading the guest details
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGuests.SelectedItem != null && ((Guest)dgGuests.SelectedItem).Active != false)
                {
                    var selectedGuest = (Guest)dgGuests.SelectedItem;
                    var detail = new frmAddEditGuest(selectedGuest);
                    var result = detail.ShowDialog();
                    _guests = _guestManager.ReadAllGuests();
                    dgGuests.ItemsSource = _guests;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Viewing Guest Failed!");
            }
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/03/05
        /// 
        /// for picking what the selected item is and the buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgGuests_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGuests.SelectedItem != null)
                {
                    Guest _selectedGuest = new Guest();
                    try
                    {
                         _selectedGuest = (Guest)dgGuests.SelectedItem;
                    }
                    catch (Exception)
                    {
                        
                    }
                    btnCheckGuest.IsEnabled = true;
                    btnActivateGuest.IsEnabled = true;

                    if (_selectedGuest.Active)
                    {
                        btnActivateGuest.Content = "Deactivate";
                        btnDeleteGuest.IsEnabled = false;
                    }
                    else
                    {
                        btnActivateGuest.Content = "Activate";
                        btnDeleteGuest.IsEnabled = true;
                    }
                    if (_selectedGuest.CheckedIn)
                    {
                        btnCheckGuest.Content = "Check Out";
                    }
                    else
                    {
                        btnCheckGuest.Content = "Check In";
                    }
                }
                else
                {
                    btnDeleteGuest.IsEnabled = false;
                    btnCheckGuest.IsEnabled = false;
                    btnActivateGuest.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Focusing for buttons failure");
            }
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/03/08
        /// 
        /// for clearing the filters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuestClearFilter_Click(object sender, RoutedEventArgs e)
        {
            txtGuestFirst.Text = "";
            txtGuestLast.Text = "";
            _guests = _guestManager.ReadAllGuests();
            dgGuests.ItemsSource = null;
            dgGuests.ItemsSource = _guests;
            _guestsSearched = _guests;
        }
    }
}
