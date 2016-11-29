'=========================================='
'= CIS 310, Andrew Robinson, Assignment 6 ='
'=========================================='

Public Class MasterUpdate

    Private wantsNewCustomer = False
    Private savedPosition = 0

    '== Load
    Private Sub SynchedGrid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PullData()

        saveCustomerButton.Enabled = False
        undoCustomerButton.Enabled = False
    End Sub

    '== Button Press Actions
    Private Sub addCustomerButton_Click(sender As Object, e As EventArgs) Handles addCustomerButton.Click
        savedPosition = CustomersBindingSource.Position
        SetReadOnlyCustomerInformation(True)
        wantsNewCustomer = True
        undoCustomerButton.Enabled = False
        deleteCustomerButton.Enabled = True
    End Sub

    Private Sub deleteCustomerButton_Click(sender As Object, e As EventArgs) Handles deleteCustomerButton.Click
        If wantsNewCustomer Then
            SetReadOnlyCustomerInformation(False)
            CustomersBindingSource.CancelEdit()
            CustomersBindingSource.Position = savedPosition
            wantsNewCustomer = False
        Else
            If MessageBox.Show("Are you sure you want to delete " + CustomerIDTextBox.Text + "?", "Deleting " + CustomerIDTextBox.Text,
                               MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then
                DeleteCustomer()
                PullData()
            End If
        End If
    End Sub

    Private Sub saveCustomerButton_Click(sender As Object, e As EventArgs) Handles saveCustomerButton.Click

        If wantsNewCustomer Then
            If SuccessfullyCreatedNewUser() Then
                wantsNewCustomer = False
                SetReadOnlyCustomerInformation(False)
            End If
        Else
            'Update Customer
            SetReadOnlyCustomerInformation(False)
        End If

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

        '== Do we have a selected row?
        If TypeOf currentRow Is Object Then
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
        Else
            itemsPriceLabel.Text = "$0.00"
            itemsFreightLabel.Text = "0"
            totalItemsPriceLabel.Text = "$0.00"
        End If
    End Sub

    '== Database calls
    Private Function SuccessfullyCreatedNewUser() As Boolean
        Dim success = False

        Dim newCustomer = Ds.Customers.NewCustomersRow()

        '== Verify that these are not empty because these cannot be stored in the new customer.
        If Not ContactNameTextBox.Text = "" Then
            newCustomer.ContactName = ContactNameTextBox.Text
        End If
        If Not ContactTitleTextBox.Text = "" Then
            newCustomer.ContactTitle = ContactTitleTextBox.Text
        End If
        If Not AddressTextBox.Text = "" Then
            newCustomer.Address = AddressTextBox.Text
        End If
        If Not CityTextBox.Text = "" Then
            newCustomer.City = CityTextBox.Text
        End If
        If Not RegionTextBox.Text = "" Then
            newCustomer._Region = RegionTextBox.Text
        End If
        If Not PostalCodeTextBox.Text = "" Then
            newCustomer.PostalCode = PostalCodeTextBox.Text
        End If
        If Not CountryTextBox.Text = "" Then
            newCustomer.Country = CountryTextBox.Text
        End If
        If Not PhoneTextBox.Text = "" Then
            newCustomer.Phone = PhoneTextBox.Text
        End If
        If Not FaxTextBox.Text = "" Then
            newCustomer.Fax = FaxTextBox.Text
        End If

        '== Verify that these are not empty.
        If CustomerIDTextBox.Text = "" Then
            ThrowError("Empty field", "Must provide a value for CustomerID")
        Else
            If CompanyNameTextBox.Text = "" Then
                ThrowError("Empty field", "Must provide a value for CompanyName")
            Else
                newCustomer.CustomerID = CustomerIDTextBox.Text
                newCustomer.CompanyName = CompanyNameTextBox.Text

                Try
                    Ds.Customers.Rows.Add(newCustomer)
                    CustomersTableAdapter.Update(Ds.Customers)

                    PullData()
                    CustomersBindingSource.Position = savedPosition

                    success = True
                Catch ex As Exception
                    ThrowError("Error creating new customer", ex.Message)
                End Try
            End If
        End If

        Return success

    End Function

    Private Sub DeleteCustomer()

        '== Check for already created removed customer
        If Ds.Customers.Select("CustomerID = '_DEL'").Count = 0 Then
            '== Delete dummy customer
            Try
                Dim newCustomer = Ds.Customers.NewCustomersRow()
                newCustomer.CustomerID = "_DEL"
                newCustomer.CompanyName = "Removed"
                Ds.Customers.Rows.Add(newCustomer)

                CustomersTableAdapter.Update(Ds.Customers)
            Catch ex As Exception
                ThrowError("Error deleting customer", ex.Message)
            End Try
        End If

        '== Are we selected on the deleted customer?
        If CustomerIDTextBox.Text = "_DEL" Then
            ThrowError("Error", "Cannot delete a deleted customer.")
        Else
            '== Delete the customer
            Try
                Dim orders() = Ds.Orders.Select("CustomerID = '" + CustomerIDTextBox.Text + "'")
                For Each order In orders
                    order("CustomerID") = "_DEL"
                Next

                Ds.Customers.Rows(CustomersBindingSource.Position).Delete()

                OrdersTableAdapter.Update(Ds.Orders)
                CustomersTableAdapter.Update(Ds.Customers)
            Catch ex As Exception
                ThrowError("Error deleting customer", ex.Message)
            End Try
        End If

    End Sub

    Private Function SuccessfullyUpdatedCustomer() As Boolean
        Dim success = False

        Return success
    End Function

    '== Helper Methods
    Private Sub SetReadOnlyCustomerInformation(state As Boolean)
        saveCustomerButton.Enabled = state

        OrdersDataGridView.ReadOnly = state
        Order_DetailsDataGridView.ReadOnly = state

        state = Not (state)

        addCustomerButton.Enabled = state
        deleteCustomerButton.Enabled = state
        editCustomerButton.Enabled = state

        '== Guard against enabling when they would not change position
        If Not CustomersBindingSource.Position = 0 Then
            BindingNavigatorMoveFirstItem.Enabled = state
            BindingNavigatorMovePreviousItem.Enabled = state
        End If
        If Not CustomersBindingSource.Position = CustomersBindingSource.Count - 1 Then
            BindingNavigatorMoveNextItem.Enabled = state
            BindingNavigatorMoveLastItem.Enabled = state
        End If

        If state Then
            CompanyNameDropDown.BringToFront()
        Else
            CompanyNameTextBox.BringToFront()
        End If

        '== Change customer text boxes
        CustomerIDTextBox.ReadOnly = state : ContactNameTextBox.ReadOnly = state : ContactTitleTextBox.ReadOnly = state
        AddressTextBox.ReadOnly = state : CityTextBox.ReadOnly = state : RegionTextBox.ReadOnly = state
        PostalCodeTextBox.ReadOnly = state : CountryTextBox.ReadOnly = state : PhoneTextBox.ReadOnly = state
        FaxTextBox.ReadOnly = state

        OrdersDataGridView.AllowUserToAddRows = state
        Order_DetailsDataGridView.AllowUserToAddRows = state
        OrdersDataGridView.AllowUserToDeleteRows = state
        Order_DetailsDataGridView.AllowUserToDeleteRows = state
    End Sub

    Private Sub PullData()
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

    Private Sub ThrowError(title As String, message As String)
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

End Class
