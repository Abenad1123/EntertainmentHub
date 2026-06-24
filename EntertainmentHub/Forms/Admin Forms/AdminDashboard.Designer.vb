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
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(54, 111)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(707, 447)
        Me.DataGridView1.TabIndex = 13
        '
        'btnOpenManageUser
        '
        Me.btnOpenManageUser.Location = New System.Drawing.Point(825, 434)
        Me.btnOpenManageUser.Name = "btnOpenManageUser"
        Me.btnOpenManageUser.Size = New System.Drawing.Size(329, 39)
        Me.btnOpenManageUser.TabIndex = 22
        Me.btnOpenManageUser.Text = "MANAGE USERS"
        Me.btnOpenManageUser.UseVisualStyleBackColor = True
        '
        'btnOpenManageEmployee
        '
        Me.btnOpenManageEmployee.Location = New System.Drawing.Point(825, 368)
        Me.btnOpenManageEmployee.Name = "btnOpenManageEmployee"
        Me.btnOpenManageEmployee.Size = New System.Drawing.Size(329, 39)
        Me.btnOpenManageEmployee.TabIndex = 23
        Me.btnOpenManageEmployee.Text = "MANAGE EMPLOYEES"
        Me.btnOpenManageEmployee.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(825, 505)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(329, 53)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "CONFIGURE ENTERTAINMENT"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'AdminDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 703)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnOpenManageEmployee)
        Me.Controls.Add(Me.btnOpenManageUser)
        Me.Controls.Add(Me.DataGridView1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "AdminDashboard"
        Me.Text = "Admin"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnOpenManageUser As Button
    Friend WithEvents btnOpenManageEmployee As Button
    Friend WithEvents Button1 As Button
End Class
