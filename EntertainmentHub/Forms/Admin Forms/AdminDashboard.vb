Imports MySql.Data.MySqlClient

Public Class AdminDashboard

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = AppColors.Background

        lblTitle.ForeColor = Color.FromArgb(255, 255, 255)
        lblTitle.Font = AppFonts.VenusRising(22)

        Label1.ForeColor = Color.FromArgb(255, 255, 255)
    End Sub

    Private Sub OpenUserManager(sender As Object, e As EventArgs) Handles btnOpenManageUser.Click
        Dim frm As New UserManagement()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub OpenEmployeeManager(sender As Object, e As EventArgs) Handles btnOpenManageEmployee.Click
        Dim frm As New EmployeeManagement()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New EntertainmentConfiguration()
        frm.Show()
        Me.Close()
    End Sub
End Class