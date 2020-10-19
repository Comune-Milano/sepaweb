Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_GestCustodi
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
            HFGriglia.Value = RadGridCustodi.ClientID
            SvuotaCampi(-1)
            caricaAggregazioni()
        End If
    End Sub
    Protected Sub btnDelCustode_Click(sender As Object, e As System.EventArgs) Handles btnDelCustode.Click
        Try
            If IsNumeric(idSel.Value) AndAlso idSel.Value > 0 Then
                connData.apri()
                'controllo pagamenti
                Dim ris As Integer = 0
                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.PAGAMENTI_CUSTODI WHERE ID_PRENOTAZIONE IS NOT NULL AND  ID_CUSTODE = " & idSel.Value
                ris = par.IfNull(par.cmd.ExecuteScalar, 0)
                If ris > 0 Then
                    RadWindowManager1.RadAlert("Custode non eliminabile perchè è stato emesso un pagamento!\nVerrà reso non attivo!", 300, 150, "Attenzione", Nothing, Nothing)
                    par.cmd.CommandText = "update siscom_mi.anagrafica_custodi set fl_eliminato = 1 where id = " & idSel.Value
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi()
                    RadGridCustodi.Rebind()
                    idSel.Value = ""
                    Exit Sub
                End If

                'controllo portierato
                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.PORTIERATO WHERE ID_CUSTODE = " & idSel.Value
                ris = par.IfNull(par.cmd.ExecuteScalar, 0)
                If ris > 0 Then
                    RadWindowManager1.RadAlert("Custode non eliminabile perchè legato al patrimonio!\nVerrà reso non attivo!", 300, 150, "Attenzione", Nothing, Nothing)
                    par.cmd.CommandText = "update siscom_mi.anagrafica_custodi set fl_eliminato = 1 where id = " & idSel.Value
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi()
                    RadGridCustodi.Rebind()
                    idSel.Value = ""
                    Exit Sub
                End If
                par.cmd.CommandText = "delete from siscom_mi.anagrafica_custodi where id = " & idSel.Value
                par.cmd.ExecuteNonQuery()
                RadWindowManager1.RadAlert("Custode eliminato correttamente!", 300, 150, "Attenzione", Nothing, Nothing)
                connData.chiudi()
                RadGridCustodi.Rebind()
                idSel.Value = ""
            End If
            idSel.Value = ""
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnDelCustode_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridCustodi_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadGridCustodi.DetailTableDataBind
        Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        Select Case e.DetailTableView.Name
            Case "Dettagli"
                If True Then
                    par.cmd.CommandText = "SELECT ID,ID_CUSTODE,(SELECT DENOMINAZIONE FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO WHERE AGGREGAZIONE_PORTIERATO.ID=PORTIERATO.ID_AGGREGAZIONE) AS NOME_AGGREGAZIONE," _
                        & " SISCOM_MI.GETDATA(DATA_INIZIO) AS DATA_INIZIO," _
                        & " SISCOM_MI.GETDATA(DATA_FINE) AS DATA_FINE,ID_AGGREGAZIONE,'AGGREGAZIONE' AS TIPO,ID_EDIFICIO,ID_COMPLESSO,DATA_FINE " _
                        & " FROM SISCOM_MI.PORTIERATO WHERE ID_CUSTODE=" & dataItem("id").Text _
                        & " AND ID_AGGREGAZIONE IS NOT NULL " _
                        & " UNION " _
                        & " SELECT ID,ID_CUSTODE,(SELECT DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=PORTIERATO.ID_COMPLESSO) AS NOME_AGGREGAZIONE," _
                        & " SISCOM_MI.GETDATA(DATA_INIZIO) AS DATA_INIZIO," _
                        & " SISCOM_MI.GETDATA(DATA_FINE) AS DATA_FINE,ID_AGGREGAZIONE,'COMPLESSO' AS TIPO,ID_EDIFICIO,ID_COMPLESSO,DATA_FINE " _
                        & " FROM SISCOM_MI.PORTIERATO WHERE ID_CUSTODE=" & dataItem("id").Text _
                        & " AND ID_AGGREGAZIONE IS NULL " _
                        & " AND ID_COMPLESSO IS NOT NULL " _
                        & " AND ID_EDIFICIO IS NULL " _
                        & " UNION " _
                        & " SELECT ID,ID_CUSTODE,(SELECT DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=PORTIERATO.ID_EDIFICIO) AS NOME_AGGREGAZIONE," _
                        & " SISCOM_MI.GETDATA(DATA_INIZIO) AS DATA_INIZIO," _
                        & " SISCOM_MI.GETDATA(DATA_FINE) AS DATA_FINE,ID_AGGREGAZIONE,'EDIFICIO' AS TIPO,ID_EDIFICIO,ID_COMPLESSO,DATA_FINE " _
                        & " FROM SISCOM_MI.PORTIERATO WHERE ID_CUSTODE=" & dataItem("id").Text _
                        & " AND ID_AGGREGAZIONE IS NULL " _
                        & " AND ID_COMPLESSO IS NOT NULL " _
                        & " AND ID_EDIFICIO IS NOT NULL " _
                        & " ORDER BY 10 DESC "
                    e.DetailTableView.DataSource = par.getDataTableGrid(par.cmd.CommandText)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
                End If
        End Select
    End Sub



    Protected Sub dgvCustodi_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridCustodi.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim stringaFlAttivo As String = ""
                If dataItem("ATTIVO").Text = "SI" Then
                    stringaFlAttivo = "document.getElementById('flAttivo').value='1';"
                Else
                    stringaFlAttivo = "document.getElementById('flAttivo').value='0';"
                End If
                e.Item.Attributes.Add("onclick", "document.getElementById('idSel').value='" & dataItem("ID").Text & "';" & stringaFlAttivo)
                e.Item.Attributes.Add("onDblclick", "document.getElementById('btnModPod').click();")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub dgvCustodi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridCustodi.NeedDataSource
        Try
            par.cmd.CommandText = "SELECT ID, MATRICOLA, COGNOME, NOME, FL_DIPENDENTE_MM, " _
                & " EMAIL_AZIENDALE, CELLULARE_AZIENDALE, TELEFONO_AZIENDALE, " _
                & " FL_ALLOGGIO_ERP_ASSEGNATO, NOTE,DECODE (FL_ELIMINATO,0,'SI',1,'NO') AS ATTIVO," _
                & " SISCOM_MI.GETOGGETTOPORTIERATO(ANAGRAFICA_CUSTODI.ID,1) AS SEDE,SISCOM_MI.GETOGGETTOPORTIERATO(ANAGRAFICA_CUSTODI.ID,0) AS VIA " _
                & " FROM SISCOM_MI.ANAGRAFICA_CUSTODI ORDER BY MATRICOLA ASC,COGNOME ASC"
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(par.cmd.CommandText)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " dgvCustodi_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Protected Sub RadButtonDisattiva_Click(sender As Object, e As System.EventArgs) Handles RadButtonDisattiva.Click
        Try
            connData.apri(True)
            par.cmd.CommandText = "UPDATE SISCOM_MI.ANAGRAFICA_CUSTODI SET FL_ELIMINATO=1 WHERE ID=" & idSel.Value
            par.cmd.ExecuteReader()
            connData.chiudi(True)
            RadWindowManager1.RadAlert("Custode disattivato correttamente!", 300, 150, "Attenzione", Nothing, Nothing)
            RadGridCustodi.Rebind()
            idSel.Value = ""
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadButtonDisattiva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub estraiTuttiAttuali()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT MATRICOLA," _
                & " COGNOME," _
                & " NOME," _
                & " EMAIL_AZIENDALE AS ""E-MAIL""," _
                & " CELLULARE_AZIENDALE AS ""CELL. AZIENDALE""," _
                & " TELEFONO_AZIENDALE AS ""TEL. AZIENDALE""," _
                & " NOTE," _
                & " DECODE (FL_ELIMINATO, 0, 'SI', 'NO') AS ATTIVO," _
                & " (CASE " _
                & " WHEN PORTIERATO.ID_EDIFICIO IS NOT NULL THEN 'EDIFICIO'" _
                & " WHEN PORTIERATO.ID_AGGREGAZIONE IS NOT NULL THEN 'AGGREGAZIONE'" _
                & " WHEN PORTIERATO.ID_COMPLESSO IS NOT NULL THEN 'COMPLESSO'" _
                & " ELSE " _
                & " '' " _
                & " END" _
                & " ) AS TIPO," _
                & " (CASE " _
                & " WHEN PORTIERATO.ID_EDIFICIO IS NOT NULL THEN (SELECT COD_EDIFICIO||' - '||DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=ID_EDIFICIO)" _
                & " WHEN PORTIERATO.ID_AGGREGAZIONE IS NOT NULL THEN (SELECT DENOMINAZIONE FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO WHERE AGGREGAZIONE_PORTIERATO.ID=ID_AGGREGAZIONE)" _
                & " WHEN PORTIERATO.ID_COMPLESSO IS NOT NULL THEN (SELECT COD_COMPLESSO||' - '||DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=ID_COMPLESSO)" _
                & " ELSE" _
                & " ''" _
                & " END" _
                & " ) AS DENOMINAZIONE, " _
                & " SISCOM_MI.GETDATA(DATA_INIZIO) AS ""INIZIO"" " _
                & " FROM SISCOM_MI.ANAGRAFICA_CUSTODI, SISCOM_MI.PORTIERATO " _
                & " WHERE ANAGRAFICA_CUSTODI.ID = PORTIERATO.ID_CUSTODE(+) " _
                & " AND PORTIERATO.DATA_FINE IS NULL"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportCustodi", "ExportCustodi", dt)
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                Response.Redirect("../FileTemp/" & nomeFile, False)
            Else
                RadWindowManager1.RadAlert("Errore durante l\'export Excel!", 300, 150, "Attenzione", Nothing, Nothing)
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " estraiTuttiAttuali - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub estraiTutti()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT MATRICOLA," _
                & " COGNOME," _
                & " NOME," _
                & " EMAIL_AZIENDALE AS ""E-MAIL""," _
                & " CELLULARE_AZIENDALE AS ""CELL. AZIENDALE""," _
                & " TELEFONO_AZIENDALE AS ""TEL. AZIENDALE""," _
                & " NOTE," _
                & " DECODE (FL_ELIMINATO, 0, 'SI', 'NO') AS ATTIVO," _
                & " (CASE " _
                & " WHEN PORTIERATO.ID_EDIFICIO IS NOT NULL THEN 'EDIFICIO'" _
                & " WHEN PORTIERATO.ID_AGGREGAZIONE IS NOT NULL THEN 'AGGREGAZIONE'" _
                & " WHEN PORTIERATO.ID_COMPLESSO IS NOT NULL THEN 'COMPLESSO'" _
                & " ELSE " _
                & " '' " _
                & " END" _
                & " ) AS TIPO," _
                & " (CASE " _
                & " WHEN PORTIERATO.ID_EDIFICIO IS NOT NULL THEN (SELECT COD_EDIFICIO||' - '||DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=ID_EDIFICIO)" _
                & " WHEN PORTIERATO.ID_AGGREGAZIONE IS NOT NULL THEN (SELECT DENOMINAZIONE FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO WHERE AGGREGAZIONE_PORTIERATO.ID=ID_AGGREGAZIONE)" _
                & " WHEN PORTIERATO.ID_COMPLESSO IS NOT NULL THEN (SELECT COD_COMPLESSO||' - '||DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=ID_COMPLESSO)" _
                & " ELSE" _
                & " ''" _
                & " END" _
                & " ) AS DENOMINAZIONE, " _
                & " SISCOM_MI.GETDATA(DATA_INIZIO) AS ""INIZIO"", " _
                & " SISCOM_MI.GETDATA(DATA_FINE) AS ""FINE"" " _
                & " FROM SISCOM_MI.ANAGRAFICA_CUSTODI, SISCOM_MI.PORTIERATO " _
                & " WHERE ANAGRAFICA_CUSTODI.ID = PORTIERATO.ID_CUSTODE(+) "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportCustodi", "ExportCustodi", dt)
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                Response.Redirect("../FileTemp/" & nomeFile, False)
            Else
                RadWindowManager1.RadAlert("Errore durante l\'export Excel!", 300, 150, "Attenzione", Nothing, Nothing)
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " estraiTuttiAttuali - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnEstraiTuttiAttuali_Click(sender As Object, e As System.EventArgs) Handles btnEstraiTuttiAttuali.Click
        estraiTuttiAttuali()
    End Sub

    Protected Sub btnEstraiTutti_Click(sender As Object, e As System.EventArgs) Handles btnEstraiTutti.Click
        estraiTutti()
    End Sub

    Private Sub caricaAggregazioni()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT ID, DENOMINAZIONE FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO ORDER BY DENOMINAZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Me.RadGridAggregazioni.DataSource = dt
            Me.RadGridAggregazioni.DataBind()
            idSelAggr.Value = ""
            RadTextBoxSelezioneAggregazione.Text = ""
            txtNomeAggregazione.Text = ""
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " caricaAggregazioni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadButtonSalvaAggregazione_Click(sender As Object, e As System.EventArgs) Handles RadButtonSalvaAggregazione.Click
        Try
            If txtNomeAggregazione.Text <> "" Then
                connData.apri(True)
                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO WHERE upper(DENOMINAZIONE)='" & UCase(txtNomeAggregazione.Text.Replace("'", "''")) & "'"
                Dim ris As Integer = 0
                ris = par.IfNull(par.cmd.ExecuteScalar, 0)
                If ris = 0 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGGREGAZIONE_PORTIERATO (ID,DENOMINAZIONE) VALUES (SISCOM_MI.SEQ_AGGREGAZIONE_PORTIERATO.NEXTVAL,'" & UCase(txtNomeAggregazione.Text.Replace("'", "''")) & "')"
                    par.cmd.ExecuteNonQuery()
                Else
                    RadWindowManager1.RadAlert("Nome aggregazione già utilizzato!", 330, 180, "Attenzione!", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioni');openWindow(sender, args, 'RadWindowAggiungiAggregazione');}", Nothing)
                    connData.chiudi(True)
                    Exit Sub
                End If
                connData.chiudi(True)
                caricaAggregazioni()
                RadWindowManager1.RadAlert("Aggregazione inserita correttamente!", 300, 150, "Attenzione", "function(sender, args){closeWindow(sender, args, 'RadWindowAggiungiAggregazione');openWindow(sender, args, 'RadWindowAggregazioni');}", Nothing)
            Else
                RadWindowManager1.RadAlert("Definire il nome dell\'aggregazione!", 330, 180, "Attenzione!", "function(sender, args){openWindow(sender, args, 'RadWindowAggiungiAggregazione');}", Nothing)
                Exit Sub
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadButtonSalvaAggregazione_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridAggregazioni_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridAggregazioni.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                e.Item.Attributes.Add("onclick", "document.getElementById('idSelAggr').value='" & dataItem("ID").Text & "';document.getElementById('RadTextBoxSelezioneAggregazione').value='Hai selezionato l\'aggregazione " & dataItem("DENOMINAZIONE").Text.Replace("'", "\'") & "';")
                'e.Item.Attributes.Add("onDblclick", "document.getElementById('btnModAggregazione').click();")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RadButtonEliminaAggregazione_Click(sender As Object, e As System.EventArgs) Handles RadButtonEliminaAggregazione.Click
        Try
            If IsNumeric(idSelAggr.Value) AndAlso idSelAggr.Value > 0 Then
                connData.apri(True)
                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.PORTIERATO WHERE ID_AGGREGAZIONE=" & idSelAggr.Value
                Dim ris As Integer = 0
                ris = par.IfNull(par.cmd.ExecuteScalar, 0)
                If ris = 0 Then
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO_dETT WHERE ID_AGGREGAZIONE=" & idSelAggr.Value
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO WHERE ID=" & idSelAggr.Value
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(True)
                    caricaAggregazioni()
                    RadWindowManager1.RadAlert("Aggregazione eliminata correttamente!", 330, 180, "Attenzione!", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioni');}", Nothing)
                Else
                    RadWindowManager1.RadAlert("Aggregazione utilizzata nel portierato! Non è possibile eliminarla!", 330, 180, "Attenzione!", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioni');}", Nothing)
                    connData.chiudi(True)
                    Exit Sub
                End If
            End If
            idSelAggr.Value = ""
            RadTextBoxSelezioneAggregazione.Text = ""
            txtNomeAggregazione.Text = ""
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadButtonElimina_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub



    Private Sub caricaDettaglioAggregazioni(Optional ByVal apriDettaglio As Boolean = True)
        Try
            connData.apri()
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
                & " FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO_DETT WHERE ID_AGGREGAZIONE=" & idSelAggr.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            RadGridAggregazione.DataSource = dt
            RadGridAggregazione.DataBind()
            If apriDettaglio = True Then
                Dim script As String = "function f(){$find(""" + RadWindowAggregazioneDettaglio.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " caricaDettaglioAggregazioni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonSalvaAggregazioneComplesso_Click(sender As Object, e As System.EventArgs) Handles RadButtonSalvaAggregazioneComplesso.Click
        Try
            If RadComboBoxAggregazioneComplesso.SelectedValue <> "-1" AndAlso idSelAggr.Value > "0" Then
                connData.apri()
                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO_DETT WHERE ID_AGGREGAZIONE=" & idSelAggr.Value & " AND ID_COMPLESSO=" & RadComboBoxAggregazioneComplesso.SelectedValue
                Dim ris As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                If ris = 0 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGGREGAZIONE_PORTIERATO_DETT (ID_AGGREGAZIONE,ID_COMPLESSO) VALUES (" & idSelAggr.Value & "," & RadComboBoxAggregazioneComplesso.SelectedValue & ")"
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi()
                Else
                    connData.chiudi()
                    RadWindowManager1.RadAlert("Complesso già inserito!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneDettaglio');openWindow(sender, args, 'RadWindowAggregazioneComplesso');}", Nothing)
                    RadComboBoxAggregazioneComplesso.ClearSelection()
                    Exit Sub
                End If
                caricaDettaglioAggregazioni(False)
                RadWindowManager1.RadAlert("Complesso aggiunto correttamente!", 300, 150, "Attenzione", "function(sender, args){closeWindow(sender, args, 'RadWindowAggregazioneComplesso', '');openWindow(sender, args, 'RadWindowAggregazioneDettaglio');}", Nothing)
            Else
                RadWindowManager1.RadAlert("Selezionare un complesso!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneComplesso');}", Nothing)
            End If
            RadComboBoxAggregazioneComplesso.ClearSelection()
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadButtonSalvaAggregazioneComplesso_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadButtonSalvaAggregazioneEdificio_Click(sender As Object, e As System.EventArgs) Handles RadButtonSalvaAggregazioneEdificio.Click
        If RadComboBoxAggregazioneEdificio.SelectedValue <> "-1" Then
            Try
                If RadComboBoxAggregazioneEdificio.SelectedValue <> "-1" AndAlso idSelAggr.Value > "0" Then
                    connData.apri()
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO_DETT WHERE ID_AGGREGAZIONE=" & idSelAggr.Value & " AND ID_EDIFICIO=" & RadComboBoxAggregazioneEdificio.SelectedValue
                    Dim ris As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                    If ris = 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGGREGAZIONE_PORTIERATO_DETT (ID_AGGREGAZIONE,ID_EDIFICIO) VALUES (" & idSelAggr.Value & "," & RadComboBoxAggregazioneEdificio.SelectedValue & ")"
                        par.cmd.ExecuteNonQuery()
                        connData.chiudi()
                    Else
                        connData.chiudi()
                        RadWindowManager1.RadAlert("Edificio già inserito!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneDettaglio');openWindow(sender, args, 'RadWindowAggregazioneEdificio');}", Nothing)
                        RadComboBoxAggregazioneEdificioComplesso.ClearSelection()
                        RadComboBoxAggregazioneEdificio.ClearSelection()
                        Exit Sub
                    End If
                    caricaDettaglioAggregazioni(False)
                    RadWindowManager1.RadAlert("Edificio aggiunto correttamente!", 300, 150, "Attenzione", "function(sender, args){closeWindow(sender, args, 'RadWindowAggregazioneEdificio', '');openWindow(sender, args, 'RadWindowAggregazioneDettaglio');}", Nothing)
                Else
                    RadWindowManager1.RadAlert("Selezionare un edificio!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneEdificio');}", Nothing)
                End If
                RadComboBoxAggregazioneEdificioComplesso.ClearSelection()
                RadComboBoxAggregazioneEdificio.ClearSelection()
            Catch ex As Exception
                connData.chiudi()
                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", Page.Title & " RadButtonSalvaAggregazioneEdificio_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If
    End Sub
    Protected Sub RadButtonSalvaAggregazioneScala_Click(sender As Object, e As System.EventArgs) Handles RadButtonSalvaAggregazioneScala.Click
        If RadComboBoxAggregazioneScala.SelectedValue <> "-1" Then
            Try
                If RadComboBoxAggregazioneScala.SelectedValue <> "-1" AndAlso idSelAggr.Value > "0" Then
                    connData.apri()
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO_DETT WHERE ID_AGGREGAZIONE=" & idSelAggr.Value & " AND ID_SCALA=" & RadComboBoxAggregazioneScala.SelectedValue
                    Dim ris As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                    If ris = 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGGREGAZIONE_PORTIERATO_DETT (ID_AGGREGAZIONE,ID_SCALA) VALUES (" & idSelAggr.Value & "," & RadComboBoxAggregazioneScala.SelectedValue & ")"
                        par.cmd.ExecuteNonQuery()
                        connData.chiudi()
                    Else
                        connData.chiudi()
                        RadWindowManager1.RadAlert("Scala già inserita!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneDettaglio');openWindow(sender, args, 'RadWindowAggregazioneScala');}", Nothing)
                        RadComboBoxAggregazioneScalaComplesso.ClearSelection()
                        RadComboBoxAggregazioneScalaEdificio.ClearSelection()
                        RadComboBoxAggregazioneScala.ClearSelection()
                        Exit Sub
                    End If
                    caricaDettaglioAggregazioni(False)
                    RadWindowManager1.RadAlert("Scala aggiunta correttamente!", 300, 150, "Attenzione", "function(sender, args){closeWindow(sender, args, 'RadWindowAggregazioneScala', '');openWindow(sender, args, 'RadWindowAggregazioneDettaglio');}", Nothing)
                Else
                    RadWindowManager1.RadAlert("Selezionare una scala!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneScala');}", Nothing)
                End If
                RadComboBoxAggregazioneScalaComplesso.ClearSelection()
                RadComboBoxAggregazioneScalaEdificio.ClearSelection()
                RadComboBoxAggregazioneScala.ClearSelection()
            Catch ex As Exception
                connData.chiudi()
                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", Page.Title & " RadButtonSalvaAggregazioneScala_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If
    End Sub
    Protected Sub RadButtonSalvaAggregazioneUnita_Click(sender As Object, e As System.EventArgs) Handles RadButtonSalvaAggregazioneUnita.Click
        If RadComboBoxAggregazioneUnita.SelectedValue <> "-1" Then
            Try
                If RadComboBoxAggregazioneUnita.SelectedValue <> "-1" AndAlso idSelAggr.Value > "0" Then
                    connData.apri()
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO_DETT WHERE ID_AGGREGAZIONE=" & idSelAggr.Value & " AND ID_UNITA=" & RadComboBoxAggregazioneUnita.SelectedValue
                    Dim ris As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                    If ris = 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGGREGAZIONE_PORTIERATO_DETT (ID_AGGREGAZIONE,ID_UNITA) VALUES (" & idSelAggr.Value & "," & RadComboBoxAggregazioneUnita.SelectedValue & ")"
                        par.cmd.ExecuteNonQuery()
                        connData.chiudi()
                    Else
                        connData.chiudi()
                        RadWindowManager1.RadAlert("Unità immobiliare già inserita!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneDettaglio');openWindow(sender, args, 'RadWindowAggregazioneUnita');}", Nothing)
                        RadComboBoxAggregazioneUnitaComplesso.ClearSelection()
                        RadComboBoxAggregazioneUnitaEdificio.ClearSelection()
                        RadComboBoxAggregazioneUnita.ClearSelection()
                        Exit Sub
                    End If
                    caricaDettaglioAggregazioni(False)
                    RadWindowManager1.RadAlert("Unità immobiliare aggiunta correttamente!", 300, 150, "Attenzione", "function(sender, args){closeWindow(sender, args, 'RadWindowAggregazioneUnita', '');openWindow(sender, args, 'RadWindowAggregazioneDettaglio');}", Nothing)
                Else
                    RadWindowManager1.RadAlert("Selezionare un'unità immobiliare!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneUnita');}", Nothing)
                End If
                RadComboBoxAggregazioneUnitaComplesso.ClearSelection()
                RadComboBoxAggregazioneUnitaEdificio.ClearSelection()
                RadComboBoxAggregazioneUnita.ClearSelection()
            Catch ex As Exception
                connData.chiudi()
                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", Page.Title & " RadButtonSalvaAggregazioneUnita_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If
    End Sub
    Protected Sub RadButtonSalvaAggregazioneImpianto_Click(sender As Object, e As System.EventArgs) Handles RadButtonSalvaAggregazioneImpianto.Click
        If RadComboBoxAggregazioneImpianto.SelectedValue <> "-1" Then
            Try
                If RadComboBoxAggregazioneImpianto.SelectedValue <> "-1" AndAlso idSelAggr.Value > "0" Then
                    connData.apri()
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO_DETT WHERE ID_AGGREGAZIONE=" & idSelAggr.Value & " AND ID_IMPIANTO=" & RadComboBoxAggregazioneImpianto.SelectedValue
                    Dim ris As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                    If ris = 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGGREGAZIONE_PORTIERATO_DETT (ID_AGGREGAZIONE,ID_IMPIANTO) VALUES (" & idSelAggr.Value & "," & RadComboBoxAggregazioneImpianto.SelectedValue & ")"
                        par.cmd.ExecuteNonQuery()
                        connData.chiudi()
                    Else
                        connData.chiudi()
                        RadWindowManager1.RadAlert("Impianto già inserito!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneDettaglio');openWindow(sender, args, 'RadWindowAggregazioneImpianto');}", Nothing)
                        RadComboBoxAggregazioneImpiantoComplesso.ClearSelection()
                        RadComboBoxAggregazioneImpianto.ClearSelection()
                        Exit Sub
                    End If
                    caricaDettaglioAggregazioni(False)
                    RadWindowManager1.RadAlert("Impianto aggiunto correttamente!", 300, 150, "Attenzione", "function(sender, args){closeWindow(sender, args, 'RadWindowAggregazioneImpianto', '');openWindow(sender, args, 'RadWindowAggregazioneDettaglio');}", Nothing)
                Else
                    RadWindowManager1.RadAlert("Selezionare un impianto!", 300, 150, "Attenzione", "function(sender, args){openWindow(sender, args, 'RadWindowAggregazioneImpianto');}", Nothing)
                End If
                RadComboBoxAggregazioneImpiantoComplesso.ClearSelection()
                RadComboBoxAggregazioneImpianto.ClearSelection()
            Catch ex As Exception
                connData.chiudi()
                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", Page.Title & " RadButtonSalvaAggregazioneImpianto_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If
    End Sub
    Protected Sub RadComboBoxAggregazioneEdificioComplesso_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxAggregazioneEdificioComplesso.SelectedIndexChanged
        Try
            Dim condizioneComplesso As String = ""
            If RadComboBoxAggregazioneEdificioComplesso.SelectedValue <> "-1" Then
                condizioneComplesso = " AND ID_COMPLESSO = " & RadComboBoxAggregazioneEdificioComplesso.SelectedValue
            End If
            par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 " & condizioneComplesso & " ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneEdificio, "ID", "NOME", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadComboBoxAggregazioneEdificioComplesso_SelectedIndexChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadComboBoxAggregazioneScalaComplesso_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxAggregazioneScalaComplesso.SelectedIndexChanged
        Try
            Dim condizioneComplesso As String = ""
            If RadComboBoxAggregazioneScalaComplesso.SelectedValue <> "-1" Then
                condizioneComplesso = " AND ID_COMPLESSO = " & RadComboBoxAggregazioneScalaComplesso.SelectedValue
            End If
            par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 " & condizioneComplesso & " ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneScalaEdificio, "ID", "NOME", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadComboBoxAggregazioneScalaComplesso_SelectedIndexChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadComboBoxAggregazioneScalaEdificio_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxAggregazioneScalaEdificio.SelectedIndexChanged
        Try
            Dim condizioneEdificio As String = ""
            If RadComboBoxAggregazioneScalaEdificio.SelectedValue <> "-1" Then
                condizioneEdificio = " AND ID_EDIFICIO = " & RadComboBoxAggregazioneScalaEdificio.SelectedValue
            End If
            par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID<>1 " & condizioneEdificio & " ORDER BY 2", RadComboBoxAggregazioneScala, "ID", "DESCRIZIONE", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadComboBoxAggregazioneScalaEdificio_SelectedIndexChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadComboBoxAggregazioneUnitaComplesso_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxAggregazioneUnitaComplesso.SelectedIndexChanged
        Try
            Dim condizioneComplesso As String = ""
            If RadComboBoxAggregazioneUnitaComplesso.SelectedValue <> "-1" Then
                condizioneComplesso = " AND ID_COMPLESSO = " & RadComboBoxAggregazioneUnitaComplesso.SelectedValue
            End If
            par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 " & condizioneComplesso & " ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneUnitaEdificio, "ID", "NOME", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadComboBoxAggregazioneUnitaComplesso_SelectedIndexChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadComboBoxAggregazioneUnitaEdificio_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxAggregazioneUnitaEdificio.SelectedIndexChanged
        Try
            Dim condizioneEdificio As String = ""
            If RadComboBoxAggregazioneUnitaEdificio.SelectedValue <> "-1" Then
                condizioneEdificio = " WHERE ID_EDIFICIO = " & RadComboBoxAggregazioneUnitaEdificio.SelectedValue
            End If
            par.caricaComboTelerik("SELECT ID,COD_UNITA_IMMOBILIARE AS DESCRIZIONE FROM SISCOM_MI.UNITA_IMMOBILIARI " & condizioneEdificio & " ORDER BY 2", RadComboBoxAggregazioneUnita, "ID", "DESCRIZIONE", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadComboBoxAggregazioneUnitaEdificio_SelectedIndexChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub RadComboBoxAggregazioneImpiantoComplesso_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxAggregazioneImpiantoComplesso.SelectedIndexChanged
        Try
            Dim condizioneComplesso As String = ""
            If RadComboBoxAggregazioneImpiantoComplesso.SelectedValue <> "-1" Then
                condizioneComplesso = " WHERE ID_COMPLESSO = " & RadComboBoxAggregazioneImpiantoComplesso.SelectedValue
            End If
            par.caricaComboTelerik("SELECT ID,COD_IMPIANTO||' - '||DESCRIZIONE AS NOME FROM SISCOM_MI.IMPIANTI " & condizioneComplesso & " ORDER BY 2", RadComboBoxAggregazioneImpianto, "ID", "NOME", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadComboBoxAggregazioneImpiantoComplesso_SelectedIndexChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub SvuotaCampi(ByVal tipo As Integer)
        Select Case tipo
            Case -2
            Case -1
                'Tutti
                par.caricaComboTelerik("SELECT ID, COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneEdificio, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID, COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneScalaEdificio, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID, COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneUnitaEdificio, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneComplesso, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneEdificioComplesso, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneScalaComplesso, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneUnitaComplesso, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneImpiantoComplesso, "ID", "NOME", True)
                RadComboBoxAggregazioneScala.Items.Clear()
                RadComboBoxAggregazioneScala.ClearSelection()
                RadComboBoxAggregazioneUnita.Items.Clear()
                RadComboBoxAggregazioneUnita.ClearSelection()
                RadComboBoxAggregazioneImpianto.Items.Clear()
                RadComboBoxAggregazioneImpianto.ClearSelection()
            Case 3
                'Aggregazione
                par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneEdificio, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneScalaEdificio, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1  ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneUnitaEdificio, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneComplesso, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneEdificioComplesso, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneScalaComplesso, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneUnitaComplesso, "ID", "NOME", True)
                par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME, DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 ORDER BY DENOMINAZIONE", RadComboBoxAggregazioneImpiantoComplesso, "ID", "NOME", True)
                RadComboBoxAggregazioneScala.Items.Clear()
                RadComboBoxAggregazioneScala.ClearSelection()
                RadComboBoxAggregazioneUnita.Items.Clear()
                RadComboBoxAggregazioneUnita.ClearSelection()
                RadComboBoxAggregazioneImpianto.Items.Clear()
                RadComboBoxAggregazioneImpianto.ClearSelection()
        End Select
    End Sub


    Protected Sub btnPortierato_Click(sender As Object, e As System.EventArgs) Handles btnPortierato.Click
        RadGridCustodi.Rebind()
        idSel.Value = ""
    End Sub

    Protected Sub btnModPod_Click(sender As Object, e As System.EventArgs) Handles btnModPod.Click
        RadGridCustodi.Rebind()
        idSel.Value = ""
    End Sub

    Protected Sub RadButtonEsciAgg_Click(sender As Object, e As System.EventArgs) Handles RadButtonEsciAgg.Click
        RadGridCustodi.Rebind()
        idSel.Value = ""
    End Sub

    Protected Sub btnModificaAggregazione_Click(sender As Object, e As System.EventArgs) Handles btnModificaAggregazione.Click
        Try
            If IsNumeric(idSelAggr.Value) AndAlso idSelAggr.Value > 0 Then
                caricaDettaglioAggregazioni()
                'Dim script As String = "function f(){$find(""" + RadWindowAggregazioneDettaglio.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                'RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnModificaAggregazione_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        RadGridCustodi.Rebind()
        idSel.Value = ""
    End Sub

    Private Sub estraiTutteLeAggregazioni()
        Try
            connData.apri()
            par.cmd.CommandText = " SELECT AGGREGAZIONE_PORTIERATO.DENOMINAZIONE AS AGGREGAZIONE, " _
                & " (CASE  " _
                & " WHEN AGGREGAZIONE_PORTIERATO_DETT.ID_EDIFICIO IS NOT NULL THEN 'EDIFICIO' " _
                & " WHEN AGGREGAZIONE_PORTIERATO_DETT.ID_SCALA IS NOT NULL THEN 'SCALA' " _
                & " WHEN AGGREGAZIONE_PORTIERATO_DETT.ID_COMPLESSO IS NOT NULL THEN 'COMPLESSO' " _
                & " WHEN AGGREGAZIONE_PORTIERATO_DETT.ID_UNITA IS NOT NULL THEN 'UNITA' " _
                & " WHEN AGGREGAZIONE_PORTIERATO_DETT.ID_IMPIANTO IS NOT NULL THEN 'IMPIANTO' " _
                & " ELSE  " _
                & " ''  " _
                & " END " _
                & " ) AS TIPO, " _
                & " (CASE  " _
                & " WHEN AGGREGAZIONE_PORTIERATO_DETT.ID_EDIFICIO IS NOT NULL THEN (SELECT COD_EDIFICIO||' - '||DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=ID_EDIFICIO) " _
                & " WHEN AGGREGAZIONE_PORTIERATO_DETT.ID_SCALA IS NOT NULL THEN (SELECT DESCRIZIONE FROM SISCOM_MI.SCALE_eDIFICI WHERE SCALE_EDIFICI.ID=ID_SCALA)||' (COD ED.'||(SELECT DISTINCT COD_eDIFICIO FROM SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID_EDIFICIO=EDIFICI.ID AND SCALE_EDIFICI.ID=ID_sCALA)||')' " _
                & " WHEN AGGREGAZIONE_PORTIERATO_DETT.ID_COMPLESSO IS NOT NULL THEN (SELECT COD_COMPLESSO||' - '||DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=ID_COMPLESSO) " _
                & " WHEN AGGREGAZIONE_PORTIERATO_DETT.ID_UNITA IS NOT NULL THEN (SELECT COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=ID_UNITA) " _
                & " WHEN AGGREGAZIONE_PORTIERATO_DETT.ID_IMPIANTO IS NOT NULL THEN (SELECT COD_IMPIANTO||' - '||DENOMINAZIONE FROM SISCOM_MI.IMPIANTI WHERE IMPIANTI.ID=ID_IMPIANTO) " _
                & " ELSE " _
                & " '' " _
                & " END " _
                & " ) AS DENOMINAZIONE " _
                & "  " _
                & " FROM SISCOM_MI.AGGREGAZIONE_PORTIERATO,SISCOM_MI.AGGREGAZIONE_PORTIERATO_DETT " _
                & " WHERE AGGREGAZIONE_PORTIERATO.ID=AGGREGAZIONE_PORTIERATO_DETT.ID_AGGREGAZIONE " _
                & " ORDER BY 1,2,3 "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportAggregazione", "ExportAggregazione", dt)
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                Response.Redirect("../FileTemp/" & nomeFile, False)
            Else
                RadWindowManager1.RadAlert("Errore durante l\'export Excel!", 300, 150, "Attenzione", Nothing, Nothing)
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " estraiTutteLeAggregazioni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub ButtonEstraiAggregazioni_Click(sender As Object, e As System.EventArgs) Handles ButtonEstraiAggregazioni.Click
        estraiTutteLeAggregazioni()
    End Sub

    Private Sub CICLO_PASSIVO_GestCustodi_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class