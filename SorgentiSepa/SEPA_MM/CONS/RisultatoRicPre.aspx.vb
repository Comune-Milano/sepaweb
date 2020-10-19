
Partial Class CONS_RisultatoRicPre
    Inherits PageSetIdMode
    Dim sValoreCF As String
    Dim sValorePG As String
    Dim sValoreNOME As String
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            sValoreCF = Request.QueryString("CF")
            sValorePG = Request.QueryString("PG")
            sValoreNOME = Request.QueryString("NOME")
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Text = "-1"
            Cerca()
        End If
    End Sub

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Public Property Nominativo() As String
        Get
            If Not (ViewState("par_Nominativo") Is Nothing) Then
                Return CStr(ViewState("par_Nominativo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Nominativo") = value
        End Set

    End Property

    Public Property Protocollo() As String
        Get
            If Not (ViewState("par_Protocollo") Is Nothing) Then
                Return CStr(ViewState("par_Protocollo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Protocollo") = value
        End Set

    End Property

    Public Property Cod_Fiscale() As String
        Get
            If Not (ViewState("par_Cod_Fiscale") Is Nothing) Then
                Return CStr(ViewState("par_Cod_Fiscale"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Cod_Fiscale") = value
        End Set

    End Property

    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim I As Integer

        bTrovato = False
        sStringaSql = ""


        If sValoreCF <> "" Then
            sValore = sValoreCF
            bTrovato = True
            sStringaSql = sStringaSql & " (INSTR(PRENOTAZIONI_APPUNTAMENTI.DOMANDA,'" & par.PulisciStrSql(sValore) & "',1)<>0 OR INSTR(PRENOTAZIONI_APPUNTAMENTI.DOMANDA1,'" & par.PulisciStrSql(sValore) & "',1)<>0 OR INSTR(PRENOTAZIONI_APPUNTAMENTI.DOMANDA2,'" & par.PulisciStrSql(sValore) & "',1)<>0) "
        End If

        If sValorePG <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValorePG
            bTrovato = True
            sStringaSql = sStringaSql & " (INSTR(PRENOTAZIONI_APPUNTAMENTI.DOMANDA,'" & par.PulisciStrSql(sValore) & "',1)<>0 OR INSTR(PRENOTAZIONI_APPUNTAMENTI.DOMANDA1,'" & par.PulisciStrSql(sValore) & "',1)<>0 OR INSTR(PRENOTAZIONI_APPUNTAMENTI.DOMANDA2,'" & par.PulisciStrSql(sValore) & "',1)<>0) "
        End If

        If sValoreNOME <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreNOME
            bTrovato = True
            sStringaSql = sStringaSql & " (INSTR(PRENOTAZIONI_APPUNTAMENTI.DOMANDA,'" & par.PulisciStrSql(sValore) & "',1)<>0 OR INSTR(PRENOTAZIONI_APPUNTAMENTI.DOMANDA1,'" & par.PulisciStrSql(sValore) & "',1)<>0 OR INSTR(PRENOTAZIONI_APPUNTAMENTI.DOMANDA2,'" & par.PulisciStrSql(sValore) & "',1)<>0) "
        End If

        sStringaSQL1 = "SELECT PRENOTAZIONI_APPUNTAMENTI.*,PRENOTAZIONI_GIORNI.GIORNO FROM PRENOTAZIONI_APPUNTAMENTI,PRENOTAZIONI_GIORNI WHERE PRENOTAZIONI_APPUNTAMENTI.ID_GIORNO=PRENOTAZIONI_GIORNI.ID AND PRENOTAZIONI_APPUNTAMENTI.ID_CAF=" & Session.Item("ID_CAF")

        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY PRENOTAZIONI_APPUNTAMENTI.ID ASC"
        par.OracleConn.Open()
        Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSQL1, par.OracleConn)
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        I = 0
        Do While myReader.Read()
            Label3.Text = CInt(Label3.Text) + 1
            If myReader("DOMANDA") <> "1" And myReader("DOMANDA") <> "0" Then
                CreaRighe(myReader("DOMANDA"))
                CH1.Items.Add("<b>" & Cod_Fiscale & " " & Nominativo & "</b> --" & ConvertiGiorno(par.FormattaData(myReader("giorno"))) & " " & par.FormattaData(myReader("giorno")) & "&nbsp;&nbsp;ore: " & ConvertiOra(myReader("orario")))
                CH1.Items(I).Value = myReader("id")
                L1.Items.Add("0")
                L1.Items(I).Value = Protocollo
            End If
            If myReader("DOMANDA1") <> "1" And myReader("DOMANDA1") <> "0" Then
                CreaRighe(myReader("DOMANDA1"))
                CH1.Items.Add("<b>" & Cod_Fiscale & " " & Nominativo & "</b> --" & ConvertiGiorno(par.FormattaData(myReader("giorno"))) & " " & par.FormattaData(myReader("giorno")) & "&nbsp;&nbsp;ore: " & ConvertiOra(myReader("orario")))
                CH1.Items(I).Value = myReader("id")
                L1.Items.Add("1")
                L1.Items(I).Value = Protocollo
            End If
            If myReader("DOMANDA2") <> "1" And myReader("DOMANDA2") <> "0" Then
                CreaRighe(myReader("DOMANDA2"))
                CH1.Items.Add("<b>" & Cod_Fiscale & " " & Nominativo & "</b> --" & ConvertiGiorno(par.FormattaData(myReader("giorno"))) & " " & par.FormattaData(myReader("giorno")) & "&nbsp;&nbsp;ore: " & ConvertiOra(myReader("orario")))
                CH1.Items(I).Value = myReader("id")
                L1.Items.Add("2")
                L1.Items(I).Value = Protocollo
            End If
        Loop
        Label3.Text = "n° " & Label3.Text
        cmd.Dispose()
        myReader.Close()
        par.OracleConn.Close()

    End Function

    Private Function ConvertiGiorno(ByVal giorno As String) As String
        If IsDate(giorno) Then
            Select Case Weekday(giorno, vbMonday)
                Case Is = 1 : ConvertiGiorno = "Lunedì&nbsp;&nbsp;&nbsp;"
                Case Is = 2 : ConvertiGiorno = "Martedì&nbsp;&nbsp;"
                Case Is = 3 : ConvertiGiorno = "Mercoledì"
                Case Is = 4 : ConvertiGiorno = "Giovedì&nbsp;&nbsp;"
                Case Is = 5 : ConvertiGiorno = "Venerdì&nbsp;&nbsp;"
                Case Is = 6 : ConvertiGiorno = "Sabato&nbsp;&nbsp;&nbsp;"
                Case Else
                    ConvertiGiorno = "Domenica&nbsp;"
            End Select
        End If
    End Function

    Private Function ConvertiOra(ByVal Ora As String) As String
        Select Case Ora
            Case "0"
                ConvertiOra = "09:30"
            Case 1
                ConvertiOra = "10:00"
            Case 2
                ConvertiOra = "10:30"
            Case 3
                ConvertiOra = "11:00"
            Case 4
                ConvertiOra = "11:30"
            Case 5
                ConvertiOra = "14:00"
            Case 6
                ConvertiOra = "14:30"
            Case 7
                ConvertiOra = "15:00"
            Case 8
                ConvertiOra = "15:30"
        End Select
    End Function

    Private Function CreaRighe(ByVal testo As String)
        Dim pos As Integer
        Dim pos1 As Integer

        Nominativo = ""
        Protocollo = ""
        Cod_Fiscale = ""

        If testo = vbCrLf Then Exit Function
        pos = InStr(1, testo, "#")
        If pos <> 0 Then
            Nominativo = Mid(testo, 1, pos - 1)
        End If

        pos1 = InStr(pos + 1, testo, "#")
        If pos1 <> 0 Then
            Protocollo = Mid(testo, pos + 1, pos1 - 1 - pos)
        End If

        Cod_Fiscale = Mid(testo, pos1 + 1, 16)


    End Function


    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaPrenotazioni.aspx""</script>")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

   

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizza.Click
        Dim n As Integer

        If par.OracleConn.State = Data.ConnectionState.Open Then
            Exit Sub
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        For n = 0 To CH1.Items.Count - 1
            If CH1.Items(n).Enabled = True Then
                If L1.Items(n).Text = "0" Then
                    par.cmd.CommandText = "update prenotazioni_appuntamenti SET DOMANDA='0',ID_CAF=NULL,ID_OPERATORE=NULL WHERE ID=" & CH1.Items(n).Value
                End If
                If L1.Items(n).Text = "1" Then
                    par.cmd.CommandText = "update prenotazioni_appuntamenti SET DOMANDA1='0',ID_CAF1=NULL,ID_OPERATORE1=NULL WHERE ID=" & CH1.Items(n).Value
                End If
                If L1.Items(n).Text = "2" Then
                    par.cmd.CommandText = "update prenotazioni_appuntamenti SET DOMANDA2='0',ID_CAF2=NULL,ID_OPERATORE2=NULL WHERE ID=" & CH1.Items(n).Value
                End If
                par.cmd.ExecuteNonQuery()

                Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("SELECT * FROM DOMANDE_BANDO WHERE PG='" & Protocollo & "'", par.OracleConn)
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()


                If myReader.Read Then
                    par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) VALUES (" & myReader("id") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & myReader("id_stato") & "','F152','" & Mid(CH1.Items(n).Text, InStr(CH1.Items(n).Text, "--", CompareMethod.Text) + 2) & "','I')"
                    par.cmd.ExecuteNonQuery()
                End If
                myReader.Close()

                CH1.Items(n).Enabled = False
            End If
        Next

    End Sub
End Class
