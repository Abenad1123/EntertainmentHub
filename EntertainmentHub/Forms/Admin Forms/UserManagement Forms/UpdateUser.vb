Public Class UpdateUser
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New UserManagement()
        frm.Show()
        Me.Close()
    End Sub
End Class