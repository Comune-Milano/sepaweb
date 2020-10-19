' TAB GENERALE DELL'IMPIANTO ANTINCENDIO

Partial Class Tab_Antincendio_Generale
    Inherits UserControlSetIdMode



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If
        End If
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

    Protected Sub cmbBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBox.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub


    Protected Sub cmbBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBox.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub

    Protected Sub CheckBoxEdifici_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxEdifici.SelectedIndexChanged
        Dim i, j As Integer
        Dim SommaUI As Integer = 0

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Dim lstEdifici As System.Collections.Generic.List(Of Epifani.Edifici)
        lstEdifici = CType(HttpContext.Current.Session.Item("LSTEDIFICI"), System.Collections.Generic.List(Of Epifani.Edifici))


        For i = 0 To CheckBoxEdifici.Items.Count - 1
            If CheckBoxEdifici.Items(i).Selected = True Then

                '************
                For j = 0 To lstEdifici.Count - 1

                    If lstEdifici(j).ID = CheckBoxEdifici.Items(i).Value Then
                        SommaUI = SommaUI + lstEdifici(j).TOT_UNITA
                    End If

                Next j
                '********************************
            End If
        Next i

        txtTotUI.Text = SommaUI

    End Sub

    Protected Sub CheckBoxEdifici_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxEdifici.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub


End Class
