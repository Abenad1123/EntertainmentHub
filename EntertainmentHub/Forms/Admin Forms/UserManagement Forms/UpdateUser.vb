Imports MySql.Data.MySqlClient

Public Class UpdateUser
    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New UserManagement()
        frm.Show()
        Me.Close()
    End Sub

    Private currentCustomerID As Integer = 0
    Private currentAccountID As Integer = 0

    Private Sub CustomerAccountManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Populate the Filter Dropdowns (Feature 3)
        LoadFilterMembership()
        LoadFilterStatus()

        ' 2. Populate the Edit Dropdowns (Feature 2)
        LoadEditMembership()
        LoadEditStatus()

        ' 3. Initialize grid arrays
        RefreshGrids()
        txtboxPassword.PasswordChar = "*"c
    End Sub

    ' --- Dropdown Initializers ---

    Private Sub LoadFilterMembership()
        Using conn = DBConnection.GetConnection()
            Try
                Dim query As String = "SELECT MembershipLevelID, MembershipLevelName FROM membershiplevel"
                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                ' Insert a structural default row for unfiltered states
                Dim dr As DataRow = dt.NewRow()
                dr("MembershipLevelID") = 0
                dr("MembershipLevelName") = "-- All Memberships --"
                dt.Rows.InsertAt(dr, 0)

                RemoveHandler cmbboxMembership.SelectedIndexChanged, AddressOf FilterChanged
                cmbboxMembership.DataSource = dt
                cmbboxMembership.DisplayMember = "MembershipLevelName"
                cmbboxMembership.ValueMember = "MembershipLevelID"
                cmbboxMembership.SelectedIndex = 0
                AddHandler cmbboxMembership.SelectedIndexChanged, AddressOf FilterChanged
            Catch ex As Exception
                MessageBox.Show("Error loading filters: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadFilterStatus()
        RemoveHandler cmbboxStatus.SelectedIndexChanged, AddressOf FilterChanged
        cmbboxStatus.Items.Clear()
        cmbboxStatus.Items.AddRange(New Object() {"-- All Statuses --", "Active", "Inactive", "Suspended"})
        cmbboxStatus.SelectedIndex = 0
        AddHandler cmbboxStatus.SelectedIndexChanged, AddressOf FilterChanged
    End Sub

    Private Sub LoadEditMembership()
        Using conn = DBConnection.GetConnection()
            Try
                Dim query As String = "SELECT MembershipLevelID, MembershipLevelName FROM membershiplevel"
                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                cmbboxEditMembership.DataSource = dt
                cmbboxEditMembership.DisplayMember = "MembershipLevelName"
                cmbboxEditMembership.ValueMember = "MembershipLevelID"
            Catch ex As Exception
                ' Handled via application error mapping
            End Try
        End Using
    End Sub

    Private Sub LoadEditStatus()
        cmbboxEditStatus.Items.Clear()
        cmbboxEditStatus.Items.AddRange(New Object() {"Active", "Inactive", "Suspended"})
        cmbboxEditStatus.SelectedIndex = 0
    End Sub

    ' --- Feature 1 & 3: Unified Data Load (Combines Search + Filters) ---
    Private Sub RefreshGrids()
        Dim searchInput As String = txtboxSearchBox.Text.Trim()

        ' Read values from filter controls
        Dim filterMembershipID As Integer = 0
        If cmbboxMembership.SelectedValue IsNot Nothing Then
            Integer.TryParse(cmbboxMembership.SelectedValue.ToString(), filterMembershipID)
        End If
        Dim filterStatus As String = cmbboxStatus.SelectedItem?.ToString()

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()

                ' --- 1. POPULATE DATAGRIEVIEW1 (customerinfo) ---
                ' To filter customerinfo by membership/status, we LEFT JOIN the account tables here
                Dim queryCust As String = "
                    SELECT DISTINCT c.CustomerID, c.FirstName, c.LastName, c.EmailAddress, c.PhoneNumber, c.created_at, c.updated_at 
                    FROM customerinfo c
                    LEFT JOIN account a ON c.CustomerID = a.CustomerID
                    WHERE 1=1"

                If Not String.IsNullOrEmpty(searchInput) Then
                    queryCust &= " AND CONCAT(c.FirstName, ' ', c.LastName) LIKE @search"
                End If
                If filterMembershipID > 0 Then
                    queryCust &= " AND a.MembershipLevelID = @memId"
                End If
                If Not String.IsNullOrEmpty(filterStatus) AndAlso filterStatus <> "-- All Statuses --" Then
                    queryCust &= " AND a.Status = @status"
                End If

                Using cmdCust As New MySqlCommand(queryCust, conn)
                    If Not String.IsNullOrEmpty(searchInput) Then cmdCust.Parameters.AddWithValue("@search", "%" & searchInput & "%")
                    If filterMembershipID > 0 Then cmdCust.Parameters.AddWithValue("@memId", filterMembershipID)
                    If Not String.IsNullOrEmpty(filterStatus) AndAlso filterStatus <> "-- All Statuses --" Then cmdCust.Parameters.AddWithValue("@status", filterStatus)

                    Dim dtCust As New DataTable()
                    Dim adapterCust As New MySqlDataAdapter(cmdCust)
                    adapterCust.Fill(dtCust)
                    DataGridView1.DataSource = dtCust
                End Using


                ' --- 2. POPULATE DATAGRIEVIEW2 (Joined accounts) ---
                ' FIXED: Removed FirstName and LastName from the SELECT clause entirely
                Dim queryAcc As String = "
                    SELECT a.AccountID, a.CustomerID, m.MembershipLevelName, a.Status, al.UserName, a.updated_at 
                    FROM account a
                    JOIN accountlogin al ON a.AccountID = al.AccountID
                    JOIN customerinfo c ON a.CustomerID = c.CustomerID
                    JOIN membershiplevel m ON a.MembershipLevelID = m.MembershipLevelID
                    WHERE 1=1"

                If Not String.IsNullOrEmpty(searchInput) Then
                    queryAcc &= " AND CONCAT(c.FirstName, ' ', c.LastName) LIKE @search"
                End If
                If filterMembershipID > 0 Then
                    queryAcc &= " AND a.MembershipLevelID = @memId"
                End If
                If Not String.IsNullOrEmpty(filterStatus) AndAlso filterStatus <> "-- All Statuses --" Then
                    queryAcc &= " AND a.Status = @status"
                End If

                Using cmdAcc As New MySqlCommand(queryAcc, conn)
                    If Not String.IsNullOrEmpty(searchInput) Then cmdAcc.Parameters.AddWithValue("@search", "%" & searchInput & "%")
                    If filterMembershipID > 0 Then cmdAcc.Parameters.AddWithValue("@memId", filterMembershipID)
                    If Not String.IsNullOrEmpty(filterStatus) AndAlso filterStatus <> "-- All Statuses --" Then cmdAcc.Parameters.AddWithValue("@status", filterStatus)

                    Dim dtAcc As New DataTable()
                    Dim adapterAcc As New MySqlDataAdapter(cmdAcc)
                    adapterAcc.Fill(dtAcc)
                    DataGridView2.DataSource = dtAcc
                End Using

                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            Catch ex As MySqlException
                MessageBox.Show("Filter Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' Wire callers up to unified processing system
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        RefreshGrids()
    End Sub

    Private Sub FilterChanged(sender As Object, e As EventArgs)
        RefreshGrids()
    End Sub

    ' --- Feature 2: Control Mapping From Selection ---

    ' DataGridView1 selection maps tracking
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            currentCustomerID = Convert.ToInt32(row.Cells("CustomerID").Value)

            txtboxFirstName.Text = row.Cells("FirstName").Value.ToString()
            txtboxLastName.Text = row.Cells("LastName").Value.ToString()
            txtboxEmail.Text = row.Cells("EmailAddress").Value.ToString()
            txtboxContactNum.Text = row.Cells("PhoneNumber").Value.ToString()

            FetchLinkedAccountDetails(currentCustomerID)
        End If
    End Sub

    ' DataGridView2 selection maps tracking
    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
            currentCustomerID = Convert.ToInt32(row.Cells("CustomerID").Value)
            currentAccountID = Convert.ToInt32(row.Cells("AccountID").Value)

            ' Fetch background fields missing on joined tables setup
            Using conn = DBConnection.GetConnection()
                Try
                    conn.Open()
                    Dim query As String = "SELECT FirstName, LastName, EmailAddress, PhoneNumber FROM customerinfo WHERE CustomerID = @id"
                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@id", currentCustomerID)
                        Using reader = cmd.ExecuteReader()
                            If reader.Read() Then
                                txtboxFirstName.Text = reader("FirstName").ToString()
                                txtboxLastName.Text = reader("LastName").ToString()
                                txtboxEmail.Text = reader("EmailAddress").ToString()
                                txtboxContactNum.Text = reader("PhoneNumber").ToString()
                            End If
                        End Using
                    End Using
                Catch ex As Exception
                End Try
            End Using

            txtboxUsername.Text = row.Cells("UserName").Value.ToString()
            txtboxPassword.Clear()
            cmbboxEditStatus.SelectedItem = row.Cells("Status").Value.ToString()
            cmbboxEditMembership.SelectedIndex = cmbboxEditMembership.FindStringExact(row.Cells("MembershipLevelName").Value.ToString())
        End If
    End Sub

    ' Inner query sync logic matching DataGridView1 targets
    Private Sub FetchLinkedAccountDetails(custId As Integer)
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "
                    SELECT a.AccountID, al.UserName, a.Status, m.MembershipLevelName 
                    FROM account a
                    JOIN accountlogin al ON a.AccountID = al.AccountID
                    JOIN membershiplevel m ON a.MembershipLevelID = m.MembershipLevelID
                    WHERE a.CustomerID = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", custId)
                    Using rdr = cmd.ExecuteReader()
                        If rdr.Read() Then
                            currentAccountID = Convert.ToInt32(rdr("AccountID"))
                            txtboxUsername.Text = rdr("UserName").ToString()
                            cmbboxEditStatus.SelectedItem = rdr("Status").ToString()
                            cmbboxEditMembership.SelectedIndex = cmbboxEditMembership.FindStringExact(rdr("MembershipLevelName").ToString())
                        Else
                            currentAccountID = 0
                            txtboxUsername.Clear()
                            cmbboxEditStatus.SelectedIndex = 0
                            cmbboxEditMembership.SelectedIndex = 0
                        End If
                        txtboxPassword.Clear()
                    End Using
                End Using
            Catch ex As Exception
            End Try
        End Using
    End Sub

    ' --- Master Update Core Transaction ---
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If currentCustomerID = 0 Then
            MessageBox.Show("Please click on a record inside the data views first.", "Selection Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        ' Step 1: Push updates to customerinfo
                        Dim queryCust As String = "UPDATE customerinfo SET FirstName=@fn, LastName=@ln, EmailAddress=@email, PhoneNumber=@phone, updated_at=NOW() WHERE CustomerID=@cid"
                        Using cmdCust As New MySqlCommand(queryCust, conn, transaction)
                            cmdCust.Parameters.AddWithValue("@fn", txtboxFirstName.Text.Trim())
                            cmdCust.Parameters.AddWithValue("@ln", txtboxLastName.Text.Trim())
                            cmdCust.Parameters.AddWithValue("@email", txtboxEmail.Text.Trim())
                            cmdCust.Parameters.AddWithValue("@phone", txtboxContactNum.Text.Trim())
                            cmdCust.Parameters.AddWithValue("@cid", currentCustomerID)
                            cmdCust.ExecuteNonQuery()
                        End Using

                        ' Step 2: Push changes to system configuration records if valid profiles exist
                        If currentAccountID > 0 Then
                            Dim queryAcc As String = "UPDATE account SET MembershipLevelID=@mid, Status=@status, updated_at=NOW() WHERE AccountID=@aid"
                            Using cmdAcc As New MySqlCommand(queryAcc, conn, transaction)
                                cmdAcc.Parameters.AddWithValue("@mid", Convert.ToInt32(cmbboxEditMembership.SelectedValue))
                                cmdAcc.Parameters.AddWithValue("@status", cmbboxEditStatus.SelectedItem.ToString())
                                cmdAcc.Parameters.AddWithValue("@aid", currentAccountID)
                                cmdAcc.ExecuteNonQuery()
                            End Using

                            ' Run conditional updates on security hashes
                            Dim queryLogin As String = "UPDATE accountlogin SET UserName=@uname"
                            Dim plainPass As String = txtboxPassword.Text
                            If Not String.IsNullOrEmpty(plainPass) Then queryLogin &= ", PasswordHash=@hash"
                            queryLogin &= " WHERE AccountID=@aid"

                            Using cmdLogin As New MySqlCommand(queryLogin, conn, transaction)
                                cmdLogin.Parameters.AddWithValue("@uname", txtboxUsername.Text.Trim())
                                cmdLogin.Parameters.AddWithValue("@aid", currentAccountID)
                                If Not String.IsNullOrEmpty(plainPass) Then
                                    cmdLogin.Parameters.AddWithValue("@hash", BCrypt.Net.BCrypt.HashPassword(plainPass))
                                End If
                                cmdLogin.ExecuteNonQuery()
                            End Using
                        End If

                        transaction.Commit()
                        MessageBox.Show("Profile successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        RefreshGrids()

                    Catch ex As MySqlException
                        transaction.Rollback()
                        If ex.Number = 1062 Then
                            MessageBox.Show("Error: A unique database constraint was hit. Username, email, or phone may already exist.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Else
                            Throw ex
                        End If
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                MessageBox.Show("Save operations failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub
End Class