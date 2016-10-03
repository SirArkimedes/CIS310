'=========================================='
'= CIS 310, Andrew Robinson, Assignment 3 ='
'=========================================='

Public Class FinUtility

    '== Privates
    Private Rate = -1
    Private Term = -1
    Private Amount = -1

    '== Properties
    WriteOnly Property AnnualRate()
        Set(value)
            If TestValue(value, 0, 0.0875) Then
                Rate = value
            ElseIf TestValue(value, 0, 6.0) Then
                Rate = value / 100
            ElseIf InStr(value, "%") > 0 Then
                value = value.ToString().Replace("%", "") / 100
                If TestValue(value, 0, 0.0875) Then
                    Rate = value
                Else
                    Rate = -1
                End If
            Else
                Rate = -1
            End If
        End Set
    End Property

    WriteOnly Property TermInMonths()
        Set(value)
            If TestValue(value, 24, 120) Then
                Term = value
            Else
                Term = -1
            End If
        End Set
    End Property

    WriteOnly Property LoanAmount()
        Set(value)
            If TestValue(value, 1000, 200000) Then
                Amount = value
            Else
                Amount = -1
            End If
        End Set
    End Property

    ReadOnly Property MonthlyPayment()
        Get
            '== Are any of the values unset?
            If Rate = -1 Then
                Throw New InvalidOperationException("Rate has not been set.")
            ElseIf Term = -1 Then
                Throw New InvalidOperationException("Term has not been set.")
            ElseIf Amount = -1 Then
                Throw New InvalidOperationException("Amount has not been set.")
            Else
                Return Pmt(Rate / 12, Term, -Amount)
            End If
        End Get
    End Property

    '== Helper methods
    Private Function TestValue(value As Object, startRange As Double, endRange As Double) As Boolean
        Dim isValueOkay = False

        If IsNumeric(value) Then
            If value >= startRange AndAlso value <= endRange Then
                isValueOkay = True
            End If
        End If

        Return isValueOkay
    End Function

End Class
