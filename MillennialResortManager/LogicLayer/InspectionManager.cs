/// <summary>
/// Danielle Russo
/// Created: 2019/01/21
/// 
/// Class that interacts with the presentation layer and building access layer
/// </summary>
///
/// <remarks>
/// </remarks>
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{

    public class InspectionManager : IInspectionManager
    {
        private IInspectionAccessor inspectionAccessor;

        public InspectionManager()
        {
            inspectionAccessor = new InspectionAccessor();
        }
        //public InspectionManager(MockInspectionAccessor accessor)
        //{
        //    inspectionAccessor = new MockInspectionAccessor();
        //}

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/27
        /// 
        /// Adds a new Inspection obj.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="newInspection">The Inspection obj to be added</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>True if Inspection was successfully added, False if Building was not added.</returns>
        public bool CreateInspection(Inspection newInspection)
        {
            bool result = false;

            try
            {
                LogicValidationExtensionMethods.ValidateInspectionName(newInspection.Name);
                LogicValidationExtensionMethods.ValidateInspectionRating(newInspection.Rating);
                LogicValidationExtensionMethods.ValidateAffiliation(newInspection.ResortInspectionAffiliation);
                LogicValidationExtensionMethods.ValidateInspectionProblemNotes(newInspection.InspectionProblemNotes);
                LogicValidationExtensionMethods.ValidateInspectionFixNotes(newInspection.InspectionFixNotes);
                result = (1 == inspectionAccessor.InsertInspection(newInspection));
            }
            catch (ArgumentNullException ane)
            {
                throw ane;
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception)
            {

                throw;
            }


            return result;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/03/14
        /// 
        /// Returns a list of inspections for the selected resort propery
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="newInspection">The Inspection obj to be added</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>List of inspections</returns>
        public List<Inspection> RetrieveAllInspectionsByResortPropertyId(int resortPropertyId)
        {
            List<Inspection> inspections = null;

            try
            {
                inspections = inspectionAccessor.SelectAllInspectionsByResortPropertyID(resortPropertyId);
            }
            catch (Exception)
            {

                throw;
            }

            return inspections;
        
        }

        public bool UpdateInspection(Inspection selectedInspection, Inspection newInspection)
        {
            bool result = false;

            try
            {
                LogicValidationExtensionMethods.ValidateInspectionName(selectedInspection.Name);
                LogicValidationExtensionMethods.ValidateInspectionRating(selectedInspection.Rating);
                LogicValidationExtensionMethods.ValidateAffiliation(selectedInspection.ResortInspectionAffiliation);
                LogicValidationExtensionMethods.ValidateInspectionProblemNotes(selectedInspection.InspectionProblemNotes);
                LogicValidationExtensionMethods.ValidateInspectionFixNotes(selectedInspection.InspectionFixNotes);
                result = (1 == inspectionAccessor.UpdateInspection(selectedInspection, newInspection));
            }
            catch (ArgumentNullException ane)
            {
                throw ane;
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
