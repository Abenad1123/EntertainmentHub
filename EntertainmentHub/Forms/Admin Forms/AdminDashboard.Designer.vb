<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminDashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDashboard))
        Me.btnOpenManageUser = New System.Windows.Forms.Button()
        Me.btnOpenManageEmployee = New System.Windows.Forms.Button()
        Me.btnOpenManageEntertainment = New System.Windows.Forms.Button()
        Me.btnOpenManageProduct = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btnOpenProductPOS = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOpenManageUser
        '
        Me.btnOpenManageUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnOpenManageUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOpenManageUser.FlatAppearance.BorderSize = 0
        Me.btnOpenManageUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenManageUser.Location = New System.Drawing.Point(10, 175)
        Me.btnOpenManageUser.Margin = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.btnOpenManageUser.Name = "btnOpenManageUser"
        Me.btnOpenManageUser.Size = New System.Drawing.Size(550, 40)
        Me.btnOpenManageUser.TabIndex = 22
        Me.btnOpenManageUser.Text = "USER"
        Me.btnOpenManageUser.UseVisualStyleBackColor = False
        '
        'btnOpenManageEmployee
        '
        Me.btnOpenManageEmployee.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnOpenManageEmployee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOpenManageEmployee.FlatAppearance.BorderSize = 0
        Me.btnOpenManageEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenManageEmployee.Location = New System.Drawing.Point(10, 225)
        Me.btnOpenManageEmployee.Margin = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.btnOpenManageEmployee.Name = "btnOpenManageEmployee"
        Me.btnOpenManageEmployee.Size = New System.Drawing.Size(550, 40)
        Me.btnOpenManageEmployee.TabIndex = 23
        Me.btnOpenManageEmployee.Text = "EMPLOYEE"
        Me.btnOpenManageEmployee.UseVisualStyleBackColor = False
        '
        'btnOpenManageEntertainment
        '
        Me.btnOpenManageEntertainment.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnOpenManageEntertainment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOpenManageEntertainment.FlatAppearance.BorderSize = 0
        Me.btnOpenManageEntertainment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenManageEntertainment.Location = New System.Drawing.Point(10, 75)
        Me.btnOpenManageEntertainment.Margin = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.btnOpenManageEntertainment.Name = "btnOpenManageEntertainment"
        Me.btnOpenManageEntertainment.Size = New System.Drawing.Size(550, 40)
        Me.btnOpenManageEntertainment.TabIndex = 24
        Me.btnOpenManageEntertainment.Text = "ENTERTAINMENT"
        Me.btnOpenManageEntertainment.UseVisualStyleBackColor = False
        '
        'btnOpenManageProduct
        '
        Me.btnOpenManageProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnOpenManageProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOpenManageProduct.FlatAppearance.BorderSize = 0
        Me.btnOpenManageProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenManageProduct.Location = New System.Drawing.Point(10, 125)
        Me.btnOpenManageProduct.Margin = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.btnOpenManageProduct.Name = "btnOpenManageProduct"
        Me.btnOpenManageProduct.Size = New System.Drawing.Size(550, 40)
        Me.btnOpenManageProduct.TabIndex = 25
        Me.btnOpenManageProduct.Text = "PRODUCT"
        Me.btnOpenManageProduct.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblTitle, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button4, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 1, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 7
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1200, 703)
        Me.TableLayoutPanel1.TabIndex = 28
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblTitle, 2)
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(33, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(1134, 75)
        Me.lblTitle.TabIndex = 14
        Me.lblTitle.Text = "ADMIN DASHBOARD"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnOpenManageEntertainment, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.btnOpenManageEmployee, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.btnOpenManageProduct, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.btnOpenManageUser, 0, 4)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(30, 230)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 7
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(570, 412)
        Me.TableLayoutPanel2.TabIndex = 30
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(564, 50)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Management Tools"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Button6, 0, 6)
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Button3, 0, 5)
        Me.TableLayoutPanel3.Controls.Add(Me.btnOpenProductPOS, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Button2, 0, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.Button1, 0, 3)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(600, 230)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 8
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(570, 412)
        Me.TableLayoutPanel3.TabIndex = 31
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(564, 50)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Menu"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.Button3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Location = New System.Drawing.Point(10, 225)
        Me.Button3.Margin = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(550, 40)
        Me.Button3.TabIndex = 30
        Me.Button3.Text = "Settings"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'btnOpenProductPOS
        '
        Me.btnOpenProductPOS.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnOpenProductPOS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOpenProductPOS.FlatAppearance.BorderSize = 0
        Me.btnOpenProductPOS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenProductPOS.Location = New System.Drawing.Point(10, 75)
        Me.btnOpenProductPOS.Margin = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.btnOpenProductPOS.Name = "btnOpenProductPOS"
        Me.btnOpenProductPOS.Size = New System.Drawing.Size(550, 40)
        Me.btnOpenProductPOS.TabIndex = 26
        Me.btnOpenProductPOS.Text = "Product POS"
        Me.btnOpenProductPOS.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(10, 175)
        Me.Button2.Margin = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(550, 40)
        Me.Button2.TabIndex = 29
        Me.Button2.Text = "Analytics"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(10, 125)
        Me.Button1.Margin = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(550, 40)
        Me.Button1.TabIndex = 28
        Me.Button1.Text = "Revenue Report"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(33, 645)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(172, 44)
        Me.Button4.TabIndex = 32
        Me.Button4.Text = "Logout"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.Label3, 2)
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(33, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1134, 100)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Label3"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.Button6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Location = New System.Drawing.Point(10, 275)
        Me.Button6.Margin = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(550, 40)
        Me.Button6.TabIndex = 32
        Me.Button6.Text = "View Audit Log"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'AdminDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 703)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "AdminDashboard"
        Me.Text = "Admin"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOpenManageUser As Button
    Friend WithEvents btnOpenManageEmployee As Button
    Friend WithEvents btnOpenManageEntertainment As Button
    Friend WithEvents btnOpenManageProduct As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblTitle As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnOpenProductPOS As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Button4 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Button6 As Button
End Class
