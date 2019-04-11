using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Created 2/6/2019
    /// frmAddItemSupplierForItem.xaml
    /// Form used to manage suppliers for an individual inventory item
    /// Constructor will take the mode (View, Edit, Add)
    /// to determine what options to give the user
    /// </summary>  
    public partial class frmAddItemSupplierForItem : Window
    {
        private ItemSupplierManager _itemSupplierManager = new ItemSupplierManager();
        private Item _item;
        //private List<ItemSupplier> _itemSuppliers;
        private ItemSupplier _itemSupplier;
        private ItemSupplier _oldItemSupplier;
        private List<Supplier> _suppliers;
        private EditMode _editMode;
        private Supplier _supplier;
        

        //Default Constructor
        public frmAddItemSupplierForItem()
        {
            InitializeComponent();
        }

        //Constructor if we are editing an item supplier
        public frmAddItemSupplierForItem(Item item, ItemSupplier itemSupplier, EditMode editMode)
        {
            InitializeComponent();
            _item = item;
            _itemSupplier = itemSupplier;
            _editMode = editMode;
            
            LoadControls();
        }

        //Contructor If we are adding an item supplier
        public frmAddItemSupplierForItem(Item item, EditMode editmode)
        {
            InitializeComponent();
            _item = item;
            _itemSupplier = new ItemSupplier();
            _editMode = editmode;
            LoadControls();
            LoadSupplierCombo();
            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            string message;
            if(_editMode == EditMode.Add)
            {
                message = "Do You Really Want to Cancel Adding a Supplier to This Item?";
            }
            else
            {
                message = "Do You Really Want to Cancel Editing the Supplier for this Item?";
            }
            result = MessageBox.Show(message, "Item Supplier Management", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LoadControls()
        {
            lblItemID.Content = _item.ItemID;
            this.txtDescription.Text = _item.Description;
            this.txtDescription.IsEnabled = false;
            this.txtName.Text = _item.Name;
            this.txtName.IsEnabled = false;
            

            if (_editMode == EditMode.Add)
            {   
                this.lblAddress.Visibility = Visibility.Hidden;
                this.txtAddress.Visibility = Visibility.Hidden;
                this.lblContact.Visibility = Visibility.Hidden;
                this.txtContact.Visibility = Visibility.Hidden;
                this.lblSupplierDescription.Visibility = Visibility.Hidden;
                this.txtSupplierDescription.Visibility = Visibility.Hidden;
                this.lblLeadTime.Visibility = Visibility.Hidden;
                this.txtLeadTime.Visibility = Visibility.Hidden;
                this.chkPrimarySupplier.Visibility = Visibility.Hidden;
                this.lblUnitPrice.Visibility = Visibility.Hidden;
                this.txtUnitPrice.Visibility = Visibility.Hidden;
                
                btnDeleteItemSupplier.Visibility = Visibility.Hidden;
                btnDeactivateItemSupplier.Visibility = Visibility.Hidden;
                btnAddItemSupplier.Visibility = Visibility.Hidden;
                btnAddItemSupplier.Content = "Add Supplier";
            }
            if (_editMode == EditMode.Edit || _editMode == EditMode.View)
            {
                LoadSupplierCombo();
                LoadItemSupplier();
                _oldItemSupplier = CopyItemSupplier(_itemSupplier);
                this.txtAddress.Text = _itemSupplier.Address + "\n" + _itemSupplier.City + ", " + _itemSupplier.State + "\n" + _itemSupplier.PostalCode;
                this.txtContact.Text = _itemSupplier.ContactFirstName + " " + _itemSupplier.ContactLastName + "\n" +
                                       _itemSupplier.Email + "\n" + _itemSupplier.PhoneNumber;
                txtSupplierDescription.TextWrapping = TextWrapping.WrapWithOverflow;
                txtSupplierDescription.Text = _itemSupplier.Description;
                txtLeadTime.Text = _itemSupplier.LeadTimeDays.ToString();
                txtUnitPrice.Text = FormatPrice(_itemSupplier.UnitPrice);
                chkPrimarySupplier.IsChecked = _itemSupplier.PrimarySupplier;
                lblAddress.Visibility = Visibility.Visible;
                txtAddress.Visibility = Visibility.Visible;
                lblContact.Visibility = Visibility.Visible;
                txtContact.Visibility = Visibility.Visible;
                lblSupplierDescription.Visibility = Visibility.Visible;
                txtSupplierDescription.Visibility = Visibility.Visible;
                lblLeadTime.Visibility = Visibility.Visible;
                txtLeadTime.Visibility = Visibility.Visible;
                chkPrimarySupplier.Visibility = Visibility.Visible;
                lblUnitPrice.Visibility = Visibility.Visible;
                txtUnitPrice.Visibility = Visibility.Visible;
                btnAddItemSupplier.Visibility = Visibility.Visible;
                btnAddItemSupplier.Content = "Update Supplier";
                btnDeleteItemSupplier.Visibility = Visibility.Visible;
                btnDeactivateItemSupplier.Visibility = Visibility.Visible;
                cboSupplier.Text = _itemSupplier.Name + " " + _itemSupplier.SupplierID;
                if (_itemSupplier.ItemSupplierActive)
                {
                    btnDeactivateItemSupplier.Content = "Deactivate ItemSupplier";
                } else
                {
                    btnDeactivateItemSupplier.Content = "Activate ItemSupplier";
                }

            }
        }
        private void LoadSupplierCombo()
        {
            try
            {
                _suppliers = _itemSupplierManager.RetrieveAllSuppliersForItemSupplierManagement(_item.ItemID);
                foreach ( Supplier supplier in _suppliers)
                {                   
                    cboSupplier.Items.Add(supplier.Name + " " + supplier.SupplierID);
                }                
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CboSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Make sure its not null and load the local _supplier object with
            //the selected supplier
            if (!this.cboSupplier.SelectedItem.Equals(null))
            {
                SetSelectedSupplier(cboSupplier.SelectedItem.ToString());
                if(_editMode == EditMode.Add)
                {
                    LoadNewSupplierControls();
                }                
            } 
            else
            {
                return;
            }      
        }
        /// <summary>
        /// Eric Bostwick
        /// 2/6/2019
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
                    break;
                }                
            }
        }
        private void LoadNewSupplierControls()
        {
            this.txtAddress.Text = _supplier.Address + "\n" + _supplier.City + ", " + _supplier.State + "\n" + _supplier.PostalCode;
            this.txtContact.Text = _supplier.ContactFirstName + " " + _supplier.ContactLastName + "\n" +
                                   _supplier.Email + "\n" + _supplier.PhoneNumber;
            txtSupplierDescription.TextWrapping = TextWrapping.WrapWithOverflow;
            txtSupplierDescription.Text = _supplier.Description;
            txtLeadTime.Text = "";
            txtUnitPrice.Text = "";

            lblAddress.Visibility = Visibility.Visible;
            txtAddress.Visibility = Visibility.Visible;
            lblContact.Visibility = Visibility.Visible;
            txtContact.Visibility = Visibility.Visible;
            lblSupplierDescription.Visibility = Visibility.Visible;
            txtSupplierDescription.Visibility = Visibility.Visible;
            lblLeadTime.Visibility = Visibility.Visible;
            txtLeadTime.Visibility = Visibility.Visible;
            chkPrimarySupplier.Visibility = Visibility.Visible;
            lblUnitPrice.Visibility = Visibility.Visible;
            txtUnitPrice.Visibility = Visibility.Visible;
            btnAddItemSupplier.Visibility = Visibility.Visible;
        }

        private bool ValidateInput()
        {
            // Lead Time Validation           
            if (!Regex.IsMatch(txtLeadTime.Text, @"^-?\d{1,3}$")) //validates 3 digit integer
            { 
                MessageBox.Show("Please Enter a Valid Lead Time Between 1 and 365");
                txtLeadTime.Select(0, txtLeadTime.Text.Length);
                txtLeadTime.Focus();
                return false;
            }
            int leadTime;
            int.TryParse(txtLeadTime.Text, out leadTime);
            if(leadTime < 0 || leadTime > 365)
            {
                MessageBox.Show("Please Enter a Valid Lead Time Between 1 and 365");
                txtLeadTime.Select(0, txtLeadTime.Text.Length);
                txtLeadTime.Focus();
                return false;
            }

            //Unit Price Validation    ^(\d*\.)?\d+$  //^[+-]?[0-9]{1,3}(?:,?[0-9]{3})*(?:\.[0-9]{2})?$  //^(\()?[0-9]+(?>,[0-9]{3})*(?>\.[0-9]{2})?(?(1)\))$   //^(\()?[0-9]+(?>,[0-9]{3})*(?>\.[0-9]{2})?(?(1)\))$    
            if (!Regex.IsMatch(txtUnitPrice.Text, @"^(\()?[0-9]+(?>,[0-9]{3})*(?>\.[0-9]{2})?(?(1)\))$"))
            {
                MessageBox.Show("Please Enter a Unit Price eg. 0.25 or 5.25");
                txtUnitPrice.Select(0, txtUnitPrice.Text.Length);
                txtUnitPrice.Focus();
                return false;
            }
            
            decimal unitPrice;
            decimal.TryParse(txtUnitPrice.Text, out unitPrice);
            if (unitPrice <= 0 || unitPrice > 9999)
            {
                MessageBox.Show("Please Enter a Unit Price between 0 and 9999.00");
                txtUnitPrice.Text = "0.01";
                txtUnitPrice.Select(0, txtUnitPrice.Text.Length);
                txtUnitPrice.Focus();
                return false;
            }

            return true;
        }

        private void BtnAddItemSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            _itemSupplier.ItemID = _item.ItemID;

            if (!(_supplier == null))
            {
                _itemSupplier.SupplierID = _supplier.SupplierID;
            }       
            
            _itemSupplier.PrimarySupplier = (bool)chkPrimarySupplier.IsChecked;
            _itemSupplier.LeadTimeDays = int.Parse(txtLeadTime.Text);
            _itemSupplier.UnitPrice = decimal.Parse(txtUnitPrice.Text);
            _itemSupplier.PrimarySupplier = (bool)(chkPrimarySupplier.IsChecked);

            if (_editMode == EditMode.Add)
            {
                try
                {
                    _itemSupplierManager.CreateItemSupplier(_itemSupplier);
                    DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (_editMode == EditMode.Edit)
            {
                try
                {
                    if (CheckForChanges())
                    {
                        
                        int result = _itemSupplierManager.UpdateItemSupplier(_itemSupplier, _oldItemSupplier);
                        if (result == 0) {
                            MessageBox.Show("Update Failed");
                        }
                        else
                        { 
                        DialogResult = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Changes Made, No Need to Save", "Change Can Be Good");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Need to Load up the _itemSupplier Object to fill out the supplier data on the screen
        private void LoadItemSupplier()
        {
            try
            {
                _itemSupplier = _itemSupplierManager.RetrieveItemSupplier(_item.ItemID, _itemSupplier.SupplierID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Eric Bostwick
        /// Created: 2/7/19
        /// Copies an itemsupplier object to a new itemsupplier object
        /// </summary>
        /// <param name="item">
        /// The itemsupplier to be copied
        /// </param>
        /// <returns>
        /// A copy of the itemsupplier object
        /// </returns>
        private static ItemSupplier CopyItemSupplier(ItemSupplier itemSupplier)
        {
            ItemSupplier _newItemSupplier = new ItemSupplier()
            {
                Address = itemSupplier.Address,
                City = itemSupplier.City,
                ContactFirstName = itemSupplier.ContactFirstName,
                ContactLastName = itemSupplier.ContactLastName,
                Country = itemSupplier.Country,
                DateAdded = itemSupplier.DateAdded,
                Description = itemSupplier.Description,
                Email = itemSupplier.Email,
                ItemID = itemSupplier.ItemID,
                LeadTimeDays = itemSupplier.LeadTimeDays,
                Name = itemSupplier.Name,
                PhoneNumber = itemSupplier.PhoneNumber,
                PostalCode = itemSupplier.PostalCode,
                PrimarySupplier = itemSupplier.PrimarySupplier,
                State = itemSupplier.State,
                SupplierID = itemSupplier.SupplierID,
                UnitPrice = itemSupplier.UnitPrice,
                ItemSupplierActive = itemSupplier.ItemSupplierActive
            };

            return _newItemSupplier;
        }
        private void BtnDeleteItemSupplier_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            string message;            
            message = "Do You Really Want to Delete This Item Supplier?";
            
            result = MessageBox.Show(message, "Item Supplier Management", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                try
                {
                    _itemSupplierManager.DeleteItemSupplier(_itemSupplier.ItemID, _itemSupplier.SupplierID); 
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
           else
            {
                return;
            }
        }
       

        //Formats the price 
        public static string FormatPrice(decimal myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return ((int)myNumber).ToString();
            }
            else
            {
                return s;
            }
        }
        //Check To see if there have been changes
        //returns false if nothing has changed
        private bool CheckForChanges()
        {
            bool isChanged = false;

            if (_oldItemSupplier.LeadTimeDays != int.Parse(txtLeadTime.Text) || _oldItemSupplier.UnitPrice != decimal.Parse(txtUnitPrice.Text)
                || (!(_supplier == null)) || _oldItemSupplier.PrimarySupplier != chkPrimarySupplier.IsChecked)
            {
                isChanged = true;
            }
            return isChanged;
        }

        private void BtnDeactivateItemSupplier_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbresult;
            string message;
            int result = 0;
            if (_itemSupplier.ItemSupplierActive)
            {
                message = "Do You Really Want to Deactivate This Item Supplier?";
            } else
            {
                message = "Do You Really Want to Activate This Item Supplier?";
            }

            mbresult = MessageBox.Show(message, "Item Supplier Management", MessageBoxButton.YesNo);
            if (mbresult == MessageBoxResult.Yes)
            {
                if (_itemSupplier.ItemSupplierActive)
                {
                    try
                    {
                        result = _itemSupplierManager.DeactivateItemSupplier(_itemSupplier.ItemID, _itemSupplier.SupplierID);
                        if (result == 1)
                        {
                            DialogResult = true;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong this item supply record was not deactivated");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else //reactivating item Supplier
                {
                    _itemSupplier.ItemSupplierActive = true;


                    result = _itemSupplierManager.UpdateItemSupplier(_itemSupplier, _oldItemSupplier);
                    if (result == 1)
                    {
                        DialogResult = true;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong this item supply record was not deactivated");
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
}
