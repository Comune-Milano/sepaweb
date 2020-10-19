Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_Report_StampaP_SingoleVoci
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable
    Dim TotParziali As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Dim EsistonoSingoli As Boolean = False
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then
            Try

                Dim TOTALE As Double
                Dim I As Long = 0

                Me.Label6.Text = "PAGAMENTI PERVENUTI PER SINGOLE VOCI BOLLETTA </br>"

                If par.IfEmpty(Request.QueryString("DAL"), "Null") <> "Null" Or par.IfEmpty(Request.QueryString("AL"), "Null") <> "Null" Then
                    Me.Label6.Text = Me.Label6.Text & " EMESSO "
                End If

                If par.IfEmpty(Request.QueryString("DAL"), "Null") <> "Null" Then
                    Me.Label6.Text = Me.Label6.Text & " DAL " & par.FormattaData(Request.QueryString("DAL"))
                End If

                If par.IfEmpty(Request.QueryString("AL"), "Null") <> "Null" Then
                    Me.Label6.Text = Me.Label6.Text & " FINO AL " & par.FormattaData(Request.QueryString("AL"))
                Else
                    'Me.Label6.Text = Me.Label6.Text & " FINO AL " & Format(Now, "dd/MM/yyyy")
                End If


                If par.IfEmpty(Request.QueryString("DAL0"), "Null") <> "Null" Or par.IfEmpty(Request.QueryString("AL0"), "Null") <> "Null" Then
                    Me.Label6.Text = Me.Label6.Text & "</br>PAGATE "
                End If

                If par.IfEmpty(Request.QueryString("DAL0"), "Null") <> "Null" Then
                    Me.Label6.Text = Me.Label6.Text & " DAL " & par.FormattaData(Request.QueryString("DAL0"))
                End If

                If par.IfEmpty(Request.QueryString("AL0"), "Null") <> "Null" Then
                    Me.Label6.Text = Me.Label6.Text & " FINO AL " & par.FormattaData(Request.QueryString("AL0"))
                Else
                    'Me.Label6.Text = Me.Label6.Text & " FINO AL " & Format(Now, "dd/MM/yyyy")
                End If

                If par.IfEmpty(Request.QueryString("RIFDAL"), "Null") <> "Null" Or par.IfEmpty(Request.QueryString("RIFAL"), "Null") <> "Null" Then
                    Me.Label6.Text = Me.Label6.Text & "</br>RIFERIMENTO "
                End If

                If par.IfEmpty(Request.QueryString("RIFDAL"), "Null") <> "Null" Then
                    Me.Label6.Text = Me.Label6.Text & " DAL " & par.FormattaData(Request.QueryString("RIFDAL"))
                End If

                If par.IfEmpty(Request.QueryString("RIFAL"), "Null") <> "Null" Then
                    Me.Label6.Text = Me.Label6.Text & " FINO AL " & par.FormattaData(Request.QueryString("RIFAL"))
                Else
                    'Me.Label6.Text = Me.Label6.Text & " FINO AL " & Format(Now, "dd/MM/yyyy")
                End If


                If par.IfEmpty(Request.QueryString("RIFDAL1"), "Null") <> "Null" Or par.IfEmpty(Request.QueryString("RIFAL1"), "Null") <> "Null" Then
                    Me.Label6.Text = Me.Label6.Text & "</br>ACCREDITO "
                End If
                If par.IfEmpty(Request.QueryString("RIFDAL1"), "Null") <> "Null" Then
                    Me.Label6.Text = Me.Label6.Text & " DAL " & par.FormattaData(Request.QueryString("RIFDAL1"))
                End If

                If par.IfEmpty(Request.QueryString("RIFAL1"), "Null") <> "Null" Then
                    Me.Label6.Text = Me.Label6.Text & " FINO AL " & par.FormattaData(Request.QueryString("RIFAL1"))
                Else
                    'Me.Label6.Text = Me.Label6.Text & " FINO AL " & Format(Now, "dd/MM/yyyy")
                End If

                If Request.QueryString("TIPO") = "Attiva" Then
                    Me.Label6.Text = Label6.Text & " </br>RELATIVE ALLE BOLLETTE DI ATTIVAZIONE CONTRATTO"
                End If

                If Request.QueryString("TIPO") = "Bollettazione" Then
                    Me.Label6.Text = Label6.Text & " </br>RELATIVE A TUTTE LE BOLLETTE INVIATE TRAMITE BANCA"
                End If

                If Request.QueryString("TIPO") = "Virt.Manuale" Then
                    Me.Label6.Text = Label6.Text & " </br>RELATIVE A TUTTE LE BOLLETTE DI CONTRATTI VIRTUALI MANUALI"
                End If

                If Request.QueryString("TIPO") = "Generale" Then
                    Me.Label6.Text = Label6.Text & " </br>RELATIVE A TUTTE LE TIPOLOGIE DI BOLLETTE"
                End If

                '********CONNESSIONE*********
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If


                Dim miaSelect As String = ""

                par.cmd.CommandText = Session.Item("REPORT") & " AND T_VOCI_BOLLETTA.competenza=1 GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE) ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC"
                miaSelect = Session.Item("REPORT") & " GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE) ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC"
                Dim row As System.Data.DataRow
                Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt2 As New Data.DataTable
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da.Fill(dt)


                If dt.Rows.Count > 0 Then
                    For Each row In dt.Rows
                        row.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=9&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "&RIFDAL1=" & Request.QueryString("RIFDAL1") & "&RIFAL1=" & Request.QueryString("RIFAL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dt.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next

                    row = dt.NewRow()
                    row.Item("DESCRIZIONE") = "T O T A L E"
                    row.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dt.Rows.Add(row)

                    DataGridRateEmesse.DataSource = dt
                    DataGridRateEmesse.DataBind()
                Else
                    lblErp0.Visible = False
                End If

                ''+++++AGGIUNTA RIGA SEPARATRICE PER LE ALTRE TIPOLOGIE
                row = dt.NewRow()
                row.Item("DESCRIZIONE") = "DI COMPETENZA GESTORE"
                row.Item("IMPORTO") = "****"
                dt.Rows.Add(row)


                'COMPETENZA GESTORE
                TOTALE = 0
                I = 0


                Dim dtI As New Data.DataTable
                'Dim S As String = miaSelect
                'Dim sFrom As String
                'Dim sWhere As String
                par.cmd.CommandText = Session.Item("REPORT") & " AND T_VOCI_BOLLETTA.competenza=2 GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE) ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC"

                Dim rowI As System.Data.DataRow
                Dim daI As Oracle.DataAccess.Client.OracleDataAdapter

                daI = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                daI.Fill(dtI)
                If dtI.Rows.Count > 0 Then

                    For Each rowI In dtI.Rows
                        'rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=9&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "' target='_blank'>Visualizza</a"
                        rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=9&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "&RIFDAL1=" & Request.QueryString("RIFDAL1") & "&RIFAL1=" & Request.QueryString("RIFAL1") & "' target='_blank'>Visualizza</a"


                        TOTALE = TOTALE + par.IfNull(dtI.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next


                    rowI = dtI.NewRow()
                    rowI.Item("DESCRIZIONE") = "T O T A L E"
                    rowI.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dtI.Rows.Add(rowI)


                    DataGridRateEmesse0.DataSource = dtI
                    DataGridRateEmesse0.DataBind()

                    dt.Merge(dtI)
                Else
                    lblErp1.Visible = False
                End If

                '+++++AGGIUNTA RIGA SEPARATRICE PER LE ALTRE TIPOLOGIE
                rowI = dt.NewRow()
                rowI.Item("DESCRIZIONE") = "DI COMPETENZA SINDACATI"
                rowI.Item("IMPORTO") = "****"
                dt.Rows.Add(rowI)


                'COMPETENZA SINDACATI
                TOTALE = 0
                I = 0


                dtI = New Data.DataTable
                'Dim S As String = miaSelect
                'Dim sFrom As String
                'Dim sWhere As String
                par.cmd.CommandText = Session.Item("REPORT") & " AND T_VOCI_BOLLETTA.competenza=3 GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE) ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC"

                'Dim rowI As System.Data.DataRow
                'Dim daI As Oracle.DataAccess.Client.OracleDataAdapter

                daI = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                daI.Fill(dtI)
                If dtI.Rows.Count > 0 Then

                    For Each rowI In dtI.Rows
                        'rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=9&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "' target='_blank'>Visualizza</a"
                        rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=9&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "&RIFDAL1=" & Request.QueryString("RIFDAL1") & "&RIFAL1=" & Request.QueryString("RIFAL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dtI.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next


                    rowI = dtI.NewRow()
                    rowI.Item("DESCRIZIONE") = "T O T A L E"
                    rowI.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dtI.Rows.Add(rowI)


                    DataGridRateEmesse1.DataSource = dtI
                    DataGridRateEmesse1.DataBind()

                    dt.Merge(dtI)
                Else
                    lblErp2.Visible = False
                End If

                '+++++AGGIUNTA RIGA SEPARATRICE PER LE ALTRE TIPOLOGIE
                rowI = dt.NewRow()
                rowI.Item("DESCRIZIONE") = "ALTRA COMPETENZA"
                rowI.Item("IMPORTO") = "****"
                dt.Rows.Add(rowI)


                'COMPETENZA ALTRO
                TOTALE = 0
                I = 0

                Dim TOTALE_ALTRO As Double = 0

                dtI = New Data.DataTable
                'Dim S As String = miaSelect
                'Dim sFrom As String
                'Dim sWhere As String
                par.cmd.CommandText = Session.Item("REPORT") & " AND (T_VOCI_BOLLETTA.competenza<>1 AND T_VOCI_BOLLETTA.competenza<>2 AND T_VOCI_BOLLETTA.competenza<>3) GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE) ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC"

                'Dim rowI As System.Data.DataRow
                'Dim daI As Oracle.DataAccess.Client.OracleDataAdapter

                daI = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                daI.Fill(dtI)
                If dtI.Rows.Count > 0 Then

                    For Each rowI In dtI.Rows
                        'rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=9&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "' target='_blank'>Visualizza</a"
                        rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=9&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "&RIFDAL1=" & Request.QueryString("RIFDAL1") & "&RIFAL1=" & Request.QueryString("RIFAL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dtI.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    TOTALE_ALTRO = TOTALE

                    rowI = dtI.NewRow()
                    rowI.Item("DESCRIZIONE") = "T O T A L E"
                    rowI.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dtI.Rows.Add(rowI)


                    DataGridRateEmesse2.DataSource = dtI
                    DataGridRateEmesse2.DataBind()

                    ''+++++AGGIUNTA RIGA SEPARATRICE PER LE ALTRE TIPOLOGIE
                    'rowI = dt.NewRow()
                    'rowI.Item("DESCRIZIONE") = "DI CUI ERP"
                    'rowI.Item("IMPORTO") = "****"
                    'dt.Rows.Add(rowI)

                    dt.Merge(dtI)
                Else
                    lblErp3.Visible = False
                End If



                '***********************ERP***********************
                TOTALE = 0
                I = 0

                dtI = New Data.DataTable
                'If Request.QueryString("TIPO") = "Attiva" Then
                TextBox1.Value = 1
                'Dim dtI As New Data.DataTable
                Dim S As String = miaSelect
                Dim sFrom As String
                sFrom = S.Split("WHERE")(0).ToString & " ,SISCOM_MI.RAPPORTI_UTENZA "
                Dim sWhere As String
                sWhere = "WHERE RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC= 'ERP' AND RAPPORTI_UTENZA.ID= BOL_BOLLETTE.ID_CONTRATTO AND " & S.Split("WHERE")(1).ToString.Substring(5)
                par.cmd.CommandText = sFrom & sWhere

                'Dim rowI As System.Data.DataRow
                'Dim daI As Oracle.DataAccess.Client.OracleDataAdapter

                daI = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                daI.Fill(dtI)
                If dtI.Rows.Count > 0 Then

                    For Each rowI In dtI.Rows
                        'rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=1&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "' target='_blank'>Visualizza</a"
                        rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=1&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "&RIFDAL1=" & Request.QueryString("RIFDAL1") & "&RIFAL1=" & Request.QueryString("RIFAL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dtI.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    TotParziali = TotParziali + TOTALE

                    rowI = dtI.NewRow()
                    rowI.Item("DESCRIZIONE") = "T O T A L E"
                    rowI.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dtI.Rows.Add(rowI)


                    DataGridERP.DataSource = dtI
                    DataGridERP.DataBind()

                    '+++++AGGIUNTA RIGA SEPARATRICE PER LE ALTRE TIPOLOGIE
                    rowI = dt.NewRow()
                    rowI.Item("DESCRIZIONE") = "DI CUI ERP"
                    rowI.Item("IMPORTO") = "****"
                    dt.Rows.Add(rowI)

                    dt.Merge(dtI)
                Else
                    lblErp.Visible = False
                End If

                'End If

                '***********************USDX***********************
                TOTALE = 0
                I = 0
                'If Request.QueryString("TIPO") = "Attiva" Then
                TextBox1.Value = 1

                'Dim dtI As New Data.DataTable
                'Dim S As String = Session.Item("REPORT")
                'Dim sFrom As String
                sFrom = S.Split("WHERE")(0).ToString & " ,SISCOM_MI.RAPPORTI_UTENZA "
                'Dim sWhere As String
                sWhere = "WHERE SUBSTR(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,1,3)= 'USD' AND RAPPORTI_UTENZA.ID= BOL_BOLLETTE.ID_CONTRATTO AND " & S.Split("WHERE")(1).ToString.Substring(5)
                par.cmd.CommandText = sFrom & sWhere

                'rowI.Delete()
                dtI = New Data.DataTable

                daI = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                daI.Fill(dtI)
                If dtI.Rows.Count > 0 Then

                    For Each rowI In dtI.Rows
                        'rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=2&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "' target='_blank'>Visualizza</a"
                        rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=2&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "&RIFDAL1=" & Request.QueryString("RIFDAL1") & "&RIFAL1=" & Request.QueryString("RIFAL1") & "' target='_blank'>Visualizza</a"

                        TOTALE = TOTALE + par.IfNull(dtI.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    TotParziali = TotParziali + TOTALE

                    rowI = dtI.NewRow()
                    rowI.Item("DESCRIZIONE") = "T O T A L E"
                    rowI.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dtI.Rows.Add(rowI)


                    DataGridUSDX.DataSource = dtI
                    DataGridUSDX.DataBind()
                    '+++++AGGIUNTA RIGA SEPARATRICE PER LE ALTRE TIPOLOGIE
                    rowI = dt.NewRow()
                    rowI.Item("DESCRIZIONE") = "DI CUI USI DIVERSI"
                    rowI.Item("IMPORTO") = "****"
                    dt.Rows.Add(rowI)

                    dt.Merge(dtI)
                Else
                    Label3.Visible = False
                End If
                'End If

                '***********************L43198***********************
                TOTALE = 0
                I = 0
                'If Request.QueryString("TIPO") = "Attiva" Then
                TextBox1.Value = 1

                'Dim dtI As New Data.DataTable
                'Dim S As String = Session.Item("REPORT")
                'Dim sFrom As String
                sFrom = S.Split("WHERE")(0).ToString & " ,SISCOM_MI.RAPPORTI_UTENZA "
                'Dim sWhere As String
                sWhere = "WHERE RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC= 'L43198' AND RAPPORTI_UTENZA.ID= BOL_BOLLETTE.ID_CONTRATTO AND " & S.Split("WHERE")(1).ToString.Substring(5)
                par.cmd.CommandText = sFrom & sWhere

                'Dim rowI As System.Data.DataRow
                'Dim daI As Oracle.DataAccess.Client.OracleDataAdapter

                dtI = New Data.DataTable

                daI = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                daI.Fill(dtI)
                If dtI.Rows.Count > 0 Then

                    For Each rowI In dtI.Rows
                        'rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=3&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "' target='_blank'>Visualizza</a"
                        rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=3&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "&RIFDAL1=" & Request.QueryString("RIFDAL1") & "&RIFAL1=" & Request.QueryString("RIFAL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dtI.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    TotParziali = TotParziali + TOTALE

                    rowI = dtI.NewRow()
                    rowI.Item("DESCRIZIONE") = "T O T A L E"
                    rowI.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dtI.Rows.Add(rowI)


                    DataGridL431.DataSource = dtI
                    DataGridL431.DataBind()

                    '+++++AGGIUNTA RIGA SEPARATRICE PER LE ALTRE TIPOLOGIE
                    rowI = dt.NewRow()
                    rowI.Item("DESCRIZIONE") = "DI CUI LEGGE 431"
                    rowI.Item("IMPORTO") = "****"
                    dt.Rows.Add(rowI)

                    dt.Merge(dtI)
                Else
                    Label2.Visible = False
                End If
                'End If
                '***********************EQC392***********************
                TOTALE = 0
                I = 0
                'If Request.QueryString("TIPO") = "Attiva" Then
                TextBox1.Value = 1

                'Dim dtI As New Data.DataTable
                'Dim S As String = Session.Item("REPORT")
                'Dim sFrom As String
                sFrom = S.Split("WHERE")(0).ToString & " ,SISCOM_MI.RAPPORTI_UTENZA "
                'Dim sWhere As String
                sWhere = "WHERE RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC= 'EQC392' AND RAPPORTI_UTENZA.ID= BOL_BOLLETTE.ID_CONTRATTO AND " & S.Split("WHERE")(1).ToString.Substring(5)
                par.cmd.CommandText = sFrom & sWhere

                'Dim rowI As System.Data.DataRow
                'Dim daI As Oracle.DataAccess.Client.OracleDataAdapter

                dtI = New Data.DataTable

                daI = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                daI.Fill(dtI)
                If dtI.Rows.Count > 0 Then

                    For Each rowI In dtI.Rows
                        'rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=4&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "' target='_blank'>Visualizza</a"
                        rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=4&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "&RIFDAL1=" & Request.QueryString("RIFDAL1") & "&RIFAL1=" & Request.QueryString("RIFAL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dtI.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    TotParziali = TotParziali + TOTALE

                    rowI = dtI.NewRow()
                    rowI.Item("DESCRIZIONE") = "T O T A L E"
                    rowI.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dtI.Rows.Add(rowI)


                    datagidEQC.DataSource = dtI
                    datagidEQC.DataBind()

                    '+++++AGGIUNTA RIGA SEPARATRICE PER LE ALTRE TIPOLOGIE
                    rowI = dt.NewRow()
                    rowI.Item("DESCRIZIONE") = "DI CUI EQUOCANONE"
                    rowI.Item("IMPORTO") = "****"
                    dt.Rows.Add(rowI)
                    dt.Merge(dtI)
                Else
                    Label1.Visible = False
                End If
                'End If



                '***********************NONE***********************
                TOTALE = 0
                I = 0
                'If Request.QueryString("TIPO") = "Attiva" Then

                TextBox1.Value = 1

                'Dim dtI As New Data.DataTable
                'Dim S As String = Session.Item("REPORT")
                'Dim sFrom As String
                sFrom = S.Split("WHERE")(0).ToString & " ,SISCOM_MI.RAPPORTI_UTENZA "
                'Dim sWhere As String
                sWhere = "WHERE RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC= 'NONE' AND RAPPORTI_UTENZA.ID= BOL_BOLLETTE.ID_CONTRATTO AND " & S.Split("WHERE")(1).ToString.Substring(5)
                par.cmd.CommandText = sFrom & sWhere

                'Dim rowI As System.Data.DataRow
                'Dim daI As Oracle.DataAccess.Client.OracleDataAdapter

                dtI = New Data.DataTable

                daI = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                daI.Fill(dtI)
                If dtI.Rows.Count > 0 Then

                    For Each rowI In dtI.Rows
                        'rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=5&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "' target='_blank'>Visualizza</a"
                        rowI.Item("DETTAGLI") = "<a href='Dettagli.aspx?X=5&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dtI.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL0=" & Request.QueryString("DAL0") & "&AL0=" & Request.QueryString("AL0") & "&DAL1=" & Request.QueryString("RIFDAL") & "&AL1=" & Request.QueryString("RIFAL") & "&RIFDAL1=" & Request.QueryString("RIFDAL1") & "&RIFAL1=" & Request.QueryString("RIFAL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dtI.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    TotParziali = TotParziali + TOTALE

                    rowI = dtI.NewRow()
                    rowI.Item("DESCRIZIONE") = "T O T A L E"
                    rowI.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dtI.Rows.Add(rowI)


                    DataGridNone.DataSource = dtI
                    DataGridNone.DataBind()

                    '+++++AGGIUNTA RIGA SEPARATRICE PER LE ALTRE TIPOLOGIE
                    rowI = dt.NewRow()
                    rowI.Item("DESCRIZIONE") = "DI CUI ABUSIVI"
                    rowI.Item("IMPORTO") = "****"
                    dt.Rows.Add(rowI)
                    dt.Merge(dtI)
                Else
                    Label4.Visible = False
                End If
                'End If

                'Else
                '    Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');</SCRIPT>")
                '    Response.Write("<script language='javascript'> { self.close() }</script>")
                'End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Me.lblTotDeiTot.Text = "SOMMA DEI TOTALI PER CIASCUNA TIPOLOGIA DI CONTRATTO PARI A €." & Format(TotParziali, "##,##0.00")

                HttpContext.Current.Session.Add("AA", dt)
                imgExcel.Attributes.Add("onclick", "javascript:window.open('DownLoad.aspx?CHIAMA=2','Distinta','');")

            Catch ex As Exception
                Me.lblErrore.Visible = True
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Text = ex.Message

            End Try
        End If

    End Sub

    Protected Sub imgExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExcel.Click

    End Sub
End Class
