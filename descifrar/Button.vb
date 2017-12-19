Public Class Button
    Inherits System.Windows.Forms.Button


    Public Sub New()
        MyBase.New()

        FlatStyle = Windows.Forms.FlatStyle.Flat
        MyBase.FlatAppearance.BorderSize = 0
        Me.ForeColor = Color.Gray
        'Me.Size = New Size(75, 30)
        Me.Size = New Size(140, 40)

    End Sub


    Private _flat As FlatStyle

    Public Overloads Property FlatStyle() As FlatStyle
        Get
            Return _flat
        End Get
        Set(ByVal value As FlatStyle)
            MyBase.FlatStyle = Windows.Forms.FlatStyle.Flat
            _flat = Windows.Forms.FlatStyle.Flat

        End Set
    End Property


    Protected Overrides Sub OnPaint(ByVal pe As PaintEventArgs)
        ' Call the OnPaint method of the base class.
        MyBase.OnPaint(pe)

        ' Declare and instantiate a drawing pen.
        Dim myPen As System.Drawing.Pen = New System.Drawing.Pen(System.Drawing.Color.FromArgb(CType(CType(219, Byte), Integer), _
                                                                                               CType(CType(232, Byte), Integer), _
                                                                                               CType(CType(243, Byte), Integer)))

        ' Draw an aqua rectangle in the rectangle represented by the control.
        pe.Graphics.DrawRectangle(myPen, New Rectangle(1, 1, MyBase.Width - 3, MyBase.Height - 3))

    End Sub


    Private Sub Button_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        Me.BackColor = BackColorActivo
    End Sub

    Private Sub Button_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Me.BackColor = BackColorMouseMove
    End Sub



    Private _enabled As Boolean = True

    Public Overloads Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            _enabled = value
            MyBase.Enabled = value
            If value = True Then
                Me.BackColor = BackColorActivo

            Else
                Me.BackColor = BackColorInactivo

            End If
            Me.Invalidate()
        End Set
    End Property



    Private _backcolor As Color = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), _
                                                                CType(CType(240, Byte), Integer), _
                                                                CType(CType(240, Byte), Integer))
    Public Overrides Property BackColor() As Color
        Get
            Return _backcolor
        End Get
        Set(ByVal value As Color)
            _backcolor = value
            MyBase.BackColor = value
            MyBase.Refresh()
        End Set
    End Property


    Private _backcoloractivo As Color = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), _
                                                                CType(CType(240, Byte), Integer), _
                                                                CType(CType(240, Byte), Integer))
    Public Property BackColorActivo() As Color
        Get
            Return _backcoloractivo
        End Get
        Set(ByVal value As Color)
            _backcoloractivo = value
            MyBase.Refresh()
        End Set
    End Property

    Private _backcolorInactivo As Color = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), _
                                                                CType(CType(169, Byte), Integer), _
                                                                CType(CType(169, Byte), Integer))
    Public Property BackColorInactivo() As Color
        Get
            Return _backcolorInactivo
        End Get
        Set(ByVal value As Color)
            _backcolorInactivo = value
        End Set
    End Property

    Private _backcolorMouseMove As Color = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), _
                                                                         CType(CType(248, Byte), Integer), _
                                                                         CType(CType(255, Byte), Integer))
    ''' <summary>
    ''' Cambia de color al momento de pasar el mouse
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BackColorMouseMove() As Color
        Get
            Return _backcolorMouseMove
        End Get
        Set(ByVal value As Color)
            _backcolorMouseMove = value
        End Set
    End Property

End Class
