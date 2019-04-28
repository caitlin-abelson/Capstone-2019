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
    /// Interaction logic for BrowseBuilding.xaml
    /// </summary>
    public partial class BrowseBuilding : Window
    {
        private List<Building> allBuildings;
        private List<Building> currentBuildings; // needed?
        private IBuildingManager buildingManager;
        public BrowseBuilding()
        {
            InitializeComponent();
            buildingManager = new BuildingManager();

            allBuildings = buildingManager.RetrieveAllBuildings();
            dgBuildings.ItemsSource = allBuildings;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/31
        /// 
        /// Displays list of buildings in the dgBuildings data grid.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// 
        /// </remarks>
        private void displayBuildings()
        {
            dgBuildings.ItemsSource = allBuildings;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/31
        /// 
        /// User double clicks a line in the dgBuildings data grid.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgBuildings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectBuilding();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSearch.Text.ToString() != "")
                {
                    currentBuildings = allBuildings.FindAll(b => b.Name.ToLower().Contains(txtSearch.Text.ToString().ToLower()));
                    dgBuildings.ItemsSource = currentBuildings;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            dgBuildings.ItemsSource = allBuildings;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/31
        /// 
        /// Displays an "Add View" BuildingDetail window.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addForm = new BuildingDetail();
            var buildingAdded = addForm.ShowDialog();

            if (buildingAdded == true)
            {
                // a building was added, update list
                try
                {
                    allBuildings = buildingManager.RetrieveAllBuildings();
                    dgBuildings.ItemsSource = allBuildings;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                // building was not added 
                MessageBox.Show("Building was not added.");
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            selectBuilding();
        }

        private void selectBuilding()
        {
            //Building selectedBuilding = (Building)dgBuildings.SelectedItem;
            //var detailForm = new BuildingDetail(selectedBuilding);
            //var formUpdated = detailForm.ShowDialog();

            //if (formUpdated == true)
            //{
            //    try
            //    {
            //        allBuildings = buildingManager.RetrieveAllBuildings();
            //        dgBuildings.ItemsSource = allBuildings;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }
    }
}
