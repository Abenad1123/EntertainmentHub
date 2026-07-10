Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

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
    Private Shared ReadOnly DarkSurfaceColor As Color = Color.FromArgb(31, 31, 34)
    Private Shared ReadOnly BorderThickness As Integer = 1

    Private Shared ReadOnly controlBorderConfigs As New Dictionary(Of Control, BorderSides)
    Private Shared ReadOnly controlRadii As New Dictionary(Of Control, Integer)
    Private Shared ReadOnly buttonDefaultIcons As New Dictionary(Of Button, Image)
    Private Shared ReadOnly buttonHoverIcons As New Dictionary(Of Button, Image)

    Private Shared Function GetRoundedPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim d As Integer = radius * 2
        path.AddArc(rect.X, rect.Y, d, d, 180, 90)
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    Public Shared Sub ApplyBorder(ctrl As Control, Optional sides As BorderSides = BorderSides.All, Optional borderRadius As Integer = 0)
        If TypeOf ctrl Is TableLayoutPanel Then
            DirectCast(ctrl, TableLayoutPanel).BorderStyle = BorderStyle.None
        ElseIf TypeOf ctrl Is Panel Then
            DirectCast(ctrl, Panel).BorderStyle = BorderStyle.None
        End If

        controlBorderConfigs(ctrl) = sides
        controlRadii(ctrl) = borderRadius

        RemoveHandler ctrl.Paint, AddressOf DrawCustomBorder
        RemoveHandler ctrl.Resize, AddressOf TriggerRedraw
        RemoveHandler ctrl.Disposed, AddressOf Control_Disposed

        AddHandler ctrl.Paint, AddressOf DrawCustomBorder
        AddHandler ctrl.Resize, AddressOf TriggerRedraw
        AddHandler ctrl.Disposed, AddressOf Control_Disposed

        TriggerRedraw(ctrl, EventArgs.Empty)
    End Sub

    Private Shared Sub DrawCustomBorder(sender As Object, e As PaintEventArgs)
        Dim ctrl As Control = DirectCast(sender, Control)
        If Not controlBorderConfigs.ContainsKey(ctrl) Then Return

        Dim sides As BorderSides = controlBorderConfigs(ctrl)
        Dim radius As Integer = If(controlRadii.ContainsKey(ctrl), controlRadii(ctrl), 0)

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        Using pen As New Pen(ThemeColor, BorderThickness)
            Dim w As Integer = ctrl.Width - 1
            Dim h As Integer = ctrl.Height - 1

            If radius > 0 Then
                Dim rect As New Rectangle(0, 0, w, h)
                Using path = GetRoundedPath(rect, radius)
                    e.Graphics.DrawPath(pen, path)
                End Using
            Else
                If sides.HasFlag(BorderSides.Top) Then e.Graphics.DrawLine(pen, 0, 0, w, 0)
                If sides.HasFlag(BorderSides.Right) Then e.Graphics.DrawLine(pen, w, 0, w, h)
                If sides.HasFlag(BorderSides.Bottom) Then e.Graphics.DrawLine(pen, 0, h, w, h)
                If sides.HasFlag(BorderSides.Left) Then e.Graphics.DrawLine(pen, 0, 0, 0, h)
            End If
        End Using
    End Sub

    Private Shared Sub TriggerRedraw(sender As Object, e As EventArgs)
        Dim ctrl As Control = DirectCast(sender, Control)

        If controlRadii.ContainsKey(ctrl) AndAlso controlRadii(ctrl) > 0 Then
            Dim rect As New Rectangle(0, 0, ctrl.Width, ctrl.Height)
            Using path = GetRoundedPath(rect, controlRadii(ctrl))
                ctrl.Region = New Region(path)
            End Using
        End If

        ctrl.Invalidate()
    End Sub

    Private Shared Sub Control_Disposed(sender As Object, e As EventArgs)
        Dim ctrl As Control = DirectCast(sender, Control)
        If controlBorderConfigs.ContainsKey(ctrl) Then controlBorderConfigs.Remove(ctrl)
        If controlRadii.ContainsKey(ctrl) Then controlRadii.Remove(ctrl)
    End Sub

    Public Shared Sub ApplyButtonTheme(btn As Button, Optional defaultIcon As Image = Nothing, Optional hoverIcon As Image = Nothing, Optional iconSize As Integer = 28, Optional borderRadius As Integer = 8)
        btn.FlatStyle = FlatStyle.Flat
        btn.Cursor = Cursors.Hand
        btn.BackColor = DarkSurfaceColor
        btn.ForeColor = Color.White
        btn.Font = AppFonts.Coolvetica(16)

        btn.FlatAppearance.MouseOverBackColor = ThemeColor
        btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, 200, 0)

        If borderRadius > 0 Then
            btn.FlatAppearance.BorderSize = 0
            ApplyBorder(btn, BorderSides.All, borderRadius)
        Else
            btn.FlatAppearance.BorderColor = ThemeColor
            btn.FlatAppearance.BorderSize = 1
        End If

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

    Public Shared Sub Log(conn As MySqlConnection, transaction As MySqlTransaction, empId As Integer, tableName As String, actionType As String, Optional description As String = "")
        Dim query As String = "INSERT INTO auditing (EmployeeID, TableName, ActionType, Description) VALUES (@empId, @table, @action, @desc)"
        Using cmd As New MySqlCommand(query, conn, transaction)
            cmd.Parameters.AddWithValue("@empId", empId)
            cmd.Parameters.AddWithValue("@table", If(String.IsNullOrWhiteSpace(tableName), DBNull.Value, tableName))
            cmd.Parameters.AddWithValue("@action", actionType)

            If String.IsNullOrWhiteSpace(description) Then
                cmd.Parameters.AddWithValue("@desc", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@desc", description.Trim())
            End If
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Shared Sub EnableDoubleBuffer(ctrl As Control)
        Dim propInfo As System.Reflection.PropertyInfo = ctrl.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
        If propInfo IsNot Nothing Then
            propInfo.SetValue(ctrl, True, Nothing)
        End If

        For Each child As Control In ctrl.Controls
            EnableDoubleBuffer(child)
        Next
    End Sub

    Public Shared Async Sub SwitchForm(currentForm As Form, nextForm As Form)
        nextForm.Opacity = 0
        nextForm.Show()
        nextForm.BringToFront()

        For i As Double = 0.0 To 1.0 Step 0.1
            nextForm.Opacity = i
            Await Task.Delay(10)
        Next

        nextForm.Opacity = 1.0

        currentForm.Hide()
        currentForm.Close()
    End Sub
End Class