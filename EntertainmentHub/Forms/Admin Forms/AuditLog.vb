Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class AuditLog
    Private Sub SystemAuditLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeComboBox()
        StyleDataGridView()
        LoadAuditData()
    End Sub

    Private Sub InitializeComboBox()
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("All Actions")
        ComboBox1.Items.AddRange(New Object() {"Insert", "Update", "Delete", "Override", "Login_Failure", "Sale", "Support"})
        ComboBox1.SelectedIndex = 0
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

    Private Sub LoadAuditData()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT a.AuditID, COALESCE(el.UserName, 'System/Unknown') AS AdminUsername, a.TableName, a.ActionType, a.created_at FROM auditing a LEFT JOIN employeelogin el ON a.EmployeeID = el.EmployeeID WHERE 1=1"

                Dim searchText As String = TextBox1.Text.Trim()
                Dim selectedAction As String = ComboBox1.SelectedItem?.ToString()

                If Not String.IsNullOrEmpty(searchText) Then
                    query &= " AND (el.UserName LIKE @search OR a.TableName LIKE @search)"
                End If

                If Not String.IsNullOrEmpty(selectedAction) AndAlso selectedAction <> "All Actions" Then
                    query &= " AND a.ActionType = @action"
                End If

                query &= " ORDER BY a.created_at DESC"

                Using cmd As New MySqlCommand(query, conn)
                    If Not String.IsNullOrEmpty(searchText) Then
                        cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                    End If

                    If Not String.IsNullOrEmpty(selectedAction) AndAlso selectedAction <> "All Actions" Then
                        cmd.Parameters.AddWithValue("@action", selectedAction)
                    End If

                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridView1.DataSource = dt
                End Using

                FormatColumns()

            Catch ex As Exception
                MessageBox.Show("Error loading audit logs: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub FormatColumns()
        If DataGridView1.Columns.Count > 0 Then
            If DataGridView1.Columns.Contains("AuditID") Then
                DataGridView1.Columns("AuditID").Visible = False
            End If

            DataGridView1.Columns("AdminUsername").HeaderText = "Admin Username"
            DataGridView1.Columns("AdminUsername").FillWeight = 25

            DataGridView1.Columns("TableName").HeaderText = "Target Table"
            DataGridView1.Columns("TableName").FillWeight = 25

            DataGridView1.Columns("ActionType").HeaderText = "Action"
            DataGridView1.Columns("ActionType").FillWeight = 20

            DataGridView1.Columns("created_at").HeaderText = "Timestamp"
            DataGridView1.Columns("created_at").FillWeight = 30
            DataGridView1.Columns("created_at").DefaultCellStyle.Format = "MMM dd, yyyy hh:mm:ss tt"
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.RowIndex >= 0 AndAlso DataGridView1.Columns.Contains("ActionType") Then
            Dim actionColIndex As Integer = DataGridView1.Columns("ActionType").Index

            If e.ColumnIndex = actionColIndex Then
                Dim actionVal As String = e.Value?.ToString()

                If actionVal = "Delete" Then
                    e.CellStyle.ForeColor = Color.FromArgb(211, 47, 47)
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                ElseIf actionVal = "Login_Failure" Then
                    e.CellStyle.ForeColor = Color.FromArgb(245, 124, 0)
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                ElseIf actionVal = "Override" Then
                    e.CellStyle.ForeColor = Color.FromArgb(106, 27, 154)
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                ElseIf actionVal = "Update" Then
                    e.CellStyle.ForeColor = Color.FromArgb(25, 118, 210)
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LoadAuditData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        ComboBox1.SelectedIndex = 0
        LoadAuditData()
    End Sub

    Private Sub btnGoBack_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
        Dim frm As New AdminDashboard()
        frm.Show()
        Me.Close()
    End Sub
End Class