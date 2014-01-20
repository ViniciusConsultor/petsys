<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.btnExibir = New System.Windows.Forms.Button
        Me.txtResultado = New System.Windows.Forms.TextBox
        Me.lblQuantidadeDeJogosSorteados = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnExibir
        '
        Me.btnExibir.Location = New System.Drawing.Point(677, 12)
        Me.btnExibir.Name = "btnExibir"
        Me.btnExibir.Size = New System.Drawing.Size(75, 23)
        Me.btnExibir.TabIndex = 0
        Me.btnExibir.Text = "Exibir"
        Me.btnExibir.UseVisualStyleBackColor = True
        '
        'txtResultado
        '
        Me.txtResultado.Location = New System.Drawing.Point(23, 37)
        Me.txtResultado.Multiline = True
        Me.txtResultado.Name = "txtResultado"
        Me.txtResultado.Size = New System.Drawing.Size(636, 342)
        Me.txtResultado.TabIndex = 1
        '
        'lblQuantidadeDeJogosSorteados
        '
        Me.lblQuantidadeDeJogosSorteados.AutoSize = True
        Me.lblQuantidadeDeJogosSorteados.Location = New System.Drawing.Point(20, 17)
        Me.lblQuantidadeDeJogosSorteados.Name = "lblQuantidadeDeJogosSorteados"
        Me.lblQuantidadeDeJogosSorteados.Size = New System.Drawing.Size(0, 13)
        Me.lblQuantidadeDeJogosSorteados.TabIndex = 2
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(764, 391)
        Me.Controls.Add(Me.lblQuantidadeDeJogosSorteados)
        Me.Controls.Add(Me.txtResultado)
        Me.Controls.Add(Me.btnExibir)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExibir As System.Windows.Forms.Button
    Friend WithEvents txtResultado As System.Windows.Forms.TextBox
    Friend WithEvents lblQuantidadeDeJogosSorteados As System.Windows.Forms.Label

End Class
