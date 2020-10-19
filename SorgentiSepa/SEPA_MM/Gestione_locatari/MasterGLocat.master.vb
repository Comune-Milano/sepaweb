Imports Telerik.Web.UI
Imports System.IO

Partial Class Gestione_locatari_MasterGLocat
    Inherits System.Web.UI.MasterPage
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ID = "MasterPage"
        If Not IsPostBack Then
            ModificheCampi(Me)
            SetPathLock()
        End If
    End Sub

    Private Sub ModificheCampi(ByVal obj As Control)
        Dim CTRL As Control = Nothing

        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                ModificheCampi(CTRL)
            End If
            If TypeOf CTRL Is RadTextBox Then
                DirectCast(CTRL, RadTextBox).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is RadComboBox Then
                DirectCast(CTRL, RadComboBox).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is RadNumericTextBox Then
                DirectCast(CTRL, RadNumericTextBox).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is RadDateInput Then
                DirectCast(CTRL, RadDateInput).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is RadAutoCompleteBox Then
                DirectCast(CTRL, RadAutoCompleteBox).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is RadDropDownList Then
                DirectCast(CTRL, RadDropDownList).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is RadDatePicker Then
                DirectCast(CTRL, RadDatePicker).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            End If
        Next

    End Sub

    Private Sub SetPathLock()
        Try
            Dim PathLock As String = "SepacomLock.svc"
            Dim PathTrovato As Boolean = False
            While PathTrovato = False
                If File.Exists(Server.MapPath(PathLock)) Then
                    HFPathLock.Value = PathLock
                    PathTrovato = True
                Else
                    PathLock = "../" & PathLock
                End If
            End While
        Catch ex As Exception
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class

