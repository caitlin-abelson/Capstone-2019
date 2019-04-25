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
    /// Eric Bostwick 4/2/2019
    /// Form for Creating Pick Sheets, Completing Picksheets, and Delivering PickSheets
    /// Interaction logic for frmPickOrders.xaml
    /// </summary>
    public partial class frmPickOrders : Window
    {
        private PickManager _pickManager = new PickManager();
        private PickOrder _pickorder = new PickOrder();
        private List<PickOrder> _currentPickOrders;
        private List<PickOrder> _pickOrders;
        private DateTime _startDate = DateTime.Today.AddDays(-60);
        private List<PickSheet> _pickSheets;
        private List<PickOrder> _picksheetDetails;
        //private List<Order> _currentacknowledge;
        //private List<PickOrder> _picksheetDetails;
        private int _employeeID;
        private string _pickSheetID;
        private int _numPickedItems;

        public frmPickOrders()
        {          
            InitializeComponent();            
            if(_employeeID == 0)
            {
                _employeeID = 100001;
            }           
            dtpStartDate.SelectedDate = _startDate;
            SetupControls();
            LoadCreatePickGrid(false);
            //Load the Process Pick Sheet grid (Second Tab)
            dtpProcessPickStartDate.SelectedDate = _startDate;
            //Load the Deliver Pick Sheet grid (Third Tab)
            dtpDeliveryStartDate.SelectedDate = _startDate;
            LoadPickSheetHeaderGrid((DateTime)dtpProcessPickStartDate.SelectedDate);
            dtpDeliveryStartDate.SelectedDate = _startDate;
            LoadPickSheetDeliveryGrid(_startDate);
           
        }

        private void LoadCreatePickGrid(bool hidePicked)
        {
            try
            {
                _pickOrders = _pickManager.Select_Orders_For_Acknowledgement(dtpStartDate.SelectedDate.Value, hidePicked);
                dgPickCreate.ItemsSource = _pickOrders;
                _currentPickOrders = _pickOrders;               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Default values for form controls
        private void SetupControls()
        {
            txtPromptCreatePickSheet.Text = "Click Start a Pick Sheet\r\nto Create a New Pick Sheet";
            dgcPickChkBox.Visibility = Visibility.Hidden;            
        }

        private void LoadPickSheetHeaderGrid(DateTime startDate)
        {
            try
            {
                _pickSheets = _pickManager.Select_All_PickSheets_By_Date(startDate);
                dgProcessPickHeader.ItemsSource = _pickSheets.FindAll(p => p.PickDeliveredBy == 0);       
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TabCreatePickSheet_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tabsetPick_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void rbAllOrdersPick_Checked(object sender, RoutedEventArgs e)
        {
            LoadCreatePickGrid(false);
        }

        private void rbNewOrdersPick_Checked(object sender, RoutedEventArgs e)
        {
            LoadCreatePickGrid(true);
        }

        private void rbPickedOrders_Checked(object sender, RoutedEventArgs e)
        {
            FilterCreatePickGrid();
        }

        private void btnCancelPick_Click(object sender, RoutedEventArgs e)
        {
            var mbr = MessageBox.Show("Do You Want to Cancel PickSheet " + _pickSheetID + "? \r\n All Order Selections Will Be Deleted", "Cancel PickSheet", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
                string ordersDeleted = CancelPick(_pickSheetID).ToString();
                string info = ordersDeleted + " Orders Were Deleted From PickSheet " + _pickSheetID;
                MessageBox.Show(info);
                txtPickInfo.Text = info;
                btnStartPick.IsEnabled = true;
                btnEndPick.IsEnabled = false;
                btnCancelPick.IsEnabled = false;
                btnRefreshPickCreateGrid.IsEnabled = true;
                dgcPickChkBox.Visibility = Visibility.Hidden;
                _pickSheetID = "";
                txtPromptCreatePickSheet.Text = "Click Start a Pick Sheet\r\nto Create a New Pick Sheet";
                LoadCreatePickGrid(false);
                _numPickedItems = 0;
            }
            rbAllOrdersPick.IsEnabled = true;
            rbNewOrdersPick.IsEnabled = true;
            rbPickedOrders.IsEnabled = true;
        }

        private void btnEndPick_Click(object sender, RoutedEventArgs e)
        {
            if (_numPickedItems == 0)
            {
                string pickInfo = "Pick Cancelled";
                //MessageBox.Show(pickInfo);
                txtPickInfo.Text = pickInfo;
                btnStartPick.IsEnabled = true;
                btnEndPick.IsEnabled = false;
                btnCancelPick.IsEnabled = false;
                btnRefreshPickCreateGrid.IsEnabled = true;
                dgcPickChkBox.Visibility = Visibility.Hidden;
                txtPromptCreatePickSheet.Text = "Click Start a Pick Sheet\r\nto Create a New Pick Sheet";
                //txtPickInfo.Text = "";
                LoadCreatePickGrid(false);
                rbAllOrdersPick.IsEnabled = true;
                rbNewOrdersPick.IsEnabled = true;
                rbPickedOrders.IsEnabled = true;
                rbAllOrdersPick.IsChecked = true;
                return;
            }
            var mbr = MessageBox.Show("Do You Want to Commit Picked Items to the PickSheet?", "Committing PickSheet", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
                int ordersCommitted = 0;
                ordersCommitted = CommitOrders(_pickSheetID);
                string pickInfo = ordersCommitted + " Orders Committed to PickSheet " + _pickSheetID;
                MessageBox.Show(pickInfo);
                txtPickInfo.Text = "Last Pick " + pickInfo;
                _pickSheetID = "";
                btnStartPick.IsEnabled = true;
                btnEndPick.IsEnabled = false;
                btnCancelPick.IsEnabled = false;
                btnRefreshPickCreateGrid.IsEnabled = true;
                dgcPickChkBox.Visibility = Visibility.Hidden;
                txtPromptCreatePickSheet.Text = "Click Start a Pick Sheet\r\nto Create a New Pick Sheet";
                //txtPickInfo.Text = "";
                LoadCreatePickGrid(false);
                //We need to go back and add this
                LoadPickSheetHeaderGrid((DateTime)dtpProcessPickStartDate.SelectedDate);
                rbAllOrdersPick.IsChecked = true;
            }
           
        }
        private int CommitOrders(string picksheetID)
        {
            int result = 0;
            try
            {
                result = _pickManager.Insert_TmpPickSheet_To_PickSheet(picksheetID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
        private int CancelPick(string picksheetNumber)
        {
            int result = 0;
            try
            {
                result = _pickManager.Delete_TmpPickSheet(picksheetNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
        private void btnStartPick_Click(object sender, RoutedEventArgs e)
        {
            //Check if there any orders to pick
            if (!CheckForAvailableOrdersToPick())
            {
                MessageBox.Show("No Orders Available to Pick");
                return;
            }
            var mbr = MessageBox.Show("This Will Create a Pick Sheet.\r\n Is This What Want to Do?", "Creating PickSheet", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
                btnStartPick.IsEnabled = false;
                btnEndPick.IsEnabled = true;
                btnCancelPick.IsEnabled = true;
                btnRefreshPickCreateGrid.IsEnabled = false;
                LoadCreatePickGrid(true);
                rbNewOrdersPick.IsChecked = true;
                dgcPickChkBox.Visibility = Visibility.Visible;

                //Get a Pick Sheet Number
                _pickSheetID = GetPickSheetID();                
                txtPromptCreatePickSheet.Text = "Creating PickSheet\r\n" + _pickSheetID;
                rbAllOrdersPick.IsEnabled = false;
                rbNewOrdersPick.IsEnabled = false;
                rbPickedOrders.IsEnabled = false;
            }
        }
        private void chkPickItem_Checked(object sender, RoutedEventArgs e)
        {
            //create a picked object for setting up picking the item
            var ackorder = (PickOrder)dgPickCreate.SelectedItem;

            if (ackorder.OrderStatusView.Equals("ORDER ACKNOWLEDGED"))
            {
                MessageBox.Show("Item Has Already Been Picked");

                return;
            }

            //ackorder.PickSheetID = _picksheetID;
            PickSheet picked = new PickSheet();
            
            picked.TempPickSheetID = _pickSheetID;
            picked.PickSheetInternalOrderID = ackorder.InternalOrderID;
            picked.PickSheetCreatedBy = _employeeID;

            ackorder.PickSheetID = _pickSheetID;

            //Insert to tmpPickTable
            if (1 == InsertRecordToTempPickTable(ackorder))
            {

                txtPickInfo.Text = "OrderID: " + ackorder.InternalOrderID
                                    + "\nPartNum: " + ackorder.ItemID
                                    + " Description: " + ackorder.ItemDescription
                                    + " Qty: " + ackorder.OrderQty
                                    + "\nAdded to PickSheet " + picked.TempPickSheetID;
            } else
            {
                
                return;
            }
        }
        private void chkPickItem_Unchecked(object sender, RoutedEventArgs e)
        {
            var mbr = MessageBox.Show("Do You Really Want to Delete this Item From the PickSheet?", "Delete Item From Pick Sheet", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.No)
            {
                return;
            }
            //Will have to back out the item
            var ackorder = (PickOrder)dgPickCreate.SelectedItem;

            ackorder.PickSheetID = _pickSheetID;
            PickSheet picked = new PickSheet();
            picked.TempPickSheetID = _pickSheetID;
            picked.PickSheetInternalOrderID = ackorder.InternalOrderID;
            picked.PickSheetCreatedBy = _employeeID;

            //Delete From tmpPickTable
            try
            {
                _pickManager.Delete_TmpPickSheet_Item(ackorder);
                _numPickedItems -= 1;
                //LoadCreatePickGrid(false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            txtPickInfo.Text = "OrderID: " + ackorder.InternalOrderID
                                     + "\nPartNum: " + ackorder.ItemID
                                     + " Description: " + ackorder.ItemDescription
                                     + " Qty: " + ackorder.OrderQty
                                     + "\nRemoved From PickSheet " + picked.TempPickSheetID;
        }

        private int InsertRecordToTempPickTable(PickOrder pickOrder)
        {
            int result = 0;
            //Insert to tmpPickTable
            try
            {                
                result = _pickManager.Insert_Record_To_TmpPicksheet(pickOrder);
                //increment the number of picked items
                _numPickedItems += 1;               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);               
            }
            return result;
        }

            private void btnRefreshPickCreateGrid_Click(object sender, RoutedEventArgs e)
        {
            LoadCreatePickGrid(false);
        }

        private void dgPickCreate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }      

        private void FilterCreatePickGrid()
        {
            if (_currentPickOrders != null) //don't do this until we load the _currentacknowledge List
            {
                try
                {
                    if (rbPickedOrders.IsChecked == true)
                    {
                        LoadCreatePickGrid(false);
                        _currentPickOrders = _pickOrders.FindAll(p => p.OrderStatusView == "ORDER ACKNOWLEDGED");
                    }

                    dgPickCreate.ItemsSource = _currentPickOrders;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Check for available orders to pick returns false if there aren't any
        private bool CheckForAvailableOrdersToPick()
        {
            _pickOrders = (List<PickOrder>)dgPickCreate.ItemsSource;

            foreach (PickOrder order in _pickOrders)
            {
                if (order.PickSheetID.Equals(null) || order.PickSheetID.Equals(""))
                {
                    return true;
                }
            }
            return false; //couldn't find any available orders
        }

        private string GetPickSheetID()
        {
            string pickSheetID = null;
            try
            {
                pickSheetID = _pickManager.Select_Pick_Sheet_Number();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return pickSheetID.Trim();
        }

        private void btnRefreshProcessPickGrid_Click(object sender, RoutedEventArgs e)
        {
            LoadPickSheetHeaderGrid((DateTime)dtpProcessPickStartDate.SelectedDate);
        }

        private void BtnPPHeaderMarkClosed_Click(object sender, RoutedEventArgs e)
        {
            //Closed a picksheet
            bool reOpen = false;
            var selectedPickSheet = (PickSheet)dgProcessPickHeader.SelectedItem;
            if (selectedPickSheet.PickSheetStatus == 2)
            {
                var mbr = MessageBox.Show("Pick Sheet " + selectedPickSheet.PickSheetID + " Is Already Closed \n\rDo you want To Reopen it?", "ReOpen PickSheet ", MessageBoxButton.YesNo);
                if (mbr == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    reOpen = true;
                }
            }
            if (selectedPickSheet.PickSheetStatus == 1)
            {
                var mbr = MessageBox.Show("Pick Sheet " + selectedPickSheet.PickSheetID + " Will Be Closed \n\rIs this what you want to do?", "Close PickSheet ", MessageBoxButton.YesNo);
                if (mbr == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    reOpen = false;
                }
            }

            var newPickSheet = new PickSheet();

            newPickSheet = CopyPickSheet(selectedPickSheet);
            DateTime dt;
            if (reOpen)
            {
                dt = new DateTime(0001, 1, 1);                
                newPickSheet.PickCompletedDate = dt;
                newPickSheet.PickSheetStatus = 1;
                newPickSheet.PickCompletedBy = 0;
                newPickSheet.PickDeliveredBy = 0;
            }
            else
            {
                dt = DateTime.Now;
                newPickSheet.PickSheetStatus = 2;
                newPickSheet.PickCompletedDate = dt;
                newPickSheet.PickCompletedDateView = dt.ToString();
                newPickSheet.PickCompletedBy = _employeeID;
                newPickSheet.PickDeliveredBy = 0;
            }

            try
            {
                int result = _pickManager.UpdatePickSheet(newPickSheet, selectedPickSheet);
                _picksheetDetails = null;
                dgProcessPickDetail.ItemsSource = _picksheetDetails;
                LoadCreatePickGrid(false);
                LoadPickSheetHeaderGrid((DateTime)dtpProcessPickStartDate.SelectedDate);
                LoadPickSheetDeliveryGrid((DateTime)dtpDeliveryStartDate.SelectedDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadPickSheetDeliveryGrid(DateTime startDate)
        {
            try
            {
                _pickSheets = _pickManager.Select_All_Closed_PickSheets_By_Date(startDate);
                dgDeliverPickSheet.ItemsSource = _pickSheets;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static PickSheet CopyPickSheet(PickSheet pickSheet)
        {
            PickSheet _newPickSheet = new PickSheet()
            {
                PickSheetID = pickSheet.PickSheetID,                
                CreateDate = pickSheet.CreateDate,                
                PickCompletedBy = pickSheet.PickCompletedBy,
                PickCompletedDate = pickSheet.PickCompletedDate,
                PickDeliveredByName = pickSheet.PickDeliveredByName,
                PickDeliveryDate = pickSheet.PickDeliveryDate,
                PickDeliveredBy = pickSheet.PickDeliveredBy,
                PickCompletedByName = pickSheet.PickCompletedByName,
                PickCompletedDateView = pickSheet.PickCompletedDateView,
                PickDeliveryDateView = pickSheet.PickDeliveryDateView,
                PickSheetCreatedBy = pickSheet.PickSheetCreatedBy,
                PickSheetCreatedByName = pickSheet.PickSheetCreatedByName,
                PickSheetIDView = pickSheet.PickSheetIDView,
                PickSheetInternalOrderID = pickSheet.PickSheetInternalOrderID,
                PickSheetStatus = pickSheet.PickSheetStatus,
                TempPickSheetID = pickSheet.TempPickSheetID,
                NumberOfOrders = pickSheet.NumberOfOrders
            };
            return _newPickSheet;
        }

        private void btnPPHeaderPrint_Click(object sender, RoutedEventArgs e)
        {
            //Print the Picksheet 
            //Sends the picksheetId to the report
            try
            {
                var picksheet = (PickSheet)dgProcessPickHeader.SelectedItem;
                var picksheetreportForm = new PickSheetReport(picksheet.PickSheetID);
                var result = picksheetreportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPPHeaderShowDetails_Click(object sender, RoutedEventArgs e)
        {
            var picksheet = (PickSheet)dgProcessPickHeader.SelectedItem;
            if(picksheet.PickSheetStatus == 2)
            {
                MessageBox.Show("This is not allowed on a Closed Picksheet");
                return;
            }
            LoadPickSheetDetailGrid(picksheet.PickSheetID);
        }

        private void ChkPPDetailMarkOutOfStock_Checked(object sender, RoutedEventArgs e)
        {
            PickOrder _selectedOrder = (PickOrder)dgProcessPickDetail.SelectedItem;
            try
            {
                var x = _selectedOrder.OutOfStock;
            }
            catch
            {
                return;
            }

            //already done do nothing
            if (_selectedOrder.OutOfStock)
            {
                return;
            }
            var _newOrder = new PickOrder();
            //_newOrder = _selectedOrder;
            _newOrder = CopyOrder(_selectedOrder);
            //Mark the order out of stock
            _newOrder.OutOfStock = true;
            try
            {
                int orderComplete = _pickManager.Update_PickOrder(_newOrder, _selectedOrder);
                // MessageBox.Show("Order Updated " + orderComplete);
                LoadPickSheetDetailGrid(_newOrder.PickSheetID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChkPPDetailMarkOutOfStock_Unchecked(object sender, RoutedEventArgs e)
        {
            PickOrder _selectedOrder = (PickOrder)dgProcessPickDetail.SelectedItem;
            try
            {
                var x = _selectedOrder.OutOfStock;
            }
            catch
            {
                return;
            }

            //already done do nothing
            if (!_selectedOrder.OutOfStock)
            {
                return;
            }
            var _newOrder = new PickOrder();
            //_newOrder = _selectedOrder;
            _newOrder = CopyOrder(_selectedOrder);
            //Mark the order out of stock
            _newOrder.OutOfStock = false;
            try
            {
                int orderComplete = _pickManager.Update_PickOrder(_newOrder, _selectedOrder);
                // MessageBox.Show("Order Updated " + orderComplete);
                LoadPickSheetDetailGrid(_newOrder.PickSheetID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void TxtPPDetailFillQty_LostFocus(object sender, RoutedEventArgs e)
        {
            //var str = e.Source.ToString();            
            int fillQty = 0;
            //string[] y = x.Split(':');
            PickOrder _selectedOrder = (PickOrder)dgProcessPickDetail.SelectedItem;
            if(_selectedOrder == null)
            {
                return;
            }
            try
            {
                var strInput = e.Source.ToString();
                string[] strValue = strInput.Split(':');
                if (strValue.Length == 1)
                {
                    MessageBox.Show("Please Enter a Valid Quantity");
                    LoadPickSheetDetailGrid(_selectedOrder.PickSheetID);
                }
                if (strValue.Length == 2)
                {
                    //has to be an integer
                    if (!int.TryParse(strValue[1], out fillQty))
                    {
                        MessageBox.Show("Invalid Input " + strValue[1]);
                        LoadPickSheetDetailGrid(_selectedOrder.PickSheetID);
                        return;
                    }
                    //can't be more than was ordered
                    if (fillQty > _selectedOrder.OrderQty)
                    {
                        MessageBox.Show("Fill Quantity Cannot be more than Order Quanity");
                        LoadPickSheetDetailGrid(_selectedOrder.PickSheetID);
                        return;
                    }
                    //no negatives
                    if (fillQty < 0)
                    {
                        MessageBox.Show("Fill Quantity Cannot be Less than Zero");
                        LoadPickSheetDetailGrid(_selectedOrder.PickSheetID);
                        return;
                    }
                    //No change does nothing
                    if (fillQty == _selectedOrder.QtyReceived)
                    {
                        return;
                    }
                    

                    //if we made it this far we are ready to save the line item
                    //and reload the grid.

                    var _newOrder = new PickOrder();
                    //make a deep copy of the order to a new order
                    _newOrder = CopyOrder(_selectedOrder);
                    //Update the new fill qty
                    _newOrder.QtyReceived = fillQty;
                    try
                    {
                        int orderComplete = _pickManager.Update_PickOrder(_newOrder, _selectedOrder);
                        //MessageBox.Show("Order Updated " + orderComplete);
                        LoadPickSheetDetailGrid(_newOrder.PickSheetID);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void chkPPDetailMarkQtyFilled_Checked(object sender, RoutedEventArgs e)
        {
            var _selectedOrder = (PickOrder)dgProcessPickDetail.SelectedItem;

            if (_selectedOrder.OrderQty == _selectedOrder.QtyReceived)
            {
                //already done do nothing
                return;
            }
            var _newOrder = new PickOrder();
            //_newOrder = _selectedOrder;
            _newOrder = CopyOrder(_selectedOrder);
            //Make the fill qty the same as the order qty
            _newOrder.QtyReceived = _selectedOrder.OrderQty;
            _newOrder.PickCompleteDate = new DateTime(1001, 1, 1);
            try
            {
                int orderComplete = _pickManager.Update_PickOrder(_newOrder, _selectedOrder);
                // MessageBox.Show("Order Updated " + orderComplete);
                LoadPickSheetDetailGrid(_newOrder.PickSheetID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkPPDetailMarkQtyFilled_Unchecked(object sender, RoutedEventArgs e)
        {
           
            

        }

        private void LoadPickSheetDetailGrid(string picksheetID)
        {
            try
            {
                _picksheetDetails = _pickManager.Select_PickSheet_By_PickSheetID(picksheetID);

                dgProcessPickDetail.ItemsSource = _picksheetDetails;
                if (_picksheetDetails[0].OrderStatus != 2)
                {
                    dgProcessPickDetail.IsEnabled = false;
                }
                else
                {
                    dgProcessPickDetail.IsEnabled = true;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static PickOrder CopyOrder(PickOrder order)
        {
            PickOrder _newOrder = new PickOrder()
            {
                EmployeeID = order.EmployeeID,
                DeptID = order.DeptID,
                DeptDescription = order.DeptDescription,                
                DeliveryDate = order.DeliveryDate,
                DeliveryDateView = order.DeliveryDateView,
                InternalOrderID = order.InternalOrderID,
                ItemDescription = order.ItemDescription,
                ItemID = order.ItemID,
                ItemOrderID = order.ItemOrderID,
                OrderDateView = order.OrderDateView, 
                OrderDate = order.OrderDate,
                Orderer = order.Orderer,
                OrderQty = order.OrderQty,
                OrderReceivedDate = order.OrderReceivedDate,
                OrderReceivedDateView = order.OrderReceivedDateView,
                OrderStatus = order.OrderStatus,
                OrderStatusView = order.OrderStatusView,
                PickCompleteDateView = order.PickCompleteDateView, 
                PickCompleteDate = order.PickCompleteDate,
                PickSheetID = order.PickSheetID,
                PickSheetIDView = order.PickSheetIDView,
                QtyReceived = order.QtyReceived,
                UnitPrice = order.UnitPrice,
                OutOfStock = order.OutOfStock                
            };
            return _newOrder;
        }

        private void BtnRefreshDeliveryGrid_Click(object sender, RoutedEventArgs e)
        {
            //dtpDeliveryStartDate.SelectedDate = _startDate;
            LoadPickSheetDeliveryGrid(dtpDeliveryStartDate.SelectedDate.Value);
        }

        private void BtnDPHeaderMarkDelivered_Click(object sender, RoutedEventArgs e)
        {
            //Deliver a picksheet
            bool reOpen = false;
            var selectedPickSheet = (PickSheet)dgDeliverPickSheet.SelectedItem;
            if (selectedPickSheet.PickDeliveredBy > 0)
            {
                var mbr = MessageBox.Show("Pick Sheet " + selectedPickSheet.PickSheetID + " Is Already Delivered \n\rDo you want To Reopen it?", " ReOpen PickSheet ", MessageBoxButton.YesNo);
                if (mbr == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    reOpen = true;
                }
            }
            if (selectedPickSheet.PickDeliveredBy == 0)
            {
                var mbr = MessageBox.Show("Pick Sheet " + selectedPickSheet.PickSheetID + " Will Be Delivered \n\rIs this what you want to do?", " ReOpen PickSheet ", MessageBoxButton.YesNo);
                if (mbr == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    reOpen = false;
                }
            }

            var newPickSheet = new PickSheet();

            newPickSheet = CopyPickSheet(selectedPickSheet);

            if (reOpen)
            {
                
                newPickSheet.PickDeliveryDate = new DateTime(0001, 1, 1);
                newPickSheet.PickDeliveredBy = 0;
            }
            else
            {
                newPickSheet.PickDeliveryDate = DateTime.Now;
                newPickSheet.PickDeliveredBy = _employeeID;
            }

            try
            {
                int result = _pickManager.UpdatePickSheet(newPickSheet, selectedPickSheet);
                _picksheetDetails = null;
                dgProcessPickDetail.ItemsSource = _picksheetDetails;
                LoadCreatePickGrid(false);               
                LoadPickSheetDeliveryGrid((DateTime)dtpDeliveryStartDate.SelectedDate);
                LoadPickSheetHeaderGrid((DateTime)dtpProcessPickStartDate.SelectedDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDPHeaderPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TabDeliverPickSheet_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPPDetailMarkQtyFilled_Click(object sender, RoutedEventArgs e)
        {
            var _selectedOrder = (PickOrder)dgProcessPickDetail.SelectedItem;

            if (_selectedOrder.OrderQty == _selectedOrder.QtyReceived)
            {
                //already done do nothing
                return;
            }
            var _newOrder = new PickOrder();
            //_newOrder = _selectedOrder;
            _newOrder = CopyOrder(_selectedOrder);
            //Make the fill qty the same as the order qty
            _newOrder.QtyReceived = _selectedOrder.OrderQty;
            _newOrder.PickCompleteDate = new DateTime(1001, 1, 1);
            try
            {
                int orderComplete = _pickManager.Update_PickOrder(_newOrder, _selectedOrder);
                // MessageBox.Show("Order Updated " + orderComplete);
                LoadPickSheetDetailGrid(_newOrder.PickSheetID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
