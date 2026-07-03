<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EntertainmentManagement
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EntertainmentManagement))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DataGridEntertainment = New System.Windows.Forms.DataGridView()
        Me.ComboBoxUsername = New System.Windows.Forms.ComboBox()
        Me.PanelShutdown = New System.Windows.Forms.Panel()
        Me.PanelMaintenance = New System.Windows.Forms.Panel()
        Me.PanelAvailable = New System.Windows.Forms.Panel()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.ComboBoxEntertainment = New System.Windows.Forms.ComboBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.LabelBalance = New System.Windows.Forms.Label()
        Me.LabelEntertainment = New System.Windows.Forms.Label()
        Me.LabelDuration = New System.Windows.Forms.Label()
        Me.LabelTotal = New System.Windows.Forms.Label()
        Me.LabelAvailable = New System.Windows.Forms.Label()
        Me.LabelInUse = New System.Windows.Forms.Label()
        Me.LabelMaintenance = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.LiveDurationTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridEntertainment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 17
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5643586!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.059041!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.23616!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.68144!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.047091!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.602955!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.047091!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.933579!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.846722!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.601108!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.210332!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.398524!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.505535!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.952029!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.121771!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.394834!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.103321!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ComboBoxUsername, 11, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelShutdown, 10, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelMaintenance, 10, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelAvailable, 13, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.ComboBox3, 3, 19)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel5, 13, 16)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel6, 14, 18)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelBalance, 11, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelEntertainment, 11, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelDuration, 11, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelTotal, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelAvailable, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelInUse, 9, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelMaintenance, 12, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 21
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.620689!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545455!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.194357!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.97179!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.291536!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.410658!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.291536!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.567398!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.605016!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.410658!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.605016!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.53918!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.583072!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.253919!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.956113!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.351097!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.023548!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.006279!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.354788!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.180534!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.761905!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1084, 638)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel1, 6)
        Me.Panel1.Controls.Add(Me.DataGridEntertainment)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(53, 101)
        Me.Panel1.Name = "Panel1"
        Me.TableLayoutPanel1.SetRowSpan(Me.Panel1, 15)
        Me.Panel1.Size = New System.Drawing.Size(587, 448)
        Me.Panel1.TabIndex = 4
        '
        'DataGridEntertainment
        '
        Me.DataGridEntertainment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridEntertainment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridEntertainment.Location = New System.Drawing.Point(0, 0)
        Me.DataGridEntertainment.Name = "DataGridEntertainment"
        Me.DataGridEntertainment.RowHeadersWidth = 51
        Me.DataGridEntertainment.RowTemplate.Height = 24
        Me.DataGridEntertainment.Size = New System.Drawing.Size(587, 448)
        Me.DataGridEntertainment.TabIndex = 0
        '
        'ComboBoxUsername
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.ComboBoxUsername, 4)
        Me.ComboBoxUsername.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxUsername.Font = New System.Drawing.Font("Times New Roman", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxUsername.FormattingEnabled = True
        Me.ComboBoxUsername.Location = New System.Drawing.Point(794, 172)
        Me.ComboBoxUsername.Name = "ComboBoxUsername"
        Me.ComboBoxUsername.Size = New System.Drawing.Size(113, 34)
        Me.ComboBoxUsername.TabIndex = 5
        '
        'PanelShutdown
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.PanelShutdown, 6)
        Me.PanelShutdown.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelShutdown.Location = New System.Drawing.Point(705, 369)
        Me.PanelShutdown.Name = "PanelShutdown"
        Me.PanelShutdown.Size = New System.Drawing.Size(293, 36)
        Me.PanelShutdown.TabIndex = 6
        '
        'PanelMaintenance
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.PanelMaintenance, 3)
        Me.PanelMaintenance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMaintenance.Location = New System.Drawing.Point(705, 419)
        Me.PanelMaintenance.Name = "PanelMaintenance"
        Me.PanelMaintenance.Size = New System.Drawing.Size(147, 32)
        Me.PanelMaintenance.TabIndex = 7
        '
        'PanelAvailable
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.PanelAvailable, 3)
        Me.PanelAvailable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelAvailable.Location = New System.Drawing.Point(858, 419)
        Me.PanelAvailable.Name = "PanelAvailable"
        Me.PanelAvailable.Size = New System.Drawing.Size(140, 32)
        Me.PanelAvailable.TabIndex = 8
        '
        'ComboBox3
        '
        Me.ComboBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBox3.Font = New System.Drawing.Font("Times New Roman", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Items.AddRange(New Object() {"In Use", "In Maintenance", "Available"})
        Me.ComboBox3.Location = New System.Drawing.Point(227, 567)
        Me.ComboBox3.Margin = New System.Windows.Forms.Padding(0)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(160, 39)
        Me.ComboBox3.TabIndex = 10
        '
        'Panel5
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel5, 3)
        Me.Panel5.Controls.Add(Me.ComboBoxEntertainment)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(858, 472)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(140, 26)
        Me.Panel5.TabIndex = 11
        '
        'ComboBoxEntertainment
        '
        Me.ComboBoxEntertainment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxEntertainment.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxEntertainment.FormattingEnabled = True
        Me.ComboBoxEntertainment.Location = New System.Drawing.Point(0, 0)
        Me.ComboBoxEntertainment.Name = "ComboBoxEntertainment"
        Me.ComboBoxEntertainment.Size = New System.Drawing.Size(140, 30)
        Me.ComboBoxEntertainment.TabIndex = 0
        '
        'Panel6
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel6, 2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(890, 555)
        Me.Panel6.Name = "Panel6"
        Me.TableLayoutPanel1.SetRowSpan(Me.Panel6, 2)
        Me.Panel6.Size = New System.Drawing.Size(108, 42)
        Me.Panel6.TabIndex = 12
        '
        'LabelBalance
        '
        Me.LabelBalance.AutoSize = True
        Me.LabelBalance.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.SetColumnSpan(Me.LabelBalance, 4)
        Me.LabelBalance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelBalance.Location = New System.Drawing.Point(794, 199)
        Me.LabelBalance.Name = "LabelBalance"
        Me.LabelBalance.Size = New System.Drawing.Size(113, 21)
        Me.LabelBalance.TabIndex = 17
        Me.LabelBalance.Text = "Label1"
        '
        'LabelEntertainment
        '
        Me.LabelEntertainment.AutoSize = True
        Me.LabelEntertainment.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.SetColumnSpan(Me.LabelEntertainment, 4)
        Me.LabelEntertainment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelEntertainment.Location = New System.Drawing.Point(794, 230)
        Me.LabelEntertainment.Name = "LabelEntertainment"
        Me.LabelEntertainment.Size = New System.Drawing.Size(113, 23)
        Me.LabelEntertainment.TabIndex = 18
        Me.LabelEntertainment.Text = "Label2"
        '
        'LabelDuration
        '
        Me.LabelDuration.AutoSize = True
        Me.LabelDuration.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.SetColumnSpan(Me.LabelDuration, 4)
        Me.LabelDuration.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelDuration.Location = New System.Drawing.Point(794, 262)
        Me.LabelDuration.Name = "LabelDuration"
        Me.LabelDuration.Size = New System.Drawing.Size(113, 23)
        Me.LabelDuration.TabIndex = 19
        Me.LabelDuration.Text = "Label3"
        '
        'LabelTotal
        '
        Me.LabelTotal.AutoSize = True
        Me.LabelTotal.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelTotal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelTotal.Font = New System.Drawing.Font("Stencil", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTotal.ForeColor = System.Drawing.Color.Lime
        Me.LabelTotal.Location = New System.Drawing.Point(390, 55)
        Me.LabelTotal.Name = "LabelTotal"
        Me.LabelTotal.Size = New System.Drawing.Size(27, 29)
        Me.LabelTotal.TabIndex = 20
        '
        'LabelAvailable
        '
        Me.LabelAvailable.AutoSize = True
        Me.LabelAvailable.BackColor = System.Drawing.Color.Black
        Me.LabelAvailable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelAvailable.Font = New System.Drawing.Font("Stencil", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelAvailable.ForeColor = System.Drawing.Color.Lime
        Me.LabelAvailable.Location = New System.Drawing.Point(527, 55)
        Me.LabelAvailable.Name = "LabelAvailable"
        Me.LabelAvailable.Size = New System.Drawing.Size(27, 29)
        Me.LabelAvailable.TabIndex = 21
        '
        'LabelInUse
        '
        Me.LabelInUse.AutoSize = True
        Me.LabelInUse.BackColor = System.Drawing.Color.Black
        Me.LabelInUse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelInUse.Font = New System.Drawing.Font("Stencil", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelInUse.ForeColor = System.Drawing.Color.Lime
        Me.LabelInUse.Location = New System.Drawing.Point(666, 55)
        Me.LabelInUse.Name = "LabelInUse"
        Me.LabelInUse.Size = New System.Drawing.Size(33, 29)
        Me.LabelInUse.TabIndex = 22
        '
        'LabelMaintenance
        '
        Me.LabelMaintenance.AutoSize = True
        Me.LabelMaintenance.BackColor = System.Drawing.Color.Black
        Me.LabelMaintenance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelMaintenance.Font = New System.Drawing.Font("Stencil", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMaintenance.ForeColor = System.Drawing.Color.Lime
        Me.LabelMaintenance.Location = New System.Drawing.Point(820, 55)
        Me.LabelMaintenance.Name = "LabelMaintenance"
        Me.LabelMaintenance.Size = New System.Drawing.Size(32, 29)
        Me.LabelMaintenance.TabIndex = 23
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'LiveDurationTimer
        '
        '
        'EntertainmentManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1084, 638)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.DoubleBuffered = True
        Me.Name = "EntertainmentManagement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "EntertainmentManagement"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridEntertainment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DataGridEntertainment As DataGridView
    Friend WithEvents ComboBoxUsername As ComboBox
    Friend WithEvents PanelShutdown As Panel
    Friend WithEvents PanelMaintenance As Panel
    Friend WithEvents PanelAvailable As Panel
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents ComboBoxEntertainment As ComboBox
    Friend WithEvents Panel6 As Panel
    Friend WithEvents LabelBalance As Label
    Friend WithEvents LabelEntertainment As Label
    Friend WithEvents LabelDuration As Label
    Friend WithEvents LabelTotal As Label
    Friend WithEvents LabelAvailable As Label
    Friend WithEvents LabelInUse As Label
    Friend WithEvents LabelMaintenance As Label
    Friend WithEvents LiveDurationTimer As Timer
End Class
