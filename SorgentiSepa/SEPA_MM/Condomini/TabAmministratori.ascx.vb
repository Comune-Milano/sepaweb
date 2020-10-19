
Partial Class Condomini_TabAmministratori
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                cerca()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabAmministratori"
        End Try

    End Sub
    Public Sub cerca()
        Try
            vId = CType(Me.Page, Object).vIdCondominio
            If vId <> "" Then
                QUERY = "SELECT ID, COGNOME, NOME, TITOLO,to_char(to_date(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') as DATA_INIZIO, to_char(to_date(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy') as DATA_FINE  FROM SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE WHERE COND_AMMINISTRATORI .ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE(+) AND ID_CONDOMINIO = " & vId & " AND DATA_FINE IS NOT NULL ORDER BY DATA_INIZIO DESC"
                BindGrid()
            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabAmministratori"
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            'Riaggancio della connessione aperta nel form principale

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT COND_AMMINISTRATORI.*,  COMUNI_NAZIONI.SIGLA, COMUNI_NAZIONI.NOME AS COMUNE  FROM SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE, COMUNI_NAZIONI  WHERE ID_CONDOMINIO = " & vId & " AND COND_AMMINISTRATORI.ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE  AND COMUNI_NAZIONI.COD = COD_COMUNE AND COND_AMMINISTRAZIONE.DATA_FINE  IS NULL"
            Dim dt As New Data.DataTable
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Me.txtCognome.Text = dt.Rows(0).Item("COGNOME").ToString
                Me.txtNome.Text = dt.Rows(0).Item("NOME").ToString
                Me.txtCF.Text = dt.Rows(0).Item("CF").ToString
                Me.txtpiva.Text = dt.Rows(0).Item("PARTITA_IVA").ToString
                Me.txtIndirizzo.Text = dt.Rows(0).Item("TIPO_INDIRIZZO") & " " & dt.Rows(0).Item("INDIRIZZO").ToString
                Me.txtCivico.Text = dt.Rows(0).Item("CIVICO").ToString
                Me.txtCap.Text = dt.Rows(0).Item("CAP").ToString
                Me.txtComune.Text = dt.Rows(0).Item("COMUNE").ToString
                Me.txtProvincia.Text = dt.Rows(0).Item("SIGLA").ToString
                Me.txtTelefono1.Text = dt.Rows(0).Item("TEL_1").ToString
                Me.txtTelefono2.Text = dt.Rows(0).Item("TEL_2").ToString
                Me.txtCellulare.Text = dt.Rows(0).Item("CELL").ToString
                Me.txtFax.Text = dt.Rows(0).Item("FAX").ToString
                Me.TxtTitolo.Text = dt.Rows(0).Item("TITOLO").ToString
                Me.txtEmail.Text = dt.Rows(0).Item("EMAIL").ToString

            End If

            par.cmd.CommandText = QUERY
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "AMMINISTRATORI")

            'Piccolo controllo: Se non vi sono amministratori precedenti la DataGrid
            'dello storico Amministratori, la riempio con un rigo vuoto per renderla 
            'comunque visibile. Altrimenti potrebbe portare confusione.
            If ds.Tables(0).Rows.Count = 0 Then
                Dim row As System.Data.DataRow
                row = ds.Tables(0).NewRow
                row.Item("COGNOME") = "- - - - - "
                row.Item("NOME") = "- - - - - "
                row.Item("TITOLO") = "- - - - - "
                row.Item("DATA_INIZIO") = "- - - - - "
                row.Item("DATA_FINE") = "- - - - - "
                ds.Tables(0).Rows.Add(row)
            End If

            DataGridAmministratori.DataSource = ds
            DataGridAmministratori.DataBind()


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabAmministratori"
        End Try
    End Sub
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

End Class
