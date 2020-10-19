
Partial Class RATEIZZAZIONE_DettaglioRat
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            'Me.lblTitolo.Text = "Dettaglio della rateizzazione sul contratto intestatao a " & Request.QueryString("IDCONT")
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
                lblTitolo.Text = "DETTAGLIO DELLA RATEIZZAZIONE A CARICO DEL R.U. INTESTATO A " & par.IfNull(lettore("INTE"), "")
            End If
            lettore.Close()


            par.cmd.CommandText = "SELECT DECODE(num_rata,0,'Anticipo',num_rata) AS RATA, " _
                        & "TO_CHAR(TO_DATE(BOL_RATEIZZAZIONI_DETT.data_emissione,'yyyymmdd'),'month yyyy') AS mese, " _
                        & "trim(TO_CHAR(importo_rata,'9G999G999G999G999G990D99')) AS importo_rata,trim(TO_CHAR(quota_capitali,'9G999G999G999G999G990D99')) AS quota_capitali,trim(TO_CHAR(quota_interessi,'9G999G999G999G999G990D99')) AS quota_interessi,BOL_BOLLETTE.num_bolletta,trim(TO_CHAR(BOL_BOLLETTE.importo_totale,'9G999G999G999G999G990D99')) AS importo_bolletta, " _
                        & "TO_CHAR(TO_DATE (BOL_BOLLETTE.data_emissione, 'yyyymmdd'),'dd/mm/yyyy') AS data_emissione, " _
                        & "TO_CHAR(TO_DATE (BOL_BOLLETTE.data_scadenza, 'yyyymmdd'),'dd/mm/yyyy') AS data_scadenza, " _
                        & "(case when ID_BOLLETTA_STORNO is NULL then (CASE WHEN NVL(importo_pagato,0)<>0 THEN 'SI' ELSE 'NO'END) else 'STORNATA'end) AS pagata, bol_rateizzazioni_dett.fl_annullata " _
                        & "FROM siscom_mi.BOL_RATEIZZAZIONI_DETT,siscom_mi.BOL_BOLLETTE " _
                        & "WHERE BOL_BOLLETTE.ID(+) = BOL_RATEIZZAZIONI_DETT.id_bolletta " _
                        & "AND BOL_RATEIZZAZIONI_DETT.ID_RATEIZZAZIONE = " & Request.QueryString("ID") _
                        & " ORDER BY num_rata ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            DataGrid.DataSource = dt
            DataGrid.DataBind()

            For Each di As DataGridItem In DataGrid.Items
                If di.Cells(TrovaIndiceColonna(DataGrid, "FL_ANNULLATA")).Text = 1 Then
                    di.Cells(0).Font.Strikeout = True
                    di.Cells(1).Font.Strikeout = True
                    di.Cells(2).Font.Strikeout = True
                    di.Cells(3).Font.Strikeout = True
                    di.Cells(4).Font.Strikeout = True
                    di.Cells(5).Font.Strikeout = True
                    di.Cells(6).Font.Strikeout = True
                    di.Cells(7).Font.Strikeout = True
                    di.Cells(8).Font.Strikeout = True
                    di.Cells(9).Font.Strikeout = True

                End If
            Next



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
    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer

        TrovaIndiceColonna = -1

        Dim Indice As Integer = 0

        Try

            For Each c As DataGridColumn In dgv.Columns
                If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                    TrovaIndiceColonna = Indice
                    Exit For
                End If
                Indice = Indice + 1
            Next

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return TrovaIndiceColonna

    End Function

End Class
