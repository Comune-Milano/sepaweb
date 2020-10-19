
Partial Class CENSIMENTO_TabMillesimaliaspx
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Write("<script language='javascript'> { self.close() }</script>")

        'Response.Write("<script>window.open('InsTabMillesim.aspx?ID=" & vId & ",&Pas=" & Passato & "','DIMENSIONI', 'resizable=yes, width=630, height=280');</script>")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            vId = Request.QueryString("ID")
            Passato = Request.QueryString("Pas")
            vIdMillesimo = Request.QueryString("Millesimo")
            Me.caricaTipi()
            CaricaListBox()
            If vIdMillesimo <> 0 Then
                ApriVisualiz()
                Me.vIdTipologia = Me.DrLMillesimi.SelectedValue.ToString
            End If
            If Session("PED2_SOLOLETTURA") = "1" Then
                FrmSolaLettura()
            End If
        End If

    End Sub
    Private Sub FrmSolaLettura()
        Try
            Me.BtnADD.Visible = False
            Me.ListEdifci.Enabled = False
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
            Me.btnSelezionaTutto.Enabled = False
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

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
    Public Property vIdMillesimo() As Long
        Get
            If Not (ViewState("par_lIdMillesimo") Is Nothing) Then
                Return CLng(ViewState("par_lIdMillesimo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdMillesimo") = value
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
    Public Property Passato() As String
        Get
            If Not (ViewState("par_lIPassato") Is Nothing) Then
                Return CStr(ViewState("par_lIPassato"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIPassato") = value
        End Set

    End Property

    Private Function caricaTipi()
        Dim ds As New Data.DataSet
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

        Try

            da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT * FROM SISCOM_MI.TIPOLOGIA_MILLESIMALE", par.OracleConn)
            da.Fill(ds)

            DrLMillesimi.DataSource = ds
            DrLMillesimi.DataTextField = "DESCRIZIONE"
            DrLMillesimi.DataValueField = "COD"
            DrLMillesimi.DataBind()


            ds = New Data.DataSet
            da = Nothing
            par.OracleConn.Close()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try

    End Function

    Protected Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnADD.Click
        If Me.vIdMillesimo = 0 Then

            Me.salva()
        Else
            Me.Update()
        End If
    End Sub
    Private Sub Update()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Select Case Passato
                Case "CO"

                    Dim i As Integer
                    Dim dt As New Data.DataTable

                    If Me.DrLMillesimi.SelectedValue <> vIdTipologia Then

                        Dim stringa As String = "select COD_TIPOLOGIA from SISCOM_MI.TABELLE_MILLESIMALI where id_complesso =" & vId
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(stringa, par.OracleConn)
                        If Not IsNothing(da) Then
                            da.Fill(dt)
                        End If
                        For i = 0 To dt.Rows.Count - 1
                            If DrLMillesimi.SelectedValue = dt.Rows(i).Item("COD_TIPOLOGIA") Then
                                Response.Write("<script>alert('Millesimo già iserito!')</script>")
                                Exit Sub
                                Me.txtDescr.Text = ""
                                Me.TxtDescTab.Text = ""
                            End If
                        Next
                    End If

                    par.cmd.CommandText = "UPDATE SISCOM_MI.TABELLE_MILLESIMALI SET COD_TIPOLOGIA= '" & par.PulisciStrSql(par.IfNull(Me.DrLMillesimi.SelectedValue.ToString, "")) & "', DESCRIZIONE_TABELLA ='" & par.IfNull(par.PulisciStrSql(Me.TxtDescTab.Text), "") & "',DESCRIZIONE= '" & par.PulisciStrSql(Me.txtDescr.Text) & "' WHERE ID = " & vIdMillesimo
                    par.cmd.ExecuteNonQuery()
                    Session.Item("MODIFICASOTTOFORM") = 1
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.TABELLE_MILLESIMALI_EDIFICI WHERE ID_TABELLA =" & vIdMillesimo
                    par.cmd.ExecuteNonQuery()
                    '++++++++NUOVA CHECk+++++++++++++++
                    If Me.ListEdifci.Items.Count > 0 Then

                        For Each o As Object In ListEdifci.Items
                            Dim item As System.Web.UI.WebControls.ListItem
                            item = CType(o, System.Web.UI.WebControls.ListItem)
                            If item.Selected Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.TABELLE_MILLESIMALI_EDIFICI (ID_TABELLA,ID_EDIFICIO ) VALUES (" & vIdMillesimo & "," & item.Value & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        Next
                    End If
                    '+++++++++fine nuova check
                Case "ED"
                    Dim i As Integer
                    Dim dt As New Data.DataTable
                    If Me.DrLMillesimi.SelectedValue <> vIdTipologia Then



                        Dim stringa As String = "select COD_TIPOLOGIA from SISCOM_MI.TABELLE_MILLESIMALI where id_edificio =" & vId
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(stringa, par.OracleConn)
                        If Not IsNothing(da) Then
                            da.Fill(dt)
                        End If
                        For i = 0 To dt.Rows.Count - 1
                            If DrLMillesimi.SelectedValue = dt.Rows(i).Item("COD_TIPOLOGIA") Then
                                Response.Write("<script>alert('Millesimo già iserito!!')</script>")
                                Exit Sub
                                Me.txtDescr.Text = ""
                                Me.TxtDescTab.Text = ""
                            End If
                        Next
                    End If

                    par.cmd.CommandText = "UPDATE SISCOM_MI.TABELLE_MILLESIMALI SET COD_TIPOLOGIA= '" & par.PulisciStrSql(par.IfNull(Me.DrLMillesimi.SelectedValue.ToString, "")) & "', DESCRIZIONE_TABELLA ='" & par.IfNull(par.PulisciStrSql(Me.TxtDescTab.Text), "") & "',DESCRIZIONE= '" & par.PulisciStrSql(Me.txtDescr.Text) & "' WHERE ID = " & vIdMillesimo
                    par.cmd.ExecuteNonQuery()
                    Session.Item("MODIFICASOTTOFORM") = 1
                    'par.cmd.CommandText = "DELETE FROM SISCOM_MI.TABELLE_MILLESIMALI_EDIFICI WHERE ID_TABELLA =" & vIdMillesimo
                    'par.cmd.ExecuteNonQuery()
                    ''++++++++NUOVA CHECk+++++++++++++++
                    'If Me.ListEdifci.Items.Count > 0 Then

                    '    For Each o As Object In ListEdifci.Items
                    '        Dim item As System.Web.UI.WebControls.ListItem
                    '        item = CType(o, System.Web.UI.WebControls.ListItem)
                    '        If item.Selected Then
                    '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.TABELLE_MILLESIMALI_EDIFICI (ID_TABELLA,ID_EDIFICIO ) VALUES (" & vIdMillesimo & "," & item.Value & ")"
                    '            par.cmd.ExecuteNonQuery()
                    '        End If
                    '    Next
                    'End If
                    ''+++++++++fine nuova check

            End Select

            Response.Write("<SCRIPT>alert('Modifica eseguita correttamente!');</SCRIPT>")
            Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        End Try

    End Sub
    Private Sub salva()
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        Try
            If Me.TxtDescTab.Text <> "" AndAlso Me.txtDescr.Text <> "" Then


                Select Case Passato
                    Case "CO"
                        Dim i As Integer
                        Dim dt As New Data.DataTable
                        Dim stringa As String = "select COD_TIPOLOGIA from SISCOM_MI.TABELLE_MILLESIMALI where id_complesso =" & vId
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(stringa, par.OracleConn)
                        If Not IsNothing(da) Then
                            da.Fill(dt)
                        End If
                        For i = 0 To dt.Rows.Count - 1
                            If DrLMillesimi.SelectedValue = dt.Rows(i).Item("COD_TIPOLOGIA") Then
                                Response.Write("<script>alert('Millesimo già iserito!')</script>")
                                Exit Sub
                                Me.txtDescr.Text = ""
                                Me.TxtDescTab.Text = ""
                            End If
                        Next
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.TABELLE_MILLESIMALI (ID, ID_COMPLESSO, COD_TIPOLOGIA, DESCRIZIONE_TABELLA, DESCRIZIONE) VALUES" _
                        & "(SISCOM_MI.SEQ_TABELLE_MILLESIMALI.NEXTVAL, " & vId & ", '" & Me.DrLMillesimi.SelectedValue.ToString & "', '" & par.IfNull(par.PulisciStrSql(Me.TxtDescTab.Text), "") & "','" & par.PulisciStrSql(Me.txtDescr.Text) & "')"
                        par.cmd.ExecuteNonQuery()
                        Session.Item("MODIFICASOTTOFORM") = 1
                        par.cmd.CommandText = ""
                        'Response.Write("<SCRIPT>alert('Salvataggio completato correttamente!');</SCRIPT>")
                        'Me.txtDescr.Text = ""
                        'Me.TxtDescTab.Text = ""

                        '++++++++NUOVA CHECk+++++++++++++++
                        If Me.ListEdifci.Items.Count > 0 Then

                            For Each o As Object In ListEdifci.Items
                                Dim item As System.Web.UI.WebControls.ListItem
                                item = CType(o, System.Web.UI.WebControls.ListItem)
                                If item.Selected Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.TABELLE_MILLESIMALI_EDIFICI (ID_TABELLA,ID_EDIFICIO ) VALUES (SISCOM_MI.SEQ_TABELLE_MILLESIMALI.CURRVAL," & item.Value & ")"
                                    par.cmd.ExecuteNonQuery()
                                End If
                            Next
                        End If
                        '+++++++++fine nuova check

                    Case "ED"
                        Dim i As Integer
                        Dim dt As New Data.DataTable
                        Dim stringa As String = "select COD_TIPOLOGIA from SISCOM_MI.TABELLE_MILLESIMALI where id_edificio =" & vId
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(stringa, par.OracleConn)
                        If Not IsNothing(da) Then
                            da.Fill(dt)
                        End If
                        For i = 0 To dt.Rows.Count - 1
                            If DrLMillesimi.SelectedValue = dt.Rows(i).Item("COD_TIPOLOGIA") Then
                                Response.Write("<script>alert('Millesimo già iserito!!')</script>")
                                Exit Sub
                                Me.txtDescr.Text = ""
                                Me.TxtDescTab.Text = ""
                            End If
                        Next

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.TABELLE_MILLESIMALI (ID, ID_EDIFICIO, COD_TIPOLOGIA, DESCRIZIONE_TABELLA, DESCRIZIONE) VALUES" _
                        & "(SISCOM_MI.SEQ_TABELLE_MILLESIMALI.NEXTVAL, " & vId & ", '" & Me.DrLMillesimi.SelectedValue.ToString & "', '" & par.IfNull(par.PulisciStrSql(Me.TxtDescTab.Text), "") & "','" & par.PulisciStrSql(Me.txtDescr.Text) & "')"
                        par.cmd.ExecuteNonQuery()
                        Session.Item("MODIFICASOTTOFORM") = 1
                        par.cmd.CommandText = ""

                        'Me.txtDescr.Text = ""
                        'Me.TxtDescTab.Text = ""

                End Select
                Response.Write("<SCRIPT>alert('Salvataggio completato correttamente!');</SCRIPT>")
                Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

            Else
                Response.Write("<SCRIPT>alert('Valorizzare tutti i campi prima di procedere con il salvataggio!');</SCRIPT>")

            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try

    End Sub


    'Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
    '    Response.Write("<script language='javascript'> { self.close() }</script>")

    '    Response.Write("<script>window.open('InsTabMillesim.aspx?ID=" & vId & ",&Pas=" & Passato & "','DIMENSIONI', 'resizable=yes, width=630, height=280');</script>")

    'End Sub
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
    Private Sub CaricaListBox()
        Try
            If Passato = "CO" And vId <> 0 Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Me.ListEdifci.Items.Clear()
                par.cmd.CommandText = "select  edifici.id  ,('COD. '||edifici.cod_edificio ||' - - '||edifici.denominazione) as DESCRIZIONE from SISCOM_MI.edifici where edifici.id_complesso = " & vId & " order by edifici.cod_edificio asc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    ListEdifci.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
                End While
                myReader.Close()
                If Me.ListEdifci.Items.Count > 0 Then
                    Me.lblEdifAssociati.Visible = True
                    Me.btnSelezionaTutto.Visible = True
                Else
                    Me.lblEdifAssociati.Visible = False
                    Me.btnSelezionaTutto.Visible = False

                End If
                myReader.Close()
            End If
            '300000046
            par.OracleConn.Close()
            '& Me.DLRComplessi.SelectedValue.ToString & 
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub ApriVisualiz()
        Try
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TABELLE_MILLESIMALI WHERE ID = " & vIdMillesimo
            da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT * FROM SISCOM_MI.TABELLE_MILLESIMALI WHERE ID = " & vIdMillesimo, par.OracleConn)
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Me.DrLMillesimi.SelectedValue = par.IfNull((dt.Rows(0).Item("COD_TIPOLOGIA")), "-1")
                Me.txtDescr.Text = par.IfNull((dt.Rows(0).Item("DESCRIZIONE")), "")
                Me.TxtDescTab.Text = par.IfNull((dt.Rows(0).Item("DESCRIZIONE_TABELLA")), "")

            End If
            par.OracleConn.Close()
            ApriUCcorrelateEdifici()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub ApriUCcorrelateEdifici()

        CaricaListBox()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TABELLE_MILLESIMALI_EDIFICI WHERE ID_TABELLA = " & vIdMillesimo
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader.Read
                Me.ListEdifci.Items.FindByValue(myReader.Item("ID_EDIFICIO")).Selected = True
                Selezionati = 1
            End While
            myReader.Close()

            par.OracleConn.Close()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnSelezionaTutto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelezionaTutto.Click
        If Selezionati = "" Then
            Selezionati = 1
        Else
            Selezionati = ""
        End If
        Dim a As Integer
        Dim i As Integer = 0
        If Selezionati <> "" Then
            a = ListEdifci.Items.Count.ToString
            While i < a
                Me.ListEdifci.Items(i).Selected = True
                i = i + 1
            End While
        Else
            a = ListEdifci.Items.Count.ToString
            While i < a
                Me.ListEdifci.Items(i).Selected = False
                i = i + 1
            End While
        End If

    End Sub
End Class
