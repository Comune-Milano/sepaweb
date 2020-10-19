
Partial Class Condomini_TabDatiTecnici
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'CercaScale()
                'CercaFabbricati()
                RiempiCampi()
                ApriRicerca()
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabDatiTecnici"

        End Try

    End Sub

    Public Sub ApriRicerca()
        Try

            If CType(Me.Page, Object).vIdCondominio() <> "" Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT CONDOMINI.ID_BUILDING_MANAGER,ID_FILIALE,COD_TIPOLOGIA_IMP_RISCALD,ID_CENTRALE_TERMICA FROM SISCOM_MI.CONDOMINI WHERE CONDOMINI.ID =  " & CType(Me.Page, Object).vIdCondominio()
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.cmbBuildingManager.SelectedValue = par.IfEmpty(myReader1("ID_BUILDING_MANAGER").ToString, "-1")
                    Me.cmbFiliale.SelectedValue = par.IfEmpty(myReader1("ID_FILIALE").ToString, "-1")
                    Me.cmbTipoRisc.SelectedValue = par.IfEmpty(myReader1("COD_TIPOLOGIA_IMP_RISCALD").ToString, "")
                    Me.cmbDenRiscald.SelectedValue = par.IfEmpty(myReader1("ID_CENTRALE_TERMICA").ToString, "-1")
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT ID_SUPERCONDOMINIO FROM SISCOM_MI.COND_SUPER WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio()
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    Me.ListSuperCond.Items.FindByValue(myReader1("ID_SUPERCONDOMINIO")).Selected = True
                End While
                myReader1.Close()
                AddSuperCond()
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabDatiTecnici"
        End Try

    End Sub

    'Private Sub BindGridScale()
    '    Try
    '        'Riaggancio della connessione aperta nel form principale


    '        '*******************RICHIAMO LA CONNESSIONE*********************
    '        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '        par.SettaCommand(par)
    '        '*******************RICHIAMO LA TRANSAZIONE*********************
    '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '        ‘‘par.cmd.Transaction = par.myTrans

    '        par.cmd.CommandText = QUERY

    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

    '        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

    '        Dim ds As New Data.DataSet()
    '        da.Fill(ds, "AMMINISTRATORI")

    '        DataGridSc.DataSource = ds
    '        DataGridSc.DataBind()

    '        Me.Lblscale.Text = "SCALE - TOT = " & ds.Tables(0).Rows.Count
    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
    '        CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabDatiTecnici"
    '    End Try

    'End Sub
    'Public Sub CercaFabbricati()

    '    Try
    '        '***CONTROLLARE***
    '        'Dim idComplesso As String = DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue

    '        'If idComplesso <> "-1" AndAlso DirectCast(Me.Page.FindControl("DrlEdificio"), DropDownList).SelectedValue = "-1" Then
    '        '    QUERY = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id_complesso = " & idComplesso & " order by denominazione asc"
    '        '    BindGridFabbricati()
    '        'ElseIf DirectCast(Me.Page.FindControl("DrlEdificio"), DropDownList).SelectedValue <> "-1" Then
    '        '    QUERY = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id = " & DirectCast(Me.Page.FindControl("DrlEdificio"), DropDownList).SelectedValue & " order by denominazione asc"
    '        '    BindGridFabbricati()
    '        'End If


    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
    '        CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabDatiTecnici"
    '    End Try
    'End Sub
    'Private Sub BindGridFabbricati()

    '    Try

    '        '*******************RICHIAMO LA CONNESSIONE*********************
    '        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '        par.SettaCommand(par)
    '        '*******************RICHIAMO LA TRANSAZIONE*********************
    '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '        ‘‘par.cmd.Transaction = par.myTrans

    '        par.cmd.CommandText = QUERY
    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

    '        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

    '        Dim ds As New Data.DataSet()
    '        da.Fill(ds, "AMMINISTRATORI")

    '        DataGridFabbricati.DataSource = ds
    '        DataGridFabbricati.DataBind()

    '        Me.lblFabbricati.Text = "FABBRICATI - TOT = " & ds.Tables(0).Rows.Count
    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
    '        CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabDatiTecnici"
    '    End Try

    'End Sub
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
    Public Sub RiempiCampi()
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            'par.cmd.CommandText = "select * from siscom_mi.cond_building_manager"
            'Me.cmbBuildingManager.Items.Add(New ListItem(" ", -1))
            'While myReader1.Read
            '    cmbBuildingManager.Items.Add(New ListItem(par.IfNull(myReader1("cognome"), " ") & " " & par.IfNull(myReader1("nome"), " "), par.IfNull(myReader1("id"), -1)))
            'End While
            'myReader1.Close()

            par.caricaComboBox("select ID, COGNOME ||' ' ||NOME as nominativo from siscom_mi.cond_building_manager", Me.cmbBuildingManager, "id", "nominativo", True)

            'par.cmd.CommandText = "select id, nome from siscom_mi.tab_filiali"
            'Me.cmbFiliale.Items.Add(New ListItem(" ", -1))
            'myReader1 = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    cmbFiliale.Items.Add(New ListItem(par.IfNull(myReader1("nome"), " "), par.IfNull(myReader1("id"), -1)))
            'End While
            'myReader1.Close()

            par.caricaComboBox("select id, nome from siscom_mi.tab_filiali", Me.cmbFiliale, "id", "nome", True)

            'par.cmd.CommandText = "select cod,descrizione from siscom_mi.tipologia_imp_riscaldamento"
            'Me.cmbTipoRisc.Items.Add(New ListItem(" ", ""))
            'myReader1 = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    cmbTipoRisc.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            'End While
            'myReader1.Close()

            par.caricaComboBox("select cod,descrizione from siscom_mi.tipologia_imp_riscaldamento", Me.cmbTipoRisc, "cod", "descrizione", True, "")

            'par.cmd.CommandText = "  SELECT ID, (TO_CHAR(CONDOMINI.ID,'00000')||'--'||DENOMINAZIONE) AS DENOMINAZIONE FROM SISCOM_MI.CONDOMINI WHERE TIPOLOGIA = 'T'"
            'Me.cmbDenRiscald.Items.Add(New ListItem(" ", "-1"))
            'myReader1 = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    cmbDenRiscald.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()

            par.caricaComboBox("SELECT ID, (TO_CHAR(CONDOMINI.ID,'00000')||'--'||DENOMINAZIONE) AS DENOMINAZIONE FROM SISCOM_MI.CONDOMINI WHERE TIPOLOGIA = 'T'", Me.cmbDenRiscald, "id", "denominazione", True)

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            par.cmd.CommandText = "  SELECT ID, (TO_CHAR(CONDOMINI.ID,'00000')||'--'||DENOMINAZIONE) AS DENOMINAZIONE FROM SISCOM_MI.CONDOMINI WHERE TIPOLOGIA = 'S'"
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                ListSuperCond.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()



        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabDatiTecnici"
        End Try

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        AddSuperCond()
    End Sub
    Private Function SelezionatiSuperCond() As Boolean
        Try
            Dim I As Integer
            SelezionatiSuperCond = False
            For I = 0 To Me.ListSuperCond.Items.Count() - 1
                If Me.ListSuperCond.Items(I).Selected = True Then
                    SelezionatiSuperCond = True
                    Exit For
                End If
            Next
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabDatiTecnici"
        End Try
    End Function
    Private Sub AddSuperCond()
        Try

            LstSuperCond.Items.Clear()
            If SelezionatiSuperCond() = True Then
                Dim I As Integer
                For I = 0 To Me.ListSuperCond.Items.Count() - 1
                    If Me.ListSuperCond.Items(I).Selected = True Then
                        LstSuperCond.Items.Add(New ListItem(Me.ListSuperCond.Items(I).Text, par.IfNull(Me.ListSuperCond.Items(I).Value, -1)))
                    End If
                Next
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabDatiTecnici"
        End Try

    End Sub

    Protected Sub LstSuperCond_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LstSuperCond.SelectedIndexChanged

    End Sub
End Class
