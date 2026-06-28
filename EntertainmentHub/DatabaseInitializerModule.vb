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
                seedproducts(conn)
                SeedEntertainmentItems(conn)

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
        ' Updated to match your latest database snapshot
        SeedEntertainmentType(conn, "PC Gaming")
        SeedEntertainmentType(conn, "Virtual Reality (VR)")
        SeedEntertainmentType(conn, "Billiards")
        SeedEntertainmentType(conn, "Ping Pong")
        SeedEntertainmentType(conn, "Gaming Consoles")
    End Sub

    Private Sub SeedEntertainmentType(conn As MySqlConnection, EntertainmentTypeName As String)
        Dim checkSql As String =
        "SELECT COUNT(*) 
         FROM EntertainmentType
         WHERE EntertainmentTypeName = @EntertainmentTypeName"

        Using cmd As New MySqlCommand(checkSql, conn)
            cmd.Parameters.AddWithValue("@EntertainmentTypeName", EntertainmentTypeName)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
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
        ' Updated explicitly to match your exact data snapshot rates and type associations
        ' 1: PC Gaming, 2: VR, 3: Billiards, 4: Ping Pong, 5: Gaming Consoles

        ' PC Gaming Tiers
        SeedEntertainmentTier(conn, "PC Standard Tier", 60D, 1)
        SeedEntertainmentTier(conn, "PC VIP (RTX 4090) Tier", 100D, 1)

        ' VR Tiers
        SeedEntertainmentTier(conn, "VR Standard Booth", 150D, 2)
        SeedEntertainmentTier(conn, "VR Premium Omni-Directional", 250D, 2)

        ' Billiards Tiers
        SeedEntertainmentTier(conn, "Billiards Standard Table", 120D, 3)
        SeedEntertainmentTier(conn, "Billiards Tournament Table", 180D, 3)

        ' Ping Pong Tiers
        SeedEntertainmentTier(conn, "Ping Pong Standard Table", 80D, 4)

        ' Gaming Consoles Tiers
        SeedEntertainmentTier(conn, "Console Lounge (PS5/Xbox)", 90D, 5)
        SeedEntertainmentTier(conn, "Console Private Room", 140D, 5)
    End Sub

    Private Sub SeedEntertainmentTier(conn As MySqlConnection, EntertainmentTierName As String, HourlyRate As Decimal, EntertainmentTypeID As Integer)
        Dim checkSql As String =
        "SELECT COUNT(*) 
         FROM EntertainmentTier
         WHERE EntertainmentTierName = @EntertainmentTierName AND EntertainmentTypeID = @EntertainmentTypeID"

        Using cmd As New MySqlCommand(checkSql, conn)
            cmd.Parameters.AddWithValue("@EntertainmentTierName", EntertainmentTierName)
            cmd.Parameters.AddWithValue("@EntertainmentTypeID", EntertainmentTypeID)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
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

#Region "Products"
    Private Sub seedproducts(conn As MySqlConnection)
        SeedProduct(conn, "Ramen", 80D)
        SeedProduct(conn, "Coke Kasalo", 40D)
        SeedProduct(conn, "Energy Drink", 50D)
        SeedProduct(conn, "Snack Bar", 30D)
        SeedProduct(conn, "Gaming Headset", 500D)
        SeedProduct(conn, "Gaming Mouse", 300D)
        SeedProduct(conn, "Gaming Keyboard", 400D)
        SeedProduct(conn, "VR Controller", 6000D)
    End Sub
    Private Sub SeedProduct(conn As MySqlConnection, ProductName As String, UnitPrice As Decimal)
        Dim checkSql As String =
           "SELECT COUNT(*) 
         FROM Products
         WHERE ProductName = @ProductName"
        Using cmd As New MySqlCommand(checkSql, conn)
            cmd.Parameters.AddWithValue("@ProductName", ProductName)
            Dim count As Integer =
            Convert.ToInt32(cmd.ExecuteScalar())
            If count = 0 Then
                Dim insertSql As String =
                "INSERT INTO Products
                 (ProductName, UnitPrice)
                 VALUES
                 (@ProductName, @UnitPrice)"
                Using insertCmd As New MySqlCommand(insertSql, conn)
                    insertCmd.Parameters.AddWithValue("@ProductName", ProductName)
                    insertCmd.Parameters.AddWithValue("@UnitPrice", UnitPrice)
                    insertCmd.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub

#End Region

#Region "Entertainment"
    Private Sub SeedEntertainmentItems(conn As MySqlConnection)
        ' Seeding individual units into the Entertainment table matching your schema

        ' PC Gaming - Standard Tier (TierID: 1)
        SeedEntertainment(conn, 1, "PC Station 01", "Available")
        SeedEntertainment(conn, 1, "PC Station 02", "Available")
        SeedEntertainment(conn, 1, "PC Station 03", "Available")
        SeedEntertainment(conn, 1, "PC Station 04", "InUse")

        ' PC Gaming - VIP Lounge (TierID: 2)
        SeedEntertainment(conn, 2, "VIP PC Station 05", "Available")
        SeedEntertainment(conn, 2, "VIP PC Station 06", "Available")

        ' VR - Standard Booth (TierID: 3)
        SeedEntertainment(conn, 3, "VR Booth 01", "Available")
        SeedEntertainment(conn, 3, "VR Booth 02", "Available")

        ' VR - Premium Omni Treadmill (TierID: 4)
        SeedEntertainment(conn, 4, "VR Omni Treadmill 01", "Maintenance")

        ' Billiards - Standard Table (TierID: 5)
        SeedEntertainment(conn, 5, "Billiards Table 01", "Available")
        SeedEntertainment(conn, 5, "Billiards Table 02", "Available")

        ' Billiards - Tournament Table (TierID: 6)
        SeedEntertainment(conn, 6, "Tournament Billiards Table 03", "Available")

        ' Table Tennis - Standard Table (TierID: 7)
        SeedEntertainment(conn, 7, "Ping Pong Table 01", "Available")
        SeedEntertainment(conn, 7, "Ping Pong Table 02", "Available")

        ' Gaming Consoles - Console Lounge Couch (TierID: 8)
        SeedEntertainment(conn, 8, "Console Lounge 01 (PS5)", "Available")
        SeedEntertainment(conn, 8, "Console Lounge 02 (Xbox)", "Available")

        ' Gaming Consoles - Private Console Room (TierID: 9)
        SeedEntertainment(conn, 9, "Private Console Room A", "InUse")
    End Sub

    Private Sub SeedEntertainment(conn As MySqlConnection, EntertainmentTierID As Integer, EntertainmentName As String, Status As String)
        ' Checks uniqueness against EntertainmentName per your UNIQUE constraint
        Dim checkSql As String =
        "SELECT COUNT(*) 
         FROM Entertainment
         WHERE EntertainmentName = @EntertainmentName"

        Using cmd As New MySqlCommand(checkSql, conn)
            cmd.Parameters.AddWithValue("@EntertainmentName", EntertainmentName)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If count = 0 Then
                Dim insertSql As String =
                "INSERT INTO Entertainment
                 (EntertainmentTierID, EntertainmentName, Status)
                 VALUES
                 (@EntertainmentTierID, @EntertainmentName, @Status)"

                Using insertCmd As New MySqlCommand(insertSql, conn)
                    insertCmd.Parameters.AddWithValue("@EntertainmentTierID", EntertainmentTierID)
                    insertCmd.Parameters.AddWithValue("@EntertainmentName", EntertainmentName)
                    insertCmd.Parameters.AddWithValue("@Status", Status)
                    insertCmd.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub
#End Region
End Module

