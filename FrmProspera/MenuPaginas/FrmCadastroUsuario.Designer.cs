namespace FrmProspera
{
    partial class FrmCadastroUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMenuText = new System.Windows.Forms.Label();
            this.TextNome = new System.Windows.Forms.TextBox();
            this.TextEmail = new System.Windows.Forms.TextBox();
            this.TextSenha = new System.Windows.Forms.TextBox();
            this.TextCargo = new System.Windows.Forms.TextBox();
            this.TextStatus = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.lblCPF = new System.Windows.Forms.Label();
            this.lblCargo = new System.Windows.Forms.Label();
            this.lblDatCadastro = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTpUsuario = new System.Windows.Forms.Label();
            this.TextDataCadastro = new System.Windows.Forms.MaskedTextBox();
            this.BtnVoltar = new System.Windows.Forms.Button();
            this.TextConfirmaSenha = new System.Windows.Forms.TextBox();
            this.lblRepitaSenha = new System.Windows.Forms.Label();
            this.ChkVisualizaSenha = new System.Windows.Forms.CheckBox();
            this.BtnCadastrar = new System.Windows.Forms.Button();
            this.CboTpUsuario = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnConsultar = new System.Windows.Forms.Button();
            this.BtnExcluir = new System.Windows.Forms.Button();
            this.BtnAlterar = new System.Windows.Forms.Button();
            this.TextIdUser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnLimpar = new System.Windows.Forms.Button();
            this.TextCPF = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMenuText
            // 
            this.lblMenuText.AutoSize = true;
            this.lblMenuText.Font = new System.Drawing.Font("Archive", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuText.Location = new System.Drawing.Point(206, 72);
            this.lblMenuText.Name = "lblMenuText";
            this.lblMenuText.Size = new System.Drawing.Size(186, 18);
            this.lblMenuText.TabIndex = 1;
            this.lblMenuText.Text = "CADASTRO DE USUARIO";
            // 
            // TextNome
            // 
            this.TextNome.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.TextNome.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextNome.Font = new System.Drawing.Font("Inter", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextNome.ForeColor = System.Drawing.Color.White;
            this.TextNome.Location = new System.Drawing.Point(57, 167);
            this.TextNome.Multiline = true;
            this.TextNome.Name = "TextNome";
            this.TextNome.Size = new System.Drawing.Size(359, 34);
            this.TextNome.TabIndex = 0;
            // 
            // TextEmail
            // 
            this.TextEmail.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.TextEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextEmail.Font = new System.Drawing.Font("Inter", 14.25F);
            this.TextEmail.ForeColor = System.Drawing.Color.White;
            this.TextEmail.Location = new System.Drawing.Point(57, 234);
            this.TextEmail.Multiline = true;
            this.TextEmail.Name = "TextEmail";
            this.TextEmail.Size = new System.Drawing.Size(235, 34);
            this.TextEmail.TabIndex = 0;
            // 
            // TextSenha
            // 
            this.TextSenha.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.TextSenha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextSenha.Font = new System.Drawing.Font("Inter", 14.25F);
            this.TextSenha.ForeColor = System.Drawing.Color.White;
            this.TextSenha.Location = new System.Drawing.Point(57, 296);
            this.TextSenha.Multiline = true;
            this.TextSenha.Name = "TextSenha";
            this.TextSenha.PasswordChar = '*';
            this.TextSenha.Size = new System.Drawing.Size(301, 34);
            this.TextSenha.TabIndex = 0;
            // 
            // TextCargo
            // 
            this.TextCargo.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.TextCargo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextCargo.Font = new System.Drawing.Font("Inter", 14.25F);
            this.TextCargo.ForeColor = System.Drawing.Color.White;
            this.TextCargo.Location = new System.Drawing.Point(58, 490);
            this.TextCargo.Multiline = true;
            this.TextCargo.Name = "TextCargo";
            this.TextCargo.Size = new System.Drawing.Size(185, 34);
            this.TextCargo.TabIndex = 0;
            // 
            // TextStatus
            // 
            this.TextStatus.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.TextStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextStatus.Font = new System.Drawing.Font("Inter", 14.25F);
            this.TextStatus.ForeColor = System.Drawing.Color.White;
            this.TextStatus.Location = new System.Drawing.Point(57, 615);
            this.TextStatus.Multiline = true;
            this.TextStatus.Name = "TextStatus";
            this.TextStatus.Size = new System.Drawing.Size(235, 34);
            this.TextStatus.TabIndex = 0;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Archive", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.Location = new System.Drawing.Point(54, 151);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(42, 14);
            this.lblNome.TabIndex = 1;
            this.lblNome.Text = "NOME";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Archive", 9.75F);
            this.lblEmail.Location = new System.Drawing.Point(54, 218);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(48, 14);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "EMAIL";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Archive", 9.75F);
            this.lbl.Location = new System.Drawing.Point(54, 280);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(50, 14);
            this.lbl.TabIndex = 1;
            this.lbl.Text = "SENHA";
            // 
            // lblCPF
            // 
            this.lblCPF.AutoSize = true;
            this.lblCPF.Font = new System.Drawing.Font("Archive", 9.75F);
            this.lblCPF.Location = new System.Drawing.Point(55, 410);
            this.lblCPF.Name = "lblCPF";
            this.lblCPF.Size = new System.Drawing.Size(31, 14);
            this.lblCPF.TabIndex = 1;
            this.lblCPF.Text = "CPF";
            // 
            // lblCargo
            // 
            this.lblCargo.AutoSize = true;
            this.lblCargo.Font = new System.Drawing.Font("Archive", 9.75F);
            this.lblCargo.Location = new System.Drawing.Point(54, 474);
            this.lblCargo.Name = "lblCargo";
            this.lblCargo.Size = new System.Drawing.Size(49, 14);
            this.lblCargo.TabIndex = 1;
            this.lblCargo.Text = "CARGO";
            // 
            // lblDatCadastro
            // 
            this.lblDatCadastro.AutoSize = true;
            this.lblDatCadastro.Font = new System.Drawing.Font("Archive", 9.75F);
            this.lblDatCadastro.Location = new System.Drawing.Point(55, 544);
            this.lblDatCadastro.Name = "lblDatCadastro";
            this.lblDatCadastro.Size = new System.Drawing.Size(130, 14);
            this.lblDatCadastro.TabIndex = 1;
            this.lblDatCadastro.Text = "DATA DE CADASTRO";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Archive", 9.75F);
            this.lblStatus.Location = new System.Drawing.Point(55, 599);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(137, 14);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "STATUS DO USUARIO";
            // 
            // lblTpUsuario
            // 
            this.lblTpUsuario.AutoSize = true;
            this.lblTpUsuario.Font = new System.Drawing.Font("Archive", 9.75F);
            this.lblTpUsuario.Location = new System.Drawing.Point(55, 663);
            this.lblTpUsuario.Name = "lblTpUsuario";
            this.lblTpUsuario.Size = new System.Drawing.Size(118, 14);
            this.lblTpUsuario.TabIndex = 1;
            this.lblTpUsuario.Text = "TIPO DO USUARIO";
            // 
            // TextDataCadastro
            // 
            this.TextDataCadastro.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.TextDataCadastro.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextDataCadastro.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextDataCadastro.ForeColor = System.Drawing.SystemColors.Window;
            this.TextDataCadastro.Location = new System.Drawing.Point(58, 561);
            this.TextDataCadastro.Mask = "00/00/0000";
            this.TextDataCadastro.Name = "TextDataCadastro";
            this.TextDataCadastro.Size = new System.Drawing.Size(118, 24);
            this.TextDataCadastro.TabIndex = 2;
            this.TextDataCadastro.ValidatingType = typeof(System.DateTime);
            this.TextDataCadastro.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.TextDataCadastro_MaskInputRejected);
            // 
            // BtnVoltar
            // 
            this.BtnVoltar.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnVoltar.Font = new System.Drawing.Font("Archive", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVoltar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnVoltar.Location = new System.Drawing.Point(427, 24);
            this.BtnVoltar.Name = "BtnVoltar";
            this.BtnVoltar.Size = new System.Drawing.Size(111, 36);
            this.BtnVoltar.TabIndex = 3;
            this.BtnVoltar.Text = "VOLTAR";
            this.BtnVoltar.UseVisualStyleBackColor = false;
            this.BtnVoltar.Click += new System.EventHandler(this.BtnVoltar_Click);
            // 
            // TextConfirmaSenha
            // 
            this.TextConfirmaSenha.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.TextConfirmaSenha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextConfirmaSenha.Font = new System.Drawing.Font("Inter", 14.25F);
            this.TextConfirmaSenha.ForeColor = System.Drawing.Color.White;
            this.TextConfirmaSenha.Location = new System.Drawing.Point(58, 361);
            this.TextConfirmaSenha.Multiline = true;
            this.TextConfirmaSenha.Name = "TextConfirmaSenha";
            this.TextConfirmaSenha.PasswordChar = '*';
            this.TextConfirmaSenha.Size = new System.Drawing.Size(301, 34);
            this.TextConfirmaSenha.TabIndex = 0;
            // 
            // lblRepitaSenha
            // 
            this.lblRepitaSenha.AutoSize = true;
            this.lblRepitaSenha.Font = new System.Drawing.Font("Archive", 9.75F);
            this.lblRepitaSenha.Location = new System.Drawing.Point(55, 344);
            this.lblRepitaSenha.Name = "lblRepitaSenha";
            this.lblRepitaSenha.Size = new System.Drawing.Size(133, 14);
            this.lblRepitaSenha.TabIndex = 1;
            this.lblRepitaSenha.Text = "CONFIRME A SENHA";
            // 
            // ChkVisualizaSenha
            // 
            this.ChkVisualizaSenha.AutoSize = true;
            this.ChkVisualizaSenha.Font = new System.Drawing.Font("Archive", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkVisualizaSenha.Location = new System.Drawing.Point(373, 301);
            this.ChkVisualizaSenha.Name = "ChkVisualizaSenha";
            this.ChkVisualizaSenha.Size = new System.Drawing.Size(132, 18);
            this.ChkVisualizaSenha.TabIndex = 5;
            this.ChkVisualizaSenha.Text = "Exibir a Senha";
            this.ChkVisualizaSenha.UseVisualStyleBackColor = true;
            this.ChkVisualizaSenha.CheckedChanged += new System.EventHandler(this.ChkVisualizaSenha_CheckedChanged);
            // 
            // BtnCadastrar
            // 
            this.BtnCadastrar.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnCadastrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCadastrar.Font = new System.Drawing.Font("Archive", 9.75F);
            this.BtnCadastrar.ForeColor = System.Drawing.Color.White;
            this.BtnCadastrar.Location = new System.Drawing.Point(409, 767);
            this.BtnCadastrar.Name = "BtnCadastrar";
            this.BtnCadastrar.Size = new System.Drawing.Size(108, 36);
            this.BtnCadastrar.TabIndex = 6;
            this.BtnCadastrar.Text = "CADASTRAR";
            this.BtnCadastrar.UseVisualStyleBackColor = false;
            this.BtnCadastrar.Click += new System.EventHandler(this.BtnCadastrar_Click);
            // 
            // CboTpUsuario
            // 
            this.CboTpUsuario.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.CboTpUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CboTpUsuario.Font = new System.Drawing.Font("Inter", 15.25F);
            this.CboTpUsuario.ForeColor = System.Drawing.Color.White;
            this.CboTpUsuario.FormattingEnabled = true;
            this.CboTpUsuario.Location = new System.Drawing.Point(58, 690);
            this.CboTpUsuario.Name = "CboTpUsuario";
            this.CboTpUsuario.Size = new System.Drawing.Size(254, 32);
            this.CboTpUsuario.TabIndex = 7;
            this.CboTpUsuario.SelectedIndexChanged += new System.EventHandler(this.CboTpUsuario_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(-15, -15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // BtnConsultar
            // 
            this.BtnConsultar.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnConsultar.Font = new System.Drawing.Font("Archive", 9.75F);
            this.BtnConsultar.ForeColor = System.Drawing.Color.White;
            this.BtnConsultar.Location = new System.Drawing.Point(287, 767);
            this.BtnConsultar.Name = "BtnConsultar";
            this.BtnConsultar.Size = new System.Drawing.Size(105, 36);
            this.BtnConsultar.TabIndex = 9;
            this.BtnConsultar.Text = "CONSULTAR";
            this.BtnConsultar.UseVisualStyleBackColor = false;
            this.BtnConsultar.Click += new System.EventHandler(this.BtnConsultar_Click);
            // 
            // BtnExcluir
            // 
            this.BtnExcluir.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExcluir.Font = new System.Drawing.Font("Archive", 9.75F);
            this.BtnExcluir.ForeColor = System.Drawing.Color.White;
            this.BtnExcluir.Location = new System.Drawing.Point(165, 767);
            this.BtnExcluir.Name = "BtnExcluir";
            this.BtnExcluir.Size = new System.Drawing.Size(103, 36);
            this.BtnExcluir.TabIndex = 10;
            this.BtnExcluir.Text = "EXCLUR";
            this.BtnExcluir.UseVisualStyleBackColor = false;
            this.BtnExcluir.Click += new System.EventHandler(this.BtnExcluir_Click);
            // 
            // BtnAlterar
            // 
            this.BtnAlterar.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnAlterar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAlterar.Font = new System.Drawing.Font("Archive", 9.75F);
            this.BtnAlterar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.BtnAlterar.Location = new System.Drawing.Point(38, 767);
            this.BtnAlterar.Name = "BtnAlterar";
            this.BtnAlterar.Size = new System.Drawing.Size(102, 36);
            this.BtnAlterar.TabIndex = 12;
            this.BtnAlterar.Text = "ALTERAR";
            this.BtnAlterar.UseVisualStyleBackColor = false;
            this.BtnAlterar.Click += new System.EventHandler(this.BtnAlterar_Click);
            // 
            // TextIdUser
            // 
            this.TextIdUser.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.TextIdUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextIdUser.Font = new System.Drawing.Font("Inter", 14.25F);
            this.TextIdUser.ForeColor = System.Drawing.Color.White;
            this.TextIdUser.Location = new System.Drawing.Point(441, 167);
            this.TextIdUser.Multiline = true;
            this.TextIdUser.Name = "TextIdUser";
            this.TextIdUser.Size = new System.Drawing.Size(54, 34);
            this.TextIdUser.TabIndex = 13;
            this.TextIdUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Archive", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(391, 302);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "NOME";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Archive", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(455, 150);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(21, 14);
            this.lblID.TabIndex = 1;
            this.lblID.Text = "ID";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FrmProspera.Properties.Resources.IconCadastroFrm;
            this.pictureBox1.Location = new System.Drawing.Point(57, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(99, 114);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // BtnLimpar
            // 
            this.BtnLimpar.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLimpar.Font = new System.Drawing.Font("Archive", 9.75F);
            this.BtnLimpar.ForeColor = System.Drawing.Color.White;
            this.BtnLimpar.Location = new System.Drawing.Point(409, 702);
            this.BtnLimpar.Name = "BtnLimpar";
            this.BtnLimpar.Size = new System.Drawing.Size(108, 36);
            this.BtnLimpar.TabIndex = 14;
            this.BtnLimpar.Text = "LIMPAR";
            this.BtnLimpar.UseVisualStyleBackColor = false;
            this.BtnLimpar.Click += new System.EventHandler(this.BtnLimpar_Click);
            // 
            // TextCPF
            // 
            this.TextCPF.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.TextCPF.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextCPF.Font = new System.Drawing.Font("Inter", 19.25F);
            this.TextCPF.Location = new System.Drawing.Point(58, 427);
            this.TextCPF.Name = "TextCPF";
            this.TextCPF.Size = new System.Drawing.Size(212, 32);
            this.TextCPF.TabIndex = 15;
            this.TextCPF.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.TextCPF_MaskInputRejected);
            // 
            // FrmCadastroUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(550, 835);
            this.Controls.Add(this.TextCPF);
            this.Controls.Add(this.BtnLimpar);
            this.Controls.Add(this.TextIdUser);
            this.Controls.Add(this.BtnAlterar);
            this.Controls.Add(this.BtnExcluir);
            this.Controls.Add(this.BtnConsultar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CboTpUsuario);
            this.Controls.Add(this.BtnCadastrar);
            this.Controls.Add(this.ChkVisualizaSenha);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BtnVoltar);
            this.Controls.Add(this.TextDataCadastro);
            this.Controls.Add(this.lblTpUsuario);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDatCadastro);
            this.Controls.Add(this.lblCargo);
            this.Controls.Add(this.lblCPF);
            this.Controls.Add(this.lblRepitaSenha);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.lblMenuText);
            this.Controls.Add(this.TextStatus);
            this.Controls.Add(this.TextCargo);
            this.Controls.Add(this.TextConfirmaSenha);
            this.Controls.Add(this.TextSenha);
            this.Controls.Add(this.TextEmail);
            this.Controls.Add(this.TextNome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCadastroUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCadastroUsuario";
            this.Load += new System.EventHandler(this.FrmCadastroUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblMenuText;
        private System.Windows.Forms.TextBox TextNome;
        private System.Windows.Forms.TextBox TextEmail;
        private System.Windows.Forms.TextBox TextSenha;
        private System.Windows.Forms.TextBox TextCargo;
        private System.Windows.Forms.TextBox TextStatus;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblCPF;
        private System.Windows.Forms.Label lblCargo;
        private System.Windows.Forms.Label lblDatCadastro;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTpUsuario;
        private System.Windows.Forms.MaskedTextBox TextDataCadastro;
        private System.Windows.Forms.Button BtnVoltar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox TextConfirmaSenha;
        private System.Windows.Forms.Label lblRepitaSenha;
        private System.Windows.Forms.CheckBox ChkVisualizaSenha;
        private System.Windows.Forms.Button BtnCadastrar;
        private System.Windows.Forms.ComboBox CboTpUsuario;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnConsultar;
        private System.Windows.Forms.Button BtnExcluir;
        private System.Windows.Forms.Button BtnAlterar;
        private System.Windows.Forms.TextBox TextIdUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button BtnLimpar;
        private System.Windows.Forms.MaskedTextBox TextCPF;
    }
}