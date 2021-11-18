using System;
using System.Collections.Generic;
using System.Text;
using LinqToDB.Mapping;

namespace DBWorkAround
{
    [Table]
    public class ToDoTable
    {
        [PrimaryKey, Identity] public int ID;
        [Column, Nullable] public string Name;
        [Column, Nullable] public string Description;
        [Column] public DateTime? CreatedON;
    }

    [Table]
    public class ToDoTable2
    {
        [PrimaryKey, Identity] public int ID;
        [Column, Nullable] public string Name;
        [Column, Nullable] public string Description;
        [Column] public DateTime? CreatedON;
    }

    [Table]
    public class ToDoTable3
    {
        [PrimaryKey] public int ID;
        [Column, Nullable] public string Name;

    }
}
