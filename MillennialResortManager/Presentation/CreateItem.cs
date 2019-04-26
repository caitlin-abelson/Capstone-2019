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
using System.Text.RegularExpressions;

namespace Presentation
{
    /// <summary>
    /// Kevin Broskow
    /// Created: 2019/01/20
    /// 
    /// Window for creation of a new Item
    /// </summary>
    /// <remarks>
    /// Jared Greenfield
    /// Updated: 2019/04/03
    /// Revamped whole class to fit Offering structure.
    /// </remarks>
    public partial class CreateItem : Window
    {
        private ItemTypeManagerMSSQL _itemTypeManager = new ItemTypeManagerMSSQL();
        private ItemManager _itemManager = new ItemManager();
        private OfferingManager _offeringManager;
        private Item oldItem = new Item();
        private Item newItem = new Item();
        private Offering _offering;
        private Employee _user;

        public CreateItem()
        {
            InitializeComponent();
            try
            {
                _offeringManager = new OfferingManager();
            }
            catch (Exception)
            {
                MessageBox.Show("Error collecting price.");
            }
            this.btnEdit.Visibility = Visibility.Hidden;
            this.lblDateActive.Visibility = Visibility.Hidden;
            this.lblActive.Visibility = Visibility.Hidden;
            this.dpDateActive.Visibility = Visibility.Hidden;
            this.cbActive.Visibility = Visibility.Hidden;
            txtPrice.Visibility = Visibility.Collapsed;
            lblPrice.Visibility = Visibility.Collapsed;
        }

        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Updated to remove products and add in Item
        /// </remarks>
        public CreateItem(Item selectedItem, Employee user)
        {
            _user = user;
            oldItem = selectedItem;
            InitializeComponent();
            try
            {
                _offeringManager = new OfferingManager();
                if (oldItem.OfferingID != null)
                {
                    _offering = _offeringManager.RetrieveOfferingByID((int)oldItem.OfferingID);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error collecting price.");
            }
            SetupReadOnly(selectedItem.ItemID);
            this.txtDescription.IsReadOnly = true;
            this.txtName.IsReadOnly = true;
            this.txtOnHand.IsReadOnly = true;
            this.txtReorder.IsReadOnly = true;
            this.txtPrice.IsReadOnly = true;
            this.cbPurchasable.IsEnabled = false;
            this.cbActive.IsEnabled = false;
            this.dpDateActive.IsEnabled = false;
            this.btnSubmit.Visibility = Visibility.Hidden;
            this.btnEdit.Visibility = Visibility.Visible;
            this.lblDateActive.Visibility = Visibility.Visible;
            this.lblActive.Visibility = Visibility.Visible;
            this.dpDateActive.Visibility = Visibility.Visible;
            this.cbActive.Visibility = Visibility.Visible;
            this.dpDateActive.SelectedDate = selectedItem.DateActive;
            this.txtDescription.Text = selectedItem.Description;
            this.txtName.Text = selectedItem.Name;
            if (_offering != null)
            {
                this.txtPrice.Text = _offering.Price.ToString();
            }
            this.txtOnHand.Text = selectedItem.OnHandQty.ToString();
            this.txtReorder.Text = selectedItem.ReorderQty.ToString();
            if (selectedItem.Active)
            {
                this.cbActive.IsChecked = true;
            }
            else
            {
                this.cbActive.IsChecked = false;
            }
            if (selectedItem.CustomerPurchasable)
            {
                this.cbPurchasable.IsChecked = true;
            }
            this.cboItemType.SelectedItem = selectedItem.ItemType;
            ///this.txtPrice.Text = selectedItem.;
            this.cbActive.IsChecked = true;
            this.Title = "View Item";
        }
        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/21
        /// 
        /// Method to handle the event of the primary button being clicked.
        /// </summary>
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/04
        /// Updated to remove Item and add in Item
        /// Added Price Added Offering Support
        /// </remarks>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool offerCreated = false;
            if (!ValidateInput())
            {
                return;
            }
            if (((String)this.btnSubmit.Content) == "Submit")
            {
                try
                {
                    int OfferingID;
                    if (cbPurchasable.IsChecked == true)
                    {
                        Decimal outVal;
                        Decimal.TryParse(txtPrice.Text, out outVal);
                        Offering newOffering = new Offering();
                        newOffering.Price = outVal;
                        newOffering.Description = txtDescription.Text;
                        newOffering.EmployeeID = 100000;
                        newOffering.OfferingTypeID = "Item";
                        OfferingID = _offeringManager.CreateOffering(newOffering);
                        oldItem.OfferingID = OfferingID;
                        offerCreated = true;
                    }
                    
                    oldItem.Name = this.txtName.Text;
                    oldItem.OnHandQty = Int32.Parse(this.txtOnHand.Text);
                    oldItem.ReorderQty = Int32.Parse(this.txtReorder.Text);
                    oldItem.ItemType = this.cboItemType.SelectedItem.ToString();
                    oldItem.Description = this.txtDescription.Text;
                    oldItem.DateActive = DateTime.Now;
                    oldItem.CustomerPurchasable = (bool)cbPurchasable.IsChecked;
                    oldItem.ItemID = _itemManager.CreateItem(oldItem);
                    if ((bool)this.cbPurchasable.IsChecked)
                    {
                        oldItem.CustomerPurchasable = true;
                    }
                    else
                    {
                        oldItem.CustomerPurchasable = false;
                    }
                    MessageBox.Show("Add worked ");
                }
                catch (Exception ex)
                {
                    if (offerCreated)
                    {
                        try
                        {
                            _offeringManager.DeleteOfferingByID((int)oldItem.OfferingID);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Blank offering Created. Please Delete.");
                        }
                    }
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else if (((String)this.btnSubmit.Content) == "Save")
            {
                try
                {
                    int offeringID = 0;
                    Offering newOffering;
                    if (_offering != null)
                    {
                        newOffering = new Offering()
                        {
                            OfferingID = _offering.OfferingID,
                            OfferingTypeID = _offering.OfferingTypeID,
                            Active = _offering.Active,
                            Description = _offering.Description,
                            EmployeeID = 100000,
                            Price = (Decimal)1.0
                        };
                    }
                    else
                    {
                        newOffering = new Offering()
                        {
                            OfferingTypeID = "Item",
                            Description = txtDescription.Text,
                            EmployeeID = 100000,
                            Price = (Decimal)1.0
                        };
                    }
                    Decimal outVal;
                    Decimal.TryParse(txtPrice.Text, out outVal);
                    newOffering.Price = outVal;
                    if (cbPurchasable.IsChecked == true && oldItem.OfferingID != null)
                    {
                        //Update the existing Offering
                        _offeringManager.UpdateOffering(_offering, newOffering);
                    }
                    else if (cbPurchasable.IsChecked == true && oldItem.OfferingID == null)
                    {
                        // Create an offering because one doesn't exist
                        newOffering = new Offering("Item", 100000, txtDescription.Text, outVal);
                        offeringID = _offeringManager.CreateOffering(newOffering);
                        newItem.OfferingID = offeringID;
                    }
                    else if (oldItem.OfferingID == null)
                    {
                        // Do nothing with offerings because the other two choices are already taken care of.
                    }
                    newItem.Name = this.txtName.Text;
                    newItem.OnHandQty = Int32.Parse(this.txtOnHand.Text);
                    newItem.ReorderQty = Int32.Parse(this.txtReorder.Text);
                    newItem.ItemType = this.cboItemType.SelectedItem.ToString();
                    newItem.Description = this.txtDescription.Text;
                    newItem.ItemID = oldItem.ItemID;
                    if (newItem.OfferingID == null)
                    {
                        newItem.OfferingID = oldItem.OfferingID;
                    }
                    newItem.DateActive = (DateTime)this.dpDateActive.SelectedDate;
                    if (this.txtPrice.Text != "" && this.txtPrice.Text != null)
                    {
                        newItem.RecipeID = oldItem.RecipeID;
                    }
                    else
                    {
                        newItem.RecipeID = null;
                    }
                    if ((bool)this.cbPurchasable.IsChecked)
                    {
                        newItem.CustomerPurchasable = true;
                    }
                    else
                    {
                        oldItem.CustomerPurchasable = false;
                    }
                    if (cbActive.IsChecked == true)
                    {
                        newItem.Active = true;
                    }
                    else
                    {
                        newItem.Active = false;
                    }
                    _itemManager.UpdateItem(oldItem, newItem);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            this.Close();
        }
        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/21
        /// 
        /// Method to check for valid data.
        /// </summary>
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Updated to remove Item and add in Item
        /// Added Price 
        /// </remarks>
        private bool ValidateInput()
        {
            if (ValidName())
            {
                if (ValidDescription())
                {
                    if (ValidOnHand())
                    {
                        if (ValidReOrder())
                        {
                            if (ValidItemType())
                            {
                                if (cbPurchasable.IsChecked == true)
                                {
                                    if (txtPrice.Text != "" && txtPrice.Text != null)
                                    {
                                        Regex priceRegex = new Regex(@"^([0-9])*(\.{0,1})*(\d{0,2})$");
                                        Match match = priceRegex.Match(txtPrice.Text);
                                        if (txtPrice.Text.IsValidPrice() && match.Success)
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Invalid Price format. Use XX.00 format.");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("You must enter a price for it to be offered.");
                                    }
                                }
                                else if (txtPrice.Text != "" && txtPrice != null)
                                {
                                    MessageBox.Show("You must check purchasable and have a price entered to offer an item.");
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid Item Type");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Reorder point");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid On Hand Point");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Description");
                }
            }
            else
            {
                MessageBox.Show("Invalid Name");
            }
            return false;
        }



        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/21
        /// 
        /// Method to check for valid ItemType Selection.
        /// </summary>
        private bool ValidItemType()
        {
            if (cboItemType.SelectedIndex > -1)
            {
                return true;
            }
            int n;
            bool canParse = Int32.TryParse(this.txtPrice.Text, out n);
            if (canParse)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/21
        /// 
        /// Method to check for valid reorder input.
        /// </summary>
        private bool ValidReOrder()
        {
            if (txtReorder.Text == null || txtReorder.Text == "")
            {
                return false;
            }
            if (int.Parse(txtReorder.Text) >= 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/21
        /// 
        /// Method to check for valid onhand input.
        /// </summary>
        private bool ValidOnHand()
        {
            try
            {
                if (txtOnHand.Text == null || txtOnHand.Text == "")
                {
                    return false;
                }
                if (int.Parse(txtOnHand.Text) >= 1)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                
            }
            return false;
        }
        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/21
        /// 
        /// Method to check for valid description input.
        /// </summary>
        private bool ValidDescription()
        {
            if (txtDescription.Text.Length > 250)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/21
        /// 
        /// Method to check for valid name input.
        /// </summary>
        private bool ValidName()
        {
            if (txtName.Text == null || txtName.Text == "")
            {
                return false;
            }
            if (txtName.Text.Length > 50)
            {
                return false;
            }
            return true;
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            var form = new frmManageItemSuppliers(oldItem);
            form.ShowDialog();
        }
        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/21
        /// 
        /// Method to handle the event of the window being loaded. Sets up the combobox.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtName.Focus();

            List<String> itemTypes = new List<String>();
            try
            {
                itemTypes = _itemTypeManager.RetrieveAllItemTypesString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            foreach (var item in itemTypes)
            {
                cboItemType.Items.Add(item);
            }
        }
        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/30
        /// 
        /// Method to setup the window for a readonly permission set.
        /// </summary>
        private void SetupReadOnly(int itemID)
        {
            this.txtDescription.IsReadOnly = true;
            this.txtName.IsReadOnly = true;
            this.txtOnHand.IsReadOnly = true;
            this.txtReorder.IsReadOnly = true;
            this.txtPrice.IsReadOnly = true;
            this.cbPurchasable.IsEnabled = false;
            this.cbActive.IsEnabled = false;
            this.cboItemType.IsEnabled = false;
            this.btnSubmit.Visibility = Visibility.Hidden;
            this.btnEdit.Visibility = Visibility.Visible;
            this.lblDateActive.Visibility = Visibility.Visible;
            this.lblActive.Visibility = Visibility.Visible;
            this.dpDateActive.Visibility = Visibility.Visible;
            this.cbActive.Visibility = Visibility.Visible;
            this.dpDateActive.SelectedDate = oldItem.DateActive;
            if (oldItem.Active)
            {
                this.cbActive.IsChecked = true;
            }
            else
            {
                this.cbActive.IsChecked = false; ;
            }
            if (oldItem.CustomerPurchasable)
            {
                this.cbPurchasable.IsChecked = true;
            }
            else
            {
                this.cbPurchasable.IsChecked = false; ;
            }
            if (cbPurchasable.IsChecked == true)
            {
                txtPrice.Visibility = Visibility.Visible;
                lblPrice.Visibility = Visibility.Visible;
            }
            else
            {
                txtPrice.Visibility = Visibility.Collapsed;
                lblPrice.Visibility = Visibility.Collapsed;

            }
            this.Title = "View Item";
        }
        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/30
        /// 
        /// Method to set up the window for editing an existing item.
        /// </summary>
        private void SetupEditable()
        {
            this.txtDescription.IsReadOnly = false;
            this.txtName.IsReadOnly = false;
            this.txtOnHand.IsReadOnly = false;
            this.txtReorder.IsReadOnly = false;
            this.txtPrice.IsReadOnly = false;
            this.cbPurchasable.IsEnabled = true;
            this.cbActive.IsEnabled = true;
            this.dpDateActive.IsEnabled = false;
            cboItemType.IsEnabled = true;
            this.btnSubmit.Visibility = Visibility.Visible;
            this.btnEdit.Visibility = Visibility.Hidden;
            this.lblDateActive.Visibility = Visibility.Visible;
            this.lblActive.Visibility = Visibility.Visible;
            this.dpDateActive.Visibility = Visibility.Visible;
            this.cbActive.Visibility = Visibility.Visible;
            this.dpDateActive.SelectedDate = oldItem.DateActive;
            if (oldItem.Active)
            {
                this.cbActive.IsChecked = true;
            }
            else
            {
                this.cbActive.IsChecked = false; ;
            }
            if (oldItem.CustomerPurchasable)
            {
                this.cbPurchasable.IsChecked = true;
            }
            else
            {
                this.cbPurchasable.IsChecked = false; ;
            }
            if (cbPurchasable.IsChecked == true)
            {
                txtPrice.Visibility = Visibility.Visible;
                lblPrice.Visibility = Visibility.Visible;
            }
            else
            {
                txtPrice.Visibility = Visibility.Collapsed;
                lblPrice.Visibility = Visibility.Collapsed;

            }
            this.Title = "Edit Item";
            this.btnSubmit.Content = "Save";
        }
        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/21
        /// 
        /// Method to handle the event of the secondary button being clicked.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SetupEditable();
        }

        /// <summary>
        ///Jared Greenfield
        /// Created: 2019/04/04
        /// 
        /// MHides Price input if it's not needed.
        /// </summary>
        private void cbPurchasable_Click(object sender, RoutedEventArgs e)
        {
            if (cbPurchasable.IsChecked == true)
            {
                txtPrice.Visibility = Visibility.Visible;
                lblPrice.Visibility = Visibility.Visible;
            }
            else
            {
                txtPrice.Visibility = Visibility.Collapsed;
                lblPrice.Visibility = Visibility.Collapsed;
                
            }
        }
    }
}
