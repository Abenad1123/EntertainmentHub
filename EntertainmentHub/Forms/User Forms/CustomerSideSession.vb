Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class CustomerSideSession

    Public targetUsername As String = AccountData.CustomerUsername
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub


#Region "Rent Termination"
    Private Sub ProcessRentTermination(targetUsername As String, deviceName As String)
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()


                Using transaction As MySqlTransaction = conn.BeginTransaction()


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


                    Dim updateSessionSql As String = "
                        UPDATE EntertainmentSession 
                        SET LogoutTime = NOW(), Status = 'Completed' 
                        WHERE EntertainmentSessionID = @SessionID;"

                    Using cmdUpdate As New MySqlCommand(updateSessionSql, conn, transaction)
                        cmdUpdate.Parameters.AddWithValue("@SessionID", sessionID)
                        cmdUpdate.ExecuteNonQuery()
                    End Using

                    Dim updateDeviceSql As String = "
                        UPDATE Entertainment 
                        SET Status = 'Available' 
                        WHERE EntertainmentName = @deviceName;"

                    Using cmdDevice As New MySqlCommand(updateDeviceSql, conn, transaction)
                        cmdDevice.Parameters.AddWithValue("@deviceName", deviceName)
                        cmdDevice.ExecuteNonQuery()
                    End Using


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

                    If calculatedCost < 0 Then calculatedCost = 0


                    Dim employeeId As Integer = If(String.IsNullOrEmpty(AccountData.AdminUsername), 0, AccountData.AdminId)


                    Dim insertTransactionSql As String = "
                        INSERT INTO WalletTransactions (EntertainmentSessionID, AccountID, Amount, TransactionType, EmployeeID, TransactionDate) 
                        VALUES (@SessionID, @accId, @amount, 'Payment', @employeeid, NOW());"

                    Using cmdInsertTx As New MySqlCommand(insertTransactionSql, conn, transaction)
                        cmdInsertTx.Parameters.AddWithValue("@SessionID", sessionID)
                        cmdInsertTx.Parameters.AddWithValue("@accId", accountId)
                        cmdInsertTx.Parameters.AddWithValue("@amount", -calculatedCost)
                        cmdInsertTx.Parameters.AddWithValue("@employeeid", employeeId)
                        cmdInsertTx.ExecuteNonQuery()
                    End Using


                    transaction.Commit()


                    MessageBox.Show($"Payment of ${calculatedCost:F2} for session #{sessionID} was successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    LogAction("Payment", -calculatedCost, targetUsername)

                    TextBox1.Clear()

                End Using

            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using



    End Sub

#End Region
End Class