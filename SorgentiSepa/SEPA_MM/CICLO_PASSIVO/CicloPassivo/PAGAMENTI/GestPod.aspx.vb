Imports Telerik.Web.UI
Imports System.Data

Partial Class CICLO_PASSIVO_GestPod
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_UTENZE") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("IDPOD")) Then
                idPOD.Value = Request.QueryString("IDPOD")
                CaricaDati()
            End If
        End If
    End Sub
    Private Sub CaricaDati(Optional ByVal tipo As Integer = -1)
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.POD WHERE  ID = " & idPOD.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each r As Data.DataRow In dt.Rows
                    txtContratto.Text = r.Item("CONTRATTO").ToString
                    txtPOD.Text = r.Item("POD").ToString
                    txtDescrizione.Text = r.Item("DESCRIZIONE").ToString
                Next
            End If
            par.cmd.CommandText = "SELECT ID,ID_POD,(SELECT DENOMINAZIONE FROM SISCOM_MI.AGGREGAZIONE_POD WHERE AGGREGAZIONE_POD.ID=PATRIMONIO_POD.ID_AGGREGAZIONE) AS NOME_AGGREGAZIONE," _
                & " TO_DATE(DATA_INIZIO,'YYYYMMDD') AS DATA_INIZIO," _
                & " TO_DATE(DATA_FINE,'YYYYMMDD') AS DATA_FINE,ID_AGGREGAZIONE,'A' AS TIPO,ID_EDIFICIO,ID_COMPLESSO,DATA_FINE " _
                & " FROM SISCOM_MI.PATRIMONIO_POD WHERE ID_POD=" & idPOD.Value _
                & " AND ID_AGGREGAZIONE IS NOT NULL " _
                & " UNION " _
                & " SELECT ID,ID_POD,(SELECT DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=PATRIMONIO_POD.ID_COMPLESSO) AS NOME_AGGREGAZIONE," _
                & " TO_DATE(DATA_INIZIO,'YYYYMMDD') AS DATA_INIZIO," _
                & " TO_DATE(DATA_FINE,'YYYYMMDD') AS DATA_FINE,ID_AGGREGAZIONE,'C' AS TIPO,ID_EDIFICIO,ID_COMPLESSO,DATA_FINE " _
                & " FROM SISCOM_MI.PATRIMONIO_POD WHERE ID_POD=" & idPOD.Value _
                & " AND ID_AGGREGAZIONE IS NULL " _
                & " AND ID_COMPLESSO IS NOT NULL " _
                & " AND ID_EDIFICIO IS NULL " _
                & " UNION " _
                & " SELECT ID,ID_POD,(SELECT DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=PATRIMONIO_POD.ID_EDIFICIO) AS NOME_AGGREGAZIONE," _
                & " TO_DATE(DATA_INIZIO,'YYYYMMDD') AS DATA_INIZIO," _
                & " TO_DATE(DATA_FINE,'YYYYMMDD') AS DATA_FINE,ID_AGGREGAZIONE,'E' AS TIPO,ID_EDIFICIO,ID_COMPLESSO,DATA_FINE " _
                & " FROM SISCOM_MI.PATRIMONIO_POD WHERE ID_POD=" & idPOD.Value _
                & " AND ID_AGGREGAZIONE IS NULL " _
                & " AND ID_COMPLESSO IS NOT NULL " _
                & " AND ID_EDIFICIO IS NOT NULL " _
                & " ORDER BY 10 DESC "
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtGrid As New Data.DataTable
            da.Fill(dtGrid)
            da.Dispose()
            dgvPATRIMONIO.DataSource = dtGrid
            dgvPATRIMONIO.DataBind()
            connData.chiudi()
            SvuotaCampi(tipo)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaDati - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SalvaComplesso()
        Try
            connData.apri()
            Dim inserimentoOK As Boolean = False
            Dim inserimentoConElementoPrecedente As Boolean = False
            Dim idPATRIMONIOPrecedente As Integer = 0
            Dim dataInizioPrecedente As String = ""
            If IsNumeric(RadComboBoxComplesso.SelectedValue) AndAlso CInt(RadComboBoxComplesso.SelectedValue) <> -1 Then
                If IsDate(RadDatePickerDataInizioComplesso.SelectedDate) Then
                    par.cmd.CommandText = "SELECT ID,DATA_INIZIO FROM SISCOM_MI.PATRIMONIO_POD WHERE ID_POD=" & idPOD.Value _
                        & " /*AND ID_COMPLESSO IS NOT NULL " _
                        & " AND ID_EDIFICIO IS NULL " _
                        & " AND ID_AGGREGAZIONE IS NULL*/ " _
                        & " AND ID=(SELECT MAX(ID) FROM SISCOM_MI.PATRIMONIO_POD WHERE ID_POD=" & idPOD.Value _
                        & " /*AND ID_COMPLESSO IS NOT NULL " _
                        & " AND ID_EDIFICIO IS NULL " _
                        & " AND ID_AGGREGAZIONE IS NULL*/ " _
                        & ")"
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        inserimentoConElementoPrecedente = True
                        dataInizioPrecedente = par.IfNull(lettore("DATA_INIZIO"), "")
                        idPATRIMONIOPrecedente = par.IfNull(lettore("ID"), 0)
                    End If
                    lettore.Close()
                    If inserimentoConElementoPrecedente = True Then
                        'Inserimento con record già esistente
                        If CStr(par.FormatoDataDB(RadDatePickerDataInizioComplesso.SelectedDate)) > dataInizioPrecedente Then
                            'Inserimento ok
                            inserimentoOK = True
                        Else
                            'Inserimento non valido perchè inserita data precedente
                            RadWindowManager1.RadAlert("La data inizio deve essere successiva al " & par.FormattaData(dataInizioPrecedente) & "!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowComplesso');}", Nothing)
                            Exit Sub
                        End If
                    Else
                        'Inserimento nuovo
                        inserimentoOK = True
                    End If
                Else
                    RadWindowManager1.RadAlert("La data inserita non è valida!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowComplesso');}", Nothing)
                    Exit Sub
                End If
            Else
                RadWindowManager1.RadAlert("Selezionare un complesso!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowComplesso');}", Nothing)
            End If
            Dim idPATRIMONIOOld As String = "NULL"
            If inserimentoOK = True Then
                If inserimentoConElementoPrecedente = True Then
                    Dim dataFine As Date = CDate(RadDatePickerDataInizioComplesso.SelectedDate).AddDays(-1)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PATRIMONIO_POD SET DATA_FINE='" & par.AggiustaData(dataFine) & "' WHERE ID=" & idPATRIMONIOPrecedente
                    par.cmd.ExecuteNonQuery()
                    idPATRIMONIOOld = CStr(idPATRIMONIOPrecedente)
                End If
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PATRIMONIO_POD.NEXTVAL FROM DUAL"
                Dim idPATRIMONIO As Integer = par.cmd.ExecuteScalar
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PATRIMONIO_POD (" _
                    & " ID_AGGREGAZIONE, CAP, COMUNE, " _
                    & " INDIRIZZO, ID_POD, ID_EDIFICIO, " _
                    & " ID_COMPLESSO, ID,DATA_INIZIO,DATA_FINE,ID_OLD) " _
                    & " VALUES (NULL," _
                    & " NULL," _
                    & " NULL," _
                    & " NULL," _
                    & idPOD.Value & "," _
                    & " NULL," _
                    & RadComboBoxComplesso.SelectedValue & "," _
                    & idPATRIMONIO & "," _
                    & "'" & par.FormatoDataDB(RadDatePickerDataInizioComplesso.SelectedDate) & "'," _
                    & " NULL," _
                    & idPATRIMONIOOld _
                    & ")"
                par.cmd.ExecuteNonQuery()
                RadWindowManager1.RadAlert("Complesso aggiunto correttamente!", 300, 150, "Attenzione", "function(sender, args){closeWindow(sender, args, 'RadWindowComplesso', '');}", Nothing)
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " SalvaComplesso - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub dgvPATRIMONIO_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles dgvPATRIMONIO.DetailTableDataBind
        Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        Select Case e.DetailTableView.Name
            Case "Dettagli"
                Select Case dataItem("TIPO").Text
                    Case "A"
                        par.cmd.CommandText = "SELECT (" _
                            & " CASE WHEN ID_COMPLESSO IS NOT NULL THEN 'COMPLESSO' " _
                            & " WHEN ID_EDIFICIO IS NOT NULL THEN 'EDIFICIO' " _
                            & " WHEN ID_SCALA IS NOT NULL THEN 'SCALA' " _
                            & " WHEN ID_IMPIANTO IS NOT NULL THEN 'IMPIANTO' " _
                            & " WHEN ID_UNITA IS NOT NULL THEN 'UNITA'' IMMOBILIARE' " _
                            & " END) AS TIPO, " _
                            & "(" _
                            & " CASE WHEN ID_COMPLESSO IS NOT NULL THEN (SELECT COD_COMPLESSO||'-'||DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=ID_COMPLESSO) " _
                            & " WHEN ID_EDIFICIO IS NOT NULL THEN (SELECT COD_EDIFICIO||'-'||DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=ID_EDIFICIO) " _
                            & " WHEN ID_SCALA IS NOT NULL THEN (SELECT DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID=ID_SCALA) " _
                            & " WHEN ID_IMPIANTO IS NOT NULL THEN (SELECT COD_IMPIANTO||'-'||DESCRIZIONE FROM SISCOM_MI.IMPIANTI WHERE IMPIANTI.ID=ID_IMPIANTO) " _
                            & " WHEN ID_UNITA IS NOT NULL THEN (SELECT COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=ID_UNITA) " _
                            & " END) AS DENOMINAZIONE " _
                            & " FROM SISCOM_MI.AGGREGAZIONE_POD_DETT WHERE ID_AGGREGAZIONE=" & dataItem("ID_AGGREGAZIONE").Text
                        e.DetailTableView.DataSource = par.getDataTableGrid(par.cmd.CommandText)
                    Case "C"
                        par.cmd.CommandText = "SELECT 'COMPLESSO' AS TIPO, COD_COMPLESSO||'-'||DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID=0"
                        e.DetailTableView.DataSource = par.getDataTableGrid(par.cmd.CommandText)
                    Case "E"
                        par.cmd.CommandText = "SELECT 'EDIFICIO' AS TIPO, COD_EDIFICIO||'-'||DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID=0"
                        e.DetailTableView.DataSource = par.getDataTableGrid(par.cmd.CommandText)
                End Select

            Case Else
                e.DetailTableView.DataSource = Nothing
        End Select
        ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
    Protected Sub dgvPATRIMONIO_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvPATRIMONIO.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            'verificare come non far passare il databound ai record figli
            Try
                e.Item.Attributes.Add("onclick", "document.getElementById('RadTextBoxSelezione').value='Hai selezionato l\'elemento " & dataItem("NOME_AGGREGAZIONE").Text & "';document.getElementById('idSelezionato').value='" & dataItem("ID").Text & "';document.getElementById('dataFine').value='" & dataItem("DATA_FINE").Text & "';document.getElementById('tipo').value='" & dataItem("TIPO").Text & "';")
            Catch ex As Exception
            End Try
            'e.Item.Attributes.Add("onDblclick", "Apri();")
        End If
    End Sub
    Protected Sub dgvPATRIMONIO_PreRender(sender As Object, e As System.EventArgs) Handles dgvPATRIMONIO.PreRender
        For Each dataItem As GridDataItem In dgvPATRIMONIO.Items
            If dataItem("TIPO").Text <> "A" Then
                dataItem.Cells(0).Text = "&nbsp;"
            End If
        Next
    End Sub
    Protected Sub RadButtonSalvaComplesso_Click(sender As Object, e As System.EventArgs) Handles RadButtonSalvaComplesso.Click
        Try
            If Not IsDate(RadDatePickerDataInizioComplesso.SelectedDate) Then
                RadWindowManager1.RadAlert("Definire la data inizio!", 330, 180, "Attenzione!", "function(sender, args){openWindow(sender, args, 'RadWindowComplesso');}", Nothing)
                Exit Sub
            End If
            SalvaComplesso()
            CaricaDati(0)
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadButtonSalvaComplesso_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonSalvaEdificio_Click(sender As Object, e As System.EventArgs) Handles RadButtonSalvaEdificio.Click
        Try
            If Not IsDate(RadDatePickerDataInizioEdificio.SelectedDate) Then
                RadWindowManager1.RadAlert("Definire la data inizio!", 330, 180, "Attenzione!", "function(sender, args){openWindow(sender, args, 'RadWindowEdificio');}", Nothing)
                Exit Sub
            End If
            SalvaEdificio()
            CaricaDati(1)
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadButtonSalvaEdificio_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SalvaEdificio()
        Try
            connData.apri()
            Dim inserimentoOK As Boolean = False
            Dim inserimentoConElementoPrecedente As Boolean = False
            Dim idPATRIMONIOPrecedente As Integer = 0
            Dim dataInizioPrecedente As String = ""
            If IsNumeric(RadComboBoxEdificio.SelectedValue) AndAlso CInt(RadComboBoxEdificio.SelectedValue) <> -1 Then
                If IsDate(RadDatePickerDataInizioEdificio.SelectedDate) Then
                    par.cmd.CommandText = "SELECT ID,DATA_INIZIO FROM SISCOM_MI.PATRIMONIO_POD WHERE ID_POD=" & idPOD.Value _
                        & " /*AND ID_COMPLESSO IS NOT NULL " _
                        & " AND ID_EDIFICIO IS NOT NULL " _
                        & " AND ID_AGGREGAZIONE IS NULL*/ " _
                        & " AND ID=(SELECT MAX(ID) FROM SISCOM_MI.PATRIMONIO_POD WHERE ID_POD=" & idPOD.Value _
                        & " /*AND ID_COMPLESSO IS NOT NULL " _
                        & " AND ID_EDIFICIO IS NOT NULL " _
                        & " AND ID_AGGREGAZIONE IS NULL*/ " _
                        & ")"
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        inserimentoConElementoPrecedente = True
                        dataInizioPrecedente = par.IfNull(lettore("DATA_INIZIO"), "")
                        idPATRIMONIOPrecedente = par.IfNull(lettore("ID"), 0)
                    End If
                    lettore.Close()
                    If inserimentoConElementoPrecedente = True Then
                        'Inserimento con record già esistente
                        If CStr(par.FormatoDataDB(RadDatePickerDataInizioEdificio.SelectedDate)) > dataInizioPrecedente Then
                            'Inserimento ok
                            inserimentoOK = True
                        Else
                            'Inserimento non valido perchè inserita data precedente
                            RadWindowManager1.RadAlert("La data inizio deve essere successiva al " & par.FormattaData(dataInizioPrecedente) & "!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowEdificio');}", Nothing)
                        End If
                    Else
                        'Inserimento nuovo
                        inserimentoOK = True
                    End If
                Else
                    RadWindowManager1.RadAlert("La data inserita non è valida!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowEdificio');}", Nothing)
                End If
            Else
                RadWindowManager1.RadAlert("Selezionare un edificio!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowEdificio');}", Nothing)
            End If
            Dim idPATRIMONIOOld As String = "NULL"
            If inserimentoOK = True Then
                If inserimentoConElementoPrecedente = True Then
                    Dim dataFine As Date = CDate(RadDatePickerDataInizioEdificio.SelectedDate).AddDays(-1)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PATRIMONIO_POD SET DATA_FINE='" & par.AggiustaData(dataFine) & "' WHERE ID=" & idPATRIMONIOPrecedente
                    par.cmd.ExecuteNonQuery()
                    idPATRIMONIOOld = CStr(idPATRIMONIOPrecedente)
                End If
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PATRIMONIO_POD.NEXTVAL FROM DUAL"
                Dim idPATRIMONIO As Integer = par.cmd.ExecuteScalar
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PATRIMONIO_POD (" _
                    & " ID_AGGREGAZIONE, CAP, COMUNE, " _
                    & " INDIRIZZO, ID_POD, ID_EDIFICIO, " _
                    & " ID_COMPLESSO, ID,DATA_INIZIO,DATA_FINE,ID_OLD) " _
                    & " VALUES (NULL," _
                    & " NULL," _
                    & " NULL," _
                    & " NULL," _
                    & idPOD.Value & "," _
                    & RadComboBoxEdificio.SelectedValue & "," _
                    & "(SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID=" & RadComboBoxEdificio.SelectedValue & ")," _
                    & idPATRIMONIO & "," _
                    & "'" & par.FormatoDataDB(RadDatePickerDataInizioEdificio.SelectedDate) & "'" _
                    & ",NULL," _
                    & idPATRIMONIOOld _
                    & ")"
                par.cmd.ExecuteNonQuery()
                RadWindowManager1.RadAlert("Edificio aggiunto correttamente!", 300, 150, "Attenzione", "function(sender, args){closeWindow(sender, args, 'RadWindowEdificio', '');}", Nothing)
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " SalvaEdificio - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonElimina_Click(sender As Object, e As System.EventArgs) Handles RadButtonElimina.Click
        Try
            If IsNumeric(idSelezionato.Value) AndAlso idSelezionato.Value > 0 Then
                If par.IfEmpty(dataFine.Value.Replace("&nbsp;", ""), "0") = "0" Then
                    connData.apri()
                    Select Case tipo.Value
                        Case "E"
                            par.cmd.CommandText = "UPDATE SISCOM_MI.PATRIMONIO_POD SET DATA_FINE=NULL WHERE ID=(SELECT ID_OLD FROM SISCOM_MI.PATRIMONIO_POD WHERE ID=" & idSelezionato.Value & ")"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.PATRIMONIO_POD WHERE ID=" & idSelezionato.Value
                            par.cmd.ExecuteNonQuery()
                            RadWindowManager1.RadAlert("Edificio eliminato correttamente!", 300, 150, "Attenzione", Nothing, Nothing)
                        Case "C"
                            par.cmd.CommandText = "UPDATE SISCOM_MI.PATRIMONIO_POD SET DATA_FINE=NULL WHERE ID=(SELECT ID_OLD FROM SISCOM_MI.PATRIMONIO_POD WHERE ID=" & idSelezionato.Value & ")"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.PATRIMONIO_POD WHERE ID=" & idSelezionato.Value
                            par.cmd.ExecuteNonQuery()
                            RadWindowManager1.RadAlert("Complesso immobiliare eliminato correttamente!", 300, 150, "Attenzione", Nothing, Nothing)
                        Case "A"
                            par.cmd.CommandText = "UPDATE SISCOM_MI.PATRIMONIO_POD SET DATA_FINE=NULL WHERE ID=(SELECT ID_OLD FROM SISCOM_MI.PATRIMONIO_POD WHERE ID=" & idSelezionato.Value & ")"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.AGGREGAZIONE_POD_DETT WHERE ID_AGGREGAZIONE IN (SELECT ID_AGGREGAZIONE FROM SISCOM_MI.PATRIMONIO_POD WHERE ID=" & idSelezionato.Value & ")"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.PATRIMONIO_POD WHERE ID=" & idSelezionato.Value
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.AGGREGAZIONE_POD WHERE ID IN (SELECT ID_AGGREGAZIONE FROM SISCOM_MI.PATRIMONIO_POD WHERE ID=" & idSelezionato.Value & ")"
                            par.cmd.ExecuteNonQuery()
                            RadWindowManager1.RadAlert("Aggregazione eliminata correttamente!", 300, 150, "Attenzione", Nothing, Nothing)
                    End Select
                    connData.chiudi()
                Else
                    RadWindowManager1.RadAlert("Non è possibile cancellare un elemento con data fine", 300, 150, "Attenzione", Nothing, Nothing)
                End If
            End If
            idSelezionato.Value = ""
            CaricaDati(-2)
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadButtonElimina_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonSalvaAggregazione_Click(sender As Object, e As System.EventArgs) Handles RadButtonSalvaAggregazione.Click
        Try
            If Not IsDate(RadDatePickerDataInizioAggregazione.SelectedDate) Then
                RadWindowManager1.RadAlert("Definire la data inizio!", 330, 180, "Attenzione!", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazione');}", Nothing)
                Exit Sub
            End If
            SalvaAggregazione()
            CaricaDati(2)
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadButtonSalvaAggregazione_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SalvaAggregazione()
        Try
            connData.apri()
            Dim inserimentoOK As Boolean = False
            Dim inserimentoConElementoPrecedente As Boolean = False
            Dim idPATRIMONIOPrecedente As Integer = 0
            Dim dataInizioPrecedente As String = ""
            If IsNumeric(RadComboBoxAggregazione.SelectedValue) AndAlso CInt(RadComboBoxAggregazione.SelectedValue) <> -1 Then
                If RadComboBoxAggregazione.SelectedValue <> "" Then
                    par.cmd.CommandText = "SELECT ID,DATA_INIZIO FROM SISCOM_MI.PATRIMONIO_POD WHERE ID_POD=" & idPOD.Value _
                        & " /*AND ID_COMPLESSO IS NOT NULL " _
                        & " AND ID_EDIFICIO IS NULL " _
                        & " AND ID_AGGREGAZIONE IS NULL*/ " _
                        & " AND ID=(SELECT MAX(ID) FROM SISCOM_MI.PATRIMONIO_POD WHERE ID_POD=" & idPOD.Value _
                        & " /*AND ID_COMPLESSO IS NOT NULL " _
                        & " AND ID_EDIFICIO IS NULL " _
                        & " AND ID_AGGREGAZIONE IS NULL*/ " _
                        & ")"
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        inserimentoConElementoPrecedente = True
                        dataInizioPrecedente = par.IfNull(lettore("DATA_INIZIO"), "")
                        idPATRIMONIOPrecedente = par.IfNull(lettore("ID"), 0)
                    End If
                    lettore.Close()
                    If inserimentoConElementoPrecedente = True Then
                        'Inserimento con record già esistente
                        If CStr(par.FormatoDataDB(RadDatePickerDataInizioAggregazione.SelectedDate)) > dataInizioPrecedente Then
                            'Inserimento ok
                            inserimentoOK = True
                        Else
                            'Inserimento non valido perchè inserita data precedente
                            RadWindowManager1.RadAlert("La data inizio deve essere successiva al " & par.FormattaData(dataInizioPrecedente) & "!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazione');}", Nothing)
                            Exit Sub
                        End If
                    Else
                        'Inserimento nuovo
                        inserimentoOK = True
                    End If
                Else
                    RadWindowManager1.RadAlert("Selezionare l\'aggregazione!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazione');}", Nothing)
                End If
            Else
                RadWindowManager1.RadAlert("Selezionare un\'aggregazione!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazione');}", Nothing)
            End If
            Dim idPATRIMONIOOld As String = "NULL"
            Dim idAggr As Integer = 0
            If inserimentoOK = True Then
                'par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_AGGREGAZIONE_POD.NEXTVAL FROM DUAL"
                'idAggr = par.cmd.ExecuteScalar()
                If inserimentoConElementoPrecedente = True Then
                    Dim dataFine As Date = CDate(RadDatePickerDataInizioAggregazione.SelectedDate).AddDays(-1)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PATRIMONIO_POD SET DATA_FINE='" & par.AggiustaData(dataFine) & "' WHERE ID=" & idPATRIMONIOPrecedente
                    par.cmd.ExecuteNonQuery()
                    idPATRIMONIOOld = CStr(idPATRIMONIOPrecedente)
                End If
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PATRIMONIO_POD.NEXTVAL FROM DUAL"
                Dim idPATRIMONIO As Integer = par.cmd.ExecuteScalar
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PATRIMONIO_POD (" _
                    & " ID_AGGREGAZIONE, CAP, COMUNE, " _
                    & " INDIRIZZO, ID_POD, ID_EDIFICIO, " _
                    & " ID_COMPLESSO, ID,DATA_INIZIO,DATA_FINE,ID_OLD,NOME_AGGREGAZIONE) " _
                    & " VALUES (" & RadComboBoxAggregazione.SelectedValue & "," _
                    & " NULL," _
                    & " NULL," _
                    & " NULL," _
                    & idPOD.Value & "," _
                    & " NULL," _
                    & " NULL," _
                    & idPATRIMONIO & "," _
                    & "'" & par.FormatoDataDB(RadDatePickerDataInizioAggregazione.SelectedDate) & "'," _
                    & " NULL," _
                    & idPATRIMONIOOld _
                    & ",'" & RadComboBoxAggregazione.SelectedValue & "')"
                par.cmd.ExecuteNonQuery()
                idAggregazione.Value = idAggr
                RadWindowManager1.RadAlert("Aggregazione aggiunta correttamente!", 300, 150, "Attenzione", "function(sender, args){closeWindow(sender, args, 'RadWindowAggregazione');}", Nothing)
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " SalvaAggregazione - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub SvuotaCampi(ByVal tipo As Integer)
        Select Case tipo
            Case -2
            Case -1
                'Tutti
                par.caricaComboTelerik("SELECT ID, COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxEdificio, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID, COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneEdificio, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID, COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneScalaEdificio, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID, COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneUnitaEdificio, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxComplesso, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneComplesso, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneEdificioComplesso, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneScalaComplesso, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneUnitaComplesso, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneImpiantoComplesso, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.AGGREGAZIONE_POD ORDER BY DENOMINAZIONE", RadComboBoxAggregazione, "ID", "NOME", True)
                RadDatePickerDataInizioAggregazione.Clear()
                RadDatePickerDataInizioComplesso.Clear()
                RadDatePickerDataInizioEdificio.Clear()
                'RadTextBoxNomeAggregazione.Text = ""
                'RadComboBoxAggregazioneScala.Items.Clear()
                'RadComboBoxAggregazioneScala.ClearSelection()
                'RadComboBoxAggregazioneUnita.Items.Clear()
                'RadComboBoxAggregazioneUnita.ClearSelection()
                'RadComboBoxAggregazioneImpianto.Items.Clear()
                'RadComboBoxAggregazioneImpianto.ClearSelection()
            Case 0
                'Salva complesso
                par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxComplesso, "ID", "NOME", True)
                RadDatePickerDataInizioComplesso.Clear()
            Case 1
                'Salva edificio
                par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxEdificio, "ID", "NOME", True)
                RadDatePickerDataInizioEdificio.Clear()
            Case 2
                'Salva aggregazione
                par.caricaComboTelerik("SELECT ID,DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.AGGREGAZIONE_POD ORDER BY DENOMINAZIONE", RadComboBoxAggregazione, "ID", "NOME", True)
                RadDatePickerDataInizioAggregazione.Clear()
            Case 3
                'Aggregazione
                'par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneEdificio, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneScalaEdificio, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneUnitaEdificio, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneComplesso, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneEdificioComplesso, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneScalaComplesso, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneUnitaComplesso, "ID", "NOME", True)
                'par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneImpiantoComplesso, "ID", "NOME", True)
                'RadTextBoxNomeAggregazione.Text = ""
                'RadComboBoxAggregazioneScala.Items.Clear()
                'RadComboBoxAggregazioneScala.ClearSelection()
                'RadComboBoxAggregazioneUnita.Items.Clear()
                'RadComboBoxAggregazioneUnita.ClearSelection()
                'RadComboBoxAggregazioneImpianto.Items.Clear()
                'RadComboBoxAggregazioneImpianto.ClearSelection()
        End Select
    End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        dgvPATRIMONIO.AllowPaging = False
        dgvPATRIMONIO.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In dgvPATRIMONIO.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In dgvPATRIMONIO.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In dgvPATRIMONIO.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In dgvPATRIMONIO.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        dgvPATRIMONIO.AllowPaging = True
        dgvPATRIMONIO.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        dgvPATRIMONIO.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "MULTE", "MULTE", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub
End Class