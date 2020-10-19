Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_Report_StampaPagamentiPerv
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            Try
                Dim Tipologia As String = Request.QueryString("TIPO")
                Dim DataInizio As String = Request.QueryString("DAL")
                Dim DataFine As String = Request.QueryString("AL")
                Dim Complesso As String = Request.QueryString("COMPLESSO")
                Dim Edificio As String = Request.QueryString("EDIFICIO")
                Dim Unita As String = Request.QueryString("UNITA")

                Dim DataInizio0 As String = Request.QueryString("DAL0")
                Dim DataFine0 As String = Request.QueryString("AL0")

                Dim DataInizio1 As String = Request.QueryString("DAL1")
                Dim DataFine1 As String = Request.QueryString("AL1")

                '********CONNESSIONE E APERTURA TRANSAZIONE*********
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                '*******COSTRUZIONE DELLA QUERY DI RICERCA IN BASE A TIPOLOGIA E OGGETTO SELEZIONATO*******
                Dim bTrovato As Boolean = False
                Dim sStringSQL As String = ""


                Select Case Request.QueryString("TIPO")

                    Case "1", "2"
                        NomeFile = "PagamentiPervenuti"
                        lblTipoPagamento.Text = "PAGAMENTI PERVENUTI"

                        bTrovato = True

                        If DataInizio <> "" Then
                            If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                            sStringSQL = sStringSQL & " DATA_EMISSIONE >=" & DataInizio
                            bTrovato = True
                        End If
                        If DataFine <> "" Then
                            If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                            sStringSQL = sStringSQL & "DATA_EMISSIONE <=" & DataFine
                            bTrovato = True
                        End If

                        If DataInizio0 <> "" Then
                            If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                            sStringSQL = sStringSQL & "DATA_PAGAMENTO >=" & DataInizio0
                            bTrovato = True
                        End If
                        If DataFine0 <> "" Then
                            If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                            sStringSQL = sStringSQL & "DATA_PAGAMENTO <=" & DataFine0
                            bTrovato = True
                        End If

                        If DataInizio1 <> "" Then
                            If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                            sStringSQL = sStringSQL & "RIFERIMENTO_DA >=" & DataInizio1
                            bTrovato = True
                        End If
                        If DataFine1 <> "" Then
                            If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                            sStringSQL = sStringSQL & "RIFERIMENTO_A <=" & DataFine1
                            bTrovato = True
                        End If


                        'Case "2"
                        '    NomeFile = "PagamentiPervenuti"
                        '    lblTipoPagamento.Text = "PAGAMENTI PERVENUTI PER DATA PAGAMENTO"

                        '    bTrovato = True
                        '    If DataInizio1 <> "" Then
                        '        If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                        '        sStringSQL = sStringSQL & "DATA_PAGAMENTO >=" & DataInizio1
                        '        bTrovato = True
                        '    End If
                        '    If DataFine1 <> "" Then
                        '        If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                        '        sStringSQL = sStringSQL & "DATA_PAGAMENTO <=" & DataFine1
                        '        bTrovato = True
                        '    End If


                    Case "3", "4"

                        bTrovato = True
                        NomeFile = "PagamentiNonPervenuti"
                        lblTipoPagamento.Text = "PAGAMENTI NON PERVENUTI"

                        If DataInizio <> "" Then
                            If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                            sStringSQL = sStringSQL & " DATA_EMISSIONE >=" & DataInizio
                            bTrovato = True
                        End If
                        If DataFine <> "" Then
                            If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                            sStringSQL = sStringSQL & "DATA_EMISSIONE <=" & DataFine
                            bTrovato = True
                        End If

                        If DataInizio0 <> "" Then
                            If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                            sStringSQL = sStringSQL & "DATA_SCADENZA >=" & DataInizio0
                            bTrovato = True
                        End If
                        If DataFine0 <> "" Then
                            If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                            sStringSQL = sStringSQL & "DATA_SCADENZA <=" & DataFine0
                            bTrovato = True
                        End If

                        If DataInizio1 <> "" Then
                            If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                            sStringSQL = sStringSQL & "RIFERIMENTO_DA >=" & DataInizio1
                            bTrovato = True
                        End If
                        If DataFine1 <> "" Then
                            If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                            sStringSQL = sStringSQL & "RIFERIMENTO_A <=" & DataFine1
                            bTrovato = True
                        End If

                End Select

                If Edificio <> "-1" Then
                    If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                    sStringSQL = sStringSQL & "ID_EDIFICIO = '" & Edificio & "'"
                    bTrovato = True
                End If
                '***********DATI RELATIVI A COMPLESSO EDIFICIO ED UNITA

                If Complesso <> "-1" Then
                    If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                    sStringSQL = sStringSQL & "ID_COMPLESSO = '" & Complesso & "'"
                    bTrovato = True
                End If

                If Unita <> "-1" And Unita <> "" Then
                    If bTrovato = True Then sStringSQL = sStringSQL & " AND "
                    sStringSQL = sStringSQL & "ID_UNITA = '" & Unita & "'"
                    bTrovato = True
                End If


                If Request.QueryString("TIPO") = 1 Or Request.QueryString("TIPO") = 2 Then
                    sStringSQL = " SELECT ROWNUM,BOL_BOLLETTE.ID,BOL_BOLLETTE.INTESTATARIO,BOL_BOLLETTE.INDIRIZZO, BOL_BOLLETTE.CAP_CITTA,CASE WHEN BOL_BOLLETTE.N_RATA=99 THEN 'MA' WHEN BOL_BOLLETTE.N_RATA=999 THEN 'AU' WHEN BOL_BOLLETTE.N_RATA=99999 THEN 'CO' ELSE TO_CHAR(BOL_BOLLETTE.N_RATA) END||'/'||BOL_BOLLETTE.ANNO AS N_RATA,to_char(to_date(BOL_BOLLETTE.DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_EMISSIONE,to_char(to_date(BOL_BOLLETTE.DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy')AS DATA_SCADENZA, to_char(to_date(BOL_BOLLETTE.DATA_PAGAMENTO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PAGAMENTO,to_char(to_date(BOL_BOLLETTE.RIFERIMENTO_DA,'yyyymmdd'),'dd/mm/yyyy')||'-'||to_char(to_date(BOL_BOLLETTE.RIFERIMENTO_A,'yyyymmdd'),'dd/mm/yyyy') AS PERIODO, RAPPORTI_UTENZA.COD_CONTRATTO, TO_CHAR (BOL_BOLLETTE.IMPORTO_PAGATO,'9G999G990D99') AS ""IMPORTO_PAGATO"",TO_CHAR (BOL_BOLLETTE.IMPORTO_TOTALE,'9G999G990D99') AS ""IMPORTO_EMESSO"" FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.RAPPORTI_UTENZA WHERE BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NULL AND  (BOL_BOLLETTE.FL_ANNULLATA='0' OR (BOL_BOLLETTE.FL_ANNULLATA<>'0' AND DATA_PAGAMENTO IS NOT NULL )) and BOL_BOLLETTE.fl_stampato='1' and RAPPORTI_UTENZA.ID = BOL_BOLLETTE.ID_CONTRATTO AND DATA_PAGAMENTO IS NOT NULL  " & sStringSQL & " ORDER BY BOL_BOLLETTE.INTESTATARIO ASC,BOL_BOLLETTE.ID DESC"
                Else
                    sStringSQL = " SELECT ROWNUM, BOL_BOLLETTE.ID,BOL_BOLLETTE.INTESTATARIO,BOL_BOLLETTE.INDIRIZZO,BOL_BOLLETTE.CAP_CITTA,CASE WHEN BOL_BOLLETTE.N_RATA=99 THEN 'MA' WHEN BOL_BOLLETTE.N_RATA=999 THEN 'AU' WHEN BOL_BOLLETTE.N_RATA=99999 THEN 'CO' ELSE TO_CHAR(BOL_BOLLETTE.N_RATA) END||'/'||BOL_BOLLETTE.ANNO AS N_RATA,to_char(to_date(BOL_BOLLETTE.DATA_EMISSIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_EMISSIONE,to_char(to_date(BOL_BOLLETTE.DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy')AS DATA_SCADENZA, to_char(to_date(BOL_BOLLETTE.DATA_PAGAMENTO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PAGAMENTO,to_char(to_date(BOL_BOLLETTE.RIFERIMENTO_DA,'yyyymmdd'),'dd/mm/yyyy')||'-'||to_char(to_date(BOL_BOLLETTE.RIFERIMENTO_A,'yyyymmdd'),'dd/mm/yyyy') AS PERIODO, RAPPORTI_UTENZA.COD_CONTRATTO, TO_CHAR (BOL_BOLLETTE.IMPORTO_PAGATO,'9G999G990D99') AS ""IMPORTO_PAGATO"",TO_CHAR (BOL_BOLLETTE.IMPORTO_TOTALE,'9G999G990D99') AS ""IMPORTO_EMESSO"" FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.RAPPORTI_UTENZA WHERE  BOL_BOLLETTE.DATA_SCADENZA < TO_CHAR(SYSDATE,'YYYYMMDD') and nvl(BOL_BOLLETTE.IMPORTO_PAGATO,0)<>BOL_BOLLETTE.IMPORTO_TOTALE AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NULL AND  (BOL_BOLLETTE.FL_ANNULLATA='0') and BOL_BOLLETTE.fl_stampato='1' and RAPPORTI_UTENZA.ID = BOL_BOLLETTE.ID_CONTRATTO  " & sStringSQL & " ORDER BY BOL_BOLLETTE.INTESTATARIO ASC,BOL_BOLLETTE.ID DESC"

                End If

                Dim row As System.Data.DataRow
                Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                Dim I As Integer = 0
                Dim TOTDAPAGARE As Double
                Dim TOTemesso As Double
                par.cmd.CommandText = sStringSQL
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)


                da.Fill(dt)

                If dt.Rows.Count > 0 Then

                    Dim COLONNA As New System.Data.DataColumn
                    COLONNA.ColumnName = "CONTATORE"
                    dt.Columns.Add(COLONNA)

                    If Request.QueryString("TIPO") = 3 Or Request.QueryString("TIPO") = 4 Then
                        Me.DataGridPagamenti.Columns(10).HeaderText = "IMPORTO DA PAGARE"

                        For Each row In dt.Rows
                            TOTDAPAGARE = TOTDAPAGARE + par.IfNull(dt.Rows(I).Item("IMPORTO_PAGATO"), 0)
                            TOTemesso = TOTemesso + par.IfNull(dt.Rows(I).Item("IMPORTO_EMESSO"), 0)
                            dt.Rows(I).Item("CONTATORE") = I + 1
                            I = I + 1
                        Next
                        row = dt.NewRow()
                        row.Item("INTESTATARIO") = ""
                        row.Item("INDIRIZZO") = ""
                        row.Item("CAP_CITTA") = ""
                        row.Item("N_RATA") = ""
                        row.Item("DATA_EMISSIONE") = ""
                        row.Item("DATA_SCADENZA") = ""
                        row.Item("PERIODO") = ""
                        row.Item("COD_CONTRATTO") = ""
                        row.Item("DATA_PAGAMENTO") = "TOTALE DA PAGARE"
                        row.Item("IMPORTO_PAGATO") = Format(TOTDAPAGARE, "##,##0.00")
                        row.Item("IMPORTO_EMESSO") = Format(TOTemesso, "##,##0.00")
                        dt.Rows.Add(row)
                    End If

                    If Request.QueryString("TIPO") = 1 Or Request.QueryString("TIPO") = 2 Then


                        For Each row In dt.Rows
                            TOTDAPAGARE = TOTDAPAGARE + par.IfNull(dt.Rows(I).Item("IMPORTO_PAGATO"), 0)
                            TOTemesso = TOTemesso + par.IfNull(dt.Rows(I).Item("IMPORTO_EMESSO"), 0)
                            dt.Rows(I).Item("CONTATORE") = I + 1
                            I = I + 1
                        Next
                        row = dt.NewRow()
                        row.Item("DATA_PAGAMENTO") = "TOTALE"
                        row.Item("IMPORTO_PAGATO") = Format(TOTDAPAGARE, "##,##0.00")
                        row.Item("IMPORTO_EMESSO") = Format(TOTemesso, "##,##0.00")
                        dt.Rows.Add(row)
                    End If



                    DataGridPagamenti.DataSource = dt
                    'If Request.QueryString("TIPO") = 3 Or Request.QueryString("TIPO") = 4 Then
                    '    Me.DataGridPagamenti.Columns(8).HeaderText = "IMPORTO DA PAGARE"
                    'End If

                    DataGridPagamenti.DataBind()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Else
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');</SCRIPT>")

                    Response.Write("<script language='javascript'> { self.close() }</script>")

                End If


                HttpContext.Current.Session.Add("AA", dt)
                If Request.QueryString("TIPO") = "1" Or Request.QueryString("TIPO") = "2" Then
                    imgExcel.Attributes.Add("onclick", "javascript:window.open('DownLoad.aspx?CHIAMA=1','Distinta','');")

                Else
                    imgExcel.Attributes.Add("onclick", "javascript:window.open('DownLoad.aspx?CHIAMA=1000','Distinta','');")

                End If




            Catch ex As Exception
                Me.lblErrore.Visible = True
                par.OracleConn.Close()
                lblErrore.Text = ex.Message

            End Try


        'par.cmd.CommandText = sStringSQL
        'myReader = par.cmd.ExecuteReader()
        'lblStampa.Text = "<table border='1'width='100%'><tr style='font-size: 10pt ;text-align: center;font-weight: bold; bgcolor='#CCCCCC'><h3><th colspan= '8'>" & lblTipoPagamento.Text & "</th><h3></tr><tr style='font-size: 10pt ;text-align: center;font-weight: bold; bgcolor='#CCCCCC'><td>INTESTATARIO</td><td>INDIRIZZO</td><td>R.NUMERO</td><td>EMISSIONE</td><td>SCADENZA</td><td>PAGAMENTO</td><td>COD_CONTRATTO</td><td>IMPORTO &euro;</td></tr>"

        'Do While myReader.Read
        '    lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: right;'>" & "<td >" & par.IfNull(myReader("INTESTATARIO"), "") & "</td>" & "<td >" & par.IfNull(myReader("INDIRIZZO"), "") & " - " & par.IfNull(myReader("CAP_CITTA"), "") & "</td>" & "<td >" & par.IfNull(myReader("N_RATA"), "") & "</td>" & "<td >" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</td>" & "<td >" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</td>" & "<td >" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "</td>" & "<td >" & par.IfNull(myReader("COD_CONTRATTO"), "") & "</td><td >" & par.IfNull(myReader("IMPORTO_PAGATO"), "") & "</td></tr>")
        'Loop
        'lblStampa.Text = lblStampa.Text & ("</table>")

        End If



    End Sub

    'Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExcel.Click
    '    Response.ContentType = "application/vnd.ms-excel"
    '    Response.AddHeader("Content-Disposition", "attachment; filename=" & NomeFile & "" & Format(Date.Now) & ".xls;")
    '    Response.Write(lblStampa.Text.ToString)
    '    Response.End()

    'End Sub
    Private Property vTabella() As String
        Get
            If Not (ViewState("par_vTabella") Is Nothing) Then
                Return CStr(ViewState("par_vTabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vTabella") = value
        End Set

    End Property
    Private Property NomeFile() As String
        Get
            If Not (ViewState("par_NomeFile") Is Nothing) Then
                Return CStr(ViewState("par_NomeFile"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_NomeFile") = value
        End Set

    End Property


    

    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExcel.Click
    '    ExportXLS()
    'End Sub


End Class
