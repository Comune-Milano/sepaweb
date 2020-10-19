
Partial Class VSA_Correlazioni3
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreCF As String
    Dim sValoreID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            sValoreCF = par.PulisciStrSql(Request.QueryString("CF"))
            sValoreID = par.PulisciStrSql(Request.QueryString("ID"))
            If Request.QueryString("V") = 1 Then
                btnStampa.Visible = False
            End If
            Label4.Text = "Correlazioni trovate Cod. Fiscale " & sValoreCF
            If Len(sValoreCF) = 16 Then
                If IsNumeric(sValoreID) Then
                    s_Stringasql = "SELECT DICHIARAZIONI_vsa.ID,DICHIARAZIONI_vsa.PG ,TO_CHAR(TO_DATE(DICHIARAZIONI_vsa.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS  ""DATA_PG"",DECODE(COMP_NUCLEO_vsa.PROGR,0,'RICHIEDENTE') AS ""INTESTAZIONE"" FROM DICHIARAZIONI_vsa,COMP_NUCLEO_vsa WHERE DICHIARAZIONI_vsa.ID=COMP_NUCLEO_vsa.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO_vsa.COD_FISCALE='" & sValoreCF & "' OR COMP_NUCLEO_vsa.COD_FISCALE='" & sValoreCF & "') AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE<>" & sValoreID & " ORDER BY COMP_NUCLEO_vsa.COD_FISCALE ASC"
                    Verifica()
                End If
            End If

        End If
    End Sub

    Private Function Verifica()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(s_Stringasql, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "DICHIARAZIONI_vsa,COMP_NUCLEO_vsa")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        par.OracleConn.Close()
        par.OracleConn.Dispose()

    End Function

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizza.Click
        Response.Write("<script>window.close();</script>")

    End Sub

    Public Property s_Stringasql() As String
        Get
            If Not (ViewState("par_s_Stringasql") Is Nothing) Then
                Return CStr(ViewState("par_s_Stringasql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_s_Stringasql") = value
        End Set

    End Property

    Public Property s_Stringasql1() As String
        Get
            If Not (ViewState("par_s_Stringasql1") Is Nothing) Then
                Return CStr(ViewState("par_s_Stringasql1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_s_Stringasql1") = value
        End Set

    End Property

    Public Property s_Valore() As String
        Get
            If Not (ViewState("par_s_Valore") Is Nothing) Then
                Return CStr(ViewState("par_s_Valore"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_s_Valore") = value
        End Set

    End Property

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        s_Stringasql1 = "SELECT COMP_NUCLEO_vsa.COGNOME,COMP_NUCLEO_vsa.NOME,COMP_NUCLEO_vsa.COD_FISCALE ,TO_CHAR(TO_DATE(COMP_NUCLEO_vsa.DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS  ""DATA_NASCITA"",T_TIPO_PARENTELA.DESCRIZIONE AS ""PARENTELA"" FROM T_TIPO_PARENTELA,COMP_NUCLEO_vsa WHERE COMP_NUCLEO_vsa.ID_DICHIARAZIONE=" & e.Item.Cells(0).Text & " AND  COMP_NUCLEO_vsa.GRADO_PARENTELA=T_TIPO_PARENTELA.COD ORDER BY COMP_NUCLEO_vsa.PROGR ASC"

        s_Valore = e.Item.Cells(1).Text
        AggiornaComp()
    End Sub

    Function AggiornaComp()
        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(s_Stringasql1, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "DICHIARAZIONI_vsa,COMP_NUCLEO_vsa")
        DataGrid2.DataSource = ds
        DataGrid2.DataBind()
        Label2.Text = "Composizione Nucleo PG " & s_Valore
        par.OracleConn.Close()
        par.OracleConn.Dispose()
    End Function

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            Verifica()
        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampa.Click
        Response.Write("<script>window.print();</script>")

    End Sub

    Protected Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid2.CurrentPageIndex = e.NewPageIndex
            AggiornaComp()
        End If
    End Sub

    Protected Sub DataGrid2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid2.SelectedIndexChanged

    End Sub
End Class
