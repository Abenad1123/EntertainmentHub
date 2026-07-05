Imports MySql.Data.MySqlClient

Public Class MainMenu

    Private currentTrackedUser As String = ""

    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LiveDurationTimer.Interval = 2000
            LiveDurationTimer.Start()
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("Error starting timer: " & ex.Message)
        End Try
    End Sub

#Region "Real-Time Timer"
    Private Sub LiveDurationTimer_Tick(sender As Object, e As EventArgs) Handles LiveDurationTimer.Tick
        Try

            If DataGridViewActivity.Rows.Count > 0 AndAlso DataGridViewActivity.Columns.Contains("LoginTime") AndAlso DataGridViewActivity.Columns.Contains("Duration") Then
                For Each row As DataGridViewRow In DataGridViewActivity.Rows
                    If row.Cells("LoginTime").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("LoginTime").Value) Then
                        Dim loginTime As DateTime
                        If DateTime.TryParse(row.Cells("LoginTime").Value.ToString(), loginTime) Then
                            Dim duration As TimeSpan = DateTime.Now - loginTime
                            row.Cells("Duration").Value = String.Format("{0:00}:{1:00}:{2:00}", Math.Floor(duration.TotalHours), duration.Minutes, duration.Seconds)
                        End If
                    End If
                Next
            End If


        Catch ex As Exception
            Diagnostics.Debug.WriteLine("Timer Error: " & ex.Message)
        End Try
    End Sub
#End Region
End Class