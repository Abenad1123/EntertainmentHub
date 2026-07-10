<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TransactionManager
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TransactionManager))
        Me.txtActionLog = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnGoBack = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnWithdraw = New System.Windows.Forms.Button()
        Me.btnDeposit = New System.Windows.Forms.Button()
        Me.btnAdjust = New System.Windows.Forms.Button()
        Me.btnBonus = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtboxAmount = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.btnPayment = New System.Windows.Forms.Button()
        Me.txtboxUsernameInput = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGoBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtActionLog
        '
        Me.txtActionLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActionLog.CausesValidation = False
        Me.txtActionLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtActionLog.Location = New System.Drawing.Point(13, 578)
        Me.txtActionLog.Multiline = True
        Me.txtActionLog.Name = "txtActionLog"
        Me.txtActionLog.ShortcutsEnabled = False
        Me.txtActionLog.Size = New System.Drawing.Size(672, 294)
        Me.txtActionLog.TabIndex = 14
        Me.txtActionLog.TabStop = False
        Me.txtActionLog.WordWrap = False
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(13, 108)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(672, 444)
        Me.DataGridView1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(672, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "WALLET TRANSACTION"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 555)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(672, 20)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "ACTION LOG"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnGoBack
        '
        Me.btnGoBack.Image = Global.EntertainmentHub.My.Resources.Resources.go_back_state_1
        Me.btnGoBack.Location = New System.Drawing.Point(1, 519)
        Me.btnGoBack.Margin = New System.Windows.Forms.Padding(1, 0, 0, 1)
        Me.btnGoBack.Name = "btnGoBack"
        Me.btnGoBack.Size = New System.Drawing.Size(50, 50)
        Me.btnGoBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnGoBack.TabIndex = 27
        Me.btnGoBack.TabStop = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.95349!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.04651!))
        Me.TableLayoutPanel2.Controls.Add(Me.txtboxUsernameInput, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.btnPayment, 0, 12)
        Me.TableLayoutPanel2.Controls.Add(Me.Button5, 0, 11)
        Me.TableLayoutPanel2.Controls.Add(Me.Label5, 0, 10)
        Me.TableLayoutPanel2.Controls.Add(Me.Label6, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.txtboxAmount, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.btnBonus, 0, 8)
        Me.TableLayoutPanel2.Controls.Add(Me.btnAdjust, 0, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.btnDeposit, 0, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.btnWithdraw, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.btnGoBack, 0, 14)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(708, 105)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0, 0, 0, 200)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 15
        Me.TableLayoutPanel1.SetRowSpan(Me.TableLayoutPanel2, 3)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(678, 570)
        Me.TableLayoutPanel2.TabIndex = 21
        '
        'btnWithdraw
        '
        Me.btnWithdraw.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnWithdraw.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel2.SetColumnSpan(Me.btnWithdraw, 2)
        Me.btnWithdraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnWithdraw.Location = New System.Drawing.Point(163, 163)
        Me.btnWithdraw.Name = "btnWithdraw"
        Me.btnWithdraw.Size = New System.Drawing.Size(352, 34)
        Me.btnWithdraw.TabIndex = 9
        Me.btnWithdraw.Text = "Withdraw"
        Me.btnWithdraw.UseVisualStyleBackColor = False
        '
        'btnDeposit
        '
        Me.btnDeposit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnDeposit.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel2.SetColumnSpan(Me.btnDeposit, 2)
        Me.btnDeposit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeposit.Location = New System.Drawing.Point(163, 203)
        Me.btnDeposit.Name = "btnDeposit"
        Me.btnDeposit.Size = New System.Drawing.Size(352, 34)
        Me.btnDeposit.TabIndex = 4
        Me.btnDeposit.Text = "Deposit"
        Me.btnDeposit.UseVisualStyleBackColor = False
        '
        'btnAdjust
        '
        Me.btnAdjust.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnAdjust.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel2.SetColumnSpan(Me.btnAdjust, 2)
        Me.btnAdjust.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdjust.Location = New System.Drawing.Point(163, 243)
        Me.btnAdjust.Name = "btnAdjust"
        Me.btnAdjust.Size = New System.Drawing.Size(352, 34)
        Me.btnAdjust.TabIndex = 11
        Me.btnAdjust.Text = "Adjust"
        Me.btnAdjust.UseVisualStyleBackColor = False
        '
        'btnBonus
        '
        Me.btnBonus.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnBonus.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel2.SetColumnSpan(Me.btnBonus, 2)
        Me.btnBonus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBonus.Location = New System.Drawing.Point(163, 283)
        Me.btnBonus.Name = "btnBonus"
        Me.btnBonus.Size = New System.Drawing.Size(352, 34)
        Me.btnBonus.TabIndex = 10
        Me.btnBonus.Text = "Bonus"
        Me.btnBonus.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(181, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 25)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Amount"
        '
        'txtboxAmount
        '
        Me.txtboxAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtboxAmount.Location = New System.Drawing.Point(267, 63)
        Me.txtboxAmount.Name = "txtboxAmount"
        Me.txtboxAmount.Size = New System.Drawing.Size(310, 30)
        Me.txtboxAmount.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(159, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 25)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Username"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label6.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.Label6, 2)
        Me.Label6.Location = New System.Drawing.Point(241, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(196, 25)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "WALLET BUTTONS"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label5.AutoSize = True
        Me.TableLayoutPanel2.SetColumnSpan(Me.Label5, 2)
        Me.Label5.Location = New System.Drawing.Point(256, 340)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(166, 25)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "SALE BUTTONS"
        '
        'Button5
        '
        Me.Button5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Button5.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel2.SetColumnSpan(Me.Button5, 2)
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Location = New System.Drawing.Point(163, 383)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(352, 34)
        Me.Button5.TabIndex = 13
        Me.Button5.Text = "Refund"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'btnPayment
        '
        Me.btnPayment.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnPayment.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel2.SetColumnSpan(Me.btnPayment, 2)
        Me.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPayment.Location = New System.Drawing.Point(163, 423)
        Me.btnPayment.Name = "btnPayment"
        Me.btnPayment.Size = New System.Drawing.Size(352, 34)
        Me.btnPayment.TabIndex = 12
        Me.btnPayment.Text = "Payment"
        Me.btnPayment.UseVisualStyleBackColor = False
        '
        'txtboxUsernameInput
        '
        Me.txtboxUsernameInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtboxUsernameInput.Location = New System.Drawing.Point(267, 23)
        Me.txtboxUsernameInput.Name = "txtboxUsernameInput"
        Me.txtboxUsernameInput.Size = New System.Drawing.Size(310, 30)
        Me.txtboxUsernameInput.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView1, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtActionLog, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 7
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1397, 885)
        Me.TableLayoutPanel1.TabIndex = 21
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label7, 3)
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(13, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1370, 75)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Transaction Manager"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TransactionManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1397, 885)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "TransactionManager"
        Me.Text = "Admin"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGoBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents txtActionLog As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnGoBack As PictureBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents txtboxUsernameInput As TextBox
    Friend WithEvents btnPayment As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtboxAmount As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnBonus As Button
    Friend WithEvents btnAdjust As Button
    Friend WithEvents btnDeposit As Button
    Friend WithEvents btnWithdraw As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label7 As Label
End Class
