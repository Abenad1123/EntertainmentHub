Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class EmployeeManagement

    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRolesFilter()
        StyleDataGridView()
        LoadEmployees()

        Me.BackgroundImage = AccountData.AdminCommonBackground
        Me.BackgroundImageLayout = ImageLayout.Stretch

        lblRole.ForeColor = Color.FromArgb(255, 255, 255)

        TableLayoutPanel2.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel2)


        Dim ctrls As Control() = {btnDelete, btnRegister, btnSearch, btnUpdate}

        For Each i In ctrls
            HelperFunc.FontDesign(i, Color.FromArgb(0, 0, 0), AppFonts.CdSaver(14))
            HelperFunc.ApplyButtonTheme(i)
        Next
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

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadEmployees()
    End Sub

    Private Sub cmbboxRoles_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadEmployees()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
        Dim frm As New AdminDashboard()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an employee to delete by clicking the row header.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to completely delete this employee and their login access? This cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Dim employeeId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("EmployeeID").Value)

            Using conn = DBConnection.GetConnection()
                Try
                    conn.Open()

                    Using transaction = conn.BeginTransaction()
                        Try
                            Dim queryDeleteLogin As String = "DELETE FROM employeelogin WHERE EmployeeID = @empId"
                            Using cmdLogin As New MySqlCommand(queryDeleteLogin, conn, transaction)
                                cmdLogin.Parameters.AddWithValue("@empId", employeeId)
                                cmdLogin.ExecuteNonQuery()
                            End Using

                            Dim queryDeleteEmployee As String = "DELETE FROM employee WHERE EmployeeID = @empId"
                            Using cmdEmployee As New MySqlCommand(queryDeleteEmployee, conn, transaction)
                                cmdEmployee.Parameters.AddWithValue("@empId", employeeId)
                                cmdEmployee.ExecuteNonQuery()
                            End Using

                            Dim queryAudit As String = "INSERT INTO auditing (EmployeeID, TableName, ActionType) VALUES (@adminId, 'employee', 'Delete')"
                            Using cmdAudit As New MySqlCommand(queryAudit, conn, transaction)
                                cmdAudit.Parameters.AddWithValue("@adminId", AccountData.AdminId)
                                cmdAudit.ExecuteNonQuery()
                            End Using

                            transaction.Commit()
                            MessageBox.Show("Employee and associated login deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
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