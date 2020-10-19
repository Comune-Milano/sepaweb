Imports System.Data.OleDb
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_GeneraEtichette
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim pdfConverter1 As PdfConverter = New PdfConverter

    Public Property datatableetichette() As Data.DataTable
        Get
            If Not (ViewState("datatableetichette") Is Nothing) Then
                Return ViewState("datatableetichette")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("datatableetichette") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
           & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
           & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
           & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
           & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
           & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
           & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
           & "</td></tr></table></div></div>"
        Response.Write(Loading)
        If Not IsPostBack Then
            Response.Flush()
            RicercaEtichette()
            Session.Remove("AMMSEL")
            Session.Remove("CONDSEL")
        End If
    End Sub
    Private Sub RicercaEtichette()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If Request.QueryString("TIPO") = 0 Then
                EtichetteAmministratori()
                Me.Page.Title = "Etichette Amministratori"
            ElseIf Request.QueryString("TIPO") = 1 Then
                EtichetteInquilini()
                Me.Page.Title = "Etichette Inquilini"
            End If
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            CostruisciTabella(datatableetichette)
            If Not String.IsNullOrEmpty(Me.lblEtichette.Text) Then
                Dim url As String = Server.MapPath("..\FileTemp\")
                Dim nomefile As String = "Etichette" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
                SettaPdf(pdfConverter1)
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(lblEtichette.Text, url & nomefile)
                'Response.Redirect("../FileTemp/" & nomefile)
                Response.Write("<script>parent.location.href=""../FileTemp/" & nomefile & """</script>")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: RicercaEtichette " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub EtichetteAmministratori()
        par.cmd.CommandText = "SELECT titolo || ' ' || cognome || ' ' || cond_amministratori.nome as rigo1, " _
                    & "(tipo_indirizzo || ' ' || indirizzo || ' ' || civico) AS rigo2, " _
                    & "comuni_nazioni.nome || ' ' || cond_amministratori.cap || ' (' || comuni_nazioni.sigla || ')' as rigo3 " _
                    & "FROM siscom_mi.COND_AMMINISTRATORI,COMUNI_NAZIONI " _
                    & "WHERE COMUNI_NAZIONI.cod = cond_amministratori.cod_comune and cond_amministratori.id in (" & Session.Item("AMMSEL") & ") " _
                    & "order by 1"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        datatableetichette = New Data.DataTable
        da.Fill(datatableetichette)
    End Sub
    Private Sub EtichetteInquilini()
        par.cmd.CommandText = "SELECT replace(siscom_mi.getintestatari(unita_contrattuale.id_contratto,0),';','') AS rigo1, " _
                    & "(tipo_cor || ' ' || rapporti_utenza.via_cor || ' ' || civico_cor) AS rigo2, " _
                    & "luogo_cor || ' ' || cap_cor || ' (' || sigla_cor || ')' as rigo3 " _
                    & "FROM siscom_mi.unita_contrattuale, siscom_mi.rapporti_utenza " _
                    & "WHERE id_unita IN (SELECT id_ui FROM siscom_mi.cond_ui WHERE id_condominio IN (" & Session.Item("CONDSEL") & ")) " _
                    & "AND SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)<>'CHIUSO' " _
                    & "AND rapporti_utenza.ID = unita_contrattuale.id_contratto " _
                    & "order by 2,1"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        datatableetichette = New Data.DataTable
        da.Fill(datatableetichette)
    End Sub
    Private Sub CostruisciTabella(ByVal dt As Data.DataTable)
        If dt.Rows.Count > 0 Then
            Me.lblEtichette.Text = "<table align='center'>"
            Dim numRigheStampate As Integer = 0
            Dim numEtichetteStampate As Integer = 0
            Dim primoRigo As Boolean = True
            For Each r As Data.DataRow In dt.Rows
                If numEtichetteStampate Mod 3 = 0 Then
                    If primoRigo = False Then
                        Me.lblEtichette.Text = lblEtichette.Text & "</tr>"
                        If numRigheStampate Mod 12 = 0 And numRigheStampate > 0 Then
                            Me.lblEtichette.Text = lblEtichette.Text & "</table>"
                            Me.lblEtichette.Text = lblEtichette.Text & "<p style='page-break-after: always'></p>"
                            Me.lblEtichette.Text = lblEtichette.Text & "<table align='center'>"
                        End If
                        Me.lblEtichette.Text = lblEtichette.Text & "<tr>"
                    Else
                        Me.lblEtichette.Text = lblEtichette.Text & "<tr>"
                        primoRigo = False
                    End If
                    numRigheStampate = numRigheStampate + 1
                End If
                TestoEtichetta(par.IfNull(r.Item("rigo1"), ""), par.IfNull(r.Item("rigo2"), ""), par.IfNull(r.Item("rigo3"), ""))
                numEtichetteStampate = numEtichetteStampate + 1
            Next
            If (numEtichetteStampate Mod 3) <> 0 Then
                For i As Integer = 1 To (3 - (numEtichetteStampate Mod 3))
                    TestoEtichetta("&nbsp;", "&nbsp;", "&nbsp;")
                Next
            End If
            'While (3 - (numEtichetteStampate Mod 3)) <> 0
            '    TestoEtichetta("&nbsp;", "&nbsp;", "&nbsp;")
            '    numEtichetteStampate += 1
            'End While
            Me.lblEtichette.Text = lblEtichette.Text & "</tr>"
            Me.lblEtichette.Text = lblEtichette.Text & "</table>"
        End If
    End Sub
    Private Sub TestoEtichetta(ByVal rigo1 As String, ByVal rigo2 As String, ByVal rigo3 As String)
        Me.lblEtichette.Text = lblEtichette.Text & "<td style='width: 415px; height: 145px; vertical-align: middle; font-family :Arial; font-size :20pt; font-weight:bold;' >"
        Me.lblEtichette.Text = lblEtichette.Text & "<table style='width: 90%;' cellpadding='0' cellspacing='0'>"
        Me.lblEtichette.Text = lblEtichette.Text & "<tr><td>" & rigo1 & "</td></tr>"
        Me.lblEtichette.Text = lblEtichette.Text & "<tr><td>" & rigo2 & "</td></tr>"
        Me.lblEtichette.Text = lblEtichette.Text & "<tr><td>" & rigo3 & "</td></tr>"
        Me.lblEtichette.Text = lblEtichette.Text & "</table>"
        Me.lblEtichette.Text = lblEtichette.Text & "</td>"
    End Sub
    Private Sub SettaPdf(ByVal pdf As PdfConverter)
        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
        If Licenza <> "" Then
            pdfConverter1.LicenseKey = Licenza
        End If
        pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
        pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        pdfConverter1.PdfDocumentOptions.ShowHeader = False
        pdfConverter1.PdfDocumentOptions.ShowFooter = False
        pdfConverter1.PdfHeaderOptions.HeaderHeight = 35
        pdfConverter1.PdfDocumentOptions.LeftMargin = 10
        pdfConverter1.PdfDocumentOptions.RightMargin = 0
        pdfConverter1.PdfDocumentOptions.TopMargin = 0
        pdfConverter1.PdfDocumentOptions.BottomMargin = 0
        pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
        pdfConverter1.PageWidth = 1250
        pdfConverter1.PdfFooterOptions.FooterText = ""
        pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
        pdfConverter1.PdfFooterOptions.DrawFooterLine = False
        'Dim sr1 As StreamReader = New StreamReader(Server.MapPath("PrintEtichette.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
        'Dim contenuto As String = sr1.ReadToEnd()
        'sr1.Close()
        'Response.Flush()
        'dgvRptPagExtraMav.RenderControl(sourcecode)
        'sourcecode.Flush()
        'Html = stringWriter.ToString
        'pdfConverter1.SavePdfFromHtmlStringToFile(par.EliminaLink(Html), url & nomefile)
        'Response.Write("<script>window.open('../FileTemp/" & nomefile & "','Etichetta','')</script>")
    End Sub
End Class
