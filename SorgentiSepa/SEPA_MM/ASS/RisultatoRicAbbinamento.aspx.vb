
Partial Class ASS_RisultatoRicAbbinamento
    Inherits PageSetIdMode
    Dim par As New CM.Global()
    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreCF As String
    Dim sValorePG As String
    Dim sValoreST As String
    Dim sStringaSql As String
    Dim dtEmerg As New System.Data.DataTable

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
            sValoreCG = Request.QueryString("CG")
            sValoreNM = Request.QueryString("NM")
            sValoreCF = Request.QueryString("CF")
            sValorePG = Request.QueryString("PG")
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Text = "-1"
            HiddenField1.Value = Request.QueryString("T")
            Select Case Request.QueryString("T")
                Case "1"
                    Cerca()
                Case "2"
                    CercaCambi()
                Case "3"
                    cercaEmergenze()
            End Select


        End If
    End Sub



    Private Function CercaEmergenze()
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
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO_vsa.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreNM <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreNM
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO_vsa.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreCF <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreCF
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO_vsa.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValorePG <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_vsa.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If

        If sStringaSql <> "" Then
            sStringaSql = sStringaSql & " AND "
        End If

        'sStringaSQL1 = "SELECT  (DOMANDE_BANDO_VSA_MOT_CAMBI.AI||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||dOMANDE_BANDO_VSA_MOT_CAMBI.RU||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.RI||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.AA||DOMANDE_BANDO_VSA_MOT_CAMBI.HANDICAP||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HM||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HA||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL=100 AND INDENNITA_ACC='1'),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HT||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL=100 AND INDENNITA_ACC='0'),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HP||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66 AND PERC_INVAL<100),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.AN||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND (SYSDATE-TO_DATE(DATA_NASCITA,'YYYYmmdd'))/365>65),'00000000'))||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.FS||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.PV||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA AS ORDINE," _
        '& "DOMANDE_BANDO_VSA.ID,DOMANDE_BANDO_VSA.PG, COMP_NUCLEO_VSA.COGNOME, COMP_NUCLEO_VSA.NOME, " _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.PV,0,'NO',1,'SI') AS PV,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.FS,0,'NO',1,'SI') AS FS," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AN,0,'NO',1,'SI') AS AN,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HP,0,'NO',1,'SI') AS HP," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HT,0,'NO',1,'SI') AS HT,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HA,0,'NO',1,'SI') AS HA," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HM,0,'NO',1,'SI') AS HM,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AA,0,'NO',1,'SI') AS AA," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AI,0,'NO',1,'SI') AS AI, DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.RU,0,'NO',1,'SI') AS RU," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.RI,0,'NO',1,'SI') AS RI " _
        '& "FROM COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,DOMANDE_BANDO_VSA_MOT_CAMBI " _
        '& "WHERE " & sStringaSql & " (DOMANDE_BANDO_vsa.FL_PROPOSTA='0' OR DOMANDE_BANDO_vsa.FL_PROPOSTA IS NULL) and COMP_NUCLEO_VSA.PROGR=0 AND " _
        '& " COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA_MOT_CAMBI.ID_DOMANDA=DOMANDE_BANDO_VSA.ID " _
        '& "AND DOMANDE_BANDO_VSA.ID_STATO='9' AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=4 AND (DOMANDE_BANDO_VSA.FL_INVITO='1') and DOMANDE_BANDO_vsa.ID_STATO<>'10' ORDER BY ORDINE DESC"
        dtEmerg.Columns.Add("ID")
        dtEmerg.Columns.Add("PG")
        dtEmerg.Columns.Add("TIPO_ALLOGGIO")
        dtEmerg.Columns.Add("POSIZIONE")
        dtEmerg.Columns.Add("COGNOME")
        dtEmerg.Columns.Add("NOME")
        dtEmerg.Columns.Add("ISBARC_R")
        dtEmerg.Columns.Add("REDDITO_ISEE")
        dtEmerg.Columns.Add("N_COMP_NUCLEO")
        dtEmerg.Columns.Add("ART")

        Dim strArt22 As String = ""
        Dim ElencoIdDomEMERG As String = ""
        strArt22 = "SELECT domande_bando_vsa.ID, SUM (punteggio) AS punteggio" _
            & " FROM tab_punti_emergenze, domande_bando_vsa_punti_em, domande_bando_vsa" _
            & " WHERE domande_bando_vsa_punti_em.id_punteggio = tab_punti_emergenze.ID" _
            & " AND domande_bando_vsa_punti_em.id_domanda = domande_bando_vsa.ID" _
            & " AND (domande_bando_vsa.id_stato = '8' OR domande_bando_vsa.id_stato = '9')" _
            & " AND domande_bando_vsa.id_motivo_domanda = 4" _
            & " GROUP BY domande_bando_vsa.ID" _
            & " ORDER BY punteggio DESC"
        Dim daE As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dtE As New Data.DataTable
        daE = New Oracle.DataAccess.Client.OracleDataAdapter(strArt22, par.OracleConn)

        daE.Fill(dtE)
        daE.Dispose()

        Dim k As Integer = 0
        Dim posizioneGrad As Integer = 0
        Dim strOrderBy As String = ""

        If dtE.Rows.Count > 1 Then
            Do While k <= dtE.Rows.Count - 1
                ElencoIdDomEMERG = ""
                strOrderBy = ""

                posizioneGrad = posizioneGrad + 1
                ElencoIdDomEMERG = ElencoIdDomEMERG & dtE.Rows(k).Item("ID") & ","

                If k <> dtE.Rows.Count - 1 Then
                    Dim idDomPreced As Long = 0
                    Dim idDomSucc As Long = 0

                    If par.IfNull(dtE.Rows(k).Item("punteggio"), 0) = par.IfNull(dtE.Rows(k + 1).Item("punteggio"), 0) Then
                        idDomPreced = dtE.Rows(k).Item("ID")
                        idDomSucc = dtE.Rows(k + 1).Item("ID")
                        ElencoIdDomEMERG = idDomPreced & "," & idDomSucc & ","
                        strOrderBy = " ORDER BY DATA_PRESENTAZIONE ASC"
                        k = k + 1
                    End If

                End If

                If ElencoIdDomEMERG <> "" Then
                    ElencoIdDomEMERG = "(" & Mid(ElencoIdDomEMERG, 1, Len(ElencoIdDomEMERG) - 1) & ")"

                    sStringaSQL1 = "SELECT dichiarazioni_vsa.ISEE AS REDDITO_ISEE,DOMANDE_BANDO_VSA.ID AS ID,DOMANDE_BANDO_VSA.ISBARC_R,DOMANDE_BANDO_VSA.TIPO_ALLOGGIO,DOMANDE_BANDO_VSA.PG,DICHIARAZIONI_VSA.N_COMP_NUCLEO,COMP_NUCLEO_VSA.NOME,COMP_NUCLEO_VSA.COGNOME " _
                        & "FROM DOMANDE_BANDO_VSA,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA WHERE " & sStringaSql & " DOMANDE_BANDO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND COMP_NUCLEO_VSA.PROGR=0 AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID " _
                        & " AND (domande_bando_vsa.fl_proposta = '0' " _
                        & "OR domande_bando_vsa.fl_proposta IS NULL " _
                        & " )" _
                        & "AND domande_bando_vsa.id_stato = '9' " _
                        & "AND domande_bando_vsa.id_motivo_domanda = 4 " _
                        & "AND domande_bando_vsa.fl_invito = '1' " _
                        & "AND DOMANDE_BANDO_VSA.ID IN " & ElencoIdDomEMERG & strOrderBy
                    Dim da0 As Oracle.DataAccess.Client.OracleDataAdapter
                    Dim dtE0 As New Data.DataTable
                    da0 = New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

                    da0.Fill(dtE0)
                    da0.Dispose()

                    Dim ROWnew As System.Data.DataRow
                    For Each row1 As Data.DataRow In dtE0.Rows
                        ROWnew = dtEmerg.NewRow()
                        ROWnew.Item("ID") = row1.Item("ID")
                        ROWnew.Item("REDDITO_ISEE") = row1.Item("REDDITO_ISEE")
                        ROWnew.Item("ISBARC_R") = row1.Item("ISBARC_R")
                        ROWnew.Item("N_COMP_NUCLEO") = row1.Item("N_COMP_NUCLEO")
                        ROWnew.Item("PG") = row1.Item("PG")
                        ROWnew.Item("POSIZIONE") = posizioneGrad
                        ROWnew.Item("COGNOME") = row1.Item("COGNOME")
                        ROWnew.Item("NOME") = row1.Item("NOME")
                        ROWnew.Item("ART") = "Art.22C.10"
                        ROWnew.Item("TIPO_ALLOGGIO") = row1.Item("TIPO_ALLOGGIO")

                        dtEmerg.Rows.Add(ROWnew)
                    Next
                End If
                k = k + 1
            Loop
            BindGridEmergenze(dtEmerg)
        Else
            For Each row As Data.DataRow In dtE.Rows
                posizioneGrad = posizioneGrad + 1
                ElencoIdDomEMERG = ElencoIdDomEMERG & row.Item("ID") & ","


                If ElencoIdDomEMERG <> "" Then
                    ElencoIdDomEMERG = "(" & Mid(ElencoIdDomEMERG, 1, Len(ElencoIdDomEMERG) - 1) & ")"

                    sStringaSQL1 = "SELECT dichiarazioni_vsa.ISEE AS REDDITO_ISEE,DOMANDE_BANDO_VSA.ID AS ID,DOMANDE_BANDO_VSA.ISBARC_R,DOMANDE_BANDO_VSA.TIPO_ALLOGGIO,DOMANDE_BANDO_VSA.PG,DICHIARAZIONI_VSA.N_COMP_NUCLEO,COMP_NUCLEO_VSA.NOME,COMP_NUCLEO_VSA.COGNOME " _
                        & "FROM " & sStringaSql & " DOMANDE_BANDO_VSA,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA WHERE DOMANDE_BANDO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND COMP_NUCLEO_VSA.PROGR=0 AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND DOMANDE_BANDO_VSA.ID IN " & ElencoIdDomEMERG
                    Dim da0 As Oracle.DataAccess.Client.OracleDataAdapter
                    Dim dtE0 As New Data.DataTable
                    da0 = New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

                    da0.Fill(dtE0)
                    da0.Dispose()

                    Dim ROWnew As System.Data.DataRow
                    For Each row1 As Data.DataRow In dtE0.Rows
                        ROWnew = dtEmerg.NewRow()
                        ROWnew.Item("ID") = row1.Item("ID")
                        ROWnew.Item("REDDITO_ISEE") = row1.Item("REDDITO_ISEE")
                        ROWnew.Item("ISBARC_R") = row1.Item("ISBARC_R")
                        ROWnew.Item("N_COMP_NUCLEO") = row1.Item("N_COMP_NUCLEO")
                        ROWnew.Item("PG") = row1.Item("PG")
                        ROWnew.Item("POSIZIONE") = posizioneGrad
                        ROWnew.Item("COGNOME") = row1.Item("COGNOME")
                        ROWnew.Item("NOME") = row1.Item("NOME")
                        ROWnew.Item("ART") = "Art.22C.10"
                        ROWnew.Item("TIPO_ALLOGGIO") = row1.Item("TIPO_ALLOGGIO")

                        dtEmerg.Rows.Add(ROWnew)
                    Next

                End If
            Next
            BindGridEmergenze(dtEmerg)
        End If


    End Function


    Private Sub BindGridEmergenze(ByVal dt As System.Data.DataTable)

        DataGrid1.DataSource = dt
        DataGrid1.DataBind()
        Label6.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & dt.Rows.Count
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Private Function CercaCambi()
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
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO_CAMBI.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreNM <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreNM
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO_CAMBI.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreCF <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreCF
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO_CAMBI.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValorePG <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_CAMBI.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If



        sStringaSQL1 = "SELECT trunc(DOMANDE_BANDO_CAMBI.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_CAMBI.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def_CAMBI.posizione,DOMANDE_BANDO_CAMBI.ID,COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME," _
                       & "DOMANDE_BANDO_CAMBI.PG AS ""PG"" " _
                       & " FROM bandi_graduatoria_def_CAMBI,DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI,DICHIARAZIONI_CAMBI " _
                       & " WHERE DOMANDE_BANDO_CAMBI.FL_ASS_ESTERNA='1' AND (DOMANDE_BANDO_CAMBI.FL_PROPOSTA='0' or DOMANDE_BANDO_CAMBI.FL_PROPOSTA is null) AND DOMANDE_BANDO_CAMBI.PROGR_COMPONENTE=COMP_NUCLEO_CAMBI.PROGR AND DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID " _
                       & " AND  domande_bando_CAMBI.id = bandi_graduatoria_def_CAMBI.id_domanda and " _
                       & " DOMANDE_BANDO_CAMBI.FL_INVITO='1' AND DOMANDE_BANDO_CAMBI.ID_STATO<>'10' AND DOMANDE_BANDO_CAMBI.FL_PRATICA_CHIUSA<>'1' " _
                       & "AND (DOMANDE_BANDO_CAMBI.ID_STATO='9')"

        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY bandi_graduatoria_def_CAMBI.posizione ASC"
        'par.OracleConn.Open()
        'Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSQL1, par.OracleConn)
        'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        'Label3.Text = "0"
        'Do While myReader.Read()
        '    Label3.Text = CInt(Label3.Text) + 1
        'Loop
        'Label3.Text = Label3.Text
        'cmd.Dispose()
        'myReader.Close()
        'par.OracleConn.Close()
        BindGridCambi()
    End Function

    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String

        bTrovato = False
        sStringaSql = ""

        If sValoreCG <> "" Then
            sValore = sValoreCG
            If InStr(sValore, "*") Then
                SCOMPARA = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                SCOMPARA = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreNM <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreNM
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreCF <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreCF
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " COMP_NUCLEO.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValorePG <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If



        sStringaSQL1 = "SELECT trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def.posizione,DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME," _
                       & "DOMANDE_BANDO.PG AS ""PG"" " _
                       & "" _
                       & "" _
                       & "" _
                       & "" _
                       & " " _
                       & " FROM bandi_graduatoria_def,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI " _
                       & " WHERE  DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND  (DOMANDE_BANDO.FL_PROPOSTA='0' or DOMANDE_BANDO.FL_PROPOSTA is null) AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
                       & " AND  domande_bando.id = bandi_graduatoria_def.id_domanda and " _
                       & "" _
                       & "" _
                       & "" _
                       & "" _
                       & " DOMANDE_BANDO.FL_INVITO='1' AND DOMANDE_BANDO.ID_STATO<>'10' AND DOMANDE_BANDO.FL_PRATICA_CHIUSA<>'1' " _
                       & " " _
                       & "AND (DOMANDE_BANDO.ID_STATO='9')"

        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY bandi_graduatoria_def.posizione ASC,domande_bando.DATA_PG ASC,domande_bando.isbarc_r DESC"
        'par.OracleConn.Open()
        'Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSQL1, par.OracleConn)
        'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        'Label3.Text = "0"
        'Do While myReader.Read()
        '    Label3.Text = CInt(Label3.Text) + 1
        'Loop
        'Label3.Text = Label3.Text
        'cmd.Dispose()
        'myReader.Close()
        'par.OracleConn.Close()
        BindGrid()
    End Function

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

    Private Sub BindGridCambi()
        Try

            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            Label6.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
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
            Label6.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text
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

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If LBLID.Text = "-1" Or LBLID.Text = "" Then
            Response.Write("<script>alert('Nessuna Domanda selezionata!')</script>")
        Else
            Response.Write("<script>location.replace('AbbinaDomanda.aspx?ID=" & LBLID.Text & "&TIPO=" & HiddenField1.Value & "');</script>")
            'Response.Write("<script>location.replace('AbbinaDomandaNUOVO.aspx?ID=" & LBLID.Text & "&TIPO=" & HiddenField1.Value & "');</script>")
        End If
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaAbbinamento.aspx""</script>")
    End Sub

    Protected Sub DataGrid2_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text
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
            'BindGridEmergenze()
        End If
    End Sub

    Protected Sub DataGrid2_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.UpdateCommand
        LBLID.Text = e.Item.Cells(0).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text
    End Sub
End Class
