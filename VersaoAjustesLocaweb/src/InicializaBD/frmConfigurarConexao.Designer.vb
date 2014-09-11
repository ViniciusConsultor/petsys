<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigurarConexao
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbMySQL = New System.Windows.Forms.RadioButton
        Me.rbSQLServer = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkUtilizaUpercase = New System.Windows.Forms.CheckBox
        Me.grbSQLServer = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtBancoDeDados = New System.Windows.Forms.TextBox
        Me.btnTestar = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPorta = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtSenha = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtUsuario = New System.Windows.Forms.TextBox
        Me.txtServidor = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnSalvar = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grbSQLServer.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbMySQL)
        Me.GroupBox1.Controls.Add(Me.rbSQLServer)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(351, 86)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Provider"
        '
        'rbMySQL
        '
        Me.rbMySQL.AutoSize = True
        Me.rbMySQL.Location = New System.Drawing.Point(6, 42)
        Me.rbMySQL.Name = "rbMySQL"
        Me.rbMySQL.Size = New System.Drawing.Size(60, 17)
        Me.rbMySQL.TabIndex = 5
        Me.rbMySQL.TabStop = True
        Me.rbMySQL.Text = "MySQL"
        Me.rbMySQL.UseVisualStyleBackColor = True
        '
        'rbSQLServer
        '
        Me.rbSQLServer.AutoSize = True
        Me.rbSQLServer.Location = New System.Drawing.Point(6, 19)
        Me.rbSQLServer.Name = "rbSQLServer"
        Me.rbSQLServer.Size = New System.Drawing.Size(126, 17)
        Me.rbSQLServer.TabIndex = 2
        Me.rbSQLServer.TabStop = True
        Me.rbSQLServer.Text = "Microsoft SQL Server"
        Me.rbSQLServer.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkUtilizaUpercase)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 356)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(351, 56)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Opções gerais"
        '
        'chkUtilizaUpercase
        '
        Me.chkUtilizaUpercase.AutoSize = True
        Me.chkUtilizaUpercase.Location = New System.Drawing.Point(6, 19)
        Me.chkUtilizaUpercase.Name = "chkUtilizaUpercase"
        Me.chkUtilizaUpercase.Size = New System.Drawing.Size(138, 17)
        Me.chkUtilizaUpercase.TabIndex = 1
        Me.chkUtilizaUpercase.Text = "Sistema utiliza UPCASE"
        Me.chkUtilizaUpercase.UseVisualStyleBackColor = True
        '
        'grbSQLServer
        '
        Me.grbSQLServer.Controls.Add(Me.Label5)
        Me.grbSQLServer.Controls.Add(Me.txtBancoDeDados)
        Me.grbSQLServer.Controls.Add(Me.btnTestar)
        Me.grbSQLServer.Controls.Add(Me.Label4)
        Me.grbSQLServer.Controls.Add(Me.txtPorta)
        Me.grbSQLServer.Controls.Add(Me.Label3)
        Me.grbSQLServer.Controls.Add(Me.txtSenha)
        Me.grbSQLServer.Controls.Add(Me.Label2)
        Me.grbSQLServer.Controls.Add(Me.txtUsuario)
        Me.grbSQLServer.Controls.Add(Me.txtServidor)
        Me.grbSQLServer.Controls.Add(Me.Label1)
        Me.grbSQLServer.Location = New System.Drawing.Point(12, 104)
        Me.grbSQLServer.Name = "grbSQLServer"
        Me.grbSQLServer.Size = New System.Drawing.Size(351, 224)
        Me.grbSQLServer.TabIndex = 4
        Me.grbSQLServer.TabStop = False
        Me.grbSQLServer.Text = "Conexão"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Banco de dados"
        '
        'txtBancoDeDados
        '
        Me.txtBancoDeDados.Location = New System.Drawing.Point(6, 75)
        Me.txtBancoDeDados.Name = "txtBancoDeDados"
        Me.txtBancoDeDados.Size = New System.Drawing.Size(212, 20)
        Me.txtBancoDeDados.TabIndex = 8
        '
        'btnTestar
        '
        Me.btnTestar.Location = New System.Drawing.Point(270, 193)
        Me.btnTestar.Name = "btnTestar"
        Me.btnTestar.Size = New System.Drawing.Size(75, 23)
        Me.btnTestar.TabIndex = 5
        Me.btnTestar.Text = "Testar"
        Me.btnTestar.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 177)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Porta"
        '
        'txtPorta
        '
        Me.txtPorta.Location = New System.Drawing.Point(6, 193)
        Me.txtPorta.Name = "txtPorta"
        Me.txtPorta.Size = New System.Drawing.Size(60, 20)
        Me.txtPorta.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 138)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Senha"
        '
        'txtSenha
        '
        Me.txtSenha.Location = New System.Drawing.Point(6, 154)
        Me.txtSenha.Name = "txtSenha"
        Me.txtSenha.Size = New System.Drawing.Size(138, 20)
        Me.txtSenha.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Usuário"
        '
        'txtUsuario
        '
        Me.txtUsuario.Location = New System.Drawing.Point(6, 114)
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(212, 20)
        Me.txtUsuario.TabIndex = 2
        '
        'txtServidor
        '
        Me.txtServidor.Location = New System.Drawing.Point(7, 36)
        Me.txtServidor.Name = "txtServidor"
        Me.txtServidor.Size = New System.Drawing.Size(297, 20)
        Me.txtServidor.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Servidor"
        '
        'btnSalvar
        '
        Me.btnSalvar.Location = New System.Drawing.Point(370, 12)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(75, 23)
        Me.btnSalvar.TabIndex = 5
        Me.btnSalvar.Text = "Salvar"
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'frmConfigurarConexao
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 461)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.grbSQLServer)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "frmConfigurarConexao"
        Me.Text = "frmConfigurarConexao"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grbSQLServer.ResumeLayout(False)
        Me.grbSQLServer.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbSQLServer As System.Windows.Forms.RadioButton
    Friend WithEvents rbMySQL As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkUtilizaUpercase As System.Windows.Forms.CheckBox
    Friend WithEvents grbSQLServer As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPorta As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSenha As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUsuario As System.Windows.Forms.TextBox
    Friend WithEvents txtServidor As System.Windows.Forms.TextBox
    Friend WithEvents btnTestar As System.Windows.Forms.Button
    Friend WithEvents btnSalvar As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtBancoDeDados As System.Windows.Forms.TextBox
End Class
