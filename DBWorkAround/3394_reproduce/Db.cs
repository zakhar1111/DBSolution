using System;
using LinqToDB;
using DBWorkAround._3394_reproduce;

namespace DBWorkAround//._3394_reproduce
{
    public partial class Db
    {
        public ITable<Entity> Entity => GetTable<Entity>();

    }
}
