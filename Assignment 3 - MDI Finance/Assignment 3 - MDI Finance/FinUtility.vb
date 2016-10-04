'=========================================='
'= CIS 310, Andrew Robinson, Assignment 3 ='
'=========================================='

Public Class FinUtility

    '== Privates
    Private Rate = -1
    Private Term = -1
    Private Am = -1
    Private Pay = -1

    '== Set Properties
    WriteOnly Property AnnualRate()
        Set(value)
            value = value.ToString().Replace("%", "")
            If TestValue(value, 0, 0.33) Then
                Rate = value
            Else
                '== Check for between 0% and 33%
                If TestValue(value, 0, 33) Then
                    Rate = value / 100
                Else
                    Rate = -1
                End If
            End If
        End Set
    End Property

    WriteOnly Property TermInMonths()
        Set(value)
            If TestValue(value, 1, 600) Then
                Term = value
            Else
                Term = -1
            End If
        End Set
    End Property

    WriteOnly Property Amount()
        Set(value)
            If TestValue(value, 1000, 10000000) Then
                Am = value
            Else
                Am = -1
            End If
        End Set
    End Property

    WriteOnly Property Payment()
        Set(value)
            If TestValue(value, 1, 10000) Then
                Pay = value
            Else
                Pay = -1
            End If
        End Set
    End Property

    '== Result Properties
    ReadOnly Property PresentValue()
        Get
            If Rate = -1 Then
                Throw New InvalidOperationException("Rate has not been set.")
            ElseIf Term = -1 Then
                Throw New InvalidOperationException("Term has not been set.")
            ElseIf Pay = -1 Then
                Throw New InvalidOperationException("Payment has not been set.")
            Else
                Return PV(Rate / 12, Term, -Pay)
            End If
        End Get
    End Property

    ReadOnly Property FutureValue()
        Get
            If Rate = -1 Then
                Throw New InvalidOperationException("Rate has not been set.")
            ElseIf Term = -1 Then
                Throw New InvalidOperationException("Term has not been set.")
            ElseIf Pay = -1 Then
                Throw New InvalidOperationException("Payment has not been set.")
            Else
                Return FV(Rate / 12, Term, -Pay)
            End If
        End Get
    End Property

    ReadOnly Property MonthlyPayment()
        Get
            If Rate = -1 Then
                Throw New InvalidOperationException("Rate has not been set.")
            ElseIf Term = -1 Then
                Throw New InvalidOperationException("Term has not been set.")
            ElseIf Am = -1 Then
                Throw New InvalidOperationException("Amount has not been set.")
            Else
                Return Pmt(Rate / 12, Term, -Am)
            End If
        End Get
    End Property

    ReadOnly Property SavingsPayment()
        Get
            If Rate = -1 Then
                Throw New InvalidOperationException("Rate has not been set.")
            ElseIf Term = -1 Then
                Throw New InvalidOperationException("Term has not been set.")
            ElseIf Am = -1 Then
                Throw New InvalidOperationException("Amount has not been set.")
            Else
                Return Pmt(Rate / 12, Term, 0, -Am)
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
