
Partial Class CENSIMENTO_Tab_AnCantine
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

            par.cmd.CommandText = "select Anomalie_uc.id_tipologia, descrizione as Tipo, valore from SISCOM_MI.anomalie_uc, SISCOM_MI.tipo_anomalie_uc where anomalie_uc.id_UNITA_COMUNE =" & CType(Me.Page, Object).vId & " and anomalie_uc.id_tipologia = tipo_anomalie_uc.id"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)


            Dim ds As New Data.DataSet()
            da.Fill(ds, "DIMENSIONI")
            DgvCaratteristiche.DataSource = ds
            DgvCaratteristiche.DataBind()

            da.Dispose()
            ds.Dispose()

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
        If Session("SLE") = "1" Then
            par.OracleConn.Open()
            par.SettaCommand(par)

        Else
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

        End If

        par.cmd.CommandText = "Select * from SISCOM_MI.tipo_anomalie_uc"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        cmbTipoDotazione.Items.Add(New ListItem(" ", -1))
        While myReader1.Read
            cmbTipoDotazione.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
        End While
        myReader1.Close()

        If Session("SLE") = "1" Then
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If

    End Sub

    Protected Sub BtnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSalva.Click
        If Me.cmbTipoDotazione.SelectedValue <> "-1" And String.IsNullOrEmpty(Me.txtQuant.Text) = False Then

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim i As Integer
            Dim dt As New Data.DataTable
            Dim stringa As String = "select id_tipologia from SISCOM_MI.anomalie_uc where id_unita_comune =" & CType(Me.Page, Object).vId & " and id_tipologia = " & Me.cmbTipoDotazione.SelectedValue.ToString
            par.cmd.CommandText = stringa

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            If Not IsNothing(da) Then
                da.Fill(dt)
            End If
            For i = 0 To dt.Rows.Count - 1
                If cmbTipoDotazione.SelectedValue = dt.Rows(i).Item("ID_TIPOLOGIA") Then
                    Response.Write("<script>alert('Dotazione già iserita!')</script>")
                    Exit Sub
                    Me.txtQuant.Text = ""
                    Me.cmbTipoDotazione.SelectedValue = -1

                End If
            Next
            par.cmd.CommandText = "insert into SISCOM_MI.anomalie_uc (ID_UNITA_COMUNE, ID_TIPOLOGIA, valore) values (" & CType(Me.Page, Object).vId & ", " & Me.cmbTipoDotazione.SelectedValue & ", " & Me.txtQuant.Text & ")"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            Me.txtQuant.Text = ""
            Me.cmbTipoDotazione.SelectedValue = -1
            Cerca()
            'par.OracleConn.Close()
            Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")
        Else
            Response.Write("<script>alert('Ineserire i dati per procedere con il salvataggio delle informazioni!')</script>")

        End If

    End Sub

    Protected Sub DgvCaratteristiche_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DgvCaratteristiche.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_AnCantine1_txtmia').value='Hai selezionato " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('Tab_AnCantine1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_AnCantine1_txtmia').value='Hai selezionato " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('Tab_AnCantine1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")
        End If
    End Sub

    Protected Sub BtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnElimina.Click
        Try
            If Me.HFtxtId.Value <> "" Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "delete from SISCOM_MI.anomalie_uc where id_unita_comune = " & CType(Me.Page, Object).vId & " and ID_TIPOLOGIA = " & HFtxtId.Value
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                Cerca()
                Me.txtmia.Text = "Nessuna Selezione"
                Me.HFtxtId.Value = ""
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " tabcaratteristiche"

        End Try
    End Sub
End Class
