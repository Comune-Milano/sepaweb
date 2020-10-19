
Partial Class SEGNALAZIONI_Agenda_StatiAppuntamenti
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href='../../AccessoNegato.htm';</script>")
            Exit Sub
        End If
        Try
            connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                caricaElencoStatiAppuntamento()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Page_Load - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaElencoStatiAppuntamento()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.APPUNTAMENTI_STATI "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                DataGridStatiAppuntamenti.DataSource = dt
                DataGridStatiAppuntamenti.DataBind()
                For Each Items As DataGridItem In DataGridStatiAppuntamenti.Items
                    CType(Items.FindControl("ImageButtonElimina"), ImageButton).OnClientClick = "javascript:Elimina(" & Items.Cells(0).Text & ");"
                Next
                TestoErrore.Text = ""
                DataGridStatiAppuntamenti.Visible = True
            Else
                DataGridStatiAppuntamenti.Visible = False
                TestoErrore.Text = "Non sono presenti tipologie di stato appuntamento"
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Elenco Stati - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub ButtonNuovoStatoAppuntamento_Click(sender As Object, e As System.EventArgs) Handles ButtonNuovoStato.Click
        MultiView1.ActiveViewIndex = 1
        MultiView2.ActiveViewIndex = 1
        inizializzaCampi()
    End Sub

    Protected Sub ButtonInserisciStatoAppuntamento_Click(sender As Object, e As System.EventArgs) Handles ButtonInserisciStatoAppuntamento.Click
        If controlloCampiStati() Then
            inserimentoStatoAppuntamento()
        End If
    End Sub

    Private Function controlloCampiStati() As Boolean
        Dim controllo As Boolean = True
        If Trim(TextBoxDescrizione.Text = "") Then
            Response.Write("<script>alert('Il campo descrizione è obbligatorio!');</script>")
            Return False
        End If
        Return controllo
    End Function

    Private Sub inserimentoStatoAppuntamento()
        Try
            connData.apri()

            par.cmd.CommandText = " INSERT INTO SISCOM_MI.APPUNTAMENTI_STATI (ID, DESCRIZIONE) " _
                & " VALUES (SISCOM_MI.SEQ_APPUNTAMENTI_STATI.NEXTVAL, " _
                & " '" & UCase(Trim(TextBoxDescrizione.Text.Replace("'", "''"))) & "')"
            par.cmd.ExecuteNonQuery()

            connData.chiudi()
            inizializzaCampi()
            Response.Write("<script>alert('Stato appuntamento aggiunto correttamente!');</script>")
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Inserimento Stati - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub ButtonIndietro_Click(sender As Object, e As System.EventArgs) Handles ButtonIndietro.Click
        MultiView1.ActiveViewIndex = 0
        MultiView2.ActiveViewIndex = 0
        caricaElencoStatiAppuntamento()
    End Sub

    Private Sub inizializzaCampi()
        TextBoxDescrizione.Text = ""
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        If StatoSelezionato.Value <> "" Then
            Try
                connData.apri()
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPUNTAMENTI_STATI WHERE ID=" & StatoSelezionato.Value
                par.cmd.ExecuteNonQuery()
                connData.chiudi()
                Response.Write("<script>alert('Stato eliminato correttamente!');</script>")
            Catch ex1 As Oracle.DataAccess.Client.OracleException
                If ex1.Number = 2292 Then
                    connData.chiudi()
                    Response.Write("<script>alert('Impossibile eliminare lo stato selezionato perchè già in uso!');</script>")
                End If
            Catch ex As Exception
                connData.chiudi()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " Eliminazione Stati - " & ex.Message)
                Response.Redirect("../../Errore.aspx", False)
            End Try
        End If
        caricaElencoStatiAppuntamento()
    End Sub

End Class