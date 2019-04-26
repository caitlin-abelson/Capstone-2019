using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Austin Berquam
    /// Created: 2019/02/06
    /// 
    /// Department class is used to store the Department table
    /// </summary>
    public class Department : IMessagable
    {
        public string DepartmentID { get; set; }
        public string Description { get; set; }

		/// <summary>
		/// All available aliases that can be used by this department
		/// </summary>
		public List<string> Aliases
		{
			get
			{
				return new List<string> { DepartmentID };
			}
		}

		/// <summary>
		/// The alias to be used when this Department is used as a recipient in the
		/// messaging system.
		/// </summary>
		public string Alias
		{
			get
			{
				return DepartmentID;
			}
		}
	}
}
