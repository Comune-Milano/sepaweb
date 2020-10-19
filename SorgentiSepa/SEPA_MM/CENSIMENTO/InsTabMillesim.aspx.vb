
Partial Class CENSIMENTO_InsTabMillesim
    Inherits PageSetIdMode
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

        End If
        cerca()

    End Sub
    Private Sub FrmSolaLettura()

        Try


            Me.BtnADD.Visible = False
            Me.BtnElimina.Visible = False
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
            Me.txtid.Enabled = True
            Me.txtdesc.Enabled = True
            Me.txtmia.Enabled = True

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub cerca()
        Try
            Select Case Passato
                Case "CO"
                    sStringaSql = "select ROWNUM, ID, TABELLE_MILLESIMALI.DESCRIZIONE, TIPOLOGIA_MILLESIMALE.DESCRIZIONE as tipo, DEscrizione_tabella from SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.TIPOLOGIA_MILLESIMALE where SISCOM_MI.TABELLE_MILLESIMALI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_MILLESIMALE.COD and ID_COMPLESSO = " & vId & "ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()
                Case "ED"
                    sStringaSql = "select ROWNUM, ID, TABELLE_MILLESIMALI.DESCRIZIONE, TIPOLOGIA_MILLESIMALE.DESCRIZIONE as tipo, DEscrizione_tabella from SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.TIPOLOGIA_MILLESIMALE where SISCOM_MI.TABELLE_MILLESIMALI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_MILLESIMALE.COD and ID_EDIFICIO = " & vId & "ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()
            End Select
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
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


    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        Label2.Text = "Hai selezionato la riga n°: " & e.Item.Cells(1).Text

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

    Protected Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnADD.Click
        Response.Write("<script>window.open('TabMillesimali.aspx?ID=" & vId & ",&Pas=" & Passato & "','DATMILLESIMALI', 'resizable=yes, width=450, height=280');</script>")

    End Sub

    Protected Sub BtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnElimina.Click
        If Me.txtid.Text <> "" Then
            Me.elimina()

        End If

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script language='javascript'> { self.close() }</script>")

    End Sub
    Private Sub elimina()
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.TABELLE_MILLESIMALI WHERE ID = " & txtid.Text
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.TABELLE_MILLESIMALI_EDIFICI WHERE ID_TABELLA= " & txtid.Text
            par.cmd.ExecuteNonQuery()
            Session.Item("MODIFICASOTTOFORM") = 1
            Response.Write("<SCRIPT>alert('Elemento selezionato eiliminato correttamente!');</SCRIPT>")
            Me.txtid.Text = ""
            Me.txtmia.Text = ""
            DataGrid1.CurrentPageIndex = 0
            cerca()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnVisualiazza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessun Millesimo selezionato!')</script>")
        Else
            Response.Write("<script>window.open('TabMillesimali.aspx?ID=" & vId & ",&Pas=" & Passato & "&Millesimo=" & txtid.Text.ToString & "','DATMILLESIMALI', 'resizable=yes, width=450, height=280');</script>")

        End If

    End Sub
End Class
