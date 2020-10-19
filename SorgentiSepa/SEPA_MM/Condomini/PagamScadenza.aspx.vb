Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_PagamScadenza
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sUnita(19) As String
    Dim sDecina(9) As String
    Dim Totale As Double = 0

    Public Property datatablepagamscad() As Data.DataTable
        Get
            If Not (ViewState("datatablepagamscad") Is Nothing) Then
                Return ViewState("datatablepagamscad")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("datatablepagamscad") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
            End If
        Try

            Dim Str As String
            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"
            Response.Write(Str)
            If Not IsPostBack Then
                Response.Flush()
                txtFiltrScad.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                CaricaTabella()

                txtDScadenza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub CaricaTabella(Optional scadenza As String = "")
        Try
            'Dim dt As New Data.DataTable
            'dt.Columns.Add(New Data.DataColumn("COND_GESTIONE_ID"))
            'dt.Columns.Add(New Data.DataColumn("ID_CONDOMINIO"))
            'dt.Columns.Add(New Data.DataColumn("DENOMINAZIONE"))
            'dt.Columns.Add(New Data.DataColumn("SCADENZA"))
            'dt.Columns.Add(New Data.DataColumn("IMPORTO"))
            'dt.Columns.Add(New Data.DataColumn("N_RATA"))
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            Dim giorni As Integer = 0
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO = 'ALLARME_SCADENZA_COND'"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                giorni = myReader1(0)
            End If
            myReader1.Close()
            'par.cmd.CommandText = "SELECT PRENOTAZIONI.ID AS ID_PRENOTAZIONE, CONDOMINI.ID AS ID_CONDOMINIO, REPLACE(REPLACE('<a href=£javascript:void(0)£ onclick=£parent.main.location.replace(''Condominio.aspx?IdCond='|| CONDOMINI.ID ||'&CALL=PagamScadenza'');£>'||CONDOMINI.DENOMINAZIONE||'</a>','$','&'),'£','" & Chr(34) & "') AS DENOMINAZIONE," _
            '                    & "PRENOTAZIONI.DESCRIZIONE, TO_CHAR(TO_DATE(PRENOTAZIONI.DATA_PRENOTAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PRENOTAZIONE , " _
            '                    & "TO_CHAR(TO_DATE(PRENOTAZIONI.DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_SCADENZA,TRIM(TO_CHAR(IMPORTO_PRENOTATO,'9G999G999G990D99'))AS IMPORTO_PRENOTATO " _
            '                    & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.CONDOMINI WHERE PRENOTAZIONI.ID_FORNITORE = CONDOMINI.ID_FORNITORE AND PRENOTAZIONI.ID_STATO<> 2 AND PRENOTAZIONI.ID_VOCE_PF IS NOT NULL" _
            '                    & "AND ID_PAGAMENTO IS NULL AND NVL(TO_DATE(DATA_SCADENZA,'yyyymmdd')- TO_DATE(SYSDATE),21) <= " & giorni & " ORDER  BY PRENOTAZIONI.DATA_SCADENZA ASC"
            Dim filtrScad As String = ""
            If Not String.IsNullOrEmpty(scadenza) Then
                filtrScad = " AND PRENOTAZIONI.DATA_SCADENZA = '" & scadenza & "'"
            End If

            par.cmd.CommandText = "SELECT COND_GESTIONE.ID AS ID_GESTIONE, CONDOMINI.ID AS ID_CONDOMINIO, " _
                                & "TRIM(TO_CHAR(SUM(NVL(IMPORTO_PRENOTATO,0)),'9G999G999G990D99'))AS IMPORTO_PRENOTATO , " _
                                & "'<a href=""javascript:parent.main.location.replace(''Condominio.aspx?IdCond='|| CONDOMINI.ID ||'&CALL=PagamScadenza'');"">'||CONDOMINI.DENOMINAZIONE||'</a>' AS DENOMINAZIONE," _
                                & "COND_GESTIONE_DETT_SCAD.N_RATA, " _
                                & "PRENOTAZIONI.DESCRIZIONE, " _
                                & "TO_CHAR(TO_DATE(PRENOTAZIONI.DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_SCADENZA ," _
                                & "(TO_CHAR(TO_DATE(cond_gestione.data_inizio,'yyyymmdd'),'dd/mm/yyyy') ||' - '||TO_CHAR(TO_DATE(cond_gestione.data_fine,'yyyymmdd'),'dd/mm/yyyy')) AS PERIODO, COND_GESTIONE.TIPO AS TIPO " _
                                & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.CONDOMINI,SISCOM_MI.COND_GESTIONE,SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                & "WHERE PRENOTAZIONI.ID_FORNITORE = CONDOMINI.ID_FORNITORE AND PRENOTAZIONI.ID_STATO<> 2 " _
                                & "AND ID_PAGAMENTO IS NULL AND NVL(TO_DATE(DATA_SCADENZA,'yyyymmdd')- TO_DATE(SYSDATE),21) <= 20 " _
                                & "AND COND_GESTIONE.ID_CONDOMINIO = CONDOMINI.ID " & filtrScad _
                                & "AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.ID_PRENOTAZIONE = PRENOTAZIONI.ID AND (ID_VOCE_PF IS NOT NULL OR ID_VOCE_PF_IMPORTO IS NOT NULL) " _
                                & "GROUP BY COND_GESTIONE.ID ,CONDOMINI.ID,COND_GESTIONE_DETT_SCAD.N_RATA,CONDOMINI.DENOMINAZIONE, " _
                                & "PRENOTAZIONI.DESCRIZIONE,PRENOTAZIONI.DATA_SCADENZA,DATA_INIZIO,DATA_FINE,tipo"

            ''26/06/2013 tolgo data prenotazione perchè nel raggruppamento fa casino, in quanto prenoto in momenti diversi e raggruppando le prenotazioni per creare il pagamento fa casino
            ''TO_CHAR(TO_DATE(PRENOTAZIONI.DATA_PRENOTAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PRENOTAZIONE , 
            ''GROUP BY PRENOTAZIONI.DATA_PRENOTAZIONE
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            datatablepagamscad = New Data.DataTable
            da.Fill(datatablepagamscad)
            'dtLettore = RaggruppaPrenotazioni(dtLettore)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            BindGrid()
            Session.Add("DT", datatablepagamscad)
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            DataGridPagScadenza.DataSource = datatablepagamscad
            DataGridPagScadenza.DataBind()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub RiempiDataTable(ByVal dt As Data.DataTable, ByVal row As Data.DataRow, ByVal rataScad As String)
        Dim riga As Data.DataRow
        riga = dt.NewRow()
        riga.Item("COND_GESTIONE_ID") = row.Item("ID")
        riga.Item("ID_CONDOMINIO") = row.Item("id_cond")
        riga.Item("DENOMINAZIONE") = "<a href=""javascript:parent.main.location.replace('Condominio.aspx?IdCond=" & row.Item("id_cond") & "&CALL=PagamScadenza');"">" & row.Item("denominazione") & "</a>"
        riga.Item("SCADENZA") = row.Item(rataScad)
        par.cmd.CommandText = "SELECT SUM(" & rataScad.Substring(0, 6) & ") FROM SISCOM_MI.COND_GESTIONE_DETT WHERE ID_GESTIONE = " & row.Item("ID")
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read Then
            riga.Item("IMPORTO") = Format(par.IfNull(myReader1(0), 0), "##,##0.00")
            Totale = Totale + par.IfNull(myReader1(0), 0)
        Else
            riga.Item("IMPORTO") = "0.00"
        End If
        myReader1.Close()
        riga.Item("N_RATA") = rataScad.Substring(0, 6)
        dt.Rows.Add(riga)
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("DT")
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub
    Protected Sub DataGridPagScadenza_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridPagScadenza.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la il Condominio: " & e.Item.Cells(2).Text.Substring(e.Item.Cells(2).Text.IndexOf(">") + 1).Replace("</a>", "").Replace("\", "").Replace("'", "\'") & "; scadenza pagamento:" & e.Item.Cells(5).Text.Replace("'", "\'") & "';document.getElementById('idGestione').value='" & e.Item.Cells(0).Text & "';document.getElementById('idCond').value='" & e.Item.Cells(1).Text & "';document.getElementById('Importo').value='" & e.Item.Cells(5).Text & "';document.getElementById('txtScadenza').value='" & e.Item.Cells(4).Text & "';document.getElementById('nRata').value='" & e.Item.Cells(6).Text & "';document.getElementById('Periodo').value='" & e.Item.Cells(7).Text & "';document.getElementById('tipoG').value='" & e.Item.Cells(par.IndDGC(DataGridPagScadenza, "TIPO")).Text & "';")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la il Condominio: " & e.Item.Cells(2).Text.Substring(e.Item.Cells(2).Text.IndexOf(">") + 1).Replace("</a>", "").Replace("\", "").Replace("'", "\'") & "; scadenza pagamento:" & e.Item.Cells(5).Text.Replace("'", "\'") & "';document.getElementById('idGestione').value='" & e.Item.Cells(0).Text & "';document.getElementById('idCond').value='" & e.Item.Cells(1).Text & "';document.getElementById('Importo').value='" & e.Item.Cells(5).Text & "';document.getElementById('txtScadenza').value='" & e.Item.Cells(4).Text & "';document.getElementById('nRata').value='" & e.Item.Cells(6).Text & "';document.getElementById('Periodo').value='" & e.Item.Cells(7).Text & "';document.getElementById('tipoG').value='" & e.Item.Cells(par.IndDGC(DataGridPagScadenza, "TIPO")).Text & "';")
        End If
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Esporta()
    End Sub
    Private Sub Esporta()
        Try
            If DataGridPagScadenza.Items.Count > 0 Then
                Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridPagScadenza, "ExportAnCondomini", 90 / 100, , , False)
                If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                    Response.Redirect("..\/FileTemp\/" & nomefile, False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Dim Html As String = ""
        Dim stringWriter As New System.IO.StringWriter
        Dim sourcecode As New HtmlTextWriter(stringWriter)
        Try
            DataGridPagScadenza.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString
            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False
            Dim nomefile As String = "Exp_PagScadenza_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile)
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','ExpPagScadenza','');</script>")
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    '-*******************----------*****************-------------*******************Da modificare tutti i PAGAMENTI*******************-------------****************------------************----------*************
    Private Function CreaPagamento(ByVal IdPrenotazioni As String) As String

        Try
            Dim Id_Fornitore As String = ""
            Dim Id_Pagamento As String = ""
            Dim idTipoPagamento As String = "null"
            Dim idModPagamento As String = "null"

            par.cmd.CommandText = "SELECT FORNITORI.ID AS ID_FORNITORE,ID_TIPO_MODALITA_PAG, ID_TIPO_PAGAMENTO  FROM SISCOM_MI.FORNITORI WHERE ID = (SELECT ID_FORNITORE FROM SISCOM_MI.CONDOMINI WHERE ID = " & idCond.Value & ")"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader1.Read Then
                Id_Fornitore = par.IfNull(myReader1("ID_FORNITORE"), "Null")
                idTipoPagamento = par.IfNull(myReader1("ID_TIPO_PAGAMENTO"), "Null")
                idModPagamento = par.IfNull(myReader1("ID_TIPO_MODALITA_PAG"), "Null")
            End If
            myReader1.Close()
            If Id_Fornitore <> "" Then

                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM DUAL"
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    Id_Pagamento = myReader1(0)
                End If
                myReader1.Close()
                'par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.PRENOTAZIONI WHERE ID=" & idPrenotazione.Value
                'myReader1 = par.cmd.ExecuteReader
                'If myReader1.Read Then

                'End If
                'myReader1.Close()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PAGAMENTI (ID,DATA_EMISSIONE,DATA_STAMPA,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_STATO,TIPO_PAGAMENTO,CONTO_CORRENTE,ID_IBAN_FORNITORE," _
                    & "data_scadenza,ID_TIPO_PAGAMENTO,ID_TIPO_MODALITA_PAG) " _
                & " VALUES (" & Id_Pagamento & "," & Format(Now, "yyyyMMdd") & ", " & Format(Now, "yyyyMMdd") & ",'" & par.PulisciStrSql(Me.txtDescrizione.Text) & "'," & par.VirgoleInPunti(Me.Importo.Value.Replace(".", "")) & "," & Id_Fornitore & ",1,1,'12000X01'," & par.PulisciStrSql(Me.cmbIbanFornitore.SelectedValue.ToString) & "," _
                & "'" & par.AggiustaData(Me.txtDScadenza.Text) & "'," & idTipoPagamento & "," & idModPagamento & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_APPROVATO= IMPORTO_PRENOTATO, ID_STATO = 2,ID_PAGAMENTO = " & Id_Pagamento & " WHERE ID IN  (" & IdPrenotazioni & ")"
                par.cmd.ExecuteNonQuery()

                ''****Scrittura evento PRENOTAZIONE DEL PAGAMENTO
                'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Id_Pagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F97','')"
                'par.cmd.ExecuteNonQuery()

                '****Scrittura evento EMISSIONE DEL PAGAMENTO
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Id_Pagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','')"
                par.cmd.ExecuteNonQuery()
            Else
                Response.Write("<script>alert('Non esiste un fornitore per emettere il pagamento!Impossibile procedere');</script>")
                Id_Pagamento = ""
            End If

            Return Id_Pagamento
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Function

    Private Sub PdfPagamento(ByVal ID As String)
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ModelloPagamento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()
            Dim InizioES As String = ""
            Dim FineEs As String = ""
            Dim Matricola As String = ""
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim tb1 As String = "<table style='width:100%;'>"
            Dim tb2 As String = "<table style='width:100%;'>"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            Dim lettDettagli As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "SELECT FORNITORI.*," _
                                & "(select descrizione from siscom_mi.tipo_modalita_pag where id = fornitori.id_tipo_modalita_pag) as modalita, " _
                                & "(select descrizione from siscom_mi.tipo_pagamento where id = fornitori.id_tipo_pagamento) as tipo_pag " _
                                & "FROM SISCOM_MI.FORNITORI, SISCOM_MI.CONDOMINI WHERE condomini.ID =" & idCond.Value & " and fornitori.ID = condomini.ID_FORNITORE"
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$chiamante$", "CONTO CORRENTE:")
                'contenuto = Replace(contenuto, "$den_chiamante$", myReader1("DENOMINAZIONE"))
                'contenuto = Replace(contenuto, "$dettaglio$", myReader1("DENOMINAZIONE"))
                'contenuto = Replace(contenuto, "$sc_rata$", par.FormattaData(txtScadenza.Value))
                'contenuto = Replace(contenuto, "$iban$", par.IfNull(myReader1("IBAN"), "n.d."))
                contenuto = Replace(contenuto, "$modalita$", par.IfNull(myReader1("modalita"), "n.d."))
                contenuto = Replace(contenuto, "$condizione$", par.IfNull(myReader1("tipo_pag"), "n.d."))

                '*****************SCRITTURA TABELLA DETTAGLI dettagli
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "") & "</td>"
                tb1 = tb1 & "</tr>"
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> IBAN: " & Me.cmbIbanFornitore.SelectedItem.ToString.ToUpper & "</td></tr>"
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> cod. fiscale: " & par.IfNull(myReader1("COD_FISCALE"), "") & "</td><tr></table>"
                '*****************FINE SCRITTURA DETTAGLI

            End If
            myReader1.Close()
            contenuto = Replace(contenuto, "$dscadenza$", Me.txtDScadenza.Text)

            ''par.cmd.CommandText = "SELECT * FROM SISCOM_MI.cond_gestione WHERE ID =" & idPrenotazione.Value
            ''myReader1 = par.cmd.ExecuteReader
            ''If myReader1.Read Then
            'InizioES = "01/01/2010"
            'FineEs = ""

            'InizioES = par.FormattaData(myReader1("DATA_INIZIO"))
            'FineEs = par.FormattaData(myReader1("DATA_FINE"))
            ''End If
            ''myReader1.Close()
            'contenuto = Replace(contenuto, "$dettagli_chiamante$", "12000X01")
            contenuto = Replace(contenuto, "$copia$", "")

            contenuto = Replace(contenuto, "$fornitori$", tb1)
            contenuto = Replace(contenuto, "$grigliaM$", "")
            Dim idvocePf As String = ""
            par.cmd.CommandText = ""


            par.cmd.CommandText = "SELECT PAGAMENTI.*,PRENOTAZIONI.*, prenotazioni.data_scadenza as d_scad,T_ESERCIZIO_FINANZIARIO.INIZIO AS INIZIO_ESERCIZIO,T_ESERCIZIO_FINANZIARIO.FINE AS FINE_ESERCIZIO " _
                                & "FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.PAGAMENTI " _
                                & "WHERE PRENOTAZIONI.ID_PAGAMENTO = " & ID & " AND PAGAMENTI.ID = PRENOTAZIONI.ID_PAGAMENTO AND PF_VOCI.ID = PRENOTAZIONI.ID_VOCE_PF " _
                                & "AND PF_VOCI.ID_PIANO_FINANZIARIO = PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID"
            myReader1 = par.cmd.ExecuteReader
            '*****Peppe Modify 27/04/2011 secondo nuove direttive stampa modelli pagamento
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$anno$", par.IfNull(myReader1("ANNO"), ""))
                contenuto = Replace(contenuto, "$progr$", par.IfNull(myReader1("PROGR"), ""))
                contenuto = Replace(contenuto, "$dettagli_chiamante$", "12000X01")
                contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(myReader1("DATA_EMISSIONE")))
                contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(myReader1("DATA_STAMPA")))
                contenuto = Replace(contenuto, "$TOT$", Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00"))
                InizioES = "01/01/" & myReader1("d_scad").ToString.Substring(0, 4)
                FineEs = "31/12/" & myReader1("d_scad").ToString.Substring(0, 4)

                '*****************SCRITTURA TABELLA CENTRALE DETTAGLI PAGAMENTO
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "N.D.") & "</td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                '*****************
                par.cmd.CommandText = "SELECT COND_VOCI_SPESA.DESCRIZIONE, PRENOTAZIONI.* FROM SISCOM_MI.COND_VOCI_SPESA,SISCOM_MI.COND_VOCI_SPESA_PF,  SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_PAGAMENTO =" & ID _
                                    & " AND COND_VOCI_SPESA.ID = COND_VOCI_SPESA_PF.ID_VOCE_COND AND (PRENOTAZIONI.ID_VOCE_PF = COND_VOCI_SPESA_PF.ID_VOCE_PF OR PRENOTAZIONI.ID_VOCE_PF_IMPORTO = COND_VOCI_SPESA_PF.ID_VOCE_PF_IMPORTO)"
                lettDettagli = par.cmd.ExecuteReader
                While lettDettagli.Read
                    tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & par.IfNull(lettDettagli("DESCRIZIONE"), "n.d.") & " €.</td>"
                    tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(lettDettagli("IMPORTO_APPROVATO"), 0), "##,##0.00") & "</td>"
                    tb2 = tb2 & "</tr>"

                    If String.IsNullOrEmpty(idvocePf) Then
                        idvocePf = lettDettagli("ID_VOCE_PF")
                    Else
                        idvocePf = idvocePf & "," & lettDettagli("ID_VOCE_PF")
                    End If

                End While
                lettDettagli.Close()
                tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>TOTALE €.</td>"
                tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00") & "</td>"
                tb2 = tb2 & "</tr>"

                tb2 = tb2 & "</table>"
                contenuto = Replace(contenuto, "$dettagli$", tb2)
                '*****************FINE SCRITTURA DETTAGLI
                contenuto = Replace(contenuto, "$imp_letterale$", "EURO " & NumeroInLettere(Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00")))

            End If
            myReader1.Close()

            tb1 = "<table style='width:100%;'>"
            tb2 = "<table style='width:100%;'>"
            Dim tb3 As String = "<table style='width:100%;'>"
            Dim tbAnBp As String = "<table style='width:100%;'>"

            par.cmd.CommandText = "SELECT PF_VOCI.*,PRENOTAZIONI.IMPORTO_PRENOTATO FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PRENOTAZIONI WHERE PF_VOCI.ID IN (" & idvocePf & ")" _
                & " AND PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PRENOTAZIONI.ID_PAGAMENTO = " & ID
            myReader1 = par.cmd.ExecuteReader

            While myReader1.Read
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:12pt;font-family :Arial ;'> " & par.IfNull(myReader1("CODICE"), "") & "</td>"
                tb1 = tb1 & "</tr>"


                tbAnBp = tbAnBp & "<tr><td style='text-align: left; font-size:12pt;font-family :Arial ;'> " & par.AnnoBPPag(ID) & "</td>"
                tbAnBp = tbAnBp & "</tr>"

                tb2 = tb2 & "<tr><td style='text-align: left; font-size:12pt;font-family :Arial ;'> " & par.IfNull(myReader1("DESCRIZIONE"), "") & "</td>"
                tb2 = tb2 & "</tr>"

                tb3 = tb3 & "<tr><td style='text-align: right; font-size:12pt;font-family :Arial ;'> €." & Format(par.IfNull(myReader1("IMPORTO_PRENOTATO"), 0), "##,##0.00") & "</td>"
                tb3 = tb3 & "</tr>"

            End While

            tb1 = tb1 & "</table>"
            tbAnBp = tbAnBp & "</table>"
            tb2 = tb2 & "</table>"
            tb3 = tb3 & "</table>"
            contenuto = Replace(contenuto, "$annobp$", tbAnBp)
            contenuto = Replace(contenuto, "$cod_capitolo$", tb1)
            contenuto = Replace(contenuto, "$voce_pf$", tb2)
            contenuto = Replace(contenuto, "$TOTSING$", tb3)

            myReader1.Close()

            par.cmd.CommandText = "SELECT COD_ANA FROM OPERATORI WHERE ID = " & Session.Item("ID_OPERATORE")
            myReader1 = par.cmd.ExecuteReader

            If myReader1.Read Then
                Matricola = par.IfNull(myReader1("COD_ANA"), "n.d.")
            End If
            myReader1.Close()
            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 20
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "AttPagamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\NuoveImm\"))
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','AttPagamento','');</script>")





            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try


    End Sub
    '******************************************************************************
    '                               NumeroToLettere
    '
    '                Converte il numero intero in lettere
    '
    ' Input : ImportoN                -->Importo Numerico
    '
    ' Ouput : NumeroToLettere         -->Il numero in lettere
    '******************************************************************************
    Function NumeroInLettere(ByVal Numero As String) As String

        '************************
        'Gestisce la virgola
        '************************
        Dim PosVirg As Integer
        Dim Lettere As String

        Numero$ = ChangeStr(Numero$, ".", "")
        PosVirg% = InStr(Numero$, ",")

        If PosVirg% Then
            Lettere$ = NumInLet(Mid(Numero$, 1, Len(Numero) + PosVirg% - 1))
            Lettere$ = Lettere$ & "\" & Format(CInt(Mid(Numero$, PosVirg% + 1, Len(Numero$))), "00")
        Else
            Lettere$ = NumInLet(CDbl(Numero$))
        End If

        NumeroInLettere = Lettere$

    End Function

    Private Function NumInLet(ByVal N As Double) As String

        '************************************************
        'inizializzo i due arry di numeri
        '************************************************
        SetNumeri()

        Dim ValT As Double     'Valore Temporaneo per la conversione
        Dim iCent As Integer    'Valore su cui calcolare le centinaia
        Dim L As String     'Importo in Lettere

        NumInLet = "zero"

        If N = 0 Then Exit Function

        ValT = N
        L = ""

        'miliardi
        iCent = Int(ValT / 1000000000.0#)
        If iCent Then
            If iCent = 1 Then
                L = "unmiliardo"
            Else
                L = LCent(iCent) + "miliardi"
            End If
            ValT = ValT - CDbl(iCent) * 1000000000.0#
        End If

        'milioni
        iCent = Int(ValT / 1000000.0#)
        If iCent Then
            If iCent = 1 Then
                L = L + "unmilione"
            Else
                L = L + LCent(iCent) + "milioni"
            End If
            ValT = ValT - CDbl(iCent) * 1000000.0#
        End If

        'miliaia
        iCent = Int(ValT / 1000)
        If iCent Then
            If iCent = 1 Then
                L = L + "mille"
            Else
                L = L + LCent(iCent) + "mila"
            End If
            ValT = ValT - CDbl(iCent) * 1000
        End If

        ''centinaia
        'If ValT Then
        '    L = L + LCent(CInt(ValT))
        'End If
        If ValT Then
            L = L + LCent(Fix(CDbl(ValT)))
        End If

        NumInLet = L

    End Function

    Function LCent(ByVal N As Integer) As String

        ' Ritorna xx% (1/999) convertito in lettere
        Dim Numero As String
        Dim Lettere As String
        Dim Centinaia As Integer
        Dim Decine As Integer
        Dim x As Integer
        Dim Unita As Integer
        Dim sDec As String

        Numero$ = Format(N, "000")

        Lettere$ = ""
        Centinaia% = Val(Left$(Numero$, 1))
        If Centinaia% Then
            If Centinaia% > 1 Then
                Lettere = sUnita(Centinaia%)
            End If
            Lettere = Lettere + "cento"
        End If

        Decine% = (N Mod 100)
        If Decine% Then
            Select Case Decine%
                Case Is >= 20                               'Decine
                    sDec = sDecina(Val(Mid$(Numero$, 2, 1)))
                    x% = Len(sDec)
                    Unita% = Val(Right$(Numero$, 1))          'Unita
                    If Unita% = 1 Or Unita% = 8 Then x% = x% - 1
                    Lettere$ = Lettere$ & Left(sDec, x%) & sUnita(Unita%)    'Tolgo l'ultima lettera della decina per i
                Case Else
                    Lettere$ = Lettere$ + sUnita(Decine)
            End Select
        End If

        LCent$ = Lettere$

    End Function


    Sub SetNumeri()

        '************************************************
        ' Stringhe per traslitterazione numeri
        '************************************************
        sUnita(1) = "uno"
        sUnita(2) = "due"
        sUnita(3) = "tre"
        sUnita(4) = "quattro"
        sUnita(5) = "cinque"
        sUnita(6) = "sei"
        sUnita(7) = "sette"
        sUnita(8) = "otto"
        sUnita(9) = "nove"
        sUnita(10) = "dieci"
        sUnita(11) = "undici"
        sUnita(12) = "dodici"
        sUnita(13) = "tredici"
        sUnita(14) = "quattordici"
        sUnita(15) = "quindici"
        sUnita(16) = "sedici"
        sUnita(17) = "diciassette"
        sUnita(18) = "diciotto"
        sUnita(19) = "diciannove"

        sDecina(1) = "dieci"
        sDecina(2) = "venti"
        sDecina(3) = "trenta"
        sDecina(4) = "quaranta"
        sDecina(5) = "cinquanta"
        sDecina(6) = "sessanta"
        sDecina(7) = "settanta"
        sDecina(8) = "ottanta"
        sDecina(9) = "novanta"

    End Sub

    '*********************************************************************
    '                ChangeStr - da usare con versioni minori del Vb6
    '
    'Input  = Stringa                           -->Da convertire
    '         Lettera da sostituire             -->Da convertire
    '         Nuova lettera da rimpiazzare      -->Da convertire
    '
    'Ouput  = Stringa rimpiazzata
    '
    '*********************************************************************
    Function ChangeStr(ByRef sBuffer As String, ByRef OldChar As String, _
                       ByRef NewChar As String) As String

        Dim TmpBuf As String
        Dim p As Integer

        On Error GoTo ErrChangeStr

        ChangeStr$ = ""   'Default Error

        TmpBuf$ = sBuffer$
        p% = InStr(TmpBuf$, OldChar$)
        Do While p > 0
            TmpBuf$ = Left$(TmpBuf$, p% - 1) + NewChar$ + Mid$(TmpBuf$, p% + Len(OldChar$))
            p% = InStr(p% + Len(NewChar$), TmpBuf$, OldChar$)
        Loop
        ChangeStr$ = TmpBuf$

        Exit Function

ErrChangeStr:
        ChangeStr$ = ""

    End Function


    Protected Sub btnPagamento_Click1(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPagamento.Click
        Try

            If txtConferma.Value = 1 Then
                If par.AggiustaData(Me.txtDScadenza.Text) < Format(Now, "yyyyMMdd") Then
                    Response.Write("<script>alert('La data scadenza non può essere precedente a quella odierna!');</script>")

                    idGestione.Value = 0
                    Importo.Value = 0
                    idCond.Value = 0

                    Exit Sub
                End If


                If idCond.Value <> 0 And idGestione.Value <> 0 And Importo.Value > 0 And nRata.Value > 0 Then
                    Dim Prenotazioni As String = ""
                    Dim Rimanente As Decimal = 0
                    Dim Stanziamento As Decimal = 0
                    Dim ElVociPf As String = ""
                    Dim ElVociPfImporto As String = ""
                    Dim Condition As String = ""

                    Dim readRim As Decimal = 0
                    '*******************APERURA CONNESSIONE*********************
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If

                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.PRENOTAZIONI WHERE ID IN " _
                                        & "(SELECT ID_PRENOTAZIONE FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                        & "WHERE ID_GESTIONE = " & idGestione.Value & "AND N_RATA = " & nRata.Value & ")"
                    myReader1 = par.cmd.ExecuteReader
                    While myReader1.Read
                        If String.IsNullOrEmpty(Prenotazioni) Then
                            Prenotazioni = par.IfNull(myReader1("ID"), 0)
                        Else
                            Prenotazioni = Prenotazioni & "," & myReader1("ID")
                        End If
                    End While
                    myReader1.Close()
                    'par.cmd.CommandText = "SELECT PF_VOCI_STRUTTURA.*,SISCOM_MI.PF_VOCI.* FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_PIANO_FINANZIARIO = (SELECT PF_MAIN.ID FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID_STATO = 5 AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID AND SUBSTR(INIZIO,0,4) = '" & par.AggiustaData(txtScadenza.Value).ToString.Substring(0, 4) & "') AND CODICE = '1.6' AND PF_VOCI.ID = PF_VOCI_STRUTTURA.ID_VOCE"
                    'par.cmd.CommandText = "SELECT PF_VOCI_STRUTTURA.*,SISCOM_MI.PF_VOCI.* FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PF_VOCI_STRUTTURA " _
                    '                    & "WHERE ID_PIANO_FINANZIARIO = (SELECT PF_MAIN.ID FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                    '                    & "WHERE ID_STATO = 5 AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID AND SUBSTR(INIZIO,0,4) = '" _
                    '                    & Format(Now, "yyyy") & "') AND PF_VOCI.codice = '1.02.09' AND PF_VOCI.ID = PF_VOCI_STRUTTURA.ID_VOCE"

                    par.cmd.CommandText = "SELECT importo_prenotato,ID_VOCE_PF, ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI WHERE ID IN (" & Prenotazioni & ")"
                    '
                    myReader1 = par.cmd.ExecuteReader()
                    While myReader1.Read
                        readRim = 0
                        Stanziamento = 0
                        If par.IfNull(myReader1("ID_VOCE_PF_IMPORTO"), 0) > 0 Then
                            par.cmd.CommandText = " SELECT * FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_VOCE = (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID = " & myReader1("id_voce_pf_importo") & " ) AND ID_STRUTTURA = " & CM.Global.StCondominio & "" ' (SELECT ID_FILIALE FROM SISCOM_MI.LOTTI WHERE DESCRIZIONE LIKE '%CONDOMINIO%')"
                            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dt As New Data.DataTable
                            da.Fill(dt)
                            For Each row As Data.DataRow In dt.Rows
                                Stanziamento = Stanziamento + CDec(par.IfNull(row.Item("VALORE_LORDO"), 0) + par.IfNull(row.Item("ASSESTAMENTO_VALORE_LORDO"), 0) + par.IfNull(row.Item("VARIAZIONI"), 0))
                            Next
                            If String.IsNullOrEmpty(ElVociPfImporto) Then
                                ElVociPfImporto = myReader1("ID_VOCE_PF_IMPORTO")
                            Else
                                ElVociPfImporto = ElVociPfImporto & "," & myReader1("ID_VOCE_PF_IMPORTO")
                            End If
                            If Stanziamento > 0 Then

                                'If Not String.IsNullOrEmpty(ElVociPf) Then
                                '    Condition = "ID_VOCE_PF IN (" & ElVociPf & ")"
                                'End If
                                If Not String.IsNullOrEmpty(ElVociPfImporto) Then
                                    If Not String.IsNullOrEmpty(Condition) Then
                                        Condition = Condition & " OR "
                                    End If
                                    Condition = Condition & "ID_VOCE_PF_IMPORTO IN (" & ElVociPfImporto & ")"
                                End If
                                par.cmd.CommandText = "SELECT NVL(SUM(IMPORTO_PRENOTATO),0) as TOT_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE ( ID_VOCE_PF_IMPORTO = " & myReader1("ID_VOCE_PF_IMPORTO") & ")" _
                                                    & " AND TIPO_PAGAMENTO = 1 AND ID_PAGAMENTO IS NULL AND ID_STATO = 0  AND PRENOTAZIONI.ID NOT IN (" & Prenotazioni & ")"
                                Dim PagatiPrenotati As Decimal = 0
                                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                If lettore.Read Then
                                    PagatiPrenotati = par.IfNull(lettore("TOT_PRENOTATO"), 0)
                                End If
                                lettore.Close()

                                par.cmd.CommandText = "SELECT NVL(SUM(IMPORTO_APPROVATO),0) as TOT_PAGATO FROM SISCOM_MI.PRENOTAZIONI WHERE ( ID_VOCE_PF_IMPORTO = " & myReader1("ID_VOCE_PF_IMPORTO") & ")" _
                                                    & " AND TIPO_PAGAMENTO = 1 AND ID_STATO > 0"
                                lettore = par.cmd.ExecuteReader
                                If lettore.Read Then
                                    PagatiPrenotati = PagatiPrenotati + par.IfNull(lettore("TOT_PAGATO"), 0)
                                    '*******Differenza fra preventivato e importi fino a ora pagati
                                    Rimanente = Stanziamento - PagatiPrenotati
                                    If Rimanente <= 0 Then
                                        Response.Write("<script>alert('Sono stati spesi più soldi di quelli previsti nel Piano Finanziario!!Impossibile continuare!!');</script>")
                                        idGestione.Value = 0
                                        Importo.Value = 0
                                        lettore.Close()
                                        Exit Sub

                                    Else
                                        If Math.Round(Rimanente, 2) < Math.Round(CDec(par.IfNull(myReader1("importo_prenotato"), 0)), 2) Then

                                            Response.Write("<script>alert('L\'ammontare residuo preventivato per questa spesa è insufficiente a eseguirne la liquidazione!');</script>")
                                            idGestione.Value = 0
                                            Importo.Value = 0
                                            lettore.Close()
                                            Exit Sub


                                        End If

                                    End If

                                End If
                            Else
                                Response.Write("<script>alert('Nessun importo stanziato per questo tipo di pagamento oppure il piano finanziario non è stato ancora approvato!');</script>")
                                idGestione.Value = 0
                                Importo.Value = 0
                                Exit Sub

                            End If




                        ElseIf par.IfNull(myReader1("ID_VOCE_PF"), 0) > 0 Then

                            par.cmd.CommandText = "SELECT CODICE FROM SISCOM_MI.PF_VOCI WHERE ID = " & myReader1("id_voce_pf")
                            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            Dim figlio As Boolean
                            If lettore.Read Then
                                Dim l As Integer = par.IfNull(lettore("CODICE"), ".").ToString.Length
                                If l - par.IfNull(lettore("CODICE"), ".").ToString.Replace(".", "").Length >= 3 Then
                                    figlio = True
                                Else
                                    figlio = False
                                End If
                            End If
                            lettore.Close()

                            If figlio = True Then
                                par.cmd.CommandText = "SELECT * FROM siscom_mi.pf_voci,SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_VOCE =  pf_voci.id_voce_madre and PF_VOCI.ID = " & myReader1("id_voce_pf") & " AND ID_STRUTTURA = " & CM.Global.StCondominio & "" ' (SELECT ID_FILIALE FROM SISCOM_MI.LOTTI WHERE DESCRIZIONE LIKE '%CONDOMINIO%')"
                            Else
                                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_VOCE = " & myReader1("id_voce_pf") & " AND ID_STRUTTURA = " & CM.Global.StCondominio & "" ' (SELECT ID_FILIALE FROM SISCOM_MI.LOTTI WHERE DESCRIZIONE LIKE '%CONDOMINIO%')"
                            End If
                            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dt As New Data.DataTable
                            da.Fill(dt)
                            For Each row As Data.DataRow In dt.Rows
                                Stanziamento = CDec(par.IfNull(row.Item("VALORE_LORDO"), 0) + par.IfNull(row.Item("ASSESTAMENTO_VALORE_LORDO"), 0) + par.IfNull(row.Item("VARIAZIONI"), 0))
                            Next
                            If String.IsNullOrEmpty(ElVociPf) Then
                                ElVociPf = myReader1("ID_VOCE_PF")
                            Else
                                ElVociPf = ElVociPf & "," & myReader1("ID_VOCE_PF")
                            End If
                            If Stanziamento > 0 Then

                                If Not String.IsNullOrEmpty(ElVociPf) Then
                                    Condition = "ID_VOCE_PF IN (" & ElVociPf & ")"
                                End If
                                'If Not String.IsNullOrEmpty(ElVociPfImporto) Then
                                '    If Not String.IsNullOrEmpty(Condition) Then
                                '        Condition = Condition & " OR "
                                '    End If
                                '    Condition = Condition & "ID_VOCE_PF_IMPORTO IN (" & ElVociPfImporto & ")"
                                'End If
                                If figlio = True Then
                                    par.cmd.CommandText = "SELECT NVL(SUM(IMPORTO_PRENOTATO),0) as TOT_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE id_voce_pf in (select id from siscom_mi.pf_voci where id_voce_madre = (select distinct (id_voce_madre) from siscom_mi.pf_voci where id = " & myReader1("ID_VOCE_PF") & "))" _
                                                        & " AND TIPO_PAGAMENTO = 1 AND ID_PAGAMENTO IS NULL AND ID_STATO = 0 AND PRENOTAZIONI.ID NOT IN (" & Prenotazioni & ")"
                                Else
                                    par.cmd.CommandText = "SELECT NVL(SUM(IMPORTO_PRENOTATO),0) as TOT_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE id_voce_pf = " & myReader1("ID_VOCE_PF") & " " _
                                                        & " AND TIPO_PAGAMENTO = 1 AND ID_PAGAMENTO IS NULL AND ID_STATO = 0 AND PRENOTAZIONI.ID NOT IN (" & Prenotazioni & ")"
                                End If

                                Dim PagatiPrenotati As Decimal = 0
                                lettore = par.cmd.ExecuteReader
                                If lettore.Read Then
                                    PagatiPrenotati = par.IfNull(lettore("TOT_PRENOTATO"), 0)
                                End If
                                lettore.Close()
                                If figlio = True Then
                                    par.cmd.CommandText = "SELECT NVL(SUM(IMPORTO_APPROVATO),0) as TOT_PAGATO FROM SISCOM_MI.PRENOTAZIONI WHERE id_voce_pf in (select id from siscom_mi.pf_voci where id_voce_madre = (select distinct (id_voce_madre) from siscom_mi.pf_voci where id = " & myReader1("ID_VOCE_PF") & "))" _
                                                        & " AND TIPO_PAGAMENTO = 1 AND ID_STATO > 0"
                                Else
                                    par.cmd.CommandText = "SELECT NVL(SUM(IMPORTO_APPROVATO),0) as TOT_PAGATO FROM SISCOM_MI.PRENOTAZIONI WHERE id_voce_pf = " & myReader1("ID_VOCE_PF") & " " _
                                                        & " AND TIPO_PAGAMENTO = 1 AND ID_STATO > 0"

                                End If
                                lettore = par.cmd.ExecuteReader
                                If lettore.Read Then
                                    PagatiPrenotati = PagatiPrenotati + par.IfNull(lettore("TOT_PAGATO"), 0)
                                    '*******Differenza fra preventivato e importi fino a ora pagati
                                    Rimanente = Stanziamento - PagatiPrenotati
                                    If Rimanente <= 0 Then
                                        Response.Write("<script>alert('Sono stati spesi più soldi di quelli previsti nel Piano Finanziario!!Impossibile continuare!!');</script>")
                                        idGestione.Value = 0
                                        Importo.Value = 0
                                        lettore.Close()
                                        Exit Sub

                                    Else
                                        If Math.Round(Rimanente, 2) < Math.Round(CDec(par.IfNull(myReader1("importo_prenotato"), 0)), 2) Then

                                            Response.Write("<script>alert('L\'ammontare residuo preventivato per questa spesa è insufficiente a eseguirne la liquidazione!');</script>")
                                            idGestione.Value = 0
                                            Importo.Value = 0
                                            lettore.Close()
                                            Exit Sub
                                        End If

                                    End If

                                End If
                                lettore.Close()
                            Else
                                Response.Write("<script>alert('Nessun importo stanziato per questo tipo di pagamento oppure il piano finanziario non è stato ancora approvato!');</script>")
                                idGestione.Value = 0
                                Importo.Value = 0
                                Exit Sub

                            End If
                        End If


                    End While
                    myReader1.Close()

                    Dim Pagamento As String = CreaPagamento(Prenotazioni)
                    If Pagamento <> "" Then
                        Response.Write("<script>alert('Il pagamento è stato emesso e storicizzato!');</script>")
                        'lettore.Close()
                        'myReader1.Close()
                        '*********************CHIUSURA CONNESSIONE**********************
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        PdfPagamento(Pagamento)
                        CaricaTabella()
                        Me.VisDesc.Value = 0
                        idGestione.Value = 0
                        Importo.Value = 0
                        idCond.Value = 0

                        Response.Flush()
                        Exit Sub
                    Else
                        Response.Write("<script>alert('Operazione annullata!Non è stato possibile emettere il pagamento!');</script>")
                        'lettore.Close()
                        'myReader1.Close()
                        idGestione.Value = 0
                        Importo.Value = 0
                        idCond.Value = 0

                        '*********************CHIUSURA CONNESSIONE**********************
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    End If

                Else
                    Response.Write("<script>alert('Selezionare una riga per emettere il pagamento!');</script>")

                End If
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            'Response.Write("<script>alert('Funzione non ancora disponibile!In fase di sviluppo!');</script>")
            idGestione.Value = 0
            Importo.Value = 0
            idCond.Value = 0
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try

    End Sub

    Protected Sub btnPrePagamento_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPrePagamento.Click
        Try


            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            'par.cmd.CommandText = "SELECT ID, IBAN FROM SISCOM_MI.FORNITORI_IBAN WHERE ID_FORNITORE = (SELECT ID_FORNITORE FROM SISCOM_MI.CONDOMINI WHERE ID = " & idCond.Value & ")"
            par.cmd.CommandText = "SELECT ID, IBAN FROM SISCOM_MI.FORNITORI_IBAN WHERE ID_FORNITORE = (SELECT ID_FORNITORE FROM SISCOM_MI.CONDOMINI WHERE ID = " & idCond.Value & ") AND FORNITORI_IBAN.FL_ATTIVO = 1 "

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Me.cmbIbanFornitore.Items.Clear()
            While lettore.Read
                cmbIbanFornitore.Items.Add(New ListItem(par.IfNull(lettore("IBAN"), " "), par.IfNull(lettore("ID"), "")))
            End While
            lettore.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        End Try
    End Sub

    Protected Sub btnFiltra_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnFiltra.Click
        CaricaTabella(par.AggiustaData(Me.txtFiltrScad.Text))
    End Sub
End Class
