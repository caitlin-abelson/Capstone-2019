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
    /// Interaction logic for ViewPerformance.xaml
    /// </summary>
    public partial class ViewPerformance : Window
    {
        private int performanceID;
        private PerformanceManager manager;

        

        public ViewPerformance(int id, PerformanceManager manager)
        {
            InitializeComponent();
            performanceID = id;
            this.manager = manager;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnCancel.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Hidden;
            Performance performance = manager.RetrievePerformanceByID(performanceID);
            txtPerformanceName.Text = performance.Name;
            txtDescription.Text = performance.Description;
            dtpDate.SelectedDate = performance.Date;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            txtDescription.IsEnabled = true;
            txtPerformanceName.IsEnabled = true;
            dtpDate.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnCancel.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Hidden;
            txtPerformanceName.IsEnabled = false;
            txtDescription.IsEnabled = false;
            dtpDate.IsEnabled = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
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
                if (manager.EditPerformance(new Performance(performanceID, txtPerformanceName.Text, (DateTime)dtpDate.SelectedDate, txtDescription.Text)) == 1)
                {
                    DialogResult = true;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            manager.DeletePerformance(new Performance(performanceID, txtPerformanceName.Text, (DateTime)dtpDate.SelectedDate, txtDescription.Text));
            DialogResult = true;
        }
    }
}
