Imports System.IO

Partial Class AMMSEPA_OperatoreSUA_elencoComuniSUA
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Public Property datatablecomuni() As Data.DataTable
        Get
            If Not (ViewState("datatablecomuni") Is Nothing) Then
                Return ViewState("datatablecomuni")
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("datatablecomuni") = value
        End Set
    End Property

    Public Property datatablecomuni0() As Data.DataTable
        Get
            If Not (ViewState("datatablecomuni0") Is Nothing) Then
                Return ViewState("datatablecomuni0")
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("datatablecomuni0") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Request.QueryString("C") <> "" Then
            If Session.Item("MOD_DISTANZE_COMUNI") = "1" Then
                btnApri70KM.Visible = True
            Else
                btnApri70KM.Visible = False
            End If
        Else
            If (Session.Item("ID_CAF") = 2 And Session.Item("RESPONSABILE") = 1) Then
                btnApri70KM.Visible = True
            End If
            If Session.Item("LIVELLO") = "1" Then
                btnApri70KM.Visible = True
            End If
        End If
        
        Try
            If Not IsPostBack Then
                RiempiProvince()
                Carica()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub Carica()
        'Try
        '    If par.OracleConn.State = Data.ConnectionState.Closed Then
        '        par.OracleConn.Open()
        '        par.SettaCommand(par)
        '    End If
        '    Dim Str As String = "SELECT id,cod,nome,sigla,cap,decode(entro_70km,1,'SI',0,'NO') AS ENTRO FROM comuni_nazioni where length(sigla)=2 ORDER BY nome asc"
        '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)
        '    datatablecomuni = New Data.DataTable
        '    da.Fill(datatablecomuni)
        '    par.OracleConn.Close()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '    DataGridIntLegali.CurrentPageIndex = 0
        '    BindGrid()
        'Catch ex As Exception
        '    par.OracleConn.Close()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        'End Try
        Try
            'Dim str As String = ""
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If DropDownList1.SelectedValue <> "-1" Then
                sStringaSQL1 = "SELECT id,cod,nome,sigla,cap,decode(entro_70km,1,'SI',0,'NO') AS ENTRO,DISTANZA_KM,POPOLAZIONE FROM comuni_nazioni where length(sigla)=2 AND SIGLA='" & DropDownList1.SelectedValue & "' ORDER BY nome asc"
            Else
                sStringaSQL1 = "SELECT id,cod,nome,sigla,cap,decode(entro_70km,1,'SI',0,'NO') AS ENTRO,DISTANZA_KM,POPOLAZIONE FROM comuni_nazioni where length(sigla)=2 ORDER BY nome asc"
            End If

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            datatablecomuni = New Data.DataTable
            da.Fill(datatablecomuni)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            DataGridIntLegali.CurrentPageIndex = 0
            BindGrid()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    Private Sub BindGrid()
        DataGridIntLegali.DataSource = datatablecomuni
        DataGridIntLegali.DataBind()
    End Sub
    Protected Sub DataGridIntLegali_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridIntLegali.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            If Request.QueryString("C") <> "" Then
                If Session.Item("MOD_DISTANZE_COMUNI") = 1 Then
                    e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';")
                End If
            Else
                If (Session.Item("ID_CAF") = 2 And Session.Item("RESPONSABILE") = 1) Or Session.Item("LIVELLO") = "1" Then
                    e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';")
                End If
            End If
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            If Request.QueryString("C") <> "" Then
                If Session.Item("MOD_DISTANZE_COMUNI") = 1 Then
                    e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';")
                End If
            Else
                If (Session.Item("ID_CAF") = 2 And Session.Item("RESPONSABILE") = 1) Or Session.Item("LIVELLO") = "1" Then
                    e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';")
                End If
            End If
        End If
    End Sub
    Protected Sub DataGridIntLegali_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridIntLegali.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridIntLegali.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
    Private Sub RiempiProvince()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            DropDownList1.Items.Clear()
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI where length(sigla)=2 AND SIGLA <> '00' ORDER BY SIGLA", DropDownList1, "SIGLA", "SIGLA", True)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            DropDownList1.Items.FindByText("MI").Selected = True
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        Carica()
    End Sub

    Private Sub CancellaBR(ByVal dt As Data.DataTable)
        Dim posizione As Integer = -1
        Try
            For Each riga As Data.DataRow In dt.Rows
                For Each colonna As Data.DataColumn In dt.Columns
                    If Not IsDBNull(riga.Item(colonna)) Then
                        posizione = riga.Item(colonna).ToString.IndexOf("<br />")
                        If posizione <> -1 Then
                            riga.Item(colonna) = Replace(riga.Item(colonna), "<br />", ", ")
                            riga.Item(colonna) = Left(riga.Item(colonna), Len(riga.Item(colonna)) - 2)
                        End If
                    End If
                Next
            Next
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        If DataGridIntLegali.Items.Count > 0 Then
            Dim datatablecomuniX As New System.Data.DataTable

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            da.Fill(datatablecomuniX)
            datatablecomuniX.Columns.Remove("ID")
            'CancellaBR(datatablecomuniX)
            Dim nomefile As String = par.EsportaExcelDaDT(datatablecomuniX, "ExportComuni", False, False, False, True)



            If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
                Response.Redirect("../../FileTemp/" & nomefile)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
            End If
        Else
            Response.Write("<script>alert('Nessun dato da esportare!')</script>")
        End If
    End Sub

    Protected Sub btnApri70KM_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnApri70KM.Click
        Carica()
    End Sub
End Class
