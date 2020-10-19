Imports Telerik.Web.UI

Partial Class Gestione_locatari_SpeseComponenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then
            idSpesa.Value = Request.QueryString("IDS")
            HFBtnToClick.Value = Request.QueryString("BTN").ToString
            operazione.Value = Request.QueryString("O")

            If Not String.IsNullOrEmpty(idSpesa.Value.ToString) Then
                VisualizzaSpese()
            End If
        End If
    End Sub

    Private Sub VisualizzaSpese()
        Try
            Dim ApertaNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                ApertaNow = True
            End If

            par.cmd.CommandText = "select * from COMP_ELENCO_SPESE_VSA where id=" & idSpesa.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                txtDescrizione.Text = par.IfNull(lettore("descrizione"), "")
                txtImporto.Text = par.VirgoleInPunti(CStr(par.IfEmpty(lettore("IMPORTO").ToString.Replace(".", ""), 0))) 'Format(par.IfNull(lettore("importo"), "0").ToString, "##,##0.00")
            End If
            lettore.Close()

            If ApertaNow Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        Try
            connData.apri(True)

            par.cmd.CommandText = "UPDATE COMP_ELENCO_SPESE_VSA SET IMPORTO=" & par.VirgoleInPunti(par.IfEmpty(txtImporto.Text, 0)) & ",DESCRIZIONE='" & par.PulisciStrSql(txtDescrizione.Text) & "' WHERE ID=" & idSpesa.Value
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

            par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
