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

namespace Presentation
{
    /// <summary>
    /// @Author: Phillip Hansen
    /// 
    /// Test window for showing all EventSponsor records
    /// 
    /// Interaction logic for frmEventSponsorMain.xaml
    /// </summary>
    public partial class frmEventSponsorMain : Window
    {
        EventSponsorManager _eventSponsManager = new EventSponsorManager();
        private List<EventSponsor> _eventSponsors;

        public frmEventSponsorMain()
        {
            InitializeComponent();

            populateEvSponsList();
            dgEventSponsor.IsEnabled = true;
        }
        
        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// When a record is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgEventSponsor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgEventSponsor.SelectedIndex > -1)
            {
                var selectedEvSpons = (EventSponsor)dgEventSponsor.SelectedItem;

                if(selectedEvSpons == null)
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

            if(headerName == "Event ID")
            {
                e.Column.Header = "Event ID";
            }
            else if(headerName == "SponsorID")
            {
                e.Column.Header = "Sponsor ID";
            }

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Event Handler for loading a create record page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEventSpons_Click(object sender, RoutedEventArgs e)
        {
            var createEventSponsForm = new frmAddEventSponsor();
            createEventSponsForm.ShowDialog();

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
            
            if(dgEventSponsor.SelectedIndex > -1)
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
                _eventSponsors = _eventSponsManager.RetrieveAllEvents();
                dgEventSponsor.ItemsSource = _eventSponsors;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not retrieve Event Sponsor List.");
            }
        }
    }
}
