Imports MySql.Data.MySqlClient

Public Class EmployeeManagement
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEmployees()
        LoadRolesFilter()

        FormatGrid()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim frm As New RegisterEmployee()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim frm As New UpdateEmployee()
        frm.Show()
        Me.Close()
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

    ' --- Feature 1: Search Button Click ---
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadEmployees()
    End Sub

    ' --- Feature 3: Auto-Search when Role Combo Box Changes ---
    Private Sub cmbboxRoles_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadEmployees()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New AdminDashboard()
        frm.Show()
        Me.Close()
    End Sub

    ' --- Feature 2: Delete Employee ---
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        ' Check if a row is actually selected
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an employee to delete by clicking the row header.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Confirm deletion
        If MessageBox.Show("Are you sure you want to completely delete this employee and their login access? This cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then

            Dim employeeId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("EmployeeID").Value)

            Using conn = DBConnection.GetConnection()
                Try
                    conn.Open()

                    ' Use a transaction so if one delete fails, the whole process rolls back
                    Using transaction = conn.BeginTransaction()
                        Try
                            ' Step A: Delete the child record first (employeelogin)
                            Dim queryDeleteLogin As String = "DELETE FROM employeelogin WHERE EmployeeID = @empId"
                            Using cmdLogin As New MySqlCommand(queryDeleteLogin, conn, transaction)
                                cmdLogin.Parameters.AddWithValue("@empId", employeeId)
                                cmdLogin.ExecuteNonQuery()
                            End Using

                            ' Step B: Delete the parent record (employee)
                            Dim queryDeleteEmployee As String = "DELETE FROM employee WHERE EmployeeID = @empId"
                            Using cmdEmployee As New MySqlCommand(queryDeleteEmployee, conn, transaction)
                                cmdEmployee.Parameters.AddWithValue("@empId", employeeId)
                                cmdEmployee.ExecuteNonQuery()
                            End Using

                            ' Commit the transaction
                            transaction.Commit()
                            MessageBox.Show("Employee and associated login deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            ' Refresh grid
                            LoadEmployees()

                        Catch ex As Exception
                            transaction.Rollback()
                            MessageBox.Show("Failed to delete records. Changes rolled back. Error: " & ex.Message, "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End Using
                Catch ex As MySqlException
                    MessageBox.Show("Database Connection Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End If
    End Sub
End Class