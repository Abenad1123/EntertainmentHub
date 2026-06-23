Imports MySql.Data.MySqlClient

Public Class AdminDashboard

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = AppColors.Background
        LoadCustomers()
    End Sub

    ' =========================
    ' LOAD DATA
    ' =========================
    Private Sub LoadCustomers()

        Dim query As String =
            "SELECT CustomerId, FirstName, LastName, EmailAddress FROM CustomerInfo"

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Using cmd As New MySqlCommand(query, conn)

                Try
                    conn.Open()

                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()

                    adapter.Fill(table)

                    DataGridView1.DataSource = table

                Catch ex As Exception
                    MessageBox.Show("Error loading data: " & ex.Message)
                End Try

            End Using
        End Using

    End Sub

    ' =========================
    ' INSERT
    ' =========================
    Private Sub Button1_Click(sender As Object, e As EventArgs)

        Dim query As String =
            "INSERT INTO CustomerInfo (FirstName, LastName, EmailAddress)
             VALUES (@FirstName, @LastName, @EmailAddress)"

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Using cmd As New MySqlCommand(query, conn)

                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text)
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text)
                cmd.Parameters.AddWithValue("@EmailAddress", txtEmail.Text)

                Try
                    conn.Open()

                    Dim rows As Integer = cmd.ExecuteNonQuery()

                    If rows > 0 Then
                        MessageBox.Show("Customer added successfully!")

                        LoadCustomers()

                        txtFirstName.Clear()
                        txtLastName.Clear()
                        txtEmail.Clear()
                    Else
                        MessageBox.Show("Insert failed.")
                    End If

                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try

            End Using
        End Using

    End Sub

    ' =========================
    ' TEST CONNECTION
    ' =========================
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Try
            Using conn As MySqlConnection = DBConnection.GetConnection()
                conn.Open()
                MessageBox.Show("Connected successfully!")
            End Using

        Catch ex As Exception
            MessageBox.Show("Connection failed: " & ex.Message)
        End Try

    End Sub

    ' =========================
    ' DELETE
    ' =========================
    Private Sub Button2_Click(sender As Object, e As EventArgs)

        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a customer to delete.")
            Exit Sub
        End If

        Dim customerId As Integer =
            Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("CustomerId").Value)

        Dim query As String =
            "DELETE FROM employeelogin WHERE CustomerId = @CustomerId"

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Using cmd As New MySqlCommand(query, conn)

                cmd.Parameters.AddWithValue("@CustomerId", customerId)

                Try
                    conn.Open()

                    Dim result As Integer = cmd.ExecuteNonQuery()

                    If result > 0 Then
                        MessageBox.Show("Customer deleted successfully!")
                        LoadCustomers()
                    Else
                        MessageBox.Show("Delete failed.")
                    End If

                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try

            End Using
        End Using

    End Sub

    Private Sub OpenUserManager(sender As Object, e As EventArgs) Handles btnOpenManageUser.Click
        Dim frm As New UserManagement()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub OpenEmployeeManager(sender As Object, e As EventArgs) Handles btnOpenManageEmployee.Click
        Dim frm As New EmployeeManagement()
        frm.Show()
        Me.Close()
    End Sub
End Class