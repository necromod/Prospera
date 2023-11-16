using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FrmProspera
{
    public partial class FrmCadastroUsuario : Form
    {
        private ErrorProvider errorProvider = new ErrorProvider();
        Usuario ObjUsuario = new Usuario();
        TipoUsuarioModel objTpUsuario = new TipoUsuarioModel();
        ValidaForm validaForm = new ValidaForm();
        // Configurar a máscara para o CPF

        private void LimpaTxt()
        {
            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                {
                    ((System.Windows.Forms.TextBox)control).Clear();
                }
                if (control is System.Windows.Forms.MaskedTextBox)
                {
                    ((System.Windows.Forms.MaskedTextBox)control).Clear();
                }

            }

            BtnCadastrar.Enabled = true;
            TextIdUser.Enabled = false;
            TextDataCadastro.Enabled = false;
            TextStatus.Enabled = false;
        }

        private void LimparErros()
        {
            // Lista de TextBoxes que você deseja validar
            System.Windows.Forms.TextBox[] campos = { TextNome, TextEmail, TextSenha, TextCargo, TextConfirmaSenha };

            foreach (var campo in campos)
            {
                // Limpar mensagem de erro se o campo estiver preenchido
                errorProvider.SetError(campo, null);
            }
        }

        private bool ValidarCampos()
        {
            // Lista de TextBoxes que você deseja validar
            System.Windows.Forms.TextBox[] campos = { TextNome, TextEmail, TextSenha, TextCargo, TextConfirmaSenha };

            foreach (var campo in campos)
            {
                if (String.IsNullOrEmpty(campo.Text))
                {
                    // Exibir mensagem de erro usando o ErrorProvider
                    errorProvider.SetError(campo, "Campo obrigatório!");
                    campo.Focus();
                    return false;  // Retorna false se houver campos não preenchidos
                }
                else
                {
                    // Limpar mensagem de erro se o campo estiver preenchido
                    errorProvider.SetError(campo, null);
                }
            }


            return true;  // Retorna true se todos os campos estiverem preenchidos
        }

        // Uso:
        public FrmCadastroUsuario()
        {
            InitializeComponent();
            
         
           TextDataCadastro.Height = 100;


            // Preenche a listaDeItens com dados do banco de dados ou de outra fonte

            List<TipoUsuarioModel> listaDeTipoUsuario = validaForm.PopularCBO();

            // Configura o ComboBox
            CboTpUsuario.DisplayMember = "DescricaoTpUsuario";
            CboTpUsuario.ValueMember = "IdTpUsuario";
            CboTpUsuario.DataSource = listaDeTipoUsuario;

            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            // Configurar a máscara para o CPF
       
            TextCPF.Mask = "000.000.000-00";
            TextCPF.PromptChar = ' ';
        }

        private void TextDataCadastro_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            FrmMenuInicial frmMenuInicial = new FrmMenuInicial();
            Close();
        }

        private void FrmCadastroUsuario_Load(object sender, EventArgs e)
        {

        }

        private void ChkVisualizaSenha_CheckedChanged(object sender, EventArgs e)
        {

            if (ChkVisualizaSenha.Checked)
            {
                // Mostra a senha
                TextSenha.PasswordChar = '\0';
            }
            else
            {
                // Oculta a senha
                TextSenha.PasswordChar = '*'; 
            }
        }

        private void BtnCadastrar_Click(object sender, EventArgs e)
        {
  
            if (TextSenha.Text != TextConfirmaSenha.Text)
            {
                MessageBox.Show("Senha de confirmação diferente!!");
                return;
            }

            if (!ValidarCampos())
            {
                // Não continue a operação se houver campos não preenchidos
                return;
            }


            lblMenuText.Text = "CADASTRAR USUARIO";

            ObjUsuario.NomeUsuario = TextNome.Text.ToString();
            ObjUsuario.EmailUsuario = TextEmail.Text.ToString();
            ObjUsuario.SenhaUsuario = TextSenha.Text.ToString();
            ObjUsuario.CPFUsuario = TextCPF.Text.ToString();
            ObjUsuario.CargoUsuario = TextCargo.Text.ToString();
            ObjUsuario.DatCadastroUsuario = DateTime.Now.ToString();
            ObjUsuario.StatusUsuario = "ATIVO";
            ObjUsuario.TpUsuario = CboTpUsuario.SelectedValue.ToString();

            validaForm.CadastrarUsuario(ObjUsuario);

            MessageBox.Show("Usuario Cadastrado com sucesso!!");
            LimpaTxt();

        }

        private void CboTpUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            BtnCadastrar.Enabled = false;
           
            TextIdUser.Enabled = true;

            if (String.IsNullOrEmpty(TextIdUser.Text))
            {

                // Exibir mensagem de erro usando o ErrorProvider
                errorProvider.SetError(TextIdUser, "Campo obrigatório!");
                TextIdUser.Focus();
                return;
            }
            else
            {
                // Limpar mensagem de erro se o campo estiver preenchido
                errorProvider.SetError(TextIdUser, null);

                // Continuar com a lógica de salvamento ou outra ação necessária
            } 

            lblMenuText.Text = "CONSULTAR USUARIO";
            string IdUsuario = TextIdUser.Text.ToString();
           Usuario BuscaUserId = validaForm.BuscaPorId(Convert.ToInt32(IdUsuario));
            if (BuscaUserId != null)
            {
                TextIdUser.Text = BuscaUserId.IdUsuario.ToString();
                TextNome.Text = BuscaUserId.NomeUsuario.ToString();
                TextEmail.Text = BuscaUserId.EmailUsuario.ToString();
                TextSenha.Text = BuscaUserId.SenhaUsuario.ToString();
                TextCPF.Text = BuscaUserId.CPFUsuario.ToString();
                TextCargo.Text = BuscaUserId.CargoUsuario.ToString();
                TextDataCadastro.Text = BuscaUserId.DatCadastroUsuario.ToString();
                TextStatus.Text= BuscaUserId.StatusUsuario.ToString();
                CboTpUsuario.DisplayMember = BuscaUserId.TpUsuario;
            }

            else
            {
                MessageBox.Show($"Id Usuario {IdUsuario} não encontrado!!  ");
            }

       

        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {

            BtnCadastrar.Enabled = false;

            TextIdUser.Enabled = false;

            if (String.IsNullOrEmpty(TextIdUser.Text))
            {

                // Exibir mensagem de erro usando o ErrorProvider
                errorProvider.SetError(TextIdUser, "Campo obrigatório!");
                TextIdUser.Focus();
                MessageBox.Show("Faça um consulta!!");
                return;
            }
            else
            {
                // Limpar mensagem de erro se o campo estiver preenchido
                errorProvider.SetError(TextIdUser, null);

                // Continuar com a lógica de salvamento ou outra ação necessária
            }
            lblMenuText.Text = "EXCLUIR USUARIO";
            DialogResult resultado = MessageBox.Show($"Deseja Realmente Excluir o Usuario ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                int IdUsuario = Convert.ToInt32(TextIdUser.Text);
                validaForm.ExcluirUsuario(IdUsuario);
                MessageBox.Show("Usuario Excluido com Sucesso!!");
                LimpaTxt();
            }
       


        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                // Não continue a operação se houver campos não preenchidos
                return;
            }

            lblMenuText.Text = "ALTERAR USUARIO";
            DialogResult resultado = MessageBox.Show($"Deseja Realmente Alterar o Usuario ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                int id = Convert.ToInt32(TextIdUser.Text);
                ObjUsuario.IdUsuario = id;
                ObjUsuario.NomeUsuario = TextNome.Text.ToString();
                ObjUsuario.EmailUsuario = TextEmail.Text.ToString();
                ObjUsuario.SenhaUsuario = TextSenha.Text.ToString();
                ObjUsuario.CPFUsuario = TextCPF.Text.ToString();
                ObjUsuario.CargoUsuario = TextCargo.Text.ToString();
                ObjUsuario.StatusUsuario = TextStatus.Text.ToString();
                ObjUsuario.TpUsuario = CboTpUsuario.SelectedValue.ToString();
                validaForm.EditarUsuario(ObjUsuario);
                MessageBox.Show("Usuario Alterado com Sucesso!!");
                LimpaTxt();
            }
            else
            {
                // Código a ser executado se o usuário escolher "Não" ou fechar a caixa de diálogo
            }
       

        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            LimpaTxt();
            LimparErros();
            TextIdUser.Enabled = true;
        }

        private void TextCPF_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
