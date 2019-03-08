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

        List<SetupList> RetrieveAllSetupLists();
    
        SetupList RetrieveSetupListByRoleId(int setupListID);
    }
}