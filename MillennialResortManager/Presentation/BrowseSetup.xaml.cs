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
    /// Interaction logic for BrowseSetup.xaml
    /// </summary>
    public partial class BrowseSetup : Window
    {
        SetupManager _setupManager;
        List<VMSetup> _setups;
        List<VMSetup> _currentSetups;

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2019-03-28
        /// 
        /// The constructor for the browse setup
        /// </summary>
        public BrowseSetup()
        {
            InitializeComponent();
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
            if(!(txtEventSetup.Text == null || txtEventSetup.Text.Length < 1))
            {
                filterEventTitle(txtEventSetup.Text);
            }

            if(!(dtpSetupDateEntered.Text == null || dtpSetupDateEntered.Text.Length < 1) 
                && (dtpSetupDateRequired.Text == null || dtpSetupDateRequired.Text.Length < 1))
            {
                filterDateEntered(dtpSetupDateEntered.SelectedDate.Value.Date);
            }

            if(!(dtpSetupDateRequired.Text == null || dtpSetupDateRequired.Text.Length < 1) 
                && (dtpSetupDateEntered == null || dtpSetupDateEntered.Text.Length < 1))
            {
                filterDateRequired(dtpSetupDateRequired.SelectedDate.Value.Date);
            }

            if(!(dtpSetupDateEntered.Text == null || dtpSetupDateEntered.Text.Length < 1)
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
    }
}
