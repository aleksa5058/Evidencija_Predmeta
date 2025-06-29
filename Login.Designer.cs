
namespace Evidencija_Predmeta
{
    partial class Login
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.textBoxKIme = new System.Windows.Forms.TextBox();
            this.textBoxLozinka = new System.Windows.Forms.TextBox();
            this.labelKIme = new System.Windows.Forms.Label();
            this.labelLozinka = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogin.Location = new System.Drawing.Point(122, 346);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Uloguj se";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // textBoxKIme
            // 
            this.textBoxKIme.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxKIme.Location = new System.Drawing.Point(143, 234);
            this.textBoxKIme.Name = "textBoxKIme";
            this.textBoxKIme.Size = new System.Drawing.Size(159, 20);
            this.textBoxKIme.TabIndex = 1;
            // 
            // textBoxLozinka
            // 
            this.textBoxLozinka.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxLozinka.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxLozinka.Location = new System.Drawing.Point(143, 295);
            this.textBoxLozinka.Name = "textBoxLozinka";
            this.textBoxLozinka.Size = new System.Drawing.Size(159, 20);
            this.textBoxLozinka.TabIndex = 2;
            this.textBoxLozinka.UseSystemPasswordChar = true;
            // 
            // labelKIme
            // 
            this.labelKIme.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelKIme.AutoSize = true;
            this.labelKIme.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKIme.Location = new System.Drawing.Point(13, 234);
            this.labelKIme.Name = "labelKIme";
            this.labelKIme.Size = new System.Drawing.Size(124, 20);
            this.labelKIme.TabIndex = 3;
            this.labelKIme.Text = "Korisničko ime";
            // 
            // labelLozinka
            // 
            this.labelLozinka.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLozinka.AutoSize = true;
            this.labelLozinka.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLozinka.Location = new System.Drawing.Point(66, 293);
            this.labelLozinka.Name = "labelLozinka";
            this.labelLozinka.Size = new System.Drawing.Size(71, 20);
            this.labelLozinka.TabIndex = 4;
            this.labelLozinka.Text = "Lozinka";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 450);
            this.Controls.Add(this.labelLozinka);
            this.Controls.Add(this.labelKIme);
            this.Controls.Add(this.textBoxLozinka);
            this.Controls.Add(this.textBoxKIme);
            this.Controls.Add(this.btnLogin);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox textBoxKIme;
        private System.Windows.Forms.TextBox textBoxLozinka;
        private System.Windows.Forms.Label labelKIme;
        private System.Windows.Forms.Label labelLozinka;
    }
}