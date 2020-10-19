
Partial Class MANUTENZIONI_SceltaFornitore
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Add("CONTRATTOAPERTO", "0")

        ' x.Value = UCase(Request.QueryString("X")) 'serve per sapere se è aperto come finestra di dialogo

        If x.Value = "1" Then
            Response.Write("<script language='javascript'> { self.close(); }</script>")
        Else
            Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
          
            If Not IsPostBack Then

                x.Value = UCase(Request.QueryString("X")) 'serve per sapere se è aperto come finestra di dialogo

                If x.Value = "1" Then
                    tipo = "_self"

                Else
                    tipo = ""

                End If

                Session.Add("CONTRATTOAPERTO", "1")
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try



            lblErrore.Visible = False

            If prosegui.Value = "1" Then

                If chFisica.Checked = False And chGiuridica.Checked = False Then

                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Selezionare la tipologia di contraente"

                    Exit Sub

                End If

                Session.Item("ID") = "0"

                If chFisica.Checked = True Then
                    If x.Value = "1" Then
                        Response.Redirect("FornitoreF.aspx?X=1")
                        'Response.Write("<script>location.href='FornitoreF.aspx?X=1';</script>")
                    Else
                        Response.Redirect("FornitoreF.aspx?")
                    End If
                Else
                    If x.Value = "1" Then
                        Response.Redirect("FornitoreG.aspx?X=1")
                    Else
                        Response.Redirect("FornitoreG.aspx?")
                    End If
                End If

            End If



        Catch ex As Exception

            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Public Property tipo() As String
        Get
            If Not (ViewState("par_tipo") Is Nothing) Then
                Return CStr(ViewState("par_tipo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_tipo") = value
        End Set

    End Property
End Class
