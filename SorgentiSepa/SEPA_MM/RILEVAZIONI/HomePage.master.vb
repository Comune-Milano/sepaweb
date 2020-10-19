Partial Class RILEVAZIONI_HomePage
    Inherits System.Web.UI.MasterPage
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            Me.ID = "MasterPage"
            If Not IsPostBack Then
                lblOperatore.Text = Session.Item("OPERATORE")
            End If
            If Session.Item("MOD_RILIEVO") <> "1" Then
                par.modalDialogMessage("Rilevazioni", "Operatore non abilitato al modulo Gestione Rilevazioni Stato di Fatto U.I.!", Page, "info", , True)
            End If

            NascondiMenu()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Rilevazioni - Master - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub NascondiMenu()
        Try
            If Session.Item("FL_RILIEVO_GEST") <> "1" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "Gestione")
            End If
            If Session.Item("FL_RILIEVO_PAR") <> "1" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "Parametri")
            End If
            If Session.Item("FL_RILIEVO_CAR") <> "1" And Session.Item("ID_CAF") <> "6" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "CDati")
            End If
            If Session.Item("LIVELLO") <> "1" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "Scheda")
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Rilevazioni - Master - NascondiMenu - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Protected Sub NavigationMenu_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles NavigationMenu.MenuItemClick
        Try
            If optMenu.Value = 0 Then
                Select Case NavigationMenu.SelectedValue
                    Case "Home"
                        Response.Redirect("Home.aspx", False)
                    Case "Utenti"
                        Response.Redirect("GestUtenti.aspx", False)
                    Case "UtentiOperatori"
                        Response.Redirect("UtentiOperatori.aspx", False)
                    Case "GestUnita"
                        Response.Redirect("GestUnita.aspx", False)
                    Case "ValoriMonetari"
                        Response.Redirect("ValoriMonetari.aspx", False)
                    Case "GestLotti"
                        Response.Redirect("GestLotti.aspx", False)
                    Case "GestRilievo"
                        Response.Redirect("GestRilievo.aspx", False)
                    Case "ScaricaScheda"
                        Response.Redirect("ScaricaScheda.aspx", False)
                    Case "CaricaScheda"
                        Response.Redirect("CaricaScheda.aspx", False)
                    Case "GestRefer"
                        Response.Redirect("GestReferenti.aspx", False)
                    Case "rpt1"
                        Response.Redirect("ReportUIrilev.aspx", False)
                    Case "rpt2"
                        Response.Redirect("ReportUIriattam.aspx", False)
                    Case "Scheda"
                        Response.Redirect("SchedaAlloggioExcel.aspx", False)
                    Case "StampaSchede"
                        Response.Redirect("RicercaSchedeDaStampare.aspx", False)
                    Case "Guida"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('Guida_ModuloRilievi.pdf','_blank','top='+altezza+',left='+larghezza+',scrollbars=yes,width=800,height=600,resizable=yes,menubar=yes');}", True)
                        'Response.Redirect("Guida_ModuloRilievi.pdf", False)
                    Case "Esci"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "validNavigation=true;self.close();", True)
                        noClose.Value = 0
                End Select
            Else
                par.modalDialogMessage("Attenzione", "Chiudere la pagina in cui si sta lavorando prima di Procedere!", Me.Page)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - Master - NavigationMenu_MenuItemClick - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class

