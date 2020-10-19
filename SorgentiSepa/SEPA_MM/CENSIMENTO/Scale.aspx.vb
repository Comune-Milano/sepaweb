
Partial Class CENSIMENTO_Scale
    Inherits PageSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sStringaSql As String = ""
        Dim par As New CM.Global
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            vidiedificio = Request.QueryString("ID")
            cerca()
            If Session("PED2_SOLOLETTURA") = "1" Then
                FrmSolaLettura()
            End If

        End If
        TxtScala.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

    End Sub
    Private Sub FrmSolaLettura()
        Try
            Me.BtnSave.Visible = False
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub cerca()

        sStringaSql = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID_EDIFICIO = " & vidiedificio & "ORDER BY DESCRIZIONE ASC"
        QUERY = sStringaSql
        BindGrid()

    End Sub
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
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Public Property vidiedificio() As Long
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
    Public Property QUERY() As String
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


    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text

    End Sub
    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub
    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        'CONTROLLO DELLE SCALE SE SONO AUMENTATE SI AUMENTA IL NUMERO DELLE SCALE NELL'EDIFICIO
        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")
    End Sub
    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSave.Click
        Me.salva()
        Me.TxtScala.Text = ""
        cerca()

    End Sub
    Private Sub salva()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID_EDIFICIO = " & vidiedificio

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader()

            While myReader1.Read
                If Me.TxtScala.Text.ToUpper = par.IfNull(myReader1("DESCRIZIONE"), " ").ToString.ToUpper Then
                    Response.Write("<SCRIPT>alert('Scala esistente per questo edificio!');</SCRIPT>")
                    par.cmd.CommandText = ""
                    Exit Sub
                End If
            End While

            If Me.TxtScala.Text <> "" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SCALE_EDIFICI(ID,ID_EDIFICIO,DESCRIZIONE) VALUES (SISCOM_MI.SEQ_SCALE_EDIFICI.NEXTVAL , " & vidiedificio & ", '" & par.PulisciStrSql(Me.TxtScala.Text.ToUpper) & "')"
                par.cmd.ExecuteNonQuery()
                Session.Item("MODIFICASOTTOFORM") = 1
            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
    End Sub


    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If txtid.Text <> "" Then
            Me.elimina()
            Me.TxtScala.Text = ""
            cerca()
        Else
            Response.Write("<SCRIPT>alert('Selezionare l\'oggetto da eliminare!');</SCRIPT>")
        End If

    End Sub
    Private Sub elimina()
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID = " & txtid.Text
            par.cmd.ExecuteNonQuery()
            Session.Item("MODIFICASOTTOFORM") = 1
            DataGrid1.CurrentPageIndex = 0
            Me.txtmia.Text = ""

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 2292 Then
                Response.Write("<script>alert('Scala in uso!Impossibile Eliminare!');</script>")
                par.myTrans.Rollback()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
    End Sub

End Class
