<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CarLoanForm
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
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.LoanAmountLabel = New System.Windows.Forms.Label()
        Me.LoanAmountTextBox = New System.Windows.Forms.TextBox()
        Me.InterestRateComboBox = New System.Windows.Forms.ComboBox()
        Me.InterestRateLabel = New System.Windows.Forms.Label()
        Me.TermLabel = New System.Windows.Forms.Label()
        Me.TermComboBox = New System.Windows.Forms.ComboBox()
        Me.CalculateButton = New System.Windows.Forms.Button()
        Me.ResetButton = New System.Windows.Forms.Button()
        Me.ExitButton = New System.Windows.Forms.Button()
        Me.MonthlyPaymentTextBox = New System.Windows.Forms.TextBox()
        Me.PaymentLabel = New System.Windows.Forms.Label()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleLabel
        '
        Me.TitleLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(14, 9)
        Me.TitleLabel.MaximumSize = New System.Drawing.Size(430, 50)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(401, 48)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "What payments will retire a given loan at a given intrest reate over a given term" &
    "?"
        Me.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LoanAmountLabel
        '
        Me.LoanAmountLabel.AutoSize = True
        Me.LoanAmountLabel.Location = New System.Drawing.Point(93, 83)
        Me.LoanAmountLabel.Name = "LoanAmountLabel"
        Me.LoanAmountLabel.Size = New System.Drawing.Size(101, 13)
        Me.LoanAmountLabel.TabIndex = 1
        Me.LoanAmountLabel.Text = "Enter Loan Amount:"
        '
        'LoanAmountTextBox
        '
        Me.LoanAmountTextBox.Location = New System.Drawing.Point(200, 80)
        Me.LoanAmountTextBox.Name = "LoanAmountTextBox"
        Me.LoanAmountTextBox.Size = New System.Drawing.Size(100, 20)
        Me.LoanAmountTextBox.TabIndex = 2
        '
        'InterestRateComboBox
        '
        Me.InterestRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.InterestRateComboBox.FormattingEnabled = True
        Me.InterestRateComboBox.Location = New System.Drawing.Point(200, 115)
        Me.InterestRateComboBox.Name = "InterestRateComboBox"
        Me.InterestRateComboBox.Size = New System.Drawing.Size(100, 21)
        Me.InterestRateComboBox.TabIndex = 3
        '
        'InterestRateLabel
        '
        Me.InterestRateLabel.AutoSize = True
        Me.InterestRateLabel.Location = New System.Drawing.Point(92, 118)
        Me.InterestRateLabel.Name = "InterestRateLabel"
        Me.InterestRateLabel.Size = New System.Drawing.Size(102, 13)
        Me.InterestRateLabel.TabIndex = 4
        Me.InterestRateLabel.Text = "Select Annual Rate:"
        '
        'TermLabel
        '
        Me.TermLabel.AutoSize = True
        Me.TermLabel.Location = New System.Drawing.Point(73, 154)
        Me.TermLabel.Name = "TermLabel"
        Me.TermLabel.Size = New System.Drawing.Size(121, 13)
        Me.TermLabel.TabIndex = 6
        Me.TermLabel.Text = "Select Term (in months):"
        '
        'TermComboBox
        '
        Me.TermComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TermComboBox.FormattingEnabled = True
        Me.TermComboBox.Location = New System.Drawing.Point(200, 151)
        Me.TermComboBox.Name = "TermComboBox"
        Me.TermComboBox.Size = New System.Drawing.Size(100, 21)
        Me.TermComboBox.TabIndex = 5
        '
        'CalculateButton
        '
        Me.CalculateButton.Location = New System.Drawing.Point(54, 212)
        Me.CalculateButton.Name = "CalculateButton"
        Me.CalculateButton.Size = New System.Drawing.Size(75, 23)
        Me.CalculateButton.TabIndex = 7
        Me.CalculateButton.Text = "Calculate"
        Me.CalculateButton.UseVisualStyleBackColor = True
        '
        'ResetButton
        '
        Me.ResetButton.Location = New System.Drawing.Point(186, 212)
        Me.ResetButton.Name = "ResetButton"
        Me.ResetButton.Size = New System.Drawing.Size(75, 23)
        Me.ResetButton.TabIndex = 8
        Me.ResetButton.Text = "Reset"
        Me.ResetButton.UseVisualStyleBackColor = True
        '
        'ExitButton
        '
        Me.ExitButton.Location = New System.Drawing.Point(309, 212)
        Me.ExitButton.Name = "ExitButton"
        Me.ExitButton.Size = New System.Drawing.Size(75, 23)
        Me.ExitButton.TabIndex = 9
        Me.ExitButton.Text = "Exit"
        Me.ExitButton.UseVisualStyleBackColor = True
        '
        'MonthlyPaymentTextBox
        '
        Me.MonthlyPaymentTextBox.Location = New System.Drawing.Point(200, 267)
        Me.MonthlyPaymentTextBox.Name = "MonthlyPaymentTextBox"
        Me.MonthlyPaymentTextBox.ReadOnly = True
        Me.MonthlyPaymentTextBox.Size = New System.Drawing.Size(100, 20)
        Me.MonthlyPaymentTextBox.TabIndex = 11
        Me.MonthlyPaymentTextBox.TabStop = False
        Me.MonthlyPaymentTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'PaymentLabel
        '
        Me.PaymentLabel.AutoSize = True
        Me.PaymentLabel.Location = New System.Drawing.Point(103, 270)
        Me.PaymentLabel.Name = "PaymentLabel"
        Me.PaymentLabel.Size = New System.Drawing.Size(91, 13)
        Me.PaymentLabel.TabIndex = 10
        Me.PaymentLabel.Text = "Monthly Payment:"
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 310)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(425, 22)
        Me.StatusStrip.TabIndex = 12
        Me.StatusStrip.Text = "?"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(204, 17)
        Me.ToolStripStatusLabel1.Text = "CIS 310 Project 1: Car Loan Calculator"
        '
        'CarLoanForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(425, 332)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MonthlyPaymentTextBox)
        Me.Controls.Add(Me.PaymentLabel)
        Me.Controls.Add(Me.ExitButton)
        Me.Controls.Add(Me.ResetButton)
        Me.Controls.Add(Me.CalculateButton)
        Me.Controls.Add(Me.TermLabel)
        Me.Controls.Add(Me.TermComboBox)
        Me.Controls.Add(Me.InterestRateLabel)
        Me.Controls.Add(Me.InterestRateComboBox)
        Me.Controls.Add(Me.LoanAmountTextBox)
        Me.Controls.Add(Me.LoanAmountLabel)
        Me.Controls.Add(Me.TitleLabel)
        Me.Name = "CarLoanForm"
        Me.ShowIcon = False
        Me.Text = "Car Loan Calculator - Andrew Robinson"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleLabel As Label
    Friend WithEvents LoanAmountLabel As Label
    Friend WithEvents LoanAmountTextBox As TextBox
    Friend WithEvents InterestRateComboBox As ComboBox
    Friend WithEvents InterestRateLabel As Label
    Friend WithEvents TermLabel As Label
    Friend WithEvents TermComboBox As ComboBox
    Friend WithEvents CalculateButton As Button
    Friend WithEvents ResetButton As Button
    Friend WithEvents ExitButton As Button
    Friend WithEvents MonthlyPaymentTextBox As TextBox
    Friend WithEvents PaymentLabel As Label
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
End Class
