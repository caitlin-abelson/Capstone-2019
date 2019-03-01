using System;
using DataObjects;
using System.Collections.Generic;

namespace DataAccessLayer
/// <summary>
/// Jacob Miller
/// Created: 2019/01/22
/// </summary>
{
    public interface IPerformanceAccessor
    {
        int CreatePerformance(Performance perf);
        List<DataObjects.Performance> RetrieveAllPerformance();
        Performance RetrievePerformanceByID(int id);
        List<DataObjects.Performance> SearchPerformances(string term);
        int UpdatePerformance(Performance perf);
        void DeletePerformance(Performance perf);
    }
}
