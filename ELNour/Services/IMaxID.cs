using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELNour.Services
{
    interface IMaxID
    {
        int MaxIDs(string Coulmn_Name, string TableDb);
        int MaxIDs(string Coulmn_Name, string TableDb, string condi, int value);
    }
}
