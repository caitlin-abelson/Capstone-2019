/// <summary>
/// Danielle Russo
/// Created: 2019/02/28
/// 
/// Interface that implements CRUD functions for Inspection objs.
/// for accessor classes.
/// </summary>
///
/// <remarks>
/// </remarks>
/// 
using DataObjects;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IInspectionAccessor
    {
        int InsertInspection(Inspection newInspection);

        List<Inspection> SelectAllInspectionsByResortPropertyID(int resortProperyId);
    }
}