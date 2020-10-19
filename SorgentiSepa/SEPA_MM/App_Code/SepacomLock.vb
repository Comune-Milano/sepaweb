Imports System.ServiceModel
Imports System.ServiceModel.Activation
Imports System.ServiceModel.Web
Imports System.Collections.Generic
Imports System.Web.Script.Serialization

<ServiceContract(Namespace:="")>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class SepacomLock
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Public Enum EsitoLockUnlock
        Eccezione = -2
        Unknown = -1
        Success = 0
        Timeout = 1
        Deadlock = 2
        ParameterError = 3
        Already_own_lock = 4
        IllegalLockHandle = 5
        InUso = 100
        Scaduto = 101
        NonAutenticato = 102
        ShowOnly = 103
        NonAggiornato = 104
        NonInserito = 105
        NonCancellato = 106
    End Enum
    Public Class RispostaLockUnlock
        Public Property count As Integer
        Public Property comando As String
        Public Property descrizione As String
        Public Property name As String
        Public Property lockname As String
        Public Property expiration As String
        Public Property operazione As String
        Public Property esito As EsitoLockUnlock
        Public Property msgException As String
    End Class

    <OperationContract()>
    Public Function GestioneLock(ByVal comando As String, pageid As String, lockname As String) As String
        Me.connData = New CM.datiConnessione(par, False, False)
        Dim dati As New RispostaLockUnlock
        Dim risposta1 As String = ""
        Dim risposta2 As String = ""
        Dim recordAggiornati As Integer = -1
        Dim scadenza As String = DateTime.Now.AddSeconds(65).ToString
        scadenza = Mid(scadenza, 7, 4) & Mid(scadenza, 4, 2) & Mid(scadenza, 1, 2) & Mid(scadenza, 12, 2) & Mid(scadenza, 15, 2) & Mid(scadenza, 18, 2)
        Dim isMultiplo As Boolean = lockname.Contains(",")
        Dim listaIdFatti As New List(Of String)
        Dim nomeTabMultipli As String = ""
        Dim elencoIdMultipli() As String = New String() {}
        If isMultiplo Then
            Dim pos As Integer = lockname.LastIndexOf("_")
            elencoIdMultipli = lockname.Substring(pos + 1).Split(New Char() {","})
            nomeTabMultipli = lockname.Substring(0, pos)
        End If
        If IsNothing(HttpContext.Current.Session("OPERATORE")) Then
            dati.esito = EsitoLockUnlock.NonAutenticato
        Else
            connData.apri(False)
            dati.esito = EsitoLockUnlock.Unknown
            Dim utente As String = HttpContext.Current.Session("ID_OPERATORE")
            dati.msgException = ""
            Select Case comando
                Case "updatelock"
                    risposta1 = comando & "@" & pageid & "@" & lockname & "@"
                    If Not isMultiplo Then
                        par.cmd.CommandText = "UPDATE SEPACOM_LOCK SET EXPIRATION = " & par.insDbValue(scadenza, False) & " " _
                                            & "WHERE LOCK_NAME = " & par.insDbValue(lockname, True)
                        recordAggiornati = par.cmd.ExecuteNonQuery()
                        If recordAggiornati = 1 Then
                            dati.esito = EsitoLockUnlock.Success
                        Else
                            dati.esito = EsitoLockUnlock.NonAggiornato
                        End If
                    Else
                        Dim totale As Integer = 0
                        For Each s As String In elencoIdMultipli
                            Dim lockNameTmp As String = nomeTabMultipli & "_" & s
                            par.cmd.CommandText = "UPDATE SEPACOM_LOCK SET EXPIRATION = " & par.insDbValue(scadenza, False) & " " _
                                                & "WHERE LOCK_NAME = " & par.insDbValue(lockNameTmp, True)
                            totale += par.cmd.ExecuteNonQuery()
                            listaIdFatti.Add(lockNameTmp)
                        Next
                        If totale = elencoIdMultipli.Length Then
                            dati.esito = EsitoLockUnlock.Success
                        Else
                            dati.esito = EsitoLockUnlock.NonAggiornato
                        End If
                    End If
                Case "lock"
                    scadenza = DateTime.Now.ToString()
                    scadenza = Mid(scadenza, 7, 4) & Mid(scadenza, 4, 2) & Mid(scadenza, 1, 2) & Mid(scadenza, 12, 2) & Mid(scadenza, 15, 2) & Mid(scadenza, 18, 2)
                    par.cmd.CommandText = "DELETE FROM SEPACOM_LOCK WHERE EXPIRATION < " & par.insDbValue(scadenza, False)
                    par.cmd.ExecuteNonQuery()
                    scadenza = DateTime.Now.AddSeconds(65).ToString
                    scadenza = Mid(scadenza, 7, 4) & Mid(scadenza, 4, 2) & Mid(scadenza, 1, 2) & Mid(scadenza, 12, 2) & Mid(scadenza, 15, 2) & Mid(scadenza, 18, 2)

                    Try
                        risposta1 = comando & "@" & pageid
                        If Not isMultiplo Then
                            par.cmd.CommandText = "INSERT INTO SEPACOM_LOCK (LOCK_NAME, EXPIRATION, PAGEID, ID_OPERATORE) VALUES " _
                                                & "(" & par.insDbValue(lockname, True) & ", " & par.insDbValue(scadenza, False) & ", " & par.insDbValue(pageid, False) & ", " & par.insDbValue(utente, False) & ")"
                            recordAggiornati = par.cmd.ExecuteNonQuery
                            If recordAggiornati = 1 Then
                                dati.esito = EsitoLockUnlock.Success
                            Else
                                dati.esito = EsitoLockUnlock.NonInserito
                            End If
                        Else
                            Dim totale As Integer = 0
                            For Each s As String In elencoIdMultipli
                                Dim lockNameTmp As String = nomeTabMultipli & "_" & s
                                par.cmd.CommandText = "INSERT INTO SEPACOM_LOCK (LOCK_NAME, EXPIRATION, PAGEID, ID_OPERATORE) VALUES " _
                                                    & "(" & par.insDbValue(lockNameTmp, True) & ", " & par.insDbValue(scadenza, False) & ", " & par.insDbValue(pageid, False) & ", " & par.insDbValue(utente, False) & ")"
                                totale += par.cmd.ExecuteNonQuery
                                listaIdFatti.Add(lockNameTmp)
                            Next
                            If totale = elencoIdMultipli.Length Then
                                dati.esito = EsitoLockUnlock.Success
                            Else
                                dati.esito = EsitoLockUnlock.NonInserito
                            End If
                        End If
                    Catch ex As Exception
                        If ex.GetType().Name.ToLower.StartsWith("oracle") AndAlso ex.Message.Contains("ORA-00001") Then
                            If isMultiplo Then
                                For Each s In listaIdFatti
                                    Try
                                        par.cmd.CommandText = "DELETE FROM SEPACOM_LOCK WHERE PAGEID = " & par.insDbValue(pageid, False) & " " _
                                                            & "AND LOCK_NAME = " & par.insDbValue(s, True)
                                        par.cmd.ExecuteNonQuery()
                                    Catch ex2 As Exception
                                        'NULL
                                    End Try
                                Next
                            End If
                            dati.esito = EsitoLockUnlock.InUso
                            risposta2 = ""
                        Else
                            dati.esito = EsitoLockUnlock.Eccezione
                            risposta2 = ex.Message
                        End If
                        dati.msgException = ex.Message
                    End Try
                Case "releaselock"
                    Try
                        If Not isMultiplo Then
                            par.cmd.CommandText = "DELETE FROM SEPACOM_LOCK WHERE PAGEID = " & par.insDbValue(pageid, False) & " " _
                                                & "AND LOCK_NAME = " & par.insDbValue(lockname, True)
                            recordAggiornati = par.cmd.ExecuteNonQuery()
                            If recordAggiornati = 1 Then
                                dati.esito = EsitoLockUnlock.Success
                            Else
                                dati.esito = EsitoLockUnlock.NonCancellato
                            End If
                        Else
                            Dim totale As Integer = 0
                            For Each s As String In elencoIdMultipli
                                Dim lockNameTmp As String = nomeTabMultipli & "_" & s
                                par.cmd.CommandText = "DELETE FROM SEPACOM_LOCK WHERE PAGEID = " & par.insDbValue(pageid, False) & " " _
                                                    & "AND LOCK_NAME = " & par.insDbValue(lockNameTmp, True)
                                totale += par.cmd.ExecuteNonQuery
                                listaIdFatti.Add(lockNameTmp)
                            Next
                            If totale = elencoIdMultipli.Length Then
                                dati.esito = EsitoLockUnlock.Success
                            Else
                                dati.esito = EsitoLockUnlock.NonInserito
                            End If
                        End If
                    Catch ex As Exception
                        risposta2 = ex.Message
                        dati.esito = EsitoLockUnlock.Eccezione
                        dati.msgException = ex.Message
                    End Try
            End Select
            connData.chiudi(False)
        End If
        dati.count = recordAggiornati
        dati.descrizione = dati.esito.ToString + " " + risposta1 + " " + risposta2
        dati.comando = comando
        dati.lockname = lockname
        dati.expiration = scadenza
        Dim JSON As New JavaScriptSerializer()
        Return JSON.Serialize(dati)
    End Function
    <OperationContract()>
    Public Function getPageLock() As String
        getPageLock = "ERRORE"
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsNothing(HttpContext.Current.Session("OPERATORE")) Then
            connData.apri(False)
            getPageLock = par.getPageId()
            If Left(getPageLock, 1).ToString = "0" Then
                getPageLock = "1" & getPageLock
            End If
            par.cmd.CommandText = "UPDATE SEPACOM_LOCK_OP SET LOCK_PAGE = " & par.insDbValue(getPageLock, True) & " " _
                                & "WHERE ID_OPERATORE = " & HttpContext.Current.Session("ID_OPERATORE")
            par.cmd.ExecuteNonQuery()
            connData.chiudi(False)
            getPageLock = par.Cripta(getPageLock)
        End If
        Dim JSON As New JavaScriptSerializer()
        Return JSON.Serialize(getPageLock)
    End Function
End Class

