'=========================================='
'= CIS 310, Andrew Robinson, Assignment 3 ='
'=========================================='

Public Enum LoanFormType
    PresentValue
    FutureValue
    LoanPayment
    SavingsPayment
End Enum

Public Class CarLoanForm

    '== Publics
    Public myType As LoanFormType = LoanFormType.LoanPayment

    '== Privatea
    Private financialUtility As New FinUtility

    '== Wake Events
    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load

        For i = 0 To 0.3325 Step 0.0025
            InterestRateComboBox.Items.Add(FormatPercent(i))
        Next

        For i = 6 To 600 Step 6
            TermComboBox.Items.Add(i)
        Next

        If myType = LoanFormType.PresentValue Then
            Text = "Loan Amount, Given Montly Payment"
            TitleLabel.Text = "How much can I borrow?"
            LoanAmountLabel.Text = "Monthly Payment:"
            ResultLabel.Text = "Loan Available:"
        ElseIf myType = LoanFormType.FutureValue Then
            Text = "Total Savings, Given Monthly Savings"
            TitleLabel.Text = "How much will I acumulate?"
            LoanAmountLabel.Text = "Monthly Savings:"
            ResultLabel.Text = "Accumulated Value:"
        ElseIf myType = LoanFormType.SavingsPayment Then
            Text = "Monthly Savings, Given Savings Target"
            TitleLabel.Text = "How much should I save each month?"
            LoanAmountLabel.Text = "Target Savings:"
            ResultLabel.Text = "Monthly Savings:"
        End If

        ResetForm()
    End Sub

    '== Click Events
    Private Sub CalculateButton_Click(sender As Object, e As EventArgs) Handles CalculateButton.Click

        If IsNumeric(LoanAmountTextBox.Text) Then
            Dim Amount = LoanAmountTextBox.Text
            If myType = LoanFormType.SavingsPayment Or myType = LoanFormType.LoanPayment Then
                If Amount >= 1000.0 And Amount <= 10000000 Then
                    '== Calculate.
                    Dim RateString = Replace(InterestRateComboBox.SelectedItem.ToString(), "%", "")
                    Dim result = -1

                    financialUtility.AnnualRate = RateString / 100
                    financialUtility.TermInMonths = TermComboBox.SelectedItem.ToString()
                    financialUtility.Amount = Amount

                    If myType = LoanFormType.SavingsPayment Then
                        Try
                            result = financialUtility.SavingsPayment
                        Catch exception As Exception
                            ErrorInComputation(exception.Message)
                        End Try
                    Else
                        Try
                            result = financialUtility.MonthlyPayment
                        Catch exception As Exception
                            ErrorInComputation(exception.Message)
                        End Try
                    End If

                    MonthlyPaymentTextBox.Text = FormatCurrency(result, , TriState.False)
                Else
                    '== Outside interval bounds.
                    ErrorInComputation("Out of bounds. Loan amount needs to be within $1,000 and $10,000,000.")
                End If
            Else
                If Amount >= 1 And Amount <= 10000 Then
                    '== Calculate.
                    Dim RateString = Replace(InterestRateComboBox.SelectedItem.ToString(), "%", "")
                    Dim result = -1

                    financialUtility.AnnualRate = RateString / 100
                    financialUtility.TermInMonths = TermComboBox.SelectedItem.ToString()
                    financialUtility.Payment = Amount

                    If myType = LoanFormType.PresentValue Then
                        Try
                            result = financialUtility.PresentValue
                        Catch exception As Exception
                            ErrorInComputation(Exception.Message)
                        End Try
                    Else
                        Try
                            result = financialUtility.FutureValue
                        Catch exception As Exception
                            ErrorInComputation(exception.Message)
                        End Try
                    End If

                    MonthlyPaymentTextBox.Text = FormatCurrency(result, , TriState.False)
                Else
                    '== Outside interval bounds.
                    ErrorInComputation("Out of bounds. Loan amount needs to be within $1 and $10,000.")
                End If
            End If
        ElseIf LoanAmountTextBox.Text = "" Then
            '== Empty input.
            ErrorInComputation("Empty input. Loan amount needs to be within $1,000 and $200,000.")
        Else
            '== Invalid input.
            ErrorInComputation("Invalid input. Loan amount needs to be within $1,000 and $200,000.")
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
        StatusStrip.BackColor = SystemColors.Control
        StatusStripLabel.ForeColor = SystemColors.ControlText
    End Sub

    '== Helper methods
    Private Sub ResetForm()

        If myType = LoanFormType.PresentValue Then
            LoanAmountTextBox.Text = "304.22"
        ElseIf myType = LoanFormType.FutureValue Then
            LoanAmountTextBox.Text = "254.22"
        Else
            LoanAmountTextBox.Text = "10,000"
        End If

        InterestRateComboBox.SelectedIndex = 24
        TermComboBox.SelectedIndex = 5
        MonthlyPaymentTextBox.Text = ""
    End Sub

    Private Sub ErrorInComputation(text As String)
        MonthlyPaymentTextBox.Text = ""
        StatusStripLabel.Text = text
        StatusStripLabel.ForeColor = Color.White
        StatusStrip.BackColor = Color.Red
        StatusTimer.Enabled = True
    End Sub

End Class
