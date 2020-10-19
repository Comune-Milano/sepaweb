
Partial Class CONS_Prenotazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim MioGiorno As String

    Dim i As Integer

    Public Property Id_Domanda() As String
        Get
            If Not (ViewState("par_Id_Domanda") Is Nothing) Then
                Return CStr(ViewState("par_Id_Domanda"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Id_Domanda") = value
        End Set

    End Property

    Public Property STATO() As String
        Get
            If Not (ViewState("par_STATO") Is Nothing) Then
                Return CStr(ViewState("par_STATO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_STATO") = value
        End Set

    End Property

    Public Property CF() As String
        Get
            If Not (ViewState("par_CF") Is Nothing) Then
                Return CStr(ViewState("par_CF"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CF") = value
        End Set

    End Property

    Public Property PG() As String
        Get
            If Not (ViewState("par_PG") Is Nothing) Then
                Return CStr(ViewState("par_PG"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_PG") = value
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Id_Domanda = Request.QueryString("ID")
            Nominativo = Request.QueryString("NOMINATIVO")
            PG = Request.QueryString("PG")
            CF = Request.QueryString("CF")
            STATO = Request.QueryString("STATO")
            Riempi(0)
        End If
    End Sub

    Private Function Riempi(ByVal periodo As Integer)
        If par.OracleConn.State = Data.ConnectionState.Open Then
            Exit Function
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        par.cmd.CommandText = "select prenotazioni_giorni.giorno,prenotazioni_appuntamenti.orario,prenotazioni_appuntamenti.id from prenotazioni_giorni,prenotazioni_appuntamenti where (prenotazioni_appuntamenti.domanda='" & Nominativo & "#" & PG & "#" & CF & "' or prenotazioni_appuntamenti.domanda1='" & Nominativo & "#" & PG & "#" & CF & "' or prenotazioni_appuntamenti.domanda2='" & Nominativo & "#" & PG & "#" & CF & "') and prenotazioni_giorni.giorno>=" & par.AggiustaData(DateSerial(Year(Now), Month(Now), Day(Now))) & " and prenotazioni_giorni.giorno<=" & par.AggiustaData(DateSerial(Year(Now), Month(Now), Day(Now) + 40)) & " and prenotazioni_giorni.id=prenotazioni_appuntamenti.id_giorno order by prenotazioni_giorni.giorno asc,prenotazioni_appuntamenti.orario asc"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            Label1.Visible = True
            Label1.Text = "<BR>Attenzione...questa domanda ha già una prenotazione valida per " & ConvertiGiorno(par.FormattaData(myReader("giorno"))) & " " & par.FormattaData(myReader("giorno")) & " alle ore " & ConvertiOra(myReader("orario"))
            btnConferma.Enabled = False
            btnSuccessivi.Visible = False
            myReader.Close()
            par.OracleConn.Close()
            Exit Function
        End If

        MioGiorno = "//"
        If periodo = 0 Then
            par.cmd.CommandText = "select prenotazioni_giorni.giorno,prenotazioni_appuntamenti.orario,prenotazioni_appuntamenti.id from prenotazioni_giorni,prenotazioni_appuntamenti where (prenotazioni_appuntamenti.domanda='0' or prenotazioni_appuntamenti.domanda1='0' or prenotazioni_appuntamenti.domanda2='0') and prenotazioni_giorni.giorno>=" & par.AggiustaData(DateSerial(Year(Now), Month(Now), Day(Now) + 1)) & " and prenotazioni_giorni.giorno<=" & par.AggiustaData(DateSerial(Year(Now), Month(Now), Day(Now) + 15)) & " and prenotazioni_giorni.id=prenotazioni_appuntamenti.id_giorno order by prenotazioni_giorni.giorno asc,prenotazioni_appuntamenti.orario asc"
        Else
            par.cmd.CommandText = "select prenotazioni_giorni.giorno,prenotazioni_appuntamenti.orario,prenotazioni_appuntamenti.id from prenotazioni_giorni,prenotazioni_appuntamenti where (prenotazioni_appuntamenti.domanda='0' or prenotazioni_appuntamenti.domanda1='0' or prenotazioni_appuntamenti.domanda2='0') and prenotazioni_giorni.giorno>=" & par.AggiustaData(DateSerial(Year(Now), Month(Now), Day(Now) + 16)) & " and prenotazioni_giorni.giorno<=" & par.AggiustaData(DateSerial(Year(Now), Month(Now), Day(Now) + 30)) & " and prenotazioni_giorni.id=prenotazioni_appuntamenti.id_giorno order by prenotazioni_giorni.giorno asc,prenotazioni_appuntamenti.orario asc"
        End If
        myReader = par.cmd.ExecuteReader()
        i = 0
        While myReader.Read
            If MioGiorno = ConvertiGiorno(par.FormattaData(myReader("giorno"))) Then
                R1.Items.Add("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & " ore: " & ConvertiOra(myReader("orario")))
                R1.Items(i).Value = myReader("id")
            Else
                R1.Items.Add(ConvertiGiorno(par.FormattaData(myReader("giorno"))) & " " & par.FormattaData(myReader("giorno")) & "&nbsp;&nbsp;ore: " & ConvertiOra(myReader("orario")))
                R1.Items(i).Value = myReader("id")
                MioGiorno = ConvertiGiorno(par.FormattaData(myReader("giorno")))
            End If
            i = i + 1
        End While
        myReader.Close()
        par.OracleConn.Close()

        If i = 0 Then
            btnConferma.Enabled = False
            btnSuccessivi.Visible = True
            btnSuccessivi.Text = "Dal " & DateSerial(Year(Now), Month(Now), Day(Now) + 16) & " Al " & DateSerial(Year(Now), Month(Now), Day(Now) + 30)
        Else
            btnConferma.Enabled = True
            btnSuccessivi.Visible = False
        End If
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


    Protected Sub btnConferma_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConferma.Click
        Dim Confermato As Boolean = False
        Dim MiaS As String = ""
        Dim scriptblock As String = ""

        If R1.SelectedValue <> "" Then
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select prenotazioni_appuntamenti.domanda,prenotazioni_appuntamenti.domanda1,prenotazioni_appuntamenti.domanda2 from prenotazioni_appuntamenti where prenotazioni_appuntamenti.id=" & R1.SelectedValue
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If myReader("domanda") = "0" Then
                    MiaS = "DOMANDA='" & Nominativo & "#" & PG & "#" & CF & "',ID_CAF=" & Session.Item("ID_CAF") & ",ID_OPERATORE=" & Session.Item("ID_OPERATORE") & " "
                ElseIf myReader("domanda1") = "0" Then
                    MiaS = "DOMANDA1='" & Nominativo & "#" & PG & "#" & CF & "',ID_CAF1=" & Session.Item("ID_CAF") & ",ID_OPERATORE1=" & Session.Item("ID_OPERATORE") & " "
                ElseIf myReader("domanda2") = "0" Then
                    MiaS = "DOMANDA2='" & Nominativo & "#" & PG & "#" & CF & "',ID_CAF2=" & Session.Item("ID_CAF") & ",ID_OPERATORE2=" & Session.Item("ID_OPERATORE") & " "
                End If
                If MiaS <> "" Then
                    par.cmd.CommandText = "UPDATE prenotazioni_appuntamenti SET " & MiaS & " WHERE ID=" & R1.SelectedValue
                    par.cmd.ExecuteNonQuery()
                    btnConferma.Enabled = False



                    btnConferma.Enabled = False
                    btnSuccessivi.Visible = False


                    par.cmd.CommandText = "select prenotazioni_giorni.giorno,prenotazioni_appuntamenti.orario,prenotazioni_appuntamenti.id from prenotazioni_giorni,prenotazioni_appuntamenti where prenotazioni_giorni.id=prenotazioni_appuntamenti.id_giorno AND prenotazioni_appuntamenti.id=" & R1.SelectedValue
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                            & "VALUES (" & Id_Domanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & STATO _
                                            & "','F151','" & par.FormattaData(myReader1("giorno")) & " alle ore " & ConvertiOra(myReader1("orario")) & "','I')"
                        par.cmd.ExecuteNonQuery()

                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "window.open('Ricevuta.aspx?DOMANDA=" & PG & "&NOMINATIVO=" & par.VaroleDaPassare(Nominativo) & "&GIORNO=" & par.VaroleDaPassare(ConvertiGiorno(par.FormattaData(myReader1("giorno"))) & " " & par.FormattaData(myReader1("giorno")) & " alle ore " & ConvertiOra(myReader1("orario"))) & "','Ricevuta');" _
                        & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript5576")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript5576", scriptblock)
                        End If
                    End If
                    myReader1.Close()
                Else
                    Response.Write("<script>alert('Non è possibile prenotare perchè questo orario è stato nel frattempo prenotato da altro utente!');</script>")
                End If
            End If
            myReader.Close()
            par.OracleConn.Close()
        End If
    End Sub

    Protected Sub btnSuccessivi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSuccessivi.Click
        Riempi(1)
    End Sub
End Class
