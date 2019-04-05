using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Pet
    {

        public int PetID { get; set; }
        public string PetName { get; set; }
        public string Gender { get; set; }
        public string Species { get; set; }
        public string PetTypeID { get; set; }
        public int GuestID { get; set; }
        public string imageFilename { get; set; }  // Added on 3/21/19 by Matt Hill.


    }

}