<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProductManagement
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProductManagement))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnGoBack = New System.Windows.Forms.PictureBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NumericUpDown5 = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnUpdateProd = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnDeleteProd = New System.Windows.Forms.Button()
        Me.nudStockControl = New System.Windows.Forms.NumericUpDown()
        Me.btnRegister = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbboxCategory = New System.Windows.Forms.ComboBox()
        Me.nudUnitPrice = New System.Windows.Forms.NumericUpDown()
        Me.nudCostPrice = New System.Windows.Forms.NumericUpDown()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtboxName = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.btnGoBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.nudStockControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudUnitPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCostPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.TableLayoutPanel1.Controls.Add(Me.btnGoBack, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 3, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1482, 827)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'btnGoBack
        '
        Me.btnGoBack.Image = Global.EntertainmentHub.My.Resources.Resources.go_back_state_1
        Me.btnGoBack.Location = New System.Drawing.Point(10, 766)
        Me.btnGoBack.Margin = New System.Windows.Forms.Padding(0)
        Me.btnGoBack.Name = "btnGoBack"
        Me.btnGoBack.Size = New System.Drawing.Size(50, 50)
        Me.btnGoBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnGoBack.TabIndex = 5
        Me.btnGoBack.TabStop = False
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
        Me.TableLayoutPanel1.SetRowSpan(Me.DataGridView1, 3)
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(721, 756)
        Me.DataGridView1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.TableLayoutPanel4)
        Me.Panel2.Location = New System.Drawing.Point(751, 398)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(721, 368)
        Me.Panel2.TabIndex = 3
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 3
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel5, 1, 6)
        Me.TableLayoutPanel4.Controls.Add(Me.Label7, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.NumericUpDown5, 1, 8)
        Me.TableLayoutPanel4.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Label12, 0, 8)
        Me.TableLayoutPanel4.Controls.Add(Me.TextBox1, 1, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.Label10, 0, 6)
        Me.TableLayoutPanel4.Controls.Add(Me.Label6, 0, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.Label9, 0, 5)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel2, 1, 5)
        Me.TableLayoutPanel4.Controls.Add(Me.ComboBox1, 1, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.Button1, 2, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.Button2, 2, 3)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 12
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(721, 368)
        Me.TableLayoutPanel4.TabIndex = 26
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 3
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.NumericUpDown4, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Label11, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.NumericUpDown3, 2, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(180, 200)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(288, 40)
        Me.TableLayoutPanel5.TabIndex = 27
        '
        'NumericUpDown4
        '
        Me.NumericUpDown4.DecimalPlaces = 2
        Me.NumericUpDown4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumericUpDown4.Location = New System.Drawing.Point(3, 3)
        Me.NumericUpDown4.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown4.Name = "NumericUpDown4"
        Me.NumericUpDown4.Size = New System.Drawing.Size(128, 30)
        Me.NumericUpDown4.TabIndex = 19
        Me.NumericUpDown4.ThousandsSeparator = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label11.Location = New System.Drawing.Point(137, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(14, 40)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "-"
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.DecimalPlaces = 2
        Me.NumericUpDown3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumericUpDown3.Location = New System.Drawing.Point(157, 3)
        Me.NumericUpDown3.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(128, 30)
        Me.NumericUpDown3.TabIndex = 20
        Me.NumericUpDown3.ThousandsSeparator = True
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(41, 70)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(136, 25)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Product Name"
        '
        'NumericUpDown5
        '
        Me.NumericUpDown5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumericUpDown5.Location = New System.Drawing.Point(183, 253)
        Me.NumericUpDown5.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown5.Name = "NumericUpDown5"
        Me.NumericUpDown5.Size = New System.Drawing.Size(282, 30)
        Me.NumericUpDown5.TabIndex = 23
        Me.NumericUpDown5.ThousandsSeparator = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.TableLayoutPanel4.SetColumnSpan(Me.Label5, 4)
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(3, 1)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(715, 48)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "FILTER"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(115, 250)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(62, 25)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "Stock"
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(183, 73)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(282, 30)
        Me.TextBox1.TabIndex = 13
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(82, 200)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(95, 25)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Unit Price"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(85, 110)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 25)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Category"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(75, 160)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(102, 25)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Cost Price"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.NumericUpDown1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label8, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.NumericUpDown2, 2, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(180, 160)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(288, 40)
        Me.TableLayoutPanel2.TabIndex = 26
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.DecimalPlaces = 2
        Me.NumericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumericUpDown1.Location = New System.Drawing.Point(3, 3)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(128, 30)
        Me.NumericUpDown1.TabIndex = 13
        Me.NumericUpDown1.ThousandsSeparator = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Location = New System.Drawing.Point(137, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(14, 40)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "-"
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.DecimalPlaces = 2
        Me.NumericUpDown2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NumericUpDown2.Location = New System.Drawing.Point(157, 3)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(128, 30)
        Me.NumericUpDown2.TabIndex = 16
        Me.NumericUpDown2.ThousandsSeparator = True
        '
        'ComboBox1
        '
        Me.ComboBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(183, 113)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(282, 33)
        Me.ComboBox1.TabIndex = 14
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(507, 73)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(174, 34)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "Search"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(507, 113)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(175, 34)
        Me.Button2.TabIndex = 25
        Me.Button2.Text = "Reset"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.btnUpdate, 3, 7)
        Me.TableLayoutPanel3.Controls.Add(Me.lblTitle, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.btnUpdateProd, 3, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 0, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.btnDeleteProd, 3, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.nudStockControl, 1, 7)
        Me.TableLayoutPanel3.Controls.Add(Me.btnRegister, 3, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 0, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 0, 5)
        Me.TableLayoutPanel3.Controls.Add(Me.cmbboxCategory, 1, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.nudUnitPrice, 1, 5)
        Me.TableLayoutPanel3.Controls.Add(Me.nudCostPrice, 1, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.Label13, 0, 7)
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.txtboxName, 1, 2)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(751, 10)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 9
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(721, 368)
        Me.TableLayoutPanel3.TabIndex = 13
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.Chartreuse
        Me.btnUpdate.FlatAppearance.BorderSize = 0
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdate.Location = New System.Drawing.Point(478, 253)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(240, 34)
        Me.btnUpdate.TabIndex = 5
        Me.btnUpdate.Text = "Update stock"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.TableLayoutPanel3.SetColumnSpan(Me.lblTitle, 4)
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(3, 1)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(715, 48)
        Me.lblTitle.TabIndex = 12
        Me.lblTitle.Text = "PRODUCT MANAGEMENT"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnUpdateProd
        '
        Me.btnUpdateProd.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnUpdateProd.FlatAppearance.BorderSize = 0
        Me.btnUpdateProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateProd.Location = New System.Drawing.Point(478, 153)
        Me.btnUpdateProd.Name = "btnUpdateProd"
        Me.btnUpdateProd.Size = New System.Drawing.Size(240, 34)
        Me.btnUpdateProd.TabIndex = 11
        Me.btnUpdateProd.Text = "Update Product"
        Me.btnUpdateProd.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(80, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 25)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Category"
        '
        'btnDeleteProd
        '
        Me.btnDeleteProd.BackColor = System.Drawing.Color.DarkRed
        Me.btnDeleteProd.FlatAppearance.BorderSize = 0
        Me.btnDeleteProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteProd.Location = New System.Drawing.Point(478, 113)
        Me.btnDeleteProd.Name = "btnDeleteProd"
        Me.btnDeleteProd.Size = New System.Drawing.Size(240, 34)
        Me.btnDeleteProd.TabIndex = 1
        Me.btnDeleteProd.Text = "Delete Product"
        Me.btnDeleteProd.UseVisualStyleBackColor = False
        '
        'nudStockControl
        '
        Me.nudStockControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nudStockControl.Location = New System.Drawing.Point(178, 253)
        Me.nudStockControl.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudStockControl.Name = "nudStockControl"
        Me.nudStockControl.Size = New System.Drawing.Size(274, 30)
        Me.nudStockControl.TabIndex = 3
        Me.nudStockControl.ThousandsSeparator = True
        '
        'btnRegister
        '
        Me.btnRegister.BackColor = System.Drawing.Color.DarkOrange
        Me.btnRegister.FlatAppearance.BorderSize = 0
        Me.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRegister.Location = New System.Drawing.Point(478, 73)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(240, 34)
        Me.btnRegister.TabIndex = 5
        Me.btnRegister.Text = "Register Product"
        Me.btnRegister.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(70, 150)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 25)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Cost Price"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(77, 190)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 25)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Unit Price"
        '
        'cmbboxCategory
        '
        Me.cmbboxCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbboxCategory.FormattingEnabled = True
        Me.cmbboxCategory.Location = New System.Drawing.Point(178, 113)
        Me.cmbboxCategory.Name = "cmbboxCategory"
        Me.cmbboxCategory.Size = New System.Drawing.Size(274, 33)
        Me.cmbboxCategory.TabIndex = 1
        '
        'nudUnitPrice
        '
        Me.nudUnitPrice.DecimalPlaces = 2
        Me.nudUnitPrice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nudUnitPrice.Location = New System.Drawing.Point(178, 193)
        Me.nudUnitPrice.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudUnitPrice.Name = "nudUnitPrice"
        Me.nudUnitPrice.Size = New System.Drawing.Size(274, 30)
        Me.nudUnitPrice.TabIndex = 9
        Me.nudUnitPrice.ThousandsSeparator = True
        '
        'nudCostPrice
        '
        Me.nudCostPrice.DecimalPlaces = 2
        Me.nudCostPrice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nudCostPrice.Location = New System.Drawing.Point(178, 153)
        Me.nudCostPrice.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudCostPrice.Name = "nudCostPrice"
        Me.nudCostPrice.Size = New System.Drawing.Size(274, 30)
        Me.nudCostPrice.TabIndex = 5
        Me.nudCostPrice.ThousandsSeparator = True
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(32, 250)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(140, 25)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Stock Quantity"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 25)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Product Name"
        '
        'txtboxName
        '
        Me.txtboxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtboxName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtboxName.Location = New System.Drawing.Point(178, 73)
        Me.txtboxName.Name = "txtboxName"
        Me.txtboxName.Size = New System.Drawing.Size(274, 30)
        Me.txtboxName.TabIndex = 0
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
        Me.MaximizeBox = False
        Me.Name = "ProductManagement"
        Me.Text = "Admin"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.btnGoBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.nudStockControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudUnitPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCostPrice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
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
    Friend WithEvents Label8 As Label
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents NumericUpDown5 As NumericUpDown
    Friend WithEvents Label12 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents NumericUpDown3 As NumericUpDown
    Friend WithEvents NumericUpDown4 As NumericUpDown
    Friend WithEvents Label9 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Label13 As Label
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
End Class
