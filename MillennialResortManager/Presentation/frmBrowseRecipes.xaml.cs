/// <summary>
/// Jared Greenfield
/// Created: 2019/02/07
/// 
///A form for browsing Recipes.
/// </summary>
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
    /// Interaction logic for frmBrowseRecipes.xaml
    /// </summary>
    public partial class frmBrowseRecipes : Window
    {
        private List<string> roles = new List<string>();
        private Employee _user;
        private List<Recipe> _recipes;
        private RecipeManager _recipeManager = new RecipeManager();
        private bool _isFilterRestting = false;
        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/01/24
        /// 
        /// Displays a list of all recipes.
        /// </summary>
        public frmBrowseRecipes()
        {
            InitializeComponent();
            roles.Add("Something");
            setupBrowsePage();
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Sets up the content and controls of the browsing window.
        /// </summary>
        private void setupBrowsePage()
        {
            try
            {
                _recipes = _recipeManager.RetrieveAllRecipes();
                dgRecipeList.ItemsSource = _recipes;
                dtpDateEnd.Focusable = false;
                dtpDateStart.Focusable = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Could not setup page.");
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Modifies the headers and sizes of the datagrid columns.
        /// </summary>
        private void DgRecipeList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yyyy";
            }
            switch (e.Column.Header)
            {
                case "RecipeID":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Name":
                    break;
                case "Description":
                    e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    break;
                case "DateAdded":
                    e.Column.Header = "Date Added";
                    break;
                case "Active":
                    break;
                case "RecipeLines":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
                
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Exits out of the Browsing screen.
        /// </summary>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Allows the user to view a Recipe.
        /// </summary>
        private void BtnViewRecipe_Click_1(object sender, RoutedEventArgs e)
        {
            if ((Recipe)dgRecipeList.SelectedItem != null)
            {
                var detailForm = new frmCreateRecipe((Recipe)dgRecipeList.SelectedItem, _user);
                var result = detailForm.ShowDialog();
                _recipes = _recipeManager.RetrieveAllRecipes();
                dgRecipeList.ItemsSource = _recipes;
            }
            else
            {
                MessageBox.Show("You must select a recipe first.");
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Allows the user to create a new Recipe.
        /// </summary>
        private void BtnCreateRecipe_Click(object sender, RoutedEventArgs e)
        {
            var createForm = new frmCreateRecipe(_user);
            var result = createForm.ShowDialog();
            if (result == true)
            {
                MessageBox.Show("Recipe created.");
            }
            else
            {
                MessageBox.Show("Recipe creation cancelled or failed.");
            }
            try
            {
                setupBrowsePage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error updating the page: " + ex.Message);
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// Filters the datagrid by the user's input.
        /// </summary>
        private void filterRecipeList()
        {
            setupBrowsePage();
            IEnumerable<Recipe> currentRecipes = _recipes;
            // Filter names
            if (txtName.Text != "" && txtName.Text != null)
            {
                currentRecipes = currentRecipes.Where(r => r.Name.ToUpper().StartsWith(txtName.Text.ToUpper()));
            }
            //Filter description
            if (txtDescription.Text != "" && txtDescription.Text != null)
            {
                currentRecipes = currentRecipes.Where(r => r.Description.ToUpper().Contains(txtDescription.Text.ToUpper()));
            }
            //Filter Start and End Date
            //Both have valid values
            if (dtpDateStart.SelectedDate.HasValue && dtpDateEnd.SelectedDate.HasValue)
            {
                // Make sure start is before end
                if (dtpDateStart.SelectedDate.Value.CompareTo(dtpDateEnd.SelectedDate.Value) < 0)
                {
                    // Filter start
                    currentRecipes = currentRecipes.Where(r => r.DateAdded >= dtpDateStart.SelectedDate.Value);

                    //Filter end
                    currentRecipes = currentRecipes.Where(r => r.DateAdded <= dtpDateEnd.SelectedDate.Value);
                }
            }
            else if (dtpDateStart.SelectedDate.HasValue && !dtpDateEnd.SelectedDate.HasValue)
            {
                // Filter start
                currentRecipes = currentRecipes.Where(r => r.DateAdded >= dtpDateStart.SelectedDate.Value);
            }
            else if (!dtpDateStart.SelectedDate.HasValue && dtpDateEnd.SelectedDate.HasValue)
            {
                //Filter end
                currentRecipes = currentRecipes.Where(r => r.DateAdded <= dtpDateEnd.SelectedDate.Value);
            }
            dgRecipeList.ItemsSource = null;
            dgRecipeList.ItemsSource = currentRecipes;

        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// On click, filters the list according to search criteria.
        /// </summary>
        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            filterRecipeList();
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// On click, clears the filters and resets the grid.
        /// </summary>
        private void BtnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            _isFilterRestting = true;
            txtName.Text = "";
            txtDescription.Text = "";
            dtpDateStart.SelectedDate = null;
            dtpDateEnd.SelectedDate = null;
            dtpDateStart.DisplayDateEnd = null;
            dtpDateEnd.DisplayDateStart = null;
            dgRecipeList.ItemsSource = _recipes;
            _isFilterRestting = false;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// When the start date changes, the end date picker updates so that that date must be after the start.
        /// </summary>
        private void DtpDateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isFilterRestting)
            {
                dtpDateEnd.DisplayDateStart = dtpDateStart.SelectedDate.Value.AddDays(1);
            }
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2019/02/07
        /// 
        /// When the end date changes, the start date picker updates so that that date must be before the end.
        /// </summary>
        private void DtpDateEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isFilterRestting)
            {
                dtpDateStart.DisplayDateEnd = dtpDateEnd.SelectedDate.Value.AddDays(-1);
            }
        }

        private void dgRecipeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((Recipe)dgRecipeList.SelectedItem != null)
            {
                var detailForm = new frmCreateRecipe((Recipe)dgRecipeList.SelectedItem, _user);
                var result = detailForm.ShowDialog();
                _recipes = _recipeManager.RetrieveAllRecipes();
                dgRecipeList.ItemsSource = _recipes;
            }
            else
            {
                MessageBox.Show("You must select a recipe first.");
            }
        }
    }
}
