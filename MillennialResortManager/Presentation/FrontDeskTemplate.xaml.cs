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
    /// Interaction logic for FrontDeskTemplate.xaml
    /// </summary>
    public partial class FrontDeskTemplate : Window
    {
        private LuggageManager luggageManager = new LuggageManager();
        private GuestManager guestManager = new GuestManager();
        public FrontDeskTemplate()
        {
            InitializeComponent();
        }

        public void setupWindow()
        {
            dgLuggage.ItemsSource = luggageManager.RetrieveAllLuggage();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setupWindow();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var frmAdd = new AddLuggage(luggageManager, guestManager);
            if (frmAdd.ShowDialog() == true)
            {
                MessageBox.Show("Luggage Added.");
                setupWindow();
            }
            return;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRow row = (DataGridRow)dgLuggage.ItemContainerGenerator.ContainerFromIndex(dgLuggage.SelectedIndex);
                DataGridCell RowColumn = dgLuggage.Columns[0].GetCellContent(row).Parent as DataGridCell;
                openView(luggageManager.RetrieveLuggageByID(int.Parse(((TextBlock)RowColumn.Content).Text)));
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("You must select a guest before editing.");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("You must select a guest before editing.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRow row = (DataGridRow)dgLuggage.ItemContainerGenerator.ContainerFromIndex(dgLuggage.SelectedIndex);
                DataGridCell RowColumn = dgLuggage.Columns[0].GetCellContent(row).Parent as DataGridCell;
                if (luggageManager.DeleteLuggage(luggageManager.RetrieveLuggageByID(int.Parse(((TextBlock)RowColumn.Content).Text))))
                {
                    MessageBox.Show("Luggage Deleted.");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("You must select a guest before deleting.");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("You must select a guest before deleting.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            setupWindow();
        }

        private void openView(Luggage l )
        {
            var frmView = new EditLuggage(luggageManager, l);
            if (frmView.ShowDialog() == true)
            {
                MessageBox.Show("Luggage Updated.");
                setupWindow();
            }
            return;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
