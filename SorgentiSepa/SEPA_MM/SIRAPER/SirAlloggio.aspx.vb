
Partial Class SIRAPER_SirAlloggio
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        idConnessione.Value = Request.QueryString("IdConnessione")
        sescon.Value = Request.QueryString("SESCON")
        If IsNothing(HttpContext.Current.Session.Item(sescon.Value & idConnessione.Value)) Then
            Me.connData = New CM.datiConnessione(par, False, False)
        Else
            Me.connData = CType(HttpContext.Current.Session.Item(sescon.Value & idConnessione.Value), CM.datiConnessione)
            par.cmd = par.OracleConn.CreateCommand()
        End If
        If Not IsPostBack Then
            IdAlloggio.Value = Request.QueryString("ID")
            idSiraper.Value = Request.QueryString("IDS")
            idSiraperVersione.Value = Request.QueryString("IDSV")
            SLE.Value = Request.QueryString("SLE")
            RiempiCampi()
            CaricaAlloggio()
        End If
    End Sub
    Private Sub RiempiCampi()
        Try
            txtCodiceMir.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtProventi.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtProventi.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtCordGaussX.Attributes.Add("onBlur", "javascript:valid(this,'onlynumbers');ControlloCordinate(this);return false;")
            txtCordGaussX.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtCordGaussY.Attributes.Add("onBlur", "javascript:valid(this,'onlynumbers');ControlloCordinate(this);return false;")
            txtCordGaussY.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtRendita.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtRendita.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtNumStanze.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtAnnoRistrutturazione.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtsuputileall.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtsuputileall.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtsupcantine.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtsupcantine.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtsupbalconi.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtsupbalconi.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtsupareaprivata.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtsupareaprivata.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtsupverdecond.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtsupverdecond.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtsupbox.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtsupbox.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtsupostauto.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtsupostauto.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            supertinenze.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            supertinenze.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtsupconante.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtsupconante.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtsupconvlr.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtsupconvlr.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtcostobasemq.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtcostobasemq.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtcanonebase.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtcanonebase.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtcanoneindicizz.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtcanoneindicizz.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtpercapllcaz.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtpercapllcaz.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtcanannante.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtcanannante.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtnumpostiautoconsep.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');return false;")
            txtcanboxconseo.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtcanboxconseo.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtvalmercato.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtvalmercato.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtvalpatrimoniale.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtvalpatrimoniale.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtpercistataggann.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,3);return false;")
            txtpercistataggann.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtpercistatagglg27.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,3);return false;")
            txtpercistatagglg27.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtperaggareadec.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);ControlloMaxValore(this, 0.99);return false;")
            txtperaggareadec.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtgettitocontabunic.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtgettitocontabunic.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtiseprondec.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtiseprondec.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtpse.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtpse.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPER_SirAlloggio - CaricaAlloggio - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaAlloggio()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            par.cmd.CommandText = "SELECT DISTINCT ID, NVL(CANONE_SOCIALE, 0) AS CANONE_SOCIALE, ROWNUM, CODICE_MIR, " _
                                & "'<a href=""javascript:window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='|| SIR_ALLOGGIO.ID ||''',''_blank'',''resizable=no,height=620,top=0,left=0,width=800,scrollbars=no'');void(0);"">'||SIR_ALLOGGIO.CODICE_ALLOGGIO||'</a>' AS CODICE, " _
                                & "(CASE WHEN SIR_ALLOGGIO.ID_CONTRATTO IS NOT NULL THEN '<a href=""javascript:window.open(''../Contratti/Contratto.aspx?LT=1&ID='|| SIR_ALLOGGIO.ID_CONTRATTO ||''',''_blank'',''resizable=no,height=750,top=0,left=0,width=900,scrollbars=no'');void(0);"">'|| 'Dettagli Contratto' ||'</a>' ELSE '' END) AS CONTRATTO, " _
                                & "COD_FISCALE_ENTE, P_IVA_ENTE, SIGLA_ENTE, TIPO_ENTE.DESCRIZIONE AS TIPO_ENTE, TIPO_AMMINISTRAZIONE_ENTE.DESCRIZIONE AS TIPO_AMMINISTRAZIONE, TIPO_DISMESSO_ALLOGGIO.DESCRIZIONE AS ALL_DISM_CART, TRIM(TO_CHAR(PROV_DISM_CART, '9G999G999G999G999G990D99')) AS PROV_DISM_CART, " _
                                & "(CASE WHEN ALL_RISCATTO = 1 THEN 'TRUE' ELSE 'FALSE' END) AS ALL_RISCATTO, SEZIONE, FOGLIO, MAPPALE, SUBALTERNO, (NVL(T_TIPO_VIA.DESCRIZIONE, 'VIA') || ' ' || NOME_VIA || ', ' || NUMERO_CIVICO || (CASE WHEN ESPONENTE IS NOT NULL THEN ', ' || ESPONENTE ELSE '' END)) AS INDIRIZZO, " _
                                & "(NOME_LOCALITA || ' - ' || CAP) AS LOCALITA, CORD_GAUSS_X, CORD_GAUSS_Y, TIPO_OCC_ALL_SIRAPER.DESCRIZIONE AS ALL_OCCUPATO, TIPO_GODIMENTO_SIRAPER.DESCRIZIONE AS TIPO_GODIMENTO, (CASE WHEN ALL_ESCLUSO = 1 THEN 'TRUE' ELSE 'FALSE' END) AS ALL_ESCLUSO, (CASE WHEN SUBSTR(NVL(CATEGORIA_CATASTALE, -1), 1, 1) = 'A' THEN NVL(CATEGORIA_CATASTALE, -1) ELSE '-1' END) AS CATEGORIA_CATASTALE, " _
                                & "TRIM(TO_CHAR(RENDITA_CATASTALE, '9G999G999G999G999G990D99')) AS RENDITA_CATASTALE, NVL(COEFF_CLASSE_DEMOGRAFICA_ANTE, -1) AS COEFF_CLASSE_DEMOGRAFICA_ANTE, NVL(COEFF_CLASSE_DEMOGRAFICA_LR, -1) AS COEFF_CLASSE_DEMOGRAFICA_LR, ANNO_RISTRUTTURAZIONE, NUMERO_STANZE, NVL(TIPO_INTERVENTO, -1) AS TIPO_INTERVENTO, " _
                                & "TIPO_LIVELLO_PIANO_SIRAPER.DESCRIZIONE AS PIANO_ALLOGGIO, COEFF_PIANO, TRIM(TO_CHAR(SUP_UTILE_ALLOGGIO, '9G999G999G999G999G990D99')) AS SUP_UTILE_ALLOGGIO, TRIM(TO_CHAR(SUP_CANTINE_SOFF, '9G999G999G999G999G990D99')) AS SUP_CANTINE_SOFF, TRIM(TO_CHAR(SUP_BALCONI, '9G999G999G999G999G990D99')) AS SUP_BALCONI, TRIM(TO_CHAR(SUP_AREA_PRIVATA, '9G999G999G999G999G990D99')) AS SUP_AREA_PRIVATA, TRIM(TO_CHAR(SUP_VERDE_COND, '9G999G999G999G999G990D99')) AS SUP_VERDE_COND, TRIM(TO_CHAR(SUP_BOX, '9G999G999G999G999G990D99')) AS SUP_BOX, NVL(NUM_BOX, 0) AS NUM_BOX, " _
                                & "TRIM(TO_CHAR(SUP_POSTO_AUTO, '9G999G999G999G999G990D99')) AS SUP_POSTO_AUTO, TRIM(TO_CHAR(SUP_PERTINENZE, '9G999G999G999G999G990D99')) AS SUP_PERTINENZE, TRIM(TO_CHAR(SUP_CONVENZIONALE_ANTE, '9G999G999G999G999G990D99')) AS SUP_CONVENZIONALE_ANTE, TRIM(TO_CHAR(SUP_CONVENZIONALE_LR, '9G999G999G999G999G990D99')) AS SUP_CONVENZIONALE_LR, " _
                                & "(CASE WHEN RISCALDAMENTO = 1 THEN 'CENTRALIZZATO' WHEN RISCALDAMENTO = 2 THEN 'AUTONOMO' WHEN RISCALDAMENTO = 3 THEN 'ASSENTE' ELSE 'NON RILEVATO' END) AS RISCALDAMENTO, (CASE WHEN ASCENSORE = 1 THEN 'SI' ELSE 'NO' END) AS ASCENSORE, " _
                                & "NVL(STATO_CONS_ACCESSI, 0) AS STATO_CONS_ACCESSI, NVL(STATO_CONS_FACCIATA, 0) AS STATO_CONS_FACCIATA, NVL(STATO_CONS_PAV, 0) AS STATO_CONS_PAV, NVL(STATO_CONS_PARETI, 0) AS STATO_CONS_PARETI, NVL(STATO_CONF_INFISSI, 0) AS STATO_CONF_INFISSI, NVL(STATO_CONF_IMP_ELE, 0) AS STATO_CONF_IMP_ELE, " _
                                & "NVL(STATO_CONS_IMP_IDR, 0) AS STATO_CONS_IMP_IDR, NVL(STATO_CONS_IMP_RISC, 0) AS STATO_CONS_IMP_RISC, NVL(STATO_CONS_ALL, -1) AS STATO_CONS_ALL, NVL(TIPO_CUCINA, 0) AS TIPO_CUCINA, NVL(BARR_ARCH, 0) AS BARR_ARCH, TRIM(TO_CHAR(COSTO_BASE_MQ, '9G999G999G999G999G990D99')) AS COSTO_BASE_MQ, PER_ISTAT_AGG, TRIM(TO_CHAR(CANONE_BASE, '9G999G999G999G999G990D99')) AS CANONE_BASE, TRIM(TO_CHAR(CANONE_IND_ANN, '9G999G999G999G999G990D99')) AS CANONE_IND_ANN, PERC_APPL, " _
                                & "FASC_APPARTENENZA, TIPO_AREA_APPARTENENZA.DESCRIZIONE AS AREA_APPARTENENZA, TRIM(TO_CHAR(CANONE_APP_ANN, '9G999G999G999G999G990D99')) AS CANONE_APP_ANN, PER_ISTAT_LEGGE27, TIPO_CANONE_SIRAPER.DESCRIZIONE AS TIPO_CANONE, TRIM(TO_CHAR(CANONE_ANN_ANTE, '9G999G999G999G999G990D99')) AS CANONE_ANN_ANTE, TRIM(TO_CHAR(CANONE_ANN_REG, '9G999G999G999G999G990D99')) AS CANONE_ANN_REG, VALORE_CONVENZIONALE, COSTO_CONVENZIONALE, CARATT_UNITA_AB, (CASE WHEN REDD_PREV_DIP = 1 THEN 'SI' ELSE 'NO' END) AS REDD_PREV_DIP, NVL(ABBATTIMENTO_CANONE, -1) AS ABBATTIMENTO_CANONE, " _
                                & "NVL(SOVRAPREZZO_DECADENZA, -1) AS SOVRAPREZZO_DECADENZA, PERC_AGG_AREA_DEC, NVL(SOVRAPREZZO_SOTTOUTILIZZO, 2) AS SOVRAPREZZO_SOTTOUTILIZZO, (CASE WHEN REDD_DIP = 1 THEN 'SI' ELSE 'NO' END) AS REDD_DIP, NUM_BOX_CONTR_SEP, TRIM(TO_CHAR(CANONE_BOX_CONTR_SEP, '9G999G999G999G999G990D99')) AS CANONE_BOX_CONTR_SEP, NVL(CONTAB_UNICA, -1) AS CONTAB_UNICA, TRIM(TO_CHAR(GETTITO_CONTAB_UNICA, '9G999G999G999G999G990D99')) AS GETTITO_CONTAB_UNICA, NVL(NUM_PERSONE_INV100_CON, 0) AS NUM_PERSONE_INV100_CON, " _
                                & "NVL(NUM_PERSONE_INV100_SENZA, 0) AS NUM_PERSONE_INV100_SENZA, NVL(NUM_PERSONE_INV_67_99, 0) AS NUM_PERSONE_INV_67_99, NVL(SPESE_PERSONE_INV100_CON, 0) AS SPESE_PERSONE_INV100_CON, TIPO_STATO_AGG_NUCLEO_SIRAPER.DESCRIZIONE AS STATO_AGG_NUCLEO, TO_CHAR(TO_DATE(DATA_CALCOLO_ISEE, 'YYYYmmdd'), 'DD-MM-YYYY') AS DATA_CALCOLO_ISEE, NVL(ISR, 0) AS ISR, NVL(ISP, 0) AS ISP, NVL(PSE, 0) AS PSE, NVL(ISE_ERP, 0) AS ISE_ERP, NVL(ISEE_ERP, 0) AS ISEE_ERP, NVL(ISE_ERP_ASS, 0) AS ISE_ERP_ASS, NVL(ISEE_ERP_ASS, 0) AS ISEE_ERP_ASS, NVL(REDD_DIP_ASS, 0) AS REDD_DIP_ASS, " _
                                & "NVL(ALTRI_REDD, 0) AS ALTRI_REDD, NVL(VALORE_LOCATIVO, 0) AS VALORE_LOCATIVO, TRIM(TO_CHAR(VALORE_MERCATO, '9G999G999G999G999G990D99')) AS VALORE_MERCATO, NVL(COEFF_VETUSTA, 0) AS COEFF_VETUSTA, NVL(NUM_COMPONENTI, 0) AS NUM_COMPONENTI, ANNO_VETUSTA, (NVL(PERC_VAL_LOCATIVO, 0) || ' %') AS PERC_VAL_LOCATIVO, TAB_CLASSI_ISEE, NVL(INV_SOCIALE, -1) AS INV_SOCIALE, TRIM(TO_CHAR(ISEE_PRON_DECADENZA, '9G999G999G999G999G990D99')) AS ISEE_PRON_DECADENZA, TO_CHAR(TO_DATE(DATA_DISPONIBILITA, 'YYYYmmdd'), 'DD-MM-YYYY') AS DATA_DISPONIBILITA, TO_CHAR(TO_DATE(DATA_ASSEGNAZIONE, 'YYYYmmdd'), 'DD-MM-YYYY') AS DATA_ASSEGNAZIONE, " _
                                & "TRIM(TO_CHAR(VALORE_PATRIMONIALE, '9G999G999G999G999G990D99')) AS VALORE_PATRIMONIALE, NVL(MOROSITA_ATTUALE_FAM, 0) AS MOROSITA_ATTUALE_FAM, NVL(MOROSITA_PREC_FAM, 0) AS MOROSITA_PREC_FAM, SIR_ALLOGGIO.CODICE_ALLOGGIO " _
                                & "FROM SISCOM_MI.SIR_ALLOGGIO, SISCOM_MI.TIPO_ENTE, SISCOM_MI.TIPO_AMMINISTRAZIONE_ENTE, SISCOM_MI.TIPO_DISMESSO_ALLOGGIO, T_TIPO_VIA, SISCOM_MI.TIPO_LIVELLO_PIANO_SIRAPER, SISCOM_MI.TIPO_OCC_ALL_SIRAPER, SISCOM_MI.TIPO_GODIMENTO_SIRAPER, SISCOM_MI.TIPO_AREA_APPARTENENZA, SISCOM_MI.TIPO_CANONE_SIRAPER, SISCOM_MI.TIPO_STATO_AGG_NUCLEO_SIRAPER " _
                                & "WHERE TIPO_ENTE.COD(+) = SIR_ALLOGGIO.TIPO_ENTE AND TIPO_AMMINISTRAZIONE_ENTE.COD(+) = SIR_ALLOGGIO.TIPO_AMMINISTRAZIONE AND TIPO_DISMESSO_ALLOGGIO.COD(+) = SIR_ALLOGGIO.ALL_DISM_CART AND TIPO_STATO_AGG_NUCLEO_SIRAPER.COD(+) = SIR_ALLOGGIO.STATO_AGG_NUCLEO " _
                                & "AND T_TIPO_VIA.COD_SIRAPER(+) = SIR_ALLOGGIO.PREFISSO_INDIRIZZO AND TIPO_LIVELLO_PIANO_SIRAPER.COD(+) = SIR_ALLOGGIO.PIANO_ALLOGGIO AND TIPO_OCC_ALL_SIRAPER.COD(+) = SIR_ALLOGGIO.ALL_OCCUPATO " _
                                & "AND TIPO_GODIMENTO_SIRAPER.COD(+) = SIR_ALLOGGIO.TIPO_GODIMENTO AND TIPO_AREA_APPARTENENZA.COD(+) = SIR_ALLOGGIO.AREA_APPARTENENZA AND TIPO_CANONE_SIRAPER.COD(+) = SIR_ALLOGGIO.TIPO_CANONE " _
                                & "AND ID_SIRAPER = " & idSiraper.Value & " AND ID = " & IdAlloggio.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            For Each row As Data.DataRow In dt.Rows
                txtCodiceMir.Text = par.IfEmpty(row.Item("CODICE_MIR").ToString, "")
                CODICE.Text = par.IfEmpty(row.Item("CODICE").ToString, "")
                CONTRATTO.Text = par.IfEmpty(row.Item("CONTRATTO").ToString, "")
                COD_FISCALE_ENTE.Text = par.IfEmpty(row.Item("COD_FISCALE_ENTE").ToString, "")
                P_IVA_ENTE.Text = par.IfEmpty(row.Item("P_IVA_ENTE").ToString, "")
                SIGLA_ENTE.Text = par.IfEmpty(row.Item("SIGLA_ENTE").ToString, "")
                TIPO_ENTE.Text = par.IfEmpty(row.Item("TIPO_ENTE").ToString, "")
                TIPO_AMMINISTRAZIONE.Text = par.IfEmpty(row.Item("TIPO_AMMINISTRAZIONE").ToString, "")
                ALL_DISM_CART.Text = par.IfEmpty(row.Item("ALL_DISM_CART").ToString, "")
                txtProventi.Text = par.IfEmpty(row.Item("PROV_DISM_CART").ToString, "")
                If par.IfEmpty(row.Item("ALL_RISCATTO").ToString, "0") = "1" Then
                    chkRiscatto.Checked = True
                End If
                SEZIONE.Text = par.IfEmpty(row.Item("SEZIONE").ToString, "")
                FOGLIO.Text = par.IfEmpty(row.Item("FOGLIO").ToString, "")
                MAPPALE.Text = par.IfEmpty(row.Item("MAPPALE").ToString, "")
                SUBALTERNO.Text = par.IfEmpty(row.Item("SUBALTERNO").ToString, "")
                INDIRIZZO.Text = par.IfEmpty(row.Item("INDIRIZZO").ToString, "")
                LOCALITA.Text = par.IfEmpty(row.Item("LOCALITA").ToString, "")
                txtCordGaussX.Text = par.IfEmpty(row.Item("CORD_GAUSS_X").ToString, "")
                txtCordGaussY.Text = par.IfEmpty(row.Item("CORD_GAUSS_Y").ToString, "")
                ALL_OCCUPATO.Text = par.IfEmpty(row.Item("ALL_OCCUPATO").ToString, "")
                TIPO_GODIMENTO.Text = par.IfEmpty(row.Item("TIPO_GODIMENTO").ToString, "")
                If par.IfEmpty(row.Item("ALL_ESCLUSO").ToString, "0") = "1" Then
                    chkEscluso.Checked = True
                End If
                ddlgestionedificio.SelectedValue = par.IfEmpty(row.Item("CATEGORIA_CATASTALE").ToString, "-1")
                txtRendita.Text = par.IfEmpty(row.Item("RENDITA_CATASTALE").ToString, "")
                ddlcoeffante.SelectedValue = par.IfEmpty(row.Item("COEFF_CLASSE_DEMOGRAFICA_ANTE").ToString, "-1")
                ddlcoefflr.SelectedValue = par.IfEmpty(row.Item("COEFF_CLASSE_DEMOGRAFICA_LR").ToString, "-1")
                txtNumStanze.Text = par.IfEmpty(row.Item("NUMERO_STANZE").ToString, "")
                txtAnnoRistrutturazione.Text = par.IfEmpty(row.Item("ANNO_RISTRUTTURAZIONE").ToString, "")
                ddltipointervento.SelectedValue = par.IfEmpty(row.Item("TIPO_INTERVENTO").ToString, "-1")
                PIANO_ALLOGGIO.Text = par.IfEmpty(row.Item("PIANO_ALLOGGIO").ToString, "")
                COEFF_PIANO.Text = par.IfEmpty(row.Item("COEFF_PIANO").ToString, "")
                txtsuputileall.Text = par.IfEmpty(row.Item("SUP_UTILE_ALLOGGIO").ToString, "")
                txtsupcantine.Text = par.IfEmpty(row.Item("SUP_CANTINE_SOFF").ToString, "")
                txtsupbalconi.Text = par.IfEmpty(row.Item("SUP_BALCONI").ToString, "")
                txtsupareaprivata.Text = par.IfEmpty(row.Item("SUP_AREA_PRIVATA").ToString, "")
                txtsupverdecond.Text = par.IfEmpty(row.Item("SUP_VERDE_COND").ToString, "")
                txtsupbox.Text = par.IfEmpty(row.Item("SUP_BOX").ToString, "")
                NUM_BOX.Text = par.IfEmpty(row.Item("NUM_BOX").ToString, "")
                txtsupostauto.Text = par.IfEmpty(row.Item("SUP_POSTO_AUTO").ToString, "")
                supertinenze.Text = par.IfEmpty(row.Item("SUP_PERTINENZE").ToString, "")
                txtsupconante.Text = par.IfEmpty(row.Item("SUP_CONVENZIONALE_ANTE").ToString, "")
                txtsupconvlr.Text = par.IfEmpty(row.Item("SUP_CONVENZIONALE_LR").ToString, "")
                RISCALDAMENTO.Text = par.IfEmpty(row.Item("RISCALDAMENTO").ToString, "")
                ASCENSORE.Text = par.IfEmpty(row.Item("ASCENSORE").ToString, "")
                ddlstatoconsaccessi.SelectedValue = par.IfEmpty(row.Item("STATO_CONS_ACCESSI").ToString, "")
                ddlstatoconsfacc.SelectedValue = par.IfEmpty(row.Item("STATO_CONS_FACCIATA").ToString, "")
                ddlstatoconspav.SelectedValue = par.IfEmpty(row.Item("STATO_CONS_PAV").ToString, "")
                ddlstatoconspareti.SelectedValue = par.IfEmpty(row.Item("STATO_CONS_PARETI").ToString, "")
                ddlstatoconsinfissi.SelectedValue = par.IfEmpty(row.Item("STATO_CONF_INFISSI").ToString, "")
                ddlstatoconsimpele.SelectedValue = par.IfEmpty(row.Item("STATO_CONF_IMP_ELE").ToString, "")
                ddlstatoconsimpidri.SelectedValue = par.IfEmpty(row.Item("STATO_CONS_IMP_IDR").ToString, "")
                ddlstatoconsimprisc.SelectedValue = par.IfEmpty(row.Item("STATO_CONS_IMP_RISC").ToString, "")
                ddlstatoconsall.SelectedValue = par.IfEmpty(row.Item("STATO_CONS_ALL").ToString, "-1")
                ddltipocucina.SelectedValue = par.IfEmpty(row.Item("TIPO_CUCINA").ToString, "")
                ddlbarrarch.SelectedValue = par.IfEmpty(row.Item("BARR_ARCH").ToString, "")
                txtcostobasemq.Text = par.IfEmpty(row.Item("COSTO_BASE_MQ").ToString, "")
                txtpercistataggann.Text = par.IfEmpty(row.Item("PER_ISTAT_AGG").ToString, "")
                txtcanonebase.Text = par.IfEmpty(row.Item("CANONE_BASE").ToString, "")
                txtcanoneindicizz.Text = par.IfEmpty(row.Item("CANONE_IND_ANN").ToString, "")
                txtpercapllcaz.Text = par.IfEmpty(row.Item("PERC_APPL").ToString, "")
                txtfasciapp.Text = par.IfEmpty(row.Item("FASC_APPARTENENZA").ToString, "")
                AREA_APPARTENENZA.Text = par.IfEmpty(row.Item("AREA_APPARTENENZA").ToString, "")
                CANONE_APP_ANN.Text = par.IfEmpty(row.Item("CANONE_APP_ANN").ToString, "")
                txtpercistatagglg27.Text = par.IfEmpty(row.Item("PER_ISTAT_LEGGE27").ToString, "")
                TIPO_CANONE.Text = par.IfEmpty(row.Item("TIPO_CANONE").ToString, "")
                txtcanannante.Text = par.IfEmpty(row.Item("CANONE_ANN_ANTE").ToString, "")
                CANONE_ANN_REG.Text = par.IfEmpty(row.Item("CANONE_ANN_REG").ToString, "")
                VALORE_CONVENZIONALE.Text = par.IfEmpty(row.Item("VALORE_CONVENZIONALE").ToString, "")
                COSTO_CONVENZIONALE.Text = par.IfEmpty(row.Item("COSTO_CONVENZIONALE").ToString, "")
                CARATT_UNITA_AB.Text = par.IfEmpty(row.Item("CARATT_UNITA_AB").ToString, "")
                REDD_PREV_DIP.Text = par.IfEmpty(row.Item("REDD_PREV_DIP").ToString, "")
                ddlabbatimentocan.SelectedValue = par.IfEmpty(row.Item("ABBATTIMENTO_CANONE").ToString, "-1")
                ddlsovraprezzodecadenza.SelectedValue = par.IfEmpty(row.Item("SOVRAPREZZO_DECADENZA").ToString, "-1")
                txtperaggareadec.Text = par.IfEmpty(row.Item("PERC_AGG_AREA_DEC").ToString, "")
                ddlsovraprsottouti.SelectedValue = par.IfEmpty(row.Item("SOVRAPREZZO_SOTTOUTILIZZO").ToString, "")
                REDD_DIP.Text = par.IfEmpty(row.Item("REDD_DIP").ToString, "")
                txtnumpostiautoconsep.Text = par.IfEmpty(row.Item("NUM_BOX_CONTR_SEP").ToString, "")
                txtcanboxconseo.Text = par.IfEmpty(row.Item("CANONE_BOX_CONTR_SEP").ToString, "")
                ddlcontunica.SelectedValue = par.IfEmpty(row.Item("CONTAB_UNICA").ToString, "-1")
                txtgettitocontabunic.Text = par.IfEmpty(row.Item("GETTITO_CONTAB_UNICA").ToString, "")
                NUM_PERSONE_INV100_CON.Text = par.IfEmpty(row.Item("NUM_PERSONE_INV100_CON").ToString, "")
                NUM_PERSONE_INV100_SENZA.Text = par.IfEmpty(row.Item("NUM_PERSONE_INV100_SENZA").ToString, "")
                NUM_PERSONE_INV_67_99.Text = par.IfEmpty(row.Item("NUM_PERSONE_INV_67_99").ToString, "")
                SPESE_PERSONE_INV100_CON.Text = par.IfEmpty(row.Item("SPESE_PERSONE_INV100_CON").ToString, "")
                STATO_AGG_NUCLEO.Text = par.IfEmpty(row.Item("STATO_AGG_NUCLEO").ToString, "")
                DATA_CALCOLO_ISEE.Text = par.IfEmpty(row.Item("DATA_CALCOLO_ISEE").ToString, "")
                ISR.Text = par.IfEmpty(row.Item("ISR").ToString, "")
                ISP.Text = par.IfEmpty(row.Item("ISP").ToString, "")
                txtpse.Text = par.IfEmpty(row.Item("PSE").ToString, "")
                ISE_ERP.Text = par.IfEmpty(row.Item("ISE_ERP").ToString, "")
                ISEE_ERP.Text = par.IfEmpty(row.Item("ISEE_ERP").ToString, "")
                ISE_ERP_ASS.Text = par.IfEmpty(row.Item("ISE_ERP_ASS").ToString, "")
                ISEE_ERP_ASS.Text = par.IfEmpty(row.Item("ISEE_ERP_ASS").ToString, "")
                REDD_DIP_ASS.Text = par.IfEmpty(row.Item("REDD_DIP_ASS").ToString, "")
                ALTRI_REDD.Text = par.IfEmpty(row.Item("ALTRI_REDD").ToString, "")
                VALORE_LOCATIVO.Text = par.IfEmpty(row.Item("VALORE_LOCATIVO").ToString, "")
                txtvalmercato.Text = par.IfEmpty(row.Item("VALORE_MERCATO").ToString, "")
                COEFF_VETUSTA.Text = par.IfEmpty(row.Item("COEFF_VETUSTA").ToString, "")
                NUM_COMPONENTI.Text = par.IfEmpty(row.Item("NUM_COMPONENTI").ToString, "")
                ANNO_VETUSTA.Text = par.IfEmpty(row.Item("ANNO_VETUSTA").ToString, "")
                PERC_VAL_LOCATIVO.Text = par.IfEmpty(row.Item("PERC_VAL_LOCATIVO").ToString, "")
                TAB_CLASSI_ISEE.Text = par.IfEmpty(row.Item("TAB_CLASSI_ISEE").ToString, "")
                ddlinvaliditasoc.SelectedValue = par.IfEmpty(row.Item("INV_SOCIALE").ToString, "-1")
                txtiseprondec.Text = par.IfEmpty(row.Item("ISEE_PRON_DECADENZA").ToString, "")
                DATA_DISPONIBILITA.Text = par.IfEmpty(row.Item("DATA_DISPONIBILITA").ToString, "")
                DATA_ASSEGNAZIONE.Text = par.IfEmpty(row.Item("DATA_ASSEGNAZIONE").ToString, "")
                txtvalpatrimoniale.Text = par.IfEmpty(row.Item("VALORE_PATRIMONIALE").ToString, "")
                MOROSITA_ATTUALE_FAM.Text = par.IfEmpty(row.Item("MOROSITA_ATTUALE_FAM").ToString, "")
                MOROSITA_PREC_FAM.Text = par.IfEmpty(row.Item("MOROSITA_PREC_FAM").ToString, "")
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPER_SirAlloggio - CaricaAlloggio - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            SalvaDettaglioAlloggio()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPER_SirAlloggio - btnProcedi_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SalvaDettaglioAlloggio()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            Dim AlloggioRiscatto As String = "2"
            If chkRiscatto.Checked = True Then
                AlloggioRiscatto = "1"
            End If
            Dim AlloggioEscluso As String = "0"
            If chkEscluso.Checked = True Then AlloggioEscluso = "1"
            Dim AnnoRistrutturazione As String = par.IfNull(txtAnnoRistrutturazione.Text, "null")
            If String.IsNullOrEmpty(AnnoRistrutturazione) Then AnnoRistrutturazione = "null"
            par.cmd.CommandText = "UPDATE SISCOM_MI.SIR_ALLOGGIO SET CODICE_MIR = '" & par.PulisciStrSql(par.IfEmpty(txtCodiceMir.Text, "")) & "', " _
                                & "PROV_DISM_CART = " & par.VirgoleInPunti(par.IfEmpty(txtProventi.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "ALL_RISCATTO = " & AlloggioRiscatto & ", " _
                                & "CORD_GAUSS_X = '" & par.IfEmpty(txtCordGaussX.Text, "") & "', " _
                                & "CORD_GAUSS_Y = '" & par.IfEmpty(txtCordGaussY.Text, "") & "', " _
                                & "ALL_ESCLUSO = " & AlloggioEscluso & ", " _
                                & "CATEGORIA_CATASTALE = " & RitornaNullSeMenoUno(par.IfNull(ddlgestionedificio.SelectedValue, "-1")) & ", " _
                                & "RENDITA_CATASTALE = " & par.VirgoleInPunti(par.IfEmpty(txtRendita.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "COEFF_CLASSE_DEMOGRAFICA_ANTE = " & RitornaNullSeMenoUno(par.IfNull(ddlcoeffante.SelectedValue, "-1")) & ", " _
                                & "COEFF_CLASSE_DEMOGRAFICA_LR = " & RitornaNullSeMenoUno(par.IfNull(ddlcoefflr.SelectedValue, "-1")) & ", " _
                                & "NUMERO_STANZE = " & par.IfEmpty(txtNumStanze.Text, "null") & ", " _
                                & "ANNO_RISTRUTTURAZIONE = " & AnnoRistrutturazione & ", " _
                                & "TIPO_INTERVENTO = " & RitornaNullSeMenoUno(par.IfNull(ddltipointervento.SelectedValue, "-1")) & ", " _
                                & "SUP_UTILE_ALLOGGIO = " & par.VirgoleInPunti(par.IfEmpty(txtsuputileall.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "SUP_CANTINE_SOFF = " & par.VirgoleInPunti(par.IfEmpty(txtsupcantine.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "SUP_BALCONI = " & par.VirgoleInPunti(par.IfEmpty(txtsupbalconi.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "SUP_AREA_PRIVATA = " & par.VirgoleInPunti(par.IfEmpty(txtsupareaprivata.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "SUP_VERDE_COND = " & par.VirgoleInPunti(par.IfEmpty(txtsupverdecond.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "SUP_BOX = " & par.VirgoleInPunti(par.IfEmpty(txtsupbox.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "SUP_POSTO_AUTO = " & par.VirgoleInPunti(par.IfEmpty(txtsupostauto.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "SUP_PERTINENZE = " & par.VirgoleInPunti(par.IfEmpty(supertinenze.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "SUP_CONVENZIONALE_ANTE = " & par.VirgoleInPunti(par.IfEmpty(txtsupconante.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "SUP_CONVENZIONALE_LR = " & par.VirgoleInPunti(par.IfEmpty(txtsupconvlr.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "STATO_CONS_ACCESSI = " & par.IfNull(ddlstatoconsaccessi.SelectedValue, 0) & ", " _
                                & "STATO_CONS_FACCIATA = " & par.IfNull(ddlstatoconsfacc.SelectedValue, 0) & ", " _
                                & "STATO_CONS_PAV = " & par.IfNull(ddlstatoconspav.SelectedValue, 0) & ", " _
                                & "STATO_CONS_PARETI = " & par.IfNull(ddlstatoconspareti.SelectedValue, 0) & ", " _
                                & "STATO_CONF_INFISSI = " & par.IfNull(ddlstatoconsinfissi.SelectedValue, 0) & ", " _
                                & "STATO_CONF_IMP_ELE = " & par.IfNull(ddlstatoconsimpele.SelectedValue, 0) & ", " _
                                & "STATO_CONS_IMP_IDR = " & par.IfNull(ddlstatoconsimpidri.SelectedValue, 0) & ", " _
                                & "STATO_CONS_IMP_RISC = " & par.IfNull(ddlstatoconsimprisc.SelectedValue, 0) & ", " _
                                & "STATO_CONS_ALL = " & RitornaNullSeMenoUno(par.IfNull(ddlstatoconsall.SelectedValue, "-1")) & ", " _
                                & "TIPO_CUCINA = " & par.IfNull(ddltipocucina.SelectedValue, 0) & ", " _
                                & "BARR_ARCH = " & par.IfNull(ddlbarrarch.SelectedValue, 0) & ", " _
                                & "PER_ISTAT_AGG = " & par.VirgoleInPunti(par.IfEmpty(txtpercistataggann.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "COSTO_BASE_MQ = " & par.VirgoleInPunti(par.IfEmpty(txtcostobasemq.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "CANONE_BASE = " & par.VirgoleInPunti(par.IfEmpty(txtcanonebase.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "CANONE_IND_ANN = " & par.VirgoleInPunti(par.IfEmpty(txtcanoneindicizz.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "PERC_APPL = " & par.VirgoleInPunti(par.IfEmpty(txtpercapllcaz.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "FASC_APPARTENENZA = '" & par.PulisciStrSql(par.IfEmpty(txtfasciapp.Text, "")) & "', " _
                                & "PER_ISTAT_LEGGE27 = " & par.VirgoleInPunti(par.IfEmpty(txtpercistatagglg27.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "CANONE_ANN_ANTE = " & par.VirgoleInPunti(par.IfEmpty(txtcanannante.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "ABBATTIMENTO_CANONE = " & RitornaNullSeMenoUno(par.IfNull(ddlabbatimentocan.SelectedValue, "-1")) & ", " _
                                & "SOVRAPREZZO_DECADENZA = " & RitornaNullSeMenoUno(par.IfNull(ddlsovraprezzodecadenza.SelectedValue, "-1")) & ", " _
                                & "PERC_AGG_AREA_DEC = " & par.VirgoleInPunti(par.IfEmpty(txtperaggareadec.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "SOVRAPREZZO_SOTTOUTILIZZO = " & par.IfNull(ddlsovraprsottouti.SelectedValue, 2) & ", " _
                                & "NUM_BOX_CONTR_SEP = " & par.VirgoleInPunti(par.IfEmpty(txtnumpostiautoconsep.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "CANONE_BOX_CONTR_SEP = " & par.VirgoleInPunti(par.IfEmpty(txtcanboxconseo.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "CONTAB_UNICA = " & RitornaNullSeMenoUno(par.IfNull(ddlcontunica.SelectedValue, "-1")) & ", " _
                                & "GETTITO_CONTAB_UNICA = " & par.VirgoleInPunti(par.IfEmpty(txtgettitocontabunic.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "VALORE_MERCATO = " & par.VirgoleInPunti(par.IfEmpty(txtvalmercato.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "INV_SOCIALE = " & RitornaNullSeMenoUno(par.IfNull(ddlinvaliditasoc.SelectedValue, "-1")) & ", " _
                                & "ISEE_PRON_DECADENZA = " & par.VirgoleInPunti(par.IfEmpty(txtiseprondec.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "VALORE_PATRIMONIALE = " & par.VirgoleInPunti(par.IfEmpty(txtvalpatrimoniale.Text, "null").ToString.Replace(".", "")) & ", " _
                                & "PSE = " & par.VirgoleInPunti(par.IfEmpty(txtpse.Text, "null").ToString.Replace(".", "")) & " " _
                                & "WHERE ID = " & IdAlloggio.Value & " AND ID_SIRAPER = " & idSiraper.Value
            par.cmd.ExecuteNonQuery()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Operazione Completata!');", True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open And sescon.Value <> "SIRAPER" Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPER_SirAlloggio - btnProcedi_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function RitornaNullSeMenoUno(ByVal Valore As String) As String
        Try
            RitornaNullSeMenoUno = "null"
            If Valore = "-1" Then
                RitornaNullSeMenoUno = "null"
            ElseIf Valore <> "-1" Then
                RitornaNullSeMenoUno = "'" & Valore & "'"
            End If
        Catch ex As Exception
            RitornaNullSeMenoUno = "null"
        End Try
    End Function
End Class
