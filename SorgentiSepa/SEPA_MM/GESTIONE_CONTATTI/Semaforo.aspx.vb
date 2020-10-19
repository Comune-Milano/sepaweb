
Imports Telerik.Web.UI

Partial Class GESTIONE_CONTATTI_Semaforo
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
                ' caricaSemaforo()

            End If
            If Not IsNothing(Request.QueryString("SL")) AndAlso IsNumeric(Request.QueryString("SL")) Then
                solaLettura()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Semaforo - Page_Load - " & ex.Message)
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
                If CType(Me.Master.FindControl("FLGC_SL"), HiddenField).Value = "1" Then
                    solaLettura()
                End If
            End If
            If CType(Me.Master.FindControl("FL_GC_TABELLE_SUPP"), HiddenField).Value = "0" Then
                DataGridElenco.Enabled = False
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Semaforo - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String, Optional ByVal valoreVecchio As String = "", Optional ByVal valoreNuovo As String = "")
        Dim connOpNow As Boolean = False
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri()
                connOpNow = True
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,VALORE_OLD,VALORE_NEW) " _
                & "VALUES ( NULL," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'" & cod & "','" & par.PulisciStrSql(motivo) & "','" & par.PulisciStrSql(valoreVecchio) & "','" & par.PulisciStrSql(valoreNuovo) & "')"
            par.cmd.ExecuteNonQuery()
            If par.OracleConn.State = Data.ConnectionState.Open And connOpNow = True Then
                connData.chiudi()
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Semaforo - WriteEvent - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            connData.apri(True)
            Dim id As Integer = 0
            Dim orarioUfficio As String = "-1"
            Dim fuoriOrarioUfficio1 As String = "-1"
            Dim fuoriOrarioUfficio2 As String = "-1"
            Dim categorizzazione As String = "-1"
            For Each elemento As GridDataItem In DataGridElenco.Items
                id = elemento.Cells(par.FindIndexColRadGrid(DataGridElenco, "ID") + 2).Text
                Select Case CType(elemento.FindControl("cmbUrgenza0"), RadComboBox).SelectedValue
                    Case "Bianco"
                        orarioUfficio = "1"
                    Case "Verde"
                        orarioUfficio = "2"
                    Case "Giallo"
                        orarioUfficio = "3"
                    Case "Rosso"
                        orarioUfficio = "4"
                    Case "Blu"
                        orarioUfficio = "0"
                End Select
                Select Case CType(elemento.FindControl("cmbUrgenza1"), RadComboBox).SelectedValue
                    Case "Bianco"
                        fuoriOrarioUfficio1 = "1"
                    Case "Verde"
                        fuoriOrarioUfficio1 = "2"
                    Case "Giallo"
                        fuoriOrarioUfficio1 = "3"
                    Case "Rosso"
                        fuoriOrarioUfficio1 = "4"
                    Case "Blu"
                        fuoriOrarioUfficio1 = "0"
                End Select
                Select Case CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue
                    Case "Bianco"
                        fuoriOrarioUfficio2 = "1"
                    Case "Verde"
                        fuoriOrarioUfficio2 = "2"
                    Case "Giallo"
                        fuoriOrarioUfficio2 = "3"
                    Case "Rosso"
                        fuoriOrarioUfficio2 = "4"
                    Case "Blu"
                        fuoriOrarioUfficio2 = "0"
                End Select
                If fuoriOrarioUfficio1 <> "-1" And fuoriOrarioUfficio2 <> "-1" And orarioUfficio <> "-1" Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEMAFORO SET ORARIO_UFFICIO=" & orarioUfficio & "," _
                        & " FUORI_ORARIO_UFFICIO1=" & fuoriOrarioUfficio1 & "," _
                        & " FUORI_ORARIO_UFFICIO2=" & fuoriOrarioUfficio2 _
                        & " WHERE ID=" & id
                    par.cmd.ExecuteNonQuery()
                End If
                par.cmd.CommandText = "UPDATE SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                                    & " SET ID_TIPO_MANUTENZIONE = " & par.RitornaNullSeMenoUno(CType(elemento.FindControl("cmbTipologiaManutenzione"), RadComboBox).SelectedValue, "-1") _
                                    & " WHERE ID IN (SELECT ID_COMB FROM SISCOM_MI.SEMAFORO WHERE ID = " & id & ")"
                par.cmd.ExecuteNonQuery()
                CType(elemento.FindControl("cmbTipologiaManutenzione"), RadComboBox).ClearSelection()
                CType(elemento.FindControl("cmbTipologiaManutenzione"), RadComboBox).SelectedValue = categorizzazione
                'Select Case semaforo3
                '    Case "1"
                '        CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "1"
                '    Case "2"
                '        CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Verde"
                '    Case "3"
                '        CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Giallo"
                'End Select
            Next
            WriteEvent("F313", "Modifica globale delle categorizzazioni", "", "")
            WriteEvent("F249", "Modifica globale dei semafori d'ufficio", "", "")
            connData.chiudi(True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
            par.modalDialogMessage("Aggiornamento semaforo", "Operazione effettuata correttamente.", Me.Page, "successo", "")
            DataGridElenco.Rebind()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Semaforo - btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub solaLettura()
        Try
            Dim CTRL As Control = Nothing
            For Each CTRL In Page.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is RadComboBox Then
                    DirectCast(CTRL, RadComboBox).Enabled = False
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
            CType(Me.Master.FindControl("NavigationMenu"), WebControls.Menu).Visible = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Semaforo - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaSemaforo()
        Try
            connData.apri(True)
            par.cmd.CommandText = "SELECT ID," _
                & " (SELECT TIPO_sEGNALAZIONE.DESCRIZIONE FROM SISCOM_MI.TIPO_sEGNALAZIONE WHERE ID_TIPOLOGIA_SEGNALAZIONE=TIPO_SEGNALAZIONE.ID) AS TIPOLOGIA," _
                & " (SELECT REPLACE(TIPO_sEGNALAZIONE_LIVELLO_1.DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_1 WHERE ID_TIPOLOGIA_SEGNALAZIONE_LIV1=TIPO_SEGNALAZIONE_LIVELLO_1.ID) AS CATEGORIA1," _
                & " (SELECT REPLACE(TIPO_sEGNALAZIONE_LIVELLO_2.DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_2 WHERE ID_TIPOLOGIA_SEGNALAZIONE_LIV2=TIPO_SEGNALAZIONE_LIVELLO_2.ID) AS CATEGORIA2," _
                & " (SELECT REPLACE(TIPO_sEGNALAZIONE_LIVELLO_3.DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_3 WHERE ID_TIPOLOGIA_SEGNALAZIONE_LIV3=TIPO_SEGNALAZIONE_LIVELLO_3.ID) AS CATEGORIA3," _
                & " (SELECT REPLACE(TIPO_sEGNALAZIONE_LIVELLO_4.DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_4 WHERE ID_TIPOLOGIA_SEGNALAZIONE_LIV4=TIPO_SEGNALAZIONE_LIVELLO_4.ID) AS CATEGORIA4, " _
                & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_TIPOLOGIA_MANUTENZIONE WHERE ID = (SELECT ID_TIPO_MANUTENZIONE FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE WHERE ID = SEMAFORO.ID_COMB)) AS CATEGORIZZAZIONE , " _
                & " (SELECT ID_TIPO_MANUTENZIONE FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE WHERE ID = SEMAFORO.ID_COMB) AS ID_CATEGORIZZAZIONE, " _
                & " ORARIO_UFFICIO AS SEMAFORO1, " _
                & " FUORI_ORARIO_UFFICIO1 AS SEMAFORO2, " _
                & " FUORI_ORARIO_UFFICIO2 AS SEMAFORO3" _
                & " FROM SISCOM_MI.SEMAFORO " _
                & " WHERE ID_TIPOLOGIA_SEGNALAZIONE IN (1) " _
                & " AND NVL(SEMAFORO.ID_TIPOLOGIA_SEGNALAZIONE_LIV1,2000)>=1000 " _
                & " AND NVL(SEMAFORO.ID_TIPOLOGIA_SEGNALAZIONE_LIV2,2000)>=1000 " _
                & " AND NVL(SEMAFORO.ID_TIPOLOGIA_SEGNALAZIONE_LIV3,2000)>=1000 " _
                & " AND NVL(SEMAFORO.ID_TIPOLOGIA_SEGNALAZIONE_LIV4,2000)>=1000 " _
                & " ORDER BY 2,3,4,5,6"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi(True)
            If dt.Rows.Count > 0 Then
                DataGridElenco.DataSource = dt
                DataGridElenco.DataBind()
                Dim semaforo1 As String = "-1"
                Dim semaforo2 As String = "-1"
                Dim semaforo3 As String = "-1"
                Dim categorizzazione As String = "-1"
                For Each elemento As GridDataItem In DataGridElenco.Items
                    CType(elemento.FindControl("cmbUrgenza0"), RadComboBox).LoadContentFile("Gravita.xml")
                    CType(elemento.FindControl("cmbUrgenza1"), RadComboBox).LoadContentFile("Gravita.xml")
                    CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).LoadContentFile("Gravita.xml")
                    semaforo1 = par.IfNull(elemento.Cells(par.FindIndexColRadGrid(DataGridElenco, "SEMAFORO1")).Text.Replace("&nbsp;", "-1"), "-1")
                    semaforo2 = par.IfNull(elemento.Cells(par.FindIndexColRadGrid(DataGridElenco, "SEMAFORO2")).Text.Replace("&nbsp;", "-1"), "-1")
                    semaforo3 = par.IfNull(elemento.Cells(par.FindIndexColRadGrid(DataGridElenco, "SEMAFORO3")).Text.Replace("&nbsp;", "-1"), "-1")
                    categorizzazione = par.IfNull(elemento.Cells(par.FindIndexColRadGrid(DataGridElenco, "ID_CATEGORIZZAZIONE")).Text.Replace("&nbsp;", "-1"), "-1")
                    If semaforo1 <> "-1" Then
                        Select Case semaforo1
                            Case "1"
                                CType(elemento.FindControl("cmbUrgenza0"), RadComboBox).SelectedValue = "Bianco"
                            Case "2"
                                CType(elemento.FindControl("cmbUrgenza0"), RadComboBox).SelectedValue = "Verde"
                            Case "3"
                                CType(elemento.FindControl("cmbUrgenza0"), RadComboBox).SelectedValue = "Giallo"
                            Case "4"
                                CType(elemento.FindControl("cmbUrgenza0"), RadComboBox).SelectedValue = "Rosso"
                            Case "0"
                                CType(elemento.FindControl("cmbUrgenza0"), RadComboBox).SelectedValue = "Blu"
                        End Select
                    End If
                    If semaforo2 <> "-1" Then
                        Select Case semaforo2
                            Case "1"
                                CType(elemento.FindControl("cmbUrgenza1"), RadComboBox).SelectedValue = "Bianco"
                            Case "2"
                                CType(elemento.FindControl("cmbUrgenza1"), RadComboBox).SelectedValue = "Verde"
                            Case "3"
                                CType(elemento.FindControl("cmbUrgenza1"), RadComboBox).SelectedValue = "Giallo"
                            Case "4"
                                CType(elemento.FindControl("cmbUrgenza1"), RadComboBox).SelectedValue = "Rosso"
                            Case "0"
                                CType(elemento.FindControl("cmbUrgenza1"), RadComboBox).SelectedValue = "Blu"
                        End Select
                    End If
                    If semaforo3 <> "-1" Then
                        Select Case semaforo3
                            Case "1"
                                CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Bianco"
                            Case "2"
                                CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Verde"
                            Case "3"
                                CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Giallo"
                            Case "4"
                                CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Rosso"
                            Case "0"
                                CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Blu"
                        End Select
                    End If
                    If categorizzazione <> "-1" Then
                        CType(elemento.FindControl("cmbTipologiaManutenzione"), RadComboBox).ClearSelection()
                        CType(elemento.FindControl("cmbTipologiaManutenzione"), RadComboBox).SelectedValue = categorizzazione
                        'Select Case semaforo3
                        '    Case "1"
                        '        CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "1"
                        '    Case "2"
                        '        CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Verde"
                        '    Case "3"
                        '        CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Giallo"
                        'End Select
                    End If
                Next
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Semaforo - caricaSemaforo - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub imgEsci_Click(sender As Object, e As System.EventArgs) Handles imgEsci.Click
        If Not IsNothing(Request.QueryString("SL")) AndAlso IsNumeric(Request.QueryString("SL")) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "validNavigation=true;self.close();", True)
        Else
            Response.Redirect("Home.aspx", False)
        End If
    End Sub

    Private Sub DataGridElenco_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles DataGridElenco.NeedDataSource
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT ID," _
                & " (SELECT TIPO_sEGNALAZIONE.DESCRIZIONE FROM SISCOM_MI.TIPO_sEGNALAZIONE WHERE ID_TIPOLOGIA_SEGNALAZIONE=TIPO_SEGNALAZIONE.ID) AS TIPOLOGIA," _
                & " (SELECT REPLACE(TIPO_sEGNALAZIONE_LIVELLO_1.DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_1 WHERE ID_TIPOLOGIA_SEGNALAZIONE_LIV1=TIPO_SEGNALAZIONE_LIVELLO_1.ID) AS CATEGORIA1," _
                & " (SELECT REPLACE(TIPO_sEGNALAZIONE_LIVELLO_2.DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_2 WHERE ID_TIPOLOGIA_SEGNALAZIONE_LIV2=TIPO_SEGNALAZIONE_LIVELLO_2.ID) AS CATEGORIA2," _
                & " (SELECT REPLACE(TIPO_sEGNALAZIONE_LIVELLO_3.DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_3 WHERE ID_TIPOLOGIA_SEGNALAZIONE_LIV3=TIPO_SEGNALAZIONE_LIVELLO_3.ID) AS CATEGORIA3," _
                & " (SELECT REPLACE(TIPO_sEGNALAZIONE_LIVELLO_4.DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_4 WHERE ID_TIPOLOGIA_SEGNALAZIONE_LIV4=TIPO_SEGNALAZIONE_LIVELLO_4.ID) AS CATEGORIA4, " _
                & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_TIPOLOGIA_MANUTENZIONE WHERE ID = (SELECT ID_TIPO_MANUTENZIONE FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE WHERE ID = SEMAFORO.ID_COMB)) AS CATEGORIZZAZIONE , " _
                & " (SELECT ID_TIPO_MANUTENZIONE FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE WHERE ID = SEMAFORO.ID_COMB) AS ID_CATEGORIZZAZIONE, " _
                & " ORARIO_UFFICIO AS SEMAFORO1, " _
                & " FUORI_ORARIO_UFFICIO1 AS SEMAFORO2, " _
                & " FUORI_ORARIO_UFFICIO2 AS SEMAFORO3" _
                & " FROM SISCOM_MI.SEMAFORO " _
                & " WHERE ID_TIPOLOGIA_SEGNALAZIONE IN (1) " _
                & " AND NVL(SEMAFORO.ID_TIPOLOGIA_SEGNALAZIONE_LIV1,2000)>=1000 " _
                & " AND NVL(SEMAFORO.ID_TIPOLOGIA_SEGNALAZIONE_LIV2,2000)>=1000 " _
                & " AND NVL(SEMAFORO.ID_TIPOLOGIA_SEGNALAZIONE_LIV3,2000)>=1000 " _
                & " AND NVL(SEMAFORO.ID_TIPOLOGIA_SEGNALAZIONE_LIV4,2000)>=1000 "
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(par.cmd.CommandText)
            DataGridElenco.DataSource = dt
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Semaforo - caricaSemaforo - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub DataGridElenco_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles DataGridElenco.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim dataItem As GridDataItem = e.Item
                Dim semaforo1 As String = "-1"
                Dim semaforo2 As String = "-1"
                Dim semaforo3 As String = "-1"
                Dim categorizzazione As String = "-1"
                CType(dataItem.FindControl("cmbUrgenza0"), RadComboBox).LoadContentFile("Gravita.xml")
                CType(dataItem.FindControl("cmbUrgenza1"), RadComboBox).LoadContentFile("Gravita.xml")
                CType(dataItem.FindControl("cmbUrgenza2"), RadComboBox).LoadContentFile("Gravita.xml")
                semaforo1 = par.IfNull(dataItem("SEMAFORO1").Text.Replace("&nbsp;", "-1"), "-1")
                semaforo2 = par.IfNull(dataItem("SEMAFORO2").Text.Replace("&nbsp;", "-1"), "-1")
                semaforo3 = par.IfNull(dataItem("SEMAFORO3").Text.Replace("&nbsp;", "-1"), "-1")
                categorizzazione = par.IfNull(dataItem("ID_CATEGORIZZAZIONE").Text.Replace("&nbsp;", "-1"), "-1")
                If semaforo1 <> "-1" Then
                    Select Case semaforo1
                        Case "1"
                            CType(dataItem.FindControl("cmbUrgenza0"), RadComboBox).SelectedValue = "Bianco"
                        Case "2"
                            CType(dataItem.FindControl("cmbUrgenza0"), RadComboBox).SelectedValue = "Verde"
                        Case "3"
                            CType(dataItem.FindControl("cmbUrgenza0"), RadComboBox).SelectedValue = "Giallo"
                        Case "4"
                            CType(dataItem.FindControl("cmbUrgenza0"), RadComboBox).SelectedValue = "Rosso"
                        Case "0"
                            CType(dataItem.FindControl("cmbUrgenza0"), RadComboBox).SelectedValue = "Blu"
                    End Select
                End If
                If semaforo2 <> "-1" Then
                    Select Case semaforo2
                        Case "1"
                            CType(dataItem.FindControl("cmbUrgenza1"), RadComboBox).SelectedValue = "Bianco"
                        Case "2"
                            CType(dataItem.FindControl("cmbUrgenza1"), RadComboBox).SelectedValue = "Verde"
                        Case "3"
                            CType(dataItem.FindControl("cmbUrgenza1"), RadComboBox).SelectedValue = "Giallo"
                        Case "4"
                            CType(dataItem.FindControl("cmbUrgenza1"), RadComboBox).SelectedValue = "Rosso"
                        Case "0"
                            CType(dataItem.FindControl("cmbUrgenza1"), RadComboBox).SelectedValue = "Blu"
                    End Select
                End If
                If semaforo3 <> "-1" Then
                    Select Case semaforo3
                        Case "1"
                            CType(dataItem.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Bianco"
                        Case "2"
                            CType(dataItem.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Verde"
                        Case "3"
                            CType(dataItem.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Giallo"
                        Case "4"
                            CType(dataItem.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Rosso"
                        Case "0"
                            CType(dataItem.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Blu"
                    End Select
                End If
                If categorizzazione <> "-1" Then
                    CType(dataItem.FindControl("cmbTipologiaManutenzione"), RadComboBox).ClearSelection()
                    CType(dataItem.FindControl("cmbTipologiaManutenzione"), RadComboBox).SelectedValue = categorizzazione
                    'Select Case semaforo3
                    '    Case "1"
                    '        CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "1"
                    '    Case "2"
                    '        CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Verde"
                    '    Case "3"
                    '        CType(elemento.FindControl("cmbUrgenza2"), RadComboBox).SelectedValue = "Giallo"
                    'End Select
                End If

            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Semaforo - DataGridElenco_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    
End Class

