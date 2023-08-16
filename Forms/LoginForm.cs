
using GestionMedicamentos.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionMedicamentos
{
    public partial class loginForm : Form
    {


        public loginForm()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Obtener los valores ingresados por el usuario
            string username = txtUsername.Text;
            string password = Tools.Sha256.GetSHA256(txtPassword.Text);

            // Validar el inicio de sesión con la base de datos

            Login log = new Login();
            log.Log(username, password);
            if (log.conf == true)
            {
                this.Hide();
                MainForm das = new MainForm();
                das.ShowDialog();
                this.Close();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cerrar la aplicación si se hace clic en "Cancelar"
            Application.Exit();
        }

        private void loginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
