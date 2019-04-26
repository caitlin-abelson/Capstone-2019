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
        private int ID;
        private List<SpecialOrderLine> _OrderLine = new List<SpecialOrderLine>();
        private CompleteSpecialOrder _completespecialOrder;
        private SpecialOrderLine _specialOrderLine;
        private CompleteSpecialOrder _selected;
        private SpecialOrderManagerMSSQL _specialOrderLogic = new SpecialOrderManagerMSSQL();
        private SpecialOrderManagerMSSQL _specialOrderLogicID = new SpecialOrderManagerMSSQL();
        BrowseSpecialOrder refreshBrowse = new BrowseSpecialOrder();

       

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
            AddItemBtn.Content = "Add";
            AddItemBtn.IsEnabled = false;
            this.SpecialOrderID.Visibility = Visibility.Hidden;
            this.labelSpecialOrderId.Visibility = Visibility.Hidden;
            ID = 0;

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
            AddItemBtn.Content = "Add Items";
            btnAddOrder.Content = "Edit";
            setReadOnly();
            setDetails();

        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {

            if (btnAddOrder.Content == "Add")
            {
                try
                {
                    if (InputUser())
                    {
                        try
                        {

                            if (true == _specialOrderLogic.CreateSpecialOrder(_completespecialOrder))
                            {

                                MessageBox.Show("New Order Added Succesfully");
                                ID = _specialOrderLogic.retrieveSpecialOrderID(_completespecialOrder);
                                Items order = new Items(ID);
                                order.Show();
                                this.Close();
                                refreshBrowse.updateList();
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Unable to add New Order to Data Base");
                        }

                    } 

                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show("Processor Usage" + ex.Message);
                }

            }
            else if (btnAddOrder.Content == "Edit")
            {
                setAddUpdate();
                btnAddOrder.Content = "Save";

            }
            else if (btnAddOrder.Content == "Save")
            {
                if (InputUser())
                {

                    try
                    {
                        _specialOrderLogic.EditSpecialOrder(_selected, _completespecialOrder);
                        this.DialogResult = true;
                        MessageBox.Show("Update was Successful");
                        refreshBrowse.updateList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Update Failed!");
                    }
                    
                } 
            }
        }


        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/01/30
        /// 
        /// Input from the user when creating a new order.
        /// </summary>
        public bool InputUser()
        {
            bool result = false;
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
                        DateOrdered = DateTime.Parse(InputDateOrdered.Text),
                        Supplier = InputSupplierID.Text,
                        //Authorized = InputAuthorized.Text
                    };

                    result = true;
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Processor Usage" + ex.Message);
            }
            return result;
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



        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/16
        /// 
        ///Setting the input fields for add or update.//Load the list of employeeId to the combo box
        ///
        /// </summary>
        public void InputEmployeeID_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var comboBox = sender as ComboBox;
                comboBox.ItemsSource = _specialOrderLogic.employeeID();

            }
            catch(Exception ex)
            {
                MessageBox.Show(" Employee ID could not be retrieved.");
            };

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
        ///Setting the input fields for read-only.
        ///
        /// </summary>
        public void setReadOnly()
        {

            SpecialOrderID.IsReadOnly = true;
            InputEmployeeID.IsEnabled = false;
            InputDescription.IsEnabled = false;
            InputDateOrdered.IsEnabled = false;
            InputSupplierID.IsEnabled = false;
            InputAuthorized.IsReadOnly = true;

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
            InputDateOrdered.IsEnabled = false;
            InputSupplierID.IsEnabled = true;
            InputAuthorized.IsReadOnly = true;
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
            InputDateOrdered.Text = _selected.DateOrdered.ToShortDateString();
            InputSupplierID.Text = _selected.Supplier;
            InputAuthorized.Text = _selected.Authorized;



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
                MessageBox.Show("Supplier must be filled in, please try again");
                valid = false;
            }
            else if (InputDescription.Text == "" || InputDescription.Text == null || InputDescription.Text.Length > 1000)
            {
                MessageBox.Show("Invalid Entry for Description, please try again");
                valid = false;
            }


            return valid;
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (AddItemBtn.Content == "Add")
            {

                Items order = new Items();
                order.ShowDialog();



            }
            else if (AddItemBtn.Content == "Add Items")

            {

                int ID = Int32.Parse(SpecialOrderID.Text);
                Items order = new Items(ID);
                order.ShowDialog();
                updateList();

            }

        }


        private void GridItems_Load(object sender, RoutedEventArgs e)
        {

            if (btnAddOrder.Content == "Add")
            {



            }
            else if (btnAddOrder.Content == "Edit")
            {
                updateList();
            }
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/04/01
        /// 
        /// Click on item, pop up new window with details of item.
        ///
        /// </summary>
        private void GridItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // pop up a detail window
            var selected = (SpecialOrderLine)GridItems.SelectedItem;
            var detailA = new Items(selected);
            detailA.ShowDialog();

        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/04/15
        /// 
        /// Click on button, to authorize order.
        ///
        /// </summary>
        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            int ID = Int32.Parse(SpecialOrderID.Text);
           
            // string email = _employee.getEmail();
            //_specialOrderLogic.AuthenticatedBy(ID, email);
          
        }


        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/04/15
        /// 
        /// Load DataGrid.
        ///
        /// </summary>
        public void updateList()
        {
            try
            {
                int iD = Int32.Parse(SpecialOrderID.Text);
                GridItems.ItemsSource = _specialOrderLogic.RetrieveOrderLinesByID(iD);
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Can't retrieve data grid");
            }
        }

    }
}
