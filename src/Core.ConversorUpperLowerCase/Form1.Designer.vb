﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.btnConverter = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.rbLower = New System.Windows.Forms.RadioButton
        Me.rbUpper = New System.Windows.Forms.RadioButton
        Me.SuspendLayout()
        '
        'btnConverter
        '
        Me.btnConverter.Location = New System.Drawing.Point(130, 89)
        Me.btnConverter.Name = "btnConverter"
        Me.btnConverter.Size = New System.Drawing.Size(75, 23)
        Me.btnConverter.TabIndex = 0
        Me.btnConverter.Text = "Converter"
        Me.btnConverter.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Modo (case)"
        '
        'rbLower
        '
        Me.rbLower.AutoSize = True
        Me.rbLower.Location = New System.Drawing.Point(16, 30)
        Me.rbLower.Name = "rbLower"
        Me.rbLower.Size = New System.Drawing.Size(54, 17)
        Me.rbLower.TabIndex = 2
        Me.rbLower.TabStop = True
        Me.rbLower.Text = "Lower"
        Me.rbLower.UseVisualStyleBackColor = True
        '
        'rbUpper
        '
        Me.rbUpper.AutoSize = True
        Me.rbUpper.Location = New System.Drawing.Point(76, 30)
        Me.rbUpper.Name = "rbUpper"
        Me.rbUpper.Size = New System.Drawing.Size(54, 17)
        Me.rbUpper.TabIndex = 3
        Me.rbUpper.TabStop = True
        Me.rbUpper.Text = "Upper"
        Me.rbUpper.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(321, 124)
        Me.Controls.Add(Me.rbUpper)
        Me.Controls.Add(Me.rbLower)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnConverter)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Core - Conversor Upper/Lower case"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnConverter As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rbLower As System.Windows.Forms.RadioButton
    Friend WithEvents rbUpper As System.Windows.Forms.RadioButton

End Class
