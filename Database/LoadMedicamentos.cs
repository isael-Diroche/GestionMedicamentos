using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionMedicamentos.Database
{
    public class LoadMedicamentos
    {
        public SqlConnection connection;
        
        public void LoadMedicamento(DataGridView inv)
        {
            DBconnect db = new DBconnect();
            connection = db.dbcon;
            DataSet data = new DataSet();
            SqlDataAdapter show = new SqlDataAdapter("Select * from medicamentos", connection);
            connection.Open();
            show.Fill(data, "Medicamentos");
            inv.DataSource = data.Tables["Medicamentos"].DefaultView;
            connection.Close();
        }
    }
}
