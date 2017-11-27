<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnCargarArchivo = New System.Windows.Forms.Button()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.txtBase = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(119, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(-9, -26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(553, 91)
        Me.Panel1.TabIndex = 0
        '
        'btnCargarArchivo
        '
        Me.btnCargarArchivo.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCargarArchivo.Location = New System.Drawing.Point(12, 80)
        Me.btnCargarArchivo.Name = "btnCargarArchivo"
        Me.btnCargarArchivo.Size = New System.Drawing.Size(75, 29)
        Me.btnCargarArchivo.TabIndex = 1
        Me.btnCargarArchivo.Text = "Cargar"
        Me.btnCargarArchivo.UseVisualStyleBackColor = True
        '
        'txtPass
        '
        Me.txtPass.Enabled = False
        Me.txtPass.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPass.Location = New System.Drawing.Point(12, 115)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.Size = New System.Drawing.Size(191, 23)
        Me.txtPass.TabIndex = 2
        '
        'txtIP
        '
        Me.txtIP.Enabled = False
        Me.txtIP.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIP.Location = New System.Drawing.Point(12, 141)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(191, 23)
        Me.txtIP.TabIndex = 3
        '
        'txtBase
        '
        Me.txtBase.Enabled = False
        Me.txtBase.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBase.Location = New System.Drawing.Point(12, 167)
        Me.txtBase.Name = "txtBase"
        Me.txtBase.Size = New System.Drawing.Size(191, 23)
        Me.txtBase.TabIndex = 4
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(285, 227)
        Me.Controls.Add(Me.txtBase)
        Me.Controls.Add(Me.txtIP)
        Me.Controls.Add(Me.txtPass)
        Me.Controls.Add(Me.btnCargarArchivo)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Descifrar"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnCargarArchivo As Button
    Friend WithEvents txtPass As TextBox
    Friend WithEvents txtIP As TextBox
    Friend WithEvents txtBase As TextBox
End Class
