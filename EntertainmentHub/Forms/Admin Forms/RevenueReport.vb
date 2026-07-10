Imports System.IO
Imports System.Diagnostics
Imports System.Windows.Forms.DataVisualization.Charting
Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class RevenueReport

    Private ReadOnly connString As String = "server=localhost;user id=root;database=entertainmenthub"

    Private Sub RevenueReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackgroundImage = AccountData.AdminCommonBackground
        Me.BackgroundImageLayout = ImageLayout.Stretch

        TableLayoutPanel2.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel2)

        TableLayoutPanel3.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel3)

        cklsbxRevenueSourceToggle.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.FontDesign(cklsbxRevenueSourceToggle, Color.FromArgb(255, 255, 255), AppFonts.CdSaver(12))

        HelperFunc.FontDesign(grpbxReportRange, Color.FromArgb(255, 255, 255), AppFonts.CdSaver(12))

        Dim ctrls1 As Control() = {Label2, Label3, Label4, Label8}
        For Each i In ctrls1
            HelperFunc.FontDesign(i, Color.FromArgb(255, 255, 255), AppFonts.VenusRising(16))
        Next

        HelperFunc.ApplyBorder(Label4, HelperFunc.BorderSides.Bottom)
        HelperFunc.ApplyBorder(Label2, HelperFunc.BorderSides.Bottom)

        HelperFunc.ApplyBorder(Label3, HelperFunc.BorderSides.Bottom)
        HelperFunc.ApplyBorder(Label8, HelperFunc.BorderSides.Top Or HelperFunc.BorderSides.Bottom)

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

        InitializeControls()
        SetYearPickerBounds()
        CheckRevenueSourceState()
        SetDefaultLabels()
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
                    If res IsNot Nothing AndAlso Not IsDBNull(res) Then Return Convert.ToDecimal(res)
                End Using
            Catch ex As Exception
            End Try
        End Using
        Return 0
    End Function

    Private Function GetCostOfGoodsSold(d1 As DateTime, d2 As DateTime) As Decimal
        Dim qry As String = "SELECT SUM(si.Quantity * si.CostPrice) FROM wallettransactions wt JOIN salesitem si ON wt.SalesID = si.SalesID WHERE wt.TransactionType = 'Payment' AND wt.TransactionDate >= @d1 AND wt.TransactionDate <= @d2"
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
            End Try
        End Using
        Return 0
    End Function

    Private Function GetPeakEarningPeriod(d1 As DateTime, d2 As DateTime) As String
        Dim qry As String = "SELECT CONCAT(DAYNAME(TransactionDate), ', ', DATE_FORMAT(TransactionDate, '%h:00 %p')) FROM wallettransactions WHERE TransactionType = 'Payment' AND TransactionDate >= @d1 AND TransactionDate <= @d2 GROUP BY DAYNAME(TransactionDate), HOUR(TransactionDate) ORDER BY SUM(Amount) DESC LIMIT 1"
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
        LoadChartData()
        UpdateFinancialSummary()
    End Sub

    Private Sub LoadChartData()
        Chart1.Series.Clear()
        Dim chartType As SeriesChartType = SeriesChartType.Column
        Select Case cmbboxFormat.SelectedItem.ToString()
            Case "Stacked Column Chart" : chartType = SeriesChartType.StackedColumn
            Case "Line Chart" : chartType = SeriesChartType.Line
            Case "Area Chart" : chartType = SeriesChartType.Area
            Case "Spline Chart" : chartType = SeriesChartType.Spline
        End Select

        Dim dateGroup As String = GetDateGrouping()
        Dim d1 As DateTime = GetStartDate()
        Dim d2 As DateTime = GetEndDate()

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

            Dim qry As String = $"SELECT {dateGroup} as period, SUM(Amount) as total, MIN(TransactionDate) as sortOrder FROM wallettransactions WHERE {filter} GROUP BY period ORDER BY sortOrder"

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
                End Try
            End Using
        Next
    End Sub

    Private Sub UpdateFinancialSummary()
        Dim d1 As DateTime = GetStartDate()
        Dim d2 As DateTime = GetEndDate()

        Dim dep As Decimal = GetTotalByTransactionType("Deposit", d1, d2)
        Dim wth As Decimal = GetTotalByTransactionType("Withdrawal", d1, d2)
        Dim bon As Decimal = GetTotalByTransactionType("Bonus", d1, d2)
        Dim adj As Decimal = GetTotalByTransactionType("Adjustment", d1, d2)
        Dim ref As Decimal = GetTotalByTransactionType("Refund", d1, d2)

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

        Dim opEx As Decimal = 0
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

        Dim titleFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.DARK_GRAY)
        Dim subFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.GRAY)

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
        Dim tableTitleFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK)
        Dim headerBgColor As New BaseColor(64, 64, 64)

        For Each series As Series In Chart1.Series
            Dim tableTitle As New Paragraph(series.Name & " Data Breakdown", tableTitleFont)
            tableTitle.SpacingAfter = 8.0F
            doc.Add(tableTitle)

            Dim table As New PdfPTable(2)
            table.WidthPercentage = 100
            table.SetWidths(New Single() {0.5F, 0.5F})

            Dim cellPeriod As New PdfPCell(New Phrase("Period", tableHeaderFont))
            cellPeriod.BackgroundColor = headerBgColor
            cellPeriod.Padding = 6.0F
            table.AddCell(cellPeriod)

            Dim cellRev As New PdfPCell(New Phrase("Revenue", tableHeaderFont))
            cellRev.BackgroundColor = headerBgColor
            cellRev.Padding = 6.0F
            cellRev.HorizontalAlignment = Element.ALIGN_RIGHT
            table.AddCell(cellRev)

            Dim rowCount As Integer = 0
            For Each p As DataPoint In series.Points
                Dim cell1 As New PdfPCell(New Phrase(p.AxisLabel.ToString(), tableCellFont))
                Dim cell2 As New PdfPCell(New Phrase(p.YValues(0).ToString("C2"), tableCellFont))

                cell1.Padding = 5.0F
                cell2.Padding = 5.0F
                cell2.HorizontalAlignment = Element.ALIGN_RIGHT
                cell1.BorderColor = BaseColor.LIGHT_GRAY
                cell2.BorderColor = BaseColor.LIGHT_GRAY

                If rowCount Mod 2 <> 0 Then
                    Dim altBg As New BaseColor(245, 245, 245)
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

        AppendFinancialSummariesToPDF(doc)
        doc.Close()
    End Sub

    Private Sub AppendFinancialSummariesToPDF(doc As Document)
        Dim d1 As DateTime = GetStartDate()
        Dim d2 As DateTime = GetEndDate()

        Dim dep As Decimal = GetTotalByTransactionType("Deposit", d1, d2)
        Dim wth As Decimal = GetTotalByTransactionType("Withdrawal", d1, d2)
        Dim bon As Decimal = GetTotalByTransactionType("Bonus", d1, d2)
        Dim adj As Decimal = GetTotalByTransactionType("Adjustment", d1, d2)
        Dim ref As Decimal = GetTotalByTransactionType("Refund", d1, d2)

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

        Dim opEx As Decimal = 0
        Dim totalSales As Decimal = grossSes + grossPro
        Dim grossProfit As Decimal = totalSales - cogs
        Dim netIncome As Decimal = grossProfit - (opEx + ref)

        Dim cellFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK)
        Dim boldFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK)
        Dim titleFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE)
        Dim headerBgColor As New BaseColor(64, 64, 64)
        Dim highlightBg As New BaseColor(230, 240, 255)

        Dim cashTable As New PdfPTable(2)
        cashTable.WidthPercentage = 100
        cashTable.SetWidths(New Single() {0.65F, 0.35F})
        cashTable.SpacingBefore = 10.0F

        Dim cashHeader As New PdfPCell(New Phrase("CASH FLOW & WALLET SUMMARY", titleFont))
        cashHeader.BackgroundColor = headerBgColor
        cashHeader.Colspan = 2
        cashHeader.Padding = 8.0F
        cashTable.AddCell(cashHeader)

        AddStyledRow(cashTable, "1. Total Cash In (Deposits)", dep.ToString("C2"), cellFont)
        AddStyledRow(cashTable, "2. Total Cash Out (Withdrawals)", wth.ToString("C2"), cellFont)
        AddStyledRow(cashTable, "3. Promotional Bonuses Issued", bon.ToString("C2"), cellFont)
        AddStyledRow(cashTable, "4. Manual Wallet Adjustments", adj.ToString("C2"), cellFont)
        doc.Add(cashTable)

        Dim revTable As New PdfPTable(2)
        revTable.WidthPercentage = 100
        revTable.SetWidths(New Single() {0.65F, 0.35F})
        revTable.SpacingBefore = 25.0F

        Dim revHeader As New PdfPCell(New Phrase("REVENUE & INCOME SUMMARY", titleFont))
        revHeader.BackgroundColor = headerBgColor
        revHeader.Colspan = 2
        revHeader.Padding = 8.0F
        revTable.AddCell(revHeader)

        AddStyledRow(revTable, "1. Total Transactions", transCount.ToString(), cellFont)
        AddStyledRow(revTable, "2. Peak Earning Period", peak, cellFont)
        AddStyledRow(revTable, "3. Total Session Sales", grossSes.ToString("C2"), cellFont)
        AddStyledRow(revTable, "4. Total Product Sales", grossPro.ToString("C2"), cellFont)
        AddStyledRow(revTable, "5. Less: Cost of Goods Sold", cogs.ToString("C2"), cellFont)
        AddStyledRow(revTable, "6. GROSS PROFIT", grossProfit.ToString("C2"), boldFont)
        AddStyledRow(revTable, "7. Less: Operating Expenses", opEx.ToString("C2"), cellFont)
        AddStyledRow(revTable, "8. Less: Refunds Processed", ref.ToString("C2"), cellFont)
        AddStyledRow(revTable, "9. NET EARNED INCOME", netIncome.ToString("C2"), boldFont, highlightBg)
        doc.Add(revTable)
    End Sub

    Private Sub AddStyledRow(table As PdfPTable, label As String, val As String, font As iTextSharp.text.Font, Optional bgColor As BaseColor = Nothing)
        Dim cellLabel As New PdfPCell(New Phrase(label, font))
        cellLabel.Padding = 6.0F
        cellLabel.BorderColor = BaseColor.LIGHT_GRAY
        If bgColor IsNot Nothing Then cellLabel.BackgroundColor = bgColor
        table.AddCell(cellLabel)

        Dim cellVal As New PdfPCell(New Phrase(val, font))
        cellVal.Padding = 6.0F
        cellVal.HorizontalAlignment = Element.ALIGN_RIGHT
        cellVal.BorderColor = BaseColor.LIGHT_GRAY
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
        Dim frm As New AdminDashboard()
        frm.Show()
        Me.Close()
    End Sub
End Class