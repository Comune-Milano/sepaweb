Partial Class SPESE_REVERSIBILI_Emesso
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If controlloProfilo() Then
            CType(Master.FindControl("TitoloMaster"), Label).Text = "Emesso - Calcolo"
            CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
            If Not IsPostBack Then
                CaricaCheckBoxList()
                SettaCampi()
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
            RadWindowManager1.RadAlert("E\' necessario selezionare almeno una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
            Return False
        End If
        If Session.Item("FL_SPESE_REV_SL") = "1" Then
            solaLettura()
        End If
        connData = New CM.datiConnessione(par, False, False)
        Return True
    End Function
    Private Sub solaLettura()
        btnCalcolaEmesso.Enabled = False
    End Sub
    Private Sub CaricaCheckBoxList()
        Try
            connData.apri()
            par.caricaCheckBoxList("SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE GRUPPO = 3 AND SELEZIONABILE = 1 ORDER BY DESCRIZIONE ASC", chkServizi, "ID", "DESCRIZIONE")
            par.caricaCheckBoxList("SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE GRUPPO = 3 AND SELEZIONABILE = 1 ORDER BY DESCRIZIONE ASC", chkRiscaldamento, "ID", "DESCRIZIONE")
            par.caricaCheckBoxList("SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE GRUPPO = 3 AND SELEZIONABILE = 1 ORDER BY DESCRIZIONE ASC", chkAscensore, "ID", "DESCRIZIONE")
            par.caricaCheckBoxList("SELECT ID, SUBSTR(INIZIO,0,4) ANNO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN) ORDER BY ANNO DESC", chkAnno, "ANNO", "ANNO")
            connData.chiudi()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - CaricaCheckBoxList - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub btnCalcolaEmesso_Click(sender As Object, e As EventArgs) Handles btnCalcolaEmesso.Click
        Try
            connData.apri(True)
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.EMESSO_VOCI"
            par.cmd.ExecuteNonQuery()
            'ID_TIPOLOGIA_SPESA 1 = SERVIZI
            For Each i As ListItem In chkServizi.Items
                If i.Selected = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EMESSO_VOCI ( " _
                                        & "  ID_TIPOLOGIA_SPESA, ID_VOCE) " _
                                        & " VALUES( 1, " _
                                        & i.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            'ID_TIPOLOGIA_SPESA 2 = RISCALDAMENTO
            For Each i As ListItem In chkRiscaldamento.Items
                If i.Selected = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EMESSO_VOCI ( " _
                                        & "  ID_TIPOLOGIA_SPESA, ID_VOCE) " _
                                        & " VALUES( 2, " _
                                        & i.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            'ID_TIPOLOGIA_SPESA 3 = ASCENSORE
            For Each i As ListItem In chkAscensore.Items
                If i.Selected = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EMESSO_VOCI ( " _
                                        & "  ID_TIPOLOGIA_SPESA, ID_VOCE) " _
                                        & " VALUES( 3, " _
                                        & i.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            'ANNO
            For Each i As ListItem In chkAnno.Items
                If i.Selected = True Then
                    par.cmd.CommandText = "SISCOM_MI.CALCOLOEMESSOOOAA"
                    par.cmd.CommandType = Data.CommandType.StoredProcedure
                    par.cmd.Parameters.Add("annoEmissione", i.Value)
                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()
                End If
            Next
            par.cmd.CommandType = Data.CommandType.Text
            par.cmd.CommandText = "UPDATE SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI SET FL_EMESSO_IMPORTATO=0"
            par.cmd.ExecuteNonQuery()
            connData.chiudi(True)
            RadWindowManager1.RadAlert("Calcolo dell\' emesso richiesto terminati!", 300, 150, "Successo", "", "null")
            'RadNotificationNote.Text = "Operazione effettuata"
            'RadNotificationNote.Show()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - CaricaCheckBoxList - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SettaCampi()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT ID_VOCE FROM SISCOM_MI.EMESSO_VOCI WHERE ID_TIPOLOGIA_SPESA = 1"
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Dim dtFiltrata As New Data.DataTable
            'ID_TIPOLOGIA_SPESA 1 = SERVIZI
            For Each i As ListItem In chkServizi.Items
                Dim view As New Data.DataView(dt)
                view.RowFilter = "ID_VOCE = " & i.Value
                dtFiltrata = view.ToTable
                If dtFiltrata.Rows.Count > 0 Then
                    i.Selected = True
                End If
            Next
            par.cmd.CommandText = "SELECT ID_VOCE FROM SISCOM_MI.EMESSO_VOCI WHERE ID_TIPOLOGIA_SPESA = 2"
            dt = par.getDataTableGrid(par.cmd.CommandText)
            'ID_TIPOLOGIA_SPESA 2 = RISCALDAMENTO
            For Each i As ListItem In chkRiscaldamento.Items
                Dim view As New Data.DataView(dt)
                view.RowFilter = "ID_VOCE = " & i.Value
                dtFiltrata = view.ToTable
                If dtFiltrata.Rows.Count > 0 Then
                    i.Selected = True
                End If
            Next
            par.cmd.CommandText = "SELECT ID_VOCE FROM SISCOM_MI.EMESSO_VOCI WHERE ID_TIPOLOGIA_SPESA = 3"
            dt = par.getDataTableGrid(par.cmd.CommandText)
            'ID_TIPOLOGIA_SPESA 3 = ASCENSORE
            For Each i As ListItem In chkAscensore.Items
                Dim view As New Data.DataView(dt)
                view.RowFilter = "ID_VOCE = " & i.Value
                dtFiltrata = view.ToTable
                If dtFiltrata.Rows.Count > 0 Then
                    i.Selected = True
                End If
            Next
            connData.chiudi()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - CaricaCheckBoxList - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
