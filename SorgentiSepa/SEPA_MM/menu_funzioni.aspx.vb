Namespace CM

Partial Class menu_funzioni
        Inherits PageSetIdMode
        Dim par As New CM.Global

#Region " Codice generato da Progettazione Web Form "

    'Chiamata richiesta da Progettazione Web Form.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: questa chiamata al metodo è richiesta da Progettazione Web Form.
        'Non modificarla nell'editor del codice.
        InitializeComponent()
    End Sub

#End Region

    Public Function Spegni()
        avviso.Visible = False
    End Function

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
            If Session.Item("OPERATORE") <> "" Then
                lblOperatore.Text = Session.Item("OPERATORE")
                If Session.Item("PW") = "SEPA" Then
                    avviso.Visible = True
                Else
                    avviso.Visible = False
                End If
                LinkButton1.Attributes.Add("OnClick", "javascript:parent.main.Uscita=1;")
            Else
                Response.Write("<script>top.location.href=""Portale.aspx""</script>")
            End If
        End Sub

        Private Sub avviso_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles avviso.Click

            Response.Write("<script>window.open('avviso.aspx','Avviso','top=0,left=0,width=400,height=150');</script>")

        End Sub


        Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
            If Session.Item("LAVORAZIONE") = "1" Then
                Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
                Exit Sub
            End If
            Response.Write("<script>top.location.href=""Chiusura.htm""</script>")
        End Sub
    End Class

End Namespace
