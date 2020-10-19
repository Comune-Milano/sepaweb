
Partial Class ASS_Inviti
    Inherits PageSetIdMode
    Dim par As New CM.Global()
    'Dim sTipo As String
    Dim sDA As String
    Dim sA As String
    Dim sPG As String
    Dim scriptblock As String
    Dim dtEmerg As New System.Data.DataTable


    Public Property sTipo() As String
        Get
            If Not (ViewState("par_sTipo") Is Nothing) Then
                Return CStr(ViewState("par_sTipo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sTipo") = value
        End Set

    End Property

    Public Property sCognome() As String
        Get
            If Not (ViewState("par_sCognome") Is Nothing) Then
                Return CStr(ViewState("par_sCognome"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCognome") = value
        End Set

    End Property

    Public Property sNome() As String
        Get
            If Not (ViewState("par_sNome") Is Nothing) Then
                Return CStr(ViewState("par_sNome"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNome") = value
        End Set

    End Property

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
            sCognome = Request.QueryString("C")
            sNome = Request.QueryString("N")
            sTipo = Request.QueryString("TIPO")
            sDA = ""
            sA = ""
            sPG = Request.QueryString("PG")
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Text = "-1"
            Select Case sTipo
                Case "BANDO CAMBI"
                    TipoBando.Value = "1"
                    CercaCambi()
                Case "CAMBI EMERGENZA"
                    TipoBando.Value = "2"
                    CercaEmergenze()
                Case Else
                    TipoBando.Value = "0"
                    Cerca()
            End Select
            'If sTipo = "BANDO CAMBI" Then
            '    CERCACAMBI()
            'Else
            '    Cerca()
            'End If

        End If
    End Sub


    Function CercaEmergenze()
        Dim Ms_Stringa As String
        Dim Ms_Stringa1 As String
        Dim m As Boolean

        m = False

        Ms_Stringa = ""
       
        If sPG <> "" Then
            Ms_Stringa = " AND domande_bando_vsa.pg='" & par.PulisciStrSql(sPG) & "' "
            m = True
        End If

        If sCognome <> "" Then
            Ms_Stringa = " AND comp_nucleo_vsa.cognome='" & par.PulisciStrSql(sCognome) & "' "
            m = True
        End If

        If sNome <> "" Then
            Ms_Stringa = " AND comp_nucleo_vsa.Nome='" & par.PulisciStrSql(sNome) & "' "
            m = True
        End If

        'sStringaSQL1 = "SELECT  DOMANDE_BANDO_VSA.TIPO_ALLOGGIO,(DOMANDE_BANDO_VSA_MOT_CAMBI.AI||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||dOMANDE_BANDO_VSA_MOT_CAMBI.RU||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.RI||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.AA||DOMANDE_BANDO_VSA_MOT_CAMBI.HANDICAP||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HM||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HA||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL=100 AND INDENNITA_ACC='1'),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HT||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL=100 AND INDENNITA_ACC='0'),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HP||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66 AND PERC_INVAL<100),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.AN||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND (SYSDATE-TO_DATE(DATA_NASCITA,'YYYYmmdd'))/365>65),'00000000'))||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.FS||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.PV||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA AS ORDINE," _
        '& "DOMANDE_BANDO_VSA.ID,DOMANDE_BANDO_VSA.PG, COMP_NUCLEO_VSA.COGNOME, COMP_NUCLEO_VSA.NOME, " _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.PV,0,'NO',1,'SI') AS PV,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.FS,0,'NO',1,'SI') AS FS," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AN,0,'NO',1,'SI') AS AN,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HP,0,'NO',1,'SI') AS HP," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HT,0,'NO',1,'SI') AS HT,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HA,0,'NO',1,'SI') AS HA," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HM,0,'NO',1,'SI') AS HM,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AA,0,'NO',1,'SI') AS AA," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AI,0,'NO',1,'SI') AS AI, DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.RU,0,'NO',1,'SI') AS RU," _
        '& "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.RI,0,'NO',1,'SI') AS RI " _
        '& "FROM COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,DOMANDE_BANDO_VSA_MOT_CAMBI " _
        '& "WHERE (DOMANDE_BANDO_vsa.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) " _
        '& " OR DOMANDE_BANDO_vsa.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1)) and " _
        '& " (DOMANDE_BANDO_vsa.FL_PROPOSTA='0' OR DOMANDE_BANDO_vsa.FL_PROPOSTA IS NULL) and COMP_NUCLEO_VSA.PROGR=0 AND " _
        '& " COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA_MOT_CAMBI.ID_DOMANDA=DOMANDE_BANDO_VSA.ID " _
        '& "AND DOMANDE_BANDO_VSA.ID_STATO='9' AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=4 AND (DOMANDE_BANDO_VSA.FL_INVITO='0' OR DOMANDE_BANDO_VSA.FL_INVITO IS NULL) " & Ms_Stringa & " ORDER BY ORDINE DESC"

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
            & " AND domande_bando_vsa.id_motivo_domanda = 4 " _
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
        Dim idDomPreced As Long = 0
        Dim idDomSucc As Long = 0

        If dtE.Rows.Count > 1 Then
            Do While k <= dtE.Rows.Count - 1
                ElencoIdDomEMERG = ""
                strOrderBy = ""
                idDomPreced = 0
                idDomSucc = 0

                posizioneGrad = posizioneGrad + 1
                ElencoIdDomEMERG = ElencoIdDomEMERG & dtE.Rows(k).Item("ID") & ","

                If k <> dtE.Rows.Count - 1 Then
                    
                    If par.IfNull(dtE.Rows(k).Item("punteggio"), 0) = par.IfNull(dtE.Rows(k + 1).Item("punteggio"), 0) Then
                        idDomPreced = dtE.Rows(k).Item("ID")
                        idDomSucc = dtE.Rows(k + 1).Item("ID")
                        ElencoIdDomEMERG = idDomPreced & "," & idDomSucc & ","
                        strOrderBy = " ORDER BY DATA_PRESENTAZIONE ASC"
                        k = k + 1
                        posizioneGrad = posizioneGrad + 1
                    End If

                End If

                If ElencoIdDomEMERG <> "" Then
                    ElencoIdDomEMERG = "(" & Mid(ElencoIdDomEMERG, 1, Len(ElencoIdDomEMERG) - 1) & ")"

                    sStringaSQL1 = "SELECT dichiarazioni_vsa.ISEE AS REDDITO_ISEE,DOMANDE_BANDO_VSA.ID AS ID,DOMANDE_BANDO_VSA.ISBARC_R,DOMANDE_BANDO_VSA.TIPO_ALLOGGIO,DOMANDE_BANDO_VSA.PG,DICHIARAZIONI_VSA.N_COMP_NUCLEO,COMP_NUCLEO_VSA.NOME,COMP_NUCLEO_VSA.COGNOME " _
                        & "FROM DOMANDE_BANDO_VSA,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA WHERE DOMANDE_BANDO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND COMP_NUCLEO_VSA.PROGR=0 AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID " _
                        & " AND (domande_bando_vsa.ID NOT IN ( " _
                        & "SELECT DISTINCT id_pratica " _
                        & "FROM rel_prat_all_ccaa_erp " _
                        & "WHERE esito <> 3 " _
                        & " AND esito <> 4 " _
                        & " AND ultimo = 1) " _
                        & "OR domande_bando_vsa.ID IN (SELECT DISTINCT id_pratica " _
                        & "FROM rel_prat_all_ccaa_erp " _
                        & " WHERE esito = 0 AND ultimo = 1)" _
                        & " )" _
                        & "AND (domande_bando_vsa.fl_proposta = '0' " _
                        & "OR domande_bando_vsa.fl_proposta IS NULL " _
                        & " )" _
                        & "AND domande_bando_vsa.id_stato = '9' " _
                        & "AND domande_bando_vsa.id_motivo_domanda = 4 " _
                        & "AND (domande_bando_vsa.fl_invito = '0' " _
                        & "OR domande_bando_vsa.fl_invito IS NULL " _
                        & ") " _
                        & "AND DOMANDE_BANDO_VSA.ID IN " & ElencoIdDomEMERG & strOrderBy
                    Dim da0 As Oracle.DataAccess.Client.OracleDataAdapter
                    Dim dtE0 As New Data.DataTable
                    da0 = New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

                    da0.Fill(dtE0)
                    da0.Dispose()

                    Dim ROWnew As System.Data.DataRow
                    For Each row1 As Data.DataRow In dtE0.Rows
                        If par.PulisciStrSql(sPG) = "" And par.PulisciStrSql(sCognome) = "" And par.PulisciStrSql(sNome) = "" Then
                            ROWnew = dtEmerg.NewRow()
                            ROWnew.Item("ID") = row1.Item("ID")
                            ROWnew.Item("REDDITO_ISEE") = row1.Item("REDDITO_ISEE")
                            ROWnew.Item("ISBARC_R") = row1.Item("ISBARC_R")
                            ROWnew.Item("N_COMP_NUCLEO") = row1.Item("N_COMP_NUCLEO")
                            ROWnew.Item("PG") = row1.Item("PG")
                            If row1.Item("ID") = idDomPreced Then
                                ROWnew.Item("POSIZIONE") = posizioneGrad
                                posizioneGrad = posizioneGrad + 1
                            ElseIf row1.Item("ID") = idDomSucc Then
                                ROWnew.Item("POSIZIONE") = posizioneGrad
                            Else
                                ROWnew.Item("POSIZIONE") = posizioneGrad
                            End If
                            ROWnew.Item("COGNOME") = row1.Item("COGNOME")
                            ROWnew.Item("NOME") = row1.Item("NOME")
                            ROWnew.Item("ART") = "Art.22C.10"
                            ROWnew.Item("TIPO_ALLOGGIO") = row1.Item("TIPO_ALLOGGIO")

                            dtEmerg.Rows.Add(ROWnew)
                        Else
                            If row1.Item("PG") = par.PulisciStrSql(sPG) Or row1.Item("COGNOME") = par.PulisciStrSql(sCognome) Or row1.Item("NOME") = par.PulisciStrSql(sNome) Then
                                ROWnew = dtEmerg.NewRow()
                                ROWnew.Item("ID") = row1.Item("ID")
                                ROWnew.Item("REDDITO_ISEE") = row1.Item("REDDITO_ISEE")
                                ROWnew.Item("ISBARC_R") = row1.Item("ISBARC_R")
                                ROWnew.Item("N_COMP_NUCLEO") = row1.Item("N_COMP_NUCLEO")
                                ROWnew.Item("PG") = row1.Item("PG")
                                If row1.Item("ID") = idDomPreced Then
                                    ROWnew.Item("POSIZIONE") = posizioneGrad
                                    posizioneGrad = posizioneGrad + 1
                                ElseIf row1.Item("ID") = idDomSucc Then
                                    ROWnew.Item("POSIZIONE") = posizioneGrad
                                Else
                                    ROWnew.Item("POSIZIONE") = posizioneGrad
                                End If
                                ROWnew.Item("COGNOME") = row1.Item("COGNOME")
                                ROWnew.Item("NOME") = row1.Item("NOME")
                                ROWnew.Item("ART") = "Art.22C.10"
                                ROWnew.Item("TIPO_ALLOGGIO") = row1.Item("TIPO_ALLOGGIO")

                                dtEmerg.Rows.Add(ROWnew)
                            End If
                        End If
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
                        & "FROM DOMANDE_BANDO_VSA,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA WHERE DOMANDE_BANDO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID AND COMP_NUCLEO_VSA.PROGR=0 AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID " _
                        & " AND (domande_bando_vsa.ID NOT IN ( " _
                        & "SELECT DISTINCT id_pratica " _
                        & "FROM rel_prat_all_ccaa_erp " _
                        & "WHERE esito <> 3 " _
                        & " AND esito <> 4 " _
                        & " AND ultimo = 1) " _
                        & "OR domande_bando_vsa.ID IN (SELECT DISTINCT id_pratica " _
                        & "FROM rel_prat_all_ccaa_erp " _
                        & " WHERE esito = 0 AND ultimo = 1)" _
                        & " )" _
                        & "AND (domande_bando_vsa.fl_proposta = '0' " _
                        & "OR domande_bando_vsa.fl_proposta IS NULL " _
                        & " )" _
                        & "AND domande_bando_vsa.id_stato = '9' " _
                        & "AND domande_bando_vsa.id_motivo_domanda = 4 " _
                        & "AND (domande_bando_vsa.fl_invito = '0' " _
                        & "OR domande_bando_vsa.fl_invito IS NULL " _
                        & ") " _
                        & "AND DOMANDE_BANDO_VSA.ID IN " & ElencoIdDomEMERG

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


    Function CercaCambi()
        Dim Ms_Stringa As String
        Dim Ms_Stringa1 As String
        Dim m As Boolean

        m = False

        Ms_Stringa = ""
        If sDA <> "" Then
            Ms_Stringa = " AND BANDI_GRADUATORIA_DEF_cambi.POSIZIONE>=" & sDA
            m = True
        End If

        If sA <> "" Then
            If m = False Then
                Ms_Stringa = " AND BANDI_GRADUATORIA_DEF_cambi.POSIZIONE<=" & sA
                m = True
            Else
                Ms_Stringa = Ms_Stringa & " AND BANDI_GRADUATORIA_DEF_cambi.POSIZIONE<=" & sA
            End If
        End If



        If sPG <> "" Then
            Ms_Stringa = " AND domande_bando_cambi.pg='" & par.PulisciStrSql(sPG) & "' "
            m = True
        End If

        If sCognome <> "" Then
            Ms_Stringa = " AND comp_nucleo_cambi.cognome='" & par.PulisciStrSql(sCognome) & "' "
            m = True
        End If

        If sNome <> "" Then
            Ms_Stringa = " AND comp_nucleo_cambi.Nome='" & par.PulisciStrSql(sNome) & "' "
            m = True
        End If


        Ms_Stringa1 = "(BANDI_GRADUATORIA_DEF_cambi.Tipo = 0 or BANDI_GRADUATORIA_DEF_cambi.Tipo = 1) "
        sStringaSQL1 = "SELECT DOMANDE_BANDO_cambi.TIPO_ALLOGGIO,BANDI_GRADUATORIA_DEF_cambi.tipo,BANDI_GRADUATORIA_DEF_cambi.POSIZIONE,BANDI_GRADUATORIA_DEF_cambi.ID as ""d11"",BANDI_GRADUATORIA_DEF_cambi.ID_DOMANDA," _
                   & "DICHIARAZIONI_cambi.N_COMP_NUCLEO,COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME,DOMANDE_BANDO_cambi.PG,DECODE(BANDI_GRADUATORIA_DEF_cambi.Tipo,1,'Art.14') AS ""Art""," _
                   & "DOMANDE_BANDO_cambi.id,DOMANDE_BANDO_cambi.FL_INVITO,trunc(DOMANDE_BANDO_cambi.reddito_isee,2) as ""reddito_isee"",trunc(DOMANDE_BANDO_cambi.isbarc_r,4) as ""isbarc_r"",DOMANDE_BANDO_cambi.FL_CONTROLLA_REQUISITI,T_TIPO_PRATICHE.DESCRIZIONE AS ""D1"" " _
                   & "FROM BANDI_GRADUATORIA_DEF_cambi,DOMANDE_BANDO_cambi,T_TIPO_PRATICHE,COMP_NUCLEO_cambi,DICHIARAZIONI_cambi " _
                   & " WHERE  (DOMANDE_BANDO_cambi.FL_INVITO='' or DOMANDE_BANDO_cambi.FL_INVITO='0' or DOMANDE_BANDO_cambi.FL_INVITO is null) and domande_bando_cambi.fl_controlla_requisiti='2' " & Ms_Stringa _
                   & "  and domande_bando_cambi.FL_ASS_ESTERNA='1' and DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND BANDI_GRADUATORIA_DEF_cambi.ID_DOMANDA=DOMANDE_BANDO_cambi.ID (+) AND " & Ms_Stringa1 _
                   & " and DOMANDE_BANDO_cambi.PROGR_COMPONENTE=COMP_NUCLEO_cambi.PROGR AND COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DOMANDE_BANDO_cambi.ID_DICHIARAZIONE AND " _
                   & "  DOMANDE_BANDO_cambi.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
                   & " (DOMANDE_BANDO_cambi.ID_STATO='9') AND (DOMANDE_BANDO_cambi.FL_PROPOSTA='0' OR DOMANDE_BANDO_cambi.FL_PROPOSTA IS NULL) " _
                   & " AND (DOMANDE_BANDO_cambi.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO_cambi.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
                   & " ORDER BY bandi_graduatoria_def_cambi.posizione ASC,domande_bando_cambi.DATA_PG ASC,domande_bando_cambi.isbarc_r DESC"


        BindGridCambi()
    End Function


    Function Cerca()
        Dim Ms_Stringa As String
        Dim Ms_Stringa1 As String
        Dim m As Boolean

        m = False

        Ms_Stringa = ""
        If sDA <> "" Then
            Ms_Stringa = " AND BANDI_GRADUATORIA_DEF.POSIZIONE>=" & sDA
            m = True
        End If

        If sA <> "" Then
            If m = False Then
                Ms_Stringa = " AND BANDI_GRADUATORIA_DEF.POSIZIONE<=" & sA
                m = True
            Else
                Ms_Stringa = Ms_Stringa & " AND BANDI_GRADUATORIA_DEF.POSIZIONE<=" & sA
            End If
        End If

        If sPG <> "" Then
            Ms_Stringa = " AND domande_bando.pg='" & par.PulisciStrSql(sPG) & "' "
            m = True
        End If

        If sCognome <> "" Then
            Ms_Stringa = " AND comp_nucleo.cognome='" & par.PulisciStrSql(sCognome) & "' "
            m = True
        End If

        If sNome <> "" Then
            Ms_Stringa = " AND comp_nucleo.Nome='" & par.PulisciStrSql(sNome) & "' "
            m = True
        End If

        'If Ms_Stringa <> "" Then
        '    Ms_Stringa = Ms_Stringa & " AND "
        'End If
        ' and domande_bando.fl_controlla_requisiti='2'

        If sTipo = "IDONEE" Then
            Ms_Stringa1 = "(BANDI_GRADUATORIA_DEF.Tipo = 0 or BANDI_GRADUATORIA_DEF.Tipo = 1) "
            sStringaSQL1 = "SELECT DOMANDE_BANDO.TIPO_ALLOGGIO,BANDI_GRADUATORIA_DEF.tipo,BANDI_GRADUATORIA_DEF.POSIZIONE,BANDI_GRADUATORIA_DEF.ID as ""d11"",BANDI_GRADUATORIA_DEF.ID_DOMANDA," _
                       & "DICHIARAZIONI.N_COMP_NUCLEO,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,DOMANDE_BANDO.PG,DECODE(BANDI_GRADUATORIA_DEF.Tipo,1,'Art.14') AS ""Art""," _
                       & "DOMANDE_BANDO.id,DOMANDE_BANDO.FL_INVITO,trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",DOMANDE_BANDO.FL_CONTROLLA_REQUISITI,T_TIPO_PRATICHE.DESCRIZIONE AS ""D1"" " _
                       & "FROM BANDI_GRADUATORIA_DEF,DOMANDE_BANDO,T_TIPO_PRATICHE,COMP_NUCLEO,DICHIARAZIONI " _
                       & " WHERE  (DOMANDE_BANDO.FL_INVITO='' or DOMANDE_BANDO.FL_INVITO='0' or DOMANDE_BANDO.FL_INVITO is null) and domande_bando.fl_controlla_requisiti='2' " & Ms_Stringa _
                       & "  and domande_bando.FL_ASS_ESTERNA='1' and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND BANDI_GRADUATORIA_DEF.ID_DOMANDA=DOMANDE_BANDO.ID (+) AND " & Ms_Stringa1 _
                       & " and DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND " _
                       & "  DOMANDE_BANDO.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
                       & " (DOMANDE_BANDO.ID_STATO='9' or DOMANDE_BANDO.ID_STATO='8') AND (DOMANDE_BANDO.FL_PROPOSTA='0' OR DOMANDE_BANDO.FL_PROPOSTA IS NULL) " _
                       & " AND (DOMANDE_BANDO.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
                       & " ORDER BY bandi_graduatoria_def.posizione ASC,domande_bando.DATA_PG ASC,domande_bando.isbarc_r DESC"
        End If
        If sTipo = "Art14" Then
            Ms_Stringa1 = "BANDI_GRADUATORIA_DEF.Tipo = 1 "

            sStringaSQL1 = "SELECT DOMANDE_BANDO.TIPO_ALLOGGIO,BANDI_GRADUATORIA_DEF.tipo,BANDI_GRADUATORIA_DEF.POSIZIONE,BANDI_GRADUATORIA_DEF.ID as ""d11"",BANDI_GRADUATORIA_DEF.ID_DOMANDA," _
                       & "DICHIARAZIONI.N_COMP_NUCLEO,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,DOMANDE_BANDO.PG,DECODE(BANDI_GRADUATORIA_DEF.Tipo,1,'Art.14') AS ""Art""," _
                       & "DOMANDE_BANDO.id,DOMANDE_BANDO.FL_INVITO,trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",DOMANDE_BANDO.FL_CONTROLLA_REQUISITI,T_TIPO_PRATICHE.DESCRIZIONE AS ""D1"" " _
                       & "FROM BANDI_GRADUATORIA_DEF,DOMANDE_BANDO,T_TIPO_PRATICHE,COMP_NUCLEO,DICHIARAZIONI " _
                       & " WHERE  (DOMANDE_BANDO.FL_INVITO='' or DOMANDE_BANDO.FL_INVITO='0' or DOMANDE_BANDO.FL_INVITO is null) and domande_bando.fl_controlla_requisiti='2' " & Ms_Stringa _
                       & "  and domande_bando.FL_ASS_ESTERNA='1' and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND BANDI_GRADUATORIA_DEF.ID_DOMANDA=DOMANDE_BANDO.ID (+) AND " & Ms_Stringa1 _
                       & " and DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND " _
                       & "  DOMANDE_BANDO.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
                       & " (DOMANDE_BANDO.ID_STATO='9' or DOMANDE_BANDO.ID_STATO='8' or DOMANDE_BANDO.ID_STATO='7a') AND (DOMANDE_BANDO.FL_PROPOSTA=0 OR DOMANDE_BANDO.FL_PROPOSTA IS NULL) " _
                       & " AND (DOMANDE_BANDO.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
                       & " ORDER BY bandi_graduatoria_def.posizione ASC,domande_bando.DATA_PG ASC,domande_bando.isbarc_r DESC"
        End If
        If sTipo = "Art15 lett.A" Then
            Ms_Stringa1 = "BANDI_GRADUATORIA_DEF.Tipo = 2"

            sStringaSQL1 = "SELECT DOMANDE_BANDO.TIPO_ALLOGGIO,BANDI_GRADUATORIA_DEF.tipo,BANDI_GRADUATORIA_DEF.POSIZIONE,BANDI_GRADUATORIA_DEF.ID as ""d11"",BANDI_GRADUATORIA_DEF.ID_DOMANDA," _
                       & "DICHIARAZIONI.N_COMP_NUCLEO,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,DOMANDE_BANDO.PG,DECODE(BANDI_GRADUATORIA_DEF.Tipo,2,'Art.15') AS ""Art""," _
                       & "DOMANDE_BANDO.id,DOMANDE_BANDO.FL_INVITO,trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",DOMANDE_BANDO.FL_CONTROLLA_REQUISITI,T_TIPO_PRATICHE.DESCRIZIONE AS ""D1"" " _
                       & "FROM DICHIARAZIONI,BANDI_GRADUATORIA_DEF,DOMANDE_BANDO,T_TIPO_PRATICHE,COMP_NUCLEO,deroghe_art_15 " _
                       & " WHERE deroghe_art_15.id_tipo=0 and deroghe_art_15.id_domanda=domande_bando.id and  (DOMANDE_BANDO.FL_INVITO='' or DOMANDE_BANDO.FL_INVITO='0' or DOMANDE_BANDO.FL_INVITO is null) and domande_bando.fl_controlla_requisiti='2' " & Ms_Stringa _
                       & " and domande_bando.FL_ASS_ESTERNA='1' and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND BANDI_GRADUATORIA_DEF.ID_DOMANDA=DOMANDE_BANDO.ID (+) AND " & Ms_Stringa1 _
                       & " and DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND " _
                       & "  DOMANDE_BANDO.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
                       & " (DOMANDE_BANDO.FL_PROPOSTA=0 OR DOMANDE_BANDO.FL_PROPOSTA IS NULL) " _
                       & " AND (DOMANDE_BANDO.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
                       & " ORDER BY bandi_graduatoria_def.posizione ASC,domande_bando.DATA_PG ASC,domande_bando.isbarc_r DESC"
        End If
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

    Private Sub BindGridEmergenze(ByVal dt As System.Data.DataTable)

        

        DataGrid1.DataSource = dt
        DataGrid1.DataBind()
        Label6.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & dt.Rows.Count
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub


    Private Sub BindGridCambi()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "DOMANDE_BANDO_cambi,COMP_NUCLEO_cambi")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label6.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label6.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub


    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        Response.Write("<script>window.open('Disponibilita.aspx?ID=" & e.Item.Cells(0).Text & "&PG=" & e.Item.Cells(1).Text & "&T=" & TipoBando.Value & "','Preferenze','height=745,top=0,left=0,width=797,scrollbars=no');</script>")
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
            Select Case sTipo
                Case "BANDO CAMBI"
                    CercaCambi()
                Case "CAMBI EMERGENZA"
                    CercaEmergenze()
                Case Else
                    Cerca()
            End Select

            'BindGrid()
        End If
    End Sub



    Protected Sub DataGrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        LBLID.Text = e.Item.Cells(0).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text
        HiddenField2.Value = LBLID.Text
        TIPOALLOGGIO.Value = e.Item.Cells(2).Text
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizza.Click
        If LBLID.Text = "-1" Or LBLID.Text = "" Then
            Response.Write("<script>alert('Nessuna Domanda selezionata!')</script>")
        Else
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                Select Case sTipo
                    Case "BANDO CAMBI"
                        par.cmd.CommandText = "SELECT ID FROM DOMANDE_REDDITI_cambi WHERE ID_DOMANDA IN (SELECT ID_DICHIARAZIONE FROM DOMANDE_BANDO_CAMBI WHERE ID=" & LBLID.Text & ")"
                        Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader123.HasRows = True Then


                            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_CAMBI WHERE ID=" & LBLID.Text & " FOR UPDATE NOWAIT"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET id_stato='9',FL_INVITO='1' WHERE ID=" & LBLID.Text
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F08','','I')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReader.Close()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            'par.OracleConn.Dispose()
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('Invito Effettuato!');" _
                            & "</script>"
                            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                            End If
                            Label2.Text = "Nessuna selezione"
                            BindGridCambi()
                        Else
                            If TIPOALLOGGIO.Value = "1" Then
                                par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_CAMBI WHERE ID=" & LBLID.Text & " FOR UPDATE NOWAIT"
                                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReader.Read() Then
                                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET id_stato='9',FL_INVITO='1' WHERE ID=" & LBLID.Text
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                        & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                        & "','F08','','I')"
                                    par.cmd.ExecuteNonQuery()

                                End If
                                myReader.Close()
                                par.OracleConn.Close()
                                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                                'par.OracleConn.Dispose()
                                scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('Invito Effettuato!');" _
                                & "</script>"
                                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                                End If
                                Label2.Text = "Nessuna selezione"
                                BindGridCambi()

                            Else
                                scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('Attenzione, non è possibile invitare. Non risultano inseriti i redditi convenzionali. Si consiglia di restituire la domanda al comune e notificare il problema!');" _
                                & "</script>"
                                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                                End If
                            End If

                        End If
                        myReader123.Close()
                    Case "CAMBI EMERGENZA"
                        par.cmd.CommandText = "SELECT ID FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA IN (SELECT ID_DICHIARAZIONE FROM DOMANDE_BANDO_VSA WHERE ID=" & LBLID.Text & ")"
                        Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader123.HasRows = True Then


                            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID=" & LBLID.Text & " FOR UPDATE NOWAIT"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET id_stato='9',FL_INVITO='1' WHERE ID=" & LBLID.Text
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F08','','I')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReader.Close()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            'par.OracleConn.Dispose()
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('Invito Effettuato!');" _
                            & "</script>"
                            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                            End If
                            Label2.Text = "Nessuna selezione"
                            'BindGridEmergenze(Session.Item("ElencoEmerg"))
                            CercaEmergenze()
                        Else
                            'If TIPOALLOGGIO.Value = "1" Then
                                par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID=" & LBLID.Text & " FOR UPDATE NOWAIT"
                                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReader.Read() Then
                                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET id_stato='9',FL_INVITO='1' WHERE ID=" & LBLID.Text
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                        & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                        & "','F08','','I')"
                                    par.cmd.ExecuteNonQuery()

                                End If
                                myReader.Close()
                                par.OracleConn.Close()
                                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                                'par.OracleConn.Dispose()
                                scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('Invito Effettuato!');" _
                                & "</script>"
                                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                                End If
                                Label2.Text = "Nessuna selezione"
                                'BindGridEmergenze(Session.Item("ElencoEmerg"))
                                CercaEmergenze()
                                'Else
                                '    scriptblock = "<script language='javascript' type='text/javascript'>" _
                                '    & "alert('Attenzione, non è possibile invitare. Non risultano inseriti i redditi convenzionali. Si consiglia di restituire la domanda al comune e notificare il problema!');" _
                                '    & "</script>"
                                '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                                '    End If
                                'End If

                            End If
                        myReader123.Close()
                    Case Else
                        par.cmd.CommandText = "SELECT ID FROM DOMANDE_REDDITI WHERE ID_DOMANDA=" & LBLID.Text
                        Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader123.HasRows = True Then

                            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO WHERE ID=" & LBLID.Text & " FOR UPDATE NOWAIT"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET id_stato='9',FL_INVITO='1' WHERE ID=" & LBLID.Text
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F08','','I')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReader.Close()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            'par.OracleConn.Dispose()
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('Invito Effettuato!');" _
                            & "</script>"
                            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                            End If
                            Label2.Text = "Nessuna selezione"
                            BindGrid()
                        Else
                            If TIPOALLOGGIO.Value = "1" Then
                                par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO WHERE ID=" & LBLID.Text & " FOR UPDATE NOWAIT"
                                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReader.Read() Then
                                    par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET id_stato='9',FL_INVITO='1' WHERE ID=" & LBLID.Text
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                        & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                        & "','F08','','I')"
                                    par.cmd.ExecuteNonQuery()

                                End If
                                myReader.Close()
                                par.OracleConn.Close()
                                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                                'par.OracleConn.Dispose()
                                scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('Invito Effettuato!');" _
                                & "</script>"
                                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                                End If
                                Label2.Text = "Nessuna selezione"
                                BindGrid()
                            Else
                                scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "alert('Attenzione, non è possibile invitare. Non risultano inseriti i redditi convenzionali. Si consiglia di restituire la domanda al comune e notificare il problema!');" _
                                & "</script>"
                                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                                End If
                            End If

                        End If
                        myReader123.Close()
                End Select



                If sTipo <> "BANDO CAMBI" Then



                Else


                End If


            Catch EX1 As Oracle.DataAccess.Client.OracleException
                If EX1.Number = 54 Then
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('Pratica aperta da un altro utente. Non è possibile invitare in questo momento!');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                Else
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('" & EX1.ToString & "');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                End If
            Catch ex As Exception
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('" & ex.ToString & "');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                End If
            End Try
        End If
    End Sub

    Protected Sub btAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""SceltaTipoInvito.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizza0.Click
        If HiddenField1.Value = "1" Then
            If LBLID.Text = "-1" Or LBLID.Text = "" Then
                Response.Write("<script>alert('Nessuna Domanda selezionata!')</script>")
            Else
                Try
                    par.OracleConn.Open()
                    par.SettaCommand(par)


                    Select Case sTipo
                        Case "BANDO CAMBI"
                            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_CAMBI WHERE ID=" & LBLID.Text & " FOR UPDATE NOWAIT"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET id_stato='8',FL_INVITO='0',FL_ASS_ESTERNA='0',DATA_RIPRESA_ASS_EST='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.Text
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F80','','I')"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F158','','I')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReader.Close()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            'par.OracleConn.Dispose()
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('Domanda ricollocata in Graduatoria!');" _
                            & "</script>"
                            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                            End If
                            Label2.Text = "Nessuna selezione"
                            BindGridCambi()
                        Case "CAMBI EMERGENZA"
                            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO_VSA WHERE ID=" & LBLID.Text & " FOR UPDATE NOWAIT"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET id_stato='8',FL_INVITO='0',FL_ASS_ESTERNA='1',DATA_RIPRESA_ASS_EST='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.Text
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F80','','I')"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F158','','I')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReader.Close()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            'par.OracleConn.Dispose()
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('Domanda ricollocata in Graduatoria!');" _
                            & "</script>"
                            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                            End If
                            Label2.Text = "Nessuna selezione"
                            CercaEmergenze()
                        Case Else
                            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO WHERE ID=" & LBLID.Text & " FOR UPDATE NOWAIT"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET id_stato='8',FL_INVITO='0',FL_ASS_ESTERNA='0',DATA_RIPRESA_ASS_EST='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.Text
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F80','','I')"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F158','','I')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReader.Close()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            'par.OracleConn.Dispose()
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('Domanda ricollocata in graduatoria!');" _
                            & "</script>"
                            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                            End If
                            Label2.Text = "Nessuna selezione"
                            BindGrid()

                    End Select


                Catch EX1 As Oracle.DataAccess.Client.OracleException
                    If EX1.Number = 54 Then
                        par.OracleConn.Close()
                        par.OracleConn.Dispose()
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('Pratica aperta da un altro utente. Non è possibile invitare in questo momento!');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                        End If
                    Else
                        par.OracleConn.Close()
                        par.OracleConn.Dispose()
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "alert('" & EX1.ToString & "');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                        End If
                    End If
                Catch ex As Exception
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                    & "alert('" & ex.ToString & "');" _
                    & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                    End If
                End Try
            End If
        End If
    End Sub

    Protected Sub DataGrid2_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.EditCommand
        Response.Write("<script>window.open('Disponibilita.aspx?ID=" & e.Item.Cells(0).Text & "&PG=" & e.Item.Cells(1).Text & "&T=" & TipoBando.Value & "','Preferenze','height=745,top=0,left=0,width=797,scrollbars=no');</script>")
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
        HiddenField2.Value = LBLID.Text
        TIPOALLOGGIO.Value = e.Item.Cells(2).Text
    End Sub



    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub
End Class
