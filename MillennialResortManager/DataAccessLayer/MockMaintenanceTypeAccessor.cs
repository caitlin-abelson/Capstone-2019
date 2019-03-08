/// <summary>
/// Austin Berquam
/// Created: 2019/02/23
/// 
/// This is a mock Data Accessor which implements ItypesAccessor.  This is for testing purposes only.
/// </summary>
/// 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class MockMaintenanceTypeAccessor : IMaintenanceTypeAccessor
    {
        private List<MaintenanceTypes> types;

        /// <summary>
        /// Author: Austin Berquam
        /// Created: 2019/02/23
        /// This constructor that sets up dummy data
        /// </summary>
        public MockMaintenanceTypeAccessor()
        {
            types = new List<MaintenanceTypes>
            {
                new MaintenanceTypes {MaintenanceTypeID = "types1", Description = "types is a types"},
                new MaintenanceTypes {MaintenanceTypeID = "types2", Description = "types is a types"},
                new MaintenanceTypes {MaintenanceTypeID = "types3", Description = "types is a types"},
                new MaintenanceTypes {MaintenanceTypeID = "types4", Description = "types is a types"}
            };
        }

        public int InsertMaintenanceType(MaintenanceTypes empRoles)
        {
            int listLength = types.Count;
            types.Add(empRoles);
            if (listLength == types.Count - 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public List<MaintenanceTypes> SelectAllMaintenanceTypes(string status)
        {
            return types;
        }

        public List<string> SelectAllMaintenanceTypeID()
        {
            throw new NotImplementedException();
        }

        public int DeleteMaintenanceType(string typesID)
        {
            int rowsDeleted = 0;
            foreach (var type in types)
            {
                if (type.MaintenanceTypeID == typesID)
                {
                    int listLength = types.Count;
                    types.Remove(type);
                    if (listLength == types.Count - 1)
                    {
                        rowsDeleted = 1;
                    }
                }
            }

            return rowsDeleted;
        }
    }
}
