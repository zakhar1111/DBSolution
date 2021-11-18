using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround
{
    public class Products
    {
        public int ProductID { get; set; }
         
        public string Name { get; set; }
         
        public string Description { get; set; }
        
        public decimal Price { get; set; }
         
        public string Category { get; set; }

        public override string ToString()
        {
            base.ToString();
            return $"id :        {ProductID},Name :        {Name}, Description : {Description},Price:        {Price},Category:     {Category}";

        }
    }
}
