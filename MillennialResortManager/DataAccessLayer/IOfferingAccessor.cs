using DataObjects;

namespace DataAccessLayer
{
    public interface IOfferingAccessor
    {
        int InsertOffering(Offering offering);
        Offering SelectOfferingByID(int offeringID);
        int UpdateOffering(Offering oldOffering, Offering newOffering);
    }
}