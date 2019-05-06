using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="Austin Berquam" created="2019/02/06">
	/// Used to manage the Guest Type table
	/// and the stored procedures as well
	/// </summary>
	public class GuestTypeManager : IGuestTypeManager
    {

        IGuestTypeAccessor guestTypeAccessor;

        public GuestTypeManager()
        {
            guestTypeAccessor = new GuestTypeAccessor();
        }
        public GuestTypeManager(MockGuestTypeAccessor mock)
        {
            guestTypeAccessor = new MockGuestTypeAccessor();
        }

		/// <summary author="Austin Berquam" created="2019/02/06">
		/// Method that collects the GuestType from the accessor
		/// </summary>
		/// <returns> List of GuestTypes </returns>
		public List<GuestType> RetrieveAllGuestTypes(string status)
        {
            List<GuestType> types = null;

            if (status != "")
            {
                try
                {
                    types = guestTypeAccessor.SelectGuestTypes(status);
                }
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}

            return types;
        }

		/// <summary author="Austin Berquam" created="2019/02/06">
		/// Method that sends the created guestType to the accessor
		/// </summary>
		/// <param name="guestType">Object holding the new guestType to add to the table</param>
		/// <returns> bool on if the role was created </returns>
		public bool CreateGuestType(GuestType guestType)
        {

            ValidationExtensionMethods.ValidateID(guestType.GuestTypeID);
            ValidationExtensionMethods.ValidateDescription(guestType.Description);
            bool result = false;

            try
            {
                result = (1 == guestTypeAccessor.InsertGuestType(guestType));
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
        }

		/// <summary author="Austin Berquam" created="2019/02/06">
		/// Method that deletes a guestType through the accessor
		/// </summary>
		/// <param name="guestTypeID">string of guestTypeId to delete</param>
		/// <returns> bool on if the guest was deleted </returns>
		public bool DeleteGuestType(string guestTypeID)
        {
            bool result = false;

            try
            {
                result = (1 == guestTypeAccessor.DeleteGuestType(guestTypeID));
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
        }

		/// <summary author="Austin Berquam" created="2019/02/06">
		/// Method that retrieves all guest types and stores it in a list
		/// </summary>
		/// <returns> GuestTypes in a List returns>
		public List<string> RetrieveAllGuestTypes()
        {
            List<string> types = null;

            try
            {
                types = guestTypeAccessor.SelectAllTypes();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return types;
        }
    }
}