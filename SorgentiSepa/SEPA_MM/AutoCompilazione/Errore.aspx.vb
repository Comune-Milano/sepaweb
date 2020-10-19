
Partial Class AutoCompilazione_Errore
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = Session.Item("ERRORE")
        HyperLink2.NavigateUrl = "mailto:Alessandro.Gobbi@comune.milano.it?subject=Errore&body=Data e Ora: " & Now & " Descrizione Errore: " & Label1.Text
    End Sub
End Class
