Imports System.Collections.Generic

Partial Class Contratti_ModuloRimborsoDC
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If Not IsPostBack Then
            'txtDataOp.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataOp.Text = Format(Now, "dd/MM/yyyy")
            txtDataOp.Enabled = False

            par.caricaComboBox("SELECT * FROM SISCOM_MI.TAB_MOD_RESTITUZIONE ORDER BY DESCRIZIONE ASC", cmbTipo, "ID", "DESCRIZIONE", False)
            IDC.Value = Request.QueryString("IDC")
            Verifica()

            ValoriCDP()
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

    Private Function CalcolaInteressi() As Double
        Dim Interessi As New SortedDictionary(Of Integer, Double)
        Dim DataCalcolo As String = ""
        Dim DataInizio As String = ""

        Dim tasso As Double = 0
        Dim baseCalcolo As Double = 0

        Dim Giorni As Integer = 0
        Dim GiorniAnno As Integer = 0
        Dim dataPartenza As String = ""

        Dim Totale As Double = 0
        Dim TotalePeriodo As Double = 0
        Dim indice As Long = 0
        Dim DataFine As String = ""

        par.cmd.CommandText = "select * from siscom_mi.tab_interessi_legali order by anno desc"
        Interessi.Clear()
        Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Do While myReaderC.Read
            Interessi.Add(myReaderC("anno"), myReaderC("tasso"))
        Loop
        myReaderC.Close()

        DataCalcolo = par.AggiustaData(txtDataOp.Text)

        baseCalcolo = txtCredito.Text
        If baseCalcolo > 0 Then

            par.cmd.CommandText = "select * from siscom_mi.adeguamento_interessi where id_contratto=" & IDC.Value & " and fl_applicato=1 order by id desc"
            Dim myReaderZ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderZ.HasRows = False Then
                DataInizio = DECORRENZA.Value
            End If
            If myReaderZ.Read Then
                DataInizio = Format(DateAdd(DateInterval.Day, 1, CDate(par.FormattaData(myReaderZ("data")))), "yyyyMMdd")
            End If

            myReaderZ.Close()

            Giorni = 0
            GiorniAnno = 0
            dataPartenza = DataInizio
            Totale = 0
            TotalePeriodo = 0

            par.cmd.CommandText = "insert into siscom_mi.adeguamento_interessi (id,id_contratto,data,fl_applicato) values (siscom_mi.seq_adeguamento_interessi.nextval," & IDC.Value & ",'" & DataCalcolo & "',1)"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "select siscom_mi.seq_adeguamento_interessi.currval from dual"
            myReaderZ = par.cmd.ExecuteReader()
            indice = 0
            If myReaderZ.Read Then
                indice = myReaderZ(0)
            End If

            For I = CInt(Mid(DataInizio, 1, 4)) To CInt(Mid(DataCalcolo, 1, 4))

                If I = CInt(Mid(DataCalcolo, 1, 4)) Then
                    DataFine = par.FormattaData(DataCalcolo)
                Else
                    DataFine = "31/12/" & I

                End If

                GiorniAnno = DateDiff(DateInterval.Day, CDate("01/01/" & I), CDate("31/12/" & I)) + 1

                Giorni = DateDiff(DateInterval.Day, CDate(par.FormattaData(dataPartenza)), CDate(DataFine)) + 1

                If I < 1990 Then
                    tasso = 5
                Else
                    If Interessi.ContainsKey(I) = True Then
                        tasso = Interessi(I)
                    End If
                End If

                TotalePeriodo = Format((((baseCalcolo * tasso) / 100) / GiorniAnno) * Giorni, "0.00")
                Totale = Totale + TotalePeriodo

                par.cmd.CommandText = "insert into siscom_mi.adeguamento_interessi_voci (id_adeguamento,dal,al,giorni,tasso,importo) values (" & indice & ",'" & dataPartenza & "','" & Format(CDate(DataFine), "yyyyMMdd") & "'," & Giorni & "," & par.VirgoleInPunti(tasso) & "," & par.VirgoleInPunti(TotalePeriodo) & ")"
                par.cmd.ExecuteNonQuery()

                dataPartenza = I + 1 & "0101"

            Next
            par.cmd.CommandText = "update siscom_mi.adeguamento_interessi set importo=" & par.VirgoleInPunti(Format(Totale, "0.00")) & " where id=" & indice
            par.cmd.ExecuteNonQuery()
            CalcolaInteressi = Totale
        Else
            CalcolaInteressi = 0
        End If
    End Function

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

    Private Function Verifica()
        Try
            Dim Errore As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = " select rapporti_utenza.cod_contratto,SISCOM_MI.GETINTESTATARI(ID) AS INTESTATARIO,SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND RAPPORTI_UTENZA.ID=" & IDC.Value
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                lblCodice.Text = "COD. CONTRATTO " & par.IfNull(myReader3("COD_CONTRATTO"), "")
                lblIntestatario.Text = par.IfNull(myReader3("INTESTATARIO"), "") 'Mid(par.IfNull(myReader3("INTESTATARIO"), ""), 1, Len(par.IfNull(myReader3("INTESTATARIO"), "")) - 1)
                ID_ANAGRAFICA_CONDUTTORE = par.IfNull(myReader3("ID_ANAGRAFICA"), "")
            End If
            myReader3.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID_TIPO=55 AND ID_CONTRATTO=" & IDC.Value & " ORDER BY ID DESC"
            myReader3 = par.cmd.ExecuteReader()
            If myReader3.Read Then
                If ID_ANAGRAFICA_CONDUTTORE <> par.IfNull(myReader3("ID_ANAGRAFICA"), 0) Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE ID=" & par.IfNull(myReader3("ID_ANAGRAFICA"), 0)
                    Dim myReaderAN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderAN.Read Then
                        If par.IfNull(myReaderAN("RAGIONE_SOCIALE"), "") <> "" Then
                            lblIntestatario.Text = par.IfNull(myReaderAN("RAGIONE_SOCIALE"), "")

                        Else
                            lblIntestatario.Text = par.IfNull(myReaderAN("COGNOME"), "") & " " & par.IfNull(myReaderAN("NOME"), "")

                        End If
                        'ID_ANAGRAFICA_CONDUTTORE = par.IfNull(myReaderAN("ID"), 0)
                    End If
                    myReaderAN.Close()
                End If
                IDG.Value = par.IfNull(myReader3("ID"), 0)
            End If
            myReader3.Close()

            par.cmd.CommandText = " select RAPPORTI_UTENZA_DEP_CAUZ.* FROM SISCOM_MI.RAPPORTI_UTENZA_DEP_CAUZ WHERE ID_CONTRATTO=" & IDC.Value
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then

                txtNote.Text = par.IfNull(myReader2("NOTE_PAGAMENTO"), "")
                txtInteressi.Text = "0,00"
                txtDataOp.Text = par.FormattaData(par.IfNull(myReader2("DATA_OPERAZIONE"), ""))
                cmbTipo.SelectedValue = par.IfNull(myReader2("TIPO_PAGAMENTO"), "-1")
                txtEstremi.Text = par.IfNull(myReader2("IBAN_CC"), "")
                txtCredito.Enabled = False

                AssegnaLabel()
                Errore = "\nFunzione già attivata"
            End If
            myReader2.Close()
            txtCredito.Text = "0,00"
            par.cmd.CommandText = "select sum(importo_totale) from siscom_mi.bol_bollette_gest where id_tipo=55 and tipo_applicazione='N' and id_contratto=" & IDC.Value
            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read Then
                txtCredito.Text = -1 * par.IfNull(myReader2(0), "0,00")
                If par.IfNull(myReader2(0), "0,00") > 0 Then
                    Errore = Errore & "\nNon è possibile restituire un deposito di importo positivo!"
                End If
            End If
            myReader2.Close()
            If txtCredito.Text = "0" Then
                Errore = Errore & "\nNessun credito per rimborso deposito cauzionale in partita gesionale!"
            End If

            ''MAX 28/08/2017 RESTITUISCO IL DEPOSITO SOLO SE E' STATO PAGATO UN DEPOSITO DA QUESTO CONDUTTORE
            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_TIPO=9 AND ID_BOLLETTA_STORNO IS NULL AND NVL(IMPORTO_PAGATO,0)>0 and id_contratto=" & IDC.Value & " AND COD_AFFITTUARIO=" & ID_ANAGRAFICA_CONDUTTORE
            'myReader2 = par.cmd.ExecuteReader()
            'If myReader2.HasRows = False Then
            '    Errore = Errore & "\nNessun DEPOSITO CAUZIONALE PAGATO in partita contabile!"
            'Else
            '    If myReader2.Read Then
            '        IdBollettaDPC = myReader2("id")
            '    End If
            'End If
            'myReader2.Close()
            '----FINE
            'MAX 07/02/2018 SOSTITUISCO PRECEDENTE CONTROLLO CON IL CAPO RESTITUIBILE
            'MAX 28/08/2017 RESTITUISCO IL DEPOSITO SOLO SE E' STATO PAGATO UN DEPOSITO DA QUESTO CONDUTTORE
            par.cmd.CommandText = "select * FROM SISCOM_MI.STORICO_DEP_CAUZIONALE where id_contratto=" & IDC.Value & " and id_anagrafica=" & ID_ANAGRAFICA_CONDUTTORE & " and restituibile=1"
            myReader2 = par.cmd.ExecuteReader()
            If myReader2.HasRows = False Then
                Errore = Errore & "\nNessun DEPOSITO CAUZIONALE PAGATO in partita contabile!"
            Else
                If myReader2.Read Then
                    IdBollettaDPC = par.IfNull(myReader2("id_bolletta"), 0)
                End If
            End If
            myReader2.Close()

            If Errore <> "" Then
                txtNote.Enabled = False
                cmbTipo.Enabled = False
                btnProcedi.Enabled = False
                btnProcedi.Visible = False
                txtEstremi.Enabled = False
                txtCredito.Enabled = False
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Non è possibile procedere per i seguenti motivi:" & Errore & "');</script>")
                Exit Function
            End If
            par.cmd.CommandText = " select DATA_RICONSEGNA,imp_deposito_cauz,DATA_DECORRENZA,FL_INTERESSI_C,SISCOM_MI.GETINTESTATARI(ID) AS INTESTATARIO FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & IDC.Value
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                If par.IfNull(myReader1("DATA_RICONSEGNA"), "") = "" Then
                    Errore = "\nNon è stata inserita la data di sloggio."
                End If
                lblIntestatario.Text = par.IfNull(myReader1("INTESTATARIO"), "") 'Mid(par.IfNull(myReader1("INTESTATARIO"), ""), 1, Len(par.IfNull(myReader1("INTESTATARIO"), "")) - 1)

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID_TIPO=55 AND ID_CONTRATTO=" & IDC.Value & " ORDER BY ID DESC"
                myReader3 = par.cmd.ExecuteReader()
                If myReader3.Read Then
                    If ID_ANAGRAFICA_CONDUTTORE <> par.IfNull(myReader3("ID_ANAGRAFICA"), 0) Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE ID=" & par.IfNull(myReader3("ID_ANAGRAFICA"), 0)
                        Dim myReaderAN1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderAN1.Read Then
                            If par.IfNull(myReaderAN1("RAGIONE_SOCIALE"), "") <> "" Then
                                lblIntestatario.Text = par.IfNull(myReaderAN1("RAGIONE_SOCIALE"), "")

                            Else
                                lblIntestatario.Text = par.IfNull(myReaderAN1("COGNOME"), "") & " " & par.IfNull(myReaderAN1("NOME"), "")

                            End If

                        End If
                        myReaderAN1.Close()
                    End If
                End If
                myReader3.Close()

                'txtCredito.Text = par.IfNull(myReader1("IMP_DEPOSITO_CAUZ"), "0,00")
                txtCredito.Enabled = False
                DECORRENZA.Value = par.IfNull(myReader1("DATA_DECORRENZA"), Format(Now, "yyyyMMdd"))
                'If myReader1("FL_INTERESSI_C") = "1" Then
                '    txtInteressi.Text = Format(CalcolaInteressi(), "0.00")
                '    INTERESSI.Value = "1"
                'Else
                '    txtInteressi.Text = "0,00"
                '    INTERESSI.Value = "0"
                'End If
                txtInteressi.Text = "0,00"
                INTERESSI.Value = "0"
            End If
            myReader1.Close()

            Dim Saldo As Double = par.CalcolaSaldoAttuale(IDC.Value)
            If Saldo <> 0 Then
                Errore = Errore & "\nIl saldo attuale non è pari a zero"
            End If

            par.cmd.CommandText = " select * FROM SISCOM_MI.eventi_contratti WHERE COD_EVENTO='F18' AND ID_contratto=" & IDC.Value
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.HasRows = False Then
                Errore = Errore & "\nNon è stata inserita la Bolletta di fine contratto."
            End If
            myReader1.Close()

            ''max da abilitare solo in caso di forzatura cdp tramite segnalazione service desk
            'txtInteressi.Text = "3,02"
            'INTERESSI.Value = "1"

            If Errore <> "" Then
                txtNote.Enabled = False
                cmbTipo.Enabled = False
                btnProcedi.Enabled = False
                btnProcedi.Visible = False
                txtEstremi.Enabled = False
                Response.Write("<script>alert('Non è possibile procedere per i seguenti motivi:" & Errore & "');</script>")
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblerrore.Visible = True
            lblerrore.Text = ex.Message
        End Try
    End Function

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click


        Try
            Dim ID_BOLLETTA As Long = 0

            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()
            par.myTrans = par.OracleConn.BeginTransaction()

            'Dim IdVocePFDep As Long = 0
            'Dim IdVocePFInt As Integer = 0
            '
            'Dim idFornitore As Long = 0
            'Dim idStruttura As Long = 0
            Dim TIPO_PAG As String = "NULL"
            Dim Estremi As String = ""

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
                    Estremi = " Assegno circolare intestato a " & lblIntestatario.Text & " - " & lblCodice.Text & "<br>" & txtNote.Text
                Case "2", "3"
                    TIPO_PAG = "2"
                    Estremi = " " & lblEstremi.Text & " " & txtEstremi.Text & " intestato a " & lblIntestatario.Text & " - " & lblCodice.Text & "<br>" & txtNote.Text
            End Select

            'puccia
            'Dim PianoF As Long = par.RicavaPianoUltimoApprovato

            'par.cmd.CommandText = " SELECT ID  FROM SISCOM_MI.PF_VOCI WHERE ID_TIPO_UTILIZZO=2 AND ID_PIANO_FINANZIARIO = " & PianoF
            'IdVocePFDep = par.IfNull(par.cmd.ExecuteScalar, 0)

            ''max istruzioni commentate, usare solo in presenza di forzatura CDP e Iinteressi segnalati sul service desk
            'par.cmd.CommandText = " SELECT ID  FROM SISCOM_MI.PF_VOCI WHERE ID_TIPO_UTILIZZO=3 AND ID_PIANO_FINANZIARIO = " & PianoF
            'IdVocePFInt = par.IfNull(par.cmd.ExecuteScalar, 0)


            If HFIdVocePF.Value = -1 Then
                lblerrore.Visible = True
                lblerrore.Text = "VOCE BP NON TROVATA"
            End If

            If lblerrore.Visible = False Then
                'par.cmd.CommandText = "select * from SISCOM_MI.TAB_GEST_REST_CREDITO WHERE DATA_INIZIO_VALIDITA<='" & par.AggiustaData(txtDataOp.Text) & "' AND DATA_FINE_VALIDITA>='" & par.AggiustaData(txtDataOp.Text) & "' ORDER BY data_fine_validita desc,data_inizio_validita desc"
                'par.cmd.CommandText = "select * from SISCOM_MI.TAB_GEST_REST_CREDITO WHERE ID_VOCE_BP_RIMBORSO IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & PianoF & ") ORDER BY data_fine_validita desc,data_inizio_validita desc"

                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader.Read() Then

                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T',DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE id_tipo=55 and tipo_applicazione='N' and id_contratto=" & IDC.Value
                par.cmd.ExecuteNonQuery()

                'IdVocePF = myReader("id_voce_bp_rimborso")
                'idFornitore = myReader("id_fornitore")
                'idStruttura = myReader("id_struttura")
                par.cmd.CommandText = "select SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA,RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO,UNITA_CONTRATTUALE.ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & IDC.Value & " AND UNITA_CONTRATTUALE.ID_CONTRATTO (+)=RAPPORTI_UTENZA.ID AND EDIFICI.ID (+)=UNITA_CONTRATTUALE.ID_EDIFICIO"
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
                                        & "NULL, '','RIMBORSO DEPOSITO CAUZIONALE'," & IDC.Value _
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
                    If myReaderA.Read Then
                        ID_BOLLETTA = myReaderA(0)

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",50" _
                        & ",-" & par.VirgoleInPunti(txtCredito.Text) & ")"
                        par.cmd.ExecuteNonQuery()

                        If INTERESSI.Value = "1" Then
                            If txtInteressi.Text > 0 Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",15" _
                                    & ",-" & par.VirgoleInPunti(txtInteressi.Text) & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If

                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & IDC.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F08','RIMBORSO DEPOSITO CAUZIONALE')"
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

                    Dim totPag As Decimal = 0
                    totPag = CDec(par.IfEmpty(Me.txtCredito.Text, 0)) + CDec(par.IfEmpty(Me.txtInteressi.Text, 0))

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PAGAMENTI (ID,DATA_EMISSIONE,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_APPALTO,ID_STATO,TIPO_PAGAMENTO,CONTO_CORRENTE) " _
                    & "VALUES " _
                    & "(" & vIdCDP & "," & par.AggiustaData(txtDataOp.Text) & ", '" & par.PulisciStrSql("RIMBORSO DEPOSITO CAUZIONALE ") & par.PulisciStrSql(Estremi) & "'," _
                    & par.VirgoleInPunti(totPag) & "," & HFidFornitore.Value & ",NULL,1,10,'---')"
                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI (ID,ID_FORNITORE,ID_VOCE_PF,ID_STATO,ID_PAGAMENTO,TIPO_PAGAMENTO,DESCRIZIONE,DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,IMPORTO_APPROVATO,DATA_STAMPA,ID_STRUTTURA,DATA_CONSUNTIVAZIONE,DATA_CERTIFICAZIONE) VALUES " _
                                        & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL," & HFidFornitore.Value & "," & HFIdVocePF.Value & ",2," & vIdCDP & ",10,'" & par.PulisciStrSql("RIMBORSO DEPOSITO CAUZIONALE ") & par.PulisciStrSql(Estremi) _
                                        & "','" & par.AggiustaData(txtDataOp.Text) & "'," & par.VirgoleInPunti(Me.txtCredito.Text.Replace(".", "")) & "," & par.VirgoleInPunti(Me.txtCredito.Text.Replace(".", "")) _
                                        & ",'" & par.AggiustaData(txtDataOp.Text) & "'," & HFidStruttura.Value & ",'" & par.AggiustaData(txtDataOp.Text) & "','" & par.AggiustaData(txtDataOp.Text) & "')"
                    par.cmd.ExecuteNonQuery()

                    ''max da usare solo in caso di forzatura cdp con restituzione interessi

                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI (ID,ID_FORNITORE,ID_VOCE_PF,ID_STATO,ID_PAGAMENTO,TIPO_PAGAMENTO,DESCRIZIONE,DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,IMPORTO_APPROVATO,DATA_STAMPA,ID_STRUTTURA) VALUES " _
                    '                    & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL," & idFornitore & "," & IdVocePFInt & ",2," & vIdCDP & ",10,'" & par.PulisciStrSql("RIMBORSO DEPOSITO CAUZIONALE ") & par.PulisciStrSql(Estremi) _
                    '                    & "','" & par.AggiustaData(txtDataOp.Text) & "'," & par.VirgoleInPunti(Me.txtInteressi.Text.Replace(".", "")) & "," & par.VirgoleInPunti(Me.txtInteressi.Text.Replace(".", "")) _
                    '                    & ",'" & par.AggiustaData(txtDataOp.Text) & "'," & idStruttura & ")"
                    'par.cmd.ExecuteNonQuery()



                    ' '' '' ''puccia
                    '' '' ''par.cmd.CommandText = "UPDATE PF_VOCI_STRUTTURA  SET VALORE_NETTO=VALORE_NETTO - " & par.VirgoleInPunti(Me.txtCredito.Text.Replace(".", "")) & ", VALORE_LORDO=VALORE_LORDO - " & par.VirgoleInPunti(Me.txtCredito.Text.Replace(".", "")) & " WHERE ID_VOCE = " & IdVocePF
                    '' '' ''par.cmd.ExecuteNonQuery()
                    '' '' ''par.cmd.CommandText = "INSERT INTO PF_VOCI_DEP_CAUZ (ID_VOCE,ID_CONTRATTO,ID_PAGAMENTO,IMPORTO,DATA_ORA) VALUES (" & IdVocePF & "," & IDC.Value & "," & vIdCDP & "," & par.VirgoleInPunti(Me.txtCredito.Text.Replace(".", "")) & " * (-1),TO_CHAR(SYSDATE,'yyyymmddHH24miss'))"
                    '' '' ''par.cmd.ExecuteNonQuery()


                    '****Scrittura evento EMISSIONE DEL PAGAMENTO
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vIdCDP & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELENCO_BOLL_CDP (ID_BOLLETTA,ID_CDP,ID_BOLL_GEST,ID_TIPO_MODALITA_PAG,ESTREMI_IBAN_CC) VALUES (" & ID_BOLLETTA & "," & vIdCDP & ",null," & TIPO_PAG & ",'" & par.PulisciStrSql(txtEstremi.Text) & "')"
                    par.cmd.ExecuteNonQuery()

                    Dim NUM_CDP As Long = 0
                    Dim ANNO_CDP As Long = 0

                    par.cmd.CommandText = " select * from SISCOM_MI.PAGAMENTI where id=" & vIdCDP
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        NUM_CDP = par.IfNull(myReader1("PROGR"), 0)
                        ANNO_CDP = par.IfNull(myReader1("ANNO"), 0)
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_DEP_CAUZ (ID_CONTRATTO,DATA_OPERAZIONE,CREDITO,INTERESSI,TIPO_PAGAMENTO,NOTE_PAGAMENTO,DATA_CERT_PAG,NUM_CDP,ANNO_CDP,DATA_MANDATO,NUM_MANDATO,ANNO_MANDATO,ID_BOLLETTA,IBAN_CC) values (" & IDC.Value & ",'" & par.AggiustaData(txtDataOp.Text) & "'," & par.VirgoleInPunti(txtCredito.Text) & "," & par.VirgoleInPunti(txtInteressi.Text) & "," & cmbTipo.SelectedItem.Value & ",'" & par.PulisciStrSql(txtNote.Text) & "','" & par.AggiustaData(txtDataOp.Text) & "','" & NUM_CDP & "','" & ANNO_CDP & "',NULL,NULL,NULL," & ID_BOLLETTA & ",'" & UCase(par.PulisciStrSql(txtEstremi.Text)) & "')"
                    par.cmd.ExecuteNonQuery()

                    Dim vIdSTO As Long = 0
                    par.cmd.CommandText = " select SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.NEXTVAL FROM dual "
                    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderX.Read Then
                        vIdSTO = myReaderX(0)
                    End If
                    myReaderX.Close()

                    If IdBollettaDPC <> 0 Then
                        par.cmd.CommandText = " select * from SISCOM_MI.STORICO_DEP_CAUZIONALE where id_bolletta=" & IdBollettaDPC
                        myReaderX = par.cmd.ExecuteReader()
                        If myReaderX.HasRows = False Then
                            'max 27/09/2017 su indicazione di Daniela,  ricevute da Pellegri, se non esiste una riga nello storico, nons crivere nulla!
                            'par.cmd.CommandText = "INSERT INTO SISCOM_MI.STORICO_DEP_CAUZIONALE (ID, ID_ANAGRAFICA, ID_CONTRATTO, DATA_RESTITUZIONE, IMPORTO_RESTITUZIONE,NOTE) VALUES (" & vIdSTO & "," & par.IfNull(myReaderS("ID_ANAGRAFICA"), "") & "," & IDC.Value & ",'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(txtCredito.Text) & ",'IMPORTO RESTITUTITO NELLA BOLLETTA " & ID_BOLLETTA & "')"
                            'par.cmd.ExecuteNonQuery()
                            'par.cmd.CommandText = "Insert into SISCOM_MI.RAPPORTI_UTENZA_DEP_PROV (ID_CONTRATTO, ID_STORICO_DEP, LIBRO, BOLLA, PROVENIENZA) Values (" & IDC.Value & ", " & vIdSTO & ", NULL, NULL, 1)"
                            'par.cmd.ExecuteNonQuery()
                        Else
                            par.cmd.CommandText = "UPDATE SISCOM_MI.STORICO_DEP_CAUZIONALE SET DATA_RESTITUZIONE='" & Format(Now, "yyyyMMdd") & "',IMPORTO_RESTITUZIONE=" & par.VirgoleInPunti(txtCredito.Text) & " where id_bolletta=" & IdBollettaDPC
                            par.cmd.ExecuteNonQuery()
                        End If
                        myReaderX.Close()
                    Else
                        par.cmd.CommandText = " select * from SISCOM_MI.STORICO_DEP_CAUZIONALE where id_contratto=" & IDC.Value & " and id_anagrafica=" & ID_ANAGRAFICA_CONDUTTORE
                        myReaderX = par.cmd.ExecuteReader()
                        If myReaderX.HasRows = False Then

                        Else
                            par.cmd.CommandText = "UPDATE SISCOM_MI.STORICO_DEP_CAUZIONALE SET DATA_RESTITUZIONE='" & Format(Now, "yyyyMMdd") & "',IMPORTO_RESTITUZIONE=" & par.VirgoleInPunti(txtCredito.Text) & " where id_contratto=" & IDC.Value & " and id_anagrafica=" & ID_ANAGRAFICA_CONDUTTORE
                            par.cmd.ExecuteNonQuery()
                        End If
                        myReaderX.Close()
                    End If

                    '--------------------------

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
                    '                    & " (ID,DATA_ORDINE,DESCRIZIONE,ID_VOCE_PF,ID_FORNITORE,ID_PRENOTAZIONE,ID_PRENOTAZIONE_RIT," _
                    '                    & " PREN_NETTO, CASSA, IVA, RIT_ACCONTO, PREN_NO_IVA, PREN_LORDO,ID_STATO,PENALE,ID_STRUTTURA,CASSA_CONS, IVA_CONS, RIT_ACCONTO_CONS)" _
                    '            & "values ('" & vIdODL & "'," _
                    '            & "'" & par.AggiustaData(txtDataOp.Text) & "', " _
                    '            & "'" & par.PulisciStrSql("RIMBORSO DEPOSITO CAUZIONALE ") & Estremi & "', " _
                    '            & "'" & IdVocePF & "', " _
                    '            & "'" & idFornitore & "', " _
                    '            & "NULL, " _
                    '            & "NULL, " _
                    '            & "'" & strToNumber(Me.txtCredito.Text.Replace(".", "")) & "', " _
                    '            & "'0', " _
                    '            & "'0', " _
                    '            & "'0', " _
                    '            & "NULL, " _
                    '            & "'" & strToNumber(Me.txtCredito.Text.Replace(".", "")) & "', " _
                    '            & "'0', " _
                    '            & "'0', " _
                    '            & "'" & RitornaNullSeIntegerMenoUno(idStruttura) & "', " _
                    '            & "NULL, " _
                    '            & "NULL, " _
                    '            & "NULL)"
                    'par.cmd.ExecuteNonQuery()

                    'par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','')"
                    'par.cmd.ExecuteNonQuery()

                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELENCO_BOLL_ODL (ID_BOLLETTA,ID_ODL,ID_BOLL_GEST,ID_TIPO_MODALITA_PAG,ESTREMI_IBAN_CC) VALUES (" & ID_BOLLETTA & "," & vIdODL & ",null," & TIPO_PAG & ",'" & par.PulisciStrSql(txtEstremi.Text) & "')"
                    'par.cmd.ExecuteNonQuery()

                    'Dim NUM_CDP As Long = 0
                    'Dim ANNO_CDP As Long = 0

                    'par.cmd.CommandText = " select * from SISCOM_MI.ODL where id=" & vIdODL
                    'myReader1 = par.cmd.ExecuteReader()
                    'If myReader1.Read Then
                    '    NUM_CDP = par.IfNull(myReader1("PROGR"), 0)
                    '    ANNO_CDP = par.IfNull(myReader1("ANNO"), 0)
                    'End If
                    'myReader1.Close()

                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_DEP_CAUZ (ID_CONTRATTO,DATA_OPERAZIONE,CREDITO,INTERESSI,TIPO_PAGAMENTO,NOTE_PAGAMENTO,DATA_CERT_PAG,NUM_CDP,ANNO_CDP,DATA_MANDATO,NUM_MANDATO,ANNO_MANDATO,ID_BOLLETTA,IBAN_CC) values (" & IDC.Value & ",'" & par.AggiustaData(txtDataOp.Text) & "'," & par.VirgoleInPunti(txtCredito.Text) & "," & par.VirgoleInPunti(txtInteressi.Text) & "," & cmbTipo.SelectedItem.Value & ",'" & par.PulisciStrSql(txtNote.Text) & "','" & par.AggiustaData(txtDataOp.Text) & "','" & NUM_CDP & "','" & ANNO_CDP & "',NULL,NULL,NULL," & ID_BOLLETTA & ",'" & UCase(par.PulisciStrSql(txtEstremi.Text)) & "')"
                    'par.cmd.ExecuteNonQuery()

                    'IMPOSTO IL FLAG "FL_CAUZ_RESTITUITA" UGUALE A 1 NELLA TABELLA CHE SI CREA PER I CAMBI DELLE TIPOLOGIE CONTRATTUALI
                    par.cmd.CommandText = "update siscom_mi.GESTIONE_CAMBIO_TIPO_CONTR set FL_CAUZ_RESTITUITA =1 where id_contratto_origine=" & IDC.Value
                    par.cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Operazione effettuata!');self.close();</script>")

                End If
                myReaderS.Close()
                'End If
                'myReader.Close()
            Else
                'Response.Write("<script>alert('Operazione NON effettuata! Verificare che nei parametri sia stata definita la voce del BP per il rimborso.');self.close();</script>")

            End If

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblerrore.Text = ex.Message
            lblerrore.Visible = True
        End Try

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

    Public Property ID_ANAGRAFICA_CONDUTTORE() As String
        Get
            If Not (ViewState("par_ID_ANAGRAFICA_CONDUTTORE") Is Nothing) Then
                Return CStr(ViewState("par_ID_ANAGRAFICA_CONDUTTORE"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_ID_ANAGRAFICA_CONDUTTORE") = value
        End Set

    End Property

    Public Property IdBollettaDPC() As Long
        Get
            If Not (ViewState("par_IdBollettaDPC") Is Nothing) Then
                Return CLng(ViewState("par_IdBollettaDPC"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdBollettaDPC") = value
        End Set

    End Property
End Class
