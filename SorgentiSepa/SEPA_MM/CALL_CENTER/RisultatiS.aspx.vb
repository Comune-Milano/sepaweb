
Partial Class CALL_CENTER_RisultatiS
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        Response.Expires = 0
        If IsPostBack = False Then
            Response.Flush()

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
            If Not IsNothing(Request.QueryString("NUM")) Then
                numero.Value = Request.QueryString("NUM")
            End If
            If Not IsNothing(Request.QueryString("URG")) Then
                urgenza.Value = Request.QueryString("URG")
            End If
            If Request.QueryString("C") = "1" Then
                origine.Value = " AND SEGNALAZIONI.ID_STATO=-1 and segnalazioni.origine='C'"
            Else
                origine.Value = " AND SEGNALAZIONI.ID_STATO<>-1 "
            End If
            If Not IsNothing(Request.QueryString("PROV")) Then
                prov.Value = Request.QueryString("PROV")
            End If
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Try

            'par.OracleConn.Open()
            ' par.SettaCommand(par)

            Dim dt As New Data.DataTable

            Dim s As String = ""

            If tipo1.Value <> "-1" Then
                s = " AND id_tipologie=" & tipo1.Value
            End If

            If numero.Value <> "-1" And numero.Value <> "" Then
                s &= " AND segnalazioni.id=" & numero.Value
            End If

            If TipoSegnalazione.Value = "1" Then

                Select Case UCase(urgenza.Value)
                    Case "---"
                        urgenza.Value = "-1"
                    Case "BIANCO"
                        urgenza.Value = "0"
                    Case "VERDE"
                        urgenza.Value = "1"
                    Case "GIALLO"
                        urgenza.Value = "2"
                    Case "ROSSO"
                        urgenza.Value = "3"
                    Case "BLU"
                        urgenza.Value = "4"
                End Select
                If urgenza.Value <> "-1" Then
                    s &= " AND segnalazioni.id_pericolo_Segnalazione=" & urgenza.Value
                End If
            End If

            If TipoSegnalazione.Value <> "-1" Then
                s &= " AND id_TIPO_SEGNALAZIONE=" & TipoSegnalazione.Value
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
                s += "AND SEGNALAZIONI.ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO=" & complesso.Value & ") "
            End If
            If Trim(segnalante.Value) <> "" Then
                Dim listaSegnalante As String()
                listaSegnalante = segnalante.Value.ToString.Split(" ")

                s += " AND ("
                If listaSegnalante.Length = 1 Then
                    s += " upper(SEGNALAZIONI.COGNOME_RS) = '" & par.PulisciStrSql(UCase(listaSegnalante(0))) & "' "
                    s += " OR upper(SEGNALAZIONI.NOME) = '" & par.PulisciStrSql(UCase(listaSegnalante(0))) & "' "
                Else
                    s += "("
                    For i As Integer = 0 To listaSegnalante.Length - 1 Step 1
                        If i = 0 Then
                            s += " upper(SEGNALAZIONI.COGNOME_RS) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
                        Else
                            s += " OR upper(SEGNALAZIONI.COGNOME_RS) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
                        End If
                    Next
                    s += ") AND ("
                    For i As Integer = 0 To listaSegnalante.Length - 1 Step 1
                        If i = 0 Then
                            s += " upper(SEGNALAZIONI.NOME) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
                        Else
                            s += " OR upper(SEGNALAZIONI.NOME) = '" & par.PulisciStrSql(UCase(listaSegnalante(i))) & "' "
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

            Dim ordine As String = ""
            If Not IsNothing(Request.QueryString("ORD")) Then
                ordine = Request.QueryString("ORD")
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
                                & "tab_stati_segnalazioni.descrizione AS stato, " _
                                & "edifici.denominazione AS indirizzo, " _
                                & "cognome_rs || ' ' || segnalazioni.nome AS richiedente, " _
                                & "TELEFONO1 AS TELEFONO_richiedente, " _
                                & " (case when segnalazioni.id_unita is not null  " _
                                & " THEN " _
                                & " (case when  (select count(rapporti_utenza.cod_contratto) from siscom_mi.rapporti_utenza where SUBSTR (data_ora_richiesta, 1, 8) between rapporti_utenza.data_Decorrenza and nvl(rapporti_utenza.data_riconsegna,'30000000') and id in (select id_contratto from siscom_mi.unita_contrattuale where unita_contrattuale.id_unita=segnalazioni.id_unita " _
                                & " and unita_contrattuale.id_unita_principale is null and bozza=0 " _
                                & " ))=1 " _
                                & " then  " _
                                & " (select rapporti_utenza.cod_contratto from siscom_mi.rapporti_utenza where SUBSTR (data_ora_richiesta, 1, 8) between rapporti_utenza.data_Decorrenza and nvl(rapporti_utenza.data_riconsegna,'30000000') and id in (select id_contratto from siscom_mi.unita_contrattuale where unita_contrattuale.id_unita=segnalazioni.id_unita " _
                                & " and unita_contrattuale.id_unita_principale is null and bozza=0 " _
                                & " )) " _
                                & " else null end  " _
                                & ") " _
                                & " else null end ) " _
                                & " AS OGGETTO, " _
                                & "TO_CHAR (TO_DATE (SUBSTR (data_ora_richiesta, 1, 8), 'YYYYmmdd'),'DD/MM/YYYY') AS data_inserimento, " _
                                & "REPLACE (segnalazioni.descrizione_ric, '''', '') AS descrizione, " _
                                & "/*segnalazioni.descrizione_ric,*/ " _
                                & "NVL (siscom_mi.tab_filiali.nome, ' ') AS filiale, " _
                                & "(CASE WHEN ID_STATO = 10 THEN (SELECT max(NOTE) FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                                & "ID_TIPO_sEGNALAZIONE_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND ID_TIPO_SEGNALAZIONE_note=2)" _
                                & ") ELSE '' END) AS NOTE_C " _
                                & ",(case when length(segnalazioni.data_chiusura)<8 then null else to_char(to_Date(substr(segnalazioni.data_chiusura,1,8),'yyyyMMdd'),'dd/MM/yyyy') end) as data_chiusura " _
                                & ",(select to_char(to_Date(min(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE),'yyyyMMdd'),'dd/MM/yyyy') from siscom_mi.manutenzioni where SISCOM_MI.MANUTENZIONI.ID_SEGNALAZIONI=segnalazioni.id) as data_ordine " _
                                & ",id_pericolo_Segnalazione " _
                                & "FROM siscom_mi.tab_stati_segnalazioni, " _
                                & "siscom_mi.segnalazioni, " _
                                & "siscom_mi.tab_filiali, " _
                                & "siscom_mi.edifici, " _
                                & "siscom_mi.unita_immobiliari, " _
                                & "siscom_mi.TIPOLOGIE_GUASTI, " _
                                & "OPERATORI " _
                                & "WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
                                & "AND segnalazioni.id_stato <> -1 " _
                                & "AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                                & "AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
                                & "AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
                                & "AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
                                & "AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " & s _
                                & condizioneOrdinamento



            'par.cmd.CommandText = "select COGNOME_RS||' '||SEGNALAZIONI.NOME AS RICHIEDENTE,CASE WHEN SISCOM_MI.SEGNALAZIONI.ID_UNITA is not null THEN SISCOM_MI.SEGNALAZIONI.id_unita WHEN SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO is not null THEN SISCOM_MI.SEGNALAZIONI.id_edificio WHEN SISCOM_MI.SEGNALAZIONI.ID_COMPLESSO is not null THEN SISCOM_MI.SEGNALAZIONI.id_complesso END AS identificativo,CASE WHEN SISCOM_MI.SEGNALAZIONI.ID_UNITA is not null THEN 'U'" _
            '                                                  & "WHEN SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO is not null THEN 'E' WHEN SISCOM_MI.SEGNALAZIONI.ID_COMPLESSO is not null THEN 'C' END AS TIPOS,substr(replace(segnalazioni.DESCRIZIONE_RIC,'''',''),1,30)||'...' as des, DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'INFORMAZIONI',1,'GUASTI') AS TIPO,Segnalazioni.id," _
            '                                                  & "TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INSERIMENTO,segnalazioni.DESCRIZIONE_RIC,tab_stati_segnalazioni.descrizione as stato,SISCOM_MI.TAB_FILIALI.NOME AS FILIALE, (CASE WHEN siscom_mi.segnalazioni.id_complesso IS NOT NULL THEN COMPLESSI_IMMOBILIARI.denominazione ELSE EDIFICI.DENOMINAZIONE END) AS INDIRIZZO,(OPERATORI.cognome || ' ' || OPERATORI.nome) AS segnalante FROM siscom_mi.tab_stati_segnalazioni,SISCOM_MI.SEGNALAZIONI,SISCOM_MI.TAB_FILIALI," _
            '                                                  & "SISCOM_MI.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari,OPERATORI where " & s & " tab_stati_segnalazioni.id=segnalazioni.id_stato " & origine.Value & " and siscom_mi.complessi_immobiliari.id_filiale  = siscom_mi.tab_filiali.id(+) " _
            '                                                  & "and siscom_mi.segnalazioni.id_complesso = siscom_mi.complessi_immobiliari.id(+) and siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.id(+) and siscom_mi.unita_immobiliari.id(+)= siscom_mi.segnalazioni.id_unita AND OPERATORI.ID = SEGNALAZIONI.ID_OPERATORE_INS ORDER BY tab_stati_segnalazioni.id desc,data_ora_richiesta desc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)

            da.Fill(dt)
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


            DataGridSegnalaz.DataSource = dt
            DataGridSegnalaz.DataBind()

            Session.Item("DataGridSegnalaz") = dt

            If dt.Rows.Count > 1 Then
                lblrisu.Text = "Trovate - " & dt.Rows.Count & " segnalazioni"
            ElseIf dt.Rows.Count = 1 Then
                lblrisu.Text = "Trovata - " & dt.Rows.Count & " segnalazione"
            ElseIf dt.Rows.Count = 0 Then
                lblrisu.Text = "Nessuna segnalazione trovata"
            End If


            'par.cmd.Dispose()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            TextBox3.Text = ex.Message

        End Try
    End Sub



    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaS.aspx""</script>")
    End Sub

    Protected Sub DataGridSegnalaz_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridSegnalaz.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------  
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")
            e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='red';document.getElementById('identificativo').value='" & e.Item.Cells(3).Text & "';document.getElementById('tipo').value='" & e.Item.Cells(2).Text & "';document.getElementById('TextBox3').value='Hai selezionato la segnalazione N°" & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
            e.Item.Attributes.Add("onDblclick", "Apri();")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='red';document.getElementById('identificativo').value='" & e.Item.Cells(3).Text & "';document.getElementById('tipo').value='" & e.Item.Cells(2).Text & "';document.getElementById('TextBox3').value='Hai selezionato la segnalazione N°" & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
            e.Item.Attributes.Add("onDblclick", "Apri();")
        End If

        If e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO")).Text = "TECNICA" Then
            Select Case e.Item.Cells(par.IndDGC(DataGridSegnalaz, "ID_PERICOLO_SEGNALAZIONE")).Text
                Case "0"
                    e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                        & "<tr><td><img src=""Immagini/Ball-white-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                        & "<td>" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text & "</td></<tr></table>"
                Case "1"
                    e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                        & "<tr><td><img src=""Immagini/Ball-green-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                        & "<td>" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text & "</td></<tr></table>"
                Case "2"
                    e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                        & "<tr><td><img src=""Immagini/Ball-yellow-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                        & "<td>" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text & "</td></<tr></table>"
                Case "3"
                    e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                        & "<tr><td><img src=""Immagini/Ball-red-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                        & "<td>" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text & "</td></<tr></table>"
                Case "4"
                    e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                        & "<tr><td><img src=""Immagini/Ball-blue-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                        & "<td>" & e.Item.Cells(par.IndDGC(DataGridSegnalaz, "TIPO_INT")).Text & "</td></<tr></table>"
                Case Else
            End Select
        End If

    End Sub

    Protected Sub DataGridSegnalaz_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridSegnalaz.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridSegnalaz.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub



    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        'Response.Write("<script>window.open('../FileTemp/" & par.EsportaExcelDaDTWithDatagrid(CType(Session.Item("DataGridSegnalaz"), Data.DataTable), Me.DataGridSegnalaz, "ExportSegnalazioni") & "')</script>")
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDataGridWithDTColor(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", DataGridSegnalaz, CType(Session.Item("DataGridSegnalaz"), Data.DataTable), True, , , "TIPO INT.")
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            Response.Redirect("../FileTemp/" & nomeFile, False)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
        End If
    End Sub
End Class

