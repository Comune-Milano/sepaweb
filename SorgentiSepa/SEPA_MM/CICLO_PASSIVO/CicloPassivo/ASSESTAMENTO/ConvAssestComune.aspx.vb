Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_ConvAssestComune
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or (Session.Item("BP_GENERALE") <> 1 And Session.Item("MOD_ASS_CONV_COMU") <> 1) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

           
            '##### CARICAMENTO PAGINA #####
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
                Response.Flush()
                CaricaVoci()
                TipoAllegato.Value = par.getIdOggettoAllegatiWs("Assestamento")
                If Session.Item("MOD_ASS_CONV_COMU") <> 1 Then
                    'Response.Write("<script>alert('Operatore non abilitato alla Convalida Comune dell\'assestamento!');</script>")
                    btnConfirm.Visible = False
                    NoConvalida.Visible = False
                End If

            End If


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub CaricaVoci()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim ElVoci As String = ""
            Dim Approvato As Decimal = 0
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader
            Dim indiceColonna As Integer = TrovaIndiceColonna(DgvApprAssest, "APPROVATO")
            Dim indiceColonnaCap As Integer = TrovaIndiceColonna(DgvApprAssCapitoli, "ASSESTAMENTO")

            If IdAssestamento.Value = 0 Then
                If par.IfEmpty(Request.QueryString("ID"), "") <> "" Then
                    IdAssestamento.Value = Request.QueryString("ID")
                    par.cmd.CommandText = "SELECT ID,ID_STATO FROM SISCOM_MI.PF_ASSESTAMENTO WHERE ID=" & IdAssestamento.Value
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        If par.IfNull(lettore(1), 3) = 5 Then
                            lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL COMUNE"
                            DgvApprAssest.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                            DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                        ElseIf par.IfNull(lettore(1), 3) = 3 Then
                            lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL GESTORE"
                            DgvApprAssest.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                            DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                        End If
                    End If
                    lettore.Close()
                Else
                    'par.cmd.CommandText = "select max(id) from siscom_mi.pf_assestamento where (id_stato = 3 or id_stato = 5)"
                    par.cmd.CommandText = "SELECT MAX(ID),ID_STATO FROM SISCOM_MI.PF_ASSESTAMENTO WHERE (ID_STATO=3 OR ID_STATO=5) GROUP BY ID_STATO ORDER BY MAX(ID) DESC"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        IdAssestamento.Value = par.IfNull(lettore(0), 0)
                        If par.IfNull(lettore(1), 3) = 5 Then
                            lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL COMUNE"
                            DgvApprAssest.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                            DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                        ElseIf par.IfNull(lettore(1), 3) = 3 Then
                            lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL GESTORE"
                            DgvApprAssest.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                            DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
                        End If
                    End If
                    lettore.Close()
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
                esercizio.Text = par.IfNull(lettoreEsercizio("inizio"), "") & " - " & par.IfNull(lettoreEsercizio("fine"), "")
                If par.IfNull(lettoreEsercizio("id_stato"), 1) = 5 Then
                    lblAssestamentoData.Text = "Assestamento approvato il " & par.IfNull(lettoreEsercizio("data_Ass"), "")
                Else
                    lblAssestamentoData.Text = "Assestamento del " & par.IfNull(lettoreEsercizio("data_Ass"), "")
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

            Me.DgvApprAssest.DataSource = dt
            Me.DgvApprAssest.DataBind()

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
                    frmSoloLettura()
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

    Private Sub frmSoloLettura()
        Try

            Me.btnConfirm.Visible = False
            NoConvalida.Visible = False
            soloLettura.Value = 1

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "frmSoloLettura - " & ex.Message

        End Try
    End Sub

    Protected Sub btnSalvaNoteRifiuto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaNoteRifiuto.Click
        Try
            If salvaok.Value = 1 Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "update siscom_mi.pf_assestamento set note_comune = '" & par.PulisciStrSql(Me.TxtNote.Text.ToUpper) & "', id_stato = 1 where id = " & IdAssestamento.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "update siscom_mi.pf_assestamento_VOCI set IMPORTO_APPROVATO=NULL,completo = 0 where id_ASSESTAMENTO = " & IdAssestamento.Value
                par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "update siscom_mi.pf_assestamento_voci set completo = 0 where id_assestamento = " & IdAssestamento.Value
                'par.cmd.ExecuteNonQuery()
                Response.Write("<script>alert('Operazione eseguita correttamente!L\'assestamento non è stato approvato!');location.href='ScegliAssestamento.aspx';</script>")
                WriteEvent("F87", "ASSESTAMENTO NON APPROVATO DAL COMUNE")
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                'CaricaVoci()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "btnSalvaNoteRifiuto - " & ex.Message
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    Protected Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConfirm.Click
        Dim indiceColonna As Integer = TrovaIndiceColonna(DgvApprAssest, "APPROVATO")
        Dim indiceColonnaCap As Integer = TrovaIndiceColonna(DgvApprAssCapitoli, "ASSESTAMENTO")
        Try
            If ConfCompleto.Value = 1 Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                DgvApprAssest.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL COMUNE"
                lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL COMUNE"

                '******MODIFICA STATO ASSESTAMENTO******
                par.cmd.CommandText = "update siscom_mi.pf_assestamento set id_stato = 5, data_app_comune = " & Format(Now, "yyyyMMdd") & " where id = " & IdAssestamento.Value
                par.cmd.ExecuteNonQuery()
                '***************************************


                '******UPDATE ASSESTAMENTO_VALORE_LORDO IN PF_VOCI_STRUTTURA PER LE VOCI COINVOLTE******* 'MODIFICATO IL 23092011 IMPORTO_APPROVATO>0 CON IMPORTO_APPROVATO<>0 
                par.cmd.CommandText = "UPDATE siscom_mi.pf_voci_struttura SET assestamento_valore_lordo = nvl(assestamento_valore_lordo,0) + " _
                                    & "nvl((SELECT importo_approvato FROM siscom_mi.pf_assestamento_voci " _
                                    & "WHERE id_assestamento = " & IdAssestamento.Value & " AND pf_assestamento_voci.id_voce = pf_voci_struttura.id_voce " _
                                    & "AND pf_assestamento_voci.id_struttura= pf_voci_struttura.id_struttura " _
                                    & "AND importo_approvato<>0),0)"
                par.cmd.ExecuteNonQuery()
                '****************************************************************************************


                '******RECUPERO VOCI RIEPILOGATIVE******* 'MODIFICATO IL 23092011 IMPORTO_APPROVATO>0 CON IMPORTO_APPROVATO<>0
                par.cmd.CommandText = "SELECT PF_VOCI.ID_VOCE_MADRE AS ID_V,ID_STRUTTURA " _
                    & "FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI, SISCOM_MI.PF_VOCI " _
                    & "WHERE IMPORTO_APPROVATO <> 0 AND ID_ASSESTAMENTO='" & IdAssestamento.Value & "' " _
                    & "AND PF_VOCI.ID=PF_ASSESTAMENTO_VOCI.ID_VOCE " _
                    & "AND PF_VOCI.ID_VOCE_MADRE IN (SELECT PF_VOCI.ID FROM SISCOM_MI.PF_VOCI " _
                    & "WHERE LEVEL = 2 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & "START WITH PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE IS NULL)) " _
                    & "GROUP BY PF_VOCI.ID_VOCE_MADRE,ID_STRUTTURA"

                Dim lettoreVoci As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While lettoreVoci.Read
                    '*******AGGIORNAMENTO DELL'ASSESTAMENTO IN PF_VOCI_STRUTTURA*******
                    Dim IdVoceA As String = par.IfNull(lettoreVoci("ID_V"), "")
                    Dim IdStrutt As String = par.IfNull(lettoreVoci("ID_STRUTTURA"), "")
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET ASSESTAMENTO_VALORE_LORDO=NVL(ASSESTAMENTO_VALORE_LORDO,0)+ " _
                            & "(SELECT SUM(NVL(ASSESTAMENTO_VALORE_LORDO,0)) FROM SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI MADRE " _
                            & "WHERE PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID " _
                            & "AND MADRE.ID=PF_VOCI.ID_VOCE_MADRE " _
                            & "AND MADRE.ID ='" & IdVoceA & "' AND ID_STRUTTURA='" & IdStrutt & "') " _
                            & "WHERE PF_VOCI_STRUTTURA.ID_VOCE='" & IdVoceA & "' " _
                            & "AND PF_VOCI_STRUTTURA.ID_STRUTTURA='" & IdStrutt & "'"
                    par.cmd.ExecuteNonQuery()
                End While
                lettoreVoci.Close()
                '***************************************************

                WriteEvent("F86", "ASSESTAMENTO APPROVATO DAL COMUNE")
                par.myTrans.Commit()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                frmSoloLettura()
                Response.Write("<script>alert('Operazione eseguita correttamente!L\'assestamento è stato approvato!');</script>")
            End If
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            DgvApprAssest.Columns.Item(indiceColonna).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
            DgvApprAssCapitoli.Columns.Item(indiceColonnaCap).HeaderText = "ASSESTAMENTO APPROVATO DAL GESTORE"
            lblAssestamento.Text = "ASSESTAMENTO APPROVATO DAL GESTORE"
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
            lblErrore.Text = "Procedi - " & ex.Message
        End Try
    End Sub

    Private Sub GeneraPDF()
        If Session.Item("MOD_ASS_CONV_COMU") <> 1 Then
            titStampa.Value = ""
        Else
            titStampa.Value = " - Convalida COMUNE"
        End If
        Try
            Dim txtTitolo As String = ""
            '***************************************************************************************************
            '******************************************CREO IL FILE PDF*****************************************
            '***************************************************************************************************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT INIZIO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID AND PF_MAIN.ID_STATO = 5"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                txtTitolo = "ASSESTAMENTO ESERCIZIO FINANZIARIO " & par.IfNull(lettore("INIZIO"), "0000").ToString.Substring(0, 4) & titStampa.Value
            End If
            lettore.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)

            DgvApprAssCapitoli.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString

            stringWriter = New System.IO.StringWriter
            sourcecode = New HtmlTextWriter(stringWriter)
            DgvApprAssest.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = Html & "<br/><br/><br/><br/>" & stringWriter.ToString


            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

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
            pdfConverter1.PdfHeaderOptions.HeaderText = txtTitolo
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


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "GeneraPDF - " & ex.Message
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


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_ASSESTAMENTO (ID_ASSESTAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_VOCE,IMPORTO) VALUES " _
                                & "(" & IdAssestamento.Value & ", " & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                & "'" & CodEvento & "','" & par.PulisciStrSql(Motivazione) & "',null,'0' )"
            par.cmd.ExecuteNonQuery()


            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "WriteEvent - " & ex.Message
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
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

    Protected Sub btnIndietro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro.Click
        Response.Redirect("ScegliAssestamento.aspx")
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        GeneraPDF()
    End Sub

    Protected Sub imgEsporta_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgEsporta.Click
        Try
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportApprAssest", "ExportApprAssest", DgvApprAssest)
            Dim nomeFile1 = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportApprAssCapitoli", "ExportApprAssCapitoli", DgvApprAssCapitoli)
            Dim strmZipOutputStream As ZipOutputStream = Nothing
            Dim nome As String = "ExportAssestamento-" & Format(Now, "yyyyMMddHHmmss")
            Dim objCrc32 As New Crc32()
            Dim zipfic As String
            zipfic = Server.MapPath("..\..\..\FileTemp\" & nome & ".zip")
            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            Dim strFile As String
            Dim strmFile As FileStream
            Dim theEntry As ZipEntry
            Dim contatore As Integer = 0
            Dim ElencoFile() As String = Nothing
            Dim fileexport As String = ""
            For i As Integer = 0 To 1

                If i = 0 Then
                    fileexport = nomeFile
                ElseIf i = 1 Then
                    fileexport = nomeFile1
                End If
                contatore += 1
                strFile = Server.MapPath("~\/FileTemp\/") & fileexport
                strmFile = File.OpenRead(strFile)
                Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                Dim sFile As String = Path.GetFileName(strFile)
                theEntry = New ZipEntry(sFile)
                Dim fi As New FileInfo(strFile)
                theEntry.DateTime = fi.LastWriteTime
                theEntry.Size = strmFile.Length
                strmFile.Close()
                objCrc32.Reset()
                objCrc32.Update(abyBuffer)
                theEntry.Crc = objCrc32.Value
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                ReDim Preserve ElencoFile(i)
                ElencoFile(i) = "..\..\..\FileTemp\" & nomeFile

            Next
            strmZipOutputStream.Close()

            If Not String.IsNullOrEmpty(nome) Then
                If System.IO.File.Exists(Server.MapPath("~\/FileTemp\/") & nome & ".zip") Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('File creato correttamente');", True)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "document.getElementById('splash').style.visibility = 'hidden';avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('..\/..\/..\/FileTemp\/" & nome & ".zip','','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('Il file non è stato creato correttamente!\nRiprovare o contattare l\'amministratore di Sistema!');", True)
                End If
            End If
            'If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            '    ' Response.Redirect("../../../FileTemp/" & nomeFile, False)
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "document.getElementById('splash').style.visibility = 'hidden';avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('..\/..\/..\/FileTemp\/" & nomeFile & "','','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            'Else
            '    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            '    'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
            'End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "WriteEvent - " & ex.Message
        End Try
    End Sub
End Class
