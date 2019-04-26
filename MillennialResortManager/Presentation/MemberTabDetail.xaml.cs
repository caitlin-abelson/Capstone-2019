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
using WpfPresentation;

namespace Presentation
{
    /// <summary>
    /// James Heim
    /// Created 2019-04-25
    /// 
    /// Interaction logic for MemberTabDetail.xaml
    /// </summary>
    public partial class MemberTabDetail : Window
    {
        private MemberManagerMSSQL _memberManager = new MemberManagerMSSQL();
        private Member _member;

        private MemberTabManager _memberTabManager = new MemberTabManager();
        private MemberTab _memberTab;

        /// <summary>
        /// Managers for all the different kinds of Offerings.
        /// </summary>
        private OfferingManager _offeringManager = new OfferingManager();
        private List<string> _offeringTypes;
        private RoomManager _roomManager = new RoomManager();
        private LogicLayer.EventManager _eventManager = new LogicLayer.EventManager();
        private ItemManager _itemManager = new ItemManager();

        private GuestManager _guestManager = new GuestManager();

        private EmployeeManager _employeeManager = new EmployeeManager();

        private VMTabLine _selectedItem;

        private List<VMTabLine> _vmTabLines;


        /// <summary>
        /// List of Strings for Labels and Buttons.
        /// </summary>
        private readonly string ACTIVE_TAB_TITLE = "Current Tab for {0}";
        private readonly string INACTIVE_TAB_TITLE = "There is No Active Tab for {0}";
        private readonly string PREVIOUS_TAB_TITLE = "Previous Tab for {0}, last used {1}";

        private readonly string TOTAL_TITLE = "Total: {0}";

        private readonly string PREVIOUS_TABS = "Previous Tabs";
        private readonly string VIEW_TABS = "View Tabs";

        /// <summary>
        /// This DateTime is set only by the MemberTabList form.
        /// It allows the title of previous forms to have a datestamp.
        /// </summary>
        private DateTime _date;

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Constructor that displays the specified Member's
        /// active tab.
        /// </summary>
        /// <param name="memberID"></param>
        public MemberTabDetail(int memberID)
        {
            InitializeComponent();

            // Get the list of Offering Types.
            _offeringTypes = _offeringManager.RetrieveAllOfferingTypes();

            try
            {
                _member = _memberManager.RetrieveMember(memberID);
                _memberTab = _memberTabManager.RetrieveActiveMemberTabByMemberID(memberID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Retrieving Tab Details.");
                DialogResult = false;
            }

            updateLabels();
            loadTabLines();

        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// Update the Labels and buttons on the form.
        /// </summary>
        private void updateLabels()
        {
            if (_memberTab != null && _memberTab.Active == true)
            {
                // Setup the form.
                lblTitle.Content = string.Format(ACTIVE_TAB_TITLE, (_member.FirstName + " " + _member.LastName));
                lblTotal.Content = string.Format(TOTAL_TITLE, _memberTab.TotalPrice.ToString("C"));
                btnViewPreviousTabs.Content = PREVIOUS_TABS;
            }
            else if (_memberTab != null && _memberTab.Active == false)
            {
                lblTitle.Content = string.Format(PREVIOUS_TAB_TITLE, 
                    (_member.FirstName + " " + _member.LastName), 
                    _date.ToString("MM/dd/yyyy hh:mm tt"));
                lblTotal.Content = string.Format(TOTAL_TITLE, _memberTab.TotalPrice.ToString("C"));
                btnViewPreviousTabs.Content = VIEW_TABS;
            }
            else
            {
                // No active tab. Set the labels accordingly.
                lblTitle.Content = string.Format(INACTIVE_TAB_TITLE, _member.FirstName + " " + _member.LastName);
                lblTotal.Content = string.Format(TOTAL_TITLE, 0.00.ToString("C"));
                btnViewPreviousTabs.Content = PREVIOUS_TABS;
            }
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-29
        /// 
        /// Populate the DataGrid with the TabLines.
        /// </summary>
        private void loadTabLines()
        {
            try
            {

                _vmTabLines = new List<VMTabLine>();

                // Build the TabLine View Models.
                foreach (var tab in _memberTab.MemberTabLines)
                {
                    var vmTab = new VMTabLine()
                    {
                        Date = tab.DatePurchased,
                        FormattedPrice = tab.Price.ToString("C"),
                        Quantity = tab.Quantity,

                        OfferingID = tab.OfferingID
                    };

                    // Get the specific Offering so we can save the Description and TypeID.
                    var offering = _offeringManager.RetrieveOfferingByID(tab.OfferingID);
                    vmTab.OfferingDescription = offering.Description;
                    vmTab.OfferingType = offering.OfferingTypeID;

                    if (tab.GuestID != null)
                    {
                        vmTab.GuestID = tab.GuestID;
                        var guest = _guestManager.ReadGuestByGuestID((int)tab.GuestID);
                        vmTab.GuestName = guest.FirstName + " " + guest.LastName;
                    }
                    else
                    {
                        vmTab.GuestName = _member.FirstName + " " + _member.LastName;
                    }

                    if (tab.EmployeeID != null)
                    {
                        vmTab.EmployeeID = tab.EmployeeID;
                        var employee = _employeeManager.RetrieveEmployeeInfo((int)tab.EmployeeID);
                        vmTab.EmployeeName = employee.FirstName + " " + employee.LastName;
                    }
                    else
                    {
                        vmTab.EmployeeName = "";
                    }

                    _vmTabLines.Add(vmTab);
                }

                dgTabLines.ItemsSource = _vmTabLines;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error", ex.Message);
            }
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// Retrieve the specific Offering by the Offering ID.
        /// Cannot implement: Current data does not have OfferingID required nor are the
        /// stored procedures in place to retrieve by OfferingID. Unable to implement in
        /// the limited time remaining.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnViewOffering_Click(object sender, RoutedEventArgs e)
        {
            //if (_selectedItem != null)
            //{

            //    Window window;

            //    try
            //    {
            //        if (_selectedItem.OfferingType == "Event")
            //        {
            //            // The Event form constructor takes the EmployeeID and the Event.    
            //            var evnt = _eventManager.RetrieveEventByOfferingID();
            //            window = new frmAddEditEvent(DevLauncher.Employee, evnt);
            //            window.ShowDialog();
            //        }
            //        else if (_selectedItem.OfferingType == "Item")
            //        {
            //            // The Item form constructor takes the ItemID.
            //            window = new Items();
            //            window.ShowDialog();
            //        }
            //        else if (_selectedItem.OfferingType == "Room")
            //        {
            //            // The Room form constructor takes an "EditMode" Enum and the RoomID.
            //            window = new frmAddEditViewRoom(EditMode.View, _selectedItem.OfferingID);
            //            window.ShowDialog();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
                

            //}
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// View the Guest Detail (if exists) for the selected ViewModel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnViewGuest_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem != null && _selectedItem.GuestID != null)
            {
                Guest guest = null;

                try
                {
                    guest = _guestManager.ReadGuestByGuestID((int)_selectedItem.GuestID);
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                if (guest != null)
                {
                    frmAddEditGuest window = new frmAddEditGuest(guest);
                    window.ShowDialog();
                }
                
            }
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// View the Employee Detail (if exists) for the selected ViewModel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnViewEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem != null && _selectedItem.EmployeeID != null)
            {
                Employee employee = null;

                try
                {
                    employee = _employeeManager.SelectEmployee((int)_selectedItem.EmployeeID);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                if (employee != null)
                {
                    EmployeeDetail window = new EmployeeDetail(employee);
                    window.ShowDialog();
                }

            }
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// View all tabs for the Member.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnViewPreviousTabs_Click(object sender, RoutedEventArgs e)
        {
            MemberTabList tabListWindow = new MemberTabList(_member.MemberID);
            var result = tabListWindow.ShowDialog();
            if (result == true && tabListWindow.SelectedItem != null)
            {
                // Retrieve the selected Tab.
                _memberTab = _memberTabManager.RetrieveMemberTabByID(tabListWindow.SelectedItem.MemberTabID);

                // Supply a date for the "previous tab" title.
                _date = tabListWindow.SelectedItem.Date;

                // Update the form.
                updateLabels();
                loadTabLines();
            }
        }


        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// If the ViewModel does not have an OfferingType with a valid detail
        /// (not Event, Item, or Room) disable the View Offering button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgTabLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedItem = (VMTabLine) dgTabLines.SelectedItem;

            if (_selectedItem != null)
            {

                // Offering Button.
                if (_selectedItem.OfferingType == "Event" || 
                    _selectedItem.OfferingType == "Item" ||
                    _selectedItem.OfferingType == "Room")
                {
                    btnViewOffering.IsEnabled = true;
                }
                else
                {
                    btnViewOffering.IsEnabled = false;
                }

                // Guest Button.
                if (_selectedItem.GuestID != null)
                {
                    btnViewGuest.IsEnabled = true;
                }
                else
                {
                    btnViewGuest.IsEnabled = false;
                }

                // Employee Button.
                if (_selectedItem.EmployeeID != null)
                {
                    btnViewEmployee.IsEnabled = true;
                }
                else
                {
                    btnViewEmployee.IsEnabled = false;
                }
            }

        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// Close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
