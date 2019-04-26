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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataObjects;
using DataAccessLayer;
using LogicLayer;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for BrowseSpecialOrder.xaml
    /// </summary>
    public partial class BrowseSpecialOrder : Window
    {
        private SpecialOrderManagerMSSQL _specialOrderLogic = new SpecialOrderManagerMSSQL();

        public BrowseSpecialOrder()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/06
        /// 
        ///When add button is click, it opens the new form to create a new order.
        /// </summary>
        private void Button_Click_AddOrder(object sender, RoutedEventArgs e)
        {

            AddSpecialOrder order = new AddSpecialOrder();
            order.ShowDialog();
            updateList();
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/06
        /// 
        ///Method that loads the list of all orders when the Tab Supply Order is selected.
        ///
        /// </summary>
        private void DatagridReadAll_loaded(object sender, RoutedEventArgs e)
        {
            updateList();

        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/06
        /// 
        ///From the grid the user will select and click on the desired order
        ///a read-only form will open with the selected information.
        ///
        /// </summary>
        private void dgListAllOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selected = (CompleteSpecialOrder)dgListAllOrders.SelectedItem;
            var detailA = new AddSpecialOrder(selected); // pop up a detail window
            detailA.ShowDialog();
            updateList();

        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/27
        /// 
        ///From the grid the user will select and click on the desired order
        ///a read-only form will open with the selected information.
        ///
        /// </summary>
        private void Deactivate_Click(object sender, RoutedEventArgs e)
        {

            if (dgListAllOrders.SelectedIndex > -1)
            {
                var result = MessageBox.Show("Do you want to cancel the selected order?", "Cancel order", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {

                    var order = (CompleteSpecialOrder)dgListAllOrders.SelectedItem;
                    try
                    {
                        _specialOrderLogic.DeactivateSpecialOrder(order.SpecialOrderID);
                        dgListAllOrders.ItemsSource = _specialOrderLogic.retrieveAllOrders();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an order to cancel");
            }
        }

        public void updateList()
        {
            try
            {
                SpecialOrderManagerMSSQL _selected = new SpecialOrderManagerMSSQL();
                dgListAllOrders.ItemsSource = _selected.retrieveAllOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Can't retrieve data grid");
            }
        }

        private void btnCompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgListAllOrders.SelectedIndex < 0)
            {
                MessageBox.Show("You must have an order selected");
            }
            else
            {
                var order = (CompleteSpecialOrder)dgListAllOrders.SelectedItem;
                var receiving = new OrderRecieving(order);
                receiving.ShowDialog();
                updateList();
            }
        }
    }
}

