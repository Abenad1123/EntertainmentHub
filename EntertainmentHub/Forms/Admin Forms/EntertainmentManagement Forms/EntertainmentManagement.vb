Imports MySql.Data.MySqlClient
Public Class EntertainmentManagement
    Private currentTrackedUser As String = ""

    Private Sub EntertainmentManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadInUse()
            UpdateStatusCounts()
            LiveDurationTimer.Interval = 5000
            LiveDurationTimer.Start()
            LoadInUseUsernames()
        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred during form load: " & ex.Message)
        End Try
    End Sub

#Region "Real-Time Timer"
    Private Sub LiveDurationTimer_Tick(sender As Object, e As EventArgs) Handles LiveDurationTimer.Tick
        Try

            If DataGridEntertainment.Rows.Count > 0 AndAlso DataGridEntertainment.Columns.Contains("LoginTime") AndAlso DataGridEntertainment.Columns.Contains("Duration") Then
                For Each row As DataGridViewRow In DataGridEntertainment.Rows
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
            Diagnostics.Debug.WriteLine("Timer Error: " & ex.Message)
        End Try
    End Sub
#End Region
#Region "Event Handlers"

    Private Sub RefreshTrackedUserMetrics()
        ' If no user is being actively tracked, clear dashboard labels and exit
        If String.IsNullOrEmpty(currentTrackedUser) Then
            LabelBalance.Text = "Balance: --"
            LabelEntertainment.Text = "Device: --"
            LabelDuration.Text = "Duration: --"
            Exit Sub
        End If

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                If conn.State <> ConnectionState.Open Then
                    conn.Open()
                End If

                Dim query As String = "SELECT " &
                  "(SELECT COALESCE(SUM(wt.Amount), 0) FROM WalletTransactions wt WHERE wt.AccountID = al.AccountID) As Balance, " &
                  "ent.EntertainmentName, es.LoginTime, es.Status As SessionStatus " &
                  "FROM AccountLogin al " &
                  "LEFT JOIN EntertainmentSession es ON al.AccountID = es.AccountID " &
                  "LEFT JOIN entertainment ent ON es.EntertainmentID = ent.EntertainmentID " &
                  "WHERE al.UserName = @UserName " &
                  "ORDER BY es.LoginTime DESC LIMIT 1"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@UserName", currentTrackedUser.Trim())

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' 1. Safely Parse Balance
                            Dim balance As Decimal = 0D
                            If Not IsDBNull(reader("Balance")) Then
                                balance = Convert.ToDecimal(reader("Balance"))
                            End If
                            LabelBalance.Text = "Balance: " & balance.ToString("C2")

                            ' 2. Safely Parse Session Status
                            Dim sessionStatus As String = ""
                            If Not IsDBNull(reader("SessionStatus")) Then
                                sessionStatus = reader("SessionStatus").ToString()
                            End If

                            ' Check for active session match strings
                            If sessionStatus.Equals("Active", StringComparison.OrdinalIgnoreCase) OrElse
                           sessionStatus.Equals("InUse", StringComparison.OrdinalIgnoreCase) Then

                                Dim deviceName As String = "Unknown"
                                If Not IsDBNull(reader("EntertainmentName")) Then
                                    deviceName = reader("EntertainmentName").ToString()
                                End If
                                LabelEntertainment.Text = "Device: " & deviceName

                                ' 3. Calculate Live Ticking Duration
                                If Not IsDBNull(reader("LoginTime")) Then
                                    Dim loginTime As DateTime = Convert.ToDateTime(reader("LoginTime"))
                                    Dim duration As TimeSpan = DateTime.Now - loginTime

                                    LabelDuration.Text = String.Format("Duration: {0:00}:{1:00}:{2:00}",
                                                            Math.Floor(duration.TotalHours),
                                                            duration.Minutes,
                                                            duration.Seconds)
                                Else
                                    LabelDuration.Text = "Duration: No Login Time"
                                End If
                            Else
                                ' Fallback indicators if a user has no active play session
                                LabelEntertainment.Text = "Device: No Active Session"
                                LabelDuration.Text = "Duration: 00:00:00"
                            End If
                        Else
                            ' User record missing completely from database table 
                            LabelBalance.Text = "Balance: User Not Found"
                            LabelEntertainment.Text = "Device: --"
                            LabelDuration.Text = "Duration: --"
                        End If
                    End Using
                End Using
            Catch ex As MySqlException
                Diagnostics.Debug.WriteLine("Database Error in metrics logic: " & ex.Message)
            Catch ex As Exception
                Diagnostics.Debug.WriteLine("Application Error in metrics logic: " & ex.Message)
            End Try
        End Using
    End Sub
    Private Sub UpdateStatusCounts()
        Using conn = DBConnection.GetConnection()
            Try
                If conn.State <> ConnectionState.Open Then
                    conn.Open()
                End If

                Dim query As String = "SELECT " &
                         "COUNT(CASE WHEN Status = 'InUse' THEN 1 END) As InUseCount, " &
                         "COUNT(CASE WHEN Status = 'Available' THEN 1 END) As AvailableCount, " &
                         "COUNT(CASE WHEN Status = 'Maintenance' THEN 1 END) As MaintenanceCount, " &
                         "COUNT(*) As TotalCount " &
                         "FROM entertainment"

                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then

                            LabelInUse.Text = reader("InUseCount").ToString()
                            LabelAvailable.Text = reader("AvailableCount").ToString()
                            LabelMaintenance.Text = reader("MaintenanceCount").ToString()
                            LabelTotal.Text = reader("TotalCount").ToString()
                        End If
                    End Using
                End Using

            Catch ex As MySqlException
                MessageBox.Show("Database error updating KPI counts: " & ex.Message)
            Catch ex As Exception
                MessageBox.Show("Application error updating KPI counts: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Try
            If ComboBox3.SelectedItem IsNot Nothing Then
                Dim selectedStatus As String = ComboBox3.SelectedItem.ToString()
                Dim targetStatus As String = selectedStatus
                Select Case selectedStatus

                    Case "In Use"
                        LoadInUse()
                    Case "Available"
                        LoadAvailable()
                    Case "In Maintenance"
                        loadInMaintenance()
                    Case Else
                        MessageBox.Show("Invalid status selected.")
                End Select
                LoadEntertainmentNamesByStatus(selectedStatus)
            End If
        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred in ComboBox3 selection: " & ex.Message)
        End Try
    End Sub


    Private Sub ComboBoxUsername_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBoxUsername.SelectionChangeCommitted
        Try
            If ComboBoxUsername.SelectedItem IsNot Nothing Then
                currentTrackedUser = ComboBoxUsername.SelectedItem.ToString()
                RefreshTrackedUserMetrics()
            End If
        Catch ex As Exception
            MessageBox.Show("Error tracking selected user: " & ex.Message)
        End Try
    End Sub



    Private Sub FormatDataGridView()
        Try

            If DataGridEntertainment.Columns.Count > 0 Then

                DataGridEntertainment.Columns("EntertainmentName").HeaderText = "Entertainment Name"
                DataGridEntertainment.Columns("HourlyRate").HeaderText = "Hourly Rate"
                DataGridEntertainment.Columns("Status").HeaderText = "Current Status"

                If DataGridEntertainment.Columns.Contains("UserName") Then
                    DataGridEntertainment.Columns("UserName").HeaderText = "Logged In User"
                End If


                If DataGridEntertainment.Columns.Contains("Duration") Then
                    DataGridEntertainment.Columns("Duration").HeaderText = "Live Duration"
                    DataGridEntertainment.Columns("Duration").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If

                DataGridEntertainment.Columns("HourlyRate").DefaultCellStyle.Format = "C2"


                DataGridEntertainment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                DataGridEntertainment.AllowUserToAddRows = False

            End If
        Catch ex As Exception
            MessageBox.Show("Error formatting grid view: " & ex.Message)
        End Try
    End Sub

    Private Sub FilterDataGridByEntertainmentName(entertainmentName As String)
        Try

            If DataGridEntertainment.DataSource IsNot Nothing AndAlso TypeOf DataGridEntertainment.DataSource Is DataTable Then
                Dim dt As DataTable = CType(DataGridEntertainment.DataSource, DataTable)
                Dim dv As New DataView(dt)

                dv.RowFilter = String.Format("EntertainmentName = '{0}'", entertainmentName.Replace("'", "''"))


                DataGridEntertainment.DataSource = dv
            Else

            End If
        Catch ex As Exception
            MessageBox.Show("Error filtering data grid: " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Panels"
    Private Sub PanelShutdown_click(sender As Object, e As EventArgs) Handles PanelShutdown.Click
        Try
            If ComboBoxEntertainment.SelectedItem IsNot Nothing Then
                UpdateEntertainmentStatus(ComboBoxEntertainment.SelectedItem.ToString(), "Shutdown")
            Else
                MessageBox.Show("Please select an entertainment item first.")
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred performing shutdown action: " & ex.Message)
        End Try
    End Sub

    Private Sub PanelMaintenance_click(sender As Object, e As EventArgs) Handles PanelMaintenance.Click
        Try
            If ComboBoxEntertainment.SelectedItem IsNot Nothing Then
                UpdateEntertainmentStatus(ComboBoxEntertainment.SelectedItem.ToString(), "Maintenance")
            Else
                MessageBox.Show("Please select an entertainment item first.")
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred performing maintenance action: " & ex.Message)
        End Try
    End Sub

    Private Sub PanelAvailable_click(sender As Object, e As EventArgs) Handles PanelAvailable.Click
        Try
            If ComboBoxEntertainment.SelectedItem IsNot Nothing Then
                UpdateEntertainmentStatus(ComboBoxEntertainment.SelectedItem.ToString(), "Available")
            Else
                MessageBox.Show("Please select an entertainment item first.")
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred making the item available: " & ex.Message)
        End Try
    End Sub
#End Region

#Region "Initialize Entertainment Configuration"

    Private Sub LoadInUse()
        Using conn = DBConnection.GetConnection()
            Try
                If conn.State <> ConnectionState.Open Then
                    conn.Open()
                End If


                Dim query As String = "SELECT ent.EntertainmentName, entt.HourlyRate, ent.Status, al.UserName, es.LoginTime, " &
                   "'' As Duration " &
                   "FROM entertainment ent " &
                   "INNER JOIN EntertainmentTier entt ON ent.EntertainmentTierID = entt.EntertainmentTierID " &
                   "LEFT JOIN EntertainmentSession es ON ent.EntertainmentID = es.EntertainmentID AND es.Status = 'Active' " &
                   "LEFT JOIN AccountLogin al ON es.AccountID = al.AccountID " &
                   "WHERE ent.Status = 'InUse'"

                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridEntertainment.DataSource = dt
                End Using

                FormatDataGridView()
                UpdateStatusCounts()
            Catch ex As MySqlException
                MessageBox.Show("Database error occurred while loading in-use entertainment: " & ex.Message)
            Catch ex As Exception
                MessageBox.Show("Application error while loading in-use entertainment: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadAvailable()
        Using conn = DBConnection.GetConnection()
            Try
                If conn.State <> ConnectionState.Open Then
                    conn.Open()
                End If

                Dim query As String = "SELECT ent.EntertainmentName, entt.HourlyRate, ent.Status " &
                           "FROM entertainment ent " &
                           "INNER JOIN EntertainmentTier entt ON ent.EntertainmentTierID = entt.EntertainmentTierID " &
                           "WHERE Status = 'Available'"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridEntertainment.DataSource = dt
                End Using

                FormatDataGridView()
                UpdateStatusCounts()

            Catch ex As MySqlException
                MessageBox.Show("Database error occurred while loading available entertainment: " & ex.Message)
            Catch ex As Exception
                MessageBox.Show("Application error while loading available entertainment: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub loadInMaintenance()
        Using conn = DBConnection.GetConnection()
            Try
                If conn.State <> ConnectionState.Open Then
                    conn.Open()
                End If

                Dim query As String = "SELECT ent.EntertainmentName, entt.HourlyRate, ent.Status " &
                           "FROM entertainment ent " &
                           "INNER JOIN EntertainmentTier entt ON ent.EntertainmentTierID = entt.EntertainmentTierID " &
                           "WHERE Status = 'Maintenance'"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridEntertainment.DataSource = dt
                End Using

                FormatDataGridView()
                UpdateStatusCounts()
            Catch ex As MySqlException
                MessageBox.Show("Database error occurred while loading in-maintenance entertainment: " & ex.Message)
            Catch ex As Exception
                MessageBox.Show("Application error while loading in-maintenance entertainment: " & ex.Message)
            End Try
        End Using
    End Sub

#End Region

#Region "Data Synchronization and Actions"

    Private Sub LoadInUseUsernames()
        ' 1. Remember what user was selected right before the clear step
        Dim savedSelectedUser As String = ""
        If ComboBoxUsername.SelectedItem IsNot Nothing Then
            savedSelectedUser = ComboBoxUsername.SelectedItem.ToString()
        End If

        ComboBoxUsername.Items.Clear()

        Using conn = DBConnection.GetConnection()
            Try
                If conn.State <> ConnectionState.Open Then
                    conn.Open()
                End If

                Dim query As String = "SELECT DISTINCT al.UserName " &
                                 "FROM EntertainmentSession es " &
                                 "INNER JOIN AccountLogin al ON es.AccountID = al.AccountID " &
                                 "WHERE es.Status = 'Active'"

                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            If Not IsDBNull(reader("UserName")) Then
                                ComboBoxUsername.Items.Add(reader("UserName").ToString())
                            End If
                        End While
                    End Using
                End Using

                ' 2. Restore their selected choice seamlessly if it still exists in the new list
                If Not String.IsNullOrEmpty(savedSelectedUser) AndAlso ComboBoxUsername.Items.Contains(savedSelectedUser) Then
                    ComboBoxUsername.SelectedItem = savedSelectedUser
                End If

            Catch ex As MySqlException
                MessageBox.Show("Database error loading active usernames: " & ex.Message)
            Catch ex As Exception
                MessageBox.Show("Application error loading active usernames: " & ex.Message)
            End Try
        End Using
    End Sub
    Private Sub LoadEntertainmentNamesByStatus(status As String)
        Dim dbStatus As String = status
        If dbStatus = "In Use" Then dbStatus = "InUse"
        If dbStatus = "In Maintenance" Then dbStatus = "Maintenance"

        ComboBoxEntertainment.Items.Clear()

        Using conn = DBConnection.GetConnection()
            Try
                If conn.State <> ConnectionState.Open Then
                    conn.Open()
                End If

                Dim query As String = "SELECT EntertainmentName FROM entertainment WHERE Status = @Status"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Status", dbStatus)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            ComboBoxEntertainment.Items.Add(reader("EntertainmentName").ToString())
                        End While
                    End Using
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Database error mapping names to ComboBoxEntertainment: " & ex.Message)
            Catch ex As Exception
                MessageBox.Show("Application error mapping names to ComboBoxEntertainment: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub UpdateEntertainmentStatus(entertainmentName As String, newStatus As String)
        Using conn = DBConnection.GetConnection()
            Try
                If conn.State <> ConnectionState.Open Then
                    conn.Open()
                End If

                Dim query As String = "UPDATE entertainment SET Status = @Status WHERE EntertainmentName = @EntertainmentName"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Status", newStatus)
                    cmd.Parameters.AddWithValue("@EntertainmentName", entertainmentName)

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        MessageBox.Show("Status updated successfully to " & newStatus)
                        UpdateStatusCounts()
                        If ComboBox3.SelectedItem IsNot Nothing Then
                            ComboBox3_SelectedIndexChanged(ComboBox3, EventArgs.Empty)
                        End If
                    Else
                        MessageBox.Show("No target entertainment found to update.")
                    End If
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Database error updating status: " & ex.Message)
            Catch ex As Exception
                MessageBox.Show("Application error updating status: " & ex.Message)
            End Try


        End Using
    End Sub





    Private Sub LabelBalance_Click(sender As Object, e As EventArgs) Handles LabelBalance.Click
        RefreshTrackedUserMetrics()
    End Sub

    Private Sub LabelEntertainment_Click(sender As Object, e As EventArgs) Handles LabelEntertainment.Click
        RefreshTrackedUserMetrics()
    End Sub

    Private Sub LabelDuration_Click(sender As Object, e As EventArgs) Handles LabelDuration.Click
        RefreshTrackedUserMetrics()
    End Sub





#End Region

End Class