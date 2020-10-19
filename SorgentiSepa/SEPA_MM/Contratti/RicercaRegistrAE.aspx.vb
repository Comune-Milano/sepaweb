Imports System
Imports System.Data
Imports System.IO
Imports Telerik.Web.UI

Partial Class Contratti_RicercaRegistrAE
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim Str As String = ""
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", True)
            Exit Sub
        End If

        Me.connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then

            cmbAnno.Items.Add("(Nessun Filtro)")
            For ii = 0 To 20
                cmbAnno.Items.Add(CStr(Year(Now) - ii))
            Next

            Dim item As RadComboBoxItem = cmbAnno.FindItemByText(CStr(Year(Now)))
            item.Selected = True

            cmbMese.Items.Add("(Nessun Filtro)")
            cmbMese.Items.Add("Gennaio")
            cmbMese.Items.Add("Febbraio")
            cmbMese.Items.Add("Marzo")
            cmbMese.Items.Add("Aprile")
            cmbMese.Items.Add("Maggio")
            cmbMese.Items.Add("Giugno")
            cmbMese.Items.Add("Luglio")
            cmbMese.Items.Add("Agosto")
            cmbMese.Items.Add("Settembre")
            cmbMese.Items.Add("Ottobre")
            cmbMese.Items.Add("Novembre")
            cmbMese.Items.Add("Dicembre")

        End If
    End Sub
    

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
        Try
            Dim anno As String = cmbAnno.SelectedItem.Text
            Dim mese As String = cmbMese.SelectedIndex

            If cmbMese.Text = "(Nessun Filtro)" Then
                mese = "0"
            End If

            If cmbAnno.Text = "(Nessun Filtro)" Then
                anno = "0"
            End If

            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "var c = window.open('SituazioneAE.aspx?ANNO=" & anno & "&MESE=" & mese & "');c.focus();", True)

        Catch ex As Exception
            connData.chiudi(False)
        End Try
    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "function PaginaHome() {document.location.href = 'pagina_home.aspx';};PaginaHome();", True)
    End Sub

End Class
