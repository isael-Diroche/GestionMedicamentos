using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionMedicamentos;

namespace GestionMedicamentos.Forms
{
    public partial class formAgregar : Form
    {
        public formAgregar()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarNuevoMedicamento_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("server=DESKTOP-MJ5IFSS; database=SGI; integrated security=true"))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO medicamentos (nombreMedicamento, descripcion, lote, fechaCaducidad, stock, distribuidor) " +
                                        "VALUES (@nombre, @descripcion, @lote, @fechaCaducidad, @stock, @distribuidor)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                        command.Parameters.AddWithValue("@lote", txtLote.Text);
                        command.Parameters.AddWithValue("@fechaCaducidad", dtpFechaCaducidad.Value);
                        command.Parameters.AddWithValue("@stock", int.Parse(txtStock.Text));
                        command.Parameters.AddWithValue("@distribuidor", txtDistribuidor.Text);

                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"Se agregaron {rowsAffected} registros a la tabla medicamentos.");
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
