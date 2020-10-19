
Partial Class GESTIONE_CONTATTI_AperturaSportelli
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                lblTitolo.Text = "Apertura sportelli"
                caricaSediTerritoriali()
                'CaricaValoriAttuali()
                TextBoxDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                TextBoxAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Operatività Sportelli - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 1
            If CType(Me.Master.FindControl("FLGC"), HiddenField).Value = "0" Then
                par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", , True)
                Exit Sub
            Else
                If CType(Me.Master.FindControl("operatoreFiliale"), HiddenField).Value <> "1" Or CType(Me.Master.FindControl("FL_GC_CALENDARIO"), HiddenField).Value <> "1" Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato ad eseguire questa operazione.", Page, "info", "Home.aspx")
                    Exit Sub
                End If
                If CType(Me.Master.FindControl("FLGC_SL"), HiddenField).Value = "1" Then
                    solaLettura()
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Operatività Sportelli - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaSediTerritoriali()
        Try
            connData.apri()
            Dim query As String = "SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT DISTINCT ID_FILIALE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI)"
            par.caricaComboBox(query, DropDownListSedeTerritoriale, "ID", "NOME", False)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Apertura Sportelli - caricaSediTerritoriali - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub solaLettura()
        Try
            Dim mpContentPlaceHolderContenuto As ContentPlaceHolder
            mpContentPlaceHolderContenuto = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
            Dim CTRL As Control = Nothing
            For Each CTRL In mpContentPlaceHolderContenuto.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                ElseIf TypeOf CTRL Is Button Then
                    DirectCast(CTRL, Button).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Apertura Sportelli - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub
    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox11.Checked = True
            CheckBox21.Checked = True
            CheckBox31.Checked = True
            CheckBox41.Checked = True
            CheckBox51.Checked = True
            CheckBox61.Checked = True
            CheckBox71.Checked = True
            CheckBox81.Checked = True
            CheckBox91.Checked = True
            CheckBox101.Checked = True
        Else
            CheckBox11.Checked = False
            CheckBox21.Checked = False
            CheckBox31.Checked = False
            CheckBox41.Checked = False
            CheckBox51.Checked = False
            CheckBox61.Checked = False
            CheckBox71.Checked = False
            CheckBox81.Checked = False
            CheckBox91.Checked = False
            CheckBox101.Checked = False
        End If
    End Sub

    Protected Sub CheckBox2_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox12.Checked = True
            CheckBox22.Checked = True
            CheckBox32.Checked = True
            CheckBox42.Checked = True
            CheckBox52.Checked = True
            CheckBox62.Checked = True
            CheckBox72.Checked = True
            CheckBox82.Checked = True
            CheckBox92.Checked = True
            CheckBox102.Checked = True
        Else
            CheckBox12.Checked = False
            CheckBox22.Checked = False
            CheckBox32.Checked = False
            CheckBox42.Checked = False
            CheckBox52.Checked = False
            CheckBox62.Checked = False
            CheckBox72.Checked = False
            CheckBox82.Checked = False
            CheckBox92.Checked = False
            CheckBox102.Checked = False
        End If
    End Sub

    Protected Sub CheckBox3_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox13.Checked = True
            CheckBox23.Checked = True
            CheckBox33.Checked = True
            CheckBox43.Checked = True
            CheckBox53.Checked = True
            CheckBox63.Checked = True
            CheckBox73.Checked = True
            CheckBox83.Checked = True
            CheckBox93.Checked = True
            CheckBox103.Checked = True
        Else
            CheckBox13.Checked = False
            CheckBox23.Checked = False
            CheckBox33.Checked = False
            CheckBox43.Checked = False
            CheckBox53.Checked = False
            CheckBox63.Checked = False
            CheckBox73.Checked = False
            CheckBox83.Checked = False
            CheckBox93.Checked = False
            CheckBox103.Checked = False
        End If
    End Sub

    Protected Sub CheckBox4_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            CheckBox14.Checked = True
            CheckBox24.Checked = True
            CheckBox34.Checked = True
            CheckBox44.Checked = True
            CheckBox54.Checked = True
            CheckBox64.Checked = True
            CheckBox74.Checked = True
            CheckBox84.Checked = True
            CheckBox94.Checked = True
            CheckBox104.Checked = True
        Else
            CheckBox14.Checked = False
            CheckBox24.Checked = False
            CheckBox34.Checked = False
            CheckBox44.Checked = False
            CheckBox54.Checked = False
            CheckBox64.Checked = False
            CheckBox74.Checked = False
            CheckBox84.Checked = False
            CheckBox94.Checked = False
            CheckBox104.Checked = False
        End If
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            If IsDate(TextBoxDal.Text) And Len(Trim(TextBoxDal.Text)) = 10 And Len(Trim(TextBoxAl.Text)) = 10 And IsDate(Trim(TextBoxAl.Text)) Then
                Dim dataIniziale As Date = Trim(TextBoxDal.Text)
                Dim dataFinale As Date = Trim(TextBoxAl.Text)
                If dataIniziale <= dataFinale Then
                    connData.apri(True)
                    Dim data As Date = dataIniziale
                    Dim mpContentPlaceHolder As ContentPlaceHolder
                    mpContentPlaceHolder = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
                    While data <= dataFinale
                        For iii As Integer = 1 To 10
                            For jjj As Integer = 1 To 4
                                For Each i As ListItem In Me.chkGiorni.Items
                                    If i.Selected = True Then
                                        par.cmd.CommandText = "select trim(to_char(to_date('" & data & "','dd/mm/yyyy'),'DAY')) from dual"
                                        Dim giorno As String = par.cmd.ExecuteScalar
                                        If giorno.ToUpper = i.Value.ToUpper Then
                                            If CType(mpContentPlaceHolder.FindControl("CheckBox" & iii & jjj), CheckBox).Checked = True Then
                                                par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_FILIALE = " & DropDownListSedeTerritoriale.SelectedValue _
                                                    & "AND GIORNO = " & par.FormatoDataDB(data) _
                                                    & "AND ID_ORARIO = (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_ORARI WHERE INDICE=" & iii & ")" _
                                                    & " AND ID_SPORTELLO = (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE=" & jjj _
                                                    & " AND ID_FILIALE=" & DropDownListSedeTerritoriale.SelectedValue & ")"
                                                par.cmd.ExecuteNonQuery()
                                            End If
                                        End If
                                    End If
                                Next
                            Next
                        Next
                        data = data.AddDays(1)
                    End While
                    'WriteEvent("F240", "Operatività sportelli modificata")
                    connData.chiudi(True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    par.modalDialogMessage("Operatività sportelli", "Operatività sportelli modificata correttamente.", Me.Page, "successo")
                Else
                    par.modalDialogMessage("Operatività sportelli", "Data iniziale superiore alla data finale.\nCorreggere il range di date.", Me.Page, "info")
                End If
            Else
                par.modalDialogMessage("Operatività sportelli", "Range di date errato o non compilato.\nInserire un range di date corretto.", Me.Page, "info")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Apertura Sportelli - btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
