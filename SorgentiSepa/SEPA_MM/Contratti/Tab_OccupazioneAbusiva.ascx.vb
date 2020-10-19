
Imports Telerik.Web.UI

Partial Class Contratti_Tab_OccupazioneAbusiva
    Inherits System.Web.UI.UserControl
    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set
    End Property
    Private Sub Contratti_Tab_OccupazioneAbusiva_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        cmbPresenzaDebito.Attributes.Add("onclick", "javascript:document.getElementById('USCITA').value='1';")
        cmbEstinzioneDebito.Attributes.Add("onclick", "javascript:document.getElementById('USCITA').value='1';")
    End Sub

    Private Sub cmbPresenzaDebito_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbPresenzaDebito.SelectedIndexChanged
        VerificaPresenzaDebito()
    End Sub
    Public Sub VerificaPresenzaDebito()
        If cmbPresenzaDebito.SelectedValue = "1" Then
            lblTipoDebito.Visible = True
            cmbTipoDebito.Visible = True
            lblEstinzioneDebito.Visible = True
            cmbEstinzioneDebito.Visible = True
        Else
            lblTipoDebito.Visible = False
            cmbTipoDebito.ClearCheckedItems()
            cmbTipoDebito.Visible = False
            lblEstinzioneDebito.Visible = False
            cmbEstinzioneDebito.Visible = False
            cmbEstinzioneDebito.ClearSelection()
            lblDataEstinzioneDebito.Visible = False
            txtDataEstinzioneDebito.Visible = False
            txtDataEstinzioneDebito.Clear()
        End If
    End Sub
    Private Sub cmbEstinzioneDebito_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbEstinzioneDebito.SelectedIndexChanged
        VerificaEstinzioneDebito()
    End Sub
    Public Sub VerificaEstinzioneDebito()
        If cmbEstinzioneDebito.SelectedValue = "1" Then
            lblDataEstinzioneDebito.Visible = True
            txtDataEstinzioneDebito.Visible = True
        Else
            lblDataEstinzioneDebito.Visible = False
            txtDataEstinzioneDebito.Visible = False
            txtDataEstinzioneDebito.Clear()
        End If
    End Sub
End Class
