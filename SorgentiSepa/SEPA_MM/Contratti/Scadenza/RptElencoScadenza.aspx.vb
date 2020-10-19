Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums


Partial Class Contratti_Scadenza_RptElencoScadenza
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        ' Dim DataCompleta As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then
            Response.Flush()

            cap.Value = Request.QueryString("COM")
            dataRif.Value = Request.QueryString("DATA")
            DataCompleta.value = par.FormattaData(dataRif.Value)
            Cerca()

        End If
    End Sub
    Private Sub Cerca()
  Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            ' dataRif.Value = par.FormattaData(dataRif.Value)


            'If cap.Value = "T" Then



            '    par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome)) END AS INTESTATARIO, " _
            '                        & " CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE cod_fiscale END AS CFIVA, " _
            '                        & " rapporti_utenza.cod_tipologia_contr_loc, rapporti_utenza.ID, rapporti_utenza.cod_contratto, " _
            '                        & " TO_CHAR (TO_DATE (rapporti_utenza.data_decorrenza, 'yyyymmdd'), 'dd/mm/yyyy') AS data_delibera, " _
            '                        & " TO_CHAR (TO_DATE (rapporti_utenza.data_decorrenza, 'yyyymmdd'),'dd/mm/yyyy') AS data_decorrenza, comuni_nazioni.nome as prov_comune, " _
            '                        & " TO_CHAR (TO_DATE (rapporti_utenza.data_scadenza_rinnovo, 'yyyymmdd'),'dd/mm/yyyy') AS data_scadenza_rinnovo, " _
            '                         & " CASE WHEN TO_DATE (rapporti_utenza.data_scadenza_rinnovo, 'yyyymmdd')>= TO_DATE (" & dataRif.Value & ", 'yyyymmdd') THEN TO_CHAR (((TO_DATE (rapporti_utenza.data_scadenza_rinnovo, 'yyyymmdd')) - TO_DATE (" & dataRif.Value & ", 'yyyymmdd'))) ELSE 'SCADUTO' END AS GG_FINE_DEC, " _
            '                        & " unita_contrattuale.scala AS scala, unita_contrattuale.interno, " _
            '                        & " (unita_contrattuale.indirizzo || ' ' || unita_contrattuale.civico) AS indirizzo, " _
            '                        & " unita_contrattuale.cap AS cap, unita_contrattuale.localita AS prov_comune, " _
            '                        & " tipo_livello_piano.descrizione, unita_immobiliari.cod_unita_immobiliare " _
            '                        & " FROM siscom_mi.rapporti_utenza, siscom_mi.anagrafica, siscom_mi.soggetti_contrattuali, siscom_mi.unita_immobiliari, " _
            '                        & " siscom_mi.tipo_livello_piano, siscom_mi.unita_contrattuale, siscom_mi.rapporti_utenza_controllo, sepa.comuni_nazioni " _
            '                        & " WHERE soggetti_contrattuali.id_contratto = rapporti_utenza.ID " _
            '                        & " And soggetti_contrattuali.id_anagrafica = anagrafica.ID " _
            '                        & " And rapporti_utenza.ID = unita_contrattuale.id_contratto " _
            '                        & " And unita_contrattuale.id_unita = unita_immobiliari.ID " _
            '                        & " And unita_immobiliari.cod_tipo_livello_piano = tipo_livello_piano.cod " _
            '                        & " AND rapporti_utenza.ID = rapporti_utenza_controllo.id_contratto(+) " _
            '                        & " and comuni_nazioni.cod = unita_contrattuale.cod_comune " _
            '                        & " AND (  (TO_DATE (rapporti_utenza.data_scadenza_rinnovo, 'yyyymmdd')) - TO_DATE (" & dataRif.Value & ", 'yyyymmdd') <= 90) " _
            '                        & " AND siscom_mi.getstatocontratto (rapporti_utenza.ID) <> 'CHIUSO' " _
            '                     

            '    ' and rapporti_utenza.fl_assegn_temp = 1 (da sostituire a durata_anni e durata_rinnovo)


            '    par.cmd.CommandText = par.cmd.CommandText & " ORDER BY CAST (rapporti_utenza.data_scadenza_rinnovo AS int) ASC"


            'Else

            par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome)) END AS INTESTATARIO, " _
                               & " CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE cod_fiscale END AS CFIVA, " _
                               & " rapporti_utenza.cod_tipologia_contr_loc, rapporti_utenza.ID, rapporti_utenza.cod_contratto, " _
                               & " TO_CHAR (TO_DATE (rapporti_utenza.data_decorrenza, 'yyyymmdd'), 'dd/mm/yyyy') AS data_delibera, " _
                               & " TO_CHAR (TO_DATE (rapporti_utenza.data_decorrenza, 'yyyymmdd'),'dd/mm/yyyy') AS data_decorrenza, comuni_nazioni.nome as prov_comune, " _
                               & " TO_CHAR (TO_DATE (rapporti_utenza.data_scadenza_rinnovo, 'yyyymmdd'),'dd/mm/yyyy') AS data_scadenza_rinnovo, " _
                               & " CASE WHEN TO_DATE (rapporti_utenza.data_scadenza_rinnovo, 'yyyymmdd')>= TO_DATE (" & dataRif.Value & ", 'yyyymmdd') THEN TO_CHAR (((TO_DATE (rapporti_utenza.data_scadenza_rinnovo, 'yyyymmdd')) - TO_DATE (" & dataRif.Value & ", 'yyyymmdd'))) ELSE 'SCADUTO' END AS GG_FINE_DEC, " _
                               & " unita_contrattuale.scala AS scala, unita_contrattuale.interno, " _
                               & " (unita_contrattuale.indirizzo || ' ' || unita_contrattuale.civico) AS indirizzo, " _
                               & " unita_contrattuale.cap AS cap,  " _
                               & " tipo_livello_piano.descrizione, unita_immobiliari.cod_unita_immobiliare " _
                               & " FROM siscom_mi.rapporti_utenza, siscom_mi.anagrafica, siscom_mi.soggetti_contrattuali, siscom_mi.unita_immobiliari, " _
                               & " siscom_mi.tipo_livello_piano, siscom_mi.unita_contrattuale, siscom_mi.rapporti_utenza_controllo, sepa.comuni_nazioni " _
                               & " WHERE soggetti_contrattuali.id_contratto = rapporti_utenza.ID " _
                               & " And soggetti_contrattuali.id_anagrafica = anagrafica.ID " _
                               & " AND unita_immobiliari.id_unita_principale is null " _
                               & " and soggetti_contrattuali.cod_tipologia_occupante= 'INTE' " _
                               & " And rapporti_utenza.ID = unita_contrattuale.id_contratto " _
                               & " And unita_contrattuale.id_unita = unita_immobiliari.ID " _
                               & " And unita_immobiliari.cod_tipo_livello_piano = tipo_livello_piano.cod " _
                               & " AND rapporti_utenza.ID = rapporti_utenza_controllo.id_contratto(+) " _
                               & " and comuni_nazioni.cod = unita_contrattuale.cod_comune " _
                               & " and comuni_nazioni.id in (" & cap.Value & ")" _
                               & " AND (  (TO_DATE (rapporti_utenza.data_scadenza_rinnovo, 'yyyymmdd')) - TO_DATE (" & dataRif.Value & ", 'yyyymmdd') <= 90) " _
                               & " AND siscom_mi.getstatocontratto (rapporti_utenza.ID) <> 'CHIUSO' " _
                               & " and rapporti_utenza.fl_assegn_temp = 1 "


            par.cmd.CommandText = par.cmd.CommandText & " ORDER BY CAST (rapporti_utenza.data_scadenza_rinnovo AS int) ASC"



            ' End If

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da.Fill(dt)
            dgvRptScadenza.DataSource = dt
            dgvRptScadenza.DataBind()
            lbl_risultati.Text = "Totale Risultati Trovati: " & dt.Rows.Count















            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If




        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub
    Public Property Query() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set

    End Property
    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Query, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "RAPPORTI_UTENZA, ANAGRAFICA,INDIRIZZI")
        dgvRptScadenza.DataSource = ds
        dgvRptScadenza.DataBind()
        '  LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

 


    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvRptScadenza.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Contratto COD. " & e.Item.Cells(1).Text & "';document.getElementById('IdContratto').value='" & e.Item.Cells(0).Text & "';document.getElementById('CodContratto').value='" & e.Item.Cells(1).Text & "';")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Contratto COD. " & e.Item.Cells(1).Text & "';document.getElementById('IdContratto').value='" & e.Item.Cells(0).Text & "';document.getElementById('CodContratto').value='" & e.Item.Cells(1).Text & "';")

    '    End If
    'End Sub

    'Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvRptScadenza.PageIndexChanged
    '    If e.NewPageIndex >= 0 Then
    '        'Label3.Text = "0"
    '        dgvRptScadenza.CurrentPageIndex = e.NewPageIndex
    '        BindGrid()
    '    End If
    'End Sub

   

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click

        Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(Me.dgvRptScadenza, "ExportRptContrattiScadenza", , , , False)
        If File.Exists(Server.MapPath("../..\FileTemp\") & nomefile) Then
            Response.Redirect("../..\/FileTemp\/" & nomefile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If

    End Sub

    


    Protected Sub TStampe_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles TStampe.MenuItemClick
        Select Case TStampe.SelectedValue

            Case 1
                Response.Write("<script>window.open('Documenti_AssegnTemp/letteraTempComune.aspx?DATA=" & dataRif.Value & "&COM=" & cap.Value & "', 'LetteraTempComune', 'resizable=yes');</script>")
            Case 2
                Response.Write("<script>window.open('Documenti_AssegnTemp/letteraTempAssegn.aspx?DATA=" & dataRif.Value & "&COM=" & cap.Value & "', 'LetteraTempAssegn', 'resizable=yes');</script>")
            Case 3
                Response.Write("<script>window.open('Documenti_AssegnTemp/NotaXUffFiscaleScad.aspx?DATA=" & dataRif.Value & "&COM=" & cap.Value & "', 'NotaXUffFiscale', 'resizable=yes');</script>")
               
        End Select

    End Sub
End Class
