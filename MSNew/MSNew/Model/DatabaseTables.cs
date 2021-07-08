using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MSNew.Model
{
    public class DatabaseTables
    {
        [Table("DmnRevizor1")]
        public class DmnRevizorTable
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }

            public string Rn { get; set; }
            public string DocNum { get; set; }
            public string DocData { get; set; }
            public string CurrentQuant { get; set; }
            public string TotalQuant { get; set; }
            public string ListSpec { get; set; }
        }
    }
}
