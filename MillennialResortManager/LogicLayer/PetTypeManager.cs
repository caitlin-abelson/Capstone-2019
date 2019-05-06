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
	/// <summary author="Craig Barkley" created="2019/02/10">
	/// This class is for the appointment Types in the logic layer, to be a connector to the 
	/// the Presentation Layer via the data.
	/// </summary>
	public class PetTypeManager : IPetTypeManager
	{
		IPetTypeAccessor _petTypeAccessor;

		public PetTypeManager()
		{
			_petTypeAccessor = new PetTypeAccessor();
		}

		public PetTypeManager(IPetTypeAccessor petTypeAccessor)
		{
			_petTypeAccessor = petTypeAccessor;
		}

		public PetTypeManager(PetTypeAccessorMock mock)
		{
			_petTypeAccessor = new PetTypeAccessorMock();
		}

		/// <summary author="Craig Barkley" created="2019/02/10">
		/// Method for creating a new Pet Type.
		/// </summary>
		/// <param name="PetType newPetType">The AddPetType calls the CreatePetType(newPetType).</param>
		/// <returns>petTypes</returns>
		public bool AddPetType(PetType newPetType)
		{
			ValidationExtensionMethods.ValidateID(newPetType.PetTypeID);
			ValidationExtensionMethods.ValidateDescription(newPetType.Description);

			bool result = false;

			try
			{
				result = (1 == _petTypeAccessor.CreatePetType(newPetType));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Craig Barkley" created="2019/02/10">
		/// Method for retrieving a Pet.
		/// </summary>
		/// <param name="()">The RetrieveAllPetTypes calls SelectAllPetTypeID.</param>
		/// <returns>result</returns>
		public List<string> RetrieveAllPetTypes()
		{
			List<string> petTypes = null;
			try
			{
				petTypes = _petTypeAccessor.SelectAllPetTypeID();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return petTypes;

		}

		/// <summary author="Craig Barkley" created="2019/02/10">
		/// Method forretrieving all PetType.
		/// </summary>
		/// <param name="string status">The RetrieveAllPetTypes calls RetrievetAllPetTypes(status).</param>
		/// <returns>petTypes</returns>
		public List<PetType> RetrieveAllPetTypes(string status)
		{
			List<PetType> petTypes = null;

			if (status != "")
			{
				try
				{
					petTypes = _petTypeAccessor.RetrievetAllPetTypes(status);
				}
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}

			return petTypes;
		}

		/// <summary author="Craig Barkley" created="2019/02/10">
		/// Method for deleting a Pet Type.
		/// </summary>
		/// <param name="string petTypeID">The DeletePetType calls DeletePetType(petTypeID).</param>
		/// <returns>result</returns>
		public bool DeletePetType(string petTypeID)
		{
			bool result = false;
			try
			{
				result = (1 == _petTypeAccessor.DeletePetType(petTypeID));
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