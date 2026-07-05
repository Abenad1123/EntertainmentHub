Imports System.Data
Imports System.Reflection.Emit
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class AnalyticsMenu

    Private ReadOnly connString As String = "server=localhost;user id=root;database=entertainmenthub"

    Private Sub UsageAnalytics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTypeFilter()
        SetDatePickerBounds()
        InitializeUI()
    End Sub

    Private Sub InitializeUI()
        Label12.Text = "0"
        Label13.Text = "0.00 hrs"
        Label14.Text = "0h 0m"
        Label15.Text = "N/A"

        Chart1.Series.Clear()
        Chart2.Series.Clear()

        AddHandler DateTimePicker1.ValueChanged, AddressOf EnforceDateOrder
        AddHandler DateTimePicker2.ValueChanged, AddressOf EnforceDateOrder
        AddHandler Button1.Click, AddressOf LoadAnalytics
    End Sub

    Private Sub LoadTypeFilter()
        Dim dt As New DataTable()
        dt.Columns.Add("TypeID", GetType(Integer))
        dt.Columns.Add("TypeName", GetType(String))

        dt.Rows.Add(0, "All")

        Using conn As New MySqlConnection(connString)
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
        Using conn As New MySqlConnection(connString)
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

        Using conn As New MySqlConnection(connString)
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
        Chart1.Series.Add(s1)

        Dim s2 As New Series("Logins")
        s2.ChartType = SeriesChartType.SplineArea
        Chart2.Series.Add(s2)

        Dim qryTerminal As String = "SELECT e.EntertainmentName, COALESCE(SUM(TIMESTAMPDIFF(MINUTE, es.LoginTime, COALESCE(es.LogoutTime, NOW()))) / 60.0, 0) as Hrs FROM entertainmentsession es JOIN entertainment e ON es.EntertainmentID = e.EntertainmentID JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID WHERE es.LoginTime >= @d1 AND es.LoginTime <= @d2 AND (@typeId = 0 OR et.EntertainmentTypeID = @typeId) GROUP BY e.EntertainmentID ORDER BY Hrs DESC LIMIT 10"
        Dim qryHours As String = "SELECT HOUR(es.LoginTime) as Hr, COUNT(es.EntertainmentSessionID) FROM entertainmentsession es JOIN entertainment e ON es.EntertainmentID = e.EntertainmentID JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID WHERE es.LoginTime >= @d1 AND es.LoginTime <= @d2 AND (@typeId = 0 OR et.EntertainmentTypeID = @typeId) GROUP BY Hr ORDER BY Hr"

        Using conn As New MySqlConnection(connString)
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

        Dim maxPossibleHours As Double = totalDays * 24.0

        Dim qryGrid1 As String = $"SELECT e.EntertainmentName AS 'Terminal', etype.EntertainmentTypeName AS 'Type', COUNT(es.EntertainmentSessionID) AS 'Total Sessions', ROUND(COALESCE(SUM(TIMESTAMPDIFF(MINUTE, es.LoginTime, COALESCE(es.LogoutTime, NOW()))), 0) / 60.0, 2) AS 'Total Hours', CONCAT(ROUND((COALESCE(SUM(TIMESTAMPDIFF(MINUTE, es.LoginTime, COALESCE(es.LogoutTime, NOW()))), 0) / 60.0) / {maxPossibleHours} * 100, 2), '%') AS 'Utilization %' FROM entertainment e JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID JOIN entertainmenttype etype ON et.EntertainmentTypeID = etype.EntertainmentTypeID LEFT JOIN entertainmentsession es ON e.EntertainmentID = es.EntertainmentID AND es.LoginTime >= @d1 AND es.LoginTime <= @d2 WHERE (@typeId = 0 OR etype.EntertainmentTypeID = @typeId) GROUP BY e.EntertainmentID ORDER BY 'Total Hours' DESC"
        Dim qryGrid2 As String = "SELECT DAYNAME(es.LoginTime) AS 'Day', CONCAT(DATE_FORMAT(es.LoginTime, '%h:00 %p'), ' - ', DATE_FORMAT(DATE_ADD(es.LoginTime, INTERVAL 1 HOUR), '%h:00 %p')) AS 'Time Block', COUNT(es.EntertainmentSessionID) AS 'Total Sessions' FROM entertainmentsession es JOIN entertainment e ON es.EntertainmentID = e.EntertainmentID JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID WHERE es.LoginTime >= @d1 AND es.LoginTime <= @d2 AND (@typeId = 0 OR et.EntertainmentTypeID = @typeId) GROUP BY DAYOFWEEK(es.LoginTime), HOUR(es.LoginTime) ORDER BY COUNT(es.EntertainmentSessionID) DESC"
        Dim qryGrid3 As String = "SELECT et.EntertainmentTierName AS 'Tier', COUNT(es.EntertainmentSessionID) AS 'Total Sessions', ROUND(COALESCE(SUM(TIMESTAMPDIFF(MINUTE, es.LoginTime, COALESCE(es.LogoutTime, NOW()))), 0) / 60.0, 2) AS 'Total Hours' FROM entertainmentsession es JOIN entertainment e ON es.EntertainmentID = e.EntertainmentID JOIN entertainmenttier et ON e.EntertainmentTierID = et.EntertainmentTierID WHERE es.LoginTime >= @d1 AND es.LoginTime <= @d2 AND (@typeId = 0 OR et.EntertainmentTypeID = @typeId) GROUP BY et.EntertainmentTierID ORDER BY COUNT(es.EntertainmentSessionID) DESC"

        BindGrid(DataGridView1, qryGrid1, d1, d2, typeId)
        BindGrid(DataGridView2, qryGrid2, d1, d2, typeId)
        BindGrid(DataGridView3, qryGrid3, d1, d2, typeId)
    End Sub

    Private Sub BindGrid(dgv As DataGridView, qry As String, d1 As DateTime, d2 As DateTime, typeId As Integer)
        Using conn As New MySqlConnection(connString)
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
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End Using
            Catch ex As Exception
            End Try
        End Using
    End Sub

End Class