Imports MySql.Data.MySqlClient

Public Class EmployeeManagement
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEmployeeData()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New RegisterEmployee()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub LoadEmployeeData()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                ' Select the columns matching your employee table structure
                Dim query As String = "SELECT EmployeeID, FirstName, LastName, BirthDate, ContactNumber, RolesID, created_at, updated_at FROM employee"

                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()

                    ' Fill the DataTable with the query results
                    adapter.Fill(dt)

                    ' Bind the DataTable to DataGridView1
                    DataGridView1.DataSource = dt
                End Using

                ' Optional: Format the DataGridView columns for better readability
                FormatGrid()

            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New AdminDashboard()
        frm.Show()
        Me.Close()
    End Sub
End Class