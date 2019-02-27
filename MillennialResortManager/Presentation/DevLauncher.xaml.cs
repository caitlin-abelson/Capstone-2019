using DataObjects;
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
    /// This is a launcher for Developers to use while we develop functionality for our program
    /// </summary>
    public partial class DevLauncher : Window
    {
        private Employee _employee;
        public DevLauncher(Employee employee)
        {
            _employee = employee;
            InitializeComponent();
            txtGreeting.Content = "Hello there "+_employee.FirstName + ". This is a temporary page meant for developers to quickly launch their code";
        }

        private void btnReservationLauncher_Click(object sender, RoutedEventArgs e)
        {
            var browseReservation = new BrowseReservation(_employee);
            browseReservation.ShowDialog();
        }

        private void btnEmployeeLauncher_Click(object sender, RoutedEventArgs e)
        {
            var browseEmployee = new BrowseEmployee();
            browseEmployee.ShowDialog();
        }

        private void btnSupplierLauncher_Click(object sender, RoutedEventArgs e)
        {
            var browseSupplier = new BrowseSupplier();
            browseSupplier.ShowDialog();
        }

        private void btnEventLauncher_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnProductLauncher_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginPage();
            this.Close();
            login.ShowDialog();
        }
    }
}
