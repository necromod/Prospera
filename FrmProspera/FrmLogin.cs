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
    public partial class FrmLogin : Form
    {
        Usuario ObjUsuario = new Usuario();
        TipoUsuarioModel objTpUsuario = new TipoUsuarioModel();
        ValidaForm validaForm = new ValidaForm();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void lblSair_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           Close();
        }

        private void TxtLogin_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            ObjUsuario.NomeUsuario = TxtLogin.Text.Trim();
            ObjUsuario.SenhaUsuario = TxtSenha.Text.Trim();
            Usuario validaLogin =  validaForm.VerificarCredenciais(ObjUsuario);

            if (validaLogin != null)
            {
                MessageBox.Show("Usuario logado com sucesso!!");
                FrmMenuInicial menuInicial = new FrmMenuInicial();
                menuInicial.ShowDialog();
                Close();

            }
            else
            {
                MessageBox.Show("Login ou Senha incorretos!! Usuario não encontrado");
            }
        }

        private void chkMostraSenha_CheckedChanged(object sender, EventArgs e)
        {

            if (chkMostraSenha.Checked)
            {
                // Mostra a senha
                TxtSenha.PasswordChar = '\0';
            }
            else
            {
                // Oculta a senha
                TxtSenha.PasswordChar = '*';
            }
        }
    }
}
