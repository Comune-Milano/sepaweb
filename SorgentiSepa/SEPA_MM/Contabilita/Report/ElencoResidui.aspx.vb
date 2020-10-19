
Partial Class Contabilita_Report_ElencoResidui
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If controlloProfilo() Then
            If Not IsPostBack Then
                apriDettagli()
            End If
        End If
    End Sub
    Private Function controlloProfilo() As Boolean
        'CONTROLLO DELLA SESSIONE OPERATORE E DELL'ABILITAZIONE AI PREVENTVI
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>alert('Accesso negato o sessione scaduta! E\' necessario rieseguire il login.');</script>")
            Return False
        End If
        connData = New CM.datiConnessione(par, False, False)
        Return True
    End Function
    Private Sub apriDettagli()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT id,NOME_OPERATORE AS OPERATORE,TO_CHAR(TO_DATE(DATA_ORA_INIZIO,'YYYYMMDDHH24MISS'),'DD/MM/YYYY-HH24:MI:SS') AS INIZIO," _
                & "TO_CHAR(TO_DATE(DATA_ORA_FINE,'YYYYMMDDHH24MISS'),'DD/MM/YYYY-HH24:MI:SS') AS FINE,ERRORE," _
                & "DECODE(ESITO,0,'IN CORSO',1,'TERMINATA',2,'INTERROTTA PER ERRORE',5,'NESSUN DATO','') AS ESITO, " _
                & "(CASE WHEN ESITO=5 THEN NULL ELSE '<a href=""javascript:window.open(''DettagliResidui.aspx?id='||id||''',''_blank'',''resizable=yes,height=800,width=1000,top=0,left=100,scrollbars=yes'');void(0);""><img src=""../../NuoveImm/search-icon.png"" style=""border:none;"" /> " _
                 & "</a>' END ) AS DETTAGLIO, PARAMETRI_RICERCA, PARZIALE,TOTALE " _
                & " FROM SISCOM_MI.PROCEDURE_RESIDUI ORDER BY DATA_ORA_INIZIO DESC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                DataGridElenco.DataSource = dt
                DataGridElenco.DataBind()
                LabelRis.Text = ""
            Else
                LabelRis.Text = "Nessuna elaborazione trovata"
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
        End Try
    End Sub

    Protected Sub DataGridElenco_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridElenco.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            If e.Item.Cells(4).Text <> "NESSUN DATO" Then
                If e.Item.Cells(4).Text <> "TERMINATA" Then
                    e.Item.Cells(6).Text = ""
                    If e.Item.Cells(4).Text = "IN CORSO" Then
                        If e.Item.Cells(8).Text = "&nbsp;" Then
                            e.Item.Cells(4).Text &= " (0%)"
                        ElseIf e.Item.Cells(8).Text = "0" Then
                            e.Item.Cells(4).Text &= " (100%)"
                        Else
                            e.Item.Cells(4).Text &= " (" & CStr(Math.Round(CDec(e.Item.Cells(7).Text) / CDec(e.Item.Cells(8).Text) * 99)) & "%)"
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub ImageButton_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton.Click
        Response.Redirect("ElencoResidui.aspx", False)
    End Sub
End Class
