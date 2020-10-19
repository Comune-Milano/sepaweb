<%@ Application Language="VB" %>
 <script runat="server">
 Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Codice eseguito all\'avvio dell\'applicazione

    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Codice eseguito all\'arresto dell\'applicazione
       ' System.Web.HttpContext.Current.Session.Item("LAVORAZIONE") = "0"

    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Codice eseguito in caso di errore non gestito
        

        'Dim EventLogName As String = "SepaWeb"
        'If (Not EventLog.SourceExists(EventLogName)) Then
        '    EventLog.CreateEventSource(EventLogName, EventLogName)
        'End If

        '' Inserting into event log

        'Dim Log As New EventLog()
        'Log.Source = EventLogName
        'Log.WriteEntry(ErrorDescription, EventLogEntryType.Error)
        'Session.Add("ErroreGenerico", ErrorDescription)
        Try
        Catch ex As Exception

        End Try
        Server.ClearError()
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Codice eseguito all\'avvio di una nuova sessione
        
        System.Web.HttpContext.Current.Session.Add("INIZIO", "1")

    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsNothing(Server.GetLastError) Then
            Dim ErrorDescription As String = Server.GetLastError.ToString
            System.Web.HttpContext.Current.Session.Add("ErroreGenerico", ErrorDescription)

        End If
        If Not IsNothing(Session.Item("LAVORAZIONE")) Then
            Session.Item("LAVORAZIONE") = "0"

        End If

        ' Codice eseguito al termine di una sessione. 
        ' Nota: l\'evento Session_End viene generato solo quando la modalità sessionstate
        ' è impostata su InProc nel file Web.config. Se la modalità è impostata su StateServer 
        ' o SQLServer, l\'evento non viene generato.
    End Sub
    </script>