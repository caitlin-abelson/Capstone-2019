using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Francis Mingomba" created="2019/04/03">
	/// Resort Property Manager
	/// </summary>
	public class ResortPropertyManager : IResortPropertyManager
	{
		private readonly IResortPropertyAccessor _resortPropertyAccessor;

		public ResortPropertyManager(IResortPropertyAccessor resortPropertyAccessor)
		{
			_resortPropertyAccessor = resortPropertyAccessor;
		}

		public ResortPropertyManager() : this(new ResortPropertyAccessor()) { }

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Adds Resort Property to database
		/// </summary>
		/// <param name="resortProperty">resort property</param>
		/// <returns>resort property id</returns>
		public int AddResortProperty(ResortProperty resortProperty)
		{
			int resortPropertyId;

			try
			{
				this.MeetsValidationCriteria(resortProperty, GetResortVehicleValidationCriteria());

				resortPropertyId = _resortPropertyAccessor.AddResortProperty(resortProperty);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return resortPropertyId;
		}

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Retrieves Resort Property By Id
		/// </summary>
		/// <param name="id">Resort Property Id</param>
		/// <returns>Resort Property Object</returns>
		public ResortProperty RetrieveResortPropertyById(string id)
		{
			ResortProperty resortProperty;

			try
			{
				resortProperty = _resortPropertyAccessor.RetrieveResortPropertyById(id);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return resortProperty;
		}

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Retrieves all Resort Properties
		/// </summary>
		/// <returns>Resort Property Collection</returns>
		public IEnumerable<ResortProperty> RetrieveResortProperties()
		{
			IEnumerable<ResortProperty> resortProperties;

			try
			{
				resortProperties = _resortPropertyAccessor.RetrieveResortProperties();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return resortProperties;
		}

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Updates Resort Property
		/// </summary>
		/// <param name="oldResortProperty">Old Resort Property (database copy)</param>
		/// <param name="newResortProperty">New Resort Property (new copy)</param>
		public void UpdateResortProperty(ResortProperty oldResortProperty, ResortProperty newResortProperty)
		{
			try
			{
				this.MeetsValidationCriteria(newResortProperty, GetResortVehicleValidationCriteria());

				_resortPropertyAccessor.UpdateResortProperty(oldResortProperty, newResortProperty);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Francis Mingomba" created="2019/04/03">
		/// Deletes Resort Property From database
		/// </summary>
		/// <param name="id"></param>
		public void DeleteResortProperty(int id, Employee employee)
		{
			try
			{
				if (!employee.HasRoles(out var errorStr, "Admin"))
					throw new ApplicationException(errorStr);

				_resortPropertyAccessor.DeleteResortProperty(id);
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
		private Dictionary<string, ValidationCriteria> GetResortVehicleValidationCriteria()
		{
			return new Dictionary<string, ValidationCriteria>
			{
				{
					nameof(ResortProperty.ResortPropertyId),
					new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = int.MaxValue}
				},
				{
					nameof(ResortProperty.ResortPropertyTypeId),
					new ValidationCriteria {CanBeNull = false, LowerBound = 0, UpperBound = 20}
				}
			};
		}
	}
}