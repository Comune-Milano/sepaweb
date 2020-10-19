Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_Stampe
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim esercizio As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or (Session.Item("BP_GENERALE") <> 1 And Session.Item("MOD_ASS_CONV_COMU") <> 1) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        '##### CARICAMENTO PAGINA #####
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        If Not IsPostBack Then
            stampa()
        End If
    End Sub
    Protected Sub stampa()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        '### date ####
        par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_ASSESTAMENTO.ID='" & Request.QueryString("ID") & "' AND PF_ASSESTAMENTO.ID_PF_MAIN=PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID"
        Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettoreEsercizio.Read Then
            esercizio = par.IfNull(lettoreEsercizio("inizio"), "") & " - " & par.IfNull(lettoreEsercizio("fine"), "")
        End If
        lettoreEsercizio.Close()

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_ASSESTAMENTO WHERE ID='" & Request.QueryString("ID") & "'"
        Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        Dim id_stato As Integer = -1
        If Lettore.Read Then
            id_stato = par.IfNull(Lettore("id_stato"), -1)
        End If
        Lettore.Close()
        If id_stato = 3 Then
            Me.lblTitolo.Text = "ASSESTAMENTO ESERCIZIO FINANZIARIO - " & esercizio & " - CONVALIDA GESTORE"
            IdAssestamento.Value = Request.QueryString("ID")
            RiepilogoAssestamento()
            GeneraPDF()
        ElseIf id_stato = 5 Then
            Me.lblTitolo.Text = "ASSESTAMENTO ESERCIZIO FINANZIARIO - " & esercizio
            IdAssestamento.Value = Request.QueryString("ID")
            RiepilogoAssestamentoComune()
            GeneraPDFAss()
        Else
            Response.Write("<script>alert('Si è verificato un errore durante la stampa!');self.close();</script>")
        End If
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub
    Protected Sub RiepilogoAssestamento()
        Dim indicecolonna As Integer = TrovaIndiceColonna(datagrid1, "APPROVATO")
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim dtriepilogo As New Data.DataTable

            dtriepilogo.Clear()
            dtriepilogo.Rows.Clear()
            dtriepilogo.Columns.Clear()
            dtriepilogo.Columns.Add("CODICE")
            dtriepilogo.Columns.Add("VOCE")
            dtriepilogo.Columns.Add("RICHIESTO")
            dtriepilogo.Columns.Add("APPROVATO")
            Dim RIGA As Data.DataRow

            '#### SELEZIONO L'ASSESTAMENTO ####
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            If IdAssestamento.Value = "0" Then
                par.cmd.CommandText = "select id,id_stato from siscom_mi.pf_assestamento where id='" & Request.QueryString("ID") & "'"
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    IdAssestamento.Value = par.IfNull(lettore("ID"), 0)
                End If
                lettore.Close()
            End If
            '##################################

            '#### CREO ELENCO VOCI ######
            par.cmd.CommandText = "SELECT DISTINCT ID_ASSESTAMENTO,ID_VOCE,DESCRIZIONE,CODICE FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI," _
                & "SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=PF_ASSESTAMENTO_VOCI.ID_VOCE AND ID_ASSESTAMENTO=" & IdAssestamento.Value & " ORDER BY CODICE ASC"
            Dim ElencoVoci As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            While ElencoVoci.Read
                par.cmd.CommandText = "SELECT SUM(NVL(IMPORTO,0)) AS IMPORTO,SUM(NVL(IMPORTO_APPROVATO,0)) AS IMPORTO_APPROVATO FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI " _
                    & "WHERE ID_VOCE IN (" _
                    & "SELECT ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE CONNECT_BY_ISLEAF = 1 " _
                    & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH ID='" & par.IfNull(ElencoVoci("ID_VOCE"), "-1") & "') " _
                    & "AND ID_ASSESTAMENTO = " & IdAssestamento.Value
                Dim RICHIESTO As Decimal = 0
                Dim APPROVATO As Decimal = 0
                Dim lettoreImportoRichiesto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreImportoRichiesto.Read Then
                    If IdStato.Value = "5" Then
                        RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO"), 0)
                        APPROVATO = par.IfNull(lettoreImportoRichiesto("IMPORTO_APPROVATO"), 0)
                        'datagrid1.Columns.Item(indicecolonna).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                    ElseIf IdStato.Value = "3" Then
                        RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO"), 0)
                        APPROVATO = par.IfNull(lettoreImportoRichiesto("IMPORTO_APPROVATO"), 0)
                        'datagrid1.Columns.Item(indicecolonna).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                    Else
                        RICHIESTO = par.IfNull(lettoreImportoRichiesto("IMPORTO"), 0)
                        APPROVATO = par.IfNull(lettoreImportoRichiesto("IMPORTO_APPROVATO"), 0)
                    End If

                End If
                lettoreImportoRichiesto.Close()

                RIGA = dtriepilogo.NewRow()
                RIGA.Item("CODICE") = par.IfNull(ElencoVoci("CODICE"), "")
                RIGA.Item("VOCE") = par.IfNull(ElencoVoci("DESCRIZIONE"), "")
                RIGA.Item("RICHIESTO") = Format(RICHIESTO, "##,##0.00")
                RIGA.Item("APPROVATO") = Format(APPROVATO, "##,##0.00")
                dtriepilogo.Rows.Add(RIGA)

            End While
            ElencoVoci.Close()
            datagrid1.DataSource = dtriepilogo
            datagrid1.DataBind()
            datagrid1.Visible = True
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaVoci - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CaricaAssAler()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim ElVoci As String = ""
            Dim Assestamento As Decimal = 0
            Dim Approvato As Decimal = 0
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader

            If par.IfEmpty(Request.QueryString("ID"), "") <> "" Then
                IdAssestamento.Value = Request.QueryString("ID")
            End If
            If IdAssestamento.Value = 0 Then
                Response.Write("<script>alert('Nessun Assestamento trovato!Impossibile procedere');</script>")
                Exit Sub
            End If
            If Request.QueryString("CHIAMA") = "ASSEST" Then
                par.cmd.CommandText = "SELECT PF_VOCI.* ,'' AS BUDGET_INIZIALE,'' AS SPESO,'' AS RESIDUO,'' AS ASSESTAMENTO,'' AS APPROVATO," _
                                    & "(CASE WHEN(SELECT COUNT(*) FROM SISCOM_MI.PF_VOCI A WHERE A.ID_VOCE_MADRE = PF_VOCI.ID) = 0 THEN 'FIGLIO' ELSE 'MADRE' END)AS TIPO " _
                                    & "FROM SISCOM_MI.PF_VOCI WHERE (ID IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE = " & Session.Item("ID_OPERATORE") & ") OR ID_VOCE_MADRE IN " _
                                    & "(SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE = " & Session.Item("ID_OPERATORE") & " )OR ID_VOCE_MADRE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE IN(SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE = 673))	) " _
                                    & "AND ID IN (SELECT ID FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI WHERE ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") & " AND ID_ASSESTAMENTO = " & IdAssestamento.Value & ") order by codice asc"
            Else
                par.cmd.CommandText = "SELECT PF_VOCI.* ,'' AS BUDGET_INIZIALE,'' AS SPESO,'' AS RESIDUO,'' AS ASSESTAMENTO,'' AS APPROVATO," _
                                    & "(CASE WHEN(SELECT COUNT(*) FROM SISCOM_MI.PF_VOCI A WHERE A.ID_VOCE_MADRE = PF_VOCI.ID) = 0 THEN 'FIGLIO' ELSE 'MADRE' END)AS TIPO " _
                                    & "FROM SISCOM_MI.PF_VOCI WHERE ID IN (SELECT ID_VOCE FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI WHERE ID_ASSESTAMENTO = " & IdAssestamento.Value & ") order by codice asc"

            End If
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            For Each r As Data.DataRow In dt.Rows
                Assestamento = 0
                Approvato = 0
                ElVoci = ""
                '*******SELEZIONE DELLE VOCI FIGLIE ASSOCIATE ALLA VOCE PRINCIPALE
                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI " _
                                  & "  where ID=" & r.Item("ID") _
                                  & "     or ID_VOCE_MADRE=" & r.Item("ID") _
                                  & "     or ID_VOCE_MADRE in (select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & r.Item("ID") & ") order by CODICE"

                lettore = par.cmd.ExecuteReader
                While lettore.Read
                    If ElVoci = "" Then
                        ElVoci = par.IfNull(lettore(0), "")
                    Else
                        ElVoci = ElVoci & "," & par.IfNull(lettore(0), "")
                    End If

                End While
                lettore.Close()
                '**********END ---- SELEZIONE DELLE VOCI FIGLIE ASSOCIATE ALLA VOCE PRINCIPALE

                '**********SELEZIONE DELL'IMPORTO ASSESTAMENTO

                par.cmd.CommandText = "select id_voce,importo,importo_approvato from siscom_mi.pf_assestamento_voci where id_voce in ( " & ElVoci & ")  and id_assestamento = " & IdAssestamento.Value

                lettore = par.cmd.ExecuteReader
                Assestamento = 0

                While lettore.Read
                    par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & lettore("ID_VOCE")

                    lettore2 = par.cmd.ExecuteReader()
                    If lettore2.Read Then

                    Else

                        Assestamento = Assestamento + Decimal.Parse(par.IfNull(lettore("importo"), "0"))
                        Approvato = Approvato + Decimal.Parse(par.IfNull(lettore("importo_approvato"), "0"))

                    End If
                    lettore2.Close()

                End While
                lettore.Close()
                r.Item("ASSESTAMENTO") = Format(Assestamento, "##,##0.00")
                r.Item("APPROVATO") = Format(Approvato, "##,##0.00")
            Next

            Me.DgvStAler.DataSource = dt
            Me.DgvStAler.DataBind()

            par.cmd.CommandText = "SELECT INIZIO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID AND PF_MAIN.ID_STATO = 5"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                lblTitolo.Text = lblTitolo.Text & par.IfNull(lettore("INIZIO"), "0000").ToString.Substring(0, 4)
            End If
            lettore.Close()
            If Request.QueryString("CHIAMA") = "ASSEST" Then
                par.cmd.CommandText = "SELECT (NOME||' '||INDIRIZZI.DESCRIZIONE||', '|| CIVICO||' - '||CAP||' '||LOCALITA)AS FILIALE FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID = TAB_FILIALI.ID_INDIRIZZO AND tab_filiali.ID = " & Session.Item("ID_STRUTTURA")
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblTitolo.Text = lblTitolo.Text & vbCrLf & par.IfNull(lettore("FILIALE"), "")
                End If
                lettore.Close()
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub
    Protected Sub RiepilogoAssestamentoComune()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim ElVoci As String = ""
            Dim Approvato As Decimal = 0
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader
            Dim indiceColonna As Integer = TrovaIndiceColonna(DgvStAler, "APPROVATO")
            Dim indiceColonnaCap As Integer = TrovaIndiceColonna(DgvApprAssCapitoli, "ASSESTAMENTO")

            If IdAssestamento.Value = 0 Then
                If par.IfEmpty(Request.QueryString("ID"), "") <> "" Then
                    IdAssestamento.Value = Request.QueryString("ID")
                Else
                    'par.cmd.CommandText = "select max(id) from siscom_mi.pf_assestamento where (id_stato = 3 or id_stato = 5)"
                    par.cmd.CommandText = "SELECT MAX(ID),ID_STATO FROM SISCOM_MI.PF_ASSESTAMENTO WHERE (ID_STATO=3 OR ID_STATO=5) GROUP BY ID_STATO ORDER BY MAX(ID) DESC"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        IdAssestamento.Value = par.IfNull(lettore(0), 0)
                        If par.IfNull(lettore(1), 3) = 5 Then
                            'lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL COMUNE"
                            DgvStAler.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                            DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                        ElseIf par.IfNull(lettore(1), 3) = 3 Then
                            'lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL GESTORE"
                            DgvStAler.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                            DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                        End If
                    End If
                End If
            End If
            If IdAssestamento.Value = 0 Then
                Response.Write("<script>alert('Nessun Assestamento approvato dal Gestore trovato!Impossibile procedere');document.location.href='../../pagina_home.aspx';</script>")
                Exit Sub
            End If

            '### date ####
            par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, CASE WHEN SISCOM_MI.PF_ASSESTAMENTO.ID_STATO=5 THEN TO_CHAR(TO_DATE(DATA_APP_COMUNE,'yyyyMMdd'),'dd/MM/yyyy') ELSE TO_CHAR(TO_DATE(DATA_INSERIMENTO,'yyyyMMdd'),'dd/MM/yyyy') END AS DATA_ASS, TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value & " AND PF_ASSESTAMENTO.ID_PF_MAIN=PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID"
            Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreEsercizio.Read Then
                'esercizio.Text = par.IfNull(lettoreEsercizio("inizio"), "") & " - " & par.IfNull(lettoreEsercizio("fine"), "")
                If par.IfNull(lettoreEsercizio("id_stato"), 1) = 5 Then
                    'lblAssestamentoData.Text = "Assestamento approvato il " & par.IfNull(lettoreEsercizio("data_Ass"), "")
                Else
                    'lblAssestamentoData.Text = "Assestamento del " & par.IfNull(lettoreEsercizio("data_Ass"), "")
                End If
            End If
            lettoreEsercizio.Close()
            '#######


            par.cmd.CommandText = "SELECT PF_VOCI.*,PF_CAPITOLI.COD AS COD_CAPITOLO, PF_CAPITOLI.DESCRIZIONE AS CAPITOLO ,'' AS APPROVATO " _
                                & "FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_CAPITOLI WHERE PF_CAPITOLI.ID = ID_CAPITOLO AND  ( PF_VOCI.ID IN " _
                                & "(SELECT PF_ASSESTAMENTO_VOCI.ID_VOCE FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI " _
                                & "WHERE ID_ASSESTAMENTO = " & IdAssestamento.Value & " )OR PF_VOCI.ID_VOCE_MADRE IN " _
                                & "(SELECT PF_ASSESTAMENTO_VOCI.ID_VOCE FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI " _
                                & "WHERE ID_ASSESTAMENTO = " & IdAssestamento.Value & " )) ORDER BY CODICE"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            For Each r As Data.DataRow In dt.Rows
                Approvato = 0
                ElVoci = ""

                '*******SELEZIONE DELLE VOCI FIGLIE ASSOCIATE ALLA VOCE PRINCIPALE
                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI " _
                                  & "  where ID=" & r.Item("ID") _
                                  & "     or ID_VOCE_MADRE=" & r.Item("ID") _
                                  & "     or ID_VOCE_MADRE in (select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & r.Item("ID") & ") order by CODICE"

                lettore = par.cmd.ExecuteReader
                While lettore.Read
                    If ElVoci = "" Then
                        ElVoci = par.IfNull(lettore(0), "")
                    Else
                        ElVoci = ElVoci & "," & par.IfNull(lettore(0), "")
                    End If

                End While
                lettore.Close()
                '**********END ---- SELEZIONE DELLE VOCI FIGLIE ASSOCIATE ALLA VOCE PRINCIPALE

                '**********SELEZIONE DELL'IMPORTO ASSESTAMENTO

                par.cmd.CommandText = "select id_voce,importo_approvato from siscom_mi.pf_assestamento_voci where id_voce in ( " & ElVoci & ") AND ID_ASSESTAMENTO=" & IdAssestamento.Value

                lettore = par.cmd.ExecuteReader

                While lettore.Read
                    par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & lettore("ID_VOCE")

                    lettore2 = par.cmd.ExecuteReader()
                    If lettore2.Read Then

                    Else

                        Approvato = Approvato + Decimal.Parse(par.IfNull(lettore("importo_approvato"), "0"))

                    End If
                    lettore2.Close()

                End While
                lettore.Close()

                r.Item("APPROVATO") = Format(Approvato, "##,##0.00")
                '**********SELEZIONE DELL'IMPORTO ASSESTAMENTO


            Next

            Me.DgvStAler.DataSource = dt
            Me.DgvStAler.DataBind()

            par.cmd.CommandText = "SELECT cod, descrizione," _
                                & "TRIM(TO_CHAR(NVL((SELECT SUM(nvl(importo_approvato,0)) FROM siscom_mi.pf_assestamento_voci " _
                                & "WHERE id_assestamento = " & IdAssestamento.Value & " and id_voce IN (SELECT ID FROM siscom_mi.pf_voci " _
                                & "WHERE id_capitolo = pf_capitoli.ID)),0),'9G999G999G990D99')) AS assestamento " _
                                & "FROM siscom_mi.pf_capitoli"

            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            dt.Dispose()
            dt = New Data.DataTable

            da.Fill(dt)
            Me.DgvApprAssCapitoli.DataSource = dt
            Me.DgvApprAssCapitoli.DataBind()




            par.cmd.CommandText = "select id_stato from siscom_mi.pf_assestamento where id = " & IdAssestamento.Value
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                If par.IfNull(lettore(0), 1) <> 3 Then
                    'frmSoloLettura()
                End If
            End If
            lettore.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaVoci - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Private Sub GeneraPDF()
        Try
            '***************************************************************************************************
            '******************************************CREO IL FILE PDF*****************************************
            '***************************************************************************************************

            Dim NomeFile As String = "ASS" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)
            datagrid1.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString

            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter



            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PageWidth = 800
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 60
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderText = Me.lblTitolo.Text
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 14
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold

            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10

            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.Blue
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = ""
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.Color.Red
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag."
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & NomeFile)
            Response.Write("<script>self.close();window.open('..\\..\\..\\FileTemp\\" & NomeFile & "','Stampe');</script>")



        Catch ex As Exception

        End Try

    End Sub

    Private Sub GeneraPDFAss()
        Try
            '***************************************************************************************************
            '******************************************CREO IL FILE PDF*****************************************
            '***************************************************************************************************

            Dim NomeFile As String = "ASS" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)
            DgvStAler.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString

            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter



            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PageWidth = 800
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 60
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderText = Me.lblTitolo.Text
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 14
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold

            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10

            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.Blue
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = ""
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.Color.Red
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag."
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & NomeFile)
            Response.Write("<script>self.close();window.open('..\\..\\..\\FileTemp\\" & NomeFile & "','Stampe');</script>")



        Catch ex As Exception

        End Try

    End Sub

    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer
        TrovaIndiceColonna = -1
        Dim Indice As Integer = 0
        Try
            For Each c As DataGridColumn In dgv.Columns
                If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                    TrovaIndiceColonna = Indice
                    Exit For
                End If
                Indice = Indice + 1
            Next

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = "TrovaIndiceColonna - " & ex.Message
        End Try

        Return TrovaIndiceColonna

    End Function
End Class
