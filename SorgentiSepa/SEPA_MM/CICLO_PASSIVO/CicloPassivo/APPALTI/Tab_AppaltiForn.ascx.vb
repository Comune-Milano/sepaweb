Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_AppaltiForn
    Inherits System.Web.UI.UserControl
    Dim par As New CM.Global

    Private Property idLotti() As Long
        Get
            If Not (ViewState("par_idLotti") Is Nothing) Then
                Return CLng(ViewState("par_idLotti"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idLotti") = value
        End Set

    End Property
    Private Property DescPF() As String
        Get
            If Not (ViewState("par_DescPf") Is Nothing) Then
                Return CStr(ViewState("par_DescPf"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DescPf") = value
        End Set

    End Property

    Public Property IdConnessione() As String
        Get
            If Not (ViewState("par_Connessione") Is Nothing) Then
                Return CStr(ViewState("par_Connessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Connessione") = value
        End Set

    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            IdConnessione = CType(Me.Page.FindControl("txtConnessione"), HiddenField).Value
            par.caricaComboTelerik("SELECT ID,COD_FORNITORE||'-'||RAGIONE_SOCIALE AS FORNITORE FROM SISCOM_MI.FORNITORI WHERE FL_BLOCCATO=0 ORDER BY RAGIONE_SOCIALE ASC", cmbAggFornitori, "id", "fornitore", True)
            'peppe modify intervengo nel commentare le righe che scrivono impossibile visualizzare ma non ho ben capito
            ' a cosa servisse questo if....la connessione di solito si setta nel aspx principale...gli ascx la usano
            'oppure per gli ascx che leggono dati e basta se ne crea una e la si chiude immediatamente

            If par.OracleConn.State = Data.ConnectionState.Open Then
                'Response.Write("IMPOSSIBILE VISUALIZZARE")
                'Exit Sub
            Else
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If
            DataGridFornitori.Rebind()
        End If
        If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Then
            FrmSolaLettura()
        End If

        'If CType(Me.Page.FindControl("DropDownListTipo"), Telerik.Web.UI.RadComboBox).SelectedValue = "1" Then
        '    nascondiFornitori()
        'Else
        '    mostraFornitori()
        'End If
    End Sub



    

    Private Sub FrmSolaLettura()
        Try
            DataGridFornitori.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
            DataGridFornitori.MasterTableView.Columns.FindByUniqueName("modificaFornitore").Visible = False
            DataGridFornitori.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
            DataGridFornitori.Rebind()
            'Me.btnAggAppalti.Visible = False
            btnInserisciFornitore.Enabled = False
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
            For Each CTRL In Me.PanelFornitori.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is RadComboBox Then
                    DirectCast(CTRL, RadComboBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadButton Then
                    DirectCast(CTRL, RadButton).Enabled = False
                End If
            Next
            imgEsciForn.Enabled = True
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub

    Public Sub nascondiFornitori()
        Session.Remove("ATI_FORNITORI")

        DataGridFornitori.Enabled = False
        DataGridFornitori.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
        'DataGridFornitori.Rebind()
        'btnEliminaFornitore.Style.Value = "visibility: hidden;"
        'btnModificaFornitore.Style.Value = "visibility: hidden;"


    End Sub
    Public Sub mostraFornitori()

        DataGridFornitori.Enabled = True
        DataGridFornitori.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top
        'DataGridFornitori.Rebind()
        'PanelFornitori.Visible = True
        'btnEliminaFornitore.Style.Value = "visibility: visible;"
        'btnModificaFornitore.Style.Value = "visibility: visible;"
    End Sub


    Protected Sub btnInserisciFornitore_Click(sender As Object, e As System.EventArgs) Handles btnInserisciFornitore.Click
        Try
            If txtTipoF.Value = "1" Then
                '***************************************** MOFIDICA *****************************************
                If IsNumeric(CType(Me.Page, Object).vIdAppalti) AndAlso CInt(CType(Me.Page, Object).vIdAppalti) > 0 Then
                    '******************* APPALTO ESISTENTE *******************
                    If cmbAggFornitori.SelectedValue <> "-1" Then
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                        Dim capofila As Integer = 0
                        If chkCapofila.Checked = True Then
                            capofila = 1
                        End If

                        'controllo se sto modificando lo stesso fornitore
                        If cmbAggFornitori.SelectedValue = txtIdFornitore.Value Then
                            'sto aggiornando lo stesso fornitore
                            'se il capofila è diverso faccio aggiornamento capofila
                            If capofila = 1 Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_FORNITORI " _
                                    & " SET CAPOFILA=0" _
                                    & " WHERE ID_FORNITORE<>" & cmbAggFornitori.SelectedValue _
                                    & " AND ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti
                                par.cmd.ExecuteNonQuery()
                            End If
                            par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_FORNITORI " _
                                & " SET CAPOFILA=" & capofila _
                                & " WHERE ID_FORNITORE=" & txtIdFornitore.Value _
                                & " AND ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti _
                                & " AND CAPOFILA<>" & capofila
                            Dim ris As Integer = par.cmd.ExecuteNonQuery()
                            If ris = 1 Then
                                ' Response.Write("<script>alert('Fornitore aggiornato correttamente!');</script>")
                                DataGridFornitori.Rebind()
                                '   impostaModalitaPagamento()
                            Else
                                Response.Write("<script>alert('Fornitore già presente nell\'ATI!');</script>")
                            End If
                        Else
                            'non sto aggiornando lo stesso fornitore
                            par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPALTI_FORNITORI " _
                                & " WHERE ID_FORNITORE=" & cmbAggFornitori.SelectedValue _
                                & " AND ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
                            Dim contaElementiEsistenti As Integer = par.cmd.ExecuteScalar
                            'controllo che il fornitore non esista già
                            If contaElementiEsistenti > 0 Then
                                'il fornitore esiste
                                Response.Write("<script>alert('Fornitore già presente nell\'ATI!');</script>")
                            Else
                                'il fornitore non esiste
                                par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_FORNITORI " _
                                    & " SET ID_FORNITORE=" & cmbAggFornitori.SelectedValue _
                                    & " ,CAPOFILA=" & capofila _
                                    & " WHERE ID_FORNITORE=" & txtIdFornitore.Value _
                                    & " AND ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti
                                par.cmd.ExecuteNonQuery()
                                'aggiorno tutti i capofila
                                If capofila = 1 Then
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_FORNITORI " _
                                        & " SET CAPOFILA=0" _
                                        & " WHERE ID_FORNITORE<>" & cmbAggFornitori.SelectedValue _
                                        & " AND ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti
                                    par.cmd.ExecuteNonQuery()
                                End If
                                'Response.Write("<script>alert('Fornitore aggiornato correttamente!');</script>")
                                DataGridFornitori.Rebind()
                                'DataGridFornitori.DataBind()
                                ' impostaModalitaPagamento()
                            End If
                        End If
                    Else
                        Response.Write("<script>alert('Selezionare un fornitore!');</script>")
                    End If
                    '******************* APPALTO ESISTENTE *******************
                Else
                    '******************* APPALTO DA INSERIRE *******************

                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                    If cmbAggFornitori.SelectedValue <> "-1" Then
                        Dim dt As Data.DataTable
                        If Not IsNothing(Session.Item("ATI_FORNITORI")) Then
                            dt = CType(Session.Item("ATI_FORNITORI"), Data.DataTable)
                        Else
                            dt = New Data.DataTable
                            dt.Columns.Add("ID")
                            dt.Columns.Add("CODICE")
                            dt.Columns.Add("FORNITORE")
                            dt.Columns.Add("INDIRIZZO")
                            dt.Columns.Add("IBAN")
                            dt.Columns.Add("CAPOFILA")
                            dt.Columns.Add("ID_TIPO_MODALITA_PAG")
                            dt.Columns.Add("ID_TIPO_PAGAMENTO")
                        End If

                        Dim capofila As String = ""
                        If chkCapofila.Checked = True Then
                            capofila = "C"
                        End If

                        'controllo se sto modificando lo stesso fornitore
                        If cmbAggFornitori.SelectedValue = txtIdFornitore.Value Then
                            'sto modificando lo stesso fornitore
                            'se capofila è 1 faccio aggiornamento tutti gli altri capofila
                            If capofila = "C" Then
                                For Each item As Data.DataRow In dt.Rows
                                    If item.Item("ID") <> cmbAggFornitori.SelectedValue Then
                                        item.Item("CAPOFILA") = ""
                                    Else
                                        item.Item("CAPOFILA") = "C"
                                    End If
                                Next
                            End If
                            ' Response.Write("<script>alert('Fornitore aggiornato correttamente!');</script>")
                            Session.Item("ATI_FORNITORI") = dt
                            DataGridFornitori.DataSource = dt
                            DataGridFornitori.Rebind()
                            ' impostaModalitaPagamento()
                        Else
                            'non sto modificando lo stesso fornitore
                            'controllo che il fornitore non sia già presente
                            Dim fornitorePresente As Boolean = False
                            For Each elemento As GridDataItem In DataGridFornitori.Items
                                If elemento.Cells(par.IndRDGC(DataGridFornitori, "ID")).Text = cmbAggFornitori.SelectedValue Then
                                    fornitorePresente = True
                                End If
                            Next
                            If fornitorePresente Then
                                'fornitore presente
                                Response.Write("<script>alert('Fornitore già presente nell\'ATI!');</script>")
                            Else
                                'fornitore non presente
                                par.cmd.CommandText = "SELECT FORNITORI.ID,COD_FORNITORE AS CODICE,RAGIONE_SOCIALE AS FORNITORE " _
                                    & " ,INDIRIZZO || ' '||CIVICO||' - '||CAP||' '||COMUNE AS INDIRIZZO " _
                                    & " ,(SELECT IBAN FROM SISCOM_MI.FORNITORI_IBAN WHERE FL_ATTIVO=1 AND FORNITORI_IBAN.ID_FORNITORE=FORNITORI.ID) AS IBAN,ID_TIPO_MODALITA_PAG,ID_TIPO_PAGAMENTO " _
                                    & " FROM SISCOM_MI.FORNITORI,SISCOM_MI.FORNITORI_INDIRIZZI WHERE FORNITORI.ID=" & cmbAggFornitori.SelectedValue _
                                    & " AND FORNITORI.ID=FORNITORI_INDIRIZZI.ID_FORNITORE "
                                Dim lettoref As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                If lettoref.Read Then
                                    For Each item As Data.DataRow In dt.Rows
                                        If item.Item("ID") = txtIdFornitore.Value Then
                                            item.Item("ID") = par.IfNull(lettoref("ID"), "")
                                            item.Item("CODICE") = par.IfNull(lettoref("CODICE"), "")
                                            item.Item("FORNITORE") = par.IfNull(lettoref("FORNITORE"), "")
                                            item.Item("INDIRIZZO") = par.IfNull(lettoref("INDIRIZZO"), "")
                                            item.Item("IBAN") = par.IfNull(lettoref("IBAN"), "")
                                            item.Item("ID_TIPO_MODALITA_PAG") = par.IfNull(lettoref("ID_TIPO_MODALITA_PAG"), "")
                                            item.Item("ID_TIPO_PAGAMENTO") = par.IfNull(lettoref("ID_TIPO_PAGAMENTO"), "")
                                            item.Item("CAPOFILA") = capofila
                                        End If
                                    Next
                                End If
                                lettoref.Close()
                                '  Response.Write("<script>alert('Fornitore aggiornato correttamente!');</script>")
                                Session.Item("ATI_FORNITORI") = dt
                                DataGridFornitori.DataSource = dt
                                DataGridFornitori.Rebind()
                                ' impostaModalitaPagamento()
                            End If
                        End If
                    Else
                        Response.Write("<script>alert('Selezionare un fornitore!');</script>")
                    End If
                End If
                txtIdFornitore.Value = ""
            Else
                '******************* INSERIMENTO *******************
                If IsNumeric(CType(Me.Page, Object).vIdAppalti) AndAlso CInt(CType(Me.Page, Object).vIdAppalti) > 0 Then
                    '******************* APPALTO ESISTENTE *******************
                    If cmbAggFornitori.SelectedValue <> "-1" Then
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                        Dim capofila As Integer = 0
                        If chkCapofila.Checked = True Then
                            capofila = 1
                        End If

                        'controllo se il fornitore esiste
                        par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.APPALTI_FORNITORI " _
                            & " WHERE ID_FORNITORE=" & cmbAggFornitori.SelectedValue _
                            & " AND ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti
                        Dim ris As Integer = par.cmd.ExecuteScalar
                        If ris > 0 Then
                            'il fornitore esiste già
                            Response.Write("<script>alert('Fornitore già presente nell\'ATI!');</script>")
                        Else
                            'il fornitore non esiste e lo inserisco
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_FORNITORI (ID_APPALTO,ID_FORNITORE,CAPOFILA) VALUES (" & CType(Me.Page, Object).vIdAppalti & "," & cmbAggFornitori.SelectedValue & "," & capofila & ")"
                            par.cmd.ExecuteNonQuery()
                            ' Response.Write("<script>alert('Fornitore aggiunto correttamente!');</script>")
                            DataGridFornitori.Rebind()
                            '   impostaModalitaPagamento()
                        End If
                    Else
                        Response.Write("<script>alert('Selezionare un fornitore!');</script>")
                    End If
                    '******************* APPALTO ESISTENTE *******************
                Else

                    If cmbAggFornitori.SelectedValue <> "-1" Then
                        '******************* INSERIMENTO *******************
                        'se non esiste ATI_FORNITORI la creo
                        Dim dt As Data.DataTable
                        If Not IsNothing(Session.Item("ATI_FORNITORI")) Then
                            dt = CType(Session.Item("ATI_FORNITORI"), Data.DataTable)
                        Else
                            dt = New Data.DataTable
                            dt.Columns.Add("ID")
                            dt.Columns.Add("CODICE")
                            dt.Columns.Add("FORNITORE")
                            dt.Columns.Add("INDIRIZZO")
                            dt.Columns.Add("IBAN")
                            dt.Columns.Add("CAPOFILA")
                            dt.Columns.Add("ID_TIPO_MODALITA_PAG")
                            dt.Columns.Add("ID_TIPO_PAGAMENTO")
                        End If
                        Dim riga As Data.DataRow
                        riga = dt.NewRow
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                        par.cmd.CommandText = "SELECT FORNITORI.ID,COD_FORNITORE AS CODICE,RAGIONE_SOCIALE AS FORNITORE " _
                            & " ,INDIRIZZO || ' '||CIVICO||' - '||CAP||' '||COMUNE AS INDIRIZZO " _
                            & " ,(SELECT IBAN FROM SISCOM_MI.FORNITORI_IBAN WHERE FL_ATTIVO=1 AND FORNITORI_IBAN.ID_FORNITORE=FORNITORI.ID) AS IBAN,ID_TIPO_MODALITA_PAG,ID_TIPO_PAGAMENTO  " _
                            & " FROM SISCOM_MI.FORNITORI,SISCOM_MI.FORNITORI_INDIRIZZI WHERE FORNITORI.ID=" & cmbAggFornitori.SelectedValue _
                            & " AND FORNITORI.ID=FORNITORI_INDIRIZZI.ID_FORNITORE "
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        Dim presente As Boolean = False
                        If lettore.Read Then
                            riga.Item("ID") = par.IfNull(lettore("ID"), "")
                            riga.Item("CODICE") = par.IfNull(lettore("CODICE"), "")
                            For Each elemento As GridDataItem In DataGridFornitori.Items
                                If elemento.Cells(par.IndRDGC(DataGridFornitori, "CODICE")).Text = riga.Item("CODICE") Then
                                    presente = True
                                End If
                            Next
                            riga.Item("FORNITORE") = par.IfNull(lettore("FORNITORE"), "")
                            riga.Item("INDIRIZZO") = par.IfNull(lettore("INDIRIZZO"), "")
                            riga.Item("IBAN") = par.IfNull(lettore("IBAN"), "")
                            riga.Item("ID_TIPO_MODALITA_PAG") = par.IfNull(lettore("ID_TIPO_MODALITA_PAG"), "")
                            riga.Item("ID_TIPO_PAGAMENTO") = par.IfNull(lettore("ID_TIPO_PAGAMENTO"), "")
                        End If
                        lettore.Close()
                        Dim capofila As String = ""
                        If chkCapofila.Checked = True Then
                            capofila = "C"
                        End If
                        riga.Item("CAPOFILA") = capofila
                        If capofila = "C" Then
                            For Each elemento As Data.DataRow In dt.Rows
                                elemento.Item("CAPOFILA") = ""
                            Next
                        End If
                        If Not presente Then
                            dt.Rows.Add(riga)
                            Session.Item("ATI_FORNITORI") = dt
                            DataGridFornitori.DataSource = dt
                            DataGridFornitori.Rebind()
                            '  impostaModalitaPagamento()
                        Else
                            Response.Write("<script>alert('Fornitore già presente nell\'ATI!');</script>")
                        End If
                    Else
                        Response.Write("<script>alert('Selezionare un fornitore!');</script>")
                    End If
                End If
            End If
            txtTipoF.Value = "0"
            cmbAggFornitori.SelectedValue = "-1"
            chkCapofila.Checked = False
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try
    End Sub

    Protected Sub imgEsciForn_Click(sender As Object, e As System.EventArgs) Handles imgEsciForn.Click
        Try
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Me.txtIdFornitore.Value = ""
            txtSelFornitori.Text = ""
            cmbAggFornitori.SelectedValue = "-1"
            chkCapofila.Checked = False
            If IsNothing(Session.Item("ATI_FORNITORI")) Then
                DataGridFornitori.Rebind()
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try
    End Sub


    Protected Sub DataGridFornitori_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGridFornitori.ItemCommand
        Try
            Select Case e.CommandName
                Case "myEdit"
                    txtTipoF.Value = "1"
                    ApriFornitore()
                Case "Delete"
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    If IsNumeric(txtIdFornitore.Value) AndAlso CInt(txtIdFornitore.Value) > 0 Then
                        If IsNumeric(CType(Me.Page, Object).vIdAppalti) AndAlso CInt(CType(Me.Page, Object).vIdAppalti) > 0 Then
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_FORNITORI WHERE ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " AND ID_FORNITORE=" & txtIdFornitore.Value
                            Dim ris As Integer = 0
                            ris = par.cmd.ExecuteNonQuery()
                            'If ris = 1 Then
                            '    Response.Write("<script>alert('Fornitore eliminato');</script>")
                            'End If
                            DataGridFornitori.Rebind()
                        Else
                            Dim nuovaDt As New Data.DataTable
                            nuovaDt.Columns.Add("ID")
                            nuovaDt.Columns.Add("CODICE")
                            nuovaDt.Columns.Add("FORNITORE")
                            nuovaDt.Columns.Add("INDIRIZZO")
                            nuovaDt.Columns.Add("IBAN")
                            nuovaDt.Columns.Add("CAPOFILA")
                            nuovaDt.Columns.Add("ID_TIPO_MODALITA_PAG")
                            nuovaDt.Columns.Add("ID_TIPO_PAGAMENTO")
                            Dim riga As Data.DataRow
                            For Each elemento As GridDataItem In DataGridFornitori.Items
                                If CInt(elemento.Cells(par.IndRDGC(DataGridFornitori, "ID")).Text) <> CInt(txtIdFornitore.Value) Then
                                    riga = nuovaDt.NewRow
                                    riga.Item("ID") = elemento.Cells(par.IndRDGC(DataGridFornitori, "ID")).Text
                                    riga.Item("CODICE") = elemento.Cells(par.IndRDGC(DataGridFornitori, "CODICE")).Text
                                    riga.Item("FORNITORE") = elemento.Cells(par.IndRDGC(DataGridFornitori, "FORNITORE")).Text
                                    riga.Item("INDIRIZZO") = elemento.Cells(par.IndRDGC(DataGridFornitori, "INDIRIZZO")).Text
                                    riga.Item("IBAN") = elemento.Cells(par.IndRDGC(DataGridFornitori, "IBAN")).Text
                                    riga.Item("CAPOFILA") = elemento.Cells(par.IndRDGC(DataGridFornitori, "CAPOFILA")).Text
                                    riga.Item("ID_TIPO_MODALITA_PAG") = elemento.Cells(par.IndRDGC(DataGridFornitori, "ID_TIPO_MODALITA_PAG")).Text
                                    riga.Item("ID_TIPO_PAGAMENTO") = elemento.Cells(par.IndRDGC(DataGridFornitori, "ID_TIPO_PAGAMENTO")).Text
                                    nuovaDt.Rows.Add(riga)
                                End If
                            Next
                            DataGridFornitori.DataSource = nuovaDt
                            DataGridFornitori.DataBind()

                        End If
                    End If
            End Select
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try
    End Sub


    Protected Sub DataGridFornitori_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGridFornitori.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('Tab_Fornitori1_txtSelFornitori').value='Hai selezionato il fornitore: " & dataItem("FORNITORE").Text.ToString.Replace("'", "\'") & "';document.getElementById('Tab_Fornitori1_txtIdFornitore').value='" & dataItem("ID").Text & "';")
            e.Item.Attributes.Add("onDblClick", "document.getElementById('Tab_Fornitori1_btnApriFornitoreAppalto').click();")
            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Then
                DataGridFornitori.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
                DataGridFornitori.MasterTableView.Columns.FindByUniqueName("modificaFornitore").Visible = False
                DataGridFornitori.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
            End If
        End If

    End Sub

    Protected Sub DataGridFornitori_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGridFornitori.NeedDataSource
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            Dim listafornitori As String = ""

            par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.APPALTI_FORNITORI WHERE ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti
            Dim ris As Integer = 0
            ris = par.IfNull(par.cmd.ExecuteScalar, 0)
            Dim query As String = ""
            If ris = 1 Then
                query = "SELECT FORNITORI.ID,COD_FORNITORE AS CODICE,RAGIONE_SOCIALE AS FORNITORE,100 AS PERCENTUALE " _
                    & " ,INDIRIZZO || ' '||CIVICO||' - '||CAP||' '||COMUNE AS INDIRIZZO " _
                    & ",(SELECT IBAN FROM SISCOM_MI.FORNITORI_IBAN WHERE FL_ATTIVO=1 AND FORNITORI_IBAN.ID_FORNITORE=FORNITORI.ID) AS IBAN,'C' AS CAPOFILA,ID_TIPO_MODALITA_PAG,ID_TIPO_PAGAMENTO " _
                    & " FROM SISCOM_MI.FORNITORI,SISCOM_MI.FORNITORI_INDIRIZZI WHERE FORNITORI.ID=FORNITORI_INDIRIZZI.ID_FORNITORE AND FORNITORI.ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI_FORNITORI WHERE ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & ")"
            Else
                query = "SELECT FORNITORI.ID,FORNITORI.COD_FORNITORE AS CODICE,FORNITORI.RAGIONE_SOCIALE AS FORNITORE,APPALTI_FORNITORI.PERCENTUALE AS PERCENTUALE " _
                    & " ,INDIRIZZO || ' '||CIVICO||' - '||CAP||' '||COMUNE AS INDIRIZZO " _
                    & ",(SELECT IBAN FROM SISCOM_MI.FORNITORI_IBAN WHERE FL_ATTIVO=1 AND FORNITORI_IBAN.ID_FORNITORE=FORNITORI.ID) AS IBAN,DECODE(CAPOFILA,1,'C','') AS CAPOFILA,ID_TIPO_MODALITA_PAG,ID_TIPO_PAGAMENTO " _
                    & " FROM SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI_FORNITORI,SISCOM_MI.FORNITORI_INDIRIZZI WHERE FORNITORI.ID=FORNITORI_INDIRIZZI.ID_FORNITORE AND FORNITORI.ID=APPALTI_FORNITORI.ID_FORNITORE AND ID_appalto=" & CType(Me.Page, Object).vIdAppalti
            End If
            par.cmd.CommandText = query
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)
            'da.Dispose()
            'DataGridFornitori.DataSource = dt
            'DataGridFornitori.Rebind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ApriFornitore()
        Try
            If IsNumeric(txtIdFornitore.Value) AndAlso CInt(txtIdFornitore.Value) > 0 Then
                If IsNumeric(CType(Me.Page, Object).vIdAppalti) AndAlso CInt(CType(Me.Page, Object).vIdAppalti) > 0 Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPALTI_FORNITORI WHERE ID_APPALTO=" & CType(Me.Page, Object).vIdAppalti & " AND ID_FORNITORE=" & txtIdFornitore.Value
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim capofila As Integer = 0
                    If lettore.Read Then
                        cmbAggFornitori.SelectedValue = par.IfNull(lettore("ID_FORNITORE"), "-1")
                        capofila = par.IfNull(lettore("CAPOFILA"), "0")
                        If capofila = 1 Then
                            chkCapofila.Checked = True
                        Else
                            chkCapofila.Checked = False
                        End If
                    End If
                    lettore.Close()
                Else
                    For Each elemento As GridDataItem In DataGridFornitori.Items
                        If elemento.Cells(par.IndRDGC(DataGridFornitori, "ID")).Text = txtIdFornitore.Value Then
                            cmbAggFornitori.SelectedValue = elemento.Cells(par.IndRDGC(DataGridFornitori, "ID")).Text
                            Dim capofila As String = "C"
                            capofila = elemento.Cells(par.IndRDGC(DataGridFornitori, "CAPOFILA")).Text.Replace("&nbsp;", "")
                            If capofila = "C" Then
                                capofila = "1"
                            Else
                                capofila = "0"
                            End If
                            If capofila = "1" Then
                                chkCapofila.Checked = True
                            Else
                                chkCapofila.Checked = False
                            End If
                        End If
                    Next
                End If
                Dim script As String = "function f(){$find(""" + RadWindowFornitori.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabServizi"
        End Try
    End Sub

    Protected Sub btnApriFornitoreAppalto_Click(sender As Object, e As System.EventArgs) Handles btnApriFornitoreAppalto.Click
        ApriFornitore()
        txtTipoF.Value = "1"
        If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Then
            FrmSolaLettura()
        End If
    End Sub

    Protected Sub DataGridFornitori_PreRender(sender As Object, e As System.EventArgs) Handles DataGridFornitori.PreRender
        If Not (CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO") Then
            If CType(Me.Page.FindControl("DropDownListTipo"), Telerik.Web.UI.RadComboBox).SelectedValue = "1" Then
                DataGridFornitori.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
                DataGridFornitori.Rebind()
            Else
                DataGridFornitori.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top
                DataGridFornitori.Rebind()
            End If
        End If
    End Sub
End Class
