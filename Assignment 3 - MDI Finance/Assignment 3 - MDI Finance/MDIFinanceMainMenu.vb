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

        LayoutMdi(MdiLayout.TileVertical)
    End Sub

    '== Click events
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub PresentValueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PresentValueToolStripMenuItem.Click
        Dim presentValue As New CarLoanForm
        presentValue.MdiParent = Me
        presentValue.myType = LoanFormType.PresentValue
        presentValue.Show()
    End Sub

    Private Sub FutureValueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FutureValueToolStripMenuItem.Click
        Dim futureValue As New CarLoanForm
        futureValue.MdiParent = Me
        futureValue.myType = LoanFormType.FutureValue
        futureValue.Show()
    End Sub

    Private Sub LoanPaymentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoanPaymentToolStripMenuItem.Click
        '== Doesn't need set, is default.
        Dim loanPayment As New CarLoanForm
        loanPayment.MdiParent = Me
        loanPayment.Show()
    End Sub

    Private Sub SavingsPaymentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SavingsPaymentToolStripMenuItem.Click
        Dim savingsPayment As New CarLoanForm
        savingsPayment.MdiParent = Me
        savingsPayment.myType = LoanFormType.SavingsPayment
        savingsPayment.Show()
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CascadeToolStripMenuItem.Click
        LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        For Each element In MdiChildren
            element.Close()
        Next
    End Sub

    '== Closing handler
    Private Sub CarLoanForm_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        If MessageBox.Show("Are you sure you wish to close?", "Exiting", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

End Class