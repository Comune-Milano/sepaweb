﻿Imports Telerik.Web.UI
Imports System.Data

'*** LISTA RISULTATO MANUTENZIONI RRS Proviene da : RicercaRRS.aspx

Partial Class RRS_RisultatiRRS
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Public sStringaSql As String
    Dim sWhere As String
    Dim sOrder As String

    Public sValoreEsercizioFinanziarioR As String

    Public sValoreStruttura As String

    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreVoce As String

    Public sValoreFornitore As String
    Public sValoreAppalto As String

    Public sValoreUnita As String

    Public sValoreData_Dal As String
    Public sValoreData_Al As String

    Public sValoreProvenienza As String
    Public sValoreStato As String
    Public sOrdinamento As String
    Public sAutorizzazione As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If


        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID

            

            '*LBLID.Text = Request.QueryString("T")
        End If
    End Sub

    'Private Sub BindGrid()
    '    Dim FlagConnessione As Boolean = False

    '    Try

    '        ' APRO CONNESSIONE
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            Exit Sub
    '        Else
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '            FlagConnessione = True
    '        End If



    '        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

    '        'Dim dt As New Data.DataTable

    '        'da.Fill(dt) ', "DOMANDE_BANDO,COMP_NUCLEO")

    '        'DataGrid1.DataSource = dt
    '        'DataGrid1.DataBind()
    '        'Label1.Text = " " & dt.Rows.Count


    '        '************CHIUSURA CONNESSIONE**********
    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '            FlagConnessione = False
    '        End If

    '    Catch ex As Exception

    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '            FlagConnessione = False
    '        End If

    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try

    'End Sub


    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(e.Item.Cells(2).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
    '    End If

    'End Sub


    'Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
    '    If e.NewPageIndex >= 0 Then

    '        DataGrid1.CurrentPageIndex = e.NewPageIndex
    '        BindGrid()
    '    End If
    'End Sub


    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property


    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Else
            Session.Add("ID", txtid.Text)

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreStruttura = Request.QueryString("STR")

            sValoreComplesso = Strings.Trim(Request.QueryString("CO"))
            sValoreEdificio = Strings.Trim(Request.QueryString("ED"))
            sValoreVoce = Strings.Trim(Request.QueryString("SV"))
            sValoreFornitore = Strings.Trim(Request.QueryString("FO"))
            sValoreAppalto = Strings.Trim(Request.QueryString("AP"))

            sValoreUnita = Strings.Trim(Request.QueryString("UI"))

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            sValoreStato = Request.QueryString("ST")


            sValoreProvenienza = Request.QueryString("PROVENIENZA")
            sOrdinamento = Request.QueryString("ORD")


            Response.Write("<script>location.replace('ManutenzioniRRS.aspx?CO=" & sValoreComplesso _
                                                                        & "&ED=" & sValoreEdificio _
                                                                        & "&SV=" & sValoreVoce _
                                                                        & "&FO=" & sValoreFornitore _
                                                                        & "&AP=" & sValoreAppalto _
                                                                        & "&DAL=" & sValoreData_Dal _
                                                                        & "&AL=" & sValoreData_Al _
                                                                        & "&UI=" & sValoreUnita _
                                                                        & "&ST=" & sValoreStato _
                                                                        & "&STR=" & sValoreStruttura _
                                                                        & "&ORD=" & sOrdinamento _
                                                                        & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                        & "&PROVENIENZA=" & sValoreProvenienza & "');</script>")


        End If
    End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        Session.Remove("IMP1")

        sValoreProvenienza = Strings.Trim(Request.QueryString("PROVENIENZA"))

        'If sValoreProvenienza = "RICERCA_SFITTI" Then
        '    Response.Write("<script>document.location.href=""RicercaRRS_Sfitti.aspx""</script>")
        'Else
        Response.Write("<script>document.location.href=""RicercaRRS.aspx""</script>")
        'End If
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub
    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(dataItem("ODL_ANNO").Text, "'", "\'") & "';document.getElementById('txtid').value='" & dataItem("ID_MANUTENZIONE").Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnVisualizza').click();")
        End If
    End Sub

    Protected Sub DataGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGrid1.ItemCommand
        Try
            If e.CommandName = RadGrid.ExportToExcelCommandName Then
                isExporting.Value = "1"
            End If
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreStruttura = Request.QueryString("STR")

            sValoreComplesso = Strings.Trim(Request.QueryString("CO"))
            sValoreEdificio = Strings.Trim(Request.QueryString("ED"))
            sValoreVoce = Strings.Trim(Request.QueryString("SV"))
            sValoreFornitore = Strings.Trim(Request.QueryString("FO"))
            sValoreAppalto = Strings.Trim(Request.QueryString("AP"))

            sValoreUnita = Strings.Trim(Request.QueryString("UI"))

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            sValoreStato = Request.QueryString("ST")


            sValoreProvenienza = Request.QueryString("PROVENIENZA")
            sOrdinamento = Request.QueryString("ORD")
            sAutorizzazione = Request.QueryString("AUT")
            Dim condizioneAutorizzazione As String = ""
            Select Case sAutorizzazione
                Case "0"
                    condizioneAutorizzazione = " AND MANUTENZIONI.FL_AUTORIZZAZIONE=0 "
                Case "1"
                    condizioneAutorizzazione = " AND MANUTENZIONI.FL_AUTORIZZAZIONE=1 "
                Case "-1"
            End Select

            Select Case sOrdinamento
                Case "UBICAZIONE"
                    sOrder = " order by UBICAZIONE asc"
                Case "VOCE"
                    sOrder = " order by VOCE,UBICAZIONE"
                Case "DATA ODL"
                    sOrder = " order by DATA_ODL desc"
                Case "NUM REPERTORIO"
                    sOrder = " order by NUM_REPERTORIO"
                Case "FORNITORE"
                    sOrder = " order by FORNITORE,UBICAZIONE"

                Case Else
                    sOrder = ""
            End Select

            sStringaSql = ""

            If par.IfEmpty(sValoreEdificio, "-1") = "-1" Then
                sStringaSql = "SELECT " _
                    & " SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                    & " SISCOM_MI.APPALTI.NUM_REPERTORIO," _
                    & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) AS ""ODL_ANNO""," _
                    & " TO_DATE(SUBSTR(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYMMDD') AS ""DATA_INIZIO_ORDINE""," _
                    & " SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""UBICAZIONE""," _
                    & " TRIM ( TO_CHAR ( NVL ( (SELECT sum(MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO) FROM SISCOM_MI.manutenzioni_interventi WHERE manutenzioni_interventi.id_manutenzione = manutenzioni.id), 0), '9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO"" ," _
                    & " TRIM(TO_CHAR(nvl((select IMPORTO_PRENOTATO from SISCOM_MI.prenotazioni where prenotazioni.id=id_prenotazione_pagamento),0),'9G999G999G999G999G990D99')) AS ""IMPORTO_PRE"", " _
                    & " TRIM(TO_CHAR(nvl((select IMPORTO_APPROVATO from SISCOM_MI.prenotazioni where prenotazioni.id=id_prenotazione_pagamento),0),'9G999G999G999G999G990D99')) AS ""IMPORTO_CON"", " _
                    & " SISCOM_MI.PF_VOCI.DESCRIZIONE AS ""VOCE""," _
                    & " (CASE WHEN FORNITORI.RAGIONE_SOCIALE IS NOT NULL THEN COD_FORNITORE || ' - ' || TRIM(FORNITORI.RAGIONE_SOCIALE) ELSE COD_FORNITORE || ' - ' || TRIM(FORNITORI.COGNOME)|| ' ' ||TRIM(FORNITORI.NOME) END) AS ""FORNITORE""," _
                    & " SISCOM_MI.TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                    & " SISCOM_MI.MANUTENZIONI.PROGR," _
                    & " SISCOM_MI.MANUTENZIONI.ANNO," _
                    & " SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE AS  ""DATA_ODL"", " _
                    & " SISCOM_MI.MANUTENZIONI.DESCRIZIONE AS  ""DESCRIZIONE"" " _
                    & " ,(CASE WHEN SISCOM_MI.MANUTENZIONI.FL_AUTORIZZAZIONE=1 THEN 'AUTORIZZATO' else 'NON AUTORIZZATO' END) AS ""AUTORIZZAZIONE"",TO_DATE(SISCOM_MI.MANUTENZIONI.DATA_ANNULLO,'YYYYMMDD')  AS ""DATA_ANNULLO"" " _
                    & " FROM SISCOM_MI.MANUTENZIONI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.PF_VOCI,SISCOM_MI.APPALTI,SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.FORNITORI" _
                    & " WHERE   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_EDIFICIO IS NULL  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO IS NULL " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_PF_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                    & " AND SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.STATO<6 " _
                    & condizioneAutorizzazione

                If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
                End If

                If sValoreStruttura <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=" & sValoreStruttura
                End If

                If par.IfEmpty(sValoreComplesso, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=" & sValoreComplesso
                End If

                If par.IfEmpty(sValoreVoce, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.ID_PF_VOCE='" & sValoreVoce & "' "
                End If

                If par.IfEmpty(sValoreStato, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.STATO=" & sValoreStato
                End If

                If par.IfEmpty(sValoreFornitore, "-1") <> "-1" Then
                    If par.IfEmpty(sValoreAppalto, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = " & sValoreAppalto & ")"
                    Else
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & sValoreFornitore & ")"
                    End If
                ElseIf par.IfEmpty(sValoreAppalto, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = " & sValoreAppalto & ")"
                End If

                If sValoreData_Dal <> "" Then
                    If sValoreData_Al <> "" Then
                        sStringaSql = sStringaSql & " and (SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al & ")"
                    Else
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal
                    End If
                ElseIf sValoreData_Al <> "" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al
                End If

                'If sValoreProvenienza = "RICERCA_SFITTI" Then
                '    If par.IfEmpty(sValoreUnita, "-1") <> "-1" Then
                '        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_UNITA_IMMOBILIARI=" & sValoreUnita
                '    Else
                '        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_UNITA_IMMOBILIARI is not null "
                '    End If
                'End If


            End If

            If par.IfEmpty(sValoreComplesso, "-1") = "-1" Or par.IfEmpty(sValoreEdificio, "-1") <> "-1" Then
                If sStringaSql <> "" Then sStringaSql = sStringaSql & " union "
                sStringaSql = sStringaSql & "  SELECT " _
                    & " SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                    & " SISCOM_MI.APPALTI.NUM_REPERTORIO," _
                    & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) AS ""ODL_ANNO"",TO_DATE(SUBSTR(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYMMDD') AS ""DATA_INIZIO_ORDINE""," _
                    & " SISCOM_MI.EDIFICI.DENOMINAZIONE AS ""UBICAZIONE""," _
                    & " TRIM ( TO_CHAR ( NVL ( (SELECT sum(MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO) FROM SISCOM_MI.manutenzioni_interventi WHERE manutenzioni_interventi.id_manutenzione = manutenzioni.id), 0), '9G999G999G999G999G990D99')) AS ""IMPORTO_PRE"" ," _
                    & " TRIM(TO_CHAR(nvl((select IMPORTO_PRENOTATO from SISCOM_MI.prenotazioni where prenotazioni.id=id_prenotazione_pagamento),0),'9G999G999G999G999G990D99')) AS ""IMPORTO_PRE"", " _
                    & " TRIM(TO_CHAR(nvl((select IMPORTO_APPROVATO from SISCOM_MI.prenotazioni where prenotazioni.id=id_prenotazione_pagamento),0),'9G999G999G999G999G990D99')) AS ""IMPORTO_CON"", " _
                    & " SISCOM_MI.PF_VOCI.DESCRIZIONE AS ""VOCE""," _
                    & " (CASE WHEN FORNITORI.RAGIONE_SOCIALE IS NOT NULL THEN COD_FORNITORE || ' - ' || TRIM(FORNITORI.RAGIONE_SOCIALE) ELSE COD_FORNITORE || ' - ' || TRIM(FORNITORI.COGNOME)|| ' ' ||TRIM(FORNITORI.NOME) END) AS ""FORNITORE""," _
                    & " SISCOM_MI.TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                    & " SISCOM_MI.MANUTENZIONI.PROGR," _
                    & " SISCOM_MI.MANUTENZIONI.ANNO," _
                    & " SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE AS  ""DATA_ODL"", " _
                    & " SISCOM_MI.MANUTENZIONI.DESCRIZIONE AS  ""DESCRIZIONE"" " _
                    & " ,(CASE WHEN SISCOM_MI.MANUTENZIONI.FL_AUTORIZZAZIONE=1 THEN 'AUTORIZZATO' else 'NON AUTORIZZATO' END) AS ""AUTORIZZAZIONE"",TO_DATE(SISCOM_MI.MANUTENZIONI.DATA_ANNULLO,'YYYYMMDD')  AS ""DATA_ANNULLO"" " _
                    & " FROM SISCOM_MI.MANUTENZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.PF_VOCI,SISCOM_MI.APPALTI,SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.FORNITORI " _
                    & " WHERE   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO IS NULL  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO IS NULL " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_PF_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)   " _
                    & " AND SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                    & " AND SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.STATO<6 " _
                    & condizioneAutorizzazione

                If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
                End If

                If sValoreStruttura <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=" & sValoreStruttura
                End If

                If par.IfEmpty(sValoreEdificio, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.ID_EDIFICIO=" & sValoreEdificio
                End If


                If par.IfEmpty(sValoreVoce, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.ID_PF_VOCE='" & sValoreVoce & "' "
                End If

                If par.IfEmpty(sValoreStato, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.STATO=" & sValoreStato
                End If

                If par.IfEmpty(sValoreFornitore, "-1") <> "-1" Then
                    If sValoreAppalto <> -1 Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = " & sValoreAppalto & ")"
                    Else
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & sValoreFornitore & ")"
                    End If
                ElseIf par.IfEmpty(sValoreAppalto, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = " & sValoreAppalto & ")"
                End If

                If sValoreData_Dal <> "" Then
                    If sValoreData_Al <> "" Then
                        sStringaSql = sStringaSql & " and (SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al & ")"
                    Else
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal
                    End If
                ElseIf sValoreData_Al <> "" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al
                End If

                'If sValoreProvenienza = "RICERCA_SFITTI" Then
                '    If par.IfEmpty(sValoreUnita, "-1") <> "-1" Then
                '        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_UNITA_IMMOBILIARI=" & sValoreUnita
                '    Else
                '        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_UNITA_IMMOBILIARI is not null "
                '    End If
                'End If

            End If

            sStringaSQL1 = sStringaSql & " order by NUM_REPERTORIO"
            Dim dt As Data.DataTable = par.getDataTableGrid(sStringaSQL1)
            TryCast(sender, RadGrid).DataSource = dt
            Session.Item("RISULTATI_RRS") = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub DataGrid1_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemCreated
        If TypeOf e.Item Is GridFilteringItem And DataGrid1.IsExporting Then
            e.Item.Visible = False
        End If
    End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGrid1.AllowPaging = False
        DataGrid1.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGrid1.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In DataGrid1.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGrid1.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGrid1.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        DataGrid1.AllowPaging = True
        DataGrid1.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGrid1.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "MANUTENZIONI", "MANUTENZIONI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub
End Class
