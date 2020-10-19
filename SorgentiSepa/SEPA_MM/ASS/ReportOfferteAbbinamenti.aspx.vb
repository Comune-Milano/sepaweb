Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports xi = Telerik.Web.UI.ExportInfrastructure
Imports System.Web.UI
Imports System.Web
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.Drawing

Partial Class Ass_ReportOfferteAbbinamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public Altezza As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            Dim Str As String = ""

            If Not IsPostBack Then
                'txtRicDA.SelectedDate = Today.AddDays(-365).Date
                'txtRicA.SelectedDate = Today.Date
                par.caricaComboTelerik("select DESCRIZIONE  from bandi where DESCRIZIONE is not null union  select  'BANDO CAMBI' AS NOME_BANDO from dual union select  'BANDO CAMBI IN EMERGENZA' AS NOME_BANDO from dual order by 1", cmbBando, "DESCRIZIONE", "DESCRIZIONE", True, "-1", "---")
                par.caricaComboTelerik("select * from tab_stati where cod not in ('71', '72','73','74','75','76','77')  order by 1", cmbStatoDomanda, "COD", "DESCRIZIONE", True, "-1", "---")
                par.caricaComboTelerik("SELECT 'BANDO ERP' AS TIPO_BANDO FROM DUAL union  select  'BANDO CAMBI' AS TIPO_BANDO from dual union select  'BANDO CAMBI IN EMERGENZA' AS TIPO_BANDO from dual order by 1", cmbTipoBando, "TIPO_BANDO", "TIPO_BANDO", True, "-1", "---")
                par.caricaComboTelerik(" select  'PROPOSTA RIFIUTATA' as descrizione from DUAL union select  'PROPOSTA ACCETTATA' from dual union select  'PROPOSTA ANNULLATA' from dual union select  'ASSEGNAZIONE ANNULLATA' from dual union select  'PROPOSTA IN CORSO' from dual ", cmbEsito, "DESCRIZIONE", "DESCRIZIONE", True, "-1", "---")
                BindGrid()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Ass - Report Offerte/Abbinamenti - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub BindGrid()

        Dim DA As String
        Dim A As String
        Dim lAddwhere1 As String = " WHERE (1=1) "

        If txtRicDA.SelectedDate Is Nothing Then
            DA = "01/01/1900"
        Else
            DA = txtRicDA.SelectedDate
        End If

        If txtRicA.SelectedDate Is Nothing Then
            A = "31/12/2999"
        Else
            A = txtRicA.SelectedDate
        End If

        Dim lAddwhere As String = " AND DATA_PG BETWEEN '" & Year(DA) & Month(DA).ToString.PadLeft(2, "0") & Day(DA).ToString.PadLeft(2, "0") & "' AND '" & Year(A) & Month(A).ToString.PadLeft(2, "0") & Day(A).ToString.PadLeft(2, "0") & "' "

        If cmbBando.SelectedItem.Text <> "---" Then
            lAddwhere1 = lAddwhere1 & " AND UPPER(A.NOME_BANDO) = '" & cmbBando.SelectedItem.Text.ToUpper & "'"
        End If

        If cmbTipoBando.SelectedItem.Text <> "---" Then
            lAddwhere1 = lAddwhere1 & " AND UPPER(A.TIPO_BANDO) = '" & cmbTipoBando.SelectedItem.Text.ToUpper & "'"
        End If

        If cmbEsito.SelectedItem.Text <> "---" Then
            lAddwhere1 = lAddwhere1 & " AND UPPER(A.ESITO) = '" & cmbEsito.SelectedItem.Text.ToUpper & "'"
        End If

        If cmbStatoDomanda.SelectedItem.Text <> "---" Then
            lAddwhere1 = lAddwhere1 & " AND UPPER(A.ID_STATO) = '" & cmbStatoDomanda.SelectedValue.ToUpper & "'"
        End If

        If cmbEsito.SelectedItem.Text <> "---" Then
            lAddwhere1 = lAddwhere1 & " AND UPPER(A.ESITO) = '" & cmbEsito.SelectedItem.Text.ToUpper & "'"
        End If

        'lblPGDom.Text = "<a href=""javascript:window.open('../VSA/NuovaDomandaVSA/domandaNuova.aspx?ID=" & idDomanda.Value & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','');void(0);"">" & lblPGDom.Text & "</a>"
        'lblPGDom.Text = "<a href=""javascript:window.open('../domanda.aspx?ID=" & par.IfNull(myReader1("ID"), "0") & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & lblPGDom.Text & "</a>"
        'lblPGDom.Text = "<a href=""javascript:window.open('../CAMBI/domanda.aspx?ID=" & par.IfNull(myReader1B("ID"), "0") & "&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & lblPGDom.Text & "</a>"

        ' sStrSql = sStrSql & ", '<a href=''javascript:void(0);'' onclick=""window.open(''DettaglioOrdine.aspx?T=X&D='||MANUTENZIONI.PROGR||'_'||MANUTENZIONI.ANNO||''',''Intervento_'||MANUTENZIONI.ID||''','''');"">'||MANUTENZIONI.PROGR || '/' || MANUTENZIONI.ANNO||'</a>' AS ODL "

        sStrSql = "select * from (" _
           & "SELECT (case when (SELECT MAX (DOMANDE_OFFERTE_SCAD.id)" _
           & "        FROM DOMANDE_OFFERTE_SCAD" _
           & "           WHERE     DOMANDE_OFFERTE_SCAD.id_domanda(+) = domande_bando.id " _
           & "                 AND DOMANDE_OFFERTE_SCAD.DATA_SCADENZA = DATA_PROPOSTA) is null then (select max(n_offerta) from SISCOM_MI.UNITA_ASSEGNATE where id_domanda=domande_bando.id) else (SELECT MAX (DOMANDE_OFFERTE_SCAD.id)" _
           & "        FROM DOMANDE_OFFERTE_SCAD" _
           & "           WHERE     DOMANDE_OFFERTE_SCAD.id_domanda(+) = domande_bando.id " _
           & "                 AND DOMANDE_OFFERTE_SCAD.DATA_SCADENZA = DATA_PROPOSTA) end) " _
           & "            AS NUM_OFFERTA," _
           & "         DOMANDE_BANDO.PG AS PG_DOMANDA," _
           & "         '<a href=''javascript:void(0);'' onclick=""window.open(''../domanda.aspx?ID1=-1&PROGR=-1&LE=1&APP=1&US=1&ID='|| DOMANDE_BANDO.ID ||''',''Domanda_'|| DOMANDE_BANDO.PG ||''',''top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no'');"">'||DOMANDE_BANDO.PG ||'</a>' AS PG_DOMANDA1," _
           & "         BANDI.DESCRIZIONE AS NOME_BANDO," _
           & "        'BANDO ERP' AS TIPO_BANDO," _
           & "         COMP_NUCLEO.COD_FISCALE AS CF_RICHIEDENTE," _
           & "         COMP_NUCLEO.COGNOME," _
           & "         COMP_NUCLEO.NOME," _
           & "         (CASE" _
           & "             WHEN UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE IS NULL" _
           & "             THEN" _
           & "        COD_ALLOGGIO" _
           & "             ELSE" _
           & "        UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE" _
           & "          END)" _
           & "            AS UNITA_IMMOBILIARE," _
           & "         INDIRIZZO," _
           & "         NUM_CIVICO," _
           & "         to_date (DATA_PROPOSTA,'yyyyMMdd') AS DATA_PROPOSTA," _
           & "         DECODE (ESITO," _
           & "                 0, 'PROPOSTA RIFIUTATA'," _
           & "                 1, 'PROPOSTA ACCETTATA'," _
           & "                 3, 'PROPOSTA ANNULLATA'," _
           & "                4, 'ASSEGNAZIONE ANNULLATA'," _
           & "                 NULL, 'PROPOSTA IN CORSO')" _
           & "            AS ESITO," _
           & "         ID_STATO," _
           & "         (CASE" _
           & "             WHEN (   motivazioni_ann_rif_all.motivazione" _
           & "                   || ' - '" _
           & "                   || rel_prat_all_ccaa_erp.motivazione) = ''" _
           & "             THEN" _
           & "        ''" _
           & "             WHEN motivazioni_ann_rif_all.motivazione IS NULL" _
           & "             THEN" _
           & "        rel_prat_all_ccaa_erp.motivazione" _
           & "             WHEN rel_prat_all_ccaa_erp.motivazione IS NULL" _
           & "             THEN" _
           & "        motivazioni_ann_rif_all.motivazione" _
           & "             WHEN (   motivazioni_ann_rif_all.motivazione" _
           & "                   || ' - '" _
           & "                   || rel_prat_all_ccaa_erp.motivazione)" _
           & "                     IS NOT NULL" _
           & "             THEN" _
           & "        motivazioni_ann_rif_all.motivazione" _
           & "                || ' - '" _
           & "                || rel_prat_all_ccaa_erp.motivazione" _
           & "          END)" _
           & "            AS motivazione_all" _
           & "        FROM COMP_NUCLEO," _
           & "             BANDI," _
           & "             REL_PRAT_ALL_CCAA_ERP," _
           & "             ALLOGGI," _
           & "             SISCOM_MI.UNITA_IMMOBILIARI," _
           & "             MOTIVAZIONI_ANN_RIF_ALL," _
           & "             t_tipo_indirizzo," _
           & "            DOMANDE_BANDO " _
           & "   WHERE     DATA_PROPOSTA >= '20141101'" _
           & "         AND COMP_NUCLEO.PROGR = 0" _
           & "         AND COMP_NUCLEO.ID_DICHIARAZIONE = DOMANDE_BANDO.ID_DICHIARAZIONE" _
           & "         AND BANDI.ID = DOMANDE_BANDO.ID_BANDO" _
           & "         AND DOMANDE_BANDO.id = REL_PRAT_ALL_CCAA_ERP.id_pratica" _
           & "         AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+)" _
           & "         AND ALLOGGI.COD_ALLOGGIO = UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE(+)" _
           & "         AND ALLOGGI.ID(+) = REL_PRAT_ALL_CCAA_ERP.ID_ALLOGGIO" _
           & "        AND MOTIVAZIONI_ANN_RIF_ALL.ID(+) =" _
           & "        rel_prat_all_ccaa_erp.ID_MOTIVAZIONE " _
           & lAddwhere _
           & "        UNION " _
           & "SELECT (case when (SELECT MAX (DOMANDE_OFFERTE_SCAD.id)" _
           & "FROM DOMANDE_OFFERTE_SCAD" _
           & "           WHERE     DOMANDE_OFFERTE_SCAD.id_domanda(+) = domande_bando_cambi.id" _
           & "                AND DOMANDE_OFFERTE_SCAD.DATA_SCADENZA = DATA_PROPOSTA) is null then (select max(n_offerta) from SISCOM_MI.UNITA_ASSEGNATE where id_domanda=domande_bando_cambi.id) else (SELECT MAX (DOMANDE_OFFERTE_SCAD.id)" _
           & "        FROM DOMANDE_OFFERTE_SCAD" _
           & "           WHERE     DOMANDE_OFFERTE_SCAD.id_domanda(+) = domande_bando_cambi.id" _
           & "                 AND DOMANDE_OFFERTE_SCAD.DATA_SCADENZA = DATA_PROPOSTA) end)" _
           & "            AS NUM_OFFERTA," _
           & "         DOMANDE_BANDO_CAMBI.PG AS PG_DOMANDA," _
           & "         '<a href=''javascript:void(0);'' onclick=""window.open(''../CAMBI/domanda.aspx?ID1=-1&PROGR=-1&LE=1&APP=1&US=1&ID='|| DOMANDE_BANDO_CAMBI.ID ||''',''Domanda_'|| DOMANDE_BANDO_CAMBI.PG ||''',''top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no'');"">'||DOMANDE_BANDO_CAMBI.PG ||'</a>' AS PG_DOMANDA1," _
           & "        'BANDO CAMBI' AS NOME_BANDO," _
           & "       'BANDO CAMBI' AS TIPO_BANDO," _
           & "         COMP_NUCLEO_CAMBI.COD_FISCALE AS CF_RICHIEDENTE, " _
           & "        COMP_NUCLEO_CAMBI.COGNOME," _
           & "        COMP_NUCLEO_CAMBI.NOME," _
           & "         (CASE" _
           & "             WHEN UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE IS NULL" _
           & "            THEN" _
           & "        COD_ALLOGGIO" _
           & "            ELSE  " _
           & "        UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE" _
           & "          END)" _
           & "            AS UNITA_IMMOBILIARE," _
           & "         INDIRIZZO," _
           & "         NUM_CIVICO," _
           & "         to_date (DATA_PROPOSTA,'yyyyMMdd') AS DATA_PROPOSTA," _
           & "         DECODE (ESITO," _
           & "                0, 'PROPOSTA RIFIUTATA'," _
           & "                1, 'PROPOSTA ACCETTATA'," _
           & "                3, 'PROPOSTA ANNULLATA'," _
           & "                 4, 'ASSEGNAZIONE ANNULLATA'," _
           & "                 NULL, 'PROPOSTA IN CORSO')" _
           & "            AS ESITO," _
           & "         ID_STATO," _
           & "         (CASE" _
           & "             WHEN (   motivazioni_ann_rif_all.motivazione" _
           & "                   || ' - '" _
           & "                   || rel_prat_all_ccaa_erp.motivazione) = ''" _
           & "             THEN" _
           & "        ''" _
           & "             WHEN motivazioni_ann_rif_all.motivazione IS NULL" _
           & "            THEN" _
           & "        rel_prat_all_ccaa_erp.motivazione" _
           & "             WHEN rel_prat_all_ccaa_erp.motivazione IS NULL" _
           & "             THEN" _
           & "        motivazioni_ann_rif_all.motivazione" _
           & "             WHEN (   motivazioni_ann_rif_all.motivazione" _
           & "                   || ' - '" _
           & "                   || rel_prat_all_ccaa_erp.motivazione)" _
           & "                     IS NOT NULL" _
           & "             THEN" _
           & "        motivazioni_ann_rif_all.motivazione" _
           & "                || ' - '" _
           & "                || rel_prat_all_ccaa_erp.motivazione" _
           & "          END)" _
           & "            AS motivazione_all" _
           & "        FROM COMP_NUCLEO_CAMBI," _
           & "             BANDI_CAMBIO," _
           & "             REL_PRAT_ALL_CCAA_ERP," _
           & "             ALLOGGI," _
           & "             SISCOM_MI.UNITA_IMMOBILIARI," _
           & "             MOTIVAZIONI_ANN_RIF_ALL," _
           & "             t_tipo_indirizzo," _
           & "             DOMANDE_BANDO_CAMBI" _
           & "        WHERE" _
           & "         DATA_PROPOSTA >= '20141101' AND" _
           & "        COMP_NUCLEO_CAMBI.PROGR = 0" _
           & "         AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE = DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE" _
           & "         AND BANDI_CAMBIO.ID = DOMANDE_BANDO_CAMBI.ID_BANDO" _
           & "          AND DOMANDE_BANDO_CAMBI.id = REL_PRAT_ALL_CCAA_ERP.id_pratica" _
           & "         AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+)" _
           & "         AND ALLOGGI.COD_ALLOGGIO = UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE(+)" _
           & "         AND ALLOGGI.ID(+) = REL_PRAT_ALL_CCAA_ERP.ID_ALLOGGIO" _
           & "         AND MOTIVAZIONI_ANN_RIF_ALL.ID(+) =" _
           & "        rel_prat_all_ccaa_erp.ID_MOTIVAZIONE" _
           & lAddwhere _
           & "        UNION " _
           & "SELECT (case when (SELECT MAX (DOMANDE_OFFERTE_SCAD.id)" _
           & "        FROM DOMANDE_OFFERTE_SCAD" _
           & "           WHERE     DOMANDE_OFFERTE_SCAD.id_domanda(+) = domande_bando_vsa.id" _
           & "                 AND DOMANDE_OFFERTE_SCAD.DATA_SCADENZA = DATA_PROPOSTA) is null then (select max(n_offerta) from SISCOM_MI.UNITA_ASSEGNATE where id_domanda=domande_bando_vsa.id) else (SELECT MAX (DOMANDE_OFFERTE_SCAD.id)" _
           & "        FROM DOMANDE_OFFERTE_SCAD " _
           & "           WHERE     DOMANDE_OFFERTE_SCAD.id_domanda(+) = domande_bando_vsa.id" _
           & "                 AND DOMANDE_OFFERTE_SCAD.DATA_SCADENZA = DATA_PROPOSTA) end)  " _
           & "            AS NUM_OFFERTA," _
           & "         DOMANDE_BANDO_VSA.PG AS PG_DOMANDA," _
           & "         '<a href=''javascript:void(0);'' onclick=""window.open(''../VSA/NuovaDomandaVSA/domandaNuova.aspx?ID1=-1&PROGR=-1&LE=1&APP=1&US=1&ID='|| DOMANDE_BANDO_VSA.ID ||''',''Domanda_'|| DOMANDE_BANDO_VSA.PG ||''');"">'||DOMANDE_BANDO_VSA.PG ||'</a>' AS PG_DOMANDA1," _
           & "        'BANDO CAMBI IN EMERGENZA' AS NOME_BANDO," _
           & "        'BANDO CAMBI IN EMERGENZA' AS TIPO_BANDO," _
           & "         COMP_NUCLEO_VSA.COD_FISCALE AS CF_RICHIEDENTE," _
           & "         COMP_NUCLEO_VSA.COGNOME," _
           & "         COMP_NUCLEO_VSA.NOME," _
           & "         (CASE" _
           & "             WHEN UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE IS NULL" _
           & "             THEN" _
           & "        COD_ALLOGGIO" _
           & "             ELSE" _
           & "        UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE" _
           & "          END)" _
           & "            AS UNITA_IMMOBILIARE," _
           & "         INDIRIZZO," _
           & "         NUM_CIVICO," _
           & "        to_date (DATA_PROPOSTA,'yyyyMMdd') AS DATA_PROPOSTA," _
           & "         DECODE (ESITO," _
           & "                 0, 'PROPOSTA RIFIUTATA'," _
           & "                 1, 'PROPOSTA ACCETTATA'," _
           & "                 3, 'PROPOSTA ANNULLATA'," _
           & "                 4, 'ASSEGNAZIONE ANNULLATA'," _
           & "                 NULL, 'PROPOSTA IN CORSO')" _
           & "            AS ESITO," _
           & "         ID_STATO," _
           & "         (CASE" _
           & "             WHEN (   motivazioni_ann_rif_all.motivazione" _
           & "                   || ' - '" _
           & "                   || rel_prat_all_ccaa_erp.motivazione) = ''" _
           & "             THEN" _
           & "        ''" _
           & "             WHEN motivazioni_ann_rif_all.motivazione IS NULL" _
           & "             THEN" _
           & "        rel_prat_all_ccaa_erp.motivazione" _
           & "             WHEN rel_prat_all_ccaa_erp.motivazione IS NULL" _
           & "             THEN" _
           & "        motivazioni_ann_rif_all.motivazione" _
           & "             WHEN (   motivazioni_ann_rif_all.motivazione" _
           & "                   || ' - '" _
           & "                   || rel_prat_all_ccaa_erp.motivazione)" _
           & "                     IS NOT NULL" _
           & "             THEN" _
           & "        motivazioni_ann_rif_all.motivazione" _
           & "                || ' - '" _
           & "                || rel_prat_all_ccaa_erp.motivazione" _
           & "          END)" _
           & "            AS motivazione_all" _
           & "        FROM COMP_NUCLEO_VSA," _
           & "             BANDI_VSA," _
           & "             REL_PRAT_ALL_CCAA_ERP," _
           & "             ALLOGGI," _
           & "             SISCOM_MI.UNITA_IMMOBILIARI," _
           & "             MOTIVAZIONI_ANN_RIF_ALL," _
           & "             t_tipo_indirizzo," _
           & "             DOMANDE_BANDO_VSA" _
           & "        WHERE" _
           & "         DATA_PROPOSTA >= '20141101' AND" _
           & "        COMP_NUCLEO_VSA.PROGR = 0" _
           & "         AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE" _
           & "         AND BANDI_VSA.ID = DOMANDE_BANDO_VSA.ID_BANDO" _
           & "         AND DOMANDE_BANDO_VSA.id = REL_PRAT_ALL_CCAA_ERP.id_pratica" _
           & "         AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+)" _
           & "         AND ALLOGGI.COD_ALLOGGIO = UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE(+)" _
           & "         AND ALLOGGI.ID(+) = REL_PRAT_ALL_CCAA_ERP.ID_ALLOGGIO" _
           & "         AND MOTIVAZIONI_ANN_RIF_ALL.ID(+) =" _
           & "        rel_prat_all_ccaa_erp.ID_MOTIVAZIONE" _
           & lAddwhere _
           & ") A " _
           & lAddwhere1 _
           & " order by 5,2,3 desc"

    End Sub

    Protected Sub btnAvviaRicerca_Click(sender As Object, e As System.EventArgs) Handles btnAvviaRicerca.Click
        BindGrid()
        dgvDocumenti.Rebind()
    End Sub

    Public Property sStrSql() As String
        Get
            If Not (ViewState("par_sStrSql") Is Nothing) Then
                Return CStr(ViewState("par_sStrSql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSql") = value
        End Set
    End Property

    Protected Sub dgvDocumenti_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvDocumenti.NeedDataSource
        TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSql)
    End Sub


End Class
