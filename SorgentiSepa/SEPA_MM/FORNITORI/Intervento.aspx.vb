Imports Telerik.Web.UI
Imports System.IO

Partial Class FORNITORI_Intervento
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    'Protected Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs)
    '    If e.Argument = "InitialPageLoad" Then

    '        Dim i As Long = 0
    '        For i = 0 To 10000

    '        Next
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    '        Panel1.Visible = True
    '    End If
    'End Sub

    Private Function CaricaInterventi(ByVal vIdManutenzione As Long) As String
        Try
            CaricaInterventi = ""
            Dim sSQL_DettaglioIMPIANTO As String = "(CASE IMPIANTI.COD_TIPOLOGIA " _
                                    & " WHEN 'AN' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'CF' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'CI' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'EL' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'GA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'ID' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'ME' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'SO' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||" _
                                            & "(select  CASE when SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO is null THEN " _
                                                            & " 'Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA " _
                                                    & " ELSE 'Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO END " _
                                            & " from SISCOM_MI.I_SOLLEVAMENTO where ID=IMPIANTI.ID) " _
                                    & " WHEN 'TA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TE' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TR' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TU' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TV' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                & " ELSE '' " _
                                & " END) as DETTAGLIO "
            sStrSql = " select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                   & sSQL_DettaglioIMPIANTO & ",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO,(SELECT DESCRIZIONE||' '||CIVICO||' '||CAP||' '||LOCALITA FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=IMPIANTI.ID_COMPLESSO AND INDIRIZZI.ID=COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO) AS INDIRIZZO_BENE " _
                   & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.IMPIANTI" _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO is not null and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                   & "       COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO,(DESCRIZIONE||' '||CIVICO||' '||CAP||' '||LOCALITA) AS INDIRIZZO_BENE " _
                   & " from SISCOM_MI.INDIRIZZI,SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='COMPLESSO' and  SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) AND INDIRIZZI.ID(+)=COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                  & "        (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO,(DESCRIZIONE||' '||CIVICO||' '||CAP||' '||LOCALITA) AS INDIRIZZO_BENE " _
                  & " from SISCOM_MI.INDIRIZZI,SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI " _
                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                  & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='EDIFICIO' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) AND INDIRIZZI.ID(+)=EDIFICI.ID_INDIRIZZO_PRINCIPALE " _
            & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                  & "       (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||(select descrizione from siscom_mi.scale_edifici where siscom_mi.scale_edifici.id=unita_immobiliari.id_scala)||' - -Interno '||SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO,(DESCRIZIONE||' '||CIVICO||' '||CAP||' '||LOCALITA) AS INDIRIZZO_BENE " _
                  & " from  SISCOM_MI.INDIRIZZI,SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INTESTATARI_UI " _
                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                  & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA IMMOBILIARE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID (+) and	SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID  (+)  and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) AND INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.ID_INDIRIZZO " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                   & "      (SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO,'' AS INDIRIZZO_BENE " _
                   & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE  " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA COMUNE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE=SISCOM_MI.UNITA_COMUNI.ID (+) and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) "

        Catch ex As Exception
            CaricaInterventi = "Errore " & ex.Message
        End Try

    End Function

    Private Sub CaricaDati()
        Try
            RadTabStrip1.Tabs(0).Visible = False
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri()
            If sIndice = "" Then sIndice = 0
            par.cmd.CommandText = "select segnalazioni_fornitori.*,TAB_STATI_SEGNALAZIONI_FO.descrizione as statoI from siscom_mi.segnalazioni_fornitori,SISCOM_MI.TAB_STATI_SEGNALAZIONI_FO where TAB_STATI_SEGNALAZIONI_FO.ID=SEGNALAZIONI_FORNITORI.ID_STATO AND segnalazioni_fornitori.id=" & sIndice
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblStatoIntervento.Text = par.IfNull(myReader("statoI"), "--")
                sStatoIntervento.Value = par.IfNull(myReader("id_stato"), 0)
                sRichiestaContab.Value = par.IfNull(myReader("FL_PR_CONTAB"), "0")
                If par.IfNull(myReader("data_fine_intervento"), "") <> "" Then
                    txtDataFineLavori.SelectedDate = par.FormattaData(par.IfNull(myReader("data_fine_intervento"), ""))
                End If

                If par.IfNull(myReader("fl_rdo"), "0") = "1" Then
                    lblRDORichiesto.Visible = True
                    lblRDORichiesto.Text = "PREVENTIVO RICHIESTO!"
                    RadGridPreventivi.Enabled = True
                    lblStatoPR.Visible = True
                    par.cmd.CommandText = "select SEGNALAZIONI_FO_PREV_STATI.ID,SEGNALAZIONI_FO_PREV_STATI.descrizione as stato_pr from siscom_mi.SEGNALAZIONI_FO_PREVENTIVI,siscom_mi.SEGNALAZIONI_FO_PREV_STATI where SEGNALAZIONI_FO_PREV_STATI.id(+)=SEGNALAZIONI_FO_PREVENTIVI.id_stato and SEGNALAZIONI_FO_PREVENTIVI.id_segnalazione=" & sIndice
                    Dim myReaderPR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderPR.Read Then
                        lblRDORichiesto.Text = par.IfNull(myReaderPR("stato_pr"), "")
                        sStatoPreventivo.Value = par.IfNull(myReaderPR("ID"), "")
                    End If
                    myReaderPR.Close()
                Else
                    lblRDORichiesto.Visible = False
                    RadGridPreventivi.Enabled = False
                    RadTabStrip1.Tabs(1).Visible = False
                    lblStatoPR.Visible = False
                    sStatoPreventivo.Value = ""
                End If
                If par.IfNull(myReader("id_manutenzione"), 0) <> 0 Then
                    par.cmd.CommandText = "select * from siscom_mi.manutenzioni where id=" & myReader("id_manutenzione")
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        indiceM.Value = myReader("id_manutenzione")
                        lblOrdine.Text = par.IfNull(myReader1("PROGR"), "") & "/" & par.IfNull(myReader1("ANNO"), "") & " del " & par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_ORDINE"), ""))
                        lblInizioODL.Text = par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_INTERVENTO"), ""))
                        lblFineODL.Text = par.FormattaData(par.IfNull(myReader1("DATA_FINE_INTERVENTO"), ""))
                        lblRichiestaODL.Text = par.IfNull(myReader1("DESCRIZIONE"), "---")
                        lblDanneggiatoODL.Text = par.IfNull(myReader1("DANNEGGIATO"), "---")
                        Dim Esito As String = CaricaInterventi(myReader("id_manutenzione"))

                        If par.IfNull(myReader1("data_pgi"), "") <> "" Then
                            txtDataPGI.SelectedDate = par.FormattaData(par.IfNull(myReader1("data_pgi"), ""))
                        End If
                        If par.IfNull(myReader1("data_tdl"), "") <> "" Then
                            txtDataTDL.SelectedDate = par.FormattaData(par.IfNull(myReader1("data_tdl"), ""))
                        End If


                        If par.IfNull(myReader1("ID_SCALA"), "0") <> "0" Then
                            par.cmd.CommandText = "select scale_edifici.descrizione as scala,indirizzi.* from siscom_mi.indirizzi,siscom_mi.EDIFICI,siscom_mi.scale_edifici where indirizzi.id(+)=EDIFICI.id_indirizzo_PRINCIPALE and EDIFICI.id=scale_edifici.id_edificio and scale_edifici.id=" & myReader1("id_scala")
                            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                lblUbicazione.Text = "SCALA " & par.IfNull(myReader2("scala"), "") & " dell'EDIFICIO in " & par.IfNull(myReader2("DESCRIZIONE"), "") & " N. " & par.IfNull(myReader2("CIVICO"), "") & " " & par.IfNull(myReader2("CAP"), "") & " - " & par.IfNull(myReader2("LOCALITA"), "")
                            End If
                            myReader2.Close()
                        ElseIf par.IfNull(myReader1("ID_EDIFICIO"), "0") <> "0" Then
                            par.cmd.CommandText = "select indirizzi.* from siscom_mi.indirizzi,siscom_mi.EDIFICI where indirizzi.id(+)=EDIFICI.id_indirizzo_PRINCIPALE and EDIFICI.id=" & myReader1("id_EDIFICIO")
                            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                lblUbicazione.Text = "EDIFICIO in " & par.IfNull(myReader2("DESCRIZIONE"), "") & " N. " & par.IfNull(myReader2("CIVICO"), "") & " " & par.IfNull(myReader2("CAP"), "") & " - " & par.IfNull(myReader2("LOCALITA"), "")
                            End If
                            myReader2.Close()
                        ElseIf par.IfNull(myReader1("ID_COMPLESSO"), "0") <> "0" Then
                            par.cmd.CommandText = "select indirizzi.* from siscom_mi.indirizzi,siscom_mi.complessi_immobiliari where indirizzi.id(+)=complessi_immobiliari.id_indirizzo_riferimento and complessi_immobiliari.id=" & myReader1("id_complesso")
                            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                lblUbicazione.Text = "COMPLESSO in " & par.IfNull(myReader2("DESCRIZIONE"), "") & " N. " & par.IfNull(myReader2("CIVICO"), "") & " " & par.IfNull(myReader2("CAP"), "") & " - " & par.IfNull(myReader2("LOCALITA"), "")
                            End If
                            myReader2.Close()
                        End If
                        Dim IDBM As String = ""
                        par.cmd.CommandText = "SELECT SISCOM_MI.GETBUILDINGMANAGER(" & indiceM.Value & ",1) FROM DUAL"
                        IDBM = par.IfNull(par.cmd.ExecuteScalar, "")
                        'If par.IfNull(myReader1("ID_SCALA"), "0") <> "0" Then
                        '    par.cmd.CommandText = "select EDIFICI.ID_BM,TAB_FILIALI.NOME  from siscom_mi.COMPLESSI_IMMOBILIARI,siscom_mi.EDIFICI,siscom_mi.scale_edifici,SISCOM_MI.TAB_FILIALI where TAB_FILIALI.ID=COMPLESSI_IMMOBILIARI.ID_FILIALE AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO and EDIFICI.id=scale_edifici.id_edificio and scale_edifici.id=" & myReader1("id_scala")
                        '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        '    If myReader2.Read Then
                        '        If par.IfNull(myReader2("id_bm"), 0) <> 0 Then
                        '            lblSedeTerritoriale.Text = par.IfNull(myReader2("NOME"), "")
                        '            IDBM = par.IfNull(myReader2("ID_BM"), "0")
                        '        End If
                        '    End If
                        '    myReader2.Close()
                        'ElseIf par.IfNull(myReader1("ID_EDIFICIO"), "0") <> "0" Then
                        '    par.cmd.CommandText = "select EDIFICI.ID_BM,TAB_FILIALI.NOME from siscom_mi.COMPLESSI_IMMOBILIARI,siscom_mi.EDIFICI,SISCOM_MI.TAB_FILIALI where TAB_FILIALI.ID=COMPLESSI_IMMOBILIARI.ID_FILIALE AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO and EDIFICI.id=" & myReader1("id_EDIFICIO")
                        '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        '    If myReader2.Read Then
                        '        If par.IfNull(myReader2("id_bm"), 0) <> 0 Then
                        '            lblSedeTerritoriale.Text = par.IfNull(myReader2("NOME"), "")
                        '            IDBM = par.IfNull(myReader2("ID_BM"), "0")
                        '        End If
                        '    End If
                        '    myReader2.Close()
                        'ElseIf par.IfNull(myReader1("ID_COMPLESSO"), "0") <> "0" Then
                        '    par.cmd.CommandText = "select TAB_FILIALI.NOME,EDIFICI.ID_BM from siscom_mi.TAB_FILIALI,siscom_mi.complessi_immobiliari,SISCOM_MI.EDIFICI where EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID AND TAB_FILIALI.ID=COMPLESSI_IMMOBILIARI.ID_FILIALE and complessi_immobiliari.id=" & myReader1("id_complesso")
                        '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        '    Do While myReader2.Read
                        '        If par.IfNull(myReader2("id_bm"), 0) <> 0 Then
                        '            lblSedeTerritoriale.Text = par.IfNull(myReader2("NOME"), "")
                        '            IDBM = IDBM & par.IfNull(myReader2("id_bm"), "") & ","
                        '        End If
                        '    Loop
                        '    myReader2.Close()
                        '    If IDBM <> "" Then IDBM = Mid(IDBM, 1, Len(IDBM) - 1)
                        'End If
                        If IDBM <> "" Then
                            Dim kk As Integer = 1
                            lblBM.Text = "<table cellpadding='0' cellspacing='0' style='width:100%;'>"
                            par.cmd.CommandText = "select DISTINCT BUILDING_MANAGER_OPERATORI.contatto_telefonico,OPERATORI.COGNOME||' '||OPERATORI.NOME AS DATIBM,TAB_FILIALI.N_TELEFONO from operatori,SISCOM_MI.TAB_FILIALI,SISCOM_MI.BUILDING_MANAGER,SISCOM_MI.BUILDING_MANAGER_OPERATORI " _
                                                & " WHERE TAB_FILIALI.ID=BUILDING_MANAGER.ID_STRUTTURA AND BUILDING_MANAGER_OPERATORI.ID_BM=BUILDING_MANAGER.ID AND OPERATORI.ID=BUILDING_MANAGER_OPERATORI.ID_OPERATORE " _
                                                & "And OPERATORI.ID IN (" & IDBM & ")"
                            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            Do While myReader2.Read
                                'lblBM1.Text = kk & ") " & par.IfNull(myReader2("DATIBM"), "")
                                'lblBM1Tel.Text = "Tel. " & par.IfNull(myReader2("N_TELEFONO"), "")
                                lblBM.Text = lblBM.Text & "<tr><td>" & kk & ") " & par.IfNull(myReader2("DATIBM"), "") & "</td><td>" & "Tel. " & par.IfNull(myReader2("CONTATTO_TELEFONICO"), "") & "</td></tr>"
                                kk += 1
                            Loop
                            lblBM.Text = lblBM.Text & "</table>"
                            myReader2.Close()
                        Else
                            lblBM.Text = "<table cellpadding='0' cellspacing='0' style='width:100%;'>"
                            lblBM.Text = lblBM.Text & "<tr><td>---</td><td></td></tr>"
                            lblBM.Text = lblBM.Text & "</table>"
                        End If

                        par.cmd.CommandText = "Select NUM_REPERTORIO,DATA_REPERTORIO,DESCRIZIONE FROM SISCOM_MI.APPALTI WHERE ID=" & myReader1("id_APPALTO")
                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader3.Read Then
                            lblNumContratto.Text = par.IfNull(myReader3("NUM_REPERTORIO"), "") & " del " & par.FormattaData(par.IfNull(myReader3("DATA_REPERTORIO"), ""))
                            lblDescrizioneContratto.Text = par.IfNull(myReader3("DESCRIZIONE"), "")
                        End If
                        myReader3.Close()

                        lblLinkPDF.Text = "<a href='#' onclick='javascript:StampaOrdine();'>Clicca per Visualizzare</a>"
                    Else
                        indiceM.Value = ""
                    End If
                    myReader1.Close()
                Else
                    'ordine non emesso
                    lblOrdine.Text = "---"
                End If

                If par.IfNull(myReader("id_SEGNALAZIONE"), 0) <> 0 Then
                    par.cmd.CommandText = "select SEGNALAZIONI.*,RAPPORTI_UTENZA.COD_CONTRATTO,(SELECT COGNOME FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS COGNOME_INT,(SELECT NOME FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS NOME_INT from SISCOM_MI.RAPPORTI_UTENZA,siscom_mi.SEGNALAZIONI where RAPPORTI_UTENZA.ID(+)=SEGNALAZIONI.ID_CONTRATTO AND SEGNALAZIONI.id=" & myReader("id_SEGNALAZIONE")
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        indiceSD.Value = par.IfNull(myReader1("ID"), "")
                        lblsegnalazione.Text = par.IfNull(myReader1("ID"), "") & " del " & par.FormattaData(Mid(par.IfNull(myReader1("DATA_ORA_RICHIESTA"), ""), 1, 8))
                        lblCodicecontratto.Text = par.IfNull(myReader1("COD_CONTRATTO"), "---")
                        lblCognomeIntestatario.Text = par.IfNull(myReader1("COGNOME_INT"), "---")
                        lblNomeIntestatario.Text = par.IfNull(myReader1("NOME_INT"), "---")

                        If par.IfNull(myReader1("ID_UNITA"), 0) <> 0 Then
                            par.cmd.CommandText = "SELECT TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,scale_edifici.descrizione as scala,UNITA_IMMOBILIARI.INTERNO,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,INDIRIZZI.DESCRIZIONE||' '||CIVICO||' '||CAP||' '||LOCALITA AS INDIRIZZO FROM SISCOM_MI.TIPO_LIVELLO_PIANO,siscom_mi.scale_edifici,SISCOM_MI.INDIRIZZi,SISCOM_MI.UNITA_IMMOBILIARI WHERE TIPO_LIVELLO_PIANO.COD(+)=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO AND scale_edifici.id(+)=unita_immobiliari.id_scala and INDIRIZZI.ID (+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=" & par.IfNull(myReader1("ID_UNITA"), 0)
                            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                lblCodiceUnita.Text = par.IfNull(myReader2("COD_UNITA_IMMOBILIARE"), "---")
                                lblIndirizzo.Text = par.IfNull(myReader2("INDIRIZZO"), "---")
                                lblScala.Text = par.IfNull(myReader2("SCALA"), "---")
                                lblInterno.Text = par.IfNull(myReader2("INTERNO"), "---")
                                lblPiano.Text = par.IfNull(myReader2("PIANO"), "---")
                            End If
                            myReader2.Close()
                        Else
                            lblCodiceUnita.Text = "---"
                        End If

                        If par.IfNull(myReader1("ID_EDIFICIO"), 0) <> 0 Then
                            par.cmd.CommandText = "SELECT EDIFICI.DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID=" & par.IfNull(myReader1("ID_EDIFICIO"), 0)
                            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader2.Read Then
                                lblEdificio.Text = par.IfNull(myReader2("DENOMINAZIONE"), "---")
                            End If
                            myReader2.Close()
                        End If

                        lblrichiesta.Text = par.IfNull(myReader1("DESCRIZIONE_RIC"), "---")
                    End If
                    myReader1.Close()
                Else
                    lblsegnalazione.Text = "---"
                End If
                If par.IfNull(myReader("id_stato"), "1") = "1" Then
                    btnCarico.Enabled = True
                Else
                    btnCarico.Enabled = False
                End If
                If par.IfNull(myReader("id_stato"), "1") = "6" Then
                    btnCarico.Enabled = False
                    txtDataPGI.Enabled = False
                    txtDataTDL.Enabled = False
                    RadGridPreventivi.Enabled = False
                End If
            Else
                par.cmd.CommandText = "select * from siscom_mi.manutenzioni where (MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO)='" & Trim(Replace(Mid(Request.QueryString("D"), 1, InStr(Request.QueryString("D"), "REP.") - 1), "ODL ", "")) & "'"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    indiceM.Value = myReader1("id")
                    lblOrdine.Text = par.IfNull(myReader1("PROGR"), "") & "/" & par.IfNull(myReader1("ANNO"), "") & " del " & par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_ORDINE"), ""))
                    lblInizioODL.Text = par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_INTERVENTO"), ""))
                    lblFineODL.Text = par.FormattaData(par.IfNull(myReader1("DATA_FINE_INTERVENTO"), ""))
                    lblRichiestaODL.Text = par.IfNull(myReader1("DESCRIZIONE"), "---")
                    lblDanneggiatoODL.Text = par.IfNull(myReader1("DANNEGGIATO"), "---")
                    Dim Esito As String = CaricaInterventi(myReader1("id"))
                    If par.IfNull(myReader1("data_pgi"), "") <> "" Then
                        txtDataPGI.SelectedDate = par.FormattaData(par.IfNull(myReader1("data_pgi"), ""))
                    End If
                    If par.IfNull(myReader1("data_tdl"), "") <> "" Then
                        txtDataTDL.SelectedDate = par.FormattaData(par.IfNull(myReader1("data_tdl"), ""))
                    End If
                    If par.IfNull(myReader1("ID_SCALA"), "0") <> "0" Then
                        par.cmd.CommandText = "select scale_edifici.descrizione as scala,indirizzi.* from siscom_mi.indirizzi,siscom_mi.EDIFICI,siscom_mi.scale_edifici where indirizzi.id(+)=EDIFICI.id_indirizzo_PRINCIPALE and EDIFICI.id=scale_edifici.id_edificio and scale_edifici.id=" & myReader1("id_scala")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            lblUbicazione.Text = "SCALA " & par.IfNull(myReader2("scala"), "") & " dell'EDIFICIO in " & par.IfNull(myReader2("DESCRIZIONE"), "") & " N. " & par.IfNull(myReader2("CIVICO"), "") & " " & par.IfNull(myReader2("CAP"), "") & " - " & par.IfNull(myReader2("LOCALITA"), "")
                        End If
                        myReader2.Close()
                    ElseIf par.IfNull(myReader1("ID_EDIFICIO"), "0") <> "0" Then
                        par.cmd.CommandText = "select indirizzi.* from siscom_mi.indirizzi,siscom_mi.EDIFICI where indirizzi.id(+)=EDIFICI.id_indirizzo_PRINCIPALE and EDIFICI.id=" & myReader1("id_EDIFICIO")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            lblUbicazione.Text = "EDIFICIO in " & par.IfNull(myReader2("DESCRIZIONE"), "") & " N. " & par.IfNull(myReader2("CIVICO"), "") & " " & par.IfNull(myReader2("CAP"), "") & " - " & par.IfNull(myReader2("LOCALITA"), "")
                        End If
                        myReader2.Close()
                    ElseIf par.IfNull(myReader1("ID_COMPLESSO"), "0") <> "0" Then
                        par.cmd.CommandText = "select indirizzi.* from siscom_mi.indirizzi,siscom_mi.complessi_immobiliari where indirizzi.id(+)=complessi_immobiliari.id_indirizzo_riferimento and complessi_immobiliari.id=" & myReader1("id_complesso")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            lblUbicazione.Text = "COMPLESSO in " & par.IfNull(myReader2("DESCRIZIONE"), "") & " N. " & par.IfNull(myReader2("CIVICO"), "") & " " & par.IfNull(myReader2("CAP"), "") & " - " & par.IfNull(myReader2("LOCALITA"), "")
                        End If
                        myReader2.Close()
                    End If
                    Dim IDBM As String = ""
                    If par.IfNull(myReader1("ID_SCALA"), "0") <> "0" Then
                        par.cmd.CommandText = "select EDIFICI.ID_BM,TAB_FILIALI.NOME  from siscom_mi.COMPLESSI_IMMOBILIARI,siscom_mi.EDIFICI,siscom_mi.scale_edifici,SISCOM_MI.TAB_FILIALI where TAB_FILIALI.ID=COMPLESSI_IMMOBILIARI.ID_FILIALE AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO and EDIFICI.id=scale_edifici.id_edificio and scale_edifici.id=" & myReader1("id_scala")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            If par.IfNull(myReader2("id_bm"), 0) <> 0 Then
                                lblSedeTerritoriale.Text = par.IfNull(myReader2("NOME"), "")
                                IDBM = par.IfNull(myReader2("ID_BM"), "0")
                            End If
                        End If
                        myReader2.Close()
                    ElseIf par.IfNull(myReader1("ID_EDIFICIO"), "0") <> "0" Then
                        par.cmd.CommandText = "select EDIFICI.ID_BM,TAB_FILIALI.NOME from siscom_mi.COMPLESSI_IMMOBILIARI,siscom_mi.EDIFICI,SISCOM_MI.TAB_FILIALI where TAB_FILIALI.ID=COMPLESSI_IMMOBILIARI.ID_FILIALE AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO and EDIFICI.id=" & myReader1("id_EDIFICIO")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            If par.IfNull(myReader2("id_bm"), 0) <> 0 Then
                                lblSedeTerritoriale.Text = par.IfNull(myReader2("NOME"), "")
                                IDBM = par.IfNull(myReader2("ID_BM"), "0")
                            End If
                        End If
                        myReader2.Close()
                    ElseIf par.IfNull(myReader1("ID_COMPLESSO"), "0") <> "0" Then
                        par.cmd.CommandText = "select TAB_FILIALI.NOME,EDIFICI.ID_BM from siscom_mi.TAB_FILIALI,siscom_mi.complessi_immobiliari,SISCOM_MI.EDIFICI where EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID AND TAB_FILIALI.ID=COMPLESSI_IMMOBILIARI.ID_FILIALE and complessi_immobiliari.id=" & myReader1("id_complesso")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader2.Read
                            If par.IfNull(myReader2("id_bm"), 0) <> 0 Then
                                lblSedeTerritoriale.Text = par.IfNull(myReader2("NOME"), "")
                                IDBM = IDBM & par.IfNull(myReader2("id_bm"), "") & ","
                            End If
                        Loop
                        myReader2.Close()
                        If IDBM <> "" Then IDBM = Mid(IDBM, 1, Len(IDBM) - 1)
                    End If
                    If IDBM <> "" Then
                        Dim kk As Integer = 1
                        lblBM.Text = "<table cellpadding='0' cellspacing='0' style='width:100%;'>"
                        par.cmd.CommandText = "select BUILDING_MANAGER_OPERATORI.contatto_telefonico,OPERATORI.COGNOME||' '||OPERATORI.NOME AS DATIBM,TAB_FILIALI.N_TELEFONO from operatori,SISCOM_MI.TAB_FILIALI,SISCOM_MI.BUILDING_MANAGER,SISCOM_MI.BUILDING_MANAGER_OPERATORI " _
                                            & " WHERE TAB_FILIALI.ID=BUILDING_MANAGER.ID_STRUTTURA AND BUILDING_MANAGER_OPERATORI.ID_BM=BUILDING_MANAGER.ID AND OPERATORI.ID=BUILDING_MANAGER_OPERATORI.ID_OPERATORE " _
                                            & "And BUILDING_MANAGER.ID IN (" & IDBM & ")"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader2.Read
                            lblBM.Text = lblBM.Text & "<tr><td>" & kk & ") " & par.IfNull(myReader2("DATIBM"), "") & "</td><td>" & "Tel. " & par.IfNull(myReader2("CONTATTO_TELEFONICO"), "") & "</td></tr>"
                            kk += 1
                        Loop
                        lblBM.Text = lblBM.Text & "</table>"
                        myReader2.Close()
                    Else
                        lblBM.Text = "<table cellpadding='0' cellspacing='0' style='width:100%;'>"
                        lblBM.Text = lblBM.Text & "<tr><td>---</td><td></td></tr>"
                        lblBM.Text = lblBM.Text & "</table>"
                    End If

                    par.cmd.CommandText = "Select NUM_REPERTORIO,DATA_REPERTORIO,DESCRIZIONE FROM SISCOM_MI.APPALTI WHERE ID=" & myReader1("id_APPALTO")
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        lblNumContratto.Text = par.IfNull(myReader3("NUM_REPERTORIO"), "") & " del " & par.FormattaData(par.IfNull(myReader3("DATA_REPERTORIO"), ""))
                        lblDescrizioneContratto.Text = par.IfNull(myReader3("DESCRIZIONE"), "")
                    End If
                    myReader3.Close()

                    lblLinkPDF.Text = "<a href='#' onclick='javascript:StampaOrdine();'>Clicca per Visualizzare</a>"
                Else
                    indiceM.Value = ""
                End If
                myReader1.Close()
                Disattivatutto()
            End If
            myReader.Close()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - CaricaIntervento - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", True)
        End If
        If Session.Item("MOD_FORNITORI") <> "1" Then
            Response.Redirect("../AccessoNegato.htm", True)
        End If
        If Session.Item("MOD_FORNITORI_RDO") <> "1" Then
            Response.Redirect("../AccessoNegato.htm", True)
        End If
        If Request.QueryString("S") = "1" Then
            If InStr(UCase(Request.QueryString("D")), "REP") > 0 Then
                Me.connData = New CM.datiConnessione(par, False, False)
                connData.apri()
                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE ID_MANUTENZIONE=(select manutenzioni.id from siscom_mi.manutenzioni where (MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO)='" & Trim(Replace(Mid(Request.QueryString("D"), 1, InStr(Request.QueryString("D"), "REP.") - 1), "ODL ", "")) & "')"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    sIndice = par.IfNull(myReader("id"), "0")
                    indiceS.Value = sIndice
                End If
                myReader.Close()
                connData.chiudi()
            End If
        Else
            sIndice = Request.QueryString("D")
            indiceS.Value = sIndice
        End If


        If Not IsPostBack Then
            Try
                CaricaDati()
                CaricaAllegati()
                CaricaPreventivi()
                CaricaSP()
                CaricaIrregolarita()
                'RadGridAllegati.MasterTableView.EditMode = CType([Enum].Parse(GetType(GridEditMode), "PopUp"), GridEditMode)
                RadGridAllegati.MasterTableView.EditFormSettings.InsertCaption = "Inserimento Allegato"
                RadGridAllegati.MasterTableView.EditFormSettings.CaptionFormatString = "Modifica Allegato"

                'RadGridPreventivi.MasterTableView.EditMode = CType([Enum].Parse(GetType(GridEditMode), "PopUp"), GridEditMode)
                'RadGridPreventivi.MasterTableView.EditFormSettings.InsertCaption = "INSERIMENTO PREVENTIVO"
                'RadGridPreventivi.MasterTableView.EditFormSettings.CaptionFormatString = "MODIFICA PREVENTIVO"
                CaricaAttributi()
                VerificaOperatore()
            Catch ex As Exception
                Session.Add("ERRORE", "Provenienza: Fornitori - Ordini - Carica - " & ex.Message)
                Response.Redirect("../Errore.aspx", True)
            End Try
        End If
        AbilitaDate()
    End Sub

    Private Sub VerificaOperatore()
        Try
            connData.apri()
            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("id_operatore")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("MOD_FO_ID_FO"), "0") = "0" And Session.Item("id_operatore") <> "1" Then
                    Disattivatutto()
                    OpSolaLettura = "1"
                Else
                    OpSolaLettura = "0"
                End If
                If par.IfNull(myReader("MOD_FORNITORI_SLE"), 0) = 1 Then
                    Disattivatutto()
                    OpSolaLettura = "1"
                End If
            End If
            myReader.Close()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - Intervento - VerificaOperatore - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Private Sub Disattivatutto()
        RadGridPreventivi.Enabled = False
        RadGridAllegati.Enabled = False
        RadGridAllegati.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
        RadGridAllegati.MasterTableView.GetColumn("EditCommandColumn").Display = False
        RadGridAllegati.MasterTableView.GetColumn("DeleteColumn").Display = False

        btnCarico.Visible = False
        btnFineLavori.Visible = False
        btnContabilizza.Visible = False
        btnSalva.Visible = False
        txtDataPGI.Enabled = False
        txtDataTDL.Enabled = False
        txtDataFineLavori.Enabled = False
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Private Function CaricaAttributi()

        'VENGONO CARICATI GLI ATTRIBUTI "CORRENTE" (VALORE CORRENTE) E "NOME" (NOME DEL CAMPO)
        'MENTRE IL VALORE "CORRENTE" VIENE CARICATO AUTOMATCAMENTE (SOLO PER CHECKBOX, TEXTBOX E DROPDOWNLIST)
        'IL VALORE DELL'ATTRIBUTO "NOME" VIENE CARICATO MANUALMENTE, IN MODO DA INSERIRE DEL TESTO 
        'PIU' SIGNIFICATIVO E NON SEMPLICEMENTE LA PROPRIETA' TEXT


        'attributi nome da memorizzare
        Me.txtDataPGI.Attributes.Add("NOME", "DATA DPIL")
        Me.txtDataPGI.Attributes.Add("CORRENTE", txtDataPGI.SelectedDate.ToString())
        Me.txtDataTDL.Attributes.Add("NOME", "DATA DPFL")
        Me.txtDataTDL.Attributes.Add("CORRENTE", txtDataTDL.SelectedDate.ToString())

    End Function

    Private Function MemorizzaAttributiDate() As Boolean
        Dim ELENCOERRORI As String = ""
        Dim DateModificate As Boolean = False

        Try
            Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")

            If txtDataPGI.SelectedDate.ToString() <> txtDataPGI.Attributes("CORRENTE").ToUpper.ToString Then
                ScriviLog(txtDataPGI.Attributes("NOME").ToUpper.ToString, txtDataPGI.Attributes("CORRENTE").ToUpper.ToString, txtDataPGI.SelectedDate.ToString(), Tempo, "F264")
                DateModificate = True
            End If
            If txtDataTDL.SelectedDate.ToString() <> txtDataTDL.Attributes("CORRENTE").ToUpper.ToString Then
                ScriviLog(txtDataTDL.Attributes("NOME").ToUpper.ToString, txtDataTDL.Attributes("CORRENTE").ToUpper.ToString, txtDataTDL.SelectedDate.ToString(), Tempo, "F265")
                DateModificate = True
            End If

            If DateModificate = True Then
                'se le date rientrano nei valori di inizio e fine odl la modifica viene automaticamente accettata
                If Format(txtDataPGI.SelectedDate, "yyyyMMdd") >= par.AggiustaData(lblInizioODL.Text) And Format(txtDataPGI.SelectedDate, "yyyyMMdd") <= par.AggiustaData(lblFineODL.Text) Then
                    If Format(txtDataTDL.SelectedDate, "yyyyMMdd") >= par.AggiustaData(lblInizioODL.Text) And Format(txtDataTDL.SelectedDate, "yyyyMMdd") <= par.AggiustaData(lblFineODL.Text) Then
                        connData.apri(True)
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F272','" & par.PulisciStrSql(Format(txtDataPGI.SelectedDate, "dd/MM/yyyy") & " - " & Format(txtDataTDL.SelectedDate, "dd/MM/yyyy")) & " accettate in quanto rientrano nei limiti stabiliti')"
                        par.cmd.ExecuteNonQuery()
                        connData.chiudi(True)
                    End If
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Ordini - MemorizzaAttributi - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Function

    Private Sub ScriviLog(ByVal CAMPO As String, ByVal VAL_PRECEDENTE As String, ByVal VAL_IMPOSTATO As String, tempo As String, CodEvento As String)
        Try
            Dim aperto As Boolean = False

            connData.apri(True)


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndice & "," & Session.Item("ID_OPERATORE") & ",'" & tempo & "','" & CodEvento & "','" & par.PulisciStrSql(CAMPO) & " - Valore Precedente: " & par.PulisciStrSql(Mid(VAL_PRECEDENTE, 1, 10)) & " - Valore Impostato: " & par.PulisciStrSql(Mid(VAL_IMPOSTATO, 1, 10)) & "')"
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - ScriviLog - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Private Sub CaricaAllegati()
        sStrSqlAllegati = "select SEGNALAZIONI_FO_ALL_TIPI.ID AS ID_TIPO_ALL,SEGNALAZIONI_FO_ALLEGATI.ID,TO_CHAR(TO_DATE(SUBSTR(SEGNALAZIONI_FO_ALLEGATI.DATA_ORA,1,8),'yyyymmdd'),'dd/mm/yyyy')||' '||SUBSTR(SEGNALAZIONI_FO_ALLEGATI.DATA_ORA,9,2)||':'||SUBSTR(SEGNALAZIONI_FO_ALLEGATI.DATA_ORA,11,2) AS DATA_ORA,SEGNALAZIONI_FO_ALLEGATI.DESCRIZIONE,(CASE WHEN NOME_FILE IS NOT NULL THEN replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/FORNITORI/'||NOME_FILE||''',''Allegati'','''');£>Visualizza</a>','$','&'),'£','" & Chr(34) & "') ELSE '' END) AS NOME_FILE,SEGNALAZIONI_FO_ALL_TIPI.DESCRIZIONE AS TIPOLOGIA FROM SISCOM_MI.SEGNALAZIONI_FO_ALLEGATI,SISCOM_MI.SEGNALAZIONI_FO_ALL_TIPI WHERE SEGNALAZIONI_FO_ALL_TIPI.ID(+)=SEGNALAZIONI_FO_ALLEGATI.ID_TIPO AND ID_SEGNALAZIONE=" & sIndice & " ORDER BY DATA_ORA DESC"
    End Sub

    Private Sub CaricaSP()
        If indiceSD.Value <> "" Then
            sStrSqlSP = "select DECODE(FL_PERICOLO,1,'SI',0,'NO') AS PERICOLO,TECNICO,RAPPORTO,TO_DATE(DATA_SOPRALLUOGO,'yyyymmdd') AS DATA_SP from siscom_mi.segnalazioni_sopralluogo where ID_SEGNALAZIONE=" & indiceSD.Value & " order by DATA_SOPRALLUOGO desc"
        Else
            sStrSqlSP = ""
        End If
    End Sub

    Private Sub CaricaIrregolarita()
        If sIndice <> "" Then
            sStrSqlIrregolarita = "select SEGNALAZIONI_FO_IRR.ID,SEGNALAZIONI_FO_IRR.ID_TIPO,TO_CHAR(TO_DATE(SUBSTR(SEGNALAZIONI_FO_IRR.DATA_ORA,1,8),'yyyymmdd'),'dd/mm/yyyy')||' '||SUBSTR(SEGNALAZIONI_FO_IRR.DATA_ORA,9,2)||':'||SUBSTR(SEGNALAZIONI_FO_IRR.DATA_ORA,11,2) AS DATA_ORA,SEGNALAZIONI_FO_IRR.DESCRIZIONE,SEGNALAZIONI_FO_TIPI_IRR.DESCRIZIONE AS TIPOLOGIA FROM SISCOM_MI.SEGNALAZIONI_FO_IRR,SISCOM_MI.SEGNALAZIONI_FO_TIPI_IRR WHERE VISIBILE=1 AND SEGNALAZIONI_FO_TIPI_IRR.ID(+)=SEGNALAZIONI_FO_IRR.ID_TIPO AND ID_SEGNALAZIONE=" & sIndice & " ORDER BY DATA_ORA DESC"
        Else
            sStrSqlIrregolarita = ""
        End If
    End Sub

    Private Function AbilitaDate()
        If OpSolaLettura <> "1" Then
            txtDataPGI.Enabled = True
            txtDataTDL.Enabled = True
            btnContabilizza.Visible = False
            btnFineLavori.Visible = False
            txtDataFineLavori.Enabled = False

            If sStatoIntervento.Value <> "2" And sStatoIntervento.Value <> "10" And sStatoIntervento.Value <> "1" Then
                txtDataPGI.Enabled = False
                txtDataTDL.Enabled = False
            End If
            Select Case sStatoIntervento.Value
                Case "1"

                Case "2", "10"
                    btnFineLavori.Visible = True
                    txtDataFineLavori.Enabled = True
                Case "9"
                    If sRichiestaContab.Value = "1" Then
                        btnContabilizza.Visible = True
                    End If
            End Select

            If indiceM.Value = "" Then
                txtDataPGI.Enabled = False
                txtDataTDL.Enabled = False
            End If
        End If
    End Function

    Public Property sIndice() As String
        Get
            If Not (ViewState("par_sIndice") Is Nothing) Then
                Return CStr(ViewState("par_sIndice"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIndice") = value
        End Set
    End Property

    Protected Sub btnCarico_Click(sender As Object, e As System.EventArgs) Handles btnCarico.Click
        If Format(txtDataPGI.SelectedDate, "yyyyMMdd") = "" Or Format(txtDataTDL.SelectedDate, "yyyyMMdd") = "" Then
            RadWindowManager1.RadAlert("Attenzione...Prima di prendere in carico è necessario inserire le date DPIL e DPFL!. Memorizzazione non effettuata!", 350, 150, "Info", Nothing, Nothing)
            Exit Sub
        End If
        If Format(txtDataTDL.SelectedDate, "yyyyMMdd") < Format(txtDataPGI.SelectedDate, "yyyyMMdd") Then
            RadWindowManager1.RadAlert("Attenzione...La data DPFL deve essere uguale o successiva alla data DPIL!. Memorizzazione non effettuata!", 350, 150, "Info", Nothing, Nothing)
            Exit Sub
        End If
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri()
            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FORNITORI SET ID_STATO=2 WHERE ID=" & sIndice
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE SISCOM_MI.MANUTENZIONI SET DATA_PGI='" & Format(txtDataPGI.SelectedDate, "yyyyMMdd") & "',DATA_TDL='" & Format(txtDataTDL.SelectedDate, "yyyyMMdd") & "' WHERE ID=" & indiceM.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F256','')"
            par.cmd.ExecuteNonQuery()
            connData.chiudi()
            sStatoIntervento.Value = "2"
            btnCarico.Enabled = False
            lblStatoIntervento.Text = "IN CARICO"
            MemorizzaAttributiDate()
            AbilitaDate()
            HModificato.Value = "0"
            RadGridPreventivi.Rebind()

            VisualizzaAlert("Operazione effettuata", 1)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - PresaInCaricoIntervento - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub dgvInterventi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvInterventi.NeedDataSource
        If sStrSql <> "" Then
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSql)
        End If
    End Sub

    Public Property OpSolaLettura() As String
        Get
            If Not (ViewState("par_OpSolaLettura") Is Nothing) Then
                Return CStr(ViewState("par_OpSolaLettura"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_OpSolaLettura") = value
        End Set
    End Property

    Public Property sStrSql() As String
        Get
            If Not (ViewState("par_sStrSql") Is Nothing) Then
                Return CStr(ViewState("par_sStrSql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSql") = value
        End Set
    End Property

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)

            If Format(txtDataPGI.SelectedDate, "yyyyMMdd") = "" Or Format(txtDataTDL.SelectedDate, "yyyyMMdd") = "" Then
                VisualizzaAlert("Attenzione...E' necessario inserire le date DPIL e DPFL!. Memorizzazione non effettuata!", 2)
                connData.chiudi(True)
                Exit Sub
            End If
            If Format(txtDataTDL.SelectedDate, "yyyyMMdd") < Format(txtDataPGI.SelectedDate, "yyyyMMdd") Then
                VisualizzaAlert("Attenzione...La data DPFL deve essere successiva alla data DPIL!. Memorizzazione non effettuata!", 2)
                connData.chiudi(True)
                Exit Sub
            End If

            'If Format(txtDataPGI.SelectedDate, "yyyyMMdd") <> "" And Format(txtDataTDL.SelectedDate, "yyyyMMdd") = "" Then
            '    RadWindowManager1.RadAlert("Attenzione...Se viene inserita la data PGI è necessario che venga inserita anche la data TDL!. Memorizzazione non effettuata!", 350, 150, "Info", Nothing, Nothing)
            '    VisualizzaAlert("Attenzione...La data TDL deve essere uguale o successiva alla data PGI!. Memorizzazione non effettuata!", 2)
            '    connData.chiudi(True)
            '    Exit Sub
            'End If
            If indiceM.Value <> "" Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.MANUTENZIONI SET DATA_PGI='" & Format(txtDataPGI.SelectedDate, "yyyyMMdd") & "',DATA_TDL='" & Format(txtDataTDL.SelectedDate, "yyyyMMdd") & "' WHERE ID=" & indiceM.Value
                par.cmd.ExecuteNonQuery()
                If Format(CDate(txtDataTDL.SelectedDate), "yyyyMMdd") >= Format(Now, "yyyyMMdd") Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FORNITORI SET ID_STATO = 2 WHERE ID=" & indiceS.Value
                    par.cmd.ExecuteNonQuery()
                ElseIf Format(CDate(txtDataTDL.SelectedDate), "yyyyMMdd") < Format(Now, "yyyyMMdd") And IsNothing(txtDataFineLavori.SelectedDate) Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FORNITORI SET ID_STATO = 10 WHERE ID=" & indiceS.Value
                    par.cmd.ExecuteNonQuery()
                End If
                If txtDataFineLavori.SelectedDate.ToString <> "" Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FORNITORI SET DATA_FINE_INTERVENTO='" & Format(CDate(txtDataFineLavori.SelectedDate), "yyyyMMdd") & "' WHERE ID=" & indiceS.Value
                    par.cmd.ExecuteNonQuery()
                End If
            End If
            connData.chiudi(True)
            AbilitaDate()
            MemorizzaAttributiDate()
            CaricaAttributi()
            VisualizzaAlert("Operazione effettuata", 1)
            HModificato.Value = "0"
        Catch ex As Exception
            connData.chiudi(True)
            Session.Add("ERRORE", "Provenienza: Fornitori - Salva - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Property sStrSqlPreventivi() As String
        Get
            If Not (ViewState("par_sStrSqlPreventivi") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlPreventivi"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlPreventivi") = value
        End Set
    End Property

    Public Property sStrSqlAllegati() As String
        Get
            If Not (ViewState("par_sStrSqlAllegati") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlAllegati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlAllegati") = value
        End Set
    End Property



    Public Property sStrSqlIrregolarita() As String
        Get
            If Not (ViewState("par_sStrSqlIrregolarita") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlIrregolarita"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlIrregolarita") = value
        End Set
    End Property
    Public Property sStrSqlSP() As String
        Get
            If Not (ViewState("par_sStrSqlSP") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlSP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlSP") = value
        End Set
    End Property

    Protected Sub RadGridAllegati_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridAllegati.DeleteCommand
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)

            par.cmd.CommandText = "delete from SISCOM_MI.SEGNALAZIONI_FO_ALLEGATI where id=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F260','" & par.PulisciStrSql(e.Item.Cells(5).Text) & "')"
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)
            VisualizzaAlert("Operazione effettuata", 1)
            RadGridAllegati.Rebind()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - EliminaAllegato - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAllegati_InsertCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridAllegati.InsertCommand
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim NomeFile As String = Format(CLng(sIndice), "0000000000") & "_" & Format(Now, "yyyyMMddHHmmss")
            connData.apri(True)

            Dim nFile As String = ""
            Dim nFileS As String = ""
            For Each file As UploadedFile In CType(userControl.FindControl("RadUploadAllegato"), RadAsyncUpload).UploadedFiles
                nFile = file.GetName()
                nFileS = NomeFile & Mid(nFile, Len(nFile) - 3, 4)
                If InStr(Mid(nFile, Len(nFile) - 3, 4), ".") = 0 Then
                    nFileS = NomeFile & "." & Mid(nFile, Len(nFile) - 3, 4)
                End If
                file.SaveAs(Server.MapPath("..\ALLEGATI\FORNITORI\") & nFileS)
            Next

            Dim DescrizioneAllegato As String = ""
            DescrizioneAllegato = CType(userControl.FindControl("txtDescrizioneAllegato"), RadTextBox).Text

            Dim tipoDoc As String = ""
            Dim DescrtipoDoc As String = ""
            If CType(userControl.FindControl("cmbTipologia"), RadComboBox).SelectedValue <> "-1" Then
                tipoDoc = par.insDbValue(CType(userControl.FindControl("cmbTipologia"), RadComboBox).SelectedValue, True, False)
            Else
                tipoDoc = "NULL"
            End If
            If tipoDoc <> "''" And nFileS <> "" Then
                If CType(userControl.FindControl("cmbTipologia"), RadComboBox).SelectedItem.Text <> "-1" Then
                    DescrtipoDoc = par.insDbValue(CType(userControl.FindControl("cmbTipologia"), RadComboBox).SelectedItem.Text, True, False)
                Else
                    DescrtipoDoc = ""
                End If
                par.cmd.CommandText = "Insert into SISCOM_MI.SEGNALAZIONI_FO_ALLEGATI (ID_SEGNALAZIONE, DATA_ORA, DESCRIZIONE, NOME_FILE, ID_OPERATORE,ID,ID_TIPO) Values (" & sIndice & ", '" & Format(Now, "yyyyMMddHHmmss") & "', '" & par.PulisciStrSql(DescrizioneAllegato) & "', '" & nFileS & "', " & Session.Item("ID_OPERATORE") & ",SISCOM_MI.SEQ_SEGNALAZIONI_FO_ALLEGATI.NEXTVAL," & tipoDoc & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F259','" & par.PulisciStrSql(DescrtipoDoc & " - " & DescrizioneAllegato) & "')"
                par.cmd.ExecuteNonQuery()

                connData.chiudi(True)
                VisualizzaAlert("Operazione effettuata", 1)
                RadGridAllegati.Rebind()
            Else
                VisualizzaAlert("Scegliere una tipologia documento valida e selezionare un file da allegare!", 2)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Inserisci allegato - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAllegati_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridAllegati.ItemCommand
        Try
            If e.CommandName = RadGrid.InitInsertCommandName Then '"Add new" button clicked

                Dim editColumn As GridEditCommandColumn = CType(RadGridAllegati.MasterTableView.GetColumn("EditCommandColumn"), GridEditCommandColumn)
                editColumn.Visible = False

            ElseIf (e.CommandName = RadGrid.RebindGridCommandName AndAlso e.Item.OwnerTableView.IsItemInserted) Then
                e.Canceled = True
            Else
                Dim editColumn As GridEditCommandColumn = CType(RadGridAllegati.MasterTableView.GetColumn("EditCommandColumn"), GridEditCommandColumn)
                If Not editColumn.Visible Then
                    editColumn.Visible = True
                End If
            End If

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Allegati - ItemCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAllegati_ItemDeleted(sender As Object, e As Telerik.Web.UI.GridDeletedEventArgs) Handles RadGridAllegati.ItemDeleted
        Try
            If Not e.Exception Is Nothing Then
                e.ExceptionHandled = True

            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Allegati - ItemDeleted - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAllegati_ItemInserted(sender As Object, e As Telerik.Web.UI.GridInsertedEventArgs) Handles RadGridAllegati.ItemInserted
        Try
            If Not e.Exception Is Nothing Then
                e.ExceptionHandled = True
                e.KeepInInsertMode = False
                'DisplayMessage(True, "Employee cannot be inserted. Reason: " + e.Exception.Message)
            Else
                'DisplayMessage(False, "Employee inserted")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Allegati - ItemInserted - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAllegati_ItemUpdated(sender As Object, e As Telerik.Web.UI.GridUpdatedEventArgs) Handles RadGridAllegati.ItemUpdated
        Try
            If Not e.Exception Is Nothing Then
                e.KeepInEditMode = True
                e.ExceptionHandled = True
                'DisplayMessage(True, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " cannot be updated. Reason: " + e.Exception.Message)
            Else
                'DisplayMessage(False, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " updated")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Allegati - ItemUpdated - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAllegati_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridAllegati.NeedDataSource
        Try
            If sStrSqlAllegati <> "" Then
                RadGridAllegati.DataSource = par.getDataTableGrid(sStrSqlAllegati)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Eventi - NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridSP_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridAllegati.NeedDataSource
        Try
            If sStrSqlSP <> "" Then
                RadGridSP.DataSource = par.getDataTableGrid(sStrSqlSP)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - SP - NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub OnItemDataBoundHandler(ByVal sender As Object, ByVal e As GridItemEventArgs)
        Try
            If (TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode) Then
                Dim radCombo1 As RadComboBox = CType(e.Item.FindControl("cmbTipologia"), RadComboBox)

                par.caricaComboTelerik("select id,descrizione from SISCOM_MI.SEGNALAZIONI_FO_ALL_TIPI order by descrizione desc", radCombo1, "ID", "DESCRIZIONE", True, "-1")
                If e.Item.DataItem.GetType.Name = "DataRowView" Then
                    radCombo1.SelectedValue = par.IfNull(e.Item.DataItem("ID_TIPO_ALL"), "-1")
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Intervento - OnItemDataBoundHandler - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Protected Sub RadGridAllegati_PreRender(sender As Object, e As System.EventArgs) Handles RadGridAllegati.PreRender
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Protected Sub RadGridAllegati_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridAllegati.UpdateCommand
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)
            connData.apri(True)

            Dim NomeFile As String = Format(sIndice, "0000000000") & "_" & Format(Now, "yyyyMMddHHmmss")

            Dim nFile As String = ""
            Dim nFileS As String = ""
            For Each file As UploadedFile In CType(userControl.FindControl("RadUploadAllegato"), RadAsyncUpload).UploadedFiles
                nFile = file.GetName()
                nFileS = NomeFile & Mid(nFile, Len(nFile) - 3, 4)
                file.SaveAs(Server.MapPath("..\ALLEGATI\FORNITORI\") & NomeFile & Mid(nFile, Len(nFile) - 3, 4))
            Next

            Dim DescrizioneAllegato As String = ""
            DescrizioneAllegato = CType(userControl.FindControl("txtDescrizioneAllegato"), RadTextBox).Text

            Dim tipoDoc As String = ""
            If CType(userControl.FindControl("cmbTipologia"), RadComboBox).SelectedValue <> "-1" Then
                tipoDoc = par.insDbValue(CType(userControl.FindControl("cmbTipologia"), RadComboBox).SelectedValue, True, False)
            Else
                tipoDoc = "NULL"
            End If
            If tipoDoc <> "''" Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FO_ALLEGATI SET NOME_FILE='" & nFileS & "', ID_TIPO=" & tipoDoc & ",DESCRIZIONE='" & par.PulisciStrSql(DescrizioneAllegato) & "' WHERE  ID = " & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
                par.cmd.ExecuteNonQuery()
                connData.chiudi(True)
                VisualizzaAlert("Operazione effettuata", 1)
                RadGridAllegati.Rebind()

                RadGridAllegati.EditIndexes.Clear()
            Else
                VisualizzaAlert("Scegliere una tipologia documento valida!", 2)
            End If


        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Allegati - UpdateCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaPreventivi()
        'sStrSqlPreventivi = "select SEGNALAZIONI_FO_PREVENTIVI.ID,SEGNALAZIONI_FO_PREVENTIVI.DESCRIZIONE,(SELECT TO_CHAR(SUM(NVL(IMPORTO,0)),'9G999G999G999G999G990D99') FROM SISCOM_MI.SEGNALAZIONI_FO_PREV_DETT WHERE ID_PREVENTIVO=SEGNALAZIONI_FO_PREVENTIVI.ID) AS IMPORTO,SEGNALAZIONI_FO_PREVENTIVI.NUMERO,TO_CHAR(TO_DATE(SEGNALAZIONI_FO_PREVENTIVI.DATA_PR,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_PR,TO_CHAR(TO_DATE(SEGNALAZIONI_FO_PREVENTIVI.DATA_INIZIO,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INIZIO,TO_CHAR(TO_DATE(SEGNALAZIONI_FO_PREVENTIVI.DATA_FINE,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_FINE,SEGNALAZIONI_FO_PREV_STATI.DESCRIZIONE AS STATO from SISCOM_MI.SEGNALAZIONI_FO_PREVENTIVI,SISCOM_MI.SEGNALAZIONI_FO_PREV_STATI WHERE SEGNALAZIONI_FO_PREV_STATI.ID (+)=SEGNALAZIONI_FO_PREVENTIVI.ID_STATO AND ID_SEGNALAZIONE=" & sIndice & " order by data_pr"
        sStrSqlPreventivi = "select SEGNALAZIONI_FO_PREVENTIVI.ID,SEGNALAZIONI_FO_PREVENTIVI.DESCRIZIONE,(SELECT TO_CHAR(SUM(NVL(IMPORTO,0)),'9G999G999G999G999G990D99') FROM SISCOM_MI.SEGNALAZIONI_FO_PREV_DETT WHERE ID_PREVENTIVO=SEGNALAZIONI_FO_PREVENTIVI.ID) AS IMPORTO,SEGNALAZIONI_FO_PREVENTIVI.NUMERO,substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_PR,7,2)||'/'||substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_PR,5,2)||'/'||substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_PR,1,4) AS DATA_PR,substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_INIZIO,7,2)||'/'||substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_INIZIO,5,2)||'/'||substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_INIZIO,1,4) AS DATA_INIZIO,substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_FINE,7,2)||'/'||substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_FINE,5,2)||'/'||substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_FINE,1,4) AS DATA_FINE,SEGNALAZIONI_FO_PREV_STATI.DESCRIZIONE AS STATO from SISCOM_MI.SEGNALAZIONI_FO_PREVENTIVI,SISCOM_MI.SEGNALAZIONI_FO_PREV_STATI WHERE SEGNALAZIONI_FO_PREV_STATI.ID (+)=SEGNALAZIONI_FO_PREVENTIVI.ID_STATO AND ID_SEGNALAZIONE=" & sIndice & " order by data_pr"
    End Sub


    Protected Sub OnItemDataBoundHandlerPR(ByVal sender As Object, ByVal e As GridItemEventArgs)
        Try
            Select Case e.Item.OwnerTableView.Name
                Case "Dettagli"

                Case Else
                    If (TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode) Then
                        If e.Item.DataItem.GetType.Name = "DataRowView" Then
                            Dim radCombo1 As RadDatePicker = CType(e.Item.FindControl("txtDataPR"), RadDatePicker)
                            radCombo1.SelectedDate = CDate(par.IfNull(e.Item.DataItem("DATA_PR"), ""))

                            radCombo1 = CType(e.Item.FindControl("txtDataInizioPR"), RadDatePicker)
                            radCombo1.SelectedDate = CDate(par.IfNull(e.Item.DataItem("DATA_INIZIO"), ""))

                            radCombo1 = CType(e.Item.FindControl("txtDataFinePR"), RadDatePicker)
                            radCombo1.SelectedDate = CDate(par.IfNull(e.Item.DataItem("DATA_FINE"), ""))
                        End If
                    End If
            End Select

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Intervento - OnItemDataBoundHandlerPR - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridPreventivi_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridPreventivi.DeleteCommand
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)

            Select Case e.Item.OwnerTableView.Name

                Case "Dettagli"
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.SEGNALAZIONI_FO_PREV_DETT WHERE ID=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("IDS").ToString()
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(True)

                    'e.Item.OwnerTableView.ClearEditItems()
                    'RadGridPreventivi.MasterTableView.Rebind()
                    'If e.Item.OwnerTableView.Name = "WSTable" Then 'check if its DetailTable
                    '    RadGridPreventivi.MasterTableView.Items("ID").Expanded = True
                    'End If

                Case Else
                    par.cmd.CommandText = "delete from SISCOM_MI.SEGNALAZIONI_FO_PREVENTIVI where id=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F262','Numero " & par.PulisciStrSql(e.Item.Cells(4).Text) & " del " & par.PulisciStrSql(e.Item.Cells(5).Text) & "')"
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(True)
                    RadGridPreventivi.Rebind()
            End Select

            VisualizzaAlert("Operazione effettuata", 1)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - EliminaPreventivo - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridPreventivi_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadGridPreventivi.DetailTableDataBind
        Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        Select Case e.DetailTableView.Name
            Case "Dettagli"
                If True Then
                    Dim IndiceS As String = Replace(dataItem("ID").Text, "&nbsp;", "-1")

                    Dim sStrSql1 As String = "SELECT ID AS IDS,DESCRIZIONE AS DESCRIZIONEP,IMPORTO AS IMPORTOP FROM SISCOM_MI.SEGNALAZIONI_FO_PREV_DETT WHERE ID_PREVENTIVO=" & IndiceS



                    e.DetailTableView.DataSource = par.getDataTableGrid(sStrSql1)

                End If
        End Select
    End Sub

    Protected Sub RadGridPreventivi_EditCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridPreventivi.EditCommand

    End Sub

    Protected Sub RadGridPreventivi_InsertCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridPreventivi.InsertCommand
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)
            Select Case e.Item.OwnerTableView.Name

                Case "Dettagli"
                    Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)


                    Dim DescrizionePR As String = ""
                    DescrizionePR = par.PulisciStrSql(CType(userControl.FindControl("txtDescrizionePR"), RadTextBox).Text)

                    Dim ImportoPR As String = ""
                    ImportoPR = par.PulisciStrSql(CType(userControl.FindControl("txtImportoPR"), RadNumericTextBox).Text)


                    Dim parentItem As GridDataItem = DirectCast(e.Item.OwnerTableView.ParentItem, GridDataItem)

                    par.cmd.CommandText = "Insert into SISCOM_MI.SEGNALAZIONI_FO_PREV_DETT (ID, ID_PREVENTIVO, DESCRIZIONE, IMPORTO)  Values (SISCOM_MI.SEQ_SEGNALAZIONI_FO_PREV_DETT.nextval," & parentItem.OwnerTableView.DataKeyValues(parentItem.ItemIndex)("ID").ToString() & ", '" & par.PulisciStrSql(DescrizionePR) & "'," & par.VirgoleInPunti(ImportoPR) & ")"
                    par.cmd.ExecuteNonQuery()

                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F261','Numero " & par.PulisciStrSql(NumeroPR) & " del " & par.FormattaData(DataInizioPR) & "')"
                    'par.cmd.ExecuteNonQuery()
                Case Else
                    Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)


                    Dim NumeroPR As String = ""
                    NumeroPR = par.PulisciStrSql(CType(userControl.FindControl("txNumPR"), RadTextBox).Text)

                    Dim DataPR As String = ""
                    DataPR = CType(userControl.FindControl("txtDataPR"), RadDatePicker).SelectedDate
                    If DataPR <> "" Then
                        DataPR = par.AggiustaData(DataPR)
                    End If

                    Dim DataInizioPR As String = ""
                    DataInizioPR = CType(userControl.FindControl("txtDataInizioPR"), RadDatePicker).SelectedDate
                    If DataInizioPR <> "" Then
                        DataInizioPR = par.AggiustaData(DataInizioPR)
                    End If

                    Dim DataFinePR As String = ""
                    DataFinePR = CType(userControl.FindControl("txtDataFinePR"), RadDatePicker).SelectedDate
                    If DataFinePR <> "" Then
                        DataFinePR = par.AggiustaData(DataFinePR)
                    End If

                    Dim DescrizionePreventivo As String = ""
                    DescrizionePreventivo = par.PulisciStrSql(CType(userControl.FindControl("txtDescrizionePR"), RadTextBox).Text)

                    par.cmd.CommandText = "Insert into SISCOM_MI.SEGNALAZIONI_FO_PREVENTIVI (ID, ID_SEGNALAZIONE, DATA_PR, ID_STATO, DATA_INIZIO,  DATA_FINE, IMPORTO, NUMERO, DESCRIZIONE)  Values (SISCOM_MI.SEQ_SEGNALAZIONI_FO_PR.nextval," & sIndice & ", '" & DataPR & "',2, '" & DataInizioPR & "', '" & DataFinePR & "', 0,'" & NumeroPR & "','" & DescrizionePreventivo & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F261','Numero " & par.PulisciStrSql(NumeroPR) & " del " & par.FormattaData(DataInizioPR) & "')"
                    par.cmd.ExecuteNonQuery()
            End Select

            connData.chiudi(True)
            CaricaDati()
            VisualizzaAlert("Operazione effettuata", 1)
            RadGridPreventivi.Rebind()

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Inserisci Preventivo - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridPreventivi_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridPreventivi.ItemCommand
        Try
            If e.CommandName = "Aggiorna" Then
                RadGridPreventivi.Rebind()
                Return
            End If
            If e.CommandName = RadGrid.InitInsertCommandName Then '"Add new" button clicked

                Dim editColumn As GridEditCommandColumn = CType(RadGridPreventivi.MasterTableView.GetColumn("EditCommandColumn"), GridEditCommandColumn)
                editColumn.Visible = False

            ElseIf (e.CommandName = RadGrid.RebindGridCommandName AndAlso e.Item.OwnerTableView.IsItemInserted) Then
                e.Canceled = True
            Else
                Dim editColumn As GridEditCommandColumn = CType(RadGridPreventivi.MasterTableView.GetColumn("EditCommandColumn"), GridEditCommandColumn)
                If Not editColumn.Visible Then
                    editColumn.Visible = True
                End If
            End If

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Preventivi - ItemCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridPreventivi_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridPreventivi.ItemCreated
        If sStatoIntervento.Value = "6" Or sStatoIntervento.Value = "1" Then
            If TypeOf e.Item Is GridCommandItem Then
                Dim citem As GridCommandItem = DirectCast(e.Item, GridCommandItem)
                Dim Cb As LinkButton = DirectCast(citem.FindControl("LinkButton3"), LinkButton)
                Cb.Visible = False
            End If
        End If
    End Sub

    Protected Sub RadGridPreventivi_ItemDeleted(sender As Object, e As Telerik.Web.UI.GridDeletedEventArgs) Handles RadGridPreventivi.ItemDeleted
        Try
            If Not e.Exception Is Nothing Then
                e.ExceptionHandled = True

            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Preventivi - ItemDeleted - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridPreventivi_ItemInserted(sender As Object, e As Telerik.Web.UI.GridInsertedEventArgs) Handles RadGridPreventivi.ItemInserted
        Try
            If Not e.Exception Is Nothing Then
                e.ExceptionHandled = True
                e.KeepInInsertMode = False
                'DisplayMessage(True, "Employee cannot be inserted. Reason: " + e.Exception.Message)
            Else
                'DisplayMessage(False, "Employee inserted")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Allegati - ItemInserted - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridPreventivi_ItemUpdated(sender As Object, e As Telerik.Web.UI.GridUpdatedEventArgs) Handles RadGridPreventivi.ItemUpdated
        Try
            If Not e.Exception Is Nothing Then
                e.KeepInEditMode = True
                e.ExceptionHandled = True
                'DisplayMessage(True, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " cannot be updated. Reason: " + e.Exception.Message)
            Else
                'DisplayMessage(False, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " updated")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Allegati - ItemUpdated - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridPreventivi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridPreventivi.NeedDataSource
        If sStrSqlPreventivi <> "" Then
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSqlPreventivi)
        End If
    End Sub

    Protected Sub RadGridPreventivi_PreRender(sender As Object, e As System.EventArgs) Handles RadGridPreventivi.PreRender
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)

    End Sub


    Protected Sub RadGridPreventivi_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridPreventivi.UpdateCommand
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)

            Select Case e.Item.OwnerTableView.Name
                Case "Dettagli"
                    Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)

                    Dim indicePR As String = ""
                    indicePR = par.PulisciStrSql(CType(userControl.FindControl("txtIndicePR"), RadTextBox).Text)

                    Dim DescrizionePR As String = ""
                    DescrizionePR = par.PulisciStrSql(CType(userControl.FindControl("txtDescrizionePR"), RadTextBox).Text)

                    Dim ImportoPR As String = ""
                    ImportoPR = par.PulisciStrSql(CType(userControl.FindControl("txtImportoPR"), RadNumericTextBox).Text)

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FO_PREV_DETT SET DESCRIZIONE='" & par.PulisciStrSql(DescrizionePR) & "', IMPORTO=" & par.VirgoleInPunti(ImportoPR) & " WHERE ID=" & indicePR
                    par.cmd.ExecuteNonQuery()

                    connData.chiudi(True)

                Case Else
                    Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)


                    Dim NumeroPR As String = ""
                    NumeroPR = CType(userControl.FindControl("txNumPR"), RadTextBox).Text

                    Dim DataPR As String = ""
                    DataPR = CType(userControl.FindControl("txtDataPR"), RadDatePicker).SelectedDate
                    If DataPR <> "" Then
                        DataPR = par.AggiustaData(DataPR)
                    End If

                    Dim DataInizioPR As String = ""
                    DataInizioPR = CType(userControl.FindControl("txtDataInizioPR"), RadDatePicker).SelectedDate
                    If DataInizioPR <> "" Then
                        DataInizioPR = par.AggiustaData(DataInizioPR)
                    End If

                    Dim DataFinePR As String = ""
                    DataFinePR = CType(userControl.FindControl("txtDataFinePR"), RadDatePicker).SelectedDate
                    If DataFinePR <> "" Then
                        DataFinePR = par.AggiustaData(DataFinePR)
                    End If

                    Dim DescrizionePreventivo As String = ""
                    DescrizionePreventivo = CType(userControl.FindControl("txtDescrizionePR"), RadTextBox).Text

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FO_PREVENTIVI SET NUMERO='" & par.PulisciStrSql(NumeroPR) & "',DESCRIZIONE='" & par.PulisciStrSql(DescrizionePreventivo) & "',DATA_FINE='" & DataFinePR & "',DATA_INIZIO='" & DataInizioPR & "',DATA_PR='" & DataPR & "' WHERE  ID = " & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F263','Numero " & par.PulisciStrSql(NumeroPR) & " del " & par.PulisciStrSql(par.FormattaData(DataPR)) & "')"
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(True)
                    RadGridPreventivi.Rebind()
                    'RadGridPreventivi.EditIndexes.Clear()
            End Select


            VisualizzaAlert("Operazione effettuata", 1)




        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Preventivi - UpdateCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    'Protected Sub RadButton1_Click(sender As Object, e As System.EventArgs) Handles RadButton1.Click
    '    RadGridPreventivi.Rebind()
    'End Sub

    Protected Sub btnFineLavori_Click(sender As Object, e As System.EventArgs) Handles btnFineLavori.Click
        If txtDataPGI.SelectedDate.ToString <> "" And txtDataTDL.SelectedDate.ToString <> "" And txtDataFineLavori.SelectedDate.ToString <> "" Then
            If txtDataFineLavori.SelectedDate < txtDataPGI.SelectedDate Then
                VisualizzaAlert("Non è possibile procedere. La DATA TERMINE LAVORAZIONE deve essere successiva alla data DPIL!", 2)
                Exit Sub
            End If
            If lblRDORichiesto.Visible = True And sStatoPreventivo.Value <> "3" Then
                VisualizzaAlert("Non è possibile procedere. Il preventivo non è stato APPROVATO!", 2)
                Exit Sub
            End If
            If txtDataFineLavori.SelectedDate > Now Then
                VisualizzaAlert("Non è possibile procedere. FINE INTERVENTO deve essere precedente/uguale alla data odierna", 2)
                Exit Sub
            End If
            Try
                Me.connData = New CM.datiConnessione(par, False, False)
                connData.apri()
                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FORNITORI SET ID_STATO=8,DATA_FINE_INTERVENTO='" & Format(txtDataFineLavori.SelectedDate, "yyyyMMdd") & "' WHERE ID=" & sIndice
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F266','')"
                par.cmd.ExecuteNonQuery()
                connData.chiudi()
                sStatoIntervento.Value = "8"
                btnCarico.Enabled = False
                lblStatoIntervento.Text = "EVASO"
                HModificato.Value = "0"
                btnFineLavori.Enabled = False
                RadGridPreventivi.Rebind()
                VisualizzaAlert("Operazione effettuata", 1)
            Catch ex As Exception
                connData.chiudi()
                Session.Add("ERRORE", "Provenienza: Fornitori - FineLavori - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        Else

            VisualizzaAlert("Non è possibile procedere. Inserire le date DPIL e DPFL e DATA TERMINE LAVORAZIONE!", 2)
        End If
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click

    End Sub

    Protected Sub btnContabilizza_Click(sender As Object, e As System.EventArgs) Handles btnContabilizza.Click
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri()
            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FORNITORI SET ID_STATO=3,FL_PR_CONTAB=2 WHERE ID=" & sIndice
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndice & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F267','')"
            par.cmd.ExecuteNonQuery()
            connData.chiudi()
            sStatoIntervento.Value = "3"
            btnCarico.Enabled = False
            lblStatoIntervento.Text = "DA CONSUNTIVARE"
            HModificato.Value = "0"
            btnFineLavori.Enabled = False
            btnContabilizza.Enabled = False
            RadGridPreventivi.Rebind()
            VisualizzaAlert("Operazione effettuata", 1)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - Contabilizza - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub VisualizzaAlert(ByVal TestoMessaggio As String, ByVal Tipo As Integer)
        Select Case Tipo
            Case 1
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','success');", True)
            Case 2
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "warn", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','warn');", True)
            Case 3
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "error", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','error');", True)
        End Select
    End Sub

    Private Sub RadGridIrregolarita_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGridIrregolarita.NeedDataSource
        Try
            If sStrSqlIrregolarita <> "" Then
                RadGridIrregolarita.DataSource = par.getDataTableGrid(sStrSqlIrregolarita)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Non Conformità - NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub


End Class
