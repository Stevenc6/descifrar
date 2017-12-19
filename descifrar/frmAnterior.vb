Public Class frmAnterior
    Private conexion As New claseConexion
    Private Sub btnCargarArchivo_Click(sender As Object, e As EventArgs) Handles btnCargarArchivo.Click
        Try

            Dim openFileDialog1 As New OpenFileDialog()

            txtPass.Text = String.Empty
            txtIP.Text = String.Empty
            txtBase.Text = String.Empty
            openFileDialog1.InitialDirectory = "c:\"
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|Todos los archivos (*.*)|*.*"
            openFileDialog1.FilterIndex = 2
            openFileDialog1.RestoreDirectory = True

            If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                'Va a mostrar la información necesaria.
                conexion.BuscarConexionEnXml(openFileDialog1.FileName())
                txtPass.Text = conexion.Password
                txtIP.Text = conexion.Server
                txtBase.Text = conexion.InitialCatalog
                txtUser.Text = conexion.UserId
            End If

        Catch ex As Exception
            MsgBox("Error", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    'Comentarios
End Class