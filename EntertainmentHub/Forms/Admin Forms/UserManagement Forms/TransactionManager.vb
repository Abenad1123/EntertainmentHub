Imports System.Data
Imports System.Drawing
Imports System.Reflection.Emit
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient

Public Class TransactionManager
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        HelperFunc.EnableDoubleBuffer(Me)

        Me.BackgroundImage = AccountData.AdminCommonBackground
        Me.BackgroundImageLayout = ImageLayout.Stretch

        Dim ctrls As Control() = {btnAdjust, btnBonus, btnDeposit, btnPayment, btnWithdraw, Button5}
        For Each i In ctrls
            HelperFunc.ApplyButtonTheme(i)
        Next

        TableLayoutPanel2.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel2)

        Label7.ForeColor = Color.FromArgb(255, 255, 255)
        Label7.Font = AppFonts.Aero(30)

        Label2.ForeColor = Color.FromArgb(255, 255, 255)
        Label2.Font = AppFonts.VenusRising(15)

        Label3.ForeColor = Color.FromArgb(255, 255, 255)
        Label3.Font = AppFonts.VenusRising(15)

        Label5.ForeColor = Color.FromArgb(255, 255, 255)
        Label5.Font = AppFonts.VenusRising(15)

        Label6.ForeColor = Color.FromArgb(255, 255, 255)
        Label6.Font = AppFonts.VenusRising(15)

        HelperFunc.ApplyBorder(DataGridView1)
        HelperFunc.ApplyBorder(txtActionLog)

        HelperFunc.FontDesign(Label4, Color.FromArgb(255, 255, 255), AppFonts.Coolvetica(18))
        HelperFunc.FontDesign(Label1, Color.FromArgb(255, 255, 255), AppFonts.Coolvetica(18))

        StyleDataGridView()
        LoadWalletTransactions()
    End Sub

    Private Sub StyleDataGridView()
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToResizeRows = False
        DataGridView1.ReadOnly = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
        DataGridView1.RowHeadersVisible = False

        DataGridView1.BackgroundColor = Color.White
        DataGridView1.BorderStyle = BorderStyle.None
        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal

        DataGridView1.EnableHeadersVisualStyles = False
        DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48)
        DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        DataGridView1.ColumnHeadersHeight = 40
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        DataGridView1.DefaultCellStyle.BackColor = Color.White
        DataGridView1.DefaultCellStyle.ForeColor = Color.Black
        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255)
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black
        DataGridView1.DefaultCellStyle.Font = New Font("Segoe UI", 9)
        DataGridView1.DefaultCellStyle.Padding = New Padding(5, 0, 5, 0)

        DataGridView1.RowTemplate.Height = 35
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub FormatColumns()
        If DataGridView1.Columns.Count > 0 Then
            DataGridView1.Columns("WalletTransactionID").HeaderText = "Txn ID"
            DataGridView1.Columns("WalletTransactionID").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns("WalletTransactionID").Width = 60
            DataGridView1.Columns("WalletTransactionID").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns("WalletTransactionID").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView1.Columns("UserName").HeaderText = "Username"
            DataGridView1.Columns("UserName").FillWeight = 25

            DataGridView1.Columns("SaleID").HeaderText = "Sale ID"
            DataGridView1.Columns("SaleID").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns("SaleID").Width = 70
            DataGridView1.Columns("SaleID").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns("SaleID").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView1.Columns("Amount").HeaderText = "Amount"
            DataGridView1.Columns("Amount").FillWeight = 20
            DataGridView1.Columns("Amount").DefaultCellStyle.Format = "C2"
            DataGridView1.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            DataGridView1.Columns("TransactionType").HeaderText = "Type"
            DataGridView1.Columns("TransactionType").FillWeight = 20

            DataGridView1.Columns("TransactionDate").HeaderText = "Date"
            DataGridView1.Columns("TransactionDate").FillWeight = 30
            DataGridView1.Columns("TransactionDate").DefaultCellStyle.Format = "MMM dd, yyyy hh:mm tt"
        End If
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

                Dim employeeid As Integer = If(AccountData.AdminId > 0, AccountData.AdminId, 0)

                Dim accountId As Integer = GetAccountIdByUsername(targetUsername, conn)
                If accountId = 0 Then
                    MessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                If transactionType = "Withdrawal" Then
                    Dim currentBalance As Decimal = GetCurrentBalance(accountId, conn)
                    If currentBalance < Math.Abs(transactionAmount) Then
                        MessageBox.Show($"Insufficient funds. Current balance is: {currentBalance:C2}", "Withdrawal Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End If

                Using transaction = conn.BeginTransaction()
                    Try
                        Dim insertQuery As String = "INSERT INTO wallettransactions (AccountID, Amount, TransactionType, EmployeeID) VALUES (@accId, @amount, @type, @employeeid)"
                        Using cmd As New MySqlCommand(insertQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@accId", accountId)
                            cmd.Parameters.AddWithValue("@amount", transactionAmount)
                            cmd.Parameters.AddWithValue("@type", transactionType)
                            cmd.Parameters.AddWithValue("@employeeid", employeeid)
                            cmd.ExecuteNonQuery()
                        End Using

                        Dim logDesc As String = $"Processed {transactionType} of {Math.Abs(transactionAmount):C2} for account '{targetUsername}' (ID {accountId})."
                        HelperFunc.Log(conn, transaction, employeeid, "wallettransactions", "Insert", logDesc)

                        transaction.Commit()

                        MessageBox.Show($"{transactionType} of {Math.Abs(transactionAmount):C2} was successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        LogAction(transactionType, transactionAmount, targetUsername)

                        txtboxAmount.Clear()
                        LoadWalletTransactions()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using

            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub LogAction(transactionType As String, amount As Decimal, targetUser As String)
        Dim adminName As String = If(String.IsNullOrEmpty(AccountData.AdminUsername), "System", AccountData.AdminUsername)
        Dim logMessage As String = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Admin '{adminName}' processed a {transactionType} of {Math.Abs(amount):C2} for user '{targetUser}'."
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
        Dim query As String = "SELECT COALESCE(SUM(Amount), 0) AS Balance FROM wallettransactions WHERE AccountID = @accId"
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
                Dim query As String = "SELECT w.WalletTransactionID, a.UserName, w.SaleID, w.Amount, w.TransactionType, w.TransactionDate FROM wallettransactions w JOIN accountlogin a ON w.AccountID = a.AccountID ORDER BY w.TransactionDate DESC"

                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridView1.DataSource = dt
                End Using

                FormatColumns()

            Catch ex As MySqlException
                MessageBox.Show("Error loading grid: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
        HelperFunc.SwitchForm(Me, New UserManagement())
    End Sub

    Private Sub BtnUserLoginEnter(sender As Object, e As EventArgs) Handles btnGoBack.MouseEnter
        btnGoBack.Image = My.Resources.go_back_state_2
    End Sub

    Private Sub BtnUserLoginLeave(sender As Object, e As EventArgs) Handles btnGoBack.MouseLeave
        btnGoBack.Image = My.Resources.go_back_state_1
    End Sub
End Class