using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier.Database.Structure.Tables
{
    class Types
    {
        private string _tableName = "types";
        public string TableName { get => _tableName; }

        private int _idtype = -1;
        private string _desc;

        public int Idtype { get => _idtype; set => _idtype = value; }
        public string Desc { get => _desc; set => _desc = value; } // może być null
    }
}
