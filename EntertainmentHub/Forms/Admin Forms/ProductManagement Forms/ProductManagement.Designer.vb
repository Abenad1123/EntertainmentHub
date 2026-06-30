<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProductManagement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProductManagement))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnUpdateProd = New System.Windows.Forms.Button()
        Me.nudStockControl = New System.Windows.Forms.NumericUpDown()
        Me.btnDeleteProd = New System.Windows.Forms.Button()
        Me.txtboxName = New System.Windows.Forms.TextBox()
        Me.cmbboxCategory = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.nudCostPrice = New System.Windows.Forms.NumericUpDown()
        Me.nudUnitPrice = New System.Windows.Forms.NumericUpDown()
        Me.btnRegister = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnGoBack = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.nudStockControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCostPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudUnitPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnGoBack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnGoBack, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 2, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1482, 827)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblTitle)
        Me.Panel1.Controls.Add(Me.btnUpdate)
        Me.Panel1.Controls.Add(Me.btnUpdateProd)
        Me.Panel1.Controls.Add(Me.nudStockControl)
        Me.Panel1.Controls.Add(Me.btnDeleteProd)
        Me.Panel1.Controls.Add(Me.txtboxName)
        Me.Panel1.Controls.Add(Me.cmbboxCategory)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.nudCostPrice)
        Me.Panel1.Controls.Add(Me.nudUnitPrice)
        Me.Panel1.Controls.Add(Me.btnRegister)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(741, 10)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(731, 403)
        Me.Panel1.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(191, 18)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(404, 36)
        Me.lblTitle.TabIndex = 12
        Me.lblTitle.Text = "PRODUCT MANAGEMENT"
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.Chartreuse
        Me.btnUpdate.FlatAppearance.BorderSize = 0
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdate.Location = New System.Drawing.Point(460, 312)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(247, 39)
        Me.btnUpdate.TabIndex = 5
        Me.btnUpdate.Text = "Update stock"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'btnUpdateProd
        '
        Me.btnUpdateProd.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUpdateProd.FlatAppearance.BorderSize = 0
        Me.btnUpdateProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateProd.Location = New System.Drawing.Point(460, 186)
        Me.btnUpdateProd.Name = "btnUpdateProd"
        Me.btnUpdateProd.Size = New System.Drawing.Size(247, 39)
        Me.btnUpdateProd.TabIndex = 11
        Me.btnUpdateProd.Text = "Update Product"
        Me.btnUpdateProd.UseVisualStyleBackColor = False
        '
        'nudStockControl
        '
        Me.nudStockControl.Location = New System.Drawing.Point(166, 312)
        Me.nudStockControl.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudStockControl.Name = "nudStockControl"
        Me.nudStockControl.Size = New System.Drawing.Size(247, 30)
        Me.nudStockControl.TabIndex = 3
        Me.nudStockControl.ThousandsSeparator = True
        '
        'btnDeleteProd
        '
        Me.btnDeleteProd.BackColor = System.Drawing.Color.DarkRed
        Me.btnDeleteProd.FlatAppearance.BorderSize = 0
        Me.btnDeleteProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteProd.Location = New System.Drawing.Point(460, 141)
        Me.btnDeleteProd.Name = "btnDeleteProd"
        Me.btnDeleteProd.Size = New System.Drawing.Size(247, 39)
        Me.btnDeleteProd.TabIndex = 1
        Me.btnDeleteProd.Text = "Delete Product"
        Me.btnDeleteProd.UseVisualStyleBackColor = False
        '
        'txtboxName
        '
        Me.txtboxName.Location = New System.Drawing.Point(166, 99)
        Me.txtboxName.Name = "txtboxName"
        Me.txtboxName.Size = New System.Drawing.Size(247, 30)
        Me.txtboxName.TabIndex = 0
        '
        'cmbboxCategory
        '
        Me.cmbboxCategory.FormattingEnabled = True
        Me.cmbboxCategory.Location = New System.Drawing.Point(166, 138)
        Me.cmbboxCategory.Name = "cmbboxCategory"
        Me.cmbboxCategory.Size = New System.Drawing.Size(247, 33)
        Me.cmbboxCategory.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(65, 224)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 25)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Unit Price"
        '
        'nudCostPrice
        '
        Me.nudCostPrice.DecimalPlaces = 2
        Me.nudCostPrice.Location = New System.Drawing.Point(166, 181)
        Me.nudCostPrice.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudCostPrice.Name = "nudCostPrice"
        Me.nudCostPrice.Size = New System.Drawing.Size(247, 30)
        Me.nudCostPrice.TabIndex = 5
        Me.nudCostPrice.ThousandsSeparator = True
        '
        'nudUnitPrice
        '
        Me.nudUnitPrice.DecimalPlaces = 2
        Me.nudUnitPrice.Location = New System.Drawing.Point(166, 222)
        Me.nudUnitPrice.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudUnitPrice.Name = "nudUnitPrice"
        Me.nudUnitPrice.Size = New System.Drawing.Size(247, 30)
        Me.nudUnitPrice.TabIndex = 9
        Me.nudUnitPrice.ThousandsSeparator = True
        '
        'btnRegister
        '
        Me.btnRegister.BackColor = System.Drawing.Color.DarkOrange
        Me.btnRegister.FlatAppearance.BorderSize = 0
        Me.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRegister.Location = New System.Drawing.Point(460, 95)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(247, 40)
        Me.btnRegister.TabIndex = 5
        Me.btnRegister.Text = "Register Product"
        Me.btnRegister.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(58, 186)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 25)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Cost Price"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 25)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Product Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(68, 141)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 25)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Category"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(10, 10)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(0)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.TableLayoutPanel1.SetRowSpan(Me.DataGridView1, 2)
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(731, 645)
        Me.DataGridView1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(741, 413)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(731, 242)
        Me.Panel2.TabIndex = 3
        '
        'btnGoBack
        '
        Me.btnGoBack.Image = Global.EntertainmentHub.My.Resources.Resources.go_back_state_1
        Me.btnGoBack.Location = New System.Drawing.Point(13, 658)
        Me.btnGoBack.Name = "btnGoBack"
        Me.btnGoBack.Size = New System.Drawing.Size(50, 50)
        Me.btnGoBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnGoBack.TabIndex = 5
        Me.btnGoBack.TabStop = False
        '
        'ProductManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1482, 827)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MinimizeBox = False
        Me.Name = "ProductManagement"
        Me.Text = "Admin"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.nudStockControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCostPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudUnitPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnGoBack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnDeleteProd As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents nudStockControl As NumericUpDown
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnRegister As Button
    Friend WithEvents nudCostPrice As NumericUpDown
    Friend WithEvents cmbboxCategory As ComboBox
    Friend WithEvents txtboxName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents nudUnitPrice As NumericUpDown
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnUpdateProd As Button
    Friend WithEvents lblTitle As Label
    Friend WithEvents btnGoBack As PictureBox
End Class
