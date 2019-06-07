using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WypozyczalniaGier.Database.Structure.Tables;

namespace WypozyczalniaGier.Data
{
    class Storage
    {
        public Storage()
        {
            _Init();
        }
        private void _Init()
        {
            TableActions = new List<Database.Structure.Tables.Actions>();
            TableInventory = new List<Actions>();
            TablePlatforms = new List<Platforms>();
            TableProducts = new List<Products>();
            TableRoles = new List<Roles>();
            TableType_of_events = new List<Type_Of_Events>();
            TableTypes = new List<Types>();
            TableUser = new List<User>();
        }

        public List<Database.Structure.Tables.Actions> TableActions { get; set; }
        public List<Actions> TableInventory { get; set; }
        public List<Platforms> TablePlatforms { get; set; }
        public List<Products> TableProducts { get; set; }
        public List<Roles> TableRoles { get; set; }
        public List<Type_Of_Events> TableType_of_events { get; set; }
        public List<Types> TableTypes { get; set; }
        public List<User> TableUser { get; set; }
    }
}