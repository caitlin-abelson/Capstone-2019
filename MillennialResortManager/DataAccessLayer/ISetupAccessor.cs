using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Caitlin Abelson
    /// Created Date: 2/28/19
    /// 
    /// The concrete interface for the SetupAccessor
    /// </summary>
    public interface ISetupAccessor
    {
        List<Setup> SelectAllSetup();
        int InsertSetup(Setup newSetup);
        void UpdateSetup(Setup newSetup, Setup oldSetup);
        Setup SelectSetup(int setupID);
        void DeleteSetup(int setupID);
        List<VMSetup> SelectVMSetups();
        List<VMSetup> SelectDateEntered(DateTime dateEntered);
        List<VMSetup> SelectDateRequired(DateTime dateRequired);
        List<VMSetup> SelectSetupEventTitle(string eventTitle);



    }
}
