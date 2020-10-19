Imports Telerik.Web.UI
Imports System.IO
Imports SubSystems.RP
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports ExpertPdf.HtmlToPdf

Partial Class Gestione_locatari_Istanza
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Private Property idc As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            PageID.Value = par.getPageId

            lIdDichiarazione.Value = par.IfEmpty(Request.QueryString("IDD"), 0)
            primaApertura.Value = par.IfEmpty(Request.QueryString("ONE"), 0)
            RiempiCampi()
            'VisualizzaDati()
        End If
    End Sub

    Private Sub CaricaMotiviDecisioni()
        par.caricaComboTelerik("select t_motivi_diniego.* from t_motivi_diniego,motivi_tipo_istanza where t_motivi_diniego.id=motivi_tipo_istanza.id_tipo_motivo " _
                                   & " and id_fase_decisione=3 and id_tipo_istanza=" & idMotivoIstanza.Value & " order by t_motivi_diniego.id asc", cmbMotivoAcc, "id", "descrizione", False)

        par.caricaComboTelerik("select t_motivi_diniego.* from t_motivi_diniego,motivi_tipo_istanza where t_motivi_diniego.id=motivi_tipo_istanza.id_tipo_motivo " _
                                   & " and id_fase_decisione=1 and id_tipo_istanza=" & idMotivoIstanza.Value & " order by t_motivi_diniego.id asc", cmbMotivoPD, "id", "descrizione", False)

        par.caricaComboTelerik("select t_motivi_diniego.* from t_motivi_diniego,motivi_tipo_istanza where t_motivi_diniego.id=motivi_tipo_istanza.id_tipo_motivo " _
                                   & " and id_fase_decisione=5 and id_tipo_istanza=" & idMotivoIstanza.Value & " order by t_motivi_diniego.id asc", cmbMotivoDin, "id", "descrizione", False)


        cmbMotivoPD.Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='0';")
        cmbMotivoAcc.Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='0';")
        cmbMotivoDin.Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='0';")

    End Sub

    Private Sub CaricaRequisiti()
        par.caricaCheckBoxList("SELECT t_requisiti_processo.* FROM requisiti_tipo_processo,t_requisiti_processo where " _
                               & " requisiti_tipo_processo.id_requisito=t_requisiti_processo.id and id_tipo_processo=" & idMotivoIstanza.Value & " order by descrizione asc", chkRequisiti, "ID", "DESCRIZIONE")
    End Sub

    Private Sub CaricaDocumenti()
        par.caricaCheckBoxList("SELECT documenti_nuova_istanza.* FROM tipo_documenti_processi,documenti_nuova_istanza where " _
                               & " documenti_nuova_istanza.id=id_documento and id_tipo_processo=" & idMotivoIstanza.Value & " order by descrizione asc", chkDocumenti, "ID", "DESCRIZIONE")
    End Sub

    Private Sub VisualizzaDati()

        connData.apri(False)


        '/* GESTIONE LOCK */
        If Not IsPostBack Then
            hiddenLockCorrenti.Value = "ISTANZA" & lIdDichiarazione.Value
            Dim risultato = par.EseguiLock(PageID.Value, hiddenLockCorrenti.Value)
            Select Case risultato.esito
                Case SepacomLock.EsitoLockUnlock.Success
                    hdnSlLocked.Value = 0
                Case SepacomLock.EsitoLockUnlock.InUso
                    hdnSlLocked.Value = 1
                Case Else
                    ' ''Beep()
            End Select
        End If
        RadComboModPres.ClearSelection()
        ddlRuolo.ClearSelection()
        ddlSesso.ClearSelection()
        RadComboStato.ClearSelection()

        Dim protocollodom As String = ""
        Dim pgDichOrigine As String = ""

        Dim numComponenti As Integer = 0
        Dim mqNetti As Decimal = 0

        par.cmd.CommandText = "select dichiarazioni_vsa.data_pg,mod_presentazione,(select pg from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as protocollo, " _
            & " (select data_presentazione from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as data_pres, " _
            & " (select data_evento from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as data_ev, " _
            & "(select id_motivo_domanda from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as id_motivo_domanda," _
            & "(select id_causale_domanda from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as id_causale_domanda," _
            & " (select id_stato_istanza from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as id_stato_istanza, " _
            & " (select id_tipo_ospitalita from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as id_tipo_ospitalita, " _
            & " (select t_tipo_provenienza.descrizione from t_tipo_provenienza,domande_bando_vsa where tipo_d_import=t_tipo_provenienza.id " _
            & " and id_dichiarazione=dichiarazioni_vsa.id) as tipo_import, " _
            & " (select tipo_d_import from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as tipo_d_import, " _
            & " (select id_d_import from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as iddich, " _
            & " (select domande_bando_vsa.id from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as idistanza, " _
            & " (select domande_bando_vsa.id_contratto from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as idcontratto, " _
            & " (select data_archiviazione from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as data_arch, " _
            & " (select data_ricorso_tar from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as data_ricorso_tar, " _
            & " (select data_provv_cdm from domande_bando_vsa where id_dichiarazione=dichiarazioni_vsa.id) as data_provv_cdm " _
            & " from dichiarazioni_vsa where id=" & lIdDichiarazione.Value
        Dim reader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If reader0.Read Then
            txtDataApertura.Text = par.FormattaData(par.IfNull(reader0("data_pg"), Format(Now, "yyyyMMdd")))
            dataApertura.Value = txtDataApertura.Text
            lIdContratto.Value = par.IfNull(reader0("idcontratto"), 0)
            idMotivoIstanza.Value = par.IfNull(reader0("id_motivo_domanda"), 0)
            RadComboMotiviSub.SelectedValue = par.IfNull(reader0("id_causale_domanda"), "")
            lIdIstanza.Value = par.IfNull(reader0("idistanza"), 0)
            protocollodom = par.IfNull(reader0("protocollo"), 0)
            RadDatePresentaz.SelectedDate = par.FormattaData(par.IfNull(reader0("data_pres"), Format(Now, "yyyyMMdd")))
            RadDateEvento.SelectedDate = par.FormattaData(par.IfNull(reader0("data_ev"), Format(Now, "yyyyMMdd")))
            RadComboModPres.SelectedValue = par.IfNull(reader0("mod_presentazione"), "")
            RadComboStato.SelectedValue = par.IfNull(reader0("id_stato_istanza"), "")
            RadComboTipo.SelectedValue = par.IfNull(reader0("id_tipo_ospitalita"), -1)
            idTipoProcesso.Value = par.IfNull(reader0("id_tipo_ospitalita"), "")
            lblDichOrigine.Text = par.IfNull(reader0("tipo_import"), "- - -")
            If par.IfNull(reader0("data_ricorso_tar"), "") <> "" Then
                RadDateTar.SelectedDate = par.FormattaData(reader0("data_ricorso_tar"))
            End If
            If par.IfNull(reader0("data_provv_cdm"), "") <> "" Then
                RadDateProvv.SelectedDate = par.FormattaData(reader0("data_provv_cdm"))
            End If
            lblLinkOrigine.Text = ""
            If par.IfNull(reader0("data_arch"), "") <> "" Then
                txtDataArchiviazione.Text = par.FormattaData(reader0("data_arch"))
                btnArchivia.Enabled = False
                btnApprova.Enabled = False
                btnRespingi.Enabled = False
            End If

            If par.IfNull(reader0("tipo_d_import"), 1) = 1 Then
                par.cmd.CommandText = "select pg from dichiarazioni_vsa where id=" & par.IfNull(reader0("iddich"), 1)
                pgDichOrigine = par.IfNull(par.cmd.ExecuteScalar, "0")

                par.cmd.CommandText = "select t_motivo_domanda_vsa.descrizione as mot_domanda from domande_bando_vsa,t_motivo_domanda_vsa " _
                    & " where domande_bando_vsa.id_motivo_domanda=t_motivo_domanda_vsa.id and id_dichiarazione=" & par.IfNull(reader0("iddich"), 1)
                lblDichOrigine.Text = par.IfNull(par.cmd.ExecuteScalar, "")

                lblLinkOrigine.Text = "PG <a href='javascript:void(0)' onclick=" & Chr(34) & "today = new Date();window.open('../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?LE=1&CH=2&ID=" & par.IfNull(reader0("IDDICH"), 0) & "','Dich'+ today.getMinutes() + today.getSeconds(),'top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');" & Chr(34) & ">" & pgDichOrigine & " </a>"

            Else
                par.cmd.CommandText = "select pg from utenza_dichiarazioni where id=" & par.IfNull(reader0("iddich"), 1)
                pgDichOrigine = par.IfNull(par.cmd.ExecuteScalar, "0")

                If pgDichOrigine <> 0 Then
                    lblLinkOrigine.Text = "PG <a href='javascript:void(0)' onclick=" & Chr(34) & "today = new Date();window.open('../ANAUT/DichAUnuova.aspx?LE=1&CH=2&ID=" & par.IfNull(reader0("IDDICH"), 0) & "','Dich'+ today.getMinutes() + today.getSeconds(),'top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');" & Chr(34) & ">" & pgDichOrigine & " </a>"
                End If
            End If
        End If
        reader0.Close()

        lblIstanza.Text = protocollodom

        par.cmd.CommandText = "select * from t_motivo_domanda_vsa where id=" & idMotivoIstanza.Value
        Dim readerint As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If readerint.Read Then
            lblTitolo.Text = "Istanza di " & par.IfNull(readerint("descrizione"), "") & " " & protocollodom
        End If
        readerint.Close()

        '******* nascondo parametri in riferimento al tab ospiti per gli altri processi
        If idMotivoIstanza.Value <> "7" And idMotivoIstanza.Value <> "13" Then
            PanelDatiOspiti.Style.Value = "display : none;"
            RadTabStrip1.Tabs(2).Visible = False
            RadMultiPage1.PageViews(2).Visible = False
        End If
        '*******

        If idMotivoIstanza.Value = "1" Then
            btnInserisciNucleo.Visible = False
            PanelDatiSubentro.Style.Value = "display : block;"
        End If

        'Nascondo Tab Redditi per tutti i processi
        RadTabStrip1.Tabs(3).Visible = False

        If idMotivoIstanza.Value = "7" Then
            PanelRedditi.Visible = False
            RadTabStrip1.Tabs(3).Visible = False
            RadMultiPage1.PageViews(3).Visible = False
        End If

        If idMotivoIstanza.Value = "13" Then
            RadTabStrip1.Tabs(2).Text = "Coabitanti"
            PanelDatiOspiti.Style.Value = "display : none;"
        End If

        'Nascondo tab ISEE
        If idMotivoIstanza.Value > 2 Then
            RadTabStrip1.Tabs(1).Visible = False
            RadMultiPage1.PageViews(1).Visible = False
        End If


        par.cmd.CommandText = "select comp_nucleo_vsa.* from comp_nucleo_vsa where id_dichiarazione=" & lIdDichiarazione.Value & " and progr=0"
        readerint = par.cmd.ExecuteReader
        If readerint.Read Then
            txtCognome.Text = par.IfNull(readerint("cognome"), "")
            txtNome.Text = par.IfNull(readerint("nome"), "")
            txtCodiceFiscale.Text = par.IfNull(readerint("cod_fiscale"), "")
            txtDataNascita.SelectedDate = par.FormattaData(par.IfNull(readerint("data_nascita"), ""))
            ddlSesso.SelectedValue = par.IfNull(readerint("sesso"), "m")
            ddlRuolo.SelectedValue = par.IfNull(readerint("grado_parentela"), "1")
        End If
        readerint.Close()


        par.cmd.CommandText = "select id_luogo_nas_dnte from dichiarazioni_vsa where id=" & lIdDichiarazione.Value
        Dim myreader5B As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If myreader5B.Read() Then
            par.cmd.CommandText = "select * from comuni_nazioni where id=" & myreader5B("id_luogo_nas_dnte")
            Dim myreader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myreader1.Read() Then
                If myreader1("sigla") = "E" Or myreader1("sigla") = "C" Then
                    ddlNazione.SelectedItem.Text = myreader1("nome")
                    ddlNazione.Text = ddlNazione.SelectedItem.Text.ToString
                Else
                    ddlNazione.SelectedItem.Text = "ITALIA"
                    ddlNazione.Text = ddlNazione.SelectedItem.Text.ToString
                    acbComune.Entries.Insert(0, New AutoCompleteBoxEntry(myreader1("nome")))
                End If
            End If
            myreader1.Close()
        End If
        myreader5B.Close()

        par.cmd.CommandText = "select * from INFO_ISEE_ISTANZA where id_istanza=" & lIdIstanza.Value
        Dim myReaderIse As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderIse.Read Then
            txtISE.Text = par.VirgoleInPunti(CStr(par.IfNull(myReaderIse("ISE").ToString.Replace(".", ""), 0)))
            txtISEE.Text = par.VirgoleInPunti(CStr(par.IfNull(myReaderIse("ISEE").ToString.Replace(".", ""), 0)))
            txtISP.Text = par.VirgoleInPunti(CStr(par.IfNull(myReaderIse("ISP").ToString.Replace(".", ""), 0)))
            txtISR.Text = par.VirgoleInPunti(CStr(par.IfNull(myReaderIse("ISR").ToString.Replace(".", ""), 0)))
            txtProtocolloDSU.Text = par.IfNull(myReaderIse("NUM_DSU"), "")
        End If
        myReaderIse.Close()

        If idMotivoIstanza.Value = "7" Then
            par.cmd.CommandText = "select id from siscom_mi.unita_immobiliari where cod_unita_immobiliare in (select cod_unita_immobiliare from DOMANDE_VSA_ALLOGGIO where id_domanda=" & lIdIstanza.Value & ")"
            idUnita.Value = par.IfNull(par.cmd.ExecuteScalar, "0")

            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=" & idUnita.Value
            mqNetti = par.IfNull(par.cmd.ExecuteScalar, "0")

            par.cmd.CommandText = "select sum(count(id)+(select count(id) from comp_nucleo_ospiti_vsa where id_domanda = " & lIdIstanza.Value & ")) as numComp from comp_nucleo_vsa where id_dichiarazione = " & lIdDichiarazione.Value & " group by id_dichiarazione"
            numComponenti = par.IfNull(par.cmd.ExecuteScalar, "0")
        End If
        If idUnita.Value = "0" Then
            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE id_unita_principale is null and ID= (SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE " _
                & " WHERE id_unita_principale is null and ID_CONTRATTO=" & lIdContratto.Value & ")"
            Dim myReader1234 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1234.Read() Then
                idUnita.Value = par.IfNull(myReader1234("ID"), 0)
            End If
            myReader1234.Close()
        End If
        par.cmd.CommandText = "select * from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value & " " _
                   & " and ID_STATO_DECISIONE=6"
        Dim myReaderSD3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderSD3.Read Then
            dataAutorizz.Value = par.IfNull(myReaderSD3("data"), Format(Now, "yyyyMMdd"))
        End If
        myReaderSD3.Close()

        AbilitaTabDecisioni()
        RicavaGiorni()
        CaricaRequisiti()
        CaricaMotiviDecisioni()
        LeggiMotivi()

        For Each Items As ListItem In chkRequisiti.Items
            If idMotivoIstanza.Value <> "7" Or primaApertura.Value = "1" Then
                Items.Selected = True
            Else
                par.cmd.CommandText = "select * from REQUISITI_NUOVA_ISTANZA where id_requisito=" & Items.Value & " and id_istanza = " & lIdIstanza.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettore.Read Then
                    If Items.Value = "2" Then
                        forzaRequisito.Value = par.IfNull(lettore("forza_flag"), "0")
                    End If
                    Items.Selected = True
                Else
                    Items.Selected = False
                End If
                lettore.Close()
                If Items.Value = "1" Then
                    Items.Selected = True
                End If
            End If
        Next

        If idMotivoIstanza.Value = "7" Or idMotivoIstanza.Value = "13" Then
            If forzaRequisito.Value = "0" Then
                If VerificaSovraffollamento(numComponenti, mqNetti) = False Then
                    alertRequisiti = False
                    par.cmd.CommandText = "SELECT * FROM REQUISITI_NUOVA_ISTANZA WHERE ID_ISTANZA=" & lIdIstanza.Value & " AND ID_REQUISITO=2"
                    Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lett.Read = False Then
                        par.cmd.CommandText = "INSERT INTO REQUISITI_NUOVA_ISTANZA (ID_ISTANZA,ID_REQUISITO) VALUES (" & lIdIstanza.Value & ",2)"
                        par.cmd.ExecuteNonQuery()
                    End If
                    lett.Close()
                Else
                    alertRequisiti = True
                    par.cmd.CommandText = "SELECT * FROM REQUISITI_NUOVA_ISTANZA WHERE ID_ISTANZA=" & lIdIstanza.Value & " AND ID_REQUISITO=2"
                    Dim lett2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lett2.Read Then
                        par.cmd.CommandText = "DELETE FROM REQUISITI_NUOVA_ISTANZA WHERE ID_ISTANZA=" & lIdIstanza.Value & " AND ID_REQUISITO=2"
                        par.cmd.ExecuteNonQuery()
                    End If
                    lett2.Close()
                End If
            Else
                alertRequisiti = False
            End If
        End If


        CaricaDocumenti()

        For Each Items As ListItem In chkDocumenti.Items
            par.cmd.CommandText = "select * from DOC_ALLEGATI_NUOVA_ISTANZA where id_documento=" & Items.Value & " and id_istanza = " & lIdIstanza.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore.Read Then
                Items.Selected = True
            Else
                Items.Selected = False
            End If
            lettore.Close()
        Next


        connData.chiudi(False)

    End Sub

    Private Sub AbilitaTabDecisioni()

        '1	PREAVVISO DI DINIEGO
        '2	SOTTOPOSTA A REVISIONE
        '3	ACCOLTA
        '4	SOTTOPOSTA A DECISIONE
        '5	DINIEGATA
        '6	APPLICATA
        '7	RESPINTA

        chkControdeduz.Enabled = False
        'RadTextBoxMotivoC.Enabled = False
        RadTextBoxNInt.Enabled = False
        RadDateControdeduzioni.Enabled = False

        chkDiniego.Enabled = False
        cmbMotivoDin.Enabled = False
        RadTextBoxMotDin.Enabled = False
        RadTextBoxNotaDin.Enabled = False
        RadDateDiniego.Enabled = False

        par.cmd.CommandText = "select distinct ITER_AUTORIZZATIVO_ISTANZA.id_stato_Decisione,ITER_AUTORIZZATIVO_ISTANZA.* from ITER_AUTORIZZATIVO_ISTANZA " _
                & " where id_istanza=" & lIdIstanza.Value & " order by ITER_AUTORIZZATIVO_ISTANZA.ID Asc"
        Dim daAutorizz As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtAutorizz As New Data.DataTable

        daAutorizz.Fill(dtAutorizz)
        daAutorizz.Dispose()

        If dtAutorizz.Rows.Count > 0 Then
            For Each row As Data.DataRow In dtAutorizz.Rows
                If row.Item("ID_STATO_DECISIONE") < 6 Then

                End If
                Select Case row.Item("ID_STATO_DECISIONE")
                    Case "1"
                        ' DisabilitaForm()
                        RadDatePreavvDiniego.SelectedDate = CDate(par.FormattaData(row.Item("DATA")))
                        ' RadTextBoxMotivoPD.Text = par.IfNull((row.Item("MOTIVO")), "")
                        RadTextBoxNotaPD.Text = par.IfNull((row.Item("NOTE")), "")
                        chkPreavviso.Checked = True

                        If chkControdeduz.Enabled = False Then
                            ' AbilitaForm()
                            btnSottoponi.Enabled = True
                            btnApprova.Enabled = False
                            btnRespingi.Enabled = False
                        End If

                        chkAccogli.Enabled = False
                        RadTextBoxAccogliAcc.Enabled = False
                        RadTextBoxMotivoAcc.Enabled = False
                        RadDateAccoglimento.Enabled = False
                        cmbMotivoAcc.Enabled = False

                        'Dim motiviSelezionati As String = par.IfEmpty(row.Item("MOTIVO").ToString, "")
                        'If Not String.IsNullOrEmpty(Trim(motiviSelezionati)) Then
                        '    Dim StatiSelezionatiS() As String = Nothing
                        '    StatiSelezionatiS = motiviSelezionati.Split(",")
                        '    For i As Integer = 0 To StatiSelezionatiS.Length - 1
                        '        If Not IsNothing(cmbMotivoPD.Items.FindItemByText(StatiSelezionatiS(i))) Then
                        '            cmbMotivoPD.Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='0';")
                        '            cmbMotivoPD.Items.FindItemByText(StatiSelezionatiS(i)).Checked = True
                        '        End If
                        '    Next
                        'End If

                    Case "2"
                        ' DisabilitaForm()
                        chkControdeduz.Checked = True
                        RadDateControdeduzioni.SelectedDate = CDate(par.FormattaData(row.Item("DATA")))
                        ' RadTextBoxMotivoC.Text = par.IfNull((row.Item("MOTIVO")), "")
                        RadTextBoxNInt.Text = par.IfNull((row.Item("NOTE")), "")

                        chkDiniego.Enabled = True
                        cmbMotivoDin.Enabled = True
                        RadTextBoxMotDin.Enabled = True
                        RadTextBoxNotaDin.Enabled = True
                        RadDateDiniego.Enabled = True

                        chkControdeduz.Enabled = True
                        'RadTextBoxMotivoC.Enabled = True
                        RadTextBoxNInt.Enabled = True
                        RadDateControdeduzioni.Enabled = True


                        chkAccogli.Enabled = True
                        RadTextBoxAccogliAcc.Enabled = True
                        RadTextBoxMotivoAcc.Enabled = True
                        RadDateAccoglimento.Enabled = True
                        cmbMotivoAcc.Enabled = True
                        btnApprova.Enabled = False
                        btnRespingi.Enabled = False
                        btnSottoponi.Enabled = False
                    Case "3"
                        '******DisabilitaDecisioni(row.Item("ID_STATO_DECISIONE"))
                        btnSottoponi.Enabled = True
                        chkPreavviso.Enabled = False
                        cmbMotivoPD.Enabled = False
                        RadTextBoxMotivoPD.Enabled = False
                        RadTextBoxNotaPD.Enabled = False
                        RadDatePreavvDiniego.Enabled = False

                        chkAccogli.Checked = True
                        RadDateAccoglimento.SelectedDate = CDate(par.FormattaData(row.Item("DATA")))
                        '  RadTextBoxMotivoAcc.Text = par.IfNull((row.Item("MOTIVO")), "")
                        RadTextBoxAccogliAcc.Text = par.IfNull((row.Item("NOTE")), "")

                        'If flSalva <> 1 Then
                        'Dim motiviSelezionati As String = par.IfEmpty(row.Item("MOTIVO").ToString, "")
                        'If Not String.IsNullOrEmpty(Trim(motiviSelezionati)) Then
                        '    Dim StatiSelezionatiS() As String = Nothing
                        '    StatiSelezionatiS = motiviSelezionati.Split(",")
                        '    For i As Integer = 0 To StatiSelezionatiS.Length - 1
                        '        If Not IsNothing(cmbMotivoAcc.Items.FindItemByText(StatiSelezionatiS(i))) Then
                        '            cmbMotivoAcc.Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='0';")
                        '            cmbMotivoAcc.Items.FindItemByText(StatiSelezionatiS(i)).Checked = True
                        '        End If
                        '    Next
                        'End If
                        'End If
                    Case "4"
                        'DisabilitaForm()

                        chkAccogli.Enabled = False
                        RadTextBoxAccogliAcc.Enabled = False
                        RadTextBoxMotivoAcc.Enabled = False
                        RadDateAccoglimento.Enabled = False
                        cmbMotivoAcc.Enabled = False
                        '******DisabilitaDecisioni(row.Item("ID_STATO_DECISIONE"))


                    Case "5"
                        ' DisabilitaDecisioni(row.Item("ID_STATO_DECISIONE"))

                        chkDiniego.Checked = True
                        RadDateDiniego.SelectedDate = CDate(par.FormattaData(row.Item("DATA")))
                        'RadTextBoxMotDin.Text = par.IfNull((row.Item("MOTIVO")), "")
                        RadTextBoxNotaDin.Text = par.IfNull((row.Item("NOTE")), "")

                        btnSottoponi.Enabled = True

                        'If flSalva <> 1 Then
                        'Dim motiviSelezionati As String = par.IfEmpty(row.Item("MOTIVO").ToString, "")
                        'If Not String.IsNullOrEmpty(Trim(motiviSelezionati)) Then
                        '    Dim StatiSelezionatiS() As String = Nothing
                        '    StatiSelezionatiS = motiviSelezionati.Split(",")
                        '    For i As Integer = 0 To StatiSelezionatiS.Length - 1
                        '        If Not IsNothing(cmbMotivoDin.Items.FindItemByText(StatiSelezionatiS(i))) Then
                        '            cmbMotivoDin.Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='0';")
                        '            cmbMotivoDin.Items.FindItemByText(StatiSelezionatiS(i)).Checked = True
                        '        End If
                        '    Next
                        'End If
                        'End If
                    Case "6"
                        DisabilitaForm()
                        btnArchivia.Enabled = True
                        btnApprova.Enabled = False
                        btnRespingi.Enabled = False
                    Case "7"

                        AbilitaForm()
                        btnSottoponi.Enabled = False
                        chkPreavviso.Enabled = True
                        RadDatePreavvDiniego.Enabled = True
                        cmbMotivoPD.Enabled = True
                        RadTextBoxMotivoPD.Enabled = True
                        'RadTextBoxMotivoC.Enabled = False
                        'RadTextBoxNInt.Enabled = False
                        'RadDateControdeduzioni.Enabled = False

                        chkDiniego.Enabled = True
                        cmbMotivoDin.Enabled = True
                        RadTextBoxMotDin.Enabled = True
                        RadTextBoxNotaDin.Enabled = True
                        RadDateDiniego.Enabled = True

                        btnApprova.Enabled = False
                        btnRespingi.Enabled = False
                        chkAccogli.Enabled = True
                        RadTextBoxAccogliAcc.Enabled = True
                        RadTextBoxMotivoAcc.Enabled = True
                        RadDateAccoglimento.Enabled = True
                        cmbMotivoAcc.Enabled = True
                        btnArchivia.Enabled = False

                    Case "9"

                        chkControdeduz.Enabled = True
                        'RadTextBoxMotivoC.Enabled = True
                        RadTextBoxNInt.Enabled = True
                        RadDateControdeduzioni.Enabled = True

                        chkDiniego.Enabled = True
                        cmbMotivoDin.Enabled = True
                        RadTextBoxMotDin.Enabled = True
                        RadTextBoxNotaDin.Enabled = True
                        RadDateDiniego.Enabled = True
                        btnApprova.Enabled = False
                        btnRespingi.Enabled = False
                        btnSottoponi.Enabled = False
                    Case "10"
                        If idMotivoIstanza.Value = "1" Then
                            If IsNothing(RadDateTar.SelectedDate) Then
                                DisabilitaForm()
                                btnApprova.Enabled = False
                                btnRespingi.Enabled = False
                                btnArchivia.Enabled = True
                            Else
                                AbilitaForm()
                                btnSottoponi.Enabled = False

                                chkDiniego.Enabled = True
                                cmbMotivoDin.Enabled = True
                                RadTextBoxMotDin.Enabled = True
                                RadTextBoxNotaDin.Enabled = True
                                RadDateDiniego.Enabled = True

                                btnApprova.Enabled = False
                                btnRespingi.Enabled = False
                                chkAccogli.Enabled = True
                                RadTextBoxAccogliAcc.Enabled = True
                                RadTextBoxMotivoAcc.Enabled = True
                                RadDateAccoglimento.Enabled = True
                                cmbMotivoAcc.Enabled = True

                                chkPreavviso.Enabled = False

                                RadComboStato.ClearSelection()
                                RadComboStato.SelectedValue = "4"
                            End If
                        Else
                            DisabilitaForm()
                            btnApprova.Enabled = False
                            btnRespingi.Enabled = False
                            btnArchivia.Enabled = True
                        End If

                    Case "11"
                        DisabilitaDecisioni("")
                        btnSottoponi.Enabled = True
                End Select

            Next
            If dtAutorizz.Rows(dtAutorizz.Rows.Count - 1).Item("id_stato_decisione") = "4" Then
                DisabilitaForm()
            End If
            If dtAutorizz.Rows(dtAutorizz.Rows.Count - 1).Item("id_stato_decisione") = "1" Then
                btnSottoponi.Enabled = True
            End If
        Else
            DisabilitaDecisioni("")
        End If

        If btnApprova.Enabled = True Then
            If Session.Item("OP_RESP_VSA") = 0 Then
                btnApprova.Enabled = False
            End If
        End If
        If btnRespingi.Enabled = True Then
            If Session.Item("OP_RESP_VSA") = 0 Then
                btnRespingi.Enabled = False
            End If
        End If
        CType(Master.FindControl("frmModify"), HiddenField).Value = "0"

    End Sub

    Private Sub ScriviMotivi()

        par.cmd.CommandText = "delete from MOTIVI_DECISIONI_ISTANZA where id_istanza=" & lIdIstanza.Value
        par.cmd.ExecuteNonQuery()

        For Each Item As RadComboBoxItem In cmbMotivoPD.Items
            If chkPreavviso.Checked = True Then
                If Item.Checked = True Then
                    par.cmd.CommandText = "INSERT INTO MOTIVI_DECISIONI_ISTANZA (id_istanza,id_motivo) values (" & lIdIstanza.Value & "," _
                        & "(select id from motivi_tipo_istanza where id_tipo_motivo=" & Item.Value & " and id_fase_decisione=1))"
                    par.cmd.ExecuteNonQuery()
                End If
            End If
        Next
        For Each Item As RadComboBoxItem In cmbMotivoAcc.Items
            If chkAccogli.Checked = True Then
                If Item.Checked = True Then
                    par.cmd.CommandText = "INSERT INTO MOTIVI_DECISIONI_ISTANZA (id_istanza,id_motivo) values (" & lIdIstanza.Value & "," _
                        & "(select id from motivi_tipo_istanza where id_tipo_motivo=" & Item.Value & " and id_fase_decisione=3))"
                    par.cmd.ExecuteNonQuery()
                End If
            End If
        Next
        For Each Item As RadComboBoxItem In cmbMotivoDin.Items
            If chkDiniego.Checked = True Then
                If Item.Checked = True Then
                    par.cmd.CommandText = "INSERT INTO MOTIVI_DECISIONI_ISTANZA (id_istanza,id_motivo) values (" & lIdIstanza.Value & "," _
                        & "(select id from motivi_tipo_istanza where id_tipo_motivo=" & Item.Value & " and id_fase_decisione=5))"
                    par.cmd.ExecuteNonQuery()
                End If
            End If
        Next

    End Sub

    Private Sub LeggiMotivi()

        par.cmd.CommandText = "select motivi_tipo_istanza.* from motivi_decisioni_istanza,motivi_tipo_istanza,t_motivi_diniego where motivi_tipo_istanza.id=motivi_decisioni_istanza.id_motivo " _
            & " And id_fase_Decisione=1 And t_motivi_diniego.id=motivi_tipo_istanza.id_tipo_motivo And id_istanza=" & lIdIstanza.Value
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        da.Dispose()
        For Each row As Data.DataRow In dt.Rows
            cmbMotivoPD.Items.FindItemByValue(row.Item("id_tipo_motivo")).Checked = True
        Next
        par.cmd.CommandText = "select motivi_tipo_istanza.* from motivi_decisioni_istanza,motivi_tipo_istanza,t_motivi_diniego where motivi_tipo_istanza.id=motivi_decisioni_istanza.id_motivo " _
           & " And id_fase_Decisione=3 And t_motivi_diniego.id=motivi_tipo_istanza.id_tipo_motivo And id_istanza=" & lIdIstanza.Value
        Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt2 As New Data.DataTable
        da2.Fill(dt2)
        da2.Dispose()
        For Each row2 As Data.DataRow In dt2.Rows
            cmbMotivoAcc.Items.FindItemByValue(row2.Item("id_tipo_motivo")).Checked = True
        Next
        par.cmd.CommandText = "select motivi_tipo_istanza.* from motivi_decisioni_istanza,motivi_tipo_istanza,t_motivi_diniego where motivi_tipo_istanza.id=motivi_decisioni_istanza.id_motivo " _
           & " And id_fase_Decisione=5 And t_motivi_diniego.id=motivi_tipo_istanza.id_tipo_motivo And id_istanza=" & lIdIstanza.Value
        Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt3 As New Data.DataTable
        da3.Fill(dt3)
        da3.Dispose()
        For Each row3 As Data.DataRow In dt3.Rows
            cmbMotivoDin.Items.FindItemByValue(row3.Item("id_tipo_motivo")).Checked = True
        Next
    End Sub

    Private Sub RiempiCampi()
        Try
            par.caricaComboTelerik("select * from t_tipo_parentela where cod<>'8' and cod<>'17' and cod<>'24' and cod<>'26' and cod<>'28' and cod<>'30' order by cod asc", ddlRuolo, "cod", "descrizione", True, "null", , , False)
            par.caricaComboTelerik("select * from t_motivo_presentaz_vsa where fl_nuova_normativa=1 order by id asc", RadComboModPres, "id", "descrizione", False)
            par.caricaComboTelerik("select * from t_stato_istanza order by id asc", RadComboStato, "id", "descrizione", False)
            par.caricaComboTelerik("select * from t_tipo_ospitalita order by id asc", RadComboTipo, "id", "descrizione", True)


            par.caricaComboTelerik("select * from t_causali_domanda_vsa where id_motivo=1 and fl_nuova_normativa=1 order by cod asc", RadComboMotiviSub, "COD", "DESCRIZIONE", True)


            Dim queryNazioni As String = "SELECT * FROM (SELECT 'ITALIA' AS COD, 'ITALIA' AS NOME FROM DUAL " _
                                           & "UNION " _
                                           & "SELECT COD, NOME FROM COMUNI_NAZIONI WHERE LENGTH(SIGLA) = 1 AND COD IS NOT NULL) A " _
                                           & "ORDER BY A.NOME ASC"
            par.caricaComboTelerik(queryNazioni, ddlNazione, "COD", "NOME", False, , , , False)
            ddlNazione.SelectedValue = "ITALIA"
            ddlNazione.Text = ddlNazione.SelectedItem.Text.ToString

        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub RicavaMotivoDomanda()
        Try
            connData.apri(True)
            Dim lvalorecorrente As Long = 0
            Dim valorepg As String = ""
            par.cmd.CommandText = "select * from t_motivo_domanda_vsa where id=" & idMotivoIstanza.Value
            Dim readerint As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If readerint.Read Then
                lblTitolo.Text = "Istanza di " & par.IfNull(readerint("descrizione"), "") & " "
            End If
            readerint.Close()

            par.cmd.CommandText = "select max(id) from num_protocolli_vsa"
            Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myreader.Read() Then
                lvalorecorrente = par.IfNull(myreader(0), 0) + 1
            End If
            myreader.Close()
            par.cmd.CommandText = "insert into num_protocolli_vsa values (" & lvalorecorrente & ")"
            par.cmd.ExecuteNonQuery()
            valorepg = Format(lvalorecorrente, "0000000000")

            lblTitolo.Text &= valorepg

            connData.chiudi(True)

        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub dgvnucleo_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvNucleo.NeedDataSource
        Try
            Dim apertanow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                apertanow = True
            End If
            Dim query As String = "select comp_nucleo_vsa.*,to_date(data_nascita, 'yyyymmdd') as data_nasc,to_date(data_ingresso_nucleo, 'yyyymmdd') as data_ingresso,data_ingresso_nucleo," _
               & " (case when nvl(data_ingresso_nucleo,'null') = 'null' then 'NO' else 'SI' end) as nuovo_comp,(select descrizione from natura_invalidita where id=id_natura_inval) as natura_invalidita,(case when nvl(indennita_acc,'0') ='0' then 'NO' else 'SI' end) as ind_accomp, t_tipo_parentela.descrizione," _
               & " (case when tipo_inval ='D' then 'DEFINITIVA' when tipo_inval ='P' then 'PROVVISORIA' else '' end) as tipo_invalidita from comp_nucleo_vsa,t_tipo_parentela,nuovi_comp_nucleo_vsa " _
               & " where comp_nucleo_vsa.grado_parentela=t_tipo_parentela.cod(+) and comp_nucleo_vsa.id=nuovi_comp_nucleo_vsa.id_componente(+) and comp_nucleo_vsa.id_dichiarazione=" & lIdDichiarazione.Value & " order by progr asc"

            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(query)
            If apertanow Then
                connData.chiudi(False)
            End If
            dgvNucleo.DataSource = dt

            VisualizzaDati()
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub dgvospiti_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvOspiti.NeedDataSource
        Try
            Dim apertanow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                apertanow = True
            End If
            Dim query As String = "select data_agg,id, cognome, nome, to_date(data_nasc, 'yyyymmdd') as data_nasc, cod_fiscale," _
            & "to_date(data_inizio_ospite, 'yyyymmdd') as data_inizio_ospite, to_date(data_fine_ospite, 'yyyymmdd') as data_fine_ospite " _
            & " from comp_nucleo_ospiti_vsa where id_domanda = (select id from domande_bando_vsa where id_dichiarazione=" & lIdDichiarazione.Value & ") order by data_agg desc"

            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(query)
            If apertanow Then
                connData.chiudi(False)
            End If
            dgvOspiti.DataSource = dt
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub dgvredditi_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvRedditi.NeedDataSource
        Try
            Dim apertanow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                apertanow = True
            End If

            Dim query As String = "select comp_nucleo_vsa.id as idcomp,domande_redditi_vsa.id as idredd,comp_nucleo_vsa.*,to_number(nvl(dipendente,0)) as dipendente,to_number(nvl(pensione,0)) as pensione,(nvl(non_imponibili,0)+ nvl(pens_esente,0)) as pensione2," _
            & " nvl(autonomo,0)+nvl(dom_ag_fab,0)+nvl(occasionali,0) as autonomo1,nvl(oneri,0)+nvl(no_isee,0) as no_isee," _
            & " nvl(dipendente,0)+nvl(pensione,0)+ nvl(autonomo,0)+nvl(non_imponibili,0)+  nvl(dom_ag_fab,0) + nvl(occasionali,0) + nvl(oneri,0)+ nvl(pens_esente,0)+nvl(no_isee,0) as tot_redditi " _
            & " from comp_nucleo_vsa,domande_redditi_vsa " _
            & "where comp_nucleo_vsa.id_dichiarazione = " & lIdDichiarazione.Value & " and comp_nucleo_vsa.id=domande_redditi_vsa.id_componente order by domande_redditi_vsa.id asc"

            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(query)
            If apertanow Then
                connData.chiudi(False)
            End If
            dgvRedditi.DataSource = dt
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub dgvdetrazioni_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvDetrazioni.NeedDataSource
        Try
            Dim apertanow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                apertanow = True
            End If

            Dim query As String = "select comp_nucleo_vsa.id as idcomp,comp_detrazioni_vsa.id as iddetr,comp_nucleo_vsa.cognome,comp_nucleo_vsa.nome," _
                                  & "comp_detrazioni_vsa.importo,t_tipo_detrazioni.descrizione,comp_detrazioni_vsa.importo as tot_detraz " _
                                  & " from t_tipo_detrazioni,comp_detrazioni_vsa,comp_nucleo_vsa where comp_detrazioni_vsa.id_componente=comp_nucleo_vsa.id " _
                                  & " and comp_nucleo_vsa.id_dichiarazione = " & lIdDichiarazione.Value & " and comp_detrazioni_vsa.id_tipo=t_tipo_detrazioni.cod(+) " _
                                  & " order by comp_detrazioni_vsa.id_componente asc"
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(query)
            If apertanow Then
                connData.chiudi(False)
            End If
            dgvDetrazioni.DataSource = dt
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub dgvspese_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvSpese.NeedDataSource
        Try
            Dim apertanow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                apertanow = True
            End If
            Dim query As String = "select comp_elenco_spese_vsa.id,comp_nucleo_vsa.cognome,comp_nucleo_vsa.nome,comp_elenco_spese_vsa.importo,comp_elenco_spese_vsa.descrizione " _
                & " from comp_elenco_spese_vsa,comp_nucleo_vsa,dichiarazioni_vsa where " _
                & " comp_nucleo_vsa.id_dichiarazione=dichiarazioni_vsa.id and comp_nucleo_vsa.id_dichiarazione=" & lIdDichiarazione.Value & "" _
                & " and comp_elenco_spese_vsa.id_componente=comp_nucleo_vsa.id order by comp_nucleo_vsa.progr asc "
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(query)
            If apertanow Then
                connData.chiudi(False)
            End If
            dgvSpese.DataSource = dt
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub dgvpatrmobiliare_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvPatrMobiliare.NeedDataSource
        Try
            Dim apertanow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                apertanow = True
            End If
            Dim query As String = "select comp_nucleo_vsa.id as idcomp,comp_patr_mob_vsa.id as idmob,comp_nucleo_vsa.cognome,comp_nucleo_vsa.nome,comp_patr_mob_vsa.*,tipologia_patr_mob.descrizione as tipo_mob " _
                                  & " from tipologia_patr_mob,comp_patr_mob_vsa,comp_nucleo_vsa where comp_patr_mob_vsa.id_componente=comp_nucleo_vsa.id and comp_nucleo_vsa.id_dichiarazione = " & lIdDichiarazione.Value & "" _
                                  & " and comp_patr_mob_vsa.id_tipo=tipologia_patr_mob.id (+) order by comp_patr_mob_vsa.id_componente asc"
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(query)
            If apertanow Then
                connData.chiudi(False)
            End If
            dgvPatrMobiliare.DataSource = dt
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub dgvpatrimmobiliare_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvPatrImmobiliare.NeedDataSource
        Try
            Dim apertanow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                apertanow = True
            End If
            Dim query As String = "select comp_nucleo_vsa.id as idcomp,comp_patr_immob_vsa.id as idimmob,valore_mercato,comp_nucleo_vsa.cognome,comp_nucleo_vsa.nome," _
                                  & " comp_patr_immob_vsa.*,tipo_piena_patr_immob.descrizione as tipo_propr,t_tipo_patr_immob.descrizione as tipo_immob " _
                                  & " from tipo_piena_patr_immob,comp_patr_immob_vsa,comp_nucleo_vsa,t_tipo_patr_immob where t_tipo_patr_immob.cod=comp_patr_immob_vsa.id_tipo " _
                                  & " and comp_patr_immob_vsa.id_componente=comp_nucleo_vsa.id and comp_nucleo_vsa.id_dichiarazione = " & lIdDichiarazione.Value & "" _
                                  & " and comp_patr_immob_vsa.id_tipo_proprieta=tipo_piena_patr_immob.id (+) order by comp_patr_immob_vsa.id_componente asc"
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(query)
            If apertanow Then
                connData.chiudi(False)
            End If
            dgvPatrImmobiliare.DataSource = dt
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub dgvStampe_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvStampe.NeedDataSource
        Try
            Dim apertanow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                apertanow = True
            End If
            Dim query As String = "select TIPO_STAMPE_PROCESSI.id,tipo_stampe_istanza.id as ordine,tipo_stampe_istanza.descrizione from tipo_stampe_istanza,TIPO_STAMPE_PROCESSI " _
                                  & " where tipo_stampe_istanza.id=TIPO_STAMPE_PROCESSI.id_stampa and ID_TIPO_PROCESSO=" & idMotivoIstanza.Value & "  order by 2 asc"
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(query)
            If apertanow Then
                connData.chiudi(False)
            End If
            dgvStampe.DataSource = dt
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnmodificanucleo_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaNucleo.Click
        Try
            idSelectedNucleo.Value = ""
            dgvNucleo.Rebind()
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btneliminanucleo_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminaNucleo.Click
        Try
            idSelectedNucleo.Value = ""
            dgvNucleo.Rebind()
            btnSalva_click(sender, e)
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnmodificaospite_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaOspite.Click
        Try
            idSelectedOspite.Value = ""
            dgvOspiti.Rebind()
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnelminaospite_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnElminaOspite.Click
        Try
            connData.apri(True)

            par.cmd.CommandText = "delete from comp_nucleo_ospiti_vsa where id= " & idSelectedOspite.Value
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

            idSelectedOspite.Value = ""
            dgvOspiti.Rebind()
            btnSalva_click(sender, e)
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btninserisciospite_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInserisciOspite.Click
        Try
            idSelectedOspite.Value = ""
            dgvOspiti.Rebind()
            btnSalva_click(sender, e)
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btninseriscinucleo_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInserisciNucleo.Click
        Try
            idSelectedNucleo.Value = ""
            dgvNucleo.Rebind()
            btnSalva_click(sender, e)
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btninserisciredditi_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInserisciRedditi.Click
        Try
            idSelectedRedditi.Value = ""
            dgvRedditi.Rebind()
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnmodificaredditi_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaRedditi.Click
        Try
            idSelectedRedditi.Value = ""
            idSelectedCompRedd.Value = ""
            dgvRedditi.Rebind()
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btneliminaredditi_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminaRedditi.Click
        Try
            connData.apri(True)

            par.cmd.CommandText = "delete from domande_redditi_vsa where id=" & idSelectedRedditi.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "delete from comp_reddito_vsa where id_componente=" & idSelectedCompRedd.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "delete from comp_altri_redditi_vsa where id_componente=" & idSelectedCompRedd.Value
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

            idSelectedRedditi.Value = ""
            dgvRedditi.Rebind()

        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnmodificaspese_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaSpese.Click
        Try
            idSelectedSpese.Value = ""
            dgvSpese.Rebind()

        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btninseriscidetrazioni_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInserisciDetrazioni.Click
        Try

            idSelectedDetrazioni.Value = ""
            dgvDetrazioni.Rebind()
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnmodificadetrazioni_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaDetrazioni.Click
        Try

            idSelectedDetrazioni.Value = ""
            dgvDetrazioni.Rebind()
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btneliminadetrazioni_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminaDetrazioni.Click
        Try
            connData.apri(True)

            par.cmd.CommandText = "delete from comp_detrazioni_vsa where id=" & idSelectedDetrazioni.Value
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

            idSelectedDetrazioni.Value = ""
            dgvDetrazioni.Rebind()

        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btninseriscipatrimoniomobiliare_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInserisciPatrimonioMobiliare.Click
        Try
            idSelectedPatrMobiliare.Value = ""
            dgvPatrMobiliare.Rebind()
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnmodificapatrimoniomobiliare_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaPatrimonioMobiliare.Click
        Try
            idSelectedPatrMobiliare.Value = ""
            dgvPatrMobiliare.Rebind()
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btneliminapatrimoniomobiliare_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminaPatrimonioMobiliare.Click
        Try
            connData.apri(True)

            par.cmd.CommandText = "delete from comp_patr_mob_vsa where id=" & idSelectedPatrMobiliare.Value
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

            idSelectedPatrMobiliare.Value = ""
            dgvPatrMobiliare.Rebind()

        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btninseriscipatrimonioimmobiliare_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInserisciPatrimonioImmobiliare.Click
        Try
            idSelectedPatrImmobiliare.Value = ""
            dgvPatrImmobiliare.Rebind()
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub


    Protected Sub btnmodificapatrimonioimmobiliare_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaPatrimonioImmobiliare.Click
        Try
            idSelectedPatrImmobiliare.Value = ""
            dgvPatrImmobiliare.Rebind()
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btneliminapatrimonioimmobiliare_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminaPatrimonioImmobiliare.Click
        Try
            connData.apri(True)

            par.cmd.CommandText = "delete from comp_patr_immob_vsa where id=" & idSelectedPatrImmobiliare.Value
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

            idSelectedPatrImmobiliare.Value = ""
            dgvPatrImmobiliare.Rebind()

        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnSalva_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        Try
            Dim apertanow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(True)
                apertanow = True
            End If

            SalvaDati()

            If apertanow Then
                connData.chiudi(True)
            End If
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub SalvaDati()
        Dim strTipoOsp As String = ""
        Dim numComponenti As Integer = 0
        Dim mqNetti As Integer = 0

        If idMotivoIstanza.Value = "1" Then
            btnInserisciNucleo.Visible = False
            PanelDatiSubentro.Style.Value = "display : block;"
        End If

        Dim docAllegati As Boolean = False
        For i = 0 To chkDocumenti.Items.Count - 1
            If chkDocumenti.Items(i).Selected = True Then
                par.cmd.CommandText = "SELECT * FROM DOC_ALLEGATI_NUOVA_ISTANZA WHERE ID_ISTANZA=" & lIdIstanza.Value & " AND ID_DOCUMENTO=" & chkDocumenti.Items(i).Value & ""
                Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lett.Read = False Then
                    par.cmd.CommandText = "INSERT INTO DOC_ALLEGATI_NUOVA_ISTANZA (ID_ISTANZA,ID_DOCUMENTO) VALUES (" & lIdIstanza.Value & "," & chkDocumenti.Items(i).Value & ")"
                    par.cmd.ExecuteNonQuery()
                    docAllegati = True
                End If
                lett.Close()
            Else
                par.cmd.CommandText = "SELECT * FROM DOC_ALLEGATI_NUOVA_ISTANZA WHERE ID_ISTANZA=" & lIdIstanza.Value & " AND ID_DOCUMENTO=" & chkDocumenti.Items(i).Value & ""
                Dim lett2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lett2.Read Then
                    par.cmd.CommandText = "DELETE FROM DOC_ALLEGATI_NUOVA_ISTANZA WHERE ID_ISTANZA=" & lIdIstanza.Value & " AND ID_DOCUMENTO=" & chkDocumenti.Items(i).Value & ""
                    par.cmd.ExecuteNonQuery()

                    'EVENTO CANCELLAZIONE DOC.ALLEGATI
                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                        & "VALUES (" & lIdIstanza.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "',' " _
                        & "','F206','','')"
                    par.cmd.ExecuteNonQuery()
                End If
                lett2.Close()
            End If
        Next


        For i = 0 To chkRequisiti.Items.Count - 1
            If chkRequisiti.Items(i).Selected = True Then
                par.cmd.CommandText = "SELECT * FROM REQUISITI_NUOVA_ISTANZA WHERE ID_ISTANZA=" & lIdIstanza.Value & " AND ID_REQUISITO=" & chkRequisiti.Items(i).Value & ""
                Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lett.Read = False Then
                    par.cmd.CommandText = "INSERT INTO REQUISITI_NUOVA_ISTANZA (ID_ISTANZA,ID_REQUISITO) VALUES (" & lIdIstanza.Value & "," & chkRequisiti.Items(i).Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
                lett.Close()
            Else
                alertRequisiti = True
                par.cmd.CommandText = "SELECT * FROM REQUISITI_NUOVA_ISTANZA WHERE ID_ISTANZA=" & lIdIstanza.Value & " AND ID_REQUISITO=" & chkRequisiti.Items(i).Value & ""
                Dim lett2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lett2.Read Then
                    If chkRequisiti.Items(i).Value <> "2" Then
                        par.cmd.CommandText = "DELETE FROM REQUISITI_NUOVA_ISTANZA WHERE ID_ISTANZA=" & lIdIstanza.Value & " AND ID_REQUISITO=" & chkRequisiti.Items(i).Value & ""
                        par.cmd.ExecuteNonQuery()
                    End If
                End If
                lett2.Close()
            End If

        Next
        If RadComboStato.SelectedValue < 4 Then
            par.cmd.CommandText = "update domande_bando_vsa set id_stato_istanza=2 where id=" & lIdIstanza.Value
            par.cmd.ExecuteNonQuery()
            RadComboStato.ClearSelection()
            RadComboStato.SelectedValue = "2"
        End If
        If idMotivoIstanza.Value = "7" Or idMotivoIstanza.Value = "13" Then
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.DIMENSIONI WHERE COD_TIPOLOGIA='SUP_NETTA' AND ID_UNITA_IMMOBILIARE=" & idUnita.Value
            mqNetti = par.IfNull(par.cmd.ExecuteScalar, "0")

            par.cmd.CommandText = "select sum(count(id)+(select count(id) from comp_nucleo_ospiti_vsa where id_domanda = " & lIdIstanza.Value & ")) as numComp from comp_nucleo_vsa where id_dichiarazione = " & lIdDichiarazione.Value & " group by id_dichiarazione"
            numComponenti = par.IfNull(par.cmd.ExecuteScalar, "0")

            If forzaRequisito.Value = "0" Then
                If VerificaSovraffollamento(numComponenti, mqNetti) = False Then
                    alertRequisiti = False
                    par.cmd.CommandText = "SELECT * FROM REQUISITI_NUOVA_ISTANZA WHERE ID_ISTANZA=" & lIdIstanza.Value & " AND ID_REQUISITO=2"
                    Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lett.Read = False Then
                        par.cmd.CommandText = "INSERT INTO REQUISITI_NUOVA_ISTANZA (ID_ISTANZA,ID_REQUISITO) VALUES (" & lIdIstanza.Value & ",2)"
                        par.cmd.ExecuteNonQuery()
                    End If
                    lett.Close()
                Else
                    alertRequisiti = True
                    par.cmd.CommandText = "SELECT * FROM REQUISITI_NUOVA_ISTANZA WHERE ID_ISTANZA=" & lIdIstanza.Value & " AND ID_REQUISITO=2"
                    Dim lett2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lett2.Read Then
                        par.cmd.CommandText = "DELETE FROM REQUISITI_NUOVA_ISTANZA WHERE ID_ISTANZA=" & lIdIstanza.Value & " AND ID_REQUISITO=2"
                        par.cmd.ExecuteNonQuery()
                    End If
                    lett2.Close()
                End If
            Else
                alertRequisiti = False
            End If
        End If

        For Each Items As ListItem In chkRequisiti.Items
            par.cmd.CommandText = "select * from REQUISITI_NUOVA_ISTANZA where id_requisito=" & Items.Value & " and id_istanza = " & lIdIstanza.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore.Read Then
                Items.Selected = True
            Else
                Items.Selected = False
            End If
            lettore.Close()

        Next



        If idMotivoIstanza.Value = "2" Then
            'If CType(Dom_Decisioni1.FindControl("ChkDecidi"), CheckBox).Checked = True Then
            par.cmd.CommandText = "SELECT * FROM NUOVI_COMP_NUCLEO_VSA,COMP_NUCLEO_VSA WHERE COMP_NUCLEO_VSA.ID = " _
            & "NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE and ID_DICHIARAZIONE=" & lIdDichiarazione.Value
            Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderN.Read = False Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Nuovo componente non inserito!", 450, 150, "Attenzione", Nothing, Nothing)
                Exit Sub
            End If
            myReaderN.Close()
            'End If
        End If



        If idMotivoIstanza.Value = "7" Then

            If RadComboTipo.SelectedValue <> "-1" Then
                strTipoOsp = ",id_tipo_ospitalita = " & RadComboTipo.SelectedValue

                par.cmd.CommandText = "update domande_bando_vsa set data_evento='" & par.AggiustaData(RadDateEvento.SelectedDate) & "'," _
                    & " data_presentazione = '" & par.AggiustaData(RadDatePresentaz.SelectedDate) & "',id_stato_istanza=" & RadComboStato.SelectedValue & "" _
                    & strTipoOsp & " where id=" & lIdIstanza.Value
                par.cmd.ExecuteNonQuery()

                idTipoProcesso.Value = RadComboTipo.SelectedValue

                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_OSPITI_VSA WHERE " _
                & "COMP_NUCLEO_OSPITI_VSA.ID_DOMANDA=" & lIdIstanza.Value
                Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderN.Read = False Then
                    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Inserire l\'ospite!", 450, 150, "Attenzione", Nothing, Nothing)
                    Exit Sub
                End If
                myReaderN.Close()

            Else
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare la tipologia di ospitalità!", 450, 150, "Attenzione", Nothing, Nothing)
                Exit Sub
            End If

        End If

        Dim dataTar As String = ""
        If Not IsNothing(RadDateTar.SelectedDate) Then
            dataTar = par.AggiustaData(RadDateTar.SelectedDate)
        End If
        Dim dataProvv As String = ""
        If Not IsNothing(RadDateProvv.SelectedDate) Then
            dataProvv = par.AggiustaData(RadDateProvv.SelectedDate)
        End If

        par.cmd.CommandText = "update domande_bando_vsa set data_evento='" & par.AggiustaData(RadDateEvento.SelectedDate) & "'," _
            & " data_presentazione = '" & par.AggiustaData(RadDatePresentaz.SelectedDate) & "'," _
            & " id_causale_domanda =" & par.IfEmpty(RadComboMotiviSub.SelectedValue, "NULL") & "," _
            & " data_provv_cdm = '" & dataProvv & "'," _
            & " data_ricorso_tar = '" & dataTar & "'," _
            & " id_stato_istanza=" & RadComboStato.SelectedValue & "" _
            & strTipoOsp & " where id=" & lIdIstanza.Value
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "select * from INFO_ISEE_ISTANZA where id_istanza=" & lIdIstanza.Value
        Dim myReaderIse As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderIse.HasRows Then
            par.cmd.CommandText = "UPDATE INFO_ISEE_ISTANZA" _
                & " SET " _
                & " ISE  = " & par.IfEmpty(txtISE.Text, 0) & "," _
                & " ISEE = " & par.IfEmpty(txtISEE.Text, 0) & "," _
                & " ISP  = " & par.IfEmpty(txtISP.Text, 0) & "," _
                & " NUM_DSU  = '" & Trim(par.IfEmpty(txtProtocolloDSU.Text, "")) & "'," _
                & " ISR  = " & par.IfEmpty(txtISR.Text, 0) _
                & " WHERE  ID_ISTANZA = " & lIdIstanza.Value
            par.cmd.ExecuteNonQuery()
        Else
            par.cmd.CommandText = "INSERT INTO INFO_ISEE_ISTANZA ( ID_ISTANZA, NUM_DSU, ISE, ISEE, ISP, ISR) " _
             & "  VALUES ( " & lIdIstanza.Value & "," _
             & "'" & Trim(par.IfEmpty(txtProtocolloDSU.Text, "")) & "'," _
             & par.IfEmpty(txtISE.Text, 0) & "," _
             & par.IfEmpty(txtISEE.Text, 0) & "," _
             & par.IfEmpty(txtISP.Text, 0) & "," _
             & par.IfEmpty(txtISR.Text, 0) & ")"
            par.cmd.ExecuteNonQuery()
        End If


        If "1" = "2" Then
            If chkAccogli.Checked = True Then
                If IsNothing(RadDateAccoglimento.SelectedDate) Then
                    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Inserire la data di accoglimento!", 450, 150, "Attenzione", Nothing, Nothing)
                    Exit Sub
                Else
                    par.cmd.CommandText = "Select * from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value & " " _
                        & " And ID_STATO_DECISIONE=3"
                    Dim myReaderSD3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderSD3.Read = False Then
                        par.cmd.CommandText = "INSERT INTO ITER_AUTORIZZATIVO_ISTANZA ( ID," _
                         & " DATA, ID_ISTANZA, ID_STATO_DECISIONE,NOTE)" _
                        & " VALUES ( SEQ_ITER_AUTORIZZ_ISTANZA.NEXTVAL,'" & par.AggiustaData(par.IfEmpty(RadDateAccoglimento.SelectedDate, Format(Now, "yyyyMMdd"))) & "', " _
                        & "" & lIdIstanza.Value & ",3,'" & par.PulisciStrSql(par.IfEmpty(RadTextBoxAccogliAcc.Text, "")) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderSD3.Close()

                    btnSottoponi.Enabled = True
                End If
            End If
        Else
            If chkPreavviso.Checked = True Then
                If IsNothing(RadDatePreavvDiniego.SelectedDate) Then
                    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Inserire la data del preavviso di diniego!", 450, 150, "Attenzione", Nothing, Nothing)
                    Exit Sub
                Else
                    'If RadDatePreavvDiniego.SelectedDate >= Format(Now, "dd/MM/yyyy") Then
                    chkControdeduz.Enabled = False
                    par.cmd.CommandText = "select * from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value & " " _
                       & " and ID_STATO_DECISIONE=1"
                    Dim myReaderSD1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderSD1.Read = False Then
                        par.cmd.CommandText = "INSERT INTO ITER_AUTORIZZATIVO_ISTANZA ( ID," _
                         & " DATA, ID_ISTANZA, ID_STATO_DECISIONE,NOTE)" _
                        & " VALUES ( SEQ_ITER_AUTORIZZ_ISTANZA.NEXTVAL,'" & par.AggiustaData(par.IfEmpty(RadDatePreavvDiniego.SelectedDate, Format(Now, "yyyyMMdd"))) & "', " _
                        & "" & lIdIstanza.Value & ",1,'" & par.PulisciStrSql(par.IfEmpty(RadTextBoxNotaPD.Text, "")) & "')"
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "UPDATE ITER_AUTORIZZATIVO_ISTANZA SET   NOTE  = '" & par.PulisciStrSql(par.IfEmpty(RadTextBoxNotaPD.Text, "")) & "'," _
                            & " DATA  = '" & par.AggiustaData(par.IfEmpty(RadDatePreavvDiniego.SelectedDate, Format(Now, "yyyyMMdd"))) & "'" _
                            & "  WHERE  ID  = " & myReaderSD1("ID")
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderSD1.Close()

                    'chkAccogli.Enabled = False
                    'RadTextBoxAccogliAcc.Enabled = False
                    'RadTextBoxMotivoAcc.Enabled = False
                    'RadDateAccoglimento.Enabled = False
                    'cmbMotivoAcc.Enabled = False
                    'Else
                    '    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Data non corretta!", 450, 150, "Attenzione", Nothing, Nothing)
                    '    Exit Sub

                    'End If
                End If
            End If

            If chkControdeduz.Checked = True Then
                If IsNothing(RadDateControdeduzioni.SelectedDate) Then
                    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Inserire la data delle controdeduzioni!", 450, 150, "Attenzione", Nothing, Nothing)
                    Exit Sub
                Else
                    'If RadDateControdeduzioni.SelectedDate >= Format(Now, "dd/MM/yyyy") Then
                    par.cmd.CommandText = "select * from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value & " " _
                       & " and ID_STATO_DECISIONE=2"
                    Dim myReaderSD3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderSD3.Read = False Then
                        par.cmd.CommandText = "INSERT INTO ITER_AUTORIZZATIVO_ISTANZA ( ID," _
                         & " DATA, ID_ISTANZA, ID_STATO_DECISIONE,NOTE)" _
                        & " VALUES ( SEQ_ITER_AUTORIZZ_ISTANZA.NEXTVAL,'" & par.AggiustaData(par.IfEmpty(RadDateControdeduzioni.SelectedDate, Format(Now, "yyyyMMdd"))) & "', " _
                        & "" & lIdIstanza.Value & ",2,'" & par.PulisciStrSql(par.IfEmpty(RadTextBoxNInt.Text, "")) & "')"
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "UPDATE ITER_AUTORIZZATIVO_ISTANZA SET   NOTE  = '" & par.PulisciStrSql(par.IfEmpty(RadTextBoxNInt.Text, "")) & "'," _
                          & " DATA  = '" & par.AggiustaData(par.IfEmpty(RadDateControdeduzioni.SelectedDate, Format(Now, "yyyyMMdd"))) & "'" _
                          & "  WHERE  ID  = " & myReaderSD3("ID")
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderSD3.Close()
                    'Else
                    '    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Data non corretta!", 450, 150, "Attenzione", Nothing, Nothing)
                    '    Exit Sub

                    'End If
                End If
            End If

            If chkAccogli.Checked = True And txtDataArchiviazione.Text = "" Then
                If chkDiniego.Checked = False Then


                    If IsNothing(RadDateAccoglimento.SelectedDate) Then
                        par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Inserire la data di accoglimento!", 450, 150, "Attenzione", Nothing, Nothing)
                        Exit Sub
                    Else
                        'If RadDateAccoglimento.SelectedDate >= Format(Now, "dd/MM/yyyy") Then
                        par.cmd.CommandText = "select * from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value & " " _
                           & " and ID_STATO_DECISIONE in (3,4)"
                        Dim myReaderSD3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderSD3.Read = False Then
                            par.cmd.CommandText = "INSERT INTO ITER_AUTORIZZATIVO_ISTANZA ( ID," _
                             & " DATA, ID_ISTANZA, ID_STATO_DECISIONE,NOTE)" _
                            & " VALUES ( SEQ_ITER_AUTORIZZ_ISTANZA.NEXTVAL,'" & par.AggiustaData(par.IfEmpty(RadDateAccoglimento.SelectedDate, Format(Now, "yyyyMMdd"))) & "', " _
                            & "" & lIdIstanza.Value & ",3,'" & par.PulisciStrSql(par.IfEmpty(RadTextBoxAccogliAcc.Text, "")) & "')"
                            par.cmd.ExecuteNonQuery()
                        Else
                            par.cmd.CommandText = "UPDATE ITER_AUTORIZZATIVO_ISTANZA SET   NOTE  = '" & par.PulisciStrSql(par.IfEmpty(RadTextBoxAccogliAcc.Text, "")) & "'," _
                            & " DATA  = '" & par.AggiustaData(par.IfEmpty(RadDateAccoglimento.SelectedDate, Format(Now, "yyyyMMdd"))) & "'" _
                            & "  WHERE  ID  = " & myReaderSD3("ID")
                            par.cmd.ExecuteNonQuery()
                        End If
                        myReaderSD3.Close()

                        par.cmd.CommandText = "delete from ITER_AUTORIZZATIVO_ISTANZA where ID_STATO_DECISIONE in (5,10)  and id_istanza=" & lIdIstanza.Value
                        par.cmd.ExecuteNonQuery()

                        btnSottoponi.Enabled = True
                        '    Else
                        '    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Data non corretta!", 450, 150, "Attenzione", Nothing, Nothing)
                        '    Exit Sub

                        'End If
                    End If
                Else
                    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Errore nei flag decisionali!", 450, 150, "Attenzione", Nothing, Nothing)
                    Exit Sub
                End If
            Else
                par.cmd.CommandText = "delete from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value & " " _
                           & " and ID_STATO_DECISIONE=3"
                par.cmd.ExecuteNonQuery()
            End If

            If chkDiniego.Checked = True And chkAccogli.Checked = False Then
                If chkAccogli.Checked = False Then
                    If IsNothing(RadDateDiniego.SelectedDate) Then
                        par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Inserire la data di diniego!", 450, 150, "Attenzione", Nothing, Nothing)
                        Exit Sub
                    Else
                        'If RadDateDiniego.SelectedDate >= Format(Now, "dd/MM/yyyy") Then
                        par.cmd.CommandText = "select * from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value & " " _
                     & " and ID_STATO_DECISIONE=5"
                        Dim myReaderSD5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderSD5.Read = False Then
                            par.cmd.CommandText = "INSERT INTO ITER_AUTORIZZATIVO_ISTANZA ( ID," _
                           & " DATA, ID_ISTANZA, ID_STATO_DECISIONE,NOTE)" _
                           & " VALUES ( SEQ_ITER_AUTORIZZ_ISTANZA.NEXTVAL,'" & par.AggiustaData(par.IfEmpty(RadDateDiniego.SelectedDate, Format(Now, "yyyyMMdd"))) & "', " _
                           & "" & lIdIstanza.Value & ",5,'" & par.PulisciStrSql(par.IfEmpty(RadTextBoxNotaDin.Text, "")) & "')"
                            par.cmd.ExecuteNonQuery()
                        Else
                            par.cmd.CommandText = "UPDATE ITER_AUTORIZZATIVO_ISTANZA SET   NOTE  = '" & par.PulisciStrSql(par.IfEmpty(RadTextBoxNotaDin.Text, "")) & "'," _
                        & " DATA  = '" & par.AggiustaData(par.IfEmpty(RadDateDiniego.SelectedDate, Format(Now, "yyyyMMdd"))) & "' " _
                        & " WHERE  ID  = " & myReaderSD5("ID")
                            par.cmd.ExecuteNonQuery()
                        End If
                        btnSottoponi.Enabled = True


                        'Else
                        '    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Data non corretta!", 450, 150, "Attenzione", Nothing, Nothing)
                        '    Exit Sub
                        'End If
                    End If
                Else
                    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Errore nei flag decisionali!", 450, 150, "Attenzione", Nothing, Nothing)
                    Exit Sub
                End If
            End If
        End If

        Dim gradoParent As Integer = 0
        Dim compCancell As Boolean = False
        If idMotivoIstanza.Value = "1" Then
            If par.IfEmpty(RadComboMotiviSub.SelectedValue, "-1") = "-1" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare la tipologia di subentro!", 450, 150, "Attenzione", Nothing, Nothing)
                Exit Sub
            End If
            par.cmd.CommandText = "select * from comp_nucleo_cancell where id_dichiarazione=" & lIdDichiarazione.Value & " AND " _
                & " COD_FISCALE=(SELECT cod_fiscale from siscom_mi.anagrafica where id in (select id_anagrafica from " _
                & " siscom_mi.soggetti_contrattuali where cod_tipologia_occupante='INTE' and id_contratto=" & lIdContratto.Value & "))"
            Dim myReaderX0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX0.Read Then
                compCancell = True
            End If
            myReaderX0.Close()

            If compCancell = False Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Eliminare l\'attuale intestatario del contratto!", 450, 150, "Attenzione", Nothing, Nothing)
                Exit Sub
            End If

            par.cmd.CommandText = "SELECT GRADO_PARENTELA FROM COMP_NUCLEO_VSA WHERE " _
                        & " ID_DICHIARAZIONE = " & lIdDichiarazione.Value & " And PROGR = 0"
            myReaderX0 = par.cmd.ExecuteReader()
            If myReaderX0.Read Then
                gradoParent = par.IfNull(myReaderX0("GRADO_PARENTELA"), 1)
            End If
            myReaderX0.Close()

            If gradoParent <> 1 Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare la tipologia di parentela corretta per il nuovo intestatario!", 450, 150, "Attenzione", Nothing, Nothing)
                Exit Sub
            End If


        End If

        ScriviMotivi()
        AbilitaTabDecisioni()
        RicavaGiorni()
        par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)

    End Sub

    'GG Preavviso Diniego = Data Preavviso – Data Apertura
    'GG Diniego = Data Diniego – Data Controdeduzione (se presente, altrimenti) Data Preavviso Diniego
    'GG Accoglimento = Data Accoglimento – Data Preavviso Diniego (se presente, altrimenti) Data Apertura
    'GG Chiusura = Data Autorizzazione – Data Apertura
    'GG Archiviazione = Data Archiviazione – Data Autorizzazione

    Private Sub RicavaGiorni()
        txtGiorniPD.Text = "0"
        txtGiorniD.Text = "0"
        txtGiorniAcc.Text = "0"
        txtGiorniCh.Text = "0"
        txtGiorniArch.Text = "0"

        Dim dataPreavviso As String = ""
        If Not IsNothing(RadDatePreavvDiniego.SelectedDate) Then
            dataPreavviso = par.IfEmpty(RadDatePreavvDiniego.SelectedDate, Format(Now, "dd/MM/yyyy"))
        End If
        Dim dataContrDed As String = ""
        If Not IsNothing(RadDateControdeduzioni.SelectedDate) Then
            dataContrDed = par.IfEmpty(RadDateControdeduzioni.SelectedDate, Format(Now, "dd/MM/yyyy"))
        End If
        Dim dataDiniego As String = ""
        If Not IsNothing(RadDateDiniego.SelectedDate) Then
            dataDiniego = par.IfEmpty(RadDateDiniego.SelectedDate, Format(Now, "dd/MM/yyyy"))
        End If
        Dim dataArch As String = par.IfEmpty(txtDataArchiviazione.Text, "")

        Dim dataAcc As String = ""
        If Not IsNothing(RadDateAccoglimento.SelectedDate) Then
            dataAcc = par.IfEmpty(RadDateAccoglimento.SelectedDate, Format(Now, "dd/MM/yyyy"))
        End If
        If dataAutorizz.Value <> "" Then
            dataAutorizz.Value = par.FormattaData(dataAutorizz.Value)
        End If
        If dataPreavviso <> "" Then
            txtGiorniPD.Text = DateDiff("d", CDate(RadDatePresentaz.SelectedDate).ToShortDateString, CDate(dataPreavviso))
        End If
        If dataContrDed <> "" Then
            If dataDiniego <> "" Then
                txtGiorniD.Text = DateDiff("d", CDate(dataContrDed), CDate(dataDiniego))
            End If
        Else
            If dataDiniego <> "" Then
                txtGiorniD.Text = DateDiff("d", CDate(dataPreavviso), CDate(dataDiniego))
            End If
        End If
        If dataPreavviso <> "" Then
            If dataAcc <> "" Then
                txtGiorniAcc.Text = DateDiff("d", CDate(dataPreavviso), CDate(dataAcc))
            End If
        Else
            If dataAcc <> "" Then
                txtGiorniAcc.Text = DateDiff("d", CDate(RadDatePresentaz.SelectedDate).ToShortDateString, CDate(dataAcc))
            End If
        End If
        If dataAutorizz.Value <> "" Then
            txtGiorniCh.Text = DateDiff("d", CDate(RadDatePresentaz.SelectedDate).ToShortDateString, CDate(dataAutorizz.Value))
        End If
        If dataArch <> "" Then
            If dataAutorizz.Value <> "" Then
                txtGiorniArch.Text = DateDiff("d", CDate(dataAutorizz.Value), CDate(dataArch))
            End If
            btnArchivia.Enabled = False
        End If
    End Sub

    Private Function AutorizzaSubentro(ByRef CF As String) As Boolean

        Dim IdAnagRichiedente As String = ""
        Dim IdExintestatario As String = ""
        Dim CognomeNome As String = ""
        Dim TipoIndir As String = ""
        Dim Indirizzo As String = ""
        Dim Civico As String = ""
        Dim LuogoRec As String = ""
        Dim Sigla As String = ""
        Dim Cap As String = ""
        Dim IdUI As Long = 0
        Dim dataAccoglim As String = ""
        Dim gradoParent As Integer = 0
        Dim codParent As String = ""

        par.cmd.CommandText = "SELECT GRADO_PARENTELA FROM COMP_NUCLEO_VSA WHERE " _
            & " ID_DICHIARAZIONE = " & lIdDichiarazione.Value & " And PROGR = 0"
        Dim myReaderX0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderX0.Read Then
            gradoParent = par.IfNull(myReaderX0("GRADO_PARENTELA"), 1)
        End If
        myReaderX0.Close()

        'If gradoParent <> 1 Then
        '    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare la tipologia di parentela corretta per il nuovo intestatario!", 450, 150, "Attenzione", Nothing, Nothing)
        '    Return True
        '    Exit Function
        'End If

        par.cmd.CommandText = "select * from t_tipo_parentela where cod=" & gradoParent
        myReaderX0 = par.cmd.ExecuteReader()
        If myReaderX0.Read Then
            codParent = par.IfNull(myReaderX0("cod_siscom_mi"), "")
        End If
        myReaderX0.Close()

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE COD_FISCALE = (SELECT COD_FISCALE FROM COMP_NUCLEO_VSA WHERE " _
            & " ID_DICHIARAZIONE = " & lIdDichiarazione.Value & " And PROGR = 0) ORDER BY ID DESC"
        Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderX1.Read Then
            IdAnagRichiedente = myReaderX1("id")
            CF = par.IfNull(myReaderX1("COD_FISCALE"), "")
            CognomeNome = Trim(par.IfNull(myReaderX1("COGNOME"), "")) & " " & Trim(par.IfNull(myReaderX1("NOME"), ""))
        End If
        myReaderX1.Close()

        Dim controlloID As Integer = 0
        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO=" & lIdContratto.Value
        Dim daIdOK As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dtIdOK As New Data.DataTable
        daIdOK = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

        daIdOK.Fill(dtIdOK)
        daIdOK.Dispose()
        For Each row As Data.DataRow In dtIdOK.Rows
            If row.Item("ID_ANAGRAFICA") = par.IfEmpty(IdAnagRichiedente, "0") Then
                controlloID = 1
            End If
        Next

        If controlloID = 0 Then
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=ANAGRAFICA.ID And " _
                & " ID_CONTRATTO=" & lIdContratto.Value & " And COD_FISCALE='" & CF & "'"
            myReaderX1 = par.cmd.ExecuteReader
            If myReaderX1.Read Then
                IdAnagRichiedente = myReaderX1("ID_ANAGRAFICA")
            End If
            myReaderX1.Close()
        End If

        par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID = " & lIdDichiarazione.Value
        myReaderX1 = par.cmd.ExecuteReader
        If myReaderX1.Read Then
            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID='" & par.IfNull(myReaderX1("ID_LUOGO_RES_DNTE"), "") & "'"
            Dim myReaderIDluogoRES As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderIDluogoRES.Read Then
                Sigla = par.IfNull(myReaderIDluogoRES("SIGLA"), "")
                LuogoRec = par.PulisciStrSql(par.IfNull(myReaderIDluogoRES("NOME"), ""))
            End If
            myReaderIDluogoRES.Close()

            par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD='" & par.IfNull(myReaderX1("ID_TIPO_IND_RES_DNTE"), "") & "'"
            Dim myReaderTipoIndir As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderTipoIndir.Read Then
                TipoIndir = par.IfNull(myReaderTipoIndir("DESCRIZIONE"), "")
            End If
            myReaderTipoIndir.Close()

            Indirizzo = par.PulisciStrSql(par.IfNull(myReaderX1("IND_RES_DNTE"), ""))
            Civico = par.PulisciStrSql(par.IfNull(myReaderX1("CIVICO_RES_DNTE"), ""))
            Cap = par.IfNull(myReaderX1("CAP_RES_DNTE"), "")
        End If
        myReaderX1.Close()

        If IdAnagRichiedente = "" Then
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Probabile errore nel codice fiscale dei componenti nel nucleo. Operazione interrotta!", 450, 150, "Attenzione", Nothing, Nothing)
            Return True
            Exit Function
        End If

        par.cmd.CommandText = "SELECT * FROM siscom_mi.soggetti_contrattuali WHERE id_contratto = " & lIdContratto.Value & " and  cod_tipologia_occupante = 'INTE'"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        IdExintestatario = par.IfNull(dt.Rows(0).Item("id_anagrafica"), "")

        '################## INSERT INTO INTESTATARI CONTRATTI
        par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTESTATARI_RAPPORTO ( ID_CONTRATTO, ID_ANAGRAFICA, DATA_INIZIO,DATA_FINE ) " _
            & "VALUES ( " & lIdContratto.Value & ", " & IdAnagRichiedente & ", '" _
            & par.AggiustaData(Date.Parse(RadDateAccoglimento.SelectedDate, New System.Globalization.CultureInfo("it-IT", False)).AddDays(1).ToString("dd/MM/yyyy")) & "'," _
            & "'29991231')"
        par.cmd.ExecuteNonQuery()


        '################# UPDATE INTO INTESTATARI CONTRATTI, DATA FINE A EX INTESTATARIO
        par.cmd.CommandText = "UPDATE SISCOM_MI.INTESTATARI_RAPPORTO SET DATA_FINE = '" & par.AggiustaData(par.IfEmpty(RadDateAccoglimento.SelectedDate, Format(Now, "yyyyMMdd"))) & "' WHERE ID_ANAGRAFICA = " & IdExintestatario & " " _
            & " AND ID_CONTRATTO = " & lIdContratto.Value
        par.cmd.ExecuteNonQuery()

        '################## UPDATE DI SOGGETTI CONTRATTUALI PER VECCHIO INTESTATARIO
        par.cmd.CommandText = "UPDATE SISCOM_MI.SOGGETTI_CONTRATTUALI SET COD_TIPOLOGIA_OCCUPANTE = 'EXINTE',DATA_FINE = '" & par.AggiustaData(par.IfEmpty(RadDateAccoglimento.SelectedDate, Format(Now, "yyyyMMdd"))) & "' " &
               "WHERE ID_ANAGRAFICA = " & IdExintestatario & " AND ID_CONTRATTO = " & lIdContratto.Value
        par.cmd.ExecuteNonQuery()


        '################## UPDATE DI SOGGETTI CONTRATTUALI PER NUOVO INTESTATARIO
        Dim sSql As String = ""
        sSql = "UPDATE SISCOM_MI.SOGGETTI_CONTRATTUALI SET COD_TIPOLOGIA_OCCUPANTE = 'INTE',DATA_INIZIO = '" & par.AggiustaData(Date.Parse(RadDateAccoglimento.SelectedDate, New System.Globalization.CultureInfo("it-IT", False)).AddDays(1).ToString("dd/MM/yyyy")) & "'," &
            "DATA_FINE='29991231',COD_TIPOLOGIA_PARENTELA='" & codParent & "' WHERE ID_ANAGRAFICA = " & IdAnagRichiedente & " and NVL(data_fine,'29991231') >= '" & Format(Now(), "yyyyMMdd") & "' AND ID_CONTRATTO = " & lIdContratto.Value
        par.cmd.CommandText = sSql
        If par.cmd.ExecuteNonQuery() > 1 Then
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Errore! Componente duplicato nel nucleo del contratto. Impossibile procedere.", 450, 150, "Attenzione", Nothing, Nothing)
            Return True

            Exit Function
        End If


        '********************* AGGIORNO ANCHE IL PRESSO_COR,ecc. DEL NUOVO INTESTATARIO SUBENTRATO *********************
        'Dim par1 As New CM.Global
        'If Not IsNothing(Session.Item("lIdConnessione")) Then
        '    par1.OracleConn = CType(HttpContext.Current.Session.Item(Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleConnection)
        '    par1.cmd = par1.OracleConn.CreateCommand()
        '    par1.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleTransaction)

        '    par1.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET PRESSO_COR='" & par.PulisciStrSql(CognomeNome) & "',TIPO_COR='" & TipoIndir & "'," _
        '    & "LUOGO_COR='" & LuogoRec & "',VIA_COR='" & Indirizzo & "',CIVICO_COR='" & Civico & "',CAP_COR='" & Cap & "' WHERE ID=" & lIdContratto.Value
        '    par1.cmd.ExecuteNonQuery()

        '    'pressoCOR.Value = par.PulisciStrSql(CognomeNome)

        '    par1.myTrans.Commit()
        '    par1.myTrans = par1.OracleConn.BeginTransaction()
        '    HttpContext.Current.Session.Add("TRANSAZIONE" & Session.Item("lIdConnessione"), par1.myTrans)
        '    par1.Dispose()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        'Else
        '    par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET PRESSO_COR='" & par.PulisciStrSql(CognomeNome) & "',TIPO_COR='" & TipoIndir & "'," _
        '    & "LUOGO_COR='" & LuogoRec & "',VIA_COR='" & Indirizzo & "',CIVICO_COR='" & Civico & "',CAP_COR='" & Cap & "' WHERE ID=" & lIdContratto.Value
        '    par.cmd.ExecuteNonQuery()
        'End If

        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                        & "VALUES (" & lIdIstanza.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" _
                        & "','F196','','')"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "SELECT id_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_UNITA_PRINCIPALE IS NULL AND ID_CONTRATTO=" & lIdContratto.Value
        Dim myReaderUI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderUI.Read Then
            IdUI = myReaderUI("id_UNITA")
        End If
        myReaderUI.Close()

        par.cmd.CommandText = "SELECT cond_edifici.* FROM SISCOM_MI.COND_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & IdUI & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND COND_EDIFICI.ID_EDIFICIO=EDIFICI.ID"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Do While myReader.Read
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO,ID_CONTRATTO) VALUES (5," & IdUI & ",'" & Format(Now, "yyyyMMdd") & "',0," & myReader("ID_CONDOMINIO") & "," & lIdContratto.Value & ")"
            par.cmd.ExecuteNonQuery()
        Loop
        myReader.Close()

        If RadComboMotiviSub.SelectedValue = 8 Then
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_CESSIONI (ID_CONTRATTO,DATA_CESSIONE,IMPORTO,ID_EX_INT,ID_INT) VALUES (" & lIdContratto.Value & ",'" & Format(Now, "yyyyMMdd") & "',0," & IdExintestatario & "," & IdAnagRichiedente & ")"
            par.cmd.ExecuteNonQuery()
        Else
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_CESSIONI (ID_CONTRATTO,DATA_CESSIONE,IMPORTO,ID_EX_INT,ID_INT) VALUES (" & lIdContratto.Value & ",'" & Format(Now, "yyyyMMdd") & "',67," & IdExintestatario & "," & IdAnagRichiedente & ")"
            par.cmd.ExecuteNonQuery()
        End If

        Return False

    End Function

    Private Function AutorizzaAmpliamento(ByRef CF As String) As Boolean


        Dim SstringaSql As String = ""
        Dim dataIngressoNucleo As String = ""

        '######### AGGIORNAMENTO DI SOGGETTI CONTRATTUALI PER NUOVI COMPONENTI DEL NUCLEO
        par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.ID,id_dichiarazione,cod_fiscale,cognome,nome,sesso,data_nascita,perc_inval,indennita_acc,grado_parentela,data_ingresso_nucleo," _
                            & "DICHIARAZIONI_VSA.id_luogo_res_dnte,DICHIARAZIONI_VSA.id_tipo_ind_res_dnte,DICHIARAZIONI_VSA.ind_res_dnte,DICHIARAZIONI_VSA.civico_res_dnte,DICHIARAZIONI_VSA.cap_res_dnte " _
                            & "FROM NUOVI_COMP_NUCLEO_VSA,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA " _
                            & "WHERE id_componente = COMP_NUCLEO_VSA.ID " _
                            & "AND DICHIARAZIONI_VSA.ID=COMP_NUCLEO_VSA.id_dichiarazione " _
                            & "AND id_dichiarazione = " & lIdDichiarazione.Value

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        If dt.Rows.Count > 0 Then

            For Each row As Data.DataRow In dt.Rows
                dataIngressoNucleo = par.IfNull(row.Item("data_ingresso_nucleo"), "")
                CF = par.IfNull(row.Item("COD_FISCALE"), "")
                par.cmd.CommandText = "select id from siscom_mi.anagrafica where cod_fiscale = '" & par.IfNull(row.Item("COD_FISCALE"), "") & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim idAnagrafico As String = ""
                Dim codParentela As String = ""
                If myReader.Read Then
                    idAnagrafico = par.IfNull(myReader("ID"), "")
                End If
                myReader.Close()

                If idAnagrafico = "" Then
                    idAnagrafico = InserInAnagrafica(row)
                End If

                par.cmd.CommandText = "select * from t_tipo_parentela where cod=" & par.IfNull(row.Item("grado_parentela"), "1")
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    codParentela = par.IfNull(myReader("cod_siscom_mi"), "")
                End If
                myReader.Close()


                If idAnagrafico = "" Then
                    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Errore nell\'inserimento!", 450, 150, "Attenzione", Nothing, Nothing)
                    Return True

                    Exit Function
                End If

                If lIdContratto.Value > 0 Then
                    par.cmd.CommandText = "insert into siscom_mi.soggetti_contrattuali " _
                                        & "(id_anagrafica,id_contratto,cod_tipologia_parentela,cod_tipologia_occupante,cod_tipologia_titolo,data_inizio,data_fine) values" _
                                        & "(" & idAnagrafico & "," & lIdContratto.Value & ",'" & codParentela & "','ALTR','LEGIT','" & dataIngressoNucleo & "','29991231')"
                    par.cmd.ExecuteNonQuery()
                End If

            Next
        Else
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Nuovo componente non inserito!", 450, 150, "Attenzione", Nothing, Nothing)
            Return True

            Exit Function
        End If
        '######### FINE AGGIORNAMENTO DI SOGGETTI CONTRATTUALI PER NUOVI COMPONENTI DEL NUCLEO


        '*************evento AMPLIAMENTO
        SstringaSql = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                        & "VALUES (" & lIdIstanza.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" _
                        & "','F192','','')"
        par.cmd.CommandText = SstringaSql
        par.cmd.ExecuteNonQuery()

        Return False
    End Function

    Private Function AutorizzaOspitalita(ByRef CF As String) As Boolean

        Dim idOspiti As String = ""


        par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_OSPITI_VSA WHERE ID_DOMANDA=" & lIdIstanza.Value & ""
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)

        For Each row As Data.DataRow In dt.Rows
            CF = par.IfNull(row.Item("COD_FISCALE"), "")
            par.cmd.CommandText = "select siscom_mi.SEQ_OSPITI.nextval from dual"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                idOspiti = lettore(0)
            End If
            lettore.Close()

            par.cmd.CommandText = "select * from siscom_mi.ospiti where cod_fiscale='" & Trim(row.Item("COD_FISCALE")) & "'"
            Dim myReaderOsp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderOsp.Read = False Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.OSPITI (ID,ID_CONTRATTO,DATA_AGG,DATA_INIZIO_OSPITE,DATA_FINE_OSPITE,NOMINATIVO,COD_FISCALE,ID_TIPO_RUOLO) " _
                    & "VALUES (" & idOspiti & "," & lIdContratto.Value & ",'" & Format(Now, "yyyyMMdd") & "','" & row.Item("DATA_INIZIO_OSPITE") & "','" & row.Item("DATA_FINE_OSPITE") & "','" _
                    & "" & par.PulisciStrSql(row.Item("COGNOME")) & " " & par.PulisciStrSql(row.Item("NOME")) & "','" & row.Item("COD_FISCALE") & "'," & par.IfNull(row.Item("ID_TIPO_RUOLO"), "1") & ")"
                par.cmd.ExecuteNonQuery()
            End If
            myReaderOsp.Close()
        Next

        If dt.Rows.Count > 0 Then
            par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                & "VALUES (" & lIdIstanza.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" _
                & "','F197','','')"
            par.cmd.ExecuteNonQuery()
        Else
            If idMotivoIstanza.Value = "7" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Ospite non inserito!", 450, 150, "Attenzione", Nothing, Nothing)
            Else
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Coabitante non inserito!", 450, 150, "Attenzione", Nothing, Nothing)
            End If

            Return True

            Exit Function
        End If

        Return False
    End Function

    Private Function InserInAnagrafica(ByVal r As Data.DataRow) As Integer
        Dim IdAna As String = ""
        Dim cittadinanza As String = ""
        Dim residenza As String = ""
        Dim comuresid As String = ""
        Dim provresid As String = ""
        Dim indirizzresid As String = ""
        Dim idrecapito As String = ""
        Dim idindrecapito As String = ""
        Dim codComuRecap As String = ""


        par.cmd.CommandText = "select * from comuni_nazioni where cod='" & par.IfNull(r.Item("COD_FISCALE"), "F205").ToString.Substring(11, 4) & "'"
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            If par.IfNull(lettore("cod"), "").ToString.Contains("Z") Then
                cittadinanza = par.PulisciStrSql(par.IfNull(lettore("nome"), ""))
            Else
                cittadinanza = "ITALIA"
            End If
        End If
        lettore.Close()
        '*************** fine CITTADINANZA **********


        '*************** campo RESIDENZA **************
        par.cmd.CommandText = "select * from comuni_nazioni where id=" & par.IfNull(r.Item("id_luogo_res_dnte"), 1)
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            comuresid = par.PulisciStrSql(par.IfNull(lettore("nome"), ""))
            provresid = par.IfNull(lettore("sigla"), "")
            residenza = comuresid & " (" & provresid & ") CAP " & par.IfNull(r.Item("cap_res_dnte"), "") & " "
        End If
        lettore.Close()
        par.cmd.CommandText = "select * from t_tipo_indirizzo where cod='" & par.IfNull(r.Item("id_tipo_ind_res_dnte"), 1) & "'"
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            indirizzresid = par.PulisciStrSql(par.IfNull(lettore("descrizione"), "")) & " " & par.PulisciStrSql(par.IfNull(r.Item("ind_res_dnte"), ""))
            residenza &= indirizzresid & ", " & par.IfNull(r.Item("civico_res_dnte"), "")
        End If
        lettore.Close()
        '*************** fine RESIDENZA **************


        '********* campo ID_INDIRIZZO_RECAPITO **********
        par.cmd.CommandText = "select siscom_mi.SEQ_INDIRIZZI.nextval from dual"
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            idindrecapito = lettore(0)
        End If
        lettore.Close()

        par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza where id=" & lIdContratto.Value
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            par.cmd.CommandText = "select cod from comuni_nazioni where nome = '" & par.PulisciStrSql(par.IfNull(lettore("luogo_cor"), "")) & "'"
            Dim lettoreCod As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreCod.Read Then
                codComuRecap = par.IfNull(lettoreCod("cod"), "")
            End If
            lettoreCod.Close()

            par.cmd.CommandText = "insert into siscom_mi.indirizzi_anagrafica (id,descrizione,civico,cap,localita," _
                & "cod_comune) values (" & idindrecapito & ",'" & par.PulisciStrSql(par.IfNull(lettore("tipo_cor"), "")) & " " _
                & par.PulisciStrSql(par.IfNull(lettore("via_cor"), "")) & "','" & par.IfNull(lettore("civico_cor"), "") & "','" & par.IfNull(lettore("cap_cor"), "") & "'," _
                & "'" & par.PulisciStrSql(par.IfNull(lettore("luogo_cor"), "")) & "','" & codComuRecap & "') "
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "select id from siscom_mi.indirizzi_anagrafica where descrizione='" & par.PulisciStrSql(par.IfNull(lettore("tipo_cor"), "")) & " " _
                & par.PulisciStrSql(par.IfNull(lettore("via_cor"), "")) & "' and civico ='" & par.IfNull(lettore("civico_cor"), "") & "'"
            Dim lettoreInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreInd.Read Then
                idrecapito = par.IfNull(lettoreInd("id"), " ")
            End If
            lettoreInd.Close()
        End If
        lettore.Close()
        '************** fine ID_INDIRIZZO_RECAPITO *************



        '************* INSERIMENTO IN ANAGRAFICA **************
        par.cmd.CommandText = "select siscom_mi.SEQ_ANAGRAFICA.nextval from dual"
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            IdAna = lettore(0)
        End If
        lettore.Close()
        par.cmd.CommandText = "insert into siscom_mi.anagrafica (id,cognome,nome,data_nascita,cod_fiscale,sesso,cod_comune_nascita,cittadinanza,residenza,id_indirizzo_recapito," _
                            & "comune_residenza,provincia_residenza,indirizzo_residenza,civico_residenza,cap_residenza,tipo_r) values " _
                            & "(" & IdAna & ",'" & par.PulisciStrSql(LTrim(RTrim(par.IfNull(r.Item("COGNOME"), "")))) & "', " _
                            & "'" & par.PulisciStrSql(LTrim(RTrim(par.IfNull(r.Item("NOME"), "")))) & "', " _
                            & "'" & par.IfNull(r.Item("DATA_NASCITA"), "") & "', " _
                            & "'" & par.PulisciStrSql(par.IfNull(r.Item("cod_fiscale"), "")) & "', " _
                            & "'" & par.PulisciStrSql(par.IfNull(r.Item("sesso"), "")) & "','" & par.IfNull(r.Item("COD_FISCALE"), "F205").ToString.Substring(11, 4) & "'," _
                            & "'" & cittadinanza & "','" & residenza & "','" & idrecapito & "','" & comuresid & "','" & provresid & "','" & indirizzresid & "'," _
                            & "'" & par.IfNull(r.Item("civico_res_dnte"), "") & "','" & par.IfNull(r.Item("cap_res_dnte"), "") & "',0)"
        par.cmd.ExecuteNonQuery()
        '************* fine INSERIMENTO IN ANAGRAFICA **************


        Return IdAna

    End Function

    Private Sub DisabilitaDecisioni(ByVal statoDecisione As String)
        Dim CTRL As Control = Nothing
        Dim mpContentPlaceHolder As ContentPlaceHolder
        mpContentPlaceHolder = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
        Dim pagina As Object = mpContentPlaceHolder.FindControl("RadPageDecisione")
        Dim panel1 As Panel = CType(pagina.FindControl("PanelRadPageDecisione"), Panel)
        For Each CTRL In panel1.Controls
            If TypeOf CTRL Is RadComboBox Then
                DirectCast(CTRL, RadComboBox).Enabled = False
            End If
            If TypeOf CTRL Is RadTextBox Then
                DirectCast(CTRL, RadTextBox).Enabled = False
            End If
            If TypeOf CTRL Is RadDatePicker Then
                DirectCast(CTRL, RadDatePicker).Enabled = False
            End If
            If TypeOf CTRL Is Button Then
                DirectCast(CTRL, Button).Enabled = False
            End If
            If TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Enabled = False
            End If
        Next

        If statoDecisione = "" Then
            Select Case idMotivoIstanza.Value
                Case "000"
                    chkAccogli.Enabled = True
                    RadTextBoxAccogliAcc.Enabled = True
                    RadTextBoxMotivoAcc.Enabled = True
                    RadDateAccoglimento.Enabled = True
                    cmbMotivoAcc.Enabled = True
                Case Else
                    chkPreavviso.Enabled = True
                    RadTextBoxNotaPD.Enabled = True
                    RadTextBoxMotivoPD.Enabled = True
                    RadDatePreavvDiniego.Enabled = True
                    cmbMotivoPD.Enabled = True

                    chkAccogli.Enabled = True
                    RadTextBoxAccogliAcc.Enabled = True
                    RadTextBoxMotivoAcc.Enabled = True
                    RadDateAccoglimento.Enabled = True
                    cmbMotivoAcc.Enabled = True
            End Select
        End If

        If statoDecisione = "3" Then
            Select Case idMotivoIstanza.Value
                Case "000"
                    chkAccogli.Enabled = True
                    RadTextBoxAccogliAcc.Enabled = True
                    RadTextBoxMotivoAcc.Enabled = True
                    RadDateAccoglimento.Enabled = True
                    cmbMotivoAcc.Enabled = True
                    btnSottoponi.Enabled = True
                Case "7", "13"
                    chkPreavviso.Enabled = True
                    RadTextBoxNotaPD.Enabled = True
                    RadTextBoxMotivoPD.Enabled = True
                    RadDatePreavvDiniego.Enabled = True
                    cmbMotivoPD.Enabled = True

                    chkAccogli.Enabled = True
                    RadTextBoxAccogliAcc.Enabled = True
                    RadTextBoxMotivoAcc.Enabled = True
                    RadDateAccoglimento.Enabled = True
                    cmbMotivoAcc.Enabled = True
                    btnSottoponi.Enabled = True

            End Select
        End If
        'If statoDecisione = "4" Then
        '    DisabilitaForm()
        'End If

    End Sub

    Private Sub DisabilitaForm()
        Dim CTRL As Control = Nothing
        Dim mpContentPlaceHolder As ContentPlaceHolder
        mpContentPlaceHolder = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
        For Each CTRL In mpContentPlaceHolder.Controls
            If TypeOf CTRL Is RadTextBox Then
                DirectCast(CTRL, RadTextBox).Enabled = False
            End If
            If TypeOf CTRL Is RadAutoCompleteBox Then
                DirectCast(CTRL, RadAutoCompleteBox).Enabled = False
            End If
            If TypeOf CTRL Is RadComboBox Then
                DirectCast(CTRL, RadComboBox).Enabled = False
            End If
            If TypeOf CTRL Is RadDatePicker Then
                DirectCast(CTRL, RadDatePicker).Enabled = False
            End If
            If TypeOf CTRL Is RadButton Then
                DirectCast(CTRL, RadButton).Enabled = False
            End If
            btnCalcoloCodFiscale.Visible = False
            btnEliminaDetrazioni.Visible = False
            btnEliminaNucleo.Visible = False
            btnEliminaPatrimonioImmobiliare.Visible = False
            btnEliminaPatrimonioMobiliare.Visible = False
            btnEliminaRedditi.Visible = False
            btnElminaOspite.Visible = False
            btnInserisciDetrazioni.Visible = False
            btnInserisciNucleo.Visible = False
            btnInserisciOspite.Visible = False
            btnInserisciPatrimonioImmobiliare.Visible = False
            btnInserisciPatrimonioMobiliare.Visible = False
            btnInserisciRedditi.Visible = False
            btnModificaDetrazioni.Visible = False
            btnModificaNucleo.Visible = False
            btnModificaOspite.Visible = False
            btnModificaPatrimonioImmobiliare.Visible = False
            btnModificaPatrimonioMobiliare.Visible = False
            btnModificaRedditi.Visible = False
            btnModificaSpese.Visible = False
        Next


        Dim CTRL2 As Control = Nothing
        Dim mpContentPlaceHolder2 As ContentPlaceHolder
        mpContentPlaceHolder2 = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
        Dim pagina2 As Object = mpContentPlaceHolder2.FindControl("RadPageDecisione")
        Dim panel2 As Panel = CType(pagina2.FindControl("PanelRadPageDecisione"), Panel)
        For Each CTRL2 In panel2.Controls
            If TypeOf CTRL2 Is RadTextBox Then
                DirectCast(CTRL2, RadTextBox).Enabled = False
            End If
            If TypeOf CTRL2 Is RadComboBox Then
                DirectCast(CTRL2, RadComboBox).Enabled = False
            End If
            If TypeOf CTRL2 Is RadDatePicker Then
                DirectCast(CTRL2, RadDatePicker).Enabled = False
            End If
            If TypeOf CTRL2 Is Button Then
                DirectCast(CTRL2, Button).Enabled = False
            End If
            If TypeOf CTRL2 Is CheckBox Then
                DirectCast(CTRL2, CheckBox).Enabled = False
            End If
        Next


        Dim CTRL3 As Control = Nothing
        Dim mpContentPlaceHolder3 As ContentPlaceHolder
        mpContentPlaceHolder3 = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
        Dim pagina3 As Object = mpContentPlaceHolder3.FindControl("RadPageRequisiti")
        Dim panel3 As Panel = CType(pagina3.FindControl("PanelRadPageRequisiti"), Panel)
        For Each CTRL3 In panel3.Controls
            If TypeOf CTRL3 Is CheckBoxList Then
                DirectCast(CTRL3, CheckBoxList).Enabled = False
            End If
        Next

        Dim CTRL4 As Control = Nothing
        Dim mpContentPlaceHolder4 As ContentPlaceHolder
        mpContentPlaceHolder4 = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
        Dim pagina4 As Object = mpContentPlaceHolder4.FindControl("RadPageISEE")
        Dim panel4 As Panel = CType(pagina4.FindControl("PanelRadPageISEE"), Panel)
        For Each CTRL4 In panel4.Controls
            If TypeOf CTRL4 Is RadTextBox Then
                DirectCast(CTRL4, RadTextBox).Enabled = False
            End If
            If TypeOf CTRL4 Is RadNumericTextBox Then
                DirectCast(CTRL4, RadNumericTextBox).Enabled = False
            End If
        Next

        'RadDateControdeduzioni.Enabled = False
        'RadTextBoxMotDin.Enabled = False
        'RadDateDiniego.Enabled = False
        'RadTextBoxMotDin.Enabled = False
        'RadTextBoxMotivoAcc.Enabled = False
        'RadTextBoxMotivoC.Enabled = False
        'RadTextBoxMotivoC.Enabled = False
        'RadTextBoxMotivoPD.Enabled = False
        'RadDatePreavvDiniego.Enabled = False
        RadComboTipo.Enabled = False
        RadComboMotiviSub.Enabled = False
        If Session.Item("OP_RESP_VSA") = 1 Then
            btnApprova.Enabled = True
            btnRespingi.Enabled = True
        End If
        btnSottoponi.Enabled = False

    End Sub

    Private Sub AbilitaForm()
        Dim CTRL As Control = Nothing
        Dim mpContentPlaceHolder As ContentPlaceHolder
        mpContentPlaceHolder = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
        For Each CTRL In mpContentPlaceHolder.Controls
            If TypeOf CTRL Is RadTextBox Then
                DirectCast(CTRL, RadTextBox).Enabled = True
            End If
            If TypeOf CTRL Is RadAutoCompleteBox Then
                DirectCast(CTRL, RadAutoCompleteBox).Enabled = True
            End If
            If TypeOf CTRL Is RadComboBox Then
                DirectCast(CTRL, RadComboBox).Enabled = True
            End If
            If TypeOf CTRL Is RadDatePicker Then
                DirectCast(CTRL, RadDatePicker).Enabled = True
            End If
            If TypeOf CTRL Is RadButton Then
                DirectCast(CTRL, RadButton).Enabled = True
            End If
            btnCalcoloCodFiscale.Visible = True
            btnEliminaDetrazioni.Visible = True
            btnEliminaNucleo.Visible = True
            btnEliminaPatrimonioImmobiliare.Visible = True
            btnEliminaPatrimonioMobiliare.Visible = True
            btnEliminaRedditi.Visible = True
            btnElminaOspite.Visible = True
            btnInserisciDetrazioni.Visible = True
            btnInserisciNucleo.Visible = True
            btnInserisciOspite.Visible = True
            btnInserisciPatrimonioImmobiliare.Visible = True
            btnInserisciPatrimonioMobiliare.Visible = True
            btnInserisciRedditi.Visible = True
            btnModificaDetrazioni.Visible = True
            btnModificaNucleo.Visible = True
            btnModificaOspite.Visible = True
            btnModificaPatrimonioImmobiliare.Visible = True
            btnModificaPatrimonioMobiliare.Visible = True
            btnModificaRedditi.Visible = True
            btnModificaSpese.Visible = True
        Next


        'Dim CTRL2 As Control = Nothing
        'Dim mpContentPlaceHolder2 As ContentPlaceHolder
        'mpContentPlaceHolder2 = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
        'Dim pagina2 As Object = mpContentPlaceHolder2.FindControl("RadPageDecisione")
        'Dim panel2 As Panel = CType(pagina2.FindControl("PanelRadPageDecisione"), Panel)
        'For Each CTRL2 In panel2.Controls
        '    If TypeOf CTRL2 Is RadTextBox Then
        '        DirectCast(CTRL2, RadTextBox).Enabled = True
        '    End If
        '    If TypeOf CTRL2 Is RadDatePicker Then
        '        DirectCast(CTRL2, RadDatePicker).Enabled = True
        '    End If
        '    If TypeOf CTRL2 Is Button Then
        '        DirectCast(CTRL2, Button).Enabled = True
        '    End If
        '    If TypeOf CTRL2 Is CheckBox Then
        '        DirectCast(CTRL2, CheckBox).Enabled = True
        '    End If
        'Next


        Dim CTRL3 As Control = Nothing
        Dim mpContentPlaceHolder3 As ContentPlaceHolder
        mpContentPlaceHolder3 = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
        Dim pagina3 As Object = mpContentPlaceHolder3.FindControl("RadPageRequisiti")
        Dim panel3 As Panel = CType(pagina3.FindControl("PanelRadPageRequisiti"), Panel)
        For Each CTRL3 In panel3.Controls
            If TypeOf CTRL3 Is CheckBox Then
                DirectCast(CTRL3, CheckBox).Enabled = True
            End If
        Next

        Select Case idMotivoIstanza.Value
            Case "000"
                chkAccogli.Enabled = True
                RadTextBoxAccogliAcc.Enabled = True
                RadTextBoxMotivoAcc.Enabled = True
                RadDateAccoglimento.Enabled = True
                cmbMotivoAcc.Enabled = True
            Case Else
                'chkPreavviso.Enabled = True
                'RadTextBoxNotaPD.Enabled = True
                'RadTextBoxMotivoPD.Enabled = True
                'RadDatePreavvDiniego.Enabled = True
                'cmbMotivoPD.Enabled = True

                'chkAccogli.Enabled = True
                'RadTextBoxAccogliAcc.Enabled = True
                'RadTextBoxMotivoAcc.Enabled = True
                'RadDateAccoglimento.Enabled = True
                'cmbMotivoAcc.Enabled = True

                Dim CTRL2 As Control = Nothing
                Dim mpContentPlaceHolder2 As ContentPlaceHolder
                mpContentPlaceHolder2 = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
                Dim pagina2 As Object = mpContentPlaceHolder2.FindControl("RadPageDecisione")
                Dim panel2 As Panel = CType(pagina2.FindControl("PanelRadPageDecisione"), Panel)
                For Each CTRL2 In panel2.Controls
                    If TypeOf CTRL2 Is RadTextBox Then
                        DirectCast(CTRL2, RadTextBox).Enabled = True
                    End If
                    If TypeOf CTRL2 Is RadComboBox Then
                        DirectCast(CTRL2, RadComboBox).Enabled = True
                    End If
                    If TypeOf CTRL2 Is RadDatePicker Then
                        DirectCast(CTRL2, RadDatePicker).Enabled = True
                    End If
                    If TypeOf CTRL2 Is Button Then
                        DirectCast(CTRL2, Button).Enabled = True
                    End If
                    If TypeOf CTRL2 Is CheckBox Then
                        DirectCast(CTRL2, CheckBox).Enabled = True
                    End If
                Next
        End Select

        btnSottoponi.Enabled = True
    End Sub

    Protected Sub btnSottoponi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSottoponi.Click
        Try
            If CType(Master.FindControl("frmModify"), HiddenField).Value = "1" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Salvare prima di procedere!", 450, 150, "Attenzione", Nothing, Nothing)
            Else

                connData.apri(True)

                DisabilitaForm()

                chkAccogli.Enabled = False
                RadTextBoxAccogliAcc.Enabled = False
                RadTextBoxMotivoAcc.Enabled = False
                RadDateAccoglimento.Enabled = False
                cmbMotivoAcc.Enabled = False

                par.cmd.CommandText = " delete from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value & " " _
                           & " and ID_STATO_DECISIONE=4"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO ITER_AUTORIZZATIVO_ISTANZA ( ID," _
                   & " DATA, ID_ISTANZA, ID_STATO_DECISIONE,NOTE)" _
                  & " VALUES ( SEQ_ITER_AUTORIZZ_ISTANZA.NEXTVAL,'" & Format(Now, "yyyyMMdd") & "', " _
                  & "" & lIdIstanza.Value & ",4,'')"
                par.cmd.ExecuteNonQuery()

                If Session.Item("OP_RESP_VSA") = 1 Then
                    btnApprova.Enabled = True
                    btnRespingi.Enabled = True
                End If

                'par.cmd.CommandText = "update domande_bando_vsa set id_stato_istanza=3 where id=" & lIdIstanza.Value
                'par.cmd.ExecuteNonQuery()
                'RadComboStato.ClearSelection()
                'RadComboStato.SelectedValue = "3"


                par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)

                connData.chiudi(True)
            End If
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Public Property alertRequisiti() As Boolean
        Get
            If Not (ViewState("par_alertRequisiti") Is Nothing) Then
                Return CLng(ViewState("par_alertRequisiti"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_alertRequisiti") = value
        End Set

    End Property

    Protected Sub btnApprova_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprova.Click
        Try
            Dim gradoParent As Integer = 0
            Dim apertanow As Boolean = False
            Dim errore As Boolean = False
            Dim strEvento As String = ""
            Dim CF As String = ""
            If CType(Master.FindControl("frmModify"), HiddenField).Value = "1" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Salvare prima di procedere!", 450, 150, "Attenzione", Nothing, Nothing)
            Else
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    connData.apri(True)
                    apertanow = True
                End If

                If chkAccogli.Checked = True Then
                    If alertRequisiti = True Then
                        par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Mancanza di requisiti per la lavorazione dell\'istanza!", 450, 150, "Attenzione", Nothing, Nothing)
                        Exit Sub
                    End If
                    If idMotivoIstanza.Value = "1" Then
                        par.cmd.CommandText = "SELECT GRADO_PARENTELA FROM COMP_NUCLEO_VSA WHERE " _
                        & " ID_DICHIARAZIONE = " & lIdDichiarazione.Value & " And PROGR = 0"
                        Dim myReaderX0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderX0.Read Then
                            gradoParent = par.IfNull(myReaderX0("GRADO_PARENTELA"), 1)
                        End If
                        myReaderX0.Close()

                        If gradoParent <> 1 Then
                            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare la tipologia di parentela corretta per il nuovo intestatario!", 450, 150, "Attenzione", Nothing, Nothing)
                            Exit Sub
                        End If
                    End If
                End If

                If chkAccogli.Checked = True Then
                    Select Case idMotivoIstanza.Value
                        Case "1"
                            errore = AutorizzaSubentro(CF)
                            strEvento = "Variazione intestazione a favore di C.F. " & CF & " Istanza num." & lblIstanza.Text
                        Case "2"
                            errore = AutorizzaAmpliamento(CF)
                            strEvento = "Inserito nuovo componente C.F. " & CF & " Istanza num." & lblIstanza.Text
                        Case "7", "13"
                            errore = AutorizzaOspitalita(CF)

                            If idMotivoIstanza.Value = "7" Then
                                strEvento = "Inserito nuovo ospite C.F. " & CF & " Istanza num." & lblIstanza.Text
                            End If
                            If idMotivoIstanza.Value = "13" Then
                                strEvento = "Inserito nuovo coabitante C.F. " & CF & " Istanza num." & lblIstanza.Text
                            End If
                    End Select
                    If errore = False Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            & "VALUES (" & lIdContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F02','" & strEvento & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO ITER_AUTORIZZATIVO_ISTANZA ( ID," _
                                & " DATA, ID_ISTANZA, ID_STATO_DECISIONE,NOTE)" _
                               & " VALUES ( SEQ_ITER_AUTORIZZ_ISTANZA.NEXTVAL,'" & Format(Now, "yyyyMMdd") & "', " _
                               & "" & lIdIstanza.Value & ",6,'')"
                        par.cmd.ExecuteNonQuery()
                        dataAutorizz.Value = Format(Now, "yyyyMMdd")
                        par.cmd.CommandText = "update domande_bando_vsa set id_stato_istanza=4 where id=" & lIdIstanza.Value
                        par.cmd.ExecuteNonQuery()
                        RadComboStato.ClearSelection()
                        RadComboStato.SelectedValue = "4"

                        DisabilitaForm()
                        btnApprova.Enabled = False
                        btnRespingi.Enabled = False
                        btnArchivia.Enabled = True

                        par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)
                    Else
                        connData.chiudi(False)
                    End If
                Else
                    If chkDiniego.Checked = True Then
                        par.cmd.CommandText = "delete from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value & " " _
                             & " and ID_STATO_DECISIONE=10"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO ITER_AUTORIZZATIVO_ISTANZA ( ID," _
                                & " DATA, ID_ISTANZA, ID_STATO_DECISIONE,NOTE)" _
                               & " VALUES ( SEQ_ITER_AUTORIZZ_ISTANZA.NEXTVAL,'" & Format(Now, "yyyyMMdd") & "', " _
                               & "" & lIdIstanza.Value & ",10,'')"
                        par.cmd.ExecuteNonQuery()

                        DisabilitaForm()
                        btnApprova.Enabled = False
                        btnRespingi.Enabled = False
                        btnArchivia.Enabled = True
                        par.cmd.CommandText = "update domande_bando_vsa set id_stato_istanza=4 where id=" & lIdIstanza.Value
                        par.cmd.ExecuteNonQuery()
                        RadComboStato.ClearSelection()
                        RadComboStato.SelectedValue = "4"
                        par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)
                    Else
                        par.cmd.CommandText = "delete from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value & " " _
                             & " and ID_STATO_DECISIONE=9"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO ITER_AUTORIZZATIVO_ISTANZA ( ID," _
                                & " DATA, ID_ISTANZA, ID_STATO_DECISIONE,NOTE)" _
                               & " VALUES ( SEQ_ITER_AUTORIZZ_ISTANZA.NEXTVAL,'" & Format(Now, "yyyyMMdd") & "', " _
                               & "" & lIdIstanza.Value & ",9,'')"
                        par.cmd.ExecuteNonQuery()

                        chkControdeduz.Enabled = True
                        'RadTextBoxMotivoC.Enabled = True
                        RadTextBoxNInt.Enabled = True
                        RadDateControdeduzioni.Enabled = True

                        chkDiniego.Enabled = True
                        cmbMotivoDin.Enabled = True
                        RadTextBoxMotDin.Enabled = True
                        RadTextBoxNotaDin.Enabled = True
                        RadDateDiniego.Enabled = True
                        btnApprova.Enabled = False
                        btnRespingi.Enabled = False
                        par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)

                    End If
                End If
                If apertanow Then
                    connData.chiudi(True)
                End If
            End If
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnRespingi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRespingi.Click
        Try
            If CType(Master.FindControl("frmModify"), HiddenField).Value = "1" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Salvare prima di procedere!", 450, 150, "Attenzione", Nothing, Nothing)
            Else

                Dim apertanow As Boolean = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    connData.apri(True)
                    apertanow = True
                End If

                chkPreavviso.Checked = False
                cmbMotivoPD.ClearSelection()
                RadTextBoxMotivoPD.Text = ""
                RadTextBoxNotaPD.Text = ""
                RadDatePreavvDiniego.Clear()

                chkControdeduz.Checked = False
                RadTextBoxNInt.Text = ""
                RadDateControdeduzioni.Clear()

                chkAccogli.Checked = False
                cmbMotivoAcc.ClearSelection()
                RadTextBoxMotivoAcc.Text = ""
                RadTextBoxAccogliAcc.Text = ""
                RadDateAccoglimento.Clear()

                chkDiniego.Checked = False
                cmbMotivoDin.ClearSelection()
                RadTextBoxMotDin.Text = ""
                RadTextBoxNotaDin.Text = ""
                RadDateDiniego.Clear()

                par.cmd.CommandText = "delete from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO ITER_AUTORIZZATIVO_ISTANZA ( ID," _
                   & " DATA, ID_ISTANZA, ID_STATO_DECISIONE,NOTE)" _
                  & " VALUES ( SEQ_ITER_AUTORIZZ_ISTANZA.NEXTVAL,'" & Format(Now, "yyyyMMdd") & "', " _
                  & "" & lIdIstanza.Value & ",7,'')"
                par.cmd.ExecuteNonQuery()


                Dim CTRL3 As Control = Nothing
                Dim mpContentPlaceHolder3 As ContentPlaceHolder
                mpContentPlaceHolder3 = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
                Dim pagina3 As Object = mpContentPlaceHolder3.FindControl("RadPageRequisiti")
                Dim panel3 As Panel = CType(pagina3.FindControl("PanelRadPageRequisiti"), Panel)
                For Each CTRL3 In panel3.Controls
                    If TypeOf CTRL3 Is CheckBoxList Then
                        DirectCast(CTRL3, CheckBoxList).Enabled = True
                    End If
                Next

                'AbilitaForm()

                'par.cmd.CommandText = "select * from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value & " " _
                '           & " and ID_STATO_DECISIONE=2"
                'Dim myReaderSD3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReaderSD3.HasRows Then
                '    chkControdeduz.Enabled = True
                '    'RadTextBoxMotivoC.Enabled = True
                '    RadTextBoxNInt.Enabled = True
                '    RadDateControdeduzioni.Enabled = True
                'End If
                'myReaderSD3.Close()

                'If chkControdeduz.Enabled = True Then
                '    par.cmd.CommandText = "delete from ITER_AUTORIZZATIVO_ISTANZA where ID_STATO_DECISIONE =2  and id_istanza=" & lIdIstanza.Value
                '    par.cmd.ExecuteNonQuery()
                '    chkControdeduz.Checked = False
                '    'RadTextBoxMotivoC.Text = ""
                '    RadTextBoxNInt.Text = ""
                '    RadDateControdeduzioni.Clear()
                'End If

                'par.cmd.CommandText = "select * from ITER_AUTORIZZATIVO_ISTANZA where id_istanza=" & lIdIstanza.Value & " " _
                '          & " and ID_STATO_DECISIONE=5"
                'myReaderSD3 = par.cmd.ExecuteReader()
                'If myReaderSD3.Read Then
                '    chkDiniego.Enabled = True
                '    cmbMotivoDin.Enabled = True
                '    RadTextBoxMotDin.Enabled = True
                '    RadTextBoxNotaDin.Enabled = True
                '    RadDateDiniego.Enabled = True
                'Else
                '    chkDiniego.Enabled = False
                '    cmbMotivoDin.Enabled = False
                '    RadTextBoxMotDin.Enabled = False
                '    RadTextBoxNotaDin.Enabled = False
                '    RadDateDiniego.Enabled = False
                'End If
                'myReaderSD3.Close()

                'If chkDiniego.Enabled = True Then
                '    par.cmd.CommandText = "delete from ITER_AUTORIZZATIVO_ISTANZA where ID_STATO_DECISIONE =5  and id_istanza=" & lIdIstanza.Value
                '    par.cmd.ExecuteNonQuery()
                '    chkDiniego.Checked = False
                '    cmbMotivoDin.SelectedValue = ""
                '    RadTextBoxMotDin.Text = ""
                '    RadTextBoxNotaDin.Text = ""
                '    RadDateDiniego.Clear()
                'End If

                'btnSottoponi.Enabled = False
                'btnApprova.Enabled = False
                'btnRespingi.Enabled = False
                AbilitaTabDecisioni()

                par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)

                If apertanow Then
                    connData.chiudi(True)
                End If
            End If
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Public Function RicavaBarCode39(ByVal Codice As String, ByVal DoveSalvare As String, Optional ByVal BarHeight As Integer = 40, Optional ByVal ImageWidth As Integer = 480, Optional ByVal ImageHeight As Integer = 40) As String
        Try
            Dim NomeFile As String = "CodeBar_" & Codice & "_" & Format(Now, "yyyyMMddHHmmss") & ".jpg"
            Dim codeBarImage As New System.Drawing.Bitmap(ImageWidth, ImageHeight)
            Dim barcode As New iTextSharp.text.pdf.Barcode39
            barcode.Code = Codice
            barcode.StartStopText = False
            barcode.Extended = False
            barcode.BarHeight = 28.0F
            barcode.Size = 12.0F
            barcode.N = 3.2F
            barcode.Baseline = 12.0F
            barcode.X = 1.09F
            codeBarImage = barcode.CreateDrawingImage(Color.Black, Color.White)
            codeBarImage.Save(System.Web.HttpContext.Current.Server.MapPath("~\" & DoveSalvare & "\") & NomeFile, System.Drawing.Imaging.ImageFormat.Jpeg)
            RicavaBarCode39 = NomeFile
        Catch ex As Exception
            RicavaBarCode39 = ""
        End Try
    End Function

    Private Sub GeneraFrontespizio()

        Dim codUi As String = ""
        Dim idDom As Long = 0
        Dim Data_Ora_Stampa As String = Format(Now, "yyyyMMddHHmmss")
        Dim CodiceProcesso As String = ""
        Dim IndiceProcesso As String = "4"
        Dim Progressivo As Integer = 0
        Dim BarCodeDaStampare As String = ""

        Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Frontespizio.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))

        Dim contenuto As String = sr1.ReadToEnd()
        sr1.Close()

        Dim BarcodeMetodo As String = "TELERIK"
        par.cmd.CommandText = "select valore from parameter where id=129"
        Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderS.Read Then
            BarcodeMetodo = par.IfNull(myReaderS(0), "TELERIK")
        End If
        myReaderS.Close()

        par.cmd.CommandText = "select  cognome||' '|| nome as rich from dichiarazioni_vsa,comp_nucleo_vsa,domande_bando_vsa where dichiarazioni_vsa.id = comp_nucleo_vsa.id_dichiarazione " _
            & " and dichiarazioni_vsa.id = domande_bando_vsa.id_dichiarazione And domande_bando_vsa.id=" & lIdIstanza.Value & " And comp_nucleo_vsa.progr = 0"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            contenuto = Replace(contenuto, "$dichiarante$", par.IfNull(myReader("rich"), ""))
        Else
            contenuto = Replace(contenuto, "$dichiarante$", "")
        End If
        myReader.Close()

        par.cmd.CommandText = "select siscom_mi.getintestatari(id) as intest from siscom_mi.rapporti_utenza where id=" & lIdContratto.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myReader("intest"), ""))
        End If
        myReader.Close()

        par.cmd.CommandText = "SELECT INDIRIZZI.*,comuni_nazioni.nome,comuni_nazioni.sigla FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI," _
                & " comuni_nazioni,siscom_mi.INDIRIZZI WHERE comuni_nazioni.cod=INDIRIZZI.cod_comune And INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO And " _
                & " UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA And UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE Is NULL And UNITA_CONTRATTUALE.ID_CONTRATTO=" & lIdContratto.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            contenuto = Replace(contenuto, "$indirizzounita$", par.IfNull(myReader("descrizione"), "") & ", " & par.IfNull(myReader("civico"), "") & "   " & par.IfNull(myReader("cap"), "") & " " & par.IfNull(myReader("nome"), "") & " " & par.IfNull(myReader("sigla"), ""))
        End If
        myReader.Close()

        par.cmd.CommandText = "select TO_CHAR(TO_DATE(data,'yyyyMMddHH24MISS'),'dd/mm/yyyy - HH24:mi') as dataA from iter_autorizzativo_istanza where id_istanza=" & lIdIstanza.Value & " and id_stato_decisione=3"
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            contenuto = Replace(contenuto, "$dataautorizzazione$", par.IfNull(myReader("dataA"), ""))
        Else
            contenuto = Replace(contenuto, "$dataautorizzazione$", "")
        End If
        myReader.Close()


        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.id as idDom,DOMANDE_BANDO_VSA.pg as pgDOM," _
               & "TO_CHAR(TO_DATE(data_presentazione,'yyyyMMddHH24MISS'),'dd/mm/yyyy - HH24:mi') as data_pres," _
               & "TO_CHAR(TO_DATE(data_autorizzazione,'yyyyMMddHH24MISS'),'dd/mm/yyyy - HH24:mi') as data_autorizz," _
               & " T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE AS CAUSALE_DOM," _
               & " T_MOTIVO_DOMANDA_VSA.COD_PROCESSO_KOFAX " _
               & "FROM DICHIARAZIONI_VSA,T_CAUSALI_DOMANDA_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA WHERE  " _
               & " DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE  " _
               & "AND DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA = T_CAUSALI_DOMANDA_VSA.COD(+) " _
               & " AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID " _
               & " AND DOMANDE_BANDO_VSA.id = " & lIdIstanza.Value & ""
        myReader = par.cmd.ExecuteReader
        If myReader.Read Then
            idDom = par.IfNull(myReader("idDom"), 0)
            CodiceProcesso = par.IfNull(myReader("COD_PROCESSO_KOFAX"), "XXXXX")
            contenuto = Replace(contenuto, "$pgprocesso$", par.IfNull(myReader("pgDOM"), ""))
            contenuto = Replace(contenuto, "$nomeprocesso$", par.IfNull(myReader("MOT_DOMANDA"), ""))
            contenuto = Replace(contenuto, "$tipospecifico$", par.IfNull(myReader("CAUSALE_DOM"), ""))
            contenuto = Replace(contenuto, "$datapresentazione$", par.IfNull(myReader("data_pres"), ""))
        Else
            CodiceProcesso = Request.QueryString("CODK")
            par.cmd.CommandText = "select * from T_MOTIVO_DOMANDA_VSA where COD_PROCESSO_KOFAX='" & CodiceProcesso & "'"
            Dim myReader00 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader00.Read Then
                contenuto = Replace(contenuto, "$nomeprocesso$", par.IfNull(myReader00("DESCRIZIONE"), ""))
            End If
            myReader00.Close()
            contenuto = Replace(contenuto, "$pgprocesso$", "")
            contenuto = Replace(contenuto, "$tipospecifico$", "")
            contenuto = Replace(contenuto, "$datapresentazione$", "")
            contenuto = Replace(contenuto, "$dataautorizzazione$", "")
        End If
        myReader.Close()

        contenuto = Replace(contenuto, "$datastampa$", Mid(Data_Ora_Stampa, 7, 2) & "/" & Mid(Data_Ora_Stampa, 5, 2) & "/" & Mid(Data_Ora_Stampa, 1, 4) & " - " & Mid(Data_Ora_Stampa, 9, 2) & ":" & Mid(Data_Ora_Stampa, 11, 2))

        If Not Directory.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\NUOVA_GEST_LOCATARI\")) Then
            Directory.CreateDirectory(Server.MapPath("..\ALLEGATI\CONTRATTI\NUOVA_GEST_LOCATARI\"))
        End If

        par.cmd.CommandText = "select cod_contratto from siscom_mi.rapporti_utenza where id =" & lIdContratto.Value
        Dim codContr As String = par.IfNull(par.cmd.ExecuteScalar, "")

        contenuto = Replace(contenuto, "$codicecontratto$", codContr)

        Dim FileCodice As String = ""
        par.cmd.CommandText = "SELECT MAX(PROGRESSIVO) FROM PROCESSI_BARCODE_STAMPE WHERE ID_PROCESSO=4 AND ID_CONTRATTO=" & lIdContratto.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            Progressivo = par.IfNull(myReader(0), 0) + 1
            BarCodeDaStampare = UCase(codContr) & CodiceProcesso & Format(Progressivo, "00")
            par.cmd.CommandText = "INSERT INTO PROCESSI_BARCODE_STAMPE (ID,ID_PROCESSO,ID_CONTRATTO,PROGRESSIVO,DATA_ORA,CODICE,ID_ISTANZA) VALUES " _
                & "(SEQ_PROCESSI_BARCODE_STAMPE.NEXTVAL," & IndiceProcesso & "," & lIdContratto.Value & "," & Progressivo & ",'" & Data_Ora_Stampa & "','" & BarCodeDaStampare & "'," & lIdIstanza.Value & ")"
            par.cmd.ExecuteNonQuery()
            contenuto = Replace(contenuto, "$codice$", "*" & BarCodeDaStampare & "*")
            If BarcodeMetodo = "TELERIK" Then
                FileCodice = par.CreaBarCode128(BarCodeDaStampare, "ALLEGATI\CONTRATTI\NUOVA_GEST_LOCATARI\", False)
            Else
                FileCodice = RicavaBarCode39(BarCodeDaStampare, "ALLEGATI\CONTRATTI\NUOVA_GEST_LOCATARI\")
            End If
            contenuto = Replace(contenuto, "$barcode$", FileCodice)
        End If
        myReader.Close()

        Dim url As String = Server.MapPath("..\FileTemp\")

        Dim pdfConverter1 As PdfConverter = New PdfConverter
        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
        If Licenza <> "" Then
            pdfConverter1.LicenseKey = Licenza
        End If
        pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        pdfConverter1.PdfDocumentOptions.ShowHeader = False
        pdfConverter1.PdfDocumentOptions.ShowFooter = False
        pdfConverter1.PdfDocumentOptions.LeftMargin = 5
        pdfConverter1.PdfDocumentOptions.RightMargin = 5
        pdfConverter1.PdfDocumentOptions.TopMargin = 5
        pdfConverter1.PdfDocumentOptions.BottomMargin = 5
        pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

        pdfConverter1.PdfDocumentOptions.ShowHeader = False
        pdfConverter1.PdfFooterOptions.FooterText = ("")
        pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
        pdfConverter1.PdfFooterOptions.DrawFooterLine = False
        pdfConverter1.PdfFooterOptions.PageNumberText = ""
        pdfConverter1.PdfFooterOptions.ShowPageNumber = False

        Dim nomefile As String = "Frontespizio_" & Format(Now, "yyyyMMddHHmmss")

        pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\ALLEGATI\CONTRATTI\NUOVA_GEST_LOCATARI\"))

        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim zipfic As String

        zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\NUOVA_GEST_LOCATARI\" & nomefile & ".zip")

        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)
        '
        Dim strFile As String
        strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
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

        Dim elencoFile As String = nomefile & ".zip"

        par.cmd.CommandText = "INSERT INTO ELENCO_STAMPE_GENERATE (ID, NOME, PATH, DESCRIZIONE,DATA_ORA,ID_ISTANZA) " _
                    & " VALUES ( SEQ_ELENCO_STAMPE_GENERATE.NEXTVAL," _
                    & "'" & elencoFile & "'," _
                    & "'../ALLEGATI/CONTRATTI/NUOVA_GEST_LOCATARI'," _
                    & "'Frontespizio'," & Format(Now, "yyyyMMddHHmmss") & "," & lIdIstanza.Value & ")"
        par.cmd.ExecuteNonQuery()

        If IO.File.Exists(Server.MapPath("../ALLEGATI/CONTRATTI/NUOVA_GEST_LOCATARI/" & nomefile & ".zip")) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { if (document.getElementById('HFForceNoLoadingPanel')) { document.getElementById('HFForceNoLoadingPanel').value = 1;} var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../ALLEGATI/CONTRATTI/NUOVA_GEST_LOCATARI/" & nomefile & ".zip','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Si è verificato un errore durante l\'esportazione. Riprovare!", 450, 150, "Attenzione", Nothing, Nothing)
        End If

    End Sub

    Private Sub GeneraLettere()
        Dim elencoFile As String = ""
        Dim nomeFile As String = "" ' = "Richiesta_Ospitalita_" & Format(Now, "yyyyMMddHHmmss")
        Dim BaseFile As String = "" ' "Richiesta_Ospitalita_" & Format(Now, "yyyyMMddHHmmss")
        Dim file1 As String = BaseFile & ".RTF"
        Dim fileName As String = Server.MapPath("..\FileTemp\") & file1
        Dim fileNamePDF As String = Server.MapPath("..\FileTemp\") & BaseFile & ".pdf"
        Dim NomeModello As String = ""

        Dim trovato As Boolean = False


        'CARICO IL MODELLO LEGGENDOLO DAL BLOB SUL DB
        par.cmd.CommandText = "SELECT GEST_LOCATARI_TIPO_MODELLI.*,replace(descrizione,' ','_') as nomeFile FROM GEST_LOCATARI_TIPO_MODELLI WHERE id_tipo_stampa = " & idSelectedDoc.Value
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            NomeModello = par.IfNull(myReader("descrizione"), "")
            nomeFile = par.IfNull(myReader("nomeFile"), "") & "_" & Format(Now, "yyyyMMddHHmmss")
            BaseFile = nomeFile
            Dim bw As BinaryWriter
            If par.IfNull(myReader("MODELLO"), "").LENGTH > 0 Then
                Dim fs As New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write)
                bw = New BinaryWriter(fs)
                bw.Write(myReader("MODELLO"))
                bw.Flush()
                bw.Close()
                trovato = True
            End If
        End If
        myReader.Close()

        Dim pdfBytes As Byte()
        Dim pdfBase64 As String = ""

        If trovato = True Then
            Dim sr1 As StreamReader = New StreamReader(fileName, System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            par.cmd.CommandText = "select * from GEST_LOCATARI_marcatori where query is not null order by id asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Dim valoreSost As String = ""
            For Each r As Data.DataRow In dt.Rows
                valoreSost = ""
                If Not String.IsNullOrEmpty(r.Item("QUERY").ToString) Then
                    par.cmd.CommandText = r.Item("QUERY").ToString.Replace("#IDISTANZA#", lIdIstanza.Value).Replace("#IDUI#", idUnita.Value).Replace("#IDOPERATORE#", Session.Item("ID_OPERATORE"))
                    valoreSost = par.IfNull(par.cmd.ExecuteScalar, "")
                Else
                    valoreSost = ""
                End If
                If valoreSost <> "" Then
                    valoreSost = valoreSost.Replace(Chr(10), "\par ")
                End If

                If r.Item("cod") = "$firmaResponsabile$" Then
                    Dim newImage As System.Drawing.Image = System.Drawing.Image.FromFile(Server.MapPath(valoreSost))
                    Dim resultImage As System.Drawing.Image = ResizeImage(newImage, New Size(180, 50))

                    Dim s1 = par.InsertImageToRTF(resultImage)
                    valoreSost = s1
                End If

                contenuto = Replace(contenuto, r.Item("cod"), par.IfEmpty(valoreSost, ""))
            Next



            File.Delete(fileName)
            Dim resRtf As Boolean = False
            BaseFile = nomeFile

            file1 = BaseFile & ".rtf"
            fileName = Server.MapPath("..\FileTemp\") & file1

            Dim basefilePDF As String
            If resRtf = False Then
                basefilePDF = BaseFile & ".pdf"

            Else
                basefilePDF = file1

            End If
            fileNamePDF = Server.MapPath("..\FileTemp\") & basefilePDF

            Dim sr As StreamWriter = New StreamWriter(fileName, False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()
            Dim rp As New Rpn
            Dim i As Boolean
            Dim K As Int64 = 0
            If resRtf = False Then

                'Dim result As Int64 = Rpn.RpsSetLicenseInfo("G927S-F6R7A-7VH31", "srab35887-1", "S&S SISTEMI E SOLUZIONI S.R.L.")

                Dim result As Int64 = Rpn.RpsSetLicenseInfo("8RWQS-6Y9UC-HA2L1-91017", "srab35887-1", "S&S SISTEMI E SOLUZIONI S.R.L.")
                rp.InWebServer = True
                rp.EmbedFonts = True
                rp.ExactTextPlacement = True
                i = rp.RpsConvertFile(fileName, fileNamePDF)
            End If
        End If

        If File.Exists(Server.MapPath("~\FileTemp\" & nomeFile & ".pdf")) Then
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim strFile As String
            strFile = Server.MapPath("~\FileTemp\" & nomeFile & ".pdf")
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
            File.Delete(strFile)
            Dim zipfic As String
            If Not Directory.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\NUOVA_GEST_LOCATARI")) Then
                Directory.CreateDirectory(Server.MapPath("..\ALLEGATI\CONTRATTI\NUOVA_GEST_LOCATARI"))
            End If
            zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\NUOVA_GEST_LOCATARI\" & nomeFile & ".zip")
            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            If String.IsNullOrEmpty(Trim(elencoFile)) Then
                elencoFile = nomeFile & ".zip"
            End If

            par.cmd.CommandText = "INSERT INTO ELENCO_STAMPE_GENERATE (ID, NOME, PATH, DESCRIZIONE,DATA_ORA,ID_ISTANZA) " _
                        & " VALUES ( SEQ_ELENCO_STAMPE_GENERATE.NEXTVAL," _
                        & "'" & elencoFile & "'," _
                        & "'../ALLEGATI/CONTRATTI/NUOVA_GEST_LOCATARI'," _
                        & "'" & NomeModello & "'," & Format(Now, "yyyyMMddHHmmss") & "," & lIdIstanza.Value & ")"
            par.cmd.ExecuteNonQuery()

            If IO.File.Exists(Server.MapPath("../ALLEGATI/CONTRATTI/NUOVA_GEST_LOCATARI/" & nomeFile & ".zip")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { if (document.getElementById('HFForceNoLoadingPanel')) { document.getElementById('HFForceNoLoadingPanel').value = 1;} var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../ALLEGATI/CONTRATTI/NUOVA_GEST_LOCATARI/" & nomeFile & ".zip','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Si è verificato un errore durante l\'esportazione. Riprovare!", 450, 150, "Attenzione", Nothing, Nothing)
            End If
        End If

    End Sub

    Protected Sub btnStampaDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaDoc.Click
        Try

            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If

            Select Case CInt(idSelectedDoc.Value)
                Case > 19
                    GeneraFrontespizio()
                Case Else
                    GeneraLettere()
            End Select

            If ApertaNow Then connData.chiudi(False)
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            par.visualizzaErrore(ex, Me.Page)
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " btnStampaDoc_Click() - " & ex.ToString)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function ResizeImage(ByVal sourceImage As System.Drawing.Image, ByVal newSize As Size) As System.Drawing.Image
        Dim bitmap As New Bitmap(newSize.Width, newSize.Height)
        Dim g As Graphics = Graphics.FromImage(DirectCast(bitmap, System.Drawing.Image))
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.DrawImage(sourceImage, 0, 0, newSize.Width, newSize.Height)
        g.Dispose()

        Return DirectCast(bitmap, System.Drawing.Image)
    End Function

    Private Function VerificaSovraffollamento(ByVal NumComponenti As Integer, ByVal supUtile As Decimal) As Boolean
        Dim sovraffollamento As Boolean = False

        'Select Case NumComponenti
        '    Case 1
        '        If supUtile < 28 Then
        '            sovraffollamento = True
        '        End If
        '    Case 2
        '        If supUtile < 38 Then
        '            sovraffollamento = True
        '        End If
        '    Case 3
        '        If supUtile < 48 Then
        '            sovraffollamento = True
        '        End If
        '    Case 4
        '        If supUtile < 58 Then
        '            sovraffollamento = True
        '        End If
        '    Case 5
        '        If supUtile < 68 Then
        '            sovraffollamento = True
        '        End If
        '    Case 6
        '        If supUtile < 78 Then
        '            sovraffollamento = True
        '        End If
        '    Case Is >= 7
        '        If supUtile < 78 Then
        '            sovraffollamento = True
        '        End If
        'End Select
		
		
		Select Case NumComponenti
            Case 1, 2
                If supUtile < 17 Then
                    sovraffollamento = True
                End If
            Case 3
                If supUtile < 34 Then
                    sovraffollamento = True
                End If
            Case 4, 5
                If supUtile < 50 Then
                    sovraffollamento = True
                End If
            Case 6
                If supUtile < 67 Then
                    sovraffollamento = True
                End If
            Case Is >= 7
                If supUtile < 84 Then
                    sovraffollamento = True
                End If
        End Select


        Return sovraffollamento

    End Function

    Protected Sub dgvElencoFile_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvElencoFile.NeedDataSource
        Try
            Dim apertanow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                apertanow = True
            End If
            Dim query As String = "SELECT id, nome,descrizione,TO_DATE(ELENCO_STAMPE_GENERATE.DATA_ORA, 'YYYYmmDDHH24miss') AS DATA_ORA FROM ELENCO_STAMPE_GENERATE " _
                                   & " where id_istanza=" & lIdIstanza.Value & " order by 4 desc"
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(query)
            If apertanow Then
                connData.chiudi(False)
            End If
            dgvElencoFile.DataSource = dt
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Try

            If Not String.IsNullOrEmpty(Trim(idSelectedStampa.Value)) Then
                Dim DownloadAllegato As String = ""

                Dim nomeAllegato As String = ""
                connData.apri(False)
                par.cmd.CommandText = "SELECT PATH, NOME FROM ELENCO_STAMPE_GENERATE WHERE ID = " & idSelectedStampa.Value
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    DownloadAllegato = par.IfNull(MyReader("PATH"), "")
                    nomeAllegato = par.IfNull(MyReader("NOME"), "")
                End If
                MyReader.Close()
                connData.chiudi(False)
                If Not String.IsNullOrEmpty(Trim(DownloadAllegato)) Then
                    If File.Exists(Server.MapPath(DownloadAllegato & "/" & nomeAllegato)) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() {if (document.getElementById('HFForceNoLoadingPanel')) { document.getElementById('HFForceNoLoadingPanel').value = 1;}  var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('" & DownloadAllegato & "/" & nomeAllegato & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                    Else
                        par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Allegato non Disponibile!", 450, 150, "Attenzione", Nothing, Nothing)
                    End If
                Else
                    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Allegato non Disponibile!", 450, 150, "Attenzione", Nothing, Nothing)

                End If

            Else
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Allegato non Disponibile!", 450, 150, "Attenzione", Nothing, Nothing)
            End If
        Catch ex As Exception
            par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub cmbMotivoAcc_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbMotivoAcc.SelectedIndexChanged
        Dim motiviSelezionati As String = ""
        For Each item As RadComboBoxItem In cmbMotivoAcc.Items
            If item.Checked Then
                If String.IsNullOrEmpty(Trim(motiviSelezionati)) Then
                    motiviSelezionati = item.Text
                Else
                    motiviSelezionati &= "," & item.Text
                End If
            Else
                motiviSelezionati &= ""
            End If
        Next
        RadTextBoxMotivoAcc.Text = motiviSelezionati
    End Sub


    Protected Sub cmbMotivoPD_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbMotivoPD.SelectedIndexChanged
        Dim motiviSelezionati As String = ""
        For Each item As RadComboBoxItem In cmbMotivoPD.Items
            If item.Checked Then
                If String.IsNullOrEmpty(Trim(motiviSelezionati)) Then
                    motiviSelezionati = item.Text
                Else
                    motiviSelezionati &= "," & item.Text
                End If
            Else
                motiviSelezionati &= ""
            End If
        Next
        RadTextBoxMotivoPD.Text = motiviSelezionati
    End Sub

    Private Sub msgOpLock()
        Try
            Dim OpenNow As Boolean = False
            If connData.Connessione.State <> Data.ConnectionState.Open Then
                connData.apri()
                OpenNow = True
            End If
            par.cmd.CommandText = "select operatore,expiration from sepacom_lock,operatori where operatori.id = id_operatore and lock_name ='" & hiddenLockCorrenti.Value & "'"
            Dim operatore As String = par.IfNull(par.cmd.ExecuteScalar, "")
            Dim msg As String = ""
            If Not String.IsNullOrEmpty(operatore) Then
                msg += operatore
            End If
            msg = "PRATICA APERTA DALL\'OPERATORE \'" & msg & "\'.<br />NON È POSSIBILE EFFETTUARE MODIFICHE!"
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), msg, 450, 150, "Attenzione", Nothing, Nothing)

            If OpenNow = True Then
                connData.chiudi()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'Metto tutto in sola lettura per maschera già aperta da altro operatore
        If hdnSlLocked.Value = 1 Then
            msgOpLock()
            DisabilitaForm()
            btnSalva.Visible = False
            btnStampaDoc.Visible = False
            btnDownload.Visible = False
            btnApprova.Enabled = False
            btnRespingi.Enabled = False
        End If
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey111", "NascondiBottoni();", True)
    End Sub

    Protected Sub btnArchivia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnArchivia.Click
        Try
            connData.apri(True)

            par.cmd.CommandText = "update domande_bando_vsa set id_stato_istanza=5,data_archiviazione='" & Format(Now, "yyyyMMdd") & "' where id=" & lIdIstanza.Value
            par.cmd.ExecuteNonQuery()
            RadComboStato.ClearSelection()
            RadComboStato.SelectedValue = "5"
            txtDataArchiviazione.Text = Format(Now, "dd/MM/yyyy")
            connData.chiudi(True)

            par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try

    End Sub

    Protected Sub RadComboTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboTipo.SelectedIndexChanged
        Try
            idTipoProcesso.Value = RadComboTipo.SelectedValue
            If RadComboTipo.SelectedValue <> "-1" Then
                connData.apri(True)

                par.cmd.CommandText = "update domande_bando_vsa set id_tipo_ospitalita=" & RadComboTipo.SelectedValue & " where id=" & lIdIstanza.Value
                par.cmd.ExecuteNonQuery()


                connData.chiudi(True)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub chkRequisiti_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkRequisiti.SelectedIndexChanged
        Try
            connData.apri(True)
            For Each Items As ListItem In chkRequisiti.Items
                If Items.Value = "2" Then
                    If Items.Selected = True Then
                        alertRequisiti = False
                        forzaRequisito.Value = "1"
                        par.cmd.CommandText = "INSERT INTO REQUISITI_NUOVA_ISTANZA (ID_ISTANZA,ID_REQUISITO,FORZA_FLAG) VALUES (" & lIdIstanza.Value & ",2,1)"
                        par.cmd.ExecuteNonQuery()
                    Else
                        alertRequisiti = True
                        forzaRequisito.Value = "0"
                    End If
                End If
            Next
            connData.chiudi(True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub cmbMotivoPD_DataBound(sender As Object, e As EventArgs) Handles cmbMotivoPD.DataBound

    End Sub
End Class
