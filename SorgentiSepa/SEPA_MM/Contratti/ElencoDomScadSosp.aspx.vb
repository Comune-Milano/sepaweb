
Partial Class Contratti_ElencoDomScadSosp
    Inherits System.Web.UI.Page
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


        If Not IsPostBack Then
            Response.Flush()
            BindGrid()
        End If

    End Sub

    Private Sub BindGrid()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim strSQL As String = ""
            Dim conta As Integer = 0

            Dim dtDomande As New Data.DataTable

            strSQL = "select t_motivo_domanda_vsa.id,comp_nucleo_vsa.COGNOME||' '||comp_nucleo_vsa.NOME AS RICH,INDIRIZZO||', '||CIVICO AS INDIRIZZO,CONTRATTO_NUM,t_motivo_domanda_vsa.descrizione as tipo_dom," _
            & "TO_CHAR (TO_DATE (SUBSTR (EVENTI_BANDI_VSA.DATA_ORA, 1, 8), 'yyyyMMdd') + 30, 'dd/mm/yyyy') AS DATASCAD,OPERATORI.OPERATORE," _
            & "TO_CHAR (TO_DATE (SUBSTR (EVENTI_BANDI_VSA.DATA_ORA, 1, 8), 'yyyyMMdd'),'dd/mm/yyyy') AS DATAPRES,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../VSA/NuovaDomandaVSA/domandaNuova.aspx?CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande'','''');£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as PGDOM," _
            & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../VSA/DichAUnuova.aspx?CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione'',''height=550,top=200,left=350,width=670'');£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') as pgdich " _
            & "from domande_bando_vsa,dichiarazioni_vsa,comp_nucleo_vsa,domande_vsa_alloggio,t_motivo_domanda_vsa,eventi_bandi_vsa,operatori " _
            & "where domande_bando_vsa.id_dichiarazione = dichiarazioni_vsa.id and comp_nucleo_vsa.id_dichiarazione = dichiarazioni_vsa.id " _
            & "and comp_nucleo_vsa.progr = 0 and domande_vsa_alloggio.id_domanda = domande_bando_vsa.id and eventi_bandi_vsa.id_domanda = domande_bando_vsa.id " _
            & "and t_motivo_domanda_vsa.id = domande_bando_vsa.id_motivo_domanda and fl_autorizzazione = 0 and dichiarazioni_vsa.id_stato<>2 and eventi_bandi_vsa.cod_evento = 'F193' and eventi_bandi_vsa.id_operatore=operatori.id(+) " _
            & "AND ((TO_DATE (SUBSTR (EVENTI_BANDI_VSA.DATA_ORA, 1, 8), 'yyyyMMdd')+ 30) - TO_DATE (SYSDATE) >= 0) " _
            & "AND ((TO_DATE (SUBSTR (EVENTI_BANDI_VSA.DATA_ORA, 1, 8), 'yyyyMMdd')+ 30) - TO_DATE (SYSDATE) <= 30) " _
            & "ORDER BY T_MOTIVO_DOMANDA_VSA.DESCRIZIONE,DATA_PRESENTAZIONE ASC"

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
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")
            'e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';")
        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            'e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            If DataGrid1.Items.Count > 0 Then
                Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGrid1, "ExportDomInScadeSosp", 90 / 100, , , False)
                If System.IO.File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                    Response.Redirect("..\/FileTemp\/" & nomefile, False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
End Class
