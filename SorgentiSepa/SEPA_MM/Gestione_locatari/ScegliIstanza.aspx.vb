
Partial Class Gestione_locatari_ScegliIstanza
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            idcont.Value = Request.QueryString("ID")
            CodContratto.Value = Request.QueryString("COD")
            id_intest.Value = Request.QueryString("INTEST")
            par.caricaComboTelerik("select * from t_motivo_domanda_vsa where fl_nuova_normativa=1 order by id asc", cmbTipoProcesso, "id", "descrizione", False)

        End If
    End Sub

    Protected Sub cmbQuadroNormativo_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbQuadroNormativo.SelectedIndexChanged
        If cmbQuadroNormativo.SelectedValue = "2" Then
            lblTipoProcesso.Visible = True
            cmbTipoProcesso.Visible = True
        Else
            lblTipoProcesso.Visible = False
            cmbTipoProcesso.Visible = False
        End If
    End Sub
End Class
