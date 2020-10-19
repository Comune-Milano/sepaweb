
Partial Class GESTIONE_CONTATTI_EventiGenerali
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
                CaricaEventi()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Eventi - Page_Load - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Eventi - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaEventi()
        Try
            connData.apri()
            lblTitolo.Text = "Eventi"
            Dim condizioneEventi As String = ""
            If Session.Item("ID_CAF") = "2" Then
                condizioneEventi = " AND TAB_EVENTI.COD IN ('F236','F239','F240','F241','F244','F245','F249','F250','F251','F252','F253') "
            Else
                condizioneEventi = " AND TAB_EVENTI.COD IN ('F239') "
            End If
            par.cmd.CommandText = "SELECT " _
                & " SEPA.OPERATORI.OPERATORE, " _
                & " TO_CHAR(TO_DATE(SUBSTR(EVENTI_SEGNALAZIONI.DATA_ORA,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS ""DATA EVENTO""," _
                & " SUBSTR(EVENTI_SEGNALAZIONI.DATA_ORA,9,2)||':'||SUBSTR(EVENTI_SEGNALAZIONI.DATA_ORA,11,2) AS ""ORA EVENTO""," _
                & " SISCOM_MI.TAB_EVENTI.DESCRIZIONE AS ""TIPO EVENTO"", " _
                & " (CASE WHEN (VALORE_NEW IS NOT NULL) THEN (EVENTI_SEGNALAZIONI.MOTIVAZIONE||'<br />'||'Valore attuale: '||VALORE_NEW||' - Valore precedente: '||VALORE_OLD) ELSE (EVENTI_SEGNALAZIONI.MOTIVAZIONE) END)  AS MOTIVAZIONE " _
                & " FROM SISCOM_MI.EVENTI_SEGNALAZIONI, SEPA.OPERATORI, SISCOM_MI.TAB_EVENTI " _
                & " WHERE EVENTI_SEGNALAZIONI.COD_EVENTO = TAB_EVENTI.COD " _
                & " AND EVENTI_SEGNALAZIONI.ID_OPERATORE = OPERATORI.ID " _
                & condizioneEventi _
                & " AND ID_SEGNALAZIONE IS NULL ORDER BY DATA_ORA DESC "
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
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Eventi - CaricaEventi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx")
    End Sub
End Class