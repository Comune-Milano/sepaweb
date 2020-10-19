
Partial Class ANAUT_CalendarioSportello
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim strScript As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0
        If IsPostBack = False Then


            Dim i As Integer = 0


            SPORTELLO.Value = Request.QueryString("S")
            FILIALE.Value = Request.QueryString("F")

            GiornoApp = Format(Now, "yyyyMMdd")
            OraApp = "08.00"

            GiornoPartenza = Format(DateAdd("d", CDate(par.FormattaData(GiornoApp)).DayOfWeek * -1, CDate(par.FormattaData(GiornoApp))), "yyyyMMdd")
            GiornoArrivo = Format(DateAdd("d", 6, CDate(par.FormattaData(GiornoPartenza))), "yyyyMMdd")

            Calendar1.TodaysDate = CDate(par.FormattaData(GiornoApp))

            CaricaDisponibilita(Format(Calendar1.TodaysDate.Month, "00"), CStr(Calendar1.TodaysDate.Year))
            CaricaDati()
            CaricaCalendario()
            imgAnnulla.Visible = False
        End If
        Label25.Text = testodomanda.Value
        Label5.Text = testodomanda1.value


    End Sub


    Private Function CaricaCalendario()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim i As Integer

            TabellaGiorni = ""
            'For i = 0 To 21 'orario
            i = 0
            TabellaGiorni = "<tr><td valign='top'><table cellpadding='0' cellspacing='0'>" & Tabella("Domenica", i, GiornoPartenza) & "</table></td><td valign='top'><table cellpadding='0' cellspacing='0'>" & Tabella("Lunedì", i, Format(DateAdd("d", 1, CDate(par.FormattaData(GiornoPartenza))), "yyyyMMdd")) & "</table></td><td valign='top'><table cellpadding='0' cellspacing='0'>" & Tabella("Martedì", i, Format(DateAdd("d", 2, CDate(par.FormattaData(GiornoPartenza))), "yyyyMMdd")) & "</table></td><td valign='top'><table cellpadding='0' cellspacing='0'>" & Tabella("Mercoledì", i, Format(DateAdd("d", 3, CDate(par.FormattaData(GiornoPartenza))), "yyyyMMdd")) & "</table></td><td valign='top'><table cellpadding='0' cellspacing='0'>" & Tabella("Giovedì", i, Format(DateAdd("d", 4, CDate(par.FormattaData(GiornoPartenza))), "yyyyMMdd")) & "</table></td><td valign='top'><table cellpadding='0' cellspacing='0'>" & Tabella("Venerdì", i, Format(DateAdd("d", 5, CDate(par.FormattaData(GiornoPartenza))), "yyyyMMdd")) & "</table></td><td valign='top'><table cellpadding='0' cellspacing='0'>" & Tabella("Sabato", i, Format(DateAdd("d", 6, CDate(par.FormattaData(GiornoPartenza))), "yyyyMMdd")) & "</table></td></tr>"
            'Next

            If Len(TabellaGiorni) = 492 Then
                TabellaGiorni = "<tr><td td style='text-align: center'>NESSUN APPUNTAMENTO DA GESTIRE IN QUESTO PERIODO</td></tr>"

            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Function


    Private Function Tabella(ByVal giorno As String, ByVal Orario As Integer, ByVal GiornoAppuntamento As String) As String

        Try
            Dim Ora As String = ""
            Dim Cognome As String = ""
            Dim Nome As String = ""
            Dim CodContratto As String = ""
            Dim ColoreSfondo As String = ""
            Dim ColoreSfondo1 As String = ""
            Dim LinkSposta As String = ""
            Dim LinkReimposta As String = ""
            Dim LinkAnnulla As String = ""
            Dim LinkCrea As String = ""
            Dim InfoLink As String = ""
            Dim NomeImmagine As String = ""
            Dim NomeImmagine1 As String = ""
            Dim NomeImmagine2 As String = ""
            Dim NomeImmagine3 As String = ""
            Dim destinatario As String = ""
            Dim cursore As String = ""
            Dim LinkRipristina As String = ""

            Dim indiceTabella As String = ""

            Dim TabAnnullati As String = ""

            NomeImmagine = "img\SpostaAppuntamento.png"
            NomeImmagine1 = "img\ReimpostaAppuntamento.png"
            NomeImmagine2 = "img\AnnullaAppuntamento.png"
            NomeImmagine3 = "img\CreaScheda.png"
          
            par.cmd.CommandText = "SELECT AGENDA_APPUNTAMENTI.* FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_FILIALE=" & FILIALE.Value & " AND N_OPERATORE='" & SPORTELLO.Value & "' AND substr(INIZIO,1,8)='" & GiornoAppuntamento & "' and to_number(substr(INIZIO,9,4))>=800 and to_number(substr(INIZIO,9,4))<=1830 order by inizio asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader1.Read

                TabAnnullati = ""

                Ora = Mid(par.IfNull(myReader1("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("inizio"), ""), 11, 2)
                Cognome = par.IfNull(myReader1("cognome"), "&nbsp;")
                Nome = par.IfNull(myReader1("nome"), "&nbsp;")
                CodContratto = par.IfNull(myReader1("cod_contratto"), "&nbsp;")

                Select Case par.IfNull(myReader1("tipo_f_oraria"), "")
                    Case "0"
                        If CodContratto <> "&nbsp;" Then
                            ColoreSfondo1 = "#C0C0C0"
                            If Session.Item("MOD_AU_CONV_SPOSTA") = "1" Then
                                If par.IfNull(myReader1("ID_STATO"), "") <> "2" Then
                                    LinkSposta = "<a href='javascript:void(0)' onclick=" & Chr(34) & "Sposta(" & myReader1("ID") & ",'" & par.IfNull(myReader1("cognome"), "") & "','" & par.IfNull(myReader1("nome"), "") & "','" & par.FormattaData(Mid(par.IfNull(myReader1("inizio"), ""), 1, 8)) & " ore " & Mid(par.IfNull(myReader1("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("inizio"), ""), 11, 2) & "');" & Chr(34) & " ><img alt=" & Chr(34) & "Sposta  questo appuntamento" & Chr(34) & " src='" & NomeImmagine & "' border=" & Chr(34) & "0" & Chr(34) & "/></a>"
                                Else
                                    LinkSposta = "&nbsp;"
                                End If
                            Else
                                LinkSposta = "&nbsp;"
                            End If

                            If Session.Item("MOD_AU_CONV_REI") = "1" Then
                                If par.IfNull(myReader1("ID_STATO"), "") <> "2" Then
                                    LinkReimposta = "<a href='javascript:void(0)' onclick=" & Chr(34) & "Reimposta(" & myReader1("ID") & ",'" & par.IfNull(myReader1("cognome"), "") & "','" & par.IfNull(myReader1("nome"), "") & "','" & par.FormattaData(Mid(par.IfNull(myReader1("inizio"), ""), 1, 8)) & " ore " & Mid(par.IfNull(myReader1("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("inizio"), ""), 11, 2) & "');" & Chr(34) & " ><img alt=" & Chr(34) & "Reimposta questo appuntamento" & Chr(34) & " src='" & NomeImmagine1 & "' border=" & Chr(34) & "0" & Chr(34) & "/></a>"
                                Else
                                    LinkReimposta = "&nbsp;"
                                End If
                            Else
                                LinkReimposta = "&nbsp;"
                            End If

                            If Session.Item("MOD_AU_CONV_ANN") = "1" Then
                                If par.IfNull(myReader1("ID_STATO"), "") <> "2" Then
                                    LinkAnnulla = "<a href='javascript:void(0)' onclick=" & Chr(34) & "Annulla(" & myReader1("ID") & "," & myReader1("ID_CONVOCAZIONE") & "," & myReader1("ID_CONTRATTO") & ");" & Chr(34) & " ><img alt=" & Chr(34) & "Sospendi questo appuntamento" & Chr(34) & " src='" & NomeImmagine2 & "' border=" & Chr(34) & "0" & Chr(34) & "/></a>"
                                Else
                                    LinkAnnulla = "&nbsp;"
                                End If
                            Else
                                LinkAnnulla = "&nbsp;"
                            End If

                            If Session.Item("MOD_AU_CONV_N") = "1" Then
                                If par.IfNull(myReader1("ID_STATO"), "") <> "2" Then
                                    LinkCrea = "<a href='javascript:void(0)' onclick=" & Chr(34) & "CreaAnagrafe(" & myReader1("ID") & "," & myReader1("ID_CONVOCAZIONE") & "," & myReader1("ID_CONTRATTO") & ");" & Chr(34) & " ><img alt=" & Chr(34) & "Crea scheda AU" & Chr(34) & " src='" & NomeImmagine3 & "' border=" & Chr(34) & "0" & Chr(34) & "/></a>"
                                Else
                                    LinkCrea = "&nbsp;"
                                End If
                            Else
                                LinkCrea = "&nbsp;"
                            End If


                            InfoLink = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReader1("ID_CONTRATTO") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & " ><img alt=" & Chr(34) & "Informazioni Utente" & Chr(34) & " src=" & Chr(34) & "info-icon.png" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & "/></a>"
                            destinatario = ""
                            cursore = ""
                        Else
                            ColoreSfondo1 = "#FFFFFF"
                            LinkSposta = "&nbsp;"
                            LinkReimposta = "&nbsp;"
                            LinkAnnulla = "&nbsp;"
                            InfoLink = "&nbsp;"
                            LinkCrea = "&nbsp;"
                            destinatario = " onclick='Assegna(" & myReader1("ID") & "," & Mid(par.IfNull(myReader1("inizio"), ""), 1, 8) & "," & Mid(par.IfNull(myReader1("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("inizio"), ""), 11, 2) & ")' "
                            cursore = " cursor:pointer; "

                            LinkRipristina = "&nbsp;"
                            If ChAnnullati.Checked = True Then


                                par.cmd.CommandText = "SELECT CONVOCAZIONI_AU.*,rapporti_utenza.cod_contratto FROM SISCOM_MI.CONVOCAZIONI_AU,siscom_mi.rapporti_utenza WHERE rapporti_utenza.id=convocazioni_au.id_contratto and ID_STATO=1 AND ID_FILIALE=" & FILIALE.Value & " AND N_OPERATORE='" & SPORTELLO.Value & "' AND DATA_APP='" & GiornoAppuntamento & "' and ORE_APP='" & Mid(par.IfNull(myReader1("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("inizio"), ""), 11, 2) & "'"
                                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderA.Read Then

                                    If Session.Item("MOD_AU_CONV_RIP") = "1" Then
                                        LinkRipristina = "<a href='javascript:void(0)' onclick=" & Chr(34) & "Ripristina(" & myReader1("ID") & "," & myReaderA("ID") & "," & myReaderA("ID_CONTRATTO") & ");" & Chr(34) & " ><img alt='Ripristina Appuntamento' src='img/RipristinaAppuntamento.png' border='0'/></a>"
                                    Else
                                        LinkRipristina = "&nbsp;"
                                    End If

                                    TabAnnullati = "<table  cellpadding='0' cellspacing='0' style='border: 1px solid #000099; width: 120px; height: 120px;'>" _
                                                     & "<tr>" _
                                                    & "<td>" _
                                                    & "<table cellpadding='0' cellspacing='0' style='width: 100%; background-color: #FFFFFF;'>" _
                                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 9pt'>" _
                                                    & "<td style='text-align: center'>&nbsp;</td>" _
                                                    & "</tr>" _
                                                    & "<tr style='font-family: Arial, Helvetica, sans-serif; font-size: 9pt'>" _
                                                    & "<td style='text-align: center'>&nbsp;</td>" _
                                                    & "</tr>" _
                                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 9pt'>" _
                                                    & "<td style='text-align: center'>&nbsp;</td>" _
                                                    & "</tr>" _
                                                    & "</table>" _
                                                    & "</td>" _
                                                    & "</tr>" _
                                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: #FFFFFF'>" _
                                                    & "<td style='text-align: center'>&nbsp;</td>" _
                                                    & "</tr>" _
                                                    & "<tr style='height: 25px;'>" _
                                                    & "<td style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: #FFFFFF; text-align: right;'>&nbsp;</td>" _
                                                    & "</tr>" _
                                                    & "<tr>" _
                                                    & "<td>" _
                                                    & "<table cellpadding='0' cellspacing='0' style='width:100%;'>" _
                                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold'>" _
                                                    & "<td style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: #808080; text-align: center;'>" & myReaderA("cod_contratto") & "</td>" _
                                                    & "</tr>" _
                                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold; background-color: #808080'>" _
                                                    & "<td style='text-align: center'>" & Mid(myReaderA("cognome"), 1, 15) & "</td>" _
                                                    & "</tr>" _
                                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold; background-color: #808080'>" _
                                                    & "<td style='text-align: center'>" & Mid(myReaderA("nome"), 1, 15) & "</td>" _
                                                    & "</tr>" _
                                                    & "<tr style='background-color: #808080'><td>&nbsp; &nbsp;</td>" _
                                                    & "</tr>" _
                                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: #808080;height: 30px'>" _
                                                    & "<td style='text-align: center'>" & LinkRipristina & "</td>" _
                                                    & "</tr>" _
                                                    & "</table>" _
                                                    & "</td>" _
                                                    & "</tr>" _
                                                    & "</table>"
                                End If
                                myReaderA.Close()
                            End If
                        End If
                    Case "4"
                            If chFuoriOrario.Checked = True Then
                                If CodContratto <> "&nbsp;" Then
                                    ColoreSfondo1 = "#C0C0C0"

                                If Session.Item("MOD_AU_CONV_SPOSTA") = "1" Then
                                    If par.IfNull(myReader1("ID_STATO"), "") <> "2" Then
                                        LinkSposta = "<a href='javascript:void(0)' onclick=" & Chr(34) & "Sposta(" & myReader1("ID") & ",'" & par.IfNull(myReader1("cognome"), "") & "','" & par.IfNull(myReader1("nome"), "") & "','" & par.FormattaData(Mid(par.IfNull(myReader1("inizio"), ""), 1, 8)) & " ore " & Mid(par.IfNull(myReader1("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("inizio"), ""), 11, 2) & "');" & Chr(34) & " ><img alt=" & Chr(34) & "Sposta questo appuntamento" & Chr(34) & " src=" & Chr(34) & NomeImmagine & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & "/></a>" '"&nbsp;"
                                    Else
                                        LinkSposta = "&nbsp;"
                                    End If

                                Else
                                    LinkSposta = "&nbsp;"
                                End If

                                If Session.Item("MOD_AU_CONV_REI") = "1" Then
                                    If par.IfNull(myReader1("ID_STATO"), "") <> "2" Then
                                        LinkReimposta = "<a href='javascript:void(0)' onclick=" & Chr(34) & "Reimposta(" & myReader1("ID") & ",'" & par.IfNull(myReader1("cognome"), "") & "','" & par.IfNull(myReader1("nome"), "") & "','" & par.FormattaData(Mid(par.IfNull(myReader1("inizio"), ""), 1, 8)) & " ore " & Mid(par.IfNull(myReader1("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("inizio"), ""), 11, 2) & "');" & Chr(34) & " ><img alt=" & Chr(34) & "Reimposta questo appuntamento" & Chr(34) & " src=" & Chr(34) & NomeImmagine1 & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & "/></a>"
                                    Else
                                        LinkReimposta = "&nbsp;"
                                    End If
                                Else
                                    LinkReimposta = "&nbsp;"
                                End If
                                If Session.Item("MOD_AU_CONV_ANN") = "1" Then
                                    If par.IfNull(myReader1("ID_STATO"), "") <> "2" Then
                                        LinkAnnulla = "<a href='javascript:void(0)' onclick=" & Chr(34) & "Annulla(" & myReader1("ID") & "," & myReader1("ID_CONVOCAZIONE") & "," & myReader1("ID_CONTRATTO") & ");" & Chr(34) & " ><img alt='Sospendi questo appuntamento' src=" & Chr(34) & NomeImmagine2 & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & "/></a>"
                                    Else
                                        LinkAnnulla = "&nbsp;"
                                    End If
                                Else
                                    LinkAnnulla = "&nbsp;"
                                End If

                                If Session.Item("MOD_AU_CONV_N") = "1" Then
                                    If par.IfNull(myReader1("ID_STATO"), "") <> "2" Then
                                        LinkCrea = "<a href='javascript:void(0)' onclick=" & Chr(34) & "CreaAnagrafe(" & myReader1("ID") & "," & myReader1("ID_CONVOCAZIONE") & "," & myReader1("ID_CONTRATTO") & ");" & Chr(34) & " ><img alt=" & Chr(34) & "Crea scheda AU" & Chr(34) & " src='" & NomeImmagine3 & "' border=" & Chr(34) & "0" & Chr(34) & "/></a>"

                                    Else
                                        LinkCrea = "&nbsp;"
                                    End If
                                Else
                                    LinkCrea = "&nbsp;"
                                End If

                                InfoLink = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReader1("ID_CONTRATTO") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & " ><img alt=" & Chr(34) & "Informazioni Utente" & Chr(34) & " src=" & Chr(34) & "info-icon.png" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & "/></a>"
                                destinatario = ""
                                cursore = ""
                            Else
                                ColoreSfondo1 = "#FFFFFF"
                                LinkSposta = "&nbsp;"
                                LinkReimposta = "&nbsp;"
                                LinkAnnulla = "&nbsp;"
                                InfoLink = "&nbsp;"
                                LinkCrea = "&nbsp;"
                                destinatario = " onclick='Assegna(" & myReader1("ID") & "," & Mid(par.IfNull(myReader1("inizio"), ""), 1, 8) & "," & Mid(par.IfNull(myReader1("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("inizio"), ""), 11, 2) & ")' "
                                cursore = " cursor:pointer; "
                                LinkRipristina = "&nbsp;"

                                If ChAnnullati.Checked = True Then


                                    par.cmd.CommandText = "SELECT CONVOCAZIONI_AU.*,rapporti_utenza.cod_contratto FROM SISCOM_MI.CONVOCAZIONI_AU,siscom_mi.rapporti_utenza WHERE rapporti_utenza.id=convocazioni_au.id_contratto and ID_STATO=1 AND ID_FILIALE=" & FILIALE.Value & " AND N_OPERATORE='" & SPORTELLO.Value & "' AND DATA_APP='" & GiornoAppuntamento & "' and ORE_APP='" & Mid(par.IfNull(myReader1("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("inizio"), ""), 11, 2) & "'"
                                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderA.Read Then

                                        If Session.Item("MOD_AU_CONV_RIP") = "1" Then
                                            LinkRipristina = "<a href='javascript:void(0)' onclick=" & Chr(34) & "Ripristina(" & myReader1("ID") & "," & myReaderA("ID") & "," & myReaderA("ID_CONTRATTO") & ");" & Chr(34) & " ><img alt='Ripristina Appuntamento' src='Ripristina.png' border='0'/></a>"
                                        Else
                                            LinkRipristina = "&nbsp;"
                                        End If

                                        TabAnnullati = "<table  cellpadding='0' cellspacing='0' style='border: 1px solid #000099; width: 120px; height: 120px;'>" _
                                                         & "<tr>" _
                                                        & "<td>" _
                                                        & "<table cellpadding='0' cellspacing='0' style='width: 100%; background-color: #FFFFFF;'>" _
                                                        & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 9pt'>" _
                                                        & "<td style='text-align: center'>&nbsp;</td>" _
                                                        & "</tr>" _
                                                        & "<tr style='font-family: Arial, Helvetica, sans-serif; font-size: 9pt'>" _
                                                        & "<td style='text-align: center'>&nbsp;</td>" _
                                                        & "</tr>" _
                                                        & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 9pt'>" _
                                                        & "<td style='text-align: center'>&nbsp;</td>" _
                                                        & "</tr>" _
                                                        & "</table>" _
                                                        & "</td>" _
                                                        & "</tr>" _
                                                        & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: #FFFFFF'>" _
                                                        & "<td style='text-align: center'>&nbsp;</td>" _
                                                        & "</tr>" _
                                                        & "<tr style='height: 25px;'>" _
                                                        & "<td style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: #FFFFFF; text-align: right;'>&nbsp;</td>" _
                                                        & "</tr>" _
                                                        & "<tr>" _
                                                        & "<td>" _
                                                        & "<table cellpadding='0' cellspacing='0' style='width:100%;'>" _
                                                        & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold'>" _
                                                        & "<td style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: #808080; text-align: center;'>" & myReaderA("cod_contratto") & "</td>" _
                                                        & "</tr>" _
                                                        & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold; background-color: #808080'>" _
                                                        & "<td style='text-align: center'>" & Mid(myReaderA("cognome"), 1, 15) & "</td>" _
                                                        & "</tr>" _
                                                        & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold; background-color: #808080'>" _
                                                        & "<td style='text-align: center'>" & Mid(myReaderA("nome"), 1, 15) & "</td>" _
                                                        & "</tr>" _
                                                        & "<tr style='background-color: #808080'><td>&nbsp; &nbsp;</td>" _
                                                        & "</tr>" _
                                                        & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: #808080;height: 30px'>" _
                                                        & "<td style='text-align: center'>" & LinkRipristina & "</td>" _
                                                        & "</tr>" _
                                                        & "</table>" _
                                                        & "</td>" _
                                                        & "</tr>" _
                                                        & "</table>"
                                    End If
                                    myReaderA.Close()
                                End If

                            End If
                        Else
                            ColoreSfondo1 = "#808080"
                            LinkSposta = "&nbsp;"
                            LinkReimposta = "&nbsp;"
                            LinkAnnulla = "&nbsp;"
                            InfoLink = "&nbsp;"
                            LinkCrea = "&nbsp;"
                            destinatario = ""
                            cursore = ""
                        End If
                    Case Else
                            Cognome = "&nbsp;"
                            Nome = "&nbsp;"
                            CodContratto = "&nbsp;"
                            ColoreSfondo1 = "#808080"
                            LinkSposta = "&nbsp;" 'LinkSposta = "<img alt='Sposta qui l'appuntamento' src='" & NomeImmagine & "' border='0'/>" '"&nbsp;"ù
                            LinkReimposta = "&nbsp;"
                        LinkAnnulla = "&nbsp;"
                        LinkCrea = "&nbsp;"
                            InfoLink = "&nbsp;" '"<img alt='Informazioni Utente' src='info-icon.png' border='0'/>"
                            destinatario = ""
                            cursore = ""
                End Select

                Dim Script As String = ""

                If GiornoAppuntamento = GiornoApp And Ora = OraApp And CodContratto <> "&nbsp;" Then
                    ColoreSfondo = "#FF5050"
                    indiceTabella = "maxTA"
                    Script = "var obj = document.getElementById('maxTA');var posX = obj.offsetLeft; var posY = obj.offsetTop; " _
    & "while (obj.offsetParent) {posX = posX + obj.offsetParent.offsetLeft;posY = posY + obj.offsetParent.offsetTop;" _
    & "if (obj == document.getElementsByTagName('body')[0]) { break }else { obj = obj.offsetParent; }" _
    & "}" _
      & " document.getElementById('yPos').value = posY;document.getElementById('xPos').value = posX;"
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(UpdatePanel), Page.ClientID, Script, True)
                Else
                    ColoreSfondo = "#FFFFCC"
                    indiceTabella = myReader1("inizio")
                End If

                Tabella = Tabella & "<tr><td>"

                If TabAnnullati <> "" Then
                    Tabella = Tabella & "<table align='left'><tr><td>"
                End If

                Tabella = Tabella & "<table id=" & indiceTabella & " " & destinatario & " cellpadding='0' cellspacing='0' style='border: 1px solid #000099; width: 150px; height: 120px; " & cursore & "'>" _
                                    & "<tr>" _
                                    & "<td>" _
                                    & "<table cellpadding='0' cellspacing='0' style='width: 100%; background-color: " & ColoreSfondo & ";'>" _
                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 9pt'>" _
                                    & "<td style='text-align: center'>" & giorno & "</td>" _
                                    & "</tr>" _
                                    & "<tr style='font-family: Arial, Helvetica, sans-serif; font-size: 9pt'>" _
                                    & "<td style='text-align: center'>" & Mid(GiornoAppuntamento, 7, 2) & "</td>" _
                                    & "</tr>" _
                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 9pt'>" _
                                    & "<td style='text-align: center'>" & MonthName(Mid(GiornoAppuntamento, 5, 2)) & "</td>" _
                                    & "</tr>" _
                                    & "</table>" _
                                    & "</td>" _
                                    & "</tr>" _
                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: #CCFFFF'>" _
                                    & "<td style='text-align: center'>" & Ora & "</td>" _
                                    & "</tr>" _
                                    & "<tr style='height: 25px;'>" _
                                    & "<td style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: #C0C0C0; text-align: right;'>" & InfoLink & "</td>" _
                                    & "</tr>" _
                                    & "<tr>" _
                                    & "<td>" _
                                    & "<table cellpadding='0' cellspacing='0' style='width:100%;'>" _
                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold'>" _
                                    & "<td style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: " & ColoreSfondo1 & "; text-align: center;'>" & CodContratto & "</td>" _
                                    & "</tr>" _
                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold; background-color: " & ColoreSfondo1 & "'>" _
                                    & "<td style='text-align: center'>" & Mid(Cognome, 1, 15) & "</td>" _
                                    & "</tr>" _
                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold; background-color: " & ColoreSfondo1 & "'>" _
                                    & "<td style='text-align: center'>" & Mid(Nome, 1, 15) & "</td>" _
                                    & "</tr>" _
                                    & "<tr style='background-color: " & ColoreSfondo1 & "'><td>&nbsp; &nbsp;</td>" _
                                    & "</tr>" _
                                    & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: " & ColoreSfondo1 & ";height: 30px'>" _
                                    & "<td style='text-align: center'>" & LinkCrea & "&nbsp;" & LinkSposta & "&nbsp;" & LinkReimposta & "&nbsp;" & LinkAnnulla & "</td>" _
                                    & "</tr>" _
                                    & "</table>" _
                                    & "</td>" _
                                    & "</tr>" _
                                    & "</table>"

                If TabAnnullati <> "" Then
                    Tabella = Tabella & "</td><td>" & TabAnnullati & "</td></tr></table>"
                End If



                Tabella = Tabella & "</tr></td>"
            Loop
            myReader1.Close()







        Catch ex As Exception




        End Try

    End Function

    Private Function CaricaDisponibilita(ByVal MESE As String, ByVal ANNO As String)
        Try
            Dim S As String = ""

            Dim NumeroGiorniMese As Integer = DateTime.DaysInMonth(CInt(ANNO), CInt(Mese))
            Dim Giorno As String = ""
            Dim i As Integer = 0

            If chFuoriOrario.Checked = True Then
                S = " TIPO_F_ORARIA=0 OR TIPO_F_ORARIA=4 "
            Else
                S = " TIPO_F_ORARIA=0 "
            End If

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT count(id),substr(INIZIO,1,8) as miadata FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE substr(INIZIO,1,6)='" & ANNO & MESE & "' and (" & S & ") AND ID_FILIALE=" & FILIALE.Value & " AND N_OPERATORE='" & SPORTELLO.Value & "' group by substr(INIZIO,1,8) having count(*)>0"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader1.Read
                Calendar1.SelectedDates.Add(CDate(par.FormattaData(Calendar1.TodaysDate.Year & Mid(myReader1("miadata"), 5, 2) & Mid(myReader1("miadata"), 7, 2))))
            Loop
            myReader1.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Function

    Private Function CaricaDati()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT TAB_FILIALI.NOME AS DESC_FILIALE FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=" & FILIALE.Value
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                lblFiliale.Text = par.IfNull(myReader1("DESC_FILIALE"), "")
                lblSportello.Text = "SPORTELLO/OPERATORE " & SPORTELLO.Value
                'If TIPO.Value = "0" Then
                '    Label3.Text = "APPUNTAMENTO CHE STAI PER SPOSTARE:<br /><br />" & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "") & "<br/>CONTRATTO COD." & par.IfNull(myReader1("COD_CONTRATTO"), "") & "<br/>DEL " & par.FormattaData(Mid(par.IfNull(myReader1("INIZIO"), ""), 1, 8)) & " ORE " & Mid(par.IfNull(myReader1("INIZIO"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("INIZIO"), ""), 11, 2)
                'Else
                '    Label3.Text = "APPUNTAMENTO CHE STAI PER REIMPOSTARE:<br /><br />" & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "") & "<br/>CONTRATTO COD." & par.IfNull(myReader1("COD_CONTRATTO"), "") & "<br/>DEL " & par.FormattaData(Mid(par.IfNull(myReader1("INIZIO"), ""), 1, 8)) & " ORE " & Mid(par.IfNull(myReader1("INIZIO"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("INIZIO"), ""), 11, 2)
                'End If
            End If
            myReader1.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
        'Try
        '    par.OracleConn.Open()
        '    par.SettaCommand(par)

        '    par.cmd.CommandText = "SELECT AGENDA_APPUNTAMENTI.*,TAB_FILIALI.NOME AS DESC_FILIALE FROM SISCOM_MI.AGENDA_APPUNTAMENTI,SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=AGENDA_APPUNTAMENTI.ID_FILIALE AND AGENDA_APPUNTAMENTI.ID=" & IDA.Value
        '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '    If myReader1.Read Then
        '        lblFiliale.Text = par.IfNull(myReader1("DESC_FILIALE"), "")
        '        lblSportello.Text = "SPORTELLO/OPERATORE " & par.IfNull(myReader1("n_operatore"), "")
        '        If TIPO.Value = "0" Then
        '            Label3.Text = "APPUNTAMENTO CHE STAI PER SPOSTARE:<br /><br />" & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "") & "<br/>CONTRATTO COD." & par.IfNull(myReader1("COD_CONTRATTO"), "") & "<br/>DEL " & par.FormattaData(Mid(par.IfNull(myReader1("INIZIO"), ""), 1, 8)) & " ORE " & Mid(par.IfNull(myReader1("INIZIO"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("INIZIO"), ""), 11, 2)
        '        Else
        '            Label3.Text = "APPUNTAMENTO CHE STAI PER REIMPOSTARE:<br /><br />" & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "") & "<br/>CONTRATTO COD." & par.IfNull(myReader1("COD_CONTRATTO"), "") & "<br/>DEL " & par.FormattaData(Mid(par.IfNull(myReader1("INIZIO"), ""), 1, 8)) & " ORE " & Mid(par.IfNull(myReader1("INIZIO"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("INIZIO"), ""), 11, 2)
        '        End If
        '    End If
        '    myReader1.Close()
        '    par.cmd.Dispose()
        '    par.OracleConn.Close()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        'Catch ex As Exception
        '    par.OracleConn.Close()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        'End Try
    End Function

    Public Property TabellaGiorni() As String
        Get
            If Not (ViewState("par_TabellaGiorni") Is Nothing) Then
                Return CStr(ViewState("par_TabellaGiorni"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_TabellaGiorni") = value
        End Set
    End Property

    Public Property GiornoApp() As String
        Get
            If Not (ViewState("par_GiornoApp") Is Nothing) Then
                Return CStr(ViewState("par_GiornoApp"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_GiornoApp") = value
        End Set
    End Property

    Public Property OraApp() As String
        Get
            If Not (ViewState("par_OraApp") Is Nothing) Then
                Return CStr(ViewState("par_OraApp"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_OraApp") = value
        End Set
    End Property

    Public Property GiornoPartenza() As String
        Get
            If Not (ViewState("par_GiornoPartenza") Is Nothing) Then
                Return CStr(ViewState("par_GiornoPartenza"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_GiornoPartenza") = value
        End Set
    End Property

    Public Property GiornoArrivo() As String
        Get
            If Not (ViewState("par_GiornoArrivo") Is Nothing) Then
                Return CStr(ViewState("par_GiornoArrivo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_GiornoArrivo") = value
        End Set
    End Property

    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged
        GiornoPartenza = Format(DateAdd("d", Calendar1.SelectedDate.DayOfWeek * -1, Calendar1.SelectedDate), "yyyyMMdd")
        CaricaDisponibilita(Format(Calendar1.TodaysDate.Month, "00"), CStr(Calendar1.TodaysDate.Year))
        CaricaCalendario()
    End Sub

    Protected Sub Calendar1_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles Calendar1.VisibleMonthChanged
        Calendar1.SelectedDates.Clear()
        GiornoPartenza = Format(DateAdd("d", Calendar1.VisibleDate.DayOfWeek * -1, Calendar1.VisibleDate), "yyyyMMdd")
        CaricaDisponibilita(Format(Calendar1.VisibleDate.Month, "00"), CStr(Calendar1.VisibleDate.Year))
        CaricaCalendario()
    End Sub

    Protected Sub chFuoriOrario_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chFuoriOrario.CheckedChanged
        CaricaCalendario()
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try
            Select Case OPERAZIONE.Value
                Case "3"
                    CaricaCalendario()
                Case "1"
                    SpostaApp()
                Case "2"
                    ReimpostaApp()
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Function ReimpostaApp()
        If SLOTDESTINATARIO.Value <> "" Then
            Try
                Dim NUOVADATA As String = ""
                Dim NUOVAORA As String = ""
                Dim INDICECONVOCAZIONE As String = ""

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT  *FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID=" & SLOTORIGINALE.Value
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    'NUOVO APPUNTAMENTO


                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID=" & SLOTDESTINATARIO.Value
                    Dim myReaderx As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderx.Read Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU (ID,ID_CONTRATTO,ID_GRUPPO,ID_FILIALE,COGNOME,NOME,N_OPERATORE,DATA_APP,ORE_APP) VALUES (SISCOM_MI.SEQ_CONVOCAZIONI_AU.NEXTVAL," & myReader1("ID_contratto") & ",0," & myReader1("ID_FILIALE") & ",'" & par.PulisciStrSql(myReader1("COGNOME")) & "','" & par.PulisciStrSql(myReader1("NOME")) & "','" & myReader1("N_OPERATORE") & "','" & Mid(myReaderx("INIZIO"), 1, 8) & "','" & Mid(myReaderx("INIZIO"), 9, 2) & "." & Mid(myReaderx("INIZIO"), 11, 2) & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select SISCOM_MI.SEQ_CONVOCAZIONI_AU.CURRVAL FROM dual "
                        Dim myReaderx1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderx1.Read Then
                            INDICECONVOCAZIONE = myReaderx1(0)
                        End If
                        myReaderx1.Close()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=1,ID_CONVOCAZIONE=" & INDICECONVOCAZIONE & ",COD_CONTRATTO='" & myReader1("COD_CONTRATTO") & "',COGNOME='" & par.PulisciStrSql(myReader1("COGNOME")) & "',NOME='" & par.PulisciStrSql(myReader1("NOME")) & "',ID_CONTRATTO=" & myReader1("ID_CONTRATTO") & " WHERE ID=" & SLOTDESTINATARIO.Value
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & SLOTORIGINALE.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO REIMPOSTATO AL " & par.FormattaData(Mid(myReaderx("INIZIO"), 1, 8)) & " ORE " & Mid(myReaderx("INIZIO"), 9, 2) & "." & Mid(myReaderx("INIZIO"), 11, 2) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderx.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & SLOTDESTINATARIO.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO IMPOSTATO DA REIMPOSTAZIONE - " & par.PulisciStrSql(myReader1("COGNOME")) & " " & par.PulisciStrSql(myReader1("NOME")) & " - Cod.Contratto: " & par.PulisciStrSql(myReader1("COD_CONTRATTO")) & "')"
                    par.cmd.ExecuteNonQuery()


                    'EVENTI CONVOCAZIONI

                    par.cmd.CommandText = "SELECT  *FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID=" & SLOTDESTINATARIO.Value
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        NUOVADATA = par.FormattaData(Mid(myReader("INIZIO"), 1, 8))
                        NUOVAORA = Mid(myReader("INIZIO"), 9, 2) & "." & Mid(myReader("INIZIO"), 11, 2)

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader1("ID_CONVOCAZIONE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO REIMPOSTATO AL " & NUOVADATA & " ORE " & NUOVAORA & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReader.Close()

                End If
                myReader1.Close()

                SLOTDESTINATARIO.Value = ""
                SLOTORIGINALE.Value = ""

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
            CaricaCalendario()
        End If
    End Function

    Private Function SpostaApp()
        If SLOTDESTINATARIO.Value <> "" Then
            Try
                Dim NUOVADATA As String = ""
                Dim NUOVAORA As String = ""
                Dim INDICECONVOCAZIONE As String = ""

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT  *FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID=" & SLOTORIGINALE.Value
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    'NUOVO APPUNTAMENTO

                    par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=1,ID_CONVOCAZIONE=" & myReader1("ID_CONVOCAZIONE") & ",COD_CONTRATTO='" & myReader1("COD_CONTRATTO") & "',COGNOME='" & par.PulisciStrSql(myReader1("COGNOME")) & "',NOME='" & par.PulisciStrSql(myReader1("NOME")) & "',ID_CONTRATTO=" & myReader1("ID_CONTRATTO") & " WHERE ID=" & SLOTDESTINATARIO.Value
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & SLOTDESTINATARIO.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO IMPOSTATO DA SPOSTAMENTO - " & par.PulisciStrSql(myReader1("COGNOME")) & " " & par.PulisciStrSql(myReader1("NOME")) & " - Cod.Contratto: " & par.PulisciStrSql(myReader1("COD_CONTRATTO")) & "')"
                    par.cmd.ExecuteNonQuery()

                    'VECCHIO APPUNTAMENTO

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & SLOTORIGINALE.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO SPOSTATO - " & par.PulisciStrSql(myReader1("COGNOME")) & " " & par.PulisciStrSql(myReader1("NOME")) & " - Cod.Contratto: " & par.PulisciStrSql(myReader1("COD_CONTRATTO")) & "')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=0,ID_CONVOCAZIONE=NULL,COD_CONTRATTO='',COGNOME='',NOME='',ID_CONTRATTO=NULL WHERE ID=" & SLOTORIGINALE.Value
                    par.cmd.ExecuteNonQuery()

                    'EVENTI CONVOCAZIONI

                    par.cmd.CommandText = "SELECT  *FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID=" & SLOTDESTINATARIO.Value
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        NUOVADATA = par.FormattaData(Mid(myReader("INIZIO"), 1, 8))
                        NUOVAORA = Mid(myReader("INIZIO"), 9, 2) & "." & Mid(myReader("INIZIO"), 11, 2)

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader1("ID_CONVOCAZIONE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO SPOSTATO AL " & NUOVADATA & " ORE " & NUOVAORA & "')"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU SET DATA_APP='" & Mid(myReader("INIZIO"), 1, 8) & "',ORE_APP='" & Mid(myReader("INIZIO"), 9, 2) & "." & Mid(myReader("INIZIO"), 11, 2) & "',ORE_FINE_APP='' WHERE ID=" & myReader1("ID_CONVOCAZIONE")
                        par.cmd.ExecuteNonQuery()

                    End If
                    myReader.Close()

                End If
                myReader1.Close()

                SLOTDESTINATARIO.Value = ""
                SLOTORIGINALE.Value = ""

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End Try

            CaricaCalendario()

        End If
    End Function

    Protected Sub ChAnnullati_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChAnnullati.CheckedChanged
        CaricaCalendario()
    End Sub

    Protected Sub imgCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCerca.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT  *FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_convocazione=" & Session.Item("IDAPP")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                GiornoApp = Mid(myReader("INIZIO"), 1, 8)
                OraApp = Mid(myReader("INIZIO"), 9, 2) & "." & Mid(myReader("INIZIO"), 11, 2)

                GiornoPartenza = Format(DateAdd("d", CDate(par.FormattaData(GiornoApp)).DayOfWeek * -1, CDate(par.FormattaData(GiornoApp))), "yyyyMMdd")
                GiornoArrivo = Format(DateAdd("d", 6, CDate(par.FormattaData(GiornoPartenza))), "yyyyMMdd")

                Calendar1.TodaysDate = CDate(par.FormattaData(GiornoApp))

            End If
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
        CaricaDisponibilita(Format(Calendar1.TodaysDate.Month, "00"), CStr(Calendar1.TodaysDate.Year))
        CaricaDati()
        CaricaCalendario()
    End Sub

    
End Class
