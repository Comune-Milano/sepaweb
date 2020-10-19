
Partial Class ASS_RisultatoRicOfferta
    Inherits PageSetIdMode
    Dim par As New CM.Global()
    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreOF As String
    Dim sStringaSql As String
    Dim sValoreCF As String
    Dim sValorePG As String
    Dim sValoreT As String
    Dim sValorePR As String
    Dim sValoreContr As String
    Dim sValoreRevoca As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then
            sValorePR = Request.QueryString("PR")

            sValoreNM = Request.QueryString("NM")
            sValoreCG = Request.QueryString("CG")
            sValorePG = Request.QueryString("PG")
            sValoreOF = Request.QueryString("OF")
            sValoreT = Request.QueryString("T")
            sValoreContr = Request.QueryString("CONTR")
            sValoreRevoca = Request.QueryString("REV")

            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Text = "-1"


            If sValorePR = "1" Then

                If (sValoreNM = "") And (sValoreCG = "") And (sValorePG = "") And (sValoreOF = "") And (sValoreT = "-1") Then

                    'Cerca()
                    CercaAbbERP()
                    CercaAbbCambi()
                    CercaAbbVSA()
                End If


                If (sValoreNM = "") And (sValoreCG = "") And (sValorePG = "") And (sValoreOF = "") And (sValoreT <> "-1") Then

                    Select Case sValoreT
                        Case "1"
                            CercaAbbERP()
                        Case "2"
                            CercaAbbCambi()
                        Case "3"
                            CercaAbbVSA()
                    End Select
                End If


                If (sValoreNM <> "") Or (sValoreCG <> "") Or (sValorePG <> "") Or (sValoreOF <> "") Then

                    Select Case sValoreT
                        Case "-1"

                            CercaAbbCambi()
                            CercaAbbERP()
                            CercaAbbVSA()

                        Case "1"
                            CercaAbbERP()
                        Case "2"
                            CercaAbbCambi()
                        Case "3"
                            CercaAbbVSA()
                    End Select
                End If

                Label3.Text = "  - Totale:" & contaRisultati

            Else
                Cerca()

            End If


        End If


    End Sub

    Private Sub CercaAbbCambi()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String

        bTrovato = False
        sStringaSql = ""

        If sValoreCG <> "" Then
            sValore = sValoreCG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            ' bTrovato = True
            sStringaSql = sStringaSql & " AND COMP_NUCLEO_CAMBI.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreNM <> "" Then
            ' If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreNM
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " AND COMP_NUCLEO_CAMBI.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValorePG <> "" Then
            ' If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " AND DOMANDE_BANDO_CAMBI.PG " & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreOF <> "" Then
            'If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreOF
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            sStringaSql = sStringaSql & " AND DOMANDE_OFFERTE_SCAD.ID " & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        Dim condizioneRevoca As String = ""

        

        Select Case sValoreContr


            Case "-1"
                If sValoreRevoca = "1" Then
                    condizioneRevoca = " AND DOMANDE_BANDO_CAMBI.ID in (select id_pratica from proposte_revoche) "
                Else
                    condizioneRevoca = " AND (domande_bando_cambi.id_stato = '9' or domande_bando_cambi.id_stato = '10') AND DOMANDE_BANDO_CAMBI.FL_INVITO='1' AND DOMANDE_BANDO_CAMBI.ID not in (select id_pratica from proposte_revoche) "
                End If

                sStringaSQL2 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_CAMBI.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_CAMBI.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def_CAMBI.posizione,DOMANDE_BANDO_CAMBI.ID,COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME," _
                               & "DOMANDE_BANDO_CAMBI.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                               & " FROM domande_offerte_scad,bandi_graduatoria_def_CAMBI,DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI,DICHIARAZIONI_CAMBI " _
                               & " WHERE DOMANDE_BANDO_CAMBI.FL_ASS_ESTERNA='1' AND DOMANDE_BANDO_CAMBI.PROGR_COMPONENTE=COMP_NUCLEO_CAMBI.PROGR AND DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID " _
                               & " AND  domande_bando_CAMBI.id = bandi_graduatoria_def_CAMBI.id_domanda and " _
                               & " DOMANDE_BANDO_CAMBI.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA " _
                               & condizioneRevoca & " AND DOMANDE_BANDO_CAMBI.FL_PRATICA_CHIUSA<>'1' " _
                               & " and domande_offerte_scad.id in (select max(id) as id from domande_offerte_scad group by domande_offerte_scad.id_domanda) " _
                               & " " & sStringaSql & " " _
                               & " ORDER BY COMP_NUCLEO_CAMBI.COGNOME ASC,COMP_NUCLEO_CAMBI.NOME ASC"



            Case "1"
                If sValoreRevoca = "1" Then
                    condizioneRevoca = " AND DOMANDE_BANDO_CAMBI.ID in (select id_pratica from proposte_revoche) "
                Else
                    condizioneRevoca = " AND (domande_bando_cambi.id_stato = '10') AND DOMANDE_BANDO_CAMBI.FL_INVITO='1' AND DOMANDE_BANDO_CAMBI.ID not in (select id_pratica from proposte_revoche) "
                End If

                sStringaSQL2 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_CAMBI.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_CAMBI.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def_CAMBI.posizione,DOMANDE_BANDO_CAMBI.ID,COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME," _
                               & "DOMANDE_BANDO_CAMBI.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                               & " FROM domande_offerte_scad,bandi_graduatoria_def_CAMBI,DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI,DICHIARAZIONI_CAMBI " _
                               & " WHERE DOMANDE_BANDO_CAMBI.FL_ASS_ESTERNA='1' AND DOMANDE_BANDO_CAMBI.PROGR_COMPONENTE=COMP_NUCLEO_CAMBI.PROGR AND DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID " _
                               & " AND  domande_bando_CAMBI.id = bandi_graduatoria_def_CAMBI.id_domanda and " _
                               & " DOMANDE_BANDO_CAMBI.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA " _
                               & condizioneRevoca & " AND DOMANDE_BANDO_CAMBI.FL_PRATICA_CHIUSA<>'1' " _
                               & " and domande_offerte_scad.id in (select max(id) as id from domande_offerte_scad group by domande_offerte_scad.id_domanda) " _
                               & " " & sStringaSql & " " _
                               & " ORDER BY COMP_NUCLEO_CAMBI.COGNOME ASC,COMP_NUCLEO_CAMBI.NOME ASC"

            Case "2"
                If sValoreRevoca = "1" Then
                    condizioneRevoca = " AND DOMANDE_BANDO_CAMBI.ID in (select id_pratica from proposte_revoche) "
                Else
                    condizioneRevoca = " AND (domande_bando_cambi.id_stato = '9') AND DOMANDE_BANDO_CAMBI.FL_INVITO='1' AND DOMANDE_BANDO_CAMBI.ID not in (select id_pratica from proposte_revoche) "
                End If

                sStringaSQL2 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_CAMBI.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_CAMBI.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def_CAMBI.posizione,DOMANDE_BANDO_CAMBI.ID,COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME," _
                               & "DOMANDE_BANDO_CAMBI.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                               & " FROM domande_offerte_scad,bandi_graduatoria_def_CAMBI,DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI,DICHIARAZIONI_CAMBI " _
                               & " WHERE DOMANDE_BANDO_CAMBI.FL_ASS_ESTERNA='1' AND DOMANDE_BANDO_CAMBI.PROGR_COMPONENTE=COMP_NUCLEO_CAMBI.PROGR AND DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID " _
                               & " AND  domande_bando_CAMBI.id = bandi_graduatoria_def_CAMBI.id_domanda and " _
                               & " DOMANDE_BANDO_CAMBI.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA " _
                               & condizioneRevoca & " AND DOMANDE_BANDO_CAMBI.FL_PRATICA_CHIUSA<>'1' " _
                               & " and domande_offerte_scad.id in (select max(id) as id from domande_offerte_scad group by domande_offerte_scad.id_domanda) " _
                               & " " & sStringaSql & " " _
                               & " ORDER BY COMP_NUCLEO_CAMBI.COGNOME ASC,COMP_NUCLEO_CAMBI.NOME ASC"

        End Select


        BindGridCambi()
    End Sub



    Private Sub CercaAbbERP()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String

        bTrovato = False
        sStringaSql = ""

        If sValoreCG <> "" Then
            sValore = sValoreCG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            'bTrovato = True
            sStringaSql = sStringaSql & "AND COMP_NUCLEO.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreNM <> "" Then
            ' If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreNM
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "AND COMP_NUCLEO.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValorePG <> "" Then
            '   If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "AND DOMANDE_BANDO.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreOF <> "" Then
            'If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreOF
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            sStringaSql = sStringaSql & " AND DOMANDE_OFFERTE_SCAD.ID " & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        Dim condizioneRevoca As String = ""

        

        Select Case sValoreContr

            Case "-1"
                If sValoreRevoca = "1" Then
                    condizioneRevoca = "  AND DOMANDE_BANDO.ID in (select id_pratica from proposte_revoche) "
                Else
                    condizioneRevoca = " AND (domande_bando.id_stato = '4' or domande_bando.id_stato = '9' or domande_bando.id_stato = '10') AND DOMANDE_BANDO.ID not in (select id_pratica from proposte_revoche) "
                End If

                sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def.posizione,DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME," _
                               & "DOMANDE_BANDO.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                               & " FROM domande_offerte_scad,bandi_graduatoria_def,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI " _
                               & " WHERE DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
                               & " AND  domande_bando.id = bandi_graduatoria_def.id_domanda " _
                               & " AND DOMANDE_BANDO.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA " _
                               & condizioneRevoca _
                               & " AND DOMANDE_BANDO.FL_PRATICA_CHIUSA<>'1' " _
                               & "  " _
                               & " and domande_offerte_scad.id in (select max(id) as id from domande_offerte_scad group by domande_offerte_scad.id_domanda) " _
                               & " " & sStringaSql & " " _
                               & " ORDER BY bandi_graduatoria_def.posizione ASC,domande_bando.DATA_PG ASC,domande_bando.isbarc_r DESC"




            Case "1"
                If sValoreRevoca = "1" Then
                    condizioneRevoca = "  AND DOMANDE_BANDO.ID in (select id_pratica from proposte_revoche) "
                Else
                    condizioneRevoca = " AND (domande_bando.id_stato = '4' or domande_bando.id_stato = '10') AND DOMANDE_BANDO.ID not in (select id_pratica from proposte_revoche) "
                End If


                sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def.posizione,DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME," _
                               & "DOMANDE_BANDO.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                               & " FROM domande_offerte_scad,bandi_graduatoria_def,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI " _
                               & " WHERE DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
                               & " AND  domande_bando.id = bandi_graduatoria_def.id_domanda " _
                               & " AND DOMANDE_BANDO.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA " _
                               & condizioneRevoca _
                               & " AND DOMANDE_BANDO.FL_PRATICA_CHIUSA<>'1' " _
                               & " and domande_offerte_scad.id in (select max(id) as id from domande_offerte_scad group by domande_offerte_scad.id_domanda) " _
                               & " " & sStringaSql & " " _
                               & " ORDER BY bandi_graduatoria_def.posizione ASC,domande_bando.DATA_PG ASC,domande_bando.isbarc_r DESC"



            Case "2"
                If sValoreRevoca = "1" Then
                    condizioneRevoca = "  AND DOMANDE_BANDO.ID in (select id_pratica from proposte_revoche) "
                Else
                    condizioneRevoca = " AND (domande_bando.id_stato = '4' or domande_bando.id_stato = '9') AND DOMANDE_BANDO.ID_STATO<>'10' AND DOMANDE_BANDO.ID not in (select id_pratica from proposte_revoche) "
                End If


                sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def.posizione,DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME," _
                               & "DOMANDE_BANDO.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                               & " FROM domande_offerte_scad,bandi_graduatoria_def,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI " _
                               & " WHERE DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
                               & " AND  domande_bando.id = bandi_graduatoria_def.id_domanda " _
                               & " AND DOMANDE_BANDO.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA " _
                               & condizioneRevoca & " AND DOMANDE_BANDO.FL_PRATICA_CHIUSA<>'1' " _
                               & " and domande_offerte_scad.id in (select max(id) as id from domande_offerte_scad group by domande_offerte_scad.id_domanda) " _
                               & " " & sStringaSql & " " _
                               & " ORDER BY bandi_graduatoria_def.posizione ASC,domande_bando.DATA_PG ASC,domande_bando.isbarc_r DESC"



        End Select




        BindGrid()
    End Sub


    Private Sub CercaAbbVSA()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String

        bTrovato = False
        sStringaSql = ""

        If sValoreCG <> "" Then
            sValore = sValoreCG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            ' bTrovato = True
            sStringaSql = sStringaSql & "AND COMP_NUCLEO_VSA.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreNM <> "" Then
            '    If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreNM
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "AND COMP_NUCLEO_VSA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValorePG <> "" Then
            '  If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "AND DOMANDE_BANDO_VSA.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreOF <> "" Then
            'If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreOF
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            sStringaSql = sStringaSql & " AND DOMANDE_OFFERTE_SCAD.ID " & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        Dim condizioneRevoca As String = ""

        

        Select Case sValoreContr

            Case "-1"

                If sValoreRevoca = "1" Then
                    condizioneRevoca = " AND DOMANDE_BANDO_vsa.ID in (select id_pratica from proposte_revoche) "
                Else
                    condizioneRevoca = " AND (domande_bando_vsa.id_stato = '9' or domande_bando_vsa.id_stato = '10') AND DOMANDE_BANDO_vsa.FL_INVITO='1' AND DOMANDE_BANDO_vsa.ID not in (select id_pratica from proposte_revoche) "
                End If

                sStringaSQL3 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_vsa.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO_vsa.ID,COMP_NUCLEO_vsa.COGNOME,COMP_NUCLEO_vsa.NOME," _
                                 & "DOMANDE_BANDO_vsa.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                                 & " FROM domande_offerte_scad,DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa,DICHIARAZIONI_vsa " _
                                 & " WHERE DOMANDE_BANDO_vsa.FL_ASS_ESTERNA='1' " _
                                 & " AND DOMANDE_BANDO_vsa.PROGR_COMPONENTE=COMP_NUCLEO_vsa.PROGR " _
                                 & " AND DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID " _
                                 & " AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID " _
                                 & " AND DOMANDE_BANDO_vsa.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA " _
                                 & condizioneRevoca & " AND DOMANDE_BANDO_vsa.FL_PRATICA_CHIUSA<>'1' " _
                                 & "  and domande_offerte_scad.id in (select max(id) as id from domande_offerte_scad group by domande_offerte_scad.id_domanda) " _
                                 & " " & sStringaSql & " " _
                                 & " ORDER BY COMP_NUCLEO_vsa.COGNOME ASC,COMP_NUCLEO_vsa.NOME ASC"



            Case "1"

                If sValoreRevoca = "1" Then
                    condizioneRevoca = " AND DOMANDE_BANDO_vsa.ID in (select id_pratica from proposte_revoche) "
                Else
                    condizioneRevoca = " AND (domande_bando_vsa.id_stato = '10') AND DOMANDE_BANDO_vsa.FL_INVITO='1' AND DOMANDE_BANDO_vsa.ID not in (select id_pratica from proposte_revoche) "
                End If

                sStringaSQL3 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_vsa.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO_vsa.ID,COMP_NUCLEO_vsa.COGNOME,COMP_NUCLEO_vsa.NOME," _
                                 & "DOMANDE_BANDO_vsa.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                                 & " FROM domande_offerte_scad,DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa,DICHIARAZIONI_vsa " _
                                 & " WHERE DOMANDE_BANDO_vsa.FL_ASS_ESTERNA='1' " _
                                 & " AND DOMANDE_BANDO_vsa.PROGR_COMPONENTE=COMP_NUCLEO_vsa.PROGR " _
                                 & " AND DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID " _
                                 & " AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID " _
                                 & " AND DOMANDE_BANDO_vsa.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA " _
                                 & condizioneRevoca & " AND DOMANDE_BANDO_vsa.FL_PRATICA_CHIUSA<>'1' " _
                                 & " and domande_offerte_scad.id in (select max(id) as id from domande_offerte_scad group by domande_offerte_scad.id_domanda) " _
                                 & " " & sStringaSql & " " _
                                 & " ORDER BY COMP_NUCLEO_vsa.COGNOME ASC,COMP_NUCLEO_vsa.NOME ASC"

            Case "2"

                If sValoreRevoca = "1" Then
                    condizioneRevoca = " AND DOMANDE_BANDO_vsa.ID in (select id_pratica from proposte_revoche) "
                Else
                    condizioneRevoca = " AND (domande_bando_vsa.id_stato = '9') AND DOMANDE_BANDO_vsa.FL_INVITO='1' AND DOMANDE_BANDO_vsa.ID not in (select id_pratica from proposte_revoche) "
                End If

                sStringaSQL3 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_vsa.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO_vsa.ID,COMP_NUCLEO_vsa.COGNOME,COMP_NUCLEO_vsa.NOME," _
                                 & "DOMANDE_BANDO_vsa.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                                 & " FROM domande_offerte_scad,DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa,DICHIARAZIONI_vsa " _
                                 & " WHERE DOMANDE_BANDO_vsa.FL_ASS_ESTERNA='1' " _
                                 & " AND DOMANDE_BANDO_vsa.PROGR_COMPONENTE=COMP_NUCLEO_vsa.PROGR " _
                                 & " AND DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID " _
                                 & " AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID " _
                                 & " AND DOMANDE_BANDO_vsa.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA " _
                                 & condizioneRevoca & " AND DOMANDE_BANDO_vsa.FL_PRATICA_CHIUSA<>'1' " _
                                 & " AND domande_offerte_scad.id in (select max(id) as id from domande_offerte_scad group by domande_offerte_scad.id_domanda) " _
                                 & " " & sStringaSql & " " _
                                 & " ORDER BY COMP_NUCLEO_vsa.COGNOME ASC,COMP_NUCLEO_vsa.NOME ASC"


        End Select

        BindGridEmergenze()
    End Sub


    Private Sub Cerca()
        Try


            Dim bTrovato As Boolean
            Dim sValore As String
            Dim sCompara As String

            bTrovato = False
            sStringaSql = ""

            If sValoreOF <> "" Then
                sValore = sValoreOF
                bTrovato = True
                sStringaSql = sStringaSql & " DOMANDE_OFFERTE_SCAD.ID =" & par.PulisciStrSql(sValore) & " AND "
            End If

            sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def.posizione,DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME," _
           & "DOMANDE_BANDO.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
           & "" _
           & "" _
           & "" _
           & "" _
           & " " _
           & " FROM domande_offerte_scad,bandi_graduatoria_def,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI " _
           & " WHERE " & sStringaSql & "  DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
           & " AND  domande_bando.id = bandi_graduatoria_def.id_domanda and " _
           & "" _
           & "" _
           & "" _
           & "DOMANDE_BANDO.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
           & " DOMANDE_BANDO.FL_INVITO='1' AND DOMANDE_BANDO.ID_STATO<>'10' AND DOMANDE_BANDO.FL_PRATICA_CHIUSA<>'1' " _
           & " " _
           & "AND (DOMANDE_BANDO.ID_STATO='9') ORDER BY COMP_NUCLEO.COGNOME ASC,COMP_NUCLEO.NOME ASC"


            sStringaSQL2 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_CAMBI.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_CAMBI.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def_CAMBI.posizione,DOMANDE_BANDO_CAMBI.ID,COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME," _
                           & "DOMANDE_BANDO_CAMBI.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                           & " FROM domande_offerte_scad,bandi_graduatoria_def_CAMBI,DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI,DICHIARAZIONI_CAMBI " _
                           & " WHERE " & sStringaSql & "  DOMANDE_BANDO_CAMBI.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO_CAMBI.PROGR_COMPONENTE=COMP_NUCLEO_CAMBI.PROGR AND DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID " _
                           & " AND  domande_bando_CAMBI.id = bandi_graduatoria_def_CAMBI.id_domanda and " _
                           & "DOMANDE_BANDO_CAMBI.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
                           & " DOMANDE_BANDO_CAMBI.FL_INVITO='1' AND DOMANDE_BANDO_CAMBI.ID_STATO<>'10' AND DOMANDE_BANDO_CAMBI.FL_PRATICA_CHIUSA<>'1' " _
                           & "AND (DOMANDE_BANDO_CAMBI.ID_STATO='9') ORDER BY COMP_NUCLEO_CAMBI.COGNOME ASC,COMP_NUCLEO_CAMBI.NOME ASC"


            sStringaSQL3 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_vsa.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO_vsa.ID,COMP_NUCLEO_vsa.COGNOME,COMP_NUCLEO_vsa.NOME," _
                           & "DOMANDE_BANDO_vsa.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                           & " FROM domande_offerte_scad,DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa,DICHIARAZIONI_vsa " _
                           & " WHERE " & sStringaSql & "  DOMANDE_BANDO_vsa.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO_vsa.PROGR_COMPONENTE=COMP_NUCLEO_vsa.PROGR AND DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID " _
                           & " AND  " _
                           & "DOMANDE_BANDO_vsa.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
                           & " DOMANDE_BANDO_vsa.FL_INVITO='1' AND DOMANDE_BANDO_vsa.ID_STATO<>'10' AND DOMANDE_BANDO_vsa.FL_PRATICA_CHIUSA<>'1' " _
                           & "AND (DOMANDE_BANDO_vsa.ID_STATO='9') ORDER BY COMP_NUCLEO_vsa.COGNOME ASC,COMP_NUCLEO_vsa.NOME ASC"


            'par.OracleConn.Open()

            'Dim STRINGA As String = ""
            'If sValore <> "" Then
            '    STRINGA = "SELECT ID_DOMANDA FROM DOMANDE_OFFERTE_SCAD WHERE ID=" & par.PulisciStrSql(sValore)
            'Else
            '    STRINGA = "SELECT ID_DOMANDA FROM DOMANDE_OFFERTE_SCAD"
            'End If
            'Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(STRINGA, par.OracleConn)

            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
            'If myReader.Read() Then
            'If myReader("ID_DOMANDA") >= 500000 Then
            '    sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_CAMBI.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_CAMBI.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def_CAMBI.posizione,DOMANDE_BANDO_CAMBI.ID,COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME," _
            '                   & "DOMANDE_BANDO_CAMBI.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
            '                   & " FROM domande_offerte_scad,bandi_graduatoria_def_CAMBI,DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI,DICHIARAZIONI_CAMBI " _
            '                   & " WHERE " & sStringaSql & "  DOMANDE_BANDO_CAMBI.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO_CAMBI.PROGR_COMPONENTE=COMP_NUCLEO_CAMBI.PROGR AND DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID " _
            '                   & " AND  domande_bando_CAMBI.id = bandi_graduatoria_def_CAMBI.id_domanda and " _
            '                   & "DOMANDE_BANDO_CAMBI.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
            '                   & " DOMANDE_BANDO_CAMBI.FL_INVITO='1' AND DOMANDE_BANDO_CAMBI.ID_STATO<>'10' AND DOMANDE_BANDO_CAMBI.FL_PRATICA_CHIUSA<>'1' " _
            '                   & "AND (DOMANDE_BANDO_CAMBI.ID_STATO='9') ORDER BY COMP_NUCLEO_CAMBI.COGNOME ASC,COMP_NUCLEO_CAMBI.NOME ASC"
            'Else
            '    sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def.posizione,DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME," _
            '   & "DOMANDE_BANDO.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
            '   & "" _
            '   & "" _
            '   & "" _
            '   & "" _
            '   & " " _
            '   & " FROM domande_offerte_scad,bandi_graduatoria_def,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI " _
            '   & " WHERE " & sStringaSql & "  DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
            '   & " AND  domande_bando.id = bandi_graduatoria_def.id_domanda and " _
            '   & "" _
            '   & "" _
            '   & "" _
            '   & "DOMANDE_BANDO.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
            '   & " DOMANDE_BANDO.FL_INVITO='1' AND DOMANDE_BANDO.ID_STATO<>'10' AND DOMANDE_BANDO.FL_PRATICA_CHIUSA<>'1' " _
            '   & " " _
            '   & "AND (DOMANDE_BANDO.ID_STATO='9') ORDER BY COMP_NUCLEO.COGNOME ASC,COMP_NUCLEO.NOME ASC"

            'End If
            'Else
            'sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def.posizione,DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME," _
            '               & "DOMANDE_BANDO.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
            '               & "" _
            '               & "" _
            '               & "" _
            '               & "" _
            '               & " " _
            '               & " FROM domande_offerte_scad,bandi_graduatoria_def,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI " _
            '               & " WHERE  DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
            '               & " AND  domande_bando.id = bandi_graduatoria_def.id_domanda and " _
            '               & "" _
            '               & "" _
            '               & "" _
            '               & "DOMANDE_BANDO.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
            '               & " DOMANDE_BANDO.FL_INVITO='1' AND DOMANDE_BANDO.ID_STATO<>'10' AND DOMANDE_BANDO.FL_PRATICA_CHIUSA<>'1' " _
            '               & " " _
            '               & "AND (DOMANDE_BANDO.ID_STATO='9') ORDER BY COMP_NUCLEO.COGNOME ASC,COMP_NUCLEO.NOME ASC"

            'End If



            BindGrid()
            BindGridCambi()
            BindGridEmergenze()

        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Public Property sStringaSQL2() As String
        Get
            If Not (ViewState("par_sStringaSQL2") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL2"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL2") = value
        End Set

    End Property


    Public Property sStringaSQL3() As String
        Get
            If Not (ViewState("par_sStringaSQL3") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL3"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL3") = value
        End Set

    End Property

    Public Property contaRisultati() As Integer
        Get
            If Not (ViewState("par_contaRisultati") Is Nothing) Then
                Return CInt(ViewState("par_contaRisultati"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_contaRisultati") = value
        End Set
    End Property

    Private Sub BindGridEmergenze()
        Try


            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL3, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa")
            DataGrid3.DataSource = ds
            DataGrid3.DataBind()
            contaRisultati = contaRisultati + ds.Tables(0).Rows.Count

            'Label3.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub


    Private Sub BindGridCambi()
        Try


            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO_cambi,COMP_NUCLEO_cambi")
            DataGrid2.DataSource = ds
            DataGrid2.DataBind()
            contaRisultati = contaRisultati + ds.Tables(0).Rows.Count

            'Label3.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub


    Private Sub BindGrid()
        Try

            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            If sValorePR <> "1" Then
                Label3.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            End If
            contaRisultati = contaRisultati + ds.Tables(0).Rows.Count
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        LBLPROGR.Text = e.Item.Cells(2).Text
        lblScad.Text = e.Item.Cells(3).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text
        HiddenField1.Value = "1"
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
    e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""IntroProcessoDec.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If LBLID.Text = "-1" Or LBLID.Text = "" Then
            Response.Write("<script>alert('Nessuna Domanda selezionata!')</script>")
        Else
            If Request.QueryString("PR") = "1" Then
                Response.Write("<script>window.open('ProcessoDecisionale.aspx?TIPO=" & HiddenField1.Value & "&ID=" & LBLID.Text & "&OF=" & LBLPROGR.Text & "&SC=" & lblScad.Text & "');</script>")
            Else
                Response.Write("<script>location.replace('DecidiOfferta.aspx?T=" & HiddenField1.Value & "&ID=" & LBLID.Text & "&OF=" & LBLPROGR.Text & "&SC=" & lblScad.Text & "');</script>")
            End If
        End If
    End Sub

    Protected Sub DataGrid2_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        LBLPROGR.Text = e.Item.Cells(2).Text
        lblScad.Text = e.Item.Cells(3).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text
        HiddenField1.Value = "2"

    End Sub

    Protected Sub DataGrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If

    End Sub

    Protected Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid2.CurrentPageIndex = e.NewPageIndex
            BindGridCambi()
        End If
    End Sub

    Protected Sub DataGrid3_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid3.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        LBLPROGR.Text = e.Item.Cells(2).Text
        lblScad.Text = e.Item.Cells(3).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text
        HiddenField1.Value = "3"

    End Sub

    Protected Sub DataGrid3_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid3.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If

    End Sub

    Protected Sub DataGrid3_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid3.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid3.CurrentPageIndex = e.NewPageIndex
            BindGridEmergenze()
        End If
    End Sub

    Protected Sub DataGrid3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid3.SelectedIndexChanged

    End Sub
End Class