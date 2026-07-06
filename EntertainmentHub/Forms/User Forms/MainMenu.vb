Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Threading.Tasks

Public Class MainMenu

    Private _customerLoginTime As DateTime? = Nothing
    Private _isBalanceHidden As Boolean = False
    Private _statusRefreshCounter As Integer = 0
    Private Const StatusRefreshIntervalSeconds As Integer = 5

    Private Async Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.DoubleBuffered = True
            Dim targetedUser As String = AccountData.CustomerUsername

            Await UpdateStatusCountsAsync()
            Await LoadRecentActivityAsync("All")
            Await LoadCustomerInformationAsync(targetedUser)

            LiveDurationTimer.Interval = 1000
            LiveDurationTimer.Start()
        Catch ex As Exception
            MessageBox.Show("Error initializing main hub workspace: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Customer Profile Operations"

    Private Async Function LoadCustomerInformationAsync(username As String) As Task
        If String.IsNullOrEmpty(username) Then Exit Function

        Dim query As String = "SELECT " &
                              "  a.Status, " &
                              "  al.UserName, " &
                              "  COALESCE(ml.MembershipLevelName, 'Unknown') As MembershipLevelName, " &
                              "  ent.EntertainmentName, " &
                              "  es.LoginTime, " &
                              "  (SELECT COALESCE(SUM(wt.Amount), 0) FROM wallettransactions wt WHERE wt.AccountID = a.AccountID) As CurrentBalance " &
                              "FROM accountlogin al " &
                              "INNER JOIN account a ON al.AccountID = a.AccountID " &
                              "LEFT JOIN membershiplevel ml ON a.MembershipLevelID = ml.MembershipLevelID " &
                              "LEFT JOIN EntertainmentSession es ON a.AccountID = es.AccountID AND es.Status = 'Active' " &
                              "LEFT JOIN entertainment ent ON es.EntertainmentID = ent.EntertainmentID " &
                              "WHERE al.UserName = @Username LIMIT 1"

        Try
            Dim dt As New DataTable()

            Await Task.Run(Sub()
                               Using conn = DBConnection.GetConnection()
                                   If conn.State <> ConnectionState.Open Then conn.Open()
                                   Using cmd As New MySqlCommand(query, conn)
                                       cmd.Parameters.AddWithValue("@Username", username.Trim())
                                       Using adapter As New MySqlDataAdapter(cmd)
                                           adapter.Fill(dt)
                                       End Using
                                   End Using
                               End Using
                           End Sub)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                Me.SuspendLayout()

                LabelStatus.Text = row("Status").ToString()
                LabelName.Text = row("UserName").ToString()
                LabelTier.Text = row("MembershipLevelName").ToString()

                Dim device As String = row("EntertainmentName").ToString()
                LabelEntertainment.Text = If(String.IsNullOrEmpty(device), "Device: None", device)

                Dim balanceAmt As Decimal = Convert.ToDecimal(row("CurrentBalance"))
                LabelBalance.Tag = balanceAmt
                RenderBalanceUI(balanceAmt)

                If Not IsDBNull(row("LoginTime")) Then
                    _customerLoginTime = Convert.ToDateTime(row("LoginTime"))
                Else
                    _customerLoginTime = Nothing
                    LabelDuration.Text = "00:00:00"
                End If

                Me.ResumeLayout(True)
            End If
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("Error reading user metrics profile: " & ex.Message)
        End Try
    End Function

    Private Sub ClearCustomerUIMetrics()
        Me.SuspendLayout()
        LabelStatus.Text = "Status: --"
        LabelName.Text = "Name: Not Found"
        LabelTier.Text = " --"
        LabelEntertainment.Text = "Device: --"
        LabelDuration.Text = "00:00:00"
        LabelBalance.Text = "₱0.00"
        _customerLoginTime = Nothing
        Me.ResumeLayout(True)
    End Sub

    Private Sub RenderBalanceUI(amt As Decimal)
        If _isBalanceHidden Then
            LabelBalance.Text = "••••••••"
        Else
            LabelBalance.Text = amt.ToString("C2")
        End If
    End Sub

    Private Sub PanelHideBalance_Click(sender As Object, e As EventArgs) Handles PanelHideBalance.Click
        _isBalanceHidden = Not _isBalanceHidden

        If LabelBalance.Tag IsNot Nothing AndAlso TypeOf LabelBalance.Tag Is Decimal Then
            Try

                TableLayoutPanel2.SuspendLayout()


                RenderBalanceUI(CType(LabelBalance.Tag, Decimal))

            Finally

                TableLayoutPanel2.ResumeLayout(True)
            End Try
        End If

    End Sub
#End Region

#Region "Dynamic Data Binding Assemblies"

    Private Async Function LoadRecentActivityAsync(transactionFilter As String) As Task
        Dim query As String = "SELECT wt.WalletTransactionID, wt.EmployeeID, wt.SalesID, wt.TransactionType, wt.TransactionDate, wt.Amount " &
                              "FROM wallettransactions wt " &
                              "INNER JOIN accountlogin al ON wt.AccountID = al.AccountID " &
                              "WHERE al.UserName = @Username "

        If Not String.IsNullOrEmpty(transactionFilter) AndAlso transactionFilter <> "All" Then
            query &= " AND wt.TransactionType = @Type "
        End If

        query &= " ORDER BY wt.TransactionDate DESC"

        Try
            Dim dt As New DataTable()

            Await Task.Run(Sub()
                               Using conn = DBConnection.GetConnection()
                                   If conn.State <> ConnectionState.Open Then conn.Open()
                                   Using cmd As New MySqlCommand(query, conn)
                                       cmd.Parameters.AddWithValue("@Username", AccountData.CustomerUsername.Trim())
                                       If query.Contains("@Type") Then
                                           cmd.Parameters.AddWithValue("@Type", transactionFilter)
                                       End If
                                       Using adapter As New MySqlDataAdapter(cmd)
                                           adapter.Fill(dt)
                                       End Using
                                   End Using
                               End Using
                           End Sub)

            DataGridViewActivity.DataSource = dt
            FormatActivityGrid()
        Catch ex As Exception
            MessageBox.Show("Failed updating transaction log records: " & ex.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Function

    Private Sub FormatActivityGrid()
        Try
            If DataGridViewActivity.Columns.Count > 0 Then
                If DataGridViewActivity.Columns.Contains("WalletTransactionID") Then DataGridViewActivity.Columns("WalletTransactionID").HeaderText = "TXN ID"
                If DataGridViewActivity.Columns.Contains("TransactionType") Then DataGridViewActivity.Columns("TransactionType").HeaderText = "Type"
                If DataGridViewActivity.Columns.Contains("TransactionDate") Then DataGridViewActivity.Columns("TransactionDate").HeaderText = "Date & Time"

                If DataGridViewActivity.Columns.Contains("Amount") Then
                    DataGridViewActivity.Columns("Amount").HeaderText = "Value Amount"
                    DataGridViewActivity.Columns("Amount").DefaultCellStyle.Format = "C2"
                End If

                DataGridViewActivity.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                DataGridViewActivity.AllowUserToAddRows = False
            End If
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("Formatting pass skipped: " & ex.Message)
        End Try
    End Sub

    Private Async Sub ComboboxType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxType.SelectedIndexChanged
        If ComboBoxType.SelectedItem IsNot Nothing Then
            Await LoadRecentActivityAsync(ComboBoxType.SelectedItem.ToString())
        End If
    End Sub
#End Region

#Region "Real-Time Interface Synchronizers"

    Private Async Sub LiveDurationTimer_Tick(sender As Object, e As EventArgs) Handles LiveDurationTimer.Tick
        Try
            ' 1. Pull the fresh user profile data from MySQL every second
            Dim targetedUser As String = AccountData.CustomerUsername
            Await LoadCustomerInformationAsync(targetedUser)

            ' 2. Pull the machine status summary counts from MySQL every second
            _statusRefreshCounter += 1
            If _statusRefreshCounter >= StatusRefreshIntervalSeconds Then
                _statusRefreshCounter = 0
                Await UpdateStatusCountsAsync()
            End If

            Me.SuspendLayout()


            If _customerLoginTime.HasValue Then
                Dim dynamicSpan As TimeSpan = DateTime.Now - _customerLoginTime.Value
                LabelDuration.Text = String.Format("{0:00}:{1:00}:{2:00}", Math.Floor(dynamicSpan.TotalHours), dynamicSpan.Minutes, dynamicSpan.Seconds)
            End If

            If DataGridViewActivity.Rows.Count > 0 AndAlso DataGridViewActivity.Columns.Contains("LoginTime") AndAlso DataGridViewActivity.Columns.Contains("Duration") Then
                For Each row As DataGridViewRow In DataGridViewActivity.Rows
                    If row.Cells("LoginTime").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("LoginTime").Value) Then
                        Dim loginTime As DateTime
                        If DateTime.TryParse(row.Cells("LoginTime").Value.ToString(), loginTime) Then
                            Dim duration As TimeSpan = DateTime.Now - loginTime
                            row.Cells("Duration").Value = String.Format("{0:00}:{1:00}:{2:00}", Math.Floor(duration.TotalHours), duration.Minutes, duration.Seconds)
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            Diagnostics.Debug.WriteLine("Error during live UI loop verification sync: " & ex.Message)
        Finally
            Me.ResumeLayout(True)
        End Try
    End Sub

    Private Async Function UpdateStatusCountsAsync() As Task
        Dim query As String = "SELECT " &
                              "  COUNT(CASE WHEN Status = 'InUse' THEN 1 END) As InUseCount, " &
                              "  COUNT(CASE WHEN Status = 'Available' THEN 1 END) As AvailableCount, " &
                              "  COUNT(CASE WHEN Status = 'Maintenance' THEN 1 END) As MaintenanceCount, " &
                              "  COUNT(*) As TotalCount " &
                              "FROM entertainment"

        Try
            Dim inUse As String = "0"
            Dim available As String = "0"
            Dim maintenance As String = "0"
            Dim total As String = "0"

            Await Task.Run(Sub()
                               Using conn = DBConnection.GetConnection()
                                   If conn.State <> ConnectionState.Open Then conn.Open()
                                   Using cmd As New MySqlCommand(query, conn)
                                       Using reader As MySqlDataReader = cmd.ExecuteReader()
                                           If reader.Read() Then
                                               inUse = reader("InUseCount").ToString()
                                               available = reader("AvailableCount").ToString()
                                               maintenance = reader("MaintenanceCount").ToString()
                                               total = reader("TotalCount").ToString()
                                           End If
                                       End Using
                                   End Using
                               End Using
                           End Sub)

            Me.SuspendLayout()
            LabelInUse.Text = inUse
            LabelAvailable.Text = available
            LabelInMaintenance.Text = maintenance
            LabelTotal.Text = total
            Me.ResumeLayout(True)

        Catch ex As Exception
            Diagnostics.Debug.WriteLine("Error pushing dynamic metrics to dashboard totals: " & ex.Message)
        End Try
    End Function
#End Region

End Class