
Partial Class Contratti_Pagamenti_ElCrediti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        'Str = "<div align='center' id='divPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Contabilita/IMMCONTABILITA/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        'Str = Str & "<" & "/div>"

        'Response.Write(Str)

        If par.IfEmpty(Request.QueryString("IDCONT"), 0) > 0 Then
            CaricaCrediti()
        End If

    End Sub
    Private Sub CaricaCrediti()
        '*****************APERTURA CONNESSIONE***************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        par.cmd.CommandText = "SELECT ID,TO_CHAR(TO_DATE(CREDITI.DATA , 'yyyymmdd'),'dd/mm/yyyy') as data, trim(to_char(nvl(importo,0),'9G999G999G990D99')) as importo FROM SISCOM_MI.CREDITI WHERE ID_CONTRATTO = " & Request.QueryString("IDCONT")
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)

        Dim dt As New Data.DataTable()
        da.Fill(dt)
        Dim Totale As Decimal = 0
        For Each row As Data.DataRow In dt.Rows
            Totale = Totale + par.IfNull(row.Item("IMPORTO").ToString.Replace(".", ""), 0)
        Next
        Dim r As Data.DataRow
        r = dt.NewRow()
        r.Item("DATA") = "TOTALE"
        r.Item("IMPORTO") = Format(Totale, "##,##0.00")
        dt.Rows.Add(r)

        If dt.Rows.Count > 0 Then
            Me.DataGridBollette.DataSource = dt
            Me.DataGridBollette.DataBind()
        End If
        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


    End Sub
End Class
