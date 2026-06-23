Imports MySql.Data.MySqlClient

Public Class AdminLoginMenu
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

        If txtboxUsername.Text.Trim() = "" Or txtboxPassword.Text.Trim() = "" Then
            MessageBox.Show("Please enter your username and password.")
            Exit Sub
        End If

        Try
            Using conn As MySqlConnection = DBConnection.GetConnection()

                conn.Open()

                Dim query As String =
                    "SELECT CustomerLoginID FROM CustomerLogin " &
                    "WHERE Username = @username AND Password = @password"

                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@username", txtboxUsername.Text.Trim())
                    cmd.Parameters.AddWithValue("@password", txtboxPassword.Text.Trim())

                    Dim result = cmd.ExecuteScalar()

                    If result IsNot Nothing Then
                        Dim updateQuery As String =
                            "UPDATE CustomerLogin " &
                            "SET LastLogin = NOW() " &
                            "WHERE CustomerLoginID = @id"

                        Using updateCmd As New MySqlCommand(updateQuery, conn)
                            updateCmd.Parameters.AddWithValue("@id", Convert.ToInt32(result))
                            updateCmd.ExecuteNonQuery()
                        End Using

                        MessageBox.Show("Login Successful!")

                        Dim frm As New MainMenu
                        frm.Show()
                        Me.Close()

                    Else
                        MessageBox.Show("Invalid username or password.")
                    End If

                End Using

            End Using

        Catch ex As Exception
            MessageBox.Show("Database Error: " & ex.Message)
        End Try
    End Sub

End Class