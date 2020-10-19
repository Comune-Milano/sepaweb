
Partial Class Contabilita_RicMorosita
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
        txtDataDalRif.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAlRif.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try

            'If par.IfEmpty(par.AggiustaData(Me.txtDataDal.Text), "Null") = "Null" Or par.IfEmpty(par.AggiustaData(Me.txtDataAl.Text), "Null") = "Null" Then
            '    Response.Write("<script>alert('E\' necessario specificare entrabe le date!')</script>")
            '    Exit Sub
            'End If
            If Not String.IsNullOrEmpty(par.AggiustaData(Me.txtDataAl.Text)) And Not String.IsNullOrEmpty(par.AggiustaData(Me.txtDataDal.Text)) Then
                If par.AggiustaData(Me.txtDataDal.Text) > par.AggiustaData(Me.txtDataAl.Text) Then
                    Response.Write("<script>alert('Intervallo non valido (DATA EMISSIONE)!')</script>")
                    'Me.txtDataAl.Text = ""
                    'Me.txtDataDal.Text = ""
                    Exit Sub
                End If
            End If

            If Not String.IsNullOrEmpty(par.AggiustaData(Me.txtDataAlRif.Text)) And Not String.IsNullOrEmpty(par.AggiustaData(Me.txtDataDalRif.Text)) Then
                If par.AggiustaData(Me.txtDataDalRif.Text) > par.AggiustaData(Me.txtDataAlRif.Text) Then
                    Response.Write("<script>alert('Intervallo non valido (DATA PERIODO RIFERIMENTO)!')</script>")
                    Exit Sub
                End If
            End If

            Response.Write("<script>window.open('Morosita.aspx?DAL=" & par.AggiustaData(Me.txtDataDal.Text) & "&AL=" & par.AggiustaData(Me.txtDataAl.Text) & "&DALRIF=" & par.AggiustaData(Me.txtDataDalRif.Text) & "&ALRIF=" & par.AggiustaData(Me.txtDataAlRif.Text) & "');</script>")


        Catch ex As Exception
            Me.lblErrore.Visible = True
            'par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
