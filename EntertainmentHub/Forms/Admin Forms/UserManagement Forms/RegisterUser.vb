Imports MySql.Data.MySqlClient

Public Class RegisterUser
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomerData()
        LoadMembershipLevels()


        TextBox5.PasswordChar = "*"c
    End Sub

    Private Sub LoadCustomerData()
        Using conn = DBConnection.GetConnection()
            Dim query As String = "SELECT CustomerID, FirstName, LastName, EmailAddress, created_at, updated_at FROM customerinfo"
            Using cmd As New MySqlCommand(query, conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                DataGridView1.DataSource = dt
            End Using
        End Using
    End Sub


    Private Sub LoadMembershipLevels()
        Using conn = DBConnection.GetConnection()
            Dim query As String = "SELECT MembershipLevelID, MembershipLevelName FROM membershiplevel"
            Using cmd As New MySqlCommand(query, conn)
                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)


                ComboBox1.DataSource = dt
                ComboBox1.DisplayMember = "MembershipLevelName"
                ComboBox1.ValueMember = "MembershipLevelID"
            End Using
        End Using
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox2.Text) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Please fill out First Name, Last Name, and Email.")
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "INSERT INTO customerinfo (FirstName, LastName, EmailAddress) VALUES (@fname, @lname, @email)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@fname", TextBox1.Text.Trim())
                    cmd.Parameters.AddWithValue("@lname", TextBox2.Text.Trim())
                    cmd.Parameters.AddWithValue("@email", TextBox3.Text.Trim())
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Customer added successfully!")


                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                LoadCustomerData()

            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message)
            End Try
        End Using
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a customer from the grid by clicking the row header.")
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox4.Text) OrElse String.IsNullOrWhiteSpace(TextBox5.Text) Then
            MessageBox.Show("Please enter a Username and Password.")
            Return
        End If


        Dim customerId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("CustomerID").Value)
        Dim membershipId As Integer = Convert.ToInt32(ComboBox1.SelectedValue)
        Dim userName As String = TextBox4.Text.Trim()
        Dim plainTextPassword As String = TextBox5.Text


        Dim passwordHash As String = BCrypt.Net.BCrypt.HashPassword(plainTextPassword)


        Using conn = DBConnection.GetConnection()
            conn.Open()

            Using transaction = conn.BeginTransaction()
                Try

                    Dim queryAccount As String = "INSERT INTO account (CustomerID, MembershipLevelID, Status) VALUES (@custId, @memId, 'Active'); SELECT LAST_INSERT_ID();"
                    Dim newAccountId As Integer

                    Using cmdAccount As New MySqlCommand(queryAccount, conn, transaction)
                        cmdAccount.Parameters.AddWithValue("@custId", customerId)
                        cmdAccount.Parameters.AddWithValue("@memId", membershipId)

                        newAccountId = Convert.ToInt32(cmdAccount.ExecuteScalar())
                    End Using


                    Dim queryLogin As String = "INSERT INTO accountlogin (AccountID, UserName, PasswordHash) VALUES (@accId, @user, @hash)"

                    Using cmdLogin As New MySqlCommand(queryLogin, conn, transaction)
                        cmdLogin.Parameters.AddWithValue("@accId", newAccountId)
                        cmdLogin.Parameters.AddWithValue("@user", userName)
                        cmdLogin.Parameters.AddWithValue("@hash", passwordHash)
                        cmdLogin.ExecuteNonQuery()
                    End Using


                    transaction.Commit()
                    MessageBox.Show("Account and login created securely!")


                    TextBox4.Clear()
                    TextBox5.Clear()

                Catch ex As MySqlException

                    transaction.Rollback()
                    If ex.Number = 1062 Then '
                        MessageBox.Show("Error: That Username or Account might already exist.")
                    Else
                        MessageBox.Show("Database Error: " & ex.Message)
                    End If
                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show("An unexpected error occurred: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New UserManagement()
        frm.Show()
        Me.Close()
    End Sub

End Class