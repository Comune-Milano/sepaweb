﻿Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Partial Class ASS_Doc_Offerta_Alloggio_Doc_OffertaAllDiffidaAccet
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



            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Doc_OffertaAllDiffidaAccet.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()

            sr1.Close()


            Select Case tipo.Value


                Case "1" 'DOMANDA
                    par.cmd.CommandText = " SELECT domande_bando.id, alloggi.cod_alloggio, t_tipo_proprieta.descrizione AS proprieta " _
                                        & " FROM domande_bando, comp_nucleo, dichiarazioni, alloggi, t_tipo_proprieta, rel_prat_all_ccaa_erp " _
                                        & " WHERE dichiarazioni.ID = domande_bando.id_dichiarazione " _
                                        & " AND domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione(+) " _
                                        & " And comp_nucleo.progr = domande_bando.progr_componente " _
                                        & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) " _
                                        & " And rel_prat_all_ccaa_erp.id_pratica = domande_bando.ID " _
                                        & " And rel_prat_all_ccaa_erp.id_alloggio = alloggi.ID " _
                                        & " And rel_prat_all_ccaa_erp.ultimo = 1 " _
                                        & " AND domande_bando.ID =" & idDomanda.Value


                Case "2"  'CAMBI


                    par.cmd.CommandText = " SELECT domande_bando_cambi.id, alloggi.cod_alloggio, t_tipo_proprieta.descrizione AS proprieta " _
                                      & " FROM domande_bando_cambi, comp_nucleo_cambi, dichiarazioni_cambi, alloggi, t_tipo_proprieta, rel_prat_all_ccaa_erp " _
                                      & " WHERE dichiarazioni_cambi.ID = domande_bando_cambi.id_dichiarazione " _
                                      & " AND domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione(+) " _
                                      & " And comp_nucleo_cambi.progr = domande_bando_cambi.progr_componente " _
                                      & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) " _
                                      & " And rel_prat_all_ccaa_erp.id_pratica = domande_bando_cambi.ID " _
                                      & " And rel_prat_all_ccaa_erp.id_alloggio = alloggi.ID " _
                                      & " And rel_prat_all_ccaa_erp.ultimo = 1 " _
                                      & " AND domande_bando_cambi.ID =" & idDomanda.Value







                Case "3"   'EMERGENZE


                    par.cmd.CommandText = " SELECT domande_bando_vsa.id, alloggi.cod_alloggio, t_tipo_proprieta.descrizione AS proprieta " _
                                      & " FROM domande_bando_vsa, comp_nucleo_vsa, dichiarazioni_vsa, alloggi, t_tipo_proprieta, rel_prat_all_ccaa_erp " _
                                      & " WHERE dichiarazioni_vsa.ID = domande_bando_vsa.id_dichiarazione " _
                                      & " AND domande_bando_vsa.id_dichiarazione = comp_nucleo_vsa.id_dichiarazione(+) " _
                                      & " And comp_nucleo_vsa.progr = domande_bando_vsa.progr_componente " _
                                      & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) " _
                                      & " And rel_prat_all_ccaa_erp.id_pratica = domande_bando_vsa.ID " _
                                      & " And rel_prat_all_ccaa_erp.id_alloggio = alloggi.ID " _
                                      & " And rel_prat_all_ccaa_erp.ultimo = 1 " _
                                      & " AND domande_bando_vsa.ID =" & idDomanda.Value






            End Select

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore.Read Then
                txt_proprieta.Value = par.IfNull(lettore("proprieta"), -1)

            End If

            lettore.Close()









            Select Case tipo.Value

                Case "1" 'DOMANDA

                    par.cmd.CommandText = " SELECT domande_bando.pg, (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS intest, " _
                                        & "  (   t_tipo_indirizzo.descrizione || ' ' || dichiarazioni.ind_res_dnte || ', ' || dichiarazioni.civico_res_dnte) AS indirizzo_intest, " _
                                        & "  comuni_nazioni.nome AS localita_intest, comuni_nazioni.cap AS cap_intest, rel_prat_all_ccaa_erp.data_proposta " _
                                        & "  FROM domande_bando, comp_nucleo, dichiarazioni, alloggi, t_tipo_indirizzo, comuni_nazioni, rel_prat_all_ccaa_erp " _
                                        & " WHERE dichiarazioni.ID = domande_bando.id_dichiarazione " _
                                        & " AND domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione(+) " _
                                        & " And comp_nucleo.progr = domande_bando.progr_componente " _
                                        & " And t_tipo_indirizzo.cod = dichiarazioni.id_tipo_ind_res_dnte " _
                                        & " And dichiarazioni.id_luogo_res_dnte = comuni_nazioni.ID " _
                                        & " And rel_prat_all_ccaa_erp.id_pratica = domande_bando.ID " _
                                        & " And rel_prat_all_ccaa_erp.ultimo = 1 " _
                                        & " And domande_bando.ID =" & idDomanda.Value



                    Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then



                        contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderJ("intest"), "_______________________"))
                        contenuto = Replace(contenuto, "$indirizzoIntest$", par.IfNull(myReaderJ("indirizzo_intest"), "_______________________"))
                        contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderJ("cap_intest"), "________"))
                        contenuto = Replace(contenuto, "$localita$", par.IfNull(myReaderJ("localita_intest"), "___________"))
                        contenuto = Replace(contenuto, "$dataProp$", par.FormattaData(par.IfNull(myReaderJ("data_proposta"), "__/__/____")))



                    Else

                        contenuto = Replace(contenuto, "$richiedente$", "_______________________")
                        contenuto = Replace(contenuto, "$indirizzoIntest$", "_______________________")
                        contenuto = Replace(contenuto, "$cap$", "________")
                        contenuto = Replace(contenuto, "$localita$", "___________")
                        contenuto = Replace(contenuto, "$dataProp$", "__/__/____")

                    End If


                    myReaderJ.Close()


                    contenuto = Replace(contenuto, "$numAlloggio$", "_______")
                    contenuto = Replace(contenuto, "$indirizzo$", "_______________________")

                    'If txt_proprieta.Value <> "ALER" Then


                    '    par.cmd.CommandText = " SELECT domande_bando.pg, (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS intest, unita_immobiliari.interno, " _
                    '                        & " (indirizzi.descrizione || ', ' || indirizzi.civico) AS indirizzo, indirizzi.cap, comuni_nazioni.nome AS comune, " _
                    '                        & " piani.descrizione AS piano, scale_edifici.descrizione AS scala, alloggi.sup " _
                    '                        & " FROM domande_bando, comp_nucleo, alloggi, siscom_mi.unita_immobiliari,comuni_nazioni, siscom_mi.indirizzi, " _
                    '                        & " siscom_mi.scale_edifici, siscom_mi.piani, siscom_mi.edifici " _
                    '                        & " WHERE domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione(+) " _
                    '                        & " And comp_nucleo.progr = domande_bando.progr_componente " _
                    '                        & " AND alloggi.prenotato = '1' " _
                    '                        & " And alloggi.id_pratica = domande_bando.ID " _
                    '                        & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                    '                        & " And unita_immobiliari.id_indirizzo = indirizzi.ID " _
                    '                        & " And unita_immobiliari.id_piano = piani.ID " _
                    '                        & " And unita_immobiliari.id_scala = scale_edifici.ID " _
                    '                        & " And unita_immobiliari.id_edificio = edifici.ID " _
                    '                        & " And comuni_nazioni.cod = indirizzi.cod_comune " _
                    '                        & " And domande_bando.ID =" & idDomanda.Value & ""




                    '    myReaderJ = par.cmd.ExecuteReader()
                    '    If myReaderJ.Read Then



                    '        contenuto = Replace(contenuto, "$numAlloggio$", par.IfNull(myReaderJ("interno"), "_______"))
                    '        contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderJ("indirizzo"), "_______________________"))



                    '    Else

                    '        contenuto = Replace(contenuto, "$numAlloggio$", "_______")
                    '        contenuto = Replace(contenuto, "$indirizzo$", "_______________________")




                    '    End If




                    '    myReaderJ.Close()


                    'Else
                    '  contenuto = Replace(contenuto, "$numAlloggio$", "_______")
                    ' contenuto = Replace(contenuto, "$indirizzo$", "_______________________")


                    'End If


                Case "2"  'CAMBI


                    par.cmd.CommandText = " SELECT domande_bando_cambi.pg, (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS intest, " _
                                      & "  (t_tipo_indirizzo.descrizione || ' ' || dichiarazioni_cambi.ind_res_dnte || ', ' || dichiarazioni_cambi.civico_res_dnte) AS indirizzo_intest, " _
                                      & "  comuni_nazioni.nome AS localita_intest, comuni_nazioni.cap AS cap_intest, rel_prat_all_ccaa_erp.data_proposta " _
                                      & "  FROM domande_bando_cambi, comp_nucleo_cambi, dichiarazioni_cambi, alloggi, t_tipo_indirizzo, comuni_nazioni, rel_prat_all_ccaa_erp " _
                                      & " WHERE dichiarazioni_cambi.ID = domande_bando_cambi.id_dichiarazione " _
                                      & " AND domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione(+) " _
                                      & " And comp_nucleo_cambi.progr = domande_bando_cambi.progr_componente " _
                                      & " And t_tipo_indirizzo.cod = dichiarazioni_cambi.id_tipo_ind_res_dnte " _
                                      & " And dichiarazioni_cambi.id_luogo_res_dnte = comuni_nazioni.ID " _
                                      & " And rel_prat_all_ccaa_erp.id_pratica = domande_bando_cambi.ID " _
                                      & " And rel_prat_all_ccaa_erp.ultimo = 1 " _
                                      & " And domande_bando_cambi.ID =" & idDomanda.Value



                    Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then


                        contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderJ("intest"), "_______________________"))
                        contenuto = Replace(contenuto, "$indirizzoIntest$", par.IfNull(myReaderJ("indirizzo_intest"), "_______________________"))
                        contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderJ("cap_intest"), "________"))
                        contenuto = Replace(contenuto, "$localita$", par.IfNull(myReaderJ("localita_intest"), "___________"))
                        contenuto = Replace(contenuto, "$dataProp$", par.FormattaData(par.IfNull(myReaderJ("data_proposta"), "__/__/____")))


                    Else

                        contenuto = Replace(contenuto, "$richiedente$", "_______________________")
                        contenuto = Replace(contenuto, "$indirizzoIntest$", "_______________________")
                        contenuto = Replace(contenuto, "$cap$", "________")
                        contenuto = Replace(contenuto, "$localita$", "___________")
                        contenuto = Replace(contenuto, "$dataProp$", "__/__/____")


                    End If


                    myReaderJ.Close()

                    contenuto = Replace(contenuto, "$numAlloggio$", "_______")
                    contenuto = Replace(contenuto, "$indirizzo$", "_______________________")


                    'If txt_proprieta.Value <> "ALER" Then


                    '    par.cmd.CommandText = " SELECT domande_bando_cambi.pg, (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS intest, unita_immobiliari.interno, " _
                    '                        & " (indirizzi.descrizione || ' ' || indirizzi.civico) AS indirizzo, indirizzi.cap, comuni_nazioni.nome AS comune, " _
                    '                        & " piani.descrizione AS piano, scale_edifici.descrizione AS scala, alloggi.sup " _
                    '                        & " FROM domande_bando_cambi, comp_nucleo_cambi, alloggi, siscom_mi.unita_immobiliari,comuni_nazioni, siscom_mi.indirizzi, " _
                    '                        & " siscom_mi.scale_edifici, siscom_mi.piani, siscom_mi.edifici " _
                    '                        & " WHERE domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione(+) " _
                    '                        & " And comp_nucleo_cambi.progr = domande_bando_cambi.progr_componente " _
                    '                        & " AND alloggi.prenotato = '1' " _
                    '                        & " And alloggi.id_pratica = domande_bando_cambi.ID " _
                    '                        & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                    '                        & " And unita_immobiliari.id_indirizzo = indirizzi.ID " _
                    '                        & " And unita_immobiliari.id_piano = piani.ID " _
                    '                        & " And unita_immobiliari.id_scala = scale_edifici.ID " _
                    '                        & " And unita_immobiliari.id_edificio = edifici.ID " _
                    '                        & " And comuni_nazioni.cod = indirizzi.cod_comune " _
                    '                        & " And domande_bando_cambi.ID =" & idDomanda.Value & ""




                    '    myReaderJ = par.cmd.ExecuteReader()
                    '    If myReaderJ.Read Then



                    '        contenuto = Replace(contenuto, "$numAlloggio$", par.IfNull(myReaderJ("interno"), "_______"))
                    '        contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderJ("indirizzo"), "_______________________"))



                    '    Else

                    '        contenuto = Replace(contenuto, "$numAlloggio$", "_______")
                    '        contenuto = Replace(contenuto, "$indirizzo$", "_______________________")


                    '    End If




                    '    myReaderJ.Close()


                    'Else

                    '    contenuto = Replace(contenuto, "$numAlloggio$", "_______")
                    '    contenuto = Replace(contenuto, "$indirizzo$", "_______________________")


                    'End If



                Case "3"   'EMERGENZE


                    par.cmd.CommandText = " SELECT domande_bando_vsa.pg, (comp_nucleo_vsa.cognome || ' ' || comp_nucleo_vsa.nome) AS intest, " _
                                       & "  (   t_tipo_indirizzo.descrizione || ' ' || dichiarazioni_vsa.ind_res_dnte || ', ' || dichiarazioni_vsa.civico_res_dnte) AS indirizzo_intest, " _
                                       & "  comuni_nazioni.nome AS localita_intest, comuni_nazioni.cap AS cap_intest, rel_prat_all_ccaa_erp.data_proposta " _
                                       & "  FROM domande_bando_vsa, comp_nucleo_vsa, dichiarazioni_vsa, alloggi, t_tipo_indirizzo, comuni_nazioni, rel_prat_all_ccaa_erp " _
                                       & " WHERE dichiarazioni_vsa.ID = domande_bando_vsa.id_dichiarazione " _
                                       & " AND domande_bando_vsa.id_dichiarazione = comp_nucleo_vsa.id_dichiarazione(+) " _
                                       & " And comp_nucleo_vsa.progr = domande_bando_vsa.progr_componente " _
                                       & " And t_tipo_indirizzo.cod = dichiarazioni_vsa.id_tipo_ind_res_dnte " _
                                       & " And dichiarazioni_vsa.id_luogo_res_dnte = comuni_nazioni.ID " _
                                       & " And rel_prat_all_ccaa_erp.id_pratica = domande_bando_vsa.ID " _
                                       & " And rel_prat_all_ccaa_erp.ultimo = 1 " _
                                       & " And domande_bando_vsa.ID =" & idDomanda.Value



                    Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then



                        contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderJ("intest"), "_______________________"))
                        contenuto = Replace(contenuto, "$indirizzoIntest$", par.IfNull(myReaderJ("indirizzo_intest"), "_______________________"))
                        contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderJ("cap_intest"), "________"))
                        contenuto = Replace(contenuto, "$localita$", par.IfNull(myReaderJ("localita_intest"), "___________"))
                        contenuto = Replace(contenuto, "$dataProp$", par.FormattaData(par.IfNull(myReaderJ("data_proposta"), "__/__/____")))



                    Else

                        contenuto = Replace(contenuto, "$richiedente$", "_______________________")
                        contenuto = Replace(contenuto, "$indirizzoIntest$", "_______________________")
                        contenuto = Replace(contenuto, "$cap$", "________")
                        contenuto = Replace(contenuto, "$localita$", "___________")
                        contenuto = Replace(contenuto, "$dataProp$", "__/__/____")


                    End If


                    myReaderJ.Close()


                    contenuto = Replace(contenuto, "$numAlloggio$", "_______")
                    contenuto = Replace(contenuto, "$indirizzo$", "_______________________")

                    'If txt_proprieta.Value <> "ALER" Then


                    '    par.cmd.CommandText = " SELECT domande_bando_vsa.pg, (comp_nucleo_vsa.cognome || ' ' || comp_nucleo_vsa.nome) AS intest, unita_immobiliari.interno, " _
                    '                        & " (indirizzi.descrizione || ' ' || indirizzi.civico) AS indirizzo, indirizzi.cap, comuni_nazioni.nome AS comune, " _
                    '                        & " piani.descrizione AS piano, scale_edifici.descrizione AS scala, alloggi.sup " _
                    '                        & " FROM domande_bando_vsa, comp_nucleo_vsa, alloggi, siscom_mi.unita_immobiliari,comuni_nazioni, siscom_mi.indirizzi, " _
                    '                        & " siscom_mi.scale_edifici, siscom_mi.piani, siscom_mi.edifici " _
                    '                        & " WHERE domande_bando_vsa.id_dichiarazione = comp_nucleo_vsa.id_dichiarazione(+) " _
                    '                        & " And comp_nucleo_vsa.progr = domande_bando_vsa.progr_componente " _
                    '                        & " AND alloggi.prenotato = '1' " _
                    '                        & " And alloggi.id_pratica = domande_bando_vsa.ID " _
                    '                        & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                    '                        & " And unita_immobiliari.id_indirizzo = indirizzi.ID " _
                    '                        & " And unita_immobiliari.id_piano = piani.ID " _
                    '                        & " And unita_immobiliari.id_scala = scale_edifici.ID " _
                    '                        & " And unita_immobiliari.id_edificio = edifici.ID " _
                    '                        & " And comuni_nazioni.cod = indirizzi.cod_comune " _
                    '                        & " And domande_bando_vsa.ID =" & idDomanda.Value & ""




                    '    myReaderJ = par.cmd.ExecuteReader()
                    '    If myReaderJ.Read Then



                    '        contenuto = Replace(contenuto, "$numAlloggio$", par.IfNull(myReaderJ("interno"), "_______"))
                    '        contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderJ("indirizzo"), "_______________________"))



                    '    Else

                    '        contenuto = Replace(contenuto, "$numAlloggio$", "_______")
                    '        contenuto = Replace(contenuto, "$indirizzo$", "_______________________")


                    '    End If




                    '    myReaderJ.Close()
                    'Else


                    '    contenuto = Replace(contenuto, "$numAlloggio$", "_______")
                    '    contenuto = Replace(contenuto, "$indirizzo$", "_______________________")



                    'End If


                    contenuto = Replace(contenuto, "$numAlloggio$", "_______")
                    contenuto = Replace(contenuto, "$indirizzo$", "_______________________")


            End Select

            


            par.cmd.CommandText = " SELECT tab_filiali.ID, tab_filiali.nome, (indirizzi.descrizione||', ' || indirizzi.civico) as indirizzo, indirizzi.localita, indirizzi.cap, " _
                                & " tab_filiali.acronimo, tab_filiali.ref_amministrativo, tab_filiali.responsabile, tab_filiali.centro_di_costo, " _
                                & " tab_filiali.n_telefono, tab_filiali.n_fax, tab_filiali.n_telefono_verde " _
                                & " FROM siscom_mi.tab_filiali, operatori, siscom_mi.indirizzi " _
                                & " WHERE tab_filiali.ID = operatori.id_ufficio(+) " _
                                & " And tab_filiali.id_indirizzo = indirizzi.ID " _
                                & " AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"



            Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderK.Read Then

                contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderK("responsabile"), "___________"))
                contenuto = Replace(contenuto, "$nomefiliale$", par.IfNull(myReaderK("nome"), "___________"))
                contenuto = Replace(contenuto, "$indirizzofiliale$", par.IfNull(myReaderK("indirizzo"), "___________"))
                contenuto = Replace(contenuto, "$capfiliale$", par.IfNull(myReaderK("cap"), "___________"))
                contenuto = Replace(contenuto, "$cittafiliale$", par.IfNull(myReaderK("localita"), "___________"))
                contenuto = Replace(contenuto, "$telfiliale$", par.IfNull(myReaderK("n_telefono"), "___________"))
                contenuto = Replace(contenuto, "$faxfiliale$", par.IfNull(myReaderK("n_fax"), "___________"))
                contenuto = Replace(contenuto, "$referente$", par.IfNull(myReaderK("ref_amministrativo"), "___________"))
                contenuto = Replace(contenuto, "$centrodicosto$", par.IfNull(myReaderK("centro_di_costo"), ""))



            Else
                contenuto = Replace(contenuto, "$responsabile$", "___________")
                contenuto = Replace(contenuto, "$nomefiliale$", "___________")
                contenuto = Replace(contenuto, "$indirizzofiliale$", "___________")
                contenuto = Replace(contenuto, "$capfiliale$", "___________")
                contenuto = Replace(contenuto, "$cittafiliale$", "___________")
                contenuto = Replace(contenuto, "$telfiliale$", "___________")
                contenuto = Replace(contenuto, "$faxfiliale$", "___________")
                contenuto = Replace(contenuto, "$referente$", "___________")
                contenuto = Replace(contenuto, "$centrodicosto$", "")
            End If
            myReaderK.Close()












            Dim PercorsoBarCode As String = par.RicavaBarCode(32, idDomanda.Value)
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

            Dim nomefile As String = "A5_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
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