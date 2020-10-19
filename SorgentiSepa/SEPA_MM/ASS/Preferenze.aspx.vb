
Partial Class ASS_Preferenze
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim s_Stringasql As String




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try


            If Not IsPostBack Then

                sValoreID.Value = par.PulisciStrSql(Request.QueryString("ID"))
                Tipo.Value = par.PulisciStrSql(Request.QueryString("T"))
                Provenienza.Value = par.PulisciStrSql(Request.QueryString("PROV"))


                If Provenienza.Value <> "1" Then
                    If Tipo.Value = "2" Then

                        Tipo.Value = "ART.22 C.10"


                    End If

                    If Tipo.Value = "1" Then

                        Tipo.Value = "BANDO CAMBI"


                    End If
                End If
                CaricaDati()

            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub



    Private Sub CaricaDati()

        Try
            '*****************APERTURA CONNESSIONE***************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If



            If Provenienza.Value = "1" Then

                If Tipo.Value = "1" Then

                    par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita1) as pref_localita1, " _
                                         & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita2) as pref_localita2, " _
                                         & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita3) as pref_localita3, " _
                                         & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona1) as pref_zona1,  " _
                                         & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona2) as pref_zona2,  " _
                                         & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona3) as pref_zona3, " _
                                         & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart1) as pref_quart1, " _
                                         & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart2) as pref_quart2,  " _
                                         & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart3) as pref_quart3, " _
                                            & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze.pref_complesso1) AS pref_complesso1, " _
                                           & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze.pref_complesso2) AS pref_complesso2, " _
                                           & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze.pref_complesso3) AS pref_complesso3, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze.pref_edificio1) AS pref_edificio1, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze.pref_edificio2) AS pref_edificio2, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze.pref_edificio3) AS pref_edificio3, " _
                                         & " domande_preferenze.pref_indirizzo1, domande_preferenze.pref_indirizzo2, domande_preferenze.pref_indirizzo3, " _
                                         & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita1) as escl_localita1, " _
                                         & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita2) as escl_localita2, " _
                                         & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita3) as escl_localita3, " _
                                         & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona1) as escl_zona1, " _
                                         & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona2) as escl_zona2, " _
                                         & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona3) as escl_zona3, " _
                                         & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart1) as escl_quart1, " _
                                         & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart2) as escl_quart2, " _
                                         & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart3) as escl_quart3, " _
                                         & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_esclusioni.escl_complesso1) AS escl_complesso1, " _
                                           & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_esclusioni.escl_complesso2) AS escl_complesso2, " _
                                           & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_esclusioni.escl_complesso3) AS escl_complesso3, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_esclusioni.escl_edificio1) AS escl_edificio1, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_esclusioni.escl_edificio2) AS escl_edificio2, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_esclusioni.escl_edificio3) AS escl_edificio3, " _
                                         & " domande_preferenze_esclusioni.escl_indirizzo1, domande_preferenze_esclusioni.escl_indirizzo2, domande_preferenze_esclusioni.escl_indirizzo3, " _
                                         & " domande_preferenze.pref_sup_max, domande_preferenze.pref_sup_min, " _
                                         & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_esclusioni.escl_piano_sa1) as escl_piano_sa1, " _
                                         & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_esclusioni.escl_piano_sa2) as escl_piano_sa2, " _
                                         & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_esclusioni.escl_piano_sa3) as escl_piano_sa3, " _
                                         & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_esclusioni.escl_piano_ca1) as escl_piano_ca1, " _
                                         & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_esclusioni.escl_piano_ca2) as escl_piano_ca2, " _
                                         & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_esclusioni.escl_piano_ca3) as escl_piano_ca3, " _
                                         & " (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS richiedente, domande_preferenze.pref_note AS note, domande_preferenze.pref_barriere AS barr, " _
                                         & " domande_bando.pg as PG, " _
                                         & " domande_preferenze.pref_condominio AS condominio " _
                                         & " FROM domande_preferenze, domande_preferenze_esclusioni, domande_bando, comp_nucleo " _
                                         & " WHERE domande_preferenze.id_domanda = domande_preferenze_esclusioni.id_domanda " _
                                         & " AND domande_bando.ID = domande_preferenze_esclusioni.id_domanda " _
                                         & " And domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione " _
                                         & " And comp_nucleo.progr = 0 " _
                                         & " And domande_preferenze.id_domanda = " & sValoreID.Value & ""



                Else
                    par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita1) as pref_localita1, " _
                                                            & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita2) as pref_localita2, " _
                                                            & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita3) as pref_localita3, " _
                                                            & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona1) as pref_zona1,  " _
                                                            & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona2) as pref_zona2,  " _
                                                            & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona3) as pref_zona3, " _
                                                            & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart1) as pref_quart1, " _
                                                            & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart2) as pref_quart2,  " _
                                                            & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart3) as pref_quart3, " _
                                                             & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_cambi.pref_complesso1) AS pref_complesso1, " _
                                                            & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_cambi.pref_complesso2) AS pref_complesso2, " _
                                                            & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_cambi.pref_complesso3) AS pref_complesso3, " _
                                                            & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_cambi.pref_edificio1) AS pref_edificio1, " _
                                                            & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_cambi.pref_edificio2) AS pref_edificio2, " _
                                                            & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_cambi.pref_edificio3) AS pref_edificio3, " _
                                                            & " domande_preferenze_cambi.pref_indirizzo1, domande_preferenze_cambi.pref_indirizzo2, domande_preferenze_cambi.pref_indirizzo3, " _
                                                            & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita1) as escl_localita1, " _
                                                            & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita2) as escl_localita2, " _
                                                            & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita3) as escl_localita3, " _
                                                            & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona1) as escl_zona1, " _
                                                            & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona2) as escl_zona2, " _
                                                            & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona3) as escl_zona3, " _
                                                            & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart1) as escl_quart1, " _
                                                            & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart2) as escl_quart2, " _
                                                            & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart3) as escl_quart3, " _
                                                             & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_escl_cambi.escl_complesso1) AS escl_complesso1, " _
                                                            & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_escl_cambi.escl_complesso2) AS escl_complesso2, " _
                                                            & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_escl_cambi.escl_complesso3) AS escl_complesso3, " _
                                                            & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_escl_cambi.escl_edificio1) AS escl_edificio1, " _
                                                            & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_escl_cambi.escl_edificio2) AS escl_edificio2, " _
                                                            & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_escl_cambi.escl_edificio3) AS escl_edificio3, " _
                                                            & " domande_preferenze_escl_cambi.escl_indirizzo1, domande_preferenze_escl_cambi.escl_indirizzo2, domande_preferenze_escl_cambi.escl_indirizzo3, " _
                                                            & " domande_preferenze_cambi.pref_sup_max, domande_preferenze_cambi.pref_sup_min, " _
                                                            & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_cambi.escl_piano_sa1) as escl_piano_sa1, " _
                                                            & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_cambi.escl_piano_sa2) as escl_piano_sa2, " _
                                                            & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_cambi.escl_piano_sa3) as escl_piano_sa3, " _
                                                            & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_cambi.escl_piano_ca1) as escl_piano_ca1, " _
                                                            & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_cambi.escl_piano_ca2) as escl_piano_ca2, " _
                                                            & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_cambi.escl_piano_ca3) as escl_piano_ca3, " _
                                                              & " (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS richiedente, domande_preferenze_cambi.pref_note AS note, domande_preferenze_cambi.pref_barriere AS barr, " _
                                                              & " domande_bando_cambi.pg as PG, " _
                                                              & " domande_preferenze_cambi.pref_condominio AS condominio " _
                                                              & " FROM domande_preferenze_cambi, domande_preferenze_escl_cambi, domande_bando_cambi, comp_nucleo_cambi " _
                                                              & " WHERE domande_preferenze_cambi.id_domanda = domande_preferenze_escl_cambi.id_domanda " _
                                                              & " AND domande_bando_cambi.ID = domande_preferenze_escl_cambi.id_domanda " _
                                                              & " And domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione " _
                                                              & " And comp_nucleo_cambi.progr = 0 " _
                                                              & " And domande_preferenze_cambi.id_domanda = " & sValoreID.Value & ""



                End If



            Else


                    Select Tipo.Value

                    Case "ART.22 C.10"

                        par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_vsa.pref_localita1) as pref_localita1, " _
                                          & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_vsa.pref_localita2) as pref_localita2, " _
                                          & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_vsa.pref_localita3) as pref_localita3, " _
                                          & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_vsa.pref_zona1) as pref_zona1,  " _
                                          & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_vsa.pref_zona2) as pref_zona2,  " _
                                          & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_vsa.pref_zona3) as pref_zona3, " _
                                          & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_vsa.pref_quart1) as pref_quart1, " _
                                          & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_vsa.pref_quart2) as pref_quart2,  " _
                                          & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_vsa.pref_quart3) as pref_quart3, " _
                                          & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_vsa.pref_complesso1) AS pref_complesso1, " _
                                          & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_vsa.pref_complesso2) AS pref_complesso2, " _
                                          & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_vsa.pref_complesso3) AS pref_complesso3, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_vsa.pref_edificio1) AS pref_edificio1, " _
                                          & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_vsa.pref_edificio2) AS pref_edificio2, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_vsa.pref_edificio3) AS pref_edificio3, " _
                                          & " domande_preferenze_vsa.pref_indirizzo1, domande_preferenze_vsa.pref_indirizzo2, domande_preferenze_vsa.pref_indirizzo3, " _
                                          & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_vsa.escl_localita1) as escl_localita1, " _
                                          & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_vsa.escl_localita2) as escl_localita2, " _
                                          & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_vsa.escl_localita3) as escl_localita3, " _
                                          & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_vsa.escl_zona1) as escl_zona1, " _
                                          & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_vsa.escl_zona2) as escl_zona2, " _
                                          & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_vsa.escl_zona3) as escl_zona3, " _
                                          & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_vsa.escl_quart1) as escl_quart1, " _
                                          & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_vsa.escl_quart2) as escl_quart2, " _
                                          & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_vsa.escl_quart3) as escl_quart3, " _
                                          & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_escl_vsa.escl_complesso1) AS escl_complesso1, " _
                                          & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_escl_vsa.escl_complesso2) AS escl_complesso2, " _
                                          & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_escl_vsa.escl_complesso3) AS escl_complesso3, " _
                                          & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_escl_vsa.escl_edificio1) AS escl_edificio1, " _
                                          & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_escl_vsa.escl_edificio2) AS escl_edificio2, " _
                                          & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_escl_vsa.escl_edificio3) AS escl_edificio3, " _
                                          & " domande_preferenze_escl_vsa.escl_indirizzo1, domande_preferenze_escl_vsa.escl_indirizzo2, domande_preferenze_escl_vsa.escl_indirizzo3, " _
                                          & " domande_preferenze_vsa.pref_sup_max, domande_preferenze_vsa.pref_sup_min, " _
                                          & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_vsa.escl_piano_sa1) as escl_piano_sa1, " _
                                          & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_vsa.escl_piano_sa2) as escl_piano_sa2, " _
                                          & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_vsa.escl_piano_sa3) as escl_piano_sa3, " _
                                          & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_vsa.escl_piano_ca1) as escl_piano_ca1, " _
                                          & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_vsa.escl_piano_ca2) as escl_piano_ca2, " _
                                          & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_vsa.escl_piano_ca3) as escl_piano_ca3, " _
                                         & " (comp_nucleo_vsa.cognome || ' ' || comp_nucleo_vsa.nome) AS richiedente, domande_preferenze_vsa.pref_note AS note, domande_preferenze_vsa.pref_barriere AS barr, " _
                                         & " domande_bando_vsa.pg as PG, " _
                                         & " domande_preferenze_vsa.pref_condominio AS condominio " _
                                         & " FROM domande_preferenze_vsa, domande_preferenze_escl_vsa, domande_bando_vsa, comp_nucleo_vsa " _
                                         & " WHERE domande_preferenze_vsa.id_domanda = domande_preferenze_escl_vsa.id_domanda " _
                                         & " AND domande_bando_vsa.ID = domande_preferenze_escl_vsa.id_domanda " _
                                         & " And domande_bando_vsa.id_dichiarazione = comp_nucleo_vsa.id_dichiarazione " _
                                         & " And comp_nucleo_vsa.progr = 0 " _
                                         & " And domande_preferenze_vsa.id_domanda = " & sValoreID.Value & ""





                    Case "BANDO CAMBI"


                        par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita1) as pref_localita1, " _
                                                            & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita2) as pref_localita2, " _
                                                            & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_cambi.pref_localita3) as pref_localita3, " _
                                                            & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona1) as pref_zona1,  " _
                                                            & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona2) as pref_zona2,  " _
                                                            & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_cambi.pref_zona3) as pref_zona3, " _
                                                            & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart1) as pref_quart1, " _
                                                            & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart2) as pref_quart2,  " _
                                                            & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_cambi.pref_quart3) as pref_quart3, " _
                                                             & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_cambi.pref_complesso1) AS pref_complesso1, " _
                                                            & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_cambi.pref_complesso2) AS pref_complesso2, " _
                                                            & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_cambi.pref_complesso3) AS pref_complesso3, " _
                                                            & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_cambi.pref_edificio1) AS pref_edificio1, " _
                                                            & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_cambi.pref_edificio2) AS pref_edificio2, " _
                                                            & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_cambi.pref_edificio3) AS pref_edificio3, " _
                                                            & " domande_preferenze_cambi.pref_indirizzo1, domande_preferenze_cambi.pref_indirizzo2, domande_preferenze_cambi.pref_indirizzo3, " _
                                                            & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita1) as escl_localita1, " _
                                                            & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita2) as escl_localita2, " _
                                                            & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_escl_cambi.escl_localita3) as escl_localita3, " _
                                                            & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona1) as escl_zona1, " _
                                                            & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona2) as escl_zona2, " _
                                                            & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_escl_cambi.escl_zona3) as escl_zona3, " _
                                                            & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart1) as escl_quart1, " _
                                                            & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart2) as escl_quart2, " _
                                                            & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_escl_cambi.escl_quart3) as escl_quart3, " _
                                                            & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_escl_cambi.escl_complesso1) AS escl_complesso1, " _
                                                            & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_escl_cambi.escl_complesso2) AS escl_complesso2, " _
                                                            & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_escl_cambi.escl_complesso3) AS escl_complesso3, " _
                                                            & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_escl_cambi.escl_edificio1) AS escl_edificio1, " _
                                                            & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_escl_cambi.escl_edificio2) AS escl_edificio2, " _
                                                            & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_escl_cambi.escl_edificio3) AS escl_edificio3, " _
                                                            & " domande_preferenze_escl_cambi.escl_indirizzo1, domande_preferenze_escl_cambi.escl_indirizzo2, domande_preferenze_escl_cambi.escl_indirizzo3, " _
                                                            & " domande_preferenze_cambi.pref_sup_max, domande_preferenze_cambi.pref_sup_min, " _
                                                            & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_cambi.escl_piano_sa1) as escl_piano_sa1, " _
                                                            & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_cambi.escl_piano_sa2) as escl_piano_sa2, " _
                                                            & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_cambi.escl_piano_sa3) as escl_piano_sa3, " _
                                                            & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_cambi.escl_piano_ca1) as escl_piano_ca1, " _
                                                            & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_cambi.escl_piano_ca2) as escl_piano_ca2, " _
                                                            & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_escl_cambi.escl_piano_ca3) as escl_piano_ca3, " _
                                                              & " (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS richiedente, domande_preferenze_cambi.pref_note AS note, domande_preferenze_cambi.pref_barriere AS barr, " _
                                                              & " domande_bando_cambi.pg as PG, " _
                                                              & " domande_preferenze_cambi.pref_condominio AS condominio " _
                                                              & " FROM domande_preferenze_cambi, domande_preferenze_escl_cambi, domande_bando_cambi, comp_nucleo_cambi " _
                                                              & " WHERE domande_preferenze_cambi.id_domanda = domande_preferenze_escl_cambi.id_domanda " _
                                                              & " AND domande_bando_cambi.ID = domande_preferenze_escl_cambi.id_domanda " _
                                                              & " And domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione " _
                                                              & " And comp_nucleo_cambi.progr = 0 " _
                                                              & " And domande_preferenze_cambi.id_domanda = " & sValoreID.Value & ""






                    Case Else


                        par.cmd.CommandText = "SELECT (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita1) as pref_localita1, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita2) as pref_localita2, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze.pref_localita3) as pref_localita3, " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona1) as pref_zona1,  " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona2) as pref_zona2,  " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze.pref_zona3) as pref_zona3, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart1) as pref_quart1, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart2) as pref_quart2,  " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze.pref_quart3) as pref_quart3, " _
                                           & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze.pref_complesso1) AS pref_complesso1, " _
                                           & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze.pref_complesso2) AS pref_complesso2, " _
                                           & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze.pref_complesso3) AS pref_complesso3, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze.pref_edificio1) AS pref_edificio1, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze.pref_edificio2) AS pref_edificio2, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze.pref_edificio3) AS pref_edificio3, " _
                                           & " domande_preferenze.pref_indirizzo1, domande_preferenze.pref_indirizzo2, domande_preferenze.pref_indirizzo3, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita1) as escl_localita1, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita2) as escl_localita2, " _
                                           & " (SELECT comuni_nazioni.nome FROM comuni_nazioni WHERE comuni_nazioni.ID =domande_preferenze_esclusioni.escl_localita3) as escl_localita3, " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona1) as escl_zona1, " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona2) as escl_zona2, " _
                                           & " (select zona from zona_aler where zona_aler.cod =domande_preferenze_esclusioni.escl_zona3) as escl_zona3, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart1) as escl_quart1, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart2) as escl_quart2, " _
                                           & " (SELECT tab_quartieri.nome FROM  siscom_mi.tab_quartieri where tab_quartieri.id =domande_preferenze_esclusioni.escl_quart3) as escl_quart3, " _
                                           & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_esclusioni.escl_complesso1) AS escl_complesso1, " _
                                           & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_esclusioni.escl_complesso2) AS escl_complesso2, " _
                                           & " (SELECT complessi_immobiliari.denominazione FROM siscom_mi.complessi_immobiliari WHERE complessi_immobiliari.ID = domande_preferenze_esclusioni.escl_complesso3) AS escl_complesso3, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_esclusioni.escl_edificio1) AS escl_edificio1, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_esclusioni.escl_edificio2) AS escl_edificio2, " _
                                           & " (SELECT edifici.denominazione FROM siscom_mi.edifici WHERE edifici.ID = domande_preferenze_esclusioni.escl_edificio3) AS escl_edificio3, " _
                                           & " domande_preferenze_esclusioni.escl_indirizzo1, domande_preferenze_esclusioni.escl_indirizzo2, domande_preferenze_esclusioni.escl_indirizzo3, " _
                                           & " domande_preferenze.pref_sup_max, domande_preferenze.pref_sup_min, " _
                                           & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_esclusioni.escl_piano_sa1) as escl_piano_sa1, " _
                                           & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_esclusioni.escl_piano_sa2) as escl_piano_sa2, " _
                                           & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_esclusioni.escl_piano_sa3) as escl_piano_sa3, " _
                                           & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_esclusioni.escl_piano_ca1) as escl_piano_ca1, " _
                                           & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_esclusioni.escl_piano_ca2) as escl_piano_ca2, " _
                                           & " (SELECT descrizione FROM SISCOM_MI.TIPO_LIVELLO_PIANO where tipo_livello_piano.cod =domande_preferenze_esclusioni.escl_piano_ca3) as escl_piano_ca3, " _
                                           & " (comp_nucleo.cognome || ' ' || comp_nucleo.nome) AS richiedente, domande_preferenze.pref_note AS note, domande_preferenze.pref_barriere AS barr, " _
                                           & " domande_bando.pg as PG, " _
                                           & " domande_preferenze.pref_condominio AS condominio " _
                                           & " FROM domande_preferenze, domande_preferenze_esclusioni, domande_bando, comp_nucleo " _
                                           & " WHERE domande_preferenze.id_domanda = domande_preferenze_esclusioni.id_domanda " _
                                           & " AND domande_bando.ID = domande_preferenze_esclusioni.id_domanda " _
                                           & " And domande_bando.id_dichiarazione = comp_nucleo.id_dichiarazione " _
                                           & " And comp_nucleo.progr = 0 " _
                                           & " And domande_preferenze.id_domanda = " & sValoreID.Value & ""








                End Select


            End If















            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then


                lbl_localita1.Text = par.IfNull(myReader("pref_localita1"), "---")
                lbl_localita2.Text = par.IfNull(myReader("pref_localita2"), "---")
                lbl_localita3.Text = par.IfNull(myReader("pref_localita3"), "---")


                lbl_zona1.Text = par.IfNull(myReader("pref_zona1"), "---")
                lbl_zona2.Text = par.IfNull(myReader("pref_zona2"), "---")
                lbl_zona3.Text = par.IfNull(myReader("pref_zona3"), "---")


                lbl_quart1.Text = par.IfNull(myReader("pref_quart1"), "---")
                lbl_quart2.Text = par.IfNull(myReader("pref_quart2"), "---")
                lbl_quart3.Text = par.IfNull(myReader("pref_quart3"), "---")


                lbl_complesso1.Text = par.IfNull(myReader("pref_complesso1"), "---")
                lbl_complesso2.Text = par.IfNull(myReader("pref_complesso2"), "---")
                lbl_complesso3.Text = par.IfNull(myReader("pref_complesso3"), "---")

                lbl_edificio1.Text = par.IfNull(myReader("pref_edificio1"), "---")
                lbl_edificio2.Text = par.IfNull(myReader("pref_edificio2"), "---")
                lbl_edificio3.Text = par.IfNull(myReader("pref_edificio3"), "---")


                lbl_indirizzo1.Text = par.IfNull(myReader("pref_indirizzo1"), "---")
                lbl_indirizzo2.Text = par.IfNull(myReader("pref_indirizzo2"), "---")
                lbl_indirizzo3.Text = par.IfNull(myReader("pref_indirizzo3"), "---")




                lbl_localita1ex.Text = par.IfNull(myReader("escl_localita1"), "---")
                lbl_localita2ex.Text = par.IfNull(myReader("escl_localita2"), "---")
                lbl_localita3ex.Text = par.IfNull(myReader("escl_localita3"), "---")


                lbl_zona1ex.Text = par.IfNull(myReader("escl_zona1"), "---")
                lbl_zona2ex.Text = par.IfNull(myReader("escl_zona2"), "---")
                lbl_zona3ex.Text = par.IfNull(myReader("escl_zona3"), "---")


                lbl_quart1ex.Text = par.IfNull(myReader("escl_quart1"), "---")
                lbl_quart2ex.Text = par.IfNull(myReader("escl_quart2"), "---")
                lbl_quart3ex.Text = par.IfNull(myReader("escl_quart3"), "---")


                lbl_complesso1ex.Text = par.IfNull(myReader("escl_complesso1"), "---")
                lbl_complesso2ex.Text = par.IfNull(myReader("escl_complesso2"), "---")
                lbl_complesso3ex.Text = par.IfNull(myReader("escl_complesso3"), "---")

                lbl_edificio1ex.Text = par.IfNull(myReader("escl_edificio1"), "---")
                lbl_edificio2ex.Text = par.IfNull(myReader("escl_edificio2"), "---")
                lbl_edificio3ex.Text = par.IfNull(myReader("escl_edificio3"), "---")


                lbl_indirizzo1ex.Text = par.IfNull(myReader("escl_indirizzo1"), "---")
                lbl_indirizzo2ex.Text = par.IfNull(myReader("escl_indirizzo2"), "---")
                lbl_indirizzo3ex.Text = par.IfNull(myReader("escl_indirizzo3"), "---")


                lbl_piano1SA.Text = par.IfNull(myReader("escl_piano_sa1"), "---")
                lbl_piano2SA.Text = par.IfNull(myReader("escl_piano_sa2"), "---")
                lbl_piano3SA.Text = par.IfNull(myReader("escl_piano_sa3"), "---")


                lbl_piano1CA.Text = par.IfNull(myReader("escl_piano_ca1"), "---")
                lbl_piano2CA.Text = par.IfNull(myReader("escl_piano_ca2"), "---")
                lbl_piano3CA.Text = par.IfNull(myReader("escl_piano_ca3"), "---")


                lbl_note.Text = par.IfNull(myReader("note"), "---")





                If par.IfNull(myReader("barr"), "") <> "0" Then
                    lbl_barrArch.Text = " Barriere Architettoniche Escluse"
                Else

                    lbl_barrArch.Text = " Barriere Architettoniche Non Escluse"
                End If


                If par.IfNull(myReader("condominio"), "") <> "0" Then
                    lbl_condominio.Text = " Condominio Escluso"
                Else

                    lbl_condominio.Text = " Condominio Non Escluso"
                End If



                If par.IfNull(myReader("pref_sup_min"), "").ToString <> "" Then

                    lbl_supMin.Text = par.IfNull(FormatNumber(myReader("pref_sup_min"), 2), " ")

                Else

                    lbl_supMin.Text = "---"

                End If


                If par.IfNull(myReader("pref_sup_max"), "").ToString <> "" Then

                    lbl_supMax.Text = par.IfNull(FormatNumber(myReader("pref_sup_max"), 2), " ")

                Else

                    lbl_supMax.Text = "---"

                End If



            Else

                'par.cmd.CommandText = " SELECT (comp_nucleo_cambi.cognome || ' ' || comp_nucleo_cambi.nome) AS richiedente, domande_bando_cambi.pg AS pg " _
                '                & "  FROM domande_bando_cambi, comp_nucleo_cambi " _
                '                & " WHERE domande_bando_cambi.id_dichiarazione = comp_nucleo_cambi.id_dichiarazione " _
                '                & " And comp_nucleo_cambi.progr = 0 AND domande_bando_cambi.ID =" & idDomanda.Value & ""


                'Dim myReaderK As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReaderK.Read Then



                '    contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReaderK("richiedente"), "_______________________"))
                '    contenuto = Replace(contenuto, "$PG$", par.IfNull(myReaderK("PG"), " "))

                'End If


                lbl_localita1.Text = "---"
                lbl_localita2.Text = "---"
                lbl_localita3.Text = "---"


                lbl_zona1.Text = "---"
                lbl_zona2.Text = "---"
                lbl_zona3.Text = "---"


                lbl_quart1.Text = "---"
                lbl_quart2.Text = "---"
                lbl_quart3.Text = "---"


                lbl_complesso1.Text = "---"
                lbl_complesso2.Text = "---"
                lbl_complesso3.Text = "---"


                lbl_edificio1.Text = "---"
                lbl_edificio2.Text = "---"
                lbl_edificio3.Text = "---"

                lbl_indirizzo1.Text = "---"
                lbl_indirizzo2.Text = "---"
                lbl_indirizzo3.Text = "---"

                lbl_localita1ex.Text = "---"
                lbl_localita2ex.Text = "---"
                lbl_localita3ex.Text = "---"


                lbl_zona1ex.Text = "---"
                lbl_zona2ex.Text = "---"
                lbl_zona3ex.Text = "---"


                lbl_quart1ex.Text = "---"
                lbl_quart2ex.Text = "---"
                lbl_quart3ex.Text = "---"


                lbl_complesso1ex.Text = "---"
                lbl_complesso2ex.Text = "---"
                lbl_complesso3ex.Text = "---"


                lbl_edificio1ex.Text = "---"
                lbl_edificio2ex.Text = "---"
                lbl_edificio3ex.Text = "---"



                lbl_indirizzo1ex.Text = "---"
                lbl_indirizzo2ex.Text = "---"
                lbl_indirizzo3ex.Text = "---"


                lbl_piano1SA.Text = "---"
                lbl_piano2SA.Text = "---"
                lbl_piano3SA.Text = "---"


                lbl_piano1CA.Text = "---"
                lbl_piano2CA.Text = "---"
                lbl_piano3CA.Text = "---"

                lbl_supMax.Text = "---"
                lbl_supMin.Text = "---"

                lbl_barrArch.Text = " "
                lbl_condominio.Text = " "


                lbl_note.Text = "---"













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
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try



    End Sub



End Class



