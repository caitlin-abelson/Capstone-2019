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
using Presentation;

namespace WpfPresentation
{
    /// <summary>
    /// @Author: Phillip Hansen
    /// @Created 1/24/2019
    /// 
    /// Interaction logic for frmCreateEvent.xaml
    /// 
    /// Presentation Window for the options in Event Requests
    /// </summary>
    public partial class frmAddEditEvent : Window
    {
        private Employee _employee;
        private LogicLayer.EventManager _eventManager = new LogicLayer.EventManager();
        private Event _oldEvent;
        private Event _newEvent;
        private EventTypeManager _eventTypeManager = new EventTypeManager();

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Updated by Phillip Hansen on 3/8/2019
        /// Updated presentation functionality to match Data Dictionary
        /// 
        /// When adding a new Event Request
        /// Pass the User ID to automatically add the ID in the field for 'creating' records
        /// </summary>
        /// <param name="user"></param>
        public frmAddEditEvent(Employee employee)
        {
            _employee = employee;
            InitializeComponent();
            setEditable(employee);

            this.Title = "New Event Record";
            this.btnEventAction1.Content = "Create";
            this.btnEventAction2.Visibility = Visibility.Hidden;

            //When creating a new Event, editable() method enables Approve check box and Sponsored check box,
            //Should be disabled only when creating new Events
            chkEventSpons.IsEnabled = false;
            chkEventAppr.IsEnabled = false;

            //this.txtEvent//SponsorID.Text = "0";

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// When editing an event record
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oldEvent"></param>
        public frmAddEditEvent(Employee employee, Event oldEvent)
        {
            InitializeComponent();

            _employee = employee;
            _oldEvent = oldEvent;
            setOldEvent();
            //Overloads setEditable
            this.Title = "Edit Event Record " + _oldEvent.EventTitle;

            this.btnEventAction1.Content = "Save";
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// When the window loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Code only if the window being loaded is 'not' for a new event
            if (this.Title != "New Event Record")
            {

                this.btnDeleteEvent.Visibility = Visibility.Visible;
                this.btnEventAction2.Visibility = Visibility.Visible;

                setReadOnly();

                this.btnEventAction2.Content = "Edit";
                this.btnEventAction1.IsEnabled = false;
                this.btnDeleteEvent.IsEnabled = true;

                try
                {
                    if (cboEventType.Items.Count == 0)
                    {
                        var eventTypes = _eventTypeManager.RetrieveEventTypes();
                        foreach (var item in eventTypes)
                        {
                            cboEventType.Items.Add(item);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Event Types not found.");
                }
            }
            //Code if the window loaded is for a new event to be created
            else
            {
                this.chkEventAppr.IsEnabled = false;
                this.txtSeatsRemaining.IsEnabled = true;
                this.chkEventSpons.IsEnabled = false;

                this.btnDeleteEvent.Visibility = Visibility.Hidden;
                this.btnEventAction2.Visibility = Visibility.Hidden;

                setEditable(_employee);

                try
                {
                    if (cboEventType.Items.Count == 0)
                    {
                        var eventTypes = _eventTypeManager.RetrieveEventTypes();
                        foreach (var item in eventTypes)
                        {
                            cboEventType.Items.Add(item);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Event Types not found.");
                }
            }

        } //End of loaded method

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// When the 'Cancel' Button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEventCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Sets the window controls to be editable by the user
        /// </summary>
        /// <param name="user"></param>
        private void setEditable(Employee employee)
        {
            //Event ID never changes
            txtEventID.IsEnabled = false;

            txtEventTitle.IsEnabled = true;
            txtEventEmployee.Text = _employee.EmployeeID.ToString();

            txtReqNumGuest.IsEnabled = true;
            txtEventLocation.IsEnabled = true;
            txtDescription.IsEnabled = true;

            cboEventType.IsReadOnly = false;

            dateEventStart.IsEnabled = true;
            dateEventEnd.IsEnabled = true;

            chkEventKids.IsEnabled = true;
            chkEventSpons.IsEnabled = true;
            chkEventPublic.IsEnabled = true;

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Sets the window controls to be editable
        /// Needs the old event field data to display on the window
        /// <param name="user"></param>
        /// <param name="_oldEvent"></param>
        /// </summary>
        private void setEditable(Employee employee, Event _oldEvent)
        {
            //Event ID never changes
            txtEventID.IsEnabled = false;
            //Offering Price never changes
            txtEventPrice.IsEnabled = false;

            txtEventTitle.IsEnabled = true;
            txtEventEmployee.Text = _oldEvent.EmployeeID.ToString();

            txtReqNumGuest.IsEnabled = true;
            txtSeatsRemaining.IsEnabled = true;
            txtEventLocation.IsEnabled = true;
            txtDescription.IsEnabled = true;

            cboEventType.IsEnabled = true;   /*<-- Use if RetrieveEventTypes() works?*/

            dateEventStart.IsEnabled = true;
            dateEventEnd.IsEnabled = true;

            chkEventAppr.IsEnabled = true;
            chkEventKids.IsEnabled = true;
            chkEventSpons.IsEnabled = true;
            chkEventPublic.IsEnabled = true;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Updated: 3/1/2019 by Phillip Hansen
        /// Updated fields to match new definition in Data Dictionary
        /// 
        /// Sets the window controls to be read only
        /// </summary>
        private void setReadOnly()
        {
            //Event ID never changes
            txtEventID.IsEnabled = false;

            txtEventPrice.IsEnabled = false;

            txtEventTitle.IsEnabled = false;
            //txtEventEmployee.IsEnabled = true;
            txtEventEmployee.Text = _employee.EmployeeID.ToString();

            txtReqNumGuest.IsEnabled = false;
            txtSeatsRemaining.IsEnabled = false;
            txtEventLocation.IsEnabled = false;
            txtDescription.IsEnabled = false;

            //txtEventType.IsEnabled = true;
            cboEventType.IsEnabled = false;   /*<-- Use if RetrieveEventTypes() works?*/

            dateEventStart.IsEnabled = false;
            dateEventEnd.IsEnabled = false;

            chkEventAppr.IsEnabled = false;
            chkEventKids.IsEnabled = false;
            chkEventSpons.IsEnabled = false;
            chkEventPublic.IsEnabled = false;
        }

        /// <summary>
        /// Sets the old event's specific fields into the correct places
        /// </summary>
        private void setOldEvent()
        {
            txtEventID.Text = _oldEvent.EventID.ToString();
            txtEventOfferingID.Text = _oldEvent.OfferingID.ToString();
            txtEventTitle.Text = _oldEvent.EventTitle;
            txtEventPrice.Text = _oldEvent.Price.ToString();
            txtDescription.Text = _oldEvent.Description;
            txtEventEmployee.Text = _oldEvent.EmployeeID.ToString();
            txtEventLocation.Text = _oldEvent.Location;
            //txtEvent//SponsorID.Text = _oldEvent.//SponsorID.ToString();
            txtReqNumGuest.Text = _oldEvent.NumGuests.ToString();
            txtSeatsRemaining.Text = _oldEvent.SeatsRemaining.ToString();

            if (_oldEvent.KidsAllowed == true)
            {
                chkEventKids.IsChecked = true;
            }
            if (_oldEvent.Approved == true)
            {
                chkEventAppr.IsChecked = true;
            }
            if (_oldEvent.Sponsored == true)
            {
                chkEventSpons.IsChecked = true;
            }
            if (_oldEvent.PublicEvent == true)
            {
                chkEventPublic.IsChecked = true;
            }

            cboEventType.SelectedItem = _oldEvent.EventTypeID;

            dateEventStart.SelectedDate = _oldEvent.EventStartDate;
            dateEventEnd.SelectedDate = _oldEvent.EventEndDate;

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Updated: 3/7/2019 by Phillip Hansen
        /// Updated fields to match new definition in Data Dictionary
        /// 
        /// Captures the fields as a new object
        /// Also validates the input necessary for each field
        /// </summary>
        private void captureEvent()
        {
            try
            {
                // Method will error check first
                if (txtEventTitle.Text == null || txtEventTitle.Text.Length < 1 || txtEventTitle.Text.Length > 50)
                {
                    MessageBox.Show("Event Title must be between 1 and 50 characters!");
                    return;
                }
                else if (!int.TryParse(txtReqNumGuest.Text, out int aNumber) || (!decimal.TryParse(txtEventPrice.Text, out decimal bNumber)) || (!int.TryParse(txtSeatsRemaining.Text, out aNumber))/*(!int.TryParse(txtEvent//SponsorID.Text, out aNumber))*/)
                {
                    MessageBox.Show("Numbers only!");
                    return;
                }
                else if (int.Parse(txtSeatsRemaining.Text) > int.Parse(txtReqNumGuest.Text))
                {
                    MessageBox.Show("Seats Remaining must be less or equal to the Number of Guests allowed!");
                    return;
                }
                else if (dateEventEnd.SelectedDate < dateEventStart.SelectedDate)
                {
                    MessageBox.Show("The date selection must start before it ends!");
                    return;
                }
                else if (dateEventStart.SelectedDate < DateTime.Now)
                {
                    MessageBox.Show("The date selection must be after today!");
                    return;
                }

                //Data is captured once there are no errors
                else
                {
                    //If a new record is being created, the place holder for 'EventID' will be blank and would cause errors if captured in its state
                    if (this.Title == "New Event Record")
                    {
                        _newEvent = new Event
                        {
                            EventTitle = txtEventTitle.Text,
                            Price = decimal.Parse(txtEventPrice.Text),
                            EmployeeID = int.Parse(txtEventEmployee.Text),
                            EventTypeID = cboEventType.SelectedItem.ToString(),
                            Description = txtDescription.Text,
                            EventStartDate = dateEventStart.SelectedDate.Value,
                            EventEndDate = dateEventEnd.SelectedDate.Value,
                            KidsAllowed = chkEventKids.IsChecked.Value,
                            NumGuests = int.Parse(txtReqNumGuest.Text),
                            SeatsRemaining = int.Parse(txtSeatsRemaining.Text),
                            Location = txtEventLocation.Text,
                            Sponsored = chkEventSpons.IsChecked.Value,
                            Approved = chkEventAppr.IsChecked.Value,
                            PublicEvent = chkEventPublic.IsChecked.Value
                        };
                    }
                    //If a record is being edited (or in a specific case deleted) the EventID in the text box place holder must be captured
                    else
                    {
                        _newEvent = new Event
                        {
                            EventID = int.Parse(txtEventID.Text),
                            OfferingID = int.Parse(txtEventOfferingID.Text),
                            EventTitle = txtEventTitle.Text,
                            Price = decimal.Parse(txtEventPrice.Text),
                            EmployeeID = int.Parse(txtEventEmployee.Text),
                            EventTypeID = cboEventType.SelectedItem.ToString(),
                            Description = txtDescription.Text,
                            EventStartDate = dateEventStart.SelectedDate.Value,
                            EventEndDate = dateEventEnd.SelectedDate.Value,
                            KidsAllowed = chkEventKids.IsChecked.Value,
                            NumGuests = int.Parse(txtReqNumGuest.Text),
                            SeatsRemaining = int.Parse(txtSeatsRemaining.Text),
                            Location = txtEventLocation.Text,
                            Sponsored = chkEventSpons.IsChecked.Value,
                            ////SponsorID = int.Parse(txtEvent//SponsorID.Text),
                            Approved = chkEventAppr.IsChecked.Value,
                            PublicEvent = chkEventPublic.IsChecked.Value
                        };
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not capture the event.");
            }

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// For saving or creating the event on the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEventAction1_Click(object sender, RoutedEventArgs e)
        {
            if (this.Title == "New Event Record")
            {
                //Captures the input within the fields
                captureEvent();

                //Cannot be approvable if it is being created
                chkEventAppr.IsEnabled = false;
                chkEventAppr.IsChecked = false;

                //Hides the Delete button when creating a new event
                this.btnDeleteEvent.Visibility = Visibility.Hidden;
                this.btnEventAction1.Visibility = Visibility.Hidden;
                this.btnEventAction2.Visibility = Visibility.Hidden;

                try
                {
                    _eventManager.CreateEvent(_newEvent);
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\nInsert for new event has failed.");
                }
            }
            if (this.btnEventAction1.Content.ToString() == "Save")
            {
                this.btnEventAction2.IsEnabled = true;

                captureEvent();

                //Make sure the delete button is visible when editing
                this.btnDeleteEvent.Visibility = Visibility.Visible;

                try
                {

                    _eventManager.UpdateEvent(_oldEvent, _newEvent);
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\nUpdate for new event has failed.");
                }
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Only for interchanging the content in the Sponser Name in confliction with the check box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkEventSpons_Click(object sender, RoutedEventArgs e)
        {
            //Sponsor elements can only be enabled if the event is sponsored
            if (chkEventSpons.IsChecked == true)
            {
                txtEventSponsor.IsEnabled = true;
                btnEventSponsor.IsEnabled = true;

                //Force the user to go through the process of selecting an existing sponsor
                btnEventAction1.IsEnabled = false;
            }
            else
            {
                txtEventSponsor.Text = null;
                txtEventSponsor.IsEnabled = false;
                btnEventSponsor.IsEnabled = false;
            }
        }


        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// When the delete button is clicked, passes event into a new confirmation window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            //Event must 'not' be approved to be deleted from data table
            if (chkEventAppr.IsChecked == true)
            {
                MessageBox.Show("Event cannot be deleted if the event is approved!");
            }
            else
            {
                //Closes window after 'delete' is chosen
                this.DialogResult = true;

                captureEvent();
                var deleteEvent = new frmEventDeleteConfirmation(_newEvent);
                var result = deleteEvent.ShowDialog();
            }

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Button to enable editing the records of a specific event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEventAction2_Click(object sender, RoutedEventArgs e)
        {
            this.btnEventAction1.Visibility = Visibility.Visible;
            this.btnEventAction1.IsEnabled = true;

            this.btnDeleteEvent.IsEnabled = false;
            this.btnEventAction2.IsEnabled = false;


            setEditable(_employee, _oldEvent);
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Button grabs text from the text box, displays a window that:
        /// 1) Opens a window with a grid for Sponsors
        /// 2) Grid is filtered with the input from the textbox and that only
        /// 3) If the requested sponsor exists, or is created if not, the sponsor should be selected and make the fields valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEventSponsor_Click(object sender, RoutedEventArgs e)
        {

            if (txtEventSponsor != null)
            {
                //Create a new SponsorMainWindow that passes in the filter value
                var filterSponsor = new SponsorMainWindow(txtEventSponsor.Text.ToString());
                var result = filterSponsor.ShowDialog();

                try
                {
                    if (filterSponsor.DialogResult == true)
                    {
                        //If the dialogResult of said window is true, do not allow
                        //user to modify the input fields (unless they wish to redo the process)
                        
                        txtEventSponsor.IsEnabled = false;
                        btnEventSponsor.IsEnabled = false;
                        
                    }
                    else
                    {
                        //If the window was not successful, then the event will not associate with a sponsor
                        chkEventSpons.IsChecked = false;
                        txtEventSponsor.Text = null;
                        txtEventSponsor.IsEnabled = false;
                        btnEventSponsor.IsEnabled = false;
                        MessageBox.Show("If an Event is sponsored, the Sponsor must exist in the Database!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\nCould not retrieve associated sponsor");
                }
                finally
                {
                    //No matter how the process goes, works or failed, the create button should be re-enabled
                    btnEventAction1.IsEnabled = true;
                }
                
            }
        }
    }
}
