
Partial Class Contratti_SelezionaTipoContratto
    Inherits PageSetIdMode

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        If RadioButton1.Checked = True Then
            Response.Write("<script>top.location.href='SelezionaUnita.aspx';</script>")
        End If
        If RadioButton2.Checked = True Then
            Response.Write("<script>top.location.href='SelezionaUnita392.aspx';</script>")
        End If
        If RadioButton3.Checked = True Then
            Response.Write("<script>top.location.href='SelezionaUnita431.aspx';</script>")
        End If
        If RadioButton4.Checked = True Then
            Response.Write("<script>top.location.href='SelezionaUnitaOA.aspx';</script>")
        End If
        If RadioButton5.Checked = True Then
            Response.Write("<script>top.location.href='SelezionaUnitaFO.aspx';</script>")
        End If
        If RadioButton6.Checked = True Then
            Response.Write("<script>top.location.href='SelezionaUnitaArt15.aspx';</script>")
        End If
        If RadioButton7.Checked = True Then
            Response.Write("<script>top.location.href='DichiarazioneCS.aspx';</script>")
        End If
        If RadioButton8.Checked = True Then
            Response.Write("<script>top.location.href='SelezionaUnitaCON.aspx';</script>")
        End If
        If RadioButton9.Checked = True Then
            Response.Write("<script>top.location.href='SelezionaUnitaFERP.aspx';</script>")
        End If
        If RadioButton10.Checked = True Then
            Response.Write("<script>top.location.href='SelezionaUnitaConc.aspx';</script>")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        RadioButton9.Visible = False
        If Not IsPostBack Then
            If Session.Item("LIVELLO") = "1" Then
                RadioButton1.Checked = True
            Else
                If Session.Item("ABB_OA") = "1" Then
                    RadioButton4.Visible = True
                    RadioButton4.Checked = True
                Else
                    RadioButton4.Visible = False
                End If

                If Session.Item("ABB_392") = "1" Then
                    RadioButton2.Visible = True
                    RadioButton2.Checked = True
                Else
                    RadioButton2.Visible = False
                End If

                If Session.Item("ABB_UD") = "1" Then
                    RadioButton1.Visible = True
                    RadioButton1.Checked = True
                Else
                    RadioButton1.Visible = False
                End If

                If Session.Item("ABB_431") = "1" Then
                    RadioButton3.Visible = True
                    RadioButton3.Checked = True
                    RadioButton6.Visible = True

                Else
                    RadioButton3.Visible = False
                    RadioButton6.Visible = False
                End If

                If Session.Item("ABB_FO") = "1" Then
                    RadioButton5.Visible = True
                    RadioButton5.Checked = True
                Else
                    RadioButton5.Visible = False
                End If

                If Session.Item("ABB_CS") = "1" Then
                    RadioButton7.Visible = True
                    RadioButton7.Checked = True
                Else
                    RadioButton7.Visible = False
                End If

                If Session.Item("ABB_CONVONEZIONATO") = "1" Then
                    RadioButton8.Visible = True
                    RadioButton8.Checked = True
                Else
                    RadioButton8.Visible = False
                End If


                'If S <> "" Then S = "(" & S & ") AND "
            End If
        End If
    End Sub
End Class
