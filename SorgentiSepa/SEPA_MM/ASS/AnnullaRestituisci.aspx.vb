
Partial Class ASS_AnnullaRestituisci
    Inherits PageSetIdMode
    Dim par As New CM.Global()
    'Dim sTipo As String
    Dim sDA As String
    Dim sA As String
    Dim sPG As String
    Dim scriptblock As String

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
            HiddenField2.Value=""
            sTipo = Request.QueryString("TIPO")
            sDA = ""
            sA = ""
            sPG = Request.QueryString("PG")
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Text = "-1"

            Select Case sTipo
                Case "BANDO CAMBI"
                    CercaCambi()
                Case "CAMBI EMERGENZA"
                    CercaEmergenze()
                Case Else
                    Cerca()
            End Select




        End If
    End Sub

    Function CercaEmergenze()
        Dim Ms_Stringa As String

        Dim m As Boolean

        m = False

        Ms_Stringa = ""
        'If sDA <> "" Then
        '    Ms_Stringa = " AND BANDI_GRADUATORIA_DEF_cambi.POSIZIONE>=" & sDA
        '    m = True
        'End If

        'If sA <> "" Then
        '    If m = False Then
        '        Ms_Stringa = " AND BANDI_GRADUATORIA_DEF_cambi.POSIZIONE<=" & sA
        '        m = True
        '    Else
        '        Ms_Stringa = Ms_Stringa & " AND BANDI_GRADUATORIA_DEF_cambi.POSIZIONE<=" & sA
        '    End If
        'End If

        If sPG <> "" Then
            Ms_Stringa = " AND domande_bando_vsa.pg='" & par.PulisciStrSql(sPG) & "' "
            m = True
        End If

        sStringaSQL1 = "SELECT  (DOMANDE_BANDO_VSA_MOT_CAMBI.AI||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||dOMANDE_BANDO_VSA_MOT_CAMBI.RU||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.RI||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.AA||DOMANDE_BANDO_VSA_MOT_CAMBI.HANDICAP||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HM||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HA||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL=100 AND INDENNITA_ACC='1'),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HT||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL=100 AND INDENNITA_ACC='0'),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.HP||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND PERC_INVAL>=66 AND PERC_INVAL<100),'00000000')||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.AN||DOMANDE_BANDO_VSA_MOT_CAMBI.V17||NVL((SELECT MAX(COMP_NUCLEO_VSA.DATA_NASCITA) FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND (SYSDATE-TO_DATE(DATA_NASCITA,'YYYYmmdd'))/365>65),'00000000'))||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.FS||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA||DOMANDE_BANDO_VSA_MOT_CAMBI.PV||DOMANDE_BANDO_VSA_MOT_CAMBI.V16||DOMANDE_BANDO_VSA_MOT_CAMBI.DATA_RICHIESTA AS ORDINE," _
        & "DOMANDE_BANDO_VSA.ID,DOMANDE_BANDO_VSA.PG, COMP_NUCLEO_VSA.COGNOME, COMP_NUCLEO_VSA.NOME, " _
        & "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.PV,0,'NO',1,'SI') AS PV,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.FS,0,'NO',1,'SI') AS FS," _
        & "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AN,0,'NO',1,'SI') AS AN,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HP,0,'NO',1,'SI') AS HP," _
        & "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HT,0,'NO',1,'SI') AS HT,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HA,0,'NO',1,'SI') AS HA," _
        & "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.HM,0,'NO',1,'SI') AS HM,DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AA,0,'NO',1,'SI') AS AA," _
        & "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.AI,0,'NO',1,'SI') AS AI, DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.RU,0,'NO',1,'SI') AS RU," _
        & "DECODE(DOMANDE_BANDO_VSA_MOT_CAMBI.RI,0,'NO',1,'SI') AS RI " _
        & "FROM COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,DOMANDE_BANDO_VSA_MOT_CAMBI " _
        & "WHERE (DOMANDE_BANDO_vsa.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) " _
        & " OR DOMANDE_BANDO_vsa.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1)) and " _
        & " (DOMANDE_BANDO_vsa.FL_PROPOSTA='0' OR DOMANDE_BANDO_vsa.FL_PROPOSTA IS NULL) and COMP_NUCLEO_VSA.PROGR=0 AND " _
        & " COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA_MOT_CAMBI.ID_DOMANDA=DOMANDE_BANDO_VSA.ID " _
        & "AND DOMANDE_BANDO_VSA.ID_STATO='9' AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=4 AND (DOMANDE_BANDO_VSA.FL_INVITO='1') " & Ms_Stringa & " ORDER BY ORDINE DESC"



        'Ms_Stringa1 = "(BANDI_GRADUATORIA_DEF_cambi.Tipo = 0 or BANDI_GRADUATORIA_DEF_cambi.Tipo = 1) "
        'sStringaSQL1 = "SELECT BANDI_GRADUATORIA_DEF_cambi.tipo,BANDI_GRADUATORIA_DEF_cambi.POSIZIONE,BANDI_GRADUATORIA_DEF_cambi.ID as ""d11"",BANDI_GRADUATORIA_DEF_cambi.ID_DOMANDA," _
        '           & "DICHIARAZIONI_cambi.N_COMP_NUCLEO,COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME,DOMANDE_BANDO_cambi.PG,DECODE(BANDI_GRADUATORIA_DEF_cambi.Tipo,1,'Art.14') AS ""Art""," _
        '           & "DOMANDE_BANDO_cambi.id,DOMANDE_BANDO_cambi.FL_INVITO,trunc(DOMANDE_BANDO_cambi.reddito_isee,2) as ""reddito_isee"",trunc(DOMANDE_BANDO_cambi.isbarc_r,4) as ""isbarc_r"",DOMANDE_BANDO_cambi.FL_CONTROLLA_REQUISITI,T_TIPO_PRATICHE.DESCRIZIONE AS ""D1"" " _
        '           & "FROM BANDI_GRADUATORIA_DEF_cambi,DOMANDE_BANDO_cambi,T_TIPO_PRATICHE,COMP_NUCLEO_cambi,DICHIARAZIONI_cambi " _
        '           & " WHERE  (DOMANDE_BANDO_cambi.FL_INVITO='' or DOMANDE_BANDO_cambi.FL_INVITO='0' or DOMANDE_BANDO_cambi.FL_INVITO is null) and domande_bando_cambi.fl_controlla_requisiti='2' " & Ms_Stringa _
        '           & "  and domande_bando_cambi.FL_ASS_ESTERNA='1' and DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND BANDI_GRADUATORIA_DEF_cambi.ID_DOMANDA=DOMANDE_BANDO_cambi.ID (+) AND " & Ms_Stringa1 _
        '           & " and DOMANDE_BANDO_cambi.PROGR_COMPONENTE=COMP_NUCLEO_cambi.PROGR AND COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DOMANDE_BANDO_cambi.ID_DICHIARAZIONE AND " _
        '           & "  DOMANDE_BANDO_cambi.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
        '           & " (DOMANDE_BANDO_cambi.ID_STATO='9') AND (DOMANDE_BANDO_cambi.FL_PROPOSTA='0' OR DOMANDE_BANDO_cambi.FL_PROPOSTA IS NULL) " _
        '           & " AND (DOMANDE_BANDO_cambi.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO_cambi.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
        '           & " ORDER BY domande_bando_cambi.isbarc_r desc"


        BindGridEmergenze()
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



        Ms_Stringa1 = "(BANDI_GRADUATORIA_DEF_cambi.Tipo = 0 or BANDI_GRADUATORIA_DEF_cambi.Tipo = 1) "
        sStringaSQL1 = "SELECT BANDI_GRADUATORIA_DEF_cambi.tipo,BANDI_GRADUATORIA_DEF_cambi.POSIZIONE,BANDI_GRADUATORIA_DEF_cambi.ID as ""d11"",BANDI_GRADUATORIA_DEF_cambi.ID_DOMANDA," _
                   & "DICHIARAZIONI_cambi.N_COMP_NUCLEO,COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME,DOMANDE_BANDO_cambi.PG,DECODE(BANDI_GRADUATORIA_DEF_cambi.Tipo,1,'Art.14') AS ""Art""," _
                   & "DOMANDE_BANDO_cambi.id,DOMANDE_BANDO_cambi.FL_INVITO,trunc(DOMANDE_BANDO_cambi.reddito_isee,2) as ""reddito_isee"",trunc(DOMANDE_BANDO_cambi.isbarc_r,4) as ""isbarc_r"",DOMANDE_BANDO_cambi.FL_CONTROLLA_REQUISITI,T_TIPO_PRATICHE.DESCRIZIONE AS ""D1"" " _
                   & "FROM BANDI_GRADUATORIA_DEF_cambi,DOMANDE_BANDO_cambi,T_TIPO_PRATICHE,COMP_NUCLEO_cambi,DICHIARAZIONI_cambi " _
                   & " WHERE  (DOMANDE_BANDO_cambi.FL_INVITO='1') and domande_bando_cambi.fl_controlla_requisiti='2' " & Ms_Stringa _
                   & "  and domande_bando_cambi.FL_ASS_ESTERNA='1' and DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND BANDI_GRADUATORIA_DEF_cambi.ID_DOMANDA=DOMANDE_BANDO_cambi.ID (+) AND " & Ms_Stringa1 _
                   & " and DOMANDE_BANDO_cambi.PROGR_COMPONENTE=COMP_NUCLEO_cambi.PROGR AND COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DOMANDE_BANDO_cambi.ID_DICHIARAZIONE AND " _
                   & "  DOMANDE_BANDO_cambi.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
                   & " (DOMANDE_BANDO_cambi.ID_STATO='9') AND (DOMANDE_BANDO_cambi.FL_PROPOSTA='0' OR DOMANDE_BANDO_cambi.FL_PROPOSTA IS NULL) " _
                   & " AND (DOMANDE_BANDO_cambi.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO_cambi.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
                   & " ORDER BY domande_bando_cambi.isbarc_r desc"


        BindGridCambi()
    End Function


    Sub Cerca()
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

        'If Ms_Stringa <> "" Then
        '    Ms_Stringa = Ms_Stringa & " AND "
        'End If
        ' and domande_bando.fl_controlla_requisiti='2'

        If sTipo = "IDONEE" Then
            Ms_Stringa1 = "(BANDI_GRADUATORIA_DEF.Tipo = 0 or BANDI_GRADUATORIA_DEF.Tipo = 1) "
            sStringaSQL1 = "SELECT BANDI_GRADUATORIA_DEF.tipo,BANDI_GRADUATORIA_DEF.POSIZIONE,BANDI_GRADUATORIA_DEF.ID as ""d11"",BANDI_GRADUATORIA_DEF.ID_DOMANDA," _
                       & "DICHIARAZIONI.N_COMP_NUCLEO,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,DOMANDE_BANDO.PG,DECODE(BANDI_GRADUATORIA_DEF.Tipo,1,'Art.14') AS ""Art""," _
                       & "DOMANDE_BANDO.id,DOMANDE_BANDO.FL_INVITO,trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",DOMANDE_BANDO.FL_CONTROLLA_REQUISITI,T_TIPO_PRATICHE.DESCRIZIONE AS ""D1"" " _
                       & "FROM BANDI_GRADUATORIA_DEF,DOMANDE_BANDO,T_TIPO_PRATICHE,COMP_NUCLEO,DICHIARAZIONI " _
                       & " WHERE  (DOMANDE_BANDO.FL_INVITO='1') and domande_bando.fl_controlla_requisiti='2' " & Ms_Stringa _
                       & "  and domande_bando.FL_ASS_ESTERNA='1' and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND BANDI_GRADUATORIA_DEF.ID_DOMANDA=DOMANDE_BANDO.ID (+) AND " & Ms_Stringa1 _
                       & " and DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND " _
                       & "  DOMANDE_BANDO.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
                       & " (DOMANDE_BANDO.ID_STATO='9') AND (DOMANDE_BANDO.FL_PROPOSTA='0' OR DOMANDE_BANDO.FL_PROPOSTA IS NULL) " _
                       & " AND (DOMANDE_BANDO.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
                       & " ORDER BY domande_bando.isbarc_r desc"
        End If
        If sTipo = "Art14" Then
            Ms_Stringa1 = "BANDI_GRADUATORIA_DEF.Tipo = 1 "

            sStringaSQL1 = "SELECT BANDI_GRADUATORIA_DEF.tipo,BANDI_GRADUATORIA_DEF.POSIZIONE,BANDI_GRADUATORIA_DEF.ID as ""d11"",BANDI_GRADUATORIA_DEF.ID_DOMANDA," _
                       & "DICHIARAZIONI.N_COMP_NUCLEO,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,DOMANDE_BANDO.PG,DECODE(BANDI_GRADUATORIA_DEF.Tipo,1,'Art.14') AS ""Art""," _
                       & "DOMANDE_BANDO.id,DOMANDE_BANDO.FL_INVITO,trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",DOMANDE_BANDO.FL_CONTROLLA_REQUISITI,T_TIPO_PRATICHE.DESCRIZIONE AS ""D1"" " _
                       & "FROM BANDI_GRADUATORIA_DEF,DOMANDE_BANDO,T_TIPO_PRATICHE,COMP_NUCLEO,DICHIARAZIONI " _
                       & " WHERE  (DOMANDE_BANDO.FL_INVITO='1') and domande_bando.fl_controlla_requisiti='2' " & Ms_Stringa _
                       & "  and domande_bando.FL_ASS_ESTERNA='0' and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND BANDI_GRADUATORIA_DEF.ID_DOMANDA=DOMANDE_BANDO.ID (+) AND " & Ms_Stringa1 _
                       & " and DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND " _
                       & "  DOMANDE_BANDO.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
                       & " (DOMANDE_BANDO.ID_STATO='9') AND (DOMANDE_BANDO.FL_PROPOSTA=0 OR DOMANDE_BANDO.FL_PROPOSTA IS NULL) " _
                       & " AND (DOMANDE_BANDO.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
                       & " ORDER BY domande_bando.isbarc_r desc"
        End If
        If sTipo = "Art15" Then
            Ms_Stringa1 = "BANDI_GRADUATORIA_DEF.Tipo = 2"

            sStringaSQL1 = "SELECT BANDI_GRADUATORIA_DEF.tipo,BANDI_GRADUATORIA_DEF.POSIZIONE,BANDI_GRADUATORIA_DEF.ID as ""d11"",BANDI_GRADUATORIA_DEF.ID_DOMANDA," _
                       & "DICHIARAZIONI.N_COMP_NUCLEO,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,DOMANDE_BANDO.PG,DECODE(BANDI_GRADUATORIA_DEF.Tipo,2,'Art.15') AS ""Art""," _
                       & "DOMANDE_BANDO.id,DOMANDE_BANDO.FL_INVITO,trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",DOMANDE_BANDO.FL_CONTROLLA_REQUISITI,T_TIPO_PRATICHE.DESCRIZIONE AS ""D1"" " _
                       & "FROM DICHIARAZIONI,BANDI_GRADUATORIA_DEF,DOMANDE_BANDO,T_TIPO_PRATICHE,COMP_NUCLEO " _
                       & " WHERE  (DOMANDE_BANDO.FL_INVITO='1' or DOMANDE_BANDO.FL_INVITO='0' or DOMANDE_BANDO.FL_INVITO is null) and domande_bando.fl_controlla_requisiti='2' " & Ms_Stringa _
                       & " and (domande_bando.FL_ASS_ESTERNA='0' OR domande_bando.FL_ASS_ESTERNA='1') and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND BANDI_GRADUATORIA_DEF.ID_DOMANDA=DOMANDE_BANDO.ID (+) AND " & Ms_Stringa1 _
                       & " and DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND " _
                       & "  DOMANDE_BANDO.TIPO_PRATICA=T_TIPO_PRATICHE.COD (+) AND " _
                       & " (DOMANDE_BANDO.FL_PROPOSTA=0 OR DOMANDE_BANDO.FL_PROPOSTA IS NULL) " _
                       & " AND (DOMANDE_BANDO.ID NOT IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO<>3 and ESITO<>4 AND ULTIMO=1) OR DOMANDE_BANDO.ID IN (SELECT DISTINCT ID_PRATICA FROM REL_PRAT_ALL_CCAA_ERP WHERE ESITO=0 AND ULTIMO=1))" _
                       & " ORDER BY domande_bando.isbarc_r desc"
        End If
        BindGrid()
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


    Private Sub BindGridEmergenze()

        DataGrid1.visible = False
        DataGrid2.visible = True


        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa")
        DataGrid2.DataSource = ds
        DataGrid2.DataBind()
        Label6.Text = "  - " & DataGrid2.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
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
        Response.Write("<script>window.open('Disponibilita.aspx?ID=" & e.Item.Cells(0).Text & "&PG=" & e.Item.Cells(1).Text & "','Preferenze');</script>")
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

    Protected Sub DataGrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        LBLID.Text = e.Item.Cells(0).Text
        HiddenField2.Value = e.Item.Cells(0).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text

    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizza.Click
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
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET id_stato='9',FL_INVITO='0' WHERE ID=" & LBLID.Text
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F103','','I')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReader.Close()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            'par.OracleConn.Dispose()
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('ANNULLO Invito Effettuato!');" _
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
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET id_stato='9',FL_INVITO='0' WHERE ID=" & LBLID.Text
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F103','','I')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReader.Close()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            'par.OracleConn.Dispose()
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('ANNULLO Invito Effettuato!');" _
                            & "</script>"
                            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                            End If
                            Label2.Text = "Nessuna selezione"
                            BindGridEmergenze()
                        Case Else
                            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO WHERE ID=" & LBLID.Text & " FOR UPDATE NOWAIT"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET id_stato='9',FL_INVITO='0' WHERE ID=" & LBLID.Text
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F103','','I')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReader.Close()
                            par.OracleConn.Close()
                            'par.OracleConn.Dispose()
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('ANNULLO Invito Effettuato!');" _
                            & "</script>"
                            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                            End If
                            Label2.Text = "Nessuna selezione"
                            BindGrid()
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
        End If
    End Sub

    Protected Sub btAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaAnnulli.aspx""</script>")

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
                                                    & "','F79','','I')"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_CAMBI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F158','','I')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReader.Close()
                            par.OracleConn.Close()
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
                                                    & "','F79','','I')"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_VSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F158','','I')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReader.Close()
                            par.OracleConn.Close()
                            'par.OracleConn.Dispose()
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('Domanda ricollocata in Graduatoria!');" _
                            & "</script>"
                            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                            End If
                            Label2.Text = "Nessuna selezione"
                            BindGridEmergenze()
                        Case Else
                            par.cmd.CommandText = "SELECT ID FROM DOMANDE_BANDO WHERE ID=" & LBLID.Text & " FOR UPDATE NOWAIT"
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.Read() Then
                                par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET id_stato='8',FL_INVITO='0',FL_ASS_ESTERNA='0',DATA_RIPRESA_ASS_EST='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & LBLID.Text
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F79','','I')"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','8" _
                                                    & "','F158','','I')"
                                par.cmd.ExecuteNonQuery()

                            End If
                            myReader.Close()
                            par.OracleConn.Close()
                            'par.OracleConn.Dispose()
                            scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "alert('Domanda ricollocata in Graduatoria!');" _
                            & "</script>"
                            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5")) Then
                                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5", scriptblock)
                            End If
                            Label2.Text = "Nessuna selezione"
                            BindGrid()
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
        End If
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
            BindGridEmergenze()
        End If
    End Sub

    Protected Sub DataGrid2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid2.SelectedIndexChanged

    End Sub

    Protected Sub DataGrid2_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.UpdateCommand
        LBLID.Text = e.Item.Cells(0).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text
        HiddenField2.Value = LBLID.Text
    End Sub
End Class
