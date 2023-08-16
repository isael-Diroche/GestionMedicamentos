using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionMedicamentos.Database
{
    public class Login
    {
        DBconnect con = new DBconnect();
        SqlConnection connect = new SqlConnection();
        public bool conf;

        public void Log(string uname, string pass)
        {
            connect = con.dbcon;
            connect.Open();

            SqlCommand logCheck = new SqlCommand($"select username,passwd from users where username='{uname}' and passwd='{pass}'", connect);
            logCheck.ExecuteNonQuery();
            SqlDataReader reader = logCheck.ExecuteReader();

            if (reader.Read())
            {
                conf = true;
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrecta");
            }

            connect.Close();

        }
    }
}
