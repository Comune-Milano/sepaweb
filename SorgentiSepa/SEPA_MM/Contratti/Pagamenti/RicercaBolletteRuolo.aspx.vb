
Partial Class Contratti_Pagamenti_RicercaBolletteRuolo
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            txtRiferimentoDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtRiferiemntoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPagamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPagamentoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


        End If
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        If Me.chkDettaglio.Checked = False Then
            If par.IfEmpty(Me.txtRiferimentoDal.Text, "null") <> "null" Then
                If par.AggiustaData(par.IfEmpty(Me.txtRiferimentoDal.Text, Format(Now, "dd/MM/yyyy"))) > par.AggiustaData(par.IfEmpty(Me.txtRiferiemntoAl.Text, Format(Now, "dd/MM/yyyy"))) Then
                    Response.Write("<script>alert('Errore nelle date!');</script>")
                    Exit Sub
                End If
            End If

            If par.AggiustaData(par.IfEmpty(Me.txtDataPagamento.Text, Format(Now, "dd/MM/yyyy"))) > par.AggiustaData(par.IfEmpty(Me.txtDataPagamentoAl.Text, Format(Now, "dd/MM/yyyy"))) Then
                Response.Write("<script>alert('Errore nelle date!');</script>")
                Exit Sub
            End If

            Response.Write("<script>window.open('RptPgRuoliGenerale.aspx?CodContratto=" & par.VaroleDaPassare(Me.txtCodContratto.Text) & "&NR=" & par.VaroleDaPassare(Me.txtNumRuolo.Text) & "&RIFDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtRiferimentoDal.Text)) _
                         & "&RIFAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtRiferiemntoAl.Text)) _
                         & "&S=" & Valore01(chkSgravio.Checked) _
                         & "&DPAG=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamento.Text)) & "&DPAGAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamentoAl.Text)) & "','RptPgExtraMav','');</script>")

        ElseIf Me.chkDettaglio.Checked = True Then

            If par.IfEmpty(Me.txtRiferimentoDal.Text, "null") <> "null" Then
                If par.AggiustaData(par.IfEmpty(Me.txtRiferimentoDal.Text, Format(Now, "dd/MM/yyyy"))) > par.AggiustaData(par.IfEmpty(Me.txtRiferiemntoAl.Text, Format(Now, "dd/MM/yyyy"))) Then
                    Response.Write("<script>alert('Errore nelle date!');</script>")
                    Exit Sub
                End If
            End If

            If par.AggiustaData(par.IfEmpty(Me.txtDataPagamento.Text, Format(Now, "dd/MM/yyyy"))) > par.AggiustaData(par.IfEmpty(Me.txtDataPagamentoAl.Text, Format(Now, "dd/MM/yyyy"))) Then
                Response.Write("<script>alert('Errore nelle date!');</script>")
                Exit Sub
            End If


            Response.Write("<script>window.open('RptPgRuoliDett.aspx?CodContratto=" & par.VaroleDaPassare(Me.txtCodContratto.Text) & "&RIFDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtRiferimentoDal.Text)) _
                    & "&RIFAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtRiferiemntoAl.Text)) _
                    & "&S=" & Valore01(chkSgravio.Checked) _
                    & "&DPAG=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamento.Text)) & "&DPAGAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamentoAl.Text)) & "','RptPgExtraMavD','');</script>")

        End If

    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function
End Class
