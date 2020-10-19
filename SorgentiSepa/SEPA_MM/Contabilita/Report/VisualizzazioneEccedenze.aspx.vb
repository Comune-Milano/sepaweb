
Partial Class Contabilita_Report_VisualizzazioneEccedenze
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String = ""
        Str = "<div id=""divPre"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"

        If Not IsPostBack Then
            Response.Write(Str)
            Response.Flush()

            CaricaEccedenze()
        End If
    End Sub

    Private Sub CaricaEccedenze()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim dtEccedenze As New Data.DataTable
            Dim conta As Integer = 0

            par.cmd.CommandText = "SELECT DISTINCT (bol_bollette_gest.ID) AS id_boll,TO_CHAR (ABS (bol_bollette_gest.importo_totale),'9G999G990D99') AS imp_emesso," _
                    & " bol_bollette_gest.importo_totale AS importotot,rapporti_utenza.ID AS ID, cod_contratto,tipologia_contratto_locazione.descrizione AS tipo_contr," _
                    & " tipologia_unita_immobiliari.descrizione AS tipo_ui,data_emissione, riferimento_da,TO_CHAR (TO_DATE (data_emissione, 'yyyymmdd'),'dd/mm/yyyy') AS data_emiss," _
                    & " TO_CHAR (TO_DATE (riferimento_da, 'yyyymmdd'),'dd/mm/yyyy') AS data_riferim1," _
                    & " TO_CHAR (TO_DATE (riferimento_a, 'yyyymmdd'),'dd/mm/yyyy') AS data_riferim2,bol_bollette_gest.NOTE," _
                    & " (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome) ) END) AS INTESTATARIO," _
                    & " (CASE WHEN rapporti_utenza.provenienza_ass = 1" _
                    & " AND unita_immobiliari.id_destinazione_uso <> 2" _
                    & " THEN 'ERP Sociale'" _
                    & " WHEN unita_immobiliari.id_destinazione_uso = 2" _
                    & " THEN 'ERP Moderato'" _
                    & " WHEN rapporti_utenza.provenienza_ass = 12" _
                    & " THEN 'CANONE CONVENZ.'" _
                    & " WHEN rapporti_utenza.provenienza_ass = 8" _
                    & " THEN 'ART.22 C.10 RR 1/2004'" _
                    & " WHEN rapporti_utenza.provenienza_ass = 10" _
                    & " THEN 'FORZE DELL''ORDINE'" _
                    & " WHEN rapporti_utenza.dest_uso = 'C'" _
                    & " THEN 'Cooperative'" _
                    & " WHEN rapporti_utenza.dest_uso = 'P'" _
                    & " THEN '431 P.O.R.'" _
                    & " WHEN rapporti_utenza.dest_uso = 'D'" _
                    & " THEN '431/98 ART.15 R.R.1/2004'" _
                    & " WHEN rapporti_utenza.dest_uso = 'V'" _
                    & " THEN '431/98 ART.15 C.2 R.R.1/2004'" _
                    & " WHEN rapporti_utenza.dest_uso = 'S'" _
                    & " THEN '431/98 Speciali'" _
                    & " WHEN rapporti_utenza.dest_uso = '0'" _
                    & " THEN 'Standard'" _
                    & " END) AS tipo_specifico," _
                    & " indirizzi.descrizione || ', '" _
                    & " || indirizzi.civico AS indirizzo," _
                    & " unita_immobiliari.cod_unita_immobiliare AS COD_UI, " _
                    & " (CASE WHEN TIPO_APPLICAZIONE<>'N' THEN 'SI' ELSE 'NO' END) AS UTILIZZATO " _
                    & " FROM siscom_mi.bol_bollette_gest," _
                    & " siscom_mi.rapporti_utenza," _
                    & " siscom_mi.unita_contrattuale," _
                    & " siscom_mi.tipologia_contratto_locazione," _
                    & " siscom_mi.tipologia_unita_immobiliari," _
                    & " siscom_mi.tipo_bollette_gest," _
                    & " siscom_mi.unita_immobiliari," _
                    & " siscom_mi.anagrafica," _
                    & " siscom_mi.soggetti_contrattuali," _
                    & " siscom_mi.indirizzi" _
                    & " WHERE bol_bollette_gest.id_contratto = rapporti_utenza.ID" _
                    & " AND rapporti_utenza.ID = unita_contrattuale.id_contratto" _
                    & " AND unita_contrattuale.id_unita = unita_immobiliari.ID" _
                    & " AND unita_immobiliari.id_unita_principale IS NULL" _
                    & " AND anagrafica.id=soggetti_contrattuali.id_anagrafica" _
                    & " AND rapporti_utenza.ID = soggetti_contrattuali.id_contratto" _
                    & " AND cod_tipologia_occupante='INTE' " _
                    & " AND tipologia_contratto_locazione.cod = rapporti_utenza.cod_tipologia_contr_loc" _
                    & " AND bol_bollette_gest.id_tipo = tipo_bollette_gest.ID" _
                    & " AND TIPO_BOLLETTE_GEST.FL_VISUALIZZABILE=1" _
                    & " AND unita_immobiliari.id_indirizzo = indirizzi.ID(+)" _
                    & " AND unita_contrattuale.tipologia = tipologia_unita_immobiliari.cod" _
                    & " AND bol_bollette_gest.id_tipo = 4" _
                    & " ORDER BY UTILIZZATO ASC,INTESTATARIO ASC, data_emissione DESC, riferimento_da DESC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dtEccedenze)
            DataGridEcced.DataSource = dtEccedenze
            conta = dtEccedenze.Rows.Count
            lblTitolo.Text &= " - Totale: " & conta
            DataGridEcced.DataBind()

            'For Each di As DataGridItem In DataGridEcced.Items
            '    If di.Cells(11).Text.Contains("SI") Then
            '        For j As Integer = 0 To di.Cells.Count - 1
            '            di.Cells(j).BackColor = Drawing.ColorTranslator.FromHtml("#E8FFE8")
            '        Next
            '    End If
            'Next

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

    Protected Sub ImageButtonExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonExcel.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridEcced, "ExportEccedenze", , , , False)
        If System.IO.File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub

    Protected Sub DataGridEcced_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridEcced.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridEcced.CurrentPageIndex = e.NewPageIndex
            CaricaEccedenze()
        End If
    End Sub
End Class
