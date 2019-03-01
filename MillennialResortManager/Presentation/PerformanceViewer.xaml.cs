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

namespace Presentation
{
    /// <summary>
    /// Jacob Miller
    /// Created: 2018/01/22
    /// Interaction logic for PerformanceViewer.xaml
    /// </summary>
    public partial class PerformanceViewer : Window
    {
        private PerformanceManager performanceManager = new PerformanceManager();

        public PerformanceViewer()
        {
            InitializeComponent();
        }

        private void setupWindow()
        {
            dgPerformaces.ItemsSource = performanceManager.RetrieveAllPerformance();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dgPerformaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                openView(int.Parse(((TextBlock)RowColumn.Content).Text));
            }
            catch (Exception)
            {
                
            }
            dgPerformaces.SelectedItem = null;
        }

        private void openView(int performanceID)
        {
            var frmView = new ViewPerformance(performanceID, performanceManager);
            if(frmView.ShowDialog() == true)
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setupWindow();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgPerformaces.ItemsSource = performanceManager.SearchPerformances(txtSearch.Text);
        }
    }
}
