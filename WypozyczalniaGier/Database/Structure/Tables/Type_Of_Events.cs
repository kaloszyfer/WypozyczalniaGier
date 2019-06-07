using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier.Database.Structure.Tables
{
    class Type_Of_Events
    {
        private string _tableName = "type_of_events";
        public string TableName { get => _tableName; }

        private int _idevent = -1;
        private string _desc;

        public int Idevent { get => _idevent; set => _idevent = value; }
        public string Desc { get => _desc; set => _desc = value; } // może być null
    }
}
