Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports BCrypt.Net
Imports MySql.Data.MySqlClient

Public Class UserManagement

    ' --- Form Load: Initialize Grid and ComboBox ---
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomerData()
    End Sub

    ' --- Helper: Load Customer Info into DataGridView ---
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim frm As New TransactionManager()
        frm.Show()
        Me.Close()
    End Sub

    ' --- Button: Delete Customer ---
    Private Sub btnDeleteAccount_Click(sender As Object, e As EventArgs) Handles btnDeleteAccount.Click
        ' 1. Check if a row is selected
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a customer to delete by clicking the row header on the left.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Confirm the deletion
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this customer? This action cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            ' 3. Extract the CustomerID from the selected row
            Dim customerId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("CustomerID").Value)

            ' 4. Execute the Delete Query
            Using conn = DBConnection.GetConnection()
                Try
                    conn.Open()
                    Dim query As String = "DELETE FROM customerinfo WHERE CustomerID = @custId"

                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@custId", customerId)
                        cmd.ExecuteNonQuery()
                    End Using

                    MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' 5. Refresh the DataGridView to reflect the changes
                    LoadCustomerData()

                Catch ex As MySqlException
                    ' Error 1451 is MySQL's code for a Foreign Key Constraint violation
                    If ex.Number = 1451 Then
                        MessageBox.Show("Cannot delete this customer because they already have an active account or transactions tied to them in the system. You must delete their accounts first.", "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Else
                        MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Catch ex As Exception
                    MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
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