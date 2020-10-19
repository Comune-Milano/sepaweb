Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_EstrazionePagamenti
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        'Response.Flush()
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            caricaCodFattura()
            caricaEserciziFinanziari()
            'TextBoxDataFatturaDa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'TextBoxDataFatturaA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'TextBoxDataPagamentoDa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'TextBoxDataPagamentoA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'TextBoxDataCdPDa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'TextBoxDataCdPA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub
    'Private Sub Ricerca()
    '    Try
    '        connData.apri()
    '        Dim condizioniRicerca As String = ""
    '        Dim condizioniRicercaFattureMM As String = ""
    '        If Trim(TextBoxCodoc.Text) <> "" Then
    '            If condizioniRicercaFattureMM = "" Then
    '                condizioniRicercaFattureMM &= " where codoc='" & par.PulisciStrSql(TextBoxCodoc.Text.ToString) & "'"
    '            Else
    '                condizioniRicercaFattureMM &= " and codoc='" & par.PulisciStrSql(TextBoxCodoc.Text.ToString) & "'"
    '            End If
    '        End If
    '        If Len(TextBoxDataFatturaDa.Text) = 10 AndAlso IsDate(TextBoxDataFatturaDa.Text) Then
    '            If condizioniRicercaFattureMM = "" Then
    '                condizioniRicercaFattureMM &= " where fatture_mm.data_Fattura>='" & par.AggiustaData(TextBoxDataFatturaDa.Text.ToString) & "'"
    '            Else
    '                condizioniRicercaFattureMM &= " and fatture_mm.data_Fattura>='" & par.AggiustaData(TextBoxDataFatturaDa.Text.ToString) & "'"
    '            End If
    '        End If
    '        If Len(TextBoxDataFatturaA.Text) = 10 AndAlso IsDate(TextBoxDataFatturaA.Text) Then
    '            If condizioniRicercaFattureMM = "" Then
    '                condizioniRicercaFattureMM &= " where fatture_mm.data_Fattura<='" & par.AggiustaData(TextBoxDataFatturaA.Text.ToString) & "'"
    '            Else
    '                condizioniRicercaFattureMM &= " and fatture_mm.data_Fattura<='" & par.AggiustaData(TextBoxDataFatturaA.Text.ToString) & "'"
    '            End If
    '        End If
    '        If Len(TextBoxDataPagamentoDa.Text) = 10 AndAlso IsDate(TextBoxDataPagamentoDa.Text) Then
    '            If condizioniRicerca = "" Then
    '                condizioniRicerca &= " and pagamenti.data_emissione>='" & par.AggiustaData(TextBoxDataPagamentoDa.Text.ToString) & "'"
    '            End If
    '        End If
    '        If Len(TextBoxDataPagamentoA.Text) = 10 AndAlso IsDate(TextBoxDataPagamentoA.Text) Then
    '            If condizioniRicerca = "" Then
    '                condizioniRicerca &= " and pagamenti.data_emissione<='" & par.AggiustaData(TextBoxDataPagamentoA.Text.ToString) & "'"
    '            End If
    '        End If
    '        par.cmd.CommandText = "SELECT PAGAMENTI.ID AS ID_PAGAMENTO," _
    '            & "(SELECT COD_FORNITORE FROM SISCOM_MI.FORNITORI WHERE ID=PAGAMENTI.ID_FORNITORE) AS COD_FORNITORE," _
    '            & "(SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID=PAGAMENTI.ID_FORNITORE) AS RAGIONE_SOCIALE," _
    '            & "(SELECT COD_FISCALE FROM SISCOM_MI.FORNITORI WHERE ID=PAGAMENTI.ID_FORNITORE) AS COD_FISCALE," _
    '            & "PAGAMENTI.PROGR||'/'||PAGAMENTI.ANNO AS NUMERO_CDP," _
    '            & "TRIM(TO_CHAR((SELECT SUM( ROUND( IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO=PAGAMENTI.ID AND ID_STATO=2),'999G999G990D99')) AS TOT, " _
    '            & "TRIM(TO_CHAR((SELECT SUM( ROUND((IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0))*100/(NVL(PERC_IVA,0) + 100),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO=PAGAMENTI.ID AND ID_STATO=2),'999G999G990D99')) AS IMPONIBILE, " _
    '            & "TRIM(TO_CHAR((SELECT SUM( ROUND((IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0))-((IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0))*100/(NVL(PERC_IVA,0) + 100)),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO=PAGAMENTI.ID AND ID_STATO=2),'999G999G990D99')) AS IVA, " _
    '            & "'' AS FATTURE,'' AS VOCE FROM SISCOM_MI.PAGAMENTI WHERE ID IN (SELECT ID_PAGAMENTO FROM SISCOM_MI.FATTURE_MM " & condizioniRicercaFattureMM & ")" & condizioniRicerca
    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim dt As New Data.DataTable
    '        da.Fill(dt)
    '        da.Dispose()
    '        If dt.Rows.Count > 0 Then
    '            DataGridPagamenti.DataSource = dt
    '            DataGridPagamenti.DataBind()
    '            Dim idPagamento As Integer = 0
    '            Dim stringaTab As String = ""
    '            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
    '            For Each elemento As DataGridItem In DataGridPagamenti.Items
    '                stringaTab = ""
    '                idPagamento = par.IfNull(elemento.Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text, 0)
    '                par.cmd.CommandText = "SELECT ANNO||'/'||NUMERO AS NUMERO_RDS," _
    '                    & " NUMERO_FATT_FORN," _
    '                    & " GETDATA(DATA_FATTURA) AS DATA_FATTURA," _
    '                    & " COD_OC,DESCRIZIONE_OC," _
    '                    & " TRIM(TO_CHAR(IMPORTO_TOTALE,'999G999G990D99')) AS IMPORTO_TOTALE " _
    '                    & " FROM SISCOM_MI.FATTURE_MM WHERE ID_PAGAMENTO=" & idPagamento
    '                lettore = par.cmd.ExecuteReader
    '                stringaTab = "<table cellpadding=""1"" cellspacing=""1""><tr>" _
    '                    & "<td style=""border: 1px solid #507CD1;width:90px;background-color:#507cd1;color:white;"">Numero RDS</td>" _
    '                    & "<td style=""border: 1px solid #507CD1;width:90px;background-color:#507cd1;color:white;"">Num.Fatt.Fornitore</td>" _
    '                    & "<td style=""border: 1px solid #507CD1;width:90px;background-color:#507cd1;color:white;"">Data Fatt.</td>" _
    '                    & "<td style=""border: 1px solid #507CD1;width:90px;background-color:#507cd1;color:white;"">Cod.Op.Cont.</td>" _
    '                    & "<td style=""border: 1px solid #507CD1;width:90px;background-color:#507cd1;color:white;"">Descr.Op.Cont.</td>" _
    '                    & "<td style=""border: 1px solid #507CD1;width:90px;background-color:#507cd1;color:white;"">Importo</td></tr>"
    '                Dim cont As Integer = 0
    '                While lettore.Read
    '                    stringaTab &= "<tr>" _
    '                        & " <td>" & par.IfNull(lettore("NUMERO_RDS"), "") & "</td>" _
    '                        & " <td>" & par.IfNull(lettore("NUMERO_FATT_FORN"), "") & "</td>" _
    '                        & " <td>" & par.IfNull(lettore("DATA_FATTURA"), "") & "</td>" _
    '                        & " <td>" & par.IfNull(lettore("COD_OC"), "") & "</td>" _
    '                        & " <td>" & par.IfNull(lettore("DESCRIZIONE_OC"), "") & "</td>" _
    '                        & " <td>" & par.IfNull(lettore("IMPORTO_TOTALE"), "") & "</td>" _
    '                        & "</tr>"
    '                    cont += 1
    '                End While
    '                lettore.Close()
    '                stringaTab &= "</table>"
    '                'par.cmd.CommandText = "SELECT " _
    '                '    & "(SELECT SUM( ROUND( importo_approvato - NVL(rit_legge_ivata,0),2)) * 100 FROM SISCOM_MI.PRENOTAZIONI WHERE id_pagamento=" & idPagamento & " AND ID_STATO=2) AS tot, " _
    '                '    & "(SELECT SUM( ROUND((importo_approvato - NVL(rit_legge_ivata,0)) * 100 /(NVL(PERC_IVA,0) + 100),2)) * 100 FROM SISCOM_MI.PRENOTAZIONI WHERE id_pagamento=PAGAMENTI.ID) AS imponibile, " _
    '                '    & "(SELECT SUM( ROUND((importo_approvato - NVL(rit_legge_ivata,0)) - ((importo_approvato - NVL(rit_legge_ivata,0)) * 100 /(NVL(PERC_IVA,0) + 100)),2)) * 100 FROM SISCOM_MI.PRENOTAZIONI WHERE id_pagamento=PAGAMENTI.ID) AS IVA, " _
    '                '    & " FROM PRENOTAZIONI "
    '                If cont > 0 Then
    '                    elemento.Cells(par.IndDGC(DataGridPagamenti, "FATTURE")).Text = stringaTab
    '                Else
    '                    elemento.Cells(par.IndDGC(DataGridPagamenti, "FATTURE")).Text = ""
    '                End If
    '                stringaTab = ""
    '                par.cmd.CommandText = " SELECT  " _
    '                    & " PF_VOCI.DESCRIZIONE AS VOCE,PF_CAPITOLI.DESCRIZIONE AS CAPITOLO, " _
    '                    & " TRIM(TO_CHAR((SUM( ROUND((IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0))*100/(NVL(PERC_IVA,0) + 100),2))),'999G999G990D99')) AS IMPONIBILE,  " _
    '                    & " TRIM(TO_CHAR((SUM( ROUND((IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0))-((IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0))*100/(NVL(PERC_IVA,0) + 100)),2))),'999G999G990D99')) AS IVA, " _
    '                    & " TRIM(TO_CHAR((SUM( ROUND( IMPORTO_APPROVATO - NVL(RIT_LEGGE_IVATA,0),2))),'999G999G990D99')) AS TOTALE  " _
    '                    & " FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_CAPITOLI " _
    '                    & " WHERE PAGAMENTI.ID=" & idPagamento _
    '                    & " AND PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID " _
    '                    & " AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF " _
    '                    & " AND PF_CAPITOLI.ID=PF_VOCI.ID_CAPITOLO " _
    '                    & " AND PRENOTAZIONI.ID_STATO=2 " _
    '                    & " GROUP BY PF_VOCI.DESCRIZIONE,PF_CAPITOLI.DESCRIZIONE,PAGAMENTI.ID "
    '                lettore = par.cmd.ExecuteReader
    '                stringaTab = "<table cellpadding=""1"" cellspacing=""1""><tr>" _
    '                    & "<td style=""border: 1px solid #507CD1;width:90px;background-color:#507cd1;color:white;"">Voce</td>" _
    '                    & "<td style=""border: 1px solid #507CD1;width:90px;background-color:#507cd1;color:white;"">Capitolo</td>" _
    '                    & "<td style=""border: 1px solid #507CD1;width:90px;background-color:#507cd1;color:white;"">Imponibile</td>" _
    '                    & "<td style=""border: 1px solid #507CD1;width:90px;background-color:#507cd1;color:white;"">Iva</td>" _
    '                    & "<td style=""border: 1px solid #507CD1;width:90px;background-color:#507cd1;color:white;"">Totale</td></tr>"
    '                cont = 0
    '                While lettore.Read
    '                    stringaTab &= "<tr>" _
    '                        & " <td>" & par.IfNull(lettore("VOCE"), "") & "</td>" _
    '                        & " <td>" & par.IfNull(lettore("CAPITOLO"), "") & "</td>" _
    '                        & " <td>" & par.IfNull(lettore("IMPONIBILE"), "") & "</td>" _
    '                        & " <td>" & par.IfNull(lettore("IVA"), "") & "</td>" _
    '                        & " <td>" & par.IfNull(lettore("TOTALE"), "") & "</td>" _
    '                        & "</tr>"
    '                    cont += 1
    '                End While
    '                lettore.Close()
    '                stringaTab &= "</table>"
    '                If cont > 0 Then
    '                    elemento.Cells(par.IndDGC(DataGridPagamenti, "VOCE")).Text = stringaTab
    '                Else
    '                    elemento.Cells(par.IndDGC(DataGridPagamenti, "VOCE")).Text = ""
    '                End If
    '            Next
    '            DataGridPagamenti.Visible = True
    '            MultiView1.ActiveViewIndex = 1
    '        Else
    '            DataGridPagamenti.Visible = False
    '            MultiView1.ActiveViewIndex = 0
    '        End If
    '        connData.chiudi()
    '    Catch ex As Exception
    '        connData.chiudi()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - Ricerca - " & ex.Message)
    '        Response.Redirect("../../../Errore.aspx", False)
    '    End Try
    'End Sub
    Protected Sub btnVisReport_Click(sender As Object, e As System.EventArgs) Handles btnVisReport.Click
        Dim listaCodici As String = ""
        For Each item As ListItem In CheckBoxList1.Items
            If item.Selected = True Then
                If listaCodici = "" Then
                    listaCodici = item.Value
                Else
                    listaCodici &= "," & item.Value
                End If
            End If
        Next

        Dim DataFatturaDa As String = ""
        If Not IsNothing(TextBoxDataFatturaDa.SelectedDate) Then
            DataFatturaDa = TextBoxDataFatturaDa.SelectedDate
        End If
        Dim DataFatturaA As String = ""
        If Not IsNothing(TextBoxDataFatturaA.SelectedDate) Then
            DataFatturaA = TextBoxDataFatturaA.SelectedDate
        End If
        Dim DataPagamentoDa As String = ""
        If Not IsNothing(TextBoxDataPagamentoDa.SelectedDate) Then
            DataPagamentoDa = TextBoxDataPagamentoDa.SelectedDate
        End If
        Dim DataPagamentoA As String = ""
        If Not IsNothing(TextBoxDataPagamentoA.SelectedDate) Then
            DataPagamentoA = TextBoxDataPagamentoA.SelectedDate
        End If
        Dim DataCdPDa As String = ""
        If Not IsNothing(TextBoxDataCdPDa.SelectedDate) Then
            DataCdPDa = TextBoxDataCdPDa.SelectedDate
        End If
        Dim DataCdPA As String = ""
        If Not IsNothing(TextBoxDataCdPA.SelectedDate) Then
            DataCdPA = TextBoxDataCdPA.SelectedDate
        End If

        Dim BoxDataRegistrazioneFatturaDa As String = ""
        If Not IsNothing(TextBoxDataRegistrazioneFatturaDa.SelectedDate) Then
            BoxDataRegistrazioneFatturaDa = TextBoxDataRegistrazioneFatturaDa.SelectedDate
        End If
        Dim DataRegistrazioneFatturaA As String = ""
        If Not IsNothing(TextBoxDataRegistrazioneFatturaA.SelectedDate) Then
            DataRegistrazioneFatturaA = TextBoxDataRegistrazioneFatturaA.SelectedDate
        End If
        Dim script As String = "window.open('RisultatiEstrazioniPagamentiNew.aspx?CODOC=" & listaCodici _
                         & "&DFATTDA=" & DataFatturaDa & "&DFATTA=" & DataFatturaA _
                          & "&DREGFATTDA=" & BoxDataRegistrazioneFatturaDa & "&DREGFATTA=" & DataRegistrazioneFatturaA _
                         & "&DPAGDA=" & DataPagamentoDa & "&DPAGA=" & DataPagamentoA _
                         & "&DCDPDA=" & DataCdPDa & "&DCDPA=" & DataCdPA _
                         & "&SPF=" & CheckBoxPagamentiFatturati.Checked.ToString _
                         & "&IDPF=" & DropDownListEsercizioFinanziario.SelectedValue.ToString _
                         & "'," _
                         & "'ExportPagamenti','');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", script, True)
    End Sub
    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "red", "location.href='../../pagina_home_ncp.aspx';", True)
        'Response.Redirect("../../pagina_home.aspx")
    End Sub

    Private Sub caricaCodFattura()
        Try
            connData.apri()
            par.caricaCheckBoxList("SELECT distinct COD_OC AS COD,COD_OC||' - '||DESCRIZIONE_OC AS DESCRIZIONE FROM SISCOM_MI.FATTURE_MM", CheckBoxList1, "COD", "DESCRIZIONE")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - Ricerca - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "red", "location.href='../../../Errore.aspx';", True)
            'Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaEserciziFinanziari()
        connData.apri()
        par.caricaComboTelerik("SELECT PF_MAIN.ID,SISCOM_MI.GETDATA(INIZIO)||'-'||SISCOM_MI.GETDATA(FINE) AS DESCRIZIONE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID ORDER BY PF_MAIN.ID DESC", DropDownListEsercizioFinanziario, "ID", "DESCRIZIONE")
        par.caricaCheckBoxList("SELECT PF_MAIN.ID,SISCOM_MI.GETDATA(INIZIO)||'-'||SISCOM_MI.GETDATA(FINE) AS DESCRIZIONE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID ORDER BY PF_MAIN.ID DESC", chkBoxList, "ID", "DESCRIZIONE")
        connData.chiudi()
    End Sub
    Protected Sub ButtonScarica_Click(sender As Object, e As System.EventArgs) Handles ButtonScarica.Click
        Dim listaSelezionati As String = ""
        For Each Items As ListItem In chkBoxList.Items
            If Items.Selected = True Then
                If listaSelezionati <> "" Then
                    listaSelezionati &= "," & Items.Value
                Else
                    listaSelezionati = Items.Value
                End If

            End If
        Next
        If listaSelezionati = "" Then
            RadWindowManager1.RadAlert("Nessun elemento selezionato!", 300, 150, "Attenzione", "", "null")
            Exit Sub
        Else


            Dim strmZipOutputStream As ZipOutputStream = Nothing
            'Dim contatore As Integer = 0
            Try
                connData.apri()
                Dim nome As String = "ReportPagamenti-" & Format(Now, "yyyyMMddHHmmss")
                Dim objCrc32 As New Crc32()
                Dim zipfic As String
                zipfic = Server.MapPath("..\..\..\FileTemp\" & nome & ".zip")
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                Dim strFile As String
                Dim strmFile As FileStream
                Dim theEntry As ZipEntry
                par.cmd.CommandText = "SELECT SUBSTR(INIZIO,1,4) FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO=SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID and pf_main.id in(" & listaSelezionati & ")"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While lettore.Read
                    'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.REPORT_PAGAMENTI_" & par.IfNull(lettore(0), 0) & " ORDER BY 1 ASC NULLS FIRST,2,3,4,5,6"
                    par.cmd.CommandText = " SELECT ID,ID_PAGAMENTO,COD_FORNITORE,RAGIONE_SOCIALE,COD_FISCALE,NUMERO_CDP,DATA_CDP,IMPONIBILE,IVA," _
                        & " TOT,ID_VOCE_PF,ANNO_PF,VOCE_PF,CAPITOLO,IMPONIBILE_D,IVA_D,TOTALE_D,ID_FATTURA_MM,NUMERO_RDS," _
                        & " N_FATT_FORN,DATA_FATT,DATA_REGISTRAZIONE,COD_OP_CONT,IMPORTO_TOTALE,ID_PAGAMENTO_MM," _
                        & " NUMERO_PAG,DATA_PAG,IMPORTO_PAGATO,COD_OP_CONTAB,CUP,CIG " _
                        & " FROM SISCOM_MI.REPORT_PAGAMENTI_" & par.IfNull(lettore(0), 0) & " UNION ALL " _
                        & " SELECT ID,ID_PAGAMENTO,COD_FORNITORE,RAGIONE_SOCIALE,COD_FISCALE,NUMERO_CDP,DATA_CDP,IMPONIBILE,IVA," _
                        & " TOT,ID_VOCE_PF,ANNO_PF,VOCE_PF,CAPITOLO,IMPONIBILE_D,IVA_D,TOTALE_D,ID_FATTURA_MM,NUMERO_RDS," _
                        & " N_FATT_FORN,DATA_FATT,DATA_REGISTRAZIONE,COD_OP_CONT,IMPORTO_TOTALE,ID_PAGAMENTO_MM," _
                        & " NUMERO_PAG,DATA_PAG,IMPORTO_PAGATO,COD_OP_CONTAB,CUP,CIG " _
                        & " FROM SISCOM_MI.REPORT_PAGAMENTI_" & par.IfNull(lettore(0), 0) & "_RIT WHERE ID IS NOT NULL ORDER BY 2 ASC NULLS FIRST,1,3,4,5,6,12,23"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtDef As New Data.DataTable
                    da.Fill(dtDef)
                    da.Dispose()
                    Dim dtDef2 As Data.DataTable = dtDef.Clone
                    Dim dtDef3 As Data.DataTable = dtDef.Clone
                    AggiustaDataTable(dtDef, dtDef2, dtdef3)
                    'DataGridPagamenti.DataSource = dtDef
                    'DataGridPagamenti.DataBind()
                    'AggiustaDataGrid()
                    Dim xls As New ExcelSiSol
                    Dim nomeFile As String = xls.EsportaExcelDaDataGridParzialeConRowspan(ExcelSiSol.Estensione.Office2007_xlsx, "ExportPagamenti_" & par.IfNull(lettore(0), 0) & "_", "ExportPagamenti", DataGridPagamenti, , , , 1, , True, dtDef, dtDef2, dtDef3)
                    If File.Exists(Server.MapPath("..\..\..\FileTemp\") & nomeFile) Then
                        'contatore += 1
                        strFile = Server.MapPath("..\..\..\FileTemp\" & nomeFile)
                        strmFile = File.OpenRead(strFile)
                        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                        strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                        Dim sFile As String = Path.GetFileName(strFile)
                        theEntry = New ZipEntry(sFile)
                        Dim fi As New FileInfo(strFile)
                        theEntry.DateTime = fi.LastWriteTime
                        theEntry.Size = strmFile.Length
                        strmFile.Close()
                        objCrc32.Reset()
                        objCrc32.Update(abyBuffer)
                        theEntry.Crc = objCrc32.Value
                        strmZipOutputStream.PutNextEntry(theEntry)
                        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                        'ReDim Preserve ElencoFile(i)
                        'ElencoFile(i) = "..\..\..\FileTemp\" & nomeFile & ".pdf"
                        'i = i + 1
                    End If

                End While
                lettore.Close()
                connData.chiudi()
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
                If File.Exists(Server.MapPath("..\..\..\FileTemp\") & nome & ".zip") Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nome & ".zip','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
                End If
            Catch ex As Exception
                If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
                If strmZipOutputStream.IsFinished = False Then
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()
                End If
                RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
            End Try
        End If
    End Sub

    Protected Sub DataGridPagamenti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridPagamenti.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Text = "CERTIFICATO DI PAGAMENTO"
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Attributes.Add("colspan", 8)
            'e.Item.Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Visible = False
            'e.Item.Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IVA")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "TOT")).Visible = False
            'e.Item.Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Text = "PIANO FINANZIARIO"
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Attributes.Add("colspan", 6)
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Text = "FATTURE"
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Attributes.Add("colspan", 6)
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "DATA_FATT")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "DATA_REGISTRAZIONE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text = "PAGAMENTI"
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Attributes.Add("colspan", 6)
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "CUP")).Visible = False
            e.Item.Cells(par.IndDGC(DataGridPagamenti, "CIG")).Visible = False
        End If
    End Sub

    Protected Sub DataGridPagamenti_PreRender(sender As Object, e As System.EventArgs) Handles DataGridPagamenti.PreRender
        AggiustaDataGrid()
    End Sub

    Protected Sub AggiustaDataGrid()
        Dim codFornitorePrecedente As String = ""
        Dim ragioneSocialePrecedente As String = ""
        Dim codFiscalePrecedente As String = ""
        Dim numeroCDPPrecedente As String = ""
        Dim annoCDPPrecedente As String = ""
        Dim dataCDPPrecedente As String = ""
        Dim imponibilePrecedente As String = ""
        Dim ivaPrecedente As String = ""
        Dim totPrecedente As String = ""
        Dim idPagamentoPrecedente As String = ""
        Dim idVocePrecedente As String = ""
        Dim vocePrecedente As String = ""
        Dim annoPrecedente As String = ""
        Dim capitoloPrecedente As String = ""
        Dim imponibileDPrecedente As String = ""
        Dim ivaDPrecedente As String = ""
        Dim totaleDPrecedente As String = ""
        Dim numeroPrecedente As String = ""
        Dim dataPrecedente As String = ""
        Dim importoPagatoPrecedente As String = ""
        Dim codOperazioneContabilePrecedente As String = ""
        Dim numeroRDSPrecedente As String = ""
        Dim idFatturammPrecedente As String = ""
        Dim nFattFornPrecedente As String = ""
        Dim dataFattPrecedente As String = ""
        Dim dataRegistrazionePrecedente As String = ""
        Dim codOpContPrecedente As String = ""
        Dim importoTotalePrecedente As String = ""
        Dim cupPrecedente As String = ""
        Dim cigPrecedente As String = ""
        Dim idPagamentoMMPrecedente As String = ""

        For i As Integer = DataGridPagamenti.Items.Count - 1 To 0 Step -1

            If i = DataGridPagamenti.Items.Count - 1 Then
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Attributes.Add("rowspan", 1)
                idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Attributes.Add("rowspan", 1)
                codFornitorePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Attributes.Add("rowspan", 1)
                ragioneSocialePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Attributes.Add("rowspan", 1)
                codFiscalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Attributes.Add("rowspan", 1)
                numeroCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Text
                'DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).Attributes.Add("rowspan", 1)
                'ANNOCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).Attributes.Add("rowspan", 1)
                dataCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Attributes.Add("rowspan", 1)
                imponibilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Attributes.Add("rowspan", 1)
                ivaPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Attributes.Add("rowspan", 1)
                totPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Attributes.Add("rowspan", 1)
                idVocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Attributes.Add("rowspan", 1)
                annoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Attributes.Add("rowspan", 1)
                vocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Attributes.Add("rowspan", 1)
                capitoloPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Attributes.Add("rowspan", 1)
                imponibileDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Attributes.Add("rowspan", 1)
                ivaDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Attributes.Add("rowspan", 1)
                totaleDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Attributes.Add("rowspan", 1)
                numeroPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Attributes.Add("rowspan", 1)
                dataPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Attributes.Add("rowspan", 1)
                importoPagatoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Attributes.Add("rowspan", 1)
                codOperazioneContabilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Attributes.Add("rowspan", 1)
                numeroRDSPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).Attributes.Add("rowspan", 1)
                numeroRDSPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Attributes.Add("rowspan", 1)
                nFattFornPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Attributes.Add("rowspan", 1)
                dataFattPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_REGISTRAZIONE")).Attributes.Add("rowspan", 1)
                dataRegistrazionePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_REGISTRAZIONE")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Attributes.Add("rowspan", 1)
                codOpContPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Attributes.Add("rowspan", 1)
                importoTotalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Attributes.Add("rowspan", 1)
                importoTotalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Text
                DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Attributes.Add("rowspan", 1)
                importoTotalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Text
            Else
                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text _
                    And codFornitorePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Text _
                    And ragioneSocialePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Text _
                    And codFiscalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Text _
                    And numeroCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Text _
                    And dataCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).Text _
                    And imponibilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text _
                    And ivaPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text _
                    And totPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text Then
                    'And annoCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).Text 

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan = 1

                    'DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).Visible = False
                    'DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).RowSpan) + 1
                    'DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan = 1

                End If


                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text _
                    And idVocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text _
                    And annoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Text _
                    And vocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Text _
                    And capitoloPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Text _
                    And imponibileDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Text _
                    And ivaDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Text _
                    And totaleDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Text Then

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).RowSpan = 1

                    'DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Visible = False
                    'DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).RowSpan) + 1
                    'DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).RowSpan = 1

                    'DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Visible = False
                    'DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).RowSpan) + 1
                    'DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).RowSpan = 1

                End If


                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text _
                    And idFatturammPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_FATTURA_MM")).Text _
                    And numeroRDSPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Text _
                    And nFattFornPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Text _
                    And dataFattPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Text _
                    And dataRegistrazionePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_REGISTRAZIONE")).Text _
                    And codOpContPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Text _
                    And importoTotalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Text Then

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_FATTURA_MM")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_FATTURA_MM")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_FATTURA_MM")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_FATTURA_MM")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_rEGISTRAZIONE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_rEGISTRAZIONE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_rEGISTRAZIONE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_rEGISTRAZIONE")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).RowSpan = 1

                End If


                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text _
                    And idPagamentoMMPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).Text _
                    And idVocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text _
                    And annoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Text _
                    And numeroPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text _
                    And dataPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Text _
                    And importoPagatoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text _
                    And codOperazioneContabilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Text _
                    And cupPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Text _
                    And cigPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Text Then


                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan = 1

                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Visible = False
                    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan) + 1
                    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan = 1

                End If



                'If numeroCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Text Then
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).RowSpan = 1

                '    If idVocePrecedente <> DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text Then
                '        DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text = Format(CDec(DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text) + CDec(DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text), "#,##0.00")
                '        DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text = Format(CDec(DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text) + CDec(DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text), "#,##0.00")
                '        DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text = Format(CDec(DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text) + CDec(DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text), "#,##0.00")
                '    End If

                'End If

                'If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text _
                '    And numeroPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text _
                '    And dataPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Text Then

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CUP")).RowSpan = 1

                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Visible = False
                '    DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan = Math.Max(1, DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan) + 1
                '    DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "CIG")).RowSpan = 1


                '    'DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text = Format(CDec(DataGridPagamenti.Items(i + 1).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text), "#,##0.00")


                'End If


            End If


            If i = DataGridPagamenti.Items.Count - 1 Then
                DataGridPagamenti.Items(i).BackColor = Drawing.ColorTranslator.FromHtml("#DDDDDD")
            Else
                If idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text Then
                    DataGridPagamenti.Items(i).BackColor = DataGridPagamenti.Items(i + 1).BackColor
                Else
                    If DataGridPagamenti.Items(i + 1).BackColor = Drawing.ColorTranslator.FromHtml("#DDDDDD") Then
                        DataGridPagamenti.Items(i).BackColor = Drawing.ColorTranslator.FromHtml("#BBBBBB")
                    Else
                        DataGridPagamenti.Items(i).BackColor = Drawing.ColorTranslator.FromHtml("#DDDDDD")
                    End If
                End If

            End If

            idPagamentoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO")).Text
            codFornitorePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FORNITORE")).Text
            ragioneSocialePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "RAGIONE_SOCIALE")).Text
            codFiscalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_FISCALE")).Text
            numeroCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_CDP")).Text
            'annoCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_CDP")).Text
            dataCDPPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_CDP")).Text
            imponibilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE")).Text
            ivaPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA")).Text
            totPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOT")).Text

            idVocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_VOCE_PF")).Text
            annoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ANNO_PF")).Text
            vocePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "VOCE_PF")).Text
            capitoloPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CAPITOLO")).Text
            imponibileDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPONIBILE_D")).Text
            ivaDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IVA_D")).Text
            totaleDPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "TOTALE_D")).Text

            idPagamentoMMPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_PAGAMENTO_MM")).Text
            numeroPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_PAG")).Text
            dataPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_PAG")).Text
            importoPagatoPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_PAGATO")).Text
            codOperazioneContabilePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONTAB")).Text
            cupPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CUP")).Text
            cigPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "CIG")).Text

            idFatturammPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "ID_FATTURA_MM")).Text
            numeroRDSPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "NUMERO_RDS")).Text
            nFattFornPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "N_FATT_FORN")).Text
            dataFattPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_fATT")).Text
            dataRegistrazionePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "DATA_REGISTRAZIONE")).Text
            codOpContPrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "COD_OP_CONT")).Text
            importoTotalePrecedente = DataGridPagamenti.Items(i).Cells(par.IndDGC(DataGridPagamenti, "IMPORTO_TOTALE")).Text

            If i = 0 Then
                DataGridPagamenti.Items(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#507CD1")
                DataGridPagamenti.Items(i).ForeColor = Drawing.Color.White
                DataGridPagamenti.Items(i).Font.Bold = True
                For K As Integer = 0 To DataGridPagamenti.Columns.Count - 1
                    DataGridPagamenti.Items(i).Cells(K).HorizontalAlign = HorizontalAlign.Center
                Next
            End If
        Next
    End Sub

    Protected Sub AggiustaDataTable(ByVal dtdef As Data.DataTable, dtdef2 As Data.DataTable, dtdef3 As Data.DataTable)
        Try
            Dim codFornitorePrecedente As String = ""
            Dim ragioneSocialePrecedente As String = ""
            Dim codFiscalePrecedente As String = ""
            Dim numeroCDPPrecedente As String = ""
            Dim annoCDPPrecedente As String = ""
            Dim dataCDPPrecedente As String = ""
            Dim imponibilePrecedente As String = ""
            Dim ivaPrecedente As String = ""
            Dim totPrecedente As String = ""
            Dim idPagamentoPrecedente As String = ""
            Dim idVocePrecedente As String = ""
            Dim vocePrecedente As String = ""
            Dim annoPrecedente As String = ""
            Dim capitoloPrecedente As String = ""
            Dim imponibileDPrecedente As String = ""
            Dim ivaDPrecedente As String = ""
            Dim totaleDPrecedente As String = ""
            Dim numeroPrecedente As String = ""
            Dim dataPrecedente As String = ""
            Dim importoPagatoPrecedente As String = ""
            Dim codOperazioneContabilePrecedente As String = ""
            Dim numeroRDSPrecedente As String = ""
            Dim idFatturammPrecedente As String = ""
            Dim nFattFornPrecedente As String = ""
            Dim dataFattPrecedente As String = ""
            Dim dataRegistrazionePrecedente As String = ""
            Dim codOpContPrecedente As String = ""
            Dim importoTotalePrecedente As String = ""
            Dim cupPrecedente As String = ""
            Dim cigPrecedente As String = ""
            Dim idPagamentoMMPrecedente As String = ""



            For Each colonna As Data.DataColumn In dtdef2.Columns
                colonna.DataType = GetType(Integer)
            Next
            Dim riga As Data.DataRow
            For j As Integer = 0 To dtdef.Rows.Count - 1
                riga = dtdef2.NewRow
                For k As Integer = 0 To dtdef.Columns.Count - 1
                    riga.Item(k) = 0
                Next
                dtdef2.Rows.Add(riga)
            Next

            For Each colonna As Data.DataColumn In dtdef3.Columns
                colonna.DataType = GetType(Integer)
            Next
            Dim riga3 As Data.DataRow
            For j As Integer = 0 To dtdef.Rows.Count - 1
                riga3 = dtdef3.NewRow
                For k As Integer = 0 To dtdef.Columns.Count - 1
                    riga3.Item(k) = 0
                Next
                dtdef3.Rows.Add(riga3)
            Next
            

            For i As Integer = dtdef.Rows.Count - 1 To 0 Step -1

                If i = dtdef.Rows.Count - 1 Then
                    dtdef2.Rows(i).Item("ID_PAGAMENTO") = 1
                    idPagamentoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_PAGAMENTO").ToString, "")
                    dtdef2.Rows(i).Item("COD_FORNITORE") = 1
                    codFornitorePrecedente = par.IfEmpty(dtdef.Rows(i).Item("COD_FORNITORE").ToString, "")
                    dtdef2.Rows(i).Item("RAGIONE_SOCIALE") = 1
                    ragioneSocialePrecedente = par.IfEmpty(dtdef.Rows(i).Item("RAGIONE_SOCIALE").ToString, "")
                    dtdef2.Rows(i).Item("COD_FISCALE") = 1
                    codFiscalePrecedente = par.IfEmpty(dtdef.Rows(i).Item("COD_FISCALE").ToString, "")
                    dtdef2.Rows(i).Item("NUMERO_CDP") = 1
                    numeroCDPPrecedente = par.IfEmpty(dtdef.Rows(i).Item("NUMERO_CDP").ToString, "")
                    dtdef2.Rows(i).Item("DATA_CDP") = 1
                    dataCDPPrecedente = par.IfEmpty(dtdef.Rows(i).Item("DATA_CDP").ToString, "")
                    dtdef2.Rows(i).Item("IMPONIBILE") = 1
                    imponibilePrecedente = par.IfEmpty(dtdef.Rows(i).Item("IMPONIBILE").ToString, "")
                    dtdef2.Rows(i).Item("IVA") = 1
                    ivaPrecedente = par.IfEmpty(dtdef.Rows(i).Item("IVA").ToString, "")
                    dtdef2.Rows(i).Item("TOT") = 1
                    totPrecedente = par.IfEmpty(dtdef.Rows(i).Item("TOT").ToString, "")
                    dtdef2.Rows(i).Item("ID_VOCE_PF") = 1
                    idVocePrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_VOCE_PF").ToString, "")
                    dtdef2.Rows(i).Item("ANNO_PF") = 1
                    annoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ANNO_PF").ToString, "")
                    dtdef2.Rows(i).Item("VOCE_PF") = 1
                    vocePrecedente = par.IfEmpty(dtdef.Rows(i).Item("VOCE_PF").ToString, "")
                    dtdef2.Rows(i).Item("CAPITOLO") = 1
                    capitoloPrecedente = par.IfEmpty(dtdef.Rows(i).Item("CAPITOLO").ToString, "")
                    dtdef2.Rows(i).Item("IMPONIBILE_D") = 1
                    imponibileDPrecedente = par.IfEmpty(dtdef.Rows(i).Item("IMPONIBILE_D").ToString, "")
                    dtdef2.Rows(i).Item("IVA_D") = 1
                    ivaDPrecedente = par.IfEmpty(dtdef.Rows(i).Item("IVA_D").ToString, "")
                    dtdef2.Rows(i).Item("TOTALE_D") = 1
                    totaleDPrecedente = par.IfEmpty(dtdef.Rows(i).Item("TOTALE_D").ToString, "")
                    dtdef2.Rows(i).Item("NUMERO_PAG") = 1
                    numeroPrecedente = par.IfEmpty(dtdef.Rows(i).Item("NUMERO_PAG").ToString, "")
                    dtdef2.Rows(i).Item("DATA_PAG") = 1
                    dataPrecedente = par.IfEmpty(dtdef.Rows(i).Item("DATA_PAG").ToString, "")
                    dtdef2.Rows(i).Item("IMPORTO_PAGATO") = 1
                    importoPagatoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("IMPORTO_PAGATO").ToString, "")
                    dtdef2.Rows(i).Item("COD_OP_CONTAB") = 1
                    codOperazioneContabilePrecedente = par.IfEmpty(dtdef.Rows(i).Item("COD_OP_CONTAB").ToString, "")
                    dtdef2.Rows(i).Item("NUMERO_RDS") = 1
                    numeroRDSPrecedente = par.IfEmpty(dtdef.Rows(i).Item("NUMERO_RDS").ToString, "")
                    dtdef2.Rows(i).Item("ID_PAGAMENTO_MM") = 1
                    numeroRDSPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_PAGAMENTO_MM").ToString, "")
                    dtdef2.Rows(i).Item("N_FATT_FORN") = 1
                    nFattFornPrecedente = par.IfEmpty(dtdef.Rows(i).Item("N_FATT_FORN").ToString, "")
                    dtdef2.Rows(i).Item("DATA_fATT") = 1
                    dataFattPrecedente = par.IfEmpty(dtdef.Rows(i).Item("DATA_fATT").ToString, "")
                    dtdef2.Rows(i).Item("DATA_REGISTRAZIONE") = 1
                    dataRegistrazionePrecedente = par.IfEmpty(dtdef.Rows(i).Item("DATA_REGISTRAZIONE").ToString, "")
                    dtdef2.Rows(i).Item("COD_OP_CONT") = 1
                    codOpContPrecedente = par.IfEmpty(dtdef.Rows(i).Item("COD_OP_CONT").ToString, "")
                    dtdef2.Rows(i).Item("IMPORTO_TOTALE") = 1
                    importoTotalePrecedente = par.IfEmpty(dtdef.Rows(i).Item("IMPORTO_TOTALE").ToString, "")
                    dtdef2.Rows(i).Item("CUP") = 1
                    importoTotalePrecedente = par.IfEmpty(dtdef.Rows(i).Item("CUP").ToString, "")
                    dtdef2.Rows(i).Item("CIG") = 1
                    importoTotalePrecedente = par.IfEmpty(dtdef.Rows(i).Item("CIG").ToString, "")
                Else
                    If idPagamentoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_PAGAMENTO").ToString, "") _
                        And codFornitorePrecedente = par.IfEmpty(dtdef.Rows(i).Item("COD_FORNITORE").ToString, "") _
                        And ragioneSocialePrecedente = par.IfEmpty(dtdef.Rows(i).Item("RAGIONE_SOCIALE").ToString, "") _
                        And codFiscalePrecedente = par.IfEmpty(dtdef.Rows(i).Item("COD_FISCALE").ToString, "") _
                        And numeroCDPPrecedente = par.IfEmpty(dtdef.Rows(i).Item("NUMERO_CDP").ToString, "") _
                        And dataCDPPrecedente = par.IfEmpty(dtdef.Rows(i).Item("DATA_CDP").ToString, "") _
                        And imponibilePrecedente = par.IfEmpty(dtdef.Rows(i).Item("IMPONIBILE").ToString, "") _
                        And ivaPrecedente = par.IfEmpty(dtdef.Rows(i).Item("IVA").ToString, "") _
                        And totPrecedente = par.IfEmpty(dtdef.Rows(i).Item("TOT").ToString, "") Then


                        dtdef2.Rows(i).Item("COD_FORNITORE") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("COD_FORNITORE"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("COD_FORNITORE") = 1
                        dtdef3.Rows(i + 1).Item("COD_FORNITORE") = 1

                        dtdef2.Rows(i).Item("RAGIONE_SOCIALE") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("RAGIONE_SOCIALE"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("RAGIONE_SOCIALE") = 1
                        dtdef3.Rows(i + 1).Item("RAGIONE_SOCIALE") = 1

                        dtdef2.Rows(i).Item("COD_FISCALE") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("COD_FISCALE"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("COD_FISCALE") = 1
                        dtdef3.Rows(i + 1).Item("COD_FISCALE") = 1

                        dtdef2.Rows(i).Item("NUMERO_CDP") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("NUMERO_CDP"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("NUMERO_CDP") = 1
                        dtdef3.Rows(i + 1).Item("NUMERO_CDP") = 1

                        dtdef2.Rows(i).Item("DATA_CDP") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("DATA_CDP"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("DATA_CDP") = 1
                        dtdef3.Rows(i + 1).Item("DATA_CDP") = 1

                        dtdef2.Rows(i).Item("IMPONIBILE") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("IMPONIBILE"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("IMPONIBILE") = 1
                        dtdef3.Rows(i + 1).Item("IMPONIBILE") = 1

                        dtdef2.Rows(i).Item("IVA") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("IVA"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("IVA") = 1
                        dtdef3.Rows(i + 1).Item("IVA") = 1

                        dtdef2.Rows(i).Item("TOT") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("TOT"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("TOT") = 1
                        dtdef3.Rows(i + 1).Item("TOT") = 1

                    End If


                    If idPagamentoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_PAGAMENTO").ToString, "") _
                        And idVocePrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_VOCE_PF").ToString, "") _
                        And annoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ANNO_PF").ToString, "") _
                        And vocePrecedente = par.IfEmpty(dtdef.Rows(i).Item("VOCE_PF").ToString, "") _
                        And capitoloPrecedente = par.IfEmpty(dtdef.Rows(i).Item("CAPITOLO").ToString, "") _
                        And imponibileDPrecedente = par.IfEmpty(dtdef.Rows(i).Item("IMPONIBILE_D").ToString, "") _
                        And ivaDPrecedente = par.IfEmpty(dtdef.Rows(i).Item("IVA_D").ToString, "") _
                        And totaleDPrecedente = par.IfEmpty(dtdef.Rows(i).Item("TOTALE_D").ToString, "") Then

                        dtdef2.Rows(i).Item("ID_VOCE_PF") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("ID_VOCE_PF"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("ID_VOCE_PF") = 1
                        dtdef3.Rows(i + 1).Item("ID_VOCE_PF") = 1

                        dtdef2.Rows(i).Item("CAPITOLO") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("CAPITOLO"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("CAPITOLO") = 1
                        dtdef3.Rows(i + 1).Item("CAPITOLO") = 1

                        dtdef2.Rows(i).Item("IMPONIBILE_D") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("IMPONIBILE_D"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("IMPONIBILE_D") = 1
                        dtdef3.Rows(i + 1).Item("IMPONIBILE_D") = 1

                        dtdef2.Rows(i).Item("IVA_D") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("IVA_D"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("IVA_D") = 1
                        dtdef3.Rows(i + 1).Item("IVA_D") = 1

                        dtdef2.Rows(i).Item("TOTALE_D") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("TOTALE_D"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("TOTALE_D") = 1
                        dtdef3.Rows(i + 1).Item("TOTALE_D") = 1

                    End If


                    If idPagamentoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_PAGAMENTO").ToString, "") _
                        And idFatturammPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_FATTURA_MM").ToString, "") _
                        And numeroRDSPrecedente = par.IfEmpty(dtdef.Rows(i).Item("NUMERO_RDS").ToString, "") _
                        And nFattFornPrecedente = par.IfEmpty(dtdef.Rows(i).Item("N_FATT_FORN").ToString, "") _
                        And dataFattPrecedente = par.IfEmpty(dtdef.Rows(i).Item("DATA_fATT").ToString, "") _
                        And dataRegistrazionePrecedente = par.IfEmpty(dtdef.Rows(i).Item("DATA_REGISTRAZIONE").ToString, "") _
                        And codOpContPrecedente = par.IfEmpty(dtdef.Rows(i).Item("COD_OP_CONT").ToString, "") _
                        And importoTotalePrecedente = par.IfEmpty(dtdef.Rows(i).Item("IMPORTO_TOTALE").ToString, "") Then

                        dtdef2.Rows(i).Item("ID_FATTURA_MM") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("ID_FATTURA_MM"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("ID_FATTURA_MM") = 1
                        dtdef3.Rows(i + 1).Item("ID_FATTURA_MM") = 1

                        dtdef2.Rows(i).Item("NUMERO_RDS") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("NUMERO_RDS"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("NUMERO_RDS") = 1
                        dtdef3.Rows(i + 1).Item("NUMERO_RDS") = 1

                        dtdef2.Rows(i).Item("N_FATT_FORN") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("N_FATT_FORN"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("N_FATT_FORN") = 1
                        dtdef3.Rows(i + 1).Item("N_FATT_FORN") = 1

                        dtdef2.Rows(i).Item("DATA_fATT") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("DATA_fATT"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("DATA_fATT") = 1
                        dtdef3.Rows(i + 1).Item("DATA_fATT") = 1

                        dtdef2.Rows(i).Item("DATA_rEGISTRAZIONE") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("DATA_rEGISTRAZIONE"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("DATA_rEGISTRAZIONE") = 1
                        dtdef3.Rows(i + 1).Item("DATA_rEGISTRAZIONE") = 1

                        dtdef2.Rows(i).Item("COD_OP_CONT") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("COD_OP_CONT"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("COD_OP_CONT") = 1
                        dtdef3.Rows(i + 1).Item("COD_OP_CONT") = 1

                        dtdef2.Rows(i).Item("IMPORTO_TOTALE") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("IMPORTO_TOTALE"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("IMPORTO_TOTALE") = 1
                        dtdef3.Rows(i + 1).Item("IMPORTO_TOTALE") = 1

                    End If


                    If idPagamentoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_PAGAMENTO").ToString, "") _
                        And idPagamentoMMPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_PAGAMENTO_MM").ToString, "") _
                        And idVocePrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_VOCE_PF").ToString, "") _
                        And annoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ANNO_PF").ToString, "") _
                        And numeroPrecedente = par.IfEmpty(dtdef.Rows(i).Item("NUMERO_PAG").ToString, "") _
                        And dataPrecedente = par.IfEmpty(dtdef.Rows(i).Item("DATA_PAG").ToString, "") _
                        And importoPagatoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("IMPORTO_PAGATO").ToString, "") _
                        And codOperazioneContabilePrecedente = par.IfEmpty(dtdef.Rows(i).Item("COD_OP_CONTAB").ToString, "") _
                        And cupPrecedente = par.IfEmpty(dtdef.Rows(i).Item("CUP").ToString, "") _
                        And cigPrecedente = par.IfEmpty(dtdef.Rows(i).Item("CIG").ToString, "") Then


                        dtdef2.Rows(i).Item("ID_PAGAMENTO_MM") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("ID_PAGAMENTO_MM"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("ID_PAGAMENTO_MM") = 1
                        dtdef3.Rows(i + 1).Item("ID_PAGAMENTO_MM") = 1

                        dtdef2.Rows(i).Item("NUMERO_PAG") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("NUMERO_PAG"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("NUMERO_PAG") = 1
                        dtdef3.Rows(i + 1).Item("NUMERO_PAG") = 1

                        dtdef2.Rows(i).Item("DATA_PAG") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("DATA_PAG"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("DATA_PAG") = 1
                        dtdef3.Rows(i + 1).Item("DATA_PAG") = 1

                        dtdef2.Rows(i).Item("IMPORTO_PAGATO") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("IMPORTO_PAGATO"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("IMPORTO_PAGATO") = 1
                        dtdef3.Rows(i + 1).Item("IMPORTO_PAGATO") = 1

                        dtdef2.Rows(i).Item("COD_OP_CONTAB") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("COD_OP_CONTAB"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("COD_OP_CONTAB") = 1
                        dtdef3.Rows(i + 1).Item("COD_OP_CONTAB") = 1

                        dtdef2.Rows(i).Item("CUP") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("CUP"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("CUP") = 1
                        dtdef3.Rows(i + 1).Item("CUP") = 1

                        dtdef2.Rows(i).Item("CIG") = Math.Max(1, par.IfEmpty(dtdef2.Rows(i + 1).Item("CIG"), 0)) + 1
                        dtdef2.Rows(i + 1).Item("CIG") = 1
                        dtdef3.Rows(i + 1).Item("CIG") = 1

                    End If
                End If

                idPagamentoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_PAGAMENTO").ToString, "")
                codFornitorePrecedente = par.IfEmpty(dtdef.Rows(i).Item("COD_FORNITORE").ToString, "")
                ragioneSocialePrecedente = par.IfEmpty(dtdef.Rows(i).Item("RAGIONE_SOCIALE").ToString, "")
                codFiscalePrecedente = par.IfEmpty(dtdef.Rows(i).Item("COD_FISCALE").ToString, "")
                numeroCDPPrecedente = par.IfEmpty(dtdef.Rows(i).Item("NUMERO_CDP").ToString, "")
                'annoCDPPrecedente = par.ifempty(dtdef.Rows(i).Item("ANNO_CDP").ToString, "")
                dataCDPPrecedente = par.IfEmpty(dtdef.Rows(i).Item("DATA_CDP").ToString, "")
                imponibilePrecedente = par.IfEmpty(dtdef.Rows(i).Item("IMPONIBILE").ToString, "")
                ivaPrecedente = par.IfEmpty(dtdef.Rows(i).Item("IVA").ToString, "")
                totPrecedente = par.IfEmpty(dtdef.Rows(i).Item("TOT").ToString, "")

                idVocePrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_VOCE_PF").ToString, "")
                annoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ANNO_PF").ToString, "")
                vocePrecedente = par.IfEmpty(dtdef.Rows(i).Item("VOCE_PF").ToString, "")
                capitoloPrecedente = par.IfEmpty(dtdef.Rows(i).Item("CAPITOLO").ToString, "")
                imponibileDPrecedente = par.IfEmpty(dtdef.Rows(i).Item("IMPONIBILE_D").ToString, "")
                ivaDPrecedente = par.IfEmpty(dtdef.Rows(i).Item("IVA_D").ToString, "")
                totaleDPrecedente = par.IfEmpty(dtdef.Rows(i).Item("TOTALE_D").ToString, "")

                idPagamentoMMPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_PAGAMENTO_MM").ToString, "")
                numeroPrecedente = par.IfEmpty(dtdef.Rows(i).Item("NUMERO_PAG").ToString, "")
                dataPrecedente = par.IfEmpty(dtdef.Rows(i).Item("DATA_PAG").ToString, "")
                importoPagatoPrecedente = par.IfEmpty(dtdef.Rows(i).Item("IMPORTO_PAGATO").ToString, "")
                codOperazioneContabilePrecedente = par.IfEmpty(dtdef.Rows(i).Item("COD_OP_CONTAB").ToString, "")
                cupPrecedente = par.IfEmpty(dtdef.Rows(i).Item("CUP").ToString, "")
                cigPrecedente = par.IfEmpty(dtdef.Rows(i).Item("CIG").ToString, "")

                idFatturammPrecedente = par.IfEmpty(dtdef.Rows(i).Item("ID_FATTURA_MM").ToString, "")
                numeroRDSPrecedente = par.IfEmpty(dtdef.Rows(i).Item("NUMERO_RDS").ToString, "")
                nFattFornPrecedente = par.IfEmpty(dtdef.Rows(i).Item("N_FATT_FORN").ToString, "")
                dataFattPrecedente = par.IfEmpty(dtdef.Rows(i).Item("DATA_fATT").ToString, "")
                dataRegistrazionePrecedente = par.IfEmpty(dtdef.Rows(i).Item("DATA_REGISTRAZIONE").ToString, "")
                codOpContPrecedente = par.IfEmpty(dtdef.Rows(i).Item("COD_OP_CONT").ToString, "")
                importoTotalePrecedente = par.IfEmpty(dtdef.Rows(i).Item("IMPORTO_TOTALE").ToString, "")

            Next
        Catch ex As Exception
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End Try
    End Sub

    Protected Sub btnRptCompleto_Click(sender As Object, e As System.EventArgs) Handles btnRptCompleto.Click
        Dim strmZipOutputStream As ZipOutputStream = Nothing
        'Dim contatore As Integer = 0
        Try
            connData.apri()
            Dim nome As String = "ReportPagamentiCompleto-" & Format(Now, "yyyyMMddHHmmss")
            Dim objCrc32 As New Crc32()
            Dim zipfic As String
            zipfic = Server.MapPath("..\..\..\FileTemp\" & nome & ".zip")
            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            Dim strFile As String
            Dim strmFile As FileStream
            Dim theEntry As ZipEntry
            par.cmd.CommandText = "SELECT SUBSTR(INIZIO,1,4) FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO=SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID "
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim stringa As String = ""
            While lettore.Read
                If stringa = "" Then
                    stringa = "SELECT ID,ID_PAGAMENTO,COD_FORNITORE,RAGIONE_SOCIALE,COD_FISCALE,NUMERO_CDP,DATA_CDP,IMPONIBILE,IVA," _
                        & " TOT,ID_VOCE_PF,ANNO_PF,VOCE_PF,CAPITOLO,IMPONIBILE_D,IVA_D,TOTALE_D,ID_FATTURA_MM,NUMERO_RDS," _
                        & " N_FATT_FORN,DATA_FATT,DATA_REGISTRAZIONE,COD_OP_CONT,IMPORTO_TOTALE,ID_PAGAMENTO_MM," _
                        & " NUMERO_PAG,DATA_PAG,IMPORTO_PAGATO,COD_OP_CONTAB,CUP,CIG " _
                        & " FROM SISCOM_MI.REPORT_PAGAMENTI_" & par.IfNull(lettore(0), 0)
                    '& " ORDER BY 1 ASC NULLS FIRST,2,3,4,5,6"
                Else
                    stringa &= " UNION SELECT ID,ID_PAGAMENTO,COD_FORNITORE,RAGIONE_SOCIALE,COD_FISCALE,NUMERO_CDP,DATA_CDP,IMPONIBILE,IVA," _
                        & " TOT,ID_VOCE_PF,ANNO_PF,VOCE_PF,CAPITOLO,IMPONIBILE_D,IVA_D,TOTALE_D,ID_FATTURA_MM,NUMERO_RDS," _
                        & " N_FATT_FORN,DATA_FATT,DATA_REGISTRAZIONE,COD_OP_CONT,IMPORTO_TOTALE,ID_PAGAMENTO_MM," _
                        & " NUMERO_PAG,DATA_PAG,IMPORTO_PAGATO,COD_OP_CONTAB,CUP,CIG " _
                        & " FROM SISCOM_MI.REPORT_PAGAMENTI_" & par.IfNull(lettore(0), 0)
                End If
                stringa &= " UNION ALL SELECT ID,ID_PAGAMENTO,COD_FORNITORE,RAGIONE_SOCIALE,COD_FISCALE,NUMERO_CDP,DATA_CDP,IMPONIBILE,IVA," _
                    & " TOT,ID_VOCE_PF,ANNO_PF,VOCE_PF,CAPITOLO,IMPONIBILE_D,IVA_D,TOTALE_D,ID_FATTURA_MM,NUMERO_RDS," _
                    & " N_FATT_FORN,DATA_FATT,DATA_REGISTRAZIONE,COD_OP_CONT,IMPORTO_TOTALE,ID_PAGAMENTO_MM," _
                    & " NUMERO_PAG,DATA_PAG,IMPORTO_PAGATO,COD_OP_CONTAB,CUP,CIG " _
                    & " FROM SISCOM_MI.REPORT_PAGAMENTI_" & par.IfNull(lettore(0), 0) & "_RIT WHERE ID IS NOT NULL "
            End While
            lettore.Close()
            par.cmd.CommandText = stringa & " ORDER BY 12,3 ASC NULLS FIRST,2,4,5,6,7,13,24"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtDef As New Data.DataTable
            da.Fill(dtDef)
            da.Dispose()
            Dim dtDef2 As Data.DataTable = dtDef.Clone
            Dim dtDef3 As Data.DataTable = dtDef.Clone
            AggiustaDataTable(dtDef, dtDef2, dtDef3)
            'DataGridPagamenti.DataSource = dtDef
            'DataGridPagamenti.DataBind()
            'AggiustaDataGrid()
            Dim xls As New ExcelSiSol
            Dim nomeFile As String = xls.EsportaExcelDaDataGridParzialeConRowspan(ExcelSiSol.Estensione.Office2007_xlsx, "ExportPagamenti", "ExportPagamenti", DataGridPagamenti, , , , 1, , True, dtDef, dtDef2, dtDef3)
            If File.Exists(Server.MapPath("..\..\..\FileTemp\") & nomeFile) Then
                'contatore += 1
                strFile = Server.MapPath("..\..\..\FileTemp\" & nomeFile)
                strmFile = File.OpenRead(strFile)
                Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                Dim sFile As String = Path.GetFileName(strFile)
                theEntry = New ZipEntry(sFile)
                Dim fi As New FileInfo(strFile)
                theEntry.DateTime = fi.LastWriteTime
                theEntry.Size = strmFile.Length
                strmFile.Close()
                objCrc32.Reset()
                objCrc32.Update(abyBuffer)
                theEntry.Crc = objCrc32.Value
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                'ReDim Preserve ElencoFile(i)
                'ElencoFile(i) = "..\..\..\FileTemp\" & nomeFile & ".pdf"
                'i = i + 1
            End If
            connData.chiudi()
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            If File.Exists(Server.MapPath("..\..\..\FileTemp\") & nome & ".zip") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nome & ".zip','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            If strmZipOutputStream.IsFinished = False Then
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
            End If
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End Try
    End Sub




End Class
