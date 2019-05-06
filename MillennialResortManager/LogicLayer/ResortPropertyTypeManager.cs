using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Francis Mingomba" created="2019/04/03">
	/// Resort Property Type Manager
	/// </summary>
	public class ResortPropertyTypeManager : IResortPropertyTypeManager
    {
        private readonly IResortPropertyTypeAccessor _resortPropertyTypeAccessor;

        public ResortPropertyTypeManager(IResortPropertyTypeAccessor resortPropertyTypeAccessor)
        {
            _resortPropertyTypeAccessor = resortPropertyTypeAccessor;
        }

        public ResortPropertyTypeManager() : this(new ResortPropertyTypeAccessor()) { }

		/// <summary author="Francis Mingomba" created="2019/04/03">
		///Adds Resort Property Type to database
		/// </summary>
		/// <param name="resortPropertyType">resort property type object</param>
		/// <returns>id to new resort property type added</returns>
		public string AddResortPropertyType(ResortPropertyType resortPropertyType)
        {
            string resortPropertyTypeId;

            try
            {
                this.MeetsValidationCriteria(resortPropertyType, GetValidationCriteria());

                resortPropertyTypeId = _resortPropertyTypeAccessor.AddResortPropertyType(resortPropertyType);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return resortPropertyTypeId;
        }

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Retrieves Resort Property Type Id from database
		/// </summary>
		/// <param name="id">Resort Property Type Id</param>
		/// <returns>Resort Property Type</returns>
		public ResortPropertyType RetrieveResortPropertyTypeById(string id)
        {
            ResortPropertyType resortPropertyType;

            try
            {
                resortPropertyType = _resortPropertyTypeAccessor.RetrieveResortPropertyTypeById(id);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return resortPropertyType;
        }

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Retrieves Resort Property Types
		/// </summary>
		/// <returns>Resort Property Type Collection</returns>
		public IEnumerable<ResortPropertyType> RetrieveResortPropertyTypes()
        {
            IEnumerable<ResortPropertyType> resortPropertyTypes;

            try
            {
                resortPropertyTypes = _resortPropertyTypeAccessor.RetrieveResortPropertyTypes();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return resortPropertyTypes;
        }

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Updates Resort Property Type in database
		/// </summary>
		/// <param name="old">Old Resort Property Type</param>
		/// <param name="newResortPropertyType">New Resort Property Type</param>
		public void UpdateResortPropertyType(ResortPropertyType old, ResortPropertyType newResortPropertyType)
        {
            try
            {
                this.MeetsValidationCriteria(newResortPropertyType, GetValidationCriteria());

                _resortPropertyTypeAccessor.UpdateResortPropertyType(old, newResortPropertyType);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Deletes Resort Property Type in database
		/// </summary>
		/// <param name="id">Resort Property Type Id</param>
		public void DeleteResortPropertyType(string id, Employee employee)
        {
            try
            {
                if (!employee.HasRoles(out string errorStr, "Admin"))
                    throw new ApplicationException(errorStr);

                _resortPropertyTypeAccessor.DeleteResortPropertyType(id);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Validation Rules for Resort Property Type
		/// </summary>
		/// <returns>Dictionary containing validation rules</returns>
		public Dictionary<string, ValidationCriteria> GetValidationCriteria()
        {
            return new Dictionary<string, ValidationCriteria>
            {
                {
                    nameof(ResortPropertyType.ResortPropertyTypeId),
                    new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = 20}
                }
            };
        }
    }
}