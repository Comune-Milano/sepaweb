
Partial Class ASS_SelezionaUnitafuoriMI
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            cod_comune_ass.Value = Request.QueryString("C")
            idDich.Value = Request.QueryString("ID")
            If ControllaSeAssegn() = True Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Assegnazione esistente!');self.close();", True)
                Exit Sub
            End If
            BindGrid()
        End If
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Key", "<script>MakeStaticHeader('" + dgvAlloggi.ClientID + "', 380, 776 , 25 ,false); </script>", False)
    End Sub

    Private Function ControllaSeAssegn() As Boolean
        Dim giaAssegn As Boolean = False
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * from siscom_mi.unita_assegnate where id_dichiarazione=" & idDich.Value
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                giaAssegn = True
            End If
            myReader1.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return giaAssegn
    End Function

    Private Sub BindGrid()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sStringaSQL1A As String = ""
            Dim sStringaSQL1B As String = ""
            Dim sStringaSQL2A As String = ""
            Dim sStringaSQL2B As String = ""

            Dim strWhereSuperf1 As String = ""
            Dim strWhereSuperf2 As String = ""

            Dim numComponenti As Integer = 0
            par.cmd.CommandText = "SELECT count(comp_nucleo_vsa.id) as numComp from comp_nucleo_vsa,dichiarazioni_vsa where comp_nucleo_vsa.id_dichiarazione=dichiarazioni_vsa.id and dichiarazioni_vsa.id=" & idDich.Value
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read() Then
                numComponenti = par.IfNull(myReader1("numComp"), 0)
            End If
            myReader1.Close()

            Dim nomeComuneRif As String = ""
            par.cmd.CommandText = "SELECT nome from comuni_nazioni where cod='" & cod_comune_ass.Value & "'"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read() Then
                nomeComuneRif = par.IfNull(myReader1("nome"), "")
            End If
            myReader1.Close()

            Select Case numComponenti
                Case 1
                    strWhereSuperf1 = " and  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)>28.80 " _
                        & "and  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=38"

                    strWhereSuperf2 = " and ((SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=28.80 " _
                        & " OR  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)>38)"
                Case 2
                    strWhereSuperf1 = " and  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)>33.60 " _
                        & "and  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=46"

                    strWhereSuperf2 = " and ((SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=33.60 " _
                        & " OR  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)>46)"
                Case 3
                    strWhereSuperf1 = " and  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)>43.35 " _
                        & "and  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=60.34"

                    strWhereSuperf2 = " and ((SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=43.35 " _
                        & " OR  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)>60.34)"
                Case 4
                    strWhereSuperf1 = " and  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)>60.35 " _
                        & "and  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=71.39"

                    strWhereSuperf2 = " and ((SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=60.35 " _
                        & " OR  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)>71.39)"
                Case 5
                    strWhereSuperf1 = " and  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)>71.40 " _
                        & "and  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=79.04"

                    strWhereSuperf2 = " and ((SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=71.40 " _
                        & " OR  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)>79.04)"
                Case Is >= 5
                    strWhereSuperf1 = " and  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)>79.05 " _
                        & "and  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=200"

                    strWhereSuperf2 = " and ((SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)<=79.05 " _
                        & " OR  (SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA = 'SUP_NETTA'  AND ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID)>200)"
            End Select

            Dim strUnion1 As String = ""
            Dim numAlloggiComune As Integer = 0

            sStringaSQL1A = "SELECT 0 as ordinam,unita_immobiliari.id as id_ui, (select nome from comuni_nazioni where cod_comune=cod) as comuneAss,(select cod from comuni_nazioni where cod_comune=cod) as COD_COMUNE_ASS,(SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS NETTA,(SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_CONV' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS CONV," _
                & " (CASE WHEN (SELECT DISTINCT UNITA_IMMOBILIARI.ID  FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.UNITA_IMMOBILIARI UI WHERE UI.ID=unita_immobiliari.ID AND IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI_SCALE.ID_SCALA=UI.ID_SCALA AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)>0 THEN 'SI' END) AS elevatore," _
                & "tipologia_unita_immobiliari.descrizione as tipo,T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE as TIPO_ALL," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE ||' '|| ALLOGGI.indirizzo AS ""VIA"",T_TIPO_INDIRIZZO.DESCRIZIONE ||''|| ALLOGGI.indirizzo,ALLOGGI.ZONA,(CASE WHEN ALLOGGI.H_MOTORIO = 1 THEN 'SI' else 'NO' END) as ""HANDICAP"", " _
                       & "('<a href=""javascript:window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='||UNITA_IMMOBILIARI.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');void(0);"">'||'<img alt=" & Chr(34) & "Dettagli Unità, Foto e Planimetrie" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Abbina_Foto.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " />'||'</a>') as Visualizza," _
                       & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI,siscom_mi.tipologia_unita_immobiliari WHERE tipologia_unita_immobiliari.cod=cod_tipologia and " _
                       & " unita_immobiliari.ID IN (SELECT id_unita FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO WHERE UNITA_STATO_MANUTENTIVO.RIASSEGNABILE=1 and (UNITA_STATO_MANUTENTIVO.TIPO_RIASSEGNABILE = '1' OR (UNITA_STATO_MANUTENTIVO.TIPO_RIASSEGNABILE = '0' AND UNITA_STATO_MANUTENTIVO.FINE_LAVORI = 1))) AND " _
                       & "ALLOGGI.COD_ALLOGGIO=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE (+) AND FL_POR='0' AND EQCANONE='0' AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                       & " (unita_immobiliari.id_destinazione_uso=1 or unita_immobiliari.id_destinazione_uso=2) AND ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                       & " ASSEGNATO='0' AND PRENOTATO='0' AND alloggi.STATO=5 AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = 'LIBE' AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND edifici.id = unita_immobiliari.id_Edificio AND fl_piano_vendita = 0 " _
                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND COD_COMUNE='" & cod_comune_ass.Value & "' " & strWhereSuperf2

            sStringaSQL1B = "SELECT 1 as ordinam,unita_immobiliari.id as id_ui, (select nome from comuni_nazioni where cod_comune=cod) as comuneAss,(select cod from comuni_nazioni where cod_comune=cod) as COD_COMUNE_ASS,(SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS NETTA,(SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_CONV' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS CONV," _
                    & " (CASE WHEN (SELECT DISTINCT UNITA_IMMOBILIARI.ID  FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.UNITA_IMMOBILIARI UI WHERE UI.ID=unita_immobiliari.ID AND IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI_SCALE.ID_SCALA=UI.ID_SCALA AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)>0 THEN 'SI' END) AS elevatore," _
                    & "tipologia_unita_immobiliari.descrizione as tipo,T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE as TIPO_ALL," _
                    & "T_TIPO_INDIRIZZO.DESCRIZIONE ||' '|| ALLOGGI.indirizzo AS ""VIA"",T_TIPO_INDIRIZZO.DESCRIZIONE ||''|| ALLOGGI.indirizzo,ALLOGGI.ZONA,(CASE WHEN ALLOGGI.H_MOTORIO = 1 THEN 'SI' else 'NO' END) as ""HANDICAP"", " _
                    & "('<a href=""javascript:window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='||UNITA_IMMOBILIARI.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');void(0);"">'||'<img alt=" & Chr(34) & "Dettagli Unità, Foto e Planimetrie" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Abbina_Foto.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " />'||'</a>') as Visualizza," _
                    & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI," _
                    & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI,siscom_mi.tipologia_unita_immobiliari WHERE tipologia_unita_immobiliari.cod=cod_tipologia and " _
                    & " unita_immobiliari.ID IN (SELECT id_unita FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO WHERE UNITA_STATO_MANUTENTIVO.RIASSEGNABILE=1 and (UNITA_STATO_MANUTENTIVO.TIPO_RIASSEGNABILE = '1' OR (UNITA_STATO_MANUTENTIVO.TIPO_RIASSEGNABILE = '0' AND UNITA_STATO_MANUTENTIVO.FINE_LAVORI = 1))) AND " _
                    & "ALLOGGI.COD_ALLOGGIO=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE (+) AND FL_POR='0' AND EQCANONE='0' AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                    & " (unita_immobiliari.id_destinazione_uso=1 or unita_immobiliari.id_destinazione_uso=2) AND ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                    & " ASSEGNATO='0' AND PRENOTATO='0' AND alloggi.STATO=5 AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = 'LIBE' AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND edifici.id = unita_immobiliari.id_Edificio AND fl_piano_vendita = 0 " _
                    & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND COD_COMUNE='" & cod_comune_ass.Value & "' " & strWhereSuperf1
            strUnion1 = sStringaSQL1A & " UNION ALL " & sStringaSQL1B & " ORDER BY comuneAss asc,ordinam desc,netta asc"


            Dim strUnion2 As String = ""

            sStringaSQL2A = "SELECT 0 as ordinam,unita_immobiliari.id as id_ui,(select nome from comuni_nazioni where cod_comune=cod) as comuneAss,(select cod from comuni_nazioni where cod_comune=cod) as COD_COMUNE_ASS,(SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS NETTA,(SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_CONV' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS CONV," _
                & " (CASE WHEN (SELECT DISTINCT UNITA_IMMOBILIARI.ID  FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.UNITA_IMMOBILIARI UI WHERE UI.ID=unita_immobiliari.ID AND IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI_SCALE.ID_SCALA=UI.ID_SCALA AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)>0 THEN 'SI' END) AS elevatore," _
                & "tipologia_unita_immobiliari.descrizione as tipo,T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE as TIPO_ALL," _
                & "T_TIPO_INDIRIZZO.DESCRIZIONE ||' '|| ALLOGGI.indirizzo AS ""VIA"",T_TIPO_INDIRIZZO.DESCRIZIONE ||''|| ALLOGGI.indirizzo,ALLOGGI.ZONA,(CASE WHEN ALLOGGI.H_MOTORIO = 1 THEN 'SI' else 'NO' END) as ""HANDICAP"", " _
                & "('<a href=""javascript:window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='||UNITA_IMMOBILIARI.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');void(0);"">'||'<img alt=" & Chr(34) & "Dettagli Unità, Foto e Planimetrie" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Abbina_Foto.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " />'||'</a>') as Visualizza," _
                & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI," _
                & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI,siscom_mi.tipologia_unita_immobiliari WHERE tipologia_unita_immobiliari.cod=cod_tipologia and " _
                & " unita_immobiliari.ID IN (SELECT id_unita FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO WHERE UNITA_STATO_MANUTENTIVO.RIASSEGNABILE=1 and (UNITA_STATO_MANUTENTIVO.TIPO_RIASSEGNABILE = '1' OR (UNITA_STATO_MANUTENTIVO.TIPO_RIASSEGNABILE = '0' AND UNITA_STATO_MANUTENTIVO.FINE_LAVORI = 1))) AND " _
                & "ALLOGGI.COD_ALLOGGIO=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE (+) AND FL_POR='0' AND EQCANONE='0' AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                & " (unita_immobiliari.id_destinazione_uso=1 or unita_immobiliari.id_destinazione_uso=2) AND ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                & " ASSEGNATO='0' AND PRENOTATO='0' AND alloggi.STATO=5 AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = 'LIBE' AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND edifici.id = unita_immobiliari.id_Edificio AND fl_piano_vendita = 0 " _
                & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND COD_COMUNE<>'" & cod_comune_ass.Value & "' " & strWhereSuperf2

            sStringaSQL2B = "SELECT 1 as ordinam,unita_immobiliari.id as id_ui,(select nome from comuni_nazioni where cod_comune=cod) as comuneAss,(select cod from comuni_nazioni where cod_comune=cod) as COD_COMUNE_ASS,(SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS NETTA,(SELECT DISTINCT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_CONV' AND ID_UNITA_IMMOBILIARE=UNITA_IMMOBILIARI.ID) AS CONV," _
                & " (CASE WHEN (SELECT DISTINCT UNITA_IMMOBILIARI.ID  FROM SISCOM_MI.IMPIANTI,SISCOM_MI.IMPIANTI_SCALE,SISCOM_MI.UNITA_IMMOBILIARI UI WHERE UI.ID=unita_immobiliari.ID AND IMPIANTI.COD_TIPOLOGIA='SO' AND IMPIANTI_SCALE.ID_SCALA=UI.ID_SCALA AND IMPIANTI.ID=IMPIANTI_SCALE.ID_IMPIANTO)>0 THEN 'SI' END) AS elevatore," _
                & "tipologia_unita_immobiliari.descrizione as tipo,T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE as TIPO_ALL," _
                & "T_TIPO_INDIRIZZO.DESCRIZIONE ||' '|| ALLOGGI.indirizzo AS ""VIA"",T_TIPO_INDIRIZZO.DESCRIZIONE ||''|| ALLOGGI.indirizzo,ALLOGGI.ZONA,(CASE WHEN ALLOGGI.H_MOTORIO = 1 THEN 'SI' else 'NO' END) as ""HANDICAP"", " _
                & "('<a href=""javascript:window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='||UNITA_IMMOBILIARI.ID||''',''Dettagli'',''height=580,top=0,left=0,width=780'');void(0);"">'||'<img alt=" & Chr(34) & "Dettagli Unità, Foto e Planimetrie" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Abbina_Foto.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " />'||'</a>') as Visualizza," _
                & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI," _
                & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI,siscom_mi.tipologia_unita_immobiliari WHERE tipologia_unita_immobiliari.cod=cod_tipologia and " _
                & " unita_immobiliari.ID IN (SELECT id_unita FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO WHERE UNITA_STATO_MANUTENTIVO.RIASSEGNABILE=1 and (UNITA_STATO_MANUTENTIVO.TIPO_RIASSEGNABILE = '1' OR (UNITA_STATO_MANUTENTIVO.TIPO_RIASSEGNABILE = '0' AND UNITA_STATO_MANUTENTIVO.FINE_LAVORI = 1))) AND " _
                & "ALLOGGI.COD_ALLOGGIO=UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE (+) AND FL_POR='0' AND EQCANONE='0' AND TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                & " (unita_immobiliari.id_destinazione_uso=1 or unita_immobiliari.id_destinazione_uso=2) AND ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND " _
                & " ASSEGNATO='0' AND PRENOTATO='0' AND alloggi.STATO=5 AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = 'LIBE' AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND edifici.id = unita_immobiliari.id_Edificio AND fl_piano_vendita = 0 " _
                & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND COD_COMUNE<>'" & cod_comune_ass.Value & "' " & strWhereSuperf1

            strUnion2 = sStringaSQL2A & " UNION ALL " & sStringaSQL2B & " ORDER BY comuneAss asc,ordinam desc,netta asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strUnion1, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            numAlloggiComune = dt.Rows.Count
            
            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(strUnion2, par.OracleConn)
            Dim dt2 As New Data.DataTable
            da2.Fill(dt)
            da2.Dispose()

            dgvAlloggi.DataSource = dt
            dgvAlloggi.DataBind()

            For Each di As DataGridItem In dgvAlloggi.Items
                If par.IfNull(di.Cells(par.IndDGC(dgvAlloggi, "ORDINAM")).Text, 0) = "1" Then
                    di.BackColor = Drawing.ColorTranslator.FromHtml("#CEFFCE")
                End If
                If par.IfNull(di.Cells(par.IndDGC(dgvAlloggi, "COD_COMUNE_ASS")).Text, 0) = cod_comune_ass.Value Then
                    di.Font.Bold = True
                End If
            Next

            lblNumTot.Text = dt.Rows.Count
            lblNumFuoriMi.Text = "- Tot. " & numAlloggiComune & " del comune di " & nomeComuneRif

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub dgvAlloggi_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvAlloggi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='yellow';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='red';" _
                                & "document.getElementById('txtmia').value='Hai selezionato l\'unità: " & e.Item.Cells(1).Text.Replace("'", "\'") & " di " & e.Item.Cells(7).Text.Replace("'", "\'") & " " & e.Item.Cells(8).Text & "';" _
                                & "document.getElementById('idSelected').value='" & e.Item.Cells(0).Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnProcedi').click();")
        End If
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            If idSelected.Value <> "0" Then
                'Response.Write("<script>function ApriFormalizzazioneNewRU() {var win = null;self.close();LeftPosition = (screen.width) ? (screen.width - 795) / 2 : 0;TopPosition = (screen.height) ? (screen.height - 600) / 2 : 0;LeftPosition = LeftPosition - 15;TopPosition = TopPosition - 15;window.open('FormalizzazioneNewRU.aspx?IDU=" & idSelected.Value & "&DICH=" & idDich.Value & "', 'Provvedimento', 'width=795,height=600,scrollbars=no,toolbar=no,resizable=no,top=' + TopPosition + ',left=' + LeftPosition);};ApriFormalizzazioneNewRU();</script>")
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "function ApriFormalizzazioneNewRU() {var win = null;self.close();LeftPosition = (screen.width) ? (screen.width - 795) / 2 : 0;TopPosition = (screen.height) ? (screen.height - 600) / 2 : 0;LeftPosition = LeftPosition - 15;TopPosition = TopPosition - 15;window.focus();window.open('FormalizzazioneNewRU.aspx?IDU=" & idSelected.Value & "&DICH=" & idDich.Value & "', 'Provvedimento', 'width=795,height=600,scrollbars=no,toolbar=no,resizable=no,top=' + TopPosition + ',left=' + LeftPosition);};ApriFormalizzazioneNewRU();", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Nessuna unità selezionata!');", True)
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
