Imports MySql.Data.MySqlClient

Module DatabaseInitializerModule
    Public Sub SeedDatabase()

        'Initialize the database with default values for MembershipLevel, Roles, EntertainmentTypes, and EntertainmentTiers
        Try
            Using conn As MySqlConnection = DBConnection.GetConnection()

                conn.Open()

                SeedMembershipLevels(conn)
                SeedRoles(conn)
                SeedEntertainmentTypes(conn)
                SeedEntertainmentTiers(conn)

            End Using
        Catch ex As Exception
            MessageBox.Show("Error seeding database: " & ex.Message)
        End Try
    End Sub
#Region "Membership Levels"
    Private Sub SeedMembershipLevels(conn As MySqlConnection)

        SeedMembershipLevel(conn, "Basic", 100D)
        SeedMembershipLevel(conn, "Premium", 250D)
        SeedMembershipLevel(conn, "VIP", 500D)

    End Sub
    Private Sub SeedMembershipLevel(conn As MySqlConnection, MembershipLevelName As String, Price As Decimal)


        Dim checkSql As String =
        "SELECT COUNT(*) 
         FROM MembershipLevel
         WHERE MembershipLevelName = @MembershipLevelName"

        Using cmd As New MySqlCommand(checkSql, conn)

            cmd.Parameters.AddWithValue("@MembershipLevelName", MembershipLevelName)

            Dim count As Integer =
            Convert.ToInt32(cmd.ExecuteScalar())

            If count = 0 Then

                Dim insertSql As String =
                "INSERT INTO MembershipLevel
                 (MembershipLevelName, Price)
                 VALUES
                 (@Name, @Price)"

                Using insertCmd As New MySqlCommand(insertSql, conn)

                    insertCmd.Parameters.AddWithValue("@Name", MembershipLevelName)
                    insertCmd.Parameters.AddWithValue("@Price", Price)

                    insertCmd.ExecuteNonQuery()

                End Using

            End If

        End Using




    End Sub
#End Region
#Region "Roles"

    Private Sub SeedRoles(conn As MySqlConnection)

        SeedRole(conn, "Admin")
        SeedRole(conn, "Cashier")
        SeedRole(conn, "Staff")

    End Sub
    Private Sub SeedRole(conn As MySqlConnection, RoleName As String)

        Dim checkSql As String =
           "SELECT COUNT(*) 
         FROM Roles
         WHERE RoleName = @RoleName"

        Using cmd As New MySqlCommand(checkSql, conn)
            cmd.Parameters.AddWithValue("@RoleName", RoleName)
            Dim count As Integer =
            Convert.ToInt32(cmd.ExecuteScalar())
            If count = 0 Then
                Dim insertSql As String =
                "INSERT INTO Roles
                 (RoleName)
                 VALUES
                 (@RoleName)"
                Using insertCmd As New MySqlCommand(insertSql, conn)
                    insertCmd.Parameters.AddWithValue("@RoleName", RoleName)
                    insertCmd.ExecuteNonQuery()
                End Using
            End If
        End Using

        ' Check and insert Admin, Cashier, Staff

    End Sub

#End Region

#Region "Entertainment Types"

    Private Sub SeedEntertainmentTypes(conn As MySqlConnection)

        SeedEntertainmentType(conn, "PC")
        SeedEntertainmentType(conn, "Console")
        SeedEntertainmentType(conn, "VR")
        SeedEntertainmentType(conn, "Billiards")
    End Sub

    Private Sub SeedEntertainmentType(conn As MySqlConnection, EntertainmentTypeName As String)
        Dim checkSql As String =
           "SELECT COUNT(*) 
         FROM EntertainmentType
         WHERE EntertainmentTypeName = @EntertainmentTypeName"

        Using cmd As New MySqlCommand(checkSql, conn)
            cmd.Parameters.AddWithValue("@EntertainmentTypeName", EntertainmentTypeName)
            Dim count As Integer =
            Convert.ToInt32(cmd.ExecuteScalar())
            If count = 0 Then
                Dim insertSql As String =
                "INSERT INTO EntertainmentType
                 (EntertainmentTypeName)
                 VALUES
                 (@EntertainmentTypeName)"
                Using insertCmd As New MySqlCommand(insertSql, conn)
                    insertCmd.Parameters.AddWithValue("@EntertainmentTypeName", EntertainmentTypeName)
                    insertCmd.ExecuteNonQuery()
                End Using
            End If
        End Using


    End Sub
#End Region

#Region "Entertainment Tiers"

    Private Sub SeedEntertainmentTiers(conn As MySqlConnection)


        ' Seed Entertainment Tiers for PC and Console 1- pc, 2- console, 3- VR, 4- Billiards
        SeedEntertainmentTier(conn, "Standard PC", 50D, 1)
        SeedEntertainmentTier(conn, "High-End PC", 80D, 1)
        SeedEntertainmentTier(conn, "PS5", 70D, 2)
        SeedEntertainmentTier(conn, "Oculus Quest 2", 60D, 3)
        SeedEntertainmentTier(conn, "8-Ball Pool", 40D, 4)

    End Sub
    Private Sub SeedEntertainmentTier(conn As MySqlConnection, EntertainmentTierName As String, HourlyRate As Decimal, EntertainmentTypeID As Integer)
        Dim checkSql As String =
           "SELECT COUNT(*) 
         FROM EntertainmentTier
         WHERE EntertainmentTierName = @EntertainmentTierName AND EntertainmentTypeID = @EntertainmentTypeID"

        Using cmd As New MySqlCommand(checkSql, conn)
            cmd.Parameters.AddWithValue("@EntertainmentTierName", EntertainmentTierName)
            cmd.Parameters.AddWithValue("@EntertainmentTypeID", EntertainmentTypeID)
            Dim count As Integer =
            Convert.ToInt32(cmd.ExecuteScalar())
            If count = 0 Then
                Dim insertSql As String =
                "INSERT INTO EntertainmentTier
                 (EntertainmentTierName, HourlyRate, EntertainmentTypeID)
                 VALUES
                 (@EntertainmentTierName, @HourlyRate, @EntertainmentTypeID)"
                Using insertCmd As New MySqlCommand(insertSql, conn)
                    insertCmd.Parameters.AddWithValue("@EntertainmentTierName", EntertainmentTierName)
                    insertCmd.Parameters.AddWithValue("@HourlyRate", HourlyRate)
                    insertCmd.Parameters.AddWithValue("@EntertainmentTypeID", EntertainmentTypeID)
                    insertCmd.ExecuteNonQuery()
                End Using
            End If
        End Using

    End Sub
#End Region
End Module
