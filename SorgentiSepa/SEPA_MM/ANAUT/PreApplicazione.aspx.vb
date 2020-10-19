
Partial Class ANAUT_PreApplicazione
    Inherits PageSetIdMode

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            If Request.querystring("T") = "2" Then
                h1.value = "2"
                Label10.Text = "Stai per applicare l'Anagrafe utenza per i rapporti CONVOCATI e NON RISPONDENTI, con diffida o meno. Sei sicuro di voler procedere? Il canone non verrà aggiornato ora, ma utilizzando le apposite funzioni."
                chSospese.Visible = True
                ChDiffidati.Visible = True
            End If
            If Request.querystring("T") = "3" Then
                h1.value = "3"
                Label10.Text = "Stai per applicare l'Anagrafe utenza per i rapporti ABUSIVI. Sei sicuro di voler procedere? Il canone non verrà aggiornato ora, ma utilizzando le apposite funzioni."
                chSospese.Visible = False
                ChDiffidati.Visible = False

            End If
        End If
    End Sub


End Class
