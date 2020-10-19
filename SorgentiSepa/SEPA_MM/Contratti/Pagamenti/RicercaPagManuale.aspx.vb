
Partial Class Contratti_Pagamenti_RicercaPagManuale
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCf.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCodContratto.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCodUi.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtRagSociale.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtPiva.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                If Not IsNothing(Request.QueryString("T")) Then
                    tipoPagamanto.Value = Request.QueryString("T")
                    If tipoPagamanto.Value = "R" Then
                        lblTitolo.Text = "RUOLI"
                    ElseIf tipoPagamanto.Value = "I" Then
                        lblTitolo.Text = "INGIUNZIONI"
                    End If
                End If

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim queryStr As String = ""
        If tipoPagamanto.Value <> "" Then
            queryStr = "T=" & tipoPagamanto.Value & "&"
        End If
        Response.Redirect("RisultatiPagManuale.aspx?COGNOME=" & par.VaroleDaPassare(Me.txtCognome.Text.ToUpper) _
                          & "&NOME=" & par.VaroleDaPassare(Me.txtNome.Text.ToUpper) _
                          & "&CF=" & par.VaroleDaPassare(Me.txtCf.Text.ToUpper) _
                          & "&RS=" & par.VaroleDaPassare(Me.txtRagSociale.Text.ToUpper) _
                          & "&PIVA=" & par.VaroleDaPassare(Me.txtPiva.Text.ToUpper) _
                          & "&CODCONT=" & par.VaroleDaPassare(Me.txtCodContratto.Text.ToUpper) _
                          & "&" & queryStr & "CODUI=" & par.VaroleDaPassare(Me.txtCodUi.Text.ToUpper))

    End Sub
End Class
