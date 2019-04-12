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
    /// Interaction logic for PerformanceViewer.xaml
    /// 
    /// Updated by Phil Hansen on 4/10/2019
    /// Added functionality for when an Event searches for a Performance
    /// </summary>
    public partial class PerformanceViewer : Window
    {
        private PerformanceManager performanceManager = new PerformanceManager();

        public Performance retrievedPerformance;

        public PerformanceViewer()
        {
            InitializeComponent();

            setupWindow();
        }
        
        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// This specific constructor is for retrieving a performance
        /// for an Event with that performance
        /// </summary>
        /// <param name="filterText"></param>
        public PerformanceViewer(string filterText)
        {
            InitializeComponent();

            this.btnBack.Content = "Select Performance";
            txtSearch.IsEnabled = false;
            txtSearch.Text = filterText;

            List<Performance> _filteredPerformances = new List<Performance>();

            foreach (var item in performanceManager.RetrieveAllPerformance().Where(p => p.Name.Equals(filterText)))
            {
                _filteredPerformances.Add(item);
            }
            try
            {
                dgPerformaces.ItemsSource = null;
                dgPerformaces.Items.Refresh();

                dgPerformaces.ItemsSource = _filteredPerformances;
                dgPerformaces.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nCould not populate filtered Performances");
            }

        }

        private void setupWindow()
        {
            dgPerformaces.ItemsSource = performanceManager.RetrieveAllPerformance();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //This if statement is for the filter constructor
            if(this.btnBack.Content.Equals("Select Performance"))
            {
                retrievedPerformance = (Performance)dgPerformaces.SelectedItem;
                if(retrievedPerformance != null)
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                    MessageBox.Show("Must have a performance selected!\nCreated a new one if necessary.");
                }
            }
            else
            {
                Close();
            }
            
        }

        private void openView(int performanceID)
        {
            var frmView = new ViewPerformance(performanceID, performanceManager);
            if (frmView.ShowDialog() == true)
            {
                MessageBox.Show("Performance Updated.");
                setupWindow();
            }
            return;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var frmAdd = new AddPerformance(performanceManager);
            if (frmAdd.ShowDialog() == true)
            {
                MessageBox.Show("Performance Added.");
                setupWindow();
            }
            return;
        }

        

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgPerformaces.ItemsSource = performanceManager.SearchPerformances(txtSearch.Text);
        }

        private void dgPerformaces_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var performance = (Performance)dgPerformaces.SelectedItem;
            if (performance != null)
            {
                openView(performance.ID);
            }
        }
    }
}
