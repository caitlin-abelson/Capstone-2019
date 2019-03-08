using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{

    /// <summary>
    /// Eduardo Colon
    /// Created: 2019/03/05
    /// 
    /// the setuplist dataObjects
    /// </summary>
    public class SetupList
    {


        public int SetupListID{ get; set; }
        public int SetupID { get; set; }
        public bool Completed { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }

    }
}
