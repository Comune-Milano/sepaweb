Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Partial Class CENSIMENTO_ModuloRappSloggio
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



            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("ModuloRappSloggio.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()





            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If




        



            par.cmd.CommandText = "select unita_immobiliari.id_unita_principale, tipologia_unita_immobiliari.descrizione as tipounita,unita_immobiliari.cod_tipologia,unita_immobiliari.cod_tipo_disponibilita,complessi_immobiliari.id as idq, identificativi_catastali.superficie_mq as SUP_MQ ,COMPLESSI_IMMOBILIARI.ID_QUARTIERE AS ID_QUART, TAB_QUARTIERI.NOME AS NOME_QUART, edifici.id as idf,edifici.fl_piano_vendita,EDIFICI.GEST_RISC_DIR,edifici.condominio,tipo_livello_piano.descrizione as miopiano,(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA,indirizzi.cap,comuni_nazioni.nome as comune,unita_immobiliari.id_destinazione_uso,SISCOM_MI.UNITA_IMMOBILIARI.interno,UNITA_IMMOBILIARI.id as idunita,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,indirizzi.descrizione,indirizzi.civico,indirizzi.cap from siscom_mi.tipologia_unita_immobiliari, siscom_mi.identificativi_catastali, siscom_mi.tipo_livello_piano,siscom_mi.tab_quartieri,comuni_nazioni,siscom_mi.indirizzi,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE tipologia_unita_immobiliari.cod=unita_immobiliari.cod_tipologia and COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND edifici.id=unita_immobiliari.id_edificio and unita_immobiliari.cod_tipo_livello_piano=tipo_livello_piano.cod (+) and indirizzi.cod_comune=comuni_nazioni.cod (+) and COMPLESSI_IMMOBILIARI.ID_QUARTIERE = tab_quartieri.id and unita_immobiliari.id_catastale=identificativi_catastali.id (+) and unita_immobiliari.id_indirizzo=indirizzi.id (+) and unita_immobiliari.ID=" & Request.QueryString("ID")
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                '







                'interno.Value = par.IfNull(myReader3("interno"), "")



                contenuto = Replace(contenuto, "$codiceui$", par.IfNull(myReader3("COD_UNITA_IMMOBILIARE"), ""))
                contenuto = Replace(contenuto, "$quartiere$", par.IfNull(myReader3("nome_quart"), ""))
                contenuto = Replace(contenuto, "$via$", par.IfNull(myReader3("descrizione"), "") & ", " & par.IfNull(myReader3("civico"), ""))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader3("scala"), ""))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader3("miopiano"), ""))
                contenuto = Replace(contenuto, "$sup_mq$", par.IfNull(myReader3("sup_mq"), ""))
                contenuto = Replace(contenuto, "$interno$", par.IfNull(myReader3("interno"), ""))

            End If

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
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\NuoveImm\"))

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

