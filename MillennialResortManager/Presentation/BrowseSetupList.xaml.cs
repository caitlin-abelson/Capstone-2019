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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for SetupList.xaml
    /// </summary>
    public partial class BrowseSetupList : Window
    {


        SetupListManager _setupListManager;
        List<VMSetupList> _setupLists;
        List<VMSetupList> _currentSetupLists;

        private VMSetup vmsetup;




        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// Default constructor:  setuplist.
        /// </summary>
        public BrowseSetupList()
        {

            InitializeComponent();
            _setupListManager = new SetupListManager();

            refreshAllSetupLists();
            populateSetupLists();

        }

        public BrowseSetupList(VMSetup vmsetup)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            _setupListManager = new SetupListManager();

            this.vmsetup = vmsetup;
            populateSetupLists(vmsetup);
        }

        private void populateSetupLists(VMSetup vmsetup)
        {
            try
            {
                _setupLists = _setupListManager.SelectAllVMSetupLists();
                _currentSetupLists = null;
                if (_currentSetupLists == null)
                {
                    _currentSetupLists = new List<VMSetupList>();
                    foreach (var item in _setupLists)
                    {
                        if (item.SetupID == vmsetup.SetupID)
                        {
                            _currentSetupLists.Add(item);
                        }
                    }
                }
                dgSetupList.ItemsSource = _currentSetupLists;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        /// <summary>
        /// Author: Caitlin Abelson
        /// Date: 2019/3/14
        /// 
        /// Populates the data grid with a fresh list of the current list of the setup lists.
        /// </summary>
        private void populateSetupLists()
        {
            try
            {
                _setupLists = _setupListManager.SelectAllVMSetupLists();
                if (_currentSetupLists == null)
                {
                    _currentSetupLists = _setupLists;
                }
                dgSetupList.ItemsSource = _currentSetupLists;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Date: 2019/3/14
        /// 
        /// Refreshes the current list to show anything new that has been added or taken away.
        /// </summary>
        private void refreshAllSetupLists()
        {
            try
            {

                _setupLists = _setupListManager.SelectAllVMSetupLists();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _currentSetupLists = _setupLists;
        }


        private void DgSetupList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetupList chosenSetupList = new SetupList();


            try
            {
                chosenSetupList = _setupListManager.SelectSetupList(((VMSetupList)dgSetupList.SelectedItem).SetupListID);
                var readSetupList = new SetupListDetail(chosenSetupList, ((VMSetupList)dgSetupList.SelectedItem).EventTitle);
                readSetupList.ShowDialog();
                refreshAllSetupLists();
                populateSetupLists();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to find selected setup list.\n" + ex.Message);
            }

        }

        private void ApplyFilters()
        {
            try
            {
                refreshAllSetupLists();

                if (cbxCompleted.IsChecked == true)
                {
                    _currentSetupLists = _setupListManager.SelectAllActiveSetupLists();
                }

                if (cbxIncomplete.IsChecked == true)
                {
                    _currentSetupLists = _setupListManager.SelectAllInActiveSetupLists();
                }

                if (txtSearchSetupLists.Text.ToString() != "")
                {
                    _currentSetupLists = _currentSetupLists.FindAll(s => s.EventTitle.ToLower().Contains(txtSearchSetupLists.Text.ToString().ToLower()));
                }

                if (vmsetup != null)
                {
                    List<VMSetupList> _cur = new List<VMSetupList>();
                    foreach (var item in _currentSetupLists)
                    {
                        if (item.SetupID == vmsetup.SetupID)
                        {
                            _cur.Add(item);
                        }
                    }
                    _currentSetupLists = _cur;
                }

                populateSetupLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// //method to call the filter method
        /// 
        /// Updated By: Caitlin Abelson
        /// Updated Date: 2019-03-27
        /// 
        /// The filter click button was calling a filter method that did not work. Changed filters and made adjusts
        /// to the button method in order for it to call the appropriate filters when needed.
        /// </summary>

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }




        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// //method to clear the filters
        /// </summary>
        private void BtnClearSetupList_Click(object sender, RoutedEventArgs e)
        {

            txtSearchSetupLists.Text = "";
            if (vmsetup != null)
            {
                _currentSetupLists = _setupLists.FindAll(x => x.SetupID == vmsetup.SetupID);
            }
            else
            {
                _currentSetupLists = _setupLists;
            }


            dgSetupList.ItemsSource = _currentSetupLists;

        }




        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
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




        private void BtnUpdateSetupList_Click(object sender, RoutedEventArgs e)
        {
            SetupList chosenSetupList = new SetupList();


            try
            {
                chosenSetupList = _setupListManager.SelectSetupList(((VMSetupList)dgSetupList.SelectedItem).SetupListID);
                var readSetupList = new SetupListDetail(chosenSetupList, ((VMSetupList)dgSetupList.SelectedItem).EventTitle);
                readSetupList.ShowDialog();
                refreshAllSetupLists();
                populateSetupLists();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to find selected setup list.\n" + ex.Message);
            }
        }

        private void BtnDeleteSetupList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _setupListManager.DeleteSetupList(((VMSetupList)dgSetupList.SelectedItem).SetupListID, ((VMSetupList)dgSetupList.SelectedItem).Completed);
                if (((VMSetupList)dgSetupList.SelectedItem).Completed)
                {
                    var result = MessageBox.Show("Are you sure you want to deactivate this Setup List?", "This Setup List will no longer be active in the system.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("The Setup List has been deactivated.");
                    }
                }
                else
                {
                    var result = MessageBox.Show("Are you sure you want to delete this Setup List?", "This Setup List will no longer be in the system.", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("The Setup List has been purged.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't delete this Setup List" + ex.Message);
            }

            refreshAllSetupLists();
            populateSetupLists();
        }

        private void BtnViewSetupList_Click(object sender, RoutedEventArgs e)
        {
            SetupList chosenSetupList = new SetupList();
            chosenSetupList = _setupListManager.SelectSetupList(((VMSetupList)dgSetupList.SelectedItem).SetupListID);
            var readSetupList = new SetupListDetail(chosenSetupList, ((VMSetupList)dgSetupList.SelectedItem).EventTitle);
            readSetupList.ShowDialog();
        }
    }


}







