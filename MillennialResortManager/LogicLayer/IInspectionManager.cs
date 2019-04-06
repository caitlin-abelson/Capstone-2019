/// <summary>
/// Danielle Russo
/// Created: 2019/02/28
/// 
/// Interface that implements CRUD functions for Inspection objs.
/// for manager classes.
/// </summary>
///
/// <remarks>
/// </remarks>
/// 
using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IInspectionManager
    {
        bool CreateInspection(Inspection newInspection);
        bool UpdateInspection(Inspection selectedInspection, Inspection newInspection);
        List<Inspection> RetrieveAllInspectionsByResortPropertyId(int resortPropertyId);
    }
}