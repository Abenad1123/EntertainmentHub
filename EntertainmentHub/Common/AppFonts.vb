Imports System.Drawing
Imports System.Drawing.Text
Imports System.IO
Imports System.Windows.Forms

Public Class AppFonts

    Private Shared ReadOnly HwygwdeCollection As New PrivateFontCollection()
    Private Shared ReadOnly HwygothCollection As New PrivateFontCollection()
    Private Shared ReadOnly VenusRisingCollection As New PrivateFontCollection()
    Private Shared ReadOnly AreoCollection As New PrivateFontCollection()
    Private Shared ReadOnly CoolveticaCollection As New PrivateFontCollection()
    Private Shared ReadOnly CdSaverCollection As New PrivateFontCollection()

    Shared Sub New()
        LoadFontSafe(HwygwdeCollection, "Assets\Fonts\HWYGWDE.ttf")
        LoadFontSafe(HwygothCollection, "Assets\Fonts\HWYGOTH.ttf")
        LoadFontSafe(VenusRisingCollection, "Assets\Fonts\Venus Rising Rg.otf")
        LoadFontSafe(AreoCollection, "Assets\Fonts\Aero.ttf")
        LoadFontSafe(CoolveticaCollection, "Assets\Fonts\Coolvetica Rg.otf")
        LoadFontSafe(CdSaverCollection, "Assets\Fonts\CodeSaver-Regular.otf")
    End Sub

    Private Shared Sub LoadFontSafe(collection As PrivateFontCollection, relativePath As String)
        Dim fullPath As String = Path.Combine(Application.StartupPath, relativePath)
        If File.Exists(fullPath) Then
            collection.AddFontFile(fullPath)
        Else
            MsgBox("Eror")
        End If
    End Sub

    Private Shared Function GetSafeFont(collection As PrivateFontCollection, size As Single, style As FontStyle) As Font
        If collection.Families.Length > 0 Then
            Return New Font(collection.Families(0), size, style)
        Else
            Return New Font("Segoe UI", size, style)
        End If
    End Function

    Public Shared Function Hwygwde(size As Single) As Font
        Return GetSafeFont(HwygwdeCollection, size, FontStyle.Regular)
    End Function

    Public Shared Function Hwygoth(size As Single) As Font
        Return GetSafeFont(HwygothCollection, size, FontStyle.Bold)
    End Function

    Public Shared Function VenusRising(size As Single) As Font
        Return GetSafeFont(VenusRisingCollection, size, FontStyle.Regular)
    End Function

    Public Shared Function Aero(size As Single) As Font
        Return GetSafeFont(AreoCollection, size, FontStyle.Regular)
    End Function

    Public Shared Function Coolvetica(size As Single) As Font
        Return GetSafeFont(CoolveticaCollection, size, FontStyle.Regular)
    End Function

    Public Shared Function CdSaver(size As Single) As Font
        Return GetSafeFont(CdSaverCollection, size, FontStyle.Regular)
    End Function

End Class