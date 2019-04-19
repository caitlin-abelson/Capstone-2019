using DataObjects;
using LogicLayer;
using System;
using System.Windows;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for Items.xaml
    /// </summary>
    public partial class Items : Window
    {
        private int ID;
        private SpecialOrderLine _specialOrderLine;
        private SpecialOrderLine _selected;
        private SpecialOrderManagerMSSQL _specialOrderLogic = new SpecialOrderManagerMSSQL();
        BrowseSpecialOrder refreshBrowse = new BrowseSpecialOrder();
            

        public Items()
        {
            InitializeComponent();
            this.DeleteItem_Btn.Visibility = Visibility.Hidden;

        }

        public Items(int iD)
        {
            InitializeComponent();
            ID = iD;
            setAddItemToOrder(ID);
            EditSaveItem_Btn.Content = "Add";
            this.DeleteItem_Btn.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/04/04
        /// 
        /// Contructor that receives and object order line, with
        /// details of a specific Item from a specifi order ID, that now
        /// can be edited.
        ///
        /// </summary>

       public Items(SpecialOrderLine selected)
        {
            InitializeComponent();
            _selected = selected;
            setDetails();
            setReadOnly();
            EditSaveItem_Btn.Content = "Edit";
           
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/16
        /// 
        /// Assigns the values, copied from previous window,
        /// to this window. 
        ///
        /// </summary>
        private void setDetails()
        {
            
            ItemName_TxtBox.Text = _selected.NameID.ToString();
            SpecialOrderId_TxtBox.Text = _selected.SpecialOrderID.ToString();
            ItemDescription_TxtBox.Text = _selected.Description;
            QtyItems_Count.Text = _selected.OrderQty.ToString();
            QtyReceived_Count.Text = _selected.QtyReceived.ToString();
            
        }

        // <summary>
        /// Carlos Arzu
        /// Created: 2019/04/05
        /// 
        ///Setting the input fields for add item to existing order.
        ///
        /// </summary>
        public void setAddItemToOrder(int ID)
        {
            SpecialOrderId_TxtBox.IsReadOnly = true;
            EditSaveItem_Btn.Content = "Save";
            SpecialOrderId_TxtBox.Text = ID.ToString();
          
        }

        // <summary>
        /// Carlos Arzu
        /// Created: 2019/04/05
        /// 
        ///Setting the input fields for read-only.
        ///
        /// </summary>
        public void setReadOnly()
        {

            SpecialOrderId_TxtBox.IsReadOnly = true;
            ItemName_TxtBox.IsEnabled = false;
            ItemDescription_TxtBox.IsEnabled = false;
            QtyItems_Count.IsEnabled = false;
            QtyReceived_Count.IsEnabled = false;

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

            SpecialOrderId_TxtBox.IsReadOnly = true;
            ItemName_TxtBox.IsEnabled = true;
            ItemDescription_TxtBox.IsEnabled = true;
            QtyItems_Count.IsEnabled = true;
            QtyReceived_Count.IsEnabled = true;
           
        }

       
        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/20
        /// 
        /// Checks if all requirements were met by user, if not, it shows a message error. 
        ///
        /// </summary>
        private bool TestDataOrder()
        {
            bool valid = true;

            if (ItemName_TxtBox.Text == "")
            {
                MessageBox.Show("Invalid Entry for NameID of Item, please try again");
                valid = false;
            }
            else if (ItemDescription_TxtBox.Text == "")
            {
                MessageBox.Show("Invalid Entry for Description, please try again");
                valid = false;
            }
            else if (QtyItems_Count.Text == "")
            {
                MessageBox.Show("Invalid Entry, you must specified a quantity, please try again");
                valid = false;
            }
            else if (QtyReceived_Count.Text == "")
            {
                MessageBox.Show("Invalid Entry, you must specified a quantity received, even if its" +
                    "0 , please try again");
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/04/05
        /// 
        /// Saves the new Input by user, as a new Item in orderline in our DB.
        ///
        /// </summary>
        private void EditSaveItem_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (EditSaveItem_Btn.Content == "Add")
            {
                try
                {
                    if (InputUser())
                    {



                        try
                        {

                            if (true == _specialOrderLogic.CreateSpecialOrderLine(_specialOrderLine))
                            {

                                MessageBox.Show("New Item Added Succesfully");
                                this.Close();
                                refreshBrowse.updateList();

                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Unable to add New Item to Data Base");
                        }
                    }
                    
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show("Processor Usage" + ex.Message);
                }

            }
            else if  (EditSaveItem_Btn.Content == "Edit")
            {
                setAddUpdate();
                EditSaveItem_Btn.Content = "Save";
                this.DeleteItem_Btn.Visibility = Visibility.Hidden;

            }
            else if (EditSaveItem_Btn.Content == "Save")
            {
                InputUser();

                try
                {
                    _specialOrderLogic.EditSpecialOrderLine(_selected, _specialOrderLine );
                    this.DialogResult = true;
                    MessageBox.Show("Update was Successful");
                    this.Close();
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
        /// Input from the user when creating a new Item.
        /// </summary>
        public bool InputUser()
        {
            bool result = false;
            try
            {
                if (TestDataOrder())
                {

                  _specialOrderLine = new SpecialOrderLine()
                    {
                        //User input for new order form
                        NameID = ItemName_TxtBox.Text,
                        SpecialOrderID = Int32.Parse(SpecialOrderId_TxtBox.Text),
                        Description = ItemDescription_TxtBox.Text,
                        OrderQty = Int32.Parse(QtyItems_Count.Text),
                        QtyReceived = Int32.Parse(QtyReceived_Count.Text)
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

        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DeleteItem_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = Int32.Parse(SpecialOrderId_TxtBox.Text);
                string NameID = ItemName_TxtBox.Text;
            
                _specialOrderLogic.DeleteItem(ID, NameID);
                this.DialogResult = true;

                MessageBox.Show("Item Deleted Succesfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Failed!");
            }


            
        }
    }
}
