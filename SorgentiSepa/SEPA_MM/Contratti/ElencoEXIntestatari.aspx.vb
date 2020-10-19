
Partial Class Contratti_ElencoEXIntestatari
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            CercaExIntest()
        End If

    End Sub

    Private Sub CercaExIntest()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Dim Str As String = ""
            Str = "SELECT anagrafica.id,CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (cognome))END AS cognome," _
            & "RTRIM (LTRIM (nome)) AS nome,TO_CHAR (TO_DATE (data_inizio, 'yyyymmdd'),'dd/mm/yyyy') AS data_inizio," _
            & "TO_CHAR (TO_DATE (data_fine, 'yyyymmdd'), 'dd/mm/yyyy') AS data_fine " _
            & ",(SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_OCCUPANTE WHERE COD=COD_TIPOLOGIA_OCCUPANTE) AS TIPO " _
            & "FROM siscom_mi.anagrafica, siscom_mi.soggetti_contrattuali " _
            & "WHERE soggetti_contrattuali.id_contratto IN (SELECT ID FROM siscom_mi.rapporti_utenza WHERE cod_contratto ='" & Request.QueryString("COD") & "') " _
            & "AND (soggetti_contrattuali.cod_tipologia_occupante IN ('EXINTE','EXCOINT') or soggetti_contrattuali.cod_tipologia_occupante IN ('INTE','COINT')) " _
            & "AND anagrafica.ID = soggetti_contrattuali.id_anagrafica " _
            & "ORDER BY soggetti_contrattuali.data_inizio DESC "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

End Class
