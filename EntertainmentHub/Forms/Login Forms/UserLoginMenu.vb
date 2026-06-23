Public Class UserLoginMenu
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = AppColors.Background

        lblPassword.Font = AppFonts.Hwygoth(14)
        lblUsername.Font = AppFonts.Hwygoth(14)
        lblTitle.Font = AppFonts.VenusRising(20)
        txtboxUsername.Font = AppFonts.Hwygwde(14)
        txtboxPassword.Font = AppFonts.Hwygwde(14)

        btnLogin.Font = AppFonts.Hwygoth(16)
    End Sub

    Private Sub GoBack(sender As Object, e As EventArgs) Handles lblGoBack.Click
        Dim frm As New StartMenu()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub GoBackBtnHoverIn(sender As Object, e As EventArgs) Handles lblGoBack.MouseEnter
        lblGoBack.ForeColor = AppColors.ForeColorHover
    End Sub

    Private Sub GoBackBtnHoverOut(sender As Object, e As EventArgs) Handles lblGoBack.MouseLeave
        lblGoBack.ForeColor = Color.White
    End Sub
End Class