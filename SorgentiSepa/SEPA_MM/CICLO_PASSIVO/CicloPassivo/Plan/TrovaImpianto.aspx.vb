
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_TrovaImpianto
    Inherits PageSetIdMode
    Dim par As New CM.Global
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

        Reindirizza(Request.QueryString("ID"))


    End Sub

    Private Function Reindirizza(ByVal Indice As String)
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim ss As String = ""

            par.cmd.CommandText = "select * from siscom_mi.impianti where id=" & Indice
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                Session.Add("ID", Indice)





                Select Case par.IfNull(myReader("cod_tipologia"), "")

                    Case "ME" '"ACQUE METEORICHE"
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_Meteoriche.aspx?SL=1');</script>")

                    Case "AN" '"ANTINCENDIO"
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_Antincendio.aspx?SL=1');</script>")

                    Case "CF" '"CANNA FUMARIA"
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_CannaFumaria.aspx?SL=1');</script>")

                    Case "EL" '"ELETTRICO"
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_Elettrico.aspx?SL=1');</script>")

                    Case "ID" '"IDRICO"
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_Idrico.aspx?SL=1');</script>")

                    Case "SO" '"SOLLEVAMENTO"
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_Sollevamento.aspx?SL=1');</script>")
                        'Response.Redirect("Imp_Sollevamento.aspx")

                    Case "TR" '"TELERISCALDAMENTO"
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_Teleriscaldamento.aspx?SL=1');</script>")
                        'Response.Redirect("Imp_Teleriscaldamento.aspx")

                    Case "TA" '"TERMICO AUTONOMO"
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_RiscaldamentoA.aspx?SL=1');</script>")
                        'Response.Redirect("Imp_RiscaldamentoA.aspx")

                    Case "TE" '"TERMICO CENTRALIZZATO"
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_Riscaldamento.aspx?SL=1');</script>")
                        'Response.Redirect("Imp_Riscaldamento.aspx")

                    Case "TU" '"TUTELA IMMOBILE"
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_Tutela.aspx?SL=1');</script>")

                    Case "GA" '"GAS
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_Gas.aspx?SL=1');</script>")

                    Case "CI" '"CITOFONICO
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_Citofonico.aspx?SL=1');</script>")

                    Case "TV" '"TV
                        Response.Write("<script>location.replace('../../../IMPIANTI/Imp_TV.aspx?SL=1');</script>")

                    Case Else
                        Response.Write("Nessun impianto da visualizzare!")
                End Select
            End If
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function
End Class
