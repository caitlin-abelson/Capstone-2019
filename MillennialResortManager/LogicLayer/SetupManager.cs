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
    public class SetupManager : ISetupManager
    {
        private ISetupAccessor _setupAccessor;

		/// <summary author="Caitlin Abelson" created="2019/02/25">
		/// The constructor for the SetupManager class
		/// </summary>
		public SetupManager()
        {
            _setupAccessor = new SetupAccessor();
        }

		/// <summary author="Caitlin Abelson" created="2019/02/25">
		/// The constructor for for the SetupManager class 
		/// that takes an alternate setupAccessor.
		/// </summary>
		/// <param name="setupAccessor"></param>
		public SetupManager(ISetupAccessor setupAccessor)
        {
            _setupAccessor = setupAccessor;
        }

		/// <summary author="Caitlin Abelson" created="2019/02/28">
		/// </summary>
		/// <param name="setupID"></param>
		public void DeleteSetup(int setupID)
        {
            try
            {
                _setupAccessor.DeleteSetup(setupID);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Caitlin Abelson" created="2019/02/28">
		/// </summary>
		/// <param name="newSetup">The object that is being added to</param>
		/// <returns></returns>
		public int InsertSetup(Setup newSetup)
        {
            int result = 0;

            try
            {
                result = _setupAccessor.InsertSetup(newSetup);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
        }

		/// <summary author="Caitlin Abelson" created="2019/02/28">
		/// SelectAllSetups retrieves all of the setups into a list to sent to the presentation layer.
		/// </summary>
		/// <returns></returns>
		public List<Setup> SelectAllSetups()
        {
            List<Setup> setups = new List<Setup>();
            try
            {
                setups = _setupAccessor.SelectAllSetup();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return setups;
        }

		/// <summary author="Caitlin Abelson" created="2019/03/28">
		/// Retrieves the DatesEntered
		/// </summary>
		/// <param name="dateEntered"></param>
		/// <returns></returns>
		public List<VMSetup> SelectDateEntered(DateTime dateEntered)
        {
            List<VMSetup> setup = new List<VMSetup>();
            try
            {
                setup = _setupAccessor.SelectDateEntered(dateEntered);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return setup;
        }

		/// <summary author="Caitlin Abelson" created="2019/03/28">
		/// Retrieves the DatesRequired
		/// </summary>
		/// <param name="dateRequired"></param>
		/// <returns></returns>
		public List<VMSetup> SelectDateRequired(DateTime dateRequired)
        {
            List<VMSetup> setup = new List<VMSetup>();
            try
            {
                setup = _setupAccessor.SelectDateRequired(dateRequired);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return setup;
        }

		/// <summary author="Caitlin Abelson" created="2019/02/28">
		/// The manager method for selecting a setup by it's ID
		/// </summary>
		/// <param name="setupID"></param>
		/// <returns></returns>
		public Setup SelectSetup(int setupID)
        {
            Setup setup = new Setup();

            try
            {
                setup = _setupAccessor.SelectSetup(setupID);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return setup;
        }

		/// <summary author="Caitlin Abelson" created="2019/03/28">
		/// The manager method for selecting a setup by an event title
		/// </summary>
		/// <param name="eventTitle"></param>
		/// <returns></returns>
		public List<VMSetup> SelectSetupEventTitle(string eventTitle)
        {
            List<VMSetup> setup = new List<VMSetup>();
            try
            {
                setup = _setupAccessor.SelectSetupEventTitle(eventTitle);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return setup;
        }

		/// <summary author="Caitlin Abelson" created="2019/03/28">
		/// The manager method to retrieve a list of the setups for the view model.
		/// </summary>
		/// <returns></returns>
		public List<VMSetup> SelectVMSetups()
        {
            List<VMSetup> vmSetup = new List<VMSetup>();

            try
            {
                vmSetup = _setupAccessor.SelectVMSetups();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return vmSetup;
        }

		/// <summary author="Caitlin Abelson" created="2019/02/28">
		/// The manager method for updating a setup
		/// </summary>
		/// <param name="newSetup"></param>
		/// <param name="oldSetup"></param>
		public void UpdateSetup(Setup newSetup, Setup oldSetup)
        {
            try
            {
                _setupAccessor.UpdateSetup(newSetup, oldSetup);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}
    }
}