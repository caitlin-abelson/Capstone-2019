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
    /// Jacob Miller
    /// Created: 2018/01/22
    /// Interaction logic for Performance.xaml
    /// </summary>
    public partial class AddPerformance : Window
    {
        private PerformanceManager performanceManager;

        public AddPerformance(PerformanceManager manager)
        {
            InitializeComponent();
            performanceManager = manager;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPerformanceName.Text == "")
                {
                    throw new ApplicationException("Performance must have a name.");
                }
                if (dtpDate.SelectedDate <= DateTime.Now)
                {
                    throw new ApplicationException("The performance date must be after today.");
                }
                if (performanceManager.AddPerformance(new Performance(999999, txtPerformanceName.Text, (DateTime)dtpDate.SelectedDate, txtDescription.Text)) == 1)
                {
                    DialogResult = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpDate.SelectedDate = DateTime.Now;
        }
    }
}
