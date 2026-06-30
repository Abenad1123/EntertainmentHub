<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EntertainmentConfiguration
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EntertainmentConfiguration))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DataGridInUse = New System.Windows.Forms.DataGridView()
        Me.DataGridAvailable = New System.Windows.Forms.DataGridView()
        Me.DataGridInMaintenance = New System.Windows.Forms.DataGridView()
        Me.Panel_ToRent = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel_ManualOff = New System.Windows.Forms.Panel()
        Me.Panel_ChangeStatus = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridInUse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridAvailable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridInMaintenance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 14
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.416667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.9166667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.83333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.166667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.333333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.75!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.333333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.583333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.166667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.666667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.83333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.833333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.833333!))
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridInUse, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridAvailable, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridInMaintenance, 11, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel_ToRent, 5, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel_ManualOff, 8, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel_ChangeStatus, 1, 5)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 8
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.54481!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.88478!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.254623!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.405406!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.974395!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.401138!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.271693!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.12091!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1200, 703)
        Me.TableLayoutPanel1.TabIndex = 17
        '
        'DataGridInUse
        '
        Me.DataGridInUse.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DataGridInUse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableLayoutPanel1.SetColumnSpan(Me.DataGridInUse, 3)
        Me.DataGridInUse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridInUse.GridColor = System.Drawing.SystemColors.GrayText
        Me.DataGridInUse.Location = New System.Drawing.Point(91, 281)
        Me.DataGridInUse.Name = "DataGridInUse"
        Me.DataGridInUse.RowHeadersWidth = 51
        Me.DataGridInUse.RowTemplate.Height = 24
        Me.DataGridInUse.Size = New System.Drawing.Size(273, 183)
        Me.DataGridInUse.TabIndex = 0
        '
        'DataGridAvailable
        '
        Me.DataGridAvailable.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DataGridAvailable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableLayoutPanel1.SetColumnSpan(Me.DataGridAvailable, 3)
        Me.DataGridAvailable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridAvailable.Location = New System.Drawing.Point(463, 281)
        Me.DataGridAvailable.Name = "DataGridAvailable"
        Me.DataGridAvailable.RowHeadersWidth = 51
        Me.DataGridAvailable.RowTemplate.Height = 24
        Me.DataGridAvailable.Size = New System.Drawing.Size(272, 183)
        Me.DataGridAvailable.TabIndex = 1
        '
        'DataGridInMaintenance
        '
        Me.DataGridInMaintenance.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DataGridInMaintenance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableLayoutPanel1.SetColumnSpan(Me.DataGridInMaintenance, 2)
        Me.DataGridInMaintenance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridInMaintenance.Location = New System.Drawing.Point(835, 281)
        Me.DataGridInMaintenance.Name = "DataGridInMaintenance"
        Me.DataGridInMaintenance.RowHeadersWidth = 51
        Me.DataGridInMaintenance.RowTemplate.Height = 24
        Me.DataGridInMaintenance.Size = New System.Drawing.Size(277, 183)
        Me.DataGridInMaintenance.TabIndex = 2
        '
        'Panel_ToRent
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel_ToRent, 2)
        Me.Panel_ToRent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_ToRent.Location = New System.Drawing.Point(370, 601)
        Me.Panel_ToRent.Name = "Panel_ToRent"
        Me.Panel_ToRent.Size = New System.Drawing.Size(163, 39)
        Me.Panel_ToRent.TabIndex = 4
        '
        'Panel2
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel2, 4)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(256, 521)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(277, 32)
        Me.Panel2.TabIndex = 5
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(277, 53)
        Me.TextBox1.TabIndex = 0
        '
        'Panel_ManualOff
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel_ManualOff, 2)
        Me.Panel_ManualOff.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_ManualOff.Location = New System.Drawing.Point(570, 601)
        Me.Panel_ManualOff.Name = "Panel_ManualOff"
        Me.Panel_ManualOff.Size = New System.Drawing.Size(203, 39)
        Me.Panel_ManualOff.TabIndex = 6
        '
        'Panel_ChangeStatus
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel_ChangeStatus, 3)
        Me.Panel_ChangeStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_ChangeStatus.Location = New System.Drawing.Point(80, 601)
        Me.Panel_ChangeStatus.Name = "Panel_ChangeStatus"
        Me.Panel_ChangeStatus.Size = New System.Drawing.Size(256, 39)
        Me.Panel_ChangeStatus.TabIndex = 7
        '
        'EntertainmentConfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1200, 703)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "EntertainmentConfiguration"
        Me.Text = "Admin"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DataGridInUse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridAvailable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridInMaintenance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents DataGridInUse As DataGridView
    Friend WithEvents DataGridAvailable As DataGridView
    Friend WithEvents DataGridInMaintenance As DataGridView
    Friend WithEvents Panel_ToRent As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Panel_ManualOff As Panel
    Friend WithEvents Panel_ChangeStatus As Panel
End Class
