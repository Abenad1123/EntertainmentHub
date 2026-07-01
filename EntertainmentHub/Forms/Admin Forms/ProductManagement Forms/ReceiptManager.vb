Imports System.IO
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Printing
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class ReceiptManager

    Private WithEvents receiptPrintDoc As New PrintDocument()

    Private Sub ReceiptManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtboxMainReceipt.Multiline = True
        txtboxMainReceipt.ScrollBars = ScrollBars.Vertical
        txtboxMainReceipt.ReadOnly = True
        txtboxMainReceipt.Font = New System.Drawing.Font("Courier New", 10)

        If AccountData.ReceiptLog IsNot Nothing AndAlso AccountData.ReceiptLog.Count > 0 Then
            GenerateReceiptContent()
        Else
            txtboxMainReceipt.Text = "No receipt data found."
        End If
    End Sub

    Private Function CenterText(text As String, width As Integer) As String
        If text.Length >= width Then Return text
        Dim leftPad As Integer = (width + text.Length) \ 2
        Return text.PadLeft(leftPad).PadRight(width)
    End Function

    Private Sub GenerateReceiptContent()
        Dim sb As New StringBuilder()
        Dim width As Integer = 40

        Dim customer = AccountData.ReceiptLog("CustomerUsername").ToString()
        Dim employee = AccountData.ReceiptLog("EmployeeUsername").ToString()
        Dim transDate As DateTime = Convert.ToDateTime(AccountData.ReceiptLog("TransactionDate"))
        Dim cart As DataTable = DirectCast(AccountData.ReceiptLog("CartItems"), DataTable)
        Dim total As Decimal = Convert.ToDecimal(AccountData.ReceiptLog("TotalAmount"))

        sb.AppendLine(CenterText("ENTERTAINMENT HUB", width))
        sb.AppendLine(CenterText("Parañaque, Metro Manila", width))
        sb.AppendLine(New String("="c, width))
        sb.AppendLine(CenterText("OFFICIAL SALES RECEIPT", width))
        sb.AppendLine(New String("="c, width))

        sb.AppendLine($"Date    : {transDate.ToString("yyyy-MM-dd HH:mm:ss")}")
        sb.AppendLine($"Cashier : {employee}")
        sb.AppendLine($"Customer: {customer}")
        sb.AppendLine(New String("-"c, width))
        sb.AppendLine(String.Format("{0,-20} {1,5} {2,13}", "Item", "Qty", "Subtotal"))
        sb.AppendLine(New String("-"c, width))

        For Each row As DataRow In cart.Rows
            Dim itemName As String = row("Name").ToString()
            If itemName.Length > 20 Then
                itemName = itemName.Substring(0, 17) & "..."
            End If

            Dim qty As Integer = Convert.ToInt32(row("Quantity"))
            Dim lineTotal As Decimal = Convert.ToDecimal(row("LineTotal"))

            sb.AppendLine(String.Format("{0,-20} {1,5} {2,13:C2}", itemName, qty, lineTotal))
        Next

        sb.AppendLine(New String("-"c, width))
        sb.AppendLine(String.Format("{0,-20} {1,19:C2}", "TOTAL AMOUNT:", total))
        sb.AppendLine(New String("="c, width))
        sb.AppendLine()
        sb.AppendLine(CenterText("THANK YOU FOR YOUR PURCHASE!", width))
        sb.AppendLine(CenterText("Please come again.", width))

        txtboxMainReceipt.Text = sb.ToString()
    End Sub

    Private Sub btnPrintReceipt_Click(sender As Object, e As EventArgs) Handles btnPrintReceipt.Click
        Dim printDialog As New PrintDialog()
        printDialog.Document = receiptPrintDoc

        If printDialog.ShowDialog() = DialogResult.OK Then
            receiptPrintDoc.Print()
        End If
    End Sub

    Private Sub receiptPrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles receiptPrintDoc.PrintPage
        Dim startX As Single = 10
        Dim startY As Single = 10
        Dim printFont As New System.Drawing.Font("Courier New", 10)

        If My.Resources.full_logo IsNot Nothing Then
            Dim logoWidth As Integer = 250
            Dim logoHeight As Integer = CInt((My.Resources.full_logo.Height / My.Resources.full_logo.Width) * logoWidth)
            Dim logoX As Single = startX + ((340 - logoWidth) / 2)

            e.Graphics.DrawImage(My.Resources.full_logo, logoX, startY, logoWidth, logoHeight)
            startY += logoHeight + 20
        End If

        e.Graphics.DrawString(txtboxMainReceipt.Text, printFont, Brushes.Black, startX, startY)
    End Sub

    Private Sub btnSavePDF_Click(sender As Object, e As EventArgs) Handles btnSavePDF.Click
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "PDF Document (*.pdf)|*.pdf"
        sfd.FileName = $"Receipt_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf"
        sfd.Title = "Save Receipt as PDF"

        If sfd.ShowDialog() = DialogResult.OK Then
            Try
                Dim receiptSize As New iTextSharp.text.Rectangle(250.0F, 800.0F)
                Dim doc As New Document(receiptSize, 15, 15, 20, 20)
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(sfd.FileName, FileMode.Create))
                doc.Open()

                Dim normalFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 9)
                Dim boldFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)
                Dim titleFont As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)

                If My.Resources.full_logo IsNot Nothing Then
                    Using ms As New MemoryStream()
                        My.Resources.full_logo.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                        Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(ms.ToArray())
                        logo.Alignment = Element.ALIGN_CENTER
                        logo.ScaleToFit(180.0F, 100.0F)
                        doc.Add(logo)
                    End Using
                End If

                Dim headerPara As New Paragraph()
                headerPara.Alignment = Element.ALIGN_CENTER
                headerPara.Add(New Chunk("ENTERTAINMENT HUB" & Environment.NewLine, titleFont))
                headerPara.Add(New Chunk("Parañaque, Metro Manila" & Environment.NewLine, normalFont))
                headerPara.Add(New Chunk("OFFICIAL SALES RECEIPT" & Environment.NewLine & Environment.NewLine, boldFont))
                doc.Add(headerPara)

                Dim customer = AccountData.ReceiptLog("CustomerUsername").ToString()
                Dim employee = AccountData.ReceiptLog("EmployeeUsername").ToString()
                Dim transDate As DateTime = Convert.ToDateTime(AccountData.ReceiptLog("TransactionDate"))

                Dim detailsPara As New Paragraph()
                detailsPara.Alignment = Element.ALIGN_LEFT
                detailsPara.Add(New Chunk($"Date    : {transDate.ToString("yyyy-MM-dd HH:mm:ss")}" & Environment.NewLine, normalFont))
                detailsPara.Add(New Chunk($"Cashier : {employee}" & Environment.NewLine, normalFont))
                detailsPara.Add(New Chunk($"Customer: {customer}" & Environment.NewLine & Environment.NewLine, normalFont))
                doc.Add(detailsPara)

                Dim drawLine As New iTextSharp.text.pdf.draw.DottedLineSeparator()
                doc.Add(New Chunk(drawLine))

                Dim table As New PdfPTable(3)
                table.WidthPercentage = 100
                table.SetWidths(New Single() {2.5F, 1.0F, 1.5F})
                table.SpacingBefore = 8
                table.SpacingAfter = 8

                Dim cellItem As New PdfPCell(New Phrase("Item", boldFont))
                cellItem.Border = iTextSharp.text.Rectangle.NO_BORDER
                Dim cellQty As New PdfPCell(New Phrase("Qty", boldFont))
                cellQty.Border = iTextSharp.text.Rectangle.NO_BORDER
                cellQty.HorizontalAlignment = Element.ALIGN_CENTER
                Dim cellSub As New PdfPCell(New Phrase("Subtotal", boldFont))
                cellSub.Border = iTextSharp.text.Rectangle.NO_BORDER
                cellSub.HorizontalAlignment = Element.ALIGN_RIGHT

                table.AddCell(cellItem)
                table.AddCell(cellQty)
                table.AddCell(cellSub)

                Dim cart As DataTable = DirectCast(AccountData.ReceiptLog("CartItems"), DataTable)
                For Each row As DataRow In cart.Rows
                    Dim iName As New PdfPCell(New Phrase(row("Name").ToString(), normalFont))
                    iName.Border = iTextSharp.text.Rectangle.NO_BORDER

                    Dim iQty As New PdfPCell(New Phrase(row("Quantity").ToString(), normalFont))
                    iQty.Border = iTextSharp.text.Rectangle.NO_BORDER
                    iQty.HorizontalAlignment = Element.ALIGN_CENTER

                    Dim lineTotal As Decimal = Convert.ToDecimal(row("LineTotal"))
                    Dim iSub As New PdfPCell(New Phrase(lineTotal.ToString("C2"), normalFont))
                    iSub.Border = iTextSharp.text.Rectangle.NO_BORDER
                    iSub.HorizontalAlignment = Element.ALIGN_RIGHT

                    table.AddCell(iName)
                    table.AddCell(iQty)
                    table.AddCell(iSub)
                Next

                doc.Add(table)
                doc.Add(New Chunk(drawLine))

                Dim total As Decimal = Convert.ToDecimal(AccountData.ReceiptLog("TotalAmount"))
                Dim totalTable As New PdfPTable(2)
                totalTable.WidthPercentage = 100
                totalTable.SetWidths(New Single() {3.0F, 2.0F})
                totalTable.SpacingBefore = 8

                Dim lblTotal As New PdfPCell(New Phrase("TOTAL AMOUNT:", boldFont))
                lblTotal.Border = iTextSharp.text.Rectangle.NO_BORDER

                Dim valTotal As New PdfPCell(New Phrase(total.ToString("C2"), boldFont))
                valTotal.Border = iTextSharp.text.Rectangle.NO_BORDER
                valTotal.HorizontalAlignment = Element.ALIGN_RIGHT

                totalTable.AddCell(lblTotal)
                totalTable.AddCell(valTotal)
                doc.Add(totalTable)

                Dim footerPara As New Paragraph()
                footerPara.Alignment = Element.ALIGN_CENTER
                footerPara.SpacingBefore = 20
                footerPara.Add(New Chunk("THANK YOU FOR YOUR PURCHASE!" & Environment.NewLine, boldFont))
                footerPara.Add(New Chunk("Please come again.", normalFont))
                doc.Add(footerPara)

                doc.Close()

                MessageBox.Show("Receipt successfully exported to PDF!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error generating PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

End Class