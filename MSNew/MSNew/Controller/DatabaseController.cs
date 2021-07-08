using System;
using System.Collections.Generic;
using System.Text;
using MSNew.Interface;
using MSNew.Model;
using SQLite;
using Xamarin.Forms;
using System.Linq;

namespace MSNew.Controller
{
    public class DatabaseController
    {
        SQLiteConnection _mySqlConnection;

        public DatabaseController(string fileName)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(fileName);
            _mySqlConnection = new SQLiteConnection(databasePath);
            _mySqlConnection.CreateTable<DatabaseTables.DmnRevizorTable>(CreateFlags.ImplicitPK);
        }

        public IEnumerable<DatabaseTables.DmnRevizorTable> GetItems()
        {
            return (from i in _mySqlConnection.Table<DatabaseTables.DmnRevizorTable>() select i).ToList();
        }

        public DatabaseTables.DmnRevizorTable GetItem(int id)
        {
            return _mySqlConnection.Get<DatabaseTables.DmnRevizorTable>(id);
        }

        public int DeleteItem(int id)
        {
            return _mySqlConnection.Delete<DatabaseTables.DmnRevizorTable>(id);
        }

        public int SaveItem(DatabaseTables.DmnRevizorTable item)
        {
            if (item.Id != 0)
            {
                _mySqlConnection.Update(item);
                return item.Id;
            }
            else
            {
                return _mySqlConnection.Insert(item);
            }
        }
    }
}
