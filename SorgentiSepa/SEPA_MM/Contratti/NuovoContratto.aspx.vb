
Partial Class Contratti_NuovoERP
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Response.Write("<script>parent.main.location.replace('RisultatiUnitaAssegnate.aspx?OFF=" & par.VaroleDaPassare(txtofferta.Text) & "&NOME=" & par.VaroleDaPassare(Me.txtNome.Text.ToUpper) & "&COGNOME=" & par.VaroleDaPassare(Me.txtCognome.Text.ToUpper) & "&CF=" & par.VaroleDaPassare(Me.txtCF.Text.ToUpper) & "&TIPO=" & Request.QueryString("TIPO") & "&ORIG=" & Request.QueryString("ORIG") & "');</script>")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Select Case Request.QueryString("TIPO")
                Case "1"
                    If Request.QueryString("ORIG") = "1" Then
                        Label3.Text = "Cambi Emergenza (ART.22 C.10 RR 1/2004)"
                    Else
                    Label3.Text = "ERP"
                    End If
                Case "2"
                    Label3.Text = "CAMBI"
                Case "3"
                    Label3.Text = "USI DIVERSI"
                    'Me.lblNome.Visible = False
                    'Me.txtNome.Visible = False
                    Me.lblCognome.Text = "Cognome/R. Sociale"
                    Me.lblCfPiva.Text = "Cof. Fis./Partita Iva"
                Case "4"
                    Label3.Text = "CAMBI CONSENSUALI"
                Case "5"
                    Label3.Text = "392/78"
                Case "6"
                    Label3.Text = "431/98"
                Case "7"
                    Label3.Text = "Abusivi"
                Case "10"
                    Label3.Text = "Forze dell'Ordine"
                Case "11"
                    Label3.Text = "Canone Convenzionato"
            End Select
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
