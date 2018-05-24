namespace ICT4Rails
{
    partial class frmLogin
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
            this.tbGebruikersnaam = new System.Windows.Forms.TextBox();
            this.lbGebruikersnaam = new System.Windows.Forms.Label();
            this.gbLogin = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbWachtwoord = new System.Windows.Forms.TextBox();
            this.lbWachtwoord = new System.Windows.Forms.Label();
            this.btnAnnuleer = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.gbLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbGebruikersnaam
            // 
            this.tbGebruikersnaam.Location = new System.Drawing.Point(163, 125);
            this.tbGebruikersnaam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbGebruikersnaam.Name = "tbGebruikersnaam";
            this.tbGebruikersnaam.Size = new System.Drawing.Size(76, 20);
            this.tbGebruikersnaam.TabIndex = 0;
            // 
            // lbGebruikersnaam
            // 
            this.lbGebruikersnaam.AutoSize = true;
            this.lbGebruikersnaam.Location = new System.Drawing.Point(64, 128);
            this.lbGebruikersnaam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbGebruikersnaam.Name = "lbGebruikersnaam";
            this.lbGebruikersnaam.Size = new System.Drawing.Size(84, 13);
            this.lbGebruikersnaam.TabIndex = 1;
            this.lbGebruikersnaam.Text = "Gebruikersnaam";
            // 
            // gbLogin
            // 
            this.gbLogin.Controls.Add(this.label2);
            this.gbLogin.Controls.Add(this.tbWachtwoord);
            this.gbLogin.Controls.Add(this.lbWachtwoord);
            this.gbLogin.Controls.Add(this.tbGebruikersnaam);
            this.gbLogin.Controls.Add(this.lbGebruikersnaam);
            this.gbLogin.Location = new System.Drawing.Point(29, 36);
            this.gbLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbLogin.Name = "gbLogin";
            this.gbLogin.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbLogin.Size = new System.Drawing.Size(301, 235);
            this.gbLogin.TabIndex = 2;
            this.gbLogin.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(84, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 37);
            this.label2.TabIndex = 4;
            this.label2.Text = "Welkom";
            // 
            // tbWachtwoord
            // 
            this.tbWachtwoord.Location = new System.Drawing.Point(163, 160);
            this.tbWachtwoord.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbWachtwoord.Name = "tbWachtwoord";
            this.tbWachtwoord.Size = new System.Drawing.Size(76, 20);
            this.tbWachtwoord.TabIndex = 2;
            this.tbWachtwoord.UseSystemPasswordChar = true;
            // 
            // lbWachtwoord
            // 
            this.lbWachtwoord.AutoSize = true;
            this.lbWachtwoord.Location = new System.Drawing.Point(64, 162);
            this.lbWachtwoord.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbWachtwoord.Name = "lbWachtwoord";
            this.lbWachtwoord.Size = new System.Drawing.Size(68, 13);
            this.lbWachtwoord.TabIndex = 3;
            this.lbWachtwoord.Text = "Wachtwoord";
            // 
            // btnAnnuleer
            // 
            this.btnAnnuleer.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuleer.Location = new System.Drawing.Point(195, 288);
            this.btnAnnuleer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAnnuleer.Name = "btnAnnuleer";
            this.btnAnnuleer.Size = new System.Drawing.Size(65, 31);
            this.btnAnnuleer.TabIndex = 4;
            this.btnAnnuleer.Text = "Annuleer";
            this.btnAnnuleer.UseVisualStyleBackColor = true;
            this.btnAnnuleer.Click += new System.EventHandler(this.btnAnnuleer_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(265, 288);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 31);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnAnnuleer;
            this.ClientSize = new System.Drawing.Size(363, 339);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnAnnuleer);
            this.Controls.Add(this.gbLogin);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.gbLogin.ResumeLayout(false);
            this.gbLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbGebruikersnaam;
        private System.Windows.Forms.Label lbGebruikersnaam;
        private System.Windows.Forms.GroupBox gbLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbWachtwoord;
        private System.Windows.Forms.Label lbWachtwoord;
        private System.Windows.Forms.Button btnAnnuleer;
        private System.Windows.Forms.Button btnOk;
    }
}