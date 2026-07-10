Imports System.Data
Imports System.Drawing
Imports System.Reflection.Emit
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class SystemSetting
    Private selectedMembershipID As Integer = 0
    Private selectedEntertainmentID As Integer = 0
    Private selectedTierID As Integer = 0

    Private Sub PricingConfiguration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        HelperFunc.EnableDoubleBuffer(Me)

        Me.BackgroundImage = AccountData.AdminCommonBackground
        Me.BackgroundImageLayout = ImageLayout.Stretch

        Label9.ForeColor = Color.FromArgb(255, 255, 255)
        Label9.Font = AppFonts.Aero(30)

        TabControl1.BackColor = Color.FromArgb(37, 36, 39)

        HelperFunc.ApplyButtonTheme(Button1)
        HelperFunc.ApplyButtonTheme(Button2)
        HelperFunc.ApplyButtonTheme(Button3)

        Dim ctrls As Control() = {Label1, Label2, Label3, Label4, Label5, Label6, Label7, Label8}
        For Each i In ctrls
            HelperFunc.FontDesign(i, Color.FromArgb(0, 0, 0), AppFonts.Coolvetica(16))
        Next

        StyleDataGridViews()
        LoadComboBoxes()
        LoadGrids()
    End Sub

    Private Sub StyleDataGridViews()
        Dim grids = {DataGridView1, DataGridView2, DataGridView3}

        For Each dgv In grids
            dgv.AllowUserToAddRows = False
            dgv.AllowUserToDeleteRows = False
            dgv.AllowUserToResizeRows = False
            dgv.ReadOnly = True
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv.MultiSelect = False
            dgv.RowHeadersVisible = False

            dgv.BackgroundColor = Color.White
            dgv.BorderStyle = BorderStyle.None
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal

            dgv.EnableHeadersVisualStyles = False
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48)
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            dgv.ColumnHeadersHeight = 40
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            dgv.DefaultCellStyle.BackColor = Color.White
            dgv.DefaultCellStyle.ForeColor = Color.Black
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255)
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black
            dgv.DefaultCellStyle.Font = New Font("Segoe UI", 9)
            dgv.DefaultCellStyle.Padding = New Padding(5, 0, 5, 0)

            dgv.RowTemplate.Height = 35
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Next
    End Sub

    Private Sub LoadComboBoxes()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Dim typeQuery As String = "SELECT EntertainmentTypeID, EntertainmentTypeName FROM entertainmenttype"
                Using cmdType As New MySqlCommand(typeQuery, conn)
                    Dim dtType As New DataTable()
                    Dim adapterType As New MySqlDataAdapter(cmdType)
                    adapterType.Fill(dtType)
                    ComboBox1.DataSource = dtType
                    ComboBox1.DisplayMember = "EntertainmentTypeName"
                    ComboBox1.ValueMember = "EntertainmentTypeID"
                    If ComboBox1.Items.Count > 0 Then ComboBox1.SelectedIndex = 0
                End Using

                Dim tierQuery As String = "SELECT EntertainmentTierID, EntertainmentTierName FROM entertainmenttier"
                Using cmdTier As New MySqlCommand(tierQuery, conn)
                    Dim dtTier As New DataTable()
                    Dim adapterTier As New MySqlDataAdapter(cmdTier)
                    adapterTier.Fill(dtTier)
                    ComboBox2.DataSource = dtTier
                    ComboBox2.DisplayMember = "EntertainmentTierName"
                    ComboBox2.ValueMember = "EntertainmentTierID"
                    If ComboBox2.Items.Count > 0 Then ComboBox2.SelectedIndex = 0
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading dropdowns: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub LoadGrids()
        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()

                Dim queryMem As String = "SELECT MembershipLevelID, MembershipLevelName, Price, Benefits, created_at, updated_at FROM membershiplevel"
                Using cmdMem As New MySqlCommand(queryMem, conn)
                    Dim dtMem As New DataTable()
                    Dim adapterMem As New MySqlDataAdapter(cmdMem)
                    adapterMem.Fill(dtMem)
                    DataGridView1.DataSource = dtMem
                End Using

                Dim queryEnt As String = "SELECT e.EntertainmentID, e.EntertainmentName, et.EntertainmentTierName FROM entertainment e JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID"
                Using cmdEnt As New MySqlCommand(queryEnt, conn)
                    Dim dtEnt As New DataTable()
                    Dim adapterEnt As New MySqlDataAdapter(cmdEnt)
                    adapterEnt.Fill(dtEnt)
                    DataGridView2.DataSource = dtEnt
                End Using

                Dim queryTier As String = "SELECT et.EntertainmentTierID, et.EntertainmentTierName, et.HourlyRate, etype.EntertainmentTypeName FROM entertainmenttier et JOIN entertainmenttype etype ON et.EntertainmentTypeID = etype.EntertainmentTypeID"
                Using cmdTier As New MySqlCommand(queryTier, conn)
                    Dim dtTier As New DataTable()
                    Dim adapterTier As New MySqlDataAdapter(cmdTier)
                    adapterTier.Fill(dtTier)
                    DataGridView3.DataSource = dtTier
                End Using

                FormatColumns()
            Catch ex As Exception
                MessageBox.Show("Error loading grid data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub FormatColumns()
        If DataGridView1.Columns.Count > 0 Then
            DataGridView1.Columns("MembershipLevelID").Visible = False
            DataGridView1.Columns("MembershipLevelName").HeaderText = "Membership Tier"
            DataGridView1.Columns("Price").HeaderText = "Price"
            DataGridView1.Columns("Price").DefaultCellStyle.Format = "C2"
            DataGridView1.Columns("Benefits").HeaderText = "Discount (%)"
            DataGridView1.Columns("created_at").HeaderText = "Date Created"
            DataGridView1.Columns("created_at").DefaultCellStyle.Format = "MMM dd, yyyy"
            DataGridView1.Columns("updated_at").HeaderText = "Last Updated"
            DataGridView1.Columns("updated_at").DefaultCellStyle.Format = "MMM dd, yyyy"
        End If

        If DataGridView2.Columns.Count > 0 Then
            DataGridView2.Columns("EntertainmentID").Visible = False
            DataGridView2.Columns("EntertainmentName").HeaderText = "Terminal Name"
            DataGridView2.Columns("EntertainmentTierName").HeaderText = "Hardware Tier"
        End If

        If DataGridView3.Columns.Count > 0 Then
            DataGridView3.Columns("EntertainmentTierID").Visible = False
            DataGridView3.Columns("EntertainmentTierName").HeaderText = "Tier Name"
            DataGridView3.Columns("HourlyRate").HeaderText = "Hourly Rate"
            DataGridView3.Columns("HourlyRate").DefaultCellStyle.Format = "C2"
            DataGridView3.Columns("EntertainmentTypeName").HeaderText = "Category"
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            selectedMembershipID = Convert.ToInt32(row.Cells("MembershipLevelID").Value)

            TextBox1.Text = row.Cells("MembershipLevelName").Value.ToString()
            NumericUpDown1.Value = Convert.ToDecimal(row.Cells("Price").Value)
            NumericUpDown2.Value = Convert.ToDecimal(row.Cells("Benefits").Value)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If selectedMembershipID = 0 Then
            MessageBox.Show("Please select a membership level from the grid first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Membership name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to save changes to this membership level?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()

                Using transaction = conn.BeginTransaction()
                    Try
                        Dim query As String = "UPDATE membershiplevel SET MembershipLevelName = @name, Price = @price, Benefits = @benefits WHERE MembershipLevelID = @id"
                        Using cmd As New MySqlCommand(query, conn, transaction)
                            cmd.Parameters.AddWithValue("@name", TextBox1.Text.Trim())
                            cmd.Parameters.AddWithValue("@price", NumericUpDown1.Value)
                            cmd.Parameters.AddWithValue("@benefits", NumericUpDown2.Value)
                            cmd.Parameters.AddWithValue("@id", selectedMembershipID)
                            cmd.ExecuteNonQuery()
                        End Using

                        Using cmdAudit As New MySqlCommand(GetAuditQuery("membershiplevel"), conn, transaction)
                            cmdAudit.Parameters.AddWithValue("@empId", AccountData.AdminId)
                            cmdAudit.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                        MessageBox.Show("Membership details successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadGrids()
                    Catch ex As MySqlException When ex.Number = 1062
                        transaction.Rollback()
                        MessageBox.Show("A membership level with this name already exists.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        transaction.Rollback()
                        MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            Catch ex As Exception
                MessageBox.Show("Database Connection Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
            selectedEntertainmentID = Convert.ToInt32(row.Cells("EntertainmentID").Value)

            TextBox3.Text = row.Cells("EntertainmentName").Value.ToString()

            Dim tierName As String = row.Cells("EntertainmentTierName").Value.ToString()
            Dim index = ComboBox2.FindStringExact(tierName)
            If index >= 0 Then ComboBox2.SelectedIndex = index
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If selectedEntertainmentID = 0 Then
            MessageBox.Show("Please select a terminal from the grid first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox3.Text) OrElse ComboBox2.SelectedValue Is Nothing Then
            MessageBox.Show("Terminal name and tier selection are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to save changes to this terminal?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim query As String = "UPDATE entertainment SET EntertainmentName = @name, EntertainmentTierID = @tierId WHERE EntertainmentID = @id"
                        Using cmd As New MySqlCommand(query, conn, transaction)
                            cmd.Parameters.AddWithValue("@name", TextBox3.Text.Trim())
                            cmd.Parameters.AddWithValue("@tierId", Convert.ToInt32(ComboBox2.SelectedValue))
                            cmd.Parameters.AddWithValue("@id", selectedEntertainmentID)
                            cmd.ExecuteNonQuery()
                        End Using

                        Using cmdAudit As New MySqlCommand(GetAuditQuery("entertainment"), conn, transaction)
                            cmdAudit.Parameters.AddWithValue("@empId", AccountData.AdminId)
                            cmdAudit.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                        MessageBox.Show("Terminal details successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadGrids()
                    Catch ex As MySqlException When ex.Number = 1062
                        transaction.Rollback()
                        MessageBox.Show("A terminal with this name already exists.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        transaction.Rollback()
                        MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            Catch ex As Exception
                MessageBox.Show("Database Connection Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView3.Rows(e.RowIndex)
            selectedTierID = Convert.ToInt32(row.Cells("EntertainmentTierID").Value)

            TextBox2.Text = row.Cells("EntertainmentTierName").Value.ToString()
            NumericUpDown3.Value = Convert.ToDecimal(row.Cells("HourlyRate").Value)

            Dim typeName As String = row.Cells("EntertainmentTypeName").Value.ToString()
            Dim index = ComboBox1.FindStringExact(typeName)
            If index >= 0 Then ComboBox1.SelectedIndex = index
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If selectedTierID = 0 Then
            MessageBox.Show("Please select a hardware tier from the grid first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(TextBox2.Text) OrElse ComboBox1.SelectedValue Is Nothing Then
            MessageBox.Show("Tier name and category type selection are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to save changes to this hardware tier?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Using conn = DBConnection.GetConnection()
            Try
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim query As String = "UPDATE entertainmenttier SET EntertainmentTierName = @name, HourlyRate = @rate, EntertainmentTypeID = @typeId WHERE EntertainmentTierID = @id"
                        Using cmd As New MySqlCommand(query, conn, transaction)
                            cmd.Parameters.AddWithValue("@name", TextBox2.Text.Trim())
                            cmd.Parameters.AddWithValue("@rate", NumericUpDown3.Value)
                            cmd.Parameters.AddWithValue("@typeId", Convert.ToInt32(ComboBox1.SelectedValue))
                            cmd.Parameters.AddWithValue("@id", selectedTierID)
                            cmd.ExecuteNonQuery()
                        End Using

                        Using cmdAudit As New MySqlCommand(GetAuditQuery("entertainmenttier"), conn, transaction)
                            cmdAudit.Parameters.AddWithValue("@empId", AccountData.AdminId)
                            cmdAudit.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                        MessageBox.Show("Hardware tier details successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadGrids()
                        LoadComboBoxes()
                    Catch ex As MySqlException When ex.Number = 1062
                        transaction.Rollback()
                        MessageBox.Show("A hardware tier with this name already exists.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        transaction.Rollback()
                        MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            Catch ex As Exception
                MessageBox.Show("Database Connection Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Function GetAuditQuery(tableName As String) As String
        Return $"INSERT INTO auditing (EmployeeID, TableName, ActionType) VALUES (@empId, '{tableName}', 'Update')"
    End Function

    Private Sub btnGoBack_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
        HelperFunc.SwitchForm(Me, New AdminDashboard())
    End Sub

    Private Sub BtnUserLoginEnter(sender As Object, e As EventArgs) Handles btnGoBack.MouseEnter
        btnGoBack.Image = My.Resources.go_back_state_2
    End Sub

    Private Sub BtnUserLoginLeave(sender As Object, e As EventArgs) Handles btnGoBack.MouseLeave
        btnGoBack.Image = My.Resources.go_back_state_1
    End Sub
End Class