
Partial Class Contratti_ModuloRimborso
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            txtDataOp.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            par.caricaComboBox("SELECT * FROM SISCOM_MI.TAB_MOD_RESTITUZIONE ORDER BY DESCRIZIONE ASC", cmbTipo, "ID", "DESCRIZIONE", False)
            IDC.Value = Request.QueryString("IDC")
            IDG.Value = Request.QueryString("IDG")
            txtDataOp.Text = par.FormattaData(Format(Now, "yyyyMMdd"))

            ValoriCDP()

            If Verifica() = False Then

            End If
        End If
    End Sub

    Private Sub ValoriCDP()
        Try
            Dim HFIdVocePF0 As Integer = -1
            Dim HFidVoceInteressi0 As Integer = -1
            Dim HFidStruttura0 As Integer = -1
            Dim HFidFornitore0 As Integer = -1
            Dim HFdocRestCred0 As Integer = -1
            par.RicavaBPcredito(IDG.Value, HFIdVocePF0, HFidVoceInteressi0, HFidStruttura0, HFidFornitore0, HFdocRestCred0)
            HFIdVocePF.Value = HFIdVocePF0
            HFidVoceInteressi.Value = HFidVoceInteressi0
            HFidStruttura.Value = HFidStruttura0
            HFidFornitore.Value = HFidFornitore0
            HFdocRestCred.Value = HFdocRestCred0
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblerrore.Visible = True
            lblerrore.Text = ex.Message
        End Try
    End Sub

    Private Sub AssegnaLabel()
        Select Case cmbTipo.SelectedItem.Value
            Case "2", "3"
                txtEstremi.Visible = True
                lblEstremi.Text = "IBAN"
                lblEstremi.Visible = True
                lblAvviso.Visible = True
                lblAvviso.Text = "L'intestatario dell'IBAN deve coincidere l'intestatario del contratto"
            Case "1"
                txtEstremi.Visible = False
                lblEstremi.Text = ""
                lblEstremi.Visible = False
                lblAvviso.Visible = False
        End Select
    End Sub


    Private Function CtrlResiduo() As Decimal
        CtrlResiduo = -1
        'Dim IdVocePF As Long = 0
        'Dim idStruttura As Long = 0
        Dim TotBilancio As Decimal = 0
        Dim TotPrenotato As Decimal = 0
        Dim TotApprovato As Decimal = 0



        'par.cmd.CommandText = "select id_voce_bp_credito,id_struttura from SISCOM_MI.TAB_GEST_REST_CREDITO WHERE DATA_INIZIO_VALIDITA<='" & par.AggiustaData(txtDataOp.Text) & "' AND DATA_FINE_VALIDITA>='" & par.AggiustaData(txtDataOp.Text) & "' ORDER BY ID DESC"
        ' ***** par.cmd.CommandText = "select id_voce_bp_credito,id_struttura from SISCOM_MI.TAB_GEST_REST_CREDITO WHERE ID_VOCE_BP_RIMBORSO IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & par.RicavaPianoUltimoApprovato & ")"
        'par.cmd.CommandText = "select * from SISCOM_MI.TAB_GEST_REST_CREDITO WHERE id_voce_bp_credito IN (" & HFIdVocePF.Value & ")"
        'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        'If myReader.Read() Then
        '    IdVocePF = par.IfNull(myReader("id_voce_bp_credito"), 0)
        '    idStruttura = par.IfNull(myReader("id_struttura"), 0)
        'End If
        'myReader.Close()
        If HFIdVocePF.Value <> 0 And HFidStruttura.Value <> 0 Then
            TotBilancio = 0
            TotPrenotato = 0
            TotApprovato = 0

            par.cmd.CommandText = "select sum(nvl(valore_lordo,0) + nvl(assestamento_valore_lordo,0) + nvl(variazioni,0)) from siscom_mi.pf_voci_struttura where id_Voce = " & HFIdVocePF.Value & " and id_struttura = " & HFidStruttura.Value
            TotBilancio = par.cmd.ExecuteScalar

            par.cmd.CommandText = "select round(sum(nvl(importo_prenotato,0)-(nvl(importo_prenotato,0)*nvl(perc_iva,0))/100),2)  as tot  from siscom_mi.prenotazioni where  id_voce_pf = " & HFIdVocePF.Value & " and id_struttura = " & HFidStruttura.Value & " and id_stato = 0"
            TotPrenotato = par.IfNull(par.cmd.ExecuteScalar, 0)

            par.cmd.CommandText = "select round(sum(nvl(importo_approvato,0)-(nvl(importo_approvato,0)*nvl(perc_iva,0))/100),2)  as tot  from siscom_mi.prenotazioni where  id_voce_pf = " & HFIdVocePF.Value & " and id_struttura = " & HFidStruttura.Value & " and id_stato > 0 "
            TotApprovato = CDec(par.IfNull(par.cmd.ExecuteScalar, 0))


            CtrlResiduo = TotBilancio - TotPrenotato - TotApprovato - Math.Abs(CDec(par.IfEmpty(txtCredito.Text.Replace(".", ""), 0)))

        End If


    End Function

    Private Function Verifica() As Boolean
        Try
            Verifica = True
            Dim TESTO As String = ""


            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = " select rapporti_utenza.cod_contratto,SISCOM_MI.GETINTESTATARI(ID) AS INTESTATARIO  FROM SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA.ID=" & IDC.Value
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                lblCodice.Text = "COD. CONTRATTO " & par.IfNull(myReader3("COD_CONTRATTO"), "")
                lblIntestatario.Text = Mid(par.IfNull(myReader3("INTESTATARIO"), ""), 1, Len(par.IfNull(myReader3("INTESTATARIO"), "")) - 1)
            End If
            myReader3.Close()

            par.cmd.CommandText = "select BOL_BOLLETTE_GEST.*,TIPO_BOLLETTE_GEST.UTILIZZABILE from SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.TIPO_BOLLETTE_GEST WHERE TIPO_BOLLETTE_GEST.ID=BOL_BOLLETTE_GEST.ID_TIPO  AND FL_VISUALIZZABILE=1 AND BOL_BOLLETTE_GEST.ID=" & IDG.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                If par.IfNull(myReader("ID_TIPO"), "") = "55" Then
                    TESTO = "\nNon è possibile utilizzare questo tipo di credito"
                End If
                If par.IfNull(myReader("TIPO_APPLICAZIONE"), "") = "T" Then
                    TESTO = "\nScrittura già elaborata"
                End If
                If par.IfNull(myReader("TIPO_APPLICAZIONE"), "") = "P" Then
                    TESTO = "\nScrittura già elaborata parzialmente"
                End If
                If par.IfNull(myReader("IMPORTO_TOTALE"), 0) >= 0 Then
                    TESTO = "\nImporto della Scrittura superiore a 0 euro"
                Else
                    txtCredito.Text = Format(par.IfNull(myReader("IMPORTO_TOTALE"), 0) * -1, "0.00")
                    txtCredito.Enabled = False
                End If
                'If par.IfNull(myReader("utilizzabile"), "0") = "0" Then
                '    TESTO = TESTO & "\nTipologia Scrittura non utilizzabile!"
                'End If
            End If
            myReader.Close()

            Dim residuo As Decimal = CtrlResiduo()
            If residuo = -1 Then
                TESTO = TESTO & "\nNon sono stati definiti i parametri per la restituzione crediti. Contattare il supervisore!"
            Else
                If residuo < 0 Then
                    TESTO = TESTO & "\nResiduo B.P. non sufficiente per emettere il rimborso!"
                End If
            End If

            If txtCredito.Text = "" Then
                TESTO = TESTO & "\nNessun credito selezionato!"
            End If

            If Session.Item("FL_FORZARIMBORSO") = "0" Then
                If par.CalcolaSaldoAttuale(IDC.Value) > 0 Then
                    TESTO = TESTO & "\nIl Saldo superiore a 0!"
                End If
            End If


            If TESTO <> "" Then
                Verifica = False
                Response.Write("<script>alert('Attenzione...non è possibile procedere per i seguenti motivi:" & TESTO & "');</script>")
                btnProcedi.Visible = False
                cmbTipo.Enabled = False
                txtCredito.Enabled = False
                txtDataOp.Enabled = False
                txtEstremi.Enabled = False
            End If


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblerrore.Text = ex.Message
            btnProcedi.Visible = False
            btnProcedi.Visible = False
            cmbTipo.Enabled = False
            txtCredito.Enabled = False
            txtDataOp.Enabled = False
            txtEstremi.Enabled = False
        End Try
    End Function

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If errore.Value = "0" Then
            If txtCredito.Text < 0 Then
                Label6.Visible = True
                Label6.Text = "Necessario valore positivo"
            End If
            Try
                Dim ID_BOLLETTA As Long = 0

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()

                'Dim IdVocePF As Long = 0
                'Dim IdVocePFInteressi As Long = 0
                'Dim idFornitore As Long = 0
                'Dim idStruttura As Long = 0

                Dim Estremi As String = ""
                Dim TIPO_PAG As String = "NULL"

                lblerrore.Visible = False
                If cmbTipo.SelectedItem.Value = "2" Or cmbTipo.SelectedItem.Value = "3" Then
                    If par.ControllaIBAN(txtEstremi.Text) = False Then
                        lblerrore.Visible = True
                        lblerrore.Text = "IBAN non corretto!"
                    End If
                End If

                Select Case cmbTipo.SelectedItem.Value
                    Case "1"
                        TIPO_PAG = "1"
                        Estremi = " Assegno circolare intestato a " & lblIntestatario.Text & " - " & lblCodice.Text
                    Case "2", "3"
                        TIPO_PAG = "2"
                        Estremi = " " & lblEstremi.Text & " " & txtEstremi.Text & " intestato a " & lblIntestatario.Text & " - " & lblCodice.Text
                End Select

                If lblerrore.Visible = False Then



                    'par.cmd.CommandText = "select * from SISCOM_MI.TAB_GEST_REST_CREDITO WHERE DATA_INIZIO_VALIDITA<='" & par.AggiustaData(txtDataOp.Text) & "' AND DATA_FINE_VALIDITA>='" & par.AggiustaData(txtDataOp.Text) & "' ORDER BY ID DESC"
                    'par.cmd.CommandText = "select * from SISCOM_MI.TAB_GEST_REST_CREDITO WHERE ID_VOCE_BP_RIMBORSO IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & par.RicavaPianoUltimoApprovato & ")"
                    'par.cmd.CommandText = "select * from SISCOM_MI.TAB_GEST_REST_CREDITO WHERE id_voce_bp_credito IN (" & HFIdVocePF.Value & ")"
                    'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReader.Read() Then
                    'IdVocePF = myReader("id_voce_bp_credito")
                    'IdVocePFInteressi = par.IfNull(myReader("id_voce_bp_interessi"), -1)
                    'idFornitore = myReader("id_fornitore")
                    'idStruttura = myReader("id_struttura")
                    par.cmd.CommandText = "select SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA,RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO,UNITA_CONTRATTUALE.ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & IDC.Value & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
                    Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderS.Read Then
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                                            & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                                            & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                            & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                            & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                            & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                            & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                                            & "Values " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") _
                                            & "', '29991231', NULL, " _
                                            & "NULL, '','RIMBORSO CREDITO'," & IDC.Value _
                                            & " ," & par.RicavaEsercizioCorrente & ", " _
                                            & par.IfNull(myReaderS("ID_UNITA"), "") _
                                            & ", '0', '', " & par.IfNull(myReaderS("ID_ANAGRAFICA"), "") _
                                            & ", '" & par.PulisciStrSql(par.IfNull(myReaderS("PRESSO_COR"), "")) & "', " _
                                            & "'" & par.PulisciStrSql(par.IfNull(myReaderS("TIPO_COR"), "") & " " & par.IfNull(myReaderS("VIA_COR"), "") & ", " & par.IfNull(myReaderS("civico_COR"), "")) _
                                            & "', '" & par.PulisciStrSql(par.IfNull(myReaderS("CAP_COR"), "") _
                                            & " " & par.IfNull(myReaderS("LUOGO_COR"), "") & "(" & par.IfNull(myReaderS("SIGLA_COR"), "") & ")") & "', NULL, '" & Format(Now, "yyyyMMdd") & "', '" & Format(Now, "yyyyMMdd") & "', " _
                                            & "'1', " & myReaderS("ID_COMPLESSO") & ", '', NULL, '', " _
                                            & Year(Now) & ", '', " & myReaderS("ID_EDIFICIO") & ", NULL, NULL,'MOD', " & HFdocRestCred.Value & ")"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Dim importoInteressi As Decimal = 0
                        If myReaderA.Read Then
                            ID_BOLLETTA = myReaderA(0)

                            par.cmd.CommandText = "select * FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & IDG.Value
                            Dim myReaderG As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            Do While myReaderG.Read
                                If myReaderG("ID_VOCE") = 15 Then
                                    importoInteressi = importoInteressi + par.IfNull(myReaderG("IMPORTO"), 0)
                                End If
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                               & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & myReaderG("ID_VOCE") _
                               & "," & par.VirgoleInPunti(myReaderG("IMPORTO")) & ")"
                                par.cmd.ExecuteNonQuery()
                            Loop
                            myReaderG.Close()

                        End If
                        myReaderA.Close()

                        par.cmd.CommandText = "select SUM(IMPORTO) FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & ID_BOLLETTA
                        Dim impTotVoci As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)
                        If Math.Abs(impTotVoci) <> CDec(txtCredito.Text) Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET IMPORTO=-" & par.VirgoleInPunti(Me.txtCredito.Text.Replace(".", "")) & " WHERE ID_BOLLETTA=" & ID_BOLLETTA
                            par.cmd.ExecuteNonQuery()
                        End If

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & IDC.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F08','RIMBORSO CREDITO DI EURO " & Me.txtCredito.Text & "')"
                        par.cmd.ExecuteNonQuery()



                        '04/05/2015 INSERIMENTO CDP
                        Dim SStringa As String = ""
                        Dim vIdCDP As Long

                        par.cmd.CommandText = " select SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM dual "
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            vIdCDP = myReader1(0)
                        End If
                        myReader1.Close()

                        Dim creditoGenerico As Decimal = 0
                        creditoGenerico = txtCredito.Text - Math.Abs(importoInteressi)

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PAGAMENTI (ID,DATA_EMISSIONE,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_APPALTO,ID_STATO,TIPO_PAGAMENTO,CONTO_CORRENTE) " _
                                            & "VALUES " _
                                            & "(" & vIdCDP & "," & par.AggiustaData(txtDataOp.Text) & ", '" & par.PulisciStrSql("RIMBORSO CREDITO ") & par.PulisciStrSql(Estremi) & "'," _
                                            & par.VirgoleInPunti(Me.txtCredito.Text.Replace(".", "")) & "," & HFidFornitore.Value & ",NULL,1,11,'---')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI (ID,ID_FORNITORE,ID_VOCE_PF,ID_STATO,ID_PAGAMENTO,TIPO_PAGAMENTO,DESCRIZIONE,DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,IMPORTO_APPROVATO,DATA_STAMPA,ID_STRUTTURA,DATA_CONSUNTIVAZIONE,DATA_CERTIFICAZIONE) VALUES " _
                                        & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL," & HFidFornitore.Value & "," & HFIdVocePF.Value & ",2," & vIdCDP & ",11,'" & par.PulisciStrSql("RIMBORSO CREDITO ") & par.PulisciStrSql(Estremi) _
                                        & "','" & par.AggiustaData(txtDataOp.Text) & "'," & par.VirgoleInPunti(creditoGenerico) & "," & par.VirgoleInPunti(creditoGenerico) _
                                        & ",'" & par.AggiustaData(txtDataOp.Text) & "'," & HFidStruttura.Value & ",'" & par.AggiustaData(txtDataOp.Text) & "','" & par.AggiustaData(txtDataOp.Text) & "')"
                        par.cmd.ExecuteNonQuery()


                        If importoInteressi <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI (ID,ID_FORNITORE,ID_VOCE_PF,ID_STATO,ID_PAGAMENTO,TIPO_PAGAMENTO,DESCRIZIONE,DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,IMPORTO_APPROVATO,DATA_STAMPA,ID_STRUTTURA,DATA_CONSUNTIVAZIONE,DATA_CERTIFICAZIONE) VALUES " _
                                               & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL," & HFidFornitore.Value & "," & HFidVoceInteressi.Value & ",2," & vIdCDP & ",10,'" & par.PulisciStrSql("RIMBORSO INTERESSI DEPOSITO CAUZIONALE ") & par.PulisciStrSql(Estremi) _
                                               & "','" & par.AggiustaData(txtDataOp.Text) & "'," & par.VirgoleInPunti(importoInteressi * -1) & "," & par.VirgoleInPunti(importoInteressi * -1) _
                                               & ",'" & par.AggiustaData(txtDataOp.Text) & "'," & HFidStruttura.Value & ",'" & par.AggiustaData(txtDataOp.Text) & "','" & par.AggiustaData(txtDataOp.Text) & "')"
                            par.cmd.ExecuteNonQuery()
                        End If


                        '****Scrittura evento EMISSIONE DEL PAGAMENTO
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vIdCDP & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELENCO_BOLL_CDP (ID_BOLLETTA,ID_CDP,ID_BOLL_GEST,ID_TIPO_MODALITA_PAG,ESTREMI_IBAN_CC) VALUES (" & ID_BOLLETTA & "," & vIdCDP & "," & IDG.Value & "," & TIPO_PAG & ",'" & par.PulisciStrSql(txtEstremi.Text) & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T',DATA_APPLICAZIONE='" & par.AggiustaData(txtDataOp.Text) & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & "  WHERE ID=" & IDG.Value
                        par.cmd.ExecuteNonQuery()


                        ''INSERIMENTO IN ODL
                        'Dim SStringa As String = ""
                        'Dim vIdODL As Long


                        'par.cmd.CommandText = " select SISCOM_MI.SEQ_ODL.NEXTVAL FROM dual "
                        'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        'If myReader1.Read Then
                        '    vIdODL = myReader1(0)
                        'End If
                        'myReader1.Close()

                        'par.cmd.CommandText = "insert into SISCOM_MI.ODL " _
                        '                & " (ID,DATA_ORDINE,DESCRIZIONE,ID_VOCE_PF,ID_FORNITORE,ID_PRENOTAZIONE,ID_PRENOTAZIONE_RIT," _
                        '                & " PREN_NETTO, CASSA, IVA, RIT_ACCONTO, PREN_NO_IVA, PREN_LORDO,ID_STATO,PENALE,ID_STRUTTURA,CASSA_CONS, IVA_CONS, RIT_ACCONTO_CONS)" _
                        '        & "values ('" & vIdODL & "'," _
                        '        & "'" & par.AggiustaData(txtDataOp.Text) & "', " _
                        '        & "'" & par.PulisciStrSql("RIMBORSO CREDITO ") & Estremi & "', " _
                        '        & "'" & IdVocePF & "', " _
                        '        & "'" & idFornitore & "', " _
                        '        & "NULL, " _
                        '        & "NULL, " _
                        '        & "'" & strToNumber(Me.txtCredito.Text.Replace(".", "")) & "', " _
                        '        & "'0', " _
                        '        & "'0', " _
                        '        & "'0', " _
                        '        & "NULL, " _
                        '        & "'" & strToNumber(Me.txtCredito.Text.Replace(".", "")) & "', " _
                        '        & "'0', " _
                        '        & "'0', " _
                        '        & "'" & RitornaNullSeIntegerMenoUno(idStruttura) & "', " _
                        '        & "NULL, " _
                        '        & "NULL, " _
                        '        & "NULL)"
                        'par.cmd.ExecuteNonQuery()

                        'par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','')"
                        'par.cmd.ExecuteNonQuery()

                        'par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELENCO_BOLL_ODL (ID_BOLLETTA,ID_ODL,ID_BOLL_GEST,ID_TIPO_MODALITA_PAG,ESTREMI_IBAN_CC) VALUES (" & ID_BOLLETTA & "," & vIdODL & "," & IDG.Value & "," & TIPO_PAG & ",'" & par.PulisciStrSql(txtEstremi.Text) & "')"
                        'par.cmd.ExecuteNonQuery()

                        'par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T',DATA_APPLICAZIONE='" & par.AggiustaData(txtDataOp.Text) & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & "  WHERE ID=" & IDG.Value
                        'par.cmd.ExecuteNonQuery()

                        Response.Write("<script>alert('Operazione effettuata!');self.close();</script>")

                    End If
                    myReaderS.Close()
                    'End If
                    'myReader.Close()
                End If

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblerrore.Visible = True
                lblerrore.Text = ex.Message
            End Try
        End If
    End Sub

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function

    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        Try
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If

        Catch ex As Exception
        End Try
        Return a

    End Function

    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function

    Protected Sub cmbTipo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged
        AssegnaLabel()
    End Sub
End Class
