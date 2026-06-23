Imports MySql.Data.MySqlClient

Public Class RegisterEmployee
    Private Sub RegisterEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRoles()
        LoadEmployeeData()

        TextBox3.PasswordChar = "*"c
    End Sub

    Private Sub LoadRoles()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT RolesID, RoleName FROM roles"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    ' Bind data to ComboBox1
                    ComboBox1.DataSource = dt
                    ComboBox1.DisplayMember = "RoleName" ' What the user sees
                    ComboBox1.ValueMember = "RolesID"    ' The underlying ID for the database
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Error loading roles: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub LoadEmployeeData()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT EmployeeID, FirstName, LastName, BirthDate, ContactNumber, RolesID, created_at, updated_at FROM employee"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridView1.DataSource = dt

                    ' Optional format for BirthDate column
                    If DataGridView1.Columns.Contains("BirthDate") Then
                        DataGridView1.Columns("BirthDate").DefaultCellStyle.Format = "yyyy-MM-dd"
                    End If
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Error loading employees: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' --- Button 1: Insert New Employee ---
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' 1. Basic validation
        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox2.Text) OrElse String.IsNullOrWhiteSpace(TextBox4.Text) OrElse ComboBox1.SelectedValue Is Nothing Then
            MessageBox.Show("Please fill out First Name, Last Name, Contact Number, and select a Role.")
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "INSERT INTO employee (FirstName, LastName, BirthDate, ContactNumber, RolesID) VALUES (@fname, @lname, @bdate, @cnum, @roleId)"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@fname", TextBox1.Text.Trim())
                    cmd.Parameters.AddWithValue("@lname", TextBox2.Text.Trim())
                    ' Format the DateTimePicker value for MySQL's DATE type
                    cmd.Parameters.AddWithValue("@bdate", DateTimePicker1.Value.ToString("yyyy-MM-dd"))
                    cmd.Parameters.AddWithValue("@cnum", TextBox4.Text.Trim())
                    cmd.Parameters.AddWithValue("@roleId", Convert.ToInt32(ComboBox1.SelectedValue))

                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Clear textboxes and refresh grid
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox4.Clear()
                DateTimePicker1.Value = DateTime.Now
                LoadEmployeeData()

            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' --- Button 2: Create Employee Login ---
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' 1. Validate Selections and Inputs
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an employee from the grid by clicking the row header on the left.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox5.Text) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Please enter a Username and Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Extract Data
        Dim employeeId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("EmployeeID").Value)
        Dim userName As String = TextBox5.Text.Trim()
        Dim plainTextPassword As String = TextBox3.Text

        ' 3. Hash the password using BCrypt
        Dim passwordHash As String = BCrypt.Net.BCrypt.HashPassword(plainTextPassword)

        ' 4. Database Insert
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()

                Dim queryLogin As String = "INSERT INTO employeelogin (EmployeeID, UserName, PasswordHash) VALUES (@empId, @user, @hash)"

                Using cmdLogin As New MySqlCommand(queryLogin, conn)
                    cmdLogin.Parameters.AddWithValue("@empId", employeeId)
                    cmdLogin.Parameters.AddWithValue("@user", userName)
                    cmdLogin.Parameters.AddWithValue("@hash", passwordHash)

                    cmdLogin.ExecuteNonQuery()
                End Using

                MessageBox.Show("Employee login credentials created securely!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Clear login fields
                TextBox5.Clear()
                TextBox3.Clear()

            Catch ex As MySqlException
                If ex.Number = 1062 Then ' MySQL error code for duplicate entry (UNI constraints)
                    MessageBox.Show("Error: That Username is already taken, or this Employee already has a login account.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub
End Class