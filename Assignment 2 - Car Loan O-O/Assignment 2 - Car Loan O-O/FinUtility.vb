'=========================================='
'= CIS 310, Andrew Robinson, Assignment 2 ='
'=========================================='

Public Class FinUtility

    '== Privates
    Private Rate = -1
    Private Term = -1
    Private Amount = -1

    '== Properties
    WriteOnly Property AnnualRate()
        Set(value)
            If TestValue(value, 0, 0.0875, True) Then
                Rate = value
            Else
                '== Process some sort of error.
            End If
        End Set
    End Property

    WriteOnly Property TermInMonths()
        Set(value)
            If TestValue(value, 24, 120, False) Then
                Term = value
            Else
                '== Process some sort of error.
            End If
        End Set
    End Property

    WriteOnly Property LoanAmount()
        Set(value)
            If TestValue(value, 1000, 200000, False) Then
                Amount = value
            Else
                '== Process some sort of error.
            End If
        End Set
    End Property

    ReadOnly Property MonthlyPayment()
        Get
            Return Pmt(Rate / 12, Term, -Amount)
        End Get
    End Property

    '== Helper methods
    Private Function TestValue(value As Object, startRange As Double, endRange As Double, checkPercent As Boolean) As Boolean
        Dim job = False

        If IsNumeric(value) Then
            If value >= startRange AndAlso value <= endRange Then
                job = True
            End If
        ElseIf checkPercent Then
            '== Isn't numeric, but could have %.
            If InStr(value, "%") > 0 Then
                value = value.ToString().Replace("%", "")
                If value >= startRange AndAlso value <= endRange Then
                    job = True
                End If
            End If
        End If

        Return job
    End Function

End Class
