
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_GestCambioFornitura
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_UTENZE") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            connData.apri()
            caricaFornitura()
            caricaFornitoreOld()
            caricaFornitoreNew()
            connData.chiudi()
        End If
    End Sub
    Private Sub caricaFornitura()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_UTENZE WHERE ID IN (SELECT ID_TIPO_FORNITURA FROM SISCOM_MI.POD)", RadComboBoxFornitura, "ID", "DESCRIZIONE", False, , , True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " caricaFornitura - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaFornitoreOld()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.caricaComboTelerik("SELECT ID,COD_FORNITORE||' - '||RAGIONE_SOCIALE AS DESCRIZIONE FROM SISCOM_MI.FORNITORI WHERE ID IN (SELECT DISTINCT ID_FORNITORE FROM SISCOM_MI.POD WHERE ID_TIPO_FORNITURA=" & RadComboBoxFornitura.SelectedValue & " AND POD.ID IN (SELECT ID_POD FROM SISCOM_MI.PATRIMONIO_POD WHERE DATA_FINE IS NULL)) ORDER BY RAGIONE_SOCIALE", RadComboBoxFornitoreOld, "ID", "DESCRIZIONE", False, , , True)
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " caricaFornitoreOld - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaFornitoreNew()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            If IsNumeric(RadComboBoxFornitoreOld.SelectedValue) Then
                par.caricaComboTelerik("SELECT ID,COD_FORNITORE||' - '||RAGIONE_SOCIALE AS DESCRIZIONE FROM SISCOM_MI.FORNITORI WHERE ID NOT IN (SELECT ID_FORNITORE FROM SISCOM_MI.POD WHERE ID_TIPO_FORNITURA=" & RadComboBoxFornitura.SelectedValue & ") ORDER BY RAGIONE_SOCIALE", RadComboBoxFornitoreNew, "ID", "DESCRIZIONE", False, , , True)
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " caricaFornitoreNew - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonModifica_Click(sender As Object, e As EventArgs) Handles RadButtonModifica.Click
        Try
            If Not IsNumeric(RadComboBoxFornitura.SelectedValue) Or RadComboBoxFornitura.SelectedValue < 0 Then
                RadWindowManager1.RadAlert("Selezionare una fornitura", 300, 150, "Duplicazione POD con nuovo fornitore", Nothing, Nothing)
                Exit Sub
            End If
            If Not IsNumeric(RadComboBoxFornitoreOld.SelectedValue) Then
                RadWindowManager1.RadAlert("Selezionare il fornitore da modificare", 300, 150, "Duplicazione POD con nuovo fornitore", Nothing, Nothing)
                Exit Sub
            End If
            If Not IsNumeric(RadComboBoxFornitoreNew.SelectedValue) Or RadComboBoxFornitoreNew.SelectedValue < 0 Then
                RadWindowManager1.RadAlert("Selezionare il nuovo fornitore", 300, 150, "Duplicazione POD con nuovo fornitore", Nothing, Nothing)
                Exit Sub
            End If
            'If Not IsDate(TextBoxDataDecorrenza.SelectedDate) Then
            '    RadWindowManager1.RadAlert("Selezionare la data di decorrenza", 300, 150, "Duplicazione POD con nuovo fornitore", Nothing, Nothing)
            '    Exit Sub
            'End If
            connData.apri(True)
            par.cmd.CommandText = "SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID=" & RadComboBoxFornitoreOld.SelectedValue
            Dim fornitore As String = par.IfNull(par.cmd.ExecuteScalar, "")
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.POD (" _
                & "ID, ID_TIPO_FORNITURA, ID_FORNITORE, " _
                & "CONTRATTO, POD, DESCRIZIONE,  " _
                & "FL_ATTIVO, FL_ELIMINATO,ID_OLD)  " _
                & "(SELECT  " _
                & "SISCOM_MI.SEQ_POD.NEXTVAL, " _
                & "ID_TIPO_FORNITURA, " _
                & RadComboBoxFornitoreNew.SelectedValue & ", " _
                & "CONTRATTO, " _
                & "POD, " _
                & "DESCRIZIONE/*(SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=" & RadComboBoxFornitoreNew.SelectedValue & ")||" _
                & "' - '||(SELECT INDIRIZZI.LOCALITA || ' - ' || DESCRIZIONE||', '||CIVICO " _
                & "FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID IN (SELECT ID_INDIRIZZO_RIFERIMENTO " _
                & "FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID IN (SELECT MAX (" _
                & "ID_COMPLESSO) FROM SISCOM_MI.PATRIMONIO_POD WHERE PATRIMONIO_POD.ID_POD = POD.ID)))*/, " _
                & "1, " _
                & "0, " _
                & "ID " _
                & " FROM SISCOM_MI.POD " _
                & " WHERE ID_TIPO_FORNITURA= " & RadComboBoxFornitura.SelectedValue _
                & " AND ID_FORNITORE= " & RadComboBoxFornitoreOld.SelectedValue _
                & " AND POD.ID IN (SELECT ID_POD FROM SISCOM_MI.PATRIMONIO_POD WHERE DATA_FINE IS NULL) " _
                & ")"
            Dim ris As Integer = par.cmd.ExecuteNonQuery
            If ris = 0 Then
                RadWindowManager1.RadAlert("Nessun POD duplicato con nuovo fornitore", 300, 150, "Duplicazione POD con nuovo fornitore", Nothing, Nothing)
            Else
                'Dim dataFine As Date = CDate(TextBoxDataDecorrenza.SelectedDate).AddDays(-1)
                'Dim dataInizio As Date = CDate(TextBoxDataDecorrenza.SelectedDate)
                'par.cmd.CommandText = "SELECT DISTINCT DATA_INIZIO FROM SISCOM_MI.PATRIMONIO_POD " _
                '    & " WHERE ID_POD IN (SELECT ID FROM SISCOM_MI.POD " _
                '    & " WHERE ID_TIPO_FORNITURA = " & RadComboBoxFornitura.SelectedValue _
                '    & " And ID_FORNITORE = " & RadComboBoxFornitoreOld.SelectedValue & ") " _
                '    & " AND DATA_INIZIO>'" & par.FormatoDataDB(dataFine) & "' " _
                '    & " AND DATA_FINE IS NULL "
                'Dim conteggioAnomalie As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                'If conteggioAnomalie > 0 Then
                '    RadWindowManager1.RadAlert("Impossibile duplicare i POD, verificare la data di decorrenza", 300, 150, "Duplicazione POD con nuovo fornitore", Nothing, Nothing)
                '    Exit Sub
                'End If
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PATRIMONIO_POD (ID, ID_COMPLESSO, ID_EDIFICIO, ID_POD, INDIRIZZO," _
                    & " COMUNE, CAP, ID_AGGREGAZIONE, DATA_INIZIO, DATA_FINE, NOME_AGGREGAZIONE, ID_OLD) " _
                    & " (SELECT SISCOM_MI.SEQ_PATRIMONIO_POD.NEXTVAL,ID_COMPLESSO,ID_EDIFICIO," _
                    & " (Select ID FROM SISCOM_MI.POD WHERE POD.ID_OLD=PATRIMONIO_POD.ID_POD)," _
                    & " INDIRIZZO, COMUNE, CAP, ID_AGGREGAZIONE, NULL, NULL, NOME_AGGREGAZIONE, ID_OLD" _
                    & " From SISCOM_MI.PATRIMONIO_POD Where ID_POD In (Select ID FROM SISCOM_MI.POD " _
                    & " WHERE ID_TIPO_FORNITURA = " & RadComboBoxFornitura.SelectedValue _
                    & " And ID_FORNITORE = " & RadComboBoxFornitoreOld.SelectedValue & ")" _
                    & " AND DATA_FINE IS NULL " _
                    & ")"
                Dim ris2 As Integer = par.cmd.ExecuteNonQuery()
                'par.cmd.CommandText = "UPDATE SISCOM_MI.PATRIMONIO_POD set " _
                '    & " DATA_FINE='" & par.FormatoDataDB(dataFine) & "' " _
                '    & " WHERE ID_POD IN (SELECT ID FROM SISCOM_MI.POD " _
                '    & " WHERE ID_TIPO_FORNITURA = " & RadComboBoxFornitura.SelectedValue _
                '    & " AND ID_FORNITORE = " & RadComboBoxFornitoreOld.SelectedValue & ") " _
                '    & " AND DATA_FINE IS NULL "
                'par.cmd.ExecuteNonQuery()
                If ris = 1 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "npod", "CloseAndRefresh('Button1')", True)
                Else
                    RadWindowManager1.RadAlert(ris & " POD duplicati con nuovo fornitore", 300, 150, "Duplicazione POD con nuovo fornitore", "CloseAndRefresh('Button1')", Nothing)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "npod", "CloseAndRefresh('Button1')", True)
                End If
            End If
            connData.chiudi(True)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadButtonModifica_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadComboBoxFornitura_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxFornitura.SelectedIndexChanged
        connData.apri()
        caricaFornitoreOld()
        caricaFornitoreNew()
        connData.chiudi()
    End Sub

    Protected Sub RadComboBoxFornitoreOld_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxFornitoreOld.SelectedIndexChanged
        caricaFornitoreNew()
    End Sub
End Class
