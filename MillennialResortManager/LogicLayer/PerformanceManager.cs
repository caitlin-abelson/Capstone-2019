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
	/// <summary author="Jacob Miller" created="2019/01/22">
	/// </summary>
	public class PerformanceManager : IPerformanceManager
    {
        private IPerformanceAccessor performanceAccessor;

		/// <summary author="Jacob Miller" created="2019/01/22">
		/// A contstructor for use with a live database
		/// </summary>
		public PerformanceManager()
        {
            performanceAccessor = new PerformanceAccessor();
        }

		/// <summary author="Jacob Miller" created="2019/01/22">
		/// A constructor using a mock data accessor object
		/// </summary>
		/// <param name="mock">The mock data accessor</param>
		public PerformanceManager(PerformanceAccessorMock mock)
        {
            performanceAccessor = mock;
        }

		/// <summary author="Jacob Miller" created="2019/01/22">
		/// </summary>
		/// <param name="perf">The Performance to be added</param>
		/// <returns>Number of rows that will be affected... should only be one</returns>
		public int AddPerformance(Performance perf)
        {
            int rows = 0;
            try
            {
                if (!perf.isValid())
                {
                    throw new ApplicationException("The performance is not valid");
                }
                rows = performanceAccessor.InsertPerformance(perf);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return rows;
        }

		/// <summary author="Jacob Miller" created="2019/01/22">
		/// </summary>
		/// <param name="id">The ID of the target Performance</param>
		/// <returns>The Performance with the matching ID</returns>
		public Performance RetrievePerformanceByID(int id)
        {
            return performanceAccessor.SelectPerformanceByID(id);
        }

		/// <summary author="Jacob Miller" created="2019/01/22">
		/// </summary>
		/// <param name="perf">The Performance containing the edited information</param>
		/// <returns>The number of rows affected... should be one</returns>
		public int EditPerformance(Performance perf)
        {
            int rows = 0;
            try
            {
                if (!perf.isValid())
                {
                    throw new ApplicationException();
                }
                rows = performanceAccessor.UpdatePerformance(perf);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return rows;
        }

		/// <summary author="Jacob Miller" created="2019/01/22">
		/// </summary>
		/// <returns>All performances in the DB</returns>
		public List<Performance> RetrieveAllPerformance()
        {
            return performanceAccessor.SelectAllPerformance();
        }

		/// <summary author="Jacob Miller" created="2019/01/22">
		/// </summary>
		/// <param name="term">The string that the user is trying to find</param>
		/// <returns>All Performances containing the string provided</returns>
		public List<Performance> SearchPerformances(string term)
        {
            return performanceAccessor.SearchPerformances(term);
        }

		/// <summary author="Jacob Miller" created="2019/01/22">
		/// This method takes a Performance object for simplicity and uniformality but only use the ID and ignore the rest of the fields
		/// </summary>
		/// <param name="perf">The Performance to be deleted</param>
		public void DeletePerformance(Performance perf)
        {
            performanceAccessor.DeletePerformance(perf);
        }
    }
}