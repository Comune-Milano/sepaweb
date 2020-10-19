Imports Microsoft.VisualBasic

Namespace Verifica
    Public Class SqlInjectionScreeningModule
        Implements IHttpModule
        Public Shared blackList As String() = Nothing

        Private Function getInjection(ByVal sender As Object) As String()
            getInjection = Nothing
            Dim Application = TryCast(sender, HttpApplication).Application
            getInjection = Application("Injection")
        End Function

        Public Sub Dispose() Implements IHttpModule.Dispose
            'no-op 
        End Sub

        'Tells ASP.NET that there is code to run during BeginRequest
        Public Sub Init(ByVal app As HttpApplication) Implements IHttpModule.Init
            AddHandler app.BeginRequest, AddressOf app_BeginRequest
        End Sub

        'For each incoming request, check the query-string, form and cookie values for suspicious values.
        Private Sub app_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
            Dim Request As HttpRequest = TryCast(sender, HttpApplication).Context.Request
            If InStr(UCase(Request.Path), "ASPX") > 0 Then
                For Each key As String In Request.QueryString
                    CheckInput(Request.QueryString(key), sender)
                Next
            End If
            'For Each key As String In Request.Form
            '    CheckInput(Request.Form(key))
            'Next
            'For Each key As String In Request.Cookies
            '    CheckInput(Request.Cookies(key).Value)
            'Next
        End Sub

        'The utility method that performs the blacklist comparisons
        'You can change the error handling, and error redirect location to whatever makes sense for your site.
        Private Sub CheckInput(ByVal parameter As String, ByVal sender As Object)
            blackList = getInjection(sender)
            If Not IsNothing(blackList) Then
                If blackList.Length > 0 Then
                    For i As Integer = 0 To blackList.Length - 1
                        If parameter.Contains("/*") Or parameter.Contains("*\") Then
                            HttpContext.Current.Response.Redirect("~/AccessoErrore.htm", False)
                            Exit Sub
                        End If
                        If (parameter.IndexOf(blackList(i), StringComparison.OrdinalIgnoreCase) >= 0) Then
                            Dim List As String() = Nothing
                            List = parameter.Split(" ")
                            For z As Integer = 0 To List.Length - 1
                                If List(z).Length = blackList(i).Length Then
                                    If List(z).ToString.ToUpper = blackList(i).ToString.ToUpper Then
                                        HttpContext.Current.Response.Redirect("~/AccessoErrore.htm", False)
                                        Exit Sub
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If
            End If
        End Sub
    End Class
End Namespace