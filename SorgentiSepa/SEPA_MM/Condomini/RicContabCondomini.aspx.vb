Imports System.IO

Partial Class Condomini_RicContabCondomini
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property datatableContabCondomini() As Data.DataTable
        Get
            If Not (ViewState("datatableContabCondomini") Is Nothing) Then
                Return ViewState("datatableContabCondomini")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("datatableContabCondomini") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.txtAnnoFine.Attributes.Add("onkeyup", "javascript:valid(this,'onlynumbers');")
            Me.txtAnnoInizio.Attributes.Add("onkeyup", "javascript:valid(this,'onlynumbers');")
            CaricaAmministratori()
            CaricaCondomini()
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
    Private Sub CaricaCheckAmministratori()
        Try
            chkCondomini.Items.Clear()
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
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                CaricaAmministratori()
                CaricaCondomini()
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
    Protected Sub chkAmministratori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAmministratori.SelectedIndexChanged
        CaricaCheckAmministratori()
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.href=""pagina_home.aspx"";</script>")
    End Sub
    Protected Sub btnExportXls_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportXls.Click
        Try
            Dim ammSelezionati As String = ""
            Dim ammSelezionati2 As String = ""
            Dim CondSelezionati As String = ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader
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
            If ammSelezionati <> "" Then
                ammSelezionati = ammSelezionati.Substring(0, ammSelezionati.LastIndexOf(","))
            End If
            If CondSelezionati <> "" Then
                CondSelezionati = CondSelezionati.Substring(0, CondSelezionati.LastIndexOf(","))
            End If
            If ammSelezionati = "" And CondSelezionati = "" Then
                Response.Write("<script>alert('Selezionare almeno un criterio(Amministratore/Condomini) per effettuare il report')</script>")
                Exit Sub
            End If
            If Not String.IsNullOrEmpty(Me.txtAnnoInizio.Text) And Not String.IsNullOrEmpty(Me.txtAnnoFine.Text) Then
                If Me.txtAnnoFine.Text < Me.txtAnnoInizio.Text Then
                    Response.Write("<script>alert('L\'anno di gestione finale non può essere inferiore a quello iniziale!')</script>")
                    Exit Sub
                End If
            End If
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            CreaDT()
            If ammSelezionati <> "" Then
                par.cmd.CommandText = "SELECT DISTINCT ID_CONDOMINIO FROM SISCOM_MI.COND_AMMINISTRAZIONE WHERE ID_AMMINISTRATORE IN (" & ammSelezionati & ") AND DATA_FINE IS NULL"
                myReader = par.cmd.ExecuteReader
                While myReader.Read
                    ammSelezionati2 = ammSelezionati2 & par.IfNull(myReader("ID_CONDOMINIO"), 0) & ","
                End While
                myReader.Close()
                If ammSelezionati2 <> "" Then
                    ammSelezionati2 = ammSelezionati2.Substring(0, ammSelezionati2.LastIndexOf(","))
                End If
            End If
            par.cmd.CommandText = "SELECT A.ID AS IDCONDOMINIO, COND_GESTIONE.ID AS IDGESTIONE, a.denominazione AS condominio, TO_CHAR (TO_DATE (cond_gestione.data_inizio, 'yyyymmdd'), 'dd/mm/yyyy') AS data_inizio, TO_CHAR (TO_DATE (cond_gestione.data_fine, 'yyyymmdd'), 'dd/mm/yyyy') AS data_fine, " _
                                & "(CASE WHEN cond_gestione.tipo = 'O' THEN 'ORDINARIA' ELSE 'STRAORDINARIA' End) AS tipo, (CASE WHEN cond_gestione.stato_bilancio = 'P0' THEN 'BOZZA' WHEN stato_bilancio = 'P1' THEN 'CONVALIDATO' ELSE 'CONSUNTIVATO' End) AS stato_bilancio, " _
                                & "cond_gestione.n_rate, TO_CHAR (TO_DATE (cond_gestione.rata_1_scad, 'yyyymmdd'), 'dd/mm/yyyy') AS scadenza_rata_1, TO_CHAR (TO_DATE (cond_gestione.rata_2_scad, 'yyyymmdd'), 'dd/mm/yyyy') AS scadenza_rata_2, " _
                                & "TO_CHAR (TO_DATE (cond_gestione.rata_3_scad, 'yyyymmdd'), 'dd/mm/yyyy') AS scadenza_rata_3, TO_CHAR (TO_DATE (cond_gestione.rata_4_scad, 'yyyymmdd'), 'dd/mm/yyyy') AS scadenza_rata_4, " _
                                & "TO_CHAR (TO_DATE (cond_gestione.rata_5_scad, 'yyyymmdd'), 'dd/mm/yyyy') AS scadenza_rata_5, TO_CHAR (TO_DATE (cond_gestione.rata_6_scad, 'yyyymmdd'), 'dd/mm/yyyy') AS scadenza_rata_6, cond_gestione.note " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.condomini a " _
                                & "WHERE cond_gestione.ID IN (SELECT DISTINCT cond_gestione.ID FROM siscom_mi.cond_gestione, siscom_mi.pagamenti, siscom_mi.prenotazioni, siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE cond_gestione_dett_scad.id_gestione = cond_gestione.ID AND prenotazioni.ID = cond_gestione_dett_scad.id_prenotazione AND prenotazioni.id_pagamento = pagamenti.ID) " _
                                & "AND cond_gestione.id_condominio = a.ID"
            If CondSelezionati <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " AND A.ID IN (" & CondSelezionati & ")"
            Else
                par.cmd.CommandText = par.cmd.CommandText & " AND A.ID IN (" & ammSelezionati2 & ")"
            End If
            If Not String.IsNullOrEmpty(Me.txtAnnoInizio.Text) Then
                par.cmd.CommandText = par.cmd.CommandText & " AND COND_GESTIONE.DATA_INIZIO >= " & par.PulisciStrSql(txtAnnoInizio.Text) & "0101"
            End If
            If Not String.IsNullOrEmpty(Me.txtAnnoFine.Text) Then
                par.cmd.CommandText = par.cmd.CommandText & " AND COND_GESTIONE.DATA_FINE <= " & par.PulisciStrSql(txtAnnoFine.Text) & "0101"
            End If
            par.cmd.CommandText = par.cmd.CommandText & " ORDER BY 3,4"
            myReader = par.cmd.ExecuteReader
            Dim row As Data.DataRow
            While myReader.Read
                row = datatableContabCondomini.NewRow()
                row.Item("CONDOMINIO") = par.IfNull(myReader("CONDOMINIO"), "")
                row.Item("DATA_INIZIO") = par.IfNull(myReader("DATA_INIZIO"), "")
                row.Item("DATA_FINE") = par.IfNull(myReader("DATA_FINE"), "")
                row.Item("TIPO") = par.IfNull(myReader("TIPO"), "")
                row.Item("STATO_BILANCIO") = par.IfNull(myReader("STATO_BILANCIO"), "")
                row.Item("NR_RATE") = par.IfNull(myReader("N_RATE"), 0)
                row.Item("NOTE") = par.IfNull(myReader("NOTE"), "")
                'row.Item("VOCE") = "DATA DI SCADENZA RATA"
                row.Item("CONG_PREC") = ""
                row.Item("PREVENTIVO") = ""
                If chkConsunt.Checked = True Then
                    row.Item("CONSUNTIVO") = ""
                End If
                If chkAnnoPrec.Checked = True Then
                    row.Item("PREVENTIVO_ANNO_PRECEDENTE") = ""
                End If
                'row.Item("RATA_1") = par.IfNull(myReader("SCADENZA_RATA_1"), "")
                'row.Item("RATA_2") = par.IfNull(myReader("SCADENZA_RATA_2"), "")
                'row.Item("RATA_3") = par.IfNull(myReader("SCADENZA_RATA_3"), "")
                'row.Item("RATA_4") = par.IfNull(myReader("SCADENZA_RATA_4"), "")
                'row.Item("RATA_5") = par.IfNull(myReader("SCADENZA_RATA_5"), "")
                'row.Item("RATA_6") = par.IfNull(myReader("SCADENZA_RATA_6"), "")
                par.cmd.CommandText = "SELECT NOME || ' ' || COGNOME AS AMMINISTRATORE " _
                                    & "FROM SISCOM_MI.COND_AMMINISTRATORI " _
                                    & "WHERE ID = (SELECT DISTINCT ID_AMMINISTRATORE FROM SISCOM_MI.COND_AMMINISTRAZIONE WHERE ID_CONDOMINIO = " & par.IfNull(myReader("IDCONDOMINIO"), 0) & " AND DATA_FINE IS NULL)"
                myReader2 = par.cmd.ExecuteReader
                If myReader2.Read Then
                    row.Item("AMMINISTRATORE") = par.IfNull(myReader2("AMMINISTRATORE"), "")
                End If
                myReader2.Close()
                datatableContabCondomini.Rows.Add(row)
                If par.IfNull(myReader("IDGESTIONE"), 0) = 0 Then
                    par.cmd.CommandText = "SELECT MAX(ID) AS ID_PIANO_F FROM SISCOM_MI.PF_MAIN WHERE ID_STATO = 5"
                Else
                    par.cmd.CommandText = "select distinct(id_piano_finanziario) as id_piano_f from siscom_mi.cond_voci_spesa_pf where id_voce_cond in (select id_voce from siscom_mi.cond_gestione_dett where id_gestione =" & par.IfNull(myReader("IDGESTIONE"), 0) & ")"
                End If
                myReader2 = par.cmd.ExecuteReader
                If myReader2.Read Then
                    idPianoF.Value = par.IfNull(myReader2("ID_PIANO_F"), 0)
                End If
                myReader2.Close()
                par.cmd.CommandText = "SELECT id_stato from siscom_mi.pf_main where id = " & idPianoF.Value
                myReader2 = par.cmd.ExecuteReader
                If myReader2.Read Then
                    If par.IfNull(myReader2("id_stato"), 0) <> 5 Then
                        myReader3 = par.cmd.ExecuteReader
                        par.cmd.CommandText = "SELECT MAX(ID) AS ID_PIANO_F FROM SISCOM_MI.PF_MAIN WHERE ID_STATO = 5"
                        myReader3 = par.cmd.ExecuteReader
                        If myReader3.Read Then
                            idPianoF.Value = par.IfNull(myReader3("ID_PIANO_F"), 0)
                        End If
                        myReader3.Close()
                    End If
                End If
                myReader2.Close()
                If idPianoF.Value = 0 Then
                    'row = datatableContabCondomini.NewRow()
                    'row.Item("VOCE") = "AMMINISTRAZIONE"
                    'datatableContabCondomini.Rows.Add(row)
                    'row = datatableContabCondomini.NewRow()
                    'row.Item("VOCE") = "MANUTENZIONE"
                    'datatableContabCondomini.Rows.Add(row)
                    'row = datatableContabCondomini.NewRow()
                    'row.Item("VOCE") = "SFITTI"
                    'datatableContabCondomini.Rows.Add(row)
                    'row = datatableContabCondomini.NewRow()
                    'row.Item("VOCE") = "SERVIZI PER U.I. CONDOMINIO - AMM. INDIRETTA"
                    'datatableContabCondomini.Rows.Add(row)
                    'row = datatableContabCondomini.NewRow()
                    'row.Item("VOCE") = "RISCALDAMENTO PER U.I. CONDOMINIO - AMM. INDIRETTA"
                    'datatableContabCondomini.Rows.Add(row)
                    'row = datatableContabCondomini.NewRow()
                    'row.Item("VOCE") = "ASCENSORE PER U.I. CONDOMINIO - AMM. INDIRETTA"
                    'datatableContabCondomini.Rows.Add(row)
                    'row = datatableContabCondomini.NewRow()
                    'row.Item("VOCE") = "TOTALE"
                    'datatableContabCondomini.Rows.Add(row)
                    'row = datatableContabCondomini.NewRow()
                    'row.Item("VOCE") = "MOROSITA'"
                    'datatableContabCondomini.Rows.Add(row)
                Else
                    Dim rigo As Integer = 1
                    Dim totalecong As Decimal = 0
                    Dim totaleprev As Decimal = 0
                    Dim totconsuntivo As Decimal = 0
                    Dim totGestPrec As Decimal = 0
                    Dim totalerata1 As Decimal = 0
                    Dim totalerata2 As Decimal = 0
                    Dim totalerata3 As Decimal = 0
                    Dim totalerata4 As Decimal = 0
                    Dim totalerata5 As Decimal = 0
                    Dim totalerata6 As Decimal = 0
                    par.cmd.CommandText = "SELECT COND_VOCI_SPESA.FL_TOTALE,COND_VOCI_SPESA.ID AS IDVOCE, " _
                                & "COND_VOCI_SPESA.DESCRIZIONE, COND_VOCI_SPESA_PF.ID_VOCE_PF, COND_VOCI_SPESA_PF.ID_VOCE_PF_IMPORTO " _
                                & "FROM SISCOM_MI.COND_VOCI_SPESA,SISCOM_MI.COND_VOCI_SPESA_PF WHERE FL_TOTALE = 1 AND COND_VOCI_SPESA.ID = ID_VOCE_COND AND ID_PIANO_FINANZIARIO = " & idPianoF.Value & " ORDER BY idvoce ASC"
                    myReader2 = par.cmd.ExecuteReader
                    Dim gestPrec As String = ""
                    If Me.chkAnnoPrec.Checked = True Then
                        par.cmd.CommandText = "SELECT * FROM siscom_mi.COND_GESTIONE WHERE data_fine = (SELECT (SUBSTR(data_fine,0,4)-1||SUBSTR(data_fine,5,4)) FROM siscom_mi.COND_GESTIONE a WHERE a.ID = " & par.IfNull(myReader("IDGESTIONE"), 0) & ") AND id_condominio = " & par.IfNull(myReader("IDCONDOMINIO"), "")
                        'par.cmd.CommandText = "SELECT * FROM COND_GESTIONE WHERE ID_CONDOMINIO = " & par.IfNull(myReader("IDCONDOMINIO"), "") & " and id <> " & par.IfNull(myReader("IDGESTIONE"), 0) & " order by id desc"
                        Dim lettGestPrec As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                        If lettGestPrec.Read Then
                            gestPrec = par.IfNull(lettGestPrec(0), "")
                        End If
                        lettGestPrec.Close()
                    End If

                    While myReader2.Read
                        row = datatableContabCondomini.NewRow()
                        If rigo = 1 Then
                            row.Item("VOCE") = "AMMINISTRAZIONE"
                            rigo = rigo + 1
                        ElseIf rigo = 2 Then
                            row.Item("VOCE") = "MANUTENZIONE"
                            rigo = rigo + 1
                        ElseIf rigo = 3 Then
                            row.Item("VOCE") = "SFITTI"
                            rigo = rigo + 1
                        ElseIf rigo = 4 Then
                            row.Item("VOCE") = "SERVIZI PER U.I. CONDOMINIO - AMM. INDIRETTA"
                            rigo = rigo + 1
                        ElseIf rigo = 5 Then
                            row.Item("VOCE") = "RISCALDAMENTO PER U.I. CONDOMINIO - AMM. INDIRETTA"
                            rigo = rigo + 1
                        ElseIf rigo = 6 Then
                            row.Item("VOCE") = "ASCENSORE PER U.I. CONDOMINIO - AMM. INDIRETTA"
                            rigo = rigo + 1
                        End If
                        par.cmd.CommandText = "SELECT CONGUAGLIO_GP, PREVENTIVO,CONSUNTIVO FROM SISCOM_MI.COND_GESTIONE_DETT WHERE ID_VOCE = " & par.IfNull(myReader2("IDVOCE"), 0) & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0)
                        myReader3 = par.cmd.ExecuteReader
                        If myReader3.Read Then
                            row.Item("CONG_PREC") = IsNumFormat(myReader3("CONGUAGLIO_GP"), 0, "##,##0.00")
                            totalecong = totalecong + par.IfNull(myReader3("CONGUAGLIO_GP"), 0)
                            row.Item("PREVENTIVO") = IsNumFormat(myReader3("PREVENTIVO"), 0, "##,##0.00")
                            totaleprev = totaleprev + par.IfNull(myReader3("PREVENTIVO"), 0)
                            If chkConsunt.Checked = True Then
                                row.Item("CONSUNTIVO") = IsNumFormat(myReader3("CONSUNTIVO"), 0, "##,##0.00")
                                totconsuntivo = totconsuntivo + par.IfNull(myReader3("CONSUNTIVO"), 0)
                            End If
                        End If

                        myReader3.Close()

                        If Me.chkAnnoPrec.Checked = True And Not String.IsNullOrEmpty(gestPrec) Then
                            par.cmd.CommandText = "SELECT (NVL(CONGUAGLIO_GP,0) + NVL(PREVENTIVO,0)) as PREV_ANNO_PREC FROM SISCOM_MI.COND_GESTIONE_DETT WHERE ID_VOCE = " & par.IfNull(myReader2("IDVOCE"), 0) & " AND ID_GESTIONE = " & gestPrec & ""
                            myReader3 = par.cmd.ExecuteReader
                            If myReader3.Read Then
                                row.Item("PREVENTIVO_ANNO_PRECEDENTE") = IsNumFormat(myReader3("PREV_ANNO_PREC"), 0, "##,##0.00")
                                totGestPrec = totGestPrec + par.IfNull(myReader3("PREV_ANNO_PREC"), 0)
                            End If
                            myReader3.Close()
                        ElseIf Me.chkAnnoPrec.Checked = True And String.IsNullOrEmpty(gestPrec) Then
                            row.Item("PREVENTIVO_ANNO_PRECEDENTE") = IsNumFormat(0, 0, "##,##0.00")

                        End If





                        'par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_VOCE = " & par.IfNull(myReader2("IDVOCE"), 0) & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND RATA_SCAD = '" & par.AggiustaData(par.IfNull(myReader("SCADENZA_RATA_1"), "")) & "'"
                        'myReader3 = par.cmd.ExecuteReader
                        'If myReader3.Read Then
                        '    row.Item("RATA_1") = IsNumFormat(myReader3("IMPORTO"), 0, "##,##0.00")
                        '    totalerata1 = totalerata1 + par.IfNull(myReader3("IMPORTO"), 0)
                        'Else
                        '    If par.IfNull(myReader("SCADENZA_RATA_1"), "") <> "" Then
                        '        row.Item("RATA_1") = "0,00"
                        '    End If
                        'End If
                        'myReader3.Close()
                        'par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_VOCE = " & par.IfNull(myReader2("IDVOCE"), 0) & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND RATA_SCAD = '" & par.AggiustaData(par.IfNull(myReader("SCADENZA_RATA_2"), "")) & "'"
                        'myReader3 = par.cmd.ExecuteReader
                        'If myReader3.Read Then
                        '    row.Item("RATA_2") = IsNumFormat(myReader3("IMPORTO"), 0, "##,##0.00")
                        '    totalerata2 = totalerata2 + par.IfNull(myReader3("IMPORTO"), 0)
                        'Else
                        '    If par.IfNull(myReader("SCADENZA_RATA_2"), "") <> "" Then
                        '        row.Item("RATA_2") = "0,00"
                        '    End If
                        'End If
                        'myReader3.Close()
                        'par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_VOCE = " & par.IfNull(myReader2("IDVOCE"), 0) & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND RATA_SCAD = '" & par.AggiustaData(par.IfNull(myReader("SCADENZA_RATA_3"), "")) & "'"
                        'myReader3 = par.cmd.ExecuteReader
                        'If myReader3.Read Then
                        '    row.Item("RATA_3") = IsNumFormat(myReader3("IMPORTO"), 0, "##,##0.00")
                        '    totalerata3 = totalerata3 + par.IfNull(myReader3("IMPORTO"), 0)
                        'Else
                        '    If par.IfNull(myReader("SCADENZA_RATA_3"), "") <> "" Then
                        '        row.Item("RATA_3") = "0,00"
                        '    End If
                        'End If
                        'myReader3.Close()
                        'par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_VOCE = " & par.IfNull(myReader2("IDVOCE"), 0) & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND RATA_SCAD = '" & par.AggiustaData(par.IfNull(myReader("SCADENZA_RATA_4"), "")) & "'"
                        'myReader3 = par.cmd.ExecuteReader
                        'If myReader3.Read Then
                        '    row.Item("RATA_4") = IsNumFormat(myReader3("IMPORTO"), 0, "##,##0.00")
                        '    totalerata4 = totalerata4 + par.IfNull(myReader3("IMPORTO"), 0)
                        'Else
                        '    If par.IfNull(myReader("SCADENZA_RATA_4"), "") <> "" Then
                        '        row.Item("RATA_4") = "0,00"
                        '    End If
                        'End If
                        'myReader3.Close()
                        'par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_VOCE = " & par.IfNull(myReader2("IDVOCE"), 0) & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND RATA_SCAD = '" & par.AggiustaData(par.IfNull(myReader("SCADENZA_RATA_5"), "")) & "'"
                        'myReader3 = par.cmd.ExecuteReader
                        'If myReader3.Read Then
                        '    row.Item("RATA_5") = IsNumFormat(myReader3("IMPORTO"), 0, "##,##0.00")
                        '    totalerata5 = totalerata5 + par.IfNull(myReader3("IMPORTO"), 0)
                        'Else
                        '    If par.IfNull(myReader("SCADENZA_RATA_5"), "") <> "" Then
                        '        row.Item("RATA_5") = "0,00"
                        '    End If
                        'End If
                        'myReader3.Close()
                        'par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_VOCE = " & par.IfNull(myReader2("IDVOCE"), 0) & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND RATA_SCAD = '" & par.AggiustaData(par.IfNull(myReader("SCADENZA_RATA_6"), "")) & "'"
                        'myReader3 = par.cmd.ExecuteReader
                        'If myReader3.Read Then
                        '    row.Item("RATA_6") = IsNumFormat(myReader3("IMPORTO"), 0, "##,##0.00")
                        '    totalerata6 = totalerata6 + par.IfNull(myReader3("IMPORTO"), 0)
                        'Else
                        '    If par.IfNull(myReader("SCADENZA_RATA_6"), "") <> "" Then
                        '        row.Item("RATA_6") = "0,00"
                        '    End If
                        'End If
                        'myReader3.Close()
                        datatableContabCondomini.Rows.Add(row)
                    End While
                    myReader2.Close()
                    row = datatableContabCondomini.NewRow()
                    row.Item("VOCE") = "TOTALE"
                    If totalecong <> 0 Then
                        row.Item("CONG_PREC") = IsNumFormat(totalecong, 0, "##,##0.00")
                    Else
                        row.Item("CONG_PREC") = totalecong
                    End If
                    If totaleprev <> 0 Then
                        row.Item("PREVENTIVO") = IsNumFormat(totaleprev, 0, "##,##0.00")
                    Else
                        row.Item("PREVENTIVO") = totaleprev
                    End If
                    If chkConsunt.Checked = True Then
                        row.Item("CONSUNTIVO") = totconsuntivo
                    End If
                    If chkAnnoPrec.Checked = True Then
                        row.Item("PREVENTIVO_ANNO_PRECEDENTE") = totGestPrec
                    End If

                    'If par.IfNull(myReader("SCADENZA_RATA_1"), "") <> "" Then
                    '    If totalerata1 <> 0 Then
                    '        row.Item("RATA_1") = IsNumFormat(totalerata1, 0, "##,##0.00")
                    '    Else
                    '        row.Item("RATA_1") = totalerata1
                    '    End If
                    'End If
                    'If par.IfNull(myReader("SCADENZA_RATA_2"), "") <> "" Then
                    '    If totalerata2 <> 0 Then
                    '        row.Item("RATA_2") = IsNumFormat(totalerata2, 0, "##,##0.00")
                    '    Else
                    '        row.Item("RATA_2") = totalerata2
                    '    End If
                    'End If
                    'If par.IfNull(myReader("SCADENZA_RATA_3"), "") <> "" Then
                    '    If totalerata3 <> 0 Then
                    '        row.Item("RATA_3") = IsNumFormat(totalerata3, 0, "##,##0.00")
                    '    Else
                    '        row.Item("RATA_3") = totalerata3
                    '    End If
                    'End If
                    'If par.IfNull(myReader("SCADENZA_RATA_4"), "") <> "" Then
                    '    If totalerata4 <> 0 Then
                    '        row.Item("RATA_4") = IsNumFormat(totalerata4, 0, "##,##0.00")
                    '    Else
                    '        row.Item("RATA_4") = totalerata4
                    '    End If
                    'End If
                    'If par.IfNull(myReader("SCADENZA_RATA_5"), "") <> "" Then
                    '    If totalerata5 <> 0 Then
                    '        row.Item("RATA_5") = IsNumFormat(totalerata5, 0, "##,##0.00")
                    '    Else
                    '        row.Item("RATA_5") = totalerata5
                    '    End If
                    'End If
                    'If par.IfNull(myReader("SCADENZA_RATA_6"), "") <> "" Then
                    '    If totalerata6 <> 0 Then
                    '        row.Item("RATA_6") = IsNumFormat(totalerata6, 0, "##,##0.00")
                    '    Else
                    '        row.Item("RATA_6") = totalerata6
                    '    End If
                    'End If
                    datatableContabCondomini.Rows.Add(row)
                    row = datatableContabCondomini.NewRow()
                    row.Item("VOCE") = "MOROSITA'"
                    par.cmd.CommandText = "select id from siscom_mi.cond_voci_spesa , siscom_mi.cond_voci_spesa_pf where fl_totale = 0 and cond_voci_spesa.id = cond_voci_spesa_pf.id_voce_cond and id_piano_finanziario = " & idPianoF.Value
                    myReader2 = par.cmd.ExecuteReader
                    If myReader2.Read Then
                        IdVoceMorosita.Value = par.IfNull(myReader2("ID"), 0)
                    End If
                    myReader2.Close()
                    par.cmd.CommandText = "SELECT CONGUAGLIO_GP, PREVENTIVO ,CONSUNTIVO FROM SISCOM_MI.COND_GESTIONE_DETT WHERE ID_VOCE = " & IdVoceMorosita.Value & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0)
                    myReader2 = par.cmd.ExecuteReader
                    If myReader2.Read Then
                        row.Item("CONG_PREC") = IsNumFormat(myReader2("CONGUAGLIO_GP"), 0, "##,##0.00")
                        row.Item("PREVENTIVO") = IsNumFormat(myReader2("PREVENTIVO"), 0, "##,##0.00")
                        If chkConsunt.Checked = True Then
                            row.Item("CONSUNTIVO") = IsNumFormat(myReader2("CONSUNTIVO"), 0, "##,##0.00")
                        End If

                    End If
                    myReader2.Close()

                    If Me.chkAnnoPrec.Checked = True And Not String.IsNullOrEmpty(gestPrec) Then
                        par.cmd.CommandText = "SELECT (NVL(CONGUAGLIO_GP,0) + NVL(PREVENTIVO,0)) as PREV_ANNO_PREC FROM SISCOM_MI.COND_GESTIONE_DETT WHERE ID_VOCE = " & IdVoceMorosita.Value & " AND ID_GESTIONE = " & gestPrec & ""
                        myReader2 = par.cmd.ExecuteReader
                        If myReader2.Read Then
                            row.Item("PREVENTIVO_ANNO_PRECEDENTE") = IsNumFormat(myReader2("PREV_ANNO_PREC"), 0, "##,##0.00")
                            totGestPrec = totGestPrec + par.IfNull(myReader2("PREV_ANNO_PREC"), 0)
                        End If
                        myReader2.Close()
                    ElseIf Me.chkAnnoPrec.Checked = True And String.IsNullOrEmpty(gestPrec) Then
                        row.Item("PREVENTIVO_ANNO_PRECEDENTE") = IsNumFormat(0, 0, "##,##0.00")

                    End If


                    'par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_VOCE = " & IdVoceMorosita.Value & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND RATA_SCAD = '" & par.AggiustaData(par.IfNull(myReader("SCADENZA_RATA_1"), "")) & "'"
                    'myReader2 = par.cmd.ExecuteReader
                    'If myReader2.Read Then
                    '    row.Item("RATA_1") = IsNumFormat(myReader2("IMPORTO"), 0, "##,##0.00")
                    'Else
                    '    If par.IfNull(myReader("SCADENZA_RATA_1"), "") <> "" Then
                    '        row.Item("RATA_1") = "0,00"
                    '    End If
                    'End If
                    'myReader2.Close()
                    'par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_VOCE = " & IdVoceMorosita.Value & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND RATA_SCAD = '" & par.AggiustaData(par.IfNull(myReader("SCADENZA_RATA_2"), "")) & "'"
                    'myReader2 = par.cmd.ExecuteReader
                    'If myReader2.Read Then
                    '    row.Item("RATA_2") = IsNumFormat(myReader2("IMPORTO"), 0, "##,##0.00")
                    'Else
                    '    If par.IfNull(myReader("SCADENZA_RATA_2"), "") <> "" Then
                    '        row.Item("RATA_2") = "0,00"
                    '    End If
                    'End If
                    'myReader2.Close()
                    'par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_VOCE = " & IdVoceMorosita.Value & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND RATA_SCAD = '" & par.AggiustaData(par.IfNull(myReader("SCADENZA_RATA_3"), "")) & "'"
                    'myReader2 = par.cmd.ExecuteReader
                    'If myReader2.Read Then
                    '    row.Item("RATA_3") = IsNumFormat(myReader2("IMPORTO"), 0, "##,##0.00")
                    'Else
                    '    If par.IfNull(myReader("SCADENZA_RATA_3"), "") <> "" Then
                    '        row.Item("RATA_3") = "0,00"
                    '    End If
                    'End If
                    'myReader2.Close()
                    'par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_VOCE = " & IdVoceMorosita.Value & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND RATA_SCAD = '" & par.AggiustaData(par.IfNull(myReader("SCADENZA_RATA_4"), "")) & "'"
                    'myReader2 = par.cmd.ExecuteReader
                    'If myReader2.Read Then
                    '    row.Item("RATA_4") = IsNumFormat(myReader2("IMPORTO"), 0, "##,##0.00")
                    'Else
                    '    If par.IfNull(myReader("SCADENZA_RATA_4"), "") <> "" Then
                    '        row.Item("RATA_4") = "0,00"
                    '    End If
                    'End If
                    'myReader2.Close()
                    'par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_VOCE = " & IdVoceMorosita.Value & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND RATA_SCAD = '" & par.AggiustaData(par.IfNull(myReader("SCADENZA_RATA_5"), "")) & "'"
                    'myReader2 = par.cmd.ExecuteReader
                    'If myReader2.Read Then
                    '    row.Item("RATA_5") = IsNumFormat(myReader2("IMPORTO"), 0, "##,##0.00")
                    'Else
                    '    If par.IfNull(myReader("SCADENZA_RATA_5"), "") <> "" Then
                    '        row.Item("RATA_5") = "0,00"
                    '    End If
                    'End If
                    'myReader2.Close()
                    'par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD WHERE ID_VOCE = " & IdVoceMorosita.Value & " AND ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND RATA_SCAD = '" & par.AggiustaData(par.IfNull(myReader("SCADENZA_RATA_6"), "")) & "'"
                    'myReader2 = par.cmd.ExecuteReader
                    'If myReader2.Read Then
                    '    row.Item("RATA_6") = IsNumFormat(myReader2("IMPORTO"), 0, "##,##0.00")
                    'Else
                    '    If par.IfNull(myReader("SCADENZA_RATA_6"), "") <> "" Then
                    '        row.Item("RATA_6") = "0,00"
                    '    End If
                    'End If
                    'myReader2.Close()
                    datatableContabCondomini.Rows.Add(row)
                End If
            End While
            myReader.Close()
            Me.dgvExport.Visible = True
            Me.dgvExport.DataSource = datatableContabCondomini
            Me.dgvExport.DataBind()

            Esporta()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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
    Private Sub CreaDT()
        Try
            '######### SVUOTA E CREA COLONNE DATATABLE #########
            datatableContabCondomini = New Data.DataTable
            datatableContabCondomini.Clear()
            datatableContabCondomini.Columns.Clear()
            datatableContabCondomini.Rows.Clear()
            datatableContabCondomini.Columns.Add("CONDOMINIO")
            datatableContabCondomini.Columns.Add("AMMINISTRATORE")
            datatableContabCondomini.Columns.Add("DATA_INIZIO")
            datatableContabCondomini.Columns.Add("DATA_FINE")
            datatableContabCondomini.Columns.Add("TIPO")
            datatableContabCondomini.Columns.Add("STATO_BILANCIO")
            datatableContabCondomini.Columns.Add("NR_RATE")
            datatableContabCondomini.Columns.Add("NOTE")
            datatableContabCondomini.Columns.Add("VOCE")
            datatableContabCondomini.Columns.Add("CONG_PREC")
            datatableContabCondomini.Columns.Add("PREVENTIVO")
            If Me.chkConsunt.Checked = True Then
                datatableContabCondomini.Columns.Add("CONSUNTIVO")
            End If
            If Me.chkAnnoPrec.Checked = True Then
                datatableContabCondomini.Columns.Add("PREVENTIVO_ANNO_PRECEDENTE")
            End If

            'datatableContabCondomini.Columns.Add("RATA_1")
            'datatableContabCondomini.Columns.Add("RATA_2")
            'datatableContabCondomini.Columns.Add("RATA_3")
            'datatableContabCondomini.Columns.Add("RATA_4")
            'datatableContabCondomini.Columns.Add("RATA_5")
            'datatableContabCondomini.Columns.Add("RATA_6")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Decimal
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDec(v), Precision)
        End If
    End Function
    Private Sub Esporta()
        Try
            If datatableContabCondomini.Rows.Count > 0 Then
                Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(Me.dgvExport, "ExpContabilita", , , , False)
                If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                    Response.Redirect("..\/FileTemp\/" & nomefile, False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                End If
            Else
                Response.Write("<script>alert('I filtri inseriti non hanno prodotto risultati. Riprovare!');</script>")
            End If
            Me.dgvExport.Visible = False

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
End Class
