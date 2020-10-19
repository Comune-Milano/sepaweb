'*** STAMPA RISULTATO RICERCA MANUTENZIONE PRE-INSERIMENTO

Partial Class ReportRisultatoManuINS
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String
    Dim sWhere As String
    Dim sOrder As String

    Dim sValoreEsercizioFinanziarioR As String

    Dim sValoreIndirizzoR As String
    Dim sValoreServizioR As String
    Dim sValoreServizioVoceR As String
    Dim sValoreAppaltoR As String
    Dim sValoreComplessoR As String
    Dim sValoreEdificioR As String
    Dim sValoreTipoR As String
    Dim sValoreUbicazione As String

    Dim sOrdinamento As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sStringaSqlPatrimonio As String = ""

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            Try
                lblTitolo.Text = "ELENCO MANUTENZIONI"

                'Passato = Request.QueryString("Pas")

                sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

                sValoreIndirizzoR = Strings.Trim(Request.QueryString("IN_R"))

                sValoreServizioR = Strings.Trim(Request.QueryString("SE_R"))
                sValoreServizioVoceR = Strings.Trim(Request.QueryString("SV_R"))

                sValoreAppaltoR = Strings.Trim(Request.QueryString("AP_R"))

                sValoreComplessoR = Strings.Trim(Request.QueryString("CO_R"))
                sValoreEdificioR = Strings.Trim(Request.QueryString("ED_R"))

                sValoreTipoR = Strings.Trim(Request.QueryString("TIPOR"))
                sValoreUbicazione = Strings.Trim(Request.QueryString("UBI"))

                sOrdinamento = Request.QueryString("ORD")


                Select Case sOrdinamento
                    Case "NUM_REPERTORIO"
                        sOrder = " order by NUM_REPERTORIO asc"
                    Case "INDIRIZZO"
                        sOrder = " order by INDIRIZZO asc"
                    Case "SERVIZIO"
                        sOrder = " order by SERVIZIO asc"
                    Case "SERVIZIO_VOCE"
                        sOrder = " order by SERVIZIO_VOCE asc"
                    Case Else
                        sOrder = ""
                End Select

                Dim sFiliale As String = ""
                If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                    sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
                End If

                'sStringaSql = "select APPALTI_LOTTI_SERVIZI.ID_APPALTO," _
                '                  & " PF_VOCI_IMPORTO.ID_LOTTO," _
                '                  & " PF_VOCI_IMPORTO.ID_SERVIZIO," _
                '                  & " APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO," _
                '                  & " APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO," _
                '                 & " APPALTI.NUM_REPERTORIO," _
                '                 & " (select SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=EDIFICI.ID_COMPLESSO) as COMPLESSO," _
                '                 & " EDIFICI.DENOMINAZIONE as DESC_EDIFICIO," _
                '                 & " (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP) AS INDIRIZZO, " _
                '                 & " TAB_COMUNI.COMUNE," _
                '                 & " LOTTI.DESCRIZIONE as DESCRIZIONE_LOTTO, " _
                '                 & " TAB_SERVIZI.DESCRIZIONE as SERVIZIO, " _
                '                 & " PF_VOCI_IMPORTO.DESCRIZIONE as SERVIZIO_VOCE " _
                '         & " from  SISCOM_MI.APPALTI_LOTTI_SERVIZI, " _
                '               & " SISCOM_MI.APPALTI_LOTTI_PATRIMONIO, " _
                '               & " SISCOM_MI.APPALTI, " _
                '               & " SISCOM_MI.LOTTI, " _
                '               & " SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO, " _
                '               & " SISCOM_MI.EDIFICI,SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_COMUNI " _
                '         & " where " _
                '         & "       APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                '         & "   and EDIFICI.ID_INDIRIZZO_PRINCIPALE=INDIRIZZI.ID (+) " _
                '         & "   and INDIRIZZI.COD_COMUNE=TAB_COMUNI.COD_COM (+) " _
                '         & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) " _
                '         & "   and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+) " _
                '         & "   and PF_VOCI_IMPORTO.ID_LOTTO=SISCOM_MI.LOTTI.ID (+) " _
                '         & "   and PF_VOCI_IMPORTO.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+) "


                If sValoreTipoR = 0 Or sValoreTipoR = 2 Then

                    DataGrid1.Visible = True
                    DataGrid2.Visible = False
                    sStringaSql = "select APPALTI_LOTTI_SERVIZI.ID_APPALTO,PF_VOCI_IMPORTO.ID_LOTTO,PF_VOCI_IMPORTO.ID_SERVIZIO," _
                                      & " APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO,APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO as ""ID_UBICAZIONE""," _
                                      & " APPALTI.NUM_REPERTORIO," _
                                      & " (select DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=EDIFICI.ID_COMPLESSO) AS COMPLESSO, " _
                                      & " EDIFICI.DENOMINAZIONE as DESC_EDIFICIO, " _
                                      & " (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP)  AS INDIRIZZO, " _
                                      & " LOTTI.DESCRIZIONE as DESCRIZIONE_LOTTO, " _
                                      & " TAB_SERVIZI.DESCRIZIONE as SERVIZIO, " _
                                      & " PF_VOCI_IMPORTO.DESCRIZIONE as SERVIZIO_VOCE,APPALTI.DESCRIZIONE AS DESCRIZIONE_APPALTI " _
                             & " from  SISCOM_MI.APPALTI_LOTTI_SERVIZI, " _
                                   & " SISCOM_MI.APPALTI_LOTTI_PATRIMONIO, " _
                                   & " SISCOM_MI.APPALTI, " _
                                   & " SISCOM_MI.LOTTI, " _
                                   & " SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO, " _
                                   & " SISCOM_MI.EDIFICI  ,SISCOM_MI.INDIRIZZI " _
                             & " where " _
                             & "       APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                             & "   and APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO is null  " _
                             & "   and EDIFICI.ID_INDIRIZZO_PRINCIPALE=INDIRIZZI.ID (+) " _
                             & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) " _
                             & "   and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+) " _
                             & "   and PF_VOCI_IMPORTO.ID_LOTTO=SISCOM_MI.LOTTI.ID (+) " _
                             & "   and PF_VOCI_IMPORTO.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+) "

                    'SERVIZIO
                    If sValoreTipoR = 0 Then
                        'NORMALE
                        sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO (+)"

                        If par.IfEmpty(sValoreServizioR, "-1") <> "-1" Then
                            'RICERCA 1
                            sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO=" & sValoreServizioR _
                                                      & " and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "
                            sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                                                                                        & " where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & sFiliale & ")"
                        Else
                            'RICERCA 2 (se si ferma all'indirizzo)

                            sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "
                            sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                                                                                        & " where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & sFiliale & ")"
                        End If
                    Else
                        'FUORI LOTTO
                        If par.IfEmpty(sValoreServizioR, "-1") <> "-1" Then
                            sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO=" & sValoreServizioR _
                                                      & " and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "
                            sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                                                                                        & " where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & sFiliale & ")"
                        End If
                    End If
                    '*****************************************************

                    'SERVIZIO VOCE
                    If par.IfEmpty(sValoreServizioVoceR, "-1") <> "-1" Then
                        If sValoreTipoR = 0 Then
                            'NORMALE
                            sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & sValoreServizioVoceR
                        Else
                            'FUORI LOTTO
                            sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO<>" & sValoreServizioVoceR _
                                                      & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                                                                                                       & " where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=" & sValoreServizioVoceR & ")) "
                        End If
                    End If
                    '*****************************************************


                    'APPALTO
                    If sValoreTipoR = 0 Then
                        'NORNALE
                        If par.IfEmpty(sValoreAppaltoR, "-1") <> "-1" Then
                            sStringaSql = sStringaSql & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO=" & sValoreAppaltoR
                        Else
                            sStringaSql = sStringaSql & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1) "
                        End If
                    Else
                        'FUORI LOTTO
                        If par.IfEmpty(sValoreAppaltoR, "-1") <> "-1" Then
                            sStringaSql = sStringaSql & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO<>" & sValoreAppaltoR _
                                                      & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO in (select ID_APPALTO " _
                                                                                                  & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                                  & " where ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                                & " where ID_SERVIZIO <>15 " _
                                                                                                                                & "   and ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                                                                                                                                                    & " where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & sFiliale & ")"

                            If par.IfEmpty(sValoreServizioR, "-1") <> "-1" Then
                                sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO=" & sValoreServizioR & ")"
                            Else
                                sStringaSql = sStringaSql & ")"
                            End If

                            If par.IfEmpty(sValoreServizioVoceR, "-1") <> "-1" Then
                                sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & sValoreServizioVoceR
                            End If

                            sStringaSql = sStringaSql & ")"
                        End If
                    End If
                    '***********************************************

                    'INDIRIZZO/COMPLESSO/EDIFICIO
                    If par.IfEmpty(sValoreEdificioR, "-1") <> "-1" Then
                        sStringaSqlPatrimonio = " and  APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=" & sValoreEdificioR

                    ElseIf par.IfEmpty(sValoreComplessoR, "-1") <> "-1" Then
                        sStringaSqlPatrimonio = " and APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI where ID_COMPLESSO=" & sValoreComplessoR & ")"

                    ElseIf par.IfEmpty(sValoreIndirizzoR, "-1") <> "-1" Then
                        sStringaSqlPatrimonio = " and  APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%') ) "
                    End If
                    '****************************************

                    sStringaSql = sStringaSql & sStringaSqlPatrimonio

                Else
                    'LOTTO SOLO IMPIANTI
                    DataGrid1.Visible = False
                    DataGrid2.Visible = True

                    sStringaSql = sStringaSql & "select APPALTI_LOTTI_SERVIZI.ID_APPALTO,PF_VOCI_IMPORTO.ID_LOTTO,PF_VOCI_IMPORTO.ID_SERVIZIO," _
                          & " APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO,IMPIANTI.ID as ""ID_UBICAZIONE""," _
                          & " APPALTI.NUM_REPERTORIO," _
                          & " (select DENOMINAZIONE  from SISCOM_MI.COMPLESSI_IMMOBILIARI" _
                          & "   where ID=IMPIANTI.ID_COMPLESSO ) AS COMPLESSO, " _
                          & " EDIFICI.DENOMINAZIONE as DESC_EDIFICIO, " _
                       & " CASE when IMPIANTI.ID_EDIFICIO is null THEN " _
                         & "   (select INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP from SISCOM_MI.INDIRIZZI    " _
                         & "     where ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI where  ID=IMPIANTI.ID_COMPLESSO)) " _
                    & " ELSE " _
                   & "   (select distinct(INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP) from SISCOM_MI.INDIRIZZI    " _
                      & "     where ID in (select ID_INDIRIZZO_PRINCIPALE  from SISCOM_MI.EDIFICI where  ID=IMPIANTI.ID_EDIFICIO) )   " _
                    & " END  as INDIRIZZO," _
                          & " TIPOLOGIA_IMPIANTI.DESCRIZIONE as TIPO_IMPIANTO,IMPIANTI.COD_IMPIANTO," _
                          & " LOTTI.DESCRIZIONE as DESCRIZIONE_LOTTO, " _
                          & " TAB_SERVIZI.DESCRIZIONE as SERVIZIO, " _
                          & " PF_VOCI_IMPORTO.DESCRIZIONE as SERVIZIO_VOCE,APPALTI.DESCRIZIONE AS DESCRIZIONE_APPALTI " _
                 & " from  SISCOM_MI.APPALTI_LOTTI_SERVIZI, " _
                       & " SISCOM_MI.APPALTI_LOTTI_PATRIMONIO, " _
                       & " SISCOM_MI.APPALTI, " _
                       & " SISCOM_MI.LOTTI, " _
                       & " SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO, " _
                       & " SISCOM_MI.IMPIANTI,SISCOM_MI.EDIFICI,SISCOM_MI.TIPOLOGIA_IMPIANTI  " _
                 & " where " _
                 & "       APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO is null  " _
                 & "   and APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+)  " _
                 & "   and IMPIANTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) " _
                 & "   and IMPIANTI.COD_TIPOLOGIA=TIPOLOGIA_IMPIANTI.COD (+) " _
                 & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) " _
                 & "   and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+) " _
                 & "   and PF_VOCI_IMPORTO.ID_LOTTO=SISCOM_MI.LOTTI.ID (+) " _
                 & "   and PF_VOCI_IMPORTO.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+) "

                    'SERVIZIO
                    If sValoreTipoR = 1 Then
                        'NORMALE
                        sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO (+)"

                        If par.IfEmpty(sValoreServizioR, "-1") <> "-1" Then
                            'RICERCA 1
                            sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO=" & sValoreServizioR _
                                                      & " and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "
                            sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                                                                                        & " where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & sFiliale & ")"
                        Else
                            'RICERCA 2 (se si ferma all'indirizzo)

                            sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "
                            sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                                                                                        & " where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & sFiliale & ")"
                        End If
                    Else
                        'FUORI LOTTO
                        If par.IfEmpty(sValoreServizioR, "-1") <> "-1" Then
                            sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO=" & sValoreServizioR _
                                                      & " and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "
                            sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                                                                                        & " where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & sFiliale & ")"
                        End If
                    End If
                    '*****************************************************

                    'SERVIZIO VOCE
                    If par.IfEmpty(sValoreServizioVoceR, "-1") <> "-1" Then
                        If sValoreTipoR = 1 Then
                            'NORMALE
                            sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & sValoreServizioVoceR
                        Else
                            'FUORI LOTTO
                            sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO<>" & sValoreServizioVoceR _
                                                      & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                                                                                                       & " where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=" & sValoreServizioVoceR & ")) "
                        End If
                    End If
                    '*****************************************************


                    'APPALTO
                    If sValoreTipoR = 1 Then
                        'NORNALE
                        If par.IfEmpty(sValoreAppaltoR, "-1") <> "-1" Then
                            sStringaSql = sStringaSql & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO=" & sValoreAppaltoR
                        Else
                            sStringaSql = sStringaSql & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1) "
                        End If
                    Else
                        'FUORI LOTTO
                        If par.IfEmpty(sValoreAppaltoR, "-1") <> "-1" Then
                            sStringaSql = sStringaSql & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO<>" & sValoreAppaltoR _
                                                      & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO in (select ID_APPALTO " _
                                                                                                  & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                                  & " where ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                                & " where ID_SERVIZIO <>15 " _
                                                                                                                                & "   and ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                                                                                                                                                    & " where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & sFiliale & ")"

                            If par.IfEmpty(sValoreServizioR, "-1") <> "-1" Then
                                sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO=" & sValoreServizioR & ")"
                            Else
                                sStringaSql = sStringaSql & ")"
                            End If

                            If par.IfEmpty(sValoreServizioVoceR, "-1") <> "-1" Then
                                sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & sValoreServizioVoceR
                            End If

                            sStringaSql = sStringaSql & ")"
                        End If
                    End If
                    '***********************************************

                    'INDIRIZOO/COMPLESSO/EDIFICIO
                    If par.IfEmpty(sValoreEdificioR, "-1") <> "-1" Then
                        sStringaSqlPatrimonio = " and  APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI where ID_EDIFICIO=" & sValoreEdificioR & ")"

                    ElseIf par.IfEmpty(sValoreComplessoR, "-1") <> "-1" Then
                        sStringaSqlPatrimonio = " and APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI where ID_COMPLESSO=" & sValoreComplessoR & " )"

                    ElseIf par.IfEmpty(sValoreIndirizzoR, "-1") <> "-1" Then
                        sStringaSqlPatrimonio = " and  ( APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_INDIRIZZO_RIFERIMENTO in (select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%')) ) " _
                                                & " or    APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI where ID_EDIFICIO  in (select ID from SISCOM_MI.EDIFICI where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%')) ) )"

                    End If
                    '****************************************

                    sStringaSql = sStringaSql & sStringaSqlPatrimonio
                End If


                sStringaSql = sStringaSql & sOrder




                par.OracleConn.Open()
                Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSql, par.OracleConn)
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

                lblTotale.Text = "0"
                Do While myReader.Read()
                    lblTotale.Text = CInt(lblTotale.Text) + 1
                Loop

                lblTotale.Text = "TOTALE MANUTENZIONI TROVATE: " & lblTotale.Text
                myReader.Close()

                '*** CARICO LA GRIGLIA
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds)

                If DataGrid1.Visible = True Then
                    DataGrid1.DataSource = ds
                    DataGrid1.DataBind()
                Else
                    DataGrid2.DataSource = ds
                    DataGrid2.DataBind()
                End If


                'If sValoreImpianto = "SO" Then
                '    DataGrid2.DataSource = ds
                '    DataGrid2.DataBind()
                'Else
                '    DataGrid1.DataSource = ds
                '    DataGrid1.DataBind()
                '    '*******************************
                'End If


                par.cmd.Dispose()
                par.OracleConn.Close()
                par.OracleConn.Dispose()

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch EX1 As Oracle.DataAccess.Client.OracleException
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            Catch ex As Exception
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            End Try
        End If



    End Sub

    Public Property Passato() As String
        Get
            If Not (ViewState("par_Passato") Is Nothing) Then
                Return CStr(ViewState("par_Passato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Passato") = value
        End Set

    End Property


End Class
