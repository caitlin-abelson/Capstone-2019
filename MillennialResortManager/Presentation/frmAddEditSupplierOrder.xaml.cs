using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
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
    /// Created: 2/26/2019
    /// Interaction logic for frmManageItemSuppliers.xaml
    /// Form for managing suppliers attached to item in
    /// the itemsuppliertable
    /// </summary>  
    public partial class frmAddEditSupplierOrder : Window
    {
        private SupplierOrderManager _supplierOrderManager = new SupplierOrderManager();
        private SupplierManager _supplierManager = new SupplierManager();
        private ProductManagerMSSQL _itemManager = new ProductManagerMSSQL();
        private ItemSupplierManager _itemSupplierManager = new ItemSupplierManager();
        //private Product _item;

        private Supplier _supplier;
        private List<VMItemSupplierItem> _itemSuppliers;
        private List<Supplier> _suppliers;
        private VMItemSupplierItem _itemSupplier;
        private SupplierOrder _supplierOrder = new SupplierOrder();
        private SupplierOrderLine _supplierOrderLine = new SupplierOrderLine();
        private List<SupplierOrderLine> _supplierOrderLines = new List<SupplierOrderLine>();
        private EditMode _editMode;
        //Need to fake the employee id for now.
        private int _loggedInEmployeeID = 100000;

        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// Default Constructor for the Form
        /// </summary>
        public frmAddEditSupplierOrder()
        {
            InitializeComponent();
            _editMode = EditMode.Add;
            this.lblOrderDate.Content = DateTime.Now.Date;
            LoadControls();
            LoadSupplierCombo();
        }

        public frmAddEditSupplierOrder(SupplierOrder supplierOrder, EditMode editMode)
        {
            InitializeComponent();
            _editMode = EditMode.Edit;
            _supplierOrder = supplierOrder;


            LoadSupplierCombo();
            _supplier = _suppliers.Find(s => s.SupplierID == supplierOrder.SupplierID);
            txtDescription.Text = _supplierOrder.Description;

            LoadControls();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// Sets the local supplier variable based upon the selection from
        /// the supplier combo box
        /// </summary>     

        private void CboSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Make sure its not null and load the local _supplier object with
            //the selected supplier


            if (cboSupplier.SelectedItem != null)
            {
                if (cboSupplier.SelectedItem != null)
                {

                    SetSelectedSupplier(cboSupplier.SelectedItem.ToString());
                }

                if (_editMode == EditMode.Add)
                {
                    LoadSupplierControls();
                    ResetFormForNextLine();
                    dgOrderLines.Visibility = Visibility.Hidden;
                    LoadSupplierItemCombo(_supplier.SupplierID);
                }
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// Fills the Item Combo box
        /// the item combo box
        /// </summary>
        private void LoadSupplierItemCombo(int supplierID)
        {
            try
            {
                _itemSuppliers = _supplierOrderManager.RetrieveAllItemSuppliersBySupplierID(supplierID);
                cboSupplierItems.Items.Clear();
                foreach (VMItemSupplierItem itemSupplier in _itemSuppliers)
                {
                    cboSupplierItems.Items.Add(itemSupplier.ItemID + " " + itemSupplier.Description);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadSupplierCombo()
        {
            try
            {
                _suppliers = _supplierManager.RetrieveAllSuppliers();
                foreach (Supplier supplier in _suppliers)
                {
                    cboSupplier.Items.Add(supplier.Name + " " + supplier.SupplierID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Eric Bostwick
        /// 2/26/2019
        /// Sets the local supplier variable based upon the selection from
        /// the supplier combo box
        /// </summary>
        private void SetSelectedSupplier(string supplierID)
        {
            int iSupplierID = int.Parse(supplierID.Substring(supplierID.Length - 6, 6));

            foreach (Supplier supplier in _suppliers)
            {
                if (supplier.SupplierID == iSupplierID)
                {
                    //found it set the supplier object and get out of here
                    _supplier = supplier;
                    //_supplierOrder.SupplierID = _supplier.SupplierID;
                    break;
                }
            }
        }
        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// Sets the controls after a supplier is selected
        /// </summary>
        private void LoadSupplierControls()
        {

            this.txtContact.Text = _supplier.ContactFirstName + " " + _supplier.ContactLastName + "\n" +
                                   _supplier.Email + "\n" + _supplier.PhoneNumber;
            this.txtAddress.Text = _supplier.Address + "\n" + _supplier.City + ", " + _supplier.State + "\n" + _supplier.PostalCode;


            lblAddress.Visibility = Visibility.Visible;
            txtAddress.Visibility = Visibility.Visible;
            lblContact.Visibility = Visibility.Visible;
            txtContact.Visibility = Visibility.Visible;
            cboSupplierItems.Visibility = Visibility.Visible;
            lblPickItem.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// Initial controls state of the form
        /// </summary>
        public void LoadControls()
        {
            this.txtDescription.ToolTip = "Type a Brief Note About This Order";

            if (_editMode == EditMode.Add)
            {
                this.lblAddress.Visibility = Visibility.Hidden;
                this.txtAddress.Visibility = Visibility.Hidden;
                this.lblContact.Visibility = Visibility.Hidden;
                this.txtContact.Visibility = Visibility.Hidden;
                this.cboSupplierItems.Visibility = Visibility.Hidden;
                this.lblPickItem.Visibility = Visibility.Hidden;
                this.lblItemDescription.Visibility = Visibility.Hidden;
                this.lblOrderQty.Visibility = Visibility.Hidden;
                this.txtOrderQuantity.Visibility = Visibility.Hidden;
                this.lblUnitPrice.Visibility = Visibility.Hidden;
                this.txtUnitPrice.Visibility = Visibility.Hidden;
                this.txtExtendedPrice.Visibility = Visibility.Hidden;
                this.lblExtendedPrice.Visibility = Visibility.Hidden;
                this.btnAddLine.Visibility = Visibility.Hidden;
                this.dgOrderLines.Visibility = Visibility.Hidden;
                this.cboSupplier.IsEnabled = true;
                btnAddOrder.Content = "Submit Order";

            }
            if (_editMode == EditMode.Edit || _editMode == EditMode.View)

            {
                txtTitle.Text = "Edit Order " + _supplierOrder.SupplierOrderID;
                lblEmployeeID.Content = _supplierOrder.EmployeeID;
                lblOrderDate.Content = _supplierOrder.DateOrdered;
                LoadSupplierItemCombo(_supplierOrder.SupplierID);
                lblOrderQty.Visibility = Visibility.Hidden;
                txtOrderQuantity.Visibility = Visibility.Hidden;
                lblUnitPrice.Visibility = Visibility.Hidden;
                txtUnitPrice.Visibility = Visibility.Hidden;
                lblExtendedPrice.Visibility = Visibility.Hidden;
                txtExtendedPrice.Visibility = Visibility.Hidden;
                lblItemDescription.Visibility = Visibility.Hidden;
                btnAddLine.Visibility = Visibility.Hidden;
                LoadOrderLineGrid(_supplierOrder.SupplierOrderID);
                lblSupplier.Visibility = Visibility.Hidden;
                cboSupplier.Visibility = Visibility.Visible;
                cboSupplier.IsEnabled = false;
                btnAddOrder.Content = "Update Order";

                cboSupplier.Text = _supplier.Name + " " + _supplier.SupplierID;

                this.txtContact.Text = _supplier.ContactFirstName + " " + _supplier.ContactLastName + "\n" +
                                       _supplier.Email + "\n" + _supplier.PhoneNumber;
                this.txtAddress.Text = _supplier.Address + "\n" + _supplier.City + ", " + _supplier.State + "\n" + _supplier.PostalCode;

                lblContact.Visibility = Visibility.Visible;
                txtContact.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// What to do when an item is selected from the items combo
        /// the item combo box
        /// </summary>
        private void CboSupplierItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Make sure its not null and load the local _supplier object with
            //the selected supplierif(_editMode == EditMode.Add)
            if (cboSupplierItems.SelectedItem != null)
            {
                SetSelectedItemSupplier(cboSupplierItems.SelectedItem.ToString());
            }
            if (_editMode == EditMode.Add)
            {
                if (this.cboSupplierItems.SelectedItem != null)
                {
                    //SetSelectedItemSupplier(cboSupplierItems.SelectedItem.ToString());
                    LoadNewOrderControls();

                }
                else
                {
                    return;
                }

            }
            if (_editMode == EditMode.Edit)
            {
                //ResetFormForNextLine();
                LoadEditOrderControls();
            }


        }

        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// Sets the local item variable based upon the selection from
        /// the item combo box
        /// </summary>
        private void SetSelectedItemSupplier(string itemSelected)
        {
            int itemID = int.Parse(itemSelected.Substring(0, 6));
            _itemSupplier = _itemSuppliers.Find(i => i.ItemID == itemID);
        }

        public void LoadNewOrderControls()
        {
            _supplierOrder = new SupplierOrder();
            _supplierOrder.DateOrdered = DateTime.Now.Date;
            _supplierOrder.EmployeeID = _loggedInEmployeeID;
            _supplierOrder.SupplierID = _supplier.SupplierID;
            _supplierOrder.OrderComplete = false;


            _supplierOrderLine = new SupplierOrderLine();
            this.txtItemDescription.Text = _itemSupplier.Name;
            this.txtUnitPrice.Text = _itemSupplier.UnitPrice.ToString("c");

            _supplierOrderLine.ItemID = _itemSupplier.ItemID;
            _supplierOrderLine.UnitPrice = _itemSupplier.UnitPrice;
            _supplierOrderLine.Description = _itemSupplier.Description;

            this.lblItemDescription.Visibility = Visibility.Visible;
            this.txtItemDescription.Visibility = Visibility.Visible;
            this.lblOrderQty.Visibility = Visibility.Visible;
            this.txtOrderQuantity.Visibility = Visibility.Visible;
            this.lblUnitPrice.Visibility = Visibility.Visible;
            this.txtUnitPrice.Visibility = Visibility.Visible;
            this.txtUnitPrice.IsReadOnly = true;
            this.txtExtendedPrice.Visibility = Visibility.Visible;
            this.lblExtendedPrice.Visibility = Visibility.Visible;
            this.dgOrderLines.Visibility = Visibility.Visible;
            btnAddOrder.Content = "Submit Order";

        }
        public void LoadEditOrderControls()
        {
            _supplierOrderLine = new SupplierOrderLine();
            this.txtItemDescription.Text = _itemSupplier.Name;
            this.txtUnitPrice.Text = _itemSupplier.UnitPrice.ToString("c");

            _supplierOrderLine.ItemID = _itemSupplier.ItemID;
            _supplierOrderLine.UnitPrice = _itemSupplier.UnitPrice;
            _supplierOrderLine.Description = _itemSupplier.Description;

            this.lblItemDescription.Visibility = Visibility.Visible;
            this.txtItemDescription.Visibility = Visibility.Visible;
            this.lblOrderQty.Visibility = Visibility.Visible;
            this.txtOrderQuantity.Visibility = Visibility.Visible;
            this.lblUnitPrice.Visibility = Visibility.Visible;
            this.txtUnitPrice.Visibility = Visibility.Visible;
            this.txtUnitPrice.IsReadOnly = true;
            this.txtExtendedPrice.Visibility = Visibility.Visible;
            this.lblExtendedPrice.Visibility = Visibility.Visible;
            this.dgOrderLines.Visibility = Visibility.Visible;
            btnAddOrder.Content = "Update Order";
        }

        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// Validation for order quantity
        /// 
        /// </summary>
        private bool ValidateInput()
        {
            // Lead Time Validation           
            if (!Regex.IsMatch(txtOrderQuantity.Text, @"^-?\d{1,5}$")) //validates 3 digit integer
            {
                MessageBox.Show("Please Enter a Valid Qty Between 1 and 10000");
                txtOrderQuantity.Select(0, txtOrderQuantity.Text.Length);
                txtOrderQuantity.Focus();
                return false;
            }
            int orderQty;
            int.TryParse(txtOrderQuantity.Text, out orderQty);
            if (orderQty < 0 || orderQty > 100000)
            {
                MessageBox.Show("Please Enter a Valid Qty Between 1 and 10000");
                txtOrderQuantity.Select(0, txtOrderQuantity.Text.Length);
                txtOrderQuantity.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// Validation will occur on text change event of order qty.
        /// the item combo box
        /// </summary>
        private void TxtOrderQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            int orderQty;
            decimal orderTotal;
            if (txtOrderQuantity.Text.Length == 0)
            {
                txtExtendedPrice.Clear();
                btnAddLine.Visibility = Visibility.Hidden;
                return;
            }
            if (ValidateInput())
            {
                if (txtOrderQuantity.Text == "0")
                {
                    return;
                }
                //Calculate Extended Price
                try
                {

                    int.TryParse(txtOrderQuantity.Text, out orderQty);
                    orderTotal = orderQty * _itemSupplier.UnitPrice;
                    txtExtendedPrice.Text = orderTotal.ToString("c");
                    _supplierOrderLine.OrderQty = orderQty;
                    btnAddLine.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// Double click the grid to edit the line
        /// It will remove the item from the grid so it can be readded
        /// the item combo box
        /// </summary>
        private void DgOrderLines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            _supplierOrderLine = (SupplierOrderLine)dgOrderLines.SelectedItem;
            _supplierOrderLines.Remove(_supplierOrderLine);

            dgOrderLines.Items.Refresh();
            _itemSuppliers = _supplierOrderManager.RetrieveAllItemSuppliersBySupplierID(_supplier.SupplierID);
            _itemSupplier = _itemSuppliers.Find(s => s.ItemID == _supplierOrderLine.ItemID);

            txtUnitPrice.Text = _supplierOrderLine.UnitPrice.ToString();

            this.txtOrderQuantity.Text = _supplierOrderLine.OrderQty.ToString();
            this.lblItemDescription.Visibility = Visibility.Visible;
            this.txtItemDescription.Visibility = Visibility.Visible;
            this.lblOrderQty.Visibility = Visibility.Visible;
            this.txtOrderQuantity.Visibility = Visibility.Visible;
            this.lblUnitPrice.Visibility = Visibility.Visible;
            this.txtUnitPrice.Visibility = Visibility.Visible;
            this.txtUnitPrice.IsReadOnly = true;
            this.txtExtendedPrice.Visibility = Visibility.Visible;
            this.lblExtendedPrice.Visibility = Visibility.Visible;
            this.dgOrderLines.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// Adds a line to the _supplierOrderLines object and updates the orderlines grid
        /// </summary>
        private void BtnAddLine_Click(object sender, RoutedEventArgs e)
        {
            foreach (SupplierOrderLine line in _supplierOrderLines)
            {
                if (line.ItemID == _supplierOrderLine.ItemID)
                {
                    MessageBox.Show("You Can't Enter the Same Item Twice to an Order");
                    ResetFormForNextLine();
                    return;
                }
            }

            _supplierOrderLines.Add(_supplierOrderLine);
            dgOrderLines.ItemsSource = _supplierOrderLines;
            dgOrderLines.Items.Refresh();
            cboSupplier.IsEnabled = false;
            ResetFormForNextLine();
        }

        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        ///Reset the form after entering a line item
        /// the item combo box
        /// </summary>
        public void ResetFormForNextLine()
        {
            this.lblOrderQty.Visibility = Visibility.Hidden;
            this.txtOrderQuantity.Visibility = Visibility.Hidden;
            this.txtOrderQuantity.Clear();
            this.lblUnitPrice.Visibility = Visibility.Hidden;
            this.txtUnitPrice.Visibility = Visibility.Hidden;
            this.txtUnitPrice.Clear();
            this.lblExtendedPrice.Visibility = Visibility.Hidden;
            this.txtExtendedPrice.Visibility = Visibility.Hidden;
            this.txtExtendedPrice.Clear();
            this.txtItemDescription.Visibility = Visibility.Hidden;
            this.lblItemDescription.Visibility = Visibility.Hidden;
            this.btnAddLine.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// Closes the Form
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrderLines.HasItems)
            {
                MessageBoxResult mbresult;

                mbresult = MessageBox.Show("You have items set up to be ordered, do you really want to cancel?", "Add/Edit Orders", MessageBoxButton.YesNo);

                if (mbresult == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    Close();
                }
            }

        }

        /// <summary>
        /// Eric Bostwick
        /// 2/27/2019
        /// Sets the local item variable based upon the selection from
        /// the item combo box
        /// </summary>
        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            string message = "";

            if (_editMode == EditMode.Add)
            {
                message = "Do You Really Want to Submit this Order?";
            }
            if (_editMode == EditMode.Edit)
            {
                message = "This Will Update the Order, Is This what you want to do?";
            }

            MessageBoxResult mbresult;

            mbresult = MessageBox.Show(message, "Add/Edit Orders", MessageBoxButton.YesNo);

            if (mbresult == MessageBoxResult.No)
            {
                return;
            }

            int result;

            if (_editMode == EditMode.Add)
            {
                _supplierOrder.Description = this.txtDescription.Text;

                try
                {
                    result = _supplierOrderManager.CreateSupplierOrder(_supplierOrder, _supplierOrderLines);
                    if (1 <= result)
                    {
                        MessageBox.Show("Order Added");
                        DialogResult = true;
                        Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (_editMode == EditMode.Edit)
            {
                _supplierOrder.Description = this.txtDescription.Text;

                try
                {
                    result = _supplierOrderManager.UpdateSupplierOrder(_supplierOrder, _supplierOrderLines);
                    if (1 <= result)
                    {
                        MessageBox.Show("Order Updated");
                        DialogResult = true;
                        Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void LoadOrderLineGrid(int supplierID)
        {
            try
            {
                _supplierOrderLines = _supplierOrderManager.RetrieveAllSupplierOrderLinesBySupplierOrderID(supplierID);
                dgOrderLines.ItemsSource = _supplierOrderLines;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
