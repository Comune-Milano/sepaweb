Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Partial Class Contratti_Scadenza_Documenti_AssegnTemp_letteraTempAssegn
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try


            If Not IsPostBack Then


                dataRif.Value = Request.QueryString("DATA")
                idComune.Value = Request.QueryString("COM")
                caricaDT()




            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Sub


    Private Sub caricaDT()

        Try
            '*****************APERTURA CONNESSIONE***************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If



            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("letteraTempAssegn.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()

            Dim contenutoORIG As String = contenuto
            sr1.Close()

            Dim tblGenerale As String = ""




            par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome)) END AS INTESTATARIO, " _
                               & " CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE cod_fiscale END AS CFIVA, " _
                               & " rapporti_utenza.cod_tipologia_contr_loc, rapporti_utenza.ID as idContr, rapporti_utenza.cod_contratto, " _
                               & " TO_CHAR (TO_DATE (rapporti_utenza.data_decorrenza, 'yyyymmdd'), 'dd/mm/yyyy') AS data_delibera, " _
                               & " TO_CHAR (TO_DATE (rapporti_utenza.data_decorrenza, 'yyyymmdd'),'dd/mm/yyyy') AS data_decorrenza, comuni_nazioni.nome as prov_comune,  comuni_nazioni.sigla as sigla, " _
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
                               & " and comuni_nazioni.id in (" & idComune.Value & ")" _
                               & " AND (  (TO_DATE (rapporti_utenza.data_scadenza_rinnovo, 'yyyymmdd')) - TO_DATE (" & dataRif.Value & ", 'yyyymmdd') <= 90) " _
                               & " AND siscom_mi.getstatocontratto (rapporti_utenza.ID) <> 'CHIUSO' " _
                               & " and rapporti_utenza.fl_assegn_temp = 1 "

            par.cmd.CommandText = par.cmd.CommandText & " ORDER BY CAST (rapporti_utenza.data_scadenza_rinnovo AS int) ASC"



            ' End If

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da.Fill(dt)
            da.Dispose()


            For Each riga As Data.DataRow In dt.Rows

                idContr.Value = par.IfNull(riga.Item("idContr"), "")

                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(riga.Item("INTESTATARIO"), ""))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(riga.Item("INDIRIZZO"), ""))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(riga.Item("SCALA"), ""))
                contenuto = Replace(contenuto, "$interno$", par.IfNull(riga.Item("INTERNO"), ""))
                contenuto = Replace(contenuto, "$cap$", par.IfNull(riga.Item("CAP"), ""))
                contenuto = Replace(contenuto, "$localita$", par.IfNull(riga.Item("PROV_COMUNE"), ""))
                contenuto = Replace(contenuto, "$sigla$", par.IfNull(riga.Item("SIGLA"), ""))
                contenuto = Replace(contenuto, "$dataScadenza$", par.FormattaData(dataRif.Value))



                Dim Responsabile As String = ""


                par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale where unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & idContr.Value & " and indirizzi.id=tab_filiali.id_indirizzo and unita_immobiliari.id=unita_contrattuale.id_unita and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
                    contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
                    contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
                    contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))

                    Responsabile = par.IfNull(myReader("RESPONSABILE"), "")
                    contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
                    contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
                    contenuto = Replace(contenuto, "$responsabile$", Responsabile)

                Else
                    par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_contrattuale,siscom_mi.filiali_virtuali where filiali_virtuali.id_contratto=unita_contrattuale.id_contratto and unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & idContr.Value & " and indirizzi.id=tab_filiali.id_indirizzo and tab_filiali.id=filiali_virtuali.id_filiale"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader2.Read Then
                        contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReader2("NOME"), ""))
                        contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReader2("DESCR"), "") & " " & par.IfNull(myReader2("CIVICO"), ""))
                        contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReader2("CAP"), ""))
                        contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReader2("LOCALITA"), ""))

                        Responsabile = par.IfNull(myReader("RESPONSABILE"), "")

                        contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
                        contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
                        contenuto = Replace(contenuto, "$responsabile$", Responsabile)
                        
                    End If
                    myReader2.Close()
                End If
                myReader.Close()

                contenuto = Replace(contenuto, "$referente$", Session.Item("NOME_OPERATORE"))

                'Dim PercorsoBarCode As String = par.RicavaBarCode(7, idContr.Value)
                'contenuto = Replace(contenuto, "$barcode$", Server.MapPath("../../..\FileTemp\") & PercorsoBarCode)



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
