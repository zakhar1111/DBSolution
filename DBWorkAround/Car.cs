using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBWorkAround
{
    class Car
    {
        //[Key]
        public int CarID { get; set; }
                
        //public int NumberOfWeel { get; set; }

        public string Color { get; set; }

        public string Name { get; set; }

        public string OwnerName { get; set; }
    }
}
