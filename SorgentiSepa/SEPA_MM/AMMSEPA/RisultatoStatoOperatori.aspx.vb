Imports System.IO

Partial Class AMMSEPA_RisultatoStatoOperatori
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property dataTableStatoOP() As Data.DataTable
        Get
            If Not (ViewState("dataTableStatoOP") Is Nothing) Then
                Return ViewState("dataTableStatoOP")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dataTableStatoOP") = value
        End Set
    End Property
    Private Property Ordinamento() As String
        Get
            If Not (ViewState("par_Ordinamento") Is Nothing) Then
                Return CStr(ViewState("par_Ordinamento"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Ordinamento") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        If Not IsPostBack Then
            Response.Flush()
            Ordinamento = "OPERATORI.operatore"
            Carica()
        End If
    End Sub
    Private Sub Carica()
        Try
            Dim ordine2 As String = " ORDER BY " & Ordinamento & " ASC"
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim ente = Request.QueryString("ENTE")
            Dim filiale = Request.QueryString("FILIALE")
            If ente = -1 Then
                par.cmd.CommandText = "SELECT operatori.id,upper(caf_web.descrizione) as ente,tab_filiali.nome AS st_aler,(select DESCRIZIONE||' '||CIVICO||' '||LOCALITA from SISCOM_MI.indirizzi WHERE ID=TAB_FILIALI.ID_INDIRIZZO) AS INDIRIZZO_FILIALE,operatori.operatore,operatori.cognome, operatori.nome, operatori.cod_fiscale, operatori.revoca, DECODE(operatori.revoca, 0, 'NO', 1, 'SI', 2, 'SI') AS revocato, operatori.motivo_revoca, operatori.cod_ana " _
                                    & " FROM SISCOM_MI.TAB_FILIALI,CAF_WEB,SEPA.OPERATORI WHERE OPERATORI.ID_UFFICIO=TAB_FILIALI.ID (+) AND OPERATORI.ID_CAF=CAF_WEB.ID AND SEPA_WEB=1 AND FL_ELIMINATO='0' AND OPERATORE<>'*' "
            ElseIf ente = 2 And filiale = -1 Then
                par.cmd.CommandText = "SELECT operatori.id,upper(caf_web.descrizione) as ente,tab_filiali.nome AS st_aler,(select DESCRIZIONE||' '||CIVICO||' '||LOCALITA from SISCOM_MI.indirizzi WHERE ID=TAB_FILIALI.ID_INDIRIZZO) AS INDIRIZZO_FILIALE,operatori.operatore,operatori.cognome, operatori.nome, operatori.cod_fiscale, operatori.revoca, DECODE(operatori.revoca, 0, 'NO', 1, 'SI', 2, 'SI') AS revocato, operatori.motivo_revoca, operatori.cod_ana " _
                                    & " FROM SISCOM_MI.TAB_FILIALI,CAF_WEB,SEPA.OPERATORI WHERE OPERATORI.ID_UFFICIO=TAB_FILIALI.ID (+) AND OPERATORI.ID_CAF=CAF_WEB.ID AND SEPA_WEB=1 AND FL_ELIMINATO='0' AND OPERATORE<>'*' AND ID_CAF = '2'"
            ElseIf ente = 2 And filiale <> -1 Then
                par.cmd.CommandText = "SELECT operatori.id,upper(caf_web.descrizione) as ente,tab_filiali.nome AS st_aler,(select DESCRIZIONE||' '||CIVICO||' '||LOCALITA from SISCOM_MI.indirizzi WHERE ID=TAB_FILIALI.ID_INDIRIZZO) AS INDIRIZZO_FILIALE,operatori.operatore,operatori.cognome, operatori.nome, operatori.cod_fiscale, operatori.revoca, DECODE(operatori.revoca, 0, 'NO', 1, 'SI', 2, 'SI') AS revocato, operatori.motivo_revoca, operatori.cod_ana " _
                                    & " FROM SISCOM_MI.TAB_FILIALI,CAF_WEB,SEPA.OPERATORI WHERE OPERATORI.ID_UFFICIO=TAB_FILIALI.ID (+) AND OPERATORI.ID_CAF=CAF_WEB.ID AND SEPA_WEB=1 AND FL_ELIMINATO='0' AND OPERATORE<>'*' AND ID_CAF = '2' AND ID_UFFICIO ='" & filiale & "'"
            Else
                par.cmd.CommandText = "SELECT operatori.id,upper(caf_web.descrizione) as ente,tab_filiali.nome AS st_aler,(select DESCRIZIONE||' '||CIVICO||' '||LOCALITA from SISCOM_MI.indirizzi WHERE ID=TAB_FILIALI.ID_INDIRIZZO) AS INDIRIZZO_FILIALE,operatori.operatore,operatori.cognome, operatori.nome, operatori.cod_fiscale, operatori.revoca, DECODE(operatori.revoca, 0, 'NO', 1, 'SI', 2, 'SI') AS revocato, operatori.motivo_revoca, operatori.cod_ana " _
                                    & " FROM SISCOM_MI.TAB_FILIALI,CAF_WEB,SEPA.OPERATORI WHERE OPERATORI.ID_UFFICIO=TAB_FILIALI.ID (+) AND OPERATORI.ID_CAF=CAF_WEB.ID AND SEPA_WEB=1 AND FL_ELIMINATO='0' AND OPERATORE<>'*' AND ID_CAF = '" & ente & "'"
            End If
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText & ordine2, par.OracleConn)
            dataTableStatoOP = New Data.DataTable
            da.Fill(dataTableStatoOP)
            da.Dispose()
            CreaDT()
            If dataTableStatoOP.Rows.Count > 0 Then
                Label10.Text = "Totale Operatori: " & dataTableStatoOP.Rows.Count
                Dim idoperatore As String = ""
                'Dim Stringa As String = par.cmd.CommandText & " and operatori.id="
                For Each riga As Data.DataRow In dataTableStatoOP.Rows
                    idoperatore = riga.Item(0)
                    If idoperatore > 0 Then
                        par.cmd.CommandText = "SELECT * FROM OPERATORI WHERE ID = " & idoperatore
                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader.Read Then
                            'DICHIARAZIONE STRINGE DI COMPOSIZIONE LABEL
                            Dim FunzioniSepaClient As String = ""
                            Dim FunzioniOperatoreWeb As String = ""
                            Dim FunzioniERP As String = ""
                            Dim FunzioniCambi As String = ""
                            Dim FunzioniFSA As String = ""
                            Dim FunzioniAnagrafeUtenza As String = ""
                            Dim FunzioniAbbinamento As String = ""
                            Dim FunzioniGestioneLocatari As String = ""
                            Dim FunzioniCambiEmergenza As String = ""
                            Dim FunzioniPED As String = ""
                            Dim FunzioniConsultazione As String = ""
                            Dim FunzioniManutenzioni As String = ""
                            Dim FunzioniAnagrafePatrimonio As String = ""
                            Dim FunzioniContabilita As String = ""
                            Dim FunzioniCicloPassivo As String = ""
                            Dim FunzioniCallCenter As String = ""
                            Dim FunzioniCondominio As String = ""
                            Dim FunzioniImpianti As String = ""
                            Dim FunzioniContratti As String = ""
                            Dim FunzioniGestioneAutonoma As String = ""
                            Dim FunzioniGestioneMorosita As String = ""
                            Dim FunzioniCustomer As String = ""
                            Dim FunzioniStampeMassive As String = ""
                            Dim FunzioniPresloggio As String = ""
                            Dim FunzioniGestOPeratori As String = ""
                            Dim FunzioniInterrogazioneAnagrafe As String = ""
                            'INIZIO STRUTTURALIZZAZIONE STRINGHE
                            '******************GESTIONE OPERATORI VSA
                            If par.IfNull(myReader("OP_RESP_VSA"), 0) = 1 Then
                                FunzioniGestioneLocatari = FunzioniGestioneLocatari & "Parere Decisionale<br />"
                            End If
                            If par.IfNull(myReader("OP_NORM_VSA"), 0) = 1 Then
                                FunzioniGestioneLocatari = FunzioniGestioneLocatari & "Operatore<br />"
                            End If
                            '############## ASSESTAMENTO#########################
                            Dim ChASSNuovo As String = ""
                            If par.IfNull(myReader("ASS_FORMALIZZAZIONE"), 0) = 1 Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Nuovo Assestamento<br />"
                            End If
                            If par.IfNull(myReader("ASS_COMPILAZIONE"), 0) = 1 Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Compila Assestamento<br />"
                            End If
                            Dim ChASSConvAler As String = ""
                            If par.IfNull(myReader("ASS_CONV_ALER"), 0) = 1 Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Convalida Assest. Gestore<br />"
                            End If
                            Dim ChASSConvComune As String = ""
                            If par.IfNull(myReader("ASS_CONV_COMUNE"), 0) = 1 Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Convalida Assest. COMUNE<br />"
                            End If
                            '############### FINE ASSESTAMENTO ####################
                            If par.IfNull(myReader("MOD_AMM_RPT_P_EXTRA"), 0) = 1 Then
                                FunzioniContratti = FunzioniContratti & "Supervisore Report Pag. EXTRA MAV<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU_CREAGRUPPI"), "0") = "1" Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "Crea Gruppo di Lavoro<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU_SIMULA_APPLICA"), "0") = "1" Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "Simula/Applica AU Gruppo di Lavoro<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU_CONV_VIS_TUTTO"), "0") = "1" Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "Convocazione AU - Ricerca Tutti<br />"
                            End If
                            If par.IfNull(myReader("MOD_MAND_PAGAMENTO"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "Mandati di Pagamento<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONT_RATEIZZA"), 0) = 1 Then
                                FunzioniContratti = FunzioniContratti & "Rateizzazione Bollette<br />"
                            End If
                            'ChMOR_ANN
                            If par.IfNull(myReader("MOD_MOROSITA_ANN"), "0") = "1" Then
                                FunzioniGestioneMorosita = FunzioniGestioneMorosita & "Annullo Morosità<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU_DIFF_MP"), "0") = "1" Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "Diffida per mancata presentazione <br />"
                            End If
                            'RSS
                            If par.IfNull(myReader("MOD_BP_RSS"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "RRS<br />"
                            End If
                            If par.IfNull(myReader("MOD_BP_RSS_SL"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "RRS (solo lettura)<br />"
                            End If
                            'GESTIONE AUTONOMA
                            If par.IfNull(myReader("mod_AUTOGESTIONI"), "0") = "1" Then
                                FunzioniGestioneAutonoma = FunzioniGestioneAutonoma & "GESTIONE AUTONOMA<br />"
                            End If
                            If par.IfNull(myReader("mod_AUTOGESTIONI_SL"), "0") = "1" Then
                                FunzioniGestioneAutonoma = FunzioniGestioneAutonoma & "GESTIONE AUTONOMA (Solo Lettura)<br />"
                            End If
                            'SEZIONE MOROSITA 23/03/2011
                            If par.IfNull(myReader("MOD_MOROSITA"), "0") = "1" Then
                                FunzioniGestioneMorosita = FunzioniGestioneMorosita & "GESTIONE MOROSITA'<br />"
                            End If
                            If par.IfNull(myReader("MOD_MOROSITA_SL"), "0") = "1" Then
                                FunzioniGestioneMorosita = FunzioniGestioneMorosita & "GESTIONE MOROSITA (Solo Lettura)<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONT_RINN_USD"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "Rinnovo USD<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONT_CAMBIO_BOX"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "Cambio Intestazione BOX<br />"
                            End If
                            'CICLO PASSIVO
                            If par.IfNull(myReader("mod_CICLO_P"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "CICLO PASSIVO<br />"
                            End If
                            If par.IfNull(myReader("BP_NUOVO_PIANO"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Nuovo Piano Finanziario <br />"
                            End If
                            If par.IfNull(myReader("BP_GENERALE"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Generale<br />"
                            End If
                            If par.IfNull(myReader("BP_FORMALIZZAZIONE"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Formalizzazione<br />"
                            End If
                            If par.IfNull(myReader("BP_COMPILAZIONE"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Compilazione<br />"
                            End If
                            If par.IfNull(myReader("BP_CONV_ALER"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Convalida Gestore<br />"
                            End If
                            If par.IfNull(myReader("BP_CONV_COMUNE"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Convalida Comune<br />"
                            End If
                            If par.IfNull(myReader("BP_CAPITOLI"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Assegn. Capitoli<br />"
                            End If
                            If par.IfNull(myReader("BP_FORMALIZZAZIONE_L"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Formalizzazione (solo lettura)<br />"
                            End If
                            If par.IfNull(myReader("BP_COMPILAZIONE_L"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Compilazione (solo lettura)<br />"
                            End If
                            If par.IfNull(myReader("BP_CONV_ALER_L"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Convalida Gestore (solo lettura)<br />"
                            End If
                            If par.IfNull(myReader("BP_CONV_COMUNE_L"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Convalida Comune (solo lettura)<br />"
                            End If
                            If par.IfNull(myReader("BP_CAPITOLI_L"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Assegn. Capitoli (solo lettura)<br />"
                            End If
                            If par.IfNull(myReader("BP_VOCI_SERVIZI"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Gestione Voci Servizi<br />"
                            End If
                            If par.IfNull(myReader("BP_VOCI_SERVIZI_L"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Voci Servizi (solo lettura)<br />"
                            End If
                            If par.IfNull(myReader("BP_MS"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "Manutenzioni e Servizi<br />"
                            End If
                            If par.IfNull(myReader("BP_MS_L"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "Manutenzioni e Servizi (solo lettura)<br />"
                            End If
                            If par.IfNull(myReader("BP_OP"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "Ordini e Pagamenti<br />"
                            End If
                            If par.IfNull(myReader("BP_OP_L"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "Ordini e Pagamenti (solo lettura)<br />"
                            End If
                            If par.IfNull(myReader("BP_PC"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "Pagamenti a Canone<br />"
                            End If
                            If par.IfNull(myReader("BP_PC_L"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "Pagamenti a Canone (solo lettura)<br />"
                            End If
                            If par.IfNull(myReader("BP_LO"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "Lotti<br />"
                            End If
                            If par.IfNull(myReader("BP_LO_L"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "Lotti (solo lettura)<br />"
                            End If
                            If par.IfNull(myReader("BP_CC"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "Contratti<br />"
                            End If
                            If par.IfNull(myReader("BP_CC_L"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "Contratti (solo lettura)<br />"
                            End If
                            If par.IfNull(myReader("BP_CC_V"), "0") = "1" Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "Contratti Variaz. Config.Patrimoniale<br />"
                            End If
                            'CALL CENTER
                            If par.IfNull(myReader("mod_call_center"), "0") = "1" Then
                                FunzioniCallCenter = FunzioniCallCenter & "CALL CENTER<br />"
                            End If
                            If par.IfNull(myReader("mod_call_center_sl"), "0") = "1" Then
                                FunzioniCallCenter = FunzioniCallCenter & "CALL CENTER (Solo Lettura)<br />"
                            End If
                            If par.IfNull(myReader("mod_call_center_GEST"), "0") = "1" Then
                                FunzioniCallCenter = FunzioniCallCenter & "Gestione Tabelle Supporto<br />"
                            End If
                            If par.IfNull(myReader("sepa"), "0") = "1" Then
                                FunzioniSepaClient = FunzioniSepaClient & "Operatore SEPA Client<br />"
                            End If
                            If par.IfNull(myReader("ass_esterna"), "0") = "1" Then
                                FunzioniSepaClient = FunzioniSepaClient & "Invio in Ass.Esterna<br />"
                            End If
                            If par.IfNull(myReader("alloggi"), "0") = "1" Then
                                FunzioniSepaClient = FunzioniSepaClient & "Query Alloggi<br />"
                            End If
                            If par.IfNull(myReader("AUTOCOMPILAZIONE"), "0") = "1" Then
                                FunzioniSepaClient = FunzioniSepaClient & "Auto Compilazione<br />"
                            End If
                            If par.IfNull(myReader("sepa_web"), "0") = "1" Then
                                FunzioniSepaClient = FunzioniSepaClient & "Operatore Web<br />"
                            End If
                            'Customer Satisfaction
                            If par.IfNull(myReader("MOD_SATISFACTION"), 0) = 1 Then
                                FunzioniCustomer = FunzioniCustomer & "CUSTOMER SATISFACTION<br />"
                            End If
                            If par.IfNull(myReader("MOD_SATISFACTION_SL"), 0) = 1 Then
                                FunzioniCustomer = FunzioniCustomer & "CUSTOMER SATISFACTION (Solo Lettura)<br />"
                            End If
                            'chPROVV
                            If par.IfNull(myReader("MOD_ERP"), "0") = "1" Then
                                FunzioniERP = FunzioniERP & "ERP<br />"
                            End If
                            If par.IfNull(myReader("MOD_CAMBI"), "0") = "1" Then
                                FunzioniCambi = FunzioniCambi & "CAMBI<br />"
                            End If
                            If par.IfNull(myReader("MOD_FSA"), "0") = "1" Then
                                FunzioniFSA = FunzioniFSA & "FSA<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU_CONS"), "0") = "1" Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "ANAGRAFE UTENZA (Solo Lettura)<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU"), "0") = "1" Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "ANAGRAFE UTENZA<br />"
                            End If
                            If par.IfNull(myReader("MOD_ABB"), "0") = "1" Then
                                FunzioniAbbinamento = FunzioniAbbinamento & "ABBINAMENTO<br />"
                            End If
                            If par.IfNull(myReader("MOD_ABB_DEC"), "0") = "1" Then
                                FunzioniGestioneLocatari = FunzioniGestioneLocatari & "GESTIONE LOCATARI<br />"
                            End If
                            If par.IfNull(myReader("MOD_PED"), "0") = "1" Then
                                FunzioniPED = FunzioniPED & "PED<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONS"), "0") = "1" Then
                                FunzioniConsultazione = FunzioniConsultazione & "CONSULTAZIONE<br />"
                            End If
                            If par.IfNull(myReader("pg"), "0") = "1" Then
                                FunzioniSepaClient = FunzioniSepaClient & "Pg@Web<br />"
                            End If
                            If par.IfNull(myReader("mod_demanio"), "0") = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "IMPIANTI<br />"
                            End If
                            If par.IfNull(myReader("mod_manutenzioni"), "0") = "1" Then
                                FunzioniManutenzioni = FunzioniManutenzioni & "MANUTENZIONI<br />"
                            End If
                            If par.IfNull(myReader("mod_contratti"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "CONTRATTI E BOLLETTE<br />"
                            End If
                            If par.IfNull(myReader("mod_contratti_boll"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "Emissione Bollettazione Massiva<br />"
                            End If
                            If par.IfNull(myReader("mod_contratti_testo"), "0") = "1" Then
                                FunzioniContratti = FunzioniCondominio & "Testo Contratti<br />"
                            End If
                            If par.IfNull(myReader("mod_contratti_PARAM"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "Parametri<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONTRATTI_MOR"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "Morosità<br />"
                            End If
                            If par.IfNull(myReader("mod_PED2"), "0") = "1" Then
                                FunzioniAnagrafePatrimonio = FunzioniAnagrafePatrimonio & "ANAGRAFE PATRIMONIO<br />"
                            End If
                            If par.IfNull(myReader("mod_contabile"), "0") = "1" Then
                                FunzioniContabilita = FunzioniContabilita & "CONTABILITA'<br />"
                            End If
                            If par.IfNull(myReader("mod_PED2_ESTERNA"), "0") = "1" Then
                                FunzioniContabilita = FunzioniContabilita & "Solo IV e V Lotto<br />"
                            End If
                            If par.IfNull(myReader("mod_PED2_SOLO_LETTURA"), "0") = "1" Then
                                FunzioniAnagrafePatrimonio = FunzioniAnagrafePatrimonio & "ANAGRAFE PATRIMONIO (Solo Lettura)<br />"
                            End If
                            If par.IfNull(myReader("FL_RESPONSABILE_ENTE"), "0") = "1" Then
                                FunzioniOperatoreWeb = FunzioniOperatoreWeb & "Responsabile<br />"
                            End If
                            If par.IfNull(myReader("FL_ABB_ERP"), "0") = "1" Then
                                FunzioniAbbinamento = FunzioniAbbinamento & "ERP<br />"
                            End If
                            If par.IfNull(myReader("FL_ABB_392"), "0") = "1" Then
                                FunzioniAbbinamento = FunzioniAbbinamento & "392<br />"
                            End If
                            If par.IfNull(myReader("FL_ABB_431"), "0") = "1" Then
                                FunzioniAbbinamento = FunzioniAbbinamento & "431<br />"
                            End If
                            If par.IfNull(myReader("FL_ABB_UD"), "0") = "1" Then
                                FunzioniAbbinamento = FunzioniAbbinamento & "UD<br />"
                            End If
                            If par.IfNull(myReader("FL_ABB_OA"), "0") = "1" Then
                                FunzioniAbbinamento = FunzioniAbbinamento & "O.A.<br />"
                            End If
                            If par.IfNull(myReader("MOD_STAMPE_MASSIVE"), 0) = 1 Then
                                FunzioniStampeMassive = FunzioniStampeMassive & "STAMPE MASSIVE<br />"
                            End If
                            If par.IfNull(myReader("FL_ABB_FO"), "0") = "1" Then
                                FunzioniAbbinamento = FunzioniAbbinamento & "F.O.<br />"
                            End If
                            If par.IfNull(myReader("FL_ABB_CS"), "0") = "1" Then
                                FunzioniAbbinamento = FunzioniAbbinamento & "C.S.<br />"
                            End If
                            If par.IfNull(myReader("FL_ABB_CONV"), "0") = "1" Then
                                FunzioniAbbinamento = FunzioniAbbinamento & "Can. Conv.<br />"
                            End If
                            'chPROVV
                            If par.IfNull(myReader("FL_ASS_PROVV"), "0") = "1" Then
                                FunzioniAbbinamento = FunzioniAbbinamento & "Provv.Assegnazione<br />"
                            End If
                            If par.IfNull(myReader("mod_contratti_l"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "CONTRATTI E BOLLETTE (Solo Lettura)<br />"
                            End If
                            If par.IfNull(myReader("mod_contratti_d"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "Op.Filiale - Dis./Recup. UI<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONTRATTI_INS"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "Ins. Contratti<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONTRATTI_INS_V"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "Ins. Contratti VIRTUALI<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONTRATTI_ISTAT"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "Calcolo Agg. ISTAT<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONTRATTI_INT"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "Calcolo Interessi Leg.<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONTRATTI_REG"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "Registrazione Cont.<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONTRATTI_IMP"), "0") = "1" Then
                                FunzioniContratti = FunzioniContratti & "Calcolo Imposte<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU_PROP_DEC"), "0") = "1" Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "Proposta Decadenza<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU_DOC_NEC"), "0") = "1" Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "Documentazione Necessaria<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU_DECIDI_DEC"), "0") = "1" Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "Decisione Decadenza<br />"
                            End If
                            If par.IfNull(myReader("MOD_EMRI"), "0") = "1" Then
                                FunzioniCambiEmergenza = FunzioniCambiEmergenza & "CAMBI EMERGENZA<br />"
                            End If
                            If par.IfNull(myReader("mod_CONDOMINIO"), "0") = "1" Then
                                FunzioniCondominio = FunzioniCondominio & "CONDOMINIO<br />"
                            End If
                            If par.IfNull(myReader("mod_CONDOMINIO_SL"), "0") = "1" Then
                                FunzioniCondominio = FunzioniCondominio & "CONDOMINIO (Solo Lettura)<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONT_ALLEGATI"), "0") = "1" Then
                                FunzioniContabilita = FunzioniContabilita & "Allega Documenti Accertato<br />"
                            End If
                            If par.IfNull(myReader("mod_cont_ragioneria"), "0") = "1" Then
                                FunzioniContabilita = FunzioniContabilita & "Ragioneria<br />"
                            End If
                            If par.IfNull(myReader("mod_cont_patrimoniali"), "0") = "1" Then
                                FunzioniContabilita = FunzioniContabilita & "Cons.Patrimoniali<br />"
                            End If
                            If par.IfNull(myReader("mod_cont_flussi"), "0") = "1" Then
                                FunzioniContabilita = FunzioniContabilita & "Flussi Finanz.<br />"
                            End If
                            If par.IfNull(myReader("mod_cont_RIMB"), "0") = "1" Then
                                FunzioniContabilita = FunzioniContabilita & "Rimborso Spese Gestore<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONT_PRELIEVI"), "0") = "1" Then
                                FunzioniContabilita = FunzioniContabilita & "Prelievi<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONT_COMPENSI"), "0") = "1" Then
                                FunzioniContabilita = FunzioniContabilita & "Compensi Gestore<br />"
                            End If
                            'ORDINE
                            'ELETTRICO - IDRICO - TERM.CENTRALIZZATO - TERM.AUTONOMO - TELERISCALD. - 
                            'SOLLEVAMENTO - METEORICHE - ANTINCENDIO - TUT. IMMOBILE - CANNA FUM. - 
                            'GAS -CITOFONICO - TV
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 1, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. ELETTRICO<br />"
                            End If
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 2, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. IDRICO<br />"
                            End If
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 3, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. CENTRALE TERM.<br />"
                            End If
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 4, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. TERM.AUTONOMO<br />"
                            End If
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 5, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. TELERISCALD.<br />"
                            End If
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 6, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. SOLLEVAMENTO<br />"
                            End If
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 7, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. ACQ. METEORICHE<br />"
                            End If
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 8, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. ANTINCENDIO<br />"
                            End If
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 9, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. TUTELA IMM.<br />"
                            End If
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 10, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. CANNA FUM.<br />"
                            End If
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 11, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. GAS<br />"
                            End If
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 12, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. CITOFONICO<br />"
                            End If
                            If Mid(par.IfNull(myReader("MOD_DEM_IMP"), "0000000000000"), 13, 1) = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "Imp. TV<br />"
                            End If
                            If par.IfNull(myReader("MOD_DEM_SL"), "0") = "1" Then
                                FunzioniImpianti = FunzioniImpianti & "IMPIANTI (Solo Lettura)<br />"
                            End If
                            If par.IfNull(myReader("MOD_CENS_MANUT"), 0) = 1 Then
                                FunzioniAnagrafePatrimonio = FunzioniAnagrafePatrimonio & "Censimento Stato Manutentivo<br />"
                            End If
                            If par.IfNull(myReader("CENS_MANUT_SL"), 0) = 1 Then
                                FunzioniAnagrafePatrimonio = FunzioniAnagrafePatrimonio & "Censimento Stato Manutentivo (Solo Lettura)<br />"
                            End If
                            If par.IfNull(myReader("MOD_SATISFACTION_SV"), 0) = 1 Then
                                FunzioniCustomer = FunzioniCustomer & "Supervisore<br />"
                            End If
                            If par.IfNull(myReader("MOD_CONT_P_EXTRA"), 0) = 1 Then
                                FunzioniContratti = FunzioniContratti & "Inserim. Pag. EXTRA MAV<br />"
                            End If
                            If par.IfNull(myReader("MOD_PAG_RUOLI"), 0) = 1 Then
                                FunzioniContratti = FunzioniContratti & "Inserim. Pag. RUOLI<br />"
                            End If
                            If par.IfNull(myReader("MOD_REPORT_RUOLI"), 0) = 1 Then
                                FunzioniContratti = FunzioniContratti & "Report Pag. RUOLI<br />"
                            End If
                            If par.IfNull(myReader("MOD_SBLOCCO_BOLL"), 0) = 1 Then
                                FunzioniContratti = FunzioniContratti & "Sblocco prossima bollettaz.<br />"
                            End If
                            If par.IfNull(myReader("BP_VARIAZIONI"), 0) = 1 Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Variazioni<br />"
                            End If
                            If par.IfNull(myReader("BP_VARIAZIONI_SL"), 0) = 1 Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Variazioni (solo lettura)<br />"
                            End If
                            '############
                            If par.IfNull(myReader("MOD_AU_CONV_REI"), 0) = 1 Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "Convocazione AU - Reimposta App.<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU_CONV_RIP"), 0) = 1 Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "Convocazione AU - Ripristina App.Annullato<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU_CONV_ANN"), 0) = 1 Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "Convocazione AU - Annulla Appuntamento<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU_CONV_SPOSTA"), 0) = 1 Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "Convocazione AU - Sposta Appuntamento<br />"
                            End If
                            If par.IfNull(myReader("MOD_AU_CONV_N"), 0) = 1 Then
                                FunzioniAnagrafeUtenza = FunzioniAnagrafeUtenza & "Convocazione AU - Inserimento Scheda AU<br />"
                            End If
                            If par.IfNull(myReader("MOD_BP_PREVENTIVI"), 0) = 1 Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Preventivi<br />"
                            End If
                            If par.IfNull(myReader("MOD_BP_RESIDUI"), 0) = 1 Then
                                FunzioniCicloPassivo = FunzioniCicloPassivo & "BP-Residui<br />"
                            End If
                            'If par.IfNull(myReader("MOD_PRE_SLOGGIO"), 0) = 1 Then
                            '    FunzioniPresloggio = FunzioniPresloggio & "PRE-SLOGGIO<br />"
                            'End If
                            If par.IfNull(myReader("anagrafe"), "0") = "1" Then
                                FunzioniInterrogazioneAnagrafe = "SI"
                            Else
                                FunzioniInterrogazioneAnagrafe = "NO"
                            End If
                            If par.IfNull(myReader("gest_operatori"), "0") = "1" Then
                                FunzioniGestOPeratori = "SI"
                            Else
                                FunzioniGestOPeratori = "NO"
                            End If

                            If par.IfNull(myReader("MOD_MASS_INGIUNZIONI"), "0") = "0" Then
                                FunzioniContratti = FunzioniContratti & "Inserim. Massivo Boll. Ingiunte<br />"
                            End If
                            If par.IfNull(myReader("MOD_SING_INGIUNZIONI"), "0") = "0" Then
                                FunzioniContratti = FunzioniContratti & "Inserim. Sing. Boll. Ingiunta<br />"
                            End If

                            If par.IfNull(myReader("MOD_PAG_INGIUNZ"), 0) = 1 Then
                                FunzioniContratti = FunzioniContratti & "Inserim. Pag. Ingiunz.<br />"
                            End If
                            If par.IfNull(myReader("MOD_REPORT_INGIUNZ"), 0) = 1 Then
                                FunzioniContratti = FunzioniContratti & "Report Pag. Ingiunz.<br />"
                            End If

                            riga.Item("INTERROGAZIONE_ANAGRAFE") = FunzioniInterrogazioneAnagrafe
                            riga.Item("GESTIONE_OPERATORI") = FunzioniGestOPeratori
                            riga.Item("SEPA_CLIENT") = FunzioniSepaClient
                            riga.Item("OPERATORE_WEB") = FunzioniOperatoreWeb
                            riga.Item("ERP") = FunzioniERP
                            riga.Item("CAMBI") = FunzioniCambi
                            riga.Item("FSA") = FunzioniFSA
                            riga.Item("ANAGRAFE_UTENZA") = FunzioniAnagrafeUtenza
                            riga.Item("ABBINAMENTO") = FunzioniAbbinamento
                            riga.Item("GESTIONE_LOCATARI") = FunzioniGestioneLocatari
                            riga.Item("CAMBI_EMERGENZA") = FunzioniCambiEmergenza
                            riga.Item("PED") = FunzioniPED
                            riga.Item("CONSULTAZIONE") = FunzioniConsultazione
                            riga.Item("MANUTENZIONE") = FunzioniManutenzioni
                            riga.Item("ANAGRAFE_PATRIMONIO") = FunzioniAnagrafePatrimonio
                            riga.Item("CONTABILITA") = FunzioniContabilita
                            riga.Item("CICLO_PASSIVO") = FunzioniCicloPassivo
                            riga.Item("CALL_CENTER") = FunzioniCallCenter
                            riga.Item("CONDOMINIO") = FunzioniCondominio
                            riga.Item("IMPIANTI") = FunzioniImpianti
                            riga.Item("CONTRATTI_BOLLETTE") = FunzioniContratti
                            riga.Item("GESTIONE_AUTONOMA") = FunzioniGestioneAutonoma
                            riga.Item("GESTIONE_MOROSITA") = FunzioniGestioneMorosita
                            riga.Item("CUSTOMER_SATISFACTION") = FunzioniCustomer
                            riga.Item("STAMPE_MASSIVE") = FunzioniPresloggio
                        End If
                        myReader.Close()
                    End If
                Next
                BindGrid()
            Else
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');document.location.href=""RicercaStatoOperatori.aspx""</SCRIPT>")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            dgvstatoOP.DataSource = dataTableStatoOP
            dgvstatoOP.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub rbente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbente.CheckedChanged
        Response.Flush()
        Ordinamento = "CAF_WEB.DESCRIZIONE"
        Carica()
    End Sub
    Protected Sub rboperatore_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboperatore.CheckedChanged
        Response.Flush()
        Ordinamento = "OPERATORI.operatore"
        Carica()
    End Sub
    Protected Sub rbindirizzo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbindirizzo.CheckedChanged
        Response.Flush()
        Ordinamento = "CAF_WEB.DESCRIZIONE ASC,ST_ALER ASC,INDIRIZZO_FILIALE"
        Carica()
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaStatoOperatori.aspx""</script>")
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        If dgvstatoOP.Items.Count > 0 Then
            CancellaBR(dataTableStatoOP)
            Dim nomefile As String = par.EsportaExcelDaDTWithDatagrid(dataTableStatoOP, dgvstatoOP, "ExportStatoOperatori", 100 / 100, , 55)
            If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
                Response.Redirect("../FileTemp/" & nomefile, False)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
            End If
        Else
            Response.Write("<script>alert('Nessun dato da esportare!')</script>")
        End If
    End Sub
    Protected Sub CreaDT()
        dataTableStatoOP.Columns.Add("INTERROGAZIONE_ANAGRAFE")
        dataTableStatoOP.Columns.Add("GESTIONE_OPERATORI")
        dataTableStatoOP.Columns.Add("SEPA_CLIENT")
        dataTableStatoOP.Columns.Add("OPERATORE_WEB")
        dataTableStatoOP.Columns.Add("ERP")
        dataTableStatoOP.Columns.Add("CAMBI")
        dataTableStatoOP.Columns.Add("FSA")
        dataTableStatoOP.Columns.Add("ANAGRAFE_UTENZA")
        dataTableStatoOP.Columns.Add("ABBINAMENTO")
        dataTableStatoOP.Columns.Add("GESTIONE_LOCATARI")
        dataTableStatoOP.Columns.Add("CAMBI_EMERGENZA")
        dataTableStatoOP.Columns.Add("PED")
        dataTableStatoOP.Columns.Add("CONSULTAZIONE")
        dataTableStatoOP.Columns.Add("MANUTENZIONE")
        dataTableStatoOP.Columns.Add("ANAGRAFE_PATRIMONIO")
        dataTableStatoOP.Columns.Add("CONTABILITA")
        dataTableStatoOP.Columns.Add("CICLO_PASSIVO")
        dataTableStatoOP.Columns.Add("CALL_CENTER")
        dataTableStatoOP.Columns.Add("CONDOMINIO")
        dataTableStatoOP.Columns.Add("IMPIANTI")
        dataTableStatoOP.Columns.Add("CONTRATTI_BOLLETTE")
        dataTableStatoOP.Columns.Add("GESTIONE_AUTONOMA")
        dataTableStatoOP.Columns.Add("GESTIONE_MOROSITA")
        dataTableStatoOP.Columns.Add("CUSTOMER_SATISFACTION")
        dataTableStatoOP.Columns.Add("STAMPE_MASSIVE")
    End Sub
    Private Sub CancellaBR(ByVal dt As Data.DataTable)
        Dim posizione As Integer = -1
        Try
            For Each riga As Data.DataRow In dt.Rows
                For Each colonna As Data.DataColumn In dt.Columns
                    If Not IsDBNull(riga.Item(colonna)) Then
                        posizione = riga.Item(colonna).ToString.IndexOf("<br />")
                        If posizione <> -1 Then
                            riga.Item(colonna) = Replace(riga.Item(colonna), "<br />", ", ")
                            riga.Item(colonna) = Left(riga.Item(colonna), Len(riga.Item(colonna)) - 2)
                        End If
                    End If
                Next
            Next
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
