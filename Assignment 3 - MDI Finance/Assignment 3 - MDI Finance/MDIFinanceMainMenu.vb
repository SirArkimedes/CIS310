'=========================================='
'= CIS 310, Andrew Robinson, Assignment 3 ='
'=========================================='

Public Class MDIFinanceMainMenu

    '== Wake Events
    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        Dim presentValue As New CarLoanForm
        presentValue.MdiParent = Me
        presentValue.myType = LoanFormType.PresentValue
        presentValue.Show()

        Dim futureValue As New CarLoanForm
        futureValue.MdiParent = Me
        futureValue.myType = LoanFormType.FutureValue
        futureValue.Show()

        '== Doesn't need set, is default.
        Dim loanPayment As New CarLoanForm
        loanPayment.MdiParent = Me
        loanPayment.Show()

        Dim savingsPayment As New CarLoanForm
        savingsPayment.MdiParent = Me
        savingsPayment.myType = LoanFormType.SavingsPayment
        savingsPayment.Show()

        LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    '== Closing handler
    Private Sub CarLoanForm_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        If MessageBox.Show("Are you sure you wish to close?", "Exiting", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

End Class