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

        txtboxPassword.PasswordChar = "*"c
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
        Dim username As String = txtboxUsername.Text.Trim()
        Dim password As String = txtboxPassword.Text

        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()

                Dim query As String = "SELECT AccountID, PasswordHash FROM accountlogin WHERE UserName = @user"
                Dim storedHash As String = String.Empty
                Dim accountId As Integer = 0

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@user", username)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            accountId = Convert.ToInt32(reader("AccountID"))
                            storedHash = reader("PasswordHash").ToString()
                        End If
                    End Using
                End Using

                If Not String.IsNullOrEmpty(storedHash) AndAlso BCrypt.Net.BCrypt.Verify(password, storedHash) Then

                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    UpdateLastLogin(accountId, conn)

                    Dim frm As New MainMenu()
                    frm.Show()
                    Me.Close()

                Else
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtboxPassword.Clear()
                    txtboxPassword.Focus()
                End If

            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' --- Helper Method: Update Last Login ---
    Private Sub UpdateLastLogin(accountId As Integer, conn As MySqlConnection)
        Dim updateQuery As String = "UPDATE accountlogin SET LastLogin = NOW() WHERE AccountID = @accId"
        Using updateCmd As New MySqlCommand(updateQuery, conn)
            updateCmd.Parameters.AddWithValue("@accId", accountId)
            updateCmd.ExecuteNonQuery()
        End Using
    End Sub
End Class