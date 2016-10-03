'=========================================='
'= CIS 310, Andrew Robinson, Assignment 3 ='
'=========================================='

Public Class MDIFinanceMainMenu

    '== Wake Events
    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        Dim presentValue As New CarLoanForm
        presentValue.MdiParent = Me
        presentValue.Show()
    End Sub

    '== Closing handler
    Private Sub CarLoanForm_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        If MessageBox.Show("Are you sure you wish to close?", "Exiting", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

End Class