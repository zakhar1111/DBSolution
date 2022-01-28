using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBWorkAround._3394_reproduce
{
    [Table]
    public class Entity
    {
        [PrimaryKey, Column("id")]
        public Guid Id { get; set; }
    }
}
