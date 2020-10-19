
Partial Class TabGeneraleA
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub


    Protected Sub cmbVentilazione_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVentilazione.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub

    Protected Sub cmbVentilazione_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVentilazione.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub

    Protected Sub cmbAreazione_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAreazione.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub

    Protected Sub cmbAreazione_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAreazione.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub



 

End Class
