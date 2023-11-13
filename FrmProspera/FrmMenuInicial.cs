using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmProspera
{
    public partial class FrmMenuInicial : Form
    {
        public FrmMenuInicial()
        {
            InitializeComponent();
        }


        private void MenuSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuCadastrar_Click(object sender, EventArgs e)
        {
            FrmCadastroUsuario frmCadastroUsuario = new FrmCadastroUsuario();
            frmCadastroUsuario.ShowDialog();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUserGrid frmUserGrid = new FrmUserGrid();
            frmUserGrid.ShowDialog();
        }
    }
}
