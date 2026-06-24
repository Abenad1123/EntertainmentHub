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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.txtboxUsernameInput = New System.Windows.Forms.TextBox()
        Me.txtboxAmount = New System.Windows.Forms.TextBox()
        Me.btnDeposit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnWithdraw = New System.Windows.Forms.Button()
        Me.btnBonus = New System.Windows.Forms.Button()
        Me.btnAdjust = New System.Windows.Forms.Button()
        Me.btnPayment = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.txtActionLog = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 68)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(481, 432)
        Me.DataGridView1.TabIndex = 0
        '
        'txtboxUsernameInput
        '
        Me.txtboxUsernameInput.Location = New System.Drawing.Point(735, 68)
        Me.txtboxUsernameInput.Name = "txtboxUsernameInput"
        Me.txtboxUsernameInput.Size = New System.Drawing.Size(261, 30)
        Me.txtboxUsernameInput.TabIndex = 1
        '
        'txtboxAmount
        '
        Me.txtboxAmount.Location = New System.Drawing.Point(735, 147)
        Me.txtboxAmount.Name = "txtboxAmount"
        Me.txtboxAmount.Size = New System.Drawing.Size(199, 30)
        Me.txtboxAmount.TabIndex = 2
        '
        'btnDeposit
        '
        Me.btnDeposit.Location = New System.Drawing.Point(564, 312)
        Me.btnDeposit.Name = "btnDeposit"
        Me.btnDeposit.Size = New System.Drawing.Size(126, 40)
        Me.btnDeposit.TabIndex = 4
        Me.btnDeposit.Text = "Deposit"
        Me.btnDeposit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(647, 150)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 25)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Amount"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(135, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(244, 25)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "WALLET TRANSACTION"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(627, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 25)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Username"
        '
        'btnWithdraw
        '
        Me.btnWithdraw.Location = New System.Drawing.Point(720, 312)
        Me.btnWithdraw.Name = "btnWithdraw"
        Me.btnWithdraw.Size = New System.Drawing.Size(126, 40)
        Me.btnWithdraw.TabIndex = 9
        Me.btnWithdraw.Text = "Withdraw"
        Me.btnWithdraw.UseVisualStyleBackColor = True
        '
        'btnBonus
        '
        Me.btnBonus.Location = New System.Drawing.Point(881, 312)
        Me.btnBonus.Name = "btnBonus"
        Me.btnBonus.Size = New System.Drawing.Size(126, 40)
        Me.btnBonus.TabIndex = 10
        Me.btnBonus.Text = "Bonus"
        Me.btnBonus.UseVisualStyleBackColor = True
        '
        'btnAdjust
        '
        Me.btnAdjust.Location = New System.Drawing.Point(1045, 312)
        Me.btnAdjust.Name = "btnAdjust"
        Me.btnAdjust.Size = New System.Drawing.Size(126, 40)
        Me.btnAdjust.TabIndex = 11
        Me.btnAdjust.Text = "Adjust"
        Me.btnAdjust.UseVisualStyleBackColor = True
        '
        'btnPayment
        '
        Me.btnPayment.Location = New System.Drawing.Point(564, 390)
        Me.btnPayment.Name = "btnPayment"
        Me.btnPayment.Size = New System.Drawing.Size(126, 40)
        Me.btnPayment.TabIndex = 12
        Me.btnPayment.Text = "Payment"
        Me.btnPayment.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(720, 390)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(126, 40)
        Me.Button5.TabIndex = 13
        Me.Button5.Text = "Refund"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'txtActionLog
        '
        Me.txtActionLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActionLog.CausesValidation = False
        Me.txtActionLog.Location = New System.Drawing.Point(12, 557)
        Me.txtActionLog.Multiline = True
        Me.txtActionLog.Name = "txtActionLog"
        Me.txtActionLog.ShortcutsEnabled = False
        Me.txtActionLog.Size = New System.Drawing.Size(481, 223)
        Me.txtActionLog.TabIndex = 14
        Me.txtActionLog.TabStop = False
        Me.txtActionLog.WordWrap = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(179, 529)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 25)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "ACTION LOG"
        '
        'TransactionManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1397, 885)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtActionLog)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.btnPayment)
        Me.Controls.Add(Me.btnAdjust)
        Me.Controls.Add(Me.btnBonus)
        Me.Controls.Add(Me.btnWithdraw)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnDeposit)
        Me.Controls.Add(Me.txtboxAmount)
        Me.Controls.Add(Me.txtboxUsernameInput)
        Me.Controls.Add(Me.DataGridView1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "TransactionManager"
        Me.Text = "Admin"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents txtboxUsernameInput As TextBox
    Friend WithEvents txtboxAmount As TextBox
    Friend WithEvents btnDeposit As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnWithdraw As Button
    Friend WithEvents btnBonus As Button
    Friend WithEvents btnAdjust As Button
    Friend WithEvents btnPayment As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents txtActionLog As TextBox
    Friend WithEvents Label3 As Label
End Class
