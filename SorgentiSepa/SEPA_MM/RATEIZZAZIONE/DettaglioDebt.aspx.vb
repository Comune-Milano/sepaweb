
Partial Class RATEIZZAZIONE_DettaglioDebt
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then

            Cerca()
        End If
    End Sub
    Private Sub Cerca()
        Try


            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand

            par.cmd.CommandText = "SELECT (CASE WHEN RAGIONE_SOCIALE IS NULL THEN COGNOME ||' '|| NOME ELSE RAGIONE_SOCIALE END) AS INTE FROM siscom_mi.ANAGRAFICA,siscom_mi.SOGGETTI_CONTRATTUALI " _
                                & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.id_anagrafica AND SOGGETTI_CONTRATTUALI.id_contratto =" & Request.QueryString("IDCONT") & " AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                lblTitolo.Text = "ELENCO DELLE BOLLETTE RATEIZZATE SUL R.U. INTESTATO A " & par.IfNull(lettore("INTE"), "")
            End If
            lettore.Close()
            Dim totBolletta As Decimal
            Dim totContabile As Decimal



            par.cmd.CommandText = "SELECT num_bolletta, TO_CHAR (TO_DATE (data_emissione, 'yyyymmdd'), 'dd/MM/yyyy') AS data_emissione,TO_CHAR (TO_DATE (data_scadenza, 'yyyymmdd'), 'dd/MM/yyyy') AS data_scadenza, " _
                                & "TO_CHAR (TO_DATE (riferimento_da, 'yyyymmdd'), 'dd/MM/yyyy') AS riferimento_da,TO_CHAR (TO_DATE (riferimento_a, 'yyyymmdd'), 'dd/MM/yyyy') AS riferimento_a, " _
                                & "trim(TO_CHAR(importo_totale,'9G999G999G999G999G990D99'))AS importo_totale,trim(TO_CHAR((nvl(importo_riclassificato,0)),'9G999G999G999G999G990D99'))AS importo_ric " _
                                & " FROM siscom_mi.bol_bollette " _
                                & "WHERE id_rateizzazione = " & Request.QueryString("ID") & " ORDER BY data_emissione ASC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Dim newR As Data.DataRow
            For Each r As Data.DataRow In dt.Rows
                totBolletta = totBolletta + par.IfNull(r.Item("IMPORTO_TOTALE"), 0)
                totContabile = totContabile + par.IfNull(r.Item("IMPORTO_RIC"), 0)
            Next

            newR = dt.NewRow
            newR.Item("IMPORTO_TOTALE") = totBolletta
            newR.Item("IMPORTO_RIC") = totContabile
            newR.Item("RIFERIMENTO_A") = "T.O.T."

            dt.Rows.Add(newR)
            DataGrid.DataSource = dt
            DataGrid.DataBind()
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub
End Class
