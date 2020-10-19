' TAB GENERALE DELL'IMPIANTO IDRICO

Partial Class Tab_Idrico_Generale
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If
        End If

    End Sub

    Protected Sub CheckBoxEdifici_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxEdifici.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub

    Private Sub FrmSolaLettura()
        Try

            Me.CheckBoxEdifici.Enabled = False

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

    Protected Sub CheckBoxEdifici_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxEdifici.SelectedIndexChanged
        Dim i, j As Integer
        Dim SommaUI As Integer = 0

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Dim lstEdificiScale As System.Collections.Generic.List(Of Epifani.EdificiScale)
        lstEdificiScale = CType(HttpContext.Current.Session.Item("LSTEDIFICISCALE"), System.Collections.Generic.List(Of Epifani.EdificiScale))


        For i = 0 To CheckBoxEdifici.Items.Count - 1
            If CheckBoxEdifici.Items(i).Selected = True Then

                '************
                For j = 0 To lstEdificiScale.Count - 1

                    If lstEdificiScale(j).ID_SCALA = CheckBoxEdifici.Items(i).Value Then
                        SommaUI = SommaUI + lstEdificiScale(j).TOT_UNITA
                    End If

                Next j
                '********************************
            End If
        Next i

        txtTotUI.Text = SommaUI       

    End Sub

End Class
