
Partial Class NEW_CENSIMENTO_Tab_ImpComuni
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Cerca()
            CaricaCmbTipologia()
            txtQuant.Attributes.Add("onkeyUp", "javascript:valid(this,'notnumbers');")
            If Session("SLE") = "1" Or CType(Me.Page.FindControl("txtVisibility"), HiddenField).Value = 1 Then
                Me.BtnElimina.Visible = False
            End If
        End If

    End Sub
    Private Sub Cerca()
        Try
            If Session("SLE") = "1" Then
                par.OracleConn.Open()
                par.SettaCommand(par)

            Else
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

            End If


            Dim StringaSql As String = ""

            If Me.Page.Title = "Inserimento EDIFICI" Then
                StringaSql = "select imp_comuni_ce.id_tipologia, descrizione as TipoImp, quant,STATO from SISCOM_MI.imp_comuni_ce, SISCOM_MI.tipologia_imp_comuni_ce where imp_comuni_ce.id_edificio=" & CType(Me.Page, Object).vId & " and imp_comuni_ce.id_tipologia = tipologia_imp_comuni_ce.id"
            ElseIf Me.Page.Title = "Inserimento Complessi" Then
                StringaSql = "select imp_comuni_ce.id_tipologia, descrizione as TipoImp, quant,STATO from SISCOM_MI.imp_comuni_ce, SISCOM_MI.tipologia_imp_comuni_ce where imp_comuni_ce.id_complesso=" & CType(Me.Page, Object).vId & " and imp_comuni_ce.id_tipologia = tipologia_imp_comuni_ce.id"
            End If

            par.cmd.CommandText = StringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")

            DgvImpComuni.DataSource = ds
            DgvImpComuni.DataBind()

            If Session("SLE") = "1" Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try

    End Sub
    Private Sub CaricaCmbTipologia()
        Try
            If Session("SLE") = "1" Then
                par.OracleConn.Open()
                par.SettaCommand(par)

            Else
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

            End If

            Me.cmbStato.Items.Add("PRESENTE")
            Me.cmbStato.Items.Add("ASSENTE")
            Me.cmbStato.Items.Add("NON VALUTABILE")
            par.cmd.CommandText = "Select * from SISCOM_MI.tipologia_imp_comuni_ce"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbTipoImpianto.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbTipoImpianto.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()

            If Session("SLE") = "1" Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try

    End Sub

    Protected Sub DgvImpComuni_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DgvImpComuni.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_ImpComuni1_txtmia').value='Hai selezionato il servizio: " & e.Item.Cells(1).Text & "';document.getElementById('Tab_ImpComuni1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_ImpComuni1_txtmia').value='Hai selezionato il servizio: " & e.Item.Cells(1).Text & "';document.getElementById('Tab_ImpComuni1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Protected Sub BtnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSalva.Click
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If Me.Page.Title = "Inserimento EDIFICI" Then

                If Me.txtQuant.Text <> "" AndAlso Me.cmbTipoImpianto.SelectedValue <> -1 Then
                    Dim i As Integer
                    Dim dt As New Data.DataTable
                    Dim stringa As String = "select id_tipologia from SISCOM_MI.IMP_comuni_ce where id_edificio =" & CType(Me.Page, Object).vId
                    par.cmd.CommandText = stringa
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    If Not IsNothing(da) Then
                        da.Fill(dt)
                    End If
                    For i = 0 To dt.Rows.Count - 1
                        If cmbTipoImpianto.SelectedValue = dt.Rows(i).Item("ID_TIPOLOGIA") Then
                            Response.Write("<script>alert('Impianto già iserito!')</script>")
                            Exit Sub
                            Me.txtQuant.Text = ""
                            Me.cmbTipoImpianto.SelectedValue = -1

                        End If
                    Next
                    par.cmd.CommandText = "insert into SISCOM_MI.IMP_comuni_ce (ID_EDIFICIO, ID_TIPOLOGIA, QUANT,STATO) values (" & CType(Me.Page, Object).vId & ", " & cmbTipoImpianto.SelectedValue & ", " & txtQuant.Text & ",'" & Me.cmbStato.SelectedItem.Text & "')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    Me.txtQuant.Text = ""
                    Me.cmbTipoImpianto.SelectedValue = -1
                    'Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")
                    Cerca()
                    CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

                End If

            ElseIf Me.Page.Title = "Inserimento Complessi" Then
                If Me.txtQuant.Text <> "" AndAlso Me.cmbTipoImpianto.SelectedValue <> -1 Then
                    Dim i As Integer
                    Dim dt As New Data.DataTable
                    Dim stringa As String = "select id_tipologia from SISCOM_MI.IMP_comuni_ce where ID_COMPLESSO =" & CType(Me.Page, Object).vId
                    par.cmd.CommandText = stringa
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    If Not IsNothing(da) Then
                        da.Fill(dt)
                    End If
                    For i = 0 To dt.Rows.Count - 1
                        If cmbTipoImpianto.SelectedValue = dt.Rows(i).Item("ID_TIPOLOGIA") Then
                            Response.Write("<script>alert('Impianto già iserita!')</script>")
                            Exit Sub
                            Me.txtQuant.Text = ""
                            Me.cmbTipoImpianto.SelectedValue = -1

                        End If
                    Next
                    par.cmd.CommandText = "insert into SISCOM_MI.IMP_comuni_ce (ID_COMPLESSO, ID_TIPOLOGIA, QUANT,STATO) values (" & CType(Me.Page, Object).vId & ", " & cmbTipoImpianto.SelectedValue & ", " & txtQuant.Text & ",'" & Me.cmbStato.SelectedItem.Text & "')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    Me.txtQuant.Text = ""
                    Me.cmbTipoImpianto.SelectedValue = -1
                    'Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")
                    Cerca()
                    CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

                End If
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnElimina.Click
        'Response.Write("<script>alert('Funzione in fase di test!Non ancora disponibile!');</script>")


        Try

            If HFtxtId.Value <> "" Then
                If Me.Page.Title = "Inserimento EDIFICI" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "delete from SISCOM_MI.imp_comuni_ce where id_EDIFICIO = " & CType(Me.Page, Object).vId & " and ID_TIPOLOGIA = '" & HFtxtId.Value & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    'par.OracleConn.Close()
                    HFtxtId.Value = ""
                    Me.txtmia.Text = "Nessuna Selezione"
                    CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1


                ElseIf Me.Page.Title = "Inserimento Complessi" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "delete from SISCOM_MI.imp_comuni_ce where id_complesso = " & CType(Me.Page, Object).vId & " and ID_TIPOLOGIA = '" & HFtxtId.Value & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    'par.OracleConn.Close()
                    CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
                    HFtxtId.Value = ""
                    Me.txtmia.Text = "Nessuna Selezione"

                End If
                Cerca()

            Else
                Response.Write("<script>alert('Selezionare il rigo da cancellare!');</script>")

            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub
End Class
