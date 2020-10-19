
Partial Class Contratti_ElencoErrDepCauz
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

            Dim dt As New Data.DataTable
            'mod_contratti_PARAM
            par.cmd.CommandText = "SELECT * FROM OPERATORI WHERE ID=" & CStr(Session.Item("ID_OPERATORE"))
            Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderJ.Read Then
                If par.IfNull(myReaderJ("LIVELLO_WEB"), "0") = "1" Or Session.Item("ID_OPERATORE") = "72" Then
                    strSQL = "SELECT TO_CHAR(TO_DATE(DATA_REST_INTERESSI,'YYYYMMDD'),'DD/MM/YYYY') AS DATA_REST_INTERESSI, " _
                           & "TO_CHAR(TO_DATE(PROCEDURE_REST_INT.INIZIO,'YYYYMMDDHH24MISS'),'DD/MM/YYYY-HH24:MI:SS') AS INIZIO, " _
                           & "(CASE WHEN TIPO_ERRORE=1 THEN replace(replace('<a href=£javascript:void(0)£ onclick=£location.href=''InteressiLegali.aspx?R=1'';£>'||ERRORE||'</a>','$','&'),'£','" & Chr(34) & "') ELSE ERRORE END) AS ERRORE, " _
                           & "OPERATORI.OPERATORE " _
                           & "FROM " _
                           & "SISCOM_MI.PROCEDURE_REST_INT LEFT OUTER JOIN OPERATORI ON (OPERATORI.ID=OPERATORE_VISTO) WHERE ESITO=1 AND VISTO=0"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
                    da.Fill(dt)
                    DataGrid1.DataSource = dt
                    DataGrid1.DataBind()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PROCEDURE_REST_INT SET OPERATORE_VISTO=" & CStr(Session.Item("ID_OPERATORE")) & " WHERE ESITO=1 AND VISTO=0"
                    par.cmd.ExecuteNonQuery()
                Else
                    If par.IfNull(myReaderJ("mod_contratti_PARAM"), "0") = "1" Then
                        strSQL = "SELECT TO_CHAR(TO_DATE(DATA_REST_INTERESSI,'YYYYMMDD'),'DD/MM/YYYY') AS DATA_REST_INTERESSI, " _
                               & "TO_CHAR(TO_DATE(PROCEDURE_REST_INT.INIZIO,'YYYYMMDDHH24MISS'),'DD/MM/YYYY-HH24:MI:SS') AS INIZIO, " _
                               & "replace(replace('<a href=£javascript:void(0)£ onclick=£location.href=''InteressiLegali.aspx?R=1'';£>'||ERRORE||'</a>','$','&'),'£','" & Chr(34) & "') as ERRORE, " _
                               & "OPERATORI.OPERATORE " _
                               & "FROM " _
                               & "SISCOM_MI.PROCEDURE_REST_INT LEFT OUTER JOIN OPERATORI ON (OPERATORI.ID=OPERATORE_VISTO) WHERE TIPO_ERRORE=1 AND ESITO=1 AND VISTO=0"
                    Else
                        strSQL = "SELECT TO_CHAR(TO_DATE(DATA_REST_INTERESSI,'YYYYMMDD'),'DD/MM/YYYY') AS DATA_REST_INTERESSI, " _
                               & "TO_CHAR(TO_DATE(PROCEDURE_REST_INT.INIZIO,'YYYYMMDDHH24MISS'),'DD/MM/YYYY-HH24:MI:SS') AS INIZIO, " _
                               & "ERRORE, " _
                               & "OPERATORI.OPERATORE FROM SISCOM_MI.PROCEDURE_REST_INT LEFT OUTER JOIN OPERATORI ON (OPERATORI.ID=OPERATORE_VISTO) " _
                               & "WHERE TIPO_ERRORE=1 AND ESITO=1 AND VISTO=0"
                    End If
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
                    da.Fill(dt)
                    DataGrid1.DataSource = dt
                    DataGrid1.DataBind()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PROCEDURE_REST_INT SET OPERATORE_VISTO=" & CStr(Session.Item("ID_OPERATORE")) & " WHERE ESITO=1 AND VISTO=0"
                    par.cmd.ExecuteNonQuery()
                End If
            End If
            myReaderJ.Close()



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
                Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGrid1, "ExportErroriElabDepCauz", 90 / 100, , , False)
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
