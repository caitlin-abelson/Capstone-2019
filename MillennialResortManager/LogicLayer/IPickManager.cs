using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Eric Bostwick" created="2019/04/02">
	/// Interface For PickManager
	/// </summary>
	public interface IPickManager
    {
        List<PickSheet> Select_All_PickSheets();        
        List<PickSheet> Select_All_PickSheets_By_Date(DateTime startDate);
        int Insert_TmpPickSheet_To_PickSheet(string picksheetID);
        int Delete_TmpPickSheet_Item(PickOrder pickOrder);
        int Delete_TmpPickSheet(string picksheetID);
        int Insert_Record_To_TmpPicksheet(PickOrder pickOrder);
        string Select_Pick_Sheet_Number();
        List<PickOrder> Select_Orders_For_Acknowledgement(DateTime startDate, bool hidePicked);
        List<PickOrder> Select_PickSheet_By_PickSheetID(string pickSheetID);
        int UpdatePickSheet(PickSheet picksheet, PickSheet oldPickSheet);
        int Update_PickOrder(PickOrder pickOrder, PickOrder oldPickOrder);        
        List<PickSheet> Select_All_Closed_PickSheets_By_Date(DateTime startDate);
        List<PickOrder> Select_All_Tmp_PickOrders();
        PickSheet Select_TmpPickSheet(string pickSheetID);
    }
}