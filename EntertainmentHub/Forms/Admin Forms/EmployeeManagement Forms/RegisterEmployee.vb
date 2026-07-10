Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports BCrypt.Net
Imports MySql.Data.MySqlClient

Public Class RegisterEmployee
    Private Sub RegisterEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        HelperFunc.EnableDoubleBuffer(Me)

        Me.BackgroundImage = AccountData.AdminCommonBackground
        Me.BackgroundImageLayout = ImageLayout.Stretch

        Label8.ForeColor = Color.FromArgb(255, 255, 255)
        Label8.Font = AppFonts.Aero(30)

        TableLayoutPanel2.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel2)

        HelperFunc.ApplyButtonTheme(Button1)
        HelperFunc.ApplyButtonTheme(Button2)

        Dim labels As Control() = {Label1, Label2, Label3, Label4, Label5, Label6, Label7}
        For Each i In labels
            HelperFunc.FontDesign(i, Color.FromArgb(255, 255, 255), AppFonts.Coolvetica(18))
        Next

        TextBox3.PasswordChar = "*"c

        EnsureDefaultAdminExists()
        LoadRoles()
        StyleDataGridView()
        LoadEmployeeData()
    End Sub

    Private Sub EnsureDefaultAdminExists()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim checkLoginsQuery As String = "SELECT COUNT(*) FROM employeelogin"
                Dim loginCount As Integer
                Using cmdCheck As New MySqlCommand(checkLoginsQuery, conn)
                    loginCount = Convert.ToInt32(cmdCheck.ExecuteScalar())
                End Using

                If loginCount = 0 Then
                    Using transaction = conn.BeginTransaction()
                        Try
                            Dim insertEmpQuery As String = "INSERT INTO employee (FirstName, LastName, BirthDate, ContactNumber, RolesID, created_at, updated_at) " &
                                                           "VALUES (@fname, @lname, @bdate, @cnum, @roleId, NOW(), NOW())"
                            Dim defaultEmpId As Integer

                            Using cmdEmp As New MySqlCommand(insertEmpQuery, conn, transaction)
                                cmdEmp.Parameters.AddWithValue("@fname", "John")
                                cmdEmp.Parameters.AddWithValue("@lname", "Doe")
                                cmdEmp.Parameters.AddWithValue("@bdate", "2026-01-01")
                                cmdEmp.Parameters.AddWithValue("@cnum", "09123456789")
                                cmdEmp.Parameters.AddWithValue("@roleId", 1)
                                cmdEmp.ExecuteNonQuery()
                                defaultEmpId = Convert.ToInt32(cmdEmp.LastInsertedId)
                            End Using

                            Dim insertLoginQuery As String = "INSERT INTO employeelogin (EmployeeID, UserName, PasswordHash) VALUES (@empId, @user, @hash)"
                            Dim defaultPasswordHash As String = BCrypt.Net.BCrypt.HashPassword("admin")

                            Using cmdLogin As New MySqlCommand(insertLoginQuery, conn, transaction)
                                cmdLogin.Parameters.AddWithValue("@empId", defaultEmpId)
                                cmdLogin.Parameters.AddWithValue("@user", "admin")
                                cmdLogin.Parameters.AddWithValue("@hash", defaultPasswordHash)
                                cmdLogin.ExecuteNonQuery()
                            End Using

                            transaction.Commit()
                        Catch ex As Exception
                            transaction.Rollback()
                        End Try
                    End Using
                End If

            Catch ex As Exception
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
            DataGridView1.Columns("EmployeeID").HeaderText = "ID"
            DataGridView1.Columns("EmployeeID").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns("EmployeeID").Width = 50
            DataGridView1.Columns("EmployeeID").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns("EmployeeID").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView1.Columns("FirstName").HeaderText = "First Name"
            DataGridView1.Columns("FirstName").FillWeight = 20

            DataGridView1.Columns("LastName").HeaderText = "Last Name"
            DataGridView1.Columns("LastName").FillWeight = 20

            DataGridView1.Columns("BirthDate").HeaderText = "Birth Date"
            DataGridView1.Columns("BirthDate").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridView1.Columns("BirthDate").DefaultCellStyle.Format = "MMM dd, yyyy"

            DataGridView1.Columns("ContactNumber").HeaderText = "Contact Number"
            DataGridView1.Columns("ContactNumber").FillWeight = 20

            DataGridView1.Columns("RoleName").HeaderText = "Role"
            DataGridView1.Columns("RoleName").FillWeight = 15

            DataGridView1.Columns("created_at").HeaderText = "Date Created"
            DataGridView1.Columns("created_at").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridView1.Columns("created_at").DefaultCellStyle.Format = "MMM dd, yyyy"

            If DataGridView1.Columns.Contains("updated_at") Then
                DataGridView1.Columns("updated_at").HeaderText = "Last Updated"
                DataGridView1.Columns("updated_at").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                DataGridView1.Columns("updated_at").DefaultCellStyle.Format = "MMM dd, yyyy"
            End If
        End If
    End Sub

    Private Sub LoadRoles()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT RolesID, RoleName FROM roles"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    ComboBox1.DataSource = dt
                    ComboBox1.DisplayMember = "RoleName"
                    ComboBox1.ValueMember = "RolesID"
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Error loading roles: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub LoadEmployeeData()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT e.EmployeeID, e.FirstName, e.LastName, e.BirthDate, e.ContactNumber, r.RoleName, e.created_at, e.updated_at FROM employee e LEFT JOIN roles r ON e.RolesID = r.RolesID ORDER BY e.EmployeeID ASC"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridView1.DataSource = dt
                End Using

                FormatColumns()

            Catch ex As MySqlException
                MessageBox.Show("Error loading employees: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox2.Text) OrElse String.IsNullOrWhiteSpace(TextBox4.Text) OrElse ComboBox1.SelectedValue Is Nothing Then
            MessageBox.Show("Please fill out First Name, Last Name, Contact Number, and select a Role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim fname As String = TextBox1.Text.Trim()
                        Dim lname As String = TextBox2.Text.Trim()
                        Dim roleId As Integer = Convert.ToInt32(ComboBox1.SelectedValue)

                        Dim query As String = "INSERT INTO employee (FirstName, LastName, BirthDate, ContactNumber, RolesID) VALUES (@fname, @lname, @bdate, @cnum, @roleId)"
                        Using cmd As New MySqlCommand(query, conn, transaction)
                            cmd.Parameters.AddWithValue("@fname", fname)
                            cmd.Parameters.AddWithValue("@lname", lname)
                            cmd.Parameters.AddWithValue("@bdate", DateTimePicker1.Value.ToString("yyyy-MM-dd"))
                            cmd.Parameters.AddWithValue("@cnum", TextBox4.Text.Trim())
                            cmd.Parameters.AddWithValue("@roleId", roleId)
                            cmd.ExecuteNonQuery()
                        End Using

                        Dim logDesc As String = $"Registered new employee '{fname} {lname}' under Role ID {roleId}."
                        HelperFunc.Log(conn, transaction, AccountData.AdminId, "employee", "Insert", logDesc)

                        transaction.Commit()
                        MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        TextBox1.Clear()
                        TextBox2.Clear()
                        TextBox4.Clear()
                        DateTimePicker1.Value = DateTime.Now
                        LoadEmployeeData()

                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an employee from the table to assign an account.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox5.Text) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Please enter a Username and Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim employeeId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("EmployeeID").Value)
        Dim userName As String = TextBox5.Text.Trim()
        Dim plainTextPassword As String = TextBox3.Text

        Dim passwordHash As String = BCrypt.Net.BCrypt.HashPassword(plainTextPassword)

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim queryLogin As String = "INSERT INTO employeelogin (EmployeeID, UserName, PasswordHash) VALUES (@empId, @user, @hash)"
                        Using cmdLogin As New MySqlCommand(queryLogin, conn, transaction)
                            cmdLogin.Parameters.AddWithValue("@empId", employeeId)
                            cmdLogin.Parameters.AddWithValue("@user", userName)
                            cmdLogin.Parameters.AddWithValue("@hash", passwordHash)
                            cmdLogin.ExecuteNonQuery()
                        End Using

                        Dim logDesc As String = $"Generated secure login credentials for Employee ID {employeeId} with username '{userName}'."
                        HelperFunc.Log(conn, transaction, AccountData.AdminId, "employeelogin", "Insert", logDesc)

                        transaction.Commit()
                        MessageBox.Show("Employee login credentials created securely!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        TextBox5.Clear()
                        TextBox3.Clear()

                    Catch ex As MySqlException When ex.Number = 1062
                        transaction.Rollback()
                        MessageBox.Show("Error: That Username is already taken, or this Employee already has a login account.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
        Dim frm As New EmployeeManagement()
        frm.Show()
        Me.Close()
    End Sub
End Class