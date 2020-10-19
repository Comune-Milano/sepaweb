Partial Class Contratti_InserimentoAvviso
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Not IsPostBack Then

            txtDataPag.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPG.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.txtImporto.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            Me.txtImporto.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

            Me.txtSanzione.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            Me.txtSanzione.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

            Me.txtInteressi.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            Me.txtInteressi.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

            Me.txtSpese.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            Me.txtSpese.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

            vIdConnessione = Request.QueryString("IDCONN")
            lIdRiferimento = Request.QueryString("IDRIF")
            tipoProvenienza = Request.QueryString("PROV")
            newClasseConn = Request.QueryString("NEWCONN")
            modifica = Request.QueryString("MOD")
            IdAvviso = Request.QueryString("IDAVVISO")
            If IdAvviso <> "0" Then
                lblRicevute.Visible = True
            Else
                lblRicevute.Visible = False
            End If
            par.RiempiDList(Me, par.OracleConn, "cmbImposta", "select ID,(codice||'-'||descrizione) as imposta from siscom_mi.tab_cod_tributo ORDER BY codice desc", "IMPOSTA", "ID")
            If modifica = "1" Or modifica = "3" Then
                CaricaInfo()
            End If
            txtModificato.Value = "0"
            If modifica = "3" Then
                btn_inserisci.Visible = False
                cmbImposta.Enabled = False
                txtImporto.Enabled = False
                txtSanzione.Enabled = False
                txtInteressi.Enabled = False
                txtSpese.Enabled = False
                txtDataPag.Enabled = False
                txtDataPG.Enabled = False
                txtNote.Enabled = False
                FileUploadQui.Enabled = False
                FileUploadRic.Enabled = False
                lblRicevute.Visible = False
            End If
        End If

        SettaControlModifiche(Me)
    End Sub

    Private Sub CaricaInfo()
        Dim Connessione As Boolean = False
        Try
            If Not (CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)) Is Nothing Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            ElseIf Not (CType(HttpContext.Current.Session.Item(vIdConnessione), Oracle.DataAccess.Client.OracleConnection)) Is Nothing Then
                par.OracleConn = CType(HttpContext.Current.Session.Item(vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If

            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA_AVVISI_LIQ.*,tab_cod_tributo.id as idt FROM SISCOM_MI.TAB_COD_TRIBUTO,SISCOM_MI.RAPPORTI_UTENZA_AVVISI_LIQ WHERE TAB_COD_TRIBUTO.ID(+)=RAPPORTI_UTENZA_AVVISI_LIQ.IMPOSTA AND RAPPORTI_UTENZA_AVVISI_LIQ.ID=" & IdAvviso
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                cmbImposta.SelectedIndex = -1
                cmbImposta.Items.FindByValue(par.IfNull(MyReader("IDT"), "0")).Selected = True
                txtImporto.Text = Format(par.IfNull(MyReader("IMPORTO"), "0"), "0.00")
                txtSanzione.Text = Format(par.IfNull(MyReader("SANZIONI"), "0"), "0.00")
                txtInteressi.Text = Format(par.IfNull(MyReader("INTERESSI"), "0"), "0.00")
                txtSpese.Text = Format(par.IfNull(MyReader("SPESE_NOTIFICA"), "0"), "0.00")

                txtDataPG.Text = par.FormattaData(par.IfNull(MyReader("DATA_PG"), ""))
                txtDataPag.Text = par.FormattaData(par.IfNull(MyReader("DATA_PAG"), ""))

                txtNote.Text = par.IfNull(MyReader("NOTE"), "")
            End If
            MyReader.Close()


        Catch ex As Exception

            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub SalvaDatiNewConn()
        Dim NomeFileRicevuta As String = ""
        Dim NomeFileQuietanza As String = ""
        Dim Riferimento As String = Format(Now, "yyyyMMddHHmmss")

        Try
            If controllaSalva.Value = "1" Then
                If newClasseConn = "1" Then
                    Me.connData = New CM.datiConnessione(par, False, False)
                    If Not (CType(HttpContext.Current.Session.Item(vIdConnessione), CM.datiConnessione)) Is Nothing Then
                        Me.connData.RiempiPar(par)
                        par.SettaCommand(par)
                    End If
                End If

                If Len(txtNote.Text) <= 4000 Then

                    If FileUploadRic.FileName <> "" Then
                        FileUploadRic.SaveAs(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & Riferimento & "_" & FileUploadRic.FileName))
                        NomeFileRicevuta = FileUploadRic.FileName
                    End If
                    If FileUploadQui.FileName <> "" Then
                        FileUploadQui.SaveAs(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & Riferimento & "_" & FileUploadQui.FileName))
                        NomeFileQuietanza = FileUploadQui.FileName
                    End If


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_AVVISI_LIQ (ID,ID_CONTRATTO,IMPOSTA,SANZIONI,INTERESSI,SPESE_NOTIFICA,DATA_PG,DATA_PAG,NOTE,IMPORTO,RICEVUTA,QUIETANZA) " _
                                    & "VALUES (SISCOM_MI.SEQ_RAPPORTI_UTENZA_LIQ.NEXTVAL," & lIdRiferimento & ", " & cmbImposta.SelectedItem.Value & ", " & par.VirgoleInPunti(txtSanzione.Text) _
                                    & "," & par.VirgoleInPunti(txtInteressi.Text) & ", " & par.VirgoleInPunti(txtSpese.Text) & ",'" & par.AggiustaData(txtDataPG.Text) & "','" _
                                    & par.AggiustaData(txtDataPag.Text) & "','" & par.PulisciStrSql(txtNote.Text) & "'," & par.VirgoleInPunti(txtImporto.Text) & ",'" _
                                    & par.PulisciStrSql(Riferimento & "_" & FileUploadRic.FileName) & "','" & par.PulisciStrSql(Riferimento & "_" & FileUploadQui.FileName) & "')"
                    par.cmd.ExecuteNonQuery()

                    salvaNote.Value = "1"

                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "self.close();", True)
                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "AlertMyScriptKey", "alert('Il campo note non può superare 4000 caratteri!');", True)
                End If
            End If
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub SalvaDatiOldConn()
        Dim NomeFileRicevuta As String = ""
        Dim NomeFileQuietanza As String = ""
        Dim Riferimento As String = Format(Now, "yyyyMMddHHmmss")
        Try
            If Not (CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)) Is Nothing Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            ElseIf Not (CType(HttpContext.Current.Session.Item(vIdConnessione), Oracle.DataAccess.Client.OracleConnection)) Is Nothing Then
                par.OracleConn = CType(HttpContext.Current.Session.Item(vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If

            If Len(txtNote.Text) <= 4000 Then
                If FileUploadRic.FileName <> "" Then
                    FileUploadRic.SaveAs(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & Riferimento & "_" & FileUploadRic.FileName))
                    NomeFileRicevuta = FileUploadRic.FileName
                End If
                If FileUploadQui.FileName <> "" Then
                    FileUploadQui.SaveAs(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & Riferimento & "_" & FileUploadQui.FileName))
                    NomeFileQuietanza = FileUploadQui.FileName
                End If

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_AVVISI_LIQ (ID,ID_CONTRATTO,IMPOSTA,SANZIONI,INTERESSI,SPESE_NOTIFICA,DATA_PG,DATA_PAG,NOTE,IMPORTO,RICEVUTA,QUIETANZA) " _
                                & "VALUES (SISCOM_MI.SEQ_RAPPORTI_UTENZA_LIQ.NEXTVAL," & lIdRiferimento & ", " & cmbImposta.SelectedItem.Value & ", " & par.VirgoleInPunti(Replace(txtSanzione.Text, ".", "")) _
                                & "," & par.VirgoleInPunti(Replace(txtInteressi.Text, ".", "")) & ", " & par.VirgoleInPunti(Replace(txtSpese.Text, ".", "")) & ",'" & par.AggiustaData(txtDataPG.Text) & "','" _
                                & par.AggiustaData(txtDataPag.Text) & "','" & par.PulisciStrSql(txtNote.Text) & "'," & par.VirgoleInPunti(Replace(txtImporto.Text, ".", "")) & ",'" _
                                & par.PulisciStrSql(Riferimento & "_" & FileUploadRic.FileName) & "','" & par.PulisciStrSql(Riferimento & "_" & FileUploadQui.FileName) & "')"
                par.cmd.ExecuteNonQuery()

                Dim DatiAvviso As String = "IMPOSTA:" & Mid(cmbImposta.SelectedItem.Text, 1, 4) & " - IMPORTO:" & txtImporto.Text & " - SANZIONI:" & txtSanzione.Text & " - INTERESSI:" & txtInteressi.Text & " - NOTE:" & Mid(txtNote.Text, 1, 100)

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
               & "VALUES (" & lIdRiferimento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
               & "'F02','INSERIMENTO AVVISO LIQUIDAZIONE : " & par.PulisciStrSql(DatiAvviso) & "')"
                par.cmd.ExecuteNonQuery()

                salvaNote.Value = "1"

                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaNote.Value & ");", True)
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "AlertMyScriptKey", "alert('Il campo note non può superare 4000 caratteri!');", True)
            End If
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub UpdateNewConn()
        'If newClasseConn = "1" Then
        '    Me.connData = New CM.datiConnessione(par, False, False)
        '    If Not (CType(HttpContext.Current.Session.Item(vIdConnessione), CM.datiConnessione)) Is Nothing Then
        '        Me.connData.RiempiPar(par)
        '        par.SettaCommand(par)
        '    End If
        'End If

        'If Len(txtNote.Text) <= 4000 Then
        '    Dim dataOra As String = ""
        '    par.cmd.CommandText = "select to_char(to_date(data_ora,'yyyyMMdd-HH24miss'),'dd/mm/yyyy - HH24:mi') as DATAORA from SISCOM_MI.GESTIONE_STORICO_NOTE WHERE ID=" & IdNota
        '    Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '    If myReaderB.Read Then
        '        dataOra = par.IfNull(myReaderB("DATAORA"), "")
        '    End If
        '    myReaderB.Close()

        '    par.cmd.CommandText = "UPDATE SISCOM_MI.GESTIONE_STORICO_NOTE set DATA_EVENTO='" & par.AggiustaData(par.IfEmpty(txtDataEvento.Text, "")) & "',NOTE ='" & par.PulisciStrSql(txtNote.Text) & "' WHERE ID=" & IdNota
        '    par.cmd.ExecuteNonQuery()

        '    Dim oggetto As String = ""
        '    salvaNote.Value = "1"

        '    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaNote.Value & ");", True)
        'Else
        '    ScriptManager.RegisterStartupScript(Page, GetType(Page), "AlertMyScriptKey", "alert('Il campo descrizione non può superare 4000 caratteri!');", True)

        'End If



    End Sub

    Private Sub UpdateOldConn()
        Dim NomeFileRicevuta As String = ""
        Dim NomeFileQuietanza As String = ""
        Dim Riferimento As String = Format(Now, "yyyyMMddHHmmss")

        Try
            If Not (CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)) Is Nothing Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            ElseIf Not (CType(HttpContext.Current.Session.Item(vIdConnessione), Oracle.DataAccess.Client.OracleConnection)) Is Nothing Then
                par.OracleConn = CType(HttpContext.Current.Session.Item(vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If

            If Len(txtNote.Text) <= 4000 Then

                If FileUploadRic.FileName <> "" Then
                    FileUploadRic.SaveAs(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & Riferimento & "_" & FileUploadRic.FileName))
                    NomeFileRicevuta = FileUploadRic.FileName
                End If
                If FileUploadQui.FileName <> "" Then
                    FileUploadQui.SaveAs(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & Riferimento & "_" & FileUploadQui.FileName))
                    NomeFileQuietanza = FileUploadQui.FileName
                End If

                par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_AVVISI_LIQ SET IMPOSTA=" & cmbImposta.SelectedItem.Value & ",SANZIONI=" & par.VirgoleInPunti(Replace(txtSanzione.Text, ".", "")) _
                                    & ",INTERESSI=" & par.VirgoleInPunti(Replace(txtInteressi.Text, ".", "")) & ",SPESE_NOTIFICA=" & par.VirgoleInPunti(Replace(txtSpese.Text, "'.'", "")) _
                                    & ",DATA_PG='" & par.AggiustaData(txtDataPG.Text) & "',DATA_PAG='" & par.AggiustaData(txtDataPag.Text) _
                                    & "',NOTE='" & par.PulisciStrSql(txtNote.Text) & "',IMPORTO=" & par.VirgoleInPunti(Replace(txtImporto.Text, ".", "")) _
                                    & ",RICEVUTA='" & par.PulisciStrSql(Riferimento & "_" & FileUploadRic.FileName) & "'," _
                                    & "QUIETANZA='" & par.PulisciStrSql(Riferimento & "_" & FileUploadQui.FileName) & "' " _
                                    & " WHERE ID=" & IdAvviso
                par.cmd.ExecuteNonQuery()

                Dim DatiAvviso As String = "IMPOSTA:" & Mid(cmbImposta.SelectedItem.Text, 1, 4) & " - IMPORTO:" & txtImporto.Text & " - SANZIONI:" & txtSanzione.Text & " - INTERESSI:" & txtInteressi.Text & " - NOTE:" & Mid(txtNote.Text, 1, 100)

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
               & "VALUES (" & lIdRiferimento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
               & "'F02','AGGIORNAMENTO AVVISO LIQUIDAZIONE : " & par.PulisciStrSql(DatiAvviso) & "')"
                par.cmd.ExecuteNonQuery()

                salvaNote.Value = "1"

                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaNote.Value & ");", True)
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "AlertMyScriptKey", "alert('Il campo note non può superare 4000 caratteri!');", True)
            End If

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
        'If Not (CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)) Is Nothing Then
        '    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        '    par.SettaCommand(par)
        'ElseIf Not (CType(HttpContext.Current.Session.Item(vIdConnessione), Oracle.DataAccess.Client.OracleConnection)) Is Nothing Then
        '    par.OracleConn = CType(HttpContext.Current.Session.Item(vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        '    par.SettaCommand(par)
        'End If

        'If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
        '    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        'End If

        'Dim dataOra As String = ""
        'par.cmd.CommandText = "select to_char(to_date(data_ora,'yyyyMMdd-HH24miss'),'dd/mm/yyyy - HH24:mi') as DATAORA from SISCOM_MI.GESTIONE_STORICO_NOTE WHERE ID=" & IdNota
        'Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        'If myReaderB.Read Then
        '    dataOra = par.IfNull(myReaderB("DATAORA"), "")
        'End If
        'myReaderB.Close()

        'If Len(txtNote.Text) <= 4000 Then
        '    par.cmd.CommandText = "UPDATE SISCOM_MI.GESTIONE_STORICO_NOTE set DATA_EVENTO='" & par.AggiustaData(par.IfEmpty(txtDataEvento.Text, "")) & "',NOTE ='" & par.PulisciStrSql(txtNote.Text) & "' WHERE ID=" & IdNota
        '    par.cmd.ExecuteNonQuery()
        '    Dim oggetto As String = ""
        '    Select Case tipoProvenienza
        '        Case "1" 'CONTRATTO
        '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
        '               & "VALUES (" & lIdRiferimento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
        '               & "'F02','MODIFICATA NOTA CON DATA-ORA INSERIM. " & dataOra & "')"
        '            par.cmd.ExecuteNonQuery()
        '            'Case "2" 'GEST. LOCATARI

        '            'Case "3" 'ANAGRAFE UTENZA
        '            '    par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
        '            '     & "VALUES (" & lIdRiferimento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
        '            '     & "'F131','MODIFICATA NOTA CON DATA-ORA INSERIM. " & dataOra & "','I')"
        '            '    par.cmd.ExecuteNonQuery()
        '    End Select
        '    salvaNote.Value = "1"
        '    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaNote.Value & ");", True)
        'Else
        '    ScriptManager.RegisterStartupScript(Page, GetType(Page), "AlertMyScriptKey", "alert('Il campo descrizione non può superare 4000 caratteri!');", True)
        'End If
    End Sub

    Protected Sub btn_inserisci_Click(sender As Object, e As EventArgs) Handles btn_inserisci.Click
        Dim sErrore As String = ""
        controllaSalva.Value = "0"
        If txtImporto.Text = "" Or txtSanzione.Text = "" Or txtInteressi.Text = "" Or txtSpese.Text = "" Or txtDataPag.Text = "" Or txtDataPG.Text = "" Or txtNote.Text = "" Then
            controllaSalva.Value = "1"
            sErrore = "Errore Dati: Tutti i campi devono essere valorizzati!\n"
        End If
        If FileUploadRic.HasFile = True Then
            If UCase(Mid(FileUploadRic.FileName, Len(FileUploadRic.FileName) - 2, 3)) <> "PDF" Then
                sErrore = sErrore & "Errore Ricevuta: Tipo file non valido! E' richiesto un file .pdf "
                controllaSalva.Value = "1"
            End If
        End If
        If FileUploadQui.HasFile = True Then
            If UCase(Mid(FileUploadQui.FileName, Len(FileUploadQui.FileName) - 2, 3)) <> "PDF" Then
                sErrore = sErrore & "Errore Quietanza: Tipo file non valido! E' richiesto un file .pdf "
                controllaSalva.Value = "1"
            End If
        End If

        If controllaSalva.Value = "0" Then
            If newClasseConn = "1" Then
                If modifica = "1" Then
                    '            UpdateNewConn()
                Else
                    SalvaDatiNewConn()
                End If
            Else
                If modifica = "1" Then
                    UpdateOldConn()
                Else
                    SalvaDatiOldConn()
                End If
            End If
        Else
            'ScriptManager.RegisterStartupScript(Page, GetType(Page), "AlertMyScriptKey", "alert('" & sErrore & "');", True)
            lblErrore.Visible = True
            lblErrore.Text = sErrore
        End If
    End Sub

    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next
    End Sub

    Public Property vIdConnessione() As String
        Get
            If Not (ViewState("par_vIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnessione") = value
        End Set

    End Property

    Public Property lIdRiferimento() As Long
        Get
            If Not (ViewState("par_lIdRiferimento") Is Nothing) Then
                Return CLng(ViewState("par_lIdRiferimento"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdRiferimento") = value
        End Set

    End Property

    Public Property tipoProvenienza() As String
        Get
            If Not (ViewState("par_tipoProvenienza") Is Nothing) Then
                Return CStr(ViewState("par_tipoProvenienza"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_tipoProvenienza") = value
        End Set

    End Property

    Public Property newClasseConn() As String
        Get
            If Not (ViewState("par_newClasseConn") Is Nothing) Then
                Return CStr(ViewState("par_newClasseConn"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_newClasseConn") = value
        End Set

    End Property

    Public Property modifica() As String
        Get
            If Not (ViewState("par_modifica") Is Nothing) Then
                Return CStr(ViewState("par_modifica"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_modifica") = value
        End Set

    End Property

    Public Property IdAvviso() As Long
        Get
            If Not (ViewState("IdAvviso") Is Nothing) Then
                Return CLng(ViewState("IdAvviso"))
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Long)
            ViewState("IdAvviso") = value
        End Set
    End Property
End Class
