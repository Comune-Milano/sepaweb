Imports System.IO

Partial Class Portale
    Inherits PageSetIdMode
    Public psNews As String
    Dim par As New CM.Global
    Dim nGiorno As String
    Dim nGiornoRif As String
    Dim GiorniAp As String



    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccedi.Click
        Dim oMioCaf As Object
        Dim oMioPw As Object
        Dim sUtente As String

        Dim data_info As String = ""


        Try
            If Session.Item("OPERATORE") <> "" Then
                Session.RemoveAll()
                Session.Abandon()
                Session.Clear()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                HttpContext.Current.Session.Abandon()
                Response.Redirect("Portale.aspx", True)
                Exit Sub
            End If
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            par.OracleConn.Open()





            Dim Inattivita As Integer = 0
            Dim GiorniScadenza As Integer = 0
            Dim DATAGENERICA As String = Format(Now, "yyyyMMdd")

            'par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=77"
            'Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderA.Read Then
            '    If par.DeCripta(par.IfNull(myReaderA("VALORE"), 0)) <> "20091101" Then
            '        par.OracleConn.Close()
            '        Session.RemoveAll()
            '        Session.Abandon()
            '        Session.Clear()
            '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '        HttpContext.Current.Session.Abandon()
            '        lblAvviso.Visible = True
            '        lblAvviso.Text = "ERRORE GENERICO"
            '        Exit Sub
            '    Else
            '        'DATAGENERICA = par.DeCripta(par.IfNull(myReaderA("VALORE"), 0))
            '    End If
            'End If
            'myReaderA.Close()

            'If DATAGENERICA > "20090831" Then
            '    par.cmd.CommandText = "update parameter set valore='0A03080109020808' where id=77"
            '    par.cmd.ExecuteNonQuery()
            '    par.OracleConn.Close()
            '    Session.RemoveAll()
            '    Session.Abandon()
            '    Session.Clear()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '    HttpContext.Current.Session.Abandon()
            '    lblAvviso.Visible = True
            '    lblAvviso.Text = "ERRORE GENERICO"
            '    Exit Sub
            'End If

            Dim env As String = "1"
            Dim isMMT As String = "0"

            If Not IsNothing(System.Configuration.ConfigurationManager.AppSettings("Env")) Then
                env = System.Configuration.ConfigurationManager.AppSettings("Env")
            End If
            If Not IsNothing(System.Configuration.ConfigurationManager.AppSettings("isMMT")) Then
                isMMT = System.Configuration.ConfigurationManager.AppSettings("isMMT")
            End If

            If env = "1" Then
                Session.Add("LicenzaHtmlToPdf", par.LicenzaHtmlToPdf)
                Session.Add("LicenzaPdfMerge", par.LicenzaPdfMerge)
                Session.Add("AmbienteDiTest", "0")
                par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=135"
                Dim myReadermav As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReadermav.Read Then
                    Session.Add("indirizzoMavOnLine", par.IfNull(myReadermav("VALORE"), ""))
                End If
                myReadermav.Close()
            Else
                Session.Add("LicenzaHtmlToPdf", "")
                Session.Add("LicenzaPdfMerge", "")
                Session.Add("AmbienteDiTest", "1")
                par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=134"
                Dim myReadermav As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReadermav.Read Then
                    Session.Add("indirizzoMavOnLine", par.IfNull(myReadermav("VALORE"), ""))
                End If
                myReadermav.Close()
            End If

            'SE AU LIGHT IMPOSTA VARIABILE DI SESSIONE, UTILE PER LE CHIAMATE A FUNZONI SUL SERVER DI MILANO
            'If InStr(UCase(indirizzo.Value), "SEPA_MM") > 0 Or InStr(UCase(indirizzo.Value), "LOCAL") > 0 Then
            '    Session.Add("ANAGRAFE_UTENZA_LIGHT", "1")
            'Else
            '    Session.Add("ANAGRAFE_UTENZA_LIGHT", "0")
            'End If
            Session.Add("ANAGRAFE_UTENZA_LIGHT", "0")

            'Dim paramPDF As Integer = 0
            'par.cmd.CommandText = "select valore from parametri where parametro = 'LICENZA PDF'"
            'paramPDF = par.IfNull(par.cmd.ExecuteScalar, 0)
            'If paramPDF > 0 Then
            '    Session.Add("LicenzaHtmlToPdf", par.LicenzaHtmlToPdf)
            '    Session.Add("LicenzaPdfMerge", par.LicenzaPdfMerge)
            '    Session.Add("AmbienteDiTest", "0")
            'Else
            '    Session.Add("LicenzaHtmlToPdf", "")
            '    Session.Add("LicenzaPdfMerge", "")
            '    Session.Add("AmbienteDiTest", "1")
            'End If


            par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=64"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                NTentativi = par.IfNull(myReader("VALORE"), 0)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=65"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Inattivita = par.IfNull(myReader("VALORE"), 0)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=61"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                GiorniScadenza = par.IfNull(myReader("VALORE"), 0)
            End If
            myReader.Close()




            sUtente = UCase(par.PulisciStrSql(txtUtente.Text))
            Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("SELECT OPERATORI.*,CAF_WEB.COD_CAF,caf_web.descrizione as ""DESCRIZIONE_CAF"",trim(nvl((select max(substr(data_ora_in,1,8)) from operatori_web_log where id_operatore = operatori.id),pw_data_inserimento)) as ultimo_accesso FROM OPERATORI,CAF_WEB WHERE CAF_WEB.ID=OPERATORI.ID_CAF AND OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" + sUtente + "' and OPERATORI.sepa_web='1'", par.OracleConn)
            myReader = cmd.ExecuteReader()
            If myReader.Read() Then
                txtID.Text = myReader("ID")
                Dim PwMatch As Boolean = par.VerifyHash(txtPw.Text, "SHA512", Trim(par.IfNull(myReader("pw"), ""))).ToString()
                If PwMatch = True And isMMT = 1 Then
                    par.cmd.CommandText = "select count(*) from operatori_sepammt where id = " & par.IfNull(myReader("id"), -1)
                    Dim cnt As Integer = par.cmd.ExecuteScalar
                    If cnt <= 0 Then
                        lblAvviso.Visible = True
                        lblAvviso.Text = "Utenza non abilitata!"
                        myReader.Close()
                        cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If
                End If
                If PwMatch = False Then
                    lblAvviso.Visible = True
                    lblAvviso.Text = "Utente e/o Password  non corretti."
                    If sNomeUtente <> txtUtente.Text Then
                        Tentativi = 0
                        sNomeUtente = txtUtente.Text
                    End If
                    Tentativi = Tentativi + 1
                    If NTentativi = Tentativi And txtUtente.Text <> "*" Then
                        lblAvviso.Text = "L'utenza è stata revocata!"
                        cmd.CommandText = "update OPERATORI set revoca=2,motivo_revoca='Limite Tentativi di accesso raggiunto' where  OPERATORE='" & Trim(txtUtente.Text) & "'"
                        cmd.ExecuteNonQuery()
                    End If
                    cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub

                End If
                If myReader("LIVELLO_WEB") <> 1 And (par.IfNull(myReader("data_pw"), Format(Now, "yyyyMMdd")) < Format(Now, "yyyyMMdd")) Then
                    lblAvviso.Visible = True
                    lblAvviso.Text = "Utenza non abilitata!"
                    myReader.Close()
                    cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                If myReader("LIVELLO_WEB") <> 1 And (par.IfNull(myReader("REVOCA"), "") = "1" Or par.IfNull(myReader("REVOCA"), "") = "2") Then
                    lblAvviso.Visible = True
                    lblAvviso.Text = "Utenza revocata, non è possibile accedere!"
                    If par.IfNull(myReader("APPOGGIO"), "") = "3" Then
                        lblAvviso.Text = "Utenza MOMENTANEAMENTE revocata !"
                    End If
                    myReader.Close()
                    cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                If myReader("LIVELLO_WEB") <> 1 And DateDiff("d", DateSerial(Mid(par.IfNull(myReader("ultimo_accesso"), "20070301"), 1, 4), Mid(par.IfNull(myReader("ultimo_accesso"), "20070301"), 5, 2), Mid(par.IfNull(myReader("ultimo_accesso"), "20070301"), 7, 2)), Now) > Inattivita Then
                    cmd.CommandText = "update operatori set revoca=2,motivo_revoca='Limite Temporale (" & Inattivita & " gg) raggiunto' where  id=" & myReader("id")
                    cmd.ExecuteNonQuery()
                    lblAvviso.Visible = True
                    lblAvviso.Text = "Utenza revocata!"
                    myReader.Close()
                    cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                data_info = par.IfNull(myReader("DATA_INFO_UTENTE"), Format(Now, "yyyyMMdd"))

                Dim LockSession As String = ""

                If txtID.Text <> 1 And (UCase(txtPw.Text) = "SEPA" Or DateDiff("d", DateSerial(Mid(par.IfNull(myReader("Pw_data_inserimento"), "19500101"), 1, 4), Mid(par.IfNull(myReader("Pw_data_inserimento"), "19500101"), 5, 2), Mid(par.IfNull(myReader("Pw_data_inserimento"), "19500101"), 7, 2)), Now) > GiorniScadenza) Then
                    lblAvviso.Visible = True
                    lblAvviso.Text = "La password deve essere modificata!"
                    Session.Add("ANAGRAFE", myReader("ANAGRAFE"))
                    Session.Add("ANAGRAFE_CONSULTAZIONE", myReader("MOD_AU_CONS"))
                    Session.Add("MOD_AU", myReader("MOD_AU"))

                    Session.Add("PARAMETRI_CONTRATTI", par.IfNull(myReader("MOD_CONTRATTI_PARAM"), "0"))
                    Session.Add("RESPONSABILE", myReader("FL_RESPONSABILE_ENTE"))
                    Session.Add("NOME_OPERATORE", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                    Session.Add("OPERATORE", UCase(txtUtente.Text))
                    Session.Add("ID_OPERATORE", txtID.Text)
                    oMioCaf = myReader("ID_CAF")
                    oMioPw = myReader("PW")
                    Session.Add("ID_CAF", oMioCaf)
                    Session.Add("PW", oMioPw)
                    oMioCaf = myReader("COD_CAF")
                    Session.Add("CAAF", oMioCaf)
                    Session.Add("DESCRIZIONE_CAF", par.IfNull(myReader("DESCRIZIONE_CAF"), ""))

                    Session.Add("GLista", txtID.Text & Format(Now, "HHmmss") & "lista")
                    Session.Add("GRiga", txtID.Text & Format(Now, "HHmmss") & "Riga")
                    Session.Add("GSpese", txtID.Text & Format(Now, "HHmmss") & "Spese")
                    Session.Add("GProgressivo", txtID.Text & Format(Now, "HHmmss") & "Progressivo")

                    Session.Add("PED2_ESTERNA", myReader("MOD_PED2_ESTERNA"))
                    Session.Add("PED2_SOLOLETTURA", myReader("MOD_PED2_SOLO_LETTURA"))
                    Session.Add("MOD_CONTABILE", myReader("MOD_CONTABILE"))

                    Session.Add("ABB_ERP", myReader("FL_ABB_ERP"))
                    Session.Add("ABB_392", myReader("FL_ABB_392"))
                    Session.Add("ABB_431", myReader("FL_ABB_431"))
                    Session.Add("ABB_UD", myReader("FL_ABB_UD"))
                    Session.Add("ABB_OA", myReader("FL_ABB_OA"))
                    Session.Add("ABB_FO", myReader("FL_ABB_FO"))
                    Session.Add("ABB_CS", myReader("FL_ABB_CS"))
                    Session.Add("ASS_PROVV", myReader("FL_ASS_PROVV"))
                    Session.Add("ABB_CONVONEZIONATO", myReader("FL_ABB_CONV"))
                    '

                    Session.Add("PARAMETRI_CONTRATTI_BOLL", par.IfNull(myReader("MOD_CONTRATTI_BOLL"), "0"))
                    Session.Add("PARAMETRI_CONTRATTI_TESTO", par.IfNull(myReader("MOD_CONTRATTI_TESTO"), "0"))

                    Session.Add("CONT_LETTURA", par.IfNull(myReader("MOD_CONTRATTI_L"), "0"))
                    Session.Add("CONT_DISDETTE", par.IfNull(myReader("MOD_CONTRATTI_D"), "0"))

                    Session.Add("MODULO_CONT", par.IfNull(myReader("MOD_CONTRATTI"), "0"))
                    Session.Add("CONT_INSERIMENTO", par.IfNull(myReader("MOD_CONTRATTI_INS"), "0"))
                    Session.Add("CONT_ISTAT", par.IfNull(myReader("MOD_CONTRATTI_ISTAT"), "0"))
                    Session.Add("CONT_INTERESSI", par.IfNull(myReader("MOD_CONTRATTI_INT"), "0"))
                    Session.Add("CONT_REGISTRAZIONE", par.IfNull(myReader("MOD_CONTRATTI_REG"), "0"))
                    Session.Add("CONT_IMPOSTE", par.IfNull(myReader("MOD_CONTRATTI_IMP"), "0"))
                    Session.Add("PROP_DEC", par.IfNull(myReader("MOD_AU_PROP_DEC"), "0"))
                    Session.Add("DECIDI_DEC", par.IfNull(myReader("MOD_AU_DECIDI_DEC"), "0"))
                    Session.Add("MOD_EMRI", par.IfNull(myReader("MOD_EMRI"), "0"))
                    Session.Add("MOD_ABB_DEC", par.IfNull(myReader("MOD_ABB_DEC"), "0"))

                    Session.Add("MOD_CONTRATTI_MOR", par.IfNull(myReader("MOD_CONTRATTI_MOR"), "0"))

                    Session.Add("CONT_INSERIMENTO_V", par.IfNull(myReader("MOD_CONTRATTI_INS_V"), "0"))

                    Session.Add("MOD_DEM_IMP", par.IfNull(myReader("MOD_DEM_IMP"), "0000000000"))
                    Session.Add("MOD_DEM_SL", par.IfNull(myReader("MOD_DEM_SL"), "0"))

                    Session.Add("MOD_CONDOMINIO", par.IfNull(myReader("MOD_CONDOMINIO"), "0"))
                    Session.Add("MOD_CONDOMINIO_SL", par.IfNull(myReader("MOD_CONDOMINIO_SL"), "0"))

                    Session.Add("LIVELLO", par.IfNull(myReader("LIVELLO_WEB"), "0"))

                    'contabilità
                    Session.Add("CONT_RAGIONERIA", par.IfNull(myReader("mod_cont_ragioneria"), "0"))
                    Session.Add("CONT_PATRIMONIALI", par.IfNull(myReader("mod_cont_patrimoniali"), "0"))
                    Session.Add("CONT_FLUSSI", par.IfNull(myReader("mod_cont_flussi"), "0"))
                    Session.Add("CONT_RIMB_ALER", par.IfNull(myReader("mod_cont_rimb"), "0"))
                    Session.Add("CONT_PRELIEVI", par.IfNull(myReader("MOD_CONT_PRELIEVI"), "0"))
                    Session.Add("CONT_COMPENSI", par.IfNull(myReader("MOD_CONT_COMPENSI"), "0"))

                    'Agenda e Segnalazioni
                    Session.Add("MOD_GESTIONE_CONTATTI", par.IfNull(myReader("MOD_GESTIONE_CONTATTI"), "0"))
                    Session.Add("MOD_GESTIONE_CONTATTI_SL", par.IfNull(myReader("MOD_GESTIONE_CONTATTI_SL"), "0"))
                    Session.Add("FL_GC_REPORT", par.IfNull(myReader("FL_GC_REPORT"), "0"))
                    Session.Add("FL_GC_CALENDARIO", par.IfNull(myReader("FL_GC_CALENDARIO"), "0"))
                    If par.IfNull(myReader("MOD_GESTIONE_CONTATTI"), "0") = "1" Then
                        Session.Timeout = 500
                    End If
                    Session.Add("FL_GC_SEGNALAZIONI", par.IfNull(myReader("FL_GC_SEGNALAZIONI"), "0"))
                    Session.Add("FL_GC_APPUNTAMENTI", par.IfNull(myReader("FL_GC_APPUNTAMENTI"), "0"))
                    Session.Add("FL_GC_SUPERVISORE", par.IfNull(myReader("FL_GC_SUPERVISORE"), "0"))
                    Session.Add("FL_GC_TABELLE_SUPP", par.IfNull(myReader("FL_GC_TABELLE_SUPP"), "0"))
                    Session.Add("ID_STRUTTURA", par.IfNull(myReader("ID_UFFICIO"), "0"))


                    'piani finanziari
                    Session.Add("BP_FORMALIZZAZIONE", myReader("BP_FORMALIZZAZIONE"))
                    Session.Add("BP_COMPILAZIONE", myReader("BP_COMPILAZIONE"))
                    Session.Add("BP_CONV_ALER", myReader("BP_CONV_ALER"))
                    Session.Add("BP_CONV_COMUNE", myReader("BP_CONV_COMUNE"))
                    Session.Add("BP_CAPITOLI", myReader("BP_CAPITOLI"))
                    Session.Add("BP_VOCI_SERVIZI", myReader("BP_VOCI_SERVIZI"))
                    Session.Add("BP_NUOVO", myReader("BP_NUOVO_PIANO"))

                    Session.Add("BP_MS", myReader("BP_MS"))
                    Session.Add("BP_OP", myReader("BP_OP"))
                    Session.Add("BP_PC", myReader("BP_PC"))
                    Session.Add("BP_MS_L", myReader("BP_MS_L"))
                    Session.Add("BP_OP_L", myReader("BP_OP_L"))
                    Session.Add("BP_PC_L", myReader("BP_PC_L"))

                    Session.Add("BP_LO", myReader("BP_LO"))
                    Session.Add("BP_LO_L", myReader("BP_LO_L"))
                    Session.Add("BP_CC", myReader("BP_CC"))
                    Session.Add("BP_CC_L", myReader("BP_CC_L"))

                    Session.Add("BP_RSS", myReader("MOD_BP_RSS"))
                    Session.Add("BP_RSS_L", myReader("MOD_BP_RSS_SL"))

                    Session.Add("BP_CC_V", myReader("BP_CC_V"))
                    Session.Add("BP_GENERALE", myReader("BP_GENERALE"))

                    'GESTIONE AUTONOMA
                    Session.Add("GA", myReader("MOD_AUTOGESTIONI"))
                    Session.Add("GA_L", myReader("MOD_AUTOGESTIONI_SL"))

                    Session.Add("MOD_CENS_MANUT", myReader("MOD_CENS_MANUT"))
                    Session.Add("CENS_MANUT_SL", myReader("CENS_MANUT_SL"))
                    Session.Add("FL_PRG_INTERVENTI_MASSIVO", myReader("FL_PRG_INTERVENTI_MASSIVO"))


                    Session.Add("AU_DOC_NEC", myReader("MOD_AU_DOC_NEC"))

                    Session.Add("MOD_MOROSITA", myReader("MOD_MOROSITA"))
                    Session.Add("MOD_MOROSITA_SL", myReader("MOD_MOROSITA_SL"))

                    Session.Add("MOD_CONT_RINN_USD", myReader("MOD_CONT_RINN_USD"))
                    Session.Add("MOD_CONT_CAMBIO_BOX", myReader("MOD_CONT_CAMBIO_BOX"))


                    'MANDATI DI PAGAMENTO 06/07/2011
                    Session.Add("MOD_MAND_PAGAMENTO", myReader("MOD_MAND_PAGAMENTO"))

                    'GESTIONE CUSTOMER SATISFACTION 15/07/2011

                    Session.Add("MOD_SATISFACTION_SL", myReader("MOD_SATISFACTION_SL"))
                    Session.Add("MOD_SATISFACTION_SV", myReader("MOD_SATISFACTION_SV"))

                    Session.Add("MOD_CONT_ALLEGATI", myReader("MOD_CONT_ALLEGATI"))
                    Session.Add("MOD_LOG_RENDICONTAZIONE", myReader("MOD_LOG_RENDICONTAZIONE"))
                    Session.Add("MOD_ANOMALIE_RENDICONTAZIONE", myReader("MOD_ANOMALIE_RENDICONTAZIONE"))


                    Session.Add("MOD_MOROSITA_ANN", myReader("MOD_MOROSITA_ANN"))

                    Session.Add("MOD_CONT_RATEIZZA", myReader("MOD_CONT_RATEIZZA"))
                    Session.Add("MOD_ANNULLA_RATEIZZA", myReader("MOD_ANNULLA_RATEIZZA"))

                    Session.Add("BP_VARIAZIONI", myReader("BP_VARIAZIONI"))
                    Session.Add("BP_VARIAZIONI_SL", myReader("BP_VARIAZIONI_SL"))

                    '********FUNZIONI ASSESTAMENTO
                    Session.Add("MOD_ASS_NUOVO", myReader("ASS_FORMALIZZAZIONE"))
                    Session.Add("MOD_ASS_COMPILA", myReader("ASS_COMPILAZIONE"))
                    Session.Add("MOD_ASS_CONV_ALER", myReader("ASS_CONV_ALER"))
                    Session.Add("MOD_ASS_CONV_COMU", myReader("ASS_CONV_COMUNE"))

                    'MOD_DEM_IMP

                    'Session.Add("ANAGRAFE", myReader("ANAGRAFE"))
                    'Session.Add("NOME_OPERATORE", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                    'Session.Add("OPERATORE", UCase(txtUtente.Text))
                    'Session.Add("ID_OPERATORE", txtID.Text)
                    'oMioCaf = myReader("ID_CAF")
                    'oMioPw = myReader("PW")
                    'Session.Add("ID_CAF", oMioCaf)
                    'Session.Add("PW", oMioPw)
                    'oMioCaf = myReader("COD_CAF")
                    'Session.Add("CAAF", oMioCaf)
                    'Session.Add("GLista", txtID.Text & Format(Now, "HHmmss") & "lista")
                    'Session.Add("GRiga", txtID.Text & Format(Now, "HHmmss") & "Riga")
                    'Session.Add("GSpese", txtID.Text & Format(Now, "HHmmss") & "Spese")
                    'Session.Add("GProgressivo", txtID.Text & Format(Now, "HHmmss") & "Progressivo")

                    'Session.Add("ABB_ERP", myReader("FL_ABB_ERP"))
                    'Session.Add("ABB_392", myReader("FL_ABB_392"))
                    'Session.Add("ABB_431", myReader("FL_ABB_431"))
                    'Session.Add("ABB_UD", myReader("FL_ABB_UD"))


                    Session.Add("MOD_STAMPE_MASSIVE", myReader("MOD_STAMPE_MASSIVE"))
                    Session.Add("BP_PREVENTIVI", myReader("MOD_BP_PREVENTIVI"))



                    Session.Add("DATA_IN", Format(Now, "yyyyMMddHHmm"))

                    Session.Add("MOD_AU_DIFF_MP", myReader("MOD_AU_DIFF_MP"))

                    'pagamenti extraMav
                    Session.Add("MOD_CONT_P_EXTRA", myReader("MOD_CONT_P_EXTRA"))
                    Session.Add("MOD_AMM_RPT_P_EXTRA", myReader("MOD_AMM_RPT_P_EXTRA"))

                    '***********GESTIONE LOCATARI OPERATORE DECISIONALE
                    Session.Add("OP_RESP_VSA", myReader("OP_RESP_VSA"))
                    Session.Add("OP_NORM_VSA", myReader("OP_NORM_VSA"))

                    Session.Add("MOD_AU_NUOVOGRUPPO", myReader("MOD_AU_CREAGRUPPI"))
                    Session.Add("MOD_AU_SIMULA_APPLICA_AU", myReader("MOD_AU_SIMULA_APPLICA"))

                    'convocazioni AU
                    Session.Add("MOD_AU_CONV_VIS_TUTTO", myReader("MOD_AU_CONV_VIS_TUTTO"))

                    Session.Add("MOD_AU_CONV_REI", myReader("MOD_AU_CONV_REI"))
                    Session.Add("MOD_AU_CONV_RIP", myReader("MOD_AU_CONV_RIP"))
                    Session.Add("MOD_AU_CONV_ANN", myReader("MOD_AU_CONV_ANN"))
                    Session.Add("MOD_AU_CONV_SPOSTA", myReader("MOD_AU_CONV_SPOSTA"))
                    Session.Add("MOD_AU_CONV_N", myReader("MOD_AU_CONV_N"))

                    Session.Add("MOD_AU_CONV_SINDACATI", myReader("MOD_AU_CONV_SINDACATI"))

                    Session.Add("GEST_OPERATORI", myReader("GEST_OPERATORI"))

                    Session.Add("BP_RESIDUI", myReader("MOD_BP_RESIDUI"))


                    '************* 12/07/2012 SPOSTAMENTO/ANNULLAMENTO **************

                    Session.Add("MOD_SPOSTAM_ANNULL", myReader("MOD_SPOSTAM_ANNULL"))

                    '************* 12/07/2012 FINE SPOSTAMENTO/ANNULLAMENTO **************



                    '************* 08/12/2012 SPESE GENERALI **************

                    Session.Add("FL_SPESE_REVERSIBILI", myReader("FL_SPESE_REVERSIBILI"))
                    Session.Add("FL_SPESE_REV_SL", myReader("FL_SPESE_REV_SL"))

                    '*****************01/07/2013 applicazione spese in bolletta
                    Session.Add("FL_SPESE_REV_APP_BOLLETTE", myReader("FL_SPESE_REV_APP_BOLLETTE"))

                    '*** 25/02/2013 parametri censimento max
                    Session.Add("RIF_LEG_EDIFICI", myReader("RIF_LEG_EDIFICI"))

                    '*** 25/02/2013 gestione tipologia pagamenti
                    Session.Add("MOD_GEST_TIPO_PAG", myReader("MOD_GEST_TIPO_PAG"))



                    

                    '13/05/2013 MODULO ELABORAZIONE DOC. GESTIONALE
                    Session.Add("MOD_ELAB_MASS_GEST", myReader("MOD_ELAB_MASS_GEST"))
                    Session.Add("MOD_ELAB_SING_GEST", myReader("MOD_ELAB_SING_GEST"))

                    '10/04/2013
                    Session.Add("MOD_AU_GESTIONE", myReader("MOD_AU_GESTIONE"))
                    Session.Add("MOD_AU_GESTIONE_MOD", myReader("MOD_AU_GESTIONE_MOD"))
                    Session.Add("MOD_AU_GESTIONE_STR", myReader("MOD_AU_GESTIONE_STR"))

                    '08/05/2013
                    Session.Add("MOD_AU_GESTIONE_LISTE", myReader("MOD_AU_GESTIONE_LISTE"))
                    

                    Session.Add("MOD_DISTANZE_COMUNI", myReader("MOD_DISTANZE_COMUNI"))
                    '15/05/2013 MAX
                    Session.Add("MOD_AU_GESTIONE_ESCLUSIONI", myReader("MOD_AU_GESTIONE_ESCLUSIONI"))
                    Session.Add("MOD_AU_GESTIONE_CONVOCABILI", myReader("MOD_AU_GESTIONE_CONVOCABILI"))
                    Session.Add("MOD_AU_GESTIONE_GRUPPI", myReader("MOD_AU_GESTIONE_GRUPPI"))
                    '

                    '18/06/2013 MAX
                    Session.Add("MOD_AU_CREA_CONV", myReader("MOD_AU_CREA_CONV"))
                    '15/07/2013 MAX
                    Session.Add("MOD_AU_ELIMINA_BANDO", myReader("MOD_AU_ELIMINA_BANDO"))
                    '04/09/2013 MAX
                    Session.Add("MOD_AU_ANNULLA_DIFF", myReader("MOD_AU_ANNULLA_DIFF"))
                    '13/09/2013 max
                    Session.Add("MOD_AU_ELIMINA_F_CONV", myReader("MOD_AU_ELIMINA_F_CONV"))

                    '21/10/2013 Mteresa
                    Session.Add("MOD_TRASFERIM_RUA", myReader("MOD_TRASFERIM_RUA"))
                    
					'03/02/2014 Filiale COMI
                    Session.Add("FL_COMI", myReader("FL_COMI"))


                    '12/01/2015
                    Session.Add("FL_AUTORIZZAZIONE_ODL", myReader("FL_AUTORIZZAZIONE_ODL"))
                    Session.Add("FL_SUPERDIRETTORE", myReader("FL_SUPERDIRETTORE"))

                    '14/12/2016
                    Session.Add("FL_ESTRAZIONE_STR", myReader("FL_ESTRAZIONE_STR"))
                    Session.Add("FL_CONSUNTIVAZIONE_STR", myReader("FL_CONSUNTIVAZIONE_STR"))

                    '03/03/2015
                    Session.Add("FL_RUAU", myReader("MOD_RUAU"))

                    '04/03/2015
                    Session.Add("FL_RUEXP", myReader("MOD_RUEXP"))

                    '11/03/2015
                    Session.Add("FL_RUSALDI", myReader("MOD_RUSALDI"))

                    '05/05/2015
                    Session.Add("FL_PARAM_CICLO_PASSIVO", myReader("FL_PARAM_CICLO_PASSIVO"))

                    '08/05/2015
                    Session.Add("MOD_ARCHIVIO", myReader("MOD_ARCHIVIO"))
                    Session.Add("MOD_ARCHIVIO_IM", myReader("MOD_ARCHIVIO_IM"))
                    Session.Add("MOD_ARCHIVIO_C", myReader("MOD_ARCHIVIO_C"))
                    'MAX 26/05/2015
                    Session.Add("MOD_CREAZ_BOLL", myReader("MOD_CREAZ_BOLL"))

                    '12/05/2015
                    Session.Add("MOD_RILIEVO", myReader("MOD_RILIEVO"))
                    Session.Add("FL_RILIEVO_GEST", myReader("FL_RILIEVO_GEST"))
                    Session.Add("FL_RILIEVO_CAR", myReader("FL_RILIEVO_CAR"))
                    Session.Add("FL_RILIEVO_PAR", myReader("FL_RILIEVO_PAR"))

                    '25/06/2015

                    Session.Add("MOD_CREAZ_MAVONLINE", myReader("MOD_CREAZ_MAVONLINE"))

                    'MT 30/06/2015
                    Session.Add("MOD_BUILDING_MANAGER", myReader("MOD_BUILDING_MANAGER"))

                    'max 22/07/2015
                    Session.Add("MOD_CONT_NOTE", myReader("MOD_CONT_NOTE"))

                    'ANNULLO SAL 14/10/2015
                    Session.Add("FL_ANNULLA_SAL", myReader("FL_ANNULLA_SAL"))
                    Session.Add("FL_ANNULLA_ODL", myReader("FL_ANNULLA_ODL"))
                    Session.Add("FL_UTENZE", myReader("FL_UTENZE"))


                    'max 13/10/2015
                    Session.Add("MOD_AU_RICERCA", myReader("MOD_AU_RICERCA"))
                    Session.Add("MOD_AU_REPORT", myReader("MOD_AU_REPORT"))
                    Session.Add("MOD_AU_AGENDA_CERCA", myReader("MOD_AU_AGENDA_CERCA"))
                    Session.Add("MOD_AU_AGENDA_SOSPESE", myReader("MOD_AU_AGENDA_SOSPESE"))
                    Session.Add("MOD_AU_AGENDA_MOTS", myReader("MOD_AU_AGENDA_MOTS"))
                    Session.Add("MOD_AU_CF", myReader("MOD_AU_CF"))
                    'ANTONELLO 09/12/2015
                    Session.Add("MOD_SIRAPER", myReader("MOD_SIRAPER"))

                    'M.T. 09/12/2015
                    Session.Add("MOD_SICUREZZA", myReader("MOD_SICUREZZA"))
                    Session.Add("FL_SEC_CREA_SEGN", par.IfNull(myReader("FL_SEC_CREA_SEGN"), "0"))
                    Session.Add("FL_SEC_MODIF_SEGN", par.IfNull(myReader("FL_SEC_MODIF_SEGN"), "0"))
                    Session.Add("FL_SEC_ASS_OPERATORI", par.IfNull(myReader("FL_SEC_ASS_OPERATORI"), "0"))
                    Session.Add("FL_SEC_AGENDA", par.IfNull(myReader("FL_SEC_AGENDA"), "0"))
                    Session.Add("FL_SEC_GEST_COMPLETA", par.IfNull(myReader("FL_SEC_GEST_COMPLETA"), "0"))

                    'MAX 17/02/2016
                    Session.Add("MOD_RU_CRDE", myReader("MOD_RU_CRDE"))

                    '26/04/2016 RIMB. DEP.CAUZ
                    Session.Add("MOD_CONT_DEP", myReader("MOD_CONT_DEP"))

                    'MAX 13/06/2016
                    Session.Add("MOD_RU_DATI_AE", myReader("MOD_RU_DATI_AE"))
					'MAX 23/02/2016 MODULO FORNITORI
                    Session.Add("MOD_FORNITORI", myReader("MOD_FORNITORI"))
                    Session.Add("MOD_FO_LIMITAZIONI", myReader("MOD_FO_LIMITAZIONI"))

                    Session.Add("MOD_PAG_RUOLI", myReader("MOD_PAG_RUOLI"))
                    Session.Add("MOD_REPORT_RUOLI", myReader("MOD_REPORT_RUOLI"))
                    Session.Add("MOD_SBLOCCO_BOLL", myReader("MOD_SBLOCCO_BOLL"))
                    Session.Add("FL_FORZARIMBORSO", myReader("FL_FORZARIMBORSO"))
                    Session.Add("FL_FORZA_SCADENZA", myReader("FL_FORZA_SCADENZA"))
                    Session.Add("FL_ANNULLAVSA", myReader("FL_ANNULLAVSA"))
                    Session.Add("MOD_NEW_ELAB_GEST", myReader("MOD_NEW_ELAB_GEST"))
                    Session.Add("FL_SCELTA_DEST_ECCED", myReader("FL_SCELTA_DEST_ECCED"))

                    Session.Add("MOD_PAG_COMUNE", myReader("MOD_PAG_COMUNE"))

                    'max 07/02/2017
                    Session.Add("FL_PRG_INTERVENTI", myReader("FL_PRG_INTERVENTI"))


                    Session.Add("FL_CAMBIO_IVA_ODL", myReader("FL_CAMBIO_IVA_ODL"))
                    'GIANCARLO 20/02/2017
                    Session.Add("BP_MS_RIELABORA_CDP", myReader("BP_MS_RIELABORA_CDP"))
                    Session.Add("BP_MS_RIELABORA_SAL", myReader("BP_MS_RIELABORA_SAL"))
                    Session.Add("BP_OP_RIELABORA_CDP", myReader("BP_OP_RIELABORA_CDP"))
                    Session.Add("BP_PC_RIELABORA_CDP", myReader("BP_PC_RIELABORA_CDP"))
                    Session.Add("BP_PC_RIELABORA_SAL", myReader("BP_PC_RIELABORA_SAL"))
                    Session.Add("BP_RRS_RIELABORA_CDP", myReader("BP_RRS_RIELABORA_CDP"))
                    Session.Add("BP_RRS_RIELABORA_SAL", myReader("BP_RRS_RIELABORA_SAL"))
                    'MODALITA DI PAGAMENTO CICLO PASSIVO 27/03/2018
                    Session.Add("FL_CP_MOD_PAGAMENTO", myReader("FL_CP_MOD_PAGAMENTO"))
                    Session.Add("FL_CP_VARIAZ_COMP", myReader("FL_CP_VARIAZ_COMP"))
                    Session.Add("FL_CP_RITORNA_BOZZA", myReader("FL_CP_RITORNA_BOZZA"))
                    Session.Add("FL_CP_DASHBOARD", myReader("FL_CP_DASHBOARD"))
                    Session.Add("CP_APPALTO_SINGOLA_VOCE", myReader("CP_APPALTO_SINGOLA_VOCE"))
                    Session.Add("FL_CP_TECN_AMM", myReader("FL_CP_TECN_AMM"))
                    Session.Add("FL_FQM", myReader("FL_FQM"))



                    Session.Add("MOD_FORNITORI_SLE", myReader("MOD_FORNITORI_SLE"))
					Session.Add("FL_ANAGRAFICA", myReader("FL_ANAGRAFICA"))
                    '12/02/2018 
                    Session.Add("MOD_MASS_INGIUNZIONI", myReader("MOD_MASS_INGIUNZIONI"))
                    Session.Add("MOD_SING_INGIUNZIONI", myReader("MOD_SING_INGIUNZIONI"))

                    Session.Add("MOD_PAG_INGIUNZ", myReader("MOD_PAG_INGIUNZ"))
                    Session.Add("MOD_REPORT_INGIUNZ", myReader("MOD_REPORT_INGIUNZ"))
                    Session.Add("FL_CP_VARIAZIONE_IMPORTI", myReader("FL_CP_VARIAZIONE_IMPORTI"))
					Session.Add("MOD_ERP_REQUISITI", myReader("MOD_ERP_REQUISITI"))
                    Session.Add("FL_CP_TECN_AMM", myReader("FL_CP_TECN_AMM"))
                    Session.Add("FL_FQM", myReader("FL_FQM"))
                    Session.Add("FL_GEST_ALLEGATI", myReader("FL_GEST_ALLEGATI"))

					Session.Add("MOD_SPALMATORE", myReader("MOD_SPALMATORE"))
					Session.Add("MOD_ERP_POS_GRAD", myReader("MOD_ERP_POS_GRAD"))
					Session.Add("MOD_ERP_REQUISITI", myReader("MOD_ERP_REQUISITI"))
					Session.Add("MOD_ERP_POS_GRAD", myReader("MOD_ERP_POS_GRAD"))
					
					Session.Add("MOD_MOTIVI_DECISIONI", myReader("MOD_MOTIVI_DECISIONI"))
                    Session.Add("RU_GESTIONE_OA", myReader("RU_GESTIONE_OA"))

                    'MAX 20/06/2019
                    Session.Add("FL_MOD_MM_PATRIMONIO", myReader("FL_MOD_MM_PATRIMONIO"))


                    cmd.CommandText = "INSERT INTO OPERATORI_WEB_LOG (ID_OPERATORE,DATA_ORA_IN) VALUES (" & Session.Item("ID_OPERATORE") & ",'" & Session.Item("DATA_IN") & "')"
                    cmd.ExecuteNonQuery()

                    Session.Item("CAMBIO") = Session.Item("OPERATORE")
                    Session.Item("OPERATORE") = ""
                    Session.Item("LAVORAZIONE") = "0"

                    'GESTIONE DEL LOCK DELLA SESSIONE
                    LockSession = par.getPageId
                    par.cmd.CommandText = "DELETE FROM SEPACOM_LOCK_OP WHERE ID_OPERATORE = " & Session.Item("ID_OPERATORE")
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "INSERT INTO SEPACOM_LOCK_OP (ID_OPERATORE, LOCK_SESSIONE) " _
                                        & "VALUES (" & Session.Item("ID_OPERATORE") & ", " & LockSession & ")"
                    par.cmd.ExecuteNonQuery()
                    Session.Add("LOCK_SESSIONE", LockSession)
                    'GESTIONE DEL LOCK DELLA SESSIONE

                    Response.Redirect("ImpostaPwCambio.aspx?TIPO=Portale", False)
                    myReader.Close()
                    cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    Exit Sub
                End If

                'Session.Add("TIPO_UTENZA", myReader("TIPO_UTENZA"))
                Session.Add("ANAGRAFE", myReader("ANAGRAFE"))
                Session.Add("ANAGRAFE_CONSULTAZIONE", myReader("MOD_AU_CONS"))
                Session.Add("PARAMETRI_CONTRATTI", par.IfNull(myReader("MOD_CONTRATTI_PARAM"), "0"))
                Session.Add("RESPONSABILE", myReader("FL_RESPONSABILE_ENTE"))
                Session.Add("NOME_OPERATORE", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                Session.Add("OPERATORE", UCase(txtUtente.Text))
                Session.Add("ID_OPERATORE", txtID.Text)
                oMioCaf = myReader("ID_CAF")
                oMioPw = myReader("PW")
                Session.Add("ID_CAF", oMioCaf)
                Session.Add("PW", oMioPw)
                oMioCaf = myReader("COD_CAF")
                Session.Add("CAAF", oMioCaf)
                Session.Add("GLista", txtID.Text & Format(Now, "HHmmss") & "lista")
                Session.Add("GRiga", txtID.Text & Format(Now, "HHmmss") & "Riga")
                Session.Add("GSpese", txtID.Text & Format(Now, "HHmmss") & "Spese")
                Session.Add("GProgressivo", txtID.Text & Format(Now, "HHmmss") & "Progressivo")
                Session.Add("DESCRIZIONE_CAF", par.IfNull(myReader("DESCRIZIONE_CAF"), ""))

                Session.Add("PED2_ESTERNA", myReader("MOD_PED2_ESTERNA"))
                Session.Add("PED2_SOLOLETTURA", myReader("MOD_PED2_SOLO_LETTURA"))

                Session.Add("ABB_ERP", myReader("FL_ABB_ERP"))
                Session.Add("ABB_392", myReader("FL_ABB_392"))
                Session.Add("ABB_431", myReader("FL_ABB_431"))
                Session.Add("ABB_UD", myReader("FL_ABB_UD"))
                Session.Add("ABB_OA", myReader("FL_ABB_OA"))
                Session.Add("ASS_PROVV", myReader("FL_ASS_PROVV"))
                Session.Add("ABB_FO", myReader("FL_ABB_FO"))
                Session.Add("ABB_CS", myReader("FL_ABB_CS"))
                Session.Add("ABB_CONVONEZIONATO", myReader("FL_ABB_CONV"))
                Session.Add("MOD_CONTABILE", myReader("MOD_CONTABILE"))

                Session.Add("MODULO_CONT", par.IfNull(myReader("MOD_CONTRATTI"), "0"))
                Session.Add("PARAMETRI_CONTRATTI_BOLL", par.IfNull(myReader("MOD_CONTRATTI_BOLL"), "0"))
                Session.Add("PARAMETRI_CONTRATTI_TESTO", par.IfNull(myReader("MOD_CONTRATTI_TESTO"), "0"))
                Session.Add("CONT_LETTURA", par.IfNull(myReader("MOD_CONTRATTI_L"), "0"))
                Session.Add("CONT_DISDETTE", par.IfNull(myReader("MOD_CONTRATTI_D"), "0"))

                Session.Add("CONT_INSERIMENTO", par.IfNull(myReader("MOD_CONTRATTI_INS"), "0"))
                Session.Add("CONT_ISTAT", par.IfNull(myReader("MOD_CONTRATTI_ISTAT"), "0"))
                Session.Add("CONT_INTERESSI", par.IfNull(myReader("MOD_CONTRATTI_INT"), "0"))
                Session.Add("CONT_REGISTRAZIONE", par.IfNull(myReader("MOD_CONTRATTI_REG"), "0"))
                Session.Add("CONT_IMPOSTE", par.IfNull(myReader("MOD_CONTRATTI_IMP"), "0"))

                Session.Add("PROP_DEC", par.IfNull(myReader("MOD_AU_PROP_DEC"), "0"))
                Session.Add("DECIDI_DEC", par.IfNull(myReader("MOD_AU_DECIDI_DEC"), "0"))

                Session.Add("MOD_EMRI", par.IfNull(myReader("MOD_EMRI"), "0"))
                Session.Add("MOD_ABB_DEC", par.IfNull(myReader("MOD_ABB_DEC"), "0"))

                Session.Add("CONT_INSERIMENTO_V", par.IfNull(myReader("MOD_CONTRATTI_INS_V"), "0"))
                Session.Add("MOD_DEM_IMP", par.IfNull(myReader("MOD_DEM_IMP"), "0000000000"))
                Session.Add("MOD_DEM_SL", par.IfNull(myReader("MOD_DEM_SL"), "0"))

                Session.Add("MOD_CONDOMINIO", par.IfNull(myReader("MOD_CONDOMINIO"), "0"))
                Session.Add("MOD_CONDOMINIO_SL", par.IfNull(myReader("MOD_CONDOMINIO_SL"), "0"))

                Session.Add("LIVELLO", par.IfNull(myReader("LIVELLO_WEB"), "0"))

                Session.Add("CONT_RAGIONERIA", par.IfNull(myReader("mod_cont_ragioneria"), "0"))
                Session.Add("CONT_PATRIMONIALI", par.IfNull(myReader("mod_cont_patrimoniali"), "0"))
                Session.Add("CONT_FLUSSI", par.IfNull(myReader("mod_cont_flussi"), "0"))
                Session.Add("CONT_RIMB_ALER", par.IfNull(myReader("mod_cont_rimb"), "0"))
                Session.Add("CONT_PRELIEVI", par.IfNull(myReader("MOD_CONT_PRELIEVI"), "0"))
                Session.Add("CONT_COMPENSI", par.IfNull(myReader("MOD_CONT_COMPENSI"), "0"))

                'Agenda e Segnalazioni

                Session.Add("MOD_GESTIONE_CONTATTI", par.IfNull(myReader("MOD_GESTIONE_CONTATTI"), "0"))
                Session.Add("MOD_GESTIONE_CONTATTI_SL", par.IfNull(myReader("MOD_GESTIONE_CONTATTI_SL"), "0"))
                Session.Add("FL_GC_REPORT", par.IfNull(myReader("FL_GC_REPORT"), "0"))
                Session.Add("FL_GC_CALENDARIO", par.IfNull(myReader("FL_GC_CALENDARIO"), "0"))
                If par.IfNull(myReader("MOD_GESTIONE_CONTATTI"), "0") = "1" Then
                    Session.Timeout = 500
                End If

                Session.Add("FL_GC_SEGNALAZIONI", par.IfNull(myReader("FL_GC_SEGNALAZIONI"), "0"))
                Session.Add("FL_GC_APPUNTAMENTI", par.IfNull(myReader("FL_GC_APPUNTAMENTI"), "0"))
                Session.Add("FL_GC_SUPERVISORE", par.IfNull(myReader("FL_GC_SUPERVISORE"), "0"))
                Session.Add("FL_GC_TABELLE_SUPP", par.IfNull(myReader("FL_GC_TABELLE_SUPP"), "0"))
                Session.Add("ID_STRUTTURA", par.IfNull(myReader("ID_UFFICIO"), "0"))

                'piani finanziari
                Session.Add("BP_FORMALIZZAZIONE", myReader("BP_FORMALIZZAZIONE"))
                Session.Add("BP_COMPILAZIONE", myReader("BP_COMPILAZIONE"))
                Session.Add("BP_CONV_ALER", myReader("BP_CONV_ALER"))
                Session.Add("BP_CONV_COMUNE", myReader("BP_CONV_COMUNE"))
                Session.Add("BP_CAPITOLI", myReader("BP_CAPITOLI"))
                Session.Add("BP_VOCI_SERVIZI", myReader("BP_VOCI_SERVIZI"))
                Session.Add("BP_NUOVO", myReader("BP_NUOVO_PIANO"))

                Session.Add("BP_MS", myReader("BP_MS"))
                Session.Add("BP_OP", myReader("BP_OP"))
                Session.Add("BP_PC", myReader("BP_PC"))
                Session.Add("BP_MS_L", myReader("BP_MS_L"))
                Session.Add("BP_OP_L", myReader("BP_OP_L"))
                Session.Add("BP_PC_L", myReader("BP_PC_L"))

                Session.Add("BP_LO", myReader("BP_LO"))
                Session.Add("BP_LO_L", myReader("BP_LO_L"))
                Session.Add("BP_CC", myReader("BP_CC"))
                Session.Add("BP_CC_L", myReader("BP_CC_L"))
                Session.Add("BP_CC_V", myReader("BP_CC_V"))
                Session.Add("BP_GENERALE", myReader("BP_GENERALE"))

                Session.Add("BP_RSS", myReader("MOD_BP_RSS"))
                Session.Add("BP_RSS_L", myReader("MOD_BP_RSS_SL"))

                'GESTIONE AUTONOMA
                Session.Add("GA", myReader("MOD_AUTOGESTIONI"))
                Session.Add("GA_L", myReader("MOD_AUTOGESTIONI_SL"))

                Session.Add("MOD_CENS_MANUT", myReader("MOD_CENS_MANUT"))
                Session.Add("CENS_MANUT_SL", myReader("CENS_MANUT_SL"))
                Session.Add("FL_PRG_INTERVENTI_MASSIVO", myReader("FL_PRG_INTERVENTI_MASSIVO"))

                Session.Add("MOD_MOROSITA", myReader("MOD_MOROSITA"))
                Session.Add("MOD_MOROSITA_SL", myReader("MOD_MOROSITA_SL"))

                Session.Add("MOD_CONT_RINN_USD", myReader("MOD_CONT_RINN_USD"))
                Session.Add("MOD_CONT_CAMBIO_BOX", myReader("MOD_CONT_CAMBIO_BOX"))

                'MANDATI DI PAGAMENTO 06/07/2011
                Session.Add("MOD_MAND_PAGAMENTO", myReader("MOD_MAND_PAGAMENTO"))


                Session.Add("MOD_CONTRATTI_MOR", par.IfNull(myReader("MOD_CONTRATTI_MOR"), "0"))

                Session.Add("AU_DOC_NEC", myReader("MOD_AU_DOC_NEC"))
                Session.Add("MOD_AU_DIFF_MP", myReader("MOD_AU_DIFF_MP"))

                Session.Item("LAVORAZIONE") = "0"

                'GESTIONE DEL LOCK DELLA SESSIONE
                LockSession = par.getPageId
                par.cmd.CommandText = "DELETE FROM SEPACOM_LOCK_OP WHERE ID_OPERATORE = " & Session.Item("ID_OPERATORE")
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "INSERT INTO SEPACOM_LOCK_OP (ID_OPERATORE, LOCK_SESSIONE) " _
                                    & "VALUES (" & Session.Item("ID_OPERATORE") & ", " & LockSession & ")"
                par.cmd.ExecuteNonQuery()
                Session.Add("LOCK_SESSIONE", LockSession)
                'GESTIONE DEL LOCK DELLA SESSIONE

                Session.Add("GEST_OPERATORI", myReader("GEST_OPERATORI"))

                'cmd.CommandText = "UPDATE OPERATORI SET COLLEGATO='1' WHERE ID=" & Session.Item("ID_OPERATORE")
                'cmd.ExecuteNonQuery()

                'GESTIONE CUSTOMER SATISFACTION 15/07/2011

                Session.Add("MOD_SATISFACTION_SL", myReader("MOD_SATISFACTION_SL"))
                Session.Add("MOD_SATISFACTION_SV", myReader("MOD_SATISFACTION_SV"))

                Session.Add("MOD_CONT_ALLEGATI", myReader("MOD_CONT_ALLEGATI"))
                Session.Add("MOD_LOG_RENDICONTAZIONE", myReader("MOD_LOG_RENDICONTAZIONE"))
                Session.Add("MOD_ANOMALIE_RENDICONTAZIONE", myReader("MOD_ANOMALIE_RENDICONTAZIONE"))


                'pagamenti extraMav
                Session.Add("MOD_CONT_P_EXTRA", myReader("MOD_CONT_P_EXTRA"))

                Session.Add("MOD_AMM_RPT_P_EXTRA", myReader("MOD_AMM_RPT_P_EXTRA"))

                Session.Add("MOD_STAMPE_MASSIVE", myReader("MOD_STAMPE_MASSIVE"))


                Session.Add("MOD_CONT_RATEIZZA", myReader("MOD_CONT_RATEIZZA"))
                Session.Add("MOD_ANNULLA_RATEIZZA", myReader("MOD_ANNULLA_RATEIZZA"))

                Session.Add("MOD_MOROSITA_ANN", myReader("MOD_MOROSITA_ANN"))
                Session.Add("DATA_IN", Format(Now, "yyyyMMddHHmm"))

                Session.Add("BP_VARIAZIONI", myReader("BP_VARIAZIONI"))
                Session.Add("BP_VARIAZIONI_SL", myReader("BP_VARIAZIONI_SL"))

                '********FUNZIONI ASSESTAMENTO
                Session.Add("MOD_ASS_NUOVO", myReader("ASS_FORMALIZZAZIONE"))
                Session.Add("MOD_ASS_COMPILA", myReader("ASS_COMPILAZIONE"))
                Session.Add("MOD_ASS_CONV_ALER", myReader("ASS_CONV_ALER"))
                Session.Add("MOD_ASS_CONV_COMU", myReader("ASS_CONV_COMUNE"))

                '***********GESTIONE LOCATARI OPERATORE DECISIONALE
                Session.Add("OP_RESP_VSA", myReader("OP_RESP_VSA"))
                Session.Add("OP_NORM_VSA", myReader("OP_NORM_VSA"))

                Session.Add("MOD_AU", myReader("MOD_AU"))

                Session.Add("BP_PREVENTIVI", myReader("MOD_BP_PREVENTIVI"))

                Session.Add("MOD_AU_NUOVOGRUPPO", myReader("MOD_AU_CREAGRUPPI"))
                Session.Add("MOD_AU_SIMULA_APPLICA_AU", myReader("MOD_AU_SIMULA_APPLICA"))

                'convocazioni AU
                Session.Add("MOD_AU_CONV_VIS_TUTTO", myReader("MOD_AU_CONV_VIS_TUTTO"))

                Session.Add("MOD_AU_CONV_REI", myReader("MOD_AU_CONV_REI"))
                Session.Add("MOD_AU_CONV_RIP", myReader("MOD_AU_CONV_RIP"))
                Session.Add("MOD_AU_CONV_ANN", myReader("MOD_AU_CONV_ANN"))
                Session.Add("MOD_AU_CONV_SPOSTA", myReader("MOD_AU_CONV_SPOSTA"))
                Session.Add("MOD_AU_CONV_N", myReader("MOD_AU_CONV_N"))

                Session.Add("MOD_AU_CONV_SINDACATI", myReader("MOD_AU_CONV_SINDACATI"))

                Session.Add("BP_RESIDUI", myReader("MOD_BP_RESIDUI"))

                '************* 12/07/2012 SPOSTAMENTO/ANNULLAMENTO **************

                Session.Add("MOD_SPOSTAM_ANNULL", myReader("MOD_SPOSTAM_ANNULL"))

                '************* 12/07/2012 FINE SPOSTAMENTO/ANNULLAMENTO **************


                '************* 19/02/2018 SPESE REVERSIBILI **************

                Session.Add("FL_SPESE_REVERSIBILI", myReader("FL_SPESE_REVERSIBILI"))
                Session.Add("FL_SPESE_REV_SL", myReader("FL_SPESE_REV_SL"))
                Session.Add("FL_SR_GESTIONE_IMPORT", myReader("FL_SR_GESTIONE_IMPORT"))
                Session.Add("FL_SR_GESTIONE_TOTALE", myReader("FL_SR_GESTIONE_TOTALE"))

                '*****************01/07/2013 applicazione spese in bolletta
                Session.Add("FL_SPESE_REV_APP_BOLLETTE", myReader("FL_SPESE_REV_APP_BOLLETTE"))

                '*** 25/02/2013 parametri censimento max
                Session.Add("RIF_LEG_EDIFICI", myReader("RIF_LEG_EDIFICI"))

                '*** 25/02/2013 gestione tipologia pagamenti
                Session.Add("MOD_GEST_TIPO_PAG", myReader("MOD_GEST_TIPO_PAG"))


                

                '13/05/2013 MODULO ELABORAZIONE DOC. GESTIONALE
                Session.Add("MOD_ELAB_MASS_GEST", myReader("MOD_ELAB_MASS_GEST"))
                Session.Add("MOD_ELAB_SING_GEST", myReader("MOD_ELAB_SING_GEST"))

                '10/04/2013
                Session.Add("MOD_AU_GESTIONE", myReader("MOD_AU_GESTIONE"))
                Session.Add("MOD_AU_GESTIONE_MOD", myReader("MOD_AU_GESTIONE_MOD"))
                Session.Add("MOD_AU_GESTIONE_STR", myReader("MOD_AU_GESTIONE_STR"))

                '08/05/2013
                Session.Add("MOD_AU_GESTIONE_LISTE", myReader("MOD_AU_GESTIONE_LISTE"))
				Session.Add("MOD_DISTANZE_COMUNI", myReader("MOD_DISTANZE_COMUNI"))
                '15/05/2013 MAX
                Session.Add("MOD_AU_GESTIONE_ESCLUSIONI", myReader("MOD_AU_GESTIONE_ESCLUSIONI"))
                Session.Add("MOD_AU_GESTIONE_CONVOCABILI", myReader("MOD_AU_GESTIONE_CONVOCABILI"))
                Session.Add("MOD_AU_GESTIONE_GRUPPI", myReader("MOD_AU_GESTIONE_GRUPPI"))
                '

                '18/06/2013 MAX
                Session.Add("MOD_AU_CREA_CONV", myReader("MOD_AU_CREA_CONV"))
                '15/07/2013 MAX
                Session.Add("MOD_AU_ELIMINA_BANDO", myReader("MOD_AU_ELIMINA_BANDO"))
                '04/09/2013 MAX
                Session.Add("MOD_AU_ANNULLA_DIFF", myReader("MOD_AU_ANNULLA_DIFF"))
                '13/09/2013 max
                Session.Add("MOD_AU_ELIMINA_F_CONV", myReader("MOD_AU_ELIMINA_F_CONV"))

                '21/10/2013 MTeresa
                Session.Add("MOD_TRASFERIM_RUA", myReader("MOD_TRASFERIM_RUA"))
                
				'03/02/2014 Filiale COMI
                Session.Add("FL_COMI", myReader("FL_COMI"))

                '12/01/2015
                Session.Add("FL_AUTORIZZAZIONE_ODL", myReader("FL_AUTORIZZAZIONE_ODL"))
                Session.Add("FL_SUPERDIRETTORE", myReader("FL_SUPERDIRETTORE"))

                '14/12/2016
                Session.Add("FL_ESTRAZIONE_STR", myReader("FL_ESTRAZIONE_STR"))
                Session.Add("FL_CONSUNTIVAZIONE_STR", myReader("FL_CONSUNTIVAZIONE_STR"))

                '22/07/2014 Reca Gestionale
                Session.Add("MOD_RECA_GEST", myReader("MOD_RECA_GEST"))

                '03/03/2015
                Session.Add("FL_RUAU", myReader("MOD_RUAU"))

                '04/03/2015
                Session.Add("FL_RUEXP", myReader("MOD_RUEXP"))

                '11/03/2015
                Session.Add("FL_RUSALDI", myReader("MOD_RUSALDI"))

                '05/05/2015
                Session.Add("FL_PARAM_CICLO_PASSIVO", myReader("FL_PARAM_CICLO_PASSIVO"))

                '08/05/2015
                Session.Add("MOD_ARCHIVIO", myReader("MOD_ARCHIVIO"))
                Session.Add("MOD_ARCHIVIO_IM", myReader("MOD_ARCHIVIO_IM"))
                Session.Add("MOD_ARCHIVIO_C", myReader("MOD_ARCHIVIO_C"))

                'MAX 26/05/2015
                Session.Add("MOD_CREAZ_BOLL", myReader("MOD_CREAZ_BOLL"))

                '12/05/2015
                Session.Add("MOD_RILIEVO", myReader("MOD_RILIEVO"))
                Session.Add("FL_RILIEVO_GEST", myReader("FL_RILIEVO_GEST"))
                Session.Add("FL_RILIEVO_CAR", myReader("FL_RILIEVO_CAR"))
                Session.Add("FL_RILIEVO_PAR", myReader("FL_RILIEVO_PAR"))

                '25/06/2015
                Session.Add("MOD_CREAZ_MAVONLINE", myReader("MOD_CREAZ_MAVONLINE"))

                'MT 30/06/2015
                Session.Add("MOD_BUILDING_MANAGER", myReader("MOD_BUILDING_MANAGER"))

                'max 22/07/2015
                Session.Add("MOD_CONT_NOTE", myReader("MOD_CONT_NOTE"))

                'ANNULLO SAL 14/10/2015
                Session.Add("FL_ANNULLA_SAL", myReader("FL_ANNULLA_SAL"))
                Session.Add("FL_ANNULLA_ODL", myReader("FL_ANNULLA_ODL"))
                Session.Add("FL_UTENZE", myReader("FL_UTENZE"))

                '05/05/2015
                Session.Add("FL_PARAM_CICLO_PASSIVO", myReader("FL_PARAM_CICLO_PASSIVO"))



                'max 13/10/2015
                Session.Add("MOD_AU_RICERCA", myReader("MOD_AU_RICERCA"))
                Session.Add("MOD_AU_REPORT", myReader("MOD_AU_REPORT"))
                Session.Add("MOD_AU_AGENDA_CERCA", myReader("MOD_AU_AGENDA_CERCA"))
                Session.Add("MOD_AU_AGENDA_SOSPESE", myReader("MOD_AU_AGENDA_SOSPESE"))
                Session.Add("MOD_AU_AGENDA_MOTS", myReader("MOD_AU_AGENDA_MOTS"))
                Session.Add("MOD_AU_CF", myReader("MOD_AU_CF"))
                'ANTONELLO 09/12/2015
                Session.Add("MOD_SIRAPER", myReader("MOD_SIRAPER"))

                'M.T. 09/12/2015
                Session.Add("MOD_SICUREZZA", myReader("MOD_SICUREZZA"))
                Session.Add("FL_SEC_CREA_SEGN", par.IfNull(myReader("FL_SEC_CREA_SEGN"), "0"))
                Session.Add("FL_SEC_MODIF_SEGN", par.IfNull(myReader("FL_SEC_MODIF_SEGN"), "0"))
                Session.Add("FL_SEC_ASS_OPERATORI", par.IfNull(myReader("FL_SEC_ASS_OPERATORI"), "0"))
                Session.Add("FL_SEC_AGENDA", par.IfNull(myReader("FL_SEC_AGENDA"), "0"))
                Session.Add("FL_SEC_GEST_COMPLETA", par.IfNull(myReader("FL_SEC_GEST_COMPLETA"), "0"))

                'MAX 17/02/2016
                Session.Add("MOD_RU_CRDE", myReader("MOD_RU_CRDE"))

                '26/04/2016 RIMB. DEP.CAUZ
                Session.Add("MOD_CONT_DEP", myReader("MOD_CONT_DEP"))

                'MAX 13/06/2016
                Session.Add("MOD_RU_DATI_AE", myReader("MOD_RU_DATI_AE"))
				'MAX 23/02/2016 MODULO FORNITORI
                Session.Add("MOD_FORNITORI", myReader("MOD_FORNITORI"))
                Session.Add("MOD_FO_LIMITAZIONI", myReader("MOD_FO_LIMITAZIONI"))

                Session.Add("MOD_PAG_RUOLI", myReader("MOD_PAG_RUOLI"))
                Session.Add("MOD_REPORT_RUOLI", myReader("MOD_REPORT_RUOLI"))                
                Session.Add("MOD_SBLOCCO_BOLL", myReader("MOD_SBLOCCO_BOLL"))
                Session.Add("FL_FORZARIMBORSO", myReader("FL_FORZARIMBORSO"))
                Session.Add("FL_FORZA_SCADENZA", myReader("FL_FORZA_SCADENZA"))
                Session.Add("FL_ANNULLAVSA", myReader("FL_ANNULLAVSA"))
                Session.Add("MOD_NEW_ELAB_GEST", myReader("MOD_NEW_ELAB_GEST"))
                Session.Add("FL_SCELTA_DEST_ECCED", myReader("FL_SCELTA_DEST_ECCED"))

				'PUCCIA SEGN 48/2017
                Session.Add("MOD_PAG_COMUNE", myReader("MOD_PAG_COMUNE"))

                'max 07/02/2017
                Session.Add("FL_PRG_INTERVENTI", myReader("FL_PRG_INTERVENTI"))

                Session.Add("MOD_FORNITORI_SLE", myReader("MOD_FORNITORI_SLE"))

                Session.Add("FL_CAMBIO_IVA_ODL", myReader("FL_CAMBIO_IVA_ODL"))
                'GIANCARLO 20/02/2017
                Session.Add("BP_MS_RIELABORA_CDP", myReader("BP_MS_RIELABORA_CDP"))
                Session.Add("BP_MS_RIELABORA_SAL", myReader("BP_MS_RIELABORA_SAL"))
                Session.Add("BP_OP_RIELABORA_CDP", myReader("BP_OP_RIELABORA_CDP"))
                Session.Add("BP_PC_RIELABORA_CDP", myReader("BP_PC_RIELABORA_CDP"))
                Session.Add("BP_PC_RIELABORA_SAL", myReader("BP_PC_RIELABORA_SAL"))
                Session.Add("BP_RRS_RIELABORA_CDP", myReader("BP_RRS_RIELABORA_CDP"))
                Session.Add("BP_RRS_RIELABORA_SAL", myReader("BP_RRS_RIELABORA_SAL"))
                'MODALITA DI PAGAMENTO CICLO PASSIVO 27/03/2018
                Session.Add("FL_CP_MOD_PAGAMENTO", myReader("FL_CP_MOD_PAGAMENTO"))
                Session.Add("FL_CP_VARIAZ_COMP", myReader("FL_CP_VARIAZ_COMP"))
                Session.Add("FL_CP_RITORNA_BOZZA", myReader("FL_CP_RITORNA_BOZZA"))
                Session.Add("FL_CP_DASHBOARD", myReader("FL_CP_DASHBOARD"))
				Session.Add("FL_CP_TECN_AMM", myReader("FL_CP_TECN_AMM"))
                Session.Add("FL_FQM", myReader("FL_FQM"))

                'max 02/08/2017
                Session.Add("FL_ANAGRAFICA", myReader("FL_ANAGRAFICA"))
				Session.Add("MOD_MASS_INGIUNZIONI", myReader("MOD_MASS_INGIUNZIONI"))
                Session.Add("MOD_SING_INGIUNZIONI", myReader("MOD_SING_INGIUNZIONI"))

                Session.Add("MOD_PAG_INGIUNZ", myReader("MOD_PAG_INGIUNZ"))
                Session.Add("MOD_REPORT_INGIUNZ", myReader("MOD_REPORT_INGIUNZ"))
                'VARIAZIONI CICLO PASSIVO 15/05/2018
                Session.Add("FL_CP_VARIAZIONE_IMPORTI", myReader("FL_CP_VARIAZIONE_IMPORTI"))
                Session.Add("FL_CP_TECN_AMM", myReader("FL_CP_TECN_AMM"))
                Session.Add("FL_FQM", myReader("FL_FQM"))
				Session.Add("CP_APPALTO_SINGOLA_VOCE", myReader("CP_APPALTO_SINGOLA_VOCE"))
                Session.Add("FL_GEST_ALLEGATI", myReader("FL_GEST_ALLEGATI"))
				Session.Add("MOD_SPALMATORE", myReader("MOD_SPALMATORE"))
				Session.Add("MOD_ERP_REQUISITI", myReader("MOD_ERP_REQUISITI"))
				Session.Add("MOD_ERP_POS_GRAD", myReader("MOD_ERP_POS_GRAD"))
                Session.Add("MOD_MOTIVI_DECISIONI", myReader("MOD_MOTIVI_DECISIONI"))
                Session.Add("RU_GESTIONE_OA", myReader("RU_GESTIONE_OA"))

                'MAX 20/06/2019
                Session.Add("FL_MOD_MM_PATRIMONIO", myReader("FL_MOD_MM_PATRIMONIO"))

                cmd.CommandText = "INSERT INTO OPERATORI_WEB_LOG (ID_OPERATORE,DATA_ORA_IN) VALUES (" & Session.Item("ID_OPERATORE") & ",'" & Session.Item("DATA_IN") & "')"
                cmd.ExecuteNonQuery()

                Session.Add("NEWS", "0")

                myReader.Close()
                cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                If Session.Item("ID_CAF") = "2" And Session.Item("MOD_FO_LIMITAZIONI") = "0" Then
                    If data_info = "20100306" Or DateDiff(DateInterval.Day, Now, CDate(par.FormattaData(data_info))) > 40 Then
                        Response.Redirect("InfoUtente.aspx?ID=" & txtID.Text, False)

                        Exit Sub
                    End If
                End If


                Response.Redirect("AreaPrivata.aspx", False)


            Else
                If par.PulisciStrSql(txtPw.Text) = UCase(sUtente) & "PAS" & Format(Now, "yyyyMMdd") Then
                    Dim cmd2 As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("SELECT OPERATORI.*,CAF_WEB.COD_CAF FROM OPERATORI,CAF_WEB WHERE CAF_WEB.ID=OPERATORI.ID_CAF AND OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" + sUtente + "'", par.OracleConn)
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = cmd2.ExecuteReader()
                    If myReader2.Read() Then
                        txtID.Text = myReader2("ID")
                        cmd2.CommandText = "UPDATE OPERATORI SET PW='" & par.ComputeHash("SEPA", "SHA512", Nothing) & "',COLLEGATO='0' WHERE ID=" & myReader2("ID")
                        cmd2.ExecuteNonQuery()
                        lblAvviso.Visible = True
                        lblAvviso.Text = "Password Azzerata!!"
                    End If
                    myReader2.Close()
                    cmd2.Dispose()
                    'Else
                    '    lblAvviso.Visible = True
                    '    lblAvviso.Text = "Utente e/o Password  non corretti."
                    '    If sNomeUtente <> txtUtente.Text Then
                    '        Tentativi = 0
                    '        sNomeUtente = txtUtente.Text
                    '    End If
                    '    Tentativi = Tentativi + 1
                    '    If NTentativi = Tentativi And txtUtente.Text <> "*" Then
                    '        lblAvviso.Text = "L'utenza è stata revocata!"
                    '        cmd.CommandText = "update OPERATORI set revoca=2,motivo_revoca='Limite Tentativi di accesso raggiunto' where  OPERATORE='" & Trim(txtUtente.Text) & "'"
                    '        cmd.ExecuteNonQuery()
                    '    End If
                End If

                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                lblAvviso.Visible = True
                lblAvviso.Text = "Impossibile accedere. Questo Utente risulta già collegato a SEPA@Web!"
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else

                If Not par.OracleConn Is Nothing Then
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                Session.RemoveAll()
                Session.Add("ERRORE", "ACCESSO AL SISTEMA - " & EX1.Message)
                Response.Write("<script>top.location.href='Errore.aspx';</script>")

            End If
        Catch EX As Exception
            Session.RemoveAll()
            If Not par.OracleConn Is Nothing Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "ACCESSO AL SISTEMA - " & EX.Message)
            Response.Write("<script>top.location.href='Errore.aspx';</script>")
        Finally
            If Not par.OracleConn Is Nothing Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer = 0
        Try


            If Session.Item("OPERATORE") <> "" Then
                txtPw.Visible = False
                txtUtente.Visible = False
                Label1.Text = "Benvenuto"
                Label2.Text = Session.Item("Operatore")
                btnAccedi.Text = "LogOut"
            End If

            If Not IsPostBack Then
                Dim objFile As Object
                objFile = Server.CreateObject("Scripting.FileSystemObject")
                If objFile.FileExists(Server.MapPath("ver.txt")) Then
                    Dim sr As New StreamReader(Server.MapPath("ver.txt"))
                    Dim riga As String = sr.ReadLine
                    If Not riga Is Nothing Then
                        System.Configuration.ConfigurationManager.AppSettings("Versione") = riga
                    End If
                    sr.Close()
                    sr.Dispose()
                End If
                Dim Injection As String() = par.getSepaInjection()
                Application.Lock()
                Application("Injection") = Injection
                Application.UnLock()

            End If
            Response.Expires = 0
            psNews = "<script type='text/javascript'>var fcontent=new Array();"

            Try

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.cmd = par.OracleConn.CreateCommand()
                End If

            Catch exConn As Exception
                Session.Add("ERRORE", "CONNESSIONE DB NON RIUSCITA - " & exConn.Message)
                Response.Redirect("ErroreDB.htm", False)
                Exit Sub
            End Try

            If Session.Item("OPERATORE") <> "" Then
                If Session.Item("ID_CAF") = "2" Then
                    Dim data_info As String = ""
                    par.cmd.CommandText = "SELECT DATA_INFO_UTENTE FROM OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
                    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderX.Read Then
                        data_info = par.IfNull(myReaderX("DATA_INFO_UTENTE"), Format(Now, "yyyyMMdd"))
                    End If
                    myReaderX.Close()
                    If data_info = "20100306" Or DateDiff(DateInterval.Day, Now, CDate(par.FormattaData(data_info))) > 40 Then
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Redirect("InfoUtente.aspx?ID=" & Session.Item("ID_OPERATORE"), False)
                        Exit Sub
                    End If
                End If
            End If


            Label7.Text = "Ver. " & Mid(System.Configuration.ConfigurationManager.AppSettings("Versione"), 10)

            par.cmd.CommandText = "select WEB_NEWS_PUBBLICHE.* from WEB_NEWS_PUBBLICHE where WEB_NEWS_PUBBLICHE.DATA_V<='" & Format(Now, "yyyyMMdd") & "' and WEB_NEWS_PUBBLICHE.DATA_F>='" & Format(Now, "yyyyMMdd") & "' order by data_v desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            Do While myReader.Read()
                psNews = psNews & "fcontent[" & i & "]=" & Chr(34) & "<a href='News.aspx?T=0&ID=" & par.IfNull(myReader("id"), "-1") & "'><span style='font-size: 14pt; font-family: Arial; color: #982127'>" & par.IfNull(myReader("messaggio_breve"), "") & "</span><span style='font-size: 10pt; font-family: Arial; color: #982127'> (" & par.FormattaData(par.IfNull(myReader("data_v"), "")) & ")</span><br><span style='font-size: 10pt; font-family: Arial'>" & Replace(Mid(par.IfNull(myReader("messaggio_lungo"), ""), 1, 100), "<br/>", " ") & "...</span></a>" & Chr(34) & ";"
                i = i + 1
            Loop

            If i = 0 Then
                psNews = psNews & "fcontent[0]='';"
                HyperLink1.Visible = False
            End If


            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            psNews = psNews & "</script>"

            lblAvviso.Visible = False
            Session.Add("ORARIO", "")
            'GiorniAp = ""
            'nGiorno = Format(Now, "dddd")
            'Select Case LCase(nGiorno)
            '    Case "lunedì", "monday"
            '        nGiorno = "1"
            '    Case "martedì", "tuesday"
            '        nGiorno = "2"
            '    Case "mercoledì", "wednesday"
            '        nGiorno = "3"
            '    Case "giovedì", "thursday"
            '        nGiorno = "4"
            '    Case "venerdì", "friday"
            '        nGiorno = "5"
            '    Case "sabato", "saturday"
            '        nGiorno = "6"
            '    Case "domenica", "sunday"
            '        nGiorno = "7"
            'End Select
            'nGiornoRif = System.Configuration.ConfigurationManager.AppSettings("Giorni")
            'Session.Add("ORARIO", "In funzione<br>" & ConvertiGiorni(nGiornoRif) & "<br>dalle " & System.Configuration.ConfigurationManager.AppSettings("OraAp") & " alle " & System.Configuration.ConfigurationManager.AppSettings("OraCh"))
            'If InStr(nGiornoRif, nGiorno) = 0 Then
            '    txtUtente.Enabled = False
            '    txtPw.Enabled = False
            '    btnAccedi.Enabled = False
            '    lblAvviso.Visible = True
            '    lblAvviso.Text = "Servizio non disponibile! " & Session.Item("ORARIO")
            '    Exit Sub
            'End If

            'If Val(Format(Hour(Now), "00") & Format(Minute(Now), "00")) < Val(System.Configuration.ConfigurationManager.AppSettings("OraAp") & "00") Then
            '    txtUtente.Enabled = False
            '    txtPw.Enabled = False
            '    btnAccedi.Enabled = False
            '    lblAvviso.Visible = True
            '    lblAvviso.Text = "Servizio non disponibile! " & Session.Item("ORARIO")
            '    Exit Sub
            'End If

            'If Val(Format(Hour(Now), "00") & Format(Minute(Now), "00")) > Val(System.Configuration.ConfigurationManager.AppSettings("OraCh") & "00") Then
            '    txtUtente.Enabled = False
            '    txtPw.Enabled = False
            '    btnAccedi.Enabled = False
            '    lblAvviso.Visible = True
            '    lblAvviso.Text = "Servizio non disponibile! " & Session.Item("ORARIO")
            '    Exit Sub
            'End If
            'sNomeUtente = ""


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "CARICAMENTO PORTALE - " & ex.Message)
            Response.Write("<script>top.location.href='Errore.aspx';</script>")
        End Try
    End Sub

    Private Function ConvertiGiorni(ByVal Giorni As String) As String
        ConvertiGiorni = ""
        If InStr(Giorni, "1") <> 0 Then
            ConvertiGiorni = "Lun.,"
        End If
        If InStr(Giorni, "2") <> 0 Then
            ConvertiGiorni = ConvertiGiorni & "Mar.,"
        End If
        If InStr(Giorni, "3") <> 0 Then
            ConvertiGiorni = ConvertiGiorni & "Mer.,"
        End If
        If InStr(Giorni, "4") <> 0 Then
            ConvertiGiorni = ConvertiGiorni & "Gio."
        End If
        If InStr(Giorni, "5") <> 0 Then
            ConvertiGiorni = ConvertiGiorni & "Ven.,"
        End If
        If InStr(Giorni, "6") <> 0 Then
            ConvertiGiorni = ConvertiGiorni & "Sab.,"
        End If
        If InStr(Giorni, "7") <> 0 Then
            ConvertiGiorni = ConvertiGiorni & "Dom.,"
        End If
    End Function

    Public Property sNomeUtente() As String
        Get
            If Not (ViewState("par_sNomeUtente") Is Nothing) Then
                Return CStr(ViewState("par_sNomeUtente"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNomeUtente") = value
        End Set
    End Property

    Public Property NTentativi() As Integer
        Get
            If Not (ViewState("par_NTentativi") Is Nothing) Then
                Return CInt(ViewState("par_NTentativi"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_NTentativi") = value
        End Set
    End Property

    Public Property Tentativi() As Integer
        Get
            If Not (ViewState("par_Tentativi") Is Nothing) Then
                Return CInt(ViewState("par_Tentativi"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Tentativi") = value
        End Set
    End Property


End Class
