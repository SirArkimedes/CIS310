'=========================================='
'= CIS 310, Andrew Robinson, Assignment 6 ='
'=========================================='

Public Enum CustomerChangeType
    Deleted
    Edited
    Created
End Enum

Public Class Customer

    Public ID = ""
    Public CompanyName = ""
    Public ContactName = ""
    Public ContactTitle = ""
    Public Address = ""
    Public City = ""
    Public Region = ""
    Public PostalCode = ""
    Public Country = ""
    Public Phone = ""
    Public Fax = ""

    Public orderIds() As String

    Public ChangeType As CustomerChangeType

    Public Sub New()
    End Sub

End Class
