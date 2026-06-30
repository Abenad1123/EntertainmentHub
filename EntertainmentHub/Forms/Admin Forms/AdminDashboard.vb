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
        Label2.ForeColor = Color.FromArgb(255, 255, 255)

        txtboxSearchBox.Font = AppFonts.Hwygoth(16)

        btnOpenManageEntertainment.Font = AppFonts.Hwygoth(16)
        btnOpenManageProduct.Font = AppFonts.Hwygoth(16)
        btnOpenManageUser.Font = AppFonts.Hwygoth(16)
        btnOpenManageEmployee.Font = AppFonts.Hwygoth(16)

        btnOpenProductPOS.Font = AppFonts.Hwygoth(16)

        Me.BackgroundImage = My.Resources.background3
        Me.BackgroundImageLayout = ImageLayout.Stretch
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnOpenManageEntertainment.Click
        Dim frm As New EntertainmentConfiguration()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnOpenManageProduct.Click
        Dim frm As New ProductDashboard()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnOpenProductPOS.Click
        Dim frm As New ProductPOS()
        frm.Show()
        Me.Close()
    End Sub
End Class