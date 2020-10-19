Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Partial Class ASS_Doc_Offerta_Alloggio_Doc_PermessoVisita
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



            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Doc_PermessoVisita.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()

            sr1.Close()

               Select tipo.Value


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



            Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderK.Read Then
                txt_proprieta.Value = par.IfNull(myReaderK("proprieta"), -1)

            End If
            myReaderK.Close()

            If txt_proprieta.Value = "MM S.P.A." Then

                Select Case tipo.Value


                    Case "1" 'DOMANDA

                        par.cmd.CommandText = " SELECT domande_bando.id, domande_bando.PG, (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS intest, alloggi.cod_alloggio, rel_prat_all_ccaa_erp.esito " _
                                            & " FROM domande_bando, comp_nucleo, dichiarazioni,alloggi,rel_prat_all_ccaa_erp " _
                                            & " WHERE dichiarazioni.ID = domande_bando.id_dichiarazione " _
                                            & " AND domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione(+) " _
                                            & " And comp_nucleo.progr = domande_bando.progr_componente " _
                                            & " And rel_prat_all_ccaa_erp.id_pratica = domande_bando.ID " _
                                            & " And rel_prat_all_ccaa_erp.id_alloggio = alloggi.ID " _
                                            & " And rel_prat_all_ccaa_erp.ultimo = 1 " _
                                            & " AND domande_bando.ID =" & idDomanda.Value



                    Case "2"  'CAMBI


                        par.cmd.CommandText = " SELECT domande_bando_cambi.id, domande_bando_cambi.PG, (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS intest, alloggi.cod_alloggio, rel_prat_all_ccaa_erp.esito " _
                                           & " FROM domande_bando_cambi, comp_nucleo_cambi, dichiarazioni_cambi,alloggi,rel_prat_all_ccaa_erp " _
                                           & " WHERE dichiarazioni_cambi.ID = domande_bando_cambi.id_dichiarazione " _
                                           & " AND domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione(+) " _
                                           & " And comp_nucleo_cambi.progr = domande_bando_cambi.progr_componente " _
                                           & " And rel_prat_all_ccaa_erp.id_pratica = domande_bando_cambi.ID " _
                                           & " And rel_prat_all_ccaa_erp.id_alloggio = alloggi.ID " _
                                           & " And rel_prat_all_ccaa_erp.ultimo = 1 " _
                                           & " AND domande_bando_cambi.ID =" & idDomanda.Value


                    Case "3"   'EMERGENZE


                        par.cmd.CommandText = " SELECT domande_bando_vsa.id, domande_bando_vsa.PG, (comp_nucleo_vsa.cognome || ' ' || comp_nucleo_vsa.nome) AS intest, alloggi.cod_alloggio, rel_prat_all_ccaa_erp.esito " _
                                           & " FROM domande_bando_vsa, comp_nucleo_vsa, dichiarazioni_vsa,alloggi,rel_prat_all_ccaa_erp " _
                                           & " WHERE dichiarazioni_vsa.ID = domande_bando_vsa.id_dichiarazione " _
                                           & " AND domande_bando_vsa.id_dichiarazione = comp_nucleo_vsa.id_dichiarazione(+) " _
                                           & " And comp_nucleo_vsa.progr = domande_bando_vsa.progr_componente " _
                                           & " And rel_prat_all_ccaa_erp.id_pratica = domande_bando_vsa.ID " _
                                           & " And rel_prat_all_ccaa_erp.id_alloggio = alloggi.ID " _
                                           & " And rel_prat_all_ccaa_erp.ultimo = 1 " _
                                           & " AND domande_bando_vsa.ID =" & idDomanda.Value


                End Select




                Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderJ.Read Then





                    contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderJ("intest"), "_______________________"))
                    contenuto = Replace(contenuto, "$codice$", par.IfNull(myReaderJ("cod_alloggio"), "_______________________"))
                    contenuto = Replace(contenuto, "$localita$", "_______________________")
                    contenuto = Replace(contenuto, "$indirizzo$", "_______________________")
                    contenuto = Replace(contenuto, "$numAlloggio$", "___________")
                    contenuto = Replace(contenuto, "$piano$", "_______________________")
                    contenuto = Replace(contenuto, "$superficie$", "___________")
                    contenuto = Replace(contenuto, "$scala$", "___________")
                    contenuto = Replace(contenuto, "$motivazione$", "____________________________")


                    idAlloggio.Value = par.IfNull(myReaderJ("cod_alloggio"), -1)


                    If par.IfNull(myReaderJ("esito"), -1) <> -1 Then

                        If par.IfNull(myReaderJ("esito"), -1) = 0 Then

                            contenuto = Replace(contenuto, "$ics$", "X")
                            contenuto = Replace(contenuto, "$ics1$", "")
                            contenuto = Replace(contenuto, "$ics2$", "")


                        End If




                        If par.IfNull(myReaderJ("esito"), -1) = 1 Then


                            contenuto = Replace(contenuto, "$ics1$", "X")
                            contenuto = Replace(contenuto, "$ics2$", "")
                            contenuto = Replace(contenuto, "$ics$", "")

                        End If




                    Else
                        contenuto = Replace(contenuto, "$ics1$", "")
                        contenuto = Replace(contenuto, "$ics2$", "")
                        contenuto = Replace(contenuto, "$ics$", "")



                    End If

                Else

                    contenuto = Replace(contenuto, "$richiedente$", "_______________________")
                    contenuto = Replace(contenuto, "$ics1$", "")
                    contenuto = Replace(contenuto, "$ics2$", "")
                    contenuto = Replace(contenuto, "$ics$", "")
                    contenuto = Replace(contenuto, "$codice$", "_______________________")
                    contenuto = Replace(contenuto, "$localita$", "_______________________")
                    contenuto = Replace(contenuto, "$indirizzo$", "_______________________")
                    contenuto = Replace(contenuto, "$numAlloggio$", "___________")
                    contenuto = Replace(contenuto, "$piano$", "_______________________")
                    contenuto = Replace(contenuto, "$superficie$", "___________")
                    contenuto = Replace(contenuto, "$scala$", "___________")
                    contenuto = Replace(contenuto, "$motivazione$", "____________________________")



                End If
                myReaderJ.Close()







            Else



                Select Case tipo.Value


                    Case "1" 'DOMANDA

                        par.cmd.CommandText = " SELECT domande_bando.pg, (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS intest, domande_bando.id_dichiarazione, unita_immobiliari.interno,  unita_immobiliari.id as id_unita, " _
                                             & " gestione_alloggi.tipo, gestione_alloggi.cod, t_tipo_gestore.descrizione, gestione_alloggi.sede, t_cond_alloggio.descrizione AS stato, unita_immobiliari.cod_unita_immobiliare, " _
                                             & " (indirizzi.descrizione || ', ' || indirizzi.civico) AS indirizzo, indirizzi.cap, comuni_nazioni.nome AS comune, t_tipo_proprieta.descrizione AS proprieta, " _
                                             & " piani.descrizione AS piano, scale_edifici.descrizione AS scala, alloggi.sup, REL_PRAT_ALL_CCAA_ERP.motivazione, REL_PRAT_ALL_CCAA_ERP.esito " _
                                             & " FROM domande_bando, comp_nucleo, dichiarazioni, alloggi, siscom_mi.unita_immobiliari, gestione_alloggi, t_tipo_gestore, t_cond_alloggio, " _
                                             & " t_tipo_proprieta, comuni_nazioni, siscom_mi.indirizzi, siscom_mi.scale_edifici, siscom_mi.piani, siscom_mi.edifici, REL_PRAT_ALL_CCAA_ERP " _
                                             & " WHERE dichiarazioni.ID = domande_bando.id_dichiarazione " _
                                             & " AND domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione(+) " _
                                             & " And comp_nucleo.progr = domande_bando.progr_componente " _
                                             & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                                             & " And t_tipo_gestore.cod = gestione_alloggi.cod_gestore " _
                                             & " And gestione_alloggi.cod_proprieta = alloggi.proprieta " _
                                             & " AND gestione_alloggi.cod_gestore = alloggi.gestione(+) " _
                                             & " AND alloggi.stato = t_cond_alloggio.cod(+) " _
                                             & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) " _
                                             & " And unita_immobiliari.id_indirizzo = indirizzi.ID " _
                                             & " And unita_immobiliari.id_piano = piani.ID " _
                                             & " And unita_immobiliari.id_scala = scale_edifici.ID " _
                                             & " And unita_immobiliari.id_edificio = edifici.ID " _
                                             & " And comuni_nazioni.cod = indirizzi.cod_comune " _
                                             & " And REL_PRAT_ALL_CCAA_ERP.id_pratica = domande_bando.id " _
                                             & " And REL_PRAT_ALL_CCAA_ERP.id_alloggio = alloggi.id " _
                                             & " And REL_PRAT_ALL_CCAA_ERP.ultimo = 1 " _
                                             & " And domande_bando.ID=" & idDomanda.Value & ""



                    Case "2"  'CAMBI



                        par.cmd.CommandText = " SELECT domande_bando_cambi.pg, (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS intest, domande_bando_cambi.id_dichiarazione, unita_immobiliari.interno, unita_immobiliari.id as id_unita," _
                                             & " gestione_alloggi.tipo, gestione_alloggi.cod, t_tipo_gestore.descrizione, gestione_alloggi.sede, t_cond_alloggio.descrizione AS stato, unita_immobiliari.cod_unita_immobiliare, " _
                                             & " (indirizzi.descrizione || ', ' || indirizzi.civico) AS indirizzo, indirizzi.cap, comuni_nazioni.nome AS comune, t_tipo_proprieta.descrizione AS proprieta, " _
                                             & " piani.descrizione AS piano, scale_edifici.descrizione AS scala, alloggi.sup, REL_PRAT_ALL_CCAA_ERP.motivazione, REL_PRAT_ALL_CCAA_ERP.esito " _
                                             & " FROM domande_bando_cambi, comp_nucleo_cambi, dichiarazioni_cambi, alloggi, siscom_mi.unita_immobiliari, gestione_alloggi, t_tipo_gestore, t_cond_alloggio, " _
                                             & " t_tipo_proprieta, comuni_nazioni, siscom_mi.indirizzi, siscom_mi.scale_edifici, siscom_mi.piani, siscom_mi.edifici, REL_PRAT_ALL_CCAA_ERP " _
                                             & " WHERE dichiarazioni_cambi.ID = domande_bando_cambi.id_dichiarazione " _
                                             & " AND domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione(+) " _
                                             & " And comp_nucleo_cambi.progr = domande_bando_cambi.progr_componente " _
                                             & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                                             & " And t_tipo_gestore.cod = gestione_alloggi.cod_gestore " _
                                             & " And gestione_alloggi.cod_proprieta = alloggi.proprieta " _
                                             & " AND gestione_alloggi.cod_gestore = alloggi.gestione(+) " _
                                             & " AND alloggi.stato = t_cond_alloggio.cod(+) " _
                                             & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) " _
                                             & " And unita_immobiliari.id_indirizzo = indirizzi.ID " _
                                             & " And unita_immobiliari.id_piano = piani.ID " _
                                             & " And unita_immobiliari.id_scala = scale_edifici.ID " _
                                             & " And unita_immobiliari.id_edificio = edifici.ID " _
                                             & " And comuni_nazioni.cod = indirizzi.cod_comune " _
                                             & " And REL_PRAT_ALL_CCAA_ERP.id_pratica = domande_bando_cambi.id " _
                                             & " And REL_PRAT_ALL_CCAA_ERP.id_alloggio = alloggi.id " _
                                              & " And REL_PRAT_ALL_CCAA_ERP.ultimo = 1 " _
                                             & " And domande_bando_cambi.ID=" & idDomanda.Value & ""





                    Case "3"   'EMERGENZE



                        par.cmd.CommandText = " SELECT domande_bando_vsa.pg, (comp_nucleo_vsa.cognome || ' ' || comp_nucleo_vsa.nome) AS intest, domande_bando_vsa.id_dichiarazione, unita_immobiliari.interno, unita_immobiliari.id as id_unita, " _
                                             & " gestione_alloggi.tipo, gestione_alloggi.cod, t_tipo_gestore.descrizione, gestione_alloggi.sede, t_cond_alloggio.descrizione AS stato, unita_immobiliari.cod_unita_immobiliare, " _
                                             & " (indirizzi.descrizione || ', ' || indirizzi.civico) AS indirizzo, indirizzi.cap, comuni_nazioni.nome AS comune, t_tipo_proprieta.descrizione AS proprieta, " _
                                             & " piani.descrizione AS piano, scale_edifici.descrizione AS scala, alloggi.sup, REL_PRAT_ALL_CCAA_ERP.motivazione, REL_PRAT_ALL_CCAA_ERP.esito " _
                                             & " FROM domande_bando_vsa, comp_nucleo_vsa, dichiarazioni_vsa, alloggi, siscom_mi.unita_immobiliari, gestione_alloggi, t_tipo_gestore, t_cond_alloggio, " _
                                             & " t_tipo_proprieta, comuni_nazioni, siscom_mi.indirizzi, siscom_mi.scale_edifici, siscom_mi.piani, siscom_mi.edifici, REL_PRAT_ALL_CCAA_ERP " _
                                             & " WHERE dichiarazioni_vsa.ID = domande_bando_vsa.id_dichiarazione " _
                                             & " AND domande_bando_vsa.id_dichiarazione = comp_nucleo_vsa.id_dichiarazione(+) " _
                                             & " And comp_nucleo_vsa.progr = domande_bando_vsa.progr_componente " _
                                             & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                                             & " And t_tipo_gestore.cod = gestione_alloggi.cod_gestore " _
                                             & " And gestione_alloggi.cod_proprieta = alloggi.proprieta " _
                                             & " AND gestione_alloggi.cod_gestore = alloggi.gestione(+) " _
                                             & " AND alloggi.stato = t_cond_alloggio.cod(+) " _
                                             & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) " _
                                             & " And unita_immobiliari.id_indirizzo = indirizzi.ID " _
                                             & " And unita_immobiliari.id_piano = piani.ID " _
                                             & " And unita_immobiliari.id_scala = scale_edifici.ID " _
                                             & " And unita_immobiliari.id_edificio = edifici.ID " _
                                             & " And comuni_nazioni.cod = indirizzi.cod_comune " _
                                             & " And REL_PRAT_ALL_CCAA_ERP.id_pratica = domande_bando_vsa.id " _
                                             & " And REL_PRAT_ALL_CCAA_ERP.id_alloggio = alloggi.id " _
                                              & " And REL_PRAT_ALL_CCAA_ERP.ultimo = 1 " _
                                             & " And domande_bando_vsa.ID=" & idDomanda.Value & ""





                End Select

                Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderJ.Read Then





                    contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderJ("intest"), "_______________________"))
                    contenuto = Replace(contenuto, "$localita$", par.IfNull(myReaderJ("comune"), "_______________________"))
                    contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderJ("indirizzo"), "_______________________"))
                    contenuto = Replace(contenuto, "$numAlloggio$", par.IfNull(myReaderJ("interno"), "___________"))
                    contenuto = Replace(contenuto, "$piano$", par.IfNull(myReaderJ("piano"), "_______________________"))
                    contenuto = Replace(contenuto, "$superficie$", par.IfNull(myReaderJ("sup"), "___________"))
                    contenuto = Replace(contenuto, "$scala$", par.IfNull(myReaderJ("scala"), "___________"))
                    contenuto = Replace(contenuto, "$codice$", par.IfNull(myReaderJ("cod_unita_immobiliare"), "_______________________"))
                    contenuto = Replace(contenuto, "$motivazione$", par.IfNull(myReaderJ("motivazione"), "____________________________"))

                    idAlloggio.Value = par.IfNull(myReaderJ("id_unita"), -1)


                    If par.IfNull(myReaderJ("esito"), -1) <> -1 Then

                        If par.IfNull(myReaderJ("esito"), -1) = 0 Then

                            contenuto = Replace(contenuto, "$ics$", "X")
                            contenuto = Replace(contenuto, "$ics1$", "")
                            contenuto = Replace(contenuto, "$ics2$", "")


                        End If




                        If par.IfNull(myReaderJ("esito"), -1) = 1 Then


                            contenuto = Replace(contenuto, "$ics1$", "X")
                            contenuto = Replace(contenuto, "$ics2$", "")
                            contenuto = Replace(contenuto, "$ics$", "")

                        End If




                    Else
                        contenuto = Replace(contenuto, "$ics1$", "")
                        contenuto = Replace(contenuto, "$ics2$", "")
                        contenuto = Replace(contenuto, "$ics$", "")



                    End If

                Else

                    contenuto = Replace(contenuto, "$motivazione$", "____________________________")
                    contenuto = Replace(contenuto, "$codice$", "_______________________")
                    contenuto = Replace(contenuto, "$richiedente$", "_______________________")
                    contenuto = Replace(contenuto, "$localita$", "_______________________")
                    contenuto = Replace(contenuto, "$indirizzo$", "_______________________")
                    contenuto = Replace(contenuto, "$numAlloggio$", "___________")
                    contenuto = Replace(contenuto, "$piano$", "_______________________")
                    contenuto = Replace(contenuto, "$superficie$", "___________")
                    contenuto = Replace(contenuto, "$scala$", "___________")
                    contenuto = Replace(contenuto, "$ics1$", "")
                    contenuto = Replace(contenuto, "$ics2$", "")
                    contenuto = Replace(contenuto, "$ics$", "")


                End If
                myReaderJ.Close()

            End If










            par.cmd.CommandText = " SELECT tab_filiali.* FROM siscom_mi.tab_filiali, operatori WHERE tab_filiali.ID = operatori.id_ufficio (+) AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"


            myReaderK = par.cmd.ExecuteReader()
            If myReaderK.Read Then

                contenuto = Replace(contenuto, "$telefonoFil$", par.IfNull(myReaderK("n_telefono"), "___________"))
                contenuto = Replace(contenuto, "$faxFil$", par.IfNull(myReaderK("n_fax"), "___________"))


            Else

                contenuto = Replace(contenuto, "$telefonoFil$", "___________")
                contenuto = Replace(contenuto, "$faxFil$", "___________")
            End If
            myReaderK.Close()



            If txt_proprieta.Value <> "MM S.P.A." Then

                par.cmd.CommandText = "  SELECT tab_filiali.*, indirizzi.descrizione AS descr, indirizzi.civico, indirizzi.cap, indirizzi.localita " _
                                      & " FROM siscom_mi.indirizzi, siscom_mi.tab_filiali, siscom_mi.complessi_immobiliari, siscom_mi.edifici, siscom_mi.unita_immobiliari " _
                                      & " WHERE unita_immobiliari.ID =" & idAlloggio.Value & "" _
                                      & " And indirizzi.ID = tab_filiali.id_indirizzo " _
                                      & " And edifici.ID = unita_immobiliari.id_edificio " _
                                      & " And complessi_immobiliari.ID = edifici.id_complesso " _
                                      & " AND tab_filiali.ID = complessi_immobiliari.id_filiale"


                myReaderK = par.cmd.ExecuteReader()
                If myReaderK.Read Then

                    contenuto = Replace(contenuto, "$filialeAll$", par.IfNull(myReaderK("nome"), "___________"))
                    contenuto = Replace(contenuto, "$telFilialeAll$", par.IfNull(myReaderK("n_telefono"), "___________"))


                Else

                    contenuto = Replace(contenuto, "$filialeAll$", "___________")
                    contenuto = Replace(contenuto, "$telFilialeAll$", "___________")
                End If
                myReaderK.Close()

            Else


                contenuto = Replace(contenuto, "$filialeAll$", "___________")
                contenuto = Replace(contenuto, "$telFilialeAll$", "___________")

            End If





            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))





            ' Dim PercorsoBarCode As String = par.RicavaBarCode(29, idDomanda.Value)
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

            Dim nomefile As String = "A2_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\FileTemp\"))



            'Response.Write("<script>window.open('../FileTemp/" & nomefile & "','Modulo','');</script>")
            Response.Redirect("..\..\ALLEGATI\ABBINAMENTI\" & nomefile, False)




            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
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


    Private Function caricaPag2(ByVal cont As String) As String

        Try


            cont = Replace(cont, "$ics$", "")


            Select Case tipo.Value

                Case "1" 'DOMANDA

                    par.cmd.CommandText = " SELECT domande_bando.pg, (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS intest, domande_bando.id_dichiarazione, unita_immobiliari.interno, " _
                                         & " gestione_alloggi.tipo, gestione_alloggi.cod, t_tipo_gestore.descrizione, gestione_alloggi.sede, t_cond_alloggio.descrizione AS stato, unita_immobiliari.cod_unita_immobiliare, " _
                                         & " (indirizzi.descrizione || ', ' || indirizzi.civico) AS indirizzo, indirizzi.cap, comuni_nazioni.nome AS comune, t_tipo_proprieta.descrizione AS proprieta, " _
                                         & " piani.descrizione AS piano, scale_edifici.descrizione AS scala, alloggi.sup, REL_PRAT_ALL_CCAA_ERP.motivazione, REL_PRAT_ALL_CCAA_ERP.esito " _
                                         & " FROM domande_bando, comp_nucleo, dichiarazioni, alloggi, siscom_mi.unita_immobiliari, gestione_alloggi, t_tipo_gestore, t_cond_alloggio, " _
                                         & " t_tipo_proprieta, comuni_nazioni, siscom_mi.indirizzi, siscom_mi.scale_edifici, siscom_mi.piani, siscom_mi.edifici, REL_PRAT_ALL_CCAA_ERP " _
                                         & " WHERE dichiarazioni.ID = domande_bando.id_dichiarazione " _
                                         & " AND domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione(+) " _
                                         & " And comp_nucleo.progr = domande_bando.progr_componente " _
                                         & " AND alloggi.prenotato = '1' " _
                                         & " And alloggi.id_pratica = domande_bando.ID " _
                                         & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                                         & " And t_tipo_gestore.cod = gestione_alloggi.cod_gestore " _
                                         & " And gestione_alloggi.cod_proprieta = alloggi.proprieta " _
                                         & " AND gestione_alloggi.cod_gestore = alloggi.gestione(+) " _
                                         & " AND alloggi.stato = t_cond_alloggio.cod(+) " _
                                         & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) " _
                                         & " And unita_immobiliari.id_indirizzo = indirizzi.ID " _
                                         & " And unita_immobiliari.id_piano = piani.ID " _
                                         & " And unita_immobiliari.id_scala = scale_edifici.ID " _
                                         & " And unita_immobiliari.id_edificio = edifici.ID " _
                                         & " And comuni_nazioni.cod = indirizzi.cod_comune " _
                                         & " And REL_PRAT_ALL_CCAA_ERP.id_pratica = domande_bando.id " _
                                         & " And REL_PRAT_ALL_CCAA_ERP.id_alloggio = alloggi.id " _
                                         & " And domande_bando.ID=" & idDomanda.Value & ""



                Case "2"  'CAMBI



                    par.cmd.CommandText = " SELECT domande_bando_cambi.pg, (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS intest, domande_bando_cambi.id_dichiarazione, unita_immobiliari.interno, " _
                                         & " gestione_alloggi.tipo, gestione_alloggi.cod, t_tipo_gestore.descrizione, gestione_alloggi.sede, t_cond_alloggio.descrizione AS stato, unita_immobiliari.cod_unita_immobiliare, " _
                                         & " (indirizzi.descrizione || ', ' || indirizzi.civico) AS indirizzo, indirizzi.cap, comuni_nazioni.nome AS comune, t_tipo_proprieta.descrizione AS proprieta, " _
                                         & " piani.descrizione AS piano, scale_edifici.descrizione AS scala, alloggi.sup, REL_PRAT_ALL_CCAA_ERP.motivazione, REL_PRAT_ALL_CCAA_ERP.esito " _
                                         & " FROM domande_bando_cambi, comp_nucleo_cambi, dichiarazioni_cambi, alloggi, siscom_mi.unita_immobiliari, gestione_alloggi, t_tipo_gestore, t_cond_alloggio, " _
                                         & " t_tipo_proprieta, comuni_nazioni, siscom_mi.indirizzi, siscom_mi.scale_edifici, siscom_mi.piani, siscom_mi.edifici, REL_PRAT_ALL_CCAA_ERP " _
                                         & " WHERE dichiarazioni_cambi.ID = domande_bando_cambi.id_dichiarazione " _
                                         & " AND domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione(+) " _
                                         & " And comp_nucleo_cambi.progr = domande_bando_cambi.progr_componente " _
                                         & " AND alloggi.prenotato = '1' " _
                                         & " And alloggi.id_pratica = domande_bando_cambi.ID " _
                                         & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                                         & " And t_tipo_gestore.cod = gestione_alloggi.cod_gestore " _
                                         & " And gestione_alloggi.cod_proprieta = alloggi.proprieta " _
                                         & " AND gestione_alloggi.cod_gestore = alloggi.gestione(+) " _
                                         & " AND alloggi.stato = t_cond_alloggio.cod(+) " _
                                         & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) " _
                                         & " And unita_immobiliari.id_indirizzo = indirizzi.ID " _
                                         & " And unita_immobiliari.id_piano = piani.ID " _
                                         & " And unita_immobiliari.id_scala = scale_edifici.ID " _
                                         & " And unita_immobiliari.id_edificio = edifici.ID " _
                                         & " And comuni_nazioni.cod = indirizzi.cod_comune " _
                                         & " And REL_PRAT_ALL_CCAA_ERP.id_pratica = domande_bando_cambi.id " _
                                         & " And REL_PRAT_ALL_CCAA_ERP.id_alloggio = alloggi.id " _
                                         & " And domande_bando_cambi.ID=" & idDomanda.Value & ""





                Case "3"   'EMERGENZE



                    par.cmd.CommandText = " SELECT domande_bando_vsa.pg, (comp_nucleo_vsa.cognome || ' ' || comp_nucleo_vsa.nome) AS intest, domande_bando_vsa.id_dichiarazione, unita_immobiliari.interno, " _
                                         & " gestione_alloggi.tipo, gestione_alloggi.cod, t_tipo_gestore.descrizione, gestione_alloggi.sede, t_cond_alloggio.descrizione AS stato, unita_immobiliari.cod_unita_immobiliare, " _
                                         & " (indirizzi.descrizione || ', ' || indirizzi.civico) AS indirizzo, indirizzi.cap, comuni_nazioni.nome AS comune, t_tipo_proprieta.descrizione AS proprieta, " _
                                         & " piani.descrizione AS piano, scale_edifici.descrizione AS scala, alloggi.sup, REL_PRAT_ALL_CCAA_ERP.motivazione, REL_PRAT_ALL_CCAA_ERP.esito " _
                                         & " FROM domande_bando_vsa, comp_nucleo_vsa, dichiarazioni_vsa, alloggi, siscom_mi.unita_immobiliari, gestione_alloggi, t_tipo_gestore, t_cond_alloggio, " _
                                         & " t_tipo_proprieta, comuni_nazioni, siscom_mi.indirizzi, siscom_mi.scale_edifici, siscom_mi.piani, siscom_mi.edifici, REL_PRAT_ALL_CCAA_ERP " _
                                         & " WHERE dichiarazioni_vsa.ID = domande_bando_vsa.id_dichiarazione " _
                                         & " AND domande_bando_vsa.id_dichiarazione = comp_nucleo_vsa.id_dichiarazione(+) " _
                                         & " And comp_nucleo_vsa.progr = domande_bando_vsa.progr_componente " _
                                         & " AND alloggi.prenotato = '1' " _
                                         & " And alloggi.id_pratica = domande_bando_vsa.ID " _
                                         & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                                         & " And t_tipo_gestore.cod = gestione_alloggi.cod_gestore " _
                                         & " And gestione_alloggi.cod_proprieta = alloggi.proprieta " _
                                         & " AND gestione_alloggi.cod_gestore = alloggi.gestione(+) " _
                                         & " AND alloggi.stato = t_cond_alloggio.cod(+) " _
                                         & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) " _
                                         & " And unita_immobiliari.id_indirizzo = indirizzi.ID " _
                                         & " And unita_immobiliari.id_piano = piani.ID " _
                                         & " And unita_immobiliari.id_scala = scale_edifici.ID " _
                                         & " And unita_immobiliari.id_edificio = edifici.ID " _
                                         & " And comuni_nazioni.cod = indirizzi.cod_comune " _
                                         & " And REL_PRAT_ALL_CCAA_ERP.id_pratica = domande_bando_vsa.id " _
                                         & " And REL_PRAT_ALL_CCAA_ERP.id_alloggio = alloggi.id " _
                                         & " And domande_bando_vsa.ID=" & idDomanda.Value & ""




            End Select

            Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderJ.Read Then




                cont = Replace(cont, "$codice$", par.IfNull(myReaderJ("cod_unita_immobiliare"), "_______________________"))
                cont = Replace(cont, "$richiedente$", par.IfNull(myReaderJ("intest"), "_______________________"))
                cont = Replace(cont, "$localita$", par.IfNull(myReaderJ("comune"), "_______________________"))
                cont = Replace(cont, "$indirizzo$", par.IfNull(myReaderJ("indirizzo"), "_______________________"))
                cont = Replace(cont, "$numAlloggio$", par.IfNull(myReaderJ("interno"), "___________"))
                cont = Replace(cont, "$piano$", par.IfNull(myReaderJ("piano"), "_______________________"))
                cont = Replace(cont, "$superficie$", par.IfNull(myReaderJ("sup"), "___________"))
                cont = Replace(cont, "$scala$", par.IfNull(myReaderJ("scala"), "___________"))
                cont = Replace(cont, "$motivazione$", par.IfNull(myReaderJ("motivazione"), "____________________________"))
                'contenuto = Replace(contenuto, "$filiale$", par.IfNull(myReaderJ("nome"), "___________"))
                'contenuto = Replace(contenuto, "$telFiliale$", par.IfNull(myReaderJ("n_telefono"), "___________"))




                If par.IfNull(myReaderJ("esito"), -1) = 0 Then
                    cont = Replace(cont, "$ics$", "X")

                End If





            Else

                cont = Replace(cont, "$codice$", "_______________________")
                cont = Replace(cont, "$richiedente$", "_______________________")
                cont = Replace(cont, "$localita$", "_______________________")
                cont = Replace(cont, "$indirizzo$", "_______________________")
                cont = Replace(cont, "$numAlloggio$", "___________")
                cont = Replace(cont, "$piano$", "_______________________")
                cont = Replace(cont, "$superficie$", "___________")
                cont = Replace(cont, "$scala$", "___________")
                cont = Replace(cont, "$motivazione$", "____________________________")
                'contenuto = Replace(contenuto, "$filiale$", "___________")
                'contenuto = Replace(contenuto, "$telFiliale$", "___________")


            End If
            myReaderJ.Close()


            cont = Replace(cont, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))














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


        Return cont


    End Function


    Private Function caricaPag3(ByVal cont As String) As String

        Try

            cont = Replace(cont, "$ics1$", "")
            cont = Replace(cont, "$ics2$", "")


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



                cont = Replace(cont, "$richiedente$", par.IfNull(myReaderJ("intest"), "_______________________"))



                If par.IfNull(myReaderJ("esito"), -1) = 0 Then

                    cont = Replace(cont, "$ics2$", "X")



                End If




                If par.IfNull(myReaderJ("esito"), -1) = 1 Then

                    cont = Replace(cont, "$ics1$", "X")



                End If






            Else


                cont = Replace(cont, "$richiedente$", "_______________________")






            End If
            myReaderJ.Close()


            cont = Replace(cont, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))


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


        Return cont

    End Function


End Class
