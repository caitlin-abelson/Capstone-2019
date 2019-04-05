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
    /// Interaction logic for FrontDeskTemplate.xaml
    /// </summary>
    public partial class FrontDeskTemplate : Window
    {
        private LuggageManager luggageManager = new LuggageManager();
        private GuestManager guestManager = new GuestManager();
        private List<HouseKeepingRequest> _allHouseKeepingRequests;
        private List<HouseKeepingRequest> _currentHouseKeepingRequests;
        private HouseKeepingRequestManagerMSSQL _houseKeepingRequestManager;
        public FrontDeskTemplate()
        {
            InitializeComponent();
            _houseKeepingRequestManager = new HouseKeepingRequestManagerMSSQL();
            refreshAllHouseKeepingRequests();
            populateHouseKeepingRequests();
        }

        public void setupWindow()
        {
            dgLuggage.ItemsSource = luggageManager.RetrieveAllLuggage();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setupWindow();
        }
        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// sets the Data Grids Item Source to our current HouseKeepingRequests
        /// </summary>
        private void populateHouseKeepingRequests()
        {
            dgHouseKeepingRequests.ItemsSource = _currentHouseKeepingRequests;
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
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tabBellHopService.IsSelected) {
                var frmAdd = new AddLuggage(luggageManager, guestManager);
                if (frmAdd.ShowDialog() == true)
                {
                    MessageBox.Show("Luggage Added.");
                    setupWindow();
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (tabBellHopService.IsSelected) {
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

        private void btnDelte_Click(object sender, RoutedEventArgs e)
        {
            if (tabBellHopService.IsSelected) {
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
            setupWindow();
        }

        private void openView(Luggage l)
        {
            var frmView = new EditLuggage(luggageManager, l);
            if (frmView.ShowDialog() == true)
            {
                MessageBox.Show("Luggage Updated.");
                setupWindow();
            }
            return;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
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
    }
}
