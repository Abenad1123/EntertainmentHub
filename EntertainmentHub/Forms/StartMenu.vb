Public Class StartMenu
    Private Sub Initialization(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = AppColors.BackgroundStart

        lblnotice.Font = AppFonts.Hwygwde(10)
        lblnotice.Parent = PictureBox2
        lblTime.Parent = PictureBox2

        Button2.Parent = PictureBox2
        Button1.Parent = PictureBox2

        Timer1.Start()
        lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New UserLoginMenu()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frm As New AdminLoginMenu()
        frm.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New MainMenu()
        frm.Show()
        Me.Close()
    End Sub
End Class