﻿using System;
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
    /// @Author Phillip Hansen
    /// @Created 1/24/2019
    /// 
    /// Interaction logic for frmCreateEvent.xaml
    /// 
    /// Presentation Window for the options in Event Requests
    /// </summary>
    public partial class frmAddEditEvent : Window
    {
        private User _user;
        private LogicLayer.EventManager _eventManager = new LogicLayer.EventManager();
        private Event _oldEvent;
        private Event _newEvent;
        private EventTypeManager _eventTypeManager = new EventTypeManager();
        
        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// When adding a new Event Request
        /// Pass the User ID to automatically add the ID in the field for 'creating' records
        /// </summary>
        /// <param name="user"></param>
        public frmAddEditEvent(User user)
        {
            _user = user;
            InitializeComponent();
            setEditable(user);

            this.Title = "New Event Record";
            this.btnEventAction1.Content = "Create";
            this.btnEventAction2.Visibility = Visibility.Hidden;

            //When creating a new Event, editable() method enables Approve check box,
            //Should be disabled only when creating new Events
            chkEventAppr.IsEnabled = false;

            this.txtEventSponsorID.Text = "0";
            
        }
        
        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// When editing an event record
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oldEvent"></param>
        public frmAddEditEvent(User user, Event oldEvent)
        {
            InitializeComponent();

            _user = user;
            _oldEvent = oldEvent;
            setOldEvent();
            //Overloads setEditable
            this.Title = "Edit Event Record " + _oldEvent.EventTitle;

            this.btnEventAction1.Content = "Save";
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// When the window loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Code only if the window being loaded is 'not' for a new event
            if(this.Title != "New Event Record")
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

                this.btnDeleteEvent.Visibility = Visibility.Hidden;
                this.btnEventAction2.Visibility = Visibility.Hidden;

                setEditable(_user);

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
        /// @Author Phillip Hansen
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
        /// @Author Phillip Hansen
        /// 
        /// Sets the window controls to be editable by the user
        /// </summary>
        /// <param name="user"></param>
        private void setEditable(User user)
        {
            //Event ID never changes
            txtEventID.IsEnabled = false;

            txtEventTitle.IsEnabled = true;
            txtEventEmployee.Text = _user.UserID.ToString();

            txtReqNumGuest.IsEnabled = true;
            txtEventLocation.IsEnabled = true;
            txtDescription.IsEnabled = true;
            
            cboEventType.IsReadOnly = false;   /*<-- Use if RetrieveEventTypes() works?*/

            dateEventStart.IsEnabled = true;
            dateEventEnd.IsEnabled = true;
            
            chkEventKids.IsEnabled = true;
            chkEventSpons.IsEnabled = true;
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Sets the window controls to be editable
        /// Needs the old event field data to display on the window
        /// <param name="user"></param>
        /// <param name="_oldEvent"></param>
        /// </summary>
        private void setEditable(User user, Event _oldEvent)
        {
            //Event ID never changes
            txtEventID.IsEnabled = false;

            txtEventTitle.IsEnabled = true;
            txtEventEmployee.Text = _oldEvent.EmployeeID.ToString();

            txtReqNumGuest.IsEnabled = true;
            txtEventLocation.IsEnabled = true;
            txtDescription.IsEnabled = true;
            
            cboEventType.IsEnabled = true;   /*<-- Use if RetrieveEventTypes() works?*/

            dateEventStart.IsEnabled = true;
            dateEventEnd.IsEnabled = true;

            chkEventAppr.IsEnabled = true;
            chkEventKids.IsEnabled = true;
            chkEventSpons.IsEnabled = true;
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Sets the window controls to be read only
        /// </summary>
        private void setReadOnly()
        {
            //Event ID never changes
            txtEventID.IsEnabled = false;

            txtEventTitle.IsEnabled = false;
            //txtEventEmployee.IsEnabled = true;
            txtEventEmployee.Text = _user.UserID.ToString();

            txtReqNumGuest.IsEnabled = false;
            txtEventLocation.IsEnabled = false;
            txtDescription.IsEnabled = false;

            //txtEventType.IsEnabled = true;
            cboEventType.IsEnabled = false;   /*<-- Use if RetrieveEventTypes() works?*/

            dateEventStart.IsEnabled = false;
            dateEventEnd.IsEnabled = false;

            chkEventAppr.IsEnabled = false;
            chkEventKids.IsEnabled = false;
            chkEventSpons.IsEnabled = false;
        }

        /// <summary>
        /// Sets the old event's specific fields into the correct places
        /// </summary>
        private void setOldEvent()
        {
            txtEventID.Text = _oldEvent.EventID.ToString();

            txtEventTitle.Text = _oldEvent.EventTitle;
            txtDescription.Text = _oldEvent.Description;
            txtEventEmployee.Text = _oldEvent.EmployeeID.ToString();
            txtEventLocation.Text = _oldEvent.Location;
            txtEventSponsorID.Text = _oldEvent.SponsorID.ToString();
            txtReqNumGuest.Text = _oldEvent.NumGuests.ToString();

            if(_oldEvent.KidsAllowed == true)
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

            cboEventType.SelectedItem = _oldEvent.EventTypeID;

            dateEventStart.SelectedDate = _oldEvent.EventStartDate;
            dateEventEnd.SelectedDate = _oldEvent.EventEndDate;
            
        }

        /// <summary>
        /// @Author Phillip Hansen
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
                else if (!int.TryParse(txtReqNumGuest.Text, out int aNumber) || (!int.TryParse(txtEventSponsorID.Text, out aNumber)))
                {
                    MessageBox.Show("Numbers only!");
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
                    if(this.Title == "New Event Record")
                    {
                        _newEvent = new Event
                        {
                            EventTitle = txtEventTitle.Text,
                            EmployeeID = int.Parse(txtEventEmployee.Text),
                            EventTypeID = cboEventType.SelectedItem.ToString(),
                            Description = txtDescription.Text,
                            EventStartDate = dateEventStart.SelectedDate.Value,
                            EventEndDate = dateEventEnd.SelectedDate.Value,
                            KidsAllowed = chkEventKids.IsChecked.Value,
                            NumGuests = int.Parse(txtReqNumGuest.Text),
                            Location = txtEventLocation.Text,
                            Sponsored = chkEventSpons.IsChecked.Value,
                            SponsorID = int.Parse(txtEventSponsorID.Text),
                            Approved = chkEventAppr.IsChecked.Value
                        };
                    }
                    //If a record is being edited (or in a specific case deleted) the EventID in the text box place holder must be captured
                    else
                    {
                        _newEvent = new Event
                        {
                            EventID = int.Parse(txtEventID.Text),
                            EventTitle = txtEventTitle.Text,
                            EmployeeID = int.Parse(txtEventEmployee.Text),
                            EventTypeID = cboEventType.SelectedItem.ToString(),
                            Description = txtDescription.Text,
                            EventStartDate = dateEventStart.SelectedDate.Value,
                            EventEndDate = dateEventEnd.SelectedDate.Value,
                            KidsAllowed = chkEventKids.IsChecked.Value,
                            NumGuests = int.Parse(txtReqNumGuest.Text),
                            Location = txtEventLocation.Text,
                            Sponsored = chkEventSpons.IsChecked.Value,
                            SponsorID = int.Parse(txtEventSponsorID.Text),
                            Approved = chkEventAppr.IsChecked.Value
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
        /// @Author Phillip Hansen
        /// 
        /// For saving or creating the event on the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEventAction1_Click(object sender, RoutedEventArgs e)
        {
            if(this.Title == "New Event Record")
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
            if(this.btnEventAction1.Content.ToString() == "Save")
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
        /// @Author Phillip Hansen
        /// 
        /// Only for interchanging the content in the Sponser Name in confliction with the check box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkEventSpons_Click(object sender, RoutedEventArgs e)
        {
            if(chkEventSpons.IsChecked == true)
            {
                txtEventSponsorID.Text = "Sponosr ID Only";
                txtEventSponsorID.IsEnabled = true;
            }

            if (chkEventSpons.IsChecked == false)
            {
                txtEventSponsorID.Text = "0";
                txtEventSponsorID.IsEnabled = false;
            }
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// When the delete button is clicked, passes event into a new confirmation window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            //Event must 'not' be approved to be deleted from data table
            if(chkEventAppr.IsChecked == true)
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
        /// @Author Phillip Hansen
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


            setEditable(_user, _oldEvent);
        }
    }
}