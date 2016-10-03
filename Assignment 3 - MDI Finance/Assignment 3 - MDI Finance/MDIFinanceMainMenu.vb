'=========================================='
'= CIS 310, Andrew Robinson, Assignment 3 ='
'=========================================='

Public Class MDIFinanceMainMenu

    '== Closing handler
    Private Sub CarLoanForm_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        If MessageBox.Show("Are you sure you wish to close?", "Exiting", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

End Class