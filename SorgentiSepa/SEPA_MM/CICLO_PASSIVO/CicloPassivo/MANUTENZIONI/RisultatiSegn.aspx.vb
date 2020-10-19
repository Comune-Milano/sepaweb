Imports Telerik.Web.UI
Imports System.Data

Partial Class CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_RisultatiSegn
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Private isFilter As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        'Dim Str As String

        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        'Str = Str & "<" & "/div>"

        'Response.Write(Str)

        'Response.Expires = 0
        If IsPostBack = False Then
            HFGriglia.Value = DataGridSegnalaz.ClientID
            ' Response.Flush()
            TipoSegnalazione.Value = Request.QueryString("TIPO")
            tipo1.Value = Request.QueryString("T")
            dal.Value = Request.QueryString("D")
            oreda.Value = Request.QueryString("OREDA")
            minda.Value = Request.QueryString("MINDA")
            al.Value = Request.QueryString("A")
            orea.Value = Request.QueryString("OREA")
            mina.Value = Request.QueryString("MINA")
            filiale.Value = Request.QueryString("F")
            edificio.Value = Request.QueryString("E")
            complesso.Value = Request.QueryString("COMP")
            segnalante.Value = Request.QueryString("O")
            stato.Value = Request.QueryString("STAT")
            idBm.Value = Request.QueryString("IDBM")
            canale.Value = Request.QueryString("CANALE")
            tipoSegnalante.Value = Request.QueryString("IDTIPOSEGN")

            If Not IsNothing(Request.QueryString("NUM")) Then
                numero.Value = Request.QueryString("NUM")
            End If
            If Not IsNothing(Request.QueryString("URG")) Then
                urgenza.Value = par.IfEmpty(Request.QueryString("URG"), "-1")
            End If
            If Request.QueryString("C") = "1" Then
                origine.Value = " AND SEGNALAZIONI.ID_STATO=-1 and segnalazioni.origine='C'"
            Else
                origine.Value = " AND SEGNALAZIONI.ID_STATO<>-1 "
            End If
            If Not IsNothing(Request.QueryString("PROV")) Then
                prov.Value = Request.QueryString("PROV")
            End If
            If Not IsNothing(Request.QueryString("FOR")) Then
                fornitore.Value = Request.QueryString("FOR")
            End If

            If Not IsNothing(Request.QueryString("CAT0")) Then
                cat0.Value = Request.QueryString("CAT0")
            End If
            If Not IsNothing(Request.QueryString("CAT1")) Then
                cat1.Value = Request.QueryString("CAT1")
            End If
            If Not IsNothing(Request.QueryString("CAT2")) Then
                cat2.Value = Request.QueryString("CAT2")
            End If
            If Not IsNothing(Request.QueryString("CAT3")) Then
                cat3.Value = Request.QueryString("CAT3")
            End If
            If Not IsNothing(Request.QueryString("CAT4")) Then
                cat4.Value = Request.QueryString("CAT4")
            End If
        End If
    End Sub

    Protected Sub imgApri_Click(sender As Object, e As System.EventArgs) Handles imgApri.Click
        If LBLID.Value = "" Then
            RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Else
            Session.Item("SEGNALAZIONIINDIETRO") = Request.Url.Query.Replace("?", "")
            Response.Write("<script>location.replace('Segnalazioni.aspx?IDS=" & LBLID.Value & "&ST=" & IDSTATO.Value & "&ORD=PER');</script>")
        End If
    End Sub



    Protected Sub DataGridSegnalaz_ItemDataBound1(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGridSegnalaz.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('identificativo').value='" & dataItem("TIPO_INT").Text.Replace("&nbsp;", "") & "';document.getElementById('tipo1').value='" & dataItem("TIPO").Text & "';document.getElementById('TextBox3').value='Hai selezionato la segnalazione N°" & dataItem("NUM").Text & "';document.getElementById('LBLID').value='" & dataItem("ID").Text & "';document.getElementById('IDSTATO').value='" & dataItem("ID_STATO").Text & "';")
            e.Item.Attributes.Add("onDblclick", "Apri();")

            If dataItem("TIPOLOGIA").Text = "1" Then
                Select Case dataItem("ID_PERICOLO_SEGNALAZIONE").Text
                    Case "1"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""Immagini/Ball-white-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "2"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""Immagini/Ball-green-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "3"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""Immagini/Ball-yellow-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "4"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""Immagini/Ball-red-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "0"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""Immagini/Ball-blue-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case Else
                End Select
            End If

        End If
    End Sub

    Protected Sub DataGridSegnalaz_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGridSegnalaz.NeedDataSource
        Try


            Dim s As String = ""


            If numero.Value <> "-1" And numero.Value <> "" Then
                s &= " AND segnalazioni.id=" & numero.Value
            End If

            If urgenza.Value <> "---" Then

                Select Case UCase(urgenza.Value)
                    Case "---"
                        urgenza.Value = "-1"
                    Case "BIANCO"
                        urgenza.Value = "1"
                    Case "VERDE"
                        urgenza.Value = "2"
                    Case "GIALLO"
                        urgenza.Value = "3"
                    Case "ROSSO"
                        urgenza.Value = "4"
                    Case "BLU"
                        urgenza.Value = "0"
                End Select
                If urgenza.Value <> "-1" And urgenza.Value <> "" Then
                    s &= " AND segnalazioni.id_pericolo_Segnalazione=" & urgenza.Value
                End If
            End If

            If TipoSegnalazione.Value <> "-1" And TipoSegnalazione.Value <> "" Then
                s &= " AND id_TIPO_SEGNALAZIONE=" & TipoSegnalazione.Value
            End If


            If cat0.Value <> "-1" And cat0.Value <> "" Then
                s &= " AND id_TIPO_SEGNALAZIONE IN (" & cat0.Value & ")"
            End If
            If cat1.Value <> "-1" And cat1.Value <> "" Then
                s &= " AND id_TIPO_SEGN_LIVELLO_1 = " & cat1.Value
            End If
            If cat2.Value <> "-1" And cat2.Value <> "" Then
                s &= " AND id_TIPO_SEGN_LIVELLO_2 = " & cat2.Value
            End If
            If cat3.Value <> "-1" And cat3.Value <> "" Then
                s &= " AND id_TIPO_SEGN_LIVELLO_3 = " & cat3.Value
            End If
            If cat4.Value <> "-1" And cat4.Value <> "" Then
                s &= " AND id_TIPO_SEGN_LIVELLO_4 = " & cat4.Value
            End If

            Dim dataMin As String = ""
            Dim dataMax As String = ""
            If dal.Value <> "" Then
                dataMin = par.AggiustaData(dal.Value)
                If oreda.Value <> "" Then
                    dataMin &= oreda.Value.ToString.PadLeft(2, "0")
                    If minda.Value <> "" Then
                        dataMin &= minda.Value.ToString.PadLeft(2, "0")
                    Else
                        dataMin &= "00"
                    End If
                Else
                    dataMin &= "0000"
                End If
            End If
            If dataMin <> "" Then
                s += " AND SUBSTR(DATA_ORA_RICHIESTA,1,12)>='" & dataMin & "'  "
            End If
            If al.Value <> "" Then
                dataMax = par.AggiustaData(al.Value)
                If orea.Value <> "" Then
                    dataMax &= orea.Value.ToString.PadLeft(2, "0")
                    If mina.Value <> "" Then
                        dataMax &= mina.Value.ToString.PadLeft(2, "0")
                    Else
                        dataMax &= "00"
                    End If
                Else
                    dataMax &= "2400"
                End If
            End If
            If dataMax <> "" Then
                s += " AND SUBSTR(DATA_ORA_RICHIESTA,1,12)<='" & dataMax & "' "
            End If

            'If orea.Value <> "" Then
            '    s += " AND SUBSTR(DATA_ORA_RICHIESTA,9,2)<='" & orea.Value.ToString.PadLeft(2, "0") & "'  "
            'End If
            'If oreda.Value <> "" Then
            '    s += " AND SUBSTR(DATA_ORA_RICHIESTA,9,2)>='" & oreda.Value.ToString.PadLeft(2, "0") & "'  "
            'End If
            'If mina.Value <> "" Then
            '    s += " AND SUBSTR(DATA_ORA_RICHIESTA,11,2)<='" & mina.Value.ToString.PadLeft(2, "0") & "'  "
            'End If
            'If minda.Value <> "" Then
            '    s += " AND SUBSTR(DATA_ORA_RICHIESTA,11,2)>='" & minda.Value.ToString.PadLeft(2, "0") & "'  "
            'End If

            'If dal.Value <> "" Then
            '    s += " AND SUBSTR(DATA_ORA_RICHIESTA,1,8)>='" & par.AggiustaData(dal.Value) & "'  "
            'End If

            'If al.Value <> "" Then
            '    s += " AND SUBSTR(DATA_ORA_RICHIESTA,1,8)<='" & par.AggiustaData(al.Value) & "' "
            'End If

            If filiale.Value <> "-1" Then
                s += "AND TAB_FILIALI.ID='" & filiale.Value & "' "
            End If

            If edificio.Value <> "-1" Then
                s += "AND SEGNALAZIONI.ID_EDIFICIO = " & edificio.Value & " "
            End If
            If complesso.Value <> "-1" Then
                s += "AND SEGNALAZIONI.ID_EDIFICIO in (select id from siscom_mi.edifici where id_complesso= " & complesso.Value & ") "
            End If
            If Trim(segnalante.Value) <> "" Then
                Dim listaSegnalante As String()
                listaSegnalante = segnalante.Value.ToString.Split(" ")

                s += " AND ("
                If listaSegnalante.Length = 1 Then
                    s += " upper(SEGNALAZIONI.COGNOME_RS) LIKE '" & par.PulisciStrSql(UCase(listaSegnalante(0).Replace("*", "%"))) & "' "
                    s += " OR upper(SEGNALAZIONI.NOME) LIKE '" & par.PulisciStrSql(UCase(listaSegnalante(0).Replace("*", "%"))) & "' "
                Else
                    s += "("
                    For i As Integer = 0 To listaSegnalante.Length - 1 Step 1
                        If i = 0 Then
                            s += " upper(SEGNALAZIONI.COGNOME_RS) LIKE '" & par.PulisciStrSql(UCase(listaSegnalante(i).Replace("*", "%"))) & "' "
                        Else
                            s += " OR upper(SEGNALAZIONI.COGNOME_RS) LIKE '" & par.PulisciStrSql(UCase(listaSegnalante(i).Replace("*", "%"))) & "' "
                        End If
                    Next
                    s += ") AND ("
                    For i As Integer = 0 To listaSegnalante.Length - 1 Step 1
                        If i = 0 Then
                            s += " upper(SEGNALAZIONI.NOME) LIKE '" & par.PulisciStrSql(UCase(listaSegnalante(i).Replace("*", "%"))) & "' "
                        Else
                            s += " OR upper(SEGNALAZIONI.NOME) LIKE '" & par.PulisciStrSql(UCase(listaSegnalante(i).Replace("*", "%"))) & "' "
                        End If
                    Next
                    s += ")"
                End If
                s += " )"
                's += "AND SEGNALAZIONI.ID_OPERATORE_INS = " & segnalante.Value & " "
            End If
            If stato.Value <> "" Then
                s += "AND SEGNALAZIONI.ID_STATO IN (" & stato.Value & ") "
            End If

            If IsNumeric(canale.Value) AndAlso canale.Value > 0 Then
                s += " AND SEGNALAZIONI.ID_CANALE = " & canale.Value & " "
            End If

            If IsNumeric(tipoSegnalante.Value) AndAlso tipoSegnalante.Value > 0 Then
                s += " AND SEGNALAZIONI.ID_TIPOLOGIA_SEGNALANTE = " & tipoSegnalante.Value & " "
            End If

            Dim ordine As String = ""
            If Not IsNothing(Request.QueryString("ORDINE")) Then
                ordine = Request.QueryString("ORDINE")
            End If
            Dim condBm As String = ""
            If par.IfEmpty(idBm.Value, 0) > 0 Then
                s += " AND BUILDING_MANAGER.ID = " & idBm.Value
                s += " and building_manager.id = edifici.id_bm "
            Else
                s += " and building_manager.id(+) = edifici.id_bm "

            End If

            If fornitore.Value <> "-1" Then
                s += "AND SEGNALAZIONI.ID IN (SELECT ID_sEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_FORNITORE=" & fornitore.Value & "))"
            End If

            Dim condizioneOrdinamento As String = ""
            Select Case ordine
                Case "0"
                    'STATO
                    condizioneOrdinamento = "ORDER BY ID_STATO,ID_PERICOLO_SEGNALAZIONE DESC,ID_TIPO_SEGNALAZIONE,DATA_ORA_RICHIESTA ASC"
                Case "1"
                    'URGENZA
                    condizioneOrdinamento = "ORDER BY ID_PERICOLO_SEGNALAZIONE DESC,ID_STATO,ID_TIPO_SEGNALAZIONE,DATA_ORA_RICHIESTA ASC"
                Case "2"
                    'TIPO SEGNALAZIONE
                    condizioneOrdinamento = "ORDER BY ID_TIPO_SEGNALAZIONE,ID_STATO,ID_PERICOLO_SEGNALAZIONE DESC,DATA_ORA_RICHIESTA ASC"
                Case Else
            End Select

            'If Session.Item("OP_COM") = "0" Then
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select COGNOME_RS||' '||NOME AS RICHIEDENTE,CASE WHEN ID_UNITA is not null THEN id_unita WHEN ID_EDIFICIO is not null THEN id_edificio WHEN ID_COMPLESSO is not null THEN id_complesso END AS identificativo,CASE WHEN ID_UNITA is not null THEN 'U' WHEN ID_EDIFICIO is not null THEN 'E' WHEN ID_COMPLESSO is not null THEN 'C' END AS TIPOS,substr(replace(segnalazioni.DESCRIZIONE_RIC,'''',''),1,30)||'...' as des, DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'GUASTI',1,'RECLAMI',2,'PROPOSTE',3,'VARIE') AS TIPO,Segnalazioni.id,TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INSERIMENTO,segnalazioni.DESCRIZIONE_RIC,tab_stati_segnalazioni.descrizione as stato FROM siscom_mi.tab_stati_segnalazioni,SISCOM_MI.SEGNALAZIONI where " & s & " tab_stati_segnalazioni.id=segnalazioni.id_stato " & origine.Value & " ORDER BY tab_stati_segnalazioni.id desc,data_ora_richiesta desc", par.OracleConn)
            par.cmd.CommandText = "SELECT   segnalazioni.ID, " _
                                & "segnalazioni.ID AS num, " _
                                & "(select TIPO_SEGNALAZIONE.DESCRIZIONE from siscom_mi.tipo_Segnalazione where tipo_Segnalazione.id=siscom_mi.segnalazioni.id_tipo_Segnalazione) " _
                                & "/*(DECODE (segnalazioni.tipo_richiesta,0, 'INFORMAZIONI',1, 'GUASTI'))*/ AS tipo, " _
                                & "(case when segnalazioni.id_tipo_segnalazione=1 then tipologie_guasti.descrizione else null end) AS tipo_int, " _
                                 & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TAB_TIPOLOGIA_SEGNALANTE WHERE TAB_TIPOLOGIA_SEGNALANTE.ID=ID_TIPOLOGIA_SEGNALANTE) AS TIPO_SEGNALANTE, " _
                                & "tab_stati_segnalazioni.descrizione " _
                                & " || " _
                                & " (CASE " _
                                & " WHEN segnalazioni.id_tipo_segnalazione in (1,6) " _
                                & " and tab_stati_segnalazioni.id=4 " _
                                & " THEN " _
                                & " ' '||  (SELECT WM_CONCAT(PROGR||'/'||ANNO) FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_SEGNALAZIONI=SEGNALAZIONI.ID AND STATO NOT IN (5,6))||'' " _
                                & " ELSE " _
                                & " NULL " _
                                & " END) " _
                                & " AS stato, " _
                                & " (select count(*) from siscom_mi.segnalazioni_note where id_segnalazione = segnalazioni.id and sollecito = 1) as n_solleciti, " _
                                & "edifici.denominazione AS indirizzo, " _
                                & "cognome_rs || ' ' || segnalazioni.nome AS richiedente, " _
                                & "TELEFONO1 AS TELEFONO_richiedente, " _
                                & "TO_DATE (SUBSTR (data_ora_richiesta, 1, 8),'YYYYMMDD') AS data_inserimento, " _
                                & "REPLACE (segnalazioni.descrizione_ric, '''', '') AS descrizione, " _
                                & "/*segnalazioni.descrizione_ric,*/ " _
                                & "NVL (siscom_mi.tab_filiali.nome, ' ') AS filiale, " _
                                & "(CASE WHEN ID_STATO = 10 THEN (SELECT max(NOTE) FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                                & " ID_TIPO_sEGNALAZIONE_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND ID_TIPO_SEGNALAZIONE_note=2)" _
                                & ") ELSE '' END) AS NOTE_C " _
                                & ",(case when length(segnalazioni.data_chiusura)<8 then null else TO_DATE (SUBSTR (SEGNALAZIONI.DATA_CHIUSURA, 1, 8),'YYYYMMDD') end) as data_chiusura " _
                                & ",(select TO_DATE(MAX(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE),'YYYYMMDD') from siscom_mi.manutenzioni where MANUTENZIONI.STATO < 5 AND SISCOM_MI.MANUTENZIONI.ID=(select max(id) from siscom_mi.manutenzioni where id_SEGNALAZIONI = segnalazioni.id)) as data_ordine " _
                                & ", id_pericolo_Segnalazione,SEGNALAZIONI.ID_sTATO AS ID_STATO , " _
                                & "id_tipo_segnalazione AS TIPOLOGIA, " _
                                & " (CASE WHEN codice IS NOT NULL THEN (CODICE) ELSE '' END) || ' ' || siscom_mi.getbuildingmanagersegnalazioni(segnalazioni.id)  as MANAGER " _
                                & ",(select count(*) from siscom_mi.segnalazioni_note where sollecito=1 and id_segnalazione=segnalazioni.id) as cont " _
                                & ",(SELECT DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO)  AS COMPLESSO " _
                                & ",(SELECT ZONA FROM SEPA.ZONA_ALER WHERE ZONA_ALER.COD=EDIFICI.ID_ZONA) AS MUNICIPIO " _
                                & ",(SELECT MAX(RAGIONE_SOCIALE) FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.PRENOTAZIONI WHERE MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO=PRENOTAZIONI.ID AND MANUTENZIONI.ID_SEGNALAZIONI=SEGNALAZIONI.ID)) AS FORNITORE " _
                                & "FROM siscom_mi.tab_stati_segnalazioni, " _
                                & "siscom_mi.segnalazioni, " _
                                & "siscom_mi.tab_filiali, " _
                                & "siscom_mi.edifici, " _
                                & "siscom_mi.complessi_immobiliari, " _
                                & "siscom_mi.unita_immobiliari, " _
                                & "siscom_mi.TIPOLOGIE_GUASTI, " _
                                & "siscom_mi.BUILDING_MANAGER, " _
                                & "OPERATORI " _
                                & "WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
                                & "AND segnalazioni.id_stato <> -1 " _
                                & "AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                                & "AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
                                & "AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
                                & "AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
                                & "AND complessi_immobiliari.id(+) = edifici.id_complesso " _
                                & "AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " & s _
                                & " AND EXISTS (SELECT ID FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI=SEGNALAZIONI.ID AND MANUTENZIONI.STATO<5) " _
                                & "ORDER BY ID_PERICOLO_sEGNALAZIONE DESC,data_ora_richiesta asc,cont desc "



            'par.cmd.CommandText = "select COGNOME_RS||' '||SEGNALAZIONI.NOME AS RICHIEDENTE,CASE WHEN SISCOM_MI.SEGNALAZIONI.ID_UNITA is not null THEN SISCOM_MI.SEGNALAZIONI.id_unita WHEN SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO is not null THEN SISCOM_MI.SEGNALAZIONI.id_edificio WHEN SISCOM_MI.SEGNALAZIONI.ID_COMPLESSO is not null THEN SISCOM_MI.SEGNALAZIONI.id_complesso END AS identificativo,CASE WHEN SISCOM_MI.SEGNALAZIONI.ID_UNITA is not null THEN 'U'" _
            '                                                  & "WHEN SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO is not null THEN 'E' WHEN SISCOM_MI.SEGNALAZIONI.ID_COMPLESSO is not null THEN 'C' END AS TIPOS,substr(replace(segnalazioni.DESCRIZIONE_RIC,'''',''),1,30)||'...' as des, DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'INFORMAZIONI',1,'GUASTI') AS TIPO,Segnalazioni.id," _
            '                                                  & "TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INSERIMENTO,segnalazioni.DESCRIZIONE_RIC,tab_stati_segnalazioni.descrizione as stato,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE, (CASE WHEN siscom_mi.segnalazioni.id_complesso IS NOT NULL THEN COMPLESSI_IMMOBILIARI.denominazione ELSE EDIFICI.DENOMINAZIONE END) AS INDIRIZZO,(OPERATORI.cognome || ' ' || OPERATORI.nome) AS segnalante FROM siscom_mi.tab_stati_segnalazioni,SISCOM_MI.SEGNALAZIONI,SISCOM_MI.TAB_FILIALI," _
            '                                                  & "SISCOM_MI.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari,OPERATORI where " & s & " tab_stati_segnalazioni.id=segnalazioni.id_stato " & origine.Value & " and siscom_mi.complessi_immobiliari.id_filiale  = siscom_mi.tab_filiali.id(+) " _
            '                                                  & "and siscom_mi.segnalazioni.id_complesso = siscom_mi.complessi_immobiliari.id(+) and siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.id(+) and siscom_mi.unita_immobiliari.id(+)= siscom_mi.segnalazioni.id_unita AND OPERATORI.ID = SEGNALAZIONI.ID_OPERATORE_INS ORDER BY tab_stati_segnalazioni.id desc,data_ora_richiesta desc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)

            'da.Fill(dt)
            'Else
            '    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select COGNOME_RS||' '||NOME AS RICHIEDENTE,CASE WHEN ID_UNITA is not null THEN id_unita WHEN ID_EDIFICIO is not null THEN id_edificio WHEN ID_COMPLESSO is not null THEN id_complesso END AS identificativo,CASE WHEN ID_UNITA is not null THEN 'U' WHEN ID_EDIFICIO is not null THEN 'E' WHEN ID_COMPLESSO is not null THEN 'C' END AS TIPOS,substr(replace(segnalazioni.DESCRIZIONE_RIC,'''',''),1,30)||'...' as des, DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'GUASTI',1,'RECLAMI',2,'PROPOSTE',3,'VARIE') AS TIPO,Segnalazioni.id,TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INSERIMENTO,segnalazioni.DESCRIZIONE_RIC,tab_stati_segnalazioni.descrizione as stato FROM siscom_mi.tab_stati_segnalazioni,SISCOM_MI.SEGNALAZIONI where " & s & " tab_stati_segnalazioni.id=segnalazioni.id_stato AND SEGNALAZIONI.ORIGINE='C' ORDER BY tab_stati_segnalazioni.id desc,data_ora_richiesta desc", par.OracleConn)
            '    par.cmd.CommandText = "select COGNOME_RS||' '||SEGNALAZIONI.NOME AS RICHIEDENTE,CASE WHEN  SISCOM_MI.SEGNALAZIONI.ID_UNITA is not null THEN  SISCOM_MI.SEGNALAZIONI.id_unita WHEN  SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO is not null THEN  SISCOM_MI.SEGNALAZIONI.id_edificio WHEN  SISCOM_MI.SEGNALAZIONI.ID_COMPLESSO is not null THEN  SISCOM_MI.SEGNALAZIONI.id_complesso END AS identificativo,CASE WHEN  SISCOM_MI.SEGNALAZIONI.ID_UNITA is not null " _
            '                                                      & "THEN 'U' WHEN  SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO is not null THEN 'E' WHEN  SISCOM_MI.SEGNALAZIONI.ID_COMPLESSO is not null THEN 'C' END AS TIPOS,substr(replace(segnalazioni.DESCRIZIONE_RIC,'''',''),1,30)||'...' as des, DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'INFORMAZIONI',1,'GUASTI')" _
            '                                                      & "AS TIPO,Segnalazioni.id,TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INSERIMENTO,segnalazioni.DESCRIZIONE_RIC,tab_stati_segnalazioni.descrizione as stato,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE,(CASE WHEN siscom_mi.segnalazioni.id_complesso IS NOT NULL THEN COMPLESSI_IMMOBILIARI.denominazione ELSE EDIFICI.DENOMINAZIONE END) AS INDIRIZZO,(OPERATORI.cognome || ' ' || OPERATORI.nome) AS segnalante FROM siscom_mi.tab_stati_segnalazioni,SISCOM_MI.SEGNALAZIONI, " _
            '                                                      & "SISCOM_MI.TAB_FILIALI,SISCOM_MI.complessi_immobiliari,OPERATORI where " & s & " tab_stati_segnalazioni.id=segnalazioni.id_stato AND SEGNALAZIONI.ORIGINE='C' and siscom_mi.complessi_immobiliari.id_filiale  = siscom_mi.tab_filiali.id(+) " _
            '                                                      & "and siscom_mi.segnalazioni.id_complesso = siscom_mi.complessi_immobiliari.id(+) and siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.id(+) and siscom_mi.unita_immobiliari.id(+)= siscom_mi.segnalazioni.id_unita AND OPERATORI.ID = SEGNALAZIONI.ID_OPERATORE_INS ORDER BY tab_stati_segnalazioni.id desc,data_ora_richiesta desc"

            '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)

            '    da.Fill(dt)
            'End If


            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)

            'Session.Item("DataGridSegnalaz") = dt

            'If dt.Rows.Count > 1 Then
            'lblrisu.Text = "Trovate - " & dt.Rows.Count & " segnalazioni"
            'ElseIf dt.Rows.Count = 1 Then
            'lblrisu.Text = "Trovata - " & dt.Rows.Count & " segnalazione"
            'ElseIf dt.Rows.Count = 0 Then
            'lblrisu.Text = "Nessuna segnalazione trovata"
            'End If


            'par.cmd.Dispose()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            TextBox3.Text = ex.Message

        End Try
    End Sub

    Private Sub EsportaExcel()
        isExporting.Value = "1"
        Dim context As RadProgressContext = RadProgressContext.Current
        context.SecondaryTotal = 0
        context.SecondaryValue = 0
        context.SecondaryPercent = 0
        DataGridSegnalaz.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx
        DataGridSegnalaz.ExportSettings.IgnorePaging = True
        DataGridSegnalaz.ExportSettings.ExportOnlyData = True
        DataGridSegnalaz.ExportSettings.OpenInNewWindow = True
        DataGridSegnalaz.MasterTableView.ExportToExcel()
    End Sub
    Protected Sub DataGridSegnalaz_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGridSegnalaz.ItemCreated
        If TypeOf e.Item Is GridFilteringItem And DataGridSegnalaz.IsExporting Then
            e.Item.Visible = False
        End If
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGridSegnalaz.AllowPaging = False
        DataGridSegnalaz.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGridSegnalaz.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In DataGridSegnalaz.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGridSegnalaz.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGridSegnalaz.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString

                i += 1
            End If
        Next
        Esporta(dtRecords)
        DataGridSegnalaz.AllowPaging = True
        DataGridSegnalaz.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGridSegnalaz.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SEGNALAZIONE", "SEGNALAZIONE", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Protected Sub brnRicerca_Click(sender As Object, e As System.EventArgs) Handles brnRicerca.Click
        Response.Redirect("RicercaSegnalazioni.aspx", True)
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Response.Redirect("../../pagina_home_ncp.aspx", True)
    End Sub

    Private Sub CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_RisultatiSegn_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class
