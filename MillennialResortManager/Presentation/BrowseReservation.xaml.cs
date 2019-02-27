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
    /// Author: Matt LaMarche
    /// Created : 1/31/2019
    /// Interaction logic for BrowseReservation.xaml
    /// </summary>
    public partial class BrowseReservation : Window
    {
        List<VMBrowseReservation> _allReservations;
        List<VMBrowseReservation> _currentReservations;
        ReservationManagerMSSQL _reservationManager;
        private Employee _employee;
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// Updated : 2/13/2019 by Matt LaMarche
        /// This constructor sets up the Reservations screen. 
        /// This will be the only place we create an instance of ReservationManager
        /// This will populate the list upon loading
        /// </summary>
        public BrowseReservation(Employee employee)
        {
            _employee = employee;
            InitializeComponent();
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
        /// The function which runs when cancel is clicked
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
        private void btnDelete_Click(object sender, RoutedEventArgs e)
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
        private void btnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            _currentReservations = _allReservations;
            populateReservations();
            dtpDateSearch.Text = "";
            txtEmail.Text = "";
            txtLastName.Text = "";
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/31/2019
        /// The function which runs when Filter is clicked
        /// </summary>
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            //Check if Email box is populated
            if (!(txtEmail.Text == null || txtEmail.Text.Length < 1))
            {
                //Not null and strings length is greater than 0
                filterEmail(txtEmail.Text);
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
            _currentReservations = _currentReservations.FindAll(x=>x.Active);
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
            _currentReservations = _currentReservations.FindAll(x=>x.LastName.Contains(lastName));
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This method takes the current list of reservations and filters out Reservations whose Arrival dates are before the given date
        /// </summary>
        /// <param name="date">The date which we want to compare our arrival dates against</param>
        private void filterByArrivalDate(DateTime date)
        {
            _currentReservations = _currentReservations.FindAll(x=>x.ArrivalDate.CompareTo(date) >= 0);
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This method takes the current list of reservations and filters out Reservations whose Departure dates are after the given date
        /// </summary>
        /// <param name="date">The date which we want to compare our arrival dates against</param>
        private void filterByDepartureDate(DateTime date)
        {
            _currentReservations = _currentReservations.FindAll(x=>x.DepartureDate.CompareTo(date) <=0);
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This method takes the current list of reservations and filters out Reservations whose given date does not fall within the Reservations Arrival date and Departure date
        /// </summary>
        /// <param name="date"></param>
        private void filterBySpecificDate(DateTime date)
        {
            _currentReservations = _currentReservations.FindAll(x=>x.ArrivalDate.Date.CompareTo(date) <=0 && x.DepartureDate.Date.CompareTo(date) >= 0);
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
            _currentReservations = _currentReservations.FindAll(x=>!((x.ArrivalDate.CompareTo(startDate) < 0 && x.DepartureDate.CompareTo(startDate) < 0) || (x.ArrivalDate.CompareTo(endDate) > 0 && x.DepartureDate.CompareTo(endDate) > 0)));
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
    }
}
