
Partial Class Contratti_Report_DistintaRateEmesse
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

        End If
        txtDataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        txtDataDal0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try

            'If par.IfEmpty(par.AggiustaData(Me.txtDataDal.Text), "Null") = "Null" Or par.IfEmpty(par.AggiustaData(Me.txtDataAl.Text), "Null") = "Null" Then
            '    Response.Write("<script>alert('E\' necessario specificare entrabe le date!')</script>")
            '    Exit Sub
            'End If
            If par.IfEmpty(par.AggiustaData(Me.txtDataAl.Text), "Null") <> "Null" And par.IfEmpty(par.AggiustaData(Me.txtDataDal.Text), "Null") <> "Null" Then
                If par.AggiustaData(Me.txtDataDal.Text) > par.AggiustaData(Me.txtDataAl.Text) Then
                    Response.Write("<script>alert('Intervallo non valido (DATA EMISSIONE)!')</script>")
                    'Me.txtDataAl.Text = ""
                    'Me.txtDataDal.Text = ""
                    Exit Sub
                End If
            End If

            If par.IfEmpty(par.AggiustaData(Me.txtDataAl0.Text), "Null") <> "Null" And par.IfEmpty(par.AggiustaData(Me.txtDataDal0.Text), "Null") <> "Null" Then
                If par.AggiustaData(Me.txtDataDal0.Text) > par.AggiustaData(Me.txtDataAl0.Text) Then
                    Response.Write("<script>alert('Intervallo non valido (DATA PERIODO RIFERIMENTO)!')</script>")
                    'Me.txtDataAl.Text = ""
                    'Me.txtDataDal.Text = ""
                    Exit Sub
                End If
            End If



            Response.Write("<script>window.open('StampaDistintaRate.aspx?T=" & Me.RdbTipologia.SelectedValue & "&DAL1=" & par.AggiustaData(Me.txtDataDal0.Text) & "&AL1=" & par.AggiustaData(Me.txtDataAl0.Text) & "&DAL=" & par.AggiustaData(Me.txtDataDal.Text) & "&AL=" & par.AggiustaData(Me.txtDataAl.Text) & "');</script>")


        Catch ex As Exception
            Me.lblErrore.Visible = True
            'par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Contabilita/pagina_home.aspx""</script>")

    End Sub
End Class
