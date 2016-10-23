Public Class SynchedGrid
    Private Sub CustomersBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs)
        Me.Validate()
        Me.CustomersBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.Ds)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Ds.Order_Details' table. You can move, or remove it, as needed.
        Me.Order_DetailsTableAdapter.Fill(Me.Ds.Order_Details)
        'TODO: This line of code loads data into the 'Ds.Orders' table. You can move, or remove it, as needed.
        Me.OrdersTableAdapter.Fill(Me.Ds.Orders)
        'TODO: This line of code loads data into the 'Ds.Customers' table. You can move, or remove it, as needed.
        Me.CustomersTableAdapter.Fill(Me.Ds.Customers)

    End Sub
End Class
