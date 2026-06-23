Imports System.Drawing
Imports System.Drawing.Text

Public Class AppFonts

    Private Shared ReadOnly HwygwdeCollection As New PrivateFontCollection()
    Private Shared ReadOnly HwygothCollection As New PrivateFontCollection()
    Private Shared ReadOnly VenusRisingCollection As New PrivateFontCollection()

    Shared Sub New()
        HwygwdeCollection.AddFontFile(IO.Path.Combine(Application.StartupPath, "Assets\Fonts\HWYGWDE.ttf"))
        HwygothCollection.AddFontFile(IO.Path.Combine(Application.StartupPath, "Assets\Fonts\HWYGOTH.ttf"))
        VenusRisingCollection.AddFontFile(IO.Path.Combine(Application.StartupPath, "Assets\Fonts\Venus Rising Rg.otf"))
    End Sub

    Public Shared Function Hwygwde(size As Single) As Font
        Return New Font(HwygwdeCollection.Families(0), size, FontStyle.Regular)
    End Function

    Public Shared Function Hwygoth(size As Single) As Font
        Return New Font(HwygothCollection.Families(0), size, FontStyle.Bold)
    End Function

    Public Shared Function VenusRising(size As Single) As Font
        Return New Font(VenusRisingCollection.Families(0), size)
    End Function



End Class