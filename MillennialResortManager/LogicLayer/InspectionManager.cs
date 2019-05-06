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
	/// <summary author="Danielle Russo" created="2019/01/21">
	/// Class that interacts with the presentation layer and building access layer
	/// </summary>
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

		/// <summary author="Danielle Russo" created="2019/02/27">
		/// Adds a new Inspection obj.
		/// </summary>
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
				ExceptionLogManager.getInstance().LogException(ane);
				throw ane;
			}
			catch (ArgumentException ae)
			{
				ExceptionLogManager.getInstance().LogException(ae);
				throw ae;
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Danielle Russo" created="2019/03/14">
		/// Returns a list of inspections for the selected resort propery
		/// </summary>
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
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
				ExceptionLogManager.getInstance().LogException(ane);
				throw ane;
			}
			catch (ArgumentException ae)
			{
				ExceptionLogManager.getInstance().LogException(ae);
				throw ae;
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}
	}
}
