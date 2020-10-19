Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_ConvAssestAler
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("BP_GENERALE") <> 1 Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            Dim Str As String
            Str = "<div id=""splash"" style=""border: thin dashed #000066; position: absolute; z-index: 500;" _
                & "text-align: center; font-size: 10px; width: 777px; height: 525px; visibility: visible;" _
                & "vertical-align: top; line-height: normal; top: 15px; left: 12px; background-color: #FFFFFF;"">" _
                & "<table style=""height: 100%; width: 100%"">" _
                & "<tr><td style=""width: 100%; height: 100%; vertical-align: middle; text-align: center"">" _
                & "<img src='../../../CONDOMINI/Immagini/load.gif' alt='caricamento in corso' />" _
                & "<br /><br /><span id=""lblCaricamento"" style=""font-family:Arial;font-size:10pt;"">caricamento in corso...</span>" _
                & "</td></tr></table></div>"

            'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            'Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            'Str = Str & "<" & "/div>"
            Response.Write(Str)
            If Not IsPostBack Then
                URLdiProvenienza.Value = Request.ServerVariables("HTTP_REFERER")
                Response.Flush()
                'UN ASSESTAMENTO è CONVALIDABILE DAL GESTORE QUANDO è IN STATO 1 ED è COMPLETO (FLAG COMPLETO=1 PER TUTTE LE VOCI IN PF_ASSESTAMENTO_VOCI)
                Select Case ControlloAssestamento()
                    Case 0
                        'ASSESTAMENTO CON STATO=3 PRESENTE, CARICO VOCI IN MODALITà DI SOLA LETTURA
                        AssestamentoCompletato.Text = "ASSESTAMENTO APPROVATO DAL GESTORE"
                        CaricaVoci()
                        SettaHLinkSottovoci()
                        frmSoloLettura()
                    Case 1
                        'è POSSIBILE APRIRE LA PAGINA PER LA CONVALIDA DA PARTE DEL GESTORE DELL'ASSESTAMENTO
                        'QUINDI CARICO LE VOCI CON GLI IMPORTI DI ASSESTAMENTO RICHIESTI
                        AssestamentoCompletato.Text = ""
                        'btnStampa.Visible = False
                        CaricaVoci()
                        SettaHLinkSottovoci()
                    Case 2
                        'L'ASSESTAMENTO NON PUò ESSERE CONVALIDATO DAL GESTORE PERCHè NON TUTTE LE STRUTTURE SONO STATE COMPLETATE
                        Response.Write("<script>alert('Attenzione! L\'assestamento non è convalidabile dal Gestore perchè non tutte le strutture sono state completate!');parent.main.location.replace('CompletaAssestamento.aspx');</script>")
                    Case 3
                        'NON ESISTE NESSUN ASSESTAMENTO CON STATO CARICAMENTO IMPORTI QUINDI REINDIRIZZO IN SCEGLI ASSESTAMENTO
                        Response.Write("<script>alert('Attenzione! Non è presente nessun Assestamento convalidabile dal Gestore!');parent.main.location.replace('../../pagina_home.aspx');</script>")
                    Case Else
                        'DI DEFAULT NON ESISTE NESSUN ASSESTAMENTO CON STATO CARICAMENTO IMPORTI QUINDI REINDIRIZZO IN SCEGLI ASSESTAMENTO
                        Response.Write("<script>alert('Attenzione! Non è presente nessun Assestamento convalidabile dal Gestore!');parent.main.location.replace('../../pagina_home.aspx');</script>")
                End Select

            End If
            If Session.Item("MOD_ASS_CONV_ALER") <> 1 Then
                btnConfirm.Visible = False
                Exit Sub
            End If
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Function ControlloAssestamento() As Integer
        Dim ritorno As Integer = 3
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'CONTROLLO CHE ESISTA UN ASSESTAMENTO IN STATO APPROVATO DAL GESTORE STATO=3
            par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.PF_ASSESTAMENTO WHERE ID_STATO=3"
            Dim LettoreStatoAssestamento As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim NumAssestamentiAttivi As Integer = 0
            If LettoreStatoAssestamento.Read Then
                NumAssestamentiAttivi = par.IfNull(LettoreStatoAssestamento(0), 0)
            End If
            LettoreStatoAssestamento.Close()

            If NumAssestamentiAttivi = 1 Then
                'ESISTE L'ASSESTAMENTO IN STATO 3
                ritorno = 0
            Else
                'CONTROLLO CHE ESISTA UN ASSESTAMENTO IN CARICAMENTO IMPORTI CON STATO=1
                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.PF_ASSESTAMENTO WHERE ID_STATO=1 or id_stato=2"
                LettoreStatoAssestamento = par.cmd.ExecuteReader
                NumAssestamentiAttivi = 0
                If LettoreStatoAssestamento.Read Then
                    NumAssestamentiAttivi = par.IfNull(LettoreStatoAssestamento(0), 0)
                End If
                LettoreStatoAssestamento.Close()

                If NumAssestamentiAttivi = 1 Then
                    'ESISTE L'ASSESTAMENTO IN CORSO CON STATO=1
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_ASSESTAMENTO_VOCI WHERE (ID_STATO=1 or id_stato=2) AND PF_ASSESTAMENTO_VOCI.ID_ASSESTAMENTO=PF_ASSESTAMENTO.ID AND COMPLETO=0"
                    Dim LettoreVoci As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim numero As Integer = 0
                    If LettoreVoci.Read Then
                        numero = par.IfNull(LettoreVoci(0), 0)
                    End If
                    LettoreVoci.Close()
                    If numero = 0 Then
                        'ESISTE UN ASSESTAMENTO CON STATO=1 E VOCI TUTTE COMPLETE
                        ritorno = 1
                    Else
                        'L'ASSESTAMENTO NON PUò ESSERE CONVALIDATO DAL GESTORE PERCHè NON TUTTE LE STRUTTURE SONO STATE COMPLETATE
                        ritorno = 2
                    End If
                Else
                    'NON ESISTE NESSUN ASSESTAMENTO CON STATO CARICAMENTO IMPORTI QUINDI REINDIRIZZO IN SCEGLI ASSESTAMENTO
                    ritorno = 3
                End If
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = "ControlloAssestamento - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
        Return ritorno
    End Function

    Private Sub CaricaVoci()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim ElVoci As String = ""
            Dim Assestamento As Decimal = 0
            Dim Approvato As Decimal = 0
            Dim ApprovatoRiep As Decimal = 0
            Dim approvString As String = ""
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader

            If IdAssestamento.Value = 0 Then
                If par.IfEmpty(Request.QueryString("ID"), "") <> "" Then
                    IdAssestamento.Value = Request.QueryString("ID")
                Else
                    par.cmd.CommandText = "select max(id)  from siscom_mi.pf_assestamento where id_stato >= 1 and id_stato < 5"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        IdAssestamento.Value = par.IfNull(lettore(0), 0)
                    End If
                End If
            End If

            '***************************CONDIZIONI PER VERIFICARE SE LA FINESTRA DOVRà ESSERE MOSTRATA IN SOLO LETTURA***********************

            par.cmd.CommandText = "select id_stato from siscom_mi.pf_assestamento where id = " & IdAssestamento.Value
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                If par.IfNull(lettore(0), 1) > 2 Then
                    frmSoloLettura()
                End If
            End If
            lettore.Close()

            par.cmd.CommandText = "select id_voce from siscom_mi.pf_assestamento_voci where id_assestamento = " & IdAssestamento.Value & " and completo = 1"
            lettore = par.cmd.ExecuteReader
            If lettore.HasRows = False Then
                frmSoloLettura()
                Response.Write("<script>alert('Nessuna Struttura ha completato l\'assestamento!Impossibile apportare modifiche!');</script>")
            End If
            lettore.Close()
            '**********************************************FINE CONDIZIONI DI SOLA LETTURA****************************************************


            '### date ####
            par.cmd.CommandText = "SELECT PF_ASSESTAMENTO.ID_STATO, CASE WHEN SISCOM_MI.PF_ASSESTAMENTO.ID_STATO=3 THEN TO_CHAR(TO_DATE(DATA_APP_ALER,'yyyyMMdd'),'dd/MM/yyyy') ELSE TO_CHAR(TO_DATE(DATA_INSERIMENTO,'yyyyMMdd'),'dd/MM/yyyy') END AS DATA_ASS, TO_CHAR(TO_DATE(INIZIO,'yyyyMMdd'),'dd/MM/yyyy') AS INIZIO, TO_CHAR(TO_DATE(FINE,'yyyyMMdd'),'dd/MM/yyyy') AS FINE FROM SISCOM_MI.PF_ASSESTAMENTO,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PF_ASSESTAMENTO.ID=" & IdAssestamento.Value & " AND PF_ASSESTAMENTO.ID_PF_MAIN=PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID"
            Dim lettoreEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreEsercizio.Read Then
                esercizio.Text = par.IfNull(lettoreEsercizio("inizio"), "") & " - " & par.IfNull(lettoreEsercizio("fine"), "")
                If par.IfNull(lettoreEsercizio("id_stato"), 1) = 3 Then
                    lblAssestamentoData.Text = "Assestamento approvato dal Gestore il " & par.IfNull(lettoreEsercizio("data_Ass"), "")
                Else
                    lblAssestamentoData.Text = "Assestamento del " & par.IfNull(lettoreEsercizio("data_Ass"), "")
                End If
            End If
            lettoreEsercizio.Close()
            '#######


            par.cmd.CommandText = "SELECT PF_VOCI.* ,'' AS BUDGET_INIZIALE,'' AS SPESO,'' AS RESIDUO,'' AS ASSESTAMENTO,'' AS APPROVATO," _
                    & "(CASE WHEN(SELECT COUNT(*) FROM SISCOM_MI.PF_VOCI A WHERE A.ID_VOCE_MADRE = PF_VOCI.ID) = 0 THEN 'FIGLIO' ELSE 'MADRE' END)AS TIPO " _
                    & "FROM SISCOM_MI.PF_VOCI WHERE ID IN (SELECT ID_VOCE FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI WHERE ID_ASSESTAMENTO = " & IdAssestamento.Value & ") order by codice asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable()
            Dim dtCopia As New Data.DataTable()
            da.Fill(dt)
            da.Fill(dtCopia)

            Dim cont As String = "0"
            Dim count As Integer = 0
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
                ApprovatoRiep = 0
                approvString = ""
                While lettore.Read

                    par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & lettore("ID_VOCE")

                    lettore2 = par.cmd.ExecuteReader()
                    If lettore2.Read Then
                        '??
                    Else

                        '*******MODIFICA APPROVATO******
                        Assestamento = Assestamento + Decimal.Parse(par.IfNull(lettore("importo"), "0"))
                        'Approvato = Approvato + Decimal.Parse(par.IfNull(lettore("importo_approvato"), "0"))
                        If par.IfNull(lettore("importo_approvato"), -1) = -1 Then
                            approvString = approvString + Nothing
                        Else
                            Approvato = Approvato + Decimal.Parse(par.IfNull(lettore("importo_approvato"), "0"))
                            approvString = CStr(Format(Approvato, "##,##0.00"))
                        End If
                        '*******************************

                    End If
                    lettore2.Close()

                End While
                lettore.Close()

                '******NUOVO******
                r.Item("APPROVATO") = approvString
                r.Item("DESCRIZIONE") = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.showModalDialog('ConvAssestStruttura.aspx?IDASS=" & IdAssestamento.Value & "&IDVOCE=" & r.Item("ID") & "&SL=" & soloLettura.Value & "','window','status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');document.getElementById('btnRicarica').click();" & Chr(34) & ">" & r.Item("DESCRIZIONE") & "</a>"
                r.Item("ASSESTAMENTO") = Format(Assestamento, "##,##0.00")

                dtCopia.Rows(count).Item("APPROVATO") = approvString
                dtCopia.Rows(count).Item("ASSESTAMENTO") = Format(Assestamento, "##,##0.00")
                '*****************

                ''*************VECCHIO****************
                'r.Item("ASSESTAMENTO") = Format(Assestamento, "##,##0.00")
                ''If Approvato > 0 Then
                'r.Item("APPROVATO") = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.showModalDialog('ConvAssestStruttura.aspx?IDASS=" & IdAssestamento.Value & "&IDVOCE=" & r.Item("ID") & "&SL=" & Me.soloLettura.Value & "','window','status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');document.getElementById('btnRicarica').click();" & Chr(34) & ">" & Format(Approvato, "##,##0.00") & "</a>"
                ''Else
                ''r.Item("APPROVATO") = Format(Assestamento, "##,##0.00")
                ''End If
                ''**********SELEZIONE DELL'IMPORTO ASSESTAMENTO

                If r.Item("ASSESTAMENTO") <> 0 And Trim(r.Item("APPROVATO")) = "" Then
                    cont = "1"
                End If
                count = count + 1
            Next
            CONThidden.Value = cont
            DgvApprAssest.DataSource = dt
            DgvApprAssest.DataBind()
            DgvApprAssestCopia.DataSource = dtCopia
            DgvApprAssestCopia.DataBind()
            DgvApprAssest.HeaderStyle.ForeColor = Drawing.Color.White

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = "CaricaVoci - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub SettaHLinkSottovoci()
        Try
            'Dim code As String = ""
            'Dim codeNext As String = ""
            'Dim i As Integer = 0
            'Dim di As DataGridItem
            'Dim diNext As DataGridItem

            'For i = 0 To DgvApprAssest.Items.Count - 1
            '    di = DgvApprAssest.Items(i)

            '    If i <> DgvApprAssest.Items.Count - 1 Then
            '        diNext = DgvApprAssest.Items(i + 1)
            '        code = di.Cells(TrovaIndiceColonna(DgvApprAssest, "CODICE")).Text
            '        codeNext = diNext.Cells(TrovaIndiceColonna(DgvApprAssest, "CODICE")).Text
            '        If codeNext.Length > code.Length Then
            '            If codeNext.Substring(0, code.Length) = code Then
            '                di.Cells(TrovaIndiceColonna(DgvApprAssest, "APPROVATO")).Text = di.Cells(TrovaIndiceColonna(DgvApprAssest, "APPROVATO")).Text.Substring(di.Cells(TrovaIndiceColonna(DgvApprAssest, "APPROVATO")).Text.IndexOf(">") + 1).Replace("</a>", "")

            '            End If
            '        End If

            '    Else
            '    End If


            'Next
            Dim code As String = ""
            Dim codeNext As String = ""
            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To DgvApprAssest.Items.Count - 1
                di = DgvApprAssest.Items(i)

                If di.Cells(TrovaIndiceColonna(DgvApprAssest, "TIPO")).Text = "MADRE" Then
                    di.Cells(TrovaIndiceColonna(DgvApprAssest, "DESCRIZIONE")).Text = di.Cells(TrovaIndiceColonna(DgvApprAssest, "DESCRIZIONE")).Text.Substring(di.Cells(TrovaIndiceColonna(DgvApprAssest, "DESCRIZIONE")).Text.IndexOf(">") + 1).Replace("</a>", "")
                    di.BackColor = Drawing.ColorTranslator.FromHtml("#eeeeee")
                Else
                    di.BackColor = Drawing.Color.Gainsboro
                End If
            Next
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = "SettaEditSottovoci - " & ex.Message
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

    Protected Sub btnRicarica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicarica.Click
        If soloLettura.Value = 0 Then
            CaricaVoci()
            SettaHLinkSottovoci()
        End If
    End Sub

    Protected Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConfirm.Click
        Try

            If ConfALerCompleto.Value = "1" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "update siscom_mi.pf_assestamento_voci set importo_approvato = nvl(importo,0) where (importo_approvato is null or importo_approvato = importo)"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "update siscom_mi.pf_assestamento set id_stato = 3 , data_app_aler = '" & Format(Now, "yyyyMMdd") & "' where id = " & IdAssestamento.Value
                par.cmd.ExecuteNonQuery()
                WriteEvent("F86", "IL GESTORE HA CONVALIDATO L'ASSESTAMENTO")
                Response.Write("<script>alert('Operazione eseguita correttamente!L\'assestamento ha ora stato: APPROVATO DAL GESTORE!');</script>")
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                CaricaVoci()

            End If
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = "Convalida - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub WriteEvent(ByVal CodEvento As String, Optional ByVal Motivazione As String = "")
        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_ASSESTAMENTO (ID_ASSESTAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_VOCE,ID_STRUTTURA,IMPORTO) VALUES " _
                                & "(" & IdAssestamento.Value & ", " & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                & "'" & CodEvento & "','" & par.PulisciStrSql(Motivazione) & "',null," & Session.Item("ID_STRUTTURA") & ",'0' )"
            par.cmd.ExecuteNonQuery()
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = "WriteEvent - " & ex.Message
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try
    End Sub

    Private Sub frmSoloLettura()
        Try
            btnConfirm.Visible = False
            soloLettura.Value = 1
            'btnStampa.Visible = True
            AssestamentoCompletato.Text = "APPROVATO DAL GESTORE"
            Response.Redirect("GestioneAssestamento.aspx?id=" & IdAssestamento.Value)
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = "frmSoloLettura - " & ex.Message

        End Try
    End Sub

    Private Sub GeneraPDF()
        Try

            '***************************************************************************************************
            '******************************************CREO IL FILE PDF*****************************************
            '***************************************************************************************************

            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)


            DgvApprAssestCopia.Width = 1100
            DgvApprAssestCopia.HeaderStyle.ForeColor = Drawing.Color.White


            DgvApprAssestCopia.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString

            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PageWidth = "1200"
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 60
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderText = "ASSESTAMENTO ESERCIZIO FINANZIARIO " & esercizio.Text & " - Convalida Gestore"
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

            Dim nomefile As String = "Exp_Assestamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile)
            Response.Write("<script>self.close;window.open('../../../FileTemp/" & nomefile & "','exptAssestamento','');</script>")


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            DgvApprAssestCopia.Width = 760

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = "GeneraPDF - " & ex.Message
        End Try

    End Sub

    'Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
    '    GeneraPDF()
    'End Sub

    Protected Sub btnindietro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnindietro.Click
        If URLdiProvenienza.Value <> "" Then
            Response.Redirect(URLdiProvenienza.Value)
        End If
    End Sub
End Class
