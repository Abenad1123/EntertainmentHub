Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections.Generic

Public Class HelperFunc

    <Flags>
    Public Enum BorderSides
        None = 0
        Top = 1
        Right = 2
        Bottom = 4
        Left = 8
        All = Top Or Right Or Bottom Or Left
    End Enum

    Private Shared ReadOnly ThemeColor As Color = Color.FromArgb(134, 255, 7)
    Private Shared ReadOnly DarkSurfaceColor As Color = Color.FromArgb(45, 45, 48)
    Private Shared ReadOnly BorderThickness As Integer = 1

    Private Shared ReadOnly controlBorderConfigs As New Dictionary(Of Control, BorderSides)
    Private Shared ReadOnly buttonDefaultIcons As New Dictionary(Of Button, Image)
    Private Shared ReadOnly buttonHoverIcons As New Dictionary(Of Button, Image)

    Public Shared Sub ApplyBorder(ctrl As Control, Optional sides As BorderSides = BorderSides.All)
        If TypeOf ctrl Is TableLayoutPanel Then
            DirectCast(ctrl, TableLayoutPanel).BorderStyle = BorderStyle.None
        ElseIf TypeOf ctrl Is Panel Then
            DirectCast(ctrl, Panel).BorderStyle = BorderStyle.None
        End If

        controlBorderConfigs(ctrl) = sides

        RemoveHandler ctrl.Paint, AddressOf DrawCustomBorder
        RemoveHandler ctrl.Resize, AddressOf TriggerRedraw
        RemoveHandler ctrl.Disposed, AddressOf Control_Disposed

        AddHandler ctrl.Paint, AddressOf DrawCustomBorder
        AddHandler ctrl.Resize, AddressOf TriggerRedraw
        AddHandler ctrl.Disposed, AddressOf Control_Disposed

        ctrl.Invalidate()
    End Sub

    Private Shared Sub DrawCustomBorder(sender As Object, e As PaintEventArgs)
        Dim ctrl As Control = DirectCast(sender, Control)

        If Not controlBorderConfigs.ContainsKey(ctrl) Then Return

        Dim sides As BorderSides = controlBorderConfigs(ctrl)

        Using pen As New Pen(ThemeColor, BorderThickness)
            Dim w As Integer = ctrl.Width - 1
            Dim h As Integer = ctrl.Height - 1

            If sides.HasFlag(BorderSides.Top) Then e.Graphics.DrawLine(pen, 0, 0, w, 0)
            If sides.HasFlag(BorderSides.Right) Then e.Graphics.DrawLine(pen, w, 0, w, h)
            If sides.HasFlag(BorderSides.Bottom) Then e.Graphics.DrawLine(pen, 0, h, w, h)
            If sides.HasFlag(BorderSides.Left) Then e.Graphics.DrawLine(pen, 0, 0, 0, h)
        End Using
    End Sub

    Private Shared Sub TriggerRedraw(sender As Object, e As EventArgs)
        Dim ctrl As Control = DirectCast(sender, Control)
        ctrl.Invalidate()
    End Sub

    Private Shared Sub Control_Disposed(sender As Object, e As EventArgs)
        Dim ctrl As Control = DirectCast(sender, Control)
        If controlBorderConfigs.ContainsKey(ctrl) Then
            controlBorderConfigs.Remove(ctrl)
        End If
    End Sub

    Public Shared Sub ApplyButtonTheme(btn As Button, Optional defaultIcon As Image = Nothing, Optional hoverIcon As Image = Nothing, Optional iconSize As Integer = 28)
        btn.FlatStyle = FlatStyle.Flat
        btn.Cursor = Cursors.Hand

        btn.BackColor = DarkSurfaceColor
        btn.ForeColor = Color.White
        btn.Font = AppFonts.Coolvetica(16)

        btn.FlatAppearance.BorderColor = ThemeColor
        btn.FlatAppearance.BorderSize = 1
        btn.FlatAppearance.MouseOverBackColor = ThemeColor
        btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, 200, 0)

        RemoveHandler btn.MouseEnter, AddressOf Button_MouseEnter
        RemoveHandler btn.MouseLeave, AddressOf Button_MouseLeave
        RemoveHandler btn.MouseDown, AddressOf Button_MouseDown
        RemoveHandler btn.MouseUp, AddressOf Button_MouseUp
        RemoveHandler btn.Disposed, AddressOf Button_Disposed

        If buttonDefaultIcons.ContainsKey(btn) Then buttonDefaultIcons.Remove(btn)
        If buttonHoverIcons.ContainsKey(btn) Then buttonHoverIcons.Remove(btn)

        If defaultIcon IsNot Nothing Then
            Dim resizedDefault As Image = New Bitmap(defaultIcon, New Size(iconSize, iconSize))

            btn.Image = resizedDefault
            btn.TextImageRelation = TextImageRelation.ImageBeforeText
            btn.ImageAlign = ContentAlignment.MiddleCenter
            btn.TextAlign = ContentAlignment.MiddleCenter
            btn.Padding = New Padding(10, 0, 0, 0)

            buttonDefaultIcons(btn) = resizedDefault

            If hoverIcon IsNot Nothing Then
                Dim resizedHover As Image = New Bitmap(hoverIcon, New Size(iconSize, iconSize))
                buttonHoverIcons(btn) = resizedHover
            End If
        Else
            btn.Image = Nothing
            btn.Padding = New Padding(0, 0, 0, 0)
        End If

        AddHandler btn.MouseEnter, AddressOf Button_MouseEnter
        AddHandler btn.MouseLeave, AddressOf Button_MouseLeave
        AddHandler btn.MouseDown, AddressOf Button_MouseDown
        AddHandler btn.MouseUp, AddressOf Button_MouseUp
        AddHandler btn.Disposed, AddressOf Button_Disposed
    End Sub

    Private Shared Sub Button_MouseEnter(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        btn.ForeColor = Color.Black

        If buttonHoverIcons.ContainsKey(btn) Then
            btn.Image = buttonHoverIcons(btn)
        End If
    End Sub

    Private Shared Sub Button_MouseLeave(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        btn.ForeColor = Color.White

        If buttonDefaultIcons.ContainsKey(btn) Then
            btn.Image = buttonDefaultIcons(btn)
        End If
    End Sub

    Private Shared Sub Button_MouseDown(sender As Object, e As MouseEventArgs)
        Dim btn As Button = DirectCast(sender, Button)

        Dim basePad As Integer = If(buttonDefaultIcons.ContainsKey(btn), 10, 0)
        btn.Padding = New Padding(basePad + 2, 2, 0, 0)
    End Sub

    Private Shared Sub Button_MouseUp(sender As Object, e As MouseEventArgs)
        Dim btn As Button = DirectCast(sender, Button)

        Dim basePad As Integer = If(buttonDefaultIcons.ContainsKey(btn), 10, 0)
        btn.Padding = New Padding(basePad, 0, 0, 0)
    End Sub

    Private Shared Sub Button_Disposed(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        If buttonDefaultIcons.ContainsKey(btn) Then buttonDefaultIcons.Remove(btn)
        If buttonHoverIcons.ContainsKey(btn) Then buttonHoverIcons.Remove(btn)
    End Sub

    Public Shared Sub FontDesign(ctrl As Control, color As Color, font As Font)
        ctrl.Font = font
        ctrl.ForeColor = color
    End Sub

End Class