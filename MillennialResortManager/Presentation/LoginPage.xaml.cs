using DataObjects;
using LogicLayer;
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

namespace Presentation
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 2/27/2019
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private Employee _employee;
        private UserManager _userManager;

        public LoginPage()
        {
            _userManager = new UserManager();
            InitializeComponent();
        }



        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = pwdPassword.Password;

                if (username.Length < 7 || username.Length > 250)
                {
                    txtUsername.Focus();
                    throw new ArgumentException("Bad Username");
                }
                if (password.Length < 6)
                {
                    pwdPassword.Focus();
                    throw new ArgumentException("Bad Password");
                }

                _employee = _userManager.AuthenticateEmployee(username,password);
                if (_employee != null)
                {
                    var devLauncher = new DevLauncher(_employee);
                    this.Close();
                    devLauncher.ShowDialog();
                    _employee = null;
                }
                else
                {
                    throw new ArgumentException("Authentication Failed");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid login attempt: "+ex );
            }
        }

        private void btnSpeedLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = "joanne@company.com";
                string password = "newuser";

                _employee = _userManager.AuthenticateEmployee(username, password);
                if (_employee != null)
                {
                    var devLauncher = new DevLauncher(_employee);
                    this.Close();
                    devLauncher.ShowDialog();
                    _employee = null;
                }
                else
                {
                    throw new ArgumentException("Authentication Failed");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid login attempt: " + ex);
            }
        }
    }
}
