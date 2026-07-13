Imports System.Data
Imports System.Drawing
Imports System.Reflection.Emit
Imports System.Windows.Forms
Imports BCrypt.Net
Imports MySql.Data.MySqlClient

Public Class UpdateUser
    Private currentCustomerID As Integer = 0
    Private currentAccountID As Integer = 0

    Private Sub UpdateUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        HelperFunc.EnableDoubleBuffer(Me)

        Me.BackgroundImage = AccountData.AdminCommonBackground
        Me.BackgroundImageLayout = ImageLayout.Stretch

        HelperFunc.ApplyBorder(DataGridView1)
        HelperFunc.ApplyBorder(DataGridView2)

        TableLayoutPanel2.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel2)

        Dim labels As Control() = {lblRole, lblStatus, Label1, Label2, Label3, Label4, Label5, Label6, Label7, Label8}
        For Each i In labels
            HelperFunc.FontDesign(i, Color.FromArgb(255, 255, 255), AppFonts.Coolvetica(17))
        Next

        HelperFunc.ApplyButtonTheme(btnSearch)
        HelperFunc.ApplyButtonTheme(btnUpdate)

        LoadFilterMembership()
        LoadFilterStatus()
        LoadEditMembership()
        LoadEditStatus()

        StyleDataGridViews()
        RefreshGrids()

        txtboxPassword.PasswordChar = "*"c
    End Sub

    Private Sub StyleDataGridViews()
        Dim grids = {DataGridView1, DataGridView2}

        For Each dgv In grids
            dgv.AllowUserToAddRows = False
            dgv.AllowUserToDeleteRows = False
            dgv.AllowUserToResizeRows = False
            dgv.ReadOnly = True
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv.MultiSelect = False
            dgv.RowHeadersVisible = False

            dgv.BackgroundColor = Color.White
            dgv.BorderStyle = BorderStyle.None
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal

            dgv.EnableHeadersVisualStyles = False
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48)
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            dgv.ColumnHeadersHeight = 40
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            dgv.DefaultCellStyle.BackColor = Color.White
            dgv.DefaultCellStyle.ForeColor = Color.Black
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255)
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black
            dgv.DefaultCellStyle.Font = New Font("Segoe UI", 9)
            dgv.DefaultCellStyle.Padding = New Padding(5, 0, 5, 0)

            dgv.RowTemplate.Height = 35
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Next
    End Sub

    Private Sub FormatColumns()
        If DataGridView1.Columns.Count > 0 Then
            DataGridView1.Columns("CustomerID").HeaderText = "ID"
            DataGridView1.Columns("CustomerID").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns("CustomerID").Width = 50
            DataGridView1.Columns("CustomerID").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns("CustomerID").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView1.Columns("FirstName").HeaderText = "First Name"
            DataGridView1.Columns("FirstName").FillWeight = 20

            DataGridView1.Columns("LastName").HeaderText = "Last Name"
            DataGridView1.Columns("LastName").FillWeight = 20

            DataGridView1.Columns("EmailAddress").HeaderText = "Email Address"
            DataGridView1.Columns("EmailAddress").FillWeight = 30

            If DataGridView1.Columns.Contains("PhoneNumber") Then
                DataGridView1.Columns("PhoneNumber").HeaderText = "Phone Number"
                DataGridView1.Columns("PhoneNumber").FillWeight = 20
            End If

            DataGridView1.Columns("created_at").HeaderText = "Date Created"
            DataGridView1.Columns("created_at").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridView1.Columns("created_at").DefaultCellStyle.Format = "MMM dd, yyyy hh:mm tt"

            DataGridView1.Columns("updated_at").HeaderText = "Last Updated"
            DataGridView1.Columns("updated_at").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridView1.Columns("updated_at").DefaultCellStyle.Format = "MMM dd, yyyy hh:mm tt"
        End If

        If DataGridView2.Columns.Count > 0 Then
            DataGridView2.Columns("AccountID").HeaderText = "Account ID"
            DataGridView2.Columns("AccountID").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView2.Columns("AccountID").Width = 80
            DataGridView2.Columns("AccountID").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView2.Columns("AccountID").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView2.Columns("CustomerID").HeaderText = "Customer ID"
            DataGridView2.Columns("CustomerID").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView2.Columns("CustomerID").Width = 80
            DataGridView2.Columns("CustomerID").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView2.Columns("CustomerID").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView2.Columns("MembershipLevelName").HeaderText = "Membership"
            DataGridView2.Columns("MembershipLevelName").FillWeight = 30

            DataGridView2.Columns("Status").HeaderText = "Status"
            DataGridView2.Columns("Status").FillWeight = 20

            DataGridView2.Columns("UserName").HeaderText = "Username"
            DataGridView2.Columns("UserName").FillWeight = 30

            DataGridView2.Columns("updated_at").HeaderText = "Last Updated"
            DataGridView2.Columns("updated_at").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridView2.Columns("updated_at").DefaultCellStyle.Format = "MMM dd, yyyy hh:mm tt"
        End If
    End Sub

    Private Sub LoadFilterMembership()
        Using conn = DBConnection.GetConnection()
            Try
                Dim query As String = "SELECT MembershipLevelID, MembershipLevelName FROM membershiplevel"
                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim dt As New DataTable()
                adapter.Fill(dt)

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
            End Try
        End Using
    End Sub

    Private Sub LoadEditStatus()
        cmbboxEditStatus.Items.Clear()
        cmbboxEditStatus.Items.AddRange(New Object() {"Active", "Inactive", "Suspended"})
        cmbboxEditStatus.SelectedIndex = 0
    End Sub

    Private Sub RefreshGrids()
        Dim searchInput As String = txtboxSearchBox.Text.Trim()
        Dim filterMembershipID As Integer = 0

        If cmbboxMembership.SelectedValue IsNot Nothing Then
            Integer.TryParse(cmbboxMembership.SelectedValue.ToString(), filterMembershipID)
        End If

        Dim filterStatus As String = cmbboxStatus.SelectedItem?.ToString()

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()

                Dim queryCust As String = "SELECT DISTINCT c.CustomerID, c.FirstName, c.LastName, c.EmailAddress, c.PhoneNumber, c.created_at, c.updated_at FROM customerinfo c LEFT JOIN account a ON c.CustomerID = a.CustomerID WHERE 1=1"

                If Not String.IsNullOrEmpty(searchInput) Then queryCust &= " AND CONCAT(c.FirstName, ' ', c.LastName) LIKE @search"
                If filterMembershipID > 0 Then queryCust &= " AND a.MembershipLevelID = @memId"
                If Not String.IsNullOrEmpty(filterStatus) AndAlso filterStatus <> "-- All Statuses --" Then queryCust &= " AND a.Status = @status"

                Using cmdCust As New MySqlCommand(queryCust, conn)
                    If Not String.IsNullOrEmpty(searchInput) Then cmdCust.Parameters.AddWithValue("@search", "%" & searchInput & "%")
                    If filterMembershipID > 0 Then cmdCust.Parameters.AddWithValue("@memId", filterMembershipID)
                    If Not String.IsNullOrEmpty(filterStatus) AndAlso filterStatus <> "-- All Statuses --" Then cmdCust.Parameters.AddWithValue("@status", filterStatus)

                    Dim dtCust As New DataTable()
                    Dim adapterCust As New MySqlDataAdapter(cmdCust)
                    adapterCust.Fill(dtCust)
                    DataGridView1.DataSource = dtCust
                End Using

                Dim queryAcc As String = "SELECT a.AccountID, a.CustomerID, m.MembershipLevelName, a.Status, al.UserName, a.updated_at FROM account a JOIN accountlogin al ON a.AccountID = al.AccountID JOIN customerinfo c ON a.CustomerID = c.CustomerID JOIN membershiplevel m ON a.MembershipLevelID = m.MembershipLevelID WHERE 1=1"

                If Not String.IsNullOrEmpty(searchInput) Then queryAcc &= " AND CONCAT(c.FirstName, ' ', c.LastName) LIKE @search"
                If filterMembershipID > 0 Then queryAcc &= " AND a.MembershipLevelID = @memId"
                If Not String.IsNullOrEmpty(filterStatus) AndAlso filterStatus <> "-- All Statuses --" Then queryAcc &= " AND a.Status = @status"

                Using cmdAcc As New MySqlCommand(queryAcc, conn)
                    If Not String.IsNullOrEmpty(searchInput) Then cmdAcc.Parameters.AddWithValue("@search", "%" & searchInput & "%")
                    If filterMembershipID > 0 Then cmdAcc.Parameters.AddWithValue("@memId", filterMembershipID)
                    If Not String.IsNullOrEmpty(filterStatus) AndAlso filterStatus <> "-- All Statuses --" Then cmdAcc.Parameters.AddWithValue("@status", filterStatus)

                    Dim dtAcc As New DataTable()
                    Dim adapterAcc As New MySqlDataAdapter(cmdAcc)
                    adapterAcc.Fill(dtAcc)
                    DataGridView2.DataSource = dtAcc
                End Using

                FormatColumns()

            Catch ex As MySqlException
                MessageBox.Show("Filter Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        RefreshGrids()
    End Sub

    Private Sub FilterChanged(sender As Object, e As EventArgs)
        RefreshGrids()
    End Sub

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

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
            currentCustomerID = Convert.ToInt32(row.Cells("CustomerID").Value)
            currentAccountID = Convert.ToInt32(row.Cells("AccountID").Value)

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

            Dim membershipIdx As Integer = cmbboxEditMembership.FindStringExact(row.Cells("MembershipLevelName").Value.ToString())
            If membershipIdx >= 0 Then cmbboxEditMembership.SelectedIndex = membershipIdx
        End If
    End Sub

    Private Sub FetchLinkedAccountDetails(custId As Integer)
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT a.AccountID, al.UserName, a.Status, m.MembershipLevelName FROM account a JOIN accountlogin al ON a.AccountID = al.AccountID JOIN membershiplevel m ON a.MembershipLevelID = m.MembershipLevelID WHERE a.CustomerID = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", custId)
                    Using rdr = cmd.ExecuteReader()
                        If rdr.Read() Then
                            currentAccountID = Convert.ToInt32(rdr("AccountID"))
                            txtboxUsername.Text = rdr("UserName").ToString()
                            cmbboxEditStatus.SelectedItem = rdr("Status").ToString()

                            Dim membershipIdx As Integer = cmbboxEditMembership.FindStringExact(rdr("MembershipLevelName").ToString())
                            If membershipIdx >= 0 Then cmbboxEditMembership.SelectedIndex = membershipIdx
                        Else
                            currentAccountID = 0
                            txtboxUsername.Clear()
                            If cmbboxEditStatus.Items.Count > 0 Then cmbboxEditStatus.SelectedIndex = 0
                            If cmbboxEditMembership.Items.Count > 0 Then cmbboxEditMembership.SelectedIndex = 0
                        End If
                        txtboxPassword.Clear()
                    End Using
                End Using
            Catch ex As Exception
            End Try
        End Using
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If currentCustomerID = 0 Then
            MessageBox.Show("Please click on a record inside the data views first.", "Selection Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim fn As String = txtboxFirstName.Text.Trim()
        Dim ln As String = txtboxLastName.Text.Trim()
        Dim email As String = txtboxEmail.Text.Trim()
        Dim phone As String = txtboxContactNum.Text.Trim()

        ' Pre-flight Validation
        If String.IsNullOrWhiteSpace(fn) OrElse String.IsNullOrWhiteSpace(ln) OrElse String.IsNullOrWhiteSpace(email) Then
            MessageBox.Show("First Name, Last Name, and Email Address are required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        ' Removed manual updated_at=NOW() as the schema utilizes 'ON UPDATE current_timestamp()'
                        Dim queryCust As String = "UPDATE customerinfo SET FirstName=@fn, LastName=@ln, EmailAddress=@email, PhoneNumber=@phone WHERE CustomerID=@cid"
                        Using cmdCust As New MySqlCommand(queryCust, conn, transaction)
                            cmdCust.Parameters.AddWithValue("@fn", fn)
                            cmdCust.Parameters.AddWithValue("@ln", ln)
                            cmdCust.Parameters.AddWithValue("@email", email)

                            ' FIX: Inserting empty string "" into a UNIQUE column throws a duplicate error. Must inject DBNull.
                            If String.IsNullOrWhiteSpace(phone) Then
                                cmdCust.Parameters.AddWithValue("@phone", DBNull.Value)
                            Else
                                cmdCust.Parameters.AddWithValue("@phone", phone)
                            End If

                            cmdCust.Parameters.AddWithValue("@cid", currentCustomerID)
                            cmdCust.ExecuteNonQuery()
                        End Using

                        Dim logDescCust As String = $"Updated profile information for Customer ID {currentCustomerID} ({fn} {ln})."
                        HelperFunc.Log(conn, transaction, AccountData.AdminId, "customerinfo", "Update", logDescCust)

                        If currentAccountID > 0 Then
                            ' Secondary Validation for existing accounts
                            If String.IsNullOrWhiteSpace(txtboxUsername.Text) Then
                                Throw New Exception("Username cannot be empty for an active account.")
                            End If

                            If cmbboxEditMembership.SelectedValue Is Nothing OrElse cmbboxEditStatus.SelectedItem Is Nothing Then
                                Throw New Exception("Please ensure both a Membership Level and Status are selected.")
                            End If

                            Dim queryAcc As String = "UPDATE account SET MembershipLevelID=@mid, Status=@status WHERE AccountID=@aid"
                            Using cmdAcc As New MySqlCommand(queryAcc, conn, transaction)
                                cmdAcc.Parameters.AddWithValue("@mid", Convert.ToInt32(cmbboxEditMembership.SelectedValue))
                                cmdAcc.Parameters.AddWithValue("@status", cmbboxEditStatus.SelectedItem.ToString())
                                cmdAcc.Parameters.AddWithValue("@aid", currentAccountID)
                                cmdAcc.ExecuteNonQuery()
                            End Using

                            Dim logDescAcc As String = $"Modified account status and membership level for Account ID {currentAccountID}."
                            HelperFunc.Log(conn, transaction, AccountData.AdminId, "account", "Update", logDescAcc)

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

                            Dim logDescLogin As String = $"Modified authentication credentials for Account ID {currentAccountID}."
                            HelperFunc.Log(conn, transaction, AccountData.AdminId, "accountlogin", "Update", logDescLogin)
                        End If

                        transaction.Commit()
                        MessageBox.Show("Profile successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        txtboxPassword.Clear() ' Clear out the password to prevent accidental re-hashing on consecutive updates
                        RefreshGrids()

                    Catch ex As MySqlException
                        transaction.Rollback()
                        If ex.Number = 1062 Then
                            MessageBox.Show("Error: A unique database constraint was hit. The Username, Email, or Phone Number you entered is already in use by another account.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
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

    Private Sub btnGoBack_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
        HelperFunc.SwitchForm(Me, New UserManagement())
    End Sub

    Private Sub BtnUserLoginEnter(sender As Object, e As EventArgs) Handles btnGoBack.MouseEnter
        btnGoBack.Image = My.Resources.go_back_state_2
    End Sub

    Private Sub BtnUserLoginLeave(sender As Object, e As EventArgs) Handles btnGoBack.MouseLeave
        btnGoBack.Image = My.Resources.go_back_state_1
    End Sub
End Class