Imports MySql.Data.MySqlClient

Public Class ProductPOS

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles btnGoBack.Click
        Dim frm As New AdminDashboard()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub BtnUserLoginEnter(sender As Object, e As EventArgs) Handles btnGoBack.MouseEnter
        btnGoBack.Image = My.Resources.go_back_state_2
    End Sub

    Private Sub BtnUserLoginLeave(sender As Object, e As EventArgs) Handles btnGoBack.MouseLeave
        btnGoBack.Image = My.Resources.go_back_state_1
    End Sub

    Private cartTable As New DataTable()
    Private undoStack As New Stack(Of DataTable)()
    Private redoStack As New Stack(Of DataTable)()
    Private selectedProductID As Integer = 0
    Private selectedCostPrice As Decimal = 0D

    Private Sub PointOfSaleManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeCartTable()
        LoadEmployeeData()
        RefreshProductsGrid()
        txtboxProdName.ReadOnly = True
        txtboxProdCategory.ReadOnly = True
        txtboxProdUnitPrice.ReadOnly = True
        txtboxProdLineTotal.ReadOnly = True
    End Sub

    Private Sub InitializeCartTable()
        cartTable.Columns.Add("ProductID", GetType(Integer))
        cartTable.Columns.Add("Name", GetType(String))
        cartTable.Columns.Add("Category", GetType(String))
        cartTable.Columns.Add("Quantity", GetType(Integer))
        cartTable.Columns.Add("UnitPrice", GetType(Decimal))
        cartTable.Columns.Add("CostPrice", GetType(Decimal))
        cartTable.Columns.Add("LineTotal", GetType(Decimal))

        DataGridView2.DataSource = cartTable
        FormatCartGrid()
    End Sub

    Private Sub LoadEmployeeData()
        If Not String.IsNullOrEmpty(AccountData.AdminUsername) Then
            lblEmployeeUsernameFill.Text = AccountData.AdminUsername
        Else
            lblEmployeeUsernameFill.Text = "ERROR"
        End If
    End Sub

    Private Sub RefreshProductsGrid()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT ProductID, ProductName, Category, CostPrice, UnitPrice, QuantityInStock FROM products"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridView1.DataSource = dt
                End Using

                If DataGridView1.Columns.Count > 0 Then
                    DataGridView1.Columns("ProductID").Visible = False

                    DataGridView1.Columns("CostPrice").DisplayIndex = 3
                    DataGridView1.Columns("UnitPrice").DisplayIndex = 4

                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                    DataGridView1.Columns("CostPrice").DefaultCellStyle.Format = "C2"
                    DataGridView1.Columns("UnitPrice").DefaultCellStyle.Format = "C2"

                    ApplyProductGridFormatting()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub ApplyProductGridFormatting()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow AndAlso row.Cells("QuantityInStock").Value IsNot Nothing Then
                Dim stock As Integer = Convert.ToInt32(row.Cells("QuantityInStock").Value)
                If stock = 0 Then
                    row.DefaultCellStyle.BackColor = Color.Maroon
                    row.DefaultCellStyle.ForeColor = Color.White
                Else
                    row.DefaultCellStyle.BackColor = DataGridView1.DefaultCellStyle.BackColor
                    row.DefaultCellStyle.ForeColor = DataGridView1.DefaultCellStyle.ForeColor
                End If
            End If
        Next
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        ApplyProductGridFormatting()
    End Sub

    Private Sub FormatCartGrid()
        If DataGridView2.Columns.Count > 0 Then
            DataGridView2.Columns("ProductID").Visible = False
            DataGridView2.Columns("CostPrice").Visible = False
            DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView2.Columns("UnitPrice").DefaultCellStyle.Format = "C2"
            DataGridView2.Columns("LineTotal").DefaultCellStyle.Format = "C2"
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            selectedProductID = Convert.ToInt32(row.Cells("ProductID").Value)
            selectedCostPrice = Convert.ToDecimal(row.Cells("CostPrice").Value)

            txtboxProdName.Text = row.Cells("ProductName").Value.ToString()
            txtboxProdCategory.Text = row.Cells("Category").Value.ToString()
            txtboxProdUnitPrice.Text = Convert.ToDecimal(row.Cells("UnitPrice").Value).ToString("F2")

            nudProdQuantity.Value = 1
            CalculateLineTotal()
        End If
    End Sub

    Private Sub CalculateLineTotal()
        If Not String.IsNullOrEmpty(txtboxProdUnitPrice.Text) Then
            Dim price As Decimal
            If Decimal.TryParse(txtboxProdUnitPrice.Text, price) Then
                txtboxProdLineTotal.Text = (price * nudProdQuantity.Value).ToString("F2")
            End If
        End If
    End Sub

    Private Sub nudProdQuantity_ValueChanged(sender As Object, e As EventArgs) Handles nudProdQuantity.ValueChanged
        CalculateLineTotal()
    End Sub

    Private Sub btnSubtOneQty_Click(sender As Object, e As EventArgs) Handles btnSubtOneQty.Click
        If nudProdQuantity.Value > 0 Then
            nudProdQuantity.Value -= 1
        End If
    End Sub

    Private Sub btnPlusOneQty_Click(sender As Object, e As EventArgs) Handles btnPlusOneQty.Click
        nudProdQuantity.Value += 1
    End Sub

    Private Sub SaveCartState()
        undoStack.Push(cartTable.Copy())
        redoStack.Clear()
    End Sub

    Private Sub btnScanItem_Click(sender As Object, e As EventArgs) Handles btnScanItem.Click
        If selectedProductID = 0 OrElse nudProdQuantity.Value = 0 Then
            Return
        End If

        SaveCartState()

        Dim newRow As DataRow = cartTable.NewRow()
        newRow("ProductID") = selectedProductID
        newRow("Name") = txtboxProdName.Text
        newRow("Category") = txtboxProdCategory.Text
        newRow("Quantity") = nudProdQuantity.Value
        newRow("UnitPrice") = Convert.ToDecimal(txtboxProdUnitPrice.Text)
        newRow("CostPrice") = selectedCostPrice
        newRow("LineTotal") = Convert.ToDecimal(txtboxProdLineTotal.Text)

        cartTable.Rows.Add(newRow)
        ResetSelection()
    End Sub

    Private Sub ResetSelection()
        selectedProductID = 0
        selectedCostPrice = 0D
        txtboxProdName.Clear()
        txtboxProdCategory.Clear()
        txtboxProdUnitPrice.Clear()
        txtboxProdLineTotal.Clear()
        nudProdQuantity.Value = 0
    End Sub

    Private Sub btnRemoveItem_Click(sender As Object, e As EventArgs) Handles btnRemoveItem.Click
        If DataGridView2.SelectedRows.Count > 0 Then
            SaveCartState()
            Dim viewRow As DataRowView = DirectCast(DataGridView2.SelectedRows(0).DataBoundItem, DataRowView)
            viewRow.Row.Delete()
            cartTable.AcceptChanges()
        End If
    End Sub

    Private Sub btnUndo_Click(sender As Object, e As EventArgs) Handles btnUndo.Click
        If undoStack.Count > 0 Then
            redoStack.Push(cartTable.Copy())
            cartTable = undoStack.Pop()
            DataGridView2.DataSource = cartTable
            FormatCartGrid()
        End If
    End Sub

    Private Sub btnRedo_Click(sender As Object, e As EventArgs) Handles btnRedo.Click
        If redoStack.Count > 0 Then
            undoStack.Push(cartTable.Copy())
            cartTable = redoStack.Pop()
            DataGridView2.DataSource = cartTable
            FormatCartGrid()
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If cartTable.Rows.Count > 0 Then
            SaveCartState()
            cartTable.Clear()
        End If
    End Sub

    Private Sub btnCheckout_Click(sender As Object, e As EventArgs) Handles btnCheckout.Click
        If cartTable.Rows.Count = 0 Then
            MessageBox.Show("Cart is empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim customerUsername As String = txtboxCustomerUsername.Text.Trim()
        Dim employeeUsername As String = lblEmployeeUsernameFill.Text

        If String.IsNullOrEmpty(customerUsername) OrElse employeeUsername = "ERROR" Then
            MessageBox.Show("Valid Customer Username and Employee Session required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim totalAmount As Decimal = 0
        For Each row As DataRow In cartTable.Rows
            totalAmount += Convert.ToDecimal(row("LineTotal"))
        Next

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim targetAccountID As Integer = 0
                Dim queryAcc As String = "SELECT AccountID FROM accountlogin WHERE UserName = @uname"
                Using cmdAcc As New MySqlCommand(queryAcc, conn)
                    cmdAcc.Parameters.AddWithValue("@uname", customerUsername)
                    Dim res = cmdAcc.ExecuteScalar()
                    If res Is Nothing Then
                        MessageBox.Show("Customer username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If
                    targetAccountID = Convert.ToInt32(res)
                End Using

                Dim targetEmployeeID As Integer = 0
                Dim queryEmp As String = "SELECT EmployeeID FROM employeelogin WHERE UserName = @ename"
                Using cmdEmp As New MySqlCommand(queryEmp, conn)
                    cmdEmp.Parameters.AddWithValue("@ename", employeeUsername)
                    Dim res = cmdEmp.ExecuteScalar()
                    If res Is Nothing Then
                        MessageBox.Show("Employee account verification failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If
                    targetEmployeeID = Convert.ToInt32(res)
                End Using

                Dim currentBalance As Decimal = 0
                Dim queryBal As String = "SELECT COALESCE(SUM(CASE WHEN TransactionType IN ('Deposit', 'Bonus', 'Refund', 'Adjustment') THEN Amount WHEN TransactionType IN ('Payment', 'Withdrawal') THEN -Amount ELSE 0 END), 0) FROM wallettransactions WHERE AccountID = @acc"
                Using cmdBal As New MySqlCommand(queryBal, conn)
                    cmdBal.Parameters.AddWithValue("@acc", targetAccountID)
                    currentBalance = Convert.ToDecimal(cmdBal.ExecuteScalar())
                End Using

                If currentBalance < totalAmount Then
                    MessageBox.Show($"Insufficient funds. Balance: {currentBalance:C2}, Total: {totalAmount:C2}", "Transaction Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return
                End If

                Using transaction = conn.BeginTransaction()
                    Try
                        Dim insertSale As String = "INSERT INTO sale (AccountID, SaleDate) VALUES (@acc, NOW())"
                        Dim newSaleID As Integer = 0
                        Using cmdSale As New MySqlCommand(insertSale, conn, transaction)
                            cmdSale.Parameters.AddWithValue("@acc", targetAccountID)
                            cmdSale.ExecuteNonQuery()
                            newSaleID = Convert.ToInt32(cmdSale.LastInsertedId)
                        End Using

                        Dim insertWallet As String = "INSERT INTO wallettransactions (EmployeeID, AccountID, SalesID, Amount, TransactionType, TransactionDate) VALUES (@emp, @acc, @sid, @amt, 'Payment', NOW())"
                        Using cmdWallet As New MySqlCommand(insertWallet, conn, transaction)
                            cmdWallet.Parameters.AddWithValue("@emp", targetEmployeeID)
                            cmdWallet.Parameters.AddWithValue("@acc", targetAccountID)
                            cmdWallet.Parameters.AddWithValue("@sid", newSaleID)
                            cmdWallet.Parameters.AddWithValue("@amt", totalAmount)
                            cmdWallet.ExecuteNonQuery()
                        End Using

                        Dim insertItem As String = "INSERT INTO salesitem (SalesID, ProductID, Quantity, UnitPrice, CostPrice) VALUES (@sid, @pid, @qty, @up, @cp)"
                        Dim updateStock As String = "UPDATE products SET QuantityInStock = QuantityInStock - @qty WHERE ProductID = @pid"

                        For Each row As DataRow In cartTable.Rows
                            Using cmdItem As New MySqlCommand(insertItem, conn, transaction)
                                cmdItem.Parameters.AddWithValue("@sid", newSaleID)
                                cmdItem.Parameters.AddWithValue("@pid", row("ProductID"))
                                cmdItem.Parameters.AddWithValue("@qty", row("Quantity"))
                                cmdItem.Parameters.AddWithValue("@up", row("UnitPrice"))
                                cmdItem.Parameters.AddWithValue("@cp", row("CostPrice"))
                                cmdItem.ExecuteNonQuery()
                            End Using

                            Using cmdUpdate As New MySqlCommand(updateStock, conn, transaction)
                                cmdUpdate.Parameters.AddWithValue("@qty", row("Quantity"))
                                cmdUpdate.Parameters.AddWithValue("@pid", row("ProductID"))
                                cmdUpdate.ExecuteNonQuery()
                            End Using
                        Next

                        transaction.Commit()
                        MessageBox.Show("Checkout successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        cartTable.Clear()
                        undoStack.Clear()
                        redoStack.Clear()
                        RefreshProductsGrid()

                    Catch ex As Exception
                        transaction.Rollback()
                        MessageBox.Show("Transaction failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub
End Class