using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formularios
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        UsuariosForm _usuariosForm = null;
        TipoSoporteForm _soporteForm = null;

        private void UsuariosToolStripButton1_Click(object sender, EventArgs e)
        {
            if (_usuariosForm == null)
            {
                _usuariosForm = new UsuariosForm();
                _usuariosForm.MdiParent = this;
                _usuariosForm.FormClosed += _usuariosForm_FormClosed;
                _usuariosForm.Show();
            }
            else
            {
                _usuariosForm.Activate();
            }
        }

        private void _usuariosForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _usuariosForm = null;
        }

        private void TipoSoporteToolStripButton1_Click(object sender, EventArgs e)
        {
            if (_soporteForm == null)
            {
                _soporteForm = new TipoSoporteForm();
                _soporteForm.MdiParent = this;
                _soporteForm.FormClosed += _soporteForm_FormClosed;
                _soporteForm.Show();
            }
            else
            {
                _soporteForm.Activate();
            }
        }

        private void _soporteForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _usuariosForm=null;
        }
    }
}
