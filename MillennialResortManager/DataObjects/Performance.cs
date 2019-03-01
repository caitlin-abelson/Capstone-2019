using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Performance
    {
        /// <summary>
        /// Jacob Miller
        /// Created: 2019/01/22
        /// </summary>
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Performance(int id, string name, DateTime date, string desc)
        {
            ID = id;
            Name = name;
            Date = date;
            Description = desc;
        }

        public bool isValid()
        {
            bool result = true;
            if(Name == "" || Name == null)
            {
                result = false;
            }
            if (Date < DateTime.Now)
            {
                result = false;
            }
            return result;
        }
    }
}
