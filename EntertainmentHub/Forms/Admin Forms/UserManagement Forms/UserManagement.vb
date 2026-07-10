Imports System.Data
Imports System.Drawing
Imports System.Reflection.Emit
Imports System.Windows.Forms
Imports BCrypt.Net
Imports MySql.Data.MySqlClient

Public Class UserManagement

    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.ForeColor = Color.FromArgb(255, 255, 255)
        Label1.Font = AppFonts.Aero(30)

        Me.BackgroundImage = AccountData.AdminCommonBackground
        Me.BackgroundImageLayout = ImageLayout.Stretch

        HelperFunc.ApplyButtonTheme(Button1)
        HelperFunc.ApplyButtonTheme(Button4)
        HelperFunc.ApplyButtonTheme(Button5)
        HelperFunc.ApplyButtonTheme(btnDeleteAccount)
        HelperFunc.ApplyButtonTheme(Button6)

        HelperFunc.ApplyBorder(DataGridView1)
        HelperFunc.ApplyBorder(TableLayoutPanel3)

        HelperFunc.FontDesign(Label2, Color.FromArgb(255, 255, 255), AppFonts.Coolvetica(18))

        StyleDataGridView()
        LoadCustomerData()
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

    Private Sub LoadCustomerData(Optional searchTerm As String = "")
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT CustomerID, FirstName, LastName, EmailAddress, created_at, updated_at FROM customerinfo"

                If Not String.IsNullOrWhiteSpace(searchTerm) Then
                    query &= " WHERE CONCAT(FirstName, ' ', LastName) LIKE @search"
                End If

                Using cmd As New MySqlCommand(query, conn)
                    If Not String.IsNullOrWhiteSpace(searchTerm) Then
                        cmd.Parameters.AddWithValue("@search", "%" & searchTerm.Trim() & "%")
                    End If

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadCustomerData(TextBox1.Text)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim frm As New TransactionManager()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub btnDeleteAccount_Click(sender As Object, e As EventArgs) Handles btnDeleteAccount.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a customer from the table to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this customer? This action cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            Dim customerId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("CustomerID").Value)

            Using conn = DBConnection.GetConnection()
                Try
                    conn.Open()
                    Using transaction = conn.BeginTransaction()
                        Try
                            Dim query As String = "DELETE FROM customerinfo WHERE CustomerID = @custId"
                            Using cmd As New MySqlCommand(query, conn, transaction)
                                cmd.Parameters.AddWithValue("@custId", customerId)
                                cmd.ExecuteNonQuery()
                            End Using

                            Dim auditQuery As String = "INSERT INTO auditing (EmployeeID, TableName, ActionType) VALUES (@adminId, 'customerinfo', 'Delete')"
                            Using cmdAudit As New MySqlCommand(auditQuery, conn, transaction)
                                cmdAudit.Parameters.AddWithValue("@adminId", AccountData.AdminId)
                                cmdAudit.ExecuteNonQuery()
                            End Using

                            transaction.Commit()
                            MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            LoadCustomerData(TextBox1.Text)

                        Catch ex As MySqlException When ex.Number = 1451
                            transaction.Rollback()
                            MessageBox.Show("Cannot delete this customer because they already have an active account or transactions tied to them in the system. You must delete their accounts first.", "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
        Dim frm As New AdminDashboard()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim frm As New RegisterUser()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim frm As New UpdateUser()
        frm.Show()
        Me.Close()
    End Sub

End Class