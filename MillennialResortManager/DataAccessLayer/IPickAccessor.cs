using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface IPickAccessor
    {
        /// <summary>
        /// Author: Eric Bostwick
        /// Created : 3/26/2019
        /// The Interface for the PickSheet Accessor
        /// for managing Picking Operations
        /// </summary>

        List<PickSheet> Select_All_PickSheets();
        int UpdatePickSheet(PickSheet picksheet, PickSheet oldPickSheet);

        List<PickSheet> Select_All_PickSheets_By_Date(DateTime startDate);

        int Insert_TmpPickSheet_To_PickSheet(string picksheetID);

        int Delete_TmpPickSheet_Item(PickOrder pickOrder);

        int Delete_TmpPickSheet(string picksheetID);

        int Insert_Record_To_TmpPicksheet(PickOrder pickOrder);

        string Select_Pick_Sheet_Number();

        List<PickOrder> Select_Orders_For_Acknowledgement(DateTime startDate, bool hidePicked);

        List<PickOrder> Select_PickSheet_By_PickSheetID(string pickSheetID);

        int Update_PickOrder(PickOrder pickOrder, PickOrder oldPickOrder);

        List<PickSheet> Select_All_Closed_PickSheets_By_Date(DateTime startDate);

        List<PickOrder> Select_All_Temp_PickOrders();

        PickSheet Select_TmpPickSheet(string pickSheetID);

    }
}
