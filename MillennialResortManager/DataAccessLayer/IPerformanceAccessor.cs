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
        int InsertPerformance(Performance perf);
        List<DataObjects.Performance> SelectAllPerformance();
        Performance SelectPerformanceByID(int id);
        List<DataObjects.Performance> SearchPerformances(string term);
        int UpdatePerformance(Performance perf);
        void DeletePerformance(Performance perf);
    }
}
