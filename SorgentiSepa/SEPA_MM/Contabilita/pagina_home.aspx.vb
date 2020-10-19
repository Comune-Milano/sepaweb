
Partial Class ANAUT_pagina_home
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim scriptblock As String
    Dim nGiorno As String
    Dim nGiornoRif As String
    Dim GiorniAp As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Label1.Text = System.Configuration.ConfigurationManager.AppSettings("Versione")
        If Not IsPostBack Then
            Session.LCID = 1040

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

            'If InStr(nGiornoRif, nGiorno) = 0 Then
            '    scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
            '                & "</script>"
            '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
            '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
            '    End If
            '    Response.Write("<script>top.location.href=""Portale.aspx""</script>")
            '    Exit Sub
            'End If

            'If Val(Format(Hour(Now), "00") & Format(Minute(Now), "00")) < Val(System.Configuration.ConfigurationManager.AppSettings("OraAp") & "00") Then
            '    scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
            '                & "</script>"
            '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
            '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
            '    End If
            '    Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            '    Exit Sub
            'End If

            'If Val(Format(Hour(Now), "00") & Format(Minute(Now), "00")) > Val(System.Configuration.ConfigurationManager.AppSettings("OraCh") & "00") Then
            '    scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
            '                & "</script>"
            '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
            '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
            '    End If
            '    Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            '    Exit Sub
            'End If

            
            txtmessaggio.Text = Session.Item("ORARIO")
            Label3.Text = Session.Item("ORARIO")



        End If



    End Sub
End Class
