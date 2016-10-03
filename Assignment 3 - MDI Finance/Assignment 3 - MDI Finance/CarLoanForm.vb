'=========================================='
'= CIS 310, Andrew Robinson, Assignment 3 ='
'=========================================='

Public Class CarLoanForm

    Private financialUtility As New FinUtility

    '== Wake Events
    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        ResetForm()
    End Sub

    '== Click Events
    Private Sub CalculateButton_Click(sender As Object, e As EventArgs) Handles CalculateButton.Click

        If IsNumeric(LoanAmountTextBox.Text) Then
            Dim LoanAmount As Double = Convert.ToDouble(LoanAmountTextBox.Text)
            If LoanAmount >= 1000.0 And LoanAmount <= 200000 Then
                '== Calculate.
                Dim RateString As String = Replace(InterestRateComboBox.SelectedItem.ToString(), "%", "")
                Dim Rate As Double = Convert.ToDouble(RateString) / 100
                Dim Months As Double = Convert.ToDouble(TermComboBox.SelectedItem.ToString())
                Dim MonthlyPayment As Double

                financialUtility.AnnualRate = Rate
                financialUtility.TermInMonths = Months
                financialUtility.LoanAmount = LoanAmount
                MonthlyPayment = financialUtility.MonthlyPayment

                '== Force these to 2 and false because we don't support multiple currency formats. $0.00
                MonthlyPaymentTextBox.Text = FormatCurrency(MonthlyPayment, 2, TriState.False, TriState.False, TriState.False)
            Else
                '== Outside interval bounds.
                ErrorInComputation("Out of bounds. Loan amount needs to be within $1,000 and $200,000.")
            End If
        ElseIf LoanAmountTextBox.Text = "" Then
            '== Empty input.
            ErrorInComputation("Empty input. Loan amount needs to be numeric.")
        Else
            '== Invalid input.
            ErrorInComputation("Invalid input. Loan amount needs to be numeric.")
        End If

    End Sub

    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        ResetForm()
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Close()
    End Sub

    '== Timer tick
    Private Sub TimerTicked(sender As Object, e As EventArgs) Handles StatusTimer.Tick
        StatusStripLabel.Text = "CIS 310 Project 2: Car Loan Calculator O-O"
    End Sub

    '== Helper methods
    Private Sub ResetForm()
        LoanAmountTextBox.Text = "10,000"
        InterestRateComboBox.SelectedIndex = 24
        TermComboBox.SelectedIndex = 2
        MonthlyPaymentTextBox.Text = ""
    End Sub

    Private Sub ErrorInComputation(text As String)
        MonthlyPaymentTextBox.Text = ""
        StatusStripLabel.Text = text
        StatusTimer.Enabled = True
    End Sub

    '== Closing handler
    Private Sub CarLoanForm_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        If MessageBox.Show("Are you sure you wish to close?", "Exiting", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

End Class
