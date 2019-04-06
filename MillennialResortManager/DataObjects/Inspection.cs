/// <summary>
/// Danielle Russo
/// Created: 2019/02/28
/// 
/// Inspection object that reflects the Inspection table in the data dictionary
/// </summary>
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Inspection
    {
        public int InspectionID { get; set; }
        public int ResortPropertyID { get; set; }
        public string Name { get; set; }
        public DateTime DateInspected { get; set; }
        public string Rating { get; set; }
        public string ResortInspectionAffiliation { get; set; }
        public string InspectionProblemNotes { get; set; }
        public string InspectionFixNotes { get; set; }
    }
}
