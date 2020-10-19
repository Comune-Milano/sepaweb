Partial Class Contratti_Pagamenti_NuovaTipologiaPagamento
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            If Request.QueryString("id") <> "" Then
                caricaDescrizione()
                ImageButtonNuova.Visible = False
                ImageButtonModifica.Visible = True
            Else
                ImageButtonNuova.Visible = True
                ImageButtonModifica.Visible = False
            End If
        End If
    End Sub
    Private Sub caricaDescrizione()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select descrizione from siscom_mi.tipo_pag_parz where id=" & Request.QueryString("id")
            Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LETTORE.Read Then
                TextBoxDescrizione.Text = par.IfNull(LETTORE(0), "")
            End If
            LETTORE.Close()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Errore durante il caricamento della tipologia di pagamento!')</script>")
        End Try
    End Sub
    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Redirect("../pagina_home.aspx")
    End Sub
    Protected Sub ImageButtonModifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonModifica.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "update siscom_mi.tipo_pag_parz set descrizione='" & Replace(TextBoxDescrizione.Text, "'", "''") & "' where id=" & Request.QueryString("id")
            par.cmd.ExecuteNonQuery()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Redirect("TipologiaPagamenti.aspx")
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Errore durante la modifica della tipologia di pagamento!')</script>")
        End Try
    End Sub

    Protected Sub ImageButtonNuova_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonNuova.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "insert into siscom_mi.tipo_pag_parz (id,descrizione) values(siscom_mi.seq_tipo_pag_parz.nextval,'" & Replace(TextBoxDescrizione.Text, "'", "''") & "') "
            par.cmd.ExecuteNonQuery()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Redirect("TipologiaPagamenti.aspx")
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Errore durante l\'inserimento della tipologia di pagamento!')</script>")
        End Try
    End Sub
End Class
