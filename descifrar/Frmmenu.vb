Public Class Frmmenu
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Dim ventanaNuevo As New Form1
        ventanaNuevo.ShowDialog()
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim ventanaAnterior As New frmAnterior
        ventanaAnterior.ShowDialog()
    End Sub
End Class