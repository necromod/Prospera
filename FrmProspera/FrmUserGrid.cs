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
    public partial class FrmUserGrid : Form
    {
        public FrmUserGrid()
        {
  
            InitializeComponent();
            ValidaForm objValida = new ValidaForm();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // Atribua a nova fonte de dados
            dataGridView1.DataSource = objValida.ListarUsuario();
        }

        private void GvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmUserGrid_Load(object sender, EventArgs e)
        {

        }
    }
}
