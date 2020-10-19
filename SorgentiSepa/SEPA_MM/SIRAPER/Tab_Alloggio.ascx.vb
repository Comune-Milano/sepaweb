Imports System.IO

Partial Class SIRAPER_Tab_Alloggio
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione
    Dim xls As New ExcelSiSol

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        par = CType(Me.Page, Object).par
        Me.connData = CType(Me.Page, Object).connData
        If Not IsNothing(Me.connData) Then
            Me.connData.RiempiPar(par)
            'par.cmd.Transaction = connData.Transazione
        End If
        If Not IsPostBack Then
            If CType(Me.Page.FindControl("MasterPage$MainContent$Elaborazione"), HiddenField).Value = 1 Then
                CaricaDataGridAlloggio()
            End If
        End If
    End Sub
    Public Sub CaricaDataGridAlloggio()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            par.cmd.CommandText = "SELECT DISTINCT SIR_ALLOGGIO.ID, NVL(CANONE_SOCIALE, 0) AS CANONE_SOCIALE, ROWNUM, SIR_ALLOGGIO.CODICE_MIR, " _
                                & "'<a href=""javascript:window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='|| SIR_ALLOGGIO.ID ||''',''_blank'',''resizable=no,height=620,top=0,left=0,width=800,scrollbars=no'');void(0);"">'||SIR_ALLOGGIO.CODICE_ALLOGGIO||'</a>' AS CODICE, " _
                                & "COD_EDIFICIO || ' - ' || DENOMINAZIONE AS EDIFICIO, " _
                                & "(CASE WHEN SIR_ALLOGGIO.ID_CONTRATTO IS NOT NULL THEN '<a href=""javascript:window.open(''../Contratti/Contratto.aspx?LT=1&ID='|| SIR_ALLOGGIO.ID_CONTRATTO ||''',''_blank'',''resizable=no,height=750,top=0,left=0,width=900,scrollbars=no'');void(0);"">'|| 'Dettagli Contratto' ||'</a>' ELSE '' END) AS CONTRATTO " _
                                & "FROM SISCOM_MI.SIR_ALLOGGIO, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI " _
                                & "WHERE ID_SIRAPER = " & CType(Me.Page.FindControl("MasterPage$MainContent$idSiraper"), HiddenField).Value & " " _
                                & "AND SIR_ALLOGGIO.ID = UNITA_IMMOBILIARI.ID AND EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_EDIFICIO " _
                                & "ORDER BY 5, 4 ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            BindGridAlloggio(dt)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Alloggio - CaricaDataGridAlloggio - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Public Sub SolaLetturaDatagrid()
        Try
            For Each Items As DataGridItem In dgvAlloggi.Items
                FrmSolaLettura(Items)
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Alloggio - SolaLetturaDatagrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Public Sub BindGridAlloggio(ByVal dtAlloggio As Data.DataTable)
        Try
            dgvAlloggi.DataSource = dtAlloggio
            dgvAlloggi.DataBind()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Alloggio - BindGridAlloggio - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('frmModify').value='1';")
            End If
        Next
    End Sub
    Public Sub FrmSolaLettura(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                FrmSolaLettura(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Enabled = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Enabled = False
            End If
        Next
    End Sub
    Protected Sub dgvAlloggi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvAlloggi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            CType(e.Item.FindControl("btnDettaglioSirAlloggio"), ImageButton).OnClientClick = "caricamentoincorso();ApriDettaglioSirAlloggio(" & e.Item.Cells(0).Text & ");"
        End If
    End Sub
    Protected Sub dgvAlloggi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvAlloggi.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            dgvAlloggi.CurrentPageIndex = e.NewPageIndex
            CaricaDataGridAlloggio()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "div", "if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
        End If
    End Sub
    Protected Sub btnExportXlsAlloggio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportXlsAlloggio.Click
        Try
            EsportaExcel()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Fabbricato - btnExportXlsAlloggio_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Public Function EsportaQuery() As String
        Try
            EsportaQuery = "SELECT DISTINCT ROWNUM AS RIGA, CODICE_MIR, SIR_ALLOGGIO.CODICE_ALLOGGIO, (SELECT COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = SIR_ALLOGGIO.ID_CONTRATTO) AS CONTRATTO, " _
                         & "COD_FISCALE_ENTE AS CODICE_FISCALE_ENTE, P_IVA_ENTE AS PARTITA_IVA_ENTE, SIGLA_ENTE, TIPO_ENTE.DESCRIZIONE AS TIPO_ENTE, TIPO_AMMINISTRAZIONE_ENTE.DESCRIZIONE AS TIPO_AMMINISTRAZIONE, " _
                         & "TIPO_DISMESSO_ALLOGGIO.DESCRIZIONE AS ALLOGGIO, TRIM (TO_CHAR (PROV_DISM_CART, '9G999G999G999G999G990D99')) AS PROVENTI_DISMISSIONE, (CASE WHEN ALL_RISCATTO = 1 THEN 'SI' ELSE 'NO' END) AS ALLOGGIO_A_RISCATTO, " _
                         & "SEZIONE, FOGLIO, MAPPALE, SUBALTERNO, (NVL(T_TIPO_VIA.DESCRIZIONE, 'VIA') || ' ' || NOME_VIA || ', ' || NUMERO_CIVICO || (CASE WHEN ESPONENTE IS NOT NULL THEN ', ' || ESPONENTE ELSE '' END)) AS INDIRIZZO, " _
                         & "(NOME_LOCALITA || ' - ' || CAP) AS LOCALITA, CORD_GAUSS_X AS CORDINATA_GAUSS_X, CORD_GAUSS_Y AS CORDINATA_GAUSS_Y, TIPO_OCC_ALL_SIRAPER.DESCRIZIONE AS ALLOGGIO_OCCUPATO, TIPO_GODIMENTO_SIRAPER.DESCRIZIONE AS TIPO_GODIMENTO, " _
                         & "(CASE WHEN ALL_ESCLUSO = 1 THEN 'SI' ELSE 'NO' END) AS ALLOGGIO_ESCLUSO, CATEGORIA_CATASTALE.DESCRIZIONE AS CATEGORIA_CATASTALE, TRIM (TO_CHAR (RENDITA_CATASTALE, '9G999G999G999G999G990D99')) AS RENDITA_CATASTALE, " _
                         & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_COEFF_UBICAZIONE_EDIFICIO WHERE COD = SIR_ALLOGGIO.COEFF_CLASSE_DEMOGRAFICA_ANTE) AS COEFF_CLASSE_DEMOGRAFICA_ANTE, " _
                         & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_COEFF_UBICAZIONE_EDIFICIO WHERE COD = SIR_ALLOGGIO.COEFF_CLASSE_DEMOGRAFICA_LR) AS COEFF_CLASSE_DEMOGRAFICA_LR, ANNO_RISTRUTTURAZIONE, NUMERO_STANZE, " _
                         & "(CASE WHEN TIPO_INTERVENTO = 'NC' THEN 'NUOVA COSTRUZIONE' WHEN TIPO_INTERVENTO = 'RE' THEN 'RECUPERO/RISTRUTTURAZIONE STRAORDINARIA' ELSE '' END) AS TIPO_INTERVENTO, " _
                         & "TIPO_LIVELLO_PIANO_SIRAPER.DESCRIZIONE AS PIANO_ALLOGGIO, COEFF_PIANO AS COEFFICENTE_PIANO, TRIM (TO_CHAR (SUP_UTILE_ALLOGGIO, '9G999G999G999G999G990D99')) AS SUP_UTILE_ALLOGGIO, " _
                         & "TRIM (TO_CHAR (SUP_CANTINE_SOFF, '9G999G999G999G999G990D99')) AS SUP_CANTINE_SOFF, TRIM (TO_CHAR (SUP_BALCONI, '9G999G999G999G999G990D99')) AS SUP_BALCONI, " _
                         & "TRIM (TO_CHAR (SUP_AREA_PRIVATA, '9G999G999G999G999G990D99')) AS SUP_AREA_PRIVATA, TRIM (TO_CHAR (SUP_VERDE_COND, '9G999G999G999G999G990D99')) AS SUP_VERDE_COND, " _
                         & "TRIM (TO_CHAR (SUP_BOX, '9G999G999G999G999G990D99')) AS SUP_BOX, NVL (NUM_BOX, 0) AS NUM_BOX, TRIM (TO_CHAR (SUP_POSTO_AUTO, '9G999G999G999G999G990D99')) AS SUP_POSTO_AUTO, " _
                         & "TRIM (TO_CHAR (SUP_PERTINENZE, '9G999G999G999G999G990D99')) AS SUP_PERTINENZE, TRIM (TO_CHAR (SUP_CONVENZIONALE_ANTE, '9G999G999G999G999G990D99')) AS SUP_CONVENZIONALE_ANTE, " _
                         & "TRIM (TO_CHAR (SUP_CONVENZIONALE_LR, '9G999G999G999G999G990D99')) AS SUP_CONVENZIONALE_LR, (CASE WHEN RISCALDAMENTO = 1 THEN 'CENTRALIZZATO' WHEN RISCALDAMENTO = 2 THEN 'AUTONOMO' WHEN RISCALDAMENTO = 3 THEN 'ASSENTE' ELSE 'NON RILEVATO' END) AS RISCALDAMENTO, " _
                         & "(CASE WHEN ASCENSORE = 1 THEN 'SI' ELSE 'NO' END) AS ASCENSORE, (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_CONSERVAZIONE_SIRAPER WHERE COD = NVL(SIR_ALLOGGIO.STATO_CONS_ACCESSI, 0)) AS STATO_CONS_ACCESSI, " _
                         & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_CONSERVAZIONE_SIRAPER WHERE COD = NVL(SIR_ALLOGGIO.STATO_CONS_ACCESSI, 0)) AS STATO_CONS_ACCESSI, (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_CONSERVAZIONE_SIRAPER WHERE COD = NVL(SIR_ALLOGGIO.STATO_CONS_FACCIATA, 0)) AS STATO_CONS_FACCIATA, " _
                         & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_CONSERVAZIONE_SIRAPER WHERE COD = NVL(SIR_ALLOGGIO.STATO_CONS_PAV, 0)) AS STATO_CONS_PAV, (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_CONSERVAZIONE_SIRAPER WHERE COD = NVL(SIR_ALLOGGIO.STATO_CONS_PARETI, 0)) AS STATO_CONS_PARETI, " _
                         & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_CONSERVAZIONE_SIRAPER WHERE COD = NVL(SIR_ALLOGGIO.STATO_CONF_INFISSI, 0)) AS STATO_CONF_INFISSI, (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_CONSERVAZIONE_SIRAPER WHERE COD = NVL(SIR_ALLOGGIO.STATO_CONF_IMP_ELE, 0)) AS STATO_CONF_IMP_ELE, " _
                         & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_CONSERVAZIONE_SIRAPER WHERE COD = NVL(SIR_ALLOGGIO.STATO_CONS_IMP_IDR, 0)) AS STATO_CONS_IMP_IDR,  (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_CONSERVAZIONE_SIRAPER WHERE COD = NVL(SIR_ALLOGGIO.STATO_CONS_IMP_RISC, 0)) AS STATO_CONS_IMP_RISC, " _
                         & "TIPO_CONSERVAZIONE_SIRAPER.DESCRIZIONE AS STATO_CONS_ALL, (CASE WHEN TIPO_CUCINA = 0 THEN 'NON RILEVATO' WHEN TIPO_CUCINA = 1 THEN 'SUP. INFERIORE AD 8 MQ' WHEN TIPO_CUCINA = 2 THEN 'SUP. SUPERIORE AD 8 MQ' WHEN TIPO_CUCINA = 3 THEN 'CUCINA ASSENTE' ELSE '' END) AS TIPO_CUCINA, " _
                         & "(CASE WHEN BARR_ARCH = 1 THEN 'SI' WHEN BARR_ARCH = 2 THEN 'NO' ELSE 'NON RILEVATO' END) AS BARRIERE_ARCH, TRIM (TO_CHAR (COSTO_BASE_MQ, '9G999G999G999G999G990D99')) AS COSTO_BASE_MQ, " _
                         & "PER_ISTAT_AGG AS PERCENTUALE_ISTAT_AGG, TRIM (TO_CHAR (CANONE_BASE, '9G999G999G999G999G990D99')) AS CANONE_BASE, TRIM (TO_CHAR (CANONE_IND_ANN, '9G999G999G999G999G990D99')) AS CANONE_IND_ANN, " _
                         & "PERC_APPL AS PERCENTUALE_APPLICATA, FASC_APPARTENENZA AS FASCIA_DI_APPARTENENZA, TIPO_AREA_APPARTENENZA.DESCRIZIONE AS AREA_APPARTENENZA, TRIM (TO_CHAR (CANONE_APP_ANN, '9G999G999G999G999G990D99')) AS CANONE_APPLICATO_ANNUALE, " _
                         & "PER_ISTAT_LEGGE27 AS PERCENTUALE_ISTAT_LEGGE27, TIPO_CANONE_SIRAPER.DESCRIZIONE AS TIPO_CANONE, TRIM (TO_CHAR (CANONE_ANN_ANTE, '9G999G999G999G999G990D99')) AS CANONE_ANN_ANTE, " _
                         & "TRIM (TO_CHAR (CANONE_ANN_REG, '9G999G999G999G999G990D99')) AS CANONE_ANN_REG, VALORE_CONVENZIONALE, COSTO_CONVENZIONALE, CARATT_UNITA_AB AS CARATTER_UNITA_ABITATIVA, " _
                         & "(CASE WHEN REDD_PREV_DIP = 1 THEN 'SI' ELSE 'NO' END) AS REDD_PREV_DIPENDENTE, (CASE WHEN ABBATTIMENTO_CANONE = 1 THEN 'SI' WHEN ABBATTIMENTO_CANONE = 2 THEN 'NO' ELSE '' END) AS ABBATTIMENTO_CANONE, " _
                         & "(CASE WHEN SOVRAPREZZO_DECADENZA = 1 THEN 'SI' WHEN SOVRAPREZZO_DECADENZA = 2 THEN 'NO' ELSE '' END) AS SOVRAPREZZO_DECADENZA, PERC_AGG_AREA_DEC AS PERCENTUALE_AGG_AREA_DEC, " _
                         & "(CASE WHEN SOVRAPREZZO_SOTTOUTILIZZO = 1 THEN 'SI' WHEN SOVRAPREZZO_SOTTOUTILIZZO = 2 THEN 'NO' ELSE '' END) AS SOVRAPREZZO_SOTTOUTILIZZO, " _
                         & "(CASE WHEN REDD_DIP = 1 THEN 'SI' ELSE 'NO' END) AS REDD_DIPENDENTE, NUM_BOX_CONTR_SEP AS NUM_BOX_CONTRATTO_SEPARATO, TRIM (TO_CHAR (CANONE_BOX_CONTR_SEP, '9G999G999G999G999G990D99')) AS CANONE_BOX_CONTRATTO_SEPARATO, " _
                         & "(CASE WHEN CONTAB_UNICA = 1 THEN 'SI' WHEN CONTAB_UNICA = 2 THEN 'NO' ELSE '' END) AS CONTABILITA_UNICA, TRIM (TO_CHAR (GETTITO_CONTAB_UNICA, '9G999G999G999G999G990D99')) AS GETTITO_CONTABILITA_UNICA, " _
                         & "NVL (NUM_PERSONE_INV100_CON, 0) AS NUM_PERSONE_INV100_CON_ACCOM, NVL (NUM_PERSONE_INV100_SENZA, 0) AS NUM_PERSONE_INV100_SENZA_ACCOM, NVL (NUM_PERSONE_INV_67_99, 0) AS NUM_PERSONE_INV_67_99, " _
                         & "NVL (SPESE_PERSONE_INV100_CON, 0) AS SPESE_PERSONE_INV100_CON_ACCOM, TIPO_STATO_AGG_NUCLEO_SIRAPER.DESCRIZIONE AS STATO_AGG_NUCLEO, TO_CHAR (TO_DATE (DATA_CALCOLO_ISEE, 'YYYYmmdd'), 'DD-MM-YYYY') AS DATA_CALCOLO_ISEE, " _
                         & "NVL (ISR, 0) AS ISR, NVL (ISP, 0) AS ISP, NVL (PSE, 0) AS PSE, NVL (ISE_ERP, 0) AS ISE_ERP, NVL (ISEE_ERP, 0) AS ISEE_ERP, NVL (ISE_ERP_ASS, 0) AS ISE_ERP_ASS, NVL (ISEE_ERP_ASS, 0) AS ISEE_ERP_ASS, " _
                         & "NVL (REDD_DIP_ASS, 0) AS REDD_DIP_ASS, NVL (ALTRI_REDD, 0) AS ALTRI_REDD, NVL (VALORE_LOCATIVO, 0) AS VALORE_LOCATIVO, TRIM (TO_CHAR (VALORE_MERCATO, '9G999G999G999G999G990D99')) AS VALORE_MERCATO, " _
                         & "NVL (COEFF_VETUSTA, 0) AS COEFF_VETUSTA, NVL (NUM_COMPONENTI, 0) AS NUMERO_COMPONENTI, ANNO_VETUSTA, (NVL (PERC_VAL_LOCATIVO, 0) || ' %') AS PERC_VAL_LOCATIVO, " _
                         & "TAB_CLASSI_ISEE,  (CASE WHEN INV_SOCIALE = 1 THEN 'SI' WHEN INV_SOCIALE = 2 THEN 'NO' ELSE '' END) AS INVALIDITA_SOCIALE, ISEE_PRON_DECADENZA, TO_CHAR (TO_DATE (DATA_DISPONIBILITA, 'YYYYmmdd'), 'DD-MM-YYYY') AS DATA_DISPONIBILITA, " _
                         & "TO_CHAR (TO_DATE (DATA_ASSEGNAZIONE, 'YYYYmmdd'), 'DD-MM-YYYY') AS DATA_ASSEGNAZIONE, TRIM (TO_CHAR (VALORE_PATRIMONIALE, '9G999G999G999G999G990D99')) AS VALORE_PATRIMONIALE, " _
                         & "NVL (MOROSITA_ATTUALE_FAM, 0) AS MOROSITA_FAMIGLIA_ATTUALE, NVL (MOROSITA_PREC_FAM, 0) AS MOROSITA_FAMIGLIA_PRECEDENTE " _
                         & "FROM SISCOM_MI.SIR_ALLOGGIO, SISCOM_MI.TIPO_ENTE, SISCOM_MI.TIPO_AMMINISTRAZIONE_ENTE, SISCOM_MI.TIPO_DISMESSO_ALLOGGIO, T_TIPO_VIA, SISCOM_MI.TIPO_LIVELLO_PIANO_SIRAPER, SISCOM_MI.TIPO_OCC_ALL_SIRAPER, SISCOM_MI.TIPO_GODIMENTO_SIRAPER, SISCOM_MI.TIPO_AREA_APPARTENENZA, SISCOM_MI.TIPO_CANONE_SIRAPER, " _
                         & "SISCOM_MI.TIPO_STATO_AGG_NUCLEO_SIRAPER, SISCOM_MI.CATEGORIA_CATASTALE, SISCOM_MI.TIPO_CONSERVAZIONE_SIRAPER " _
                         & "WHERE TIPO_ENTE.COD(+) = SIR_ALLOGGIO.TIPO_ENTE AND TIPO_AMMINISTRAZIONE_ENTE.COD(+) = SIR_ALLOGGIO.TIPO_AMMINISTRAZIONE AND TIPO_DISMESSO_ALLOGGIO.COD(+) = SIR_ALLOGGIO.ALL_DISM_CART AND TIPO_STATO_AGG_NUCLEO_SIRAPER.COD(+) = SIR_ALLOGGIO.STATO_AGG_NUCLEO " _
                         & "AND T_TIPO_VIA.COD_SIRAPER(+) = SIR_ALLOGGIO.PREFISSO_INDIRIZZO AND TIPO_LIVELLO_PIANO_SIRAPER.COD(+) = SIR_ALLOGGIO.PIANO_ALLOGGIO  AND TIPO_OCC_ALL_SIRAPER.COD(+) = SIR_ALLOGGIO.ALL_OCCUPATO AND TIPO_GODIMENTO_SIRAPER.COD(+) = SIR_ALLOGGIO.TIPO_GODIMENTO " _
                         & "AND TIPO_AREA_APPARTENENZA.COD(+) = SIR_ALLOGGIO.AREA_APPARTENENZA AND TIPO_CANONE_SIRAPER.COD(+) = SIR_ALLOGGIO.TIPO_CANONE AND CATEGORIA_CATASTALE.COD(+) = SIR_ALLOGGIO.CATEGORIA_CATASTALE AND TIPO_CONSERVAZIONE_SIRAPER.COD(+) = SIR_ALLOGGIO.STATO_CONS_ALL " _
                         & "AND ID_SIRAPER = " & CType(Me.Page.FindControl("MasterPage$MainContent$idSiraper"), HiddenField).Value & " " _
                         & "ORDER BY ROWNUM"
        Catch ex As Exception
            EsportaQuery = ""
        End Try
    End Function
    Private Sub EsportaExcel()
        Try
            If dgvAlloggi.Items.Count > 0 Then
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                    'par.cmd.Transaction = connData.Transazione
                End If
                par.cmd.CommandText = EsportaQuery()
                If String.IsNullOrEmpty(par.cmd.CommandText) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                    Exit Sub
                End If
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                Dim NomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportAlloggiSiraper" & CType(Me.Page.FindControl("MasterPage$MainContent$idSiraper"), HiddenField).Value, "Alloggi", dt)
                If File.Exists(Server.MapPath("~\FileTemp\") & NomeFile) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "downloadFile('../FileTemp/" & NomeFile & "');", True)
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "div", "if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Nessun risultato da esportare!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Alloggio - EsportaExcel - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    'Protected Sub btnCercaAlloggio_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCercaAlloggio.Click
    '    Try
    '        CercaAlloggio()
    '    Catch ex As Exception
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            connData.chiudi(False)
    '        End If
    '        Session.Add("ERRORE", "Provenienza: Siraper_Tab_Alloggio - btnCercaAlloggio_Click - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub
    'Private Sub CercaAlloggio()
    '    Try
    '        If Not String.IsNullOrEmpty(txtCodiceUnita.Text) Then
    '            Dim dt As Data.DataTable = Session.Item("dtAlloggio")
    '            Dim row As Data.DataRow
    '            Try
    '                row = dt.Select("CODICE_ALLOGGIO = '" & par.PulisciStrSql(txtCodiceUnita.Text.ToUpper) & "'")(0)
    '                If Not IsNothing(row) Then
    '                    CType(Me.Page, Object).TrovaRigaDataGrid(2, dgvAlloggi, par.IfNull(row.Item("ROWNUM"), 0))
    '                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "div", "if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
    '                Else
    '                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Il Codice dell\'Unità Immobiliare inserito non è presente! Controllare il Codice!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
    '                End If
    '            Catch ex As Exception
    '                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Il Codice dell\'Unità Immobiliare inserito non è presente! Controllare il Codice!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
    '            End Try
    '        Else
    '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Inserire il Codice dell\'Unità Immobiliare da Ricercare!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
    '            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, Me.Page.ClientID, "RicercaOggetto(2,1);", True)
    '        End If
    '        Me.txtCodiceUnita.Text = ""
    '    Catch ex As Exception
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            connData.chiudi(False)
    '        End If
    '        Session.Add("ERRORE", "Provenienza: Siraper_Tab_Alloggio - CercaAlloggio - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub
    Protected Sub btnDettaglioSirAlloggio_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Try
            CaricaDataGridAlloggio()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "div", "if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Alloggio - EsportaExcel - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
