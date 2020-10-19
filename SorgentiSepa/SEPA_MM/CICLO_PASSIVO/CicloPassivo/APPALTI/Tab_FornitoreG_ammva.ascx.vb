' TAB GENERALE DEL FORNITORE FISICO

Partial Class Tab_FornitoreG_ammva
    Inherits UserControlSetIdMode


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If
        End If
    End Sub

    Private Sub FrmSolaLettura()
        Try

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.lblErrore.Visible = True
            'lblErrore.Text = ex.Message
        End Try
    End Sub


    Protected Sub btncopia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncopia.Click
        Me.txtIndirizzoResidenzaA.Text = CType(Page.FindControl("Tab_FornitoreG_Legale").FindControl("txtIndirizzoResidenza"), TextBox).Text
        Me.txtCivicoResidenzaA.Text = CType(Page.FindControl("Tab_FornitoreG_Legale").FindControl("txtCivicoResidenza"), TextBox).Text
        Me.txtComuneResidenzaA.Text = CType(Page.FindControl("Tab_FornitoreG_Legale").FindControl("txtComuneResidenza"), TextBox).Text
        Me.txtProvinciaResidenzaA.Text = CType(Page.FindControl("Tab_FornitoreG_Legale").FindControl("txtProvinciaResidenza"), TextBox).Text
        Me.txtTelA.Text = CType(Page.FindControl("Tab_FornitoreG_Legale").FindControl("txtTel"), TextBox).Text
        Me.txtfaxA.Text = CType(Page.FindControl("Tab_FornitoreG_Legale").FindControl("txtfax"), TextBox).Text
        Me.txtCAPA.Text = CType(Page.FindControl("Tab_FornitoreG_Legale").FindControl("txtCAP"), TextBox).Text
        Me.DrLTipoIndA.SelectedValue = CType(Page.FindControl("Tab_FornitoreG_Legale").FindControl("DrLTipoInd"), DropDownList).SelectedValue
    End Sub
End Class
