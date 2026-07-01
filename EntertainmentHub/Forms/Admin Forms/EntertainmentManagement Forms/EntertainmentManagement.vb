Imports MySql.Data.MySqlClient

Public Class EntertainmentManagement

#Region "Initialize Entertainment Configuration"
    Private Sub LoadInUse()
        Using conn = DBConnection.GetConnection()
            Try
                Dim query As String = "SELECT ent.EntertainmentName, entt.HourlyRate, ent.Status, al.UserName, es.LoginTime, es.LogoutTime " &
                                       "FROM entertainment ent " &
                                       "INNER JOIN EntertainmentTier entt ON ent.EntertainmentTierID = entt.EntertainmentTierID " &
                                       "LEFT JOIN EntertainmentSession es ON ent.EntertainmentID = es.EntertainmentID AND es.Status = 'Active' " &
                                       "LEFT JOIN AccountLogin al ON es.AccountID = al.AccountID " &
                                       "WHERE ent.Status = 'InUse'"

                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridEntertainment.DataSource = dt
                End Using

                If DataGridEntertainment.Columns.Count > 0 Then
                    DataGridEntertainment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End If
                If DataGridEntertainment.Columns.Contains("HourlyRate") Then
                    DataGridEntertainment.Columns("HourlyRate").DefaultCellStyle.Format = "C2"
                End If

            Catch ex As MySqlException
                MessageBox.Show("Error occurred while loading in-use entertainment: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadAvailable()
        Using conn = DBConnection.GetConnection()
            Try
                Dim query As String = "SELECT ent.EntertainmentName, entt.HourlyRate, ent.Status " &
                                       "FROM entertainment ent " &
                                       "INNER JOIN EntertainmentTier entt ON ent.EntertainmentTierID = entt.EntertainmentTierID " &
                                       "WHERE Status = 'Available'"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridEntertainment.DataSource = dt
                End Using

                If DataGridEntertainment.Columns.Count > 0 Then
                    DataGridEntertainment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End If
                If DataGridEntertainment.Columns.Contains("HourlyRate") Then
                    DataGridEntertainment.Columns("HourlyRate").DefaultCellStyle.Format = "C2"
                End If

            Catch ex As MySqlException
                MessageBox.Show("Error occurred while loading available entertainment: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub loadInMaintenance()
        Using conn = DBConnection.GetConnection()
            Try
                Dim query As String = "SELECT ent.EntertainmentName, entt.HourlyRate, ent.Status " &
                                       "FROM entertainment ent " &
                                       "INNER JOIN EntertainmentTier entt ON ent.EntertainmentTierID = entt.EntertainmentTierID " &
                                       "WHERE Status = 'Maintenance'"
                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    DataGridEntertainment.DataSource = dt
                End Using

                If DataGridEntertainment.Columns.Count > 0 Then
                    DataGridEntertainment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End If
                If DataGridEntertainment.Columns.Contains("HourlyRate") Then
                    DataGridEntertainment.Columns("HourlyRate").DefaultCellStyle.Format = "C2"
                End If

            Catch ex As MySqlException
                MessageBox.Show("Error occurred while loading in-maintenance entertainment: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedItem IsNot Nothing Then
            Dim selectedStatus As String = ComboBox3.SelectedItem.ToString()
            Select Case selectedStatus
                Case "In Use"
                    LoadInUse()
                Case "Available"
                    LoadAvailable()
                Case "In Maintenance"
                    loadInMaintenance()
                Case Else
                    MessageBox.Show("Invalid status selected.")
            End Select
        End If
    End Sub

#End Region
End Class