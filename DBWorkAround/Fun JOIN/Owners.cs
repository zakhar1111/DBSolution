using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround
{
    [Table]
    public class Owners
    {
        [Column, Nullable] public string Owner;
        [PrimaryKey, Nullable] public int id;
    }
}
