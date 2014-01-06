namespace MP.Migrador
{
    partial class frmMigrador
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
            this.btnMigrar = new System.Windows.Forms.Button();
            this.txtStringDeConexaoSolureg = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStrConexaoSiscopat = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnMigrar
            // 
            this.btnMigrar.Location = new System.Drawing.Point(377, 36);
            this.btnMigrar.Name = "btnMigrar";
            this.btnMigrar.Size = new System.Drawing.Size(75, 23);
            this.btnMigrar.TabIndex = 0;
            this.btnMigrar.Text = "Migrar";
            this.btnMigrar.UseVisualStyleBackColor = true;
            this.btnMigrar.Click += new System.EventHandler(this.btnMigrar_Click);
            // 
            // txtStringDeConexaoSolureg
            // 
            this.txtStringDeConexaoSolureg.Location = new System.Drawing.Point(12, 39);
            this.txtStringDeConexaoSolureg.Name = "txtStringDeConexaoSolureg";
            this.txtStringDeConexaoSolureg.Size = new System.Drawing.Size(323, 20);
            this.txtStringDeConexaoSolureg.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "String de conexão Solureg";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "String de conexão Siscopat";
            // 
            // txtStrConexaoSiscopat
            // 
            this.txtStrConexaoSiscopat.Location = new System.Drawing.Point(12, 78);
            this.txtStrConexaoSiscopat.Name = "txtStrConexaoSiscopat";
            this.txtStrConexaoSiscopat.Size = new System.Drawing.Size(323, 20);
            this.txtStrConexaoSiscopat.TabIndex = 3;
            // 
            // frmMigrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 215);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStrConexaoSiscopat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStringDeConexaoSolureg);
            this.Controls.Add(this.btnMigrar);
            this.Name = "frmMigrador";
            this.Text = "frmMigrador";
            this.Load += new System.EventHandler(this.frmMigrador_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMigrar;
        private System.Windows.Forms.TextBox txtStringDeConexaoSolureg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStrConexaoSiscopat;
    }
}