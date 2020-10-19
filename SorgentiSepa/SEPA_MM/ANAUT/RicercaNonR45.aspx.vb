
Partial Class ANAUT_RicercaNonR
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                par.RiempiDList(Me, par.OracleConn, "cmbBando", "SELECT * FROM utenza_bandi where id>1 ORDER BY id desc", "DESCRIZIONE", "ID")

                If Session.Item("LIVELLO") = "1" Then
                    'par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "select * from siscom_mi.tab_filiali where id_tipo_st=0 and acronimo is not null ORDER BY nome asc", "NOME", "ID")
                    par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "select * from siscom_mi.tab_filiali where id_tipo_st=0 and acronimo is not null ORDER BY nome asc", "NOME", "ID")
                Else
                    par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "select * from siscom_mi.tab_filiali where id_tipo_st=0 and acronimo is not null AND ID=(SELECT ID_UFFICIO FROM OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE") & ") ORDER BY nome asc", "NOME", "ID")
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If

        txtStipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtStipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If cmbFiliale.SelectedItem.Value <> "-1" Then
            Response.Write("<script>location.replace('RisultatoNonR45.aspx?BA=" & cmbBando.SelectedItem.Value & "&FI=" & cmbFiliale.SelectedItem.Value & "&SDAL=" & par.AggiustaData(txtStipulaDal.Text) & "&SAL=" & par.AggiustaData(txtStipulaAl.Text) & "');</script>")
        Else
            Response.Write("<script>alert('Scelta non valida!');</script>")
        End If
    End Sub
End Class
