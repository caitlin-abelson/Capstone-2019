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


        private ISetupListManager _setupListManager;
        private List<SetupList> _setupLists;
        private List<SetupList> _currentSetupLists;
        SetupList _selectedSetupList = new SetupList();


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// Default constructor:  setuplist.
        /// </summary>
        public BrowseSetupList()
        {
            _setupListManager = new SetupListManager();

            InitializeComponent();
            
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// constructor: BrowseSetupList with one parameter.
        /// </summary>
        public BrowseSetupList(ISetupListManager setupListManager = null)
        {
            if(setupListManager == null)
            {
                _setupListManager = new SetupListManager();
            }

            _setupListManager = setupListManager;
            InitializeComponent();
        }
       
        private void TabSetupList_GotFocus(object sender, RoutedEventArgs e)
        {

            //dgRole.Items.Refresh();
          

        }

        private void DgSetupList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {  
           //   var role = (Role)dgRole.SelectedItem;
            //  var detailForm = new UpdateEmployeeRole(role); 
   
        }

       
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// method to refresh browse setup list.
        /// </summary>
        private void refreshRoles()
        {
            try
            {
                _setupLists = _setupListManager.RetrieveAllSetupLists();

                _currentSetupLists = _setupLists;
               
                dgSetupList.ItemsSource = _currentSetupLists;
                filterRoles();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

       
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// //method to call the filter method
        /// </summary>

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            filterRoles();
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// //method to filter the setup list
        /// </summary>
        private void filterRoles()
        {
           
            IEnumerable<SetupList> _currentSetupLists = _setupLists;
            try
            {
               
                
                if (txtSearch.Text.ToString() != "")
                {
                   
                    if (txtSearch.Text != "" && txtSearch.Text != null)
                    {
                        _currentSetupLists = _currentSetupLists.Where(b => b.Description.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

                        
                    }
                }

                if (cbCompleted.IsChecked == true && cbUncompleted.IsChecked == false)
                {
                    _currentSetupLists = _currentSetupLists.Where(b => b.Completed == true);
                }
                else if (cbCompleted.IsChecked == false && cbUncompleted.IsChecked == true)
                {
                    _currentSetupLists = _currentSetupLists.Where(b => b.Completed == false);
                }
                else if (cbCompleted.IsChecked == false && cbUncompleted.IsChecked == false)
                {
                    _currentSetupLists = _currentSetupLists.Where(b => b.Completed == false && b.Completed == true);
                }
                
                dgSetupList.ItemsSource = null;

                dgSetupList.ItemsSource = _currentSetupLists;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);


            }

        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// //method to clear the filters
        /// </summary>
        private void BtnClearSetupList_Click(object sender, RoutedEventArgs e)
        {

            txtSearch.Text = "";
            _currentSetupLists = _setupLists;
            cbUncompleted.IsChecked = true;
            cbCompleted.IsChecked = true;

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
            if(result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

     

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/05
        /// 
        /// method window loaded to refresh roles
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshRoles();
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/25
        /// 
        /// method to filter uncompleted
        /// </summary>
        private void CbUncompleted_Click(object sender, RoutedEventArgs e)
        {
            filterRoles();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// method to filter completed
        /// </summary>
        private void CbCompleted_Click(object sender, RoutedEventArgs e)
        {
            filterRoles();
        }
    }


}







