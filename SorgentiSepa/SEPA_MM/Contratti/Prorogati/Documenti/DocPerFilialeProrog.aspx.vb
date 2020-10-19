Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Partial Class Contratti_Prorogati_Documenti_DocPerFilialeProrog
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable
    Dim dt2 As New Data.DataTable
    Dim vDataDal As String = ""
    Dim vDataAl As String = ""
    Dim vComune As String = ""
    Dim vFiliale As String = ""




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then


                vDataDal = Request.QueryString("DATADAL")
                vDataAl = Request.QueryString("DATAAL")
                vComune = Request.QueryString("COM")
                vFiliale = Request.QueryString("FIL")
                caricaDT()




            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub


    Private Sub caricaDT()

        Try
            '*****************APERTURA CONNESSIONE***************



            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Dim StrSQL As String = ""
            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("DocPerFilialeProrog.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()


            sr1.Close()



            If vDataAl <> "" And vDataDal <> "" Then


                StrSQL = " AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')<= TO_DATE (" & vDataAl & ", 'yyyymmdd') AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')>= TO_DATE (" & vDataDal & ", 'yyyymmdd') "

            ElseIf vDataDal = "" And vDataAl <> "" Then


                StrSQL = " AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')<= TO_DATE (" & vDataAl & ", 'yyyymmdd') "

            ElseIf vDataAl = "" And vDataDal <> "" Then


                StrSQL = "AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')>= TO_DATE (" & vDataDal & ", 'yyyymmdd') "

            End If

            If vFiliale <> "T" Then

                StrSQL = StrSQL & " AND TAB_FILIALI.ID=" & vFiliale



                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID=" & vFiliale

                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettore.Read Then
                    '

                    contenuto = Replace(contenuto, "$acronimoFil$", par.IfNull(lettore("ACRONIMO"), ""))
                    contenuto = Replace(contenuto, "$nomeFiliale$", par.IfNull(lettore("NOME"), ""))

                End If
                lettore.Close()







                Dim tabella1 As String = "  <table style='border: thin solid #000000; width: 100%; border-collapse: collapse;' CellPadding='2'>"
                tabella1 = tabella1 & "<tr><td align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000; font-size:9px;'>&nbspINTESTATARIO </td>"
                tabella1 = tabella1 & " <td align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000;font-size:9px;'>TIPO &nbsp&nbsp</td>"
                tabella1 = tabella1 & " <td  align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000; font-size:9px;'>COD. CONTR. </td>"
                tabella1 = tabella1 & " <td align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000; font-size:9px;'>DATA SC. RINNOVO </td>"
                tabella1 = tabella1 & " <td align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000; font-size:9px;'>DATA PROROGA </td>"









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
                                   & " and comuni_nazioni.id in (" & vComune & ") " _
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

                '  contenuto = Replace(contenuto, "$dataInserim$", par.FormattaData(dataRif.Value))
                contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))

                For Each riga As Data.DataRow In dt.Rows


                    tabella1 = tabella1 & "<tr><td  align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid; font-size:8px;'>" & par.IfNull(riga.Item("INTESTATARIO"), "") & "</td><td  align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;font-size:8px;'>" & par.IfNull(riga.Item("COD_TIPOLOGIA_CONTR_LOC"), "") & "</td><td   align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;font-size:8px;'>" & par.IfNull(riga.Item("COD_CONTRATTO"), "") & "</td>"
                    tabella1 = tabella1 & "<td  align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;font-size:8px;'>" & par.IfNull(riga.Item("DATA_SCADENZA_RINNOVO"), "") & "</td>"
                    tabella1 = tabella1 & "<td  align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;font-size:8px;'>" & par.IfNull(riga.Item("DATA_PROROGA"), "") & "</td></tr>"




                Next


                tabella1 = tabella1 & "</table>"

                contenuto = Replace(contenuto, "$tabellaProrogati$", tabella1)



                Dim url As String = Server.MapPath("../../..\FileTemp\")
                Dim pdfConverter1 As PdfConverter = New PdfConverter

                Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
                If Licenza <> "" Then
                    pdfConverter1.LicenseKey = Licenza
                End If

                pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                pdfConverter1.PdfDocumentOptions.ShowFooter = False
                pdfConverter1.PdfDocumentOptions.LeftMargin = 10
                pdfConverter1.PdfDocumentOptions.RightMargin = 10
                pdfConverter1.PdfDocumentOptions.TopMargin = 10
                pdfConverter1.PdfDocumentOptions.BottomMargin = 10
                pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                pdfConverter1.PdfFooterOptions.FooterText = ("")
                pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                pdfConverter1.PdfFooterOptions.PageNumberText = ""
                pdfConverter1.PdfFooterOptions.ShowPageNumber = False

                Dim nomefile As String = "Export_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("../../..\NuoveImm\"))
                ' pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(tblGenerale, url & nomefile & ".pdf", Server.MapPath("../../..\FileTemp\"))

                Response.Redirect("../../..\FileTemp\" & nomefile, False)




            Else


                Dim contenutoORIG As String = contenuto
                Dim tblGenerale As String = ""



                par.cmd.CommandText = "SELECT tab_filiali.id as id, tab_filiali.nome as nome_filiale, tab_filiali.acronimo as acronimo  " _
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
                                  & " and comuni_nazioni.id in (" & vComune & ") " _
                                  & " and complessi_immobiliari.id_filiale = tab_filiali.ID(+)" _
                                  & " and edifici.id_complesso = complessi_immobiliari.ID" _
                                  & " AND edifici.ID = unita_immobiliari.id_edificio " _
                                  & " " & StrSQL & " " _
                                  & " AND siscom_mi.getstatocontratto (rapporti_utenza.ID) <> 'CHIUSO' " _
                                  & " AND rapporti_utenza.fl_proroga = 1"



                par.cmd.CommandText = par.cmd.CommandText & " ORDER BY nome_filiale ASC"




                Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da.Fill(dt2)




                For Each riga As Data.DataRow In dt2.Rows



                    vFiliale = par.IfNull(riga.Item("id"), "")



                    If vDataAl <> "" And vDataDal <> "" Then


                        StrSQL = " AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')<= TO_DATE (" & vDataAl & ", 'yyyymmdd') AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')>= TO_DATE (" & vDataDal & ", 'yyyymmdd') "

                    ElseIf vDataDal = "" And vDataAl <> "" Then


                        StrSQL = " AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')<= TO_DATE (" & vDataAl & ", 'yyyymmdd') "

                    ElseIf vDataAl = "" And vDataDal <> "" Then


                        StrSQL = "AND TO_DATE (rapporti_utenza.data_proroga, 'yyyymmdd')>= TO_DATE (" & vDataDal & ", 'yyyymmdd') "

                    End If

                    If vFiliale <> "" Then

                        StrSQL = StrSQL & " AND TAB_FILIALI.ID=" & vFiliale



                        contenuto = Replace(contenuto, "$acronimoFil$", par.IfNull(riga.Item("acronimo"), ""))
                        contenuto = Replace(contenuto, "$nomeFiliale$", par.IfNull(riga.Item("nome_filiale"), ""))
                        contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))

                    End If






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
                                 & " and comuni_nazioni.id in (" & vComune & ") " _
                                 & " and complessi_immobiliari.id_filiale = tab_filiali.ID(+)" _
                                 & " and edifici.id_complesso = complessi_immobiliari.ID" _
                                 & " AND edifici.ID = unita_immobiliari.id_edificio " _
                                 & " " & StrSQL & " " _
                                 & " AND siscom_mi.getstatocontratto (rapporti_utenza.ID) <> 'CHIUSO' " _
                                 & " AND rapporti_utenza.fl_proroga = 1"



                    par.cmd.CommandText = par.cmd.CommandText & " ORDER BY CAST (rapporti_utenza.data_scadenza_rinnovo AS int) ASC"


                    dt = New Data.DataTable
                    Dim da2 As Oracle.DataAccess.Client.OracleDataAdapter
                    da2 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                    da2.Fill(dt)



                    Dim tabella2 As String = "  <table style='border: thin solid #000000; width: 100%; border-collapse: collapse;' CellPadding='2'>"
                    tabella2 = tabella2 & "<tr><td align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000; font-size:9px;'>&nbspINTESTATARIO </td>"
                    tabella2 = tabella2 & " <td align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000;font-size:9px;'>TIPO &nbsp&nbsp</td>"
                    tabella2 = tabella2 & " <td  align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000; font-size:9px;'>COD. CONTR. </td>"
                    tabella2 = tabella2 & " <td align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000; font-size:9px;'>DATA SC. RINNOVO </td>"
                    tabella2 = tabella2 & " <td align='center' style='border-bottom-style: solid; border-bottom-width: thin;border-bottom-color: #000000; font-size:9px;'>DATA PROROGA </td>"



                    For Each row As Data.DataRow In dt.Rows








                        tabella2 = tabella2 & "<tr><td  align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid; font-size:8px;'>" & par.IfNull(row.Item("INTESTATARIO"), "") & "</td><td  align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;font-size:8px;'>" & par.IfNull(row.Item("COD_TIPOLOGIA_CONTR_LOC"), "") & "</td><td   align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;font-size:8px;'>" & par.IfNull(row.Item("COD_CONTRATTO"), "") & "</td>"
                        tabella2 = tabella2 & "<td  align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;font-size:8px;'>" & par.IfNull(row.Item("DATA_SCADENZA_RINNOVO"), "") & "</td>"
                        tabella2 = tabella2 & "<td  align='left' style='border-width: thin; border-color: #000000; border-style: none solid solid solid;font-size:8px;'>" & par.IfNull(row.Item("DATA_PROROGA"), "") & "</td></tr>"




                    Next


                    tabella2 = tabella2 & "</table>"

                    contenuto = Replace(contenuto, "$tabellaProrogati$", tabella2)


                    tblGenerale = tblGenerale & contenuto
                    contenuto = contenutoORIG


                Next








                Dim url As String = Server.MapPath("../../..\FileTemp\")
                Dim pdfConverter1 As PdfConverter = New PdfConverter

                Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
                If Licenza <> "" Then
                    pdfConverter1.LicenseKey = Licenza
                End If

                pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                pdfConverter1.PdfDocumentOptions.ShowFooter = False
                pdfConverter1.PdfDocumentOptions.LeftMargin = 10
                pdfConverter1.PdfDocumentOptions.RightMargin = 10
                pdfConverter1.PdfDocumentOptions.TopMargin = 10
                pdfConverter1.PdfDocumentOptions.BottomMargin = 10
                pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                pdfConverter1.PdfFooterOptions.FooterText = ("")
                pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                pdfConverter1.PdfFooterOptions.PageNumberText = ""
                pdfConverter1.PdfFooterOptions.ShowPageNumber = False

                Dim nomefile As String = "Export_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(tblGenerale, url & nomefile, Server.MapPath("../../..\NuoveImm\"))
                ' pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(tblGenerale, url & nomefile & ".pdf", Server.MapPath("../../..\FileTemp\"))

                Response.Redirect("../../..\FileTemp\" & nomefile, False)





















            End If








           












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
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try




    End Sub





End Class