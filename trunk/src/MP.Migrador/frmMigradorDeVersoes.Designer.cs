namespace MP.Migrador
{
    partial class frmMigradorDeVersoes
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
            this.btnConverter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStringConexao = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnConverter
            // 
            this.btnConverter.Location = new System.Drawing.Point(12, 90);
            this.btnConverter.Name = "btnConverter";
            this.btnConverter.Size = new System.Drawing.Size(75, 23);
            this.btnConverter.TabIndex = 0;
            this.btnConverter.Text = "Converter";
            this.btnConverter.UseVisualStyleBackColor = true;
            this.btnConverter.Click += new System.EventHandler(this.btnConverter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "String de Conexão";
            // 
            // txtStringConexao
            // 
            this.txtStringConexao.Location = new System.Drawing.Point(16, 42);
            this.txtStringConexao.Name = "txtStringConexao";
            this.txtStringConexao.Size = new System.Drawing.Size(350, 20);
            this.txtStringConexao.TabIndex = 2;
            // 
            // frmMigradorDeVersoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 125);
            this.Controls.Add(this.txtStringConexao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConverter);
            this.Name = "frmMigradorDeVersoes";
            this.Text = "Migrador de Versões";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConverter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStringConexao;
    }
}