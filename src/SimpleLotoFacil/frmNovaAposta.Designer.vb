<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNovaAposta
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
        Me.pnlDezenasDisponiveis = New System.Windows.Forms.GroupBox
        Me.chkDezenasEscolhidas = New System.Windows.Forms.CheckedListBox
        Me.btnNovo = New System.Windows.Forms.Button
        Me.pnlDadosDoConcurso = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.MaskedTextBox1 = New System.Windows.Forms.MaskedTextBox
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlDadosDaAposta = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.btnExcluir = New System.Windows.Forms.Button
        Me.btnModificar = New System.Windows.Forms.Button
        Me.btnImprimir = New System.Windows.Forms.Button
        Me.pnlDezenasDisponiveis.SuspendLayout()
        Me.pnlDadosDoConcurso.SuspendLayout()
        Me.pnlDadosDaAposta.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlDezenasDisponiveis
        '
        Me.pnlDezenasDisponiveis.Controls.Add(Me.chkDezenasEscolhidas)
        Me.pnlDezenasDisponiveis.Location = New System.Drawing.Point(12, 195)
        Me.pnlDezenasDisponiveis.Name = "pnlDezenasDisponiveis"
        Me.pnlDezenasDisponiveis.Size = New System.Drawing.Size(283, 109)
        Me.pnlDezenasDisponiveis.TabIndex = 0
        Me.pnlDezenasDisponiveis.TabStop = False
        Me.pnlDezenasDisponiveis.Text = "Dezenas disponíveis"
        '
        'chkDezenasEscolhidas
        '
        Me.chkDezenasEscolhidas.CheckOnClick = True
        Me.chkDezenasEscolhidas.ColumnWidth = 50
        Me.chkDezenasEscolhidas.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.chkDezenasEscolhidas.FormattingEnabled = True
        Me.chkDezenasEscolhidas.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkDezenasEscolhidas.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25"})
        Me.chkDezenasEscolhidas.Location = New System.Drawing.Point(6, 19)
        Me.chkDezenasEscolhidas.MultiColumn = True
        Me.chkDezenasEscolhidas.Name = "chkDezenasEscolhidas"
        Me.chkDezenasEscolhidas.Size = New System.Drawing.Size(257, 79)
        Me.chkDezenasEscolhidas.TabIndex = 0
        '
        'btnNovo
        '
        Me.btnNovo.Location = New System.Drawing.Point(304, 12)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Size = New System.Drawing.Size(75, 23)
        Me.btnNovo.TabIndex = 1
        Me.btnNovo.Text = "Nova"
        Me.btnNovo.UseVisualStyleBackColor = True
        '
        'pnlDadosDoConcurso
        '
        Me.pnlDadosDoConcurso.Controls.Add(Me.Label2)
        Me.pnlDadosDoConcurso.Controls.Add(Me.DateTimePicker1)
        Me.pnlDadosDoConcurso.Controls.Add(Me.MaskedTextBox1)
        Me.pnlDadosDoConcurso.Controls.Add(Me.Label1)
        Me.pnlDadosDoConcurso.Location = New System.Drawing.Point(12, 83)
        Me.pnlDadosDoConcurso.Name = "pnlDadosDoConcurso"
        Me.pnlDadosDoConcurso.Size = New System.Drawing.Size(283, 106)
        Me.pnlDadosDoConcurso.TabIndex = 2
        Me.pnlDadosDoConcurso.TabStop = False
        Me.pnlDadosDoConcurso.Text = "Dados do concurso"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Número"
        '
        'MaskedTextBox1
        '
        Me.MaskedTextBox1.Location = New System.Drawing.Point(10, 37)
        Me.MaskedTextBox1.Name = "MaskedTextBox1"
        Me.MaskedTextBox1.Size = New System.Drawing.Size(158, 20)
        Me.MaskedTextBox1.TabIndex = 1
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(10, 76)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(231, 20)
        Me.DateTimePicker1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Data"
        '
        'pnlDadosDaAposta
        '
        Me.pnlDadosDaAposta.Controls.Add(Me.ComboBox1)
        Me.pnlDadosDaAposta.Controls.Add(Me.Label3)
        Me.pnlDadosDaAposta.Location = New System.Drawing.Point(12, 4)
        Me.pnlDadosDaAposta.Name = "pnlDadosDaAposta"
        Me.pnlDadosDaAposta.Size = New System.Drawing.Size(283, 73)
        Me.pnlDadosDaAposta.TabIndex = 3
        Me.pnlDadosDaAposta.TabStop = False
        Me.pnlDadosDaAposta.Text = "Dados da aposta"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Nome"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(10, 37)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(253, 21)
        Me.ComboBox1.TabIndex = 1
        '
        'btnExcluir
        '
        Me.btnExcluir.Location = New System.Drawing.Point(304, 41)
        Me.btnExcluir.Name = "btnExcluir"
        Me.btnExcluir.Size = New System.Drawing.Size(75, 23)
        Me.btnExcluir.TabIndex = 4
        Me.btnExcluir.Text = "Excluir"
        Me.btnExcluir.UseVisualStyleBackColor = True
        '
        'btnModificar
        '
        Me.btnModificar.Location = New System.Drawing.Point(304, 12)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(75, 23)
        Me.btnModificar.TabIndex = 5
        Me.btnModificar.Text = "Modificar"
        Me.btnModificar.UseVisualStyleBackColor = True
        '
        'btnImprimir
        '
        Me.btnImprimir.Location = New System.Drawing.Point(304, 71)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(75, 23)
        Me.btnImprimir.TabIndex = 6
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'frmNovaAposta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 312)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.btnModificar)
        Me.Controls.Add(Me.btnExcluir)
        Me.Controls.Add(Me.pnlDadosDaAposta)
        Me.Controls.Add(Me.pnlDadosDoConcurso)
        Me.Controls.Add(Me.btnNovo)
        Me.Controls.Add(Me.pnlDezenasDisponiveis)
        Me.Name = "frmNovaAposta"
        Me.Text = "Apostas"
        Me.pnlDezenasDisponiveis.ResumeLayout(False)
        Me.pnlDadosDoConcurso.ResumeLayout(False)
        Me.pnlDadosDoConcurso.PerformLayout()
        Me.pnlDadosDaAposta.ResumeLayout(False)
        Me.pnlDadosDaAposta.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlDezenasDisponiveis As System.Windows.Forms.GroupBox
    Friend WithEvents chkDezenasEscolhidas As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnNovo As System.Windows.Forms.Button
    Friend WithEvents pnlDadosDoConcurso As System.Windows.Forms.GroupBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents MaskedTextBox1 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlDadosDaAposta As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnExcluir As System.Windows.Forms.Button
    Friend WithEvents btnModificar As System.Windows.Forms.Button
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
End Class
