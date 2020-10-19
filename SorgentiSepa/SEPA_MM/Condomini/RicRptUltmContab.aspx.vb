
Partial Class Condomini_RicRptUltmContab
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            End If
            If Not IsPostBack Then
                Me.rdbLstScelta.SelectedValue = 0
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        Response.Write("<script>parent.main.location.href=""pagina_home.aspx""</script>")
    End Sub
    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Select Case rdbLstScelta.SelectedValue
            Case 0
                Response.Write("<script>window.open('RptUltmContabilitaStat.aspx?X=0','','height=10,width=10');</script>")
                '    'Response.Write("<script>window.open('RptUltmContabilitaStat.aspx','ReportUltmContabilitaStat','scrollbars=yes,resizable=yes');</script>")
            Case 1
                Response.Write("<script>window.open('RptScadenzario.aspx','','height=20,width=20');</script>")
                'Response.Write("<script>window.open('RptUltmContabilitaImporti.aspx?X=0','','height=20,width=20');</script>")
                '    'Response.Write("<script>window.open('RptUltmContabilitaImporti.aspx','RptUltmContabilitaImporti','scrollbars=yes,resizable=yes');</script>")
            Case 2
                If Controllo() = True Then
                    Response.Write("<script>window.open('RptUltmContabilitaStat.aspx?X=1&Dal=" & txtdatadal.Text & "&Al=" & txtdataal.Text & "','','height=20,width=20');</script>")
                Else
                    Response.Write("<script>alert('L\'anno di fine gestione deve essere maggiore di quello di inizio. Riprovare!');</script>")
                End If
            Case 3
                If Controllo() = True Then
                    Response.Write("<script>window.open('RptUltmContabilitaImporti.aspx?X=1&Dal=" & txtdatadal.Text & "&Al=" & txtdataal.Text & "','','height=20,width=20');</script>")
                Else
                    Response.Write("<script>alert('L\'anno di fine gestione deve essere maggiore di quello di inizio. Riprovare!');</script>")
                End If
            Case 4
                Response.Write("<script>parent.main.location.replace('RicrContNonPagate.aspx');</script>")
        End Select
    End Sub
    Private Function Controllo() As Boolean
        Controllo = True
        If Not String.IsNullOrEmpty(txtdatadal.Text) And Not String.IsNullOrEmpty(txtdataal.Text) Then
            If txtdatadal.Text > txtdataal.Text Then
                Controllo = False
                Exit Function
            End If
        End If
        Return Controllo
    End Function
    Protected Sub rdbLstScelta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbLstScelta.SelectedIndexChanged
        Select Case rdbLstScelta.SelectedValue
            Case 2
                Me.lbldataal.Visible = True
                Me.lbldatadal.Visible = True
                Me.txtdataal.Visible = True
                Me.txtdatadal.Visible = True
            Case 3
                Me.lbldataal.Visible = True
                Me.lbldatadal.Visible = True
                Me.txtdataal.Visible = True
                Me.txtdatadal.Visible = True
            Case Else
                Me.lbldataal.Visible = False
                Me.lbldatadal.Visible = False
                Me.txtdataal.Visible = False
                Me.txtdatadal.Visible = False
        End Select
    End Sub
End Class
