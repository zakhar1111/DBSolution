using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround
{
    [Table]
    public class Cars
    {
        [Column, Nullable] public string Name;
        [Column, Nullable] public string Color;
        [Column] public int OwnerName;
        // [Association(ThisKey =nameof(OwnerNameID), OtherKey = nameof(Owners.id))]
        // public Owners OwnerNameID { get; set; }

        public override string ToString()
        {
            base.ToString();
            return $"Name : {Name},Color : {Color}, OwmerName : {OwnerName}";

        }
    }
}
