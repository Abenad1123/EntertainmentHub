Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class ProductManagement
    Private selectedProductID As Integer = 0

    Private Sub ProductManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackgroundImage = AccountData.AdminCommonBackground
        Me.BackgroundImageLayout = ImageLayout.Stretch

        DataGridView1.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(DataGridView1)

        HelperFunc.ApplyButtonTheme(Button1)
        HelperFunc.ApplyButtonTheme(Button2)

        TableLayoutPanel3.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel3)

        TableLayoutPanel4.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel4)

        HelperFunc.FontDesign(lblTitle, Color.FromArgb(255, 255, 255), AppFonts.VenusRising(18))
        HelperFunc.FontDesign(Label5, Color.FromArgb(255, 255, 255), AppFonts.VenusRising(18))

        Dim labels As Control() = {Label1, Label2, Label3, Label4, Label6, Label7, Label9, Label10, Label12, Label13}
        For Each i In labels
            HelperFunc.FontDesign(i, Color.FromArgb(255, 255, 255), AppFonts.Coolvetica(16))
        Next

        InitializeCategories()
        StyleDataGridView()
        ResetFilters()
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
            If DataGridView1.Columns.Contains("ProductID") Then
                DataGridView1.Columns("ProductID").Visible = False
            End If

            DataGridView1.Columns("ProductName").HeaderText = "Product Name"
            DataGridView1.Columns("ProductName").FillWeight = 30

            DataGridView1.Columns("Category").HeaderText = "Category"
            DataGridView1.Columns("Category").FillWeight = 20

            DataGridView1.Columns("CostPrice").HeaderText = "Cost Price"
            DataGridView1.Columns("CostPrice").FillWeight = 15
            DataGridView1.Columns("CostPrice").DefaultCellStyle.Format = "C2"
            DataGridView1.Columns("CostPrice").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            DataGridView1.Columns("UnitPrice").HeaderText = "Unit Price"
            DataGridView1.Columns("UnitPrice").FillWeight = 15
            DataGridView1.Columns("UnitPrice").DefaultCellStyle.Format = "C2"
            DataGridView1.Columns("UnitPrice").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            DataGridView1.Columns("QuantityInStock").HeaderText = "Stock"
            DataGridView1.Columns("QuantityInStock").FillWeight = 10
            DataGridView1.Columns("QuantityInStock").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns("QuantityInStock").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If
    End Sub

    Private Sub InitializeCategories()
        cmbboxCategory.Items.Clear()
        ComboBox1.Items.Clear()

        Dim categories As String() = {"Drinks", "Easy to eat", "Miscellaneous", "Pastries"}

        cmbboxCategory.Items.AddRange(categories)
        cmbboxCategory.SelectedItem = "Miscellaneous"

        ComboBox1.Items.Add("All")
        ComboBox1.Items.AddRange(categories)
        ComboBox1.SelectedItem = "All"
    End Sub

    Private Sub RefreshProductsGrid()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT ProductID, ProductName, Category, CostPrice, UnitPrice, QuantityInStock FROM products WHERE 1=1"

                Dim searchName As String = TextBox1.Text.Trim()
                Dim searchCategory As String = ComboBox1.SelectedItem?.ToString()
                Dim minCost As Decimal = NumericUpDown1.Value
                Dim maxCost As Decimal = NumericUpDown2.Value
                Dim minUnit As Decimal = NumericUpDown4.Value
                Dim maxUnit As Decimal = NumericUpDown3.Value
                Dim maxStock As Decimal = NumericUpDown5.Value

                If Not String.IsNullOrEmpty(searchName) Then query &= " AND ProductName LIKE @name"
                If Not String.IsNullOrEmpty(searchCategory) AndAlso searchCategory <> "All" Then query &= " AND Category = @cat"
                If minCost > 0 Then query &= " AND CostPrice >= @minCost"
                If maxCost > 0 Then query &= " AND CostPrice <= @maxCost"
                If minUnit > 0 Then query &= " AND UnitPrice >= @minUnit"
                If maxUnit > 0 Then query &= " AND UnitPrice <= @maxUnit"
                If maxStock > 0 Then query &= " AND QuantityInStock <= @stock"

                Using cmd As New MySqlCommand(query, conn)
                    If Not String.IsNullOrEmpty(searchName) Then cmd.Parameters.AddWithValue("@name", "%" & searchName & "%")
                    If Not String.IsNullOrEmpty(searchCategory) AndAlso searchCategory <> "All" Then cmd.Parameters.AddWithValue("@cat", searchCategory)
                    If minCost > 0 Then cmd.Parameters.AddWithValue("@minCost", minCost)
                    If maxCost > 0 Then cmd.Parameters.AddWithValue("@maxCost", maxCost)
                    If minUnit > 0 Then cmd.Parameters.AddWithValue("@minUnit", minUnit)
                    If maxUnit > 0 Then cmd.Parameters.AddWithValue("@maxUnit", maxUnit)
                    If maxStock > 0 Then cmd.Parameters.AddWithValue("@stock", maxStock)

                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridView1.DataSource = dt
                End Using

                FormatColumns()

            Catch ex As Exception
                MessageBox.Show("Error rendering products view: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.RowIndex >= 0 Then
            Dim stockVal = DataGridView1.Rows(e.RowIndex).Cells("QuantityInStock").Value
            If stockVal IsNot Nothing AndAlso Not IsDBNull(stockVal) Then
                Dim stockCount As Integer = Convert.ToInt32(stockVal)
                If stockCount <= 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(255, 235, 238)
                    e.CellStyle.ForeColor = Color.FromArgb(183, 28, 28)
                    e.CellStyle.SelectionBackColor = Color.FromArgb(239, 154, 154)
                    e.CellStyle.SelectionForeColor = Color.FromArgb(183, 28, 28)
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click_Search(sender As Object, e As EventArgs) Handles Button1.Click
        RefreshProductsGrid()
    End Sub

    Private Sub Button2_Click_ResetFilters(sender As Object, e As EventArgs) Handles Button2.Click
        ResetFilters()
    End Sub

    Private Sub ResetFilters()
        TextBox1.Clear()
        ComboBox1.SelectedItem = "All"
        NumericUpDown1.Value = 0
        NumericUpDown2.Value = 0
        NumericUpDown4.Value = 0
        NumericUpDown3.Value = 0
        NumericUpDown5.Value = 0
        RefreshProductsGrid()
    End Sub

    Private Sub ResetInputs()
        selectedProductID = 0
        txtboxName.Clear()
        nudCostPrice.Value = nudCostPrice.Minimum
        nudUnitPrice.Value = nudUnitPrice.Minimum
        nudStockControl.Value = nudStockControl.Minimum
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
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim query As String = "INSERT INTO products (ProductName, CostPrice, UnitPrice, QuantityInStock, Category) VALUES (@name, @cost, @unit, 0, @cat)"
                        Using cmd As New MySqlCommand(query, conn, transaction)
                            cmd.Parameters.AddWithValue("@name", productName)
                            cmd.Parameters.AddWithValue("@cost", costPrice)
                            cmd.Parameters.AddWithValue("@unit", unitPrice)
                            cmd.Parameters.AddWithValue("@cat", selectedCategory)
                            cmd.ExecuteNonQuery()
                        End Using

                        Dim auditQuery As String = "INSERT INTO auditing (EmployeeID, TableName, ActionType) VALUES (@adminId, 'products', 'Insert')"
                        Using cmdAudit As New MySqlCommand(auditQuery, conn, transaction)
                            cmdAudit.Parameters.AddWithValue("@adminId", AccountData.AdminId)
                            cmdAudit.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                        MessageBox.Show($"'{productName}' successfully saved to catalog!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ResetInputs()
                        RefreshProductsGrid()

                    Catch ex As MySqlException When ex.Number = 1062
                        transaction.Rollback()
                        MessageBox.Show("An item with this product name already exists in the catalog system.", "Duplicate Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
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
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim query As String = "UPDATE products SET ProductName = @name, CostPrice = @cost, UnitPrice = @unit, Category = @cat WHERE ProductID = @pid"
                        Using cmd As New MySqlCommand(query, conn, transaction)
                            cmd.Parameters.AddWithValue("@name", productName)
                            cmd.Parameters.AddWithValue("@cost", costPrice)
                            cmd.Parameters.AddWithValue("@unit", unitPrice)
                            cmd.Parameters.AddWithValue("@cat", selectedCategory)
                            cmd.Parameters.AddWithValue("@pid", selectedProductID)
                            cmd.ExecuteNonQuery()
                        End Using

                        Dim auditQuery As String = "INSERT INTO auditing (EmployeeID, TableName, ActionType) VALUES (@adminId, 'products', 'Update')"
                        Using cmdAudit As New MySqlCommand(auditQuery, conn, transaction)
                            cmdAudit.Parameters.AddWithValue("@adminId", AccountData.AdminId)
                            cmdAudit.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                        MessageBox.Show("Product successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ResetInputs()
                        RefreshProductsGrid()

                    Catch ex As MySqlException When ex.Number = 1062
                        transaction.Rollback()
                        MessageBox.Show("Another product with this name already exists.", "Duplicate Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
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
                    Using transaction = conn.BeginTransaction()
                        Try
                            Dim query As String = "DELETE FROM products WHERE ProductID = @pid"
                            Using cmd As New MySqlCommand(query, conn, transaction)
                                cmd.Parameters.AddWithValue("@pid", selectedProductID)
                                cmd.ExecuteNonQuery()
                            End Using

                            Dim auditQuery As String = "INSERT INTO auditing (EmployeeID, TableName, ActionType) VALUES (@adminId, 'products', 'Delete')"
                            Using cmdAudit As New MySqlCommand(auditQuery, conn, transaction)
                                cmdAudit.Parameters.AddWithValue("@adminId", AccountData.AdminId)
                                cmdAudit.ExecuteNonQuery()
                            End Using

                            transaction.Commit()
                            MessageBox.Show("Product deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ResetInputs()
                            RefreshProductsGrid()

                        Catch ex As MySqlException When ex.Number = 1451
                            transaction.Rollback()
                            MessageBox.Show("Cannot delete this product because it has existing sales or transaction records attached to it.", "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Catch ex As Exception
                            transaction.Rollback()
                            Throw ex
                        End Try
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Deletion error: " & ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If selectedProductID = 0 Then
            MessageBox.Show("Please select a target row item inside the data view grid view first.", "Selection Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim newStockTarget As Integer = Convert.ToInt32(nudStockControl.Value)

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim query As String = "UPDATE products SET QuantityInStock = @stock WHERE ProductID = @pid"
                        Using cmd As New MySqlCommand(query, conn, transaction)
                            cmd.Parameters.AddWithValue("@stock", newStockTarget)
                            cmd.Parameters.AddWithValue("@pid", selectedProductID)
                            cmd.ExecuteNonQuery()
                        End Using

                        Dim auditQuery As String = "INSERT INTO auditing (EmployeeID, TableName, ActionType) VALUES (@adminId, 'products', 'Update')"
                        Using cmdAudit As New MySqlCommand(auditQuery, conn, transaction)
                            cmdAudit.Parameters.AddWithValue("@adminId", AccountData.AdminId)
                            cmdAudit.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                        MessageBox.Show("Inventory balance successfully adjusted!", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        RefreshProductsGrid()

                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            Catch ex As Exception
                MessageBox.Show("Database update error: " & ex.Message, "Execution Failure", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub btnGoBack_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
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
End Class