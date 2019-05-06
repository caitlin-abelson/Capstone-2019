using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Jacob Miller" created="2019/03/28">
	/// </summary>
	public class LuggageManager
	{
		IGuestAccessor guestAccessor;
		ILuggageAccessor luggageAccessor;
		public LuggageManager()
		{
			luggageAccessor = new LuggageAccessor();
			guestAccessor = new GuestAccessor();
		}
		public LuggageManager(MockLuggageAccessor mockLuggage, GuestAccessor mockGuest)
		{
			luggageAccessor = mockLuggage;
			guestAccessor = mockGuest;
		}
		public bool AddLuggage(Luggage l)
		{
			bool result = false;
			try
			{
				result = luggageAccessor.CreateLuggage(l);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		public Luggage RetrieveLuggageByID(int id)
		{
			Luggage pleaseUseATryCatch = new Luggage();
			try
			{
				pleaseUseATryCatch = luggageAccessor.RetrieveLuggageByID(id);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return pleaseUseATryCatch;
		}

		public List<Luggage> RetrieveAllLuggage()
		{
			List<Luggage> luggage;
			try
			{
				luggage = luggageAccessor.RetrieveAllLuggage();
				Guest g;
				for (int l = 0; l < luggage.Count; l++)
				{
					g = guestAccessor.SelectGuestByGuestID(luggage[l].GuestID);
					luggage[l].GuestFirstName = g.FirstName;
					luggage[l].GuestLastName = g.LastName;
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return luggage;
		}
		public bool EditLuggage(Luggage oldLuggage, Luggage newLuggage)
		{
			bool result = false;
			try
			{
				result = luggageAccessor.UpdateLuggage(oldLuggage, newLuggage);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}
		public bool DeleteLuggage(Luggage l)
		{
			bool result = false;
			try
			{
				result = luggageAccessor.DeleteLuggage(l);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}
		public List<LuggageStatus> RetrieveAllLuggageStatus()
		{
			List<LuggageStatus> s;
			try
			{
				s = luggageAccessor.RetrieveAllLuggageStatus();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return s;
		}
	}
}