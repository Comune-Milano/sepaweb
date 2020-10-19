'12/01/2015 PUCCIA Nuova connessione  tls ssl
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security


Partial Class Condomini_ComunicazSloggio
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim pp As New MavOnline.MAVOnlineBeanService
    Dim RichiestaEmissioneMAV As New MavOnline.richiestaMAVOnlineWS
    Dim Esito As New MavOnline.rispostaMAVOnlineWS
    Dim binaryData() As Byte
    Dim outFile As System.IO.FileStream
    Dim outputFileName As String = ""
    '12/01/2015 PUCCIA Nuova connessione  tls ssl
    Private Shared Function CertificateHandler(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal SSLerror As SslPolicyErrors) As Boolean
        Return True
    End Function


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If
        If Not IsPostBack Then
            txtDataInvInq.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataInvioAmm.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataRicAmm.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


            txtDebitoInquilino.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
            txtDebitoInquilino.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")


            txtCreditoInquilino.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
            txtCreditoInquilino.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")

            CaricaDatiContrattuali()
        End If
    End Sub
    Private Sub CaricaDatiContrattuali()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT cod_contratto,(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO " _
                                & "FROM siscom_mi.rapporti_utenza,siscom_mi.soggetti_contrattuali,siscom_mi.anagrafica " _
                                & "WHERE soggetti_contrattuali.id_contratto = rapporti_utenza.ID " _
                                & "AND soggetti_contrattuali.id_anagrafica = anagrafica.ID " _
                                & "AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' " _
                                & "AND rapporti_utenza.ID = " & Request.QueryString("IDCONT")
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                Me.lblContratto.Text = "COD.CONTRATTO: " & par.IfNull(lettore("cod_contratto"), "")
                Me.lblIntestatario.Text = "INTESTATO A: " & par.IfNull(lettore("INTECONTRATTO"), "")
            End If
            lettore.Close()

            par.cmd.CommandText = "select * from siscom_mi.cond_avv_sloggio where id_cond_avviso = " & Request.QueryString("IDAVVISO")
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                id.Value = par.IfNull(lettore("id"), 0)
                Me.txtDataInvioAmm.Text = par.FormattaData(par.IfNull(lettore("DATA_INVIO_COM_AMM"), ""))
                Me.txtDataRicAmm.Text = par.FormattaData(par.IfNull(lettore("DATA_RIC_ESTRATTO_C"), ""))
                Me.txtDataInvInq.Text = par.FormattaData(par.IfNull(lettore("DATA_INVIO_INQUILINO"), ""))
                'Me.txtCreditoInquilino.Text = Format(CDec(par.IfNull(lettore("IMPORTO_CREDITO"),"0,00"), "##,##0.00")
                Me.txtCreditoInquilino.Text = Format(CDec(par.IfNull(lettore("IMPORTO_CREDITO"), "0,00")), "##,##0.00")

                Me.txtDebitoInquilino.Text = Format(CDec(par.IfNull(lettore("IMPORTO_DEBITO"), "0,00")), "##,##0.00")
                If par.IfNull(lettore("id_bolletta"), 0) > 0 Then
                    DisableField()

                End If
                If par.IfNull(lettore("non_sollecitare"), 0) = 1 Then
                    Me.chkNonSollecitare.Checked = True
                End If
            End If
            lettore.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>self.close();top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try
            '******APERTURA CONNESSIONE*****
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            '**********apertura transazione
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans


            If CDec(Me.txtDebitoInquilino.Text.Replace(".", "")) > CDec(Me.txtCreditoInquilino.Text.Replace(".", "")) Then
                If creaMav.Value = 1 Then
                    If SalvaDati() = False Then
                        par.myTrans.Rollback()
                        '*********************CHIUSURA CONNESSIONE**********************
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>self.close();top.location.href='../Errore.aspx';</script>")
                        'If EmettiMav() = False Then
                        '    par.myTrans.Rollback()
                        '    '*********************CHIUSURA CONNESSIONE**********************
                        '    par.OracleConn.Close()
                        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        '    Response.Write("<script>self.close();top.location.href='../Errore.aspx';</script>")
                        'Else
                        '    Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

                        'End If
                    Else

                        Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

                    End If
                End If
            Else
                If SalvaDati() = False Then
                    par.myTrans.Rollback()
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>self.close();top.location.href='../Errore.aspx';</script>")
                    Exit Sub
                Else
                    Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

                End If
            End If


            par.myTrans.Commit()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            creaMav.Value = 0
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub
    Private Function SalvaDati() As Boolean
        SalvaDati = True

        Try
            If id.Value = 0 Then
                par.cmd.CommandText = "insert into siscom_mi.cond_avv_sloggio(id,id_cond_avviso,data_invio_com_amm,data_ric_estratto_c,importo_debito,importo_credito,data_invio_inquilino,non_sollecitare) values " _
                    & "(siscom_mi.seq_cond_avv_sloggio.nextval," & Request.QueryString("IDAVVISO") & ",'" & par.AggiustaData(Me.txtDataInvioAmm.Text) & "'," _
                    & "'" & par.AggiustaData(Me.txtDataRicAmm.Text) & "'," & par.VirgoleInPunti(Me.txtDebitoInquilino.Text.Replace(".", "")) & "," _
                    & par.VirgoleInPunti(Me.txtCreditoInquilino.Text.Replace(".", "")) & ",'" & par.AggiustaData(Me.txtDataInvInq.Text) & "'," & chkZeroUno(chkNonSollecitare) & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "select siscoM_mi.seq_cond_avv_sloggio.currval from dual"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    id.Value = par.IfNull(lettore(0), "0")
                End If
                lettore.Close()

                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCOND") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F193','INSERIMENTO DATI ESTRATTO CONTO')"
                par.cmd.ExecuteNonQuery()

            Else
                par.cmd.CommandText = "update siscom_mi.cond_avv_sloggio set data_invio_com_amm = '" & par.AggiustaData(Me.txtDataInvioAmm.Text) & "', " _
                                    & "data_ric_estratto_c = '" & par.AggiustaData(Me.txtDataRicAmm.Text) & "' , " _
                                    & "importo_debito = " & par.VirgoleInPunti(Me.txtDebitoInquilino.Text.Replace(".", "")) & "," _
                                    & "importo_credito= " & par.VirgoleInPunti(Me.txtCreditoInquilino.Text.Replace(".", "")) & ", " _
                                    & "data_invio_inquilino = '" & par.AggiustaData(Me.txtDataInvInq.Text) & "', " _
                                    & "non_sollecitare = " & chkZeroUno(chkNonSollecitare) _
                                    & "where id = " & id.Value
                par.cmd.ExecuteNonQuery()

                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCOND") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F193','MODIFICA DATI ESTRATTO CONTO')"
                par.cmd.ExecuteNonQuery()

            End If

            Return SalvaDati
        Catch ex As Exception
            SalvaDati = False

        End Try
    End Function

    Public Property DisabilitaExpect100Continue() As String
        Get
            If Not (ViewState("par_DisabilitaExpect100Continue") Is Nothing) Then
                Return CStr(ViewState("par_DisabilitaExpect100Continue"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DisabilitaExpect100Continue") = value
        End Set
    End Property

    Private Function EmettiMav() As Boolean
        EmettiMav = False
        Try
            Dim APPLICABOLLO As Double = 0
            Dim SPESEmav As Double = 0
            Dim BOLLO As Double = 0
            Dim TipoIngiunzione As String = ""
            Dim Tot_Bolletta As Double = 0
            Dim CodiceContratto As String = ""
            Dim IdAnagrafica As String = "-1"
            Dim ScadenzaBollettino As String = ""
            Dim periodo As String


            Dim IdContratto As String = ""
            Dim idUnita As String = ""
            Dim idEdificio As String = ""
            Dim idComplesso As String = ""
            Dim presso_cor As String = ""
            Dim civico_cor As String = ""
            Dim luogo_cor As String = ""
            Dim cap_cor As String = ""
            Dim indirizzo_cor As String = ""
            Dim tipo_cor As String = ""
            Dim sigla_cor As String = ""

            Dim causalepagamento As String = ""


            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=25"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                APPLICABOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO='EXPECT100CONTINUE'"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                DisabilitaExpect100Continue = par.IfNull(myReaderA("valore"), "0")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                SPESEmav = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                BOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()



            par.cmd.CommandText = "SELECT ANAGRAFICA.ID AS ID_ANA , anagrafica.sesso, " _
                        & "ragione_sociale, COGNOME ,NOME, " _
                        & "PARTITA_IVA, COD_FISCALE , RAPPORTI_UTENZA.COD_CONTRATTO " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPO_BOLLETTE, SISCOM_MI.ANAGRAFICA, SISCOM_MI.RAPPORTI_UTENZA " _
                        & "WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO= BOL_BOLLETTE.ID_CONTRATTO " _
                        & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO " _
                        & "AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                        & "AND RAPPORTI_UTENZA.ID = " & Request.QueryString("IDCONT")
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                If Len(par.IfNull(dt.Rows(0).Item("PARTITA_IVA"), 0)) = 11 Or par.ControllaCF(par.IfNull(dt.Rows(0).Item("COD_FISCALE"), 0)) = True Then
                    Tot_Bolletta = 0
                    TipoIngiunzione = "SPESE CONDOMINIALI ARRETRATE"
                    CodiceContratto = par.IfNull(dt.Rows(0).Item("COD_CONTRATTO"), "")

                    IdAnagrafica = par.IfNull(dt.Rows(0).Item("id_ana"), "")
                    par.cmd.CommandText = "select complessi_immobiliari.id as idcomplesso,edifici.id as idedificio," _
                                        & "siscom_mi.rapporti_utenza.*,unita_contrattuale.id_unita from SISCOM_MI.EDIFICI," _
                                        & "siscom_mi.complessi_immobiliari,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale," _
                                        & "siscom_mi.rapporti_utenza where complessi_immobiliari.id=edifici.id_complesso and unita_immobiliari.id=unita_contrattuale.id_unita " _
                                        & "and edifici.id=unita_immobiliari.id_edificio and unita_contrattuale.id_contratto=rapporti_utenza.id and " _
                                        & "unita_contrattuale.id_unita_principale is null and rapporti_utenza.id =" & Request.QueryString("IDCONT")
                    Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader123.Read Then
                        IdContratto = par.IfNull(myReader123("id"), "-1")
                        idUnita = par.IfNull(myReader123("id_unita"), "-1")
                        presso_cor = par.IfNull(myReader123("presso_cor"), "")
                        luogo_cor = par.IfNull(myReader123("luogo_cor"), "")
                        civico_cor = par.IfNull(myReader123("civico_cor"), "")
                        cap_cor = par.IfNull(myReader123("cap_cor"), "")
                        indirizzo_cor = par.IfNull(myReader123("VIA_cor"), "")
                        tipo_cor = par.IfNull(myReader123("tipo_cor"), "")
                        sigla_cor = par.IfNull(myReader123("sigla_cor"), "")
                        idEdificio = par.IfNull(myReader123("idedificio"), "0")
                        idComplesso = par.IfNull(myReader123("idcomplesso"), "0")
                    End If
                    myReader123.Close()


                    Dim Titolo As String = ""
                    Dim Nome As String = ""
                    Dim Cognome As String = ""
                    Dim CF As String = ""

                    Dim ID_BOLLETTA As Long = 0

                    If par.IfNull(dt.Rows(0).Item("ragione_sociale"), "") <> "" Then
                        Titolo = ""
                        Cognome = par.IfNull(dt.Rows(0).Item("ragione_sociale"), "")
                        Nome = ""
                        CF = par.IfNull(dt.Rows(0).Item("partita_iva"), "")
                    Else
                        If par.IfNull(dt.Rows(0).Item("sesso"), "") = "M" Then
                            Titolo = "Sign."
                        Else
                            Titolo = "Sign.ra"
                        End If
                        Cognome = par.IfNull(dt.Rows(0).Item("cognome"), "")
                        Nome = par.IfNull(dt.Rows(0).Item("nome"), "")
                        CF = par.IfNull(dt.Rows(0).Item("cod_fiscale"), "")
                    End If
                    'attenzione la scadenza del bollettino é stata fissata a 10GG MA è PUARAMENTE DI ESEMPIO
                    ScadenzaBollettino = Format(DateAdd(DateInterval.Day, 10, Now), "yyyyMMdd")
                    periodo = Format(Now, "yyyyMMdd") & " - " & Format(Now, "yyyyMMdd")

                    Dim Nome1 As String = ""
                    Dim Nome2 As String = ""

                    If UCase(Cognome & " " & Nome) <> UCase(presso_cor) Then
                        Nome1 = Cognome & " " & Nome
                        Nome2 = presso_cor
                    Else
                        Nome1 = presso_cor
                    End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE " _
                    & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                    & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                    & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                    & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                    & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                    & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                    & "Values " _
                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999 , '" & Format(Now, "yyyyMMdd") _
                    & "', '" & ScadenzaBollettino & "', NULL,NULL,NULL,'BOLLETTA ANTICIPO RATEIZZAZIONE'," _
                    & "" & IdContratto _
                    & " ," & par.RicavaEsercizioCorrente & ", " _
                    & par.IfNull(idUnita, 0) _
                    & ", '0', ''," & par.IfNull(dt.Rows(0).Item("ID_ANA"), 0) _
                    & ", '" & par.PulisciStrSql(Nome1) & "', " _
                    & "'" & tipo_cor & " " & par.PulisciStrSql(indirizzo_cor) & ", " & par.PulisciStrSql(civico_cor) _
                    & "', '" & par.PulisciStrSql(cap_cor & " " & luogo_cor & "(" & sigla_cor & ")") _
                    & "', '" & par.PulisciStrSql(Nome2) & "', '" & Format(Now, "yyyyMMdd") _
                    & "', '" & Format(Now, "yyyyMMdd") & "', " _
                    & "'0', " & idComplesso & ", '', NULL, '', " _
                    & Year(Now) & ", '', " & idEdificio & ", NULL, NULL,'MOD',5)"

                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                    Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderB.Read Then
                        ID_BOLLETTA = myReaderB(0)
                    Else
                        ID_BOLLETTA = -1
                    End If
                    myReaderB.Close()

                    If CDec(Me.txtCreditoInquilino.Text.Replace(".", "")) > 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",626" _
                                            & ",-" & par.VirgoleInPunti(CDec(Me.txtCreditoInquilino.Text.Replace(".", ""))) & ")"
                        par.cmd.ExecuteNonQuery()
                        Tot_Bolletta = Tot_Bolletta - CDec(Me.txtCreditoInquilino.Text.Replace(".", ""))
                    End If

                    'nuova voce 706 SPESE CONDOMINIALI ARRETRATE
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",706" _
                    & "," & par.VirgoleInPunti(CDec(Me.txtDebitoInquilino.Text.Replace(".", ""))) & ")"
                    par.cmd.ExecuteNonQuery()
                    Tot_Bolletta = Tot_Bolletta + CDec(Me.txtDebitoInquilino.Text.Replace(".", ""))

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",407" _
                        & "," & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                    par.cmd.ExecuteNonQuery()

                    Tot_Bolletta = Tot_Bolletta + SPESEmav

                    If Tot_Bolletta >= APPLICABOLLO Then

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",95" _
                                                    & "," & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                        par.cmd.ExecuteNonQuery()
                        Tot_Bolletta = Tot_Bolletta + BOLLO
                    End If

                    par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta where id=31"
                    Dim letty As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If letty.Read Then
                        causalepagamento = par.IfNull(letty("valore"), "")
                    End If
                    letty.Close()
                    'If Session.Item("AmbienteDiTest") = "1" Then
                    '    causalepagamento = "COMMITEST01"
                    '    'pp.Url = "https://web1.unimaticaspa.it/pagamenti20-test-ws/services/MAVOnline"
                    '    pp.Url = "https://demoweb.infogroup.it/pagamenti20-test-ws/services/MAVOnline"

                    'End If
                    If Session.Item("AmbienteDiTest") = "1" Then
                        causalepagamento = "COMMITEST01"
                        'pp.Url = "https://incassonline-coll.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                        pp.Url = Session.Item("indirizzoMavOnLine")
                    Else
                        'pp.Url = "https://incassonline.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                        pp.Url = Session.Item("indirizzoMavOnLine")
                    End If

                    par.cmd.CommandText = "update siscom_mi.cond_avv_sloggio set id_bolletta = " & ID_BOLLETTA & " where id = " & id.Value
                    par.cmd.ExecuteNonQuery()

                    idBolletta.Value = ID_BOLLETTA

                    RichiestaEmissioneMAV.codiceEnte = "commi"
                    RichiestaEmissioneMAV.tipoPagamento = causalepagamento
                    RichiestaEmissioneMAV.idOperazione = Format(ID_BOLLETTA, "0000000000")
                    RichiestaEmissioneMAV.codiceDebitore = Format(CDbl(IdAnagrafica), "0000000000")

                    RichiestaEmissioneMAV.causalePagamento = CreaCausale(TipoIngiunzione, ID_BOLLETTA, par.IfNull(dt.Rows(0).Item("COD_CONTRATTO"), ""))

                    'RichiestaEmissioneMAV.scadenzaPagamento = Mid(ScadenzaBollettino, 1, 4) & "-" & Mid(ScadenzaBollettino, 5, 2) & "-" & Mid(ScadenzaBollettino, 7, 2)
                    RichiestaEmissioneMAV.scadenzaPagamento = Mid(ScadenzaBollettino, 1, 4) & "-" & Mid(ScadenzaBollettino, 5, 2) & "-" & Mid(ScadenzaBollettino, 7, 2)

                    RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(Tot_Bolletta * 100)
                    RichiestaEmissioneMAV.userName = Format(CDbl(IdAnagrafica), "0000000000")
                    RichiestaEmissioneMAV.codiceFiscaleDebitore = CF

                    RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = Mid(Cognome, 1, 30)
                    If Nome <> "" Then
                        RichiestaEmissioneMAV.nomeDebitore = Mid(Nome, 1, 30)
                    End If


                    If Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor) <= 23 Then
                        RichiestaEmissioneMAV.indirizzoDebitore = tipo_cor & " " & indirizzo_cor & ", " & civico_cor
                    Else
                        RichiestaEmissioneMAV.indirizzoDebitore = Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 1, 23)
                        RichiestaEmissioneMAV.frazioneDebitore = Mid(Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 24, Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor)), 1, 28)
                    End If

                    RichiestaEmissioneMAV.capDebitore = Mid(cap_cor, 1, 5)
                    RichiestaEmissioneMAV.localitaDebitore = Mid(luogo_cor, 1, 23)
                    RichiestaEmissioneMAV.provinciaDebitore = Mid(sigla_cor, 1, 2)
                    RichiestaEmissioneMAV.nazioneDebitore = "IT"

                    '12/01/2015 PUCCIA Nuova connessione  tls ssl
                    If DisabilitaExpect100Continue = "1" Then
                        System.Net.ServicePointManager.Expect100Continue = False
                    End If

                   

                    '/*/*/*/*/*tls v1
                    Dim v As String = ""
                    par.cmd.CommandText = "select valore from siscom_mi.parametri where parametro='SSL MAV ON LINE'"
                    v = par.cmd.ExecuteScalar
                    System.Net.ServicePointManager.SecurityProtocol = CType(v, Net.SecurityProtocolType)
                    '/*/*/*/*/*tls v1


                    System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateHandler



                    Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)
                    If Esito.codiceRisultato = "0" Then


                        outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"
                        binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
                        outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                        outFile.Write(binaryData, 0, binaryData.Length - 1)
                        outFile.Close()

                        outputFileName = ("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"


                        EmettiMav = True
                        Response.Write("<script>window.open('../ALLEGATI/CONTRATTI/ELABORAZIONI/MAV/" & Format(ID_BOLLETTA, "0000000000") & ".pdf" & "','MAV_CONDOMINIO','');</script>")
                        DisableField()
                    Else

                        Dim FileDaCreare As String = Format(ID_BOLLETTA, "0000000000")
                        If System.IO.File.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & FileDaCreare & ".xml") = True Then
                            FileDaCreare = FileDaCreare & "_" & Format(Now, "yyyyMMddHHmmss")
                        End If
                        

                        outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & FileDaCreare & ".xml"
                        binaryData = System.Convert.FromBase64String(Esito.descrizioneTecnicaRisultato)
                        outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                        outFile.Write(binaryData, 0, binaryData.Length)
                        outFile.Close()
                    End If

                Else
                    'errore in caso di partita iva o codice fiscale
                    Response.Write("<script>alert('Operazione non riuscita!\nCodice Fiscale/P.IVA intestatario non corretti!Correggere i dati e rieseguire per completare l\'operazione!');</script>")

                End If
            Else
                Response.Write("<script>alert('Operazione non riuscita!Impossibile identificare l\'intestatario del contratto!');</script>")

            End If


        Catch ex As Exception
            EmettiMav = False
        End Try
    End Function
    Private Function CreaCausale(ByVal Tipo As String, ByVal idb As Long, Optional ByVal codContratto As String = "") As String
        CreaCausale = ""
        Try
            Dim sCausale As String = ""
            Dim sImporto As String = ""
            Dim iDifferenza As Integer = 0
            Dim sDescrizione As String = ""

            sCausale = ""
            If Not String.IsNullOrEmpty(codContratto) Then
                sCausale = ("COD.RAPPORTO: " & codContratto).ToString.PadRight(55)
            End If
            par.cmd.CommandText = "select t_voci_bolletta.descrizione,bol_bollette_voci.importo from siscom_mi.bol_bollette,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette_voci where bol_bollette_voci.id_bolletta=bol_bollette.id and t_voci_bolletta.id=bol_bollette_voci.id_voce and bol_bollette.id=" & idb & " order by t_voci_bolletta.descrizione asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                sImporto = Format(par.IfNull(myReader("importo"), "0"), "##,##0.00")

                'If sImporto < 1 And sImporto > 0 Then
                '    sImporto = Format(CDbl(sImporto), "0.00")
                'End If

                If sImporto < 1 And sImporto > 0 Then
                    sImporto = "0" & sImporto
                End If

                If sImporto > -1 And sImporto < 0 Then
                    sImporto = "-0" & Replace(sImporto, "-", "")
                End If

                iDifferenza = 55 - Len(sImporto)
                sDescrizione = par.IfNull(myReader("descrizione"), "")
                sCausale = sCausale & Mid(sDescrizione.PadRight(iDifferenza), 1, iDifferenza) & sImporto
            Loop
            CreaCausale = sCausale
            myReader.Close()
            'par.cmd.Dispose()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.myTrans.Rollback()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Function
    Private Sub DisableField()

        Me.txtDebitoInquilino.ReadOnly = True
        Me.txtCreditoInquilino.ReadOnly = True
    End Sub
    Private Function chkZeroUno(ByVal chk As CheckBox) As Integer
        chkZeroUno = 0
        Try
            If chk.Checked = True Then
                chkZeroUno = 1
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Function
End Class
