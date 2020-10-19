Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class Contratti_RisultatoContratti_VSA
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String
    Dim scriptblock As String

    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreCF As String
    Dim sValorePIVA As String
    Dim sValoreCO As String
    Dim sValoreUN As String
    Dim sValoreST As String
    Dim sValoreDUR As String
    Dim sValoreRINN As String
    Dim sValoreTC As String
    Dim sValorePROV As String
    Dim sValoreTI As String
    Dim sValoreFIL As String
    Dim sValoreTDOM As String
    Dim sValoreCAUS As String
    Dim sValoreSTDOM As String
    Dim sValorePG As String
    Dim sValoreEDAL As String
    Dim sValoreEAL As String
    Dim sValorePDAL As String
    Dim sValorePAL As String
    Dim sValoreIDAL As String
    Dim sValoreIAL As String
    Dim sValoreFDAL As String
    Dim sValoreFAL As String
    Dim sValoreAUTDAL As String
    Dim sValoreAUTAL As String
    Dim sValoreDPGDAL As String
    Dim sValoreDPGAL As String
    Dim sValoreInval As String
    Dim sValoreOperat As String
    Dim sValoreStatoSosp As String
    Dim sValoreQN As String

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


        If Not IsPostBack Then

            sValoreCG = UCase(Request.QueryString("CG"))
            sValoreNM = UCase(Request.QueryString("NM"))
            sValoreCF = UCase(Request.QueryString("CF"))
            sValorePIVA = UCase(Request.QueryString("PIVA"))
            sValoreCO = UCase(Request.QueryString("CO"))
            sValoreUN = UCase(Request.QueryString("UN"))
            sValoreST = UCase(Request.QueryString("ST"))
            sValoreDUR = Request.QueryString("DUR")
            sValoreRINN = Request.QueryString("RINN")
            sValoreTC = UCase(Request.QueryString("TC"))
            sValorePROV = Request.QueryString("PROV")
            sValoreTI = Request.QueryString("TI")
            sValoreFIL = Request.QueryString("FIL")
            sValoreTDOM = Request.QueryString("TDOM")
            sValoreCAUS = Request.QueryString("CAUS")
            sValoreSTDOM = Request.QueryString("STDOM")
            sValorePG = Request.QueryString("PG")
            sValoreEDAL = Request.QueryString("EDAL")
            sValoreEAL = Request.QueryString("EAL")
            sValorePDAL = Request.QueryString("PDAL")
            sValorePAL = Request.QueryString("PAL")
            sValoreIDAL = Request.QueryString("IDAL")
            sValoreIAL = Request.QueryString("IAL")
            sValoreFDAL = Request.QueryString("FDAL")
            sValoreFAL = Request.QueryString("FAL")
            sValoreAUTDAL = Request.QueryString("AUTDAL")
            sValoreAUTAL = Request.QueryString("AUTAL")
            sValoreDPGDAL = Request.QueryString("DPGDAL")
            sValoreDPGAL = Request.QueryString("DPGAL")
            sValoreInval = Request.QueryString("INVAL")
            sValoreOperat = Request.QueryString("OP")
            sValoreStatoSosp = Request.QueryString("STSOSP")
            '///////////////////////
            '// 1450/2019
            sValoreQN = Request.QueryString("QN")

            '//////////////////////
            Response.Flush()
            LBLID.Value = "-1"
            Cerca()



        End If
    End Sub


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


    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String

        bTrovato = False
        sStringaSql = ""


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
            sStringaSql = sStringaSql & " ANAGRAFICA.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " ANAGRAFICA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
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
            sStringaSql = sStringaSql & " ANAGRAFICA.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValorePIVA <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePIVA
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " ANAGRAFICA.PARTITA_IVA " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreUN <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreUN
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UNITA_CONTRATTUALE.COD_UNITA_IMMOBILIARE " & sCompara & " '" & par.PulisciStrSql(sValore) & "'"
        End If

        If sValoreST <> "TUTTI" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreST
            bTrovato = True
            sStringaSql = sStringaSql & " SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreTC = "ERP" Then
            If sValorePROV <> "0" And sValorePROV <> "2" Then 'Canone convenz., Art.22, Forze dell'ordine
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValorePROV
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.PROVENIENZA_ASS = " & sValorePROV & " "
            End If

            If sValorePROV = "2" Then 'Erp Moderato
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValorePROV
                bTrovato = True
                sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = " & sValorePROV & " "
            End If
        End If

        If sValoreTC = "L43198" Then
            If sValorePROV <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = sValorePROV
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DEST_USO = '" & sValorePROV & "' "
            End If
        End If


        If sValoreCO <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreCO
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreTC <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreTC
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC ='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreTI <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreTI
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UNITA_CONTRATTUALE.TIPOLOGIA ='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreDUR <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreDUR
            bTrovato = True
            sStringaSql = sStringaSql & " SISCOM_MI.RAPPORTI_UTENZA.DURATA_ANNI='" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreRINN <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreRINN
            bTrovato = True
            sStringaSql = sStringaSql & " SISCOM_MI.RAPPORTI_UTENZA.DURATA_RINNOVO='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreFIL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreFIL
            bTrovato = True
            sStringaSql = sStringaSql & " TAB_FILIALI.ID =" & sValore & " "
        End If

        If sValoreTDOM <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            Select Case sValoreTDOM
                Case "R.R. 1/2004 e s.m.i." 'VECCHIA NORMATIVA
                    bTrovato = True
                    sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA IN (Select ID FROM T_MOTIVO_DOMANDA_VSA WHERE FL_FRONTESPIZIO=0 Or FL_NUOVA_NORMATIVA=0) "
                Case "R.R. 4/2017 e s.m.i." 'NUOVANORMATIVA
                    bTrovato = True
                    sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA IN (SELECT ID FROM T_MOTIVO_DOMANDA_VSA WHERE FL_NUOVA_NORMATIVA=1) "
                Case Else
                    sValore = sValoreTDOM
                    bTrovato = True
                    sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = " & sValore & " "
            End Select


        End If

        If sValoreCAUS <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreCAUS
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = " & sValore & " "
        End If

        If sValoreSTDOM <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreSTDOM
            bTrovato = True
            If sValore = "001" Then
                sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE = 1"
            ElseIf sValore = "002" Then
                sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.FL_CONTABILIZZATO = 1"
            ElseIf sValore = "0" Then
                sStringaSql = sStringaSql & " FL_AUTORIZZAZIONE = 0 "
            Else
                sStringaSql = sStringaSql & " VSA_DECISIONI_REV_C.COD_DECISIONE =" & sValore & " AND FL_AUTORIZZAZIONE = 0 "
            End If
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
            sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.PG " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreEDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreEDAL
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.DATA_EVENTO>='" & sValore & "' "
        End If

        If sValoreEAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreEAL
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.DATA_EVENTO<='" & sValore & "' "
        End If

        If sValorePDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePDAL
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.DATA_PRESENTAZIONE>='" & sValore & "' "
        End If

        If sValorePAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePAL
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.DATA_PRESENTAZIONE<='" & sValore & "' "
        End If

        If sValoreIDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreIDAL
            bTrovato = True
            sStringaSql = sStringaSql & " DICHIARAZIONI_VSA.DATA_INIZIO_VAL>='" & sValore & "' "
        End If

        If sValoreIAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreIAL
            bTrovato = True
            sStringaSql = sStringaSql & " DICHIARAZIONI_VSA.DATA_INIZIO_VAL<='" & sValore & "' "
        End If

        If sValoreFDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreFDAL
            bTrovato = True
            sStringaSql = sStringaSql & " DICHIARAZIONI_VSA.DATA_FINE_VAL>='" & sValore & "' "
        End If

        If sValoreFAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreFAL
            bTrovato = True
            sStringaSql = sStringaSql & " DICHIARAZIONI_VSA.DATA_FINE_VAL<='" & sValore & "' "
        End If

        If sValoreAUTDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreAUTDAL
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.DATA_AUTORIZZAZIONE>='" & sValore & "' "
        End If

        If sValoreAUTAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreAUTAL
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.DATA_AUTORIZZAZIONE<='" & sValore & "' "
        End If

        If sValoreDPGDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreDPGDAL
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.DATA_PG>='" & sValore & "' "
        End If

        If sValoreDPGAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreDPGAL
            bTrovato = True
            sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.DATA_PG<='" & sValore & "' "
        End If

        '----- Nuovi filtri 09/01/2015 (segnalazione SD num. 1024/2014)
        If sValoreOperat <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreOperat
            bTrovato = True
            sStringaSql = sStringaSql & " ID_OPERATORE =" & sValore & ""
        End If

        If sValoreStatoSosp <> "0" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreStatoSosp
            bTrovato = True
            If sValore = "1" Then
                sStringaSql = sStringaSql & " (DICHIARAZIONI_VSA.ID NOT IN (SELECT ID_DICHIARAZIONE FROM VSA_DOC_MANCANTI) OR nvl(FL_FINE_SOSPENSIONE,0)=1 OR FL_PRESENT_DOC_MANC=1) "
            Else
                sStringaSql = sStringaSql & " DICHIARAZIONI_VSA.ID IN (SELECT ID_DICHIARAZIONE FROM VSA_DOC_MANCANTI) AND nvl(FL_FINE_SOSPENSIONE,0)=0 AND FL_PRESENT_DOC_MANC=0 "
            End If
        End If
        '----- Fine nuovi filtri 09/01/2015

        '/////////////////////////////
        '// 1450/2019
        Dim contr1 As String = "1"
        Dim contr2 As String = "1"
        If sValoreQN <> "" And sValoreQN <> "TUTTI" Then
            If sValoreQN = "R.R. 1/2004 e s.m.i." Then
                contr2 = "0"
            Else
                contr1 = "0"
            End If
        End If
        '////////////////////////////

        If (sValoreInval <> "" And sValoreInval <> "0") Then
            If sValoreInval = "SI" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " id_dichiarazione IN ( SELECT id_dichiarazione FROM comp_nucleo_vsa, dichiarazioni_vsa WHERE dichiarazioni_vsa.ID = comp_nucleo_vsa.id_dichiarazione AND natura_inval = 'Motoria con carrozzella') "
            Else
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " id_dichiarazione NOT IN ( SELECT id_dichiarazione FROM comp_nucleo_vsa, dichiarazioni_vsa WHERE dichiarazioni_vsa.ID = comp_nucleo_vsa.id_dichiarazione AND natura_inval = 'Motoria con carrozzella') "
            End If
        End If







        'sStringaSQL1 = "SELECT DISTINCT(RAPPORTI_UTENZA.COD_CONTRATTO) AS CODCONTR,(CASE WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 1 AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO <> 2 then 'ERP Sociale' WHEN UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = 2 THEN 'ERP Moderato' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=12 THEN 'CANONE CONVENZ.' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=8 THEN 'ART.22 C.10 RR 1/2004' " _
        '             & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=10 THEN 'FORZE DELL''ORDINE' WHEN RAPPORTI_UTENZA.DEST_USO='C' THEN 'Cooperative' WHEN RAPPORTI_UTENZA.DEST_USO='P' THEN '431 P.O.R.' WHEN RAPPORTI_UTENZA.DEST_USO='D' THEN '431/98 ART.15 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='S' THEN '431/98 Speciali' " _
        '             & "WHEN RAPPORTI_UTENZA.DEST_USO='0' THEN 'Standard' END) AS TIPO_SPECIFICO, (CASE WHEN RAPPORTI_UTENZA.DURATA_ANNI IS NULL AND RAPPORTI_UTENZA.DURATA_RINNOVO IS NULL THEN RAPPORTI_UTENZA.DURATA_ANNI||''||RAPPORTI_UTENZA.DURATA_RINNOVO ELSE " _
        '             & "RAPPORTI_UTENZA.DURATA_ANNI||'+'||RAPPORTI_UTENZA.DURATA_RINNOVO END) AS DURATA,TAB_FILIALI.NOME AS FILIALE_ALER,COMPLESSI_IMMOBILIARI.COD_COMPLESSO,UNITA_IMMOBILIARI.COD_TIPOLOGIA,INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO"",INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) AS COMUNE_UNITA," _
        '             & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE, " _
        '             & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''ElencoAllegati.aspx?COD='||rapporti_utenza.cod_contratto||''',''Allegati'','''');£>Visualizza</a>','$','&'),'£','" & Chr(34) & "') as ALLEGATI_CONTRATTO, " _
        '             & "RAPPORTI_UTENZA.*,siscom_mi.getstatocontratto(rapporti_utenza.id) as STATO_DEL_CONTRATTO, SISCOM_MI.GETINTESTATARI(RAPPORTI_UTENZA.ID) AS NOME_INTEST," _
        '             & "CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS ""INTESTATARIO"" ," _
        '             & "CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""COD FISCALE/PIVA"" ,TO_CHAR(TO_DATE(ANAGRAFICA.DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_NASCITA,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.PARTITA_IVA," _
        '             & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) AS ""POSIZIONE_CONTRATTO"",TIPOLOGIA_OCCUPANTE.DESCRIZIONE AS TIPO_OCCUPANTE FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.TIPOLOGIA_OCCUPANTE," _
        '             & "SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID (+) AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID AND TIPOLOGIA_RAPP_CONTRATTUALE.COD=RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  " _
        '             & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+)  AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
        '             & "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND TIPOLOGIA_OCCUPANTE.COD = SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL "
        If sValoreSTDOM <> "0" Then
            '& "replace(replace('<a href=£javascript:void(0)£ onclick=£document.getElementById(''H1'').value=''0'';today = new Date();window.open(''../VSA/domanda.aspx?CONT=0&CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande''+ today.getMinutes() + today.getSeconds(),''height=580,width=670'');£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') AS PG_DOM,T_MOTIVO_DOMANDA_VSA.descrizione AS TIPO_DOMANDA_VSA, " _

            sStringaSQL1 = "SELECT rapporti_utenza.cod_contratto AS cod_CONTRATTO, " _
    & "                (CASE" _
    & "                    WHEN rapporti_utenza.provenienza_ass = 1 " _
                        & "AND unita_immobiliari.id_destinazione_uso <> 2 " _
    & "                       THEN 'ERP Sociale' " _
                        & "WHEN unita_immobiliari.id_destinazione_uso = 2 " _
    & "                       THEN 'ERP Moderato' " _
                        & "WHEN rapporti_utenza.provenienza_ass = 12 " _
    & "                       THEN 'CANONE CONVENZ.' " _
                        & "WHEN rapporti_utenza.provenienza_ass = 10 " _
    & "                       THEN 'FORZE DELL''ORDINE' " _
                        & "WHEN rapporti_utenza.dest_uso = 'C' " _
    & "                       THEN 'Cooperative' " _
                        & "WHEN rapporti_utenza.dest_uso = 'P' " _
    & "                       THEN '431 P.O.R.' " _
                        & "WHEN rapporti_utenza.dest_uso = 'D' " _
    & "                       THEN '431/98 ART.15 R.R.1/2004' " _
                        & "WHEN rapporti_utenza.dest_uso = 'V' " _
    & "                       THEN '431/98 ART.15 C.2 R.R.1/2004' " _
                        & "WHEN rapporti_utenza.dest_uso = 'S' " _
    & "                       THEN '431/98 Speciali' " _
                        & "WHEN rapporti_utenza.dest_uso = '0' " _
    & "                       THEN 'Standard' " _
                     & "END " _
                    & ") AS tipo_specifico, " _
                    & "tab_filiali.nome AS filiale_aler, " _
                    & "complessi_immobiliari.cod_complesso, " _
                    & "unita_immobiliari.cod_tipologia, " _
                    & "indirizzi.descrizione AS INDIRIZZO, indirizzi.civico, " _
                    & "rapporti_utenza.*, RAPPORTI_UTENZA.ID AS IDCONTRATTO, " _
                    & "CASE " _
    & "                   WHEN anagrafica.ragione_sociale IS NOT NULL " _
                          & "THEN ragione_sociale " _
                       & "ELSE RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome) " _
                                  & ") " _
                    & "END AS INTESTATARIO, T_MOTIVO_PRESENTAZ_VSA.DESCRIZIONE AS MOT_PRES," _
        & "replace(replace('<a href=£javascript:void(0)£ onclick=£document.getElementById(''H1'').value=''0'';today = new Date();window.open(''../VSA/NuovaDomandaVSA/domandaNuova.aspx?CONT=0&CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande''+ today.getMinutes() + today.getSeconds());£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') AS PG_DOM,T_MOTIVO_DOMANDA_VSA.descrizione AS TIPO_DOMANDA_VSA, " _
        & "T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS TIPO_SPECIFICO_DOMANDA,replace(replace('<a href=£javascript:void(0)£ onclick=£today = new Date();window.open(''../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione''+ today.getMinutes() + today.getSeconds());£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') AS PG_DIC, " _
        & "TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_AUTORIZZAZIONE,'YYYYmmdd'),'DD/MM/YYYY') as DATA_AUTORIZZAZIONE, " _
        & "CASE " _
    & "                   WHEN DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE ='0' " _
                          & "THEN 'NO' " _
                       & "ELSE 'SI' " _
                    & "END AS FL_AUTORIZZATA," _
                            & "CASE " _
    & "                   WHEN DICHIARAZIONI_VSA.ID_STATO ='0' " _
                          & "THEN 'DA COMPLETARE' " _
                       & "ELSE 'COMPLETA' " _
                    & "END AS STATO_DICH," _
            & "CASE " _
    & "                   WHEN DOMANDE_BANDO_VSA.FL_CONTABILIZZATO ='0' " _
                          & "THEN 'NO' " _
                       & "ELSE 'SI' " _
                    & "END AS FL_CONTABILIZZATA,DICHIARAZIONI_VSA.ANNO_SIT_ECONOMICA, " _
    & "TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_PRESENTAZIONE,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_PRESENTAZIONE,TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_EVENTO,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_EVENTO, " _
        & "(CASE WHEN DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE = 1 THEN 'AUTORIZZATA' ELSE (CASE WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 1 THEN 'SOTTOPOSTA A DECISIONE' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 2 THEN 'ACCOLTA' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 3 THEN 'NON ACCOLTA' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 4 THEN 'SOTTOPOSTA A REVISIONE' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 5 THEN 'REVISIONE ACCOLTA' WHEN VSA_DECISIONI_REV_C.COD_DECISIONE = 6 THEN 'REVISIONE NON ACCOLTA' ELSE 'NESSUNA DECISIONE' END) END) AS STATO_DOMANDA, " _
        & "DOMANDE_BANDO_VSA.ID AS IDDOM,TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.data_pg,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_PG,TO_CHAR(TO_DATE(dichiarazioni_vsa.data_inizio_val,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INIZIO_VAL,TO_CHAR(TO_DATE(dichiarazioni_vsa.data_fine_val,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_FINE_VAL,OPERATORI.OPERATORE, 'R.R. 1/2004 e s.m.i.' as Normativa " _
               & "FROM VSA_DECISIONI_REV_C,T_CAUSALI_DOMANDA_VSA,T_MOTIVO_DOMANDA_VSA,eventi_bandi_vsa,operatori, " _
              & "DOMANDE_BANDO_VSA,DICHIARAZIONI_VSA,T_MOTIVO_PRESENTAZ_VSA, " _
              & "siscom_mi.tab_filiali, " _
                    & "siscom_mi.complessi_immobiliari, " _
                    & "siscom_mi.rapporti_utenza, " _
                    & "siscom_mi.anagrafica, " _
                    & "siscom_mi.indirizzi, " _
                    & "siscom_mi.edifici, " _
                    & "siscom_mi.unita_contrattuale, " _
                    & "siscom_mi.unita_immobiliari, " _
                    & "siscom_mi.soggetti_contrattuali " _
              & "WHERE VSA_DECISIONI_REV_C.ID_DOMANDA(+) = DOMANDE_BANDO_VSA.ID AND VSA_DECISIONI_REV_C.DATA = (SELECT MAX(DATA) FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=DOMANDE_BANDO_VSA.ID) AND " _
          & "DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_CAUSALI_DOMANDA_VSA.COD (+) =DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA " _
       & "AND DOMANDE_BANDO_VSA.data_pg>='20120101' AND T_MOTIVO_PRESENTAZ_VSA.ID=DICHIARAZIONI_VSA.MOD_PRESENTAZIONE AND T_MOTIVO_DOMANDA_VSA.ID=DOMANDE_BANDO_VSA.id_motivo_domanda " _
          & "AND DOMANDE_BANDO_VSA.contratto_num=rapporti_utenza.cod_contratto " _
                & "AND complessi_immobiliari.id_filiale = tab_filiali.ID(+) " _
                & "AND edifici.id_complesso = complessi_immobiliari.ID " _
                & "AND edifici.ID = unita_immobiliari.id_edificio and dichiarazioni_vsa.id_stato<>2 " _
                & "AND unita_immobiliari.id_indirizzo = indirizzi.ID (+) " _
                & "AND eventi_bandi_vsa.id_domanda = domande_bando_vsa.id AND eventi_bandi_vsa.cod_evento = 'F190' AND eventi_bandi_vsa.id_operatore=operatori.id(+) " _
                & "AND unita_contrattuale.id_contratto = rapporti_utenza.ID " _
                & "AND unita_immobiliari.ID = unita_contrattuale.id_unita and DICHIARAZIONI_VSA.id_stato<>2 " _
                & "AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID AND soggetti_contrattuali.cod_tipologia_occupante='INTE' " _
                & "AND anagrafica.ID = soggetti_contrattuali.id_anagrafica " _
                & "AND unita_contrattuale.id_unita_principale IS NULL " _
                & "AND (nvl(DOMANDE_BANDO_VSA.fl_nuova_normativa,0)=0 and 1=" & contr1 & ") "


            If sStringaSql <> "" Then
                If Left(sStringaSql, 4) = " AND" Then
                    sStringaSql = Replace(sStringaSql, "AND", " ")
                End If
                sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
            End If
        End If

        If sValoreSTDOM = "" Then
            sStringaSQL1 = sStringaSQL1 & " UNION "
        End If


        If sValoreSTDOM = "0" Or sValoreSTDOM = "" Then
            '& "replace(replace('<a href=£javascript:void(0)£ onclick=£document.getElementById(''H1'').value=''0'';today = new Date();window.open(''../VSA/domanda.aspx?CONT=0&CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande''+ today.getMinutes() + today.getSeconds(),''height=580,width=670'');£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') AS PG_DOM,T_MOTIVO_DOMANDA_VSA.descrizione AS TIPO_DOMANDA_VSA, " _

            sStringaSQL1 = sStringaSQL1 & " SELECT rapporti_utenza.cod_contratto AS cod_CONTRATTO, " _
            & "                (CASE" _
            & "                    WHEN rapporti_utenza.provenienza_ass = 1 " _
                                & "AND unita_immobiliari.id_destinazione_uso <> 2 " _
            & "                       THEN 'ERP Sociale' " _
                                & "WHEN unita_immobiliari.id_destinazione_uso = 2 " _
            & "                       THEN 'ERP Moderato' " _
                                & "WHEN rapporti_utenza.provenienza_ass = 12 " _
            & "                       THEN 'CANONE CONVENZ.' " _
                                & "WHEN rapporti_utenza.provenienza_ass = 10 " _
            & "                       THEN 'FORZE DELL''ORDINE' " _
                                & "WHEN rapporti_utenza.dest_uso = 'C' " _
            & "                       THEN 'Cooperative' " _
                                & "WHEN rapporti_utenza.dest_uso = 'P' " _
            & "                       THEN '431 P.O.R.' " _
                                & "WHEN rapporti_utenza.dest_uso = 'D' " _
            & "                       THEN '431/98 ART.15 R.R.1/2004' " _
                                & "WHEN rapporti_utenza.dest_uso = 'S' " _
            & "                       THEN '431/98 Speciali' " _
                                & "WHEN rapporti_utenza.dest_uso = '0' " _
            & "                       THEN 'Standard' " _
                             & "END " _
                            & ") AS tipo_specifico, " _
                            & "tab_filiali.nome AS filiale_aler, " _
                            & "complessi_immobiliari.cod_complesso, " _
                            & "unita_immobiliari.cod_tipologia, " _
                            & "indirizzi.descrizione AS INDIRIZZO, indirizzi.civico, " _
                            & "rapporti_utenza.*, RAPPORTI_UTENZA.ID AS IDCONTRATTO, " _
                            & "CASE " _
            & "                   WHEN anagrafica.ragione_sociale IS NOT NULL " _
                                  & "THEN ragione_sociale " _
                               & "ELSE RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome) " _
                                          & ") " _
                            & "END AS INTESTATARIO, T_MOTIVO_PRESENTAZ_VSA.DESCRIZIONE AS MOT_PRES," _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£document.getElementById(''H1'').value=''0'';today = new Date();window.open(''../VSA/NuovaDomandaVSA/domandaNuova.aspx?CONT=0&CH=1$ID='||DOMANDE_BANDO_VSA.ID||''',''Domande''+ today.getMinutes() + today.getSeconds());£>'||DOMANDE_BANDO_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') AS PG_DOM,T_MOTIVO_DOMANDA_VSA.descrizione AS TIPO_DOMANDA_VSA, " _
                & "T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS TIPO_SPECIFICO_DOMANDA,replace(replace('<a href=£javascript:void(0)£ onclick=£today = new Date();window.open(''../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione''+ today.getMinutes() + today.getSeconds());£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') AS PG_DIC, " _
                & "TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_AUTORIZZAZIONE,'YYYYmmdd'),'DD/MM/YYYY') as DATA_AUTORIZZAZIONE, " _
                & "CASE " _
            & "                   WHEN DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE ='0' " _
                                  & "THEN 'NO' " _
                               & "ELSE 'SI' " _
                            & "END AS FL_AUTORIZZATA," _
                            & "CASE " _
    & "                   WHEN DICHIARAZIONI_VSA.ID_STATO ='0' " _
                          & "THEN 'DA COMPLETARE' " _
                       & "ELSE 'COMPLETA' " _
                    & "END AS STATO_DICH," _
                    & "CASE " _
            & "                   WHEN DOMANDE_BANDO_VSA.FL_CONTABILIZZATO ='0' " _
                                  & "THEN 'NO' " _
                               & "ELSE 'SI' " _
                            & "END AS FL_CONTABILIZZATA,DICHIARAZIONI_VSA.ANNO_SIT_ECONOMICA, " _
            & "TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_PRESENTAZIONE,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_PRESENTAZIONE,TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_EVENTO,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_EVENTO, " _
                & "'NESSUNA DECISIONE' AS STATO_DOMANDA, " _
                & "DOMANDE_BANDO_VSA.ID AS IDDOM,TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.data_pg,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_PG,TO_CHAR(TO_DATE(dichiarazioni_vsa.data_inizio_val,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INIZIO_VAL,TO_CHAR(TO_DATE(dichiarazioni_vsa.data_fine_val,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_FINE_VAL,OPERATORI.OPERATORE, 'R.R. 1/2004 e s.m.i.' as Normativa " _
                       & "FROM T_CAUSALI_DOMANDA_VSA,T_MOTIVO_DOMANDA_VSA,eventi_bandi_vsa,operatori, " _
                      & "DOMANDE_BANDO_VSA,DICHIARAZIONI_VSA,T_MOTIVO_PRESENTAZ_VSA, " _
                      & "siscom_mi.tab_filiali, " _
                            & "siscom_mi.complessi_immobiliari, " _
                            & "siscom_mi.rapporti_utenza, " _
                            & "siscom_mi.anagrafica, " _
                            & "siscom_mi.indirizzi, " _
                            & "siscom_mi.edifici, " _
                            & "siscom_mi.unita_contrattuale, " _
                            & "siscom_mi.unita_immobiliari, " _
                            & "siscom_mi.soggetti_contrattuali " _
                      & "WHERE DOMANDE_BANDO_VSA.ID NOT IN (SELECT ID_DOMANDA FROM VSA_DECISIONI_REV_C) AND " _
                  & "DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_CAUSALI_DOMANDA_VSA.COD (+) =DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA " _
               & "AND DOMANDE_BANDO_VSA.data_pg>='20120101' AND T_MOTIVO_PRESENTAZ_VSA.ID=DICHIARAZIONI_VSA.MOD_PRESENTAZIONE AND T_MOTIVO_DOMANDA_VSA.ID=DOMANDE_BANDO_VSA.id_motivo_domanda " _
                  & "AND DOMANDE_BANDO_VSA.contratto_num=rapporti_utenza.cod_contratto " _
                        & "AND complessi_immobiliari.id_filiale = tab_filiali.ID(+) " _
                        & "AND edifici.id_complesso = complessi_immobiliari.ID " _
                        & "AND edifici.ID = unita_immobiliari.id_edificio and dichiarazioni_vsa.id_stato<>2 " _
                        & "AND eventi_bandi_vsa.id_domanda = domande_bando_vsa.id AND eventi_bandi_vsa.cod_evento = 'F190' and eventi_bandi_vsa.id_operatore=operatori.id(+) " _
                        & "AND unita_immobiliari.id_indirizzo = indirizzi.ID (+) " _
                        & "AND unita_contrattuale.id_contratto = rapporti_utenza.ID " _
                        & "AND unita_immobiliari.ID = unita_contrattuale.id_unita and DICHIARAZIONI_VSA.id_stato<>2 " _
                        & "AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID AND soggetti_contrattuali.cod_tipologia_occupante='INTE' " _
                        & "AND anagrafica.ID = soggetti_contrattuali.id_anagrafica " _
                        & "AND unita_contrattuale.id_unita_principale IS NULL " _
                        & "AND (nvl(DOMANDE_BANDO_VSA.fl_nuova_normativa,0)=0 and 1=" & contr1 & ") "

            If sStringaSql <> "" Then
                If Left(sStringaSql, 4) = " AND" Then
                    sStringaSql = Replace(sStringaSql, "AND", " ")
                End If
                sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
            End If

            '////////////////////////////////////////////////////
            '// 1450/2019
            sStringaSQL1 = sStringaSQL1 & " UNION "

            sStringaSQL1 = sStringaSQL1 & " SELECT rapporti_utenza.cod_contratto AS cod_CONTRATTO, " _
            & "                (CASE" _
            & "                    WHEN rapporti_utenza.provenienza_ass = 1 " _
                                & "AND unita_immobiliari.id_destinazione_uso <> 2 " _
            & "                       THEN 'ERP Sociale' " _
                                & "WHEN unita_immobiliari.id_destinazione_uso = 2 " _
            & "                       THEN 'ERP Moderato' " _
                                & "WHEN rapporti_utenza.provenienza_ass = 12 " _
            & "                       THEN 'CANONE CONVENZ.' " _
                                & "WHEN rapporti_utenza.provenienza_ass = 10 " _
            & "                       THEN 'FORZE DELL''ORDINE' " _
                                & "WHEN rapporti_utenza.dest_uso = 'C' " _
            & "                       THEN 'Cooperative' " _
                                & "WHEN rapporti_utenza.dest_uso = 'P' " _
            & "                       THEN '431 P.O.R.' " _
                                & "WHEN rapporti_utenza.dest_uso = 'D' " _
            & "                       THEN '431/98 ART.15 R.R.1/2004' " _
                                & "WHEN rapporti_utenza.dest_uso = 'S' " _
            & "                       THEN '431/98 Speciali' " _
                                & "WHEN rapporti_utenza.dest_uso = '0' " _
            & "                       THEN 'Standard' " _
                             & "END " _
                            & ") AS tipo_specifico, " _
                            & "tab_filiali.nome AS filiale_aler, " _
                            & "complessi_immobiliari.cod_complesso, " _
                            & "unita_immobiliari.cod_tipologia, " _
                            & "indirizzi.descrizione AS INDIRIZZO, indirizzi.civico, " _
                            & "rapporti_utenza.*, RAPPORTI_UTENZA.ID AS IDCONTRATTO, " _
                            & "CASE " _
            & "                   WHEN anagrafica.ragione_sociale IS NOT NULL " _
                                  & "THEN ragione_sociale " _
                               & "ELSE RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome) " _
                                          & ") " _
                            & "END AS INTESTATARIO, T_MOTIVO_PRESENTAZ_VSA.DESCRIZIONE AS MOT_PRES," _
                & "replace(replace('<a href=£javascript:void(0)£ onclick=£document.getElementById(''H1'').value=''0'';today = new Date();window.open(''../Gestione_locatari/Istanza.aspx?IDD='|| to_char(domande_bando_vsa.id_dichiarazione) ||''',''Istanze''+ today.getMinutes() + today.getSeconds());£>'|| DOMANDE_BANDO_VSA.PG ||'</a>','$','&'),'£','" & Chr(34) & "') AS PG_DOM, T_MOTIVO_DOMANDA_VSA.descrizione AS TIPO_DOMANDA_VSA, " _
                & "T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS TIPO_SPECIFICO_DOMANDA,  ''    AS PG_DIC, " _
                & "(select distinct max(TO_CHAR(TO_DATE(ITER_AUTORIZZATIVO_ISTANZA.data ,'YYYYmmdd'),'DD/MM/YYYY'))  from ITER_AUTORIZZATIVO_ISTANZA  where ID_STATO_DECISIONE = 3 and id_istanza = domande_bando_vsa.id ) as DATA_AUTORIZZAZIONE, " _
                & " case when (select distinct max(TO_CHAR(TO_DATE(ITER_AUTORIZZATIVO_ISTANZA.data ,'YYYYmmdd'),'DD/MM/YYYY'))  from ITER_AUTORIZZATIVO_ISTANZA  where ID_STATO_DECISIONE = 3 and id_istanza =   domande_bando_vsa.id ) is null then 'NO' else 'SI' end AS FL_AUTORIZZATA, " _
                    & " (select descrizione from t_stato_istanza where t_stato_istanza.id=domande_bando_vsa.id_stato_istanza) AS STATO_DICH," _
                    & "CASE " _
            & "                   WHEN DOMANDE_BANDO_VSA.FL_CONTABILIZZATO ='0' " _
                                  & "THEN '' " _
                               & "ELSE '' " _
                            & "END AS FL_CONTABILIZZATA, DICHIARAZIONI_VSA.ANNO_SIT_ECONOMICA, " _
            & "TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_PRESENTAZIONE,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_PRESENTAZIONE,TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_EVENTO,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_EVENTO, " _
                & " (select descrizione from t_stato_istanza where t_stato_istanza.id=domande_bando_vsa.id_stato_istanza)  || ' ' || (SELECT distinct descrizione  FROM t_stati_decisionali, iter_autorizzativo_istanza  WHERE T_STATI_DECISIONALI.id = id_stato_decisione  AND iter_autorizzativo_istanza.id_istanza = domande_bando_vsa.id AND iter_autorizzativo_istanza.ID = (SELECT MAX (iter_autorizzativo_istanza.ID) FROM iter_autorizzativo_istanza WHERE iter_autorizzativo_istanza.id_istanza = domande_bando_vsa.id )) AS STATO_DOMANDA, " _
                & "DOMANDE_BANDO_VSA.ID AS IDDOM, TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.data_pg,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_PG, " _
                & "(case when T_MOTIVO_DOMANDA_VSA.descrizione in ('OSPITALITÀ','COABITAZIONE') then (select min( TO_CHAR(TO_DATE(Data_inizio_ospite  ,'YYYYmmdd'),'DD/MM/YYYY')   ) from comp_nucleo_ospiti_vsa  where id_domanda = domande_bando_vsa.id) else (case when T_MOTIVO_DOMANDA_VSA.descrizione in ('AMPLIAMENTO') then ( select  min( TO_CHAR(TO_DATE(data_ingresso_nucleo  ,'YYYYmmdd'),'DD/MM/YYYY') )  from comp_nucleo_vsa,t_tipo_parentela,nuovi_comp_nucleo_vsa  where comp_nucleo_vsa.grado_parentela=t_tipo_parentela.cod(+) and comp_nucleo_vsa.id=nuovi_comp_nucleo_vsa.id_componente(+)  and comp_nucleo_vsa.id_dichiarazione=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE ) else ((select distinct max(TO_CHAR(TO_DATE(ITER_AUTORIZZATIVO_ISTANZA.data ,'YYYYmmdd'),'DD/MM/YYYY'))  from ITER_AUTORIZZATIVO_ISTANZA  where ID_STATO_DECISIONE = 3 and id_istanza = domande_bando_vsa.id )) end) end) as DATA_INIZIO_VAL," _
                & "case when T_MOTIVO_DOMANDA_VSA.descrizione in ('OSPITALITÀ','COABITAZIONE') then (select max( TO_CHAR(TO_DATE(Data_fine_ospite  ,'YYYYmmdd'),'DD/MM/YYYY')   ) from comp_nucleo_ospiti_vsa  where id_domanda = domande_bando_vsa.id) else ( case when T_MOTIVO_DOMANDA_VSA.descrizione in ('AMPLIAMENTO') then '' else '' end ) end AS DATA_FINE_VAL, " _
                & "OPERATORI.OPERATORE, 'R.R. 4/2017 e s.m.i.'as Normativa " _
                       & "FROM T_CAUSALI_DOMANDA_VSA,T_MOTIVO_DOMANDA_VSA,eventi_bandi_vsa,operatori, " _
                      & "DOMANDE_BANDO_VSA,DICHIARAZIONI_VSA,T_MOTIVO_PRESENTAZ_VSA, " _
                      & "siscom_mi.tab_filiali, " _
                            & "siscom_mi.complessi_immobiliari, " _
                            & "siscom_mi.rapporti_utenza, " _
                            & "siscom_mi.anagrafica, " _
                            & "siscom_mi.indirizzi, " _
                            & "siscom_mi.edifici, " _
                            & "siscom_mi.unita_contrattuale, " _
                            & "siscom_mi.unita_immobiliari, " _
                            & "siscom_mi.soggetti_contrattuali " _
                      & "WHERE DOMANDE_BANDO_VSA.ID NOT IN (SELECT ID_DOMANDA FROM VSA_DECISIONI_REV_C) AND " _
                  & "DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_CAUSALI_DOMANDA_VSA.COD (+) =DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA " _
               & "AND DOMANDE_BANDO_VSA.data_pg>='20120101' AND T_MOTIVO_PRESENTAZ_VSA.ID=DICHIARAZIONI_VSA.MOD_PRESENTAZIONE AND T_MOTIVO_DOMANDA_VSA.ID=DOMANDE_BANDO_VSA.id_motivo_domanda " _
                  & "AND DOMANDE_BANDO_VSA.contratto_num=rapporti_utenza.cod_contratto " _
                        & "AND complessi_immobiliari.id_filiale = tab_filiali.ID(+) " _
                        & "AND edifici.id_complesso = complessi_immobiliari.ID " _
                        & "AND edifici.ID = unita_immobiliari.id_edificio and dichiarazioni_vsa.id_stato<>2 " _
                        & "AND eventi_bandi_vsa.id_domanda = domande_bando_vsa.id AND eventi_bandi_vsa.cod_evento = 'F190' and eventi_bandi_vsa.id_operatore=operatori.id(+) " _
                        & "AND unita_immobiliari.id_indirizzo = indirizzi.ID (+) " _
                        & "AND unita_contrattuale.id_contratto = rapporti_utenza.ID " _
                        & "AND unita_immobiliari.ID = unita_contrattuale.id_unita and DICHIARAZIONI_VSA.id_stato<>2 " _
                        & "AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID AND soggetti_contrattuali.cod_tipologia_occupante='INTE' " _
                        & "AND anagrafica.ID = soggetti_contrattuali.id_anagrafica " _
                        & "AND unita_contrattuale.id_unita_principale IS NULL " _
                        & "AND (nvl(DOMANDE_BANDO_VSA.fl_nuova_normativa,0)=1 and 1=" & contr2 & ") "

            If sStringaSql <> "" Then
                If Left(sStringaSql, 4) = " AND" Then
                    sStringaSql = Replace(sStringaSql, "AND", " ")
                End If
                sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
            End If
            '////////////////////////////////////////////////////
            ' & "(select distinct max(TO_CHAR(TO_DATE(ITER_AUTORIZZATIVO_ISTANZA.data ,'YYYYmmdd'),'DD/MM/YYYY'))  from ITER_AUTORIZZATIVO_ISTANZA  where ID_STATO_DECISIONE = 3 and id_istanza = domande_bando_vsa.id ) AS DATA_INIZIO_VAL_OLD,   " _

        End If



        sStringaSQL1 = sStringaSQL1 & " ORDER BY ""INTESTATARIO"" ASC,PG_DOM DESC"


        'sStringaSQL2 = "SELECT COUNT(distinct rapporti_utenza.cod_contratto) FROM SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA," _
        '     & "SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE TIPOLOGIA_RAPP_CONTRATTUALE.COD=RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  " _
        '     & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+)  AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
        '     & "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL "

        'If sStringaSql <> "" Then
        '    sStringaSQL2 = sStringaSQL2 & " AND " & Replace(sStringaSql, " AND (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') ", "")
        'End If


        BindGrid()

    End Function

    Private Sub BindGrid()
        Try

            Dim miocolore As String = "#99CCFF"

            Dim dt As New Data.DataTable
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            da.Fill(dt)

            Label4.Text = DataGrid1.Items.Count
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()

            Label4.Text = dt.Rows.Count

            'Dim maggDueRighe As Boolean
            'For i As Integer = 0 To dt.Rows.Count - 1
            '    For Each di As DataGridItem In DataGrid1.Items
            '        If i < dt.Rows.Count - 1 Then
            '            maggDueRighe = False
            '            If i > 0 Then
            '                If dt.Rows(i).Item(0) = dt.Rows(i + 1).Item(0) And dt.Rows(i).Item(0) = dt.Rows(i - 1).Item(0) Then
            '                    di.BackColor = Drawing.ColorTranslator.FromHtml(miocolore)
            '                    maggDueRighe = True
            '                End If
            '            End If
            '            If dt.Rows(i).Item(0) = dt.Rows(i + 1).Item(0) Then
            '                If maggDueRighe = False Then
            '                    If i > 0 Then
            '                        If miocolore = "#ffffff" Then
            '                            miocolore = "#99CCFF"
            '                        Else
            '                            miocolore = "#ffffff"
            '                        End If
            '                    End If
            '                End If
            '                di.BackColor = Drawing.ColorTranslator.FromHtml(miocolore)
            '            Else
            '                If i > 0 Then
            '                    If dt.Rows(i).Item(0) = dt.Rows(i - 1).Item(0) Then
            '                        If miocolore = "#ffffff" Then
            '                            miocolore = "#ffffff"
            '                        Else
            '                            miocolore = "#99CCFF"
            '                        End If
            '                        di.BackColor = Drawing.ColorTranslator.FromHtml(miocolore)
            '                    Else
            '                        If miocolore <> "#ffffff" Then
            '                            miocolore = "#ffffff"
            '                        End If
            '                        di.BackColor = Drawing.ColorTranslator.FromHtml(miocolore)
            '                    End If
            '                End If
            '            End If
            '            i = i + 1
            '        Else
            '            If i > 0 Then

            '                If dt.Rows(i).Item(0) = dt.Rows(i - 1).Item(0) Then
            '                    If miocolore = "#ffffff" Then
            '                        miocolore = "#ffffff"
            '                    Else
            '                        miocolore = "#99CCFF"
            '                    End If
            '                    di.BackColor = Drawing.ColorTranslator.FromHtml(miocolore)
            '                Else
            '                    If miocolore <> "#ffffff" Then
            '                        miocolore = "#ffffff"
            '                    End If
            '                    di.BackColor = Drawing.ColorTranslator.FromHtml(miocolore)
            '                End If
            '            End If
            '        End If
            '    Next
            'Next


        Catch ex As Exception
            ' par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza: Risultati Ricerca Dom. Gest.Locatari - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='silver';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato il contratto Cod. " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('Label3').value='" & e.Item.Cells(1).Text & "'")
            e.Item.Attributes.Add("onDblclick", "ApriContratto();")
        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='silver';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato il contratto Cod. " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('Label3').value='" & e.Item.Cells(1).Text & "'")
        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        If H1.Value = "1" Then
            ExportXLS_Chiama100()
            H1.Value = "0"
        End If
    End Sub

    Private Sub ExportXLS_Chiama100()

        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String = ""
        Dim row As System.Data.DataRow
        Dim dt As New Data.DataTable
        Dim par As New CM.Global

        Dim FileCSV As String = ""

        Try
            par.OracleConn.Open()
            FileCSV = "Estrazione" & Format(Now, "yyyyMMddHHmmss")

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter

            da = New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                i = 0
                With myExcelFile

                    .CreateFile(Server.MapPath("..\FileTemp\" & FileCSV & ".xls"))
                    .PrintGridLines = False
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                    .SetDefaultRowHeight(14)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                    .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)
                    .SetColumnWidth(1, 1, 30)
                    .SetColumnWidth(2, 2, 35)
                    .SetColumnWidth(3, 3, 20)
                    .SetColumnWidth(4, 4, 40)
                    .SetColumnWidth(5, 5, 70)
                    .SetColumnWidth(6, 6, 30)
                    '                    .SetColumnWidth(7, 7, 20)
                    .SetColumnWidth(8, 7, 25)
                    .SetColumnWidth(9, 8, 25)
                    .SetColumnWidth(10, 9, 20)
                    .SetColumnWidth(11, 10, 20)
                    .SetColumnWidth(12, 11, 25)
                    .SetColumnWidth(13, 12, 20)
                    .SetColumnWidth(14, 13, 25)
                    .SetColumnWidth(15, 14, 20)
                    .SetColumnWidth(16, 15, 20)
                    .SetColumnWidth(17, 16, 30)
                    .SetColumnWidth(18, 17, 20)
                    .SetColumnWidth(19, 18, 20)
                    .SetColumnWidth(20, 19, 25)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD. CONTRATTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INTESTATARIO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "NUM. DOM./PROT.", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "TIPO DOMANDA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "TIPO SPECIFICO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "STATO DOMANDA", 12)
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "NUM. DICH.", 12)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "DATA EVENTO", 12)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "DATA PRESENTAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "MOTIVO PRESENTAZ.", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "DATA INIZIO VALIDITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "DATA FINE VALIDITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "ANNO REDDITI", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "AUTORIZZATA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "DATA AUTORIZZAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "CONTABILIZZATA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "FILIALE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "INDIRIZZO UNITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "CIVICO UNITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "DATA INSERIMENTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "NORMATIVA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "OPERATORE", 12)

                    K = 2
                    For Each row In dt.Rows
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTESTATARIO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(Left(Right(par.IfNull(dt.Rows(i).Item("PG_DOM"), ""), 14), 10)))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO_DOMANDA_VSA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPO_SPECIFICO_DOMANDA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("STATO_DOMANDA"), "")))

                        '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(Left(Right(par.IfNull(dt.Rows(i).Item("PG_DIC"), ""), 14), 10)))

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_EVENTO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_PRESENTAZIONE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOT_PRES"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_INIZIO_VAL"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_FINE_VAL"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ANNO_SIT_ECONOMICA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FL_AUTORIZZATA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_AUTORIZZAZIONE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FL_CONTABILIZZATA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FILIALE_ALER"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_PG"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NORMATIVA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("OPERATORE"), "")))
                        i = i + 1
                        K = K + 1
                    Next

                    .CloseFile()
                End With

            End If

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & FileCSV & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & FileCSV & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte

            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            File.Delete(strFile)

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            ' Response.Write("<script>window.open('../FileTemp/" & FileCSV & ".zip','Estrazione','');</script>")
            Response.Redirect("..\FileTemp\" & FileCSV & ".zip")

        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try




    End Sub
End Class
