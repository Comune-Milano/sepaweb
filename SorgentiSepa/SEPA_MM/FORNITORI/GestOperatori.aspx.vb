Imports Telerik.Web.UI

Partial Class FORNITORI_Default
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            If Session.Item("MOD_FORNITORI") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            If Session.Item("MOD_FORNITORI_PARAM") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                BindGrid()
            End If
        Catch ex As Exception
                        Session.Add("ERRORE", "Provenienza: Fornitori - Operatori - Carica - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub OnItemDataBoundHandler(ByVal sender As Object, ByVal e As GridItemEventArgs)
        Try
            If (TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode) Then
                Dim radCombo1 As RadComboBox = CType(e.Item.FindControl("cmbFornitore"), RadComboBox)
                par.caricaComboTelerik("select id,ragione_sociale from SISCOM_MI.FORNITORI order by RAGIONE_SOCIALE ASC", radCombo1, "ID", "RAGIONE_SOCIALE", True)
                radCombo1.AutoPostBack = True


                If e.Item.DataItem.GetType.Name = "DataRowView" Then
                    If par.IfNull(CStr(par.IfNull(e.Item.DataItem("DATA_PW"), "")), "") <> "" Then
                        Dim radCombo As RadDatePicker = CType(e.Item.FindControl("txtDataPW"), RadDatePicker)
                        radCombo.SelectedDate = CDate(par.IfNull(e.Item.DataItem("DATA_PW"), ""))
                    End If


                    radCombo1.SelectedValue = par.IfNull(e.Item.DataItem("IDF"), "-1")

                    Dim radtext As RadTextBox = CType(e.Item.FindControl("txtOperatore"), RadTextBox)
                    vOperatore.Value = radtext.Text

                    radtext = CType(e.Item.FindControl("txtCognome"), RadTextBox)
                    vCognome.Value = radtext.Text

                    radtext = CType(e.Item.FindControl("txtNome"), RadTextBox)
                    vNome.Value = radtext.Text

                    radtext = CType(e.Item.FindControl("txtCodFiscale"), RadTextBox)
                    vCF.Value = radtext.Text

                    Dim radcmb As RadComboBox = CType(e.Item.FindControl("cmbFornitore"), RadComboBox)
                    vFornitore.Value = radcmb.Text
                    HiddenIdFornitore.Value = radcmb.SelectedValue

                    par.caricaComboTelerik("select DISTINCT ID_GRUPPO,NUM_REPERTORIO from SISCOM_MI.APPALTI WHERE ID_FORNITORE = " _
                                           & HiddenIdFornitore.Value & " order by 2 DESC", CType(e.Item.FindControl("cmbAppalto"), RadComboBox), "ID_GRUPPO", "NUM_REPERTORIO", False)
                    CType(e.Item.FindControl("cmbAppalto"), RadComboBox).Enabled = True
                    par.cmd.CommandText = "SELECT ID_GRUPPO FROM SISCOM_MI.OPERATORI_FO_APPALTI WHERE ID_FORNITORE = " & HiddenIdFornitore.Value & " AND ID_OPERATORE = " & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
                    Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                    If dt.Rows.Count > 0 Then
                        For Each riga As Data.DataRow In dt.Rows
                            For Each item As RadComboBoxItem In CType(e.Item.FindControl("cmbAppalto"), RadComboBox).Items
                                If item.Value = riga.Item("ID_GRUPPO") Then
                                    item.Checked = True
                                End If
                            Next
                        Next
                    End If
                    Dim radData As RadDatePicker = CType(e.Item.FindControl("txtDataPW"), RadDatePicker)
                    If radData.SelectedDate.ToString <> "" Then
                        vFornitore.Value = Format(CDate(radData.SelectedDate.ToString), "yyyyMMdd")
                    Else
                        vFornitore.Value = ""
                    End If

                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - GestOperatori - OnItemDataBoundHandler - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Private Sub BindGrid()

        sStrSql = "SELECT OPERATORI.ID,OPERATORE,OPERATORI.COGNOME,OPERATORI.NOME,OPERATORI.COD_FISCALE,TO_DATE(DATA_PW,'YYYYmmdd') AS DATA_PW,FORNITORI.RAGIONE_SOCIALE,FORNITORI.ID AS IDF FROM OPERATORI,SISCOM_MI.FORNITORI WHERE MOD_FO_LIMITAZIONI=1 and OPERATORI.FL_ELIMINATO='0' AND NVL(MOD_FORNITORI,0)=1 AND OPERATORI.MOD_FO_ID_FO=FORNITORI.ID ORDER BY OPERATORE ASC,RAGIONE_SOCIALE"
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

    Private Function ScriviLogOp(ByVal lIdOp As String, ByVal CAMPO As String, ByVal VAL_PRECEDENTE As String, ByVal VAL_IMPOSTATO As String, OPERAZIONE As Integer, tempo As String) As Boolean
        Try
            Dim aperto As Boolean = False

            If par.cmd.Connection.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
                aperto = True
            End If

            par.cmd.CommandText = "Insert into OPERATORI_LOG (ID_OPERATORE, DATA_ORA, ID_OPERATORE_M, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) Values (" & Session.Item("ID_OPERATORE") & ", '" & tempo & "', " & lIdOp & ", '" & par.PulisciStrSql(CAMPO) & "', '" & par.PulisciStrSql(VAL_PRECEDENTE) & "', '" & par.PulisciStrSql(VAL_IMPOSTATO) & "', " & OPERAZIONE & ")"
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

    Protected Sub RadGridOperatori_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridOperatori.DeleteCommand
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)

            par.cmd.CommandText = "update OPERATORI set fl_eliminato='1' where id=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
            par.cmd.ExecuteNonQuery()

            If ScriviLogOp(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString(), "", "", "", 5, Format(Now, "yyyyMMddHHmmss")) = False Then

            End If

            connData.chiudi(True)
            VisualizzaAlert("Operazione effettuata", 1)
            RadGridOperatori.Rebind()
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Naviga", "validNavigation = true;", True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - EliminaOperatore - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridOperatori_InsertCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridOperatori.InsertCommand
        Try

            Dim VALIDO As Boolean = True
            Me.connData = New CM.datiConnessione(par, False, False)
            Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)
            connData.apri(True)

            Dim Operatore As String = ""
            Operatore = par.PulisciStrSql(CType(userControl.FindControl("txtOperatore"), RadTextBox).Text)

            Dim Cognome As String = ""
            Cognome = par.PulisciStrSql(CType(userControl.FindControl("txtCognome"), RadTextBox).Text)

            Dim Nome As String = ""
            Nome = par.PulisciStrSql(CType(userControl.FindControl("txtNome"), RadTextBox).Text)

            Dim CodFiscale As String = ""
            CodFiscale = UCase(CType(userControl.FindControl("txtCodFiscale"), RadTextBox).Text)
            If CodFiscale <> "" Then
                If par.ControllaCF(CodFiscale) = True Then
                    If par.ControllaCFNomeCognome(CodFiscale, Cognome, Nome) = False Then
                        VALIDO = False
                        VisualizzaAlert("Inserire un codice fiscale valido", 2)
                    End If
                Else
                    VALIDO = False
                    VisualizzaAlert("Inserire un codice fiscale valido", 2)
                End If
            End If

            Dim DataPW As String = ""
            DataPW = CType(userControl.FindControl("txtDataPW"), RadDatePicker).SelectedDate.ToString

            Dim Fornitore As String = ""
            If CType(userControl.FindControl("cmbFornitore"), RadComboBox).SelectedValue <> "-1" Then
                Fornitore = par.insDbValue(CType(userControl.FindControl("cmbFornitore"), RadComboBox).SelectedValue, True, False)
            Else
                Fornitore = "NULL"
            End If
            If Fornitore = "''" Or Fornitore = "NULL" Then
                VALIDO = False
                'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Scegliere un Fornitoree valido!", 300, 150, "Attenzione", Nothing, Nothing)
                VisualizzaAlert("Scegliere un fornitore valido", 2)
            End If

            par.cmd.CommandText = "select * from OPERATORI where fl_eliminato='0' and UPPER(OPERATORE)='" & Operatore.ToUpper & "'"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.HasRows = True Then
                VALIDO = False
                'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Il campo Operatore è già utilizzato!", 300, 150, "Attenzione", Nothing, Nothing)
                VisualizzaAlert("Il campo operatore è già utilizzato", 2)
            End If
            myReader1.Close()

            If VALIDO = True Then
                Dim LID_OPERATORE As Long = 0
                par.cmd.CommandText = "INSERT INTO OPERATORI (ID,PW,PW_DATA_INSERIMENTO,REVOCA,FL_ELIMINATO,MOD_FO_ID_FO,MOD_FO_LIMITAZIONI) VALUES (SEQ_OPERATORI.NEXTVAL,'" & par.ComputeHash("SEPA", "SHA512", Nothing) & "','" & Format(Now, "yyyyMMdd") & "',0,'0'," & Fornitore & ",1)"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "SELECT SEQ_OPERATORI.CURRVAL FROM DUAL"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    LID_OPERATORE = myReader(0)
                End If
                myReader.Close()
                If DataPW <> "" Then
                    par.cmd.CommandText = "UPDATE OPERATORI SET ID_UFFICIO = -1,MOD_BP_RSS_SL=0,FL_AUTORIZZAZIONE_ODL=0,FL_UTENZE=0,FL_SUPERDIRETTORE=0,MOD_BP_RSS=0,MOD_CONT_RINN_USD=0,MOD_CONT_CAMBIO_BOX=0,MOD_MOROSITA=0,MOD_MOROSITA_SL=0,FL_DA_CONFERMARE='0',OPERATORE='" & par.PulisciStrSql(UCase(Operatore)) & "',COGNOME='" & par.PulisciStrSql(UCase(Cognome)) & "',NOME='" & par.PulisciStrSql(UCase(Nome)) & "',COD_FISCALE='" & par.PulisciStrSql(UCase(CodFiscale)) & "',COD_ANA='',MOD_AU='0',MOD_AU_CONS='0',MOD_ABB='0',MOD_ABB_DEC='0',mod_demanio='0',mod_contratti='0',mod_contratti_TESTO='0',mod_bollette='0',SEPA_WEB='1',ID_CAF='2',SEPA='0',PG='0',CAMPUS='0',ASS_ESTERNA='0',ALLOGGI='0',LIVELLO='0',MOD_CONS='0',MOD_PED='0',AUTOCOMPILAZIONE='0',MOD_MANUTENZIONI='0',MOD_CONTRATTI_PARAM='0',MOD_PED2='0',mod_contabile='0',MOD_PED2_ESTERNA='0',MOD_PED2_SOLO_LETTURA='1',DATA_PW='" & Format(CDate(DataPW), "yyyyMMdd") & "',FL_ABB_ERP='0',FL_ABB_392='0',FL_ABB_431='0',FL_ABB_UD='0',FL_ABB_OA='0',MOD_CONTRATTI_L='0',MOD_CONTRATTI_D='0',MOD_CONTRATTI_INS='0',MOD_CONTRATTI_ISTAT='0',MOD_CONTRATTI_INT='0',MOD_CONTRATTI_REG='0',MOD_CONTRATTI_IMP='0',MOD_AU_PROP_DEC='0',MOD_AU_DOC_NEC=0,MOD_AU_DIFF_MP=0,MOD_CONTRATTI_MOR=0,FL_ABB_FO='0',FL_ABB_CS='0',FL_ABB_CONV='0',MOD_DEM_SL='0',mod_cont_ragioneria=0,mod_cont_patrimoniali=0,mod_cont_flussi=0,MOD_CONT_RIMB=0,MOD_CONT_COMPENSI=0,MOD_CENS_MANUT=0,CENS_MANUT_SL=1,MOD_DEM_IMP='0000000000000',MOD_CICLO_P=0,BP_NUOVO_PIANO=0,BP_FORMALIZZAZIONE=0,BP_COMPILAZIONE=0,BP_CONV_ALER=0,BP_CONV_COMUNE=0,BP_CAPITOLI=0,BP_FORMALIZZAZIONE_L=0,BP_COMPILAZIONE_L=0,BP_CONV_ALER_L=0,BP_CONV_COMUNE_L=0,BP_CAPITOLI_L=0,BP_VOCI_SERVIZI=0,BP_VOCI_SERVIZI_L=0,BP_MS=0,BP_MS_L=0,BP_OP=0,BP_OP_L=0,BP_PC=0,BP_PC_L=0,BP_LO=0,BP_LO_L=0,BP_CC=0,BP_CC_L=0,mod_GESTIONE_CONTATTI=0,mod_GESTIONE_CONTATTI_sl=0,FL_GC_TABELLE_SUPP=0,FL_GC_REPORT=0,FL_GC_SEGNALAZIONI=0,FL_GC_APPUNTAMENTI=0,FL_GC_CALENDARIO=0,FL_GC_SUPERVISORE=0,MOD_AUTOGESTIONI=0,MOD_AUTOGESTIONI_SL=0,BP_CC_V=0,BP_GENERALE=0,MOD_MAND_PAGAMENTO = 0,MOD_SATISFACTION = 0,MOD_SATISFACTION_SL = 0,MOD_SATISFACTION_SV=0,MOD_CONT_P_EXTRA=0,MOD_AU_CREAGRUPPI=0,MOD_AU_SIMULA_APPLICA=0,MOD_MOROSITA_ANN=0,MOD_CONT_RATEIZZA=0,MOD_AMM_RPT_P_EXTRA = 0,BP_VARIAZIONI = 0,BP_VARIAZIONI_SL = 0,ASS_FORMALIZZAZIONE = 0,ASS_COMPILAZIONE = 0,ASS_CONV_ALER = 0,ASS_CONV_COMUNE = 0,OP_RESP_VSA = 0,OP_NORM_VSA = 0,MOD_AU_CONV_VIS_TUTTO=0,MOD_AU_CONV_SINDACATI=0,MOD_AU_CONV_REI= 0,MOD_AU_CONV_RIP= 0,MOD_AU_CONV_ANN= 0,MOD_AU_CONV_SPOSTA= 0,MOD_AU_CONV_N= 0,MOD_BP_RESIDUI=0,MOD_CONDOMINIO='0',MOD_CONDOMINIO_SL='0',FL_CAMBIO_STRUTTURA=0,MOD_EMRI='0',MOD_SPOSTAM_ANNULL=0,FL_SPESE_REVERSIBILI=0,FL_SPESE_REV_SL=0,FL_SPESE_REV_APP_BOLLETTE=0,MOD_ELAB_MASS_GEST=0,MOD_ELAB_SING_GEST=0,MOD_AU_GESTIONE=0,MOD_AU_GESTIONE_MOD=0,MOD_AU_GESTIONE_STR=0,MOD_AU_GESTIONE_LISTE=0,MOD_AU_GESTIONE_ESCLUSIONI=0,MOD_AU_GESTIONE_CONVOCABILI=0,MOD_AU_GESTIONE_GRUPPI=0,MOD_AU_CREA_CONV=0,MOD_AU_ELIMINA_BANDO=0,MOD_AU_ANNULLA_DIFF=0,MOD_AU_ELIMINA_F_CONV=0,MOD_TRASFERIM_RUA=0,MOD_DISTANZE_COMUNI=0,MOD_RECA_GEST=0,MOD_RUAU=0,MOD_RUEXP=0,MOD_RUSALDI=0,MOD_CONT_DEP=0,MOD_ARCHIVIO=0,MOD_ARCHIVIO_IM=0,MOD_ARCHIVIO_C=0,MOD_RILIEVO=0,FL_RILIEVO_GEST=0,FL_RILIEVO_CAR=0,FL_RILIEVO_PAR=0,MOD_CREAZ_BOLL=0,MOD_BUILDING_MANAGER=0,MOD_CONT_NOTE=0,FL_ANNULLA_SAL = 0,MOD_AU_RICERCA=0,MOD_AU_REPORT=0,MOD_AU_AGENDA_CERCA=0,MOD_AU_AGENDA_SOSPESE=0,MOD_AU_AGENDA_MOTS=0,MOD_AU_CF=0,MOD_SIRAPER=0,MOD_SICUREZZA=0,MOD_SICUREZZA_SL=0,FL_SEC_CREA_SEGN = 0,FL_SEC_MODIF_SEGN = 0,FL_SEC_ASS_OPERATORI = 0,FL_SEC_AGENDA = 0,FL_SEC_GEST_COMPLETA = 0,MOD_RU_CRDE=0,MOD_FORNITORI=1,MOD_FO_ID_FO=" & Fornitore & ",MOD_FORNITORI_RDO=1,MOD_FORNITORI_ODL=1,MOD_FORNITORI_RPT=1,MOD_FORNITORI_PARAM=0 where id=" & LID_OPERATORE
                Else
                    par.cmd.CommandText = "UPDATE OPERATORI SET ID_UFFICIO = -1,MOD_BP_RSS_SL=0,FL_AUTORIZZAZIONE_ODL=0,FL_UTENZE=0,FL_SUPERDIRETTORE=0,MOD_BP_RSS=0,MOD_CONT_RINN_USD=0,MOD_CONT_CAMBIO_BOX=0,MOD_MOROSITA=0,MOD_MOROSITA_SL=0,FL_DA_CONFERMARE='0',OPERATORE='" & par.PulisciStrSql(UCase(Operatore)) & "',COGNOME='" & par.PulisciStrSql(UCase(Cognome)) & "',NOME='" & par.PulisciStrSql(UCase(Nome)) & "',COD_FISCALE='" & par.PulisciStrSql(UCase(CodFiscale)) & "',COD_ANA='',MOD_AU='0',MOD_AU_CONS='0',MOD_ABB='0',MOD_ABB_DEC='0',mod_demanio='0',mod_contratti='0',mod_contratti_TESTO='0',mod_bollette='0',SEPA_WEB='1',ID_CAF='2',SEPA='0',PG='0',CAMPUS='0',ASS_ESTERNA='0',ALLOGGI='0',LIVELLO='0',MOD_CONS='0',MOD_PED='0',AUTOCOMPILAZIONE='0',MOD_MANUTENZIONI='0',MOD_CONTRATTI_PARAM='0',MOD_PED2='0',mod_contabile='0',MOD_PED2_ESTERNA='0',MOD_PED2_SOLO_LETTURA='1',DATA_PW='',FL_ABB_ERP='0',FL_ABB_392='0',FL_ABB_431='0',FL_ABB_UD='0',FL_ABB_OA='0',MOD_CONTRATTI_L='0',MOD_CONTRATTI_D='0',MOD_CONTRATTI_INS='0',MOD_CONTRATTI_ISTAT='0',MOD_CONTRATTI_INT='0',MOD_CONTRATTI_REG='0',MOD_CONTRATTI_IMP='0',MOD_AU_PROP_DEC='0',MOD_AU_DOC_NEC=0,MOD_AU_DIFF_MP=0,MOD_CONTRATTI_MOR=0,FL_ABB_FO='0',FL_ABB_CS='0',FL_ABB_CONV='0',MOD_DEM_SL='0',mod_cont_ragioneria=0,mod_cont_patrimoniali=0,mod_cont_flussi=0,MOD_CONT_RIMB=0,MOD_CONT_COMPENSI=0,MOD_CENS_MANUT=0,CENS_MANUT_SL=1,MOD_DEM_IMP='0000000000000',MOD_CICLO_P=0,BP_NUOVO_PIANO=0,BP_FORMALIZZAZIONE=0,BP_COMPILAZIONE=0,BP_CONV_ALER=0,BP_CONV_COMUNE=0,BP_CAPITOLI=0,BP_FORMALIZZAZIONE_L=0,BP_COMPILAZIONE_L=0,BP_CONV_ALER_L=0,BP_CONV_COMUNE_L=0,BP_CAPITOLI_L=0,BP_VOCI_SERVIZI=0,BP_VOCI_SERVIZI_L=0,BP_MS=0,BP_MS_L=0,BP_OP=0,BP_OP_L=0,BP_PC=0,BP_PC_L=0,BP_LO=0,BP_LO_L=0,BP_CC=0,BP_CC_L=0,mod_GESTIONE_CONTATTI=0,mod_GESTIONE_CONTATTI_sl=0,FL_GC_TABELLE_SUPP=0,FL_GC_REPORT=0,FL_GC_SEGNALAZIONI=0,FL_GC_APPUNTAMENTI=0,FL_GC_CALENDARIO=0,FL_GC_SUPERVISORE=0,MOD_AUTOGESTIONI=0,MOD_AUTOGESTIONI_SL=0,BP_CC_V=0,BP_GENERALE=0,MOD_MAND_PAGAMENTO = 0,MOD_SATISFACTION = 0,MOD_SATISFACTION_SL = 0,MOD_SATISFACTION_SV=0,MOD_CONT_P_EXTRA=0,MOD_AU_CREAGRUPPI=0,MOD_AU_SIMULA_APPLICA=0,MOD_MOROSITA_ANN=0,MOD_CONT_RATEIZZA=0,MOD_AMM_RPT_P_EXTRA = 0,BP_VARIAZIONI = 0,BP_VARIAZIONI_SL = 0,ASS_FORMALIZZAZIONE = 0,ASS_COMPILAZIONE = 0,ASS_CONV_ALER = 0,ASS_CONV_COMUNE = 0,OP_RESP_VSA = 0,OP_NORM_VSA = 0,MOD_AU_CONV_VIS_TUTTO=0,MOD_AU_CONV_SINDACATI=0,MOD_AU_CONV_REI= 0,MOD_AU_CONV_RIP= 0,MOD_AU_CONV_ANN= 0,MOD_AU_CONV_SPOSTA= 0,MOD_AU_CONV_N= 0,MOD_BP_RESIDUI=0,MOD_CONDOMINIO='0',MOD_CONDOMINIO_SL='0',FL_CAMBIO_STRUTTURA=0,MOD_EMRI='0',MOD_SPOSTAM_ANNULL=0,FL_SPESE_REVERSIBILI=0,FL_SPESE_REV_SL=0,FL_SPESE_REV_APP_BOLLETTE=0,MOD_ELAB_MASS_GEST=0,MOD_ELAB_SING_GEST=0,MOD_AU_GESTIONE=0,MOD_AU_GESTIONE_MOD=0,MOD_AU_GESTIONE_STR=0,MOD_AU_GESTIONE_LISTE=0,MOD_AU_GESTIONE_ESCLUSIONI=0,MOD_AU_GESTIONE_CONVOCABILI=0,MOD_AU_GESTIONE_GRUPPI=0,MOD_AU_CREA_CONV=0,MOD_AU_ELIMINA_BANDO=0,MOD_AU_ANNULLA_DIFF=0,MOD_AU_ELIMINA_F_CONV=0,MOD_TRASFERIM_RUA=0,MOD_DISTANZE_COMUNI=0,MOD_RECA_GEST=0,MOD_RUAU=0,MOD_RUEXP=0,MOD_RUSALDI=0,MOD_CONT_DEP=0,MOD_ARCHIVIO=0,MOD_ARCHIVIO_IM=0,MOD_ARCHIVIO_C=0,MOD_RILIEVO=0,FL_RILIEVO_GEST=0,FL_RILIEVO_CAR=0,FL_RILIEVO_PAR=0,MOD_CREAZ_BOLL=0,MOD_BUILDING_MANAGER=0,MOD_CONT_NOTE=0,FL_ANNULLA_SAL = 0,MOD_AU_RICERCA=0,MOD_AU_REPORT=0,MOD_AU_AGENDA_CERCA=0,MOD_AU_AGENDA_SOSPESE=0,MOD_AU_AGENDA_MOTS=0,MOD_AU_CF=0,MOD_SIRAPER=0,MOD_SICUREZZA=0,MOD_SICUREZZA_SL=0,FL_SEC_CREA_SEGN = 0,FL_SEC_MODIF_SEGN = 0,FL_SEC_ASS_OPERATORI = 0,FL_SEC_AGENDA = 0,FL_SEC_GEST_COMPLETA = 0,MOD_RU_CRDE=0,MOD_FORNITORI=1,MOD_FO_ID_FO=" & Fornitore & ",MOD_FORNITORI_RDO=1,MOD_FORNITORI_ODL=1,MOD_FORNITORI_RPT=1,MOD_FORNITORI_PARAM=0 where id=" & LID_OPERATORE
                End If
                par.cmd.ExecuteNonQuery()

                If ScriviLogOp(LID_OPERATORE, "", "", "", 1, Format(Now, "yyyyMMddHHmmss")) = False Then

                End If
                connData.chiudi(True)
                VisualizzaAlert("Operazione effettuata", 1)
                RadGridOperatori.Rebind()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Naviga", "validNavigation = true;", True)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Inserisci Operatore - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Public Property NumeroElementi() As Integer
        Get
            If Not (ViewState("par_NumeroElementi") Is Nothing) Then
                Return CStr(ViewState("par_NumeroElementi"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_NumeroElementi") = value
        End Set
    End Property

    Protected Sub RadGridOperatori_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridOperatori.ItemCommand

    End Sub

    Protected Sub RadGridOperatori_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridOperatori.ItemDataBound
        If isExporting.Value = "1" Then
            If e.Item.ItemIndex > 0 Then
                Dim context As RadProgressContext = RadProgressContext.Current
                If context.SecondaryTotal <> NumeroElementi Then
                    context.SecondaryTotal = NumeroElementi
                End If
                context.SecondaryValue = e.Item.ItemIndex.ToString()
                context.SecondaryPercent = Int((e.Item.ItemIndex.ToString() * 100) / NumeroElementi)
                context.CurrentOperationText = "Export excel in corso"

            End If
        End If
    End Sub



    'Protected Sub RadGridOperatori_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridOperatori.ItemCommand
    '    Try
    '        If e.CommandName = RadGrid.InitInsertCommandName Then

    '            isExporting.Value = "1"
    '            Dim context As RadProgressContext = RadProgressContext.Current
    '            context.SecondaryTotal = 0
    '            context.SecondaryValue = 0
    '            context.SecondaryPercent = 0
    '            RadGridOperatori.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx
    '            RadGridOperatori.ExportSettings.IgnorePaging = True
    '            RadGridOperatori.ExportSettings.ExportOnlyData = True
    '            RadGridOperatori.ExportSettings.OpenInNewWindow = True
    '            RadGridOperatori.MasterTableView.ExportToExcel()


    '        End If

    '    Catch ex As Exception
    '        connData.chiudi(False)
    '        Session.Add("ERRORE", "Provenienza: Fornitori - GestOperatori - ItemCommand - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    'Protected Sub RadGridOperatori_ItemDeleted(sender As Object, e As Telerik.Web.UI.GridDeletedEventArgs) Handles RadGridOperatori.ItemDeleted
    '    Try
    '        If Not e.Exception Is Nothing Then
    '            e.ExceptionHandled = True

    '        End If
    '    Catch ex As Exception
    '        connData.chiudi(False)
    '        Session.Add("ERRORE", "Provenienza: Fornitori - GestOperatori - ItemDeleted - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    'Protected Sub RadGridOperatori_ItemInserted(sender As Object, e As Telerik.Web.UI.GridInsertedEventArgs) Handles RadGridOperatori.ItemInserted
    '    Try
    '        If Not e.Exception Is Nothing Then
    '            e.ExceptionHandled = True
    '            e.KeepInInsertMode = False
    '            'DisplayMessage(True, "Employee cannot be inserted. Reason: " + e.Exception.Message)
    '        Else
    '            'DisplayMessage(False, "Employee inserted")
    '        End If
    '    Catch ex As Exception
    '        connData.chiudi(False)
    '        Session.Add("ERRORE", "Provenienza: Fornitori - GestOperatori - ItemInserted - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    'Protected Sub RadGridOperatori_ItemUpdated(sender As Object, e As Telerik.Web.UI.GridUpdatedEventArgs) Handles RadGridOperatori.ItemUpdated
    '    Try
    '        If Not e.Exception Is Nothing Then
    '            e.KeepInEditMode = True
    '            e.ExceptionHandled = True
    '            'DisplayMessage(True, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " cannot be updated. Reason: " + e.Exception.Message)
    '        Else
    '            'DisplayMessage(False, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " updated")
    '        End If
    '    Catch ex As Exception
    '        connData.chiudi(False)
    '        Session.Add("ERRORE", "Provenienza: Fornitori - GestOperatori - ItemUpdated - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    Protected Sub RadGridOperatori_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridOperatori.NeedDataSource
        Try
            If sStrSql <> "" Then
                RadGridOperatori.ClientSettings.AllowKeyboardNavigation = True
                RadGridOperatori.DataSource = par.getDataTableGrid(sStrSql)
                Dim dt As System.Data.DataTable = RadGridOperatori.DataSource
                NumeroElementi = dt.Rows.Count
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - GestOperatori - NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridOperatori_PreRender(sender As Object, e As System.EventArgs) Handles RadGridOperatori.PreRender
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Protected Sub RadGridOperatori_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridOperatori.UpdateCommand
        Try
            Dim VALIDO As Boolean = True
            Me.connData = New CM.datiConnessione(par, False, False)
            Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)
            connData.apri(True)

            Dim Operatore As String = ""
            Operatore = par.PulisciStrSql(CType(userControl.FindControl("txtOperatore"), RadTextBox).Text)

            Dim Cognome As String = ""
            Cognome = par.PulisciStrSql(CType(userControl.FindControl("txtCognome"), RadTextBox).Text)

            Dim Nome As String = ""
            Nome = par.PulisciStrSql(CType(userControl.FindControl("txtNome"), RadTextBox).Text)

            Dim CodFiscale As String = ""
            CodFiscale = UCase(CType(userControl.FindControl("txtCodFiscale"), RadTextBox).Text)
            If CodFiscale <> "" Then
                If par.ControllaCF(CodFiscale) = True Then
                    If par.ControllaCFNomeCognome(CodFiscale, Cognome, Nome) = False Then
                        VALIDO = False
                        VisualizzaAlert("Inserire un codice fiscale valido", 2)
                    End If
                Else
                    VALIDO = False
                    VisualizzaAlert("Inserire un codice fiscale valido", 2)
                End If
            End If

            Dim DataPW As String = ""
            DataPW = CType(userControl.FindControl("txtDataPW"), RadDatePicker).SelectedDate.ToString

            Dim Fornitore As String = ""
            If CType(userControl.FindControl("cmbFornitore"), RadComboBox).SelectedValue <> "-1" Then
                Fornitore = par.insDbValue(CType(userControl.FindControl("cmbFornitore"), RadComboBox).SelectedValue, True, False)
            Else
                Fornitore = "NULL"
            End If
            If Fornitore = "''" Or Fornitore = "NULL" Then
                VALIDO = False
                VisualizzaAlert("Scegliere un fornitore valido", 2)
            End If

            par.cmd.CommandText = "select * from OPERATORI where fl_eliminato='0' and id<>" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString() & " and UPPER(OPERATORE)='" & Operatore.ToUpper & "'"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.HasRows = True Then
                VALIDO = False
                VisualizzaAlert("Il campo operatore è già utilizzato", 2)
            End If
            myReader1.Close()

            If VALIDO = True Then
                Dim DATA_DA_INSERIRE As String = ""
                If DataPW = "" Then
                    DATA_DA_INSERIRE = ""
                Else
                    DATA_DA_INSERIRE = Format(CDate(CType(userControl.FindControl("txtDataPW"), RadDatePicker).SelectedDate), "yyyyMMdd")
                End If
                par.cmd.CommandText = "UPDATE OPERATORI SET MOD_FO_ID_FO=" & Fornitore & ",OPERATORE='" & Operatore & "',cognome='" & Cognome & "',nome='" & Nome & "',cod_fiscale='" & par.PulisciStrSql(CodFiscale) & "',DATA_PW='" & DATA_DA_INSERIRE & "' WHERE  ID = " & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.OPERATORI_FO_APPALTI WHERE ID_FORNITORE = " & HiddenIdFornitore.Value & " AND ID_OPERATORE = " & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
                par.cmd.ExecuteNonQuery()
                Dim cmbAppalto As RadComboBox = CType(userControl.FindControl("cmbAppalto"), RadComboBox)

                If cmbAppalto.SelectedValue <> "-1" Then
                    'Gestione appalti fornitore

                    For Each item As RadComboBoxItem In cmbAppalto.Items
                        If item.Checked = True Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.OPERATORI_FO_APPALTI(ID_FORNITORE,ID_GRUPPO, ID_OPERATORE) VALUES" _
                                                & "(" & HiddenIdFornitore.Value & "," & item.Value & "," & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString() & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    Next
                End If

                If UCase(Operatore) <> UCase(vOperatore.Value) Then
                    If ScriviLogOp(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString(), "OPERATORE", UCase(vOperatore.Value), UCase(Operatore), 6, Format(Now, "yyyyMMddHHmmss")) = False Then

                    End If
                End If

                If UCase(Cognome) <> UCase(vCognome.Value) Then
                    If ScriviLogOp(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString(), "COGNOME", UCase(vCognome.Value), UCase(Cognome), 6, Format(Now, "yyyyMMddHHmmss")) = False Then

                    End If
                End If

                If UCase(Nome) <> UCase(vNome.Value) Then
                    If ScriviLogOp(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString(), "NOME", UCase(vNome.Value), UCase(Nome), 6, Format(Now, "yyyyMMddHHmmss")) = False Then

                    End If
                End If

                If UCase(CodFiscale) <> UCase(vCF.Value) Then
                    If ScriviLogOp(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString(), "CODICE FISCALE", UCase(vCF.Value), UCase(CodFiscale), 6, Format(Now, "yyyyMMddHHmmss")) = False Then

                    End If
                End If

                If UCase(Fornitore) <> UCase(vFornitore.Value) Then
                    If ScriviLogOp(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString(), "DITTA", UCase(vFornitore.Value), UCase(Fornitore), 6, Format(Now, "yyyyMMddHHmmss")) = False Then

                    End If
                End If
                If DataPW <> "" Then
                    If Format(CDate(CType(userControl.FindControl("txtDataPW"), RadDatePicker).SelectedDate), "yyyyMMdd") <> UCase(vDataPW.Value) Then
                        If UCase(vDataPW.Value) <> "" Then
                            If ScriviLogOp(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString(), "DATA SCADENZA PW", UCase(Format(CDate(vDataPW.Value), "dd/MM/yyyy")), UCase(Format(CDate(DataPW), "dd/MM/yyyy")), 6, Format(Now, "yyyyMMddHHmmss")) = False Then

                            End If
                        Else
                            If ScriviLogOp(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString(), "DATA SCADENZA PW", "", UCase(Format(CDate(DataPW), "dd/MM/yyyy")), 6, Format(Now, "yyyyMMddHHmmss")) = False Then

                            End If
                        End If
                    End If
                Else
                    If DataPW <> UCase(vDataPW.Value) Then
                        If UCase(vDataPW.Value) <> "" Then
                            If ScriviLogOp(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString(), "DATA SCADENZA PW", UCase(Format(CDate(vDataPW.Value), "dd/MM/yyyy")), "", 6, Format(Now, "yyyyMMddHHmmss")) = False Then

                            End If
                        Else
                            If ScriviLogOp(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString(), "DATA SCADENZA PW", "", "", 6, Format(Now, "yyyyMMddHHmmss")) = False Then

                            End If
                        End If
                    End If
                End If
                connData.chiudi(True)
                VisualizzaAlert("Operazione effettuata", 1)
                RadGridOperatori.Rebind()
                RadGridOperatori.EditIndexes.Clear()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Naviga", "validNavigation = true;", True)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Operatori - UpdateCommand - " & ex.Message)
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

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Naviga", "validNavigation = true;", True)
    End Sub

    Protected Sub btnEsporta_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        isExporting.Value = "1"
        Dim context As RadProgressContext = RadProgressContext.Current
        context.SecondaryTotal = 0
        context.SecondaryValue = 0
        context.SecondaryPercent = 0
        RadGridOperatori.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx
        RadGridOperatori.ExportSettings.IgnorePaging = True
        RadGridOperatori.ExportSettings.ExportOnlyData = True
        RadGridOperatori.ExportSettings.OpenInNewWindow = True
        RadGridOperatori.MasterTableView.ExportToExcel()

    End Sub



    Protected Sub CaricaAppalti(ByVal o As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            Dim cmbFornitore As RadComboBox = TryCast((TryCast(o, RadComboBox)).Parent.FindControl("cmbFornitore"), RadComboBox)
            Dim cmbAppalto As RadComboBox = TryCast((TryCast(o, RadComboBox)).Parent.FindControl("cmbAppalto"), RadComboBox)
            par.caricaComboTelerik("select DISTINCT ID_GRUPPO,NUM_REPERTORIO from SISCOM_MI.APPALTI WHERE ID_FORNITORE = " & cmbFornitore.SelectedValue & " order by 2 DESC", cmbAppalto, "ID_GRUPPO", "NUM_REPERTORIO", False)
            cmbAppalto.Enabled = True
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Operatori - CaricaAppalti - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub Page_PreRenderComplete(sender As Object, e As System.EventArgs) Handles Me.PreRenderComplete
        If isExporting.Value = "1" Then
            Dim context As RadProgressContext = RadProgressContext.Current
            context.CurrentOperationText = "Export in corso..."
            context("ProgressDone") = True
            context.OperationComplete = True
            context.SecondaryTotal = 0
            context.SecondaryValue = 0
            context.SecondaryPercent = 0
            isExporting.Value = "0"
        End If
    End Sub
End Class
