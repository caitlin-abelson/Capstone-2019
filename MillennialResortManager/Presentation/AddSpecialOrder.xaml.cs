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
    /// Interaction logic for AddSpecialOrder.xaml
    /// </summary>
    public partial class AddSpecialOrder : Window
    {

        private List<SpecialOrderLine> _OrderLine = new List<SpecialOrderLine>();
        private CompleteSpecialOrder _completespecialOrder;
        private SpecialOrderLine _specialOrderLine;
        private CompleteSpecialOrder _selected;
        private SpecialOrderManagerMSSQL _specialOrderLogic = new SpecialOrderManagerMSSQL();
        private SpecialOrderManagerMSSQL _specialOrderLogicID = new SpecialOrderManagerMSSQL();

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/01/30
        /// 
        /// The Default Constructor.
        /// 
        /// </summary>
        public AddSpecialOrder()
        {
            InitializeComponent();
            InputDateOrdered.Text = DateTime.Now.Date.ToShortDateString();
            setAddUpdate();
            btnAddOrder.Content = "Add";
            Btn_AddItem.Content = "Add";
            lbl_AddItem.Content = "Add Item";

        }              

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/01/30
        /// 
        /// The Second Constructor.
        /// Set the the order selected in a new window, with read only
        /// data, with the option to edit.
        /// </summary>
        public AddSpecialOrder(CompleteSpecialOrder selected)
        {
            InitializeComponent();
            _selected = selected;
            setReadOnly();
            setDetails();

        }
		
        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
           
            if (btnAddOrder.Content == "Add")
            {
                
                    try
                    {
                            
                            InputUser();
                        
                        try
                        {

                            if (true == _specialOrderLogic.CreateSpecialOrder(_completespecialOrder, _specialOrderLine))
                            {

                                MessageBox.Show("New Item Added Succesfully");
                            }


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Unable to add New Item to Data Base");
                        }

                        this.Close();
                    }
                    catch (NullReferenceException ex)
                    {
                        MessageBox.Show("Processor Usage" + ex.Message);
                    }
              
              
            }
            else if (btnAddOrder.Content == "Save")
           
            {
                   InputUser();

                    try
                    {
                        _specialOrderLogic.EditSpecialOrder(_selected, _completespecialOrder);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Update Failed!");
                    }
                   
            }
         
        }
        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/01/30
        /// 
        /// Input from the user when creating a new order.
        /// </summary>
        public void InputUser()
        {
            AddItem order = new AddItem();
            SpecialOrderID.IsEnabled = false;
            try
            {
                if (TestDataOrder())
                {

                    _completespecialOrder = new CompleteSpecialOrder()
                    {

                        //User input for new order form

                        EmployeeID = Int32.Parse(InputEmployeeID.Text),
                        Description = InputDescription.Text,
                        OrderComplete = (bool)OrderComplete.IsChecked,
                        DateOrdered = DateTime.Parse(InputDateOrdered.Text),
                        SupplierID = Int32.Parse(InputSupplierID.Text)

                    };

                    _specialOrderLine = new SpecialOrderLine()
                    {

                        Description = InputDescription.Text
                        /*ItemID = Int32.Parse(order.InputItemID.Text),
                        OrderQty = Int32.Parse(order.QuantityNeed.Text),
                        QtyReceived = Int32.Parse(order.InputQTYRec.Text)*/


                    };
                }
            }
            catch(NullReferenceException ex)
            {
                MessageBox.Show("Processor Usage" + ex.Message);
            }
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/16
        /// 
        ///Returns to previous Window
        ///
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        /*private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
           
            InputUser();
            
            try
            {

                if (true == _specialOrderLogic.CreateSpecialOrder(_completespecialOrder, _specialOrderLine))
                {

                    MessageBox.Show("New Item Added Succesfully");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to add New Item to Data Base");
            }
        
            this.Close();

        }*/

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/16
        /// 
        ///Setting the input fields for add or update.//Load the list of employeeId to the combo box
        ///
        /// </summary>
        public void InputEmployeeID_Loaded(object sender, RoutedEventArgs e)
        {
            

            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = _specialOrderLogic.employeeID();
           
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/16
        /// 
        ///Manages the scroll change of selection of employeeID in the combobox
        ///
        /// </summary>
        private void InputEmployeeID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            string value = comboBox.SelectedItem as string;
            this.Title = "Selected:" + value;

        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/16
        /// 
        /// Filling Combo box the Supplier ID .
        ///
        /// </summary>
        private void InputItemID_Loaded(object sender, RoutedEventArgs e)
        {
           
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = _specialOrderLogic.listOfitemID();
         

        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/16
        /// 
        /// Filling Combo box the Supplier ID .
        ///
        /// </summary>
        private void InputSupplierID_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = _specialOrderLogic.employeeID();

        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/16
        /// 
        ///Setting the input fields for read-only.
        ///
        /// </summary>
        public void setReadOnly()
        {

            SpecialOrderID.IsReadOnly = true;
            InputEmployeeID.IsEnabled = false;
            InputDescription.IsEnabled = false;
            OrderComplete.IsEnabled = false;
            InputDateOrdered.IsEnabled = false;
            InputSupplierID.IsEnabled = false;

        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/16
        /// 
        /// Setting the input fields for add or update.
        /// window.
        ///
        /// </summary>
        public void setAddUpdate()
        {

            SpecialOrderID.IsReadOnly = true;
            InputEmployeeID.IsEnabled = true;
            InputDescription.IsEnabled = true;
            OrderComplete.IsEnabled = true;
            InputDateOrdered.IsEnabled = false;
            InputSupplierID.IsEnabled = true;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/16
        /// 
        /// Assigns the values of the order in the browser window, to the edit'
        /// window.
        ///
        /// </summary>
        private void setDetails()
        {

            SpecialOrderID.Text = _selected.SpecialOrderID.ToString();
            InputEmployeeID.SelectedItem = _selected.EmployeeID;
            InputDescription.Text = _selected.Description;
            OrderComplete.IsChecked = _selected.OrderComplete;
            InputDateOrdered.Text = _selected.DateOrdered.ToShortDateString();
            InputSupplierID.SelectedItem = _selected.SupplierID;
            InputSupplierID.Text = _selected.SupplierID.ToString();
            /* InputItemID.Text = _selected.ItemID.ToString();
             InputQTYRec.Text = _selected.QtyReceived.ToString();
             QuantityUpDown.Text = _specialOrderLine.OrderQty.ToString(); */
            

        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/20
        /// 
        /// Checks the input data if it's valid
        ///
        /// </summary>
        private bool TestDataOrder()
        {
            bool valid = true;

            if (InputEmployeeID.Text == "" || InputEmployeeID.Text == null)
            {
                MessageBox.Show("EmployeeID must be filled in, please try again");
                valid = false;
            }
            else if (InputSupplierID.Text == "" || InputSupplierID.Text == null)
            {
                MessageBox.Show("SupplierID must be filled in, please try again");
                valid = false;
            }
            else if (InputDescription.Text == "" || InputDescription.Text == null || InputDescription.Text.Length > 1000)
            {
                MessageBox.Show("Invalid Entry for Description, please try again");
                valid = false;
            }
            
            
            return valid;
        }
              
        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/20
        /// 
        /// Sets up the Order form ready to edit.
        ///
        /// </summary>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            setAddUpdate();
            btnAddOrder.Content = "Save";
        }

        private void Btn_AddItem_Click(object sender, RoutedEventArgs e)
        {


            if (Btn_AddItem.Content == "Add")
            {

                AddItem();


            }
            else if (Btn_AddItem.Content == "Browse")

            {
                
                try
                {
                    _specialOrderLogic.EditSpecialOrder(_selected, _completespecialOrder);
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Update Failed!");
                }

            }
                       
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/20
        /// 
        /// Calls the add item form to add items.
        ///
        /// </summary>
        private void AddItem()
        {

            //int orderId = Int32.Parse(SpecialOrderID.Text);
            AddItem order = new AddItem();

            order.Show();
        }
    }
}
