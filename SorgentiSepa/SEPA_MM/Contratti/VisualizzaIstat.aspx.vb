
Partial Class Contratti_VisualizzaIstat
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Dim i As Integer

            For i = Year(Now) - 5 To Year(Now) + 1
                cmbAnno.Items.Add(New ListItem(i, i))
            Next
        End If

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub imgVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgVisualizza.Click
        Dim Str As String = "SELECT to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_INIZIO,'yyyymmdd'),'dd/mm/yyyy') as ""INIZIO"",to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_FINE,'yyyymmdd'),'dd/mm/yyyy') as ""FINE"",RAPPORTI_UTENZA.ID,COD_CONTRATTO,to_char(to_date(data_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') as ""DECORRENZA"",PERC_ISTAT,ADEGUAMENTO_ISTAT.IMPORTO_CANONE_INIZIALE AS ""IMP_CANONE_INIZIALE"" ,IMPORTO_TR_AGG,IMPORTO_CANONE_AGG  FROM SISCOM_MI.ADEGUAMENTO_ISTAT,SISCOM_MI.RAPPORTI_UTENZA WHERE ADEGUAMENTO_ISTAT.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ADEGUAMENTO_ISTAT.ANNO_MESE='" & cmbAnno.SelectedItem.Value & cmbMese.SelectedItem.Value & "'"

        HttpContext.Current.Session.Add("BB", Str)
        Response.Write("<script>window.open('SceltaIstat2.aspx');</script>")

    End Sub
End Class
