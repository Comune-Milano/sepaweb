Imports System.IO

Partial Class Condomini_RisultatiDatiChiusura
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()

            Me.rdbFiltro.SelectedValue = "S"

            CaricaGriglia()

        End If
    End Sub
    Private Sub CaricaGriglia()
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT COND_AVV_SLOGGIO.ID,COND_AVV_SLOGGIO.ID_COND_AVVISO,TO_CHAR(TO_DATE(COND_AVV_SLOGGIO.DATA_INVIO_COM_AMM,'yyyymmdd'),'dd/mm/yyyy') AS DATA_INVIO_AMM, " _
                                & "TRIM (TO_CHAR(COND_AVV_SLOGGIO.IMPORTO_DEBITO,'9G999G999G990D99')) AS DEBITO,TRIM (TO_CHAR(COND_AVV_SLOGGIO.IMPORTO_CREDITO,'9G999G999G990D99')) AS CREDITO, " _
                                & "ID_BOLLETTA,COND_AVVISI.ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO, " _
                                & "CONDOMINI.DENOMINAZIONE AS CONDOMINIO,TO_CHAR(TO_DATE(rapporti_utenza.data_riconsegna,'yyyymmdd'),'dd/mm/yyyy')AS DATA_SLOGGIO, " _
                                & "(CASE WHEN (TO_DATE(SYSDATE) - TO_DATE(DATA_INVIO_COM_AMM,'yyyyMMdd'))>15 AND DATA_RIC_ESTRATTO_C IS NULL THEN 'SI' ELSE 'NO' END) AS scaduta, " _
                                & "(CASE WHEN NON_SOLLECITARE = 0 THEN (CASE WHEN (TO_DATE(SYSDATE) - TO_DATE(DATA_INVIO_COM_AMM,'yyyyMMdd'))>15 AND DATA_RIC_ESTRATTO_C IS NULL THEN 'SI' ELSE 'NO' END) ELSE 'NO' END) AS DA_SOLLECITARE, " _
                                & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTE " _
                                & "FROM SISCOM_MI.COND_AVV_SLOGGIO,SISCOM_MI.COND_AVVISI," _
                                & "SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.CONDOMINI,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA " _
                                & "WHERE COND_AVV_SLOGGIO.ID_COND_AVVISO = COND_AVVISI.ID(+) " _
                                & "AND RAPPORTI_UTENZA.ID = COND_AVVISI.ID_CONTRATTO " _
                                & "AND CONDOMINI.ID = COND_AVVISI.ID_CONDOMINIO " _
                                & "AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                & "AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                & "AND SOGGETTI_CONTRATTUALI.cod_tipologia_occupante = 'INTE'"
            If Me.rdbFiltro.SelectedValue = "S" Then
                par.cmd.CommandText = par.cmd.CommandText & " AND (CASE WHEN NON_SOLLECITARE = 0 THEN (CASE WHEN (TO_DATE(SYSDATE) - TO_DATE(DATA_INVIO_COM_AMM,'yyyyMMdd'))>15 AND DATA_RIC_ESTRATTO_C IS NULL THEN 'SI' ELSE 'NO' END) ELSE 'NO' END) = 'SI' "
            End If
            par.cmd.CommandText = par.cmd.CommandText & " ORDER BY INTE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)


            DataGridChiusi.DataSource = dt
            DataGridChiusi.DataBind()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        End Try

    End Sub

    Protected Sub DataGridChiusi_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridChiusi.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Then

            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor = 'pointer'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'avviso  dell\'inquilino: " & e.Item.Cells(3).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text.Replace("'", "\'") & "';document.getElementById('idAvviso').value='" & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('idContratto').value='" & e.Item.Cells(1).Text.Replace("'", "\'") & "';")

        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Then

            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor = 'pointer'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'avviso  dell\'inquilino: " & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text.Replace("'", "\'") & "';document.getElementById('idAvviso').value='" & e.Item.Cells(0).Text.Replace("'", "\'") & "';document.getElementById('idContratto').value='" & e.Item.Cells(0).Text.Replace("'", "\'") & "';")

        End If

    End Sub

    Protected Sub rdbFiltro_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbFiltro.SelectedIndexChanged
        CaricaGriglia()
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridChiusi, "ExportCondSloggio", True)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If

    End Sub
End Class
