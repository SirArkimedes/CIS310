Public Class CarLoanForm

    '== Wake Events
    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        Me.ResetForm()
    End Sub

    '== Click Events
    Private Sub CalculateButton_Click(sender As Object, e As EventArgs) Handles CalculateButton.Click

        If IsNumeric(LoanAmountTextBox.Text) Then
            Dim LoanAmount As Double = Convert.ToDouble(LoanAmountTextBox.Text)
            If LoanAmount >= 1000.0 And LoanAmount <= 200000 Then
                '== Calculate.
            Else
                '== Outside interval bounds.
                StatusStripLabel.Text = "Out of bounds. Loan amount needs to be within $10,000 and $200,000."
                StatusTimer.Enabled = True
            End If
        Else
            '== Invalid input.
            StatusStripLabel.Text = "Invalid input. Loan amount needs to be numeric."
            StatusTimer.Enabled = True
        End If

    End Sub

    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        Me.ResetForm()
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Close()
    End Sub

    '== Timer tick
    Private Sub TimerTicked(sender As Object, e As EventArgs) Handles StatusTimer.Tick
        StatusStripLabel.Text = "CIS 310 Project 1: Car Loan Calculator"
    End Sub

    '== Helper methods
    Private Sub ResetForm()
        LoanAmountTextBox.Text = "10,000"
        InterestRateComboBox.SelectedIndex = 24
        TermComboBox.SelectedIndex = 2
        MonthlyPaymentTextBox.Text = ""
    End Sub

    '== Closing handler
    Private Sub CarLoanForm_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        If MessageBox.Show("Are you sure you wish to close?", "Exiting", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

End Class
