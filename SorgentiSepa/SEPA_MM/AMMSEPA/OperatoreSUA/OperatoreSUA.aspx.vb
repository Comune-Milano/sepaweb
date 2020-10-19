﻿
Partial Class AMMSEPA_OperatoreSUA_OperatoreSUA
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Function Eventi_Gestione(ByVal operatore As String, ByVal cod_evento As String, ByVal motivazione As String) As Integer
        Dim data As String = Format(Now, "yyyyMMddHHmmss")
        Try
            par.cmd.CommandText = "INSERT INTO EVENTI_GESTIONE (ID_OPERATORE, COD_EVENTO, DATA_ORA, MOTIVAZIONE) VALUES" _
                            & " (" & operatore & ",'" & cod_evento & "'," & data & ",'" & motivazione & "')"
            par.cmd.ExecuteNonQuery()
        Catch ex As Exception
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
        Return 0
    End Function

    Public Property lid_Operatore() As Long
        Get
            If Not (ViewState("par_lid_Operatore") Is Nothing) Then
                Return CLng(ViewState("par_lid_Operatore"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lid_Operatore") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:500px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()

        Dim filiale As String = 0
        filiale = cmbFiliale.SelectedValue
        If filiale = "" Then
            filiale = 0
        End If
        Session.Remove("cmbFiliale")
        Session.Add("cmbFiliale", filiale)

        If Not IsPostBack Then
            Try
                Dim id_caf1 As String = Session.Item("ID_CAF")
                Dim id_caf2 As String = ""
                Dim fl_responsabile_ente As String = Session.Item("RESPONSABILE")
                lid_Operatore = CLng(Request.QueryString("ID"))
                par.RiempiDList(Me, par.OracleConn, "cmbEnte", "SELECT * FROM CAF_WEB WHERE ID IN (2,63)", "COD_CAF", "ID")
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbFiliale", "SELECT id,nome FROM siscom_mi.tab_filiali order by nome", "NOME", "ID")
                par.RiempiDList(Me, par.OracleConn, "cmbFornitori", "SELECT DISTINCT fornitori.ID, RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI ORDER BY fornitori.RAGIONE_SOCIALE ASC", "RAGIONE_SOCIALE", "ID")
                cmbFornitori.Items.Add(New ListItem("QUALSIASI FORNITORE", "NULL"))

                If lid_Operatore <> -1 Then
                    ApriConnessione()
                    par.cmd.CommandText = "select * from OPERATORI where id=" & lid_Operatore
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        id_caf2 = par.IfNull(myReader("ID_CAF"), "")
                    End If
                    myReader.Close()
                    par.Dispose()
                    ChiudiConnessione()
                    Nuovo_Operatore = 0
                    If id_caf1 = 2 And fl_responsabile_ente = 1 And (id_caf2 = 2 Or id_caf2 = 63) Then
                        Visualizza()
                    Else
                        Response.Write("<script>alert('Non hai i permessi per gestire questo operatore!');document.location.href='../pagina_home.aspx';</script>")
                    End If
                Else
                    CreaNuovo()
                    Nuovo_Operatore = 1

                End If
                FunctionLivello()
                CaricaAttributi()
                IdOperatore.Value = lid_Operatore
                If OperatoreFornitori = 1 Then
                    DisattivaAutorizzazioni()
                    ChModFornitori.Enabled = True
                    cmbFornitori.Enabled = True
                    ChFO_RDO.Enabled = True
                    ChFO_ODL.Enabled = True
                    ChFO_RPT.Enabled = True
                    ChFO_PAR.Enabled = True
                    ChFO_SLE.Enabled = True
                    'MAX 20/12/2017
                    ChFO_LOG.Enabled = True
                End If
                If id_caf2 = "63" Then
                    DisattivaAutorizzazioni()
                    chGC.Enabled = True
                    ChGC_SL.Enabled = True
                    ChGC_Segnalazioni.Enabled = True
                    ChGC_Supervisore.Enabled = True
                    ChGC_TabelleSupporto.Enabled = True
                    ChGC_Appuntamenti.Enabled = True
                    ChGC_Report.Enabled = True
                    ChGC_Calendario.Enabled = True
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Private Function CaricaAttributi()

        'VENGONO CARICATI GLI ATTRIBUTI "CORRENTE" (VALORE CORRENTE) E "NOME" (NOME DEL CAMPO)
        'MENTRE IL VALORE "CORRENTE" VIENE CARICATO AUTOMATCAMENTE (SOLO PER CHECKBOX, TEXTBOX E DROPDOWNLIST)
        'IL VALORE DELL'ATTRIBUTO "NOME" VIENE CARICATO MANUALMENTE, IN MODO DA INSERIRE DEL TESTO 
        'PIU' SIGNIFICATIVO E NON SEMPLICEMENTE LA PROPRIETA' TEXT

        Dim CTRL As Control = Nothing

        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("CORRENTE", UCase(DirectCast(CTRL, TextBox).Text))
            End If
            If TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("CORRENTE", Valore01(DirectCast(CTRL, CheckBox).Checked))
            End If
            If TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("CORRENTE", UCase(DirectCast(CTRL, DropDownList).SelectedItem.Text))
            End If
        Next

        'attributi nome da memorizzare
        Me.txtUtente.Attributes.Add("NOME", "OPERATORE")
        Me.txtCognome.Attributes.Add("NOME", "COGNOME")
        Me.txtNome.Attributes.Add("NOME", "NOME")
        Me.txtCF.Attributes.Add("NOME", "CODICE FISCALE")
        Me.txtAnagrafico.Attributes.Add("NOME", "CODICE ANAGRAFICO")
        Me.txtScadenzaPW.Attributes.Add("NOME", "SCADENZA PASSWORD")
        'Me.ChAnagrafe.Attributes.Add("NOME", "INTERROGAZIONE ANAGRAFE")

        Me.cmbEnte.Attributes.Add("NOME", "ENTE")
        Me.cmbFiliale.Attributes.Add("NOME", "STRUTTURA")
        Me.chkcambiostruttura.Attributes.Add("NOME", "ABILITATO CAMBIO STRUTTURA")



        Me.Chau.Attributes.Add("NOME", "MODULO ANAGRAFE UTENZA")
        Me.ChAUconsulta.Attributes.Add("NOME", "AU SOLO LETTURA")
        Me.ChAUpropDec.Attributes.Add("NOME", "AU PROPOSTA DECADENZA")
        Me.ChAUCreaGruppo.Attributes.Add("NOME", "AU CREA GRUPPO DI LAVORO")

        Me.ChAUDocNecessaria.Attributes.Add("NOME", "AU DOCUMENTAZIONE NECESSARIA")
        Me.ChAUDiffida.Attributes.Add("NOME", "AU DIFFIDA MANCATA PRESENTAZIONE")
        Me.ChAUSimulaApplica.Attributes.Add("NOME", "AU SIMULA/APPLICA GRUPPO DI LAVORO")

        Me.ChAUGestione.Attributes.Add("NOME", "AU GESTIONE ANAGRAFE UTENZA APERTURA-CHIUSURA")
        Me.ChAUGestione0.Attributes.Add("NOME", "AU GESTIONE ANAGRAFE UTENZA ELIMINA")
        Me.ChAUGestioneMod.Attributes.Add("NOME", "AU GESTIONE MODELLI COMUNICAZIONE")
        Me.ChAUGestioneStr.Attributes.Add("NOME", "AU GESTIONE ASSOCIAZIONE STR./SPORT./OPER.")
        Me.ChAUGestioneEsclusione.Attributes.Add("NOME", "AU GESTIONE MOTIVI CONVOCABILI")
        Me.ChAUGestioneConvocabili.Attributes.Add("NOME", "AU GESTIONE CONVOCABILI")
        Me.ChAUGestioneGrConv.Attributes.Add("NOME", "AU GESTIONE GRUPPI CONVOCABILI")
        Me.ChAUGestioneLista.Attributes.Add("NOME", "AU GESTIONE LISTE CONVOCAZIONE")
        Me.ChAUDefConv.Attributes.Add("NOME", "AU SIMULAZIONE/DEFINITIVO CONVOCAZIONI")
        Me.ChAUEliminaConv.Attributes.Add("NOME", "AU ELIMINA FILE CONVOCAZIONI")
        Me.ChAUConvTutti.Attributes.Add("NOME", "CONVOCAZIONI AU-RICERCA TUTTI")
        Me.ChAUConvIns.Attributes.Add("NOME", "CONVOCAZIONI AU-INSERIMENTO SCHEDA AU")
        Me.ChAUConvSposta.Attributes.Add("NOME", "CONVOCAZIONI AU-SPOSTA APPUNTAMENTO")
        Me.ChAUConvAnnulla.Attributes.Add("NOME", "CONVOCAZIONI AU-ANNULLA APPUNTAMENTO")
        Me.ChAUConvRip.Attributes.Add("NOME", "CONVOCAZIONI AU-RIPRISTINA APPUNTAMENTO ANNULLATO")
        Me.ChAUConvReimposta.Attributes.Add("NOME", "CONVOCAZIONI AU-REIMPOSTA APPUNTAMENTO ANNULLATO")
        Me.ChAUConvSind.Attributes.Add("NOME", "CONVOCAZIONI AU-SOSPESE SINDACATI")
        Me.ChAUAnnullaStampa.Attributes.Add("NOME", "DIFFIDE AU-ANNULLA STAMPA LETTERE")


        Me.Chabbinamento.Attributes.Add("NOME", "MODULO ABBINAMENTO")
        Me.cERP.Attributes.Add("NOME", "ABBINAMENTO ERP")
        Me.c392.Attributes.Add("NOME", "ABINAMENTO 392")
        Me.c431.Attributes.Add("NOME", "ABBINAMENTO 431")
        Me.cUD.Attributes.Add("NOME", "ABBINAMENTO U.D.")
        Me.cOA.Attributes.Add("NOME", "ABBINAMENTO OCCUPAZIONI ABUSIVE")
        Me.ChFO.Attributes.Add("NOME", "ABBINAMENTO F.O.")
        Me.ChCS.Attributes.Add("NOME", "ABBINAMENTO C.S.")
        Me.ChConvenzionato.Attributes.Add("NOME", "ABBINAMENTO CANONE CONV.")


        Me.ChABB_DEC.Attributes.Add("NOME", "MODULO GESTIONE LOCATARI")
        Me.ch_OP_VSA.Attributes.Add("NOME", "GESTIONE LOCATARI-OPERATORE")
        Me.ch_OP_RESP_VSA.Attributes.Add("NOME", "GESTIONE LOCATARI-PARERE DECISIONALE")

        Me.ChABB_EMRI.Attributes.Add("NOME", "MODULO CAMBI ALLOGGIO")



        Me.chPED2Completa.Attributes.Add("NOME", "MODULO ANAGRAFE DEL PATRIMONIO")
        Me.ChAPConsultazione.Attributes.Add("NOME", "A.P.-SOLO LETTURA")
        Me.CHPED2ESTERNA.Attributes.Add("NOME", "A.P.-SOLO IV E V LOTTO")
        Me.MOD_CENS_MANUT.Attributes.Add("NOME", "A.P.-STATO MANUTENTIVO")
        Me.ChCensManutSL.Attributes.Add("NOME", "A.P.-STATO MANUTENTIVO SOLO LETTURA")
        chkCaricMassProgrInt.Attributes.Add("NOME", "ANAGRAFE DEL PATRIMONIO - CARICAMENTO MASSIVO PROGRAMMA INTERVENTI")

        Me.chContabilita.Attributes.Add("NOME", "MODULO CONTABILITA'")
        Me.ChContRagioneria.Attributes.Add("NOME", "CONT.-RAGIONERIA")
        Me.ChContPatrimoniali.Attributes.Add("NOME", "CONT.-CONS.PATRIMONIALI")
        Me.ChContFlussi.Attributes.Add("NOME", "CONT.-FLUSSI FINANZIARI")
        Me.ChContRimborsi.Attributes.Add("NOME", "CONT.-RIMBORSO SPESE GESTORE")
        Me.ChContCompensi.Attributes.Add("NOME", "CONT.-COMPENSI GESTORE")

        Me.chCicloP.Attributes.Add("NOME", "MODULO CICLO PASSIVO")
        Me.ChBPNuovo.Attributes.Add("NOME", "C.P.-B.P. NUOVO PIANO FINANZIARIO")
        Me.ChBPGenerale.Attributes.Add("NOME", "C.P.-B.P. GENERALE")
        Me.ChSpReversibili.Attributes.Add("NOME", "C.P.-B.P. SPESE REVERSIBILI")
        Me.ChBPResidui.Attributes.Add("NOME", "C.P.-B.P. RESIDUI")
        Me.ChBPAppPreventivi.Attributes.Add("NOME", "C.P.-B.P. APPLICAZIONE PREVENTIVI")
        Me.ChSpReversibiliSl.Attributes.Add("NOME", "C.P.-B.P. SPESE REVERSIBILI SOLO LETTURA")
        Me.ChBPFormalizzazione.Attributes.Add("NOME", "C.P.-B.P. FORMALIZZAZIONE")
        Me.ChBPCompilazione.Attributes.Add("NOME", "C.P.-B.P. COMPILAZIONE")
        Me.ChVariazioni.Attributes.Add("NOME", "C.P.-B.P. VARIAZIONI")
        Me.ChBPFormalizzazione0.Attributes.Add("NOME", "C.P.-B.P. FORMALIZZAZIONE SOLO LETTURA")
        Me.ChBPCompilazione0.Attributes.Add("NOME", "C.P.-B.P. COMPILAZIONE SOLO LETTURA")
        Me.ChVariazioniSL.Attributes.Add("NOME", "C.P.-B.P. VARIAZIONI SOLO LETTURA")
        Me.ChBPConvalidaAler.Attributes.Add("NOME", "C.P.-B.P. CONVALIDA GESTORE")
        Me.ChBPCapitoli.Attributes.Add("NOME", "C.P.-B.P. ASSEGNAZIONE CAPITOLI")
        Me.ChBPConvalidaComune.Attributes.Add("NOME", "C.P.-B.P. CONVALIDA COMUNE")
        Me.ChBPConvalidaAler0.Attributes.Add("NOME", "C.P.-B.P. CONVALIDA GESTORE SOLO LETTURA")
        Me.ChBPCapitoli0.Attributes.Add("NOME", "C.P.-B.P. ASSEGNAZIONE CAPITOLI SOLO LETTURA")
        Me.ChBPConvalidaComune0.Attributes.Add("NOME", "C.P.-B.P. CONVALIDA COMUNE SOLO LETTURA")
        Me.ChBPVociServizio.Attributes.Add("NOME", "C.P.-B.P. GESTIONE VOCI SERVIZI")
        Me.ChASSNuovo.Attributes.Add("NOME", "C.P.-B.P. NUOVO ASSESTAMENTO")
        Me.ChASSConvAler.Attributes.Add("NOME", "C.P.-B.P. CONVALIDA ASSEST. GESTORE")
        Me.ChBPVociServizio0.Attributes.Add("NOME", "C.P.-B.P. VOCI SERVIZI SOLO LETTURA")
        Me.ChASSCompila.Attributes.Add("NOME", "C.P.-B.P. COMPILA ASSESTAMENTO")
        Me.ChASSConvComune.Attributes.Add("NOME", "C.P.-B.P. CONVALIDA ASSESTAMENTO COMUNE")
        Me.ChMS.Attributes.Add("NOME", "C.P.-MANUTENZIONI E SERVIZI")
        Me.ChOP.Attributes.Add("NOME", "C.P.-ORDINI E PAGAMENTI")
        Me.ChPC.Attributes.Add("NOME", "C.P.-PAGAMENTI A CANONE")
        Me.ChMSL.Attributes.Add("NOME", "C.P.-MANUTENZIONI E SERVIZI SOLO LETTURA")
        Me.ChOPL.Attributes.Add("NOME", "C.P.-ORDINI E PAGAMENTI SOLO LETTURA")

        Me.chkCambioIvaOdl.Attributes.Add("NOME", "CAMBIO IVA ODL")
        'Rielabora CDP - SAL
        Me.chkRielaboraCDP.Attributes.Add("NOME", "C.P-RIELABORA CDP DA MANUTENZIONI")
        Me.chkRielaboraSAL.Attributes.Add("NOME", "C.P-RIELABORA SAL DA MANUTENZIONI")
        Me.chkPagRielabCDP.Attributes.Add("NOME", "C.P-RIELABORA CDP DA ORDINI E PAGAMENTI")
        Me.chkPagCanoneRielCDP.Attributes.Add("NOME", "C.P-RIELABORA CDP DA PAGAMENTI A CANONE")
        Me.chkPagCanoneRielSAL.Attributes.Add("NOME", "C.P-RIELABORA SAL DA PAGAMENTI A CANONE")
        Me.chkRRSRielabCDP.Attributes.Add("NOME", "C.P-RIELABORA CDP DA GESTIONE ESCOMI")
        Me.chkRRSRielabSAL.Attributes.Add("NOME", "C.P-RIELABORA SAL DA GESTIONE ESCOMI")

        Me.ChPCL.Attributes.Add("NOME", "C.P.-PAGAMENTI A CANONE SOLO LETTURA")
        Me.ChLO.Attributes.Add("NOME", "C.P.-LOTTI")
        Me.ChCC.Attributes.Add("NOME", "C.P.-CONTRATTI")

        Me.ChBuildingM.Attributes.Add("NOME", "C.P.-BUILDING MANAGER")
        Me.ChCCV.Attributes.Add("NOME", "C.P.-CONTRATTI VARIAZIONE CONFIGURAZIONE PATR.")
        Me.ChLOL.Attributes.Add("NOME", "C.P.-LOTTI SOLO LETTURA")
        Me.ChCCL.Attributes.Add("NOME", "C.P.-CONTRATTI SOLO LETTURA")
        Me.chRSS.Attributes.Add("NOME", "C.P.-RSS")
        Me.CHMAND_PAG.Attributes.Add("NOME", "C.P.-MANDATI DI PAGAMENTO")
        Me.chRSSSl.Attributes.Add("NOME", "C.P.-RSS SOLO LETTURA")
        Me.chAutorizzazione.Attributes.Add("NOME", "C.P.-DIRETTORE LAVORI")
        Me.ChEstraiSTR.Attributes.Add("NOME", "C.P.-ESTRAZIONE STR")
        Me.ChImportaSTR.Attributes.Add("NOME", "C.P.-CONSUNTIVAZIONE STR")
        Me.chModPagamento.Attributes.Add("NOME", "C.P.-MODALITA DI PAGAMENTO")
        Me.ChVariazioneImporti.Attributes.Add("NOME", "C.P.-VARIAZIONE IMPORTI")
        Me.chVariazioneComp.Attributes.Add("NOME", "C.P.-VARIAZIONE COMPOSIZIONE APPALTO")
        Me.chDashboard.Attributes.Add("NOME", "C.P.-DASHBOARD")
        Me.chAnticipoSingolaVoce.Attributes.Add("NOME", "C.P.-ANTICIPO SU SINGOLA VOCE")
        Me.chTecnicoAmm.Attributes.Add("NOME", "C.P.-TECNICO AMMINISTRATIVO")
        Me.chFQM.Attributes.Add("NOME", "C.P.-FIELD QUALITY MANAGER")
        chkRitornaInBozza.Attributes.Add("NOME", "C.P.-RITORNA IN BOZZA")
        Me.chkGestAllegati.Attributes.Add("NOME", "GESTIONE ALLEGATI")

        Me.chkUtenze.Attributes.Add("NOME", "UTENZE/MULTE/CUSTODI")
        Me.chSuperDirettore.Attributes.Add("NOME", "C.P.-GESTORE DIRETTORI LAVORI")


        Me.chGC.Attributes.Add("NOME", "MODULO AGENDA E SEGNALAZIONI")
        Me.ChGC_SL.Attributes.Add("NOME", "AGENDA E SEGNALAZIONI-SOLO LETTURA")
        Me.ChGC_TabelleSupporto.Attributes.Add("NOME", "AGENDA E SEGNALAZIONI-GEST.TABELLE SUPPORTO")
        Me.ChGC_Report.Attributes.Add("NOME", "AGENDA E SEGNALAZIONI-REPORT")
        Me.ChGC_Segnalazioni.Attributes.Add("NOME", "AGENDA E SEGNALAZIONI-INSERIMENTO/MODIFICA SEGNALAZIONI")
        Me.ChGC_Appuntamenti.Attributes.Add("NOME", "AGENDA E SEGNALAZIONI-INSERIMENTO/MODIFICA APPUNTAMENTI")
        Me.ChGC_Appuntamenti.Attributes.Add("NOME", "AGENDA E SEGNALAZIONI-GESTIONE CALENDARIO")
        Me.ChGC_Appuntamenti.Attributes.Add("NOME", "AGENDA E SEGNALAZIONI-SUPERVISORE")


        Me.chCondominio.Attributes.Add("NOME", "MODULO CONDOMINIO")
        Me.ChCondominioSL.Attributes.Add("NOME", "CONDOMINI-SOLO LETTURA")

        Me.chDemanio.Attributes.Add("NOME", "MODULO IMPIANTI")
        Me.ChDemanioSL.Attributes.Add("NOME", "IMPIANTI-SOLO LETTURA")
        Me.ChDemEl.Attributes.Add("NOME", "IMPIANTI-ELETTRICO")
        Me.ChDemTA.Attributes.Add("NOME", "IMPIANTI-TERMICO AUTONOMO")
        Me.ChDemAM.Attributes.Add("NOME", "IMPIANTI-ACQUE METEORICHE")
        Me.ChDemCF.Attributes.Add("NOME", "IMPIANTI-CANNA FUMARIA")
        Me.ChDemTV.Attributes.Add("NOME", "IMPIANTI-TV")
        Me.ChDemID.Attributes.Add("NOME", "IMPIANTI-IDRICO")
        Me.ChDemTR.Attributes.Add("NOME", "IMPIANTI-TELERISCALDAMENTO")
        Me.ChDemAI.Attributes.Add("NOME", "IMPIANTI-ANTINCENDIO")
        Me.ChDemGAS.Attributes.Add("NOME", "IMPIANTI-GAS")
        Me.ChDemTC.Attributes.Add("NOME", "IMPIANTI-CENTRALE TERMICA")
        Me.ChDemSO.Attributes.Add("NOME", "IMPIANTI-SOLLEVAMENTO")
        Me.ChDemTI.Attributes.Add("NOME", "IMPIANTI-TUTELA IMM.")
        Me.ChDemCITOFONICO.Attributes.Add("NOME", "IMPIANTI-CITOFONICO")

        Me.ChContratti.Attributes.Add("NOME", "MODULO RAPPORTI UTENZA")
        Me.ChCONTRATTILETTURA.Attributes.Add("NOME", "R.U-SOLO LETTURA")
        Me.ChCONTRATTIins.Attributes.Add("NOME", "R.U-INSERIMENTO CONTRATTI")
        Me.ChCONTRATTIreg.Attributes.Add("NOME", "R.U-REGISTRAZIONE CONTRATTI")
        Me.ChCONTRATTIimp.Attributes.Add("NOME", "R.U-CALCOLO IMPOSTE")
        Me.ChCONTRATTIist.Attributes.Add("NOME", "R.U-CALCOLO AGG. ISTAT")
        Me.ChCONTRATTItesto.Attributes.Add("NOME", "R.U-TESTO CONTRATTI")
        Me.ChCONTRATTId.Attributes.Add("NOME", "R.U-OP. SEDE TERRITORIALE/DIS./REC.UI")
        Me.ChCONTRATTIint.Attributes.Add("NOME", "R.U-CALCOLO INTERESSI LEGALI")
        Me.ChCONTRATTIMor.Attributes.Add("NOME", "R.U-MOROSITA")
        Me.ChCONTRATTIRinnoviUSD.Attributes.Add("NOME", "R.U-RINNOVO USD")
        Me.ChCONTRATTICambiBox.Attributes.Add("NOME", "R.U-CAMBIO INTESTAZIONE BOX")
        Me.ChCONTRATTIPEXTRA.Attributes.Add("NOME", "R.U-INS. PAGAMENTO EXTRAMAV")
        Me.ChCONTRATTIRateizzazione.Attributes.Add("NOME", "R.U-RATEIZZAZIONE BOLLETTE")
        Me.ChkAnnullaRAT.Attributes.Add("NOME", "ANNULLA RATEIZZAZIONE")
        Me.ChSpostaAnnullo.Attributes.Add("NOME", "R.U-SPOSTAMENTO/ANNULLAMENTO")
        Me.ChAmmRptPagExtraMav.Attributes.Add("NOME", "R.U-SUPERVISORE REPORT PAG. EXTRAMAV")
        Me.ChElaborazMass.Attributes.Add("NOME", "R.U-ELABORAZIONE MASSIVA PARTITE GESTIONALI")
        Me.ChElaborazSing.Attributes.Add("NOME", "ELABORAZIONE SINGOLO DOC. GESTIONALE")
        Me.ChTrasfPag.Attributes.Add("NOME", "R.U-TRASFER. PAGAMENTO IN NUOVO RU")
        Me.chDistanzaKM.Attributes.Add("NOME", "R.U-DISTANZA KM COMUNI")
        Me.chRecaGest.Attributes.Add("NOME", "PARAMETRIZZAZIONE RECA")
        Me.chRUAU.Attributes.Add("NOME", "REPORT RU-AU")
        Me.ChNewBoll.Attributes.Add("NOME", "CREAZIONE/MODIFICA BOLLETTA")
        Me.chRUSALDI.Attributes.Add("NOME", "REPORT RU-SALDI")
        Me.chRUExport.Attributes.Add("NOME", "EXPORT RICERCA CONTRATTI")

        Me.ChGE.Attributes.Add("NOME", "MODULO GESTIONE AUTONOMA")
        Me.ChGEL.Attributes.Add("NOME", "GESTIONE AUTONOMA SOLO LETTURA")

        Me.ChMOR.Attributes.Add("NOME", "MODULO GESTIONE MOROSITA")
        Me.ChMOR_SL.Attributes.Add("NOME", "GEST. MOROSITA SOLO LETTURA")
        Me.ChMOR_ANN.Attributes.Add("NOME", "GEST. MOROSITA ANNULLA")

        Me.ChSatisfSuperVisore.Attributes.Add("NOME", "MODULO CUSTOMER SATISFACTION")
        Me.ChSatisfSuperVisore.Attributes.Add("NOME", "CUSTOMER S. SOLO LETTURA")
        Me.ChSatisfSuperVisore.Attributes.Add("NOME", "CUSTOMER S. SUPERVISORE")


        'MAX 08/05/2015
        Me.ChArchivio.Attributes.Add("NOME", "MODULO ARCHIVIO")
        Me.ChArchIM.Attributes.Add("NOME", "MODULO ARCHIVIO-INSERIMENTO/MODIFICA")
        Me.ChArchC.Attributes.Add("NOME", "MODULO ARCHIVIO-CANCELLAZIONE")

        'MAX 12/05/2015
        Me.chModRilievo.Attributes.Add("NOME", "MODULO RILIEVO")
        Me.chRilievoGEST.Attributes.Add("NOME", "MODULO RILIEVO-GESTIONE")
        Me.chRilievoCDati.Attributes.Add("NOME", "MODULO RILIEVO-CARIC. DATI")
        Me.chRilievoParam.Attributes.Add("NOME", "MODULO RILIEVO-PARAMETRI")

        'ANTONELLO
        Me.ChSiraper.Attributes.Add("NOME", "MODULO SIRAPER")
        'ANTONELLO 17/03/2017
        Me.cbARPA.Attributes.Add("NOME", "MODULO A.R.P.A.")

        'max 24/06/2015
        Me.ChRUMAV.Attributes.Add("NOME", "MODULO RU-MAV ON LINE")

        Me.chkParamCP.Attributes.Add("NOME", "CICLO PASSIVO - PARAMETRI")

        Me.ChSatisfaction.Attributes.Add("NOME", "CUSTOMER SATISFACTION")
        Me.ChSatisfactionL.Attributes.Add("NOME", "CUSTOMER SATISFACTION-SOLO LETTURA")
        Me.ChSatisfSuperVisore.Attributes.Add("NOME", "CUSTOMER SATISFACTION-SUPERVISORE")


        'annulloSal
        Me.chkAnnullaSal.Attributes.Add("NOME", "ANNULLA SAL")
        Me.chkAnnullaODL.Attributes.Add("NOME", "ANNULLA ODL")

        'max 14/10/2015
        Me.ChAUDocNecessaria.Attributes.Add("NOME", "AU DOCUMENTAZIONE NECESSARIA")
        ChAURicerca.Attributes.Add("NOME", "AU DOCUMENTAZIONE NECESSARIA")
        ChAUReport.Attributes.Add("NOME", "AU REPORT")

        ChAUCercaInquilino.Attributes.Add("NOME", "AU AGENDA - RICERCA INQUILINO")
        ChAUSospeseSind.Attributes.Add("NOME", "AU AGENDA - SOSPESE SINDACATI")
        ChAUGestMotSosp.Attributes.Add("NOME", "AU AGENDA - MOTIVI SOSPENSIONE")

        ChAUCalcolaCF.Attributes.Add("NOME", "AU CALCOLO COD. FISCALE")

        'max 20/07/2015
        Me.chRUNote.Attributes.Add("NOME", "RAPPORTI UTENZA-INSERIMENTO MASSIVO NOTE")
        ChSicurezza.Attributes.Add("NOME", "MODULO SICUREZZA")
        ChTpa_SL.Attributes.Add("NOME", "MODULO SICUREZZA-SOLO LETTURA")
        ChCreaSegn.Attributes.Add("NOME", "MODULO SICUREZZA-INSERIMENTO SEGNALAZIONI")
        ChModificaSegn.Attributes.Add("NOME", "MODULO SICUREZZA-MODIFICA SEGNALAZIONI")
        ChAssegnOperatori.Attributes.Add("NOME", "MODULO SICUREZZA-ASSEGNAZIONE OPERATORI")
        ChVisualizzaAgenda.Attributes.Add("NOME", "MODULO SICUREZZA-VISUALIZZAZIONE AGENDA")
        ChCreaSegnIntProc.Attributes.Add("NOME", "MODULO SICUREZZA-CREAZIONE SEGNALAZ./INTERV./PROCEDIM.")

        'MAX 17/02/2016
        chCrDe.Attributes.Add("NOME", "ABILITA/DISABILITA SCRITT. GESTIONALI")

        'max 26/04/2016
        Me.chRimbDep.Attributes.Add("NOME", "RIMBORSO DEPOSITO CAUZIONALE")

        'max 13/06/2016
        Me.chRUDatiAE.Attributes.Add("NOME", "RAPPORTI UTENZA-MODIFICA DATI A.E.")
        'MAX 23/02/2016
        ChModFornitori.Attributes.Add("NOME", "MODULO FORNITORI")
        cmbFornitori.Attributes.Add("NOME", "MODULO FORNITORI-IMPOSTA FORNITORE")
        ChFO_RDO.Attributes.Add("NOME", "MODULO FORNITORI-SEGNALAZIONI")
        ChFO_ODL.Attributes.Add("NOME", "MODULO FORNITORI-CALENDARIO LAVORI")
        ChFO_RPT.Attributes.Add("NOME", "MODULO FORNITORI-REPORTISTICA")
        ChFO_SLE.Attributes.Add("NOME", "MODULO FORNITORI-SOLO LETTURA")

        ChFO_PAR.Attributes.Add("NOME", "MODULO FORNITORI-PARAMETRI")



        Me.chkInserimRuoli.Attributes.Add("NOME", "R.U-INS. PAGAMENTO RUOLI")
        Me.chkReportRuoli.Attributes.Add("NOME", "R.U-REPORT PAGAMENTO RUOLI")
        Me.chkSbloccoBoll.Attributes.Add("NOME", "R.U-SBLOCCO BOLLETTAZIONE")
        Me.chkModificaANA.Attributes.Add("NOME", "ANAGRAFICA-MODIFICA DATI")
        Me.chkForzaRestituz.Attributes.Add("NOME", "R.U-FORZA RESTITUZIONE CREDITI")
        Me.chkForzaScad.Attributes.Add("NOME", "R.U-FORZA SCADENZA BOLL. RIPARTIBILI")
        Me.chkSceltaDestEcc.Attributes.Add("NOME", "R.U-SCELTA DESTINAZIONE ECCEDENZA")

        'MAX 20/12/2017
        ChFO_LOG.Attributes.Add("NOME", "MODULO FORNITORI-LOG EVENTI")

        Me.chMassIngiunz.Attributes.Add("NOME", "RAPPORTI UTENZA-INSERIMENTO MASSIVO BOLL.INGIUNTE")
        Me.chSingIngiunz.Attributes.Add("NOME", "RAPPORTI UTENZA-INSERIMENTO SING. BOLL.INGIUNTA")
        Me.chkInserimIng.Attributes.Add("NOME", "R.U-INS. PAGAMENTO ING.")
        Me.chkReportIng.Attributes.Add("NOME", "R.U-REPORT PAGAMENTO ING.")

        Me.ChERPVerificaReq.Attributes.Add("NOME", "E.R.P.-VERIFICA MANTENIMENTO REQUISITI")
        Me.ChPosizGrad.Attributes.Add("NOME", "E.R.P.-CAMBIO POSIZIONE GRADUATORIA")
        Me.chkMotiviDec.Attributes.Add("NOME", "RAPPORTI UTENZA-MOTIVI DECISIONI PER STAMPA")
        Me.chkOA.Attributes.Add("NOME", "RAPPORTI UTENZA-GESTIONE OA")


    End Function


    Private Function MemorizzaAttributi() As Boolean
        Dim ELENCOERRORI As String = ""
        Try
            Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
            Dim CTRL As Control = Nothing


            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    If DirectCast(CTRL, TextBox).Text.ToUpper <> DirectCast(CTRL, TextBox).Attributes("CORRENTE").ToUpper.ToString Then
                        If ScriviLogOp(DirectCast(CTRL, TextBox).Attributes("NOME").ToUpper.ToString, DirectCast(CTRL, TextBox).Attributes("CORRENTE").ToUpper.ToString, DirectCast(CTRL, TextBox).Text.ToUpper, 6, Tempo) = False Then
                            ELENCOERRORI = ELENCOERRORI & DirectCast(CTRL, TextBox).Attributes("NOME").ToUpper.ToString & "<br/>"
                        End If
                    End If
                End If
                If TypeOf CTRL Is CheckBox Then
                    If Valore01(DirectCast(CTRL, CheckBox).Checked) <> DirectCast(CTRL, CheckBox).Attributes("CORRENTE").ToUpper.ToString Then
                        If ScriviLogOp(DirectCast(CTRL, CheckBox).Attributes("NOME").ToUpper.ToString, DirectCast(CTRL, CheckBox).Attributes("CORRENTE").ToUpper.ToString, Valore01(DirectCast(CTRL, CheckBox).Checked), 6, Tempo) = False Then
                            ELENCOERRORI = ELENCOERRORI & DirectCast(CTRL, CheckBox).Attributes("NOME").ToUpper.ToString & "<br/>"
                        End If
                    End If
                End If
                If TypeOf CTRL Is DropDownList Then
                    If DirectCast(CTRL, DropDownList).SelectedItem.Text.ToUpper <> DirectCast(CTRL, DropDownList).Attributes("CORRENTE").ToUpper.ToString Then
                        If ScriviLogOp(DirectCast(CTRL, DropDownList).Attributes("NOME").ToUpper.ToString, DirectCast(CTRL, DropDownList).Attributes("CORRENTE").ToUpper.ToString, DirectCast(CTRL, DropDownList).SelectedItem.Text.ToUpper, 6, Tempo) = False Then
                            ELENCOERRORI = ELENCOERRORI & DirectCast(CTRL, DropDownList).Attributes("NOME").ToUpper.ToString & "<br/>"
                        End If
                    End If
                End If
            Next

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = "ERRORE MEMORIZZAZIONE ATTRIBUTI - " & ELENCOERRORI & ex.Message
        End Try
    End Function

    Private Function ScriviLogOp(ByVal CAMPO As String, ByVal VAL_PRECEDENTE As String, ByVal VAL_IMPOSTATO As String, OPERAZIONE As Integer, tempo As String) As Boolean
        Try
            Dim aperto As Boolean = False

            If par.cmd.Connection.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
                aperto = True
            End If

            par.cmd.CommandText = "Insert into OPERATORI_LOG (ID_OPERATORE, DATA_ORA, ID_OPERATORE_M, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) Values (" & Session.Item("ID_OPERATORE") & ", '" & tempo & "', " & lid_Operatore & ", '" & par.PulisciStrSql(CAMPO) & "', '" & par.PulisciStrSql(VAL_PRECEDENTE) & "', '" & par.PulisciStrSql(VAL_IMPOSTATO) & "', " & OPERAZIONE & ")"
            par.cmd.ExecuteNonQuery()

            If aperto = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
            End If
            ScriviLogOp = True
        Catch ex As Exception
            par.OracleConn.Close()
            ScriviLogOp = False
        End Try
    End Function


    Private Sub ApriConnessione()
        par.OracleConn.Open()
        par.SettaCommand(par)
    End Sub

    Private Sub ChiudiConnessione()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Private Sub CreaNuovo()
        cmbFiliale.Items.FindByValue("-1").Selected = True
        ChAPConsultazione.Checked = True
        ChCensManutSL.Checked = True
    End Sub

    Private Sub Visualizza()
        Try
            imgAnnullaRevoca.Enabled = True
            imgAzzera0.Enabled = True
            imgRevoca.Enabled = True
            imgAzzera.Enabled = True
            ApriConnessione()
            par.cmd.CommandText = "select * from OPERATORI where id=" & lid_Operatore
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtUtente.Text = par.IfNull(myReader("operatore"), "")
                nomeutente.Value = txtUtente.Text.ToUpper
                nomeoperatore.Value = txtUtente.Text
                txtCognome.Text = par.IfNull(myReader("cognome"), "")
                txtNome.Text = par.IfNull(myReader("nome"), "")
                txtCF.Text = par.IfNull(myReader("cod_fiscale"), "")
                txtAnagrafico.Text = par.IfNull(myReader("cod_ana"), "")
                txtScadenzaPW.Text = par.FormattaData(par.IfNull(myReader("DATA_PW"), ""))
                cmbFiliale.SelectedValue = par.IfNull(myReader("ID_UFFICIO"), -1)

                If par.IfNull(myReader("MOD_EMRI"), "0") = "0" Then
                    ChABB_EMRI.Checked = False
                Else
                    ChABB_EMRI.Checked = True
                End If

                If par.IfNull(myReader("mod_CONDOMINIO"), "0") = "0" Then
                    chCondominio.Checked = False
                Else
                    chCondominio.Checked = True
                End If

                If par.IfNull(myReader("mod_CONDOMINIO_SL"), "0") = "0" Then
                    ChCondominioSL.Checked = False
                Else
                    ChCondominioSL.Checked = True
                End If

                '******************GESTIONE OPERATORI VSA
                If par.IfNull(myReader("OP_RESP_VSA"), 0) = 0 Then
                    Me.ch_OP_RESP_VSA.Checked = False
                Else
                    ch_OP_RESP_VSA.Checked = True
                End If

                If par.IfNull(myReader("OP_NORM_VSA"), 0) = 0 Then
                    Me.ch_OP_VSA.Checked = False
                Else
                    ch_OP_VSA.Checked = True
                End If

                '############## ASSESTAMENTO#########################
                If par.IfNull(myReader("ASS_FORMALIZZAZIONE"), 0) = 0 Then
                    Me.ChASSNuovo.Checked = False
                Else
                    Me.ChASSNuovo.Checked = True
                End If

                If par.IfNull(myReader("ASS_COMPILAZIONE"), 0) = 0 Then
                    Me.ChASSCompila.Checked = False
                Else
                    Me.ChASSCompila.Checked = True
                End If

                If par.IfNull(myReader("ASS_CONV_ALER"), 0) = 0 Then
                    Me.ChASSConvAler.Checked = False
                Else
                    Me.ChASSConvAler.Checked = True
                End If

                If par.IfNull(myReader("ASS_CONV_COMUNE"), 0) = 0 Then
                    Me.ChASSConvComune.Checked = False
                Else
                    Me.ChASSConvComune.Checked = True
                End If
                '############### FINE ASSESTAMENTO ####################

                '22/07/2014 Reca in partita gestionale
                If par.IfNull(myReader("MOD_RECA_GEST"), 0) = 0 Then
                    chRecaGest.Checked = False
                Else
                    chRecaGest.Checked = True
                End If
                '22/07/2014 FINE Reca in partita gestionale

                '03/03/2015
                If par.IfNull(myReader("MOD_RUAU"), 0) = 0 Then
                    chRUAU.Checked = False
                Else
                    chRUAU.Checked = True
                End If
                '04/03/2015 MAX
                'If PAR.IfNull(myReader("MOD_RUEXP"), 0) = 0 Then
                '    chRUExport.Checked = False
                'Else
                '    chRUExport.Checked = True
                'End If

                ''15/05/2015 Puccia, seguito richiesta Marco Miniussi segnalazione sd
                If par.IfNull(myReader("mod_contratti"), 0) = 0 Then
                    chRUExport.Checked = False
                Else
                    chRUExport.Checked = True
                End If

                '11/03/2015
                If par.IfNull(myReader("MOD_RUSALDI"), 0) = 0 Then
                    chRUSALDI.Checked = False
                Else
                    chRUSALDI.Checked = True
                End If

                If par.IfNull(myReader("MOD_AMM_RPT_P_EXTRA"), 0) = 0 Then
                    Me.ChAmmRptPagExtraMav.Checked = False
                Else
                    ChAmmRptPagExtraMav.Checked = True
                End If

                '13/05/2013 Elaborazione massiva documenti gestionali
                If par.IfNull(myReader("MOD_ELAB_MASS_GEST"), 0) = 0 Then
                    ChElaborazMass.Checked = False
                Else
                    ChElaborazMass.Checked = True
                End If
                '13/05/2013 FINE Elaborazione massiva documenti gestionali


                '13/05/2013 Elaborazione singola documenti gestionali
                If par.IfNull(myReader("MOD_ELAB_SING_GEST"), 0) = 0 Then
                    ChElaborazSing.Checked = False
                Else
                    ChElaborazSing.Checked = True
                End If
                '13/05/2013 FINE Elaborazione singola documenti gestionali



                '17/02/2014 Gestione distanza km
                If par.IfNull(myReader("MOD_DISTANZE_COMUNI"), 0) = 0 Then
                    chDistanzaKM.Checked = False
                Else
                    chDistanzaKM.Checked = True
                End If
                '17/02/2014 FINE Gestione distanza km
                If par.IfNull(myReader("MOD_AU_CREAGRUPPI"), "0") = "0" Then
                    ChAUCreaGruppo.Checked = False
                Else
                    ChAUCreaGruppo.Checked = True
                End If
                If par.IfNull(myReader("MOD_AU_CONV_SINDACATI"), "0") = "0" Then
                    ChAUConvSind.Checked = False
                Else
                    ChAUConvSind.Checked = True
                End If


                If par.IfNull(myReader("MOD_AU_SIMULA_APPLICA"), "0") = "0" Then
                    ChAUSimulaApplica.Checked = False
                Else
                    ChAUSimulaApplica.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_CONV_VIS_TUTTO"), "0") = "0" Then
                    ChAUConvTutti.Checked = False
                Else
                    ChAUConvTutti.Checked = True
                End If

                If par.IfNull(myReader("MOD_MAND_PAGAMENTO"), "0") = "0" Then
                    CHMAND_PAG.Checked = False
                Else
                    CHMAND_PAG.Checked = True
                End If

                If par.IfNull(myReader("MOD_CONT_RATEIZZA"), 0) = 0 Then
                    Me.ChCONTRATTIRateizzazione.Checked = False
                Else
                    ChCONTRATTIRateizzazione.Checked = True
                End If

                If par.IfNull(myReader("MOD_ANNULLA_RATEIZZA"), 0) = 0 Then
                    Me.ChkAnnullaRAT.Checked = False
                Else
                    ChkAnnullaRAT.Checked = True
                End If

                'ChMOR_ANN
                If par.IfNull(myReader("MOD_MOROSITA_ANN"), "0") = "0" Then
                    ChMOR_ANN.Checked = False
                Else
                    ChMOR_ANN.Checked = True
                End If


                If par.IfNull(myReader("MOD_AU_DIFF_MP"), "0") = "0" Then
                    ChAUDiffida.Checked = False
                Else
                    ChAUDiffida.Checked = True
                End If

                'RSS
                If par.IfNull(myReader("MOD_BP_RSS"), "0") = "0" Then
                    chRSS.Checked = False
                Else
                    chRSS.Checked = True
                End If
                If par.IfNull(myReader("MOD_BP_RSS_SL"), "0") = "0" Then
                    chRSSSl.Checked = False
                Else
                    chRSSSl.Checked = True
                End If

                If par.IfNull(myReader("FL_AUTORIZZAZIONE_ODL"), "0") = "0" Then
                    chAutorizzazione.Checked = False
                Else
                    chAutorizzazione.Checked = True
                End If

                If par.IfNull(myReader("FL_ESTRAZIONE_STR"), "0") = "0" Then
                    ChEstraiSTR.Checked = False
                Else
                    ChEstraiSTR.Checked = True
                End If

                If par.IfNull(myReader("FL_CONSUNTIVAZIONE_STR"), "0") = "0" Then
                    ChImportaSTR.Checked = False
                Else
                    ChImportaSTR.Checked = True
                End If

                If par.IfNull(myReader("FL_CP_MOD_PAGAMENTO"), "0") = "0" Then
                    chModPagamento.Checked = False
                Else
                    chModPagamento.Checked = True
                End If

                If par.IfNull(myReader("FL_CP_VARIAZIONE_IMPORTI"), "0") = "0" Then
                    ChVariazioneImporti.Checked = False
                Else
                    ChVariazioneImporti.Checked = True
                End If
                If par.IfNull(myReader("FL_CP_VARIAZ_COMP"), "0") = "0" Then
                    chVariazioneComp.Checked = False
                Else
                    chVariazioneComp.Checked = True
                End If
                If par.IfNull(myReader("FL_CP_DASHBOARD"), "0") = "0" Then
                    chDashboard.Checked = False
                Else
                    chDashboard.Checked = True
                End If
                If par.IfNull(myReader("CP_APPALTO_SINGOLA_VOCE"), "0") = "0" Then
                    chAnticipoSingolaVoce.Checked = False
                Else
                    chAnticipoSingolaVoce.Checked = True
                End If
                If par.IfNull(myReader("FL_CP_TECN_AMM"), "0") = "0" Then
                    chTecnicoAmm.Checked = False
                Else
                    chTecnicoAmm.Checked = True
                End If
                If par.IfNull(myReader("FL_FQM"), "0") = "0" Then
                    chFQM.Checked = False
                Else
                    chFQM.Checked = True
                End If
                If par.IfNull(myReader("FL_CP_RITORNA_BOZZA"), "0") = "0" Then
                    chkRitornaInBozza.Checked = False
                Else
                    chkRitornaInBozza.Checked = True
                End If
                If par.IfNull(myReader("FL_GEST_ALLEGATI"), "0") = "0" Then
                    chkGestAllegati.Checked = False
                Else
                    chkGestAllegati.Checked = True
                End If
                If par.IfNull(myReader("FL_UTENZE"), "0") = "0" Then
                    chkUtenze.Checked = False
                Else
                    chkUtenze.Checked = True
                End If

                If par.IfNull(myReader("FL_SUPERDIRETTORE"), "0") = "0" Then
                    chSuperDirettore.Checked = False
                Else
                    chSuperDirettore.Checked = True
                End If

                'GESTIONE AUTONOMA
                If par.IfNull(myReader("mod_AUTOGESTIONI"), "0") = "0" Then
                    ChGE.Checked = False
                Else
                    ChGE.Checked = True
                End If

                If par.IfNull(myReader("mod_AUTOGESTIONI_SL"), "0") = "0" Then
                    ChGEL.Checked = False
                Else
                    ChGEL.Checked = True
                End If

                'SEZIONE MOROSITA 23/03/2011
                If par.IfNull(myReader("MOD_MOROSITA"), "0") = "0" Then
                    ChMOR.Checked = False
                Else
                    ChMOR.Checked = True
                End If

                If par.IfNull(myReader("MOD_MOROSITA_SL"), "0") = "0" Then
                    ChMOR_SL.Checked = False
                Else
                    ChMOR_SL.Checked = True
                End If

                If par.IfNull(myReader("MOD_CONT_RINN_USD"), "0") = "0" Then
                    ChCONTRATTIRinnoviUSD.Checked = False
                Else
                    ChCONTRATTIRinnoviUSD.Checked = True
                End If

                If par.IfNull(myReader("MOD_CONT_CAMBIO_BOX"), "0") = "0" Then
                    ChCONTRATTICambiBox.Checked = False
                Else
                    ChCONTRATTICambiBox.Checked = True
                End If

                'CICLO PASSIVO
                If par.IfNull(myReader("mod_CICLO_P"), "0") = "0" Then
                    chCicloP.Checked = False
                Else
                    chCicloP.Checked = True
                End If

                If par.IfNull(myReader("BP_NUOVO_PIANO"), "0") = "0" Then
                    ChBPNuovo.Checked = False
                Else
                    ChBPNuovo.Checked = True
                End If

                If par.IfNull(myReader("BP_GENERALE"), "0") = "0" Then
                    ChBPGenerale.Checked = False
                Else
                    ChBPGenerale.Checked = True
                End If

                If par.IfNull(myReader("BP_FORMALIZZAZIONE"), "0") = "0" Then
                    ChBPFormalizzazione.Checked = False
                Else
                    ChBPFormalizzazione.Checked = True
                End If

                If par.IfNull(myReader("BP_COMPILAZIONE"), "0") = "0" Then
                    ChBPCompilazione.Checked = False
                Else
                    ChBPCompilazione.Checked = True
                End If

                If par.IfNull(myReader("BP_CONV_ALER"), "0") = "0" Then
                    ChBPConvalidaAler.Checked = False
                Else
                    ChBPConvalidaAler.Checked = True
                End If

                If par.IfNull(myReader("BP_CONV_COMUNE"), "0") = "0" Then
                    ChBPConvalidaComune.Checked = False
                Else
                    ChBPConvalidaComune.Checked = True
                End If

                If par.IfNull(myReader("BP_CAPITOLI"), "0") = "0" Then
                    ChBPCapitoli.Checked = False
                Else
                    ChBPCapitoli.Checked = True
                End If

                If par.IfNull(myReader("BP_FORMALIZZAZIONE_L"), "0") = "0" Then
                    ChBPFormalizzazione0.Checked = False
                Else
                    ChBPFormalizzazione0.Checked = True
                End If

                If par.IfNull(myReader("BP_COMPILAZIONE_L"), "0") = "0" Then
                    ChBPCompilazione0.Checked = False
                Else
                    ChBPCompilazione0.Checked = True
                End If

                If par.IfNull(myReader("BP_CONV_ALER_L"), "0") = "0" Then
                    ChBPConvalidaAler0.Checked = False
                Else
                    ChBPConvalidaAler0.Checked = True
                End If

                If par.IfNull(myReader("BP_CONV_COMUNE_L"), "0") = "0" Then
                    ChBPConvalidaComune0.Checked = False
                Else
                    ChBPConvalidaComune0.Checked = True
                End If

                If par.IfNull(myReader("BP_CAPITOLI_L"), "0") = "0" Then
                    ChBPCapitoli0.Checked = False
                Else
                    ChBPCapitoli0.Checked = True
                End If

                If par.IfNull(myReader("BP_VOCI_SERVIZI"), "0") = "0" Then
                    ChBPVociServizio.Checked = False
                Else
                    ChBPVociServizio.Checked = True
                End If

                If par.IfNull(myReader("BP_VOCI_SERVIZI_L"), "0") = "0" Then
                    ChBPVociServizio0.Checked = False
                Else
                    ChBPVociServizio0.Checked = True
                End If

                If par.IfNull(myReader("BP_MS"), "0") = "0" Then
                    ChMS.Checked = False
                Else
                    ChMS.Checked = True
                End If
                If par.IfNull(myReader("FL_CAMBIO_IVA_ODL"), "0") = "0" Then
                    chkCambioIvaOdl.Checked = False
                Else
                    chkCambioIvaOdl.Checked = True
                End If
                'Rielabora SAL - CDP
                If par.IfNull(myReader("BP_MS_RIELABORA_CDP"), "0") = "0" Then
                    chkRielaboraCDP.Checked = False
                Else
                    chkRielaboraCDP.Checked = True
                End If
                If par.IfNull(myReader("BP_MS_RIELABORA_SAL"), "0") = "0" Then
                    chkRielaboraSAL.Checked = False
                Else
                    chkRielaboraSAL.Checked = True
                End If
                If par.IfNull(myReader("BP_OP_RIELABORA_CDP"), "0") = "0" Then
                    chkPagRielabCDP.Checked = False
                Else
                    chkPagRielabCDP.Checked = True
                End If
                If par.IfNull(myReader("BP_PC_RIELABORA_CDP"), "0") = "0" Then
                    chkPagCanoneRielCDP.Checked = False
                Else
                    chkPagCanoneRielCDP.Checked = True
                End If
                If par.IfNull(myReader("BP_PC_RIELABORA_SAL"), "0") = "0" Then
                    chkPagCanoneRielSAL.Checked = False
                Else
                    chkPagCanoneRielSAL.Checked = True
                End If
                If par.IfNull(myReader("BP_RRS_RIELABORA_CDP"), "0") = "0" Then
                    chkRRSRielabCDP.Checked = False
                Else
                    chkRRSRielabCDP.Checked = True
                End If
                If par.IfNull(myReader("BP_RRS_RIELABORA_SAL"), "0") = "0" Then
                    chkRRSRielabSAL.Checked = False
                Else
                    chkRRSRielabSAL.Checked = True
                End If

                If par.IfNull(myReader("BP_MS_L"), "0") = "0" Then
                    ChMSL.Checked = False
                Else
                    ChMSL.Checked = True
                End If

                If par.IfNull(myReader("BP_OP"), "0") = "0" Then
                    ChOP.Checked = False
                Else
                    ChOP.Checked = True
                End If

                If par.IfNull(myReader("BP_OP_L"), "0") = "0" Then
                    ChOPL.Checked = False
                Else
                    ChOPL.Checked = True
                End If

                If par.IfNull(myReader("BP_PC"), "0") = "0" Then
                    ChPC.Checked = False
                Else
                    ChPC.Checked = True
                End If

                If par.IfNull(myReader("BP_PC_L"), "0") = "0" Then
                    ChPCL.Checked = False
                Else
                    ChPCL.Checked = True
                End If

                If par.IfNull(myReader("BP_LO"), "0") = "0" Then
                    ChLO.Checked = False
                Else
                    ChLO.Checked = True
                End If

                If par.IfNull(myReader("BP_LO_L"), "0") = "0" Then
                    ChLOL.Checked = False
                Else
                    ChLOL.Checked = True
                End If

                If par.IfNull(myReader("BP_CC"), "0") = "0" Then
                    ChCC.Checked = False
                Else
                    ChCC.Checked = True
                End If



                If par.IfNull(myReader("MOD_BUILDING_MANAGER"), "0") = "0" Then
                    ChBuildingM.Checked = False
                Else
                    ChBuildingM.Checked = True
                End If

                If par.IfNull(myReader("BP_CC_L"), "0") = "0" Then
                    ChCCL.Checked = False
                Else
                    ChCCL.Checked = True
                End If

                If par.IfNull(myReader("BP_CC_V"), "0") = "0" Then
                    ChCCV.Checked = False
                Else
                    ChCCV.Checked = True
                End If

                'AGENDA E SEGNALAZIONI
                If par.IfNull(myReader("mod_GESTIONE_CONTATTI"), "0") = "0" Then
                    chGC.Checked = False
                Else
                    chGC.Checked = True
                End If

                If par.IfNull(myReader("mod_GESTIONE_CONTATTI_sl"), "0") = "0" Then
                    ChGC_SL.Checked = False
                Else
                    ChGC_SL.Checked = True
                End If

                If par.IfNull(myReader("FL_GC_TABELLE_SUPP"), "0") = "0" Then
                    ChGC_TabelleSupporto.Checked = False
                Else
                    ChGC_TabelleSupporto.Checked = True
                End If

                If par.IfNull(myReader("FL_GC_REPORT"), "0") = "0" Then
                    ChGC_Report.Checked = False
                Else
                    ChGC_Report.Checked = True
                End If

                If par.IfNull(myReader("FL_GC_SEGNALAZIONI"), "0") = "0" Then
                    ChGC_Segnalazioni.Checked = False
                Else
                    ChGC_Segnalazioni.Checked = True
                End If

                If par.IfNull(myReader("FL_GC_APPUNTAMENTI"), "0") = "0" Then
                    ChGC_Appuntamenti.Checked = False
                Else
                    ChGC_Appuntamenti.Checked = True
                End If
                If par.IfNull(myReader("FL_GC_CALENDARIO"), "0") = "0" Then
                    ChGC_Calendario.Checked = False
                Else
                    ChGC_Calendario.Checked = True
                End If
                If par.IfNull(myReader("FL_GC_SUPERVISORE"), "0") = "0" Then
                    ChGC_Supervisore.Checked = False
                Else
                    ChGC_Supervisore.Checked = True
                End If


                'If par.IfNull(myReader("anagrafe"), "0") = "0" Then
                '    ChAnagrafe.Checked = False
                'Else
                '    ChAnagrafe.Checked = True
                'End If

                'Customer Satisfaction
                If par.IfNull(myReader("MOD_SATISFACTION"), 0) = 0 Then
                    Me.ChSatisfaction.Checked = False
                Else
                    ChSatisfaction.Checked = True
                End If
                If par.IfNull(myReader("MOD_SATISFACTION_SL"), 0) = 0 Then
                    Me.ChSatisfactionL.Checked = False
                Else
                    ChSatisfactionL.Checked = True
                End If

                'chPROVV
                If par.IfNull(myReader("MOD_AU_CONS"), "0") = "0" Then
                    ChAUconsulta.Checked = False
                Else
                    ChAUconsulta.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU"), "0") = "0" Then
                    Chau.Checked = False
                Else
                    Chau.Checked = True
                End If

                If par.IfNull(myReader("MOD_ABB"), "0") = "0" Then
                    Chabbinamento.Checked = False
                Else
                    Chabbinamento.Checked = True
                End If

                If par.IfNull(myReader("MOD_ABB_DEC"), "0") = "0" Then
                    ChABB_DEC.Checked = False
                Else
                    ChABB_DEC.Checked = True
                End If

                If par.IfNull(myReader("mod_demanio"), "0") = "0" Then
                    chDemanio.Checked = False
                Else
                    chDemanio.Checked = True
                End If

                If par.IfNull(myReader("mod_contratti"), "0") = "0" Then
                    ChContratti.Checked = False
                Else
                    ChContratti.Checked = True
                End If

                If par.IfNull(myReader("mod_contratti_testo"), "0") = "0" Then
                    ChCONTRATTItesto.Checked = False
                Else
                    ChCONTRATTItesto.Checked = True
                End If

                If par.IfNull(myReader("MOD_CONTRATTI_MOR"), "0") = "0" Then
                    ChCONTRATTIMor.Checked = False
                Else
                    ChCONTRATTIMor.Checked = True
                End If

                If par.IfNull(myReader("mod_PED2"), "0") = "0" Then
                    chPED2Completa.Checked = False
                Else
                    chPED2Completa.Checked = True
                End If

                If par.IfNull(myReader("mod_contabile"), "0") = "0" Then
                    chContabilita.Checked = False
                Else
                    chContabilita.Checked = True
                End If

                If par.IfNull(myReader("mod_PED2_ESTERNA"), "0") = "0" Then
                    CHPED2ESTERNA.Checked = False
                Else
                    CHPED2ESTERNA.Checked = True
                End If

                If par.IfNull(myReader("mod_PED2_SOLO_LETTURA"), "0") = "0" Then
                    ChAPConsultazione.Checked = False
                Else
                    ChAPConsultazione.Checked = True
                End If

                Select Case par.IfNull(myReader("revoca"), "0")
                    Case "1"
                        lblRevoca.Visible = True
                        lblRevocaMotivo.Visible = True
                        lblRevocaMotivo.Text = par.IfNull(myReader("motivo_revoca"), "")
                        imgAnnullaRevoca.Visible = False
                        imgRevoca.Visible = False
                    Case "2"
                        lblRevoca.Visible = True
                        lblRevocaMotivo.Visible = True
                        lblRevocaMotivo.Text = par.IfNull(myReader("motivo_revoca"), "")
                End Select

                If par.IfNull(myReader("FL_ABB_ERP"), "0") = "0" Then
                    cERP.Checked = False
                Else
                    cERP.Checked = True
                End If

                If par.IfNull(myReader("FL_ABB_392"), "0") = "0" Then
                    c392.Checked = False
                Else
                    c392.Checked = True
                End If

                If par.IfNull(myReader("FL_ABB_431"), "0") = "0" Then
                    c431.Checked = False
                Else
                    c431.Checked = True
                End If

                If par.IfNull(myReader("FL_ABB_UD"), "0") = "0" Then
                    cUD.Checked = False
                Else
                    cUD.Checked = True
                End If

                If par.IfNull(myReader("FL_ABB_OA"), "0") = "0" Then
                    cOA.Checked = False
                Else
                    cOA.Checked = True
                End If

                If par.IfNull(myReader("FL_ABB_FO"), "0") = "0" Then
                    ChFO.Checked = False
                Else
                    ChFO.Checked = True
                End If

                If par.IfNull(myReader("FL_ABB_CS"), "0") = "0" Then
                    ChCS.Checked = False
                Else
                    ChCS.Checked = True
                End If

                If par.IfNull(myReader("FL_ABB_CONV"), "0") = "0" Then
                    ChConvenzionato.Checked = False
                Else
                    ChConvenzionato.Checked = True
                End If

                'chPROVV
                If par.IfNull(myReader("mod_contratti_l"), "0") = "0" Then
                    ChCONTRATTILETTURA.Checked = False
                Else
                    ChCONTRATTILETTURA.Checked = True
                End If

                If par.IfNull(myReader("mod_contratti_d"), "0") = "0" Then
                    ChCONTRATTId.Checked = False
                Else
                    ChCONTRATTId.Checked = True
                End If

                If par.IfNull(myReader("MOD_CONTRATTI_INS"), "0") = "0" Then
                    ChCONTRATTIins.Checked = False
                Else
                    ChCONTRATTIins.Checked = True
                End If

                If par.IfNull(myReader("MOD_CONTRATTI_ISTAT"), "0") = "0" Then
                    ChCONTRATTIist.Checked = False
                Else
                    ChCONTRATTIist.Checked = True
                End If

                If par.IfNull(myReader("MOD_CONTRATTI_INT"), "0") = "0" Then
                    ChCONTRATTIint.Checked = False
                Else
                    ChCONTRATTIint.Checked = True
                End If

                If par.IfNull(myReader("MOD_CONTRATTI_REG"), "0") = "0" Then
                    ChCONTRATTIreg.Checked = False
                Else
                    ChCONTRATTIreg.Checked = True
                End If

                If par.IfNull(myReader("MOD_CONTRATTI_IMP"), "0") = "0" Then
                    ChCONTRATTIimp.Checked = False
                Else
                    ChCONTRATTIimp.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_PROP_DEC"), "0") = "0" Then
                    ChAUpropDec.Checked = False
                Else
                    ChAUpropDec.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_DOC_NEC"), "0") = "0" Then
                    ChAUDocNecessaria.Checked = False
                Else
                    ChAUDocNecessaria.Checked = True
                End If

                If par.IfNull(myReader("mod_cont_ragioneria"), "0") = "0" Then
                    ChContRagioneria.Checked = False
                Else
                    ChContRagioneria.Checked = True
                End If

                If par.IfNull(myReader("mod_cont_patrimoniali"), "0") = "0" Then
                    ChContPatrimoniali.Checked = False
                Else
                    ChContPatrimoniali.Checked = True
                End If

                If par.IfNull(myReader("mod_cont_flussi"), "0") = "0" Then
                    ChContFlussi.Checked = False
                Else
                    ChContFlussi.Checked = True
                End If

                If par.IfNull(myReader("mod_cont_RIMB"), "0") = "0" Then
                    ChContRimborsi.Checked = False
                Else
                    ChContRimborsi.Checked = True
                End If

                If par.IfNull(myReader("MOD_CONT_COMPENSI"), "0") = "0" Then
                    ChContCompensi.Checked = False
                Else
                    ChContCompensi.Checked = True
                End If

                'ORDINE
                'ELETTRICO - IDRICO - TERM.CENTRALIZZATO - TERM.AUTONOMO - TELERISCALD. - 
                'SOLLEVAMENTO - METEORICHE - ANTINCENDIO - TUT. IMMOBILE - CANNA FUM. - 
                'GAS -CITOFONICO - TV

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 1, 1) = "1" Then
                    ChDemEl.Checked = True
                Else
                    ChDemEl.Checked = False
                End If

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 2, 1) = "1" Then
                    ChDemID.Checked = True
                Else
                    ChDemID.Checked = False
                End If

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 3, 1) = "1" Then
                    ChDemTC.Checked = True
                Else
                    ChDemTC.Checked = False
                End If

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 4, 1) = "1" Then
                    ChDemTA.Checked = True
                Else
                    ChDemTA.Checked = False
                End If

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 5, 1) = "1" Then
                    ChDemTR.Checked = True
                Else
                    ChDemTR.Checked = False
                End If

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 6, 1) = "1" Then
                    ChDemSO.Checked = True
                Else
                    ChDemSO.Checked = False
                End If

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 7, 1) = "1" Then
                    ChDemAM.Checked = True
                Else
                    ChDemAM.Checked = False
                End If

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 8, 1) = "1" Then
                    ChDemAI.Checked = True
                Else
                    ChDemAI.Checked = False
                End If

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 9, 1) = "1" Then
                    ChDemTI.Checked = True
                Else
                    ChDemTI.Checked = False
                End If

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 10, 1) = "1" Then
                    ChDemCF.Checked = True
                Else
                    ChDemCF.Checked = False
                End If

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 11, 1) = "1" Then
                    ChDemGAS.Checked = True
                Else
                    ChDemGAS.Checked = False
                End If

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 12, 1) = "1" Then
                    ChDemCITOFONICO.Checked = True
                Else
                    ChDemCITOFONICO.Checked = False
                End If

                If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 13, 1) = "1" Then
                    ChDemTV.Checked = True
                Else
                    ChDemTV.Checked = False
                End If

                If par.IfNull(myReader("MOD_DEM_SL"), "0") = "1" Then
                    ChDemanioSL.Checked = True
                Else
                    ChDemanioSL.Checked = False
                End If

                If par.IfNull(myReader("MOD_CENS_MANUT"), 0) = 0 Then
                    Me.MOD_CENS_MANUT.Checked = False
                Else
                    MOD_CENS_MANUT.Checked = True
                End If

                If par.IfNull(myReader("FL_PRG_INTERVENTI_MASSIVO"), 0) = 0 Then
                    Me.chkCaricMassProgrInt.Checked = False
                Else
                    chkCaricMassProgrInt.Checked = True
                End If

                If par.IfNull(myReader("CENS_MANUT_SL"), 0) = 0 Then
                    Me.ChCensManutSL.Checked = False
                Else
                    ChCensManutSL.Checked = True
                End If

                If par.IfNull(myReader("MOD_SATISFACTION_SV"), 0) = 0 Then
                    Me.ChSatisfSuperVisore.Checked = False
                Else
                    ChSatisfSuperVisore.Checked = True
                End If

                If par.IfNull(myReader("MOD_CONT_P_EXTRA"), 0) = 0 Then
                    ChCONTRATTIPEXTRA.Checked = False
                Else
                    ChCONTRATTIPEXTRA.Checked = True
                End If



                If par.IfNull(myReader("MOD_PAG_RUOLI"), 0) = 0 Then
                    chkInserimRuoli.Checked = False
                Else
                    chkInserimRuoli.Checked = True
                End If

                If par.IfNull(myReader("MOD_REPORT_RUOLI"), 0) = 0 Then
                    chkReportRuoli.Checked = False
                Else
                    chkReportRuoli.Checked = True
                End If

                If par.IfNull(myReader("FL_FORZARIMBORSO"), 0) = 0 Then
                    chkForzaRestituz.Checked = False
                Else
                    chkForzaRestituz.Checked = True
                End If

                If par.IfNull(myReader("FL_FORZA_SCADENZA"), 0) = 0 Then
                    chkForzaScad.Checked = False
                Else
                    chkForzaScad.Checked = True
                End If

                If par.IfNull(myReader("MOD_SBLOCCO_BOLL"), 0) = 0 Then
                    chkSbloccoBoll.Checked = False
                Else
                    chkSbloccoBoll.Checked = True
                End If

                If par.IfNull(myReader("FL_ANAGRAFICA"), 0) = 0 Then
                    chkModificaANA.Checked = False
                Else
                    chkModificaANA.Checked = True
                End If


                If par.IfNull(myReader("BP_VARIAZIONI"), 0) = 0 Then
                    Me.ChVariazioni.Checked = False
                Else
                    ChVariazioni.Checked = True
                End If

                If par.IfNull(myReader("BP_VARIAZIONI_SL"), 0) = 0 Then
                    Me.ChVariazioniSL.Checked = False
                Else
                    ChVariazioniSL.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_CONV_REI"), 0) = 0 Then
                    ChAUConvReimposta.Checked = False
                Else
                    ChAUConvReimposta.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_CONV_RIP"), 0) = 0 Then
                    ChAUConvRip.Checked = False
                Else
                    ChAUConvRip.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_CONV_ANN"), 0) = 0 Then
                    ChAUConvAnnulla.Checked = False
                Else
                    ChAUConvAnnulla.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_CONV_SPOSTA"), 0) = 0 Then
                    ChAUConvSposta.Checked = False
                Else
                    ChAUConvSposta.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_CONV_N"), 0) = 0 Then
                    ChAUConvIns.Checked = False
                Else
                    ChAUConvIns.Checked = True
                End If

                'If par.IfNull(myReader("MOD_BP_PREVENTIVI"), 0) = 0 Then
                '    ChBpPreventivi.Checked = False
                'Else
                '    ChBpPreventivi.Checked = True
                'End If

                '************* 12/07/2012 CONDIZIONE PER SPOSTAMENTO/ANNULLAMENTO **************
                If par.IfNull(myReader("MOD_SPOSTAM_ANNULL"), 0) = 0 Then
                    ChSpostaAnnullo.Checked = False
                Else
                    ChSpostaAnnullo.Checked = True
                End If
                '************* 12/07/2012 FINE CONDIZIONE PER SPOSTAMENTO/ANNULLAMENTO **********

                '************* 15/11/2012 CONDIZIONE PER SPESE REVERSIBILI **************
                If par.IfNull(myReader("FL_SPESE_REVERSIBILI"), 0) = 0 Then
                    ChSpReversibili.Checked = False
                Else
                    ChSpReversibili.Checked = True
                End If
                If par.IfNull(myReader("FL_SPESE_REV_SL"), 0) = 0 Then
                    ChSpReversibiliSl.Checked = False
                Else
                    ChSpReversibiliSl.Checked = True
                End If

                '01/07/2013 applicazione spese bolletta
                If par.IfNull(myReader("FL_SPESE_REV_APP_BOLLETTE"), 0) = 0 Then
                    ChBPAppPreventivi.Checked = False
                Else
                    ChBPAppPreventivi.Checked = True
                End If



                If par.IfNull(myReader("MOD_BP_RESIDUI"), 0) = 0 Then
                    ChBPResidui.Checked = False
                Else
                    ChBPResidui.Checked = True
                End If

                If par.IfNull(myReader("FL_CAMBIO_STRUTTURA"), 0) = 0 Then
                    chkcambiostruttura.Checked = False
                Else
                    chkcambiostruttura.Checked = True
                End If

                'MAX 10/04/2013
                If par.IfNull(myReader("MOD_AU_GESTIONE"), 0) = 0 Then
                    ChAUGestione.Checked = False
                Else
                    ChAUGestione.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_GESTIONE_MOD"), 0) = 0 Then
                    ChAUGestioneMod.Checked = False
                Else
                    ChAUGestioneMod.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_GESTIONE_STR"), 0) = 0 Then
                    ChAUGestioneStr.Checked = False
                Else
                    ChAUGestioneStr.Checked = True
                End If

                '*MAX 08/05/2013
                If par.IfNull(myReader("MOD_AU_GESTIONE_LISTE"), 0) = 0 Then
                    ChAUGestioneLista.Checked = False
                Else
                    ChAUGestioneLista.Checked = True
                End If

                'MAX 15/05/2013
                If par.IfNull(myReader("MOD_AU_GESTIONE_ESCLUSIONI"), 0) = 0 Then
                    ChAUGestioneEsclusione.Checked = False
                Else
                    ChAUGestioneEsclusione.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_GESTIONE_CONVOCABILI"), 0) = 0 Then
                    ChAUGestioneConvocabili.Checked = False
                Else
                    ChAUGestioneConvocabili.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_GESTIONE_GRUPPI"), 0) = 0 Then
                    ChAUGestioneGrConv.Checked = False
                Else
                    ChAUGestioneGrConv.Checked = True
                End If

                'MAX 18/06/2013

                If par.IfNull(myReader("MOD_AU_CREA_CONV"), 0) = 0 Then
                    ChAUDefConv.Checked = False
                Else
                    ChAUDefConv.Checked = True
                End If

                If par.IfNull(myReader("MOD_AU_ELIMINA_BANDO"), 0) = 0 Then
                    ChAUGestione0.Checked = False
                Else
                    ChAUGestione0.Checked = True
                End If

                'MAX 04/09/2013
                If par.IfNull(myReader("MOD_AU_ANNULLA_DIFF"), 0) = 0 Then
                    ChAUAnnullaStampa.Checked = False
                Else
                    ChAUAnnullaStampa.Checked = True
                End If

                'MAX 13/09/2013
                If par.IfNull(myReader("MOD_AU_ELIMINA_F_CONV"), 0) = 0 Then
                    ChAUEliminaConv.Checked = False
                Else
                    ChAUEliminaConv.Checked = True
                End If
                '************* 05/05/2015 condizione per gestione parametri ciclo passivo *******
                If par.IfNull(myReader("FL_PARAM_CICLO_PASSIVO"), 0) = 0 Then
                    chkParamCP.Checked = False
                Else
                    chkParamCP.Checked = True
                End If

                'MTERESA 21/10/2013
                If par.IfNull(myReader("MOD_TRASFERIM_RUA"), 0) = 0 Then
                    ChTrasfPag.Checked = False
                Else
                    ChTrasfPag.Checked = True
                End If


                'MAX 08/05/2015
                If par.IfNull(myReader("MOD_ARCHIVIO"), "0") = "0" Then
                    ChArchivio.Checked = False
                Else
                    ChArchivio.Checked = True
                End If

                If par.IfNull(myReader("MOD_ARCHIVIO_IM"), "0") = "0" Then
                    ChArchIM.Checked = False
                Else
                    ChArchIM.Checked = True
                End If

                If par.IfNull(myReader("MOD_ARCHIVIO_C"), "0") = "0" Then
                    ChArchC.Checked = False
                Else
                    ChArchC.Checked = True
                End If

                'MAX 26/05/2015
                If par.IfNull(myReader("MOD_CREAZ_BOLL"), "0") = "0" Then
                    ChNewBoll.Checked = False
                Else
                    ChNewBoll.Checked = True
                End If

                'MAX 12/05/2015
                If par.IfNull(myReader("MOD_RILIEVO"), "0") = "0" Then
                    chModRilievo.Checked = False
                Else
                    chModRilievo.Checked = True
                End If

                If par.IfNull(myReader("FL_RILIEVO_GEST"), "0") = "0" Then
                    chRilievoGEST.Checked = False
                Else
                    chRilievoGEST.Checked = True
                End If

                If par.IfNull(myReader("FL_RILIEVO_CAR"), "0") = "0" Then
                    chRilievoCDati.Checked = False
                Else
                    chRilievoCDati.Checked = True
                End If

                If par.IfNull(myReader("FL_RILIEVO_PAR"), "0") = "0" Then
                    chRilievoParam.Checked = False
                Else
                    chRilievoParam.Checked = True
                End If

                'ANTONELLO 01/02/2016
                If par.IfNull(myReader("MOD_SIRAPER"), "0") = "0" Then
                    ChSiraper.Checked = False
                Else
                    ChSiraper.Checked = True
                End If

                'ANTONELLO 17/03/2017
                If par.IfNull(myReader("MOD_ARPA"), "0") = "0" Then
                    cbARPA.Checked = False
                Else
                    cbARPA.Checked = True
                End If

                'MAX 24/06/2015
                If par.IfNull(myReader("MOD_CREAZ_MAVONLINE"), "0") = "0" Then
                    ChRUMAV.Checked = False
                Else
                    ChRUMAV.Checked = True
                End If

                'annulla sal
                If par.IfNull(myReader("FL_ANNULLA_SAl"), "0") = "0" Then
                    chkAnnullaSal.Checked = False
                Else
                    chkAnnullaSal.Checked = True
                End If
                If par.IfNull(myReader("FL_ANNULLA_ODL"), "0") = "0" Then
                    chkAnnullaODL.Checked = False
                Else
                    chkAnnullaODL.Checked = True
                End If
                'MAX 14/10/2015
                If par.IfNull(myReader("MOD_AU_RICERCA"), "0") = "0" Then
                    ChAURicerca.Checked = False
                Else
                    ChAURicerca.Checked = True
                End If
                If par.IfNull(myReader("MOD_AU_REPORT"), "0") = "0" Then
                    ChAUReport.Checked = False
                Else
                    ChAUReport.Checked = True
                End If
                If par.IfNull(myReader("MOD_AU_AGENDA_CERCA"), "0") = "0" Then
                    ChAUCercaInquilino.Checked = False
                Else
                    ChAUCercaInquilino.Checked = True
                End If
                If par.IfNull(myReader("MOD_AU_AGENDA_SOSPESE"), "0") = "0" Then
                    ChAUSospeseSind.Checked = False
                Else
                    ChAUSospeseSind.Checked = True
                End If
                If par.IfNull(myReader("MOD_AU_AGENDA_MOTS"), "0") = "0" Then
                    ChAUGestMotSosp.Checked = False
                Else
                    ChAUGestMotSosp.Checked = True
                End If
                If par.IfNull(myReader("MOD_AU_CF"), "0") = "0" Then
                    ChAUCalcolaCF.Checked = False
                Else
                    ChAUCalcolaCF.Checked = True
                End If

                'MT 18/12/2015
                If par.IfNull(myReader("MOD_SICUREZZA"), "0") = "0" Then
                    ChSicurezza.Checked = False
                Else
                    ChSicurezza.Checked = True
                End If

                If par.IfNull(myReader("MOD_SICUREZZA_sl"), "0") = "0" Then
                    ChTpa_SL.Checked = False
                Else
                    ChTpa_SL.Checked = False
                End If

                If par.IfNull(myReader("FL_SEC_CREA_SEGN"), "0") = "0" Then
                    ChCreaSegn.Checked = False
                Else
                    ChCreaSegn.Checked = True
                End If

                If par.IfNull(myReader("FL_SEC_MODIF_SEGN"), "0") = "0" Then
                    ChModificaSegn.Checked = False
                Else
                    ChModificaSegn.Checked = True
                End If

                If par.IfNull(myReader("FL_SEC_ASS_OPERATORI"), "0") = "0" Then
                    ChAssegnOperatori.Checked = False
                Else
                    ChAssegnOperatori.Checked = True
                End If

                If par.IfNull(myReader("FL_SEC_AGENDA"), "0") = "0" Then
                    ChVisualizzaAgenda.Checked = False
                Else
                    ChVisualizzaAgenda.Checked = True
                End If

                If par.IfNull(myReader("FL_SEC_GEST_COMPLETA"), "0") = "0" Then
                    ChCreaSegnIntProc.Checked = False
                Else
                    ChCreaSegnIntProc.Checked = True
                End If

                'MAX 20/07/2015
                If par.IfNull(myReader("MOD_CONT_NOTE"), "0") = "0" Then
                    chRUNote.Checked = False
                Else
                    chRUNote.Checked = True
                End If

                'MAX 17/02/2016
                If par.IfNull(myReader("MOD_RU_CRDE"), "0") = "0" Then
                    chCrDe.Checked = False
                Else
                    chCrDe.Checked = True
                End If


                If par.IfNull(myReader("FL_SCELTA_DEST_ECCED"), 0) = 0 Then
                    chkSceltaDestEcc.Checked = False
                Else
                    chkSceltaDestEcc.Checked = True
                End If

                'MAX 26/04/2016
                If par.IfNull(myReader("MOD_CONT_DEP"), 0) = 0 Then
                    chRimbDep.Checked = False
                Else
                    chRimbDep.Checked = True
                End If

                'MAX 13/06/2016
                If par.IfNull(myReader("MOD_RU_DATI_AE"), "0") = "0" Then
                    chRUDatiAE.Checked = False
                Else
                    chRUDatiAE.Checked = True
                End If
                'MAX 23/02/2016
                If par.IfNull(myReader("MOD_FORNITORI"), 0) = 0 Then
                    ChModFornitori.Checked = False
                Else
                    ChModFornitori.Checked = True
                End If
                cmbFornitori.SelectedIndex = -1
                cmbFornitori.Items.FindByValue(par.IfNull(myReader("MOD_FO_ID_FO"), "NULL")).Selected = True
                If par.IfNull(myReader("MOD_FORNITORI_RDO"), 0) = 0 Then
                    ChFO_RDO.Checked = False
                Else
                    ChFO_RDO.Checked = True
                End If
                If par.IfNull(myReader("MOD_FORNITORI_ODL"), 0) = 0 Then
                    ChFO_ODL.Checked = False
                Else
                    ChFO_ODL.Checked = True
                End If
                If par.IfNull(myReader("MOD_FORNITORI_RPT"), 0) = 0 Then
                    ChFO_RPT.Checked = False
                Else
                    ChFO_RPT.Checked = True
                End If

                If par.IfNull(myReader("MOD_FORNITORI_PARAM"), 0) = 0 Then
                    ChFO_PAR.Checked = False
                Else
                    ChFO_PAR.Checked = True
                End If

                If par.IfNull(myReader("MOD_FO_LIMITAZIONI"), "0") = "1" Then
                    OperatoreFornitori = 1
                    cmbEnte.Enabled = False
                End If

                If par.IfNull(myReader("MOD_FORNITORI_SLE"), 0) = 0 Then
                    ChFO_SLE.Checked = False
                Else
                    ChFO_SLE.Checked = True
                End If
                '12/02/2018
                If par.IfNull(myReader("MOD_MASS_INGIUNZIONI"), "0") = "0" Then
                    chMassIngiunz.Checked = False
                Else
                    chMassIngiunz.Checked = True
                End If
                If par.IfNull(myReader("MOD_SING_INGIUNZIONI"), "0") = "0" Then
                    chSingIngiunz.Checked = False
                Else
                    chSingIngiunz.Checked = True
                End If

                If par.IfNull(myReader("MOD_PAG_INGIUNZ"), 0) = 0 Then
                    chkInserimIng.Checked = False
                Else
                    chkInserimIng.Checked = True
                End If

                If par.IfNull(myReader("MOD_REPORT_INGIUNZ"), 0) = 0 Then
                    chkReportIng.Checked = False
                Else
                    chkReportIng.Checked = True
                End If

                cmbEnte.SelectedIndex = -1
                cmbEnte.Items.FindByValue(par.IfNull(myReader("id_caf"), "2")).Selected = True

                'MAX 20/12/2017
                If par.IfNull(myReader("MOD_FORNITORI_LOG"), 0) = 0 Then
                    ChFO_LOG.Checked = False
                Else
                    ChFO_LOG.Checked = True
                End If
				If par.IfNull(myReader("MOD_MOTIVI_DECISIONI"), 0) = 0 Then
                    chkMotiviDec.Checked = False
                Else
                    chkMotiviDec.Checked = True
                End If
                If par.IfNull(myReader("RU_GESTIONE_OA"), 0) = 0 Then
                    chkOA.Checked = False
                Else
                    chkOA.Checked = True
                End If

				If par.IfNull(myReader("MOD_ERP_REQUISITI"), 0) = 0 Then
                    ChERPVerificaReq.Checked = False
                    Cherp.Checked = False
                Else
                    ChERPVerificaReq.Checked = True
                    Cherp.Checked = True
                End If
                If par.IfNull(myReader("MOD_ERP_POS_GRAD"), 0) = 0 Then
                    ChPosizGrad.Checked = False
                Else
                    ChPosizGrad.Checked = True
                End If


            End If
            myReader.Close()
            par.cmd.Dispose()
            ChiudiConnessione()
        Catch ex As Exception
            ChiudiConnessione()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Function DisattivaAutorizzazioni()
        Chau.Enabled = False
        ChAUconsulta.Enabled = False
        ChAUpropDec.Enabled = False
        ChAUDiffida.Enabled = False
        ChAUCreaGruppo.Enabled = False
        ChAUGestione.Enabled = False
        ChAUGestione.Enabled = False
        ChAUGestioneMod.Enabled = False
        ChAUGestioneStr.Enabled = False
        ChAUGestioneEsclusione.Enabled = False
        ChAUGestioneConvocabili.Enabled = False
        ChAUGestioneGrConv.Enabled = False
        ChAUGestioneLista.Enabled = False
        ChAUDefConv.Enabled = False
        ChAUEliminaConv.Enabled = False
        ChAUConvTutti.Enabled = False
        ChAUConvIns.Enabled = False
        ChAUConvSposta.Enabled = False
        ChAUConvAnnulla.Enabled = False
        ChAUConvRip.Enabled = False
        ChAUConvReimposta.Enabled = False
        ChAUConvSind.Enabled = False
        ChAUAnnullaStampa.Enabled = False
        Chabbinamento.Enabled = False
        cERP.Enabled = False
        c392.Enabled = False
        c431.Enabled = False
        cUD.Enabled = False
        cOA.Enabled = False
        ChFO.Enabled = False
        ChCS.Enabled = False
        ChConvenzionato.Enabled = False
        ChABB_DEC.Enabled = False
        ch_OP_VSA.Enabled = False
        ch_OP_RESP_VSA.Enabled = False
        ChABB_EMRI.Enabled = False
        chPED2Completa.Enabled = False
        ChAPConsultazione.Enabled = False
        CHPED2ESTERNA.Enabled = False
        MOD_CENS_MANUT.Enabled = False
        chkCaricMassProgrInt.Enabled = False
        ChCensManutSL.Enabled = False
        chContabilita.Enabled = False
        ChContRagioneria.Enabled = False
        ChContPatrimoniali.Enabled = False
        ChContFlussi.Enabled = False
        ChContRimborsi.Enabled = False
        ChContCompensi.Enabled = False
        chCicloP.Enabled = False
        ChBPNuovo.Enabled = False
        ChBPGenerale.Enabled = False
        ChSpReversibili.Enabled = False
        ChBPResidui.Enabled = False
        ChBPAppPreventivi.Enabled = False
        ChSpReversibiliSl.Enabled = False
        ChBPFormalizzazione.Enabled = False
        ChBPCompilazione.Enabled = False
        ChVariazioni.Enabled = False
        ChBPFormalizzazione0.Enabled = False
        ChBPCompilazione0.Enabled = False
        ChVariazioniSL.Enabled = False
        ChBPConvalidaAler.Enabled = False
        ChBPCapitoli.Enabled = False
        ChBPConvalidaComune.Enabled = False
        ChBPConvalidaAler0.Enabled = False
        ChBPCapitoli0.Enabled = False
        ChBPConvalidaComune0.Enabled = False
        ChBPVociServizio.Enabled = False
        ChASSNuovo.Enabled = False
        ChASSConvAler.Enabled = False
        ChBPVociServizio0.Enabled = False
        ChASSCompila.Enabled = False
        ChASSConvComune.Enabled = False
        ChMS.Enabled = False

        chkCambioIvaOdl.Checked = False
        'Rielabora SAL-CDP
        chkRielaboraCDP.Checked = False
        chkRielaboraSAL.Checked = False
        chkPagRielabCDP.Checked = False
        chkPagCanoneRielCDP.Checked = False
        chkPagCanoneRielSAL.Checked = False
        chkRRSRielabCDP.Checked = False
        chkRRSRielabSAL.Checked = False

        ChOP.Enabled = False
        ChPC.Enabled = False
        ChMSL.Enabled = False
        ChOPL.Enabled = False
        ChPCL.Enabled = False
        ChLO.Enabled = False
        ChCC.Enabled = False

        ChBuildingM.Enabled = False
        ChCCV.Enabled = False
        ChLOL.Enabled = False
        ChCCL.Enabled = False
        chRSS.Enabled = False
        CHMAND_PAG.Enabled = False
        chRSSSl.Enabled = False
        chAutorizzazione.Enabled = False
        ChEstraiSTR.Enabled = False
        ChImportaSTR.Enabled = False
        chModPagamento.Enabled = False
        ChVariazioneImporti.Enabled = False
        chVariazioneComp.Enabled = False
        chDashboard.Enabled = False
chAnticipoSingolaVoce.Enabled = False
        chkRitornaInBozza.Enabled = False
        chTecnicoAmm.Enabled = False
        chFQM.Enabled = False
        chkGestAllegati.Enabled = False
        chkAnnullaSal.Enabled = False
        chkAnnullaODL.Enabled = False
        chkUtenze.Enabled = False
        chSuperDirettore.Enabled = False
        chGC.Enabled = False
        ChGC_SL.Enabled = False
        ChGC_TabelleSupporto.Enabled = False
        ChGC_Report.Enabled = False
        ChGC_Segnalazioni.Enabled = False
        ChGC_Appuntamenti.Enabled = False
        ChGC_Calendario.Enabled = False
        ChGC_Supervisore.Enabled = False
        chCondominio.Enabled = False
        ChCondominioSL.Enabled = False
        chDemanio.Enabled = False
        ChDemanioSL.Enabled = False
        ChDemEl.Enabled = False
        ChDemTA.Enabled = False
        ChDemAM.Enabled = False
        ChDemCF.Enabled = False
        ChDemTV.Enabled = False
        ChDemID.Enabled = False
        ChDemTR.Enabled = False
        ChDemAI.Enabled = False
        ChDemGAS.Enabled = False
        ChDemTC.Enabled = False
        ChDemSO.Enabled = False
        ChDemTI.Enabled = False
        ChDemCITOFONICO.Enabled = False
        ChContratti.Enabled = False
        ChCONTRATTILETTURA.Enabled = False
        ChCONTRATTIins.Enabled = False
        ChCONTRATTIreg.Enabled = False
        ChCONTRATTIimp.Enabled = False
        ChCONTRATTIist.Enabled = False
        ChCONTRATTItesto.Enabled = False
        ChCONTRATTId.Enabled = False
        ChCONTRATTIint.Enabled = False
        ChCONTRATTIMor.Enabled = False
        ChCONTRATTIRinnoviUSD.Enabled = False
        ChCONTRATTICambiBox.Enabled = False
        ChCONTRATTIPEXTRA.Enabled = False
        ChCONTRATTIRateizzazione.Enabled = False
        ChSpostaAnnullo.Enabled = False
        ChAmmRptPagExtraMav.Enabled = False
        ChElaborazMass.Enabled = False
        ChElaborazSing.Enabled = False
        ChTrasfPag.Enabled = False
        chDistanzaKM.Enabled = False
        chRecaGest.Enabled = False
        chRUAU.Enabled = False
        chRUSALDI.Enabled = False
        chRUExport.Enabled = False
        ChGE.Enabled = False
        ChGEL.Enabled = False
        ChMOR.Enabled = False
        ChMOR_SL.Enabled = False
        ChMOR_ANN.Enabled = False
        ChSatisfaction.Enabled = False
        ChSatisfactionL.Enabled = False
        ChSatisfSuperVisore.Enabled = False
        ChAUGestione0.Enabled = False
        ChAUSimulaApplica.Enabled = False
        ChBPNuovo.Enabled = False
        chCicloP.Enabled = False
        chRimbDep.Enabled = False
        ChArchivio.Enabled = False
        ChArchC.Enabled = False
        ChArchIM.Enabled = False
        chModRilievo.Enabled = False
        chRilievoCDati.Enabled = False
        chRilievoGEST.Enabled = False
        chRilievoParam.Enabled = False
        'MAX 20/07/2015
        chRUNote.Enabled = False
        'MAX 14/10/2015
        ChAUDocNecessaria.Enabled = False
        ChAURicerca.Enabled = False
        ChAUReport.Enabled = False
        ChAUCercaInquilino.Enabled = False
        ChAUSospeseSind.Enabled = False
        ChAUGestMotSosp.Enabled = False
        ChAUCalcolaCF.Enabled = False
        'ANTONELLO 09/12/2015
        ChSiraper.Enabled = False
        'ANTONELLO 17/03/2017
        cbARPA.Enabled = False
        ChSicurezza.Enabled = False
        ChTpa_SL.Enabled = False
        ChCreaSegn.Enabled = False
        ChModificaSegn.Enabled = False
        ChAssegnOperatori.Enabled = False
        ChVisualizzaAgenda.Enabled = False
        ChCreaSegnIntProc.Enabled = False
        'MAX 17/02/2016
        chCrDe.Enabled = False
        'MAX 23_02_2016
        ChModFornitori.Enabled = False
        cmbFornitori.Enabled = False
        ChFO_RDO.Enabled = False
        ChFO_ODL.Enabled = False
        ChFO_RPT.Enabled = False
        ChFO_PAR.Enabled = False
        ChFO_SLE.Enabled = False
        '13/06/2016 MAX
        chRUDatiAE.Enabled = False

        ChNewBoll.Enabled = False
        chRSS.Enabled = False
        ChLO.Enabled = False
        CHMAND_PAG.Enabled = False
        ChBuildingM.Enabled = False
        'ChFornitori.Enabled = False
        ChCCV.Enabled = False
        ChCC.Enabled = False
        ChBPNuovo.Enabled = False
        ChBPFormalizzazione.Enabled = False
        ChBPConvalidaAler.Enabled = False
        ChBPVociServizio.Enabled = False
        ChMS.Enabled = False

        ChkAnnullaRAT.Enabled = False
        ChRUMAV.Enabled = False
        chkParamCP.Enabled = False

        chkcambiostruttura.Enabled = False
        cmbFiliale.Enabled = False

        'MAX 20/12/2017
        ChFO_LOG.Enabled = False
        chMassIngiunz.Enabled = False
        chSingIngiunz.Enabled = False
		ChERPVerificaReq.Enabled = False
        ChPosizGrad.Enabled = False
        chkMotiviDec.Enabled = False
        chkOA.Enabled = False
    End Function

    Private Function AbilitaAutorizzazioni()
        Chau.Enabled = True
        ChAUconsulta.Enabled = True
        ChAUpropDec.Enabled = True
        ChAUDiffida.Enabled = True
        ChAUCreaGruppo.Enabled = True
        ChAUGestione.Enabled = True
        ChAUGestione.Enabled = True
        ChAUGestioneMod.Enabled = True
        ChAUGestioneStr.Enabled = True
        ChAUGestioneEsclusione.Enabled = True
        ChAUGestioneConvocabili.Enabled = True
        ChAUGestioneGrConv.Enabled = True
        ChAUGestioneLista.Enabled = True
        ChAUDefConv.Enabled = True
        ChAUEliminaConv.Enabled = True
        ChAUConvTutti.Enabled = True
        ChAUConvIns.Enabled = True
        ChAUConvSposta.Enabled = True
        ChAUConvAnnulla.Enabled = True
        ChAUConvRip.Enabled = True
        ChAUConvReimposta.Enabled = True
        ChAUConvSind.Enabled = True
        ChAUAnnullaStampa.Enabled = True
        Chabbinamento.Enabled = True
        cERP.Enabled = True
        c392.Enabled = True
        c431.Enabled = True
        cUD.Enabled = True
        cOA.Enabled = True
        ChFO.Enabled = True
        ChCS.Enabled = True
        ChConvenzionato.Enabled = True
        ChABB_DEC.Enabled = True
        ch_OP_VSA.Enabled = True
        ch_OP_RESP_VSA.Enabled = True
        ChABB_EMRI.Enabled = True
        chPED2Completa.Enabled = True
        ChAPConsultazione.Enabled = True
        CHPED2ESTERNA.Enabled = True
        MOD_CENS_MANUT.Enabled = True
        chkCaricMassProgrInt.Enabled = True
        ChCensManutSL.Enabled = True
        chContabilita.Enabled = True
        ChContRagioneria.Enabled = True
        ChContPatrimoniali.Enabled = True
        ChContFlussi.Enabled = True
        ChContRimborsi.Enabled = True
        ChContCompensi.Enabled = True
        chCicloP.Enabled = True
        ChBPNuovo.Enabled = True
        ChBPGenerale.Enabled = True
        ChSpReversibili.Enabled = True
        ChBPResidui.Enabled = True
        ChBPAppPreventivi.Enabled = True
        ChSpReversibiliSl.Enabled = True
        ChBPFormalizzazione.Enabled = True
        ChBPCompilazione.Enabled = True
        ChVariazioni.Enabled = True
        ChBPFormalizzazione0.Enabled = True
        ChBPCompilazione0.Enabled = True
        ChVariazioniSL.Enabled = True
        ChBPConvalidaAler.Enabled = True
        ChBPCapitoli.Enabled = True
        ChBPConvalidaComune.Enabled = True
        ChBPConvalidaAler0.Enabled = True
        ChBPCapitoli0.Enabled = True
        ChBPConvalidaComune0.Enabled = True
        ChBPVociServizio.Enabled = True
        ChASSNuovo.Enabled = True
        ChASSConvAler.Enabled = True
        ChBPVociServizio0.Enabled = True
        ChASSCompila.Enabled = True
        ChASSConvComune.Enabled = True
        ChMS.Enabled = True
        ChOP.Enabled = True
        ChPC.Enabled = True
        ChMSL.Enabled = True
        ChOPL.Enabled = True
        ChPCL.Enabled = True
        ChLO.Enabled = True
        ChCC.Enabled = True

        ChBuildingM.Enabled = True
        ChCCV.Enabled = True
        ChLOL.Enabled = True
        ChCCL.Enabled = True
        chRSS.Enabled = True
        CHMAND_PAG.Enabled = True
        chRSSSl.Enabled = True
        chAutorizzazione.Enabled = True
        chkAnnullaSal.Enabled = True
        Me.chkAnnullaODL.Enabled = True
        chkUtenze.Enabled = True
        chSuperDirettore.Enabled = True
        chGC.Enabled = True
        ChGC_SL.Enabled = True
        ChGC_TabelleSupporto.Enabled = True
        ChGC_Report.Enabled = True
        ChGC_Segnalazioni.Enabled = True
        ChGC_Appuntamenti.Enabled = True
        ChGC_Calendario.Enabled = True
        ChGC_Supervisore.Enabled = True
        chCondominio.Enabled = True
        ChCondominioSL.Enabled = True
        chDemanio.Enabled = True
        ChDemanioSL.Enabled = True
        ChDemEl.Enabled = True
        ChDemTA.Enabled = True
        ChDemAM.Enabled = True
        ChDemCF.Enabled = True
        ChDemTV.Enabled = True
        ChDemID.Enabled = True
        ChDemTR.Enabled = True
        ChDemAI.Enabled = True
        ChDemGAS.Enabled = True
        ChDemTC.Enabled = True
        ChDemSO.Enabled = True
        ChDemTI.Enabled = True
        ChDemCITOFONICO.Enabled = True
        ChContratti.Enabled = True
        ChCONTRATTILETTURA.Enabled = True
        ChCONTRATTIins.Enabled = True
        ChCONTRATTIreg.Enabled = True
        ChCONTRATTIimp.Enabled = True
        ChCONTRATTIist.Enabled = True
        ChCONTRATTItesto.Enabled = True
        ChCONTRATTId.Enabled = True
        ChCONTRATTIint.Enabled = True
        ChCONTRATTIMor.Enabled = True
        ChCONTRATTIRinnoviUSD.Enabled = True
        ChCONTRATTICambiBox.Enabled = True
        ChCONTRATTIPEXTRA.Enabled = True
        ChCONTRATTIRateizzazione.Enabled = True
        ChSpostaAnnullo.Enabled = True
        ChAmmRptPagExtraMav.Enabled = True
        ChElaborazMass.Enabled = True
        ChElaborazSing.Enabled = True
        ChTrasfPag.Enabled = True
        chDistanzaKM.Enabled = True
        chRecaGest.Enabled = True
        chRUAU.Enabled = True
        chRUSALDI.Enabled = True
        chRUExport.Enabled = True
        ChGE.Enabled = True
        ChGEL.Enabled = True
        ChMOR.Enabled = True
        ChMOR_SL.Enabled = True
        ChMOR_ANN.Enabled = True
        ChSatisfaction.Enabled = True
        ChSatisfactionL.Enabled = True
        ChSatisfSuperVisore.Enabled = True
        ChAUGestione0.Enabled = True
        ChAUSimulaApplica.Enabled = True
        ChBPNuovo.Enabled = True
        chCicloP.Enabled = True
        chRimbDep.Enabled = True
        ChArchivio.Enabled = True
        ChArchC.Enabled = True
        ChArchIM.Enabled = True
        chModRilievo.Enabled = True
        chRilievoCDati.Enabled = True
        chRilievoGEST.Enabled = True
        chRilievoParam.Enabled = True
        'MAX 20/07/2015
        chRUNote.Enabled = True
        'MAX 14/10/2015
        ChAUDocNecessaria.Enabled = True
        ChAURicerca.Enabled = True
        ChAUReport.Enabled = True
        ChAUCercaInquilino.Enabled = True
        ChAUSospeseSind.Enabled = True
        ChAUGestMotSosp.Enabled = True
        ChAUCalcolaCF.Enabled = True
        'ANTONELLO 09/12/2015
        ChSiraper.Enabled = True
        'ANTONELLO 17/03/2017
        cbARPA.Enabled = True
        ChSicurezza.Enabled = True
        ChTpa_SL.Enabled = True
        ChCreaSegn.Enabled = True
        ChModificaSegn.Enabled = True
        ChAssegnOperatori.Enabled = True
        ChVisualizzaAgenda.Enabled = True
        ChCreaSegnIntProc.Enabled = True
        'MAX 17/02/2016
        chCrDe.Enabled = True
        'MAX 23_02_2016
        ChModFornitori.Enabled = True
        cmbFornitori.Enabled = True
        ChFO_RDO.Enabled = True
        ChFO_ODL.Enabled = True
        ChFO_RPT.Enabled = True
        ChFO_PAR.Enabled = True
        ChFO_SLE.Enabled = True
        '13/06/2016 MAX
        chRUDatiAE.Enabled = True

        ChNewBoll.Enabled = True
        chRSS.Enabled = True
        ChLO.Enabled = True
        CHMAND_PAG.Enabled = True
        ChBuildingM.Enabled = True
        'ChFornitori.Enabled =true
        ChCCV.Enabled = True
        ChCC.Enabled = True
        ChBPNuovo.Enabled = True
        ChBPFormalizzazione.Enabled = True
        ChBPConvalidaAler.Enabled = True
        ChBPVociServizio.Enabled = True
        ChMS.Enabled = True
        chModPagamento.Enabled = True
        chVariazioneComp.Enabled = True
        chDashboard.Enabled = True
chAnticipoSingolaVoce.Enabled = True
        chkRitornaInBozza.Enabled = True
        chTecnicoAmm.Enabled = True
        chFQM.Enabled = True
        chkGestAllegati.Enabled = True
        ChkAnnullaRAT.Enabled = True
        ChRUMAV.Enabled = True
        chkParamCP.Enabled = True

        chkcambiostruttura.Enabled = True
        cmbFiliale.Enabled = True

        chMassIngiunz.Enabled = True
        chSingIngiunz.Enabled = True

        'MAX 20/12/2017
        ChFO_LOG.Enabled = True
		ChERPVerificaReq.Enabled = True
        ChPosizGrad.Enabled = True
        chkMotiviDec.Enabled = True
        chkOA.Enabled = True
    End Function

    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Try
            Dim IMPIANTI As String = "0000000000000"
            Dim BUONO As Boolean = True
            Dim filiale As String = Session.Item("cmbFiliale")

            ApriConnessione()
            par.myTrans = par.OracleConn.BeginTransaction()

            If String.IsNullOrEmpty(txtUtente.Text) Then
                BUONO = False
                Response.Write("<script>alert('Inserire il nome utente!! Memorizzazione non effettuata.');</script>")
            End If

            If txtCognome.Text <> "" And txtNome.Text <> "" Then
                If par.ControllaCF(UCase(txtCF.Text)) = True Then
                    If par.ControllaCFNomeCognome(UCase(txtCF.Text), UCase(txtCognome.Text), UCase(txtNome.Text)) = False Then
                        BUONO = False
                        Response.Write("<script>alert('Codice fiscale errato!! Memorizzazione non effettuata.');</script>")
                    End If
                Else
                    BUONO = False
                    Response.Write("<script>alert('Codice fiscale errato!! Memorizzazione non effettuata.');</script>")
                End If
            End If

            If txtUtente.Text.ToUpper <> nomeutente.Value Then
                par.cmd.CommandText = "select * from OPERATORI where UPPER(OPERATORE)='" & txtUtente.Text.ToUpper & "'"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.HasRows = True Then
                    BUONO = False
                    Response.Write("<script>alert('Esiste già un operatore con lo stesso nome utente!');</script>")
                Else
                    nomeutente.Value = txtUtente.Text.ToUpper
                End If
                myReader1.Close()
            End If
            Dim flag_evento As Integer = 0
            If lid_Operatore = -1 Then
                If BUONO = True Then
                    par.cmd.CommandText = "INSERT INTO OPERATORI (ID,PW,PW_DATA_INSERIMENTO,REVOCA,FL_ELIMINATO) VALUES (SEQ_OPERATORI.NEXTVAL,'" & par.ComputeHash("SEPA", "SHA512", Nothing) & "','" & Format(Now, "yyyyMMdd") & "',0,'0')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT SEQ_OPERATORI.CURRVAL FROM DUAL"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        lid_Operatore = myReader(0)
                    End If
                    myReader.Close()
                    'par.cmd.CommandText = "insert into eventi_operatori (id_operatore,data_ora,testo,funzione,operatore_mod) values (" & Session.Item("id_operatore") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & par.PulisciStrSql(par.cmd.CommandText) & "','INSERIMENTO OPERATORE','" & par.PulisciStrSql(txtUtente.Text) & "')"
                    'par.cmd.ExecuteNonQuery()
                    'Try
                    '    Dim operatore As String = Session.Item("ID_OPERATORE")
                    '    Eventi_Gestione(operatore, "F55", "INSERIMENTO NUOVO OPERATORE " & txtUtente.Text)
                    '    flag_evento = 1
                    'Catch ex As Exception
                    '    lblErrore.Text = ex.Message
                    '    lblErrore.Visible = True
                    'End Try
                    If ScriviLogOp("", "", "", 1, Format(Now, "yyyyMMddHHmmss")) = False Then

                    End If
                End If
            End If

            'ORDINE
            'ELETTRICO - IDRICO - TERM.CENTRALIZZATO - TERM.AUTONOMO - TELERISCALD. - SOLLEVAMENTO - METEORICHE - ANTINCENDIO - TUT. IMMOBILE - CANNA FUM.

            If ChDemEl.Checked = True Then
                IMPIANTI = "1"
            Else
                IMPIANTI = "0"
            End If

            If ChDemID.Checked = True Then
                IMPIANTI = IMPIANTI & "1"
            Else
                IMPIANTI = IMPIANTI & "0"
            End If

            If ChDemTC.Checked = True Then
                IMPIANTI = IMPIANTI & "1"
            Else
                IMPIANTI = IMPIANTI & "0"
            End If

            If ChDemTA.Checked = True Then
                IMPIANTI = IMPIANTI & "1"
            Else
                IMPIANTI = IMPIANTI & "0"
            End If

            If ChDemTR.Checked = True Then
                IMPIANTI = IMPIANTI & "1"
            Else
                IMPIANTI = IMPIANTI & "0"
            End If

            If ChDemSO.Checked = True Then
                IMPIANTI = IMPIANTI & "1"
            Else
                IMPIANTI = IMPIANTI & "0"
            End If

            If ChDemAM.Checked = True Then
                IMPIANTI = IMPIANTI & "1"
            Else
                IMPIANTI = IMPIANTI & "0"
            End If

            If ChDemAI.Checked = True Then
                IMPIANTI = IMPIANTI & "1"
            Else
                IMPIANTI = IMPIANTI & "0"
            End If

            If ChDemTI.Checked = True Then
                IMPIANTI = IMPIANTI & "1"
            Else
                IMPIANTI = IMPIANTI & "0"
            End If

            If ChDemCF.Checked = True Then
                IMPIANTI = IMPIANTI & "1"
            Else
                IMPIANTI = IMPIANTI & "0"
            End If

            If ChDemGAS.Checked = True Then
                IMPIANTI = IMPIANTI & "1"
            Else
                IMPIANTI = IMPIANTI & "0"
            End If

            If ChDemCITOFONICO.Checked = True Then
                IMPIANTI = IMPIANTI & "1"
            Else
                IMPIANTI = IMPIANTI & "0"
            End If

            If ChDemTV.Checked = True Then
                IMPIANTI = IMPIANTI & "1"
            Else
                IMPIANTI = IMPIANTI & "0"
            End If

            If BUONO = True Then

                par.cmd.CommandText = "UPDATE OPERATORI SET ID_UFFICIO = " & filiale & ",MOD_BP_RSS_SL=" & Valore01(chRSSSl.Checked) _
                & ",FL_AUTORIZZAZIONE_ODL=" & Valore01(chAutorizzazione.Checked) _
                & ",FL_ESTRAZIONE_STR=" & Valore01(ChEstraiSTR.Checked) _
                & ",FL_CONSUNTIVAZIONE_STR=" & Valore01(ChImportaSTR.Checked) _
                & ",FL_UTENZE=" & Valore01(chkUtenze.Checked) _
                & ",FL_SUPERDIRETTORE=" & Valore01(chSuperDirettore.Checked) _
                & ",MOD_BP_RSS=" & Valore01(chRSS.Checked) _
                & ",MOD_CONT_RINN_USD=" & Valore01(ChCONTRATTIRinnoviUSD.Checked) _
                & ",MOD_CONT_CAMBIO_BOX=" & Valore01(ChCONTRATTICambiBox.Checked) & ",MOD_MOROSITA=" & Valore01(ChMOR.Checked) _
                & ",MOD_MOROSITA_SL=" & Valore01(ChMOR_SL.Checked) & ",FL_DA_CONFERMARE='0'," _
                & "OPERATORE='" & Trim(par.PulisciStrSql(txtUtente.Text.ToUpper)) _
                & "',COGNOME='" & Trim(par.PulisciStrSql(txtCognome.Text.ToUpper)) _
                & "',NOME='" & Trim(par.PulisciStrSql(txtNome.Text.ToUpper)) _
                & "',COD_FISCALE='" & par.PulisciStrSql(txtCF.Text.ToUpper) _
                & "',COD_ANA='" & par.PulisciStrSql(txtAnagrafico.Text) _
                & "',MOD_AU='" & Valore01(Chau.Checked) _
                & "',MOD_AU_CONS='" & Valore01(ChAUconsulta.Checked) _
                & "',MOD_ABB='" & Valore01(Chabbinamento.Checked) _
                & "',MOD_ABB_DEC='" & Valore01(ChABB_DEC.Checked) _
                & "',mod_demanio='" & Valore01(chDemanio.Checked) _
                & "',mod_contratti='" & Valore01(ChContratti.Checked) _
                & "',mod_contratti_TESTO='" & Valore01(ChCONTRATTItesto.Checked) _
                & "',mod_bollette='0" _
                & "',SEPA_WEB='1" _
                & "',ID_CAF='" & cmbEnte.SelectedItem.Value _
                & "',SEPA='0" _
                & "',PG='0" _
                & "',CAMPUS='0" _
                & "',ASS_ESTERNA='0" _
                & "',ALLOGGI='0" _
                & "',LIVELLO='0" _
                & "',MOD_CONS='0" _
                & "',MOD_PED='0" _
                & "',AUTOCOMPILAZIONE='0" _
                & "',MOD_MANUTENZIONI='0" _
                & "',MOD_CONTRATTI_PARAM='0" _
                & "',MOD_PED2='" & Valore01(chPED2Completa.Checked) _
                & "',mod_contabile='" & Valore01(chContabilita.Checked) & "',MOD_PED2_ESTERNA='" & Valore01(CHPED2ESTERNA.Checked) _
                & "',MOD_PED2_SOLO_LETTURA='" & Valore01(ChAPConsultazione.Checked) _
                & "',DATA_PW='" & par.PulisciStrSql(par.AggiustaData(txtScadenzaPW.Text)) _
                & "',FL_ABB_ERP='" & Valore01(cERP.Checked) _
                & "',FL_ABB_392='" & Valore01(c392.Checked) _
                & "',FL_ABB_431='" & Valore01(c431.Checked) _
                & "',FL_ABB_UD='" & Valore01(cUD.Checked) _
                & "',FL_ABB_OA='" & Valore01(cOA.Checked) _
                & "',MOD_CONTRATTI_L='" & Valore01(ChCONTRATTILETTURA.Checked) _
                & "',MOD_CONTRATTI_D='" & Valore01(ChCONTRATTId.Checked) _
                & "',MOD_CONTRATTI_INS='" & Valore01(ChCONTRATTIins.Checked) _
                & "',MOD_CONTRATTI_ISTAT='" & Valore01(ChCONTRATTIist.Checked) _
                & "',MOD_CONTRATTI_INT='" & Valore01(ChCONTRATTIint.Checked) _
                & "',MOD_CONTRATTI_REG='" & Valore01(ChCONTRATTIreg.Checked) _
                & "',MOD_CONTRATTI_IMP='" & Valore01(ChCONTRATTIimp.Checked) _
                & "',MOD_AU_PROP_DEC='" & Valore01(ChAUpropDec.Checked) _
                & "',MOD_AU_DOC_NEC=" & Valore01(ChAUDocNecessaria.Checked) _
                & ",MOD_AU_DIFF_MP=" & Valore01(ChAUDiffida.Checked) _
                & ",MOD_CONTRATTI_MOR=" & Valore01(ChCONTRATTIMor.Checked) _
                & ",FL_ABB_FO='" & Valore01(ChFO.Checked) _
                & "',FL_ABB_CS='" & Valore01(ChCS.Checked) _
                & "',FL_ABB_CONV='" & Valore01(ChConvenzionato.Checked) _
                & "',MOD_DEM_SL='" & Valore01(ChDemanioSL.Checked) _
                & "',mod_cont_ragioneria=" & Valore01(ChContRagioneria.Checked) _
                & ",mod_cont_patrimoniali=" & Valore01(ChContPatrimoniali.Checked) _
                & ",mod_cont_flussi=" & Valore01(ChContFlussi.Checked) _
                & ",MOD_CONT_RIMB=" & Valore01(ChContRimborsi.Checked) _
                & ",MOD_CONT_COMPENSI=" & Valore01(ChContCompensi.Checked) _
                & ",MOD_CENS_MANUT=" & Valore01(Me.MOD_CENS_MANUT.Checked) _
                & ",FL_PRG_INTERVENTI_MASSIVO=" & Valore01(Me.chkCaricMassProgrInt.Checked) _
                & ",CENS_MANUT_SL=" & Valore01(Me.ChCensManutSL.Checked) _
                & ",MOD_DEM_IMP='" & IMPIANTI _
                & "',MOD_CICLO_P=" & Valore01(chCicloP.Checked) _
                & ",BP_NUOVO_PIANO=" & Valore01(ChBPNuovo.Checked) _
                & ",BP_FORMALIZZAZIONE=" & Valore01(ChBPFormalizzazione.Checked) _
                & ",BP_COMPILAZIONE=" & Valore01(ChBPCompilazione.Checked) _
                & ",BP_CONV_ALER=" & Valore01(ChBPConvalidaAler.Checked) _
                & ",BP_CONV_COMUNE=" & Valore01(ChBPConvalidaComune.Checked) _
                & ",BP_CAPITOLI=" & Valore01(ChBPCapitoli.Checked) _
                & ",BP_FORMALIZZAZIONE_L=" & Valore01(ChBPFormalizzazione0.Checked) _
                & ",BP_COMPILAZIONE_L=" & Valore01(ChBPCompilazione0.Checked) _
                & ",BP_CONV_ALER_L=" & Valore01(ChBPConvalidaAler0.Checked) _
                & ",BP_CONV_COMUNE_L=" & Valore01(ChBPConvalidaComune0.Checked) _
                & ",BP_CAPITOLI_L=" & Valore01(ChBPCapitoli0.Checked) _
                & ",BP_VOCI_SERVIZI=" & Valore01(ChBPVociServizio.Checked) _
                & ",BP_VOCI_SERVIZI_L=" & Valore01(ChBPVociServizio0.Checked) _
                & ",BP_MS=" & Valore01(ChMS.Checked) _
                & ",BP_MS_L=" & Valore01(ChMSL.Checked) _
                & ",BP_OP=" & Valore01(ChOP.Checked) _
                & ",BP_OP_L=" & Valore01(ChOPL.Checked) _
                & ",BP_PC=" & Valore01(ChPC.Checked) _
                & ",BP_PC_L=" & Valore01(ChPCL.Checked) _
                & ",BP_LO=" & Valore01(ChLO.Checked) _
                & ",BP_LO_L=" & Valore01(ChLOL.Checked) _
                & ",BP_CC=" & Valore01(ChCC.Checked) _
                & ",BP_CC_L=" & Valore01(ChCCL.Checked) _
                & ",mod_GESTIONE_CONTATTI=" & Valore01(chGC.Checked) _
                & ",mod_GESTIONE_CONTATTI_sl=" & Valore01(ChGC_SL.Checked) _
                & ",FL_GC_TABELLE_SUPP=" & Valore01(ChGC_TabelleSupporto.Checked) _
                & ",FL_GC_REPORT=" & Valore01(ChGC_Report.Checked) _
                & ",FL_GC_SEGNALAZIONI=" & Valore01(ChGC_Segnalazioni.Checked) _
                & ",FL_GC_APPUNTAMENTI=" & Valore01(ChGC_Appuntamenti.Checked) _
                & ",FL_GC_CALENDARIO=" & Valore01(ChGC_Calendario.Checked) _
                & ",FL_GC_SUPERVISORE=" & Valore01(ChGC_Supervisore.Checked) _
                & ",MOD_AUTOGESTIONI=" & Valore01(ChGE.Checked) _
                & ",MOD_AUTOGESTIONI_SL=" & Valore01(ChGEL.Checked) _
                & ",BP_CC_V=" & Valore01(ChCCV.Checked) _
                & ",BP_GENERALE=" & Valore01(ChBPGenerale.Checked) _
                & ",MOD_MAND_PAGAMENTO = " & Valore01(CHMAND_PAG.Checked) _
                & ",MOD_SATISFACTION = " & Valore01(ChSatisfaction.Checked) _
                & ",MOD_SATISFACTION_SL = " & Valore01(ChSatisfactionL.Checked) _
                & ",MOD_SATISFACTION_SV=" & Valore01(ChSatisfSuperVisore.Checked) _
                & ",MOD_CONT_P_EXTRA=" & Valore01(ChCONTRATTIPEXTRA.Checked) _
                & ",MOD_AU_CREAGRUPPI=" & Valore01(ChAUCreaGruppo.Checked) _
                & ",MOD_AU_SIMULA_APPLICA=" & Valore01(ChAUSimulaApplica.Checked) _
                & ",MOD_MOROSITA_ANN=" & Valore01(ChMOR_ANN.Checked) _
                & ",MOD_CONT_RATEIZZA=" & Valore01(ChCONTRATTIRateizzazione.Checked) _
                & ",MOD_ANNULLA_RATEIZZA=" & Valore01(ChkAnnullaRAT.Checked) _
                & ",MOD_AMM_RPT_P_EXTRA = " & Valore01(ChAmmRptPagExtraMav.Checked) _
                & ",BP_VARIAZIONI = " & Valore01(ChVariazioni.Checked) _
                & ",BP_VARIAZIONI_SL = " & Valore01(ChVariazioniSL.Checked) _
                & ",ASS_FORMALIZZAZIONE = " & Valore01(ChASSNuovo.Checked) _
                & ",ASS_COMPILAZIONE = " & Valore01(ChASSCompila.Checked) _
                & ",ASS_CONV_ALER = " & Valore01(ChASSConvAler.Checked) _
                & ",ASS_CONV_COMUNE = " & Valore01(ChASSConvComune.Checked) _
                & ",OP_RESP_VSA = " & Valore01(ch_OP_RESP_VSA.Checked) _
                & ",OP_NORM_VSA = " & Valore01(ch_OP_VSA.Checked) _
                & ",MOD_AU_CONV_VIS_TUTTO=" & Valore01(ChAUConvTutti.Checked) _
                & ",MOD_AU_CONV_SINDACATI=" & Valore01(ChAUConvSind.Checked) _
                & ",MOD_AU_CONV_REI= " & Valore01(ChAUConvReimposta.Checked) _
                & ",MOD_AU_CONV_RIP= " & Valore01(ChAUConvRip.Checked) _
                & ",MOD_AU_CONV_ANN= " & Valore01(ChAUConvAnnulla.Checked) _
                & ",MOD_AU_CONV_SPOSTA= " & Valore01(ChAUConvSposta.Checked) _
                & ",MOD_AU_CONV_N= " & Valore01(ChAUConvIns.Checked) _
                & ",MOD_BP_RESIDUI=" & Valore01(ChBPResidui.Checked) _
                & ",MOD_CONDOMINIO='" & Valore01(chCondominio.Checked) _
                & "',MOD_CONDOMINIO_SL='" & Valore01(ChCondominioSL.Checked) _
                & "',FL_CAMBIO_STRUTTURA=" & Valore01(chkcambiostruttura.Checked) _
                & ",MOD_EMRI='" & Valore01(ChABB_EMRI.Checked) _
                & "',MOD_SPOSTAM_ANNULL=" & Valore01(ChSpostaAnnullo.Checked) _
                & ",FL_SPESE_REVERSIBILI=" & Valore01(ChSpReversibili.Checked) _
                & ",FL_SPESE_REV_SL=" & Valore01(ChSpReversibiliSl.Checked) _
                & ",FL_SPESE_REV_APP_BOLLETTE=" & Valore01(ChBPAppPreventivi.Checked) _
                & ",MOD_ELAB_MASS_GEST=" & Valore01(ChElaborazMass.Checked) _
                & ",MOD_ELAB_SING_GEST=" & Valore01(ChElaborazSing.Checked) _
                & ",MOD_AU_GESTIONE=" & Valore01(ChAUGestione.Checked) _
                & ",MOD_AU_GESTIONE_MOD=" & Valore01(ChAUGestioneMod.Checked) _
                & ",MOD_AU_GESTIONE_STR=" & Valore01(ChAUGestioneStr.Checked) _
                & ",MOD_AU_GESTIONE_LISTE=" & Valore01(ChAUGestioneLista.Checked) _
                & ",MOD_AU_GESTIONE_ESCLUSIONI=" & Valore01(ChAUGestioneEsclusione.Checked) _
                & ",MOD_AU_GESTIONE_CONVOCABILI=" & Valore01(ChAUGestioneConvocabili.Checked) _
                & ",MOD_AU_GESTIONE_GRUPPI=" & Valore01(ChAUGestioneGrConv.Checked) _
                & ",MOD_AU_CREA_CONV=" & Valore01(ChAUDefConv.Checked) _
                & ",MOD_AU_ELIMINA_BANDO=" & Valore01(ChAUGestione0.Checked) _
                & ",MOD_AU_ANNULLA_DIFF=" & Valore01(ChAUAnnullaStampa.Checked) _
                & ",MOD_AU_ELIMINA_F_CONV=" & Valore01(ChAUEliminaConv.Checked) _
                & ",MOD_TRASFERIM_RUA=" & Valore01(ChTrasfPag.Checked) _
                & ",MOD_DISTANZE_COMUNI=" & Valore01(chDistanzaKM.Checked) _
                & ",MOD_RECA_GEST=" & Valore01(chRecaGest.Checked) _
                & ",MOD_RUAU=" & Valore01(chRUAU.Checked) _
                & ",MOD_RUEXP=" & Valore01(chRUExport.Checked) _
                & ",MOD_RUSALDI=" & Valore01(chRUSALDI.Checked) _
                & ",FL_PARAM_CICLO_PASSIVO=" & Valore01(chkParamCP.Checked) _
                & ",MOD_ARCHIVIO=" & Valore01(ChArchivio.Checked) _
                & ",MOD_ARCHIVIO_IM=" & Valore01(ChArchIM.Checked) _
                & ",MOD_ARCHIVIO_C=" & Valore01(ChArchC.Checked) _
                & ",MOD_CREAZ_BOLL=" & Valore01(ChNewBoll.Checked) _
                & ",MOD_RILIEVO=" & Valore01(chModRilievo.Checked) _
                & ",FL_RILIEVO_GEST=" & Valore01(chRilievoGEST.Checked) _
                & ",FL_RILIEVO_CAR=" & Valore01(chRilievoCDati.Checked) _
                & ",FL_RILIEVO_PAR=" & Valore01(chRilievoParam.Checked) _
                & ",MOD_SIRAPER=" & Valore01(ChSiraper.Checked) _
                & ",MOD_ARPA=" & Valore01(cbARPA.Checked) _
                & ",MOD_BUILDING_MANAGER=" & Valore01(ChBuildingM.Checked) _
                & ",MOD_CONT_NOTE=" & Valore01(chRUNote.Checked) _
                & ",MOD_CREAZ_MAVONLINE=" & Valore01(ChRUMAV.Checked) _
                & ",FL_ANNULLA_SAL = " & Valore01(chkAnnullaSal.Checked) _
                 & ",FL_ANNULLA_ODL=" & Valore01(chkAnnullaODL.Checked) _
                & ",MOD_AU_RICERCA=" & Valore01(ChAURicerca.Checked) _
                & ",MOD_AU_REPORT=" & Valore01(ChAUReport.Checked) _
                & ",MOD_AU_AGENDA_CERCA=" & Valore01(ChAUCercaInquilino.Checked) _
                & ",MOD_AU_AGENDA_SOSPESE=" & Valore01(ChAUSospeseSind.Checked) _
                & ",MOD_AU_AGENDA_MOTS=" & Valore01(ChAUGestMotSosp.Checked) _
                & ",MOD_AU_CF=" & Valore01(ChAUCalcolaCF.Checked) _
                & ",MOD_SICUREZZA=" & Valore01(ChSicurezza.Checked) _
                & ",MOD_SICUREZZA_SL=" & Valore01(ChTpa_SL.Checked) _
                & ",FL_SEC_CREA_SEGN = " & Valore01(ChCreaSegn.Checked) _
                & ",FL_SEC_MODIF_SEGN = " & Valore01(ChModificaSegn.Checked) _
                & ",FL_SEC_ASS_OPERATORI = " & Valore01(ChAssegnOperatori.Checked) _
                & ",FL_SEC_AGENDA = " & Valore01(ChVisualizzaAgenda.Checked) _
                & ",FL_SEC_GEST_COMPLETA = " & Valore01(ChCreaSegnIntProc.Checked) _
                & ",MOD_RU_CRDE=" & Valore01(chCrDe.Checked) _
                & ",MOD_CONT_DEP=" & Valore01(chRimbDep.Checked) _
                & ",MOD_RU_DATI_AE=" & Valore01(chRUDatiAE.Checked) _
                & ",MOD_FORNITORI=" & Valore01(ChModFornitori.Checked) _
                & ",MOD_FO_ID_FO=" & cmbFornitori.SelectedItem.Value _
                & ",MOD_FORNITORI_RDO=" & Valore01(ChFO_RDO.Checked) _
                & ",MOD_FORNITORI_ODL=" & Valore01(ChFO_ODL.Checked) _
                & ",MOD_FORNITORI_RPT=" & Valore01(ChFO_RPT.Checked) _
                & ",MOD_FORNITORI_SLE=" & Valore01(ChFO_SLE.Checked) _
                & ",MOD_FORNITORI_PARAM=" & Valore01(ChFO_PAR.Checked) _
                & ",MOD_PAG_RUOLI=" & Valore01(chkInserimRuoli.Checked) _
                & ",MOD_REPORT_RUOLI=" & Valore01(chkReportRuoli.Checked) _
                & ",MOD_SBLOCCO_BOLL=" & Valore01(chkSbloccoBoll.Checked) _
                & ",FL_FORZARIMBORSO=" & Valore01(chkForzaRestituz.Checked) _
                & ",FL_CAMBIO_IVA_ODL= " & Valore01(chkCambioIvaOdl.Checked) _
                & ",BP_MS_RIELABORA_CDP = " & Valore01(chkRielaboraCDP.Checked) _
                & ",BP_MS_RIELABORA_SAL = " & Valore01(chkRielaboraSAL.Checked) _
                & ",BP_OP_RIELABORA_CDP = " & Valore01(chkPagRielabCDP.Checked) _
                & ",BP_PC_RIELABORA_CDP = " & Valore01(chkPagCanoneRielCDP.Checked) _
                & ",BP_PC_RIELABORA_SAL = " & Valore01(chkPagCanoneRielSAL.Checked) _
                & ",BP_RRS_RIELABORA_CDP = " & Valore01(chkRRSRielabCDP.Checked) _
                & ",BP_RRS_RIELABORA_SAL = " & Valore01(chkRRSRielabSAL.Checked) _
                & ",FL_ANAGRAFICA = " & Valore01(chkModificaANA.Checked) _
                & ",MOD_FORNITORI_LOG=" & Valore01(ChFO_LOG.Checked) _
                & ",FL_CP_MOD_PAGAMENTO=" & Valore01(chModPagamento.Checked) _
                & ",FL_CP_VARIAZ_COMP=" & Valore01(chVariazioneComp.Checked) _
                & ",FL_CP_DASHBOARD=" & Valore01(chDashboard.Checked) _
                & ",FL_CP_RITORNA_BOZZA=" & Valore01(chkRitornaInBozza.Checked) _
                & ",CP_APPALTO_SINGOLA_VOCE=" & Valore01(chAnticipoSingolaVoce.Checked) _
                & ",FL_CP_TECN_AMM=" & Valore01(chTecnicoAmm.Checked) _
                & ",FL_FQM=" & Valore01(chFQM.Checked) _
                & ",FL_GEST_ALLEGATI=" & Valore01(chkGestAllegati.Checked) _
                & ",FL_CP_VARIAZIONE_IMPORTI=" & Valore01(ChVariazioneImporti.Checked) _
                & ",FL_SCELTA_DEST_ECCED=" & Valore01(chkSceltaDestEcc.Checked) _
                & ",MOD_MASS_INGIUNZIONI=" & Valore01(chMassIngiunz.Checked) _
                & ",MOD_SING_INGIUNZIONI=" & Valore01(chSingIngiunz.Checked) _
                & ",MOD_PAG_INGIUNZ=" & Valore01(chkInserimIng.Checked) _
                & ",MOD_REPORT_INGIUNZ=" & Valore01(chkReportIng.Checked) _
                & ",MOD_ERP=" & Valore01(Cherp.Checked) _
                & ",MOD_ERP_REQUISITI=" & Valore01(ChERPVerificaReq.Checked) _
                & ",MOD_ERP_POS_GRAD=" & Valore01(ChPosizGrad.Checked) _
                & ",FL_FORZA_SCADENZA=" & Valore01(chkForzaScad.Checked) _
    & ",MOD_MOTIVI_DECISIONI=" & Valore01(chkMotiviDec.Checked) _
                & ",RU_GESTIONE_OA=" & Valore01(chkOA.Checked) _
                & " where id=" & lid_Operatore
                par.cmd.ExecuteNonQuery()

                If cmbEnte.SelectedItem.Value = "63" Then
                    par.cmd.CommandText = "UPDATE OPERATORI SET MOD_CONTRATTI=1,MOD_DEMANIO=1,MOD_PED2=1,MOD_PED2_SOLO_LETTURA=1,MOD_CONTRATTI_L=1,MOD_DEM_SL=1,MOD_AU_CONV_VIS_TUTTO=1,MOD_AU_CONV_SPOSTA=1,MOD_AU_CONV_ANN=1,MOD_AU_CONV_RIP=1,MOD_AU_CONV_REI=1,MOD_RUEXP=1,MOD_AU_RICERCA=1,MOD_AU_AGENDA_CERCA=1 " _
                                        & " where id=" & lid_Operatore
                    par.cmd.ExecuteNonQuery()
                End If

                MemorizzaAttributi()

                CaricaAttributi()
                IdOperatore.Value = lid_Operatore
                Response.Write("<script>alert('Operazione Effettuata!');</script>")
            End If

            par.myTrans.Commit()
            par.cmd.Dispose()
            ChiudiConnessione()
            BUONO = True
        Catch ex As Exception
            par.myTrans.Rollback()
            ChiudiConnessione()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Protected Sub imgAzzera_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAzzera.Click
        If lid_Operatore <> -1 Then
            Try

                ApriConnessione()
                par.myTrans = par.OracleConn.BeginTransaction()

                par.cmd.CommandText = "update OPERATORI set pw='" & par.ComputeHash("SEPA", "SHA512", Nothing) & "',pw_data_inserimento='" & Format(Now, "yyyyMMdd") & "' where id=" & lid_Operatore
                par.cmd.ExecuteNonQuery()
                If ScriviLogOp("", "", "", 2, Format(Now, "yyyyMMddHHmmss")) = False Then

                End If
                'Try
                '    Dim operatore As String = Session.Item("ID_OPERATORE")
                '    Eventi_Gestione(operatore, "F02", "AZZERAMENTO PASSWORD OPERATORE " & txtUtente.Text)
                'Catch ex As Exception
                '    lblErrore.Text = ex.Message
                '    lblErrore.Visible = True
                'End Try

                'par.cmd.CommandText = "insert into eventi_operatori (id_operatore,data_ora,testo,funzione,operatore_mod) values (" & Session.Item("id_operatore") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','','AZZERA PASSWORD','" & par.PulisciStrSql(nomeoperatore.Value) & "')"
                'par.cmd.ExecuteNonQuery()

                Response.Write("<script>alert('Operazione Effettuata!');</script>")
                par.myTrans.Commit()
                ChiudiConnessione()
            Catch ex As Exception
                par.myTrans.Rollback()
                ChiudiConnessione()
                lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub imgRevoca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRevoca.Click
        If lid_Operatore <> -1 Then
            Try

                ApriConnessione()
                par.myTrans = par.OracleConn.BeginTransaction()

                par.cmd.CommandText = "update OPERATORI set revoca=2,motivo_revoca='REVOCATO DA SUPER USER' where id=" & lid_Operatore
                par.cmd.ExecuteNonQuery()

                If ScriviLogOp("", "", "", 3, Format(Now, "yyyyMMddHHmmss")) = False Then

                End If
                'Try
                '    Dim operatore As String = Session.Item("ID_OPERATORE")
                '    Eventi_Gestione(operatore, "F02", "REVOCA UTENZA OPERATORE " & txtUtente.Text)
                'Catch ex As Exception
                '    lblErrore.Text = ex.Message
                '    lblErrore.Visible = True
                'End Try

                'par.cmd.CommandText = "insert into eventi_operatori (id_operatore,data_ora,testo,funzione,operatore_mod) values (" & Session.Item("id_operatore") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','','REVOCA UTENZA','" & par.PulisciStrSql(nomeoperatore.Value) & "')"
                'par.cmd.ExecuteNonQuery()

                Response.Write("<script>alert('Operazione Effettuata!');</script>")
                par.myTrans.Commit()
                ChiudiConnessione()

                'Image1.Visible = True
                lblRevoca.Visible = True
                lblRevocaMotivo.Visible = True
                lblRevocaMotivo.Text = "REVOCATO DA SUPER USER"

            Catch ex As Exception
                par.myTrans.Rollback()
                ChiudiConnessione()
                lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub imgAnnullaRevoca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAnnullaRevoca.Click
        If lid_Operatore <> -1 Then
            Try
                ApriConnessione()
                par.myTrans = par.OracleConn.BeginTransaction()

                par.cmd.CommandText = "update OPERATORI set revoca=0,motivo_revoca='',pw_data_inserimento='" & Format(Now, "yyyyMMdd") & "' where id=" & lid_Operatore
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO OPERATORI_WEB_LOG (ID_OPERATORE,DATA_ORA_IN,DATA_ORA_OUT) VALUES (" & IdOperatore.Value & ",'" & Format(Now, "yyyyMMddhhmm") & "','" & Format(Now, "yyyyMMddhhmm") & "')"
                par.cmd.ExecuteNonQuery()

                If ScriviLogOp("", "", "", 4, Format(Now, "yyyyMMddHHmmss")) = False Then

                End If
                'Try
                '    Dim operatore As String = Session.Item("ID_OPERATORE")
                '    Eventi_Gestione(operatore, "F02", "ANNULLAMENTO REVOCA UTENZA OPERATORE " & txtUtente.Text)
                'Catch ex As Exception
                '    lblErrore.Text = ex.Message
                '    lblErrore.Visible = True
                'End Try

                'par.cmd.CommandText = "insert into eventi_operatori (id_operatore,data_ora,testo,funzione,operatore_mod) values (" & Session.Item("id_operatore") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','','ANNULLA REVOCA','" & par.PulisciStrSql(nomeoperatore.Value) & "')"
                'par.cmd.ExecuteNonQuery()

                Response.Write("<script>alert('Operazione Effettuata!');</script>")
                par.myTrans.Commit()
                ChiudiConnessione()

                'Image1.Visible = False
                lblRevoca.Visible = False
                lblRevocaMotivo.Visible = False

            Catch ex As Exception
                par.myTrans.Rollback()
                ChiudiConnessione()
                lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        End If
    End Sub

    Public Property OperatoreFornitori() As Long
        Get
            If Not (ViewState("par_OperatoreFornitori") Is Nothing) Then
                Return CLng(ViewState("par_OperatoreFornitori"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_OperatoreFornitori") = value
        End Set

    End Property

    Public Property Nuovo_Operatore() As Long
        Get
            If Not (ViewState("par_Nuovo_Operatore") Is Nothing) Then
                Return CLng(ViewState("par_Nuovo_Operatore"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_Nuovo_Operatore") = value
        End Set

    End Property

    Protected Sub ImgEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgEsci.Click
        Try
            If Nuovo_Operatore = 0 Then
                Response.Write("<script>location.href='RisultatoRicercaOpSUA.aspx?RG=" & Request.QueryString("RG") & "&CN=" & Request.QueryString("CN") & "&NM=" & Request.QueryString("NM") & "&CF=" & Request.QueryString("CF") & "&OP=" & par.VaroleDaPassare(Request.QueryString("OP")) & "&EN=" & par.IfEmpty(Request.QueryString("EN"), "-1") & "';</script>")
            Else
                Response.Write("<script>location.href='../pagina_home.aspx';</script>")
            End If
        Catch ex As Exception
            Beep()
        End Try
    End Sub

    Protected Sub imgAzzera0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAzzera0.Click
        If lid_Operatore <> -1 Then
            Try
                ApriConnessione()
                par.myTrans = par.OracleConn.BeginTransaction()

                Dim utilizzato As String = ""
                par.cmd.CommandText = "select count(id_operatore) as contatore from operatori_web_log where id_operatore =" & lid_Operatore
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    utilizzato = par.IfNull(myReader("contatore"), 0)
                End If
                myReader.Close()

                If utilizzato = 0 Then
                    If sicuro.Value = "1" Then
                        par.cmd.CommandText = "update OPERATORI set fl_eliminato='1' where id=" & lid_Operatore
                        par.cmd.ExecuteNonQuery()

                        If ScriviLogOp("", "", "", 5, Format(Now, "yyyyMMddHHmmss")) = False Then

                        End If
                        'Try
                        '    Dim operatore As String = Session.Item("ID_OPERATORE")
                        '    Eventi_Gestione(operatore, "F02", "CANCELLAZIONE OPERATORE " & txtUtente.Text)
                        'Catch ex As Exception
                        '    lblErrore.Text = ex.Message
                        '    lblErrore.Visible = True
                        'End Try

                        'par.cmd.CommandText = "insert into eventi_operatori (id_operatore,data_ora,testo,funzione,operatore_mod) values (" & Session.Item("id_operatore") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','','ELIMINA OPERATORE','" & par.PulisciStrSql(nomeoperatore.Value) & "')"
                        'par.cmd.ExecuteNonQuery()

                        Response.Write("<script>alert('Operazione Effettuata!');document.location.href='../pagina_home.aspx';</script>")
                    End If
                Else
                    Response.Write("<script>alert('Non puoi cancellare questo operatore');</script>")
                End If
                par.myTrans.Commit()
                ChiudiConnessione()
            Catch ex As Exception
                par.myTrans.Rollback()
                ChiudiConnessione()
                lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        End If
    End Sub

#Region "ChkCicloPassivo"

    Protected Sub FunctionLivello()
        Try
            Me.ChBPConvalidaComune.Checked = False
            Me.ChBPConvalidaComune.Enabled = False

            Me.ChASSConvComune.Checked = False
            Me.ChASSConvComune.Enabled = False

            Me.ChBPNuovo.Enabled = True

            Me.ChBPFormalizzazione.Enabled = True

            Me.ChBPCompilazione.Enabled = True

            Me.ChBPConvalidaAler.Enabled = True

            Me.ChBPCapitoli.Enabled = True

            Me.ChBPVociServizio.Enabled = True

            Me.ChVariazioni.Enabled = True

            Me.ChMS.Enabled = True
            chkCambioIvaOdl.Enabled = True
            'Rielabora SAL-CDP
            chkRielaboraCDP.Enabled = True
            chkRielaboraSAL.Enabled = True
            chkPagRielabCDP.Enabled = True
            chkPagCanoneRielCDP.Enabled = True
            chkPagCanoneRielSAL.Enabled = True
            chkRRSRielabCDP.Enabled = True
            chkRRSRielabSAL.Enabled = True

            Me.ChOP.Enabled = True

            Me.ChPC.Enabled = True

            Me.ChLO.Enabled = True

            Me.ChCC.Enabled = True



            Me.ChBuildingM.Enabled = True

            Me.ChCCV.Enabled = True

            Me.chRSS.Enabled = True

            Me.CHMAND_PAG.Enabled = True

            Me.ChASSNuovo.Enabled = True

            Me.ChASSCompila.Enabled = True

            Me.ChASSConvAler.Enabled = True

            Me.chCicloP.Enabled = True
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub ChBPCompilazione0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChBPCompilazione0.CheckedChanged
        If ChBPCompilazione0.Checked = True Then
            ChBPCompilazione.Checked = True
        End If
    End Sub

    Protected Sub ChVariazioniSL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChVariazioniSL.CheckedChanged
        If ChVariazioniSL.Checked = True Then
            ChVariazioni.Checked = True
        End If
    End Sub

    Protected Sub ChBPConvalidaComune0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChBPConvalidaComune0.CheckedChanged
        If ChBPConvalidaComune0.Checked = True Then
            ChBPConvalidaComune.Checked = True
        End If
    End Sub

    Protected Sub ChBPCapitoli0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChBPCapitoli0.CheckedChanged
        If ChBPCapitoli0.Checked = True Then
            ChBPCapitoli.Checked = True
        End If
    End Sub

    Protected Sub ChBPConvalidaAler0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChBPConvalidaAler0.CheckedChanged
        If ChBPConvalidaAler0.Checked = True Then
            ChBPConvalidaAler.Checked = True
        End If
    End Sub

    Protected Sub ChBPVociServizio0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChBPVociServizio0.CheckedChanged
        If ChBPVociServizio0.Checked = True Then
            ChBPVociServizio.Checked = True
        End If
    End Sub

    Protected Sub ChOPL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChOPL.CheckedChanged
        If ChOPL.Checked = True Then
            ChOP.Checked = True
        End If
    End Sub

    Protected Sub ChMSL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChMSL.CheckedChanged
        If ChMSL.Checked = True Then
            ChMS.Checked = True
        End If
    End Sub

    Protected Sub ChPCL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChPCL.CheckedChanged
        If ChPCL.Checked = True Then
            ChPC.Checked = True
        End If
    End Sub

    Protected Sub ChCCL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCCL.CheckedChanged
        If ChCCL.Checked = True Then
            ChCC.Checked = True
        End If
    End Sub

    Protected Sub ChLOL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChLOL.CheckedChanged
        If ChLOL.Checked = True Then
            ChLO.Checked = True
        End If
    End Sub

    Protected Sub chRSSSl_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chRSSSl.CheckedChanged
        If chRSSSl.Checked = True Then
            chRSS.Checked = True
        End If
    End Sub

    Protected Sub ChBPFormalizzazione0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChBPFormalizzazione0.CheckedChanged
        If ChBPFormalizzazione0.Checked = True Then
            ChBPFormalizzazione.Checked = True
        End If
    End Sub
    Protected Sub ChSpReversibiliSl_CheckedChanged(sender As Object, e As System.EventArgs) Handles ChSpReversibiliSl.CheckedChanged
        If ChSpReversibiliSl.Checked = True Then
            ChSpReversibili.Checked = True
        End If
    End Sub

#End Region


    Protected Sub cmbEnte_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEnte.SelectedIndexChanged
        If cmbEnte.SelectedItem.Value = "63" Then
            DisattivaAutorizzazioni()
            AzzeraValori()
            chGC.Enabled = True
            ChGC_SL.Enabled = True
            ChGC_Segnalazioni.Enabled = True
            ChGC_Supervisore.Enabled = True
            ChGC_TabelleSupporto.Enabled = True
            ChGC_Appuntamenti.Enabled = True
            ChGC_Report.Enabled = True
            ChGC_Calendario.Enabled = True
        Else
            AbilitaAutorizzazioni()
        End If
    End Sub

    Private Function AzzeraValori()
        Chau.Checked = False
        ChAUconsulta.Checked = False
        ChAUpropDec.Checked = False
        ChAUDiffida.Checked = False
        ChAUCreaGruppo.Checked = False
        ChAUGestione.Checked = False
        ChAUGestione.Checked = False
        ChAUGestioneMod.Checked = False
        ChAUGestioneStr.Checked = False
        ChAUGestioneEsclusione.Checked = False
        ChAUGestioneConvocabili.Checked = False
        ChAUGestioneGrConv.Checked = False
        ChAUGestioneLista.Checked = False
        ChAUDefConv.Checked = False
        ChAUEliminaConv.Checked = False
        ChAUConvTutti.Checked = False
        ChAUConvIns.Checked = False
        ChAUConvSposta.Checked = False
        ChAUConvAnnulla.Checked = False
        ChAUConvRip.Checked = False
        ChAUConvReimposta.Checked = False
        ChAUConvSind.Checked = False
        ChAUAnnullaStampa.Checked = False
        Chabbinamento.Checked = False
        cERP.Checked = False
        c392.Checked = False
        c431.Checked = False
        cUD.Checked = False
        cOA.Checked = False
        ChFO.Checked = False
        ChCS.Checked = False
        ChConvenzionato.Checked = False
        ChABB_DEC.Checked = False
        ch_OP_VSA.Checked = False
        ch_OP_RESP_VSA.Checked = False
        ChABB_EMRI.Checked = False
        chPED2Completa.Checked = False
        ChAPConsultazione.Checked = False
        CHPED2ESTERNA.Checked = False
        MOD_CENS_MANUT.Checked = False
        chkCaricMassProgrInt.Checked = False
        ChCensManutSL.Checked = False
        chContabilita.Checked = False
        ChContRagioneria.Checked = False
        ChContPatrimoniali.Checked = False
        ChContFlussi.Checked = False
        ChContRimborsi.Checked = False
        ChContCompensi.Checked = False
        chCicloP.Checked = False
        ChBPNuovo.Checked = False
        ChBPGenerale.Checked = False
        ChSpReversibili.Checked = False
        ChBPResidui.Checked = False
        ChBPAppPreventivi.Checked = False
        ChSpReversibiliSl.Checked = False
        ChBPFormalizzazione.Checked = False
        ChBPCompilazione.Checked = False
        ChVariazioni.Checked = False
        ChBPFormalizzazione0.Checked = False
        ChBPCompilazione0.Checked = False
        ChVariazioniSL.Checked = False
        ChBPConvalidaAler.Checked = False
        ChBPCapitoli.Checked = False
        ChBPConvalidaComune.Checked = False
        ChBPConvalidaAler0.Checked = False
        ChBPCapitoli0.Checked = False
        ChBPConvalidaComune0.Checked = False
        ChBPVociServizio.Checked = False
        ChASSNuovo.Checked = False
        ChASSConvAler.Checked = False
        ChBPVociServizio0.Checked = False
        ChASSCompila.Checked = False
        ChASSConvComune.Checked = False
        ChMS.Checked = False
        ChOP.Checked = False
        ChPC.Checked = False
        ChMSL.Checked = False
        chkPagCanoneRielCDP.Enabled = False
        chkPagRielabCDP.Enabled = False
        chkRielaboraSAL.Enabled = False
        chkRRSRielabCDP.Enabled = False
        chkRRSRielabSAL.Enabled = False
        ChOPL.Checked = False
        ChPCL.Checked = False
        ChLO.Checked = False
        ChCC.Checked = False

        ChBuildingM.Checked = False
        ChCCV.Checked = False
        ChLOL.Checked = False
        ChCCL.Checked = False
        chRSS.Checked = False
        CHMAND_PAG.Checked = False
        chRSSSl.Checked = False
        chAutorizzazione.Checked = False
        chkAnnullaSal.Checked = False
        chkAnnullaODL.Checked = False
        chkUtenze.Checked = False
        chSuperDirettore.Checked = False
        chGC.Checked = False
        ChGC_SL.Checked = False
        ChGC_TabelleSupporto.Checked = False
        ChGC_Report.Checked = False
        ChGC_Segnalazioni.Checked = False
        ChGC_Appuntamenti.Checked = False
        ChGC_Calendario.Checked = False
        ChGC_Supervisore.Checked = False
        chCondominio.Checked = False
        ChCondominioSL.Checked = False
        chDemanio.Checked = False
        ChDemanioSL.Checked = False
        ChDemEl.Checked = False
        ChDemTA.Checked = False
        ChDemAM.Checked = False
        ChDemCF.Checked = False
        ChDemTV.Checked = False
        ChDemID.Checked = False
        ChDemTR.Checked = False
        ChDemAI.Checked = False
        ChDemGAS.Checked = False
        ChDemTC.Checked = False
        ChDemSO.Checked = False
        ChDemTI.Checked = False
        ChDemCITOFONICO.Checked = False
        ChContratti.Checked = False
        ChCONTRATTILETTURA.Checked = False
        ChCONTRATTIins.Checked = False
        ChCONTRATTIreg.Checked = False
        ChCONTRATTIimp.Checked = False
        ChCONTRATTIist.Checked = False
        ChCONTRATTItesto.Checked = False
        ChCONTRATTId.Checked = False
        ChCONTRATTIint.Checked = False
        ChCONTRATTIMor.Checked = False
        ChCONTRATTIRinnoviUSD.Checked = False
        ChCONTRATTICambiBox.Checked = False
        ChCONTRATTIPEXTRA.Checked = False
        ChCONTRATTIRateizzazione.Checked = False
        ChSpostaAnnullo.Checked = False
        ChAmmRptPagExtraMav.Checked = False
        ChElaborazMass.Checked = False
        ChElaborazSing.Checked = False
        ChTrasfPag.Checked = False
        chDistanzaKM.Checked = False
        chRecaGest.Checked = False
        chRUAU.Checked = False
        chRUSALDI.Checked = False
        chRUExport.Checked = False
        ChGE.Checked = False
        ChGEL.Checked = False
        ChMOR.Checked = False
        ChMOR_SL.Checked = False
        ChMOR_ANN.Checked = False
        ChSatisfaction.Checked = False
        ChSatisfactionL.Checked = False
        ChSatisfSuperVisore.Checked = False
        ChAUGestione0.Checked = False
        ChAUSimulaApplica.Checked = False
        ChBPNuovo.Checked = False
        chCicloP.Checked = False
        chRimbDep.Checked = False
        ChArchivio.Checked = False
        ChArchC.Checked = False
        ChArchIM.Checked = False
        chModRilievo.Checked = False
        chRilievoCDati.Checked = False
        chRilievoGEST.Checked = False
        chRilievoParam.Checked = False
        'MAX 20/07/2015
        chRUNote.Checked = False
        'MAX 14/10/2015
        ChAUDocNecessaria.Checked = False
        ChAURicerca.Checked = False
        ChAUReport.Checked = False
        ChAUCercaInquilino.Checked = False
        ChAUSospeseSind.Checked = False
        ChAUGestMotSosp.Checked = False
        ChAUCalcolaCF.Checked = False
        'ANTONELLO 09/12/2015
        ChSiraper.Checked = False
        'ANTONELLO 17/03/2017
        cbARPA.Checked = False
        ChSicurezza.Checked = False
        ChTpa_SL.Checked = False
        ChCreaSegn.Checked = False
        ChModificaSegn.Checked = False
        ChAssegnOperatori.Checked = False
        ChVisualizzaAgenda.Checked = False
        ChCreaSegnIntProc.Checked = False
        'MAX 17/02/2016
        chCrDe.Checked = False
        'MAX 23_02_2016
        ChModFornitori.Checked = False
        cmbFornitori.Enabled = False
        ChFO_RDO.Checked = False
        ChFO_ODL.Checked = False
        ChFO_RPT.Checked = False
        ChFO_PAR.Checked = False
        ChFO_SLE.Checked = False
        '13/06/2016 MAX
        chRUDatiAE.Checked = False

        ChNewBoll.Checked = False
        chRSS.Checked = False
        ChLO.Checked = False
        CHMAND_PAG.Checked = False
        ChBuildingM.Checked = False
        'ChFornitori.Checked = False
        ChCCV.Checked = False
        ChCC.Checked = False
        ChBPNuovo.Checked = False
        ChBPFormalizzazione.Checked = False
        ChBPConvalidaAler.Checked = False
        ChBPVociServizio.Checked = False
        ChMS.Checked = False

        ChkAnnullaRAT.Checked = False
        ChRUMAV.Checked = False
        chkParamCP.Checked = False


        chkcambiostruttura.Checked = False
        cmbFiliale.SelectedIndex = -1
        cmbFiliale.Items.FindByValue("-1").Selected = True
        cmbFiliale.Enabled = False

        'MAX 20/12/2017
        ChFO_LOG.Checked = False

        chMassIngiunz.Checked = False
        chSingIngiunz.Checked = False
    End Function

End Class