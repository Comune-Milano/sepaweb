Imports System.IO
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip

Partial Class SIRAPER_CreaFileXml
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim EsitoPositivo As Boolean = True
    Dim XMLError As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        idConnessione.Value = Request.QueryString("IdConnessione")
        sescon.Value = Request.QueryString("SESCON")
        If IsNothing(HttpContext.Current.Session.Item(sescon.Value & idConnessione.Value)) Then
            Me.connData = New CM.datiConnessione(par, False, False)
        Else
            Me.connData = CType(HttpContext.Current.Session.Item(sescon.Value & idConnessione.Value), CM.datiConnessione)
            par.cmd = par.OracleConn.CreateCommand()
        End If
        If Not IsPostBack Then
            idSiraper.Value = Request.QueryString("ID")
            idSiraperVersione.Value = Request.QueryString("IDV")
            ControlloDate()
        End If
    End Sub
    Private Sub ControlloDate()
        Try
            If Format(Now, "yyyyMMdd") < par.AggiustaData(Request.QueryString("DR")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType, "msg", "alert('La data di trasmissione non può essere antecedente a quella di riferimento!');", True)
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "self.close();", True)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: CreaFileXml - ControlloDate - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
#Region "CreazioneFileXml"
    Protected Sub btnCreaFile_Click(sender As Object, e As System.EventArgs) Handles btnCreaFile.Click
        Try
            Select Case idSiraperVersione.Value.ToString
                Case "1"
                    CreaFileXml_2013()
                Case "2"
                    CreaFileXml_2015()
            End Select
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: CreaFileXml - btnCreaFile_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
#Region "2013"
    Private Sub CreaFileXml_2013()
        Try
            Dim TestoXml As String = ""
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            CodIstatDefault.Value = "015146"
            If EsitoPositivo Then RichiestaType_2013(TestoXml, True, True, False) 'SCRIVO LA IL TAG RICHIESTA
            par.cmd.CommandText = "SELECT ID, COD_FABBRICATO, PROPRIETA, GESTIONE, COD_ISTAT_COMUNE, UBICAZIONE, TIPO_COEFF_UBICAZIONE_EDIFICIO.VALORE AS COEFF_UBICAZIONE, ANNO_COSTRUZIONE, ANNO_RISTRUTTURAZIONE, " _
                                & "PREFISSO_INDIRIZZO, NOME_VIA, NUMERO_CIVICO, LOCALITA, CAP, NUM_ALL_RISCATTO, ESPONENTE " _
                                & "FROM TIPO_COEFF_UBICAZIONE_EDIFICIO, SIR_FABBRICATO WHERE TIPO_COEFF_UBICAZIONE_EDIFICIO.COD(+) = COEFF_UBICAZIONE AND ID_SIRAPER = " & idSiraper.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtFabbricati As New Data.DataTable
            da.Fill(dtFabbricati)
            da.Dispose()
            For Each rowFabbricati As Data.DataRow In dtFabbricati.Rows
                If EsitoPositivo Then FabbricatoType_2013(TestoXml, True, True, False, rowFabbricati)
                If EsitoPositivo Then FabbricatoType_2013(TestoXml, False, False, True, rowFabbricati)
            Next
            dtFabbricati.Dispose()
            If EsitoPositivo Then ProgrammazioneType_2013(TestoXml, True, True, True) 'SCRIVO IL TAG PROGRAMMAZIONE
            If EsitoPositivo Then RichiestaType_2013(TestoXml, False, False, True) 'CHIUDO IL TAG RICHIESTA
            If EsitoPositivo Then
                Dim MessaggioErrore As String = ""
                Dim NomeFile As String = ""
                If ScriviFileXml(TestoXml, MessaggioErrore, NomeFile) Then
                    If ValidificazioneXml(NomeFile, MessaggioErrore) Then
                        CreaZipFile("..\ALLEGATI\" & Session.Item("ComuneCollegato") & "\SIRAPER\" & NomeFile)
                        If EsitoPositivo = False Then Exit Sub
                        NomeFile = NomeFile.Replace("xml", "zip")
                        par.cmd.CommandText = "INSERT INTO SIRAPER_FILE (ID, ID_SIRAPER, CARTELLA, NOME_FILE, DATA_ORA, ID_OPERATORE) VALUES " _
                                            & "(SEQ_SIRAPER_FILE.NEXTVAL, " & idSiraper.Value & ", 'SIRAPER', '" & par.PulisciStrSql(NomeFile) & "', " & Format(Now, "yyyyMMddHHmmss") & ", " & Session.Item("ID_OPERATORE") & ")"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "UPDATE SIRAPER SET DATA_TRASMISSIONE = " & Format(Now, "yyyyMMdd") & " WHERE ID = " & idSiraper.Value
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "INSERT INTO SIRAPER_EVENTI (ID_SIRAPER, ID_OPERATORE, DATA_ORA, COD_EVENTO, DESCRIZIONE) VALUES " _
                                            & "(" & idSiraper.Value & ", " & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'S05', " _
                                            & "'CREAZIONE FILE XML DATI ELABORAZIONE SIRAPER')"
                        par.cmd.ExecuteNonQuery()
                        connData.chiudi(True)
                        connData.apri(True)
                        par.cmd.CommandText = "SELECT * FROM SIRAPER WHERE ID = " & idSiraper.Value & " FOR UPDATE NOWAIT"
                        Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        MyReader.Close()
                        Session.Add("FILECREATO", 1)
                        ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType, "msg", "alert('Creazione File Xml Completata! \n Scaricare il File dalla Maschera Principale.');", True)
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "self.close();", True)
                    Else
                        txtNote.Text = MessaggioErrore
                        If File.Exists(Server.MapPath("..\ALLEGATI\SIRAPER\") & NomeFile) Then
                            File.Delete(Server.MapPath("..\ALLEGATI\SIRAPER\") & NomeFile)
                        End If
                        ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType, "msg", "alert('Creazione File Xml Errata!');", True)
                    End If
                Else
                    If String.IsNullOrEmpty(MessaggioErrore) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType, "msg", "alert('Errore durante la scrittura del File Xml!');", True)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType, "msg", "alert('" & MessaggioErrore.Replace("'", "\'") & "');", True)
                    End If
                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: CreaFileXml - CreaFileXml_2013 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub RichiestaType_2013(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean)
        Try
            If ApriTag Then
                TestoXml &= "<?xml version='1.0' encoding='UTF-8'?>" & vbCrLf
                TestoXml &= "<RICHIESTA xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:noNamespaceSchemaLocation='" & Server.MapPath("Siraper2013_1.0.3.xsd") & "'>"
            End If
            If Tag Then
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                    'par.cmd.Transaction = connData.Transazione
                End If
                CreaTag(TestoXml, "DATA_DI_TRASMISSIONE", Format(Now, "dd-MM-yyyy"), 1)
                par.cmd.CommandText = "SELECT SIGLA_ENTE, TIPO_ENTE, COD_FISCALE_ENTE, P_IVA_ENTE, TO_CHAR(TO_DATE(DATA_RIFERIMENTO,'yyyyMMdd'),'dd-MM-yyyy') AS DATA_RIFERIMENTO, ANNO_RIFERIMENTO, RAG_SOCIALE " _
                                    & "FROM SISCOM_MI.SIRAPER WHERE ID = " & idSiraper.Value
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                For Each row As Data.DataRow In dt.Rows
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("SIGLA_ENTE"), "")) Then CreaTag(TestoXml, "SIGLA_ENTE_PROPRIETARIO", par.IfNull(row.Item("SIGLA_ENTE"), ""), 1)
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("TIPO_ENTE"), "")) Then CreaTag(TestoXml, "TIPO_ENTE_PROPRIETARIO", par.IfNull(row.Item("TIPO_ENTE"), ""), 1)
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("COD_FISCALE_ENTE"), "")) Then CreaTag(TestoXml, "ENTE_PROPRIETARIO_CODICE_FISCALE", par.IfNull(row.Item("COD_FISCALE_ENTE"), ""), 1)
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("P_IVA_ENTE"), "")) Then CreaTag(TestoXml, "ENTE_PROPRIETARIO_PARTITA_IVA", par.IfNull(row.Item("P_IVA_ENTE"), ""), 1)
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("RAG_SOCIALE"), "")) Then CreaTag(TestoXml, "RAGIONE_SOCIALE_ENTE_PROPRIETARIO", par.IfNull(row.Item("RAG_SOCIALE"), ""), 1)
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("DATA_RIFERIMENTO"), "")) Then CreaTag(TestoXml, "DATA_DI_RIFERIMENTO", par.IfNull(row.Item("DATA_RIFERIMENTO"), ""), 1)
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("ANNO_RIFERIMENTO"), "")) Then CreaTag(TestoXml, "PERIODO_DI_RIFERIMENTO", par.IfNull(row.Item("ANNO_RIFERIMENTO"), ""), 1)
                Next
                dt.Dispose()
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & "</RICHIESTA>"
        Catch ex As Exception
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - RichiestaType_2013 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub ProgrammazioneType_2013(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean)
        Try
            If ApriTag Then TestoXml &= vbCrLf & vbTab & "<PROGRAMMAZIONE>"
            If Tag Then
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                    'par.cmd.Transaction = connData.Transazione
                End If
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIR_PROGRAMMAZIONE WHERE ID_SIRAPER = " & idSiraper.Value
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                For Each row As Data.DataRow In dt.Rows
                    If Not String.IsNullOrEmpty(CodIstatDefault.Value) Or Not String.IsNullOrEmpty(par.IfNull(row.Item("COD_COMUNE"), "")) Then CreaTag(TestoXml, "CODICE_ISTAT_COMUNE", par.IfNull(row.Item("COD_COMUNE"), CodIstatDefault.Value), 2)
                    CreaTag(TestoXml, "ALLOGGI_NUOVI_ERP_SOCIALE", par.IfNull(row.Item("NEW_ALL_ERP"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_NUOVI_ERP_MODERATO", par.IfNull(row.Item("NEW_ALL_ERP_MODERATO"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_NUOVI_NON_ERP", par.IfNull(row.Item("NEW_ALL_NON_ERP"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_DA_ACQUISTARE_ERP_SOCIALE", par.IfNull(row.Item("ACQ_ALL_ERP"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_DA_ACQUISTARE_ERP_MODERATO", par.IfNull(row.Item("ACQ_ALL_ERP_MODERATO"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_DA_ACQUISTARE_NON_ERP", par.IfNull(row.Item("ACQ_ALL_NON_ERP"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_DA_RECUPERARE_RISTRUTTURARE_ERP_SOCIALE", par.IfNull(row.Item("RIST_ALL_ERP"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_DA_RECUPERARE_RISTRUTTURARE_ERP_MODERATO", par.IfNull(row.Item("RIST_ALL_ERP_MODERATO"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_DA_RECUPERARE_RISTRUTTURARE_NON_ERP", par.IfNull(row.Item("RIST_ALL_NON_ERP"), 0), 2)
                Next
                dt.Dispose()
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & vbTab & "</PROGRAMMAZIONE>"
        Catch ex As Exception
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - ProgrammazioneType_2013 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub FabbricatoType_2013(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, Optional ByVal rowFabbricato As Data.DataRow = Nothing)
        Try
            If Not IsNothing(rowFabbricato) Then
                If ApriTag Then TestoXml &= vbCrLf & vbTab & "<FABBRICATO>"
                If Tag Then
                    If Not IsNothing(Me.connData) Then
                        Me.connData.RiempiPar(par)
                        'par.cmd.Transaction = connData.Transazione
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("COD_FABBRICATO"), "")) Then CreaTag(TestoXml, "FABBRICATO_CODICE_DI_RICONOSCIMENTO", par.IfNull(rowFabbricato.Item("COD_FABBRICATO"), ""), 2)
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("PROPRIETA"), "")) Then CreaTag(TestoXml, "FABBRICATO_TIPO_PROPRIETA", par.IfNull(rowFabbricato.Item("PROPRIETA"), ""), 2)
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("GESTIONE"), "")) Then CreaTag(TestoXml, "FABBRICATO_TIPO_GESTIONE", par.IfNull(rowFabbricato.Item("GESTIONE"), ""), 2)
                    If Not String.IsNullOrEmpty(CodIstatDefault.Value) Or Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("COD_ISTAT_COMUNE"), "")) Then CreaTag(TestoXml, "FABBRICATO_CODICE_ISTAT_COMUNE", par.IfNull(rowFabbricato.Item("COD_ISTAT_COMUNE"), CodIstatDefault.Value), 2)
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("UBICAZIONE"), "")) Then CreaTag(TestoXml, "FABBRICATO_UBICAZIONE", par.IfNull(rowFabbricato.Item("UBICAZIONE"), ""), 2)
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("COEFF_UBICAZIONE"), "")) Then CreaTag(TestoXml, "FABBRICATO_COEFFICIENTE_UBICAZIONE", par.VirgoleInPunti(Format(Math.Round(par.IfNull(rowFabbricato.Item("COEFF_UBICAZIONE"), 0), 2), "##,##0.00")), 2)
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("ANNO_COSTRUZIONE"), "")) Then CreaTag(TestoXml, "FABBRICATO_ANNO_COSTRUZIONE", par.IfNull(rowFabbricato.Item("ANNO_COSTRUZIONE"), ""), 2)
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("ANNO_RISTRUTTURAZIONE"), "")) Then CreaTag(TestoXml, "FABBRICATO_ANNO_RISTRUTTURAZIONE", par.IfNull(rowFabbricato.Item("ANNO_RISTRUTTURAZIONE"), ""), 2)
                    If IndirizzoType_2013(TestoXml, True, True, True, par.IfNull(rowFabbricato.Item("PREFISSO_INDIRIZZO"), 1), par.IfNull(rowFabbricato.Item("NOME_VIA"), ""), par.IfNull(rowFabbricato.Item("NUMERO_CIVICO"), ""), par.IfNull(rowFabbricato.Item("ESPONENTE"), ""), par.IfNull(rowFabbricato.Item("LOCALITA"), ""), par.IfNull(rowFabbricato.Item("CAP"), "")) = False Then
                        Exit Sub
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("NUM_ALL_RISCATTO"), 0)) Then CreaTag(TestoXml, "NUMERO_ALLOGGI_A_RISCATTO", par.IfNull(rowFabbricato.Item("NUM_ALL_RISCATTO"), 0), 2)
                    par.cmd.CommandText = "SELECT * FROM SIR_ALLOGGIO WHERE ID_FABBRICATO = " & par.IfNull(rowFabbricato.Item("ID"), "-1") & " AND ID_SIRAPER = " & idSiraper.Value
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtAlloggi As New Data.DataTable
                    da.Fill(dtAlloggi)
                    da.Dispose()
                    For Each rowAlloggi As Data.DataRow In dtAlloggi.Rows
                        If EsitoPositivo Then AlloggioType_2013(TestoXml, True, True, True, rowAlloggi, par.IfNull(rowFabbricato.Item("ANNO_COSTRUZIONE"), ""))
                    Next
                    dtAlloggi.Dispose()
                End If
                If ChiudiTag Then TestoXml &= vbCrLf & vbTab & "</FABBRICATO>"
            End If
        Catch ex As Exception
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - FabbricatoType_2013 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function IndirizzoType_2013(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, Optional ByVal PrefissoIndirizzo As Integer = 1, Optional ByVal NomeVia As String = "", Optional Civico As String = "", Optional ByVal Esponente As String = "", Optional ByVal Localita As String = "", Optional ByVal Cap As String = "") As Boolean
        Try
            IndirizzoType_2013 = False
            If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & "<FABBRICATO_INDIRIZZO>"
            If Tag Then
                CreaTag(TestoXml, "PREFISSO_INDIRIZZO", PrefissoIndirizzo, 3)
                If Not String.IsNullOrEmpty(NomeVia) Then CreaTag(TestoXml, "NOME_VIA", NomeVia, 3)
                If Not String.IsNullOrEmpty(Civico) Then
                    If Len(Civico) > 10 Then
                        CreaTag(TestoXml, "NUMERO_CIVICO", Left(Civico, 10), 3)
                    Else
                        CreaTag(TestoXml, "NUMERO_CIVICO", Civico, 3)
                    End If
                End If
                If Not String.IsNullOrEmpty(Esponente) Then CreaTag(TestoXml, "ESPONENTE", Esponente, 3)
                If Not String.IsNullOrEmpty(Localita) Then CreaTag(TestoXml, "NOME_LOCALITA", Localita, 3)
                If Not String.IsNullOrEmpty(Cap) Then CreaTag(TestoXml, "CAP", Cap, 3)
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & "</FABBRICATO_INDIRIZZO>"
            IndirizzoType_2013 = True
        Catch ex As Exception
            IndirizzoType_2013 = False
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - IndirizzoType_2013 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Sub AlloggioType_2013(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, Optional ByVal rowAlloggio As Data.DataRow = Nothing, Optional ByVal AnnoCostruzione As String = "")
        Try
            If Not IsNothing(rowAlloggio) Then
                If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & "<ALLOGGIO>"
                If Tag Then
                    If Not IsNothing(Me.connData) Then
                        Me.connData.RiempiPar(par)
                        'par.cmd.Transaction = connData.Transazione
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CODICE_ALLOGGIO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_CODICE_DI_RICONOSCIMENTO", par.IfNull(rowAlloggio.Item("CODICE_ALLOGGIO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COD_FISCALE_ENTE"), "")) Then CreaTag(TestoXml, "ENTE_GESTORE_CODICE_FISCALE", par.IfNull(rowAlloggio.Item("COD_FISCALE_ENTE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("P_IVA_ENTE"), "")) Then CreaTag(TestoXml, "ENTE_GESTORE_PARTITA_IVA", par.IfNull(rowAlloggio.Item("P_IVA_ENTE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("SIGLA_ENTE"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_NOME_ENTE_GESTORE", par.IfNull(rowAlloggio.Item("SIGLA_ENTE"), "NULL"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TIPO_ENTE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_ENTE_GESTORE", par.IfNull(rowAlloggio.Item("TIPO_ENTE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TIPO_AMMINISTRAZIONE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_AMMINISTRAZIONE", par.IfNull(rowAlloggio.Item("TIPO_AMMINISTRAZIONE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ALL_DISM_CART"), "")) Then CreaTag(TestoXml, "ALLOGGIO_DISMESSO_CARTOLARIZZATO", par.IfNull(rowAlloggio.Item("ALL_DISM_CART"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PROV_DISM_CART"), "")) Then CreaTag(TestoXml, "PROVENTI_DA_DISMISSIONE_CARTOLARIZZAZIONE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("PROV_DISM_CART"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ALL_RISCATTO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_A_RISCATTO", par.IfNull(rowAlloggio.Item("ALL_RISCATTO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("SEZIONE"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_SEZIONE", par.IfNull(rowAlloggio.Item("SEZIONE"), "NULL"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("FOGLIO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_FOGLIO", par.IfNull(rowAlloggio.Item("FOGLIO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("MAPPALE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_MAPPALE", par.IfNull(rowAlloggio.Item("MAPPALE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("SUBALTERNO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_SUBALTERNO", par.IfNull(rowAlloggio.Item("SUBALTERNO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PREFISSO_INDIRIZZO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_PREFISSO_INDIRIZZO", par.IfNull(rowAlloggio.Item("PREFISSO_INDIRIZZO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NOME_VIA"), "")) Then CreaTag(TestoXml, "ALLOGGIO_NOME_VIA", par.IfNull(rowAlloggio.Item("NOME_VIA"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUMERO_CIVICO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_NUMERO_CIVICO", Left(par.IfNull(rowAlloggio.Item("NUMERO_CIVICO"), ""), 10), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ESPONENTE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_ESPONENTE", par.IfNull(rowAlloggio.Item("ESPONENTE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NOME_LOCALITA"), "")) Then CreaTag(TestoXml, "ALLOGGIO_NOME_LOCALITA", par.IfNull(rowAlloggio.Item("NOME_LOCALITA"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CAP"), "")) Then CreaTag(TestoXml, "ALLOGGIO_CAP", par.IfNull(rowAlloggio.Item("CAP"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CORD_GAUSS_X"), "")) Then CreaTag(TestoXml, "COORDINATA_GAUSS_BOAGA_X", par.PuntiInVirgole(par.IfNull(rowAlloggio.Item("CORD_GAUSS_X"), "")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CORD_GAUSS_Y"), "")) Then CreaTag(TestoXml, "COORDINATA_GAUSS_BOAGA_Y", par.PuntiInVirgole(par.IfNull(rowAlloggio.Item("CORD_GAUSS_Y"), "")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ALL_OCCUPATO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_OCCUPATO_NON_OCCUPATO", par.IfNull(rowAlloggio.Item("ALL_OCCUPATO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TIPO_GODIMENTO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_GODIMENTO", par.IfNull(rowAlloggio.Item("TIPO_GODIMENTO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ALL_ESCLUSO"), "2")) Then CreaTag(TestoXml, "ALLOGGIO_ESCLUSO", par.IfNull(rowAlloggio.Item("ALL_ESCLUSO"), "2"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CATEGORIA_CATASTALE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_CATEGORIA_CATASTALE", par.IfNull(rowAlloggio.Item("CATEGORIA_CATASTALE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("RENDITA_CATASTALE"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_RENDITA_CATASTALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("RENDITA_CATASTALE"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COEFF_CLASSE_DEMOGRAFICA_ANTE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_COEFFICIENTE_CLASSE_DEMOGRAFICA_ANTE_LEGEM", RicavaCoeff(par.IfNull(rowAlloggio.Item("COEFF_CLASSE_DEMOGRAFICA_ANTE"), "")), 3) 'par.VirgoleInPunti(par.IfNull(rowAlloggio.Item("COEFF_CLASSE_DEMOGRAFICA_ANTE"), "")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COEFF_CLASSE_DEMOGRAFICA_LR"), "")) Then CreaTag(TestoXml, "ALLOGGIO_COEFFICIENTE_CLASSE_DEMOGRAFICA_LR27", RicavaCoeff(par.IfNull(rowAlloggio.Item("COEFF_CLASSE_DEMOGRAFICA_LR"), "")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUMERO_STANZE"), "1")) Then CreaTag(TestoXml, "ALLOGGIO_NUMERO_STANZE", par.IfNull(rowAlloggio.Item("NUMERO_STANZE"), "1").ToString.Replace("0", "1"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ANNO_RISTRUTTURAZIONE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), AnnoCostruzione))) Then CreaTag(TestoXml, "ALLOGGIO_ANNO_DI_ULTIMAZIONE_RECUPERO_RISTRUTTURAZIONE", par.IfNull(rowAlloggio.Item("ANNO_RISTRUTTURAZIONE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), AnnoCostruzione)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TIPO_INTERVENTO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_INTERVENTO", par.IfNull(rowAlloggio.Item("TIPO_INTERVENTO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PIANO_ALLOGGIO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_PIANO", par.IfNull(rowAlloggio.Item("PIANO_ALLOGGIO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COEFF_PIANO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_COEFFICIENTE_PIANO", par.VirgoleInPunti(Format(Math.Round(CDec(par.IfNull(rowAlloggio.Item("COEFF_PIANO"), "")), 2), "##,##0.00")), 3)
                    If EsitoPositivo Then
                        If SuperficiType_2013(TestoXml, True, True, True, par.IfNull(rowAlloggio.Item("SUP_UTILE_ALLOGGIO"), 0), par.IfNull(rowAlloggio.Item("SUP_CANTINE_SOFF"), 0), par.IfNull(rowAlloggio.Item("SUP_BALCONI"), 0), par.IfNull(rowAlloggio.Item("SUP_AREA_PRIVATA"), 0), par.IfNull(rowAlloggio.Item("SUP_VERDE_COND"), 0), par.IfNull(rowAlloggio.Item("SUP_BOX"), 0), par.IfNull(rowAlloggio.Item("NUM_BOX"), 0), par.IfNull(rowAlloggio.Item("SUP_POSTO_AUTO"), 0), par.IfNull(rowAlloggio.Item("SUP_CONVENZIONALE_ANTE"), 0), par.IfNull(rowAlloggio.Item("SUP_CONVENZIONALE_LR"), 0), par.IfNull(rowAlloggio.Item("SUP_PERTINENZE"), 0)) = False Then Exit Sub
                    Else
                        Exit Sub
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("RISCALDAMENTO"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_RISCALDAMENTO", par.IfNull(rowAlloggio.Item("RISCALDAMENTO"), "0"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ASCENSORE"), "2")) Then CreaTag(TestoXml, "ALLOGGIO_ASCENSORE_AL_SERVIZIO", par.IfNull(rowAlloggio.Item("ASCENSORE"), "2"), 3)
                    If EsitoPositivo Then
                        If ConservazioneType_2013(TestoXml, True, True, True, par.IfNull(rowAlloggio.Item("STATO_CONS_ACCESSI"), 0), par.IfNull(rowAlloggio.Item("STATO_CONS_FACCIATA"), 0), par.IfNull(rowAlloggio.Item("STATO_CONS_PAV"), 0), par.IfNull(rowAlloggio.Item("STATO_CONS_PARETI"), 0), par.IfNull(rowAlloggio.Item("STATO_CONF_INFISSI"), 0), par.IfNull(rowAlloggio.Item("STATO_CONF_IMP_ELE"), 0), par.IfNull(rowAlloggio.Item("STATO_CONS_IMP_IDR"), 0), par.IfNull(rowAlloggio.Item("STATO_CONS_IMP_RISC"), 0), par.IfNull(rowAlloggio.Item("STATO_CONS_ALL"), 0)) = False Then Exit Sub
                    Else
                        Exit Sub
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TIPO_CUCINA"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_CUCINA", par.IfNull(rowAlloggio.Item("TIPO_CUCINA"), "0"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("BARR_ARCH"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_PRESENZA_BARRIERE_ARCHITETTONICHE", par.IfNull(rowAlloggio.Item("BARR_ARCH"), "0"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COSTO_BASE_MQ"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_COSTO_BASE_MQ_ANNUALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("COSTO_BASE_MQ"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PER_ISTAT_AGG"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_PERCENTUALE_ISTAT_AGGIORNAMENTO_ANNUALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("PER_ISTAT_AGG"), 0), 3)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CANONE_BASE"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_CANONE_BASE_EQUO_CANONE_ANNUALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CANONE_BASE"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CANONE_IND_ANN"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_CANONE_INDICIZZATO_ANNUALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CANONE_IND_ANN"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PERC_APPL"), "")) Then CreaTag(TestoXml, "ALLOGGIO_PERCENTUALE_APPLICAZIONE", par.VirgoleInPunti(par.IfNull(rowAlloggio.Item("PERC_APPL"), "")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("FASC_APPARTENENZA"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_FASCIA_APPARTENENZA", par.IfNull(rowAlloggio.Item("FASC_APPARTENENZA").ToString.ToUpper, "NULL"), 3)
                    If par.IfNull(rowAlloggio.Item("AREA_APPARTENENZA"), "0") <> 0 Then CreaTag(TestoXml, "ALLOGGIO_AREA_DI_APPARTENENZA", par.IfNull(rowAlloggio.Item("AREA_APPARTENENZA"), "0"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CANONE_APP_ANN"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_CANONE_APPLICATO_ANNUALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CANONE_APP_ANN"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PER_ISTAT_LEGGE27"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_PERCENTUALE_ISTAT_DI_AGGIORNAMENTO_LR27", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("PER_ISTAT_LEGGE27"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TIPO_CANONE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_CANONE", par.IfNull(rowAlloggio.Item("TIPO_CANONE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CANONE_ANN_ANTE"), "")) Then CreaTag(TestoXml, "CANONE_ANNUALE_ANTE_LEGEM", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CANONE_ANN_ANTE"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CANONE_ANN_REG"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "CANONE_ANUALE_A_REGIME", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CANONE_ANN_REG"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("VALORE_CONVENZIONALE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_VALORE_CONVENZIONALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("VALORE_CONVENZIONALE"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COSTO_CONVENZIONALE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_COSTO_CONVENZIONALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("COSTO_CONVENZIONALE"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CARATT_UNITA_AB"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_CARATTERISTICHE_UNITA_ABITATIVA", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CARATT_UNITA_AB"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("REDD_PREV_DIP"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2"))) Then CreaTag(TestoXml, "ALLOGGIO_REDDITO_PREVALENTEMENTE_DIPENDENTE", par.IfNull(rowAlloggio.Item("REDD_PREV_DIP"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ABBATTIMENTO_CANONE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2"))) Then CreaTag(TestoXml, "ALLOGGIO_ABBATTIMENTO_CANONE", par.IfNull(rowAlloggio.Item("ABBATTIMENTO_CANONE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("SOVRAPREZZO_DECADENZA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2"))) Then CreaTag(TestoXml, "ALLOGGIO_SOVRAPPREZZO_DECADENZA", par.IfNull(rowAlloggio.Item("SOVRAPREZZO_DECADENZA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PERC_AGG_AREA_DEC"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_PERCENTUALE_AGGIUNTIVA_PER_AREA_DECADENZA", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("PERC_AGG_AREA_DEC"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("SOVRAPREZZO_SOTTOUTILIZZO"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2"))) Then CreaTag(TestoXml, "ALLOGGIO_SOVRAPPREZZO_SOTTOUTILIZZO", par.IfNull(rowAlloggio.Item("SOVRAPREZZO_SOTTOUTILIZZO"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("REDD_DIP"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2"))) Then CreaTag(TestoXml, "ALLOGGIO_REDDITO_DIPENDENTE_MINORE_PENSIONE", par.IfNull(rowAlloggio.Item("REDD_DIP"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUM_BOX_CONTR_SEP"), "")) Then CreaTag(TestoXml, "NUMERO_BOX_POSTI_AUTO_CONTRATTO_SEPARATO", par.IfNull(rowAlloggio.Item("NUM_BOX_CONTR_SEP"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CANONE_BOX_CONTR_SEP"), "")) Then CreaTag(TestoXml, "CANONE_BOX_POSTI_AUTO_CONTRATTO_SEPARATO_ANNUALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CANONE_BOX_CONTR_SEP"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CONTAB_UNICA"), "2")) Then CreaTag(TestoXml, "ALLOGGIO_CONTABILITA_UNICA", par.IfNull(rowAlloggio.Item("CONTAB_UNICA"), "2"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("GETTITO_CONTAB_UNICA"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_GETTITO_PREVISTO_CONTABILITA_UNICA", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("GETTITO_CONTAB_UNICA"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("MOROSITA_PREC_FAM"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_MOROSITA_PRECEDENTI_FAMIGLIE_OCCUPANTI", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("MOROSITA_PREC_FAM"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("MOROSITA_ATTUALE_FAM"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_MOROSITA_ATTUALE_FAMIGLIA_OCCUPANTE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("MOROSITA_ATTUALE_FAM"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CODICE_MIR"), "")) Then CreaTag(TestoXml, "CODICE_MIR", par.IfNull(rowAlloggio.Item("CODICE_MIR"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV100_CON"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_NUM_PERSONE_INVALIDE_100_CON_INDENNITA_ACCOMPAGNAMENTO", par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV100_CON"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV100_SENZA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_NUM_PERSONE_INVALIDE_100_SENZA_INDENNITA_ACCOMPAGNAMENTO", par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV100_SENZA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV_67_99"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_NUM_PERSONE_INVALIDE_AL_67_O_99", par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV_67_99"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0")), 3)
                    If CInt(par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV100_CON"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), 0), 0))) > 0 Then
                        For i As Integer = 1 To CInt(par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV100_CON"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), 0), 0)))
                            If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("SPESE_PERSONE_INV100_CON"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_SPESE_SOST_INVALIDI_100", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("SPESE_PERSONE_INV100_CON"), 0), 2)), 3)
                        Next
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("STATO_AGG_NUCLEO"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_STATO_AGGIORNAMENTO_ANAGRAFE_NUCLEO_FAMILIARE", par.IfNull(rowAlloggio.Item("STATO_AGG_NUCLEO"), "0"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("DATA_CALCOLO_ISEE"), "")) Then CreaTag(TestoXml, "DATA_CALCOLO_ISEE", par.FormattaData(par.IfNull(rowAlloggio.Item("DATA_CALCOLO_ISEE"), ""), "-"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISR"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_ISR", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISR"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISP"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_ISP", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISP"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PSE"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_PSE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("PSE"), 1), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISE_ERP"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_ISE_ERP", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISE_ERP"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISEE_ERP"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_ISEE_ERP", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISEE_ERP"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISE_ERP_ASS"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_ISE_ERP_ASSEGNATO", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISE_ERP_ASS"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISEE_ERP_ASS"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_ISEE_ERP_ASSEGNATO", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISEE_ERP_ASS"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("REDD_DIP_ASS"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "REDDITO_DIPENDENTE_ASSIMILATO", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("REDD_DIP_ASS"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ALTRI_REDD"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALTRI_TIPI_REDDITI_IMPONIBILI", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ALTRI_REDD"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("VALORE_LOCATIVO"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "VALORE_LOCATIVO", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("VALORE_LOCATIVO"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("VALORE_MERCATO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_VALORE_MERCATO", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("VALORE_MERCATO"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COEFF_VETUSTA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "VETUSTA", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("COEFF_VETUSTA"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUM_COMPONENTI"), "0")) Then CreaTag(TestoXml, "NUMERO_COMPONENTI", par.IfNull(rowAlloggio.Item("NUM_COMPONENTI"), "0"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ANNO_VETUSTA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), AnnoCostruzione))) Then CreaTag(TestoXml, "ANNO_VETUSTA", par.IfNull(rowAlloggio.Item("ANNO_VETUSTA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), AnnoCostruzione)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PERC_VAL_LOCATIVO"), "")) Then CreaTag(TestoXml, "PERCENTUALE_VALORE_LOCATIVO", par.IfNull(rowAlloggio.Item("PERC_VAL_LOCATIVO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TAB_CLASSI_ISEE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2011-ISTAT"))) Then CreaTag(TestoXml, "TABELLA_CLASSI_ISEE", par.IfNull(rowAlloggio.Item("TAB_CLASSI_ISEE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2011-ISTAT")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("INV_SOCIALE"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2"))) Then CreaTag(TestoXml, "INVALIDITA_SOCIALE", par.IfNull(rowAlloggio.Item("INV_SOCIALE"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISEE_PRON_DECADENZA"), "")) Then CreaTag(TestoXml, "ISEE_PRONUNCIA_DECADENZA", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISEE_PRON_DECADENZA"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("DATA_DISPONIBILITA"), "")) Then CreaTag(TestoXml, "DATA_DISPONIBILITA", par.FormattaData(par.IfNull(rowAlloggio.Item("DATA_DISPONIBILITA"), ""), "-"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("DATA_ASSEGNAZIONE"), "")) Then CreaTag(TestoXml, "DATA_CONTRATTO", par.FormattaData(par.IfNull(rowAlloggio.Item("DATA_ASSEGNAZIONE"), ""), "-"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("VALORE_PATRIMONIALE"), "")) Then CreaTag(TestoXml, "VALORE_PATRIMONIALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("VALORE_PATRIMONIALE"), ""), 2)), 3)
                    par.cmd.CommandText = "SELECT * FROM SIR_INQUILINO WHERE ID_ALLOGGIO = " & par.IfNull(rowAlloggio.Item("ID"), "-1") & " AND ID_SIRAPER = " & idSiraper.Value
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtInquilini As New Data.DataTable
                    da.Fill(dtInquilini)
                    da.Dispose()
                    For Each rowInquilini As Data.DataRow In dtInquilini.Rows
                        If EsitoPositivo Then InquilinoType_2013(TestoXml, True, True, True, rowInquilini)
                    Next
                    dtInquilini.Dispose()
                End If
                If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & "</ALLOGGIO>"
            End If
        Catch ex As Exception
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - AlloggioType_2013 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function SuperficiType_2013(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, ByVal SupUtile As Decimal, ByVal SupCantine As Decimal, ByVal SupBalconi As Decimal, ByVal SupAreaPrivata As Decimal, ByVal SupVerdeCond As Decimal, ByVal SupBox As Decimal, ByVal NumPostoAuto As Integer, ByVal SupPostAuto As Decimal, ByVal SupConvAnte As Decimal, ByVal SupConvLr As Decimal, ByVal SupAltrePert As Decimal) As Boolean
        Try
            SuperficiType_2013 = False
            If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & "<ALLOGGIO_SUPERFICI>"
            If Tag Then
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_UTILE", par.VirgoleInPunti(par.IfNull(SupUtile, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_EFFETIVA_CANTINE_SOFFITTE", par.VirgoleInPunti(par.IfNull(SupCantine, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_EFFETTIVA_BALCONI", par.VirgoleInPunti(par.IfNull(SupBalconi, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_EFFETTIVA_AREA_PRIVATA", par.VirgoleInPunti(par.IfNull(SupAreaPrivata, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_VERDE_CONDOMINIALE", par.VirgoleInPunti(par.IfNull(SupVerdeCond, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_BOX", par.VirgoleInPunti(par.IfNull(SupBox, 0)), 4)
                CreaTag(TestoXml, "NUMERO_BOX_POSTI_AUTO", par.VirgoleInPunti(par.IfNull(NumPostoAuto, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_POSTO_AUTO", par.VirgoleInPunti(par.IfNull(SupPostAuto, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_CONVENZIONALE_ANTE_LEGEM", par.VirgoleInPunti(par.IfNull(SupConvAnte, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_CONVENZIONALE_LR27", par.VirgoleInPunti(par.IfNull(SupConvLr, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_ALTRE_PERTINENZE", par.VirgoleInPunti(par.IfNull(SupAltrePert, 0)), 4)
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & "</ALLOGGIO_SUPERFICI>"
            SuperficiType_2013 = True
        Catch ex As Exception
            SuperficiType_2013 = False
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - SuperficiType_2013 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function ConservazioneType_2013(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, ByVal StConsAccessi As Integer, ByVal StConsFacciata As Integer, ByVal StConsPavimenti As Integer, ByVal StConsPareti As Integer, ByVal StConsInfissi As Integer, ByVal StConsImpEle As Integer, StConsImpIdri As Integer, ByVal StConsRisc As Integer, ByVal StConsGen As Integer) As Boolean
        Try
            ConservazioneType_2013 = False
            If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & "<ALLOGGIO_STATI_CONSERVAZIONE>"
            If Tag Then
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_ACCESSI", StConsAccessi, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_FACCIATA", StConsFacciata, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_PAVIMENTI", StConsPavimenti, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_PARETI", StConsPareti, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_INFISSI", StConsInfissi, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_IMPIANTO_ELETTRICO", StConsImpEle, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_IMPIANTO_IDRICO", StConsImpIdri, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_RISCALDAMENTO", StConsRisc, 4)
                If StConsGen <> 0 Then CreaTag(TestoXml, "ALLOGGIO_CONSERVAZIONE_STATO_GENERALE", StConsGen, 4)
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & "</ALLOGGIO_STATI_CONSERVAZIONE>"
            ConservazioneType_2013 = True
        Catch ex As Exception
            ConservazioneType_2013 = False
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - ConservazioneType_2013 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Sub InquilinoType_2013(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, Optional ByVal rowInquilino As Data.DataRow = Nothing)
        Try
            If Not IsNothing(rowInquilino) Then
                If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & "<INQUILINO>"
                If Tag Then
                    If Not IsNothing(Me.connData) Then
                        Me.connData.RiempiPar(par)
                        'par.cmd.Transaction = connData.Transazione
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("COD_INQUILINO"), "")) Then CreaTag(TestoXml, "INQUILINO_CODICE_DI_RICONOSCIMENTO", par.IfNull(rowInquilino.Item("COD_INQUILINO"), ""), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("COD_FISCALE"), "")) Then CreaTag(TestoXml, "INQUILINO_CODICE_FISCALE", Trim(par.IfNull(rowInquilino.Item("COD_FISCALE"), "")), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("COGNOME"), "")) Then CreaTag(TestoXml, "INQUILINO_COGNOME", par.IfNull(rowInquilino.Item("COGNOME"), ""), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("NOME"), "")) Then CreaTag(TestoXml, "INQUILINO_NOME", par.IfNull(rowInquilino.Item("NOME"), ""), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("SESSO"), "")) Then CreaTag(TestoXml, "INQUILINO_SESSO", par.IfNull(rowInquilino.Item("SESSO"), ""), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("GR_PARENTELA"), "8")) Then CreaTag(TestoXml, "INQUILINO_RAPPORTO_PARENTELA", par.IfNull(rowInquilino.Item("GR_PARENTELA"), "8"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("TIPO_NUCLEO"), "1")) Then CreaTag(TestoXml, "INQUILINO_TIPOLOGIA_NUCLEO", par.IfNull(rowInquilino.Item("TIPO_NUCLEO"), "1"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("NUCLEO"), "1")) Then CreaTag(TestoXml, "INQUILINO_NUCLEO_FAMILIARE", par.IfNull(rowInquilino.Item("NUCLEO"), "1"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("FISC_A_CARICO"), "2")) Then CreaTag(TestoXml, "INQUILINO_FISCALMENTE_A_CARICO", par.IfNull(rowInquilino.Item("FISC_A_CARICO"), "2"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("DATA_NASCITA"), "")) Then CreaTag(TestoXml, "INQUILINO_DATA_NASCITA", par.FormattaData(par.IfNull(rowInquilino.Item("DATA_NASCITA"), ""), "-"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("LUOGO_NASCITA"), "")) Then CreaTag(TestoXml, "INQUILINO_LUOGO_NASCITA", par.IfNull(rowInquilino.Item("LUOGO_NASCITA"), ""), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("CITTADINANZA"), "1")) Then CreaTag(TestoXml, "INQUILINO_CITTADINANZA", par.IfNull(rowInquilino.Item("CITTADINANZA"), "1"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("COND_PROFESSIONALE"), "3")) Then CreaTag(TestoXml, "INQUILINO_CONDIZIONE_PROFESSIONALE_E_NON", par.IfNull(rowInquilino.Item("COND_PROFESSIONALE"), "3"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("PROFESSIONE"), "12")) Then CreaTag(TestoXml, "INQUILINO_PROFESSIONE", par.IfNull(rowInquilino.Item("PROFESSIONE"), "12"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_COMPLESSIVO"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_COMPLESSIVO", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_COMPLESSIVO"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_DIPENDENTE"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DA_LAVORO_DIPENDENTE", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_DIPENDENTE"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_AUTONOMO"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DA_LAVORO_AUTONOMO", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_AUTONOMO"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_PENSIONE"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DA_PENSIONE", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_PENSIONE"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_TERRENI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DA_TERRENI", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_TERRENI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_FABBRICATI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DA_FABBRICATI", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_FABBRICATI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_ALTRI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DI_ALTRO_TIPO", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_ALTRI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("EMOLUMENTI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_ALTRI_EMOLUMENTI", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("EMOLUMENTI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_AGRARI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DA_PROVENTI_AGRARI", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_AGRARI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("DETRAZ_IRPEF"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_DETRAZIONE_IRPEF_DOVUTA", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("DETRAZ_IRPEF"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("ANNO_REDDITO"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), CInt(Format(Now, "yyyy")) - 1))) Then CreaTag(TestoXml, "INQUILINO_ANNO_PERCEZIONE_REDDITO", par.IfNull(rowInquilino.Item("ANNO_REDDITO"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), CInt(Format(Now, "yyyy")) - 1)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("DETRAZ_SP_SANITARIE"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_DETRAZIONE_SPESE_SANITARIE", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("DETRAZ_SP_SANITARIE"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("DETRAZ_ANZ_DISABILI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_DETRAZIONE_SPESE_PER_ANZIANI_O_DISABILI", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("DETRAZ_ANZ_DISABILI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("SUSSIDI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_SUSSIDI_ENTI_PUBBLICI", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("SUSSIDI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIR_INQUILINO_PATR_MOBILIARE WHERE ID_INQUILINO = " & par.IfNull(rowInquilino.Item("ID"), "-1") & " AND ID_SIRAPER = " & idSiraper.Value
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtPatrMob As New Data.DataTable
                    da.Fill(dtPatrMob)
                    da.Dispose()
                    If dtPatrMob.Rows.Count > 0 Then
                        For Each rowPatrMob As Data.DataRow In dtPatrMob.Rows
                            If EsitoPositivo Then MobiliareType_2013(TestoXml, True, True, True, TipoPatrimonio.Completo, rowPatrMob)
                        Next
                    Else
                        'If EsitoPositivo Then MobiliareType(TestoXml, True, True, True, TipoPatrimonio.Vuoto)
                    End If
                    dtPatrMob.Dispose()
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIR_INQUILINO_PATR_IMMOBILIARE WHERE ID_INQUILINO = " & par.IfNull(rowInquilino.Item("ID"), "-1") & " AND ID_SIRAPER = " & idSiraper.Value
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtPatrImmob As New Data.DataTable
                    da.Fill(dtPatrImmob)
                    da.Dispose()
                    If dtPatrImmob.Rows.Count > 0 Then
                        For Each rowPatrImmob As Data.DataRow In dtPatrImmob.Rows
                            If EsitoPositivo Then ImmobiliareType_2013(TestoXml, True, True, True, TipoPatrimonio.Completo, rowPatrImmob)
                        Next
                    Else
                        'If EsitoPositivo Then ImmobiliareType(TestoXml, True, True, True, TipoPatrimonio.Vuoto)
                    End If
                    dtPatrImmob.Dispose()
                End If
                If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & "</INQUILINO>"
            End If
        Catch ex As Exception
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - InquilinoType_2013 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub MobiliareType_2013(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, ByVal Tipo As TipoPatrimonio, Optional ByVal rowPatrimonio As Data.DataRow = Nothing)
        Try
            If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & vbTab & "<INQUILINO_PATRIMONIO_MOBILIARE>"
            If Tag Then
                If Tipo = TipoPatrimonio.Vuoto Then
                    CreaTag(TestoXml, "ANNO_RIFERIMENTO", "2013", 5)
                    CreaTag(TestoXml, "CODICE_INTERMEDIARIO_GESTORE", "NULL", 5)
                    CreaTag(TestoXml, "DENOMINAZIONE_INTERMEDIARIO_GESTORE", "NULL", 5)
                    CreaTag(TestoXml, "IMPORTO_PATRIMONIO_MOBILIARE", "0.00", 5)
                    CreaTag(TestoXml, "VALORE_PATRIMONIO_MOBILIARE_NETTO_IMPRESE", "0.00", 5)
                Else
                    If Not IsNothing(rowPatrimonio) Then
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("ANNO_RIFERIMENTO"), "")) Then CreaTag(TestoXml, "ANNO_RIFERIMENTO", par.IfNull(rowPatrimonio.Item("ANNO_RIFERIMENTO"), ""), 5)
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("CODICE_GESTORE"), "")) Then CreaTag(TestoXml, "CODICE_INTERMEDIARIO_GESTORE", par.IfNull(rowPatrimonio.Item("CODICE_GESTORE"), ""), 5)
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("DENOMINAZIONE_GESTORE"), "")) Then CreaTag(TestoXml, "DENOMINAZIONE_INTERMEDIARIO_GESTORE", par.IfNull(rowPatrimonio.Item("DENOMINAZIONE_GESTORE"), ""), 5)
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("IMPORTO_PATRIMONIO"), "")) Then CreaTag(TestoXml, "IMPORTO_PATRIMONIO_MOBILIARE", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowPatrimonio.Item("IMPORTO_PATRIMONIO"), 0)), 2)), 5)
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("VALORE_PATRIMONIO"), "")) Then CreaTag(TestoXml, "VALORE_PATRIMONIO_MOBILIARE_NETTO_IMPRESE", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowPatrimonio.Item("VALORE_PATRIMONIO"), 0)), 2)), 5)
                    Else
                        CreaTag(TestoXml, "ANNO_RIFERIMENTO", "2013", 5)
                        CreaTag(TestoXml, "CODICE_INTERMEDIARIO_GESTORE", "NULL", 5)
                        CreaTag(TestoXml, "DENOMINAZIONE_INTERMEDIARIO_GESTORE", "NULL", 5)
                        CreaTag(TestoXml, "IMPORTO_PATRIMONIO_MOBILIARE", "0.00", 5)
                        CreaTag(TestoXml, "VALORE_PATRIMONIO_MOBILIARE_NETTO_IMPRESE", "0.00", 5)
                    End If
                End If
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & vbTab & "</INQUILINO_PATRIMONIO_MOBILIARE>"
        Catch ex As Exception
            EsitoPositivo = False
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - MobiliareType_2013 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub ImmobiliareType_2013(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, ByVal Tipo As TipoPatrimonio, Optional ByVal rowPatrimonio As Data.DataRow = Nothing)
        Try
            If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & vbTab & "<INQUILINO_PATRIMONIO_IMMOBILIARE>"
            If Tag Then
                If Tipo = TipoPatrimonio.Vuoto Then
                    CreaTag(TestoXml, "ANNO_RIFERIMENTO", "2013", 5)
                    CreaTag(TestoXml, "TIPO_PATRIMONIO_IMMOBILIARE", "3", 5)
                    CreaTag(TestoXml, "QUOTA_PROPRIETA_IMMOBILE", "1.00", 5)
                    CreaTag(TestoXml, "VALORE_ICI_IMMOBILE", "1.00", 5)
                    'CreaTag(TestoXml, "QUOTA_MUTUO_RESIDUO_IMMOBILE", "1.00", 5)
                Else
                    If Not IsNothing(rowPatrimonio) Then
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("ANNO_RIFERIMENTO"), "")) Then CreaTag(TestoXml, "ANNO_RIFERIMENTO", par.IfNull(rowPatrimonio.Item("ANNO_RIFERIMENTO"), ""), 5)
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("TIPO_PATRIMONIO"), "")) Then CreaTag(TestoXml, "TIPO_PATRIMONIO_IMMOBILIARE", par.IfNull(rowPatrimonio.Item("TIPO_PATRIMONIO"), "3"), 5)
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("QUOTA_PROPRIETA"), "")) Then CreaTag(TestoXml, "QUOTA_PROPRIETA_IMMOBILE", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowPatrimonio.Item("QUOTA_PROPRIETA"), 1)), 2)), 5)
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("VALORE_ICI"), "")) Then CreaTag(TestoXml, "VALORE_ICI_IMMOBILE", par.VirgoleInPunti(Math.Round(CDec(Format(par.IfNull(rowPatrimonio.Item("VALORE_ICI"), 1), "0,00")), 2)), 5)
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("QUOTA_MUTUO_RESIDUA"), "")) Then CreaTag(TestoXml, "QUOTA_MUTUO_RESIDUO_IMMOBILE", par.VirgoleInPunti(Math.Round(CDec(Format(par.IfNull(rowPatrimonio.Item("QUOTA_MUTUO_RESIDUA"), 0), "1,00")), 2)), 5)
                    Else
                        CreaTag(TestoXml, "ANNO_RIFERIMENTO", "2013", 5)
                        CreaTag(TestoXml, "TIPO_PATRIMONIO_IMMOBILIARE", "3", 5)
                        CreaTag(TestoXml, "QUOTA_PROPRIETA_IMMOBILE", "1.00", 5)
                        CreaTag(TestoXml, "VALORE_ICI_IMMOBILE", "1.00", 5)
                        'CreaTag(TestoXml, "QUOTA_MUTUO_RESIDUO_IMMOBILE", "1.00", 5)
                    End If
                End If
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & vbTab & "</INQUILINO_PATRIMONIO_IMMOBILIARE>"
        Catch ex As Exception
            EsitoPositivo = False
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - ImmobiliareType_2013 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
#End Region
#Region "2015"
    Private Sub CreaFileXml_2015()
        Try
            Dim TestoXml As String = ""
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            CodIstatDefault.Value = "015146"
            If EsitoPositivo Then RichiestaType_2015(TestoXml, True, True, False) 'SCRIVO LA IL TAG RICHIESTA
            par.cmd.CommandText = "SELECT ID, COD_FABBRICATO, PROPRIETA, GESTIONE, COD_ISTAT_COMUNE, UBICAZIONE, TIPO_COEFF_UBICAZIONE_EDIFICIO.VALORE AS COEFF_UBICAZIONE, ANNO_COSTRUZIONE, ANNO_RISTRUTTURAZIONE, " _
                                & "PREFISSO_INDIRIZZO, NOME_VIA, NUMERO_CIVICO, LOCALITA, CAP, NUM_ALL_RISCATTO, ESPONENTE " _
                                & "FROM SISCOM_MI.TIPO_COEFF_UBICAZIONE_EDIFICIO, SISCOM_MI.SIR_FABBRICATO WHERE TIPO_COEFF_UBICAZIONE_EDIFICIO.COD(+) = COEFF_UBICAZIONE AND ID_SIRAPER = " & idSiraper.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtFabbricati As New Data.DataTable
            da.Fill(dtFabbricati)
            da.Dispose()
            For Each rowFabbricati As Data.DataRow In dtFabbricati.Rows
                If EsitoPositivo Then FabbricatoType_2015(TestoXml, True, True, False, rowFabbricati)
                If EsitoPositivo Then FabbricatoType_2015(TestoXml, False, False, True, rowFabbricati)
            Next
            dtFabbricati.Dispose()
            If EsitoPositivo Then ProgrammazioneType_2015(TestoXml, True, True, True) 'SCRIVO IL TAG PROGRAMMAZIONE
            If EsitoPositivo Then RichiestaType_2015(TestoXml, False, False, True) 'CHIUDO IL TAG RICHIESTA
            If EsitoPositivo Then
                Dim MessaggioErrore As String = ""
                Dim NomeFile As String = ""
                If ScriviFileXml(TestoXml, MessaggioErrore, NomeFile) Then
                    If ValidificazioneXml(NomeFile, MessaggioErrore) Then
                        CreaZipFile("..\ALLEGATI\SIRAPER\" & NomeFile)
                        If EsitoPositivo = False Then Exit Sub
                        NomeFile = NomeFile.Replace("xml", "zip")
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.SIRAPER_FILE (ID, ID_SIRAPER, CARTELLA, NOME_FILE, DATA_ORA, ID_OPERATORE) VALUES " _
                                            & "(SISCOM_MI.SEQ_SIRAPER_FILE.NEXTVAL, " & idSiraper.Value & ", 'SIRAPER', '" & par.PulisciStrSql(NomeFile) & "', " & Format(Now, "yyyyMMddHHmmss") & ", " & Session.Item("ID_OPERATORE") & ")"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER SET DATA_TRASMISSIONE = " & Format(Now, "yyyyMMdd") & " WHERE ID = " & idSiraper.Value
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.SIRAPER_EVENTI (ID_SIRAPER, ID_OPERATORE, DATA_ORA, COD_EVENTO, DESCRIZIONE) VALUES " _
                                            & "(" & idSiraper.Value & ", " & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'S05', " _
                                            & "'CREAZIONE FILE XML DATI ELABORAZIONE SIRAPER')"
                        par.cmd.ExecuteNonQuery()
                        connData.chiudi(True)
                        connData.apri(True)
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIRAPER WHERE ID = " & idSiraper.Value & " FOR UPDATE NOWAIT"
                        Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        MyReader.Close()
                        Session.Add("FILECREATO", 1)
                        ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType, "msg", "alert('Creazione File Xml Completata! \n Scaricare il File dalla Maschera Principale.');", True)
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "self.close();", True)
                    Else
                        txtNote.Text = MessaggioErrore
                        If File.Exists(Server.MapPath("..\ALLEGATI\SIRAPER\") & NomeFile) Then
                            File.Delete(Server.MapPath("..\ALLEGATI\SIRAPER\") & NomeFile)
                        End If
                        ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType, "msg", "alert('Creazione File Xml Errata!');", True)
                    End If
                Else
                    If String.IsNullOrEmpty(MessaggioErrore) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType, "msg", "alert('Errore durante la scrittura del File Xml!');", True)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType, "msg", "alert('" & MessaggioErrore.Replace("'", "\'") & "');", True)
                    End If
                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: CreaFileXml - CreaFileXml_2015 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub RichiestaType_2015(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean)
        Try
            If ApriTag Then
                TestoXml &= "<?xml version='1.0' encoding='UTF-8'?>" & vbCrLf
                TestoXml &= "<RICHIESTA xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:noNamespaceSchemaLocation='" & Server.MapPath("Siraper2013_1.0.3.xsd") & "'>"
            End If
            If Tag Then
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                    'par.cmd.Transaction = connData.Transazione
                End If
                CreaTag(TestoXml, "DATA_DI_TRASMISSIONE", Format(Now, "dd-MM-yyyy"), 1)
                par.cmd.CommandText = "SELECT SIGLA_ENTE, TIPO_ENTE, COD_FISCALE_ENTE, P_IVA_ENTE, TO_CHAR(TO_DATE(DATA_RIFERIMENTO,'yyyyMMdd'),'dd-MM-yyyy') AS DATA_RIFERIMENTO, ANNO_RIFERIMENTO, RAG_SOCIALE " _
                                    & "FROM SISCOM_MI.SIRAPER WHERE ID = " & idSiraper.Value
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                For Each row As Data.DataRow In dt.Rows
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("SIGLA_ENTE"), "")) Then CreaTag(TestoXml, "SIGLA_ENTE_PROPRIETARIO", par.IfNull(row.Item("SIGLA_ENTE"), ""), 1)
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("TIPO_ENTE"), "")) Then CreaTag(TestoXml, "TIPO_ENTE_PROPRIETARIO", par.IfNull(row.Item("TIPO_ENTE"), ""), 1)
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("COD_FISCALE_ENTE"), "")) Then CreaTag(TestoXml, "ENTE_PROPRIETARIO_CODICE_FISCALE", par.IfNull(row.Item("COD_FISCALE_ENTE"), ""), 1)
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("P_IVA_ENTE"), "")) Then CreaTag(TestoXml, "ENTE_PROPRIETARIO_PARTITA_IVA", par.IfNull(row.Item("P_IVA_ENTE"), ""), 1)
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("RAG_SOCIALE"), "")) Then CreaTag(TestoXml, "RAGIONE_SOCIALE_ENTE_PROPRIETARIO", par.IfNull(row.Item("RAG_SOCIALE"), ""), 1)
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("DATA_RIFERIMENTO"), "")) Then CreaTag(TestoXml, "DATA_DI_RIFERIMENTO", par.IfNull(row.Item("DATA_RIFERIMENTO"), ""), 1)
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("ANNO_RIFERIMENTO"), "")) Then CreaTag(TestoXml, "PERIODO_DI_RIFERIMENTO", par.IfNull(row.Item("ANNO_RIFERIMENTO"), ""), 1)
                Next
                dt.Dispose()
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & "</RICHIESTA>"
        Catch ex As Exception
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - RichiestaType_2015 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function IndirizzoType_2015(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, Optional ByVal PrefissoIndirizzo As Integer = 1, Optional ByVal NomeVia As String = "", Optional Civico As String = "", Optional ByVal Esponente As String = "", Optional ByVal Localita As String = "", Optional ByVal Cap As String = "") As Boolean
        Try
            IndirizzoType_2015 = False
            If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & "<FABBRICATO_INDIRIZZO>"
            If Tag Then
                CreaTag(TestoXml, "PREFISSO_INDIRIZZO", PrefissoIndirizzo, 3)
                If Not String.IsNullOrEmpty(NomeVia) Then CreaTag(TestoXml, "NOME_VIA", NomeVia, 3)
                If Not String.IsNullOrEmpty(Civico) Then
                    If Len(Civico) > 10 Then
                        CreaTag(TestoXml, "NUMERO_CIVICO", Left(Civico, 10), 3)
                    Else
                        CreaTag(TestoXml, "NUMERO_CIVICO", Civico, 3)
                    End If
                End If
                If Not String.IsNullOrEmpty(Esponente) Then CreaTag(TestoXml, "ESPONENTE", Esponente, 3)
                If Not String.IsNullOrEmpty(Localita) Then CreaTag(TestoXml, "NOME_LOCALITA", Localita, 3)
                If Not String.IsNullOrEmpty(Cap) Then CreaTag(TestoXml, "CAP", Cap, 3)
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & "</FABBRICATO_INDIRIZZO>"
            IndirizzoType_2015 = True
        Catch ex As Exception
            IndirizzoType_2015 = False
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - IndirizzoType_2015 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Sub ProgrammazioneType_2015(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean)
        Try
            If ApriTag Then TestoXml &= vbCrLf & vbTab & "<PROGRAMMAZIONE>"
            If Tag Then
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                    'par.cmd.Transaction = connData.Transazione
                End If
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIR_PROGRAMMAZIONE WHERE ID_SIRAPER = " & idSiraper.Value
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                For Each row As Data.DataRow In dt.Rows
                    If Not String.IsNullOrEmpty(CodIstatDefault.Value) Or Not String.IsNullOrEmpty(par.IfNull(row.Item("COD_COMUNE"), "")) Then CreaTag(TestoXml, "CODICE_ISTAT_COMUNE", par.IfNull(row.Item("COD_COMUNE"), CodIstatDefault.Value), 2)
                    CreaTag(TestoXml, "ALLOGGI_NUOVI_ERP_SOCIALE", par.IfNull(row.Item("NEW_ALL_ERP"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_NUOVI_ERP_MODERATO", par.IfNull(row.Item("NEW_ALL_ERP_MODERATO"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_NUOVI_NON_ERP", par.IfNull(row.Item("NEW_ALL_NON_ERP"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_DA_ACQUISTARE_ERP_SOCIALE", par.IfNull(row.Item("ACQ_ALL_ERP"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_DA_ACQUISTARE_ERP_MODERATO", par.IfNull(row.Item("ACQ_ALL_ERP_MODERATO"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_DA_ACQUISTARE_NON_ERP", par.IfNull(row.Item("ACQ_ALL_NON_ERP"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_DA_RECUPERARE_RISTRUTTURARE_ERP_SOCIALE", par.IfNull(row.Item("RIST_ALL_ERP"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_DA_RECUPERARE_RISTRUTTURARE_ERP_MODERATO", par.IfNull(row.Item("RIST_ALL_ERP_MODERATO"), 0), 2)
                    CreaTag(TestoXml, "ALLOGGI_DA_RECUPERARE_RISTRUTTURARE_NON_ERP", par.IfNull(row.Item("RIST_ALL_NON_ERP"), 0), 2)
                Next
                dt.Dispose()
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & vbTab & "</PROGRAMMAZIONE>"
        Catch ex As Exception
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - ProgrammazioneType_2015 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub FabbricatoType_2015(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, Optional ByVal rowFabbricato As Data.DataRow = Nothing)
        Try
            If Not IsNothing(rowFabbricato) Then
                If ApriTag Then TestoXml &= vbCrLf & vbTab & "<FABBRICATO>"
                If Tag Then
                    If Not IsNothing(Me.connData) Then
                        Me.connData.RiempiPar(par)
                        'par.cmd.Transaction = connData.Transazione
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("COD_FABBRICATO"), "")) Then CreaTag(TestoXml, "FABBRICATO_CODICE_DI_RICONOSCIMENTO", par.IfNull(rowFabbricato.Item("COD_FABBRICATO"), ""), 2)
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("PROPRIETA"), "")) Then CreaTag(TestoXml, "FABBRICATO_TIPO_PROPRIETA", par.IfNull(rowFabbricato.Item("PROPRIETA"), ""), 2)
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("GESTIONE"), "")) Then CreaTag(TestoXml, "FABBRICATO_TIPO_GESTIONE", par.IfNull(rowFabbricato.Item("GESTIONE"), ""), 2)
                    If Not String.IsNullOrEmpty(CodIstatDefault.Value) Or Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("COD_ISTAT_COMUNE"), "")) Then CreaTag(TestoXml, "FABBRICATO_CODICE_ISTAT_COMUNE", par.IfNull(rowFabbricato.Item("COD_ISTAT_COMUNE"), CodIstatDefault.Value), 2)
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("UBICAZIONE"), "")) Then CreaTag(TestoXml, "FABBRICATO_UBICAZIONE", par.IfNull(rowFabbricato.Item("UBICAZIONE"), ""), 2)
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("COEFF_UBICAZIONE"), "")) Then CreaTag(TestoXml, "FABBRICATO_COEFFICIENTE_UBICAZIONE", par.VirgoleInPunti(Format(Math.Round(par.IfNull(rowFabbricato.Item("COEFF_UBICAZIONE"), 0), 2), "##,##0.00")), 2)
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("ANNO_COSTRUZIONE"), "")) Then CreaTag(TestoXml, "FABBRICATO_ANNO_COSTRUZIONE", par.IfNull(rowFabbricato.Item("ANNO_COSTRUZIONE"), ""), 2)
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("ANNO_RISTRUTTURAZIONE"), "")) Then CreaTag(TestoXml, "FABBRICATO_ANNO_RISTRUTTURAZIONE", par.IfNull(rowFabbricato.Item("ANNO_RISTRUTTURAZIONE"), ""), 2)
                    If IndirizzoType_2015(TestoXml, True, True, True, par.IfNull(rowFabbricato.Item("PREFISSO_INDIRIZZO"), 1), par.IfNull(rowFabbricato.Item("NOME_VIA"), ""), par.IfNull(rowFabbricato.Item("NUMERO_CIVICO"), ""), par.IfNull(rowFabbricato.Item("ESPONENTE"), ""), par.IfNull(rowFabbricato.Item("LOCALITA"), ""), par.IfNull(rowFabbricato.Item("CAP"), "")) = False Then
                        Exit Sub
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowFabbricato.Item("NUM_ALL_RISCATTO"), 0)) Then CreaTag(TestoXml, "NUMERO_ALLOGGI_A_RISCATTO", par.IfNull(rowFabbricato.Item("NUM_ALL_RISCATTO"), 0), 2)
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIR_ALLOGGIO WHERE ID_FABBRICATO = " & par.IfNull(rowFabbricato.Item("ID"), "-1") & " AND ID_SIRAPER = " & idSiraper.Value
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtAlloggi As New Data.DataTable
                    da.Fill(dtAlloggi)
                    da.Dispose()
                    For Each rowAlloggi As Data.DataRow In dtAlloggi.Rows
                        If EsitoPositivo Then AlloggioType_2015(TestoXml, True, True, True, rowAlloggi, par.IfNull(rowFabbricato.Item("ANNO_COSTRUZIONE"), ""))
                    Next
                    dtAlloggi.Dispose()
                End If
                If ChiudiTag Then TestoXml &= vbCrLf & vbTab & "</FABBRICATO>"
            End If
        Catch ex As Exception
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - FabbricatoType_2015 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub AlloggioType_2015(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, Optional ByVal rowAlloggio As Data.DataRow = Nothing, Optional ByVal AnnoCostruzione As String = "")
        Try
            If Not IsNothing(rowAlloggio) Then
                If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & "<ALLOGGIO>"
                If Tag Then
                    If Not IsNothing(Me.connData) Then
                        Me.connData.RiempiPar(par)
                        'par.cmd.Transaction = connData.Transazione
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CODICE_ALLOGGIO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_CODICE_DI_RICONOSCIMENTO", par.IfNull(rowAlloggio.Item("CODICE_ALLOGGIO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COD_FISCALE_ENTE"), "")) Then CreaTag(TestoXml, "ENTE_GESTORE_CODICE_FISCALE", par.IfNull(rowAlloggio.Item("COD_FISCALE_ENTE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("P_IVA_ENTE"), "")) Then CreaTag(TestoXml, "ENTE_GESTORE_PARTITA_IVA", par.IfNull(rowAlloggio.Item("P_IVA_ENTE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("SIGLA_ENTE"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_NOME_ENTE_GESTORE", par.IfNull(rowAlloggio.Item("SIGLA_ENTE"), "NULL"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TIPO_ENTE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_ENTE_GESTORE", par.IfNull(rowAlloggio.Item("TIPO_ENTE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TIPO_AMMINISTRAZIONE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_AMMINISTRAZIONE", par.IfNull(rowAlloggio.Item("TIPO_AMMINISTRAZIONE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ALL_DISM_CART"), "")) Then CreaTag(TestoXml, "ALLOGGIO_DISMESSO_CARTOLARIZZATO", par.IfNull(rowAlloggio.Item("ALL_DISM_CART"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PROV_DISM_CART"), "")) Then CreaTag(TestoXml, "PROVENTI_DA_DISMISSIONE_CARTOLARIZZAZIONE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("PROV_DISM_CART"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ALL_RISCATTO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_A_RISCATTO", par.IfNull(rowAlloggio.Item("ALL_RISCATTO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("SEZIONE"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_SEZIONE", par.IfNull(rowAlloggio.Item("SEZIONE"), "NULL"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("FOGLIO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_FOGLIO", par.IfNull(rowAlloggio.Item("FOGLIO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("MAPPALE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_MAPPALE", par.IfNull(rowAlloggio.Item("MAPPALE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("SUBALTERNO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_SUBALTERNO", par.IfNull(rowAlloggio.Item("SUBALTERNO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PREFISSO_INDIRIZZO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_PREFISSO_INDIRIZZO", par.IfNull(rowAlloggio.Item("PREFISSO_INDIRIZZO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NOME_VIA"), "")) Then CreaTag(TestoXml, "ALLOGGIO_NOME_VIA", par.IfNull(rowAlloggio.Item("NOME_VIA"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUMERO_CIVICO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_NUMERO_CIVICO", Left(par.IfNull(rowAlloggio.Item("NUMERO_CIVICO"), ""), 10), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ESPONENTE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_ESPONENTE", par.IfNull(rowAlloggio.Item("ESPONENTE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NOME_LOCALITA"), "")) Then CreaTag(TestoXml, "ALLOGGIO_NOME_LOCALITA", par.IfNull(rowAlloggio.Item("NOME_LOCALITA"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CAP"), "")) Then CreaTag(TestoXml, "ALLOGGIO_CAP", par.IfNull(rowAlloggio.Item("CAP"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CORD_GAUSS_X"), "")) Then CreaTag(TestoXml, "COORDINATA_GAUSS_BOAGA_X", par.PuntiInVirgole(par.IfNull(rowAlloggio.Item("CORD_GAUSS_X"), "")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CORD_GAUSS_Y"), "")) Then CreaTag(TestoXml, "COORDINATA_GAUSS_BOAGA_Y", par.PuntiInVirgole(par.IfNull(rowAlloggio.Item("CORD_GAUSS_Y"), "")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ALL_OCCUPATO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_OCCUPATO_NON_OCCUPATO", par.IfNull(rowAlloggio.Item("ALL_OCCUPATO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TIPO_GODIMENTO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_GODIMENTO", par.IfNull(rowAlloggio.Item("TIPO_GODIMENTO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ALL_ESCLUSO"), "2")) Then CreaTag(TestoXml, "ALLOGGIO_ESCLUSO", par.IfNull(rowAlloggio.Item("ALL_ESCLUSO"), "2"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CATEGORIA_CATASTALE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_CATEGORIA_CATASTALE", par.IfNull(rowAlloggio.Item("CATEGORIA_CATASTALE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("RENDITA_CATASTALE"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_RENDITA_CATASTALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("RENDITA_CATASTALE"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COEFF_CLASSE_DEMOGRAFICA_ANTE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_COEFFICIENTE_CLASSE_DEMOGRAFICA_ANTE_LEGEM", RicavaCoeff(par.IfNull(rowAlloggio.Item("COEFF_CLASSE_DEMOGRAFICA_ANTE"), "")), 3) 'par.VirgoleInPunti(par.IfNull(rowAlloggio.Item("COEFF_CLASSE_DEMOGRAFICA_ANTE"), "")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COEFF_CLASSE_DEMOGRAFICA_LR"), "")) Then CreaTag(TestoXml, "ALLOGGIO_COEFFICIENTE_CLASSE_DEMOGRAFICA_LR27", RicavaCoeff(par.IfNull(rowAlloggio.Item("COEFF_CLASSE_DEMOGRAFICA_LR"), "")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUMERO_STANZE"), "1")) Then CreaTag(TestoXml, "ALLOGGIO_NUMERO_STANZE", par.IfNull(rowAlloggio.Item("NUMERO_STANZE"), "1").ToString.Replace("0", "1"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ANNO_RISTRUTTURAZIONE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), AnnoCostruzione))) Then CreaTag(TestoXml, "ALLOGGIO_ANNO_DI_ULTIMAZIONE_RECUPERO_RISTRUTTURAZIONE", par.IfNull(rowAlloggio.Item("ANNO_RISTRUTTURAZIONE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), AnnoCostruzione)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TIPO_INTERVENTO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_INTERVENTO", par.IfNull(rowAlloggio.Item("TIPO_INTERVENTO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PIANO_ALLOGGIO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_PIANO", par.IfNull(rowAlloggio.Item("PIANO_ALLOGGIO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COEFF_PIANO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_COEFFICIENTE_PIANO", par.VirgoleInPunti(Format(Math.Round(CDec(par.IfNull(rowAlloggio.Item("COEFF_PIANO"), "")), 2), "##,##0.00")), 3)
                    If EsitoPositivo Then
                        If SuperficiType_2015(TestoXml, True, True, True, par.IfNull(rowAlloggio.Item("SUP_UTILE_ALLOGGIO"), 0), par.IfNull(rowAlloggio.Item("SUP_CANTINE_SOFF"), 0), par.IfNull(rowAlloggio.Item("SUP_BALCONI"), 0), par.IfNull(rowAlloggio.Item("SUP_AREA_PRIVATA"), 0), par.IfNull(rowAlloggio.Item("SUP_VERDE_COND"), 0), par.IfNull(rowAlloggio.Item("SUP_BOX"), 0), par.IfNull(rowAlloggio.Item("NUM_BOX"), 0), par.IfNull(rowAlloggio.Item("SUP_POSTO_AUTO"), 0), par.IfNull(rowAlloggio.Item("SUP_CONVENZIONALE_ANTE"), 0), par.IfNull(rowAlloggio.Item("SUP_CONVENZIONALE_LR"), 0), par.IfNull(rowAlloggio.Item("SUP_PERTINENZE"), 0)) = False Then Exit Sub
                    Else
                        Exit Sub
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("RISCALDAMENTO"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_RISCALDAMENTO", par.IfNull(rowAlloggio.Item("RISCALDAMENTO"), "0"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ASCENSORE"), "2")) Then CreaTag(TestoXml, "ALLOGGIO_ASCENSORE_AL_SERVIZIO", par.IfNull(rowAlloggio.Item("ASCENSORE"), "2"), 3)
                    If EsitoPositivo Then
                        If ConservazioneType_2015(TestoXml, True, True, True, par.IfNull(rowAlloggio.Item("STATO_CONS_ACCESSI"), 0), par.IfNull(rowAlloggio.Item("STATO_CONS_FACCIATA"), 0), par.IfNull(rowAlloggio.Item("STATO_CONS_PAV"), 0), par.IfNull(rowAlloggio.Item("STATO_CONS_PARETI"), 0), par.IfNull(rowAlloggio.Item("STATO_CONF_INFISSI"), 0), par.IfNull(rowAlloggio.Item("STATO_CONF_IMP_ELE"), 0), par.IfNull(rowAlloggio.Item("STATO_CONS_IMP_IDR"), 0), par.IfNull(rowAlloggio.Item("STATO_CONS_IMP_RISC"), 0), par.IfNull(rowAlloggio.Item("STATO_CONS_ALL"), 0)) = False Then Exit Sub
                    Else
                        Exit Sub
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TIPO_CUCINA"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_CUCINA", par.IfNull(rowAlloggio.Item("TIPO_CUCINA"), "0"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("BARR_ARCH"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_PRESENZA_BARRIERE_ARCHITETTONICHE", par.IfNull(rowAlloggio.Item("BARR_ARCH"), "0"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COSTO_BASE_MQ"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_COSTO_BASE_MQ_ANNUALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("COSTO_BASE_MQ"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PER_ISTAT_AGG"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_PERCENTUALE_ISTAT_AGGIORNAMENTO_ANNUALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("PER_ISTAT_AGG"), 0), 3)), 3)
                    If CDec(par.IfNull(rowAlloggio.Item("CANONE_BASE"), "0")) > 0 Then CreaTag(TestoXml, "ALLOGGIO_CANONE_BASE_EQUO_CANONE_ANNUALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CANONE_BASE"), 0), 2)), 3)
                    If CDec(par.IfNull(rowAlloggio.Item("CANONE_IND_ANN"), "0")) > 0 Then CreaTag(TestoXml, "ALLOGGIO_CANONE_INDICIZZATO_ANNUALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CANONE_IND_ANN"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PERC_APPL"), "")) Then CreaTag(TestoXml, "ALLOGGIO_PERCENTUALE_APPLICAZIONE", par.VirgoleInPunti(par.IfNull(rowAlloggio.Item("PERC_APPL"), "")), 3)
                    If Not String.IsNullOrEmpty(Trim(par.IfNull(rowAlloggio.Item("FASC_APPARTENENZA"), ""))) Then CreaTag(TestoXml, "ALLOGGIO_FASCIA_APPARTENENZA", par.IfNull(rowAlloggio.Item("FASC_APPARTENENZA").ToString.ToUpper, "NULL"), 3)
                    If par.IfNull(rowAlloggio.Item("AREA_APPARTENENZA"), "0") <> 0 Then CreaTag(TestoXml, "ALLOGGIO_AREA_DI_APPARTENENZA", par.IfNull(rowAlloggio.Item("AREA_APPARTENENZA"), "0"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CANONE_APP_ANN"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_CANONE_APPLICATO_ANNUALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CANONE_APP_ANN"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PER_ISTAT_LEGGE27"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_PERCENTUALE_ISTAT_DI_AGGIORNAMENTO_LR27", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("PER_ISTAT_LEGGE27"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TIPO_CANONE"), "")) Then CreaTag(TestoXml, "ALLOGGIO_TIPO_CANONE", par.IfNull(rowAlloggio.Item("TIPO_CANONE"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CANONE_ANN_ANTE"), "")) Then CreaTag(TestoXml, "CANONE_ANNUALE_ANTE_LEGEM", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CANONE_ANN_ANTE"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CANONE_ANN_REG"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "CANONE_ANUALE_A_REGIME", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CANONE_ANN_REG"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("VALORE_CONVENZIONALE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_VALORE_CONVENZIONALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("VALORE_CONVENZIONALE"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COSTO_CONVENZIONALE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_COSTO_CONVENZIONALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("COSTO_CONVENZIONALE"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CARATT_UNITA_AB"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_CARATTERISTICHE_UNITA_ABITATIVA", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CARATT_UNITA_AB"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("REDD_PREV_DIP"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2"))) Then CreaTag(TestoXml, "ALLOGGIO_REDDITO_PREVALENTEMENTE_DIPENDENTE", par.IfNull(rowAlloggio.Item("REDD_PREV_DIP"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ABBATTIMENTO_CANONE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2"))) Then CreaTag(TestoXml, "ALLOGGIO_ABBATTIMENTO_CANONE", par.IfNull(rowAlloggio.Item("ABBATTIMENTO_CANONE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("SOVRAPREZZO_DECADENZA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2"))) Then CreaTag(TestoXml, "ALLOGGIO_SOVRAPPREZZO_DECADENZA", par.IfNull(rowAlloggio.Item("SOVRAPREZZO_DECADENZA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PERC_AGG_AREA_DEC"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_PERCENTUALE_AGGIUNTIVA_PER_AREA_DECADENZA", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("PERC_AGG_AREA_DEC"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("SOVRAPREZZO_SOTTOUTILIZZO"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2"))) Then CreaTag(TestoXml, "ALLOGGIO_SOVRAPPREZZO_SOTTOUTILIZZO", par.IfNull(rowAlloggio.Item("SOVRAPREZZO_SOTTOUTILIZZO"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("REDD_DIP"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2"))) Then CreaTag(TestoXml, "ALLOGGIO_REDDITO_DIPENDENTE_MINORE_PENSIONE", par.IfNull(rowAlloggio.Item("REDD_DIP"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUM_BOX_CONTR_SEP"), "")) Then CreaTag(TestoXml, "NUMERO_BOX_POSTI_AUTO_CONTRATTO_SEPARATO", par.IfNull(rowAlloggio.Item("NUM_BOX_CONTR_SEP"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CANONE_BOX_CONTR_SEP"), "")) Then CreaTag(TestoXml, "CANONE_BOX_POSTI_AUTO_CONTRATTO_SEPARATO_ANNUALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("CANONE_BOX_CONTR_SEP"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CONTAB_UNICA"), "2")) Then CreaTag(TestoXml, "ALLOGGIO_CONTABILITA_UNICA", par.IfNull(rowAlloggio.Item("CONTAB_UNICA"), "2"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("GETTITO_CONTAB_UNICA"), "NULL")) Then CreaTag(TestoXml, "ALLOGGIO_GETTITO_PREVISTO_CONTABILITA_UNICA", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("GETTITO_CONTAB_UNICA"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("MOROSITA_PREC_FAM"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_MOROSITA_PRECEDENTI_FAMIGLIE_OCCUPANTI", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("MOROSITA_PREC_FAM"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("MOROSITA_ATTUALE_FAM"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_MOROSITA_ATTUALE_FAMIGLIA_OCCUPANTE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("MOROSITA_ATTUALE_FAM"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("CODICE_MIR"), "")) Then CreaTag(TestoXml, "CODICE_MIR", par.IfNull(rowAlloggio.Item("CODICE_MIR"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV100_CON"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_NUM_PERSONE_INVALIDE_100_CON_INDENNITA_ACCOMPAGNAMENTO", par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV100_CON"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV100_SENZA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_NUM_PERSONE_INVALIDE_100_SENZA_INDENNITA_ACCOMPAGNAMENTO", par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV100_SENZA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV_67_99"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_NUM_PERSONE_INVALIDE_AL_67_O_99", par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV_67_99"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0")), 3)
                    If CInt(par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV100_CON"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), 0), 0))) > 0 Then
                        For i As Integer = 1 To CInt(par.IfNull(rowAlloggio.Item("NUM_PERSONE_INV100_CON"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), 0), 0)))
                            If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("SPESE_PERSONE_INV100_CON"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_SPESE_SOST_INVALIDI_100", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("SPESE_PERSONE_INV100_CON"), 0), 2)), 3)
                        Next
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("STATO_AGG_NUCLEO"), "0")) Then CreaTag(TestoXml, "ALLOGGIO_STATO_AGGIORNAMENTO_ANAGRAFE_NUCLEO_FAMILIARE", par.IfNull(rowAlloggio.Item("STATO_AGG_NUCLEO"), "0"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("DATA_CALCOLO_ISEE"), "")) Then CreaTag(TestoXml, "DATA_CALCOLO_ISEE", par.FormattaData(par.IfNull(rowAlloggio.Item("DATA_CALCOLO_ISEE"), ""), "-"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISR"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_ISR", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISR"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISP"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_ISP", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISP"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PSE"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_PSE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("PSE"), 1), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISE_ERP"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_ISE_ERP", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISE_ERP"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISEE_ERP"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_ISEE_ERP", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISEE_ERP"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISE_ERP_ASS"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_ISE_ERP_ASSEGNATO", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISE_ERP_ASS"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISEE_ERP_ASS"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALLOGGIO_ISEE_ERP_ASSEGNATO", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISEE_ERP_ASS"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("REDD_DIP_ASS"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "REDDITO_DIPENDENTE_ASSIMILATO", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("REDD_DIP_ASS"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ALTRI_REDD"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "ALTRI_TIPI_REDDITI_IMPONIBILI", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ALTRI_REDD"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("VALORE_LOCATIVO"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "VALORE_LOCATIVO", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("VALORE_LOCATIVO"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("VALORE_MERCATO"), "")) Then CreaTag(TestoXml, "ALLOGGIO_VALORE_MERCATO", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("VALORE_MERCATO"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("COEFF_VETUSTA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "VETUSTA", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("COEFF_VETUSTA"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("NUM_COMPONENTI"), "0")) Then CreaTag(TestoXml, "NUMERO_COMPONENTI", par.IfNull(rowAlloggio.Item("NUM_COMPONENTI"), "0"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ANNO_VETUSTA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), AnnoCostruzione))) Then CreaTag(TestoXml, "ANNO_VETUSTA", par.IfNull(rowAlloggio.Item("ANNO_VETUSTA"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), AnnoCostruzione)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("PERC_VAL_LOCATIVO"), "")) Then CreaTag(TestoXml, "PERCENTUALE_VALORE_LOCATIVO", par.IfNull(rowAlloggio.Item("PERC_VAL_LOCATIVO"), ""), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("TAB_CLASSI_ISEE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2011-ISTAT"))) Then CreaTag(TestoXml, "TABELLA_CLASSI_ISEE", par.IfNull(rowAlloggio.Item("TAB_CLASSI_ISEE"), CanoneSociale(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2011-ISTAT")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("INV_SOCIALE"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2"))) Then CreaTag(TestoXml, "INVALIDITA_SOCIALE", par.IfNull(rowAlloggio.Item("INV_SOCIALE"), CanoneSocialeModerato(par.IfNull(rowAlloggio.Item("CANONE_SOCIALE"), ""), "2")), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("ISEE_PRON_DECADENZA"), "")) Then CreaTag(TestoXml, "ISEE_PRONUNCIA_DECADENZA", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("ISEE_PRON_DECADENZA"), 0), 2)), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("DATA_DISPONIBILITA"), "")) Then CreaTag(TestoXml, "DATA_DISPONIBILITA", par.FormattaData(par.IfNull(rowAlloggio.Item("DATA_DISPONIBILITA"), ""), "-"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("DATA_ASSEGNAZIONE"), "")) Then CreaTag(TestoXml, "DATA_CONTRATTO", par.FormattaData(par.IfNull(rowAlloggio.Item("DATA_ASSEGNAZIONE"), ""), "-"), 3)
                    If Not String.IsNullOrEmpty(par.IfNull(rowAlloggio.Item("VALORE_PATRIMONIALE"), "")) Then CreaTag(TestoXml, "VALORE_PATRIMONIALE", par.VirgoleInPunti(Math.Round(par.IfNull(rowAlloggio.Item("VALORE_PATRIMONIALE"), ""), 2)), 3)
                    par.cmd.CommandText = "SELECT * FROM SIR_INQUILINO WHERE ID_ALLOGGIO = " & par.IfNull(rowAlloggio.Item("ID"), "-1") & " AND ID_SIRAPER = " & idSiraper.Value
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtInquilini As New Data.DataTable
                    da.Fill(dtInquilini)
                    da.Dispose()
                    For Each rowInquilini As Data.DataRow In dtInquilini.Rows
                        If EsitoPositivo Then InquilinoType_2015(TestoXml, True, True, True, rowInquilini)
                    Next
                    dtInquilini.Dispose()
                End If
                If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & "</ALLOGGIO>"
            End If
        Catch ex As Exception
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - AlloggioType_2015 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function SuperficiType_2015(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, ByVal SupUtile As Decimal, ByVal SupCantine As Decimal, ByVal SupBalconi As Decimal, ByVal SupAreaPrivata As Decimal, ByVal SupVerdeCond As Decimal, ByVal SupBox As Decimal, ByVal NumPostoAuto As Integer, ByVal SupPostAuto As Decimal, ByVal SupConvAnte As Decimal, ByVal SupConvLr As Decimal, ByVal SupAltrePert As Decimal) As Boolean
        Try
            SuperficiType_2015 = False
            If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & "<ALLOGGIO_SUPERFICI>"
            If Tag Then
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_UTILE", par.VirgoleInPunti(par.IfNull(SupUtile, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_EFFETIVA_CANTINE_SOFFITTE", par.VirgoleInPunti(par.IfNull(SupCantine, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_EFFETTIVA_BALCONI", par.VirgoleInPunti(par.IfNull(SupBalconi, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_EFFETTIVA_AREA_PRIVATA", par.VirgoleInPunti(par.IfNull(SupAreaPrivata, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_VERDE_CONDOMINIALE", par.VirgoleInPunti(par.IfNull(SupVerdeCond, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_BOX", par.VirgoleInPunti(par.IfNull(SupBox, 0)), 4)
                CreaTag(TestoXml, "NUMERO_BOX_POSTI_AUTO", par.VirgoleInPunti(par.IfNull(NumPostoAuto, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_POSTO_AUTO", par.VirgoleInPunti(par.IfNull(SupPostAuto, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_CONVENZIONALE_ANTE_LEGEM", par.VirgoleInPunti(par.IfNull(SupConvAnte, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_CONVENZIONALE_LR27", par.VirgoleInPunti(par.IfNull(SupConvLr, 0)), 4)
                CreaTag(TestoXml, "ALLOGGIO_SUPERFICIE_ALTRE_PERTINENZE", par.VirgoleInPunti(par.IfNull(SupAltrePert, 0)), 4)
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & "</ALLOGGIO_SUPERFICI>"
            SuperficiType_2015 = True
        Catch ex As Exception
            SuperficiType_2015 = False
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - SuperficiType_2015 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function ConservazioneType_2015(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, ByVal StConsAccessi As Integer, ByVal StConsFacciata As Integer, ByVal StConsPavimenti As Integer, ByVal StConsPareti As Integer, ByVal StConsInfissi As Integer, ByVal StConsImpEle As Integer, StConsImpIdri As Integer, ByVal StConsRisc As Integer, ByVal StConsGen As Integer) As Boolean
        Try
            ConservazioneType_2015 = False
            If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & "<ALLOGGIO_STATI_CONSERVAZIONE>"
            If Tag Then
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_ACCESSI", StConsAccessi, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_FACCIATA", StConsFacciata, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_PAVIMENTI", StConsPavimenti, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_PARETI", StConsPareti, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_INFISSI", StConsInfissi, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_IMPIANTO_ELETTRICO", StConsImpEle, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_IMPIANTO_IDRICO", StConsImpIdri, 4)
                CreaTag(TestoXml, "ALLOGGIO_STATO_CONSERVAZIONE_RISCALDAMENTO", StConsRisc, 4)
                If StConsGen <> 0 Then CreaTag(TestoXml, "ALLOGGIO_CONSERVAZIONE_STATO_GENERALE", StConsGen, 4)
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & "</ALLOGGIO_STATI_CONSERVAZIONE>"
            ConservazioneType_2015 = True
        Catch ex As Exception
            ConservazioneType_2015 = False
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - ConservazioneType_2015 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Sub InquilinoType_2015(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, Optional ByVal rowInquilino As Data.DataRow = Nothing)
        Try
            If Not IsNothing(rowInquilino) Then
                If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & "<INQUILINO>"
                If Tag Then
                    If Not IsNothing(Me.connData) Then
                        Me.connData.RiempiPar(par)
                        'par.cmd.Transaction = connData.Transazione
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("COD_INQUILINO"), "")) Then CreaTag(TestoXml, "INQUILINO_CODICE_DI_RICONOSCIMENTO", par.IfNull(rowInquilino.Item("COD_INQUILINO"), ""), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("COD_FISCALE"), "")) Then CreaTag(TestoXml, "INQUILINO_CODICE_FISCALE", Trim(par.IfNull(rowInquilino.Item("COD_FISCALE"), "")), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("COGNOME"), "")) Then CreaTag(TestoXml, "INQUILINO_COGNOME", par.IfNull(rowInquilino.Item("COGNOME"), ""), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("NOME"), "")) Then CreaTag(TestoXml, "INQUILINO_NOME", par.IfNull(rowInquilino.Item("NOME"), ""), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("SESSO"), "")) Then CreaTag(TestoXml, "INQUILINO_SESSO", par.IfNull(rowInquilino.Item("SESSO"), ""), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("GR_PARENTELA"), "8")) Then CreaTag(TestoXml, "INQUILINO_RAPPORTO_PARENTELA", par.IfNull(rowInquilino.Item("GR_PARENTELA"), "8"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("TIPO_NUCLEO"), "1")) Then CreaTag(TestoXml, "INQUILINO_TIPOLOGIA_NUCLEO", par.IfNull(rowInquilino.Item("TIPO_NUCLEO"), "1"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("NUCLEO"), "1")) Then CreaTag(TestoXml, "INQUILINO_NUCLEO_FAMILIARE", par.IfNull(rowInquilino.Item("NUCLEO"), "1"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("FISC_A_CARICO"), "2")) Then CreaTag(TestoXml, "INQUILINO_FISCALMENTE_A_CARICO", par.IfNull(rowInquilino.Item("FISC_A_CARICO"), "2"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("DATA_NASCITA"), "")) Then CreaTag(TestoXml, "INQUILINO_DATA_NASCITA", par.FormattaData(par.IfNull(rowInquilino.Item("DATA_NASCITA"), ""), "-"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("LUOGO_NASCITA"), "")) Then CreaTag(TestoXml, "INQUILINO_LUOGO_NASCITA", par.IfNull(rowInquilino.Item("LUOGO_NASCITA"), ""), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("CITTADINANZA"), "1")) Then CreaTag(TestoXml, "INQUILINO_CITTADINANZA", par.IfNull(rowInquilino.Item("CITTADINANZA"), "1"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("COND_PROFESSIONALE"), "3")) Then CreaTag(TestoXml, "INQUILINO_CONDIZIONE_PROFESSIONALE_E_NON", par.IfNull(rowInquilino.Item("COND_PROFESSIONALE"), "3"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("PROFESSIONE"), "12")) Then CreaTag(TestoXml, "INQUILINO_PROFESSIONE", par.IfNull(rowInquilino.Item("PROFESSIONE"), "12"), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_COMPLESSIVO"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_COMPLESSIVO", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_COMPLESSIVO"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_DIPENDENTE"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DA_LAVORO_DIPENDENTE", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_DIPENDENTE"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_AUTONOMO"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DA_LAVORO_AUTONOMO", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_AUTONOMO"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_PENSIONE"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DA_PENSIONE", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_PENSIONE"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_TERRENI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DA_TERRENI", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_TERRENI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_FABBRICATI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DA_FABBRICATI", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_FABBRICATI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_ALTRI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DI_ALTRO_TIPO", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_ALTRI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("EMOLUMENTI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_ALTRI_EMOLUMENTI", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("EMOLUMENTI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("REDD_AGRARI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_REDDITO_DA_PROVENTI_AGRARI", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("REDD_AGRARI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("DETRAZ_IRPEF"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_DETRAZIONE_IRPEF_DOVUTA", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("DETRAZ_IRPEF"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("ANNO_REDDITO"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), CInt(Format(Now, "yyyy")) - 1))) Then CreaTag(TestoXml, "INQUILINO_ANNO_PERCEZIONE_REDDITO", par.IfNull(rowInquilino.Item("ANNO_REDDITO"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), CInt(Format(Now, "yyyy")) - 1)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("DETRAZ_SP_SANITARIE"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_DETRAZIONE_SPESE_SANITARIE", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("DETRAZ_SP_SANITARIE"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("DETRAZ_ANZ_DISABILI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_DETRAZIONE_SPESE_PER_ANZIANI_O_DISABILI", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("DETRAZ_ANZ_DISABILI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    If Not String.IsNullOrEmpty(par.IfNull(rowInquilino.Item("SUSSIDI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), "0"))) Then CreaTag(TestoXml, "INQUILINO_SUSSIDI_ENTI_PUBBLICI", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowInquilino.Item("SUSSIDI"), CanoneSociale(par.IfNull(rowInquilino.Item("CANONE_SOCIALE"), ""), 0))), 2)), 4)
                    Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIR_INQUILINO_PATR_MOBILIARE WHERE ID_INQUILINO = " & par.IfNull(rowInquilino.Item("ID"), "-1") & " AND ID_SIRAPER = " & idSiraper.Value
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtPatrMob As New Data.DataTable
                    da.Fill(dtPatrMob)
                    da.Dispose()
                    If dtPatrMob.Rows.Count > 0 Then
                        For Each rowPatrMob As Data.DataRow In dtPatrMob.Rows
                            If EsitoPositivo Then MobiliareType_2015(TestoXml, True, True, True, TipoPatrimonio.Completo, rowPatrMob)
                        Next
                    Else
                        'If EsitoPositivo Then MobiliareType(TestoXml, True, True, True, TipoPatrimonio.Vuoto)
                    End If
                    dtPatrMob.Dispose()
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIR_INQUILINO_PATR_IMMOBILIARE WHERE ID_INQUILINO = " & par.IfNull(rowInquilino.Item("ID"), "-1") & " AND ID_SIRAPER = " & idSiraper.Value
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtPatrImmob As New Data.DataTable
                    da.Fill(dtPatrImmob)
                    da.Dispose()
                    If dtPatrImmob.Rows.Count > 0 Then
                        For Each rowPatrImmob As Data.DataRow In dtPatrImmob.Rows
                            If EsitoPositivo Then ImmobiliareType_2015(TestoXml, True, True, True, TipoPatrimonio.Completo, rowPatrImmob)
                        Next
                    Else
                        'If EsitoPositivo Then ImmobiliareType(TestoXml, True, True, True, TipoPatrimonio.Vuoto)
                    End If
                    dtPatrImmob.Dispose()
                End If
                If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & "</INQUILINO>"
            End If
        Catch ex As Exception
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - InquilinoType_2015 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub MobiliareType_2015(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, ByVal Tipo As TipoPatrimonio, Optional ByVal rowPatrimonio As Data.DataRow = Nothing)
        Try
            If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & vbTab & "<INQUILINO_PATRIMONIO_MOBILIARE>"
            If Tag Then
                If Tipo = TipoPatrimonio.Vuoto Then
                    CreaTag(TestoXml, "ANNO_RIFERIMENTO", "2015", 5)
                    CreaTag(TestoXml, "CODICE_INTERMEDIARIO_GESTORE", "NULL", 5)
                    CreaTag(TestoXml, "DENOMINAZIONE_INTERMEDIARIO_GESTORE", "NULL", 5)
                    CreaTag(TestoXml, "IMPORTO_PATRIMONIO_MOBILIARE", "0.00", 5)
                    CreaTag(TestoXml, "VALORE_PATRIMONIO_MOBILIARE_NETTO_IMPRESE", "0.00", 5)
                Else
                    If Not IsNothing(rowPatrimonio) Then
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("ANNO_RIFERIMENTO"), "")) Then
                            CreaTag(TestoXml, "ANNO_RIFERIMENTO", par.IfNull(rowPatrimonio.Item("ANNO_RIFERIMENTO"), ""), 5)
                        Else
                            CreaTag(TestoXml, "ANNO_RIFERIMENTO", "2015", 5)
                        End If
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("CODICE_GESTORE"), "")) Then
                            CreaTag(TestoXml, "CODICE_INTERMEDIARIO_GESTORE", par.IfNull(rowPatrimonio.Item("CODICE_GESTORE"), "NULL"), 5)
                        Else
                            CreaTag(TestoXml, "CODICE_INTERMEDIARIO_GESTORE", "NULL", 5)
                        End If
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("DENOMINAZIONE_GESTORE"), "")) Then
                            CreaTag(TestoXml, "DENOMINAZIONE_INTERMEDIARIO_GESTORE", par.IfNull(rowPatrimonio.Item("DENOMINAZIONE_GESTORE"), "NULL"), 5)
                        Else
                            CreaTag(TestoXml, "DENOMINAZIONE_INTERMEDIARIO_GESTORE", "NULL", 5)
                        End If
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("IMPORTO_PATRIMONIO"), "")) Then
                            CreaTag(TestoXml, "IMPORTO_PATRIMONIO_MOBILIARE", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowPatrimonio.Item("IMPORTO_PATRIMONIO"), 0.0)), 2)), 5)
                        Else
                            CreaTag(TestoXml, "IMPORTO_PATRIMONIO_MOBILIARE", "0.00", 5)
                        End If
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("VALORE_PATRIMONIO"), "")) Then
                            CreaTag(TestoXml, "VALORE_PATRIMONIO_MOBILIARE_NETTO_IMPRESE", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowPatrimonio.Item("VALORE_PATRIMONIO"), 0.0)), 2)), 5)
                        Else
                            CreaTag(TestoXml, "VALORE_PATRIMONIO_MOBILIARE_NETTO_IMPRESE", "0.00", 5)
                        End If
                    Else
                        CreaTag(TestoXml, "ANNO_RIFERIMENTO", "2015", 5)
                        CreaTag(TestoXml, "CODICE_INTERMEDIARIO_GESTORE", "NULL", 5)
                        CreaTag(TestoXml, "DENOMINAZIONE_INTERMEDIARIO_GESTORE", "NULL", 5)
                        CreaTag(TestoXml, "IMPORTO_PATRIMONIO_MOBILIARE", "0.00", 5)
                        CreaTag(TestoXml, "VALORE_PATRIMONIO_MOBILIARE_NETTO_IMPRESE", "0.00", 5)
                    End If
                End If
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & vbTab & "</INQUILINO_PATRIMONIO_MOBILIARE>"
        Catch ex As Exception
            EsitoPositivo = False
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - MobiliareType_2015 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub ImmobiliareType_2015(ByRef TestoXml As String, ByVal ApriTag As Boolean, ByVal Tag As Boolean, ByVal ChiudiTag As Boolean, ByVal Tipo As TipoPatrimonio, Optional ByVal rowPatrimonio As Data.DataRow = Nothing)
        Try
            If ApriTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & vbTab & "<INQUILINO_PATRIMONIO_IMMOBILIARE>"
            If Tag Then
                If Tipo = TipoPatrimonio.Vuoto Then
                    CreaTag(TestoXml, "ANNO_RIFERIMENTO", "2015", 5)
                    CreaTag(TestoXml, "TIPO_PATRIMONIO_IMMOBILIARE", "3", 5)
                    CreaTag(TestoXml, "QUOTA_PROPRIETA_IMMOBILE", "1.00", 5)
                    CreaTag(TestoXml, "VALORE_ICI_IMMOBILE", "1.00", 5)
                    'CreaTag(TestoXml, "QUOTA_MUTUO_RESIDUO_IMMOBILE", "1.00", 5)
                Else
                    If Not IsNothing(rowPatrimonio) Then
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("ANNO_RIFERIMENTO"), "")) Then
                            CreaTag(TestoXml, "ANNO_RIFERIMENTO", par.IfNull(rowPatrimonio.Item("ANNO_RIFERIMENTO"), ""), 5)
                        Else
                            CreaTag(TestoXml, "ANNO_RIFERIMENTO", "2015", 5)
                        End If
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("TIPO_PATRIMONIO"), "")) Then
                            CreaTag(TestoXml, "TIPO_PATRIMONIO_IMMOBILIARE", par.IfNull(rowPatrimonio.Item("TIPO_PATRIMONIO"), "3"), 5)
                        Else
                            CreaTag(TestoXml, "TIPO_PATRIMONIO_IMMOBILIARE", "3", 5)
                        End If
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("QUOTA_PROPRIETA"), "")) Then
                            CreaTag(TestoXml, "QUOTA_PROPRIETA_IMMOBILE", par.VirgoleInPunti(Math.Round(CDec(par.IfNull(rowPatrimonio.Item("QUOTA_PROPRIETA"), 1)), 2)), 5)
                        Else
                            CreaTag(TestoXml, "QUOTA_PROPRIETA_IMMOBILE", "1.00", 5)
                        End If

                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("VALORE_ICI"), "")) Then
                            CreaTag(TestoXml, "VALORE_ICI_IMMOBILE", par.VirgoleInPunti(Math.Round(CDec(Format(par.IfNull(rowPatrimonio.Item("VALORE_ICI"), 1), "0,00")), 2)), 5)
                        Else
                            CreaTag(TestoXml, "VALORE_ICI_IMMOBILE", "1.00", 5)
                        End If
                        If Not String.IsNullOrEmpty(par.IfNull(rowPatrimonio.Item("QUOTA_MUTUO_RESIDUA"), "")) Then
                            CreaTag(TestoXml, "QUOTA_MUTUO_RESIDUO_IMMOBILE", par.VirgoleInPunti(Math.Round(CDec(Format(par.IfNull(rowPatrimonio.Item("QUOTA_MUTUO_RESIDUA"), 0), "1,00")), 2)), 5)
                        End If
                    Else
                        CreaTag(TestoXml, "ANNO_RIFERIMENTO", "2015", 5)
                        CreaTag(TestoXml, "TIPO_PATRIMONIO_IMMOBILIARE", "3", 5)
                        CreaTag(TestoXml, "QUOTA_PROPRIETA_IMMOBILE", "1.00", 5)
                        CreaTag(TestoXml, "VALORE_ICI_IMMOBILE", "1.00", 5)
                        'CreaTag(TestoXml, "QUOTA_MUTUO_RESIDUO_IMMOBILE", "1.00", 5)
                    End If
                End If
            End If
            If ChiudiTag Then TestoXml &= vbCrLf & vbTab & vbTab & vbTab & vbTab & "</INQUILINO_PATRIMONIO_IMMOBILIARE>"
        Catch ex As Exception
            EsitoPositivo = False
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - ImmobiliareType_2015 " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
#End Region
    Private Function CreaZipFile(ByVal PathFile As String) As String
        Try
            CreaZipFile = ""
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim strFile As String
            strFile = Server.MapPath(PathFile)
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
            zipfic = Server.MapPath(PathFile.Replace("xml", "zip"))
            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            File.Delete(strFile)
            Return CreaZipFile
        Catch ex As Exception
            Return ""
            EsitoPositivo = False
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: CreaFileXml - CreaZipFile - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Public Enum TipoPatrimonio
        Vuoto = 0
        Completo = 1
    End Enum
#End Region
#Region "FileXml"
    Private Function ScriviFileXml(ByVal TestoXml As String, ByRef MessaggioErrore As String, ByRef NomeFile As String) As Boolean
        Try
            ScriviFileXml = False
            If Directory.Exists(Server.MapPath("..\ALLEGATI\SIRAPER")) = False Then
                MessaggioErrore = "Cartella per la creazione del File del Comune: non presente"
                Directory.CreateDirectory(Server.MapPath("..\ALLEGATI\SIRAPER"))
                MessaggioErrore = ""
            End If
            NomeFile = "Siraper_" & Format(Now, "yyyyMMddHHmmss") & ".xml"
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\SIRAPER\") & NomeFile, False, System.Text.Encoding.Default)
            sr.WriteLine(TestoXml)
            sr.Close()
            ScriviFileXml = True
        Catch ex As Exception
            ScriviFileXml = True
            If String.IsNullOrEmpty(MessaggioErrore) Then
                MessaggioErrore = ex.Message
            End If
        End Try
    End Function
    Private Function ValidificazioneXml(ByVal NomeFile As String, ByRef MessaggioErrore As String) As Boolean
        Try
            ValidificazioneXml = False
            Dim Percorso As String = ""
            Select Case idSiraperVersione.Value.ToString
                Case "1"
                    Percorso = "Validazione/Siraper2013_1.0.0.xsd"
                Case "2"
                    Percorso = "Validazione/Siraper2015_1.0.0.xsd"
            End Select
            Dim MyfileXsd As New System.IO.StreamReader(Server.MapPath(Percorso), True)
            Dim txtXsd As String = MyfileXsd.ReadToEnd
            MyfileXsd.Close()
            Dim MyfileXml As New System.IO.StreamReader(Server.MapPath("..\ALLEGATI\SIRAPER\") & NomeFile, True)
            Dim txtXml As String = MyfileXml.ReadToEnd
            MyfileXml.Close()
            If LoadXMLData(txtXml, txtXsd, MessaggioErrore) Then
                ValidificazioneXml = True
            Else
                ValidificazioneXml = False
            End If
        Catch ex As Exception
            ValidificazioneXml = False
        End Try
    End Function
    Private Sub XMLEvent(ByVal sender As Object, ByVal e As System.Xml.Schema.ValidationEventArgs)
        XMLError += (e.Message & vbCrLf)
    End Sub
    Public Function LoadXMLData(ByVal XMLData As String, ByVal XSDSchema As String, ByRef MessaggioErrore As String) As Boolean
        Dim MyXMLDocument As New System.Xml.XmlDocument
        Try
            XMLError = ""
            Dim MyXSDTextReader As New System.IO.StringReader(XSDSchema)
            Dim MyXMLSchema As System.Xml.Schema.XmlSchema = System.Xml.Schema.XmlSchema.Read(MyXSDTextReader, New System.Xml.Schema.ValidationEventHandler(AddressOf XMLEvent))
            Dim MyXMLTextReader As New System.IO.StringReader(XMLData)
            MyXMLDocument.Load(MyXMLTextReader)
            MyXMLDocument.Schemas.Add(MyXMLSchema)
            MyXMLDocument.Validate(New System.Xml.Schema.ValidationEventHandler(AddressOf XMLEvent))
            If String.IsNullOrEmpty(XMLError) Then
                LoadXMLData = True
            Else
                LoadXMLData = False
            End If
            MessaggioErrore = XMLError
        Catch ex As Exception
            MessaggioErrore = "File xml errato. Verificare che i tag di apertura e chiusura coincidano e che il file sia sintatticamente corretto!"
            LoadXMLData = False
        End Try
    End Function
#End Region
#Region "Funzioni"
    Private Sub CreaTag(ByRef TestoXml As String, ByVal Tag As String, ByVal Testo As String, ByVal Tab As Integer)
        Dim TagDaInserire As String = ""
        TagDaInserire = vbCrLf
        For i As Integer = 1 To Tab Step 1
            TagDaInserire = TagDaInserire & vbTab
        Next
        TagDaInserire = TagDaInserire & "<" & Tag & ">" & Testo & "</" & Tag & ">"
        TestoXml &= TagDaInserire
    End Sub
    Private Function CanoneSociale(ByVal Canone As Integer, ByVal Valore As String) As String
        Try
            CanoneSociale = ""
            Select Case Canone
                Case 1
                    CanoneSociale = Valore
                Case Else
                    CanoneSociale = ""
            End Select
        Catch ex As Exception
            CanoneSociale = ""
        End Try
    End Function
    Private Function CanoneSocialeModerato(ByVal Canone As Integer, ByVal Valore As String) As String
        Try
            CanoneSocialeModerato = ""
            Select Case Canone
                Case 1, 2
                    CanoneSocialeModerato = Valore
                Case Else
                    CanoneSocialeModerato = ""
            End Select
        Catch ex As Exception
            CanoneSocialeModerato = ""
        End Try
    End Function
    Private Function RicavaCoeff(ByVal Cod As Integer) As String
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            RicavaCoeff = "NULL"
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.TIPO_COEFF_UBICAZIONE_EDIFICIO WHERE COD = " & Cod
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                RicavaCoeff = par.VirgoleInPunti(par.IfNull(MyReader("VALORE"), 0))
            End If
            MyReader.Close()
        Catch ex As Exception
            EsitoPositivo = False
            RicavaCoeff = "NULL"
            Session.Add("ERRORE", "Provenienza: SIRAPERXml - ImmobiliareType " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
#End Region
End Class
