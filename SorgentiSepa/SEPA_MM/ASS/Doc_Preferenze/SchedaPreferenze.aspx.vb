Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Partial Class ASS_Doc_Preferenze_SchedaPreferenze
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
                tipo.Value = Request.QueryString("TIPO")
                idDomanda.Value = Request.QueryString("IDDOMANDA")
                Provenienza.Value = Request.QueryString("PROV")
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





            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("SchedaPreferenze.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()

            sr1.Close()




            If Provenienza.Value = "1" Then

                If tipo.Value = "1" Then

                    par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita1) as pref_localita1, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita2) as pref_localita2, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita3) as pref_localita3, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita4) as pref_localita4, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita5) as pref_localita5, " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona1) as pref_zona1,  " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona2) as pref_zona2,  " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona3) as pref_zona3, " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona4) as pref_zona4, " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona5) as pref_zona5, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart1) as pref_quart1, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart2) as pref_quart2,  " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart3) as pref_quart3, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart4) as pref_quart4, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart5) as pref_quart5, " _
                                           & " domande_preferenze.pref_indirizzo1, domande_preferenze.pref_indirizzo2, domande_preferenze.pref_indirizzo3, domande_preferenze.pref_indirizzo4,domande_preferenze.pref_indirizzo5, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita1) as escl_localita1, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita2) as escl_localita2, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita3) as escl_localita3, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita4) as escl_localita4, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita5) as escl_localita5, " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona1) as escl_zona1, " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona2) as escl_zona2, " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona3) as escl_zona3, " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona4) as escl_zona4, " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona5) as escl_zona5, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart1) as escl_quart1, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart2) as escl_quart2, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart3) as escl_quart3, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart4) as escl_quart4, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart5) as escl_quart5, " _
                                           & " domande_preferenze_esclusioni.escl_indirizzo1, domande_preferenze_esclusioni.escl_indirizzo2, domande_preferenze_esclusioni.escl_indirizzo3,domande_preferenze_esclusioni.escl_indirizzo4,domande_preferenze_esclusioni.escl_indirizzo5, " _
                                           & " domande_preferenze.pref_sup_max, domande_preferenze.pref_sup_min, " _
                                           & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze.pref_piani_da_con) as pref_piani_da_con, " _
                                           & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze.pref_piani_a_con) as pref_piani_a_con, " _
                                           & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze.pref_piani_da_senza) as pref_piani_da_senza, " _
                                           & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze.pref_piani_a_senza) as pref_piani_a_senza, " _
                                           & " (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS richiedente, domande_preferenze.pref_note AS note, domande_preferenze.pref_barriere AS richieste_particolari, " _
                                           & " domande_bando.pg as PG, " _
                                           & " domande_preferenze.pref_condominio AS condominio " _
                                           & " FROM domande_preferenze, domande_preferenze_esclusioni, domande_bando, comp_nucleo " _
                                           & " WHERE domande_preferenze.id_domanda = domande_preferenze_esclusioni.id_domanda " _
                                           & " AND domande_bando.ID = domande_preferenze_esclusioni.id_domanda " _
                                           & " And domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione " _
                                           & " And comp_nucleo.progr = 0 " _
                                           & " And domande_preferenze.id_domanda = " & idDomanda.Value & ""



                Else

                    par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita1) as pref_localita1, " _
                                                              & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita2) as pref_localita2, " _
                                                              & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita3) as pref_localita3, " _
                                                              & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita4) as pref_localita4, " _
                                                              & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita5) as pref_localita5, " _
                                                              & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona1) as pref_zona1,  " _
                                                              & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona2) as pref_zona2,  " _
                                                              & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona3) as pref_zona3, " _
                                                              & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona4) as pref_zona4, " _
                                                              & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona5) as pref_zona5, " _
                                                              & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart1) as pref_quart1, " _
                                                              & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart2) as pref_quart2,  " _
                                                              & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart3) as pref_quart3, " _
                                                              & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart4) as pref_quart4, " _
                                                              & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart5) as pref_quart5, " _
                                                              & " domande_preferenze_cambi.pref_indirizzo1, domande_preferenze_cambi.pref_indirizzo2, domande_preferenze_cambi.pref_indirizzo3,domande_preferenze_cambi.pref_indirizzo4,domande_preferenze_cambi.pref_indirizzo5, " _
                                                              & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita1) as escl_localita1, " _
                                                              & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita2) as escl_localita2, " _
                                                              & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita3) as escl_localita3, " _
                                                              & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita4) as escl_localita4, " _
                                                              & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita5) as escl_localita5, " _
                                                              & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona1) as escl_zona1, " _
                                                              & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona2) as escl_zona2, " _
                                                              & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona3) as escl_zona3, " _
                                                              & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona4) as escl_zona4, " _
                                                              & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona5) as escl_zona5, " _
                                                              & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart1) as escl_quart1, " _
                                                              & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart2) as escl_quart2, " _
                                                              & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart3) as escl_quart3, " _
                                                              & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart4) as escl_quart4, " _
                                                              & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart5) as escl_quart5, " _
                                                              & " domande_preferenze_escl_cambi.escl_indirizzo1, domande_preferenze_escl_cambi.escl_indirizzo2, domande_preferenze_escl_cambi.escl_indirizzo3, domande_preferenze_escl_cambi.escl_indirizzo4,domande_preferenze_escl_cambi.escl_indirizzo5, " _
                                                              & " domande_preferenze_cambi.pref_sup_max, domande_preferenze_cambi.pref_sup_min, " _
                                                              & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_cambi.pref_piani_da_con) as pref_piani_da_con, " _
                                                              & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_cambi.pref_piani_a_con) as pref_piani_a_con, " _
                                                              & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_cambi.pref_piani_da_senza) as pref_piani_da_senza, " _
                                                              & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_cambi.pref_piani_a_senza) as pref_piani_a_senza, " _
                                             & " (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS richiedente, domande_preferenze_cambi.pref_note AS note, domande_preferenze_cambi.pref_barriere AS richieste_particolari, " _
                                             & " domande_bando_cambi.pg as PG, " _
                                             & " domande_preferenze_cambi.pref_condominio AS condominio " _
                                             & " FROM domande_preferenze_cambi, domande_preferenze_escl_cambi, domande_bando_cambi, comp_nucleo_cambi " _
                                             & " WHERE domande_preferenze_cambi.id_domanda = domande_preferenze_escl_cambi.id_domanda " _
                                             & " AND domande_bando_cambi.ID = domande_preferenze_escl_cambi.id_domanda " _
                                             & " And domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione " _
                                             & " And comp_nucleo_cambi.progr = 0 " _
                                             & " And domande_preferenze_cambi.id_domanda = " & idDomanda.Value & ""



                End If


                Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderJ.Read Then



                    contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderJ("richiedente"), "_______________________"))
                    contenuto = Replace(contenuto, "$PG$", par.IfNull(myReaderJ("PG"), " "))

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

                        contenuto = Replace(contenuto, "$supMin$", par.IfNull(FormatNumber(myReaderJ("pref_sup_min"), 2), " "))

                    Else

                        contenuto = Replace(contenuto, "$supMin$", " ")

                    End If


                    If par.IfNull(myReaderJ("pref_sup_max"), "").ToString <> "" Then

                        contenuto = Replace(contenuto, "$supMax$", par.IfNull(FormatNumber(myReaderJ("pref_sup_max"), 2), " "))

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

                    contenuto = Replace(contenuto, "$note$", par.IfNull(myReaderJ("note"), " "))

                    If par.IfNull(myReaderJ("richieste_particolari"), "") = "1" Then
                        contenuto = Replace(contenuto, "$barrArchit$", " Barriere Architettoniche escluse")
                    Else

                        contenuto = Replace(contenuto, "$barrArchit$", " ---")
                    End If


                    If par.IfNull(myReaderJ("condominio"), "") = "1" Then
                        contenuto = Replace(contenuto, "$condominio$", " SI")
                    Else

                        contenuto = Replace(contenuto, "$condominio$", " NO")
                    End If



                Else


                    If tipo.Value = "1" Then


                        par.cmd.CommandText = " SELECT (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS richiedente, domande_bando.pg AS pg " _
                                        & "  FROM domande_bando, comp_nucleo " _
                                        & " WHERE domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione " _
                                        & " And comp_nucleo.progr = 0 AND domande_bando.ID =" & idDomanda.Value & ""


                    Else



                        par.cmd.CommandText = " SELECT (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS richiedente, domande_bando_cambi.pg AS pg " _
                                               & "  FROM domande_bando_cambi, comp_nucleo_cambi " _
                                               & " WHERE domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione " _
                                               & " And comp_nucleo_cambi.progr = 0 AND domande_bando_cambi.ID =" & idDomanda.Value & ""




                    End If
                    Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderK.Read Then



                        contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderK("richiedente"), "_______________________"))
                        contenuto = Replace(contenuto, "$PG$", par.IfNull(myReaderK("PG"), " "))

                    End If
                    myReaderK.Close()


                    contenuto = Replace(contenuto, "$localita1$", " ")
                    contenuto = Replace(contenuto, "$localita2$", " ")
                    contenuto = Replace(contenuto, "$localita3$", " ")
                    contenuto = Replace(contenuto, "$localita4$", " ")
                    contenuto = Replace(contenuto, "$localita5$", " ")
                    contenuto = Replace(contenuto, "$quart1$", " ")
                    contenuto = Replace(contenuto, "$quart2$", " ")
                    contenuto = Replace(contenuto, "$quart3$", " ")
                    contenuto = Replace(contenuto, "$quart4$", " ")
                    contenuto = Replace(contenuto, "$quart5$", " ")
                    contenuto = Replace(contenuto, "$localita1ex$", " ")
                    contenuto = Replace(contenuto, "$localita2ex$", " ")
                    contenuto = Replace(contenuto, "$localita3ex$", " ")
                    contenuto = Replace(contenuto, "$localita4ex$", " ")
                    contenuto = Replace(contenuto, "$localita5ex$", " ")
                    contenuto = Replace(contenuto, "$quart1ex$", " ")
                    contenuto = Replace(contenuto, "$quart2ex$", " ")
                    contenuto = Replace(contenuto, "$quart3ex$", " ")
                    contenuto = Replace(contenuto, "$quart4ex$", " ")
                    contenuto = Replace(contenuto, "$quart5ex$", " ")
                    contenuto = Replace(contenuto, "$supMin$", " ")
                    contenuto = Replace(contenuto, "$supMax$", " ")
                    contenuto = Replace(contenuto, "$pianoDaCon$", "")
                    contenuto = Replace(contenuto, "$pianoACon$", "")
                    contenuto = Replace(contenuto, "$pianoDaSenza$", "")
                    contenuto = Replace(contenuto, "$pianoASenza$", "")
                    contenuto = Replace(contenuto, "$note$", " ")
                    contenuto = Replace(contenuto, "$barrArchit$", " ")
                    contenuto = Replace(contenuto, "$condominio$", " ")





                End If
                myReaderJ.Close()


            Else

                Select Case tipo.Value

                    Case "ART.22 C.10"
                        par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_vsa.pref_localita1) as pref_localita1, " _
                                     & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_vsa.pref_localita2) as pref_localita2, " _
                                     & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_vsa.pref_localita3) as pref_localita3, " _
                                     & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_vsa.pref_localita4) as pref_localita4, " _
                                     & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_vsa.pref_localita5) as pref_localita5, " _
                                     & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_vsa.pref_zona1) as pref_zona1,  " _
                                     & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_vsa.pref_zona2) as pref_zona2,  " _
                                     & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_vsa.pref_zona3) as pref_zona3, " _
                                     & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_vsa.pref_zona4) as pref_zona4, " _
                                     & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_vsa.pref_zona5) as pref_zona5, " _
                                     & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_vsa.pref_quart1) as pref_quart1, " _
                                     & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_vsa.pref_quart2) as pref_quart2,  " _
                                     & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_vsa.pref_quart3) as pref_quart3, " _
                                     & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_vsa.pref_quart4) as pref_quart4, " _
                                     & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_vsa.pref_quart5) as pref_quart5, " _
                                     & " domande_preferenze_vsa.pref_indirizzo1, domande_preferenze_vsa.pref_indirizzo2, domande_preferenze_vsa.pref_indirizzo3,domande_preferenze_vsa.pref_indirizzo4,domande_preferenze_vsa.pref_indirizzo5, " _
                                     & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_vsa.escl_localita1) as escl_localita1, " _
                                     & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_vsa.escl_localita2) as escl_localita2, " _
                                     & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_vsa.escl_localita3) as escl_localita3, " _
                                     & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_vsa.escl_localita4) as escl_localita4, " _
                                     & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_vsa.escl_localita5) as escl_localita5, " _
                                     & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_vsa.escl_zona1) as escl_zona1, " _
                                     & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_vsa.escl_zona2) as escl_zona2, " _
                                     & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_vsa.escl_zona3) as escl_zona3, " _
                                     & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_vsa.escl_zona4) as escl_zona4, " _
                                     & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_vsa.escl_zona5) as escl_zona5, " _
                                     & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_vsa.escl_quart1) as escl_quart1, " _
                                     & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_vsa.escl_quart2) as escl_quart2, " _
                                     & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_vsa.escl_quart3) as escl_quart3, " _
                                     & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_vsa.escl_quart4) as escl_quart4, " _
                                     & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_vsa.escl_quart5) as escl_quart5, " _
                                     & " domande_preferenze_escl_vsa.escl_indirizzo1, domande_preferenze_escl_vsa.escl_indirizzo2, domande_preferenze_escl_vsa.escl_indirizzo3,domande_preferenze_escl_vsa.escl_indirizzo4,domande_preferenze_escl_vsa.escl_indirizzo5, " _
                                     & " domande_preferenze_vsa.pref_sup_max, domande_preferenze_vsa.pref_sup_min, " _
                                     & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_vsa.pref_piani_da_con) as pref_piani_da_con, " _
                                     & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_vsa.pref_piani_a_con) as pref_piani_a_con, " _
                                     & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_vsa.pref_piani_da_senza) as pref_piani_da_senza, " _
                                     & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_vsa.pref_piani_a_senza) as pref_piani_a_senza, " _
                                          & " (comp_nucleo_vsa.cognome || ' ' || comp_nucleo_vsa.nome) AS richiedente, domande_preferenze_vsa.pref_note AS note, domande_preferenze_vsa.pref_barriere AS richieste_particolari, " _
                                          & " domande_preferenze_vsa.pref_condominio AS condominio, " _
                                          & " domande_bando_vsa.pg as PG " _
                                          & " FROM domande_preferenze_vsa, domande_preferenze_escl_vsa, domande_bando_vsa, comp_nucleo_vsa " _
                                          & " WHERE domande_preferenze_vsa.id_domanda = domande_preferenze_escl_vsa.id_domanda " _
                                          & " AND domande_bando_vsa.ID = domande_preferenze_escl_vsa.id_domanda " _
                                          & " And domande_bando_vsa.id_dichiarazione = comp_nucleo_vsa.id_dichiarazione " _
                                          & " And comp_nucleo_vsa.progr = 0 " _
                                          & " And domande_preferenze_vsa.id_domanda = " & idDomanda.Value & ""

                        Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderJ.Read Then



                            contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderJ("richiedente"), "_______________________"))
                            contenuto = Replace(contenuto, "$PG$", par.IfNull(myReaderJ("PG"), " "))

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

                                contenuto = Replace(contenuto, "$supMin$", par.IfNull(FormatNumber(myReaderJ("pref_sup_min"), 2), " "))

                            Else

                                contenuto = Replace(contenuto, "$supMin$", " ")

                            End If


                            If par.IfNull(myReaderJ("pref_sup_max"), "").ToString <> "" Then

                                contenuto = Replace(contenuto, "$supMax$", par.IfNull(FormatNumber(myReaderJ("pref_sup_max"), 2), " "))

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
                            
                            contenuto = Replace(contenuto, "$note$", par.IfNull(myReaderJ("note"), " "))

                            If par.IfNull(myReaderJ("richieste_particolari"), "") = "1" Then
                                contenuto = Replace(contenuto, "$barrArchit$", " Barriere Architettoniche escluse")
                            Else

                                contenuto = Replace(contenuto, "$barrArchit$", " ---")
                            End If


                            If par.IfNull(myReaderJ("condominio"), "") = "1" Then
                                contenuto = Replace(contenuto, "$condominio$", " SI")
                            Else

                                contenuto = Replace(contenuto, "$condominio$", " NO")
                            End If

                        Else
                            par.cmd.CommandText = " SELECT (comp_nucleo_vsa.cognome || ' ' || comp_nucleo_vsa.nome) AS richiedente, domande_bando_vsa.pg AS pg " _
                                                    & "  FROM domande_bando_vsa, comp_nucleo_vsa " _
                                                    & " WHERE domande_bando_vsa.id_dichiarazione = comp_nucleo_vsa.id_dichiarazione " _
                                                    & " And comp_nucleo_vsa.progr = 0 AND domande_bando_vsa.ID =" & idDomanda.Value & ""
                            Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderK.Read Then

                                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderK("richiedente"), "_______________________"))
                                contenuto = Replace(contenuto, "$PG$", par.IfNull(myReaderK("PG"), " "))

                            End If
                            myReaderK.Close()



                            contenuto = Replace(contenuto, "$localita1$", " ")
                            contenuto = Replace(contenuto, "$localita2$", " ")
                            contenuto = Replace(contenuto, "$localita3$", " ")
                            contenuto = Replace(contenuto, "$localita4$", " ")
                            contenuto = Replace(contenuto, "$localita5$", " ")
                            contenuto = Replace(contenuto, "$quart1$", " ")
                            contenuto = Replace(contenuto, "$quart2$", " ")
                            contenuto = Replace(contenuto, "$quart3$", " ")
                            contenuto = Replace(contenuto, "$quart4$", " ")
                            contenuto = Replace(contenuto, "$quart5$", " ")
                            contenuto = Replace(contenuto, "$localita1ex$", " ")
                            contenuto = Replace(contenuto, "$localita2ex$", " ")
                            contenuto = Replace(contenuto, "$localita3ex$", " ")
                            contenuto = Replace(contenuto, "$localita4ex$", " ")
                            contenuto = Replace(contenuto, "$localita5ex$", " ")
                            contenuto = Replace(contenuto, "$quart1ex$", " ")
                            contenuto = Replace(contenuto, "$quart2ex$", " ")
                            contenuto = Replace(contenuto, "$quart3ex$", " ")
                            contenuto = Replace(contenuto, "$quart4ex$", " ")
                            contenuto = Replace(contenuto, "$quart5ex$", " ")
                            contenuto = Replace(contenuto, "$supMin$", " ")
                            contenuto = Replace(contenuto, "$supMax$", " ")
                            contenuto = Replace(contenuto, "$pianoDaCon$", " ")
                            contenuto = Replace(contenuto, "$pianoACon$", " ")
                            contenuto = Replace(contenuto, "$pianoDaSenza$", " ")
                            contenuto = Replace(contenuto, "$pianoASenza$", " ")
                            contenuto = Replace(contenuto, "$note$", " ")
                            contenuto = Replace(contenuto, "$barrArchit$", " ")
                            contenuto = Replace(contenuto, "$condominio$", " ")





                        End If
                        myReaderJ.Close()





                    Case "BANDO CAMBI"
                        par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita1) as pref_localita1, " _
                                                        & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita2) as pref_localita2, " _
                                                        & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita3) as pref_localita3, " _
                                                        & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita4) as pref_localita4, " _
                                                        & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita5) as pref_localita5, " _
                                                        & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona1) as pref_zona1,  " _
                                                        & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona2) as pref_zona2,  " _
                                                        & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona3) as pref_zona3, " _
                                                        & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona4) as pref_zona4, " _
                                                        & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona5) as pref_zona5, " _
                                                        & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart1) as pref_quart1, " _
                                                        & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart2) as pref_quart2,  " _
                                                        & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart3) as pref_quart3, " _
                                                        & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart4) as pref_quart4, " _
                                                        & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart5) as pref_quart5, " _
                                                        & " domande_preferenze_cambi.pref_indirizzo1, domande_preferenze_cambi.pref_indirizzo2, domande_preferenze_cambi.pref_indirizzo3, domande_preferenze_cambi.pref_indirizzo4,domande_preferenze_cambi.pref_indirizzo5," _
                                                        & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita1) as escl_localita1, " _
                                                        & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita2) as escl_localita2, " _
                                                        & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita3) as escl_localita3, " _
                                                        & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita4) as escl_localita4, " _
                                                        & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita5) as escl_localita5, " _
                                                        & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona1) as escl_zona1, " _
                                                        & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona2) as escl_zona2, " _
                                                        & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona3) as escl_zona3, " _
                                                        & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona4) as escl_zona4, " _
                                                        & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona5) as escl_zona5, " _
                                                        & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart1) as escl_quart1, " _
                                                        & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart2) as escl_quart2, " _
                                                        & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart3) as escl_quart3, " _
                                                        & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart4) as escl_quart4, " _
                                                        & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart5) as escl_quart5, " _
                                                        & " domande_preferenze_escl_cambi.escl_indirizzo1, domande_preferenze_escl_cambi.escl_indirizzo2, domande_preferenze_escl_cambi.escl_indirizzo3, domande_preferenze_escl_cambi.escl_indirizzo4,domande_preferenze_escl_cambi.escl_indirizzo5, " _
                                                        & " domande_preferenze_cambi.pref_sup_max, domande_preferenze_cambi.pref_sup_min, " _
                                                        & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_cambi.pref_piani_da_con) as pref_piani_da_con, " _
                                                        & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_cambi.pref_piani_a_con) as pref_piani_a_con, " _
                                                        & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_cambi.pref_piani_da_senza) as pref_piani_da_senza, " _
                                                        & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_cambi.pref_piani_a_senza) as pref_piani_a_senza, " _
                                           & " (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS richiedente, domande_preferenze_cambi.pref_note AS note, domande_preferenze_cambi.pref_barriere AS richieste_particolari, " _
                                           & " domande_bando_cambi.pg as PG, " _
                                           & " domande_preferenze_cambi.pref_condominio AS condominio " _
                                           & " FROM domande_preferenze_cambi, domande_preferenze_escl_cambi, domande_bando_cambi, comp_nucleo_cambi " _
                                           & " WHERE domande_preferenze_cambi.id_domanda = domande_preferenze_escl_cambi.id_domanda " _
                                           & " AND domande_bando_cambi.ID = domande_preferenze_escl_cambi.id_domanda " _
                                           & " And domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione " _
                                           & " And comp_nucleo_cambi.progr = 0 " _
                                           & " And domande_preferenze_cambi.id_domanda = " & idDomanda.Value & ""

                        Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderJ.Read Then

                            contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderJ("richiedente"), "_______________________"))

                            contenuto = Replace(contenuto, "$PG$", par.IfNull(myReaderJ("PG"), " "))

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

                                contenuto = Replace(contenuto, "$supMin$", par.IfNull(FormatNumber(myReaderJ("pref_sup_min"), 2), " "))

                            Else

                                contenuto = Replace(contenuto, "$supMin$", " ")

                            End If


                            If par.IfNull(myReaderJ("pref_sup_max"), "").ToString <> "" Then

                                contenuto = Replace(contenuto, "$supMax$", par.IfNull(FormatNumber(myReaderJ("pref_sup_max"), 2), " "))

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


                            contenuto = Replace(contenuto, "$note$", par.IfNull(myReaderJ("note"), " "))

                            If par.IfNull(myReaderJ("richieste_particolari"), "") = "1" Then
                                contenuto = Replace(contenuto, "$barrArchit$", " Barriere Architettoniche escluse")
                            Else

                                contenuto = Replace(contenuto, "$barrArchit$", " ---")
                            End If


                            If par.IfNull(myReaderJ("condominio"), "") = "1" Then
                                contenuto = Replace(contenuto, "$condominio$", " SI")
                            Else

                                contenuto = Replace(contenuto, "$condominio$", " NO")
                            End If



                        Else

                            par.cmd.CommandText = " SELECT (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS richiedente, domande_bando_cambi.pg AS pg " _
                                            & "  FROM domande_bando_cambi, comp_nucleo_cambi " _
                                            & " WHERE domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione " _
                                            & " And comp_nucleo_cambi.progr = 0 AND domande_bando_cambi.ID =" & idDomanda.Value & ""


                            Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderK.Read Then

                                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderK("richiedente"), "_______________________"))
                                contenuto = Replace(contenuto, "$PG$", par.IfNull(myReaderK("PG"), " "))

                            End If
                            myReaderK.Close()


                            contenuto = Replace(contenuto, "$localita1$", " ")
                            contenuto = Replace(contenuto, "$localita2$", " ")
                            contenuto = Replace(contenuto, "$localita3$", " ")
                            contenuto = Replace(contenuto, "$localita4$", " ")
                            contenuto = Replace(contenuto, "$localita5$", " ")
                            contenuto = Replace(contenuto, "$quart1$", " ")
                            contenuto = Replace(contenuto, "$quart2$", " ")
                            contenuto = Replace(contenuto, "$quart3$", " ")
                            contenuto = Replace(contenuto, "$quart4$", " ")
                            contenuto = Replace(contenuto, "$quart5$", " ")
                            contenuto = Replace(contenuto, "$localita1ex$", " ")
                            contenuto = Replace(contenuto, "$localita2ex$", " ")
                            contenuto = Replace(contenuto, "$localita3ex$", " ")
                            contenuto = Replace(contenuto, "$localita4ex$", " ")
                            contenuto = Replace(contenuto, "$localita5ex$", " ")
                            contenuto = Replace(contenuto, "$quart1ex$", " ")
                            contenuto = Replace(contenuto, "$quart2ex$", " ")
                            contenuto = Replace(contenuto, "$quart3ex$", " ")
                            contenuto = Replace(contenuto, "$quart4ex$", " ")
                            contenuto = Replace(contenuto, "$quart5ex$", " ")
                            contenuto = Replace(contenuto, "$supMin$", " ")
                            contenuto = Replace(contenuto, "$supMax$", " ")
                            contenuto = Replace(contenuto, "$pianoACon$", " ")
                            contenuto = Replace(contenuto, "$pianoDaCon$", " ")
                            contenuto = Replace(contenuto, "$pianoASenza$", " ")
                            contenuto = Replace(contenuto, "$pianoDaSenza$", " ")
                            contenuto = Replace(contenuto, "$note$", " ")
                            contenuto = Replace(contenuto, "$barrArchit$", " ")
                            contenuto = Replace(contenuto, "$condominio$", " ")



                        End If
                        myReaderJ.Close()


                    Case Else
                        par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita1) as pref_localita1, " _
                                       & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita2) as pref_localita2, " _
                                       & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita3) as pref_localita3, " _
                                       & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita4) as pref_localita4, " _
                                       & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita5) as pref_localita5, " _
                                       & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona1) as pref_zona1,  " _
                                       & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona2) as pref_zona2,  " _
                                       & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona3) as pref_zona3, " _
                                       & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona4) as pref_zona4, " _
                                       & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona5) as pref_zona5, " _
                                       & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart1) as pref_quart1, " _
                                       & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart2) as pref_quart2,  " _
                                       & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart3) as pref_quart3, " _
                                       & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart4) as pref_quart4, " _
                                       & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart5) as pref_quart5, " _
                                       & " domande_preferenze.pref_indirizzo1, domande_preferenze.pref_indirizzo2, domande_preferenze.pref_indirizzo3, domande_preferenze.pref_indirizzo4,domande_preferenze.pref_indirizzo5," _
                                       & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita1) as escl_localita1, " _
                                       & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita2) as escl_localita2, " _
                                       & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita3) as escl_localita3, " _
                                       & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita4) as escl_localita4, " _
                                       & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita5) as escl_localita5, " _
                                       & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona1) as escl_zona1, " _
                                       & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona2) as escl_zona2, " _
                                       & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona3) as escl_zona3, " _
                                       & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona4) as escl_zona4, " _
                                       & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona5) as escl_zona5, " _
                                       & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart1) as escl_quart1, " _
                                       & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart2) as escl_quart2, " _
                                       & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart3) as escl_quart3, " _
                                       & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart4) as escl_quart4, " _
                                       & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart5) as escl_quart5, " _
                                       & " domande_preferenze_esclusioni.escl_indirizzo1, domande_preferenze_esclusioni.escl_indirizzo2, domande_preferenze_esclusioni.escl_indirizzo3,domande_preferenze_esclusioni.escl_indirizzo4,domande_preferenze_esclusioni.escl_indirizzo5, " _
                                       & " domande_preferenze.pref_sup_max, domande_preferenze.pref_sup_min, " _
                                       & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze.pref_piani_da_con) as pref_piani_da_con, " _
                                       & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze.pref_piani_a_con) as pref_piani_a_con, " _
                                       & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze.pref_piani_a_senza) as pref_piani_a_senza, " _
                                       & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze.pref_piani_da_senza) as pref_piani_da_senza, " _
                                       & " (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS richiedente, domande_preferenze.pref_note AS note, domande_preferenze.pref_barriere AS richieste_particolari, " _
                                              & " domande_preferenze.pref_condominio AS condominio, " _
                                              & " domande_bando.pg as PG " _
                                              & " FROM domande_preferenze, domande_preferenze_esclusioni, domande_bando, comp_nucleo " _
                                              & " WHERE domande_preferenze.id_domanda = domande_preferenze_esclusioni.id_domanda " _
                                              & " AND domande_bando.ID = domande_preferenze_esclusioni.id_domanda " _
                                              & " And domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione " _
                                              & " And comp_nucleo.progr = 0 " _
                                              & " And domande_preferenze.id_domanda = " & idDomanda.Value & ""


                        Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderJ.Read Then

                            contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderJ("richiedente"), "_______________________"))
                            contenuto = Replace(contenuto, "$PG$", par.IfNull(myReaderJ("PG"), " "))

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

                                contenuto = Replace(contenuto, "$supMin$", par.IfNull(FormatNumber(myReaderJ("pref_sup_min"), 2), " "))

                            Else

                                contenuto = Replace(contenuto, "$supMin$", " ")

                            End If


                            If par.IfNull(myReaderJ("pref_sup_max"), "").ToString <> "" Then

                                contenuto = Replace(contenuto, "$supMax$", par.IfNull(FormatNumber(myReaderJ("pref_sup_max"), 2), " "))

                            Else
                                contenuto = Replace(contenuto, "$supMax$", " ")
                            End If


                            contenuto = Replace(contenuto, "$pianoACon$", par.IfNull(myReaderJ("pref_piani_a_con"), " "))
                            contenuto = Replace(contenuto, "$pianoDaCon$", par.IfNull(myReaderJ("pref_piani_da_con"), " "))
                            contenuto = Replace(contenuto, "$pianoASenza$", par.IfNull(myReaderJ("pref_piani_a_senza"), " "))
                            contenuto = Replace(contenuto, "$pianoDaSenza$", par.IfNull(myReaderJ("pref_piani_da_senza"), " "))
                            
                            contenuto = Replace(contenuto, "$note$", par.IfNull(myReaderJ("note"), " "))

                            If par.IfNull(myReaderJ("richieste_particolari"), "") = "1" Then
                                contenuto = Replace(contenuto, "$barrArchit$", " Barriere Architettoniche escluse")
                            Else

                                contenuto = Replace(contenuto, "$barrArchit$", " ---")
                            End If


                            If par.IfNull(myReaderJ("condominio"), "") = "1" Then
                                contenuto = Replace(contenuto, "$condominio$", " SI")
                            Else

                                contenuto = Replace(contenuto, "$condominio$", " NO")
                            End If



                        Else

                            par.cmd.CommandText = " SELECT (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS richiedente, domande_bando.pg AS pg " _
                                          & "  FROM domande_bando, comp_nucleo " _
                                          & " WHERE domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione " _
                                          & " And comp_nucleo.progr = 0 AND domande_bando.ID =" & idDomanda.Value & ""


                            Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderK.Read Then



                                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderK("richiedente"), "_______________________"))
                                contenuto = Replace(contenuto, "$PG$", par.IfNull(myReaderK("PG"), " "))

                            End If
                            myReaderK.Close()

                            contenuto = Replace(contenuto, "$localita1$", " ")
                            contenuto = Replace(contenuto, "$localita2$", " ")
                            contenuto = Replace(contenuto, "$localita3$", " ")
                            contenuto = Replace(contenuto, "$localita4$", " ")
                            contenuto = Replace(contenuto, "$localita5$", " ")
                            contenuto = Replace(contenuto, "$quart1$", " ")
                            contenuto = Replace(contenuto, "$quart2$", " ")
                            contenuto = Replace(contenuto, "$quart3$", " ")
                            contenuto = Replace(contenuto, "$quart4$", " ")
                            contenuto = Replace(contenuto, "$quart5$", " ")
                            contenuto = Replace(contenuto, "$localita1ex$", " ")
                            contenuto = Replace(contenuto, "$localita2ex$", " ")
                            contenuto = Replace(contenuto, "$localita3ex$", " ")
                            contenuto = Replace(contenuto, "$localita4ex$", " ")
                            contenuto = Replace(contenuto, "$localita5ex$", " ")
                            contenuto = Replace(contenuto, "$quart1ex$", " ")
                            contenuto = Replace(contenuto, "$quart2ex$", " ")
                            contenuto = Replace(contenuto, "$quart3ex$", " ")
                            contenuto = Replace(contenuto, "$quart4ex$", " ")
                            contenuto = Replace(contenuto, "$quart5ex$", " ")
                            contenuto = Replace(contenuto, "$supMin$", " ")
                            contenuto = Replace(contenuto, "$supMax$", " ")
                            contenuto = Replace(contenuto, "$pianoDaSenza$", " ")
                            contenuto = Replace(contenuto, "$pianoASenza$", " ")
                            contenuto = Replace(contenuto, "$pianoDaCon$", " ")
                            contenuto = Replace(contenuto, "$pianoACon$", " ")
                            contenuto = Replace(contenuto, "$note$", " ")
                            contenuto = Replace(contenuto, "$barrArchit$", " ")
                            contenuto = Replace(contenuto, "$condominio$", " ")




                        End If
                        myReaderJ.Close()


                End Select




            End If


            contenuto = Replace(contenuto, "$dataOggi$", DateTime.Now.ToString("dd/MM/yyyy"))

            Dim PercorsoBarCode As String = par.RicavaBarCode(26, idDomanda.Value)
            contenuto = Replace(contenuto, "$barcode$", Server.MapPath("..\..\FileTemp\") & PercorsoBarCode)



            Dim url As String = Server.MapPath("..\..\FileTemp\")
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
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\FileTemp\"))



            'Response.Write("<script>window.open('../FileTemp/" & nomefile & "','Modulo','');</script>")
            Response.Redirect("..\..\FileTemp\" & nomefile, False)




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
