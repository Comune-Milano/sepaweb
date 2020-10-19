Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Partial Class CENSIMENTO_ModuloPromUtente
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then





            caricaDati()


        End If
    End Sub





    Private Sub caricaDati()

        Try
            '*****************APERTURA CONNESSIONE***************



            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("ModuloPromUtente.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()





            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If Request.QueryString("PROV") = "1" Then

                par.cmd.CommandText = "Select DATA_APP_PRE_SLOGGIO, DATA_APP_RAPPORTO_SLOGGIO FROM SISCOM_MI.SL_SLOGGIO WHERE ID_CONTRATTO = " & Request.QueryString("COD")

            Else

                par.cmd.CommandText = "Select DATA_APP_PRE_SLOGGIO, DATA_APP_RAPPORTO_SLOGGIO FROM SISCOM_MI.SL_SLOGGIO WHERE ID = " & Request.QueryString("IDSLOGGIO")



            End If


            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If lettore.Read Then




                contenuto = Replace(contenuto, "$datapreSL$", par.FormattaData(Mid(par.IfNull(lettore("DATA_APP_PRE_SLOGGIO"), "__________________________"), 1, 8)))
                contenuto = Replace(contenuto, "$orapreSL$", Mid(par.IfNull(lettore("DATA_APP_PRE_SLOGGIO"), "____________"), 9, 2) & ":" & Mid(par.IfNull(lettore("DATA_APP_PRE_SLOGGIO"), "____________"), 11, 2))
                contenuto = Replace(contenuto, "$dataSL$", par.FormattaData(Mid(par.IfNull(lettore("DATA_APP_RAPPORTO_SLOGGIO"), "__________________________"), 1, 8)))
                contenuto = Replace(contenuto, "$oraSL$", Mid(par.IfNull(lettore("DATA_APP_RAPPORTO_SLOGGIO"), "____________"), 9, 2) & ":" & Mid(par.IfNull(lettore("DATA_APP_RAPPORTO_SLOGGIO"), "____________"), 11, 2))

            Else


                contenuto = Replace(contenuto, "$datapreSL$", "___/___/______")
                contenuto = Replace(contenuto, "$orapreSL$", "___:___")
                contenuto = Replace(contenuto, "$dataSL$", "___/___/______")
                contenuto = Replace(contenuto, "$oraSL$", "___:___")



            End If


            lettore.Close()


            Dim url As String = Server.MapPath("..\FileTemp\")
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
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))

            'Response.Write("<script>window.open('../FileTemp/" & nomefile & "','Modulo','');</script>")
            Response.Redirect("..\FileTemp\" & nomefile, False)































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
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try




    End Sub








End Class