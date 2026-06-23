Imports MySql.Data.MySqlClient

Public Class MainMenu

    ' ✅ SINGLE CONNECTION STRING (CHANGE ONLY THIS IF NEEDED)
    Dim connString As String = "server=localhost;userid=root;password=;database=entertainmenthub"

    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = AppColors.Background
        LoadCustomers()
    End Sub

    ' =========================
    ' LOAD DATA TO DATAGRIDVIEW
    ' =========================
    Private Sub LoadCustomers()

        Dim query As String = "SELECT CustomerId, FirstName, LastName, EmailAddress FROM CustomerInfo"

        Using conn As New MySqlConnection(connString)
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
    ' INSERT CUSTOMER (BUTTON1)
    ' =========================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim query As String = "INSERT INTO CustomerInfo (FirstName, LastName, EmailAddress) " &
                              "VALUES (@FirstName, @LastName, @EmailAddress)"

        Using conn As New MySqlConnection(connString)
            Using cmd As New MySqlCommand(query, conn)

                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text)
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text)
                cmd.Parameters.AddWithValue("@EmailAddress", txtEmail.Text)

                Try
                    conn.Open()

                    Dim rows As Integer = cmd.ExecuteNonQuery()

                    If rows > 0 Then
                        MessageBox.Show("Customer added successfully!")

                        ' refresh table
                        LoadCustomers()

                        ' clear fields
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
    ' TEST CONNECTION (BUTTON3)
    ' =========================
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Try
            Using conn As New MySqlConnection(connString)
                conn.Open()
                MessageBox.Show("Connected successfully!")
            End Using

        Catch ex As Exception
            MessageBox.Show("Connection failed: " & ex.Message)
        End Try

    End Sub

    ' =========================
    ' DELETE SELECTED CUSTOMER (BUTTON2)
    ' =========================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a customer to delete.")
            Exit Sub
        End If

        Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
        Dim customerId As Integer = Convert.ToInt32(selectedRow.Cells("CustomerId").Value)

        Dim query As String = "DELETE FROM Customers WHERE CustomerId = @CustomerId"

        Using conn As New MySqlConnection(connString)
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

End Class