
Partial Class SPESE_REVERSIBILI_ImportAutogestioni
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If controlloProfilo() Then
            CType(Master.FindControl("TitoloMaster"), Label).Text = "Consuntivi e Conguagli - Gestione - Autogestioni"
            CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
            If Not IsPostBack Then
                settaCampi()
                caricaPiano()
            End If
        End If
    End Sub
    Private Function controlloProfilo() As Boolean
        'CONTROLLO DELLA SESSIONE OPERATORE E DELL'ABILITAZIONE ALLE SPESE REVERSIBILI
        If Session.Item("OPERATORE") <> "" Then
            If Session.Item("fl_spese_reversibili") = "0" Then
                Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Operatore non abilitato alla gestione delle spese reversibili")
                RadWindowManager1.RadAlert("Operatore non abilitato alla gestione delle spese reversibili!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
                Return False
            End If
        Else
            RadWindowManager1.RadAlert("Accesso negato o sessione scaduta! E\' necessario rieseguire il login!", 300, 150, "Attenzione", "", "null")
            Return False
        End If
        If Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = 0 Then
            RadWindowManager1.RadAlert("E\' necessario selezionare una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
            Return False
        End If
        If Session.Item("FL_SPESE_REV_SL") = "1" Then
            solaLettura()
        End If
        connData = New CM.datiConnessione(par, False, False)
        Return True
    End Function
    Private Sub solaLettura()
        btnImporta.Enabled = False
    End Sub
    Private Sub settaCampi()
        Try
            connData.apri(False)
            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.CRITERI_RIPARTIZIONE ORDER BY DESCRIZIONE ASC", cmbCriterioRipartizione, "ID", "DESCRIZIONE", True)
            cmbCriterioRipartizione.SelectedValue = 4
            cmbCriterioRipartizione.Enabled = False
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: settaCampi - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Private Sub btnImporta_Click(sender As Object, e As EventArgs) Handles btnImporta.Click
        Try
            connData.apri(True)
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            par.cmd.CommandText = "SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
            Dim idpiano As Integer = CInt(par.IfNull(par.cmd.ExecuteScalar, 0))
            If idpiano = 0 Then
                'piano finanziario non selezionato in sede di creazione dell'elaborazione
                par.cmd.CommandText = "SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN"
                idpiano = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If
            par.cmd.CommandText = "SISCOM_MI.CONSUNTIVO_AUTOGESTIONI"
            par.cmd.CommandType = Data.CommandType.StoredProcedure
            par.cmd.Parameters.Add("ris", 0).Direction = Data.ParameterDirection.ReturnValue
            par.cmd.Parameters.Add("idPf", idElaborazione)
            par.cmd.Parameters.Add("rip", cmbCriterioRipartizione.SelectedValue)
            par.cmd.Parameters.Add("idPiano", idpiano)
            par.cmd.ExecuteNonQuery()
            Dim risOp As Integer = par.cmd.Parameters("ris").Value
            Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
            par.cmd.Parameters.Clear()
            par.cmd.CommandType = Data.CommandType.Text
            If risOp > 0 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG (ID_PF,ID_OPERATORE, DATA_ORA, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) " _
                    & " VALUES (" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," & Session.Item("ID_OPERATORE") & ", '" & Tempo & "', '-'," _
                    & "'-', '-' , 8)"
                par.cmd.ExecuteNonQuery()
            End If
            connData.chiudi(True)
            RadWindowManager1.RadAlert("Operazione effettuata: importate " & risOp & " autogestioni.", 300, 150, "Attenzione", "function f(sender,args){location.href='ProspettoConsuntivi.aspx?';}", "null")
            'RadNotificationNote.Text = "Operazione effettuata: importate " & ris & " autogestioni"
            'RadNotificationNote.Show()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: btnImporta_Click - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Private Sub caricaPiano()
        Try
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            connData.apri()
            par.cmd.CommandText = "SELECT DATA_RIFERIMENTO_INIZIO_C,DATA_RIFERIMENTO_FINE_C FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                Try
                    txtDataInizio.SelectedDate = CDate(par.IfNull(lettore("DATA_RIFERIMENTO_INIZIO_C"), ""))
                    txtDataFine.SelectedDate = CDate(par.IfNull(lettore("DATA_RIFERIMENTO_FINE_C"), ""))
                    txtDataInizio.Enabled = False
                    txtDataFine.Enabled = False
                Catch ex As Exception
                    txtDataInizio.Enabled = True
                    txtDataFine.Enabled = True
                End Try
            End If
            lettore.Close()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            RadWindowManager1.RadAlert("Errore durante il caricamento del piano finanziario", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx?';}", "null")
        End Try
    End Sub
End Class
