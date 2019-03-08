/// <summary>
/// Jared Greenfield
/// Created: 2019/01/22
/// 
/// The interface for Offering object logic operations.
/// </summary>
///
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IOfferingManager
    {
        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/26
        ///
        /// Adds an Offering to the database.
        /// </summary>
        /// <param name="offering">The Offering to be added.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>1 if successful, 0 if unsuccessful</returns>
        int CreateOffering(Offering offering);

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/24
        ///
        /// Retrieves an Offering based on an ID
        /// </summary>
        /// <param name="offeringID">The ID of the Offering.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Offering Object</returns>
        Offering RetrieveOfferingByID(int offeringID);

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/02/09
        ///
        /// Updates an Offering with a new Offering.
        /// </summary>
        /// 
        /// <param name="oldOffering">The old Offering.</param>
        /// <param name="newOffering">The updated Offering.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>ID of Offering.</returns>
        bool UpdateOffering(Offering oldOffering, Offering newOffering);
    }
}
