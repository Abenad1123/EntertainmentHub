Imports MySql.Data.MySqlClient

Public Class DBConnection
    Public Shared Function GetConnection() As MySqlConnection
        Dim connStr As String = "server=localhost;port=3306;database=entertainmenthub;uid=root;pwd=;"
        Return New MySqlConnection(connStr)
    End Function
End Class