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
    /// @Author: Phillip Hansen
    /// @Created 4/10/2019
    /// 
    /// Interaction logic for frmEventPerformanceMain.xaml
    /// </summary>
    public partial class frmEventPerformanceMain : Window
    {
        EventPerformanceManager _eventPerfManager = new EventPerformanceManager();
        private List<EventPerformance> _eventPerformances;

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Main constructor for the window
        /// </summary>
        public frmEventPerformanceMain()
        {
            InitializeComponent();

            populateEvPerfList();
            dgEventPerformance.IsEnabled = true;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Populates the data grid list with the complete table
        /// </summary>
        private void populateEvPerfList()
        {
            _eventPerformances = null;
            dgEventPerformance.ItemsSource = null;
            dgEventPerformance.Items.Refresh();

            try
            {
                _eventPerformances = _eventPerfManager.RetrieveAllEventPerformances();
                dgEventPerformance.ItemsSource = _eventPerformances;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not retrieve Event Performance List.");
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Changes the names of the header columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgEventPerformance_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();

            if (headerName == "EventID")
            {
                e.Column.Header = "Event ID";
            }
            else if (headerName == "PerformanceID")
            {
                e.Column.Header = "Performance ID";
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Event listener when a record is double clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgEventPerformance_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dgEventPerformance.SelectedIndex > -1)
            {
                var selectedEvPerf = (EventPerformance)dgEventPerformance.SelectedItem;

                if(selectedEvPerf == null)
                {
                    MessageBox.Show("No record selected!");
                }
            }
            else
            {
                MessageBox.Show("No record selected!");
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Button action for deleting a selected record
        /// (Keep this?)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteEventPerf_Click(object sender, RoutedEventArgs e)
        {
            EventPerformance selectedRecord = (EventPerformance)dgEventPerformance.SelectedItem;

            if(dgEventPerformance.SelectedIndex > -1)
            {
                //Add delete method here
            }
        }
    }
}
