using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer
{

    /// <summary>
    /// Eduardo Colon
    /// Created: 2019/03/05
    /// 
    /// the interface ISetupListAccessor
    /// </summary>
    public interface ISetupListAccessor
    {

        List<SetupList> SelectAllSetupLists();
        List<VMSetupList> SelectActiveSetupLists();
        List<VMSetupList> SelectInactiveSetupLists();
        void InsertSetupList(SetupList newSetupList);
        int UpdateSetupList(SetupList newSetupList, SetupList oldSetupList);
        SetupList SelectSetupList(int setupListID);
        List<VMSetupList> SelectVMSetupLists();
        void DeleteSetupList(int setupListID);
        void DeactiveSetupList(int setupListID);
    }
}