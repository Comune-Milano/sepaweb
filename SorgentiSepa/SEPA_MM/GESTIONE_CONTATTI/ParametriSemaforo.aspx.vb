
Partial Class GESTIONE_CONTATTI_ParametriSemaforo
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
                caricaParametri()
            End If
            If Not IsNothing(Request.QueryString("SL")) AndAlso IsNumeric(Request.QueryString("SL")) Then
                solaLettura()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Parametri Semaforo - Page_Load - " & ex.Message)
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
                TextBoxGiorniGiacenza1.Enabled = False
                TextBoxGiorniGiacenza2.Enabled = False
                TextBoxNote1.Enabled = False
                TextBoxNote2.Enabled = False
                ButtonSalva.Enabled = False
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Parametri Semaforo - Page_LoadComplete - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Parametri Semaforo - WriteEvent - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Parametri Semaforo - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaParametri()
        Try
            connData.apri(True)
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_SEMAFORI"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim id As Integer = 0
            While lettore.Read
                id = par.IfNull(lettore("id"), 0)
                If id = 1 Then
                    TextBoxGiorniGiacenza1.Text = par.IfNull(lettore("giacenza"), 30)
                    TextBoxNote1.Text = par.IfNull(lettore("note"), 10)
                ElseIf id = 2 Then
                    TextBoxGiorniGiacenza2.Text = par.IfNull(lettore("giacenza"), 15)
                    TextBoxNote2.Text = par.IfNull(lettore("note"), 6)
                End If
            End While
            lettore.Close()
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Parametri Semaforo - caricaSemaforo - " & ex.Message)
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

    Protected Sub ButtonSalva_Click(sender As Object, e As System.EventArgs) Handles ButtonSalva.Click
        Try
            connData.apri(True)
            If giacenza1.Value <> TextBoxGiorniGiacenza1.Text AndAlso IsNumeric(TextBoxGiorniGiacenza1.Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.PARAMETRI_SEMAFORI SET GIACENZA=" & TextBoxGiorniGiacenza1.Text.Replace(".", "").Replace("-", "").Replace(",", "") & " WHERE ID=1"
                par.cmd.ExecuteNonQuery()
                WriteEvent("F253", "Modifica parametri dei semafori", giacenza1.Value, TextBoxGiorniGiacenza1.Text.Replace(".", "").Replace("-", "").Replace(",", ""))
            End If
            If giacenza2.Value <> TextBoxGiorniGiacenza2.Text AndAlso IsNumeric(TextBoxGiorniGiacenza2.Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.PARAMETRI_SEMAFORI SET GIACENZA=" & TextBoxGiorniGiacenza2.Text.Replace(".", "").Replace("-", "").Replace(",", "") & " WHERE ID=2"
                par.cmd.ExecuteNonQuery()
                WriteEvent("F253", "Modifica parametri dei semafori", giacenza2.Value, TextBoxGiorniGiacenza2.Text.Replace(".", "").Replace("-", "").Replace(",", ""))
            End If
           
            If note1.Value <> TextBoxNote1.Text AndAlso IsNumeric(TextBoxNote1.Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.PARAMETRI_SEMAFORI SET NOTE=" & TextBoxNote1.Text.Replace(".", "").Replace("-", "").Replace(",", "") & " WHERE ID=1"
                par.cmd.ExecuteNonQuery()
                WriteEvent("F253", "Modifica parametri dei semafori", note1.Value, TextBoxNote1.Text.Replace(".", "").Replace("-", "").Replace(",", ""))
            End If
            If note2.Value <> TextBoxNote2.Text AndAlso IsNumeric(TextBoxNote2.Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.PARAMETRI_SEMAFORI SET NOTE=" & TextBoxNote2.Text.Replace(".", "").Replace("-", "").Replace(",", "") & " WHERE ID=2"
                par.cmd.ExecuteNonQuery()
                WriteEvent("F253", "Modifica parametri dei semafori", note2.Value, TextBoxNote2.Text.Replace(".", "").Replace("-", "").Replace(",", ""))
            End If
            connData.chiudi(True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
            par.modalDialogMessage("Aggiornamento semaforo", "Operazione effettuata correttamente.", Me.Page, "successo", "ParametriSemaforo.aspx")
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Parametri Semaforo - ButtonSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class