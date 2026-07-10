Imports System.Data
Imports System.Drawing
Imports System.Reflection.Emit
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class AnalyticsMenu

    Private Sub AnalyticsMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackgroundImage = AccountData.AdminCommonBackground
        Me.BackgroundImageLayout = ImageLayout.Stretch

        TableLayoutPanel2.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel2)

        TableLayoutPanel8.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel8, HelperFunc.BorderSides.Left Or HelperFunc.BorderSides.Right Or HelperFunc.BorderSides.Bottom)

        FlowLayoutPanel1.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(FlowLayoutPanel1, HelperFunc.BorderSides.Left Or HelperFunc.BorderSides.Right Or HelperFunc.BorderSides.Bottom)

        TableLayoutPanel3.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(TableLayoutPanel3, HelperFunc.BorderSides.Left Or HelperFunc.BorderSides.Right Or HelperFunc.BorderSides.Bottom)

        Label16.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(Label16)

        Label2.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(Label2)

        Label3.BackColor = Color.FromArgb(37, 36, 39)
        HelperFunc.ApplyBorder(Label3)

        Dim ctrls As Control() = {Label16, Label2, Label3, Label4, Label8, Label9, Label10, Label11}
        For Each i In ctrls
            HelperFunc.FontDesign(i, Color.FromArgb(255, 255, 255), AppFonts.Coolvetica(18))
        Next

        Dim ctrls1 As Control() = {Label5, Label6, Label7, Label12, Label13, Label14, Label15}
        For Each i In ctrls1
            HelperFunc.FontDesign(i, Color.FromArgb(255, 255, 255), AppFonts.CdSaver(13))
        Next

        HelperFunc.ApplyButtonTheme(Button1)

        StyleDataGridViews()
        LoadTypeFilter()
        SetDatePickerBounds()
        InitializeUI()
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

    Private Sub StyleCharts()
        Dim charts = {Chart1, Chart2}
        For Each cht In charts
            If cht.ChartAreas.Count > 0 Then
                Dim ca = cht.ChartAreas(0)
                ca.BackColor = Color.White
                ca.AxisX.MajorGrid.Enabled = False
                ca.AxisY.MajorGrid.LineColor = Color.FromArgb(235, 235, 235)
                ca.AxisX.LabelStyle.Font = New Font("Segoe UI", 8)
                ca.AxisY.LabelStyle.Font = New Font("Segoe UI", 8)
                ca.AxisX.LineColor = Color.FromArgb(200, 200, 200)
                ca.AxisY.LineColor = Color.FromArgb(200, 200, 200)
            End If
        Next
    End Sub

    Private Sub FormatColumns()
        If DataGridView1.Columns.Count > 0 Then
            DataGridView1.Columns("Terminal").FillWeight = 30
            DataGridView1.Columns("Type").FillWeight = 25

            DataGridView1.Columns("Total Sessions").FillWeight = 15
            DataGridView1.Columns("Total Sessions").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns("Total Sessions").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView1.Columns("Total Hours").FillWeight = 15
            DataGridView1.Columns("Total Hours").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns("Total Hours").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView1.Columns("Utilization %").FillWeight = 15
            DataGridView1.Columns("Utilization %").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns("Utilization %").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If

        If DataGridView2.Columns.Count > 0 Then
            DataGridView2.Columns("Day").FillWeight = 20
            DataGridView2.Columns("Time Block").FillWeight = 50

            DataGridView2.Columns("Total Sessions").FillWeight = 30
            DataGridView2.Columns("Total Sessions").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView2.Columns("Total Sessions").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If

        If DataGridView3.Columns.Count > 0 Then
            DataGridView3.Columns("Tier").FillWeight = 40

            DataGridView3.Columns("Total Sessions").FillWeight = 30
            DataGridView3.Columns("Total Sessions").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView3.Columns("Total Sessions").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView3.Columns("Total Hours").FillWeight = 30
            DataGridView3.Columns("Total Hours").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView3.Columns("Total Hours").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If
    End Sub

    Private Sub InitializeUI()
        Label12.Text = "0"
        Label13.Text = "0.00 hrs"
        Label14.Text = "0h 0m"
        Label15.Text = "N/A"

        Chart1.Series.Clear()
        Chart2.Series.Clear()
        StyleCharts()

        AddHandler DateTimePicker1.ValueChanged, AddressOf EnforceDateOrder
        AddHandler DateTimePicker2.ValueChanged, AddressOf EnforceDateOrder
        AddHandler Button1.Click, AddressOf LoadAnalytics
    End Sub

    Private Sub LoadTypeFilter()
        Dim dt As New DataTable()
        dt.Columns.Add("TypeID", GetType(Integer))
        dt.Columns.Add("TypeName", GetType(String))

        dt.Rows.Add(0, "All")

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                conn.Open()
                Using cmd As New MySqlCommand("SELECT EntertainmentTypeID, EntertainmentTypeName FROM entertainmenttype ORDER BY EntertainmentTypeName", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            dt.Rows.Add(reader.GetInt32(0), reader.GetString(1))
                        End While
                    End Using
                End Using
            Catch ex As Exception
            End Try
        End Using

        ComboBox1.DataSource = dt
        ComboBox1.DisplayMember = "TypeName"
        ComboBox1.ValueMember = "TypeID"
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub SetDatePickerBounds()
        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                conn.Open()
                Using cmd As New MySqlCommand("SELECT MIN(LoginTime), MAX(LoginTime) FROM entertainmentsession", conn)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() AndAlso Not IsDBNull(reader(0)) Then
                            Dim minD As DateTime = Convert.ToDateTime(reader(0)).Date
                            Dim maxD As DateTime = Convert.ToDateTime(reader(1)).Date

                            DateTimePicker1.MinDate = minD
                            DateTimePicker1.MaxDate = maxD
                            DateTimePicker2.MinDate = minD
                            DateTimePicker2.MaxDate = maxD

                            DateTimePicker1.Value = minD
                            DateTimePicker2.Value = maxD
                        Else
                            DateTimePicker1.MinDate = DateTime.Today
                            DateTimePicker1.MaxDate = DateTime.Today
                            DateTimePicker2.MinDate = DateTime.Today
                            DateTimePicker2.MaxDate = DateTime.Today
                        End If
                    End Using
                End Using
            Catch ex As Exception
            End Try
        End Using
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

    Private Sub LoadAnalytics(sender As Object, e As EventArgs)
        If ComboBox1.SelectedValue Is Nothing Then Return

        Dim d1 As DateTime = DateTimePicker1.Value.Date
        Dim d2 As DateTime = DateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1)
        Dim typeId As Integer = Convert.ToInt32(ComboBox1.SelectedValue)
        Dim totalDays As Double = (d2 - d1).TotalDays

        LoadKPIs(d1, d2, typeId)
        LoadCharts(d1, d2, typeId)
        LoadGrids(d1, d2, typeId, totalDays)
    End Sub

    Private Sub LoadKPIs(d1 As DateTime, d2 As DateTime, typeId As Integer)
        Dim qryStats As String = "SELECT COUNT(es.EntertainmentSessionID), COALESCE(SUM(TIMESTAMPDIFF(MINUTE, es.LoginTime, COALESCE(es.LogoutTime, NOW()))), 0) FROM entertainmentsession es JOIN entertainment e ON es.EntertainmentID = e.EntertainmentID JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID WHERE es.LoginTime >= @d1 AND es.LoginTime <= @d2 AND (@typeId = 0 OR et.EntertainmentTypeID = @typeId)"
        Dim qryTop As String = "SELECT e.EntertainmentName FROM entertainmentsession es JOIN entertainment e ON es.EntertainmentID = e.EntertainmentID JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID WHERE es.LoginTime >= @d1 AND es.LoginTime <= @d2 AND (@typeId = 0 OR et.EntertainmentTypeID = @typeId) GROUP BY e.EntertainmentID ORDER BY COUNT(es.EntertainmentSessionID) DESC LIMIT 1"

        Dim totalSessions As Integer = 0
        Dim totalMinutes As Double = 0

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                conn.Open()
                Using cmd As New MySqlCommand(qryStats, conn)
                    cmd.Parameters.AddWithValue("@d1", d1)
                    cmd.Parameters.AddWithValue("@d2", d2)
                    cmd.Parameters.AddWithValue("@typeId", typeId)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            totalSessions = Convert.ToInt32(reader(0))
                            totalMinutes = Convert.ToDouble(reader(1))
                        End If
                    End Using
                End Using

                Using cmdTop As New MySqlCommand(qryTop, conn)
                    cmdTop.Parameters.AddWithValue("@d1", d1)
                    cmdTop.Parameters.AddWithValue("@d2", d2)
                    cmdTop.Parameters.AddWithValue("@typeId", typeId)
                    Dim res = cmdTop.ExecuteScalar()
                    Label15.Text = If(res IsNot Nothing, res.ToString(), "N/A")
                End Using
            Catch ex As Exception
            End Try
        End Using

        Dim totalHours As Double = totalMinutes / 60.0
        Label12.Text = totalSessions.ToString()
        Label13.Text = totalHours.ToString("F2") & " hrs"

        If totalSessions > 0 Then
            Dim avgMinutes As Double = totalMinutes / totalSessions
            Dim hrs As Integer = Math.Floor(avgMinutes / 60)
            Dim mins As Integer = Math.Round(avgMinutes Mod 60)
            Label14.Text = $"{hrs}h {mins}m"
        Else
            Label14.Text = "0h 0m"
        End If
    End Sub

    Private Sub LoadCharts(d1 As DateTime, d2 As DateTime, typeId As Integer)
        Chart1.Series.Clear()
        Chart2.Series.Clear()

        Dim s1 As New Series("Total Hours")
        s1.ChartType = SeriesChartType.Column
        s1.Color = Color.FromArgb(41, 128, 185)
        s1.IsValueShownAsLabel = True
        s1.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        s1.LabelForeColor = Color.FromArgb(64, 64, 64)
        Chart1.Series.Add(s1)

        Dim s2 As New Series("Logins")
        s2.ChartType = SeriesChartType.SplineArea
        s2.Color = Color.FromArgb(46, 204, 113)
        s2.BackSecondaryColor = Color.FromArgb(200, 235, 210)
        s2.BackGradientStyle = GradientStyle.TopBottom
        s2.BorderWidth = 2
        Chart2.Series.Add(s2)

        Dim qryTerminal As String = "SELECT e.EntertainmentName, COALESCE(SUM(TIMESTAMPDIFF(MINUTE, es.LoginTime, COALESCE(es.LogoutTime, NOW()))) / 60.0, 0) as Hrs FROM entertainmentsession es JOIN entertainment e ON es.EntertainmentID = e.EntertainmentID JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID WHERE es.LoginTime >= @d1 AND es.LoginTime <= @d2 AND (@typeId = 0 OR et.EntertainmentTypeID = @typeId) GROUP BY e.EntertainmentID ORDER BY Hrs DESC LIMIT 10"
        Dim qryHours As String = "SELECT HOUR(es.LoginTime) as Hr, COUNT(es.EntertainmentSessionID) FROM entertainmentsession es JOIN entertainment e ON es.EntertainmentID = e.EntertainmentID JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID WHERE es.LoginTime >= @d1 AND es.LoginTime <= @d2 AND (@typeId = 0 OR et.EntertainmentTypeID = @typeId) GROUP BY Hr ORDER BY Hr"

        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                conn.Open()
                Using cmd As New MySqlCommand(qryTerminal, conn)
                    cmd.Parameters.AddWithValue("@d1", d1)
                    cmd.Parameters.AddWithValue("@d2", d2)
                    cmd.Parameters.AddWithValue("@typeId", typeId)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            s1.Points.AddXY(reader(0).ToString(), Convert.ToDouble(reader(1)))
                        End While
                    End Using
                End Using

                Using cmd As New MySqlCommand(qryHours, conn)
                    cmd.Parameters.AddWithValue("@d1", d1)
                    cmd.Parameters.AddWithValue("@d2", d2)
                    cmd.Parameters.AddWithValue("@typeId", typeId)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim hrStr As String = New DateTime(2000, 1, 1, Convert.ToInt32(reader(0)), 0, 0).ToString("tt")
                            s2.Points.AddXY(Convert.ToInt32(reader(0)).ToString() & hrStr, Convert.ToDouble(reader(1)))
                        End While
                    End Using
                End Using
            Catch ex As Exception
            End Try
        End Using
    End Sub

    Private Sub LoadGrids(d1 As DateTime, d2 As DateTime, typeId As Integer, totalDays As Double)
        If totalDays < 1 Then totalDays = 1

        Dim openingHour As Double = 8.0
        Dim closingHour As Double = 24.0

        Dim dailyOperatingHours As Double = closingHour - openingHour
        If dailyOperatingHours <= 0 Or dailyOperatingHours > 24 Then
            dailyOperatingHours = 24.0
        End If

        Dim maxPossibleHours As Double = totalDays * dailyOperatingHours

        Dim qryGrid1 As String = $"SELECT e.EntertainmentName AS 'Terminal', etype.EntertainmentTypeName AS 'Type', COUNT(es.EntertainmentSessionID) AS 'Total Sessions', ROUND(COALESCE(SUM(TIMESTAMPDIFF(MINUTE, es.LoginTime, COALESCE(es.LogoutTime, NOW()))), 0) / 60.0, 2) AS 'Total Hours', CONCAT(ROUND((COALESCE(SUM(TIMESTAMPDIFF(MINUTE, es.LoginTime, COALESCE(es.LogoutTime, NOW()))), 0) / 60.0) / {maxPossibleHours} * 100, 2), '%') AS 'Utilization %' FROM entertainment e JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID JOIN entertainmenttype etype ON et.EntertainmentTypeID = etype.EntertainmentTypeID LEFT JOIN entertainmentsession es ON e.EntertainmentID = es.EntertainmentID AND es.LoginTime >= @d1 AND es.LoginTime <= @d2 WHERE (@typeId = 0 OR etype.EntertainmentTypeID = @typeId) GROUP BY e.EntertainmentID ORDER BY 'Total Hours' DESC"
        Dim qryGrid2 As String = "SELECT DAYNAME(es.LoginTime) AS 'Day', CONCAT(DATE_FORMAT(es.LoginTime, '%h:00 %p'), ' - ', DATE_FORMAT(DATE_ADD(es.LoginTime, INTERVAL 1 HOUR), '%h:00 %p')) AS 'Time Block', COUNT(es.EntertainmentSessionID) AS 'Total Sessions' FROM entertainmentsession es JOIN entertainment e ON es.EntertainmentID = e.EntertainmentID JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID WHERE es.LoginTime >= @d1 AND es.LoginTime <= @d2 AND (@typeId = 0 OR et.EntertainmentTypeID = @typeId) GROUP BY DAYOFWEEK(es.LoginTime), HOUR(es.LoginTime) ORDER BY COUNT(es.EntertainmentSessionID) DESC"
        Dim qryGrid3 As String = "SELECT et.EntertainmentTierName AS 'Tier', COUNT(es.EntertainmentSessionID) AS 'Total Sessions', ROUND(COALESCE(SUM(TIMESTAMPDIFF(MINUTE, es.LoginTime, COALESCE(es.LogoutTime, NOW()))), 0) / 60.0, 2) AS 'Total Hours' FROM entertainmentsession es JOIN entertainment e ON e.EntertainmentID = e.EntertainmentID JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID WHERE es.LoginTime >= @d1 AND es.LoginTime <= @d2 AND (@typeId = 0 OR et.EntertainmentTypeID = @typeId) GROUP BY et.EntertainmentTierID ORDER BY COUNT(es.EntertainmentSessionID) DESC"

        BindGrid(DataGridView1, qryGrid1, d1, d2, typeId)
        BindGrid(DataGridView2, qryGrid2, d1, d2, typeId)
        BindGrid(DataGridView3, qryGrid3, d1, d2, typeId)

        FormatColumns()
    End Sub

    Private Sub BindGrid(dgv As DataGridView, qry As String, d1 As DateTime, d2 As DateTime, typeId As Integer)
        Using conn As MySqlConnection = DBConnection.GetConnection()
            Try
                conn.Open()
                Using cmd As New MySqlCommand(qry, conn)
                    cmd.Parameters.AddWithValue("@d1", d1)
                    cmd.Parameters.AddWithValue("@d2", d2)
                    cmd.Parameters.AddWithValue("@typeId", typeId)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    dgv.DataSource = dt
                End Using
            Catch ex As Exception
            End Try
        End Using
    End Sub

    Private Sub btnGoBack_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click
        Dim frm As New AdminDashboard()
        frm.Show()
        Me.Close()
    End Sub
End Class