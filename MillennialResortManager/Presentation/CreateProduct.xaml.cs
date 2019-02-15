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
	/// Kevin Broskow
	/// Created: 2019/01/20
	/// 
	/// Window for creation of a new product
	/// </summary>
    public partial class CreateProduct : Window
    {
        ItemTypeManagerMSSQL _itemTypeManager = new ItemTypeManagerMSSQL();
        ProductManagerMSSQL _productManager = new ProductManagerMSSQL();
        Product oldProduct = new Product();
        Product newProduct = new Product();

        public CreateProduct()
        {
            InitializeComponent();
            this.btnEdit.Visibility = Visibility.Hidden;
            this.lblDateActive.Visibility = Visibility.Hidden;
            this.lblActive.Visibility = Visibility.Hidden;
            this.dpDateActive.Visibility = Visibility.Hidden;
            this.cbActive.Visibility = Visibility.Hidden;
        }
        public CreateProduct(Product selectedProduct)
        {
            oldProduct = selectedProduct;
            InitializeComponent();
            SetupReadOnly(selectedProduct.ProductID);
            this.txtDescription.IsReadOnly = true;
            this.txtName.IsReadOnly = true;
            this.txtOnHand.IsReadOnly = true;
            this.txtReorder.IsReadOnly = true;
            this.txtRecipeID.IsReadOnly = true;
            this.cbPurchasable.IsEnabled = false;
            this.cbActive.IsEnabled = false;
            this.dpDateActive.IsEnabled = false;
            this.btnSubmit.Visibility = Visibility.Hidden;
            this.btnEdit.Visibility = Visibility.Visible;
            this.lblDateActive.Visibility = Visibility.Visible;
            this.lblActive.Visibility = Visibility.Visible;
            this.dpDateActive.Visibility = Visibility.Visible;
            this.cbActive.Visibility = Visibility.Visible;
            this.dpDateActive.SelectedDate = selectedProduct.DateActive;
            this.txtDescription.Text = selectedProduct.Description;
            this.txtName.Text = selectedProduct.Name;
            this.txtOnHand.Text = selectedProduct.OnHandQty.ToString();
            this.txtReorder.Text = selectedProduct.ReorderQty.ToString();
            if (selectedProduct.Active)
            {
                this.cbActive.IsChecked = true;
            }
            if (selectedProduct.CustomerPurchasable)
            {
                this.cbPurchasable.IsChecked = true;
            }
            this.cboItemType.SelectedItem = selectedProduct.ItemType;
            this.txtRecipeID.Text = selectedProduct.RecipeID.ToString();
            this.cbActive.IsChecked = true;
            this.Title = "View Product";
        }
        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/21
        /// 
        /// Method to handle the event of the primary button being clicked.
        /// </summary>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            if (!ValidateInput())
            {
                return;
            }
            if (((String)this.btnSubmit.Content) == "Submit")
            {
                try
                {
                    oldProduct.Name = this.txtName.Text;
                    oldProduct.OnHandQty = Int32.Parse(this.txtOnHand.Text);
                    oldProduct.ReorderQty = Int32.Parse(this.txtReorder.Text);
                    oldProduct.ItemType = this.cboItemType.SelectedItem.ToString();
                    oldProduct.Description = this.txtDescription.Text;
                    oldProduct.DateActive = DateTime.Now;
                    oldProduct.ProductID = _productManager.CreateProduct(oldProduct);
                    oldProduct.RecipeID = Int32.Parse(this.txtRecipeID.Text);
                    if ((bool)this.cbPurchasable.IsChecked)
                    {
                        oldProduct.CustomerPurchasable = true;
                    }else
                    {
                        oldProduct.CustomerPurchasable = false;
                    }
                    MessageBox.Show("Add worked ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else if(((String)this.btnSubmit.Content) == "Save")
            {
                newProduct.Name = this.txtName.Text;
                newProduct.OnHandQty = Int32.Parse(this.txtOnHand.Text);
                newProduct.ReorderQty = Int32.Parse(this.txtReorder.Text);
                newProduct.ItemType = this.cboItemType.SelectedItem.ToString();
                newProduct.Description = this.txtDescription.Text;
                newProduct.DateActive = (DateTime)this.dpDateActive.SelectedDate;
                MessageBox.Show(oldProduct.ProductID.ToString());
                newProduct.RecipeID = Int32.Parse(this.txtRecipeID.Text);
                if ((bool)this.cbPurchasable.IsChecked)
                {
                    newProduct.CustomerPurchasable = true;
                }
                else
                {
                    oldProduct.CustomerPurchasable = false;
                }
                if (!(bool)this.cbActive.IsChecked)
                {
                    newProduct.Active = false;
                }
                _productManager.UpdateProduct(newProduct, oldProduct);
                    
                }
            this.Close();
        }
        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/21
        /// 
        /// Method to check for valid data.
        /// </summary>
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
                                if (ValidRecipeID())
                                {

                                    MessageBox.Show("Valid Data");
                                    return true;
                                }
                                else
                                {
                                    MessageBox.Show("Invalid RecipeID");
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

        private bool ValidRecipeID()
        {
            if(this.txtRecipeID == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///Kevin Broskow
        /// Created: 2019/01/21
        /// 
        /// Method to check for valid ItemType Selection.
        /// </summary>
        private bool ValidItemType()
        {
            if(cboItemType.SelectedIndex > -1)
            {
                return true;
            }
            int n;
            bool canParse = Int32.TryParse(this.txtRecipeID.Text, out n);
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
            if (txtOnHand.Text == null || txtOnHand.Text == "")
            {
                return false;
            }
            if (int.Parse(txtOnHand.Text) >= 1)
            {
                return true;
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
                itemTypes = _itemTypeManager.RetrieveAllItemTypes();
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
        private void SetupReadOnly(int productID)
        {
            this.txtDescription.IsReadOnly = true;
            this.txtName.IsReadOnly = true;
            this.txtOnHand.IsReadOnly = true;
            this.txtReorder.IsReadOnly = true;
            this.txtRecipeID.IsReadOnly = true;
            this.cbPurchasable.IsEnabled = false;
            this.cbActive.IsEnabled = false;
            this.txtRecipeID.IsReadOnly = true;
            this.btnSubmit.Visibility = Visibility.Hidden;
            this.btnEdit.Visibility = Visibility.Visible;
            this.lblDateActive.Visibility = Visibility.Visible;
            this.lblActive.Visibility = Visibility.Visible;
            this.dpDateActive.Visibility = Visibility.Visible;
            this.cbActive.Visibility = Visibility.Visible;
            this.dpDateActive.SelectedDate = oldProduct.DateActive; if (oldProduct.Active)
            {
                this.cbActive.IsChecked = true;
            }
            this.Title = "View Product";
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
            this.txtRecipeID.IsReadOnly = false;
            this.cbPurchasable.IsEnabled = true;
            this.cbActive.IsEnabled = true;
            this.txtRecipeID.IsReadOnly = false;
            this.dpDateActive.IsEnabled = false;
            this.btnSubmit.Visibility = Visibility.Visible;
            this.btnEdit.Visibility = Visibility.Hidden;
            this.lblDateActive.Visibility = Visibility.Visible;
            this.lblActive.Visibility = Visibility.Visible;
            this.dpDateActive.Visibility = Visibility.Visible;
            this.cbActive.Visibility = Visibility.Visible;
            this.dpDateActive.SelectedDate = oldProduct.DateActive;
            if (oldProduct.Active)
            {
                this.cbActive.IsChecked = true;
            }
            this.Title = "Edit Product";
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
    }
}
