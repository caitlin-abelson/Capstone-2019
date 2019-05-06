using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Caitlin Abelson" created="2019/02/28">
	/// The concrete interface for SetupListManager implementing all CRUD functions
	/// </summary>
	public interface ISetupListManager
    {
        void InsertSetupList(SetupList newSetupList);
        void UpdateSetupList(SetupList newSetupList, SetupList oldSetupList);
        SetupList SelectSetupList(int setupListID);
        List<SetupList> SelectAllSetupLists();
        List<VMSetupList> SelectAllActiveSetupLists();
        List<VMSetupList> SelectAllInActiveSetupLists();
        void DeleteSetupList(int setupListID, bool isActive);
        List<VMSetupList> SelectAllVMSetupLists();
    }
}