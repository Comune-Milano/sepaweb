
Partial Class VSA_Locatari_ElencoDomande
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            BindGrid()
        End If

    End Sub

    

    Private Sub BindGrid()

        Try
            Dim strSQL0 As String = ""
            Dim strSQL As String = ""
            Dim conta As Integer = 0
            Dim anniRedditi As String = ""
            Dim completaSQL As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim dtDomande As New Data.DataTable

            strSQL = "SELECT T_MOTIVO_DOMANDA_VSA.DESCRIZIONE," _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../max.aspx?CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione'',''height=550,top=0,left=0,width=670'');£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as  PG_DICHIARAZIONE, " _
                & "TO_CHAR(TO_DATE(DICHIARAZIONI_VSA.DATA_PG,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PG," _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../NuovaDomandaVSA/domandaNuova.aspx?CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande'');£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as PG_DOMANDA " _
                & "FROM DICHIARAZIONI_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA WHERE DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE " _
                & "AND T_MOTIVO_DOMANDA_VSA.ID = DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA ORDER BY DATA_PG ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
            da.Fill(dtDomande)
            DataGrid1.DataSource = dtDomande
            conta = dtDomande.Rows.Count
            DataGrid1.DataBind()

            Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & conta
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='gainsboro';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';")

        End If

    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
End Class
