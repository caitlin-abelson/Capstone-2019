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
using EventManager = LogicLayer.EventManager;

namespace WpfPresentation
{
    /// <summary>
    /// @Author Phillip Hansen
    /// 
    /// Test window for showing all Event Requests
    /// 
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class frmEventMain : Window
    {
        private User _user;
        EventManager _eventManager = new EventManager();
        private EventTypeManager _eventTypeManager = new EventTypeManager();
        private List<Event> _events;
        public frmEventMain(User user)
        {
            _user = user;
            InitializeComponent();

            populateEvents();
            dgEvents.IsEnabled = true;
        }
        
        
        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// When an event record is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgEvents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgEvents.SelectedIndex > -1)
            {
                var selectedEvent = (Event)dgEvents.SelectedItem;
                
                if (selectedEvent == null)
                {
                    MessageBox.Show("No Event Selected!");
                }
                else
                {
                    var detailA = new frmAddEditEvent(_user, selectedEvent);
                    detailA.ShowDialog();
                    if(detailA.DialogResult == true)
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
        /// @Author Phillip Hansen
        /// 
        /// Code for when the 'create' button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreateEvReq_Click(object sender, RoutedEventArgs e)
        {
            //The Form requires the User's ID for a field in the record
            var addEventReq = new frmAddEditEvent(_user);
            var result = addEventReq.ShowDialog();
            if(result == true)
            {
                populateEvents();
            }
        }

        /// <summary>
        /// @Author Phillip Hansen
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
            if (headerName == "SponsorID")
            {
                e.Cancel = true;
            }
            if(headerName == "EventTitle")
            {
                e.Column.Header = "Event Title";
            }
            if(headerName == "EmployeeName")
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
            if(headerName == "EventEndDate")
            {
                e.Column.Header = "End Date";
            }
            if(headerName == "KidsAllowed")
            {
                e.Column.Header = "Kids Allowed?";
            }
            if(headerName == "NumGuests")
            {
                e.Column.Header = "Max Guests";
            }
            if(headerName == "Sponsored")
            {
                e.Column.Header = "Sponsored?";
            }
            if(headerName == "SponsorName")
            {
                e.Column.Header = "Sponsor Name";
            }
            if(headerName == "Approved")
            {
                e.Column.Header = "Approved?";
            }
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Method for populating the events
        /// </summary>
        private void populateEvents()
        {
            try
            {
                _events = _eventManager.RetrieveAllEvents();
                dgEvents.ItemsSource = _events;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not retrieve the list of Event Requests.");
            }
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Closes the window if the 'cancel' button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelEventMain_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
