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
    /// Interaction logic for DevLauncher.xaml
    /// </summary>
    public partial class DevLauncher : Window
    {
        private Employee _employee;
        public DevLauncher(Employee employee)
        {
            _employee = employee;
            InitializeComponent();
            txtGreeting.Content = "Hello there "+_employee.FirstName;
        }
    }
}
