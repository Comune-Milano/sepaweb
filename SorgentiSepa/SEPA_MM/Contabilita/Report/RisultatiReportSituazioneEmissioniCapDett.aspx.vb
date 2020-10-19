Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip

Partial Class Contabilita_Report_RisultatiReportSituazioneEmissioniCapDett
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)
        If Not IsPostBack Then
            Response.Flush()
            caricaDati()
            filtriRicerca = Session.Item("filtriRicerca")
            Session.Remove("filtriRicerca")
        End If
    End Sub
    Public Property queryBolBolletteVoci() As String
        Get
            If Not (ViewState("queryBolBolletteVoci") Is Nothing) Then
                Return CStr(ViewState("queryBolBolletteVoci"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("queryBolBolletteVoci") = value
        End Set
    End Property
    Public Property filtriRicerca() As String
        Get
            If Not (ViewState("filtriRicerca") Is Nothing) Then
                Return CStr(ViewState("filtriRicerca"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("filtriRicerca") = value
        End Set
    End Property

    Protected Sub chiudiConnessione()
        par.cmd.Dispose()
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
    Protected Sub ApriConnessione()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
    End Sub
    Private Sub caricaDati()
        Dim macroCategoriaSi As Boolean = False
        Dim categoriaSi As Boolean = False
        Dim TipologiaUISi As Boolean = False
        Dim vociSi As Boolean = False
        Dim competenzaSi As Boolean = False
        Dim capitoliSi As Boolean = True

        '##########################################################################
        Dim dataAggiornamento As String = ""
        If Not IsNothing(Request.QueryString("Aggiornamento")) AndAlso Trim(Request.QueryString("Aggiornamento")) <> "" Then
            dataAggiornamento = Request.QueryString("Aggiornamento")
        Else
            dataAggiornamento = Format(Now, "yyyyMMdd")
        End If
        Dim condizioneAggiornamento As String = ""
        If dataAggiornamento <> "" Then
            condizioneAggiornamento = " DATA_INSERIMENTO_BOL_BOLLETTE<=" & dataAggiornamento
        End If
        '##########################################################################

        Dim dataEmissioneDal As String = ""
        If Not IsNothing(Request.QueryString("EmissioneDal")) Then
            dataEmissioneDal = Request.QueryString("EmissioneDal")
        End If
        Dim condizioneEmissioneDal As String = ""
        If dataEmissioneDal <> "" Then
            condizioneEmissioneDal = " AND DATA_EMISSIONE_BOL_BOLLETTE>=" & dataEmissioneDal
        End If
        Dim dataEmissioneAl As String = ""
        If Not IsNothing(Request.QueryString("EmissioneAl")) Then
            dataEmissioneAl = Request.QueryString("EmissioneAl")
        End If
        Dim condizioneEmissioneAl As String = ""
        If dataEmissioneAl <> "" Then
            condizioneEmissioneAl = " AND DATA_EMISSIONE_BOL_BOLLETTE<=" & dataEmissioneAl
        End If
        '##########################################################################
        Dim dataRiferimentoDal As String = ""
        If Not IsNothing(Request.QueryString("RiferimentoDal")) Then
            dataRiferimentoDal = Request.QueryString("RiferimentoDal")
        End If
        Dim condizioneRiferimentoDal As String = ""
        If dataRiferimentoDal <> "" Then
            condizioneRiferimentoDal = " AND RIFERIMENTO_DA_BOL_BOLLETTE>=" & dataRiferimentoDal
        End If
        Dim dataRiferimentoAl As String = ""
        If Not IsNothing(Request.QueryString("RiferimentoAl")) Then
            dataRiferimentoAl = Request.QueryString("RiferimentoAl")
        End If
        Dim condizioneRiferimentoAl As String = ""
        If dataRiferimentoAl <> "" Then
            condizioneRiferimentoAl = " AND RIFERIMENTO_A_BOL_BOLLETTE<=" & dataRiferimentoAl
        End If
        '##########################################################################
        Dim listaBollettazione As System.Collections.Generic.List(Of String) = Session.Item("listaTipologiaBollettazione")
        Session.Remove("listaTipologiaBollettazione")
        Dim condizioneTipologiaBollettazione As String = ""
        If Not IsNothing(listaBollettazione) Then
            For Each Items As String In listaBollettazione
                If CInt(Items) = 100 Then
                    condizioneTipologiaBollettazione = "100"
                    Exit For
                Else
                    condizioneTipologiaBollettazione &= Items & "," & CStr(CInt(Items) + 100) & ","
                End If
            Next
        End If
        If condizioneTipologiaBollettazione <> "" Then
            'condizioneTipologiaBollettazione &= "22,"
            If condizioneTipologiaBollettazione = "100" Then
                condizioneTipologiaBollettazione = " AND ID_TIPO_BOL_BOLLETTE>=100"
            Else
                condizioneTipologiaBollettazione = Left(condizioneTipologiaBollettazione, Len(condizioneTipologiaBollettazione) - 1)
                condizioneTipologiaBollettazione = " AND ID_TIPO_BOL_BOLLETTE IN (" & condizioneTipologiaBollettazione & ") "
            End If
        Else
            'condizioneTipologiaBollettazione = " AND (BOL_BOLLETTE.ID_TIPO IN (1,2,7) OR BOL_BOLLETTE.ID_TIPO>20) "
            condizioneTipologiaBollettazione = " "
        End If
        '##########################################################################

        Dim listaEserciziContabili As System.Collections.Generic.List(Of String) = Session.Item("listaEserciziContabili")
        Session.Remove("listaEserciziContabili")
        Dim condizioneListaEserciziContabili As String = ""
        If Not IsNothing(listaEserciziContabili) Then
            For Each Items As String In listaEserciziContabili
                condizioneListaEserciziContabili &= Items & ","
            Next
        End If
        Dim condizioneEsercizioContabile As String = ""
        Dim fromEsercizioContabile As String = ""
        Dim selectEsercizioContabile As String = ""
        Dim fromEsercizioContabileB As String = ""
        Dim groupByEsercizioContabile As String = ""
        If condizioneListaEserciziContabili <> "" Then
            condizioneListaEserciziContabili = Left(condizioneListaEserciziContabili, Len(condizioneListaEserciziContabili) - 1)
        End If
        If condizioneListaEserciziContabili <> "" Then
            condizioneEsercizioContabile = " AND ID_ES_CONTABILE IN (" & condizioneListaEserciziContabili & ") "
        End If

        selectEsercizioContabile = "INITCAP(CAPITOLO) AS CAPITOLO,"
        groupByEsercizioContabile = "INITCAP(CAPITOLO) "


        '##########################################################################
        Dim condizioneListaVoci As String = ""
        If Not IsNothing(Request.QueryString("TutteVoci")) AndAlso IsNumeric(Request.QueryString("TutteVoci")) AndAlso Request.QueryString("TutteVoci") = "0" Then
            Dim listaVoci As System.Collections.Generic.List(Of String) = Session.Item("listaVoci")
            Session.Remove("listaVoci")
            If Not IsNothing(listaVoci) Then
                For Each Items As String In listaVoci
                    condizioneListaVoci &= Items & ","
                Next
            End If
            If condizioneListaVoci <> "" Then
                condizioneListaVoci = Left(condizioneListaVoci, Len(condizioneListaVoci) - 1)
                condizioneListaVoci = " AND ID_VOCE_BOL_BOLLETTE_VOCI IN (" & condizioneListaVoci & ") "
                vociSi = True
            End If
        Else
            Session.Remove("listaVoci")
            vociSi = True
        End If
        '##########################################################################
        Dim listaCategorie As System.Collections.Generic.List(Of String) = Session.Item("listaCategorie")
        Session.Remove("listaCategorie")
        Dim condizioneCategoria As String = ""
        If Not IsNothing(listaCategorie) Then
            For Each Items As String In listaCategorie
                condizioneCategoria &= Items & ","
            Next
        End If
        If condizioneCategoria <> "" Then
            condizioneCategoria = Left(condizioneCategoria, Len(condizioneCategoria) - 1)
            condizioneCategoria = " AND TIPO_VOCE_T_VOCI_BOLLETTA IN (" & condizioneCategoria & ") "
            categoriaSi = True
        End If
        '##########################################################################
        Dim listaMacroCategorie As System.Collections.Generic.List(Of String) = Session.Item("listaMacrocategorie")
        Session.Remove("listaMacrocategorie")
        Dim condizionemacroCategoria As String = ""
        If Not IsNothing(listaMacroCategorie) Then
            For Each Items As String In listaMacroCategorie
                condizionemacroCategoria &= Items & ","
            Next
        End If
        If condizionemacroCategoria <> "" Then
            condizionemacroCategoria = Left(condizionemacroCategoria, Len(condizionemacroCategoria) - 1)
            condizionemacroCategoria = " AND GRUPPO_T_VOCI_BOLLETTA IN (" & condizionemacroCategoria & ") "
            macroCategoriaSi = True
        End If
        '##########################################################################
        Dim listaCapitoli As System.Collections.Generic.List(Of String) = Session.Item("listaCapitoli")
        Session.Remove("listaCapitoli")
        Dim condizioneCapitoli As String = ""
        If Not IsNothing(listaCapitoli) Then
            For Each Items As String In listaCapitoli
                condizioneCapitoli &= Items & ","
            Next
        End If
        If condizioneCapitoli <> "" Then
            condizioneCapitoli = Left(condizioneCapitoli, Len(condizioneCapitoli) - 1)
            condizioneCapitoli = " AND ID_T_VOCI_BOLLETTA_CAP IN (" & condizioneCapitoli & ") "
        End If
        '##########################################################################
        Dim listatipologieUI As System.Collections.Generic.List(Of String) = Session.Item("listatipologieUI")
        Session.Remove("listatipologieUI")
        Dim condizionetipologiaUI As String = ""
        If Not IsNothing(listatipologieUI) Then
            For Each Items As String In listatipologieUI
                condizionetipologiaUI &= "'" & Items & "',"
            Next
        End If
        If condizionetipologiaUI <> "" Then
            condizionetipologiaUI = Left(condizionetipologiaUI, Len(condizionetipologiaUI) - 1)
            condizionetipologiaUI = " AND COD_TIPOLOGIA_UI IN (" & condizionetipologiaUI & ") "
            TipologiaUISi = True
        End If
        '##########################################################################
        Dim listaCompetenza As System.Collections.Generic.List(Of String) = Session.Item("listaCompetenza")
        Session.Remove("listaCompetenza")
        Dim condizioneCompetenza As String = ""
        If Not IsNothing(listaCompetenza) Then
            For Each Items As String In listaCompetenza
                condizioneCompetenza &= Items & ","
            Next
        End If
        If condizioneCompetenza <> "" Then
            condizioneCompetenza = Left(condizioneCompetenza, Len(condizioneCompetenza) - 1)
            condizioneCompetenza = " AND COMPETENZA_T_VOCI_BOLLETTA IN (" & condizioneCompetenza & ") "

            competenzaSi = True
        End If
        '##########################################################################
        Dim tipologiaCondominio As String = ""
        Dim condizioneTipologiaCondominio As String = ""
        If Not IsNothing(Request.QueryString("Condominio")) Then
            tipologiaCondominio = Request.QueryString("Condominio")
            Select Case tipologiaCondominio
                Case -1
                    'nessuna condizione
                    condizioneTipologiaCondominio = ""
                Case 0
                    'non in condominio
                    condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO=0 "
                Case 1
                    'condomini gestione diretta
                    condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO=1 "
                Case 2
                    'condomini gestione indiretta
                    condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO=2 "
                Case 3
                    'tutti i condomini
                    condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO>0 "
                Case Else
                    'nessuna condizione
                    condizioneTipologiaCondominio = ""
            End Select
        End If
        '##########################################################################
        Dim condizioneAccertato As String = ""
        If Request.QueryString("Accertato") = 2 Then
            condizioneAccertato = ""
        ElseIf Request.QueryString("Accertato") = 1 Then
            condizioneAccertato = " AND FL_ACCERTATO_BOL_BOLLETTE_VOCI=1 "
        ElseIf Request.QueryString("Accertato") = 0 Then
            condizioneAccertato = " AND FL_ACCERTATO_BOL_BOLLETTE_VOCI=0 "
        Else
            condizioneAccertato = ""
        End If
        '##########################################################################
        Dim condizioneVociDaAccertare As String = ""
        If Request.QueryString("VociDaAccertare") = 2 Then
            condizioneVociDaAccertare = ""
        ElseIf Request.QueryString("VociDaAccertare") = 1 Then
            condizioneVociDaAccertare = " AND ACCERTARE_EM_T_VOCI_BOLLETTA=1 "
        ElseIf Request.QueryString("VociDaAccertare") = 0 Then
            condizioneVociDaAccertare = " AND ACCERTARE_EM_T_VOCI_BOLLETTA=0 "
        Else
            condizioneVociDaAccertare = ""
        End If
        '##########################################################################



        Dim selectMacroCategoria As String = ""
        Dim selectCategoria As String = ""
        Dim selectVoci As String = ""
        Dim selectTipologiaUI As String = ""
        Dim selectCompetenza As String = ""
        Dim groupByMacroCategoria As String = ""
        Dim groupByCategoria As String = ""
        Dim groupByVoci As String = ""
        Dim groupByTipologiaUI As String = ""
        Dim groupByCompetenza As String = ""


        selectMacroCategoria = " INITCAP(nvl(MACROCATEGORIA,'')) AS MACROCATEGORIA, "
        groupByMacroCategoria = ",nvl(MACROCATEGORIA,'') "


        selectCategoria = " INITCAP(nvl(CATEGORIA,'')) AS CATEGORIA, "
        groupByCategoria = ",nvl(CATEGORIA,'') "

        
        selectVoci = " INITCAP(nvl(VOCE,'')) AS VOCE, "
        



        selectCompetenza = " INITCAP(COMPETENZA) AS COMPETENZA, "
        groupByCompetenza = " ,COMPETENZA "


        selectTipologiaUI = " INITCAP(USI_ABITATIVI) AS USI_ABITATIVI, INITCAP(NVL(TIPO_UI,'')) AS TIPO_UI, "
        groupByTipologiaUI = ",USI_ABITATIVI,TIPO_UI "

        Dim dettaglio As String = ""
        Dim dettaglioGroup As String = ""

        dettaglio = " ANNO_RIF AS ANNO, " _
            & " INITCAP(BIMESTRE) AS BIMESTRE, "
        dettaglioGroup = ",ANNO_RIF, BIMESTRE "


        Try
            ApriConnessione()
            Dim condizioneAnnulli As String = ""
            Dim condizioneTotale As String = ""
            If dataAggiornamento <> "" Then
                condizioneAnnulli = " TRIM(TO_CHAR((NVL(CASE WHEN (DATA_ANNULLO<=" & dataAggiornamento & ") THEN (IMPORTO) ELSE (0) END,0)),'999G999G990D99')) AS ANNULLI, "
                condizioneTotale = " TRIM(TO_CHAR((NVL(IMPORTO,0))-(NVL(CASE WHEN (DATA_ANNULLO<=" & dataAggiornamento & ") THEN (IMPORTO) ELSE (0) END,0)),'999G999G990D99')) AS TOTALE, "
            Else
                condizioneAnnulli = " TRIM(TO_CHAR((NVL(CASE WHEN (DATA_ANNULLO<=" & Format(Now, "yyyyMMdd") & ") THEN (IMPORTO) ELSE (0) END,0)),'999G999G990D99')) AS ANNULLI, "
                condizioneTotale = " TRIM(TO_CHAR((NVL(IMPORTO,0))-(NVL(CASE WHEN (DATA_ANNULLO<=" & dataAggiornamento & ") THEN (IMPORTO) ELSE (0) END,0)),'999G999G990D99')) AS TOTALE, "
            End If

            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_EMISSIONI SET ID_BOL_BOLLETTE_VOCI=ID_BOL_BOLLETTE_VOCI WHERE DATA_EMISSIONE_BOL_BOLLETTE=0"
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_REPORT.NEXTVAL FROM DUAL"
            Dim idReport As Integer = par.cmd.ExecuteScalar

            par.cmd.CommandText = " SELECT  ''''||COD_CONTRATTO AS COD_CONTRATTO,INTESTATARIO,NUM_BOLLETTA," _
                & selectEsercizioContabile _
                & " INITCAP(BOLLETTAZIONE) AS BOLLETTAZIONE,ANNO_ES_CONTABILE, " _
                & dettaglio _
                & selectCompetenza _
                & selectMacroCategoria _
                & selectCategoria _
                & selectVoci _
                & selectTipologiaUI _
                & " TRIM(TO_CHAR((NVL(IMPORTO,0)),'999G999G990D99')) AS importo, " _
                & condizioneAnnulli _
                & condizioneTotale _
                & " INITCAP(ACCERTATO) as accertato " _
                & " FROM SISCOM_MI.BOL_BOLLETTE_VOCI_EMISSIONI " _
                & " WHERE " _
                & condizioneAggiornamento _
                & condizioneEmissioneDal _
                & condizioneEmissioneAl _
                & condizioneRiferimentoDal _
                & condizioneRiferimentoAl _
                & condizioneTipologiaBollettazione _
                & condizioneListaVoci _
                & condizioneCategoria _
                & condizionemacroCategoria _
                & condizioneCapitoli _
                & condizionetipologiaUI _
                & condizioneCompetenza _
                & condizioneTipologiaCondominio _
                & condizioneAccertato _
                & condizioneVociDaAccertare _
                & condizioneEsercizioContabile

            queryBolBolletteVoci = "SELECT FL_ACCERTATO_BOL_BOLLETTE_VOCI,ACCERTATO,DATA_ACCERTAMENTO,data_rimozione_accertamento " _
                & " FROM SISCOM_MI.BOL_BOLLETTE_VOCI_EMISSIONI " _
                & " WHERE " _
                & condizioneAggiornamento _
                & condizioneEmissioneDal _
                & condizioneEmissioneAl _
                & condizioneRiferimentoDal _
                & condizioneRiferimentoAl _
                & condizioneTipologiaBollettazione _
                & condizioneListaVoci _
                & condizioneCategoria _
                & condizionemacroCategoria _
                & condizioneCapitoli _
                & condizionetipologiaUI _
                & condizioneCompetenza _
                & condizioneTipologiaCondominio _
                & condizioneAccertato _
                & condizioneVociDaAccertare _
                & condizioneEsercizioContabile


            If Len(par.cmd.CommandText) < 4000 Then
                par.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                & " INIZIO, FINE, ESITO,  " _
                & " Q1,  " _
                & " PARAMETRI, PARZIALE, TOTALE,  " _
                & " ERRORE, NOMEFILE,Q4)  " _
                & " VALUES (" & idReport & ", " _
                & " 12, " _
                & Session.Item("id_operatore") & ", " _
                & Format(Now, "yyyyMMddHHmmss") & " , " _
                & "NULL, " _
                & "0, " _
                & "'" & par.cmd.CommandText.ToString.Replace("'", "''") & "', " _
                & "NULL, " _
                & "NULL, " _
                & "NULL, " _
                & "NULL, " _
                & "NULL,NULL)"
            Else
                par.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                & " INIZIO, FINE, ESITO,  " _
                & " Q1,  " _
                & " PARAMETRI, PARZIALE, TOTALE,  " _
                & " ERRORE, NOMEFILE,Q4)  " _
                & " VALUES (" & idReport & ", " _
                & " 12, " _
                & Session.Item("id_operatore") & ", " _
                & Format(Now, "yyyyMMddHHmmss") & " , " _
                & "NULL, " _
                & "0, " _
                & "'', " _
                & "NULL, " _
                & "NULL, " _
                & "NULL, " _
                & "NULL, " _
                & "NULL,:TEXT_DATA)"
                Dim paramData As New Oracle.DataAccess.Client.OracleParameter
                With paramData
                    .Direction = Data.ParameterDirection.Input
                    .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Clob
                    .ParameterName = "TEXT_DATA"
                    .Value = par.cmd.CommandText
                End With

                par.cmd.Parameters.Add(paramData)
                paramData = Nothing
            End If

            par.cmd.ExecuteNonQuery()
            chiudiConnessione()
            Dim p As New System.Diagnostics.Process
            Dim elParameter As String() = System.Configuration.ConfigurationManager.AppSettings("ConnectionString").ToString.Split(";")
            Dim dicParaConnection As New Generic.Dictionary(Of String, String)
            Dim sParametri As String = ""
            For i As Integer = 0 To elParameter.Length - 1
                Dim s As String() = elParameter(i).Split("=")
                If s.Length > 1 Then
                    dicParaConnection.Add(s(0), s(1))
                End If
            Next
            sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idReport
            p.StartInfo.UseShellExecute = False
            p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/Report.exe")
            p.StartInfo.Arguments = sParametri
            p.Start()
            filtriRicerca = Session.Item("filtriRicerca")
            Session.Remove("filtriRicerca")
            'Response.Redirect("VisualizzaEstrazioni_RU.aspx?TIPO=RPT_EM", True)
            Response.Write("<script>location.href='../../Contratti/VisualizzaEstrazioni_RU.aspx?TIPO=RPT_EM';</script>")
        Catch ex As Exception
            chiudiConnessione()
            Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati! Ripetere la ricerca!');self.close();</script>")
        End Try
    End Sub

    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub



    Protected Sub ImageButtonExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonExcel.Click
        'Dim nomeFile As String = EsportaExcelAutomaticoDaDataGrid_11(DataGridEmissioni, "ExportEmissioni", , , , False)
        'If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
        '    Response.Redirect("../../FileTemp/" & nomeFile, False)
        'Else
        '    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        'End If
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportEmissioni", "ExportEmissioni", DataGridEmissioni, True, , )
        If IO.File.Exists(Server.MapPath("..\/..\/FileTemp\/") & nomeFile) Then
            Response.Redirect("../../FileTemp/" & nomeFile, False)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
        End If
    End Sub

    Function EsportaExcelAutomaticoDaDataGrid_11(ByVal datagrid As DataGrid, Optional ByVal nomeFile As String = "", Optional ByVal FattoreLarghezzaColonne As Decimal = 4.75, Optional ByVal EliminazioneLink As Boolean = True, Optional ByVal Titolo As String = "", Optional ByVal creazip As Boolean = True) As String
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
            Dim LarghezzaDataGrid As Integer = Math.Max(datagrid.Width.Value, 200)
            Dim TipoLarghezzaDataGrid As UnitType = datagrid.Width.Type
            Dim LarghezzaColonnaHeader As Decimal = 0
            Dim LarghezzaColonnaItem As Decimal = 0
            'SETTO A ZERO LA VARIABILE DELLE RIGHE
            indiceRighe = 0
            With FileExcel
                'CREO IL FILE 
                .CreateFile(Server.MapPath("~\FileTemp\" & FileName & ".xls"))
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
                If Titolo <> "" Then
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, Titolo, 0)
                    indiceRighe += 1
                    IndiceColonne += 1
                End If
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
                        LarghezzaMinimaColonna = FattoreLarghezzaColonne * Math.Max(LarghezzaColonnaHeader, LarghezzaColonnaItem)
                        .SetColumnWidth(indiceVisibile, indiceVisibile, Math.Max(LarghezzaMinimaColonna, 30))
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
                            Select Case EliminazioneLink
                                Case False
                                    Select Case allineamentoCella
                                        Case 1
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", ""), 0)
                                        Case 2
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", ""), 0)
                                        Case 3
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", ""), 4)
                                        Case Else
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", ""), 0)
                                    End Select

                                Case True
                                    Select Case allineamentoCella
                                        Case 1
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                        Case 2
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                        Case 3
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 4)
                                        Case Else
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                    End Select
                                Case Else
                                    Select Case allineamentoCella
                                        Case 1
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                        Case 2
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                        Case 3
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 4)
                                        Case Else
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                    End Select
                            End Select
                            Cella = Cella + 1
                        End If
                    Next

                Next
                'CHIUSURA FILE
                .CloseFile()
            End With
            If creazip = True Then
                'COSTRUZIONE ZIPFILE
                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream

                Dim strFile As String
                strFile = Server.MapPath("~\FileTemp\" & FileName & ".xls")
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
                zipfic = Server.MapPath("~\FileTemp\" & FileName & ".zip")
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
                File.Delete(strFile)
                Dim FileNameZip As String = FileName & ".zip"
                Return FileNameZip
            Else
                Dim FileNameExcel As String = FileName & ".xls"
                Return FileNameExcel
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Function StampaDataGridPDF_1(ByVal datagrid As DataGrid, ByVal nomeStampa As String, Optional ByVal titolo As String = "", Optional ByVal footer As String = "", Optional ByVal larghezzaPagina As Integer = 1200, Optional ByVal orientamentoLandscape As Boolean = True, Optional ByVal mostraNumeriPagina As Boolean = True, Optional ByVal contaRighe As Boolean = False, Optional righe As Integer = 25, Optional ByVal ripetiIntestazioniSoloConContaRighe As Boolean = False, Optional ByVal sottotitolo As String = "") As String
        Try
            'RENDERCONTROL DEL DATAGRID
            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)
            stringWriter = New System.IO.StringWriter
            sourcecode = New HtmlTextWriter(stringWriter)
            datagrid.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = Html & stringWriter.ToString
            'ELIMINAZIONE EVENTUALI HYPERLINK
            Html = par.EliminaLink(Html)
            If contaRighe = True And righe > 0 Then
                Dim TitoliDaRipetere As String = ""
                If ripetiIntestazioniSoloConContaRighe = True Then
                    Dim indiceInizioPrimoTR As Integer = Html.IndexOf("</tr>")
                    TitoliDaRipetere = Left(Html, indiceInizioPrimoTR + 5)
                End If


                Dim htmldaConsiderare As String = Html
                Dim nuovoHtml As String = ""
                Dim indiceTRiniziale As Integer = 0
                Dim indiceTRfinale As Integer = 0
                Dim contatoreRighe As Integer = 0
                Dim stringaAdd As String = ""
                While indiceTRiniziale <> -1
                    indiceTRiniziale = htmldaConsiderare.IndexOf("<tr ")
                    If indiceTRiniziale <> -1 Then
                        contatoreRighe += 1
                        htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRiniziale)
                        indiceTRfinale = htmldaConsiderare.IndexOf("</tr>") + 5
                        If indiceTRfinale <> -1 Then
                            stringaAdd = Left(htmldaConsiderare, indiceTRfinale)
                            htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRfinale)
                        End If
                    End If
                    If contatoreRighe = righe Then
                        nuovoHtml &= stringaAdd & "</table><p style='page-break-after: always'>&nbsp;</p><table>" & TitoliDaRipetere & Left(Html, Html.IndexOf("<tr ") - 1)
                        contatoreRighe = 0
                    Else
                        nuovoHtml &= stringaAdd
                    End If
                End While
                Html = Left(Html, Html.IndexOf("<tr ") - 1) & nuovoHtml
            End If

            Dim url As String = Server.MapPath("~\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Dim Licenza As String = System.Web.HttpContext.Current.Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If
            pdfConverter1.PageWidth = larghezzaPagina
            If orientamentoLandscape = True Then
                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            Else
                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            End If
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 63
            pdfConverter1.PdfHeaderOptions.HeaderText = UCase(titolo)
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 7
            pdfConverter1.PdfHeaderOptions.HeaderTextAlign = HorizontalTextAlign.Left
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold


            pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = sottotitolo
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 7
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.ColorTranslator.FromHtml("#507CD1")


            'pdfConverter1.PdfHeaderOptions.HeaderImage = Drawing.Image.FromFile(Server.MapPath("~\NuoveImm\") & "rett2.png")


            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.ColorTranslator.FromHtml("#507CD1")
            pdfConverter1.PdfFooterOptions.FooterText = "Report Situazione Emissioni, stampato da " & Session.Item("NOME_OPERATORE") & " il " & Format(Now, "dd/MM/yyyy") & " alle " & Format(Now, "HH:mm")
            pdfConverter1.PdfFooterOptions.FooterTextFontName = "Arial"
            pdfConverter1.PdfFooterOptions.FooterTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfFooterOptions.FooterTextFontSize = 8
            'pdfConverter1.PdfFooterOptions.FooterText = UCase(footer)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.ColorTranslator.FromHtml("#507CD1")
            pdfConverter1.PdfFooterOptions.FooterHeight = 30
            pdfConverter1.PdfFooterOptions.DrawFooterLine = True
            If mostraNumeriPagina = True Then
                pdfConverter1.PdfFooterOptions.PageNumberText = "Pag."
                pdfConverter1.PdfFooterOptions.PageNumberTextFontName = "Arial"
                pdfConverter1.PdfFooterOptions.PageNumberTextFontSize = 8
                pdfConverter1.PdfFooterOptions.ShowPageNumber = True
                pdfConverter1.PdfFooterOptions.PageNumberTextColor = Drawing.ColorTranslator.FromHtml("#507CD1")
            Else
                pdfConverter1.PdfFooterOptions.PageNumberText = ""
                pdfConverter1.PdfFooterOptions.ShowPageNumber = False
            End If

            Dim nomefile As String = nomeStampa & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile, Server.MapPath("~\NuoveImm\"))

            Return nomefile
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class

