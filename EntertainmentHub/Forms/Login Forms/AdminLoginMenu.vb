Public Class AdminLoginMenu
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = AppColors.Background

        Label1.BackColor = AppColors.Background
        Label2.BackColor = AppColors.Background

        Label1.Font = AppFonts.Hwygoth(14)
        Label2.Font = AppFonts.Hwygoth(14)
    End Sub
End Class