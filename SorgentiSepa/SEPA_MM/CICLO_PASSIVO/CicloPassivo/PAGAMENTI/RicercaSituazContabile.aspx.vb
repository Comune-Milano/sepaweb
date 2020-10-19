'*** RICERCA SITUAZIONE CONTABILE

Partial Class RicercaSituazContabile
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            CaricaEsercizi()

            Me.txtDataAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        End If

    End Sub



    'CARICO COMBO ESERCIZI FINANZIARI
    Private Sub CaricaEsercizi()
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmbEsercizio.Items.Clear()
            Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = " select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE " _
                                & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN " _
                                & " where SISCOM_MI.PF_MAIN.ID_STATO=5 " _
                                & "  and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                Me.cmbEsercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            '**************************

            Me.cmbEsercizio.SelectedValue = "-1"


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub



    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        Dim FlagConnessione As Boolean

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            par.cmd.CommandText = " select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, T_ESERCIZIO_FINANZIARIO.INIZIO,T_ESERCIZIO_FINANZIARIO.FINE " _
                                & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                                & " where SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=" & cmbEsercizio.SelectedValue

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader1.Read Then
                txtDataDAL.Text = par.FormattaData(par.IfNull(myReader1("INIZIO"), ""))
                txtDataAL.Text = par.FormattaData(par.IfNull(myReader1("FINE"), ""))
                txtDATA.Value = par.FormattaData(par.IfNull(myReader1("FINE"), ""))
            End If
            myReader1.Close()
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim ControlloCampi As Boolean
        Try

            ControlloCampi = True

            If Me.cmbEsercizio.SelectedValue = -1 Then
                Response.Write("<script>alert('Selezionare l\'esercizio finanziario!');</script>")
                ControlloCampi = False
                Exit Sub
            End If

            If par.AggiustaData(Me.txtDataAL.Text) > par.AggiustaData(Me.txtDATA.Value) Then

                If par.AggiustaData(Me.txtDataAL.Text) < par.AggiustaData(Me.txtDataDAL.Text) Then
                    Response.Write("<script>alert('La data deve essere maggiore o uguale alla data di inizio esercizio!');</script>")
                    ControlloCampi = False
                    Exit Sub
                Else
                    Response.Write("<script>alert('La data deve essere minore o uguale alla data di fine esercizio!');</script>")
                    ControlloCampi = False
                    Exit Sub
                End If
            Else
                If par.AggiustaData(Me.txtDataAL.Text) < par.AggiustaData(Me.txtDataDAL.Text) Then
                    Response.Write("<script>alert('La data deve essere maggiore o uguale alla data di inizio esercizio!');</script>")
                    ControlloCampi = False
                    Exit Sub
                End If
            End If

            If ControlloCampi = True Then

                Response.Write("<script>window.open('StampaSC.aspx?ID=" & Me.cmbEsercizio.SelectedValue & "&VOCI=" & ChkStampa.Checked & "&DAL=" & par.IfEmpty(par.AggiustaData(Me.txtDataDAL.Text), "") & "&AL=" & par.IfEmpty(par.AggiustaData(Me.txtDataAL.Text), "") & "','REPORT','');</script>")
            End If

            '            Response.Write("<script>location.replace('StampaSC.aspx?ID=" & Me.cmbEsercizio.SelectedValue & "&DAL=" & par.IfEmpty(par.AggiustaData(Me.txtDataDAL.Text), "") & "&AL=" & par.IfEmpty(par.AggiustaData(Me.txtDataAL.Text), "") & "');</script>")

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function


End Class
