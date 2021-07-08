using System;
using System.Collections.Generic;
using System.Text;

namespace MSNew.Interface
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
