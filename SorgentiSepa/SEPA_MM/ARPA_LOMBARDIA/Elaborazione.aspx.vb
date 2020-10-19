Imports Telerik.Web.UI
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO
Imports System.Xml

Partial Class ARPA_LOMBARDIA_Elaborazione
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing
    Dim XMLError As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFIdElaborazione.Value = Request.QueryString("ID").ToString
            HFGriglia.Value = RadGridElaborazione.ClientID.ToString.Replace("ctl00", "MasterHomePage")
            CaricaTitolo()
        End If
    End Sub
    Private Sub CaricaTitolo()
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT ANNO FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_ELABORAZIONI WHERE ID = " & HFIdElaborazione.Value
            lblTitolo.Text = "Elaborazione Anno " & par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT NVL(FL_VALIDAZIONE, 0) FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_ELABORAZIONI WHERE ID = " & HFIdElaborazione.Value
            Dim FlValidazione As Integer = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, 0), 0)
            If FlValidazione = 0 Then lblElabValidazione.Visible = True
            connData.chiudi(False)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Elaborazione - CaricaTitolo - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub RadGridElaborazione_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridElaborazione.NeedDataSource
        Try
            Dim Query As String = "SELECT CD_FABBRICATO, CF_ENTE_PROPRIETARIO, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_TIPO_PROPRIETA_FABBRICATO WHERE ID = TIPO_PROPRIETA) AS TIPO_PROPRIETA, " _
                                & "NUM_ALLOGGI_ALTRA_PROPRIETA, ANNO_COSTRUZIONE, CD_CATASTALE_COMUNE, CD_ISTAT_COMUNE, " _
                                & "ID_ENTE_PROPRIETARIO, ID_EDIFICIO " _
                                & "FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_ELABORAZIONI_FABBRICATI " _
                                & "WHERE ID_ELABORAZIONE = " & HFIdElaborazione.Value
            RadGridElaborazione.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Elaborazione - RadGridElaborazione_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub RadGridElaborazione_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadGridElaborazione.DetailTableDataBind
        Try
            Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
            Select Case e.DetailTableView.Name
                Case "UnitaImmobiliari"
                    par.cmd.CommandText = "SELECT ID_ENTE_PROPRIETARIO, TIPO_ENTE_PROPRIETARIO, CD_ALLOGGIO, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_TIPOLOGIA_GESTORE WHERE ID = TIPOLOGIA_GESTORE) AS TIPOLOGIA_GESTORE, " _
                                        & "ID_ENTE_GESTORE, CF_ENTE_GESTORE, RAG_SOCIALE_ENTE_GESTORE, CD_CATASTALE_ENTE_GESTORE, " _
                                        & "DENOMINAZIONE_ENTE_GESTORE, FOGLIO, PARTICELLA, " _
                                        & "SUBALTERNO, CATEGORIA, CLASSE, CONSISTENZA, RENDITA, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_PREFISSO_INDIRIZZO WHERE ID = PREFISSO_INDIRIZZO) AS PREFISSO_INDIRIZZO, " _
                                        & "VIA_PIAZZA, NUMERO_CIVICO, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_TIPOLOGIA_PIANO WHERE ID = PIANO) AS PIANO, " _
                                        & "SCALA, CAP, LOCALITA, SUPERFICIE_UTILE, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_BARRIERE_ARCHITETTONICHE) AS FL_BARRIERE_ARCHITETTONICHE, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_ASCENSORE) AS FL_ASCENSORE, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_CANTINA_SOLAIO) AS FL_CANTINA_SOLAIO, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_BOX_POSTO_AUTO) AS FL_BOX_POSTO_AUTO, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_DESTINAZIONI_USO WHERE ID = DESTINAZIONE_USO) AS DESTINAZIONE_USO, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_ALLOGGIO_ESCLUSO) AS FL_ALLOGGIO_ESCLUSO, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_STATO_UNITA WHERE ID = STATO_UNITA) AS STATO_UNITA, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_ACCORPATO) AS FL_ACCORPATO, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_FRAZIONATO) AS FL_FRAZIONATO, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_DEMOLITA) AS FL_DEMOLITA, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_VENDUTO) AS FL_VENDUTO, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_PIANO_VENDITA) AS FL_PIANO_VENDITA, " _
                                        & "NUM_AUTORIZZAZIONE_REG_VEND, DATA_AUTORIZZAZIONE_REG_VEND, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_PIANO_VALORIZZAZIONE) AS FL_PIANO_VALORIZZAZIONE, " _
                                        & "NUM_AUTORIZZAZIONE_REG_VAL, DATA_AUTORIZZAZIONE_REG_VAL, " _
                                        & "ID_EDIFICIO, ID_UNITA " _
                                        & "FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_ELABORAZIONI_UNITA " _
                                        & "WHERE ARPA_ELABORAZIONI_UNITA.ID_EDIFICIO = " & dataItem("ID_EDIFICIO").Text.ToString & " " _
                                        & "AND ID_ELABORAZIONE = " & HFIdElaborazione.Value
                    e.DetailTableView.DataSource = par.getDataTableGrid(par.cmd.CommandText)
                Case "Nuclei"
                    par.cmd.CommandText = "SELECT (SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_TIPO_SOGGETTO_OCCUPANTE WHERE ID = TIPO_SOGGETTO_OCCUPANTE) AS TIPO_SOGGETTO_OCCUPANTE, " _
                                        & "CODICE_FISCALE, RAGIONE_SOCIALE, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_AREA_ISEE WHERE ID = AREA_ISEE) AS AREA_ISEE, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_FASCIA_ISEE WHERE ID = FASCIA_ISEE) AS FASCIA_ISEE, " _
                                        & "ISEE_ERP, ISEE, ISR, ISP, PSE, GETDATA(DATA_STIPULA_CONTRATTO) AS DATA_STIPULA_CONTRATTO, " _
                                        & "TRIM(TO_CHAR(CANONE_APPLICATO, '99999990D99')) AS CANONE_APPLICATO, " _
                                        & "ID_EDIFICIO, ID_UNITA, ID_CONTRATTO " _
                                        & "FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_ELABORAZIONI_NUCLEI " _
                                        & "WHERE ARPA_ELABORAZIONI_NUCLEI.ID_UNITA = " & dataItem("ID_UNITA").Text.ToString & " " _
                                        & "AND ID_ELABORAZIONE = " & HFIdElaborazione.Value
                    e.DetailTableView.DataSource = par.getDataTableGrid(par.cmd.CommandText)
                Case "Inquilini"
                    par.cmd.CommandText = "SELECT CODICE_INQUILINO, CODICE_FISCALE, NOME, COGNOME, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SESSO WHERE ID = SESSO) AS SESSO, " _
                                        & "GETDATA(DATA_NASCITA) AS DATA_NASCITA, CITTADINANZA, NAZIONE_NASCITA, COMUNE_NASCITA, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = INTESTATARIO_CONTRATTO) AS INTESTATARIO_CONTRATTO, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_PARENTELA WHERE ID = RAPPORTO_PARENTELA) AS RAPPORTO_PARENTELA, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_COND_LAVORO WHERE ID = CONDIZIONE_LAVORO) AS CONDIZIONE_LAVORO, " _
                                        & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_NUCLEO_FAMILIARE WHERE ID = NUCLEO_FAMILIARE) AS NUCLEO_FAMILIARE, ID_CONTRATTO " _
                                        & "FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_ELABORAZIONI_INQUILINI " _
                                        & "WHERE ARPA_ELABORAZIONI_INQUILINI.ID_CONTRATTO = " & dataItem("ID_CONTRATTO").Text.ToString & " " _
                                        & "AND ID_ELABORAZIONE = " & HFIdElaborazione.Value
                    e.DetailTableView.DataSource = par.getDataTableGrid(par.cmd.CommandText)
            End Select
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Elaborazione - RadGridElaborazione_DetailTableDataBind - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Try
            par.cmd.CommandText = "SELECT CD_FABBRICATO, CF_ENTE_PROPRIETARIO, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_TIPO_PROPRIETA_FABBRICATO WHERE ID = TIPO_PROPRIETA) AS TIPO_PROPRIETA, " _
                                & "NUM_ALLOGGI_ALTRA_PROPRIETA, ANNO_COSTRUZIONE, CD_CATASTALE_COMUNE, CD_ISTAT_COMUNE, " _
                                & "ARPA_ELABORAZIONI_FABBRICATI.ID_ENTE_PROPRIETARIO, " _
                                & "ARPA_ELABORAZIONI_UNITA.ID_ENTE_PROPRIETARIO, TIPO_ENTE_PROPRIETARIO, CD_ALLOGGIO, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_TIPOLOGIA_GESTORE WHERE ID = TIPOLOGIA_GESTORE) AS TIPOLOGIA_GESTORE, " _
                                & "ID_ENTE_GESTORE, CF_ENTE_GESTORE, RAG_SOCIALE_ENTE_GESTORE, CD_CATASTALE_ENTE_GESTORE, " _
                                & "DENOMINAZIONE_ENTE_GESTORE, FOGLIO, PARTICELLA, " _
                                & "SUBALTERNO, CATEGORIA, CLASSE, CONSISTENZA, RENDITA, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_PREFISSO_INDIRIZZO WHERE ID = PREFISSO_INDIRIZZO) AS PREFISSO_INDIRIZZO, " _
                                & "VIA_PIAZZA, NUMERO_CIVICO, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_TIPOLOGIA_PIANO WHERE ID = PIANO) AS PIANO, " _
                                & "SCALA, CAP, LOCALITA, SUPERFICIE_UTILE, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_BARRIERE_ARCHITETTONICHE) AS FL_BARRIERE_ARCHITETTONICHE, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_ASCENSORE) AS FL_ASCENSORE, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_CANTINA_SOLAIO) AS FL_CANTINA_SOLAIO, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_BOX_POSTO_AUTO) AS FL_BOX_POSTO_AUTO, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_DESTINAZIONI_USO WHERE ID = DESTINAZIONE_USO) AS DESTINAZIONE_USO, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_ALLOGGIO_ESCLUSO) AS FL_ALLOGGIO_ESCLUSO, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_STATO_UNITA WHERE ID = STATO_UNITA) AS STATO_UNITA, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_ACCORPATO) AS FL_ACCORPATO, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_FRAZIONATO) AS FL_FRAZIONATO, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_DEMOLITA) AS FL_DEMOLITA, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_VENDUTO) AS FL_VENDUTO, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_PIANO_VENDITA) AS FL_PIANO_VENDITA, " _
                                & "NUM_AUTORIZZAZIONE_REG_VEND, DATA_AUTORIZZAZIONE_REG_VEND, " _
                                & "(SELECT VALORE || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_SINO WHERE ID = FL_PIANO_VALORIZZAZIONE) AS FL_PIANO_VALORIZZAZIONE, " _
                                & "NUM_AUTORIZZAZIONE_REG_VAL, DATA_AUTORIZZAZIONE_REG_VAL " _
                                & "FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_ELABORAZIONI_FABBRICATI, " & CType(Me.Master, Object).StringaSiscom & "ARPA_ELABORAZIONI_UNITA " _
                                & "WHERE ARPA_ELABORAZIONI_FABBRICATI.ID_ELABORAZIONE = " & HFIdElaborazione.Value & " " _
                                & "AND ARPA_ELABORAZIONI_FABBRICATI.ID_EDIFICIO = ARPA_ELABORAZIONI_UNITA.ID_EDIFICIO " _
                                & "ORDER BY CD_FABBRICATO, CD_ALLOGGIO ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportElaborazione", "Elaborazione", dt)
                If System.IO.File.Exists(Server.MapPath("../FileTemp/" & nomeFile)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    RadNotificationNote.Text = "Errore durante l'Export. Riprovare!!"
                    RadNotificationNote.Show()
                End If
            Else
                RadNotificationNote.Text = par.Messaggio_NoExport
                RadNotificationNote.Show()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Elaborazione - btnExport_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub btnFileXml_Click(sender As Object, e As System.EventArgs) Handles btnFileXml.Click
        Try
            connData.apri(False)
            Dim FileXmlL As Long = 0
            Dim TestoXml As String = ""
            par.cmd.CommandText = "SELECT FILE_XML, LENGTH(FILE_XML) AS FILE_XML_L " _
                                & "FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_ELABORAZIONI " _
                                & "WHERE ID = " & HFIdElaborazione.Value.ToString
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                FileXmlL = par.IfNull(MyReader("FILE_XML_L"), 0)
                If FileXmlL > 0 Then
                    Dim ind As Integer = MyReader.GetOrdinal("FILE_XML")
                    Dim buf() As Byte = CType(MyReader.GetValue(ind), Byte())
                    TestoXml = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(buf)
                End If
            End If
            MyReader.Close()
            connData.chiudi(False)
            If FileXmlL = 0 Then
                'CreaFileXML()
                connData.apri(False)
                par.cmd.CommandText = "SELECT COUNT(*) FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_PROCEDURE WHERE ESITO = 0"
                Dim NrElaborazioni As Integer = par.cmd.ExecuteScalar
                If NrElaborazioni > 0 Then
                    connData.chiudi(False)
                    RadNotificationNote.Text = "E' stata già avviata una Procedura! Attendere il termine."
                    RadNotificationNote.Show()
                    Exit Sub
                End If
                par.cmd.CommandText = "SELECT " & CType(Me.Master, Object).StringaSiscom & "SEQ_ARPA_PROCEDURE.NEXTVAL FROM DUAL"
                Dim idProcedura As String = par.cmd.ExecuteScalar
                par.cmd.CommandText = "INSERT INTO " & CType(Me.Master, Object).StringaSiscom & "ARPA_PROCEDURE (ID, ID_OPERATORE, DATA_ORA_INIZIO, DATA_ORA_FINE, ESITO, ERRORE, " _
                                    & "PARZIALE, TOTALE, PARAMETRI, ID_TIPO) VALUES " _
                                    & "(" & idProcedura & ", " & Session.Item("ID_OPERATORE").ToString & ", '" & Format(Now, "yyyyMMddHHmmss") & "', '', 0, '', " _
                                    & "0, 100, " & HFIdElaborazione.Value & ", 2)"
                par.cmd.ExecuteNonQuery()
                connData.chiudi(False)
                Try
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
                    sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idProcedura
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/ARPA_Lombardia.exe")
                    p.StartInfo.Arguments = sParametri
                    p.Start()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "msg", "alert('Creazione File XML avviata correttamente!');", True)
                Catch ex As Exception
                    par.cmd.CommandText = "UPDATE " & CType(Me.Master, Object).StringaSiscom & "ARPA_PROCEDURE SET ESITO = 2, DATA_ORA_FINE = '" & Format(Now, "yyyyMMddHHmmss") & "', ERRORE = 'Procedura non Avviata' WHERE ID = " & HFIdElaborazione.Value
                    par.cmd.ExecuteNonQuery()
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante l\'avvio della procedura di creazione del File XML!');", True)
                End Try
            Else
                Dim nomeFile As String = "Elaborazione_" & Format(Now, "yyyyMMddHHmmss") & ".zip"
                Dim sw As StreamWriter = New StreamWriter(Server.MapPath("~/FileTemp/") & nomeFile, False, System.Text.Encoding.GetEncoding("ISO-8859-1"))
                sw.WriteLine(TestoXml)
                sw.Close()
                If Not String.IsNullOrEmpty(Trim(Server.MapPath("~/FileTemp/") & nomeFile)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');};", True)
                Else
                    CType(Me.Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Errore nel download del File!", 450, 150, "Attenzione", Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Elaborazione - btnFileXml_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub btnVisualizzaAnomalie_Click(sender As Object, e As System.EventArgs) Handles btnVisualizzaAnomalie.Click
        Try
            connData.apri(False)
            Dim FileXmlL As Long = 0
            Dim AnomalieXmlL As Long = 0
            Dim AnomalieXmlTesto As String = ""
            par.cmd.CommandText = "SELECT NVL(FL_VALIDAZIONE, 0) FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_ELABORAZIONI WHERE ID = " & HFIdElaborazione.Value
            Dim FlValidazione As Integer = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, 0), 0)
            par.cmd.CommandText = "SELECT LENGTH(FILE_XML) AS FILE_XML_L, " _
                                & "ANOMALIE_XML, LENGTH(ANOMALIE_XML) AS ANOMALIE_XML_L " _
                                & "FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_ELABORAZIONI " _
                                & "WHERE ID = " & HFIdElaborazione.Value.ToString
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                FileXmlL = par.IfNull(MyReader("FILE_XML_L"), 0)
                AnomalieXmlL = par.IfNull(MyReader("ANOMALIE_XML_L"), 0)
                If AnomalieXmlL > 0 Then
                    Dim blob As Oracle.DataAccess.Types.OracleClob = MyReader.GetOracleClob(1)
                    AnomalieXmlTesto = blob.Value()
                    blob.Close()
                End If
            End If
            MyReader.Close()
            connData.chiudi(False)
            If FileXmlL = 0 Then
                RadNotificationNote.Text = "Non è stato creato ancora nessun file Xml!"
                RadNotificationNote.Show()
            Else
                If AnomalieXmlL = 0 Then
                    If FlValidazione = 1 Then
                        RadNotificationNote.Text = "Non sono presenti anomalie nel file Xml!"
                    Else
                        RadNotificationNote.Text = "L'elaborazione è stata generata senza validazione!"
                    End If
                    RadNotificationNote.Show()
                Else
                    txtAnomalieElaborazioni.Text = AnomalieXmlTesto
                    Dim script As String = "function f(){$find(""" + RadWindowElaborazioneAnomalie.ClientID & """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "key", script, True)
                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Elaborazione - btnVisualizzaAnomalie_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
