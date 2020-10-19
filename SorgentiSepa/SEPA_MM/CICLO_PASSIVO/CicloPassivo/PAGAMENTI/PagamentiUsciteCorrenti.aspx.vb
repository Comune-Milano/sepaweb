Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Math

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_PagamentiUsciteCorrenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim totale As Decimal = 0
    Dim condizioneStruttura As String = ""
    Dim condizioneStrutturaRit As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("BP_RESIDUI") <> 1 Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)
        If Not IsPostBack Then
            Response.Flush()
            CaricaTabella()
        End If
    End Sub
    Protected Sub CaricaTabella()
        Dim Esercizio As String = ""
        Try
            Dim ANNO As String = ""
            Dim ANNO2 As String = ""
            ApriConnessione()
            Dim DATAAL As String = ""

            Dim voce As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE ID=" & Request.QueryString("ID_VOCE")
            Dim LettoreVoce As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettoreVoce.Read Then
                voce = par.IfNull(LettoreVoce("CODICE"), "") & "  " & par.IfNull(LettoreVoce("DESCRIZIONE"), "")
            End If
            LettoreVoce.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND PF_MAIN.ID='" & Request.QueryString("ID_PF") & "' "
            Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LETTORE.Read Then
                Esercizio = "Esercizio Finanziario " & par.FormattaData(par.IfNull(LETTORE("INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(LETTORE("FINE"), ""))
                DATAAL = par.IfNull(LETTORE("FINE"), "")
                ANNO = Left(DATAAL, 4)
                ANNO2 = CStr(CInt(ANNO) + 1)
                Titolo.Text = "Dettaglio Pagamenti - Voce " & voce & " - " & Esercizio
            End If
            LETTORE.Close()
            If Not IsNothing(Request.QueryString("ID_STRUTTURA")) Then
                If Request.QueryString("ID_STRUTTURA") = "-1" Then
                    condizioneStruttura = ""
                    condizioneStrutturaRit = ""
                Else
                    condizioneStruttura = " AND PAGAMENTI_LIQUIDATI.ID_STRUTTURA=" & Request.QueryString("ID_STRUTTURA") & " "
                    condizioneStrutturaRit = " AND PAGAMENTI_RIT_LIQUIDATI.ID_STRUTTURA=" & Request.QueryString("ID_STRUTTURA") & " "
                End If
            End If

            '& " (CASE WHEN PAGAMENTI.TIPO_PAGAMENTO=3 OR PAGAMENTI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
            par.cmd.CommandText = " SELECT PF_VOCI.CODICE, " _
                & " PF_VOCI.DESCRIZIONE AS VOCE, " _
                & " TIPO_PAGAMENTI.DESCRIZIONE AS TIPO_PAGAMENTO, " _
                & " PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_PAG, " _
                & " (SELECT DISTINCT NOME " _
                & " FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.PRENOTAZIONI " _
                & " WHERE PRENOTAZIONI.ID_STRUTTURA = TAB_FILIALI.ID " _
                & " AND PRENOTAZIONI.ID_PAGAMENTO = PAGAMENTI.ID) " _
                & " AS STRUTTURA, " _
                & " FORNITORI.RAGIONE_SOCIALE, " _
                & " TRIM (TO_CHAR ( (NVL (IMPORTO, 0)), '999G999G990D99')) AS IMPORTO_LIQUIDATO, " _
                & " (SELECT SUM(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO=PAGAMENTI_LIQUIDATI.ID_PAGAMENTO AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PRENOTAZIONI.ID_VOCE_PF) AS IMPORTO_LIQUIDATO2, " _
                & " APPALTI.NUM_REPERTORIO, " _
                & " PAGAMENTI_LIQUIDATI.NUM_MANDATO ||'/'|| PAGAMENTI_LIQUIDATI.ANNO_MANDATO AS MANDATO, " _
                & " TO_CHAR(TO_DATE(PAGAMENTI_LIQUIDATI.DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy')  AS DATA_MANDATO, " _
                & " PAGAMENTI.PROGR ||'/'||PAGAMENTI.ANNO AS MAE, " _
                & "'<a href=""javascript:ApriPagamenti('|| PAGAMENTI.ID || ',' || PAGAMENTI.TIPO_PAGAMENTO || ');"">' ||" _
                & " CASE WHEN PAGAMENTI.PROGR IS NOT NULL THEN PAGAMENTI.PROGR ||'/'|| PAGAMENTI.ANNO ELSE '' END " _
                & "|| '</a>' AS ADP, " _
                & " TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ADP," _
                & " TRIM(TO_CHAR(PAGAMENTI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO_CONSUNTIVATO,PAGAMENTI.ID AS ID_PAGAMENTO " _
                & " FROM SISCOM_MI.PAGAMENTI_LIQUIDATI, " _
                & " SISCOM_MI.PF_VOCI, " _
                & " SISCOM_MI.PAGAMENTI, " _
                & " SISCOM_MI.FORNITORI, " _
                & " SISCOM_MI.TIPO_PAGAMENTI, " _
                & " /*SISCOM_MI.MANUTENZIONI,*/ " _
                & " SISCOM_MI.APPALTI " _
                & " WHERE     PAGAMENTI_LIQUIDATI.ANNO_MANDATO >= '" & ANNO2 & "' " _
                & " /*AND MANUTENZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+)*/ " _
                & " AND PAGAMENTI_LIQUIDATI.DATA_MANDATO > '" & DATAAL & "' " _
                & condizioneStruttura _
                & " AND ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE fl_cc=0 and CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ") " _
                & " AND PF_VOCI.ID = PAGAMENTI_LIQUIDATI.ID_VOCE_PF " _
                & " AND PAGAMENTI.ID = PAGAMENTI_LIQUIDATI.ID_PAGAMENTO " _
                & " AND PAGAMENTI.ID_FORNITORE = FORNITORI.ID(+) " _
                & " AND TIPO_PAGAMENTI.ID = PAGAMENTI.TIPO_PAGAMENTO " _
                & " AND APPALTI.ID(+) = PAGAMENTI.ID_APPALTO " _
                & " AND FL_CC <> 1 " _
                & "UNION " _
                & " SELECT PF_VOCI.CODICE, " _
                & " PF_VOCI.DESCRIZIONE AS VOCE, " _
                & " TIPO_PAGAMENTI.DESCRIZIONE AS TIPO_PAGAMENTO, " _
                & " PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_PAG, " _
                & " (SELECT DISTINCT NOME " _
                & " FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.PRENOTAZIONI " _
                & " WHERE PRENOTAZIONI.ID_STRUTTURA = TAB_FILIALI.ID " _
                & " AND PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID) " _
                & " AS STRUTTURA, " _
                & " FORNITORI.RAGIONE_SOCIALE, " _
                & " TRIM (TO_CHAR ( (NVL (IMPORTO, 0)), '999G999G990D99')) AS IMPORTO_LIQUIDATO, " _
                & " (SELECT SUM(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI_RIT_LIQUIDATI.ID_PAGAMENTO_RIT_LEGGE AND PAGAMENTI_RIT_LIQUIDATI.ID_VOCE_PF=PRENOTAZIONI.ID_VOCE_PF) AS IMPORTO_LIQUIDATO2, " _
                & " APPALTI.NUM_REPERTORIO, " _
                & " PAGAMENTI_RIT_LIQUIDATI.NUM_MANDATO ||'/'|| PAGAMENTI_RIT_LIQUIDATI.ANNO_MANDATO AS MANDATO, " _
                & " TO_CHAR(TO_DATE(PAGAMENTI_RIT_LIQUIDATI.DATA_MANDATO,'yyyyMMdd'),'dd/MM/yyyy')  AS DATA_MANDATO, " _
                & " PAGAMENTI.PROGR ||'/'||PAGAMENTI.ANNO AS MAE, " _
                & "'<a href=""javascript:ApriPagamenti('|| PAGAMENTI.ID || ',' || PAGAMENTI.TIPO_PAGAMENTO || ');"">' ||" _
                & " CASE WHEN PAGAMENTI.PROGR IS NOT NULL THEN PAGAMENTI.PROGR ||'/'|| PAGAMENTI.ANNO ELSE '' END " _
                & "|| '</a>' AS ADP, " _
                & " TO_CHAR(TO_DATE(DATA_EMISSIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ADP," _
                & " TRIM(TO_CHAR(PAGAMENTI.IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO_CONSUNTIVATO,PAGAMENTI.ID AS ID_PAGAMENTO_RIT_LEGGE " _
                & " FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI, " _
                & " SISCOM_MI.PF_VOCI, " _
                & " SISCOM_MI.PAGAMENTI, " _
                & " SISCOM_MI.FORNITORI, " _
                & " SISCOM_MI.TIPO_PAGAMENTI, " _
                & " /*SISCOM_MI.MANUTENZIONI,*/ " _
                & " SISCOM_MI.APPALTI " _
                & " WHERE     PAGAMENTI_RIT_LIQUIDATI.ANNO_MANDATO >= '" & ANNO2 & "' " _
                & " /*AND MANUTENZIONI.ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI.ID(+)*/ " _
                & " AND PAGAMENTI_RIT_LIQUIDATI.DATA_MANDATO > '" & DATAAL & "' " _
                & condizioneStrutturaRit _
                & " AND ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE fl_cc=0 and CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ") " _
                & " AND PF_VOCI.ID = PAGAMENTI_RIT_LIQUIDATI.ID_VOCE_PF " _
                & " AND PAGAMENTI.ID = PAGAMENTI_RIT_LIQUIDATI.ID_PAGAMENTO_RIT_LEGGE " _
                & " AND PAGAMENTI.ID_FORNITORE = FORNITORI.ID(+) " _
                & " AND TIPO_PAGAMENTI.ID = PAGAMENTI.TIPO_PAGAMENTO " _
                & " AND APPALTI.ID(+) = PAGAMENTI.ID_APPALTO " _
                & " AND FL_CC <> 1 "



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                DataGrid.DataSource = dt
                DataGrid.DataBind()


                Dim LettoreP As Oracle.DataAccess.Client.OracleDataReader = Nothing
                For Each RIGA As DataGridItem In DataGrid.Items

                    par.cmd.CommandText = "SELECT DISTINCT " _
                        & " TAB_SERVIZI.DESCRIZIONE " _
                        & " FROM SISCOM_MI.PRENOTAZIONI, " _
                        & " SISCOM_MI.PF_VOCI_IMPORTO, " _
                        & " SISCOM_MI.TAB_sERVIZI " _
                        & " WHERE ID_PAGAMENTO = '" & RIGA.Cells(14).Text & "' " _
                        & " AND PF_VOCI_IMPORTO.ID = PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                        & " AND TAB_SERVIZI.ID = PF_VOCI_IMPORTO.ID_SERVIZIO "

                    LettoreP = par.cmd.ExecuteReader
                    Dim app As String = ""
                    While LettoreP.Read
                        app = app & par.IfNull(LettoreP(0), "") & "<br />"
                    End While
                    LettoreP.Close()
                    If Len(app) > 6 Then
                        app = Left(app, Len(app) - 6)
                    End If
                    CType(RIGA.FindControl("serviziDGR"), Label).Text = app
                Next


            Else
                Errore.Text = "Nessun dato disponibile per la voce selezionata!"
            End If

            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            btnStampa.Visible = False
            btnExport.Visible = False
            Errore.Text = "Si è verificato un errore durante il caricamento dei dati!"
        End Try
    End Sub
    Protected Sub chiudiConnessione()
        'CHIUSURA CONNESSIONE
        '************************
        par.cmd.Dispose()
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '************************
    End Sub
    Protected Sub ApriConnessione()
        'APERTURA CONNESSIONE E TRANSAZIONE
        '************************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        '************************
    End Sub
 Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        Dim nomeFile As String = par.StampaDataGridPDF(DataGrid, "StampaUsciteCorrenti", Titolo.Text)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Write("<script>window.open('../../../FileTemp/" & nomeFile & "');</script>")
            FIN.Value = "1"
        Else
            Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!')</script>")
        End If
    End Sub
  Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Dim nomeFile As String = EsportaExcelAutomaticoDaDataGrid(DataGrid, "ExportStampaUsciteCorrenti")
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../../../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub

  
    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Protected Sub DataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                totale += CType((e.Item.Cells(12).Text), Double)

            Case ListItemType.Footer
                e.Item.Cells(12).Text = Format(totale, "#,##0.00")
        End Select
    End Sub
    Function EsportaExcelAutomaticoDaDataGrid(ByVal datagrid As DataGrid, Optional ByVal nomeFile As String = "", Optional ByVal FattoreLarghezzaColonne As Decimal = 4.75, Optional ByVal EliminazioneLink As Boolean = True) As String
        Try
            'CONTO IL NUMERO DELLE COLONNE DEL DATAGRID
            Dim NumeroColonneDatagrid As Integer = datagrid.Columns.Count
            'CONTO IL NUMERO DELLE COLONNE VISIBILI DEL DATAGRID
            Dim NumeroColonneVisibiliDatagrid As Integer = 0
            For indiceColonna As Integer = 0 To NumeroColonneDatagrid - 1 Step 1
                If datagrid.Columns.Item(indiceColonna).Visible = True Then
                    NumeroColonneVisibiliDatagrid = NumeroColonneVisibiliDatagrid + 1
                End If
            Next
            'INIZIALIZZAZIONE RIGHE, COLONNE E FILENAME
            Dim FileExcel As New CM.ExcelFile
            Dim indiceRighe As Long = 0
            Dim IndiceColonne As Long = 1
            Dim FileName As String = nomeFile & Format(Now, "yyyyMMddHHmmss")
            Dim LarghezzaMinimaColonna As Integer = 30
            Dim allineamentoCella As String = "Center"
            Dim LarghezzaDataGrid As Integer = Max(datagrid.Width.Value, 200)
            Dim TipoLarghezzaDataGrid As UnitType = datagrid.Width.Type
            Dim LarghezzaColonnaHeader As Decimal = 0
            Dim LarghezzaColonnaItem As Decimal = 0
            'SETTO A ZERO LA VARIABILE DELLE RIGHE
            indiceRighe = 0
            With FileExcel
                'CREO IL FILE 
                .CreateFile(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & FileName & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                'GESTIONE LARGHEZZA DELLE COLONNE TRAMITE FATTORE DATO IN INPUT OPZIONALE
                Dim indiceVisibile As Integer = 1
                For j = 0 To NumeroColonneDatagrid - 1 Step 1
                    'GESTIONE LARGHEZZA DELLE COLONNE TRAMITE FATTORE DATO IN INPUT OPZIONALE
                    If datagrid.Columns.Item(j).Visible = True Then
                        If datagrid.Columns.Item(j).HeaderStyle.Width.Type = UnitType.Pixel Then
                            If TipoLarghezzaDataGrid = UnitType.Pixel Then
                                LarghezzaColonnaHeader = datagrid.Columns.Item(j).HeaderStyle.Width.Value * 100 / LarghezzaDataGrid
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        Else
                            If TipoLarghezzaDataGrid = UnitType.Percentage Then
                                LarghezzaColonnaHeader = datagrid.Columns.Item(j).HeaderStyle.Width.Value
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        End If

                        If datagrid.Columns.Item(j).ItemStyle.Width.Type = UnitType.Pixel Then
                            If TipoLarghezzaDataGrid = UnitType.Pixel Then
                                LarghezzaColonnaHeader = datagrid.Columns.Item(j).ItemStyle.Width.Value * 100 / LarghezzaDataGrid
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        Else
                            If TipoLarghezzaDataGrid = UnitType.Percentage Then
                                LarghezzaColonnaHeader = datagrid.Columns.Item(j).ItemStyle.Width.Value
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        End If
                        LarghezzaMinimaColonna = FattoreLarghezzaColonne * Max(LarghezzaColonnaHeader, LarghezzaColonnaItem)
                        .SetColumnWidth(indiceVisibile, indiceVisibile, Max(LarghezzaMinimaColonna, 30))
                        'GESTIONE DELLE INTESTAZIONI
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, IndiceColonne, indiceVisibile, datagrid.Columns.Item(j).HeaderText, 0)
                        indiceVisibile = indiceVisibile + 1
                    End If
                Next
                indiceRighe = indiceRighe + 1
                For Each Items As DataGridItem In datagrid.Items
                    indiceRighe = indiceRighe + 1
                    Dim Cella As Integer = 0
                    For IndiceColonne = 0 To NumeroColonneDatagrid - 1
                        'RIEPILOGO ALLINEAMENTI
                        'CENTER 2,LEFT 1,RIGHT 3
                        'CONSIDERO DI FORMATO NUMERICO TUTTE LE CELLE CON ALLINEAMENTO A DESTRA
                        If datagrid.Columns.Item(IndiceColonne).Visible = True Then
                            allineamentoCella = datagrid.Columns.Item(IndiceColonne).ItemStyle.HorizontalAlign
                            If IndiceColonne = 2 Then
                                Dim app As String = CType(Items.Cells(2).FindControl("serviziDGR"), Label).Text
                                app = Replace(app, "<br />", ";")
                                If app = "" Then
                                    app = " "
                                End If
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, strip_tags(app), 0)
                            Else
                                Select Case EliminazioneLink
                                    Case False
                                        Select Case allineamentoCella
                                            Case 1
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), 0)
                                            Case 2
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), 0)
                                            Case 3
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", ""), 4)
                                            Case Else
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), 0)
                                        End Select

                                    Case True
                                        Select Case allineamentoCella
                                            Case 1
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                            Case 2
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                            Case 3
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", "")), 4)
                                            Case Else
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                        End Select
                                    Case Else
                                        Select Case allineamentoCella
                                            Case 1
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                            Case 2
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                            Case 3
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", "")), 4)
                                            Case Else
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                        End Select
                                End Select
                            End If
                            Cella = Cella + 1
                        End If
                    Next

                Next
                'CHIUSURA FILE
                .CloseFile()
            End With
            'COSTRUZIONE ZIPFILE
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream

            Dim strFile As String
            strFile = System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & FileName & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)
            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            Dim zipfic As String
            zipfic = System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & FileName & ".zip")
            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            File.Delete(strFile)
            Dim FileNameZip As String = FileName & ".zip"
            Return FileNameZip
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Function strip_tags(ByVal strHTML As String, Optional allowedTags As String = "") As String
        Dim regHtml As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("<[^>]*>")
        Dim s As String = regHtml.Replace(strHTML, "")
        If s = "" Then
            s = " "
        End If
        Return s
    End Function

  

   
End Class