' TAB GENERALE DEL FORNITORE FISICO

Partial Class Tab_FornitoreG_rappresentante
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

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

    'Protected Sub txtCFR_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCFR.TextChanged
    '    If par.ControllaCF(UCase(Me.txtCFR.Text)) = False Then
    '        Me.lblErroreCF.Visible = True
    '        Me.txtCFR.Text = ""
    '        Me.txtCFR.Focus()
    '        Response.Write("<SCRIPT>alert('Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!');</SCRIPT>")
    '    Else
    '        Me.lblErroreCF.Visible = False
    '        If par.ControllaCFNomeCognome(UCase(Me.txtCFR.Text), Me.txtCognome.Text, Me.txtNome.Text) = True Then

    '        Else
    '            Response.Write("<SCRIPT>alert('Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!');</SCRIPT>")
    '        End If
    '    End If
    'End Sub


    Protected Sub DrLTipoR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLTipoR.SelectedIndexChanged
        If Me.DrLTipoR.Items.FindByText("PROCURATORE LEG.").Selected Then
            Me.lblprocura.Visible = True
            Me.lbldataprocura.Visible = True
            Me.txtnumprocura.Visible = True
            Me.txtdataprocura.Visible = True
            Me.controllodata.Enabled = True
        Else
            Me.txtnumprocura.Text = ""
            Me.txtdataprocura.Text = ""
            Me.lblprocura.Visible = False
            Me.lbldataprocura.Visible = False
            Me.txtnumprocura.Visible = False
            Me.txtdataprocura.Visible = False
            Me.controllodata.Enabled = False
        End If
    End Sub
End Class
