
using GestionMedicamentos.Database;
using GestionMedicamentos.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionMedicamentos
{
    public partial class MainForm : Form
    {
        public SqlConnection connection;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sGIDataSet_facturas.facturas' Puede moverla o quitarla según sea necesario.
            this.facturasTableAdapter.Fill(this.sGIDataSet_facturas.facturas);
            // TODO: esta línea de código carga datos en la tabla 'sGIDataSet.medicamentos' Puede moverla o quitarla según sea necesario.
            this.medicamentosTableAdapter.Fill(this.sGIDataSet.medicamentos);
            // Cargar los datos de la tabla Medicamentos en el DataGridView
            LoadMedicamentos show = new LoadMedicamentos();
            panelHome.BringToFront();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            panelHome.BringToFront();
        }

        private void btnMedicamentos_Click(object sender, EventArgs e)
        {
            panelMedicamentos.BringToFront();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            panelFacturas.BringToFront();
        }

        private void btnLimpiarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("server=DESKTOP-MJ5IFSS; database=SGI; integrated security=true"))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM facturas";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"Se eliminaron {rowsAffected} registros de la tabla facturas.");
                    }
                }
                CargarDatosEnDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CargarDatosEnDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("server=DESKTOP-MJ5IFSS; database=SGI; integrated security=true"))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM facturas";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection))
                    {
                        DataTable dtFacturas = new DataTable();
                        adapter.Fill(dtFacturas);

                        dgvCarrito.DataSource = dtFacturas;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }

            try
            {
                using (SqlConnection connection = new SqlConnection("server=DESKTOP-MJ5IFSS; database=SGI; integrated security=true"))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM medicamentos";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection))
                    {
                        DataTable dtMedicamentos = new DataTable();
                        adapter.Fill(dtMedicamentos);

                        dgvMedicamentos2.DataSource = dtMedicamentos;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo para confirmar el cierre de sesión
            DialogResult result = MessageBox.Show("¿Está seguro de querer cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Si el usuario confirma cerrar sesión, regresa a LoginForm
                this.Hide();

                loginForm loginForm = new loginForm();
                loginForm.ShowDialog();

                if (loginForm.DialogResult == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formAgregar formAgregar = new formAgregar();
            formAgregar.ShowDialog();
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            CargarDatosEnDataGridView();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvMedicamentos2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvMedicamentos2.SelectedRows[0];
                dgvMedicamentos2.Rows.Remove(selectedRow);
            }
            else
            {
                MessageBox.Show("Selecciona una fila para eliminar.");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            formAgregar formAgregar = new formAgregar();
            formAgregar.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMedicamentos2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvMedicamentos2.SelectedRows[0];
                dgvMedicamentos2.Rows.Remove(selectedRow);
                MessageBox.Show("Registro eliminado correctamente");
            }
            else
            {
                MessageBox.Show("Selecciona una fila para eliminar.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("server=DESKTOP-MJ5IFSS; database=SGI; integrated security=true"))
                {
                    connection.Open();

                    foreach (DataGridViewRow row in dgvMedicamentos2.Rows)
                    {
                        int idMedicamento = Convert.ToInt32(row.Cells["idMedicamento"].Value);
                        string nombre = row.Cells["nombreMedicamento"].Value.ToString();
                        string descripcion = row.Cells["descripcion"].Value.ToString();
                        string lote = row.Cells["lote"].Value.ToString();
                        DateTime fechaCaducidad = Convert.ToDateTime(row.Cells["fechaCaducidad"].Value);
                        int stock = Convert.ToInt32(row.Cells["stock"].Value);
                        string distribuidor = row.Cells["distribuidor"].Value.ToString();

                        string updateQuery = "UPDATE medicamentos SET nombreMedicamento = @nombre, descripcion = @descripcion, lote = @lote, " +
                                             "fechaCaducidad = @fechaCaducidad, stock = @stock, distribuidor = @distribuidor " +
                                             "WHERE idMedicamento = @idMedicamento";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@nombre", nombre);
                            command.Parameters.AddWithValue("@descripcion", descripcion);
                            command.Parameters.AddWithValue("@lote", lote);
                            command.Parameters.AddWithValue("@fechaCaducidad", fechaCaducidad);
                            command.Parameters.AddWithValue("@stock", stock);
                            command.Parameters.AddWithValue("@distribuidor", distribuidor);
                            command.Parameters.AddWithValue("@idMedicamento", idMedicamento);

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Cambios guardados correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cambios: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("server=DESKTOP-MJ5IFSS; database=SGI; integrated security=true"))
                {
                    connection.Open();

                    foreach (DataGridViewRow row in dgvMedicamentos2.Rows)
                    {
                        int idMedicamento = Convert.ToInt32(row.Cells["idMedicamento"].Value);
                        string nombre = row.Cells["nombreMedicamento"].Value.ToString();
                        string descripcion = row.Cells["descripcion"].Value.ToString();
                        string lote = row.Cells["lote"].Value.ToString();
                        DateTime fechaCaducidad = Convert.ToDateTime(row.Cells["fechaCaducidad"].Value);
                        int stock = Convert.ToInt32(row.Cells["stock"].Value);
                        string distribuidor = row.Cells["distribuidor"].Value.ToString();

                        string updateQuery = "UPDATE medicamentos SET nombreMedicamento = @nombre, descripcion = @descripcion, lote = @lote, " +
                                             "fechaCaducidad = @fechaCaducidad, stock = @stock, distribuidor = @distribuidor " +
                                             "WHERE idMedicamento = @idMedicamento";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@nombre", nombre);
                            command.Parameters.AddWithValue("@descripcion", descripcion);
                            command.Parameters.AddWithValue("@lote", lote);
                            command.Parameters.AddWithValue("@fechaCaducidad", fechaCaducidad);
                            command.Parameters.AddWithValue("@stock", stock);
                            command.Parameters.AddWithValue("@distribuidor", distribuidor);
                            command.Parameters.AddWithValue("@idMedicamento", idMedicamento);

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Cambios guardados correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cambios: " + ex.Message);
            }
        }
    }
}
