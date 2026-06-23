Imports MySql.Data.MySqlClient

Public Class UserLoginMenu
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = AppColors.Background

        lblPassword.Font = AppFonts.Hwygoth(14)
        lblUsername.Font = AppFonts.Hwygoth(14)
        lblTitle.Font = AppFonts.VenusRising(20)
        txtboxUsername.Font = AppFonts.Hwygwde(14)
        txtboxPassword.Font = AppFonts.Hwygwde(14)

        btnLogin.Font = AppFonts.Hwygoth(16)
    End Sub

    Private Sub GoBack(sender As Object, e As EventArgs) Handles lblGoBack.Click
        Dim frm As New StartMenu()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub GoBackBtnHoverIn(sender As Object, e As EventArgs) Handles lblGoBack.MouseEnter
        lblGoBack.ForeColor = AppColors.ForeColorHover
    End Sub

    Private Sub GoBackBtnHoverOut(sender As Object, e As EventArgs) Handles lblGoBack.MouseLeave
        lblGoBack.ForeColor = Color.White
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim txtUsername = txtboxUsername
        Dim txtPassword = txtboxPassword

        If txtUsername.Text.Trim() = "" Or txtPassword.Text.Trim() = "" Then
            MessageBox.Show("Please fill in all fields.")
            Exit Sub
        End If

        Try
            Using conn As MySqlConnection = DBConnection.GetConnection()

                conn.Open()

                Dim password As String = txtPassword.Text.Trim()
                Dim hash As String = BCrypt.Net.BCrypt.HashPassword(password)

                Dim query As String =
                    "INSERT INTO AccountLogin (UserName, PasswordHash) " &
                    "VALUES (@username, @hash)"

                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim())
                    cmd.Parameters.AddWithValue("@hash", hash)

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Account created successfully!")
                    Else
                        MessageBox.Show("Failed to create account.")
                    End If

                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show("Database Error: " & ex.Message)
        End Try
    End Sub

End Class