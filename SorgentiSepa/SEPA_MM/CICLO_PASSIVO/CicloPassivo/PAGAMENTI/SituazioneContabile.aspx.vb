
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_SituazioneContabile
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            caricaElencoEsercizi()
            TextBoxAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub
    Private Sub caricaElencoEsercizi()
        Try
            connData.apri()
            Dim query As String = "SELECT PF_MAIN.ID ID,GETDATA(INIZIO)||'-'||GETDATA(FINE) || ' (' || DESCRIZIONE || ')' AS PERIODO " _
                & " FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI " _
                & " WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                & " AND PF_STATI.ID=PF_MAIN.ID_STATO " _
                & " AND ID_STATO>=5 ORDER BY PF_MAIN.ID DESC"
            par.caricaComboTelerik(query, DropDownListEsercizioContabile, "ID", "PERIODO", False)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - caricaElencoEsercizi - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub


    Private Sub btnStampa_Click(sender As Object, e As EventArgs) Handles btnStampa.Click
        Dim script As String = "window.open('RisultatiSituazioneContabileGenerale.aspx?id=" & DropDownListEsercizioContabile.SelectedValue _
                       & "&es=" & DropDownListEsercizioContabile.SelectedItem.Text _
                       & "&dal=" & TextBoxAl.Text & "','SituazioneContabileGen','');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", script, True)
    End Sub

    Private Sub btnAnnulla_Click(sender As Object, e As EventArgs) Handles btnAnnulla.Click
        Response.Redirect("../../pagina_home_ncp.aspx")
    End Sub
End Class
