Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_RicRptAnCondomini
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaAmministratori()
            CaricaCondomini()
            CaricaIndirizzi()

        End If

    End Sub
    Private Sub CaricaAmministratori()
        Try


            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select id, (cognome || ' ' || nome ) as amministratore from siscom_mi.cond_amministratori order by cognome asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.chkAmministratori.DataSource = dt
            Me.chkAmministratori.DataBind()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza: CaricaAmministratori " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub
    Private Sub CaricaCondomini()
        Try


            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select id, denominazione from siscom_mi.condomini order by denominazione asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.chkCondomini.DataSource = dt
            Me.chkCondomini.DataBind()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza: CaricaCondomini " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub
    Private Sub CaricaIndirizzi()
        Try



            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT ID, (descrizione||' '||civico||', '||cap||' '||localita) AS descIndirizzo " _
                                & "FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT(ID_INDIRIZZO_PRINCIPALE) " _
                                & "FROM SISCOM_MI.EDIFICI WHERE ID IN (SELECT DISTINCT(ID_EDIFICIO) " _
                                & "FROM SISCOM_MI.COND_EDIFICI)) ORDER BY descrizione, civico ASC "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.chkIndirizzi.DataSource = dt
            Me.chkIndirizzi.DataBind()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza: CaricaIndirizzi " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub
    Protected Sub btnSelAmm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelAmm.Click
        If SelAmminist.Value = 0 Then
            For Each i As ListItem In chkAmministratori.Items
                i.Selected = True
            Next
            SelAmminist.Value = 1
        Else
            For Each i As ListItem In chkAmministratori.Items
                i.Selected = False
            Next
            SelAmminist.Value = 0
        End If
        CaricaCheckAmministratori()
    End Sub
    Protected Sub btnSelCondomini_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelCondomini.Click
        If SelCondomini.Value = 0 Then
            For Each i As ListItem In chkCondomini.Items
                i.Selected = True
            Next
            SelCondomini.Value = 1
        Else
            For Each i As ListItem In chkCondomini.Items
                i.Selected = False
            Next
            SelCondomini.Value = 0
        End If
    End Sub
    Protected Sub btnSelIndirizzi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelIndirizzi.Click
        If SelIndirizzi.Value = 0 Then
            For Each i As ListItem In chkIndirizzi.Items
                i.Selected = True
            Next
            SelIndirizzi.Value = 1
        Else
            For Each i As ListItem In chkIndirizzi.Items
                i.Selected = False
            Next
            SelIndirizzi.Value = 0
        End If
        CaricaCheckIndirizzi()
    End Sub
    Protected Sub btnExportXls_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportXls.Click
        Try
            Dim ammSelezionati As String = ""
            Dim CondSelezionati As String = ""
            Dim indSelezionati As String = ""
            For Each i As ListItem In chkAmministratori.Items
                If i.Selected = True Then
                    ammSelezionati += i.Value & ","
                End If
            Next
            For Each i As ListItem In chkCondomini.Items
                If i.Selected = True Then
                    CondSelezionati += i.Value & ","
                End If
            Next
            For Each i As ListItem In chkIndirizzi.Items
                If i.Selected = True Then
                    indSelezionati += i.Value & ","
                End If
            Next
            If ammSelezionati = "" And CondSelezionati = "" And indSelezionati = "" Then
                Response.Write("<script>alert('Selezionare almeno un criterio per effettuare il report')</script>")
                Exit Sub
            End If
            If ammSelezionati <> "" Then
                ammSelezionati = ammSelezionati.Substring(0, ammSelezionati.LastIndexOf(","))
                ammSelezionati = " AND COND_AMMINISTRATORI.ID IN  (" & ammSelezionati & ") "
            End If
            If CondSelezionati <> "" Then
                CondSelezionati = CondSelezionati.Substring(0, CondSelezionati.LastIndexOf(","))
                CondSelezionati = " AND CONDOMINI.ID IN (" & CondSelezionati & ") "
            End If
            If indSelezionati <> "" Then
                indSelezionati = indSelezionati.Substring(0, indSelezionati.LastIndexOf(","))
                indSelezionati = "  AND CONDOMINI.ID IN (SELECT DISTINCT(ID_CONDOMINIO) FROM SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO IN (SELECT DISTINCT ID FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID_INDIRIZZO_PRINCIPALE IN (" & indSelezionati & ") ))"
            End If
            '*******************APERURA CONNESSIONE*********************182
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT TO_CHAR (condomini.ID, '00000') AS cod_condominio," _
                                & "condomini.denominazione AS condominio, COMUNI_NAZIONI.nome AS citta," _
                                & "(cond_amministratori.cognome || ' ' || cond_amministratori.nome) AS amminist," _
                                & "TO_CHAR(TO_DATE(data_costituzione,'yyyymmdd'),'dd/mm/yyyy')AS data_costituzione, " _
                                & "(CASE WHEN tipo_gestione = 'D' THEN 'DIRETTA' WHEN TIPO_GESTIONE = 'I' THEN 'INDIRETTA' ELSE '' END) AS TIPO_GESTIONE , " _
                                & "(SUBSTR(gestione_inizio,3,2)||'/'||SUBSTR(gestione_inizio,1,2)||' - '|| SUBSTR(gestione_fine,3,2)||'/'||SUBSTR(gestione_fine,1,2)) AS GESTIONE," _
                                & "(CASE WHEN fornitori.ragione_sociale IS NULL THEN (fornitori.cognome ||' ' || fornitori.nome) ELSE fornitori.ragione_sociale END)AS fornitore," _
                                & "fornitori.cod_fiscale," _
                                & "(CASE WHEN tipologia = 'S' THEN 'SUPER CONDOMINIO' WHEN TIPOLOGIA='C' THEN 'CONDOMINO' WHEN TIPOLOGIA='T' THEN 'TERMICA' ELSE '' END) AS TIPOLOGIA, " _
                                & "MIL_PRES_ASS_TOT_COND,condomini.iban,CONDOMINI.note " _
                                & "FROM SISCOM_MI.CONDOMINI,SISCOM_MI.COND_AMMINISTRATORI, " _
                                & "SISCOM_MI.COND_AMMINISTRAZIONE,SEPA.COMUNI_NAZIONI,siscom_mi.fornitori " _
                                & "WHERE COMUNI_NAZIONI.COD(+) = CONDOMINI.COD_COMUNE " _
                                & "AND COND_AMMINISTRATORI.ID(+) = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE " _
                                & "AND COND_AMMINISTRAZIONE.ID_CONDOMINIO(+) = CONDOMINI.ID AND fornitori.ID(+) = condomini.id_fornitore " _
                                & "" & ammSelezionati _
                                & "" & CondSelezionati _
                                & "" & indSelezionati _
                                & "AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL " _
                                & "ORDER BY CONDOMINI.DENOMINAZIONE ASC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.dgvExport.Visible = True
            Me.dgvExport.DataSource = dt
            Me.dgvExport.DataBind()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Esporta()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: CaricaIndirizzi " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub Esporta()
        Try
            Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(Me.dgvExport, "ExportCondominiAmministrati", , , , False)
            If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                Response.Redirect("..\/FileTemp/" & nomefile, False)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
            Me.dgvExport.Visible = False

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: btnExp " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub chkAmministratori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAmministratori.SelectedIndexChanged
        CaricaCheckAmministratori()
    End Sub
    Protected Sub chkIndirizzi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIndirizzi.SelectedIndexChanged
        CaricaCheckIndirizzi()
    End Sub
    Private Sub CaricaCheckAmministratori()
        Try
            chkCondomini.Items.Clear()
            chkIndirizzi.Items.Clear()
            Dim StringaCheckAmministratori As String = ""
            For Each Items As ListItem In chkAmministratori.Items
                If Items.Selected = True Then
                    StringaCheckAmministratori = StringaCheckAmministratori & Items.Value & ","
                End If
            Next
            If StringaCheckAmministratori <> "" Then
                StringaCheckAmministratori = Left(StringaCheckAmministratori, Len(StringaCheckAmministratori) - 1)
                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "select id, denominazione from siscom_mi.condomini " _
                                    & "WHERE SISCOM_MI.CONDOMINI.ID IN (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & StringaCheckAmministratori & ") and siscom_mi.cond_amministrazione.DATA_FINE is null) " _
                                    & "order by denominazione asc"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                Me.chkCondomini.DataSource = dt
                Me.chkCondomini.DataBind()
                Dim StringaCheckCondomini As String = ""
                For Each Items As ListItem In chkCondomini.Items
                    StringaCheckCondomini = StringaCheckCondomini & Items.Value & ","
                Next
                If StringaCheckCondomini <> "" Then
                    StringaCheckCondomini = Left(StringaCheckCondomini, Len(StringaCheckCondomini) - 1)
                    par.cmd.CommandText = "SELECT ID, (descrizione||' '||civico||', '||cap||' '||localita) AS descIndirizzo " _
                                        & "FROM siscom_mi.indirizzi " _
                                        & "WHERE ID IN (SELECT DISTINCT(ID_INDIRIZZO_PRINCIPALE) FROM SISCOM_MI.EDIFICI WHERE siscom_mi.edifici.ID in (select id_edificio from siscom_mi.cond_edifici where siscom_mi.cond_edifici.id_condominio in (" & StringaCheckCondomini & "))) " _
                                        & "ORDER BY descrizione, civico ASC"
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    dt = New Data.DataTable
                    da.Fill(dt)
                    Me.chkIndirizzi.DataSource = dt
                    Me.chkIndirizzi.DataBind()
                End If
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                CaricaAmministratori()
                CaricaCondomini()
                CaricaIndirizzi()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: CaricaCheckAmministratori " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub CaricaCheckIndirizzi()
        Dim StringaCheckAmministratori As String = ""
        Try
            chkCondomini.Items.Clear()
            Dim StringaCheckIndirizzi As String = ""
            For Each Items As ListItem In chkIndirizzi.Items
                If Items.Selected = True Then
                    StringaCheckIndirizzi = StringaCheckIndirizzi & Items.Value & ","
                End If
            Next
            If StringaCheckIndirizzi <> "" Then
                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                StringaCheckIndirizzi = Left(StringaCheckIndirizzi, Len(StringaCheckIndirizzi) - 1)
                For Each Items As ListItem In chkAmministratori.Items
                    If Items.Selected = True Then
                        StringaCheckAmministratori = StringaCheckAmministratori & Items.Value & ","
                    End If
                Next
                If StringaCheckAmministratori <> "" Then
                    StringaCheckAmministratori = Left(StringaCheckAmministratori, Len(StringaCheckAmministratori) - 1)
                    par.cmd.CommandText = "select id, denominazione " _
                                        & "from siscom_mi.condomini " _
                                        & "where id in (select distinct id_condominio from siscom_mi.cond_edifici where siscom_mi.cond_edifici.ID_EDIFICIO in (select id from siscom_mi.edifici where siscom_mi.edifici.id_indirizzo_principale in (" & StringaCheckIndirizzi & "))) " _
                                        & "and SISCOM_MI.CONDOMINI.ID IN (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & StringaCheckAmministratori & ") and siscom_mi.cond_amministrazione.DATA_FINE is null) " _
                                        & "order by denominazione asc"
                Else
                    par.cmd.CommandText = "select id, denominazione " _
                                        & "from siscom_mi.condomini " _
                                        & "where id in (select distinct id_condominio from siscom_mi.cond_edifici where siscom_mi.cond_edifici.ID_EDIFICIO in (select id from siscom_mi.edifici where siscom_mi.edifici.id_indirizzo_principale in (" & StringaCheckIndirizzi & "))) " _
                                        & "order by denominazione asc"
                End If
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                Me.chkCondomini.DataSource = dt
                Me.chkCondomini.DataBind()
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                For Each Items As ListItem In chkAmministratori.Items
                    If Items.Selected = True Then
                        StringaCheckAmministratori = StringaCheckAmministratori & Items.Value & ","
                    End If
                Next
                If StringaCheckAmministratori <> "" Then
                    CaricaCheckAmministratori()
                Else
                    CaricaAmministratori()
                    CaricaCondomini()
                    CaricaIndirizzi()
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: CaricaCheckIndirizzi " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

End Class
