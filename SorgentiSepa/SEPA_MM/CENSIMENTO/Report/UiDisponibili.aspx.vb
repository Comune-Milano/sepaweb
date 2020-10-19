
Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class CENSIMENTO_UiDisponibili
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../../Portale.aspx""</script>")
            Exit Sub
        End If
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:300px; left:750px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then
            Cerca()
            Response.Flush()
        End If
    End Sub
    Private Sub Cerca()

        '********CONNESSIONE APERTURA ******************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Try

            Dim AGG As String = ""
            Dim AGG1 As String = ""
            If Request.QueryString("T") = "1" Then
                AGG = "AND TIPOLOGIA_UNITA_IMMOBILIARI.COD='AL' "
            End If

            If Request.QueryString("T") = "2" Then
                AGG = "AND TIPOLOGIA_UNITA_IMMOBILIARI.COD<>'AL' "
            End If

            If Request.QueryString("F") <> "-1" Then
                AGG1 = "AND TAB_FILIALI.ID=" & Request.QueryString("F")
            End If

            par.cmd.CommandText = "SELECT " _
                                & "(select trim(to_char(costo_intervento,'999G999G990D99')) from UI_COSTO_INTERVENTO where data_operazione_ord=(select max(data_operazione_ord) from UI_COSTO_INTERVENTO UIC WHERE UIC.COD_UNITA_IMMOBILIARE=UI_COSTO_INTERVENTO.COD_UNITA_IMMOBILIARE) and cod_unita_immobiliare=unita_immobiliari.cod_unita_immobiliare) AS COSTO_INTERVENTO," _
                                & "ZONA_ALER.ZONA,PROGRAMMAZIONE_INTERVENTI.DESCRIZIONE AS PRGINTERVENTI,UNITA_IMMOBILIARI.ID,TIPO_DISPONIBILITA.DESCRIZIONE AS DISPONIBILITA, 'Agibile' as AGIBILITA,(CASE WHEN UNITA_STATO_MANUTENTIVO.tipo_riassegnabile = 0 THEN " _
                                & "'Riassegnabile Con Lavori' WHEN UNITA_STATO_MANUTENTIVO.tipo_riassegnabile = 1 THEN 'Riassegnabile Senza Lavori' WHEN UNITA_STATO_MANUTENTIVO.tipo_riassegnabile = 2 THEN 'Non Riassegnabile' " _
                                & "END) AS STATO,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA," _
                                & "replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE, " _
                                & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_DISP,(CASE (UNITA_STATO_MANUTENTIVO.FINE_LAVORI) WHEN 1 THEN 'SI' ELSE 'NO' END) AS FINE_LAVORI,DESTINAZIONI_USO_UI.DESCRIZIONE AS USO_UI, " _
                                & "COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS COMPLESSO,COMUNI_NAZIONI.NOME AS COMUNE," _
                                & "(SELECT MAX(TO_CHAR(TO_DATE(SUBSTR((DATA_ORA),0,8),'yyyymmdd'),'dd/mm/yyyy')) FROM SISCOM_MI.EVENTI_CONTRATTI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA AND RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND RAPPORTI_UTENZA.ID = EVENTI_CONTRATTI.ID_CONTRATTO AND EVENTI_CONTRATTI.COD_EVENTO='F179') AS DATA_DISDETTA_EVENTO, " _
                                & "(SELECT MAX(TO_CHAR(TO_DATE(SUBSTR((DATA_ORA),0,8),'yyyymmdd'),'dd/mm/yyyy')) FROM SISCOM_MI.EVENTI_CONTRATTI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA AND RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND RAPPORTI_UTENZA.ID = EVENTI_CONTRATTI.ID_CONTRATTO AND EVENTI_CONTRATTI.COD_EVENTO='F180') AS DATA_SLOGGIO_EVENTO, " _
                                & "(SELECT MAX(TO_CHAR(TO_DATE(SUBSTR((DATA_ORA),0,8),'yyyymmdd'),'dd/mm/yyyy')) FROM SISCOM_MI.EVENTI_CONTRATTI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA AND RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND RAPPORTI_UTENZA.ID = EVENTI_CONTRATTI.ID_CONTRATTO AND EVENTI_CONTRATTI.COD_EVENTO='F187') AS DATA_F_LAVORI_EVENTO, " _
                                & "(SELECT MAX(TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_RICONSEGNA,'yyyymmdd'),'dd/mm/yyyy')) FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE " _
                                & "WHERE(UNITA_CONTRATTUALE.ID_UNITA = UNITA_STATO_MANUTENTIVO.id_unita And RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO And UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE Is NULL)" _
                                & "AND SISCOM_MI.Getstatocontratto(RAPPORTI_UTENZA.ID)='CHIUSO' and rapporti_utenza.data_riconsegna= " _
                                & "(select max(data_riconsegna) from siscom_mi.rapporti_utenza,siscom_mi.UNITA_CONTRATTUALE " _
                                & "where(UNITA_CONTRATTUALE.ID_UNITA = UNITA_STATO_MANUTENTIVO.id_unita And RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO) " _
                                & "AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                & "AND SISCOM_MI.Getstatocontratto(RAPPORTI_UTENZA.ID)='CHIUSO')) as DATA_VISITA_SLOGGIO," _
                                & "(CASE (ALLOGGI.STATO) WHEN 10 THEN 'SI' ELSE 'NO' END) AS RISERVATA,(INDIRIZZI.DESCRIZIONE ||', '||INDIRIZZI.CIVICO||' cap.'|| INDIRIZZI.CAP) AS INDIRIZZO, TIPO_LIVELLO_PIANO.DESCRIZIONE AS " _
                                & "LIVELLO_PIANO ,(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA, UNITA_IMMOBILIARI.INTERNO, IDENTIFICATIVI_CATASTALI.SUPERFICIE_CATASTALE,IDENTIFICATIVI_CATASTALI.FOGLIO,IDENTIFICATIVI_CATASTALI.NUMERO,IDENTIFICATIVI_CATASTALI.SUB,TO_CHAR(TO_DATE(UNITA_STATO_MANUTENTIVO.DATA_S,'yyyymmdd'),'dd/mm/yyyy') " _
                                & "AS DATA_POST_SLOGGIO,TO_CHAR(TO_DATE(UNITA_STATO_MANUTENTIVO.DATA_PRE_SLOGGIO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PRE_SLOGGIO," _
                                & "TO_CHAR(TO_DATE(UNITA_STATO_MANUTENTIVO.DATA_CONSEGNA_CHIAVI,'yyyymmdd'),'dd/mm/yyyy') AS DATA_CONSEGNA_CHIAVI,(CASE WHEN EDIFICI.FL_PIANO_VENDITA = 0 THEN 'NO' ELSE 'SI' END)AS PIANO_VENDITA,TAB_QUARTIERI.NOME AS QUARTIERE, TAB_FILIALI.NOME AS FILIALE," _
                                & " (SELECT SUM(DIMENSIONI.VALORE) FROM SISCOM_MI.DIMENSIONI WHERE DIMENSIONI.COD_TIPOLOGIA='SUP_NETTA' AND UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE) AS SUP_NETTA, " _
                                & "(SELECT SUM(DIMENSIONI.VALORE) FROM SISCOM_MI.DIMENSIONI WHERE DIMENSIONI.COD_TIPOLOGIA='SUP_CONV' AND UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE) AS SUP_CONV, (SELECT SUM(DIMENSIONI.VALORE) FROM " _
                                & "SISCOM_MI.DIMENSIONI WHERE DIMENSIONI.COD_TIPOLOGIA='SUP_COMM' AND UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE) AS SUP_COMM," _
                                & " " _
                                & "'' AS StDepChiavi,TAB_STRUTTURE.DESCRIZIONE AS STCOMPETENTE,  " _
                                & "TO_CHAR(TO_DATE(UNITA_STATO_MANUTENTIVO.DATA_consegna_str,'yyyymmdd'),'dd/mm/yyyy') AS DATA_CONSEGNA_STR,TO_CHAR(TO_DATE(UNITA_STATO_MANUTENTIVO.DATA_RIPRESA_STR,'yyyymmdd'),'dd/mm/yyyy') AS DATA_RIPRESA_STR " _
                                & " FROM ZONA_ALER,SISCOM_MI.PROGRAMMAZIONE_INTERVENTI,SISCOM_MI.TAB_STRUTTURE,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.TIPO_DISPONIBILITA,SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.DESTINAZIONI_USO_UI," _
                                & "SISCOM_MI.IDENTIFICATIVI_CATASTALI,SISCOM_MI.UNITA_STATO_MANUTENTIVO, SISCOM_MI.TAB_QUARTIERI, SISCOM_MI.TAB_FILIALI,SEPA.COMUNI_NAZIONI, ALLOGGI " _
                                & "WHERE ZONA_ALER.COD = EDIFICI.ID_ZONA AND PROGRAMMAZIONE_INTERVENTI.ID (+)=UNITA_IMMOBILIARI.ID_PRG_EVENTI AND edifici.id<>1 and UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = 'LIBE' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD " _
                                & "AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE= COMUNI_NAZIONI.COD AND " _
                                & "UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND UNITA_IMMOBILIARI.ID_CATASTALE = IDENTIFICATIVI_CATASTALI.ID(+) AND UNITA_IMMOBILIARI.ID = UNITA_STATO_MANUTENTIVO.ID_UNITA(+) AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE = TAB_QUARTIERI.ID(+) " _
                                & "AND COMPLESSI_IMMOBILIARI.ID_FILIALE = TAB_FILIALI.ID(+) AND ALLOGGI.COD_ALLOGGIO(+) = UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " & AGG & " " & AGG1 & " " _
                                & " AND UNITA_STATO_MANUTENTIVO.ID_STRUTTURA_COMP=TAB_STRUTTURE.ID (+) AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = DESTINAZIONI_USO_UI.ID " _
                                & "ORDER BY INDIRIZZO ASC "


            '************* Condizione query eliminata (MTeresa) **************
            '"AND (unita_stato_manutentivo.data_s is not null) "


            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Session.Add("MIADT", dt)
                DataGridUnitDispo.DataSource = dt
                DataGridUnitDispo.DataBind()
                Label1.Text = "Totale Unità: " & dt.Rows.Count
            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('La ricerca non ha prodotto risultati!');</script>")
                Response.Write("<script language='javascript'> { self.close() }</script>")
            End If



            '********CONNESSIONE CHIUSURA ******************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



        Catch ex As Exception
            '********CONNESSIONE CHIUSURA ******************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message

        End Try

    End Sub


    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "Export_UD_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\..\FileTemp\" & sNomeFile & ".xls"))
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
                .SetColumnWidth(1, 1, 25)
                .SetColumnWidth(2, 2, 20)
                .SetColumnWidth(3, 3, 15)
                .SetColumnWidth(4, 4, 35)
                .SetColumnWidth(5, 5, 25)
                .SetColumnWidth(6, 6, 35)
                .SetColumnWidth(7, 7, 20)
                .SetColumnWidth(8, 8, 20)
                .SetColumnWidth(9, 9, 25)
                .SetColumnWidth(10, 10, 45)
                .SetColumnWidth(11, 11, 15)
                .SetColumnWidth(12, 12, 20)
                .SetColumnWidth(13, 13, 15)
                .SetColumnWidth(14, 14, 15)
                .SetColumnWidth(15, 15, 15)
                .SetColumnWidth(16, 16, 20)
                .SetColumnWidth(17, 17, 20)
                .SetColumnWidth(18, 18, 20)
                .SetColumnWidth(19, 19, 20)
                .SetColumnWidth(20, 20, 25)
                .SetColumnWidth(21, 21, 25)
                .SetColumnWidth(22, 22, 25)
                .SetColumnWidth(23, 23, 35)
                .SetColumnWidth(24, 24, 55)
                .SetColumnWidth(25, 25, 22)
                .SetColumnWidth(26, 26, 25)
                .SetColumnWidth(27, 27, 25)
                .SetColumnWidth(28, 28, 35)
                .SetColumnWidth(29, 29, 15)
                .SetColumnWidth(30, 30, 35)
                .SetColumnWidth(31, 31, 35)
                .SetColumnWidth(32, 32, 36)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "TIPOLOGIA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "STATO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "AGIBILITA'", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "ASSEGNABILITA'", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "COD. U.I.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "COMPLESSO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "FILIALE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "COMUNE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "QUARTIERE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "INDIRIZZO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "SCALA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "PIANO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "INTERNO", 0)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "FOGLIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "MAPPALE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "SUB", 0)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "SUP. NETTA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "SUP. CONV.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "SUP. CATAST.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "SUP. COMMERC.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "PIANO VENDITA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 22, "RISERVATA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 23, "DATA PRE-SLOGGIO", 0)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 24, "DATA POST-SLOGGIO", 0)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 25, "DATA SLOGGIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 26, "ST.DEPOSITARIA CHIAVI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 27, "ST.COMP. IN CASO DI LAVORI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 28, "DATA CONSEGNA STR.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 29, "DATA RIPRESA STR.", 0)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 30, "DESTINAZIONE USO U.I.", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 31, "DATA DISPONIBILITA' PRESUNTA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 32, "FINE LAVORI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 33, "DATA INSERIM. (data disdetta)", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 34, "DATA INSERIM. (data sloggio)", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 35, "DATA INSERIM. (data fine lavori)", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 36, "PROGR. INTERVENTI", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 37, "ZONA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 38, "COSTO_INTERVENTO", 0)
                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPOLOGIA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DISPONIBILITA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("AGIBILITA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("STATO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(Left(Right(par.IfNull(dt.Rows(i).Item("COD_UNITA_IMMOBILIARE"), ""), 21), 17)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COMPLESSO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FILIALE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COMUNE"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("QUARTIERE"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SCALA"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("LIVELLO_PIANO"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO"), " ")))

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FOGLIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NUMERO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SUB"), "")))

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SUP_NETTA"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SUP_CONV"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SUPERFICIE_CATASTALE"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SUP_COMM"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO_VENDITA"), " ")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RISERVATA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_PRE_SLOGGIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_POST_SLOGGIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 25, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_VISITA_SLOGGIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 26, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("STDEPCHIAVI"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 27, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("STCOMPETENTE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 28, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_CONSEGNA_STR"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 29, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_RIPRESA_STR"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 30, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("USO_UI"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 31, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_DISP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 32, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FINE_LAVORI"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 33, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_DISDETTA_EVENTO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 34, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_SLOGGIO_EVENTO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 35, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_F_LAVORI_EVENTO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 36, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PRGINTERVENTI"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 37, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ZONA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 38, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COSTO_INTERVENTO"), "")))
                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\..\FileTemp\" & sNomeFile & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            '
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
            Response.Redirect("..\..\FileTemp\" & sNomeFile & ".zip")


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
