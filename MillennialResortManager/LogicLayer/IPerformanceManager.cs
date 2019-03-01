using System;
using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Jacob Miller
    /// Created: 2019/01/22
    /// </summary>
    public interface IPerformanceManager
    {
        int AddPerformance(Performance perf);
        int EditPerformance(Performance perf);
        List<Performance> RetrieveAllPerformance();
        Performance RetrievePerformanceByID(int id);
        List<Performance> SearchPerformances(string term);
        void DeletePerformance(Performance perf);
    }
}