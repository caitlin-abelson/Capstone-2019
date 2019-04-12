using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// Jared Greenfield
    /// Created: 2018/01/22
    /// Interface for Offerings
    /// </summary>
    public interface IOfferingAccessor
    {
        int InsertOffering(Offering offering);
        Offering SelectOfferingByID(int offeringID);
        int UpdateOffering(Offering oldOffering, Offering newOffering);
        List<OfferingVM> SelectAllOfferingViewModels();
        List<string> SelectAllOfferingTypes();

        int DeleteOfferingByID(int offeringID);

        int DeactivateOfferingByID(int offeringID);

        int ReactivateOfferingByID(int offeringID);

        Object SelectOfferingInternalRecordByIDAndType(int id, string offeringType);
    }
}