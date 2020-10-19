Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class Contratti_Prorogati_RptElencoProrogati
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

            idComune.Value = Request.QueryString("COM")
            idFiliale.Value = Request.QueryString("FIL")
            dataDal.Value = Request.QueryString("DATADAL")
            dataAl.Value = Request.QueryString("DATAAL")
            ' DataCompleta.value = par.FormattaData(dataRif.Value)
            Cerca()

        End If
    End Sub
    Private Sub Cerca()
        Try
            Dim StrSQL As String = ""
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If dataAl.Value <> "" And dataDal.Value <> "" Then

                lblTitle.Text = "REPORT CONTRATTI PROROGATI (DAL " & par.FormattaData(dataDal.Value) & " AL " & par.FormattaData(dataAl.Value) & ")"
                StrSQL = " AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')<= TO_DATE (" & dataAl.Value & ", 'yyyymmdd') AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')>= TO_DATE (" & dataDal.Value & ", 'yyyymmdd') "

            ElseIf dataDal.Value = "" And dataAl.Value <> "" Then

                lblTitle.Text = "REPORT CONTRATTI PROROGATI (FINO AL " & par.FormattaData(dataAl.Value) & ")"
                StrSQL = " AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')<= TO_DATE (" & dataAl.Value & ", 'yyyymmdd') "

            ElseIf dataAl.Value = "" And dataDal.Value <> "" Then

                lblTitle.Text = "REPORT CONTRATTI PROROGATI (DAL " & par.FormattaData(dataDal.Value) & ")"
                StrSQL = "AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')>= TO_DATE (" & dataDal.Value & ", 'yyyymmdd') "

            End If

            If idFiliale.Value <> "" Then

                StrSQL = StrSQL & " AND TAB_FILIALI.ID=" & idFiliale.Value

            Else

                idFiliale.Value = "T"

            End If







            'par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome)) END AS INTESTATARIO, " _
            '                   & " CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE cod_fiscale END AS CFIVA, " _
            '                   & " rapporti_utenza.cod_tipologia_contr_loc, rapporti_utenza.ID, rapporti_utenza.cod_contratto, " _
            '                   & " TO_CHAR (TO_DATE (rapporti_utenza.data_decorrenza, 'yyyymmdd'), 'dd/mm/yyyy') AS data_delibera, " _
            '                   & " TO_CHAR (TO_DATE (rapporti_utenza.data_decorrenza, 'yyyymmdd'),'dd/mm/yyyy') AS data_decorrenza, comuni_nazioni.nome as prov_comune, " _
            '                   & " TO_CHAR (TO_DATE (rapporti_utenza.data_scadenza_rinnovo, 'yyyymmdd'),'dd/mm/yyyy') AS data_scadenza_rinnovo, " _
            '                    & " TO_CHAR (TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd'),'dd/mm/yyyy') AS data_proroga, " _
            '                   & " unita_contrattuale.scala AS scala, unita_contrattuale.interno, " _
            '                   & " (unita_contrattuale.indirizzo || ' ' || unita_contrattuale.civico) AS indirizzo, " _
            '                   & " unita_contrattuale.cap AS cap,  " _
            '                   & " tipo_livello_piano.descrizione, unita_immobiliari.cod_unita_immobiliare " _
            '                   & " FROM siscom_mi.rapporti_utenza, siscom_mi.anagrafica, siscom_mi.soggetti_contrattuali, siscom_mi.unita_immobiliari, " _
            '                   & " siscom_mi.tipo_livello_piano, siscom_mi.unita_contrattuale, siscom_mi.rapporti_utenza_controllo, sepa.comuni_nazioni " _
            '                   & " WHERE soggetti_contrattuali.id_contratto = rapporti_utenza.ID " _
            '                   & " And soggetti_contrattuali.id_anagrafica = anagrafica.ID " _
            '                   & " AND unita_immobiliari.id_unita_principale is null " _
            '                   & " and soggetti_contrattuali.cod_tipologia_occupante= 'INTE' " _
            '                   & " And rapporti_utenza.ID = unita_contrattuale.id_contratto " _
            '                   & " And unita_contrattuale.id_unita = unita_immobiliari.ID " _
            '                   & " And unita_immobiliari.cod_tipo_livello_piano = tipo_livello_piano.cod " _
            '                   & " AND rapporti_utenza.ID = rapporti_utenza_controllo.id_contratto(+) " _
            '                   & " and comuni_nazioni.cod = unita_contrattuale.cod_comune " _
            '                   & " and comuni_nazioni.id in (" & idComune.Value & ")" _
            '                   & " AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')>= TO_DATE (" & dataDal.Value & ", 'yyyymmdd') " _
            '                   & " AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')<= TO_DATE (" & dataAl.Value & ", 'yyyymmdd') " _
            '                   & " AND siscom_mi.getstatocontratto (rapporti_utenza.ID) <> 'CHIUSO' " _
            '                   & " AND rapporti_utenza.fl_proroga = 1"



            'par.cmd.CommandText = par.cmd.CommandText & " ORDER BY CAST (rapporti_utenza.data_scadenza_rinnovo AS int) ASC"






            par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome)) END AS INTESTATARIO, " _
                               & " CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE cod_fiscale END AS CFIVA,  tab_filiali.nome as nome_filiale, " _
                               & " rapporti_utenza.cod_tipologia_contr_loc, rapporti_utenza.ID, rapporti_utenza.cod_contratto, " _
                               & " TO_CHAR (TO_DATE (rapporti_utenza.data_decorrenza, 'yyyymmdd'), 'dd/mm/yyyy') AS data_delibera, " _
                               & " TO_CHAR (TO_DATE (rapporti_utenza.data_decorrenza, 'yyyymmdd'),'dd/mm/yyyy') AS data_decorrenza, comuni_nazioni.nome as prov_comune, " _
                               & " TO_CHAR (TO_DATE (rapporti_utenza.data_scadenza_rinnovo, 'yyyymmdd'),'dd/mm/yyyy') AS data_scadenza_rinnovo, " _
                                & " TO_CHAR (TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd'),'dd/mm/yyyy') AS data_proroga, " _
                               & " unita_contrattuale.scala AS scala, unita_contrattuale.interno, " _
                               & " (unita_contrattuale.indirizzo || ' ' || unita_contrattuale.civico) AS indirizzo, " _
                               & " unita_contrattuale.cap AS cap,  " _
                               & " tipo_livello_piano.descrizione, unita_immobiliari.cod_unita_immobiliare " _
                               & " FROM siscom_mi.rapporti_utenza, siscom_mi.anagrafica, siscom_mi.soggetti_contrattuali, siscom_mi.unita_immobiliari, " _
                               & " siscom_mi.tipo_livello_piano, siscom_mi.unita_contrattuale, siscom_mi.rapporti_utenza_controllo, sepa.comuni_nazioni, " _
                               & " siscom_mi.tab_filiali, siscom_mi.complessi_immobiliari, siscom_mi.edifici " _
                               & " WHERE soggetti_contrattuali.id_contratto = rapporti_utenza.ID " _
                               & " And soggetti_contrattuali.id_anagrafica = anagrafica.ID " _
                               & " AND unita_immobiliari.id_unita_principale is null " _
                               & " and soggetti_contrattuali.cod_tipologia_occupante= 'INTE' " _
                               & " And rapporti_utenza.ID = unita_contrattuale.id_contratto " _
                               & " And unita_contrattuale.id_unita = unita_immobiliari.ID " _
                               & " And unita_immobiliari.cod_tipo_livello_piano = tipo_livello_piano.cod " _
                               & " AND rapporti_utenza.ID = rapporti_utenza_controllo.id_contratto(+) " _
                               & " and comuni_nazioni.cod = unita_contrattuale.cod_comune " _
                               & " and comuni_nazioni.id in (" & idComune.Value & ") " _
                               & " and complessi_immobiliari.id_filiale = tab_filiali.ID(+)" _
                               & " and edifici.id_complesso = complessi_immobiliari.ID" _
                               & " AND edifici.ID = unita_immobiliari.id_edificio " _
                               & " " & StrSQL & " " _
                               & " AND siscom_mi.getstatocontratto (rapporti_utenza.ID) <> 'CHIUSO' " _
                               & " AND rapporti_utenza.fl_proroga = 1"



            par.cmd.CommandText = par.cmd.CommandText & " ORDER BY CAST (rapporti_utenza.data_scadenza_rinnovo AS int) ASC"








            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da.Fill(dt)
            dgvRptProrogati.DataSource = dt
            dgvRptProrogati.DataBind()
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
        dgvRptProrogati.DataSource = ds
        dgvRptProrogati.DataBind()
        '  LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub



    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click

        Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(Me.dgvRptProrogati, "ExportRptContrattiProrogati", , , , False)
        If File.Exists(Server.MapPath("../..\FileTemp\") & nomefile) Then
            Response.Redirect("../..\/FileTemp\/" & nomefile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If

    End Sub




    Protected Sub TStampe_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles TStampe.MenuItemClick
        Select Case TStampe.SelectedValue

            Case 1
                Response.Write("<script>window.open('Documenti/NotaXUffFiscaleProrog.aspx?DATADAL=" & dataDal.Value & "&DATAAL=" & dataAl.Value & "&FIL=" & idFiliale.Value & "&COM=" & idComune.Value & "', 'NotaXUffFiscale',  'resizable=yes');</script>")
            Case 2
                Response.Write("<script>window.open('Documenti/DocPerFilialeProrog.aspx?DATADAL=" & dataDal.Value & "&DATAAL=" & dataAl.Value & "&FIL=" & idFiliale.Value & "&COM=" & idComune.Value & "', 'NotaXUffFiscale', 'resizable=yes');</script>")
           

        End Select

    End Sub
End Class

