
Partial Class Condomini_TabConvocazione
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.btnOrdGiorno.Visible = False
            If Not IsPostBack Then
                RiempiCampi()
                TxtDataArrivoAler.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                'txtDataArrivoSEGE.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataConv.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataArrivoVerbAler.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                'txtDataArrVerbSege.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataArrivo.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataArrivoVerb.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                txtProtAler.Attributes.Add("onblur", "javascript:valid(this,'quotes');")
                txtProtAler.Attributes.Add("onkeyup", "javascript:valid(this,'quotes');")

                txthh.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
                txtMM.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

                txtPercMillComp.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
                txtPercMilProp.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
                txtpercSup.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

                cerca()
                If DirectCast(Me.Page.FindControl("ImgVisibility"), HiddenField).Value <> 1 Then
                    Me.btnDelete.Visible = False
                    Me.btnVisualizza.Visible = False
                    Me.btnSalvaCambioAmm.Visible = False
                    Me.btnOrdGiorno.Visible = False
                End If
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabConvocazione"

        End Try

    End Sub
    Public Sub RiempiCampi()
        Try


            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Me.CmbTipoConvoc.Items.Add(New ListItem("ORDINARIA", "O"))
            Me.CmbTipoConvoc.Items.Add(New ListItem("STRAORDINARIA", "S"))
            Me.CmbTipoConvoc.Items.Add(New ListItem("COSTITUZIONE", "C"))

            par.cmd.CommandText = "SELECT COND_DELEGATI.ID, (COND_DELEGATI.NOME ||' '|| COND_DELEGATI.COGNOME) AS DELEGATO FROM SISCOM_MI.COND_DELEGATI"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.listDelegati.Items.Add(New ListItem(par.IfNull(myReader1("DELEGATO"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()

            'Me.txtDenomCond.Text = DirectCast(Me.Page.FindControl("txtDenCondominio"), TextBox).Text

            Me.cmbTipoInvio.Items.Add(New ListItem("", ""))
            Me.cmbTipoInvio.Items.Add(New ListItem("POSTA", "POSTA"))
            Me.cmbTipoInvio.Items.Add(New ListItem("FAX", "FAX"))
            Me.cmbTipoInvio.Items.Add(New ListItem("MAIL", "MAIL"))
            Me.cmbTipoInvio.Items.Add(New ListItem("A MANO", "A MANO"))

            Me.cmbTipoInvioVerb.Items.Add(New ListItem("", ""))
            Me.cmbTipoInvioVerb.Items.Add(New ListItem("POSTA", "POSTA"))
            Me.cmbTipoInvioVerb.Items.Add(New ListItem("FAX", "FAX"))
            Me.cmbTipoInvioVerb.Items.Add(New ListItem("MAIL", "MAIL"))
            Me.cmbTipoInvioVerb.Items.Add(New ListItem("A MANO", "A MANO"))
            Me.txtDenomConv.Text = DirectCast(Me.Page.FindControl("txtDenCondominio"), TextBox).Text

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabConvocazione"
        End Try

    End Sub
    Public Sub cerca()
        Try
            If Not String.IsNullOrEmpty(CType(Me.Page, Object).vIdCondominio.ToString) Then
                QUERY = "SELECT ID,PROTOCOLLO_ALER,TO_CHAR(TO_DATE(DATA_CONVOCAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_CONVOCAZIONE, (CASE(COND_CONVOCAZIONI.TIPO) WHEN 'O' THEN 'ORDINARIA' WHEN 'S' THEN 'STRAORDINARIA' END) AS TIPO,ALTRE_PRESENZE, DELEGATO, TO_CHAR(TO_DATE(DATA_ARRIVO_ALER,'yyyymmdd'),'dd/mm/yyyy') AS DATA_ARRIVO_ALER,TO_CHAR(TO_DATE(DATA_ARRIVO_VERBALE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_ARRIVO_VERBALE, NUM_VERBALE FROM SISCOM_MI.COND_CONVOCAZIONI WHERE  ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & " ORDER BY DATA_CONVOCAZIONE DESC"
                BindGrid()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub
    Private Sub BindGrid()
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = QUERY
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "CONVOCAZIONI")

            DataGridConvocazioni.DataSource = ds
            DataGridConvocazioni.DataBind()
            If ds.Tables(0).Rows.Count > 0 Then
                Me.btnVisualizza.Visible = True
                btnDelete.Visible = True
            Else
                Me.btnVisualizza.Visible = False
                Me.btnDelete.Visible = False
            End If

            da.Dispose()
            ds.Dispose()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabConvocazione"
        End Try
    End Sub

    Public Property vId() As String
        Get
            If Not (ViewState("par_Vid") Is Nothing) Then
                Return CStr(ViewState("par_Vid"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_Vid") = value
        End Set
    End Property

    Private Property QUERY() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set

    End Property

    Protected Sub btnSalvaCambioAmm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaCambioAmm.Click
        DirectCast(Me.Page.FindControl("AggPercent"), HiddenField).Value = 0

        If vModifica.Value = 0 Then
            Salva()
        Else
            Update()
        End If
    End Sub
    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        If valorepass = "-1" Then
            RitornaNullSeMenoUno = "NULL"
        Else
            RitornaNullSeMenoUno = valorepass
        End If
        Return RitornaNullSeMenoUno
    End Function

    Private Sub PulisciControls()
        Me.txtDataArrivo.Text = ""
        Me.txtProtAler.Text = ""
        Me.TxtDataArrivoAler.Text = ""
        Me.txtDataConv.Text = ""
        Me.txthh.Text = ""
        Me.txtMM.Text = ""
        Me.txtDelegato.Text = ""
        Me.txtAltrePresenze.Text = ""
        Me.txtVerbNProtAler.Text = ""
        Me.txtNote.Text = ""
        Me.txtDataArrivoVerb.Text = ""
        Me.cmbTipoInvioVerb.ClearSelection()
        Me.txtDataArrivoVerbAler.Text = ""
    End Sub
    Private Sub Update()
        Try
            If txtidConv.Value <> "0" Then

                'If Not String.IsNullOrEmpty(Me.txtProtAler.Text) Then

                If String.IsNullOrEmpty(Me.txthh.Text) Then
                    If Not String.IsNullOrEmpty(Me.txtMM.Text) Then
                        Response.Write("<script>alert('Definire correttamente l'ora della convocazione!');</script>")
                        Exit Sub
                        DirectCast(Me.Page.FindControl("TextBox2"), HiddenField).Value = 2
                    End If
                End If

                If String.IsNullOrEmpty(Me.txtDataConv.Text) And Not String.IsNullOrEmpty(Me.txthh.Text) Then
                    Response.Write("<script>alert('Per definire l\'ora della convocazione è necessaria anche la data!');</script>")
                    DirectCast(Me.Page.FindControl("TextBox2"), HiddenField).Value = 2
                    Exit Sub
                End If
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_CONVOCAZIONI SET PROTOCOLLO_ALER = '" & par.PulisciStrSql(Me.txtProtAler.Text.ToUpper) & "',ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & ", DATA_ARRIVO_ALER = '" & par.AggiustaData(TxtDataArrivoAler.Text) & "', TIPO = '" & RitornaNullSeMenoUno(Me.CmbTipoConvoc.SelectedValue.ToUpper) & "', DATA_CONVOCAZIONE = '" & par.AggiustaData(Me.txtDataConv.Text) & "', " _
                & " PERC_MILLEISMI_PRO =" & par.IfEmpty(par.VirgoleInPunti(Me.txtPercMilProp.Text), "Null") & " , PERC_SUPEFICI = " & par.IfEmpty(par.VirgoleInPunti(Me.txtpercSup.Text), "Null") & ", PERC_MILLESIMI_CONPRO = " & par.IfEmpty(par.VirgoleInPunti(Me.txtPercMillComp.Text), "Null") & ", DELEGATO = '" & par.PulisciStrSql(Me.txtDelegato.Text) & "', ALTRE_PRESENZE = '" & par.PulisciStrSql(Me.txtAltrePresenze.Text.ToUpper) & "', ID_AMMINISTRATORE = " & DirectCast(Me.Page.FindControl("cmbAmministratori"), DropDownList).SelectedValue.ToString & "," _
                & " NUM_VERBALE = '" & par.PulisciStrSql(Me.txtVerbNProtAler.Text.ToUpper) & "', DATA_ARRIVO_VERBALE_ALER = '" & par.AggiustaData(Me.txtDataArrivoVerbAler.Text) & "', ORA_CONVOCAZIONE = '" & par.IfEmpty(Me.txthh.Text, "") & par.IfEmpty(Me.txtMM.Text, "") & "'," _
                & " DATA_ARRIVO_VERBALE = '" & par.AggiustaData(Me.txtDataArrivoVerb.Text) & "', TIPO_INVIO_VERBALE = '" & Me.cmbTipoInvioVerb.SelectedValue.ToString & "', DATA_ARRIVO = '" & par.AggiustaData(Me.txtDataArrivo.Text) & "', TIPO_INVIO = '" & Me.cmbTipoInvio.SelectedValue.ToString & "', NOTE = '" & par.PulisciStrSql(Me.txtNote.Text) & "',PERC_MILLESIMI_PRES_ASS=" & par.IfEmpty(par.VirgoleInPunti(Me.txtPercMillPresAss.Text), "Null") & " WHERE ID = '" & txtidConv.Value & "'"
                par.cmd.ExecuteNonQuery()

                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F36','')"
                par.cmd.ExecuteNonQuery()

                DirectCast(Me.Page.FindControl("TextBox2"), HiddenField).Value = 1

                PulisciControls()
                cerca()
                Me.txtidConv.Value = 0

                Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                Response.Write("<script>alert('Per completare l\'operazione fare click sul pulsante salva della finestra principale!');</script>")


            End If
            'End If

            vModifica.Value = 0
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabConvocazione"
        End Try

    End Sub
    Private Sub Salva()
        Try

            'If Not String.IsNullOrEmpty(Me.txtProtAler.Text) Then
            Dim IdConvocazione As String = 0
            If String.IsNullOrEmpty(Me.txthh.Text) Then
                If Not String.IsNullOrEmpty(Me.txtMM.Text) Then
                    Response.Write("<script>alert('Definire correttamente l'ora della convocazione!');</script>")
                    Exit Sub
                    DirectCast(Me.Page.FindControl("TextBox2"), HiddenField).Value = 2
                End If
            End If

            If String.IsNullOrEmpty(Me.txtDataConv.Text) And Not String.IsNullOrEmpty(Me.txthh.Text) Then
                Response.Write("<script>alert('Per definire l\'ora della convocazione è necessaria anche la data!');</script>")
                DirectCast(Me.Page.FindControl("TextBox2"), HiddenField).Value = 2
                Exit Sub
            End If

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_CONVOCAZIONI WHERE PROTOCOLLO_ALER = '" & par.PulisciStrSql(Me.txtProtAler.Text.ToUpper) & "'"

            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_COND_CONVOCAZIONI.NEXTVAL FROM DUAL"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader1.Read Then
                IdConvocazione = myReader1(0)
                'Response.Write("<script>alert('Esiste già una convocazione con lo stesso protocollo!');</script>")
                'DirectCast(Me.Page.FindControl("TextBox2"), HiddenField).Value = 2
                'Exit Sub
            End If
            myReader1.Close()

            Dim MILLESIMI As Double
            If CType(Me.Page.FindControl("cmbTipoCond"), DropDownList).SelectedValue = "C" Then
                MILLESIMI = par.IfEmpty(CType(Me.Page.FindControl("TabMillesimalil1").FindControl("txtMilPropComune"), TextBox).Text, 0)
            ElseIf CType(Me.Page.FindControl("cmbTipoCond"), DropDownList).SelectedValue = "S" Then
                MILLESIMI = par.IfEmpty(CType(Me.Page.FindControl("TabMillesimalil1").FindControl("txtMillCompComune"), TextBox).Text, 0)
            ElseIf CType(Me.Page.FindControl("cmbTipoCond"), DropDownList).SelectedValue = "T" Then
                MILLESIMI = par.IfEmpty(CType(Me.Page.FindControl("TabMillesimalil1").FindControl("TxtMillSupComune"), TextBox).Text, 0)
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_CONVOCAZIONI (ID,PROTOCOLLO_ALER,ID_CONDOMINIO, DATA_ARRIVO_ALER," _
            & " TIPO, DATA_CONVOCAZIONE, PERC_MILLEISMI_PRO, PERC_SUPEFICI, PERC_MILLESIMI_CONPRO, DELEGATO, ALTRE_PRESENZE," _
            & " ID_AMMINISTRATORE, NUM_VERBALE, DATA_ARRIVO_VERBALE_ALER, ORA_CONVOCAZIONE,DATA_ARRIVO,TIPO_INVIO, DATA_ARRIVO_VERBALE," _
            & " TIPO_INVIO_VERBALE, NOTE, MILLESIMI,PERC_MILLESIMI_PRES_ASS) " _
            & " VALUES(" & IdConvocazione & ",'" & par.PulisciStrSql(Me.txtProtAler.Text.ToUpper) & "'," & CType(Me.Page, Object).vIdCondominio & ", '" & par.AggiustaData(TxtDataArrivoAler.Text) & "', '" & RitornaNullSeMenoUno(Me.CmbTipoConvoc.SelectedValue.ToUpper) & "', '" & par.AggiustaData(Me.txtDataConv.Text) & "', " & par.IfEmpty(par.VirgoleInPunti(Me.txtPercMilProp.Text), "Null") & ", " _
            & par.IfEmpty(par.VirgoleInPunti(Me.txtpercSup.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtPercMillComp.Text), "Null") & ", '" & par.PulisciStrSql(Me.txtDelegato.Text) & "', '" & par.PulisciStrSql(Me.txtAltrePresenze.Text.ToUpper) & "', " & DirectCast(Me.Page.FindControl("cmbAmministratori"), DropDownList).SelectedValue.ToString & ", '" _
            & par.PulisciStrSql(Me.txtVerbNProtAler.Text.ToUpper) & "', '" & par.AggiustaData(Me.txtDataArrivoVerbAler.Text) & "', '" & par.IfEmpty(Me.txthh.Text, "") & par.IfEmpty(Me.txtMM.Text, "") & "', '" & par.AggiustaData(Me.txtDataArrivo.Text) & "', '" & Me.cmbTipoInvio.SelectedValue & "','" & par.AggiustaData(Me.txtDataArrivoVerb.Text) & "', '" & Me.cmbTipoInvioVerb.SelectedValue & "', '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.VirgoleInPunti(MILLESIMI) & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtPercMillPresAss.Text), "Null") & ")"
            par.cmd.ExecuteNonQuery()

            '****************MYEVENT*****************
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F35','')"
            par.cmd.ExecuteNonQuery()

            DirectCast(Me.Page.FindControl("TextBox2"), HiddenField).Value = 2

            Me.btnOrdGiorno.Visible = True

            'PulisciControls()
            cerca()

            Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
            Response.Write("<script>alert('Per completare l\'operazione fare click sul pulsante salva della finestra principale!');</script>")
            vModifica.Value = 1
            Me.txtidConv.Value = IdConvocazione
            'Else
            'Response.Write("<script>alert('Impossibile salvare senza protocollo!');</script>")
            'PulisciControls()
            'End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabConvocazione"
        End Try
    End Sub
    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        Try
            If Me.txtidConv.Value <> "0" Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_CONVOCAZIONI WHERE ID = '" & Me.txtidConv.Value & "'"
                Me.btnOrdGiorno.Visible = True
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                DirectCast(Me.Page.FindControl("AggPercent"), HiddenField).Value = 1

                If myReader1.Read Then
                    Me.txtProtAler.Text = myReader1("PROTOCOLLO_ALER").ToString
                    Me.TxtDataArrivoAler.Text = par.FormattaData(myReader1("DATA_ARRIVO_ALER").ToString)
                    'Me.txtDataArrivoSEGE.Text = par.FormattaData(myReader1("DATA_ARRIVO_SEGE").ToString)
                    Me.CmbTipoConvoc.SelectedValue = myReader1("TIPO").ToString
                    Me.txtDataConv.Text = par.FormattaData(myReader1("DATA_CONVOCAZIONE").ToString)
                    Me.txtPercMilProp.Text = Format(CDbl(par.IfNull(myReader1("PERC_MILLEISMI_PRO"), 0)), "0.00")
                    Me.txtpercSup.Text = Format(CDbl(par.IfNull(myReader1("PERC_SUPEFICI"), 0)), "0.00")
                    Me.txtPercMillComp.Text = Format(CDbl(par.IfNull(myReader1("PERC_MILLESIMI_CONPRO"), 0)), "0.00")
                    Me.txtDelegato.Text = par.IfEmpty(myReader1("DELEGATO").ToString, "")
                    Me.txtAltrePresenze.Text = myReader1("ALTRE_PRESENZE").ToString
                    Me.txtVerbNProtAler.Text = myReader1("NUM_VERBALE").ToString
                    Me.txtDataArrivoVerbAler.Text = par.FormattaData(par.IfNull(myReader1("DATA_ARRIVO_VERBALE_ALER"), ""))
                    'Me.txtDataArrVerbSege.Text = par.FormattaData(par.IfNull(myReader1("DATA_ARRIVO_VERBALE_SEGE"), ""))
                    If myReader1("ORA_CONVOCAZIONE").ToString.Length >= 4 Then
                        Me.txthh.Text = myReader1("ORA_CONVOCAZIONE").ToString.Substring(0, 2)
                        Me.txtMM.Text = myReader1("ORA_CONVOCAZIONE").ToString.Substring(2)
                    End If
                    vModifica.Value = 1
                    Me.txtDataArrivo.Text = par.FormattaData(par.IfNull(myReader1("DATA_ARRIVO"), ""))
                    Me.cmbTipoInvio.SelectedValue = par.IfNull(myReader1("TIPO_INVIO"), "")
                    Me.txtDataArrivoVerb.Text = par.FormattaData(par.IfNull(myReader1("DATA_ARRIVO_VERBALE"), ""))
                    Me.cmbTipoInvioVerb.SelectedValue = par.IfNull(myReader1("TIPO_INVIO_VERBALE"), "")
                    Me.txtNote.Text = myReader1("NOTE").ToString
                    Me.txtDenomConv.Text = DirectCast(Me.Page.FindControl("txtDenCondominio"), TextBox).Text
                End If
                myReader1.Close()
                DirectCast(Me.Page.FindControl("TextBox2"), HiddenField).Value = 2
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabConvocazione"
        End Try
    End Sub

    Protected Sub DataGridConvocazioni_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridConvocazioni.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TabConvocazione1_txtmia').value='Hai selezionato la Convocazione del " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('TabConvocazione1_txtidConv').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TabConvocazione1_txtmia').value='Hai selezionato la Convocazione del " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('TabConvocazione1_txtidConv').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    'Protected Sub imgBtnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAnnulla.Click
    '    DirectCast(Me.Page.FindControl("TextBox2"), HiddenField).Value = 1

    '    Me.txtProtAler.Text = ""
    '    Me.TxtDataArrivoAler.Text = ""
    '    Me.txtDataArrivoSEGE.Text = ""
    '    Me.txtDataConv.Text = ""
    '    Me.txtPercMilProp.Text = ""
    '    Me.txtpercSup.Text = ""
    '    Me.txtPercMillComp.Text = ""
    '    Me.txtDelegato.Text = ""
    '    Me.txtAltrePresenze.Text = ""
    '    Me.txtVerbNProtAler.Text = ""
    '    Me.txtDataArrivoVerbAler.Text = ""
    '    Me.txtDataArrVerbSege.Text = ""
    '    Me.txthh.Text = ""
    '    Me.txtMM.Text = ""
    '    vModifica = ""
    '    Me.txtidConv.Value = 0
    'End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
        Try
            If Me.txtidConv.Value <> "0" Then
                If txtConfElimina.Value = 1 Then
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_CONVOCAZIONI WHERE ID = '" & Me.txtidConv.Value & "'"
                    par.cmd.ExecuteNonQuery()
                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F36','CANCELLATA CONVOCAZIONE')"
                    par.cmd.ExecuteNonQuery()

                    Me.txtidConv.Value = 0
                    txtConfElimina.Value = 0
                    cerca()

                    Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                    Response.Write("<script>alert('Per completare l\'operazione fare click sul pulsante salva della finestra principale!');</script>")
                Else
                    Me.txtidConv.Value = 0
                    txtConfElimina.Value = 0
                End If
            Else
                Response.Write("<script>alert('Selezionare un elemento da eliminare!');</script>")
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabConvocazione"
        End Try
    End Sub

End Class
