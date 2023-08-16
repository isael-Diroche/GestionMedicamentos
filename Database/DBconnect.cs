using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMedicamentos.Database
{
    public class DBconnect
    {
        public SqlConnection dbcon = new SqlConnection("server=DESKTOP-MJ5IFSS; database=SGI; integrated security=true");

    }
}

