using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DataObjects;


/// <summary>
/// Eric Bostwick
/// Created: 4/9/2019
/// MockAccessor for testing PickAccessor Methods
/// </summary>
///
/// <remarks>

namespace DataAccessLayer
{
    public class PickAccessorMock : IPickAccessor
    {
        private List<PickOrder> _pickOrders;
        private List<PickSheet> _pickSheets;
        private List<PickOrder> _tmpPickOrders;
        private PickSheet _tmpPickSheet;
        public PickAccessorMock()
        {
            _pickOrders = new List<PickOrder>();
            _pickSheets = new List<PickSheet>();
            _tmpPickOrders = new List<PickOrder>();
            _tmpPickSheet = new PickSheet();

            //Set up some orders to pick
            _pickOrders.Add(new PickOrder()
            {
                ItemID = 100000,
                ItemOrderID = 100000,
                OrderQty = 500,
                QtyReceived = 0,
                PickSheetID = "1000011111",
                OrderReceivedDate = new DateTime(0001, 1, 1),
                PickCompleteDate = new DateTime(0001, 1, 1),
                DeliveryDate = new DateTime(0001, 1, 1),
                OrderStatus = 1
            });
            _pickOrders.Add(new PickOrder()
            {
                ItemID = 100001,
                ItemOrderID = 100000,
                OrderQty = 500,
                QtyReceived = 0,
                PickSheetID = "10000011111",
                OrderReceivedDate = new DateTime(0001, 1, 1),
                PickCompleteDate = new DateTime(0001, 1, 1),
                DeliveryDate = new DateTime(0001, 1, 1),
                OrderStatus = 1
            });
            _pickOrders.Add(new PickOrder()
            {
                ItemID = 100002,
                ItemOrderID = 100000,
                OrderQty = 500,
                QtyReceived = 0,
                PickSheetID = "10000011111",
                OrderReceivedDate = new DateTime(0001, 1, 1),
                PickCompleteDate = new DateTime(0001, 1, 1),
                DeliveryDate = new DateTime(0001, 1, 1),
                OrderStatus = 1
            });

            _pickOrders.Add(new PickOrder()
            {
                ItemID = 100003,
                ItemOrderID = 100001,
                OrderQty = 500,
                QtyReceived = 0,
                PickSheetID = "10000022222",
                OrderReceivedDate = new DateTime(0001, 1, 1),
                PickCompleteDate = new DateTime(0001, 1, 1),
                DeliveryDate = new DateTime(0001, 1, 1),
                OrderStatus = 1
            });
            _pickOrders.Add(new PickOrder()
            {
                ItemID = 100004,
                ItemOrderID = 100001,
                OrderQty = 500,
                QtyReceived = 0,
                PickSheetID = "10000022222",
                OrderReceivedDate = new DateTime(0001, 1, 1),
                PickCompleteDate = new DateTime(0001, 1, 1),
                DeliveryDate = new DateTime(0001, 1, 1),
                OrderStatus = 1
            });
            _pickOrders.Add(new PickOrder()
            {
                ItemID = 100005,
                ItemOrderID = 100001,
                OrderQty = 500,
                QtyReceived = 0,
                PickSheetID = "10000011111",
                OrderReceivedDate = new DateTime(0001, 1, 1),
                PickCompleteDate = new DateTime(0001, 1, 1),
                DeliveryDate = new DateTime(0001, 1, 1),
                OrderStatus = 1
            });
            _pickOrders.Add(new PickOrder()
            {
                ItemID = 100006,
                ItemOrderID = 100002,
                OrderQty = 500,
                QtyReceived = 0,
                PickSheetID = "10000011111",
                OrderReceivedDate = new DateTime(0001, 1, 1),
                PickCompleteDate = new DateTime(0001, 1, 1),
                DeliveryDate = new DateTime(0001, 1, 1),
                OrderStatus = 1
            });
            _pickOrders.Add(new PickOrder()
            {
                ItemID = 100007,
                ItemOrderID = 100002,
                OrderQty = 500,
                QtyReceived = 0,
                PickSheetID = "10000011111",
                OrderReceivedDate = new DateTime(2019, 4, 1),
                PickCompleteDate = new DateTime(0001, 1, 1),
                DeliveryDate = new DateTime(0001, 1, 1),
                OrderStatus = 2
            });
            _pickOrders.Add(new PickOrder()
            {
                ItemID = 100008,
                ItemOrderID = 100002,
                OrderQty = 500,
                QtyReceived = 0,
                PickSheetID = "10000011111",
                OrderReceivedDate = new DateTime(2019, 4, 1),
                PickCompleteDate = new DateTime(0001, 1, 1),
                DeliveryDate = new DateTime(0001, 1, 1),
                OrderStatus = 2
            });

            _pickSheets.Add(new PickSheet()
            {
                CreateDate = DateTime.Now,
                PickSheetID = "10000022222",
                PickSheetCreatedBy = 0,
                PickSheetCreatedByName = "Name",
                PickCompletedBy = 0,
                PickCompletedByName = "",
                PickCompletedDate = new DateTime(0001, 1, 1),
                PickSheetStatus = 0,
                PickDeliveredBy = 0,
                PickDeliveredByName = "",
                PickDeliveryDate = new DateTime(0001, 1, 1),
                PickSheetStatusView = "",
                NumberOfOrders = 0,
                TempPickSheetID = ""
            });

            _pickSheets.Add(new PickSheet()
            {
                CreateDate = DateTime.Now.AddDays(-10),
                PickSheetID = "10000033333",
                PickSheetCreatedBy = 0,
                PickSheetCreatedByName = "Name",
                PickCompletedBy = 0,
                PickCompletedByName = "",
                PickCompletedDate = new DateTime(0001, 1, 1),
                PickSheetStatus = 0,
                PickDeliveredBy = 0,
                PickDeliveredByName = "",
                PickDeliveryDate = new DateTime(0001, 1, 1),
                PickSheetStatusView = "",
                NumberOfOrders = 0,
                TempPickSheetID = ""
            });

            _tmpPickSheet = new PickSheet
            {
                PickSheetID = "100000" + "12345",
                PickCompletedBy = 100001
            };

            _tmpPickOrders.Add(new PickOrder()
            {
                PickSheetID = "100000" + "12345",
                InternalOrderID = 100000,
                ItemID = 100000,
                EmployeeID = 100001,
                PickCompleteDate = DateTime.Now.AddDays(-1)
            });
            _tmpPickOrders.Add(new PickOrder()
            {
                PickSheetID = "100000" + "12345",
                InternalOrderID = 10000,
                ItemID = 100001,
                EmployeeID = 100001,
                PickCompleteDate = DateTime.Now.AddDays(-1)
            });
            _tmpPickOrders.Add(new PickOrder()
            {
                PickSheetID = "100000" + "12345",
                InternalOrderID = 10000,
                ItemID = 100002,
                EmployeeID = 100001,
                PickCompleteDate = DateTime.Now.AddDays(-1)
            });
        } //end constructor

        public int Delete_TmpPickSheet(string picksheetID)
        {
            int result = 0;
            _tmpPickSheet = null;
            if(null == _tmpPickSheet)
            {
                result = 1;
            }
            return result;
        }

        public int Delete_TmpPickSheet_Item(PickOrder pickOrder)
        {
            int result = 0;
            int count = _tmpPickOrders.Count;
            _tmpPickOrders.Remove(pickOrder);
            if (_tmpPickOrders.Count == count - 1)
            {
                result = 1;
            }
            return result;
        }

        public int Insert_Record_To_TmpPicksheet(PickOrder pickOrder)
        {
            int result = 0;
            int count = _tmpPickOrders.Count;
            _tmpPickOrders.Add(pickOrder);
           
            if(_tmpPickOrders.Count == count + 1)
            {
                result = 1;
            }
            return result;
        }

        public int Insert_TmpPickSheet_To_PickSheet(string picksheetID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {                    
                    foreach(PickOrder pickOrder in _tmpPickOrders)
                    {
                        if(pickOrder.PickSheetID == picksheetID)
                        {
                            pickOrder.OrderReceivedDate = DateTime.Now;
                            pickOrder.OrderStatus = 2;
                        }
                    }
                    int orderNumber = _tmpPickOrders.FindAll(o => o.PickSheetID == picksheetID).Count;
                    int pickedBy = _tmpPickSheet.PickCompletedBy;

                    PickSheet pickSheet = new PickSheet();

                    pickSheet.PickSheetID = picksheetID;
                    pickSheet.PickSheetCreatedBy = pickedBy;
                    pickSheet.NumberOfOrders = orderNumber;
                    _pickSheets.Add(pickSheet);
                                     
                    scope.Complete();
                    return 1;
                }
            }

            catch (TransactionAbortedException ex)
            {
                return 0;
                throw ex;
            }

        }

        public List<PickSheet> Select_All_Closed_PickSheets_By_Date(DateTime startDate)
        {
            return _pickSheets.FindAll(d => (d.CreateDate >= startDate) && (d.PickCompletedBy != 0));
        }

        public List<PickSheet> Select_All_PickSheets()
        {
            return _pickSheets;
        }

        public List<PickSheet> Select_All_PickSheets_By_Date(DateTime startDate)
        {
            return _pickSheets.FindAll(d => d.CreateDate >= startDate);
        }

        public List<PickOrder> Select_All_Temp_PickOrders()
        {
            return _tmpPickOrders;
        }

        public List<PickOrder> Select_All_Temp_PickSheets()
        {
            return _tmpPickOrders;
        }

        public List<PickOrder> Select_Orders_For_Acknowledgement(DateTime startDate, bool hidePicked)
        {
            List<PickOrder> pickOrders;
            int orderStatus = 1;
            if (hidePicked)
            {
                orderStatus = 2;
            }
            pickOrders = _pickOrders.FindAll(d => (d.OrderReceivedDate <= startDate) && (d.OrderStatus == orderStatus));
            return pickOrders;
        }

        public List<PickOrder> Select_PickSheet_By_PickSheetID(string pickSheetID)
        {
            return _pickOrders.FindAll(d => (d.PickSheetID == pickSheetID));
        }

        public string Select_Pick_Sheet_Number()
        {
            var suffix = Environment.MachineName;
            suffix = suffix.Substring(suffix.Length - 4, 4);
            int maxPrefix = 0;
            foreach (PickSheet pickSheet in _pickSheets)
            {
                string s = pickSheet.PickSheetID.Substring(0, 6);
                int prefix;                
                int.TryParse(s, out prefix);
                if(prefix > maxPrefix)
                {
                    maxPrefix = prefix;
                }                  
            }            
            return  maxPrefix.ToString() + suffix;
        }

        public int UpdatePickSheet(PickSheet picksheet, PickSheet oldPickSheet)
        {
            var index = _pickSheets.FindIndex(x => x.PickSheetID == picksheet.PickSheetID);

            _pickSheets[index] = picksheet;

           if (index > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }               
        }

        public int Update_PickOrder(PickOrder pickOrder, PickOrder oldPickOrder)
        {
            var index = _pickOrders.FindIndex(x => x.PickSheetID == pickOrder.PickSheetID);

            _pickOrders[index] = pickOrder;

            if (index > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public  PickSheet Select_TmpPickSheet(string pickSheetID)
        {
            return _tmpPickSheet;
        }
    }
}
