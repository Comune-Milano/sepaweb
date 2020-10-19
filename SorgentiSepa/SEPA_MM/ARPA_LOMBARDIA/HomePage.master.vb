Imports System.IO
Imports Telerik.Web.UI

Partial Class ARPA_LOMBARDIA_HomePage
    Inherits System.Web.UI.MasterPage
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing
    Public StringaSiscom As String = "SISCOM_MI."
    Dim CodFiscEnteProp As String = ""
    Public version As String = Mid(System.Configuration.ConfigurationManager.AppSettings("Versione"), 10).ToString.Trim().Replace(" ", "")

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Session.Item("OPERATORE")) Then
            If String.IsNullOrEmpty(Trim(Session.Item("OPERATORE"))) Then
                Response.Redirect("~/AccessoNegato.htm", False)
                Response.End()
            Else
                If par.getLockSessione = False Then
                    Response.Redirect("~/AccessoNegato.htm", False)
                    Response.End()
                End If
            End If
        Else
            Response.Redirect("~/AccessoNegato.htm", False)
            Response.End()
        End If
        Me.ID = "MasterHomePage"
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("KEY")) Then
                If par.getLockPage(Request.QueryString("KEY").ToString) = False Then
                    Response.Redirect("~/AccessoNegato.htm", False)
                    Response.End()
                End If
            Else
                Response.Redirect("~/AccessoNegato.htm", False)
                Response.End()
            End If
            CaricaOperatore()
            SetPathLock()
            CaricaAnniElaborazioni()
            ElaborazioneEsistente()
            NascondiMenu()
            'SEGNALAZIONE SD 1543/2018
            trAnnoElaborazione.Visible = False
        End If
    End Sub
    Private Sub CaricaOperatore()
        connData = New CM.datiConnessione(par, False, False)
        Try
            lblOperatore.Text = par.IfNull(Session.Item("NOME_OPERATORE").ToString, "- - -")
            If Not IsNothing(Session.Item("ID_STRUTTURA")) Then
                connData.apri(False)
                par.cmd.CommandText = "SELECT NOME, TIPOLOGIA_STRUTTURA_ALER.DESCRIZIONE " _
                                    & "FROM " & StringaSiscom & "TAB_FILIALI, " & StringaSiscom & "TIPOLOGIA_STRUTTURA_ALER " _
                                    & "WHERE TIPOLOGIA_STRUTTURA_ALER.ID(+) = TAB_FILIALI.ID_TIPO_ST AND TAB_FILIALI.ID = " & par.insDbValue(Session.Item("ID_STRUTTURA").ToString, True)
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    lblFiliale.Text = par.IfEmpty(MyReader("NOME"), "- - -").ToString.ToUpper & "<br>" & par.IfEmpty(MyReader("DESCRIZIONE"), "").ToString.ToUpper
                Else
                    lblFiliale.Text = "- - -<br>- - -"
                End If
                MyReader.Close()
                connData.chiudi(False)
            Else
                lblFiliale.Text = "- - -<br>- - -"
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_HomePage - HomePage_Master - CaricaOperatore - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        Finally
            connData.PulisciPool()
        End Try
    End Sub
    Private Sub SetPathLock()
        Try
            Dim PathLock As String = "SepacomLock.svc"
            Dim PathTrovato As Boolean = False
            While PathTrovato = False
                If File.Exists(Server.MapPath(PathLock)) Then
                    HFPathLock.Value = PathLock
                    PathTrovato = True
                Else
                    PathLock = "../" & PathLock
                End If
            End While
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_HomePage - HomePage_Master - SetPathLock - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Sub CaricaAnniElaborazioni()
        Try
            ddlAnnoElaborazione.Items.Clear()
            For i = Format(Now, "yyyy") - 1 To 2015 Step -1
                Dim item As New Telerik.Web.UI.RadComboBoxItem()
                item.Text = CStr(i)
                item.Value = CStr(i)
                ddlAnnoElaborazione.Items.Add(item)
            Next
            ddlAnnoElaborazione.SelectedValue = ddlAnnoElaborazione.Items(0).Value
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_HomePage - HomePage_Master - CaricaAnniElaborazioni - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Sub ElaborazioneEsistente()
        Try
            connData = New CM.datiConnessione(par, False, False)
            connData.apri(False)
            par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "ARPA_ELABORAZIONI WHERE ANNO = " & par.RitornaNullSeMenoUno(ddlAnnoElaborazione.SelectedValue, True)
            Dim NrElaborazioni As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            connData.chiudi(False)
            If NrElaborazioni > 0 Then
                imgAlert.Visible = True
                lblAlert.Visible = True
            Else
                imgAlert.Visible = False
                lblAlert.Visible = False
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_HomePage - HomePage_Master - ElaborazioneEsistente - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub ddlAnnoElaborazione_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlAnnoElaborazione.SelectedIndexChanged
        ElaborazioneEsistente()
    End Sub
    Protected Sub btnNuovaElaborazione_Click(sender As Object, e As System.EventArgs) Handles btnNuovaElaborazione.Click
        NuovaElaborazione()
    End Sub
    Private Sub NuovaElaborazione()
        Try
            'If imgAlert.Visible Then
            '    RadWindowManager1.RadConfirm("Sei sicuro di voler ricrearne una nuova elaborazione per l\'anno selezionato?", "confirmCallbackFnElaborazione", 400, 150, Nothing, "Attenzione")
            'Else
            '    NuovaElaborazioneConferma(False)
            'End If
            'SEGNALAZIONE SD 1543/2018
            NuovaElaborazioneConferma(False)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_HomePage - HomePage_Master - NuovaElaborazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Sub NuovaElaborazioneConferma(ByVal Conferma As Boolean)
        Try
            connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)
            par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "ARPA_PROCEDURE WHERE ESITO = 0"
            Dim NrElaborazioni As Integer = par.cmd.ExecuteScalar
            If NrElaborazioni > 0 Then
                connData.chiudi(False)
                RadNotificationNote.Text = "E' stata già avviata una Procedura! Attendere il termine."
                RadNotificationNote.Show()
                Exit Sub
            End If
            'If Conferma Then
            '    par.cmd.CommandText = "INSERT INTO " & StringaSiscom & "ARPA_LOG (DATA_ORA, ID_OPERATORE, DESCRIZIONE) VALUES " _
            '                        & "('" & Format(Now, "yyyyMMddHHmmss") & "', " & Session.Item("ID_OPERATORE").ToString & ", 'Rielaborazione Anno: " & ddlAnnoElaborazione.SelectedItem.Text.ToString & "')"
            'Else
            '    par.cmd.CommandText = "INSERT INTO " & StringaSiscom & "ARPA_LOG (DATA_ORA, ID_OPERATORE, DESCRIZIONE) VALUES " _
            '                        & "('" & Format(Now, "yyyyMMddHHmmss") & "', " & Session.Item("ID_OPERATORE").ToString & ", 'Nuova Elaborazione Anno: " & ddlAnnoElaborazione.SelectedItem.Text.ToString & "')"
            'End If
            'SEGNALAZIONE SD 1543/2018
            par.cmd.CommandText = "INSERT INTO " & StringaSiscom & "ARPA_LOG (DATA_ORA, ID_OPERATORE, DESCRIZIONE) VALUES " _
                                & "('" & Format(Now, "yyyyMMddHHmmss") & "', " & Session.Item("ID_OPERATORE").ToString & ", 'Nuova Elaborazione Anno: " & Format(Now, "yyyy") & "')"
            par.cmd.ExecuteNonQuery()
            Dim Continua As Boolean = True
            'If imgAlert.Visible Then
            '    If EliminaElaborazionePrecedente() = False Then
            '        Continua = False
            '    End If
            'End If
            If Continua = True Then
                If ControlliPreElaborazione() Then
                    AvviaElaborazione("1")
                    'If CreaElaborazione(idElaborazione) Then
                    '    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Elaborazione Completata');", True)
                    '    connData.chiudi(True)
                    '    CaricaAnniElaborazioni()
                    '    ElaborazioneEsistente()
                    'Else
                    '    connData.chiudi(False)
                    'End If
                Else
                    connData.chiudi(False)
                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_HomePage - HomePage_Master - NuovaElaborazioneConferma - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Sub AvviaElaborazione(ByVal Validazione As String)
        par.cmd.CommandText = "SELECT " & StringaSiscom & "SEQ_ARPA_ELABORAZIONI.NEXTVAL FROM DUAL"
        Dim idElaborazione As String = par.cmd.ExecuteScalar
        'par.cmd.CommandText = "INSERT INTO " & StringaSiscom & "ARPA_ELABORAZIONI (ID, ANNO, ID_OPERATORE, DATA_ORA, CF_ENTE_PROPRIETARIO, FL_VALIDAZIONE) VALUES " _
        '                    & "(" & idElaborazione & ", '" & ddlAnnoElaborazione.SelectedItem.Text.ToString & "', " & Session.Item("ID_OPERATORE").ToString & ", '" & Format(Now, "yyyyMMddHHmmss") & "', " & par.insDbValue(CodFiscEnteProp, True) & ", " & Validazione & ")"
        'SEGNALAZIONE SD 1543/2018
        par.cmd.CommandText = "INSERT INTO " & StringaSiscom & "ARPA_ELABORAZIONI (ID, ANNO, ID_OPERATORE, DATA_ORA, CF_ENTE_PROPRIETARIO, FL_VALIDAZIONE) VALUES " _
                            & "(" & idElaborazione & ", '" & Format(Now, "yyyy") & "', " & Session.Item("ID_OPERATORE").ToString & ", '" & Format(Now, "yyyyMMddHHmmss") & "', " & par.insDbValue(CodFiscEnteProp, True) & ", " & Validazione & ")"
        par.cmd.ExecuteNonQuery()
        Dim FlagDatiAccatastati As String = "0"
        If cbDatiNonAccatastati.Checked Then FlagDatiAccatastati = "1"
        par.cmd.CommandText = "SELECT " & StringaSiscom & "SEQ_ARPA_PROCEDURE.NEXTVAL FROM DUAL"
        Dim idProcedura As String = par.cmd.ExecuteScalar
        par.cmd.CommandText = "INSERT INTO " & StringaSiscom & "ARPA_PROCEDURE (ID, ID_OPERATORE, DATA_ORA_INIZIO, DATA_ORA_FINE, ESITO, ERRORE, " _
                            & "PARZIALE, TOTALE, PARAMETRI, ID_TIPO) VALUES " _
                            & "(" & idProcedura & ", " & Session.Item("ID_OPERATORE").ToString & ", '" & Format(Now, "yyyyMMddHHmmss") & "', '', 0, '', " _
                            & "0, 100, '" & idElaborazione & ";" & FlagDatiAccatastati & ";', 1)"
        par.cmd.ExecuteNonQuery()
        connData.chiudi(True)
        connData.apri(False)
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
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "msg", "alert('Elaborazione avviata correttamente!');", True)
        Catch ex As Exception
            par.cmd.CommandText = "UPDATE " & StringaSiscom & "ARPA_PROCEDURE SET ESITO = 2, DATA_ORA_FINE = '" & Format(Now, "yyyyMMddHHmmss") & "', ERRORE = 'Procedura non Avviata' WHERE ID = " & idElaborazione
            par.cmd.ExecuteNonQuery()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante l\'avvio dell\'Elaborazione!');", True)
        End Try
        connData.chiudi(False)
    End Sub
    Protected Sub btnConfermaElaborazione_Click(sender As Object, e As System.EventArgs) Handles btnConfermaElaborazione.Click
        Try
            NuovaElaborazioneConferma(True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_HomePage - HomePage_Master - btnConfermaElaborazione_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Function EliminaElaborazionePrecedente() As Boolean
        EliminaElaborazionePrecedente = False
        par.cmd.CommandText = "SELECT ID FROM " & StringaSiscom & "ARPA_ELABORAZIONI WHERE ANNO = " & par.RitornaNullSeMenoUno(ddlAnnoElaborazione.SelectedValue, True)
        Dim idElaborazionePrecedente As String = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, "-1"), "-1")
        par.cmd.CommandText = "DELETE FROM " & StringaSiscom & "ARPA_ELABORAZIONI_UNITA WHERE ID_ELABORAZIONE = " & idElaborazionePrecedente
        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = "DELETE FROM " & StringaSiscom & "ARPA_ELABORAZIONI_FABBRICATI WHERE ID_ELABORAZIONE = " & idElaborazionePrecedente
        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = "DELETE FROM " & StringaSiscom & "ARPA_ELABORAZIONI WHERE ID = " & idElaborazionePrecedente
        par.cmd.ExecuteNonQuery()
        EliminaElaborazionePrecedente = True
    End Function
    Private Function ControlliPreElaborazione() As Boolean
        ControlliPreElaborazione = False
        Dim ControlloBloccante As Boolean = False
        Dim Errori As String = ""
        Dim ErroriBloccanti As String = ""
        par.cmd.CommandText = "SELECT VALORE FROM " & StringaSiscom & "ARPA_PARAMETRI WHERE ID = 1"
        CodFiscEnteProp = par.IfNull(par.cmd.ExecuteScalar, "")
        If String.IsNullOrEmpty(Trim(CodFiscEnteProp)) Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- Definire il Codice Fiscale Ente Proprietario;"
            Else
                ErroriBloccanti &= "<br>" & "- Definire il Codice Fiscale Ente Proprietario;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "TAB_GESTORI_ARCHIVIO WHERE FL_ARPA = 1 AND COD_ARPA IS NULL"
        Dim NrTipologieGestori As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrTipologieGestori > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- Definire tutte le Tipologie dei Gestori;"
            Else
                ErroriBloccanti &= "<br>" & "- Definire tutte le Tipologie dei Gestori;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "TAB_GESTORI_ARCHIVIO WHERE FL_ARPA = 1 AND CODICE_FISCALE IS NULL AND COD_ARPA <> 4"
        Dim NrTipologieGestoriCodFiscale As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrTipologieGestoriCodFiscale > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- Definire tutti i Codici Fiscali dei Gestori di tipo Aler, Comune e Operatore Privato;"
            Else
                ErroriBloccanti &= "<br>" & "- Definire tutti i Codici Fiscali dei Gestori di tipo Aler, Comune e Operatore Privato;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "TAB_GESTORI_ARCHIVIO WHERE FL_ARPA = 1 AND RAG_SOCIALE IS NULL AND COD_ARPA <> 4"
        Dim NrTipologieGestoriRagSociale As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrTipologieGestoriRagSociale > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- Definire tutte le Ragioni Sociali dei Gestori di tipo Aler, Comune e Operatore Privato;"
            Else
                ErroriBloccanti &= "<br>" & "- Definire tutte le Ragioni Sociali dei Gestori di tipo Aler, Comune e Operatore Privato;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "TAB_GESTORI_ARCHIVIO WHERE FL_ARPA = 1 AND DENOMINAZIONE IS NULL AND COD_ARPA = 4"
        Dim NrTipologieGestoriDenominazione As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrTipologieGestoriDenominazione > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- Definire tutti le Denominazioni dei Gestori di tipo Autogestione;"
            Else
                ErroriBloccanti &= "<br>" & "- Definire tutti le Denominazioni dei Gestori di tipo Autogestione;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "TIPOLOGIA_INDIRIZZO WHERE COD_ARPA IS NULL"
        Dim NrTipologiaIndirizzo As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrTipologiaIndirizzo > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- Definire tutti i prefissi degli indirizzi;"
            Else
                ErroriBloccanti &= "<br>" & "- Definire tutti i prefissi degli indirizzi;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "TIPO_LIVELLO_PIANO WHERE COD_ARPA IS NULL"
        Dim NrTipologiaPiano As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrTipologiaPiano > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- Definire tutte le tipologie del livello piano;"
            Else
                ErroriBloccanti &= "<br>" & "- Definire tutte le tipologie del livello piano;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "DESTINAZIONI_USO_RL_UI WHERE COD_ARPA IS NULL"
        Dim NrDestinazioniUso As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrDestinazioniUso > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- Definire tutte le destinazioni d\'Uso RL;"
            Else
                ErroriBloccanti &= "<br>" & "- Definire tutte le destinazioni d\'Uso RL;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM T_TIPO_PARENTELA WHERE COD_ARPA IS NULL"
        Dim NrTipParentela As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrTipParentela > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- Definire tutte le tipologie di parentela;"
            Else
                ErroriBloccanti &= "<br>" & "- Definire tutte le tipologie di parentela;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "CATEGORIA_CATASTALE WHERE COD_ARPA_CATEGORIA IS NULL AND COD <> '000'"
        Dim NrCatCatastaleCat As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrCatCatastaleCat > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- Anomalia sulle categoria catastali (Contattare l\'amministratore del sistema);"
            Else
                ErroriBloccanti &= "<br>" & "- Anomalia sulle categoria catastali (Contattare l\'amministratore del sistema);"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "CATEGORIA_CATASTALE WHERE COD_ARPA_CLASSE IS NULL AND COD <> '000'"
        Dim NrCatCatastaleClasse As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrCatCatastaleClasse > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- Anomalia sulla classe catastale (Contattare l\'amministratore del sistema);"
            Else
                ErroriBloccanti &= "<br>" & "- Anomalia sulla classe catastale (Contattare l\'amministratore del sistema);"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "UNITA_IMMOBILIARI, " & StringaSiscom & "IDENTIFICATIVI_CATASTALI " _
                            & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                            & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                            & "AND (IDENTIFICATIVI_CATASTALI.FOGLIO IS NULL OR IDENTIFICATIVI_CATASTALI.NUMERO IS NULL OR IDENTIFICATIVI_CATASTALI.SUB IS NULL)"
        Dim NrAnomaliaUiFogPartSub As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrAnomaliaUiFogPartSub > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                Errori = "- <a href=""javascript:ApriAnomalie(1);"">Anomalia sui dati catastali (Foglio, Particella, Sub) delle UI</a>;"
            Else
                Errori &= "<br>" & "- <a href=""javascript:ApriAnomalie(1);"">Anomalia sui dati catastali (Foglio, Particella, Sub) delle UI</a>;"
            End If
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "UNITA_IMMOBILIARI, " & StringaSiscom & "IDENTIFICATIVI_CATASTALI " _
                            & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                            & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                            & "AND (COD_CATEGORIA_CATASTALE IS NULL OR COD_CATEGORIA_CATASTALE = '000')"
        Dim NrAnomaliaUiCatCatastale As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrAnomaliaUiCatCatastale > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                Errori = "- <a href=""javascript:ApriAnomalie(2);"">Anomalia sulla categoria catastale delle UI</a>;"
            Else
                Errori &= "<br>" & "- <a href=""javascript:ApriAnomalie(2);"">Anomalia sulla categoria catastale delle UI</a>;"
            End If
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "UNITA_IMMOBILIARI, " & StringaSiscom & "IDENTIFICATIVI_CATASTALI " _
                            & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                            & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                            & "AND (NUM_VANI IS NULL OR NVL(NUM_VANI, 0) = 0)"
        Dim NrAnomaliaUiConsistenza As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrAnomaliaUiConsistenza > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                Errori = "- <a href=""javascript:ApriAnomalie(3);"">Anomalia sulla consistenza (Numero Vani) delle UI</a>;"
            Else
                Errori &= "<br>" & "- <a href=""javascript:ApriAnomalie(3);"">Anomalia sulla consistenza (Numero Vani) delle UI</a>;"
            End If
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "UNITA_IMMOBILIARI, " & StringaSiscom & "IDENTIFICATIVI_CATASTALI " _
                            & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                            & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                            & "AND (RENDITA IS NULL OR NVL(RENDITA, 0) = 0)"
        Dim NrAnomaliaUiRendita As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrAnomaliaUiRendita > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                Errori = "- <a href=""javascript:ApriAnomalie(4);"">Anomalia sulla Rendita delle UI</a>;"
            Else
                Errori &= "<br>" & "- <a href=""javascript:ApriAnomalie(4);"">Anomalia sulla Rendita delle UI</a>;"
            End If
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "UNITA_IMMOBILIARI " _
                            & "WHERE UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                            & "AND ID_INDIRIZZO IS NULL"
        Dim NrAnomaliaIndirizzi As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrAnomaliaIndirizzi > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- <a href=""javascript:ApriAnomalie(5);"">Anomalia sugli indirizzi delle UI</a>;"
            Else
                ErroriBloccanti &= "<br>" & "- <a href=""javascript:ApriAnomalie(5);"">Anomalia sugli indirizzi delle UI</a>;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "UNITA_IMMOBILIARI " _
                            & "WHERE UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                            & "AND COD_TIPO_LIVELLO_PIANO IS NULL"
        Dim NrAnomaliaPiano As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrAnomaliaPiano > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- <a href=""javascript:ApriAnomalie(6);"">Anomalia sul piano delle UI</a>;"
            Else
                ErroriBloccanti &= "<br>" & "- <a href=""javascript:ApriAnomalie(6);"">Anomalia sul piano delle UI</a>;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        'par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "UNITA_IMMOBILIARI " _
        '                    & "WHERE UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
        '                    & "AND NOT EXISTS (SELECT ID_UNITA_IMMOBILIARE FROM " & StringaSiscom & "DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA' AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)"
        'Dim NrAnomaliaSuperficie As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        'If NrAnomaliaSuperficie > 0 Then
        '    If String.IsNullOrEmpty(Trim(Errori)) Then
        '        Errori = "- <a href=""javascript:ApriAnomalie(7);"">Anomalia sulla superficie netta delle UI</a>;"
        '    Else
        '        Errori &= "<br>" & "- <a href=""javascript:ApriAnomalie(7);"">Anomalia sulla superficie netta delle UI</a>;"
        '    End If
        'End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "UNITA_IMMOBILIARI " _
                            & "WHERE UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                            & "AND ID_DESTINAZIONE_USO_RL IS NULL"
        Dim NrAnomaliaDestinazioneUsoRL As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrAnomaliaDestinazioneUsoRL > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- <a href=""javascript:ApriAnomalie(8);"">Anomalia sulla destinazione d\'uso LR delle UI</a>;"
            Else
                ErroriBloccanti &= "<br>" & "- <a href=""javascript:ApriAnomalie(8);"">Anomalia sulla destinazione d\'uso LR delle UI</a>;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "UNITA_IMMOBILIARI, " & StringaSiscom & "IDENTIFICATIVI_CATASTALI " _
                            & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                            & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                            & "AND (" & StringaSiscom & "IS_NUMERIC(IDENTIFICATIVI_CATASTALI.FOGLIO) = 0 OR " & StringaSiscom & "IS_NUMERIC(IDENTIFICATIVI_CATASTALI.NUMERO) = 0 OR " & StringaSiscom & "IS_NUMERIC(IDENTIFICATIVI_CATASTALI.SUB) = 0)"
        Dim NrAnomaliaUiFogPartSubNum As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrAnomaliaUiFogPartSubNum > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                Errori = "- <a href=""javascript:ApriAnomalie(9);"">Anomalia sui dati catastali (Foglio, Particella, Sub non numerici) delle UI</a>;"
            Else
                Errori &= "<br>" & "- <a href=""javascript:ApriAnomalie(9);"">Anomalia sui dati catastali (Foglio, Particella, Sub non numerici) delle UI</a>;"
            End If
            'If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "UNITA_IMMOBILIARI, " & StringaSiscom & "IDENTIFICATIVI_CATASTALI " _
                            & "WHERE IDENTIFICATIVI_CATASTALI.ID(+) = UNITA_IMMOBILIARI.ID_CATASTALE " _
                            & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                            & "AND NVL(NUM_VANI, 0) NOT IN (1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10)"
        Dim NrAnomaliaUiConsistenzaNr As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrAnomaliaUiConsistenzaNr > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                Errori = "- <a href=""javascript:ApriAnomalie(10);"">Anomalia sulla consistenza (Numero Vani) delle UI. Valori ammissibili: 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10</a>;"
            Else
                Errori &= "<br>" & "- <a href=""javascript:ApriAnomalie(10);"">Anomalia sulla consistenza (Numero Vani) delle UI. Valori ammissibili: 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, 8, 8.5, 9, 9.5, 10</a>;"
            End If
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "UNITA_IMMOBILIARI, " & StringaSiscom & "INDIRIZZI " _
                            & "WHERE INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                            & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                            & "AND ID_INDIRIZZO IS NOT NULL AND INDIRIZZI.LOCALITA IS NULL"
        Dim NrAnomaliaIndirizziLocalita As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrAnomaliaIndirizziLocalita > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- <a href=""javascript:ApriAnomalie(11);"">Anomalia sugli indirizzi delle UI (Località)</a>;"
            Else
                ErroriBloccanti &= "<br>" & "- <a href=""javascript:ApriAnomalie(11);"">Anomalia sugli indirizzi delle UI (Località)</a>;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "UNITA_IMMOBILIARI, " & StringaSiscom & "IDENTIFICATIVI_CATASTALI " _
                            & "WHERE IDENTIFICATIVI_CATASTALI.ID/*(+)*/ = UNITA_IMMOBILIARI.ID_CATASTALE " _
                            & "AND UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL' " _
                            & "AND (FOGLIO LIKE '%.%' OR FOGLIO LIKE '%,%' OR NUMERO LIKE '%,%' OR NUMERO LIKE '%,%' OR SUB LIKE '%,%' OR SUB LIKE '%,%')"
        Dim NrAnomaliaUiFogPartSubNumInt As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrAnomaliaUiFogPartSubNumInt > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- <a href=""javascript:ApriAnomalie(12);"">Anomalia sui dati catastali (Foglio, Particella, Sub non numerici interi) delle UI</a>;"
            Else
                ErroriBloccanti &= "<br>" & "- <a href=""javascript:ApriAnomalie(12);"">Anomalia sui dati catastali (Foglio, Particella, Sub non numerici interi) delle UI</a>;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "EDIFICI " _
                            & "WHERE EDIFICI.ID <> 1 AND EDIFICI.ID IN (" _
                            & "SELECT DISTINCT ID_EDIFICIO FROM " & StringaSiscom & "UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID <> 1 AND UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND COD_TIPOLOGIA = 'AL') " _
                            & "AND NVL(UNITA_NON_PROPRIETA, -1) = -1 AND EDIFICI.ID IN (SELECT DISTINCT ID_EDIFICIO FROM " & StringaSiscom & "COND_EDIFICI)"
        Dim NrAnomaliaEdificiUiNoProp As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If NrAnomaliaEdificiUiNoProp > 0 Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- <a href=""javascript:ApriAnomalieF(1);"">Anomalia Unità non in Proprietà Edifici</a>;"
            Else
                ErroriBloccanti &= "<br>" & "- <a href=""javascript:ApriAnomalieF(1);"">Anomalia Unità non in Proprietà Edifici</a>;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If
        If String.IsNullOrEmpty(Trim(Errori)) And String.IsNullOrEmpty(Trim(ErroriBloccanti)) Then
            ControlliPreElaborazione = True
        Else
            Dim ErroreDaVisualizzare As String = ""
            ErroreDaVisualizzare &= "<fieldset>"
            ErroreDaVisualizzare &= "<legend>&nbsp;&nbsp;&nbsp;<strong>Errori Bloccanti</strong>&nbsp;&nbsp;&nbsp;</legend>"
            If String.IsNullOrEmpty(Trim(ErroriBloccanti)) Then
                ErroreDaVisualizzare &= "Nessun errore bloccante. (E\'possibile continuare con l\'elaborazione)"
            Else
                ErroreDaVisualizzare &= ErroriBloccanti
            End If
            ErroreDaVisualizzare &= "</fieldset>"
            ErroreDaVisualizzare &= "<fieldset>"
            ErroreDaVisualizzare &= "<legend>&nbsp;&nbsp;&nbsp;<strong>Errori NON Bloccanti</strong>&nbsp;&nbsp;&nbsp;</legend>"
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroreDaVisualizzare &= "Nessun errore NON bloccante."
            Else
                ErroreDaVisualizzare &= Errori
            End If
            ErroreDaVisualizzare &= "</fieldset>"
            If ControlloBloccante Then
                RadWindowManager1.RadAlert(ErroreDaVisualizzare, 600, 200, "Attenzione", Nothing, Nothing)
            Else
                RadWindowManager1.RadAlert(ErroreDaVisualizzare, 600, 200, "Attenzione", "proceduraNONValidazione", Nothing)
            End If
        End If
    End Function
    Protected Sub btnConfermaElabNoValidazione_Click(sender As Object, e As System.EventArgs) Handles btnConfermaElabNoValidazione.Click
        connData = New CM.datiConnessione(par, False, False)
        Try
            connData.apri(True)
            'par.cmd.CommandText = "SELECT " & StringaSiscom & "SEQ_ARPA_ELABORAZIONI.NEXTVAL FROM DUAL"
            'Dim idElaborazione As String = par.cmd.ExecuteScalar
            par.cmd.CommandText = "SELECT VALORE FROM " & StringaSiscom & "ARPA_PARAMETRI WHERE ID = 1"
            CodFiscEnteProp = par.IfNull(par.cmd.ExecuteScalar, "")
            AvviaElaborazione("0")
            'par.cmd.CommandText = "INSERT INTO " & StringaSiscom & "ARPA_ELABORAZIONI (ID, ANNO, ID_OPERATORE, DATA_ORA, CF_ENTE_PROPRIETARIO, FL_VALIDAZIONE) VALUES " _
            '                    & "(" & idElaborazione & ", '" & ddlAnnoElaborazione.SelectedItem.Text.ToString & "', " & Session.Item("ID_OPERATORE").ToString & ", '" & Format(Now, "yyyyMMddHHmmss") & "', " & par.insDbValue(CodFiscEnteProp, True) & ", 0)"
            'par.cmd.ExecuteNonQuery()
            'If CreaElaborazione(idElaborazione) Then
            '    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Elaborazione Completata');", True)
            '    connData.chiudi(True)
            '    CaricaAnniElaborazioni()
            '    ElaborazioneEsistente()
            'Else
            '    connData.chiudi(False)
            'End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_HomePage - HomePage_Master - btnConfermaElabNoValidazione_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub NuovaElaborazioneConfermaOA(ByVal Conferma As Boolean)
        Try
            connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)
            par.cmd.CommandText = "SELECT COUNT(*) FROM " & StringaSiscom & "ARPA_PROCEDURE WHERE ESITO = 0"
            Dim NrElaborazioni As Integer = par.cmd.ExecuteScalar
            If NrElaborazioni > 0 Then
                connData.chiudi(False)
                RadNotificationNote.Text = "E' stata già avviata una Procedura! Attendere il termine."
                RadNotificationNote.Show()
                Exit Sub
            End If
            par.cmd.CommandText = "INSERT INTO " & StringaSiscom & "OA_LOG (DATA_ORA, ID_OPERATORE, DESCRIZIONE) VALUES " _
                                & "('" & Format(Now, "yyyyMMddHHmmss") & "', " & Session.Item("ID_OPERATORE").ToString & ", 'Nuova Elaborazione Anno: " & Format(Now, "yyyy") & "')"
            par.cmd.ExecuteNonQuery()
            Dim Continua As Boolean = True
            If Continua = True Then
                If ControlliPreElaborazioneOA() Then
                    AvviaElaborazioneOA("1")
                Else
                    connData.chiudi(False)
                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_HomePage - HomePage_Master - NuovaElaborazioneConferma - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Sub AvviaElaborazioneOA(ByVal Validazione As String)
        par.cmd.CommandText = "SELECT " & StringaSiscom & "SEQ_OA_ELABORAZIONI.NEXTVAL FROM DUAL"
        Dim idElaborazione As String = par.cmd.ExecuteScalar
        par.cmd.CommandText = "INSERT INTO " & StringaSiscom & "OA_ELABORAZIONI (ID, ANNO, ID_OPERATORE, DATA_ORA, CF_ENTE_PROPRIETARIO, FL_VALIDAZIONE) VALUES " _
                            & "(" & idElaborazione & ", '" & Format(Now, "yyyy") & "', " & Session.Item("ID_OPERATORE").ToString & ", '" & Format(Now, "yyyyMMddHHmmss") & "', " & par.insDbValue(CodFiscEnteProp, True) & ", " & Validazione & ")"
        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = "SELECT " & StringaSiscom & "SEQ_ARPA_PROCEDURE.NEXTVAL FROM DUAL"
        Dim idProcedura As String = par.cmd.ExecuteScalar
        par.cmd.CommandText = "INSERT INTO " & StringaSiscom & "ARPA_PROCEDURE (ID, ID_OPERATORE, DATA_ORA_INIZIO, DATA_ORA_FINE, ESITO, ERRORE, " _
                            & "PARZIALE, TOTALE, PARAMETRI, ID_TIPO) VALUES " _
                            & "(" & idProcedura & ", " & Session.Item("ID_OPERATORE").ToString & ", '" & Format(Now, "yyyyMMddHHmmss") & "', '', 0, '', " _
                            & "0, 100, '" & idElaborazione & ";" & cmbAzione.SelectedValue & ";', 3)"
        par.cmd.ExecuteNonQuery()
        connData.chiudi(True)
        connData.apri(False)
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
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "msg", "alert('Elaborazione avviata correttamente!');", True)
        Catch ex As Exception
            par.cmd.CommandText = "UPDATE " & StringaSiscom & "ARPA_PROCEDURE SET ESITO = 2, DATA_ORA_FINE = '" & Format(Now, "yyyyMMddHHmmss") & "', ERRORE = 'Procedura non Avviata' WHERE ID = " & idElaborazione
            par.cmd.ExecuteNonQuery()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante l\'avvio dell\'Elaborazione!');", True)
        End Try
        connData.chiudi(False)
    End Sub

    Private Sub btnAvviaElaborazioneOA_Click(sender As Object, e As EventArgs) Handles btnAvviaElaborazioneOA.Click
        NuovaElaborazioneConfermaOA(False)
    End Sub
    Private Function ControlliPreElaborazioneOA() As Boolean
        ControlliPreElaborazioneOA = False
        Dim ControlloBloccante As Boolean = False
        Dim Errori As String = ""
        Dim ErroriBloccanti As String = ""
        par.cmd.CommandText = "SELECT VALORE FROM " & StringaSiscom & "ARPA_PARAMETRI WHERE ID = 1"
        CodFiscEnteProp = par.IfNull(par.cmd.ExecuteScalar, "")
        If String.IsNullOrEmpty(Trim(CodFiscEnteProp)) Then
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroriBloccanti = "- Definire il Codice Fiscale Ente Proprietario;"
            Else
                ErroriBloccanti &= "<br>" & "- Definire il Codice Fiscale Ente Proprietario;"
            End If
            If ControlloBloccante = False Then ControlloBloccante = True
        End If

        If String.IsNullOrEmpty(Trim(Errori)) And String.IsNullOrEmpty(Trim(ErroriBloccanti)) Then
            ControlliPreElaborazioneOA = True
        Else
            Dim ErroreDaVisualizzare As String = ""
            ErroreDaVisualizzare &= "<fieldset>"
            ErroreDaVisualizzare &= "<legend>&nbsp;&nbsp;&nbsp;<strong>Errori Bloccanti</strong>&nbsp;&nbsp;&nbsp;</legend>"
            If String.IsNullOrEmpty(Trim(ErroriBloccanti)) Then
                ErroreDaVisualizzare &= "Nessun errore bloccante. (E\'possibile continuare con l\'elaborazione)"
            Else
                ErroreDaVisualizzare &= ErroriBloccanti
            End If
            ErroreDaVisualizzare &= "</fieldset>"
            ErroreDaVisualizzare &= "<fieldset>"
            ErroreDaVisualizzare &= "<legend>&nbsp;&nbsp;&nbsp;<strong>Errori NON Bloccanti</strong>&nbsp;&nbsp;&nbsp;</legend>"
            If String.IsNullOrEmpty(Trim(Errori)) Then
                ErroreDaVisualizzare &= "Nessun errore NON bloccante."
            Else
                ErroreDaVisualizzare &= Errori
            End If
            ErroreDaVisualizzare &= "</fieldset>"
            If ControlloBloccante Then
                RadWindowManager1.RadAlert(ErroreDaVisualizzare, 600, 200, "Attenzione", Nothing, Nothing)
            Else
                RadWindowManager1.RadAlert(ErroreDaVisualizzare, 600, 200, "Attenzione", "proceduraNONValidazione", Nothing)
            End If
        End If


    End Function
    Private Sub NascondiMenu()
        If Session.Item("RU_GESTIONE_OA") = "0" Then
            par.RimuoviNodoMenuTelerik(NavigationMenu, "SepArpaOA")
            par.RimuoviNodoMenuTelerik(NavigationMenu, "ElaborazioneOA")
        End If
    End Sub
End Class

