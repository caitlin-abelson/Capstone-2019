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
    /// Author: Matt LaMarche
    /// Created : 2/27/2019
    /// This is a launcher for Developers to use while we develop functionality for our program
    /// 
    /// To quickly find the section of code you are looking for Ctrl + F and look for one of these Keys:
    /// #Sidebar
    /// #NavBar
    /// #BrowseReservation
    /// #BrowseShops
    /// #BrowseEmployees
    /// #BrowseSuppliers
    /// #BrowseProducts
    /// #BrowseBuilding
    /// #BrowseOrder
    /// #BrowseEmployeeRole
    /// #BrowseItemSuppliers
    /// #BrowseRoom
    /// #BrowseGuestTypes
    /// #BrowseRoomTypes
    /// 
    /// #BrowsePerformance
    /// 
    /// </summary>
    public partial class DevLauncher : Window
    {
        private Employee _employee;
        private List<VMBrowseReservation> _allReservations;
        private List<VMBrowseReservation> _currentReservations;
        private ReservationManagerMSSQL _reservationManager;
        private List<VMBrowseShop> _allShops;
        private List<VMBrowseShop> _currentShops;
        private ShopManagerMSSQL _shopManager;
        private EmployeeManager _employeeManager;
        private List<Employee> _employees;
        private List<Employee> _currentEmployees;
        private List<Supplier> _suppliers;
        private List<Supplier> _currentSuppliers;
        private SupplierManager _supplierManager;
        private List<Product> _allProducts;
        private List<Product> _currentProducts;
        private ProductManagerMSSQL _productManager;
        private Product _selectedProduct;
        private List<Building> allBuildings;
        private List<Building> currentBuildings; // needed?
        private IBuildingManager buildingManager;
        private List<string> _searchCategories;
        private UserManager _userManager;
        private InternalOrderManager _internalOrderManager;
        private User _fullUser;
        private List<VMInternalOrder> _orders;
        private List<VMInternalOrder> _currentOrders;
        private IRoleManager _roleManager;
        private List<Role> _roles;
        private List<Role> _currentRoles;
        private Role _selectedRole;
        private List<GuestType> _guests;
        private List<GuestType> _currentGuests;
        private IGuestTypeManager guestManager;
        private List<RoomType> _room;
        private List<RoomType> _currentRoom;
        private IRoomType roomManager;
        //Performance
        private PerformanceManager performanceManager;
        //Item Supplier
        private ItemSupplierManager _itemSupplierManager;
        //private Product _item;
        //private List<ItemSupplier> _itemSuppliers;
        //private ItemSupplier _itemSupplier;

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// Initializes all the pages components required at log in
        /// </summary>
        /// <param name="employee"></param>
        public DevLauncher(Employee employee)
        {
            _employee = employee;
            InitializeComponent();
            //For Sidebar
            HideSidebarSubItems();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// Returns the user to the Login page and closes the current session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginPage();
            this.Close();
            login.ShowDialog();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// For the Sidebar - Toggles the sidebars visibility
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            ToggleSidebar();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// Displays a page based on the name of the page and hides all the other pages
        /// </summary>
        /// <param name="v">The name of the page we want to display</param>
        private void DisplayPage(string v)
        {
            //Goes to the Content Grid and does a for loop through the Grids located within Content
            foreach (Grid c in Content.Children)
            {
                if (c is Grid)
                {
                    Grid b = c as Grid;
                    //We only care about Grids which act as a Page. Hides all pages which do not contain the name we are searching for and shows the one we want
                    if (b.Name.Contains("Page"))
                    {
                        if (b.Name.Contains(v))
                        {
                            b.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            b.Visibility = Visibility.Hidden;
                        }
                    }
                }
            }
        }


        /*--------------------------- Starting SideBar Code #Sidebar --------------------------------*/

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// Hides the buttons in the sidebar which are not headers. This makes the sidebar cleaner and appears more responsive
        /// </summary>
        private void HideSidebarSubItems()
        {
            int HeaderCount = 0;
            foreach (Control c in SideBarButtons.Children)
            {
                if (c is Button)
                {
                    Button b = c as Button;
                    if (b.Name.Contains("SubHeader"))
                    {
                        HideButton(b);
                        b.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        b.SetValue(Grid.RowProperty, HeaderCount);
                        HeaderCount++;
                    }
                }
            }
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// All references to Hide a button are done here in case we want to do more than just toggle visibility
        /// </summary>
        /// <param name="b"></param>
        private void HideButton(Button b)
        {
            b.Visibility = Visibility.Hidden;
            b.Height = 0;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// All references to Show a button are done here to be sure buttons are restored properly in case we are not just toggling visibility
        /// </summary>
        /// <param name="b"></param>
        private void ShowButton(Button b)
        {
            b.Visibility = Visibility.Visible;
            b.Height = double.NaN;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// Displays all the sidebar sub item buttons based on which header button was clicked on
        /// </summary>
        /// <param name="v">Name of the Header we are using to find subheaders of</param>
        private void DisplaySideBarSubButtonsByHeader(string v)
        {
            int HeaderCount = 0;
            foreach (Control c in SideBarButtons.Children)
            {
                if (c is Button)
                {
                    Button b = c as Button;
                    if (b.Name.Contains("SubHeader") && !b.Name.Contains(v))
                    {
                        b.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        if (!b.Name.Contains("SubHeader"))
                        {
                            //Header
                            b.SetValue(Grid.RowProperty, HeaderCount);
                            HeaderCount++;
                        }
                        else if (b.Visibility == Visibility.Hidden)
                        {
                            b.SetValue(Grid.RowProperty, HeaderCount);
                            ShowButton(b);
                            HeaderCount++;
                        }
                        else
                        {
                            HideButton(b);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// Hides the Sidebar and Sidebar subheader buttons
        /// </summary>
        private void HideSidebar()
        {
            SideBar.Visibility = Visibility.Hidden;
            HideSidebarSubItems();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// Shows the Sidebar
        /// </summary>
        private void ShowSidebar()
        {
            SideBar.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// Toggles the sidebar depending on its current visibility status 
        /// </summary>
        private void ToggleSidebar()
        {
            if (SideBar.Visibility == Visibility.Visible)
            {
                HideSidebar();
            }
            else
            {
                ShowSidebar();
            }
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the header button for Reservation is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarHeaderReservation_Click(object sender, RoutedEventArgs e)
        {
            DisplaySideBarSubButtonsByHeader("Reservation");
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the header button for Guest Services is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarHeaderGuestServices_Click(object sender, RoutedEventArgs e)
        {
            DisplaySideBarSubButtonsByHeader("Guest");
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the header button for Food Services is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarHeaderFoodServices_Click(object sender, RoutedEventArgs e)
        {
            DisplaySideBarSubButtonsByHeader("Food");
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Reserving a room is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarReservationSubHeaderReserveARoom_Click(object sender, RoutedEventArgs e)
        {
            HideSidebar();
            //Display stuff for Browse Reservation
            /*var BrowseReservation = new BrowseReservation(_employee);
            BrowseReservation.ShowDialog();*/
            DisplayPage("BrowseReservation");

            //Check. do we want to reset BrowseReservation here or do we want to keep the instance?
            //BrowseReservationDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Shops is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarReservationSubHeaderBrowseShops_Click(object sender, RoutedEventArgs e)
        {
            HideSidebar();
            DisplayPage("BrowseShops");
            // BrowseShopsDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Employees is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarReservationSubHeaderBrowseEmployees_Click(object sender, RoutedEventArgs e)
        {
            HideSidebar();
            DisplayPage("BrowseEmployees");
            //BrowseEmployeesDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Suppliers is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarReservationSubHeaderBrowseSuppliers_Click(object sender, RoutedEventArgs e)
        {
            HideSidebar();
            DisplayPage("BrowseSuppliers");
            //BrowseSuppliersDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Products is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarReservationSubHeaderBrowseProducts_Click(object sender, RoutedEventArgs e)
        {
            HideSidebar();
            DisplayPage("BrowseProducts");
            //BrowseProductsDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Buildings is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarReservationSubHeaderBrowseBuilding_Click(object sender, RoutedEventArgs e)
        {
            HideSidebar();
            DisplayPage("BrowseBuilding");
            //BrowseBuildingDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Orders is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarReservationSubHeaderBrowseOrders_Click(object sender, RoutedEventArgs e)
        {
            HideSidebar();
            DisplayPage("BrowseOrders");
            //BrowseOrderDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Employee Roles is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarReservationSubHeaderBrowseEmployeeRoles_Click(object sender, RoutedEventArgs e)
        {
            HideSidebar();
            DisplayPage("BrowseEmployeeRoles");
            //BrowseEmployeeRolesDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Guest Types is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarReservationSubHeaderBrowseGuestType_Click(object sender, RoutedEventArgs e)
        {
            HideSidebar();
            DisplayPage("BrowseGuestTypes");
            //BrowseGuestTypesDoOnStart();

        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Room Types is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarReservationSubHeaderBrowseRoomType_Click(object sender, RoutedEventArgs e)
        {
            HideSidebar();
            DisplayPage("BrowseRoomType");
            //BrowseRoomTypesDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Performances is clicked from the sidebar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSidebarReservationSubHeaderBrowsePerformance_Click(object sender, RoutedEventArgs e)
        {
            HideSidebar();
            DisplayPage("BrowsePerformance");
            //BrowsePerformanceDoOnStart();
        }

        /*--------------------------- Ending SideBar Code --------------------------------*/


        /*--------------------------- Starting NavBar Code #NavBar --------------------------------*/

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Reservations is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderReservation_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseReservation");
            //BrowseReservationDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Shops is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderShops_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseShops");
            // BrowseShopsDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Employees is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderEmployee_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseEmployees");
            //BrowseEmployeesDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Suppliers is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderSuppliers_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseSuppliers");
            //BrowseSuppliersDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Products is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderProducts_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseProducts");
            //BrowseProductsDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Buildings is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderBuildings_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseBuilding");
            //BrowseBuildingDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Orders is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderOrders_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseOrders");
            //BrowseOrderDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Employee Roles is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderEmployeeRoles_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseEmployeeRoles");
            //BrowseEmployeeRolesDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Guest Types is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderGuestTypes_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseGuestTypes");
            //BrowseGuestTypesDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Room Types is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderRoomTypes_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseRoomType");
            //BrowseRoomTypesDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/07/2019
        /// This is what happens when the subheader button for Performances is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderPerformances_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowsePerformance");
            //BrowsePerformanceDoOnStart();
        }


        /*--------------------------- Ending NavBar Code --------------------------------*/


        /*--------------------------- Starting BrowseReservation Code #BrowseReservation --------------------------------*/

        private void BrowseReservationDoOnStart()
        {
            _reservationManager = new ReservationManagerMSSQL();
            refreshAllReservations();
            //Add Business rules based on Employee Roles and whatnot
            //Stick this in refreshAllReservations() when business rules are known
            //For now this would filter to all Reservations which have at least one day fall within the next 7 days
            //filterByDateRange(DateTime.Now.Date, DateTime.Now.AddDays(7).Date);
            populateReservations();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// Updated : 2/08/2019
        /// gets a list of all Reservations from our database and updates our lists
        /// </summary>
        private void refreshAllReservations()
        {
            try
            {
                _allReservations = _reservationManager.RetrieveAllVMReservations();
                _currentReservations = _allReservations;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// sets the Data Grids Item Source to our current reservations
        /// </summary>
        private void populateReservations()
        {
            dgReservations.ItemsSource = _currentReservations;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// The function which runs when Add Reservation is clicked
        /// </summary>
        private void btnAddReservation_Click(object sender, RoutedEventArgs e)
        {
            var createReservation = new CreateReservation(_reservationManager);
            createReservation.ShowDialog();
            refreshAllReservations();
            populateReservations();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// Updated : 2/08/2019 by Matt LaMarche
        /// The function which runs when Delete is clicked
        /// </summary>
        private void btnDeleteReservation_Click(object sender, RoutedEventArgs e)
        {
            if (dgReservations.SelectedIndex != -1)
            {
                var deleteReservation = new DeactivateReservation(((Reservation)dgReservations.SelectedItem), _reservationManager);
                deleteReservation.ShowDialog();
                refreshAllReservations();
                populateReservations();
            }
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// The function which runs when Add Member is clicked
        /// </summary>
        private void dgReservations_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yy";
            }

            string headerName = e.Column.Header.ToString();

            if (headerName == "ReservationID")
            {
                e.Cancel = true;
            }
            if (headerName == "MemberID")
            {
                e.Cancel = true;
            }
            /*
            if (headerName == "ArrivalDate")
            {
                e.Cancel = true;
            }
            if (headerName == "DepartureDate")
            {
                e.Cancel = true;
            }
            if (headerName == "NumberOfGuests")
            {
                e.Cancel = true;
            }
            if (headerName == "NumberOfPets")
            {
                e.Cancel = true;
            }
            if (headerName == "Notes")
            {
                e.Cancel = true;
            }*/
            if (headerName == "Active")
            {
                e.Cancel = true;
            }/*
            if (headerName == "FirstName")
            {
                e.Cancel = true;
            }
            if (headerName == "LastName")
            {
                e.Cancel = true;
            }
            if (headerName == "Email")
            {
                e.Cancel = true;
            }
            if (headerName == "PhoneNumber")
            {
                e.Cancel = true;
            }
            */
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// The function which runs when Clear Filters is clicked
        /// </summary>
        private void btnClearFiltersReservation_Click(object sender, RoutedEventArgs e)
        {
            _currentReservations = _allReservations;
            populateReservations();
            dtpDateSearch.Text = "";
            txtEmailReservation.Text = "";
            txtLastName.Text = "";
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// The function which runs when Filter is clicked
        /// </summary>
        private void btnFilterReservation_Click(object sender, RoutedEventArgs e)
        {
            //Check if Email box is populated
            if (!(txtEmailReservation.Text == null || txtEmailReservation.Text.Length < 1))
            {
                //Not null and strings length is greater than 0
                filterEmail(txtEmailReservation.Text);
            }

            //Check if last Name is populated
            if (!(txtLastName.Text == null || txtLastName.Text.Length < 1))
            {
                //Not null and strings length is freater than 0
                filterLastName(txtLastName.Text);
            }

            //Check if a date is populated
            if (!(dtpDateSearch.Text == null || dtpDateSearch.Text.Length < 1))
            {

                //date is not null and there is at least one character in the box
                DateTime tempDate = dtpDateSearch.SelectedDate.Value.Date;
                if (tempDate != null)
                {
                    MessageBox.Show("test: " + dtpDateSearch.Text);
                    filterBySpecificDate(tempDate);
                }
            }
            populateReservations();
            //Check and apply filters
            //MessageBox.Show("This has not been implemented yet");
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This method takes the current list of reservations and filters out the deactive ones 
        /// </summary>
        private void filterActiveOnly()
        {
            _currentReservations = _currentReservations.FindAll(x => x.Active);
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This method takes the current list of reservations and filters out the active ones
        /// </summary>
        private void filterDeActiveOnly()
        {
            _currentReservations = _currentReservations.FindAll(x => x.Active == false);
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This method takes the current list of reservations and filters out Reservations whose emails do not have the matching email string
        /// </summary>
        /// <param name="email">The email string we want to search our Reservations for</param>
        private void filterEmail(string email)
        {
            _currentReservations = _currentReservations.FindAll(x => x.Email.Contains(email));
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This method takes the current list of reservations and filters out Reservations whose last names do not have the matching lastName string
        /// </summary>
        /// <param name="lastName">The last name string which we want to search out Reservations for</param>
        private void filterLastName(string lastName)
        {
            _currentReservations = _currentReservations.FindAll(x => x.LastName.Contains(lastName));
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This method takes the current list of reservations and filters out Reservations whose Arrival dates are before the given date
        /// </summary>
        /// <param name="date">The date which we want to compare our arrival dates against</param>
        private void filterByArrivalDate(DateTime date)
        {
            _currentReservations = _currentReservations.FindAll(x => x.ArrivalDate.CompareTo(date) >= 0);
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This method takes the current list of reservations and filters out Reservations whose Departure dates are after the given date
        /// </summary>
        /// <param name="date">The date which we want to compare our arrival dates against</param>
        private void filterByDepartureDate(DateTime date)
        {
            _currentReservations = _currentReservations.FindAll(x => x.DepartureDate.CompareTo(date) <= 0);
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This method takes the current list of reservations and filters out Reservations whose given date does not fall within the Reservations Arrival date and Departure date
        /// </summary>
        /// <param name="date"></param>
        private void filterBySpecificDate(DateTime date)
        {
            _currentReservations = _currentReservations.FindAll(x => x.ArrivalDate.Date.CompareTo(date) <= 0 && x.DepartureDate.Date.CompareTo(date) >= 0);
        }


        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// Updated : 2/13/2019 by Matt LaMarche
        /// This method takes a date range and filters out all Reservations which do not have a date within the given range
        /// </summary>
        /// <param name="startDate">startDate is the start of a date range of which we will check to see if the Reservation falls within</param>
        /// <param name="endDate">endDate is the end of a date range of which we will check to see if the Reservation falls within</param>
        private void filterByDateRange(DateTime startDate, DateTime endDate)
        {
            //First check to see if this is a valid range (start date is before endDate)
            if (startDate.CompareTo(endDate) > 0)
            {
                //Start Date is later than endDate so return before filtering
                return;
            }
            //The goal is to see if the Reservations ArrivalDate through the DepartureDate falls within the range given for startDate and endDate
            //Check if DepartureDate.compareto(start) < 0 (Departure date is before the start of this range so we do not care)
            //Check if ArrivalDate.CompareTo(end) > 0 (Arrival Date is after our range so we do not care)
            //If we were given bad data we need full compares--
            // check if ((ArrivalDate.CompareTo(start) < 0 && DepartureDate.CompareTo(start) < 0) || (ArrivalDate.CompareTo(end) > 0 && DepartureDate.CompareTo(end) > 0))
            //That gives us all the bad data so add !
            _currentReservations = _currentReservations.FindAll(x => !((x.ArrivalDate.CompareTo(startDate) < 0 && x.DepartureDate.CompareTo(startDate) < 0) || (x.ArrivalDate.CompareTo(endDate) > 0 && x.DepartureDate.CompareTo(endDate) > 0)));
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// The function which runs when a reservation is double clicked
        /// </summary>
        private void dgReservations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgReservations.SelectedIndex != -1)
            {
                Reservation selectedReservation = new Reservation();
                try
                {
                    selectedReservation = _reservationManager.RetrieveReservation(((VMBrowseReservation)dgReservations.SelectedItem).ReservationID);
                    var readUpdateReservation = new CreateReservation(selectedReservation, _reservationManager);
                    readUpdateReservation.ShowDialog();
                    refreshAllReservations();
                    populateReservations();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to find that Reservation\n" + ex.Message);
                }

            }
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// The function which runs when the view reservation button is clicked. 
        /// It will launch the CreateReservation window in view mode with the option of updating the 
        /// </summary>
        private void btnViewReservation_Click(object sender, RoutedEventArgs e)
        {
            if (dgReservations.SelectedIndex != -1)
            {
                Reservation selectedReservation = new Reservation();
                try
                {
                    selectedReservation = _reservationManager.RetrieveReservation(((VMBrowseReservation)dgReservations.SelectedItem).ReservationID);
                    var readUpdateReservation = new CreateReservation(selectedReservation, _reservationManager);
                    readUpdateReservation.ShowDialog();
                    refreshAllReservations();
                    populateReservations();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to find that Reservation\n" + ex.Message);
                }

            }
        }



        /*--------------------------- Ending BrowseReservation Code --------------------------------*/

        /*--------------------------- Starting BrowseShops Code #BrowseShops --------------------------------*/
        private void BrowseShopsDoOnStart()
        {
            _shopManager = new ShopManagerMSSQL();

            // Load all active shops and populate the data grid.
            refreshShops();

            // Focus the filter textbox.
            txtSearchName.Focus();
        }



        /// <summary>
        /// Author: James Heim
        /// Created 2019-02-28
        /// 
        /// Retrieve all Shops from the View Model.
        /// </summary>
        private void refreshShops()
        {
            try
            {
                _allShops = (List<VMBrowseShop>)_shopManager.RetrieveAllVMShops();
                _currentShops = _allShops;
                populateDataGrid();
            }
            catch (NullReferenceException)
            {
                // Form hasn't been instantiated yet. Ignore.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException);
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019-02-28
        /// 
        /// Populate the DataGrid with filtered list of shops.
        /// Show active or inactive based on which corresponding
        /// radio button is checked via Lambda expression.
        /// </summary>
        /// <param name="active">Sort by active or inactive.</param>
        private void populateDataGrid()
        {
            dgShops.ItemsSource = _currentShops.Where(s => s.Active == rbtnActive.IsChecked.Value);
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-02-28
        /// 
        /// Filter the Shops by Name and/or Building.
        /// </summary>
        public void ApplyFiltersShops()
        {
            try
            {

                _currentShops = _allShops;

                if (txtSearchName.Text.ToString() != "")
                {
                    _currentShops = _currentShops.FindAll(s => s.Name.ToLower().Contains(txtSearchName.Text.ToString().ToLower()));
                }

                if (txtSearchBuilding.Text.ToString() != "")
                {
                    _currentShops = _currentShops.FindAll(s => s.BuildingID.ToLower().Contains(txtSearchBuilding.Text.ToString().ToLower()));
                }

                populateDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Clear the filters and reset the textboxes.
        public void ClearFiltersShops()
        {
            txtSearchBuilding.Text = "";
            txtSearchName.Text = "";

            _currentShops = _allShops;
            populateDataGrid();
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019-02-28
        /// 
        /// Call the filter method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFilterShops_Click(object sender, RoutedEventArgs e)
        {
            ApplyFiltersShops();
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-02-28
        /// 
        /// Call the filter clear method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearFiltersShops_Click(object sender, RoutedEventArgs e)
        {
            ClearFiltersShops();
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-01
        /// 
        /// Display the Create form.
        /// If the form was saved, refresh the list of Shops and the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var varAddForm = new CreateShop();
            var formResult = varAddForm.ShowDialog();

            if (formResult == true)
            {
                // If the create form was saved,
                // Clear the filters and refresh the grid.
                ClearFiltersShops();
                refreshShops();
            }
        }

        private void BtnView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeactivate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DgShops_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-02-28
        /// 
        /// Refresh the shops when Active Shops are selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtnActive_Checked(object sender, RoutedEventArgs e)
        {
            refreshShops();
        }


        /// <summary>
        /// Author James Heim
        /// Created 2019-02-28
        /// 
        /// Refresh the shops when Inactive Shops are selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtnInactive_Checked(object sender, RoutedEventArgs e)
        {
            refreshShops();
        }
        /*--------------------------- Ending BrowseShops Code --------------------------------*/

        /*--------------------------- Starting BrowseEmployees Code #BrowseEmployees --------------------------------*/
        private void BrowseEmployeesDoOnStart()
        {
            _employeeManager = new EmployeeManager();
            refreshAllEmployees();
            populateEmployees();
        }

        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/02/04
        /// 
        /// A method to create filters for the browse window.
        /// </summary>
        public void ApplyFiltersEmployees()
        {
            try
            {

                // Get a fresh grid.
                repopulateEmployees();

                if (txtSearchFirstName.Text.ToString() != "")
                {
                    _currentEmployees = _currentEmployees.FindAll(s => s.FirstName.ToLower().Contains(txtSearchFirstName.Text.ToString().ToLower()));
                }

                if (txtSearchLastName.Text.ToString() != "")
                {
                    _currentEmployees = _currentEmployees.FindAll(s => s.LastName.ToLower().Contains(txtSearchLastName.Text.ToString().ToLower()));
                }

                if (txtSearchDepartment.Text.ToString() != "")
                {
                    _currentEmployees = _currentEmployees.FindAll(s => s.DepartmentID.ToLower().Contains(txtSearchDepartment.Text.ToString().ToLower()));
                }

                if (rbtnActiveEmployee.IsChecked == true)
                {
                    _currentEmployees = _employeeManager.SelectAllActiveEmployees();
                }

                if (rbtnInactiveEmployee.IsChecked == true)
                {
                    _currentEmployees = _employeeManager.SelectAllInActiveEmployees();
                }

                dgEmployees.ItemsSource = _currentEmployees;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/02/04
        /// 
        /// A method to creat the clear functions for the clear button
        /// </summary>
        public void ClearFiltersEmployees()
        {
            txtSearchFirstName.Clear();
            txtSearchLastName.Clear();
            txtSearchRole.Clear();
            txtSearchDepartment.Clear();

            repopulateEmployees();

            rbtnActiveEmployee.IsChecked = true;
        }

        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/02/04
        /// 
        /// Used to populate the DataGrid.
        /// </summary>
        private void populateEmployees()
        {
            try
            {
                _employees = _employeeManager.SelectAllEmployees();
                if (_currentEmployees == null)
                {
                    _currentEmployees = _employees;
                }
                dgEmployees.ItemsSource = _currentEmployees;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/02/04
        /// 
        /// Used to repopulate the current employees in the datagrid.
        /// </summary>
        private void repopulateEmployees()
        {
            _currentEmployees = _employees;
            dgEmployees.ItemsSource = _currentEmployees;
        }

        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/02/04
        /// 
        /// Used to apply the filters to the browse window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFilterEmployees_Click(object sender, RoutedEventArgs e)
        {
            ApplyFiltersEmployees();


        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/6/2019
        /// 
        /// Retrieves all of the employees in order for the data grid to be refreshed.
        /// </summary>
        private void refreshAllEmployees()
        {
            try
            {
                _employees = _employeeManager.SelectAllEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            _currentEmployees = _employees;
        }


        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/02/04
        /// 
        /// Used to clear the filters in the Browse window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            ClearFiltersEmployees();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// This opens with window to add a new employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var createEmployee = new EmployeeDetail();
            createEmployee.ShowDialog();
            refreshAllEmployees();
            populateEmployees();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// This button opens the window to read the information for the chosen employee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee chosenEmployee = new Employee();

            chosenEmployee = (Employee)dgEmployees.SelectedItem;
            try
            {
                var readUpdateEmployee = new EmployeeDetail(chosenEmployee);
                readUpdateEmployee.ShowDialog();

                refreshAllEmployees();
                populateEmployees();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to find Employee." + ex.Message);
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/13/19
        /// 
        /// The delete button first deactivates and then deletes an employee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                _employeeManager.DeleteEmployee(((Employee)dgEmployees.SelectedItem).EmployeeID, ((Employee)dgEmployees.SelectedItem).Active);
                if (((Employee)dgEmployees.SelectedItem).Active)
                {
                    var result = MessageBox.Show("Are you sure you want to deactivate this employee?", "This employee will no longer be active in the system.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("The employee has been deactivated.");
                    }
                }
                else
                {
                    var result = MessageBox.Show("Are you sure you want to delete this employee?", "This employee will no longer be in the system.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("The employee has been purged.");
                    }
                }
                refreshAllEmployees();
                populateEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't delete this employee" + ex.Message);
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// Opens the window to read the information for the chosen employee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgEmployees_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Employee chosenEmployee = new Employee();

            chosenEmployee = (Employee)dgEmployees.SelectedItem;
            try
            {
                var readUpdateEmployee = new EmployeeDetail(chosenEmployee);
                readUpdateEmployee.ShowDialog();

                refreshAllEmployees();
                populateEmployees();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to find Employee." + ex.Message);
            }
        }
        /*--------------------------- Ending BrowseEmployees Code --------------------------------*/

        /*--------------------------- Starting BrowseSuppliers Code #BrowseSuppliers --------------------------------*/

        private void BrowseSuppliersDoOnStart()
        {
            _supplierManager = new SupplierManager();
            populateSuppliers();
        }


        /// <summary>
        /// Author: James Heim
        /// Created Date: 2019/01/31
        /// 
        /// View the selected record.
        /// </summary>
        public void ViewSelectedRecord()
        {
            var supplier = (Supplier)dgSuppliers.SelectedItem;

            if (supplier != null)
            {
                var viewSupplierForm = new frmSupplier(supplier);

                // Capture the result of the Dialog.
                var result = viewSupplierForm.ShowDialog();

                if (result == true)
                {
                    // If the form was edited, refresh the datagrid.
                    try
                    {
                        _currentSuppliers = null;
                        _suppliers = _supplierManager.RetrieveAllSuppliers();

                        if (_currentSuppliers == null)
                        {
                            _currentSuppliers = _suppliers;
                        }
                        dgSuppliers.ItemsSource = _currentSuppliers;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message); ;
                    }

                }
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// 
        /// Calls the procedure to view the selected record.
        /// </summary>
        /// <remarks>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// Brings up the data grid for the user to view.
        /// 
        /// Modified: James Heim
        /// Modified: 2019/01/31
        /// Repurposed the button to view the details for the selected record
        /// since the datagrid populates on form load.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadSuppliers_Click(object sender, RoutedEventArgs e)
        {
            ViewSelectedRecord();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/25/19
        /// 
        /// This is a helper method that we can use to populate the data grid with
        /// only active Suppliers.
        /// 
        /// <remarks>
        /// Updated by James Heim
        /// Updated 2019/02/21
        /// Now only populates _currentSuppliers with active suppliers.
        /// </remarks>
        /// </summary>
        private void populateSuppliers()
        {
            try
            {
                _suppliers = _supplierManager.RetrieveAllSuppliers();
                if (_currentSuppliers == null)
                {
                    _currentSuppliers = _suppliers.FindAll(s => s.Active == true);
                }
                dgSuppliers.ItemsSource = _currentSuppliers;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// 
        /// Calls the method to filter the datagrid.
        /// </summary>
        /// <remarks>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// The ReadSuppliers button allows for filtering by the company name and city location using lambda expressions.
        /// 
        /// Modified by James Heim
        /// Modified 2019/01/31
        /// Extracted the filter code to a method the button calls.
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFilterSuppliers_Click(object sender, RoutedEventArgs e)
        {
            FilterSuppliers();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// 
        /// The ReadSuppliers button allows for filtering by the company name and city location using lambda expressions.
        /// 
        /// <remarks>
        /// Modified by James Heim
        /// Modified 2019/01/31
        /// Moved this code out of BtnFilter_Click into its own method.
        /// 
        /// </remarks>
        /// </summary>
        public void FilterSuppliers()
        {
            try
            {
                if (txtSearchSupplierName.Text.ToString() != "")
                {
                    _currentSuppliers = _currentSuppliers.FindAll(s => s.Name.ToLower().Contains(txtSearchSupplierName.Text.ToString().ToLower()));
                }

                if (txtSearchSupplierCity.Text.ToString() != "")
                {
                    _currentSuppliers = _currentSuppliers.FindAll(s => s.City.ToLower().Contains(txtSearchSupplierCity.Text.ToString().ToLower()));
                }

                dgSuppliers.ItemsSource = _currentSuppliers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/23/19
        /// 
        /// The Clear button allows the user to clear the filter that they have done so that they can see all of the 
        /// suppliers in the data grid once again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearSuppliers_Click(object sender, RoutedEventArgs e)
        {
            _currentSuppliers = _suppliers;
            dgSuppliers.ItemsSource = _currentSuppliers;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/25/19
        /// 
        /// This method allows us to select which columns we want to show to the user. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgSuppliers_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yy";
            }

            string headerName = e.Column.Header.ToString();

            if (headerName == "ContactFirstName")
            {
                e.Cancel = true;
            }
            if (headerName == "ContactLastName")
            {
                e.Cancel = true;
            }
            if (headerName == "Address")
            {
                e.Cancel = true;
            }
            if (headerName == "Country")
            {
                e.Cancel = true;
            }
            if (headerName == "ZipCode")
            {
                e.Cancel = true;
            }
            if (headerName == "Active")
            {
                e.Cancel = true;
            }
            if (headerName == "DateAdded")
            {
                e.Cancel = true;
            }
            if (headerName == "SupplierEmail")
            {
                e.Cancel = true;
            }
            if (headerName == "State")
            {
                e.Cancel = true;
            }
        }

        private void BtnAddSuppliers_Click(object sender, RoutedEventArgs e)
        {
            var createSupplierForm = new frmSupplier();
            var formResult = createSupplierForm.ShowDialog();

            if (formResult == true)
            {
                // If the create form was saved, refresh the datagrid.
                try
                {
                    _currentSuppliers = null;
                    _suppliers = _supplierManager.RetrieveAllSuppliers();

                    if (_currentSuppliers == null)
                    {
                        _currentSuppliers = _suppliers;
                    }
                    dgSuppliers.ItemsSource = _currentSuppliers;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message); ;
                }
            }
        }

        private void DgSuppliers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewSelectedRecord();
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019/02/21
        /// 
        /// Handle logic for deleting a record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteSuppliers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var supplier = (Supplier)dgSuppliers.SelectedItem;

                // Remove the supplier from the Grid to update faster.
                _currentSuppliers.Remove(supplier);
                dgSuppliers.Items.Refresh();


                // Remove the supplier from the DB.
                _supplierManager.DeleteSupplier(supplier);

                // Refresh the Supplier List.
                _currentSuppliers = null;
                populateSuppliers();
            }
            catch (NullReferenceException)
            {
                // Nothing selected. Do nothing.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException);
            }
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019/02/21
        /// 
        /// Set the Supplier to Inactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeactivateSuppliers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var supplier = (Supplier)dgSuppliers.SelectedItem;

                // Remove the record from the list of Active Suppliers.
                _currentSuppliers.Remove(supplier);
                dgSuppliers.Items.Refresh();

                // Set the record to inactive.
                _supplierManager.DeactivateSupplier(supplier);

                // Refresh the Supplier List.
                _currentSuppliers = null;
                populateSuppliers();
            }
            catch (NullReferenceException)
            {
                // Nothing selected. Do nothing.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException);
            }
        }

        private void RbtnInactiveSupplier_Checked(object sender, RoutedEventArgs e)
        {

        }

        /*--------------------------- Ending BrowseSuppliers Code --------------------------------*/


        /*--------------------------- Starting BrowseProducts Code #BrowseProducts --------------------------------*/
        private void BrowseProductsDoOnStart()
        {
            _productManager = new ProductManagerMSSQL();
            _selectedProduct = new Product();
            populateProducts();
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler for a mouse double click on an item within the data grid.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var product = (Product)dgProducts.SelectedItem;
            var createForm = new CreateProduct(product);
            var productAdded = createForm.ShowDialog();
            refreshProducts();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Method to populate the datagrid.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void populateProducts()
        {
            try
            {
                _allProducts = _productManager.RetrieveAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgProducts.ItemsSource = _allProducts;
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Method to refresh the datagrid information after a change
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void refreshProducts()
        {
            try
            {
                _allProducts = _productManager.RetrieveAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            _currentProducts = _allProducts;
            dgProducts.ItemsSource = _currentProducts;
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user clicking on a add product button. Calls the createProduct window.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var createForm = new CreateProduct();
            createForm.ShowDialog();
            refreshProducts();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user clicking on a button labled read/update. Checks to assure an item is selected.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedIndex != -1)
            {
                _selectedProduct = (Product)dgProducts.SelectedItem;

                var createForm = new CreateProduct(_selectedProduct);
                createForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must have a product selected.");
            }
            refreshProducts();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user clicking on a delete button. Assures that there is an item selected.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = (Product)dgProducts.SelectedItem;
            MessageBoxResult result;
            if (dgProducts.SelectedIndex != -1)
            {
                if (selectedProduct.Active)
                {
                    result = MessageBox.Show("Are you sure you want to deactivate " + selectedProduct.Name, "Deactivating Item", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _productManager.DeactivateProduct(_selectedProduct);
                    }
                }
                if (!selectedProduct.Active)
                {
                    result = MessageBox.Show("Are you sure you want to purge " + selectedProduct.Name, "Purging Item", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _productManager.DeleteProduct(_selectedProduct);
                    }
                }
            }
            else
            {
                MessageBox.Show("You must have a product selected.");
            }
            populateProducts();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user checking a box labled active to view only active products.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbActive_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)cbActive.IsChecked && (bool)cbDeactive.IsChecked)
            {
                populateProducts();
            }
            else if ((bool)cbActive.IsChecked)
            {
                try
                {
                    _currentProducts = _productManager.RetrieveActiveProducts();
                    dgProducts.ItemsSource = _currentProducts;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (!(bool)cbActive.IsChecked)
            {
                populateProducts();
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user checking a box labled deactive to view only deactive *should be inactive* products.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDeactive_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)cbActive.IsChecked && (bool)cbDeactive.IsChecked)
            {
                populateProducts();
            }
            else if ((bool)cbDeactive.IsChecked)
            {
                try
                {
                    _currentProducts = _productManager.RetrieveDeactiveProducts();
                    dgProducts.ItemsSource = _currentProducts;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (!(bool)cbDeactive.IsChecked)
            {
                populateProducts();
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user clicking the search button. Assures the user has entered something to search for.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSearchBox.Text.ToString() != "")
                {
                    _currentProducts = _allProducts.FindAll(b => b.Name.ToLower().Contains(txtSearchBox.Text.ToString().ToLower()));
                    dgProducts.ItemsSource = _currentProducts;
                }
                else
                {
                    MessageBox.Show("You must search for a product");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user clciking the clear button. Clears all filters and checkboxes
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            populateProducts();
            this.txtSearchBox.Text = "";
            this.cbActive.IsChecked = false;
            this.cbDeactive.IsChecked = false;
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with the columns that populate on the datagrid. Can be changed moving forward as many fields have been added to original.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgProducts_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
            }
            string headerName = e.Column.Header.ToString();
            if (headerName == "ProductID")
            {
                e.Cancel = true;
            }
            if (headerName == "Active")
            {
                e.Cancel = true;
            }
            if (headerName == "CustomerPurchasable")
            {
                e.Cancel = true;
            }
            if (headerName == "RecipeID")
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with the serachbox being focused. Highlights the text for easier searching.
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtSearchBox.SelectAll();
        }


        /*--------------------------- Ending BrowseProducts Code --------------------------------*/


        /*--------------------------- Starting BrowseBuilding Code #BrowseBuilding --------------------------------*/
        private void BrowseBuildingDoOnStart()
        {
            buildingManager = new BuildingManager();

            allBuildings = buildingManager.RetrieveAllBuildings();
            dgBuildings.ItemsSource = allBuildings;
        }


        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/31
        /// 
        /// Displays list of buildings in the dgBuildings data grid.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// 
        /// </remarks>
        private void displayBuildings()
        {
            dgBuildings.ItemsSource = allBuildings;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/31
        /// 
        /// User double clicks a line in the dgBuildings data grid.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgBuildings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectBuilding();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSearch.Text.ToString() != "")
                {
                    currentBuildings = allBuildings.FindAll(b => b.Name.ToLower().Contains(txtSearch.Text.ToString().ToLower()));
                    dgBuildings.ItemsSource = currentBuildings;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnClearFiltersBuilding_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            dgBuildings.ItemsSource = allBuildings;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/31
        /// 
        /// Displays an "Add View" BuildingDetail window.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBuilding_Click(object sender, RoutedEventArgs e)
        {
            var addForm = new BuildingDetail();
            var buildingAdded = addForm.ShowDialog();

            if (buildingAdded == true)
            {
                // a building was added, update list
                try
                {
                    allBuildings = buildingManager.RetrieveAllBuildings();
                    dgBuildings.ItemsSource = allBuildings;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                // building was not added 
                MessageBox.Show("Building was not added.");
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            selectBuilding();
        }

        private void selectBuilding()
        {
            Building selectedBuilding = (Building)dgBuildings.SelectedItem;
            var detailForm = new BuildingDetail(selectedBuilding);
            var formUpdated = detailForm.ShowDialog();

            if (formUpdated == true)
            {
                try
                {
                    allBuildings = buildingManager.RetrieveAllBuildings();
                    dgBuildings.ItemsSource = allBuildings;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /*--------------------------- Ending BrowseBuilding Code --------------------------------*/


        /*--------------------------- Starting BrowseOrder Code #BrowseOrder --------------------------------*/
        private void BrowseOrderDoOnStart()
        {
            _searchCategories = new List<string>();
            _userManager = new UserManager();
            _internalOrderManager = new InternalOrderManager();
            _fullUser = new User();
            _orders = new List<VMInternalOrder>();
            dgInternalOrders.Visibility = Visibility.Visible;
            refreshGrid();
            _searchCategories.Add("First Name");
            _searchCategories.Add("Last Name");
            _searchCategories.Add("Department");
            _searchCategories.Add("Description");
            cboSearchCategory.ItemsSource = _searchCategories;

        }


        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Filters out either the orders that are completed or the orders that 
        /// are incomplete.
        /// </summary>
        private void ChkbxOrderCompleted_Click(object sender, RoutedEventArgs e)
        {
            if (chkbxOrderCompleted.IsChecked == true)
            {
                _currentOrders = _orders.FindAll(o => o.OrderComplete == true);
                applyFilters();

            }
            else
            {
                _currentOrders = _orders.FindAll(o => o.OrderComplete == false);
                applyFilters();
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Attempts to fill the data grid with the information in
        /// the database.
        /// </summary>
        private void refreshGrid()
        {
            try
            {
                _orders = _internalOrderManager.RetrieveAllInternalOrders();
                dgInternalOrders.ItemsSource = _orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve orders from database: \n" + ex.Message);
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Takes the order information from the grid(if applicable)
        /// and opens a new Detail Window for viewing the order information.
        /// </summary>
        private void DgInternalOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgInternalOrders.SelectedItem != null)
            {
                var order = (VMInternalOrder)dgInternalOrders.SelectedItem;
                var viewOrderDetail = new TestWindow(order);
                viewOrderDetail.ShowDialog();
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Takes the order information from the grid(if applicable)
        /// and opens a new Detail Window for viewing the order information.
        /// </summary>
        private void BtnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (dgInternalOrders.SelectedItem != null)
            {
                var order = (VMInternalOrder)dgInternalOrders.SelectedItem;
                var viewOrderDetail = new TestWindow(order);
                viewOrderDetail.ShowDialog();
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Takes information about the order from the grid and
        /// attempts to update the status of the order to complete,
        /// then refreshes the grid
        /// </summary>
        private void BtnFillOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgInternalOrders.SelectedItem != null)
            {

                var order = (VMInternalOrder)dgInternalOrders.SelectedItem;
                if (order.OrderComplete != true)
                {
                    try
                    {
                        if (_internalOrderManager.UpdateOrderStatusToComplete(order.InternalOrderID, order.OrderComplete))
                        {
                            MessageBox.Show("Order status successfully updated");
                            refreshGrid();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update order status");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: failed to update order status: \n" + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Actively changes the filter selected as the 
        /// text changes in the search bar
        /// </summary>
        private void TxtSearchTerm_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cboSearchCategory.SelectedIndex != -1)
            {
                switch (cboSearchCategory.SelectedIndex)
                {
                    case 0:
                        _currentOrders = _orders.FindAll(o => o.FirstName.ToLower()
                        .Contains(txtSearchTerm.Text.ToLower()));
                        applyFilters();
                        break;
                    case 1:
                        _currentOrders = _orders.FindAll(o => o.LastName.ToLower()
                        .Contains(txtSearchTerm.Text.ToLower()));
                        applyFilters();
                        break;
                    case 2:
                        _currentOrders = _orders.FindAll(o => o.DepartmentID.ToLower()
                        .Contains(txtSearchTerm.Text.ToLower()));
                        applyFilters();
                        break;
                    case 3:
                        _currentOrders = _orders.FindAll(o => o.Description.ToLower()
                        .Contains(txtSearchTerm.Text.ToLower()));
                        applyFilters();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Applies the current filters to the data grid and 
        /// updates the grid view to reflect them.
        /// </summary>
        private void applyFilters()
        {
            dgInternalOrders.ItemsSource = _currentOrders;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Removes all current filters from use and 
        /// refreshes the grid to its original state.
        /// </summary>
        private void BtnClearFiltersOrders_Click(object sender, RoutedEventArgs e)
        {
            cboSearchCategory.SelectedIndex = -1;
            txtSearchTerm.Text = "";
            dgInternalOrders.ItemsSource = _orders;
        }

        private void BtnAddNewOrder_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                _fullUser = _userManager.RetrieveFullUserByEmail(_fullUser.Email);
                var addOrder = new TestWindow(_fullUser);
                var result = addOrder.ShowDialog();
                if (result == true)
                {
                    refreshGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Retrieve user to Add Orders \n" + ex.Message);
            }
        }

        /*--------------------------- Ending BrowseOrder Code --------------------------------*/

        /*--------------------------- Starting BrowseEmployeeRole Code #BrowseEmployeeRole --------------------------------*/
        private void BrowseEmployeeRolesDoOnStart()
        {
            _selectedRole = new Role();
            _roleManager = new RoleManager();
            refreshRoles();
        }




        private void TabRole_GotFocus(object sender, RoutedEventArgs e)
        {

            //dgRole.Items.Refresh();


        }

        private void DgRole_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //   var role = (Role)dgRole.SelectedItem;
            //  var detailForm = new UpdateEmployeeRole(role); 

        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// method to open the create employee roles.
        /// </summary>
        private void BtnAddEmployeeRole_Click(object sender, RoutedEventArgs e)
        {


            var detailForm = new CreateEmployeeRole();

            var result = detailForm.ShowDialog();// need to be added



            if (result == true)
            {

                MessageBox.Show(result.ToString());
            }
            refreshRoles();

        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// method to refresh employee roles list.
        /// </summary>
        private void refreshRoles()
        {
            try
            {
                _roles = _roleManager.RetrieveAllRoles();

                _currentRoles = _roles;
                //txtSearch.Text = "";
                dgRole.ItemsSource = _currentRoles;
                filterRoles();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to call the filter method
        /// </summary>

        private void BtnFilterEmployeeRole_Click(object sender, RoutedEventArgs e)
        {
            filterRoles();
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to filter the  view employee roles
        /// </summary>
        private void filterRoles()
        {

            IEnumerable<Role> currentRoles = _roles;
            try
            {
                List<Role> _currentRoles = new List<Role>();

                if (txtSearch.Text.ToString() != "")
                {

                    if (txtSearch.Text != "" && txtSearch.Text != null)
                    {
                        currentRoles = currentRoles.Where(b => b.Description.ToLower().Contains(txtSearch.Text.ToLower()));

                    }
                }
                /*
                if (cbActive.IsChecked == true && cbDeactive.IsChecked == false)
                {
                    currentRoles = currentRoles.Where(b => b.Active == true);
                }
                else if (cbActive.IsChecked == false && cbDeactive.IsChecked == true)
                {
                    currentRoles = currentRoles.Where(b => b.Active == false);
                }
                else if (cbActive.IsChecked == false && cbDeactive.IsChecked == false)
                {
                    currentRoles = currentRoles.Where(b => b.Active == false && b.Active == true);
                }
                */
                dgRole.ItemsSource = null;

                dgRole.ItemsSource = currentRoles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to clear the filters
        /// </summary>
        private void BtnClearRoles_Click(object sender, RoutedEventArgs e)
        {

            txtSearch.Text = "";
            _currentRoles = _roles;
            //    cbDeactive.IsChecked = true;
            //     cbActive.IsChecked = true;

            dgRole.ItemsSource = _currentRoles;
        }





        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to update an employee role
        /// </summary>
        private void BtnUpdateEmployeeRole_Click(object sender, RoutedEventArgs e)
        {

            if (dgRole.SelectedItem != null)
            {
                _selectedRole = (Role)dgRole.SelectedItem;
                var assign = new CreateEmployeeRole(_selectedRole);
                assign.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must select an item first");
            }
            refreshRoles();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to cancel and exit a window
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to quit?", "Closing Application", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to Deactivate an employee role
        /// </summary>
        private void BtnDeactivateEmployeeRole_Click(object sender, RoutedEventArgs e)
        {
            if (dgRole.SelectedItem != null)
            {
                Role current = (Role)dgRole.SelectedItem;

                try
                {
                    var result = MessageBox.Show("Are you sure that you want to delete this role?", "Delete Role", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        //  _roleManager.DeleteRole(current.RoleID, current.Active);

                        _roleManager.DeleteRole(current.RoleID);

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
            else
            {

                MessageBox.Show("You must select an item first");

            }
            refreshRoles();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/25
        /// 
        /// method to filter deactive
        /// </summary>
        private void CbDeactive_Click(object sender, RoutedEventArgs e)
        {
            filterRoles();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/25
        /// 
        /// method to filter active
        /// </summary>
        private void CbActive_Click(object sender, RoutedEventArgs e)
        {
            filterRoles();
        }

        /*--------------------------- Ending BrowseEmployeeRole Code --------------------------------*/


        /*--------------------------- Starting BrowseGuestTypes Code #BrowseGuestTypes --------------------------------*/
        //GuestTypes
        private void BrowseGuestTypesDoOnStart()
        {
            guestManager = new GuestTypeManager();
            try
            {
                _guests = guestManager.RetrieveAllGuestTypes("All");
                if (_currentGuests == null)
                {
                    _currentGuests = _guests;
                }
                dgGuests.ItemsSource = _currentGuests;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Opens up the add window and updates the datagrid if guest type was created successfully
        /// </summary>
        private void btnAddGuestType_Click(object sender, RoutedEventArgs e)
        {
            var addGuest = new AddGuestType();
            var result = addGuest.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentGuests = null;
                    _guests = guestManager.RetrieveAllGuestTypes("All");
                    if (_currentGuests == null)
                    {
                        _currentGuests = _guests;
                    }
                    dgGuests.ItemsSource = _currentGuests;
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
        private void btnDeleteGuestType_Click(object sender, RoutedEventArgs e)
        {
            var deleteGuestType = new DeleteGuestType();
            var result = deleteGuestType.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentGuests = null;
                    _guests = guestManager.RetrieveAllGuestTypes("All");
                    if (_currentGuests == null)
                    {
                        _currentGuests = _guests;
                    }
                    dgGuests.ItemsSource = _currentGuests;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /*--------------------------- Ending BrowseGuestTypes Code --------------------------------*/


        /*--------------------------- Starting BrowseRoomTypes Code #BrowseRoomTypes --------------------------------*/
        //RoomTypes
        private void BrowseRoomTypesDoOnStart()
        {
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
        private void btnAddRoomType_Click(object sender, RoutedEventArgs e)
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
        private void btnDeleteRoomType_Click(object sender, RoutedEventArgs e)
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


        /*--------------------------- Ending BrowseRoomTypes Code --------------------------------*/


        /*--------------------------- Starting BrowsePerformance Code #BrowsePerformance --------------------------------*/
        private void BrowsePerformanceDoOnStart()
        {
            setupWindowPerformance();
            performanceManager = new PerformanceManager();
        }

        private void dgPerformaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                openView(int.Parse(((TextBlock)RowColumn.Content).Text));
            }
            catch (Exception)
            {

            }
            dgPerformaces.SelectedItem = null;
        }

        private void openView(int performanceID)
        {
            var frmView = new ViewPerformance(performanceID, performanceManager);
            if (frmView.ShowDialog() == true)
            {
                MessageBox.Show("Performance Updated.");
                setupWindowPerformance();
            }
            return;
        }

        private void btnAddPerformance_Click(object sender, RoutedEventArgs e)
        {
            var frmAdd = new AddPerformance(performanceManager);
            if (frmAdd.ShowDialog() == true)
            {
                MessageBox.Show("Performance Added.");
                setupWindowPerformance();
            }
            return;
        }

        private void txtSearchPerformance_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgPerformaces.ItemsSource = performanceManager.SearchPerformances(txtSearch.Text);
        }

        private void setupWindowPerformance()
        {
            dgPerformaces.ItemsSource = performanceManager.RetrieveAllPerformance();
        }

        /*--------------------------- Ending BrowsePerformance Code --------------------------------*/


        /*--------------------------- Starting BrowseItemSuppliers Code #BrowseItemSuppliers --------------------------------*/
        //frmManageItemSuppliers
        private void BrowseItemSuppliersDoOnStart()
        {
            _itemSupplierManager = new ItemSupplierManager();
        }






        /*--------------------------- Ending BrowseItemSuppliers Code --------------------------------*/


        /*--------------------------- Starting BrowseRoom Code #BrowseRoom --------------------------------*/
        //frmRoomManagement
        /*--------------------------- Ending BrowseRoom Code --------------------------------*/


    }
}
