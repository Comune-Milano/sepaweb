
Partial Class CENSIMENTO_AdNormativo
    Inherits PageSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global
    Dim vComplesso As String
    Dim vIndirizzo As String
    Dim vCivico As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            vId = Request.QueryString("ID")
            Passato = Request.QueryString("Pas")

            If Session("PED2_SOLOLETTURA") = "1" Then
                FrmSolaLettura()
            End If
            If Session("LE") = "1" Then
                FrmSolaLettura()
            End If

        End If
        cerca()

    End Sub
    Private Sub FrmSolaLettura()
        Try
            Me.BtnElimina.Visible = False
            Me.BtnADD.Visible = False
            'Dim CTRL As Control = Nothing
            'For Each CTRL In Me.Form1.Controls
            '    If TypeOf CTRL Is TextBox Then
            '        DirectCast(CTRL, TextBox).Enabled = False
            '    ElseIf TypeOf CTRL Is DropDownList Then
            '        DirectCast(CTRL, DropDownList).Enabled = False
            '    End If
            'Next
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Public Property Passato() As String
        Get
            If Not (ViewState("par_Passato") Is Nothing) Then
                Return CStr(ViewState("par_Passato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Passato") = value
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
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub cerca()
        Try

            Select Case Passato
                Case "UI"
                    sStringaSql = "Select ROWNUM, ID, TIPOLOGIA_ADEGUAMENTO.DESCRIZIONE AS TIPO, INTERV_ADEG_NORM.DESCRIZIONE FROM SISCOM_MI.INTERV_ADEG_NORM, SISCOM_MI.TIPOLOGIA_ADEGUAMENTO WHERE SISCOM_MI.INTERV_ADEG_NORM.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_ADEGUAMENTO.COD AND ID_UNITA_IMMOBILIARE = " & vId & " ORDER BY ROWNUM ASC "
                    QUERY = sStringaSql
                    BindGrid()

                Case "ED"

                    sStringaSql = "Select ROWNUM, ID, TIPOLOGIA_ADEGUAMENTO.DESCRIZIONE AS TIPO, INTERV_ADEG_NORM.DESCRIZIONE FROM SISCOM_MI.INTERV_ADEG_NORM, SISCOM_MI.TIPOLOGIA_ADEGUAMENTO WHERE SISCOM_MI.INTERV_ADEG_NORM.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_ADEGUAMENTO.COD AND ID_EDIFICIO = " & vId & " ORDER BY ROWNUM ASC "
                    QUERY = sStringaSql
                    BindGrid()
            End Select
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
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

    Protected Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnADD.Click
        Response.Write("<script>window.open('IntervAdeguaNorm.aspx?ID=" & vId & ",&Pas=" & Passato & "','VARIAZIONI', 'resizable=yes, width=650, height=250');</script>")
    End Sub

    Protected Sub BtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnElimina.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            Delete()
            cerca()

        End If

    End Sub
    Private Sub Delete()
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.INTERV_ADEG_NORM WHERE ID = " & txtid.Text
            par.cmd.ExecuteNonQuery()
            Session.Item("MODIFICASOTTOFORM") = 1
            Response.Write("<SCRIPT>alert('Elemento selezionato eiliminato correttamente!');</SCRIPT>")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Me.txtid.Text = ""
            Me.txtmia.Text = ""

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        'Response.Write("<script language='javascript'> { self.close() }</script>")
        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

    End Sub
End Class
