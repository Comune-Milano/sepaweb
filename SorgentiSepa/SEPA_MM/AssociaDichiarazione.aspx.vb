Namespace CM

Partial Class AssociaDichiarazione
    Inherits PageSetIdMode
    Dim clMioPar As New [Global]()

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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("AccessoNegato.htm", True)
                Exit Sub
            End If
            If Not IsPostBack Then
                txtPG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtPG.Text = Session.Item("ID_NUOVA_DIC")
            End If

        End Sub



        Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button1.Click
            Response.Redirect("RisultatoDichiarazioni.aspx?PG=" & clMioPar.VaroleDaPassare(txtPG.Text) & "&CF=" & clMioPar.VaroleDaPassare(txtCF.Text))
        End Sub


    End Class

End Namespace
