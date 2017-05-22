using System;
using SQLite;

namespace Core.Storage.Entity
{
    public class ServicesEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Service { get; set; }

        public int Requirements { get; set; }
    }
}
