
Partial Class Contratti_Report_StampaP_SingoleVoci
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                Dim Str As String

                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
                Str = Str & "<" & "/div>"

                Response.Write(Str)
                Response.Flush()
                Dim TOTALE As Double
                Dim I As Long = 0

                If Request.QueryString("G") = "1" Then
                    Me.Label6.Text = "EMESSO ACCERTATO</br>DISTINTA PER SINGOLE VOCI BOLLETTA "
                Else
                    Me.Label6.Text = "DISTINTA PER SINGOLE VOCI BOLLETTA "
                End If

                If Request.QueryString("G") = "1" Then
                    Select Case Request.QueryString("DAL1")
                        Case "0"
                            Me.Label6.Text = Me.Label6.Text & " EMESSO ACCERTATO GENNAIO-FEBBRAIO " & Request.QueryString("AL1")
                        Case "1"
                            Me.Label6.Text = Me.Label6.Text & " EMESSO ACCERTATO MARZO-APRILE " & Request.QueryString("AL1")
                        Case "2"
                            Me.Label6.Text = Me.Label6.Text & " EMESSO ACCERTATO MAGGIO-GIUGNO " & Request.QueryString("AL1")
                        Case "3"
                            Me.Label6.Text = Me.Label6.Text & " EMESSO ACCERTATO LUGLIO-AGOSTO " & Request.QueryString("AL1")
                        Case "4"
                            Me.Label6.Text = Me.Label6.Text & " EMESSO ACCERTATO SETTEMBRE-OTTOBRE " & Request.QueryString("AL1")
                        Case "5"
                            Me.Label6.Text = Me.Label6.Text & " EMESSO ACCERTATO NOVEMBRE-DICEMBRE " & Request.QueryString("AL1")
                    End Select
                End If

                If par.IfEmpty(Request.QueryString("DAL"), "Null") <> "Null" Then
                    If Request.QueryString("G") = "1" Then
                        Select Case Request.QueryString("DAL1")
                            Case "0"
                                Me.Label6.Text = Me.Label6.Text & " EMESSO ACCERTATO GENNAIO-FEBBRAIO " & Request.QueryString("AL1")
                            Case "1"
                                Me.Label6.Text = Me.Label6.Text & " EMESSO ACCERTATO MARZO-APRILE " & Request.QueryString("AL1")
                            Case "2"
                                Me.Label6.Text = Me.Label6.Text & " EMESSO ACCERTATO MAGGIO-GIUGNO " & Request.QueryString("AL1")
                            Case "3"
                                Me.Label6.Text = Me.Label6.Text & " EMESSO ACCERTATO LUGLIO-AGOSTO " & Request.QueryString("AL1")
                            Case "4"
                                Me.Label6.Text = Me.Label6.Text & " EMESSO ACCERTATO SETTEMBRE-OTTOBRE " & Request.QueryString("AL1")
                            Case "5"
                                Me.Label6.Text = Me.Label6.Text & " EMESSO ACCERTATO NOVEMBRE-DICEMBRE " & Request.QueryString("AL1")
                        End Select

                    Else
                        Me.Label6.Text = Me.Label6.Text & " EMESSO DAL " & par.FormattaData(Request.QueryString("DAL"))
                    End If
                End If

                If Request.QueryString("G") <> "1" Then
                    If par.IfEmpty(Request.QueryString("AL"), "Null") <> "Null" Then
                        Me.Label6.Text = Me.Label6.Text & " AL " & par.FormattaData(Request.QueryString("AL"))
                    End If

                    If par.IfEmpty(Request.QueryString("DAL1"), "Null") <> "Null" Then
                        Me.Label6.Text = Me.Label6.Text & " PERIODO DAL " & par.FormattaData(Request.QueryString("DAL1"))
                    End If

                    If par.IfEmpty(Request.QueryString("AL1"), "Null") <> "Null" Then
                        Me.Label6.Text = Me.Label6.Text & " AL " & par.FormattaData(Request.QueryString("AL1"))
                    Else
                        Me.Label6.Text = Me.Label6.Text & " AL " & Format(Now, "dd/MM/yyyy")
                    End If
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

                If Request.QueryString("USD") = True Then
                    Me.Label6.Text = Label6.Text & " </br>RELATIVI AI SOLI USI DIVERSI"
                End If

                '********CONNESSIONE*********
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Dim TOTALE_GENERALE As Double = 0
                Dim TOT_DEPOSITI As Double = 0

                'voci comune
                par.cmd.CommandText = Session.Item("REPORT") & " AND T_VOCI_BOLLETTA.competenza=1 AND ID_CAPITOLO<>8 AND ID_CAPITOLO<>5 AND ID_CAPITOLO<>6  " & " GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE)  ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC "


                Dim row As System.Data.DataRow
                Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt2 As New Data.DataTable
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    For Each row In dt.Rows
                        'row.Item("DETTAGLI") = "<a href='Dettagli.aspx?O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        row.Item("DETTAGLI") = "<a href='Dettagli.aspx?USD=" & Request.QueryString("USD") & "&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"

                        '& "&T=" & Request.QueryString("TIPO") &
                        TOTALE = TOTALE + par.IfNull(dt.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    'row = dt.NewRow()
                    'row.Item("DESCRIZIONE") = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
                    'row.Item("IMPORTO") = "000000000000000"
                    'dt.Rows.Add(row)

                    row = dt.NewRow()
                    row.Item("DESCRIZIONE") = "T O T A L E"
                    row.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dt.Rows.Add(row)

                    TOTALE_GENERALE = TOTALE_GENERALE + TOTALE


                    DataGridRateEmesse.DataSource = dt
                    DataGridRateEmesse.DataBind()
                Else
                    Label7.Visible = False
                End If
                da.Dispose()

                'deposito cauzionale
                par.cmd.CommandText = Session.Item("REPORT") & " AND T_VOCI_BOLLETTA.COMPETENZA=0 " & " GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE)  ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC "
                I = 0
                TOTALE = 0

                Dim row1 As System.Data.DataRow
                Dim da1 As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt21 As New Data.DataTable
                da1 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da1.Fill(dt21)
                If dt21.Rows.Count > 0 Then
                    For Each row1 In dt21.Rows
                        'row1.Item("DETTAGLI") = "<a href='Dettagli.aspx?O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt21.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        row1.Item("DETTAGLI") = "<a href='Dettagli.aspx?USD=" & Request.QueryString("USD") & "&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt21.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dt21.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    'row = dt.NewRow()
                    'row.Item("DESCRIZIONE") = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
                    'row.Item("IMPORTO") = "000000000000000"
                    'dt.Rows.Add(row)

                    row1 = dt21.NewRow()
                    row1.Item("DESCRIZIONE") = "T O T A L E"
                    row1.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dt21.Rows.Add(row1)
                    TOTALE_GENERALE = TOTALE_GENERALE + TOTALE
                    TOT_DEPOSITI = TOTALE

                    DataGridRateEmesse0.DataSource = dt21
                    DataGridRateEmesse0.DataBind()
                Else
                    Label1.Visible = False

                End If
                da1.Dispose()

                'oneri accessori
                par.cmd.CommandText = Session.Item("REPORT") & " AND T_VOCI_BOLLETTA.COMPETENZA=2 AND ID_CAPITOLO<>4 " & " GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE)  ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC "

                I = 0
                TOTALE = 0

                Dim row2 As System.Data.DataRow
                Dim da2 As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt22 As New Data.DataTable
                da2 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da2.Fill(dt22)
                If dt22.Rows.Count > 0 Then
                    For Each row2 In dt22.Rows
                        'row2.Item("DETTAGLI") = "<a href='Dettagli.aspx?O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt22.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        row2.Item("DETTAGLI") = "<a href='Dettagli.aspx?USD=" & Request.QueryString("USD") & "&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt22.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dt22.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    'row = dt.NewRow()
                    'row.Item("DESCRIZIONE") = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
                    'row.Item("IMPORTO") = "000000000000000"
                    'dt.Rows.Add(row)

                    row2 = dt22.NewRow()
                    row2.Item("DESCRIZIONE") = "T O T A L E"
                    row2.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dt22.Rows.Add(row2)
                    TOTALE_GENERALE = TOTALE_GENERALE + TOTALE

                    DataGridRateEmesse1.DataSource = dt22
                    DataGridRateEmesse1.DataBind()
                Else
                    Label8.Visible = False
                    '    Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');</SCRIPT>")

                    '    Response.Write("<script language='javascript'> { self.close() }</script>")

                End If
                da2.Dispose()

                'quota sindacale
                par.cmd.CommandText = Session.Item("REPORT") & " AND T_VOCI_BOLLETTA.COMPETENZA=3 " & " GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE)  ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC "

                I = 0
                TOTALE = 0

                Dim row3 As System.Data.DataRow
                Dim da3 As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt23 As New Data.DataTable
                da3 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da3.Fill(dt23)
                If dt23.Rows.Count > 0 Then
                    For Each row3 In dt23.Rows
                        'row3.Item("DETTAGLI") = "<a href='Dettagli.aspx?O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt23.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        row3.Item("DETTAGLI") = "<a href='Dettagli.aspx?USD=" & Request.QueryString("USD") & "&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt23.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dt23.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    'row = dt.NewRow()
                    'row.Item("DESCRIZIONE") = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
                    'row.Item("IMPORTO") = "000000000000000"
                    'dt.Rows.Add(row)

                    row3 = dt23.NewRow()
                    row3.Item("DESCRIZIONE") = "T O T A L E"
                    row3.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dt23.Rows.Add(row3)
                    TOTALE_GENERALE = TOTALE_GENERALE + TOTALE


                    DataGridRateEmesse2.DataSource = dt23
                    DataGridRateEmesse2.DataBind()
                Else
                    Label9.Visible = False
                    '    Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');</SCRIPT>")

                    '    Response.Write("<script language='javascript'> { self.close() }</script>")

                End If
                da3.Dispose()

                'bollo su bollette
                par.cmd.CommandText = Session.Item("REPORT") & " AND T_VOCI_BOLLETTA.ID_CAPITOLO=4 " & " GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE)  ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC "

                I = 0
                TOTALE = 0

                Dim row4 As System.Data.DataRow
                Dim da4 As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt24 As New Data.DataTable
                da4 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da4.Fill(dt24)
                If dt24.Rows.Count > 0 Then
                    For Each row4 In dt24.Rows
                        'row4.Item("DETTAGLI") = "<a href='Dettagli.aspx?O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt24.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        row4.Item("DETTAGLI") = "<a href='Dettagli.aspx?USD=" & Request.QueryString("USD") & "&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt24.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dt24.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    'row = dt.NewRow()
                    'row.Item("DESCRIZIONE") = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
                    'row.Item("IMPORTO") = "000000000000000"
                    'dt.Rows.Add(row)

                    row4 = dt24.NewRow()
                    row4.Item("DESCRIZIONE") = "T O T A L E"
                    row4.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dt24.Rows.Add(row4)
                    TOTALE_GENERALE = TOTALE_GENERALE + TOTALE


                    DataGridRateEmesse3.DataSource = dt24
                    DataGridRateEmesse3.DataBind()
                Else
                    Label10.Visible = False
                    '    Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');</SCRIPT>")

                    '    Response.Write("<script language='javascript'> { self.close() }</script>")

                End If
                da4.Dispose()

                'spese mav
                par.cmd.CommandText = Session.Item("REPORT") & " AND T_VOCI_BOLLETTA.ID_CAPITOLO=8 " & " GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE)  ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC "

                I = 0
                TOTALE = 0

                Dim row5 As System.Data.DataRow
                Dim da5 As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt25 As New Data.DataTable
                da5 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da5.Fill(dt25)
                If dt25.Rows.Count > 0 Then
                    For Each row5 In dt25.Rows
                        'row5.Item("DETTAGLI") = "<a href='Dettagli.aspx?O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt25.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        row5.Item("DETTAGLI") = "<a href='Dettagli.aspx?USD=" & Request.QueryString("USD") & "&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt25.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dt25.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    'row = dt.NewRow()
                    'row.Item("DESCRIZIONE") = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
                    'row.Item("IMPORTO") = "000000000000000"
                    'dt.Rows.Add(row)

                    row5 = dt25.NewRow()
                    row5.Item("DESCRIZIONE") = "T O T A L E"
                    row5.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dt25.Rows.Add(row5)
                    TOTALE_GENERALE = TOTALE_GENERALE + TOTALE


                    DataGridRateEmesse4.DataSource = dt25
                    DataGridRateEmesse4.DataBind()
                Else
                    Label11.Visible = False
                    '    Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');</SCRIPT>")

                    '    Response.Write("<script language='javascript'> { self.close() }</script>")

                End If
                da5.Dispose()


                'imposte di registro
                par.cmd.CommandText = Session.Item("REPORT") & " AND T_VOCI_BOLLETTA.ID_CAPITOLO=5 " & " GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE)  ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC "

                I = 0
                TOTALE = 0

                Dim row6 As System.Data.DataRow
                Dim da6 As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt26 As New Data.DataTable
                da6 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da6.Fill(dt26)
                If dt26.Rows.Count > 0 Then
                    For Each row6 In dt26.Rows
                        'row6.Item("DETTAGLI") = "<a href='Dettagli.aspx?O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt26.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        row6.Item("DETTAGLI") = "<a href='Dettagli.aspx?USD=" & Request.QueryString("USD") & "&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt26.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dt26.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    'row = dt.NewRow()
                    'row.Item("DESCRIZIONE") = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
                    'row.Item("IMPORTO") = "000000000000000"
                    'dt.Rows.Add(row)

                    row6 = dt26.NewRow()
                    row6.Item("DESCRIZIONE") = "T O T A L E"
                    row6.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dt26.Rows.Add(row6)
                    TOTALE_GENERALE = TOTALE_GENERALE + TOTALE


                    DataGridRateEmesse5.DataSource = dt26
                    DataGridRateEmesse5.DataBind()
                Else
                    Label12.Visible = False
                    '    Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');</SCRIPT>")

                    '    Response.Write("<script language='javascript'> { self.close() }</script>")

                End If
                da6.Dispose()

                'bollo su contratti
                par.cmd.CommandText = Session.Item("REPORT") & " AND T_VOCI_BOLLETTA.ID_CAPITOLO=6 " & " GROUP BY (T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE)  ORDER BY T_VOCI_BOLLETTA.DESCRIZIONE ASC "

                I = 0
                TOTALE = 0

                Dim row7 As System.Data.DataRow
                Dim da7 As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt27 As New Data.DataTable
                da7 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da7.Fill(dt27)
                If dt27.Rows.Count > 0 Then
                    For Each row7 In dt27.Rows
                        'row7.Item("DETTAGLI") = "<a href='Dettagli.aspx?O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt27.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        row7.Item("DETTAGLI") = "<a href='Dettagli.aspx?USD=" & Request.QueryString("USD") & "&O=" & Request.QueryString("O") & "&T=" & Request.QueryString("TIPO") & "&IDV=" & par.IfNull(dt27.Rows(I).Item("ID"), 0) & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&DAL1=" & Request.QueryString("DAL1") & "&AL1=" & Request.QueryString("AL1") & "' target='_blank'>Visualizza</a"
                        TOTALE = TOTALE + par.IfNull(dt27.Rows(I).Item("IMPORTO"), 0)
                        I = I + 1
                    Next
                    'row = dt.NewRow()
                    'row.Item("DESCRIZIONE") = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
                    'row.Item("IMPORTO") = "000000000000000"
                    'dt.Rows.Add(row)

                    row7 = dt27.NewRow()
                    row7.Item("DESCRIZIONE") = "T O T A L E"
                    row7.Item("IMPORTO") = Format(TOTALE, "##,##0.00")
                    dt27.Rows.Add(row7)
                    TOTALE_GENERALE = TOTALE_GENERALE + TOTALE


                    DataGridRateEmesse6.DataSource = dt27
                    DataGridRateEmesse6.DataBind()
                Else
                    Label13.Visible = False
                    '    Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');</SCRIPT>")

                    '    Response.Write("<script language='javascript'> { self.close() }</script>")

                End If
                da7.Dispose()

                lblTotDeiTot.Text = lblTotDeiTot.Text & " " & Format(TOTALE_GENERALE, "##,##0.00")

                If Request.QueryString("G") <> "1" Then
                    par.cmd.CommandText = Session.Item("REPORT1")
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        If par.IfNull(myReader(0), "0,00") <> "0,00" Then
                            lblTotDeiTot0.Text = "<a href='DettagliAnnullate.aspx' target='_blank'>TOTALE ANNULLATE NELLO STESSO PERIODO PARI A € " & par.IfNull(myReader(0), "0,00") & "</a>"
                        Else
                            lblTotDeiTot0.Text = "TOTALE ANNULLATE NELLO STESSO PERIODO PARI A € " & par.IfNull(myReader(0), "0,00")

                        End If
                    End If
                    myReader.Close()
                End If



                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Remove("AA")
                Session.Remove("BB")
                Session.Remove("CC")
                Session.Remove("DD")
                Session.Remove("EE")
                Session.Remove("FF")
                Session.Remove("GG")
                Session.Remove("HH")
                Session.Remove("II")
                Session.Remove("LL")


                HttpContext.Current.Session.Add("AA", dt)
                HttpContext.Current.Session.Add("BB", dt21)
                HttpContext.Current.Session.Add("CC", dt22)
                HttpContext.Current.Session.Add("DD", dt23)
                HttpContext.Current.Session.Add("EE", dt24)
                HttpContext.Current.Session.Add("FF", dt25)
                HttpContext.Current.Session.Add("GG", dt26)
                HttpContext.Current.Session.Add("HH", dt27)
                HttpContext.Current.Session.Add("II", Format(TOTALE_GENERALE, "##,##0.00"))
                HttpContext.Current.Session.Add("LL", lblTotDeiTot0.Text)

                dt.Dispose()
                dt21.Dispose()
                dt22.Dispose()
                dt23.Dispose()
                dt24.Dispose()
                dt25.Dispose()
                dt26.Dispose()
                dt27.Dispose()


                imgExcel.Attributes.Add("onclick", "javascript:window.open('DownLoad.aspx?CHIAMA=3','Distinta','');")

            Catch ex As Exception
                Me.lblErrore.Visible = True
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Text = ex.Message

            End Try
        End If

    End Sub
End Class
