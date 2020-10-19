
Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_ImportiNP
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If durata.Value <> "" Then
            DirectCast(Me.Page.FindControl("txtdurata"), TextBox).Text = durata.Value
        End If

        If Not IsPostBack Then
            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Then
                FrmSolaLettura()
            End If
        End If


    End Sub
    Private Sub FrmSolaLettura()
        Try
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabImportiNP"
            'Me.lblErrore.Visible = True
            'lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub txtonericonsumo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtonericonsumo.TextChanged
        CType(Me.Page.FindControl("Tab_VociNPl1"), Object).CalcolaImpContrattuale()
    End Sub

    Protected Sub btnStampaCDP_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnStampaCDP.Click
        CType(Me.Page, Object).StampaAnticipo()
    End Sub

    Protected Sub btnPrintPagParz_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPrintPagParz.Click
        CType(Me.Page, Object).PdfPagamento(DirectCast(Me.Page.FindControl("idPagRitLegge"), HiddenField).Value)

    End Sub
End Class
