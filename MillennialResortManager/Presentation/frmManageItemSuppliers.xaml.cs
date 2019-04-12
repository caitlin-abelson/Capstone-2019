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
using DataObjects;
using LogicLayer;

namespace Presentation
{
    /// <summary>
    /// Eric Bostwick
    /// Interaction logic for frmManageItemSuppliers.xaml
    /// Form for managing suppliers attached to item in
    /// the itemsuppliertable
    /// </summary>  
    
    public partial class frmManageItemSuppliers : Window
    {
        private ItemSupplierManager _itemSupplierManager = new ItemSupplierManager();
        private Item _item;
        private List<ItemSupplier> _itemSuppliers;
        private ItemSupplier _itemSupplier;

        public frmManageItemSuppliers()
        {
            InitializeComponent();
        }
        public frmManageItemSuppliers(Item item)
        {
            _item = item;
            InitializeComponent();
            LoadControls(_item);
            LoadItemSupplierGrid();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadControls(Item item)
        {
            this.Title = "Item Supplier Management for Item# " + item.ItemID;
            this.txtTitle.Text = this.Title;
            this.lblItemID.Content = item.ItemID;
            this.txtDescription.Text = item.Description;
            this.txtName.Text = item.Name;
        }
        private void LoadItemSupplierGrid()
        {
            try
            {
                _itemSuppliers = _itemSupplierManager.RetrieveAllItemSuppliersByItemID(_item.ItemID);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            this.dgItemSupplier.ItemsSource = _itemSuppliers;

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Do You Really Want to Cancel Managing Suppliers?", "Cancel Item Supplier Management", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
            
        }

        private void BtnAddItemSupplier_Click(object sender, RoutedEventArgs e)
        {
            var itemSupplyManager = new frmAddItemSupplierForItem(_item, EditMode.Add );
            var result = itemSupplyManager.ShowDialog();
            if (result == true)
            {
                LoadControls(_item);
                LoadItemSupplierGrid();
            }
            else
            {
                return;
            }
        }

        private void DgItemSupplier_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _itemSupplier = (ItemSupplier)dgItemSupplier.SelectedItem;
            
            var itemSupplyManager = new frmAddItemSupplierForItem(_item, _itemSupplier, EditMode.Edit);
            var result = itemSupplyManager.ShowDialog();
            if (result == true)
            {
                LoadControls(_item);
                LoadItemSupplierGrid();
            }
            else
            {
                return;
            }

        }

       
    }
}
