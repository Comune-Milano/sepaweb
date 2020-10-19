
Partial Class InserimentoNote
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
            txtDataEvento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            vIdConnessione = Request.QueryString("IDCONN")
            lIdRiferimento = Request.QueryString("IDRIF")
            tipoProvenienza = Request.QueryString("PROV")
            newClasseConn = Request.QueryString("NEWCONN")
            modifica = Request.QueryString("MOD")
            IdNota = Request.QueryString("IDNOTA")
            If modifica = "1" Then
                CaricaInfo()
            End If
            If Val(lIdRiferimento) = 0 Then
                btn_inserisci.Visible = False
                txtDataEvento.Enabled = False
                txtNote.Enabled = False
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "AlertMyScriptKey", "alert('Attenzione...prima di inserire delle note premere SALVA nella maschera dell\'unità!');", True)
            End If
        End If

        SettaControlModifiche(Me)
    End Sub

    Private Sub CaricaInfo()
        connData = New CM.datiConnessione(par, False, False)
        par.cmd = connData.apri(True)

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.GESTIONE_STORICO_NOTE WHERE ID=" & IdNota
        Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If MyReader.Read Then
            txtDataEvento.Text = par.FormattaData(par.IfNull(MyReader("DATA_EVENTO"), ""))
            txtNote.Text = par.IfNull(MyReader("NOTE"), "")
        End If
        MyReader.Close()

        connData.chiudi(True)
    End Sub

    Private Sub SalvaDatiNewConn()
        If controllaSalva.Value = "1" Then
            If newClasseConn = "1" Then
                Me.connData = New CM.datiConnessione(par, False, False)
                If Not (CType(HttpContext.Current.Session.Item(vIdConnessione), CM.datiConnessione)) Is Nothing Then
                    Me.connData.RiempiPar(par)
                    par.SettaCommand(par)
                End If
            End If

            If Len(txtNote.Text) <= 4000 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.GESTIONE_STORICO_NOTE (ID,ID_RIFERIMENTO, NOTE, DATA_ORA,DATA_EVENTO, ID_OPERATORE,ID_PROVENIENZA) " _
                                & "VALUES (SISCOM_MI.SEQ_GESTIONE_STORICO_NOTE.NEXTVAL," & lIdRiferimento & ", '" & par.PulisciStrSql(txtNote.Text) & "', '" & Format(Now, "yyyyMMddHHmmss") & "','" & par.AggiustaData(txtDataEvento.Text) & "', " & Session.Item("ID_OPERATORE") & "," & tipoProvenienza & ")"
                par.cmd.ExecuteNonQuery()

                salvaNote.Value = "1"

                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "self.close();", True)
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "AlertMyScriptKey", "alert('Il campo descrizione non può superare 4000 caratteri!');", True)
            End If
        End If
    End Sub

    Private Sub SalvaDatiOldConn()

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
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.GESTIONE_STORICO_NOTE (ID,ID_RIFERIMENTO, NOTE, DATA_ORA,DATA_EVENTO, ID_OPERATORE,ID_PROVENIENZA) " _
                            & "VALUES (SISCOM_MI.SEQ_GESTIONE_STORICO_NOTE.NEXTVAL," & lIdRiferimento & ", '" & par.PulisciStrSql(txtNote.Text) & "', '" & Format(Now, "yyyyMMddHHmmss") & "','" & par.AggiustaData(txtDataEvento.Text) & "', " & Session.Item("ID_OPERATORE") & "," & tipoProvenienza & ")"
            par.cmd.ExecuteNonQuery()
            salvaNote.Value = "1"
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaNote.Value & ");", True)
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "AlertMyScriptKey", "alert('Il campo descrizione non può superare 4000 caratteri!');", True)
        End If
        

    End Sub

    Private Sub UpdateNewConn()
        If newClasseConn = "1" Then
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not (CType(HttpContext.Current.Session.Item(vIdConnessione), CM.datiConnessione)) Is Nothing Then
                Me.connData.RiempiPar(par)
                par.SettaCommand(par)
            End If
        End If

        If Len(txtNote.Text) <= 4000 Then
            Dim dataOra As String = ""
            par.cmd.CommandText = "select to_char(to_date(data_ora,'yyyyMMdd-HH24miss'),'dd/mm/yyyy - HH24:mi') as DATAORA from SISCOM_MI.GESTIONE_STORICO_NOTE WHERE ID=" & IdNota
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderB.Read Then
                dataOra = par.IfNull(myReaderB("DATAORA"), "")
            End If
            myReaderB.Close()

            par.cmd.CommandText = "UPDATE SISCOM_MI.GESTIONE_STORICO_NOTE set DATA_EVENTO='" & par.AggiustaData(par.IfEmpty(txtDataEvento.Text, "")) & "',NOTE ='" & par.PulisciStrSql(txtNote.Text) & "' WHERE ID=" & IdNota
            par.cmd.ExecuteNonQuery()

            Dim oggetto As String = ""
            'Select Case tipoProvenienza
            '    Case "4"
            '        oggetto = "FABBRICATO"
            '        par.ScriviEventiDemanio(oggetto, "D02", 0, lIdRiferimento, "MODIFICATA NOTA CON DATA-ORA INSERIM. " & dataOra)
            '    Case "5"
            '        oggetto = "UNITA"
            '        par.ScriviEventiDemanio(oggetto, "D02", 0, lIdRiferimento, "MODIFICATA NOTA CON DATA-ORA INSERIM. " & dataOra)
            '    Case "6"
            '        oggetto = "FASCICOLO"
            '        par.ScriviEventiDemanio(oggetto, "D02", lIdRiferimento, 0, "MODIFICATA NOTA CON DATA-ORA INSERIM. " & dataOra)
            'End Select

            salvaNote.Value = "1"

            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaNote.Value & ");", True)
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "AlertMyScriptKey", "alert('Il campo descrizione non può superare 4000 caratteri!');", True)

        End If


        
    End Sub

    Private Sub UpdateOldConn()
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

        Dim dataOra As String = ""
        par.cmd.CommandText = "select to_char(to_date(data_ora,'yyyyMMdd-HH24miss'),'dd/mm/yyyy - HH24:mi') as DATAORA from SISCOM_MI.GESTIONE_STORICO_NOTE WHERE ID=" & IdNota
        Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderB.Read Then
            dataOra = par.IfNull(myReaderB("DATAORA"), "")
        End If
        myReaderB.Close()

        If Len(txtNote.Text) <= 4000 Then
            par.cmd.CommandText = "UPDATE SISCOM_MI.GESTIONE_STORICO_NOTE set DATA_EVENTO='" & par.AggiustaData(par.IfEmpty(txtDataEvento.Text, "")) & "',NOTE ='" & par.PulisciStrSql(txtNote.Text) & "' WHERE ID=" & IdNota
            par.cmd.ExecuteNonQuery()
            Dim oggetto As String = ""
            Select Case tipoProvenienza
                Case "1" 'CONTRATTO
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                       & "VALUES (" & lIdRiferimento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                       & "'F02','MODIFICATA NOTA CON DATA-ORA INSERIM. " & dataOra & "')"
                    par.cmd.ExecuteNonQuery()
                    'Case "2" 'GEST. LOCATARI

                    'Case "3" 'ANAGRAFE UTENZA
                    '    par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                    '     & "VALUES (" & lIdRiferimento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    '     & "'F131','MODIFICATA NOTA CON DATA-ORA INSERIM. " & dataOra & "','I')"
                    '    par.cmd.ExecuteNonQuery()
            End Select
            salvaNote.Value = "1"
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaNote.Value & ");", True)
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "AlertMyScriptKey", "alert('Il campo descrizione non può superare 4000 caratteri!');", True)
        End If
    End Sub

    Protected Sub btn_inserisci_Click(sender As Object, e As System.EventArgs) Handles btn_inserisci.Click
        If controllaSalva.Value = "1" Then
            If newClasseConn = "1" Then
                If modifica = "1" Then
                    UpdateNewConn()
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

    Public Property IdNota() As Long
        Get
            If Not (ViewState("IdNota") Is Nothing) Then
                Return CLng(ViewState("IdNota"))
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Long)
            ViewState("IdNota") = value
        End Set
    End Property
End Class
