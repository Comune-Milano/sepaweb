
Partial Class ANAUT_SimulazioneConv
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            CaricaAU()
            CaricaListe()
        End If
        txtDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Private Function CaricaListe()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            chListListe.Items.Clear()

            par.cmd.CommandText = "SELECT * FROM UTENZA_LISTE WHERE lettera_creata=0 AND id IN (SELECT ID_lista_conv from UTENZA_LISTE_CDETT where id_lista in (select id from UTENZA_LISTE_CONV where id_au=" & IndiceBando & "))"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                chListListe.Items.Add(New ListItem(par.IfNull(myReader("descrizione"), ""), par.IfNull(myReader("ID"), "0")))
            Loop
            myReader.Close()

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Public Property IndiceBando() As Long
        Get
            If Not (ViewState("par_IndiceBando") Is Nothing) Then
                Return CLng(ViewState("par_IndiceBando"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IndiceBando") = value
        End Set
    End Property

    Private Sub CaricaAU()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "SELECT DESCRIZIONE,ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                IndiceBando = myReader("ID")
                Label1.Text = par.IfNull(myReader("DESCRIZIONE"), "")
                Label2.Attributes.Add("onclick", "javascript:window.open('BandoAU.aspx?ID=" & IndiceBando & "&L=" & par.Cripta("LETTURA") & "', 'Anagrafe', 'status:no;dialogWidth:600px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');")
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:SimulazioneConv - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        Dim trovato As Boolean = False

        trovato = False
        If txtDal.Text = "" Or txtAl.Text = "" Then
            trovato = True
        End If
        If trovato = True Then
            Response.Write("<script>alert('Inserire entrambe le date di inizio e fine convocazione!');</script>")
            Exit Sub
        End If

        trovato = False
        If IsDate(txtDal.Text) = False Or IsDate(txtAl.Text) = False Then
            trovato = True
        End If
        If trovato = True Then
            Response.Write("<script>alert('Inserire entrambe le date VALIDE!');</script>")
            Exit Sub
        End If

        trovato = False
        If par.AggiustaData(txtDal.Text) > par.AggiustaData(txtAl.Text) Then
            trovato = True
        End If
        If trovato = True Then
            Response.Write("<script>alert('Intervallo Date non valido!');</script>")
            Exit Sub
        End If



        Dim s As String = ""
        Dim Sportelli As String = ""
        For i = 0 To chListListe.Items.Count - 1
            If chListListe.Items(i).Selected = True Then
                s = s & chListListe.Items(i).Value & ","
            End If
        Next
        If s <> "" Then
            s = Mid(s, 1, Len(s) - 1)
            s = " ID_LISTA_CONV IN (" & s & ")"
        Else
            Response.Write("<script>alert('Selezionare almeno una lista di convocazione!');</script>")
            Exit Sub
        End If


        If par.AggiustaData(txtDal.Text) < par.AggiustaData(txtAl.Text) Then
            Response.Redirect("SimulazioneConv1.aspx?ID=" & IndiceBando & "&DA=" & par.AggiustaData(txtDal.Text) & "&A=" & par.AggiustaData(txtAl.Text) & "&S=" & par.Cripta(s))
        Else
            Response.Write("<script>alert('Intervallo Date non valido!')</script>")
        End If
    End Sub

    'Protected Sub chListStrutture_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chListListe.SelectedIndexChanged
    '    CaricaSportelli()
    'End Sub

    'Private Function CaricaSportelli()
    '    Try
    '        Dim i As Integer = 0
    '        Dim s As String = ""

    '        If PAR.OracleConn.State = Data.ConnectionState.Closed Then
    '            PAR.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If

    '        chListSportelli.Items.Clear()

    '        For i = 0 To chListListe.Items.Count - 1
    '            If chListListe.Items(i).Selected = True Then
    '                s = s & chListListe.Items(i).Value & ","
    '            End If
    '        Next
    '        If s <> "" Then
    '            s = Mid(s, 1, Len(s) - 1)
    '            s = " WHERE ID_TAB_FILIALI IN (" & s & ")"

    '            par.cmd.CommandText = "SELECT DISTINCT SEDE,ID_SPORTELLO FROM UTENZA_LISTE_CDETT " & s & " ORDER BY SEDE ASC"
    '            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
    '            Do While myReader.Read
    '                chListSportelli.Items.Add(New ListItem(PAR.IfNull(myReader("SEDE"), ""), PAR.IfNull(myReader("id_SPORTELLO"), "0")))
    '            Loop
    '            myReader.Close()

    '        End If



    '        PAR.OracleConn.Close()
    '        PAR.cmd.Dispose()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '    Catch ex As Exception
    '        PAR.OracleConn.Close()
    '        PAR.cmd.Dispose()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '    End Try
    'End Function
End Class
