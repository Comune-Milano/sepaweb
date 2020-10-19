Namespace CM

Partial  Class Dom_Dichiara
    Inherits UserControlSetIdMode

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
    End Sub

        Public Sub DisattivaTutto()
            CF1.Enabled = False
            CF2.Enabled = False
            CF3.Enabled = False
            CF4.Enabled = False
            cmbPresentaD.Enabled = False
            cfProfugo.Enabled = False

        End Sub

    End Class

End Namespace
