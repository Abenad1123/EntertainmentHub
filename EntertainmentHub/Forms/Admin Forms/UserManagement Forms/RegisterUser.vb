Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports BCrypt.Net
Imports MySql.Data.MySqlClient

Public Class RegisterUser

    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        StyleDataGridView()
        LoadCustomerData()
        LoadMembershipLevels()
        TextBox5.PasswordChar = "*"c
    End Sub

    Private Sub LoadCustomerData()
        Using conn = DBConnection.GetConnection()
            Try
                Dim query As String = "SELECT CustomerID, FirstName, LastName, EmailAddress, created_at, updated_at FROM customerinfo"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridView1.DataSource = dt
                End Using
                FormatColumns()
            Catch ex As Exception
                MessageBox.Show("Error loading customer data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub StyleDataGridView()
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToResizeRows = False
        DataGridView1.ReadOnly = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
        DataGridView1.RowHeadersVisible = False

        DataGridView1.BackgroundColor = Color.White
        DataGridView1.BorderStyle = BorderStyle.None
        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal

        DataGridView1.EnableHeadersVisualStyles = False
        DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48)
        DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        DataGridView1.ColumnHeadersHeight = 40
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        DataGridView1.DefaultCellStyle.BackColor = Color.White
        DataGridView1.DefaultCellStyle.ForeColor = Color.Black
        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255)
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black
        DataGridView1.DefaultCellStyle.Font = New Font("Segoe UI", 9)
        DataGridView1.DefaultCellStyle.Padding = New Padding(5, 0, 5, 0)

        DataGridView1.RowTemplate.Height = 35
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub FormatColumns()
        If DataGridView1.Columns.Count > 0 Then
            DataGridView1.Columns("CustomerID").HeaderText = "ID"
            DataGridView1.Columns("CustomerID").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns("CustomerID").Width = 50
            DataGridView1.Columns("CustomerID").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns("CustomerID").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView1.Columns("FirstName").HeaderText = "First Name"
            DataGridView1.Columns("FirstName").FillWeight = 25

            DataGridView1.Columns("LastName").HeaderText = "Last Name"
            DataGridView1.Columns("LastName").FillWeight = 25

            DataGridView1.Columns("EmailAddress").HeaderText = "Email Address"
            DataGridView1.Columns("EmailAddress").FillWeight = 50

            DataGridView1.Columns("created_at").HeaderText = "Date Created"
            DataGridView1.Columns("created_at").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridView1.Columns("created_at").DefaultCellStyle.Format = "MMM dd, yyyy"

            DataGridView1.Columns("updated_at").HeaderText = "Last Updated"
            DataGridView1.Columns("updated_at").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridView1.Columns("updated_at").DefaultCellStyle.Format = "MMM dd, yyyy"
        End If
    End Sub

    Private Sub LoadMembershipLevels()
        Using conn = DBConnection.GetConnection()
            Try
                Dim query As String = "SELECT MembershipLevelID, MembershipLevelName FROM membershiplevel"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    ComboBox1.DataSource = dt
                    ComboBox1.DisplayMember = "MembershipLevelName"
                    ComboBox1.ValueMember = "MembershipLevelID"
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading membership levels: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox2.Text) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Please fill out First Name, Last Name, and Email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim query As String = "INSERT INTO customerinfo (FirstName, LastName, EmailAddress) VALUES (@fname, @lname, @email)"
                        Using cmd As New MySqlCommand(query, conn, transaction)
                            cmd.Parameters.AddWithValue("@fname", TextBox1.Text.Trim())
                            cmd.Parameters.AddWithValue("@lname", TextBox2.Text.Trim())
                            cmd.Parameters.AddWithValue("@email", TextBox3.Text.Trim())
                            cmd.ExecuteNonQuery()
                        End Using

                        Dim auditQuery As String = "INSERT INTO auditing (EmployeeID, TableName, ActionType) VALUES (@adminId, 'customerinfo', 'Insert')"
                        Using cmdAudit As New MySqlCommand(auditQuery, conn, transaction)
                            cmdAudit.Parameters.AddWithValue("@adminId", AccountData.AdminId)
                            cmdAudit.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                        MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        TextBox1.Clear()
                        TextBox2.Clear()
                        TextBox3.Clear()
                        LoadCustomerData()

                    Catch ex As MySqlException When ex.Number = 1062
                        transaction.Rollback()
                        MessageBox.Show("An item with this email address already exists.", "Duplicate Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            Catch MySqlEx As MySqlException
                MessageBox.Show("Database Error: " & MySqlEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a customer from the table to assign an account.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox4.Text) OrElse String.IsNullOrWhiteSpace(TextBox5.Text) Then
            MessageBox.Show("Please enter a Username and Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim customerId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("CustomerID").Value)
        Dim membershipId As Integer = Convert.ToInt32(ComboBox1.SelectedValue)
        Dim userName As String = TextBox4.Text.Trim()
        Dim plainTextPassword As String = TextBox5.Text

        Dim passwordHash As String = BCrypt.Net.BCrypt.HashPassword(plainTextPassword)

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim queryAccount As String = "INSERT INTO account (CustomerID, MembershipLevelID, Status) VALUES (@custId, @memId, 'Active')"
                        Using cmdAccount As New MySqlCommand(queryAccount, conn, transaction)
                            cmdAccount.Parameters.AddWithValue("@custId", customerId)
                            cmdAccount.Parameters.AddWithValue("@memId", membershipId)
                            cmdAccount.ExecuteNonQuery()
                        End Using

                        Dim newAccountId As Integer = 0
                        Using cmdId As New MySqlCommand("SELECT LAST_INSERT_ID()", conn, transaction)
                            newAccountId = Convert.ToInt32(cmdId.ExecuteScalar())
                        End Using

                        Dim queryLogin As String = "INSERT INTO accountlogin (AccountID, UserName, PasswordHash) VALUES (@accId, @user, @hash)"
                        Using cmdLogin As New MySqlCommand(queryLogin, conn, transaction)
                            cmdLogin.Parameters.AddWithValue("@accId", newAccountId)
                            cmdLogin.Parameters.AddWithValue("@user", userName)
                            cmdLogin.Parameters.AddWithValue("@hash", passwordHash)
                            cmdLogin.ExecuteNonQuery()
                        End Using

                        Dim auditQuery As String = "INSERT INTO auditing (EmployeeID, TableName, ActionType) VALUES (@adminId, 'account', 'Insert')"
                        Using cmdAudit As New MySqlCommand(auditQuery, conn, transaction)
                            cmdAudit.Parameters.AddWithValue("@adminId", AccountData.AdminId)
                            cmdAudit.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                        MessageBox.Show("Account and login created securely!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        TextBox4.Clear()
                        TextBox5.Clear()

                    Catch ex As MySqlException When ex.Number = 1062
                        transaction.Rollback()
                        MessageBox.Show("Error: That Username or Account might already exist.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            Catch MySqlEx As MySqlException
                MessageBox.Show("Database Error: " & MySqlEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
        Dim frm As New UserManagement()
        frm.Show()
        Me.Close()
    End Sub
End Class