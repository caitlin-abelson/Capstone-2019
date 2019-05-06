using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Jared Greenfield" created="2019/01/22">
	/// Handles logic operations for Offering objects.
	/// </summary>
	public class OfferingManager : IOfferingManager
    {
        private IOfferingAccessor _offeringAccessor;

		/// <summary author="Jared Greenfield" created="2019/02/20">
		/// OfferingManager is an implementation of IOfferingManager meant to deal with OfferingAccessor.
		/// </summary>
		public OfferingManager()
        {
            _offeringAccessor = new OfferingAccessor();
        }

		/// <summary author="Jared Greenfield" created="2019/02/20">
		/// OfferingManager is an implementation of IOfferingManager meant to deal with mock OfferingAccessor.
		/// </summary>
		public OfferingManager(OfferingAccessorMock mock)
        {
            _offeringAccessor = mock;
        }

		/// <summary author="Jared Greenfield" created="2019/01/26">
		/// Adds an Offering to the database.
		/// </summary>
		/// <updates>
		/// <update author="Jared Greenfield" date="2019/02/09">
		/// Added IsValid() check
		/// </update>
		/// </updates>
		/// <param name="offering">The Offering to be added.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>Id of Offering</returns>
		public int CreateOffering(Offering offering)
        {
            int returnedID = 0;
            if (offering.IsValid())
            {
                try
                {
                    returnedID = _offeringAccessor.InsertOffering(offering);
                }
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}
            else
            {
                throw new ArgumentException("Data for this Offering is not valid.");
            }
            return returnedID;
        }

		/// <summary author="Jared Greenfield" created="2019/01/27">
		/// Retrieves all Offering View Models
		/// </summary>
		/// <exception cref="SQLException">Select Fails</exception>
		/// <returns>List of Offering VMs</returns>
		public List<OfferingVM> RetrieveAllOfferingViewModels()
        {
            List<OfferingVM> offerings = new List<OfferingVM>();
            try
            {
                offerings = _offeringAccessor.SelectAllOfferingViewModels();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return offerings;
        }

		/// <summary author="Jared Greenfield" created="2019/01/28">
		/// Retrieves all Offering Types
		/// </summary>
		/// <exception cref="SQLException">Select Fails</exception>
		/// <returns>List of Offering types</returns>
		public List<string> RetrieveAllOfferingTypes()
        {
            List<string> offeringTypes = new List<string>();
            try
            {
                offeringTypes = _offeringAccessor.SelectAllOfferingTypes();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return offeringTypes;
        }

		/// <summary author="Jared Greenfield" created="2019/01/24">
		/// Retrieves an Offering based on an ID
		/// </summary>
		/// <param name="offeringID">The ID of the Offering.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>Offering Object</returns>
		public Offering RetrieveOfferingByID(int offeringID)
        {
            Offering offering = null;

            try
            {
                offering = _offeringAccessor.SelectOfferingByID(offeringID);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return offering;
        }

		/// <summary author="Jared Greenfield" created="2019/02/09">
		/// Updates an Offering with a new Offering.
		/// </summary>
		/// <updates>
		/// /// <update author="Jared Greenfield" date="2019/02/09">
		/// Added IsValid() check
		/// </update>
		/// </updates>
		/// <param name="oldOffering">The old Offering.</param>
		/// <param name="newOffering">The updated Offering.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>True if the update was successful, false if not.</returns>
		public bool UpdateOffering(Offering oldOffering, Offering newOffering)
        {
            bool result = false;

            if (newOffering.IsValid())
            {
                if (1 == _offeringAccessor.UpdateOffering(oldOffering, newOffering))
                {
                    result = true;
                }
            }
            else
            {
                throw new ArgumentException("Data for this New Offering is not valid.");
            }
            return result;
        }

		/// <summary author="Jared Greenfield" created="2019/01/24">
		/// Deletes an Offering based on an ID
		/// </summary>
		/// <param name="offeringID">The ID of the Offering.</param>
		/// <exception cref="SQLException">Delete Fails (example of exception tag)</exception>
		/// <returns>True if successful, false if not</returns>
		public bool DeleteOfferingByID(int offeringID)
        {
            bool result = false;

            try
            {
                result = (1 == _offeringAccessor.DeleteOfferingByID(offeringID));
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
        }

		/// <summary author="Jared Greenfield" created="2019/01/24">
		/// Deactivates an Offering based on an ID
		/// </summary>
		/// <param name="offeringID">The ID of the Offering.</param>
		/// <exception cref="SQLException">Update Fails (example of exception tag)</exception>
		/// <returns>True if successful, false if not</returns>
		public bool DeactivateOfferingByID(int offeringID)
        {
            bool result = false;

            try
            {
                result = (1 == _offeringAccessor.DeactivateOfferingByID(offeringID));
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
        }

		/// <summary author="Jared Greenfield" created="2019/01/24">
		/// Reactivates an Offering based on an ID
		/// </summary>
		/// <param name="offeringID">The ID of the Offering.</param>
		/// <exception cref="SQLException">Update Fails (example of exception tag)</exception>
		/// <returns>True if successful, false if not</returns>
		public bool ReactivateOfferingByID(int offeringID)
        {
            bool result = false;

            try
            {
                result = (1 == _offeringAccessor.ReactivateOfferingByID(offeringID));
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
        }

		/// <summary author="Jared Greenfield" created="2019/04/04">
		/// Retrieves a variety of Objects based on the OfferingType and ID
		/// </summary>
		/// <exception cref="SQLException">Select Fails</exception>
		/// <param name="offeringID">ID of offering</param>
		/// <param name="offeringType">Type of Offering</param>
		/// <returns>Object object</returns>
		public Object RetrieveOfferingInternalRecordByIDAndType(int offeringID, string offeringType)
        {
            Object objectValue = null;
            try
            {
                objectValue = _offeringAccessor.SelectOfferingInternalRecordByIDAndType(offeringID, offeringType);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return objectValue;
        }
    }
}