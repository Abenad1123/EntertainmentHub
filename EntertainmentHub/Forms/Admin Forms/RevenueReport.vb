Imports System.IO
Imports System.Diagnostics
Imports System.Windows.Forms.DataVisualization.Charting
Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class RevenueReport

    Private ReadOnly connString As String = "server=localhost;user id=root;database=entertainmenthub"

    Private Sub RevenueReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        HelperFunc.EnableDoubleBuffer(Me)

        Me.BackgroundImage = AccountData.AdminCommonBackground
        Me.BackgroundImageLayout = ImageLayout.Stretch

        TableLayoutPanel2.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel2)

        TableLayoutPanel3.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel3)

        TabPage3.BackColor = Color.FromArgb(37, 36, 39)
        Panel1.BackColor = Color.FromArgb(37, 36, 39)

        cklsbxRevenueSourceToggle.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.FontDesign(cklsbxRevenueSourceToggle, Color.FromArgb(255, 255, 255), AppFonts.CdSaver(12))

        HelperFunc.FontDesign(grpbxReportRange, Color.FromArgb(255, 255, 255), AppFonts.CdSaver(12))

        Dim ctrls1 As Control() = {Label2, Label3, Label8}
        For Each i In ctrls1
            HelperFunc.FontDesign(i, Color.FromArgb(255, 255, 255), AppFonts.VenusRising(16))
        Next

        HelperFunc.ApplyBorder(Label2, HelperFunc.BorderSides.Bottom)
        HelperFunc.ApplyBorder(Label3, HelperFunc.BorderSides.Bottom)
        HelperFunc.ApplyBorder(Label8, HelperFunc.BorderSides.Top Or HelperFunc.BorderSides.Bottom)
        HelperFunc.ApplyBorder(TabControl2, HelperFunc.BorderSides.Right)

        Dim ctrls2 As Control() = {
            Label1, Label5, Label6, Label7, Label9,
            Label10, Label11, Label12, Label13, Label14,
            Label20, Label21, Label22, Label23, Label24,
            Label25, Label32, Label33, Label35
        }
        For Each i In ctrls2
            HelperFunc.FontDesign(i, Color.FromArgb(255, 255, 255), AppFonts.Coolvetica(16))
        Next

        Dim ctrls3 As Control() = {
            Label26, Label27, Label28, Label29, Label30, Label31,
            Label15, Label16, Label17, Label18, Label34, Label36,
            Label19
        }
        For Each i In ctrls3
            HelperFunc.FontDesign(i, Color.FromArgb(255, 255, 255), AppFonts.CdSaver(16))
        Next

        HelperFunc.ApplyButtonTheme(btnLoad)
        HelperFunc.ApplyButtonTheme(btnGenerate)
        HelperFunc.ApplyButtonTheme(btnPrint)

        StyleDataGridView()
        InitializeControls()
        SetYearPickerBounds()
        CheckRevenueSourceState()
        SetDefaultLabels()
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
        DataGridView1.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10, FontStyle.Bold)
        DataGridView1.ColumnHeadersHeight = 40
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        DataGridView1.DefaultCellStyle.BackColor = Color.White
        DataGridView1.DefaultCellStyle.ForeColor = Color.Black
        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255)
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black
        DataGridView1.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9)
        DataGridView1.DefaultCellStyle.Padding = New Padding(5, 0, 5, 0)

        DataGridView1.RowTemplate.Height = 35
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub InitializeControls()
        cmbboxFormat.Items.AddRange({"Column Chart", "Stacked Column Chart", "Line Chart", "Area Chart", "Spline Chart"})
        cmbboxFormat.SelectedIndex = 1
        rbbtnMonthly.Checked = True

        dtpckrDate.Format = DateTimePickerFormat.Custom
        dtpckrDate.CustomFormat = "yyyy"
        dtpckrDate.ShowUpDown = True

        DateTimePicker1.ShowUpDown = True
        DateTimePicker2.ShowUpDown = True

        AddHandler cmbboxFormat.SelectedIndexChanged, AddressOf ValidateChartFormat
        AddHandler cklsbxRevenueSourceToggle.ItemCheck, AddressOf RevenueSourceToggle_ItemCheck

        AddHandler rbbtnDaily.CheckedChanged, AddressOf UpdateDatePickerFormat
        AddHandler rbbtnWeekly.CheckedChanged, AddressOf UpdateDatePickerFormat
        AddHandler rbbtnMonthly.CheckedChanged, AddressOf UpdateDatePickerFormat

        AddHandler dtpckrDate.ValueChanged, AddressOf UpdateDatePickersBounds
        AddHandler DateTimePicker1.ValueChanged, AddressOf EnforceDateOrder
        AddHandler DateTimePicker2.ValueChanged, AddressOf EnforceDateOrder
    End Sub

    Private Sub SetDefaultLabels()
        Label26.Text = "0.00"
        Label27.Text = "0.00"
        Label28.Text = "0.00"
        Label29.Text = "0.00"
        Label15.Text = "0"
        Label16.Text = "0.00"
        Label17.Text = "0.00"
        Label18.Text = "0.00"
        Label34.Text = "0.00"
        Label30.Text = "0.00"
        Label36.Text = "0.00"
        Label31.Text = "0.00"
        Label19.Text = "0.00"
    End Sub

    Private Sub RevenueSourceToggle_ItemCheck(sender As Object, e As ItemCheckEventArgs)
        BeginInvoke(New Action(AddressOf CheckRevenueSourceState))
        BeginInvoke(New Action(AddressOf ValidateChartFormatSafe))
    End Sub

    Private Sub CheckRevenueSourceState()
        Dim hasSource As Boolean = cklsbxRevenueSourceToggle.CheckedItems.Count > 0
        DateTimePicker1.Enabled = hasSource
        DateTimePicker2.Enabled = hasSource
    End Sub

    Private Sub UpdateDatePickerFormat()
        If rbbtnMonthly.Checked Then
            DateTimePicker1.Format = DateTimePickerFormat.Custom
            DateTimePicker1.CustomFormat = "MMMM yyyy"
            DateTimePicker2.Format = DateTimePickerFormat.Custom
            DateTimePicker2.CustomFormat = "MMMM yyyy"
        Else
            DateTimePicker1.Format = DateTimePickerFormat.Short
            DateTimePicker2.Format = DateTimePickerFormat.Short
        End If
    End Sub

    Private Sub EnforceDateOrder(sender As Object, e As EventArgs)
        If DateTimePicker1.Value > DateTimePicker2.Value Then
            If sender Is DateTimePicker1 Then
                DateTimePicker2.Value = DateTimePicker1.Value
            Else
                DateTimePicker1.Value = DateTimePicker2.Value
            End If
        End If
    End Sub

    Private Sub ValidateChartFormat(sender As Object, e As EventArgs)
        ValidateChartFormatSafe()
    End Sub

    Private Sub ValidateChartFormatSafe()
        If cklsbxRevenueSourceToggle.CheckedItems.Count = 2 Then
            Dim sel As String = cmbboxFormat.SelectedItem.ToString()
            If sel = "Area Chart" Or sel = "Column Chart" Then
                cmbboxFormat.SelectedItem = "Stacked Column Chart"
            End If
        End If
    End Sub

    Private Sub SetYearPickerBounds()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Using cmd As New MySqlCommand("SELECT MIN(TransactionDate), MAX(TransactionDate) FROM wallettransactions", conn)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() AndAlso Not IsDBNull(reader(0)) Then
                            Dim minDate As DateTime = Convert.ToDateTime(reader(0))
                            Dim maxDate As DateTime = Convert.ToDateTime(reader(1))
                            dtpckrDate.MinDate = New DateTime(minDate.Year, 1, 1)
                            dtpckrDate.MaxDate = New DateTime(maxDate.Year, 12, 31)
                            dtpckrDate.Value = New DateTime(maxDate.Year, 1, 1)
                        Else
                            dtpckrDate.MinDate = New DateTime(DateTime.Now.Year, 1, 1)
                            dtpckrDate.MaxDate = New DateTime(DateTime.Now.Year, 12, 31)
                        End If
                    End Using
                End Using
            Catch ex As Exception
            End Try
        End Using
        UpdateDatePickersBounds()
    End Sub

    Private Sub UpdateDatePickersBounds()
        Dim selectedYear As Integer = dtpckrDate.Value.Year
        Dim minDbDate As DateTime = New DateTime(selectedYear, 1, 1)
        Dim maxDbDate As DateTime = New DateTime(selectedYear, 12, 31)
        Dim hasData As Boolean = False

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Dim qry As String = "SELECT MIN(TransactionDate), MAX(TransactionDate) FROM wallettransactions WHERE YEAR(TransactionDate) = @yr"
                Using cmd As New MySqlCommand(qry, conn)
                    cmd.Parameters.AddWithValue("@yr", selectedYear)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() AndAlso Not IsDBNull(reader(0)) Then
                            minDbDate = Convert.ToDateTime(reader(0)).Date
                            maxDbDate = Convert.ToDateTime(reader(1)).Date
                            hasData = True
                        End If
                    End Using
                End Using
            Catch ex As Exception
            End Try
        End Using

        If Not hasData Then
            minDbDate = New DateTime(selectedYear, 1, 1)
            maxDbDate = New DateTime(selectedYear, 12, 31)
        End If

        DateTimePicker1.MinDate = DateTimePicker.MinimumDateTime
        DateTimePicker1.MaxDate = DateTimePicker.MaximumDateTime
        DateTimePicker2.MinDate = DateTimePicker.MinimumDateTime
        DateTimePicker2.MaxDate = DateTimePicker.MaximumDateTime

        DateTimePicker1.Value = minDbDate
        DateTimePicker1.MinDate = minDbDate
        DateTimePicker1.MaxDate = maxDbDate

        DateTimePicker2.Value = maxDbDate
        DateTimePicker2.MinDate = minDbDate
        DateTimePicker2.MaxDate = maxDbDate

        UpdateDatePickerFormat()
    End Sub

    Private Function GetDateGrouping() As String
        If rbbtnDaily.Checked Then Return "DATE_FORMAT(TransactionDate, '%b %d, %Y')"
        If rbbtnWeekly.Checked Then Return "CONCAT('Wk ', WEEK(TransactionDate), ' - ', YEAR(TransactionDate))"
        Return "DATE_FORMAT(TransactionDate, '%b %Y')"
    End Function

    Private Function GetStartDate() As DateTime
        Dim dt As DateTime = DateTimePicker1.Value.Date
        If rbbtnMonthly.Checked Then Return New DateTime(dt.Year, dt.Month, 1)
        Return dt
    End Function

    Private Function GetEndDate() As DateTime
        Dim dt As DateTime = DateTimePicker2.Value.Date
        If rbbtnMonthly.Checked Then Return New DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59)
        Return dt.AddDays(1).AddSeconds(-1)
    End Function

    Private Function GetTotalByTransactionType(transType As String, d1 As DateTime, d2 As DateTime) As Decimal
        Dim qry As String = "SELECT SUM(Amount) FROM wallettransactions WHERE TransactionType = @type AND TransactionDate >= @d1 AND TransactionDate <= @d2"
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Using cmd As New MySqlCommand(qry, conn)
                    cmd.Parameters.AddWithValue("@type", transType)
                    cmd.Parameters.AddWithValue("@d1", d1)
                    cmd.Parameters.AddWithValue("@d2", d2)
                    Dim res = cmd.ExecuteScalar()
                    If res IsNot Nothing AndAlso Not IsDBNull(res) Then Return Convert.ToDecimal(res)
                End Using
            Catch ex As Exception
                MessageBox.Show("GetTotalByTransactionType Error: " & ex.Message)
            End Try
        End Using
        Return 0
    End Function

    Private Function GetTransactionCount(transType As String, d1 As DateTime, d2 As DateTime) As Integer
        Dim qry As String = "SELECT COUNT(*) FROM wallettransactions WHERE TransactionType = @type AND TransactionDate >= @d1 AND TransactionDate <= @d2"
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Using cmd As New MySqlCommand(qry, conn)
                    cmd.Parameters.AddWithValue("@type", transType)
                    cmd.Parameters.AddWithValue("@d1", d1)
                    cmd.Parameters.AddWithValue("@d2", d2)
                    Dim res = cmd.ExecuteScalar()
                    If res IsNot Nothing AndAlso Not IsDBNull(res) Then Return Convert.ToInt32(res)
                End Using
            Catch ex As Exception
                MessageBox.Show("GetTransactionCount Error: " & ex.Message)
            End Try
        End Using
        Return 0
    End Function

    Private Function GetRevenueBySource(sourceType As String, d1 As DateTime, d2 As DateTime) As Decimal
        Dim sessionFilter As String = If(sourceType = "Session", "IS NOT NULL", "IS NULL")
        Dim qry As String = $"SELECT SUM(Amount) FROM wallettransactions WHERE TransactionType = 'Payment' AND EntertainmentSessionID {sessionFilter} AND TransactionDate >= @d1 AND TransactionDate <= @d2"
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Using cmd As New MySqlCommand(qry, conn)
                    cmd.Parameters.AddWithValue("@d1", d1)
                    cmd.Parameters.AddWithValue("@d2", d2)
                    Dim res = cmd.ExecuteScalar()
                    If res IsNot Nothing AndAlso Not IsDBNull(res) Then Return Math.Abs(Convert.ToDecimal(res))
                End Using
            Catch ex As Exception
                MessageBox.Show("GetRevenueBySource Error: " & ex.Message)
            End Try
        End Using
        Return 0
    End Function

    Private Function GetCostOfGoodsSold(d1 As DateTime, d2 As DateTime) As Decimal
        Dim qry As String = "SELECT SUM(si.Quantity * si.CostPrice) FROM wallettransactions wt JOIN salesitem si ON wt.SaleID = si.SaleID WHERE wt.TransactionType = 'Payment' AND wt.TransactionDate >= @d1 AND wt.TransactionDate <= @d2"
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Using cmd As New MySqlCommand(qry, conn)
                    cmd.Parameters.AddWithValue("@d1", d1)
                    cmd.Parameters.AddWithValue("@d2", d2)
                    Dim res = cmd.ExecuteScalar()
                    If res IsNot Nothing AndAlso Not IsDBNull(res) Then Return Convert.ToDecimal(res)
                End Using
            Catch ex As Exception
                MessageBox.Show("GetCostOfGoodsSold Error: " & ex.Message)
            End Try
        End Using
        Return 0
    End Function

    Private Function GetPeakEarningPeriod(d1 As DateTime, d2 As DateTime) As String
        Dim qry As String = "SELECT CONCAT(DAYNAME(TransactionDate), ', ', DATE_FORMAT(TransactionDate, '%h:00 %p')) FROM wallettransactions WHERE TransactionType = 'Payment' AND TransactionDate >= @d1 AND TransactionDate <= @d2 GROUP BY DAYNAME(TransactionDate), HOUR(TransactionDate) ORDER BY SUM(Amount) ASC LIMIT 1"
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Using cmd As New MySqlCommand(qry, conn)
                    cmd.Parameters.AddWithValue("@d1", d1)
                    cmd.Parameters.AddWithValue("@d2", d2)
                    Dim res = cmd.ExecuteScalar()
                    If res IsNot Nothing AndAlso Not IsDBNull(res) Then Return res.ToString()
                End Using
            Catch ex As Exception
                MessageBox.Show("GetPeakEarningPeriod Error: " & ex.Message)
            End Try
        End Using
        Return "N/A"
    End Function

    Private Function ValidateSelections() As Boolean
        If cklsbxRevenueSourceToggle.CheckedItems.Count = 0 Then
            MessageBox.Show("Please select at least one Revenue Source before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If Not (rbbtnDaily.Checked Or rbbtnWeekly.Checked Or rbbtnMonthly.Checked) Then
            MessageBox.Show("Please select a Report Range.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        If Not ValidateSelections() Then Return
        Dim d1 As DateTime = GetStartDate()
        Dim d2 As DateTime = GetEndDate()

        LoadChartData(d1, d2)
        LoadTransactionGrid(d1, d2)
        LoadEntertainmentChart(d1, d2)
        LoadProductChart(d1, d2)
        UpdateFinancialSummary(d1, d2)
    End Sub

    Private Sub LoadChartData(d1 As DateTime, d2 As DateTime)
        Chart1.Series.Clear()
        Dim chartType As SeriesChartType = SeriesChartType.Column
        Select Case cmbboxFormat.SelectedItem.ToString()
            Case "Stacked Column Chart" : chartType = SeriesChartType.StackedColumn
            Case "Line Chart" : chartType = SeriesChartType.Line
            Case "Area Chart" : chartType = SeriesChartType.Area
            Case "Spline Chart" : chartType = SeriesChartType.Spline
        End Select

        Dim dateGroup As String = GetDateGrouping()

        For Each item In cklsbxRevenueSourceToggle.CheckedItems
            Dim isSession As Boolean = (item.ToString() = "Entertainment Sessions")
            Dim s As New Series(item.ToString())
            s.ChartType = chartType
            Chart1.Series.Add(s)

            Dim filter As String = "TransactionType = 'Payment' AND TransactionDate >= @d1 AND TransactionDate <= @d2"
            If isSession Then
                filter &= " AND EntertainmentSessionID IS NOT NULL"
            Else
                filter &= " AND EntertainmentSessionID IS NULL"
            End If

            Dim qry As String = $"SELECT {dateGroup} as period, SUM(ABS(Amount)) as total, MIN(TransactionDate) as sortOrder FROM wallettransactions WHERE {filter} GROUP BY period ORDER BY sortOrder"

            Using conn As New MySqlConnection(connString)
                Try
                    conn.Open()
                    Using cmd As New MySqlCommand(qry, conn)
                        cmd.Parameters.AddWithValue("@d1", d1)
                        cmd.Parameters.AddWithValue("@d2", d2)
                        Using reader = cmd.ExecuteReader()
                            While reader.Read()
                                s.Points.AddXY(reader("period").ToString(), Convert.ToDouble(reader("total")))
                            End While
                        End Using
                    End Using
                Catch ex As Exception
                    MessageBox.Show("LoadChartData Error: " & ex.Message)
                End Try
            End Using
        Next
    End Sub

    Private Sub LoadTransactionGrid(d1 As DateTime, d2 As DateTime)
        Dim dateGroup As String = GetDateGrouping()
        Dim qry As String = $"SELECT {dateGroup} AS Period, SUM(ABS(Amount)) AS TotalAmount, COUNT(WalletTransactionID) AS TotalTransactions, (SUM(ABS(Amount)) / COUNT(WalletTransactionID)) AS MeanTransaction FROM wallettransactions WHERE TransactionType = 'Payment' AND TransactionDate >= @d1 AND TransactionDate <= @d2 GROUP BY period ORDER BY MIN(TransactionDate)"

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Using cmd As New MySqlCommand(qry, conn)
                    cmd.Parameters.AddWithValue("@d1", d1)
                    cmd.Parameters.AddWithValue("@d2", d2)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridView1.DataSource = dt
                End Using

                If DataGridView1.Columns.Count > 0 Then
                    DataGridView1.Columns("Period").HeaderText = "Time Period"
                    DataGridView1.Columns("TotalAmount").HeaderText = "Total Revenue"
                    DataGridView1.Columns("TotalAmount").DefaultCellStyle.Format = "C2"
                    DataGridView1.Columns("TotalTransactions").HeaderText = "Total Transactions"
                    DataGridView1.Columns("MeanTransaction").HeaderText = "Average Spend"
                    DataGridView1.Columns("MeanTransaction").DefaultCellStyle.Format = "C2"
                End If
            Catch ex As Exception
                MessageBox.Show("LoadTransactionGrid Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadEntertainmentChart(d1 As DateTime, d2 As DateTime)
        Chart2.Series.Clear()
        Dim s As New Series("Entertainment")
        s.ChartType = SeriesChartType.Pie
        s.IsValueShownAsLabel = True
        s.Label = "#PERCENT{P1}"
        s.LegendText = "#VALX"
        Chart2.Series.Add(s)

        Dim qry As String = "SELECT et.EntertainmentTierName, SUM(ABS(wt.Amount)) as TotalVal FROM wallettransactions wt JOIN entertainmentsession es ON wt.EntertainmentSessionID = es.EntertainmentSessionID JOIN entertainment e ON es.EntertainmentID = e.EntertainmentID JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID WHERE wt.TransactionType = 'Payment' AND wt.TransactionDate >= @d1 AND wt.TransactionDate <= @d2 GROUP BY et.EntertainmentTierID, et.EntertainmentTierName ORDER BY TotalVal DESC"

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Using cmd As New MySqlCommand(qry, conn)
                    cmd.Parameters.AddWithValue("@d1", d1)
                    cmd.Parameters.AddWithValue("@d2", d2)
                    Using reader = cmd.ExecuteReader()
                        Dim count As Integer = 0
                        Dim otherTotal As Double = 0
                        While reader.Read()
                            Dim val As Double = Convert.ToDouble(reader("TotalVal"))
                            If count < 10 Then
                                s.Points.AddXY(reader("EntertainmentTierName").ToString(), val)
                            Else
                                otherTotal += val
                            End If
                            count += 1
                        End While
                        If otherTotal > 0 Then
                            s.Points.AddXY("Other", otherTotal)
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Chart Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadProductChart(d1 As DateTime, d2 As DateTime)
        Chart3.Series.Clear()
        Dim s As New Series("Products")
        s.ChartType = SeriesChartType.Pie
        s.IsValueShownAsLabel = True
        s.Label = "#PERCENT{P1}"
        s.LegendText = "#VALX"
        Chart3.Series.Add(s)

        Dim qry As String = "SELECT p.ProductName, SUM(si.Quantity * si.UnitPrice) as TotalVal FROM wallettransactions wt JOIN salesitem si ON wt.SaleID = si.SaleID JOIN products p ON si.ProductID = p.ProductID WHERE wt.TransactionType = 'Payment' AND wt.TransactionDate >= @d1 AND wt.TransactionDate <= @d2 GROUP BY p.ProductID, p.ProductName ORDER BY TotalVal DESC"

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Using cmd As New MySqlCommand(qry, conn)
                    cmd.Parameters.AddWithValue("@d1", d1)
                    cmd.Parameters.AddWithValue("@d2", d2)
                    Using reader = cmd.ExecuteReader()
                        Dim count As Integer = 0
                        Dim otherTotal As Double = 0
                        While reader.Read()
                            Dim val As Double = Convert.ToDouble(reader("TotalVal"))
                            If count < 10 Then
                                s.Points.AddXY(reader("ProductName").ToString(), val)
                            Else
                                otherTotal += val
                            End If
                            count += 1
                        End While
                        If otherTotal > 0 Then
                            s.Points.AddXY("Other", otherTotal)
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Chart Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub UpdateFinancialSummary(d1 As DateTime, d2 As DateTime)
        Dim dep As Decimal = GetTotalByTransactionType("Deposit", d1, d2)
        Dim wth As Decimal = Math.Abs(GetTotalByTransactionType("Withdrawal", d1, d2))
        Dim bon As Decimal = GetTotalByTransactionType("Bonus", d1, d2)
        Dim adj As Decimal = Math.Abs(GetTotalByTransactionType("Adjustment", d1, d2))
        Dim ref As Decimal = Math.Abs(GetTotalByTransactionType("Refund", d1, d2))
        Dim transCount As Integer = GetTransactionCount("Payment", d1, d2)

        Dim grossSes As Decimal = 0
        Dim grossPro As Decimal = 0
        Dim cogs As Decimal = 0

        If cklsbxRevenueSourceToggle.CheckedItems.Contains("Entertainment Sessions") Then
            grossSes = GetRevenueBySource("Session", d1, d2)
        End If
        If cklsbxRevenueSourceToggle.CheckedItems.Contains("Product Sales") Then
            grossPro = GetRevenueBySource("Product", d1, d2)
            cogs = GetCostOfGoodsSold(d1, d2)
        End If

        Dim monthlyOverhead As Decimal = NumericUpDown1.Value + NumericUpDown2.Value + NumericUpDown3.Value + NumericUpDown4.Value + NumericUpDown5.Value
        Dim dailyCost As Decimal = monthlyOverhead / 30D
        Dim daysInRange As Integer = (d2.Date - d1.Date).Days + 1
        If daysInRange < 1 Then daysInRange = 1
        Dim opEx As Decimal = dailyCost * daysInRange

        Dim totalSales As Decimal = grossSes + grossPro
        Dim grossProfit As Decimal = totalSales - cogs
        Dim netIncome As Decimal = grossProfit - (opEx + ref)
        Dim avgSpend As Decimal = If(transCount > 0, totalSales / transCount, 0)

        Label26.Text = dep.ToString("C2")
        Label27.Text = wth.ToString("C2")
        Label28.Text = bon.ToString("C2")
        Label29.Text = adj.ToString("C2")

        Label15.Text = transCount.ToString()
        Label16.Text = avgSpend.ToString("C2")

        Label17.Text = grossSes.ToString("C2")
        Label18.Text = grossPro.ToString("C2")
        Label34.Text = cogs.ToString("C2")
        Label30.Text = grossProfit.ToString("C2")

        Label36.Text = opEx.ToString("C2")
        Label31.Text = ref.ToString("C2")

        Label19.Text = netIncome.ToString("C2")
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        If Not ValidateSelections() Then Return
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "PDF Document (*.pdf)|*.pdf"
        sfd.FileName = $"RevenueReport_{DateTime.Now.ToString("yyyyMMdd")}.pdf"
        If sfd.ShowDialog() = DialogResult.OK Then
            GeneratePDF(sfd.FileName)
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If Not ValidateSelections() Then Return
        Dim tempPath As String = Path.Combine(Path.GetTempPath(), $"Print_{Guid.NewGuid().ToString()}.pdf")
        GeneratePDF(tempPath)
        Try
            Dim proc As New Process()
            proc.StartInfo.FileName = tempPath
            proc.StartInfo.Verb = "Print"
            proc.StartInfo.CreateNoWindow = True
            proc.Start()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GeneratePDF(filePath As String)
        Dim doc As New Document(PageSize.A4, 35, 35, 40, 45)
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(filePath, FileMode.Create))
        writer.PageEvent = New PDFHeaderFooter()
        doc.Open()

        If My.Resources.full_logo IsNot Nothing Then
            Using ms As New MemoryStream()
                Dim bmp As New System.Drawing.Bitmap(My.Resources.full_logo, 1100, 400)
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(ms.ToArray())
                logo.Alignment = Element.ALIGN_CENTER
                logo.ScaleToFit(350.0F, 127.0F)
                logo.SpacingAfter = 10.0F
                doc.Add(logo)
            End Using
        End If

        Dim titleFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY)
        Dim subFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 11, BaseColor.GRAY)

        Dim header As New Paragraph("FINANCIAL REVENUE REPORT", titleFont)
        header.Alignment = Element.ALIGN_CENTER
        header.SpacingAfter = 5.0F
        doc.Add(header)

        Dim d1Str As String = If(rbbtnMonthly.Checked, GetStartDate().ToString("MMMM yyyy"), GetStartDate().ToString("MMM dd, yyyy"))
        Dim d2Str As String = If(rbbtnMonthly.Checked, GetEndDate().ToString("MMMM yyyy"), GetEndDate().ToString("MMM dd, yyyy"))
        Dim subHeader As New Paragraph($"Reporting Period: {d1Str} to {d2Str}", subFont)
        subHeader.Alignment = Element.ALIGN_CENTER
        subHeader.SpacingAfter = 20.0F
        doc.Add(subHeader)

        If Chart1.Series.Count > 0 Then
            Using msChart As New MemoryStream()
                Chart1.SaveImage(msChart, System.Drawing.Imaging.ImageFormat.Png)
                Dim chartImg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(msChart.ToArray())
                chartImg.Alignment = Element.ALIGN_CENTER
                chartImg.ScaleToFit(480.0F, 240.0F)
                chartImg.SpacingAfter = 25.0F
                doc.Add(chartImg)
            End Using
        End If

        Dim tableHeaderFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE)
        Dim tableCellFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)
        Dim tableTitleFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.DARK_GRAY)
        Dim headerBgColor As New BaseColor(41, 128, 185)

        For Each series As Series In Chart1.Series
            Dim tableTitle As New Paragraph(series.Name & " Data Breakdown", tableTitleFont)
            tableTitle.SpacingAfter = 8.0F
            doc.Add(tableTitle)

            Dim table As New PdfPTable(2)
            table.WidthPercentage = 100
            table.SetWidths(New Single() {0.5F, 0.5F})
            table.KeepTogether = True

            Dim cellPeriod As New PdfPCell(New Phrase("Period", tableHeaderFont))
            cellPeriod.BackgroundColor = headerBgColor
            cellPeriod.Padding = 8.0F
            cellPeriod.Border = Rectangle.NO_BORDER
            table.AddCell(cellPeriod)

            Dim cellRev As New PdfPCell(New Phrase("Revenue", tableHeaderFont))
            cellRev.BackgroundColor = headerBgColor
            cellRev.Padding = 8.0F
            cellRev.HorizontalAlignment = Element.ALIGN_RIGHT
            cellRev.Border = Rectangle.NO_BORDER
            table.AddCell(cellRev)

            Dim rowCount As Integer = 0
            For Each p As DataPoint In series.Points
                Dim cell1 As New PdfPCell(New Phrase(p.AxisLabel.ToString(), tableCellFont))
                Dim cell2 As New PdfPCell(New Phrase(p.YValues(0).ToString("C2"), tableCellFont))

                cell1.Padding = 7.0F
                cell2.Padding = 7.0F
                cell2.HorizontalAlignment = Element.ALIGN_RIGHT
                cell1.BorderColor = BaseColor.LIGHT_GRAY
                cell2.BorderColor = BaseColor.LIGHT_GRAY
                cell1.BorderWidth = 0.5F
                cell2.BorderWidth = 0.5F

                If rowCount Mod 2 <> 0 Then
                    Dim altBg As New BaseColor(245, 247, 250)
                    cell1.BackgroundColor = altBg
                    cell2.BackgroundColor = altBg
                End If

                table.AddCell(cell1)
                table.AddCell(cell2)
                rowCount += 1
            Next
            table.SpacingAfter = 25.0F
            doc.Add(table)
        Next

        If Chart2.Series(0).Points.Count > 0 OrElse Chart3.Series(0).Points.Count > 0 Then
            Dim pieTitle As New Paragraph("Popularity & Trend Analysis", tableTitleFont)
            pieTitle.SpacingAfter = 8.0F
            doc.Add(pieTitle)

            Dim pieTable As New PdfPTable(2)
            pieTable.WidthPercentage = 100
            pieTable.SetWidths(New Single() {0.5F, 0.5F})
            pieTable.KeepTogether = True
            pieTable.SpacingAfter = 25.0F

            If Chart2.Series(0).Points.Count > 0 Then
                Using ms2 As New MemoryStream()
                    Chart2.SaveImage(ms2, System.Drawing.Imaging.ImageFormat.Png)
                    Dim img2 As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(ms2.ToArray())
                    img2.ScaleToFit(240.0F, 180.0F)
                    Dim cell As New PdfPCell(img2)
                    cell.Border = Rectangle.NO_BORDER
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    pieTable.AddCell(cell)
                End Using
            Else
                Dim emptyCell As New PdfPCell(New Phrase(" "))
                emptyCell.Border = Rectangle.NO_BORDER
                pieTable.AddCell(emptyCell)
            End If

            If Chart3.Series(0).Points.Count > 0 Then
                Using ms3 As New MemoryStream()
                    Chart3.SaveImage(ms3, System.Drawing.Imaging.ImageFormat.Png)
                    Dim img3 As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(ms3.ToArray())
                    img3.ScaleToFit(240.0F, 180.0F)
                    Dim cell As New PdfPCell(img3)
                    cell.Border = Rectangle.NO_BORDER
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    pieTable.AddCell(cell)
                End Using
            Else
                Dim emptyCell As New PdfPCell(New Phrase(" "))
                emptyCell.Border = Rectangle.NO_BORDER
                pieTable.AddCell(emptyCell)
            End If
            doc.Add(pieTable)
        End If

        AppendFinancialSummariesToPDF(doc)
        doc.Close()
    End Sub

    Private Sub AppendFinancialSummariesToPDF(doc As Document)
        Dim d1 As DateTime = GetStartDate()
        Dim d2 As DateTime = GetEndDate()

        Dim dep As Decimal = GetTotalByTransactionType("Deposit", d1, d2)
        Dim wth As Decimal = Math.Abs(GetTotalByTransactionType("Withdrawal", d1, d2))
        Dim bon As Decimal = GetTotalByTransactionType("Bonus", d1, d2)
        Dim adj As Decimal = Math.Abs(GetTotalByTransactionType("Adjustment", d1, d2))
        Dim ref As Decimal = Math.Abs(GetTotalByTransactionType("Refund", d1, d2))

        Dim transCount As Integer = GetTransactionCount("Payment", d1, d2)
        Dim peak As String = GetPeakEarningPeriod(d1, d2)

        Dim grossSes As Decimal = 0
        Dim grossPro As Decimal = 0
        Dim cogs As Decimal = 0

        If cklsbxRevenueSourceToggle.CheckedItems.Contains("Entertainment Sessions") Then
            grossSes = GetRevenueBySource("Session", d1, d2)
        End If
        If cklsbxRevenueSourceToggle.CheckedItems.Contains("Product Sales") Then
            grossPro = GetRevenueBySource("Product", d1, d2)
            cogs = GetCostOfGoodsSold(d1, d2)
        End If

        Dim salary As Decimal = NumericUpDown1.Value
        Dim water As Decimal = NumericUpDown2.Value
        Dim elec As Decimal = NumericUpDown3.Value
        Dim internet As Decimal = NumericUpDown4.Value
        Dim maint As Decimal = NumericUpDown5.Value

        Dim monthlyOverhead As Decimal = salary + water + elec + internet + maint
        Dim dailyCost As Decimal = monthlyOverhead / 30D
        Dim daysInRange As Integer = (d2.Date - d1.Date).Days + 1
        If daysInRange < 1 Then daysInRange = 1
        Dim opEx As Decimal = dailyCost * daysInRange

        Dim totalSales As Decimal = grossSes + grossPro
        Dim grossProfit As Decimal = totalSales - cogs
        Dim netIncome As Decimal = grossProfit - (opEx + ref)

        Dim cellFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)
        Dim boldFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK)
        Dim titleFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE)
        Dim headerBgColor As New BaseColor(44, 62, 80)
        Dim highlightBg As New BaseColor(236, 240, 241)

        Dim cashTable As New PdfPTable(2)
        cashTable.WidthPercentage = 100
        cashTable.SetWidths(New Single() {0.65F, 0.35F})
        cashTable.SpacingBefore = 10.0F
        cashTable.KeepTogether = True

        Dim cashHeader As New PdfPCell(New Phrase("CASH FLOW & WALLET SUMMARY", titleFont))
        cashHeader.BackgroundColor = headerBgColor
        cashHeader.Colspan = 2
        cashHeader.Padding = 8.0F
        cashHeader.Border = Rectangle.NO_BORDER
        cashTable.AddCell(cashHeader)

        AddStyledRow(cashTable, "Total Cash In (Deposits)", dep.ToString("C2"), cellFont)
        AddStyledRow(cashTable, "Total Cash Out (Withdrawals)", wth.ToString("C2"), cellFont)
        AddStyledRow(cashTable, "Promotional Bonuses Issued", bon.ToString("C2"), cellFont)
        AddStyledRow(cashTable, "Manual Wallet Adjustments", adj.ToString("C2"), cellFont)
        doc.Add(cashTable)

        Dim opexTable As New PdfPTable(2)
        opexTable.WidthPercentage = 100
        opexTable.SetWidths(New Single() {0.65F, 0.35F})
        opexTable.SpacingBefore = 20.0F
        opexTable.KeepTogether = True

        Dim opexHeader As New PdfPCell(New Phrase("OPERATING EXPENSES BREAKDOWN (MONTHLY OVERHEAD)", titleFont))
        opexHeader.BackgroundColor = headerBgColor
        opexHeader.Colspan = 2
        opexHeader.Padding = 8.0F
        opexHeader.Border = Rectangle.NO_BORDER
        opexTable.AddCell(opexHeader)

        AddStyledRow(opexTable, "Employee Salary", salary.ToString("C2"), cellFont)
        AddStyledRow(opexTable, "Water", water.ToString("C2"), cellFont)
        AddStyledRow(opexTable, "Electricity", elec.ToString("C2"), cellFont)
        AddStyledRow(opexTable, "Internet", internet.ToString("C2"), cellFont)
        AddStyledRow(opexTable, "Maintenance", maint.ToString("C2"), cellFont)
        AddStyledRow(opexTable, "TOTAL MONTHLY OVERHEAD", monthlyOverhead.ToString("C2"), boldFont, highlightBg)
        AddStyledRow(opexTable, $"Prorated Operational Cost ({daysInRange} days)", opEx.ToString("C2"), boldFont, highlightBg)
        doc.Add(opexTable)

        Dim revTable As New PdfPTable(2)
        revTable.WidthPercentage = 100
        revTable.SetWidths(New Single() {0.65F, 0.35F})
        revTable.SpacingBefore = 20.0F
        revTable.KeepTogether = True

        Dim revHeader As New PdfPCell(New Phrase("REVENUE & INCOME SUMMARY", titleFont))
        revHeader.BackgroundColor = headerBgColor
        revHeader.Colspan = 2
        revHeader.Padding = 8.0F
        revHeader.Border = Rectangle.NO_BORDER
        revTable.AddCell(revHeader)

        AddStyledRow(revTable, "Total Transactions", transCount.ToString(), cellFont)
        AddStyledRow(revTable, "Peak Earning Period", peak, cellFont)
        AddStyledRow(revTable, "Total Session Sales", grossSes.ToString("C2"), cellFont)
        AddStyledRow(revTable, "Total Product Sales", grossPro.ToString("C2"), cellFont)
        AddStyledRow(revTable, "Less: Cost of Goods Sold", cogs.ToString("C2"), cellFont)
        AddStyledRow(revTable, "GROSS PROFIT", grossProfit.ToString("C2"), boldFont)
        AddStyledRow(revTable, "Less: Operating Expenses", opEx.ToString("C2"), cellFont)
        AddStyledRow(revTable, "Less: Refunds Processed", ref.ToString("C2"), cellFont)
        AddStyledRow(revTable, "NET EARNED INCOME", netIncome.ToString("C2"), boldFont, highlightBg)
        doc.Add(revTable)
    End Sub

    Private Sub AddStyledRow(table As PdfPTable, label As String, val As String, font As iTextSharp.text.Font, Optional bgColor As BaseColor = Nothing)
        Dim cellLabel As New PdfPCell(New Phrase(label, font))
        cellLabel.Padding = 7.0F
        cellLabel.BorderColor = BaseColor.LIGHT_GRAY
        cellLabel.BorderWidth = 0.5F
        If bgColor IsNot Nothing Then cellLabel.BackgroundColor = bgColor
        table.AddCell(cellLabel)

        Dim cellVal As New PdfPCell(New Phrase(val, font))
        cellVal.Padding = 7.0F
        cellVal.HorizontalAlignment = Element.ALIGN_RIGHT
        cellVal.BorderColor = BaseColor.LIGHT_GRAY
        cellVal.BorderWidth = 0.5F
        If bgColor IsNot Nothing Then cellVal.BackgroundColor = bgColor
        table.AddCell(cellVal)
    End Sub

    Private Class PDFHeaderFooter
        Inherits PdfPageEventHelper

        Public Overrides Sub OnEndPage(writer As PdfWriter, document As Document)
            Dim cb As PdfContentByte = writer.DirectContent
            Dim font As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.GRAY)

            Dim printDate As String = "Date Printed: " & DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt")
            Dim pageText As String = "Page " & writer.PageNumber

            ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, New Phrase(printDate, font), document.LeftMargin, document.BottomMargin - 20, 0)
            ColumnText.ShowTextAligned(cb, Element.ALIGN_RIGHT, New Phrase(pageText, font), document.RightMargin, document.BottomMargin - 20, 0)

            Dim lineY As Single = document.BottomMargin - 5
            cb.SetColorStroke(BaseColor.LIGHT_GRAY)
            cb.MoveTo(document.LeftMargin, lineY)
            cb.LineTo(document.RightMargin, lineY)
            cb.Stroke()
        End Sub
    End Class

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