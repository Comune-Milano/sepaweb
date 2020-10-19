﻿Imports Telerik.Web.UI
Imports System.Data
Imports System.IO

'*** LISTA RISULTATO MANUTENZIONI

Partial Class MANUTENZIONI_RisultatiManutenzioniD
    Inherits PageSetIdMode

    Dim par As New CM.Global


    Public sStringaSql As String
    Dim sWhere As String
    Dim sOrder As String

    Public sValoreEsercizioFinanziarioR As String

    Public sValoreRepertorio As String
    Public sValoreODL As String
    Public sValoreAnno As String
    Public sOrdinamento As String
    Private isFilter As Boolean = False

    Public sAutorizzazione As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        'Dim Str As String

        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        'Str = Str & "<" & "/div>"

        'Response.Write(Str)

        If Not IsPostBack Then

            HFGriglia.Value = DataGrid1.ClientID
            'Response.Flush()

            '*LBLID.Text = Request.QueryString("T")
        End If
    End Sub

    Private Sub BindGrid()
        Dim FlagConnessione As Boolean = False

        Try


            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            Dim dt As New Data.DataTable

            da.Fill(dt) ', "DOMANDE_BANDO,COMP_NUCLEO")

            DataGrid1.DataSource = dt
            DataGrid1.DataBind()

            Session.Item("RISULTATI_MANUTENZIONI_D") = dt
            ' Label1.Text = " " & dt.Rows.Count

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub




    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(e.Item.Cells(2).Text, "'", "\'") & " ubicato in: " & Left(Replace(e.Item.Cells(4).Text.Replace("&nbsp;", "-"), "'", "\'"), 30) & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    '        ''            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l'impianto del Cod. Complesso: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(e.Item.Cells(2).Text, "'", "\'") & " ubicato in: " & Left(Replace(e.Item.Cells(4).Text.Replace("&nbsp;", "-"), "'", "\'"), 30) & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    '        ''            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l'impianto del Cod. Complesso: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
    '        ';document.getElementById('txtImpianto').value='" & e.Item.Cells(2).Text & "'"
    '    End If

    'End Sub

    'Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
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

    Protected Sub DataGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGrid1.ItemCommand
        Try
            If e.CommandName = RadGrid.ExportToExcelCommandName Then
                isExporting.Value = "1"
            End If
            If e.CommandName = RadGrid.FilterCommandName Then
                isFilter = True
            Else
                isFilter = False
            End If
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    'Protected Sub imgExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgExport.Click
    '    Dim xls As New ExcelSiSol
    '    Dim nomeFile = xls.EsportaExcelDaDataGridWithDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportRisultatiManutenzioni", "ExportRisultatiManutenzioni", DataGrid1, CType(Session.Item("RISULTATI_MANUTENZIONI_D"), Data.DataTable))
    '    If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
    '        Response.Redirect("../../../FileTemp/" & nomeFile, False)
    '    Else
    '        Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
    '    End If
    'End Sub

    Protected Sub DataGrid1_ItemDataBound1(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(dataItem("ODL_ANNO").Text, "'", "\'") & " ubicato in: " & Left(Replace(dataItem("UBICAZIONE").Text.Replace("&nbsp;", "-"), "'", "\'"), 30) & "';document.getElementById('txtid').value='" & dataItem("ID_MANUTENZIONE").Text & "'")
                e.Item.Attributes.Add("onDblclick", "document.getElementById('btnVisualizza').click();")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Else
            Session.Add("ID", txtid.Text)

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreRepertorio = Request.QueryString("REP")
            sValoreODL = Request.QueryString("ODL")
            sValoreAnno = Request.QueryString("ANNO")



            Response.Write("<script>location.replace('Manutenzioni.aspx?REP=" & par.VaroleDaPassare(sValoreRepertorio) _
                                                                    & "&ODL=" & sValoreODL _
                                                                    & "&ANNO=" & sValoreAnno _
                                                                    & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                    & "&PROVENIENZA=RICERCA_DIRETTA');</script>")

        End If
    End Sub
    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""RicercaManutenzioniD.aspx""</script>")
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub



    Protected Sub Page_PreRenderComplete(sender As Object, e As System.EventArgs) Handles Me.PreRenderComplete
        If isExporting.Value = "1" Then
            Dim context As RadProgressContext = RadProgressContext.Current
            context.CurrentOperationText = "Export in corso..."
            context("ProgressDone") = True
            context.OperationComplete = True
            context.SecondaryTotal = 0
            context.SecondaryValue = 0
            context.SecondaryPercent = 0
            isExporting.Value = "0"
        End If
    End Sub
    Private Function EsportaQueryODL() As String
        Try

            Dim sFiliale As String = "-1"
            If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            End If

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreRepertorio = Strings.Trim(Request.QueryString("REP"))
            sValoreODL = Strings.Trim(Request.QueryString("ODL"))

            sValoreAnno = Strings.Trim(Request.QueryString("ANNO"))

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
                Case "SERVIZIO"
                    sOrder = " order by SERVIZIO,SERVIZIO_VOCI,UBICAZIONE"
                Case "DATA ODL"
                    sOrder = " order by DATA_ODL desc"
                Case "NUM REPERTORIO"
                    sOrder = " order by NUM_REPERTORIO"
                Case "FORNITORE"
                    sOrder = " order by FORNITORE,UBICAZIONE"
                Case "STRUTTURA_ALER"
                    sOrder = " order by STRUTTURA_ALER,UBICAZIONE"
                Case "VOCE_BP"
                    sOrder = " order by VOCE_BP,UBICAZIONE"
                Case Else
                    sOrder = ""
            End Select


            sStringaSql = ""

            sStringaSql = "select " _
                & " SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                & " SISCOM_MI.APPALTI.NUM_REPERTORIO," _
                & "LTRIM(RTRIM((CASE WHEN APPALTI.ANNO IS NOT NULL THEN TO_CHAR(APPALTI.ANNO) ELSE SUBSTR(NUM_REPERTORIO,1,4) END)||LTRIM(RTRIM(TO_CHAR(APPALTI.PROGR,'0000000000'))))) AS REP_ORD, " _
                & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) AS ""ODL_ANNO""," _
                & " TO_DATE(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,'YYYYMMDD')  AS ""DATA_INIZIO_ORDINE""," _
                & " TRIM(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE) AS ""UBICAZIONE""," _
                & " TRIM ( TO_CHAR ( NVL ( (SELECT sum(MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO) FROM SISCOM_MI.manutenzioni_interventi WHERE manutenzioni_interventi.id_manutenzione = manutenzioni.id), 0), '9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO"" ," _
                & " TRIM(TO_CHAR(nvl((select IMPORTO_PRENOTATO from SISCOM_MI.prenotazioni where prenotazioni.id=id_prenotazione_pagamento),0),'9G999G999G999G999G990D99')) AS ""IMPORTO_PRE"", " _
                & " TRIM(TO_CHAR(nvl((select IMPORTO_APPROVATO from SISCOM_MI.prenotazioni where prenotazioni.id=id_prenotazione_pagamento),0),'9G999G999G999G999G990D99')) AS ""IMPORTO_CON"", " _
                & " TRIM(SISCOM_MI.TAB_SERVIZI.DESCRIZIONE) AS ""SERVIZIO""," _
                & " TRIM(SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE) AS ""SERVIZIO_VOCI""," _
                & " (CASE WHEN FORNITORI.RAGIONE_SOCIALE IS NOT NULL THEN COD_FORNITORE || ' - ' || TRIM(FORNITORI.RAGIONE_SOCIALE) ELSE COD_FORNITORE || ' - ' || TRIM(FORNITORI.COGNOME)|| ' ' ||TRIM(FORNITORI.NOME) END) AS ""FORNITORE""," _
                & " TRIM(SISCOM_MI.PF_VOCI.CODICE) AS ""CODICE_BP""," _
                & " TRIM(SISCOM_MI.PF_VOCI.DESCRIZIONE) AS ""VOCE_BP""," _
                & " TRIM(SISCOM_MI.TAB_FILIALI.NOME) AS ""STRUTTURA_ALER""," _
                & " SISCOM_MI.TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                & " SISCOM_MI.MANUTENZIONI.PROGR," _
                & " SISCOM_MI.MANUTENZIONI.ANNO," _
                & " SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE AS  ""DATA_ODL""," _
                & " SISCOM_MI.MANUTENZIONI.DESCRIZIONE as ""DESCRIZIONE"" " _
                & " ,(CASE WHEN SISCOM_MI.MANUTENZIONI.FL_AUTORIZZAZIONE=1 THEN 'AUTORIZZATO' else 'NON AUTORIZZATO' END) AS ""AUTORIZZAZIONE"", MANUTENZIONI.ID_SEGNALAZIONI AS NUM_SEGN,TO_DATE(SISCOM_MI.MANUTENZIONI.DATA_ANNULLO,'YYYYMMDD')  AS ""DATA_ANNULLO""  " _
                & " FROM SISCOM_MI.MANUTENZIONI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI,SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.FORNITORI,SISCOM_MI.PF_VOCI,SISCOM_MI.TAB_FILIALI" _
                & " WHERE SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+)  " _
                & " AND SISCOM_MI.MANUTENZIONI.ID_EDIFICIO IS NULL  " _
                & " AND SISCOM_MI.MANUTENZIONI.ID_PF_VOCE IS NULL " _
                & " AND SISCOM_MI.MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
                & " AND SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+)  " _
                & " AND SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)   " _
                & " AND SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                & " AND SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                & " AND SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                & " AND SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=SISCOM_MI.TAB_FILIALI.ID (+) " _
                & " AND SISCOM_MI.MANUTENZIONI.STATO<6 " _
    & condizioneAutorizzazione

            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
            End If


            If sFiliale <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI." & sFiliale
            End If


            If par.IfEmpty(sValoreRepertorio, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.APPALTI.NUM_REPERTORIO LIKE '" & par.PulisciStrSql(sValoreRepertorio).Replace("*", "%") & "'"
            End If

            If par.IfEmpty(sValoreODL, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.PROGR=" & sValoreODL
            End If

            If par.IfEmpty(sValoreAnno, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ANNO=" & sValoreAnno
            End If

            If sStringaSql <> "" Then sStringaSql = sStringaSql & " UNION "
            sStringaSql = sStringaSql & "  select " _
                & " SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                & " SISCOM_MI.APPALTI.NUM_REPERTORIO," _
                & "LTRIM(RTRIM((CASE WHEN APPALTI.ANNO IS NOT NULL THEN TO_CHAR(APPALTI.ANNO) ELSE SUBSTR(NUM_REPERTORIO,1,4) END)||LTRIM(RTRIM(TO_CHAR(APPALTI.PROGR,'0000000000'))))) AS REP_ORD, " _
                & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) AS ""ODL_ANNO"",TO_DATE(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,'YYYYMMDD')  AS ""DATA_INIZIO_ORDINE""," _
                & " TRIM(SISCOM_MI.EDIFICI.DENOMINAZIONE) AS ""UBICAZIONE""," _
                & " TRIM ( TO_CHAR ( NVL ( (SELECT sum(MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO) FROM SISCOM_MI.manutenzioni_interventi WHERE manutenzioni_interventi.id_manutenzione = manutenzioni.id), 0), '9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO"" ," _
                & " TRIM(TO_CHAR(nvl((select IMPORTO_PRENOTATO from SISCOM_MI.prenotazioni where prenotazioni.id=id_prenotazione_pagamento),0),'9G999G999G999G999G990D99')) AS ""IMPORTO_PRE"", " _
                & " TRIM(TO_CHAR(nvl((select IMPORTO_APPROVATO from SISCOM_MI.prenotazioni where prenotazioni.id=id_prenotazione_pagamento),0),'9G999G999G999G999G990D99')) AS ""IMPORTO_CON"", " _
                & " TRIM(SISCOM_MI.TAB_SERVIZI.DESCRIZIONE) AS ""SERVIZIO""," _
                & " TRIM(SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE) AS ""SERVIZIO_VOCI""," _
                & " (CASE WHEN FORNITORI.RAGIONE_SOCIALE IS NOT NULL THEN COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE ELSE COD_FORNITORE || ' - ' || FORNITORI.COGNOME|| ' ' ||FORNITORI.NOME END) AS ""FORNITORE""," _
                & " TRIM(SISCOM_MI.PF_VOCI.CODICE) AS ""CODICE_BP""," _
                & " TRIM(SISCOM_MI.PF_VOCI.DESCRIZIONE) AS ""VOCE_BP""," _
                & " TRIM(SISCOM_MI.TAB_FILIALI.NOME) AS ""STRUTTURA_ALER""," _
                & " SISCOM_MI.TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                & " SISCOM_MI.MANUTENZIONI.PROGR," _
                & " SISCOM_MI.MANUTENZIONI.ANNO," _
                & " SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE as ""DATA_ODL""," _
                & " SISCOM_MI.MANUTENZIONI.DESCRIZIONE as ""DESCRIZIONE"" " _
                & " ,(CASE WHEN SISCOM_MI.MANUTENZIONI.FL_AUTORIZZAZIONE=1 THEN 'AUTORIZZATO' else 'NON AUTORIZZATO' END) AS ""AUTORIZZAZIONE"", MANUTENZIONI.ID_SEGNALAZIONI AS NUM_SEGN, TO_DATE(SISCOM_MI.MANUTENZIONI.DATA_ANNULLO,'YYYYMMDD')  AS ""DATA_ANNULLO"" " _
                & " FROM SISCOM_MI.MANUTENZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI,SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.FORNITORI,SISCOM_MI.PF_VOCI,SISCOM_MI.TAB_FILIALI " _
                & " WHERE SISCOM_MI.MANUTENZIONI.ID_COMPLESSO IS NULL  " _
                & " AND SISCOM_MI.MANUTENZIONI.ID_PF_VOCE IS NULL " _
                & " AND SISCOM_MI.MANUTENZIONI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                & " AND SISCOM_MI.MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
                & " AND SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+)  " _
                & " AND SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)   " _
                & " AND SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                & " AND SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                & " AND SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                & " AND SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=SISCOM_MI.TAB_FILIALI.ID (+) " _
                & " AND SISCOM_MI.MANUTENZIONI.STATO<6 " _
    & condizioneAutorizzazione
            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
            End If
            If sFiliale <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI." & sFiliale
            End If
            If par.IfEmpty(sValoreRepertorio, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.APPALTI.NUM_REPERTORIO LIKE '" & par.PulisciStrSql(sValoreRepertorio).Replace("*", "%") & "'"
            End If
            If par.IfEmpty(sValoreODL, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.PROGR=" & sValoreODL
            End If
            If par.IfEmpty(sValoreAnno, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ANNO=" & sValoreAnno
            End If
            sStringaSQL1 = sStringaSql & sOrder
            'sWhere = Session.Item("IMP2")
            EsportaQueryODL = sStringaSql
            'par.OracleConn.Open()
            'Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSql, par.OracleConn)
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
            'Label3.Text = "0"
            'Do While myReader.Read()
            '    Label3.Text = CInt(Label3.Text) + 1
            'Loop
            'Label3.Text = Label3.Text
            'cmd.Dispose()
            'myReader.Close()
            'par.OracleConn.Close()




        Catch ex As Exception

            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Function
    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            ' connData.apri(False)
            Dim Query As String = EsportaQueryODL()
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            DataGrid1.DataSource = dt
            'connData.chiudi(False)
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

    Private Sub btnEsporta_Click(sender As Object, e As EventArgs) Handles btnEsporta.Click
        Try
            ' connData.apri(False)
            Dim Query As String = EsportaQueryODL()
            Dim dt As New Data.DataTable
            dt = par.getDataTableFilterSortRadGrid(Query, DataGrid1)
            'dt = par.getDataTableGrid(Query)
            'connData.chiudi(False)
            If dt.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportODL", "ExportODL", dt)
                If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager).RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 450, 150, "Attenzione", Nothing, Nothing)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "key", "function f(){NotificaTelerik('" & CType(Me.Master.FindControl("RadNotificationNote"), RadNotification).ClientID & "', 'Attenzione', '" & par.Messaggio_NoExport & "'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dim", "setDimensioni();", True)
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnExport_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

End Class
