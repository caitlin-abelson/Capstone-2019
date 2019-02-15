using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Author: Caitlin Abelson
    /// Created Date: 1/30/19
    /// 
    /// The Department class holds all the objects pertaining to the departments that employees can 
    /// work in at the resort.
    /// </summary>
    public class Department
    {
        public string DepartmentID{ get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return DepartmentID;
        }
    }
}
