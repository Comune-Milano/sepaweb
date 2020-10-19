Imports Telerik.Web.UI
Imports System.Drawing
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class SICUREZZA_NuovoIntervento
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing




    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then

                'CaricaSedeTerritoriale()

                par.caricaComboTelerik("SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 ORDER BY DENOMINAZIONE ASC", cmbEdificio, "ID", "DENOMINAZIONE", True)
                par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TIPO_MESSA_IN_SICUREZZA ORDER BY id ASC", cmbTipoMessaInSicurezza, "ID", "DESCRIZIONE")
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TAB_STATI_INTERVENTI ORDER BY id ASC", cmbStatoInterv, "ID", "DESCRIZIONE", True)
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPO_INTERVENTO ORDER BY DESCRIZIONE ASC", cmbTipo, "ID", "DESCRIZIONE", True)
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPO_INTERVENTO ORDER BY DESCRIZIONE ASC", cmbTipo, "ID", "DESCRIZIONE", True)

                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.RICHIEDENTE_INTERV_SICUREZZA ORDER BY DESCRIZIONE ASC", cmbRichiedente, "ID", "DESCRIZIONE", True)
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.MODALITA_VERIFICA_SICUREZZA ORDER BY DESCRIZIONE ASC", cmbVerifica, "ID", "DESCRIZIONE", True)
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.MOTIVI_RECUPERO_SICUREZZA ORDER BY DESCRIZIONE ASC", cmbMotivoRecupero, "ID", "DESCRIZIONE", True)

                idIntervento.Value = Request.QueryString("IDI")
                caricaAllegati()
                RicavaTipoIntervento()
                RadGridAllegati.MasterTableView.EditFormSettings.InsertCaption = "Inserimento Allegato"
                RadGridAllegati.MasterTableView.EditFormSettings.CaptionFormatString = "Modifica Allegato"


                par.caricaCheckBoxList("SELECT (select descrizione from siscom_mi.TIPO_INTERVENTO_SOGG_COINVOLTO where id=TIPI_INTERVENTI_SOGG_COINV.ID_TIPO_ENTE_COINVOLTO) as descrizione,ID_TIPO_ENTE_COINVOLTO FROM SISCOM_MI.TIPI_INTERVENTI_SOGG_COINV where id_tipo_intervento=" & tipoIntervento.Value & " and id_ente_coinvolto=8 ORDER BY descrizione ASC", cmbTipoIntLuce, "ID_TIPO_ENTE_COINVOLTO", "descrizione")
                par.caricaCheckBoxList("SELECT (select descrizione from siscom_mi.TIPO_INTERVENTO_SOGG_COINVOLTO where id=TIPI_INTERVENTI_SOGG_COINV.ID_TIPO_ENTE_COINVOLTO) as descrizione,ID_TIPO_ENTE_COINVOLTO FROM SISCOM_MI.TIPI_INTERVENTI_SOGG_COINV where id_tipo_intervento=" & tipoIntervento.Value & " and id_ente_coinvolto=7 ORDER BY descrizione ASC", cmbTipoIntGas, "ID_TIPO_ENTE_COINVOLTO", "descrizione")
                par.caricaComboTelerik("SELECT (select descrizione from siscom_mi.TIPO_INTERVENTO_SOGG_COINVOLTO where id=TIPI_INTERVENTI_SOGG_COINV.ID_TIPO_ENTE_COINVOLTO) as descrizione,ID_TIPO_ENTE_COINVOLTO FROM SISCOM_MI.TIPI_INTERVENTI_SOGG_COINV where id_tipo_intervento=" & tipoIntervento.Value & " and id_ente_coinvolto=5 ORDER BY descrizione ASC", cmbServiziSociali, "ID_TIPO_ENTE_COINVOLTO", "DESCRIZIONE", True)

                par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TIPI_SERVIZIO_INTERVENTO ORDER BY id ASC", chkElencoServizi, "ID", "DESCRIZIONE")
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TAB_STATI_CONTRATTUALE_NUCLEO ORDER BY DESCRIZIONE ASC", cmbNewStatoRU, "ID", "DESCRIZIONE", True)
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TAB_INGRESSO_ALLOGGIO ORDER BY DESCRIZIONE ASC", cmbIngressoAlloggio, "ID", "DESCRIZIONE", True)
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TAB_STATI_ALLOGGIO_ARRIVO ORDER BY DESCRIZIONE ASC", cmbStatoAll, "ID", "DESCRIZIONE", True)

                If Not IsNothing(idIntervento.Value) And idIntervento.Value <> "" And idIntervento.Value <> "0" Then
                    CaricaInterventoDaSegn()
                End If
                If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
                    CType(Me.Master.FindControl("NavigationMenu"), Telerik.Web.UI.RadMenu).Visible = False
                End If

                If txtAssegnatario.Text = "" Then
                    cmbStatoInterv.Enabled = False
                Else
                    cmbStatoInterv.Enabled = True
                    'CaricaGruppiOperatori()
                End If

                If cmbStatoInterv.SelectedValue = "-1" Then
                    imgChiudiSegnalazione.Visible = False
                Else
                    imgChiudiSegnalazione.Visible = True
                End If

                If cmbTipo.SelectedValue = "1" Or cmbTipo.SelectedValue = "2" Then
                    btnCreaIntervServ.Visible = True
                Else
                    btnCreaIntervServ.Visible = False
                End If

                If tipoIntervento.Value = "1" Then
                    cmbStatoInterv.Enabled = True
                End If

                CaricaProcedimenti()
                If Session.Item("FL_SEC_GEST_COMPLETA") = "0" Then
                    btnSalva.Visible = False
                    imgChiudiSegnalazione.Visible = False
                    btnApriProc.Visible = False
                End If
                If Session.Item("FL_SEC_ASS_OPERATORI") = "0" Then
                    txtAssegnatario.ReadOnly = True
                    btnCercaOp2.Visible = False
                End If
                If Not IsNothing(Session.Item("DataGridIntervEsist")) Then
                    Session.Remove("DataGridIntervEsist")
                End If
                CaricaGrid()
                CaricaGridEnteCoinv()

                txtDataApertura.DatePopupButton.ToolTip = String.Empty
                If cmbStatoInterv.SelectedValue > "1" Then
                    txtAssegnatario.ReadOnly = True
                    btnCercaOp2.Visible = False
                Else
                    txtAssegnatario.ReadOnly = False
                    btnCercaOp2.Visible = True
                End If

            End If
            'If cmbTipo.SelectedValue = "5" Then
            '    If cmbStatoInterv.SelectedValue <> "4" Then
            '        btnStampa.Visible = False
            '    Else
            '        btnStampa.Visible = True
            '    End If
            'End If
            'If cmbStatoInterv.SelectedValue <> "1" And cmbStatoInterv.SelectedValue <> "4" Then
            '    btnStampa.Visible = False
            'End If
            txtCodUI.Attributes.Add("onblur", "javascript:document.getElementById('HFBeforeLoading').value='1';")
            cmbTipo.Attributes.Add("onchange", "javascript:visibleOggetti();")
            'cmbStatoInterv.Attributes.Add("onchange", "javascript:cambiaStatoIntervento();")
            btnSalva.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='0';")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub
  
    Private Sub VisualizzaAlert(ByVal TestoMessaggio As String, ByVal Tipo As Integer)
        Select Case Tipo
            Case 1
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','success');", True)
            Case 2
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "warn", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','warn');", True)
            Case 3
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "error", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','error');", True)
        End Select
    End Sub

    Private Sub CreaFascicolo()

        Dim idFasc As Long = 0
        Dim idFascBC As Long = 0
        Dim FileCodice As String = ""
        Dim BarCodeDaStampare As String = ""
        Dim idUnita As Long = 0
        Dim codUI As String = ""

        par.cmd.CommandText = "select * from siscom_mi.interventi_sicurezza where id=" & idIntervento.Value
        Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore0.Read Then
            idUnita = par.IfNull(lettore0("id_unita"), 0)
        End If
        lettore0.Close()

        par.cmd.CommandText = "select * from siscom_mi.interventi_sicurezza where id<>" & idIntervento.Value & " and id_unita=" & idUnita
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            If par.IfNull(lettore("id_fascicolo"), 0) = 0 Then
                par.cmd.CommandText = "select cod_unita_immobiliare from siscom_mi.unita_immobiliari where id=" & idUnita
                Dim lettoreC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreC.Read Then
                    codUI = par.IfNull(lettoreC("cod_unita_immobiliare"), "")
                End If
                lettoreC.Close()

                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_FASCICOLO_SICUREZZA.NEXTVAL FROM DUAL"
                Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore2.Read Then
                    idFasc = par.IfNull(lettore2(0), "-1")
                End If
                lettore2.Close()

                If idUnita <> 0 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.FASCICOLO_SICUREZZA (ID, ID_UNITA) VALUES (" & idFasc & "," & lettore("ID_UNITA") & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE siscom_mi.interventi_sicurezza SET id_fascicolo=" & idFasc & " where id=" & idIntervento.Value
                    par.cmd.ExecuteNonQuery()

                    'Dim BarcodeMetodo As String = "TELERIK"
                    'par.cmd.CommandText = "select valore from parameter where id=129"
                    'Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReaderS.Read Then
                    '    BarcodeMetodo = par.IfNull(myReaderS(0), "TELERIK")
                    'End If
                    'myReaderS.Close()

                    'par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_FASC_BARCODE_SICUREZZA.NEXTVAL FROM DUAL"
                    'Dim lettore22 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    'If lettore22.Read Then
                    '    idFascBC = par.IfNull(lettore22(0), "-1")
                    'End If
                    'lettore22.Close()

                    'BarCodeDaStampare = UCase(codUI) & idIntervento.Value & Format(CStr(idFascBC), "00")

                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.FASCICOLO_BARCODE_SICUREZZA ( ID, ID_FASCICOLO, DATA_ORA, CODICE_A_BARRE)" _
                    '    & "VALUES (" & idFascBC & ", " & idFasc & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & BarCodeDaStampare & "' )"
                    'par.cmd.ExecuteNonQuery()

                    'If BarcodeMetodo = "TELERIK" Then
                    '    FileCodice = par.CreaBarCode128(BarCodeDaStampare, "ALLEGATI\SICUREZZA", False)
                    'Else
                    '    FileCodice = RicavaBarCode39(BarCodeDaStampare, "ALLEGATI\SICUREZZA")
                    'End If
                    idFascicolo.Value = idFasc
                End If
            Else
                par.cmd.CommandText = "UPDATE siscom_mi.interventi_sicurezza SET id_fascicolo=" & par.IfNull(lettore("id_fascicolo"), 0) & " where id=" & idIntervento.Value
                par.cmd.ExecuteNonQuery()
            End If
        End If
        lettore.Close()

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
            barcode.N = 3.20000005F
            barcode.Baseline = 12.0F
            barcode.X = 1.09000003F
            codeBarImage = barcode.CreateDrawingImage(Color.Black, Color.White)
            codeBarImage.Save(System.Web.HttpContext.Current.Server.MapPath("~\" & DoveSalvare & "\") & NomeFile, System.Drawing.Imaging.ImageFormat.Jpeg)
            RicavaBarCode39 = NomeFile
        Catch ex As Exception
            RicavaBarCode39 = ""
        End Try
    End Function

    Private Sub RicavaTipoIntervento()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INTERVENTI_SICUREZZA WHERE ID = " & idIntervento.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                tipoIntervento.Value = par.IfNull(lettore("ID_TIPO_INTERVENTO"), -1)
            End If
            lettore.Close()

            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - CaricaInterventoDaSegn - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaInterventoDaSegn()
        Try
            If idIntervento.Value <> "-1" Then
                connData.apri()
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INTERVENTI_SICUREZZA WHERE ID = " & idIntervento.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then

                    idSegnalazione.Value = par.IfNull(lettore("id_segnalazione"), -1)
                    cmbRichiedente.SelectedValue = par.IfNull(lettore("id_richiedente"), -1)
                    cmbVerifica.SelectedValue = par.IfNull(lettore("ID_MOD_VERIFICA"), -1)
                    cmbTipo.SelectedValue = par.IfNull(lettore("ID_TIPO_INTERVENTO"), -1)
                    tipoIntervento.Value = par.IfNull(lettore("ID_TIPO_INTERVENTO"), -1)
                    txtDataCInt.Text = par.FormattaData(Mid(par.IfNull(Trim(lettore("DATA_ORA_INSERIMENTO")), ""), 1, 8))
                    txtOraCInt.Text = Mid(par.IfNull(Trim(lettore("DATA_ORA_INSERIMENTO")), ""), 9, 2) & ":" & Mid(par.IfNull(Trim(lettore("DATA_ORA_INSERIMENTO")), "          "), 11, 2)
                    'txtOraInterv.Text = Mid(par.IfNull(Trim(lettore("DATA_ORA_INSERIMENTO")), ""), 9, 2) & ":" & Mid(par.IfNull(Trim(lettore("DATA_ORA_INSERIMENTO")), "          "), 11, 2)
                    'txtDataApertura.SelectedDate = par.FormattaData(Mid(par.IfNull(Trim(lettore("DATA_ORA_INSERIMENTO")), ""), 1, 8))
                    Dim query As String = ""
                    Dim unita As Long = 0
                    Dim idCanale As Integer = 0
                    Dim idContrattoSegn As Integer = 0
                    Dim codContrattoSegn As String = ""
                    par.cmd.CommandText = "select nvl(id_contratto,0) as idcont,id_canale from siscom_mi.segnalazioni where id=" & par.IfNull(lettore("id_segnalazione"), -1)
                    Dim lettoreIdC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreIdC.Read Then
                        idCanale = par.IfNull(lettoreIdC("id_Canale"), 0)
                        idContrattoSegn = par.IfNull(lettoreIdC("idcont"), 0)
                    End If
                    lettoreIdC.Close()

                    Dim condCanale As String = ""
                    If idCanale <> 0 Then
                        condCanale = " where id=" & idCanale
                    End If
                    par.caricaComboTelerik("SELECT * FROM SISCOM_MI.CANALE " & condCanale & " ORDER BY ID ASC", cmbSoggCoinvolti, "ID", "DESCRIZIONE", False)

                    If idContrattoSegn = 0 Then
                        par.cmd.CommandText = "SELECT id_contratto,cod_contratto FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.rapporti_utenza WHERE UNITA_CONTRATTUALE.id_contratto=rapporti_utenza.id and id_unita = " & par.IfNull(lettore("id_unita"), 0) & " and rapporti_utenza.id = (siscom_mi.getultimoru(id_unita))"
                        lettoreIdC = par.cmd.ExecuteReader
                        If lettoreIdC.Read Then
                            idContrattoSegn = par.IfNull(lettoreIdC("id_contratto"), 0)
                            codContrattoSegn = par.IfNull(lettoreIdC("cod_contratto"), "")
                        End If
                        lettoreIdC.Close()
                    Else
                        par.cmd.CommandText = "SELECT cod_contratto FROM siscom_mi.rapporti_utenza WHERE id=" & idContrattoSegn
                        lettoreIdC = par.cmd.ExecuteReader
                        If lettoreIdC.Read Then
                            codContrattoSegn = par.IfNull(lettoreIdC("cod_contratto"), "")
                        End If
                        lettoreIdC.Close()
                    End If

                    lblContratto.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "document.getElementById('txtModificato').value='1';window.open('../Contratti/Contratto.aspx?LT=1&CC=1&ID=" & idContrattoSegn & "&COD=" & codContrattoSegn & "','RU','top=0,left=0,width=1160,height=780,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">" & codContrattoSegn & "</a>"

                    If idContrattoSegn <> 0 Then
                        query = "select tipologia_contratto_locazione.COD, tipologia_contratto_locazione.descrizione from siscom_mi.rapporti_utenza,siscom_mi.tipologia_contratto_locazione where rapporti_utenza.id=" & idContrattoSegn & " and " _
                            & " rapporti_utenza.cod_tipologia_contr_loc=tipologia_contratto_locazione.cod"
                        par.caricaComboTelerik(query, cmbTipoRU, "COD", "DESCRIZIONE", True, , , True)

                        query = "select siscom_mi.getstatocontratto(id) as stato from siscom_mi.rapporti_utenza where rapporti_utenza.id=" & idContrattoSegn
                        par.caricaComboTelerik(query, cmbStatoRU, "STATO", "STATO", True, , , True)
                    End If

                    query = "SELECT EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id = " & par.IfNull(lettore("ID_EDIFICIO"), 0)
                    par.caricaComboTelerik(query, cmbEdificio, "ID", "DENOMINAZIONE", True, , , True)

                    unita = par.IfNull(lettore("id_unita"), 0)
                    If par.IfEmpty(unita, 0) > 0 Then
                        par.cmd.CommandText = "SELECT * FROM siscom_mi.UNITA_IMMOBILIARI WHERE id = " & unita
                        Dim readerInt As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If readerInt.Read Then
                            txtCodUI.Text = par.IfNull(readerInt("COD_UNITA_IMMOBILIARE"), "")
                            query = "SELECT ID,descrizione ||' '|| civico as descr FROM siscom_mi.indirizzi WHERE ID = " & par.IfNull(readerInt("id_indirizzo"), "-1")
                            par.caricaComboTelerik(query, cmbIndirizzo, "ID", "DESCR", True, , , True)
                            query = "SELECT ID,descrizione FROM siscom_mi.SCALE_EDIFICI WHERE ID = " & par.IfNull(readerInt("id_scala"), "-1")
                            par.caricaComboTelerik(query, cmbScala, "ID", "DESCRIZIONE", True, , , True)
                            query = "select COD, descrizione from siscom_mi.TIPO_LIVELLO_PIANO where COD = " & par.IfNull(readerInt("COD_TIPO_LIVELLO_PIANO"), "-1")
                            par.caricaComboTelerik(query, DropDownListPiano, "COD", "DESCRIZIONE", True, , , True)
                            If par.IfEmpty(unita, 0) > 0 Then
                                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE id = " & unita & " ORDER BY INTERNO ASC"
                            ElseIf cmbScala.SelectedValue <> "-1" And DropDownListPiano.SelectedValue <> "-1" Then
                                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & cmbEdificio.SelectedValue & " AND ID_SCALA = " & cmbScala.SelectedValue & " AND COD_TIPO_LIVELLO_PIANO='" & DropDownListPiano.SelectedValue & "' ORDER BY INTERNO ASC"
                            Else
                                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & cmbEdificio.SelectedValue & " ORDER BY INTERNO ASC"
                            End If
                            par.caricaComboTelerik(query, DropDownListInterno, "INTERNO", "INTERNO", True, , , True)
                            txtCodUI.Text = par.IfNull(readerInt("cod_unita_immobiliare"), "")
                        End If
                        readerInt.Close()

                        cmbStatoInterv.SelectedValue = par.IfNull(lettore("id_stato"), -1)
                        statoIntervento.Value = par.IfNull(lettore("id_stato"), -1)
                        'cmbGruppo.SelectedValue = par.IfNull(lettore("id_gruppo"), -1)
                        'gruppo.Value = cmbGruppo.SelectedValue
                        If par.IfEmpty(lettore("DATA_CONSEGNA_CHIAVI").ToString, "") <> "" Then
                            'txtDataConsegnaChiavi.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_CONSEGNA_CHIAVI"), ""))
                        End If

                        ' txtOperatConsegnaChiavi.Text = par.IfNull(lettore("OPERATORE_CONSEGNA_CHIAVI"), "")

                        If par.IfEmpty(lettore("DATA_RESTITUZIONE_CHIAVI").ToString, "") <> "" Then
                            ' txtDataConsegnaChiaviST.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_RESTITUZIONE_CHIAVI"), ""))
                        End If

                        txtAssegnatario.Text = par.IfNull(lettore("ASSEGNATARIO"), "")
                        txtAssegnatario2.Text = par.IfNull(lettore("ASSEGNATARIO_2"), "")

                        If par.IfEmpty(lettore("DATA_PROGRAMMAZIONE").ToString, "") <> "" Then
                            txtDataProgrammaz.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_PROGRAMMAZIONE"), ""))
                        End If

                        If par.IfEmpty(lettore("DATA_APERTURA").ToString, "") <> "" Then
                            txtDataApertura.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_APERTURA"), ""))
                        End If

                        If par.IfEmpty(lettore("ORA_INIZIO_INTERVENTO").ToString, "") <> "" Then
                            txtOraInterv.Text = Mid(par.IfNull(Trim(lettore("ORA_INIZIO_INTERVENTO")), ""), 1, 2) & ":" & Mid(par.IfNull(Trim(lettore("ORA_INIZIO_INTERVENTO")), ""), 3, 2)
                        End If
                        If par.IfEmpty(lettore("ORA_FINE_INTERVENTO").ToString, "") <> "" Then
                            txtOraFineInterv.Text = Mid(par.IfNull(Trim(lettore("ORA_FINE_INTERVENTO")), ""), 1, 2) & ":" & Mid(par.IfNull(Trim(lettore("ORA_FINE_INTERVENTO")), ""), 3, 2)
                        End If

                        If par.IfEmpty(lettore("DATA_PRE_ASSEGNATO").ToString, "") <> "" Then
                            txtDataPreAss.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_PRE_ASSEGNATO"), ""))
                        End If

                        If par.IfEmpty(lettore("DATA_PRESA_IN_CARICO").ToString, "") <> "" Then
                            txtDataPresaInCarico.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_PRESA_IN_CARICO"), ""))
                        End If

                        If par.IfEmpty(lettore("DATA_MESSA_IN_SICUREZZA").ToString, "") <> "" Then
                            txtDataMessaInSicurezza.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_MESSA_IN_SICUREZZA"), ""))
                        End If

                        If par.IfEmpty(lettore("DATA_CHIUSURA").ToString, "") <> "" Then
                            txtDataChiusura.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_CHIUSURA"), ""))
                        End If


                        cmbRichiedente.SelectedValue = par.IfNull(lettore("id_richiedente"), -1)
                        cmbVerifica.SelectedValue = par.IfNull(lettore("ID_MOD_VERIFICA"), -1)
                        cmbNuovoStatoUI.SelectedValue = par.IfNull(lettore("ID_NEW_STATO_UI"), -1)
                        cmbNewStatoRU.SelectedValue = par.IfNull(lettore("ID_NEW_STATO_CONTR_NUCLEO"), -1)
                        cmbServiziSociali.SelectedValue = par.IfNull(lettore("ID_TIPO_SERV_SOCIALE"), -1)
                        cmbIngressoAlloggio.SelectedValue = par.IfNull(lettore("INGRESSO_ALLOGGIO"), -1)
                        cmbStatoAll.SelectedValue = par.IfNull(lettore("ID_STATO_ALLOGGIO_ARRIVO"), -1)
                        cmbMotivoRecupero.SelectedValue = par.IfNull(lettore("ID_MOTIVO_RECUPERO"), -1)
                        cmbTipo.SelectedValue = par.IfNull(lettore("ID_TIPO_INTERVENTO"), -1)

                        If par.IfNull(lettore("FL_PROTOCOLLO_ATTIVO"), "0") = "1" Then
                            chkAttivoProtocollo.Checked = True
                        Else
                            chkAttivoProtocollo.Checked = False
                        End If

                        If par.IfNull(lettore("FL_ABBANDONO_ALLOGGIO"), "0") = "1" Then
                            chkAbbandonoAlloggio.Checked = True
                        Else
                            chkAbbandonoAlloggio.Checked = False
                        End If
                        txtDescrizIntervento.Text = par.IfNull(lettore("DESCRIZIONE_INTERV"), "")
                    End If
                End If
                lettore.Close()

                par.cmd.CommandText = "select * from siscom_mi.segnalazioni where id=" & idSegnalazione.Value
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    txtDescrizioneSegn.Text = par.IfNull(lettore("DESCRIZIONE_RIC"), "")
                End If
                lettore.Close()

                'For Each Items As ListItem In chkListEnti.Items
                '    par.cmd.CommandText = "select * from siscom_mi.INTERVENTI_ENTI_COINVOLTI where id_tipo_ente=" & Items.Value & " and id_intervento=" & idIntervento.Value
                '    lettore = par.cmd.ExecuteReader
                '    If lettore.Read Then
                '        Items.Selected = True
                '    Else
                '        Items.Selected = False
                '    End If
                '    lettore.Close()
                'Next

                For Each Items As ListItem In cmbTipoMessaInSicurezza.Items
                    par.cmd.CommandText = "select * from siscom_mi.INTERVENTI_TIPO_IN_SICUREZZA where ID_TIPO_MESSO_IN_SICUREZZA=" & Items.Value & " and id_intervento=" & idIntervento.Value
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        Items.Selected = True
                    Else
                        Items.Selected = False
                    End If
                    lettore.Close()
                Next

                For Each Items As ListItem In cmbTipoIntGas.Items
                    par.cmd.CommandText = "select * from SISCOM_MI.INTERVENTI_TIPO_ENTE_GAS where ID_TIPO_INTERVENTO_ENTE=" & Items.Value & " and id_intervento=" & idIntervento.Value
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        Items.Selected = True
                    Else
                        Items.Selected = False
                    End If
                    lettore.Close()
                Next

                For Each Items As ListItem In cmbTipoIntLuce.Items
                    par.cmd.CommandText = "select * from SISCOM_MI.INTERVENTI_TIPO_ENTE_LUCE where ID_TIPO_INTERVENTO_ENTE=" & Items.Value & " and id_intervento=" & idIntervento.Value
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        Items.Selected = True
                    Else
                        Items.Selected = False
                    End If
                    lettore.Close()
                Next

                For Each Items As ListItem In chkTipiServizio.Items
                    par.cmd.CommandText = "select * from SISCOM_MI.INTERVENTI_TIPO_SERVIZIO where ID_TIPO_INTERVENTO_SERVIZIO=" & Items.Value & " and id_intervento=" & idIntervento.Value
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        Items.Selected = True
                    Else
                        Items.Selected = False
                    End If
                    lettore.Close()
                Next

                For Each Items As ListItem In chkElencoServizi.Items
                    par.cmd.CommandText = "select * from SISCOM_MI.INTERVENTI_VOCI_SERVIZIO where ID_SERVIZIO=" & Items.Value & " and id_intervento=" & idIntervento.Value
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        Items.Selected = True
                    Else
                        Items.Selected = False
                    End If
                    lettore.Close()
                Next

                lblTitolo.Text = " numero " & idIntervento.Value
                lblTitolo2.Text = cmbTipo.SelectedItem.Text

                CaricaTabellaNote(idIntervento.Value)
                'CaricaElencoAllegati()
                connData.chiudi()
            Else
                connData.chiudi()
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)

                'par.modalDialogMessage("Caricamento dati segnalazione", "Non è possibile caricare correttamente i dati", Me.Page, "info", "Home.aspx", )
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Non è possibile caricare correttamente i dati", 300, 150, "Attenzione", Nothing, Nothing)

            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - CaricaInterventoDaSegn - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaInterventiTrovati(ByVal idInt As Integer, ByVal codFisc As String)

        Try
            Dim connAperta As Boolean = True
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connAperta = False
                connData.apri(False)
            End If
            par.cmd.CommandText = "select * from SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA where id_intervento=" & idInt & " and cod_fiscale='" & codFisc & "'"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows
                    par.cmd.CommandText = "select INTERVENTI_SICUREZZA.id,'' as num_int,ANAGRAFICA_SOGG_COINVOLTI.COGNOME_SOGG_COINVOLTO,ANAGRAFICA_SOGG_COINVOLTI.SESSO_SOGG_COINVOLTO,ANAGRAFICA_SOGG_COINVOLTI.NOME_SOGG_COINVOLTO,TIPO_INTERVENTO.DESCRIZIONE AS TIPO,TO_CHAR(TO_DATE(SUBSTR(data_ora_inserimento,0,8),'YYYYMMDD'),'DD/MM/YYYY') as DATA_APERTURA from siscom_mi.interventi_sicurezza,SISCOM_MI.TIPO_INTERVENTO,SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA,SISCOM_mi.ANAGRAFICA_SOGG_COINVOLTI " _
                        & " where interventi_sicurezza.id=" & idInt _
                         & " AND TIPO_INTERVENTO.ID(+)=INTERVENTI_SICUREZZA.ID_TIPO_INTERVENTO AND ANAGRAFICA_SOGG_COINVOLTI.COD_FISC_SOGG_COINVOLTO='" & codFisc & "'" _
                     & " AND ELENCO_SOGG_COINV_SICUREZZA.ID_INTERVENTO=INTERVENTI_SICUREZZA.ID AND ELENCO_SOGG_COINV_SICUREZZA.ID_ANAGR_SOGG_COINV=ANAGRAFICA_SOGG_COINVOLTI.ID "
                    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt2 As New Data.DataTable
                    If Not IsNothing(Session.Item("DataGridIntervEsist")) Then
                        dt2 = Session.Item("DataGridIntervEsist")
                    End If
                    da2.Fill(dt2)
                    da2.Dispose()
                    If dt2.Rows.Count > 0 Then
                        For Each rDett As Data.DataRow In dt2.Rows
                            Dim apertura As String = "window.open('NuovoIntervento.aspx?NM=1&IDI=" & dt2.Rows.Item(0).Item("ID") & "', 'nint' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
                            rDett.Item("num_int") = "<a href=""#"" onclick=""javascript:" & apertura & """>" & rDett.Item("ID").ToString & "</a>"
                        Next
                        Session.Item("DataGridIntervEsist") = dt2
                        RadGridInterventiAperti.CurrentPageIndex = 0
                        RadGridInterventiAperti.Rebind()
                        lblIntervento.Visible = False
                        PanelElencoInterventi.Visible = True
                    Else
                        PanelElencoInterventi.Visible = False
                        lblIntervento.Visible = True
                    End If
                Next
            End If
            If connAperta = False Then
                connData.chiudi(False)
            End If

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Segnalazione - CaricaInterventiTrovati - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub InsertIntestatariSegnalazione(ByVal cFiscale As String, ByVal idAnag As Integer)

        Dim idContratto As String = "NULL"
        Dim connAperta As Boolean = True
        'If par.OracleConn.State = Data.ConnectionState.Closed Then
        '    connAperta = False
        '    connData.apri(True)
        'End If

        'If cFiscale <> "" Then
        par.cmd.CommandText = "select rapporti_utenza.id as idcontr from siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali,siscom_mi.rapporti_utenza where rapporti_utenza.id=soggetti_contrattuali.id_contratto and " _
            & " anagrafica.id=soggetti_contrattuali.id_anagrafica and cod_fiscale='" & cFiscale.ToUpper & "' and nvl(data_riconsegna,'29991231')>'" & Format(Now, "yyyyMMdd") & "'"
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            idContratto = par.IfNull(lettore("IDCONTR"), "")
        End If
        lettore.Close()
        par.cmd.CommandText = "SELECT * from SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA where COD_FISCALE='" & cFiscale.ToUpper & "'"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        da.Dispose()
        If dt.Rows.Count > 0 Then
            For Each row As Data.DataRow In dt.Rows
                If row.Item("ID_INTERVENTO") <> idIntervento.Value Then
                    CaricaInterventiTrovati(row.Item("ID_INTERVENTO"), row.Item("COD_FISCALE"))
                End If
            Next
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA (" _
            & "   ID,ID_ANAGR_SOGG_COINV, COD_FISCALE, ID_CONTRATTO, ID_INTERVENTO)" _
            & " VALUES (SISCOM_MI.SEQ_ELENCO_SOGG_COINV_SIC.NEXTVAL, " & idAnag & ",'" & cFiscale.ToUpper & "'," & idContratto & "," & idIntervento.Value & ")"
            par.cmd.ExecuteNonQuery()

        Else
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA (" _
            & "   ID,ID_ANAGR_SOGG_COINV, COD_FISCALE, ID_CONTRATTO, ID_INTERVENTO)" _
            & " VALUES (SISCOM_MI.SEQ_ELENCO_SOGG_COINV_SIC.NEXTVAL, " & idAnag & ",'" & cFiscale.ToUpper & "'," & idContratto & "," & idIntervento.Value & ")"
            par.cmd.ExecuteNonQuery()

        End If

        'End If
        'If connAperta = False Then
        '    connData.chiudi(True)
        'End If
    End Sub

    'Private Sub CaricaSedeTerritoriale()
    '    Try
    '        connData.apri(False)
    '        Dim query As String = "SELECT ID, NOME FROM SISCOM_MI.TAB_FILIALI where ID_INDIRIZZO IS NOT NULL ORDER BY NOME ASC"
    '        par.caricaComboTelerik(query, cmbSedeConsegnaChiavi, "ID", "NOME", True)
    '        connData.chiudi(False)
    '        If cmbSedeConsegnaChiavi.Items.Count = 2 Then
    '            If Not IsNothing(cmbSedeConsegnaChiavi.SelectedValue = "-1") Then
    '                cmbSedeConsegnaChiavi.Items.Remove(cmbSedeConsegnaChiavi.SelectedValue = "-1")
    '            End If
    '        End If

    '    Catch ex As Exception
    '        connData.chiudi(False)
    '        Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - CaricaSedeTerritoriale - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub





    Private Sub CaricaInterni(Optional ByVal intestatario As Boolean = False)
        Try
            Dim query As String = ""

            If cmbScala.SelectedValue <> "-1" And cmbScala.SelectedValue <> "" And DropDownListPiano.SelectedValue <> "-1" And DropDownListPiano.SelectedValue <> "" Then
                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & cmbEdificio.SelectedValue & " AND ID_SCALA = " & cmbScala.SelectedValue & " AND COD_TIPO_LIVELLO_PIANO='" & DropDownListPiano.SelectedValue & "' ORDER BY INTERNO ASC"
            Else
                query = "SELECT DISTINCT INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & cmbEdificio.SelectedValue & " ORDER BY INTERNO ASC"
            End If
            par.caricaComboTelerik(query, DropDownListInterno, "INTERNO", "INTERNO", True)
            If DropDownListInterno.Items.Count = 2 Then
                If Not IsNothing(DropDownListInterno.SelectedValue = "-1") Then
                    DropDownListInterno.Items.Remove(DropDownListInterno.SelectedValue = "-1")
                End If
            End If
            'DropDownListInterno.Enabled = True
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - CaricaInterni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaScale(Optional ByVal intestatario As Boolean = False)
        Try
            Dim query As String = ""

            query = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & cmbEdificio.SelectedValue & ") ORDER BY DESCRIZIONE ASC"

            par.caricaComboTelerik(query, cmbScala, "ID", "DESCRIZIONE", True)
            If cmbScala.Items.Count = 2 Then
                If Not IsNothing(cmbScala.SelectedValue = "-1") Then
                    cmbScala.Items.Remove(cmbScala.SelectedValue = "-1")
                End If
            End If

            'cmbScala.Enabled = True
            'DropDownListPiano.Items.Clear()
            'DropDownListPiano.Enabled = False
            'DropDownListInterno.Items.Clear()
            'DropDownListInterno.Enabled = False
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - CaricaScale - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaPiano(Optional ByVal intestatario As Boolean = False)
        Try
            Dim query As String = ""


            query = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & cmbEdificio.SelectedValue & ") ORDER BY DESCRIZIONE ASC"


            par.caricaComboTelerik(query, DropDownListPiano, "COD", "DESCRIZIONE", True)

            'If DropDownListPiano.Items.Count = 2 Then
            '    If Not IsNothing(DropDownListPiano.Items.FindByValue("-1")) Then
            '        DropDownListPiano.Items.Remove(DropDownListPiano.Items.FindByValue("-1"))
            '    End If
            'End If
            'DropDownListPiano.Enabled = True
            'DropDownListInterno.Items.Clear()
            'DropDownListInterno.Enabled = False
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - CaricaPiano - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        CaricaPiano()
        CaricaScale()
        CaricaInterni()
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click

        If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "chiudi", "validNavigation=true;self.close();", True)
        Else
            Response.Redirect("Home.aspx", False)
        End If
    End Sub

    Protected Sub txtCodUI_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodUI.TextChanged
        Try
            connData.apri(False)

            par.cmd.CommandText = "select id_edificio,id_Scala,cod_tipo_livello_piano,interno from siscom_mi.unita_immobiliari where cod_unita_immobiliare='" & txtCodUI.Text.ToUpper & "'"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                cmbEdificio.SelectedValue = par.IfNull(lettore("id_edificio"), -1)


                CaricaPiano()
                CaricaScale()
                CaricaInterni()


                cmbScala.SelectedValue = par.IfNull(lettore("id_scala"), -1)
                DropDownListPiano.SelectedValue = par.IfNull(lettore("cod_tipo_livello_piano"), -1)
                DropDownListInterno.SelectedValue = par.IfNull(lettore("interno"), -1)
            End If
            lettore.Close()

            connData.chiudi(False)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - txtCodUI_TextChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub SalvaIntervento()
        Try
            Dim STATOS As String = "0"
            Dim ORIGINE As String = "A"
            Dim idInterv As String = "-1"

            connData.apri(True)
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_INTERVENTI_SICUREZZA.NEXTVAL FROM DUAL"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                idInterv = par.IfNull(lettore(0), "-1")
            End If
            lettore.Close()
            idIntervento.Value = idInterv

            Dim flagMessoInSicurezza As Integer = 0

            If chkSecurity.Checked = True Then
                flagMessoInSicurezza = 1
            Else
                flagMessoInSicurezza = 0
            End If

            Dim idUnita As String = "NULL"
            par.cmd.CommandText = "select id from siscom_mi.unita_immobiliari where cod_unita_immobiliare='" & txtCodUI.Text & "'"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                idUnita = par.IfNull(lettore(0), "")
            End If
            lettore.Close()

            Dim idContr As Long = 0
            par.cmd.CommandText = "select id_contratto from siscom_mi.segnalazione where id=" & idSegnalazione.Value
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                idContr = par.IfNull(lettore(0), 0)
            End If
            lettore.Close()

            par.cmd.CommandText = " INSERT INTO SISCOM_MI.INTERVENTI_SICUREZZA (" _
                & " ID, ID_STATO," _
                & " ID_SEGNALAZIONE, ID_UNITA,ID_EDIFICIO, FL_MESSO_IN_SICUREZZA," _
                & " ID_FASCICOLO, ID_PROCEDIMENTO, " _
                & "   ASSEGNATARIO, ASSEGNATARIO_2," _
                & "  DATA_PROGRAMMAZIONE," _
                & " DATA_APERTURA, DATA_PRESA_IN_CARICO, DATA_MESSA_IN_SICUREZZA," _
                & " DATA_CHIUSURA, ID_TIPO_INTERVENTO,ID_RICHIEDENTE,ID_MOD_VERIFICA,ID_NEW_STATO_UI,ID_NEW_STATO_CONTR_NUCLEO,ID_TIPO_SERV_SOCIALE,INGRESSO_ALLOGGIO,ID_STATO_ALLOGGIO_ARRIVO) " _
                & " VALUES ( " & idInterv & "," _
                & " " & par.insDbValue(cmbStatoInterv.SelectedValue, False, False, True) & "," _
                & "  NULL," _
                & "  " & idUnita & "," _
                & "  " & par.insDbValue(cmbEdificio.SelectedValue, False, False, True) & "," _
                & "  " & flagMessoInSicurezza & "," _
                & "  NULL," _
                & "  NULL," _
                & " " & par.insDbValue(txtAssegnatario.Text, True, False).ToUpper & "," _
                & " " & par.insDbValue(txtAssegnatario2.Text, True, False).ToUpper & "," _
                & "  " & par.insDbValue(txtDataProgrammaz.SelectedDate, True, True) & "," _
                & "  " & par.insDbValue(txtDataApertura.SelectedDate, True, True) & "," _
                & "  " & par.insDbValue(txtDataPresaInCarico.SelectedDate, True, True) & "," _
                & "  " & par.insDbValue(txtDataMessaInSicurezza.SelectedDate, True, True) & "," _
                & "  " & par.insDbValue(txtDataChiusura.SelectedDate, True, True) & "," _
                & "  " & par.insDbValue(cmbTipoMessaInSicurezza.SelectedValue, False, False, True) & "," _
                & "  " & par.insDbValue(cmbTipo.SelectedValue, False, False, True) & "," _
                & "  " & par.insDbValue(cmbRichiedente.SelectedValue, False, False, True) & "," _
                & "  " & par.insDbValue(cmbVerifica.SelectedValue, False, False, True) & "," _
                & "  " & par.insDbValue(cmbNuovoStatoUI.SelectedValue, False, False, True) & "," _
                & "  " & par.insDbValue(cmbStatoRU.SelectedValue, False, False, True) & "," _
                & "  " & par.insDbValue(cmbServiziSociali.SelectedValue, False, False, True) & "," _
                & "  " & par.insDbValue(cmbIngressoAlloggio.SelectedValue, False, False, True) & "," _
                & "  " & par.insDbValue(cmbStatoAll.SelectedValue, False, False, True) & "" _
                & ")"
            par.cmd.ExecuteNonQuery()

            If TextBoxNota.Text <> "" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_NOTE (ID_INTERVENTO, NOTE, DATA_ORA, ID_OPERATORE) " _
                                    & "VALUES (" & idInterv & ", '" & par.PulisciStrSql(TextBoxNota.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
                par.cmd.ExecuteNonQuery()

                TextBoxNota.Text = ""
                CaricaTabellaNote(idInterv)
            End If

            connData.chiudi(True)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - SalvaIntervento - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub ModificaIntervento()
        Try
            connData.apri(True)

            Dim idUnita As String = "NULL"
            par.cmd.CommandText = "select id from siscom_mi.unita_immobiliari where cod_unita_immobiliare='" & txtCodUI.Text & "'"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                idUnita = par.IfNull(lettore(0), "")
            End If
            lettore.Close()

            Dim flagMessoInSicurezza As Integer = 0

            If chkSecurity.Checked = True Then
                flagMessoInSicurezza = 1
            Else
                flagMessoInSicurezza = 0
            End If

            If txtAssegnatario.Text <> "" Then
                par.cmd.CommandText = "select * from operatori where operatore=" & par.insDbValue(txtAssegnatario.Text, True)
                Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore0.HasRows = False Then
                    txtAssegnatario.Text = CaricaOperatori(Trim(txtAssegnatario.Text))
                End If
                lettore0.Close()
            End If
            If txtAssegnatario2.Text <> "" Then
                par.cmd.CommandText = "select * from operatori where operatore=" & par.insDbValue(txtAssegnatario2.Text, True)
                Dim lettore0B As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore0B.HasRows = False Then
                    txtAssegnatario2.Text = CaricaOperatori(Trim(txtAssegnatario2.Text))
                End If
                lettore0B.Close()
            End If

            par.cmd.CommandText = "UPDATE SISCOM_MI.INTERVENTI_SICUREZZA" _
                    & " SET  " _
                    & " ID_STATO                   = " & par.insDbValue(cmbStatoInterv.SelectedValue, False, False, True) & "," _
                    & " ID_UNITA                   = " & idUnita & "," _
                    & " ID_EDIFICIO                = " & par.insDbValue(cmbEdificio.SelectedValue, False, False, True) & "," _
                    & " FL_MESSO_IN_SICUREZZA      = " & flagMessoInSicurezza & "," _
                    & " ASSEGNATARIO               = " & par.insDbValue(txtAssegnatario.Text, True, False).ToUpper & "," _
                    & " ASSEGNATARIO_2             = " & par.insDbValue(txtAssegnatario2.Text, True, False).ToUpper & "," _
                    & " DATA_PROGRAMMAZIONE        = " & par.insDbValue(par.IfEmpty(txtDataProgrammaz.SelectedDate, ""), True, True) & "," _
                    & " DATA_APERTURA              = " & par.insDbValue(par.IfEmpty(txtDataApertura.SelectedDate, ""), True, True) & "," _
                    & " ORA_INIZIO_INTERVENTO      = " & par.insDbValue(par.IfEmpty(Replace(txtOraInterv.Text, ":", ""), ""), True, False) & "," _
                    & " ORA_FINE_INTERVENTO        = " & par.insDbValue(par.IfEmpty(Replace(txtOraFineInterv.Text, ":", ""), ""), True, False) & "," _
                    & " DATA_PRE_ASSEGNATO         = " & par.insDbValue(par.IfEmpty(txtDataPreAss.SelectedDate, ""), True, True) & "," _
                    & " DATA_PRESA_IN_CARICO       = " & par.insDbValue(par.IfEmpty(txtDataPresaInCarico.SelectedDate, ""), True, True) & "," _
                    & " DATA_MESSA_IN_SICUREZZA    = " & par.insDbValue(par.IfEmpty(txtDataMessaInSicurezza.SelectedDate, ""), True, True) & "," _
                    & " DATA_CHIUSURA              = " & par.insDbValue(par.IfEmpty(txtDataChiusura.SelectedDate, ""), True, True) & "," _
                    & " ID_TIPO_INTERVENTO         = " & par.insDbValue(cmbTipo.SelectedValue, False, False, True) & "," _
                    & " FL_PROTOCOLLO_ATTIVO       = " & par.insDbValue(chkAttivoProtocollo.Checked.ToString, False, , , True, chkAttivoProtocollo.Checked) & "," _
                    & " FL_ABBANDONO_ALLOGGIO      = " & par.insDbValue(chkAbbandonoAlloggio.Checked.ToString, False, , , True, chkAbbandonoAlloggio.Checked) & "," _
                    & " ID_RICHIEDENTE       = " & par.insDbValue(cmbRichiedente.SelectedValue, False, False, True) & "," _
                    & " ID_MOD_VERIFICA = " & par.insDbValue(cmbVerifica.SelectedValue, False, False, True) & "," _
                    & " ID_NEW_STATO_UI = " & par.insDbValue(cmbNuovoStatoUI.SelectedValue, False, False, True) & "," _
                    & " ID_NEW_STATO_CONTR_NUCLEO =" & par.insDbValue(cmbNewStatoRU.SelectedValue, False, False, True) & "," _
                    & " ID_TIPO_SERV_SOCIALE =" & par.insDbValue(cmbServiziSociali.SelectedValue, False, False, True) & "," _
                    & " INGRESSO_ALLOGGIO =" & par.insDbValue(cmbIngressoAlloggio.SelectedValue, False, False, True) & "," _
                    & " ID_STATO_ALLOGGIO_ARRIVO =" & par.insDbValue(cmbStatoAll.SelectedValue, False, False, True) & "," _
                    & " DESCRIZIONE_INTERV =" & par.insDbValue(txtDescrizIntervento.Text, True, False).ToUpper & "," _
                    & " ID_MOTIVO_RECUPERO =" & par.insDbValue(cmbMotivoRecupero.SelectedValue, False, False, True) & "" _
                    & " WHERE  ID= " & idIntervento.Value
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "DELETE FROM SISCOM_MI.INTERVENTI_TIPO_ENTE_GAS WHERE ID_INTERVENTO=" & idIntervento.Value
            par.cmd.ExecuteNonQuery()
            For Each Items As ListItem In cmbTipoIntGas.Items
                If Items.Selected = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_TIPO_ENTE_GAS (ID_TIPO_INTERVENTO_ENTE,id_intervento) " _
                        & " values (" & Items.Value & "," & idIntervento.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.INTERVENTI_TIPO_ENTE_LUCE WHERE ID_INTERVENTO=" & idIntervento.Value
            par.cmd.ExecuteNonQuery()
            For Each Items As ListItem In cmbTipoIntLuce.Items
                If Items.Selected = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_TIPO_ENTE_LUCE (ID_TIPO_INTERVENTO_ENTE,id_intervento) " _
                        & " values (" & Items.Value & "," & idIntervento.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.INTERVENTI_TIPO_IN_SICUREZZA WHERE ID_INTERVENTO=" & idIntervento.Value
            par.cmd.ExecuteNonQuery()
            For Each Items As ListItem In cmbTipoMessaInSicurezza.Items
                If Items.Selected = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_TIPO_IN_SICUREZZA (ID_TIPO_MESSO_IN_SICUREZZA,id_intervento) " _
                        & " values (" & Items.Value & "," & idIntervento.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.INTERVENTI_TIPO_SERVIZIO WHERE ID_INTERVENTO=" & idIntervento.Value
            par.cmd.ExecuteNonQuery()
            For Each Items As ListItem In chkTipiServizio.Items
                If Items.Selected = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_TIPO_SERVIZIO (ID_TIPO_INTERVENTO_SERVIZIO,id_intervento) " _
                        & " values (" & Items.Value & "," & idIntervento.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.INTERVENTI_VOCI_SERVIZIO WHERE ID_INTERVENTO=" & idIntervento.Value
            par.cmd.ExecuteNonQuery()
            For Each Items As ListItem In chkElencoServizi.Items
                If Items.Selected = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_VOCI_SERVIZIO (ID_SERVIZIO,id_intervento) " _
                        & " values (" & Items.Value & "," & idIntervento.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next

            If TextBoxNota.Text <> "" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_NOTE (ID_INTERVENTO, NOTE, DATA_ORA, ID_OPERATORE) " _
                                    & "VALUES (" & idIntervento.Value & ", '" & par.PulisciStrSql(TextBoxNota.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
                par.cmd.ExecuteNonQuery()

                TextBoxNota.Text = ""
                CaricaTabellaNote(idIntervento.Value)
            End If

            If cmbStatoInterv.SelectedValue <> "-1" Then
                statoIntervento.Value = cmbStatoInterv.SelectedValue
                imgChiudiSegnalazione.Visible = True
            End If

            CreaFascicolo()

            '12/07/2016 AGGIORNO STATO SEGNALAZIONE
            If cmbStatoInterv.SelectedValue = 5 Then
                par.cmd.CommandText = "select * from siscom_mi.segnalazioni where id = (select id_segnalazione from siscom_mi.interventi_sicurezza where id=" & idIntervento.Value & ")" _
                    & " or id_segnalazione_padre in (select id_segnalazione from siscom_mi.interventi_sicurezza where id=" & idIntervento.Value & ")"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    For Each row As Data.DataRow In dt.Rows
                        'If par.IfNull(row.Item("id_segnalazione_padre"), 0) = 0 Then
                        par.cmd.CommandText = "select * from siscom_mi.interventi_sicurezza where id_segnalazione=" & row.Item("id") & " and id_stato<>5"
                        Dim lettoreS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettoreS.Read = False Then
                            par.cmd.CommandText = "update siscom_mi.segnalazioni set id_stato=3 where id=" & row.Item("id")
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "update siscom_mi.segnalazioni set id_stato=3 where id=" & par.IfNull(row.Item("id_segnalazione_padre"), 0)
                            par.cmd.ExecuteNonQuery()
                        End If
                        lettoreS.Close()
                        'End If
                    Next
                End If
            End If

            connData.chiudi(True)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - ModificaIntervento - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaGrid()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT ANAGRAFICA_SOGG_COINVOLTI.id,COD_FISC_SOGG_COINVOLTO,SESSO_SOGG_COINVOLTO, COD_LUOGO_NASCITA,(SELECT nome FROM comuni_nazioni where cod=COD_LUOGO_NASCITA) AS luogo_nasc, COD_TIPOLOGIA_OCCUPANTE, (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_OCCUPANTE_SOGG_COINV where id=cod_tipologia_occupante) AS occupante," _
                & " COGNOME_SOGG_COINVOLTO, TO_CHAR(TO_DATE(SUBSTR(DATA_NASC_SOGG_COINVOLTO,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_NASC_SOGG_COINVOLTO, " _
                & " INDIRIZZO_RESIDENZA, NOME_SOGG_COINVOLTO, PRESSO_COR," _
                & " TELEFONO, disabilita " _
                & " FROM SISCOM_MI.ANAGRAFICA_SOGG_COINVOLTI,SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA WHERE ANAGRAFICA_SOGG_COINVOLTI.id=ELENCO_SOGG_COINV_SICUREZZA.ID_ANAGR_SOGG_COINV and id_intervento=" & idIntervento.Value & " order by COGNOME_SOGG_COINVOLTO asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Session.Item("elencoSoggCoinvolti") = dt

            connData.chiudi()
            If IsPostBack Then
                RadGrid1.Rebind()
            End If

            If Not IsNothing(Session.Item("DataGridIntervEsist")) Then
                Session.Remove("DataGridIntervEsist")
            End If
            For Each row As Data.DataRow In dt.Rows
                par.cmd.CommandText = "SELECT * from SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA where COD_FISCALE='" & row.Item("COD_FISC_SOGG_COINVOLTO") & "'"
                Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt2 As New Data.DataTable
                da2.Fill(dt2)
                da2.Dispose()
                If dt2.Rows.Count > 0 Then
                    For Each row2 As Data.DataRow In dt2.Rows
                        If row2.Item("ID_INTERVENTO") <> idIntervento.Value Then
                            CaricaInterventiTrovati(row2.Item("ID_INTERVENTO"), row2.Item("COD_FISCALE"))
                        End If
                    Next
                End If
            Next


        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaGridEnteCoinv()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT ANAGRAFICA_ENTI_COINVOLTI.id,ID_TIPO_ENTE,(select descrizione from siscom_mi.TIPI_ENTI_COINVOLTI where TIPI_ENTI_COINVOLTI.id=ID_TIPO_ENTE) as DESCRIZIONE_ENTE," _
                & " COGNOME_ENTE_COINVOLTO, NOME_ENTE_COINVOLTO, " _
                & " RUOLO, EMAIL," _
                & " TELEFONO" _
                & " FROM SISCOM_MI.ANAGRAFICA_ENTI_COINVOLTI,SISCOM_MI.ELENCO_ENTI_COINVOLTI WHERE ANAGRAFICA_ENTI_COINVOLTI.id=ELENCO_ENTI_COINVOLTI.ID_ANAGR_ENTE_COINV and id_intervento=" & idIntervento.Value & " order by COGNOME_ENTE_COINVOLTO asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Session.Item("elencoEntiCoinvolti") = dt

            connData.chiudi()
            'RadGridEnteCoinv.Rebind()

            'For Each row As Data.DataRow In dt.Rows
            '    par.cmd.CommandText = "SELECT * from SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA where COD_FISCALE='" & row.Item("COD_FISC_SOGG_COINVOLTO") & "'"
            '    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            '    Dim dt2 As New Data.DataTable
            '    da2.Fill(dt2)
            '    da2.Dispose()
            '    If dt2.Rows.Count > 0 Then
            '        For Each row2 As Data.DataRow In dt2.Rows
            '            If row2.Item("ID_INTERVENTO") <> idIntervento.Value Then
            '                CaricaInterventiTrovati(row2.Item("ID_INTERVENTO"), row2.Item("COD_FISCALE"))
            '            End If
            '        Next
            '    End If
            'Next


        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim itemRadgrid As GridDataItem = TryCast(e.Item, GridDataItem)
            CType(e.Item.FindControl("btnVerificaSipo"), Button).OnClientClick = "document.getElementById('HFBeforeLoading').value='1';window.open('../Anagrafe.aspx?ID=-1&CF=" & e.Item.DataItem("COD_FISC_SOGG_COINVOLTO") & "&T=7','Anagrafe','top=0,left=0,width=600,height=400');"
            'itemRadgrid.Attributes.Add("onclick", "document.getElementById('hfCodFiscale').value='" & itemRadgrid.DataItem("COD_FISC_SOGG_COINVOLTO") & "';")
        End If

    End Sub

    Protected Sub RadGrid1_PreRender(sender As Object, e As System.EventArgs) Handles RadGrid1.PreRender
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "document.getElementById('HFBeforeLoading').value='1';", True)
    End Sub
    Protected Sub RadGrid1_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.UpdateCommand
        Try
            Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)
            connData.apri(True)

            Dim tipoOcc As String = ""
            If CType(userControl.FindControl("cmbTipoOccupante"), RadComboBox).SelectedValue <> "-1" Then
                tipoOcc = par.insDbValue(CType(userControl.FindControl("cmbTipoOccupante"), RadComboBox).SelectedValue, True, False)
            Else
                tipoOcc = "NULL"
            End If
            Dim codLuogoNasc As String = ""
            codLuogoNasc = Mid(CType(userControl.FindControl("txtCodFiscale"), TextBox).Text, 12, 4)


            par.cmd.CommandText = "UPDATE SISCOM_MI.ANAGRAFICA_SOGG_COINVOLTI " _
                    & "SET    COD_FISC_SOGG_COINVOLTO  =" & par.insDbValue(CType(userControl.FindControl("txtCodFiscale"), TextBox).Text, True).ToUpper & "," _
                         & "COD_LUOGO_NASCITA        = " & par.insDbValue(codLuogoNasc, True) & "," _
                         & "COD_TIPOLOGIA_OCCUPANTE  = " & tipoOcc & "," _
                        & "SESSO_SOGG_COINVOLTO=" & par.insDbValue(CType(userControl.FindControl("cmbSesso"), RadComboBox).SelectedItem.Text, True) & "," _
                        & "COGNOME_SOGG_COINVOLTO   = " & par.insDbValue(CType(userControl.FindControl("txtCognome"), TextBox).Text, True).ToUpper & "," _
                         & "DATA_NASC_SOGG_COINVOLTO = " & par.insDbValue(par.IfEmpty(CType(userControl.FindControl("txtDataNascSoggCoinv"), RadDatePicker).SelectedDate,""), True, True) & "," _
                         & "INDIRIZZO_RESIDENZA      = " & par.insDbValue(CType(userControl.FindControl("txtIndirizzoResidenza"), TextBox).Text, True).ToUpper & "," _
                         & "NOME_SOGG_COINVOLTO      = " & par.insDbValue(CType(userControl.FindControl("txtNome"), TextBox).Text, True).ToUpper & "," _
                         & "PRESSO_COR               = " & par.insDbValue(CType(userControl.FindControl("txtPressoCor"), TextBox).Text, True).ToUpper & "," _
                         & "TELEFONO                 = " & par.insDbValue(CType(userControl.FindControl("txtTelefono"), TextBox).Text, True) & "," _
                         & "DISABILITA                = " & par.insDbValue(CType(userControl.FindControl("txtDisabilita"), TextBox).Text, True) & "" _
                    & " WHERE  ID                    = " & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
            par.cmd.ExecuteNonQuery()
            connData.chiudi(True)
            'par.insDbValue(par.IfEmpty(txtDataChiusura.SelectedDate, ""), True, True)
            CaricaGrid()

            RadGrid1.EditIndexes.Clear()

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaInterventi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Protected Sub RadGrid1_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.DeleteCommand
        Try
            connData.apri(True)

            par.cmd.CommandText = "delete from SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA where ID_ANAGR_SOGG_COINV=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

            CaricaGrid()
            'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Eliminazione effettuata!", 300, 150, "Info", Nothing, Nothing)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - EsempioEditRadgrid - RadGrid1_DeleteCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Property idAnagrSoggCoinv() As Long
        Get
            If Not (ViewState("par_idAnagrSoggCoinv") Is Nothing) Then
                Return CLng(ViewState("par_idAnagrSoggCoinv"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idAnagrSoggCoinv") = value
        End Set

    End Property

    Public Property idAnagrEnteCoinv() As Long
        Get
            If Not (ViewState("par_idAnagrEnteCoinv") Is Nothing) Then
                Return CLng(ViewState("par_idAnagrEnteCoinv"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idAnagrEnteCoinv") = value
        End Set

    End Property

    Protected Sub RadGrid1_InsertCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.InsertCommand
        Try
            Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)
            connData.apri(True)

            Dim tipoOcc As String = ""
            If CType(userControl.FindControl("cmbTipoOccupante"), RadComboBox).SelectedValue <> "-1" Then
                tipoOcc = par.insDbValue(CType(userControl.FindControl("cmbTipoOccupante"), RadComboBox).SelectedValue, True, False)
            Else
                tipoOcc = "NULL"
            End If

            Dim codLuogoNasc As String = ""
            codLuogoNasc = Mid(CType(userControl.FindControl("txtCodFiscale"), TextBox).Text, 12, 4)


            par.cmd.CommandText = "select * from SISCOM_MI.ANAGRAFICA_SOGG_COINVOLTI where COD_FISC_SOGG_COINVOLTO='" & CType(userControl.FindControl("txtCodFiscale"), TextBox).Text & "'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idAnagrSoggCoinv = par.IfNull(myReader0("ID"), 0)
            End If
            myReader0.Close()

            If idAnagrSoggCoinv = 0 Then
                par.cmd.CommandText = "select siscom_mi.SEQ_ANAGRAFICA_SOGG_COINVOLTI.nextval from dual"
                Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderX.Read Then
                    idAnagrSoggCoinv = myReaderX(0)
                End If
                myReaderX.Close()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.ANAGRAFICA_SOGG_COINVOLTI (" _
                & "  ID, COD_FISC_SOGG_COINVOLTO, COD_LUOGO_NASCITA, COD_TIPOLOGIA_OCCUPANTE,SESSO_SOGG_COINVOLTO, " _
                & "   COGNOME_SOGG_COINVOLTO, DATA_NASC_SOGG_COINVOLTO, " _
                & "   INDIRIZZO_RESIDENZA, NOME_SOGG_COINVOLTO, PRESSO_COR, " _
                & "   TELEFONO,DISABILITA) " _
                & " VALUES ( " & idAnagrSoggCoinv & ", " & par.insDbValue(CType(userControl.FindControl("txtCodFiscale"), TextBox).Text, True).ToUpper & "," _
                & " " & par.insDbValue(codLuogoNasc.ToUpper, True) & "," _
                & " " & tipoOcc & "," _
                 & par.insDbValue(CType(userControl.FindControl("cmbSesso"), RadComboBox).SelectedItem.Text, True) & "," _
                & par.insDbValue(CType(userControl.FindControl("txtCognome"), TextBox).Text, True).ToUpper & "," _
                & par.insDbValue(par.IfEmpty(CType(userControl.FindControl("txtDataNascSoggCoinv"), RadDatePicker).SelectedDate, ""), True, True) & "," _
                & par.insDbValue(CType(userControl.FindControl("txtIndirizzoResidenza"), TextBox).Text, True).ToUpper & "," _
                & par.insDbValue(CType(userControl.FindControl("txtNome"), TextBox).Text, True).ToUpper & "," _
                & par.insDbValue(CType(userControl.FindControl("txtPressoCor"), TextBox).Text, True).ToUpper & "," _
                & par.insDbValue(CType(userControl.FindControl("txtTelefono"), TextBox).Text, True) & "," & par.insDbValue(CType(userControl.FindControl("txtDisabilita"), TextBox).Text, True) & ")"
                par.cmd.ExecuteNonQuery()

            End If

            InsertIntestatariSegnalazione(CType(userControl.FindControl("txtCodFiscale"), TextBox).Text, idAnagrSoggCoinv)

            connData.chiudi(True)
            CaricaGrid()
            idAnagrSoggCoinv = 0
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - EsempioEditRadgrid - RadGrid1_DeleteCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand

        Try
            If e.CommandName = RadGrid.InitInsertCommandName Then '"Add new" button clicked

                Dim editColumn As GridEditCommandColumn = CType(RadGrid1.MasterTableView.GetColumn("EditCommandColumn"), GridEditCommandColumn)
                editColumn.Visible = False

            ElseIf (e.CommandName = RadGrid.RebindGridCommandName AndAlso e.Item.OwnerTableView.IsItemInserted) Then
                e.Canceled = True
            Else
                Dim editColumn As GridEditCommandColumn = CType(RadGrid1.MasterTableView.GetColumn("EditCommandColumn"), GridEditCommandColumn)
                If Not editColumn.Visible Then
                    editColumn.Visible = True
                End If

            End If

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaInterventi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGrid1_ItemDeleted(sender As Object, e As Telerik.Web.UI.GridDeletedEventArgs) Handles RadGrid1.ItemDeleted
        Try
            If Not e.Exception Is Nothing Then
                e.ExceptionHandled = True
                'DisplayMessage(True, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " cannot be deleted. Reason: " + e.Exception.Message)
            Else
                'DisplayMessage(False, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " deleted")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaInterventi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGrid1_ItemInserted(sender As Object, e As Telerik.Web.UI.GridInsertedEventArgs) Handles RadGrid1.ItemInserted
        Try
            If Not e.Exception Is Nothing Then
                e.ExceptionHandled = True
                e.KeepInInsertMode = False
                'DisplayMessage(True, "Employee cannot be inserted. Reason: " + e.Exception.Message)
            Else
                'DisplayMessage(False, "Employee inserted")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaInterventi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGrid1_ItemUpdated(sender As Object, e As Telerik.Web.UI.GridUpdatedEventArgs) Handles RadGrid1.ItemUpdated
        Try
            If Not e.Exception Is Nothing Then
                e.KeepInEditMode = True
                e.ExceptionHandled = True
                'DisplayMessage(True, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " cannot be updated. Reason: " + e.Exception.Message)
            Else
                'DisplayMessage(False, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " updated")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaInterventi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("elencoSoggCoinvolti"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("elencoSoggCoinvolti"), Data.DataTable)
    End Sub

    Protected Sub OnItemDataBoundHandler(ByVal sender As Object, ByVal e As GridItemEventArgs)
        Try
            If (TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode) Then
                Dim radCombo1 As RadComboBox = CType(e.Item.FindControl("cmbTipoOccupante"), RadComboBox)
                Dim radCombo2 As RadComboBox = CType(e.Item.FindControl("cmbLuogoNascSoggCoinv"), RadComboBox)
                Dim radCombo3 As RadComboBox = CType(e.Item.FindControl("cmbSesso"), RadComboBox)
                Dim sesso As String = ""
                par.caricaComboTelerik("SELECT cod, nome FROM COMUNI_NAZIONI ORDER BY nome ASC", radCombo2, "COD", "NOME", True)
                If e.Item.DataItem.GetType.Name = "DataRowView" Then
                    radCombo2.SelectedValue = par.IfNull(e.Item.DataItem("COD_LUOGO_NASCITA"), -1)
                    Select Case par.IfNull(e.Item.DataItem("SESSO_SOGG_COINVOLTO"), -1)
                        Case "-1"
                        Case "M"
                            sesso = "0"
                        Case "F"
                            sesso = "1"
                    End Select
                    radCombo3.SelectedValue = sesso
                End If
                If e.Item.DataItem.GetType.Name = "GridInsertionObject" Then
                    radCombo2.ClearSelection()
                    radCombo3.ClearSelection()
                End If


                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPO_OCCUPANTE_SOGG_COINV ORDER BY DESCRIZIONE ASC", radCombo1, "id", "DESCRIZIONE", True)
                If e.Item.DataItem.GetType.Name = "DataRowView" Then
                    radCombo1.SelectedValue = par.IfNull(e.Item.DataItem("cod_tipologia_occupante"), -1)
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - OnItemDataBoundHandler - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub OnItemDataBoundHandler2(ByVal sender As Object, ByVal e As GridItemEventArgs)
        Try
            If (TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode) Then
                Dim radCombo1 As RadComboBox = CType(e.Item.FindControl("cmbTipoEnte"), RadComboBox)

                par.caricaComboTelerik("SELECT ID, descrizione FROM siscom_mi.TIPI_ENTI_COINVOLTI ORDER BY id ASC", radCombo1, "ID", "descrizione", True)
                If e.Item.DataItem.GetType.Name = "DataRowView" Then
                    radCombo1.SelectedValue = par.IfNull(e.Item.DataItem("ID_TIPO_ENTE"), -1)
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - OnItemDataBoundHandler2 - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGrid1_Clicking(sender As Object, e As EventArgs)
        Dim msgAnomalia0 As String = ""
        Dim msgAnomalia As String = ""
        Dim txtEditCF As RadComboBox = DirectCast(sender, RadComboBox)
        Dim edit As GridEditableItem = DirectCast(txtEditCF.NamingContainer, GridEditableItem)
        Dim drpEditLuogoNasc As RadComboBox = DirectCast(edit.FindControl("cmbLuogoNascSoggCoinv"), RadComboBox)
        Dim drpEditSesso As RadComboBox = DirectCast(edit.FindControl("cmbSesso"), RadComboBox)
        Dim drpEditDataNasc As RadDatePicker = DirectCast(edit.FindControl("txtDataNascSoggCoinv"), RadDatePicker)
        Dim drpEditCognome As TextBox = DirectCast(edit.FindControl("txtCognome"), TextBox)
        Dim drpEditNome As TextBox = DirectCast(edit.FindControl("txtNome"), TextBox)
        Try
            msgAnomalia0 = "Errore nel calcolo del cod.fiscale: "
            If drpEditCognome.Text = "" Then
                msgAnomalia &= "<br/>- Campo cognome non valorizzato"
            End If
            If drpEditNome.Text = "" Then
                msgAnomalia &= "<br/>- Campo nome non valorizzato"
            End If
            If drpEditLuogoNasc.SelectedValue = "-1" Then
                msgAnomalia &= "<br/>- Campo luogo di nascita non valorizzato"
            End If
            If IsNothing(drpEditDataNasc.SelectedDate) Then
                msgAnomalia &= "<br/>- Campo data di nascita non valorizzata"
            End If
            If drpEditSesso.SelectedValue = "-1" Then
                msgAnomalia &= "<br/>- Campo sesso non valorizzato"
            End If

            If msgAnomalia <> "" Then
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert(msgAnomalia0 & msgAnomalia, 340, 150, "Attenzione", Nothing, Nothing)
                Exit Sub
            End If

            connData.apri(False)

            par.cmd.CommandText = "select sigla from comuni_nazioni where cod='" & drpEditLuogoNasc.SelectedValue & "'"
            prNasc.Value = par.cmd.ExecuteScalar
            luogoNasc.Value = drpEditLuogoNasc.SelectedItem.Text
            dataNasc.Value = drpEditDataNasc.SelectedDate
            sesso.Value = drpEditSesso.SelectedValue

            connData.chiudi(False)

            ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav1", "CalcolaCodiceFiscale(document.getElementById('txtCognome').value.toUpperCase(),document.getElementById('dataNasc').value.substr(0,2),document.getElementById('dataNasc').value.substr(6,2),document.getElementById('dataNasc').value.substr(8,1),document.getElementById('dataNasc').value.substr(9,1),document.getElementById('dataNasc').value.substr(3,2),document.getElementById('luogoNasc').value.toUpperCase(),document.getElementById('sesso').value,document.getElementById('txtNome').value.toUpperCase(),document.getElementById('prNasc').value.toUpperCase());document.getElementById('txtCodFiscale').value = document.getElementById('CodiceFiscale').value;", True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - RadGrid1_Clicking - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Protected Sub RadGrid1_TextChanged(sender As Object, e As EventArgs)

        Dim txtEditCF As TextBox = DirectCast(sender, TextBox)

        Dim edit As GridEditableItem = DirectCast(txtEditCF.NamingContainer, GridEditableItem)
        Dim drpEditCfiscale As TextBox = DirectCast(edit.FindControl("txtCodFiscale"), TextBox)
        Dim drpEditCognome As TextBox = DirectCast(edit.FindControl("txtCognome"), TextBox)
        Dim drpEditNome As TextBox = DirectCast(edit.FindControl("txtNome"), TextBox)
        Dim drpEditDataNasc As RadDatePicker = DirectCast(edit.FindControl("txtDataNascSoggCoinv"), RadDatePicker)
        Dim drpEditLuogoNasc As RadComboBox = DirectCast(edit.FindControl("cmbLuogoNascSoggCoinv"), RadComboBox)
        Dim drpEditSesso As RadComboBox = DirectCast(edit.FindControl("cmbSesso"), RadComboBox)
        Dim dataNasc As String = ""
        Dim luogoNasc As String = ""
        Try
            If par.ControllaCF(UCase(drpEditCfiscale.Text)) = False Then
                drpEditCfiscale.Text = ""
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Se viene inserito un cognome/nome specificare un codice fiscale valido", 300, 150, "Attenzione", Nothing, Nothing)
            Else
                If par.ControllaCFNomeCognome(UCase(drpEditCfiscale.Text), drpEditCognome.Text, drpEditNome.Text) = True Then
                    dataNasc = CompletaDati(UCase(drpEditCfiscale.Text), luogoNasc)
                    drpEditDataNasc.SelectedDate = par.FormattaData(dataNasc)
                    drpEditLuogoNasc.SelectedValue = luogoNasc
                    drpEditSesso.SelectedItem.Text = par.RicavaSesso(drpEditCfiscale.Text)
                Else
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Se viene inserito un cognome/nome specificare un codice fiscale valido!", 300, 150, "Attenzione", Nothing, Nothing)

                    drpEditNome.Text = ""
                    drpEditCognome.Text = ""
                End If
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - txtCFSoggCoinv_TextChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function CompletaDati(ByVal CF As String, ByRef LuogoNasc As String) As String

        connData.apri(False)
        Dim MIADATA As String = ""
        par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD='" & Mid(CF, 12, 4) & "'"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read() Then

            If par.IfNull(myReader1("SIGLA"), " ") <> "E" And par.IfNull(myReader1("SIGLA"), " ") <> "C" Then
                'Me.cmbLuogoNascSoggCoinv.SelectedIndex = -1
                'Me.cmbLuogoNascSoggCoinv.SelectedValue = par.IfNull(myReader1("COD"), " ")
                LuogoNasc = par.IfNull(myReader1("COD"), "-1")
            End If


            'txtDataNascSoggCoinv.SelectedDate = ""
            If Val(Mid(CF, 10, 2)) > 40 Then
                MIADATA = Format(Val(Mid(CF, 10, 2)) - 40, "00")
            Else
                MIADATA = Mid(CF, 10, 2)
            End If

            Select Case Mid(CF, 9, 1)
                Case "A"
                    MIADATA = MIADATA & "/01"
                Case "B"
                    MIADATA = MIADATA & "/02"
                Case "C"
                    MIADATA = MIADATA & "/03"
                Case "D"
                    MIADATA = MIADATA & "/04"
                Case "E"
                    MIADATA = MIADATA & "/05"
                Case "H"
                    MIADATA = MIADATA & "/06"
                Case "L"
                    MIADATA = MIADATA & "/07"
                Case "M"
                    MIADATA = MIADATA & "/08"
                Case "P"
                    MIADATA = MIADATA & "/09"
                Case "R"
                    MIADATA = MIADATA & "/10"
                Case "S"
                    MIADATA = MIADATA & "/11"
                Case "T"
                    MIADATA = MIADATA & "/12"
            End Select
            If Mid(CF, 7, 1) = "0" Then
                MIADATA = MIADATA & "/200" & Mid(CF, 8, 1)
                If Format(CDate(MIADATA), "yyyyMMdd") > Format(Now, "yyyyMMdd") Then
                    MIADATA = Mid(MIADATA, 1, 6) & "19" & Mid(MIADATA, 9, 2)
                End If
            Else
                MIADATA = MIADATA & "/19" & Mid(CF, 7, 2)
            End If


        End If
        myReader1.Close()


        connData.chiudi(False)
        Return MIADATA


    End Function


    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        If CheckControl() = True Then
            If idIntervento.Value = "0" Then
                SalvaIntervento()
            Else
                ModificaIntervento()
            End If

            'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata!", 300, 150, "Info", Nothing, Nothing)
            VisualizzaAlert("Operazione effettuata!", 1)
            CType(Me.Master.FindControl("txtModificato"), HiddenField).Value = "0"
            lblTitolo.Text = "Intervento numero " & idIntervento.Value
        End If
    End Sub

    Private Sub CaricaTabellaNote(idInterv As String)
        Dim tabellaNote As String = ""
        tabellaNote &= "<table cellpadding='0' cellspacing='0' style='width:95%;'><tr style='font-family: ARIAL; font-size: 8pt; font-weight: bold'><td width='100px'>DATA-ORA</td><td width='150px'>OPERATORE</td><td>NOTE</td></tr>"
        par.cmd.CommandText = "SELECT interventi_note.*,operatori.operatore FROM sepa.operatori,siscom_mi.interventi_note where interventi_note.id_intervento=" & idInterv & " and interventi_note.id_operatore=operatori.id (+) order by data_ora desc"
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While lettore.Read
            If par.IfNull(lettore("NOTE"), "").ToString <> "" Then
                tabellaNote &= "<tr style='height: 16px;font-family: ARIAL; font-size: 8pt;'><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='100px'>" & par.FormattaData(Mid(par.IfNull(lettore("data_ora"), ""), 1, 8)) & " " & Mid(par.IfNull(lettore("data_ora"), ""), 9, 2) & ":" & Mid(par.IfNull(lettore("data_ora"), ""), 11, 2) & "</td><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='150px'>" & par.IfNull(lettore("operatore"), "") & "</td><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;'>" & Replace(par.IfNull(lettore("note"), ""), vbCrLf, "</br>") & "</td></tr>"
            End If
        End While
        lettore.Close()
        tabellaNote &= "</table>"
        If tabellaNote <> "" Then
            TabellaNoteComplete.Text = tabellaNote
        End If
    End Sub

    Private Sub caricaAllegati()
        sStrSqlAllegati = "select ID,DESCRIZIONE,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/SICUREZZA/'||NOME_FILE||''',''Allegati'','''');£>Visualizza</a>','$','&'),'£','" & Chr(34) & "') AS NOME_FILE," _
            & " TO_CHAR(TO_DATE(SUBSTR(allegati_interventi.DATA_ORA,1,8),'yyyymmdd'),'dd/mm/yyyy')||' '||SUBSTR(allegati_interventi.DATA_ORA,9,2)||':'||SUBSTR(allegati_interventi.DATA_ORA,11,2) AS DATA_ORA2 from siscom_mi.allegati_interventi where id_intervento = " & idIntervento.Value
        RadGridAllegati.Rebind()
    End Sub



    Private Function CheckControl() As Boolean
        CheckControl = True
        Try
            Dim msgAnomalia As String = ""
            If cmbEdificio.SelectedValue = "-1" Then
                CheckControl = False
                msgAnomalia &= "\n- Inserire l\'oggetto dell\'intervento"
                'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Inserire l\'oggetto dell\'intervento", 300, 150, "Attenzione", Nothing, Nothing)
            End If
            If cmbTipo.SelectedValue = "-1" Then
                CheckControl = False
                msgAnomalia &= "\n- Inserire la tipologia dell\'intervento"
                'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Inserire la tipologia dell'intervento", 300, 150, "Attenzione", Nothing, Nothing)
            End If
            If tipoIntervento.Value <> "1" Then
                If txtAssegnatario.Text = "" Then
                    CheckControl = False
                    msgAnomalia &= "\n- Inserire l\'assegnatario!"
                    'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Inserire l\'assegnatario!", 300, 150, "Attenzione", Nothing, Nothing)
                End If
            End If
            If txtCodUI.Text = "" Then
                CheckControl = False
                msgAnomalia &= "\n- Inserire l\'unità immobiliare!"
                'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("", 300, 150, "Attenzione", Nothing, Nothing)
            End If
            If statoIntervento.Value = "4" And tipoIntervento.Value <> "4" Then

                If IsNothing(txtDataMessaInSicurezza.SelectedDate) Then
                    CheckControl = False
                    msgAnomalia &= "<br />- Definire la data di messa in sicurezza"

                End If
                'If IsNothing(txtDataConsegnaChiavi.SelectedDate) Then
                '    CheckControl = False
                '    msgAnomalia &= "<br />- Definire la data di consegna chiavi a SEC"

                'End If
                'If IsNothing(txtDataConsegnaChiaviST.SelectedDate) Then
                '    CheckControl = False
                '    msgAnomalia &= "<br />- Definire la data di consegna chiavi a ST"

                'End If
                'If txtOperatConsegnaChiavi.Text = "" Then
                '    CheckControl = False

                '    msgAnomalia &= "<br />- Definire l\'operatore che consegna le chiavi"
                'End If

                'If cmbSedeConsegnaChiavi.SelectedValue = "-1" Then
                '    CheckControl = False
                '    msgAnomalia &= "<br /> Definire la sede in possesso chiavi!"
                'End If


            End If
            If msgAnomalia <> "" Then
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert(msgAnomalia, 340, 150, "Attenzione", Nothing, Nothing)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - CheckControl - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
        Return CheckControl
    End Function





    Private Function ControllaPresenzaSegnalazione() As Boolean
        Dim presenza As Boolean = False
        Try
            connData.apri(False)
            par.cmd.CommandText = "select * from siscom_mi.interventi_sicurezza where id=" & idIntervento.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                If par.IfNull(lettore("id_Segnalazione"), 0) = 0 Then
                    presenza = False
                Else
                    presenza = True
                End If
            End If
            lettore.Close()

            connData.chiudi(False)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - ControllaPresenzaSegnalazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

        Return presenza
    End Function

    Private Sub VisualizzaViewOperatori()
        MultiViewTitoli.ActiveViewIndex = 1
        MultiViewPulsanti.ActiveViewIndex = 1
        MultiViewCorpo.ActiveViewIndex = 1
    End Sub

    Private Sub VisualizzaViewPrincipale()
        MultiViewTitoli.ActiveViewIndex = 0
        MultiViewPulsanti.ActiveViewIndex = 0
        MultiViewCorpo.ActiveViewIndex = 0
    End Sub

    Private Sub VisualizzaPaginaProc()
        MultiViewTitoli.ActiveViewIndex = 2
        MultiViewPulsanti.ActiveViewIndex = 2
        MultiViewCorpo.ActiveViewIndex = 2
    End Sub

    Private Sub VisualizzaPaginaAllegati()
        MultiViewTitoli.ActiveViewIndex = 3
        MultiViewPulsanti.ActiveViewIndex = 3
        MultiViewCorpo.ActiveViewIndex = 3
    End Sub

    'Protected Sub btnCercaOp1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCercaOp1.Click
    '    Try
    '        tipoOperatore.Value = "1"
    '        If Trim(txtOperatConsegnaChiavi.Text) <> "" Then
    '            CaricaOperatori(Trim(txtOperatConsegnaChiavi.Text))
    '            VisualizzaViewOperatori()
    '        Else
    '            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Campo Obbligatorio!", 300, 150, "Attenzione", Nothing, Nothing)
    '        End If
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Intervento - btnCercaOp1_Click - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    Private Function CaricaOperatori(ByVal campoNome As String) As String
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        Try
            Dim stato As Boolean = False

            If Not par.OracleConn.State = Data.ConnectionState.Open Then
                stato = True
                connData.apri(False)
            End If
            Dim condizioneOperat As String = ""
            Dim cognome = campoNome.ToUpper.Replace("*", "%").Replace("'", "''")
            Dim nome = campoNome.ToUpper.Replace("*", "%").Replace("'", "''")
            If cognome <> "" Then
                condizioneOperat &= " AND (UPPER(OPERATORI.COGNOME) LIKE '" & cognome & "%' OR UPPER(OPERATORI.OPERATORE) LIKE '" & cognome & "%' "
            End If
            If nome <> "" Then
                condizioneOperat &= " or (UPPER(OPERATORI.NOME) LIKE '" & nome & "%')"
            End If
            If campoNome <> "" Then
                condizioneOperat &= " or (UPPER (OPERATORI.COGNOME) ||' '|| UPPER (OPERATORI.NOME)  LIKE '" & campoNome & "%'))"
            End If
            par.cmd.CommandText = "select OPERATORI.ID,operatori.operatore,operatori.cognome,operatori.nome,tab_filiali.nome as sede_territoriale " _
                & " from operatori,siscom_mi.tab_filiali where operatori.id_ufficio=tab_filiali.id " & condizioneOperat & " order by operatore"

            da.Fill(dt)
            da.Dispose()

            Session.Item("DataGridOperatori") = dt
            RadDataGridOperatori.Rebind()

            If stato = True Then
                connData.chiudi(False)

            End If

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - CaricaInCaricaOperatoritestatari - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

        If dt.Rows.Count > 0 Then

            Return dt.Rows(0).Item("operatore").ToString
        Else
            Return ""

        End If
    End Function

    Protected Sub RadDataGridOperatori_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadDataGridOperatori.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadDataGridOperatori.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text
                End If
            Next
            If tipoOperatore.Value = "1" Then
                dataItem.Attributes.Add("onclick", "document.getElementById('operatoreSelected').value='" & Replace(dataItem("OPERATORE").Text, "'", "\'") & "';")
                'dataItem.Attributes.Add("onDblclick", "document.getElementById('txtOperatConsegnaChiavi').text = document.getElementById('operatoreSelected').value;")
            End If
            If tipoOperatore.Value = "2" Then
                dataItem.Attributes.Add("onclick", "document.getElementById('operatoreSelected2').value='" & Replace(dataItem("OPERATORE").Text, "'", "\'") & "';")
                'dataItem.Attributes.Add("onDblclick", "document.getElementById('txtOperatPresaConsegnaChiavi').value = document.getElementById('operatoreSelected2').value;")
            End If
            If tipoOperatore.Value = "3" Then
                dataItem.Attributes.Add("onclick", "document.getElementById('operatoreSelected3').value='" & Replace(dataItem("OPERATORE").Text, "'", "\'") & "';")
                'dataItem.Attributes.Add("onDblclick", "document.getElementById('txtOperatPresaConsegnaChiavi').value = document.getElementById('operatoreSelected2').value;")
            End If
        End If
    End Sub

    Protected Sub RadDataGridOperatori_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadDataGridOperatori.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("DataGridOperatori"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("DataGridOperatori"), Data.DataTable)
    End Sub

    Protected Sub btnConfermaOp_Click(sender As Object, e As System.EventArgs) Handles btnConfermaOp.Click
        'If tipoOperatore.Value = "1" Then
        '    txtOperatConsegnaChiavi.Text = operatoreSelected.Value
        'End If
        If tipoOperatore.Value = "2" Then
            cmbStatoInterv.Enabled = True
            cmbStatoInterv.SelectedValue = "1"
            txtDataPresaInCarico.SelectedDate = Format(Now, "dd/MM/yyyy")
            imgChiudiSegnalazione.Visible = True
            txtAssegnatario.Text = operatoreSelected2.Value
        End If
        If tipoOperatore.Value = "3" Then
            txtAssegnatario2.Text = operatoreSelected3.Value
        End If
        tipoOperatore.Value = "0"
        operatoreSelected.Value = ""
        operatoreSelected2.Value = ""
        operatoreSelected3.Value = ""
    End Sub

    Protected Sub btnCercaOp2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCercaOp2.Click
        Try
            tipoOperatore.Value = "2"
            'If Trim(txtAssegnatario.Text) <> "" Then
            CaricaOperatori(Trim(txtAssegnatario.Text))
            VisualizzaViewOperatori()
            'Else
            'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Campo Obbligatorio!", 300, 150, "Attenzione", Nothing, Nothing)
            'End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Intervento - btnCercaOp1_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnIndietroOp_Click(sender As Object, e As System.EventArgs) Handles btnIndietroOp.Click
        VisualizzaViewPrincipale()
        RadGrid1.Rebind()
        RadGridEnteCoinv.Rebind()
        'tipoOperatore.Value = "0"
        'operatoreSelected.Value = ""
        'operatoreSelected2.Value = ""
    End Sub

    'Private Sub CaricaGruppiOperatori()
    '    Try
    '        connData.apri()

    '        Dim Str As String = ""
    '        If cmbGruppo.SelectedItem.Value <> "" Then
    '            Str = "select GRUPPI_OPERATORI_SICUREZZA.ID,id_gruppo,operatori.operatore,operatori.cognome,operatori.nome from operatori,SISCOM_MI.GRUPPI_OPERATORI_SICUREZZA where operatori.id=GRUPPI_OPERATORI_SICUREZZA.id_operatore and GRUPPI_OPERATORI_SICUREZZA.id_gruppo=" & cmbGruppo.SelectedItem.Value & " order by operatore"
    '        End If
    '        par.cmd.CommandText = Str
    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim dt As New Data.DataTable
    '        da.Fill(dt)

    '        da.Dispose()
    '        Session.Item("ElencoOperatori") = dt

    '        connData.chiudi()
    '        RadGridOperatori.Rebind()
    '        If dt.Rows.Count > 0 Then

    '        End If


    '        connData.chiudi()

    '    Catch ex As Exception
    '        connData.chiudi()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '    End Try
    'End Sub

    'Protected Sub cmbGruppo_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbGruppo.SelectedIndexChanged
    '    If cmbGruppo.SelectedValue <> "-1" Then
    '        cmbStatoInterv.Enabled = True
    '        cmbStatoInterv.SelectedValue = "1"
    '        txtDataPresaInCarico.SelectedDate = Format(Now, "dd/MM/yyyy")
    '        imgChiudiSegnalazione.Visible = True
    '    Else
    '        cmbStatoInterv.SelectedValue = "-1"
    '        cmbStatoInterv.Enabled = False
    '    End If
    'End Sub

    Protected Sub imgChiudiSegnalazione_Click(sender As Object, e As System.EventArgs) Handles imgChiudiSegnalazione.Click
        cmbStatoInterv.SelectedValue = "5"
        statoIntervento.Value = "5"
        btnSalva_Click(sender, e)

    End Sub

    Protected Sub RadGridInterventiAperti_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridInterventiAperti.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("DataGridIntervEsist"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("DataGridIntervEsist"), Data.DataTable)
    End Sub

    Protected Sub btnSalvaTipoProc_Click(sender As Object, e As System.EventArgs) Handles btnSalvaTipoProc.Click
        If cmbTipoProc.SelectedValue <> "-1" Then
            InserisciProcedimento()

            Dim apertura As String = "window.open('NuovoProcedimento.aspx?NM=1&IDP=" & idProcedim.Value & "&TIPO=" & cmbTipoProc.SelectedItem.Text & "', 'nint' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apri", apertura, True)
        Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessuna tipologia selezionata!", 300, 150, "Attenzione", Nothing, Nothing)
        End If
    End Sub

    Private Sub InserisciProcedimento()
        Try
            Dim idProc As String = "-1"

            connData.apri(True)
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PROCEDIMENTI_SICUREZZA.NEXTVAL FROM DUAL"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                idProc = par.IfNull(lettore(0), "-1")
            End If
            lettore.Close()
            idProcedim.Value = idProc

            tipoProc.Value = cmbTipoProc.SelectedItem.Text

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PROCEDIMENTI_SICUREZZA (" _
                 & " ID, TIPO, " _
                  & " ID_SEGNALAZIONE, ID_INTERVENTO, ID_FASCICOLO, DATA_ORA_INSERIMENTO) " _
                & " VALUES (" & idProc & ",'" & cmbTipoProc.SelectedItem.Text & "'," _
                & "" & idSegnalazione.Value & "," _
                & "" & idIntervento.Value & "," _
                & "" & idFascicolo.Value & "," _
                & "'" & Format(Now, "yyyyMMddHHmm") & "')"
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

            CaricaProcedimenti()

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - InserisciProcedimento - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaProcedimenti()
        Try
            connData.apri()
            par.cmd.CommandText = " SELECT distinct PROCEDIMENTI_SICUREZZA.ID," _
               & " PROCEDIMENTI_SICUREZZA.ID AS NUM,(CASE WHEN STATO=1 THEN 'In Corso' WHEN STATO=2 THEN 'Chiuso' END) as STATO, TIPO, " _
               & " REFERENTE, " _
               & " TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_INSERIMENTO,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_ORA_INSERIMENTO " _
               & " FROM SISCOM_MI.PROCEDIMENTI_SICUREZZA " _
               & " WHERE " _
               & " PROCEDIMENTI_SICUREZZA.id_intervento =" & idIntervento.Value _
               & " ORDER BY DATA_ORA_INSERIMENTO DESC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Session.Item("ElencoProcedimenti") = dt
            CreaFascicolo()
            connData.chiudi()
            RadGridProc.Rebind()

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - NuovoIntervento - CaricaProcedimenti - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnElencoProc_Click(sender As Object, e As System.EventArgs) Handles btnElencoProc.Click
        VisualizzaPaginaProc()
    End Sub

    Protected Sub RadGridProc_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridProc.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridProc.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text.ToString.Replace("&nbsp;", "")
                End If
            Next
            dataItem.Attributes.Add("onclick", "document.getElementById('HiddenFieldProc').value='" & dataItem("ID").Text & "';document.getElementById('tipoProc').value='" & dataItem("TIPO").Text & "';" _
                                             & "document.getElementById('txtProcSelected').value='Hai selezionato il procedimento numero " & dataItem("ID").Text & "';")
            dataItem.Attributes.Add("onDblclick", "apriProcedimento();")
        End If
    End Sub

    Protected Sub RadGridProc_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridProc.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("ElencoProcedimenti"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("ElencoProcedimenti"), Data.DataTable)
    End Sub

    Protected Sub btnVisualizzaProc_Click(sender As Object, e As System.EventArgs) Handles btnVisualizzaProc.Click
        If HiddenFieldProc.Value <> "-1" AndAlso HiddenFieldProc.Value <> "0" Then
            Dim apertura As String = "window.open('NuovoProcedimento.aspx?NM=1&IDP=" & HiddenFieldProc.Value & "', 'nint' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apri", apertura, True)
        Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessun procedimento selezionato!", 300, 150, "Attenzione", Nothing, Nothing)
        End If
    End Sub


    Public Property sStrSqlAllegati() As String
        Get
            If Not (ViewState("par_sStrSqlAllegati") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlAllegati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlAllegati") = value
        End Set
    End Property

    Protected Sub RadGridAllegati_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridAllegati.DeleteCommand
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)

            par.cmd.CommandText = "delete from SISCOM_MI.ALLEGATI_INTERVENTI where id=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)
            'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata!", 300, 150, "Info", Nothing, Nothing)
            VisualizzaAlert("Operazione effettuata!", 1)
            RadGridAllegati.Rebind()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: SICUREZZA - EliminaAllegato - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAllegati_InsertCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridAllegati.InsertCommand
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)
            Dim NomeFile As String = Format(idIntervento.Value, "0000000000") & "_" & Format(Now, "yyyyMMddHHmmss")
            connData.apri(True)

            Dim nFile As String = ""
            Dim nFileS As String = ""
            Dim oraAllegato As String = Format(Now, "yyyyMMddHHmmss")
            For Each file As UploadedFile In CType(userControl.FindControl("RadUploadAllegato"), RadAsyncUpload).UploadedFiles
                nFile = file.GetName()
                nFileS = NomeFile & Mid(nFile, Len(nFile) - 3, 4)
                file.SaveAs(Server.MapPath("..\ALLEGATI\SICUREZZA\") & NomeFile & Mid(nFile, Len(nFile) - 3, 4))
            Next

            Dim DescrizioneAllegato As String = ""
            DescrizioneAllegato = CType(userControl.FindControl("txtDescrizioneAllegato"), RadTextBox).Text

            Dim tipoDoc As String = ""
            Dim DescrtipoDoc As String = ""
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.ALLEGATI_INTERVENTI (ID,NOME_FILE,ID_INTERVENTO,DATA_ORA,DESCRIZIONE) VALUES " _
                                            & "(SISCOM_MI.SEQ_ALLEGATI_INTERVENTI.NEXTVAL,'" & par.PulisciStrSql(nFileS) & "'," & idIntervento.Value & "," _
                                                    & "'" & oraAllegato & "', '" & par.PulisciStrSql(DescrizioneAllegato.ToUpper) & "')"
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)
            'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata!", 300, 150, "Info", Nothing, Nothing)
            'VisualizzaAlert("Operazione effettuata!", 1)
            RadGridAllegati.Rebind()

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: SICUREZZA - Inserisci allegato - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAllegati_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridAllegati.ItemCommand
        Try
            If e.CommandName = RadGrid.InitInsertCommandName Then '"Add new" button clicked
                VisualizzaPaginaAllegati()
                'Dim editColumn As GridEditCommandColumn = CType(RadGridAllegati.MasterTableView.GetColumn("EditCommandColumn"), GridEditCommandColumn)
                'editColumn.Visible = False

            ElseIf (e.CommandName = RadGrid.RebindGridCommandName AndAlso e.Item.OwnerTableView.IsItemInserted) Then
                e.Canceled = True
            Else
                VisualizzaPaginaAllegati()
                'Dim editColumn As GridEditCommandColumn = CType(RadGridAllegati.MasterTableView.GetColumn("EditCommandColumn"), GridEditCommandColumn)
                'If Not editColumn.Visible Then
                '    editColumn.Visible = True
                'End If
            End If

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: SICUREZZA - Allegati - ItemCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAllegati_ItemDeleted(sender As Object, e As Telerik.Web.UI.GridDeletedEventArgs) Handles RadGridAllegati.ItemDeleted
        Try
            If Not e.Exception Is Nothing Then
                e.ExceptionHandled = True

            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: SICUREZZA - Allegati - ItemDeleted - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAllegati_ItemInserted(sender As Object, e As Telerik.Web.UI.GridInsertedEventArgs) Handles RadGridAllegati.ItemInserted
        Try
            If Not e.Exception Is Nothing Then
                e.ExceptionHandled = True
                e.KeepInInsertMode = False
                'DisplayMessage(True, "Employee cannot be inserted. Reason: " + e.Exception.Message)
            Else
                'DisplayMessage(False, "Employee inserted")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: SICUREZZA - Allegati - ItemInserted - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAllegati_ItemUpdated(sender As Object, e As Telerik.Web.UI.GridUpdatedEventArgs) Handles RadGridAllegati.ItemUpdated
        Try
            If Not e.Exception Is Nothing Then
                e.KeepInEditMode = True
                e.ExceptionHandled = True
                'DisplayMessage(True, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " cannot be updated. Reason: " + e.Exception.Message)
            Else
                'DisplayMessage(False, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " updated")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: SICUREZZA - Allegati - ItemUpdated - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAllegati_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridAllegati.NeedDataSource
        Try
            If sStrSqlAllegati <> "" Then
                RadGridAllegati.DataSource = par.getDataTableGrid(sStrSqlAllegati)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: SICUREZZA - Eventi - NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub



    'Protected Sub btnAllegaFile_Click(sender As Object, e As System.EventArgs) Handles btnAllegaFile.Click
    '    Dim nFile As String = ""
    '    Try
    '        If FileUpload1. = True Then
    '            If txtDescrizioneA.Text <> "" Then
    '                connData.apri(True)
    '                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ALLEGATI_INTERVENTI WHERE NOME_FILE = '" & par.PulisciStrSql(Me.txtDescrizioneA.Text.ToUpper) & "' AND ID_INTERVENTO = " & idIntervento.Value
    '                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

    '                Dim oraAllegato As String = Format(Now, "yyyyMMddHHmmss")

    '                If lettore.Read Then
    '                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Esiste già un allegato con lo stesso nome!\nImpossibile allegare il file scelto.", 300, 150, "Attenzione", Nothing, Nothing)
    '                    'par.modalDialogMessage("Inserimento allegato", "Esiste già un allegato con lo stesso nome!\nImpossibile allegare il file scelto.", Me.Page, "info", , )
    '                    VisualizzaPaginaAllegati()
    '                Else
    '                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.ALLEGATI_INTERVENTI (ID,NOME_FILE,ID_INTERVENTO,DATA_ORA,DESCRIZIONE) VALUES " _
    '                                        & "(SISCOM_MI.SEQ_ALLEGATI_INTERVENTI.NEXTVAL,'" & par.PulisciStrSql(Me.txtDescrizioneA.Text.ToUpper) & "'," & idIntervento.Value & "," _
    '                                        & "'" & oraAllegato & "', '" & par.PulisciStrSql(Me.txtDescrizioneAll.Text.ToUpper) & "')"
    '                    par.cmd.ExecuteNonQuery()
    '                End If
    '                lettore.Close()

    '                nFile = Server.MapPath("..\ALLEGATI\SICUREZZA\" & FileUpload1.FileName)
    '                FileUpload1.SaveAs(nFile)
    '                Dim objCrc32 As New Crc32()
    '                Dim strmZipOutputStream As ZipOutputStream
    '                Dim zipfic As String
    '                zipfic = Server.MapPath("..\ALLEGATI\SICUREZZA\" & idIntervento.Value & "_" & oraAllegato & "-" & UCase(txtDescrizioneA.Text) & ".zip")
    '                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
    '                strmZipOutputStream.SetLevel(6)
    '                Dim strFile As String
    '                strFile = nFile
    '                Dim strmFile As FileStream = File.OpenRead(strFile)
    '                Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '                strmFile.Read(abyBuffer, 0, abyBuffer.Length)
    '                Dim sFile As String = Path.GetFileName(strFile)
    '                Dim theEntry As ZipEntry = New ZipEntry(sFile)
    '                Dim fi As New FileInfo(strFile)
    '                theEntry.DateTime = fi.LastWriteTime
    '                theEntry.Size = strmFile.Length
    '                strmFile.Close()
    '                objCrc32.Reset()
    '                objCrc32.Update(abyBuffer)
    '                theEntry.Crc = objCrc32.Value
    '                strmZipOutputStream.PutNextEntry(theEntry)
    '                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
    '                File.Delete(strFile)

    '                If Not String.IsNullOrEmpty(Me.txtDescrizioneAll.Text) Then
    '                    Dim fileDescrizione As String = ""

    '                    fileDescrizione = Server.MapPath("..\ALLEGATI\SICUREZZA\") & UCase(txtDescrizioneA.Text) & oraAllegato & "_DESCRIZIONE.txt"
    '                    Dim sr As StreamWriter = New StreamWriter(fileDescrizione, False, System.Text.Encoding.Default)
    '                    sr.WriteLine("Data del documento:" & par.FormattaData(Format(Now, "yyyyMMdd")) & vbCrLf & "DESCRIZIONE:" & vbCrLf & txtDescrizioneAll.Text.ToUpper)
    '                    sr.Close()

    '                    strFile = Server.MapPath("..\ALLEGATI\SICUREZZA\") & UCase(txtDescrizioneA.Text) & oraAllegato & "_DESCRIZIONE.txt"
    '                    strmFile = File.OpenRead(strFile)
    '                    Dim abyBuffer12(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '                    strmFile.Read(abyBuffer12, 0, abyBuffer12.Length)
    '                    Dim sFile12 As String = Path.GetFileName(strFile)
    '                    theEntry = New ZipEntry(sFile12)
    '                    fi = New FileInfo(strFile)
    '                    theEntry.DateTime = fi.LastWriteTime
    '                    theEntry.Size = strmFile.Length
    '                    strmFile.Close()
    '                    objCrc32.Reset()
    '                    objCrc32.Update(abyBuffer12)
    '                    theEntry.Crc = objCrc32.Value
    '                    strmZipOutputStream.PutNextEntry(theEntry)
    '                    strmZipOutputStream.Write(abyBuffer12, 0, abyBuffer12.Length)
    '                    File.Delete(strFile)

    '                End If
    '                strmZipOutputStream.Finish()
    '                strmZipOutputStream.Close()
    '                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata!", 300, 150, "Info", Nothing, Nothing)
    '                'par.modalDialogMessage("Inserimento allegato", "Operazione completata correttamente.", Me.Page, "successo", , )

    '                connData.chiudi(True)
    '                CaricaElencoAllegati()
    '            Else
    '                'par.modalDialogMessage("Inserimento allegato", "Inserire il nome.", Me.Page, "info", , )
    '                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Inserire il nome file", 300, 150, "Attenzione", Nothing, Nothing)
    '                VisualizzaPaginaAllegati()
    '            End If
    '        Else
    '            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Errore! Nessun file selezionato.", 300, 150, "Attenzione", Nothing, Nothing)
    '            VisualizzaPaginaAllegati()
    '        End If
    '    Catch ex As Exception
    '        connData.chiudi(False)
    '        Session.Add("ERRORE", "Provenienza: Sicurezza - NuovoIntervento - btnAllegaFile_Click - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try

    'End Sub

    Protected Sub imgAllega_Click(sender As Object, e As System.EventArgs) Handles imgAllega.Click
        caricaAllegati()
        VisualizzaPaginaAllegati()
    End Sub

    Protected Sub btnIndietroAll_Click(sender As Object, e As System.EventArgs) Handles btnIndietroAll.Click
        VisualizzaViewPrincipale()
        RadGrid1.Rebind()
        RadGridEnteCoinv.Rebind()
    End Sub


    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 1


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function txtTelefono() As Object
        Throw New NotImplementedException
    End Function

    Protected Sub cmbTipo_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbTipo.SelectedIndexChanged
        tipoIntervento.Value = cmbTipo.SelectedValue
    End Sub

    Protected Sub cmbStatoInterv_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbStatoInterv.SelectedIndexChanged
        statoIntervento.Value = cmbStatoInterv.SelectedValue
        If statoIntervento.Value = "2" Then
            txtDataPresaInCarico.SelectedDate = Format(Now, "dd/MM/yyyy")
        End If
    End Sub

    Protected Sub btnCercaOp2B_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCercaOp2B.Click
        Try
            tipoOperatore.Value = "3"
            'If Trim(txtAssegnatario.Text) <> "" Then
            CaricaOperatori(Trim(txtAssegnatario2.Text))
            VisualizzaViewOperatori()
            'Else
            'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Campo Obbligatorio!", 300, 150, "Attenzione", Nothing, Nothing)
            'End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuova Intervento - btnCercaOp2B_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnCreaIntervServ_Click(sender As Object, e As System.EventArgs) Handles btnCreaIntervServ.Click
        InserisciInterventoServ()
    End Sub

    Private Sub InserisciInterventoServ()
        Try
            Dim idInterv As String = "-1"

            connData.apri(True)
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_INTERVENTI_SICUREZZA.NEXTVAL FROM DUAL"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                idInterv = par.IfNull(lettore(0), "-1")
            End If
            lettore.Close()

            par.cmd.CommandText = " INSERT INTO SISCOM_MI.INTERVENTI_SICUREZZA ( " _
                & " ID, ID_STATO, ID_TIPO_INTERVENTO, " _
                & " ASSEGNATARIO, ID_SEGNALAZIONE, ID_UNITA, " _
                & " ID_EDIFICIO, FL_MESSO_IN_SICUREZZA, ID_FASCICOLO, " _
                & " ID_PROCEDIMENTO, DATA_CONSEGNA_CHIAVI, OPERATORE_CONSEGNA_CHIAVI, " _
                & " DATA_RESTITUZIONE_CHIAVI, OPERATORE_PRESA_CHIAVI, ID_SEDE_TERRITORIALE, " _
                & " DATA_PROGRAMMAZIONE, DATA_APERTURA, DATA_PRESA_IN_CARICO, " _
                & " DATA_MESSA_IN_SICUREZZA, DATA_CHIUSURA, " _
                & " FL_PROTOCOLLO_ATTIVO, FL_ABBANDONO_ALLOGGIO, DATA_ORA_INSERIMENTO, " _
                & " DATA_PRE_ASSEGNATO, ASSEGNATARIO_2,ID_RICHIEDENTE,ID_MOD_VERIFICA,ID_NEW_STATO_UI,ID_NEW_STATO_CONTR_NUCLEO,ID_TIPO_SERV_SOCIALE,INGRESSO_ALLOGGIO,ID_STATO_ALLOGGIO_ARRIVO,ID_MOTIVO_RECUPERO)" _
                & " (SELECT " _
                & " " & idInterv & ", 1, 3, " _
                & " ASSEGNATARIO, ID_SEGNALAZIONE, ID_UNITA, " _
                & " ID_EDIFICIO, FL_MESSO_IN_SICUREZZA, ID_FASCICOLO, " _
                & " ID_PROCEDIMENTO, DATA_CONSEGNA_CHIAVI, OPERATORE_CONSEGNA_CHIAVI, " _
                & " DATA_RESTITUZIONE_CHIAVI, OPERATORE_PRESA_CHIAVI, ID_SEDE_TERRITORIALE, " _
                & " NULL, NULL, NULL, " _
                & " DATA_MESSA_IN_SICUREZZA, DATA_CHIUSURA, " _
                & " FL_PROTOCOLLO_ATTIVO, FL_ABBANDONO_ALLOGGIO, '" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & " '" & Format(Now, "yyyyMMdd") & "', ASSEGNATARIO_2,ID_RICHIEDENTE,ID_MOD_VERIFICA,ID_NEW_STATO_UI,ID_NEW_STATO_CONTR_NUCLEO,ID_TIPO_SERV_SOCIALE,INGRESSO_ALLOGGIO,ID_STATO_ALLOGGIO_ARRIVO,ID_MOTIVO_RECUPERO from SISCOM_MI.INTERVENTI_SICUREZZA WHERE ID=" & idIntervento.Value & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA (" _
                & " ID, COD_FISCALE, ID_CONTRATTO, " _
                & " ID_INTERVENTO, ID_ANAGR_SOGG_COINV) " _
                & " (SELECT " _
                & " SISCOM_MI.SEQ_ELENCO_SOGG_COINV_SIC.NEXTVAL, COD_FISCALE, ID_CONTRATTO, " _
                & " " & idInterv & ", ID_ANAGR_SOGG_COINV from SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA where id_intervento=" & idIntervento.Value & ")"
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_ENTI_COINVOLTI (" _
            & " ID_INTERVENTO, ID_TIPO_ENTE) " _
            & "  ( SELECT " & idInterv & "," _
            & "  ID_TIPO_ENTE from  SISCOM_MI.INTERVENTI_ENTI_COINVOLTI  where id_intervento=" & idIntervento.Value & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_TIPO_IN_SICUREZZA (" _
                & " ID_INTERVENTO, ID_TIPO_MESSO_IN_SICUREZZA) " _
                & "(select  " & idInterv & "," _
                & " ID_TIPO_MESSO_IN_SICUREZZA from SISCOM_MI.INTERVENTI_TIPO_IN_SICUREZZA  where id_intervento=" & idIntervento.Value & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,VALORE_OLD,VALORE_NEW) " _
                & "VALUES ( " & idSegnalazione.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'F258','" & par.PulisciStrSql(cmbTipo.SelectedItem.Text) & "','','')"
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

            Dim apertura As String = "window.open('NuovoIntervento.aspx?NM=1&IDI=" & idInterv & "', 'nintS' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apri", apertura, True)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Intervento - InserisciInterventoServ - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridEnteCoinv_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridEnteCoinv.DeleteCommand
        Try
            connData.apri(True)

            par.cmd.CommandText = "delete from SISCOM_MI.ELENCO_ENTI_COINVOLTI where ID_ANAGR_ENTE_COINV=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

            CaricaGridEnteCoinv()
            'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Eliminazione effettuata!", 300, 150, "Info", Nothing, Nothing)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - RadGridEnteCoinv_DeleteCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridEnteCoinv_InsertCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridEnteCoinv.InsertCommand
        Try
            Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)
            connData.apri(True)

            Dim tipoEnte As String = ""
            If CType(userControl.FindControl("cmbTipoEnte"), RadComboBox).SelectedValue <> "-1" Then
                tipoEnte = par.insDbValue(CType(userControl.FindControl("cmbTipoEnte"), RadComboBox).SelectedValue, True, False)
            Else
                tipoEnte = "NULL"
            End If

            par.cmd.CommandText = "select * from SISCOM_MI.ANAGRAFICA_ENTI_COINVOLTI where " _
                & "COGNOME_ENTE_COINVOLTO='" & CType(userControl.FindControl("txtCognome"), TextBox).Text & "'" _
                & " AND NOME_ENTE_COINVOLTO='" & CType(userControl.FindControl("txtNome"), TextBox).Text & "'" _
                & " AND ID_TIPO_ENTE=" & tipoEnte & ""
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idAnagrEnteCoinv = par.IfNull(myReader0("ID"), 0)
            End If
            myReader0.Close()

            If idAnagrEnteCoinv = 0 Then
                par.cmd.CommandText = "select siscom_mi.SEQ_ANAGRAFICA_ENTI_COINVOLTI.nextval from dual"
                Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderX.Read Then
                    idAnagrEnteCoinv = myReaderX(0)
                End If
                myReaderX.Close()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.ANAGRAFICA_ENTI_COINVOLTI (" _
                & "  ID, ID_TIPO_ENTE," _
                & "   COGNOME_ENTE_COINVOLTO, NOME_ENTE_COINVOLTO, " _
                & "   RUOLO, EMAIL, " _
                & "   TELEFONO) " _
                & " VALUES ( " & idAnagrEnteCoinv & "," _
                & " " & tipoEnte & "," _
                & par.insDbValue(CType(userControl.FindControl("txtCognome"), TextBox).Text, True).ToUpper & "," _
                & par.insDbValue(CType(userControl.FindControl("txtNome"), TextBox).Text, True).ToUpper & "," _
                & par.insDbValue(CType(userControl.FindControl("txtRuolo"), TextBox).Text, True).ToUpper & "," _
                & par.insDbValue(CType(userControl.FindControl("txtEmail"), TextBox).Text, True).ToUpper & "," _
                & par.insDbValue(CType(userControl.FindControl("txtTelefono"), TextBox).Text, True) & ")"
                par.cmd.ExecuteNonQuery()


                par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELENCO_ENTI_COINVOLTI (" _
                    & "   ID,ID_ANAGR_ENTE_COINV, ID_INTERVENTO)" _
                    & " VALUES (SISCOM_MI.SEQ_ELENCO_ENTI_COINVOLTI.NEXTVAL, " & idAnagrEnteCoinv & "," & idIntervento.Value & ")"
                par.cmd.ExecuteNonQuery()
            End If

            connData.chiudi(True)

            CaricaGridEnteCoinv()

            idAnagrEnteCoinv = 0

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - RadGridEnteCoinv_InsertCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridEnteCoinv_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridEnteCoinv.ItemCommand
        Try
            If e.CommandName = RadGrid.InitInsertCommandName Then '"Add new" button clicked

                Dim editColumn As GridEditCommandColumn = CType(RadGridEnteCoinv.MasterTableView.GetColumn("EditCommandColumn"), GridEditCommandColumn)
                editColumn.Visible = False

            ElseIf (e.CommandName = RadGrid.RebindGridCommandName AndAlso e.Item.OwnerTableView.IsItemInserted) Then
                e.Canceled = True
            Else
                Dim editColumn As GridEditCommandColumn = CType(RadGridEnteCoinv.MasterTableView.GetColumn("EditCommandColumn"), GridEditCommandColumn)
                If Not editColumn.Visible Then
                    editColumn.Visible = True
                End If

            End If

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - RadGridEnteCoinv_ItemCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridEnteCoinv_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridEnteCoinv.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("elencoEntiCoinvolti"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("elencoEntiCoinvolti"), Data.DataTable)

    End Sub

    Protected Sub RadGridEnteCoinv_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridEnteCoinv.UpdateCommand
        Try
            Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)
            connData.apri(True)

            Dim tipoEnte As String = ""
            If CType(userControl.FindControl("cmbTipoEnte"), RadComboBox).SelectedValue <> "-1" Then
                tipoEnte = par.insDbValue(CType(userControl.FindControl("cmbTipoEnte"), RadComboBox).SelectedValue, True, False)
            Else
                tipoEnte = "NULL"
            End If

            'ID, ID_TIPO_ENTE, " _"
            '    & "   COGNOME_ENTE_COINVOLTO, NOME_ENTE_COINVOLTO, " _
            '    & "   RUOLO, EMAIL, " _
            '    & "   TELEFONO) " _

            par.cmd.CommandText = "UPDATE SISCOM_MI.ANAGRAFICA_ENTI_COINVOLTI " _
                    & "SET RUOLO =" & par.insDbValue(CType(userControl.FindControl("txtRuolo"), TextBox).Text, True).ToUpper & "," _
                    & "ID_TIPO_ENTE  = " & tipoEnte & "," _
                    & "COGNOME_ENTE_COINVOLTO = " & par.insDbValue(CType(userControl.FindControl("txtCognome"), TextBox).Text, True).ToUpper & "," _
                    & "NOME_ENTE_COINVOLTO = " & par.insDbValue(CType(userControl.FindControl("txtNome"), TextBox).Text, True).ToUpper & "," _
                    & "EMAIL = " & par.insDbValue(CType(userControl.FindControl("txtEmail"), TextBox).Text, True).ToUpper & "," _
                    & "TELEFONO = " & par.insDbValue(CType(userControl.FindControl("txtTelefono"), TextBox).Text, True) & "" _
                    & "WHERE ID = " & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
            par.cmd.ExecuteNonQuery()
            connData.chiudi(True)

            CaricaGridEnteCoinv()

            RadGridEnteCoinv.EditIndexes.Clear()

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Segnalazione - CaricaInterventi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click

        'OnClientClick="window.open('StampeDocumenti.aspx?TIPO=' + document.getElementById('tipoIntervento').value + '&ST=' + document.getElementById('statoIntervento').value + '&IDINTERV=' + document.getElementById('idIntervento').value, '');"


        Dim apertura As String = "window.open('StampeDocumenti.aspx?TIPO=" & tipoIntervento.Value & "&ST=" & statoIntervento.Value & "&IDINTERV=" & idIntervento.Value & "', '');"
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apri", apertura, True)

    End Sub
End Class
