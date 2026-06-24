Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient

Public Class TransactionManager
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadWalletTransactions()
    End Sub

    ' --- Button Events: Call the central processor with the specific type ---
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

    ' --- Central Transaction Processor ---
    Private Sub ProcessTransaction(transactionType As String)
        Dim targetUsername As String = txtboxUsernameInput.Text.Trim()
        Dim amountText As String = txtboxAmount.Text.Trim()
        Dim transactionAmount As Decimal

        ' 1. Basic Validation
        If String.IsNullOrWhiteSpace(targetUsername) Then
            MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not Decimal.TryParse(amountText, transactionAmount) Then
            MessageBox.Show("Please enter a valid numeric amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Enforce positive amounts for everything EXCEPT Adjustments
        If transactionType <> "Adjustment" AndAlso transactionAmount <= 0 Then
            MessageBox.Show("Amount must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()

                ' 2. Look up AccountID based on Username
                Dim accountId As Integer = GetAccountIdByUsername(targetUsername, conn)
                If accountId = 0 Then
                    MessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                ' 3. If Withdrawal, check if they have enough balance
                If transactionType = "Withdrawal" Then
                    Dim currentBalance As Decimal = GetCurrentBalance(accountId, conn)
                    If currentBalance < transactionAmount Then
                        MessageBox.Show($"Insufficient funds. Current balance is: ${currentBalance:F2}", "Withdrawal Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End If

                ' 4. Insert the Transaction (Updated to wallettransactions)
                Dim insertQuery As String = "INSERT INTO wallettransactions (AccountID, Amount, TransactionType) VALUES (@accId, @amount, @type)"
                Using cmd As New MySqlCommand(insertQuery, conn)
                    cmd.Parameters.AddWithValue("@accId", accountId)
                    cmd.Parameters.AddWithValue("@amount", transactionAmount)
                    cmd.Parameters.AddWithValue("@type", transactionType)
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show($"{transactionType} of ${Math.Abs(transactionAmount):F2} was successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' --- NEW LOGIC: Update Action Log ---
                LogAction(transactionType, transactionAmount, targetUsername)

                ' 5. Clear inputs and Refresh Grid
                txtboxAmount.Clear()
                LoadWalletTransactions()

            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' --- NEW HELPER: Append to Textbox Action Log ---
    Private Sub LogAction(transactionType As String, amount As Decimal, targetUser As String)
        ' Use the stored admin username (fallback to "System" if empty for some reason)
        Dim adminName As String = If(String.IsNullOrEmpty(AccountData.AdminUsername), "System", AccountData.AdminUsername)

        ' Format a clean log message
        Dim logMessage As String = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Admin '{adminName}' processed a {transactionType} of ${amount:F2} for user '{targetUser}'."

        ' Append text with a new line
        txtActionLog.AppendText(logMessage & Environment.NewLine)

        ' Scroll the textbox to the bottom so the newest log is always visible
        txtActionLog.SelectionStart = txtActionLog.Text.Length
        txtActionLog.ScrollToCaret()
    End Sub

    ' --- Helper: Get AccountID ---
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

    ' --- Helper: Calculate Balance (Updated to wallettransactions) ---
    Private Function GetCurrentBalance(accountId As Integer, conn As MySqlConnection) As Decimal
        Dim query As String = "
            SELECT COALESCE(SUM(
                CASE 
                    WHEN TransactionType IN ('Deposit', 'Bonus', 'Refund') THEN Amount
                    WHEN TransactionType IN ('Payment', 'Withdrawal') THEN -Amount
                    WHEN TransactionType = 'Adjustment' THEN Amount
                    ELSE 0 
                END), 0) AS Balance
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

    ' --- Helper: Refresh DataGridView1 (Updated to wallettransactions) ---
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
End Class