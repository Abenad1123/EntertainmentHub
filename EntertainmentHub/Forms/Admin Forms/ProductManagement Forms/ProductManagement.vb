Imports MySql.Data.MySqlClient
Imports System.Drawing

Public Class ProductManagement
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
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

    Private selectedProductID As Integer = 0

    Private Sub ProductInventoryManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeCategoryDropdown()
        RefreshProductsGrid()
    End Sub

    Private Sub InitializeCategoryDropdown()
        cmbboxCategory.Items.Clear()
        Dim categories As String() = {"Drinks", "Easy to eat", "Miscellaneous", "Pastries"}
        cmbboxCategory.Items.AddRange(categories)
        cmbboxCategory.SelectedItem = "Miscellaneous"
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
                    If DataGridView1.Columns.Contains("ProductID") Then
                        DataGridView1.Columns("ProductID").Visible = False
                    End If

                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                    If DataGridView1.Columns.Contains("CostPrice") Then
                        DataGridView1.Columns("CostPrice").DefaultCellStyle.Format = "C2"
                    End If
                    If DataGridView1.Columns.Contains("UnitPrice") Then
                        DataGridView1.Columns("UnitPrice").DefaultCellStyle.Format = "C2"
                    End If

                    ApplyGridRowFormatting()
                End If

            Catch ex As Exception
                MessageBox.Show("Error rendering products view: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub ApplyGridRowFormatting()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("QuantityInStock").Value IsNot Nothing AndAlso Not row.IsNewRow Then
                Dim stockCount As Integer = Convert.ToInt32(row.Cells("QuantityInStock").Value)

                If stockCount = 0 Then
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
        ApplyGridRowFormatting()
    End Sub

    Private Sub ResetInputs()
        selectedProductID = 0
        txtboxName.Clear()
        nudCostPrice.Value = nudCostPrice.Minimum
        nudUnitPrice.Value = nudUnitPrice.Minimum
        cmbboxCategory.SelectedItem = "Miscellaneous"
    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        If selectedProductID > 0 Then
            MessageBox.Show("These inputs are currently populated with an existing product's data. Please clear the selection or use the Update button.", "Action Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim productName As String = txtboxName.Text.Trim()
        Dim selectedCategory As String = cmbboxCategory.SelectedItem?.ToString()
        Dim costPrice As Decimal = nudCostPrice.Value
        Dim unitPrice As Decimal = nudUnitPrice.Value

        If productName.Length <= 3 Then
            MessageBox.Show("Product Name must be greater than 3 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If costPrice <= 0 OrElse unitPrice <= 0 Then
            MessageBox.Show("Cost Price and Unit Price values must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "INSERT INTO products (ProductName, CostPrice, UnitPrice, QuantityInStock, Category) VALUES (@name, @cost, @unit, 0, @cat)"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", productName)
                    cmd.Parameters.AddWithValue("@cost", costPrice)
                    cmd.Parameters.AddWithValue("@unit", unitPrice)
                    cmd.Parameters.AddWithValue("@cat", selectedCategory)
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show($"'{productName}' successfully saved to catalog!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ResetInputs()
                RefreshProductsGrid()

            Catch ex As MySqlException When ex.Number = 1062
                MessageBox.Show("An item with this product name already exists in the catalog system.", "Duplicate Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("Save operations aborted: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            selectedProductID = Convert.ToInt32(row.Cells("ProductID").Value)

            Dim currentStock As Integer = Convert.ToInt32(row.Cells("QuantityInStock").Value)
            nudStockControl.Minimum = 0

            If currentStock > nudStockControl.Maximum Then
                nudStockControl.Maximum = currentStock + 500
            End If
            nudStockControl.Value = currentStock

            txtboxName.Text = row.Cells("ProductName").Value.ToString()
            cmbboxCategory.SelectedItem = row.Cells("Category").Value.ToString()

            Dim costVal As Decimal = Convert.ToDecimal(row.Cells("CostPrice").Value)
            If costVal > nudCostPrice.Maximum Then nudCostPrice.Maximum = costVal + 1000
            nudCostPrice.Value = costVal

            Dim unitVal As Decimal = Convert.ToDecimal(row.Cells("UnitPrice").Value)
            If unitVal > nudUnitPrice.Maximum Then nudUnitPrice.Maximum = unitVal + 1000
            nudUnitPrice.Value = unitVal
        End If
    End Sub

    Private Sub btnUpdateProd_Click(sender As Object, e As EventArgs) Handles btnUpdateProd.Click
        If selectedProductID = 0 Then
            MessageBox.Show("Please select a product from the grid to update.", "Selection Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim productName As String = txtboxName.Text.Trim()
        Dim selectedCategory As String = cmbboxCategory.SelectedItem?.ToString()
        Dim costPrice As Decimal = nudCostPrice.Value
        Dim unitPrice As Decimal = nudUnitPrice.Value

        If productName.Length <= 3 Then
            MessageBox.Show("Product Name must be greater than 3 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If costPrice <= 0 OrElse unitPrice <= 0 Then
            MessageBox.Show("Cost Price and Unit Price values must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "UPDATE products SET ProductName = @name, CostPrice = @cost, UnitPrice = @unit, Category = @cat WHERE ProductID = @pid"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", productName)
                    cmd.Parameters.AddWithValue("@cost", costPrice)
                    cmd.Parameters.AddWithValue("@unit", unitPrice)
                    cmd.Parameters.AddWithValue("@cat", selectedCategory)
                    cmd.Parameters.AddWithValue("@pid", selectedProductID)
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Product successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ResetInputs()
                RefreshProductsGrid()

            Catch ex As MySqlException When ex.Number = 1062
                MessageBox.Show("Another product with this name already exists.", "Duplicate Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("Update aborted: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub btnDeleteProd_Click(sender As Object, e As EventArgs) Handles btnDeleteProd.Click
        If selectedProductID = 0 Then
            MessageBox.Show("Please select a product from the grid to delete.", "Selection Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this product? This action cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Using conn = DBConnection.GetConnection()
                Try
                    conn.Open()
                    Dim query As String = "DELETE FROM products WHERE ProductID = @pid"

                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@pid", selectedProductID)
                        cmd.ExecuteNonQuery()
                    End Using

                    MessageBox.Show("Product deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ResetInputs()
                    RefreshProductsGrid()

                Catch ex As MySqlException When ex.Number = 1451
                    MessageBox.Show("Cannot delete this product because it has existing sales or transaction records attached to it.", "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    MessageBox.Show("Deletion error: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs)
        If selectedProductID = 0 Then
            MessageBox.Show("Please select a target row item inside the data view grid view first.", "Selection Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim newStockTarget As Integer = Convert.ToInt32(nudStockControl.Value)

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "UPDATE products SET QuantityInStock = @stock WHERE ProductID = @pid"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@stock", newStockTarget)
                    cmd.Parameters.AddWithValue("@pid", selectedProductID)
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Inventory balance successfully adjusted!", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                RefreshProductsGrid()

            Catch ex As Exception
                MessageBox.Show("Database update error: " & ex.Message, "Execution Failure", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

End Class