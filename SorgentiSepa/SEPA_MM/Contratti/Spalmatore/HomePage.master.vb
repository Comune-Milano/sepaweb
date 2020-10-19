Imports System.IO
Imports Telerik.Web.UI

Partial Class Spalmatore_HomePage
    Inherits System.Web.UI.MasterPage
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../../AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Me.ID = "MasterPage"
            If Not IsPostBack Then
                FormCompleto()
                lblOperatore.Text = Session.Item("OPERATORE")
            End If
            If Session.Item("MOD_SPALMATORE") <> "1" Then
                Response.Redirect("../../AccessoNegato.htm", False)
                Exit Sub
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub NavigationMenu_ItemClick(sender As Object, e As Telerik.Web.UI.RadMenuEventArgs) Handles NavigationMenu.ItemClick
        Try
            If optMenu.Value = 0 Then
                Select Case NavigationMenu.SelectedValue
                    Case "Home"
                        Session.Remove("DT_GEST")
                        Response.Redirect("SpalmatoreHome.aspx", False)
                    Case "Elaborazione"
                        Response.Redirect("RicercaGestDaElaborare.aspx", False)
                    Case "stato_elaborazione"
                        Response.Redirect("Procedure.aspx", False)
                    Case "Log"
                        Response.Redirect("Log_Spalmatore.aspx", False)
                    Case "Report1"
                        Response.Redirect("RptBollGestionali.aspx", False)
                    Case "Report2"
                        Response.Redirect("RptEmessoTotale.aspx", False)
                    Case "Report3"
                        Response.Redirect("RptBollContabili.aspx", False)
                    Case "elencoFile"
                        Response.Redirect("VisualizzazioneRpt.aspx", False)
                    Case "KPI1"
                        Response.Redirect("TabellaKPI1.aspx", False)
                    Case "Report4"
                        Response.Redirect("RptEmessoTotaleCompleto.aspx", False)
                    Case "Esci"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "validNavigation=true;self.close();", True)
                        noClose.Value = 0
                End Select
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType, "key", "alert('Chiudere la pagina in cui si sta lavorando prima di Procedere!');", True)

            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub


    Private Sub FormCompleto()
        Dim contenuto As ContentPlaceHolder = Me.Page.Controls.Item(0).FindControl("Form1").FindControl("CPContenuto")
        For Each controllo In contenuto.Controls
            If controllo.controls.count > 0 Then
                Dim collezioneControlli As Object = controllo.controls
                For Each controllo2 In collezioneControlli
                    If controllo2.controls.count > 0 Then
                        Dim collezioneControlli2 As Object = controllo2.controls
                        For Each controllo3 In collezioneControlli2
                            If TypeOf controllo3 Is TextBox Then
                                DirectCast(controllo3, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                            ElseIf TypeOf controllo3 Is DropDownList Then
                                DirectCast(controllo3, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                            ElseIf TypeOf controllo3 Is CheckBoxList Then
                                DirectCast(controllo3, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                            ElseIf TypeOf controllo3 Is CheckBox Then
                                DirectCast(controllo3, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                            ElseIf TypeOf controllo3 Is RadComboBox Then
                                DirectCast(controllo3, RadComboBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                            ElseIf TypeOf controllo3 Is RadDatePicker Then
                                DirectCast(controllo3, RadDatePicker).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")

                            End If
                        Next
                    End If
                    If TypeOf controllo2 Is TextBox Then
                        DirectCast(controllo2, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf controllo2 Is DropDownList Then
                        DirectCast(controllo2, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf controllo2 Is CheckBoxList Then
                        DirectCast(controllo2, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf controllo2 Is RadComboBox Then
                        DirectCast(controllo2, RadComboBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf controllo2 Is CheckBox Then
                        DirectCast(controllo2, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf controllo2 Is RadDatePicker Then
                        DirectCast(controllo2, RadDatePicker).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")

                    End If
                Next
            End If
            If TypeOf controllo Is TextBox Then
                DirectCast(controllo, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf controllo Is DropDownList Then
                DirectCast(controllo, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf controllo Is CheckBoxList Then
                DirectCast(controllo, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf controllo Is RadComboBox Then
                DirectCast(controllo, RadComboBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf controllo Is CheckBox Then
                DirectCast(controllo, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf controllo Is RadDatePicker Then
                DirectCast(controllo, RadDatePicker).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next
    End Sub


End Class

