Module DatabaseInitializerModule
    Public Sub SeedDatabase()

        SeedMembershipLevels()
        SeedRoles()
        SeedEntertainmentTypes()
        SeedEntertainmentTiers()

    End Sub

    Private Sub SeedMembershipLevels()

        ' Check and insert Basic, Premium, VIP

    End Sub

    Private Sub SeedRoles()

        ' Check and insert Admin, Cashier, Staff

    End Sub

    Private Sub SeedEntertainmentTypes()

        ' Check and insert PC, Console

    End Sub

    Private Sub SeedEntertainmentTiers()

        ' Check and insert Standard PC, High-End PC, PS5

    End Sub

End Module
