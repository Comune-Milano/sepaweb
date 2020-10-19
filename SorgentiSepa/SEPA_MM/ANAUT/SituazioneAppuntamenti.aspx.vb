
Partial Class ANAUT_SituazioneAppuntamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim strScript As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        'Dim Str As String
        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        'Str = Str & "<" & "/div>"

        'Response.Write(Str)
        'Response.Flush()

        Response.Expires = 0
        If IsPostBack = False Then


            Dim i As Integer = 0
            TIPO.Value = Request.QueryString("T")
            If TIPO.Value = "0" Then
                Label25.Text = "Sei sicuro di voler SPOSTARE l'appuntamento in data "
            Else
                Label25.Text = "Sei sicuro di voler FISSARE UN NUOVO APPUNTAMENTO in data "
            End If
            IDA.Value = Request.QueryString("IDA")
            IDCONVOCAZIONE.Value = Request.QueryString("IC")
            SPORTELLO.Value = Request.QueryString("SP")
            FILIALE.Value = Request.QueryString("F")
            GiornoApp = Request.QueryString("G")
            OraApp = Request.QueryString("O")
            GRUPPO.Value = Request.QueryString("GR")

            GiornoPartenza = Format(DateAdd("d", CDate(par.FormattaData(GiornoApp)).DayOfWeek * -1, CDate(par.FormattaData(GiornoApp))), "yyyyMMdd")
            GiornoArrivo = Format(DateAdd("d", 6, CDate(par.FormattaData(GiornoPartenza))), "yyyyMMdd")

            Calendar1.TodaysDate = CDate(par.FormattaData(GiornoApp))

            CaricaDisponibilita(Format(Calendar1.TodaysDate.Month, "00"), CStr(Calendar1.TodaysDate.Year))
            CaricaDati()
            CaricaCalendario()

        End If
    End Sub


    Private Function CaricaDisponibilita(ByVal MESE As String, ByVal ANNO As String)
        Try
            Dim S As String = ""

            Dim NumeroGiorniMese As Integer = DateTime.DaysInMonth(CInt(ANNO), CInt(MESE))
            Dim Giorno As String = ""
            Dim i As Integer = 0

            If chFuoriOrario.Checked = True Then
                S = " N_OPERATORE<>0 AND TIPO_F_ORARIA=0 OR TIPO_F_ORARIA=4 "
            Else
                S = " N_OPERATORE<>0 AND TIPO_F_ORARIA=0 "
            End If

            par.OracleConn.Open()
            par.SettaCommand(par)

            'par.cmd.CommandText = "SELECT count(id),substr(INIZIO,1,8) as miadata FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE substr(INIZIO,1,6)='" & ANNO & MESE & "' and (" & S & ") AND ID_FILIALE=" & FILIALE.Value & " AND N_OPERATORE='" & SPORTELLO.Value & "' group by substr(INIZIO,1,8) having count(*)>0"
            par.cmd.CommandText = "SELECT count(id),substr(INIZIO,1,8) as miadata FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE cod_contratto is null and substr(INIZIO,1,6)='" & ANNO & MESE & "' and (" & S & ") AND ID_FILIALE=" & FILIALE.Value & " AND ID_SPORTELLO=" & SPORTELLO.Value & " group by substr(INIZIO,1,8) having count(*)>0"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader1.Read
                Calendar1.SelectedDates.Add(CDate(par.FormattaData(ANNO & Mid(myReader1("miadata"), 5, 2) & Mid(myReader1("miadata"), 7, 2))))
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
            If IDA.Value <> "" Then
                par.cmd.CommandText = "SELECT AGENDA_APPUNTAMENTI.*,TAB_FILIALI.NOME AS DESC_FILIALE FROM SISCOM_MI.AGENDA_APPUNTAMENTI,SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=AGENDA_APPUNTAMENTI.ID_FILIALE AND AGENDA_APPUNTAMENTI.ID=" & IDA.Value
            Else
                par.cmd.CommandText = "SELECT CONVOCAZIONI_AU.*,TAB_FILIALI.NOME AS DESC_FILIALE,RAPPORTI_UTENZA.COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.CONVOCAZIONI_AU,SISCOM_MI.TAB_FILIALI WHERE RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO AND TAB_FILIALI.ID=CONVOCAZIONI_AU.ID_FILIALE AND CONVOCAZIONI_AU.ID=" & IDCONVOCAZIONE.Value
            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                lblFiliale.Text = par.IfNull(myReader1("DESC_FILIALE"), "")
                CodiceContratto.Value = par.IfNull(myReader1("COD_CONTRATTO"), "")

                If IDA.Value <> "" Then
                    If TIPO.Value = "0" Then
                        Label3.Text = "APPUNTAMENTO CHE STAI PER SPOSTARE:<br /><br />" & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "") & "<br/>CONTRATTO COD." & par.IfNull(myReader1("COD_CONTRATTO"), "") & "<br/>DEL " & par.FormattaData(Mid(par.IfNull(myReader1("INIZIO"), ""), 1, 8)) & " ORE " & Mid(par.IfNull(myReader1("INIZIO"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("INIZIO"), ""), 11, 2)
                    Else
                        Label3.Text = "APPUNTAMENTO CHE STAI PER FISSARE:<br /><br />" & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "") & "<br/>CONTRATTO COD." & par.IfNull(myReader1("COD_CONTRATTO"), "") & "<br/>DEL " & par.FormattaData(Mid(par.IfNull(myReader1("INIZIO"), ""), 1, 8)) & " ORE " & Mid(par.IfNull(myReader1("INIZIO"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("INIZIO"), ""), 11, 2)
                    End If
                Else
                    If TIPO.Value = "0" Then
                        Label3.Text = "APPUNTAMENTO CHE STAI PER SPOSTARE:<br /><br />" & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "") & "<br/>CONTRATTO COD." & par.IfNull(myReader1("COD_CONTRATTO"), "") & "<br/>DEL " & par.FormattaData(Mid(par.IfNull(myReader1("DATA_APP"), ""), 1, 8)) & " ORE " & par.IfNull(myReader1("ORE_APP"), "")
                    Else
                        Label3.Text = "APPUNTAMENTO CHE STAI PER FISSARE:<br /><br />" & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "") & "<br/>CONTRATTO COD." & par.IfNull(myReader1("COD_CONTRATTO"), "") & "<br/>DEL " & par.FormattaData(Mid(par.IfNull(myReader1("DATA_APP"), ""), 1, 8)) & " ORE " & par.IfNull(myReader1("ORE_APP"), "")
                    End If
                End If
            End If
            myReader1.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Function

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
            Dim InfoLink As String = ""
            Dim NomeImmagine As String = ""
            Dim indiceTabella As String = ""

            If TIPO.Value = "0" Then
                NomeImmagine = "Reload1.png"
            Else
                NomeImmagine = "Reimposta.png"
            End If

            Dim destinatario As String = ""
            Dim cursore As String = ""


            'par.cmd.CommandText = "SELECT AGENDA_APPUNTAMENTI.* FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_FILIALE=" & FILIALE.Value & " AND N_OPERATORE='" & SPORTELLO.Value & "' AND substr(INIZIO,1,8)='" & GiornoAppuntamento & "' and to_number(substr(INIZIO,9,4))>=800 and to_number(substr(INIZIO,9,4))<=1830 order by inizio asc"
            par.cmd.CommandText = "SELECT distinct AGENDA_APPUNTAMENTI.inizio FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_SPORTELLO=" & SPORTELLO.Value & " AND substr(INIZIO,1,8)='" & GiornoAppuntamento & "' and to_number(substr(INIZIO,9,4))>=800 and to_number(substr(INIZIO,9,4))<=1830 order by inizio asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader1.Read
                Ora = Mid(par.IfNull(myReader1("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("inizio"), ""), 11, 2)

                par.cmd.CommandText = "SELECT AGENDA_APPUNTAMENTI.* FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_SPORTELLO=" & SPORTELLO.Value & " AND INIZIO='" & myReader1("INIZIO") & "' order by n_operatore asc"
                Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReaderX.Read
                    Cognome = par.IfNull(myReaderX("cognome"), "&nbsp;")
                    Nome = par.IfNull(myReaderX("nome"), "&nbsp;")
                    CodContratto = par.IfNull(myReaderX("cod_contratto"), "&nbsp;")

                    If myReaderX("N_OPERATORE") <> "0" Then
                        Select Case par.IfNull(myReaderX("tipo_f_oraria"), "")
                            Case "0"
                                If CodContratto <> "&nbsp;" Then
                                    ColoreSfondo1 = "#FFFFFF"
                                    LinkSposta = "&nbsp;"
                                    destinatario = destinatario & "&nbsp;"
                                    cursore = ""
                                    If CodiceContratto.Value <> CodContratto Then
                                        InfoLink = InfoLink & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReaderX("ID_CONTRATTO") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & " ><img alt='Informazioni Utente contratto " & CodContratto & " SPORTELLO " & myReaderX("N_OPERATORE") & "' src='info-icon.png' border='0'/></a>"
                                    Else
                                        InfoLink = InfoLink & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReaderX("ID_CONTRATTO") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & " ><img alt='Informazioni Utente contratto " & CodContratto & " SPORTELLO " & myReaderX("N_OPERATORE") & "' src='info-icon1.png' border='0'/></a>"
                                    End If
                                Else
                                    ColoreSfondo1 = "#FFFFFF"
                                    'destinatario = destinatario & " onclick='Sposta(" & myReader1("ID") & "," & Mid(par.IfNull(myReader1("inizio"), ""), 1, 8) & "," & Mid(par.IfNull(myReader1("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("inizio"), ""), 11, 2) & ")' "
                                    destinatario = destinatario & "&nbsp;" & "<a href='javascript:void(0)' onclick=" & Chr(34) & "Sposta(" & myReaderX("ID") & "," & Mid(par.IfNull(myReaderX("inizio"), ""), 1, 8) & "," & Mid(par.IfNull(myReaderX("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReaderX("inizio"), ""), 11, 2) & "," & myReaderX("n_operatore") & ");" & Chr(34) & " ><img alt='SPORTELLO " & myReaderX("n_operatore") & " LIBERO' src='SlotLibero.png' border='0'/></a>"
                                    LinkSposta = "&nbsp;"
                                    InfoLink = InfoLink & "&nbsp;"
                                    cursore = " cursor:pointer; "
                                End If
                            Case "4"
                                If chFuoriOrario.Checked = True Then
                                    If CodContratto <> "&nbsp;" Then
                                        ColoreSfondo1 = "#FFFFFF"
                                        LinkSposta = "&nbsp;"
                                        cursore = ""
                                        destinatario = destinatario & "&nbsp;"
                                        'InfoLink = InfoLink & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReaderX("ID_CONTRATTO") & "','Contratto" & Format(Now, "hhss") & "','height=650,width=900');" & Chr(34) & " ><img alt='Informazioni Utente contratto " & CodContratto & " SPORTELLO " & vbAbort & "' src='info-icon.png' border='0'/></a>"
                                        If CodiceContratto.Value <> CodContratto Then
                                            InfoLink = InfoLink & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReaderX("ID_CONTRATTO") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & " ><img alt='Informazioni Utente contratto " & CodContratto & " SPORTELLO " & myReaderX("N_OPERATORE") & "' src='info-icon.png' border='0'/></a>"
                                        Else
                                            InfoLink = InfoLink & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReaderX("ID_CONTRATTO") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & " ><img alt='Informazioni Utente contratto " & CodContratto & " SPORTELLO " & myReaderX("N_OPERATORE") & "' src='info-icon1.png' border='0'/></a>"
                                        End If
                                    Else
                                        ColoreSfondo1 = "#FFFFFF"
                                        'destinatario = destinatario & " onclick='Sposta(" & myReader1("ID") & "," & Mid(par.IfNull(myReader1("inizio"), ""), 1, 8) & "," & Mid(par.IfNull(myReader1("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReader1("inizio"), ""), 11, 2) & ")' "

                                        destinatario = destinatario & "&nbsp;" & "<a href='javascript:void(0)' onclick=" & Chr(34) & "Sposta(" & myReaderX("ID") & "," & Mid(par.IfNull(myReaderX("inizio"), ""), 1, 8) & "," & Mid(par.IfNull(myReaderX("inizio"), ""), 9, 2) & "." & Mid(par.IfNull(myReaderX("inizio"), ""), 11, 2) & "," & myReaderX("n_operatore") & ");" & Chr(34) & " ><img alt='SPORTELLO " & myReaderX("n_operatore") & " LIBERO' src='SlotLibero.png' border='0'/></a>"
                                        LinkSposta = "&nbsp;"
                                        InfoLink = InfoLink & "&nbsp;"
                                        cursore = " cursor:pointer; "
                                    End If
                                Else
                                    ColoreSfondo1 = "#808080"
                                    If CodContratto <> "&nbsp;" Then

                                        LinkSposta = "&nbsp;"
                                        cursore = ""
                                        destinatario = destinatario & "&nbsp;"

                                        If CodiceContratto.Value <> CodContratto Then
                                            InfoLink = InfoLink & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReaderX("ID_CONTRATTO") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & " ><img alt='Informazioni Utente contratto " & CodContratto & " SPORTELLO " & myReaderX("N_OPERATORE") & "' src='info-icon.png' border='0'/></a>"
                                        Else
                                            InfoLink = InfoLink & "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/Contratto.aspx?LT=1&ID=" & myReaderX("ID_CONTRATTO") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & " ><img alt='Informazioni Utente contratto " & CodContratto & " SPORTELLO " & myReaderX("N_OPERATORE") & "' src='info-icon1.png' border='0'/></a>"
                                        End If
                                    Else

                                        destinatario = destinatario & "&nbsp;" & "<img alt='SPORTELLO " & myReaderX("n_operatore") & " LIBERO' src='SlotLiberoN.png' border='0'/>"
                                        LinkSposta = "&nbsp;"
                                        InfoLink = InfoLink & "&nbsp;"
                                        cursore = " cursor:pointer; "
                                    End If
                                    cursore = " "
                                End If
                            Case Else
                                Cognome = "&nbsp;"
                                Nome = "&nbsp;"
                                CodContratto = "&nbsp;"
                                ColoreSfondo1 = "#808080"
                                LinkSposta = "&nbsp;"
                                InfoLink = InfoLink & "&nbsp;"
                                destinatario = destinatario & "&nbsp;"
                                cursore = " "
                        End Select
                    Else
                        Cognome = "&nbsp;"
                        Nome = "&nbsp;"
                        CodContratto = "&nbsp;"
                        ColoreSfondo1 = "#808080"
                        LinkSposta = "&nbsp;"
                        InfoLink = InfoLink & "&nbsp;"
                        destinatario = destinatario & "&nbsp;"
                        cursore = " "
                    End If
                Loop
                myReaderX.Close()

                Dim Script As String = ""

                If GiornoAppuntamento = GiornoApp And Ora = OraApp And CodContratto <> "&nbsp;" Then
                    ColoreSfondo = "#FF5050"
                    indiceTabella = "maxTA"
                    Script = "var obj = document.getElementById('maxTA');var posX = obj.offsetLeft; var posY = obj.offsetTop; " _
                      & "document.getElementById('Elenco').scrollTop=obj.offsetParent.offsetTop;"
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType, Page.ClientID, Script, True)



                Else
                    ColoreSfondo = "#FFFFCC"
                    indiceTabella = myReader1("inizio")
                End If


                Tabella = Tabella & "<tr><td><table id=" & indiceTabella & " cellpadding='0' cellspacing='0' style='border: 1px solid #000099; width: 120px; height: 120px;'>" _
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
  & "<tr style='height: 20px'>" _
        & "<td style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: #C0C0C0; text-align: right;'>" & InfoLink & "</td>" _
  & "</tr>" _
  & "<tr>" _
  & "<td>" _
  & "<table cellpadding='0' cellspacing='0' style='width:100%;'>" _
        & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold'>" _
        & "<td style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: " & ColoreSfondo1 & "; text-align: center;'></td>" _
  & "</tr>" _
        & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold; background-color: " & ColoreSfondo1 & "'>" _
        & "<td style='text-align: center'></td>" _
  & "</tr>" _
  & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold; background-color: " & ColoreSfondo1 & "'>" _
        & "<td style='text-align: center'></td>" _
  & "</tr>" _
        & "<tr style='background-color: " & ColoreSfondo1 & "'><td>&nbsp; &nbsp;</td>" _
  & "</tr>" _
        & "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 8pt; background-color: " & ColoreSfondo1 & ";height: 20px'>" _
        & "<td style='text-align: center'>" & destinatario & "</td>" _
  & "</tr>" _
  & "</table>" _
  & "</td>" _
  & "</tr>" _
  & "</table></tr></td>"


                destinatario = ""
                InfoLink = ""

            Loop
            myReader1.Close()







        Catch ex As Exception




        End Try

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

    Protected Sub chFuoriOrario_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chFuoriOrario.CheckedChanged
        CaricaDisponibilita(Format(Calendar1.TodaysDate.Month, "00"), CStr(Calendar1.TodaysDate.Year))
        CaricaCalendario()
    End Sub

    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged
        'GiornoPartenza = Format(DateAdd("d", Calendar1.SelectedDate.DayOfWeek * -1, Calendar1.SelectedDate), "yyyyMMdd")
        'CaricaCalendario()

        GiornoPartenza = Format(DateAdd("d", Calendar1.SelectedDate.DayOfWeek * -1, Calendar1.SelectedDate), "yyyyMMdd")

        ' CaricaDisponibilita(Format(Calendar1.TodaysDate.Month, "00"), CStr(Calendar1.TodaysDate.Year))
        CaricaDisponibilita(Format(Calendar1.SelectedDate.Month, "00"), CStr(Calendar1.SelectedDate.Year))
        CaricaCalendario()

    End Sub

    Protected Sub Calendar1_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles Calendar1.VisibleMonthChanged
        Calendar1.SelectedDates.Clear()
        GiornoPartenza = Format(DateAdd("d", Calendar1.VisibleDate.DayOfWeek * -1, Calendar1.VisibleDate), "yyyyMMdd")
        CaricaDisponibilita(Format(Calendar1.VisibleDate.Month, "00"), CStr(Calendar1.VisibleDate.Year))
        CaricaCalendario()
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        If SLOTDESTINATARIO.Value <> "" Then
            Try
                Dim NUOVADATA As String = ""
                Dim NUOVAORA As String = ""
                Dim INDICECONVOCAZIONE As String = ""

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans

                If IDA.Value <> "" Then
                    par.cmd.CommandText = "SELECT  *FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID=" & IDA.Value
                Else
                    par.cmd.CommandText = "SELECT convocazioni_au.id as id_convocazione,convocazioni_au.*,rapporti_utenza.cod_contratto FROM siscom_mi.rapporti_utenza, SISCOM_MI.convocazioni_au WHERE rapporti_utenza.id=convocazioni_au.id_contratto and convocazioni_au.ID=" & IDCONVOCAZIONE.Value
                End If

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    'NUOVO APPUNTAMENTO

                    If TIPO.Value = "0" Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=1,ID_CONVOCAZIONE=" & myReader1("ID_CONVOCAZIONE") & ",COD_CONTRATTO='" & myReader1("COD_CONTRATTO") & "',COGNOME='" & par.PulisciStrSql(par.IfNull(myReader1("COGNOME"), "")) & "',NOME='" & par.PulisciStrSql(par.IfNull(myReader1("NOME"), "")) & "',ID_CONTRATTO=" & myReader1("ID_CONTRATTO") & " WHERE ID=" & SLOTDESTINATARIO.Value
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & SLOTDESTINATARIO.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO IMPOSTATO DA SPOSTAMENTO - " & par.PulisciStrSql(par.IfNull(myReader1("COGNOME"), "")) & " " & par.PulisciStrSql(par.IfNull(myReader1("NOME"), "")) & " - Cod.Contratto: " & par.PulisciStrSql(myReader1("COD_CONTRATTO")) & "')"
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID=" & SLOTDESTINATARIO.Value
                        Dim myReaderx As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderx.Read Then
                            'par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU (ID,ID_CONTRATTO,ID_GRUPPO,ID_FILIALE,COGNOME,NOME,N_OPERATORE,DATA_APP,ORE_APP,ID_SPORTELLO) VALUES (SISCOM_MI.SEQ_CONVOCAZIONI_AU.NEXTVAL," & myReader1("ID_contratto") & "," & GRUPPO.Value & "," & myReader1("ID_FILIALE") & ",'" & par.PulisciStrSql(par.IfNull(myReader1("COGNOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReader1("NOME"), "")) & "','" & myReader1("N_OPERATORE") & "','" & Mid(myReaderx("INIZIO"), 1, 8) & "','" & Mid(myReaderx("INIZIO"), 9, 2) & "." & Mid(myReaderx("INIZIO"), 11, 2) & "'," & SPORTELLO.Value & ")"
                            'par.cmd.ExecuteNonQuery()

                            'par.cmd.CommandText = "select SISCOM_MI.SEQ_CONVOCAZIONI_AU.CURRVAL FROM dual "
                            'Dim myReaderx1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            'If myReaderx1.Read Then
                            '    INDICECONVOCAZIONE = myReaderx1(0)
                            'End If
                            'myReaderx1.Close()

                            par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=1,ID_CONVOCAZIONE=" & myReader1("ID_CONVOCAZIONE") & ",COD_CONTRATTO='" & myReader1("COD_CONTRATTO") & "',COGNOME='" & par.PulisciStrSql(par.IfNull(myReader1("COGNOME"), "")) & "',NOME='" & par.PulisciStrSql(par.IfNull(myReader1("NOME"), "")) & "',ID_CONTRATTO=" & myReader1("ID_CONTRATTO") & " WHERE ID=" & SLOTDESTINATARIO.Value
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & SLOTDESTINATARIO.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO IMPOSTATO DA FISSA NUOVO APPUNTAMENTO - " & par.PulisciStrSql(par.IfNull(myReader1("COGNOME"), "")) & " " & par.PulisciStrSql(par.IfNull(myReader1("NOME"), "")) & " - Cod.Contratto: " & par.PulisciStrSql(myReader1("COD_CONTRATTO")) & "')"
                            par.cmd.ExecuteNonQuery()
                        End If
                        myReaderx.Close()


                    End If


                    'VECCHIO APPUNTAMENTO
                    If TIPO.Value = "0" Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & IDA.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO SPOSTATO - " & par.PulisciStrSql(par.IfNull(myReader1("COGNOME"), "")) & " " & par.PulisciStrSql(par.IfNull(myReader1("NOME"), "")) & " - Cod.Contratto: " & par.PulisciStrSql(myReader1("COD_CONTRATTO")) & "')"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=0,ID_CONVOCAZIONE=NULL,COD_CONTRATTO='',COGNOME='',NOME='',ID_CONTRATTO=NULL WHERE ID=" & IDA.Value
                        par.cmd.ExecuteNonQuery()
                    End If

                    If TIPO.Value = "1" And IDA.Value <> "" Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & IDA.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO SOSPESO PER FISSATO NUOVO APPUNTAMENTO  - Cod.Contratto: " & par.PulisciStrSql(myReader1("COD_CONTRATTO")) & "')"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=0,ID_CONVOCAZIONE=NULL,COD_CONTRATTO='',COGNOME='',NOME='',ID_CONTRATTO=NULL WHERE ID=" & IDA.Value
                        par.cmd.ExecuteNonQuery()
                    End If


                    'EVENTI CONVOCAZIONI

                    par.cmd.CommandText = "SELECT  *FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID=" & SLOTDESTINATARIO.Value
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        NUOVADATA = par.FormattaData(Mid(myReader("INIZIO"), 1, 8))
                        NUOVAORA = Mid(myReader("INIZIO"), 9, 2) & "." & Mid(myReader("INIZIO"), 11, 2)

                        If TIPO.Value = "0" Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader1("ID_CONVOCAZIONE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO SPOSTATO AL " & NUOVADATA & " ORE " & NUOVAORA & "')"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU SET DATA_APP='" & Mid(myReader("INIZIO"), 1, 8) & "',ORE_APP='" & Mid(myReader("INIZIO"), 9, 2) & "." & Mid(myReader("INIZIO"), 11, 2) & "',ORE_FINE_APP='' WHERE ID=" & myReader1("ID_CONVOCAZIONE")
                            par.cmd.ExecuteNonQuery()
                        Else
                            If IDA.Value <> "" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader1("ID_CONVOCAZIONE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO FISSATO AL " & NUOVADATA & " ORE " & NUOVAORA & "')"
                            Else
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader1("ID") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO FISSATO AL " & NUOVADATA & " ORE " & NUOVAORA & "')"
                            End If
                            par.cmd.ExecuteNonQuery()
                            Dim indiceMotivo As String = "6"
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_MOTIVO_ANNULLO_APP WHERE ID_AU=(SELECT MAX(ID) FROM UTENZA_BANDI) AND SOSP_MP=1"
                            Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderX.Read Then
                                indiceMotivo = myReaderX("ID")
                            End If
                            myReaderX.Close()

                            par.cmd.CommandText = "select * from utenza_dichiarazioni where rapporto='" & CodiceContratto.Value & "' and id_bando=(select max(id) from utenza_bandi where stato=1)"
                            Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderX1.HasRows = False Then
                                If IDA.Value <> "" Then
                                    par.cmd.CommandText = "update siscom_mi.convocazioni_au set DATA_APP='" & Mid(myReader("INIZIO"), 1, 8) & "',ORE_APP='" & Mid(myReader("INIZIO"), 9, 2) & "." & Mid(myReader("INIZIO"), 11, 2) & "',ORE_FINE_APP='',id_stato=1,id_motivo_annullo=" & indiceMotivo & " where id=" & myReader1("ID_CONVOCAZIONE")
                                Else
                                    par.cmd.CommandText = "update siscom_mi.convocazioni_au set DATA_APP='" & Mid(myReader("INIZIO"), 1, 8) & "',ORE_APP='" & Mid(myReader("INIZIO"), 9, 2) & "." & Mid(myReader("INIZIO"), 11, 2) & "',ORE_FINE_APP='',id_stato=1,id_motivo_annullo=" & indiceMotivo & " where id=" & myReader1("ID")
                                End If
                                par.cmd.ExecuteNonQuery()

                                If IDA.Value <> "" Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader1("ID_CONVOCAZIONE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO SOSPESO PER NUOVO APPUNTAMENTO FISSATO')"
                                Else
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader1("ID") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO SOSPESO PER NUOVO APPUNTAMENTO FISSATO')"
                                End If
                                par.cmd.ExecuteNonQuery()
                            Else
                                If IDA.Value <> "" Then
                                    par.cmd.CommandText = "update siscom_mi.convocazioni_au set DATA_APP='" & Mid(myReader("INIZIO"), 1, 8) & "',ORE_APP='" & Mid(myReader("INIZIO"), 9, 2) & "." & Mid(myReader("INIZIO"), 11, 2) & "',ORE_FINE_APP='' where id=" & myReader1("ID_CONVOCAZIONE")
                                Else
                                    par.cmd.CommandText = "update siscom_mi.convocazioni_au set DATA_APP='" & Mid(myReader("INIZIO"), 1, 8) & "',ORE_APP='" & Mid(myReader("INIZIO"), 9, 2) & "." & Mid(myReader("INIZIO"), 11, 2) & "',ORE_FINE_APP='' where id=" & myReader1("ID")
                                End If
                                par.cmd.ExecuteNonQuery()

                                'If IDA.Value <> "" Then
                                '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader1("ID_CONVOCAZIONE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO SOSPESO PER NUOVO APPUNTAMENTO FISSATO')"
                                'Else
                                '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader1("ID") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO SOSPESO PER NUOVO APPUNTAMENTO FISSATO')"
                                'End If
                                'par.cmd.ExecuteNonQuery()
                            End If
                        End If



                    End If
                    myReader.Close()
                    IDA.Value = SLOTDESTINATARIO.Value
                Else
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID=" & SLOTDESTINATARIO.Value
                    Dim myReaderx As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderx.Read Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU (ID,ID_CONTRATTO,ID_GRUPPO,ID_FILIALE,COGNOME,NOME,N_OPERATORE,DATA_APP,ORE_APP,ID_SPORTELLO) VALUES (SISCOM_MI.SEQ_CONVOCAZIONI_AU.NEXTVAL," & myReader1("ID_contratto") & "," & GRUPPO.Value & "," & myReader1("ID_FILIALE") & ",'" & par.PulisciStrSql(par.IfNull(myReader1("COGNOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReader1("NOME"), "")) & "','" & myReader1("N_OPERATORE") & "','" & Mid(myReaderx("INIZIO"), 1, 8) & "','" & Mid(myReaderx("INIZIO"), 9, 2) & "." & Mid(myReaderx("INIZIO"), 11, 2) & "'," & SPORTELLO.Value & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "select SISCOM_MI.SEQ_CONVOCAZIONI_AU.CURRVAL FROM dual "
                        Dim myReaderx1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderx1.Read Then
                            INDICECONVOCAZIONE = myReaderx1(0)
                        End If
                        myReaderx1.Close()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.AGENDA_APPUNTAMENTI SET ID_STATO=1,ID_CONVOCAZIONE=" & INDICECONVOCAZIONE & ",COD_CONTRATTO='" & myReader1("COD_CONTRATTO") & "',COGNOME='" & par.PulisciStrSql(par.IfNull(myReader1("COGNOME"), "")) & "',NOME='" & par.PulisciStrSql(par.IfNull(myReader1("NOME"), "")) & "',ID_CONTRATTO=" & myReader1("ID_CONTRATTO") & " WHERE ID=" & SLOTDESTINATARIO.Value
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & IDA.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'NUOVO APPUNTAMENTO FISSATO AL " & par.FormattaData(Mid(myReaderx("INIZIO"), 1, 8)) & " ORE " & Mid(myReaderx("INIZIO"), 9, 2) & "." & Mid(myReaderx("INIZIO"), 11, 2) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderx.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI_EVENTI (ID_APPUNTAMENTO,DATA_ORA,ID_OPERATORE,NOTE) VALUES (" & SLOTDESTINATARIO.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO IMPOSTATO DA FISSA NUOVO APPUNTAMENTO - " & par.PulisciStrSql(myReader1("COGNOME")) & " " & par.PulisciStrSql(myReader1("NOME")) & " - Cod.Contratto: " & par.PulisciStrSql(myReader1("COD_CONTRATTO")) & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT  *FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID=" & SLOTDESTINATARIO.Value
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        NUOVADATA = par.FormattaData(Mid(myReader("INIZIO"), 1, 8))
                        NUOVAORA = Mid(myReader("INIZIO"), 9, 2) & "." & Mid(myReader("INIZIO"), 11, 2)

                        If TIPO.Value = "0" Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader1("ID_CONVOCAZIONE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO SPOSTATO AL " & NUOVADATA & " ORE " & NUOVAORA & "')"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU SET DATA_APP='" & Mid(myReader("INIZIO"), 1, 8) & "',ORE_APP='" & Mid(myReader("INIZIO"), 9, 2) & "." & Mid(myReader("INIZIO"), 11, 2) & "',ORE_FINE_APP='' WHERE ID=" & myReader1("ID_CONVOCAZIONE")
                            par.cmd.ExecuteNonQuery()
                        Else
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader1("ID_CONVOCAZIONE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'APPUNTAMENTO FISSATO AL " & NUOVADATA & " ORE " & NUOVAORA & "')"
                            par.cmd.ExecuteNonQuery()
                        End If

                    End If
                    myReader.Close()
                    IDA.Value = SLOTDESTINATARIO.Value

                End If
                myReader1.Close()

                GiornoApp = Format(CDate(NUOVADATA), "yyyyMMdd")
                OraApp = NUOVAORA

                GiornoPartenza = Format(DateAdd("d", CDate(par.FormattaData(GiornoApp)).DayOfWeek * -1, CDate(par.FormattaData(GiornoApp))), "yyyyMMdd")
                GiornoArrivo = Format(DateAdd("d", 6, CDate(par.FormattaData(GiornoPartenza))), "yyyyMMdd")

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()






            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End Try

            Calendar1.TodaysDate = CDate(par.FormattaData(GiornoApp))
            CaricaDisponibilita(Format(Calendar1.TodaysDate.Month, "00"), CStr(Calendar1.TodaysDate.Year))
            CaricaDati()
            CaricaCalendario()


            'CaricaDisponibilita(Format(Calendar1.TodaysDate.Month, "00"), CStr(Calendar1.TodaysDate.Year))
            'CaricaDati()
            'CaricaCalendario()
        End If
    End Sub

    Protected Sub imgNuovoSlot_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgNuovoSlot.Click
        CaricaCalendario()
    End Sub
End Class
