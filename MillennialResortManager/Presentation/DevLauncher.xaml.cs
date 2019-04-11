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
using WpfPresentation;
using EventManager = LogicLayer.EventManager;

namespace Presentation
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 2/27/2019
    /// This is a launcher for Developers to use while we develop functionality for our program
    /// 
    /// To quickly find the section of code you are looking for Ctrl + F and look for one of these Keys:
    /// #NavBar
    /// #BrowseReservation
    /// #BrowseShops
    /// #BrowseEmployees
    /// #BrowseSuppliers
    /// #BrowseItems
    /// #BrowseBuilding
    /// #BrowseOrder
    /// #BrowseEmployeeRole
    /// #BrowseItemSuppliers
    /// #BrowseRoom
    /// #BrowseGuestTypes
    /// #BrowseRoomTypes
    /// 
    /// #BrowsePerformance
    /// #BrowseEventTypes
    /// #BrowseAppointment
    /// #BrowseGuest
    /// #BrowseGuestVehicle
    /// #BrowseSetupList
    /// #BrowseSponsor
    /// #BrowseRecipe
    /// #BrowseEvent
    /// #BrowseEventSponsor
    /// #BrowseEventPerformance
    /// #BrowseSupplierOrders
    /// #BrowsePets
    /// #BrowseRoom
    /// #BrowseMaintenanceType
    /// #BrowseMember
    /// #Receiving
    /// #BrowseOffering
    /// #Profile
    /// #FrontDesk
    /// 
    /// </summary>
    public partial class DevLauncher : Window
    {
        #region Variables Code #Variables
        //This is the employee who is logged in to our system
        private Employee _employee;
        //Reservation
        private List<VMBrowseReservation> _allReservations;
        private List<VMBrowseReservation> _currentReservations;
        private ReservationManagerMSSQL _reservationManager;
        //Shops
        private List<VMBrowseShop> _allShops;
        private List<VMBrowseShop> _currentShops;
        private ShopManagerMSSQL _shopManager;
        //Employee
        private EmployeeManager _employeeManager;
        private List<Employee> _employees;
        private List<Employee> _currentEmployees;
        //Suppliers
        private List<Supplier> _suppliers;
        private List<Supplier> _currentSuppliers;
        private SupplierManager _supplierManager;
        //Items
        private List<Item> _allItems;
        private List<Item> _currentItems;
        private ItemManager _itemManager;
        private Item _selectedItem;
        //Buildings
        private List<Building> allBuildings;
        private List<Building> currentBuildings; // needed?
        private IBuildingManager buildingManager;
        //Orders
        private List<string> _searchCategories;
        //private UserManager _userManager;
        private InternalOrderManager _internalOrderManager;
       // private User _fullUser;
        private List<VMInternalOrder> _orders;
        private List<VMInternalOrder> _currentOrders;
        //Employee Roles
        private IRoleManager _roleManager;
        private List<Role> _roles;
        private List<Role> _currentRoles;
        private Role _selectedRole;
        //Guest Types
        private List<GuestType> _guests;
        private List<GuestType> _currentGuests;
        private IGuestTypeManager guestManager;
        //Room types
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
        //EventType
        private List<EventType> _eventType;
        private List<EventType> _currentEventType;
        private IEventTypeManager _eventTypeManager;
        //Appointment Types
        private List<AppointmentType> _appointmentType;
        private List<AppointmentType> _currentAppointmentType;
        private IAppointmentTypeManager _appointmentTypeManager;
        //Guests
        private List<Guest> _guestsBrowseGuests;
        private List<Guest> _guestsSearched;
        private GuestManager _guestManager;
        //GuestVehicles
        private List<VMGuestVehicle> _vehicles;
        private GuestVehicleManager _guestVehicleManager;
        private List<string> _searchOptions;
        private List<VMGuestVehicle> _currentListGuestVehicle;
        //Setup
        SetupManager _setupManager;
        List<VMSetup> _setups;
        List<VMSetup> _currentSetups;
        //Sponsor
        private List<Sponsor> _allSponsors;
        private List<Sponsor> _currentSponsors;
        private SponsorManager _sponsorManager;
        //Recipe
        private List<string> roles;
        private List<Recipe> _recipes;
        private RecipeManager _recipeManager;
        private bool _isFilterRestting;
        //EventSponsor
        private EventSponsorManager _eventSponsManager;
        private List<EventSponsor> _eventSponsors;
        //EventPerformance
        private EventPerformanceManager _eventPerfManager;
        private List<EventPerformance> _eventPerformances;
        //Event
        private EventManager _eventManager;
        //private EventTypeManager _eventTypeManager = new EventTypeManager();  Already in use 
        private List<Event> _events;
        private Event _selectedEvent;
        //Pets
        //private Pet _pet;
        private PetManager _petManager;
        private List<Pet> _pets;
        private PetTypeManager petTypeManager;
        //Rooms
        private RoomManager _roomManager;
        private List<string> _buidlingIDList;
        private List<string> _roomTypesIDList;
        private List<Room> _roomList;
        private List<Room> _currentRooms;
        //MaintenanceTypes
        private List<MaintenanceType> type;
        private List<MaintenanceType> currentType;
        private IMaintenanceTypeManager maintenanceManager;
        //Member
        private List<Member> _members;
        private List<Member> _currentMembers;
        private MemberManagerMSSQL _memberManager;
        private Member _selectedMember;
        //Supplier Orders
        private SupplierOrderManager _supplierOrderManager = new SupplierOrderManager();
        //private SupplierManager _supplierManager = new SupplierManager(); Already in use
        private SupplierOrder _supplierOrder;
        //private List<Supplier> _suppliers; Already in use
        private List<SupplierOrder> _supplierOrders;
        private List<SupplierOrder> _currentSupplierOrders;
        //Profile
        private List<Department> _departments;
        // EmployeeManager _employeeManager;
        DepartmentManager _departmentManager;
        private Employee _newEmployee;
        //Maintenance Work Order
        private List<MaintenanceWorkOrder> _allMaintenanceWorkOrders;
        private List<MaintenanceWorkOrder> _currentMaintenanceWorkOrders;
        private MaintenanceWorkOrderManagerMSSQL _maintenanceWorkOrderManager;
        //Order receiving 
        ReceivingTicketManager _receivingManager = new ReceivingTicketManager();
        private SupplierOrderManager _receivingSupplierManager = new SupplierOrderManager();
        //FrontDesk
        private LuggageManager luggageManager;
        //private GuestManager guestManager = new GuestManager();
        private List<HouseKeepingRequest> _allHouseKeepingRequests;
        private List<HouseKeepingRequest> _currentHouseKeepingRequests;
        private HouseKeepingRequestManagerMSSQL _houseKeepingRequestManager;
        //ShuttleReservation
        private IShuttleReservationManager _shuttleReservationManager;
        private List<ShuttleReservation> _shuttleReservations;
        private List<ShuttleReservation> _currentShuttleReservations;
        private ShuttleReservation _shuttleReservation;

        //Offering
        private List<OfferingVM> _offeringVms;
        private List<OfferingVM> _currentOfferingVms;
        private OfferingManager _offeringManager;

        #endregion

        #region DevLauncher Code #DevLauncher

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

            //For Navbar
            HideNavBarOptions();
        }



        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/11/2019
        /// Hides all the navbar options the viewer does not have permission to see
        /// </summary>
        private void HideNavBarOptions()
        {
            /*
             * Admin,
        Maintenance,
        Events,
        ResortOperations,
        Pet,
        FoodService,
        Ordering,
        NewEmployee
             * */
            if (_employee.DepartmentID == "Admin")
            {
                //This person can see everything
            }
            else if (_employee.DepartmentID == "Maintenance")
            {
                HideNavbarOption();
                if (_employee.EmployeeRoles.Count(x => x.RoleID == "Manager") > 0)
                {
                    //This employee is not an Admin so hide all admin only things within Maintenance Pages

                }
                else if (_employee.EmployeeRoles.Count(x => x.RoleID == "Worker") > 0)
                {
                    //This employee is not an Admin or a Manager so hide all admin/manager things within Maintenance Pages
                    NavBarSubHeaderMaintenanceTypes.Visibility = Visibility.Collapsed;
                }
                else
                {
                    //This person has no assigned roles or his roles are messed up.
                    NavBarSubHeaderMaintenanceTypes.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderMaintenanceWorkOrders.Visibility = Visibility.Collapsed;
                }
            }
            else if (_employee.DepartmentID == "Events")
            {
                HideNavbarOption();
                if (_employee.EmployeeRoles.Count(x => x.RoleID == "Manager") > 0)
                {
                    //This employee is not an Admin so hide all admin only things within Events Pages

                }
                else if (_employee.EmployeeRoles.Count(x => x.RoleID == "Worker") > 0)
                {
                    //This employee is not an Admin or a Manager so hide all admin/manager things within Events Pages
                    NavBarSubHeaderEventTypes.Visibility = Visibility.Collapsed;
                }
                else
                {
                    //This person has no assigned roles or his roles are messed up so be safe and hide everything.
                    NavBarSubHeaderEventTypes.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderPerformances.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderSetupLists.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderEvents.Visibility = Visibility.Collapsed;
                }
            }
            else if (_employee.DepartmentID == "ResortOperations")
            {
                HideNavbarOption();
                if (_employee.EmployeeRoles.Count(x => x.RoleID == "Manager") > 0)
                {
                    //This employee is not an Admin so hide all admin only things within Resort Operations Pages

                }
                else if (_employee.EmployeeRoles.Count(x => x.RoleID == "Worker") > 0)
                {
                    //This employee is not an Admin or a Manager so hide all admin/manager things within Resort Operations Pages
                    NavBarSubHeaderAppointmentTypes.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderGuests.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderGuestTypes.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderPets.Visibility = Visibility.Collapsed;
                }
                else
                {
                    //This person has no assigned roles or his roles are messed up.
                    NavBarSubHeaderAppointmentTypes.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderGuests.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderGuestVehicles.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderPets.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderReservation.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderRooms.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderMembers.Visibility = Visibility.Collapsed;
                }
            }
            else if (_employee.DepartmentID == "FoodService")
            {
                HideNavbarOption();
                if (_employee.EmployeeRoles.Count(x => x.RoleID == "Manager") > 0)
                {
                    //This employee is not an Admin so hide all admin only things within Food Service Pages

                }
                else if (_employee.EmployeeRoles.Count(x => x.RoleID == "Worker") > 0)
                {
                    //This employee is not an Admin or a Manager so hide all admin/manager things within Food Service Pages
                }
                else
                {
                    //This person has no assigned roles or his roles are messed up.
                    NavBarSubHeaderRecipes.Visibility = Visibility.Collapsed;
                }
            }
            else if (_employee.DepartmentID == "Ordering")
            {
                HideNavbarOption();
                if (_employee.EmployeeRoles.Count(x => x.RoleID == "Manager") > 0)
                {
                    //This employee is not an Admin so hide all admin only things within Ordering Pages

                }
                else if (_employee.EmployeeRoles.Count(x => x.RoleID == "Worker") > 0)
                {
                    //This employee is not an Admin or a Manager so hide all admin/manager things within Ordering Pages
                    NavBarSubHeaderProducts.Visibility = Visibility.Collapsed;
                }
                else
                {
                    //This person has no assigned roles or his roles are messed up.
                    NavBarSubHeaderProducts.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderSuppliers.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderSupplierOrders.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderOrders.Visibility = Visibility.Collapsed;
                    NavBarSubHeaderSponsors.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                //No department assigned or department has not been added above
                HideNavbarOption();
            }

        }

        private void HideNavbarOption()
        {
            foreach (MenuItem mi in NavbarMenu.Items)
            {
                if (!(mi.Name.Contains("_") && mi.Name.Contains(_employee.DepartmentID)))
                {
                    mi.Visibility = Visibility.Collapsed;
                }
            }
            _NavBarHeaderProfile.Visibility = Visibility.Visible;
            NavBarSubHeaderEmployee.Visibility = Visibility.Collapsed; //This is admin only test
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/11/2019
        /// Hides sidebar items based on role. Department will come soon as well
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        private bool CanSeeOption(string uid)
        {
            //Check for admin
            if (_employee.EmployeeRoles.Count(x => x.RoleID.Equals("Admin")) > 0)
            {
                return true;
            }

            //For each department show buttons that department can see
            //In development
            //for each role check if there is a role that matches the uid
            foreach (var role in _employee.EmployeeRoles)
            {
                if (uid.ToLower().Contains(role.RoleID.ToLower()))
                {
                    return true;
                }
            }

            return false;
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


        #endregion

        #region Navbar Code
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
            BrowseReservationDoOnStart();
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
            BrowseShopsDoOnStart();
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
            BrowseEmployeesDoOnStart();
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
            BrowseSuppliersDoOnStart();
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
            BrowseItemsDoOnStart();
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
            BrowseBuildingDoOnStart();
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
            BrowseOrderDoOnStart();
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
            BrowseEmployeeRolesDoOnStart();
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
            BrowseGuestTypesDoOnStart();
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
            BrowseRoomTypesDoOnStart();
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
            BrowsePerformanceDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Event Types is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderEventTypes_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseEventTypes");
            BrowseEventTypesDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Appointment Types is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderAppointmentTypes_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseAppointmentType");
            BrowseAppointmentTypeDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Guests is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderGuests_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseGuests");
            BrowseGuestDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Guest Vehicles is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderGuestVehicles_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseGuestVehicle");
            BrowseGuestVehicleDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Setup Lists is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderSetupLists_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseSetup");
            BrowseSetupListDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Sponsors is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderSponsors_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseSponsor");
            BrowseSponsorDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Recipes is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderRecipes_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseRecipe");
            BrowseRecipeDoOnStart();
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// @Created: 4/3/2019
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderEventSponsList_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseEventSponsorsList");
            BrowseEventSponsorsListDoOnStart();
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// Created 4/10/2019
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderEventPerfList_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseEventPerformancesList");
            BrowseEventPerformancesListDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Events is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderEvents_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseEvents");
            BrowseEventDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Supplier Orders is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderSupplierOrders_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseSupplierOrders");
            BrowseSupplierOrdersDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Pets is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderPets_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowsePets");
            BrowsePetsDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Rooms is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderRooms_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseRooms");
            BrowseRoomsDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Maintenance Types is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderMaintenanceTypes_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseMaintenanceTypes");
            BrowseMaintenanceTypeDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// This is what happens when the subheader button for Members is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderMembers_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseMembers");
            BrowseMemberDoOnStart();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/23/2019
        /// This is what happens when the subheader button for Profile is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderProfile_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("Profile");
            ProfileDoOnStart();
        }

        /// <summary>
        /// This is what happens when the subheader button for Maintenance Work Orders is clicked from the navbar
        /// </summary>
        /// <param name=""></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderMaintenanceWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("MaintenanceWorkOrder");
            BrowseMaintenanceWorkOrderDoOnStart();
        }
        /// <summary>
        /// This is what happens when the subheader button for Maintenance Work Orders is clicked from the navbar
        /// </summary>
        /// <param name=""></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderReceiving_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("Receiving");
            BrowseReceivingDoOnStart();
        }
        /**
        * Created By Francis Mingomba
        * Date: 3/16/2019
        */
        private void NavBarSubHeaderManageShuttleVehicles_OnClick(object sender, RoutedEventArgs e)
        {

            DisplayPage("BrowseShuttleVehiclesPage");

            foreach (UserControl item in this.BrowseShuttleVehiclesPage.Children)
            {
                if (item.GetType() != typeof(FrmBrowseShuttleVehicles)) continue;

                FrmBrowseShuttleVehicles instance = (FrmBrowseShuttleVehicles)item;
                instance.setupForm(_employee);
            }
        }

        private void NavBarSubHeaderFrontDesk_OnClick(object sender, RoutedEventArgs e)
        {
            DisplayPage("FrontDesk");
            frontDeskDoOnStart();
        }


        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 03/28/2019
        /// This is what happens when the subheader button for Offerings is clicked from the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarSubHeaderOfferings_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseOfferings");
            BrowseOfferingDoOnStart();
        }
        private void NavBarSubHeaderShuttleReservation_OnClick(object sender, RoutedEventArgs e)
        {
            DisplayPage("ShuttleReservation");
            BrowseShuttleReservationDoOnStart();
        }

        /*--------------------------- Ending NavBar Code --------------------------------*/
        #endregion

        #region Reservation Code
        /*--------------------------- Starting BrowseReservation Code #BrowseReservation --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
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
        #endregion

        #region Shops Code
        /*--------------------------- Starting BrowseShops Code #BrowseShops --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
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
        private void BtnAddShops_Click(object sender, RoutedEventArgs e)
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

        private void BtnViewShop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeactivateShop_Click(object sender, RoutedEventArgs e)
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
        #endregion

        #region Employees Code
        /*--------------------------- Starting BrowseEmployees Code #BrowseEmployees --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
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
        #endregion

        #region Suppliers Code
        /*--------------------------- Starting BrowseSuppliers Code #BrowseSuppliers --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
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
        #endregion

        #region Items Code
        /*--------------------------- Starting BrowseItems Code #BrowseItems --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseItemsDoOnStart()
        {
            _itemManager = new ItemManager();
            _selectedItem = new Item();
            populateItems();
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
            if (dgProducts.SelectedItem != null)
            {
                var item = (Item)dgProducts.SelectedItem;
                var createForm = new CreateItem(item, _employee);
                var productAdded = createForm.ShowDialog();
                refreshItems();
            }
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
        private void populateItems()
        {
            try
            {
                _allItems = _itemManager.RetrieveAllItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgProducts.ItemsSource = _allItems;
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
        private void refreshItems()
        {
            try
            {
                _allItems = _itemManager.RetrieveAllItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            _currentItems = _allItems;
            dgProducts.ItemsSource = _currentItems;
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user clicking on a add item button. Calls the createItem window.
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Converted to Items from Products
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var createForm = new CreateItem();
            createForm.ShowDialog();
            refreshItems();
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
                _selectedItem = (Item)dgProducts.SelectedItem;

                var createForm = new CreateItem(_selectedItem, _employee);
                createForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must have a item selected.");
            }
            refreshItems();
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
            Item selectedItem = (Item)dgProducts.SelectedItem;
            MessageBoxResult result;
            if (dgProducts.SelectedIndex != -1)
            {
                if (selectedItem.Active)
                {
                    result = MessageBox.Show("Are you sure you want to deactivate " + selectedItem.Name, "Deactivating Item", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _itemManager.DeactivateItem(selectedItem);
                    }
                }
                if (!selectedItem.Active)
                {
                    result = MessageBox.Show("Are you sure you want to purge " + selectedItem.Name, "Purging Item", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        _itemManager.DeleteItem(selectedItem);
                    }
                }
            }
            else
            {
                MessageBox.Show("You must have a Item selected.");
            }
            populateItems();
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user checking a box labled active to view only active items.
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
                populateItems();
            }
            else if ((bool)cbActive.IsChecked)
            {
                try
                {
                    _currentItems = _itemManager.RetrieveActiveItems();
                    dgProducts.ItemsSource = _currentItems;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (!(bool)cbActive.IsChecked)
            {
                populateItems();
            }
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// </summary>
        /// Handler to deal with a user checking a box labled deactive to view only deactive *should be inactive* items.
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
                populateItems();
            }
            else if ((bool)cbDeactive.IsChecked)
            {
                try
                {
                    _currentItems = _itemManager.RetrieveDeactiveItems();
                    dgProducts.ItemsSource = _currentItems;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (!(bool)cbDeactive.IsChecked)
            {
                populateItems();
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
                    _currentItems = _allItems.FindAll(b => b.Name.ToLower().Contains(txtSearchBox.Text.ToString().ToLower()));
                    dgProducts.ItemsSource = _currentItems;
                }
                else
                {
                    MessageBox.Show("You must search for an item.");
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
            populateItems();
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
            if (headerName == "ItemID")
            {
                e.Cancel = true;
            }
            if (headerName == "OfferingID")
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


        /*--------------------------- Ending BrowseItems Code --------------------------------*/
        #endregion

        #region Building Code
        /*--------------------------- Starting BrowseBuilding Code #BrowseBuilding --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseBuildingDoOnStart()
        {
            buildingManager = new BuildingManager();
            try{
                allBuildings = buildingManager.RetrieveAllBuildings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
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
            try
            {
                dgBuildings.ItemsSource = allBuildings;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
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

        private void btnFilterBuilding_Click(object sender, RoutedEventArgs e)
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
        #endregion

        #region Order Code
        /*--------------------------- Starting BrowseOrder Code #BrowseOrder --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseOrderDoOnStart()
        {
            _searchCategories = new List<string>();
            //_userManager = new UserManager();
            _internalOrderManager = new InternalOrderManager();
            //_fullUser = new User();
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
                //var viewOrderDetail = new TestWindow(order);
                //viewOrderDetail.ShowDialog();
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
                //var viewOrderDetail = new TestWindow(order);
                //viewOrderDetail.ShowDialog();
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
                //_fullUser = _userManager.RetrieveFullUserByEmail(_fullUser.Email);
                //var addOrder = new TestWindow(_fullUser);
                //var result = addOrder.ShowDialog();
                //if (result == true)
                //{
                //    refreshGrid();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Retrieve user to Add Orders \n" + ex.Message);
            }
        }

        /*--------------------------- Ending BrowseOrder Code --------------------------------*/
        #endregion

        #region Employee Role Code
        /*--------------------------- Starting BrowseEmployeeRole Code #BrowseEmployeeRole --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseEmployeeRolesDoOnStart()
        {
            _selectedRole = new Role();
            _roleManager = new RoleManager();
            refreshRolesEmployeeRole();
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
            refreshRolesEmployeeRole();

        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// method to refresh employee roles list.
        /// </summary>
        private void refreshRolesEmployeeRole()
        {
            try
            {
                _roles = _roleManager.RetrieveAllRoles();

                _currentRoles = _roles;
                //txtSearch.Text = "";
                dgRole.ItemsSource = _currentRoles;
                filterRolesEmployeeRole();

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
            filterRolesEmployeeRole();
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to filter the  view employee roles
        /// </summary>
        private void filterRolesEmployeeRole()
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
            refreshRolesEmployeeRole();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// //method to cancel and exit a window
        /// </summary>
        private void BtnCancelEmployeeRole_Click(object sender, RoutedEventArgs e)
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
            refreshRolesEmployeeRole();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/25
        /// 
        /// method to filter deactive
        /// </summary>
        private void CbDeactive_Click(object sender, RoutedEventArgs e)
        {
            filterRolesEmployeeRole();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/25
        /// 
        /// method to filter active
        /// </summary>
        private void CbActive_Click(object sender, RoutedEventArgs e)
        {
            filterRolesEmployeeRole();
        }

        /*--------------------------- Ending BrowseEmployeeRole Code --------------------------------*/
        #endregion

        #region Guest Types Code
        /*--------------------------- Starting BrowseGuestTypes Code #BrowseGuestTypes --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
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

        #endregion

        #region Room Types Code
        /*--------------------------- Starting BrowseRoomTypes Code #BrowseRoomTypes --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
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
        #endregion

        #region Performance Code
        /*--------------------------- Starting BrowsePerformance Code #BrowsePerformance --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
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
            try
            {
                dgPerformaces.ItemsSource = performanceManager.SearchPerformances(txtSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void setupWindowPerformance()
        {
            try
            {
                dgPerformaces.ItemsSource = performanceManager.RetrieveAllPerformance();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /*--------------------------- Ending BrowsePerformance Code --------------------------------*/
        #endregion

        #region Item Suppliers Code
        /*--------------------------- Starting BrowseItemSuppliers Code #BrowseItemSuppliers --------------------------------*/
        //frmManageItemSuppliers Has required parameters.
        private void BrowseItemSuppliersDoOnStart()
        {
            _itemSupplierManager = new ItemSupplierManager();
        }






        /*--------------------------- Ending BrowseItemSuppliers Code --------------------------------*/
        #endregion

        #region Event Types Code
        /*--------------------------- Starting BrowseEventTypes Code #BrowseEventTypes --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseEventTypesDoOnStart()
        {
            _eventTypeManager = new EventTypeManager();
            try
            {
                _eventType = _eventTypeManager.RetrieveAllEventTypes("All");
                if (_currentEventType == null)
                {
                    _currentEventType = _eventType;
                }
                dgEventTypes.ItemsSource = _currentEventType;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnEventTypeAddAction_Click(object sender, RoutedEventArgs e)
        {
            //An empty constructor allows us to invoke the Event Type Add.
            //form with out having starting data. So we can add it. 

            var addEventType = new CreateEventType();
            var result = addEventType.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentEventType = null;
                    _eventType = _eventTypeManager.RetrieveAllEventTypes("All");
                    if (_currentEventType == null)
                    {
                        _currentEventType = _eventType;
                    }
                    dgEventTypes.ItemsSource = _currentEventType;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnEventTypeActionDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteEventType = new DeleteEventType();
            var result = deleteEventType.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentEventType = null;
                    _eventType = _eventTypeManager.RetrieveAllEventTypes("All");
                    if (_currentEventType == null)
                    {
                        _currentEventType = _eventType;
                    }
                    dgEventTypes.ItemsSource = _currentEventType;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /*--------------------------- Ending BrowseEventTypes Code --------------------------------*/
        #endregion

        #region Appointment Type Code
        /*--------------------------- Starting BrowseAppointment Code #BrowseAppointment --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseAppointmentTypeDoOnStart()
        {
            _appointmentTypeManager = new AppointmentTypeManager();
            try
            {

                _appointmentType = _appointmentTypeManager.RetrieveAllAppointmentTypes("All");
                if (_currentAppointmentType == null)
                {
                    _currentAppointmentType = _appointmentType;
                }
                dgAppointmentTypes.ItemsSource = _currentAppointmentType;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        //  BtnAppointmentTypeAddAction_Click
        /// <summary>
        ///Button click event to Add an appointmentType.
        /// </summary>
        /// <param name="PetType newPetType">The BtnAppointmentTypeAddAction calls RetrieveAllAppointmentTypes.</param>
        /// <returns></returns>
        private void BtnAppointmentTypeAddAction_Click(object sender, RoutedEventArgs e)
        {
            var addAppointmentType = new CreateAppointmentType();
            var result = addAppointmentType.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentAppointmentType = null;
                    _appointmentType = _appointmentTypeManager.RetrieveAllAppointmentTypes("All");
                    if (_currentAppointmentType == null)
                    {
                        _currentAppointmentType = _appointmentType;
                    }
                    dgAppointmentTypes.ItemsSource = _currentAppointmentType;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        //  BtnAppointmentTypeActionDelete_Click
        /// <summary>
        /// Button for deleting an appointment Type.
        /// </summary>
        /// <param name="">The BtnAppointmentTypeActionDelete calls the RetrieveAllAppointmentTypes("All").</param>
        /// <returns></returns>

        private void BtnAppointmentTypeActionDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteAppointmentType = new DeleteAppointmentType();
            var result = deleteAppointmentType.ShowDialog();
            if (result == true)
            {
                try
                {
                    _currentAppointmentType = null;
                    _appointmentType = _appointmentTypeManager.RetrieveAllAppointmentTypes("All");
                    if (_currentAppointmentType == null)
                    {
                        _currentAppointmentType = _appointmentType;
                    }
                    dgAppointmentTypes.ItemsSource = _currentAppointmentType;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }





        /*--------------------------- Ending BrowseAppointment Code --------------------------------*/
        #endregion

        #region Guest Code
        /*--------------------------- Starting BrowseGuest Code #BrowseGuest --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseGuestDoOnStart()
        {
            _guestsBrowseGuests = new List<Guest>();
            _guestsSearched = new List<Guest>();
            _guestManager = new GuestManager();


            try
            {
                _guestsBrowseGuests = _guestManager.ReadAllGuests();
                if (dgGuestsList.ItemsSource == null)
                {
                    dgGuestsList.ItemsSource = _guests;
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
        private void dgGuestsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgGuestsList.SelectedItem != null && ((Guest)dgGuests.SelectedItem).Active != false)
                {
                    var selectedGuest = (Guest)dgGuestsList.SelectedItem;
                    var detail = new frmAddEditGuest(selectedGuest);
                    var result = detail.ShowDialog();
                    _guestsBrowseGuests = _guestManager.ReadAllGuests();
                    dgGuestsList.ItemsSource = _guests;
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
            try
            {
                _guestsBrowseGuests = _guestManager.ReadAllGuests();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            dgGuestsList.ItemsSource = _guestsBrowseGuests;
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
            _guestsSearched = _guestsBrowseGuests.FindAll(g => g.FirstName.ToLower().Contains(searchFirst)
                && g.LastName.ToLower().Contains(searchLast));
            dgGuestsList.ItemsSource = _guestsSearched;
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
                if (dgGuestsList.SelectedItem != null)
                {
                    Guest guest = _guestManager.ReadGuestByGuestID(((Guest)dgGuestsList.SelectedItem).GuestID);
                    if (guest.Active == true)
                    {
                        _guestManager.DeactivateGuest(guest.GuestID);
                    }
                    else if (guest.Active == false)
                    {
                        _guestManager.ReactivateGuest(guest.GuestID);
                    }
                    _guestsBrowseGuests = _guestManager.ReadAllGuests();
                    dgGuestsList.ItemsSource = _guestsBrowseGuests;
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
                if (dgGuestsList.SelectedItem != null)
                {
                    Guest guest = _guestManager.ReadGuestByGuestID(((Guest)dgGuestsList.SelectedItem).GuestID);
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
                    _guestsBrowseGuests = _guestManager.ReadAllGuests();
                    dgGuestsList.ItemsSource = _guests;
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
                if (dgGuestsList.SelectedItem != null)
                {
                    Guest guest = _guestManager.ReadGuestByGuestID(((Guest)dgGuestsList.SelectedItem).GuestID);
                    if (guest.CheckedIn == false)
                    {
                        _guestManager.CheckInGuest(guest.GuestID);
                    }
                    else if (guest.CheckedIn == true)
                    {
                        _guestManager.CheckOutGuest(guest.GuestID);
                    }
                    _guestsBrowseGuests = _guestManager.ReadAllGuests();
                    dgGuestsList.ItemsSource = _guestsBrowseGuests;
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
                if (dgGuestsList.SelectedItem != null && ((Guest)dgGuestsList.SelectedItem).Active != false)
                {
                    var selectedGuest = (Guest)dgGuestsList.SelectedItem;
                    var detail = new frmAddEditGuest(selectedGuest);
                    var result = detail.ShowDialog();
                    _guestsBrowseGuests = _guestManager.ReadAllGuests();
                    dgGuestsList.ItemsSource = _guestsBrowseGuests;
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
        private void dgGuestsList_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGuestsList.SelectedItem != null)
                {
                    Guest _selectedGuest = new Guest();
                    try
                    {
                        _selectedGuest = (Guest)dgGuestsList.SelectedItem;
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
            _guestsBrowseGuests = _guestManager.ReadAllGuests();
            dgGuestsList.ItemsSource = null;
            dgGuestsList.ItemsSource = _guestsBrowseGuests;
            _guestsSearched = _guestsBrowseGuests;
        }

        /*--------------------------- Ending BrowseGuest Code --------------------------------*/
        #endregion

        #region Guest Vehicle Code
        /*--------------------------- Starting BrowseGuestVehicle Code #BrowseGuestVehicle --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseGuestVehicleDoOnStart()
        {
            _vehicles = new List<VMGuestVehicle>();
            _guestVehicleManager = new GuestVehicleManager();
            _searchOptions = new List<string>();
            refreshGridGuestVehicle();
            fillOptions();
        }


        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Makes a Detail Form for adding a new GuestVehicle
        /// </summary>
        private void BtnAddNewGuestVehicle_Click(object sender, RoutedEventArgs e)
        {
            var guestVehicleDetail = new GuestVehicleDetail();
            var result = guestVehicleDetail.ShowDialog();
            if (result == true)
            {
                refreshGridGuestVehicle();
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Sets the combo box and Search bar to blank, and refreshes the Grid
        /// </summary>
        private void BtnClearFiltersGuestVehicle_Click(object sender, RoutedEventArgs e)
        {
            cboSearchCategory.SelectedIndex = -1;
            txtSearchTerm.Text = "";
            refreshGridGuestVehicle();
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Searches through the existing Grid for data matching what's in the search bar
        /// with what's in the Grid
        /// </summary>
        private void TxtSearchTermGuestVehicle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cboSearchCategoryGuestVehicle.SelectedIndex != -1)
            {
                switch (cboSearchCategoryGuestVehicle.SelectedIndex)
                {
                    case 0:
                        _currentListGuestVehicle = _vehicles.FindAll(v => v.FirstName.ToLower().Contains(txtSearchTerm.Text));
                        applyFiltersGuestVehicle();
                        break;
                    case 1:
                        _currentListGuestVehicle = _vehicles.FindAll(v => v.LastName.ToLower().Contains(txtSearchTerm.Text));
                        applyFiltersGuestVehicle();
                        break;
                    case 2:
                        _currentListGuestVehicle = _vehicles.FindAll(v => v.Make.ToLower().Contains(txtSearchTerm.Text));
                        applyFiltersGuestVehicle();
                        break;
                    case 3:
                        _currentListGuestVehicle = _vehicles.FindAll(v => v.Model.ToLower().Contains(txtSearchTerm.Text));
                        applyFiltersGuestVehicle();
                        break;
                    case 4:
                        _currentListGuestVehicle = _vehicles.FindAll(v => v.Color.ToLower().Contains(txtSearchTerm.Text));
                        applyFiltersGuestVehicle();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Opens a Detail Form for Viewing the Details of a GuestVehicle
        /// </summary>
        private void DgGuestVehicles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgGuestVehicles.SelectedIndex != -1)
            {
                VMGuestVehicle vehicle = (VMGuestVehicle)dgGuestVehicles.SelectedItem;
                var guestVehicleDetail = new GuestVehicleDetail(vehicle, false);
                var result = guestVehicleDetail.ShowDialog();
                if (result == true)
                {
                    refreshGridGuestVehicle();
                }
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Opens a Detail Form for Viewing the Details of a GuestVehicle
        /// </summary>
        private void BtnViewDetailGuestVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (dgGuestVehicles.SelectedIndex != -1)
            {
                VMGuestVehicle vehicle = (VMGuestVehicle)dgGuestVehicles.SelectedItem;
                var guestVehicleDetail = new GuestVehicleDetail(vehicle, false);
                guestVehicleDetail.ShowDialog();
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Opens a Detail Form for Updating a GuestVehicle
        /// </summary>
        private void BtnUpdateGuestVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (dgGuestVehicles.SelectedIndex != -1)
            {
                VMGuestVehicle vehicle = (VMGuestVehicle)dgGuestVehicles.SelectedItem;
                var guestVehicleDetail = new GuestVehicleDetail(vehicle, true);
                var result = guestVehicleDetail.ShowDialog();
                if (result == true)
                {
                    refreshGridGuestVehicle();
                }
            }

        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Refreshes the Grid
        /// </summary>
        private void refreshGridGuestVehicle()
        {
            try
            {
                _vehicles = _guestVehicleManager.RetrieveAllGuestVehicles();
                _currentListGuestVehicle = _vehicles;
                dgGuestVehicles.ItemsSource = null;
                dgGuestVehicles.ItemsSource = _vehicles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Load Vehicle List: \n" + ex.Message);
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Fills the Search options for the combo box
        /// </summary>
        private void fillOptions()
        {
            _searchOptions.Add("First Name");
            _searchOptions.Add("Last Name");
            _searchOptions.Add("Make");
            _searchOptions.Add("Model");
            _searchOptions.Add("Color");
            cboSearchCategoryGuestVehicle.ItemsSource = _searchOptions;
        }

        private void applyFiltersGuestVehicle()
        {
            dgGuestVehicles.ItemsSource = _currentListGuestVehicle;
        }

        private void CboSearchCategoryGuestVehicle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboSearchCategoryGuestVehicle.SelectedIndex != -1)
            {
                switch (cboSearchCategoryGuestVehicle.SelectedIndex)
                {
                    case 0:
                        _currentListGuestVehicle = _vehicles.FindAll(v => v.FirstName.ToLower().Contains(txtSearchTerm.Text));
                        applyFiltersGuestVehicle();
                        break;
                    case 1:
                        _currentListGuestVehicle = _vehicles.FindAll(v => v.LastName.ToLower().Contains(txtSearchTerm.Text));
                        applyFiltersGuestVehicle();
                        break;
                    case 2:
                        _currentListGuestVehicle = _vehicles.FindAll(v => v.Make.ToLower().Contains(txtSearchTerm.Text));
                        applyFiltersGuestVehicle();
                        break;
                    case 3:
                        _currentListGuestVehicle = _vehicles.FindAll(v => v.Model.ToLower().Contains(txtSearchTerm.Text));
                        applyFiltersGuestVehicle();
                        break;
                    case 4:
                        _currentListGuestVehicle = _vehicles.FindAll(v => v.Color.ToLower().Contains(txtSearchTerm.Text));
                        applyFiltersGuestVehicle();
                        break;
                    default:
                        break;
                }
            }
        }
        /*--------------------------- Ending BrowseRoom Code --------------------------------*/
        #endregion

        #region Setup List Code
        /*--------------------------- Starting BrowseSetupList Code #BrowseSetupList --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseSetupListDoOnStart()
        {
            _setupManager = new SetupManager();
            refreshAllSetups();
            populateSetups();

        }
        private void refreshAllSetups()
        {
            try
            {
                _setups = _setupManager.SelectVMSetups();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            _currentSetups = _setups;
        }

        private void populateSetups()
        {
            try
            {
                _setups = _setupManager.SelectVMSetups();
                if (_currentSetups == null)
                {
                    _currentSetups = _setups;
                }
                dgSetups.ItemsSource = _currentSetups;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgSetups_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Setup chosenSetup = new Setup();

            chosenSetup = _setupManager.SelectSetup(((VMSetup)dgSetups.SelectedItem).SetupID);
            try
            {
                var readSetup = new SetupDetail(chosenSetup);
                readSetup.ShowDialog();

                refreshAllSetups();
                populateSetups();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to find that specific Setup." + ex.Message);
            }
        }

        private void btnAddSetup_Click(object sender, RoutedEventArgs e)
        {
            var createSetup = new SetupDetail();
            createSetup.ShowDialog();
            refreshAllSetups();
            populateSetups();
        }


        private void filterDateEntered(DateTime date)
        {
            _currentSetups = _currentSetups.FindAll(s => s.DateEntered.CompareTo(date) >= 0);
        }

        private void filterDateRequired(DateTime date)
        {
            _currentSetups = _currentSetups.FindAll(s => s.DateRequired.CompareTo(date) >= 0);
        }

        private void filterSpecificDate(DateTime dateEntered, DateTime dateRequired)
        {
            _currentSetups = _currentSetups.FindAll(s => s.DateEntered.Date.CompareTo(dateEntered) <= 0 && s.DateEntered.Date.CompareTo(dateRequired) >= 0);
        }

        private void filterEventTitle(string eventTitle)
        {
            _currentSetups = _currentSetups.FindAll(s => s.EventTitle.Contains(eventTitle));
        }

        private void btnFilterSetup_Click(object sender, RoutedEventArgs e)
        {
            if (!(txtEventSetup.Text == null || txtEventSetup.Text.Length < 1))
            {
                filterEventTitle(txtEventSetup.Text);
            }

            if (!(dtpSetupDateEntered.Text == null || dtpSetupDateEntered.Text.Length < 1)
                && (dtpSetupDateRequired.Text == null || dtpSetupDateRequired.Text.Length < 1))
            {
                filterDateEntered(dtpSetupDateEntered.SelectedDate.Value.Date);
            }

            if (!(dtpSetupDateRequired.Text == null || dtpSetupDateRequired.Text.Length < 1)
                && (dtpSetupDateEntered == null || dtpSetupDateEntered.Text.Length < 1))
            {
                filterDateRequired(dtpSetupDateRequired.SelectedDate.Value.Date);
            }

            if (!(dtpSetupDateEntered.Text == null || dtpSetupDateEntered.Text.Length < 1)
                && !(dtpSetupDateRequired.Text == null || dtpSetupDateRequired.Text.Length < 1))
            {
                filterSpecificDate(dtpSetupDateEntered.SelectedDate.Value.Date, dtpSetupDateRequired.SelectedDate.Value.Date);
            }

            populateSetups();
        }

        private void btnClearSetup_Click(object sender, RoutedEventArgs e)
        {
            _currentSetups = _setups;
            populateSetups();
            dtpSetupDateEntered.Text = "";
            dtpSetupDateRequired.Text = "";
            txtEventSetup.Text = "";
        }

        private void btnBrowseSetupList_Click(object sender, RoutedEventArgs e)
        {
            //check to see if an item is selected from the datagrid
            VMSetup vmsetup = new VMSetup();
            if (dgSetups.SelectedItem != null)
            {
                vmsetup = (VMSetup)dgSetups.SelectedItem;
                var browseSetupList = new BrowseSetupList(vmsetup);
                browseSetupList.ShowDialog();
            }
            else
            {
                var browseSetupList = new BrowseSetupList();
                browseSetupList.ShowDialog();
            }


        }

        private void BtnDeleteSetup_Click(object sender, RoutedEventArgs e)
        {


            _setupManager.DeleteSetup(((VMSetup)dgSetups.SelectedItem).SetupID);

            var result = MessageBox.Show("Are you sure you want to delete this setup? You will also have to " +
                "delete the Setup List too.", "This Setup will no longer be in the system.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("The Setup has been purged from the system.");
            }
            refreshAllSetups();
            populateSetups();

        }



        /*--------------------------- Ending BrowseSetupList Code --------------------------------*/
        #endregion

        #region Sponsor Code
        /*--------------------------- Starting BrowseSponsor Code #BrowseSponsor --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseSponsorDoOnStart()
        {
            _sponsorManager = new SponsorManager();
            refreshAllSponsors();
            populateSponsors();
        }


        private void refreshAllSponsors()
        {
            try
            {
                _allSponsors = _sponsorManager.SelectAllSponsors();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
            _currentSponsors = _allSponsors;
        }

        private void populateSponsors()
        {
            dgSponsors.ItemsSource = _currentSponsors;
        }

        private void btnCancelBrowseSponsor_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddSponsor_Click(object sender, RoutedEventArgs e)
        {
            var createSponsor = new FrmSponsor();
            createSponsor.ShowDialog();
            refreshAllSponsors();
            populateSponsors();
        }

        private void btnDeleteBrowseSponsor_Click(object sender, RoutedEventArgs e)
        {
            if (dgSponsors.SelectedIndex != -1)
            {
                try
                {
                    _sponsorManager.DeleteSponsor(((Sponsor)dgSponsors.SelectedItem).SponsorID, ((Sponsor)dgSponsors.SelectedItem).Active);
                    refreshAllSponsors();
                    populateSponsors();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to Delete that Sponsor\n" + ex.Message);
                }

            }
        }

        private void dgSponsors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yy";
            }

        }

        private void btnClearFiltersBrowseSponsor_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            filterSponsors();
        }

        private void filterSponsors()
        {
            string searchTerm = null;

            try
            {
                searchTerm = (txtSearch.Text).ToLower().ToString();
                _currentSponsors = _allSponsors.FindAll(m => m.Name.ToLower().Contains(searchTerm));


                if (txtSearch.Text.ToString() != "")
                {
                    _currentSponsors = _currentSponsors.FindAll(m => m.Name.ToLower().Contains(txtSearch.Text.ToString().ToLower()));
                }

                dgSponsors.ItemsSource = _currentSponsors;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFilterBrowseSponsor_Click(object sender, RoutedEventArgs e)
        {
            filterSponsors();
        }




        private void dgSponsors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgSponsors.SelectedIndex != -1)
            {
                Sponsor selectedSponsor = new Sponsor();
                try
                {
                    selectedSponsor = _sponsorManager.SelectSponsor(((Sponsor)dgSponsors.SelectedItem).SponsorID);
                    var readUpdateSponsor = new FrmSponsor(selectedSponsor);
                    readUpdateSponsor.ShowDialog();
                    refreshAllSponsors();
                    populateSponsors();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to find that Sponsor\n" + ex.Message);
                }

            }
        }

        /// <summary>
        /// @Author by Phillip Hansen
        /// @Created 3/29/2019
        /// 
        /// Needed a 'Read Sponsor' button to exist and perform functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadSponsor_Click(object sender, RoutedEventArgs e)
        {

        }


        /*--------------------------- Ending BrowseSponsor Code --------------------------------*/
        #endregion

        #region Recipe Code
        /*--------------------------- Starting BrowseRecipe Code #BrowseRecipe --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseRecipeDoOnStart()
        {
            roles = new List<string>();
            _recipeManager = new RecipeManager();
            _isFilterRestting = false;
            setupBrowsePage();
        }


        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Sets up the content and controls of the browsing window.
        /// </summary>
        private void setupBrowsePage()
        {
            try
            {
                _recipes = _recipeManager.RetrieveAllRecipes();
                dgRecipeList.ItemsSource = _recipes;
                dtpDateEndBrowseRecipe.Focusable = false;
                dtpDateStartBrowseRecipe.Focusable = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Could not setup page.");
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Modifies the headers and sizes of the datagrid columns.
        /// </summary>
        private void DgRecipeList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
            }
            switch (e.Column.Header)
            {
                case "RecipeID":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Name":
                    break;
                case "Description":
                    e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
                case "DateAdded":
                    e.Column.Header = "Date Added";
                    break;
                case "Active":
                    break;
                case "RecipeLines":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Exits out of the Browsing screen.
        /// </summary>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Allows the user to view a Recipe.
        /// </summary>
        private void BtnViewRecipe_Click_1(object sender, RoutedEventArgs e)
        {
            if ((Recipe)dgRecipeList.SelectedItem != null)
            {
                var detailForm = new frmCreateRecipe((Recipe)dgRecipeList.SelectedItem, _employee);
                var result = detailForm.ShowDialog();
                _recipes = _recipeManager.RetrieveAllRecipes();
                dgRecipeList.ItemsSource = _recipes;
            }
            else
            {
                MessageBox.Show("You must select a recipe first.");
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Allows the user to create a new Recipe.
        /// </summary>
        private void BtnCreateRecipe_Click(object sender, RoutedEventArgs e)
        {
            var createForm = new frmCreateRecipe(_employee);
            var result = createForm.ShowDialog();
            if (result == true)
            {
                MessageBox.Show("Recipe created.");
            }
            else
            {
                MessageBox.Show("Recipe creation cancelled or failed.");
            }
            try
            {
                setupBrowsePage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error updating the page: " + ex.Message);
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Filters the datagrid by the user's input.
        /// </summary>
        private void filterRecipeList()
        {
            setupBrowsePage();
            IEnumerable<Recipe> currentRecipes = _recipes;
            // Filter names
            if (txtNameBrowseRecipe.Text != "" && txtNameBrowseRecipe.Text != null)
            {
                currentRecipes = currentRecipes.Where(r => r.Name.ToUpper().StartsWith(txtNameBrowseRecipe.Text.ToUpper()));
            }
            //Filter description
            if (txtDescriptionBrowseRecipe.Text != "" && txtDescriptionBrowseRecipe.Text != null)
            {
                currentRecipes = currentRecipes.Where(r => r.Description.ToUpper().Contains(txtDescriptionBrowseRecipe.Text.ToUpper()));
            }
            //Filter Start and End Date
            //Both have valid values
            if (dtpDateStartBrowseRecipe.SelectedDate.HasValue && dtpDateEndBrowseRecipe.SelectedDate.HasValue)
            {
                // Make sure start is before end
                if (dtpDateStartBrowseRecipe.SelectedDate.Value.CompareTo(dtpDateEndBrowseRecipe.SelectedDate.Value) < 0)
                {
                    // Filter start
                    currentRecipes = currentRecipes.Where(r => r.DateAdded >= dtpDateStartBrowseRecipe.SelectedDate.Value);

                    //Filter end
                    currentRecipes = currentRecipes.Where(r => r.DateAdded <= dtpDateEndBrowseRecipe.SelectedDate.Value);
                }
            }
            else if (dtpDateStartBrowseRecipe.SelectedDate.HasValue && !dtpDateEndBrowseRecipe.SelectedDate.HasValue)
            {
                // Filter start
                currentRecipes = currentRecipes.Where(r => r.DateAdded >= dtpDateStartBrowseRecipe.SelectedDate.Value);
            }
            else if (!dtpDateStartBrowseRecipe.SelectedDate.HasValue && dtpDateEndBrowseRecipe.SelectedDate.HasValue)
            {
                //Filter end
                currentRecipes = currentRecipes.Where(r => r.DateAdded <= dtpDateEndBrowseRecipe.SelectedDate.Value);
            }
            dgRecipeList.ItemsSource = null;
            dgRecipeList.ItemsSource = currentRecipes;

        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// On click, filters the list according to search criteria.
        /// </summary>
        private void BtnFilterBrowseRecipe_Click(object sender, RoutedEventArgs e)
        {
            filterRecipeList();
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// On click, clears the filters and resets the grid.
        /// </summary>
        private void BtnClearFilterBrowseRecipe_Click(object sender, RoutedEventArgs e)
        {
            _isFilterRestting = true;
            txtNameBrowseRecipe.Text = "";
            txtDescriptionBrowseRecipe.Text = "";
            dtpDateStartBrowseRecipe.SelectedDate = null;
            dtpDateEndBrowseRecipe.SelectedDate = null;
            dtpDateStartBrowseRecipe.DisplayDateEnd = null;
            dtpDateEndBrowseRecipe.DisplayDateStart = null;
            dgRecipeList.ItemsSource = _recipes;
            _isFilterRestting = false;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// When the start date changes, the end date picker updates so that that date must be after the start.
        /// </summary>
        private void DtpDateStartBrowseRecipe_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isFilterRestting)
            {
                dtpDateEndBrowseRecipe.DisplayDateStart = dtpDateStartBrowseRecipe.SelectedDate.Value.AddDays(1);
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// When the end date changes, the start date picker updates so that that date must be before the end.
        /// </summary>
        private void DtpDateEndBrowseRecipe_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isFilterRestting)
            {
                dtpDateStartBrowseRecipe.DisplayDateEnd = dtpDateEndBrowseRecipe.SelectedDate.Value.AddDays(-1);
            }
        }

        private void dgRecipeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((Recipe)dgRecipeList.SelectedItem != null)
            {
                var detailForm = new frmCreateRecipe((Recipe)dgRecipeList.SelectedItem, _employee);
                var result = detailForm.ShowDialog();
                _recipes = _recipeManager.RetrieveAllRecipes();
                dgRecipeList.ItemsSource = _recipes;
            }
            else
            {
                MessageBox.Show("You must select a recipe first.");
            }
        }


        /// <summary>
        /// @Author by Phillip Hansen
        /// @Created 3/29/2019
        /// 
        /// Needed a 'Delete Recipe' button to exist and perform functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteRecipe_Click(object sender, RoutedEventArgs e)
        {

        }


        /*--------------------------- Ending BrowseRecipe Code --------------------------------*/
        #endregion

        #region Event Sponsor Code
        /*--------------------------- Starting BrowseEventSponsor Code #BrowseEventSponsor --------------------------------*/
        /// <summary>
        /// @Author: Phillip Hansen
        /// @Created : 3/13/2019
        /// 
        /// 
        /// </summary>
        private void BrowseEventSponsorsListDoOnStart()
        {
            _eventSponsManager = new EventSponsorManager();
            populateEvSponsList();
            dgEventSponsor.IsEnabled = true;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// When a record is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgEventSponsor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgEventSponsor.SelectedIndex > -1)
            {
                var selectedEvSpons = (EventSponsor)dgEventSponsor.SelectedItem;

                if (selectedEvSpons == null)
                {
                    MessageBox.Show("No record selected!");
                }
            }
            else
            {
                MessageBox.Show("No record selected!");
            }

        }

        private void dgEventSponsor_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();

            if (headerName == "Event ID")
            {
                e.Column.Header = "Event ID";
            }
            else if (headerName == "SponsorID")
            {
                e.Column.Header = "Sponsor ID";
            }

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Event Handler for deleting a selected record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteEventSpons_Click(object sender, RoutedEventArgs e)
        {
            EventSponsor selectedRecord = (EventSponsor)dgEventSponsor.SelectedItem;

            if (dgEventSponsor.SelectedIndex > -1)
            {
                _eventSponsManager.DeleteEventSponsor(selectedRecord);
            }
            else
            {
                MessageBox.Show("A record from the list must be selected!");
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Method for populating the data grid with records
        /// </summary>
        private void populateEvSponsList()
        {
            try
            {
                _eventSponsors = _eventSponsManager.RetrieveAllEventSponsors();
                dgEventSponsor.ItemsSource = _eventSponsors;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not retrieve Event Sponsor List.");
            }
        }


        /*--------------------------- Ending BrowseEventSponsor Code --------------------------------*/
        #endregion

        #region Event Performance Code
        /*--------------------------- Starting BrowseEventPerformance Code #BrowseEventPerformance --------------------------------*/
        /// <summary>
        /// @Author: Phillip Hansen
        /// @Created : 4/10/2019
        /// 
        /// 
        /// </summary>
        private void BrowseEventPerformancesListDoOnStart()
        {
            _eventPerfManager = new EventPerformanceManager();
            populateEvPerfList();
            
            dgEventPerformance.IsEnabled = true;
        }

       
        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Populates the data grid list with the complete table
        /// </summary>
        private void populateEvPerfList()
        {
            _eventPerformances = null;
            dgEventPerformance.ItemsSource = null;
            dgEventPerformance.Items.Refresh();

            try
            {
                _eventPerformances = _eventPerfManager.RetrieveAllEventPerformances();
                dgEventPerformance.ItemsSource = _eventPerformances;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not retrieve Event Performance List.");
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Changes the names of the header columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgEventPerformance_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();

            if (headerName == "EventID")
            {
                e.Column.Header = "Event ID";
            }
            else if (headerName == "PerformanceID")
            {
                e.Column.Header = "Performance ID";
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Event listener when a record is double clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgEventPerformance_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgEventPerformance.SelectedIndex > -1)
            {
                var selectedEvPerf = (EventPerformance)dgEventPerformance.SelectedItem;

                if(selectedEvPerf == null)
                {
                    MessageBox.Show("No record selected!");
                }
            }
            else
            {
                MessageBox.Show("No record selected!");
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Button action for deleting a selected record
        /// (Keep this?)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteEventPerf_Click(object sender, RoutedEventArgs e)
        {
            EventPerformance selectedRecord = (EventPerformance)dgEventPerformance.SelectedItem;

            if(dgEventPerformance.SelectedIndex > -1)
            {
                //Add delete method here
            }
        }


        /*--------------------------- Ending BrowseEventPerformance Code --------------------------------*/
        #endregion

        #region Event Code

        /*--------------------------- Starting BrowseEvent Code #BrowseEvent --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// 
        /// Updated by Phillip Hansen on 4/4/2019
        /// Added data for improved functionality flow
        /// </summary>
        private void BrowseEventDoOnStart()
        {
            _eventManager = new EventManager();
            _selectedEvent = new Event();
            btnEventUncancelled.IsChecked = true;
            populateEvents();
            dgEvents.IsEnabled = true;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// When an event record is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgEvents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgEvents.SelectedIndex > -1)
            {
                _selectedEvent = (Event)dgEvents.SelectedItem;

                if (_selectedEvent == null)
                {
                    MessageBox.Show("No Event Selected!");
                }
                else
                {
                    var detailA = new frmAddEditEvent(_employee, _selectedEvent);
                    detailA.ShowDialog();
                    if (detailA.DialogResult == true)
                    {
                        populateEvents();
                    }
                }
            }
            else
            {
                MessageBox.Show("No event selected!");
            }

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// @Created: 4/3/2019
        /// 
        /// Event handler for when the radio button 'UncancelledEvents' is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEventUncancelled_Checked(object sender, RoutedEventArgs e)
        {
            //make sure the button for 'cancelled' is inaccessable
            btnUncancelEvent.Visibility = Visibility.Hidden;
            
            //re-populate the data grid with the events
            populateEvents();

            //The "delete" button for Event should be 'Cancel Event' instead
            btnDeleteEvent.Content = "Cancel Event";
            
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// @Created: 4/3/2019
        /// 
        /// Event handler for when the radio button 'CancelledEvents' is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEventCancelled_Checked(object sender, RoutedEventArgs e)
        {
            btnUncancelEvent.Visibility = Visibility.Visible;

            populateEvents();

            btnDeleteEvent.Content = "Delete";
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// @Created: 4/4/2019
        /// 
        /// Event handler for a button to un-cancel a pre-selected event object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUncancelEvent_Click(object sender, RoutedEventArgs e)
        {
            _selectedEvent = (Event)dgEvents.SelectedItem;

            try
            {
                _eventManager.UpdatEventToUncancel(_selectedEvent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not update event to un-cancelled!");
            }
            finally
            {
                populateEvents();
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// Created: 4/4/2019
        /// 
        /// Re-populates the list based on what is in the text for searching an event by name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEventFilter_Click(object sender, RoutedEventArgs e)
        {
            if (btnEventUncancelled.IsChecked == true)
            {
                if(txtEventSearchName != null)
                {
                    List<Event> _filteredEvents = new List<Event>();
                    foreach (var item in _eventManager.RetrieveAllEvents().Where(b => b.EventTitle.Equals(txtEventSearchName.Text.ToString())))
                    {
                        _filteredEvents.Add(item);
                    }
                    
                    dgEvents.ItemsSource = _filteredEvents;
                }
                else
                {
                    dgEvents.ItemsSource = _events;
                }
                
            }
            else if(btnEventCancelled.IsChecked == true)
            {
                if (txtEventSearchName != null)
                {
                    List<Event> _filteredEvents = new List<Event>();
                    foreach (var item in _eventManager.RetrieveAllCancelledEvents().Where(b => b.EventTitle.Equals(txtEventSearchName.Text.ToString())))
                    {
                        _filteredEvents.Add(item);
                    }

                    dgEvents.ItemsSource = _filteredEvents;
                }
                else
                {
                    dgEvents.ItemsSource = _events;
                }
            }
            
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// Created: 4/4/2019
        /// 
        /// Method for clearing the filter, depending on what radio button is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEventClearFilter_Click(object sender, RoutedEventArgs e)
        {
            txtEventSearchName.Text = "";
            populateEvents();

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Code for when the 'create' button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreateEvent_Click(object sender, RoutedEventArgs e)
        {
            _eventSponsManager = new EventSponsorManager();
            _eventPerfManager = new EventPerformanceManager();

            //The Form requires the User's ID for a field in the record
            var addEventReq = new frmAddEditEvent(_employee);
            var result = addEventReq.ShowDialog();
            if (addEventReq._createdEventID != 0 && addEventReq._retrievedSponsor != null)
            {
                _eventSponsManager.CreateEventSponsor(addEventReq._createdEventID, addEventReq._retrievedSponsor.SponsorID);
            }
            else if (addEventReq._createdEventID != 0 && addEventReq._retrievedPerf != null)
            {
                _eventPerfManager.CreateEventSponsor(addEventReq._createdEventID, addEventReq._retrievedPerf.ID);
            }
            if (result == true)
            {
                populateEvents();
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// @Created: 4/3/2019
        /// 
        /// Button to capture a selected record and allow it to be read/updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnViewEvent_Click(object sender, RoutedEventArgs e)
        {
            _selectedEvent = (Event)dgEvents.SelectedItem;

            if (dgEvents.SelectedIndex > -1)
            {
                var viewEvent = new frmAddEditEvent(_employee, _selectedEvent);
                var result = viewEvent.ShowDialog();
                if(result == true)
                {
                    populateEvents();
                }
            }
            else
            {
                MessageBox.Show("A record from the list must be selected!");
            }

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// @Created: 4/3/2019
        /// 
        /// Button to capture a selected record and allow it to be cancelled or deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            _selectedEvent = (Event)dgEvents.SelectedItem;

            if (dgEvents.SelectedIndex > -1)
            {
                if(btnDeleteEvent.Content.Equals("Cancel Event"))
                {
                    try
                    {
                        _eventManager.UpdateEventToCancel(_selectedEvent);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\nCould not update event to cancel!");
                    }
                    finally
                    {
                        populateEvents();
                    }
                    
                }
                else if(btnDeleteEvent.Content.Equals("Delete"))
                {
                    if (_selectedEvent.Approved == false)
                    {
                        var deleteEvent = new frmEventDeleteConfirmation(_selectedEvent);
                        var result = deleteEvent.ShowDialog();
                        if(result == true)
                        {
                            populateEvents();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Event must not be approved!");
                    }
                }
                
            }
            else
            {
                MessageBox.Show("A record from the list must be selected!");
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Changes the titles for the columns in the event datagrid to be human-readable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgEvents_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();

            if (headerName == "EventID")
            {
                e.Cancel = true;
            }
            if (headerName == "EmployeeID")
            {
                e.Cancel = true;
            }
            if(headerName == "Cancelled")
            {
                e.Cancel = true;
            }
            if(headerName == "SeatsRemaining")
            {
                e.Column.Header = "Open Seats";
            }
            if(headerName == "PublicEvent")
            {
                e.Column.Header = "Public?";
            }
            if(headerName == "Price")
            {
                e.Column.Header = "Entry Price";
            }
            if (headerName == "EventTitle")
            {
                e.Column.Header = "Event Title";
            }
            if (headerName == "EmployeeName")
            {
                e.Column.Header = "Created by";
            }
            if (headerName == "EventTypeID")
            {
                e.Column.Header = "Event Type";
            }
            if (headerName == "EventStartDate")
            {
                e.Column.Header = "Start Date";
            }
            if (headerName == "EventEndDate")
            {
                e.Column.Header = "End Date";
            }
            if (headerName == "KidsAllowed")
            {
                e.Column.Header = "Kids Allowed?";
            }
            if (headerName == "NumGuests")
            {
                e.Column.Header = "Max Guests";
            }
            if (headerName == "Sponsored")
            {
                e.Column.Header = "Sponsored?";
            }
            if (headerName == "Approved")
            {
                e.Column.Header = "Approved?";
            }
        }



        /// <summary>
        /// @Author: Phillip Hansen
        /// @Created 4/3/2019
        /// 
        /// Method for populating the events, depending on what the Event list contains
        /// 
        /// </summary>
        private void populateEvents()
        {
            //Empty the event list
            _events = null;
            //Make the source of the data grid null
            dgEvents.ItemsSource = null;
            //Refresh the data grid to empty all items
            dgEvents.Items.Refresh();


            //Re-Add the events based on the radio button selected
            if(btnEventCancelled.IsChecked == true)
            {
                try
                {
                    _events = _eventManager.RetrieveAllCancelledEvents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                
            }
            else if(btnEventUncancelled.IsChecked == true)
            {
                try
                {
                    _events = _eventManager.RetrieveAllEvents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: "+ex.Message);
                }
                
            }


            try
            {
                dgEvents.ItemsSource = _events;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not retrieve the list of Events.");
            }

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Closes the window if the 'cancel' button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelEventMain_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }


        /*--------------------------- Ending BrowseEvent Code --------------------------------*/
        #endregion

        #region Supplier Orders Code
        /*--------------------------- Starting BrowseSupplierOrders Code #BrowseSupplierOrders --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseSupplierOrdersDoOnStart()
        {
            _supplierOrderManager = new SupplierOrderManager();
            LoadSupplierCombo();
            LoadSupplierOrderGrid();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CboSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnCancelBrowseSupplierOrders_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Do You Really Want to Cancel Managing Supplier Orders?", "Cancel Supplier Order Management", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
        }


        private void LoadSupplierCombo()
        {
            try
            {
                _suppliers = _supplierManager.RetrieveAllSuppliers();
                cboSupplier.Items.Clear();
                foreach (Supplier supplier in _suppliers)
                {
                    cboSupplier.Items.Add(supplier.Name + " " + supplier.SupplierID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadSupplierOrderGrid()
        {
            try
            {
                _supplierOrders = _supplierOrderManager.RetrieveAllSupplierOrders();
                _currentSupplierOrders = _supplierOrders;

                dgSupplierOrders.ItemsSource = _supplierOrders;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgSupplierOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _supplierOrder = (SupplierOrder)dgSupplierOrders.SelectedItem;

            var supplierOrderManager = new frmAddEditSupplierOrder(_supplierOrder, EditMode.Edit);
            var result = supplierOrderManager.ShowDialog();
            if (result == true)
            {
                LoadSupplierCombo();
                LoadSupplierOrderGrid();
            }
            else
            {
                return;
            }
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            var supplierOrderManager = new frmAddEditSupplierOrder();
            var result = supplierOrderManager.ShowDialog();
            if (result == true)
            {
                LoadSupplierCombo();
                LoadSupplierOrderGrid();
            }
            else
            {
                return;
            }
        }

        private void BtnClearBrowseSupplierOrders_Click(object sender, RoutedEventArgs e)
        {
            cboSupplier.Text = "";
            dgSupplierOrders.ItemsSource = _supplierOrders;
            dgSupplierOrders.Items.Refresh();
        }

        private void BtnFilterBrowseSupplierOrders_Click(object sender, RoutedEventArgs e)
        {
            if (cboSupplier.Text.Length > 6)
            {
                int iSupplierID = int.Parse(cboSupplier.Text.Substring(cboSupplier.Text.Length - 6, 6));
                FilterOrders(iSupplierID);
            }



        }
        public void FilterOrders(int iSupplierID)
        {
            try
            {
                _currentSupplierOrders = _supplierOrders.FindAll(s => s.SupplierID == iSupplierID);

                dgSupplierOrders.ItemsSource = _currentSupplierOrders;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if ((SupplierOrder)dgSupplierOrders.SelectedItem != null)
            {
                _supplierOrder = (SupplierOrder)dgSupplierOrders.SelectedItem;
                MessageBoxResult mbresult;

                mbresult = MessageBox.Show("Are you sure you want to delete order number " + _supplierOrder.SupplierOrderID + "?", "Delete Order", MessageBoxButton.YesNo);

                if (mbresult == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    if (1 == _supplierOrderManager.DeleteSupplierOrder(_supplierOrder.SupplierOrderID))
                    {
                        MessageBox.Show("Record Deleted");
                        LoadSupplierOrderGrid();
                    }

                }

            }

        }


        /*--------------------------- Ending BrowseSupplierOrders Code --------------------------------*/
        #endregion

        #region Pets Code        
        /*--------------------------- Starting BrowsePets Code #BrowsePets --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowsePetsDoOnStart()
        {
            _petManager = new PetManager();
            petTypeManager = new PetTypeManager();

        }

        private void BtnCreatePet_Click(object sender, RoutedEventArgs e)
        {
            var addPet = new frmAddEditPet();
            var result = addPet.ShowDialog();
            if (result == true)
            {
                populatePets();
            }
        }


        private void DgPets_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();
            // if(headerName == "PetID") { e.Cancel = true; }
            // if (headerName == "EmployeeID") { e.Cancel = true; 


        }

        private void populatePets()
        {
            try
            {
                _pets = _petManager.RetrieveAllPets();
                dgPets.ItemsSource = _pets;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not retrieve the list of Pets." + "\n" + ex.StackTrace);

            }
        }

        private void BtnViewPet_Click(object sender, RoutedEventArgs e)
        {
            if (dgPets.SelectedIndex > -1)
            {
                var selectedPet = (Pet)dgPets.SelectedItem;

                if (selectedPet == null)
                {
                    MessageBox.Show("No Selected Pets.");
                }
                else
                {
                    var petDetail = new frmAddEditPet(selectedPet);

                    petDetail.ShowDialog();

                    if (petDetail.DialogResult == true)
                    {
                        populatePets();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a pet to view");
            }
        }

        private void BtnDeletePet_Click(object sender, RoutedEventArgs e)
        {
            Pet currentPet = (Pet)dgPets.SelectedItem;

            if (currentPet == null)
            {
                MessageBox.Show("Please select a pet to delete.");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete the pet?", "Delete Pet", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (_petManager.DeletePet(currentPet.PetID))
                    {
                        MessageBox.Show("Pet deleted");

                    }
                    else
                    {
                        MessageBox.Show("Pet was not deleted");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
            populatePets();
        }

        private void BtnEditPet_Click(object sender, RoutedEventArgs e)
        {
            if (dgPets.SelectedIndex > -1)
            {
                var selectedPet = (Pet)dgPets.SelectedItem;

                if (selectedPet == null)
                {
                    MessageBox.Show("No Selected Pets.");
                }
                else
                {
                    var petDetail = new frmAddEditPet(selectedPet, null, true);

                    petDetail.ShowDialog();

                    if (petDetail.DialogResult == true)
                    {
                        populatePets();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a pet to edit");
            }
        }


        /*--------------------------- Ending BrowsePets Code --------------------------------*/
        #endregion

        #region Room Code
        /*--------------------------- Starting BrowseRoom Code #BrowseRoom --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseRoomsDoOnStart()
        {
            _roomManager = new RoomManager();
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
                if (_roomTypesIDList != null)
                {
                    foreach (var item in _roomTypesIDList)
                    {
                        cboRoomType.Items.Add(item);
                    }
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

        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/05
        /// Removed lambda expression used to find all current rooms since active is no longer a field
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        /*--------------------------- Ending BrowseRoom Code --------------------------------*/
        #endregion

        #region Maintenance Type Code
        /*--------------------------- Starting BrowseMaintenanceType Code #BrowseMaintenanceType --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseMaintenanceTypeDoOnStart()
        {
            maintenanceManager = new MaintenanceTypeManagerMSSQL();
            try
            {
                //type = maintenanceManager.RetrieveMaintenanceTypes("status");
                //type = maintenanceManager.RetrieveMaintenanceType("status");
                if (currentType == null)
                {
                    currentType = type;
                }
                dgMaintenanceTypes.ItemsSource = currentType;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Opens up the add window and updates the datagrid if MaintenanceType was created successfully
        /// </summary>
        private void btnAddBrowseMaintenanceTypes_Click(object sender, RoutedEventArgs e)
        {
            //var addType = new AddMaintenanceType();
            return;
            //var addType = new AddMaintenanceType();
            //var result = addType.ShowDialog();
            //if (result == true)
            //{
            //    try
            //    {
            //        currentType = null;
            //        type = maintenanceManager.RetrieveAllMaintenanceTypes();
            //        if (currentType == null)
            //        {
            //            currentType = type;
            //        }
            //        dgMaintenanceTypes.ItemsSource = currentType;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }

        /// <summary>
        /// Opens up the delete window and updates the datagrid if MaintenanceType was deleted successfully
        /// </summary>
        private void btnDeleteBrowseMaintenanceTypes_Click(object sender, RoutedEventArgs e)
        {
            return;
            //var deleteMaintenanceType = new DeleteMaintenanceType();
            //var result = deleteMaintenanceType.ShowDialog();
            //if (result == true)
            //{
            //    try
            //    {
            //        currentType = null;
            //        type = maintenanceManager.RetrieveMaintenanceTypes("All");
            //        if (currentType == null)
            //        {
            //            currentType = type;
            //        }
            //        dgMaintenanceTypes.ItemsSource = currentType;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }


        /*--------------------------- Ending BrowseMaintenanceType Code --------------------------------*/
        #endregion

        #region Member Code        
        /*--------------------------- Starting BrowseMember Code #BrowseMember --------------------------------*/
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 3/13/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseMemberDoOnStart()
        {
            _memberManager = new MemberManagerMSSQL();
            _selectedMember = new Member();
            populateMembers();
        }

        public void ViewSelectedRecordBrowseMembers()
        {
            var member = (Member)dgMember.SelectedItem;
            var viewMemberForm = new frmAccount(member);
            var result = viewMemberForm.ShowDialog();
            if (result == true)
            {

                try
                {
                    _currentMembers = null;
                    _members = _memberManager.RetrieveAllMembers();

                    if (_currentMembers == null)
                    {
                        _currentMembers = _members;
                    }
                    dgMember.ItemsSource = _currentMembers;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// Populate the members
        /// </summary>
        private void populateMembers()
        {
            try
            {

                _members = _memberManager.RetrieveAllMembers();

                if (_currentMembers == null)
                {
                    _currentMembers = _members;
                }
                dgMember.ItemsSource = _currentMembers;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// When user clicks cancel reload the grids
        /// </summary>
        private void btnFilterBrowseMembers_Click(object sender, RoutedEventArgs e)
        {
            FilterMembers();
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// Filters search for the first name of the member and displays the result
        /// </summary>
        public void FilterMembers()
        {
            try
            {
                _currentMembers = _members;

                if (txtSearch.Text.ToString() != "")
                {
                    _currentMembers = _currentMembers.FindAll(s => s.FirstName.ToLower().Contains(txtSearch.Text.ToString().ToLower()));

                }


                if (btnActive.IsChecked == true)
                {
                    _currentMembers = _currentMembers.FindAll(s => s.Active.Equals(btnInActive.IsChecked));

                }
                else if (btnInActive.IsChecked == true)
                {
                    _currentMembers = _currentMembers.FindAll(s => s.Active.Equals(btnActive.IsChecked));
                }


                //_currentMembers = _currentMembers.FindAll(s => s.Active.Equals(btnActive.IsChecked));





                dgMember.ItemsSource = _currentMembers;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// </summary>
        /// 
        private void btnClearBrowseMembers_Click(object sender, RoutedEventArgs e)
        {
            _currentMembers = _members;
            dgMember.ItemsSource = _currentMembers;
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// </summary>
        private void dgMember_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {


            string headerName = e.Column.Header.ToString();

            if (headerName == "FirstName")
            {
                e.Cancel = true;
            }
            if (headerName == "LastName")
            {
                e.Cancel = true;
            }
            if (headerName == "PhoneNumber")
            {
                e.Cancel = true;
            }
            if (headerName == "Email")
            {
                e.Cancel = true;
            }
            if (headerName == "Password")
            {
                e.Cancel = true;
            }



        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// when click on add member a new empty form will displays
        /// </summary>

        private void btnAddMember_Click(object sender, RoutedEventArgs e)
        {

            var createMemberForm = new frmAccount();
            var formResult = createMemberForm.ShowDialog();

            if (formResult == true)
            {

                try
                {
                    _currentMembers = null;
                    _members = _memberManager.RetrieveAllMembers();

                    if (_currentMembers == null)
                    {
                        _currentMembers = _members;
                    }
                    dgMember.ItemsSource = _currentMembers;

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void dgMember_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            ViewSelectedRecordBrowseMembers();

        }

        private void btnCancelBrowseMembers_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// </summary>

        private void btnDeactivateBrowseMembers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Member)dgMember.SelectedItem).Active)
                {
                    var result = MessageBox.Show("Are you sure you want to deactivate member", "Member deactivating.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("Member has been deactivated");
                    }
                    else if (result == MessageBoxResult.No)
                    {

                    }
                }
                else
                {
                    var result = MessageBox.Show("Are you sure you want to delete member", "Member Account Deleting.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("Member has been deleted");
                    }

                }
                var Member = (Member)dgMember.SelectedItem;




                // Set the record to inactive.
                _memberManager.DeleteMember(Member);

                // Refresh the Member List.
                _currentMembers = null;
                populateMembers();

                // Remove the record from the list of Active Members.
                _currentMembers.Remove(Member);
                dgMember.Items.Refresh();
            }
            catch (NullReferenceException)
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException);
            }
        }



        /*--------------------------- Ending BrowseMember Code --------------------------------*/
        #endregion

        #region Profile Code
        /*--------------------------- Starting Profile Code #Profile--------------------------------*/
        private void ProfileDoOnStart()
        {
            _departmentManager = new DepartmentManager();
            _employeeManager = new EmployeeManager();

            try
            {
                _departments = _departmentManager.GetAllDepartments();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cbxDepartmentProfile.ItemsSource = _departments;
            populateReadOnly();
            readOnlyForm();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/3/19
        /// 
        /// This method establishes what information is read only when someone is reading information about 
        /// and employee.
        /// </summary>
        private void readOnlyForm()
        {
            txtFirstNameProfile.Text = _employee.FirstName;
            txtLastNameProfile.Text = _employee.LastName;
            txtPhoneProfile.Text = _employee.PhoneNumber;
            txtEmailProfile.Text = _employee.Email;
            cbxDepartmentProfile.SelectedItem = _employee.DepartmentID;
            chkActiveProfile.IsChecked = _employee.Active;

            txtFirstNameProfile.IsReadOnly = true;
            txtLastNameProfile.IsReadOnly = true;
            txtPhoneProfile.IsReadOnly = true;
            txtEmailProfile.IsReadOnly = true;
            cbxDepartmentProfile.IsEnabled = false;
            chkActiveProfile.Visibility = Visibility.Hidden;
            lblActiveProfile.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/5/19
        /// 
        /// This sets up the information that can be edited on the form when the user
        /// clicks update
        /// </summary>
        private void editableForm()
        {
            txtFirstNameProfile.IsReadOnly = false;
            txtLastNameProfile.IsReadOnly = false;
            txtPhoneProfile.IsReadOnly = false;
            txtEmailProfile.IsReadOnly = false;
            cbxDepartmentProfile.IsEnabled = true;
            btnSaveProfile.Content = "Submit";
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/5/19
        /// 
        /// This method fills in all of the information for the employee that was chosen in browse.
        /// </summary>
        private void populateReadOnly()
        {
            txtFirstNameProfile.Text = _employee.FirstName;
            txtLastNameProfile.Text = _employee.LastName;
            txtPhoneProfile.Text = _employee.PhoneNumber;
            txtEmailProfile.Text = _employee.Email;
            if (_departments != null)
            {
                cbxDepartmentProfile.SelectedItem = _departments.Find(d => d.DepartmentID == _employee.DepartmentID);
            }
            else
            {
                cbxDepartmentProfile.ItemsSource = new List<string>() { _employee.DepartmentID };
                cbxDepartmentProfile.SelectedIndex = 0;
            }
            
            chkActiveProfile.IsChecked = _employee.Active;
            readOnlyForm();
            btnSaveProfile.Content = "Update";
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The btnSave_Click method is used for saving a new employee or updating 
        /// an existing employee in the system.
        /// </summary>
        private void btnSaveProfile_Click(object sender, RoutedEventArgs e)
        {
            if (btnSaveProfile.Content.Equals("Submit"))
            {
                if (!ValidateInput())
                {
                    return;
                }

                _newEmployee = new Employee()
                {
                    FirstName = txtFirstNameProfile.Text,
                    LastName = txtLastNameProfile.Text,
                    Email = txtEmailProfile.Text,
                    PhoneNumber = txtPhoneProfile.Text,
                    DepartmentID = cbxDepartmentProfile.SelectedItem.ToString()
                };

                try
                {
                    if (_employee == null)
                    {
                        _employeeManager.InsertEmployee(_newEmployee);

                        MessageBox.Show("Employee Created: " +
                            "\nFirst Name: " + _newEmployee.FirstName +
                            "\nLast Name: " + _newEmployee.LastName +
                            "\nEmail: " + _newEmployee.Email +
                            "\nPhone Number: " + _newEmployee.PhoneNumber +
                            "\nDepartment: " + _newEmployee.DepartmentID);
                    }
                    else
                    {
                        _newEmployee.Active = (bool)chkActiveProfile.IsChecked;
                        _employeeManager.UpdateEmployee(_newEmployee, _employee);
                        SetError("");
                        MessageBox.Show("Employee update successful: " +
                            "\nNew First Name: " + _newEmployee.FirstName +
                            "\nNew Last Name: " + _newEmployee.LastName +
                            "\nNew Phone Number: " + _newEmployee.PhoneNumber +
                            "\nNew Email: " + _newEmployee.Email +
                            "\nNew DepartmentID: " + _newEmployee.DepartmentID +
                            "\nOld First Name: " + _employee.FirstName +
                            "\nOld Last Name: " + _employee.LastName +
                            "\nOld Phone Number: " + _employee.PhoneNumber +
                            "\nOld Email: " + _employee.Email +
                            "\nOld DepartmentID: " + _employee.DepartmentID);
                    }

                }
                catch (Exception ex)
                {
                    SetError(ex.Message);
                }

                Close();
            }
            else if (btnSaveProfile.Content.Equals("Update"))
            {
                editableForm();
            }



        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// This is a helper method in order to set error messages to print to the screen for the user to see.
        /// </summary>
        /// <param name="error"></param>
        private void SetError(string error)
        {
            lblError.Content = error;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The ValidateInput goes through every validation method to see if they pass. If they do, then true is returned.
        /// </summary>
        /// <returns></returns>
        private bool ValidateInput()
        {
            if (ValidateFirstName())
            {
                if (ValidateLastName())
                {
                    if (ValidatePhone())
                    {
                        if (ValidateEmail())
                        {
                            if (ValidateDepartmentID())
                            {
                                return true;
                            }
                            else
                            {
                                SetError("You must choose a department for this employee.");
                            }
                        }
                        else
                        {
                            SetError("Invalid email.");
                        }
                    }
                    else
                    {
                        SetError("Invalid phone number.");
                    }
                }
                else
                {
                    SetError("Invalid  last name.");
                }
            }
            else
            {
                SetError("Invalid first name.");
            }
            return false;
        }




        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The ValidateFirstName method makes sure that the FirstName has the correct amount of characters.
        /// </summary>
        /// <returns></returns>
        private bool ValidateFirstName()
        {
            // FirstName can't be a null or empty string
            if (txtFirstNameProfile.Text == null || txtFirstNameProfile.Text == "")
            {
                return false;
            }
            // FirstName must be no more than 50 characters long
            if (txtLastNameProfile.Text.Length >= 1 && txtFirstNameProfile.Text.Length <= 50)
            {
                return true;
            }

            // If FirstName is greater than 50 characters, then the method returns false
            return false;

        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The ValidateLastName method makes sure that the LastName has the correct amount of characters.
        /// </summary>
        /// <returns></returns>
        private bool ValidateLastName()
        {
            // LastName can't be a null or empty string
            if (txtLastNameProfile.Text == null || txtLastNameProfile.Text == "")
            {
                return false;
            }
            // LastName must be no more than 100 characters long
            if (txtLastNameProfile.Text.Length >= 1 && txtLastNameProfile.Text.Length <= 100)
            {
                return true;
            }

            // If LastName is greater than 100 characters, then the method returns false
            return false;

        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The ValidateEmail method makes sure that the Email has the correct amount of characters.
        /// </summary>
        /// <returns></returns>
        private bool ValidateEmail()
        {
            bool validExtension = false;

            // Email can't be a null or empty string
            if (txtEmailProfile.Text == null || txtLastNameProfile.Text == "")
            {
                return false;
            }

            // Email must be no more than 11 characters long
            if (txtEmailProfile.Text.Length >= 1 && txtEmailProfile.Text.Length <= 250 && txtEmailProfile.Text.Contains("."))
            {
                // Email must contain an @ and a .com in order to be an email
                if (txtEmailProfile.Text.Contains("@"))
                {
                    if (txtEmailProfile.Text.Contains("com"))
                    {
                        validExtension = true;
                    }
                    else if (txtEmailProfile.Text.Contains("edu"))
                    {
                        validExtension = true;
                    }
                    else
                    {
                        validExtension = false;
                    }
                }
            }

            // If Email is greater than 250 characters, then the method returns false
            return validExtension;

        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The ValidatePhone method makes sure that the Phone has the correct amount of characters.
        /// </summary>
        /// <returns></returns>
        private bool ValidatePhone()
        {
            // Phone can't be a null or empty string
            if (txtPhoneProfile.Text == null || txtLastNameProfile.Text == "")
            {
                return false;
            }
            // Phone must be no more than 11 characters long
            if (txtPhoneProfile.Text.Length == 11)
            {
                return true;
            }

            // If Phone is greater than 100 characters, then the method returns false
            return false;

        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The ValidateDepartmentID checks to see if an item was selected from the Department drop down combo box
        /// and returns true if there was and false if there wasn't
        /// </summary>
        /// <returns></returns>
        private bool ValidateDepartmentID()
        {
            // The method will return false if nothing was selected.
            if (cbxDepartmentProfile.SelectedItem == null)
            {
                return false;
            }
            else
            {
                // If an item was selected from the Department drop down then method returns true
                return true;
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// This button closes the details screeen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelProfile_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel?", "Leaving Employee detail screen.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }



        /*--------------------------- Ending Profile Code --------------------------------*/

        #endregion

        #region Maintenance Work Order Code
        /*----------------------------- Starting BrowseMaintenanceWorkOrder code #BrowseMaintenanceWorkOrder ---------------------------*/
        private void BrowseMaintenanceWorkOrderDoOnStart()
        {
            _maintenanceWorkOrderManager = new MaintenanceWorkOrderManagerMSSQL();
            refreshAllMaintenanceWorkOrders();
            populateMaintenanceWorkOrders();
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// gets a list of all Work Orders from our database and updates our lists
        /// </summary>
        private void refreshAllMaintenanceWorkOrders()
        {

            try
            {
                _allMaintenanceWorkOrders = _maintenanceWorkOrderManager.RetrieveAllMaintenanceWorkOrders();
                _currentMaintenanceWorkOrders = _allMaintenanceWorkOrders;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// sets the Data Grids Item Source to our current WorkOrders
        /// </summary>
        private void populateMaintenanceWorkOrders()
        {
            dgMaintenanceWorkOrders.ItemsSource = _currentMaintenanceWorkOrders;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when cancel is clicked
        /// </summary>
        private void btnCancelMaintenanceWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when Add is clicked
        /// </summary>
        private void btnAddMaintenanceWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            var createMaintenanceWorkOrder = new CreateMaintenanceWorkOrder(_maintenanceWorkOrderManager);
            createMaintenanceWorkOrder.ShowDialog();
            refreshAllMaintenanceWorkOrders();
            populateMaintenanceWorkOrders();
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when Delete is clicked
        /// </summary>
        private void btnDeleteMaintenanceWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgMaintenanceWorkOrders.SelectedIndex != -1)
            {
                var deleteMaintenanceWorkOrder = new DeactivateMaintenanceWorkOrder(((MaintenanceWorkOrder)dgMaintenanceWorkOrders.SelectedItem), _maintenanceWorkOrderManager);
                deleteMaintenanceWorkOrder.ShowDialog();
                refreshAllMaintenanceWorkOrders();
                populateMaintenanceWorkOrders();
            }
        }


        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when Clear Filters is clicked
        /// </summary>
        private void btnClearFiltersMaintenanceWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            _currentMaintenanceWorkOrders = _allMaintenanceWorkOrders;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when Filter is clicked
        /// </summary>
        private void btnFilterMaintenanceWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented Yet");
        }

        private void filterMaintenanceWorkOrders()
        {
            string status = null;
            try
            {
                status = (cboStatus.SelectedItem).ToString();
                _currentMaintenanceWorkOrders = _allMaintenanceWorkOrders.FindAll(m => m.MaintenanceStatusID == status);

                if (cboStatus.SelectedItem.ToString() != null)
                {
                    _currentMaintenanceWorkOrders = _currentMaintenanceWorkOrders.FindAll(m => m.MaintenanceStatusID == cboStatus.SelectedItem.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// This method takes the current list of MaintenanceWorkOrdesr and filters out the deactive ones 
        /// </summary>
        private void filterActiveOnlyMaintenanceWorkOrder()
        {
            _currentMaintenanceWorkOrders = _currentMaintenanceWorkOrders.FindAll(x => x.Complete == false);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// This method takes the current list of MaintenanceWorkOrders and filters out the active ones
        /// </summary>
        private void filterDeActiveOnlyMaintenanceWorkOrder()
        {
            _currentMaintenanceWorkOrders = _currentMaintenanceWorkOrders.FindAll(x => x.Complete == false);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when a MaintenanceWorkOrder is double clicked
        /// </summary>
        private void dgMaintenanceWorkOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgMaintenanceWorkOrders.SelectedIndex != -1)
            {
                MaintenanceWorkOrder selectedMaintenanceWorkOrder = new MaintenanceWorkOrder();
                try
                {
                    selectedMaintenanceWorkOrder = _maintenanceWorkOrderManager.RetrieveMaintenanceWorkOrder(((MaintenanceWorkOrder)dgMaintenanceWorkOrders.SelectedItem).MaintenanceWorkOrderID);
                    var readUpdateMaintenanceWorkOrder = new CreateMaintenanceWorkOrder(selectedMaintenanceWorkOrder, _maintenanceWorkOrderManager);
                    readUpdateMaintenanceWorkOrder.ShowDialog();
                    refreshAllMaintenanceWorkOrders();
                    populateMaintenanceWorkOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to find that Maintenance Work Order\n" + ex.Message);
                }

            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// The function which runs when the view MaintenanceWorkOrder button is clicked. 
        /// It will launch the CreateMaintenanceWorkOrder window in view mode with the option of updating 
        /// </summary>
        private void btnViewMaintenanceWorkOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgMaintenanceWorkOrders.SelectedIndex != -1)
            {
                MaintenanceWorkOrder selectedMaintenanceWorkOrder = new MaintenanceWorkOrder();
                try
                {
                    selectedMaintenanceWorkOrder = _maintenanceWorkOrderManager.RetrieveMaintenanceWorkOrder(((MaintenanceWorkOrder)dgMaintenanceWorkOrders.SelectedItem).MaintenanceWorkOrderID);
                    var readUpdateMaintenanceWorkOrder = new CreateMaintenanceWorkOrder(selectedMaintenanceWorkOrder, _maintenanceWorkOrderManager);
                    readUpdateMaintenanceWorkOrder.ShowDialog();
                    refreshAllMaintenanceWorkOrders();
                    populateMaintenanceWorkOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to find that Maintenance Work Order\n" + ex.Message);
                }

            }
        }

        private void dgMaintenanceWorkOrders_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
            }

            string headerName = e.Column.Header.ToString();

            if (headerName == "MaintenanceTypeID")
            {
                e.Cancel = true;
            }

            if (headerName == "MaintenanceStatusID")
            {
                e.Cancel = true;
            }
        }
        /*----------------------------- Ending BrowseMaintenanceWorkOrder code ----------------------------------*/
        #endregion

        #region Front Desk Code
            //#FrontDesk
        private void frontDeskDoOnStart()
        {
            luggageManager = new LuggageManager();
            _houseKeepingRequestManager = new HouseKeepingRequestManagerMSSQL();
            refreshAllHouseKeepingRequests();
            populateHouseKeepingRequests();
            setupFrontDeskWindow();
        }

        public void setupFrontDeskWindow()
        {
            try
            {
                dgLuggage.ItemsSource = luggageManager.RetrieveAllLuggage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// sets the Data Grids Item Source to our current HouseKeepingRequests
        /// </summary>
        private void populateHouseKeepingRequests()
        {
            try
            {
                dgHouseKeepingRequests.ItemsSource = _currentHouseKeepingRequests;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }
        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// gets a list of all HouseKeepingRequests from our database and updates our lists
        /// </summary>
        private void refreshAllHouseKeepingRequests()
        {
            try
            {
                _allHouseKeepingRequests = _houseKeepingRequestManager.RetrieveAllHouseKeepingRequests();
                _currentHouseKeepingRequests = _allHouseKeepingRequests;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void btnAddFrontDesk_Click(object sender, RoutedEventArgs e)
        {
            if (tabBellHopService.IsSelected)
            {
                var frmAdd = new AddLuggage(luggageManager, _guestManager);
                if (frmAdd.ShowDialog() == true)
                {
                    MessageBox.Show("Luggage Added.");
                    setupFrontDeskWindow();
                }
            }
            /// <summary>
            /// Author: Dalton Cleveland
            /// Created : 3/27/2019
            /// The function which runs when Add is clicked
            /// </summary>
            else if (tabHousekeepingService.IsSelected)
            {
                var createHouseKeepingRequest = new CreateHouseKeepingRequest(_houseKeepingRequestManager);
                createHouseKeepingRequest.ShowDialog();
                refreshAllHouseKeepingRequests();
                populateHouseKeepingRequests();
            }
            return;
        }

        private void btnUpdateFrontDesk_Click(object sender, RoutedEventArgs e)
        {
            if (tabBellHopService.IsSelected)
            {
                try
                {
                    DataGridRow row = (DataGridRow)dgLuggage.ItemContainerGenerator.ContainerFromIndex(dgLuggage.SelectedIndex);
                    DataGridCell RowColumn = dgLuggage.Columns[0].GetCellContent(row).Parent as DataGridCell;
                    openView(luggageManager.RetrieveLuggageByID(int.Parse(((TextBlock)RowColumn.Content).Text)));
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("You must select a guest before editing.");
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("You must select a guest before editing.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            /// <summary>
            /// Author: Dalton Cleveland
            /// Created : 3/27/2019
            /// The function which runs when the view HouseKeepingRequest button is clicked. 
            /// It will launch the CreateHouseKeepingRequest window in view mode with the option of updating 
            /// </summary>
            else if (tabHousekeepingService.IsSelected)
            {
                if (dgHouseKeepingRequests.SelectedIndex != -1)
                {
                    HouseKeepingRequest selectedHouseKeepingRequest = new HouseKeepingRequest();
                    try
                    {
                        selectedHouseKeepingRequest = _houseKeepingRequestManager.RetrieveHouseKeepingRequest(((HouseKeepingRequest)dgHouseKeepingRequests.SelectedItem).HouseKeepingRequestID);
                        var readUpdateHouseKeepingRequest = new CreateHouseKeepingRequest(selectedHouseKeepingRequest, _houseKeepingRequestManager);
                        readUpdateHouseKeepingRequest.ShowDialog();
                        refreshAllHouseKeepingRequests();
                        populateHouseKeepingRequests();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to find that HouseKeepingRequest\n" + ex.Message);
                    }

                }
            }
        }

        private void btnDelteFrontDesk_Click(object sender, RoutedEventArgs e)
        {
            if (tabBellHopService.IsSelected)
            {
                try
                {
                    DataGridRow row = (DataGridRow)dgLuggage.ItemContainerGenerator.ContainerFromIndex(dgLuggage.SelectedIndex);
                    DataGridCell RowColumn = dgLuggage.Columns[0].GetCellContent(row).Parent as DataGridCell;
                    if (luggageManager.DeleteLuggage(luggageManager.RetrieveLuggageByID(int.Parse(((TextBlock)RowColumn.Content).Text))))
                    {
                        MessageBox.Show("Luggage Deleted.");
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("You must select a guest before deleting.");
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("You must select a guest before deleting.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            /// <summary>
            /// Author: Dalton Cleveland
            /// Created : 3/27/2019
            /// The function which runs when Delete is clicked
            /// </summary>
            else if (tabHousekeepingService.IsSelected)
            {
                if (dgHouseKeepingRequests.SelectedIndex != -1)
                {
                    var deleteHouseKeepingRequest = new DeactivateHouseKeepingRequest(((HouseKeepingRequest)dgHouseKeepingRequests.SelectedItem), _houseKeepingRequestManager);
                    deleteHouseKeepingRequest.ShowDialog();
                    refreshAllHouseKeepingRequests();
                    populateHouseKeepingRequests();
                }
            }
            setupFrontDeskWindow();
        }

        private void openView(Luggage l)
        {
            var frmView = new EditLuggage(luggageManager, l);
            if (frmView.ShowDialog() == true)
            {
                MessageBox.Show("Luggage Updated.");
                setupFrontDeskWindow();
            }
            return;
        }

        private void btnCancelFrontDesk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// The function which runs when a HouseKeepingRequest is double clicked
        /// </summary>
        private void dgHouseKeepingRequests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgHouseKeepingRequests.SelectedIndex != -1)
            {
                HouseKeepingRequest selectedHouseKeepingRequest = new HouseKeepingRequest();
                try
                {
                    selectedHouseKeepingRequest = _houseKeepingRequestManager.RetrieveHouseKeepingRequest(((HouseKeepingRequest)dgHouseKeepingRequests.SelectedItem).HouseKeepingRequestID);
                    var readUpdateHouseKeepingRequest = new CreateHouseKeepingRequest(selectedHouseKeepingRequest, _houseKeepingRequestManager);
                    readUpdateHouseKeepingRequest.ShowDialog();
                    refreshAllHouseKeepingRequests();
                    populateHouseKeepingRequests();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to find that HouseKeepingRequest\n" + ex.Message);
                }

            }
        }

        #endregion

        #region Receiving #Receiving
        private void BrowseReceivingDoOnStart()
        {
            try
            {
                dgReceiving.ItemsSource = _receivingManager.retrieveAllReceivingTickets();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
            }
            

        }

        private void dgReceiving_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (dgReceiving.SelectedIndex < 0)
            {
                MessageBox.Show("You must have a ticket selected");
            }
            else
            {
                ReceivingTicket ticket = (ReceivingTicket)dgReceiving.SelectedItem;
                var viewTicket = new OrderRecieving(ticket);
                viewTicket.ShowDialog();
            }
        }

        private void dgReceiving_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yy";
            }
            if (headerName == "Active")
            {
                e.Cancel = true;
            }
            if (headerName == "ReceivingTicketExceptions")
            {
                e.Cancel = true;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgReceiving.SelectedIndex < 0)
            {
                MessageBox.Show("You must have a ticket selected");
            }
            else
            {
                ReceivingTicket ticket = (ReceivingTicket)dgReceiving.SelectedItem;
                var viewTicket = new OrderRecieving(ticket);
                viewTicket.ShowDialog();
            }
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgReceiving.SelectedIndex < 0)
                {
                    MessageBox.Show("You must have a ticket selected");
                }
                else
                {
                    ReceivingTicket ticket = (ReceivingTicket)dgReceiving.SelectedItem;
                    _receivingSupplierManager.CompleteSupplierOrder(ticket.SupplierOrderID);
                    _receivingManager.deactivateReceivingTicket(ticket.ReceivingTicketID);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR:" + ex.Message);
            }
        }


        #endregion
        #region Offering
        /*--------------------------- Starting BrowseOffering Code #BrowseOffering --------------------------------*/

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 03/28/2019
        /// 
        /// This is where you stick all the code you want to run in your Constructor/Window_Loaded statement
        /// </summary>
        private void BrowseOfferingDoOnStart()
        {
            _offeringManager = new OfferingManager();
            try
            {
                _offeringVms = _offeringManager.RetrieveAllOfferingViewModels();
                _currentOfferingVms = _offeringVms;
                List<string> offeringTypes = new List<string>();
                offeringTypes = _offeringManager.RetrieveAllOfferingTypes();

                cboOfferingType.Items.Clear();
                foreach (var item in offeringTypes)
                {
                    cboOfferingType.Items.Add(item);
                }
                cboOfferingType.Items.Add("All");
                cboOfferingType.SelectedItem = "All";
            }
            catch (Exception)
            {
            }
            dgOfferings.ItemsSource = _offeringVms;
            btnAddOffering.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 03/27/2019
        /// </summary>
        private void NavBarSubHeadersOfferings_Click(object sender, RoutedEventArgs e)
        {
            DisplayPage("BrowseOfferingsPage");
            BrowseOfferingDoOnStart();
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 03/28/2019
        /// Filters the Offering Grid
        /// </summary>
        private void btnFilterOfferings_Click(object sender, RoutedEventArgs e)
        {
            _currentOfferingVms = _offeringVms;
            if (cboOfferingType.SelectedItem.ToString() != "All")
            {
                _currentOfferingVms = _currentOfferingVms.Where(x => x.OfferingTypeID == cboOfferingType.SelectedItem.ToString()).ToList();
            }
            if (txtOfferingName.Text != "")
            {
                _currentOfferingVms = _currentOfferingVms.Where(x => x.OfferingName.ToUpper().Contains(txtOfferingName.Text.ToUpper())).ToList();
            }
            if (txtOfferingDescription.Text != "")
            {
                _currentOfferingVms = _currentOfferingVms.Where(x => x.Description.ToUpper().Contains(txtOfferingDescription.Text.ToUpper())).ToList();
            }
            _currentOfferingVms = _currentOfferingVms.Where(x => x.Active == chkOfferingActive.IsChecked).ToList();
            dgOfferings.ItemsSource = _currentOfferingVms;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 03/28/2019
        /// Clears the filters for offerings
        /// </summary>
        private void BtnClearFiltersOfferings_Click(object sender, RoutedEventArgs e)
        {
            txtOfferingName.Text = "";
            txtOfferingDescription.Text = "";
            cboOfferingType.SelectedItem = "All";
            chkOfferingActive.IsChecked = true;
            dgOfferings.ItemsSource = _offeringVms;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 03/28/2019
        /// Makes the Offering datagrid human-readable
        /// </summary>
        private void DgOfferings_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string header = e.Column.Header.ToString();
            if (e.PropertyType == typeof(Decimal))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "c";
            }
            switch (header)
            {
                case "OfferingID":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "OfferingTypeID":
                    e.Column.Header = "Offering Type";
                    break;
                case "Description":
                    e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
                case "OfferingPrice":
                    e.Column.Header = "Price";
                    break;
                case "OfferingActive":
                    e.Column.Header = "Active";
                    e.Column.DisplayIndex = 5;
                    break;
                case "OfferingName":
                    e.Column.Header = "Offering Name";
                    e.Column.DisplayIndex = 0;
                    e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 03/28/2019
        /// Makes the Offering datagrid human-readable
        /// </summary>
        private void BtnViewOffering_Click(object sender, RoutedEventArgs e)
        {
            if (dgOfferings.SelectedItem != null)
            {
                var offering = _offeringManager.RetrieveOfferingByID(((OfferingVM)dgOfferings.SelectedItem).OfferingID);
                var form = new frmOffering(CrudFunction.Retrieve, offering, _employee);
                var result = form.ShowDialog();
                BrowseOfferingDoOnStart();
            }
            else
            {
                MessageBox.Show("Select an Offering first.");
            }
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 04/03/2019
        /// Opens the details of an offering.
        /// </summary>
        private void DgOfferings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgOfferings.SelectedItem != null)
            {
                var offering = _offeringManager.RetrieveOfferingByID(((OfferingVM)dgOfferings.SelectedItem).OfferingID);
                var form = new frmOffering(CrudFunction.Retrieve, offering, _employee);
                var result = form.ShowDialog();
                BrowseOfferingDoOnStart();
            }
            else
            {
                MessageBox.Show("Select an Offering first.");
            }
        }
        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 04/03/2019
        /// Deletes / Deactivates a record. 
        /// </summary>
        private void BtnDeleteOffering_Click(object sender, RoutedEventArgs e)
        {
            if (dgOfferings.SelectedItem != null && btnDeleteOffering.Content.ToString() == "Delete Offering")
            {
                try
                {
                    _offeringManager.DeactivateOfferingByID(((OfferingVM)dgOfferings.SelectedItem).OfferingID);

                }
                catch (Exception)
                {
                    MessageBox.Show("This operation could not be completed.");
                }
                BrowseOfferingDoOnStart();
            }
            else if (dgOfferings.SelectedItem != null && btnDeleteOffering.Content.ToString() == "Purge Offering")
            {
                try
                {
                    _offeringManager.DeleteOfferingByID(((OfferingVM)dgOfferings.SelectedItem).OfferingID);

                }
                catch (Exception)
                {
                    MessageBox.Show("This operation could not be completed.");
                }
                BrowseOfferingDoOnStart();
            }
            else
            {
                MessageBox.Show("Select an Offering first.");
            }
        }
        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 04/03/2019
        /// Changes button text for different operations.
        /// </summary>
        private void DgOfferings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((OfferingVM)dgOfferings.SelectedItem != null)
            {
                if (((OfferingVM)dgOfferings.SelectedItem).Active)
                {
                    btnDeleteOffering.Content = "Delete Offering";
                }
                else
                {
                    btnDeleteOffering.Content = "Purge Offering";
                }
            }


        }
        #endregion

        /*--------------------------- Ending BrowseOffering Code --------------------------------*/

        #region Browse Shuttle Reservation
        //#ShuttleReservation
        private void BrowseShuttleReservationDoOnStart()
        {
            _shuttleReservation = new ShuttleReservation();
            _shuttleReservationManager = new ShuttleReservationManager();
            refreshShuttleReservation();
        }



        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to refresh browse shuttlereservations.
        /// </summary>
        private void refreshShuttleReservation()
        {
            try
            {
                _shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();

                _currentShuttleReservations = _shuttleReservations;

                dgShuttleReservation.ItemsSource = _currentShuttleReservations;
                dgShuttleReservation.Items.Refresh();
                filterShuttleReservations();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }





        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// //method to call the filter method
        /// </summary>

        private void BtnFilterShuttleReservation_Click(object sender, RoutedEventArgs e)
        {
            filterShuttleReservations();
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// //method to filter the shuttleReservation list
        /// </summary>
        private void filterShuttleReservations()
        {

            IEnumerable<Guest> _currentGuestLists = _shuttleReservations.Select(s => s.Guest);
            IEnumerable<ShuttleReservation> _currentLists = _shuttleReservations;
            try
            {


                if (txtSearchShuttleReservation.Text.ToString() != "")
                {

                    if (txtSearchShuttleReservation.Text != "" && txtSearchShuttleReservation.Text != null)
                    {
                        _currentLists = _currentLists.Where(b => b.PickupLocation.ToLower().Contains(txtSearchShuttleReservation.Text.ToLower())).ToList();


                    }
                }

                if (txtSearchLastNameShuttleReservation.Text.ToString() != "")
                {

                    if (txtSearchLastNameShuttleReservation.Text != "" && txtSearchLastNameShuttleReservation.Text != null)
                    {

                        _currentLists = _currentLists.Where(s => s.Guest.LastName.ToLower().Contains(txtSearchLastNameShuttleReservation.Text.ToLower()));

                    }
                }


                if (dtpSearchDate.Text != null & dtpSearchDate.Text != "")
                {
                    //  DateTime date = TimeZone.Now;
                    _currentLists = _currentLists.Where(d => d.PickupDateTime.ToString().Contains(dtpSearchDate.Text.ToLower())).ToList();
                }


                if (cbActiveShuttleReservation.IsChecked == true && cbDeactiveShuttleReservation.IsChecked == false)
                {
                    _currentLists = _currentLists.Where(b => b.Active == true);
                }
                else if (cbActiveShuttleReservation.IsChecked == false && cbDeactiveShuttleReservation.IsChecked == true)
                {
                    _currentLists = _currentLists.Where(b => b.Active == false);
                }
                else if (cbActiveShuttleReservation.IsChecked == false && cbDeactiveShuttleReservation.IsChecked == false)
                {
                    _currentLists = _currentLists.Where(b => b.Active == false && b.Active == true);
                }

                dgShuttleReservation.ItemsSource = null;

                dgShuttleReservation.ItemsSource = _currentLists;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);


            }

        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// //method to clear the filters
        /// </summary>
        private void BtnClearSetupListShuttleReservation_Click(object sender, RoutedEventArgs e)
        {
            cbDeactiveShuttleReservation.IsChecked = true;
            cbActiveShuttleReservation.IsChecked = true;
            txtSearchShuttleReservation.Text = "";
            txtSearchLastNameShuttleReservation.Text = "";
            dtpSearchDate.Text = "";
            _currentShuttleReservations = _shuttleReservations;

            dgShuttleReservation.ItemsSource = _currentShuttleReservations;

        }




        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// //method to cancel and exit a window
        /// </summary>
        private void BtnCancelShuttleReservation_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to quit?", "Closing Application", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }




        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// //method to open the update  dialog
        /// </summary>
        private void BtnUpdateShuttleReservation_Click(object sender, RoutedEventArgs e)
        {

            if (dgShuttleReservation.SelectedItem != null)
            {
                _shuttleReservation = (ShuttleReservation)dgShuttleReservation.SelectedItem;


                var assign = new ShuttleReservationDetail(_shuttleReservation);
                assign.ShowDialog();
            }
            else
            {

                MessageBox.Show("You must select an item first");

            }
            refreshShuttleReservation();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/10
        /// 
        /// method to open the create shuttlereservation dialog.
        /// </summary>
        private void BtnAddShuttleReservation_Click(object sender, RoutedEventArgs e)
        {


            var detailForm = new ShuttleReservationDetail();

            var result = detailForm.ShowDialog();// need to be added



            if (result == true)
            {

                MessageBox.Show(result.ToString());
            }
            refreshShuttleReservation();

        }
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/10
        /// 
        /// //method to Deactivate shuttleReservation
        /// </summary>
        private void BtnDeactivateShuttleReservation_Click(object sender, RoutedEventArgs e)
        {

            if (dgShuttleReservation.SelectedItem != null)
            {
                ShuttleReservation current = (ShuttleReservation)dgShuttleReservation.SelectedItem;

                try
                {
                    if (current.Active == true)
                    {
                        var result = MessageBox.Show("Are you sure that you want to cancel this shuttle reservation?", "cancel ShuttleReservation", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            _shuttleReservationManager.DeactivateShuttleReservation(current.ShuttleReservationID, current.Active);
                        }
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
            refreshShuttleReservation();

        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/10
        /// 
        /// //method to filter active shuttle reservation
        /// </summary>
        private void CbDeactiveShuttleReservation_Click(object sender, RoutedEventArgs e)
        {
            filterShuttleReservations();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/10
        /// 
        /// //method to filter inactive shuttle reservation
        /// </summary>
        private void CbActiveShuttleReservation_Click(object sender, RoutedEventArgs e)
        {
            filterShuttleReservations();
        }

        #endregion

    }
}
