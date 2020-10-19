
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_SituazioneContabileSintesi
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
            'TextBoxAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'TextBoxCDPAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'TextBoxDataFatturaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'TextBoxDataPagamentoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'TextBoxCDPDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'TextBoxDataFatturaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'TextBoxDataPagamentoDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub
    Private Sub caricaElencoEsercizi()
        Try
            connData.apri()
            Dim query As String = "SELECT PF_MAIN.ID ID,GETDATA(INIZIO)||'-'||GETDATA(FINE)|| ' (' || DESCRIZIONE || ')' AS PERIODO " _
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
    
    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        Dim CDPAl As String = ""
        If Not IsNothing(TextBoxCDPAl.SelectedDate) Then
            CDPAl = TextBoxCDPAl.SelectedDate
        End If
        Dim DataFatturaAl As String = ""
        If Not IsNothing(TextBoxDataFatturaAl.SelectedDate) Then
            DataFatturaAl = TextBoxDataFatturaAl.SelectedDate
        End If

        Dim DataPagamentoAl As String = ""
        If Not IsNothing(TextBoxDataPagamentoAl.SelectedDate) Then
            DataPagamentoAl = TextBoxDataPagamentoAl.SelectedDate
        End If
        Dim CDPDal As String = ""
        If Not IsNothing(TextBoxCDPDal.SelectedDate) Then
            CDPDal = TextBoxCDPDal.SelectedDate
        End If
        Dim DataFatturaDal As String = ""
        If Not IsNothing(TextBoxDataFatturaDal.SelectedDate) Then
            DataFatturaDal = TextBoxDataFatturaDal.SelectedDate
        End If
        Dim DataPagamentoDal As String = ""
        If Not IsNothing(TextBoxDataPagamentoDal.SelectedDate) Then
            DataPagamentoDal = TextBoxDataPagamentoDal.SelectedDate
        End If

        Dim DataRegistrazioneFatturaDal As String = ""
        If Not IsNothing(TextBoxDataRegistrazioneFatturaDal.SelectedDate) Then
            DataRegistrazioneFatturaDal = TextBoxDataRegistrazioneFatturaDal.SelectedDate
        End If
        Dim DataRegistrazioneFatturaAl As String = ""
        If Not IsNothing(TextBoxDataRegistrazioneFatturaAl.SelectedDate) Then
            DataRegistrazioneFatturaAl = TextBoxDataRegistrazioneFatturaAl.SelectedDate
        End If

        Dim script As String = "window.open('RisultatiSituazioneContabileGeneraleSintesi.aspx?id=" _
                       & DropDownListEsercizioContabile.SelectedValue _
                       & "&es=" & DropDownListEsercizioContabile.SelectedItem.Text _
                       & "&dal=" & TextBoxAl.Text _
                       & "&cdpal=" & CDPAl _
                       & "&fatal=" & DataFatturaAl _
                       & "&regfatal=" & DataRegistrazioneFatturaAl _
                       & "&pagal=" & DataPagamentoAl _
                       & "&cdpdal=" & CDPDal _
                       & "&fatdal=" & DataFatturaDal _
                       & "&pagdal=" & DataPagamentoDal _
                       & "&regfatdal=" & DataRegistrazioneFatturaDal _
                       & "','SituazioneContabileGenSintesi','');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", script, True)

    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Redirect("../../pagina_home_ncp.aspx")
    End Sub
End Class
