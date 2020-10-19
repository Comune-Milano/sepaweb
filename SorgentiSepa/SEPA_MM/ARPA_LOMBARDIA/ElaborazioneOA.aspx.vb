
Imports Telerik.Web.UI
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO
Imports System.Xml

Partial Class ARPA_LOMBARDIA_ElaborazioneOA
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
            par.cmd.CommandText = "SELECT ANNO FROM " & CType(Me.Master, Object).StringaSiscom & "OA_ELABORAZIONI WHERE ID = " & HFIdElaborazione.Value
            lblTitolo.Text = "Elaborazione Anno " & par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT NVL(FL_VALIDAZIONE, 0) FROM " & CType(Me.Master, Object).StringaSiscom & "OA_ELABORAZIONI WHERE ID = " & HFIdElaborazione.Value
            Dim FlValidazione As Integer = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, 0), 0)
            If FlValidazione = 0 Then lblElabValidazione.Visible = True
            ControlloValidazioneFile()
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
            Dim Query As String = EsportaQuery()
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            RadGridElaborazione.DataSource = dt
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Elaborazione - RadGridElaborazione_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Try
            Dim dt As Data.DataTable = par.getDataTableGrid(EsportaQuery())
            If dt.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportElaborazioneOA", "ElaborazioneOA", dt)
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
                                & "FROM " & CType(Me.Master, Object).StringaSiscom & "OA_ELABORAZIONI " _
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
                                    & "0, 100, " & HFIdElaborazione.Value & ", 4)"
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
            ControlloValidazioneFile()
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
            par.cmd.CommandText = "SELECT NVL(FL_VALIDAZIONE, 0) FROM " & CType(Me.Master, Object).StringaSiscom & "OA_ELABORAZIONI WHERE ID = " & HFIdElaborazione.Value
            Dim FlValidazione As Integer = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, 0), 0)
            par.cmd.CommandText = "SELECT LENGTH(FILE_XML) AS FILE_XML_L, " _
                                & "ANOMALIE_XML, LENGTH(ANOMALIE_XML) AS ANOMALIE_XML_L " _
                                & "FROM " & CType(Me.Master, Object).StringaSiscom & "OA_ELABORAZIONI " _
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
    Private Function EsportaQuery() As String
        EsportaQuery = "SELECT CF_ENTE_PROPRIETARIO,  " _
                    & "(CASE WHEN AZIONE = 'I' THEN 'I - Inserimento'  " _
                    & "WHEN AZIONE = 'M' THEN 'M - Modifica' else '' END) AS AZIONE, " _
                    & "CD_OCCUPAZIONE, " _
                    & "(CASE WHEN FLG_CF_CONOSCIUTO = 1 THEN '1 - Sì' ELSE '0 - No' END) AS FLG_CF_CONOSCIUTO, " _
                    & "CF, " _
                    & "COGNOME, " _
                    & "NOME, " _
                    & "TO_DATE (DT_NASCITA, 'YYYYMMDD') AS DT_NASCITA, " _
                    & "FLG_SESSO, " _
                    & "CD_CATASTALE_NAZ_NASCITA, " _
                    & "SIGLA_PROVINCIA_NASCITA, " _
                    & "CD_CATASTALE_COM_NASCITA, " _
                    & "CD_CITTADINANZA, " _
                    & "CD_ALLOGGIO, " _
                    & "SIGLA_PROVINCIA_ALLOGGIO, " _
                    & "CD_CATASTALE_ALLOGGIO, " _
                    & "PREFISSO_INDIRIZZO, " _
                    & "DENOMINAZIONE_INDIRIZZO, " _
                    & "NUM_CIVICO, " _
                    & "TO_DATE (DT_INIZIO_OCCUPAZIONE, 'YYYYMMDD') AS DT_INIZIO_OCCUPAZIONE, " _
                    & "ID_TIPO_OCCUPAZIONE, " _
                    & "ID_TIPO_ATTO_RILEVAZIONE, " _
                    & "IDENTIFICATIVO_ATTO_RIL, " _
                    & "TO_DATE (DT_ATTO_RILEVAZIONE, 'YYYYMMDD') AS DT_ATTO_RILEVAZIONE, " _
                    & "ID_TIPO_ATTO_LEGITTIMANTE, " _
                    & "IDENTIFICATIVO_ATTO_LEGIT, " _
                    & "TO_DATE (DT_ATTO_LEGIT, 'YYYYMMDD') AS DT_ATTO_LEGIT, " _
                    & "IDENTIFICATIVO_ATTO_RILASCIO, " _
                    & "PROTOCOLLO_ATTO_RILASCIO, " _
                    & "TO_DATE (DT_ATTO_RILASCIO, 'YYYYMMDD') AS DT_ATTO_RILASCIO, " _
                    & "(SELECT COD || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "TAB_DEBITO_OA WHERE TAB_DEBITO_OA.COD = ID_TIPO_PRESENZA_DEBITO) ID_TIPO_PRESENZA_DEBITO, " _
                    & "TIPO_DEBITO, " _
                    & "(CASE WHEN FLGTIPODEBITODANNEGGIAMENTO = 1 THEN (SELECT COD || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "TAB_TIPO_DEBITO_OA WHERE ID = 1) ELSE '' END) AS FLGTIPODEBITODANNEGGIAMENTO, " _
                    & "(CASE WHEN FLGTIPODEBITOMANCATO = 1 THEN (SELECT COD || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "TAB_TIPO_DEBITO_OA WHERE ID = 2) ELSE '' END) AS FLGTIPODEBITOMANCATO, " _
                    & "(CASE WHEN FLGTIPODEBITOCOSTI = 1 THEN (SELECT COD || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "TAB_TIPO_DEBITO_OA WHERE ID = 3) ELSE '' END) AS FLGTIPODEBITOCOSTI, " _
                    & "(CASE WHEN FLGTIPODEBITOALTRO = 1 THEN (SELECT COD || ' - ' || DESCRIZIONE FROM " & CType(Me.Master, Object).StringaSiscom & "TAB_TIPO_DEBITO_OA WHERE ID = 4) ELSE '' END) AS FLGTIPODEBITOALTRO, " _
                    & "(CASE WHEN FLG_ESTINZIONE_DEBITO = 1 THEN '1 - Sì' ELSE '0 - No' END) AS FLG_ESTINZIONE_DEBITO, " _
                    & "TO_DATE (DT_ESTINZIONE_DEBITO, 'YYYYMMDD') AS DT_ESTINZIONE_DEBITO, " _
                    & "(CASE WHEN FLG_CESSAZIONE_OCCU = 1 THEN '1 - Sì' ELSE '0 - No' END) AS FLG_CESSAZIONE_OCCU, " _
                    & "TO_DATE (DT_CESSAZIONE_OCCUPAZIONE, 'YYYYMMDD') AS DT_CESSAZIONE_OCCUPAZIONE, " _
                    & "IDENT_PROVV_CESSAZ, " _
                    & "PROT_PROVV_CESSAZ, " _
                    & "TO_DATE (DT_PROVV_CESSAZ, 'YYYYMMDD') AS DT_PROVV_CESSAZ " _
                    & "FROM " & CType(Me.Master, Object).StringaSiscom & "OA_ELABORAZIONI_OCCUPANTI " _
                    & "WHERE ID_ELABORAZIONE =  " & HFIdElaborazione.Value
    End Function

    Private Sub ControlloValidazioneFile()
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT NVL(LENGTH(OA_ELABORAZIONI.ANOMALIE_XML),0) FROM " & CType(Me.Master, Object).StringaSiscom & "OA_ELABORAZIONI WHERE ID = " & HFIdElaborazione.Value
            Dim FlValidazioneOk As Integer = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, 0), 0)
            If FlValidazioneOk = 0 Then
                lblValidazioneFile.Text = "Il file ha superato la validazione."
                lblValidazioneFile.ForeColor = Drawing.Color.Green
            Else
                lblValidazioneFile.Text = "Il file non ha superato la validazione. Premere il tasto ""Visualizza anomalie"" per i dettagli"
                lblValidazioneFile.ForeColor = Drawing.Color.Red
            End If
            connData.chiudi(False)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Elaborazione - btnVisualizzaAnomalie_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
