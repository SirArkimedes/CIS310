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

        saveCustomerButton.Enabled = False
        undoCustomerButton.Enabled = False
    End Sub

    '== Button Press Actions
    Private Sub addCustomerButton_Click(sender As Object, e As EventArgs) Handles addCustomerButton.Click
        SetReadOnlyCustomerInformation(True)
    End Sub

    Private Sub deleteCustomerButton_Click(sender As Object, e As EventArgs) Handles deleteCustomerButton.Click

    End Sub

    Private Sub saveCustomerButton_Click(sender As Object, e As EventArgs) Handles saveCustomerButton.Click
        SetReadOnlyCustomerInformation(False)
    End Sub

    Private Sub undoCustomerButton_Click(sender As Object, e As EventArgs) Handles undoCustomerButton.Click
        SetReadOnlyCustomerInformation(False)
    End Sub

    Private Sub editCustomerButton_Click(sender As Object, e As EventArgs) Handles editCustomerButton.Click
        SetReadOnlyCustomerInformation(True)
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

    '== Helper Methods
    Private Sub SetReadOnlyCustomerInformation(state As Boolean)
        saveCustomerButton.Enabled = state
        undoCustomerButton.Enabled = state

        state = Not (state)

        addCustomerButton.Enabled = state
        deleteCustomerButton.Enabled = state
        editCustomerButton.Enabled = state

        If state Then
            CompanyNameDropDown.BringToFront()
        Else
            CompanyNameTextBox.BringToFront()
        End If

        CustomerIDTextBox.ReadOnly = state : ContactNameTextBox.ReadOnly = state : ContactTitleTextBox.ReadOnly = state
        AddressTextBox.ReadOnly = state : CityTextBox.ReadOnly = state : RegionTextBox.ReadOnly = state
        PostalCodeTextBox.ReadOnly = state : CountryTextBox.ReadOnly = state : PhoneTextBox.ReadOnly = state
        FaxTextBox.ReadOnly = state
    End Sub

End Class
