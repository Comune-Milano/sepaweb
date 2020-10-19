
Partial Class Contabilita_DateDettBoll
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        TxtDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")

    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        '******controllo che la data iniziale sia avvalorata e che se avvalorata anche quella finale questa non sia minore di quella iniziale altimenti
        '******l'estratto conto generato non averebbe alcun senso!

        If par.IfEmpty(Me.TxtDal.Text, "Null") <> "Null" And par.IfEmpty(Me.TxtAl.Text, "Null") <> "Null" Then

            If par.IfEmpty(par.AggiustaData(Me.TxtAl.Text), "Null") < par.IfEmpty(par.AggiustaData(Me.TxtDal.Text), "Null") Then

                Response.Write("<script>alert('Definire correttamente l\'intervallo di tempo!')</script>")
                Exit Sub

            End If


        End If
        Response.Write("<script>window.open('DettaglioBolletta.aspx?DAL=" & par.AggiustaData(Me.TxtDal.Text) & "&AL=" & par.AggiustaData(Me.TxtAl.Text) & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "','Dettagli', '');</script>")

        'If par.IfEmpty(Me.TxtDal.Text, "Null") <> "Null" Then
        '    If par.IfEmpty(Me.TxtAl.Text, "Null") <> "Null" Then
        '        If par.AggiustaData(Me.TxtAl.Text) < par.AggiustaData(Me.TxtDal.Text) Then
        '            Response.Write("<script>alert('Definire correttamente l\'intervallo di tempo!')</script>")
        '            Exit Sub
        '        Else
        '            Response.Write("<script>window.open('PagamentiPervenuti.aspx?DAL=" & par.AggiustaData(Me.TxtDal.Text) & "&AL=" & par.AggiustaData(Me.TxtAl.Text) & "&IDANA=" & Request.QueryString("IDANA") & "','Pagamenti', '');</script>")
        '        End If
        '    End If

        '    Response.Write("<script>window.open('PagamentiPervenuti.aspx?DAL=" & par.AggiustaData(Me.TxtDal.Text) & "&AL=" & par.AggiustaData(Me.TxtAl.Text) & "&IDANA=" & Request.QueryString("IDANA") & "','Pagamenti', '');</script>")
        'Else
        '    Response.Write("<script>alert('Definire almeno la data iniziale dell\'intervallo di tempo!')</script>")
        '    Exit Sub
        'End If
    End Sub
End Class
