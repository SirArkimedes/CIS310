'=========================================='
'= CIS 310, Andrew Robinson, Assignment 6 ='
'=========================================='

Public Class MasterUpdate

    Private wantsNewCustomer = False
    Private savedPosition = 0
    Private previousCustomer As Customer
    Private previousProductID = 0

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
        CustomerIDTextBox.ReadOnly = False
        undoCustomerButton.Enabled = False
        deleteCustomerButton.Enabled = True

        wantsNewCustomer = True
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
                GrabPreviousCustomer(CustomerChangeType.Deleted)
                DeleteCustomer()
            End If
        End If
    End Sub

    Private Sub saveCustomerButton_Click(sender As Object, e As EventArgs) Handles saveCustomerButton.Click

        If wantsNewCustomer Then
            If SuccessfullyCreatedNewCustomer() Then
                wantsNewCustomer = False
                SetReadOnlyCustomerInformation(False)
            End If
        Else
            '== Wants to update customer
            If SuccessfullyUpdatedCustomer() Then
                SetReadOnlyCustomerInformation(False)
            End If
        End If

    End Sub

    Private Sub undoCustomerButton_Click(sender As Object, e As EventArgs) Handles undoCustomerButton.Click
        SetReadOnlyCustomerInformation(False)

        If wantsNewCustomer Then
            '== Wants to undo while a new customer is being created.
            '== Cancel the currently edited customer.
            SetReadOnlyCustomerInformation(False)
            CustomersBindingSource.CancelEdit()
            CustomersBindingSource.Position = savedPosition
            wantsNewCustomer = False
        Else
            '== Wants to undo at any other point.

            If previousCustomer.ChangeType = CustomerChangeType.Deleted Then
                '== Previous customer was deleted - Add it back

                Try
                    Dim newCustomer = Ds.Customers.NewCustomersRow()

                    InputPreviousCustomerDataForCustomer(newCustomer)

                    Ds.Customers.Rows.Add(newCustomer)
                    CustomersTableAdapter.Update(Ds.Customers)

                    '== Reassign Orders to the customer
                    For Each value In previousCustomer.orderIds
                        Dim orders() = Ds.Orders.Select("OrderID = '" + value + "'")
                        For Each order In orders
                            order("CustomerID") = previousCustomer.ID
                        Next
                    Next

                    OrdersTableAdapter.Update(Ds.Orders)

                    CustomersBindingSource.Position = CustomersBindingSource.Count - 1

                Catch ex As Exception
                    ThrowError("Error undoing deleted customer", ex.Message)
                End Try

            ElseIf previousCustomer.ChangeType = CustomerChangeType.Edited Then
                '== Previous customer was edited - Rewrite the edits

                Try
                    Dim oldCustomer = Ds.Customers.FindByCustomerID(previousCustomer.ID)

                    InputPreviousCustomerDataForCustomer(oldCustomer)

                    CustomersBindingSource.EndEdit()
                    CustomersTableAdapter.Update(Ds.Customers)

                Catch ex As Exception
                    ThrowError("Error undoing edited customer", ex.Message)
                End Try

            ElseIf previousCustomer.ChangeType = CustomerChangeType.Created Then
                '== Previous customer was Created - Delete it

                Try
                    Dim customer = Ds.Customers.FindByCustomerID(previousCustomer.ID)
                    Dim orders() = Ds.Orders.Select("CustomerID = '" + previousCustomer.ID + "'")

                    '== Check for already created dummy customer
                    If Ds.Customers.Select("CustomerID = '_DEL'").Count = 0 Then
                        '== Verify that we actually have orders to move
                        If Not orders.Count = 0 Then
                            '== Create dummy customer
                            Dim newCustomer = Ds.Customers.NewCustomersRow()
                            newCustomer.CustomerID = "_DEL"
                            newCustomer.CompanyName = "** Removed **"
                            Ds.Customers.Rows.Add(newCustomer)

                            CustomersTableAdapter.Update(Ds.Customers)
                        End If
                    End If

                    '== Delete the customer
                    For Each order In orders
                        order("CustomerID") = "_DEL"
                    Next

                    Ds.Customers.FindByCustomerID(previousCustomer.ID).Delete()

                    OrdersTableAdapter.Update(Ds.Orders)
                    CustomersTableAdapter.Update(Ds.Customers)
                Catch ex As Exception
                    ThrowError("Error undoing created customer", ex.Message)
                End Try

            End If

        End If

        previousCustomer = Nothing
        undoCustomerButton.Enabled = False

    End Sub

    Private Sub editCustomerButton_Click(sender As Object, e As EventArgs) Handles editCustomerButton.Click
        If Not CustomerIDTextBox.Text = "_DEL" Then
            SetReadOnlyCustomerInformation(True)
            GrabPreviousCustomer(CustomerChangeType.Edited)
        Else
            ThrowError("Error editing customer", "Not allowed to edit this dummy customer")
        End If
    End Sub

    '== DataGridView Actions
    Private Sub OrdersDataGridView_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) _
        Handles OrdersDataGridView.DataError, Order_DetailsDataGridView.DataError
        '== Catch the errors to not make them look so ugly.
        If e.Exception IsNot Nothing Then
            ThrowError("Error while editing cell", e.Exception.Message)
        End If
    End Sub

    Private Sub OrdersDataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles OrdersDataGridView.CellEndEdit
        Try
            OrdersBindingSource.EndEdit()
            OrdersTableAdapter.Update(Ds.Orders)
            OrdersTableAdapter.Fill(Ds.Orders)
        Catch ex As Exception
            ThrowError("Error in Orders end edit", ex.Message)
        End Try
    End Sub

    Private Sub OrdersDataGridView_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles OrdersDataGridView.RowsRemoved
        '== Delete specific entry - Check for user interaction
        If OrdersDataGridView.SelectedRows.Count > 0 Then
            Try
                '== Wasn't sure if needed that I reassign Order_Details.
                '== Figured an entire order was deleted, so it did not make a difference.
                Ds.Orders.Rows(e.RowIndex).Delete()

                OrdersBindingSource.EndEdit()
                OrdersTableAdapter.Update(Ds.Orders)
                OrdersTableAdapter.Fill(Ds.Orders)
            Catch ex As Exception
                ThrowError("Error when updating Order Details", ex.Message)
            End Try
        End If
    End Sub

    Private Sub Order_DetailsDataGridView_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Order_DetailsDataGridView.CellBeginEdit
        previousProductID = Order_DetailsDataGridView.Rows(e.RowIndex).Cells(1).Value.ToString()
    End Sub

    Private Sub Order_DetailsDataGridView_UserAddedRow(sender As Object, e As DataGridViewRowEventArgs) Handles Order_DetailsDataGridView.UserAddedRow

        Dim Row = Order_DetailsDataGridView.Rows(Order_DetailsDataGridView.CurrentCell.RowIndex)

        '== Add newly created Order Detail to the table
        If Not IsDBNull(Row.Cells(0).Value) Then
            Dim updateCmd As New OleDb.OleDbCommand
            updateCmd.CommandType = CommandType.Text
            updateCmd.Connection = Order_DetailsTableAdapter.Connection
            updateCmd.CommandText = "INSERT INTO [Order Details] (OrderID, ProductID) "
            updateCmd.CommandText += "VALUES (" + Row.Cells(0).Value.ToString() + ", 1)"

            Order_DetailsTableAdapter.Connection.Open()
            Try
                updateCmd.ExecuteNonQuery()
                Order_DetailsTableAdapter.Connection.Close()

                Order_DetailsTableAdapter.Fill(Ds.Order_Details)
            Catch ex As Exception
                Order_DetailsTableAdapter.Connection.Close()
                ThrowError("Error when updating Order Details", ex.Message)
            End Try
        End If
    End Sub

    Private Sub Orders_DetailsDataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Order_DetailsDataGridView.CellEndEdit
        '== Update this table in the database to reflect changes made from DataGridView
        Dim Row = Order_DetailsDataGridView.Rows(e.RowIndex)

        Dim updateCmd As New OleDb.OleDbCommand
        updateCmd.CommandType = CommandType.Text
        updateCmd.Connection = Order_DetailsTableAdapter.Connection
        updateCmd.CommandText = "UPDATE [Order Details] "
        updateCmd.CommandText += "SET ProductID=" + Row.Cells(1).Value.ToString()
        updateCmd.CommandText += ", Quantity=" + Row.Cells(3).Value.ToString()
        updateCmd.CommandText += ", Discount=" + Row.Cells(4).Value.ToString()
        updateCmd.CommandText += " WHERE ProductID = " + previousProductID.ToString()
        updateCmd.CommandText += " AND OrderID = " + Row.Cells(0).Value.ToString()

        Dim priceGrab As New OleDb.OleDbCommand
        priceGrab.CommandType = CommandType.Text
        priceGrab.Connection = Order_DetailsTableAdapter.Connection
        priceGrab.CommandText = "UPDATE [Order Details]"
        priceGrab.CommandText += " INNER JOIN Products ON [Order Details].ProductID = Products.ProductID"
        priceGrab.CommandText += " SET [Order Details].UnitPrice = Products.UnitPrice"
        priceGrab.CommandText += " WHERE [Order Details].OrderID = " + Row.Cells(0).Value.ToString()
        priceGrab.CommandText += " AND [Order Details].ProductID = " + Row.Cells(1).Value.ToString()

        Order_DetailsTableAdapter.Connection.Open()
        Try
            updateCmd.ExecuteNonQuery()
            priceGrab.ExecuteNonQuery()
            Order_DetailsTableAdapter.Connection.Close()

            Order_DetailsTableAdapter.Fill(Ds.Order_Details)
        Catch ex As Exception
            Order_DetailsTableAdapter.Connection.Close()
            ThrowError("Error when updating Order Details", ex.Message)
        End Try
    End Sub

    Private Sub Order_DetailsDataGridView_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles Order_DetailsDataGridView.RowsRemoved
        '== Delete specific entry - Check for user interaction
        If Order_DetailsDataGridView.SelectedRows.Count > 0 Then
            Dim Row = Order_DetailsDataGridView.Rows(e.RowIndex)

            Dim cmd As New OleDb.OleDbCommand
            cmd.CommandType = CommandType.Text
            cmd.Connection = Order_DetailsTableAdapter.Connection
            cmd.CommandText = "DELETE FROM [Order Details] "
            cmd.CommandText += " WHERE ProductID = " + Row.Cells(1).Value.ToString()
            cmd.CommandText += "AND OrderID = " + Row.Cells(0).Value.ToString()

            Order_DetailsTableAdapter.Connection.Open()
            Try
                cmd.ExecuteNonQuery()
                Order_DetailsTableAdapter.Connection.Close()

                Order_DetailsTableAdapter.Fill(Ds.Order_Details)
            Catch ex As Exception
                Order_DetailsTableAdapter.Connection.Close()
                ThrowError("Error when updating Order Details", ex.Message)
            End Try
        End If
    End Sub

    '== Binding Source Changes
    Private Sub OrdersBindingSource_CurrentChanged(sender As Object, e As EventArgs) _
        Handles OrdersBindingSource.CurrentChanged, Order_DetailsBindingSource.CurrentItemChanged

        Dim currentRow = OrdersBindingSource.Current

        '== Do we have a selected row?
        If Not IsDBNull(currentRow.Item("Freight")) Then
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
    Private Function SuccessfullyCreatedNewCustomer() As Boolean
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
            ThrowError("Empty field", "Must provide a value For CustomerID")
        Else
            If CompanyNameTextBox.Text = "" Then
                ThrowError("Empty field", "Must provide a value For CompanyName")
            Else
                newCustomer.CustomerID = CustomerIDTextBox.Text
                newCustomer.CompanyName = CompanyNameTextBox.Text

                Try
                    CustomersBindingSource.EndEdit()
                    CustomersTableAdapter.Update(Ds.Customers)
                    GrabPreviousCustomer(CustomerChangeType.Created)

                    CustomersBindingSource.Position = CustomersBindingSource.Count - 1

                    success = True
                Catch ex As Exception
                    ThrowError("Error creating New customer", ex.Message)
                End Try
            End If
        End If

        Return success

    End Function

    Private Sub DeleteCustomer()

        '== Check for already created dummy customer
        Try
            If Ds.Customers.Select("CustomerID = '_DEL'").Count = 0 Then
                '== Verify that we actually have orders to move
                If Not Ds.Orders.Select("CustomerID = '" + CustomerIDTextBox.Text + "'").Count = 0 Then
                    '== Create dummy customer
                    Dim newCustomer = Ds.Customers.NewCustomersRow()
                    newCustomer.CustomerID = "_DEL"
                    newCustomer.CompanyName = "** Removed **"
                    Ds.Customers.Rows.Add(newCustomer)

                    CustomersTableAdapter.Update(Ds.Customers)
                End If
            End If
        Catch ex As Exception
            ThrowError("Error deleting customer", ex.Message)
        End Try

        '== Are we selected on the deleted customer?
        If CustomerIDTextBox.Text = "_DEL" Then
            ThrowError("Error", "Cannot delete a deleted customer.")
        Else
            '== Delete the customer
            Try
                Dim orders() = Ds.Orders.Select("CustomerID = '" + CustomerIDTextBox.Text + "'")
                Dim ids(OrdersBindingSource.Count - 1) As String
                Dim i = 0
                For Each order In orders
                    ids(i) = order("OrderID")
                    order("CustomerID") = "_DEL"
                    i += 1
                Next
                previousCustomer.orderIds = ids

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

        Try
            CustomersBindingSource.EndEdit()
            CustomersTableAdapter.Update(Ds.Customers)

            success = True
        Catch ex As Exception
            ThrowError("Error updating customer", ex.Message)
        End Try

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
        ContactNameTextBox.ReadOnly = state : ContactTitleTextBox.ReadOnly = state
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

    Private Sub GrabPreviousCustomer(action As CustomerChangeType)
        previousCustomer = New Customer()
        previousCustomer.ID = CustomerIDTextBox.Text
        previousCustomer.CompanyName = CompanyNameTextBox.Text
        previousCustomer.ContactName = ContactNameTextBox.Text
        previousCustomer.ContactTitle = ContactTitleTextBox.Text
        previousCustomer.Address = AddressTextBox.Text
        previousCustomer.City = CityTextBox.Text
        previousCustomer.Region = RegionTextBox.Text
        previousCustomer.PostalCode = PostalCodeTextBox.Text
        previousCustomer.Country = CountryTextBox.Text
        previousCustomer.Phone = PhoneTextBox.Text
        previousCustomer.Fax = FaxTextBox.Text
        previousCustomer.ChangeType = action

        undoCustomerButton.Enabled = True
    End Sub

    Private Sub InputPreviousCustomerDataForCustomer(customer As ds.CustomersRow)
        customer.CustomerID = previousCustomer.ID
        customer.CompanyName = previousCustomer.CompanyName

        '== Verify that these are not empty because these cannot be stored in the new customer.
        If Not previousCustomer.ContactName = "" Then
            customer.ContactName = previousCustomer.ContactName
        Else
            customer.ContactName = " "
        End If
        If Not previousCustomer.ContactTitle = "" Then
            customer.ContactTitle = previousCustomer.ContactTitle
        Else
            customer.ContactTitle = " "
        End If
        If Not previousCustomer.Address = "" Then
            customer.Address = previousCustomer.Address
        Else
            customer.Address = " "
        End If
        If Not previousCustomer.City = "" Then
            customer.City = previousCustomer.City
        Else
            customer.City = " "
        End If
        If Not previousCustomer.Region = "" Then
            customer._Region = previousCustomer.Region
        Else
            customer._Region = " "
        End If
        If Not previousCustomer.PostalCode = "" Then
            customer.PostalCode = previousCustomer.PostalCode
        Else
            customer.PostalCode = " "
        End If
        If Not previousCustomer.Country = "" Then
            customer.Country = previousCustomer.Country
        Else
            customer.Country = " "
        End If
        If Not previousCustomer.Phone = "" Then
            customer.Phone = previousCustomer.Phone
        Else
            customer.Phone = " "
        End If
        If Not previousCustomer.Fax = "" Then
            customer.Fax = previousCustomer.Fax
        Else
            customer.Fax = " "
        End If
    End Sub

    Private Sub ThrowError(title As String, message As String)
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

End Class
