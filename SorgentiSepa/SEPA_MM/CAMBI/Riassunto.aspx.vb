
Partial Class CAMBI_Riassunto
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            Riassunto(CLng(Request.QueryString("ID")), Request.QueryString("CF"))
        End If
    End Sub

    Private Function Riassunto(ByVal id As Long, ByVal cf As String)
        Dim ID_domanda As Long
        Dim ID_DICHIARAIONE As Long
        Dim Esito As String

        Dim StringaHTML111 As String = ""
        Dim StringaHTML112 As String = ""
        Dim StringaHTML150 As String = ""
        Dim StringaHTML1000 As String = ""
        Dim bStringaHTML111 As Boolean
        Dim bStringaHTML150 As Boolean
        Dim bStringaHTML151 As Boolean
        Dim bStringaHTML1000 As Boolean
        Dim bStringaHTML1001 As Boolean
        Dim bStringaHTML175 As Boolean
        Dim StringaHTML As String = ""
        Dim StringaHTML_1 As String = ""
        Dim StringaHTML_2 As String = ""
        Dim StringaHTML_3 As String = ""
        Dim StringaHTML_4 As String = ""
        Dim StringaHTML_5 As String = ""
        Dim StringaHTML_6 As String = ""
        Dim StringaHTML_7 As String = ""
        Dim StringaHTML_8 As String = ""
        Dim StringaHTML_9 As String = ""
        Dim StringaHTML_10 As String = ""
        Dim StringaHTML_11 As String = ""
        Dim StringaHTML_12 As String = ""
        Dim StringaHTML_13 As String = ""
        Dim StringaHTML_14 As String = ""
        Dim StringaHTML_15 As String = ""
        Dim StringaHTML151 As String = ""
        Dim StringaHTML1001 As String = ""
        Dim STRINGAHTML171 As String = ""
        Dim STRINGAHTML172 As String = ""
        Dim STRINGAHTML173 As String = ""

        Dim STRINGAHTML175 As String = ""
        Dim STRINGAHTML176 As String = ""

        Dim bStringaHTML176 As Boolean


        StringaHTML_1 = ""
        StringaHTML_2 = ""
        StringaHTML_3 = ""
        StringaHTML_4 = ""
        StringaHTML_5 = ""
        StringaHTML_6 = ""
        StringaHTML_7 = ""
        StringaHTML_8 = ""
        StringaHTML_9 = ""
        StringaHTML_10 = ""
        StringaHTML_11 = ""
        StringaHTML_12 = ""
        StringaHTML_13 = ""
        StringaHTML_14 = ""
        StringaHTML_15 = ""
        StringaHTML111 = ""
        StringaHTML112 = ""
        StringaHTML150 = ""
        StringaHTML151 = ""
        STRINGAHTML171 = ""
        STRINGAHTML172 = ""
        STRINGAHTML173 = ""
        STRINGAHTML175 = ""
        STRINGAHTML176 = ""
        Esito = ""
        Try



            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)


            par.cmd.CommandText = "SELECT DOMANDE_BANDO_cambi.*,bandi_cambio.descrizione as ""Db"" FROM bandi_cambio,DOMANDE_BANDO_cambi WHERE DOMANDE_BANDO_cambi.id_bando=bandi_cambio.id and DOMANDE_BANDO_cambi.ID=" & id
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then

                ID_domanda = par.IfNull(myReader("ID"), -1)
                ID_DICHIARAIONE = par.IfNull(myReader("ID_DICHIARAZIONE"), -1)

                par.cmd.CommandText = "SELECT T_TIPO_VIA.DESCRIZIONE AS ""DVIA"",COMUNI_NAZIONI.NOME AS ""NN"",COMUNI_NAZIONI.SIGLA AS ""SS"",COMUNI_NAZIONI.CAP AS ""CC"",IND_RES_DNTE,CIVICO_RES_DNTE FROM DICHIARAZIONI_cambi,COMUNI_NAZIONI,T_TIPO_VIA WHERE ID_TIPO_IND_RES_DNTE=T_TIPO_VIA.COD AND ID_LUOGO_RES_DNTE=COMUNI_NAZIONI.ID AND DICHIARAZIONI_cambi.ID=" & ID_DICHIARAIONE
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then


                    bStringaHTML175 = False

                    STRINGAHTML175 = STRINGAHTML175 & "<p><b><font face='Arial' size='3'>RESIDENZA</font></b></p>"
                    STRINGAHTML175 = STRINGAHTML175 & "<table border='1' cellpadding='0' cellspacing='0' width='100%'>"
                    STRINGAHTML175 = STRINGAHTML175 & "<tr>"
                    STRINGAHTML175 = STRINGAHTML175 & "<td width='50%'><font face='Arial' size='2'>INDIRIZZO</font></td>"
                    STRINGAHTML175 = STRINGAHTML175 & "<td width='50%'><font face='Arial' size='2'>COMUNE</font></td>"

                    STRINGAHTML175 = STRINGAHTML175 & "</tr>"


                    bStringaHTML175 = True
                    STRINGAHTML175 = STRINGAHTML175 & "<tr>"
                    STRINGAHTML175 = STRINGAHTML175 & "<td width='50%'><font face='Arial' size='1'>" & par.IfNull(myReader1("DVIA"), "") & " " & par.IfNull(myReader1("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader1("CIVICO_RES_DNTE"), "") & "</font></td>"
                    STRINGAHTML175 = STRINGAHTML175 & "<td width='50%'><font face='Arial' size='1'>" & par.IfNull(myReader1("CC"), "") & " " & par.IfNull(myReader1("NN"), "") & " (" & par.IfNull(myReader1("SS"), "") & ")" & "</font></td>"
                    STRINGAHTML175 = STRINGAHTML175 & "</tr>"


                    STRINGAHTML175 = STRINGAHTML175 & "</table>"

                End If
                myReader1.Close()
                StringaHTML = "<table cellpadding='0' cellspacing='0' width='100%'>"
                StringaHTML = StringaHTML & "<tr>"
                StringaHTML = StringaHTML & "<td width='100%'><b><font face='Arial' size='4'>DOMANDA N° " & par.IfNull(myReader("PG"), "") & " DEL " & par.FormattaData(par.IfNull(myReader("DATA_PG"), "")) & " - " & par.IfNull(myReader("db"), "") & "</font></b></td>"
                StringaHTML = StringaHTML & "</tr>"
                StringaHTML = StringaHTML & "</table>"

                par.cmd.CommandText = "select * from comp_nucleo_cambi where id_dichiarazione=" & myReader("id_dichiarazione") & " AND progr=0"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    StringaHTML = StringaHTML & "<p><font face='Arial' size='3'>Intestata a: <b>" & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "") & "</b></font></p>"
                End If
                myReader1.Close()


                StringaHTML = StringaHTML & "<p><b><font face='Arial' size='3'>DATI OPERATIVI</font></b></p>"
                StringaHTML = StringaHTML & "<table border='1' cellpadding='0' cellspacing='0' width='100%'>"
                StringaHTML = StringaHTML & "<tr>"

                StringaHTML = StringaHTML & "<td width='50%'><font face='Arial' size='2'>ISE</font></td>"
                StringaHTML = StringaHTML & "<td width='50%'><font face='Arial' size='2'>ISEE ERP</font></td>"

                StringaHTML = StringaHTML & "</tr>"

                Dim Motivo As String

                Motivo = ""

                StringaHTML = StringaHTML & "<tr>"

                StringaHTML = StringaHTML & "<td width='50%'><font face='Arial' size='1'>" & par.IfNull(myReader("ISE_ERP"), "") & "</font></td>"
                StringaHTML = StringaHTML & "<td width='50%'><font face='Arial' size='1'>" & par.IfNull(myReader("REDDITO_ISEE"), "") & "</font></td>"


                StringaHTML = StringaHTML & "</tr>"
                StringaHTML = StringaHTML & "</table>"


                ''NOTE PREFERENZE
                bStringaHTML176 = False

                STRINGAHTML176 = STRINGAHTML176 & "<p><b><font face='Arial' size='3'>NOTE</font></b></p>"
                STRINGAHTML176 = STRINGAHTML176 & "<table border='1' cellpadding='0' cellspacing='0' width='100%'>"
                STRINGAHTML176 = STRINGAHTML176 & "<tr>"
                STRINGAHTML176 = STRINGAHTML176 & "<td width='100%'><font face='Arial' size='2'>DESCRIZIONE</font></td>"



                STRINGAHTML176 = STRINGAHTML176 & "</tr>"

                If par.IfNull(myReader("NOTE"), "") <> "" Then
                    bStringaHTML176 = True
                    STRINGAHTML176 = STRINGAHTML176 & "<tr>"
                    STRINGAHTML176 = STRINGAHTML176 & "<td width='100%'><font face='Arial' size='1'>" & par.IfNull(myReader("NOTE"), "") & "</font></td>"

                    STRINGAHTML176 = STRINGAHTML176 & "</tr>"

                End If

                STRINGAHTML176 = STRINGAHTML176 & "</table>"


                'CORRELAZIONI CON PED

                bStringaHTML1000 = False

                StringaHTML1000 = StringaHTML1000 & "<p><b><font face='Arial' size='3'>CORRELAZIONI PED</font></b></p>"
                StringaHTML1000 = StringaHTML1000 & "<table border='1' cellpadding='0' cellspacing='0' width='100%'>"
                StringaHTML1000 = StringaHTML1000 & "<tr>"
                StringaHTML1000 = StringaHTML1000 & "<td width='15%'><font face='Arial' size='2'>NOMINATIVO</font></td>"
                StringaHTML1000 = StringaHTML1000 & "<td width='15%'><font face='Arial' size='2'>COD.FISCALE</font></td>"
                StringaHTML1000 = StringaHTML1000 & "<td width='14%'><font face='Arial' size='2'>INDIRIZZO</font></td>"
                StringaHTML1000 = StringaHTML1000 & "<td width='18%'><font face='Arial' size='2'>TIPO/STATO CONTRATTO</font></td>"
                StringaHTML1000 = StringaHTML1000 & "<td width='14%'><font face='Arial' size='2'>GESTORE ATTUALE</font></td>"
                StringaHTML1000 = StringaHTML1000 & "<td width='14%'><font face='Arial' size='2'>CODICE U.I.</font></td>"
                StringaHTML1000 = StringaHTML1000 & "<td width='10%'><font face='Arial' size='2'>PERTINENZE</font></td>"


                StringaHTML1000 = StringaHTML1000 & "</tr>"

                Dim GESTORE As String
                Dim PERTINENZA As String


                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_cambi WHERE ID_DICHIARAZIONE=" & myReader("ID_DICHIARAZIONE") & " ORDER BY PROGR ASC"
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE,INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO"",INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.ID,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE AS ""STATO"",TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS ""TIPO"" FROM SISCOM_MI.EDIFICI,SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE UNITA_IMMOBILIARI.ID_EDIFICIO=EDIFICI.ID AND INDIRIZZI.ID=EDIFICI.ID_INDIRIZZO_PRINCIPALE AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND  RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR=TIPOLOGIA_RAPP_CONTRATTUALE.COD (+) AND RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC=TIPOLOGIA_CONTRATTO_LOCAZIONE.COD (+) AND RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND ANAGRAFICA.COD_FISCALE='" & myReader1("COD_FISCALE") & "'"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read
                        If par.IfNull(myReader2("ID_unita_principale"), 0) = 0 Then
                            bStringaHTML1000 = True

                            Select Case Mid(CStr(par.IfNull(myReader2("ID"), "")), 1, 1)
                                Case "1"
                                    GESTORE = "GEFI"
                                Case "2"
                                    GESTORE = "PIRELLI"
                                Case "3"
                                    GESTORE = "ROMEO"
                                Case Else
                                    GESTORE = ""
                            End Select

                            PERTINENZA = ""
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_UNITA_PRINCIPALE=" & par.IfNull(myReader2("ID"), "-1")
                            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            While myReader3.Read
                                PERTINENZA = PERTINENZA & par.IfNull(myReader3("COD_UNITA_IMMOBILIARE"), "") & " - "
                            End While
                            myReader3.Close()
                            If PERTINENZA <> "" Then PERTINENZA = Mid(PERTINENZA, 1, Len(PERTINENZA) - 2)

                            StringaHTML1000 = StringaHTML1000 & "<tr>"
                            StringaHTML1000 = StringaHTML1000 & "<td width='15%'><font face='Arial' size='1'>" & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "") & "</font></td>"
                            StringaHTML1000 = StringaHTML1000 & "<td width='15%'><font face='Arial' size='1'>" & par.IfNull(myReader1("COD_FISCALE"), "") & "</font></td>"
                            StringaHTML1000 = StringaHTML1000 & "<td width='14%'><font face='Arial' size='1'>" & par.IfNull(myReader2("INDIRIZZO"), "") & ", " & par.IfNull(myReader2("CIVICO"), "") & "</font></td>"
                            StringaHTML1000 = StringaHTML1000 & "<td width='18%'><font face='Arial' size='1'>" & par.IfNull(myReader2("TIPO"), "") & " / " & par.IfNull(myReader2("STATO"), "") & "</font></td>"
                            StringaHTML1000 = StringaHTML1000 & "<td width='14%'><font face='Arial' size='1'>" & GESTORE & "</font></td>"
                            StringaHTML1000 = StringaHTML1000 & "<td width='14%'><font face='Arial' size='1'>" & par.IfNull(myReader2("COD_UNITA_IMMOBILIARE"), "") & "</font></td>"
                            StringaHTML1000 = StringaHTML1000 & "<td width='10%'><font face='Arial' size='1'>" & PERTINENZA & "</font></td>"

                            StringaHTML1000 = StringaHTML1000 & "</tr>"
                        End If
                    End While
                    myReader2.Close()

                End While
                myReader1.Close()

                StringaHTML1000 = StringaHTML1000 & "</table>"



                StringaHTML1001 = ""

                'CORRELAZIONI CON ANAGRAFE UTENZA

                bStringaHTML1001 = False

                StringaHTML1001 = StringaHTML1001 & "<p><b><font face='Arial' size='3'>CORRELAZIONI ANAGRAFE UTENZA</font></b></p>"
                StringaHTML1001 = StringaHTML1001 & "<table border='1' cellpadding='0' cellspacing='0' width='100%'>"
                StringaHTML1001 = StringaHTML1001 & "<tr>"
                StringaHTML1001 = StringaHTML1001 & "<td width='15%'><font face='Arial' size='2'>NOMINATIVO</font></td>"
                StringaHTML1001 = StringaHTML1001 & "<td width='10%'><font face='Arial' size='2'>COD.FISCALE</font></td>"
                StringaHTML1001 = StringaHTML1001 & "<td width='8%'><font face='Arial' size='2'>INTESTATARIO</font></td>"
                StringaHTML1001 = StringaHTML1001 & "<td width='16%'><font face='Arial' size='2'>INDIRIZZO</font></td>"
                StringaHTML1001 = StringaHTML1001 & "<td width='12%'><font face='Arial' size='2'>TIPO C.</font></td>"
                'StringaHTML1001 = StringaHTML1001 & "<td width='14%'><font face='Arial' size='2'>GESTORE ATTUALE</font></td>"
                StringaHTML1001 = StringaHTML1001 & "<td width='13%'><font face='Arial' size='2'>ANNO REDD.</font></td>"
                StringaHTML1001 = StringaHTML1001 & "<td width='16%'><font face='Arial' size='2'>REDDITI DICHIARATI</font></td>"
                StringaHTML1001 = StringaHTML1001 & "<td width='10%'><font face='Arial' size='2'>CODICE U.I.</font></td>"


                StringaHTML1001 = StringaHTML1001 & "</tr>"

                Dim MiaTab As String


                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_cambi WHERE ID_DICHIARAZIONE=" & myReader("ID_DICHIARAZIONE") & " ORDER BY PROGR ASC"
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    par.cmd.CommandText = "select * from utenza_comp_nucleo where cod_fiscale='" & myReader1("COD_FISCALE") & "'"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read
                        MiaTab = "<table cellpadding='0' cellspacing='0' width='100%'>"
                        bStringaHTML1001 = True

                        par.cmd.CommandText = "select * from utenza_dichiarazioni where id=" & myReader2("id_dichiarazione")
                        Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader123.Read Then
                            StringaHTML1001 = StringaHTML1001 & "<tr>"
                            StringaHTML1001 = StringaHTML1001 & "<td width='15%'><font face='Arial' size='1'>" & par.IfNull(myReader2("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "") & "</font></td>"
                            StringaHTML1001 = StringaHTML1001 & "<td width='10%'><font face='Arial' size='1'>" & par.IfNull(myReader2("COD_FISCALE"), "") & "</font></td>"

                            If myReader2("PROGR") = 0 Then
                                StringaHTML1001 = StringaHTML1001 & "<td width='8%'><font face='Arial' size='1'>SI</font></td>"
                            Else
                                StringaHTML1001 = StringaHTML1001 & "<td width='8%'><font face='Arial' size='1'>NO</font></td>"
                            End If

                            StringaHTML1001 = StringaHTML1001 & "<td width='16%'><font face='Arial' size='1'>" & par.IfNull(myReader123("ind_res_dnte"), "") & ", " & par.IfNull(myReader123("CIVICO_res_dnte"), "") & "</font></td>"
                            If par.IfNull(myReader123("ind_res_dnte"), "0") = "0" Then
                                StringaHTML1001 = StringaHTML1001 & "<td width='12%'><font face='Arial' size='1'>EC</font></td>"
                            Else
                                StringaHTML1001 = StringaHTML1001 & "<td width='12%'><font face='Arial' size='1'>ERP</font></td>"
                            End If
                            StringaHTML1001 = StringaHTML1001 & "<td width='13%'><font face='Arial' size='1'>" & par.IfNull(myReader123("anno_sit_economica"), "") & "</font></td>"

                            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & myReader2("ID")
                            Dim myReader33 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            While myReader33.Read
                                MiaTab = MiaTab & vbCrLf
                                MiaTab = MiaTab & "<tr>" & "<td width='50%'><font face='Arial' size='1'>SPESE</font></td>" & vbCrLf & "<td width='50%'><font face='Arial' size='1'>" & Format(par.IfNull(myReader33("IMPORTO"), ""), "##0.00") & "</font></td></tr>"

                            End While
                            myReader33.Close()
                            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & myReader2("ID")
                            myReader33 = par.cmd.ExecuteReader()
                            While myReader33.Read
                                MiaTab = MiaTab & vbCrLf
                                MiaTab = MiaTab & "<tr>" & "<td width='50%'><font face='Arial' size='1'>MOB.</font></td>" & vbCrLf & "<td width='50%'><font face='Arial' size='1'>" & Format(par.IfNull(myReader33("IMPORTO"), ""), "##0.00") & "</font></td></tr>"
                            End While
                            myReader33.Close()

                            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & myReader2("ID")
                            myReader33 = par.cmd.ExecuteReader()
                            While myReader33.Read
                                MiaTab = MiaTab & vbCrLf
                                MiaTab = MiaTab & "<tr>" & "<td width='50%'><font face='Arial' size='1'>IMMOB.</font></td>" & vbCrLf & "<td width='50%'><font face='Arial' size='1'>" & Format(par.IfNull(myReader33("IMPORTO"), ""), "##0.00") & "</font></td></tr>"
                            End While
                            myReader33.Close()

                            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & myReader2("ID")
                            myReader33 = par.cmd.ExecuteReader()
                            While myReader33.Read
                                MiaTab = MiaTab & vbCrLf
                                MiaTab = MiaTab & "<tr>" & "<td width='50%'><font face='Arial' size='1'>IRPEF</font></td>" & vbCrLf & "<td width='50%'><font face='Arial' size='1'>" & Format(par.IfNull(myReader33("REDDITO_IRPEF"), ""), "##0.00") & "</font></td></tr>"
                                MiaTab = MiaTab & "<tr>" & "<td width='50%'><font face='Arial' size='1'>AGRAR</font></td>" & vbCrLf & "<td width='50%'><font face='Arial' size='1'>" & Format(par.IfNull(myReader33("PROV_AGRARI"), ""), "##0.00") & "</font></td></tr>"
                            End While
                            myReader33.Close()

                            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & myReader2("ID")
                            myReader33 = par.cmd.ExecuteReader()
                            While myReader33.Read
                                MiaTab = MiaTab & vbCrLf
                                MiaTab = MiaTab & "<tr>" & "<td width='50%'><font face='Arial' size='1'>ALTRI R.</font></td>" & vbCrLf & "<td width='50%'><font face='Arial' size='1'>" & Format(par.IfNull(myReader33("IMPORTO"), ""), "##0.00") & "</font></td></tr>"
                            End While
                            myReader33.Close()

                            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_DETRAZIONI WHERE ID_COMPONENTE=" & myReader2("ID")
                            myReader33 = par.cmd.ExecuteReader()
                            While myReader33.Read
                                MiaTab = MiaTab & vbCrLf
                                MiaTab = MiaTab & "<tr>" & "<td width='50%'><font face='Arial' size='1'>DETRAZ.</font></td>" & vbCrLf & "<td width='50%'><font face='Arial' size='1'>" & Format(par.IfNull(myReader33("IMPORTO"), ""), "##0.00") & "</font></td></tr>"
                            End While
                            myReader33.Close()


                            If MiaTab <> "<table cellpadding='0' cellspacing='0' width='100%'>" Then
                                StringaHTML1001 = StringaHTML1001 & "<td width='16%'><font face='Arial' size='1'>" & MiaTab & "</table></font></td>"
                            Else
                                StringaHTML1001 = StringaHTML1001 & "<td width='16%'><font face='Arial' size='1'>---</font></td>"
                            End If
                            StringaHTML1001 = StringaHTML1001 & "<td width='10%'><font face='Arial' size='1'>" & par.IfNull(myReader123("posizione"), "") & "</font></td>"

                            StringaHTML1001 = StringaHTML1001 & "</tr>"
                        End If
                        myReader123.Close()

                    End While


                    myReader2.Close()

                End While
                myReader1.Close()

                StringaHTML1001 = StringaHTML1001 & "</table>"







                If bStringaHTML111 = False Then StringaHTML111 = ""
                If bStringaHTML150 = False Then StringaHTML150 = ""
                If bStringaHTML1000 = False Then StringaHTML1000 = ""
                If bStringaHTML1001 = False Then StringaHTML1001 = ""

                If StringaHTML_8 <> "" Then StringaHTML_8 = StringaHTML_8 & "</table>"
                If StringaHTML_7 <> "" Then StringaHTML_7 = StringaHTML_7 & "</table>"
                If StringaHTML_9 <> "" Then StringaHTML_9 = StringaHTML_9 & "</table>"
                If StringaHTML_10 <> "" Then StringaHTML_10 = StringaHTML_10 & "</table>"
                If StringaHTML_11 <> "" Then StringaHTML_11 = StringaHTML_11 & "</table>"
                If StringaHTML_12 <> "" Then StringaHTML_12 = StringaHTML_12 & "</table>"
                If StringaHTML_13 <> "" Then StringaHTML_13 = StringaHTML_13 & "</table>"
                If StringaHTML_14 <> "" Then StringaHTML_14 = StringaHTML_14 & "</table>"
                If StringaHTML_15 <> "" Then StringaHTML_15 = StringaHTML_15 & "</table>"
                If bStringaHTML151 = False Then StringaHTML151 = ""
                If bStringaHTML176 = False Then STRINGAHTML176 = ""


                Response.Write(StringaHTML & StringaHTML_6 & StringaHTML_1 & StringaHTML_2 & StringaHTML_3 & StringaHTML_4 & StringaHTML_5 & StringaHTML_7 & StringaHTML_8 & StringaHTML1000 & StringaHTML1001 & StringaHTML_9 & StringaHTML_10 & StringaHTML_11 & StringaHTML_12 & StringaHTML150 & StringaHTML151 & StringaHTML_13 & StringaHTML_14 & StringaHTML_15 & StringaHTML111 & STRINGAHTML176 & "<p><font face='Arial' size='2'>Situazione al " & Format(Now, "dd/MM/yyyy") & "</font></p>")
            End If
            myReader.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function
End Class
