
Partial Class GESTIONE_CONTATTI_AttivazioneSportelli
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
                lblTitolo.Text = "Abilitazione sportelli"
                CaricaAttivazione()
                CaricaValoriAttuali()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Abilitazione Sportelli - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 1
            'l'attività di gestione del calendario è demandata solo agli operatori di filiale
            If CType(Me.Master.FindControl("FLGC"), HiddenField).Value = "0" Then
                par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", , True)
                Exit Sub
            Else
                If CType(Me.Master.FindControl("operatoreFiliale"), HiddenField).Value <> "1" Or CType(Me.Master.FindControl("supervisore"), HiddenField).Value <> "1" Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato ad eseguire questa operazione.", Page, "info", "Home.aspx")
                    Exit Sub
                End If
                If CType(Me.Master.FindControl("FLGC_SL"), HiddenField).Value = "1" Then
                    solaLettura()
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Abilitazione Sportelli - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            connData.apri(True)
            Dim attivo As Integer = 0
            If CheckBox1.Checked = True Then
                attivo = 1
            End If
            par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_SPORTELLI SET FL_ATTIVO=" & attivo & " WHERE ID_FILIALE=" & DropDownListSedeTerritoriale.SelectedValue & " AND INDICE=1"
            par.cmd.ExecuteNonQuery()
            attivo = 0
            If CheckBox2.Checked = True Then
                attivo = 1
            End If
            par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_SPORTELLI SET FL_ATTIVO=" & attivo & " WHERE ID_FILIALE=" & DropDownListSedeTerritoriale.SelectedValue & " AND INDICE=2"
            par.cmd.ExecuteNonQuery()
            attivo = 0
            If CheckBox3.Checked = True Then
                attivo = 1
            End If
            par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_SPORTELLI SET FL_ATTIVO=" & attivo & " WHERE ID_FILIALE=" & DropDownListSedeTerritoriale.SelectedValue & " AND INDICE=3"
            par.cmd.ExecuteNonQuery()
            attivo = 0
            If CheckBox4.Checked = True Then
                attivo = 1
            End If
            par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_SPORTELLI SET FL_ATTIVO=" & attivo & " WHERE ID_FILIALE=" & DropDownListSedeTerritoriale.SelectedValue & " AND INDICE=4"
            par.cmd.ExecuteNonQuery()
            attivo = 0
            If CheckBox5.Checked = True Then
                attivo = 1
            End If
            par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_SPORTELLI SET FL_ATTIVO=" & attivo & " WHERE ID_FILIALE=" & DropDownListSedeTerritoriale.SelectedValue & " AND INDICE=5"
            par.cmd.ExecuteNonQuery()
            attivo = 0
            If CheckBox6.Checked = True Then
                attivo = 1
            End If
            par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_SPORTELLI SET FL_ATTIVO=" & attivo & " WHERE ID_FILIALE=" & DropDownListSedeTerritoriale.SelectedValue & " AND INDICE=6"
            par.cmd.ExecuteNonQuery()
            WriteEvent("F236", "Abilitazione/disabilitazione sportelli sede territoriale " & DropDownListSedeTerritoriale.SelectedItem.Text)
            connData.chiudi(True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
            par.modalDialogMessage("Abilitazione sportelli", "Sportelli abilitati/disabilitati correttamente.", Me.Page, "successo", "AttivazioneSportelli.aspx")
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Abilitazione Sportelli - btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaAttivazione()
        Try
            connData.apri()
            Dim query As String = "SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT DISTINCT ID_FILIALE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI)"
            par.caricaComboBox(query, DropDownListSedeTerritoriale, "ID", "NOME", False)
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE ID_FILIALE=" & DropDownListSedeTerritoriale.SelectedValue
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                Select Case par.IfNull(lettore("INDICE"), "")
                    Case "1"
                        If par.IfNull(lettore("FL_ATTIVO"), "0") = "0" Then
                            CheckBox1.Checked = False
                        Else
                            CheckBox1.Checked = True
                        End If
                    Case "2"
                        If par.IfNull(lettore("FL_ATTIVO"), "0") = "0" Then
                            CheckBox2.Checked = False
                        Else
                            CheckBox2.Checked = True
                        End If
                    Case "3"
                        If par.IfNull(lettore("FL_ATTIVO"), "0") = "0" Then
                            CheckBox3.Checked = False
                        Else
                            CheckBox3.Checked = True
                        End If
                    Case "4"
                        If par.IfNull(lettore("FL_ATTIVO"), "0") = "0" Then
                            CheckBox4.Checked = False
                        Else
                            CheckBox4.Checked = True
                        End If
                    Case "5"
                        If par.IfNull(lettore("FL_ATTIVO"), "0") = "0" Then
                            CheckBox5.Checked = False
                        Else
                            CheckBox5.Checked = True
                        End If
                    Case "6"
                        If par.IfNull(lettore("FL_ATTIVO"), "0") = "0" Then
                            CheckBox6.Checked = False
                        Else
                            CheckBox6.Checked = True
                        End If
                    Case Else
                End Select
            End While
            lettore.Close()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Abilitazione Sportelli - CaricaAttivazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub DropDownListSedeTerritoriale_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListSedeTerritoriale.SelectedIndexChanged
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE ID_FILIALE=" & DropDownListSedeTerritoriale.SelectedValue
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                Select Case par.IfNull(lettore("INDICE"), "")
                    Case "1"
                        If par.IfNull(lettore("FL_ATTIVO"), "0") = "0" Then
                            CheckBox1.Checked = False
                        Else
                            CheckBox1.Checked = True
                        End If
                    Case "2"
                        If par.IfNull(lettore("FL_ATTIVO"), "0") = "0" Then
                            CheckBox2.Checked = False
                        Else
                            CheckBox2.Checked = True
                        End If
                    Case "3"
                        If par.IfNull(lettore("FL_ATTIVO"), "0") = "0" Then
                            CheckBox3.Checked = False
                        Else
                            CheckBox3.Checked = True
                        End If
                    Case "4"
                        If par.IfNull(lettore("FL_ATTIVO"), "0") = "0" Then
                            CheckBox4.Checked = False
                        Else
                            CheckBox4.Checked = True
                        End If
                    Case "5"
                        If par.IfNull(lettore("FL_ATTIVO"), "0") = "0" Then
                            CheckBox5.Checked = False
                        Else
                            CheckBox5.Checked = True
                        End If
                    Case "6"
                        If par.IfNull(lettore("FL_ATTIVO"), "0") = "0" Then
                            CheckBox6.Checked = False
                        Else
                            CheckBox6.Checked = True
                        End If
                    Case Else
                End Select
            End While
            lettore.Close()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Abilitazione Sportelli - DropDownListSedeTerritoriale_SelectedIndexChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaValoriAttuali()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT " _
                & " DESCRIZIONE AS SPORTELLO,NOME AS SEDE_TERRITORIALE,(CASE WHEN FL_ATTIVO=1 THEN 'Sì' ELSE 'No' END) AS ATTIVO " _
                & " FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI,SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=APPUNTAMENTI_SPORTELLI.ID_FILIALE ORDER BY 2,1"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                DataGridAttivazioneSportelli.DataSource = dt
                DataGridAttivazioneSportelli.DataBind()
                DataGridAttivazioneSportelli.Visible = True
            Else
                DataGridAttivazioneSportelli.Visible = False
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Abilitazione Sportelli - CaricaValoriAttuali - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Abilitazione Sportelli - WriteEvent - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Abilitazione Sportelli - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub
End Class
