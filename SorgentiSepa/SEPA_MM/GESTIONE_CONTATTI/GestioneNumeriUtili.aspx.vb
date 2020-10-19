
Partial Class GESTIONE_CONTATTI_GestioneNumeriUtili
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
                lblTitolo.Text = "Gestione numeri utili"
                caricaSediTerritoriali()
                caricaTipologiaNumeri()
                'caricaFasceOrarie()
                'caricaGiorni()
                caricaCategoria()
                CaricaNumeriUtili()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 1
            If CType(Me.Master.FindControl("FLGC"), HiddenField).Value = "0" Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", "Home.aspx")
                Exit Sub
            Else
                If CType(Me.Master.FindControl("operatoreFiliale"), HiddenField).Value <> "1" Or CType(Me.Master.FindControl("FL_GC_TABELLE_SUPP"), HiddenField).Value <> "1" Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato ad eseguire questa operazione.", Page, "info", "Home.aspx")
                    Exit Sub
                End If
                If CType(Me.Master.FindControl("FLGC_SL"), HiddenField).Value = "1" Then
                    solaLettura()
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            If Trim(TextBoxValore.Text) <> "" Then
                connData.apri(True)
                Dim sedeTerritoriale As String = ""
                Dim inseriti = 0
                For Each item As ListItem In CheckBoxListSedi.Items
                    If item.Selected = True Then
                        sedeTerritoriale = item.Value
                        If CheckBox1.Checked = True Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.NUMERI_UTILI (ID, ID_TIPOLOGIE_NUMERI_UTILI, ID_STRUTTURA, VALORE,FASCIA_ORARIA,GIORNI,ID_TIPO_FASCIA,id_tipo_Segnalazione) " _
                                & " values (SISCOM_MI.SEQ_NUMERI_UTILI.NEXTVAL," & DropDownListTipologiaNumeroUtile.SelectedValue & "," & sedeTerritoriale & " , '" & par.PulisciStrSql(TextBoxValore.Text) & "','','',1," & DropDownListCategoria.SelectedValue & ")"
                            par.cmd.ExecuteNonQuery()
                            WriteEvent("F245", "")
                            inseriti += 1
                        End If
                        If CheckBox2.Checked = True Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.NUMERI_UTILI (ID, ID_TIPOLOGIE_NUMERI_UTILI, ID_STRUTTURA, VALORE,FASCIA_ORARIA,GIORNI,ID_TIPO_FASCIA,id_tipo_Segnalazione) " _
                                    & " values (SISCOM_MI.SEQ_NUMERI_UTILI.NEXTVAL," & DropDownListTipologiaNumeroUtile.SelectedValue & "," & sedeTerritoriale & " , '" & par.PulisciStrSql(TextBoxValore.Text) & "','','',2," & DropDownListCategoria.SelectedValue & ")"
                            par.cmd.ExecuteNonQuery()
                            WriteEvent("F245", "")
                            inseriti += 1
                        End If
                        If CheckBox3.Checked = True Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.NUMERI_UTILI (ID, ID_TIPOLOGIE_NUMERI_UTILI, ID_STRUTTURA, VALORE,FASCIA_ORARIA,GIORNI,ID_TIPO_FASCIA,id_tipo_Segnalazione) " _
                                    & " values (SISCOM_MI.SEQ_NUMERI_UTILI.NEXTVAL," & DropDownListTipologiaNumeroUtile.SelectedValue & "," & sedeTerritoriale & " , '" & par.PulisciStrSql(TextBoxValore.Text) & "','','',3," & DropDownListCategoria.SelectedValue & ")"
                            par.cmd.ExecuteNonQuery()
                            WriteEvent("F245", "")
                            inseriti += 1
                        End If
                        'If CheckBox4.Checked = True Then
                        '    Dim giorni As String = DropDownListGiorniIni4.SelectedValue & "-" & DropDownListGiorniFin4.SelectedValue
                        '    Dim fasciaOraria As String = DropDownListOraInizio4.SelectedValue & ":" & DropDownListMinutiInizio4.SelectedValue & "-" & DropDownListOraFine4.SelectedValue & ":" & DropDownListMinutifine4.SelectedValue
                        '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.NUMERI_UTILI (ID, ID_TIPOLOGIE_NUMERI_UTILI, ID_STRUTTURA, VALORE,FASCIA_ORARIA,GIORNI,ID_TIPO_FASCIA,id_tipo_Segnalazione) " _
                        '            & " values (SISCOM_MI.SEQ_NUMERI_UTILI.NEXTVAL," & DropDownListTipologiaNumeroUtile.SelectedValue & "," & sedeTerritoriale & " , '" & par.PulisciStrSql(TextBoxValore.Text) & "','" & fasciaOraria & "','" & giorni & "',4," & DropDownListCategoria.SelectedValue & ")"
                        '    par.cmd.ExecuteNonQuery()
                        '    WriteEvent("F245", "")
                        '    inseriti += 1
                        'End If
                    End If
                Next

                'Dim fasciaOraria As String = ""
                'Dim fasciaOrariaInizio As String = DropDownListOraInizio.SelectedValue & DropDownListMinutiInizio.SelectedValue
                'Dim fasciaOrariaFine As String = DropDownListOraFine.SelectedValue & DropDownListMinutifine.SelectedValue
                'fasciaOraria = DropDownListOraInizio.SelectedValue & ":" & DropDownListMinutiInizio.SelectedValue & "-" & DropDownListOraFine.SelectedValue & ":" & DropDownListMinutifine.SelectedValue
                'If fasciaOraria = "00:00-00:00" Then
                '    fasciaOraria = ""
                'End If
                'Dim sedeTerritoriale As String = "NULL"
                'Dim listaSelezionate As String = ""
                'Dim inseriti As Integer = 0
                'For Each item As ListItem In CheckBoxListSedi.Items
                '    If item.Selected = True Then
                '        inseriti += 1
                '        If item.Value = 0 Then
                '            sedeTerritoriale = "NULL"
                '        Else
                '            sedeTerritoriale = item.Value
                '        End If
                '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.NUMERI_UTILI (ID, ID_TIPOLOGIE_NUMERI_UTILI, ID_STRUTTURA, VALORE,FASCIA_ORARIA,GIORNI) " _
                '            & " values (SISCOM_MI.SEQ_NUMERI_UTILI.NEXTVAL," & DropDownListTipologiaNumeroUtile.SelectedValue & "," & sedeTerritoriale & " , '" & par.PulisciStrSql(TextBoxValore.Text) & "','" & fasciaOraria & "')"
                '        par.cmd.ExecuteNonQuery()
                '        WriteEvent("F245", "")
                '    End If
                'Next
                connData.chiudi(True)
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                If inseriti > 0 Then
                    If inseriti = 1 Then
                        par.modalDialogMessage("Gestione numeri utili", "Numero inserito correttamente", Page, "successo", "GestioneNumeriUtili.aspx")
                    Else
                        par.modalDialogMessage("Gestione numeri utili", "Numeri inseriti correttamente", Page, "successo", "GestioneNumeriUtili.aspx")
                    End If
                Else
                    par.modalDialogMessage("Gestione numeri utili", "Nessun numero inserito", Page, "info", "GestioneNumeriUtili.aspx")
                End If
                connData.chiudi(True)
            Else
                par.modalDialogMessage("Gestione numeri utili", "Inserire un valore valido", Page, "info")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaNumeriUtili()
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT NUMERI_UTILI.ID,DESCRIZIONE AS TIPO, VALORE," _
                & " (case when id_tipo_fascia = 1 " _
                & " then 'LUN-VEN 09:00-18:00' " _
                & " when id_tipo_fascia = 2 " _
                & " then 'LUN-VEN 18:00-09:00' " _
                & " when id_tipo_fascia = 3 " _
                & " then 'SAB-DOM' " _
                & " when id_tipo_fascia = 4 " _
                & " THEN giorni ||' '||FASCIA_ORARIA " _
                & " ELSE " _
                & " NULL " _
                & " END " _
                & " ) " _
                & " AS FASCIA, " _
                & " (SELECT TIPO_SEGNALAZIONE.DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_sEGNALAZIONE.ID=ID_TIPO_sEGNALAZIONE) AS CATEGORIA, " _
                & " (CASE WHEN TAB_FILIALI.NOME IS NULL THEN 'SEDE CENTRALE' ELSE TAB_FILIALI.NOME END) AS SEDE_TERRITORIALE,'' AS ELIMINA " _
                & " FROM SISCOM_MI.NUMERI_UTILI, SISCOM_MI.TIPOLOGIE_NUMERI_UTILI, SISCOM_MI.TAB_FILIALI " _
                & " WHERE NUMERI_UTILI.ID_TIPOLOGIE_NUMERI_UTILI = TIPOLOGIE_NUMERI_UTILI.ID " _
                & " AND TAB_FILIALI.ID(+)=NUMERI_UTILI.ID_STRUTTURA "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                DataGridNumeriUtili.DataSource = dt
                DataGridNumeriUtili.DataBind()
            End If
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - caricaNumeriUtili - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaSediTerritoriali()
        Try
            connData.apri()
            Dim query As String = "SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID=105 UNION SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT DISTINCT ID_FILIALE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI)"
            par.caricaCheckBoxList(query, CheckBoxListSedi, "ID", "NOME")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - caricaSediTerritoriali - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub DataGridNumeriUtili_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridNumeriUtili.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            e.Item.Cells(par.IndDGC(DataGridNumeriUtili, "ELIMINA")).Text = "<a href=""#"" onclick=""javascript:EliminaNumeroUtile(" & e.Item.Cells(par.IndDGC(DataGridNumeriUtili, "ID")).Text & ");void(0);""><img border=""none"" src=""Immagini/delete_ico.png"" alt=""Elimina il numero"" height=""16"" width=""16"" /></a>"
        End If
    End Sub
    Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        Try
            If idEliminazione.Value <> "" AndAlso IsNumeric(idEliminazione.Value) Then
                connData.apri(True)
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.NUMERI_UTILI WHERE ID=" & idEliminazione.Value
                par.cmd.ExecuteNonQuery()
                idEliminazione.Value = ""
                WriteEvent("F244", "")
                connData.chiudi(True)
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                par.modalDialogMessage("Gestione Numeri Utili", "Numero eliminato correttamente", Page, "successo", "GestioneNumeriUtili.aspx")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - btnElimina_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String)
        Dim connOpNow As Boolean = False
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri()
                connOpNow = True
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (NULL," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'" & cod & "','" & par.PulisciStrSql(motivo) & "')"
            par.cmd.ExecuteNonQuery()
            If par.OracleConn.State = Data.ConnectionState.Open And connOpNow = True Then
                connData.chiudi()
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - WriteEvent - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaTipologiaNumeri()
        Try
            connData.apri()
            Dim query As String = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_NUMERI_UTILI"
            par.caricaComboBox(query, DropDownListTipologiaNumeroUtile, "ID", "DESCRIZIONE", False)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - caricaTipologiaNumeri - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    'Private Sub caricaFasceOrarie()
    '    Try
    '        DropDownListOraInizio4.Items.Clear()
    '        DropDownListOraFine4.Items.Clear()
    '        DropDownListMinutiInizio4.Items.Clear()
    '        DropDownListMinutifine4.Items.Clear()
    '        For i As Integer = 0 To 24
    '            DropDownListOraInizio4.Items.Add(CStr(i).PadLeft(2, "0"))
    '            DropDownListOraFine4.Items.Add(CStr(i).PadLeft(2, "0"))
    '        Next
    '        For i As Integer = 0 To 45 Step 15
    '            DropDownListMinutiInizio4.Items.Add(CStr(i).PadLeft(2, "0"))
    '            DropDownListMinutifine4.Items.Add(CStr(i).PadLeft(2, "0"))
    '        Next

    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - caricaFasceOrarie - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub
    'Private Sub caricaGiorni()
    '    Try
    '        DropDownListGiorniIni4.SelectedValue = "LUN"
    '        DropDownListGiorniIni4.SelectedValue = "LUN"
    '        DropDownListOraInizio4.SelectedValue = "00"
    '        DropDownListMinutiInizio4.SelectedValue = "00"
    '        DropDownListOraFine4.SelectedValue = "00"
    '        DropDownListMinutifine4.SelectedValue = "00"
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - caricaGiorni - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub
    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub

    Protected Sub ButtonAggiungi_Click(sender As Object, e As System.EventArgs) Handles ButtonAggiungi.Click
        MultiView1.ActiveViewIndex = 0
        MultiView2.ActiveViewIndex = 0
    End Sub

    Private Sub caricaCategoria()
        Try
            connData.apri()
            par.caricaComboBox("select * from siscom_mi.tipo_segnalazione order by 2 asc", DropDownListCategoria, "id", "descrizione", False)
            connData.chiudi()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - caricaCategoria - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

End Class
