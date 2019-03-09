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
using DataObjects;
using LogicLayer;
using System.Data;
using Presentation;
using System.Configuration;
    
namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private UserManager _userManager = new UserManager();
        private SpecialOrderManagerMSSQL _specialOrderLogic = new SpecialOrderManagerMSSQL();
        private User _user;

        // helper methods
        private void resetWindow()
        {
            // set up the main window when no one is logged in
            // put the screen back it was in the first palce
            _user = null;

         
            btnLogin.Content = "Log In";
            txtUsername.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;

            Message.Content = "Welcome";
            Alert.Content = "You must log in to continue.";

            txtUsername.Text = "Email Address";
            pwdPassword.Password = "password";
            txtUsername.Focus();
            txtUsername.SelectAll();
            hideAllUserTabs();
       

        }

        private void setUpWindow()
        {
            // set up the main window for the logged in user
            btnLogin.Content = "Log out";
            txtUsername.Visibility = Visibility.Hidden;
            pwdPassword.Visibility = Visibility.Hidden;

            string name = _user.FirstName + " " + _user.LastName;
            Message.Content = name;

            // the space between roles
            string roles = "";

            for (int i = 0; i < _user.Roles.Count; i++)
            {
                roles += _user.Roles[i];
                if (i < _user.Roles.Count - 2)
                {
                    roles += ", ";
                }
                else if (i < _user.Roles.Count - 1)
                {
                    roles += " and ";
                }
            }

            Alert.Content = "You are logged in as: " + roles;
            showUserTabs();
        }

        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            txtUsername.Focus();
           // hideAllUserTabs();
        }

        private void pwdPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pwdPassword.SelectAll();
        }

        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsername.SelectAll();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (this._user != null)
            {
                resetWindow();
                return; // needed
            }

            // test our loging function
            try
            {
                string username = txtUsername.Text;
                string password = pwdPassword.Password;

                if (username.Length < 7 || username.Length > 250)
                {
                    MessageBox.Show("Bad Username");
                    txtUsername.Focus();
                    return;
                }
                if (password.Length < 6)
                {
                    MessageBox.Show("Bad Password!");
                    pwdPassword.Focus();
                }

                _user = _userManager.AuthenticateUser(username, password);

                if (_user != null)
                {
                    MessageBox.Show("Welcome back " + _user.FirstName + ", authentication Succeeded!");

                    if (_user.Roles[0] == "New User")
                    {
                        this.Alert.Content = _user.FirstName + ", this is your first login. Please change your password.";

                        // open a password change dialog
                        var frmPassword = new frmUpdatePassword(_user, _userManager, true);
                        if (frmPassword.ShowDialog() == true)
                        {
                            // call setUpWindow to prepare the window for the logged in user
                            MessageBox.Show("Password successfully reset.");

                        }
                    }
                    setUpWindow();
                }
                else
                {
                    MessageBox.Show("Authentication Failed");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                throw;
            }


        }

        private void hideAllUserTabs()
        {
            foreach (var item in tabsetMain.Items)
            {
                // cast to TabItem
                ((TabItem)item).Visibility = Visibility.Collapsed;
            }
        }

        private void showUserTabs()
        {
         // loop througth the roles
            foreach (var r in _user.Roles)
            {
                switch (r)
                {
                    case "Event Management":
                        tabEventManagement.Visibility = Visibility.Visible;
                        tabEventManagement.IsSelected = true;
                        break;
                    case "Inventory":
                        tabInventory.Visibility = Visibility.Visible;
                        tabInventory.IsSelected = true;
                        break;

                    case "Supplier Order":
                        tabInventory.Visibility = Visibility.Visible;
                        tabInventory.IsSelected = true;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/06
        /// 
        ///When add button is click, it opens the new form to create a new order.
        /// </summary>
        private void Button_Click_AddOrder(object sender, RoutedEventArgs e)
        {

            AddSpecialOrder order = new AddSpecialOrder();
            order.Show();
            updateList();
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/06
        /// 
        ///Method that loads the list of all orders when the Tab Supply Order is selected.
        ///
        /// </summary>
        private void DatagridReadAll_loaded(object sender, RoutedEventArgs e)
        {
                        
            try
            {
                updateList();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/06
        /// 
        ///From the grid the user will select and click on the desired order
        ///a read-only form will open with the selected information.
        ///
        /// </summary>
        private void ListAllOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selected = (CompleteSpecialOrder)ListAllOrders.SelectedItem;
            var detailA = new AddSpecialOrder(selected); // pop up a detail window
            detailA.ShowDialog();
            updateList();
           
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/27
        /// 
        ///From the grid the user will select and click on the desired order
        ///a read-only form will open with the selected information.
        ///
        /// </summary>
        private void Deactivate_Click(object sender, RoutedEventArgs e)
        {

            if (ListAllOrders.SelectedIndex > -1)
            {
                var result = MessageBox.Show("Do you want to cancel the selected order?", "Cancel order", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {

                    var order = (CompleteSpecialOrder)ListAllOrders.SelectedItem;
                    try
                    {
                        _specialOrderLogic.DeactivateSpecialOrder(order.SpecialOrderID);
                        ListAllOrders.ItemsSource = _specialOrderLogic.retrieveAllOrders();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an order to cancel");
            }
        }

        public void updateList()
        {
            ListAllOrders.ItemsSource = _specialOrderLogic.retrieveAllOrders();
        }
    }
}
