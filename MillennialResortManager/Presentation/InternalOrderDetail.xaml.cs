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
    /// Richard Carroll
    /// Created: 2019/01/30
    /// 
    /// This is the Window used for inserting/viewing 
    /// Internal Orders and the lines associated with them.
    /// </summary>
    public partial class TestWindow : Window
    {
        VMInternalOrder order = new VMInternalOrder();
        private bool isEditable;
        private User user = null;
        private List<string> _supplierNames = new List<string>();
        private List<string> _itemNames = new List<string>();
        private List<Item> _items = new List<Item>();
        private ItemManager _itemManager = new ItemManager();
        private InternalOrderManager _internalOrderManager = new InternalOrderManager();
        private List<string> _options = new List<string>();
        private List<VMInternalOrderLine> lines = new List<VMInternalOrderLine>();

        public TestWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// The First constructor for the window.
        /// This is used when inserting orders.
        /// </summary>
        public TestWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            isEditable = true;
            setupUserFields();

        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Secondary constructor for the window.
        /// This is used when viewing orders.
        /// </summary>

        public TestWindow(VMInternalOrder order)
        {
            InitializeComponent();
            this.order = order;
            isEditable = false;
            setupViewing();

        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// When in view mode, this method disables all fields and only 
        /// allows for selecting lines to view their details.
        /// </summary>
        private void setupViewing()
        {
            txtEmployeeID.Text = order.EmployeeID.ToString();
            _options.Add("Yes");
            _options.Add("No");
            cboOrderComplete.ItemsSource = _options;
            if (order.OrderComplete)
            {
                cboOrderComplete.SelectedIndex = 0;
            }
            else
            {
                cboOrderComplete.SelectedIndex = 1;
            }
            dtpDateOrdered.SelectedDate = order.DateOrdered;
            txtDescription.Text = order.Description;
            lblAddViewItems.Content = "View Items Below";
            try
            {
                lines = _internalOrderManager.RetrieveOrderLinesByID(order.InternalOrderID);
                refreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Retrieve order lines: \n" + ex.Message);
            }
            btnAddLine.Visibility = Visibility.Hidden;
            btnDeleteLine.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Content = "Back";
            cboItemID.IsEnabled = false;
            txtOrderQty.IsReadOnly = true;
            txtQtyReceived.IsReadOnly = true;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// This method is used in editing mode to fill in 
        /// employee information as it was passed from 
        /// the previous window.
        /// </summary>
        private void setupUserFields()
        {
            txtEmployeeID.Text = user.UserID.ToString();
            txtDepartmentID.Text = user.DepartmentID;

            _options.Add("Yes");
            _options.Add("No");

            cboItemID.ItemsSource = _itemNames;
            cboOrderComplete.ItemsSource = _options;
            cboOrderComplete.SelectedIndex = 1;
            cboOrderComplete.IsEnabled = false;

        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// This is used primarily to fill in the combo box
        /// for the item IDs.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)

        {
            dtpDateOrdered.SelectedDate = DateTime.Now;
            try
            {
                _items = _itemManager.RetrieveItemNamesAndIDs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Retrieve Item List: \n" + ex.Message);
            }
            foreach (var item in _items)
            {
                _itemNames.Add(item.Name);
            }
            


        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Checks the validity of the Order Fields that don't
        /// go with individual lines.
        /// </summary>
        private bool checkOrderFields()
        {
            bool isGoodData = true;
            
            if (txtEmployeeID.Text == "" || txtEmployeeID.Text == null)
            {
                MessageBox.Show("EmployeeID must be filled in, please try again");
                isGoodData = false;
            }
            else if (cboOrderComplete.SelectedIndex == -1)
            {
                MessageBox.Show("Order Complete must be selected, please try again");
                isGoodData = false;
            }
            else if (txtDepartmentID.Text == "" || txtDepartmentID.Text == null || txtDepartmentID.Text.Length > 50)
            {
                MessageBox.Show("Invalid Entry for Department ID, please try again");
                isGoodData = false;
            }
            else if (txtDescription.Text.Length > 1000)
            {
                MessageBox.Show("Description is too long, please try again");
                isGoodData = false;
            }

            return isGoodData;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Checks the validity of the Order Line Fields
        /// before submitting them to the list.
        /// </summary>
        private bool checkLineFields()
        {
            bool isGoodData = true;
            int intNumber;

            if (cboItemID.SelectedIndex == -1)
            {
                MessageBox.Show("Item ID must be selected, please try again");
                isGoodData = false;
            }
            else if (txtOrderQty.Text == "" || txtOrderQty.Text == null || !int.TryParse(txtOrderQty.Text, out intNumber))
            {
                MessageBox.Show("Invalid Entry for Order Quantity, please try again");
                isGoodData = false;
            }
            else if (txtOrderQty.Text == "" || txtOrderQty.Text == null || !int.TryParse(txtQtyReceived.Text, out intNumber))
            {
                MessageBox.Show("Invalid Entry for Unit Price, please try again");
                isGoodData = false;
            }

            return isGoodData;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Checks validity of the data in the Order Line Fields 
        /// before placing them into a temporary list that will 
        /// be displayed in the data grid.
        /// </summary>
        private void BtnAddLine_Click(object sender, RoutedEventArgs e)
        {
            if (checkLineFields())
            {
                VMInternalOrderLine line = new VMInternalOrderLine();
                line.ItemID = _items[cboItemID.SelectedIndex].ItemID;
                line.ItemName = cboItemID.SelectedItem.ToString();
                line.OrderQty = int.Parse(txtOrderQty.Text);
                line.QtyReceived = int.Parse(txtQtyReceived.Text);
                lines.Add(line);
                refreshGrid();
                clearFields();

            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Sets the Order Line Fields to their default state.
        /// </summary>
        private void clearFields()
        {
            cboItemID.SelectedIndex = -1;
            txtOrderQty.Text = "";
            txtQtyReceived.Text = "";
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Takes data from the temporary list and populates 
        /// the data grid with said data.
        /// </summary>
        private void refreshGrid()
        {
            lineGrid.ItemsSource = null;
            lineGrid.ItemsSource = lines;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Pulls up a confirmation window before allowing the user
        /// to delete a line from the temporary list and the 
        /// grid that takes from it.
        /// </summary>
        private void BtnDeleteLine_Click(object sender, RoutedEventArgs e)
        {
            if (lineGrid.SelectedIndex != -1)
            {
                var result = MessageBox.Show("Are you sure you want to delete this line?", 
                                    "Removing line...", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    lines.Remove((VMInternalOrderLine)lineGrid.SelectedItem);
                    refreshGrid();
                }
            }
        }


        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Pulls Order Data from the fields and Line data from 
        /// the temporary list to call the insert method in the Logic Layer
        /// </summary>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkOrderFields() && lines.Count != 0)
            {
                InternalOrder internalOrder = new InternalOrder();
                internalOrder.EmployeeID = int.Parse(txtEmployeeID.Text);
                internalOrder.OrderComplete = ((string)cboOrderComplete.SelectedItem == "Yes");
                internalOrder.DateOrdered = dtpDateOrdered.DisplayDate;
                internalOrder.Description = txtDescription.Text;
                internalOrder.DepartmentID = txtDepartmentID.Text;

                try
                {
                    _internalOrderManager.CreateInternalOrder(internalOrder, lines);
                    MessageBox.Show("Order Submitted Successfully");
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to Insert Internal order records: \n" +
                                        ex.Message);
                }
            }
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Populates List data into the Entry fields while in 
        /// view mode.
        /// </summary>
        private void LineGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isEditable)
            {
                var line = (VMInternalOrderLine)lineGrid.SelectedItem;
                List<string> options = new List<string>();
                options.Add(line.ItemName);
                cboItemID.ItemsSource = options;
                cboItemID.SelectedIndex = 0;
                txtOrderQty.Text = line.OrderQty.ToString();
                txtQtyReceived.Text = line.QtyReceived.ToString();

            }
        }


        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// Exits the window without requesting confirmation in 
        /// viewing mode,
        /// and pops up a window requesting confirmation in 
        /// editing mode.
        /// </summary>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (isEditable)
            {
                var result = MessageBox.Show("Are you sure you want to exit? \n " +
                    "Changes will be Discarded!", "Canceling Order.",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}
