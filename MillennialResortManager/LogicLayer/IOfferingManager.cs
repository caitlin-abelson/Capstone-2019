using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="Jared Greenfield" created="2019/01/22">
	/// The interface for Offering object logic operations.
	/// </summary>
	public interface IOfferingManager
	{
		/// <summary author="Jared Greenfield" created="2019/01/26">
		/// Adds an Offering to the database.
		/// </summary>
		/// <param name="offering">The Offering to be added.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>1 if successful, 0 if unsuccessful</returns>
		int CreateOffering(Offering offering);

		/// <summary author="Jared Greenfield" created="2019/01/24">
		/// Retrieves an Offering based on an ID
		/// </summary>
		/// <param name="offeringID">The ID of the Offering.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>Offering Object</returns>
		Offering RetrieveOfferingByID(int offeringID);

		/// <summary author="Jared Greenfield" created="2019/01/24">
		/// Retrieves all Offering View Models
		/// </summary>
		/// <exception cref="SQLException">Select Fails</exception>
		/// <returns>List of Offering VMs</returns>
		List<OfferingVM> RetrieveAllOfferingViewModels();

		/// <summary author="Jared Greenfield" created="2019/01/28">
		/// Retrieves all Offering Types
		/// </summary>
		/// <exception cref="SQLException">Select Fails</exception>
		/// <returns>List of Offering types</returns>
		List<string> RetrieveAllOfferingTypes();

		/// <summary author="Jared Greenfield" created="2019/02/09">
		/// Updates an Offering with a new Offering.
		/// </summary>
		/// <param name="oldOffering">The old Offering.</param>
		/// <param name="newOffering">The updated Offering.</param>
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>ID of Offering.</returns>
		bool UpdateOffering(Offering oldOffering, Offering newOffering);

		/// <summary author="Jared Greenfield" created="2019/01/24">
		/// Deletes an Offering based on an ID
		/// </summary>
		/// <param name="offeringID">The ID of the Offering.</param>
		/// <exception cref="SQLException">Delete Fails (example of exception tag)</exception>
		/// <returns>True if successful, false if not</returns>
		bool DeleteOfferingByID(int offeringID);

		/// <summary author="Jared Greenfield" created="2019/01/24">
		/// Deactivates an Offering based on an ID
		/// </summary>
		/// <param name="offeringID">The ID of the Offering.</param>
		/// <exception cref="SQLException">Update Fails (example of exception tag)</exception>
		/// <returns>True if successful, false if not</returns>
		bool DeactivateOfferingByID(int offeringID);

		/// <summary author="Jared Greenfield" created="2019/01/24">
		/// Reactivates an Offering based on an ID
		/// </summary>
		/// <param name="offeringID">The ID of the Offering.</param>
		/// <exception cref="SQLException">Update Fails (example of exception tag)</exception>
		/// <returns>True if successful, false if not</returns>
		bool ReactivateOfferingByID(int offeringID);

		/// <summary author="Jared Greenfield" created="2019/04/04">
		/// Retrieves a variety of Objects based on the OfferingType and ID
		/// NOTE: You must cast to the Data Object that the type refers to.
		/// </summary>
		/// <exception cref="SQLException">Select Fails</exception>
		/// <param name="offeringID">ID of offering</param>
		/// <param name="offeringType">Type of Offering</param>
		/// <returns>Object object</returns>
		Object RetrieveOfferingInternalRecordByIDAndType(int offeringID, string offeringType);
    }
}
