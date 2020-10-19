
Partial Class Contabilita_Report_ReportSitIncassiRuoli
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 796px; height: 540px;" _
              & "top: 0px; left: 0px;background-color: #eeeeee;background-image: url('../../NuoveImm/SfondoMascheraContratti2.jpg');z-index:1000;"">" _
              & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
              & "margin-top: -48px; background-image: url('../../NuoveImm/sfondo.png');"">" _
              & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
              & "<img src=""../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
              & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
              & "</td></tr></table></div></div>"
        Response.Write(Loading)
        If Not IsPostBack Then
            Response.Flush()
            par.caricaComboBox("select id, UPPER(descrizione) as descrizione from siscom_mi.TIPO_PAG_RUOLO order by descrizione asc", DropDownListTipoIncasso, "id", "descrizione")

        End If
        ImpostaJavaScriptFunction()
    End Sub

    Private Sub ImpostaJavaScriptFunction()
        TextBoxDataPagamentoDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxDataPagamentoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxRiferimentoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxRiferimentoDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub


    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "red", "location.replace('../pagina_home.aspx')", True)
    End Sub

    Protected Sub ImageButtonAvanti_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAvanti.Click
        Dim parametriDaPassare As String = ""
        parametriDaPassare &= "?DataPagamentoDal=" & par.FormatoDataDB(TextBoxDataPagamentoDal.Text)
        parametriDaPassare &= "&DataPagamentoAl=" & par.FormatoDataDB(TextBoxDataPagamentoAl.Text)
        parametriDaPassare &= "&RiferimentoDal=" & par.FormatoDataDB(TextBoxRiferimentoDal.Text)
        parametriDaPassare &= "&RiferimentoAl=" & par.FormatoDataDB(TextBoxRiferimentoAl.Text)
        parametriDaPassare &= "&NumeroAssegno=" & TextBoxNumeroAssegno.Text
        parametriDaPassare &= "&CodContratto=" & txtCodContratto.Text.ToUpper
        parametriDaPassare &= "&TipologiaIncasso=" & DropDownListTipoIncasso.SelectedValue

        Dim parametriRicercaDataPagamento As String = ""
        If TextBoxDataPagamentoAl.Text <> "" And TextBoxDataPagamentoDal.Text = "" Then
            parametriRicercaDataPagamento = "Data Pagamento al: " & TextBoxDataPagamentoAl.Text
        ElseIf TextBoxDataPagamentoAl.Text <> "" And TextBoxDataPagamentoDal.Text <> "" Then
            parametriRicercaDataPagamento = "Data Pagamento dal: " & TextBoxDataPagamentoDal.Text & " al: " & TextBoxDataPagamentoAl.Text
        ElseIf TextBoxDataPagamentoAl.Text = "" And TextBoxDataPagamentoDal.Text = "" Then
            parametriRicercaDataPagamento = ""
        ElseIf TextBoxDataPagamentoAl.Text = "" And TextBoxDataPagamentoDal.Text <> "" Then
            parametriRicercaDataPagamento = "Data Pagamento dal: " & TextBoxDataPagamentoDal.Text
        End If

        Dim parametriRicercaRiferimento As String = ""
        If TextBoxRiferimentoAl.Text <> "" And TextBoxRiferimentoDal.Text = "" Then
            parametriRicercaRiferimento = "Data riferimento al: " & TextBoxRiferimentoAl.Text
        ElseIf TextBoxRiferimentoAl.Text <> "" And TextBoxRiferimentoDal.Text <> "" Then
            parametriRicercaRiferimento = "Data riferimento dal: " & TextBoxRiferimentoDal.Text & " al: " & TextBoxRiferimentoAl.Text
        ElseIf TextBoxRiferimentoAl.Text = "" And TextBoxRiferimentoDal.Text = "" Then
            parametriRicercaRiferimento = ""
        ElseIf TextBoxRiferimentoAl.Text = "" And TextBoxRiferimentoDal.Text <> "" Then
            parametriRicercaRiferimento = "Data riferimento dal: " & TextBoxRiferimentoDal.Text
        End If

        Dim parametriDate As String = ""
        parametriDate = parametriRicercaDataPagamento
        If parametriDate <> "" And parametriRicercaRiferimento <> "" Then
            parametriDate &= ", " & parametriRicercaRiferimento
        Else
            parametriDate &= parametriRicercaRiferimento
        End If

        Dim parametriRicercaNumeroAssegno As String = ""
        If TextBoxNumeroAssegno.Text <> "" Then
            parametriRicercaNumeroAssegno = "Num. assegno: " & TextBoxNumeroAssegno.Text
        End If

        Dim parametriRicercaTipoIncasso As String = ""
        If DropDownListTipoIncasso.SelectedValue <> -1 Then
            parametriRicercaTipoIncasso = "Tipo incasso: " & DropDownListTipoIncasso.SelectedItem.Text
        End If

        Dim parametriRicercaCodRU As String = ""
        If txtCodContratto.Text <> "" Then
            parametriRicercaCodRU = "Cod. contratto: " & txtCodContratto.Text.ToUpper
        End If

        Session.Add("filtriRicerca", parametriRicercaCodRU & vbCrLf & parametriDate & vbCrLf & parametriRicercaTipoIncasso & vbCrLf _
                   & parametriRicercaNumeroAssegno)

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "window.open('RisultatiRptIncassiRuoli.aspx" & parametriDaPassare & "','_blank','');", True)

    End Sub
End Class
