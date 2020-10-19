
Partial Class ANAUT_pagina_home
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim scriptblock As String
    Dim nGiorno As String
    Dim nGiornoRif As String
    Dim GiorniAp As String


    '*** EPIFANI

    Public lstScambiatori As New System.Collections.Generic.List(Of Epifani.Generatori)
    Public lstGeneratori As New System.Collections.Generic.List(Of Epifani.Generatori)
    Public lstBruciatori As New System.Collections.Generic.List(Of Epifani.Bruciatori)
    Public lstPompe As New System.Collections.Generic.List(Of Epifani.Pompe)
    Public lstPompeS As New System.Collections.Generic.List(Of Epifani.PompeS)
    Public lstSerbatoi As New System.Collections.Generic.List(Of Epifani.Serbatoi)
    Public lstPreSerbatoi As New System.Collections.Generic.List(Of Epifani.Serbatoi)
    Public lstControlloRendimento As New System.Collections.Generic.List(Of Epifani.ControlloRendimento)


    Public lstEdifici As New System.Collections.Generic.List(Of Epifani.Edifici)
    Public lstEdificiScale As New System.Collections.Generic.List(Of Epifani.EdificiScale)

    Public lstPortineria As New System.Collections.Generic.List(Of Epifani.Portineria)
    Public lstBox As New System.Collections.Generic.List(Of Epifani.Box)
    Public lstQuadroSE As New System.Collections.Generic.List(Of Epifani.Quadro)
    Public lstQuadroSC As New System.Collections.Generic.List(Of Epifani.Quadro)

    Public lstScale As New System.Collections.Generic.List(Of Epifani.Scale)
    Public lstScaleSel As New System.Collections.Generic.List(Of Epifani.Scale)
    Public lstScaleSel2 As New System.Collections.Generic.List(Of Epifani.Scale)

    Public lstTV As New System.Collections.Generic.List(Of Epifani.TV)
    Public lstEdificiTV As New System.Collections.Generic.List(Of Epifani.Scale)
    Public lstEdificiTVSel As New System.Collections.Generic.List(Of Epifani.Scale)

    Public lstPompeSM As New System.Collections.Generic.List(Of Epifani.PompeSM)

    Public lstSerbatoiAccumulo As New System.Collections.Generic.List(Of Epifani.SerbatoiAccumulo)
    Public lstMotopompe As New System.Collections.Generic.List(Of Epifani.Motopompe)

    Public lstCancelli As New System.Collections.Generic.List(Of Epifani.Cancelli)
    Public lstCitofoni As New System.Collections.Generic.List(Of Epifani.Citofoni)
    Public lstAlloggi As New System.Collections.Generic.List(Of Epifani.Alloggi)
    Public lstCarrabile As New System.Collections.Generic.List(Of Epifani.Carrabile)


    Public lstVerifiche As New System.Collections.Generic.List(Of Epifani.VerificheImpianti)
    Public lstVerifiche2 As New System.Collections.Generic.List(Of Epifani.VerificheImpianti)

    Public lstVerificheRicerche As New System.Collections.Generic.List(Of Epifani.VerificheRicerche)

    Public lstSprinkler As New System.Collections.Generic.List(Of Epifani.Sprinkler)
    Public lstFumi As New System.Collections.Generic.List(Of Epifani.RilevatoreFumi)
    Public lstIdranti As New System.Collections.Generic.List(Of Epifani.Idranti)
    Public lstPiani As New System.Collections.Generic.List(Of Epifani.Scale)
    Public lstPianiSel As New System.Collections.Generic.List(Of Epifani.Scale)
    Public lstPianiAutoPompa As New System.Collections.Generic.List(Of Epifani.Scale)
    Public lstPianiAutoPompaSel As New System.Collections.Generic.List(Of Epifani.Scale)
    Public lstEstintori As New System.Collections.Generic.List(Of Epifani.Estintori)
    Public lstAutoPompa As New System.Collections.Generic.List(Of Epifani.AutoPompa)

    '************************************

    Public ListaEdificiCT As New System.Collections.Generic.List(Of Epifani.EdificiCT)
    Public ListaUI As New System.Collections.Generic.List(Of Epifani.ListaUI)

    Public ListaEdificiCT_Extra As New System.Collections.Generic.List(Of Epifani.EdificiCT)
    Public ListaUI_Extra As New System.Collections.Generic.List(Of Epifani.ListaUI)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Label1.Text = System.Configuration.ConfigurationManager.AppSettings("Versione")
        If Not IsPostBack Then
            Session.LCID = 1040




            '*** EPIFANI
            Session.Add("LSTSCAMBIATORI", lstScambiatori)
            Session.Add("LSTGENERATORI", lstGeneratori)
            Session.Add("LSTBRUCIATORI", lstBruciatori)
            Session.Add("LSTPOMPE", lstPompe)
            Session.Add("LSTPOMPES", lstPompeS)
            Session.Add("LSTSERBATOI", lstSerbatoi)
            Session.Add("LSTPRESERBATOI", lstPreSerbatoi)
            Session.Add("LSTCONTROLLORENDIMENTO", lstControlloRendimento)


            Session.Add("LSTEDIFICI", lstEdifici)
            Session.Add("LSTEDIFICISCALE", lstEdificiScale)

            Session.Add("LSTPORTINERIA", lstPortineria)
            Session.Add("LSTBOX", lstBox)
            Session.Add("LSTQUADRO_SE", lstQuadroSE)
            Session.Add("LSTQUADRO_SC", lstQuadroSC)

            Session.Add("LSTSCALE", lstScale)
            Session.Add("LSTSCALE_SEL", lstScaleSel)
            Session.Add("LSTSCALE_SEL2", lstScaleSel2)

            Session.Add("LSTTV", lstTV)
            Session.Add("LSTTV_EDIFICI", lstEdificiTV)
            Session.Add("LSTTV_EDIFICISEL", lstEdificiTVSel)

            Session.Add("LSTPOMPESM", lstPompeSM)

            Session.Add("LSTSERBATOIACCUMULO", lstSerbatoiAccumulo)
            Session.Add("LSTMOTOPOMPE", lstMotopompe)

            Session.Add("LSTCANCELLI", lstCancelli)
            Session.Add("LSTCITOFONI", lstCitofoni)
            Session.Add("LSTALLOGGI", lstAlloggi)
            Session.Add("LSTCARRABILE", lstCarrabile)

            Session.Add("LSTVERIFICHE", lstVerifiche)
            Session.Add("LSTVERIFICHE2", lstVerifiche2)

            Session.Add("LSTVERIFICHE_RICERCHE", lstVerificheRicerche)

            Session.Add("LSTSPRINKLER", lstSprinkler)
            Session.Add("LSTFUMI", lstFumi)
            Session.Add("LSTIDRANTI", lstIdranti)
            Session.Add("LSTPIANIIDRANTI", lstPiani)
            Session.Add("LSTPIANIIDRANTI_SEL", lstPianiSel)
            Session.Add("LSTESTINTORI", lstEstintori)

            Session.Add("LSTPIANIAUTOPOMPA", lstPianiAutoPompa)
            Session.Add("LSTPIANIAUTOPOMPA_SEL", lstPianiAutoPompaSel)

            Session.Add("LSTAUTOPOMPA", lstAutoPompa)

            Session.Add("LST_EDIFICI_CT", ListaEdificiCT)
            Session.Add("LST_UI", ListaUI)

            Session.Add("LST_EDIFICI_CT_EXTRA", ListaEdificiCT_Extra)
            Session.Add("LST_UI_EXTRA", ListaUI_Extra)
            '******************************

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

           
            Me.txtMessaggio.Value = Session.Item("ORARIO")
            Label3.Text = Session.Item("ORARIO")



        End If

    End Sub
End Class
