
Partial Class Contratti_StoricoDatiRegistrazione
    Inherits System.Web.UI.Page
    Dim par As New CM.Global()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim Str As String = ""
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            CaricaDati()
        End If

    End Sub
    Private Sub CaricaDati()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()

            par.cmd.CommandText = "SELECT " _
                & " ID, ID_CONTRATTO, NUM_REGISTRAZIONE, " _
                & " SERIE_REGISTRAZIONE, getdata(DATA_REG) as DATA_REG, getdata(DATA_ASSEGNAZIONE_PG) as DATA_ASSEGNAZIONE_PG, " _
                & " NRO_ASSEGNAZIONE_PG" _
                & " FROM SISCOM_MI.RAPP_UTENZA_INFO_REGISTRAZONE where " _
                & " ID_CONTRATTO=" & Request.QueryString("IDC") & " order by id asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()

            DataGrid.DataSource = dt
            DataGrid.DataBind()

            par.OracleConn.Close()
            par.OracleConn.Dispose()

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

End Class
