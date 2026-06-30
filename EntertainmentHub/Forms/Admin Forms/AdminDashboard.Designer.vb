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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnOpenManageUser = New System.Windows.Forms.Button()
        Me.btnOpenManageEmployee = New System.Windows.Forms.Button()
        Me.btnOpenManageEntertainment = New System.Windows.Forms.Button()
        Me.btnOpenManageProduct = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblEntertainment = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOpenProductPOS = New System.Windows.Forms.Button()
        Me.txtboxSearchBox = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(33, 88)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.TableLayoutPanel1.SetRowSpan(Me.DataGridView1, 2)
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(564, 602)
        Me.DataGridView1.TabIndex = 13
        '
        'btnOpenManageUser
        '
        Me.btnOpenManageUser.BackColor = System.Drawing.Color.White
        Me.btnOpenManageUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenManageUser.Location = New System.Drawing.Point(12, 200)
        Me.btnOpenManageUser.Name = "btnOpenManageUser"
        Me.btnOpenManageUser.Size = New System.Drawing.Size(240, 39)
        Me.btnOpenManageUser.TabIndex = 22
        Me.btnOpenManageUser.Text = "USER"
        Me.btnOpenManageUser.UseVisualStyleBackColor = False
        '
        'btnOpenManageEmployee
        '
        Me.btnOpenManageEmployee.BackColor = System.Drawing.Color.White
        Me.btnOpenManageEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenManageEmployee.Location = New System.Drawing.Point(12, 245)
        Me.btnOpenManageEmployee.Name = "btnOpenManageEmployee"
        Me.btnOpenManageEmployee.Size = New System.Drawing.Size(240, 39)
        Me.btnOpenManageEmployee.TabIndex = 23
        Me.btnOpenManageEmployee.Text = "EMPLOYEE"
        Me.btnOpenManageEmployee.UseVisualStyleBackColor = False
        '
        'btnOpenManageEntertainment
        '
        Me.btnOpenManageEntertainment.BackColor = System.Drawing.Color.White
        Me.btnOpenManageEntertainment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenManageEntertainment.Location = New System.Drawing.Point(12, 110)
        Me.btnOpenManageEntertainment.Name = "btnOpenManageEntertainment"
        Me.btnOpenManageEntertainment.Size = New System.Drawing.Size(240, 39)
        Me.btnOpenManageEntertainment.TabIndex = 24
        Me.btnOpenManageEntertainment.Text = "ENTERTAINMENT"
        Me.btnOpenManageEntertainment.UseVisualStyleBackColor = False
        '
        'btnOpenManageProduct
        '
        Me.btnOpenManageProduct.BackColor = System.Drawing.Color.White
        Me.btnOpenManageProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenManageProduct.Location = New System.Drawing.Point(12, 155)
        Me.btnOpenManageProduct.Name = "btnOpenManageProduct"
        Me.btnOpenManageProduct.Size = New System.Drawing.Size(240, 39)
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
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView1, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTitle, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 2, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.lblEntertainment)
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Controls.Add(Me.ComboBox2)
        Me.Panel1.Controls.Add(Me.ComboBox1)
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(600, 85)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(570, 304)
        Me.Panel1.TabIndex = 29
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.Panel3.Controls.Add(Me.txtboxSearchBox)
        Me.Panel3.Location = New System.Drawing.Point(19, 21)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(381, 33)
        Me.Panel3.TabIndex = 6
        '
        'lblEntertainment
        '
        Me.lblEntertainment.AutoSize = True
        Me.lblEntertainment.Location = New System.Drawing.Point(14, 119)
        Me.lblEntertainment.Name = "lblEntertainment"
        Me.lblEntertainment.Size = New System.Drawing.Size(132, 25)
        Me.lblEntertainment.TabIndex = 5
        Me.lblEntertainment.Text = "Entertainment"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(78, 76)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(68, 25)
        Me.lblStatus.TabIndex = 4
        Me.lblStatus.Text = "Status"
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(152, 116)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(119, 33)
        Me.ComboBox2.TabIndex = 3
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(152, 73)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 33)
        Me.ComboBox1.TabIndex = 2
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.ForeColor = System.Drawing.Color.Black
        Me.Button4.Location = New System.Drawing.Point(416, 21)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(101, 34)
        Me.Button4.TabIndex = 1
        Me.Button4.Text = "Search"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.btnOpenManageEntertainment)
        Me.Panel2.Controls.Add(Me.btnOpenProductPOS)
        Me.Panel2.Controls.Add(Me.btnOpenManageEmployee)
        Me.Panel2.Controls.Add(Me.btnOpenManageUser)
        Me.Panel2.Controls.Add(Me.btnOpenManageProduct)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(600, 389)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(570, 304)
        Me.Panel2.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(399, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 29)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Menu"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(11, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(246, 29)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Management Tools"
        '
        'btnOpenProductPOS
        '
        Me.btnOpenProductPOS.BackColor = System.Drawing.Color.White
        Me.btnOpenProductPOS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenProductPOS.Location = New System.Drawing.Point(316, 110)
        Me.btnOpenProductPOS.Name = "btnOpenProductPOS"
        Me.btnOpenProductPOS.Size = New System.Drawing.Size(240, 39)
        Me.btnOpenProductPOS.TabIndex = 26
        Me.btnOpenProductPOS.Text = "Product POS"
        Me.btnOpenProductPOS.UseVisualStyleBackColor = False
        '
        'txtboxSearchBox
        '
        Me.txtboxSearchBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtboxSearchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtboxSearchBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtboxSearchBox.ForeColor = System.Drawing.SystemColors.InactiveBorder
        Me.txtboxSearchBox.Location = New System.Drawing.Point(0, 0)
        Me.txtboxSearchBox.Margin = New System.Windows.Forms.Padding(0)
        Me.txtboxSearchBox.Multiline = True
        Me.txtboxSearchBox.Name = "txtboxSearchBox"
        Me.txtboxSearchBox.Size = New System.Drawing.Size(381, 30)
        Me.txtboxSearchBox.TabIndex = 0
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
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnOpenManageUser As Button
    Friend WithEvents btnOpenManageEmployee As Button
    Friend WithEvents btnOpenManageEntertainment As Button
    Friend WithEvents btnOpenManageProduct As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblTitle As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnOpenProductPOS As Button
    Friend WithEvents lblEntertainment As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents txtboxSearchBox As TextBox
End Class
