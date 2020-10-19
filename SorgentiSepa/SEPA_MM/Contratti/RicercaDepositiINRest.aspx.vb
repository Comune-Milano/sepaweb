
Partial Class Contratti_RicercaDepositiINRest
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            txtCodContr.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtDataEventoAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataEventoDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Caricacombo()
        End If
    End Sub

    Private Function Caricacombo()
        Try
            par.caricaCheckBoxList("SELECT  * FROM SISCOM_MI.TAB_MOD_RESTITUZIONE ORDER BY DESCRIZIONE ASC", chTipoPagamento, "ID", "DESCRIZIONE")

            par.caricaComboBox("SELECT  * FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC", cmbStruttura, "ID", "NOME", True)

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim i As Long = 0
        Dim tipoP As String = ""

        For i = 0 To chTipoPagamento.Items.Count - 1
            If chTipoPagamento.Items(i).Selected = True Then
                tipoP = tipoP & chTipoPagamento.Items(i).Value & ","
            End If
        Next
        If tipoP <> "" Then tipoP = Mid(tipoP, 1, Len(tipoP) - 1)

        Dim DatiDaPassare As String = "CDC=" & txtCodContr.Text & "&DCPD=" & par.AggiustaData(txtDataEventoDAL.Text) & "&DCPA=" & par.AggiustaData(txtDataEventoAL.Text) & "&TP=" & tipoP & "&ST=" & cmbStruttura.SelectedItem.Value

        Response.Write("<script>location.replace('RisultatiDepINRest.aspx?" & DatiDaPassare & "');</script>")
    End Sub
End Class
