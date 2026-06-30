Imports MySql.Data.MySqlClient

Public Class EntertainmentConfiguration

    Public selectedEntertainmentName As String
    Public status As String

    Private Sub InitializatizeEntertainment() Handles MyBase.Load
        LoadInUse()
        LoadAvailable()
        loadInMaintenance()
    End Sub

    Private Sub RefreshGrids()
        DataGridAvailable.DataSource = Nothing
        DataGridInMaintenance.DataSource = Nothing
        DataGridInUse.DataSource = Nothing

        LoadAvailable()
        loadInMaintenance()
        LoadInUse()

        ' Clear selection variables after refresh to avoid accidental toggles
        selectedEntertainmentName = ""
        status = ""
    End Sub


#Region "Grid Cell Click Handlers"
    ' Handles row selection for the Available Grid
    Private Sub DataGridAvailable_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridAvailable.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridAvailable.Rows(e.RowIndex)
            If row.Cells("EntertainmentName").Value IsNot DBNull.Value AndAlso row.Cells("Status").Value.ToString() = "Available" Then
                selectedEntertainmentName = row.Cells("EntertainmentName").Value.ToString()
                status = "Available"
            End If
        End If
    End Sub

    ' Handles row selection for the Maintenance Grid
    Private Sub DataGridInMaintenance_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridInMaintenance.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridInMaintenance.Rows(e.RowIndex)
            If row.Cells("EntertainmentName").Value IsNot DBNull.Value AndAlso row.Cells("Status").Value.ToString() = "Maintenance" Then
                selectedEntertainmentName = row.Cells("EntertainmentName").Value.ToString()
                status = "Maintenance"
            End If
        End If
    End Sub

    ' UPDATED: Handles row selection for the In Use Grid and automatically populates TextBox1
    Private Sub DataGridInUse_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridInUse.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridInUse.Rows(e.RowIndex)
            If row.Cells("Status").Value IsNot DBNull.Value AndAlso row.Cells("Status").Value.ToString() = "InUse" Then
                selectedEntertainmentName = row.Cells("EntertainmentName").Value.ToString()
                status = "InUse"

                ' Automatically fill TextBox1 with the active username from the row clicked
                If row.Cells("UserName").Value IsNot DBNull.Value Then
                    TextBox1.Text = row.Cells("UserName").Value.ToString()
                End If
            End If
        End If
    End Sub

#End Region

#Region "Initialize Entertainment Configuration"

    ' UPDATED: Modified query to join with active sessions and display the occupying customer's username
    Private Sub LoadInUse()
        Using conn = DBConnection.GetConnection()
            Try
                Dim query As String = "SELECT ent.EntertainmentName, entt.HourlyRate, ent.Status, al.UserName " &
                                       "FROM entertainment ent " &
                                       "INNER JOIN EntertainmentTier entt ON ent.EntertainmentTierID = entt.EntertainmentTierID " &
                                       "LEFT JOIN EntertainmentSession es ON ent.EntertainmentID = es.EntertainmentID AND es.Status = 'Active' " &
                                       "LEFT JOIN AccountLogin al ON es.AccountID = al.AccountID " &
                                       "WHERE ent.Status = 'InUse'"

                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridInUse.DataSource = dt
                End Using

                If DataGridInUse.Columns.Count > 0 Then
                    DataGridInUse.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End If
                If DataGridInUse.Columns.Contains("HourlyRate") Then
                    DataGridInUse.Columns("HourlyRate").DefaultCellStyle.Format = "C2"
                End If

            Catch ex As MySqlException
                MessageBox.Show("Error occurred while loading in-use entertainment: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadAvailable()
        Using conn = DBConnection.GetConnection()
            Try
                Dim query As String = "SELECT ent.EntertainmentName, entt.HourlyRate, ent.Status " &
                                       "FROM entertainment ent " &
                                       "INNER JOIN EntertainmentTier entt ON ent.EntertainmentTierID = entt.EntertainmentTierID " &
                                       "WHERE Status = 'Available'"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridAvailable.DataSource = dt
                End Using

                If DataGridAvailable.Columns.Count > 0 Then
                    DataGridAvailable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End If
                If DataGridAvailable.Columns.Contains("HourlyRate") Then
                    DataGridAvailable.Columns("HourlyRate").DefaultCellStyle.Format = "C2"
                End If

            Catch ex As MySqlException
                MessageBox.Show("Error occurred while loading available entertainment: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub loadInMaintenance()
        Using conn = DBConnection.GetConnection()
            Try
                Dim query As String = "SELECT ent.EntertainmentName, entt.HourlyRate, ent.Status " &
                                       "FROM entertainment ent " &
                                       "INNER JOIN EntertainmentTier entt ON ent.EntertainmentTierID = entt.EntertainmentTierID " &
                                       "WHERE Status = 'Maintenance'"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridInMaintenance.DataSource = dt
                End Using

                If DataGridInMaintenance.Columns.Count > 0 Then
                    DataGridInMaintenance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End If
                If DataGridInMaintenance.Columns.Contains("HourlyRate") Then
                    DataGridInMaintenance.Columns("HourlyRate").DefaultCellStyle.Format = "C2"
                End If

            Catch ex As MySqlException
                MessageBox.Show("Error occurred while loading in-maintenance entertainment: " & ex.Message)
            End Try
        End Using
    End Sub

#End Region

#Region "Grid Cell Click Handlers Change Status"
    Private Sub Panel_ChangeStatus_click(sender As Object, e As EventArgs) Handles Panel_ChangeStatus.Click

        ' Siguraduhing may napiling item bago magpatuloy
        If String.IsNullOrEmpty(selectedEntertainmentName) Then
            MessageBox.Show("Choose an entertainment item.", "CAUTION", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim newStatus As String = ""

        ' Pagpasyahan ang bagong status depende sa kung ano ang kasalukuyang pinindot
        If status = "Maintenance" Then
            newStatus = "Available" ' Capitalized to ensure consistent database casing
        ElseIf status = "Available" Then
            newStatus = "Maintenance"
        Else
            ' Kung galing sa 'InUse', block status switches until manual off is used
            MessageBox.Show("This device is currently In Use. Please use Manual Off to clear sessions first.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim query As String = "UPDATE Entertainment SET Status = @Status WHERE EntertainmentName = @EntertainmentName;"

        Using conn = DBConnection.GetConnection()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@EntertainmentName", selectedEntertainmentName)
                cmd.Parameters.AddWithValue("@Status", newStatus)

                Try
                    conn.Open()
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show($"Status changed to {newStatus}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Catch ex As MySqlException
                    MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using

        RefreshGrids()
    End Sub
#End Region

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
    End Sub

    Private Sub Panel_ToRent_Click(sender As Object, e As EventArgs) Handles Panel_ToRent.Click
        Dim targetUsername As String = TextBox1.Text.Trim()

        ' 1. Validate input
        If String.IsNullOrWhiteSpace(targetUsername) Then
            MessageBox.Show("Please type the customer's Username in TextBox1 first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Ensure an AVAILABLE device is selected from DataGridAvailable
        If String.IsNullOrWhiteSpace(selectedEntertainmentName) OrElse status <> "Available" Then
            MessageBox.Show("Please select an available device from the 'Available' grid first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 3. Confirm Rental
        Dim confirm As DialogResult = MessageBox.Show($"Rent '{selectedEntertainmentName}' to user '{targetUsername}'?", "Confirm Rental", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm <> DialogResult.Yes Then Return

        ' 4. Process the rental insertion
        StartRentSession(targetUsername, selectedEntertainmentName)
    End Sub

    Private Sub StartRentSession(userName As String, deviceName As String)
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()

                Using transaction As MySqlTransaction = conn.BeginTransaction()

                    ' STEP 1: Get the AccountID from the username
                    Dim getAccountSql As String = "SELECT AccountID FROM AccountLogin WHERE UserName = @user LIMIT 1;"
                    Dim accountId As Integer = 0

                    Using cmdAcc As New MySqlCommand(getAccountSql, conn, transaction)
                        cmdAcc.Parameters.AddWithValue("@user", userName)
                        Dim res = cmdAcc.ExecuteScalar()
                        If res IsNot Nothing AndAlso Not DBNull.Value.Equals(res) Then
                            accountId = Convert.ToInt32(res)
                        Else
                            MessageBox.Show($"Username '{userName}' not found in the system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            transaction.Rollback()
                            Return
                        End If
                    End Using

                    ' STEP 2: Get the EntertainmentID and the HourlyRate from the selected device name
                    Dim getDeviceSql As String = "
                    SELECT ent.EntertainmentID, entt.HourlyRate 
                    FROM Entertainment ent
                    INNER JOIN EntertainmentTier entt ON ent.EntertainmentTierID = entt.EntertainmentTierID
                    WHERE ent.EntertainmentName = @deviceName AND ent.Status = 'Available'
                    LIMIT 1;"

                    Dim deviceId As Integer = 0
                    Dim hourlyRate As Decimal = 0

                    Using cmdDev As New MySqlCommand(getDeviceSql, conn, transaction)
                        cmdDev.Parameters.AddWithValue("@deviceName", deviceName)
                        Using reader As MySqlDataReader = cmdDev.ExecuteReader()
                            If reader.Read() Then
                                deviceId = Convert.ToInt32(reader("EntertainmentID"))
                                hourlyRate = Convert.ToDecimal(reader("HourlyRate"))
                            Else
                                MessageBox.Show($"Device '{deviceName}' is no longer available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                transaction.Rollback()
                                Return
                            End If
                        End Using
                    End Using

                    ' STEP 3: Insert new active record into EntertainmentSession
                    Dim insertSessionSql As String = "
                    INSERT INTO EntertainmentSession (AccountID, EntertainmentID, LoginTime, Status, RateApplied) 
                    VALUES (@accId, @devId, NOW(), 'Active', @rate);"

                    Using cmdInsert As New MySqlCommand(insertSessionSql, conn, transaction)
                        cmdInsert.Parameters.AddWithValue("@accId", accountId)
                        cmdInsert.Parameters.AddWithValue("@devId", deviceId)
                        cmdInsert.Parameters.AddWithValue("@rate", hourlyRate)
                        cmdInsert.ExecuteNonQuery()
                    End Using

                    ' STEP 4: Change the device status in the Entertainment table to 'InUse'
                    Dim updateDeviceSql As String = "UPDATE Entertainment SET Status = 'InUse' WHERE EntertainmentID = @devId;"
                    Using cmdUpdate As New MySqlCommand(updateDeviceSql, conn, transaction)
                        cmdUpdate.Parameters.AddWithValue("@devId", deviceId)
                        cmdUpdate.ExecuteNonQuery()
                    End Using

                    ' Commit transaction completely
                    transaction.Commit()

                    MessageBox.Show($"Device '{deviceName}' successfully rented to '{userName}'!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TextBox1.Clear()

                End Using
            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("Unexpected Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

        ' Automatically refresh the UI layouts to move the device into the "In Use" grid
        RefreshGrids()
    End Sub
    ' UPDATED: Added safety check ensuring a grid item from DataGridInUse is chosen
    Private Sub Panel_ManualOff_Click(sender As Object, e As EventArgs) Handles Panel_ManualOff.Click
        Dim targetUsername As String = TextBox1.Text.Trim()

        ' 1. Basic Validation
        If String.IsNullOrWhiteSpace(targetUsername) Then
            MessageBox.Show("Please enter a username or select an active device row.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(selectedEntertainmentName) OrElse status <> "InUse" Then
            MessageBox.Show("Please select the active device row from the grid that you wish to close.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Confirm Action
        Dim confirm As DialogResult = MessageBox.Show($"Are you sure you want to end session for device '{selectedEntertainmentName}' assigned to user '{targetUsername}'?", "Confirm Manual Off", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm <> DialogResult.Yes Then Return

        ' 3. Process the complete rent termination using BOTH device name and username
        ProcessRentTermination(targetUsername, selectedEntertainmentName)
    End Sub

    ' --- Central Rent Processing Engine ---
    ' UPDATED: Now receives and queries via deviceName to perfectly link up with grid click selection
    Private Sub ProcessRentTermination(targetUsername As String, deviceName As String)
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()

                ' Start a Database Transaction to ensure all or nothing changes apply
                Using transaction As MySqlTransaction = conn.BeginTransaction()

                    ' 1. Find the active EntertainmentSessionID for BOTH this specific Username AND Device Name
                    Dim findSessionSql As String = "
                        SELECT es.EntertainmentSessionID 
                        FROM EntertainmentSession es 
                        INNER JOIN Account a ON es.AccountID = a.AccountID 
                        INNER JOIN AccountLogin al ON a.AccountID = al.AccountID 
                        INNER JOIN Entertainment ent ON es.EntertainmentID = ent.EntertainmentID
                        WHERE al.UserName = @user AND ent.EntertainmentName = @deviceName AND es.Status = 'Active' 
                        LIMIT 1;"

                    Dim sessionID As Integer = 0

                    Using cmdFind As New MySqlCommand(findSessionSql, conn, transaction)
                        cmdFind.Parameters.AddWithValue("@user", targetUsername)
                        cmdFind.Parameters.AddWithValue("@deviceName", deviceName)
                        Dim result = cmdFind.ExecuteScalar()

                        If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                            sessionID = Convert.ToInt32(result)
                        Else
                            MessageBox.Show($"No active matching session found for user '{targetUsername}' on device '{deviceName}'.", "Process Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            transaction.Rollback()
                            Return
                        End If
                    End Using

                    ' 2. Close out the active session by setting LogoutTime to current timestamp
                    Dim updateSessionSql As String = "
                        UPDATE EntertainmentSession 
                        SET LogoutTime = NOW(), Status = 'Completed' 
                        WHERE EntertainmentSessionID = @SessionID;"

                    Using cmdUpdate As New MySqlCommand(updateSessionSql, conn, transaction)
                        cmdUpdate.Parameters.AddWithValue("@SessionID", sessionID)
                        cmdUpdate.ExecuteNonQuery()
                    End Using

                    ' NEW STEP: Update the Entertainment device status itself back to 'Available' 
                    Dim updateDeviceSql As String = "
                        UPDATE Entertainment 
                        SET Status = 'Available' 
                        WHERE EntertainmentName = @deviceName;"

                    Using cmdDevice As New MySqlCommand(updateDeviceSql, conn, transaction)
                        cmdDevice.Parameters.AddWithValue("@deviceName", deviceName)
                        cmdDevice.ExecuteNonQuery()
                    End Using

                    ' 3. Retrieve the AccountID and the calculated cost using TIMESTAMPDIFF
                    Dim getCostSql As String = "
                        SELECT AccountID, 
                               (TIMESTAMPDIFF(SECOND, LoginTime, LogoutTime) / 3600.0) * RateApplied AS CalculatedCost 
                        FROM EntertainmentSession 
                        WHERE EntertainmentSessionID = @SessionID;"

                    Dim accountId As Integer = 0
                    Dim calculatedCost As Decimal = 0

                    Using cmdGetCost As New MySqlCommand(getCostSql, conn, transaction)
                        cmdGetCost.Parameters.AddWithValue("@SessionID", sessionID)
                        Using reader As MySqlDataReader = cmdGetCost.ExecuteReader()
                            If reader.Read() Then
                                accountId = Convert.ToInt32(reader("AccountID"))
                                calculatedCost = Convert.ToDecimal(reader("CalculatedCost"))
                            End If
                        End Using
                    End Using

                    ' Enforce floor valuation (prevent negative costs due to rapid logouts)
                    If calculatedCost < 0 Then calculatedCost = 0

                    ' 4. Pull current global Admin details
                    Dim employeeId As Integer = If(String.IsNullOrEmpty(AccountData.AdminUsername), 0, AccountData.AdminId)

                    ' 5. Insert the negative Cost amount into WalletTransactions as a 'Payment'
                    Dim insertTransactionSql As String = "
                        INSERT INTO WalletTransactions (EntertainmentSessionID, AccountID, Amount, TransactionType, EmployeeID, TransactionDate) 
                        VALUES (@SessionID, @accId, @amount, 'Payment', @employeeid, NOW());"

                    Using cmdInsertTx As New MySqlCommand(insertTransactionSql, conn, transaction)
                        cmdInsertTx.Parameters.AddWithValue("@SessionID", sessionID)
                        cmdInsertTx.Parameters.AddWithValue("@accId", accountId)
                        cmdInsertTx.Parameters.AddWithValue("@amount", -calculatedCost) ' Saved negatively to deduct balance
                        cmdInsertTx.Parameters.AddWithValue("@employeeid", employeeId)
                        cmdInsertTx.ExecuteNonQuery()
                    End Using

                    ' Commit all changes cleanly
                    transaction.Commit()

                    ' 6. Display Success Alert matching your structural logging rules
                    MessageBox.Show($"Payment of ${calculatedCost:F2} for session #{sessionID} was successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' 7. Append to text log component
                    LogAction("Payment", -calculatedCost, targetUsername)

                    ' 8. UI Reset
                    TextBox1.Clear()

                End Using

            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

        ' Refresh all UI layout grids automatically
        RefreshGrids()
    End Sub

    ' --- Action Logger Integration ---
    Private Sub LogAction(transactionType As String, amount As Decimal, targetUser As String)
        Dim adminName As String = If(String.IsNullOrEmpty(AccountData.AdminUsername), "System", AccountData.AdminUsername)
        Dim logMessage As String = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Admin '{adminName}' processed a {transactionType} of ${amount:F2} for user '{targetUser}'."
    End Sub
End Class