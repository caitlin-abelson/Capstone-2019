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
    /// 
    /// Author: Matt LaMarche
    /// Updated Date: 3/7/19
    /// Switched from UserManager to an IEmployeeManager implementation
    /// </summary>
    public partial class LoginPage : Window
    {
        private Employee _employee;
        private IEmployeeManager _employeeManager;

        public LoginPage()
        {
            _employeeManager = new EmployeeManager();
            InitializeComponent();
        }


        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/27/2019
        /// Attempts to log in and retrieve an Employee from our database
        /// 
        /// Author: Matt LaMarche
        /// Updated Date: 3/7/19
        /// Switched from UserManager to an IEmployeeManager implementation
        /// </summary>
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

                _employee = _employeeManager.AuthenticateEmployee(username, password);
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
                MessageBox.Show("Invalid login attempt: " + ex.Message);
            }
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/27/2019
        /// Logs in with a valid Email and Password. Meant to save time for developers. Delete before launch
        /// 
        /// Author: Matt LaMarche
        /// Updated Date: 3/7/19
        /// Switched from using a UserManager to using an IEmployeeManager implementation
        /// </summary>
        private void btnSpeedLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //string username = "joanne@company.com";
                //string password = "newuser";
                Employee emp = new Employee();
                emp.FirstName = "Joanne";
                emp.LastName = "Smith";
                emp.Email = "joanne@company.com";
                emp.PhoneNumber = "1234567890";
                _employee = emp;
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
                MessageBox.Show("Invalid login attempt: " + ex.Message);
            }
        }
    }
}
