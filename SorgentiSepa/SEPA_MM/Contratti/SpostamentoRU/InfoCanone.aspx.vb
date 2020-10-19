Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports System.Data.OleDb
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Collections.Generic


Partial Class Contratti_SpostamentoRU_InfoCanone
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            txtDataProvv.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            idAnagrafica = Request.QueryString("IDANAG")
            codiceUI = Request.QueryString("CODUI")
            lblTipoContr.Text = Request.QueryString("TIPOUI") & " - cod. unità: " & codiceUI
            CaricaInfoCanone()
        End If
    End Sub

    Public Property idAnagrafica() As Long
        Get
            If Not (ViewState("par_idAnagrafica") Is Nothing) Then
                Return CLng(ViewState("par_idAnagrafica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idAnagrafica") = value
        End Set

    End Property

    Public Property idUNITAnew() As Long
        Get
            If Not (ViewState("par_idUNITAnew") Is Nothing) Then
                Return CLng(ViewState("par_idUNITAnew"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idUNITAnew") = value
        End Set

    End Property


    Public Property CanoneCorrente() As Decimal
        Get
            If Not (ViewState("par_CanoneCorrente") Is Nothing) Then
                Return CDec(ViewState("par_CanoneCorrente"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_CanoneCorrente") = value
        End Set

    End Property

    Public Property codiceUI() As String
        Get
            If Not (ViewState("par_codiceUI") Is Nothing) Then
                Return CStr(ViewState("par_codiceUI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_codiceUI") = value
        End Set

    End Property

    Public Property IdConnessContr() As String
        Get
            If Not (ViewState("par_IdConnessContr") Is Nothing) Then
                Return CStr(ViewState("par_IdConnessContr"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IdConnessContr") = value
        End Set

    End Property

    Public Property Provvedimento() As String
        Get
            If Not (ViewState("par_Provvedimento") Is Nothing) Then
                Return CStr(ViewState("par_Provvedimento"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Provvedimento") = value
        End Set

    End Property

    Public Property dataProvvedimento() As String
        Get
            If Not (ViewState("par_dataProvvedimento") Is Nothing) Then
                Return CStr(ViewState("par_dataProvvedimento"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataProvvedimento") = value
        End Set

    End Property

    Private Sub CaricaInfoCanone()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * from siscom_mi.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                CanoneCorrente = par.IfNull(myReader0("imp_canone_iniziale"), "0")
                par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_CONTRATTO=" & par.IfNull(myReader0("ID"), "")
                Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderX1.Read Then
                    CanoneCorrente = CanoneCorrente + par.IfNull(myReaderX1(0), 0)
                End If
                myReaderX1.Close()
                Provvedimento = par.IfNull(myReader0("delibera"), "")
                dataProvvedimento = par.FormattaData(par.IfNull(myReader0("data_delibera"), ""))
            End If
            myReader0.Close()

            txtCanoneAttuale.Text = CanoneCorrente
            txtNumProvv.Text = Provvedimento
            txtDataProvv.Text = dataProvvedimento

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub InsertUIAssegn()
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE1111", par.OracleConn)

            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)

            Dim nome As String = ""
            Dim cognome As String = ""
            Dim cf_iva As String = ""
            Dim CanoneCorrente As Decimal = 0
            Dim idUI As Long = 0
            Dim codUI As String = ""
            Dim provenienza As String = ""

            Select Case Request.QueryString("TIPOUI")
                Case "ERP"
                    InsertUIAssegnERP()
                Case "USD"
                    provenienza = "U"
                Case "EQC392"
                    provenienza = "Q"
                Case "L43198"
                    provenienza = "W"
                Case "L43198_ART15"
                    provenienza = "D"
                Case "NONE"
                    provenienza = "X"
                Case "10"
                    provenienza = "Z"
                Case "12"
                    provenienza = "A"
            End Select

            codUI = Request.QueryString("CODUI")
            idUI = idUNITAnew

            par.cmd.CommandText = "SELECT * from siscom_mi.ANAGRAFICA where ID=" & idAnagrafica
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                cognome = par.IfNull(myReader0("COGNOME"), "")
                nome = par.IfNull(myReader0("NOME"), "")
                cf_iva = par.IfNull(myReader0("COD_FISCALE"), "")
            End If
            myReader0.Close()


            par.cmd.CommandText = "Insert into siscom_mi.UNITA_ASSEGNATE (ID_DOMANDA, ID_UNITA, DATA_ASSEGNAZIONE, " _
                    & "GENERATO_CONTRATTO, ID_DICHIARAZIONE, COGNOME_RS, NOME, CF_PIVA, PROVENIENZA, N_OFFERTA,CANONE,ID_ANAGRAFICA,PROVVEDIMENTO,DATA_PROVVEDIMENTO) " _
                    & " Values " _
                    & "(-1, " & idUI & ", '" & Format(Now, "yyyymmdd") & "', 0, -1, " _
                    & "'" & par.PulisciStrSql(cognome) & "', '" & par.PulisciStrSql(nome) _
                    & "', '" & par.PulisciStrSql(cf_iva) & "', '" & provenienza & "', 0," & par.VirgoleInPunti(CanoneCorrente) & "," & idAnagrafica & ",'" & Provvedimento & "','" & par.AggiustaData(dataProvvedimento) & "')"
            par.cmd.ExecuteNonQuery()

            If Request.QueryString("TIPOUI") = "USD" Then
                par.cmd.CommandText = "update SISCOM_MI.UI_USI_DIVERSI set stato='8',assegnato='1',DATA_PRENOTATO='" & Format(Now, "yyyymmdd") & "' where COD_ALLOGGIO='" & codUI & "'"
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "update ALLOGGI set stato='8',assegnato='1',DATA_PRENOTATO='" & Format(Now, "yyyymmdd") & "' where COD_ALLOGGIO='" & codUI & "'"
                par.cmd.ExecuteNonQuery()
            End If


            Response.Write("<script>alert('Operazione Effettuata con successo!');</script>")
            btnProcedi.Enabled = False
            txtCanoneAttuale.ReadOnly = True
            txtDataProvv.ReadOnly = True
            txtNumProvv.ReadOnly = True
            btnChiudiContratto.Visible = True
            lblAlert.Text = "Procedere alla chiusura dell'attuale contratto e all'attivazione del nuovo!"

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)
            par.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub InsertUIAssegnERP()
        Try
            par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                & "1,8," _
                                & idUNITAnew & "," _
                                & "''" & ",'SPOSTAMENTO DA UI AD UN''ALTRA')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT * FROM PRODUZIONE_ALLOGGI WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read() = False Then
                par.cmd.CommandText = "INSERT INTO PRODUZIONE_ALLOGGI (DATA,RESI,DISPONIBILI,PRENOTATI,ASSEGNATI,OCCUPATI,RISERVATI) VALUES ('" & Format(Now, "yyyyMMdd") & "',0,0,0,0,0,0)"
                par.cmd.ExecuteNonQuery()
            End If
            myReader1.Close()

            par.cmd.CommandText = "UPDATE PRODUZIONE_ALLOGGI SET PRENOTATI=PRENOTATI-1,ASSEGNATI=ASSEGNATI+1 WHERE DATA='" & Format(Now, "yyyyMMdd") & "'"
            par.cmd.ExecuteNonQuery()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If ConfAssegn.Value = "1" Then
            InsertUIAssegn()
        End If
    End Sub

    Protected Sub btnChiudiContratto_Click(sender As Object, e As System.EventArgs) Handles btnChiudiContratto.Click
        If ConfChiudiApri.Value = "1" Then
            ChiudiCrea()
        End If
    End Sub

    Private Sub ChiudiCrea()
        Try
            Dim idContratto As Long = 0
            Dim codUIold As String = ""
            Dim idUNITAOLD As Long = 0
            Dim dataConsegna As String = ""
            Dim dataRiconsegna As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE1111", par.OracleConn)

            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)

            par.cmd.CommandText = "SELECT * from siscom_mi.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idContratto = par.IfNull(myReader0("ID"), "")
                dataConsegna = par.IfNull(myReader0("DATA_CONSEGNA"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND " _
            & " RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND RAPPORTI_UTENZA.ID=" & idContratto
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                codUIold = par.IfNull(myReader0("COD_UNITA_IMMOBILIARE"), "")
                idUNITAOLD = par.IfNull(myReader0("ID"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUI & "'"
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idUNITAnew = par.IfNull(myReader0("ID"), "")
            End If
            myReader0.Close()


            'IMPOSTO DATA SLOGGIO UGUALE ALLA DATA DI CONSEGNA IN QUANTO L'INQUILINO NON E' MAI ENTRATO
            dataRiconsegna = dataConsegna


            'LIBERO L'UNITA' IMMOBILIARE
            par.cmd.CommandText = "update siscom_mi.unita_immobiliari set cod_tipo_disponibilita='LIBE' where id=" & idUNITAOLD
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "update siscom_mi.unita_immobiliari set cod_tipo_disponibilita='LIBE' where id_unita_principale=" & idUNITAOLD
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE ALLOGGI SET data_disponibilita='" & dataRiconsegna & "',PRENOTATO='0', ASSEGNATO='0', ID_PRATICA=null,stato='5' WHERE COD_ALLOGGIO='" & codUIold & "'"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE siscom_mi.ui_usi_diversi SET data_disponibilita='" & dataRiconsegna & "',PRENOTATO='0', ASSEGNATO='0', ID_PRATICA=null,stato='5' WHERE COD_ALLOGGIO='" & codUIold & "'"
            par.cmd.ExecuteNonQuery()


            If Not IsNothing(Session.Item("lIdConnessione")) Then
                Dim par1 As New CM.Global
                par1.OracleConn = CType(HttpContext.Current.Session.Item(Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleConnection)
                par1.cmd = par1.OracleConn.CreateCommand()
                par1.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleTransaction)
                ‘'par1.cmd.Transaction = par1.myTrans

                par1.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET DATA_RICONSEGNA = '" & dataRiconsegna & "' where ID=" & idContratto
                par1.cmd.ExecuteNonQuery()

                par1.myTrans.Commit()
                par1.myTrans = par1.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & Session.Item("lIdConnessione"), par1.myTrans)
                par1.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Dim note As String = ""

            par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO='" & idContratto & "' AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0))"
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable
            da1.Fill(dt1)
            da1.Dispose()
            For Each row As Data.DataRow In dt1.Rows

                note = "(storno) " & row.Item("NOTE")

                par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE ( ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO," _
                & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, FL_ANNULLATA," _
                & "PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA," _
                & "RIFERIMENTO_A, FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, ANNO," _
                & "OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG, RIF_FILE, RIF_BOLLETTINO," _
                & "RIF_FILE_TXT, DATA_VALUTA, DATA_VALUTA_CREDITORE, RIF_CONTABILE, RIF_FILE_RENDICONTO, DATA_ANNULLO," _
                & "FL_INCASSI, DATA_MORA, IMPORTO_TOTALE, NUM_BOLLETTA, ID_MOROSITA, ID_TIPO, ID_BOLLETTA_RIC," _
                & "FL_SOLLECITO, IMP_TMP, ID_RATEIZZAZIONE, IMP_PAGATO_OLD, QUOTA_SIND_B, IMPORTO_RIC_B," _
                & "QUOTA_SIND_PAGATA_B, IMPORTO_RIC_PAGATO_B, IMPORTO_RICLASSIFICATO, IMPORTO_RICLASSIFICATO_PAGATO," _
                & "FL_PAG_PARZ, ID_MOROSITA_LETTERA, FL_PAG_MAV, ID_STATO ) VALUES ( " _
                & "SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") & "', '', '', '', '', '" & par.PulisciStrSql(note) & "', " & idContratto & "" _
                & ", " & par.IfNull(row.Item("ID_ESERCIZIO_F"), "") & ", " & row.Item("ID_UNITA") & ", '0','" & par.IfNull(row.Item("PAGABILE_PRESSO"), "") & "'," & par.IfNull(row.Item("COD_AFFITTUARIO"), "") & ", '" & par.PulisciStrSql(row.Item("INTESTATARIO")) & "', '" & par.PulisciStrSql(par.IfNull(row.Item("INDIRIZZO"), "")) & "', '" & par.PulisciStrSql(par.IfNull(row.Item("CAP_CITTA"), "")) & "'" _
                & ", '" & par.IfNull(row.Item("PRESSO"), "") & "', '" & Format(Now, "yyyyMMdd") & "', '" & Format(Now, "yyyyMMdd") & "', '" & par.IfNull(row.Item("FL_STAMPATO"), "") & "', " & par.IfNull(row.Item("ID_COMPLESSO"), "") & ", '', " & par.IfNull(row.Item("IMPORTO_PAGATO"), "NULL") & ", '', " & par.IfNull(row.Item("ANNO"), "") & ", '', " & par.IfNull(row.Item("ID_EDIFICIO"), "") & "" _
                & ", '', '', '" & par.IfNull(row.Item("RIF_FILE"), "") & "', '" & par.IfNull(row.Item("RIF_BOLLETTINO"), "") & "', '" & par.IfNull(row.Item("RIF_FILE_TXT"), "") & "', '', '', '" & par.IfNull(row.Item("RIF_CONTABILE"), "") & "', '" & par.IfNull(row.Item("RIF_FILE_RENDICONTO"), "") & "', '', " & par.IfNull(row.Item("FL_INCASSI"), "") & ", '', " & par.VirgoleInPunti((row.Item("IMPORTO_TOTALE") * (-1))) & ", '" & row.Item("NUM_BOLLETTA") & "'" _
                & ", " & par.IfNull(row.Item("ID_MOROSITA"), "NULL") & ", 22, " & par.IfNull(row.Item("ID_BOLLETTA_RIC"), "NULL") & "," & par.IfNull(row.Item("FL_SOLLECITO"), "NULL") & "," & par.IfNull(row.Item("IMP_TMP"), "NULL") & "," & par.IfNull(row.Item("ID_RATEIZZAZIONE"), "NULL") & ", " & par.IfNull(row.Item("IMP_PAGATO_OLD"), "NULL") & "," & par.IfNull(row.Item("QUOTA_SIND_B"), "NULL") & "," & par.IfNull(row.Item("IMPORTO_RIC_B"), "NULL") & "," _
                & par.IfNull(row.Item("QUOTA_SIND_PAGATA_B"), "NULL") & "," & par.IfNull(row.Item("IMPORTO_RIC_PAGATO_B"), "NULL") & "," & par.IfNull(row.Item("IMPORTO_RICLASSIFICATO"), "NULL") & "," & par.IfNull(row.Item("IMPORTO_RICLASSIFICATO_PAGATO"), "NULL") & "," & par.IfNull(row.Item("FL_PAG_PARZ"), "NULL") & "," & par.IfNull(row.Item("ID_MOROSITA_LETTERA"), "NULL") & "," & par.IfNull(row.Item("FL_PAG_MAV"), "NULL") & "," & par.IfNull(row.Item("ID_STATO"), "NULL") & ")"
                par.cmd.ExecuteNonQuery()

                Dim ID_BOLLETTA_NEW As Long = 0
                par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    ID_BOLLETTA_NEW = myReaderA(0)
                End If
                myReaderA.Close()

                par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.* from siscom_mi.BOL_BOLLETTE,siscom_mi.BOL_BOLLETTE_VOCI where BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND ID_CONTRATTO=" & idContratto & " and id_bolletta= " & row.Item("ID")
                Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt3 As New Data.DataTable
                da3.Fill(dt3)
                da3.Dispose()
                For Each row3 As Data.DataRow In dt3.Rows
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,NOTE) VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & "," & row3.Item("ID_VOCE") & "," & par.VirgoleInPunti(row3.Item("IMPORTO") * (-1)) & ",'STORNO')"
                    par.cmd.ExecuteNonQuery()
                Next

                par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set ID_BOLLETTA_STORNO=" & ID_BOLLETTA_NEW & ",FL_STAMPATO='1',DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "',DATA_VALUTA='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & row.Item("ID")
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set DATA_PAGAMENTO='" & Format(Now, "yyyyMMdd") & "',DATA_VALUTA='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & ID_BOLLETTA_NEW
                par.cmd.ExecuteNonQuery()
            Next

            
            par.cmd.CommandText = "SELECT cond_edifici.* FROM SISCOM_MI.COND_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & idUNITAOLD & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND COND_EDIFICI.ID_EDIFICIO=EDIFICI.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO,ID_CONTRATTO) VALUES (1," & idUNITAOLD & ",'" & Format(Now, "yyyyMMdd") & "',0," & myReader("ID_CONDOMINIO") & "," & idContratto & ")"
                par.cmd.ExecuteNonQuery()
            Loop
            myReader.Close()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO,ID_CONTRATTO) VALUES (1," & idUNITAnew & ",'" & Format(Now, "yyyyMMdd") & "','0'," & myReader("ID_CONDOMINIO") & "," & idContratto & ")"
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & idContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F18','')"
            par.cmd.ExecuteNonQuery()


            '********* CREAZIONE NUOVO CONTRATTO *********
            Dim idEdificio As Long = 0
            Dim idComplesso As Long = 0
            Dim via As String = ""
            Dim civico As String = ""
            Dim cap As String = ""
            Dim luogo As String = ""

            par.cmd.CommandText = "select unita_immobiliari.id_edificio,edifici.id_complesso,indirizzi.* from siscom_mi.unita_immobiliari,siscom_mi.edifici,siscom_mi.indirizzi where indirizzi.id = unita_immobiliari.id_indirizzo and edifici.id =unita_immobiliari.id_edificio and unita_immobiliari.id=" & idUNITAnew
            Dim myReaderX2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX2.Read Then
                idEdificio = par.IfNull(myReaderX2("id_edificio"), "")
                idComplesso = par.IfNull(myReaderX2("id_complesso"), "")
                via = par.IfNull(myReaderX2("descrizione"), "")
                civico = par.IfNull(myReaderX2("civico"), "")
                cap = par.IfNull(myReaderX2("cap"), "")
                luogo = par.IfNull(myReaderX2("localita"), "")
            End If
            myReaderX2.Close()


            Dim progressivo As Integer
            par.cmd.CommandText = "select MAX(TO_NUMBER(TRANSLATE(SUBSTR(cod_contratto,18,2),'A','0'))) from SISCOM_MI.rapporti_utenza where id=" & idContratto
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                progressivo = par.IfNull(myReader1(0), 0) + 1
            Else
                progressivo = 1
            End If
            myReader1.Close()
            Dim NuovoCodiceContratto As String = Mid(Request.QueryString("COD"), 1, 17) & Format((progressivo), "00")

            Dim proxBolletta As String = ""
            Dim idDomanda As String = ""
            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.*,RAPPORTI_UTENZA_PROSSIMA_BOL.PROSSIMA_BOLLETTA FROM SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL,SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA_PROSSIMA_BOL.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID=" & idContratto
            Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX.Read = True Then
                par.cmd.CommandText = "Insert into SISCOM_MI.RAPPORTI_UTENZA    (ID, COD_CONTRATTO_GIMI, COD_CONTRATTO, COD_TIPOLOGIA_RAPP_CONTR, COD_TIPOLOGIA_CONTR_LOC," _
                                    & "DURATA_ANNI, DURATA_MESI, DURATA_GIORNI, DATA_DECORRENZA, DATA_SCADENZA,     DATA_DISDETTA_LOCATARIO, NUM_REGISTRAZIONE, SERIE_REGISTRAZIONE, " _
                                    & "IMP_CANONE_INIZIALE, IMP_DEPOSITO_CAUZ,     COD_FASCIA_REDDITO, MESSA_IN_MORA, PRATICA_AL_LEGALE, ISCRIZIONE_RUOLO, RATEIZZAZIONI_IN_CORSO,     " _
                                    & "DECADENZA, SFRATTO, NOTE, DATA_REG, COD_UFFICIO_REG,     DATA_STIPULA, DATA_CONSEGNA, DATA_SCADENZA_RINNOVO, DURATA_RINNOVO, MESI_DISDETTA,     " _
                                    & "DATA_RICONSEGNA, DELIBERA, DATA_DELIBERA, NRO_RATE, FREQ_VAR_ISTAT,     VERSAMENTO_TR, PER_BANDO, TIPO_COR, LUOGO_COR, VIA_COR,     " _
                                    & "NOTE_COR, CIVICO_COR, SIGLA_COR, CAP_COR, PRESSO_COR,     BOZZA, INTERESSI_CAUZIONE, NRO_REPERTORIO, DATA_REPERTORIO, NRO_ASSEGNAZIONE_PG,     " _
                                    & "DATA_ASSEGNAZIONE_PG, INIZIO_PERIODO, LIBRETTO_DEPOSITO, ID_DEST_RATE, INVIO_BOLLETTA,     FL_CONGUAGLIO, PERC_RINNOVO_CONTRATTO, PERC_ISTAT, " _
                                    & "IMP_CANONE_ATTUALE, PROVENIENZA_ASS,     INTERESSI_RIT_PAG, IMPORTO_ANTICIPO, ID_DOMANDA, ID_AU, ID_ISEE,     ID_COMMISSARIATO, REG_TELEMATICA, " _
                                    & "DEST_USO, DESCR_DEST_USO, DATA_INVIO_RIC_DISDETTA,MOTIVO_REC_FORZOSO, FL_STAMPATO, BOLLO, N_OFFERTA, DATA_NOTIFICA_DISDETTA,     " _
                                    & "MITTENTE_DISDETTA, DATA_CONVALIDA_SFRATTO, DATA_ESECUZIONE_SFRATTO, DATA_RINVIO_SFRATTO, DATA_CONFERMA_FP) " _
                                    & "Values   (SISCOM_MI.SEQ_RAPPORTI_UTENZA.NEXTVAL, '', '" & NuovoCodiceContratto & "', '" _
                                    & par.IfNull(myReaderX("COD_TIPOLOGIA_RAPP_CONTR"), "") & "', '" & par.IfNull(myReaderX("COD_TIPOLOGIA_CONTR_LOC"), "") _
                                    & "', " & par.IfNull(myReaderX("DURATA_ANNI"), "0") & ", " & par.IfNull(myReaderX("DURATA_MESI"), "0") _
                                    & ", NULL, '', '',  '' , NULL, NULL, " & par.VirgoleInPunti(txtCanoneAttuale.Text) & ",0,  NULL, NULL, NULL, NULL, NULL,  NULL, NULL, NULL, NULL, '" _
                                    & "TNP',  '', '', '', " & par.IfNull(myReaderX("DURATA_RINNOVO"), "NULL") _
                                    & ", " & par.IfNull(myReaderX("MESI_DISDETTA"), "6") & ",     NULL, '" & txtNumProvv.Text _
                                    & "', '" & par.AggiustaData(txtDataProvv.Text) & "', 12, '" & par.IfNull(myReaderX("FREQ_VAR_ISTAT"), "0") _
                                    & "' ,'" & par.IfNull(myReaderX("VERSAMENTO_TR"), "NULL") & "', NULL, '" & par.IfNull(myReaderX("TIPO_COR"), "VIA") _
                                    & "', '" & par.PulisciStrSql(luogo) & "', '" _
                                    & par.PulisciStrSql(via) _
                                    & "', NULL, '" & par.PulisciStrSql(civico) & "', '" _
                                    & par.PulisciStrSql(par.IfNull(myReaderX("SIGLA_COR"), "")) & "', '" _
                                    & par.PulisciStrSql(cap) & "', '" _
                                    & par.PulisciStrSql(par.IfNull(myReaderX("PRESSO_COR"), "")) & "',     1, NULL, NULL, NULL, NULL,     NULL, '" _
                                    & par.IfNull(myReaderX("PROSSIMA_BOLLETTA"), "") & "', '" & par.IfNull(myReaderX("LIBRETTO_DEPOSITO"), "") _
                                    & "', " & par.IfNull(myReaderX("ID_DEST_RATE"), "1") & ", " & par.IfNull(myReaderX("INVIO_BOLLETTA"), "1") _
                                    & ",'" & par.IfNull(myReaderX("FL_CONGUAGLIO"), "1") & "', " & par.IfNull(myReaderX("PERC_RINNOVO_CONTRATTO"), "0") _
                                    & ", " & par.IfNull(myReaderX("PERC_ISTAT"), "0") & ", " & par.VirgoleInPunti(txtCanoneAttuale.Text) _
                                    & ", " & par.IfNull(myReaderX("PROVENIENZA_ASS"), "0") & " ,     '0', 0, " & par.IfNull(myReaderX("ID_DOMANDA"), "NULL") _
                                    & ", " & par.IfNull(myReaderX("ID_AU"), "NULL") & ", " & par.IfNull(myReaderX("ID_ISEE"), "NULL") _
                                    & "," & par.IfNull(myReaderX("ID_COMMISSARIATO"), "1") & ", NULL, '" & par.IfNull(myReaderX("DEST_USO"), "N") _
                                    & "', '" & par.PulisciStrSql(par.IfNull(myReaderX("DESCR_DEST_USO"), "0")) _
                                    & "', NULL, 4, 0, NULL, NULL, NULL, -1, NULL, NULL, NULL, NULL)"
                par.cmd.ExecuteNonQuery()

                proxBolletta = par.IfNull(myReaderX("PROSSIMA_BOLLETTA"), "")
                idDomanda = par.IfNull(myReaderX("ID_DOMANDA"), "")
            End If
            myReaderX.Close()

            Dim INDICE_CONTRATTO As Long = 0

            par.cmd.CommandText = "select siscom_mi.seq_RAPPORTI_UTENZA.currval from dual"
            myReaderX2 = par.cmd.ExecuteReader()
            If myReaderX2.Read Then
                INDICE_CONTRATTO = myReaderX2(0)
            End If
            myReaderX2.Close()


            par.cmd.CommandText = "update SISCOM_MI.unita_assegnate set generato_contratto=1 where id_unita=" & idUNITAnew
            par.cmd.ExecuteNonQuery()


            'IMPORTO LE TRE BOLLETTE DI PRE-ATTIVAZIONE
            par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO=" & idContratto & " AND ID<2003660 AND (FL_ANNULLATA=0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0))"
            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt2 As New Data.DataTable
            da2.Fill(dt2)
            da2.Dispose()
            For Each row As Data.DataRow In dt2.Rows

                note = row.Item("NOTE")

                par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE ( ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO," _
                & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, FL_ANNULLATA," _
                & "PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA," _
                & "RIFERIMENTO_A, FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, ANNO," _
                & "OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG, RIF_FILE, RIF_BOLLETTINO," _
                & "RIF_FILE_TXT, DATA_VALUTA, DATA_VALUTA_CREDITORE, RIF_CONTABILE, RIF_FILE_RENDICONTO, DATA_ANNULLO," _
                & "FL_INCASSI, DATA_MORA, IMPORTO_TOTALE, NUM_BOLLETTA, ID_MOROSITA, ID_TIPO, ID_BOLLETTA_RIC," _
                & "FL_SOLLECITO, IMP_TMP, ID_RATEIZZAZIONE, IMP_PAGATO_OLD, QUOTA_SIND_B, IMPORTO_RIC_B," _
                & "QUOTA_SIND_PAGATA_B, IMPORTO_RIC_PAGATO_B, IMPORTO_RICLASSIFICATO, IMPORTO_RICLASSIFICATO_PAGATO," _
                & "FL_PAG_PARZ, ID_MOROSITA_LETTERA, FL_PAG_MAV, ID_STATO ) VALUES ( " _
                & "SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") & "', '', '', '', '', '" & par.PulisciStrSql(note) & "', " & INDICE_CONTRATTO & "" _
                & ", " & par.IfNull(row.Item("ID_ESERCIZIO_F"), "") & ", " & idUNITAnew & ", '0',''," & par.IfNull(row.Item("COD_AFFITTUARIO"), "") & ", '" & par.PulisciStrSql(row.Item("INTESTATARIO")) & "', '" & par.PulisciStrSql(via & ", " & civico) & "', '" & par.PulisciStrSql(cap) & "'" _
                & ", '" & par.IfNull(row.Item("PRESSO"), "") & "', '" & Format(Now, "yyyyMMdd") & "', '" & Format(Now, "yyyyMMdd") & "', '" & par.IfNull(row.Item("FL_STAMPATO"), "") & "', " & idComplesso & ", '', " & par.IfNull(row.Item("IMPORTO_PAGATO"), "NULL") & ", '', " & par.IfNull(row.Item("ANNO"), "NULL") & ", '', " & idEdificio & "" _
                & ", '', '', '" & par.IfNull(row.Item("RIF_FILE"), "") & "', '" & par.IfNull(row.Item("RIF_BOLLETTINO"), "") & "', '" & par.IfNull(row.Item("RIF_FILE_TXT"), "") & "', '', '', '" & par.IfNull(row.Item("RIF_CONTABILE"), "") & "', '" & par.IfNull(row.Item("RIF_FILE_RENDICONTO"), "") & "', '', " & par.IfNull(row.Item("FL_INCASSI"), "NULL") & ", '', " & par.VirgoleInPunti((row.Item("IMPORTO_TOTALE") * (-1))) & ", '" & row.Item("NUM_BOLLETTA") & "'" _
                & ", " & par.IfNull(row.Item("ID_MOROSITA"), "NULL") & ", " & par.IfNull(row.Item("ID_TIPO"), "") & ", " & par.IfNull(row.Item("ID_BOLLETTA_RIC"), "NULL") & "," & par.IfNull(row.Item("FL_SOLLECITO"), "NULL") & "," & par.IfNull(row.Item("IMP_TMP"), "NULL") & "," & par.IfNull(row.Item("ID_RATEIZZAZIONE"), "NULL") & ", " & par.IfNull(row.Item("IMP_PAGATO_OLD"), "NULL") & "," & par.IfNull(row.Item("QUOTA_SIND_B"), "NULL") & "," & par.IfNull(row.Item("IMPORTO_RIC_B"), "NULL") & "," _
                & par.IfNull(row.Item("QUOTA_SIND_PAGATA_B"), "NULL") & "," & par.IfNull(row.Item("IMPORTO_RIC_PAGATO_B"), "NULL") & "," & par.IfNull(row.Item("IMPORTO_RICLASSIFICATO"), "NULL") & "," & par.IfNull(row.Item("IMPORTO_RICLASSIFICATO_PAGATO"), "NULL") & "," & par.IfNull(row.Item("FL_PAG_PARZ"), "NULL") & "," & par.IfNull(row.Item("ID_MOROSITA_LETTERA"), "NULL") & "," & par.IfNull(row.Item("FL_PAG_MAV"), "NULL") & "," & par.IfNull(row.Item("ID_STATO"), "NULL") & ")"
                par.cmd.ExecuteNonQuery()
            Next

            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.* from siscom_mi.BOL_BOLLETTE,siscom_mi.BOL_BOLLETTE_VOCI where BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND ID_CONTRATTO=" & idContratto
            Dim da4 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt4 As New Data.DataTable
            da4.Fill(dt4)
            da4.Dispose()
            For Each row4 As Data.DataRow In dt4.Rows
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & row4.Item("ID") & "," & row4.Item("ID_VOCE") & "," & par.VirgoleInPunti(row4.Item("IMPORTO")) & ")"
                par.cmd.ExecuteNonQuery()
            Next

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL (ID_CONTRATTO,PROSSIMA_BOLLETTA) VALUES (" & INDICE_CONTRATTO & ",'" & proxBolletta & "')"
            par.cmd.ExecuteNonQuery()

            If Request.QueryString("TIPOUI") = "ERP" Then
                If idDomanda <> 0 Then

                    Dim s As String
                    Dim comunicazioni As String = ""
                    Dim nomeFileCanone As String = ""
                    Dim fileName As String = NuovoCodiceContratto & ".txt"

                    s = par.CalcolaCanone27(idDomanda, 1, idUNITAnew, NuovoCodiceContratto, CanoneCorrente, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)

                    If comunicazioni <> "" Then
                        Response.Write("<script>alert('" & comunicazioni & "');</script>")
                    End If


                    Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeCanoni27\") & fileName, False, System.Text.Encoding.Default)
                    sr.WriteLine(s)
                    sr.Close()
                    nomeFileCanone = Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeCanoni27\") & fileName


                    If System.IO.File.Exists(nomeFileCanone) = True Then

                        Dim sr1 As StreamReader = New StreamReader(nomeFileCanone, System.Text.Encoding.GetEncoding("iso-8859-1"))
                        Dim contenuto As String = sr1.ReadToEnd()
                        sr1.Close()

                        If sNOTE = "" Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC (CANONE_CLASSE,CANONE_SOPPORTABILE,DECADENZA_ALL_ADEGUATO,DECADENZA_VAL_ICI,CANONE_MINIMO_AREA,VALORE_LOCATIVO,SUP_ACCESSORI,MINORI_15,MAGGIORI_65,DATA_STIPULA,COD_CONTRATTO,ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI, LIMITE_PENSIONI, " _
                                                & "ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,TESTO,NOTE,ID_BANDO_AU,DEM, SUPCONVENZIONALE, COSTOBASE, ZONA, PIANO, CONSERVAZIONE, VETUSTA,INCIDENZA_ISE," _
                                                & "COEFF_NUCLEO_FAM,SOTTO_AREA,ANNOTAZIONI,PATRIMONIO_SUP,NON_RISPONDENTE,LIMITE_ISEE,ID_DICHIARAZIONE,CANONE_ATTUALE,ADEGUAMENTO,ISTAT," _
                                                & "NUM_COMP,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_PREV_DIP,DETRAZIONI,REDD_MOBILIARI,REDD_IMMOBILIARI,REDD_COMPLESSIVO,DETRAZIONI_FRAGILITA,ANNO_COSTRUZIONE," _
                                                & "LOCALITA,PRESENTE_ASCENSORE,NUMERO_PIANO,SUP_NETTA,ALTRE_SUP,PERC_ISTAT_APPLICATA,CANONE_CLASSE_ISTAT,CANONE_91,INIZIO_VALIDITA_CAN,FINE_VALIDITA_CAN,TIPO_PROVENIENZA) " _
                                                & "VALUES ('" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) & "'," & sMINORI15 & "," & sMAGGIORI65 & ",'','" & NuovoCodiceContratto & "'," & INDICE_CONTRATTO & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                                & "'," & AreaEconomica & ",'" & sISEE & "','" & sISE & "','" & sISR & "','" & sISP & "','" & sPSE & "','" & sVSE & "','" & sREDD_DIP & "','" _
                                                & sREDD_ALT & "','" & sLimitePensione & "','" & sISE_MIN & "','" & sPER_VAL_LOC & "','" & sPERC_INC_MAX_ISE_ERP & "','" & sCanone & "',:TESTO,'" _
                                                & par.PulisciStrSql(sNOTE) & "',NULL,'" & sDEM & "','" & sSUPCONVENZIONALE & "','" & sCOSTOBASE & "','" & sZONA & "','" & sPIANO & "','" _
                                                & sCONSERVAZIONE & "','" & sVETUSTA & "','" & sINCIDENZAISE & "','" & sCOEFFFAM & "','" & sSOTTOAREA & "','" & sMOTIVODECADENZA & "'," _
                                                & "0" & ",0," & "0" & "," & "null" _
                                                & ",'" & "0" & "','" & "0" & "','" _
                                                & "0" & "'," & sNUMCOMP & "," & sNUMCOMP66 & "," & sNUMCOMP100 & "," & sNUMCOMP100C & "," & sPREVDIP _
                                                & ",'" & sDETRAZIONI & "','" & sMOBILIARI & "','" & sIMMOBILIARI & "','" & sCOMPLESSIVO & "','" & sDETRAZIONEF & "','" & sANNOCOSTRUZIONE & "','" & par.PulisciStrSql(sLOCALITA) _
                                                & "','" & sASCENSORE & "','" & par.PulisciStrSql(sDESCRIZIONEPIANO) & "','" & sSUPNETTA & "','" & par.PulisciStrSql(sALTRESUP) & "','" & sISTAT & "','" & sCANONECLASSEISTAT & "','0','" & sANNOINIZIOVAL & "','" & sANNOFINEVAL & "',4) "
                        Else
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC (CANONE_CLASSE,CANONE_SOPPORTABILE,DECADENZA_ALL_ADEGUATO,DECADENZA_VAL_ICI,CANONE_MINIMO_AREA,VALORE_LOCATIVO,SUP_ACCESSORI,MINORI_15,MAGGIORI_65,DATA_STIPULA,COD_CONTRATTO,ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI," _
                                                & "LIMITE_PENSIONI, ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,TESTO,NOTE,ID_BANDO_AU,ANNOTAZIONI,PATRIMONIO_SUP,NON_RISPONDENTE,LIMITE_ISEE,ID_DICHIARAZIONE," _
                                                & "CANONE_ATTUALE,ADEGUAMENTO,ISTAT,NUM_COMP,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_PREV_DIP,DETRAZIONI,REDD_MOBILIARI,REDD_IMMOBILIARI," _
                                                & "REDD_COMPLESSIVO,DETRAZIONI_FRAGILITA,ANNO_COSTRUZIONE,LOCALITA,PRESENTE_ASCENSORE,NUMERO_PIANO,SUP_NETTA,ALTRE_SUP,PERC_ISTAT_APPLICATA,CANONE_CLASSE_ISTAT,CANONE_91,INIZIO_VALIDITA_CAN,FINE_VALIDITA_CAN,TIPO_PROVENIENZA) " _
                                                & "VALUES ('" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) & "'," & sMINORI15 & "," & sMAGGIORI65 & ",'','" & NuovoCodiceContratto & "'," & INDICE_CONTRATTO & ",'" & Format(Now, "yyyyMMddHHmmss") & "',NULL,'','','','','','','','','','','','','',:TESTO,'" & _
                                                par.PulisciStrSql(sNOTE) & "',NULL,'" & sMOTIVODECADENZA & "',0,0,0," _
                                                & "null" & ",'0','0" _
                                                & "','0'," & sNUMCOMP & "," & sNUMCOMP66 & "," & sNUMCOMP100 & "," & sNUMCOMP100C & "," & sPREVDIP _
                                                & ",'" & sDETRAZIONI & "','" & sMOBILIARI & "','" & sIMMOBILIARI & "','" & sCOMPLESSIVO & "','" & sDETRAZIONEF & "','" & sANNOCOSTRUZIONE & "','" _
                                                & par.PulisciStrSql(sLOCALITA) & "','" & sASCENSORE & "','" & par.PulisciStrSql(sDESCRIZIONEPIANO) & "','" & sSUPNETTA & "','" & par.PulisciStrSql(sALTRESUP) & "','" & sISTAT & "','" & sCANONECLASSEISTAT & "','0','" & sANNOINIZIOVAL & "','" & sANNOFINEVAL & "',4) "
                        End If

                        Dim objStream As Stream = File.Open(nomeFileCanone, FileMode.Open)
                        Dim buffer(objStream.Length) As Byte
                        objStream.Read(buffer, 0, objStream.Length)
                        objStream.Close()

                        Dim parmData As New Oracle.DataAccess.Client.OracleParameter
                        With parmData
                            .Direction = Data.ParameterDirection.Input
                            .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Blob
                            .ParameterName = "TESTO"
                            .Value = buffer
                        End With

                        par.cmd.Parameters.Add(parmData)
                        par.cmd.ExecuteNonQuery()
                        System.IO.File.Delete(nomeFileCanone)
                        par.cmd.Parameters.Remove(parmData)

                        buffer = Nothing
                        objStream = Nothing
                    End If
                End If
            End If

            Dim CAUZIONE As Double = 0
            CAUZIONE = Format(Format((txtCanoneAttuale.Text / 12), "0.00") * 3, "0.00")

            par.cmd.CommandText = "update siscom_mi.rapporti_utenza set imp_canone_iniziale=" & par.VirgoleInPunti(CanoneCorrente) & ",IMP_DEPOSITO_CAUZ=" & par.VirgoleInPunti(CAUZIONE) & " WHERE ID=" & INDICE_CONTRATTO
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO=" & idContratto
            myReaderX = par.cmd.ExecuteReader()
            Do While myReaderX.Read
                If par.IfNull(myReaderX("ID_ANAGRAFICA"), "0") = idAnagrafica Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SOGGETTI_CONTRATTUALI (ID_ANAGRAFICA,ID_CONTRATTO,COD_TIPOLOGIA_PARENTELA,COD_TIPOLOGIA_OCCUPANTE,COD_TIPOLOGIA_TITOLO) VALUES (" _
                    & idAnagrafica & "," & INDICE_CONTRATTO & ",'" & par.IfNull(myReaderX("COD_TIPOLOGIA_PARENTELA"), "1") _
                    & "','INTE','" & par.IfNull(myReaderX("COD_TIPOLOGIA_TITOLO"), "") & "')"
                ElseIf par.IfNull(myReaderX("COD_TIPOLOGIA_OCCUPANTE"), "") = "INTE" Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SOGGETTI_CONTRATTUALI (ID_ANAGRAFICA,ID_CONTRATTO,COD_TIPOLOGIA_PARENTELA,COD_TIPOLOGIA_OCCUPANTE,COD_TIPOLOGIA_TITOLO) VALUES (" _
                    & par.IfNull(myReaderX("ID_ANAGRAFICA"), "0") & "," & INDICE_CONTRATTO & ",'" & par.IfNull(myReaderX("COD_TIPOLOGIA_PARENTELA"), "1") _
                    & "','VOLT','" & par.IfNull(myReaderX("COD_TIPOLOGIA_TITOLO"), "") & "')"
                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SOGGETTI_CONTRATTUALI (ID_ANAGRAFICA,ID_CONTRATTO,COD_TIPOLOGIA_PARENTELA,COD_TIPOLOGIA_OCCUPANTE,COD_TIPOLOGIA_TITOLO) VALUES (" _
                    & par.IfNull(myReaderX("ID_ANAGRAFICA"), "0") & "," & INDICE_CONTRATTO & ",'" & par.IfNull(myReaderX("COD_TIPOLOGIA_PARENTELA"), "1") _
                    & "','" & par.IfNull(myReaderX("COD_TIPOLOGIA_OCCUPANTE"), "") & "','" & par.IfNull(myReaderX("COD_TIPOLOGIA_TITOLO"), "") & "')"
                End If
                par.cmd.ExecuteNonQuery()
            Loop
            myReaderX.Close()


            par.cmd.CommandText = "SELECT SCALE_EDIFICI.DESCRIZIONE AS ""SCALAUNITA"",UNITA_IMMOBILIARI.*,IDENTIFICATIVI_CATASTALI.FOGLIO,IDENTIFICATIVI_CATASTALI.SEZIONE," _
                    & "IDENTIFICATIVI_CATASTALI.NUMERO,IDENTIFICATIVI_CATASTALI.SUB,IDENTIFICATIVI_CATASTALI.COD_TIPOLOGIA_CATASTO," _
                    & "IDENTIFICATIVI_CATASTALI.RENDITA,IDENTIFICATIVI_CATASTALI.COD_CATEGORIA_CATASTALE " _
                    & ",IDENTIFICATIVI_CATASTALI.COD_CLASSE_CATASTALE,IDENTIFICATIVI_CATASTALI.COD_QUALITA_CATASTALE, " _
                    & "IDENTIFICATIVI_CATASTALI.SUPERFICIE_MQ,IDENTIFICATIVI_CATASTALI.CUBATURA,IDENTIFICATIVI_CATASTALI.NUM_VANI" _
                    & ",IDENTIFICATIVI_CATASTALI.SUPERFICIE_CATASTALE,IDENTIFICATIVI_CATASTALI.RENDITA_STORICA," _
                    & "IDENTIFICATIVI_CATASTALI.IMMOBILE_STORICO,IDENTIFICATIVI_CATASTALI.VALORE_IMPONIBILE" _
                    & ",IDENTIFICATIVI_CATASTALI.DATA_ACQUISIZIONE,IDENTIFICATIVI_CATASTALI.DATA_FINE_VALIDITA" _
                    & ",IDENTIFICATIVI_CATASTALI.DITTA,IDENTIFICATIVI_CATASTALI.NUM_PARTITA" _
                    & ",IDENTIFICATIVI_CATASTALI.ESENTE_ICI,IDENTIFICATIVI_CATASTALI.PERC_POSSESSO" _
                    & ",IDENTIFICATIVI_CATASTALI.INAGIBILE,IDENTIFICATIVI_CATASTALI.MICROZONA_CENSUARIA" _
                    & ",IDENTIFICATIVI_CATASTALI.ZONA_CENSUARIA,IDENTIFICATIVI_CATASTALI.COD_STATO_CATASTALE" _
                    & ",INDIRIZZI.DESCRIZIONE AS ""IND"",INDIRIZZI.CIVICO,INDIRIZZI.CAP,INDIRIZZI.LOCALITA,INDIRIZZI.COD_COMUNE" _
                    & " FROM SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.IDENTIFICATIVI_CATASTALI " _
                    & "WHERE UNITA_IMMOBILIARI.ID_SCALA=SCALE_EDIFICI.ID (+) AND INDIRIZZI.ID=unita_immobiliari.id_indirizzo AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND UNITA_IMMOBILIARI.ID=" & idUNITAnew
            Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderD.Read Then
                par.cmd.CommandText = "Insert into SISCOM_MI.UNITA_CONTRATTUALE (ID_CONTRATTO, ID_UNITA, COD_UNITA_IMMOBILIARE, TIPOLOGIA, ID_EDIFICIO, " _
                                    & "SCALA, COD_TIPO_LIVELLO_PIANO, INTERNO, ID_UNITA_PRINCIPALE, SEZIONE, FOGLIO, NUMERO, SUB, COD_TIPOLOGIA_CATASTO, RENDITA, " _
                                    & "COD_CATEGORIA_CATASTALE, COD_CLASSE_CATASTALE, COD_QUALITA_CATASTALE, SUPERFICIE_MQ, CUBATURA, " _
                                    & "NUM_VANI, SUPERFICIE_CATASTALE, RENDITA_STORICA, IMMOBILE_STORICO, REDDITO_DOMINICALE, " _
                                    & "VALORE_IMPONIBILE, REDDITO_AGRARIO, VALORE_BILANCIO, DATA_ACQUISIZIONE, DATA_FINE_VALIDITA,  " _
                                    & "DITTA, NUM_PARTITA, ESENTE_ICI, PERC_POSSESSO, INAGIBILE, " _
                                    & "MICROZONA_CENSUARIA, ZONA_CENSUARIA, COD_STATO_CATASTALE, INDIRIZZO, CIVICO, " _
                                    & "CAP, LOCALITA, COD_COMUNE, SUP_CONVENZIONALE,VAL_LOCATIVO_UNITA) " _
                                    & "Values " _
                                    & "(" & INDICE_CONTRATTO & " , " & idUNITAnew & " , '" _
                                    & myReaderD("COD_UNITA_IMMOBILIARE") & "', '" _
                                    & myReaderD("COD_TIPOLOGIA") & "', " & myReaderD("ID_EDIFICIO") & ", " _
                                    & "'" & par.IfNull(myReaderD("SCALAUNITA"), "") & "', '" _
                                    & par.IfNull(myReaderD("COD_TIPO_LIVELLO_PIANO"), "01") & "', '" _
                                    & par.IfNull(myReaderD("INTERNO"), "") & "', NULL, '" & par.IfNull(myReaderD("SEZIONE"), "") & "', " _
                                    & "'" & par.IfNull(myReaderD("FOGLIO"), "") & "', '" & par.IfNull(myReaderD("NUMERO"), "") & "', '" & par.IfNull(myReaderD("SUB"), "") & "', '" & par.IfNull(myReaderD("COD_TIPOLOGIA_CATASTO"), "") & "', " & par.VirgoleInPunti(par.IfNull(myReaderD("RENDITA"), "NULL")) & " , " _
                                    & "'" & par.IfNull(myReaderD("COD_CATEGORIA_CATASTALE"), "") & "', '" _
                                    & par.IfNull(myReaderD("COD_CLASSE_CATASTALE"), "") & "', '" & par.IfNull(myReaderD("COD_QUALITA_CATASTALE"), "") _
                                    & "', " & par.VirgoleInPunti(par.IfNull(myReaderD("SUPERFICIE_MQ"), "NULL")) _
                                    & ", " & par.VirgoleInPunti(par.IfNull(myReaderD("CUBATURA"), "NULL")) & ", " _
                                    & par.VirgoleInPunti(par.IfNull(myReaderD("NUM_VANI"), "NULL")) _
                                    & ", " & par.VirgoleInPunti(par.IfNull(myReaderD("SUPERFICIE_CATASTALE"), "NULL")) _
                                    & ", " & par.VirgoleInPunti(par.IfNull(myReaderD("RENDITA_STORICA"), "NULL")) _
                                    & ", " & par.VirgoleInPunti(par.IfNull(myReaderD("IMMOBILE_STORICO"), "NULL")) & ", NULL, " _
                                    & par.VirgoleInPunti(par.IfNull(myReaderD("VALORE_IMPONIBILE"), "NULL")) _
                                    & ", NULL, " & par.VirgoleInPunti(par.IfNull(myReaderD("VALORE_IMPONIBILE"), "NULL")) _
                                    & ", '" & par.IfNull(myReaderD("DATA_ACQUISIZIONE"), "") _
                                    & "', '" & par.IfNull(myReaderD("DATA_FINE_VALIDITA"), "") & "', '" _
                                    & par.IfNull(myReaderD("DITTA"), "") _
                                    & "', '" & par.IfNull(myReaderD("NUM_PARTITA"), "") _
                                    & "', " & par.IfNull(myReaderD("ESENTE_ICI"), "NULL") _
                                    & "," & par.VirgoleInPunti(par.IfNull(myReaderD("PERC_POSSESSO"), "NULL")) _
                                    & "," & par.IfNull(myReaderD("INAGIBILE"), "NULL") & ", '" _
                                    & par.IfNull(myReaderD("MICROZONA_CENSUARIA"), "") _
                                    & "', '" & par.IfNull(myReaderD("ZONA_CENSUARIA"), "") _
                                    & "', '" & par.IfNull(myReaderD("COD_STATO_CATASTALE"), "") _
                                    & "', '" & par.PulisciStrSql(par.IfNull(myReaderD("IND"), "")) _
                                    & "', '" & par.IfNull(myReaderD("CIVICO"), "") & "', " _
                                    & "'" & par.IfNull(myReaderD("CAP"), "") _
                                    & "', NULL, '" & par.IfNull(myReaderD("COD_COMUNE"), "") _
                                    & "', 0,'" & VAL_LOCATIVO_UNITA & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DIMENSIONI WHERE ID_UNITA_IMMOBILIARE=" & idUNITAnew
                Dim myReaderF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReaderF.Read
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.DIMENSIONI_UNITA_CONTRATTUALE (ID_CONTRATTO, ID_UNITA, COD_TIPOLOGIA, VALORE) VALUES (" _
                                        & INDICE_CONTRATTO & "," & idUNITAnew & ",'" _
                                        & par.IfNull(myReaderF("COD_TIPOLOGIA"), "") _
                                        & "'," & par.VirgoleInPunti(par.IfNull(myReaderF("VALORE"), "NULL")) & ")"
                    par.cmd.ExecuteNonQuery()
                Loop
                myReaderF.Close()

                Dim VAL_MILLESIMO As Double
                Dim TOT_VERDE As Double = 0


                par.cmd.CommandText = "SELECT VALORI_MILLESIMALI.VALORE_MILLESIMO FROM SISCOM_MI.TABELLE_MILLESIMALI,SISCOM_MI.VALORI_MILLESIMALI WHERE VALORI_MILLESIMALI.ID_UNITA_IMMOBILIARE=" & idUNITAnew & " AND TABELLE_MILLESIMALI.ID=VALORI_MILLESIMALI.ID_TABELLA AND TABELLE_MILLESIMALI.COD_TIPOLOGIA='VE'"
                Dim myReaderH As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReaderH.Read
                    VAL_MILLESIMO = VAL_MILLESIMO + CDbl(par.IfNull(myReaderH("VALORE_MILLESIMO"), 0))
                Loop
                myReaderH.Close()
                If VAL_MILLESIMO > 0 Then
                    par.cmd.CommandText = "SELECT DIMENSIONI.VALORE FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.UNITA_COMUNI WHERE dimensioni.cod_tipologia='SUVEC' AND UNITA_COMUNI.ID_EDIFICIO=" & myReaderD("ID_EDIFICIO") & " AND DIMENSIONI.ID_UNITA_COMUNE=UNITA_COMUNI.ID"
                    Dim myReaderG As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReaderG.Read
                        TOT_VERDE = TOT_VERDE + CDbl(par.IfNull(myReaderG("VALORE"), 0))
                    Loop
                    myReaderG.Close()

                    If idComplesso <> 0 Then
                        par.cmd.CommandText = "SELECT DIMENSIONI.VALORE FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.UNITA_COMUNI WHERE dimensioni.cod_tipologia='SUVEC' AND UNITA_COMUNI.ID_COMPLESSO=" & idComplesso & " AND DIMENSIONI.ID_UNITA_COMUNE=UNITA_COMUNI.ID"
                        myReaderG = par.cmd.ExecuteReader()
                        Do While myReaderG.Read
                            TOT_VERDE = TOT_VERDE + CDbl(par.IfNull(myReaderG("VALORE"), 0))
                        Loop
                        myReaderG.Close()
                    End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.DIMENSIONI_UNITA_CONTRATTUALE (ID_CONTRATTO, ID_UNITA, COD_TIPOLOGIA, VALORE) VALUES (" _
                                        & INDICE_CONTRATTO & "," & idUNITAnew & ",'SUVEC" _
                                        & "'," & par.VirgoleInPunti((TOT_VERDE / 1000) * VAL_MILLESIMO) & ")"
                    par.cmd.ExecuteNonQuery()
                End If

            End If
            myReaderD.Close()


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTESTATARI_RAPPORTO (ID_CONTRATTO,ID_ANAGRAFICA,DATA_INIZIO,DATA_FINE) VALUES (" & INDICE_CONTRATTO & "," & idAnagrafica & ",'" & par.AggiustaData(txtDataProvv.Text) & "','29991231')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & INDICE_CONTRATTO & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F01','')"
            par.cmd.ExecuteNonQuery()


            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)
            par.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>alert('Operazione Effettuata con successo!');</script>")
            btnChiudiContratto.Enabled = False

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Public Property sISE() As String
        Get
            If Not (ViewState("par_sISE") Is Nothing) Then
                Return CStr(ViewState("par_sISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE") = value
        End Set
    End Property

    Public Property sVSE() As String
        Get
            If Not (ViewState("par_sVSE") Is Nothing) Then
                Return CStr(ViewState("par_sVSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVSE") = value
        End Set
    End Property

    Public Property sISP() As String
        Get
            If Not (ViewState("par_sISP") Is Nothing) Then
                Return CStr(ViewState("par_sISP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISP") = value
        End Set
    End Property

    Public Property sPER_VAL_LOC() As String
        Get
            If Not (ViewState("par_sPER_VAL_LOC") Is Nothing) Then
                Return CStr(ViewState("par_sPER_VAL_LOC"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPER_VAL_LOC") = value
        End Set
    End Property


    Public Property sPERC_INC_MAX_ISE_ERP() As String
        Get
            If Not (ViewState("par_sPERC_INC_MAX_ISE_ERP") Is Nothing) Then
                Return CStr(ViewState("par_sPERC_INC_MAX_ISE_ERP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPERC_INC_MAX_ISE_ERP") = value
        End Set
    End Property

    Public Property sCANONE_MIN() As String
        Get
            If Not (ViewState("par_sCANONE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE_MIN") = value
        End Set
    End Property

    Public Property sISE_MIN() As String
        Get
            If Not (ViewState("par_sISE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sISE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE_MIN") = value
        End Set
    End Property

    Public Property sANNOINIZIOVAL() As String
        Get
            If Not (ViewState("par_sANNOINIZIOVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOINIZIOVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOINIZIOVAL") = value
        End Set
    End Property

    Public Property sANNOFINEVAL() As String
        Get
            If Not (ViewState("par_sANNOFINEVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOFINEVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOFINEVAL") = value
        End Set
    End Property


    Public Property sISR() As String
        Get
            If Not (ViewState("par_sISR") Is Nothing) Then
                Return CStr(ViewState("par_sISR"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISR") = value
        End Set
    End Property

    Public Property sREDD_DIP() As String
        Get
            If Not (ViewState("par_sREDD_DIP") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_DIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_DIP") = value
        End Set
    End Property

    Public Property sREDD_ALT() As String
        Get
            If Not (ViewState("par_sREDD_ALT") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_ALT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_ALT") = value
        End Set
    End Property

    Public Property sLimitePensione() As String
        Get
            If Not (ViewState("par_sLimitePensione") Is Nothing) Then
                Return CStr(ViewState("par_sLimitePensione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLimitePensione") = value
        End Set
    End Property


    Public Property sISEE() As String
        Get
            If Not (ViewState("par_sISEE") Is Nothing) Then
                Return CStr(ViewState("par_sISEE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISEE") = value
        End Set

    End Property

    Public Property sPSE() As String
        Get
            If Not (ViewState("par_sPSE") Is Nothing) Then
                Return CStr(ViewState("par_sPSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPSE") = value
        End Set
    End Property

    Public Property AreaEconomica() As Integer
        Get
            If Not (ViewState("par_AreaEconomica") Is Nothing) Then
                Return CInt(ViewState("par_AreaEconomica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_AreaEconomica") = value
        End Set

    End Property


    Public Property sCanone() As String
        Get
            If Not (ViewState("par_sCanone") Is Nothing) Then
                Return CStr(ViewState("par_sCanone"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCanone") = value
        End Set
    End Property


    Public Property sNOTE() As String
        Get
            If Not (ViewState("par_sNOTE") Is Nothing) Then
                Return CStr(ViewState("par_sNOTE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNOTE") = value
        End Set
    End Property

    Public Property sDEM() As String
        Get
            If Not (ViewState("par_sDEM") Is Nothing) Then
                Return CStr(ViewState("par_sDEM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDEM") = value
        End Set
    End Property

    Public Property sSUPCONVENZIONALE() As String
        Get
            If Not (ViewState("par_sSUPCONVENZIONALE") Is Nothing) Then
                Return CStr(ViewState("par_sSUPCONVENZIONALE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPCONVENZIONALE") = value
        End Set
    End Property

    Public Property sCOSTOBASE() As String
        Get
            If Not (ViewState("par_sCOSTOBASE") Is Nothing) Then
                Return CStr(ViewState("par_sCOSTOBASE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOSTOBASE") = value
        End Set
    End Property

    Public Property sZONA() As String
        Get
            If Not (ViewState("par_sZONA") Is Nothing) Then
                Return CStr(ViewState("par_sZONA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sZONA") = value
        End Set
    End Property

    Public Property sPIANO() As String
        Get
            If Not (ViewState("par_sPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPIANO") = value
        End Set
    End Property

    Public Property sCONSERVAZIONE() As String
        Get
            If Not (ViewState("par_sCONSERVAZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sCONSERVAZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCONSERVAZIONE") = value
        End Set
    End Property

    Public Property sVETUSTA() As String
        Get
            If Not (ViewState("par_sVETUSTA") Is Nothing) Then
                Return CStr(ViewState("par_sVETUSTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVETUSTA") = value
        End Set
    End Property

    Public Property sINCIDENZAISE() As String
        Get
            If Not (ViewState("par_sINCIDENZAISE") Is Nothing) Then
                Return CStr(ViewState("par_sINCIDENZAISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sINCIDENZAISE") = value
        End Set
    End Property


    Public Property sCOEFFFAM() As String
        Get
            If Not (ViewState("par_sCOEFFFAM") Is Nothing) Then
                Return CStr(ViewState("par_sCOEFFFAM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOEFFFAM") = value
        End Set
    End Property

    Public Property sSOTTOAREA() As String
        Get
            If Not (ViewState("par_sSOTTOAREA") Is Nothing) Then
                Return CStr(ViewState("par_sSOTTOAREA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSOTTOAREA") = value
        End Set
    End Property

    Public Property sMOTIVODECADENZA() As String
        Get
            If Not (ViewState("par_sMOTIVODECADENZA") Is Nothing) Then
                Return CStr(ViewState("par_sMOTIVODECADENZA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOTIVODECADENZA") = value
        End Set
    End Property

    Public Property sNUMCOMP() As String
        Get
            If Not (ViewState("par_sNUMCOMP") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP") = value
        End Set
    End Property

    Public Property sNUMCOMP66() As String
        Get
            If Not (ViewState("par_sNUMCOMP66") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP66"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP66") = value
        End Set
    End Property

    Public Property sNUMCOMP100() As String
        Get
            If Not (ViewState("par_sNUMCOMP100") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100") = value
        End Set
    End Property

    Public Property sNUMCOMP100C() As String
        Get
            If Not (ViewState("par_sNUMCOMP100C") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100C"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100C") = value
        End Set
    End Property

    Public Property sPREVDIP() As String
        Get
            If Not (ViewState("par_sPREVDIP") Is Nothing) Then
                Return CStr(ViewState("par_sPREVDIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPREVDIP") = value
        End Set
    End Property

    Public Property sDETRAZIONI() As String
        Get
            If Not (ViewState("par_sDETRAZIONI") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONI") = value
        End Set
    End Property

    Public Property sMOBILIARI() As String
        Get
            If Not (ViewState("par_sMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOBILIARI") = value
        End Set
    End Property


    Public Property sIMMOBILIARI() As String
        Get
            If Not (ViewState("par_sIMMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sIMMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIMMOBILIARI") = value
        End Set
    End Property

    Public Property sCOMPLESSIVO() As String
        Get
            If Not (ViewState("par_sCOMPLESSIVO") Is Nothing) Then
                Return CStr(ViewState("par_sCOMPLESSIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOMPLESSIVO") = value
        End Set
    End Property

    Public Property sDETRAZIONEF() As String
        Get
            If Not (ViewState("par_sDETRAZIONEF") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONEF"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONEF") = value
        End Set
    End Property

    Public Property sANNOCOSTRUZIONE() As String
        Get
            If Not (ViewState("par_sANNOCOSTRUZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sANNOCOSTRUZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOCOSTRUZIONE") = value
        End Set
    End Property

    Public Property sLOCALITA() As String
        Get
            If Not (ViewState("par_sLOCALITA") Is Nothing) Then
                Return CStr(ViewState("par_sLOCALITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLOCALITA") = value
        End Set
    End Property

    Public Property sASCENSORE() As String
        Get
            If Not (ViewState("par_sASCENSORE") Is Nothing) Then
                Return CStr(ViewState("par_sASCENSORE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sASCENSORE") = value
        End Set
    End Property

    Public Property sDESCRIZIONEPIANO() As String
        Get
            If Not (ViewState("par_sDESCRIZIONEPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sDESCRIZIONEPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDESCRIZIONEPIANO") = value
        End Set
    End Property

    Public Property sSUPNETTA() As String
        Get
            If Not (ViewState("par_sSUPNETTA") Is Nothing) Then
                Return CStr(ViewState("par_sSUPNETTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPNETTA") = value
        End Set
    End Property

    Public Property sALTRESUP() As String
        Get
            If Not (ViewState("par_sALTRESUP") Is Nothing) Then
                Return CStr(ViewState("par_sALTRESUP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALTRESUP") = value
        End Set
    End Property

    Public Property sMINORI15() As String
        Get
            If Not (ViewState("par_sMINORI15") Is Nothing) Then
                Return CStr(ViewState("par_sMINORI15"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMINORI15") = value
        End Set
    End Property


    Public Property sMAGGIORI65() As String
        Get
            If Not (ViewState("par_sMAGGIORI65") Is Nothing) Then
                Return CStr(ViewState("par_sMAGGIORI65"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMAGGIORI65") = value
        End Set
    End Property

    Public Property sSUPACCESSORI() As String
        Get
            If Not (ViewState("par_sSUPACCESSORI") Is Nothing) Then
                Return CStr(ViewState("par_sSUPACCESSORI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPACCESSORI") = value
        End Set
    End Property

    Public Property sVALORELOCATIVO() As String
        Get
            If Not (ViewState("par_sVALORELOCATIVO") Is Nothing) Then
                Return CStr(ViewState("par_sVALORELOCATIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALORELOCATIVO") = value
        End Set
    End Property

    Public Property sCANONEMINIMO() As String
        Get
            If Not (ViewState("par_sCANONEMINIMO") Is Nothing) Then
                Return CStr(ViewState("par_sCANONEMINIMO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONEMINIMO") = value
        End Set
    End Property

    Public Property sCANONECLASSE() As String
        Get
            If Not (ViewState("par_sCANONECLASSE") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSE") = value
        End Set
    End Property

    Public Property sCANONESOPP() As String
        Get
            If Not (ViewState("par_sCANONESOPP") Is Nothing) Then
                Return CStr(ViewState("par_sCANONESOPP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONESOPP") = value
        End Set
    End Property


    Public Property sVALOCIICI() As String
        Get
            If Not (ViewState("par_sVALOCIICI") Is Nothing) Then
                Return CStr(ViewState("par_sVALOCIICI"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALOCIICI") = value
        End Set
    End Property

    Public Property sALLOGGIOIDONEO() As String
        Get
            If Not (ViewState("par_sALLOGGIOIDONEO") Is Nothing) Then
                Return CStr(ViewState("par_sALLOGGIOIDONEO"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALLOGGIOIDONEO") = value
        End Set
    End Property

    Public Property sISTAT() As String
        Get
            If Not (ViewState("par_sISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISTAT") = value
        End Set
    End Property

    Public Property sCANONECLASSEISTAT() As String
        Get
            If Not (ViewState("par_sCANONECLASSEISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSEISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSEISTAT") = value
        End Set
    End Property

    Public Property VAL_LOCATIVO_UNITA() As String
        Get
            If Not (ViewState("par_VAL_LOCATIVO_UNITA") Is Nothing) Then
                Return CStr(ViewState("par_VAL_LOCATIVO_UNITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_VAL_LOCATIVO_UNITA") = value
        End Set

    End Property


End Class
