Imports System.IO

Partial Class Contratti_Proroga_AssDefinitiva_AssegnDefinitiva
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False Then
            txtDataCons.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataDecorr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataProvv.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataStipula.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtDataProvv.Attributes.Add("onblur", "javascript:confronta_dataDelibera(document.getElementById('txtDataProvv').value,'" & Format(Now, "dd/MM/yyyy") & "');")

            txtDataDecorr.Attributes.Add("onblur", "javascript:confronta_date(document.getElementById('txtDataProvv').value,document.getElementById('txtDataDecorr').value,'Data Provvedimento','Data Decorrenza',document.getElementById('txtDataDecorr'));")
            txtDataDecorr.Attributes.Add("onchange", "javascript:ScriviDate();")
            txtDataCons.Attributes.Add("onblur", "javascript:confronta_date(document.getElementById('txtDataProvv').value,document.getElementById('txtDataCons').value,'Data Provvedimento','Data Consegna',document.getElementById('txtDataCons'));confronta_date(document.getElementById('txtDataDecorr').value,document.getElementById('txtDataCons').value,'Data Decorrenza','Data Consegna',document.getElementById('txtDataCons'));")

        End If
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If confermaAssegnDef.Value = 1 Then
            If ControllaAssTemp() = True Then
                CreazioneNuovoContratto()
                btnProcedi.Enabled = False
            Else
                Response.Write("<script>alert('Attenzione...Il contratto non risulta TEMPORANEO. Impossibile procedere!')</script>")
                Exit Sub
            End If
        End If
    End Sub

    Private Function ControllaAssTemp() As Boolean
        Dim assTemp As Boolean = True
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & Request.QueryString("IDC") & " AND FL_ASSEGN_TEMP=1"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                assTemp = True
            Else
                assTemp = False
            End If
            myReader0.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

        Return assTemp

    End Function

    Private Sub CreazioneNuovoContratto()
        Try
            Dim IdContratto As Long = 0
            Dim codContratto As String = ""
            Dim codUI As String = ""
            Dim idUI As Long = 0
            Dim indiceContratto As Long = 0
            Dim RinnovoCanone As Double = 0
            Dim sNuovoCodiceRapporto As String = ""
            Dim lNuovoIdRapporto As Long = 0
            Dim idDomanda As Long = 0
            Dim provCalcoloCanone As Integer

            IdContratto = Request.QueryString("IDC")
            codContratto = Request.QueryString("COD")

            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE1111", par.OracleConn)

            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)


            '-----------06/12/2011
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & codContratto & "'"
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable
            da1.Fill(dt1)
            da1.Dispose()
            IdContratto = dt1.Rows(0).Item("id")


            If par.IfNull(dt1.Rows(0).Item("ID_AU"), 0) = 0 Then
                If par.IfNull(dt1.Rows(0).Item("ID_DOMANDA"), 0) = 0 Then
                    idDomanda = par.IfNull(dt1.Rows(0).Item("ID_ISEE"), 0)
                    provCalcoloCanone = 4
                Else
                    idDomanda = dt1.Rows(0).Item("ID_DOMANDA")
                    provCalcoloCanone = 1
                End If
            Else

                par.cmd.CommandText = "SELECT ID FROM UTENZA_DICHIARAZIONI WHERE rapporto='" & codContratto & "' order by id_bando desc"
                Dim myReaderMAX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderMAX.Read Then
                    idDomanda = par.IfNull(myReaderMAX(0), 0)
                    provCalcoloCanone = 0
                End If
                myReaderMAX.Close()

            End If

            RinnovoCanone = dt1.Rows(0).Item("IMP_CANONE_INIZIALE")

            par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_CONTRATTO=" & IdContratto
            Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX1.Read Then
                RinnovoCanone = RinnovoCanone + par.IfNull(myReaderX1(0), 0)
            End If
            myReaderX1.Close()

            Dim aggiornamento_istat As Double = 0
            Dim AltriAdeguamenti As Double = 0

            par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE id_motivo=2 and ID_CONTRATTO=" & IdContratto
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore.Read Then
                aggiornamento_istat = par.IfNull(lettore(0), 0)
            End If
            lettore.Close()

            par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE id_motivo<>2 and ID_CONTRATTO=" & IdContratto
            lettore = par.cmd.ExecuteReader()
            If lettore.Read Then
                AltriAdeguamenti = par.IfNull(lettore(0), 0)
            End If
            lettore.Close()


            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,UNITA_IMMOBILIARI.id as IDUI FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE WHERE unita_contrattuale.id_unita_principale is null and UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & IdContratto
            lettore = par.cmd.ExecuteReader()
            If lettore.Read Then
                codUI = par.IfNull(lettore("COD_UNITA_IMMOBILIARE"), "")
                idUI = par.IfNull(lettore("IDUI"), "")
            End If
            lettore.Close()

            'par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID=" & lIdDichiarazione & " AND PROGR=0"
            'lettore = par.cmd.ExecuteReader()
            'If lettore.Read Then
            '    RinnovoDataPG = par.IfNull(lettore("DATA_PG"), "")
            '    RinnovoNumeroPG = par.IfNull(lettore("PG"), "")
            '    CFNuovoIntest = par.IfNull(lettore("COD_FISCALE"), "")
            'End If
            'lettore.Close()
            '-----------fine 06/12/2011



            Dim progressivo As Integer
            par.cmd.CommandText = "select MAX(TO_NUMBER(TRANSLATE(SUBSTR(cod_contratto,18,2),'A','0'))) from SISCOM_MI.rapporti_utenza,siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari where id_unita=" & idUI & " and rapporti_utenza.id=unita_contrattuale.id_contratto and unita_contrattuale.id_unita=siscom_mi.unita_immobiliari.id"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                progressivo = par.IfNull(myReader1(0), 0) + 1
            Else
                progressivo = 1
            End If
            myReader1.Close()
            Dim NuovoCodiceContratto As String = Mid(codContratto, 1, 17) & Format((progressivo), "00")

            Dim nome As String = ""
            Dim Cognome As String = ""
            Dim CF As String = ""

            par.cmd.CommandText = "select anagrafica.* from siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND soggetti_contrattuali.id_CONTRATTO=" & IdContratto


            Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReaderX.Read = True Then
                If par.IfNull(myReaderX("ragione_sociale"), "") = "" Then
                    Cognome = par.IfNull(myReaderX("cognome"), "")
                    nome = par.IfNull(myReaderX("nome"), "")
                Else
                    Cognome = par.IfNull(myReaderX("ragione_sociale"), "")
                    nome = ""
                End If
                If par.IfNull(myReaderX("partita_iva"), "") = "" Then
                    CF = par.IfNull(myReaderX("cod_fiscale"), "")
                Else
                    CF = par.IfNull(myReaderX("partita_iva"), "")
                End If

            End If
            myReaderX.Close()


            Dim proxBolletta As String = ""

            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.*,RAPPORTI_UTENZA_PROSSIMA_BOL.PROSSIMA_BOLLETTA FROM SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL,SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA_PROSSIMA_BOL.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID=" & IdContratto
            myReaderX = par.cmd.ExecuteReader()
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
                                    & par.IfNull(myReaderX("COD_TIPOLOGIA_RAPP_CONTR"), "") & "', 'ERP" _
                                    & "',4, " & par.IfNull(myReaderX("DURATA_MESI"), "0") _
                                    & ", NULL, '" & par.AggiustaData(txtDataDecorr.Text) & "', '" & par.AggiustaData(dataScadenza.Value) & "',  '' , NULL, NULL, " & par.VirgoleInPunti(RinnovoCanone) & ",0,  NULL, NULL, NULL, NULL, NULL,  NULL, NULL, NULL, NULL, '" _
                                    & "TNP',  '" & par.AggiustaData(txtDataStipula.Text) & "', '" & par.AggiustaData(txtDataCons.Text) & "', '" & par.AggiustaData(dataScadenza2.Value) & "', 4 " _
                                    & ", " & par.IfNull(myReaderX("MESI_DISDETTA"), "6") & ",     NULL, '" & par.PulisciStrSql(txtNumProvv.Text) _
                                    & "', '" & par.AggiustaData(par.IfNull(txtDataProvv.Text, "")) & "', 12, '" & par.IfNull(myReaderX("FREQ_VAR_ISTAT"), "0") _
                                    & "' ,'" & par.IfNull(myReaderX("VERSAMENTO_TR"), "") & "', NULL, '" & par.IfNull(myReaderX("TIPO_COR"), "VIA") _
                                    & "', '" & par.PulisciStrSql(par.IfNull(myReaderX("LUOGO_COR"), "")) & "', '" _
                                    & par.PulisciStrSql(par.IfNull(myReaderX("VIA_COR"), "")) _
                                    & "', NULL, '" & par.PulisciStrSql(par.IfNull(myReaderX("CIVICO_COR"), "")) & "', '" _
                                    & par.PulisciStrSql(par.IfNull(myReaderX("SIGLA_COR"), "")) & "', '" _
                                    & par.PulisciStrSql(par.IfNull(myReaderX("CAP_COR"), "")) & "', '" _
                                    & par.PulisciStrSql(par.IfNull(myReaderX("PRESSO_COR"), "")) & "',     1, NULL, NULL, NULL, NULL,     NULL, '" _
                                    & par.IfNull(myReaderX("PROSSIMA_BOLLETTA"), "") & "', '" & par.IfNull(myReaderX("LIBRETTO_DEPOSITO"), "") _
                                    & "', " & par.IfNull(myReaderX("ID_DEST_RATE"), "1") & ", " & par.IfNull(myReaderX("INVIO_BOLLETTA"), "1") _
                                    & ",'" & par.IfNull(myReaderX("FL_CONGUAGLIO"), "1") & "', " & par.IfNull(myReaderX("PERC_RINNOVO_CONTRATTO"), "0") _
                                    & ", " & par.IfNull(myReaderX("PERC_ISTAT"), "0") & ", " & par.VirgoleInPunti(RinnovoCanone) _
                                    & ", 1 ,     '0', 0, " & par.IfNull(myReaderX("ID_DOMANDA"), "NULL") _
                                    & ", " & par.IfNull(myReaderX("ID_AU"), "NULL") & ", " & par.IfNull(myReaderX("ID_ISEE"), "NULL") _
                                    & "," & par.IfNull(myReaderX("ID_COMMISSARIATO"), "1") & ", NULL, 'R" _
                                    & "', '" & par.PulisciStrSql(par.IfNull(myReaderX("DESCR_DEST_USO"), "")) _
                                    & "', NULL, 4, 0, NULL, NULL, NULL,     -1, NULL, NULL, NULL, NULL)"
                par.cmd.ExecuteNonQuery()

                proxBolletta = par.IfNull(myReaderX("PROSSIMA_BOLLETTA"), "")

            End If
            myReaderX.Close()


            par.cmd.CommandText = "select siscom_mi.seq_RAPPORTI_UTENZA.currval from dual"
            Dim myReaderX2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX2.Read Then
                indiceContratto = myReaderX2(0)
            End If
            myReaderX2.Close()


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL (ID_CONTRATTO,PROSSIMA_BOLLETTA) VALUES (" & indiceContratto & ",'" & proxBolletta & "')"
            par.cmd.ExecuteNonQuery()


            Dim s As String = ""
            Dim comunicazioni As String = ""
            Dim LBLNOMEFILECANONE As String = ""
            Dim fileName As String = NuovoCodiceContratto & ".txt"

            s = par.CalcolaCanone27(idDomanda, provCalcoloCanone, idUI, NuovoCodiceContratto, RinnovoCanone, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)

            If comunicazioni <> "" Then
                Response.Write("<script>alert('" & comunicazioni & "');</script>")
            End If

            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeCanoni27\") & fileName, False, System.Text.Encoding.Default)
            sr.WriteLine(s)
            sr.Close()
            LBLNOMEFILECANONE = Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeCanoni27\") & fileName

            If System.IO.File.Exists(LBLNOMEFILECANONE) = True Then

                'Dim sr1 As StreamReader = New StreamReader(LBLNOMEFILECANONE, System.Text.Encoding.GetEncoding("iso-8859-1"))
                'Dim contenuto As String = sr1.ReadToEnd()
                'sr1.Close()

                'par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC (ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI, LIMITE_PENSIONI, ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,TESTO) " _
                '                    & "VALUES (" & lIdContratto & ",'" & Format(My.Computer.FileSystem.GetFileInfo(LBLNOMEFILECANONE.Value).CreationTime, "yyyyMMddHHmmss") _
                '                    & "'," & AreaEconomica & ",'" & sISEE & "','" & sISE & "','" & sISR & "','" & sISP & "','" & sPSE & "','" & sVSE & "','" & sREDD_DIP & "','" & sREDD_ALT & "','" & sLimitePensione & "','" & sISE_MIN & "','" & sPER_VAL_LOC & "','" & sPERC_INC_MAX_ISE_ERP & "','" & sCanone & "',:TESTO) "
                If sNOTE = "" Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.CANONI_EC (CANONE_CLASSE,CANONE_SOPPORTABILE,DECADENZA_ALL_ADEGUATO,DECADENZA_VAL_ICI,CANONE_MINIMO_AREA,VALORE_LOCATIVO,SUP_ACCESSORI,MINORI_15,MAGGIORI_65,DATA_STIPULA,COD_CONTRATTO,ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI, LIMITE_PENSIONI, " _
                                        & "ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,TESTO,NOTE,ID_BANDO_AU,DEM, SUPCONVENZIONALE, COSTOBASE, ZONA, PIANO, CONSERVAZIONE, VETUSTA,INCIDENZA_ISE," _
                                        & "COEFF_NUCLEO_FAM,SOTTO_AREA,ANNOTAZIONI,PATRIMONIO_SUP,NON_RISPONDENTE,LIMITE_ISEE,ID_DICHIARAZIONE,CANONE_ATTUALE,ADEGUAMENTO,ISTAT," _
                                        & "NUM_COMP,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_PREV_DIP,DETRAZIONI,REDD_MOBILIARI,REDD_IMMOBILIARI,REDD_COMPLESSIVO,DETRAZIONI_FRAGILITA,ANNO_COSTRUZIONE," _
                                        & "LOCALITA,PRESENTE_ASCENSORE,NUMERO_PIANO,SUP_NETTA,ALTRE_SUP,PERC_ISTAT_APPLICATA,CANONE_CLASSE_ISTAT,CANONE_91,INIZIO_VALIDITA_CAN,FINE_VALIDITA_CAN,TIPO_PROVENIENZA) " _
                                        & "VALUES ('" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) & "'," & sMINORI15 & "," & sMAGGIORI65 & ",'','" & NuovoCodiceContratto & "'," & indiceContratto & ",'" & Format(Now, "yyyyMMddHHmmss") _
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
                                        & "VALUES ('" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) & "'," & sMINORI15 & "," & sMAGGIORI65 & ",'','" & NuovoCodiceContratto & "'," & indiceContratto & ",'" & Format(Now, "yyyyMMddHHmmss") & "',NULL,'','','','','','','','','','','','','',:TESTO,'" & _
                                        par.PulisciStrSql(sNOTE) & "',NULL,'" & sMOTIVODECADENZA & "',0,0,0," _
                                        & "null" & ",'0','0" _
                                        & "','0'," & sNUMCOMP & "," & sNUMCOMP66 & "," & sNUMCOMP100 & "," & sNUMCOMP100C & "," & sPREVDIP _
                                        & ",'" & sDETRAZIONI & "','" & sMOBILIARI & "','" & sIMMOBILIARI & "','" & sCOMPLESSIVO & "','" & sDETRAZIONEF & "','" & sANNOCOSTRUZIONE & "','" _
                                        & par.PulisciStrSql(sLOCALITA) & "','" & sASCENSORE & "','" & par.PulisciStrSql(sDESCRIZIONEPIANO) & "','" & sSUPNETTA & "','" & par.PulisciStrSql(sALTRESUP) & "','" & sISTAT & "','" & sCANONECLASSEISTAT & "','0','" & sANNOINIZIOVAL & "','" & sANNOFINEVAL & "',4) "
                End If

                Dim objStream As Stream = File.Open(LBLNOMEFILECANONE, FileMode.Open)
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
                System.IO.File.Delete(LBLNOMEFILECANONE)
                par.cmd.Parameters.Remove(parmData)

                buffer = Nothing
                objStream = Nothing
            End If

            Dim CAUZIONE As Double = 0
            CAUZIONE = Format(Format((RinnovoCanone / 12), "0.00") * 3, "0.00")

            par.cmd.CommandText = "update siscom_mi.rapporti_utenza set imp_canone_iniziale=" & par.VirgoleInPunti(RinnovoCanone) & ",IMP_DEPOSITO_CAUZ=" & par.VirgoleInPunti(CAUZIONE) & " WHERE ID=" & indiceContratto
            par.cmd.ExecuteNonQuery()

            '16/10/2014 IMPORTO COMPONENTI DALL'ULTIMA ANAGRAFE UTENZA E NON DAL CONTRATTO
            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & idDomanda
            Dim myReaderUT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderUT.Read Then
                AggiungiComponente(idDomanda, indiceContratto)
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO=" & IdContratto & " and NVL(data_fine,'29991231') >= '" & Format(Now(), "yyyyMMdd") & "'"
                myReaderX = par.cmd.ExecuteReader()
                Do While myReaderX.Read
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SOGGETTI_CONTRATTUALI (ID_ANAGRAFICA,ID_CONTRATTO,COD_TIPOLOGIA_PARENTELA,COD_TIPOLOGIA_OCCUPANTE,COD_TIPOLOGIA_TITOLO,DATA_INIZIO) VALUES (" _
                                        & par.IfNull(myReaderX("ID_ANAGRAFICA"), "0") & "," & indiceContratto & ",'" & par.IfNull(myReaderX("COD_TIPOLOGIA_PARENTELA"), "1") _
                                        & "','" & par.IfNull(myReaderX("COD_TIPOLOGIA_OCCUPANTE"), "") & "','" & par.IfNull(myReaderX("COD_TIPOLOGIA_TITOLO"), "") & "','" & par.AggiustaData(txtDataStipula.Text) & "')"
                    par.cmd.ExecuteNonQuery()
                Loop
                myReaderX.Close()
            End If
            myReaderUT.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & IdContratto
            myReaderX = par.cmd.ExecuteReader()
            Do While myReaderX.Read
                par.cmd.CommandText = "Insert into SISCOM_MI.UNITA_CONTRATTUALE (ID_CONTRATTO, ID_UNITA, COD_UNITA_IMMOBILIARE, TIPOLOGIA, ID_EDIFICIO, SCALA, COD_TIPO_LIVELLO_PIANO, " _
                                 & "INTERNO, ID_UNITA_PRINCIPALE, SEZIONE, FOGLIO, NUMERO, SUB, COD_TIPOLOGIA_CATASTO, RENDITA, COD_CATEGORIA_CATASTALE, COD_CLASSE_CATASTALE," _
                                 & "COD_QUALITA_CATASTALE, SUPERFICIE_MQ, CUBATURA, NUM_VANI, SUPERFICIE_CATASTALE, RENDITA_STORICA, IMMOBILE_STORICO, REDDITO_DOMINICALE, VALORE_IMPONIBILE, " _
                                 & "REDDITO_AGRARIO, VALORE_BILANCIO, DATA_ACQUISIZIONE, DATA_FINE_VALIDITA, DITTA, NUM_PARTITA, ESENTE_ICI, PERC_POSSESSO, INAGIBILE, MICROZONA_CENSUARIA, " _
                                 & "ZONA_CENSUARIA, COD_STATO_CATASTALE, INDIRIZZO, CIVICO, CAP, LOCALITA, COD_COMUNE, SUP_CONVENZIONALE, VAL_LOCATIVO_UNITA) Values (" _
                                 & indiceContratto & "," & par.IfNull(myReaderX("id_unita"), "") & ", '" & par.IfNull(myReaderX("cod_unita_immobiliare"), "") _
                                 & "', '" & par.IfNull(myReaderX("tipologia"), "") & "', " & par.IfNull(myReaderX("id_edificio"), "") _
                                 & ", '" & par.IfNull(myReaderX("scala"), "null") & "', '" & par.IfNull(myReaderX("cod_tipo_livello_piano"), "") _
                                 & "', '" & par.IfNull(myReaderX("interno"), "") & "', " & par.IfNull(myReaderX("id_unita_principale"), "null") _
                                 & ", '" & par.IfNull(myReaderX("sezione"), "") & "','" & par.IfNull(myReaderX("foglio"), "") & "','" & par.IfNull(myReaderX("numero"), "") _
                                 & "', '" & par.IfNull(myReaderX("sub"), "") & "', '" & par.IfNull(myReaderX("COD_TIPOLOGIA_catasto"), "") & "', " & par.VirgoleInPunti(par.IfNull(myReaderX("rendita"), "null")) _
                                 & " ,'" & par.IfNull(myReaderX("cod_categoria_catastale"), "") & "', '" & par.IfNull(myReaderX("cod_classe_catastale"), "") _
                                 & "', " & par.VirgoleInPunti(par.IfNull(myReaderX("superficie_mq"), "null")) & ", " & par.VirgoleInPunti(par.IfNull(myReaderX("cubatura"), "null")) & ", " _
                                 & par.VirgoleInPunti(par.IfNull(myReaderX("num_vani"), "null")) & ", " & par.VirgoleInPunti(par.IfNull(myReaderX("superficie_catastale"), "null")) _
                                 & ", " & par.VirgoleInPunti(par.IfNull(myReaderX("rendita_storica"), "null")) & ", " & par.VirgoleInPunti(par.IfNull(myReaderX("immobile_storico"), "null")) _
                                 & ", " & par.VirgoleInPunti(par.IfNull(myReaderX("reddito_dominicale"), "null")) & ", " & par.VirgoleInPunti(par.IfNull(myReaderX("valore_imponibile"), "null")) _
                                 & ",  " & par.VirgoleInPunti(par.IfNull(myReaderX("reddito_agrario"), "null")) & ", " & par.VirgoleInPunti(par.IfNull(myReaderX("valore_bilancio"), "null")) _
                                 & ",null, '" & par.IfNull(myReaderX("data_acquisizione"), "") & "', '" & par.IfNull(myReaderX("data_fine_validita"), "null") _
                                 & "', '" & par.IfNull(myReaderX("ditta"), "") & "', '" & par.IfNull(myReaderX("num_partita"), "") & "', NULL, NULL, NULL, NULL,  NULL, NULL, '" _
                                 & par.PulisciStrSql(par.IfNull(myReaderX("indirizzo"), "")) & "', '" & par.IfNull(myReaderX("civico"), "") _
                                 & "', '" & par.IfNull(myReaderX("cap"), "") & "', NULL, '" & par.IfNull(myReaderX("cod_comune"), "") & "', NULL, NULL)"
                par.cmd.ExecuteNonQuery()
            Loop
            myReaderX.Close()


            par.cmd.CommandText = "UPDATE SISCOM_MI.INTESTATARI_RAPPORTO SET ID_CONTRATTO=" & indiceContratto & " WHERE ID_CONTRATTO=" & IdContratto
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & indiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F01','contratto creato in seguito ad assegnazione definitiva')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & IdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F189','creazione nuovo contratto ERP - cod." & NuovoCodiceContratto & "')"
            par.cmd.ExecuteNonQuery()


            sNuovoCodiceRapporto = NuovoCodiceContratto
            lNuovoIdRapporto = indiceContratto

            '************* 30/07/2012 INSERIMENTO AVVISO PER CONDOMINI ********
            par.cmd.CommandText = "SELECT cond_edifici.* FROM SISCOM_MI.COND_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & idUI & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND COND_EDIFICI.ID_EDIFICIO=EDIFICI.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO,ID_CONTRATTO) VALUES (2," & idUI & ",'" & Format(Now, "yyyyMMdd") & "',0," & myReader("ID_CONDOMINIO") & "," & IdContratto & ")"
                par.cmd.ExecuteNonQuery()
            Loop
            myReader.Close()
            '************* 30/07/2012 FINE SCRITTURA EVENTO PER CONDOMINI ********




            'par.cmd.CommandText = "update siscom_mi.rapporti_utenza set FL_ASSEGN_DEF = 1,DATA_ASSEGN_DEF='" & Format(Now, "yyyymmdd") & "' WHERE ID=" & indiceContratto
            'par.cmd.ExecuteNonQuery()

            If Not IsNothing(Session.Item("lIdConnessione")) Then
                Dim par1 As New CM.Global
                par1.OracleConn = CType(HttpContext.Current.Session.Item(Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleConnection)
                par1.cmd = par1.OracleConn.CreateCommand()
                par1.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleTransaction)
                ‘'par1.cmd.Transaction = par1.myTrans

                par1.cmd.CommandText = "update siscom_mi.rapporti_utenza set FL_ASSEGN_DEF = 1,DATA_ASSEGN_DEF='" & Format(Now, "yyyymmdd") & "' WHERE ID=" & IdContratto
                par1.cmd.ExecuteNonQuery()

                par1.myTrans.Commit()
                par1.myTrans = par1.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & Session.Item("lIdConnessione"), par1.myTrans)
                par1.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE1111", par.myTrans)

            Response.Write("<script>alert('Operazione Effettuata con successo!');</script>")
            btnProcedi.Visible = False
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            HttpContext.Current.Session.Remove("TRANSAZIONE1111")
            HttpContext.Current.Session.Remove("CONNESSIONE1111")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub AggiungiComponente(ByVal idDichiarazione As Long, ByVal idContratto As Long)
        Try
            par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.ID,progr,id_dichiarazione,cod_fiscale,cognome,nome,sesso,data_nascita,perc_inval,indennita_acc,grado_parentela," _
                & "UTENZA_DICHIARAZIONI.id_luogo_res_dnte,UTENZA_DICHIARAZIONI.id_tipo_ind_res_dnte,UTENZA_DICHIARAZIONI.ind_res_dnte,UTENZA_DICHIARAZIONI.civico_res_dnte,UTENZA_DICHIARAZIONI.cap_res_dnte " _
                & "FROM UTENZA_COMP_NUCLEO,UTENZA_DICHIARAZIONI WHERE UTENZA_DICHIARAZIONI.ID=UTENZA_COMP_NUCLEO.id_dichiarazione " _
                & "AND id_dichiarazione = " & idDichiarazione
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows

                    par.cmd.CommandText = "select id from siscom_mi.anagrafica where cod_fiscale = '" & par.IfNull(row.Item("COD_FISCALE"), "") & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim idAnagrafico As Long = 0
                    Dim codParentela As String = ""
                    Dim codTipoOccup As String = ""
                    If myReader.Read Then
                        idAnagrafico = par.IfNull(myReader("ID"), 0)
                    End If
                    myReader.Close()

                    If idAnagrafico = 0 Then
                        idAnagrafico = InserInAnagrafica(row, idContratto)
                    End If

                    If par.IfNull(row.Item("PROGR"), 0) = 0 Then
                        codTipoOccup = "INTE"
                    Else
                        codTipoOccup = "ALTR"
                    End If

                    '****** GRADO PARENTELA ******
                    par.cmd.CommandText = "select * from t_tipo_parentela where cod=" & par.IfNull(row.Item("grado_parentela"), "")
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        codParentela = par.IfNull(myReader("cod_siscom_mi"), "")
                    End If
                    myReader.Close()
                    '****** fine GRADO PARENTELA ******

                    par.cmd.CommandText = "select * from SISCOM_MI.soggetti_contrattuali where id_contratto=" & idContratto & " AND ID_ANAGRAFICA=" & idAnagrafico
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read = False Then
                        par.cmd.CommandText = "insert into SISCOM_MI.soggetti_contrattuali " _
                        & "(id_anagrafica,id_contratto,cod_tipologia_parentela,cod_tipologia_occupante,cod_tipologia_titolo,data_inizio) values" _
                        & "(" & idAnagrafico & "," & idContratto & ",'" & codParentela & "','" & codTipoOccup & "','LEGIT','" & par.AggiustaData(txtDataStipula.Text) & "')"
                        par.cmd.ExecuteNonQuery()
                    Else
                        ConfrontaComponente(row, idContratto, idAnagrafico)
                    End If

                Next
            End If

            'EliminaComponente(idDichiarazione, idContratto)

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ConfrontaComponente(ByVal r As Data.DataRow, ByVal idContratto As Long, ByVal idAnagrafico As Long)

        Dim cittadinanza As String = ""
        Dim residenza As String = ""
        Dim comuresid As String = ""
        Dim provresid As String = ""
        Dim indirizzresid As String = ""
        Dim idrecapito As String = ""
        Dim idindrecapito As String = ""
        Dim codComuRecap As String = ""

        Try
            '*************** campo CITTADINANZA **************
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
            par.cmd.CommandText = "select * from comuni_nazioni where id=" & par.IfNull(r.Item("id_luogo_res_dnte"), "")
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                comuresid = par.PulisciStrSql(par.IfNull(lettore("nome"), ""))
                provresid = par.IfNull(lettore("sigla"), "")
                residenza = comuresid & " (" & provresid & ") CAP " & par.IfNull(r.Item("cap_res_dnte"), "") & " "
            End If
            lettore.Close()
            par.cmd.CommandText = "select * from t_tipo_indirizzo where cod='" & par.IfNull(r.Item("id_tipo_ind_res_dnte"), "") & "'"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                indirizzresid = par.PulisciStrSql(par.IfNull(lettore("descrizione"), "")) & " " & par.PulisciStrSql(par.IfNull(r.Item("ind_res_dnte"), ""))
                residenza &= indirizzresid & ", " & par.IfNull(r.Item("civico_res_dnte"), "")
            End If
            lettore.Close()
            '*************** fine RESIDENZA **************


            '********* campo ID_INDIRIZZO_RECAPITO **********
            par.cmd.CommandText = "select SISCOM_MI.SEQ_INDIRIZZI.nextval from dual"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                idindrecapito = lettore(0)
            End If
            lettore.Close()

            par.cmd.CommandText = "select * from SISCOM_MI.rapporti_utenza where id=" & idContratto
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                par.cmd.CommandText = "select cod from comuni_nazioni where nome = '" & par.PulisciStrSql(par.IfNull(lettore("luogo_cor"), "")) & "'"
                Dim lettoreCod As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreCod.Read Then
                    codComuRecap = par.IfNull(lettoreCod("cod"), "")
                End If
                lettoreCod.Close()

                par.cmd.CommandText = "insert into SISCOM_MI.indirizzi_anagrafica (id,descrizione,civico,cap,localita," _
                    & "cod_comune) values (" & idindrecapito & ",'" & par.PulisciStrSql(par.IfNull(lettore("tipo_cor"), "")) & " " _
                    & par.PulisciStrSql(par.IfNull(lettore("via_cor"), "")) & "','" & par.IfNull(lettore("civico_cor"), "") & "','" & par.IfNull(lettore("cap_cor"), "") & "'," _
                    & "'" & par.PulisciStrSql(par.IfNull(lettore("luogo_cor"), "")) & "','" & codComuRecap & "') "
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select id from SISCOM_MI.indirizzi_anagrafica where descrizione='" & par.PulisciStrSql(par.IfNull(lettore("tipo_cor"), "")) & " " _
                    & par.PulisciStrSql(par.IfNull(lettore("via_cor"), "")) & "' and civico ='" & par.IfNull(lettore("civico_cor"), "") & "'"
                Dim lettoreInd As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreInd.Read Then
                    idrecapito = par.IfNull(lettoreInd("id"), " ")
                End If
                lettoreInd.Close()
            End If
            lettore.Close()
            '************** fine ID_INDIRIZZO_RECAPITO *************



            '************* AGGIORNAMENTO IN ANAGRAFICA **************

            par.cmd.CommandText = "UPDATE SISCOM_MI.ANAGRAFICA SET " _
                & "COGNOME= '" & par.PulisciStrSql(LTrim(RTrim(par.IfNull(r.Item("COGNOME"), "")))) & "'," _
                & "NOME= '" & par.PulisciStrSql(LTrim(RTrim(par.IfNull(r.Item("NOME"), "")))) & "'," _
                & "COD_FISCALE= '" & par.PulisciStrSql(par.IfNull(r.Item("cod_fiscale"), "")) & "'," _
                & "CITTADINANZA= '" & cittadinanza & "'," _
                & "RESIDENZA= '" & residenza & "'," _
                & "DATA_NASCITA= '" & par.IfNull(r.Item("DATA_NASCITA"), "") & "'," _
                & "COD_COMUNE_NASCITA= '" & par.IfNull(r.Item("COD_FISCALE"), "F205").ToString.Substring(11, 4) & "'," _
                & "SESSO= '" & par.PulisciStrSql(par.IfNull(r.Item("sesso"), "")) & "'," _
                & "ID_INDIRIZZO_RECAPITO = '" & idrecapito & "'," _
                & "COMUNE_RESIDENZA = '" & comuresid & "'," _
                & "PROVINCIA_RESIDENZA= '" & provresid & "'," _
                & "INDIRIZZO_RESIDENZA= '" & indirizzresid & "'," _
                & "CIVICO_RESIDENZA= '" & par.IfNull(r.Item("civico_res_dnte"), "") & "'," _
                & "CAP_RESIDENZA= '" & par.IfNull(r.Item("cap_res_dnte"), "") & "'," _
                & "TIPO_R=0 " _
                & "WHERE ID= " & idAnagrafico
            par.cmd.ExecuteNonQuery()
            '************* fine INSERIMENTO IN ANAGRAFICA **************

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function InserInAnagrafica(ByVal r As Data.DataRow, ByVal idContratto As Long)
        Dim IdAna As String = ""
        Dim cittadinanza As String = ""
        Dim residenza As String = ""
        Dim comuresid As String = ""
        Dim provresid As String = ""
        Dim indirizzresid As String = ""
        Dim idrecapito As String = ""
        Dim idindrecapito As String = ""
        Dim codComuRecap As String = ""

        Try
            '*************** campo CITTADINANZA **************
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
            par.cmd.CommandText = "select * from comuni_nazioni where id=" & par.IfNull(r.Item("id_luogo_res_dnte"), "")
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                comuresid = par.PulisciStrSql(par.IfNull(lettore("nome"), ""))
                provresid = par.IfNull(lettore("sigla"), "")
                residenza = comuresid & " (" & provresid & ") CAP " & par.IfNull(r.Item("cap_res_dnte"), "") & " "
            End If
            lettore.Close()
            par.cmd.CommandText = "select * from SISCOM_MI.t_tipo_indirizzo where cod='" & par.IfNull(r.Item("id_tipo_ind_res_dnte"), "") & "'"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                indirizzresid = par.PulisciStrSql(par.IfNull(lettore("descrizione"), "")) & " " & par.PulisciStrSql(par.IfNull(r.Item("ind_res_dnte"), ""))
                residenza &= indirizzresid & ", " & par.IfNull(r.Item("civico_res_dnte"), "")
            End If
            lettore.Close()
            '*************** fine RESIDENZA **************


            '********* campo ID_INDIRIZZO_RECAPITO **********
            par.cmd.CommandText = "select SISCOM_MI.SEQ_INDIRIZZI.nextval from dual"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                idindrecapito = lettore(0)
            End If
            lettore.Close()

            par.cmd.CommandText = "select * from SISCOM_MI.rapporti_utenza where id=" & idContratto
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                par.cmd.CommandText = "select cod from comuni_nazioni where nome = '" & par.PulisciStrSql(par.IfNull(lettore("luogo_cor"), "")) & "'"
                Dim lettoreCod As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreCod.Read Then
                    codComuRecap = par.IfNull(lettoreCod("cod"), "")
                End If
                lettoreCod.Close()

                par.cmd.CommandText = "insert into SISCOM_MI.indirizzi_anagrafica (id,descrizione,civico,cap,localita," _
                    & "cod_comune) values (" & idindrecapito & ",'" & par.PulisciStrSql(par.IfNull(lettore("tipo_cor"), "")) & " " _
                    & par.PulisciStrSql(par.IfNull(lettore("via_cor"), "")) & "','" & par.IfNull(lettore("civico_cor"), "") & "','" & par.IfNull(lettore("cap_cor"), "") & "'," _
                    & "'" & par.PulisciStrSql(par.IfNull(lettore("luogo_cor"), "")) & "','" & codComuRecap & "') "
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select id from SISCOM_MI.indirizzi_anagrafica where descrizione='" & par.PulisciStrSql(par.IfNull(lettore("tipo_cor"), "")) & " " _
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
            par.cmd.CommandText = "select SISCOM_MI.SEQ_ANAGRAFICA.nextval from dual"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                IdAna = lettore(0)
            End If
            lettore.Close()
            par.cmd.CommandText = "insert into SISCOM_MI.anagrafica (id,cognome,nome,data_nascita,cod_fiscale,sesso,cod_comune_nascita,cittadinanza,residenza,id_indirizzo_recapito," _
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

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return IdAna

    End Function

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
