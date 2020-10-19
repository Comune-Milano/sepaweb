
Partial Class GESTIONE_CONTATTI_Eventi
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                CaricaEventiSegnalazione()
            End If
            If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
                NoMenu()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Eventi Segnalazione - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub NoMenu()
        Try
            CType(Me.Master.FindControl("NavigationMenu"), System.Web.UI.WebControls.Menu).Visible = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Eventi - NoMenu - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 1
            If CType(Me.Master.FindControl("FLGC"), HiddenField).Value = "0" Then
                par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", , True)
                Exit Sub
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Eventi Segnalazione - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaEventiSegnalazione()
        Try
            connData.apri()
            idSegnalazione.Value = "0"
            If Not IsNothing(Request.QueryString("IDS")) Then
                idSegnalazione.Value = Request.QueryString("IDS")
                lblTitolo.Text = "Segnalazione numero " & idSegnalazione.Value
                par.cmd.CommandText = "SELECT " _
                    & " SEPA.OPERATORI.OPERATORE, " _
                    & " TO_CHAR(TO_DATE(SUBSTR(EVENTI_SEGNALAZIONI.DATA_ORA,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS ""DATA EVENTO""," _
                    & " SUBSTR(EVENTI_SEGNALAZIONI.DATA_ORA,9,2)||':'||SUBSTR(EVENTI_SEGNALAZIONI.DATA_ORA,11,2) AS ""ORA EVENTO""," _
                    & " SISCOM_MI.TAB_EVENTI.DESCRIZIONE AS ""TIPO EVENTO"", " _
                    & " (CASE WHEN (VALORE_NEW IS NOT NULL) THEN (EVENTI_SEGNALAZIONI.MOTIVAZIONE||'<br />'||'Valore attuale: '||VALORE_NEW||' - Valore precedente: '||VALORE_OLD) ELSE (EVENTI_SEGNALAZIONI.MOTIVAZIONE) END)  AS MOTIVAZIONE " _
                    & " FROM SISCOM_MI.EVENTI_SEGNALAZIONI, SEPA.OPERATORI, SISCOM_MI.TAB_EVENTI, " _
                    & " SISCOM_MI.SEGNALAZIONI WHERE EVENTI_SEGNALAZIONI.COD_EVENTO = TAB_EVENTI.COD " _
                    & " AND EVENTI_SEGNALAZIONI.ID_SEGNALAZIONE = SEGNALAZIONI.ID " _
                    & " AND EVENTI_SEGNALAZIONI.ID_OPERATORE = OPERATORI.ID " _
                    & " AND ID_SEGNALAZIONE =  " & idSegnalazione.Value & " ORDER BY DATA_ORA DESC"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    DataGridEventi.DataSource = dt
                    DataGridEventi.DataBind()
                    DataGridEventi.Visible = True
                Else
                    DataGridEventi.Visible = False
                End If
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Eventi Segnalazione - CaricaEventiSegnalazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub imgIndietro_Click(sender As Object, e As System.EventArgs) Handles imgIndietro.Click
        If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
            Response.Redirect("Segnalazione.aspx?NM=1&IDS=" & idSegnalazione.Value)
        Else
            Response.Redirect("Segnalazione.aspx?IDS=" & idSegnalazione.Value)
        End If
    End Sub
End Class
