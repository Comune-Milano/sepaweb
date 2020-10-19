
Imports Telerik.Web.UI

Partial Class SPESE_REVERSIBILI_AggiungiSpesaConsuntivi
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If controlloProfilo() Then
            If Not IsPostBack Then
                Try
                    idSpesa.Value = Request.QueryString("IDSPESA").ToString
                    HFBtnToClick.Value = Request.QueryString("BTN").ToString
                    connData.apri()
                    caricaCriteriRipartizione()
                    caricaTipologieDivisione()
                    caricaTipologieSpesa()
                    caricaCategorie()
                    caricaMaschera()
                    reimpostaNumerieDate()
                    CaricaAttributi()
                    connData.chiudi()
                Catch ex As Exception
                    connData.chiudi()
                    Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - " & ex.Message)
                    RadWindowManager1.RadAlert("Errore durante il caricamento della spesa!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
                End Try
            End If
            CType(Master.FindControl("TitoloMaster"), Label).Text = "Aggiungi spesa"
        End If

    End Sub
    Private Sub reimpostaNumerieDate()
        Try
            TextBoxImporto.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            TextBoxImporto.Attributes.Add("onblur", "javascript:valid(this,'notnumbersOnlyPositive');AutoDecimal2(this);")
            TextBoxImporto.Style.Item("text-align") = "right"
        Catch ex As Exception
            Session.Add("ERRORE", ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Function controlloProfilo() As Boolean
        'CONTROLLO DELLA SESSIONE OPERATORE E DELL'ABILITAZIONE ALLE SPESE REVERSIBILI
        If Session.Item("OPERATORE") <> "" Then
            If Session.Item("fl_spese_reversibili") = "0" Then
                Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Operatore non abilitato alla gestione delle spese reversibili")
                RadWindowManager1.RadAlert("Operatore non abilitato alla gestione delle spese reversibili!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")

                Return False
            End If
        Else
            RadWindowManager1.RadAlert("Accesso negato o sessione scaduta! E\' necessario rieseguire il login!", 300, 150, "Attenzione", "", "null")
            Return False
        End If
        If Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = 0 Then
            RadWindowManager1.RadAlert("E\' necessario selezionare una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")

            Return False
        End If
        If Session.Item("FL_SPESE_REV_SL") = "1" Then
            solaLettura()
        End If
        connData = New CM.datiConnessione(par, False, False)
        Return True
    End Function
    Private Sub solaLettura()
        ButtonAggiungiSpesa.Enabled = False
    End Sub
    Private Sub caricaCriteriRipartizione()
        par.caricaComboTelerik("SELECT * FROM SISCOM_MI.CRITERI_RIPARTIZIONE", DropDownListCriterioRipartizione, "ID", "DESCRIZIONE", False)
        DropDownListCriterioRipartizione.SelectedValue = 4
    End Sub
    Private Sub caricaTipologieDivisione()
        par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPOLOGIA_DIVISIONE", DropDownListTipologiaDivisione, "ID", "DESCRIZIONE", False)
    End Sub
    Private Sub caricaCategorie()
        par.caricaComboTelerik("SELECT ID,GRUPPO||'-'||DESCRIZIONE AS DESCRIZIONE FROM SISCOM_MI.PF_CATEGORIE", DropDownListCategoria, "ID", "DESCRIZIONE", True)
    End Sub
    Private Sub caricaLotto(Optional ByVal idLotto As String = "-1")
        par.caricaComboTelerik("SELECT DISTINCT LOTTI.ID, LOTTI.DESCRIZIONE " _
            & " FROM SISCOM_MI.LOTTI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
            & " WHERE LOTTI.TIPO<>'X' " _
            & " AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
            & " AND LOTTI.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID" _
            & " AND PF_MAIN.ID=" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") _
            & " ORDER BY 1 ASC ", DropDownListLotto, "ID", "DESCRIZIONE", True, "-1", "")
        DropDownListLotto.SelectedValue = idLotto
    End Sub
    Private Sub caricaEdifici(Optional ByVal idEdificio As String = "-1")
        par.caricaComboTelerik("SELECT ID,DENOMINAZIONE " _
            & " FROM SISCOM_MI.EDIFICI WHERE ID<>1 AND FL_PREVENTIVI = 1 " _
            & " ORDER BY 2 ASC ", DropDownListEdificio, "ID", "DENOMINAZIONE", True, "-1", "")
        DropDownListEdificio.SelectedValue = idEdificio
    End Sub
    Private Sub caricaComplessi(Optional ByVal idComplesso As String = "-1")
        par.caricaComboTelerik("SELECT ID,DENOMINAZIONE||' - cod.'|| COD_COMPLESSO AS DENOMINAZIONE " _
            & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " _
            & " ORDER BY 2 ASC ", DropDownListComplesso, "ID", "DENOMINAZIONE", True, "-1", "")
        DropDownListComplesso.SelectedValue = idComplesso
    End Sub
    Private Sub caricaScale(Optional ByVal idScala As String = "-1")
        If DropDownListEdificio.SelectedValue <> "-1" Then
            par.caricaComboTelerik("SELECT ID,DESCRIZIONE " _
                    & " FROM SISCOM_MI.SCALE_EDIFICI WHERE ID_EDIFICIO= " & DropDownListEdificio.SelectedValue _
                    & " ORDER BY 2 ASC ", DropDownListScala, "ID", "DESCRIZIONE", True, "-1", "")
            DropDownListScala.SelectedValue = idScala
        End If
    End Sub
    Private Sub caricaAggregazioni(Optional ByVal idAggregazione As String = "-1")
        par.caricaComboTelerik("SELECT ID,DENOMINAZIONE AS DESCRIZIONE " _
        & " FROM SISCOM_MI.AGGREGAZIONE " _
        & " ORDER BY 2 ASC ", DropDownListAggregazione, "ID", "DESCRIZIONE", True, "-1", "")
        DropDownListAggregazione.SelectedValue = idAggregazione
    End Sub
    Private Sub caricaImpianti(Optional ByVal idImpianto As String = "-1")
        par.caricaComboTelerik("SELECT ID,COD_IMPIANTO||'-'||COD_TIPOLOGIA||'-'||DESCRIZIONE AS DESCRIZIONE " _
            & " FROM SISCOM_MI.IMPIANTI " _
            & " ORDER BY 2 ASC ", DropDownListImpianti, "ID", "DESCRIZIONE", True, "-1", "")
        DropDownListImpianti.SelectedValue = idImpianto
    End Sub
    Protected Sub DropDownListEdificio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListEdificio.SelectedIndexChanged
        caricaScale()
        CaricaTabellaMillesimale()
    End Sub
    Private Sub controlloDropDownList()
        If idSpesa.Value.ToString <> "-1" Then
            'caso modifica
            Select Case DropDownListTipologiaDivisione.SelectedValue
                Case "1"
                    'divisione per impianti
                    DropDownListScala.Enabled = False
                    DropDownListScala.ClearSelection()
                    DropDownListEdificio.Enabled = False
                    DropDownListEdificio.ClearSelection()
                    DropDownListImpianti.Enabled = True
                    DropDownListLotto.Enabled = False
                    DropDownListLotto.ClearSelection()
                    DropDownListComplesso.Enabled = False
                    DropDownListComplesso.ClearSelection()
                    DropDownListAggregazione.Enabled = False
                    DropDownListAggregazione.ClearSelection()
                Case "2"
                    'divisione per scale
                    DropDownListScala.Enabled = True
                    DropDownListEdificio.Enabled = False
                    DropDownListEdificio.ClearSelection()
                    DropDownListImpianti.Enabled = False
                    DropDownListImpianti.ClearSelection()
                    DropDownListLotto.Enabled = False
                    DropDownListLotto.ClearSelection()
                    DropDownListComplesso.Enabled = False
                    DropDownListComplesso.ClearSelection()
                    DropDownListAggregazione.Enabled = False
                    DropDownListAggregazione.ClearSelection()
                Case "3"
                    'divisione per edifici
                    DropDownListScala.Enabled = False
                    DropDownListScala.ClearSelection()
                    DropDownListEdificio.Enabled = True
                    DropDownListImpianti.Enabled = False
                    DropDownListImpianti.ClearSelection()
                    DropDownListLotto.Enabled = False
                    DropDownListLotto.ClearSelection()
                    DropDownListComplesso.Enabled = False
                    DropDownListComplesso.ClearSelection()
                    DropDownListAggregazione.Enabled = False
                    DropDownListAggregazione.ClearSelection()
                Case "4"
                    'divisione intero patrimonio
                    DropDownListScala.Enabled = False
                    DropDownListScala.ClearSelection()
                    DropDownListEdificio.Enabled = False
                    DropDownListEdificio.ClearSelection()
                    DropDownListImpianti.Enabled = False
                    DropDownListImpianti.ClearSelection()
                    DropDownListLotto.Enabled = False
                    DropDownListLotto.ClearSelection()
                    DropDownListComplesso.Enabled = False
                    DropDownListComplesso.ClearSelection()
                    DropDownListAggregazione.Enabled = False
                    DropDownListAggregazione.ClearSelection()
                Case "5"
                    'divisione per lotti
                    DropDownListScala.Enabled = False
                    DropDownListScala.ClearSelection()
                    DropDownListEdificio.Enabled = False
                    DropDownListEdificio.ClearSelection()
                    DropDownListImpianti.Enabled = False
                    DropDownListImpianti.ClearSelection()
                    DropDownListLotto.Enabled = True
                    DropDownListComplesso.Enabled = False
                    DropDownListComplesso.ClearSelection()
                    DropDownListAggregazione.Enabled = False
                    DropDownListAggregazione.ClearSelection()
                Case "6"
                    'divisione per complessi
                    DropDownListScala.Enabled = False
                    DropDownListScala.ClearSelection()
                    DropDownListEdificio.Enabled = False
                    DropDownListEdificio.ClearSelection()
                    DropDownListImpianti.Enabled = False
                    DropDownListImpianti.ClearSelection()
                    DropDownListLotto.Enabled = False
                    DropDownListLotto.ClearSelection()
                    DropDownListComplesso.Enabled = True
                    DropDownListAggregazione.Enabled = False
                    DropDownListAggregazione.ClearSelection()
                Case "7"
                    'divisione per aggregazione
                    DropDownListScala.Enabled = False
                    DropDownListScala.ClearSelection()
                    DropDownListEdificio.Enabled = False
                    DropDownListEdificio.ClearSelection()
                    DropDownListImpianti.Enabled = False
                    DropDownListImpianti.ClearSelection()
                    DropDownListLotto.Enabled = False
                    DropDownListLotto.ClearSelection()
                    DropDownListComplesso.Enabled = False
                    DropDownListComplesso.ClearSelection()
                    DropDownListAggregazione.Enabled = True
            End Select
        Else
            'caso aggiunta spesa
            Select Case DropDownListTipologiaDivisione.SelectedValue
                Case "1"
                    'divisione per impianti
                    DropDownListScala.Enabled = False
                    DropDownListScala.ClearSelection()
                    DropDownListEdificio.Enabled = False
                    DropDownListEdificio.ClearSelection()
                    DropDownListImpianti.Enabled = True
                    DropDownListLotto.Enabled = False
                    DropDownListLotto.ClearSelection()
                    DropDownListComplesso.Enabled = False
                    DropDownListComplesso.ClearSelection()
                    DropDownListAggregazione.Enabled = False
                    DropDownListAggregazione.ClearSelection()
                Case "2"
                    'divisione per scale
                    DropDownListScala.Enabled = True
                    DropDownListEdificio.Enabled = False
                    DropDownListEdificio.ClearSelection()
                    DropDownListImpianti.Enabled = False
                    DropDownListImpianti.ClearSelection()
                    DropDownListLotto.Enabled = False
                    DropDownListLotto.ClearSelection()
                    DropDownListComplesso.Enabled = False
                    DropDownListComplesso.ClearSelection()
                    DropDownListAggregazione.Enabled = False
                    DropDownListAggregazione.ClearSelection()
                Case "3"
                    'divisione per edifici
                    DropDownListScala.Enabled = False
                    DropDownListScala.ClearSelection()
                    DropDownListEdificio.Enabled = True
                    DropDownListImpianti.Enabled = False
                    DropDownListImpianti.ClearSelection()
                    DropDownListLotto.Enabled = False
                    DropDownListLotto.ClearSelection()
                    DropDownListComplesso.Enabled = False
                    DropDownListComplesso.ClearSelection()
                    DropDownListAggregazione.Enabled = False
                    DropDownListAggregazione.ClearSelection()
                Case "4"
                    'divisione intero patrimonio
                    DropDownListScala.Enabled = False
                    DropDownListScala.ClearSelection()
                    DropDownListEdificio.Enabled = False
                    DropDownListEdificio.ClearSelection()
                    DropDownListImpianti.Enabled = False
                    DropDownListImpianti.ClearSelection()
                    DropDownListLotto.Enabled = False
                    DropDownListLotto.ClearSelection()
                    DropDownListComplesso.Enabled = False
                    DropDownListComplesso.ClearSelection()
                    DropDownListAggregazione.Enabled = False
                    DropDownListAggregazione.ClearSelection()
                Case "5"
                    'divisione per lotti
                    DropDownListScala.Enabled = False
                    DropDownListScala.ClearSelection()
                    DropDownListEdificio.Enabled = False
                    DropDownListEdificio.ClearSelection()
                    DropDownListImpianti.Enabled = False
                    DropDownListImpianti.ClearSelection()
                    DropDownListLotto.Enabled = True
                    DropDownListComplesso.Enabled = False
                    DropDownListComplesso.ClearSelection()
                    DropDownListAggregazione.Enabled = False
                    DropDownListAggregazione.ClearSelection()
                Case "6"
                    'divisione per complessi
                    DropDownListScala.Enabled = False
                    DropDownListScala.ClearSelection()
                    DropDownListEdificio.Enabled = False
                    DropDownListEdificio.ClearSelection()
                    DropDownListImpianti.Enabled = False
                    DropDownListImpianti.ClearSelection()
                    DropDownListLotto.Enabled = False
                    DropDownListLotto.ClearSelection()
                    DropDownListComplesso.Enabled = True
                    DropDownListAggregazione.Enabled = False
                    DropDownListAggregazione.ClearSelection()
                Case "7"
                    'divisione per aggregazione
                    DropDownListScala.Enabled = False
                    DropDownListScala.ClearSelection()
                    DropDownListEdificio.Enabled = False
                    DropDownListEdificio.ClearSelection()
                    DropDownListImpianti.Enabled = False
                    DropDownListImpianti.ClearSelection()
                    DropDownListLotto.Enabled = False
                    DropDownListLotto.ClearSelection()
                    DropDownListComplesso.Enabled = False
                    DropDownListComplesso.ClearSelection()
                    DropDownListAggregazione.Enabled = True
            End Select
        End If
        If DropDownListTipologiaDivisione.SelectedValue = 2 Or DropDownListTipologiaDivisione.SelectedValue = 3 Then
            DropDownListTabellaMillesimale.Enabled = True
            CaricaTabellaMillesimale(idMilles.Value)
        Else
            DropDownListTabellaMillesimale.Enabled = False
        End If
    End Sub
    Protected Sub DropDownListTipologiaDivisione_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListTipologiaDivisione.SelectedIndexChanged
        controlloDropDownList()
        idMilles.Value = "-1"
        CaricaTabellaMillesimale()
    End Sub
    Protected Sub ButtonAggiungiSpesa_Click(sender As Object, e As System.EventArgs) Handles ButtonAggiungiSpesa.Click
        'controllo sui campi inseriti
        If idSpesa.Value.ToString <> "-1" Then
            Try
                Dim errore As Boolean = False
                Dim tabellamill As String = "NULL"
                If Not IsNothing(DropDownListTabellaMillesimale) Then
                    If par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) <> -1 Then
                        tabellamill = DropDownListTabellaMillesimale.SelectedValue
                    End If
                End If
                Select Case DropDownListTipologiaDivisione.SelectedValue
                    Case "1"
                        'divisione per impianti
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListImpianti.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            par.cmd.CommandText = " UPDATE SISCOM_MI.PF_CONS_RIPARTIZIONI SET " _
                                & " ID_CRITERIO_RIPARTIZIONE=" & DropDownListCriterioRipartizione.SelectedValue & ", " _
                                & " ID_TIPOLOGIA_DIVISIONE=" & DropDownListTipologiaDivisione.SelectedValue & ", " _
                                & " ID_IMPIANTO=" & DropDownListImpianti.SelectedValue & "," _
                                & " ID_SCALA=NULL, " _
                                & " ID_EDIFICIO=NULL, " _
                                & " ID_LOTTO=NULL, " _
                                & " ID_COMPLESSO = NULL, " _
                                & " ID_TABELLA_MILLESIMALE = " & tabellamill & ", " _
                                & " IMPORTO=" & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & "," _
                                & " DESCRIZIONE='" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "', " _
                                & " ID_CATEGORIA='" & DropDownListCategoria.SelectedValue & "', " _
                                & " ID_TIPO_SPESA= " & DropDownListTipologiaSpesa.SelectedValue _
                                & " WHERE ID=" & idSpesa.Value.ToString
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                    Case "2"
                        'divisione per scale
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListEdificio.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListScala.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            par.cmd.CommandText = " UPDATE SISCOM_MI.PF_CONS_RIPARTIZIONI SET " _
                                & " ID_CRITERIO_RIPARTIZIONE=" & DropDownListCriterioRipartizione.SelectedValue & ", " _
                                & " ID_TIPOLOGIA_DIVISIONE=" & DropDownListTipologiaDivisione.SelectedValue & ", " _
                                & " ID_SCALA=" & DropDownListScala.SelectedValue & ", " _
                                & " ID_EDIFICIO=" & DropDownListEdificio.SelectedValue & "," _
                                & " ID_IMPIANTO=NULL, " _
                                & " ID_LOTTO=NULL, " _
                                & " ID_COMPLESSO = NULL, " _
                                & " ID_TABELLA_MILLESIMALE = " & tabellamill & ", " _
                                & " IMPORTO=" & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & "," _
                                & " DESCRIZIONE='" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "', " _
                                & " ID_CATEGORIA='" & DropDownListCategoria.SelectedValue & "', " _
                                & " ID_TIPO_SPESA= " & DropDownListTipologiaSpesa.SelectedValue _
                                & " WHERE ID=" & idSpesa.Value.ToString
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                    Case "3"
                        'divisione per edifici
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListEdificio.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            par.cmd.CommandText = " UPDATE SISCOM_MI.PF_CONS_RIPARTIZIONI SET " _
                                & " ID_CRITERIO_RIPARTIZIONE=" & DropDownListCriterioRipartizione.SelectedValue & ", " _
                                & " ID_TIPOLOGIA_DIVISIONE=" & DropDownListTipologiaDivisione.SelectedValue & ", " _
                                & " ID_EDIFICIO=" & DropDownListEdificio.SelectedValue & "," _
                                & " ID_SCALA=NULL, " _
                                & " ID_IMPIANTO=NULL, " _
                                & " ID_LOTTO=NULL, " _
                                & " ID_COMPLESSO = NULL, " _
                                & " ID_TABELLA_MILLESIMALE = " & tabellamill & ", " _
                                & " IMPORTO=" & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & "," _
                                & " DESCRIZIONE='" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "', " _
                                & " ID_CATEGORIA='" & DropDownListCategoria.SelectedValue & "', " _
                                & " ID_TIPO_SPESA= " & DropDownListTipologiaSpesa.SelectedValue _
                                & " WHERE ID=" & idSpesa.Value.ToString
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                    Case "4"
                        'divisione per intero patrimonio
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()

                            par.cmd.CommandText = " UPDATE SISCOM_MI.PF_CONS_RIPARTIZIONI SET " _
                                & " ID_CRITERIO_RIPARTIZIONE=" & DropDownListCriterioRipartizione.SelectedValue & ", " _
                                & " ID_TIPOLOGIA_DIVISIONE=" & DropDownListTipologiaDivisione.SelectedValue & ", " _
                                & " ID_IMPIANTO=NULL, " _
                                & " ID_SCALA=NULL, " _
                                & " ID_EDIFICIO=NULL, " _
                                & " ID_LOTTO=NULL, " _
                                & " ID_COMPLESSO = NULL, " _
                                & " ID_TABELLA_MILLESIMALE = " & tabellamill & ", " _
                                & " IMPORTO=" & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & "," _
                                & " DESCRIZIONE='" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "', " _
                                & " ID_CATEGORIA='" & DropDownListCategoria.SelectedValue & "', " _
                                & " ID_TIPO_SPESA= " & DropDownListTipologiaSpesa.SelectedValue _
                                & " WHERE ID=" & idSpesa.Value.ToString
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                    Case "5"
                        'divisione per lotti
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) <= 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListLotto.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            par.cmd.CommandText = " UPDATE SISCOM_MI.PF_CONS_RIPARTIZIONI SET " _
                                & " ID_CRITERIO_RIPARTIZIONE=" & DropDownListCriterioRipartizione.SelectedValue & ", " _
                                & " ID_TIPOLOGIA_DIVISIONE=" & DropDownListTipologiaDivisione.SelectedValue & ", " _
                                & " ID_LOTTO=" & DropDownListLotto.SelectedValue & ", " _
                                & " ID_IMPIANTO=NULL, " _
                                & " ID_SCALA=NULL, " _
                                & " ID_EDIFICIO=NULL, " _
                                & " ID_COMPLESSO = NULL, " _
                                & " ID_TABELLA_MILLESIMALE = " & tabellamill & ", " _
                                & " IMPORTO=" & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & "," _
                                & " DESCRIZIONE='" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "', " _
                                & " ID_CATEGORIA='" & DropDownListCategoria.SelectedValue & "', " _
                                & " ID_TIPO_SPESA= " & DropDownListTipologiaSpesa.SelectedValue _
                                & " WHERE ID=" & idSpesa.Value.ToString
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                    Case "6"
                        'divisione per complessi
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListComplesso.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            par.cmd.CommandText = " UPDATE SISCOM_MI.PF_CONS_RIPARTIZIONI SET " _
                                & " ID_CRITERIO_RIPARTIZIONE=" & DropDownListCriterioRipartizione.SelectedValue & ", " _
                                & " ID_TIPOLOGIA_DIVISIONE=" & DropDownListTipologiaDivisione.SelectedValue & ", " _
                                & " ID_LOTTO=NULL, " _
                                & " ID_COMPLESSO = " & DropDownListComplesso.SelectedValue & ", " _
                                & " ID_IMPIANTO=NULL, " _
                                & " ID_SCALA=NULL, " _
                                & " ID_EDIFICIO=NULL, " _
                                & " ID_TABELLA_MILLESIMALE = " & tabellamill & ", " _
                                & " IMPORTO=" & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & "," _
                                & " DESCRIZIONE='" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "', " _
                                & " ID_CATEGORIA='" & DropDownListCategoria.SelectedValue & "', " _
                                & " ID_TIPO_SPESA= " & DropDownListTipologiaSpesa.SelectedValue _
                                & " WHERE ID=" & idSpesa.Value.ToString
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                    Case "7"
                        'divisione per aggregazione
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListAggregazione.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            par.cmd.CommandText = " UPDATE SISCOM_MI.PF_CONS_RIPARTIZIONI SET " _
                                & " ID_CRITERIO_RIPARTIZIONE=" & DropDownListCriterioRipartizione.SelectedValue & ", " _
                                & " ID_TIPOLOGIA_DIVISIONE=" & DropDownListTipologiaDivisione.SelectedValue & ", " _
                                & " ID_SCALA=NULL, " _
                                & " ID_AGGREGAZIONE=" & DropDownListAggregazione.SelectedValue & ", " _
                                & " ID_EDIFICIO=NULL," _
                                & " ID_IMPIANTO=NULL, " _
                                & " ID_LOTTO=NULL, " _
                                & " ID_COMPLESSO = NULL, " _
                                & " IMPORTO=" & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & "," _
                                & " DESCRIZIONE='" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "', " _
                                & " ID_CATEGORIA='" & DropDownListCategoria.SelectedValue & "', " _
                                & " ID_TIPO_SPESA= " & DropDownListTipologiaSpesa.SelectedValue _
                                & " WHERE ID=" & idSpesa.Value.ToString
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                End Select
                MemorizzaAttributi()
                CaricaAttributi()
            Catch ex As Exception
                LabelErrore.Text = "Errore durante la modifica della spesa!"
            End Try
            If LabelErrore.Text = "" Then
                RadWindowManager1.RadAlert("Spesa modificata correttamente!", 300, 150, "Attenzione", "function f(sender,args){CloseAndNextJS(document.getElementById('HFBtnToClick').value);}", "null")

            End If
        Else
            Try
                Dim errore As Boolean = False
                Dim tabellamill As String = "NULL"
                If Not IsNothing(DropDownListTabellaMillesimale) Then
                    If par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) <> -1 Then
                        tabellamill = DropDownListTabellaMillesimale.SelectedValue
                    End If
                End If
                Select Case DropDownListTipologiaDivisione.SelectedValue
                    Case "1"
                        'divisione per impianti
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListImpianti.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_CONS_RIPARTIZIONI (ID,ID_PF,ID_VOCE_SPESA,ID_CRITERIO_RIPARTIZIONE,FL_MANUALE,ID_TIPOLOGIA_DIVISIONE,ID_COMPLESSO,ID_LOTTO,ID_IMPIANTO,ID_SCALA,ID_EDIFICIO,IMPORTO,DESCRIZIONE,ID_TIPO_SPESA,ID_CATEGORIA,ID_TABELLA_MILLESIMALE) " _
                                & " VALUES (SISCOM_MI.SEQ_PF_CONS_RIPARTIZIONI.NEXTVAL," & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & ",NULL," & DropDownListCriterioRipartizione.SelectedValue & ",1," & DropDownListTipologiaDivisione.SelectedValue & ",NULL,NULL," & DropDownListImpianti.SelectedValue & ",NULL,NULL," & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & ",'" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "'," & DropDownListTipologiaSpesa.SelectedValue & "," & DropDownListCategoria.SelectedValue & "," & tabellamill & ") "
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                    Case "2"
                        'divisione per scale
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListEdificio.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListScala.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_CONS_RIPARTIZIONI (ID,ID_PF,ID_VOCE_SPESA,ID_CRITERIO_RIPARTIZIONE,FL_MANUALE,ID_TIPOLOGIA_DIVISIONE,ID_COMPLESSO,ID_LOTTO,ID_IMPIANTO,ID_SCALA,ID_EDIFICIO,IMPORTO,DESCRIZIONE,ID_TIPO_SPESA,ID_cATEGORIA,ID_TABELLA_MILLESIMALE) " _
                                & " VALUES (SISCOM_MI.SEQ_PF_CONS_RIPARTIZIONI.NEXTVAL," & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & ",NULL," & DropDownListCriterioRipartizione.SelectedValue & ",1," & DropDownListTipologiaDivisione.SelectedValue & ",NULL,NULL,NULL," & DropDownListScala.SelectedValue & "," & DropDownListEdificio.SelectedValue & "," & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & ",'" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "'," & DropDownListTipologiaSpesa.SelectedValue & "," & DropDownListCategoria.SelectedValue & "," & tabellamill & ") "
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                    Case "3"
                        'divisione per edifici
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListEdificio.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_CONS_RIPARTIZIONI (ID,ID_PF,ID_VOCE_SPESA,ID_CRITERIO_RIPARTIZIONE,FL_MANUALE,ID_TIPOLOGIA_DIVISIONE,ID_COMPLESSO,ID_LOTTO,ID_IMPIANTO,ID_SCALA,ID_EDIFICIO,IMPORTO,DESCRIZIONE,ID_TIPO_SPESA,ID_cATEGORIA,ID_TABELLA_MILLESIMALE) " _
                                & " VALUES (SISCOM_MI.SEQ_PF_CONS_RIPARTIZIONI.NEXTVAL," & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & ",NULL," & DropDownListCriterioRipartizione.SelectedValue & ",1," & DropDownListTipologiaDivisione.SelectedValue & ",NULL,NULL,NULL,NULL," & DropDownListEdificio.SelectedValue & "," & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & ",'" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "'," & DropDownListTipologiaSpesa.SelectedValue & "," & DropDownListCategoria.SelectedValue & "," & tabellamill & ") "
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                    Case "4"
                        'divisione per intero patrimonio
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_CONS_RIPARTIZIONI (ID,ID_PF,ID_VOCE_SPESA,ID_CRITERIO_RIPARTIZIONE,FL_MANUALE,ID_TIPOLOGIA_DIVISIONE,ID_COMPLESSO,ID_LOTTO,ID_IMPIANTO,ID_SCALA,ID_EDIFICIO,IMPORTO,DESCRIZIONE,ID_TIPO_SPESA,ID_cATEGORIA,ID_TABELLA_MILLESIMALE) " _
                                & " VALUES (SISCOM_MI.SEQ_PF_CONS_RIPARTIZIONI.NEXTVAL," & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & ",NULL," & DropDownListCriterioRipartizione.SelectedValue & ",1," & DropDownListTipologiaDivisione.SelectedValue & ",NULL,NULL,NULL,NULL,NULL," & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & ",'" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "'," & DropDownListTipologiaSpesa.SelectedValue & "," & DropDownListCategoria.SelectedValue & "," & tabellamill & ") "
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                    Case "5"
                        'divisione per lotti
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListLotto.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_CONS_RIPARTIZIONI (ID,ID_PF,ID_VOCE_SPESA,ID_CRITERIO_RIPARTIZIONE,FL_MANUALE,ID_TIPOLOGIA_DIVISIONE,ID_COMPLESSO,ID_LOTTO,ID_IMPIANTO,ID_SCALA,ID_EDIFICIO,IMPORTO,DESCRIZIONE,ID_TIPO_SPESA,ID_cATEGORIA,ID_TABELLA_MILLESIMALE) " _
                                & " VALUES (SISCOM_MI.SEQ_PF_CONS_RIPARTIZIONI.NEXTVAL," & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & ",NULL," & DropDownListCriterioRipartizione.SelectedValue & ",1," & DropDownListTipologiaDivisione.SelectedValue & ",NULL," & DropDownListLotto.SelectedValue & ",NULL,NULL,NULL," & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & ",'" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "'," & DropDownListTipologiaSpesa.SelectedValue & "," & DropDownListCategoria.SelectedValue & "," & tabellamill & ") "
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                    Case "6"
                        'divisione per complessi
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListComplesso.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_CONS_RIPARTIZIONI (ID,ID_PF,ID_VOCE_SPESA,ID_CRITERIO_RIPARTIZIONE,FL_MANUALE,ID_TIPOLOGIA_DIVISIONE,ID_LOTTO,ID_COMPLESSO,ID_IMPIANTO,ID_SCALA,ID_EDIFICIO,IMPORTO,DESCRIZIONE,ID_TIPO_SPESA,ID_cATEGORIA,ID_TABELLA_MILLESIMALE) " _
                                & " VALUES (SISCOM_MI.SEQ_PF_CONS_RIPARTIZIONI.NEXTVAL," & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & ",NULL," & DropDownListCriterioRipartizione.SelectedValue & ",1," & DropDownListTipologiaDivisione.SelectedValue & ",NULL," & DropDownListComplesso.SelectedValue & ",NULL,NULL,NULL," & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & ",'" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "'," & DropDownListTipologiaSpesa.SelectedValue & "," & DropDownListCategoria.SelectedValue & "," & tabellamill & ") "
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi()
                        End If
                    Case "7"
                        'divisione per aggregazione
                        If TextBoxDescrizioneSpesa.Text = "" Then
                            errore = True
                        End If
                        If TextBoxImporto.Text <> "" Then
                            If CDec(TextBoxImporto.Text) = 0 Then
                                errore = True
                            End If
                        Else
                            errore = True
                        End If
                        If DropDownListAggregazione.SelectedValue = "-1" Then
                            errore = True
                        End If
                        If DropDownListTabellaMillesimale.Enabled = True And DropDownListCriterioRipartizione.SelectedValue = "2" And par.IfEmpty(DropDownListTabellaMillesimale.SelectedValue, -1) < 1 Then
                            errore = True
                        End If
                        If errore = True Then
                            LabelErrore.Text = "Compilare correttamente i campi obbligatori!"
                        Else
                            LabelErrore.Text = ""
                            connData.apri()
                            'par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_CONS_RIPARTIZIONI (ID,ID_PF,ID_VOCE_SPESA,ID_CRITERIO_RIPARTIZIONE,FL_MANUALE,ID_TIPOLOGIA_DIVISIONE,ID_COMPLESSO,ID_LOTTO,ID_IMPIANTO,ID_AGGREGAZIONE,ID_EDIFICIO,IMPORTO,DESCRIZIONE,ID_TIPO_SPESA,ID_cATEGORIA,id_tabella_millesimale) " _
                            '& " VALUES (SISCOM_MI.SEQ_PF_CONS_RIPARTIZIONI.NEXTVAL," & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & ",NULL," & DropDownListCriterioRipartizione.SelectedValue & ",1," & DropDownListTipologiaDivisione.SelectedValue & ",NULL,NULL,NULL," & DropDownListAggregazione.SelectedValue & ",NULL," & par.VirgoleInPunti(CDec(TextBoxImporto.Text)) & ",'" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "'," & DropDownListTipologiaSpesa.SelectedValue & "," & DropDownListCategoria.SelectedValue & ",NULL) "
                            'par.cmd.ExecuteNonQuery()
                            Dim idAggregazione = DropDownListAggregazione.SelectedValue
                            par.cmd.CommandText = "SELECT count(*) FROM SISCOM_MI.AGGREGAZIONE_DETT WHERE ID_AGGREGAZIONE=" & idAggregazione
                            Dim count As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.AGGREGAZIONE_DETT WHERE ID_AGGREGAZIONE=" & idAggregazione
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            'IMPORTO TOTALE GESTITO PER NON PERDERE CENTESIMI
                            Dim importo As Decimal = 0
                            Dim importoTotale As Decimal = 0
                            Dim i As Integer = 0
                            While lettore.Read
                                i += 1
                                importo = Math.Round(CDec(TextBoxImporto.Text) * par.IfNull(lettore("PERCENTUALE"), 0) / 100, 2)
                                If i = count Then
                                    importo = CDec(TextBoxImporto.Text) - importoTotale
                                End If
                                importoTotale += importo
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_CONS_RIPARTIZIONI (ID,ID_PF,ID_VOCE_SPESA,ID_CRITERIO_RIPARTIZIONE,FL_MANUALE,ID_TIPOLOGIA_DIVISIONE,ID_COMPLESSO,ID_LOTTO,ID_IMPIANTO,ID_AGGREGAZIONE,ID_EDIFICIO,IMPORTO,DESCRIZIONE,ID_TIPO_SPESA,ID_cATEGORIA,id_tabella_millesimale) " _
                                & " VALUES (SISCOM_MI.SEQ_PF_CONS_RIPARTIZIONI.NEXTVAL," & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & ",NULL," & DropDownListCriterioRipartizione.SelectedValue & ",1," & DropDownListTipologiaDivisione.SelectedValue & ",NULL,NULL,NULL," & DropDownListAggregazione.SelectedValue & ",NULL," & par.VirgoleInPunti(importo) & ",'" & Replace(TextBoxDescrizioneSpesa.Text, "'", "''") & "'," & DropDownListTipologiaSpesa.SelectedValue & "," & DropDownListCategoria.SelectedValue & ", null) "
                                par.cmd.ExecuteNonQuery()
                            End While


                            lettore.Close()
                            connData.chiudi()
                        End If
                End Select
                connData.apri()
                Dim tempo As String = Format(Now, "yyyyMMddHHmmss")
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG (ID_PF,ID_OPERATORE, DATA_ORA, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) " _
                          & " VALUES (" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," & Session.Item("ID_OPERATORE") & ", '" & tempo & "', 'INSERIMENTO VOCE PROSPETTO:" & par.PulisciStrSql(TextBoxDescrizioneSpesa.Text.ToUpper) & " '," _
                          & "'- - -', '- - - ', 1)"
                par.cmd.ExecuteNonQuery()
                connData.chiudi()
            Catch ex As Exception
                LabelErrore.Text = "Errore durante l'inserimento della spesa!"
            End Try
            If LabelErrore.Text = "" Then
                RadWindowManager1.RadAlert("Spesa aggiunta correttamente!", 300, 150, "Attenzione", "function f(sender,args){CloseAndRefresh('btnRefreshGrid');}", "null")

            End If
        End If
    End Sub
    Private Sub caricaTipologieSpesa()
        par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_CARATURE ORDER BY 2 ASC", DropDownListTipologiaSpesa, "ID", "DESCRIZIONE", True)
    End Sub
    Private Sub caricaMaschera()
        If idSpesa.Value.ToString <> "-1" Then
            par.cmd.CommandText = " SELECT  " _
                & " DECODE(FL_MANUALE,0,PF_VOCI_IMPORTO.DESCRIZIONE,1,INITCAP(PF_CONS_RIPARTIZIONI.DESCRIZIONE)) AS VOCE_SPESA, " _
                & " INITCAP(CRITERI_RIPARTIZIONE.DESCRIZIONE) AS CRITERIO_RIPARTIZIONE, " _
                & " DECODE(PF_CONS_RIPARTIZIONI.FL_MANUALE,1,'Sì',0,'No') AS FL_MANUALE, " _
                & " INITCAP(TIPOLOGIA_DIVISIONE.DESCRIZIONE) AS TIPOLOGIA_DIVISIONE, " _
                & " LOTTI.DESCRIZIONE AS LOTTO, " _
                & " IMPIANTI.DESCRIZIONE AS IMPIANTO, " _
                & " SCALE_EDIFICI.DESCRIZIONE AS SCALA, " _
                & " EDIFICI.DENOMINAZIONE AS EDIFICIO, " _
                & " TRIM(TO_CHAR(PF_CONS_RIPARTIZIONI.IMPORTO,'999G999G999G990D99')) AS IMPORTO, " _
                & " PF_CONS_RIPARTIZIONI.ID AS ID,'' AS ELIMINA, " _
                & " PF_CONS_RIPARTIZIONI.ID_TIPOLOGIA_DIVISIONE, " _
                & " PF_CONS_RIPARTIZIONI.ID_CRITERIO_RIPARTIZIONE, " _
                & " LOTTI.ID AS ID_LOTTO, " _
                & " EDIFICI.ID AS ID_EDIFICIO, " _
                & " SCALE_EDIFICI.ID AS ID_SCALA, " _
                & " IMPIANTI.ID AS ID_IMPIANTO, AGGREGAZIONE.ID AS ID_AGGREGAZIONE, " _
                & " COMPLESSI_IMMOBILIARI.ID AS ID_COMPLESSO, ID_TABELLA_MILLESIMALE, " _
                & " PF_CONS_RIPARTIZIONI.ID_CATEGORIA,PF_CONS_RIPARTIZIONI.FL_MANUALE AS MANUALE,PF_CONS_RIPARTIZIONI.ID_TIPO_SPESA " _
                & " FROM SISCOM_MI.PF_CONS_RIPARTIZIONI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.CRITERI_RIPARTIZIONE,SISCOM_MI.TIPOLOGIA_DIVISIONE, " _
                & "SISCOM_MI.LOTTI,SISCOM_MI.IMPIANTI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.AGGREGAZIONE " _
                & " WHERE PF_VOCI_IMPORTO.ID(+)=PF_CONS_RIPARTIZIONI.ID_VOCE_SPESA " _
                & " AND CRITERI_RIPARTIZIONE.ID=PF_CONS_RIPARTIZIONI.ID_CRITERIO_RIPARTIZIONE " _
                & " AND TIPOLOGIA_DIVISIONE.ID=PF_CONS_RIPARTIZIONI.ID_TIPOLOGIA_DIVISIONE " _
                & " AND LOTTI.ID(+)=PF_CONS_RIPARTIZIONI.ID_LOTTO " _
                & " AND IMPIANTI.ID(+)=PF_CONS_RIPARTIZIONI.ID_IMPIANTO " _
                & " AND SCALE_EDIFICI.ID(+)=PF_CONS_RIPARTIZIONI.ID_SCALA " _
                & " AND COMPLESSI_IMMOBILIARI.ID(+) = PF_CONS_RIPARTIZIONI.ID_COMPLESSO " _
                & " AND EDIFICI.ID(+)=PF_CONS_RIPARTIZIONI.ID_EDIFICIO " _
                & " AND AGGREGAZIONE.ID(+)=PF_CONS_RIPARTIZIONI.ID_AGGREGAZIONE " _
                & " AND PF_CONS_RIPARTIZIONI.ID=" & idSpesa.Value.ToString _
                & " ORDER BY 4,2"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim check As Boolean = False
            Dim idLotto As String = "-1"
            Dim idEdificio As String = "-1"
            Dim idScala As String = "-1"
            Dim idImpianto As String = "-1"
            Dim idComplesso As String = "-1"
            Dim idAggregazione As String = "-1"
            Dim idMillesimale As String = "-1"
            Dim flManuale As String = "0"
            If lettore.Read Then
                TextBoxDescrizioneSpesa.Text = par.IfNull(lettore("VOCE_SPESA"), "")
                TextBoxImporto.Text = par.IfNull(lettore("IMPORTO"), "")
                DropDownListTipologiaDivisione.SelectedValue = par.IfNull(lettore("ID_TIPOLOGIA_DIVISIONE"), 1)
                DropDownListCriterioRipartizione.SelectedValue = par.IfNull(lettore("ID_CRITERIO_RIPARTIZIONE"), 1)
                DropDownListCategoria.SelectedValue = par.IfNull(lettore("ID_CATEGORIA"), 22)
                DropDownListTipologiaSpesa.SelectedValue = par.IfNull(lettore("ID_TIPO_SPESA"), 1)
                idLotto = par.IfNull(lettore("ID_LOTTO"), "-1")
                idEdificio = par.IfNull(lettore("ID_EDIFICIO"), "-1")
                idScala = par.IfNull(lettore("ID_SCALA"), "-1")
                idImpianto = par.IfNull(lettore("ID_IMPIANTO"), "-1")
                idComplesso = par.IfNull(lettore("ID_COMPLESSO"), "-1")
                idAggregazione = par.IfNull(lettore("ID_AGGREGAZIONE"), "-1")
                idMillesimale = par.IfNull(lettore("ID_TABELLA_MILLESIMALE"), "-1")
                idMilles.Value = idMillesimale
                flManuale = par.IfNull(lettore("MANUALE"), "")
                check = True
            End If
            lettore.Close()
            If flManuale = "0" Then
                DropDownListCategoria.Enabled = False
            End If
            If check Then
                caricaLotto(idLotto)
                caricaEdifici(idEdificio)
                caricaScale(idScala)
                caricaImpianti(idImpianto)
                caricaComplessi(idComplesso)
                caricaAggregazioni(idAggregazione)
                CaricaTabellaMillesimale(idMilles.Value)
                ButtonAggiungiSpesa.Text = "Modifica"
            Else
                caricaLotto()
                caricaEdifici()
                caricaScale()
                caricaImpianti()
                caricaComplessi()
                caricaAggregazioni()
                CaricaTabellaMillesimale()
            End If
        Else
            caricaLotto()
            caricaEdifici()
            caricaComplessi()
            caricaScale()
            caricaImpianti()
            caricaAggregazioni()
            CaricaTabellaMillesimale()
            ButtonAggiungiSpesa.Text = "Aggiungi"
            DropDownListEdificio.Enabled = False
            DropDownListScala.Enabled = False
            DropDownListImpianti.Enabled = False
            DropDownListComplesso.Enabled = False
            DropDownListAggregazione.Enabled = False
        End If
    End Sub

    Protected Sub DropDownListTipologiaSpesa_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles DropDownListTipologiaSpesa.SelectedIndexChanged
        par.caricaComboTelerik("SELECT ID,GRUPPO||'-'||DESCRIZIONE AS DESCRIZIONE FROM SISCOM_MI.PF_CATEGORIE WHERE ID_GRUPPO = " & DropDownListTipologiaSpesa.SelectedValue, DropDownListCategoria, "ID", "DESCRIZIONE", True)
    End Sub
    Protected Sub CaricaTabellaMillesimale(Optional ByVal idMillesimale As String = "-1")
        Select Case DropDownListCriterioRipartizione.SelectedValue

            Case "2"
                Select Case DropDownListTipologiaDivisione.SelectedValue
                    Case "1"
                        '1	PER IMPIANTI
                        Dim impianto As Integer = -1
                        If DropDownListImpianti.SelectedValue <> "" Then
                            impianto = DropDownListImpianti.SelectedValue
                        End If
                        par.caricaComboTelerik("SELECT ID,DESCRIZIONE || ' - ' || DESCRIZIONE_TABELLA AS DESCRIZIONE FROM SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_EDIFICIO IN (SELECT ID_EDIFICIO FROM SISCOM_MI.IMPIANTI WHERE ID=" & impianto & ")", DropDownListTabellaMillesimale, "ID", "DESCRIZIONE", True, "-1", "")
                        DropDownListTabellaMillesimale.Enabled = True
                        DropDownListTabellaMillesimale.SelectedValue = idMillesimale
                    Case "2"
                        '2	PER SCALE
                        Dim scala As Integer = -1
                        If DropDownListScala.SelectedValue <> "" Then
                            scala = DropDownListScala.SelectedValue
                        End If
                        par.caricaComboTelerik("SELECT ID,DESCRIZIONE || ' - ' || DESCRIZIONE_TABELLA AS DESCRIZIONE FROM SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_EDIFICIO IN (SELECT ID_EDIFICIO FROM SISCOM_MI.SCALE_EDIFICI WHERE ID=" & scala & ")", DropDownListTabellaMillesimale, "ID", "DESCRIZIONE", True, "-1", "")
                        DropDownListTabellaMillesimale.Enabled = True
                        DropDownListTabellaMillesimale.SelectedValue = idMillesimale
                    Case "3"
                        '3	PER EDIFICI
                        Dim edificio As Integer = -1
                        If DropDownListEdificio.SelectedValue <> "" Then
                            edificio = DropDownListEdificio.SelectedValue
                        End If
                        par.caricaComboTelerik("SELECT ID,DESCRIZIONE || ' - ' || DESCRIZIONE_TABELLA AS DESCRIZIONE FROM SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_EDIFICIO=" & edificio, DropDownListTabellaMillesimale, "ID", "DESCRIZIONE", True, "-1", "")
                        DropDownListTabellaMillesimale.Enabled = True
                        DropDownListTabellaMillesimale.SelectedValue = idMillesimale
                    Case "4"
                        '4	PER INTERO PATRIMONIO
                        par.caricaComboTelerik("SELECT ID,DESCRIZIONE || ' - ' || DESCRIZIONE_TABELLA AS DESCRIZIONE FROM SISCOM_MI.TABELLE_MILLESIMALI", DropDownListTabellaMillesimale, "ID", "DESCRIZIONE", True, "-1", "")
                        DropDownListTabellaMillesimale.Enabled = True
                        DropDownListTabellaMillesimale.SelectedValue = idMillesimale
                    Case "5"
                        '5	PER LOTTI
                        Dim lotto As Integer = -1
                        If DropDownListLotto.SelectedValue <> "" Then
                            lotto = DropDownListLotto.SelectedValue
                        End If
                        par.caricaComboTelerik("SELECT ID,DESCRIZIONE || ' - ' || DESCRIZIONE_TABELLA AS DESCRIZIONE FROM SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_EDIFICIO IN (SELECT ID_eDIFICIO FROM SISCOM_MI.LOTTI_PATRIMONIO WHERE ID_LOTTO=" & lotto & ")", DropDownListTabellaMillesimale, "ID", "DESCRIZIONE", True, "-1", "")
                        DropDownListTabellaMillesimale.Enabled = True
                        DropDownListTabellaMillesimale.SelectedValue = idMillesimale
                    Case "6"
                        '6	PER COMPLESSI
                        Dim complesso As Integer = -1
                        If DropDownListComplesso.SelectedValue <> "" Then
                            complesso = DropDownListComplesso.SelectedValue
                        End If
                        par.caricaComboTelerik("SELECT ID,DESCRIZIONE || ' - ' || DESCRIZIONE_TABELLA AS DESCRIZIONE FROM SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO=" & complesso & ")", DropDownListTabellaMillesimale, "ID", "DESCRIZIONE", True, "-1", "")
                        DropDownListTabellaMillesimale.Enabled = True
                        DropDownListTabellaMillesimale.SelectedValue = idMillesimale
                    Case "7"
                        ''7	PER AGGREGAZIONE
                        'par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM TABELLE_MILLESIMALI WHERE ID= SELECT ID_TABELLA FROM SISCOM_MI.AGGREGAZIONE_POD_dETT WHERE ID_AGGREGAZIONE=" & DropDownListComplesso.SelectedValue & ")", DropDownListTabellaMillesimale, "ID", "DESCRIZIONE", True)
                        DropDownListTabellaMillesimale.Enabled = False
                End Select
            Case Else
                DropDownListTabellaMillesimale.Enabled = False
        End Select
    End Sub
    Protected Sub DropDownListCriterioRipartizione_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListCriterioRipartizione.SelectedIndexChanged
        idMilles.Value = "-1"
        CaricaTabellaMillesimale()
    End Sub
    Protected Sub DropDownListComplesso_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListComplesso.SelectedIndexChanged
        idMilles.Value = "-1"
        CaricaTabellaMillesimale()
    End Sub
    Protected Sub DropDownListLotto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListLotto.SelectedIndexChanged
        idMilles.Value = "-1"
        CaricaTabellaMillesimale()
    End Sub
    Protected Sub DropDownListScala_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListScala.SelectedIndexChanged
        idMilles.Value = "-1"
        CaricaTabellaMillesimale()
    End Sub
    Protected Sub DropDownListImpianti_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListImpianti.SelectedIndexChanged
        idMilles.Value = "-1"
        CaricaTabellaMillesimale()
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        controlloDropDownList()
    End Sub

    Private Sub CaricaAttributi()

        'VENGONO CARICATI GLI ATTRIBUTI "CORRENTE" (VALORE CORRENTE) E "NOME" (NOME DEL CAMPO)
        'MENTRE IL VALORE CORRENTE VIENE CARICATO AUTOMATCAMENTE (SOLO PER CHECKBOX, TEXTBOX E DROPDOWNLIST)
        'IL VALORE DELL'ATTRIBUTO "NOME" VIENE CARICATO MANUALMENTE, IN MODO DA INSERIRE DEL TESTO PIU' 
        'SIGNIFICATIVO E NON SEMPLICEMENTE LA PROPRIETA' TEXT

        Dim CTRL As Control = Nothing
        Dim mpContentPlaceHolder As ContentPlaceHolder
        mpContentPlaceHolder = CType(Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)

        For Each CTRL In mpContentPlaceHolder.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("CORRENTE", UCase(DirectCast(CTRL, TextBox).Text))
            End If
            If TypeOf CTRL Is RadComboBox Then
                If Not IsNothing(DirectCast(CTRL, RadComboBox).SelectedItem) Then
                    DirectCast(CTRL, RadComboBox).Attributes.Add("CORRENTE", UCase(DirectCast(CTRL, RadComboBox).SelectedItem.Text))
                End If

            End If
        Next

        'attributi nome da memorizzare
        DropDownListTipologiaSpesa.Attributes.Add("NOME", "TIPOLOGIA SPESA")
        TextBoxDescrizioneSpesa.Attributes.Add("NOME", "DESCRIZIONE SPESA")
        DropDownListCategoria.Attributes.Add("NOME", "CATEGORIA")
        TextBoxImporto.Attributes.Add("NOME", "IMPORTO")
        DropDownListTipologiaDivisione.Attributes.Add("NOME", "TIPOLOGIA DI DIVISIONE")
        DropDownListCriterioRipartizione.Attributes.Add("NOME", "CRITERIO RIPARTIZIONE")
        DropDownListComplesso.Attributes.Add("NOME", "COMPLESSO")
        DropDownListLotto.Attributes.Add("NOME", "LOTTO")
        DropDownListEdificio.Attributes.Add("NOME", "EDIFICIO")
        DropDownListScala.Attributes.Add("NOME", "SCALA")
        DropDownListImpianti.Attributes.Add("NOME", "IMPIANTI")
        DropDownListAggregazione.Attributes.Add("NOME", "AGGREGAZIONE")
        DropDownListTabellaMillesimale.Attributes.Add("NOME", "TABELLA MILLESIMALE")

    End Sub

    Private Sub MemorizzaAttributi()
        Dim ELENCOERRORI As String = ""
        Try
            Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
            Dim CTRL As Control = Nothing
            Dim mpContentPlaceHolder As ContentPlaceHolder
            mpContentPlaceHolder = CType(Master.FindControl("ContentPlaceHolder1"), ContentPlaceHolder)

            For Each CTRL In mpContentPlaceHolder.Controls
                If TypeOf CTRL Is TextBox Then
                    If DirectCast(CTRL, TextBox).Text.ToUpper <> DirectCast(CTRL, TextBox).Attributes("CORRENTE").ToUpper.ToString Then
                        If ScriviLogSpese(DirectCast(CTRL, TextBox).Attributes("NOME").ToUpper.ToString & " DELLA VOCE PROSPETTO: " & par.PulisciStrSql(TextBoxDescrizioneSpesa.Text.ToUpper), DirectCast(CTRL, TextBox).Attributes("CORRENTE").ToUpper.ToString, DirectCast(CTRL, TextBox).Text.ToUpper, 2, Tempo) = False Then
                            ELENCOERRORI = ELENCOERRORI & DirectCast(CTRL, TextBox).Attributes("NOME").ToUpper.ToString & "<br/>"
                        End If
                    End If
                End If
                If TypeOf CTRL Is RadComboBox Then
                    If Not IsNothing(DirectCast(CTRL, RadComboBox).SelectedItem) Then
                        If DirectCast(CTRL, RadComboBox).SelectedItem.Text.ToUpper <> DirectCast(CTRL, RadComboBox).Attributes("CORRENTE").ToUpper.ToString Then
                            If ScriviLogSpese(DirectCast(CTRL, RadComboBox).Attributes("NOME").ToUpper.ToString & " DELLA VOCE PROSPETTO: " & par.PulisciStrSql(TextBoxDescrizioneSpesa.Text.ToUpper), DirectCast(CTRL, RadComboBox).Attributes("CORRENTE").ToUpper.ToString, DirectCast(CTRL, RadComboBox).SelectedItem.Text.ToUpper, 2, Tempo) = False Then
                                ELENCOERRORI = ELENCOERRORI & DirectCast(CTRL, RadComboBox).Attributes("NOME").ToUpper.ToString & "<br/>"
                            End If
                        End If
                    End If

                End If
            Next

        Catch ex As Exception
            LabelErrore.Text = "ERRORE MEMORIZZAZIONE ATTRIBUTI - " & ELENCOERRORI & ex.Message
        End Try
    End Sub

    Private Function ScriviLogSpese(ByVal CAMPO As String, ByVal VAL_PRECEDENTE As String, ByVal VAL_IMPOSTATO As String, OPERAZIONE As Integer, tempo As String) As Boolean
        Try
            Dim aperto As Boolean = False
            If par.cmd.Connection.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
                aperto = True
            End If
            par.cmd.CommandText = "Insert into SISCOM_MI.SPESE_REVER_LOG (ID_PF,ID_OPERATORE, DATA_ORA, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) " _
                                & " Values (" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," & Session.Item("ID_OPERATORE") & ", '" & tempo & "', '" & par.PulisciStrSql(CAMPO) _
                                & "', '" & par.PulisciStrSql(VAL_PRECEDENTE) & "', '" & par.PulisciStrSql(VAL_IMPOSTATO) & "', " & OPERAZIONE & ")"
            par.cmd.ExecuteNonQuery()
            If aperto = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
            End If
            ScriviLogSpese = True
        Catch ex As Exception
            par.OracleConn.Close()
            ScriviLogSpese = False
        End Try
    End Function

    Protected Sub DropDownListCategoria_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles DropDownListCategoria.SelectedIndexChanged
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT ID_TIPO_DIVISIONE FROM SISCOM_MI.PF_CATEGORIE WHERE PF_CATEGORIE.ID=" & DropDownListCategoria.SelectedValue
            Dim idTipo As Int64 = par.IfNull(par.cmd.ExecuteScalar, 0)
            DropDownListTipologiaDivisione.SelectedValue = idTipo
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
        End Try
    End Sub
End Class