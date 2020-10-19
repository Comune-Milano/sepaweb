Imports Telerik.Web.UI

Partial Class SICUREZZA_HomePage
    Inherits System.Web.UI.MasterPage
    Dim par As New CM.Global

    

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Me.ID = "MasterPage"
            If Not IsPostBack Then
                FormCompleto()
                lblOperatore.Text = Session.Item("OPERATORE")
            End If
            If Session.Item("MOD_SICUREZZA") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
                Exit Sub
            End If

            If Session.Item("MOD_SICUREZZA") = "1" Then
                fl_sicurezza.Value = "1"
            Else
                fl_sicurezza.Value = "0"
            End If
            If Session.Item("MOD_SICUREZZA_SL") = "1" Then
                fl_sicurezza_sl.Value = "1"
            Else
                fl_sicurezza_sl.Value = "0"
            End If
            If Session.Item("MLoading") = "" Then
                Session.Add("MLoading", "1")
                If RicavaNotifiche() > 0 Then
                    RadNotification1.Show()
                End If
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Master - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub NavigationMenu_ItemClick(sender As Object, e As Telerik.Web.UI.RadMenuEventArgs) Handles NavigationMenu.ItemClick
        Try
            If optMenu.Value = 0 Then
                Select Case NavigationMenu.SelectedValue
                    Case "Home"
                        Response.Redirect("Home.aspx", False)
                    Case "NuovaS"
                        Response.Redirect("NuovaSegnalazione.aspx", False)
                    Case "RicercaS"
                        Response.Redirect("RicercaSegnalazioni.aspx", False)
                    Case "NuovoI"
                        Response.Redirect("NuovoIntervento.aspx", False)
                    Case "RicercaI"
                        Response.Redirect("RicercaInterventi.aspx", False)
                    Case "RicercaP"
                        Response.Redirect("RicercaProcedimenti.aspx", False)
                    Case "RicercaF"
                        Response.Redirect("RicercaFascicolo.aspx", False)
                    Case "Contratti"
                        Response.Redirect("RicercaContratti.aspx", False)
                    Case "Unita"
                        Response.Redirect("RicercaUI.aspx", False)
                    Case "Gruppi"
                        Response.Redirect("GestioneGruppi.aspx", False)
                    Case "OpGruppi"
                        Response.Redirect("GestioneGruppiOperatori.aspx", False)
                    Case "ElencoAb"
                        Response.Redirect("ReportAbusivi.aspx", False)
                    Case "Esci"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "validNavigation=true;self.close();", True)
                        noClose.Value = 0
                End Select
            Else
                'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Chiudere la pagina in cui si sta lavorando prima di Procedere!", 300, 150, "Attenzione", Nothing, Nothing)
                ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType, "key", "alert('Chiudere la pagina in cui si sta lavorando prima di Procedere!');", True)

            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Master - NavigationMenu_MenuItemClick - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
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

    Protected Sub OnCallbackUpdate(sender As Object, e As RadNotificationEventArgs)
        RicavaNotifiche()
    End Sub

    Private Function RicavaNotifiche() As Integer
        Dim newMsgs As Integer = DateTime.Now.Second Mod 10
        RicavaNotifiche = 0
        Try
            Dim s As String = ""

            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()
            lbl0.Text = "Elenco interventi da concludere:<br />"

            Dim nominativo As String = ""
            par.cmd.CommandText = "select cognome||' '|| nome as nominativo from operatori where id=" & Session.Item("id_operatore")
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                nominativo = par.IfNull(lettore("nominativo"), "")
            End If
            lettore.Close()

            par.cmd.CommandText = "select (select descrizione from siscom_mi.tab_stati_interventi where id=id_stato) as statoInterv,interventi_sicurezza.id from siscom_mi.interventi_sicurezza where id_stato not in (4,5) and assegnatario='" & nominativo.Replace("'", "''") & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                RicavaNotifiche = RicavaNotifiche + 1
                lbl0.Text = lbl0.Text & "<br/>" & [String].Concat(New Object() {"<a href='#' onclick=" & Chr(34) & "javascript:window.open('NuovoIntervento.aspx?NM=1&IDI=" & myReader("id") & "');" & Chr(34) & ";" & Chr(34) & ">Intervento num." & myReader("id") & " - (" & myReader("statoInterv") & ")!</a>"})
            Loop

            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If RicavaNotifiche > 0 Then
                RadNotification1.Value = newMsgs.ToString()
            Else
                RadNotification1.Value = ""
                lbl0.Text = ""
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "HideNotification();", True)
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza: Sicurezza - RicavaNotifiche - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Function

    
End Class

