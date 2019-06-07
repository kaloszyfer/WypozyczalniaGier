using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier.Database.Structure.Tables
{
    class Platforms
    {
        private string _tableName = "platforms";
        public string TableName { get => _tableName; }

        private int _idplatform = -1;
        private string _desc;

        public int Idplatform { get => _idplatform; set => _idplatform = value; }
        public string Desc { get => _desc; set => _desc = value; } // może być null
    }
}
