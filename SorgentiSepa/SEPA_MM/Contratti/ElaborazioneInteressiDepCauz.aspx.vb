Imports System.IO

Partial Class Contratti_ElDepCauz
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Trim(Session.Item("OPERATORE"))) Then
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
            par.cmd.CommandText = "SELECT FROM_ID, TO_ID, TO_CHAR(TO_DATE(DATA_REST_INTERESSI,'YYYYMMDD'),'DD/MM/YYYY') AS DATA_REST_INTERESSI, TO_CHAR(TO_DATE(INIZIO,'YYYYMMDDHH24MISS'),'DD/MM/YYYY-HH24:MI:SS') AS INIZIO," _
                & "TO_CHAR(TO_DATE(FINE,'YYYYMMDDHH24MISS'),'DD/MM/YYYY-HH24:MI:SS') AS FINE,ERRORE," _
                & "DECODE(ESITO,0,'DA ELABORARE',1,'IN CORSO',2,'TERMINATA','') AS ESITO, NVL(ROUND(PARZIALE/TOTALE*100),0)||' %' AS PERCENTUALE " _
                & " FROM SISCOM_MI.PROCEDURE_REST_INT ORDER BY ID DESC "
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
                'Me.btnExpUltSim.Visible = False
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
        End Try
    End Sub

    Protected Sub ImageButton_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton.Click
        Response.Redirect("ElaborazioneInteressiDepCauz.aspx", False)
    End Sub

    Protected Sub DataGridElenco_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridElenco.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('idSel').value='" & e.Item.Cells(0).Text & "@" & e.Item.Cells(1).Text & "';document.getElementById('Perc').value='" & e.Item.Cells(6).Text & "';")
        End If

    End Sub


    Protected Sub btnReportDettagli_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnReportDettagli.Click
        If idSel.Value <> "0" And Perc.Value = "100 %" Then
            Response.Write("<script>window.open('CruscottoElabCauz.aspx?IDELAB=" & idSel.Value & "','CruscottoElabCauz','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no,top=0,left=0,width=800,height=600');</script>")
            idSel.Value = "0"
        Else
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Selezionare un record elaborato senza errori!')", True)
        End If
    End Sub

    Protected Sub btnReportTotali_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnReportTotali.Click
        If idSel.Value <> "0" And Perc.Value = "100 %" Then
            Response.Write("<script>window.open('CruscottoElabCauzT.aspx?IDELAB=" & idSel.Value & "','CruscottoElabCauzT','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no,top=0,left=0,width=800,height=600');</script>")
            idSel.Value = "0"
        Else
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Selezionare un record elaborato senza errori!')", True)
        End If
    End Sub
End Class
