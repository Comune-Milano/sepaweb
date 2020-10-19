Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Partial Class ASS_Doc_Offerta_Alloggio_Doc_RapportoOffertaAll
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



            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Doc_RapportoOffertaAll.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()

            sr1.Close()







            Select Case tipo.Value

                Case "1" 'DOMANDA

                    par.cmd.CommandText = " SELECT distinct domande_bando.*, domande_bando.id_dichiarazione as dichiar, comp_nucleo.id as idCompon, " _
                                         & "  TRUNC (domande_bando.isbarc, 2) as isbarc_tr, TRUNC (domande_bando.isbar, 3) as isbar_t, TRUNC (domande_bando.isbarc_r, 3) as isbarc_r_tr, " _
                                         & " TRUNC (domande_bando.disagio_f, 2) as disagio_f_tr, TRUNC (domande_bando.disagio_a, 2) as disagio_a_tr, TRUNC (domande_bando.disagio_e, 2) as disagio_e_tr , " _
                                         & " trunc(DOMANDE_BANDO.reddito_isee,3)as reddito_isee_tr, " _
                                         & " comp_nucleo.*, dichiarazioni.*, (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS intest, " _
                                         & " (t_tipo_indirizzo.descrizione || ' ' || dichiarazioni.ind_res_dnte || ' ' || dichiarazioni.civico_res_dnte) AS indirizzo_intest, " _
                                         & " comuni_nazioni.nome AS localita_intest, comuni_nazioni.cap AS cap_intest, comuni_nazioni.sigla AS siglaloc_intest, bandi_graduatoria_def.posizione, t_tipo_parentela.descrizione as parentela, COMP_PATR_MOB.importo " _
                                         & " FROM domande_bando, comp_nucleo, dichiarazioni, alloggi, " _
                                         & " siscom_mi.unita_immobiliari, t_tipo_indirizzo, siscom_mi.edifici, comuni_nazioni, bandi_graduatoria_def, bandi, COMP_PATR_MOB, t_tipo_parentela " _
                                         & " WHERE dichiarazioni.ID = domande_bando.id_dichiarazione " _
                                         & " AND domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione(+) " _
                                         & " And comp_nucleo.progr = domande_bando.progr_componente " _
                                         & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                                         & " And unita_immobiliari.id_edificio = edifici.ID " _
                                         & " And t_tipo_indirizzo.cod = dichiarazioni.id_tipo_ind_res_dnte " _
                                         & " And dichiarazioni.id_luogo_res_dnte = comuni_nazioni.ID " _
                                         & " and bandi_graduatoria_def.ID_DOMANDA (+)= domande_bando.id " _
                                         & " AND bandi_graduatoria_def.ID_BANDO = bandi.id " _
                                         & " AND t_tipo_parentela.cod= comp_nucleo.grado_parentela " _
                                         & " and COMP_PATR_MOB.id_componente(+)= comp_nucleo.id " _
                                         & " AND domande_bando.ID =" & idDomanda.Value & ""

                    Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then

                        contenuto = Replace(contenuto, "$pg_dom$", par.IfNull(myReaderJ("pg"), "________"))
                        contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReaderJ("intest"), "___________________"))
                        contenuto = Replace(contenuto, "$vIsbarc$", par.IfNull(myReaderJ("isbarc_tr"), "---"))
                        contenuto = Replace(contenuto, "$posGraduatoria$", par.IfNull(myReaderJ("posizione"), "---"))
                        contenuto = Replace(contenuto, "$cognomeIntest$", par.IfNull(myReaderJ("cognome"), "---"))
                        contenuto = Replace(contenuto, "$nomeIntest$", par.IfNull(myReaderJ("nome"), "---"))
                        contenuto = Replace(contenuto, "$dataNascIntest$", par.FormattaData(par.IfNull(myReaderJ("data_nascita"), "---")))
                        idDichiarazione.Value = par.IfNull(myReaderJ("dichiar"), 0)
                        idComponente.Value = par.IfNull(myReaderJ("idCompon"), 0)


                        par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = '" & par.IfNull(myReaderJ("cod_fiscale"), "").ToString.Substring(11, 4) & "'"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myReader2.Read Then
                            contenuto = Replace(contenuto, "$luogoNascitaInt$", par.IfNull(myReader2("NOME"), ""))
                        End If
                        myReader2.Close()

                        contenuto = Replace(contenuto, "$comuneIntest$", par.IfNull(myReaderJ("localita_intest"), "---"))
                        contenuto = Replace(contenuto, "$capIntest$", par.IfNull(myReaderJ("cap_intest"), "---"))
                        contenuto = Replace(contenuto, "$provIntest$", par.IfNull(myReaderJ("siglaloc_intest"), "---"))
                        contenuto = Replace(contenuto, "$numDocIntest$", par.IfNull(myReaderJ("carta_i"), "---"))
                        contenuto = Replace(contenuto, "$dataRilascioIntest$", par.FormattaData(par.IfNull(myReaderJ("carta_i_data"), "---")))
                        contenuto = Replace(contenuto, "$localitaDocIntest$", par.IfNull(myReaderJ("carta_i_rilasciata"), "---"))
                        contenuto = Replace(contenuto, "$telefonoIntest$", par.IfNull(myReaderJ("telefono_dnte"), "---"))
                        contenuto = Replace(contenuto, "$telefonoIntest$", par.IfNull(myReaderJ("n_comp_nucleo"), "---"))
                        'contenuto = Replace(contenuto, "$codiceIntest$", par.IfNull(myReaderJ("codice_intest"), "---"))
                        contenuto = Replace(contenuto, "$indirizzoIntest$", par.IfNull(myReaderJ("indirizzo_intest"), "_______________________"))
                        contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderJ("cap_intest"), "________"))
                        contenuto = Replace(contenuto, "$numComponenti$", par.IfNull(myReaderJ("n_comp_nucleo"), "___________"))
                        contenuto = Replace(contenuto, "$annoRedditi$", par.IfNull(myReaderJ("ANNO_SIT_ECONOMICA"), "---"))
                        contenuto = Replace(contenuto, "$iserp$", par.IfNull(FormatNumber(myReaderJ("ise_erp"), 2), ""))
                        contenuto = Replace(contenuto, "$PSE$", par.IfNull(FormatNumber(myReaderJ("pse"), 2), ""))
                        contenuto = Replace(contenuto, "$ruoloFamiglia$", par.IfNull(myReaderJ("parentela"), "---"))
                        contenuto = Replace(contenuto, "$importoMob$", par.IfNull(myReaderJ("importo"), "---"))
                        contenuto = Replace(contenuto, "$dataPres$", par.FormattaData(par.IfNull(myReaderJ("data_pg"), "---")))

                    Else

                        contenuto = Replace(contenuto, "$pg_dom$", "________")
                        contenuto = Replace(contenuto, "$intestatario$", "___________________")
                        contenuto = Replace(contenuto, "$vIsbarc$", "---")
                        contenuto = Replace(contenuto, "$posGraduatoria$", "---")
                        contenuto = Replace(contenuto, "$cognomeIntest$", "---")
                        contenuto = Replace(contenuto, "$nomeIntest$", "---")
                        contenuto = Replace(contenuto, "$dataNascIntest$", "---")
                        contenuto = Replace(contenuto, "$comuneIntest$", "---")
                        contenuto = Replace(contenuto, "$capIntest$", "---")
                        contenuto = Replace(contenuto, "$provIntest$", "---")
                        contenuto = Replace(contenuto, "$numDocIntest$", "---")
                        contenuto = Replace(contenuto, "$dataRilascioIntest$", "---")
                        contenuto = Replace(contenuto, "$localitaDocIntest$", "---")
                        contenuto = Replace(contenuto, "$telefonoIntest$", "---")
                        contenuto = Replace(contenuto, "$telefonoIntest$", "---")
                        contenuto = Replace(contenuto, "$indirizzoIntest$", "_______________________")
                        contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderJ("cap_intest"), "________"))
                        contenuto = Replace(contenuto, "$numComponenti$", "___________")
                        contenuto = Replace(contenuto, "$annoRedditi$", "---")
                        contenuto = Replace(contenuto, "$iserp$", par.IfNull(FormatNumber(myReaderJ("ise_erp"), 2), ""))
                        contenuto = Replace(contenuto, "$PSE$", "")
                        contenuto = Replace(contenuto, "$ruoloFamiglia$", "---")
                        contenuto = Replace(contenuto, "$importoMob$", "---")
                        contenuto = Replace(contenuto, "$dataPres$", "---")

                    End If
                    myReaderJ.Close()
                    contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString)







                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO.ID) FROM COMP_NUCLEO,dichiarazioni,domande_bando WHERE comp_nucleo.perc_inval>=66 and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.ID=" & idDomanda.Value
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        contenuto = Replace(contenuto, "$compHandicap$", par.IfNull(myReaderJ(0), "0"))
                    Else
                        contenuto = Replace(contenuto, "$compHandicap$", "0")
                    End If
                    myReaderJ.Close()
                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO.ID) FROM COMP_NUCLEO,dichiarazioni,domande_bando WHERE (select MONTHS_BETWEEN(SYSDATE,to_char(TO_DATE(COMP_NUCLEO.DATA_NASCITA,'YYYYmmdd'),'DD/MON/YYYY')) from dual)>=780 and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.ID=" & idDomanda.Value
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        contenuto = Replace(contenuto, "$compAnziano$", par.IfNull(myReaderJ(0), "0"))
                    Else
                        contenuto = Replace(contenuto, "$compAnziano$", "0")
                    End If
                    myReaderJ.Close()

                    par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita1) as pref_localita1, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita2) as pref_localita2, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita3) as pref_localita3, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita4) as pref_localita4, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita5) as pref_localita5, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart1) as pref_quart1, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart2) as pref_quart2,  " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart3) as pref_quart3, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart4) as pref_quart4, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart5) as pref_quart5, " _
                                      & " domande_preferenze.pref_indirizzo1, domande_preferenze.pref_indirizzo2, domande_preferenze.pref_indirizzo3, domande_preferenze.pref_indirizzo4, domande_preferenze.pref_indirizzo5, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita1) as escl_localita1, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita2) as escl_localita2, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita3) as escl_localita3, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita4) as escl_localita4, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita5) as escl_localita5, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart1) as escl_quart1, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart2) as escl_quart2, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart3) as escl_quart3, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart4) as escl_quart4, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart5) as escl_quart5, " _
                                      & " domande_preferenze_esclusioni.escl_indirizzo1, domande_preferenze_esclusioni.escl_indirizzo2, domande_preferenze_esclusioni.escl_indirizzo3, domande_preferenze_esclusioni.escl_indirizzo4, domande_preferenze_esclusioni.escl_indirizzo5, " _
                                      & " domande_preferenze.pref_sup_max, domande_preferenze.pref_sup_min, " _
                                      & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze.pref_piani_da_con) as pref_piani_da_con, " _
                                      & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze.pref_piani_a_con) as pref_piani_a_con, " _
                                      & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze.pref_piani_da_senza) as pref_piani_da_senza, " _
                                      & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze.pref_piani_a_senza) as pref_piani_a_senza, " _
                                             & " domande_preferenze.pref_note AS note, domande_preferenze.pref_barriere AS richieste_particolari, " _
                                             & " domande_preferenze.pref_condominio AS condominio " _
                                             & " FROM domande_preferenze, domande_preferenze_esclusioni" _
                                             & " WHERE domande_preferenze.id_domanda = domande_preferenze_esclusioni.id_domanda " _
                                             & " And domande_preferenze.id_domanda = " & idDomanda.Value & ""


                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        contenuto = Replace(contenuto, "$prefInserite$", "si")
                        contenuto = Replace(contenuto, "$localita1$", par.IfNull(myReaderJ("pref_localita1"), " "))

                        If par.IfNull(myReaderJ("pref_localita1"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart1$", " (" & par.IfNull(myReaderJ("pref_quart1") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart1$", " ")
                        End If
                        contenuto = Replace(contenuto, "$localita2$", par.IfNull(myReaderJ("pref_localita2"), " "))

                        If par.IfNull(myReaderJ("pref_localita2"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart2$", " (" & par.IfNull(myReaderJ("pref_quart2") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart2$", " ")
                        End If

                        contenuto = Replace(contenuto, "$localita3$", par.IfNull(myReaderJ("pref_localita3"), " "))
                        If par.IfNull(myReaderJ("pref_localita3"), "") <> "" Then

                            contenuto = Replace(contenuto, "$quart3$", " (" & par.IfNull(myReaderJ("pref_quart3") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart3$", " ")
                        End If

                        contenuto = Replace(contenuto, "$localita4$", par.IfNull(myReaderJ("pref_localita4"), " "))
                        If par.IfNull(myReaderJ("pref_localita4"), "") <> "" Then

                            contenuto = Replace(contenuto, "$quart4$", " (" & par.IfNull(myReaderJ("pref_quart4") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart4$", " ")
                        End If

                        contenuto = Replace(contenuto, "$localita5$", par.IfNull(myReaderJ("pref_localita5"), " "))
                        If par.IfNull(myReaderJ("pref_localita5"), "") <> "" Then

                            contenuto = Replace(contenuto, "$quart5$", " (" & par.IfNull(myReaderJ("pref_quart5") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart5$", " ")
                        End If




                        contenuto = Replace(contenuto, "$localita1ex$", par.IfNull(myReaderJ("escl_localita1"), " "))

                        If par.IfNull(myReaderJ("escl_localita1"), "") <> "" Then

                            contenuto = Replace(contenuto, "$quart1ex$", " (" & par.IfNull(myReaderJ("escl_quart1") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart1ex$", " ")
                        End If

                        contenuto = Replace(contenuto, "$localita2ex$", par.IfNull(myReaderJ("escl_localita2"), " "))
                        If par.IfNull(myReaderJ("escl_localita2"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart2ex$", " (" & par.IfNull(myReaderJ("escl_quart2") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart2ex$", " ")
                        End If


                        contenuto = Replace(contenuto, "$localita3ex$", par.IfNull(myReaderJ("escl_localita3"), " "))

                        If par.IfNull(myReaderJ("escl_localita3"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart3ex$", " (" & par.IfNull(myReaderJ("escl_quart3") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart3ex$", " ")
                        End If


                        contenuto = Replace(contenuto, "$localita4ex$", par.IfNull(myReaderJ("escl_localita4"), " "))

                        If par.IfNull(myReaderJ("escl_localita4"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart4ex$", " (" & par.IfNull(myReaderJ("escl_quart4") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart4ex$", " ")
                        End If



                        contenuto = Replace(contenuto, "$localita5ex$", par.IfNull(myReaderJ("escl_localita5"), " "))

                        If par.IfNull(myReaderJ("escl_localita5"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart5ex$", " (" & par.IfNull(myReaderJ("escl_quart5") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart5ex$", " ")
                        End If




                        If par.IfNull(myReaderJ("pref_sup_min"), "").ToString <> "" Then

                            contenuto = Replace(contenuto, "$supMin$", par.IfNull(myReaderJ("pref_sup_min") * 100, " "))
                        Else
                            contenuto = Replace(contenuto, "$supMin$", " ")
                        End If


                        If par.IfNull(myReaderJ("pref_sup_max"), "").ToString <> "" Then
                            contenuto = Replace(contenuto, "$supMax$", par.IfNull(myReaderJ("pref_sup_max") * 100, " "))
                        Else
                            contenuto = Replace(contenuto, "$supMax$", " ")
                        End If






                        If par.IfNull(myReaderJ("pref_piani_da_con"), "").ToString <> "" Then

                            contenuto = Replace(contenuto, "$pianoDaCon$", "Da: " & par.IfNull(myReaderJ("pref_piani_da_con") & "", " "))

                            If par.IfNull(myReaderJ("pref_piani_a_con"), "").ToString <> "" Then
                                contenuto = Replace(contenuto, "$pianoACon$", "A: " & par.IfNull(myReaderJ("pref_piani_a_con") & "", " "))
                            Else
                                contenuto = Replace(contenuto, "$pianoACon$", " ")
                            End If

                        Else
                            contenuto = Replace(contenuto, "$pianoACon$", " ")
                            contenuto = Replace(contenuto, "$pianoDaCon$", " ")

                        End If



                        If par.IfNull(myReaderJ("pref_piani_da_senza"), "").ToString <> "" Then

                            contenuto = Replace(contenuto, "$pianoDaSenza$", "Da: " & par.IfNull(myReaderJ("pref_piani_da_senza") & "", " "))

                            If par.IfNull(myReaderJ("pref_piani_a_senza"), "").ToString <> "" Then
                                contenuto = Replace(contenuto, "$pianoASenza$", "A: " & par.IfNull(myReaderJ("pref_piani_a_senza") & "", " "))
                            Else
                                contenuto = Replace(contenuto, "$pianoASenza$", " ")
                            End If

                        Else
                            contenuto = Replace(contenuto, "$pianoASenza$", " ")
                            contenuto = Replace(contenuto, "$pianoDaSenza$", " ")

                        End If












                        contenuto = Replace(contenuto, "$notePref$", par.IfNull(myReaderJ("note"), " "))
                        If par.IfNull(myReaderJ("richieste_particolari"), "") = "1" Then
                            contenuto = Replace(contenuto, "$barrArch$", " Barriere Architettoniche escluse")
                        Else
                            contenuto = Replace(contenuto, "$barrArch$", " ---")
                        End If
                        If par.IfNull(myReaderJ("condominio"), "") = "1" Then
                            contenuto = Replace(contenuto, "$condominio$", " SI")
                        Else
                            contenuto = Replace(contenuto, "$condominio$", " NO")
                        End If

                    Else

                        contenuto = Replace(contenuto, "$prefInserite$", "no")
                        contenuto = Replace(contenuto, "$localita1$", "")
                        contenuto = Replace(contenuto, "$quart1$", "")
                        contenuto = Replace(contenuto, "$localita2$", "")
                        contenuto = Replace(contenuto, "$quart2$", "")
                        contenuto = Replace(contenuto, "$localita3$", "")
                        contenuto = Replace(contenuto, "$quart3$", "")
                        contenuto = Replace(contenuto, "$localita4$", "")
                        contenuto = Replace(contenuto, "$quart4$", "")
                        contenuto = Replace(contenuto, "$localita5$", "")
                        contenuto = Replace(contenuto, "$quart5$", "")
                        contenuto = Replace(contenuto, "$localita1ex$", "")
                        contenuto = Replace(contenuto, "$quart1ex$", "")
                        contenuto = Replace(contenuto, "$localita2ex$", "")
                        contenuto = Replace(contenuto, "$quart2ex$", "")
                        contenuto = Replace(contenuto, "$localita3ex$", "")
                        contenuto = Replace(contenuto, "$quart3ex$", "")
                        contenuto = Replace(contenuto, "$localita4ex$", "")
                        contenuto = Replace(contenuto, "$quart4ex$", "")
                        contenuto = Replace(contenuto, "$localita5ex$", "")
                        contenuto = Replace(contenuto, "$quart5ex$", "")
                        contenuto = Replace(contenuto, "$pianoDaCon$", "")
                        contenuto = Replace(contenuto, "$pianoACon$", "")
                        contenuto = Replace(contenuto, "$pianoDaSenza$", "")
                        contenuto = Replace(contenuto, "$pianoASenza$", "")
                        contenuto = Replace(contenuto, "$condominio$", "")
                        contenuto = Replace(contenuto, "$barrArch$", "")
                        contenuto = Replace(contenuto, "$notePref$", "")
                        contenuto = Replace(contenuto, "$supMax$", " ")
                        contenuto = Replace(contenuto, "$supMin$", " ")

                    End If
                    myReaderJ.Close()

















                    'par.cmd.CommandText = " SELECT unita_immobiliari.interno, indirizzi.descrizione as via, indirizzi.civico, " _
                    '                    & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                    '                    & " indirizzi.cap, comuni_nazioni.nome AS comune, piani.descrizione AS piano, scale_edifici.descrizione AS scala, " _
                    '                    & "  alloggi.sup, unita_immobiliari.cod_unita_immobiliare as codiceUI, alloggi.num_locali,siscom_mi.unita_immobiliari.id as id_UI" _
                    '                    & " FROM domande_bando, comp_nucleo, alloggi, siscom_mi.unita_immobiliari, " _
                    '                    & " comuni_nazioni, siscom_mi.indirizzi, siscom_mi.scale_edifici, siscom_mi.piani, siscom_mi.edifici " _
                    '                    & " WHERE domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione(+) " _
                    '                    & " AND comp_nucleo.progr = domande_bando.progr_componente " _
                    '                    & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                    '                    & " And unita_immobiliari.id_indirizzo = indirizzi.ID " _
                    '                    & " And unita_immobiliari.id_piano = piani.ID " _
                    '                    & " And unita_immobiliari.id_scala = scale_edifici.ID " _
                    '                    & " And unita_immobiliari.id_edificio = edifici.ID " _
                    '                    & " And comuni_nazioni.cod = indirizzi.cod_comune " _
                    '                    & " And domande_bando.ID =" & idDomanda.Value & ""



                    par.cmd.CommandText = " SELECT  unita_immobiliari.ID AS id_UI, alloggi.sup, alloggi.num_locali,alloggi.cod_alloggio as codiceUI, " _
                                       & "(CASE WHEN alloggi.ascensore=1 THEN 'SI' else 'NO' END) AS elevatore, alloggi.comune, " _
                                       & " (   t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo) as via, alloggi.num_civico as civico, alloggi.num_alloggio as interno, alloggi.piano,alloggi.scala " _
                                       & " FROM rel_prat_all_ccaa_erp, alloggi, siscom_mi.unita_immobiliari, t_tipo_indirizzo, domande_bando " _
                                       & " WHERE domande_bando.ID = rel_prat_all_ccaa_erp.id_pratica " _
                                       & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                                       & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) " _
                                       & " And alloggi.ID = rel_prat_all_ccaa_erp.id_alloggio " _
                                       & " And rel_prat_all_ccaa_erp.id_pratica =" & idDomanda.Value & "" _
                                       & " ORDER BY rel_prat_all_ccaa_erp.ultimo DESC"


                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then

                        contenuto = Replace(contenuto, "$codiceUI$", par.IfNull(myReaderJ("codiceUI"), " "))
                        contenuto = Replace(contenuto, "$localitaUIAbb$", par.IfNull(myReaderJ("comune"), " "))
                        contenuto = Replace(contenuto, "$indirizzoUI$", par.IfNull(myReaderJ("via"), " "))
                        contenuto = Replace(contenuto, "$civicoUI$", par.IfNull(myReaderJ("civico"), " "))
                        contenuto = Replace(contenuto, "$numUI$", par.IfNull(myReaderJ("interno"), " "))
                        contenuto = Replace(contenuto, "$supUI$", par.IfNull(myReaderJ("sup") * 100, " "))
                        contenuto = Replace(contenuto, "$nVaniUI$", par.IfNull(myReaderJ("num_locali"), " "))
                        contenuto = Replace(contenuto, "$pianoUI$", par.IfNull(myReaderJ("piano"), " "))
                        contenuto = Replace(contenuto, "$ascensoreUI$", par.IfNull(myReaderJ("elevatore"), " "))
                        idUI.Value = par.IfNull(myReaderJ("id_UI"), 0)
                    Else
                        contenuto = Replace(contenuto, "$codiceUI$", "")
                        contenuto = Replace(contenuto, "$localitaUIAbb$", "")
                        contenuto = Replace(contenuto, "$indirizzoUI$", "")
                        contenuto = Replace(contenuto, "$civicoUI$", "")
                        contenuto = Replace(contenuto, "$numUI$", "")
                        contenuto = Replace(contenuto, "$supUI$", "")
                        contenuto = Replace(contenuto, "$nVaniUI$", "")
                        contenuto = Replace(contenuto, "$pianoUI$", "")
                        contenuto = Replace(contenuto, "$ascensoreUI$", "")


                    End If
                    myReaderJ.Close()









                    par.cmd.CommandText = " select * from siscom_mi.unita_stato_manutentivo where id_unita=" & idUI.Value

                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        If par.IfNull(myReaderJ("riassegnabile"), -1) <> 0 Then
                            contenuto = Replace(contenuto, "$daRistrutturare$", "no")
                        Else
                            contenuto = Replace(contenuto, "$daRistrutturare$", "si")
                        End If
                    Else
                        contenuto = Replace(contenuto, "$daRistrutturare$", "---")
                    End If
                    myReaderJ.Close()







                    par.cmd.CommandText = " select * from REL_PRAT_ALL_CCAA_ERP where id_pratica=" & idDomanda.Value & " AND ULTIMO=1"
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then

                        If par.IfNull(myReaderJ("esito"), -1) = 1 Then
                            contenuto = Replace(contenuto, "$esito$", "esito positivo")

                        End If

                        If par.IfNull(myReaderJ("esito"), -1) = 0 Or par.IfNull(myReaderJ("esito"), -1) = 3 Or par.IfNull(myReaderJ("esito"), -1) = 4 Then
                            contenuto = Replace(contenuto, "$esito$", "esito negativo")

                        End If

                        If par.IfNull(myReaderJ("esito"), -1) = -1 Then
                            contenuto = Replace(contenuto, "$esito$", "---")

                        End If
                        contenuto = Replace(contenuto, "$dataOfferta$", par.FormattaData(par.IfNull(myReaderJ("data_proposta"), " ")))
                        contenuto = Replace(contenuto, "$datarispofferta$", par.FormattaData(par.IfNull(myReaderJ("data"), " ")))
                    Else


                        contenuto = Replace(contenuto, "$esito$", "---")
                        contenuto = Replace(contenuto, "$dataOfferta$", " ")
                        contenuto = Replace(contenuto, "$datarispofferta$", " ")


                    End If
                    myReaderJ.Close()







                   par.cmd.CommandText = " SELECT tab_filiali.* FROM siscom_mi.tab_filiali, operatori WHERE tab_filiali.ID = operatori.id_ufficio (+) AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        contenuto = Replace(contenuto, "$filialeComp$", par.IfNull(myReaderJ("nome"), " "))
                        contenuto = Replace(contenuto, "$ReferenteAmm$", par.IfNull(myReaderJ("ref_amministrativo"), " "))
                        contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderJ("responsabile"), "_________________________"))
                    Else
                        contenuto = Replace(contenuto, "$filialeComp$", " ")
                        contenuto = Replace(contenuto, "$ReferenteAmm$", " ")
                        contenuto = Replace(contenuto, "$responsabile$", "_______________________")
                    End If
                    myReaderJ.Close()



                    '           '--------reddito
          


                    Dim DETRAZIONI_FR As Long
                    Dim TOT_SPESE As Long
                    Dim REDDITO_COMPLESSIVO As Double = 0
                    Dim REDD_DIP As Double = 0
                    Dim REDD_ALT As Double = 0
                    Dim INV_100_CON As Integer
                    Dim INV_100_NO As Integer
                    Dim INV_66_99 As Integer
                    Dim TASSO_RENDIMENTO As Double
                    Dim DETRAZIONI As Long
                    Dim FIGURATIVO_MOBILI As Double
                    Dim ISEE_ERP As Double
                    Dim ISR_ERP As Double


                    par.cmd.CommandText = "SELECT DOMANDE_BANDO.ID AS ID_DOM,DICHIARAZIONI.PG AS PG_DICH,DOMANDE_BANDO.PG AS PG_DOM,T_CAUSALI_DOMANDA.DESCRIZIONE AS CAUSALE_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPOVIA,DICHIARAZIONI.*,DOMANDE_BANDO.*,COMP_NUCLEO.* " _
         & "FROM DICHIARAZIONI,COMP_NUCLEO,DOMANDE_BANDO,T_CAUSALI_DOMANDA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI.ID = COMP_NUCLEO.ID_DICHIARAZIONE " _
         & "AND DICHIARAZIONI.ID = DOMANDE_BANDO.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA.COD AND DICHIARAZIONI.ID = " & idDichiarazione.Value & " AND COMP_NUCLEO.PROGR = 0"

                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read() Then

                        INV_100_CON = par.IfNull(myReaderJ("N_INV_100_CON"), 0)
                        INV_100_NO = par.IfNull(myReaderJ("N_INV_100_SENZA"), 0)
                        INV_66_99 = par.IfNull(myReaderJ("N_INV_100_66"), 0)
                        TASSO_RENDIMENTO = par.RicavaTasso(par.IfNull(myReaderJ("ANNO_SIT_ECONOMICA"), 0))

                    End If
                    myReaderJ.Close()


                    par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO WHERE ID_DICHIARAZIONE= " & idDichiarazione.Value
                    myReaderJ = par.cmd.ExecuteReader()
                    While myReaderJ.Read
                        par.cmd.CommandText = "SELECT * FROM COMP_REDDITO WHERE ID_COMPONENTE=" & idComponente.Value
                        Dim myReaderR1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        While myReaderR1.Read
                            REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderR1("REDDITO_IRPEF"), 0) + par.IfNull(myReaderR1("PROV_AGRARI"), 0)
                        End While
                        myReaderR1.Close()


                        par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & idComponente.Value
                        Dim myReaderR2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        While myReaderR2.Read
                            REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderR2("IMPORTO"), 0)
                        End While
                        myReaderR2.Close()


                        Dim DETRAZIONI_FRAGILE As Double = 0
                        par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & idComponente.Value
                        Dim myReaderDetr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myReaderDetr.HasRows Then
                            While myReaderDetr.Read
                                DETRAZIONI_FRAGILE = DETRAZIONI_FRAGILE + par.IfNull(myReaderDetr("IMPORTO"), 0)
                                TOT_SPESE = TOT_SPESE + par.IfNull(myReaderDetr("IMPORTO"), 0)

                                If DETRAZIONI_FRAGILE > 10000 Then
                                    DETRAZIONI_FR = DETRAZIONI_FR + DETRAZIONI_FRAGILE
                                Else
                                    DETRAZIONI_FR = DETRAZIONI_FR + 10000
                                End If

                            End While
                            myReaderDetr.Close()
                        Else
                            If par.IfNull(myReaderJ("indennita_acc"), 0) = "1" Then
                                DETRAZIONI_FR = DETRAZIONI_FR + 10000
                                TOT_SPESE = TOT_SPESE + 10000
                            End If
                            myReaderDetr.Close()
                        End If

                    End While
                    myReaderJ.Close()

                    par.cmd.CommandText = "select sum(dipendente+non_imponibili+pensione) from DOMANDE_REDDITI where ID_DOMANDA=" & idDomanda.Value
                    Dim myReaderW As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderW.Read Then
                        REDD_DIP = par.IfNull(myReaderW(0), 0)
                    End If
                    myReaderW.Close()

                    par.cmd.CommandText = "select sum(autonomo+dom_ag_fab+occasionali) from DOMANDE_REDDITI where ID_DOMANDA=" & idDomanda.Value
                    myReaderW = par.cmd.ExecuteReader()
                    If myReaderW.Read Then
                        REDD_ALT = par.IfNull(myReaderW(0), 0)
                    End If
                    myReaderW.Close()

                    If REDD_DIP > ((REDD_ALT + REDD_DIP) * 80) / 100 Then
                        contenuto = Replace(contenuto, "$reddPrev$", "DIPENDENTE")
                    Else
                        contenuto = Replace(contenuto, "$reddPrev$", "ALTRO")
                    End If

                    DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)

                    ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
                    If ISEE_ERP < 0 Then
                        ISEE_ERP = 0
                    End If

                    ISR_ERP = ISEE_ERP
                    contenuto = Replace(contenuto, "$reddTot$", par.Converti(Format(REDDITO_COMPLESSIVO, "##,##0.00")))
                    contenuto = Replace(contenuto, "$reddEff$", par.Converti(Format(ISR_ERP, "##,##0.00")))



                    '-----dati da funzione



                    CalcolaCanone("1", "" & idDomanda.Value & "", "" & idUI.Value & "")


                    contenuto = Replace(contenuto, "$IseePrevisto$", Format(CDbl(sISEE), "##,##0.00"))
                    ' contenuto = Replace(contenuto, "$area$", AreaEconomica)
                    If sSOTTOAREA <> "" Then

                        contenuto = Replace(contenuto, "$classe$", sSOTTOAREA)

                    Else
                        contenuto = Replace(contenuto, "$classe$", "Valore non rilevabile: unità non comunale.")

                    End If





                    Select Case AreaEconomica
                        Case 1
                            contenuto = Replace(contenuto, "$area$", "PROTEZIONE")
                        Case 2
                            contenuto = Replace(contenuto, "$area$", "ACCESSO")
                        Case 3
                            contenuto = Replace(contenuto, "$area$", "PERMANENZA")

                        Case 4
                            contenuto = Replace(contenuto, "$area$", "DECADENZA")
                        Case Else
                            contenuto = Replace(contenuto, "$area$", "Valore non rilevabile: unità non comunale.")
                    End Select





                Case "2"  'CAMBI



                    par.cmd.CommandText = " SELECT distinct domande_bando_cambi.*, domande_bando_cambi.id_dichiarazione as dichiar, comp_nucleo_cambi.id as idCompon, " _
                                    & "  TRUNC (domande_bando_cambi.isbarc, 2) as isbarc_tr, TRUNC (domande_bando_cambi.isbar, 3) as isbar_t, TRUNC (domande_bando_cambi.isbarc_r, 3) as isbarc_r_tr, " _
                                    & " TRUNC (domande_bando_cambi.disagio_f, 2) as disagio_f_tr, TRUNC (domande_bando_cambi.disagio_a, 2) as disagio_a_tr, TRUNC (domande_bando_cambi.disagio_e, 2) as disagio_e_tr , " _
                                    & " trunc(DOMANDE_BANDO_cambi.reddito_isee,3)as reddito_isee_tr, " _
                                    & " comp_nucleo_cambi.*, dichiarazioni_cambi.*, (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS intest, " _
                                    & " (t_tipo_indirizzo.descrizione || ' ' || dichiarazioni_cambi.ind_res_dnte || ' ' || dichiarazioni_cambi.civico_res_dnte) AS indirizzo_intest, " _
                                    & " comuni_nazioni.nome AS localita_intest, comuni_nazioni.cap AS cap_intest, comuni_nazioni.sigla AS siglaloc_intest, bandi_graduatoria_def_cambi.posizione, t_tipo_parentela.descrizione as parentela, COMP_PATR_MOB_cambi.importo " _
                                    & " FROM domande_bando_cambi, comp_nucleo_cambi, dichiarazioni_cambi, alloggi, " _
                                    & " siscom_mi.unita_immobiliari, t_tipo_indirizzo, siscom_mi.edifici, comuni_nazioni, bandi_graduatoria_def_cambi, bandi_cambio, COMP_PATR_MOB_cambi, t_tipo_parentela " _
                                    & " WHERE dichiarazioni_cambi.ID = domande_bando_cambi.id_dichiarazione " _
                                    & " AND domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione(+) " _
                                    & " And comp_nucleo_cambi.progr = domande_bando_cambi.progr_componente " _
                                    & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                                    & " And unita_immobiliari.id_edificio = edifici.ID " _
                                    & " And t_tipo_indirizzo.cod = dichiarazioni_cambi.id_tipo_ind_res_dnte " _
                                    & " And dichiarazioni_cambi.id_luogo_res_dnte = comuni_nazioni.ID " _
                                    & " and bandi_graduatoria_def_cambi.ID_DOMANDA (+)= domande_bando_cambi.id " _
                                    & " AND bandi_graduatoria_def_cambi.ID_BANDO = bandi_cambio.id " _
                                    & " AND t_tipo_parentela.cod= comp_nucleo_cambi.grado_parentela " _
                                    & " and COMP_PATR_MOB_cambi.id_componente(+)= comp_nucleo_cambi.id " _
                                    & " AND domande_bando_cambi.ID =" & idDomanda.Value & ""

                    Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then

                        contenuto = Replace(contenuto, "$pg_dom$", par.IfNull(myReaderJ("pg"), "________"))
                        contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReaderJ("intest"), "___________________"))
                        contenuto = Replace(contenuto, "$vIsbarc$", par.IfNull(myReaderJ("isbarc_tr"), "---"))
                        contenuto = Replace(contenuto, "$posGraduatoria$", par.IfNull(myReaderJ("posizione"), "---"))
                        contenuto = Replace(contenuto, "$cognomeIntest$", par.IfNull(myReaderJ("cognome"), "---"))
                        contenuto = Replace(contenuto, "$nomeIntest$", par.IfNull(myReaderJ("nome"), "---"))
                        contenuto = Replace(contenuto, "$dataNascIntest$", par.FormattaData(par.IfNull(myReaderJ("data_nascita"), "---")))
                        idDichiarazione.Value = par.IfNull(myReaderJ("dichiar"), 0)
                        idComponente.Value = par.IfNull(myReaderJ("idCompon"), 0)


                        par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = '" & par.IfNull(myReaderJ("cod_fiscale"), "").ToString.Substring(11, 4) & "'"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myReader2.Read Then
                            contenuto = Replace(contenuto, "$luogoNascitaInt$", par.IfNull(myReader2("NOME"), ""))
                        End If
                        myReader2.Close()

                        contenuto = Replace(contenuto, "$comuneIntest$", par.IfNull(myReaderJ("localita_intest"), "---"))
                        contenuto = Replace(contenuto, "$capIntest$", par.IfNull(myReaderJ("cap_intest"), "---"))
                        contenuto = Replace(contenuto, "$provIntest$", par.IfNull(myReaderJ("siglaloc_intest"), "---"))
                        contenuto = Replace(contenuto, "$numDocIntest$", par.IfNull(myReaderJ("carta_i"), "---"))
                        contenuto = Replace(contenuto, "$dataRilascioIntest$", par.FormattaData(par.IfNull(myReaderJ("carta_i_data"), "---")))
                        contenuto = Replace(contenuto, "$localitaDocIntest$", par.IfNull(myReaderJ("carta_i_rilasciata"), "---"))
                        contenuto = Replace(contenuto, "$telefonoIntest$", par.IfNull(myReaderJ("telefono_dnte"), "---"))
                        contenuto = Replace(contenuto, "$telefonoIntest$", par.IfNull(myReaderJ("n_comp_nucleo"), "---"))
                        'contenuto = Replace(contenuto, "$codiceIntest$", par.IfNull(myReaderJ("codice_intest"), "---"))
                        contenuto = Replace(contenuto, "$indirizzoIntest$", par.IfNull(myReaderJ("indirizzo_intest"), "_______________________"))
                        contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderJ("cap_intest"), "________"))
                        contenuto = Replace(contenuto, "$numComponenti$", par.IfNull(myReaderJ("n_comp_nucleo"), "___________"))
                        contenuto = Replace(contenuto, "$annoRedditi$", par.IfNull(myReaderJ("ANNO_SIT_ECONOMICA"), "---"))
                        contenuto = Replace(contenuto, "$iserp$", par.IfNull(FormatNumber(myReaderJ("ise_erp"), 2), ""))
                        contenuto = Replace(contenuto, "$PSE$", par.IfNull(FormatNumber(myReaderJ("pse"), 2), ""))
                        contenuto = Replace(contenuto, "$ruoloFamiglia$", par.IfNull(myReaderJ("parentela"), "---"))
                        contenuto = Replace(contenuto, "$importoMob$", par.IfNull(myReaderJ("importo"), "---"))
                        contenuto = Replace(contenuto, "$dataPres$", par.FormattaData(par.IfNull(myReaderJ("data_pg"), "---")))

                    Else

                        contenuto = Replace(contenuto, "$pg_dom$", "________")
                        contenuto = Replace(contenuto, "$intestatario$", "___________________")
                        contenuto = Replace(contenuto, "$vIsbarc$", "---")
                        contenuto = Replace(contenuto, "$posGraduatoria$", "---")
                        contenuto = Replace(contenuto, "$cognomeIntest$", "---")
                        contenuto = Replace(contenuto, "$nomeIntest$", "---")
                        contenuto = Replace(contenuto, "$dataNascIntest$", "---")
                        contenuto = Replace(contenuto, "$comuneIntest$", "---")
                        contenuto = Replace(contenuto, "$capIntest$", "---")
                        contenuto = Replace(contenuto, "$provIntest$", "---")
                        contenuto = Replace(contenuto, "$numDocIntest$", "---")
                        contenuto = Replace(contenuto, "$dataRilascioIntest$", "---")
                        contenuto = Replace(contenuto, "$localitaDocIntest$", "---")
                        contenuto = Replace(contenuto, "$telefonoIntest$", "---")
                        contenuto = Replace(contenuto, "$telefonoIntest$", "---")
                        contenuto = Replace(contenuto, "$indirizzoIntest$", "_______________________")
                        contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderJ("cap_intest"), "________"))
                        contenuto = Replace(contenuto, "$numComponenti$", "___________")
                        contenuto = Replace(contenuto, "$annoRedditi$", "---")
                        contenuto = Replace(contenuto, "$iserp$", par.IfNull(FormatNumber(myReaderJ("ise_erp"), 2), ""))
                        contenuto = Replace(contenuto, "$PSE$", "")
                        contenuto = Replace(contenuto, "$ruoloFamiglia$", "---")
                        contenuto = Replace(contenuto, "$importoMob$", "---")
                        contenuto = Replace(contenuto, "$dataPres$", "---")

                    End If
                    myReaderJ.Close()
                    contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString)




                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO_CAMBI.ID) FROM COMP_NUCLEO_CAMBI,dichiarazioni_CAMBI,domande_bando_CAMBI WHERE comp_nucleo_CAMBI.perc_inval>=66 and DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND DOMANDE_BANDO_CAMBI.ID=" & idDomanda.Value
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        contenuto = Replace(contenuto, "$compHandicap$", par.IfNull(myReaderJ(0), "0"))
                    Else
                        contenuto = Replace(contenuto, "$compHandicap$", "0")
                    End If
                    myReaderJ.Close()
                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO_CAMBI.ID) FROM COMP_NUCLEO_CAMBI,dichiarazioni_CAMBI,domande_bando_CAMBI WHERE (select MONTHS_BETWEEN(SYSDATE,to_char(TO_DATE(COMP_NUCLEO_CAMBI.DATA_NASCITA,'YYYYmmdd'),'DD/MON/YYYY')) from dual)>=780 and DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND DOMANDE_BANDO_CAMBI.ID=" & idDomanda.Value
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        contenuto = Replace(contenuto, "$compAnziano$", par.IfNull(myReaderJ(0), "0"))
                    Else
                        contenuto = Replace(contenuto, "$compAnziano$", "0")
                    End If
                    myReaderJ.Close()

                    par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_CAMBI.pref_localita1) as pref_localita1, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_CAMBI.pref_localita2) as pref_localita2, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_CAMBI.pref_localita3) as pref_localita3, " _
                                       & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_CAMBI.pref_localita4) as pref_localita4, " _
                                        & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_CAMBI.pref_localita5) as pref_localita5, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_CAMBI.pref_quart1) as pref_quart1, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_CAMBI.pref_quart2) as pref_quart2,  " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_CAMBI.pref_quart3) as pref_quart3, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_CAMBI.pref_quart4) as pref_quart4, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_CAMBI.pref_quart5) as pref_quart5, " _
                                      & " domande_preferenze_CAMBI.pref_indirizzo1, domande_preferenze_CAMBI.pref_indirizzo2, domande_preferenze_CAMBI.pref_indirizzo3, domande_preferenze_CAMBI.pref_indirizzo4, domande_preferenze_CAMBI.pref_indirizzo5, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_CAMBI.escl_localita1) as escl_localita1, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_CAMBI.escl_localita2) as escl_localita2, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_CAMBI.escl_localita3) as escl_localita3, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_CAMBI.escl_localita4) as escl_localita4, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_CAMBI.escl_localita5) as escl_localita5, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_CAMBI.escl_quart1) as escl_quart1, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_CAMBI.escl_quart2) as escl_quart2, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_CAMBI.escl_quart3) as escl_quart3, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_CAMBI.escl_quart4) as escl_quart4, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_CAMBI.escl_quart5) as escl_quart5, " _
                                      & " domande_preferenze_escl_CAMBI.escl_indirizzo1, domande_preferenze_escl_CAMBI.escl_indirizzo2, domande_preferenze_escl_CAMBI.escl_indirizzo3, domande_preferenze_escl_CAMBI.escl_indirizzo4, domande_preferenze_escl_CAMBI.escl_indirizzo5, " _
                                      & " domande_preferenze_CAMBI.pref_sup_max, domande_preferenze_CAMBI.pref_sup_min, " _
                                      & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_CAMBI.pref_piani_da_con) as pref_piani_da_con, " _
                                      & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_CAMBI.pref_piani_a_con) as pref_piani_a_con, " _
                                      & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_CAMBI.pref_piani_da_senza) as pref_piani_da_senza, " _
                                      & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_CAMBI.pref_piani_a_senza) as pref_piani_a_senza, " _
                                             & " domande_preferenze_CAMBI.pref_note AS note, domande_preferenze_CAMBI.pref_barriere AS richieste_particolari, " _
                                             & " domande_preferenze_CAMBI.pref_condominio AS condominio " _
                                             & " FROM domande_preferenze_CAMBI, domande_preferenze_escl_CAMBI" _
                                             & " WHERE domande_preferenze_CAMBI.id_domanda = domande_preferenze_escl_CAMBI.id_domanda " _
                                             & " And domande_preferenze_CAMBI.id_domanda = " & idDomanda.Value & ""


                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        contenuto = Replace(contenuto, "$prefInserite$", "si")
                        contenuto = Replace(contenuto, "$localita1$", par.IfNull(myReaderJ("pref_localita1"), " "))

                        If par.IfNull(myReaderJ("pref_localita1"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart1$", " (" & par.IfNull(myReaderJ("pref_quart1") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart1$", " ")
                        End If
                        contenuto = Replace(contenuto, "$localita2$", par.IfNull(myReaderJ("pref_localita2"), " "))

                        If par.IfNull(myReaderJ("pref_localita2"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart2$", " (" & par.IfNull(myReaderJ("pref_quart2") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart2$", " ")
                        End If

                        contenuto = Replace(contenuto, "$localita3$", par.IfNull(myReaderJ("pref_localita3"), " "))
                        If par.IfNull(myReaderJ("pref_localita3"), "") <> "" Then

                            contenuto = Replace(contenuto, "$quart3$", " (" & par.IfNull(myReaderJ("pref_quart3") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart3$", " ")
                        End If

                        contenuto = Replace(contenuto, "$localita4$", par.IfNull(myReaderJ("pref_localita4"), " "))
                        If par.IfNull(myReaderJ("pref_localita4"), "") <> "" Then

                            contenuto = Replace(contenuto, "$quart4$", " (" & par.IfNull(myReaderJ("pref_quart4") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart4$", " ")
                        End If

                        contenuto = Replace(contenuto, "$localita5$", par.IfNull(myReaderJ("pref_localita5"), " "))
                        If par.IfNull(myReaderJ("pref_localita5"), "") <> "" Then

                            contenuto = Replace(contenuto, "$quart5$", " (" & par.IfNull(myReaderJ("pref_quart5") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart5$", " ")
                        End If




                        contenuto = Replace(contenuto, "$localita1ex$", par.IfNull(myReaderJ("escl_localita1"), " "))

                        If par.IfNull(myReaderJ("escl_localita1"), "") <> "" Then

                            contenuto = Replace(contenuto, "$quart1ex$", " (" & par.IfNull(myReaderJ("escl_quart1") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart1ex$", " ")
                        End If

                        contenuto = Replace(contenuto, "$localita2ex$", par.IfNull(myReaderJ("escl_localita2"), " "))
                        If par.IfNull(myReaderJ("escl_localita2"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart2ex$", " (" & par.IfNull(myReaderJ("escl_quart2") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart2ex$", " ")
                        End If


                        contenuto = Replace(contenuto, "$localita3ex$", par.IfNull(myReaderJ("escl_localita3"), " "))

                        If par.IfNull(myReaderJ("escl_localita3"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart3ex$", " (" & par.IfNull(myReaderJ("escl_quart3") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart3ex$", " ")
                        End If


                        contenuto = Replace(contenuto, "$localita4ex$", par.IfNull(myReaderJ("escl_localita4"), " "))

                        If par.IfNull(myReaderJ("escl_localita4"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart4ex$", " (" & par.IfNull(myReaderJ("escl_quart4") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart4ex$", " ")
                        End If



                        contenuto = Replace(contenuto, "$localita5ex$", par.IfNull(myReaderJ("escl_localita5"), " "))

                        If par.IfNull(myReaderJ("escl_localita5"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart5ex$", " (" & par.IfNull(myReaderJ("escl_quart5") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart5ex$", " ")
                        End If




                        If par.IfNull(myReaderJ("pref_sup_min"), "").ToString <> "" Then

                            contenuto = Replace(contenuto, "$supMin$", par.IfNull(myReaderJ("pref_sup_min") * 100, " "))
                        Else
                            contenuto = Replace(contenuto, "$supMin$", " ")
                        End If


                        If par.IfNull(myReaderJ("pref_sup_max"), "").ToString <> "" Then
                            contenuto = Replace(contenuto, "$supMax$", par.IfNull(myReaderJ("pref_sup_max") * 100, " "))
                        Else
                            contenuto = Replace(contenuto, "$supMax$", " ")
                        End If





                        If par.IfNull(myReaderJ("pref_piani_da_con"), "").ToString <> "" Then

                            contenuto = Replace(contenuto, "$pianoDaCon$", "Da: " & par.IfNull(myReaderJ("pref_piani_da_con") & "", " "))

                            If par.IfNull(myReaderJ("pref_piani_a_con"), "").ToString <> "" Then
                                contenuto = Replace(contenuto, "$pianoACon$", "A: " & par.IfNull(myReaderJ("pref_piani_a_con") & "", " "))
                            Else
                                contenuto = Replace(contenuto, "$pianoACon$", " ")
                            End If

                        Else
                            contenuto = Replace(contenuto, "$pianoACon$", " ")
                            contenuto = Replace(contenuto, "$pianoDaCon$", " ")

                        End If



                        If par.IfNull(myReaderJ("pref_piani_da_senza"), "").ToString <> "" Then

                            contenuto = Replace(contenuto, "$pianoDaSenza$", "Da: " & par.IfNull(myReaderJ("pref_piani_da_senza") & "", " "))

                            If par.IfNull(myReaderJ("pref_piani_a_senza"), "").ToString <> "" Then
                                contenuto = Replace(contenuto, "$pianoASenza$", "A: " & par.IfNull(myReaderJ("pref_piani_a_senza") & "", " "))
                            Else
                                contenuto = Replace(contenuto, "$pianoASenza$", " ")
                            End If

                        Else
                            contenuto = Replace(contenuto, "$pianoASenza$", " ")
                            contenuto = Replace(contenuto, "$pianoDaSenza$", " ")

                        End If












                            contenuto = Replace(contenuto, "$notePref$", par.IfNull(myReaderJ("note"), " "))
                            If par.IfNull(myReaderJ("richieste_particolari"), "") = "1" Then
                                contenuto = Replace(contenuto, "$barrArch$", " Barriere Architettoniche escluse")
                            Else
                                contenuto = Replace(contenuto, "$barrArch$", " ---")
                            End If
                            If par.IfNull(myReaderJ("condominio"), "") = "1" Then
                                contenuto = Replace(contenuto, "$condominio$", " SI")
                            Else
                                contenuto = Replace(contenuto, "$condominio$", " NO")
                            End If

                        Else

                            contenuto = Replace(contenuto, "$prefInserite$", "no")
                            contenuto = Replace(contenuto, "$localita1$", "")
                            contenuto = Replace(contenuto, "$quart1$", "")
                            contenuto = Replace(contenuto, "$localita2$", "")
                            contenuto = Replace(contenuto, "$quart2$", "")
                            contenuto = Replace(contenuto, "$localita3$", "")
                        contenuto = Replace(contenuto, "$quart3$", "")
                        contenuto = Replace(contenuto, "$localita4$", "")
                        contenuto = Replace(contenuto, "$quart4$", "")
                        contenuto = Replace(contenuto, "$localita5$", "")
                        contenuto = Replace(contenuto, "$quart5$", "")
                            contenuto = Replace(contenuto, "$localita1ex$", "")
                            contenuto = Replace(contenuto, "$quart1ex$", "")
                            contenuto = Replace(contenuto, "$localita2ex$", "")
                            contenuto = Replace(contenuto, "$quart2ex$", "")
                            contenuto = Replace(contenuto, "$localita3ex$", "")
                        contenuto = Replace(contenuto, "$quart3ex$", "")
                        contenuto = Replace(contenuto, "$localita4ex$", "")
                        contenuto = Replace(contenuto, "$quart4ex$", "")
                        contenuto = Replace(contenuto, "$localita5ex$", "")
                        contenuto = Replace(contenuto, "$quart5ex$", "")
                        contenuto = Replace(contenuto, "$pianoDaCon$", "")
                        contenuto = Replace(contenuto, "$pianoACon$", "")
                        contenuto = Replace(contenuto, "$pianoDaSenza$", "")
                        contenuto = Replace(contenuto, "$pianoASenza$", "")
                            contenuto = Replace(contenuto, "$condominio$", "")
                            contenuto = Replace(contenuto, "$barrArch$", "")
                            contenuto = Replace(contenuto, "$notePref$", "")
                            contenuto = Replace(contenuto, "$supMax$", " ")
                            contenuto = Replace(contenuto, "$supMin$", " ")

                        End If
                        myReaderJ.Close()






                        par.cmd.CommandText = " SELECT  unita_immobiliari.ID AS id_UI, alloggi.sup, alloggi.num_locali,alloggi.cod_alloggio as codiceUI, " _
                                             & "(CASE WHEN alloggi.ascensore=1 THEN 'SI' else 'NO' END) AS elevatore, alloggi.comune, " _
                                             & " (   t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo) as via, alloggi.num_civico as civico,alloggi.num_alloggio as interno ,alloggi.piano,alloggi.scala " _
                                             & " FROM rel_prat_all_ccaa_erp, alloggi, siscom_mi.unita_immobiliari, t_tipo_indirizzo, domande_bando_cambi " _
                                             & " WHERE domande_bando_cambi.ID = rel_prat_all_ccaa_erp.id_pratica " _
                                             & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                                             & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) " _
                                             & " And alloggi.ID = rel_prat_all_ccaa_erp.id_alloggio " _
                                             & " And rel_prat_all_ccaa_erp.id_pratica =" & idDomanda.Value & "" _
                                             & " ORDER BY rel_prat_all_ccaa_erp.ultimo DESC"




                        myReaderJ = par.cmd.ExecuteReader()
                        If myReaderJ.Read Then

                            contenuto = Replace(contenuto, "$codiceUI$", par.IfNull(myReaderJ("codiceUI"), " "))
                            contenuto = Replace(contenuto, "$localitaUIAbb$", par.IfNull(myReaderJ("comune"), " "))
                            contenuto = Replace(contenuto, "$indirizzoUI$", par.IfNull(myReaderJ("via"), " "))
                            contenuto = Replace(contenuto, "$civicoUI$", par.IfNull(myReaderJ("civico"), " "))
                            contenuto = Replace(contenuto, "$numUI$", par.IfNull(myReaderJ("interno"), " "))
                            contenuto = Replace(contenuto, "$supUI$", par.IfNull(myReaderJ("sup") * 100, " "))
                            contenuto = Replace(contenuto, "$nVaniUI$", par.IfNull(myReaderJ("num_locali"), " "))
                            contenuto = Replace(contenuto, "$pianoUI$", par.IfNull(myReaderJ("piano"), " "))
                            contenuto = Replace(contenuto, "$ascensoreUI$", par.IfNull(myReaderJ("elevatore"), " "))
                            idUI.Value = par.IfNull(myReaderJ("id_UI"), 0)
                        Else
                            contenuto = Replace(contenuto, "$codiceUI$", "")
                            contenuto = Replace(contenuto, "$localitaUIAbb$", "")
                            contenuto = Replace(contenuto, "$indirizzoUI$", "")
                            contenuto = Replace(contenuto, "$civicoUI$", "")
                            contenuto = Replace(contenuto, "$numUI$", "")
                            contenuto = Replace(contenuto, "$supUI$", "")
                            contenuto = Replace(contenuto, "$nVaniUI$", "")
                            contenuto = Replace(contenuto, "$pianoUI$", "")
                            contenuto = Replace(contenuto, "$ascensoreUI$", "")


                        End If
                        myReaderJ.Close()









                        par.cmd.CommandText = " select * from siscom_mi.unita_stato_manutentivo where id_unita=" & idUI.Value

                        myReaderJ = par.cmd.ExecuteReader()
                        If myReaderJ.Read Then
                            If par.IfNull(myReaderJ("riassegnabile"), -1) <> 0 Then
                                contenuto = Replace(contenuto, "$daRistrutturare$", "no")
                            Else
                                contenuto = Replace(contenuto, "$daRistrutturare$", "si")
                            End If
                        Else
                            contenuto = Replace(contenuto, "$daRistrutturare$", "---")
                        End If
                        myReaderJ.Close()







                        par.cmd.CommandText = " select * from REL_PRAT_ALL_CCAA_ERP where id_pratica=" & idDomanda.Value & " AND ULTIMO=1"
                        myReaderJ = par.cmd.ExecuteReader()
                        If myReaderJ.Read Then

                            If par.IfNull(myReaderJ("esito"), -1) = 1 Then
                                contenuto = Replace(contenuto, "$esito$", "esito positivo")

                            End If

                            If par.IfNull(myReaderJ("esito"), -1) = 0 Or par.IfNull(myReaderJ("esito"), -1) = 3 Or par.IfNull(myReaderJ("esito"), -1) = 4 Then
                                contenuto = Replace(contenuto, "$esito$", "esito negativo")

                            End If

                            If par.IfNull(myReaderJ("esito"), -1) = -1 Then
                                contenuto = Replace(contenuto, "$esito$", "---")

                            End If
                            contenuto = Replace(contenuto, "$dataOfferta$", par.FormattaData(par.IfNull(myReaderJ("data_proposta"), " ")))
                            contenuto = Replace(contenuto, "$datarispofferta$", par.FormattaData(par.IfNull(myReaderJ("data"), " ")))
                        Else


                            contenuto = Replace(contenuto, "$esito$", "---")
                            contenuto = Replace(contenuto, "$dataOfferta$", " ")
                            contenuto = Replace(contenuto, "$datarispofferta$", " ")


                        End If
                        myReaderJ.Close()







                        par.cmd.CommandText = " SELECT tab_filiali.* FROM siscom_mi.tab_filiali, operatori WHERE tab_filiali.ID = operatori.id_ufficio (+) AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"
                        myReaderJ = par.cmd.ExecuteReader()
                        If myReaderJ.Read Then
                            contenuto = Replace(contenuto, "$filialeComp$", par.IfNull(myReaderJ("nome"), " "))
                            contenuto = Replace(contenuto, "$ReferenteAmm$", par.IfNull(myReaderJ("ref_amministrativo"), " "))
                            contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderJ("responsabile"), "_________________________"))
                        Else
                            contenuto = Replace(contenuto, "$filialeComp$", " ")
                            contenuto = Replace(contenuto, "$ReferenteAmm$", " ")
                            contenuto = Replace(contenuto, "$responsabile$", "_______________________")
                        End If
                        myReaderJ.Close()



                        '           '--------reddito



                        Dim DETRAZIONI_FR As Long
                        Dim TOT_SPESE As Long
                        Dim REDDITO_COMPLESSIVO As Double = 0
                        Dim REDD_DIP As Double = 0
                        Dim REDD_ALT As Double = 0
                        Dim INV_100_CON As Integer
                        Dim INV_100_NO As Integer
                        Dim INV_66_99 As Integer
                        Dim TASSO_RENDIMENTO As Double
                        Dim DETRAZIONI As Long
                        Dim FIGURATIVO_MOBILI As Double
                        Dim ISEE_ERP As Double
                        Dim ISR_ERP As Double


                        par.cmd.CommandText = " SELECT DOMANDE_BANDO_CAMBI.ID AS ID_DOM,DICHIARAZIONI_CAMBI.PG AS PG_DICH,DOMANDE_BANDO_CAMBI.PG AS PG_DOM, " _
                                              & " T_CAUSALI_DOMANDA.DESCRIZIONE AS CAUSALE_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPOVIA, " _
                                              & " DICHIARAZIONI_CAMBI.*,DOMANDE_BANDO_CAMBI.*,COMP_NUCLEO_CAMBI.* " _
                                              & " FROM DICHIARAZIONI_CAMBI,COMP_NUCLEO_CAMBI,DOMANDE_BANDO_CAMBI,T_CAUSALI_DOMANDA,T_TIPO_INDIRIZZO " _
                                              & " WHERE DICHIARAZIONI_CAMBI.ID = COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE " _
                                              & " AND DICHIARAZIONI_CAMBI.ID = DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE " _
                                              & " AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_CAMBI.ID_TIPO_IND_RES_DNTE " _
                                              & " AND DOMANDE_BANDO_CAMBI.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA.COD " _
                                              & " AND DICHIARAZIONI_CAMBI.ID = " & idDichiarazione.Value & " AND COMP_NUCLEO_CAMBI.PROGR = 0"

                        myReaderJ = par.cmd.ExecuteReader()
                        If myReaderJ.Read() Then

                            INV_100_CON = par.IfNull(myReaderJ("N_INV_100_CON"), 0)
                            INV_100_NO = par.IfNull(myReaderJ("N_INV_100_SENZA"), 0)
                            INV_66_99 = par.IfNull(myReaderJ("N_INV_100_66"), 0)
                            TASSO_RENDIMENTO = par.RicavaTasso(par.IfNull(myReaderJ("ANNO_SIT_ECONOMICA"), 0))

                        End If
                        myReaderJ.Close()


                        par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_CAMBI WHERE ID_DICHIARAZIONE= " & idDichiarazione.Value
                        myReaderJ = par.cmd.ExecuteReader()
                        While myReaderJ.Read
                            par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_CAMBI WHERE ID_COMPONENTE=" & idComponente.Value
                            Dim myReaderR1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            While myReaderR1.Read
                                REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderR1("REDDITO_IRPEF"), 0) + par.IfNull(myReaderR1("PROV_AGRARI"), 0)
                            End While
                            myReaderR1.Close()


                            par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_CAMBI WHERE ID_COMPONENTE=" & idComponente.Value
                            Dim myReaderR2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            While myReaderR2.Read
                                REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderR2("IMPORTO"), 0)
                            End While
                            myReaderR2.Close()


                            Dim DETRAZIONI_FRAGILE As Double = 0
                            par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE_CAMBI WHERE ID_COMPONENTE=" & idComponente.Value
                            Dim myReaderDetr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If myReaderDetr.HasRows Then
                                While myReaderDetr.Read
                                    DETRAZIONI_FRAGILE = DETRAZIONI_FRAGILE + par.IfNull(myReaderDetr("IMPORTO"), 0)
                                    TOT_SPESE = TOT_SPESE + par.IfNull(myReaderDetr("IMPORTO"), 0)

                                    If DETRAZIONI_FRAGILE > 10000 Then
                                        DETRAZIONI_FR = DETRAZIONI_FR + DETRAZIONI_FRAGILE
                                    Else
                                        DETRAZIONI_FR = DETRAZIONI_FR + 10000
                                    End If

                                End While
                                myReaderDetr.Close()
                            Else
                                If par.IfNull(myReaderJ("indennita_acc"), 0) = "1" Then
                                    DETRAZIONI_FR = DETRAZIONI_FR + 10000
                                    TOT_SPESE = TOT_SPESE + 10000
                                End If
                                myReaderDetr.Close()
                            End If

                        End While
                        myReaderJ.Close()

                        par.cmd.CommandText = "select sum(dipendente+non_imponibili+pensione) from DOMANDE_REDDITI_CAMBI where ID_DOMANDA=" & idDomanda.Value
                        Dim myReaderW As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderW.Read Then
                            REDD_DIP = par.IfNull(myReaderW(0), 0)
                        End If
                        myReaderW.Close()

                        par.cmd.CommandText = "select sum(autonomo+dom_ag_fab+occasionali) from DOMANDE_REDDITI_CAMBI where ID_DOMANDA=" & idDomanda.Value
                        myReaderW = par.cmd.ExecuteReader()
                        If myReaderW.Read Then
                            REDD_ALT = par.IfNull(myReaderW(0), 0)
                        End If
                        myReaderW.Close()

                        If REDD_DIP > ((REDD_ALT + REDD_DIP) * 80) / 100 Then
                            contenuto = Replace(contenuto, "$reddPrev$", "DIPENDENTE")
                        Else
                            contenuto = Replace(contenuto, "$reddPrev$", "ALTRO")
                        End If

                        DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)

                        ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
                        If ISEE_ERP < 0 Then
                            ISEE_ERP = 0
                        End If

                        ISR_ERP = ISEE_ERP
                        contenuto = Replace(contenuto, "$reddTot$", par.Converti(Format(REDDITO_COMPLESSIVO, "##,##0.00")))
                        contenuto = Replace(contenuto, "$reddEff$", par.Converti(Format(ISR_ERP, "##,##0.00")))



                        '-----dati da funzione


                        'par.cmd.CommandText = " SELECT  unita_immobiliari.ID AS id_UI, alloggi.sup, alloggi.num_locali,alloggi.cod_alloggio as codiceUI, " _
                        '               & "(CASE WHEN alloggi.ascensore=1 THEN 'SI' else 'NO' END) AS elevatore, alloggi.comune, " _
                        '               & " (   t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo) as via, alloggi.num_civico as civico,alloggi.num_alloggio as interno ,alloggi.piano,alloggi.scala " _
                        '               & " FROM rel_prat_all_ccaa_erp, alloggi, siscom_mi.unita_immobiliari, t_tipo_indirizzo, domande_bando_cambi " _
                        '               & " WHERE domande_bando_cambi.ID = rel_prat_all_ccaa_erp.id_pratica " _
                        '               & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                        '               & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) " _
                        '               & " And alloggi.ID = rel_prat_all_ccaa_erp.id_alloggio " _
                        '               & " And rel_prat_all_ccaa_erp.id_pratica =" & idDomanda.Value & "" _
                        '               & " ORDER BY rel_prat_all_ccaa_erp.ultimo DESC"









                        CalcolaCanone("2", "" & idDomanda.Value & "", "" & idUI.Value & "")


                        contenuto = Replace(contenuto, "$IseePrevisto$", Format(CDbl(sISEE), "##,##0.00"))
                        ' contenuto = Replace(contenuto, "$area$", AreaEconomica)

                        If sSOTTOAREA <> "" Then

                            contenuto = Replace(contenuto, "$classe$", sSOTTOAREA)

                        Else
                            contenuto = Replace(contenuto, "$classe$", "Valore non rilevabile: unità non comunale.")

                        End If





                        Select Case AreaEconomica
                            Case 1
                                contenuto = Replace(contenuto, "$area$", "PROTEZIONE")
                            Case 2
                                contenuto = Replace(contenuto, "$area$", "ACCESSO")
                            Case 3
                                contenuto = Replace(contenuto, "$area$", "PERMANENZA")

                            Case 4
                                contenuto = Replace(contenuto, "$area$", "DECADENZA")
                            Case Else
                                contenuto = Replace(contenuto, "$area$", "Valore non rilevabile: unità non comunale.")
                        End Select





                Case "3"   'EMERGENZE



                    par.cmd.CommandText = " SELECT distinct domande_bando_VSA.*, domande_bando_VSA.id_dichiarazione as dichiar, comp_nucleo_VSA.id as idCompon, " _
                                 & "  TRUNC (domande_bando_VSA.isbarc, 2) as isbarc_tr, TRUNC (domande_bando_VSA.isbar, 3) as isbar_t, TRUNC (domande_bando_VSA.isbarc_r, 3) as isbarc_r_tr, " _
                                 & " TRUNC (domande_bando_VSA.disagio_f, 2) as disagio_f_tr, TRUNC (domande_bando_VSA.disagio_a, 2) as disagio_a_tr, TRUNC (domande_bando_VSA.disagio_e, 2) as disagio_e_tr , " _
                                 & " trunc(DOMANDE_BANDO_VSA.reddito_isee,3)as reddito_isee_tr, " _
                                 & " comp_nucleo_VSA.*, dichiarazioni_VSA.*, (comp_nucleo_VSA.cognome || ' ' || comp_nucleo_VSA.nome) AS intest, " _
                                 & " (t_tipo_indirizzo.descrizione || ' ' || dichiarazioni_VSA.ind_res_dnte || ' ' || dichiarazioni_VSA.civico_res_dnte) AS indirizzo_intest, " _
                                 & " comuni_nazioni.nome AS localita_intest, comuni_nazioni.cap AS cap_intest, comuni_nazioni.sigla AS siglaloc_intest, t_tipo_parentela.descrizione as parentela, COMP_PATR_MOB_VSA.importo " _
                                 & " FROM domande_bando_VSA, comp_nucleo_VSA, dichiarazioni_VSA, alloggi, " _
                                 & " siscom_mi.unita_immobiliari, t_tipo_indirizzo, siscom_mi.edifici, comuni_nazioni, bandi_VSA, COMP_PATR_MOB_VSA, t_tipo_parentela " _
                                 & " WHERE dichiarazioni_VSA.ID = domande_bando_VSA.id_dichiarazione " _
                                 & " AND domande_bando_VSA.id_dichiarazione = comp_nucleo_VSA.id_dichiarazione(+) " _
                                 & " And comp_nucleo_VSA.progr = domande_bando_VSA.progr_componente " _
                                 & " And unita_immobiliari.cod_unita_immobiliare = alloggi.cod_alloggio " _
                                 & " And unita_immobiliari.id_edificio = edifici.ID " _
                                 & " And t_tipo_indirizzo.cod = dichiarazioni_VSA.id_tipo_ind_res_dnte " _
                                 & " And dichiarazioni_VSA.id_luogo_res_dnte = comuni_nazioni.ID " _
                                 & " AND t_tipo_parentela.cod= comp_nucleo_VSA.grado_parentela " _
                                 & " and COMP_PATR_MOB_VSA.id_componente(+)= comp_nucleo_VSA.id " _
                                 & " AND domande_bando_VSA.ID =" & idDomanda.Value & ""

                    Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then

                        contenuto = Replace(contenuto, "$pg_dom$", par.IfNull(myReaderJ("pg"), "________"))
                        contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReaderJ("intest"), "___________________"))
                        contenuto = Replace(contenuto, "$vIsbarc$", par.IfNull(myReaderJ("isbarc_tr"), "---"))
                        contenuto = Replace(contenuto, "$posGraduatoria$", "---")
                        contenuto = Replace(contenuto, "$cognomeIntest$", par.IfNull(myReaderJ("cognome"), "---"))
                        contenuto = Replace(contenuto, "$nomeIntest$", par.IfNull(myReaderJ("nome"), "---"))
                        contenuto = Replace(contenuto, "$dataNascIntest$", par.FormattaData(par.IfNull(myReaderJ("data_nascita"), "---")))
                        idDichiarazione.Value = par.IfNull(myReaderJ("dichiar"), 0)
                        idComponente.Value = par.IfNull(myReaderJ("idCompon"), 0)


                        par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = '" & par.IfNull(myReaderJ("cod_fiscale"), "").ToString.Substring(11, 4) & "'"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myReader2.Read Then
                            contenuto = Replace(contenuto, "$luogoNascitaInt$", par.IfNull(myReader2("NOME"), ""))
                        End If
                        myReader2.Close()

                        contenuto = Replace(contenuto, "$comuneIntest$", par.IfNull(myReaderJ("localita_intest"), "---"))
                        contenuto = Replace(contenuto, "$capIntest$", par.IfNull(myReaderJ("cap_intest"), "---"))
                        contenuto = Replace(contenuto, "$provIntest$", par.IfNull(myReaderJ("siglaloc_intest"), "---"))
                        contenuto = Replace(contenuto, "$numDocIntest$", par.IfNull(myReaderJ("carta_i"), "---"))
                        contenuto = Replace(contenuto, "$dataRilascioIntest$", par.FormattaData(par.IfNull(myReaderJ("carta_i_data"), "---")))
                        contenuto = Replace(contenuto, "$localitaDocIntest$", par.IfNull(myReaderJ("carta_i_rilasciata"), "---"))
                        contenuto = Replace(contenuto, "$telefonoIntest$", par.IfNull(myReaderJ("telefono_dnte"), "---"))
                        contenuto = Replace(contenuto, "$telefonoIntest$", par.IfNull(myReaderJ("n_comp_nucleo"), "---"))
                        'contenuto = Replace(contenuto, "$codiceIntest$", par.IfNull(myReaderJ("codice_intest"), "---"))
                        contenuto = Replace(contenuto, "$indirizzoIntest$", par.IfNull(myReaderJ("indirizzo_intest"), "_______________________"))
                        contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderJ("cap_intest"), "________"))
                        contenuto = Replace(contenuto, "$numComponenti$", par.IfNull(myReaderJ("n_comp_nucleo"), "___________"))
                        contenuto = Replace(contenuto, "$annoRedditi$", par.IfNull(myReaderJ("ANNO_SIT_ECONOMICA"), "---"))
                        contenuto = Replace(contenuto, "$iserp$", par.IfNull(FormatNumber(myReaderJ("ise_erp"), 2), ""))
                        contenuto = Replace(contenuto, "$PSE$", par.IfNull(FormatNumber(myReaderJ("pse"), 2), ""))
                        contenuto = Replace(contenuto, "$ruoloFamiglia$", par.IfNull(myReaderJ("parentela"), "---"))
                        contenuto = Replace(contenuto, "$importoMob$", par.IfNull(myReaderJ("importo"), "---"))
                        contenuto = Replace(contenuto, "$dataPres$", par.FormattaData(par.IfNull(myReaderJ("data_pg"), "---")))

                    Else

                        contenuto = Replace(contenuto, "$pg_dom$", "________")
                        contenuto = Replace(contenuto, "$intestatario$", "___________________")
                        contenuto = Replace(contenuto, "$vIsbarc$", "---")
                        contenuto = Replace(contenuto, "$posGraduatoria$", "---")
                        contenuto = Replace(contenuto, "$cognomeIntest$", "---")
                        contenuto = Replace(contenuto, "$nomeIntest$", "---")
                        contenuto = Replace(contenuto, "$dataNascIntest$", "---")
                        contenuto = Replace(contenuto, "$comuneIntest$", "---")
                        contenuto = Replace(contenuto, "$capIntest$", "---")
                        contenuto = Replace(contenuto, "$provIntest$", "---")
                        contenuto = Replace(contenuto, "$numDocIntest$", "---")
                        contenuto = Replace(contenuto, "$dataRilascioIntest$", "---")
                        contenuto = Replace(contenuto, "$localitaDocIntest$", "---")
                        contenuto = Replace(contenuto, "$telefonoIntest$", "---")
                        contenuto = Replace(contenuto, "$telefonoIntest$", "---")
                        contenuto = Replace(contenuto, "$indirizzoIntest$", "_______________________")
                        contenuto = Replace(contenuto, "$cap$", par.IfNull(myReaderJ("cap_intest"), "________"))
                        contenuto = Replace(contenuto, "$numComponenti$", "___________")
                        contenuto = Replace(contenuto, "$annoRedditi$", "---")
                        contenuto = Replace(contenuto, "$iserp$", par.IfNull(FormatNumber(myReaderJ("ise_erp"), 2), ""))
                        contenuto = Replace(contenuto, "$PSE$", "")
                        contenuto = Replace(contenuto, "$ruoloFamiglia$", "---")
                        contenuto = Replace(contenuto, "$importoMob$", "---")
                        contenuto = Replace(contenuto, "$dataPres$", "---")

                    End If
                    myReaderJ.Close()
                    contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString)


                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO_vsa.ID) FROM COMP_NUCLEO_vsa,dichiarazioni_vsa,domande_bando_vsa WHERE comp_nucleo_vsa.perc_inval>=66 and DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND DOMANDE_BANDO_vsa.ID=" & idDomanda.Value
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        contenuto = Replace(contenuto, "$compHandicap$", par.IfNull(myReaderJ(0), "0"))
                    Else
                        contenuto = Replace(contenuto, "$compHandicap$", "0")
                    End If
                    myReaderJ.Close()
                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO_vsa.ID) FROM COMP_NUCLEO_vsa,dichiarazioni_vsa,domande_bando_vsa WHERE (select MONTHS_BETWEEN(SYSDATE,to_char(TO_DATE(COMP_NUCLEO_vsa.DATA_NASCITA,'YYYYmmdd'),'DD/MON/YYYY')) from dual)>=780 and DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND DOMANDE_BANDO_vsa.ID=" & idDomanda.Value
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        contenuto = Replace(contenuto, "$compAnziano$", par.IfNull(myReaderJ(0), "0"))
                    Else
                        contenuto = Replace(contenuto, "$compAnziano$", "0")
                    End If
                    myReaderJ.Close()

                    par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_VSA.pref_localita1) as pref_localita1, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_VSA.pref_localita2) as pref_localita2, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_VSA.pref_localita3) as pref_localita3, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_VSA.pref_localita4) as pref_localita4, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_VSA.pref_localita5) as pref_localita5, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_VSA.pref_quart1) as pref_quart1, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_VSA.pref_quart2) as pref_quart2,  " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_VSA.pref_quart3) as pref_quart3, " _
                                       & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_VSA.pref_quart4) as pref_quart4, " _
                                        & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_VSA.pref_quart5) as pref_quart5, " _
                                      & " domande_preferenze_VSA.pref_indirizzo1, domande_preferenze_VSA.pref_indirizzo2, domande_preferenze_VSA.pref_indirizzo3, domande_preferenze_VSA.pref_indirizzo4, domande_preferenze_VSA.pref_indirizzo5, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_VSA.escl_localita1) as escl_localita1, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_VSA.escl_localita2) as escl_localita2, " _
                                      & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_VSA.escl_localita3) as escl_localita3, " _
                                       & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_VSA.escl_localita4) as escl_localita4, " _
                                        & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_VSA.escl_localita5) as escl_localita5, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_VSA.escl_quart1) as escl_quart1, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_VSA.escl_quart2) as escl_quart2, " _
                                      & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_VSA.escl_quart3) as escl_quart3, " _
                                       & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_VSA.escl_quart3) as escl_quart4, " _
                                        & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_VSA.escl_quart3) as escl_quart5, " _
                                      & " domande_preferenze_escl_VSA.escl_indirizzo1, domande_preferenze_escl_VSA.escl_indirizzo2, domande_preferenze_escl_VSA.escl_indirizzo3, domande_preferenze_escl_VSA.escl_indirizzo4, domande_preferenze_escl_VSA.escl_indirizzo5, " _
                                      & " domande_preferenze_VSA.pref_sup_max, domande_preferenze_VSA.pref_sup_min, " _
                                      & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_VSA.pref_piani_da_con) as pref_piani_da_con, " _
                                      & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_VSA.pref_piani_a_con) as pref_piani_a_con, " _
                                      & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_VSA.pref_piani_da_senza) as pref_piani_da_senza, " _
                                      & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_VSA.pref_piani_a_senza) as pref_piani_a_senza, " _
                                      & " domande_preferenze_VSA.pref_note AS note, domande_preferenze_VSA.pref_barriere AS richieste_particolari, " _
                                      & " domande_preferenze_VSA.pref_condominio AS condominio " _
                                      & " FROM domande_preferenze_VSA, domande_preferenze_escl_VSA" _
                                      & " WHERE domande_preferenze_VSA.id_domanda = domande_preferenze_escl_VSA.id_domanda " _
                                      & " And domande_preferenze_VSA.id_domanda = " & idDomanda.Value & ""


                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        contenuto = Replace(contenuto, "$prefInserite$", "si")
                        contenuto = Replace(contenuto, "$localita1$", par.IfNull(myReaderJ("pref_localita1"), " "))

                        If par.IfNull(myReaderJ("pref_localita1"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart1$", " (" & par.IfNull(myReaderJ("pref_quart1") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart1$", " ")
                        End If
                        contenuto = Replace(contenuto, "$localita2$", par.IfNull(myReaderJ("pref_localita2"), " "))

                        If par.IfNull(myReaderJ("pref_localita2"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart2$", " (" & par.IfNull(myReaderJ("pref_quart2") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart2$", " ")
                        End If

                        contenuto = Replace(contenuto, "$localita3$", par.IfNull(myReaderJ("pref_localita3"), " "))
                        If par.IfNull(myReaderJ("pref_localita3"), "") <> "" Then

                            contenuto = Replace(contenuto, "$quart3$", " (" & par.IfNull(myReaderJ("pref_quart3") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart3$", " ")
                        End If

                        contenuto = Replace(contenuto, "$localita4$", par.IfNull(myReaderJ("pref_localita4"), " "))
                        If par.IfNull(myReaderJ("pref_localita4"), "") <> "" Then

                            contenuto = Replace(contenuto, "$quart4$", " (" & par.IfNull(myReaderJ("pref_quart4") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart4$", " ")
                        End If

                        contenuto = Replace(contenuto, "$localita5$", par.IfNull(myReaderJ("pref_localita5"), " "))
                        If par.IfNull(myReaderJ("pref_localita5"), "") <> "" Then

                            contenuto = Replace(contenuto, "$quart5$", " (" & par.IfNull(myReaderJ("pref_quart5") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart5$", " ")
                        End If




                        contenuto = Replace(contenuto, "$localita1ex$", par.IfNull(myReaderJ("escl_localita1"), " "))

                        If par.IfNull(myReaderJ("escl_localita1"), "") <> "" Then

                            contenuto = Replace(contenuto, "$quart1ex$", " (" & par.IfNull(myReaderJ("escl_quart1") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart1ex$", " ")
                        End If

                        contenuto = Replace(contenuto, "$localita2ex$", par.IfNull(myReaderJ("escl_localita2"), " "))
                        If par.IfNull(myReaderJ("escl_localita2"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart2ex$", " (" & par.IfNull(myReaderJ("escl_quart2") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart2ex$", " ")
                        End If


                        contenuto = Replace(contenuto, "$localita3ex$", par.IfNull(myReaderJ("escl_localita3"), " "))

                        If par.IfNull(myReaderJ("escl_localita3"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart3ex$", " (" & par.IfNull(myReaderJ("escl_quart3") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart3ex$", " ")
                        End If


                        contenuto = Replace(contenuto, "$localita4ex$", par.IfNull(myReaderJ("escl_localita4"), " "))

                        If par.IfNull(myReaderJ("escl_localita4"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart4ex$", " (" & par.IfNull(myReaderJ("escl_quart4") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart4ex$", " ")
                        End If



                        contenuto = Replace(contenuto, "$localita5ex$", par.IfNull(myReaderJ("escl_localita5"), " "))

                        If par.IfNull(myReaderJ("escl_localita5"), "") <> "" Then
                            contenuto = Replace(contenuto, "$quart5ex$", " (" & par.IfNull(myReaderJ("escl_quart5") & ")", " "))
                        Else
                            contenuto = Replace(contenuto, "$quart5ex$", " ")
                        End If




                        If par.IfNull(myReaderJ("pref_sup_min"), "").ToString <> "" Then

                            contenuto = Replace(contenuto, "$supMin$", par.IfNull(myReaderJ("pref_sup_min") * 100, " "))
                        Else
                            contenuto = Replace(contenuto, "$supMin$", " ")
                        End If


                        If par.IfNull(myReaderJ("pref_sup_max"), "").ToString <> "" Then
                            contenuto = Replace(contenuto, "$supMax$", par.IfNull(myReaderJ("pref_sup_max") * 100, " "))
                        Else
                            contenuto = Replace(contenuto, "$supMax$", " ")
                        End If







                        If par.IfNull(myReaderJ("pref_piani_da_con"), "").ToString <> "" Then

                            contenuto = Replace(contenuto, "$pianoDaCon$", "Da: " & par.IfNull(myReaderJ("pref_piani_da_con") & "", " "))

                            If par.IfNull(myReaderJ("pref_piani_a_con"), "").ToString <> "" Then
                                contenuto = Replace(contenuto, "$pianoACon$", "A: " & par.IfNull(myReaderJ("pref_piani_a_con") & "", " "))
                            Else
                                contenuto = Replace(contenuto, "$pianoACon$", " ")
                            End If

                        Else
                            contenuto = Replace(contenuto, "$pianoACon$", " ")
                            contenuto = Replace(contenuto, "$pianoDaCon$", " ")

                        End If



                        If par.IfNull(myReaderJ("pref_piani_da_senza"), "").ToString <> "" Then

                            contenuto = Replace(contenuto, "$pianoDaSenza$", "Da: " & par.IfNull(myReaderJ("pref_piani_da_senza") & "", " "))

                            If par.IfNull(myReaderJ("pref_piani_a_senza"), "").ToString <> "" Then
                                contenuto = Replace(contenuto, "$pianoASenza$", "A: " & par.IfNull(myReaderJ("pref_piani_a_senza") & "", " "))
                            Else
                                contenuto = Replace(contenuto, "$pianoASenza$", " ")
                            End If

                        Else
                            contenuto = Replace(contenuto, "$pianoASenza$", " ")
                            contenuto = Replace(contenuto, "$pianoDaSenza$", " ")

                        End If











                        contenuto = Replace(contenuto, "$notePref$", par.IfNull(myReaderJ("note"), " "))
                        If par.IfNull(myReaderJ("richieste_particolari"), "") = "1" Then
                            contenuto = Replace(contenuto, "$barrArch$", " Barriere Architettoniche escluse")
                        Else
                            contenuto = Replace(contenuto, "$barrArch$", " ---")
                        End If
                        If par.IfNull(myReaderJ("condominio"), "") = "1" Then
                            contenuto = Replace(contenuto, "$condominio$", " SI")
                        Else
                            contenuto = Replace(contenuto, "$condominio$", " NO")
                        End If

                    Else

                        contenuto = Replace(contenuto, "$prefInserite$", "no")
                        contenuto = Replace(contenuto, "$localita1$", "")
                        contenuto = Replace(contenuto, "$quart1$", "")
                        contenuto = Replace(contenuto, "$localita2$", "")
                        contenuto = Replace(contenuto, "$quart2$", "")
                        contenuto = Replace(contenuto, "$localita3$", "")
                        contenuto = Replace(contenuto, "$quart3$", "")
                        contenuto = Replace(contenuto, "$localita4$", "")
                        contenuto = Replace(contenuto, "$quart4$", "")
                        contenuto = Replace(contenuto, "$localita5$", "")
                        contenuto = Replace(contenuto, "$quart5$", "")
                        contenuto = Replace(contenuto, "$localita1ex$", "")
                        contenuto = Replace(contenuto, "$quart1ex$", "")
                        contenuto = Replace(contenuto, "$localita2ex$", "")
                        contenuto = Replace(contenuto, "$quart2ex$", "")
                        contenuto = Replace(contenuto, "$localita3ex$", "")
                        contenuto = Replace(contenuto, "$quart3ex$", "")
                        contenuto = Replace(contenuto, "$localita4ex$", "")
                        contenuto = Replace(contenuto, "$quart4ex$", "")
                        contenuto = Replace(contenuto, "$localita5ex$", "")
                        contenuto = Replace(contenuto, "$quart5ex$", "")
                        contenuto = Replace(contenuto, "$pianoDaCon$", "")
                        contenuto = Replace(contenuto, "$pianoACon$", "")
                        contenuto = Replace(contenuto, "$pianoDaSenza$", "")
                        contenuto = Replace(contenuto, "$pianoASenza$", "")
                        contenuto = Replace(contenuto, "$condominio$", "")
                        contenuto = Replace(contenuto, "$barrArch$", "")
                        contenuto = Replace(contenuto, "$notePref$", "")
                        contenuto = Replace(contenuto, "$supMax$", " ")
                        contenuto = Replace(contenuto, "$supMin$", " ")

                    End If
                    myReaderJ.Close()



                    par.cmd.CommandText = " SELECT  unita_immobiliari.ID AS id_UI, alloggi.sup, alloggi.num_locali,alloggi.cod_alloggio as codiceUI, " _
                                             & "(CASE WHEN alloggi.ascensore=1 THEN 'SI' else 'NO' END) AS elevatore, alloggi.comune, " _
                                             & " (   t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo) as via, alloggi.num_civico as civico,alloggi.num_alloggio as interno ,alloggi.piano,alloggi.scala " _
                                             & " FROM rel_prat_all_ccaa_erp, alloggi, siscom_mi.unita_immobiliari, t_tipo_indirizzo, domande_bando_vsa " _
                                             & " WHERE domande_bando_vsa.ID = rel_prat_all_ccaa_erp.id_pratica " _
                                             & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                                             & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) " _
                                             & " And alloggi.ID = rel_prat_all_ccaa_erp.id_alloggio " _
                                             & " And rel_prat_all_ccaa_erp.id_pratica =" & idDomanda.Value & "" _
                                             & " ORDER BY rel_prat_all_ccaa_erp.ultimo DESC"




                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then

                        contenuto = Replace(contenuto, "$codiceUI$", par.IfNull(myReaderJ("codiceUI"), " "))
                        contenuto = Replace(contenuto, "$localitaUIAbb$", par.IfNull(myReaderJ("comune"), " "))
                        contenuto = Replace(contenuto, "$indirizzoUI$", par.IfNull(myReaderJ("via"), " "))
                        contenuto = Replace(contenuto, "$civicoUI$", par.IfNull(myReaderJ("civico"), " "))
                        contenuto = Replace(contenuto, "$numUI$", par.IfNull(myReaderJ("interno"), " "))
                        contenuto = Replace(contenuto, "$supUI$", par.IfNull(myReaderJ("sup") * 100, " "))
                        contenuto = Replace(contenuto, "$nVaniUI$", par.IfNull(myReaderJ("num_locali"), " "))
                        contenuto = Replace(contenuto, "$pianoUI$", par.IfNull(myReaderJ("piano"), " "))
                        contenuto = Replace(contenuto, "$ascensoreUI$", par.IfNull(myReaderJ("elevatore"), " "))
                        idUI.Value = par.IfNull(myReaderJ("id_UI"), 0)
                    Else
                        contenuto = Replace(contenuto, "$codiceUI$", "")
                        contenuto = Replace(contenuto, "$localitaUIAbb$", "")
                        contenuto = Replace(contenuto, "$indirizzoUI$", "")
                        contenuto = Replace(contenuto, "$civicoUI$", "")
                        contenuto = Replace(contenuto, "$numUI$", "")
                        contenuto = Replace(contenuto, "$supUI$", "")
                        contenuto = Replace(contenuto, "$nVaniUI$", "")
                        contenuto = Replace(contenuto, "$pianoUI$", "")
                        contenuto = Replace(contenuto, "$ascensoreUI$", "")


                    End If
                    myReaderJ.Close()



                    par.cmd.CommandText = " select * from siscom_mi.unita_stato_manutentivo where id_unita=" & idUI.Value

                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        If par.IfNull(myReaderJ("riassegnabile"), -1) <> 0 Then
                            contenuto = Replace(contenuto, "$daRistrutturare$", "no")
                        Else
                            contenuto = Replace(contenuto, "$daRistrutturare$", "si")
                        End If
                    Else
                        contenuto = Replace(contenuto, "$daRistrutturare$", "---")
                    End If
                    myReaderJ.Close()



                    par.cmd.CommandText = " select * from REL_PRAT_ALL_CCAA_ERP where id_pratica=" & idDomanda.Value & " AND ULTIMO=1"
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then

                        If par.IfNull(myReaderJ("esito"), -1) = 1 Then
                            contenuto = Replace(contenuto, "$esito$", "esito positivo")

                        End If

                        If par.IfNull(myReaderJ("esito"), -1) = 0 Or par.IfNull(myReaderJ("esito"), -1) = 3 Or par.IfNull(myReaderJ("esito"), -1) = 4 Then
                            contenuto = Replace(contenuto, "$esito$", "esito negativo")

                        End If

                        If par.IfNull(myReaderJ("esito"), -1) = -1 Then
                            contenuto = Replace(contenuto, "$esito$", "---")

                        End If
                        contenuto = Replace(contenuto, "$dataOfferta$", par.FormattaData(par.IfNull(myReaderJ("data_proposta"), " ")))
                        contenuto = Replace(contenuto, "$datarispofferta$", par.FormattaData(par.IfNull(myReaderJ("data"), " ")))
                    Else


                        contenuto = Replace(contenuto, "$esito$", "---")
                        contenuto = Replace(contenuto, "$dataOfferta$", " ")
                        contenuto = Replace(contenuto, "$datarispofferta$", " ")


                    End If
                    myReaderJ.Close()







                    par.cmd.CommandText = " SELECT tab_filiali.* FROM siscom_mi.tab_filiali, operatori WHERE tab_filiali.ID = operatori.id_ufficio (+) AND operatori.operatore ='" & Session.Item("OPERATORE") & "'"
                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        contenuto = Replace(contenuto, "$filialeComp$", par.IfNull(myReaderJ("nome"), " "))
                        contenuto = Replace(contenuto, "$ReferenteAmm$", par.IfNull(myReaderJ("ref_amministrativo"), " "))
                        contenuto = Replace(contenuto, "$responsabile$", par.IfNull(myReaderJ("responsabile"), "_________________________"))
                    Else
                        contenuto = Replace(contenuto, "$filialeComp$", " ")
                        contenuto = Replace(contenuto, "$ReferenteAmm$", " ")
                        contenuto = Replace(contenuto, "$responsabile$", "_______________________")
                    End If
                    myReaderJ.Close()







                    '           '--------reddito
                    Dim DETRAZIONI_FR As Long
                    Dim TOT_SPESE As Long
                    Dim REDDITO_COMPLESSIVO As Double = 0
                    Dim REDD_DIP As Double = 0
                    Dim REDD_ALT As Double = 0
                    Dim INV_100_CON As Integer
                    Dim INV_100_NO As Integer
                    Dim INV_66_99 As Integer
                    Dim TASSO_RENDIMENTO As Double
                    Dim DETRAZIONI As Long
                    Dim FIGURATIVO_MOBILI As Double
                    Dim ISEE_ERP As Double
                    Dim ISR_ERP As Double


                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPOVIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.*,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,SINDACATI_VSA.DESCRIZIONE AS SINDACATO " _
         & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_CAUSALI_DOMANDA_VSA,T_TIPO_INDIRIZZO,T_MOTIVO_DOMANDA_VSA,SINDACATI_VSA WHERE DICHIARAZIONI_VSA.ID_SINDACATO_VSA =SINDACATI_VSA.ID(+) AND DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID " _
         & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD AND DICHIARAZIONI_VSA.ID = " & idDichiarazione.Value & " AND COMP_NUCLEO_VSA.PROGR = 0"

                    myReaderJ = par.cmd.ExecuteReader()
                    If myReaderJ.Read() Then

                        INV_100_CON = par.IfNull(myReaderJ("N_INV_100_CON"), 0)
                        INV_100_NO = par.IfNull(myReaderJ("N_INV_100_SENZA"), 0)
                        INV_66_99 = par.IfNull(myReaderJ("N_INV_100_66"), 0)
                        TASSO_RENDIMENTO = par.RicavaTasso(par.IfNull(myReaderJ("ANNO_SIT_ECONOMICA"), 0))

                    End If
                    myReaderJ.Close()


                    par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE= " & idDichiarazione.Value
                    myReaderJ = par.cmd.ExecuteReader()
                    While myReaderJ.Read
                        par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReaderJ("ID"), -1)
                        Dim myReaderR1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        While myReaderR1.Read
                            REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderR1("REDDITO_IRPEF"), 0) + par.IfNull(myReaderR1("PROV_AGRARI"), 0)
                        End While
                        myReaderR1.Close()


                        par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReaderJ("ID"), -1)
                        Dim myReaderR2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        While myReaderR2.Read
                            REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReaderR2("IMPORTO"), 0)
                        End While
                        myReaderR2.Close()


                        Dim DETRAZIONI_FRAGILE As Double = 0
                        par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReaderJ("ID"), -1)
                        Dim myReaderDetr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myReaderDetr.HasRows Then
                            While myReaderDetr.Read
                                DETRAZIONI_FRAGILE = DETRAZIONI_FRAGILE + par.IfNull(myReaderDetr("IMPORTO"), 0)
                                TOT_SPESE = TOT_SPESE + par.IfNull(myReaderDetr("IMPORTO"), 0)

                                If DETRAZIONI_FRAGILE > 10000 Then
                                    DETRAZIONI_FR = DETRAZIONI_FR + DETRAZIONI_FRAGILE
                                Else
                                    DETRAZIONI_FR = DETRAZIONI_FR + 10000
                                End If

                            End While
                            myReaderDetr.Close()
                        Else
                            If par.IfNull(myReaderJ("indennita_acc"), 0) = "1" Then
                                DETRAZIONI_FR = DETRAZIONI_FR + 10000
                                TOT_SPESE = TOT_SPESE + 10000
                            End If
                            myReaderDetr.Close()
                        End If

                    End While
                    myReaderJ.Close()

                    par.cmd.CommandText = "select sum(dipendente+non_imponibili+pensione) from DOMANDE_REDDITI_VSA where ID_DOMANDA=" & idDomanda.Value
                    Dim myReaderW As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderW.Read Then
                        REDD_DIP = par.IfNull(myReaderW(0), 0)
                    End If
                    myReaderW.Close()

                    par.cmd.CommandText = "select sum(autonomo+dom_ag_fab+occasionali) from DOMANDE_REDDITI_VSA where ID_DOMANDA=" & idDomanda.Value
                    myReaderW = par.cmd.ExecuteReader()
                    If myReaderW.Read Then
                        REDD_ALT = par.IfNull(myReaderW(0), 0)
                    End If
                    myReaderW.Close()

                    If REDD_DIP > ((REDD_ALT + REDD_DIP) * 80) / 100 Then
                        contenuto = Replace(contenuto, "$reddPrev$", "DIPENDENTE")
                    Else
                        contenuto = Replace(contenuto, "$reddPrev$", "ALTRO")
                    End If

                    DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)

                    ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
                    If ISEE_ERP < 0 Then
                        ISEE_ERP = 0
                    End If

                    ISR_ERP = ISEE_ERP
                    contenuto = Replace(contenuto, "$reddTot$", par.Converti(Format(REDDITO_COMPLESSIVO, "##,##0.00")))
                    contenuto = Replace(contenuto, "$reddEff$", par.Converti(Format(ISR_ERP, "##,##0.00")))



                    '-----dati da funzione



                    CalcolaCanone("3", "" & idDomanda.Value & "", "" & idUI.Value & "")


                    contenuto = Replace(contenuto, "$IseePrevisto$", Format(CDbl(sISEE), "##,##0.00"))
                    ' contenuto = Replace(contenuto, "$area$", AreaEconomica)
                    If sSOTTOAREA <> "" Then

                        contenuto = Replace(contenuto, "$classe$", sSOTTOAREA)

                    Else
                        contenuto = Replace(contenuto, "$classe$", "Valore non rilevabile: unità non comunale.")

                    End If





                    Select Case AreaEconomica
                        Case 1
                            contenuto = Replace(contenuto, "$area$", "PROTEZIONE")
                        Case 2
                            contenuto = Replace(contenuto, "$area$", "ACCESSO")
                        Case 3
                            contenuto = Replace(contenuto, "$area$", "PERMANENZA")

                        Case 4
                            contenuto = Replace(contenuto, "$area$", "DECADENZA")
                        Case Else
                            contenuto = Replace(contenuto, "$area$", "Valore non rilevabile: unità non comunale.")
                    End Select












            End Select





            Dim PercorsoBarCode As String = par.RicavaBarCode(34, idDomanda.Value)
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

            Dim nomefile As String = "A7_" & idDomanda.Value & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
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
    Private Function CalcolaCanone(ByVal Tipo As String, ByVal IdDomanda As String, ByVal IndiceUnita As String)
        Dim CANONE As Double = 0
        Dim S As String = ""
        Dim id_unita As Long = 0

        'Try
        '    PAR.OracleConn.Open()
        '    par.SettaCommand(par)

        '    PAR.cmd.CommandText = "SELECT id from siscom_MI.unita_immobiliari where cod_unita_immobiliare='" & IndiceUnita & "'"
        '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        '    If myReader.Read() Then
        '        id_unita = PAR.IfNull(myReader(0), -1)
        '    End If
        '    myReader.Close()
        '    PAR.cmd.Dispose()
        '    PAR.OracleConn.Close()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        'Catch ex As Exception
        '    PAR.OracleConn.Close()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        'End Try

        Dim VAL_LOCATIVO_UNITA As String = ""
        Dim comunicazioni As String = ""

        Select Case Tipo
            Case "1"
                S = par.CalcolaCanone27(IdDomanda, 1, IndiceUnita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
            Case "2"
                S = par.CalcolaCanone27(IdDomanda, 2, IndiceUnita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
            Case "3"
                S = par.CalcolaCanone27(IdDomanda, 3, IndiceUnita, "TEST", CANONE, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)
        End Select

        '  LBLTESTO.Text = Replace(S, vbCrLf, "<br />")



    End Function


    Public Property AreaEconomica() As Integer
        Get
            If Not (ViewState("par_AreaEconomica") Is Nothing) Then
                Return CInt(ViewState("par_AreaEconomica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_AreaEconomica") = value
        End Set

    End Property


    Public Property sCanone() As String
        Get
            If Not (ViewState("par_sCanone") Is Nothing) Then
                Return CStr(ViewState("par_sCanone"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCanone") = value
        End Set
    End Property

    Public Property sISEE() As String
        Get
            If Not (ViewState("par_sISEE") Is Nothing) Then
                Return CStr(ViewState("par_sISEE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISEE") = value
        End Set

    End Property


    Public Property sISE() As String
        Get
            If Not (ViewState("par_sISE") Is Nothing) Then
                Return CStr(ViewState("par_sISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE") = value
        End Set
    End Property

    Public Property sVSE() As String
        Get
            If Not (ViewState("par_sVSE") Is Nothing) Then
                Return CStr(ViewState("par_sVSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVSE") = value
        End Set
    End Property


    Public Property sPSE() As String
        Get
            If Not (ViewState("par_sPSE") Is Nothing) Then
                Return CStr(ViewState("par_sPSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPSE") = value
        End Set
    End Property

    Public Property sISP() As String
        Get
            If Not (ViewState("par_sISP") Is Nothing) Then
                Return CStr(ViewState("par_sISP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISP") = value
        End Set
    End Property


    Public Property sISR() As String
        Get
            If Not (ViewState("par_sISR") Is Nothing) Then
                Return CStr(ViewState("par_sISR"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISR") = value
        End Set
    End Property

    Public Property sREDD_DIP() As String
        Get
            If Not (ViewState("par_sREDD_DIP") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_DIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_DIP") = value
        End Set
    End Property

    Public Property sREDD_ALT() As String
        Get
            If Not (ViewState("par_sREDD_ALT") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_ALT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_ALT") = value
        End Set
    End Property

    Public Property sLimitePensione() As String
        Get
            If Not (ViewState("par_sLimitePensione") Is Nothing) Then
                Return CStr(ViewState("par_sLimitePensione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLimitePensione") = value
        End Set
    End Property

    Public Property sPER_VAL_LOC() As String
        Get
            If Not (ViewState("par_sPER_VAL_LOC") Is Nothing) Then
                Return CStr(ViewState("par_sPER_VAL_LOC"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPER_VAL_LOC") = value
        End Set
    End Property


    Public Property sPERC_INC_MAX_ISE_ERP() As String
        Get
            If Not (ViewState("par_sPERC_INC_MAX_ISE_ERP") Is Nothing) Then
                Return CStr(ViewState("par_sPERC_INC_MAX_ISE_ERP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPERC_INC_MAX_ISE_ERP") = value
        End Set
    End Property

    Public Property sCANONE_MIN() As String
        Get
            If Not (ViewState("par_sCANONE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE_MIN") = value
        End Set
    End Property

    Public Property sISE_MIN() As String
        Get
            If Not (ViewState("par_sISE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sISE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE_MIN") = value
        End Set
    End Property

    Public Property sNOTE() As String
        Get
            If Not (ViewState("par_sNOTE") Is Nothing) Then
                Return CStr(ViewState("par_sNOTE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNOTE") = value
        End Set
    End Property

    Public Property sDEM() As String
        Get
            If Not (ViewState("par_sDEM") Is Nothing) Then
                Return CStr(ViewState("par_sDEM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDEM") = value
        End Set
    End Property

    Public Property sSUPCONVENZIONALE() As String
        Get
            If Not (ViewState("par_sSUPCONVENZIONALE") Is Nothing) Then
                Return CStr(ViewState("par_sSUPCONVENZIONALE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPCONVENZIONALE") = value
        End Set
    End Property

    Public Property sCOSTOBASE() As String
        Get
            If Not (ViewState("par_sCOSTOBASE") Is Nothing) Then
                Return CStr(ViewState("par_sCOSTOBASE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOSTOBASE") = value
        End Set
    End Property

    Public Property sZONA() As String
        Get
            If Not (ViewState("par_sZONA") Is Nothing) Then
                Return CStr(ViewState("par_sZONA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sZONA") = value
        End Set
    End Property

    Public Property sPIANO() As String
        Get
            If Not (ViewState("par_sPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPIANO") = value
        End Set
    End Property

    Public Property sCONSERVAZIONE() As String
        Get
            If Not (ViewState("par_sCONSERVAZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sCONSERVAZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCONSERVAZIONE") = value
        End Set
    End Property

    Public Property sVETUSTA() As String
        Get
            If Not (ViewState("par_sVETUSTA") Is Nothing) Then
                Return CStr(ViewState("par_sVETUSTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVETUSTA") = value
        End Set
    End Property

    Public Property sINCIDENZAISE() As String
        Get
            If Not (ViewState("par_sINCIDENZAISE") Is Nothing) Then
                Return CStr(ViewState("par_sINCIDENZAISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sINCIDENZAISE") = value
        End Set
    End Property


    Public Property sCOEFFFAM() As String
        Get
            If Not (ViewState("par_sCOEFFFAM") Is Nothing) Then
                Return CStr(ViewState("par_sCOEFFFAM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOEFFFAM") = value
        End Set
    End Property

    Public Property sSOTTOAREA() As String
        Get
            If Not (ViewState("par_sSOTTOAREA") Is Nothing) Then
                Return CStr(ViewState("par_sSOTTOAREA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSOTTOAREA") = value
        End Set
    End Property

    Public Property sMOTIVODECADENZA() As String
        Get
            If Not (ViewState("par_sMOTIVODECADENZA") Is Nothing) Then
                Return CStr(ViewState("par_sMOTIVODECADENZA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOTIVODECADENZA") = value
        End Set
    End Property

    Public Property sNUMCOMP() As String
        Get
            If Not (ViewState("par_sNUMCOMP") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP") = value
        End Set
    End Property

    Public Property sNUMCOMP66() As String
        Get
            If Not (ViewState("par_sNUMCOMP66") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP66"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP66") = value
        End Set
    End Property

    Public Property sNUMCOMP100() As String
        Get
            If Not (ViewState("par_sNUMCOMP100") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100") = value
        End Set
    End Property

    Public Property sNUMCOMP100C() As String
        Get
            If Not (ViewState("par_sNUMCOMP100C") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100C"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100C") = value
        End Set
    End Property

    Public Property sPREVDIP() As String
        Get
            If Not (ViewState("par_sPREVDIP") Is Nothing) Then
                Return CStr(ViewState("par_sPREVDIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPREVDIP") = value
        End Set
    End Property

    Public Property sDETRAZIONI() As String
        Get
            If Not (ViewState("par_sDETRAZIONI") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONI") = value
        End Set
    End Property

    Public Property sMOBILIARI() As String
        Get
            If Not (ViewState("par_sMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOBILIARI") = value
        End Set
    End Property


    Public Property sIMMOBILIARI() As String
        Get
            If Not (ViewState("par_sIMMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sIMMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIMMOBILIARI") = value
        End Set
    End Property

    Public Property sCOMPLESSIVO() As String
        Get
            If Not (ViewState("par_sCOMPLESSIVO") Is Nothing) Then
                Return CStr(ViewState("par_sCOMPLESSIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOMPLESSIVO") = value
        End Set
    End Property

    Public Property sDETRAZIONEF() As String
        Get
            If Not (ViewState("par_sDETRAZIONEF") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONEF"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONEF") = value
        End Set
    End Property

    Public Property sANNOCOSTRUZIONE() As String
        Get
            If Not (ViewState("par_sANNOCOSTRUZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sANNOCOSTRUZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOCOSTRUZIONE") = value
        End Set
    End Property

    Public Property sLOCALITA() As String
        Get
            If Not (ViewState("par_sLOCALITA") Is Nothing) Then
                Return CStr(ViewState("par_sLOCALITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLOCALITA") = value
        End Set
    End Property

    Public Property sASCENSORE() As String
        Get
            If Not (ViewState("par_sASCENSORE") Is Nothing) Then
                Return CStr(ViewState("par_sASCENSORE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sASCENSORE") = value
        End Set
    End Property

    Public Property sDESCRIZIONEPIANO() As String
        Get
            If Not (ViewState("par_sDESCRIZIONEPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sDESCRIZIONEPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDESCRIZIONEPIANO") = value
        End Set
    End Property

    Public Property sSUPNETTA() As String
        Get
            If Not (ViewState("par_sSUPNETTA") Is Nothing) Then
                Return CStr(ViewState("par_sSUPNETTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPNETTA") = value
        End Set
    End Property

    Public Property sALTRESUP() As String
        Get
            If Not (ViewState("par_sALTRESUP") Is Nothing) Then
                Return CStr(ViewState("par_sALTRESUP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALTRESUP") = value
        End Set
    End Property

    Public Property sMINORI15() As String
        Get
            If Not (ViewState("par_sMINORI15") Is Nothing) Then
                Return CStr(ViewState("par_sMINORI15"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMINORI15") = value
        End Set
    End Property


    Public Property sMAGGIORI65() As String
        Get
            If Not (ViewState("par_sMAGGIORI65") Is Nothing) Then
                Return CStr(ViewState("par_sMAGGIORI65"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMAGGIORI65") = value
        End Set
    End Property

    Public Property sSUPACCESSORI() As String
        Get
            If Not (ViewState("par_sSUPACCESSORI") Is Nothing) Then
                Return CStr(ViewState("par_sSUPACCESSORI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPACCESSORI") = value
        End Set
    End Property

    Public Property sVALORELOCATIVO() As String
        Get
            If Not (ViewState("par_sVALORELOCATIVO") Is Nothing) Then
                Return CStr(ViewState("par_sVALORELOCATIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALORELOCATIVO") = value
        End Set
    End Property

    Public Property sCANONEMINIMO() As String
        Get
            If Not (ViewState("par_sCANONEMINIMO") Is Nothing) Then
                Return CStr(ViewState("par_sCANONEMINIMO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONEMINIMO") = value
        End Set
    End Property

    Public Property sCANONECLASSE() As String
        Get
            If Not (ViewState("par_sCANONECLASSE") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSE") = value
        End Set
    End Property

    Public Property sCANONESOPP() As String
        Get
            If Not (ViewState("par_sCANONESOPP") Is Nothing) Then
                Return CStr(ViewState("par_sCANONESOPP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONESOPP") = value
        End Set
    End Property


    Public Property sVALOCIICI() As String
        Get
            If Not (ViewState("par_sVALOCIICI") Is Nothing) Then
                Return CStr(ViewState("par_sVALOCIICI"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALOCIICI") = value
        End Set
    End Property

    Public Property sALLOGGIOIDONEO() As String
        Get
            If Not (ViewState("par_sALLOGGIOIDONEO") Is Nothing) Then
                Return CStr(ViewState("par_sALLOGGIOIDONEO"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALLOGGIOIDONEO") = value
        End Set
    End Property

    Public Property sISTAT() As String
        Get
            If Not (ViewState("par_sISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISTAT") = value
        End Set
    End Property

    Public Property sCANONECLASSEISTAT() As String
        Get
            If Not (ViewState("par_sCANONECLASSEISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSEISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSEISTAT") = value
        End Set
    End Property

    Public Property VAL_LOCATIVO_UNITA() As String
        Get
            If Not (ViewState("par_VAL_LOCATIVO_UNITA") Is Nothing) Then
                Return CStr(ViewState("par_VAL_LOCATIVO_UNITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_VAL_LOCATIVO_UNITA") = value
        End Set

    End Property



    Public Property CanoneCorrente() As Double
        Get
            If Not (ViewState("par_CanoneCorrente") Is Nothing) Then
                Return CDbl(ViewState("par_CanoneCorrente"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Double)
            ViewState("par_CanoneCorrente") = value
        End Set

    End Property


    Public Property sANNOINIZIOVAL() As String
        Get
            If Not (ViewState("par_sANNOINIZIOVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOINIZIOVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOINIZIOVAL") = value
        End Set
    End Property

    Public Property sANNOFINEVAL() As String
        Get
            If Not (ViewState("par_sANNOFINEVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOFINEVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOFINEVAL") = value
        End Set
    End Property



End Class
