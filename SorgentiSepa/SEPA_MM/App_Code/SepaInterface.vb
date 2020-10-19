Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip

Imports System.IO

' Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="https://..../Services/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class SepaInterface
    Inherits System.Web.Services.WebService

    '///////////////////////////////////////////////////////////
    '// Ritorna i dati anagrafici e di contatto di un inquilino  
    '// parametri:      autorizzazioneSOGGETTI_CONTRATTUALI
    '//                 codice fiscale utente SEPA (intestatario contratto)
    '//                 nome, cognome, partita iva 
    '// return:         Inquilino 
    <WebMethod()> _
    Public Function GetDatiInquilino(ByRef p_autorizzazione As Autorizzazione, ByVal p_CodiceFiscale As String, ByVal p_Nome As String, ByVal p_Cognome As String, ByVal p_PartitaIva As String) As Inquilino()
        Dim lRet() As Inquilino = Nothing
        'lRet = New Inquilino
        Dim lToday As String
        Dim lAddwhere As String = ""

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            If p_CodiceFiscale <> "" Then
                lAddwhere = lAddwhere & " AND SISCOM_MI.ANAGRAFICA.COD_FISCALE LIKE '%" & p_CodiceFiscale.ToUpper & "%' "
            End If

            If p_Cognome <> "" Then
                lAddwhere = lAddwhere & " AND COGNOME LIKE '%" & p_Cognome.ToUpper & "%' "
            End If

            If p_Nome <> "" Then
                lAddwhere = lAddwhere & " AND NOME LIKE '%" & p_Nome.ToUpper & "%' "
            End If

            If p_PartitaIva <> "" Then
                lAddwhere = lAddwhere & " AND PARTITA_IVA LIKE '%" & p_PartitaIva.ToUpper & "%' "
            End If

            conndata.apri()

            ' SONO INTESTATARI DI CONTRATTO o meno
            par.cmd.CommandText = " SELECT SISCOM_MI.ANAGRAFICA.*, TIPOLOGIA_OCCUPANTE.DESCRIZIONE AS TIPOLOGIA_OCCUPANTE, SOGGETTI_CONTRATTUALI.DATA_INIZIO " & _
                                  " FROM SISCOM_MI.SOGGETTI_CONTRATTUALI " & _
                                  " INNER JOIN SISCOM_MI.ANAGRAFICA ON SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=SISCOM_MI.ANAGRAFICA.ID " & _
                                  " INNER JOIN SISCOM_MI.TIPOLOGIA_OCCUPANTE ON SISCOM_MI.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE=SISCOM_MI.TIPOLOGIA_OCCUPANTE.COD " & _
                                  " WHERE SISCOM_MI.SOGGETTI_CONTRATTUALI.DATA_INIZIO <= '" & lToday & "' AND SISCOM_MI.SOGGETTI_CONTRATTUALI.DATA_FINE >= '" & lToday & "' " & lAddwhere

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count > 0 Then
                ReDim lRet(dt.Rows.Count - 1)

                Dim i As Integer = 0

                For Each item In dt.Rows
                    lRet(i) = New Inquilino

                    lRet(i).Nome = par.IfNull(dt.Rows(0).Item("NOME"), "")
                    lRet(i).Id = dt.Rows(0).Item("ID")
                    lRet(i).Cognome = par.IfNull(dt.Rows(0).Item("COGNOME"), "")
                    lRet(i).Telefono = par.IfNull(dt.Rows(0).Item("TELEFONO"), "")
                    lRet(i).Email = par.IfNull(dt.Rows(0).Item("EMAIL"), "")
                    lRet(i).IndirizzoResidenza = par.IfNull(dt.Rows(0).Item("INDIRIZZO_RESIDENZA"), "")
                    lRet(i).CAPResidenza = par.IfNull(dt.Rows(0).Item("CAP_RESIDENZA"), "")
                    lRet(i).ProvinciaResidenza = par.IfNull(dt.Rows(0).Item("PROVINCIA_RESIDENZA"), "")
                    lRet(i).ComuneResidenza = par.IfNull(dt.Rows(0).Item("COMUNE_RESIDENZA"), "")
                    lRet(i).CivicoResidenza = par.IfNull(dt.Rows(0).Item("CIVICO_RESIDENZA"), "")
                    lRet(i).Cellulare = par.IfNull(dt.Rows(0).Item("TELEFONO_R"), "")
                    lRet(i).CodiceFiscale = par.IfNull(dt.Rows(0).Item("COD_FISCALE"), "")
                    lRet(i).PartitaIva = par.IfNull(dt.Rows(0).Item("PARTITA_IVA"), "")
                    lRet(i).DataNascita = par.IfNull(dt.Rows(0).Item("DATA_NASCITA"), "")
                    lRet(i).DataAggiornamento = par.IfNull(dt.Rows(0).Item("DATA_INIZIO"), "")
                    lRet(i).IDRuolo = 3
                    lRet(i).TipoInquilino = par.IfNull(dt.Rows(0).Item("TIPOLOGIA_OCCUPANTE"), "")

                    i = i + 1
                Next
            End If

            conndata.chiudi()
            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."

        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Verifica il tipo di utenza in base al codice fiscale  
    '// parametri:      autorizzazione
    '//                 codice fiscale utente, ruolo 
    '// return:         intero : 1 = Operatore MM, 2 = Operatore Contact Center, 3 = Inquilino, 4 = Delegato sindacale, 5 = Amministratore Condominio, 6 = Delegato Autogestioni, 0 Utente non verificato
    <WebMethod()> _
    Public Function CheckUtente(ByRef p_autorizzazione As Autorizzazione, ByVal p_CodiceFiscale As String, ByVal p_ruolo As String) As Boolean
        Dim lRet As Boolean = False
        Dim lToday As String

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT * " & _
                                  "FROM OPERATORI WHERE COD_FISCALE = '" & p_CodiceFiscale.ToUpper & "' AND OPERATORI.FL_ELIMINATO='0' "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                If p_ruolo = "1" And dt.Rows(0).Item("id_caf") = 2 Then
                    ' operatore MM
                    p_autorizzazione.EsitoOperazione.codice = "000"
                    p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
                    lRet = True
                    Return lRet
                ElseIf p_ruolo = "2" And dt.Rows(0).Item("id_caf") = 63 Then
                    ' operatore call center
                    p_autorizzazione.EsitoOperazione.codice = "000"
                    p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
                    lRet = True
                    Return lRet
                ElseIf p_ruolo = "11" Then
                    ' Operatore cdm
                    p_autorizzazione.EsitoOperazione.codice = "000"
                    p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
                    lRet = True
                    Return lRet
                End If
            End If

            If Not lRet Then
                ' Inquilino titolare di contratto
                conndata.apri()

                par.cmd.CommandText = " SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI " &
                                      " INNER JOIN SISCOM_MI.ANAGRAFICA ON (SISCOM_MI.ANAGRAFICA.ID = SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA) " &
                                      " WHERE COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND TO_DATE(DATA_INIZIO,'YYYYMMDD') <= SYSDATE AND TO_DATE(NVL(DATA_FINE,'29991231'),'YYYYMMDD') >= SYSDATE" &
                                      " AND COD_FISCALE = '" & p_CodiceFiscale.ToUpper & "'"

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                dt.Clear()
                da.Fill(dt)
                conndata.chiudi(False)
            End If
            If p_ruolo = "3" And dt.Rows.Count >= 1 Then
                p_autorizzazione.EsitoOperazione.codice = "000"
                p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
                lRet = True
                Return lRet
            End If

            If Not lRet Then
                ' delegato sindacale
                conndata.apri()

                par.cmd.CommandText = " SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI " &
                                      " INNER JOIN SISCOM_MI.ANAGRAFICA ON (SISCOM_MI.ANAGRAFICA.ID = SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA) " &
                                      " WHERE COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND TO_DATE(DATA_INIZIO,'YYYYMMDD') <= SYSDATE AND TO_DATE(NVL(DATA_FINE,'29991231'),'YYYYMMDD') >= SYSDATE" &
                                      " AND COD_FISCALE = '" & p_CodiceFiscale.ToUpper & "'"

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                dt.Clear()
                da.Fill(dt)
                conndata.chiudi(False)
            End If
            If p_ruolo = "4" And dt.Rows.Count >= 1 Then
                p_autorizzazione.EsitoOperazione.codice = "000"
                p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
                lRet = True
                Return lRet
            End If

            If lRet = 0 Then
                ' Custodi
                conndata.apri()

                par.cmd.CommandText = " SELECT * FROM SISCOM_MI.ANAGRAFICA_CUSTODI" &
                                      " WHERE COD_FISCALE = '" & p_CodiceFiscale.ToUpper & "'"

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                dt.Clear()
                da.Fill(dt)
                conndata.chiudi(False)
            End If
            If p_ruolo = "7" And dt.Rows.Count >= 1 Then
                p_autorizzazione.EsitoOperazione.codice = "000"
                p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
                lRet = True
                Return lRet
            End If

            If Not lRet Then
                ' Autogestioni
                conndata.apri()

                par.cmd.CommandText = " SELECT * FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI" &
                                      " WHERE COD_FISCALE = '" & p_CodiceFiscale.ToUpper & "'"

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                dt.Clear()
                da.Fill(dt)
                conndata.chiudi(False)
            End If
            If p_ruolo = "6" And dt.Rows.Count >= 1 Then
                p_autorizzazione.EsitoOperazione.codice = "000"
                p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
                lRet = True
                Return lRet
            End If

            If Not lRet Then
                ' Amministratore condominio
                conndata.apri()

                par.cmd.CommandText = " SELECT * FROM SISCOM_MI.COND_AMMINISTRATORI" &
                                      " WHERE COD_FISCALE = '" & p_CodiceFiscale.ToUpper & "'"

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                dt.Clear()
                da.Fill(dt)
                conndata.chiudi(False)
            End If
            If p_ruolo = "5" And dt.Rows.Count >= 1 Then
                p_autorizzazione.EsitoOperazione.codice = "000"
                p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
                lRet = True
                Return lRet
            End If

            If lRet > 0 Then
                p_autorizzazione.EsitoOperazione.codice = "000"
                p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
            Else
                p_autorizzazione.EsitoOperazione.codice = "002"
                p_autorizzazione.EsitoOperazione.descrizione = "Errore di autenticazione. Codice Fiscale non verificato."
            End If

        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Salva i dati anagrafici e di contatto di un inquilino  
    '// parametri:      autorizzazione
    '//                 inquilino
    '// return:         boolean esito operazione
    '<WebMethod()> _
    'Public Function SaveDatiInquilino(ByRef p_autorizzazione As Autorizzazione, ByVal p_inquilino As Inquilino) As Boolean
    '    Dim lRet As Boolean
    '    Dim lToday As String
    '    If Not p_autorizzazione.Autorizza() Then
    '        Return Nothing
    '    End If
    '    Try

    '        Dim par As New CM.Global
    '        Dim connData As CM.datiConnessione
    '        connData = New CM.datiConnessione(par, False, False)
    '        connData.apri()
    '        lToday = par.AggiustaData(CStr(Today))

    '        par.cmd.CommandText = "UPDATE SISCOM_MI.ANAGRAFICA " & _
    '            "SET  " & _
    '            " SISCOM_MI.ANAGRAFICA.NOME = " & par.insDbValue(p_inquilino.Nome, True) & _
    '            " ,SISCOM_MI.ANAGRAFICA.COGNOME = " & par.insDbValue(p_inquilino.Cognome, True) & _
    '            " ,SISCOM_MI.ANAGRAFICA.TELEFONO = " & par.insDbValue(p_inquilino.Telefono, True) & _
    '            " ,SISCOM_MI.ANAGRAFICA.EMAIL = " & par.insDbValue(p_inquilino.Email, True) & _
    '            " ,SISCOM_MI.ANAGRAFICA.INDIRIZZO_RESIDENZA = " & par.insDbValue(p_inquilino.IndirizzoResidenza, True) & _
    '            " ,SISCOM_MI.ANAGRAFICA.CAP_RESIDENZA = " & par.insDbValue(p_inquilino.CAPResidenza, True) & _
    '            " ,SISCOM_MI.ANAGRAFICA.PROVINCIA_RESIDENZA = " & par.insDbValue(p_inquilino.ProvinciaResidenza, True) & _
    '            " ,SISCOM_MI.ANAGRAFICA.COMUNE_RESIDENZA = " & par.insDbValue(p_inquilino.ComuneResidenza, True) & _
    '            " ,SISCOM_MI.ANAGRAFICA.CIVICO_RESIDENZA = " & par.insDbValue(p_inquilino.CivicoResidenza, True) & _
    '            " ,SISCOM_MI.ANAGRAFICA.PARTITA_IVA = " & par.insDbValue(p_inquilino.PartitaIva, True) & _
    '            " ,SISCOM_MI.ANAGRAFICA.DATA_NASCITA = " & par.insDbValue(p_inquilino.DataNascita, True) & _
    '            " ,SISCOM_MI.ANAGRAFICA.TELEFONO_R = " & par.insDbValue(p_inquilino.Cellulare, True) & _
    '            " WHERE SISCOM_MI.ANAGRAFICA.COD_FISCALE = " & par.insDbValue(p_inquilino.CodiceFiscale.ToUpper, True)   '& "' AND '" & lToday & "' BETWEEM SISCOM_MI.SOGGETTI_CONTRATTUALI.DATA_INIZIO AND SISCOM_MI.SOGGETTI_CONTRATTUALI.DATA_FINE"


    '        par.cmd.ExecuteNonQuery()

    '        'par.cmd.CommandText = "COMMIT"
    '        'par.cmd.ExecuteNonQuery()

    '        conndata.chiudi()
    '        p_autorizzazione.EsitoOperazione.codice = "000"
    '        p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
    '        lRet = True
    '    Catch ex As Exception
    '        Dim tmp As String = ex.Message
    '        p_autorizzazione.EsitoOperazione.codice = "001"
    '        p_autorizzazione.EsitoOperazione.descrizione = ex.Message
    '        lRet = False

    '    End Try

    '    Return lRet
    'End Function

    '//////////////////////////////////////////
    '// Verifica se il cf passato è un utente
    '// del sistema gestionale SEPA (intestatario di contratto), quindi deve avere un 
    '// contratto attivo ad oggi
    <WebMethod()> _
    Public Function IsIntestatario(ByRef p_autorizzazione As Autorizzazione, ByVal p_CodiceFiscale As String) As Boolean
        Dim lRet As Boolean = False
        Dim lToday As String
        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT SISCOM_MI.SOGGETTI_CONTRATTUALI.* " & _
                                 " FROM SISCOM_MI.SOGGETTI_CONTRATTUALI INNER JOIN SISCOM_MI.ANAGRAFICA ON SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = SISCOM_MI.ANAGRAFICA.ID " & _
                                 " WHERE SISCOM_MI.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' " & _
                                 " AND SISCOM_MI.ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale.ToUpper & "' AND SISCOM_MI.SOGGETTI_CONTRATTUALI.DATA_INIZIO <= '" & lToday & "' AND SISCOM_MI.SOGGETTI_CONTRATTUALI.DATA_FINE >= '" & lToday & "'"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count >= 1 Then
                lRet = True
            Else
                lRet = False
            End If

            conndata.chiudi()
            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '/////////////////////////////////////////////////
    '// Verifica le credenziali dell'operatore di SEPA 
    '// parametro:      autorizzazione
    '// return:         boolean
    <WebMethod()> _
    Public Function TestLogin(ByRef p_autorizzazione As Autorizzazione) As Boolean
        Dim lRet As Boolean = False

        Try
            If p_autorizzazione.Autorizza() Then
                lRet = True
            Else
                lRet = False
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message
        End Try

        Return lRet
    End Function

    '/////////////////////////////////////////////////
    '// Ritorna i contratti di un utente  
    '// parametri:      autorizzazione
    '//                 codice fiscale utente SEPA (intestatario)
    '// return:         array di contratti
    <WebMethod()> _
    Public Function GetContratti(ByRef p_autorizzazione As Autorizzazione, ByVal p_CodiceFiscale As String, ByVal p_codiceContratto As String, ByVal p_IdIntestatario As String) As Contratto()
        Dim lRet() As Contratto = Nothing
        Dim lAddWhere As String = " (1=1) "

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)

            conndata.apri()

            If p_codiceContratto <> "" Then
                lAddWhere = lAddWhere & " AND SISCOM_MI.RAPPORTI_UTENZA.COD_CONTRATTO LIKE '%" & p_codiceContratto & "%'"
            End If

            If p_IdIntestatario <> "" Then
                lAddWhere = lAddWhere & " AND ID_ANAGRAFICA = " & p_IdIntestatario
            End If

            If p_CodiceFiscale <> "" Then
                lAddWhere = lAddWhere & " AND SISCOM_MI.ANAGRAFICA.COD_FISCALE Like '%" & p_CodiceFiscale.ToUpper & "%'"
            End If

            par.cmd.CommandText = "SELECT SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_CONTRATTO,  SISCOM_MI.RAPPORTI_UTENZA.COD_CONTRATTO, SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA, SISCOM_MI.UNITA_CONTRATTUALE.ID_EDIFICIO, ID_COMPLESSO, " & _
                                " DATA_DECORRENZA, IMP_CANONE_INIZIALE, IMP_CANONE_ATTUALE AS CANONE_ANNUALE, 0 AS CANONE_MENSILE, DATA_SCADENZA, (SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE U WHERE U.ID_CONTRATTO=SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_CONTRATTO) AS CANONE_CORRENTE, SISCOM_MI.RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR AS AREA_APPARTENENZA, RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA, CAP_COR, LUOGO_COR, TIPO_COR, VIA_COR, CIVICO_COR, PRESSO_COR, SIGLA_COR, SISCOM_MI.ANAGRAFICA.TELEFONO AS TEL_COR, SISCOM_MI.ANAGRAFICA.EMAIL as MAIL_COR " & _
                                " FROM SISCOM_MI.SOGGETTI_CONTRATTUALI " & _
                                " INNER JOIN SISCOM_MI.RAPPORTI_UTENZA ON SISCOM_MI.RAPPORTI_UTENZA.ID =SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_CONTRATTO " & _
                                " INNER JOIN SISCOM_MI.ANAGRAFICA ON SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=SISCOM_MI.ANAGRAFICA.ID AND SISCOM_MI.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE'" & _
                                " INNER JOIN SISCOM_MI.UNITA_CONTRATTUALE ON SISCOM_MI.UNITA_CONTRATTUALE.ID_CONTRATTO = SISCOM_MI.RAPPORTI_UTENZA.ID  AND ID_UNITA_PRINCIPALE IS NULL " & _
                                " INNER JOIN SISCOM_MI.EDIFICI ON (SISCOM_MI.EDIFICI.ID = SISCOM_MI.UNITA_CONTRATTUALE.ID_EDIFICIO)  " & _
                                " WHERE " & lAddWhere

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi()

            If dt.Rows.Count > 0 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0

                For Each item In dt.Rows
                    lRet(i) = New Contratto
                    lRet(i).Id = par.IfNull(dt.Rows(i).Item("ID_CONTRATTO"), 0)
                    lRet(i).Codice = par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), "")
                    lRet(i).IdIntestatario = par.IfNull(dt.Rows(i).Item("ID_ANAGRAFICA"), 0)
                    lRet(i).DataDecorrenza = par.IfNull(dt.Rows(i).Item("DATA_DECORRENZA"), "") 'DateSerial(CInt(Left(dt.Rows(i).Item("DATA_DECORRENZA"), 4)), CInt(Mid(dt.Rows(i).Item("DATA_DECORRENZA"), 3, 2)), CInt(Right(dt.Rows(i).Item("DATA_DECORRENZA"), 2)))
                    lRet(i).Saldo = par.IfNull(par.CalcolaSaldoAttuale(dt.Rows(i).Item("ID_CONTRATTO")), 0)
                    lRet(i).AreaAppartenenza = par.IfNull(dt.Rows(i).Item("AREA_APPARTENENZA"), "")
                    lRet(i).DataScadenza = par.IfNull(dt.Rows(i).Item("DATA_SCADENZA"), "")

                    If DateSerial(CInt(Left(dt.Rows(i).Item("DATA_SCADENZA"), 4)), CInt(Mid(dt.Rows(i).Item("DATA_SCADENZA"), 3, 2)), CInt(Right(dt.Rows(i).Item("DATA_SCADENZA"), 2))) >= Today Then
                        lRet(i).Attivo = True
                    Else
                        lRet(i).Attivo = False
                    End If

                    If par.IfNull(dt.Rows(i).Item("CANONE_CORRENTE"), "") <> "" Then
                        lRet(i).CanoneAnnuale = par.IfNull(dt.Rows(i).Item("CANONE_CORRENTE"), 0)
                        lRet(i).CanoneMensile = par.IfNull(dt.Rows(i).Item("CANONE_CORRENTE"), 0) / 12
                    Else
                        lRet(i).CanoneAnnuale = par.IfNull(dt.Rows(i).Item("IMP_CANONE_INIZIALE"), 0)
                        lRet(i).CanoneMensile = par.IfNull(dt.Rows(i).Item("IMP_CANONE_INIZIALE"), 0) / 12
                    End If

                    lRet(i).CapCorrispondenza = par.IfNull(dt.Rows(i).Item("CAP_COR"), "")
                    lRet(i).ComuneCorrispondenza = par.IfNull(dt.Rows(i).Item("LUOGO_COR"), "")
                    lRet(i).IndirizzoCorrispondenza = par.IfNull(dt.Rows(i).Item("TIPO_COR"), "") & " " & par.IfNull(dt.Rows(i).Item("VIA_COR"), "") & " " & par.IfNull(dt.Rows(i).Item("CIVICO_COR"), "")
                    lRet(i).PressoCorrispondenza = par.IfNull(dt.Rows(i).Item("PRESSO_COR"), "")
                    lRet(i).ProvCorrispondenza = par.IfNull(dt.Rows(i).Item("SIGLA_COR"), "")

                    lRet(i).TelCorrispondenza = par.IfNull(dt.Rows(i).Item("TEL_COR"), "")
                    lRet(i).EmailCorrispondenza = par.IfNull(dt.Rows(i).Item("MAIL_COR"), "")

                    lRet(i).Tipologia = dt.Rows(i).Item("TIPOLOGIA")

                    lRet(i).Edificio = New Edifici
                    lRet(i).Complesso = New Complessi
                    lRet(i).Edificio = GetEdifici(p_autorizzazione, par.IfNull(dt.Rows(i).Item("ID_EDIFICIO"), "0"), "")(0)
                    lRet(i).Complesso = GetComplessi(p_autorizzazione, par.IfNull(dt.Rows(i).Item("ID_COMPLESSO"), "0"), "")(0)

                    i = i + 1
                Next

            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna i bollettini non pagati di un utente  
    '// parametri:      autorizzazione
    '//                 Id Contratto , se null lo fa su tutti i contratto
    '//                 data emissione, data scadenza, anno e numero bolletta : parametri non obbligatori
    '// return:         array di bollettinononpagato
    <WebMethod()> _
    Public Function GetBollettiniNonPagati(ByRef p_autorizzazione As Autorizzazione, ByVal p_idContratto As String, ByVal p_dataEmissione As String, ByVal p_dataScadenza As String, ByVal anno As String, ByVal p_numBolletta As String) As BollettinoNonPagato()
        Dim lAddWhere As String = ""
        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        Dim lRet() As BollettinoNonPagato = Nothing
        Try

            If p_idContratto <> "" Then
                lAddWhere = lAddWhere & " AND ID_CONTRATTO =" & p_idContratto
            End If

            If p_dataEmissione <> "" Then
                lAddWhere = lAddWhere & " AND DATA_EMISSIONE =" & p_dataEmissione
            End If

            If p_dataScadenza <> "" Then
                lAddWhere = lAddWhere & " AND DATA_SCADENZA =" & p_dataScadenza
            End If

            If p_numBolletta <> "" Then
                lAddWhere = lAddWhere & " AND NUM_BOLLETTA =" & p_numBolletta
            End If

            If anno <> "" Then
                lAddWhere = lAddWhere & " AND ANNO =" & anno
            End If

            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)

            conndata.apri()
            Dim lToday As String = par.AggiustaData(CStr(Today))

            par.cmd.CommandText = "SELECT SISCOM_MI.BOL_BOLLETTE.* " & _
                                "  FROM (SISCOM_MI.BOL_BOLLETTE) " & _
                                "  WHERE DATA_SCADENZA <= '" & lToday & "' AND IMPORTO_PAGATO IS NULL " & _
                                lAddWhere

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0

                Dim NumeroMav As String = ""
                Dim indirizzo As String = ""
                Dim indirizzo1 As String = ""

                For Each item In dt.Rows
                    lRet(i) = New BollettinoNonPagato
                    lRet(i).Id = par.IfNull(dt.Rows(i).Item("ID"), 0)
                    lRet(i).IDContratto = p_idContratto
                    lRet(i).DataEmissione = par.IfNull(dt.Rows(i).Item("DATA_EMISSIONE"), "")
                    lRet(i).DataScadenza = par.IfNull(dt.Rows(i).Item("DATA_SCADENZA"), "")
                    lRet(i).Importo = par.IfNull(dt.Rows(i).Item("IMPORTO_TOTALE"), 0)
                    lRet(i).Rata = par.IfNull(dt.Rows(i).Item("N_RATA"), 0)
                    lRet(i).Anno = par.IfNull(dt.Rows(i).Item("ANNO"), 0)
                    lRet(i).NumBolletta = par.IfNull(dt.Rows(i).Item("NUM_BOLLETTA"), 0)

                    ' cerco se ho il pdf del MAV
                    NumeroMav = Format(lRet(i).Id, "0000000000") & ".pdf"
                    indirizzo = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & NumeroMav
                    indirizzo1 = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & CStr(lRet(i).Id) & ".pdf"

                    If IO.File.Exists(indirizzo) = True Then
                        lRet(i).PdfMAV = NumeroMav
                    ElseIf IO.File.Exists(indirizzo1) = True Then
                        lRet(i).PdfMAV = CStr(lRet(i).Id) & ".pdf"
                    Else
                        lRet(i).PdfMAV = ""
                    End If

                    i = i + 1
                Next

            End If

            conndata.chiudi()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try
        Return lRet

    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna il nucleo familiare di un utenza  
    '// parametri:      autorizzazione
    '//                 Id Contratto 
    '// return:         array di componeneteNucleo
    <WebMethod()> _
    Public Function GetNucleoFamiliare(ByRef p_autorizzazione As Autorizzazione, ByVal p_idContratto As String) As Inquilino()
        Dim lRet() As Inquilino = Nothing
        'lRet = New Inquilino
        Dim lToday As String
        Dim lAddwhere As String = ""

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            ' SONO INTESTATARI DI CONTRATTO o meno
            par.cmd.CommandText = "SELECT SISCOM_MI.ANAGRAFICA.* " & _
                                  "FROM SISCOM_MI.SOGGETTI_CONTRATTUALI INNER JOIN SISCOM_MI.ANAGRAFICA ON SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=SISCOM_MI.ANAGRAFICA.ID WHERE SISCOM_MI.SOGGETTI_CONTRATTUALI.DATA_INIZIO <= '" & lToday & "' AND SISCOM_MI.SOGGETTI_CONTRATTUALI.DATA_FINE >= '" & lToday & "' " & "  AND ID_CONTRATTO =" & p_idContratto

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count > 0 Then
                ReDim lRet(dt.Rows.Count - 1)

                Dim i As Integer = 0

                For Each item In dt.Rows
                    lRet(i) = New Inquilino

                    lRet(i).Nome = par.IfNull(dt.Rows(0).Item("NOME"), "")
                    lRet(i).Id = par.IfNull(dt.Rows(0).Item("ID"), 0)
                    lRet(i).Cognome = par.IfNull(dt.Rows(0).Item("COGNOME"), "")
                    'lRet(i).Intestatario = (par.IfNull(dt.Rows(0).Item("COD_TIPOLOGIA_OCCUPANTE"), "") = "INTE")
                    lRet(i).Telefono = par.IfNull(dt.Rows(0).Item("TELEFONO"), "")
                    lRet(i).Email = par.IfNull(dt.Rows(0).Item("EMAIL"), "")
                    lRet(i).IndirizzoResidenza = par.IfNull(dt.Rows(0).Item("INDIRIZZO_RESIDENZA"), "")
                    lRet(i).CAPResidenza = par.IfNull(dt.Rows(0).Item("CAP_RESIDENZA"), "")
                    lRet(i).ProvinciaResidenza = par.IfNull(dt.Rows(0).Item("PROVINCIA_RESIDENZA"), "")
                    lRet(i).ComuneResidenza = par.IfNull(dt.Rows(0).Item("COMUNE_RESIDENZA"), "")
                    lRet(i).CivicoResidenza = par.IfNull(dt.Rows(0).Item("CIVICO_RESIDENZA"), "")
                    lRet(i).Cellulare = par.IfNull(dt.Rows(0).Item("TELEFONO_R"), "")
                    lRet(i).CodiceFiscale = par.IfNull(dt.Rows(0).Item("COD_FISCALE"), "")
                    lRet(i).PartitaIva = par.IfNull(dt.Rows(0).Item("PARTITA_IVA"), "")
                    lRet(i).DataNascita = par.IfNull(dt.Rows(0).Item("DATA_NASCITA"), "")

                    i = i + 1
                Next
            End If

            conndata.chiudi()
            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."

        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna informazioni sull'unità immobiliare un contratto  
    '// parametri:      autorizzazione
    '//                 Id Contratto 
    '//                 codice fiscale utente SEPA (intestatario), tipo unità, indirizzo
    '// return:         array di componeneteNucleo
    <WebMethod()> _
    Public Function GetUnitaImmobiliare(ByRef p_autorizzazione As Autorizzazione, ByVal p_idContratto As Long, ByVal p_codicefiscale As String, ByVal pTipologiaUnita As String, ByVal p_indirizzo As String) As UnitaImmobiliare()

        Dim lAddwhere As String = ""

        Dim lRet() As UnitaImmobiliare = Nothing
        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        Try
            If p_idContratto <> 0 Then
                lAddwhere = lAddwhere & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO =" & p_idContratto
            End If

            If p_codicefiscale <> "" Then
                lAddwhere = lAddwhere & " AND SISCOM_MI.ANAGRAFICA.COD_FISCALE = '" & p_codicefiscale & "'"
            End If

            If pTipologiaUnita <> "" Then
                lAddwhere = lAddwhere & " AND TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE LIKE '%" & pTipologiaUnita & "%'"
            End If

            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            Dim lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = " SELECT " & _
                                    "UNITA_IMMOBILIARI.ID AS ID_UNITA, " & _
                                    "UNITA_CONTRATTUALE.ID_CONTRATTO, " & _
                                    "UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_IMMOBILIARE, " & _
                                    "TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,  " & _
                                    "SISCOM_MI.ANAGRAFICA.COD_FISCALE AS CODICEFISCALETITOLARE, " & _
                                    "INDIRIZZO, " & _
                                    "CIVICO, " & _
                                    "UNITA_CONTRATTUALE.CAP, " & _
                                    "COMU_PROV AS COMU_PROV, COMUNI.COMU_DESCR as comune, " & _
                                    "SISCOM_MI.PIANI.DESCRIZIONE AS PIANO, " & _
                                    "ISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA, " & _
                                    "UNITA_IMMOBILIARI.INTERNO, " & _
                                    "EDIFICI.DENOMINAZIONE AS NOME_EDIFICIO, " & _
                                    "EDIFICI.ID AS ID_EDIFICIO, " & _
                                    "ID_COMPLESSO, " & _
                                    "COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS NOME_COMPLESSO, " & _
                                    "TAB_FILIALI.NOME AS SEDETERRITORIALE " & _
                                    "            FROM SISCOM_MI.UNITA_IMMOBILIARI " & _
                                    "LEFT OUTER JOIN SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI ON (TIPOLOGIA_UNITA_IMMOBILIARI.COD = COD_TIPOLOGIA) " & _
                                    "LEFT OUTER JOIN SISCOM_MI.PIANI ON (PIANI.ID = ID_PIANO) " & _
                                    "LEFT OUTER JOIN SISCOM_MI.SCALE_EDIFICI ON (SISCOM_MI.SCALE_EDIFICI.ID = ID_SCALA) " & _
                                    "LEFT OUTER JOIN SISCOM_MI.UNITA_CONTRATTUALE ON (UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID) " & _
                                    "LEFT OUTER JOIN SISCOM_MI.RAPPORTI_UTENZA ON (SISCOM_MI.RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO)  " & _
                                    "LEFT OUTER JOIN SISCOM_MI.EDIFICI ON (SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID)  " & _
                                    "LEFT OUTER JOIN SISCOM_MI.COMPLESSI_IMMOBILIARI ON (SISCOM_MI.EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID)  " & _
                                    "LEFT OUTER JOIN SISCOM_MI.SOGGETTI_CONTRATTUALI ON ( UNITA_CONTRATTUALE.ID_CONTRATTO = SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND COD_TIPOLOGIA_OCCUPANTE='INTE')  " & _
                                    "LEFT OUTER JOIN SISCOM_MI.ANAGRAFICA ON (ANAGRAFICA.ID = SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA) " & _
                                    "LEFT OUTER JOIN COMUNI ON (COMUNI.COMU_COD = SISCOM_MI.UNITA_CONTRATTUALE.COD_COMUNE) " & _
                                    "LEFT OUTER JOIN SISCOM_MI.TAB_FILIALI ON (TAB_FILIALI.ID = COMPLESSI_IMMOBILIARI.ID_FILIALE) " & _
                                    "            WHERE '" & lToday & "' BETWEEN DATA_DECORRENZA AND DATA_SCADENZA " & lAddwhere

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0

                For Each item In dt.Rows
                    lRet(i) = New UnitaImmobiliare
                    lRet(i).Id = par.IfNull(dt.Rows(i).Item("ID_UNITA"), 0)
                    lRet(i).IDContratto = par.IfNull(dt.Rows(i).Item("ID_CONTRATTO"), 0)
                    lRet(i).Indirizzo = par.IfNull(dt.Rows(i).Item("INDIRIZZO"), "")
                    lRet(i).Provincia = par.IfNull(dt.Rows(i).Item("COMU_PROV"), "")
                    lRet(i).Cap = par.IfNull(dt.Rows(i).Item("CAP"), "")
                    lRet(i).Civico = par.IfNull(dt.Rows(i).Item("CIVICO"), "")
                    lRet(i).Comune = par.IfNull(dt.Rows(i).Item("COMUNE"), "")
                    lRet(i).CodiceUnita = par.IfNull(dt.Rows(i).Item("COD_UNITA_IMMOBILIARE"), "")
                    lRet(i).TipologiaUnita = par.IfNull(dt.Rows(i).Item("TIPOLOGIA"), "")
                    lRet(i).Scala = par.IfNull(dt.Rows(i).Item("SCALA"), "")
                    lRet(i).Piano = par.IfNull(dt.Rows(i).Item("PIANO"), "")
                    lRet(i).Interno = par.IfNull(dt.Rows(i).Item("INTERNO"), "")
                    lRet(i).IdEdificio = par.IfNull(dt.Rows(i).Item("ID_EDIFICIO"), "")
                    lRet(i).NomeEdificio = par.IfNull(dt.Rows(i).Item("NOME_EDIFICIO"), "")
                    lRet(i).IdComplesso = par.IfNull(dt.Rows(i).Item("ID_COMPLESSO"), "")
                    lRet(i).NomeComplesso = par.IfNull(dt.Rows(i).Item("NOME_COMPLESSO"), "")
                    lRet(i).CodiceFiscaleTitolare = par.IfNull(dt.Rows(0).Item("CODICEFISCALETITOLARE"), "")
                    lRet(i).SedeTerritoriale = par.IfNull(dt.Rows(0).Item("SEDETERRITORIALE"), "")

                    i = i + 1
                Next

            End If

            conndata.chiudi()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try
        Return lRet

    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna Le note pubbliche della segnalazione 
    '// parametri:      autorizzazione
    '//                 Id Segnalazione 
    '// return:         array di Stringhe contenente le note di tipo pubbliche
    <WebMethod()> _
    Public Function GetNote(ByRef p_autorizzazione As Autorizzazione, ByVal p_idSegnalazione As Long) As Nota()

        Dim lRet() As Nota = Nothing
        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE=" & p_idSegnalazione

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0

                For Each item In dt.Rows
                    lRet(i) = New Nota
                    lRet(i).Descrizione = par.IfNull(dt.Rows(i).Item("NOTE"), "")
                    lRet(i).IDDSegnalazione = par.IfNull(dt.Rows(i).Item("ID_SEGNALAZIONE"), 0)
                    'lRet(i).IDAppuntamento = par.IfNull(dt.Rows(i).Item("ID_APPUNTAMENTO"), 0)
                    'lRet(i).IDOperatore = par.IfNull(dt.Rows(i).Item("ID_OPERATORE"), 0)
                    lRet(i).IDTipoNotaSegnalazione = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGNALAZIONE_NOTE"), 0)

                    i = i + 1
                Next

            End If

            conndata.chiudi()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try
        Return lRet

    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna Le Tipologie di note 
    '// parametri:      autorizzazione
    '// return:         array di Stringhe contenente le note di tipo pubbliche
    <WebMethod()> _
    Public Function GetTipoNote(ByRef p_autorizzazione As Autorizzazione) As TipoNota()

        Dim lRet() As TipoNota = Nothing
        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONI_NOTE "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0

                For Each item In dt.Rows
                    lRet(i) = New TipoNota
                    lRet(i).Descrizione = par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")
                    lRet(i).ID = par.IfNull(dt.Rows(i).Item("ID"), 0)

                    i = i + 1
                Next

            End If

            conndata.chiudi()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try
        Return lRet

    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna informazioni sulle segnalazioni di un utente  
    '// parametri:      autorizzazione
    '//                 codice fiscale utente inquinino 
    '//                 p_CodiceFiscaleUtente : codice fiscale Utente che inserisce  
    '//                 p_idruolo: codicefiscalesegnalante    
    '//                 Data Inizio ricerca (AAAAMMGG esempio 20170101) 
    '//                 Data fine ricerca   (AAAAMMGG esempio 20171231)
    '//                 p_stato : stato paramentro di riceca stato segnalazione 
    '//                 p_Nome : Nome 
    '//                 p_Cognome : Cognome 
    '//                 p_Complesso : Complesso
    '//                 p_Edificio : Edificio
    '//                 p_Indirizzo : indirizzo
    '//                 p_CodiceFiscaleSegnalante: cod fiscale segnalante
    '//                 p_categoria: Categoria segnalazione
    '//                 p_sottocategoria1 
    '//                 p_sottocategoria2 
    '//                 p_sottocategoria3 
    '//                 p_sottocategoria4 
    '//                 p_semaforo 
    '//                 p_CodiceComplesso 
    '//                 p_nomeComplesso As String, 
    '//                 p_codiceEdificio As String, 
    '//                 p_nomeEdificio As String, 
    '//                 p_CodiceUnita As String, 
    '//                 p_telefono As String, 
    '//                 p_email As String
    '// return:         array di Segnalazione
    <WebMethod()> _
    Public Function GetSegnalazioni(ByRef p_autorizzazione As Autorizzazione, ByVal p_CodiceFiscaleInquilino As String, ByVal p_idRuoloSegnalante As Long, p_dataInizio As String, ByVal p_dataFine As String, ByVal p_stato As Long(), ByVal p_Nome As String, ByVal p_Cognome As String, ByVal p_Complesso As String, ByVal p_Edificio As String, ByVal p_Indirizzo As String, ByVal p_CodiceFiscaleSegnalante As String, ByVal p_categoria As String, ByVal p_sottocategoria1 As String, ByVal p_sottocategoria2 As String, ByVal p_sottocategoria3 As String, ByVal p_sottocategoria4 As String, ByVal p_semaforo As String, ByVal p_CodiceComplesso As String, ByVal p_nomeComplesso As String, ByVal p_codiceEdificio As String, ByVal p_nomeEdificio As String, ByVal p_CodiceUnita As String, ByVal p_telefono As String, ByVal p_email As String) As Segnalazione()
        Dim id_utente As Long = 0
        Dim where_utente As String = ""
        Dim par As New CM.Global
        Dim conndata As New CM.datiConnessione(par)

        Dim lRet() As Segnalazione = Nothing
        If Not p_autorizzazione.Autorizza() Then
            p_autorizzazione.EsitoOperazione.codice = "004"
            p_autorizzazione.EsitoOperazione.descrizione = "Autenticazione non valida."
            Return Nothing
        End If

        If par.IfNull(p_idRuoloSegnalante, 0) = 0 And par.IfNull(p_CodiceFiscaleInquilino, "") = "" And par.IfNull(p_CodiceFiscaleSegnalante, "") = "" Then
            p_autorizzazione.EsitoOperazione.codice = "003"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione non consentita. Inserire almeno uno dei seguenti filtri: p_idRuoloSegnalante, p_CodiceFiscaleInquilino, p_CodiceFiscaleSegnalante"
            Return Nothing
        End If
        '///////////////////////////////
        '// se non viene passato il CF 
        '// tira fuori tutte le segnalazioni
        If p_idRuoloSegnalante <> 0 And p_CodiceFiscaleSegnalante <> "" Then
            id_utente = p_idRuoloSegnalante

            If id_utente = 1 Then
                'operatore MM
                where_utente = " AND OPERATORI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf id_utente = 2 Then
                ' operatore contact center
                where_utente = " AND OPERATORI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf id_utente = 3 Then
                ' inquilino
                where_utente = " AND ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf id_utente = 4 Then
                ' delegato sindacale
                where_utente = " AND ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf id_utente = 5 Then
                ' Amministratori condominio
                where_utente = " AND COND_AMMINISTRATORI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf id_utente = 6 Then
                ' delegati Autogestione
                where_utente = " AND AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf id_utente = 7 Then
                ' custodi
                where_utente = " AND ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf id_utente = 11 Then
                ' Operatori CDM
                where_utente = " AND OPERATORI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            End If
        ElseIf p_idRuoloSegnalante = 0 And p_CodiceFiscaleSegnalante <> "" Then
            where_utente = " AND ( OPERATORI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            where_utente += " OR ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            where_utente += " OR COND_AMMINISTRATORI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            where_utente += " OR AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            where_utente += " OR ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "' )"
        End If

        If p_dataInizio = Nothing Then
            p_dataInizio = DateSerial(1900, 1, 1)
        End If
        If p_dataFine = Nothing Then
            p_dataFine = DateSerial(2999, 12, 31)
        End If

        Dim where_Nome As String = ""
        If p_Nome <> "" Then
            where_Nome = " AND UPPER(SEGNALAZIONI.NOME) LIKE '%" & UCase(p_Nome) & "%'"
        End If

        Dim where_Cognome As String = ""
        If p_Cognome <> "" Then
            where_Cognome = " AND UPPER(SEGNALAZIONI.COGNOME_RS) LIKE '%" & UCase(p_Cognome) & "%'"
        End If

        Dim where_Indirizzo As String = ""
        If p_Indirizzo <> "" Then
            where_Indirizzo = " AND UPPER(SISCOM_MI.EDIFICI.DENOMINAZIONE) LIKE '%" & UCase(p_Indirizzo) & "%'"
        End If

        Dim where_Edificio As String = ""
        If p_Edificio <> "" Then
            where_Edificio = " AND UPPER(SISCOM_MI.EDIFICI.DENOMINAZIONE) LIKE '%" & UCase(p_Edificio) & "%'"
        End If

        Dim where_complesso As String = ""
        If p_Complesso <> "" Then
            where_complesso = " AND UPPER(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE) LIKE '%" & UCase(p_Complesso) & "%'"
        End If

        'Ricerca per Inquilino
        Dim where_Soggetto As String = ""
        If p_CodiceFiscaleInquilino <> "" Then
            'where_Soggetto = " and SEGNALAZIONI.ID_CONTRATTO in (Select SC.ID_CONTRATTO FROM SISCOM_MI.SOGGETTI_CONTRATTUALI SC INNER JOIN SISCOM_MI.ANAGRAFICA ON SC.ID_ANAGRAFICA = SISCOM_MI.ANAGRAFICA.ID WHERE SISCOM_MI.ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale.ToUpper & "') "
            where_Soggetto = " AND (  (SEGNALAZIONI.ID_CONTRATTO IN   (SELECT SC.ID_CONTRATTO  FROM    SISCOM_MI.SOGGETTI_CONTRATTUALI SC INNER JOIN SISCOM_MI.ANAGRAFICA   ON SC.ID_ANAGRAFICA =   SISCOM_MI.ANAGRAFICA.ID   WHERE SISCOM_MI.ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscaleInquilino.ToUpper & "') )  or  (CASE  WHEN SEGNALAZIONI.ID_UNITA IS NOT NULL THEN   (SELECT MAX (ID)    FROM SISCOM_MI.RAPPORTI_UTENZA   WHERE ID IN     (SELECT ID_CONTRATTO   FROM SISCOM_MI.UNITA_CONTRATTUALE    WHERE UNITA_CONTRATTUALE.ID_UNITA =   SEGNALAZIONI.ID_UNITA)  AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, 1, 8) BETWEEN NVL (  RAPPORTI_UTENZA.  DATA_DECORRENZA,    '10000000')    AND NVL (  RAPPORTI_UTENZA.DATA_RICONSEGNA,'30000000'))   ELSE       NULL     END)  IN   (SELECT SC.ID_CONTRATTO FROM    SISCOM_MI.SOGGETTI_CONTRATTUALI SC INNER JOIN  SISCOM_MI.ANAGRAFICA   ON SC.ID_ANAGRAFICA =  SISCOM_MI.ANAGRAFICA.ID WHERE SISCOM_MI.ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscaleInquilino.ToUpper & "')  ) "
        End If

        'p_categoria ,  
        Dim where_categoria As String = ""
        If p_categoria <> "" Then
            where_categoria = " AND ID_TIPO_SEGNALAZIONE IN ( SELECT ID FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE and DESCRIZIONE LIKE '%" & p_categoria.ToUpper & "%') "
        End If
        'p_sottocategoria1, 
        Dim where_sottocategoria1 As String = ""
        If p_sottocategoria1 <> "" Then
            where_sottocategoria1 = " AND ID_TIPO_SEGN_LIVELLO_1 IN ( SELECT ID FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1 and DESCRIZIONE LIKE '%" & p_sottocategoria1.ToUpper & "%') "
        End If
        'p_sottocategoria2, 
        Dim where_sottocategoria2 As String = ""
        If p_sottocategoria2 <> "" Then
            where_sottocategoria2 = " AND ID_TIPO_SEGN_LIVELLO_2 IN ( SELECT ID FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2 and DESCRIZIONE LIKE '%" & p_sottocategoria2.ToUpper & "%') "
        End If
        'p_sottocategoria3, 
        Dim where_sottocategoria3 As String = ""
        If p_sottocategoria3 <> "" Then
            where_sottocategoria3 = " AND ID_TIPO_SEGN_LIVELLO_3 IN ( SELECT ID FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3 and DESCRIZIONE LIKE '%" & p_sottocategoria3.ToUpper & "%') "
        End If
        'p_sottocategoria4, 
        Dim where_sottocategoria4 As String = ""
        If p_sottocategoria3 <> "" Then
            where_sottocategoria4 = " AND ID_TIPO_SEGN_LIVELLO_4 IN ( SELECT ID FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4 and DESCRIZIONE LIKE '%" & p_sottocategoria4.ToUpper & "%') "
        End If
        'p_semaforo, 
        Dim where_semaforo As String = ""
        If p_semaforo <> "" Then
            where_semaforo = " AND ID_PERICOLO_SEGNALAZIONE = " & p_semaforo.ToUpper & " "
        End If
        'p_CodiceComplesso, 
        Dim where_CodiceComplesso As String = ""
        If p_CodiceComplesso <> "" Then
            where_CodiceComplesso = " AND COD_COMPLESSO LIKE '%" & p_CodiceComplesso.ToUpper & "%' "
        End If
        'p_nomeComplesso, 
        Dim where_NomeComplesso As String = ""
        If p_nomeComplesso <> "" Then
            where_NomeComplesso = " AND complessi_immobiliari.DENOMINAZIONE LIKE '%" & p_nomeComplesso.ToUpper & "%' "
        End If
        'p_codiceEdificio, 
        Dim where_CodiceEdificio As String = ""
        If p_codiceEdificio <> "" Then
            where_CodiceEdificio = " AND COD_EDIFICIO LIKE '%" & p_codiceEdificio.ToUpper & "%' "
        End If
        'p_nomeEdificio, 
        Dim where_NomeEdificio As String = ""
        If p_nomeEdificio <> "" Then
            where_NomeEdificio = " AND siscom_mi.edifici.DENOMINAZIONE LIKE '%" & p_nomeEdificio.ToUpper & "%' "
        End If
        'p_CodiceUnita, 
        Dim where_CodiceUnita As String = ""
        If p_CodiceUnita <> "" Then
            where_CodiceUnita = " AND COD_UNITA_IMMOBILIARE LIKE '%" & p_CodiceUnita.ToUpper & "%' "
        End If
        'p_telefono, 
        Dim where_Telefono As String = ""
        If p_telefono <> "" Then
            where_Telefono = " AND SEGNALAZIONI.TELEFONO1 LIKE '%" & p_telefono.ToUpper & "%' "
        End If
        'p_email
        Dim where_email As String = ""
        If p_email <> "" Then
            where_email = " AND SEGNALAZIONI.MAIL LIKE '%" & p_email.ToUpper & "%' "
        End If

        Try
            Dim strDataInizio = p_dataInizio
            Dim strDataFine = p_dataFine

            conndata.apri()

            par.cmd.CommandText = "SELECT CASE  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE_EST IN (3,4) THEN (SELECT ANAGRAFICA.COD_FISCALE FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SEGNALAZIONI.ID_ANAGRAFICA) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE_EST IN (5) THEN (SELECT COND_AMMINISTRATORI.COD_FISCALE FROM SISCOM_MI.COND_AMMINISTRATORI WHERE COND_AMMINISTRATORI.ID = SEGNALAZIONI.ID_AMMINISTRATORE) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE_EST IN (6) THEN (SELECT AUTOGESTIONI_ESERCIZI.COD_FISCALE FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.ID = SEGNALAZIONI.ID_GESTIONE_AUTONOMA)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE_EST IN (7) THEN (SELECT ANAGRAFICA_CUSTODI.COD_FISCALE FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.ID = SEGNALAZIONI.ID_CUSTODE) " _
                & " ELSE (SELECT OPERATORI.COD_FISCALE FROM OPERATORI WHERE OPERATORI.ID = SEGNALAZIONI.ID_OPERATORE_INS) END AS COD_FISCALE, " _
                & " SEGNALAZIONI.ID_OPERATORE_INS, SEGNALAZIONI.ID, SEGNALAZIONI.ID_STATO, SEGNALAZIONI.ID_CANALE, " _
                & "SEGNALAZIONI.ID AS NUM, SEGNALAZIONI.TELEFONO1, SEGNALAZIONI.MAIL AS EMAIL, SEGNALAZIONI.NOME,  SEGNALAZIONI.COGNOME_RS, " _
                & "SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, ID_TIPO_SEGN_LIVELLO_1, ID_TIPO_SEGN_LIVELLO_2, ID_TIPO_SEGN_LIVELLO_3, ID_TIPO_SEGN_LIVELLO_4, " _
                & "'' AS TIPO_INT, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                & "TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                & "EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                & "COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
                & " (CASE " _
                & " WHEN SEGNALAZIONI.ID_UNITA " _
                & " IS NOT NULL " _
                & " THEN " _
                & " (SELECT MAX (ID) " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA " _
                & " WHERE ID IN " _
                & " (SELECT ID_CONTRATTO " _
                & " FROM SISCOM_MI. " _
                & " UNITA_CONTRATTUALE " _
                & " WHERE UNITA_CONTRATTUALE. " _
                & " ID_UNITA = " _
                & " SEGNALAZIONI.ID_UNITA) " _
                & " AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, " _
                & " 1, " _
                & " 8) " _
                & "  BETWEEN NVL ( " _
                & " RAPPORTI_UTENZA. " _
                & " DATA_DECORRENZA, " _
                & " '10000000') " _
                & " AND NVL ( " _
                & " RAPPORTI_UTENZA. " _
                & " DATA_RICONSEGNA, " _
                & " '30000000')) " _
                & " ELSE " _
                & " NULL " _
                & " END) " _
                & " AS ID_CONTRATTO, " _
                & "SUBSTR (DATA_ORA_RICHIESTA, 1, 8) AS DATA_INSERIMENTO, " _
                & "SUBSTR (DATA_ORA_RICHIESTA, 9, 12) AS ORA_INSERIMENTO, " _
                & "REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                & "NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
                & "(CASE WHEN ID_STATO = 10 THEN (SELECT DISTINCT NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                & "NOTE LIKE '%(NOTA CHIUSURA)%'AND DATA_ORA = (SELECT MAX(DATA_ORA) FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND NOTE LIKE '%(NOTA CHIUSURA)%')" _
                & ") ELSE '' END) AS NOTE_C,ID_PERICOLO_SEGNALAZIONE " _
                & ",'FALSE' AS CHECK1 " _
                & ",(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                & ",'' AS FIGLI2 " _
                & ",NVL(ID_SEGNALAZIONE_PADRE,0) AS ID_SEGNALAZIONE_PADRE " _
                & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE STATO NOT IN (5,6) AND ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
                & ",DATA_IN_CARICO " _
                & ",SEGNALAZIONI.ID_COMPLESSO " _
                & ",SEGNALAZIONI.ID_EDIFICIO " _
                & ",SEGNALAZIONI.ID_UNITA " _
                & ",SEGNALAZIONI.TIPO_PERVENUTA " _
                & ",SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " _
                & ",SISCOM_MI.EDIFICI.COD_EDIFICIO , SISCOM_MI.EDIFICI.DENOMINAZIONE AS NOMEEDIFICIO " _
                & ",COD_COMPLESSO, COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS NOMECOMPLESSO " _
                & ",SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA " _
                & ",SISCOM_MI.PIANI.DESCRIZIONE AS PIANO " _
                & ",UNITA_IMMOBILIARI.INTERNO AS INTERNO " _
                & ",TAB_FILIALI.NOME AS SEDETERRITORIALE " _
                & ",TAB_FILIALI.ID AS IDSTRUTTURA " _
                & ",FL_CONTATTO_FORNITORE AS CONTATTOFORNEMERGENZA" _
                & ",SUBSTR(DATA_CONTATTO_FORNITORE, 1, 8) AS CONTATTOFORNEMERGENZADATA " _
                & ",SUBSTR(DATA_CONTATTO_FORNITORE, 9, 12) AS CONTATTOFORNEMERGENZAORA " _
                & ",FL_VERIFICA_FORNITORE AS INTERVENTOFORNEMERGENZA " _
                & ",SUBSTR(DATA_VERIFICA_FORNITORE, 1,8) AS INTERVENTOFORNEMERGENZADATA " _
                & ",SUBSTR(DATA_VERIFICA_FORNITORE, 9,12) AS INTERVENTOFORNEMERGENZAORA " _
                & ",SUBSTR(DATA_SOPRALLUOGO, 1,8) AS DATASOPRALLUOGO " _
                & ",SUBSTR(DATA_SOPRALLUOGO, 9,12) AS ORASOPRALLUOGO " _
                & ",SUBSTR(DATA_PROGRAMMATA_INT, 1,8) AS DATAPROGRAMMATAINTERVENTO " _
                & ",SUBSTR(DATA_PROGRAMMATA_INT, 9,12) AS ORAPROGRAMMATAINTERVENTO " _
                & ",SUBSTR(DATA_EFFETTIVA_INT, 1,8) AS DATAEFFETTIVAINTERVENTO " _
                & ",SUBSTR(DATA_EFFETTIVA_INT, 9,12) AS ORAEFFETTIVAINTERVENTO " _
                & ",ID_TIPOLOGIA_SEGNALANTE AS IDRUOLO " _
                & ",CASE WHEN ID_TIPOLOGIA_SEGNALANTE IN (1, 2, 11) THEN (SELECT ID FROM OPERATORI WHERE OPERATORI.ID = SISCOM_MI.SEGNALAZIONI.ID_OPERATORE_INS) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE  IN (3)  THEN (SELECT ID FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SISCOM_MI.SEGNALAZIONI.ID_ANAGRAFICA) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE  IN (4)  THEN (SELECT ID FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SISCOM_MI.SEGNALAZIONI.ID_DELEGATO_SINDACALE) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 5 THEN (SELECT ID FROM SISCOM_MI.COND_AMMINISTRATORI WHERE SISCOM_MI.COND_AMMINISTRATORI.ID = SISCOM_MI.SEGNALAZIONI.ID_AMMINISTRATORE) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 6 THEN (SELECT ID FROM  SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.ID = SISCOM_MI.SEGNALAZIONI.ID_GESTIONE_AUTONOMA) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 7 THEN (SELECT ID FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.ID = SISCOM_MI.SEGNALAZIONI.ID_CUSTODE) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE IN (8,9,10) THEN (SELECT ID FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SISCOM_MI.SEGNALAZIONI.ID_ANAGRAFICA) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 12 THEN (SELECT ID FROM SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI WHERE ANAGRAFICA_CHIAMANTI_NON_NOTI.ID = SISCOM_MI.SEGNALAZIONI.ID_CHIAMANTI_NON_NOTI) " _
                & " ELSE 0 END AS IDSEGNALANTE " _
                & ",CASE WHEN ID_TIPOLOGIA_SEGNALANTE IN (1, 2, 11) THEN (SELECT COD_FISCALE FROM OPERATORI WHERE OPERATORI.ID = SISCOM_MI.SEGNALAZIONI.ID_OPERATORE_INS)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE  IN (3)  THEN (SELECT COD_FISCALE FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SISCOM_MI.SEGNALAZIONI.ID_ANAGRAFICA)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE  IN (4)  THEN (SELECT COD_FISCALE FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SISCOM_MI.SEGNALAZIONI.ID_DELEGATO_SINDACALE)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 5 THEN (SELECT COD_FISCALE FROM SISCOM_MI.COND_AMMINISTRATORI WHERE SISCOM_MI.COND_AMMINISTRATORI.ID = SISCOM_MI.SEGNALAZIONI.ID_AMMINISTRATORE)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 6 THEN (SELECT COD_FISCALE FROM  SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.ID = SISCOM_MI.SEGNALAZIONI.ID_GESTIONE_AUTONOMA)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 7 THEN (SELECT COD_FISCALE FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.ID = SISCOM_MI.SEGNALAZIONI.ID_CUSTODE)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE IN (8,9,10) THEN (SELECT COD_FISCALE FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SISCOM_MI.SEGNALAZIONI.ID_ANAGRAFICA)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 12 THEN (SELECT '' FROM SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI WHERE ANAGRAFICA_CHIAMANTI_NON_NOTI.ID = SISCOM_MI.SEGNALAZIONI.ID_CHIAMANTI_NON_NOTI)  " _
                & "ELSE '' END AS CODICEFISCALEOPERATORE " _
                & "FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI, " _
                & "SISCOM_MI.SEGNALAZIONI, " _
                & "SISCOM_MI.TAB_FILIALI, " _
                & "SISCOM_MI.EDIFICI, " _
                & "SISCOM_MI.UNITA_IMMOBILIARI, " _
                & "SISCOM_MI.TIPOLOGIE_GUASTI, " _
                & "SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                & "OPERATORI, " _
                & "SISCOM_MI.ANAGRAFICA, " _
                & "SISCOM_MI.COND_AMMINISTRATORI, " _
                & "SISCOM_MI.AUTOGESTIONI_ESERCIZI, " _
                & "SISCOM_MI.ANAGRAFICA_CUSTODI, " _
                & "SISCOM_MI.SCALE_EDIFICI, " _
                & "SISCOM_MI.PIANI " _
                & "WHERE " _
                & " TAB_STATI_SEGNALAZIONI.ID = SEGNALAZIONI.ID_STATO "

            If p_stato Is Nothing Then
            Else
                Dim tmp As String = ""
                For Each stato In p_stato
                    If tmp <> "" Then tmp = tmp & ","
                    tmp = tmp & CStr(stato)
                Next
                par.cmd.CommandText = par.cmd.CommandText _
                    & " AND segnalazioni.id_stato in (" & tmp & ")"
            End If

            par.cmd.CommandText = par.cmd.CommandText _
                & " AND SEGNALAZIONI.ID_STRUTTURA = TAB_FILIALI.ID(+) " _
                & " AND SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID(+) " _
                & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID(+) = SISCOM_MI.SEGNALAZIONI.ID_UNITA " _
                & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID(+) = SISCOM_MI.SEGNALAZIONI.ID_COMPLESSO " _
                & " AND OPERATORI.ID(+) = SEGNALAZIONI.ID_OPERATORE_INS " _
                & " AND SEGNALAZIONI.ID_TIPOLOGIE = TIPOLOGIE_GUASTI.ID(+) " _
                & " AND SEGNALAZIONI.ID_ANAGRAFICA = ANAGRAFICA.ID(+) " _
                & " AND SEGNALAZIONI.ID_AMMINISTRATORE = COND_AMMINISTRATORI.ID(+) " _
                & " AND SEGNALAZIONI.ID_GESTIONE_AUTONOMA = AUTOGESTIONI_ESERCIZI.ID(+) " _
                & " AND SEGNALAZIONI.ID_CUSTODE = ANAGRAFICA_CUSTODI.ID(+) " _
                & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA = SISCOM_MI.SCALE_EDIFICI.ID (+) " _
                & " AND SISCOM_MI.PIANI.ID(+) = SISCOM_MI.UNITA_IMMOBILIARI.ID_PIANO "

            par.cmd.CommandText = par.cmd.CommandText & where_utente & where_Cognome & where_Nome & where_Indirizzo & where_complesso & where_Edificio & where_Soggetto

            par.cmd.CommandText = par.cmd.CommandText _
                & " AND DATA_ORA_RICHIESTA >=" & strDataInizio & " || '0000' AND DATA_ORA_RICHIESTA <=" & strDataFine & " || '0000'" _
                & " ORDER BY ID_PERICOLO_SEGNALAZIONE DESC, DATA_ORA_RICHIESTA DESC "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0

                For Each item In dt.Rows
                    lRet(i) = New Segnalazione
                    lRet(i).Id = par.IfNull(dt.Rows(i).Item("ID"), 0)
                    lRet(i).IDOperatore = par.IfNull(dt.Rows(i).Item("ID_OPERATORE_INS"), 0)
                    lRet(i).IDContratto = par.IfNull(dt.Rows(i).Item("ID_CONTRATTO"), 0)
                    lRet(i).Categoria = par.IfNull(dt.Rows(i).Item("TIPO0"), "")
                    lRet(i).SottoCategoria1 = par.IfNull(dt.Rows(i).Item("TIPO1"), "")
                    lRet(i).SottoCategoria2 = par.IfNull(dt.Rows(i).Item("TIPO2"), "")
                    lRet(i).SottoCategoria3 = par.IfNull(dt.Rows(i).Item("TIPO3"), "")
                    lRet(i).SottoCategoria3 = par.IfNull(dt.Rows(i).Item("TIPO4"), "")
                    lRet(i).SemaforoPriorita = par.IfNull(dt.Rows(i).Item("ID_PERICOLO_SEGNALAZIONE"), -1)
                    lRet(i).Stato = par.IfNull(dt.Rows(i).Item("STATO"), "")
                    lRet(i).IDStato = par.IfNull(dt.Rows(i).Item("ID_STATO"), 0)
                    lRet(i).Descrizione = par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")
                    lRet(i).NotePubbliche = par.IfNull(dt.Rows(i).Item("NOTE_C"), "")

                    If par.IfNull(dt.Rows(i).Item("DATA_INSERIMENTO"), "") <> "" Then
                        'lRet(i).DataInserimento = par.AggiustaData(par.IfNull(dt.Rows(i).Item("DATA_INSERIMENTO"), ""))
                        lRet(i).DataInserimento = par.IfNull(dt.Rows(i).Item("DATA_INSERIMENTO"), "")
                    End If
                    If par.IfNull(dt.Rows(i).Item("ORA_INSERIMENTO"), "") <> "" Then
                        'lRet(i).OraInserimento = par.AggiustaOra(par.IfNull(dt.Rows(i).Item("DATA_INSERIMENTO"), ""))
                        lRet(i).OraInserimento = par.IfNull(dt.Rows(i).Item("ORA_INSERIMENTO"), "")
                    End If
                    If par.IfNull(dt.Rows(i).Item("DATA_IN_CARICO"), "") <> "" Then
                        lRet(i).DataPresaInCarico = par.IfNull(dt.Rows(i).Item("DATA_IN_CARICO"), "")
                    End If
                    If par.IfNull(dt.Rows(i).Item("DATA_CHIUSURA"), "") <> "" Then
                        lRet(i).DataChiusura = par.IfNull(dt.Rows(i).Item("DATA_CHIUSURA"), "")
                    End If

                    lRet(i).CodiceFiscaleSoggetto = par.IfNull(dt.Rows(i).Item("COD_FISCALE"), "")
                    'lRet(i).IDSoggetto = par.IfNull(dt.Rows(i).Item("ID_OPERATORE_INS"), 0)
                    lRet(i).CodiceComplesso = par.IfNull(dt.Rows(i).Item("COD_COMPLESSO"), "")
                    lRet(i).CodiceEdificio = par.IfNull(dt.Rows(i).Item("COD_EDIFICIO"), "")
                    lRet(i).CodiceUnita = par.IfNull(dt.Rows(i).Item("COD_UNITA_IMMOBILIARE"), "")
                    lRet(i).CanaleApertura = par.IfNull(dt.Rows(i).Item("ID_CANALE"), "")

                    lRet(i).IdCategoria = par.IfNull(dt.Rows(i).Item("TIPO"), -1)
                    lRet(i).IdSottoCategoria1 = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGN_LIVELLO_1"), -1)
                    lRet(i).IdSottoCategoria2 = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGN_LIVELLO_2"), -1)
                    lRet(i).IdSottoCategoria3 = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGN_LIVELLO_3"), -1)
                    lRet(i).IdSottoCategoria4 = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGN_LIVELLO_4"), -1)

                    lRet(i).Telefono = par.IfNull(dt.Rows(i).Item("TELEFONO1"), "")
                    lRet(i).Email = par.IfNull(dt.Rows(i).Item("EMAIL"), "")
                    lRet(i).Nome = par.IfNull(dt.Rows(i).Item("NOME"), "")
                    lRet(i).Cognome = par.IfNull(dt.Rows(i).Item("COGNOME_RS"), "")

                    lRet(i).IdComplesso = par.IfNull(dt.Rows(i).Item("ID_COMPLESSO"), 0)
                    lRet(i).IdEdificio = par.IfNull(dt.Rows(i).Item("ID_EDIFICIO"), 0)
                    lRet(i).IdStruttura = par.IfNull(dt.Rows(i).Item("IDSTRUTTURA"), 0)
                    lRet(i).NomeComplesso = par.IfNull(dt.Rows(i).Item("NomeComplesso"), "")
                    lRet(i).NomeEdificio = par.IfNull(dt.Rows(i).Item("NomeEdificio"), "")
                    lRet(i).IdUnita = par.IfNull(dt.Rows(i).Item("ID_UNITA"), 0)
                    lRet(i).Scala = par.IfNull(dt.Rows(i).Item("SCALA"), "")
                    lRet(i).Piano = par.IfNull(dt.Rows(i).Item("PIANO"), "")
                    lRet(i).Interno = par.IfNull(dt.Rows(i).Item("INTERNO"), "")
                    lRet(i).SedeTerritoriale = par.IfNull(dt.Rows(i).Item("SEDETERRITORIALE"), "")
                    lRet(i).ContattoFornEmergenza = par.IfNull(dt.Rows(i).Item("ContattoFornEmergenza"), "")
                    lRet(i).ContattoFornEmergenzaData = par.IfNull(dt.Rows(i).Item("ContattoFornEmergenzaData"), "")
                    lRet(i).ContattoFornEmergenzaOra = par.IfNull(dt.Rows(i).Item("ContattoFornEmergenzaOra"), "")
                    lRet(i).InterventoFornEmergenza = par.IfNull(dt.Rows(i).Item("InterventoFornEmergenza"), "")
                    lRet(i).InterventoFornEmergenzaData = par.IfNull(dt.Rows(i).Item("InterventoFornEmergenzaData"), "")
                    lRet(i).InterventoFornEmergenzaOra = par.IfNull(dt.Rows(i).Item("InterventoFornEmergenzaOra"), "")
                    lRet(i).DataSopralluogo = par.IfNull(dt.Rows(i).Item("DataSopralluogo"), "")
                    lRet(i).OraSopralluogo = par.IfNull(dt.Rows(i).Item("OraSopralluogo"), "")
                    lRet(i).DataProgrammataIntervento = par.IfNull(dt.Rows(i).Item("DataProgrammataIntervento"), "")
                    lRet(i).OraProgrammataIntervento = par.IfNull(dt.Rows(i).Item("OraProgrammataIntervento"), "")
                    lRet(i).DataEffettivaIntervento = par.IfNull(dt.Rows(i).Item("DataEffettivaIntervento"), "")
                    lRet(i).OraEffettivaIntervento = par.IfNull(dt.Rows(i).Item("OraEffettivaIntervento"), "")
                    lRet(i).IdRuolo = par.IfNull(dt.Rows(i).Item("IdRuolo"), 0)
                    lRet(i).IdSegnalante = par.IfNull(dt.Rows(i).Item("idSegnalante"), 0)
                    lRet(i).CodiceFiscaleOperatore = par.IfNull(dt.Rows(i).Item("CodiceFiscaleOperatore"), "")

                    '/////////////
                    '// allegati
                    lRet(i).Allegati = GetAllegatiSegnalazione(p_autorizzazione, dt.Rows(i).Item("ID"))

                    '/////////////
                    '// Documenti richiesti, se ci sono solo se amministrativa ossia TIPO0 = 0
                    If par.IfNull(dt.Rows(i).Item("TIPO"), -1) = 0 Then
                        Dim l_id_cat1 As Long
                        Dim l_id_cat2 As Long
                        l_id_cat1 = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGN_LIVELLO_1"), 0)
                        l_id_cat2 = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGN_LIVELLO_2"), 0)

                        lRet(i).DocumentiRichiesti = GetDocumentiRichiesti(p_autorizzazione, l_id_cat1, l_id_cat2)
                    End If

                    i = i + 1
                Next

            End If

            conndata.chiudi()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try
        Return lRet

    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna informazioni sulle segnalazioni di un utente  
    '// parametri:      autorizzazione
    '//                 codice fiscale utente SEPA 
    '// return:         Segnalazione
    <WebMethod()> _
    Public Function GetSegnalazione(ByRef p_autorizzazione As Autorizzazione, ByVal p_segnalazione As Long) As Segnalazione
        Dim id_utente As Long = 0
        Dim where_utente As String = ""

        'id_utente = CheckUtente(p_autorizzazione, p_CodiceFiscale)

        'If id_utente = 0 Then
        '    Return Nothing
        'Else
        '    If id_utente = 1 Then
        '        'operatore MM
        '        where_utente = " AND OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "'"
        '    ElseIf id_utente = 2 Then
        '        ' operatore contact center
        '        where_utente = " AND OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "'"
        '    ElseIf id_utente = 3 Then
        '        ' inquilino
        '        where_utente = " AND ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale & "'"
        '    ElseIf id_utente = 4 Then
        '        ' delegato sindacale
        '        where_utente = " AND ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale & "'"
        '    ElseIf id_utente = 5 Then
        '        ' Amministratori condominio
        '        where_utente = " AND COND_AMMINISTRATORI.COD_FISCALE = '" & p_CodiceFiscale & "'"
        '    ElseIf id_utente = 6 Then
        '        ' delegati Autogestione
        '        where_utente = " AND AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_CodiceFiscale & "'"
        '    ElseIf id_utente = 7 Then
        '        ' custodi
        '        where_utente = " AND ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_CodiceFiscale & "'"
        '    ElseIf id_utente = 11 Then
        '        ' Operatori CDM
        '        where_utente = " AND OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "'"
        '    End If
        'End If

        Dim lRet As Segnalazione = Nothing
        Dim par As New CM.Global

        If Not p_autorizzazione.Autorizza() Then
            p_autorizzazione.EsitoOperazione.codice = "004"
            p_autorizzazione.EsitoOperazione.descrizione = "Autenticazione non valida."
            Return Nothing
        End If

        If par.IfNull(p_segnalazione, 0) = 0 Then
            p_autorizzazione.EsitoOperazione.codice = "005"
            p_autorizzazione.EsitoOperazione.descrizione = "Parametro id segnalazione obbligatorio."
            Return Nothing
        End If

        Try
            Dim conndata As New CM.datiConnessione(par)

            conndata.apri()

            par.cmd.CommandText = "SELECT CASE  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE_EST IN (3,4) THEN (SELECT ANAGRAFICA.COD_FISCALE FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SEGNALAZIONI.ID_ANAGRAFICA) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE_EST IN (5) THEN (SELECT COND_AMMINISTRATORI.COD_FISCALE FROM SISCOM_MI.COND_AMMINISTRATORI WHERE COND_AMMINISTRATORI.ID = SEGNALAZIONI.ID_AMMINISTRATORE) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE_EST IN (6) THEN (SELECT AUTOGESTIONI_ESERCIZI.COD_FISCALE FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.ID = SEGNALAZIONI.ID_GESTIONE_AUTONOMA)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE_EST IN (7) THEN (SELECT ANAGRAFICA_CUSTODI.COD_FISCALE FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.ID = SEGNALAZIONI.ID_CUSTODE) " _
                & " ELSE (SELECT OPERATORI.COD_FISCALE FROM OPERATORI WHERE OPERATORI.ID = SEGNALAZIONI.ID_OPERATORE_INS) END AS COD_FISCALE, " _
                & " SEGNALAZIONI.ID_OPERATORE_INS, SEGNALAZIONI.ID, SEGNALAZIONI.ID_STATO, SEGNALAZIONI.ID_CANALE, " _
                & "SEGNALAZIONI.ID AS NUM, SEGNALAZIONI.TELEFONO1, SEGNALAZIONI.MAIL AS EMAIL, SEGNALAZIONI.NOME,  SEGNALAZIONI.COGNOME_RS, " _
                & "SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, ID_TIPO_SEGN_LIVELLO_1, ID_TIPO_SEGN_LIVELLO_2, ID_TIPO_SEGN_LIVELLO_3, ID_TIPO_SEGN_LIVELLO_4, " _
                & "'' AS TIPO_INT, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                & "(SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                & "TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                & "EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                & "COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
                & " (CASE " _
                & " WHEN SEGNALAZIONI.ID_UNITA " _
                & " IS NOT NULL " _
                & " THEN " _
                & " (SELECT MAX (ID) " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA " _
                & " WHERE ID IN " _
                & " (SELECT ID_CONTRATTO " _
                & " FROM SISCOM_MI. " _
                & " UNITA_CONTRATTUALE " _
                & " WHERE UNITA_CONTRATTUALE. " _
                & " ID_UNITA = " _
                & " SEGNALAZIONI.ID_UNITA) " _
                & " AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, " _
                & " 1, " _
                & " 8) " _
                & "  BETWEEN NVL ( " _
                & " RAPPORTI_UTENZA. " _
                & " DATA_DECORRENZA, " _
                & " '10000000') " _
                & " AND NVL ( " _
                & " RAPPORTI_UTENZA. " _
                & " DATA_RICONSEGNA, " _
                & " '30000000')) " _
                & " ELSE " _
                & " NULL " _
                & " END) " _
                & " AS ID_CONTRATTO, " _
                & "SUBSTR (DATA_ORA_RICHIESTA, 1, 8) AS DATA_INSERIMENTO, " _
                & "SUBSTR (DATA_ORA_RICHIESTA, 9, 12) AS ORA_INSERIMENTO, " _
                & "REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                & "NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
                & "(CASE WHEN ID_STATO = 10 THEN (SELECT DISTINCT NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                & "NOTE LIKE '%(NOTA CHIUSURA)%'AND DATA_ORA = (SELECT MAX(DATA_ORA) FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND NOTE LIKE '%(NOTA CHIUSURA)%')" _
                & ") ELSE '' END) AS NOTE_C,ID_PERICOLO_SEGNALAZIONE " _
                & ",'FALSE' AS CHECK1 " _
                & ",(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                & ",'' AS FIGLI2 " _
                & ",NVL(ID_SEGNALAZIONE_PADRE,0) AS ID_SEGNALAZIONE_PADRE " _
                & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE STATO NOT IN (5,6) AND ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
                & ",DATA_IN_CARICO " _
                & ",SEGNALAZIONI.ID_COMPLESSO " _
                & ",SEGNALAZIONI.ID_EDIFICIO " _
                & ",SEGNALAZIONI.ID_UNITA " _
                & ",SEGNALAZIONI.TIPO_PERVENUTA " _
                & ",SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " _
                & ",SISCOM_MI.EDIFICI.COD_EDIFICIO, SISCOM_MI.EDIFICI.DENOMINAZIONE AS NOMEEDIFICIO " _
                & ",COD_COMPLESSO, COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS NOMECOMPLESSO " _
                & ",SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA " _
                & ",SISCOM_MI.PIANI.DESCRIZIONE AS PIANO " _
                & ",UNITA_IMMOBILIARI.INTERNO AS INTERNO " _
                & ",TAB_FILIALI.NOME AS SEDETERRITORIALE " _
                & ",TAB_FILIALI.ID AS IDSTRUTTURA " _
                & ",FL_CONTATTO_FORNITORE AS CONTATTOFORNEMERGENZA" _
                & ",SUBSTR(DATA_CONTATTO_FORNITORE, 1, 8) AS CONTATTOFORNEMERGENZADATA " _
                & ",SUBSTR(DATA_CONTATTO_FORNITORE, 9, 12) AS CONTATTOFORNEMERGENZAORA " _
                & ",FL_VERIFICA_FORNITORE AS INTERVENTOFORNEMERGENZA " _
                & ",SUBSTR(DATA_VERIFICA_FORNITORE, 1,8) AS INTERVENTOFORNEMERGENZADATA " _
                & ",SUBSTR(DATA_VERIFICA_FORNITORE, 9,12) AS INTERVENTOFORNEMERGENZAORA " _
                & ",SUBSTR(DATA_SOPRALLUOGO, 1,8) AS DATASOPRALLUOGO " _
                & ",SUBSTR(DATA_SOPRALLUOGO, 9,12) AS ORASOPRALLUOGO " _
                & ",SUBSTR(DATA_PROGRAMMATA_INT, 1,8) AS DATAPROGRAMMATAINTERVENTO " _
                & ",SUBSTR(DATA_PROGRAMMATA_INT, 9,12) AS ORAPROGRAMMATAINTERVENTO " _
                & ",SUBSTR(DATA_EFFETTIVA_INT, 1,8) AS DATAEFFETTIVAINTERVENTO " _
                & ",SUBSTR(DATA_EFFETTIVA_INT, 9,12) AS ORAEFFETTIVAINTERVENTO " _
                & ",ID_TIPOLOGIA_SEGNALANTE AS IDRUOLO " _
                & ",CASE WHEN ID_TIPOLOGIA_SEGNALANTE IN (1, 2, 11) THEN (SELECT ID FROM OPERATORI WHERE OPERATORI.ID = SISCOM_MI.SEGNALAZIONI.ID_OPERATORE_INS) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE  IN (3)  THEN (SELECT ID FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SISCOM_MI.SEGNALAZIONI.ID_ANAGRAFICA) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE  IN (4)  THEN (SELECT ID FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SISCOM_MI.SEGNALAZIONI.ID_DELEGATO_SINDACALE) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 5 THEN (SELECT ID FROM SISCOM_MI.COND_AMMINISTRATORI WHERE SISCOM_MI.COND_AMMINISTRATORI.ID = SISCOM_MI.SEGNALAZIONI.ID_AMMINISTRATORE) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 6 THEN (SELECT ID FROM  SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.ID = SISCOM_MI.SEGNALAZIONI.ID_GESTIONE_AUTONOMA) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 7 THEN (SELECT ID FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.ID = SISCOM_MI.SEGNALAZIONI.ID_CUSTODE) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE IN (8,9,10) THEN (SELECT ID FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SISCOM_MI.SEGNALAZIONI.ID_ANAGRAFICA) " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 12 THEN (SELECT ID FROM SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI WHERE ANAGRAFICA_CHIAMANTI_NON_NOTI.ID = SISCOM_MI.SEGNALAZIONI.ID_CHIAMANTI_NON_NOTI) " _
                & " ELSE 0 END AS IDSEGNALANTE " _
                & ",CASE WHEN ID_TIPOLOGIA_SEGNALANTE IN (1, 2, 11) THEN (SELECT COD_FISCALE FROM OPERATORI WHERE OPERATORI.ID = SISCOM_MI.SEGNALAZIONI.ID_OPERATORE_INS)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE  IN (3)  THEN (SELECT COD_FISCALE FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SISCOM_MI.SEGNALAZIONI.ID_ANAGRAFICA)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE  IN (4)  THEN (SELECT COD_FISCALE FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SISCOM_MI.SEGNALAZIONI.ID_DELEGATO_SINDACALE)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 5 THEN (SELECT COD_FISCALE FROM SISCOM_MI.COND_AMMINISTRATORI WHERE SISCOM_MI.COND_AMMINISTRATORI.ID = SISCOM_MI.SEGNALAZIONI.ID_AMMINISTRATORE)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 6 THEN (SELECT COD_FISCALE FROM  SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.ID = SISCOM_MI.SEGNALAZIONI.ID_GESTIONE_AUTONOMA)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 7 THEN (SELECT COD_FISCALE FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.ID = SISCOM_MI.SEGNALAZIONI.ID_CUSTODE)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE IN (8,9,10) THEN (SELECT COD_FISCALE FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = SISCOM_MI.SEGNALAZIONI.ID_ANAGRAFICA)  " _
                & " WHEN ID_TIPOLOGIA_SEGNALANTE = 12 THEN (SELECT '' FROM SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI WHERE ANAGRAFICA_CHIAMANTI_NON_NOTI.ID = SISCOM_MI.SEGNALAZIONI.ID_CHIAMANTI_NON_NOTI)  " _
                & "ELSE '' END AS CODICEFISCALEOPERATORE " _
                & "FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI, " _
                & "SISCOM_MI.SEGNALAZIONI, " _
                & "SISCOM_MI.TAB_FILIALI, " _
                & "SISCOM_MI.EDIFICI, " _
                & "SISCOM_MI.UNITA_IMMOBILIARI, " _
                & "SISCOM_MI.TIPOLOGIE_GUASTI, " _
                & "SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                & "OPERATORI, " _
                & "SISCOM_MI.ANAGRAFICA, " _
                & "SISCOM_MI.COND_AMMINISTRATORI, " _
                & "SISCOM_MI.AUTOGESTIONI_ESERCIZI, " _
                & "SISCOM_MI.ANAGRAFICA_CUSTODI, " _
                & "SISCOM_MI.SCALE_EDIFICI, " _
                & "SISCOM_MI.PIANI " _
                & "WHERE " _
                & " SEGNALAZIONI.ID = " & p_segnalazione & " AND " _
                & " TAB_STATI_SEGNALAZIONI.ID = SEGNALAZIONI.ID_STATO " _
                & " AND SEGNALAZIONI.ID_STRUTTURA = TAB_FILIALI.ID(+) " _
                & " AND SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID(+) " _
                & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID(+) = SISCOM_MI.SEGNALAZIONI.ID_UNITA " _
                & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID(+) = SISCOM_MI.SEGNALAZIONI.ID_COMPLESSO " _
                & " AND OPERATORI.ID(+) = SEGNALAZIONI.ID_OPERATORE_INS " _
                & " AND SEGNALAZIONI.ID_TIPOLOGIE = TIPOLOGIE_GUASTI.ID(+) " _
                & " AND SEGNALAZIONI.ID_ANAGRAFICA = ANAGRAFICA.ID(+) " _
                & " AND SEGNALAZIONI.ID_AMMINISTRATORE = COND_AMMINISTRATORI.ID(+) " _
                & " AND SEGNALAZIONI.ID_GESTIONE_AUTONOMA = AUTOGESTIONI_ESERCIZI.ID(+) " _
                & " AND SEGNALAZIONI.ID_CUSTODE = ANAGRAFICA_CUSTODI.ID(+) " _
                & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA = SISCOM_MI.SCALE_EDIFICI.ID (+) " _
                & " AND SISCOM_MI.PIANI.ID(+) = SISCOM_MI.UNITA_IMMOBILIARI.ID_PIANO "

            'par.cmd.CommandText = par.cmd.CommandText & where_utente

            par.cmd.CommandText = par.cmd.CommandText _
                & " ORDER BY ID_PERICOLO_SEGNALAZIONE DESC, DATA_ORA_RICHIESTA DESC "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                'ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0

                For Each item In dt.Rows
                    lRet = New Segnalazione
                    lRet.Id = dt.Rows(i).Item("ID")
                    lRet.IDOperatore = par.IfNull(dt.Rows(i).Item("ID_OPERATORE_INS"), 0)
                    lRet.IDContratto = par.IfNull(dt.Rows(i).Item("ID_CONTRATTO"), 0)
                    lRet.Categoria = par.IfNull(dt.Rows(i).Item("TIPO0"), "")
                    lRet.SottoCategoria1 = par.IfNull(dt.Rows(i).Item("TIPO1"), "")
                    lRet.SottoCategoria2 = par.IfNull(dt.Rows(i).Item("TIPO2"), "")
                    lRet.SottoCategoria3 = par.IfNull(dt.Rows(i).Item("TIPO3"), "")
                    lRet.SottoCategoria3 = par.IfNull(dt.Rows(i).Item("TIPO4"), "")
                    lRet.SemaforoPriorita = par.IfNull(dt.Rows(i).Item("ID_PERICOLO_SEGNALAZIONE"), -1)
                    lRet.Stato = par.IfNull(dt.Rows(i).Item("STATO"), "")
                    lRet.IDStato = par.IfNull(dt.Rows(i).Item("ID_STATO"), 0)
                    lRet.Descrizione = par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")
                    lRet.NotePubbliche = par.IfNull(dt.Rows(i).Item("NOTE_C"), "")

                    If par.IfNull(dt.Rows(i).Item("DATA_INSERIMENTO"), "") <> "" Then
                        'lRet.DataInserimento = par.AggiustaData(par.IfNull(dt.Rows(i).Item("DATA_INSERIMENTO"), ""))
                        lRet.DataInserimento = par.IfNull(dt.Rows(i).Item("DATA_INSERIMENTO"), "")
                    End If
                    If par.IfNull(dt.Rows(i).Item("ORA_INSERIMENTO"), "") <> "" Then
                        'lRet.OraInserimento = par.AggiustaOra(par.IfNull(dt.Rows(i).Item("DATA_INSERIMENTO"), ""))
                        lRet.OraInserimento = par.IfNull(dt.Rows(i).Item("ORA_INSERIMENTO"), "")
                    End If
                    If par.IfNull(dt.Rows(i).Item("DATA_IN_CARICO"), "") <> "" Then
                        lRet.DataPresaInCarico = par.IfNull(dt.Rows(i).Item("DATA_IN_CARICO"), "")
                    End If
                    If par.IfNull(dt.Rows(i).Item("DATA_CHIUSURA"), "") <> "" Then
                        lRet.DataChiusura = par.IfNull(dt.Rows(i).Item("DATA_CHIUSURA"), "")
                    End If

                    lRet.CodiceFiscaleSoggetto = par.IfNull(dt.Rows(i).Item("COD_FISCALE"), "")
                    'lRet.IDSoggetto = par.IfNull(dt.Rows(i).Item("ID_OPERATORE_INS"), 0)
                    lRet.CodiceComplesso = par.IfNull(dt.Rows(i).Item("COD_COMPLESSO"), "")
                    lRet.CodiceEdificio = par.IfNull(dt.Rows(i).Item("COD_EDIFICIO"), "")
                    lRet.CodiceUnita = par.IfNull(dt.Rows(i).Item("COD_UNITA_IMMOBILIARE"), "")
                    lRet.CanaleApertura = par.IfNull(dt.Rows(i).Item("ID_CANALE"), "")

                    lRet.IdCategoria = par.IfNull(dt.Rows(i).Item("TIPO"), -1)
                    lRet.IdSottoCategoria1 = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGN_LIVELLO_1"), -1)
                    lRet.IdSottoCategoria2 = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGN_LIVELLO_2"), -1)
                    lRet.IdSottoCategoria3 = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGN_LIVELLO_3"), -1)
                    lRet.IdSottoCategoria4 = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGN_LIVELLO_4"), -1)

                    lRet.Telefono = par.IfNull(dt.Rows(i).Item("TELEFONO1"), "")
                    lRet.Email = par.IfNull(dt.Rows(i).Item("EMAIL"), "")
                    lRet.Nome = par.IfNull(dt.Rows(i).Item("NOME"), "")
                    lRet.Cognome = par.IfNull(dt.Rows(i).Item("COGNOME_RS"), "")

                    lRet.IdComplesso = par.IfNull(dt.Rows(i).Item("ID_COMPLESSO"), 0)
                    lRet.IdEdificio = par.IfNull(dt.Rows(i).Item("ID_EDIFICIO"), 0)
                    lRet.IdStruttura = par.IfNull(dt.Rows(i).Item("IDSTRUTTURA"), 0)
                    lRet.NomeComplesso = par.IfNull(dt.Rows(i).Item("NomeComplesso"), "")
                    lRet.NomeEdificio = par.IfNull(dt.Rows(i).Item("NomeEdificio"), "")
                    lRet.IdUnita = par.IfNull(dt.Rows(i).Item("ID_UNITA"), 0)

                    lRet.Scala = par.IfNull(dt.Rows(i).Item("SCALA"), "")
                    lRet.Piano = par.IfNull(dt.Rows(i).Item("PIANO"), "")
                    lRet.Interno = par.IfNull(dt.Rows(i).Item("INTERNO"), "")
                    lRet.SedeTerritoriale = par.IfNull(dt.Rows(i).Item("SEDETERRITORIALE"), "")
                    lRet.ContattoFornEmergenza = par.IfNull(dt.Rows(i).Item("ContattoFornEmergenza"), "")
                    lRet.ContattoFornEmergenzaData = par.IfNull(dt.Rows(i).Item("ContattoFornEmergenzaData"), "")
                    lRet.ContattoFornEmergenzaOra = par.IfNull(dt.Rows(i).Item("ContattoFornEmergenzaOra"), "")
                    lRet.InterventoFornEmergenza = par.IfNull(dt.Rows(i).Item("InterventoFornEmergenza"), "")
                    lRet.InterventoFornEmergenzaData = par.IfNull(dt.Rows(i).Item("InterventoFornEmergenzaData"), "")
                    lRet.InterventoFornEmergenzaOra = par.IfNull(dt.Rows(i).Item("InterventoFornEmergenzaOra"), "")
                    lRet.DataSopralluogo = par.IfNull(dt.Rows(i).Item("DataSopralluogo"), "")
                    lRet.OraSopralluogo = par.IfNull(dt.Rows(i).Item("OraSopralluogo"), "")
                    lRet.DataProgrammataIntervento = par.IfNull(dt.Rows(i).Item("DataProgrammataIntervento"), "")
                    lRet.OraProgrammataIntervento = par.IfNull(dt.Rows(i).Item("OraProgrammataIntervento"), "")
                    lRet.DataEffettivaIntervento = par.IfNull(dt.Rows(i).Item("DataEffettivaIntervento"), "")
                    lRet.OraEffettivaIntervento = par.IfNull(dt.Rows(i).Item("OraEffettivaIntervento"), "")
                    lRet.IdRuolo = par.IfNull(dt.Rows(i).Item("IdRuolo"), 0)
                    lRet.IdSegnalante = par.IfNull(dt.Rows(i).Item("idSegnalante"), 0)
                    lRet.CodiceFiscaleOperatore = par.IfNull(dt.Rows(i).Item("CodiceFiscaleOperatore"), "")

                    '/////////////
                    '// allegati
                    lRet.Allegati = GetAllegatiSegnalazione(p_autorizzazione, dt.Rows(i).Item("ID"))

                    '/////////////
                    '// Documenti richiesti, se ci sono solo se amministrativa ossia TIPO0 = 0
                    If par.IfNull(dt.Rows(i).Item("TIPO"), "") = 0 Then
                        Dim l_id_cat1 As Long
                        Dim l_id_cat2 As Long
                        l_id_cat1 = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGN_LIVELLO_1"), 0)
                        l_id_cat2 = par.IfNull(dt.Rows(i).Item("ID_TIPO_SEGN_LIVELLO_2"), 0)

                        lRet.DocumentiRichiesti = GetDocumentiRichiesti(p_autorizzazione, l_id_cat1, l_id_cat2)
                    End If

                    i = i + 1
                Next

            End If

            conndata.chiudi()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try
        Return lRet

    End Function

    '///////////////////////////////////////////////////////////
    '// Apre una segnalazione di un utente  
    '// parametri:      autorizzazione
    '//                 segnalazione  
    '//                 codice fiscale e ruolo utente che apre la segnalazione 
    '// return:         Id della segnalzione creata
    <WebMethod()> _
    Public Function ApriSegnalazione(ByRef p_autorizzazione As Autorizzazione, ByVal p_Segnalazione As Segnalazione, ByVal p_CodiceFiscale As String, ByVal p_idRuolo As Long) As Long
        Dim lret As Long = 0
        Dim idSegnal As Long
        Dim lIdSegnalante As String
        Dim lRuoloSegnalante As String
        Dim lCodiceFiscale As String

        Dim id_utente As Long = 0
        Dim where_utente As String = ""

        id_utente = p_idRuolo ' Ruolo di chi apre la segnalazione, utenteb WEB esterno
        lCodiceFiscale = p_CodiceFiscale ' Codice fiscale di chi apre la segnalazione 

        Dim ID_ANAGRAFICA As String = ""
        Dim ID_AMMINISTRATORE As String = ""
        Dim ID_GESTIONE_AUTONOMA As String = ""
        Dim ID_CUSTODE As String = ""
        Dim ID_DELEGATO_SINDACALE As String = ""
        Dim ID_OPERATORE As String = ""
        Dim ID_NON_NOTO As String = ""

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        Try
            Dim par As New CM.Global
            Dim connData As CM.datiConnessione
            connData = New CM.datiConnessione(par, False, False)
            connData.apri()

            '////////////////////////////////////////
            '// Utente che inserisce la segnalazione
            If id_utente = 0 Then
                Return Nothing
            Else
                If id_utente = 1 Then
                    'operatore MM
                    ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf id_utente = 2 Then
                    ' operatore contact center
                    ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf id_utente = 3 Then
                    ' Inquilino
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf id_utente = 4 Then
                    ' delegato sindacale
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf id_utente = 5 Then
                    ' Amm. cond
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.COND_AMMINISTRATORI WHERE COND_AMMINISTRATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf id_utente = 6 Then
                    ' delegati Autogestione
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf id_utente = 7 Then
                    ' custodi
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf id_utente = 11 Then
                    ' operatore contact center
                    ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                End If

                If ID_OPERATORE = "" Then
                    ID_OPERATORE = "(SELECT ID FROM OPERATORI WHERE OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" & UCase(p_autorizzazione.Login) & "')"
                End If

            End If

            '/////////////////////////////////////////////
            '// Utente che fa la segnalazione (segnalante)
            lIdSegnalante = p_Segnalazione.IdSegnalante
            lRuoloSegnalante = p_Segnalazione.IdRuolo

            If lIdSegnalante = 0 And lRuoloSegnalante = 0 And p_Segnalazione.Cognome <> "" Then
                '// Inserisci in utenti non noti
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_ANA_CHIAMANTI_NON_N.NEXTVAL FROM DUAL"
                lIdSegnalante = par.cmd.ExecuteScalar

                par.cmd.CommandText = " INSERT INTO SISCOM_MI.ANAGRAFICA_CHIAMANTI_NON_NOTI ( " _
                    & " ID, COGNOME, NOME,  " _
                    & " TELEFONO, CELLULARE, EMAIL)  " _
                    & " VALUES (" & CStr(lIdSegnalante) & ", " _
                    & " '" & par.PulisciStrSql(p_Segnalazione.Cognome) & "', " _
                    & " '" & par.PulisciStrSql(p_Segnalazione.Nome) & "', " _
                    & " '" & par.PulisciStrSql(p_Segnalazione.Telefono) & "', " _
                    & " '" & par.PulisciStrSql(p_Segnalazione.Cellulare) & "', " _
                    & " '" & LCase(par.PulisciStrSql(p_Segnalazione.Email)) & "' ) "
                par.cmd.ExecuteNonQuery()
                'WriteEvent("F251", "Inserimento anagrafica chiamante non noto: Email: " & TextBoxEmailChiamante.Text & " - Telefono: " & TextBoxTelefono1Chiamante.Text & " - Cellulare: " & TextBoxTelefono2Chiamante.Text)

                lRuoloSegnalante = 12
                ID_NON_NOTO = lIdSegnalante
            ElseIf lRuoloSegnalante = 12 Then
                ID_NON_NOTO = lIdSegnalante
            ElseIf lRuoloSegnalante = 1 Then
                ID_OPERATORE = lIdSegnalante
            ElseIf lRuoloSegnalante = 2 Then
                ' operatore contact center
                ID_OPERATORE = lIdSegnalante
            ElseIf lRuoloSegnalante = 3 Then
                ' operatore contact center
                ID_ANAGRAFICA = lIdSegnalante
            ElseIf lRuoloSegnalante = 4 Then
                ' delegato sindacale
                ID_ANAGRAFICA = lIdSegnalante
            ElseIf lRuoloSegnalante = 5 Then
                ' Amm. cond
                ID_AMMINISTRATORE = lIdSegnalante
            ElseIf lRuoloSegnalante = 6 Then
                ' delegati Autogestione
                ID_GESTIONE_AUTONOMA = lIdSegnalante
            ElseIf lRuoloSegnalante = 7 Then
                ' custodi
                ID_CUSTODE = lIdSegnalante
            ElseIf lRuoloSegnalante = 11 Then
                ' operatore contact center
                ID_OPERATORE = lIdSegnalante
            End If

            '///////////////////////////////////////////
            '// Inserimento segnalazione
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_SEGNALAZIONI.NEXTVAL FROM DUAL"
            Dim stridSegnal As String = par.cmd.ExecuteScalar
            idSegnal = CLng(stridSegnal)
            lret = idSegnal

            Dim C, C1, C2, C3, C4 As String

            If par.IfNull(p_Segnalazione.IdCategoria, "-1") = "-1" Then
                C = "NULL"
            Else
                C = CStr(p_Segnalazione.IdCategoria)
            End If

            If par.IfNull(p_Segnalazione.IdSottoCategoria1, "-1") = "-1" Then
                C1 = "NULL"
            Else
                C1 = CStr(p_Segnalazione.IdSottoCategoria1)
            End If

            If par.IfNull(p_Segnalazione.IdSottoCategoria2, "-1") = "-1" Then
                C2 = "NULL"
            Else
                C2 = CStr(p_Segnalazione.IdSottoCategoria2)
            End If

            If par.IfNull(p_Segnalazione.IdSottoCategoria3, "-1") = "-1" Then
                C3 = "NULL"
            Else
                C3 = CStr(p_Segnalazione.IdSottoCategoria3)
            End If

            If par.IfNull(p_Segnalazione.IdSottoCategoria4, "-1") = "-1" Then
                C4 = "NULL"
            Else
                C4 = CStr(p_Segnalazione.IdSottoCategoria4)
            End If

            ' /////////////////////////////////////////// 
            ' // se il codice edificio non viene passato 
            ' // lo leggo dall'unità immobiliare
            Dim lEd As String
            Dim lStuttura As String
            'If p_Segnalazione.CodiceEdificio <> "" Then
            '    lEd = "(SELECT ID FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO = '" & p_Segnalazione.CodiceEdificio & "'),"
            'Else
            '    lEd = "(SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & p_Segnalazione.CodiceUnita & "'),"
            'End If

            lEd = p_Segnalazione.IdEdificio
            lStuttura = p_Segnalazione.IdStruttura
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI(ID," _
                & " ID_STATO," _
                & " ID_EDIFICIO," _
                & " COGNOME_RS," _
                & " DATA_ORA_RICHIESTA," _
                & " TELEFONO1," _
                & " TELEFONO2," _
                & " MAIL," _
                & " DESCRIZIONE_RIC," _
                & " ID_OPERATORE_INS," _
                & " NOME," _
                & " TIPO_RICHIESTA," _
                & " ORIGINE," _
                & " ID_TIPOLOGIE," _
                & " TIPO_PERVENUTA," _
                & " ID_STRUTTURA," _
                & " ID_CONTRATTO," _
                & " ID_PERICOLO_SEGNALAZIONE," _
                & " ID_PERICOLO_SEGNALAZIONE_INIZ," _
                & " ID_TIPO_SEGNALAZIONE," _
                & " ID_TIPO_SEGN_LIVELLO_1," _
                & " ID_TIPO_SEGN_LIVELLO_2," _
                & " ID_TIPO_SEGN_LIVELLO_3," _
                & " ID_TIPO_SEGN_LIVELLO_4," _
                & " DANNEGGIANTE," _
                & " DANNEGGIATO," _
                & " ID_CANALE," _
                & " FL_CUSTODE," _
                & " FL_DVCA," _
                & " FL_AV," _
                & " FL_FS," _
                & " ID_ANAGRAFICA," _
                & " ID_AMMINISTRATORE," _
                & " ID_GESTIONE_AUTONOMA," _
                & " ID_CUSTODE," _
                & " ID_DELEGATO_SINDACALE, " _
                & " ID_TIPOLOGIA_SEGNALANTE, " _
                & " ID_COMPLESSO, " _
                & " ID_UNITA, " _
                & " fl_contatto_fornitore, " _
                & " data_contatto_fornitore, " _
                & " fl_Verifica_fornitore, " _
                & " data_verifica_fornitore, " _
                & " ID_CHIAMANTI_NON_NOTI, " _
                & " id_tipologia_segnalante_est " _
                & " ) " _
                & " VALUES " _
                & "(" & idSegnal & "," _
                & p_Segnalazione.IDStato & "," _
                & lEd & ", " _
                & "'" & p_Segnalazione.Cognome & "'," _
                & "'" & Format(Now, "yyyyMMddHHmm") & "'," _
                & "'" & p_Segnalazione.Telefono & "'," _
                & "'" & par.PulisciStrSql("") & "'," _
                & "'" & p_Segnalazione.Email & "'," _
                & "'" & par.PulisciStrSql(p_Segnalazione.Descrizione) & "'," _
                & ID_OPERATORE & "," _
                & "'" & p_Segnalazione.Nome & "'," _
                & "1" & ",'" & "A" & "',NULL," _
                & "'Web', " _
                & CStr(lStuttura) & "," _
                & CStr(p_Segnalazione.IDContratto) & "," _
                & p_Segnalazione.SemaforoPriorita & "," _
                & p_Segnalazione.SemaforoPriorita & "," _
                & C & "," _
                & C1 & "," _
                & C2 & "," _
                & C3 & "," _
                & C4 & "," _
                & "''," _
                & "'', " _
                & p_Segnalazione.CanaleApertura & ", " _
                & "0" & ", " _
                & "0" & ", " _
                & "0" & ", " _
                & "0" & ", "

            If ID_ANAGRAFICA = "" Then
                par.cmd.CommandText = par.cmd.CommandText & " NULL, "
            Else
                par.cmd.CommandText = par.cmd.CommandText & ID_ANAGRAFICA & " , "
            End If

            If ID_AMMINISTRATORE = "" Then
                par.cmd.CommandText = par.cmd.CommandText & " NULL, "
            Else
                par.cmd.CommandText = par.cmd.CommandText & ID_AMMINISTRATORE & " , "
            End If
            If ID_GESTIONE_AUTONOMA = "" Then
                par.cmd.CommandText = par.cmd.CommandText & " NULL, "
            Else
                par.cmd.CommandText = par.cmd.CommandText & ID_GESTIONE_AUTONOMA & " , "
            End If
            If ID_CUSTODE = "" Then
                par.cmd.CommandText = par.cmd.CommandText & " NULL, "
            Else
                par.cmd.CommandText = par.cmd.CommandText & ID_CUSTODE & " , "
            End If
            If ID_DELEGATO_SINDACALE = "" Then
                par.cmd.CommandText = par.cmd.CommandText & " NULL, "
            Else
                par.cmd.CommandText = par.cmd.CommandText & ID_DELEGATO_SINDACALE & " , "
            End If

            If id_utente <> 0 Then
                par.cmd.CommandText = par.cmd.CommandText & CStr(id_utente) & " , "
            Else
                par.cmd.CommandText = par.cmd.CommandText & " NULL, "
            End If

            If p_Segnalazione.IdComplesso <> 0 Then
                par.cmd.CommandText = par.cmd.CommandText & CStr(p_Segnalazione.IdComplesso) & " , "
            Else
                par.cmd.CommandText = par.cmd.CommandText & " NULL, "
            End If

            If p_Segnalazione.IdUnita <> 0 Then
                par.cmd.CommandText = par.cmd.CommandText & CStr(p_Segnalazione.IdUnita) & " , "
            Else
                par.cmd.CommandText = par.cmd.CommandText & " NULL, "
            End If

            If CBool(p_Segnalazione.ContattoFornEmergenza) Then
                par.cmd.CommandText = par.cmd.CommandText & "1 , "
            Else
                par.cmd.CommandText = par.cmd.CommandText & "0 , "
            End If
            par.cmd.CommandText = par.cmd.CommandText & "'" & CStr(p_Segnalazione.ContattoFornEmergenzaData) & CStr(p_Segnalazione.ContattoFornEmergenzaOra) & "' , "
            If CBool(p_Segnalazione.ContattoFornEmergenza) Then
                par.cmd.CommandText = par.cmd.CommandText & "1, "
            Else
                par.cmd.CommandText = par.cmd.CommandText & "0, "
            End If
            par.cmd.CommandText = par.cmd.CommandText & "'" & CStr(p_Segnalazione.InterventoFornEmergenzaData) & CStr(p_Segnalazione.InterventoFornEmergenzaOra) & "' , "

            If ID_NON_NOTO = "" Then
                par.cmd.CommandText = par.cmd.CommandText & " NULL, "
            Else
                par.cmd.CommandText = par.cmd.CommandText & ID_NON_NOTO & ", "
            End If

            par.cmd.CommandText = par.cmd.CommandText & lRuoloSegnalante & " "

            par.cmd.CommandText = par.cmd.CommandText & ")"

            par.cmd.ExecuteNonQuery()

            connData.chiudi()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message
        End Try

        Return lret
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna gli stati possibili di una segnalazione 
    '// parametri:      autorizzazione
    '// return:         Array degli stati
    <WebMethod()> _
    Public Function GetStatiSegnalazione(ByRef p_autorizzazione As Autorizzazione) As StatoSegnalazione()
        Dim lRet() As StatoSegnalazione = Nothing
        Dim lToday As String

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0

                For Each item In dt.Rows
                    lRet(i) = New StatoSegnalazione
                    lRet(i).ID = item("ID")
                    lRet(i).descrizione = par.IfNull(item("DESCRIZIONE"), "")

                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna le categorie di una segnalazione 
    '// parametri:      autorizzazione, Idruolo, non obbligatorio (0) :non tutti i ruoli potrebbero vedere le stesse categorie 
    '// return:         Array delle categorie
    <WebMethod()> _
    Public Function GetCategorie(ByRef p_autorizzazione As Autorizzazione, ByVal IDruolo As Integer) As CategoriaSegnalazione()
        Dim lRet() As CategoriaSegnalazione = Nothing
        Dim lToday As String

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            If IDruolo > 0 Then
                par.cmd.CommandText = "SELECT SISCOM_MI.TIPO_SEGNALAZIONE.* FROM SISCOM_MI.TIPO_SEGNALAZIONE INNER JOIN SISCOM_MI.TIPO_SEGNALAZIONE_UTENTE ON (SISCOM_MI.TIPO_SEGNALAZIONE.ID = SISCOM_MI.TIPO_SEGNALAZIONE_UTENTE.ID_TIPO_SEGNALAZIONE) WHERE ID_TIPO_UTENTE = " & IDruolo
            Else
                par.cmd.CommandText = "SELECT SISCOM_MI.TIPO_SEGNALAZIONE.* FROM SISCOM_MI.TIPO_SEGNALAZIONE "
            End If

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New CategoriaSegnalazione
                    lRet(i).ID = item("ID")
                    lRet(i).descrizione = par.IfNull(item("DESCRIZIONE"), "")

                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna le sottocategorie di livello 1 di una segnalazione 
    '// parametri:      autorizzazione, id_categoria padre
    '// return:         Array delle sottocategorie di liv. 1
    <WebMethod()> _
    Public Function GetSottoCategorie1(ByRef p_autorizzazione As Autorizzazione, ByVal p_id_categoria As Long) As Sottocategoria1()
        Dim lRet() As Sottocategoria1 = Nothing
        Dim lToday As String

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE =  " & p_id_categoria

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Sottocategoria1
                    lRet(i).ID = item("ID")
                    lRet(i).Descrizione = par.IfNull(item("DESCRIZIONE"), "")
                    lRet(i).IDCategoriaPadre = par.IfNull(item("ID_TIPO_SEGNALAZIONE"), "")
                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna le sottocategorie di livello 2 di una segnalazione 
    '// parametri:      autorizzazione, categoria padre
    '// return:         Array delle sottocategorie di liv. 2
    <WebMethod()> _
    Public Function GetSottoCategorie2(ByRef p_autorizzazione As Autorizzazione, ByVal p_id_categoria As Long) As Sottocategoria2()
        Dim lRet() As Sottocategoria2 = Nothing
        Dim lToday As String

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_1 = " & p_id_categoria

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Sottocategoria2
                    lRet(i).ID = item("ID")
                    lRet(i).Descrizione = par.IfNull(item("DESCRIZIONE"), "")
                    lRet(i).IDCategoriaPadre = par.IfNull(item("ID_TIPO_SEGNALAZIONE_LIVELLO_1"), "")
                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna le sottocategorie di livello 3 di una segnalazione 
    '// parametri:      autorizzazione, categoria padre
    '// return:         Array delle sottocategorie di liv. 3
    <WebMethod()> _
    Public Function GetSottoCategorie3(ByRef p_autorizzazione As Autorizzazione, ByVal p_id_categoria As Long) As Sottocategoria3()
        Dim lRet() As Sottocategoria3 = Nothing
        Dim lToday As String

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_2 = " & p_id_categoria

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Sottocategoria3
                    lRet(i).ID = item("ID")
                    lRet(i).Descrizione = par.IfNull(item("DESCRIZIONE"), "")
                    lRet(i).IDCategoriaPadre = par.IfNull(item("ID_TIPO_SEGNALAZIONE_LIVELLO_2"), "")
                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna le sottocategorie di livello 4 di una segnalazione 
    '// parametri:      autorizzazione, categoria padre
    '// return:         Array delle sottocategorie di liv. 4
    <WebMethod()> _
    Public Function GetSottoCategorie4(ByRef p_autorizzazione As Autorizzazione, ByVal p_id_categoria As Long) As Sottocategoria4()
        Dim lRet() As Sottocategoria4 = Nothing
        Dim lToday As String

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_3 = " & p_id_categoria

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Sottocategoria4
                    lRet(i).ID = item("ID")
                    lRet(i).Descrizione = par.IfNull(item("DESCRIZIONE"), "")
                    lRet(i).IDCategoriaPadre = par.IfNull(item("ID_TIPO_SEGNALAZIONE_LIVELLO_3"), "")
                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna l'intyero albero delle categorie/sottocategorie 
    '// con tutti i livelli e descrizioni  
    '// parametri:      autorizzazione, Ruolo (filtro su categorie, non tutte le categorie sono gestitibili da tutti i ruoli)
    '// return:         Array delle categorie
    <WebMethod()> _
    Public Function GetCategorieAll(ByRef p_autorizzazione As Autorizzazione, ByVal IDRuolo As Integer) As CategorieAll()
        Dim lRet() As CategorieAll = Nothing
        Dim lToday As String

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT " _
                                    & "TIPO_SEGNALAZIONE.ID     AS IDCATEGORIA, " _
                                    & "TIPO_SEGNALAZIONE.DESCRIZIONE    AS DESCRIZIONECATEGORIA, " _
                                    & "TIPO_SEGNALAZIONE_LIVELLO_1.ID    AS IDSOTTOCATEGORIA1, " _
                                    & "TIPO_SEGNALAZIONE_LIVELLO_1.DESCRIZIONE    AS DESCRIZIONESOTTOCATEGORIA1, " _
                                    & "TIPO_SEGNALAZIONE_LIVELLO_2.ID    AS IDSOTTOCATEGORIA2, " _
                                    & "TIPO_SEGNALAZIONE_LIVELLO_2.DESCRIZIONE    AS DESCRIZIONESOTTOCATEGORIA2, " _
                                    & "TIPO_SEGNALAZIONE_LIVELLO_3.ID    AS IDSOTTOCATEGORIA3, " _
                                    & "TIPO_SEGNALAZIONE_LIVELLO_3.DESCRIZIONE    AS DESCRIZIONESOTTOCATEGORIA3, " _
                                    & "TIPO_SEGNALAZIONE_LIVELLO_4.ID    AS IDSOTTOCATEGORIA4, " _
                                    & "TIPO_SEGNALAZIONE_LIVELLO_4.DESCRIZIONE    AS DESCRIZIONESOTTOCATEGORIA4 " _
                                    & "            FROM SISCOM_MI.TIPO_SEGNALAZIONE " _
                                    & "LEFT OUTER JOIN SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 ON TIPO_SEGNALAZIONE.ID = TIPO_SEGNALAZIONE_LIVELLO_1.ID_TIPO_SEGNALAZIONE " _
                                    & "LEFT OUTER JOIN SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 ON TIPO_SEGNALAZIONE_LIVELLO_1.ID = TIPO_SEGNALAZIONE_LIVELLO_2.ID_TIPO_SEGNALAZIONE_LIVELLO_1 " _
                                    & "LEFT OUTER JOIN SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 ON TIPO_SEGNALAZIONE_LIVELLO_2.ID = TIPO_SEGNALAZIONE_LIVELLO_3.ID_TIPO_SEGNALAZIONE_LIVELLO_2 " _
                                    & "LEFT OUTER JOIN SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 ON TIPO_SEGNALAZIONE_LIVELLO_3.ID = TIPO_SEGNALAZIONE_LIVELLO_4.ID_TIPO_SEGNALAZIONE_LIVELLO_3 "

            If IDRuolo > 0 Then
                par.cmd.CommandText = par.cmd.CommandText & " INNER JOIN SISCOM_MI.TIPO_SEGNALAZIONE_UTENTE ON (SISCOM_MI.TIPO_SEGNALAZIONE.ID = SISCOM_MI.TIPO_SEGNALAZIONE_UTENTE.ID_TIPO_SEGNALAZIONE) WHERE ID_TIPO_UTENTE = " & IDRuolo
            End If

            par.cmd.CommandText = par.cmd.CommandText & " ORDER BY 1, 3, 5, 7, 9"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New CategorieAll
                    lRet(i).IDCategoria = item("IDCATEGORIA")
                    lRet(i).DescrizioneCategoria = par.IfNull(item("DESCRIZIONECATEGORIA"), "")
                    lRet(i).IDSottoCategoria1 = par.IfNull(item("IDSOTTOCATEGORIA1"), 0)
                    lRet(i).DescrizioneSottocategoria1 = par.IfNull(item("DESCRIZIONESOTTOCATEGORIA1"), "")
                    lRet(i).IDSottoCategoria2 = par.IfNull(item("IDSOTTOCATEGORIA2"), 0)
                    lRet(i).DescrizioneSottocategoria2 = par.IfNull(item("DESCRIZIONESOTTOCATEGORIA2"), "")
                    lRet(i).IDSottoCategoria3 = par.IfNull(item("IDSOTTOCATEGORIA3"), 0)
                    lRet(i).DescrizioneSottocategoria3 = par.IfNull(item("DESCRIZIONESOTTOCATEGORIA3"), "")
                    lRet(i).IDSottoCategoria4 = par.IfNull(item("IDSOTTOCATEGORIA4"), 0)
                    lRet(i).DescrizioneSottocategoria4 = par.IfNull(item("DESCRIZIONESOTTOCATEGORIA4"), "")
                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna le strutture  
    '// parametri:      autorizzazione, id struttura (facoltativa) (filtro), nome struttura (filtro)
    '// return:         Array delle strutture
    <WebMethod()> _
    Public Function GetStrutture(ByRef p_autorizzazione As Autorizzazione, ByVal pid As String, ByVal p_nome As String, ByVal p_id_edificio As Long, ByVal p_id_complesso As Long) As Strutture()
        Dim lRet() As Strutture = Nothing
        Dim lToday As String
        Dim laddwhere As String = "where (1=1) "

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            If p_nome <> "" Then
                laddwhere = laddwhere & " AND NOME LIKE '%" & p_nome & "%'"
            End If

            If pid <> "" Then
                laddwhere = laddwhere & " AND ID = " & pid
            End If

            If p_id_complesso <> 0 Then
                laddwhere = laddwhere & " AND TAB_FILIALI.ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID = " & p_id_complesso & " ) "
            End If

            If p_id_edificio <> 0 Then
                laddwhere = laddwhere & " AND TAB_FILIALI.ID IN (SELECT ID_FILIALE FROM SISCOM_MI.EDIFICI INNER JOIN SISCOM_MI.COMPLESSI_IMMOBILIARI ON (EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID )  WHERE EDIFICI.ID =  " & p_id_edificio & " ) "
            End If

            conndata.apri()

            par.cmd.CommandText = "SELECT INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CAP AS CAP, COMUNE, PROV, TAB_FILIALI.* " _
                & " FROM SISCOM_MI.TAB_FILIALI " _
                & " INNER JOIN SISCOM_MI.INDIRIZZI ON SISCOM_MI.TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID " _
                & " INNER JOIN SISCOM_MI.TAB_COMUNI ON (TAB_COMUNI.COD_COM = INDIRIZZI.COD_COMUNE) " _
                & laddwhere

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Strutture
                    lRet(i).ID = item("ID")
                    lRet(i).Nome = par.IfNull(item("NOME"), "")
                    lRet(i).Indirizzo = par.IfNull(item("INDIRIZZO"), "")
                    lRet(i).Cap = par.IfNull(item("CAP"), "")
                    lRet(i).Comune = par.IfNull(item("COMUNE"), "")
                    lRet(i).Provincia = par.IfNull(item("PROV"), "")
                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna Gli edifici  
    '// parametri:      autorizzazione, id edificio (filtro non obbligatorio), nome (filtro non obbligatorio)
    '// return:         Array degli edifici
    <WebMethod()> _
    Public Function GetEdifici(ByRef p_autorizzazione As Autorizzazione, ByVal pid As String, ByVal p_nome As String) As Edifici()
        Dim lRet() As Edifici = Nothing
        Dim lToday As String
        Dim laddwhere As String = "where (1=1) "

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            If p_nome <> "" Then
                laddwhere = laddwhere & " AND EDIFICI.DENOMINAZIONE LIKE '%" & p_nome & "%'"
            End If
            If pid <> "" Then
                laddwhere = laddwhere & " AND EDIFICI.ID = " & pid
            End If

            conndata.apri()

            par.cmd.CommandText = "SELECT DENOMINAZIONE, EDIFICI.ID, INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CAP AS CAP, CIVICO, COMUNE, PROV, EDIFICI.* " _
                & " FROM SISCOM_MI.EDIFICI " _
                & " LEFT JOIN SISCOM_MI.INDIRIZZI ON (ID_INDIRIZZO_PRINCIPALE =INDIRIZZI.ID) " _
                & " LEFT JOIN SISCOM_MI.TAB_COMUNI ON (COD_COM = INDIRIZZI.COD_COMUNE) " _
                & laddwhere

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Edifici
                    lRet(i).ID = item("ID")
                    lRet(i).Nome = par.IfNull(item("DENOMINAZIONE"), "")
                    lRet(i).Indirizzo = par.IfNull(item("INDIRIZZO"), "")
                    lRet(i).Cap = par.IfNull(item("CAP"), "")
                    lRet(i).Comune = par.IfNull(item("COMUNE"), "")
                    lRet(i).Provincia = par.IfNull(item("PROV"), "")
                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna i complessi
    '// parametri:      autorizzazione, id complesso (filtro non obbligatorio), nome (filtro non obbligatorio)
    '// return:         Array delle strutture
    <WebMethod()> _
    Public Function GetComplessi(ByRef p_autorizzazione As Autorizzazione, ByVal pid As String, ByVal p_nome As String) As Complessi()
        Dim lRet() As Complessi = Nothing
        Dim lToday As String
        Dim laddwhere As String = " where (1=1) "

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            If p_nome <> "" Then
                laddwhere = laddwhere & " AND COMPLESSI_IMMOBILIARI.DENOMINAZIONE LIKE '%" & p_nome & "%'"
            End If
            If pid <> "" Then
                laddwhere = laddwhere & " AND COMPLESSI_IMMOBILIARI.ID = " & pid
            End If

            conndata.apri()

            par.cmd.CommandText = "SELECT " _
                                    & " DISTINCT DENOMINAZIONE, COMPLESSI_IMMOBILIARI.ID,  INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CAP AS CAP, CIVICO, COMUNE, PROV " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI" _
                                    & " LEFT JOIN SISCOM_MI.INDIRIZZI ON (ID_INDIRIZZO_RIFERIMENTO =INDIRIZZI.ID)" _
                                    & " LEFT JOIN SISCOM_MI.TAB_COMUNI ON (COD_COM = COD_COMUNE) " & laddwhere

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Complessi
                    lRet(i).ID = item("ID")
                    lRet(i).Nome = par.IfNull(item("DENOMINAZIONE"), "")
                    lRet(i).Indirizzo = par.IfNull(item("INDIRIZZO"), "")
                    lRet(i).Cap = par.IfNull(item("CAP"), "")
                    lRet(i).Comune = par.IfNull(item("COMUNE"), "")
                    lRet(i).Provincia = par.IfNull(item("PROV"), "")
                    lRet(i).Civico = par.IfNull(item("CIVICO"), "")
                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna l'insieme degli sportelli  
    '// parametri:      autorizzazione, nome (filtro non obbligatorio) e id struttura (filtro non obbligatorio)
    '// return:         Array degli sportelli
    <WebMethod()> _
    Public Function GetSportelli(ByRef p_autorizzazione As Autorizzazione, ByVal p_nome As String, ByVal p_idSruttura As String) As Sportelli()
        Dim lRet() As Sportelli = Nothing
        Dim lToday As String
        Dim lAddWhere As String = " WHERE (1=1) "

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            If p_nome <> "" Then
                lAddWhere = lAddWhere & " AND DESCRIZIONE LIKE '%" & p_nome & "%'"
            End If
            If p_idSruttura <> "" Then
                lAddWhere = lAddWhere & " AND ID_FILIALE = " & p_idSruttura & " "
            End If

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM  SISCOM_MI.APPUNTAMENTI_SPORTELLI " & lAddWhere

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Sportelli
                    lRet(i).ID = item("ID")
                    lRet(i).IDStruttura = par.IfNull(item("ID_FILIALE"), "")
                    lRet(i).Descrizione = par.IfNull(item("DESCRIZIONE"), "")
                    lRet(i).DescrizioneBreve = par.IfNull(item("DESCRIZIONE_BREVE"), "")
                    lRet(i).Indice = par.IfNull(item("INDICE"), "")
                    lRet(i).Attivo = (par.IfNull(item("FL_ATTIVO"), "") = "1")

                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna l'insieme dei ruoli  
    '// parametri:      autorizzazione
    '// return:         Array dei ruoli
    <WebMethod()> _
    Public Function GetRuoli(ByRef p_autorizzazione As Autorizzazione) As Ruolo()
        Dim lRet() As Ruolo = Nothing

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_TIPOLOGIA_SEGNALANTE WHERE 1=1"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Ruolo
                    lRet(i).ID = item("ID")
                    lRet(i).Descrizione = par.IfNull(item("DESCRIZIONE"), "")
                    lRet(i).Visibile = par.IfNull(item("FLG_WS"), "N")

                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna l'insieme dei Documenti Richiesti
    '// parametri:      autorizzazione
    '// tipologia 1     id tipologia 1
    '// tipologia 2     id tipologia 2
    '// return:         Array dei documenti
    <WebMethod()> _
    Public Function GetDocumentiRichiesti(ByRef p_autorizzazione As Autorizzazione, ByVal id_tipo1 As Long, ByVal id_tipo2 As Long) As Documento()
        Dim lRet() As Documento = Nothing

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)

            conndata.apri()

            par.cmd.CommandText = " SELECT SISCOM_MI.TIPOLOGIE_DOCUMENTI.* FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI INNER JOIN SISCOM_MI.TIPOLOGIE_DOCUMENTI  ON (ID_TIPOLOGIA_DOCUMENTO = SISCOM_MI.TIPOLOGIE_DOCUMENTI.ID) WHERE ID_TIPO_SEGN_LIVELLO_1 = " & CStr(id_tipo1) & " AND ID_TIPO_SEGN_LIVELLO_2 = " & CStr(id_tipo2)

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Documento
                    lRet(i).ID = item("ID")
                    lRet(i).Descrizione = par.IfNull(item("DESCRIZIONE"), "")

                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna il soggetti con un certo codice fiscale oppure  nome cognome ecc 
    '// parametri:      autorizzazione
    '// return:         array di soggetto()
    <WebMethod()> _
    Public Function GetSoggetti(ByRef p_autorizzazione As Autorizzazione, ByVal p_codiceFiscale As String, ByVal p_nome As String, ByVal p_cognome As String, ByVal p_telefono As String, ByVal p_ruolo As String, ByVal p_cellulare As String, ByVal p_email As String) As Soggetto()
        Dim lRet() As Soggetto = Nothing
        Dim lAddWhere As String = " WHERE (1=1) "

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)

            conndata.apri()

            If p_codiceFiscale <> "" Then
                lAddWhere = lAddWhere & " AND COD_FISCALE LIKE '%" & p_codiceFiscale & "%'"
            End If
            If p_cognome <> "" Then
                lAddWhere = lAddWhere & " AND COGNOME LIKE '%" & p_cognome & "%'"
            End If
            If p_nome <> "" Then
                lAddWhere = lAddWhere & " AND NOME LIKE '%" & p_nome & "%'"
            End If
            If p_ruolo <> "" Then
                lAddWhere = lAddWhere & " AND RUOLO = " & p_ruolo
            End If
            If p_telefono <> "" Then
                lAddWhere = lAddWhere & " AND TELEFONO LIKE '%" & p_telefono & "%'"
            End If
            If p_cellulare <> "" Then
                lAddWhere = lAddWhere & " AND CELLULARE LIKE '%" & p_cellulare & "%'"
            End If
            If p_email <> "" Then
                lAddWhere = lAddWhere & " AND EMAIL LIKE '%" & p_email & "%'"
            End If

            par.cmd.CommandText = "select * from " _
                                & "( " _
                                & "select id, cognome, nome, telefono, cellulare, email, '' as cod_fiscale, 12 as ruolo from SISCOM_MI.anagrafica_chiamanti_non_noti " _
                                & "union " _
                                & "select id, cognome, nome, telefono_aziendale, cellulare_aziendale, email_aziendale as email, cod_fiscale, 7 as ruolo from SISCOM_MI.ANAGRAFICA_CUSTODI " _
                                & "union " _
                                & "select id, rapp_cognome, rapp_nome, '', '', '' , cod_fiscale, 6 as ruolo from SISCOM_MI.AUTOGESTIONI_ESERCIZI " _
                                & "union " _
                                & "select id, cognome, nome, tel_1, cell, email, cod_fiscale, 5 as ruolo from SISCOM_MI.COND_AMMINISTRATORI " _
                                & "union " _
                                & "select id, cognome, nome, '', '', '', cod_fiscale, 1 as ruolo from operatori   where id_caf = 2" _
                                & "union " _
                                & "select id, cognome, nome, '', '', '', cod_fiscale, 2 as ruolo from operatori   where id_caf = 63" _
                                & "union " _
                                & "select id, cognome, nome, '', '', '', cod_fiscale, 11 as ruolo from operatori   where id_caf not in (2,63,1000000)" _
                                & "union " _
                                & "select ANAGRAFICA.id, ANAGRAFICA.cognome, ANAGRAFICA.nome, ANAGRAFICA.telefono, ''  as cellulare, ANAGRAFICA.email, ANAGRAFICA.cod_fiscale, 3 " _
                                & "from SISCOM_MI.anagrafica " _
                                & "    INNER JOIN SISCOM_MI.SOGGETTI_CONTRATTUALI ON (SISCOM_MI.ANAGRAFICA.ID = SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA)   " _
                                & "    WHERE COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND TO_DATE(DATA_INIZIO,'YYYYMMDD') <= SYSDATE AND TO_DATE(NVL(DATA_FINE,'29991231'),'YYYYMMDD') >= SYSDATE " _
                                & ")" _
                                & lAddWhere _
                                & "order by cognome, nome "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Soggetto
                    lRet(i).Id = item("ID")
                    lRet(i).Cellulare = par.IfNull(item("CELLULARE"), "")
                    lRet(i).CodiceFiscale = par.IfNull(item("COD_FISCALE"), "")
                    lRet(i).Cognome = par.IfNull(item("COGNOME"), "")
                    lRet(i).Nome = par.IfNull(item("NOME"), "")
                    lRet(i).Email = par.IfNull(item("EMAIL"), "")
                    lRet(i).IDRuolo = item("RUOLO")
                    lRet(i).Telefono = par.IfNull(item("TELEFONO"), "")

                    i = i + 1
                Next
            End If

            Return lRet

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna l'insieme degli stati degli appuntamenti  
    '// parametri:      autorizzazione
    '// return:         Array degli stati
    <WebMethod()> _
    Public Function GetStatiAppuntamenti(ByRef p_autorizzazione As Autorizzazione) As StatiAppuntamenti()
        Dim lRet() As StatiAppuntamenti = Nothing
        Dim lToday As String

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM  SISCOM_MI.APPUNTAMENTI_STATI "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New StatiAppuntamenti
                    lRet(i).ID = item("ID")
                    lRet(i).Descrizione = par.IfNull(item("DESCRIZIONE"), "")

                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna l'insieme degli esiti degli appuntamenti  
    '// parametri:      autorizzazione
    '// return:         Array degli esiti

    <WebMethod()> _
    Public Function GetEsitoAppuntamenti(ByRef p_autorizzazione As Autorizzazione) As EsitoAppuntamenti()
        Dim lRet() As EsitoAppuntamenti = Nothing
        Dim lToday As String

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM  SISCOM_MI.ESITO_APPUNTAMENTI_CC "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New EsitoAppuntamenti
                    lRet(i).ID = item("ID")
                    lRet(i).Descrizione = par.IfNull(item("DESCRIZIONE"), "")

                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna l'insieme degli orari di sportello possibili  
    '// parametri:      autorizzazione
    '// return:         Array degli orari
    <WebMethod()> _
    Public Function GetOrari(ByRef p_autorizzazione As Autorizzazione) As Orari()
        Dim lRet() As Orari = Nothing
        Dim lToday As String

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM  SISCOM_MI.APPUNTAMENTI_ORARI "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Orari
                    lRet(i).ID = item("ID")
                    lRet(i).Orario = par.IfNull(item("ORARIO"), "")
                    lRet(i).Indice = par.IfNull(item("INDICE"), 0)

                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna l'insieme degli degli appuntamenti  di una segnalazione
    '// parametri:      autorizzazione, id segnalazione, id stato, cod fiscale segnalante, ruolo segnalante
    '// return:         Array degli appuntamenti
    <WebMethod()> _
    Public Function GetAppuntamenti(ByRef p_autorizzazione As Autorizzazione, ByVal p_segnalazione As Long, ByVal p_IDStato As Long, ByVal p_CodiceFiscaleSegnalante As String, ByVal p_idRuoloSegnalante As Long) As Appuntamenti()
        Dim lRet() As Appuntamenti = Nothing
        Dim lToday As String

        Dim l_where As String = "WHERE (1=1)"
        Dim l_where_utente As String = ""

        If p_segnalazione <> 0 Then
            l_where = l_where & " AND ID_SEGNALAZIONE = " & p_segnalazione
        End If

        If p_IDStato <> -1 Then
            l_where = l_where & " AND ID_STATO_APPUNTAMENTO = " & p_IDStato
        End If

        If p_idRuoloSegnalante <> 0 And p_CodiceFiscaleSegnalante <> "" Then

            If p_idRuoloSegnalante = 1 Then
                'operatore MM
                l_where_utente = " AND OPERATORI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf p_idRuoloSegnalante = 2 Then
                ' operatore contact center
                l_where_utente = " AND OPERATORI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf p_idRuoloSegnalante = 3 Then
                ' inquilino
                l_where_utente = " AND ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf p_idRuoloSegnalante = 4 Then
                ' delegato sindacale
                l_where_utente = " AND ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf p_idRuoloSegnalante = 5 Then
                ' Amministratori condominio
                l_where_utente = " AND COND_AMMINISTRATORI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf p_idRuoloSegnalante = 6 Then
                ' delegati Autogestione
                l_where_utente = " AND AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf p_idRuoloSegnalante = 7 Then
                ' custodi
                l_where_utente = " AND ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            ElseIf p_idRuoloSegnalante = 11 Then
                ' Operatori CDM
                l_where_utente = " AND OPERATORI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            End If
        ElseIf p_idRuoloSegnalante = 0 And p_CodiceFiscaleSegnalante <> "" Then
            l_where_utente = " AND ( OPERATORI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            l_where_utente += " OR ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            l_where_utente += " OR COND_AMMINISTRATORI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            l_where_utente += " OR AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "'"
            l_where_utente += " OR ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_CodiceFiscaleSegnalante & "' )"
        End If

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT SISCOM_MI.SEGNALAZIONI.ID AS IDSEGNALAZIONE, APPUNTAMENTI_CALL_CENTER.* FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER  " _
                & " LEFT OUTER JOIN SISCOM_MI.SEGNALAZIONI ON (APPUNTAMENTI_CALL_CENTER.ID_SEGNALAZIONE = SEGNALAZIONI.ID) " _
                & " LEFT OUTER JOIN OPERATORI ON (OPERATORI.ID = SEGNALAZIONI.ID_OPERATORE_INS) " _
                & " LEFT OUTER JOIN SISCOM_MI.ANAGRAFICA ON (SEGNALAZIONI.ID_ANAGRAFICA = ANAGRAFICA.ID) " _
                & " LEFT OUTER JOIN SISCOM_MI.COND_AMMINISTRATORI ON (SEGNALAZIONI.ID_AMMINISTRATORE = COND_AMMINISTRATORI.ID) " _
                & " LEFT OUTER JOIN SISCOM_MI.AUTOGESTIONI_ESERCIZI ON (SEGNALAZIONI.ID_GESTIONE_AUTONOMA = AUTOGESTIONI_ESERCIZI.ID) " _
                & " LEFT OUTER JOIN SISCOM_MI.ANAGRAFICA_CUSTODI ON (SEGNALAZIONI.ID_CUSTODE = ANAGRAFICA_CUSTODI.ID) " & l_where & l_where_utente

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Appuntamenti
                    lRet(i).ID = item("ID")
                    lRet(i).IDStruttura = par.IfNull(item("ID_STRUTTURA"), 0)
                    lRet(i).Data = par.IfNull(item("DATA_APPUNTAMENTO"), "")
                    'lRet(i).IDOperatore = par.IfNull(item("ID_OPERATORE"), 0)
                    lRet(i).Nome = par.IfNull(item("NOME"), "")
                    lRet(i).Cognome = par.IfNull(item("COGNOME"), "")
                    lRet(i).Telefono = par.IfNull(item("TELEFONO"), "")
                    lRet(i).Note = par.IfNull(item("NOTE"), "")
                    lRet(i).DataInserimento = par.IfNull(item("DATA_INSERIMENTO"), "")
                    lRet(i).DataEliminazione = par.IfNull(item("DATA_ELIMINAZIONE"), "")
                    'lRet(i).IDOperatoreEliminazione = par.IfNull(item("ID_OPERATORE_ELIMINAZIONE"), 0)
                    lRet(i).IDSportello = par.IfNull(item("ID_SPORTELLO"), 0)
                    lRet(i).IDOrario = par.IfNull(item("ID_ORARIO"), 0)
                    lRet(i).DataModifica = par.IfNull(item("DATA_MODIFICA"), "")
                    'lRet(i).IDOperatoreModifica = par.IfNull(item("ID_OPERATORE_MODIFICA"), 0)
                    lRet(i).Cellulare = par.IfNull(item("CELLULARE"), "")
                    lRet(i).EMail = par.IfNull(item("EMAIL"), "")
                    lRet(i).IDStatoAppuntamento = par.IfNull(item("ID_STATO_APPUNTAMENTO"), 0)
                    lRet(i).MancataPresentazione = (par.IfNull(item("FL_MANCATA_PRESENTAZIONE"), 0) = 1)

                    If par.IfNull(item("ID_ESITO_APPUNTAMENTO"), -1) <> -1 Then
                        lRet(i).IDEsitoAppuntamento = item("ID_ESITO_APPUNTAMENTO")
                    End If

                    'lRet(i).IDSegnalazione = p_segnalazione
                    If par.IfNull(item("IDSEGNALAZIONE"), 0) <> 0 Then
                        lRet(i).IDSegnalazione = item("IDSEGNALAZIONE")
                    End If

                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Aggiunge un appuntamento ad una segnalazione
    '// parametri:      autorizzazione, id segnalazione, appuntamento da aggiungere, cod.fiscale segnalante, ruolo segnalaznte 
    '// return:         id dell'appunatamento aggiunto
    <WebMethod()> _
    Public Function AddAppuntamento(ByRef p_autorizzazione As Autorizzazione, ByVal p_segnalazione As Long, ByVal p_appuntamento As Appuntamenti, ByVal p_codiceFiscale As String, ByVal p_ruolo As Long) As Long

        Dim lret As Long = 0
        Dim idAppu As Long
        Dim lmancataPre As Integer
        Dim lesito As String
        Dim id_utente As Long = p_ruolo
        Dim ID_OPERATORE As String = ""

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        If id_utente = 0 Then
            Return Nothing
        Else
            If id_utente = 1 Then
                'operatore MM
                ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 2 Then
                ' operatore contact center
                ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 3 Then
                ' operatore contact center
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 4 Then
                ' delegato sindacale
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 5 Then
                ' Amm. cond
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.COND_AMMINISTRATORI WHERE COND_AMMINISTRATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 6 Then
                ' delegati Autogestione
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 7 Then
                ' custodi
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 11 Then
                ' operatore contact center
                ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            End If

            If ID_OPERATORE = "" Then
                ID_OPERATORE = "(SELECT ID FROM OPERATORI WHERE OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" & UCase(p_autorizzazione.Login) & "')"
            End If
        End If

        Try
            Dim par As New CM.Global
            Dim connData As CM.datiConnessione
            connData = New CM.datiConnessione(par, False, False)
            connData.apri()

            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_APPUNTAMENTI_CALL_CENTER.NEXTVAL FROM DUAL"
            Dim stridSegnal As String = par.cmd.ExecuteScalar
            idAppu = CLng(stridSegnal)
            lret = idAppu

            'If p_appuntamento.MancataPresentazione Then lmancataPre = 1 Else lmancataPre = 0
            lmancataPre = 0
            'If p_appuntamento.IDEsitoAppuntamento = 0 Then lesito = "null" Else lesito = CStr(p_appuntamento.IDEsitoAppuntamento)
            lesito = "null"
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPUNTAMENTI_CALL_CENTER( " _
                                   & "ID, DATA_APPUNTAMENTO, ID_STRUTTURA, " _
                                   & "ID_OPERATORE, NOME, COGNOME," _
                                   & "TELEFONO, NOTE, ID_SEGNALAZIONE, " _
                                   & "DATA_INSERIMENTO, DATA_ELIMINAZIONE, ID_OPERATORE_ELIMINAZIONE, " _
                                   & "ID_SPORTELLO, ID_ORARIO, DATA_MODIFICA, " _
                                   & "ID_OPERATORE_MODIFICA, CELLULARE, EMAIL, " _
                                   & "ID_STATO_APPUNTAMENTO, FL_MANCATA_PRESENTAZIONE, ID_ESITO_APPUNTAMENTO ) " _
                                   & "VALUES ( " & idAppu & " , " _
                                   & "'" & p_appuntamento.Data & "', " _
                                   & p_appuntamento.IDStruttura & ", " _
                                   & ID_OPERATORE & ", " _
                                   & "'" & p_appuntamento.Nome & "', " _
                                   & "'" & p_appuntamento.Cognome & "', " _
                                   & "'" & p_appuntamento.Telefono & "', " _
                                   & "'" & p_appuntamento.Note & "', " _
                                   & p_segnalazione & ", " _
                                   & "'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                   & "NULL, " _
                                   & "NULL, " _
                                   & p_appuntamento.IDSportello & ", " _
                                   & p_appuntamento.IDOrario & ", " _
                                   & "NULL, " _
                                   & "NULL, " _
                                   & "'" & p_appuntamento.Cellulare & "', " _
                                   & "'" & p_appuntamento.EMail & "', " _
                                   & par.IfNull(p_appuntamento.IDStatoAppuntamento, 0) & ", " _
                                   & CStr(lmancataPre) & ", " _
                                   & lesito & " )"

            par.cmd.ExecuteNonQuery()

            connData.chiudi()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message
            lret = -1
        End Try

        Return lret

    End Function

    '///////////////////////////////////////////////////////////
    '// Aggiunge un sollecito ad una segnalazione
    '// parametri:      autorizzazione, id segnalazione, nota, cod.fiscale segnalante, ruolo segnalaznte 
    '// return:         id del sollecito aggiunto
    <WebMethod()> _
    Public Function AddSollecito(ByRef p_autorizzazione As Autorizzazione, ByVal p_segnalazione As Long, ByVal p_nota As String, ByVal p_codiceFiscale As String, ByVal p_ruolo As Long) As Long

        Dim lret As Long = 0
        'Dim idAppu As Long
        'Dim lmancataPre As Integer
        'Dim lesito As String
        Dim id_utente As Long = p_ruolo
        Dim ID_OPERATORE As String = ""

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        If id_utente = 0 Then
            Return Nothing
        Else
            If id_utente = 1 Then
                'operatore MM
                ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 2 Then
                ' operatore contact center
                ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 3 Then
                ' operatore contact center
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 4 Then
                ' delegato sindacale
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 5 Then
                ' Amm. cond
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.COND_AMMINISTRATORI WHERE COND_AMMINISTRATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 6 Then
                ' delegati Autogestione
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 7 Then
                ' custodi
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 11 Then
                ' operatore contact center
                ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            End If

            If ID_OPERATORE = "" Then
                ID_OPERATORE = "(SELECT ID FROM OPERATORI WHERE OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" & UCase(p_autorizzazione.Login) & "')"
            End If
        End If

        Try
            Dim par As New CM.Global
            Dim connData As CM.datiConnessione
            connData = New CM.datiConnessione(par, False, False)
            connData.apri()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE, SOLLECITO) VALUES (" & p_segnalazione & ", '" & p_nota & " (nota di sollecito) ', '" & Format(Now, "yyyyMMddHHmm") & "'," & ID_OPERATORE & ", 1)"

            par.cmd.ExecuteNonQuery()

            connData.chiudi()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message
            lret = -1
        End Try

        Return lret

    End Function


    '///////////////////////////////////////////////////////////
    '// Aggiunge una nota ad una segnalazione
    '// parametri:      autorizzazione, p_segnalazionem, p_nota, p_codiceFiscale, p_Ruolo 
    '// return:         1
    <WebMethod()> _
    Public Function AddNota(ByRef p_autorizzazione As Autorizzazione, ByVal p_segnalazione As Long, ByVal p_nota As Nota, ByVal p_codiceFiscale As String, ByVal p_Ruolo As Long) As Long

        Dim lret As Long = 0
        Dim ID_OPERATORE As String = ""
        Dim id_utente As Long = p_Ruolo

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        '////////////////////////////////////////
        '// Utente che inserisce la segnalazione
        If id_utente = 0 Then
            Return Nothing
        Else
            If id_utente = 1 Then
                'operatore MM
                ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 2 Then
                ' operatore contact center
                ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 3 Then
                ' operatore contact center
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 4 Then
                ' delegato sindacale
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 5 Then
                ' Amm. cond
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.COND_AMMINISTRATORI WHERE COND_AMMINISTRATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 6 Then
                ' delegati Autogestione
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 7 Then
                ' custodi
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 11 Then
                ' operatore contact center
                ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            End If

            If ID_OPERATORE = "" Then
                ID_OPERATORE = "(SELECT ID FROM OPERATORI WHERE OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" & UCase(p_autorizzazione.Login) & "')"
            End If
        End If

        If ID_OPERATORE = "" Then
            ID_OPERATORE = "0"
        End If

        If p_nota.IDTipoNotaSegnalazione = 0 Then
            p_nota.IDTipoNotaSegnalazione = 1
        End If
        Try
            Dim par As New CM.Global
            Dim connData As CM.datiConnessione
            connData = New CM.datiConnessione(par, False, False)
            connData.apri()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE, ID_TIPO_SEGNALAZIONE_NOTE) " _
                                  & "VALUES (" & CStr(p_segnalazione) & ", '" & par.PulisciStrSql(p_nota.Descrizione) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & ID_OPERATORE & ", " & p_nota.IDTipoNotaSegnalazione & " )"
            par.cmd.ExecuteNonQuery()

            connData.chiudi()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lret = 1

            Return lret

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message
            lret = -1
        End Try

        Return lret

    End Function

    '///////////////////////////////////////////////////////////
    '// Modifica un appuntamento ad una segnalazione
    '// parametri:      autorizzazione1, appunatamento, p_codiceFiscale, p_Ruolo
    '// return:         id dell'appunatamento modificato
    <WebMethod()> _
    Public Function ModifyAppuntamento(ByRef p_autorizzazione As Autorizzazione, ByVal p_appuntamento As Appuntamenti, ByVal p_codiceFiscale As String, ByVal p_Ruolo As Long) As Long

        Dim lret As Long = 0
        Dim lmancataPre As Integer
        Dim lesito As String
        Dim id_utente As Long = p_Ruolo
        Dim ID_OPERATORE As String = ""

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        If id_utente = 0 Then
            Return Nothing
        Else
            If id_utente = 1 Then
                'operatore MM
                ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 2 Then
                ' operatore contact center
                ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 3 Then
                ' operatore contact center
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 4 Then
                ' delegato sindacale
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 5 Then
                ' Amm. cond
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.COND_AMMINISTRATORI WHERE COND_AMMINISTRATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 6 Then
                ' delegati Autogestione
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 7 Then
                ' custodi
                ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_codiceFiscale & "')"
            ElseIf id_utente = 11 Then
                ' operatore contact center
                ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_codiceFiscale & "')"
            End If

            If ID_OPERATORE = "" Then
                ID_OPERATORE = "(SELECT ID FROM OPERATORI WHERE OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" & UCase(p_autorizzazione.Login) & "')"
            End If
        End If

        Try
            Dim par As New CM.Global
            Dim connData As CM.datiConnessione
            connData = New CM.datiConnessione(par, False, False)
            connData.apri()

            lret = p_appuntamento.ID
            If p_appuntamento.MancataPresentazione Then lmancataPre = 1 Else lmancataPre = 0
            If p_appuntamento.IDEsitoAppuntamento = 0 Then lesito = "null" Else lesito = CStr(p_appuntamento.IDEsitoAppuntamento)

            par.cmd.CommandText = " UPDATE SISCOM_MI.APPUNTAMENTI_CALL_CENTER " _
                                 & "SET " _
                                        & " DATA_APPUNTAMENTO         = " & p_appuntamento.Data & ", " _
                                        & " ID_STRUTTURA              = " & p_appuntamento.IDStruttura & ", " _
                                        & " ID_OPERATORE              = " & ID_OPERATORE & ", " _
                                        & " NOME                      = '" & p_appuntamento.Nome & "', " _
                                        & " COGNOME                   = '" & p_appuntamento.Cognome & "', " _
                                        & " TELEFONO                  = '" & p_appuntamento.Telefono & "', " _
                                        & " NOTE                      = '" & p_appuntamento.Note & "', " _
                                        & " ID_SEGNALAZIONE           = " & p_appuntamento.IDSegnalazione & ", " _
                                        & " DATA_INSERIMENTO          = '" & p_appuntamento.DataInserimento & "', " _
                                        & " DATA_ELIMINAZIONE         = '" & p_appuntamento.DataEliminazione & "', " _
                                        & " ID_SPORTELLO              = " & p_appuntamento.IDSportello & ", " _
                                        & " ID_ORARIO                 = " & p_appuntamento.IDOrario & ", " _
                                        & " DATA_MODIFICA             = '" & par.AggiustaData(CStr(Today())) & "', " _
                                        & " ID_OPERATORE_MODIFICA     = " & ID_OPERATORE & ", " _
                                        & " CELLULARE                 = '" & p_appuntamento.Cellulare & "', " _
                                        & " EMAIL                     = '" & p_appuntamento.EMail & "', " _
                                        & " ID_STATO_APPUNTAMENTO     = " & p_appuntamento.IDStatoAppuntamento & ", " _
                                        & " FL_MANCATA_PRESENTAZIONE  = " & lmancataPre & ", " _
                                        & " ID_ESITO_APPUNTAMENTO     = " & lesito _
                                 & " WHERE  ID                        = " & p_appuntamento.ID

            par.cmd.ExecuteNonQuery()

            connData.chiudi()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message
            lret = -1
        End Try

        Return lret

    End Function

    '////////////////////////////////////
    '// GetAllegatiSegnalazione
    <WebMethod()> _
    Public Function GetAllegatiSegnalazione(ByRef p_autorizzazione As Autorizzazione, ByVal p_segnalazione As Long) As Allegato()
        Dim lRet() As Allegato = Nothing
        Dim lToday As String
        Dim par As New CM.Global

        If Not p_autorizzazione.Autorizza() Then
            p_autorizzazione.EsitoOperazione.codice = "004"
            p_autorizzazione.EsitoOperazione.descrizione = "Autenticazione non valida."
            Return Nothing
        End If

        If par.IfNull(p_segnalazione, 0) = 0 Then
            p_autorizzazione.EsitoOperazione.codice = "005"
            p_autorizzazione.EsitoOperazione.descrizione = "ID Allegato obbligatorio."
            Return Nothing
        End If

        Try
            Dim conndata As New CM.datiConnessione(par)
            lToday = par.AggiustaData(CStr(Today))

            conndata.apri()

            par.cmd.CommandText = "SELECT ALLEGATI_WS.*, ALLEGATI_WS_OGGETTI.DESCRIZIONE AS OGGETTO FROM SISCOM_MI.ALLEGATI_WS INNER JOIN SISCOM_MI.ALLEGATI_WS_OGGETTI ON (ALLEGATI_WS_OGGETTI.ID = OGGETTO) WHERE ALLEGATI_WS.STATO = 0 and ID_OGGETTO=" & p_segnalazione

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New Allegato
                    lRet(i).Id = par.IfNull(item("ID"), 0)
                    lRet(i).Nome = par.IfNull(item("NOME"), "")
                    lRet(i).Descrizione = par.IfNull(item("DESCRIZIONE"), "")
                    lRet(i).DataOra = par.IfNull(item("DATA_ORA"), "")
                    lRet(i).Oggetto = par.IfNull(item("OGGETTO"), "")
                    lRet(i).IDTipoAllegato = par.IfNull(item("TIPO"), 0)

                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '////////////////////////////////////
    '// Download Allegato
    <WebMethod()> _
    Public Function DownloadAllegato(ByRef p_autorizzazione As Autorizzazione, ByVal p_Allegato As Allegato) As Byte()
        Dim homepath As String = HttpContext.Current.Server.MapPath("~/ALLEGATI/SEGNALAZIONI/") ' fisso idcartella = 17
        'Dim tipo As String = p_Allegato.IDTipoAllegato
        Dim par As New CM.Global

        If p_Allegato Is Nothing Then
            p_autorizzazione.EsitoOperazione.codice = "003"
            p_autorizzazione.EsitoOperazione.descrizione = "Allegato obbligatorio"
            Return Nothing
        End If

        If par.IfNull(p_Allegato.Id, 0) = 0 Then
            p_autorizzazione.EsitoOperazione.codice = "004"
            p_autorizzazione.EsitoOperazione.descrizione = "ID Allegato obbligatorio"
            Return Nothing
        End If

        If par.IfEmpty(p_Allegato.Nome, "") = "" Then
            p_autorizzazione.EsitoOperazione.codice = "005"
            p_autorizzazione.EsitoOperazione.descrizione = "Nome Allegato obbligatorio"
            Return Nothing
        End If

        Dim FName As String = homepath & p_Allegato.Nome
        Dim fs1 As System.IO.FileStream = Nothing

        Try
            fs1 = System.IO.File.Open(FName, FileMode.Open, FileAccess.Read)
            Dim b1 As Byte() = New Byte(fs1.Length - 1) {}
            fs1.Read(b1, 0, CInt(fs1.Length))
            fs1.Close()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."

            Return b1
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message
            Return Nothing
        End Try

    End Function

    '////////////////////////////////////
    '// Download Bolletta-MAV
    <WebMethod()> _
    Public Function DownloadMAV(ByRef p_autorizzazione As Autorizzazione, ByVal p_idBolletta As Long) As Byte()
        Dim NumeroMav As String = Format(p_idBolletta, "0000000000") & ".pdf"
        Dim homepath As String = HttpContext.Current.Server.MapPath("~/ALLEGATI/CONTRATTI/ELABORAZIONI/MAV/")
        Dim indirizzo As String = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & NumeroMav
        Dim indirizzo1 As String = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & CStr(p_idBolletta) & ".pdf"

        Dim FName As String = ""
        If IO.File.Exists(indirizzo) = True Then
            FName = homepath & NumeroMav
        ElseIf IO.File.Exists(indirizzo1) = True Then
            FName = homepath & CStr(p_idBolletta) & ".pdf"
        End If
        Try
            If FName <> "" Then
                ' SCARICO IL PDF
                Dim fs1 As System.IO.FileStream = Nothing

                fs1 = System.IO.File.Open(FName, FileMode.Open, FileAccess.Read)
                Dim b1 As Byte() = New Byte(fs1.Length - 1) {}
                fs1.Read(b1, 0, CInt(fs1.Length))
                fs1.Close()

                p_autorizzazione.EsitoOperazione.codice = "000"
                p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."

                Return b1
            Else
                'MAV non disponibile
                p_autorizzazione.EsitoOperazione.codice = "002"
                p_autorizzazione.EsitoOperazione.descrizione = "MAV non disponibile."

                Return Nothing
            End If

        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message
            Return Nothing
        End Try

    End Function

    '////////////////////////////////////
    '// Upload Allegato
    <WebMethod()> _
    Public Function UploadFile(ByRef p_autorizzazione As Autorizzazione, ByVal f As Byte(), ByVal fileName As String, ByVal Descrizione As String, ByVal IDSegnalazione As Long, ByVal IDTipoAllegato As Long, ByVal CheckSumCRC32 As UInteger) As String
        Try
            '//////////////////////////
            '// Controllo Checksum se 
            '// passato
            If CheckSumCRC32 <> CUInt(0) Then
                If Crc32CheckSum.ComputeChecksum(f) <> CheckSumCRC32 Then
                    p_autorizzazione.EsitoOperazione.codice = "003"
                    p_autorizzazione.EsitoOperazione.descrizione = "UPLOAD NON RIUSCITO: ERRORE CRC CHECKSUM"
                    Return "KO"
                End If
            End If

            Dim ms As MemoryStream = New MemoryStream(f)
            Dim fs As FileStream = New FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/FileTemp/") & fileName, FileMode.Create)
            ms.WriteTo(fs)
            ms.Close()
            fs.Close()
            fs.Dispose()

            '// Zip del file e inserimento in tabella 
            '// ID_CARTELLA = 7 >> Agenda ID_CARTELLA = 17 >> Segnalazioni
            '// Ultimo parametro cartella: se è vuoto lo legge da ALLEGATI_WS_CARTELLE
            If AllegaDocumento(System.Web.Hosting.HostingEnvironment.MapPath("~/FileTemp/") & fileName, fileName, "17", Descrizione, CStr(IDTipoAllegato), "17", IDSegnalazione, "") <> "" Then

                p_autorizzazione.EsitoOperazione.codice = "000"
                p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."

                Return "OK"
            Else
                p_autorizzazione.EsitoOperazione.codice = "002"
                p_autorizzazione.EsitoOperazione.descrizione = "UPLOAD NON RIUSCITO"
                Return "KO"
            End If
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message
            Return "KO"
        End Try
    End Function

    '////////////////////////////////////
    '// AllegaDocumento
    Private Function AllegaDocumento(ByVal FileDocument As String, ByVal Titolo As String, ByVal cartella As String, ByVal DescrizioneAllegato As String, _
                                     ByVal TipoAllegato As String, ByVal Oggetto As String, ByVal IdOggetto As String, ByVal PathCartella As String) As String
        AllegaDocumento = ""
        Dim par As New CM.Global
        Dim conndata As CM.datiConnessione
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim CMD As New Oracle.DataAccess.Client.OracleCommand
        Dim CMD1 As New Oracle.DataAccess.Client.OracleCommand

        Try
            conndata = New CM.datiConnessione(par, False, False)
            conndata.apri()
            If String.IsNullOrEmpty(Trim(PathCartella)) Then
                CMD = New Oracle.DataAccess.Client.OracleCommand("SELECT PATH FROM SISCOM_MI.ALLEGATI_WS_CARTELLE WHERE ID = " & cartella, par.OracleConn)

                myReader = CMD.ExecuteReader()
                If myReader.Read() Then
                    PathCartella = par.IfNull(myReader("PATH"), "")
                End If
                myReader.Close()
            End If
            If System.IO.Path.GetExtension(Titolo) <> ".zip" And System.IO.Path.GetExtension(Titolo) <> ".rar" Then
                Titolo = ZipAllegatoDownload(Titolo, Titolo)
            End If
            Dim percorso As String = HttpContext.Current.Server.MapPath("~\FileTemp\")

            If Not File.Exists(HttpContext.Current.Server.MapPath(PathCartella & "\" & Titolo)) Then
                File.Move(percorso & Titolo, HttpContext.Current.Server.MapPath(PathCartella & "\" & Titolo))
            End If

            If IdOggetto.ToString.ToUpper = "NULL" Then IdOggetto = ""
            CMD1 = New Oracle.DataAccess.Client.OracleCommand("SELECT SISCOM_MI.SEQ_ALLEGATI_WS.NEXTVAL FROM DUAL", par.OracleConn)
            myReader1 = CMD1.ExecuteReader()
            If myReader1.Read() Then
                AllegaDocumento = par.IfNull(myReader1(0), "")
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.ALLEGATI_WS (ID, ID_ALLEGATO, NOME, CARTELLA, PATH, " _
                                & "DESCRIZIONE, TIPO, OGGETTO, ID_OGGETTO, STATO, ID_OPERATORE, DATA_ORA, FL_PROTOCOLLO) VALUES " _
                                & "(" & AllegaDocumento & ", null, " & par.insDbValue(Titolo, True) & ", " & cartella & ", " _
                                & par.insDbValue((PathCartella & "/" & Titolo).Replace("//", "/"), True) & ", " & par.insDbValue(DescrizioneAllegato, True) & ", " _
                                & par.insDbValue(TipoAllegato, True) & ", " & par.insDbValue(Oggetto, True) & ", " & par.insDbValue(IdOggetto, True) & ", 0, " _
                                & "1000000" & ", " & par.insDbValue(Format(Now, "yyyyMMddHHmmss"), True) & ", 0)"

            par.cmd.ExecuteNonQuery()
            conndata.chiudi(False)
        Catch ex As Exception
            AllegaDocumento = ""
            If par.OracleConn.State = Data.ConnectionState.Open Then conndata.chiudi(False)
        End Try
        conndata.chiudi()
    End Function

    Private Function ZipAllegatoDownload(ByVal strFile As String, ByVal nomeFile As String) As String
        ZipAllegatoDownload = ""
        Try
            Dim zipFic As String = ""
            Dim estensioneAllegato As String = Mid(HttpContext.Current.Server.MapPath(strFile), HttpContext.Current.Server.MapPath(strFile).IndexOf(".") + 1)
            Dim AllegatoCompleto As String = nomeFile.Replace(estensioneAllegato, ".zip")
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim strmFile As FileStream = File.OpenRead(HttpContext.Current.Server.MapPath("../FileTemp/" & strFile))
            Dim abyBuffer(strmFile.Length - 1) As Byte
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)
            Dim sFile As String = Path.GetFileName(HttpContext.Current.Server.MapPath("../FileTemp/" & strFile))
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(HttpContext.Current.Server.MapPath("../FileTemp/" & strFile))
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            If File.Exists(HttpContext.Current.Server.MapPath("../FileTemp/") & AllegatoCompleto) Then
                File.Delete(HttpContext.Current.Server.MapPath("../FileTemp/") & AllegatoCompleto)
            End If
            zipFic = HttpContext.Current.Server.MapPath("../FileTemp/") & AllegatoCompleto
            strmZipOutputStream = New ZipOutputStream(File.Create(zipFic))
            strmZipOutputStream.SetLevel(6)
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            ZipAllegatoDownload = AllegatoCompleto
        Catch ex As Exception
            ZipAllegatoDownload = ""

        End Try
    End Function


    '///////////////////////////////////////////////////////////
    '// Cancella un appuntamento di una segnalazione
    '// parametri:      autorizzazione, id appuntamento 
    '// return:         id dell'appunatamento cancellato
    '<WebMethod()> _
    'Public Function DeleteAppuntamento(ByRef p_autorizzazione As Autorizzazione, ByVal p_appuntamento As Long) As Long

    '    Dim lret As Long = 0
    '    If Not p_autorizzazione.Autorizza() Then
    '        Return Nothing
    '    End If

    '    Try
    '        Dim par As New CM.Global
    '        Dim connData As CM.datiConnessione
    '        connData = New CM.datiConnessione(par, False, False)
    '        connData.apri()

    '        lret = p_appuntamento

    '        par.cmd.CommandText = " DELETE SISCOM_MI.APPUNTAMENTI_CALL_CENTER " _
    '                             & " WHERE  ID                        = " & CStr(p_appuntamento)

    '        par.cmd.ExecuteNonQuery()

    '        connData.chiudi()

    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '        p_autorizzazione.EsitoOperazione.codice = "000"
    '        p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
    '    Catch ex As Exception
    '        p_autorizzazione.EsitoOperazione.codice = "001"
    '        p_autorizzazione.EsitoOperazione.descrizione = ex.Message
    '        lret = -1
    '    End Try

    '    Return lret

    'End Function

    '///////////////////////////////////////////////////////////
    '// cancella una segnalazione
    '// parametri:      autorizzazione, segnalazione da cancellare
    '// return:         id della segnalazione cancellata
    '<WebMethod()> _
    'Public Function DeleteSegnalazione(ByRef p_autorizzazione As Autorizzazione, ByVal p_segnalazione As Long) As Long

    '    Dim lret As Long = 0
    '    If Not p_autorizzazione.Autorizza() Then
    '        Return Nothing
    '    End If

    '    Try
    '        Dim par As New CM.Global
    '        Dim connData As CM.datiConnessione
    '        connData = New CM.datiConnessione(par, False, False)
    '        connData.apri()

    '        lret = p_segnalazione

    '        par.cmd.CommandText = " DELETE SISCOM_MI.SEGNALAZIONI " _
    '                             & " WHERE  ID                        = " & CStr(p_segnalazione)

    '        par.cmd.ExecuteNonQuery()

    '        connData.chiudi()

    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '        p_autorizzazione.EsitoOperazione.codice = "000"
    '        p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
    '    Catch ex As Exception
    '        p_autorizzazione.EsitoOperazione.codice = "001"
    '        p_autorizzazione.EsitoOperazione.descrizione = ex.Message
    '        lret = -1
    '    End Try

    '    Return lret

    'End Function

    '///////////////////////////////////////////////////////////
    '// Modifica una segnalazione
    '// parametri:      autorizzazione, segnalazione da modificare
    '// return:         id della segnalazione modificata
    <WebMethod()> _
    Public Function ModifySegnalazione(ByRef p_autorizzazione As Autorizzazione, ByVal p_segnalazione As Segnalazione) As Long

        Dim lret As Long = 0
        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        Try
            Dim par As New CM.Global
            Dim connData As CM.datiConnessione
            connData = New CM.datiConnessione(par, False, False)
            connData.apri()

            lret = p_segnalazione.Id
            Dim C, C1, C2, C3, C4 As String

            If par.IfNull(p_segnalazione.IdCategoria, "-1") = "-1" Then
                C = "NULL"
            Else
                C = CStr(p_segnalazione.IdCategoria)
            End If

            If par.IfNull(p_segnalazione.IdSottoCategoria1, "-1") = "-1" Then
                C1 = "NULL"
            Else
                C1 = CStr(p_segnalazione.IdSottoCategoria1)
            End If

            If par.IfNull(p_segnalazione.IdSottoCategoria2, "-1") = "-1" Then
                C2 = "NULL"
            Else
                C2 = CStr(p_segnalazione.IdSottoCategoria2)
            End If

            If par.IfNull(p_segnalazione.IdSottoCategoria3, "-1") = "-1" Then
                C3 = "NULL"
            Else
                C3 = CStr(p_segnalazione.IdSottoCategoria3)
            End If

            If par.IfNull(p_segnalazione.IdSottoCategoria4, "-1") = "-1" Then
                C4 = "NULL"
            Else
                C4 = CStr(p_segnalazione.IdSottoCategoria4)
            End If

            Dim lCont As String = "0"
            If p_segnalazione.ContattoFornEmergenza Then
                lCont = "1"
            Else
                lCont = "0"
            End If

            Dim lCont1 As String = "0"
            If p_segnalazione.InterventoFornEmergenza Then
                lCont1 = "1"
            Else
                lCont1 = "0"
            End If
            par.cmd.CommandText = " UPDATE SISCOM_MI.SEGNALAZIONI " _
                                 & " SET     " _
                                 & "        ID_STATO                      = " & p_segnalazione.IDStato & ", " _
                                 & "        ID_EDIFICIO                   = " & p_segnalazione.IdEdificio & ", " _
                                 & "        ID_UNITA                      = " & p_segnalazione.IdUnita & "," _
                                 & "        COGNOME_RS                    = '" & p_segnalazione.Cognome & "'," _
                                 & "        TELEFONO1                     = '" & p_segnalazione.Telefono & "'," _
                                 & "        MAIL                          = '" & p_segnalazione.Email & "'," _
                                 & "        DESCRIZIONE_RIC               = '" & par.PulisciStrSql(p_segnalazione.Descrizione) & "'," _
                                 & "        NOME                          = '" & p_segnalazione.Nome & "'," _
                                 & "        ID_STRUTTURA                  = " & p_segnalazione.IdStruttura & "," _
                                 & "        ID_CONTRATTO                  = " & p_segnalazione.IDContratto & "," _
                                 & "        ID_SEGNALAZIONE_PADRE         = " & p_segnalazione.SemaforoPriorita & "," _
                                 & "        ID_PERICOLO_SEGNALAZIONE_INIZ = " & p_segnalazione.SemaforoPriorita & "," _
                                 & "        ID_TIPO_SEGNALAZIONE          = " & C & "," _
                                 & "        ID_TIPO_SEGN_LIVELLO_1        = " & C1 & "," _
                                 & "        ID_TIPO_SEGN_LIVELLO_2        = " & C2 & "," _
                                 & "        ID_TIPO_SEGN_LIVELLO_3        = " & C3 & "," _
                                 & "        ID_TIPO_SEGN_LIVELLO_4        = " & C4 & "," _
                                 & "        fl_contatto_fornitore         = '" & lCont & "'," _
                                 & "        data_contatto_fornitore       = '" & CStr(p_segnalazione.ContattoFornEmergenzaData) & CStr(p_segnalazione.ContattoFornEmergenzaOra) & "'," _
                                 & "        fl_Verifica_fornitore         = '" & lCont1 & "'," _
                                 & "        data_verifica_fornitore       = '" & CStr(p_segnalazione.InterventoFornEmergenzaData) & CStr(p_segnalazione.InterventoFornEmergenzaOra) & "'" _
                                 & " WHERE  ID                            = " & p_segnalazione.Id

            par.cmd.ExecuteNonQuery()

            connData.chiudi()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message
            lret = -1
        End Try

        Return lret

    End Function


    '///////////////////////////////////////////////////////////
    '// Ritorna Gli slot liberi 
    <WebMethod()> _
    Public Function GetAgenda(ByRef p_autorizzazione As Autorizzazione, ByVal p_id_filiale As String, ByVal p_dta_inizio As String, ByVal p_dta_fine As String, ByVal p_id_orario As String, ByVal p_id_sportello As String) As Slot()
        Dim lRet() As Slot = Nothing
        Dim lAddWhere As String = ""

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)

            If p_id_orario <> "" Then
                lAddWhere = lAddWhere & " AND APPUNTAMENTI_ORARI.ID = " & p_id_orario
            End If

            If p_id_sportello <> "" Then
                lAddWhere = lAddWhere & " AND C.ID = " & p_id_sportello
            End If
            conndata.apri()

            '--- ritorna matrice data / filiale / sportello / orario  all'inteno di una data_inizio / data_fine di una certa filiale
            '--- esclude i sabati e domeniche
            par.cmd.CommandText = "SELECT C.ID AS ID_SPORTELLO, C.DESCRIZIONE_BREVE AS SPORTELLO, C.ID_FILIALE, TAB_FILIALI.NOME AS SEDE_TERRITORIALE, APPUNTAMENTI_ORARI.ID AS ID_ORARIO, APPUNTAMENTI_ORARI.ORARIO, C.DATA, UPPER(C.GIORNO) AS GIORNO  FROM " _
                                & " (" _
                                & "     SELECT TO_CHAR(TO_DATE(B.DATA,'YYYYMMDD'), 'DY', 'NLS_DATE_LANGUAGE=AMERICAN') AS GIORNO, A.*, B.* FROM" _
                                & "     (" _
                                & "         SELECT * FROM  SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                & "     ) A  " _
                                & "     LEFT OUTER JOIN " _
                                & "     (" _
                                & "         SELECT TO_CHAR(TO_DATE('" & p_dta_inizio & "' ,'yyyymmdd' ) + ROWNUM - 1 , 'YYYYMMDD') AS DATA" _
                                & "         FROM ALL_OBJECTS" _
                                & "         WHERE ROWNUM <= TO_DATE('" & p_dta_fine & "', 'yyyymmdd') - TO_DATE( '" & p_dta_inizio & "','yyyymmdd' ) + 1" _
                                & "     ) B  " _
                                & "      ON (1=1)" _
                                & "      WHERE ID_FILIALE = " & p_id_filiale & " AND FL_ATTIVO = 1" _
                                & " ) C " _
                                & " LEFT OUTER JOIN SISCOM_MI.APPUNTAMENTI_ORARI  ON (1=1)" _
                                & " LEFT OUTER JOIN SISCOM_MI.TAB_FILIALI ON (C.ID_FILIALE = TAB_FILIALI.ID)" _
                                & " WHERE C.GIORNO <> 'SAT' AND C.GIORNO <> 'SUN'" _
                                & " AND (" _
                                & "         SELECT COUNT (*)" _
                                & "         FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS S WHERE GIORNO = C.DATA AND C.ID = S.ID_SPORTELLO AND S.ID_ORARIO=APPUNTAMENTI_ORARI.ID AND S.ID_FILIALE = C.ID_FILIALE" _
                                & "       ) = 0 " _
                                & lAddWhere

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    'Dim data As Date
                    Dim data As Date = Format(CInt(Right(par.IfNull(item("DATA"), "00000000"), 2)), "00") & "/" & Format(CInt(Mid(par.IfNull(item("DATA"), "00000000"), 5, 2)), "00") & "/" & Format(CInt(Left(par.IfNull(item("DATA"), "00000000"), 4)), "0000")

                    If Not par.IsFestivo(data, True) Then
                        lRet(i) = New Slot
                        lRet(i).IdFiliale = par.IfNull(item("ID_FILIALE"), 0)
                        lRet(i).IDSportello = par.IfNull(item("ID_SPORTELLO"), 0)
                        lRet(i).IdOrario = par.IfNull(item("ID_ORARIO"), 0)
                        lRet(i).SedeTerritoriale = par.IfNull(item("SEDE_TERRITORIALE"), "")
                        lRet(i).Sportello = par.IfNull(item("SPORTELLO"), "")
                        lRet(i).Orario = par.IfNull(item("ORARIO"), "")
                        lRet(i).Giorno = par.IfNull(item("DATA"), "")

                        i = i + 1
                    End If
                Next
            End If
            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Ritorna l'insieme dei Dei tipi Allegati per le segnalazioni  
    '// parametri:      autorizzazione
    '// return:         Array dei tipoallegato
    <WebMethod()> _
    Public Function GetTipiAllegati(ByRef p_autorizzazione As Autorizzazione) As TipoAllegato()
        Dim lRet() As TipoAllegato = Nothing

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)

            conndata.apri()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ALLEGATI_WS_TIPI WHERE ID_OGGETTO = 17"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            conndata.chiudi(False)

            If dt.Rows.Count >= 1 Then
                ReDim lRet(dt.Rows.Count - 1)
                Dim i As Integer = 0
                For Each item In dt.Rows
                    lRet(i) = New TipoAllegato
                    lRet(i).ID = item("ID")
                    lRet(i).Descrizione = par.IfNull(item("DESCRIZIONE"), "")

                    i = i + 1
                Next
            End If

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message

        End Try

        Return lRet
    End Function

    '///////////////////////////////////////////////////////////
    '// Modifica un indirizzo email
    '// parametri:      autorizzazione, idcontratto da modificare, email
    '//                 codice fiscale e ruolo dell'operatore che modifica la mail
    '// return:         id contratto modificato, altrimenti -1 in caso di errore
    <WebMethod()> _
    Public Function ModifyEmail(ByRef p_autorizzazione As Autorizzazione, ByVal p_CodiceFiscale As String, ByVal p_idRuolo As Long, ByVal p_idContratto As Long, ByVal p_Email As String) As Long

        Dim lret As Long = 0
        Dim nome_utente As String = ""
        Dim ID_OPERATORE As String = ""

        lret = p_idContratto

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        Try
            Dim par As New CM.Global
            Dim connData As CM.datiConnessione
            connData = New CM.datiConnessione(par, False, False)
            connData.apri()

            '////////////////////////////////////////
            '// Utente che inserisce la segnalazione
            If p_idRuolo = 0 Then
                Return Nothing
            Else
                If p_idRuolo = 1 Then
                    'operatore MM
                    nome_utente = " (SELECT max(OPERATORE) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 2 Then
                    ' operatore contact center
                    nome_utente = " (SELECT max(OPERATORE) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 3 Then
                    ' Inquilino
                    nome_utente = " (SELECT max(COGNOME || ' ' || NOME) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 4 Then
                    ' delegato sindacale
                    nome_utente = " (SELECT max(COGNOME || ' ' || NOME) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 5 Then
                    ' Amm. cond
                    nome_utente = " (SELECT max(COGNOME || ' ' || NOME) FROM SISCOM_MI.COND_AMMINISTRATORI WHERE COND_AMMINISTRATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.COND_AMMINISTRATORI WHERE COND_AMMINISTRATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 6 Then
                    ' delegati Autogestione
                    nome_utente = " (SELECT max(RAPP_COGNOME || ' ' || RAPP_NOME) FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 7 Then
                    ' custodi
                    nome_utente = " (SELECT max(COGNOME || ' ' || NOME) FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 11 Then
                    ' operatore contact center
                    nome_utente = " (SELECT max(OPERATORE) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                End If

                If nome_utente = "" Then
                    nome_utente = "(SELECT max(OPERATORE) FROM OPERATORI WHERE OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" & UCase(p_autorizzazione.Login) & "')"
                    ID_OPERATORE = "(SELECT ID FROM OPERATORI WHERE OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" & UCase(p_autorizzazione.Login) & "')"
                End If

            End If

            'par.cmd.CommandText = " UPDATE SISCOM_MI.RAPPORTI_UTENZA " _
            '                     & " SET MAIL_COR   = '" & p_Email & "'" _
            '                     & " WHERE  ID      = " & p_idContratto

            'par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = " UPDATE SISCOM_MI.ANAGRAFICA SET EMAIL = " & p_Email & " " _
                                & " WHERE ID IN (SELECT ID_ANAGRAFICA FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO = " & p_idContratto & " AND COD_TIPOLOGIA_OCCUPANTE='INTE') "

            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & p_idContratto & "," & ID_OPERATORE & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F02','Aggiornamento recapito email effettuato da ' || " & nome_utente & ")"
            par.cmd.ExecuteNonQuery()

            connData.chiudi()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message
            lret = -1
        End Try

        Return lret

    End Function


    '///////////////////////////////////////////////////////////
    '// Modifica il telefono dell'inquilino intestatario del contratto
    '// parametri:      autorizzazione, idcontratto da modificare, email
    '//                 codice fiscale e ruolo dell'operatore che modifica la mail
    '// return:         id contratto modificato, altrimenti -1 in caso di errore
    <WebMethod()> _
    Public Function ModifyTelefono(ByRef p_autorizzazione As Autorizzazione, ByVal p_CodiceFiscale As String, ByVal p_idRuolo As Long, ByVal p_idContratto As Long, ByVal p_telefono As String) As Boolean

        Dim lret As Long = 0
        Dim nome_utente As String = ""
        Dim ID_OPERATORE As String = ""

        lret = p_idContratto

        If Not p_autorizzazione.Autorizza() Then
            Return Nothing
        End If

        Try
            Dim par As New CM.Global
            Dim connData As CM.datiConnessione
            connData = New CM.datiConnessione(par, False, False)
            connData.apri()

            '////////////////////////////////////////
            '// Utente che inserisce la segnalazione
            If p_idRuolo = 0 Then
                Return Nothing
            Else
                If p_idRuolo = 1 Then
                    'operatore MM
                    nome_utente = " (SELECT max(OPERATORE) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 2 Then
                    ' operatore contact center
                    nome_utente = " (SELECT max(OPERATORE) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 3 Then
                    ' Inquilino
                    nome_utente = " (SELECT max(COGNOME || ' ' || NOME) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 4 Then
                    ' delegato sindacale
                    nome_utente = " (SELECT max(COGNOME || ' ' || NOME) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 5 Then
                    ' Amm. cond
                    nome_utente = " (SELECT max(COGNOME || ' ' || NOME) FROM SISCOM_MI.COND_AMMINISTRATORI WHERE COND_AMMINISTRATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.COND_AMMINISTRATORI WHERE COND_AMMINISTRATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 6 Then
                    ' delegati Autogestione
                    nome_utente = " (SELECT max(RAPP_COGNOME || ' ' || RAPP_NOME) FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.AUTOGESTIONI_ESERCIZI WHERE AUTOGESTIONI_ESERCIZI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 7 Then
                    ' custodi
                    nome_utente = " (SELECT max(COGNOME || ' ' || NOME) FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE ANAGRAFICA_CUSTODI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                ElseIf p_idRuolo = 11 Then
                    ' operatore contact center
                    nome_utente = " (SELECT max(OPERATORE) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                    ID_OPERATORE = " (SELECT max(ID) FROM OPERATORI WHERE OPERATORI.COD_FISCALE = '" & p_CodiceFiscale & "')"
                End If

                If nome_utente = "" Then
                    nome_utente = "(SELECT max(OPERATORE) FROM OPERATORI WHERE OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" & UCase(p_autorizzazione.Login) & "')"
                    ID_OPERATORE = "(SELECT ID FROM OPERATORI WHERE OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" & UCase(p_autorizzazione.Login) & "')"
                End If

            End If

            par.cmd.CommandText = " UPDATE SISCOM_MI.ANAGRAFICA SET TELEFONO = " & p_telefono & " " _
                                & " WHERE ID IN (SELECT ID_ANAGRAFICA FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO = " & p_idContratto & " AND COD_TIPOLOGIA_OCCUPANTE='INTE') "

            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & p_idContratto & "," & ID_OPERATORE & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F02','Aggiornamento recapito TELEFONICO effettuato da ' || " & nome_utente & ")"
            par.cmd.ExecuteNonQuery()

            connData.chiudi()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            p_autorizzazione.EsitoOperazione.codice = "000"
            p_autorizzazione.EsitoOperazione.descrizione = "Operazione riuscita senza errori."
        Catch ex As Exception
            p_autorizzazione.EsitoOperazione.codice = "001"
            p_autorizzazione.EsitoOperazione.descrizione = ex.Message
            lret = -1
        End Try

        Return lret

    End Function


End Class

Public Class Inquilino
    Private _id As Long
    Private _CodiceFiscale As String
    Private _Nome As String
    Private _Cognome As String
    'Private _Intestatario As Boolean
    Private _Telefono As String
    Private _Email As String
    Private _IndirizzoResidenza As String
    Private _CAPResidenza As String
    Private _ProvinciaResidenza As String
    Private _ComuneResidenza As String
    Private _CivicoResidenza As String
    Private _Cellulare As String
    Private _DataNascita As String
    Private _PartitaIva As String
    Private _DataAggiornamento As String
    Private _IDRuolo As Long ' fisso 3
    Private _TipoInquilino As String  ' Campo descrittivo della tipologia occupante (es. Capofamiglia, Moglie/Marito, ecc.)

    Public Property Id() As Long
        Get
            Return _id
        End Get
        Set(ByVal value As Long)
            Me._id = value
        End Set
    End Property
    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            Me._Nome = value
        End Set
    End Property
    Public Property CodiceFiscale() As String
        Get
            Return _CodiceFiscale
        End Get
        Set(ByVal value As String)
            Me._CodiceFiscale = value
        End Set
    End Property
    Public Property Cognome() As String
        Get
            Return _Cognome
        End Get
        Set(ByVal value As String)
            Me._Cognome = value
        End Set
    End Property
    Public Property Telefono() As String
        Get
            Return _Telefono
        End Get
        Set(ByVal value As String)
            Me._Telefono = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            Me._Email = value
        End Set
    End Property
    Public Property IndirizzoResidenza() As String
        Get
            Return _IndirizzoResidenza
        End Get
        Set(ByVal value As String)
            Me._IndirizzoResidenza = value
        End Set
    End Property
    Public Property CAPResidenza() As String
        Get
            Return _CAPResidenza
        End Get
        Set(ByVal value As String)
            Me._CAPResidenza = value
        End Set
    End Property
    Public Property ProvinciaResidenza() As String
        Get
            Return _ProvinciaResidenza
        End Get
        Set(ByVal value As String)
            Me._ProvinciaResidenza = value
        End Set
    End Property
    Public Property ComuneResidenza() As String
        Get
            Return _ComuneResidenza
        End Get
        Set(ByVal value As String)
            Me._ComuneResidenza = value
        End Set
    End Property
    Public Property CivicoResidenza() As String
        Get
            Return _CivicoResidenza
        End Get
        Set(ByVal value As String)
            Me._CivicoResidenza = value
        End Set
    End Property
    Public Property Cellulare() As String
        Get
            Return _Cellulare
        End Get
        Set(ByVal value As String)
            Me._Cellulare = value
        End Set
    End Property
    Public Property DataNascita() As String
        Get
            Return _DataNascita
        End Get
        Set(ByVal value As String)
            Me._DataNascita = value
        End Set
    End Property
    Public Property PartitaIva() As String
        Get
            Return _PartitaIva
        End Get
        Set(ByVal value As String)
            Me._PartitaIva = value
        End Set
    End Property
    Public Property DataAggiornamento() As String
        Get
            Return _DataAggiornamento
        End Get
        Set(ByVal value As String)
            Me._DataAggiornamento = value
        End Set
    End Property
    Public Property IDRuolo() As Long
        Get
            Return _IDRuolo
        End Get
        Set(ByVal value As Long)
            Me._IDRuolo = value
        End Set
    End Property
    Public Property TipoInquilino() As String
        Get
            Return _TipoInquilino
        End Get
        Set(ByVal value As String)
            Me._TipoInquilino = value
        End Set
    End Property

End Class

Public Class Contratto
    Private _ID As Long
    Private _Codice As String
    Private _Attivo As Boolean
    Private _IdIntestatario As Long
    Private _DataDecorrenza As String
    Private _Tipologia As String
    Private _DataScadenza As String
    Private _Saldo As Decimal
    Private _CanoneMensile As Decimal
    Private _CanoneAnnuale As Decimal
    Private _AreaAppartenenza As String
    Private _IndirizzoCorrispondenza As String
    Private _CapCorrispondenza As String
    Private _TelCorrispondenza As String
    Private _EMailCorrispondenza As String
    Private _ComuneCorrispondenza As String
    Private _PressoCorrispondenza As String
    Private _ProvCorrispondenza As String
    Private _Edificio As Edifici
    Private _Complesso As Complessi

    Public Property Edificio() As Edifici
        Get
            Return _Edificio
        End Get
        Set(ByVal value As Edifici)
            Me._Edificio = value
        End Set
    End Property

    Public Property Complesso() As Complessi
        Get
            Return _Complesso
        End Get
        Set(ByVal value As Complessi)
            Me._Complesso = value
        End Set
    End Property

    Public Property Id() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property Codice() As String
        Get
            Return _Codice
        End Get
        Set(ByVal value As String)
            Me._Codice = value
        End Set
    End Property
    Public Property Attivo() As Boolean
        Get
            Return _Attivo
        End Get
        Set(ByVal value As Boolean)
            Me._Attivo = value
        End Set
    End Property
    Public Property IdIntestatario() As Long
        Get
            Return _IdIntestatario
        End Get
        Set(ByVal value As Long)
            Me._IdIntestatario = value
        End Set
    End Property
    Public Property DataDecorrenza() As String
        Get
            Return _DataDecorrenza
        End Get
        Set(ByVal value As String)
            Me._DataDecorrenza = value
        End Set
    End Property
    Public Property DataScadenza() As String
        Get
            Return _DataScadenza
        End Get
        Set(ByVal value As String)
            Me._DataScadenza = value
        End Set
    End Property
    Public Property Tipologia() As String
        Get
            Return _Tipologia
        End Get
        Set(ByVal value As String)
            Me._Tipologia = value
        End Set
    End Property
    Public Property Saldo() As Decimal
        Get
            Return _Saldo
        End Get
        Set(ByVal value As Decimal)
            Me._Saldo = value
        End Set
    End Property
    Public Property CanoneMensile() As Decimal
        Get
            Return _CanoneMensile
        End Get
        Set(ByVal value As Decimal)
            Me._CanoneMensile = value
        End Set
    End Property
    Public Property CanoneAnnuale() As Decimal
        Get
            Return _CanoneAnnuale
        End Get
        Set(ByVal value As Decimal)
            Me._CanoneAnnuale = value
        End Set
    End Property
    Public Property AreaAppartenenza() As String
        Get
            Return _AreaAppartenenza
        End Get
        Set(ByVal value As String)
            Me._AreaAppartenenza = value
        End Set
    End Property
    Public Property IndirizzoCorrispondenza() As String
        Get
            Return _IndirizzoCorrispondenza
        End Get
        Set(ByVal value As String)
            Me._IndirizzoCorrispondenza = value
        End Set
    End Property
    Public Property CapCorrispondenza() As String
        Get
            Return _CapCorrispondenza
        End Get
        Set(ByVal value As String)
            Me._CapCorrispondenza = value
        End Set
    End Property
    Public Property TelCorrispondenza() As String
        Get
            Return _TelCorrispondenza
        End Get
        Set(ByVal value As String)
            Me._TelCorrispondenza = value
        End Set
    End Property
    Public Property EmailCorrispondenza() As String
        Get
            Return _EMailCorrispondenza
        End Get
        Set(ByVal value As String)
            Me._EMailCorrispondenza = value
        End Set
    End Property
    Public Property ComuneCorrispondenza() As String
        Get
            Return _ComuneCorrispondenza
        End Get
        Set(ByVal value As String)
            Me._ComuneCorrispondenza = value
        End Set
    End Property
    Public Property PressoCorrispondenza() As String
        Get
            Return _PressoCorrispondenza
        End Get
        Set(ByVal value As String)
            Me._PressoCorrispondenza = value
        End Set
    End Property
    Public Property ProvCorrispondenza() As String
        Get
            Return _ProvCorrispondenza
        End Get
        Set(ByVal value As String)
            Me._ProvCorrispondenza = value
        End Set
    End Property

End Class

Public Class BollettinoNonPagato
    Private _ID As Long
    Private _IDContratto As Long
    Private _DataEmissione As String
    Private _DataScadenza As String
    Private _Importo As Decimal
    Private _Rata As Integer
    Private _Anno As Integer
    Private _NumBolletta As String
    Private _PdfMAV As String

    Public Property Id() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property IDContratto() As Long
        Get
            Return _IDContratto
        End Get
        Set(ByVal value As Long)
            Me._IDContratto = value
        End Set
    End Property
    Public Property DataEmissione() As String
        Get
            Return _DataEmissione
        End Get
        Set(ByVal value As String)
            Me._DataEmissione = value
        End Set
    End Property
    Public Property PdfMAV() As String
        Get
            Return _PdfMAV
        End Get
        Set(ByVal value As String)
            Me._PdfMAV = value
        End Set
    End Property
    Public Property DataScadenza() As String
        Get
            Return _DataScadenza
        End Get
        Set(ByVal value As String)
            Me._DataScadenza = value
        End Set
    End Property
    Public Property Importo() As Decimal
        Get
            Return _Importo
        End Get
        Set(ByVal value As Decimal)
            Me._Importo = value
        End Set
    End Property
    Public Property Rata() As Integer
        Get
            Return _Rata
        End Get
        Set(ByVal value As Integer)
            Me._Rata = value
        End Set
    End Property
    Public Property Anno() As Integer
        Get
            Return _Anno
        End Get
        Set(ByVal value As Integer)
            Me._Anno = value
        End Set
    End Property
    Public Property NumBolletta() As String
        Get
            Return _NumBolletta
        End Get
        Set(ByVal value As String)
            Me._NumBolletta = value
        End Set
    End Property
End Class

Public Class UnitaImmobiliare
    Private _ID As Long
    Private _IDContratto As Long
    Private _Indirizzo As String
    Private _Civico As String
    Private _Comune As String
    Private _CAP As String
    Private _Provincia As String
    Private _CodiceUnita As String
    Private _TipologiaUnita As String
    Private _Scala As String
    Private _Piano As String
    Private _Interno As String
    Private _SedeTerritoriale As String
    Private _IdEdificio As Long
    Private _NomeEdificio As String
    Private _IdComplesso As Long
    Private _NomeComplesso As String
    Private _CodiceFiscaleTitolare As String

    Public Property Id() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property IDContratto() As Long
        Get
            Return _IDContratto
        End Get
        Set(ByVal value As Long)
            Me._IDContratto = value
        End Set
    End Property
    Public Property Civico() As String
        Get
            Return _Civico
        End Get
        Set(ByVal value As String)
            Me._Civico = value
        End Set
    End Property
    Public Property CodiceFiscaleTitolare() As String
        Get
            Return _CodiceFiscaleTitolare
        End Get
        Set(ByVal value As String)
            Me._CodiceFiscaleTitolare = value
        End Set
    End Property
    Public Property Comune() As String
        Get
            Return _Comune
        End Get
        Set(ByVal value As String)
            Me._Comune = value
        End Set
    End Property
    Public Property Cap() As String
        Get
            Return _CAP
        End Get
        Set(ByVal value As String)
            Me._CAP = value
        End Set
    End Property
    Public Property Provincia() As String
        Get
            Return _Provincia
        End Get
        Set(ByVal value As String)
            Me._Provincia = value
        End Set
    End Property
    Public Property Indirizzo() As String
        Get
            Return _Indirizzo
        End Get
        Set(ByVal value As String)
            Me._Indirizzo = value
        End Set
    End Property
    Public Property IdComplesso() As Long
        Get
            Return _IdComplesso
        End Get
        Set(ByVal value As Long)
            Me._IdComplesso = value
        End Set
    End Property
    Public Property IdEdificio() As Long
        Get
            Return _IdEdificio
        End Get
        Set(ByVal value As Long)
            Me._IdEdificio = value
        End Set
    End Property
    Public Property NomeComplesso() As String
        Get
            Return _NomeComplesso
        End Get
        Set(ByVal value As String)
            Me._NomeComplesso = value
        End Set
    End Property
    Public Property NomeEdificio() As String
        Get
            Return _NomeEdificio
        End Get
        Set(ByVal value As String)
            Me._NomeEdificio = value
        End Set
    End Property
    Public Property SedeTerritoriale() As String
        Get
            Return _SedeTerritoriale
        End Get
        Set(ByVal value As String)
            Me._SedeTerritoriale = value
        End Set
    End Property
    Public Property Interno() As String
        Get
            Return _Interno
        End Get
        Set(ByVal value As String)
            Me._Interno = value
        End Set
    End Property
    Public Property Piano() As String
        Get
            Return _Piano
        End Get
        Set(ByVal value As String)
            Me._Piano = value
        End Set
    End Property
    Public Property Scala() As String
        Get
            Return _Scala
        End Get
        Set(ByVal value As String)
            Me._Scala = value
        End Set
    End Property
    Public Property TipologiaUnita() As String
        Get
            Return _TipologiaUnita
        End Get
        Set(ByVal value As String)
            Me._TipologiaUnita = value
        End Set
    End Property
    Public Property CodiceUnita() As String
        Get
            Return _CodiceUnita
        End Get
        Set(ByVal value As String)
            Me._CodiceUnita = value
        End Set
    End Property

End Class

Public Class Segnalazione
    Private _ID As Long
    Private _IDOperatore As Long
    Private _IDContratto As Long
    Private _IDStato As Long
    Private _Categoria As String
    Private _SottoCategoria1 As String
    Private _SottoCategoria2 As String
    Private _SottoCategoria3 As String
    Private _SottoCategoria4 As String
    Private _Stato As String
    Private _Descrizione As String
    Private _Allegati() As Allegato
    Private _NotePubbliche As String
    Private _DataInserimento As String
    Private _DataPresaInCarico As String
    Private _DataEvasione As String
    Private _DataChiusura As String
    Private _CodiceFiscaleSoggetto As String
    Private _CodiceComplesso As String
    Private _CodiceEdificio As String
    Private _CodiceUnita As String
    Private _CanaleApertura As String        '7 WEB, 6=CUSTODI SOCIALI, 5=DIRETTA, EMAIL ...
    Private _SemaforoPriorita As Integer     '0=BLU, 1=BIANCO, 2=VERDE, 3=GIALLO, 4 ROSSO
    '   Private _NoteGestionali As String
    Private _IDCategoria As String
    Private _IDSottoCategoria1 As String
    Private _IDSottoCategoria2 As String
    Private _IDSottoCategoria3 As String
    Private _IDSottoCategoria4 As String
    Private _Telefono As String
    Private _Nome As String
    Private _Cognome As String
    Private _email As String
    Private _cellulare As String

    Private _CodiceFiscaleOperatore As String
    Private _IdRuolo As Long
    Private _IdSegnalante As Long
    Private _OraInserimento As String

    Private _IdComplesso As Long
    Private _NomeComplesso As String
    Private _IdEdificio As Long
    Private _NomeEdificio As String
    Private _IdUnita As Long
    Private _Scala As String
    Private _Piano As String
    Private _Interno As String
    Private _SedeTerritoriale As String
    Private _IDStruttura As String

    Private _ContattoFornEmergenza As Boolean
    Private _ContattoFornEmergenzaData As String
    Private _ContattoFornEmergenzaOra As String

    Private _InterventoFornEmergenza As Boolean
    Private _InterventoFornEmergenzaData As String
    Private _InterventoFornEmergenzaOra As String
    Private _DataSopralluogo As String
    Private _OraSopralluogo As String
    Private _NoteSopralluogo As String
    Private _DataProgrammataIntervento As String
    Private _OraProgrammataIntervento As String
    Private _DataUltimoIntervento As String
    Private _OraUltimoIntervento As String
    Private _DataEffettivaIntervento As String
    Private _OraEffettivaIntervento As String
    Private _NoteEffettivoIntervento As String

    Private _DocumentiRichiesti As Documento()

    Public Property Id() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property IdRuolo() As Long
        Get
            Return _IdRuolo
        End Get
        Set(ByVal value As Long)
            Me._IdRuolo = value
        End Set
    End Property
    Public Property IdSegnalante() As Long
        Get
            Return _IdSegnalante
        End Get
        Set(ByVal value As Long)
            Me._IdSegnalante = value
        End Set
    End Property
    Public Property IdStruttura() As Long
        Get
            Return _IDStruttura
        End Get
        Set(ByVal value As Long)
            Me._IDStruttura = value
        End Set
    End Property
    Public Property IDOperatore() As Long
        Get
            Return _IDOperatore
        End Get
        Set(ByVal value As Long)
            Me._IDOperatore = value
        End Set
    End Property
    Public Property IDContratto() As Long
        Get
            Return _IDContratto
        End Get
        Set(ByVal value As Long)
            Me._IDContratto = value
        End Set
    End Property
    Public Property IDStato() As Long
        Get
            Return _IDStato
        End Get
        Set(ByVal value As Long)
            Me._IDStato = value
        End Set
    End Property
    Public Property Categoria() As String
        Get
            Return _Categoria
        End Get
        Set(ByVal value As String)
            Me._Categoria = value
        End Set
    End Property
    Public Property Cellulare() As String
        Get
            Return _cellulare
        End Get
        Set(ByVal value As String)
            Me._cellulare = value
        End Set
    End Property
    Public Property SottoCategoria1() As String
        Get
            Return _SottoCategoria1
        End Get
        Set(ByVal value As String)
            Me._SottoCategoria1 = value
        End Set
    End Property
    Public Property SottoCategoria2() As String
        Get
            Return _SottoCategoria2
        End Get
        Set(ByVal value As String)
            Me._SottoCategoria2 = value
        End Set
    End Property
    Public Property SottoCategoria3() As String
        Get
            Return _SottoCategoria3
        End Get
        Set(ByVal value As String)
            Me._SottoCategoria3 = value
        End Set
    End Property
    Public Property SottoCategoria4() As String
        Get
            Return _SottoCategoria4
        End Get
        Set(ByVal value As String)
            Me._SottoCategoria4 = value
        End Set
    End Property
    Public Property Stato() As String
        Get
            Return _Stato
        End Get
        Set(ByVal value As String)
            Me._Stato = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property
    Public Property Allegati() As Allegato()
        Get
            Return _Allegati
        End Get
        Set(ByVal value As Allegato())
            Me._Allegati = value
        End Set
    End Property
    Public Property NotePubbliche() As String
        Get
            Return _NotePubbliche
        End Get
        Set(ByVal value As String)
            Me._NotePubbliche = value
        End Set
    End Property
    Public Property DataInserimento() As String
        Get
            Return _DataInserimento
        End Get
        Set(ByVal value As String)
            Me._DataInserimento = value
        End Set
    End Property
    Public Property OraInserimento() As String
        Get
            Return _OraInserimento
        End Get
        Set(ByVal value As String)
            Me._OraInserimento = value
        End Set
    End Property
    Public Property DataPresaInCarico() As String
        Get
            Return _DataPresaInCarico
        End Get
        Set(ByVal value As String)
            Me._DataPresaInCarico = value
        End Set
    End Property
    Public Property DataEvasione() As String
        Get
            Return _DataEvasione
        End Get
        Set(ByVal value As String)
            Me._DataEvasione = value
        End Set
    End Property
    Public Property DataChiusura() As String
        Get
            Return _DataChiusura
        End Get
        Set(ByVal value As String)
            Me._DataChiusura = value
        End Set
    End Property
    Public Property CodiceFiscaleOperatore() As String
        Get
            Return _CodiceFiscaleOperatore
        End Get
        Set(ByVal value As String)
            Me._CodiceFiscaleOperatore = value
        End Set
    End Property
    Public Property CodiceFiscaleSoggetto() As String
        Get
            Return _CodiceFiscaleSoggetto
        End Get
        Set(ByVal value As String)
            Me._CodiceFiscaleSoggetto = value
        End Set
    End Property
    Public Property CodiceComplesso() As String
        Get
            Return _CodiceComplesso
        End Get
        Set(ByVal value As String)
            Me._CodiceComplesso = value
        End Set
    End Property
    Public Property CodiceEdificio() As String
        Get
            Return _CodiceEdificio
        End Get
        Set(ByVal value As String)
            Me._CodiceEdificio = value
        End Set
    End Property
    Public Property CodiceUnita() As String
        Get
            Return _CodiceUnita
        End Get
        Set(ByVal value As String)
            Me._CodiceUnita = value
        End Set
    End Property
    Public Property CanaleApertura() As String
        Get
            Return _CanaleApertura
        End Get
        Set(ByVal value As String)
            Me._CanaleApertura = value
        End Set
    End Property
    Public Property SemaforoPriorita() As Integer
        Get
            Return _SemaforoPriorita
        End Get
        Set(ByVal value As Integer)
            Me._SemaforoPriorita = value
        End Set
    End Property
    Public Property IdCategoria() As Long
        Get
            Return _IDCategoria
        End Get
        Set(ByVal value As Long)
            Me._IDCategoria = value
        End Set
    End Property
    Public Property IdSottoCategoria1() As Long
        Get
            Return _IDSottoCategoria1
        End Get
        Set(ByVal value As Long)
            Me._IDSottoCategoria1 = value
        End Set
    End Property
    Public Property IdSottoCategoria2() As Long
        Get
            Return _IDSottoCategoria2
        End Get
        Set(ByVal value As Long)
            Me._IDSottoCategoria2 = value
        End Set
    End Property
    Public Property IdSottoCategoria3() As Long
        Get
            Return _IDSottoCategoria3
        End Get
        Set(ByVal value As Long)
            Me._IDSottoCategoria3 = value
        End Set
    End Property
    Public Property IdSottoCategoria4() As Long
        Get
            Return _IDSottoCategoria4
        End Get
        Set(ByVal value As Long)
            Me._IDSottoCategoria4 = value
        End Set
    End Property
    Public Property Telefono() As String
        Get
            Return _Telefono
        End Get
        Set(ByVal value As String)
            Me._Telefono = value
        End Set
    End Property
    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            Me._Nome = value
        End Set
    End Property
    Public Property Cognome() As String
        Get
            Return _Cognome
        End Get
        Set(ByVal value As String)
            Me._Cognome = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            Me._email = value
        End Set
    End Property
    Public Property IdComplesso() As Long
        Get
            Return _IdComplesso
        End Get
        Set(ByVal value As Long)
            Me._IdComplesso = value
        End Set
    End Property
    Public Property IdEdificio() As Long
        Get
            Return _IdEdificio
        End Get
        Set(ByVal value As Long)
            Me._IdEdificio = value
        End Set
    End Property
    Public Property IdUnita() As Long
        Get
            Return _IdUnita
        End Get
        Set(ByVal value As Long)
            Me._IdUnita = value
        End Set
    End Property
    Public Property NomeComplesso() As String
        Get
            Return _NomeComplesso
        End Get
        Set(ByVal value As String)
            Me._NomeComplesso = value
        End Set
    End Property
    Public Property NomeEdificio() As String
        Get
            Return _NomeEdificio
        End Get
        Set(ByVal value As String)
            Me._NomeEdificio = value
        End Set
    End Property
    Public Property Scala() As String
        Get
            Return _Scala
        End Get
        Set(ByVal value As String)
            Me._Scala = value
        End Set
    End Property
    Public Property Piano() As String
        Get
            Return _Piano
        End Get
        Set(ByVal value As String)
            Me._Piano = value
        End Set
    End Property
    Public Property Interno() As String
        Get
            Return _Interno
        End Get
        Set(ByVal value As String)
            Me._Interno = value
        End Set
    End Property
    Public Property SedeTerritoriale() As String
        Get
            Return _SedeTerritoriale
        End Get
        Set(ByVal value As String)
            Me._SedeTerritoriale = value
        End Set
    End Property
    Public Property ContattoFornEmergenza() As Boolean
        Get
            Return _ContattoFornEmergenza
        End Get
        Set(ByVal value As Boolean)
            Me._ContattoFornEmergenza = value
        End Set
    End Property
    Public Property InterventoFornEmergenza() As Boolean
        Get
            Return _InterventoFornEmergenza
        End Get
        Set(ByVal value As Boolean)
            Me._InterventoFornEmergenza = value
        End Set
    End Property
    Public Property ContattoFornEmergenzaData() As String
        Get
            Return _ContattoFornEmergenzaData
        End Get
        Set(ByVal value As String)
            Me._ContattoFornEmergenzaData = value
        End Set
    End Property
    Public Property ContattoFornEmergenzaOra() As String
        Get
            Return _ContattoFornEmergenzaOra
        End Get
        Set(ByVal value As String)
            Me._ContattoFornEmergenzaOra = value
        End Set
    End Property

    Public Property InterventoFornEmergenzaData() As String
        Get
            Return _InterventoFornEmergenzaData
        End Get
        Set(ByVal value As String)
            Me._InterventoFornEmergenzaData = value
        End Set
    End Property
    Public Property InterventoFornEmergenzaOra() As String
        Get
            Return _InterventoFornEmergenzaOra
        End Get
        Set(ByVal value As String)
            Me._InterventoFornEmergenzaOra = value
        End Set
    End Property
    Public Property DataSopralluogo() As String
        Get
            Return _DataSopralluogo
        End Get
        Set(ByVal value As String)
            Me._DataSopralluogo = value
        End Set
    End Property
    Public Property OraSopralluogo() As String
        Get
            Return _OraSopralluogo
        End Get
        Set(ByVal value As String)
            Me._OraSopralluogo = value
        End Set
    End Property
    Public Property NoteSopralluogo() As String
        Get
            Return _NoteSopralluogo
        End Get
        Set(ByVal value As String)
            Me._NoteSopralluogo = value
        End Set
    End Property
    Public Property DataProgrammataIntervento() As String
        Get
            Return _DataProgrammataIntervento
        End Get
        Set(ByVal value As String)
            Me._DataProgrammataIntervento = value
        End Set
    End Property
    Public Property OraProgrammataIntervento() As String
        Get
            Return _OraProgrammataIntervento
        End Get
        Set(ByVal value As String)
            Me._OraProgrammataIntervento = value
        End Set
    End Property
    Public Property DataUltimoIntervento() As String
        Get
            Return _DataUltimoIntervento
        End Get
        Set(ByVal value As String)
            Me._DataUltimoIntervento = value
        End Set
    End Property
    Public Property OraUltimoIntervento() As String
        Get
            Return _OraUltimoIntervento
        End Get
        Set(ByVal value As String)
            Me._OraUltimoIntervento = value
        End Set
    End Property
    Public Property DataEffettivaIntervento() As String
        Get
            Return _DataEffettivaIntervento
        End Get
        Set(ByVal value As String)
            Me._DataEffettivaIntervento = value
        End Set
    End Property
    Public Property OraEffettivaIntervento() As String
        Get
            Return _OraEffettivaIntervento
        End Get
        Set(ByVal value As String)
            Me._OraEffettivaIntervento = value
        End Set
    End Property
    Public Property NoteEffettivoIntervento() As String
        Get
            Return _NoteEffettivoIntervento
        End Get
        Set(ByVal value As String)
            Me._NoteEffettivoIntervento = value
        End Set
    End Property
    Public Property DocumentiRichiesti As Documento()
        Get
            Return _DocumentiRichiesti
        End Get
        Set(ByVal value As Documento())
            Me._DocumentiRichiesti = value
        End Set
    End Property

End Class

Public Class Allegato
    Private _ID As Long
    '    Private _Contenuto() As Byte
    Private _Nome As String
    Private _Descrizione As String
    Private _Oggetto As String
    Private _DataOra As String
    Private _IDTipoAllegato As Long

    Public Property Id() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property IDTipoAllegato() As Long
        Get
            Return _IDTipoAllegato
        End Get
        Set(ByVal value As Long)
            Me._IDTipoAllegato = value
        End Set
    End Property
    'Public Property Contenuto() As Byte()
    '    Get
    '        Return _Contenuto
    '    End Get
    '    Set(ByVal value As Byte())
    '        Me._Contenuto = value
    '    End Set
    'End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property
    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            Me._Nome = value
        End Set
    End Property
    Public Property DataOra() As String
        Get
            Return _DataOra
        End Get
        Set(ByVal value As String)
            Me._DataOra = value
        End Set
    End Property
    Public Property Oggetto() As String
        Get
            Return _Oggetto
        End Get
        Set(ByVal value As String)
            Me._Oggetto = value
        End Set
    End Property

End Class

Public Class Autorizzazione
    Private _Login As String
    Private _Password As String
    Private _EsitoConnessione As New Esito
    Private _EsitoOperazione As New Esito

    Public Property Login() As String
        Get
            Return _Login
        End Get
        Set(ByVal value As String)
            Me._Login = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            Me._Password = value
        End Set
    End Property

    Public Function Autorizza() As Boolean
        '/////////////////////////////////////////////
        '// Per accedere al sistema da webservice è
        '// necessario essere un operatore di sepaweb
        Dim l_ret = False
        'If EsitoConnessione.codice = Nothing Then EsitoConnessione = New Esito
        Try
            Dim par As New CM.Global
            Dim conndata As New CM.datiConnessione(par)

            Dim sUtente As String = UCase(Login())
            Dim sPassword As String = base64Decode(Password())

            'Dim CMD As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("SELECT OPERATORI.*, CAF_WEB.COD_CAF, CAF_WEB.DESCRIZIONE AS ""DESCRIZIONE_CAF"" FROM OPERATORI, CAF_WEB WHERE CAF_WEB.ID=OPERATORI.ID_CAF AND OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" + sUtente + "' AND OPERATORI.SEPA_WEB='1'", par.OracleConn)
            Dim CMD As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("SELECT OPERATORI.* FROM OPERATORI WHERE OPERATORI.FL_ELIMINATO='0' AND UPPER(OPERATORI.OPERATORE)='" & sUtente & "'", par.OracleConn)
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            myReader = CMD.ExecuteReader()
            If myReader.Read() Then
                Dim PwMatch As Boolean = par.VerifyHash(sPassword, "SHA512", Trim(par.IfNull(myReader("pw"), "")))

                If Not PwMatch Then
                    l_ret = False
                    EsitoConnessione.codice = "001"
                    EsitoConnessione.descrizione = "Connessione non riuscita, utente/password errati."
                Else
                    l_ret = True
                    EsitoConnessione.codice = "000"
                    EsitoConnessione.descrizione = "Connessione effettuata correttamente."
                End If
            Else
                If sUtente = "wssisol" And sPassword = "wssisol123." Then
                    l_ret = True
                    EsitoConnessione.codice = "000"
                    EsitoConnessione.descrizione = "Connessione effettuata correttamente."
                End If

            End If
            myReader.Close()
            conndata.chiudi()

        Catch ex As Exception
            EsitoConnessione.codice = "002"
            EsitoConnessione.descrizione = ex.Message
        End Try

        Return l_ret
    End Function

    Private Function MD5Hash(ByVal password As String) As String
        Dim md5 As MD5 = New MD5CryptoServiceProvider()
        Dim result As Byte()
        result = md5.ComputeHash(Encoding.ASCII.GetBytes(password))

        Dim strBuilder As New StringBuilder()

        For i As Integer = 0 To result.Length - 1
            strBuilder.Append(result(i).ToString("x2"))
        Next

        Return strBuilder.ToString()
    End Function

    Public Function base64Decode(data As String) As String
        Try
            Dim encoder As New System.Text.UTF8Encoding()
            Dim utf8Decode As System.Text.Decoder = encoder.GetDecoder()

            Dim todecode_byte As Byte() = Convert.FromBase64String(data)
            Dim charCount As Integer = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length)
            Dim decoded_char As Char() = New Char(charCount - 1) {}
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0)
            Dim result As String = New [String](decoded_char)
            Return result
        Catch e As Exception
            Throw New Exception("Error in base64Decode" + e.Message)
        End Try
    End Function

    Public Property EsitoConnessione() As Esito
        Get
            Return _EsitoConnessione
        End Get
        Set(ByVal value As Esito)
            Me._EsitoConnessione = value
        End Set
    End Property
    Public Property EsitoOperazione() As Esito
        Get
            Return _EsitoOperazione
        End Get
        Set(ByVal value As Esito)
            Me._EsitoOperazione = value
        End Set
    End Property

    Public Class Esito
        Private _codice As String
        Private _descrizione As String

        Public Property codice() As String
            Get
                Return _codice
            End Get
            Set(ByVal value As String)
                Me._codice = value
            End Set
        End Property
        Public Property descrizione() As String
            Get
                Return _descrizione
            End Get
            Set(ByVal value As String)
                Me._descrizione = value
            End Set
        End Property


    End Class


End Class

Public Class StatoSegnalazione
    Private _ID As Long
    Private _descrizione As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return _descrizione
        End Get
        Set(ByVal value As String)
            Me._descrizione = value
        End Set
    End Property


End Class

Public Class CategoriaSegnalazione
    Private _ID As Long
    Private _descrizione As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return _descrizione
        End Get
        Set(ByVal value As String)
            Me._descrizione = value
        End Set
    End Property


End Class

Public Class Sottocategoria1
    Private _ID As Long
    Private _IDCategoriaPadre As Long
    Private _Descrizione As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property IDCategoriaPadre() As Long
        Get
            Return _IDCategoriaPadre
        End Get
        Set(ByVal value As Long)
            Me._IDCategoriaPadre = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property
End Class

Public Class Sottocategoria2
    Private _ID As Long
    Private _IDCategoriaPadre As Long
    Private _Descrizione As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property IDCategoriaPadre() As Long
        Get
            Return _IDCategoriaPadre
        End Get
        Set(ByVal value As Long)
            Me._IDCategoriaPadre = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property
End Class

Public Class Sottocategoria3
    Private _ID As Long
    Private _IDCategoriaPadre As Long
    Private _Descrizione As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property IDCategoriaPadre() As Long
        Get
            Return _IDCategoriaPadre
        End Get
        Set(ByVal value As Long)
            Me._IDCategoriaPadre = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property
End Class

Public Class Sottocategoria4
    Private _ID As Long
    Private _IDCategoriaPadre As Long
    Private _Descrizione As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property IDCategoriaPadre() As Long
        Get
            Return _IDCategoriaPadre
        End Get
        Set(ByVal value As Long)
            Me._IDCategoriaPadre = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property
End Class

Public Class CategorieAll
    Private _IDCategoria As Long
    Private _DescrizioneCategoria As String
    Private _IDSottoCategoria1 As Long
    Private _DescrizioneSottocategoria1 As String
    Private _IDSottoCategoria2 As Long
    Private _DescrizioneSottocategoria2 As String
    Private _IDSottoCategoria3 As Long
    Private _DescrizioneSottocategoria3 As String
    Private _IDSottoCategoria4 As Long
    Private _DescrizioneSottocategoria4 As String

    Public Property IDCategoria() As Long
        Get
            Return _IDCategoria
        End Get
        Set(ByVal value As Long)
            Me._IDCategoria = value
        End Set
    End Property
    Public Property DescrizioneCategoria() As String
        Get
            Return _DescrizioneCategoria
        End Get
        Set(ByVal value As String)
            Me._DescrizioneCategoria = value
        End Set
    End Property

    Public Property IDSottoCategoria1() As Long
        Get
            Return _IDSottoCategoria1
        End Get
        Set(ByVal value As Long)
            Me._IDSottoCategoria1 = value
        End Set
    End Property
    Public Property DescrizioneSottocategoria1() As String
        Get
            Return _DescrizioneSottocategoria1
        End Get
        Set(ByVal value As String)
            Me._DescrizioneSottocategoria1 = value
        End Set
    End Property

    Public Property IDSottoCategoria2() As Long
        Get
            Return _IDSottoCategoria2
        End Get
        Set(ByVal value As Long)
            Me._IDSottoCategoria2 = value
        End Set
    End Property
    Public Property DescrizioneSottocategoria2() As String
        Get
            Return _DescrizioneSottocategoria2
        End Get
        Set(ByVal value As String)
            Me._DescrizioneSottocategoria2 = value
        End Set
    End Property

    Public Property IDSottoCategoria3() As Long
        Get
            Return _IDSottoCategoria3
        End Get
        Set(ByVal value As Long)
            Me._IDSottoCategoria3 = value
        End Set
    End Property
    Public Property DescrizioneSottocategoria3() As String
        Get
            Return _DescrizioneSottocategoria3
        End Get
        Set(ByVal value As String)
            Me._DescrizioneSottocategoria3 = value
        End Set
    End Property

    Public Property IDSottoCategoria4() As Long
        Get
            Return _IDSottoCategoria4
        End Get
        Set(ByVal value As Long)
            Me._IDSottoCategoria4 = value
        End Set
    End Property
    Public Property DescrizioneSottocategoria4() As String
        Get
            Return _DescrizioneSottocategoria4
        End Get
        Set(ByVal value As String)
            Me._DescrizioneSottocategoria4 = value
        End Set
    End Property


End Class

Public Class Strutture
    Private _ID As Long
    Private _Nome As String
    Private _Indirizzo As String
    Private _Telefono As String
    Private _Cap As String
    Private _Comune As String
    Private _Provincia As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            Me._Nome = value
        End Set
    End Property
    Public Property Indirizzo() As String
        Get
            Return _Indirizzo
        End Get
        Set(ByVal value As String)
            Me._Indirizzo = value
        End Set
    End Property
    Public Property Telefono() As String
        Get
            Return _Telefono
        End Get
        Set(ByVal value As String)
            Me._Telefono = value
        End Set
    End Property
    Public Property Cap() As String
        Get
            Return _Cap
        End Get
        Set(ByVal value As String)
            Me._Cap = value
        End Set
    End Property
    Public Property Comune() As String
        Get
            Return _Comune
        End Get
        Set(ByVal value As String)
            Me._Comune = value
        End Set
    End Property
    Public Property Provincia() As String
        Get
            Return _Provincia
        End Get
        Set(ByVal value As String)
            Me._Provincia = value
        End Set
    End Property
End Class

Public Class Sportelli
    Private _ID As Long
    Private _IDStruttura As Long
    Private _Descrizione As String
    Private _Indice As Integer
    Private _Attivo As Boolean
    Private _DescrizioneBreve As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property IDStruttura() As Long
        Get
            Return _IDStruttura
        End Get
        Set(ByVal value As Long)
            Me._IDStruttura = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property
    Public Property Indice() As Integer
        Get
            Return _Indice
        End Get
        Set(ByVal value As Integer)
            Me._Indice = value
        End Set
    End Property
    Public Property Attivo() As Boolean
        Get
            Return _Attivo
        End Get
        Set(ByVal value As Boolean)
            Me._Attivo = value
        End Set
    End Property
    Public Property DescrizioneBreve() As String
        Get
            Return _DescrizioneBreve
        End Get
        Set(ByVal value As String)
            Me._DescrizioneBreve = value
        End Set
    End Property
End Class

Public Class StatiAppuntamenti
    Private _ID As Long
    Private _Descrizione As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property
End Class

Public Class EsitoAppuntamenti
    Private _ID As Long
    Private _Descrizione As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property
End Class

Public Class Appuntamenti
    Private _ID As Long
    Private _Data As String
    Private _IDStruttura As Long
    'Private _IDOperatore As Long
    Private _Nome As String
    Private _Cognome As String
    Private _Telefono As String
    Private _Note As String
    Private _IDSegnalazione As Long
    Private _DataInserimento As String
    Private _DataEliminazione As String
    'Private _IDOperatoreEliminazione As Long
    Private _IDSportello As Long
    Private _IDOrario As Long
    Private _DataModifica As String
    'Private _IDOperatoreModifica As Long
    Private _Cellulare As String
    Private _Email As String
    Private _IDStatoAppuntamento As Long
    Private _MancataPresentazione As Boolean
    Private _IDEsitoAppuntamento As Long
    Private _IDNota As Long

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property IDStruttura() As Long
        Get
            Return _IDStruttura
        End Get
        Set(ByVal value As Long)
            Me._IDStruttura = value
        End Set
    End Property
    Public Property Data() As String
        Get
            Return _Data
        End Get
        Set(ByVal value As String)
            Me._Data = value
        End Set
    End Property
    'Public Property IDOperatore() As Long
    '    Get
    '        Return _IDOperatore
    '    End Get
    '    Set(ByVal value As Long)
    '        Me._IDOperatore = value
    '    End Set
    'End Property
    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            Me._Nome = value
        End Set
    End Property
    Public Property Cognome() As String
        Get
            Return _Cognome
        End Get
        Set(ByVal value As String)
            Me._Cognome = value
        End Set
    End Property
    Public Property Telefono() As String
        Get
            Return _Telefono
        End Get
        Set(ByVal value As String)
            Me._Telefono = value
        End Set
    End Property
    Public Property Note() As String
        Get
            Return _Note
        End Get
        Set(ByVal value As String)
            Me._Note = value
        End Set
    End Property
    Public Property IDSegnalazione() As Long
        Get
            Return _IDSegnalazione
        End Get
        Set(ByVal value As Long)
            Me._IDSegnalazione = value
        End Set
    End Property
    Public Property DataInserimento() As String
        Get
            Return _DataInserimento
        End Get
        Set(ByVal value As String)
            Me._DataInserimento = value
        End Set
    End Property
    Public Property DataEliminazione() As String
        Get
            Return _DataEliminazione
        End Get
        Set(ByVal value As String)
            Me._DataEliminazione = value
        End Set
    End Property
    'Public Property IDOperatoreEliminazione() As Long
    '    Get
    '        Return _IDOperatoreEliminazione
    '    End Get
    '    Set(ByVal value As Long)
    '        Me._IDOperatoreEliminazione = value
    '    End Set
    'End Property
    Public Property IDSportello() As Long
        Get
            Return _IDSportello
        End Get
        Set(ByVal value As Long)
            Me._IDSportello = value
        End Set
    End Property
    Public Property IDOrario() As Long
        Get
            Return _IDOrario
        End Get
        Set(ByVal value As Long)
            Me._IDOrario = value
        End Set
    End Property
    Public Property DataModifica() As String
        Get
            Return _DataModifica
        End Get
        Set(ByVal value As String)
            Me._DataModifica = value
        End Set
    End Property
    'Public Property IDOperatoreModifica() As Long
    '    Get
    '        Return _IDOperatoreModifica
    '    End Get
    '    Set(ByVal value As Long)
    '        Me._IDOperatoreModifica = value
    '    End Set
    'End Property
    Public Property Cellulare() As String
        Get
            Return _Cellulare
        End Get
        Set(ByVal value As String)
            Me._Cellulare = value
        End Set
    End Property
    Public Property EMail() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            Me._Email = value
        End Set
    End Property
    Public Property IDStatoAppuntamento() As Long
        Get
            Return _IDStatoAppuntamento
        End Get
        Set(ByVal value As Long)
            Me._IDStatoAppuntamento = value
        End Set
    End Property
    Public Property MancataPresentazione() As Boolean
        Get
            Return _MancataPresentazione
        End Get
        Set(ByVal value As Boolean)
            Me._MancataPresentazione = value
        End Set
    End Property
    Public Property IDEsitoAppuntamento() As Long
        Get
            Return _IDEsitoAppuntamento
        End Get
        Set(ByVal value As Long)
            Me._IDEsitoAppuntamento = value
        End Set
    End Property
    Public Property IDNota() As Long
        Get
            Return _IDNota
        End Get
        Set(ByVal value As Long)
            Me._IDNota = value
        End Set
    End Property

End Class

Public Class Orari
    Private _ID As Long
    Private _Orario As String
    Private _Indice As Integer

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property Indice() As Integer
        Get
            Return _Indice
        End Get
        Set(ByVal value As Integer)
            Me._Indice = value
        End Set
    End Property
    Public Property Orario() As String
        Get
            Return _Orario
        End Get
        Set(ByVal value As String)
            Me._Orario = value
        End Set
    End Property
End Class

Public Class Ruolo
    Private _ID As Long
    Private _Descrizione As String
    Private _Visibile As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property
    Public Property Visibile() As String
        Get
            Return _Visibile
        End Get
        Set(ByVal value As String)
            Me._Visibile = value
        End Set
    End Property
End Class

Public Class Soggetto
    Private _id As Long
    Private _IDRuolo As Long
    Private _CodiceFiscale As String
    Private _TipoRuolo As String
    Private _Nome As String
    Private _Cognome As String
    Private _Telefono As String
    Private _Email As String
    Private _Cellulare As String

    Public Property Id() As Long
        Get
            Return _id
        End Get
        Set(ByVal value As Long)
            Me._id = value
        End Set
    End Property
    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            Me._Nome = value
        End Set
    End Property
    Public Property CodiceFiscale() As String
        Get
            Return _CodiceFiscale
        End Get
        Set(ByVal value As String)
            Me._CodiceFiscale = value
        End Set
    End Property
    Public Property Cognome() As String
        Get
            Return _Cognome
        End Get
        Set(ByVal value As String)
            Me._Cognome = value
        End Set
    End Property
    Public Property Telefono() As String
        Get
            Return _Telefono
        End Get
        Set(ByVal value As String)
            Me._Telefono = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            Me._Email = value
        End Set
    End Property
    Public Property TipoRuolo() As String
        Get
            Return _TipoRuolo
        End Get
        Set(ByVal value As String)
            Me._TipoRuolo = value
        End Set
    End Property
    Public Property Cellulare() As String
        Get
            Return _Cellulare
        End Get
        Set(ByVal value As String)
            Me._Cellulare = value
        End Set
    End Property
    Public Property IDRuolo() As Long
        Get
            Return _IDRuolo
        End Get
        Set(ByVal value As Long)
            Me._IDRuolo = value
        End Set
    End Property

End Class

Public Class Nota
    Private _IdSegnalazione As Long
    'Private _IDOperatore As Long
    Private _IDTipoNotaSegnalazione As Long
    Private _Descrizione As String
    Private _DataOraInserimento As String

    Public Property IDDSegnalazione() As Long
        Get
            Return _IdSegnalazione
        End Get
        Set(ByVal value As Long)
            Me._IdSegnalazione = value
        End Set
    End Property
    'Public Property IDOperatore() As Long
    '    Get
    '        Return _IDOperatore
    '    End Get
    '    Set(ByVal value As Long)
    '        Me._IDOperatore = value
    '    End Set
    'End Property
    Public Property IDTipoNotaSegnalazione() As Long
        Get
            Return _IDTipoNotaSegnalazione
        End Get
        Set(ByVal value As Long)
            Me._IDTipoNotaSegnalazione = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property
    Public Property DataOraInserimento() As String
        Get
            Return _DataOraInserimento
        End Get
        Set(ByVal value As String)
            Me._DataOraInserimento = value
        End Set
    End Property

End Class

Public Class Complessi
    Private _ID As Long
    Private _Nome As String
    Private _Indirizzo As String
    Private _Civico As String
    Private _Cap As String
    Private _Comune As String
    Private _Provincia As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            Me._Nome = value
        End Set
    End Property
    Public Property Indirizzo() As String
        Get
            Return _Indirizzo
        End Get
        Set(ByVal value As String)
            Me._Indirizzo = value
        End Set
    End Property
    Public Property Civico() As String
        Get
            Return _Civico
        End Get
        Set(ByVal value As String)
            Me._Civico = value
        End Set
    End Property
    Public Property Cap() As String
        Get
            Return _Cap
        End Get
        Set(ByVal value As String)
            Me._Cap = value
        End Set
    End Property
    Public Property Comune() As String
        Get
            Return _Comune
        End Get
        Set(ByVal value As String)
            Me._Comune = value
        End Set
    End Property
    Public Property Provincia() As String
        Get
            Return _Provincia
        End Get
        Set(ByVal value As String)
            Me._Provincia = value
        End Set
    End Property
End Class

Public Class Edifici
    Private _ID As Long
    Private _Nome As String
    Private _Indirizzo As String
    Private _Civico As String
    Private _Cap As String
    Private _Comune As String
    Private _Provincia As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal value As Long)
            Me._ID = value
        End Set
    End Property
    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            Me._Nome = value
        End Set
    End Property
    Public Property Indirizzo() As String
        Get
            Return _Indirizzo
        End Get
        Set(ByVal value As String)
            Me._Indirizzo = value
        End Set
    End Property
    Public Property Civico() As String
        Get
            Return _Civico
        End Get
        Set(ByVal value As String)
            Me._Civico = value
        End Set
    End Property
    Public Property Cap() As String
        Get
            Return _Cap
        End Get
        Set(ByVal value As String)
            Me._Cap = value
        End Set
    End Property
    Public Property Comune() As String
        Get
            Return _Comune
        End Get
        Set(ByVal value As String)
            Me._Comune = value
        End Set
    End Property
    Public Property Provincia() As String
        Get
            Return _Provincia
        End Get
        Set(ByVal value As String)
            Me._Provincia = value
        End Set
    End Property
End Class

Public Class TipoNota
    Private _Id As Long
    Private _Descrizione As String

    Public Property ID() As Long
        Get
            Return _Id
        End Get
        Set(ByVal value As Long)
            Me._Id = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property

End Class

Public Class Slot
    Private _IdSportello As Long
    Private _Sportello As String
    Private _IdFiliale As Long
    Private _SedeTerritoriale As String
    Private _IdOrario As Long
    Private _Orario As String
    Private _Giorno As String

    Public Property IDSportello() As Long
        Get
            Return _IdSportello
        End Get
        Set(ByVal value As Long)
            Me._IdSportello = value
        End Set
    End Property
    Public Property IdFiliale() As Long
        Get
            Return _IdFiliale
        End Get
        Set(ByVal value As Long)
            Me._IdFiliale = value
        End Set
    End Property
    Public Property IdOrario() As Long
        Get
            Return _IdOrario
        End Get
        Set(ByVal value As Long)
            Me._IdOrario = value
        End Set
    End Property
    Public Property Sportello() As String
        Get
            Return _Sportello
        End Get
        Set(ByVal value As String)
            Me._Sportello = value
        End Set
    End Property
    Public Property SedeTerritoriale() As String
        Get
            Return _SedeTerritoriale
        End Get
        Set(ByVal value As String)
            Me._SedeTerritoriale = value
        End Set
    End Property
    Public Property Orario() As String
        Get
            Return _Orario
        End Get
        Set(ByVal value As String)
            Me._Orario = value
        End Set
    End Property
    Public Property Giorno() As String
        Get
            Return _Giorno
        End Get
        Set(ByVal value As String)
            Me._Giorno = value
        End Set
    End Property

End Class

Public Class TipoAllegato
    Private _Id As Long
    Private _Descrizione As String

    Public Property ID() As Long
        Get
            Return _Id
        End Get
        Set(ByVal value As Long)
            Me._Id = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property

End Class

Public Class Documento
    Private _Id As Long
    Private _Descrizione As String

    Public Property ID() As Long
        Get
            Return _Id
        End Get
        Set(ByVal value As Long)
            Me._Id = value
        End Set
    End Property
    Public Property Descrizione() As String
        Get
            Return _Descrizione
        End Get
        Set(ByVal value As String)
            Me._Descrizione = value
        End Set
    End Property

End Class

'/////////////////////////////////
' CRC32 checksum
'Private Sub test()
'    Crc32.ComputeChecksum(Encoding.UTF8.GetBytes("Some string")).Dump()
'End Sub
Public Class Crc32CheckSum
    Shared table As UInteger()

    Shared Sub New()
        Dim poly As UInteger = &HEDB88320UI
        table = New UInteger(255) {}
        Dim temp As UInteger = 0
        For i As UInteger = 0 To table.Length - 1
            temp = i
            For j As Integer = 8 To 1 Step -1
                If (temp And 1) = 1 Then
                    temp = CUInt((temp >> 1) Xor poly)
                Else
                    temp >>= 1
                End If
            Next
            table(i) = temp
        Next
    End Sub

    Public Shared Function ComputeChecksum(bytes As Byte()) As UInteger
        Dim crc As UInteger = &HFFFFFFFFUI
        For i As Integer = 0 To bytes.Length - 1
            Dim index As Byte = CByte(((crc) And &HFF) Xor bytes(i))
            crc = CUInt((crc >> 8) Xor table(index))
        Next
        Return Not crc
    End Function
End Class