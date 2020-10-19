Imports System.IO

Partial Class SIRAPER_Siraper
    Inherits PageSetIdMode
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim xls As New ExcelSiSol

    Public Property vIdConnessione() As String
        Get
            If Not (ViewState("vIdConnessione") Is Nothing) Then
                Return CStr(ViewState("vIdConnessione"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("vIdConnessione") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        If IsNothing(Session.Item("SIRAPER" & vIdConnessione)) Then
            Me.connData = New CM.datiConnessione(par, False, False)
        Else
            Me.connData = CType(HttpContext.Current.Session.Item("SIRAPER" & vIdConnessione), CM.datiConnessione)
            par.cmd = par.OracleConn.CreateCommand()
        End If
        If Not IsPostBack Then
            SettaControlModifiche(Me)
            RiempiCampi()
            If Not String.IsNullOrEmpty(Request.QueryString("ID")) Then
                idSiraper.Value = Request.QueryString("ID")
                If Not IsNothing(Request.QueryString("IND")) Then
                    If Request.QueryString("IND").ToString = "0" Then
                        btnIndietro.Visible = False
                    End If
                End If
                ApriRicerca()
            Else
                NuovaElaborazione()
            End If
        End If
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "div", "if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
    End Sub
#Region "Inizializzazione"
    Private Sub RiempiCampi()
        Try
            Me.txtDataRiferimento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtPartitaIva.Attributes.Add("onkeyup", "javascript:valid(this,'codice');")
            par.caricaComboBox("SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_ENTE_SIRAPER", ddlTipoEnte, "COD", "DESCRIZIONE", True)
            txtCodIstatProg.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtallnuovierpsoc.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtallnuovierpmod.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtallnuovinonerp.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtallacqerpsoc.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtallacqerpmod.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtallacqnonerp.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtallrsterpsoc.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtallrsterpmod.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtallrsterpnonerp.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - NuovaElaborazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub FrmSolaLettura(ByVal Nowait As Boolean)
        Try
            If Nowait = True Then
                btnElabora.Visible = False
                btnLastSiraper.Visible = False
                btnSalva.Visible = False
                If FileCreato.Value = 0 Then
                    btnCreaFile.Visible = False
                End If
            End If
            txtSiglaEnte.ReadOnly = True
            ddlTipoEnte.Enabled = False
            txtDataRiferimento.Enabled = False
            txtCodFiscale.ReadOnly = True
            txtPartitaIva.ReadOnly = True
            txtRagioneSociale.ReadOnly = True
            txtCodIstatProg.ReadOnly = True
            txtallnuovierpsoc.ReadOnly = True
            txtallnuovierpmod.ReadOnly = True
            txtallnuovinonerp.ReadOnly = True
            txtallacqerpsoc.ReadOnly = True
            txtallacqerpmod.ReadOnly = True
            txtallacqnonerp.ReadOnly = True
            txtallrsterpsoc.ReadOnly = True
            txtallrsterpmod.ReadOnly = True
            txtallrsterpnonerp.ReadOnly = True
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - FrmSolaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            End If
        Next
    End Sub
#End Region
#Region "ElaborazioneSiraper"
    Private Sub NuovaElaborazione()
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'CODICE ISTAT'"
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                If Not String.IsNullOrEmpty(par.IfNull(MyReader("VALORE"), "")) Then
                    txtSiglaEnte.Text = "C_" & par.IfNull(MyReader("VALORE"), "")
                Else
                    txtSiglaEnte.Text = "AL"
                End If
            Else
                txtSiglaEnte.Text = "AL"
            End If
            MyReader.Close()
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'RAGIONE SOCIALE'"
            txtRagioneSociale.Text = par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'CODICE FISCALE'"
            txtCodFiscale.Text = par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'PARTITA IVA'"
            txtPartitaIva.Text = par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'SIGLA ENTE'"
            txtSiglaEnte.Text = par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'TIPO ENTE'"
            ddlTipoEnte.SelectedValue = par.IfNull(par.cmd.ExecuteScalar, "-1")
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.SIRAPER_GESTIONE WHERE DESCRIZIONE = 'TIPO AMMINISTRAZIONE'"
            Dim TipoAmministrazione As String = par.IfNull(par.cmd.ExecuteScalar, "")
            connData.chiudi(False)
            txtDataRiferimento.Text = Format(Now, "dd/MM/yyyy")
            btnElabora.Visible = False
            btnLastSiraper.Visible = False
            btnEsporta.Visible = False
            btnCreaFile.Visible = False
            btnEventi.Visible = False
            btnIndietro.Visible = False
            If String.IsNullOrEmpty(Trim(TipoAmministrazione)) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Definire tutti i parametri in gestione!');location.href='Gestione.aspx';", True)
                Exit Sub
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - NuovaElaborazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function CercaProcedura() As Boolean
        CercaProcedura = False
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIRAPER_PROCEDURE WHERE ESITO = 0"
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.HasRows Then
                CercaProcedura = True
            End If
            MyReader.Close()
            connData.chiudi(False)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - CercaProcedura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Sub ApriRicerca()
        Try
            If CercaProcedura() = True Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('E\' in corso una procedura di elaborazione dati! Riprovare più tardi.');document.getElementById('noClose').value='0';location.href='Home.aspx';Procedure();", True)
                Exit Sub
            End If
            par.cmd = connData.apri(True)
            'par.cmd.Transaction = connData.Transazione
            vIdConnessione = Format(Now, "yyyyMMddHHmmss")
            idConnessione.Value = vIdConnessione
            HttpContext.Current.Session.Add("SIRAPER" & vIdConnessione, connData)
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIRAPER WHERE ID = " & idSiraper.Value & " FOR UPDATE NOWAIT"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                SettaCampiRicerca(lettore)
            End If
            lettore.Close()
        Catch ex1 As Oracle.DataAccess.Client.OracleException
            If ex1.Number = 54 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Elaborazione già aperta da un altro utente! Non sarà possibile apportarvi modifiche!');", True)
                SLE.Value = 1
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIRAPER WHERE ID = " & idSiraper.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    SettaCampiRicerca(lettore)
                    FrmSolaLettura(True)
                End If
                lettore.Close()
            Else
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
                Session.Add("ERRORE", "Provenienza: Siraper - ApriRicerca - " & ex1.Message)
                Response.Redirect("../Errore.aspx", False)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - ApriRicerca - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SettaCampiRicerca(ByVal lettore As Oracle.DataAccess.Client.OracleDataReader)
        Try
            Me.txtSiglaEnte.Text = par.IfNull(lettore("SIGLA_ENTE"), "")
            Me.ddlTipoEnte.SelectedValue = par.IfNull(lettore("TIPO_ENTE"), "-1")
            Me.txtDataRiferimento.Text = par.FormattaData(par.IfNull(lettore("DATA_RIFERIMENTO"), ""))
            Me.txtAnnoRif.Text = par.IfNull(lettore("ANNO_RIFERIMENTO"), "")
            Me.txtCodFiscale.Text = par.IfNull(lettore("COD_FISCALE_ENTE"), "")
            Me.txtPartitaIva.Text = par.IfNull(lettore("P_IVA_ENTE"), "")
            Me.txtRagioneSociale.Text = par.IfNull(lettore("RAG_SOCIALE"), "")
            Me.idSiraperVersione.Value = par.IfNull(lettore("ID_SIRAPER_VERSIONE"), "1")
            Elaborazione.Value = par.IfNull(lettore("ELABORATO"), 0)
            Controllo.Value = par.IfNull(lettore("ESITO_CONTROLLO"), 0)
            If Elaborazione.Value <> 1 Then
                btnCreaFile.Visible = False
                btnLastSiraper.Visible = False
            End If
            If Not String.IsNullOrEmpty(par.IfNull(lettore("DATA_TRASMISSIONE"), "")) Then
                btnElabora.Visible = False
                btnSalva.Visible = False
                FrmSolaLettura(False)
                SLE.Value = 1
            End If
            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SIRAPER_FILE WHERE ID_SIRAPER = " & idSiraper.Value
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.HasRows Then
                FileCreato.Value = 1
                btnCreaFile.Text = "Scarica File"
                btnElabora.Visible = False
                btnLastSiraper.Visible = False
            End If
            MyReader.Close()
            txtDataRiferimento.Enabled = False
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - SettaCampiRicerca - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
#End Region
#Region "Salva"
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            If idSiraper.Value = -1 Then
                If ControlliNuovaElaborazione() Then
                    If SalvaNuovaElaborazione() Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Operazione Completata!');", True)
                        frmModify.Value = 0
                    End If
                End If
            Else
                If ControlliNuovaElaborazione() Then
                    If UpdateElaborazione() Then
                        If SalvaDataGrid() Then
                            If Not IsNothing(Me.connData) Then
                                Me.connData.RiempiPar(par)
                                'par.cmd.Transaction = connData.Transazione
                            End If
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SIRAPER_EVENTI (ID_SIRAPER, ID_OPERATORE, DATA_ORA, COD_EVENTO, DESCRIZIONE) VALUES " _
                                                & "(" & idSiraper.Value & ", " & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'S02', " _
                                                & "'MODIFICA DATI ELABORAZIONE SIRAPER')"
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi(True)
                            connData.apri(True)
                            'par.cmd.Transaction = connData.Transazione
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIRAPER WHERE ID = " & idSiraper.Value & " FOR UPDATE NOWAIT"
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            lettore.Close()
                            frmModify.Value = 0
                        End If
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function ControlliNuovaElaborazione() As Boolean
        Try
            ControlliNuovaElaborazione = False
            If String.IsNullOrEmpty(txtSiglaEnte.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Inserire la corretta Sigla dell\'Ente Proprietario!');", True)
                Exit Function
            End If
            If ddlTipoEnte.SelectedValue.ToString = "-1" Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Selezionare la Tipologia dell\'Ente!');", True)
                Exit Function
            End If
            If String.IsNullOrEmpty(txtDataRiferimento.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Inserire la data di Riferimento!');", True)
                txtDataRiferimento.Enabled = True
                Exit Function
            Else
                txtAnnoRif.Text = (Right(txtDataRiferimento.Text, 4) - 1)
            End If
            If String.IsNullOrEmpty(txtCodFiscale.Text) And String.IsNullOrEmpty(txtPartitaIva.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Inserire il Codice Fiscale dell\'Ente o la Partita Iva dell\'Ente!');", True)
                Exit Function
            Else
                If Not String.IsNullOrEmpty(txtCodFiscale.Text) And Not String.IsNullOrEmpty(txtPartitaIva.Text) Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Inserire solo il Codice Fiscale o la Partita Iva dell\'Ente!');", True)
                    Exit Function
                End If
                If Not String.IsNullOrEmpty(txtCodFiscale.Text) Then
                    If Len(txtCodFiscale.Text) <> 11 And Len(txtCodFiscale.Text) <> 16 Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Inserire il Codice Fiscale corretto dell\'Ente!');", True)
                        Exit Function
                    End If
                End If
                If Not String.IsNullOrEmpty(txtPartitaIva.Text) Then
                    If Len(txtPartitaIva.Text) <> 11 Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Inserire la Partita Iva corretta dell\'Ente!');", True)
                        Exit Function
                    End If
                End If
            End If
            If String.IsNullOrEmpty(txtRagioneSociale.Text) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Inserire la Ragione Sociale dell\'Ente!');", True)
                Exit Function
            End If
            txtDataRiferimento.Enabled = False
            ControlliNuovaElaborazione = True
        Catch ex As Exception
            ControlliNuovaElaborazione = False
        End Try
    End Function
    Private Function SalvaNuovaElaborazione() As Boolean
        Try
            SalvaNuovaElaborazione = False
            connData.apri(False)
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_SIRAPER.NEXTVAL FROM DUAL"
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                idSiraper.Value = par.IfNull(MyReader(0), "-1")
            End If
            MyReader.Close()
            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SIRAPER_VERSIONI WHERE FL_ATTIVA = 1"
            idSiraperVersione.Value = par.IfNull(par.cmd.ExecuteScalar, "1")
            If idSiraper.Value <> -1 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SIRAPER (ID, SIGLA_ENTE, TIPO_ENTE, COD_FISCALE_ENTE, P_IVA_ENTE, RAG_SOCIALE, DATA_RIFERIMENTO, ANNO_RIFERIMENTO, ID_SIRAPER_VERSIONE) VALUES " _
                                    & "(" & idSiraper.Value & ", '" & par.PulisciStrSql(txtSiglaEnte.Text.ToUpper) & "', " & ddlTipoEnte.SelectedValue & ", '" & par.PulisciStrSql(txtCodFiscale.Text.ToUpper) & "', '" & par.PulisciStrSql(txtPartitaIva.Text) & "', " _
                                    & "'" & par.PulisciStrSql(txtRagioneSociale.Text.ToUpper) & "', '" & par.AggiustaData(txtDataRiferimento.Text) & "', '" & par.PulisciStrSql(txtAnnoRif.Text) & "', '" & par.PulisciStrSql(idSiraperVersione.Value) & "')"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SIRAPER_EVENTI (ID_SIRAPER, ID_OPERATORE, DATA_ORA, COD_EVENTO, DESCRIZIONE) VALUES " _
                                    & "(" & idSiraper.Value & ", " & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'S01', " _
                                    & "'CREAZIONE NUOVA ELABORAZIONE DATI SIRAPER SIGLA " & par.PulisciStrSql(txtSiglaEnte.Text.ToUpper) & ", ENTE: " & par.PulisciStrSql(ddlTipoEnte.SelectedItem.Text) & ", DATA RIFERIMENTO: " & par.PulisciStrSql(txtDataRiferimento.Text) & ", ANNO RIFERIMENTO: " & par.PulisciStrSql(txtAnnoRif.Text) & "')"
                par.cmd.ExecuteNonQuery()
                btnEsporta.Visible = True
                btnElabora.Visible = True
                frmModify.Value = 0
            Else
                connData.chiudi(False)
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore nella Creazione dell\'Elaborazione Siraper!');", True)
                Exit Function
            End If
            connData.chiudi(False)
            par.cmd = connData.apri(True)
            'par.cmd.Transaction = connData.Transazione
            vIdConnessione = Format(Now, "yyyyMMddHHmmss")
            idConnessione.Value = vIdConnessione
            HttpContext.Current.Session.Add("SIRAPER" & vIdConnessione, connData)
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIRAPER WHERE ID = " & idSiraper.Value & " FOR UPDATE NOWAIT"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            lettore.Close()
            btnEventi.Visible = True
            SalvaNuovaElaborazione = True
        Catch ex As Exception
            SalvaNuovaElaborazione = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - SalvaNuovaElaborazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function UpdateElaborazione() As Boolean
        Try
            UpdateElaborazione = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER SET SIGLA_ENTE = '" & par.PulisciStrSql(txtSiglaEnte.Text.ToUpper) & "', " _
                                & "TIPO_ENTE = " & ddlTipoEnte.SelectedValue & ", " _
                                & "COD_FISCALE_ENTE = '" & par.PulisciStrSql(txtCodFiscale.Text.ToUpper) & "', " _
                                & "P_IVA_ENTE = '" & par.PulisciStrSql(txtPartitaIva.Text) & "', " _
                                & "RAG_SOCIALE = '" & par.PulisciStrSql(txtRagioneSociale.Text.ToUpper) & "', " _
                                & "DATA_RIFERIMENTO = '" & par.AggiustaData(txtDataRiferimento.Text) & "', " _
                                & "ANNO_RIFERIMENTO = '" & par.PulisciStrSql(txtAnnoRif.Text) & "' " _
                                & "WHERE ID = " & idSiraper.Value
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.SIR_PROGRAMMAZIONE SET COD_COMUNE = '" & par.PulisciStrSql(txtCodIstatProg.Text.ToUpper) & "', " _
                                & "NEW_ALL_ERP = " & par.IfEmpty(txtallnuovierpsoc.Text.ToUpper, "null") & ", " _
                                & "NEW_ALL_ERP_MODERATO = " & par.IfEmpty(txtallnuovierpmod.Text.ToUpper, "null") & ", " _
                                & "NEW_ALL_NON_ERP = " & par.IfEmpty(txtallnuovinonerp.Text.ToUpper, "null") & ", " _
                                & "ACQ_ALL_ERP = " & par.IfEmpty(txtallacqerpsoc.Text.ToUpper, "null") & ", " _
                                & "ACQ_ALL_ERP_MODERATO = " & par.IfEmpty(txtallacqerpmod.Text.ToUpper, "null") & ", " _
                                & "ACQ_ALL_NON_ERP = " & par.IfEmpty(txtallacqnonerp.Text.ToUpper, "null") & ", " _
                                & "RIST_ALL_ERP = " & par.IfEmpty(txtallrsterpsoc.Text.ToUpper, "null") & ", " _
                                & "RIST_ALL_ERP_MODERATO = " & par.IfEmpty(txtallrsterpmod.Text.ToUpper, "null") & ", " _
                                & "RIST_ALL_NON_ERP = " & par.IfEmpty(txtallrsterpnonerp.Text.ToUpper, "null") & " " _
                                & "WHERE ID_SIRAPER = " & idSiraper.Value
            par.cmd.ExecuteNonQuery()
            UpdateElaborazione = True
        Catch ex As Exception
            UpdateElaborazione = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - UpdateElaborazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function SalvaDataGrid() As Boolean
        Try
            SalvaDataGrid = False
            Dim Messaggio As String = ""
            Tab_Fabbricato1.AggiustaCompSessioneFabbricato()
            Tab_Inquilino1.AggiustaCompSessioneInquilino()
            Dim ProseguiControllo As Boolean = True
            'Dim ProseguiControllo As Boolean = False
            'Select Case idSiraperVersione.Value
            '    Case "1"
            '        ProseguiControllo = ControlliUpdateDataGrid_2013(Messaggio)
            '    Case "2"
            '        ProseguiControllo = ControlliUpdateDataGrid_2015(Messaggio)
            'End Select
            If ProseguiControllo Then
                'If SalvataggioDataGrid() Then
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                    'par.cmd.Transaction = connData.Transazione
                End If
                If String.IsNullOrEmpty(Messaggio) Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Operazione Completata!');", True)
                    'If Elaborazione.Value.ToString = "1" Then
                    '    par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER SET ESITO_CONTROLLO = 1 WHERE ID = " & idSiraper.Value
                    '    par.cmd.ExecuteNonQuery()
                    '    Controllo.Value = 1
                    '    btnCreaFile.Visible = True
                    'Else
                    '    par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER SET ESITO_CONTROLLO = 0 WHERE ID = " & idSiraper.Value
                    '    par.cmd.ExecuteNonQuery()
                    '    Controllo.Value = 0
                    'End If
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('I dati sono stati salvati ma non sono stati superati i controlli di validità!\n" & Messaggio.Replace("'", "\'") & "');", True)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER SET ESITO_CONTROLLO = 0 WHERE ID = " & idSiraper.Value
                    par.cmd.ExecuteNonQuery()
                    Controllo.Value = 0
                End If
                'Else
                'Exit Function
                'End If
            Else
                Exit Function
            End If
            SalvaDataGrid = True
        Catch ex As Exception
            SalvaDataGrid = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - SalvaDataGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function ControlliUpdateDataGrid_2013(ByRef Messaggio As String) As Boolean
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader
            ControlliUpdateDataGrid_2013 = True
            Dim dt As New Data.DataTable
            Dim row As Data.DataRow
            'CONTROLLO FABBRICATO
            dt = Session.Item("dtFabbricato")
            If Not IsNothing(dt) Then
                For Each row In dt.Rows
                    If par.IfNull(row.Item("GESTIONE"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire la tipologia di Gestione dell'Edificio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Fabbricato, CType(Me.Tab_Fabbricato1.FindControl("dgvFabbricati"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If par.IfNull(row.Item("UBICAZIONE"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire la tipologia di Ubicazione dell'Edificio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Fabbricato, CType(Me.Tab_Fabbricato1.FindControl("dgvFabbricati"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If par.IfNull(row.Item("COEFF_UBICAZIONE"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire il coefficente di Ubicazione dell'Edificio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Fabbricato, CType(Me.Tab_Fabbricato1.FindControl("dgvFabbricati"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                Next
            End If
            'CONTROLLO ALLOGGIO
            dt = Session.Item("dtAlloggio")
            If Not IsNothing(dt) Then
                For Each row In dt.Rows
                    If String.IsNullOrEmpty(par.IfNull(row.Item("CODICE_MIR"), "")) Then
                        Messaggio = "Inserire il Codice Mir di 7 cifre dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If Len(Trim(par.IfNull(row.Item("CODICE_MIR"), ""))) <> 7 Then
                            Messaggio = "Inserire il Codice Mir di 7 cifre dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If par.IfNull(row.Item("ALL_DISM_CART"), "") = "DISMESSO" Or par.IfNull(row.Item("ALL_DISM_CART"), "") = "CARTOLARIZZATO" Then
                        If String.IsNullOrEmpty(par.IfNull(row.Item("PROV_DISM_CART"), "")) Then
                            Messaggio = "Inserire i Proventi di Dismissione o Cartolarizzazione dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If par.IfNull(row.Item("CATEGORIA_CATASTALE"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire la Categoria Catastale dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("RENDITA_CATASTALE"), "")) Then
                        Messaggio = "Inserire la Rendita Catastale dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If par.IfNull(row.Item("CANONE_SOCIALE"), 0) = 1 Then 'CONTROLLO SE L'UNITA E A CANONE SOCIALE
                        If par.IfNull(row.Item("COEFF_CLASSE_DEMOGRAFICA_LR"), "-1").ToString = "-1" Then
                            Messaggio = "Inserire la Coefficente della Classe Demografica L.R. n°27/2007 dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                        If String.IsNullOrEmpty(par.IfNull(row.Item("ANNO_RISTRUTTURAZIONE"), "")) Then
                            Messaggio = "Inserire l'anno di ultimazione o recupero ristrutturazione dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        Else
                            If Len(par.IfNull(row.Item("ANNO_RISTRUTTURAZIONE"), "")) <> 4 Then
                                Messaggio = "Inserire l'anno corretto di ultimazione o recupero ristrutturazione dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                                TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                                Exit Function
                            End If
                            par.cmd.CommandText = "SELECT DATA_COSTRUZIONE FROM EDIFICI WHERE ID = (SELECT ID_EDIFICIO FROM UNITA_IMMOBILIARI WHERE ID = " & par.IfNull(row.Item("ID"), "-1") & ")"
                            MyReader = par.cmd.ExecuteReader
                            If MyReader.Read Then
                                If par.IfNull(row.Item("ANNO_RISTRUTTURAZIONE"), "") < Left(par.IfNull(MyReader("DATA_COSTRUZIONE"), "1000"), 4) Then
                                    Messaggio = "L'anno di ultimazione o recupero ristrutturazione dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può essere minore di quello di costruzione"
                                    TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                                    Exit Function
                                End If
                            End If
                            MyReader.Close()
                            If par.IfNull(row.Item("ANNO_RISTRUTTURAZIONE"), "") > Format(Now, "yyyy") Then
                                Messaggio = "L'anno di ultimazione o recupero ristrutturazione dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può essere maggiore di quello in corso"
                                TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                                Exit Function
                            End If
                        End If
                        If par.IfNull(row.Item("STATO_CONS_ALL"), "-1").ToString = "-1" Then
                            Messaggio = "Inserire lo stato di Conservazione dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può essere maggiore di quello in corso"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                        If String.IsNullOrEmpty(par.IfNull(row.Item("PER_ISTAT_LEGGE27"), "")) Then
                            Messaggio = "Inserire la percentuale di aggiornamento ISTAT Legge 27/2007 dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                        If par.IfNull(row.Item("ABBATTIMENTO_CANONE"), "-1").ToString = "-1" Then
                            Messaggio = "Inserire l'abbatimento del Canone dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                        If par.IfNull(row.Item("SOVRAPREZZO_DECADENZA"), "-1").ToString = "-1" Then
                            Messaggio = "Inserire il sovraprezzo per decadenza dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                        If String.IsNullOrEmpty(par.IfNull(row.Item("PERC_AGG_AREA_DEC"), "")) Then
                            Messaggio = "Inserire la percentuale aggiuntiva per area decadenza dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                        If par.IfNull(row.Item("INV_SOCIALE"), "-1").ToString = "-1" Then
                            Messaggio = "Inserire la invalidità sociale dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                        If par.IfNull(row.Item("AREA_APPARTENENZA"), "-1").ToString = "4" Then
                            If String.IsNullOrEmpty(par.IfNull(row.Item("ISEE_PRON_DECADENZA"), "")) Then
                                Messaggio = "Inserire l'ISEE per pronuncia decadenza dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                                TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                                Exit Function
                            End If
                        End If
                    End If
                    If par.IfNull(row.Item("CANONE_SOCIALE"), 0) = 2 Then 'CONTROLLO SE L'UNITA E A CANONE MODERATO
                        If par.IfNull(row.Item("INV_SOCIALE"), "-1").ToString = "-1" Then
                            Messaggio = "Inserire la invalidità sociale dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                        If par.IfNull(row.Item("AREA_APPARTENENZA"), "-1").ToString = "4" Then
                            If String.IsNullOrEmpty(par.IfNull(row.Item("ISEE_PRON_DECADENZA"), "")) Then
                                Messaggio = "Inserire l'ISEE per pronuncia decadenza dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                                TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                                Exit Function
                            End If
                        End If
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("NUMERO_STANZE"), "")) Then
                        Messaggio = "Inserire il numero di Stanze dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If par.IfNull(row.Item("TIPO_INTERVENTO"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire il tipo di Intervento dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUP_UTILE_ALLOGGIO"), "")) Then
                        Messaggio = "Inserire la superficie utile dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If par.IfNull(row.Item("SUP_UTILE_ALLOGGIO"), 0) < 14 Or par.IfNull(row.Item("SUP_UTILE_ALLOGGIO"), 0) > 300 Then
                            Messaggio = "Inserire la superficie utile dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 14mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUP_CANTINE_SOFF"), "")) Then
                        Messaggio = "Inserire la superficie delle catine o soffitte dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If par.IfNull(row.Item("SUP_CANTINE_SOFF"), 0) < 0 Or par.IfNull(row.Item("SUP_CANTINE_SOFF"), 0) > 300 Then
                            Messaggio = "Inserire la superficie delle catine o soffitte dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUP_BALCONI"), "")) Then
                        Messaggio = "Inserire la superficie dei balconi dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If par.IfNull(row.Item("SUP_BALCONI"), 0) < 0 Or par.IfNull(row.Item("SUP_BALCONI"), 0) > 300 Then
                            Messaggio = "Inserire la superficie dei balconi dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUP_AREA_PRIVATA"), "")) Then
                        Messaggio = "Inserire la superficie dell'Area Privata dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If par.IfNull(row.Item("SUP_AREA_PRIVATA"), 0) < 0 Or par.IfNull(row.Item("SUP_AREA_PRIVATA"), 0) > 300 Then
                            Messaggio = "Inserire la superficie dell'Area Privata dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUP_VERDE_COND"), "")) Then
                        Messaggio = "Inserire la superficie Verde Condominiale dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If par.IfNull(row.Item("SUP_VERDE_COND"), 0) < 0 Or par.IfNull(row.Item("SUP_VERDE_COND"), 0) > 300 Then
                            Messaggio = "Inserire la superficie Verde Condominiale dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUP_BOX"), "")) Then
                        Messaggio = "Inserire la superficie dei Box dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If par.IfNull(row.Item("SUP_BOX"), 0) < 0 Or par.IfNull(row.Item("SUP_BOX"), 0) > 300 Then
                            Messaggio = "Inserire la superficie dei Box dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUP_POSTO_AUTO"), "")) Then
                        Messaggio = "Inserire la superficie dei Posto Auto dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If par.IfNull(row.Item("SUP_POSTO_AUTO"), 0) < 0 Or par.IfNull(row.Item("SUP_POSTO_AUTO"), 0) > 300 Then
                            Messaggio = "Inserire la superficie dei Posto Auto dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUP_PERTINENZE"), "")) Then
                        Messaggio = "Inserire la superficie delle Pertinenze dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If par.IfNull(row.Item("SUP_PERTINENZE"), 0) < 0 Or par.IfNull(row.Item("SUP_PERTINENZE"), 0) > 300 Then
                            Messaggio = "Inserire la superficie delle Pertinenze dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUP_CONVENZIONALE_ANTE"), "")) Then
                        Messaggio = "Inserire la superficie Convenzionale Ante Legem dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUP_CONVENZIONALE_LR"), "")) Then
                        Messaggio = "Inserire la superficie Convenzionale L.R. n°27/2007 dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("NUM_BOX_CONTR_SEP"), "")) Then
                        If String.IsNullOrEmpty(par.IfNull(row.Item("CANONE_BOX_CONTR_SEP"), "")) Then
                            Messaggio = "Inserire il canone dei Box a contratto separato dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If par.IfNull(row.Item("CONTAB_UNICA"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire la contabilità unica dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If par.IfNull(row.Item("CONTAB_UNICA"), "2").ToString = "1" Then
                            Messaggio = "Inserire il gettito per la contabilità unica dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("PSE"), "")) Then
                        Messaggio = "Inserire il PSE dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If par.IfNull(row.Item("PSE"), 0) < 1 Then
                            Messaggio = "Inserire il PSE maggiore/uguale a 1 dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("DATA_CALCOLO_ISEE"), "")) And Not String.IsNullOrEmpty(par.IfNull(row.Item("CONTRATTO"), "")) Then
                        Messaggio = "Data Calcolo ISEE mancante per l'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If ((par.IfNull(row.Item("ISE_ERP"), 0) = 0 Or par.IfNull(row.Item("ISEE_ERP"), 0) = 0 Or par.IfNull(row.Item("ISE_ERP_ASS"), 0) = 0 Or par.IfNull(row.Item("ISEE_ERP_ASS"), 0) = 0)) And row.Item("AREA_APPARTENENZA").ToString = "DECADENZA" Then
                        If String.IsNullOrEmpty(par.IfNull(row.Item("ISEE_PRON_DECADENZA"), "")) Then
                            Messaggio = "ISEE per Pronuncia Decadenza mancante per l'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                Next
            End If
            'CONTROLLO INQUILINO
            dt = Session.Item("dtInquilino")
            If Not IsNothing(dt) Then
                For Each row In dt.Rows
                    If par.IfNull(row.Item("FISC_A_CARICO"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire se è fiscalmente a carico dell'intestatario l'Inquilino n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Inquilino, CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    par.cmd.CommandText = "SELECT ID FROM SIR_INQUILINO_PATR_MOBILIARE WHERE ID_INQUILINO = " & par.IfNull(row.Item("ID"), "-1") & " AND ID_SIRAPER = " & idSiraper.Value & " " _
                                        & "AND (ANNO_RIFERIMENTO IS NULL OR CODICE_GESTORE IS NULL OR DENOMINAZIONE_GESTORE IS NULL OR IMPORTO_PATRIMONIO IS NULL OR VALORE_PATRIMONIO IS NULL)"
                    MyReader = par.cmd.ExecuteReader
                    If MyReader.HasRows Then
                        Messaggio = "I dati del patrimonio mobiliare dell'Inquilino n° " & par.IfNull(row.Item("ROWNUM"), "") & " sono incompleti!"
                        TrovaRigaDataGrid(TipoRiga.Inquilino, CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    MyReader.Close()
                    par.cmd.CommandText = "SELECT ID FROM SIR_INQUILINO_PATR_IMMOBILIARE WHERE ID_INQUILINO = " & par.IfNull(row.Item("ID"), "-1") & " AND ID_SIRAPER = " & idSiraper.Value & " " _
                                        & "AND (ANNO_RIFERIMENTO IS NULL OR TIPO_PATRIMONIO IS NULL OR QUOTA_PROPRIETA IS NULL OR VALORE_ICI IS NULL)"
                    MyReader = par.cmd.ExecuteReader
                    If MyReader.HasRows Then
                        Messaggio = "I dati del patrimonio immobiliare dell'Inquilino n° " & par.IfNull(row.Item("ROWNUM"), "") & " sono incompleti!"
                        TrovaRigaDataGrid(TipoRiga.Inquilino, CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    MyReader.Close()
                Next
            End If
        Catch ex As Exception
            ControlliUpdateDataGrid_2013 = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - ControlliUpdateDataGrid_2013 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function ControlliUpdateDataGrid_2015(ByRef Messaggio As String) As Boolean
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader
            ControlliUpdateDataGrid_2015 = True
            Dim dt As New Data.DataTable
            Dim row As Data.DataRow
            'CONTROLLO FABBRICATO
            dt = Session.Item("dtFabbricato")
            If Not IsNothing(dt) Then
                For Each row In dt.Rows
                    If par.IfNull(row.Item("GESTIONE"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire la tipologia di Gestione dell'Edificio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Fabbricato, CType(Me.Tab_Fabbricato1.FindControl("dgvFabbricati"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If par.IfNull(row.Item("UBICAZIONE"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire la tipologia di Ubicazione dell'Edificio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Fabbricato, CType(Me.Tab_Fabbricato1.FindControl("dgvFabbricati"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If par.IfNull(row.Item("COEFF_UBICAZIONE"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire il coefficente di Ubicazione dell'Edificio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Fabbricato, CType(Me.Tab_Fabbricato1.FindControl("dgvFabbricati"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                Next
            End If
            'CONTROLLO ALLOGGIO
            dt = Session.Item("dtAlloggio")
            If Not IsNothing(dt) Then
                For Each row In dt.Rows
                    If String.IsNullOrEmpty(par.IfNull(row.Item("CODICE_MIR"), "")) Then
                        Messaggio = "Inserire il Codice Mir di 7 cifre dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If Len(Trim(par.IfNull(row.Item("CODICE_MIR"), ""))) <> 7 Then
                            Messaggio = "Inserire il Codice Mir di 7 cifre dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If par.IfNull(row.Item("ALL_DISM_CART"), "") = "DISMESSO" Or par.IfNull(row.Item("ALL_DISM_CART"), "") = "CARTOLARIZZATO" Then
                        If String.IsNullOrEmpty(par.IfNull(row.Item("PROV_DISM_CART"), "")) Then
                            Messaggio = "Inserire i Proventi di Dismissione o Cartolarizzazione dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If par.IfNull(row.Item("CATEGORIA_CATASTALE"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire la Categoria Catastale dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("RENDITA_CATASTALE"), "")) Then
                        Messaggio = "Inserire la Rendita Catastale dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If par.IfNull(row.Item("COEFF_CLASSE_DEMOGRAFICA_LR"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire la Coefficente della Classe Demografica L.R. n°27/2009 dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("NUMERO_STANZE"), "")) Then
                        Messaggio = "Inserire il numero di stanze dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    ElseIf CInt(par.IfNull(row.Item("NUMERO_STANZE"), 0)) < 0 Then
                        Messaggio = "Il numero di stanze dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può essere minore di 0!"
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("ANNO_RISTRUTTURAZIONE"), "")) Then
                        Messaggio = "Inserire l'anno di ultimazione o recupero ristrutturazione dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If Len(par.IfNull(row.Item("ANNO_RISTRUTTURAZIONE"), "")) <> 4 Then
                            Messaggio = "Inserire l'anno corretto di ultimazione o recupero ristrutturazione dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                        par.cmd.CommandText = "SELECT DATA_COSTRUZIONE FROM SISCOM_MI.EDIFICI WHERE ID = (SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & par.IfNull(row.Item("ID"), "-1") & ")"
                        MyReader = par.cmd.ExecuteReader
                        If MyReader.Read Then
                            If par.IfNull(row.Item("ANNO_RISTRUTTURAZIONE"), "") < Left(par.IfNull(MyReader("DATA_COSTRUZIONE"), "1000"), 4) Then
                                Messaggio = "L'anno di ultimazione o recupero ristrutturazione dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può essere minore di quello di costruzione"
                                TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                                Exit Function
                            End If
                        End If
                        MyReader.Close()
                        If par.IfNull(row.Item("ANNO_RISTRUTTURAZIONE"), "") > Format(Now, "yyyy") Then
                            Messaggio = "L'anno di ultimazione o recupero ristrutturazione dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può essere maggiore di quello in corso"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If par.IfNull(row.Item("TIPO_INTERVENTO"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire il tipo di Intervento dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUP_UTILE_ALLOGGIO"), "")) Then
                        Messaggio = "Inserire la superficie utile dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If par.IfNull(row.Item("SUP_UTILE_ALLOGGIO"), 0) < 20 Or par.IfNull(row.Item("SUP_UTILE_ALLOGGIO"), 0) > 150 Then
                            Messaggio = "Inserire la superficie utile dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 20mq e 150mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("SUP_CANTINE_SOFF"), "")) Then
                        If par.IfNull(row.Item("SUP_CANTINE_SOFF"), 0) < 0 Or par.IfNull(row.Item("SUP_CANTINE_SOFF"), 0) > 300 Then
                            Messaggio = "Inserire la superficie delle catine o soffitte dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("SUP_BALCONI"), 0)) Then
                        If par.IfNull(row.Item("SUP_BALCONI"), 0) < 0 Or par.IfNull(row.Item("SUP_BALCONI"), 0) > 300 Then
                            Messaggio = "Inserire la superficie dei balconi dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("SUP_AREA_PRIVATA"), 0)) Then
                        If par.IfNull(row.Item("SUP_AREA_PRIVATA"), 0) < 0 Or par.IfNull(row.Item("SUP_AREA_PRIVATA"), 0) > 300 Then
                            Messaggio = "Inserire la superficie dell'Area Privata dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("SUP_VERDE_COND"), 0)) Then
                        If par.IfNull(row.Item("SUP_VERDE_COND"), 0) < 0 Or par.IfNull(row.Item("SUP_VERDE_COND"), 0) > 300 Then
                            Messaggio = "Inserire la superficie Verde Condominiale dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("SUP_BOX"), 0)) Then
                        If par.IfNull(row.Item("SUP_BOX"), 0) < 0 Or par.IfNull(row.Item("SUP_BOX"), 0) > 300 Then
                            Messaggio = "Inserire la superficie dei Box dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("SUP_POSTO_AUTO"), 0)) Then
                        If par.IfNull(row.Item("SUP_POSTO_AUTO"), 0) < 0 Or par.IfNull(row.Item("SUP_POSTO_AUTO"), 0) > 300 Then
                            Messaggio = "Inserire la superficie dei Posto Auto dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("SUP_PERTINENZE"), 0)) Then
                        If par.IfNull(row.Item("SUP_PERTINENZE"), 0) < 0 Or par.IfNull(row.Item("SUP_PERTINENZE"), 0) > 300 Then
                            Messaggio = "Inserire la superficie delle Pertinenze dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("SUP_CONVENZIONALE_ANTE"), 0)) Then
                        If par.IfNull(row.Item("SUP_CONVENZIONALE_ANTE"), 0) < 0 Or par.IfNull(row.Item("SUP_CONVENZIONALE_ANTE"), 0) > 300 Then
                            Messaggio = "Inserire la superficie Convenzionale Ante Legem dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUP_CONVENZIONALE_LR"), "")) Then
                        Messaggio = "Inserire la superficie Convenzionale L.R. n°27/2009 dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If par.IfNull(row.Item("STATO_CONS_ALL"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire lo stato di Conservazione dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può essere maggiore di quello in corso"
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("PER_ISTAT_LEGGE27"), "")) Then
                        Messaggio = "Inserire la percentuale di aggiornamento ISTAT Legge 27/2009 dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    ElseIf CInt(par.IfNull(row.Item("PER_ISTAT_LEGGE27"), 0)) < 0 Then
                        Messaggio = "La percentuale di aggiornamento ISTAT Legge 27/2009 dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " deve essere >= 0!"
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If par.IfNull(row.Item("ABBATTIMENTO_CANONE"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire l'abbatimento del Canone dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If par.IfNull(row.Item("SOVRAPREZZO_DECADENZA"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire il sovraprezzo per decadenza dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("PERC_AGG_AREA_DEC"), "")) Then
                        Messaggio = "Inserire la percentuale aggiuntiva per area decadenza dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("NUM_BOX_CONTR_SEP"), "")) Then
                        Messaggio = "Inserire il numero dei Box a contratto separato dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If Not String.IsNullOrEmpty(par.IfNull(row.Item("NUM_BOX_CONTR_SEP"), "")) Then
                        If String.IsNullOrEmpty(par.IfNull(row.Item("CANONE_BOX_CONTR_SEP"), "")) Then
                            Messaggio = "Inserire il canone dei Box a contratto separato dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "") & " compresa tra 0mq e 300mq"
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If par.IfNull(row.Item("CONTAB_UNICA"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire la contabilità unica dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    Else
                        If par.IfNull(row.Item("CONTAB_UNICA"), "2").ToString = "1" Then
                            Messaggio = "Inserire il gettito per la contabilità unica dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                    End If
                    If par.IfNull(row.Item("CANONE_SOCIALE"), 0) = 1 Or par.IfNull(row.Item("CANONE_SOCIALE"), 0) = 2 Then
                        If String.IsNullOrEmpty(par.IfNull(row.Item("PSE"), "")) Then
                            Messaggio = "Inserire il PSE dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        Else
                            If par.IfNull(row.Item("PSE"), 0) < 1 Then
                                Messaggio = "Inserire il PSE maggiore/uguale a 1 dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                                TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                                Exit Function
                            End If
                        End If
                        If String.IsNullOrEmpty(par.IfNull(row.Item("DATA_CALCOLO_ISEE"), "")) And Not String.IsNullOrEmpty(par.IfNull(row.Item("CONTRATTO"), "")) Then
                            Messaggio = "Data Calcolo ISEE mancante per l'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                            TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                            Exit Function
                        End If
                        If ((par.IfNull(row.Item("ISE_ERP"), 0) = 0 Or par.IfNull(row.Item("ISEE_ERP"), 0) = 0 Or par.IfNull(row.Item("ISE_ERP_ASS"), 0) = 0 Or par.IfNull(row.Item("ISEE_ERP_ASS"), 0) = 0)) And row.Item("AREA_APPARTENENZA").ToString = "DECADENZA" Then
                            If String.IsNullOrEmpty(par.IfNull(row.Item("ISEE_PRON_DECADENZA"), "")) Then
                                Messaggio = "ISEE per Pronuncia Decadenza mancante per l'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                                TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                                Exit Function
                            End If
                        End If
                    End If
                    If par.IfNull(row.Item("INV_SOCIALE"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire la invalidità sociale dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If par.IfNull(row.Item("CANONE_SOCIALE"), 0) = 1 Then 'CONTROLLO SE L'UNITA E A CANONE SOCIALE     
                        If par.IfNull(row.Item("AREA_APPARTENENZA"), "-1").ToString = "4" Then
                            If String.IsNullOrEmpty(par.IfNull(row.Item("ISEE_PRON_DECADENZA"), "")) Then
                                Messaggio = "Inserire l'ISEE per pronuncia decadenza dell'Alloggio n° " & par.IfNull(row.Item("ROWNUM"), "")
                                TrovaRigaDataGrid(TipoRiga.Alloggio, CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                                Exit Function
                            End If
                        End If
                    End If
                Next
            End If
            'CONTROLLO INQUILINO
            dt = Session.Item("dtInquilino")
            If Not IsNothing(dt) Then
                For Each row In dt.Rows
                    If String.IsNullOrEmpty(par.IfNull(row.Item("REDD_TERRENI"), "").ToString) Then
                        Messaggio = "Il reddito dei terreni n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può esserre vuoto!"
                        TrovaRigaDataGrid(TipoRiga.Inquilino, CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    ElseIf CDec(par.IfNull(row.Item("REDD_TERRENI"), 0)) < 0 Then
                        Messaggio = "Il reddito dei terreni n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può esserre minore di 0!"
                        TrovaRigaDataGrid(TipoRiga.Inquilino, CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("EMOLUMENTI"), "").ToString) Then
                        Messaggio = "Il reddito degli altri emolumenti n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può esserre vuoto!"
                        TrovaRigaDataGrid(TipoRiga.Inquilino, CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    ElseIf CDec(par.IfNull(row.Item("EMOLUMENTI"), 0)) < 0 Then
                        Messaggio = "Il reddito degli altri emolumenti n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può esserre minore di 0!"
                        TrovaRigaDataGrid(TipoRiga.Inquilino, CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If String.IsNullOrEmpty(par.IfNull(row.Item("SUSSIDI"), "").ToString) Then
                        Messaggio = "Il campo sussidi enti pubblici n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può esserre vuoto!"
                        TrovaRigaDataGrid(TipoRiga.Inquilino, CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    ElseIf CDec(par.IfNull(row.Item("SUSSIDI"), 0)) < 0 Then
                        Messaggio = "Il campo sussidi enti pubblici n° " & par.IfNull(row.Item("ROWNUM"), "") & " non può esserre minore di 0!"
                        TrovaRigaDataGrid(TipoRiga.Inquilino, CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                    If par.IfNull(row.Item("FISC_A_CARICO"), "-1").ToString = "-1" Then
                        Messaggio = "Inserire se è fiscalmente a carico dell'intestatario l'Inquilino n° " & par.IfNull(row.Item("ROWNUM"), "")
                        TrovaRigaDataGrid(TipoRiga.Inquilino, CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid), par.IfNull(row.Item("ROWNUM"), 0))
                        Exit Function
                    End If
                Next
            End If
        Catch ex As Exception
            ControlliUpdateDataGrid_2015 = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - ControlliUpdateDataGrid_2015 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    'Private Function SalvataggioDataGrid() As Boolean
    '    Try
    '        SalvataggioDataGrid = False
    '        If Not IsNothing(Me.connData) Then
    '            Me.connData.RiempiPar(par)
    '            'par.cmd.Transaction = connData.Transazione
    '        End If
    '        Dim dt As New Data.DataTable
    '        Dim row As Data.DataRow
    '        'UPDATE FABBRICATO
    '        dt = Session.Item("dtFabbricato")
    '        If Not IsNothing(dt) Then
    '            For Each row In dt.Rows
    '                par.cmd.CommandText = "UPDATE SISCOM_MI.SIR_FABBRICATO SET GESTIONE = " & RitornaNullSeMenoUno(par.IfNull(row.Item("GESTIONE"), "-1")) & ", " _
    '                                    & "UBICAZIONE = " & RitornaNullSeMenoUno(par.IfNull(row.Item("UBICAZIONE"), "-1")) & ", " _
    '                                    & "COEFF_UBICAZIONE = " & RitornaNullSeMenoUno(par.IfNull(row.Item("COEFF_UBICAZIONE"), "-1")) & " " _
    '                                    & "WHERE ID = " & par.IfNull(row.Item("ID"), "-1") & " AND ID_SIRAPER = " & idSiraper.Value
    '                par.cmd.ExecuteNonQuery()
    '            Next
    '        End If
    '        'UPDATE INQUILINO
    '        dt = Session.Item("dtInquilino")
    '        If Not IsNothing(dt) Then
    '            For Each row In dt.Rows
    '                par.cmd.CommandText = "UPDATE SISCOM_MI.SIR_INQUILINO SET FISC_A_CARICO = " & RitornaNullSeMenoUno(par.IfNull(row.Item("FISC_A_CARICO"), "-1")) & ", " _
    '                                    & "REDD_TERRENI = " & par.VirgoleInPunti(par.IfNull(row.Item("REDD_TERRENI"), "null").ToString.Replace(".", "")) & ", " _
    '                                    & "EMOLUMENTI = " & par.VirgoleInPunti(par.IfNull(row.Item("EMOLUMENTI"), "null").ToString.Replace(".", "")) & ", " _
    '                                    & "SUSSIDI = " & par.VirgoleInPunti(par.IfNull(row.Item("SUSSIDI"), "null").ToString.Replace(".", "")) & ", " _
    '                                    & "REDD_COMPLESSIVO = " & par.VirgoleInPunti(par.IfNull(row.Item("REDD_COMPLESSIVO"), "null").ToString.Replace(".", "")) & " " _
    '                                    & "WHERE ID = " & par.IfNull(row.Item("ID"), "-1") & " AND ID_SIRAPER = " & idSiraper.Value
    '                par.cmd.ExecuteNonQuery()
    '            Next
    '        End If
    '        SalvataggioDataGrid = True
    '    Catch ex As Exception
    '        SalvataggioDataGrid = False
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            connData.chiudi(False)
    '        End If
    '        Session.Add("ERRORE", "Provenienza: Siraper - SalvataggioDataGrid - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Function
#End Region
#Region "Uscita"
    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Uscita()
    End Sub
    Private Sub Uscita(Optional esci As Boolean = True)
        Try
            If frmModify.Value <> 111 Then
                If Not IsNothing(HttpContext.Current.Session.Item("SIRAPER" & vIdConnessione)) Then
                    CType(HttpContext.Current.Session.Item("SIRAPER" & vIdConnessione), CM.datiConnessione).chiudi(False)
                    HttpContext.Current.Session.Remove("SIRAPER" & vIdConnessione)
                End If
                PulisciSessioni()
                If esci = True Then
                    Response.Redirect("Home.aspx", False)
                End If
            Else
                frmModify.Value = 1
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - Uscita - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnIndietro_Click(sender As Object, e As System.EventArgs) Handles btnIndietro.Click
        Indietro()
    End Sub
    Private Sub Indietro(Optional esci As Boolean = True)
        Try
            If frmModify.Value <> 111 Then
                If Not IsNothing(HttpContext.Current.Session.Item("SIRAPER" & vIdConnessione)) Then
                    CType(HttpContext.Current.Session.Item("SIRAPER" & vIdConnessione), CM.datiConnessione).chiudi(False)
                    HttpContext.Current.Session.Remove("SIRAPER" & vIdConnessione)
                End If
                PulisciSessioni()
                If esci = True Then
                    Dim condizioni As String = ""
                    condizioni &= "&SE=" & Request.QueryString("SE")
                    condizioni &= "&TE=" & Request.QueryString("TE")
                    condizioni &= "&DRD=" & Request.QueryString("DRD")
                    condizioni &= "&DRA=" & Request.QueryString("DRA")
                    condizioni &= "&CF=" & Request.QueryString("CF")
                    condizioni &= "&PI=" & Request.QueryString("PI")
                    condizioni &= "&RS=" & Request.QueryString("RS")
                    Response.Redirect("RisultatiElaborazioni.aspx?R=1" & condizioni, False)
                End If
            Else
                frmModify.Value = 1
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - Indietro - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub PulisciSessioni()
        Try
            If Not IsNothing(Session.Item("dtFabbricato")) Then
                Session.Remove("dtFabbricato")
            End If
            If Not IsNothing(Session.Item("dtAlloggio")) Then
                Session.Remove("dtAlloggio")
            End If
            If Not IsNothing(Session.Item("dtInquilino")) Then
                Session.Remove("dtInquilino")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - PulisciSessioni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
#End Region
#Region "Elaborazione"
    Protected Sub btnElabora_Click(sender As Object, e As System.EventArgs) Handles btnElabora.Click
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            If idSiraper.Value <> -1 Then
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_SIRAPER_PROCEDURE.NEXTVAL FROM DUAL"
                Dim idProceduraSiraper As String = par.IfNull(par.cmd.ExecuteScalar, "")
                Dim Parametri As String = idSiraper.Value & ";" & Elaborazione.Value & ";" & txtAnnoRif.Text & ";" & txtDataRiferimento.Text
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SIRAPER_PROCEDURE (ID, DATA_ORA_INIZIO, ID_OPERATORE, DATA_ORA_FINE, ESITO, ERRORE, " _
                                    & "PARZIALE, TOTALE, TIPO, DESCRIZIONE_ERRORE, PARAMETRI) VALUES " _
                                    & "(" & idProceduraSiraper & ", " & Format(Now, "yyyyMMddHHmmss") & ", " & Session.Item("ID_OPERATORE") & ", '', 0, '', " _
                                    & "0, 100, 1, '', '" & Parametri & "')"
                par.cmd.ExecuteNonQuery()
                connData.chiudi(True)
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
                    sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idProceduraSiraper
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/Siraper.exe")
                    p.StartInfo.Arguments = sParametri
                    p.Start()
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Procedura avviata correttamente!');location.href='Home.aspx';Procedure();", True)
                Catch ex As Exception
                    connData.apri(False)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER_PROCEDURE SET ESITO=2,ERRORE='PROCEDURA NON AVVIATA' WHERE ID = " & idProceduraSiraper
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(False)
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante l\'avvio della procedura!');location.href='Home.aspx';", True)
                End Try
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - btnElabora_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
#End Region
#Region "GestioneDatagrid"
    Private Sub CaricaDatagrid()
        Try
            Tab_Fabbricato1.CaricaDatagridFabbricato()
            Tab_Inquilino1.CaricaDataGridInquilino()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - CaricaDatagrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
#End Region
#Region "Funzioni"
    Private Function RicavaStatoAlloggio(ByVal Stato As String) As String
        Try
            RicavaStatoAlloggio = "null"
            Select Case Stato
                Case "MEDIO"
                    RicavaStatoAlloggio = "2"
                Case "NORMA"
                    RicavaStatoAlloggio = "1"
                Case "SCADE"
                    RicavaStatoAlloggio = "3"
                Case Else
                    RicavaStatoAlloggio = "null"
            End Select
        Catch ex As Exception
            RicavaStatoAlloggio = "null"
        End Try
    End Function
    Private Function RicavaVial(ByVal indirizzo As String) As Integer
        Dim pos As Integer = 0
        Dim via As String = ""
        Try
            pos = InStr(1, indirizzo, " ")
            If pos > 0 Then
                via = Mid(indirizzo, 1, pos - 1)
                Select Case via.ToUpper
                    Case "V.", "VIA"
                        RicavaVial = 1
                    Case "PIAZZA", "PZ.", "P.ZZA"
                        RicavaVial = 2
                    Case "VICOLO"
                        RicavaVial = 3
                    Case "LARGO"
                        RicavaVial = 4
                    Case "CORSO", "C.SO"
                        RicavaVial = 5
                    Case "STRETTO"
                        RicavaVial = 6
                    Case "VIALE", "V.LE"
                        RicavaVial = 7
                    Case "PIAZZALE", "P.LE"
                        RicavaVial = 8
                    Case "PIAZZETTA"
                        RicavaVial = 9
                    Case "CORSETTO"
                        RicavaVial = 10
                    Case "TRAVERSA", "TRAV."
                        RicavaVial = 11
                    Case "PASSAGGIO"
                        RicavaVial = 12
                    Case "RAMPA"
                        RicavaVial = 13
                    Case "S.T.R.", "STRADA"
                        RicavaVial = 14
                    Case "CONTRADA"
                        RicavaVial = 15
                    Case "RUA"
                        RicavaVial = 16
                    Case "LOCALITA'"
                        RicavaVial = 17
                    Case "QUARTIERE"
                        RicavaVial = 18
                    Case Else
                        RicavaVial = 1
                End Select
            Else
                RicavaVial = ""
            End If
        Catch ex As Exception
            RicavaVial = 1
        End Try
    End Function
    Private Function RicavaDescVia(ByVal indirizzo As String) As String
        Try
            RicavaDescVia = ""
            Dim pos As Integer = 0
            Dim descrizione As String = ""
            pos = InStr(1, indirizzo, " ")
            If pos > 0 Then
                descrizione = Mid(indirizzo, pos + 1)
                RicavaDescVia = descrizione
            End If
        Catch ex As Exception
            RicavaDescVia = ""
        End Try
    End Function
    Private Function NumeriNull(ByVal Numero As Decimal) As String
        Try
            NumeriNull = "null"
            If String.IsNullOrEmpty(Numero.ToString) Then
                NumeriNull = "null"
            Else
                If Numero = 0 Then
                    NumeriNull = "null"
                Else
                    NumeriNull = (par.VirgoleInPunti(Numero)).ToString
                End If
            End If
        Catch ex As Exception
            NumeriNull = "null"
        End Try
    End Function
    Private Function RitornaNullSeMenoUno(ByVal Valore As String) As String
        Try
            RitornaNullSeMenoUno = "null"
            If Valore = "-1" Then
                RitornaNullSeMenoUno = "null"
            ElseIf Valore <> "-1" Then
                RitornaNullSeMenoUno = "'" & Valore & "'"
            End If
        Catch ex As Exception
            RitornaNullSeMenoUno = "null"
        End Try
    End Function
    Enum TipoRiga
        Fabbricato = 1
        Alloggio = 2
        Inquilino = 3
    End Enum
    Public Sub TrovaRigaDataGrid(ByVal Tipo As TipoRiga, ByVal DataGrid As DataGrid, ByVal row As Long)
        Dim pagina As Integer = 0
        Select Case Tipo
            Case 1
                ''Tab_Fabbricato1.AggiustaCompSessioneFabbricato()
                'pagina = Math.Floor(row / CType(Me.Tab_Fabbricato1.FindControl("dgvFabbricati"), DataGrid).PageSize)
                'CType(Me.Tab_Fabbricato1.FindControl("dgvFabbricati"), DataGrid).CurrentPageIndex = pagina
                'Tab_Fabbricato1.BindGridFabbricato()
                'CType(Me.Tab_Fabbricato1.FindControl("dgvFabbricati"), DataGrid).Items(TrovaRigaPaginaDatagrid(CInt(row), CType(Me.Tab_Fabbricato1.FindControl("dgvFabbricati"), DataGrid))).BackColor = Drawing.Color.Orange
                'tabSelect.Value = 0
            Case 2
                ''Tab_Alloggio1.AggiustaCompSessioneAlloggio()
                'pagina = Math.Floor(row / CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid).PageSize)
                'CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid).CurrentPageIndex = pagina
                'Tab_Alloggio1.BindGridAlloggio()
                'CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid).Items(TrovaRigaPaginaDatagrid(CInt(row), CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid))).BackColor = Drawing.Color.Orange
                'tabSelect.Value = 1
            Case 3
                ''Tab_Inquilino1.AggiustaCompSessioneInquilino()
                'pagina = Math.Floor(row / CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid).PageSize)
                'CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid).CurrentPageIndex = pagina
                'Tab_Inquilino1.BindGridInquilino()
                'CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid).Items(TrovaRigaPaginaDatagrid(CInt(row), CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid))).BackColor = Drawing.Color.Orange
                'tabSelect.Value = 2
        End Select
    End Sub
    Private Function TrovaRigaPaginaDatagrid(ByVal riga As Integer, ByVal Datagrid As DataGrid) As Integer
        Try
            While riga > Datagrid.PageSize
                If riga > Datagrid.PageSize Then
                    riga = riga - Datagrid.PageSize
                Else
                    Exit While
                End If
            End While
            TrovaRigaPaginaDatagrid = riga - 1
        Catch ex As Exception
            TrovaRigaPaginaDatagrid = 0
        End Try
    End Function
    Private Function RicavaCoeffLivelloPiano(ByVal CodPiano As String, ByVal Ascensore As Integer) As String
        Try
            RicavaCoeffLivelloPiano = "null"
            If Not String.IsNullOrEmpty(CodPiano) Then
                Select Case CodPiano
                    Case "SS"
                        RicavaCoeffLivelloPiano = "0.80"
                    Case "TT"
                        RicavaCoeffLivelloPiano = "0.90"
                    Case "AA"
                        RicavaCoeffLivelloPiano = "1.20"
                    Case Else
                        If Ascensore = 1 Then 'SI
                            RicavaCoeffLivelloPiano = "1.00"
                        Else 'NO
                            Select Case CodPiano
                                Case "1", "2", "3"
                                    RicavaCoeffLivelloPiano = "1.00"
                                Case Else
                                    RicavaCoeffLivelloPiano = "0.95"
                            End Select
                        End If
                End Select
            End If
        Catch ex As Exception
            RicavaCoeffLivelloPiano = "null"
        End Try
    End Function
    Private Function RicavaInfoObjAlloggio(ByVal IdDestinazioneUso As Integer, ByRef DestinazioneUso As Integer, ByRef TipoGodimento As Integer, ByVal TipoDisponibilita As String, ByVal IdContratto As Long, ByRef StatoAlloggio As Integer, ByRef TipoContratto As String, ByVal IdAlloggio As Long, ByVal DataDisponibilita As String, ByRef CoeffUbicazioneAnte As Integer, ByRef CoeffUbicazioneLr As Integer, ByRef AnnoCostrRistr As String, ByRef TipoIntervento As String) As Boolean
        Try
            RicavaInfoObjAlloggio = False
            Dim TipoContrattoProv As String = ""
            Select Case IdDestinazioneUso
                Case 1
                    DestinazioneUso = 1
                    TipoGodimento = 1
                Case 2
                    DestinazioneUso = 2
                    TipoGodimento = 2
                Case 3
                    DestinazioneUso = 0
                    TipoGodimento = 3
                Case 4
                    DestinazioneUso = 0
                    TipoGodimento = 9
                Case 5
                    DestinazioneUso = 0
                    TipoGodimento = 3
                Case 6
                    DestinazioneUso = 0
                    TipoGodimento = 3
                Case 7
                    DestinazioneUso = 0
                    TipoGodimento = 10
                Case Else
                    DestinazioneUso = 0
                    TipoGodimento = 9
            End Select
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            If IdContratto <> 0 Then
                par.cmd.CommandText = "SELECT SISCOM_MI.GETSTATOCONTRATTO(" & IdContratto & ") AS STATO FROM DUAL"
                Dim StatoContratto As String = par.IfNull(par.cmd.ExecuteScalar, "")
                Select Case StatoContratto
                    Case "IN CORSO", "IN CORSO (S.T.)", "BOZZA", "IN ATTESA"
                        StatoAlloggio = 1
                    Case "CHIUSO"
                        StatoAlloggio = 3
                    Case Else
                        StatoAlloggio = 6
                End Select
                Select Case TipoContratto
                    Case "CON"
                        TipoContrattoProv = "9"
                    Case "EQC392"
                        TipoContrattoProv = "9"
                    Case "ERP"
                        If DestinazioneUso = 1 Then
                            TipoContrattoProv = "1"
                        ElseIf DestinazioneUso = 2 Then
                            TipoContrattoProv = "2"
                        Else
                            TipoContrattoProv = "9"
                        End If
                    Case "L43198"
                        TipoContrattoProv = "3"
                    Case "NONE"
                        TipoContrattoProv = "9"
                    Case "USD"
                        TipoContrattoProv = "9"
                    Case Else
                        TipoContrattoProv = "8"
                End Select
            Else
                Select Case TipoDisponibilita.ToUpper
                    Case "LOCA"
                        StatoAlloggio = 2
                    Case "LIBE"
                        StatoAlloggio = 4
                    Case "MOB"
                        StatoAlloggio = 5
                    Case "CART", "INDEF", "NAGI", "RIST", "VEND"
                        StatoAlloggio = 6
                    Case Else
                        StatoAlloggio = 6
                End Select
                TipoContrattoProv = "8"
            End If
            TipoContratto = TipoContrattoProv
            par.cmd.CommandText = "SELECT DATA_DISPONIBILITA FROM ALLOGGI WHERE ID_UNITA = " & IdAlloggio
            DataDisponibilita = par.IfNull(par.cmd.ExecuteScalar, "")
            If String.IsNullOrEmpty(Trim(DataDisponibilita)) Then
                par.cmd.CommandText = "SELECT DATA_DISPONIBILITA FROM SISCOM_MI.UI_USI_DIVERSI WHERE ID_UNITA = " & IdAlloggio
                DataDisponibilita = par.IfNull(par.cmd.ExecuteScalar, "")
            End If
            par.cmd.CommandText = "SELECT COD_COMUNE FROM SISCOM_MI.INDIRIZZI WHERE ID = (SELECT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & IdAlloggio & ")"
            Dim CodComune As String = par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT TIPO_COEFF_UBICAZIONE_EDIFICIO.COD " _
                                & "FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SISCOM_MI.TIPO_COEFF_UBICAZIONE_EDIFICIO " _
                                & "WHERE INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO AND TIPO_COEFF_UBICAZIONE_EDIFICIO.COD IN (19,20,21,22,23,24) " _
                                & "AND TIPO_COEFF_UBICAZIONE_EDIFICIO.VALORE = " & par.VirgoleInPunti(par.RicavaDemografia(CodComune)) & " " _
                                & "AND UNITA_IMMOBILIARI.ID = " & IdAlloggio
            CoeffUbicazioneAnte = par.IfNull(par.cmd.ExecuteScalar, 0)
            par.cmd.CommandText = "SELECT TIPO_COEFF_UBICAZIONE_EDIFICIO.COD " _
                                & "FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SISCOM_MI.TIPO_COEFF_UBICAZIONE_EDIFICIO " _
                                & "WHERE INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO AND TIPO_COEFF_UBICAZIONE_EDIFICIO.COD IN (19,20,21,25,26,23,24) " _
                                & "AND TIPO_COEFF_UBICAZIONE_EDIFICIO.VALORE = " & par.VirgoleInPunti(par.RicavaDemografia(CodComune)) & " " _
                                & "AND UNITA_IMMOBILIARI.ID = " & IdAlloggio
            CoeffUbicazioneLr = par.IfNull(par.cmd.ExecuteScalar, 0)
            par.cmd.CommandText = "SELECT DATA_COSTRUZIONE, DATA_RISTRUTTURAZIONE FROM SISCOM_MI.EDIFICI WHERE ID = (SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & IdAlloggio & ")"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                If Not String.IsNullOrEmpty(par.IfNull(dt.Rows(0).Item("DATA_RISTRUTTURAZIONE"), "")) Then
                    AnnoCostrRistr = Left(par.IfNull(dt.Rows(0).Item("DATA_RISTRUTTURAZIONE"), ""), 4)
                    TipoIntervento = "RE"
                Else
                    AnnoCostrRistr = Left(par.IfNull(dt.Rows(0).Item("DATA_COSTRUZIONE"), ""), 4)
                    TipoIntervento = "NC"
                End If
            End If
            RicavaInfoObjAlloggio = True
        Catch ex As Exception
            RicavaInfoObjAlloggio = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - ElaboraAlloggio - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function RicavaDatiContrObj(ByVal IdContratto As Long, ByVal DestinazioneUso As Integer, ByRef AreaAppartenenza As Integer, ByRef CanoneAnnRegime As Decimal, ByRef CostoConvenzionale As Decimal, ByRef RedditoPrevDipendente As Integer, ByRef CompInv100Con As Integer, ByRef CompInv100Senza As Integer, ByRef CompInv67 As Integer, ByRef StatoAggNucleo As Integer, ByRef DataCalcoloIsee As String, ByRef ISR As Decimal, ByRef ISP As Decimal, ByRef PSE As Decimal, ByRef ISERP As Decimal, ByRef ISEERP As Decimal, ByRef RedditoDip As Decimal, ByRef RedditoAltri As Decimal, ByRef ValoreLocativo As Decimal, ByRef Vetusta As Decimal, ByRef AnnoVetusta As String, ByRef NumComponenti As Integer, ByRef ReddDipPens As Integer, ByRef SpeseInv As Decimal, ByRef PercValLoc As Decimal, ByRef IdDichiarazione As Long, ByRef InvaliditaSoc As Integer) As Boolean
        Try
            RicavaDatiContrObj = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            par.cmd.CommandText = "SELECT ID_AREA_ECONOMICA, CANONE, COSTOBASE, REDD_PREV_DIP, NUM_COMP_100, NUM_COMP_100_CON, NUM_COMP_66, SOTTO_AREA, ID_DICHIARAZIONE, ISR, ISP, PSE, ISE, ISEE, " _
                                & "REDDITI_DIP, REDDITI_ATRI, VALORE_LOCATIVO, VETUSTA, NUM_COMP, LIMITE_PENSIONI, DETRAZIONI_FRAGILITA, PERC_VAL_LOC " _
                                & "FROM SISCOM_MI.CANONI_EC " _
                                & "WHERE DATA_CALCOLO = (SELECT MAX(DATA_CALCOLO) FROM SISCOM_MI.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO) AND ID_CONTRATTO = " & IdContratto
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                AreaAppartenenza = par.IfNull(dt.Rows(0).Item("ID_AREA_ECONOMICA"), 0)
                If DestinazioneUso = 1 And AreaAppartenenza = 0 Then
                    AreaAppartenenza = 1
                End If
                CanoneAnnRegime = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("CANONE"), 0), ".", ""))
                CostoConvenzionale = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("COSTOBASE"), 0), ".", ""))
                If par.IfNull(dt.Rows(0).Item("REDD_PREV_DIP"), 0) = 1 Then
                    RedditoPrevDipendente = 1
                Else
                    RedditoPrevDipendente = 2
                End If
                CompInv100Con = par.IfNull(dt.Rows(0).Item("NUM_COMP_100"), 0)
                CompInv100Senza = par.IfNull(dt.Rows(0).Item("NUM_COMP_100_CON"), 0)
                CompInv67 = par.IfNull(dt.Rows(0).Item("NUM_COMP_66"), 0)
                If CompInv100Con + CompInv100Senza + CompInv67 >= 1 Then
                    InvaliditaSoc = 1
                Else
                    InvaliditaSoc = 2
                End If
                Select Case par.IfNull(dt.Rows(0).Item("SOTTO_AREA"), "")
                    Case "D5", "D6"
                        StatoAggNucleo = 2
                    Case ""
                        StatoAggNucleo = 0
                    Case Else
                        StatoAggNucleo = 1
                End Select
                par.cmd.CommandText = "SELECT DATA_ORA FROM UTENZA_EVENTI_DICHIARAZIONI " _
                                    & "WHERE ID_PRATICA = " & par.IfNull(dt.Rows(0).Item("ID_DICHIARAZIONE"), "-1") & " AND COD_EVENTO = 'F132' " _
                                    & "ORDER BY DATA_ORA DESC"
                Dim DataProv As String = par.IfEmpty(par.cmd.ExecuteScalar, "")
                If Not String.IsNullOrEmpty(DataProv) Then
                    DataCalcoloIsee = par.FormattaData(Left(DataProv, 8))
                End If
                If String.IsNullOrEmpty(DataCalcoloIsee) Then
                    par.cmd.CommandText = "SELECT MAX(DATA_ORA) AS DATA_ORA " _
                                        & "FROM SISCOM_MI.RAPPORTI_UTENZA, EVENTI_DICHIARAZIONI_VSA " _
                                        & "WHERE ID = " & par.IfNull(IdContratto, -1) & " AND  ID_PRATICA = ID_ISEE AND COD_EVENTO = 'F132'"
                    DataProv = par.IfNull(par.cmd.ExecuteScalar, "")
                    If Not String.IsNullOrEmpty(DataProv) Then
                        DataCalcoloIsee = par.FormattaData(Left(DataProv, 8))
                    End If
                End If
                ISR = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("ISR"), 0), ".", ""))
                ISP = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("ISP"), 0), ".", ""))
                PSE = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("PSE"), 0), ".", ""))
                ISERP = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("ISE"), 0), ".", ""))
                ISEERP = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("ISEE"), 0), ".", ""))
                RedditoDip = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("REDDITI_DIP"), 0), ".", ""))
                RedditoAltri = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("REDDITI_ATRI"), 0), ".", ""))
                ValoreLocativo = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("VALORE_LOCATIVO"), 0), ".", ""))
                Vetusta = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("VETUSTA"), 0), ".", ""))
                If Vetusta <> 0 Then
                    par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.T_VETUSTA_27 WHERE VALORE = " & par.VirgoleInPunti(par.IfNull(dt.Rows(0).Item("VETUSTA"), 0).ToString.Replace(".", ""))
                    AnnoVetusta = par.IfNull(par.cmd.ExecuteScalar, "")
                End If
                NumComponenti = par.IfNull(dt.Rows(0).Item("NUM_COMP"), 0)
                If par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("REDDITI_DIP"), 0), ".", "")) <= par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("LIMITE_PENSIONI"), 0), ".", "")) Then
                    ReddDipPens = 1
                Else
                    ReddDipPens = 2
                End If
                SpeseInv = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("DETRAZIONI_FRAGILITA"), 0), ".", ""))
                PercValLoc = par.PuntiInVirgole(Replace(par.IfNull(dt.Rows(0).Item("PERC_VAL_LOC"), 0), ".", ""))
                IdDichiarazione = par.IfNull(dt.Rows(0).Item("ID_DICHIARAZIONE"), 0)
            End If
            RicavaDatiContrObj = True
        Catch ex As Exception
            RicavaDatiContrObj = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - RicavaDatiContrObj - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function RicavaMorosita(ByVal IdContratto As Long, ByVal IdContrattoPrec As Long, ByRef MoroAtt As Decimal, ByRef MoroPrec As Decimal) As Boolean
        Try
            RicavaMorosita = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            'RICAVO MOROSITA FAMIGLIA ATTUALE
            If IdContratto <> 0 Then
                par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMPORTO " _
                                    & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI " _
                                    & "WHERE BOL_BOLLETTE_VOCI.ID_VOCE = 150 " _
                                    & "AND ID_BOLLETTA IN (SELECT ID FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_CONTRATTO = " & IdContratto & " AND ID_TIPO = 4)"
                MoroAtt = par.PuntiInVirgole(par.IfNull(par.cmd.ExecuteScalar, 0))
            End If
            'RICAVO MOROSITA FAMIGLIA PRECEDENTE
            If IdContrattoPrec <> 0 Then
                par.cmd.CommandText = "SELECT SUM(BOL_BOLLETTE_VOCI.IMPORTO) AS IMPORTO " _
                                   & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI " _
                                   & "WHERE BOL_BOLLETTE_VOCI.ID_VOCE = 150 " _
                                   & "AND ID_BOLLETTA IN (SELECT ID FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_CONTRATTO = " & IdContrattoPrec & " AND ID_TIPO = 4)"
                MoroPrec = par.PuntiInVirgole(par.IfNull(par.cmd.ExecuteScalar, 0))
            End If
            RicavaMorosita = True
        Catch ex As Exception
            RicavaMorosita = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - RicavaMorosita - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Function CalcolaValoreConvenzionale(ByVal Tipo As Integer, ByVal id_unita As Long) As Decimal
        Try
            Dim DEM As Double = 0
            Dim SUP_CONVENZIONALE As Double = 0
            Dim COSTO_BASE_MC As Double = 0
            Dim ZONA As Double = 0
            Dim PIANO As Double = 0
            Dim CONSERVAZIONE As Double = 0
            Dim VETUSTA As Double = 0
            Dim VALORECONVENZIONALE As Double = 0
            par.cmd.CommandText = "SELECT EDIFICI.NUM_ASCENSORI, TIPO_UBICAZIONE_LG_392_78.DESCRIZIONE AS ""UBICAZIONE"",TIPO_UBICAZIONE_LG_392_78.VALORE_PER_CANONE AS ""FTERR"", " _
                                & "EDIFICI.DATA_COSTRUZIONE,EDIFICI.DATA_RISTRUTTURAZIONE,EDIFICI.COD_COMUNE,COMUNI_NAZIONI.SIGLA AS ""PROVINCIA"",COMUNI_NAZIONI.NOME AS ""COMUNE_DI"", " _
                                & "DIMENSIONI.VALORE AS ""SUP_CONV"", INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO"",INDIRIZZI.CIVICO,INDIRIZZI.CAP,UNITA_IMMOBILIARI.*,TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"", " _
                                & "TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS ""TIPO_ALLOGGIO"",IDENTIFICATIVI_CATASTALI.FOGLIO,IDENTIFICATIVI_CATASTALI.NUMERO,IDENTIFICATIVI_CATASTALI.SUB," _
                                & "IDENTIFICATIVI_CATASTALI.COD_CATEGORIA_CATASTALE,IDENTIFICATIVI_CATASTALI.RENDITA,IDENTIFICATIVI_CATASTALI.COD_CLASSE_CATASTALE " _
                                & "FROM SEPA.COMUNI_NAZIONI, SISCOM_MI.DIMENSIONI,SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI,SISCOM_MI.IDENTIFICATIVI_CATASTALI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI," _
                                & "SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPO_UBICAZIONE_LG_392_78,SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                & "WHERE TIPO_UBICAZIONE_LG_392_78.COD=COMPLESSI_IMMOBILIARI.COD_TIPO_UBICAZIONE_LG_392_78 AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO " _
                                & "AND COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND UNITA_IMMOBILIARI.ID=DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA='SUP_CONV' " _
                                & "AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) " _
                                & "AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA (+) AND  TIPO_LIVELLO_PIANO.COD=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO AND UNITA_IMMOBILIARI.ID=" & id_unita
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                DEM = par.RicavaDemografia(par.IfNull(dt.Rows(0).Item("COD_COMUNE"), "-1"))
                SUP_CONVENZIONALE = par.IfNull(dt.Rows(0).Item("SUP_CONV"), 0)
                If Mid(par.IfNull(dt.Rows(0).Item("DATA_RISTRUTTURAZIONE"), 1111), 1, 4) > Mid(par.IfNull(dt.Rows(0).Item("DATA_COSTRUZIONE"), 1111), 1, 4) Then
                    If Mid(par.IfNull(dt.Rows(0).Item("DATA_RISTRUTTURAZIONE"), 1111), 1, 4) < 1977 Then
                        COSTO_BASE_MC = 1000
                    Else
                        COSTO_BASE_MC = 1250
                    End If
                Else
                    If Mid(par.IfNull(dt.Rows(0).Item("DATA_COSTRUZIONE"), 1111), 1, 4) < 1977 Then
                        COSTO_BASE_MC = 1000
                    Else
                        COSTO_BASE_MC = 1250
                    End If
                End If
                ZONA = par.IfNull(dt.Rows(0).Item("FTERR"), 0)
                par.cmd.CommandText = "SELECT TIPO_LIVELLO_PIANO.COD,TIPO_LIVELLO_PIANO.DESCRIZIONE,TIPO_LIVELLO_PIANO.VALORE_PER_CANONE " _
                                    & "FROM SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.PIANI WHERE PIANI.COD_TIPO_LIVELLO_PIANO=" & par.IfNull(dt.Rows(0).Item("COD_TIPO_LIVELLO_PIANO"), 0) & " " _
                                    & "AND TIPO_LIVELLO_PIANO.COD=PIANI.COD_TIPO_LIVELLO_PIANO"
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtPiano As New Data.DataTable
                da.Fill(dtPiano)
                da.Dispose()
                If dtPiano.Rows.Count > 0 Then
                    PIANO = CDbl(par.PuntiInVirgole(par.IfNull(dtPiano.Rows(0).Item("VALORE_PER_CANONE"), 1)))
                    If par.IfNull(dt.Rows(0).Item("NUM_ASCENSORI"), 0) <= 0 Then
                        If PIANO = 1.2 Then PIANO = 1.1D
                        If par.IfNull(dtPiano.Rows(0).Item("COD"), 0) >= 5 And par.IfNull(dtPiano.Rows(0).Item("COD"), 0) <= 31 Then
                            PIANO = 0.95D
                        End If
                        If par.IfNull(dtPiano.Rows(0).Item("COD"), 0) >= 46 And par.IfNull(dtPiano.Rows(0).Item("COD"), 0) <= 71 Then
                            PIANO = 0.95D
                        End If
                    End If
                End If
                par.cmd.CommandText = "select VALORE_PER_CANONE  from SISCOM_MI.STATO_CONSERVATIVO_LG_392_78 where COD='" & par.IfNull(dt.Rows(0).Item("COD_STATO_CONS_LG_392_78"), "") & "'"
                CONSERVAZIONE = par.IfNull(par.cmd.ExecuteScalar, 0)
                If Mid(par.IfNull(dt.Rows(0).Item("DATA_RISTRUTTURAZIONE"), 1111), 1, 4) > Mid(par.IfNull(dt.Rows(0).Item("DATA_COSTRUZIONE"), 1111), 1, 4) Then
                    If Mid(par.IfNull(dt.Rows(0).Item("DATA_RISTRUTTURAZIONE"), 1111), 1, 4) > 1986 Then
                        VETUSTA = 1
                    Else
                        If Mid(par.IfNull(dt.Rows(0).Item("DATA_RISTRUTTURAZIONE"), 1111), 1, 4) < 1956 Then
                            VETUSTA = 0.875
                        Else
                            par.cmd.CommandText = "select VALORE  from T_VETUSTA_27 where DESCRIZIONE='" & Mid(par.IfNull(dt.Rows(0).Item("DATA_RISTRUTTURAZIONE"), 1234), 1, 4) & "'"
                            VETUSTA = par.IfNull(par.cmd.ExecuteScalar, 0)
                        End If
                    End If
                Else
                    If Mid(par.IfNull(dt.Rows(0).Item("DATA_COSTRUZIONE"), 1111), 1, 4) > 1986 Then
                        VETUSTA = 1
                    Else
                        If Mid(par.IfNull(dt.Rows(0).Item("DATA_COSTRUZIONE"), 1111), 1, 4) < 1956 Then
                            VETUSTA = 0.875
                        Else
                            par.cmd.CommandText = "select VALORE  from T_VETUSTA_27 where DESCRIZIONE='" & Mid(par.IfNull(dt.Rows(0).Item("DATA_COSTRUZIONE"), 1234), 1, 4) & "'"
                            VETUSTA = par.IfNull(par.cmd.ExecuteScalar, 0)
                        End If
                    End If
                End If
            End If
            If Tipo = 1 Then
                VALORECONVENZIONALE = Math.Round(CDec(COSTO_BASE_MC * SUP_CONVENZIONALE * DEM * ZONA * PIANO * CONSERVAZIONE * VETUSTA), 2)
            ElseIf Tipo = 2 Then
                VALORECONVENZIONALE = Math.Round(CDec(DEM * ZONA * PIANO * CONSERVAZIONE * VETUSTA), 2)
            End If
            CalcolaValoreConvenzionale = VALORECONVENZIONALE
        Catch ex As Exception
            CalcolaValoreConvenzionale = 0
        End Try
    End Function
    Private Function RicavaDatiInquilino(ByVal IdComponente As Long, ByVal CodFiscale As String, ByRef LuogoNascita As String, ByRef Cittadinanza As Integer, ByVal IdAlloggio As Long, ByRef TipoNucleo As Integer, ByRef Condizione As Integer, ByRef Professione As Integer, ByVal IdDichirazione As Long, ByRef AnnoRedditi As Integer, ByVal Redditi As String, ByVal Bandi As String, ByVal Dichiarazioni As String) As Boolean
        Try
            RicavaDatiInquilino = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            LuogoNascita = Mid(CodFiscale, 12, 4).ToUpper
            par.cmd.CommandText = "SELECT SIGLA FROM COMUNI_NAZIONI WHERE COD = '" & par.PulisciStrSql(LuogoNascita).ToUpper & "'"
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                Select Case Len(par.IfNull(MyReader("SIGLA"), "").ToString.ToUpper)
                    Case 1
                        If par.IfNull(MyReader("SIGLA"), "").ToString.ToUpper = "C" Then
                            Cittadinanza = 2
                        Else
                            Cittadinanza = 3
                        End If
                    Case 2
                        Cittadinanza = 1
                    Case Else
                        Cittadinanza = 1
                End Select
            End If
            MyReader.Close()
            par.cmd.CommandText = "SELECT COD_TIPO_DISPONIBILITA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = " & IdAlloggio
            MyReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                If par.IfNull(MyReader("COD_TIPO_DISPONIBILITA"), "") = "LOCA" Then
                    TipoNucleo = 2
                Else
                    TipoNucleo = 1
                End If
            End If
            MyReader.Close()
            If Not String.IsNullOrEmpty(Redditi) Then
                par.cmd.CommandText = "SELECT DISTINCT CONDIZIONE, PROFESSIONE FROM " & Redditi & " WHERE ID_COMPONENTE = " & IdComponente
                MyReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    Condizione = AssociaCondizione(par.IfNull(MyReader("CONDIZIONE"), 0))
                    Professione = AssociaOccupazione(par.IfNull(MyReader("PROFESSIONE"), 0))
                End If
                MyReader.Close()
            End If
            If Not String.IsNullOrEmpty(Bandi) And Not String.IsNullOrEmpty(Dichiarazioni) Then
                par.cmd.CommandText = "SELECT ANNO_ISEE FROM " & Bandi & " WHERE ID = (SELECT ID_BANDO FROM " & Dichiarazioni & " WHERE ID = " & IdDichirazione & ")"
                MyReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    AnnoRedditi = par.IfNull(MyReader("ANNO_ISEE"), 0)
                End If
                MyReader.Close()
            End If
            RicavaDatiInquilino = True
        Catch ex As Exception
            RicavaDatiInquilino = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - RicavaDatiInquilino - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function AssociaCondizione(ByVal Condizione As Integer) As Integer
        Try
            AssociaCondizione = 7
            Select Case Condizione
                Case 1
                    AssociaCondizione = 1
                Case 2
                    AssociaCondizione = 2
                Case 3
                    AssociaCondizione = 3
                Case 4
                    AssociaCondizione = 4
                Case 5
                    AssociaCondizione = 5
                Case 7
                    AssociaCondizione = 6
                Case Else
                    AssociaCondizione = 7
            End Select
        Catch ex As Exception
            AssociaCondizione = 7
        End Try
    End Function
    Private Function AssociaOccupazione(ByVal Professione As Integer) As Integer
        Try
            AssociaOccupazione = 12
            Select Case Professione
                Case 1
                    AssociaOccupazione = 1
                Case 2
                    AssociaOccupazione = 2
                Case 3
                    AssociaOccupazione = 3
                Case 4
                    AssociaOccupazione = 4
                Case 5
                    AssociaOccupazione = 5
                Case 6
                    AssociaOccupazione = 6
                Case 7
                    AssociaOccupazione = 7
                Case 8
                    AssociaOccupazione = 8
                Case 9
                    AssociaOccupazione = 9
                Case 10
                    AssociaOccupazione = 10
                Case Else
                    AssociaOccupazione = 12
            End Select
        Catch ex As Exception
            AssociaOccupazione = 12
        End Try
    End Function
    Private Function RicavaRedditiInquilino(ByVal IdComponente As Long, ByRef RedditoCompl As Decimal, ByRef RedditoDip As Decimal, ByRef RedditoAuto As Decimal, ByRef RedditoPens As Decimal, ByRef RedditoTerr As Decimal, ByRef RedditoFabbr As Decimal, ByRef RedditoAltro As Decimal, ByRef AltriEmo As Decimal, ByRef RedditoProvAgra As Decimal, ByRef DetrazioniIrpef As Decimal, ByRef DetrazioniSanitarie As Decimal, ByRef DetrazioniDisabili As Decimal, ByVal Redditi As String, ByVal CompReddito As String, ByVal CompAltriRedditi As String, ByVal CompDetrazioni As String) As Boolean
        Try
            RicavaRedditiInquilino = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            par.cmd.CommandText = "SELECT SUM(DIPENDENTE) AS DIPENDENTE, SUM(AUTONOMO) AS AUTONOMO, SUM(PENSIONE) AS PENSIONE, SUM(DOM_AG_FAB) AS FABBRICATI " _
                                & "FROM " & Redditi & " " _
                                & "WHERE ID_COMPONENTE = " & IdComponente
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                RedditoDip = Math.Round(CDec(par.PuntiInVirgole(par.IfNull(MyReader("DIPENDENTE"), 0))), 2)
                RedditoAuto = Math.Round(CDec(par.PuntiInVirgole(par.IfNull(MyReader("AUTONOMO"), 0))), 2)
                RedditoPens = Math.Round(CDec(par.PuntiInVirgole(par.IfNull(MyReader("PENSIONE"), 0))), 2)
                RedditoFabbr = Math.Round(CDec(par.PuntiInVirgole(par.IfNull(MyReader("FABBRICATI"), 0))), 2)
            End If
            MyReader.Close()
            par.cmd.CommandText = "SELECT SUM(PROV_AGRARI) AS PROVENTI FROM " & CompReddito & " WHERE ID_COMPONENTE = " & IdComponente
            MyReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                RedditoProvAgra = Math.Round(CDec(par.PuntiInVirgole(par.IfNull(MyReader("PROVENTI"), 0))), 2)
            End If
            MyReader.Close()
            par.cmd.CommandText = "SELECT SUM(IMPORTO) AS ALTRI FROM " & CompAltriRedditi & " WHERE ID_COMPONENTE = " & IdComponente
            MyReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                RedditoAltro = Math.Round(CDec(par.PuntiInVirgole(par.IfNull(MyReader("ALTRI"), 0))), 2)
            End If
            MyReader.Close()
            par.cmd.CommandText = "SELECT SUM(IMPORTO) AS DETRAZIONI FROM " & CompDetrazioni & " WHERE ID_TIPO = 0 AND ID_COMPONENTE = " & IdComponente
            MyReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                DetrazioniIrpef = Math.Round(CDec(par.PuntiInVirgole(par.IfNull(MyReader("DETRAZIONI"), 0))), 2)
            End If
            MyReader.Close()
            par.cmd.CommandText = "SELECT SUM(IMPORTO) AS DETRAZIONI FROM " & CompDetrazioni & " WHERE ID_TIPO = 1 AND ID_COMPONENTE = " & IdComponente
            MyReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                DetrazioniSanitarie = Math.Round(CDec(par.PuntiInVirgole(par.IfNull(MyReader("DETRAZIONI"), 0))), 2)
            End If
            MyReader.Close()
            par.cmd.CommandText = "SELECT SUM(IMPORTO) AS DETRAZIONI FROM " & CompDetrazioni & " WHERE ID_TIPO = 2 AND ID_COMPONENTE = " & IdComponente
            MyReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                DetrazioniDisabili = Math.Round(CDec(par.PuntiInVirgole(par.IfNull(MyReader("DETRAZIONI"), 0))), 2)
            End If
            MyReader.Close()
            RedditoCompl = Math.Round(CDec(RedditoDip + RedditoAuto + RedditoPens + RedditoFabbr + RedditoProvAgra + RedditoAltro), 2)
            If RedditoCompl > 150000D Then
                RedditoCompl = 150000D
            End If
            RicavaRedditiInquilino = True
        Catch ex As Exception
            RicavaRedditiInquilino = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - RicavaRedditiInquilino - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function RicavaPatrimonioMobiliareInquilino(ByVal IdComponente As Long, ByVal IdInquilino As Long, ByVal AnnoRiferimento As Integer, ByVal CompPatrMob As String) As Boolean
        Try
            RicavaPatrimonioMobiliareInquilino = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            par.cmd.CommandText = "SELECT COD_INTERMEDIARIO, INTERMEDIARIO, IMPORTO " _
                                & "FROM " & CompPatrMob & " " _
                                & "WHERE ID_COMPONENTE = " & IdComponente
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            For Each row As Data.DataRow In dt.Rows
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SIR_INQUILINO_PATR_MOBILIARE (ID, ID_SIRAPER, ID_INQUILINO, ANNO_RIFERIMENTO, CODICE_GESTORE, " _
                                    & "DENOMINAZIONE_GESTORE, IMPORTO_PATRIMONIO, VALORE_PATRIMONIO) VALUES (" _
                                    & "SISCOM_MI.SEQ_SIR_INQUILINO_PATR_MOBIL.NEXTVAL, " & idSiraper.Value & ", " & IdInquilino & ", " & NumeriNull(AnnoRiferimento) & ", '" & par.PulisciStrSql(par.IfNull(row.Item("COD_INTERMEDIARIO"), "")) & "', " _
                                    & "'" & par.PulisciStrSql(par.IfNull(row.Item("INTERMEDIARIO"), "")) & "', " & NumeriNull(Math.Round(par.IfNull(row.Item("IMPORTO"), 0), 2)) & ", null)"
                par.cmd.ExecuteNonQuery()
            Next
            RicavaPatrimonioMobiliareInquilino = True
        Catch ex As Exception
            RicavaPatrimonioMobiliareInquilino = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - RicavaPatrimonioMobiliareInquilino - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function RicavaPatrimonioImmobiliareInquilino(ByVal IdComponente As Long, ByVal IdInquilino As Long, ByVal AnnoRiferimento As Integer, ByVal CompPatrImmob As String) As Boolean
        Try
            RicavaPatrimonioImmobiliareInquilino = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            par.cmd.CommandText = "SELECT ID_TIPO, PERC_PATR_IMMOBILIARE, VALORE, MUTUO " _
                                & "FROM " & CompPatrImmob & " " _
                                & "WHERE ID_COMPONENTE = " & IdComponente
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            For Each row As Data.DataRow In dt.Rows
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SIR_INQUILINO_PATR_IMMOBILIARE (ID, ID_SIRAPER, ID_INQUILINO, ANNO_RIFERIMENTO, TIPO_PATRIMONIO, " _
                                    & "QUOTA_PROPRIETA, VALORE_ICI, QUOTA_MUTUO_RESIDUA) VALUES (" _
                                    & "SISCOM_MI.SEQ_SIR_INQUILINO_PATR_IMMOBIL.NEXTVAL, " & idSiraper.Value & ", " & IdInquilino & ", " & NumeriNull(AnnoRiferimento) & ", " & NumeriNull(RicavaTipoPAtrimonioImmobiliare(par.IfNull(row.Item("ID_TIPO"), 1))) & ", " _
                                    & NumeriNull(Math.Round(par.IfNull(row.Item("PERC_PATR_IMMOBILIARE"), 0), 2).ToString.Replace(".", "")) & ", " & NumeriNull(Math.Round(par.IfNull(row.Item("VALORE"), 0), 2).ToString.Replace(".", "")) & ", " & NumeriNull(Math.Round(par.IfNull(row.Item("MUTUO"), 0), 2).ToString.Replace(".", "")) & ")"
                par.cmd.ExecuteNonQuery()
            Next
            RicavaPatrimonioImmobiliareInquilino = True
        Catch ex As Exception
            RicavaPatrimonioImmobiliareInquilino = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - RicavaPatrimonioImmobiliareInquilino - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function RicavaTipoPAtrimonioImmobiliare(ByVal Tipo As Integer) As Integer
        Try
            RicavaTipoPAtrimonioImmobiliare = 3
            Select Case Tipo
                Case 0
                    RicavaTipoPAtrimonioImmobiliare = 1
                Case 1
                    RicavaTipoPAtrimonioImmobiliare = 3
                Case 2
                    RicavaTipoPAtrimonioImmobiliare = 2
                Case Else
            End Select
        Catch ex As Exception
            RicavaTipoPAtrimonioImmobiliare = 3
        End Try
    End Function
    Private Function RicavaCoefficentiUbicazioneEdificio(ByVal IdEdificio As Long, ByVal Localita As String, ByRef Ubicazione As Integer, ByRef CoefficenteUb As Decimal) As Boolean
        Try
            RicavaCoefficentiUbicazioneEdificio = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            Ubicazione = 0
            CoefficenteUb = 0
            Dim CodTipoUbicazione As String = ""
            Dim CodComune As String = ""
            Dim RicDemografia As Decimal = 0
            Dim Popolazione As Long = 0
            par.cmd.CommandText = "SELECT COD_TIPO_UBICAZIONE_LG_392_78 FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID = " & IdEdificio & ")"
            CodTipoUbicazione = par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "SELECT COD, POPOLAZIONE FROM COMUNI_NAZIONI WHERE NOME = '" & par.PulisciStrSql(Localita.ToUpper) & "'"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                CodComune = par.IfNull(dt.Rows(0).Item("COD"), "")
                Popolazione = par.IfNull(dt.Rows(0).Item("POPOLAZIONE"), 0)
            End If
            If Not String.IsNullOrEmpty(CodTipoUbicazione) And Not String.IsNullOrEmpty(CodComune) Then
                RicDemografia = CDec(par.RicavaDemografia(CodComune))
                If RicDemografia = 0.8D Then
                    Select Case CodTipoUbicazione
                        Case "CESTO"
                            Ubicazione = 8
                        Case "ZAGRA"
                            Ubicazione = 6
                        Case "ZEDIP"
                            Ubicazione = 7
                        Case "ZEDPC"
                            Ubicazione = 7
                        Case "ZOPPA"
                            Ubicazione = 7
                    End Select
                ElseIf RicDemografia = 0.9D Then
                    If Popolazione > 20000 Then
                        Select Case CodTipoUbicazione
                            Case "CESTO"
                                Ubicazione = 5
                            Case "ZAGRA"
                                Ubicazione = 1
                            Case "ZEDIP"
                                Ubicazione = 2
                            Case "ZEDPC"
                                Ubicazione = 3
                            Case "ZOPPA"
                                Ubicazione = 4
                        End Select
                    Else
                        Select Case CodTipoUbicazione
                            Case "CESTO"
                                Ubicazione = 8
                            Case "ZAGRA"
                                Ubicazione = 6
                            Case "ZEDIP"
                                Ubicazione = 7
                            Case "ZEDPC"
                                Ubicazione = 7
                            Case "ZOPPA"
                                Ubicazione = 7
                        End Select
                    End If
                Else
                    Select Case CodTipoUbicazione
                        Case "CESTO"
                            Ubicazione = 5
                        Case "ZAGRA"
                            Ubicazione = 1
                        Case "ZEDIP"
                            Ubicazione = 2
                        Case "ZEDPC"
                            Ubicazione = 3
                        Case "ZOPPA"
                            Ubicazione = 4
                    End Select
                End If
                par.cmd.CommandText = "SELECT VALORE_PER_CANONE FROM SISCOM_MI.TIPO_UBICAZIONE_LG_392_78 WHERE COD = 'ZAGRA'"
                Dim Zagra As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)
                par.cmd.CommandText = "SELECT VALORE_PER_CANONE FROM SISCOM_MI.TIPO_UBICAZIONE_LG_392_78 WHERE COD = 'ZEDIP'"
                Dim Zedip As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)
                par.cmd.CommandText = "SELECT VALORE_PER_CANONE FROM SISCOM_MI.TIPO_UBICAZIONE_LG_392_78 WHERE COD = 'ZEDPC'"
                Dim Zedpc As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)
                par.cmd.CommandText = "SELECT VALORE_PER_CANONE FROM SISCOM_MI.TIPO_UBICAZIONE_LG_392_78 WHERE COD = 'ZOPPA'"
                Dim Zoppa As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)
                par.cmd.CommandText = "SELECT VALORE_PER_CANONE FROM SISCOM_MI.TIPO_UBICAZIONE_LG_392_78 WHERE COD = 'CESTO'"
                Dim Cesto As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)
                If CDec(Zagra) = 0.9D And CDec(Zedip) = 0.9D And CDec(Zedpc) = 1.25D And CDec(Zoppa) = 1.4D And CDec(Cesto) = 1.5D Then
                    Select Case CodTipoUbicazione
                        Case "CESTO"
                            CoefficenteUb = 4
                        Case "ZAGRA"
                            CoefficenteUb = 1
                        Case "ZEDIP"
                            CoefficenteUb = 2
                        Case "ZEDPC"
                            CoefficenteUb = 2
                        Case "ZOPPA"
                            CoefficenteUb = 3
                    End Select
                ElseIf CDec(Zagra) = 0.85D And CDec(Zedip) = 1D And CDec(Zedpc) = 1.3D And CDec(Zoppa) = 1.4D And CDec(Cesto) = 1.5D Then
                    Select Case CodTipoUbicazione
                        Case "CESTO"
                            CoefficenteUb = 10
                        Case "ZAGRA"
                            CoefficenteUb = 5
                        Case "ZEDIP"
                            CoefficenteUb = 6
                        Case "ZEDPC"
                            CoefficenteUb = 7
                        Case "ZOPPA"
                            CoefficenteUb = 9
                    End Select
                ElseIf CDec(Zagra) = 0.85D And CDec(Zedip) = 0.95D And CDec(Zedpc) = 1.1D And CDec(Zoppa) = 1.2D And CDec(Cesto) = 1.3D Then
                    Select Case CodTipoUbicazione
                        Case "CESTO"
                            CoefficenteUb = 15
                        Case "ZAGRA"
                            CoefficenteUb = 11
                        Case "ZEDIP"
                            CoefficenteUb = 12
                        Case "ZEDPC"
                            CoefficenteUb = 13
                        Case "ZOPPA"
                            CoefficenteUb = 14
                    End Select
                ElseIf CDec(Zagra) = 0.85D And CDec(Zedip) = 0.9D And CDec(Zedpc) = 0.9D And CDec(Zoppa) = 0.9D And CDec(Cesto) = 1D Then
                    Select Case CodTipoUbicazione
                        Case "CESTO"
                            CoefficenteUb = 18
                        Case "ZAGRA"
                            CoefficenteUb = 16
                        Case "ZEDIP"
                            CoefficenteUb = 17
                        Case "ZEDPC"
                            CoefficenteUb = 17
                        Case "ZOPPA"
                            CoefficenteUb = 17
                    End Select
                End If
            End If
            RicavaCoefficentiUbicazioneEdificio = True
        Catch ex As Exception
            RicavaCoefficentiUbicazioneEdificio = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - RicavaCoefficentiUbicazioneEdificio - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Class Dichiarazione
        Public IdDichirazioneVSA As Long = -1
        Public IdDomandaVSA As Long = -1
        Public DataInizioVSA As String = ""
        Public DataFineVSA As String = ""
        Public IdDichirazioneAU As Long = -1
        Public IdDomandaAU As Long = -1
        Public DataInizioAU As String = ""
        Public DataFineAU As String = ""
        Public IdDomandaERP As Long = -1
        Public IdDomandaERPVeloce As Long = -1
        Public DataRiferimento As String = ""
    End Class
    Private Function RicavaNameTableInq(ByVal IdContratto As Long, ByRef IdDichiarazione As Long, ByRef CompNucleo As String, ByRef Redditi As String, ByRef Bandi As String, ByRef Dichiarazioni As String, ByRef CompReddito As String, ByRef CompAltriRedditi As String, ByRef CompDetrazioni As String, ByRef CompPatrMob As String, ByRef CompPatrImmob As String, ByRef ElencoSpese As String) As Boolean
        Try
            RicavaNameTableInq = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            Dim Obj As New Dichiarazione
            Obj.DataRiferimento = CStr(CInt(txtAnnoRif.Text) + 1) & "0630"
            Dim TrovataDichirazione As Boolean = False
            Dim Condizione1 As Boolean = False
            Dim Condizione2 As Boolean = False
            '1. CERCO DICHIRAZIONE GESTIONE LOCATARI
            par.cmd.CommandText = "SELECT DICHIARAZIONI_VSA.ID AS IDDICH, DOMANDE_BANDO_VSA.ID AS IDDOM, DICHIARAZIONI_VSA.DATA_INIZIO_VAL, DICHIARAZIONI_VSA.DATA_FINE_VAL " _
                                & "FROM DOMANDE_BANDO_VSA, DICHIARAZIONI_VSA " _
                                & "WHERE DICHIARAZIONI_VSA.ID(+) = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND CONTRATTO_NUM = (SELECT COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = " & IdContratto & ") " _
                                & "AND FL_AUTORIZZAZIONE = 1 " _
                                & "ORDER BY DOMANDE_BANDO_VSA.DATA_PG DESC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                Obj.IdDichirazioneVSA = par.IfNull(dt.Rows(0).Item("IDDICH"), -1)
                Obj.IdDomandaVSA = par.IfNull(dt.Rows(0).Item("IDDOM"), -1)
                Obj.DataInizioVSA = par.IfNull(dt.Rows(0).Item("DATA_INIZIO_VAL"), "")
                Obj.DataFineVSA = par.IfNull(dt.Rows(0).Item("DATA_FINE_VAL"), "")
                TrovataDichirazione = True
            End If
            '2. CERCO DICHIRAZIONE ANAGRAFE UTENZA
            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.ID AS IDDICH, UTENZA_BANDI.ID AS IDDOM, UTENZA_BANDI.DATA_INIZIO, UTENZA_BANDI.DATA_FINE " _
                                & "FROM UTENZA_DICHIARAZIONI, UTENZA_BANDI " _
                                & "WHERE NVL(FL_GENERAZ_AUTO,0) = 0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB <> 'GENERATA_AUTOMATICAMENTE') " _
                                & "AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO AND RAPPORTO = (SELECT COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = " & IdContratto & ") " _
                                & "ORDER BY ID_BANDO DESC"
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dt = New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                Obj.IdDichirazioneAU = par.IfNull(dt.Rows(0).Item("IDDICH"), -1)
                Obj.IdDomandaAU = par.IfNull(dt.Rows(0).Item("IDDOM"), -1)
                Obj.DataInizioAU = par.IfNull(dt.Rows(0).Item("DATA_INIZIO"), "")
                Obj.DataFineAU = par.IfNull(dt.Rows(0).Item("DATA_FINE"), "")
                TrovataDichirazione = True
            End If
            '3. CERCO DICHIRAZIONE BANDO ERP
            If TrovataDichirazione = False Then
                par.cmd.CommandText = "SELECT DOMANDE_BANDO.ID " _
                                    & "FROM DOMANDE_BANDO " _
                                    & "WHERE CONTRATTO_NUM = (SELECT COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = " & IdContratto & ")"
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                dt = New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    Obj.IdDomandaERP = par.IfNull(dt.Rows(0).Item("ID"), -1)
                    TrovataDichirazione = True
                End If
            End If
            '4. CERCO DICHIRAZIONE BANDO ERP VELOCE
            If TrovataDichirazione = False Then
                par.cmd.CommandText = "SELECT DICHIARAZIONI_VSA.ID " _
                                    & "FROM DICHIARAZIONI_VSA " _
                                    & "WHERE DICHIARAZIONI_VSA.ID IN (SELECT ID_ISEE FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & IdContratto & ") "
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                dt = New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    Obj.IdDomandaERPVeloce = par.IfNull(dt.Rows(0).Item("ID"), -1)
                    TrovataDichirazione = True
                End If
            End If
            If TrovataDichirazione Then
                If Obj.IdDichirazioneVSA <> -1 Or Obj.IdDichirazioneAU <> -1 Then
                    If Obj.IdDichirazioneVSA <> -1 And Obj.IdDichirazioneAU <> -1 Then
                        If Obj.DataFineVSA = Obj.DataFineAU Then
                            IdDichiarazione = Obj.IdDichirazioneVSA
                            CompNucleo = "COMP_NUCLEO_VSA"
                            Redditi = "DOMANDE_REDDITI_VSA"
                            Bandi = "BANDI_VSA"
                            Dichiarazioni = "DICHIARAZIONI_VSA"
                            CompReddito = "COMP_REDDITO_VSA"
                            CompAltriRedditi = "COMP_ALTRI_REDDITI_VSA"
                            CompDetrazioni = "COMP_DETRAZIONI_VSA"
                            CompPatrMob = "COMP_PATR_MOB_VSA"
                            CompPatrImmob = "COMP_PATR_IMMOB_VSA"
                            ElencoSpese = "COMP_ELENCO_SPESE_VSA"
                        Else
                            If (Obj.DataRiferimento >= Obj.DataInizioVSA And Obj.DataRiferimento <= Obj.DataFineVSA) Then Condizione1 = True
                            If (Obj.DataRiferimento >= Obj.DataInizioAU And Obj.DataRiferimento <= Obj.DataFineAU) Then Condizione2 = True
                            If Condizione1 = True And Condizione2 = True Then
                                If Obj.DataFineVSA > Obj.DataFineAU Then
                                    IdDichiarazione = Obj.IdDichirazioneVSA
                                    CompNucleo = "COMP_NUCLEO_VSA"
                                    Redditi = "DOMANDE_REDDITI_VSA"
                                    Bandi = "BANDI_VSA"
                                    Dichiarazioni = "DICHIARAZIONI_VSA"
                                    CompReddito = "COMP_REDDITO_VSA"
                                    CompAltriRedditi = "COMP_ALTRI_REDDITI_VSA"
                                    CompDetrazioni = "COMP_DETRAZIONI_VSA"
                                    CompPatrMob = "COMP_PATR_MOB_VSA"
                                    CompPatrImmob = "COMP_PATR_IMMOB_VSA"
                                    ElencoSpese = "COMP_ELENCO_SPESE_VSA"
                                Else
                                    IdDichiarazione = Obj.IdDichirazioneAU
                                    CompNucleo = "UTENZA_COMP_NUCLEO"
                                    Redditi = "UTENZA_REDDITI"
                                    Bandi = "UTENZA_BANDI"
                                    Dichiarazioni = "UTENZA_DICHIARAZIONI"
                                    CompReddito = "UTENZA_COMP_REDDITO"
                                    CompAltriRedditi = "UTENZA_COMP_ALTRI_REDDITI"
                                    CompDetrazioni = "UTENZA_COMP_DETRAZIONI"
                                    CompPatrMob = "UTENZA_COMP_PATR_MOB"
                                    CompPatrImmob = "UTENZA_COMP_PATR_IMMOB"
                                    ElencoSpese = "UTENZA_COMP_ELENCO_SPESE"
                                End If
                            ElseIf Condizione1 = True And Condizione2 = False Then
                                IdDichiarazione = Obj.IdDichirazioneVSA
                                CompNucleo = "COMP_NUCLEO_VSA"
                                Redditi = "DOMANDE_REDDITI_VSA"
                                Bandi = "BANDI_VSA"
                                Dichiarazioni = "DICHIARAZIONI_VSA"
                                CompReddito = "COMP_REDDITO_VSA"
                                CompAltriRedditi = "COMP_ALTRI_REDDITI_VSA"
                                CompDetrazioni = "COMP_DETRAZIONI_VSA"
                                CompPatrMob = "COMP_PATR_MOB_VSA"
                                CompPatrImmob = "COMP_PATR_IMMOB_VSA"
                                ElencoSpese = "COMP_ELENCO_SPESE_VSA"
                            ElseIf Condizione1 = False And Condizione2 = True Then
                                IdDichiarazione = Obj.IdDichirazioneAU
                                CompNucleo = "UTENZA_COMP_NUCLEO"
                                Redditi = "UTENZA_REDDITI"
                                Bandi = "UTENZA_BANDI"
                                Dichiarazioni = "UTENZA_DICHIARAZIONI"
                                CompReddito = "UTENZA_COMP_REDDITO"
                                CompAltriRedditi = "UTENZA_COMP_ALTRI_REDDITI"
                                CompDetrazioni = "UTENZA_COMP_DETRAZIONI"
                                CompPatrMob = "UTENZA_COMP_PATR_MOB"
                                CompPatrImmob = "UTENZA_COMP_PATR_IMMOB"
                                ElencoSpese = "UTENZA_COMP_ELENCO_SPESE"
                            Else
                                IdDichiarazione = Obj.IdDichirazioneVSA
                                CompNucleo = "COMP_NUCLEO_VSA"
                                Redditi = "DOMANDE_REDDITI_VSA"
                                Bandi = "BANDI_VSA"
                                Dichiarazioni = "DICHIARAZIONI_VSA"
                                CompReddito = "COMP_REDDITO_VSA"
                                CompAltriRedditi = "COMP_ALTRI_REDDITI_VSA"
                                CompDetrazioni = "COMP_DETRAZIONI_VSA"
                                CompPatrMob = "COMP_PATR_MOB_VSA"
                                CompPatrImmob = "COMP_PATR_IMMOB_VSA"
                                ElencoSpese = "COMP_ELENCO_SPESE_VSA"
                            End If
                        End If
                    ElseIf Obj.IdDichirazioneVSA <> -1 And Obj.IdDichirazioneAU = -1 Then
                        IdDichiarazione = Obj.IdDichirazioneVSA
                        CompNucleo = "COMP_NUCLEO_VSA"
                        Redditi = "DOMANDE_REDDITI_VSA"
                        Bandi = "BANDI_VSA"
                        Dichiarazioni = "DICHIARAZIONI_VSA"
                        CompReddito = "COMP_REDDITO_VSA"
                        CompAltriRedditi = "COMP_ALTRI_REDDITI_VSA"
                        CompDetrazioni = "COMP_DETRAZIONI_VSA"
                        CompPatrMob = "COMP_PATR_MOB_VSA"
                        CompPatrImmob = "COMP_PATR_IMMOB_VSA"
                        ElencoSpese = "COMP_ELENCO_SPESE_VSA"
                    ElseIf Obj.IdDichirazioneVSA = -1 And Obj.IdDichirazioneAU <> -1 Then
                        IdDichiarazione = Obj.IdDichirazioneAU
                        CompNucleo = "UTENZA_COMP_NUCLEO"
                        Redditi = "UTENZA_REDDITI"
                        Bandi = "UTENZA_BANDI"
                        Dichiarazioni = "UTENZA_DICHIARAZIONI"
                        CompReddito = "UTENZA_COMP_REDDITO"
                        CompAltriRedditi = "UTENZA_COMP_ALTRI_REDDITI"
                        CompDetrazioni = "UTENZA_COMP_DETRAZIONI"
                        CompPatrMob = "UTENZA_COMP_PATR_MOB"
                        CompPatrImmob = "UTENZA_COMP_PATR_IMMOB"
                        ElencoSpese = "UTENZA_COMP_ELENCO_SPESE"
                    End If
                Else
                    If Obj.IdDomandaERP <> -1 Then
                        IdDichiarazione = Obj.IdDomandaERP
                        CompNucleo = "COMP_NUCLEO"
                        Redditi = "DOMANDE_REDDITI"
                        Bandi = "BANDI"
                        Dichiarazioni = "DOMANDE_BANDO"
                        CompReddito = "COMP_REDDITO"
                        CompAltriRedditi = "COMP_ALTRI_REDDITI"
                        CompDetrazioni = "COMP_DETRAZIONI"
                        CompPatrMob = "COMP_PATR_MOB"
                        CompPatrImmob = "COMP_PATR_IMMOB"
                        ElencoSpese = "COMP_ELENCO_SPESE"
                    Else
                        IdDichiarazione = Obj.IdDomandaERPVeloce
                        CompNucleo = "COMP_NUCLEO_VSA"
                        Redditi = "DOMANDE_REDDITI_VSA"
                        Bandi = "BANDI_VSA"
                        Dichiarazioni = "DICHIARAZIONI_VSA"
                        CompReddito = "COMP_REDDITO_VSA"
                        CompAltriRedditi = "COMP_ALTRI_REDDITI_VSA"
                        CompDetrazioni = "COMP_DETRAZIONI_VSA"
                        CompPatrMob = "COMP_PATR_MOB_VSA"
                        CompPatrImmob = "COMP_PATR_IMMOB_VSA"
                        ElencoSpese = "COMP_ELENCO_SPESE_VSA"
                    End If
                End If
            Else
                IdDichiarazione = -1
                CompNucleo = ""
                Redditi = ""
                Bandi = ""
                Dichiarazioni = ""
                CompReddito = ""
                CompAltriRedditi = ""
                CompDetrazioni = ""
                CompPatrMob = ""
                CompPatrImmob = ""
            End If
            RicavaNameTableInq = True
        Catch ex As Exception
            RicavaNameTableInq = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - RicavaNameTableInq - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Function CalcolaISEEDecadenza(ByVal lIdDichiarazione As Long, ByVal CompNucleo As String, ByVal Redditi As String, ByVal Bandi As String, ByVal Dichiarazioni As String, ByVal CompReddito As String, ByVal CompAltriRedditi As String, ByVal CompDetrazioni As String, ByVal CompPatrMob As String, ByVal CompPatrImmob As String, ByVal ElencoSpese As String) As Double
        Try
            Dim DETRAZIONI As Long = 0
            Dim INV_100_CON As Integer = 0
            Dim INV_100_NO As Integer = 0
            Dim INV_66_99 As Integer = 0
            Dim TOT_COMPONENTI As Integer = 0
            Dim REDDITO_COMPLESSIVO As Double = 0
            Dim TOT_SPESE As Long = 0
            Dim DETRAZIONI_FRAGILE As Long = 0
            Dim DETRAZIONI_FR As Long = 0
            Dim MOBILI As Double = 0
            Dim TASSO_RENDIMENTO As Double = 0
            Dim FIGURATIVO_MOBILI As Double = 0
            Dim TOTALE_ISEE_ERP As Double = 0
            Dim IMMOBILI As Long = 0
            Dim MUTUI As Long = 0
            Dim IMMOBILI_RESIDENZA As Long = 0
            Dim MUTUI_RESIDENZA As Long = 0
            Dim TOTALE_PATRIMONIO_ISEE_ERP As Double = 0
            Dim TOTALE_IMMOBILI As Long = 0
            Dim LIMITE_PATRIMONIO As Double = 0
            Dim ISR_ERP As Double = 0
            Dim ISP_ERP As Double = 0
            Dim ISE_ERP As Double = 0
            Dim VSE As Double = 0
            Dim ISEE_ERP As Double = 0
            Dim ESCLUSIONE As String = ""
            Dim PARAMETRO_MINORI As Double = 0
            Dim MINORI As Integer = 0
            Dim adulti As Integer = 0
            Dim limite_isee As Long = 0
            Dim data_pg As String = ""
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            par.cmd.CommandText = "SELECT N_INV_100_CON, N_INV_100_SENZA, N_INV_100_66, DATA_PG, ANNO_SIT_ECONOMICA FROM " & Dichiarazioni & " WHERE ID=" & lIdDichiarazione
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                INV_100_CON = par.IfNull(dt.Rows(0).Item("N_INV_100_CON"), 0)
                INV_100_NO = par.IfNull(dt.Rows(0).Item("N_INV_100_SENZA"), 0)
                INV_66_99 = par.IfNull(dt.Rows(0).Item("N_INV_100_66"), 0)
                data_pg = par.IfNull(dt.Rows(0).Item("DATA_PG"), "")
                TASSO_RENDIMENTO = par.RicavaTasso(par.IfNull(dt.Rows(0).Item("ANNO_SIT_ECONOMICA"), 0))
                limite_isee = 35000
            End If
            par.cmd.CommandText = "SELECT * FROM " & CompNucleo & " WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dt = New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            For Each row As Data.DataRow In dt.Rows
                If par.RicavaEta(row.Item("DATA_NASCITA")) >= 15 Then
                    If par.RicavaEta(row.Item("DATA_NASCITA")) >= 18 Then
                        adulti = adulti + 1
                    End If
                Else
                    MINORI = MINORI + 1
                End If
                DETRAZIONI = 0
                par.cmd.CommandText = "SELECT * FROM " & CompReddito & " WHERE ID_COMPONENTE=" & par.IfNull(row.Item("ID"), -1)
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtRedditi As New Data.DataTable
                da.Fill(dtRedditi)
                da.Dispose()
                For Each rowRedditi As Data.DataRow In dtRedditi.Rows
                    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(rowRedditi.Item("REDDITO_IRPEF"), 0) + par.IfNull(rowRedditi.Item("PROV_AGRARI"), 0)
                Next
                DETRAZIONI_FRAGILE = 0
                par.cmd.CommandText = "SELECT * FROM " & ElencoSpese & " WHERE ID_COMPONENTE=" & par.IfNull(row.Item("ID"), -1)
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                dtRedditi = New Data.DataTable
                da.Fill(dtRedditi)
                da.Dispose()
                If dtRedditi.Rows.Count > 0 Then
                    For Each rowRedditi As Data.DataRow In dtRedditi.Rows
                        DETRAZIONI_FRAGILE = DETRAZIONI_FRAGILE + par.IfNull(rowRedditi.Item("IMPORTO"), 0)
                        TOT_SPESE = TOT_SPESE + par.IfNull(rowRedditi.Item("IMPORTO"), 0)
                        If DETRAZIONI_FRAGILE > 10000 Then
                            DETRAZIONI_FR = DETRAZIONI_FR + DETRAZIONI_FRAGILE
                        Else
                            DETRAZIONI_FR = DETRAZIONI_FR + 10000
                        End If
                    Next
                Else
                    If par.IfNull(row.Item("indennita_acc"), 0) = "1" Then
                        DETRAZIONI_FR = DETRAZIONI_FR + 10000
                        TOT_SPESE = TOT_SPESE + 10000
                    End If
                End If
                par.cmd.CommandText = "SELECT * FROM " & CompPatrMob & " WHERE ID_COMPONENTE=" & par.IfNull(row.Item("ID"), -1)
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                dtRedditi = New Data.DataTable
                da.Fill(dtRedditi)
                da.Dispose()
                If dtRedditi.Rows.Count > 0 Then
                    For Each rowRedditi As Data.DataRow In dtRedditi.Rows
                        MOBILI = MOBILI + par.IfNull(rowRedditi.Item("IMPORTO"), 0)
                    Next
                End If
                par.cmd.CommandText = "SELECT * FROM " & CompPatrImmob & " WHERE ID_COMPONENTE=" & par.IfNull(row.Item("ID"), -1)
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                dtRedditi = New Data.DataTable
                da.Fill(dtRedditi)
                da.Dispose()
                If dtRedditi.Rows.Count > 0 Then
                    For Each rowRedditi As Data.DataRow In dtRedditi.Rows
                        If par.IfNull(rowRedditi.Item("F_RESIDENZA"), 0) = 1 Then
                            IMMOBILI_RESIDENZA = IMMOBILI_RESIDENZA + par.IfNull(rowRedditi.Item("VALORE"), 0)
                            MUTUI_RESIDENZA = MUTUI_RESIDENZA + par.IfNull(rowRedditi.Item("MUTUO"), 0)
                        Else
                            IMMOBILI = IMMOBILI + par.IfNull(rowRedditi.Item("VALORE"), 0)
                            MUTUI = MUTUI + par.IfNull(rowRedditi.Item("MUTUO"), 0)
                        End If
                    Next
                End If
                TOT_COMPONENTI = TOT_COMPONENTI + 1
            Next
            MOBILI = MOBILI - 25000
            If MOBILI < 0 Then MOBILI = 0
            DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)
            FIGURATIVO_MOBILI = (MOBILI \ 5165) * 5165
            ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
            If ISEE_ERP < 0 Then
                ISEE_ERP = 0
            End If
            ISR_ERP = ISEE_ERP
            ISEE_ERP = 0
            TOTALE_IMMOBILI = (IMMOBILI - MUTUI) + (IMMOBILI_RESIDENZA - MUTUI_RESIDENZA) - 25000
            If TOTALE_IMMOBILI < 0 Then TOTALE_IMMOBILI = 0
            TOTALE_ISEE_ERP = (TOTALE_IMMOBILI) * 0.2D
            ISP_ERP = TOTALE_ISEE_ERP
            TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + FIGURATIVO_MOBILI)
            Dim PARAMETRO As Double
            Select Case TOT_COMPONENTI
                Case 1
                    PARAMETRO = 1
                Case 2
                    PARAMETRO = (138 / 100)
                Case 3
                    PARAMETRO = (167 / 100)
                Case 4
                    PARAMETRO = (190 / 100)
                Case 5
                    PARAMETRO = (211 / 100)
                Case Else
                    PARAMETRO = (211 / 100) + ((TOT_COMPONENTI - 5) * (17 / 100))
            End Select
            PARAMETRO_MINORI = 0
            VSE = PARAMETRO
            If adulti >= 2 Then
                VSE = VSE - (MINORI * (1 / 10))
                PARAMETRO_MINORI = (MINORI * (1 / 10))
            End If
            LIMITE_PATRIMONIO = 16000 + (6000 * VSE)
            ISE_ERP = ISR_ERP + ISP_ERP
            ISEE_ERP = ISE_ERP / VSE
            CalcolaISEEDecadenza = ISE_ERP
        Catch ex As Exception
            CalcolaISEEDecadenza = 0
        End Try
    End Function
#End Region
#Region "CreaFile&Esporta"
    Protected Sub btnCreaFile_Click(sender As Object, e As System.EventArgs) Handles btnCreaFile.Click
        Try
            If FileCreato.Value = 0 Then
                If Controllo.Value = 0 Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('L\'elaborazione Siraper non ha ancora superato i controlli di validità!\n Completare i dati e salvare le modifiche!');", True)
                Else
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_SIRAPER_PROCEDURE.NEXTVAL FROM DUAL"
                    Dim idProceduraSiraper As String = par.IfNull(par.cmd.ExecuteScalar, "")
                    Dim Parametri As String = idSiraper.Value & ";" & Elaborazione.Value & ";" & txtAnnoRif.Text & ";" & txtDataRiferimento.Text
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SIRAPER_PROCEDURE (ID, DATA_ORA_INIZIO, ID_OPERATORE, DATA_ORA_FINE, ESITO, ERRORE, " _
                                        & "PARZIALE, TOTALE, TIPO, DESCRIZIONE_ERRORE, PARAMETRI) VALUES " _
                                        & "(" & idProceduraSiraper & ", " & Format(Now, "yyyyMMddHHmmss") & ", " & Session.Item("ID_OPERATORE") & ", '', 0, '', " _
                                        & "0, 100, 3, '', '" & Parametri & "')"
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(True)
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
                        sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idProceduraSiraper
                        p.StartInfo.UseShellExecute = False
                        p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/Siraper.exe")
                        p.StartInfo.Arguments = sParametri
                        p.Start()
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Procedura avviata correttamente!');location.href='Home.aspx';Procedure();", True)
                    Catch ex As Exception
                        connData.apri(False)
                        par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER_PROCEDURE SET ESITO=2,ERRORE='PROCEDURA NON AVVIATA' WHERE ID = " & idProceduraSiraper
                        par.cmd.ExecuteNonQuery()
                        connData.chiudi(False)
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante l\'avvio della procedura!');location.href='Home.aspx';", True)
                    End Try
                End If
            Else
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                    'par.cmd.Transaction = connData.Transazione
                End If
                par.cmd.CommandText = "SELECT NOME_FILE FROM SIRAPER_FILE WHERE ID_SIRAPER = " & idSiraper.Value
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    If File.Exists(Server.MapPath("..\ALLEGATI\" & Session.Item("ComuneCollegato") & "\SIRAPER\") & par.IfNull(MyReader("NOME_FILE"), "")) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../ALLEGATI/" & Session.Item("ComuneCollegato") & "/SIRAPER/" & par.IfNull(MyReader("NOME_FILE"), "") & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Impossibile accedere al file. Contattare l\'amministratore!');", True)
                    End If
                End If
                MyReader.Close()
                par.cmd.CommandText = "INSERT INTO SIRAPER_EVENTI (ID_SIRAPER, ID_OPERATORE, DATA_ORA, COD_EVENTO, DESCRIZIONE) VALUES " _
                                    & "(" & idSiraper.Value & ", " & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'S06', " _
                                    & "'DOWNLOAD FILE XML DATI SIRAPER PER TRASMISSIONE DATI')"
                par.cmd.ExecuteNonQuery()
                FrmSolaLettura(True)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - btnCreaFile_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnEsporta_Click(sender As Object, e As System.EventArgs) Handles btnEsporta.Click
        Try
            Dim DataGridFabbricati As Integer = CType(Me.Tab_Fabbricato1.FindControl("dgvFabbricati"), DataGrid).Items.Count
            Dim DataGridAlloggi As Integer = CType(Me.Tab_Alloggio1.FindControl("dgvAlloggi"), DataGrid).Items.Count
            Dim DataGridInquilini As Integer = CType(Me.Tab_Inquilino1.FindControl("dgvInquilino"), DataGrid).Items.Count
            If DataGridFabbricati <> 0 Or DataGridAlloggi <> 0 Or DataGridInquilini <> 0 Then
                EsportaExcel()
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Nessun dato elaborato!');", True)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - btnEsporta_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub EsportaExcel()
        Try
            Dim FileExcel = xls.IstanziaFileExcel()
            Dim File = xls.IstanziaFile()
            Dim WorkSheet = xls.IstanziaWorkSheet()
            FileExcel = xls.CreaFile(File, ExcelSiSol.Estensione.Office2007_xlsx, "ExportSiraper" & idSiraper.Value)
            WorkSheet = xls.AggiungiNuovoWorkSheetEAssegna(File, WorkSheet, "Fabbricati")
            par.cmd.CommandText = Tab_Fabbricato1.EsportaQuery()
            If String.IsNullOrEmpty(par.cmd.CommandText) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                Exit Sub
            End If
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            xls.LoadExcelFromDT(WorkSheet, dt, True)
            WorkSheet = xls.AggiungiNuovoWorkSheetEAssegna(File, WorkSheet, "Alloggi")
            par.cmd.CommandText = Tab_Alloggio1.EsportaQuery()
            If String.IsNullOrEmpty(par.cmd.CommandText) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                Exit Sub
            End If
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dt = New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            xls.LoadExcelFromDT(WorkSheet, dt, True)
            WorkSheet = xls.AggiungiNuovoWorkSheetEAssegna(File, WorkSheet, "Inquilini")
            par.cmd.CommandText = Tab_Inquilino1.EsportaQuery()
            If String.IsNullOrEmpty(par.cmd.CommandText) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                Exit Sub
            End If
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dt = New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            xls.LoadExcelFromDT(WorkSheet, dt, True)
            xls.SetMetaData(File, FileExcel.NomeFileOriginale, "S&S Sistemi & Soluzioni S.r.l.")
            If xls.ChiudiDocumentoClean(File, FileExcel) Then
                xls.RitornaFileExcel(FileExcel, Me.Page, Me.Page.GetType)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - EsportaExcel - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
#End Region
    Protected Sub btnLastSiraper_Click(sender As Object, e As System.EventArgs) Handles btnLastSiraper.Click
        Try
            If HiddenConf.Value = 1 Then
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                    'par.cmd.Transaction = connData.Transazione
                End If
                'RICAVO L'ULTIMA ELABORAZIONE SIRAPER ANDATA A BUON FINE
                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.SIRAPER WHERE ID = (SELECT MAX(ID) FROM SIRAPER WHERE ESITO_CONTROLLO = 1) ORDER BY ID"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtLastSira As New Data.DataTable
                da.Fill(dtLastSira)
                da.Dispose()
                If dtLastSira.Rows.Count > 0 Then
                    For Each rigaSira As Data.DataRow In dtLastSira.Rows
                        'RICAVO I FABBRICATI DELL'ULTIMA ELABORAZIONE SIRAPER
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIR_FABBRICATO WHERE ID_SIRAPER = " & rigaSira.Item("ID")
                        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtLastFabb As New Data.DataTable
                        da.Fill(dtLastFabb)
                        da.Dispose()
                        For Each rigaFabb As Data.DataRow In dtLastFabb.Rows
                            'AGGIORNO I FABBRICATI IN BASE AI DATI PRESENTI NELL'ULTIMA ELABORAZIONE
                            par.cmd.CommandText = "UPDATE SISCOM_MI.SIR_FABBRICATO " _
                                                & "SET GESTIONE         = " & par.insDbValue(par.IfNull(rigaFabb.Item("GESTIONE").ToString, "-1"), True) & "," _
                                                & "UBICAZIONE           = " & par.insDbValue(par.IfNull(rigaFabb.Item("UBICAZIONE").ToString, "-1"), True) & "," _
                                                & "COEFF_UBICAZIONE     = " & par.insDbValue(par.IfNull(rigaFabb.Item("COEFF_UBICAZIONE").ToString, "-1"), True) & " " _
                                                & "WHERE ID_SIRAPER     = " & par.IfNull(idSiraper.Value, "-1") & " " _
                                                & "AND COD_FABBRICATO   = " & par.IfNull(rigaFabb.Item("COD_FABBRICATO").ToString, "-1")
                            par.cmd.ExecuteNonQuery()
                        Next
                        'RICAVO GLI ALLOGGI DELL'ULTIMA ELABORAZIONE SIRAPER
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIR_ALLOGGIO WHERE ID_SIRAPER = " & rigaSira.Item("ID")
                        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtLastAll As New Data.DataTable
                        da.Fill(dtLastAll)
                        da.Dispose()
                        For Each rigaAll As Data.DataRow In dtLastAll.Rows
                            'AGGIORNO GLI ALLOGGI IN BASE AI DATI PRESENTI NELL'ULTIMA ELABORAZIONE
                            par.cmd.CommandText = "UPDATE SISCOM_MI.SIR_ALLOGGIO " _
                                                & "SET PER_ISTAT_LEGGE27    = " & par.insDbValue(rigaAll.Item("PER_ISTAT_LEGGE27").ToString, False) & "," _
                                                & "ABBATTIMENTO_CANONE      = " & par.insDbValue(par.IfNull(rigaAll.Item("ABBATTIMENTO_CANONE").ToString, "-1"), True) & "," _
                                                & "SOVRAPREZZO_DECADENZA    = " & par.insDbValue(par.IfNull(rigaAll.Item("SOVRAPREZZO_DECADENZA"), "-1").ToString, True) & "," _
                                                & "PERC_AGG_AREA_DEC        = " & par.insDbValue(rigaAll.Item("PERC_AGG_AREA_DEC").ToString, False) & "," _
                                                & "NUM_BOX_CONTR_SEP        = " & par.insDbValue(rigaAll.Item("NUM_BOX_CONTR_SEP").ToString, False) & "," _
                                                & "CANONE_BOX_CONTR_SEP     = " & par.insDbValue(rigaAll.Item("CANONE_BOX_CONTR_SEP").ToString, False) & "," _
                                                & "PSE                      = " & par.insDbValue(rigaAll.Item("PSE").ToString, False) & "," _
                                                & "SUP_CANTINE_SOFF         = " & par.insDbValue(rigaAll.Item("SUP_CANTINE_SOFF").ToString, False) & "," _
                                                & "ISEE_PRON_DECADENZA      = " & par.insDbValue(rigaAll.Item("ISEE_PRON_DECADENZA").ToString, False) & " " _
                                                & "WHERE ID_SIRAPER         = " & par.IfNull(idSiraper.Value, "-1") & " " _
                                                & "AND CODICE_MIR           = " & par.IfNull(rigaAll.Item("CODICE_MIR").ToString, "-1")
                            par.cmd.ExecuteNonQuery()
                        Next
                        'RICAVO GLI INQUILINI DELL'ULTIMA ELABORAZIONE SIRAPER
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SIR_INQUILINO WHERE ID_SIRAPER = " & rigaSira.Item("ID")
                        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtLastInq As New Data.DataTable
                        da.Fill(dtLastInq)
                        da.Dispose()
                        For Each rigaInq As Data.DataRow In dtLastInq.Rows
                            'AGGIORNO GLI INQUILINI IN BASE AI DATI PRESENTI NELL'ULTIMA ELABORAZIONE
                            par.cmd.CommandText = "UPDATE SISCOM_MI.SIR_INQUILINO " _
                                                & "SET FISC_A_CARICO    = " & par.insDbValue(par.IfNull(rigaInq.Item("FISC_A_CARICO").ToString, "-1"), False) & "," _
                                                & "REDD_TERRENI         = " & par.insDbValue(rigaInq.Item("REDD_TERRENI").ToString, False) & "," _
                                                & "EMOLUMENTI           = " & par.insDbValue(rigaInq.Item("EMOLUMENTI").ToString, False) & "," _
                                                & "SUSSIDI              = " & par.insDbValue(rigaInq.Item("SUSSIDI").ToString, False) & "," _
                                                & "REDD_COMPLESSIVO     = " & par.insDbValue(rigaInq.Item("REDD_COMPLESSIVO").ToString, False) & " " _
                                                & "WHERE ID_SIRAPER     = " & par.IfNull(idSiraper.Value, "-1") _
                                                & "AND COD_INQUILINO    = " & par.IfNull(rigaInq.Item("COD_INQUILINO").ToString, "-1")
                            par.cmd.ExecuteNonQuery()
                        Next
                        'RICAVO I DETTAGLI DEL PATRIMONIO MOBILIARE DELL'ULTIMA ELABORAZIONE SIRAPER
                        'par.cmd.CommandText = "SELECT * FROM SIR_INQUILINO_PATR_MOBILIARE WHERE ID_SIRAPER = " & rigaSira.Item("ID")
                        'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        'Dim dtLastMob As New Data.DataTable
                        'da.Fill(dtLastMob)
                        'da.Dispose()
                        'For Each rigaMob As Data.DataRow In dtLastMob.Rows
                        '    'AGGIORNO I DETTAGLI DEL PATRIMONIO MOBILIARE IN BASE ALL'ULTIMA ELABORAZIONE SIRAPER
                        '    par.cmd.CommandText = "UPDATE SIR_INQUILINO_PATR_MOBILIARE " _
                        '                        & "SET ANNO_RIFERIMENTO         = " & par.insDbValue(rigaMob.Item("ANNO_RIFERIMENTO").ToString, False) & "," _
                        '                        & "CODICE_GESTORE               = " & par.insDbValue(par.IfNull(rigaMob.Item("CODICE_GESTORE").ToString, ""), True) & "," _
                        '                        & "DENOMINAZIONE_GESTORE        = " & par.insDbValue(par.IfNull(rigaMob.Item("DENOMINAZIONE_GESTORE").ToString, ""), True) & "," _
                        '                        & "IMPORTO_PATRIMONIO           = " & par.insDbValue(rigaMob.Item("IMPORTO_PATRIMONIO").ToString, False) & "," _
                        '                        & "VALORE_PATRIMONIO            = " & par.insDbValue(par.IfNull(rigaMob.Item("VALORE_PATRIMONIO").ToString, ""), False) & " " _
                        '                        & "WHERE ID_SIRAPER             = " & par.IfNull(idSiraper.Value, "-1") & " " _
                        '                        & "AND ID_INQUILINO             = " & par.IfNull(rigaMob.Item("ID_INQUILINO").ToString, "-1")
                        '    par.cmd.ExecuteNonQuery()
                        'Next
                        ''RICAVO I DETTAGLI DEL PATRIMONIO MOBILIARE DELL'ULTIMA ELABORAZIONE SIRAPER
                        'par.cmd.CommandText = "SELECT * FROM SIR_INQUILINO_PATR_IMMOBILIARE WHERE ID_SIRAPER = " & rigaSira.Item("ID")
                        'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        'Dim dtLastImmob As New Data.DataTable
                        'da.Fill(dtLastImmob)
                        'da.Dispose()
                        'For Each rigaImmob As Data.DataRow In dtLastImmob.Rows
                        '    'RICAVO I DETTAGLI DEL PATRIMONIO MOBILIARE DELL'ULTIMA ELABORAZIONE SIRAPER
                        '    par.cmd.CommandText = "UPDATE SIR_INQUILINO_PATR_IMMOBILIARE " _
                        '                        & "SET ANNO_RIFERIMENTO     = " & par.insDbValue(rigaImmob.Item("ANNO_RIFERIMENTO").ToString, False) & "," _
                        '                        & "TIPO_PATRIMONIO          = " & par.insDbValue(rigaImmob.Item("TIPO_PATRIMONIO").ToString, False) & "," _
                        '                        & "QUOTA_PROPRIETA          = " & par.insDbValue(rigaImmob.Item("QUOTA_PROPRIETA").ToString, False) & "," _
                        '                        & "VALORE_ICI               = " & par.insDbValue(rigaImmob.Item("VALORE_ICI").ToString, False) & "," _
                        '                        & "QUOTA_MUTUO_RESIDUA      = " & par.insDbValue(rigaImmob.Item("QUOTA_MUTUO_RESIDUA").ToString, False) & " " _
                        '                        & "WHERE  ID_SIRAPER        = " & par.IfNull(idSiraper.Value, "-1") & " " _
                        '                        & "AND ID_INQUILINO         = " & par.IfNull(rigaImmob.Item("ID_INQUILINO").ToString, "-1")
                        '    par.cmd.ExecuteNonQuery()
                        'Next
                    Next
                    connData.chiudiTransazione(True)
                    connData.apriTransazione()
                    HiddenConf.Value = 0
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Elaborazione Dati Completata Correttamente!');", True)
                    CaricaDatagrid()
                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - btnUltimoSiraper_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnControllo_Click(sender As Object, e As System.EventArgs) Handles btnControllo.Click
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            If idSiraper.Value <> -1 Then
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_SIRAPER_PROCEDURE.NEXTVAL FROM DUAL"
                Dim idProceduraSiraper As String = par.IfNull(par.cmd.ExecuteScalar, "")
                Dim Parametri As String = idSiraper.Value & ";" & Elaborazione.Value & ";" & txtAnnoRif.Text & ";" & txtDataRiferimento.Text
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SIRAPER_PROCEDURE (ID, DATA_ORA_INIZIO, ID_OPERATORE, DATA_ORA_FINE, ESITO, ERRORE, " _
                                    & "PARZIALE, TOTALE, TIPO, DESCRIZIONE_ERRORE, PARAMETRI) VALUES " _
                                    & "(" & idProceduraSiraper & ", " & Format(Now, "yyyyMMddHHmmss") & ", " & Session.Item("ID_OPERATORE") & ", '', 0, '', " _
                                    & "0, 100, 2, '', '" & Parametri & "')"
                par.cmd.ExecuteNonQuery()
                connData.chiudi(True)
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
                    sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idProceduraSiraper
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/Siraper.exe")
                    p.StartInfo.Arguments = sParametri
                    p.Start()
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Procedura avviata correttamente!');location.href='Home.aspx';Procedure();", True)
                Catch ex As Exception
                    connData.apri(False)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SIRAPER_PROCEDURE SET ESITO=2,ERRORE='PROCEDURA NON AVVIATA' WHERE ID = " & idProceduraSiraper
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(False)
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante l\'avvio della procedura!');location.href='Home.aspx';", True)
                End Try
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - btnElabora_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnScaricaControllo_Click(sender As Object, e As System.EventArgs) Handles btnScaricaControllo.Click
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            If idSiraper.Value <> -1 Then
                Dim NomeFile As String = ""
                par.cmd.CommandText = "SELECT ERRORE " _
                                    & "FROM SISCOM_MI.SIRAPER_PROCEDURE " _
                                    & "WHERE TIPO = 2 AND SUBSTR(PARAMETRI, 1, INSTR(PARAMETRI, ';') - 1) = " & idSiraper.Value & " " _
                                    & "ORDER BY ID DESC"
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    NomeFile = par.IfNull(MyReader("ERRORE"), "")
                End If
                MyReader.Close()
                If Not String.IsNullOrEmpty(Trim(NomeFile)) Then
                    If NomeFile.Contains(".zip") Then
                        If File.Exists(Server.MapPath("../ALLEGATI/SIRAPER/") & NomeFile) Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../ALLEGATI/SIRAPER/" & NomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                        Else
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('File Controllo non Disponibile!');", True)
                        End If
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('File Controllo non Disponibile!');", True)
                    End If
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('File Controllo non Disponibile!');", True)
                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper - btnElabora_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
