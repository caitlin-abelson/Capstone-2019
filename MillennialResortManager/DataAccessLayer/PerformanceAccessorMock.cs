using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Jacob Miller
    /// Created: 2019/2/15
    /// Mock Data Accessor that implements the IPerformanceManager interface for testing
    /// </summary>
    public class PerformanceAccessorMock : IPerformanceAccessor
    {
        private List<Performance> _performances;

        /// <summary>
        /// Setting up test data
        /// </summary>
        public PerformanceAccessorMock()
        {
            _performances = new List<Performance>();
            _performances.Add(new Performance(100000, "Test Name 1", new DateTime(2018, 3, 21), "Test Description 1"));
            _performances.Add(new Performance(100001, "Test Name 2", new DateTime(2018, 3, 22), "Test Description 2"));
            _performances.Add(new Performance(100002, "Test Name 3", new DateTime(2018, 3, 23), "Test Description 3"));
            _performances.Add(new Performance(100003, "Test Name 4", new DateTime(2018, 3, 24), "Test Description 4"));
        }
        public int InsertPerformance(Performance perf)
        {
            _performances.Add(perf);
            return 0;
        }

        public List<Performance> SelectAllPerformance()
        {
            return _performances;
        }

        public Performance SelectPerformanceByID(int id)
        {
            Performance p = null;
            p = _performances.Find(x => x.ID == id);
            if (p == null)
            {
                throw new ApplicationException("Couldn't find any Performance with matching ID.");
            }
            return p;
        }

        public List<Performance> SearchPerformances(string term)
        {
            List<Performance> performances = new List<Performance>();
            performances.Add(_performances.Find(x => x.Name.Contains(term)));
            return performances;
        }

        public int UpdatePerformance(Performance perf)
        {
            bool found = false;
            for (int i = 0; i < _performances.Capacity; i++)
            {
                if (_performances[i].ID == perf.ID)
                {
                    _performances[i].Name = perf.Name;
                    _performances[i].Date = perf.Date;
                    _performances[i].Description = perf.Description;
                }
            }
            if (found)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public void DeletePerformance(Performance perf)
        {
            _performances.Remove(perf);
        }
    }
}
