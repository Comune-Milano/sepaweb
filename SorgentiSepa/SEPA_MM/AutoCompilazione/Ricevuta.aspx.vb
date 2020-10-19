
Partial Class AutoCompilazione_Ricevuta
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = par.DeCriptaMolto(Request.QueryString("DO"))
        Label2.Text = par.DeCriptaMolto(Request.QueryString("NO"))
        lblTelefono.Text = par.DeCriptaMolto(Request.QueryString("TE"))
    End Sub
End Class
