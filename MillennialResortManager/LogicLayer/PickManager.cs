using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Eric Bostwick
    /// Created 4/2/2019
    /// Logic Layer Class for interacting with Data Access Layer
    /// for managing Picking Operations
    /// </summary>
    public class PickManager : IPickManager
    {
        private IPickAccessor _pickAccessor;

        public PickManager()
        {
            _pickAccessor = new PickAccessor();
        }
        public PickManager(PickAccessorMock pickAccessorMock)
        {
            _pickAccessor = pickAccessorMock;
        }

        public int Delete_TmpPickSheet(string picksheetID)
        {
            int result;
            try
            {
                result = _pickAccessor.Delete_TmpPickSheet(picksheetID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int Delete_TmpPickSheet_Item(PickOrder pickOrder)
        {
            int result;
            try
            {
                result = _pickAccessor.Delete_TmpPickSheet_Item(pickOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int Insert_Record_To_TmpPicksheet(PickOrder pickOrder)
        {
            int result;
            try
            {
                if (!pickOrder.IsValid())
                {
                    throw new ArgumentException("Data for this order record is invalid");
                }

                result = _pickAccessor.Insert_Record_To_TmpPicksheet(pickOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int Insert_TmpPickSheet_To_PickSheet(string picksheetID)
        {
            int result;
            try
            {
                result = _pickAccessor.Insert_TmpPickSheet_To_PickSheet(picksheetID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<PickSheet> Select_All_Closed_PickSheets_By_Date(DateTime startDate)
        {
            List<PickSheet> pickSheets;
            try
            {
                pickSheets = _pickAccessor.Select_All_Closed_PickSheets_By_Date(startDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pickSheets;
        }

        public List<PickSheet> Select_All_PickSheets()
        {
            List<PickSheet> pickSheets;
            try
            {
                pickSheets = _pickAccessor.Select_All_PickSheets();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pickSheets;
        }

        public List<PickSheet> Select_All_PickSheets_By_Date(DateTime startDate)
        {
            List<PickSheet> pickSheets;
            try
            {
                pickSheets = _pickAccessor.Select_All_PickSheets_By_Date(startDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pickSheets;
        }

        public List<PickOrder> Select_All_Tmp_PickOrders()
        {
            return _pickAccessor.Select_All_Temp_PickOrders();
        }

        public List<PickOrder> Select_Orders_For_Acknowledgement(DateTime startDate, bool hidePicked)
        {
            List<PickOrder> orders;
            try
            {
                orders = _pickAccessor.Select_Orders_For_Acknowledgement(startDate, hidePicked);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return orders;
        }

        public List<PickOrder> Select_PickSheet_By_PickSheetID(string pickSheetID)
        {
            List<PickOrder> pickOrders = new List<PickOrder>();

            try
            {
                pickOrders = _pickAccessor.Select_PickSheet_By_PickSheetID(pickSheetID);
            }
            catch (Exception)
            {
                throw;
            }

            return pickOrders;
        }
        

        public string Select_Pick_Sheet_Number()
        {
            string picksheetID;

            try
            {
                picksheetID = _pickAccessor.Select_Pick_Sheet_Number();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return picksheetID;
        }

        public PickSheet Select_TmpPickSheet(string pickSheetID)
        {
            return _pickAccessor.Select_TmpPickSheet(pickSheetID);
        }

        public int UpdatePickSheet(PickSheet pickSheet, PickSheet oldPickSheet)
        {            
            int result;
            try
            {
                if (!pickSheet.IsValid() || (!oldPickSheet.IsValid()))
                {
                    throw new ArgumentException("Data for this itemsupplier record is invalid");
                }
                result = _pickAccessor.UpdatePickSheet(pickSheet, oldPickSheet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int Update_PickOrder(PickOrder pickOrder, PickOrder oldPickOrder)
        {
            int result;
            try
            {
                if (!pickOrder.IsValid())
                {
                    throw new ArgumentException("Data for this order record is invalid");
                }
                result = _pickAccessor.Update_PickOrder(pickOrder, oldPickOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
