Public Class InventoryManagement
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New ProductDashboard()
        frm.Show()
        Me.Close()
    End Sub
End Class