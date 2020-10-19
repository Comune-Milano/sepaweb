Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Partial Class ASS_Doc_Offerta_Alloggio_Doc_AutocertAbbinam
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

                tipo.Value = Request.QueryString("T")
                idDomanda.Value = Request.QueryString("IDDOM")
                idOfferta.Value = Request.QueryString("IDOFF")
                'Provenienza.Value = Request.QueryString("PROV")
                caricaDati()




            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Sub


    Private Sub caricaDati()

        Try
            '*****************APERTURA CONNESSIONE***************




            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If



            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Doc_AutocertAbbinam.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()

            sr1.Close()


            contenuto = Replace(contenuto, "$ics1$", "")
            contenuto = Replace(contenuto, "$ics2$", "")







            Select Case tipo.Value

                Case "1" 'DOMANDA

                    par.cmd.CommandText = " SELECT domande_bando.pg, (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS intest, unita_immobiliari.cod_unita_immobiliare, " _
                                        & " REL_PRAT_ALL_CCAA_ERP.motivazione, REL_PRAT_ALL_CCAA_ERP.esito " _
                                        & " FROM domande_bando, comp_nucleo, dichiarazioni, alloggi, siscom_mi.unita_immobiliari, REL_PRAT_ALL_CCAA_ERP " _
                                        & " WHERE dichiarazioni.ID = domande_bando.id_dichiarazione " _
                                        & " AND domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione(+) " _
                                        & " And comp_nucleo.progr = domande_bando.progr_componente " _
                                        & " And alloggi.id_pratica = domande_bando.ID " _
                                        & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                                        & " And REL_PRAT_ALL_CCAA_ERP.id_pratica = domande_bando.id " _
                                        & " And REL_PRAT_ALL_CCAA_ERP.id_alloggio = alloggi.id " _
                                        & " And domande_bando.ID=" & idDomanda.Value & ""









                Case "2"  'CAMBI



                    par.cmd.CommandText = " SELECT domande_bando_cambi.pg, (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS intest, unita_immobiliari.cod_unita_immobiliare, " _
                                         & " REL_PRAT_ALL_CCAA_ERP.motivazione, REL_PRAT_ALL_CCAA_ERP.esito " _
                                         & " FROM domande_bando_cambi, comp_nucleo_cambi, dichiarazioni_cambi, alloggi, siscom_mi.unita_immobiliari, REL_PRAT_ALL_CCAA_ERP " _
                                         & " WHERE dichiarazioni_cambi.ID = domande_bando_cambi.id_dichiarazione " _
                                         & " AND domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione(+) " _
                                         & " And comp_nucleo_cambi.progr = domande_bando_cambi.progr_componente " _
                                         & " And alloggi.id_pratica = domande_bando_cambi.ID " _
                                         & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                                         & " And REL_PRAT_ALL_CCAA_ERP.id_pratica = domande_bando_cambi.id " _
                                         & " And REL_PRAT_ALL_CCAA_ERP.id_alloggio = alloggi.id " _
                                         & " And domande_bando_cambi.ID=" & idDomanda.Value & ""





                Case "3"   'EMERGENZE


                    par.cmd.CommandText = " SELECT domande_bando_vsa.pg, (comp_nucleo_vsa.cognome || ' ' || comp_nucleo_vsa.nome) AS intest, unita_immobiliari.cod_unita_immobiliare, " _
                                        & " REL_PRAT_ALL_CCAA_ERP.motivazione, REL_PRAT_ALL_CCAA_ERP.esito " _
                                        & " FROM domande_bando_vsa, comp_nucleo_vsa, dichiarazioni_vsa, alloggi, siscom_mi.unita_immobiliari, REL_PRAT_ALL_CCAA_ERP " _
                                        & " WHERE dichiarazioni_vsa.ID = domande_bando_vsa.id_dichiarazione " _
                                        & " AND domande_bando_vsa.id_dichiarazione = comp_nucleo_vsa.id_dichiarazione(+) " _
                                        & " And comp_nucleo_vsa.progr = domande_bando_vsa.progr_componente " _
                                        & " And alloggi.id_pratica = domande_bando_vsa.ID " _
                                        & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                                        & " And REL_PRAT_ALL_CCAA_ERP.id_pratica = domande_bando_vsa.id " _
                                        & " And REL_PRAT_ALL_CCAA_ERP.id_alloggio = alloggi.id " _
                                        & " And domande_bando_vsa.ID=" & idDomanda.Value & ""



            End Select

            Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderJ.Read Then



                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderJ("intest"), "_______________________"))



                If par.IfNull(myReaderJ("esito"), -1) = 0 Then

                    contenuto = Replace(contenuto, "$ics2$", "X")



                End If




                If par.IfNull(myReaderJ("esito"), -1) = 1 Then

                    contenuto = Replace(contenuto, "$ics1$", "X")



                End If

       




            Else


            contenuto = Replace(contenuto, "$richiedente$", "_______________________")



           


            End If
            myReaderJ.Close()


            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))






            Dim PercorsoBarCode As String = par.RicavaBarCode(30, idDomanda.Value)
            contenuto = Replace(contenuto, "$barcode$", "")



            Dim url As String = Server.MapPath("..\..\ALLEGATI\ABBINAMENTI\")
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

            Dim nomefile As String = "A3_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\FileTemp\"))



            'Response.Write("<script>window.open('../FileTemp/" & nomefile & "','Modulo','');</script>")
            Response.Redirect("..\..\ALLEGATI\ABBINAMENTI\" & nomefile, False)




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







End Class
