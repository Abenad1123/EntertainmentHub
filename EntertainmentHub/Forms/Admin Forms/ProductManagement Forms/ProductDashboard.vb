Imports MySql.Data.MySqlClient

Public Class ProductDashboard
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New AdminDashboard()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New UpdateProduct()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub ProductDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProductsGrid()
    End Sub

    Private Sub LoadProductsGrid()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "SELECT ProductID, ProductName, UnitPrice, QuantityInStock FROM products"

                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    DataGridView1.DataSource = dt
                End Using

                If DataGridView1.Columns.Count > 0 Then
                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                    If DataGridView1.Columns.Contains("UnitPrice") Then
                        DataGridView1.Columns("UnitPrice").DefaultCellStyle.Format = "C2"
                    End If
                End If

            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Error loading products", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("Unexpected Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private selectedProductID As Integer = 0

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            selectedProductID = Convert.ToInt32(row.Cells("ProductID").Value)

            Dim currentStock As Integer = Convert.ToInt32(row.Cells("QuantityInStock").Value)

            If currentStock > NumericUpDown1.Maximum Then
                NumericUpDown1.Maximum = currentStock + 100
            End If

            NumericUpDown1.Value = currentStock
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If selectedProductID = 0 Then
            MessageBox.Show("Please click on a product row from the grid first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim newStockValue As Integer = Convert.ToInt32(NumericUpDown1.Value)
        If newStockValue < 0 Then
            MessageBox.Show("Inventory stock cannot be a negative number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim query As String = "UPDATE products SET QuantityInStock = @newStock WHERE ProductID = @prodId"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@newStock", Convert.ToInt32(NumericUpDown1.Value))
                    cmd.Parameters.AddWithValue("@prodId", selectedProductID)

                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Inventory stock updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LoadProductsGrid()

            Catch ex As MySqlException
                MessageBox.Show("Database Error: " & ex.Message, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("Unexpected Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub
End Class