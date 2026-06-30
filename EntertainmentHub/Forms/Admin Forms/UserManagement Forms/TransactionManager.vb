Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient

Public Class TransactionManager
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadWalletTransactions()
    End Sub


    Private Sub btnDeposit_Click(sender As Object, e As EventArgs) Handles btnDeposit.Click
        ProcessTransaction("Deposit")
    End Sub

    Private Sub btnWithdraw_Click(sender As Object, e As EventArgs) Handles btnWithdraw.Click
        ProcessTransaction("Withdrawal")
    End Sub

    Private Sub btnBonus_Click(sender As Object, e As EventArgs) Handles btnBonus.Click
        ProcessTransaction("Bonus")
    End Sub

    Private Sub btnAdjust_Click(sender As Object, e As EventArgs) Handles btnAdjust.Click
        ProcessTransaction("Adjustment")
    End Sub

    Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        ProcessTransaction("Payment")
    End Sub


    Private Sub ProcessTransaction(transactionType As String)
        Dim targetUsername As String = txtboxUsernameInput.Text.Trim()
        Dim amountText As String = txtboxAmount.Text.Trim()
        Dim transactionAmount As Decimal


        If String.IsNullOrWhiteSpace(targetUsername) Then
            MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not Decimal.TryParse(amountText, transactionAmount) Then
            MessageBox.Show("Please enter a valid numeric amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If


        If transactionType = "Withdrawal" AndAlso transactionAmount > 0 Then
            transactionAmount = -transactionAmount
        End If

        If transactionType = "Payment" AndAlso transactionAmount > 0 Then
            transactionAmount = -transactionAmount
        End If


        If (transactionType = "Deposit" Or transactionType = "Bonus") AndAlso transactionAmount <= 0 Then
            MessageBox.Show("Amount must be greater than zero for Deposits and Bonuses.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()

                Dim employeeid As Integer = If(String.IsNullOrEmpty(AccountData.AdminUsername), 0, AccountData.AdminId)

                Dim accountId As Integer = GetAccountIdByUsername(targetUsername, conn)
                If accountId = 0 Then
                    MessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If


                If transactionType = "Withdrawal" Then
                    Dim currentBalance As Decimal = GetCurrentBalance(accountId, conn)
                    If currentBalance < Math.Abs(transactionAmount) Then
                        MessageBox.Show($"Insufficient funds. Current balance is: ${currentBalance:F2}", "Withdrawal Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End If


                Dim insertQuery As String = "INSERT INTO wallettransactions (AccountID, Amount, TransactionType, EmployeeID) VALUES (@accId, @amount, @type, @employeeid)"
                Using cmd As New MySqlCommand(insertQuery, conn)
                    cmd.Parameters.AddWithValue("@accId", accountId)
                    cmd.Parameters.AddWithValue("@amount", transactionAmount)
                    cmd.Parameters.AddWithValue("@type", transactionType)
                    cmd.Parameters.AddWithValue("@employeeid", employeeid)
                    cmd.ExecuteNonQuery()
                End Using


                MessageBox.Show($"{transactionType} of ${transactionAmount:F2} was successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)


                LogAction(transactionType, transactionAmount, targetUsername)

                txtboxAmount.Clear()
                LoadWalletTransactions()

            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub


    Private Sub LogAction(transactionType As String, amount As Decimal, targetUser As String)
        Dim adminName As String = If(String.IsNullOrEmpty(AccountData.AdminUsername), "System", AccountData.AdminUsername)
        Dim logMessage As String = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Admin '{adminName}' processed a {transactionType} of ${amount:F2} for user '{targetUser}'."
        txtActionLog.AppendText(logMessage & Environment.NewLine)
        txtActionLog.SelectionStart = txtActionLog.Text.Length
        txtActionLog.ScrollToCaret()
    End Sub


    Private Function GetAccountIdByUsername(username As String, conn As MySqlConnection) As Integer
        Dim query As String = "SELECT AccountID FROM accountlogin WHERE UserName = @user LIMIT 1"
        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@user", username)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                Return Convert.ToInt32(result)
            End If
        End Using
        Return 0
    End Function


    Private Function GetCurrentBalance(accountId As Integer, conn As MySqlConnection) As Decimal

        Dim query As String = "
            SELECT COALESCE(SUM(Amount), 0) AS Balance
            FROM wallettransactions 
            WHERE AccountID = @accId"

        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@accId", accountId)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                Return Convert.ToDecimal(result)
            End If
        End Using
        Return 0D
    End Function


    Private Sub LoadWalletTransactions()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "
                    SELECT w.WalletTransactionID, a.UserName, w.SalesID, w.Amount, w.TransactionType, w.TransactionDate 
                    FROM wallettransactions w
                    JOIN accountlogin a ON w.AccountID = a.AccountID
                    ORDER BY w.TransactionDate DESC"

                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridView1.DataSource = dt
                End Using

                If DataGridView1.Columns.Count > 0 Then
                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                    If DataGridView1.Columns.Contains("Amount") Then
                        DataGridView1.Columns("Amount").DefaultCellStyle.Format = "C2"
                    End If
                    If DataGridView1.Columns.Contains("TransactionDate") Then
                        DataGridView1.Columns("TransactionDate").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss"
                    End If
                End If

            Catch ex As MySqlException
                MessageBox.Show("Error loading grid: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New UserManagement()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub
End Class