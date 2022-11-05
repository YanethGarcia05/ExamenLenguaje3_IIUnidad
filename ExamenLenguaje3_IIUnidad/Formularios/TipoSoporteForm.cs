using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formularios
{
    public partial class TipoSoporteForm : Form
    {
        public TipoSoporteForm()
        {
            InitializeComponent();
        }

        SoporteDatos sopDatos = new SoporteDatos();
        string tipoOperacion = string.Empty;
        Soporte soporte;

        private async void LlenarDataGrid()
        {
            TipoDataGridView.DataSource = await sopDatos.DevolverListaAsync();
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            HabilitarControles();
            tipoOperacion = "Nuevo";
        }

        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            TipoSoporteComboBox.Enabled = true;
        }

        private void DesabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            TipoSoporteComboBox.Enabled = false;
  ;
        }

        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            TipoSoporteComboBox.Text = String.Empty;
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            tipoOperacion = "Modificar";
            if (TipoDataGridView.SelectedRows.Count > 0)
            {
                CodigoTextBox.Text = TipoDataGridView.CurrentRow.Cells["Codigo"].Value.ToString();
                TipoSoporteComboBox.Text = TipoDataGridView.CurrentRow.Cells["TipoSoporte"].Value.ToString();
                HabilitarControles();
                CodigoTextBox.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void GuardarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CodigoTextBox.Text))
            {
                errorProvider1.SetError(CodigoTextBox, "Ingrese el código");
                CodigoTextBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TipoSoporteComboBox.Text))
            {
                errorProvider1.SetError(TipoSoporteComboBox, "Seleccione un tipo de soporte");
                TipoSoporteComboBox.Focus();
                return;
            }
            soporte = new Soporte ();
            soporte.Codigo = Convert.ToInt32(CodigoTextBox.Text);
            soporte.TipoSoporte = TipoSoporteComboBox.Text;

            if (tipoOperacion == "Nuevo")
            {
                bool inserto = await sopDatos.InsertarAsync(soporte);
                if (inserto)
                {
                    LlenarDataGrid();
                    LimpiarControles();
                    DesabilitarControles();
                    MessageBox.Show("Soporte Guardado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tipo Soporte no se pudo guardar", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (tipoOperacion == "Modificar")
            {
                bool modifico = await sopDatos.ActualizarAsync(soporte);
                if (modifico)
                {
                    LlenarDataGrid();
                    LimpiarControles();
                    DesabilitarControles();
                    MessageBox.Show("Producto Guardado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Producto no se pudo guardar", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void EliminarButton_Click(object sender, EventArgs e)
        {
            if (TipoDataGridView.SelectedRows.Count > 0)
            {
                bool elimino = await sopDatos.EliminarAsync(TipoDataGridView.CurrentRow.Cells["Codigo"].Value.ToString());
                if (elimino)
                {
                    LlenarDataGrid();
                    MessageBox.Show("Soporte Eliminado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("El tipo de soporte no se pudo eliminar", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un soporte", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DesabilitarControles();
            LimpiarControles();
        }

        private void TipoSoporteForm_Load(object sender, EventArgs e)
        {
            LlenarDataGrid();
        }
    }
}
