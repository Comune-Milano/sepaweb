
Partial Class Contratti_VisualizzaIstatERP
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Trim(Session.Item("OPERATORE"))) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Dim i As Integer

            For i = Year(Now) - 5 To Year(Now) + 1
                cmbAnno.Items.Add(New ListItem(i, i))
            Next
            cmbAnno.Items.FindByValue(i - 1).Selected = True
            cmbMese.Items.FindByValue("00").Selected = True
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub imgVisualizza_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgVisualizza.Click
        Dim Str As String = ""
        If cmbMese.SelectedItem.Value = "00" Then
            Str = "SELECT to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_INIZIO,'yyyymmdd'),'dd/mm/yyyy') as ""INIZIO"",to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_FINE,'yyyymmdd'),'dd/mm/yyyy') as ""FINE"",RAPPORTI_UTENZA.ID,COD_CONTRATTO,to_char(to_date(data_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') as ""DECORRENZA"",PERC_ISTAT,ADEGUAMENTO_ISTAT.IMPORTO_CANONE_INIZIALE AS ""IMP_CANONE_INIZIALE"" ,IMPORTO_TR_AGG,IMPORTO_CANONE_AGG  FROM SISCOM_MI.ADEGUAMENTO_ISTAT,SISCOM_MI.RAPPORTI_UTENZA WHERE ADEGUAMENTO_ISTAT.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SUBSTR(ADEGUAMENTO_ISTAT.ANNO_MESE,1,4)='" & cmbAnno.SelectedItem.Value & "' order by rapporti_utenza.cod_contratto asc"
        Else
            Str = "SELECT to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_INIZIO,'yyyymmdd'),'dd/mm/yyyy') as ""INIZIO"",to_char(to_date(ADEGUAMENTO_ISTAT.DATA_AGG_FINE,'yyyymmdd'),'dd/mm/yyyy') as ""FINE"",RAPPORTI_UTENZA.ID,COD_CONTRATTO,to_char(to_date(data_DECORRENZA,'yyyymmdd'),'dd/mm/yyyy') as ""DECORRENZA"",PERC_ISTAT,ADEGUAMENTO_ISTAT.IMPORTO_CANONE_INIZIALE AS ""IMP_CANONE_INIZIALE"" ,IMPORTO_TR_AGG,IMPORTO_CANONE_AGG  FROM SISCOM_MI.ADEGUAMENTO_ISTAT,SISCOM_MI.RAPPORTI_UTENZA WHERE ADEGUAMENTO_ISTAT.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SUBSTR(ADEGUAMENTO_ISTAT.ANNO_MESE,1,6)='" & cmbAnno.SelectedItem.Value & cmbMese.SelectedItem.Value & "' order by rapporti_utenza.cod_contratto asc"
        End If


        HttpContext.Current.Session.Add("BB", Str)
        Response.Write("<script>window.open('SceltaIstatERP2.aspx');</script>")
    End Sub
End Class
