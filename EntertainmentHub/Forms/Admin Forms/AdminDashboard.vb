Imports System.Drawing
Imports System.Reflection.Emit
Imports MySql.Data.MySqlClient

Public Class AdminDashboard

    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackgroundImage = AccountData.AdminCommonBackground
        Me.BackgroundImageLayout = ImageLayout.Stretch

        lblTitle.ForeColor = Color.FromArgb(255, 255, 255)
        Label1.ForeColor = Color.FromArgb(255, 255, 255)
        Label2.ForeColor = Color.FromArgb(255, 255, 255)
        Label3.ForeColor = Color.FromArgb(255, 255, 255)

        lblTitle.Font = AppFonts.Aero(30)
        Label2.Font = AppFonts.VenusRising(20)
        Label1.Font = AppFonts.VenusRising(20)
        Label3.Font = AppFonts.Hwygwde(18)

        Dim ctrls As Control() = {btnOpenManageEntertainment, btnOpenManageProduct, btnOpenManageUser, btnOpenManageEmployee, btnOpenProductPOS, Button1, Button2, Button3, Button4, Button6}
        For Each i In ctrls
            HelperFunc.ApplyButtonTheme(i)
        Next
        LoadAdminGreeting()
    End Sub

    Private Sub LoadAdminGreeting()
        If AccountData.AdminId > 0 Then
            Using conn = DBConnection.GetConnection()
                Try
                    conn.Open()
                    Dim query As String = "SELECT FirstName, LastName FROM employee WHERE EmployeeID = @id"
                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@id", AccountData.AdminId)
                        Using reader = cmd.ExecuteReader()
                            If reader.Read() Then
                                Dim fullName As String = $"{reader("FirstName")} {reader("LastName")}"
                                Label3.Text = $"Welcome back to the hub, {fullName}!"
                            Else
                                Label3.Text = "Welcome to the Admin Dashboard!"
                            End If
                        End Using
                    End Using
                Catch ex As Exception
                    Label3.Text = "Welcome to the Admin Dashboard!"
                End Try
            End Using
        Else
            Label3.Text = "Welcome to the Admin Dashboard!"
        End If
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
        Dim frm As New ProductManagement()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnOpenProductPOS.Click
        Dim frm As New ProductPOS()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New RevenueReport()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frm As New AnalyticsMenu()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New SystemSetting()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim frm As New StartMenu()
        frm.Show()
        Me.Close()
        AccountData.AdminId = 0
        AccountData.AdminUsername = ""
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim frm As New AuditLog()
        frm.Show()
        Me.Close()
    End Sub
End Class