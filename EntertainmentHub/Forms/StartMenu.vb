Public Class StartMenu
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = AppColors.BackgroundStart

        lblnotice.Font = AppFonts.Hwygwde(10)
        lblTitle.Font = AppFonts.VenusRising(8)
        lblnotice.Parent = PictureBox2
        lblTime.Parent = PictureBox2
        lblTitle.Parent = PictureBox2

        btnUserLogin.Parent = PictureBox2
        btnAdminLogin.Parent = PictureBox2

        Timer1.Start()
        lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt")

        DatabaseInitializerModule.SeedDatabase()
    End Sub

#Region "Button Hover effects"
    Private Sub BtnUserLoginEnter(sender As Object, e As EventArgs) Handles btnUserLogin.MouseEnter
        btnUserLogin.Image = My.Resources.user_login_btn_state_2
    End Sub

    Private Sub BtnUserLoginLeave(sender As Object, e As EventArgs) Handles btnUserLogin.MouseLeave
        btnUserLogin.Image = My.Resources.user_login_btn_state_1
    End Sub

    Private Sub BtnAdminLoginEnter(sender As Object, e As EventArgs) Handles btnAdminLogin.MouseEnter
        btnAdminLogin.Image = My.Resources.admin_login_btn_state_2
    End Sub

    Private Sub BtnAdminLoginLeave(sender As Object, e As EventArgs) Handles btnAdminLogin.MouseLeave
        btnAdminLogin.Image = My.Resources.admin_login_btn_state_1
    End Sub

#End Region

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt")
    End Sub

    Private Sub UserLoginClick(sender As Object, e As EventArgs) Handles btnUserLogin.Click
        HelperFunc.SwitchForm(Me, New UserLoginMenu())
    End Sub

    Private Sub AdminLoginClick(sender As Object, e As EventArgs) Handles btnAdminLogin.Click
        HelperFunc.SwitchForm(Me, New AdminLoginMenu())
    End Sub

    Private Sub lblTime_Click(sender As Object, e As EventArgs) Handles lblTime.Click
        HelperFunc.SwitchForm(Me, New AdminDashboard())
    End Sub
End Class