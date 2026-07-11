Imports MySql.Data.MySqlClient
Imports System.Drawing
    Imports System.Resources

Public Class EntertainmentCustomer
    ' Core state identifiers resolved directly through your AccountData module
    Private currentTrackedUser As String = ""
    Private currentAccountID As Integer = 0
    Private activeSessionID As Integer = 0
    Private activeEntertainmentID As Integer = 0
    Private currentActiveLoginTime As DateTime

    ' Aesthetic Hex styling codes (#2D2D30 background with Vibrant Lime Green accents)
    Private ReadOnly ColorCharcoal As Color = ColorTranslator.FromHtml("#2D2D30")
    Private ReadOnly ColorLimeGreen As Color = ColorTranslator.FromHtml("#32CD32")
    Private ReadOnly ColorCardBg As Color = ColorTranslator.FromHtml("#1E1E1E")

    Private Sub CustomerSideSession_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Explicitly bind state strings from your dynamic system module
            currentTrackedUser = AccountData.CustomerUsername

            ' Enforce core interface styling architecture guidelines
            Me.BackColor = ColorCharcoal
            FlowEntertainmentCards.BackColor = ColorCharcoal

            ' Resolve Account ID structural identity contexts safely
            FetchAccountContextByUsername()

            ' Core data operations pipeline
            LoadInUse()
            UpdateStatusCounts()

            ' Configure and initialize standard layout metrics timers (1-second intervals)
            LiveDurationTimer.Interval = 1000
            LiveDurationTimer.Start()

            LoadInUseUsernames()
            RefreshTrackedUserMetrics()
            RenderEntertainmentCards()
        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred during form load: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FetchAccountContextByUsername()
        If String.IsNullOrEmpty(currentTrackedUser) Then Exit Sub

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Dim query As String = "SELECT AccountID FROM AccountLogin WHERE UserName = @UserName LIMIT 1;"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@UserName", currentTrackedUser.Trim())
                Try
                    If conn.State <> ConnectionState.Open Then conn.Open()
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        currentAccountID = Convert.ToInt32(result)
                    End If
                Catch ex As Exception
                    Diagnostics.Debug.WriteLine("Error resolving AccountID context: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

#Region "FUNCTION 1: VIEW ENTERTAINMENTS WITH TIER-BASED IMAGE GROUPING"
    Private Sub RenderEntertainmentCards()
        ' Intercept control flickering arrays during active UI loops
        FlowEntertainmentCards.SuspendLayout()
        FlowEntertainmentCards.Controls.Clear()

        Dim query As String = "SELECT e.EntertainmentID, e.EntertainmentTierID, e.EntertainmentName, e.Status, t.HourlyRate, t.EntertainmentTierName " &
                              "FROM entertainment e " &
                              "INNER JOIN entertainmenttier t ON e.EntertainmentTierID = t.EntertainmentTierID " &
                              "ORDER BY e.EntertainmentID ASC;"

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Using cmd As New MySqlCommand(query, conn)
                Try
                    If conn.State <> ConnectionState.Open Then conn.Open()
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim id As Integer = Convert.ToInt32(reader("EntertainmentID"))
                            Dim tierId As Integer = Convert.ToInt32(reader("EntertainmentTierID"))
                            Dim name As String = reader("EntertainmentName").ToString()
                            Dim status As String = reader("Status").ToString()
                            Dim rate As Decimal = Convert.ToDecimal(reader("HourlyRate"))
                            Dim tierName As String = reader("EntertainmentTierName").ToString()

                            ' --- STEP 1: DYNAMIC PHYSICAL DISK FILE PATH LOOKUP LOOP ---
                            ' Automatically maps all records with matching Tier IDs to "Assets/Tiers/tier_X.png"
                            Dim assetFolderName As String = IO.Path.Combine(Application.StartupPath, "Assets", "Images")
                            Dim targetFileName As String = $"tier_{tierId}.png"
                            Dim fullImagePath As String = IO.Path.Combine(assetFolderName, targetFileName)

                            Dim deviceImage As Image = Nothing

                            Try
                                ' Verify the file physically exists on disk before attempting to initialize memory handlers
                                If IO.File.Exists(fullImagePath) Then
                                    ' Using FromStream instead of FromFile prevents the file lock from blocking write updates
                                    Using fs As New IO.FileStream(fullImagePath, IO.FileMode.Open, IO.FileAccess.Read)
                                        deviceImage = Image.FromStream(fs)
                                    End Using
                                End If
                            Catch ex As Exception
                                Diagnostics.Debug.WriteLine($"Physical asset read failure for {targetFileName}: {ex.Message}")
                            End Try

                            ' Fallback generic block preview graphic if the file doesn't exist or crashes on read
                            If deviceImage Is Nothing Then
                                Dim bmp As New Bitmap(200, 90)
                                Using g As Graphics = Graphics.FromImage(bmp)
                                    g.Clear(Color.FromArgb(40, 40, 40))
                                    Using font As New Font("Segoe UI", 8.0F)
                                        g.DrawString("[Missing Image File]", font, Brushes.Gray, New PointF(45, 35))
                                    End Using
                                End Using
                                deviceImage = bmp
                            End If

                            ' --- BUILD CARD PANEL & COMPONENT HIERARCHY ---
                            Dim cardPanel As New Panel With {
                                .Size = New Size(220, 260), ' Expanded height layout to cleanly present images
                                .BackColor = ColorCardBg,
                                .Margin = New Padding(12),
                                .BorderStyle = BorderStyle.FixedSingle,
                                .Tag = id
                            }

                            ' Shared Tier Image Element Box
                            Dim picBox As New PictureBox With {
                                .Size = New Size(200, 90),
                                .Location = New Point(10, 10),
                                .Image = deviceImage,
                                .SizeMode = PictureBoxSizeMode.Zoom,
                                .BackColor = Color.Black
                            }

                            Dim lblName As New Label With {
                                .Text = name,
                                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
                                .ForeColor = Color.White,
                                .Location = New Point(10, 110),
                                .Size = New Size(200, 22)
                            }

                            Dim lblTier As New Label With {
                                .Text = $"{tierName} - {rate:C2}/hr",
                                .Font = New Font("Segoe UI", 9.0F, FontStyle.Regular),
                                .ForeColor = Color.LightGray,
                                .Location = New Point(10, 135),
                                .Size = New Size(200, 18)
                            }

                            Dim lblStatus As New Label With {
                                .Text = $"Status: {status}",
                                .Font = New Font("Segoe UI", 9.0F, FontStyle.Italic),
                                .Location = New Point(10, 160),
                                .Size = New Size(200, 18)
                            }

                            Dim btnAction As New Button With {
                                .Size = New Size(200, 35),
                                .Location = New Point(10, 210),
                                .FlatStyle = FlatStyle.Flat,
                                .Font = New Font("Segoe UI", 9.5F, FontStyle.Bold),
                                .Tag = New With {.ID = id, .Rate = rate, .Name = name}
                            }
                            btnAction.FlatAppearance.BorderSize = 1

                            ' --- APPLY CONDITIONAL UI STATE ACCENTS ---
                            Select Case status.ToLower()
                                Case "available"
                                    lblStatus.ForeColor = ColorLimeGreen
                                    btnAction.Text = "START SESSION"
                                    btnAction.BackColor = ColorCharcoal
                                    btnAction.ForeColor = ColorLimeGreen
                                    btnAction.FlatAppearance.BorderColor = ColorLimeGreen
                                    btnAction.Enabled = (activeSessionID = 0)
                                    AddHandler btnAction.Click, AddressOf BtnStartSession_Click

                                Case "inuse"
                                    lblStatus.ForeColor = Color.Orange
                                    If activeEntertainmentID = id Then
                                        btnAction.Text = "END SESSION"
                                        btnAction.BackColor = ColorLimeGreen
                                        btnAction.ForeColor = Color.Black
                                        btnAction.FlatAppearance.BorderColor = ColorLimeGreen
                                        AddHandler btnAction.Click, AddressOf BtnEndSession_Click
                                    Else
                                        btnAction.Text = "OCCUPIED"
                                        btnAction.BackColor = Color.FromArgb(45, 45, 48)
                                        btnAction.ForeColor = Color.DarkGray
                                        btnAction.FlatAppearance.BorderColor = Color.DimGray
                                        btnAction.Enabled = False
                                    End If

                                Case "maintenance"
                                    lblStatus.ForeColor = Color.Crimson
                                    btnAction.Text = "UNAVAILABLE"
                                    btnAction.BackColor = Color.FromArgb(30, 30, 30)
                                    btnAction.ForeColor = Color.DarkGray
                                    btnAction.FlatAppearance.BorderColor = Color.Crimson
                                    btnAction.Enabled = False
                            End Select

                            ' Bind components layout together
                            cardPanel.Controls.Add(picBox)
                            cardPanel.Controls.Add(lblName)
                            cardPanel.Controls.Add(lblTier)
                            cardPanel.Controls.Add(lblStatus)
                            cardPanel.Controls.Add(btnAction)

                            FlowEntertainmentCards.Controls.Add(cardPanel)
                        End While
                    End Using
                Catch ex As MySqlException
                    MessageBox.Show("Database access failure compiling asset layouts: " & ex.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End Using
        End Using
        FlowEntertainmentCards.ResumeLayout()
    End Sub
#End Region

#Region "FUNCTION 2: RENT ENTERTAINMENT (START SESSION)"
    Private Sub BtnStartSession_Click(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        Dim targetAsset = DirectCast(btn.Tag, Object)
        Dim entID As Integer = targetAsset.ID

        If String.IsNullOrEmpty(AccountData.CustomerUsername) Then
            MessageBox.Show("No active customer context loaded. Action canceled.", "Security Guard", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If activeSessionID <> 0 Then
            MessageBox.Show("You already have an active open rental session running.", "Operation Guard", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using conn As MySqlConnection = DBConnection.GetConnection()
            If conn.State <> ConnectionState.Open Then conn.Open()

            Using tx As MySqlTransaction = conn.BeginTransaction()
                Try
                    ' 1. Concurrency row-level pessimistic locking protection
                    Dim checkQuery As String = "SELECT Status FROM entertainment WHERE EntertainmentID = @EntID FOR UPDATE;"
                    Using cmdCheck As New MySqlCommand(checkQuery, conn, tx)
                        cmdCheck.Parameters.AddWithValue("@EntID", entID)
                        Dim dynamicStatus As String = Convert.ToString(cmdCheck.ExecuteScalar())
                        If Not "Available".Equals(dynamicStatus, StringComparison.OrdinalIgnoreCase) Then
                            Throw New Exception("Target device is no longer marked available.")
                        End If
                    End Using

                    ' 2. Update status mapping to 'InUse'
                    Dim updateAssetQuery As String = "UPDATE entertainment SET Status = 'InUse' WHERE EntertainmentID = @EntID;"
                    Using cmdUpdate = New MySqlCommand(updateAssetQuery, conn, tx)
                        cmdUpdate.Parameters.AddWithValue("@EntID", entID)
                        cmdUpdate.ExecuteNonQuery()
                    End Using

                    ' 3. Insert transaction log context record
                    Dim insertSessionQuery As String = "INSERT INTO entertainmentsession (AccountID, EntertainmentID, LoginTime, Status, RateApplied) " &
                                                       "VALUES (@AccountID, @EntID, NOW(), 'Active', @RateApplied);"
                    Using cmdInsert = New MySqlCommand(insertSessionQuery, conn, tx)
                        cmdInsert.Parameters.AddWithValue("@AccountID", currentAccountID)
                        cmdInsert.Parameters.AddWithValue("@EntID", entID)
                        cmdInsert.Parameters.AddWithValue("@RateApplied", targetAsset.Rate)
                        cmdInsert.ExecuteNonQuery()

                        activeSessionID = Convert.ToInt32(cmdInsert.LastInsertedId)
                    End Using

                    tx.Commit()

                    activeEntertainmentID = entID
                    currentActiveLoginTime = DateTime.Now

                    MessageBox.Show($"Session initialized for device: {targetAsset.Name}!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    RefreshTrackedUserMetrics()
                    RenderEntertainmentCards()

                Catch ex As Exception
                    tx.Rollback()
                    MessageBox.Show("Transaction failure. Action aborted: " & ex.Message, "Rollback Executed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
#End Region

#Region "FUNCTION 3: VIEW ACTIVE SESSION TIMER"
    Private Sub LiveDurationTimer_Tick(sender As Object, e As EventArgs) Handles LiveDurationTimer.Tick
        Try
            ' Section A: DataGrid rows live computation update sweeps safely
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

            ' Section B: Main runtime dashboard profile metrics ticker updates
            If activeSessionID <> 0 Then
                Dim delta As TimeSpan = DateTime.Now - currentActiveLoginTime
                LabelDuration.Text = String.Format("Duration: {0:00}:{1:00}:{2:00}", Math.Floor(delta.TotalHours), delta.Minutes, delta.Seconds)
            End If
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("Timer Delta Calculation Error: " & ex.Message)
        End Try
    End Sub
#End Region

#Region "FUNCTION 4: END ACTIVE SESSION"
    Private Sub BtnEndSession_Click(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        Dim targetAsset = DirectCast(btn.Tag, Object)

        LiveDurationTimer.Stop()

        Dim endTime As DateTime = DateTime.Now
        Dim billingSpan As TimeSpan = endTime - currentActiveLoginTime

        Dim calculatedTotalHours As Double = billingSpan.TotalHours
        If calculatedTotalHours < (1.0 / 60.0) Then calculatedTotalHours = 1.0 / 60.0

        Dim ratePerHour As Decimal = Convert.ToDecimal(targetAsset.Rate)
        Dim calculatedCost As Decimal = Math.Round(Convert.ToDecimal(calculatedTotalHours) * ratePerHour, 2)

        Dim confirmationText As String = $"Are you sure you want to end your session?{Environment.NewLine}" &
                                         $"Elapsed Time: {Math.Floor(billingSpan.TotalHours):00}:{billingSpan.Minutes:00}:{billingSpan.Seconds:00}{Environment.NewLine}" &
                                         $"Total Due: {calculatedCost:C2}"

        If MessageBox.Show(confirmationText, "Confirm System Checkout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            LiveDurationTimer.Start()
            Exit Sub
        End If

        Using conn As MySqlConnection = DBConnection.GetConnection()
            If conn.State <> ConnectionState.Open Then conn.Open()
            Using tx As MySqlTransaction = conn.BeginTransaction()
                Try
                    ' Step 1: Update log configurations for the active row block
                    Dim updateSessionQuery As String = "UPDATE entertainmentsession SET LogoutTime = @LogoutTime, Status = 'Completed', RateApplied = @Rate " &
                                                       "WHERE EntertainmentSessionID = @SessionID;"
                    Using cmdSession = New MySqlCommand(updateSessionQuery, conn, tx)
                        cmdSession.Parameters.AddWithValue("@LogoutTime", endTime)
                        cmdSession.Parameters.AddWithValue("@Rate", ratePerHour)
                        cmdSession.Parameters.AddWithValue("@SessionID", activeSessionID)
                        cmdSession.ExecuteNonQuery()
                    End Using

                    ' Step 2: Return status mapping of device to 'Available'
                    Dim updateAssetQuery As String = "UPDATE entertainment SET Status = 'Available' WHERE EntertainmentID = @EntID;"
                    Using cmdAsset = New MySqlCommand(updateAssetQuery, conn, tx)
                        cmdAsset.Parameters.AddWithValue("@EntID", activeEntertainmentID)
                        cmdAsset.ExecuteNonQuery()
                    End Using

                    ' Step 3: Insert negative signature notation balance transaction record
                    Dim insertTransactionQuery As String = "INSERT INTO wallettransactions (EmployeeID, EntertainmentSessionID, AccountID, Amount, TransactionType, TransactionDate) " &
                                                           "VALUES (@EmployeeID, @SessionID, @AccountID, @Amount, 'Payment', @TxDate);"
                    Using cmdTx = New MySqlCommand(insertTransactionQuery, conn, tx)
                        cmdTx.Parameters.AddWithValue("@EmployeeID", DBNull.Value)
                        cmdTx.Parameters.AddWithValue("@SessionID", activeSessionID)
                        cmdTx.Parameters.AddWithValue("@AccountID", currentAccountID)
                        cmdTx.Parameters.AddWithValue("@Amount", -calculatedCost) ' Absolute negative signature matched
                        cmdTx.Parameters.AddWithValue("@TxDate", endTime)
                        cmdTx.ExecuteNonQuery()
                    End Using

                    tx.Commit()

                    ' Post dynamic session receipt string logs out to external global state tracking structures
                    AccountData.ReceiptLog = $"Session ID: {activeSessionID} | Total Paid: {calculatedCost:C2}"

                    activeSessionID = 0
                    activeEntertainmentID = 0

                    MessageBox.Show($"Checkout completed successfully. Amount deducted: {calculatedCost:C2}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    tx.Rollback()
                    MessageBox.Show("Critical transactional checkout processing crash occurred. Reverted." & Environment.NewLine & ex.Message, "Transaction Crash Guard", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using

        LiveDurationTimer.Start()
        RefreshTrackedUserMetrics()
        RenderEntertainmentCards()
    End Sub
#End Region

#Region "PRESERVED HOOK INTERFACE METHOD OUTLINES"
    Private Sub LoadInUse()
    End Sub
    Private Sub UpdateStatusCounts()
    End Sub
    Private Sub LoadInUseUsernames()
    End Sub
#End Region

#Region "Event Handlers (Dynamic State Synchronizer Engines)"
    Private Sub RefreshTrackedUserMetrics()
        currentTrackedUser = AccountData.CustomerUsername

        If String.IsNullOrEmpty(currentTrackedUser) Then
            LabelBalance.Text = "Balance: --"
            LabelEntertainment.Text = "Device: --"
            LabelDuration.Text = "Duration: --"
            Exit Sub
        End If

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                If conn.State <> ConnectionState.Open Then conn.Open()

                Dim query As String = "SELECT " &
                  "(SELECT COALESCE(SUM(wt.Amount), 0) FROM wallettransactions wt WHERE wt.AccountID = al.AccountID) As Balance, " &
                  "ent.EntertainmentID, ent.EntertainmentName, es.EntertainmentSessionID, es.LoginTime, es.Status As SessionStatus " &
                  "FROM AccountLogin al " &
                  "LEFT JOIN entertainmentsession es ON al.AccountID = es.AccountID AND es.Status = 'Active' " &
                  "LEFT JOIN entertainment ent ON es.EntertainmentID = ent.EntertainmentID " &
                  "WHERE al.UserName = @UserName " &
                  "ORDER BY es.LoginTime DESC LIMIT 1"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@UserName", currentTrackedUser.Trim())
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim balance As Decimal = If(Not IsDBNull(reader("Balance")), Convert.ToDecimal(reader("Balance")), 0D)
                            LabelBalance.ForeColor = ColorLimeGreen
                            LabelBalance.Text = "Balance: " & balance.ToString("C2")

                            Dim sessionStatus As String = If(Not IsDBNull(reader("SessionStatus")), reader("SessionStatus").ToString(), "")
                            If sessionStatus.Equals("Active", StringComparison.OrdinalIgnoreCase) Then
                                activeSessionID = Convert.ToInt32(reader("EntertainmentSessionID"))
                                activeEntertainmentID = Convert.ToInt32(reader("EntertainmentID"))

                                Dim deviceName As String = If(Not IsDBNull(reader("EntertainmentName")), reader("EntertainmentName").ToString(), "Unknown")
                                LabelEntertainment.Text = "Device: " & deviceName

                                If Not IsDBNull(reader("LoginTime")) Then
                                    currentActiveLoginTime = Convert.ToDateTime(reader("LoginTime"))
                                    Dim duration As TimeSpan = DateTime.Now - currentActiveLoginTime
                                    LabelDuration.Text = String.Format("Duration: {0:00}:{1:00}:{2:00}", Math.Floor(duration.TotalHours), duration.Minutes, duration.Seconds)
                                Else
                                    LabelDuration.Text = "Duration: No Login Time"
                                End If
                            Else
                                LabelEntertainment.Text = "Device: No Active Session"
                                LabelDuration.Text = "Duration: 00:00:00"
                            End If
                        Else
                            LabelBalance.Text = "Balance: User Not Found"
                            LabelEntertainment.Text = "Device: --"
                            LabelDuration.Text = "Duration: --"
                        End If
                    End Using
                End Using
            Catch ex As Exception
                Diagnostics.Debug.WriteLine("Error in metrics dashboard lookup: " & ex.Message)
            End Try
        End Using
    End Sub




#End Region
End Class