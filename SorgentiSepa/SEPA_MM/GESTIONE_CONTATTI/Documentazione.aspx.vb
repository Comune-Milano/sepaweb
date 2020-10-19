
Partial Class GESTIONE_CONTATTI_Documentazione
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
                caricaDocumentazione()
            End If
            If Not IsNothing(Request.QueryString("SL")) AndAlso IsNumeric(Request.QueryString("SL")) Then
                solaLettura()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Documentazione - Page_Load - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Documentazione - Page_LoadComplete - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Documentazione - WriteEvent - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            connData.apri(True)
            Dim id As Integer = 0
            Dim idLivello1 As String = "-1"
            Dim idLivello2 As String = "-1"

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI"
            par.cmd.ExecuteNonQuery()

            For Each elemento As DataGridItem In DataGridElenco.Items
                id = elemento.Cells(par.IndDGC(DataGridElenco, "ID")).Text
                idLivello1 = elemento.Cells(par.IndDGC(DataGridElenco, "IDLIVELLO1")).Text.Replace("&nbsp;", "-1")
                idLivello2 = elemento.Cells(par.IndDGC(DataGridElenco, "IDLIVELLO2")).Text.Replace("&nbsp;", "-1")
                'If idLivello1 <> "-1" And idLivello2 = "-1" Then
                '    idLivello2 = "NULL"
                '    par.cmd.CommandText = "delete from siscom_mi.tipo_segnalazioni_documenti where id_tipo_Segn_livello_1=" & idLivello1
                '    par.cmd.ExecuteNonQuery()
                'ElseIf idLivello1 <> "-1" And idLivello2 <> "-1" Then
                '    par.cmd.CommandText = "delete from siscom_mi.tipo_segnalazioni_documenti where id_tipo_Segn_livello_2=" & idLivello2 & " and id_tipo_Segn_livello_1=" & idLivello1
                '    par.cmd.ExecuteNonQuery()
                'ElseIf idLivello1 = "-1" And idLivello2 = "-1" Then
                '    idLivello1 = "NULL"
                '    par.cmd.CommandText = "delete from siscom_mi.tipo_segnalazioni_documenti where id_tipo_Segn_livello_2=" & idLivello2
                '    par.cmd.ExecuteNonQuery()
                'ElseIf idLivello1 = "-1" And idLivello2 <> "-1" Then
                'End If
                'If idLivello1 <> "-1" Or idLivello2 <> "-1" Then
                    For Each item As ListItem In CType(elemento.FindControl("CheckBoxList1"), CheckBoxList).Items
                        If item.Selected = True Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI (" _
                                & " ID_TIPOLOGIA_DOCUMENTO, ID_TIPO_SEGN_LIVELLO_1, ID_TIPO_SEGN_LIVELLO_2) " _
                                & " VALUES (" & item.Value & " ," _
                                & idLivello1 & "," _
                                & idLivello2 & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    Next
                'End If
            Next
            WriteEvent("F250", "Modifica globale della documentazione da elencare", "", "")
            connData.chiudi(True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
            par.modalDialogMessage("Aggiornamento documentazione", "Operazione effettuata correttamente.", Me.Page, "successo", "Documentazione.aspx")
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Documentazione - btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub solaLettura()
        Try
            Dim CTRL As Control = Nothing
            For Each CTRL In Page.Controls
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
            CType(Me.Master.FindControl("NavigationMenu"), Menu).Visible = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Documentazione - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaDocumentazione()
        Try
            connData.apri(True)
            par.cmd.CommandText = "SELECT ID," _
                & " (SELECT TIPO_sEGNALAZIONE.DESCRIZIONE FROM SISCOM_MI.TIPO_sEGNALAZIONE WHERE ID_TIPO_SEGNALAZIONE=TIPO_SEGNALAZIONE.ID) AS TIPOLOGIA," _
                & " (SELECT TIPO_sEGNALAZIONE_LIVELLO_2.ID FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_2 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_2=TIPO_SEGNALAZIONE_LIVELLO_2.ID) AS IDLIVELLO2," _
                & " (SELECT TIPO_sEGNALAZIONE_LIVELLO_1.ID FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_1=TIPO_SEGNALAZIONE_LIVELLO_1.ID) AS IDLIVELLO1," _
                & " (SELECT REPLACE(TIPO_sEGNALAZIONE_LIVELLO_1.DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_1 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_1=TIPO_SEGNALAZIONE_LIVELLO_1.ID) AS CATEGORIA1," _
                & " (SELECT REPLACE(TIPO_sEGNALAZIONE_LIVELLO_2.DESCRIZIONE,'#','') FROM SISCOM_MI.TIPO_sEGNALAZIONE_LIVELLO_2 WHERE ID_TIPO_SEGNALAZIONE_LIVELLO_2=TIPO_SEGNALAZIONE_LIVELLO_2.ID) AS CATEGORIA2 " _
                & " FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                & " WHERE ID_TIPO_SEGNALAZIONE=0 " _
                & " AND NVL(COMBINAZIONE_TIPOLOGIE.ID_TIPO_SEGNALAZIONE_LIVELLO_1,2000)>=1000 " _
                & " AND NVL(COMBINAZIONE_TIPOLOGIE.ID_TIPO_SEGNALAZIONE_LIVELLO_2,2000)>=1000 " _
                & " AND NVL(COMBINAZIONE_TIPOLOGIE.ID_TIPO_SEGNALAZIONE_LIVELLO_3,2000)>=1000 " _
                & " AND NVL(COMBINAZIONE_TIPOLOGIE.ID_TIPO_SEGNALAZIONE_LIVELLO_4,2000)>=1000 " _
                & " ORDER BY 2,3,4,5,6"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()

            If dt.Rows.Count > 0 Then
                DataGridElenco.DataSource = dt
                DataGridElenco.DataBind()
                Dim idlivello1 As String = ""
                Dim idlivello2 As String = ""
                Dim lista As New Generic.List(Of Integer)
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader
                For Each elemento As DataGridItem In DataGridElenco.Items
                    lista.Clear()
                    idlivello1 = elemento.Cells(par.IndDGC(DataGridElenco, "IDLIVELLO1")).Text.Replace("&nbsp;", "-1")
                    idlivello2 = elemento.Cells(par.IndDGC(DataGridElenco, "IDLIVELLO2")).Text.Replace("&nbsp;", "-1")
                    If idlivello1 <> "-1" And idlivello2 = "-1" Then
                        par.cmd.CommandText = "SELECT ID_TIPOLOGIA_DOCUMENTO FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI WHERE ID_TIPO_sEGN_LIVELLO_1=" & idlivello1 & " AND ID_TIPO_SEGN_LIVELLO_2 IS NULL"
                    ElseIf idlivello1 <> "-1" And idlivello2 <> "-1" Then
                        par.cmd.CommandText = "SELECT ID_TIPOLOGIA_DOCUMENTO FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI WHERE ID_TIPO_sEGN_LIVELLO_1=" & idlivello1 & " AND ID_TIPO_SEGN_LIVELLO_2=" & idlivello2
                    ElseIf idlivello1 = "-1" And idlivello2 = "-1" Then
                        par.cmd.CommandText = "SELECT ID_TIPOLOGIA_DOCUMENTO FROM SISCOM_MI.TIPO_SEGNALAZIONI_DOCUMENTI WHERE ID_TIPO_sEGN_LIVELLO_1 IS NULL AND ID_TIPO_SEGN_LIVELLO_2=" & idlivello2
                    ElseIf idlivello1 = "-1" And idlivello2 <> "-1" Then
                    End If

                    If idlivello1 <> "-1" Or idlivello2 <> "-1" Then
                        lettore = par.cmd.ExecuteReader
                        While lettore.Read
                            lista.Add(lettore(0))
                        End While
                        lettore.Close()
                        par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TIPOLOGIE_DOCUMENTI ORDER BY DESCRIZIONE", CType(elemento.FindControl("CheckBoxList1"), CheckBoxList), "ID", "DESCRIZiONE")
                        For Each Item As ListItem In CType(elemento.FindControl("CheckBoxList1"), CheckBoxList).Items
                            If lista.Contains(Item.Value) Then
                                Item.Selected = True
                            Else
                                Item.Selected = False
                            End If
                        Next
                    End If
                Next
            End If
            connData.chiudi(True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Documentazione - caricaDocumentazione - " & ex.Message)
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
End Class
