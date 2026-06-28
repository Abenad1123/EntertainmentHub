Imports MySql.Data.MySqlClient

Public Class UpdateEmployee
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRolesFilter()
        LoadRolesForUpdate() ' NEW: Loads roles for the update combo box
        LoadEmployees()
        LoadEmployeeLogins() ' NEW: Loads DataGridView2

        FormatGrid()

        ' Mask the password box
        txtboxPassword.PasswordChar = "*"c
    End Sub

    ' --- Core Helper: Load DataGridView1 (Handles both Search and Filter) ---
    Private Sub LoadEmployees()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()

                ' Base query joining employee and roles to show the actual RoleName
                Dim query As String = "
                    SELECT e.EmployeeID, e.FirstName, e.LastName, e.BirthDate, e.ContactNumber, r.RoleName, e.created_at, e.updated_at 
                    FROM employee e 
                    LEFT JOIN roles r ON e.RolesID = r.RolesID 
                    WHERE 1=1"

                ' 1. Apply Search Filter (Feature 1)
                Dim searchTerm As String = txtboxSearchBox.Text.Trim()
                If Not String.IsNullOrEmpty(searchTerm) Then
                    query &= " AND (e.FirstName LIKE @search OR e.LastName LIKE @search OR e.EmployeeID = @exactId)"
                End If

                ' 2. Apply Role Filter (Feature 3)
                Dim selectedRole As Integer = 0
                If cmbboxRoles.SelectedValue IsNot Nothing AndAlso Integer.TryParse(cmbboxRoles.SelectedValue.ToString(), selectedRole) Then
                    If selectedRole > 0 Then
                        query &= " AND e.RolesID = @roleId"
                    End If
                End If

                query &= " ORDER BY e.EmployeeID ASC"

                Using cmd As New MySqlCommand(query, conn)
                    ' Assign Parameters
                    If Not String.IsNullOrEmpty(searchTerm) Then
                        cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")

                        ' Allow exact ID searching if they type a number
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

                ' Optional: Format the Date column
                If DataGridView1.Columns.Contains("BirthDate") Then
                    DataGridView1.Columns("BirthDate").DefaultCellStyle.Format = "yyyy-MM-dd"
                End If

            Catch ex As MySqlException
                MessageBox.Show("Error loading employees: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub FormatGrid()
        If DataGridView1.Columns.Count > 0 Then
            ' Automatically resize columns to fit the content
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells

            ' Specifically format the BirthDate column to show only the date
            If DataGridView1.Columns.Contains("BirthDate") Then
                DataGridView1.Columns("BirthDate").DefaultCellStyle.Format = "yyyy-MM-dd"
            End If

            ' Specifically format datetime columns
            If DataGridView1.Columns.Contains("created_at") Then
                DataGridView1.Columns("created_at").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss"
            End If
            If DataGridView1.Columns.Contains("updated_at") Then
                DataGridView1.Columns("updated_at").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss"
            End If
        End If
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

                    ' Create a default "All Roles" row so the user can see everyone
                    Dim dr As DataRow = dt.NewRow()
                    dr("RolesID") = 0
                    dr("RoleName") = "-- All Roles --"
                    dt.Rows.InsertAt(dr, 0)

                    ' Bind to Combo Box
                    ' Temporarily detach the event handler so it doesn't fire while loading
                    RemoveHandler cmbboxRoles.SelectedIndexChanged, AddressOf cmbboxRoles_SelectedIndexChanged

                    cmbboxRoles.DataSource = dt
                    cmbboxRoles.DisplayMember = "RoleName"
                    cmbboxRoles.ValueMember = "RolesID"
                    cmbboxRoles.SelectedIndex = 0 ' Select default

                    AddHandler cmbboxRoles.SelectedIndexChanged, AddressOf cmbboxRoles_SelectedIndexChanged
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Error loading roles: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' --- NEW: Load Roles specifically for the Update ComboBox ---
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

    ' --- NEW: Load employeelogin table into DataGridView2 ---
    Private Sub LoadEmployeeLogins()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                ' We omit PasswordHash for security
                Dim query As String = "SELECT EmployeeLoginID, EmployeeID, UserName, LastLogin FROM employeelogin"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridView2.DataSource = dt

                    If DataGridView2.Columns.Count > 0 Then
                        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                    End If
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Error loading logins: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' --- NEW: Populate Textboxes when a row is clicked ---
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)

            ' Populate Employee fields
            txtboxFirstName.Text = row.Cells("FirstName").Value.ToString()
            txtboxLastName.Text = row.Cells("LastName").Value.ToString()
            txtboxContactNum.Text = row.Cells("ContactNumber").Value.ToString()
            ' Note: txtboxEmail is not populated because 'employee' table has no Email column

            ' Set the Role ComboBox to match the selected employee's role
            Dim roleName As String = row.Cells("RoleName").Value.ToString()
            Dim index = cmbboxRoleUpd.FindStringExact(roleName)
            If index >= 0 Then
                cmbboxRoleUpd.SelectedIndex = index
            End If

            ' Fetch Login info based on the EmployeeID
            Dim empId As Integer = Convert.ToInt32(row.Cells("EmployeeID").Value)
            txtboxPassword.Clear() ' Always clear password field for security

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
                    ' Silently fail on selection if DB is busy, to avoid spamming the user
                End Try
            End Using
        End If
    End Sub

    ' --- NEW: Update Button Click ---
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an employee from the grid first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim empId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("EmployeeID").Value)

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                ' Use a transaction to ensure both tables update safely
                Using transaction = conn.BeginTransaction()
                    Try
                        ' 1. UPDATE EMPLOYEE TABLE
                        Dim queryEmp As String = "UPDATE employee SET FirstName = @fname, LastName = @lname, ContactNumber = @cnum, RolesID = @role, updated_at = NOW() WHERE EmployeeID = @empId"
                        Using cmdEmp As New MySqlCommand(queryEmp, conn, transaction)
                            cmdEmp.Parameters.AddWithValue("@fname", txtboxFirstName.Text.Trim())
                            cmdEmp.Parameters.AddWithValue("@lname", txtboxLastName.Text.Trim())
                            cmdEmp.Parameters.AddWithValue("@cnum", txtboxContactNum.Text.Trim())
                            cmdEmp.Parameters.AddWithValue("@role", Convert.ToInt32(cmbboxRoleUpd.SelectedValue))
                            cmdEmp.Parameters.AddWithValue("@empId", empId)
                            ' If you add an Email column later, add it to the query string and add the parameter here:
                            ' cmdEmp.Parameters.AddWithValue("@email", txtboxEmail.Text.Trim())

                            cmdEmp.ExecuteNonQuery()
                        End Using

                        ' 2. UPDATE EMPLOYEELOGIN TABLE
                        Dim uname As String = txtboxUsername.Text.Trim()
                        Dim plainPass As String = txtboxPassword.Text

                        If Not String.IsNullOrWhiteSpace(uname) Then
                            ' Dynamically build query depending on if they typed a new password or not
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
                        End If

                        transaction.Commit()
                        MessageBox.Show("Employee and Login records updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' Refresh both grids
                        LoadEmployees()
                        LoadEmployeeLogins()

                    Catch ex As MySqlException
                        transaction.Rollback()
                        If ex.Number = 1062 Then ' Duplicate Entry
                            MessageBox.Show("Update failed. That Username is already taken by another employee.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Else
                            Throw ex ' Send to outer catch
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

    ' --- Feature 1: Search Button Click ---
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadEmployees()
    End Sub

    ' --- Feature 3: Auto-Search when Role Combo Box Changes ---
    Private Sub cmbboxRoles_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadEmployees()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New EmployeeManagement()
        frm.Show()
        Me.Close()
    End Sub
End Class