<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EntertainmentCustomer
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
        Me.LiveDurationTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.FlowEntertainmentCards = New System.Windows.Forms.FlowLayoutPanel()
        Me.DataGridEntertainment = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LabelDuration = New System.Windows.Forms.Label()
        Me.LabelEntertainment = New System.Windows.Forms.Label()
        Me.LabelBalance = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridEntertainment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'LiveDurationTimer
        '
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridEntertainment, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(800, 450)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.FlowEntertainmentCards)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(394, 219)
        Me.Panel1.TabIndex = 0
        '
        'FlowEntertainmentCards
        '
        Me.FlowEntertainmentCards.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowEntertainmentCards.Location = New System.Drawing.Point(0, 0)
        Me.FlowEntertainmentCards.Name = "FlowEntertainmentCards"
        Me.FlowEntertainmentCards.Size = New System.Drawing.Size(394, 219)
        Me.FlowEntertainmentCards.TabIndex = 0
        '
        'DataGridEntertainment
        '
        Me.DataGridEntertainment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridEntertainment.Location = New System.Drawing.Point(403, 228)
        Me.DataGridEntertainment.Name = "DataGridEntertainment"
        Me.DataGridEntertainment.RowHeadersWidth = 51
        Me.DataGridEntertainment.RowTemplate.Height = 24
        Me.DataGridEntertainment.Size = New System.Drawing.Size(240, 150)
        Me.DataGridEntertainment.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.LabelBalance)
        Me.Panel2.Controls.Add(Me.LabelEntertainment)
        Me.Panel2.Controls.Add(Me.LabelDuration)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(403, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(394, 219)
        Me.Panel2.TabIndex = 2
        '
        'LabelDuration
        '
        Me.LabelDuration.AutoSize = True
        Me.LabelDuration.Location = New System.Drawing.Point(0, 0)
        Me.LabelDuration.Name = "LabelDuration"
        Me.LabelDuration.Size = New System.Drawing.Size(48, 16)
        Me.LabelDuration.TabIndex = 0
        Me.LabelDuration.Text = "Label1"
        '
        'LabelEntertainment
        '
        Me.LabelEntertainment.AutoSize = True
        Me.LabelEntertainment.Location = New System.Drawing.Point(46, 46)
        Me.LabelEntertainment.Name = "LabelEntertainment"
        Me.LabelEntertainment.Size = New System.Drawing.Size(48, 16)
        Me.LabelEntertainment.TabIndex = 1
        Me.LabelEntertainment.Text = "Label2"
        '
        'LabelBalance
        '
        Me.LabelBalance.AutoSize = True
        Me.LabelBalance.Location = New System.Drawing.Point(140, 142)
        Me.LabelBalance.Name = "LabelBalance"
        Me.LabelBalance.Size = New System.Drawing.Size(48, 16)
        Me.LabelBalance.TabIndex = 2
        Me.LabelBalance.Text = "Label3"
        '
        'EntertainmentCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "EntertainmentCustomer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EntertainmentCustomer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridEntertainment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LiveDurationTimer As Timer
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents FlowEntertainmentCards As FlowLayoutPanel
    Friend WithEvents DataGridEntertainment As DataGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents LabelBalance As Label
    Friend WithEvents LabelEntertainment As Label
    Friend WithEvents LabelDuration As Label
End Class
