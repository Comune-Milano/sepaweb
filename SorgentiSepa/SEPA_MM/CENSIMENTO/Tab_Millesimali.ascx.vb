
Partial Class CENSIMENTO_Tab_UtMillesimali
    Inherits UserControlSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global
    Public Property vId() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property
    Public Property vIdTipologia() As String
        Get
            If Not (ViewState("par_IdTipologia") Is Nothing) Then
                Return CStr(ViewState("par_IdTipologia"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IdTipologia") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            If Not IsPostBack Then

                vId = CType(Me.Page, Object).vId
                If Session("PED2_SOLOLETTURA") = "1" Then
                    FrmSolaLettura()
                End If
                If Session("SLE") = "1" Then
                    FrmSolaLettura()
                End If
                RiempiCampi()

            End If
            '**********SERVE PER RECUPERARE ID SUBITO DOPO NUOVO INSERIMENTO IMMOBILE*****************
            If vId = 0 Then
                vId = CType(Me.Page, Object).vId
            End If
            '**********************FINE MODIFICA PER ID NUOVO INSERMENTO******************************
            cerca()


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message

        End Try
    End Sub

    Private Sub RiempiCampi()
        Try
            Dim apertoOra As Boolean = False



            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If IsNothing(par.myTrans) And par.OracleConn.State = Data.ConnectionState.Closed Then
                'Apro la connsessione con il DB
                par.OracleConn.Open()
                par.SettaCommand(par)
                apertoOra = True
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_MILLESIMALE"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Me.DrLMillesimi.Items.Add(New ListItem(" ", -1))
            While myReader.Read
                DrLMillesimi.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("COD"), -1)))
            End While
            myReader.Close()


            If Me.Page.Title = "Inserimento Complessi" Then
                lblElenco.Text = "Elenco Edifici"
                par.cmd.CommandText = "select  edifici.id  ,('COD. '||edifici.cod_edificio ||' - - '||edifici.denominazione) as DESCRIZIONE from SISCOM_MI.edifici where edifici.id_complesso = " & vId & " order by edifici.cod_edificio asc"
                myReader = par.cmd.ExecuteReader
                While myReader.Read
                    ListLista.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
                End While

            Else
                lblElenco.Text = "Elenco Scale"
                par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID_EDIFICIO = " & vId
                myReader = par.cmd.ExecuteReader
                While myReader.Read
                    ListLista.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
                End While

            End If
            myReader.Close()
            If apertoOra = True Then
                par.OracleConn.Close()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = "Tab_Millesimali_SubRimepicampi" & ex.Message
        End Try

    End Sub
    Private Sub FrmSolaLettura()
        Try
            Me.btnDelete.Visible = False
            Me.btnModifica.Visible = False
            Me.BtnADD.Visible = False
            Me.imgAddConv.Visible = False
            'Dim CTRL As Control = Nothing
            'For Each CTRL In Me.Form1.Controls
            '    If TypeOf CTRL Is TextBox Then
            '        DirectCast(CTRL, TextBox).Enabled = False
            '    ElseIf TypeOf CTRL Is DropDownList Then
            '        DirectCast(CTRL, DropDownList).Enabled = False
            '    End If
            'Next
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub
    Private Sub cerca()
        Try

            Select Case Me.Page.Title

                Case "Inserimento Complessi"

                    sStringaSql = "select ROWNUM, ID, TABELLE_MILLESIMALI.DESCRIZIONE, TIPOLOGIA_MILLESIMALE.DESCRIZIONE as tipo, DEscrizione_tabella from SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.TIPOLOGIA_MILLESIMALE where SISCOM_MI.TABELLE_MILLESIMALI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_MILLESIMALE.COD and ID_COMPLESSO = " & vId & "ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()

                Case "Inserimento EDIFICI"
                    sStringaSql = "select ROWNUM, ID, TABELLE_MILLESIMALI.DESCRIZIONE, TIPOLOGIA_MILLESIMALE.DESCRIZIONE as tipo, DEscrizione_tabella from SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.TIPOLOGIA_MILLESIMALE where SISCOM_MI.TABELLE_MILLESIMALI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_MILLESIMALE.COD and ID_EDIFICIO = " & vId & "ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()

            End Select
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message

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
    Private Sub BindGrid()
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = QUERY
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "INTERV_ADEG_NORM")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Millesimali1_txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_Millesimali1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Millesimali1_txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_Millesimali1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Protected Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnADD.Click

        Select Case Me.Page.Title

            Case "Inserimento Complessi"

                If Me.HFtxtId.Value = 0 Then
                    SaveComp()
                Else
                    UpdateComp()
                End If

            Case "Inserimento EDIFICI"

                If Me.HFtxtId.Value = 0 Then
                    SaveEdif()
                Else
                    UpdateEdif()
                End If

        End Select
        cerca()
        Me.txtDescr.Text = ""
        Me.txtDescTabella.Text = ""
        Me.DrLMillesimi.SelectedValue = "-1"
        Me.TextBox2.Value = 0
        'Desleziona gli elementi della lista
        Dim a As Integer
        Dim i As Integer = 0
        a = ListLista.Items.Count.ToString
        While i < a
            Me.ListLista.Items(i).Selected = False
            i = i + 1
        End While
    End Sub
    Private Sub SaveComp()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If Me.txtDescTabella.Text <> "" AndAlso Me.txtDescr.Text <> "" Then
                Dim i As Integer
                Dim dt As New Data.DataTable
                par.cmd.CommandText = "select COD_TIPOLOGIA from SISCOM_MI.TABELLE_MILLESIMALI where id_complesso =" & vId
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                If Not IsNothing(da) Then
                    da.Fill(dt)
                End If
                For i = 0 To dt.Rows.Count - 1
                    If DrLMillesimi.SelectedValue = dt.Rows(i).Item("COD_TIPOLOGIA") Then
                        Response.Write("<script>alert('Millesimo già iserito!')</script>")
                        TextBox2.Value = 1
                        Exit Sub
                        Me.txtDescr.Text = ""
                        Me.txtDescTabella.Text = ""
                    End If
                Next
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.TABELLE_MILLESIMALI (ID, ID_COMPLESSO, COD_TIPOLOGIA, DESCRIZIONE_TABELLA, DESCRIZIONE) VALUES" _
                & "(SISCOM_MI.SEQ_TABELLE_MILLESIMALI.NEXTVAL, " & vId & ", '" & Me.DrLMillesimi.SelectedValue.ToString & "', '" & par.IfNull(par.PulisciStrSql(Me.txtDescTabella.Text), "") & "','" & par.PulisciStrSql(Me.txtDescr.Text) & "')"
                par.cmd.ExecuteNonQuery()
                DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
                par.cmd.CommandText = ""

            End If
            If Me.ListLista.Items.Count > 0 Then

                For Each o As Object In ListLista.Items
                    Dim item As System.Web.UI.WebControls.ListItem
                    item = CType(o, System.Web.UI.WebControls.ListItem)
                    If item.Selected Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.TABELLE_MILLESIMALI_EDIFICI (ID_TABELLA,ID_EDIFICIO ) VALUES (SISCOM_MI.SEQ_TABELLE_MILLESIMALI.CURRVAL," & item.Value & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                Next
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = "Tab_Millesimali_SubSalva" & ex.Message
        End Try

    End Sub
    Private Sub UpdateComp()
        Try
            Dim i As Integer
            Dim dt As New Data.DataTable

            If Me.DrLMillesimi.SelectedValue <> vIdTipologia Then

                Dim stringa As String = "select COD_TIPOLOGIA from SISCOM_MI.TABELLE_MILLESIMALI where id_complesso =" & vId
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                If Not IsNothing(da) Then
                    da.Fill(dt)
                End If
                For i = 0 To dt.Rows.Count - 1
                    If DrLMillesimi.SelectedValue = dt.Rows(i).Item("COD_TIPOLOGIA") Then
                        Response.Write("<script>alert('Millesimo già iserito!')</script>")
                        Exit Sub
                        Me.txtDescr.Text = ""
                        Me.txtDescTabella.Text = ""
                    End If
                Next
            End If

            par.cmd.CommandText = "UPDATE SISCOM_MI.TABELLE_MILLESIMALI SET COD_TIPOLOGIA= '" & par.PulisciStrSql(par.IfNull(Me.DrLMillesimi.SelectedValue.ToString, "")) & "', DESCRIZIONE_TABELLA ='" & par.IfNull(par.PulisciStrSql(Me.txtDescTabella.Text), "") & "',DESCRIZIONE= '" & par.PulisciStrSql(Me.txtDescr.Text) & "' WHERE ID = " & Me.HFtxtId.Value
            par.cmd.ExecuteNonQuery()
            DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.TABELLE_MILLESIMALI_EDIFICI WHERE ID_TABELLA =" & Me.HFtxtId.Value
            par.cmd.ExecuteNonQuery()
            '++++++++NUOVA CHECk+++++++++++++++
            If Me.ListLista.Items.Count > 0 Then

                For Each o As Object In ListLista.Items
                    Dim item As System.Web.UI.WebControls.ListItem
                    item = CType(o, System.Web.UI.WebControls.ListItem)
                    If item.Selected Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.TABELLE_MILLESIMALI_EDIFICI (ID_TABELLA,ID_EDIFICIO ) VALUES (" & Me.HFtxtId.Value & "," & item.Value & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                Next
            End If
            '+++++++++fine nuova check



        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = "Tab_Millesimali_SubUpdate" & ex.Message
        End Try

    End Sub
    Private Sub SaveEdif()
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If Me.txtDescTabella.Text <> "" AndAlso Me.txtDescr.Text <> "" Then
                Dim i As Integer
                Dim dt As New Data.DataTable
                par.cmd.CommandText = "select COD_TIPOLOGIA from SISCOM_MI.TABELLE_MILLESIMALI where ID_EDIFICIO =" & vId
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                If Not IsNothing(da) Then
                    da.Fill(dt)
                End If
                For i = 0 To dt.Rows.Count - 1
                    If DrLMillesimi.SelectedValue = dt.Rows(i).Item("COD_TIPOLOGIA") Then
                        Response.Write("<script>alert('Millesimo già iserito!')</script>")
                        TextBox2.Value = 1
                        Exit Sub
                        Me.txtDescr.Text = ""
                        Me.txtDescTabella.Text = ""
                    End If
                Next
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.TABELLE_MILLESIMALI (ID, ID_EDIFICIO, COD_TIPOLOGIA, DESCRIZIONE_TABELLA, DESCRIZIONE) VALUES" _
                & "(SISCOM_MI.SEQ_TABELLE_MILLESIMALI.NEXTVAL, " & vId & ", '" & Me.DrLMillesimi.SelectedValue.ToString & "', '" & par.IfNull(par.PulisciStrSql(Me.txtDescTabella.Text), "") & "','" & par.PulisciStrSql(Me.txtDescr.Text) & "')"
                par.cmd.ExecuteNonQuery()
                DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
                par.cmd.CommandText = ""

            End If
            If Me.ListLista.Items.Count > 0 Then

                For Each o As Object In ListLista.Items
                    Dim item As System.Web.UI.WebControls.ListItem
                    item = CType(o, System.Web.UI.WebControls.ListItem)
                    If item.Selected Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.TABELLE_MILLESIMALI_SCALE (ID_TABELLA,ID_SCALA ) VALUES (SISCOM_MI.SEQ_TABELLE_MILLESIMALI.CURRVAL," & item.Value & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                Next
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = "Tab_Millesimali_SubSalvaEdifici" & ex.Message
        End Try

    End Sub
    Private Sub UpdateEdif()
        Try

            Dim i As Integer
            Dim dt As New Data.DataTable

            If Me.DrLMillesimi.SelectedValue <> vIdTipologia Then

                Dim stringa As String = "select COD_TIPOLOGIA from SISCOM_MI.TABELLE_MILLESIMALI where ID_EDIFICIO =" & vId
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                If Not IsNothing(da) Then
                    da.Fill(dt)
                End If
                For i = 0 To dt.Rows.Count - 1
                    If DrLMillesimi.SelectedValue = dt.Rows(i).Item("COD_TIPOLOGIA") Then
                        Response.Write("<script>alert('Millesimo già iserito!')</script>")
                        Exit Sub
                        Me.txtDescr.Text = ""
                        Me.txtDescTabella.Text = ""
                    End If
                Next
            End If

            par.cmd.CommandText = "UPDATE SISCOM_MI.TABELLE_MILLESIMALI SET COD_TIPOLOGIA= '" & par.PulisciStrSql(par.IfNull(Me.DrLMillesimi.SelectedValue.ToString, "")) & "', DESCRIZIONE_TABELLA ='" & par.IfNull(par.PulisciStrSql(Me.txtDescTabella.Text), "") & "',DESCRIZIONE= '" & par.PulisciStrSql(Me.txtDescr.Text) & "' WHERE ID = " & Me.HFtxtId.Value
            par.cmd.ExecuteNonQuery()
            DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.TABELLE_MILLESIMALI_SCALE WHERE ID_TABELLA =" & Me.HFtxtId.Value
            par.cmd.ExecuteNonQuery()
            '++++++++NUOVA CHECk+++++++++++++++
            If Me.ListLista.Items.Count > 0 Then

                For Each o As Object In ListLista.Items
                    Dim item As System.Web.UI.WebControls.ListItem
                    item = CType(o, System.Web.UI.WebControls.ListItem)
                    If item.Selected Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.TABELLE_MILLESIMALI_SCALE (ID_TABELLA,ID_SCALA ) VALUES (" & Me.HFtxtId.Value & "," & item.Value & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                Next
            End If
            '+++++++++fine nuova check



        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = "Tab_Millesimali_SubUpdateEdifici" & ex.Message
        End Try

    End Sub
    Public Property Selezionati() As String
        Get
            If Not (ViewState("par_Selezionati") Is Nothing) Then
                Return CStr(ViewState("par_Selezionati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Selezionati") = value
        End Set

    End Property
    'Protected Sub btnSelezionaTutto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelezionaTutto.Click
    '    If Selezionati = "" Then
    '        Selezionati = 1
    '    Else
    '        Selezionati = ""
    '    End If
    '    Dim a As Integer
    '    Dim i As Integer = 0
    '    If Selezionati <> "" Then
    '        a = ListLista.Items.Count.ToString
    '        While i < a
    '            Me.ListLista.Items(i).Selected = True
    '            i = i + 1
    '        End While
    '    Else
    '        a = ListLista.Items.Count.ToString
    '        While i < a
    '            Me.ListLista.Items(i).Selected = False
    '            i = i + 1
    '        End While
    '    End If

    'End Sub

    Protected Sub btnModifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnModifica.Click
        If Me.HFtxtId.Value <> 0 Then
            ApriVisualizzazione()
            TextBox2.Value = 2

        Else
            Me.TextBox2.Value = 0
            Response.Write("<script>alert('Selezionare una riga!');</script>")

        End If
    End Sub
    Public Sub ApriVisualizzazione()
        Try


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TABELLE_MILLESIMALI WHERE ID = " & HFtxtId.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.DrLMillesimi.SelectedValue = par.IfNull(myReader("COD_TIPOLOGIA"), "-1")
                vIdTipologia = Me.DrLMillesimi.SelectedValue
                Me.txtDescr.Text = par.IfNull((myReader("DESCRIZIONE")), "")
                Me.txtDescTabella.Text = par.IfNull((myReader("DESCRIZIONE_TABELLA")), "")
            End If
            SelecListCorr()


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = "Tab_Millesimali_SubApriVisualizzazione" & ex.Message
        End Try

    End Sub
    Private Sub SelecListCorr()

        'CaricaListBox()
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans
            If Me.Page.Title = "Inserimento Complessi" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TABELLE_MILLESIMALI_EDIFICI WHERE ID_TABELLA = " & HFtxtId.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader.Read
                    Me.ListLista.Items.FindByValue(myReader.Item("ID_EDIFICIO")).Selected = True
                    Selezionati = 1
                End While
                myReader.Close()
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TABELLE_MILLESIMALI_SCALE WHERE ID_TABELLA = " & HFtxtId.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader.Read
                    Me.ListLista.Items.FindByValue(myReader.Item("ID_SCALA")).Selected = True
                    Selezionati = 1
                End While
                myReader.Close()

            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = "Tab_Millesimali_SelecListCorr" & ex.Message
        End Try

    End Sub



    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Me.DrLMillesimi.SelectedValue = "-1"
        Me.txtDescr.Text = ""
        Me.txtDescTabella.Text = ""
        vIdTipologia = ""
        'Desleziona gli elementi della lista
        Dim a As Integer
        Dim i As Integer = 0
        a = ListLista.Items.Count.ToString
        While i < a
            Me.ListLista.Items(i).Selected = False
            i = i + 1
        End While
        TextBox2.Value = 0

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If Me.HFtxtId.Value <> 0 Then
                If txtConfElimina.Value = 1 Then

                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.TABELLE_MILLESIMALI WHERE ID = " & Me.HFtxtId.Value
                    par.cmd.ExecuteNonQuery()
                    cerca()
                    Me.txtDescr.Text = ""
                    Me.txtDescTabella.Text = ""
                    Me.DrLMillesimi.SelectedValue = "-1"
                    Me.TextBox2.Value = 0
                    Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                    Response.Write("<script>alert('Per completare l\'operazione fare click sul pulsante salva della finestra principale!');</script>")
                    DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
                Else
                    Me.HFtxtId.Value = 0
                    txtConfElimina.Value = 0
                End If

            Else
                Response.Write("<script>alert('Selezionare una riga!');</script>")

            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = "Tab_Millesimali_SubDelete" & ex.Message
        End Try

    End Sub
End Class
