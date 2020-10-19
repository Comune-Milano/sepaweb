Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Partial Class ASS_Doc_Offerta_Alloggio_Doc_TestoTelegramma
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



            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Doc_TestoTelegramma.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()

            sr1.Close()





            Select Case tipo.Value

                Case "1" 'DOMANDA

                    par.cmd.CommandText = "SELECT domande_bando.pg, (comp_nucleo.cognome || ' ' || comp_nucleo.nome) as intest, comp_nucleo.cod_fiscale, domande_bando.fl_ass_esterna, " _
                                          & " domande_bando.tipo_alloggio, domande_bando.id_dichiarazione, " _
                                          & " (t_tipo_indirizzo.descrizione || ' ' || dichiarazioni.ind_res_dnte || ', ' || dichiarazioni.civico_res_dnte) AS indirizzo_intest, " _
                                          & " comuni_nazioni.nome as localita, comuni_nazioni.cap " _
                                          & " FROM domande_bando, t_tipo_indirizzo, comp_nucleo, dichiarazioni, comuni_nazioni " _
                                          & " WHERE dichiarazioni.ID = domande_bando.id_dichiarazione " _
                                          & " AND domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione(+) " _
                                          & " AND comp_nucleo.progr = domande_bando.progr_componente " _
                                          & " AND t_tipo_indirizzo.cod = dichiarazioni.id_tipo_ind_res_dnte " _
                                          & " AND dichiarazioni.id_luogo_res_dnte = comuni_nazioni.ID " _
                                          & " AND domande_bando.ID =" & idDomanda.Value & ""


                Case "2"  'CAMBI

                    par.cmd.CommandText = "SELECT domande_bando_cambi.pg, (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) as intest, comp_nucleo_cambi.cod_fiscale, domande_bando_cambi.fl_ass_esterna, " _
                                         & " domande_bando_cambi.tipo_alloggio, domande_bando_cambi.id_dichiarazione, " _
                                         & " (t_tipo_indirizzo.descrizione || ' ' || dichiarazioni_cambi.ind_res_dnte || ', ' || dichiarazioni_cambi.civico_res_dnte) AS indirizzo_intest, " _
                                         & " comuni_nazioni.nome as localita, comuni_nazioni.cap " _
                                         & " FROM domande_bando_cambi, t_tipo_indirizzo, comp_nucleo_cambi, dichiarazioni_cambi, comuni_nazioni " _
                                         & " WHERE dichiarazioni_cambi.ID = domande_bando_cambi.id_dichiarazione " _
                                         & " AND domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione(+) " _
                                         & " AND comp_nucleo_cambi.progr = domande_bando_cambi.progr_componente " _
                                         & " AND t_tipo_indirizzo.cod = dichiarazioni_cambi.id_tipo_ind_res_dnte " _
                                         & " AND dichiarazioni_cambi.id_luogo_res_dnte = comuni_nazioni.ID " _
                                         & " AND domande_bando_cambi.ID =" & idDomanda.Value & ""








                Case "3"   'EMERGENZE


                    par.cmd.CommandText = "SELECT domande_bando_vsa.pg, (comp_nucleo_vsa.cognome || ' ' || comp_nucleo_vsa.nome) as intest, comp_nucleo_vsa.cod_fiscale, domande_bando_vsa.fl_ass_esterna, " _
                                       & " domande_bando_vsa.tipo_alloggio, domande_bando_vsa.id_dichiarazione, " _
                                       & " (t_tipo_indirizzo.descrizione || ' ' || dichiarazioni_vsa.ind_res_dnte || ', ' || dichiarazioni_vsa.civico_res_dnte) AS indirizzo_intest, " _
                                       & " comuni_nazioni.nome as localita, comuni_nazioni.cap " _
                                       & " FROM domande_bando_vsa, t_tipo_indirizzo, comp_nucleo_vsa, dichiarazioni_vsa, comuni_nazioni " _
                                       & " WHERE dichiarazioni_vsa.ID = domande_bando_vsa.id_dichiarazione " _
                                       & " AND domande_bando_vsa.id_dichiarazione = comp_nucleo_vsa.id_dichiarazione(+) " _
                                       & " AND comp_nucleo_vsa.progr = domande_bando_vsa.progr_componente " _
                                       & " AND t_tipo_indirizzo.cod = dichiarazioni_vsa.id_tipo_ind_res_dnte " _
                                       & " AND dichiarazioni_vsa.id_luogo_res_dnte = comuni_nazioni.ID " _
                                       & " AND domande_bando_vsa.ID =" & idDomanda.Value & ""





            End Select

            Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderJ.Read Then



                contenuto = Replace(contenuto, "$nome$", par.IfNull(myReaderJ("intest"), "_______________________"))
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderJ("indirizzo_intest"), "_______________________"))
                contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderJ("cap"), "________"))
                contenuto = Replace(contenuto, "$localita$", par.IfNull(myReaderJ("localita"), "___________"))

            



            Else

                contenuto = Replace(contenuto, "$nome$", "_______________________")
                contenuto = Replace(contenuto, "$indirizzo$", "_______________________")
                contenuto = Replace(contenuto, "$cap$", "________")
                contenuto = Replace(contenuto, "$localita$", "___________")



            End If
            myReaderJ.Close()






            par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, (operatori.cognome||' ' || operatori.nome) as operatorefil, " _
                                & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, " _
                                & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
                                & " FROM siscom_mi.tab_filiali, operatori " _
                                & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
                                & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"



            Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderK.Read Then

                contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
                contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
                contenuto = Replace(contenuto, "$acronimo$", par.IfNull(myReaderK("acronimo"), "___________"))
                contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
                contenuto = Replace(contenuto, "$operatore$", par.IfNull(myReaderK("operatorefil"), "___________"))



            Else
                contenuto = Replace(contenuto, "$responsabile$", "___________")
                contenuto = Replace(contenuto, "$telfiliale$", "___________")
                contenuto = Replace(contenuto, "$acronimo$", "___________")
                contenuto = Replace(contenuto, "$faxfiliale$", "___________")
                contenuto = Replace(contenuto, "$operatore$", "___________")
            End If
            myReaderK.Close()





            ' Dim PercorsoBarCode As String = par.RicavaBarCode(28, idDomanda.Value)
            ' contenuto = Replace(contenuto, "$barcode$", Server.MapPath("..\..\FileTemp\") & PercorsoBarCode)
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

            Dim nomefile As String = "A1_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
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
