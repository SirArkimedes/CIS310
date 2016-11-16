Public Class MasterUpdate

    '== Load
    Private Sub SynchedGrid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.EmployeesTableAdapter.Fill(Me.Ds.Employees)
            Me.ProductsTableAdapter.Fill(Me.Ds.Products)
            Me.ShippersTableAdapter.Fill(Me.Ds.Shippers)
            Me.Order_DetailsTableAdapter.Fill(Me.Ds.Order_Details)
            Me.OrdersTableAdapter.Fill(Me.Ds.Orders)
            Me.CustomersTableAdapter.Fill(Me.Ds.Customers)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    '== Binding Source Changes
    Private Sub OrdersBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles OrdersBindingSource.CurrentChanged

        Dim currentRow = OrdersBindingSource.Current

        '== Grab total item price
        Dim fltr() = Nothing
        Try
            fltr = Ds.Order_Details.Select("OrderID =" & currentRow.Item("OrderID"))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim total = 0.0
        For Each dr In fltr
            total += dr.Item("Ext Price")
        Next
        itemsPriceLabel.Text = FormatCurrency(total)

        '== Grab freight
        itemsFreightLabel.Text = currentRow.Item("Freight")

        '== Display total amount of order
        totalItemsPriceLabel.Text = FormatCurrency(total + currentRow.Item("Freight"))

    End Sub

End Class
