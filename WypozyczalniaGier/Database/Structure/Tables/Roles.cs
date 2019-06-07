using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier.Database.Structure.Tables
{
    class Roles
    {
        private string _tableName = "roles";
        public string TableName { get => _tableName; }

        private int _idrole = -1;
        private string _roledesc;

        public int Idrole { get => _idrole; set => _idrole = value; }
        public string Roledesc { get => _roledesc; set => _roledesc = value; } // może być null
    }
}