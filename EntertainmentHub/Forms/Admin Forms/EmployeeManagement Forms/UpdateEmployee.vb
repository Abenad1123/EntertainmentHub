Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports BCrypt.Net
Imports MySql.Data.MySqlClient

Public Class UpdateEmployee

    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        StyleDataGridViews()
        LoadRolesFilter()
        LoadRolesForUpdate()
        LoadEmployees()
        LoadEmployeeLogins()

        txtboxPassword.PasswordChar = "*"c
    End Sub

    Private Sub StyleDataGridViews()
        Dim grids = {DataGridView1, DataGridView2}

        For Each dgv In grids
            dgv.AllowUserToAddRows = False
            dgv.AllowUserToDeleteRows = False
            dgv.AllowUserToResizeRows = False
            dgv.ReadOnly = True
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv.MultiSelect = False
            dgv.RowHeadersVisible = False

            dgv.BackgroundColor = Color.White
            dgv.BorderStyle = BorderStyle.None
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal

            dgv.EnableHeadersVisualStyles = False
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48)
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            dgv.ColumnHeadersHeight = 40
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            dgv.DefaultCellStyle.BackColor = Color.White
            dgv.DefaultCellStyle.ForeColor = Color.Black
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255)
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black
            dgv.DefaultCellStyle.Font = New Font("Segoe UI", 9)
            dgv.DefaultCellStyle.Padding = New Padding(5, 0, 5, 0)

            dgv.RowTemplate.Height = 35
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Next
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
            DataGridView1.Columns("RoleName").FillWeight = 20

            DataGridView1.Columns("created_at").HeaderText = "Date Created"
            DataGridView1.Columns("created_at").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridView1.Columns("created_at").DefaultCellStyle.Format = "MMM dd, yyyy"

            If DataGridView1.Columns.Contains("updated_at") Then
                DataGridView1.Columns("updated_at").HeaderText = "Last Updated"
                DataGridView1.Columns("updated_at").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                DataGridView1.Columns("updated_at").DefaultCellStyle.Format = "MMM dd, yyyy"
            End If
        End If

        If DataGridView2.Columns.Count > 0 Then
            DataGridView2.Columns("EmployeeLoginID").HeaderText = "Login ID"
            DataGridView2.Columns("EmployeeLoginID").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView2.Columns("EmployeeLoginID").Width = 80
            DataGridView2.Columns("EmployeeLoginID").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView2.Columns("EmployeeLoginID").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView2.Columns("EmployeeID").HeaderText = "Employee ID"
            DataGridView2.Columns("EmployeeID").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView2.Columns("EmployeeID").Width = 100
            DataGridView2.Columns("EmployeeID").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView2.Columns("EmployeeID").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView2.Columns("UserName").HeaderText = "Username"
            DataGridView2.Columns("UserName").FillWeight = 40

            DataGridView2.Columns("LastLogin").HeaderText = "Last Login Date"
            DataGridView2.Columns("LastLogin").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridView2.Columns("LastLogin").DefaultCellStyle.Format = "MMM dd, yyyy"
        End If
    End Sub

    Private Sub LoadEmployees()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()

                Dim query As String = "SELECT e.EmployeeID, e.FirstName, e.LastName, e.BirthDate, e.ContactNumber, r.RoleName, e.created_at, e.updated_at FROM employee e LEFT JOIN roles r ON e.RolesID = r.RolesID WHERE 1=1"

                Dim searchTerm As String = txtboxSearchBox.Text.Trim()
                If Not String.IsNullOrEmpty(searchTerm) Then
                    query &= " AND (e.FirstName LIKE @search OR e.LastName LIKE @search OR e.EmployeeID = @exactId)"
                End If

                Dim selectedRole As Integer = 0
                If cmbboxRoles.SelectedValue IsNot Nothing AndAlso Integer.TryParse(cmbboxRoles.SelectedValue.ToString(), selectedRole) Then
                    If selectedRole > 0 Then
                        query &= " AND e.RolesID = @roleId"
                    End If
                End If

                query &= " ORDER BY e.EmployeeID ASC"

                Using cmd As New MySqlCommand(query, conn)
                    If Not String.IsNullOrEmpty(searchTerm) Then
                        cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
                        Dim exactId As Integer = 0
                        Integer.TryParse(searchTerm, exactId)
                        cmd.Parameters.AddWithValue("@exactId", exactId)
                    End If

                    If selectedRole > 0 Then
                        cmd.Parameters.AddWithValue("@roleId", selectedRole)
                    End If

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

    Private Sub LoadRolesFilter()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT RolesID, RoleName FROM roles"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    Dim dr As DataRow = dt.NewRow()
                    dr("RolesID") = 0
                    dr("RoleName") = "-- All Roles --"
                    dt.Rows.InsertAt(dr, 0)

                    RemoveHandler cmbboxRoles.SelectedIndexChanged, AddressOf cmbboxRoles_SelectedIndexChanged
                    cmbboxRoles.DataSource = dt
                    cmbboxRoles.DisplayMember = "RoleName"
                    cmbboxRoles.ValueMember = "RolesID"
                    cmbboxRoles.SelectedIndex = 0
                    AddHandler cmbboxRoles.SelectedIndexChanged, AddressOf cmbboxRoles_SelectedIndexChanged
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Error loading roles: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub LoadRolesForUpdate()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT RolesID, RoleName FROM roles"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    cmbboxRoleUpd.DataSource = dt
                    cmbboxRoleUpd.DisplayMember = "RoleName"
                    cmbboxRoleUpd.ValueMember = "RolesID"
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Error loading update roles: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub LoadEmployeeLogins()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT EmployeeLoginID, EmployeeID, UserName, LastLogin FROM employeelogin"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridView2.DataSource = dt
                End Using

                FormatColumns()

            Catch ex As MySqlException
                MessageBox.Show("Error loading logins: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)

            txtboxFirstName.Text = row.Cells("FirstName").Value.ToString()
            txtboxLastName.Text = row.Cells("LastName").Value.ToString()
            txtboxContactNum.Text = row.Cells("ContactNumber").Value.ToString()

            If Not IsDBNull(row.Cells("BirthDate").Value) Then
                Dim parsedDate As DateTime
                If DateTime.TryParse(row.Cells("BirthDate").Value.ToString(), parsedDate) Then
                    dtpBirthDate.Value = parsedDate
                End If
            End If

            Dim roleName As String = row.Cells("RoleName").Value.ToString()
            Dim index = cmbboxRoleUpd.FindStringExact(roleName)
            If index >= 0 Then
                cmbboxRoleUpd.SelectedIndex = index
            End If

            Dim empId As Integer = Convert.ToInt32(row.Cells("EmployeeID").Value)
            txtboxPassword.Clear()

            Using conn = DBConnection.GetConnection()
                Try
                    conn.Open()
                    Dim query As String = "SELECT UserName FROM employeelogin WHERE EmployeeID = @empId"
                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@empId", empId)
                        Dim result = cmd.ExecuteScalar()

                        If result IsNot Nothing Then
                            txtboxUsername.Text = result.ToString()
                        Else
                            txtboxUsername.Clear()
                        End If
                    End Using
                Catch ex As Exception
                End Try
            End Using
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an employee from the grid first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim empId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("EmployeeID").Value)

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim queryEmp As String = "UPDATE employee SET FirstName = @fname, LastName = @lname, BirthDate = @bdate, ContactNumber = @cnum, RolesID = @role, updated_at = NOW() WHERE EmployeeID = @empId"
                        Using cmdEmp As New MySqlCommand(queryEmp, conn, transaction)
                            cmdEmp.Parameters.AddWithValue("@fname", txtboxFirstName.Text.Trim())
                            cmdEmp.Parameters.AddWithValue("@lname", txtboxLastName.Text.Trim())
                            cmdEmp.Parameters.AddWithValue("@cnum", txtboxContactNum.Text.Trim())
                            cmdEmp.Parameters.AddWithValue("@role", Convert.ToInt32(cmbboxRoleUpd.SelectedValue))
                            cmdEmp.Parameters.AddWithValue("@bdate", dtpBirthDate.Value.ToString("yyyy-MM-dd"))
                            cmdEmp.Parameters.AddWithValue("@empId", empId)
                            cmdEmp.ExecuteNonQuery()
                        End Using

                        Dim auditEmpQuery As String = "INSERT INTO auditing (EmployeeID, TableName, ActionType) VALUES (@adminId, 'employee', 'Update')"
                        Using cmdAuditEmp As New MySqlCommand(auditEmpQuery, conn, transaction)
                            cmdAuditEmp.Parameters.AddWithValue("@adminId", AccountData.AdminId)
                            cmdAuditEmp.ExecuteNonQuery()
                        End Using

                        Dim uname As String = txtboxUsername.Text.Trim()
                        Dim plainPass As String = txtboxPassword.Text

                        If Not String.IsNullOrWhiteSpace(uname) Then
                            Dim queryLog As String = "UPDATE employeelogin SET UserName = @uname"
                            If Not String.IsNullOrEmpty(plainPass) Then
                                queryLog &= ", PasswordHash = @hash"
                            End If
                            queryLog &= " WHERE EmployeeID = @empId"

                            Using cmdLog As New MySqlCommand(queryLog, conn, transaction)
                                cmdLog.Parameters.AddWithValue("@uname", uname)
                                cmdLog.Parameters.AddWithValue("@empId", empId)

                                If Not String.IsNullOrEmpty(plainPass) Then
                                    cmdLog.Parameters.AddWithValue("@hash", BCrypt.Net.BCrypt.HashPassword(plainPass))
                                End If

                                cmdLog.ExecuteNonQuery()
                            End Using

                            Dim auditLogQuery As String = "INSERT INTO auditing (EmployeeID, TableName, ActionType) VALUES (@adminId, 'employeelogin', 'Update')"
                            Using cmdAuditLog As New MySqlCommand(auditLogQuery, conn, transaction)
                                cmdAuditLog.Parameters.AddWithValue("@adminId", AccountData.AdminId)
                                cmdAuditLog.ExecuteNonQuery()
                            End Using
                        End If

                        transaction.Commit()
                        MessageBox.Show("Employee and Login records updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        LoadEmployees()
                        LoadEmployeeLogins()

                    Catch ex As MySqlException
                        transaction.Rollback()
                        If ex.Number = 1062 Then
                            MessageBox.Show("Update failed. That Username is already taken by another employee.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Else
                            Throw ex
                        End If
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadEmployees()
    End Sub

    Private Sub cmbboxRoles_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadEmployees()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
        Dim frm As New EmployeeManagement()
        frm.Show()
        Me.Close()
    End Sub

End Class