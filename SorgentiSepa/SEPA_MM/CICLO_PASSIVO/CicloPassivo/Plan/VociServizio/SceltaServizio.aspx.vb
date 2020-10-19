
Partial Class CicloPassivo_CicloPassivo_APPALTI_SceltaServizio
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            Response.Expires = 0
            If IsPostBack = False Then
                CaricaEsercizio()
                CaricaServizi()

            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub CaricaEsercizio()
        Try
            '*****PEPPE MODIFY 30/09/2010*****
            '************APERTURA CONNESSIONE**********
            par.OracleConn.Open()
            par.SettaCommand(par)

            'par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN WHERE SISCOM_MI.PF_MAIN.ID_STATO=5 AND SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO"
            par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') AS INIZIO, " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_STATI.descrizione FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,siscom_mi.PF_STATI " _
                                & "WHERE id_stato > = 5 and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND id_stato = PF_STATI.ID order by T_ESERCIZIO_FINANZIARIO.ID desc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'cmbesercizio.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbesercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE"), "") & " (" & par.IfNull(myReader1("DESCRIZIONE"), "") & ")", par.IfNull(myReader1("ID"), -1)))
            End While
            'cmbesercizio.SelectedValue = -1
            myReader1.Close()


            '************CHIUSURA CONNESSIONE**********
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Sub CaricaServizi()
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.RiempiDList(Me, par.OracleConn, "cmbservizio", "select * from siscom_mi.tab_servizi order by descrizione asc, id asc ", "DESCRIZIONE", "ID")


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        If cmbservizio.SelectedValue <> "" And Me.cmbEsercizio.SelectedValue <> "" Then

            Response.Redirect("VociServizio.aspx?IDS=" & cmbservizio.SelectedValue & "&SE=" & cmbservizio.SelectedItem.Text & "&IDES=" & Me.cmbEsercizio.SelectedValue)

        Else
            Response.Write("<script>alert('Attenzione! Se non si seleziona un servizio ed un esercizio non si possono inserire voci di servizio ')</script>")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click

        Response.Write("<script>document.location.href=""../../../pagina_home.aspx""</script>")

    End Sub

    Public Property tipo() As String
        Get
            If Not (ViewState("par_tipo") Is Nothing) Then
                Return CStr(ViewState("par_tipo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_tipo") = value
        End Set

    End Property

    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        Try
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If

        Catch ex As Exception
        End Try
        Return a

    End Function
End Class
